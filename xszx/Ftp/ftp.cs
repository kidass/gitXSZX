/*==========================================================================
  File     : ftp.cs
  Summary  : This file implements the ftp:// protocol
             using the Pluggable protocol feature 
             of System.Net namespace
  Namespace: System.Net
==========================================================================*/
using System;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;

namespace Xydc
{
    namespace Net
    {
        // 支持的FTP命令如下：
        // Method   FTP命令     操作含义
        // GET      -> RETR       -> DOWNLOAD
        // PUT      -> STOR       -> UPLOAD
        // LIST     -> LIST       -> LIST
        // CD       -> CWD        -> ChangeDir
        // PWD      -> PWD        -> GetCurrentDirectory
        // DELETE   -> DELE       -> Delete
        // MD       -> MKD        -> MakeDir
        // RD       -> RMD        -> RemoveDir
        // REN      -> RNFR+RNTO  -> Rename from ... to ...

        //
        // FTP命令类型
        //
        public enum FtpCommandType
        {
            FtpControlCommand = 1,
            FtpDataReceiveCommand = 2,
            FtpDataSendCommand = 3,
            FtpCommandNotSupported = 4,
        }

        // 
        // FTP响应描述类
        //
        public class ResponseDescription
        {
            private string m_szStatusDescription = "";   //状态描述串
            private int m_dwStatus = 0;               //状态码

            internal ResponseDescription()
            {
                m_szStatusDescription = "";
                m_dwStatus = 0;
            }

            public int Status
            {
                get
                {
                    return m_dwStatus;
                }
                set
                {
                    m_dwStatus = value;
                }
            }
            public string StatusDescription
            {
                get
                {
                    return m_szStatusDescription;
                }
                set
                {
                    m_szStatusDescription = value;
                }
            }
            public bool PositivePreliminary
            {
                get
                {
                    return (m_dwStatus / 100 == 1);
                }
            }
            public bool PositiveCompletion
            {
                get
                {
                    return (m_dwStatus / 100 == 2);
                }
            }
            public bool PositiveIntermediate
            {
                get
                {
                    return (m_dwStatus / 100 == 3);
                }
            }
            public bool TransientNegativeCompletion
            {
                get
                {
                    return (m_dwStatus / 100 == 4);
                }
            }
            public bool PermanentNegativeCompletion
            {
                get
                {
                    return (m_dwStatus / 100 == 5);
                }
            }
        }

        // 
        // WebRequest 可插接协议必须实现！
        //
        
        public class FtpRequestCreator:System.Net.IWebRequestCreate
        {
            public FtpRequestCreator()
            {
            }
            public WebRequest Create(Uri Url)
            {
                return new FtpWebRequest(Url);
            }
        }

        // 
        // FTP流
        //
        internal class FtpStream : System.IO.Stream
        {

            private Stream m_Stream = null;        //流对象
            private bool m_fCanRead = false;       //能读？
            private bool m_fCanWrite = false;       //能写？
            private bool m_fCanSeek = false;       //能定位？
            private bool m_fClosedByUser = false;       //用户已关闭流？

            internal FtpStream()
            {
                m_Stream = null;
                m_fCanRead = false;
                m_fCanWrite = false;
                m_fCanSeek = false;
                m_fClosedByUser = false;
            }
            internal FtpStream(Stream stream, bool canread, bool canwrite, bool canseek)
            {
                m_Stream = stream;
                m_fCanRead = canread;
                m_fCanWrite = canwrite;
                m_fCanSeek = canseek;
                m_fClosedByUser = false;
            }

            internal long InternalPosition
            {
                get
                {
                    return m_Stream.Position;
                }
                set
                {
                    m_Stream.Position = value;
                }
            }
            internal long InternalLength
            {
                get
                {
                    return m_Stream.Length;
                }
            }

            internal void InternalWrite(Byte[] buffer, int offset, int length)
            {
                m_Stream.Write(buffer, offset, length);
            }
            internal int InternalRead(Byte[] buffer, int offset, int length)
            {
                return m_Stream.Read(buffer, offset, length);
            }
            internal void InternalClose()
            {
                try
                {
                    if (m_Stream != null)
                        m_Stream.Close();
                }
                catch { }
            }
            internal Stream GetStream()
            {
                return m_Stream;
            }

            public override bool CanRead
            {
                get
                {
                    return m_fCanRead;
                }
            }
            public override bool CanWrite
            {
                get
                {
                    return m_fCanWrite;
                }
            }
            public override bool CanSeek
            {
                get
                {
                    return m_fCanSeek;
                }
            }
            public override long Length
            {
                get
                {
                    throw new System.NotSupportedException("Error: This stream cannot be seeked!");
                }
            }
            public override long Position
            {
                get
                {
                    throw new System.NotSupportedException("Error: This stream cannot be seeked!");
                }
                set
                {
                    throw new System.NotSupportedException("Error: This stream cannot be seeked!");
                }
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                throw new System.NotSupportedException("Error: This stream cannot be seeked!");
            }
            public override void Flush()
            {
                try
                {
                    if (m_Stream != null)
                        m_Stream.Flush();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            public override void SetLength(long value)
            {
                throw new System.NotSupportedException("Error: This stream cannot be seeked!");
            }
            public override void Close()
            {
                try
                {
                    m_fClosedByUser = true;
                    if (m_Stream != null)
                        m_Stream.Close();
                }
                catch { }
            }
            public override void Write(Byte[] buffer, int offset, int length)
            {
                if (m_fClosedByUser)
                    throw new System.IO.IOException("Error: Cannot operate on a closed stream!");
                InternalWrite(buffer, offset, length);
            }
            public override int Read(Byte[] buffer, int offset, int length)
            {
                if (m_fClosedByUser)
                    throw new System.IO.IOException("Error: Cannot operate on a closed stream!");
                return InternalRead(buffer, offset, length);
            }
        }

        // 
        // FTP响应类
        //
        public class FtpWebResponse : System.Net.WebResponse
        {
            private Stream m_ResponseStream;
            private int m_StatusCode;
            private String m_StatusDescription;
            private String m_ContentType;
            private String m_Log;

            internal FtpWebResponse()
            {
                m_ResponseStream = null;
                m_StatusDescription = null;
                m_Log = null;
                m_ContentType = null;
                m_StatusCode = -1;
            }
            internal FtpWebResponse(int StatusCode, string StatusDescription, String Log)
            {
                m_StatusCode = StatusCode;
                m_StatusDescription = StatusDescription;
                m_Log = Log;
                m_ContentType = null;
                m_ResponseStream = Stream.Null;
            }

            internal void SetDownloadStream(Stream datastream)
            {
                m_ResponseStream = datastream;
                m_ResponseStream.Position = 0;
            }

            public override String ContentType
            {
                get
                {
                    return m_ContentType;
                }
                set
                {
                    throw new System.NotSupportedException("Error: [ContentType] cannot be set!");
                }
            }
            public int Status
            {
                get
                {
                    return m_StatusCode;
                }
            }
            public String StatusDescription
            {
                get
                {
                    return m_StatusDescription;
                }
            }
            public string TransactionLog
            {
                get
                {
                    return m_Log;
                }
            }

            public override Stream GetResponseStream()
            {
                if (m_ResponseStream == null)
                    throw new System.ApplicationException("Error: No response stream for this kind of method!");
                return m_ResponseStream;
            }
            public override void Close()
            {
                try
                {
                    if (m_ResponseStream != null)
                    {
                        m_ResponseStream.Close();
                        m_ResponseStream = null;
                    }
                }
                catch { }
            }
        }

        //
        // 所有Ftp请求的入口点
        //
        public class FtpWebRequest : System.Net.WebRequest
        {
            public const int BUFFER_SIZE = 1024;   //流缓冲区大小
            public String m_szCmdParameter;     //FTP命令参数
            public String m_szFromFile;         //Rename from
            public String m_szToFile;           //Rename to
            private const int SOCKET_ERROR = -1;    //scoket错误标志
            private Socket m_DataSocket;         //数据socket
            private Socket m_ControlSocket;	     //控制socket
            private Uri m_RequestUri;         //请求uri
            private Uri m_ProxyUri;           //代理uri
            private Stream m_RequestStream;      //请求流
            private ICredentials m_Credentials;        //认证对象
            private IWebProxy m_Proxy;              //web代理对象
            private UriBuilder m_ServicePoint;       //主机名、端口信息
            private bool m_bPassiveMode;       //传输模式
            private long m_dwContentLength;    //传输字节长度
            private string m_szContentType;      //传输类型：ascii,binary
            private StringBuilder m_sbControlSocketLog; //日志记录
            private string m_szMethod;           //对外的ftp命令
            private String m_szServerMethod;     //ftp支持的命令
            private FtpCommandType m_CommandType;        //ftp命令类型
            private int m_CommandSendTimeout; //命令发送超时(过指定时间未发出命令:毫秒)
            private int m_DataReceiveTimeout; //数据接收超时(过指定时间未收到数据:毫秒)
            private int m_DataSendTimeout;    //数据发送超时(过指定时间未发送数据:毫秒)
            private int m_ConnectTimeout;     //建立连接超时(过指定时间未建立连接:毫秒)

            internal FtpWebRequest()
            {
                m_RequestStream = null;
                m_szCmdParameter = null;
                m_szFromFile = null;
                m_szToFile = null;
                m_DataSocket = null;
                m_ControlSocket = null;
                m_RequestUri = null;
                m_ProxyUri = null;
                m_Credentials = null;
                m_Proxy = null;
                m_ServicePoint = null;
                m_sbControlSocketLog = null;
                m_szServerMethod = null;
                m_szContentType = "binary";
                m_szMethod = "dir";
                m_dwContentLength = 0;
                m_CommandSendTimeout = 60000;
                m_DataReceiveTimeout = 60000;
                m_ConnectTimeout = 60000;
                m_DataSendTimeout = 60000;
                m_bPassiveMode = false;
            }
            public FtpWebRequest(Uri Url)
            {
                if (Url.Scheme.ToLower() != "ftp")
                    throw new System.NotSupportedException("Error: This protocol is not supported!");
                m_sbControlSocketLog = new StringBuilder();
                m_ServicePoint = new UriBuilder(Url);
                m_szCmdParameter = Url.AbsolutePath;
                m_RequestUri = Url;
                m_CommandSendTimeout = 60000;
                m_DataReceiveTimeout = 60000;
                m_ConnectTimeout = 60000;
                m_DataSendTimeout = 60000;
                m_szMethod = "dir";
                m_szFromFile = "";
                m_szToFile = "";
                m_bPassiveMode = false;
            }

            //***************************************************************************************
            // FtpWebRequest 扩展特性 
            //***************************************************************************************
            public bool Passive
            {
                set
                {
                    m_bPassiveMode = value;
                }
                get
                {
                    return m_bPassiveMode;
                }
            }
            public int DataSendTimeout
            {
                set
                {
                    m_DataSendTimeout = value;
                }
                get
                {
                    return m_DataSendTimeout;
                }
            }
            public int DataReceiveTimeout
            {
                set
                {
                    m_DataReceiveTimeout = value;
                }
                get
                {
                    return m_DataReceiveTimeout;
                }
            }
            public int ConnectTimeout
            {
                set
                {
                    m_ConnectTimeout = value;
                }
                get
                {
                    return m_ConnectTimeout;
                }
            }
            public int CommandTimeout
            {
                set
                {
                    m_CommandSendTimeout = value;
                }
                get
                {
                    return m_CommandSendTimeout;
                }
            }
            public String FromFile
            {
                set
                {
                    m_szFromFile = value;
                }
                get
                {
                    return m_szFromFile;
                }
            }
            public String ToFile
            {
                set
                {
                    m_szToFile = value;
                }
                get
                {
                    return m_szToFile;
                }
            }

            //***************************************************************************************
            // FtpWebRequest 重载特性
            //***************************************************************************************
            public override String Method
            {
                get
                {
                    return m_szMethod;
                }
                set
                {
                    if (value == null)
                        value = "";
                    value = value.Trim();
                    if (value == "")
                        throw new System.NotSupportedException("Error: [Method] must be specified!");
                    m_szServerMethod = GetServerCommand(value);
                    m_CommandType = FindCommandType(m_szServerMethod);
                    if (m_CommandType == FtpCommandType.FtpCommandNotSupported)
                        throw new System.NotSupportedException("Error: [" + value + "] is not supported!");
                    m_szMethod = value;
                }
            }
            public override ICredentials Credentials
            {
                get
                {
                    return m_Credentials;
                }
                set
                {
                    m_Credentials = value;
                }
            }
            public override string ConnectionGroupName
            {
                get
                {
                    throw new System.NotSupportedException("Error: [ConnectionGroupName] is not supported!");
                }
                set
                {
                    throw new System.NotSupportedException("Error: [ConnectionGroupName] is not supported!");
                }
            }
            public override long ContentLength
            {
                get
                {
                    return m_dwContentLength;
                }
                set
                {
                    m_dwContentLength = value;
                }
            }
            public override string ContentType
            {
                get
                {
                    return m_szContentType;
                }
                set
                {
                    m_szContentType = value;
                }
            }
            public override IWebProxy Proxy
            {
                get
                {
                    return m_Proxy;
                }
                set
                {
                    m_Proxy = value;
                }
            }

            private void memset(ref byte[] array, int start, int length)
            {
                for (int i = 0; i < length; i++)
                    array[i] = 0;
            }

            private void SafeRelease(ref Socket obj)
            {
                if (obj != null)
                {
                    try { obj.Shutdown(SocketShutdown.Both); }
                    catch { }
                    try { obj.Close(); }
                    catch { }
                    obj = null;
                }
            }
            private void SafeRelease(ref MemoryStream obj)
            {
                if (obj != null)
                {
                    try { obj.Close(); }
                    catch { }
                    obj = null;
                }
            }
            private void SafeRelease(ref Stream obj)
            {
                if (obj != null)
                {
                    try { obj.Close(); }
                    catch { }
                    obj = null;
                }
            }
            private void SafeRelease(ref FtpStream obj)
            {
                if (obj != null)
                {
                    try { obj.Close(); }
                    catch { }
                    obj = null;
                }
            }

            private void CloseControlConnection()
            {
                SafeRelease(ref m_ControlSocket);
            }
            public void Close()
            {
                SafeRelease(ref m_ControlSocket);
                SafeRelease(ref m_DataSocket);
            }
            public void CloseDataConnection()
            {
                SafeRelease(ref m_DataSocket);
            }

            // 解析用户指定的FTP操作，返回对应的FTP命令
            public static String GetServerCommand(String szCommand)
            {
                if (szCommand == null)
                    szCommand = "";
                szCommand = szCommand.Trim();
                if (szCommand == "")
                    throw new System.ArgumentNullException("Command");

                string szCmd = szCommand.ToLower();
                string szRet = null;

                if (szCmd == "dir")
                    szRet = "LIST";
                else if (szCmd == "get")
                    szRet = "RETR";
                else if (szCmd == "cd")
                    szRet = "CWD";
                else if (szCmd == "pwd")
                    szRet = "PWD";
                else if (szCmd == "put")
                    szRet = "STOR";
                else if (szCmd == "delete")
                    szRet = "DELE";
                else if (szCmd == "md")
                    szRet = "MKD";
                else if (szCmd == "rd")
                    szRet = "RMD";
                else if (szCmd == "ren")
                    szRet = "REN";

                if (szRet == null)
                    throw new System.NotSupportedException(szCommand);

                return szRet;
            }

            // 对FTP命令进行分类
            public static FtpCommandType FindCommandType(String szCommand)
            {
                if (szCommand == null)
                    szCommand = "";
                szCommand = szCommand.Trim();
                szCommand = szCommand.ToUpper();

                if (szCommand.Equals("USER")
                    || szCommand.Equals("PASS")
                    || szCommand.Equals("CWD")
                    || szCommand.Equals("PWD")
                    || szCommand.Equals("CDUP")
                    || szCommand.Equals("DELE")
                    || szCommand.Equals("MKD")
                    || szCommand.Equals("RMD")
                    || szCommand.Equals("REN")
                    || szCommand.Equals("RNFR")
                    || szCommand.Equals("RNTO")
                    || szCommand.Equals("QUIT"))
                    return FtpCommandType.FtpControlCommand;
                else if (szCommand.Equals("RETR")
                    || szCommand.Equals("LIST"))
                    return FtpCommandType.FtpDataReceiveCommand;
                else if (szCommand.Equals("STOR")
                    || szCommand.Equals("STOU"))
                    return FtpCommandType.FtpDataSendCommand;
                else
                    return FtpCommandType.FtpCommandNotSupported;
            }

            // 将Address(UInt32)、Port(int)合并为地址字符串
            private String FormatAddress(UInt32 Address, int Port)
            {
                StringBuilder sb = new StringBuilder(32);
                UInt32 Quotient = Address;
                UInt32 Remainder = 0;

                while (Quotient != 0)
                {
                    Remainder = Quotient % 256;
                    Quotient = Quotient / 256;
                    sb.Append(Remainder);
                    sb.Append(',');
                }
                sb.Append(Port / 256);
                sb.Append(',');
                sb.Append(Port % 256);

                return sb.ToString();
            }

            // 将Address(IPAddress)、Port(int)合并为地址字符串
            private String FormatAddress(IPAddress Address, int Port)
            {
                StringBuilder sb = new StringBuilder(32);
                String szAddress = "";

                szAddress = Address.ToString();
                szAddress = szAddress.Replace('.', ',');
                sb.Append(szAddress);
                sb.Append(',');
                sb.Append(Port / 256);
                sb.Append(',');
                sb.Append(Port % 256);

                return sb.ToString();
            }

            // 从PASV响应字符串中获取地址字符串
            private String getIPAddress(String str)
            {
                StringBuilder IPstr = new StringBuilder(32);
                String Substr = null;

                int pos1 = str.IndexOf("(") + 1;
                int pos2 = str.IndexOf(",");

                for (int i = 0; i < 3; i++)
                {
                    Substr = str.Substring(pos1, pos2 - pos1) + ".";
                    IPstr.Append(Substr);
                    pos1 = pos2 + 1;
                    pos2 = str.IndexOf(",", pos1);
                }

                Substr = str.Substring(pos1, pos2 - pos1);
                IPstr.Append(Substr);

                return IPstr.ToString();
            }

            // 从PASV响应字符串中获取端口字符串
            private int getPort(String str)
            {
                int pos1 = str.IndexOf("(");
                int pos2 = str.IndexOf(",");
                int Port = 0;

                //skip the ip addresss
                for (int i = 0; i < 3; i++)
                {
                    pos1 = pos2 + 1;
                    pos2 = str.IndexOf(",", pos1);
                }
                pos1 = pos2 + 1;
                pos2 = str.IndexOf(",", pos1);
                String PortSubstr1 = str.Substring(pos1, pos2 - pos1);

                pos1 = pos2 + 1;
                pos2 = str.IndexOf(")", pos1);
                String PortSubstr2 = str.Substring(pos1, pos2 - pos1);

                // 计算端口号
                Port = Convert.ToInt32(PortSubstr1) * 256;
                Port = Port + Convert.ToInt32(PortSubstr2);

                return Port;
            }

            internal string ComposeExceptionMessage(ResponseDescription resp_desc, string log)
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("FTP Protocol Error.....\r\n");
                sb.Append("Status: " + resp_desc.Status + "\r\n");
                sb.Append("Description: " + resp_desc.StatusDescription + "\r\n");
               
                //sb.Append("\r\n");
                //sb.Append("--------------------------------\r\n");
                //sb.Append(log);
                //sb.Append("\r\n");
               

                return sb.ToString();
            }

            // 发送FTP命令
            private void SendCommand(String szRequestedMethod, String szParameterToPass)
            {
                String szCommand = szRequestedMethod;

                // 形成要执行的命令
                if ((szParameterToPass != null) && (!szParameterToPass.Equals("")))
                    szCommand = szCommand + " " + szParameterToPass;
                szCommand = szCommand + "\r\n";

                // 记录运行日志
                m_sbControlSocketLog.Append(szCommand);

                // 连接检查
                if (m_ControlSocket == null)
                    throw new System.Net.ProtocolViolationException();

                // 向FTP服务器发送命令
                Byte[] sendbuffer = Encoding.ASCII.GetBytes(szCommand.ToCharArray());
                int cbReturn = m_ControlSocket.Send(sendbuffer, sendbuffer.Length, 0);
                if (cbReturn < 0)
                    throw new System.ApplicationException("Error: can not writing to control socket!");

                return;
            }

            //
            // FTP服务器当次响应完成？
            //
            // 控制响应格式
            //     单行响应：nnn xxxxxxxxxxxxxxxxxxxxxxxxxxxx\r\n
            //     多行响应：nnn-xxxxxxxxxxxxxxxxxxxxxxxxxxxx\r\n
            //               nnn xxxxxxxxxxxxxxxxxxxxxxxxxxxx\r\n(同一响应码)
            // FTP响应时有可能在一次接收中获取多个响应码和响应描述？
            //     例如：下载文件时文件很小，服务器[准备下发数据]+[下载完成]响应同时一起到达！
            private bool IsCompleteResponse(Stream responseStream)
            {
                bool bIsComplete = false;
                int responselength = 0;

                // 定位流头
                responselength = (int)responseStream.Length;
                responseStream.Position = 0;

                // 检查处理
                if (responselength >= 5)
                {
                    // 从响应流中读取数据到缓冲区
                    Byte[] ByteArray = new Byte[responselength];
                    memset(ref ByteArray, 0, responselength);

                    responselength = responseStream.Read(ByteArray, 0, responselength);
                    String szResponse = Encoding.ASCII.GetString(ByteArray, 0, responselength);
                    String szHeadStatus = "";

                    // 完成检查一
                    if ((responselength == 5) && (ByteArray[responselength - 1] == '\n'))
                        bIsComplete = true;
                    else if ((ByteArray[responselength - 1] == '\n') && (ByteArray[responselength - 2] == '\r'))
                        bIsComplete = true;

                    // 完成检查二
                    if (bIsComplete)
                    {
                        // 执行多行响应检查(必须以\r\n结束响应)
                        if (szResponse[3] == '-')
                        {
                            // 是否以\r\n结束?
                            if (((ByteArray[responselength - 1] == '\n') && (ByteArray[responselength - 2] == '\r')) == false)
                                bIsComplete = false;
                            else
                            {
                                // 获取首行StatusCode
                                szHeadStatus = szResponse.Substring(0, 3);

                                // 获取最后一行的开始位置(避免最后的\r\n，所以responselength-2)
                                bool bFound = false;
                                int lastlinestart = 0;
                                for (lastlinestart = responselength - 2; lastlinestart > 0; lastlinestart--)
                                {
                                    if (ByteArray[lastlinestart] == '\n' && ByteArray[lastlinestart - 1] == '\r')
                                    {
                                        bFound = true;
                                        break;
                                    }
                                }
                                if (bFound == false)
                                {
                                    // 后续行的响应没有得到！
                                    bIsComplete = false;
                                }
                                else if (szResponse[lastlinestart + 4] != ' ')
                                {
                                    // 多行响应的最后一行必须：nnn xxxxxxxxxx\r\n
                                    // lastlinestart + 4 获取最后一行的"nnn "中的空格
                                    bIsComplete = false;
                                }
                                else
                                {
                                    String szTailStatus = szResponse.Substring(lastlinestart + 1, 3);
                                    if (szHeadStatus != szTailStatus)
                                    {
                                        // 多行响应时，首行与尾行的状态码必须相等！
                                        bIsComplete = false;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                    bIsComplete = false;

                return bIsComplete;
            }

            // 接收FTP命令响应
            private ResponseDescription ReceiveCommandResponse()
            {
                ResponseDescription resp_desc = new ResponseDescription();
                MemoryStream responseStream = null;
                String StatusDescription = null;
                String szResponse = null;
                int StatusCode = 0;
                bool bCompleteResponse = false;

                if (m_ControlSocket == null)
                    throw new System.ApplicationException("Error: Control Connection is not opened!");

                // 接收响应数据
                try
                {
                    int BufferSize = BUFFER_SIZE;
                    int bytesread = 0;
                    Byte[] recvbuffer = new Byte[BufferSize];

                    responseStream = new MemoryStream();
                    while (true)
                    {
                        memset(ref recvbuffer, 0, BufferSize);

                        // 接收完本次控制连接响应数据(在接收超时范围内，可能收到下一命令的响应？)
                        bytesread = m_ControlSocket.Receive(recvbuffer, BufferSize, 0);
                        if (bytesread <= 0)
                            break;
                        responseStream.Write(recvbuffer, 0, bytesread);

                        // 记录操作日志
                        szResponse = Encoding.ASCII.GetString(recvbuffer, 0, bytesread);
                        m_sbControlSocketLog.Append(szResponse);

                        // 控制响应接收完毕？
                        bCompleteResponse = IsCompleteResponse(responseStream);
                        if (bCompleteResponse)
                            break;
                    }
                }
                catch (Exception e)
                {
                    SafeRelease(ref responseStream);
                    throw e;
                }

                // 获取控制响应的响应码、响应描述
                try
                {
                    if (bCompleteResponse)
                    {
                        // 获取全部的控制响应结果
                        int responsesize = (int)responseStream.Length;
                        responseStream.Position = 0;
                        Byte[] bStatusDescription = new Byte[responsesize];
                        memset(ref bStatusDescription, 0, responsesize);

                        responsesize = responseStream.Read(bStatusDescription, 0, responsesize);
                        StatusDescription = Encoding.ASCII.GetString(bStatusDescription, 0, responsesize);

                        // 获取响应码
                        String szStatusCode = StatusDescription.Substring(0, 3);
                        StatusCode = Convert.ToInt16(szStatusCode);

                        // 获取响应描述
                        StatusDescription = StatusDescription.Substring(4, StatusDescription.Length - 4);
                    }
                    else
                        throw new System.Net.ProtocolViolationException();
                }
                catch (Exception e)
                {
                    SafeRelease(ref responseStream);
                    throw e;
                }

                // 关闭控制响应流
                SafeRelease(ref responseStream);

                resp_desc.StatusDescription = StatusDescription;
                resp_desc.Status = StatusCode;
                return resp_desc;
            }

            //***************************************************************************************
            // FtpWebRequest 重载操作
            //***************************************************************************************
            // 获取请求数据流,仅用于上传数据！
            public override Stream GetRequestStream()
            {
                if (m_CommandType != FtpCommandType.FtpDataSendCommand)
                    throw new System.InvalidOperationException("Error: Can not upload data with this method type!");
                if (m_RequestStream == null)
                    m_RequestStream = new FtpStream(new MemoryStream(), false, true, false);
                else
                    throw new System.InvalidOperationException("Error: request stream already retrieved!");
                return m_RequestStream;
            }

            // 获取响应对象
            public override WebResponse GetResponse()
            {
                String user = "anonymous";
                String pass = "User@";

                // 通过ftp代理登录
                if (m_Proxy != null)
                {
                    // 获取代理Uri
                    m_ProxyUri = GetProxyUri();
                    // 存在代理！
                    if (m_ProxyUri != null)
                    {
                        // 存在验证
                        if (m_Proxy.Credentials != null)
                        {
                            // 从代理获取身份
                            NetworkCredential cred = m_Proxy.Credentials.GetCredential(m_ProxyUri, null);
                            user = cred.UserName;
                            pass = cred.Password;
                            // 不存在，则缺省为匿名
                            if ((user == null) || (user == ""))
                                user = "anonymous";
                            if ((pass == null) || (pass == ""))
                                pass = "User@";
                        }
                        else
                        {
                            // 请求指定验证
                            if (m_Credentials != null)
                            {
                                // 从指定验证中获取身份
                                NetworkCredential cred = m_Credentials.GetCredential(m_RequestUri, null);
                                if (cred != null)
                                {
                                    user = cred.UserName;
                                    pass = cred.Password;
                                }
                                // 不存在，则缺省为匿名
                                if ((user == null) || (user == ""))
                                    user = "anonymous";
                                if ((pass == null) || (pass == ""))
                                    pass = "User@";
                            }
                            user = user + "@" + m_RequestUri.Host.ToString();
                        }
                    }
                    // 获取主机和端口信息
                    Uri uriProxy = m_Proxy.GetProxy(m_RequestUri);
                    m_ServicePoint.Host = uriProxy.Host;
                    m_ServicePoint.Port = uriProxy.Port;
                }
                else
                {
                    // 直接访问，不通过代理
                    m_ServicePoint.Host = m_RequestUri.Host;
                    m_ServicePoint.Port = m_RequestUri.Port;
                    // 指定验证
                    if (m_Credentials != null)
                    {
                        NetworkCredential cred = m_Credentials.GetCredential(m_RequestUri, null);
                        user = cred.UserName;
                        pass = cred.Password;
                        // 不存在，则缺省为匿名
                        if ((user == null) || (user == ""))
                            user = "anonymous";
                        if ((pass == null) || (pass == ""))
                            pass = "User@";
                    }
                    //其他为：匿名访问！
                }

                // 登录检查？
                if (!doLogin(user, pass))
                    throw new System.ApplicationException("Error: Login Failed\r\nServer Log:\r\n" + m_sbControlSocketLog.ToString());

                // 获取ftp命令执行的响应信息
                return (WebResponse)GetFtpResponse();
            }

            // 获取ftp命令执行的响应信息
            private WebResponse GetFtpResponse()
            {
                FtpWebResponse ftpresponse = null;
                ResponseDescription resp_desc = null;
                Socket objNewDataConnection = null;
                Socket DataConnection = null;

                // 根据命令类型执行PASV或PORT命令
                switch (m_CommandType)
                {
                    case FtpCommandType.FtpDataReceiveCommand:
                    case FtpCommandType.FtpDataSendCommand:
                        if (m_bPassiveMode)
                            OpenPassiveDataConnection();
                        else
                            OpenDataConnection();
                        break;
                    default:
                        break;
                }

                // 设置传输方式
                // A - ascii  (文本)
                // I - binary (二进制 - 缺省)
                string sztype = "I";
                if (m_szContentType != null)
                    if (m_szContentType.ToLower() == "ascii")
                        sztype = "A";
                SendCommand("TYPE", sztype);
                resp_desc = ReceiveCommandResponse();
                if (!resp_desc.PositiveCompletion)
                    throw new System.ApplicationException("Error: Data negotiation failed.\r\n" + m_sbControlSocketLog.ToString());

                // 执行请求的FTP操作
                switch (m_szServerMethod)
                {
                    case "PWD":
                        // 打印工作目录
                        m_szCmdParameter = null;
                        SendCommand(m_szServerMethod, m_szCmdParameter);
                        resp_desc = ReceiveCommandResponse();
                        break;
                    case "REN":
                        // 更改文件名
                        // 1. 转到指定目录
                        SendCommand("CWD", m_szCmdParameter);
                        resp_desc = ReceiveCommandResponse();
                        if (resp_desc.Status != 550)
                        {
                            // 2. 执行RNFR命令
                            SendCommand("RNFR", m_szFromFile);
                            resp_desc = ReceiveCommandResponse();
                            if (resp_desc.Status != 550)
                            {
                                // 3. 执行RNTO命令
                                SendCommand("RNTO", m_szToFile);
                                resp_desc = ReceiveCommandResponse();
                            }
                        }
                        break;
                    default:
                        // 其他FTP操作
                        SendCommand(m_szServerMethod, m_szCmdParameter);
                        resp_desc = ReceiveCommandResponse();
                        break;
                }

                // 执行操作后，对响应进行分析
                try
                {
                    if (m_CommandType == FtpCommandType.FtpDataSendCommand)
                    {
                        // 上传数据
                        if (resp_desc.PositivePreliminary)
                        {
                            // 服务器准备接收上传数据
                            if (m_RequestStream != null)
                            {
                                // 获取数据连接
                                if (m_bPassiveMode)
                                {
                                    DataConnection = m_DataSocket;
                                    objNewDataConnection = null;
                                }
                                else
                                {
                                    DataConnection = m_DataSocket.Accept();
                                    objNewDataConnection = DataConnection;
                                }
                                if (DataConnection == null)
                                    throw new System.Net.ProtocolViolationException("Error: can not build data connectios!");

                                // 上载处理
                                try
                                {
                                    // 发送数据
                                    SendData(DataConnection);

                                    // 关闭数据连接，以获取服务器的完毕响应
                                    SafeRelease(ref DataConnection);

                                    //[完毕响应]与[开始上载]响应同时获得！
                                    if (resp_desc.StatusDescription.IndexOf("\r\n226", 0) != -1)
                                    {
                                        //文件传输完毕
                                        resp_desc.Status = 226;
                                        ftpresponse = new FtpWebResponse(resp_desc.Status, resp_desc.StatusDescription, m_sbControlSocketLog.ToString());
                                    }
                                    else
                                    {
                                        // 等待[文件传输完毕]响应
                                        ResponseDescription resp = ReceiveCommandResponse();
                                        ftpresponse = new FtpWebResponse(resp.Status, resp.StatusDescription, m_sbControlSocketLog.ToString());
                                    }
                                }
                                catch (Exception e)
                                {
                                    throw e;
                                }
                            }
                            else
                                throw new System.ApplicationException("Error: Data to be uploaded not specified!");
                        }
                        else
                            throw new System.ApplicationException(ComposeExceptionMessage(resp_desc, m_sbControlSocketLog.ToString()));
                    }
                    else if (m_CommandType == FtpCommandType.FtpDataReceiveCommand)
                    {
                        // 下载数据
                        if (resp_desc.PositivePreliminary)
                        {
                            // 获取数据连接
                            if (m_bPassiveMode)
                            {
                                DataConnection = m_DataSocket;
                                objNewDataConnection = null;
                            }
                            else
                            {
                                DataConnection = m_DataSocket.Accept();
                                objNewDataConnection = DataConnection;
                            }
                            if (DataConnection == null)
                                throw new System.Net.ProtocolViolationException("Error: can not build data connectios!");

                            // 下载处理
                            Stream datastream = null;
                            try
                            {
                                // 获取命令响应信息
                                if (resp_desc.StatusDescription.IndexOf("\r\n550", 0) != -1)
                                {
                                    // 源文件不存在！
                                    resp_desc.Status = 550;
                                    ftpresponse = new FtpWebResponse(resp_desc.Status, resp_desc.StatusDescription, m_sbControlSocketLog.ToString());
                                }
                                else if (resp_desc.StatusDescription.IndexOf("\r\n226", 0) != -1)
                                {
                                    // 小文件情况 - 数据下载完毕！
                                    switch (m_szServerMethod)
                                    {
                                        case "LIST":
                                            // 不创建流
                                            resp_desc.Status = 226;
                                            ftpresponse = new FtpWebResponse(resp_desc.Status, resp_desc.StatusDescription, m_sbControlSocketLog.ToString());
                                            break;
                                        default:
                                            datastream = ReceiveData(DataConnection);
                                            resp_desc.Status = 226;
                                            ftpresponse = new FtpWebResponse(resp_desc.Status, resp_desc.StatusDescription, m_sbControlSocketLog.ToString());
                                            ftpresponse.SetDownloadStream(datastream);
                                            break;
                                    }
                                }
                                else
                                {
                                   
                                    // 大文件情况 - 需要最后获取[数据下载完毕]的响应
                                    datastream = ReceiveData(DataConnection);
                                    ResponseDescription resp = ReceiveCommandResponse();
                                    ftpresponse = new FtpWebResponse(resp.Status, resp.StatusDescription, m_sbControlSocketLog.ToString());
                                    ftpresponse.SetDownloadStream(datastream);
                                   
                                }
                            }
                            catch (Exception e)
                            {
                                SafeRelease(ref datastream);
                                throw e;
                            }
                        }
                        else
                            throw new ApplicationException(ComposeExceptionMessage(resp_desc, m_sbControlSocketLog.ToString()));
                    }
                    else
                        ftpresponse = new FtpWebResponse(resp_desc.Status, resp_desc.StatusDescription, m_sbControlSocketLog.ToString());
                }
                catch (Exception e)
                {
                    SafeRelease(ref objNewDataConnection);
                    CloseDataConnection();
                    throw e;
                }

                SafeRelease(ref objNewDataConnection);
                CloseDataConnection();

                return ftpresponse;
            }

            private bool doLogin(String UserID, String Password)
            {
                ResponseDescription resp = null;

                // 连接服务器
                OpenControlConnection(m_ServicePoint.Uri);

                // 传送用户
                SendCommand("USER", UserID);
                resp = ReceiveCommandResponse();
                // 通知需要密码
                if (resp.Status == 331)
                {
                    SendCommand("PASS", Password);
                    resp = ReceiveCommandResponse();
                    // 密码正确？
                    if (resp.Status == 230)
                        return true;
                }

                return false;
            }

            // 获取代理的Uri
            private Uri GetProxyUri()
            {
                Uri uriProxy = null;
                if (m_Proxy != null && (!m_Proxy.IsBypassed(m_RequestUri)))
                    uriProxy = m_Proxy.GetProxy(m_RequestUri);
                return uriProxy;
            }

            // 打开数据连接Socket (非Passive模式, 只建立侦听数据连接，具体数据传输时需通过Accept建立连接)
            private void OpenDataConnection()
            {
                ResponseDescription resp_desc = null;
                Socket tempSocket = null;

                if (m_DataSocket != null)
                    throw new System.ApplicationException("Error: Data socket is already open!");

                // 打开连接处理
                try
                {
                    // 建立socket
                    tempSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    tempSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, m_ConnectTimeout);
                    tempSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, m_DataReceiveTimeout);

                    // 建立连接监听器
                    IPHostEntry localHostEntry = Dns.GetHostEntry(Dns.GetHostName());
                    //IPHostEntry localHostEntry = Dns.GetHostByName(Dns.GetHostName());
                    IPEndPoint epListener = new IPEndPoint(localHostEntry.AddressList[0], 0);
                    tempSocket.Bind(epListener);

                    // 设置挂起连接队列的最大长度(允许<=2个线程)
                    tempSocket.Listen(2);

                    // 设置连接端口
                    IPEndPoint localEP = (IPEndPoint)tempSocket.LocalEndPoint;
                    String szLocal = FormatAddress(localEP.Address, localEP.Port);
                    SendCommand("PORT", szLocal);
                    resp_desc = ReceiveCommandResponse();
                    if (!resp_desc.PositiveCompletion)
                        throw new System.ApplicationException("Error: can not open data connection!\r\n" + ComposeExceptionMessage(resp_desc, m_sbControlSocketLog.ToString()));
                }
                catch (Exception e)
                {
                    SafeRelease(ref tempSocket);
                    throw e;
                }

                m_DataSocket = tempSocket;
                return;
            }

            // 打开控制连接Socket (Passive模式)
            private void OpenPassiveDataConnection()
            {
                ResponseDescription resp_desc = null;
                Socket tempSocket = null;

                if (m_DataSocket != null)
                    throw new ProtocolViolationException();

                // 打开连接处理
                try
                {
                    // 执行PASV命令
                    String IPAddressStr = null;
                    int Port = 0;
                    SendCommand("PASV", "");
                    resp_desc = ReceiveCommandResponse();
                    if (!resp_desc.PositiveCompletion)
                        throw new System.ApplicationException("Error: can not open passive data connection!\r\n" + ComposeExceptionMessage(resp_desc, m_sbControlSocketLog.ToString()));

                    // 从响应信息中获取IPAddress、port address
                    IPAddressStr = getIPAddress(resp_desc.StatusDescription);
                    Port = getPort(resp_desc.StatusDescription);

                    // 创建socket
                    tempSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    tempSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, m_ConnectTimeout);
                    tempSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, m_DataReceiveTimeout);

                    // 建立连接
                    IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(IPAddressStr), Port);
                    tempSocket.Connect(serverEndPoint);
                }
                catch (Exception e)
                {
                    SafeRelease(ref tempSocket);
                    throw e;
                }

                m_DataSocket = tempSocket;
                return;
            }

            // 打开控制Socket
            private void OpenControlConnection(Uri uriToConnect)
            {
                MemoryStream responseStream = null;
                Socket tempSocket = null;
                String Host = uriToConnect.Host;
                int Port = uriToConnect.Port;

                // Socket已在使用
                if (m_ControlSocket != null)
                    throw new System.Net.ProtocolViolationException("Error: Control connection already in use");

                // 打开连接处理
                try
                {
                    EndPoint clientIPEndPoint = null;
                    EndPoint clientEndPoint = null;
                    IPHostEntry serverHostEntry = null;
                    IPEndPoint serverEndPoint = null;
                    Byte[] recvbuffer = null;
                    int BufferSize = BUFFER_SIZE;
                    int bytesread = 0;

                    // 建立socket
                    tempSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    tempSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, m_ConnectTimeout);
                    tempSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, m_DataReceiveTimeout);

                    // 建立绑定
                    clientIPEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    clientEndPoint = clientIPEndPoint;
                    tempSocket.Bind(clientEndPoint);

                    // 建立连接
                    clientEndPoint = tempSocket.LocalEndPoint;
                    serverHostEntry = Dns.GetHostEntry(Host);
                    //serverHostEntry = Dns.GetHostByName(Host);
                    serverEndPoint = new IPEndPoint(serverHostEntry.AddressList[0], Port);
                    tempSocket.Connect(serverEndPoint);

                    // 获取连接后的初始响应
                    responseStream = new MemoryStream();
                    recvbuffer = new Byte[BufferSize];
                    while (true)
                    {
                        memset(ref recvbuffer, 0, BufferSize);

                        bytesread = tempSocket.Receive(recvbuffer, BufferSize, 0);
                        if (bytesread <= 0)
                            break;
                        responseStream.Write(recvbuffer, 0, bytesread);

                        // 不记初始响应日志！

                        // 控制响应接收完毕？
                        if (IsCompleteResponse(responseStream))
                            break;
                    }
                }
                catch (Exception e)
                {
                    SafeRelease(ref responseStream);
                    SafeRelease(ref tempSocket);
                    throw e;
                }

                SafeRelease(ref responseStream);

                m_ControlSocket = tempSocket;
                return;
            }

            // 接收数据
            private Stream ReceiveData(Socket Accept)
            {
                MemoryStream responseStream = null;

                if (Accept == null)
                    throw new ArgumentNullException();

                try
                {
                    int BufferSize = BUFFER_SIZE;
                    int bytesread = 0;
                    Byte[] recvbuffer = new Byte[BufferSize];

                    responseStream = new MemoryStream();
                    while (true)
                    {
                        memset(ref recvbuffer, 0, BufferSize);

                        bytesread = Accept.Receive(recvbuffer, BufferSize, 0);
                        if (bytesread <= 0)
                            break;
                        responseStream.Write(recvbuffer, 0, bytesread);
                    }
                }
                catch (Exception e)
                {
                    SafeRelease(ref responseStream);
                    throw e;
                }

                return responseStream;
            }

            // 发送数据
            private int SendData(Socket Accept)
            {
                if (Accept == null)
                    throw new ArgumentNullException();

                // 获取数据流
                if (m_RequestStream == null)
                    throw new ArgumentNullException();

                // 流转换
                FtpStream tempStream = (FtpStream)m_RequestStream;
                if (tempStream == null)
                    throw new ApplicationException("Error: can not convert to FtpStream!");

                // 发送
                int BufferSize = (int)tempStream.InternalLength;
                int BytesSend = 0;
                int BytesRead = 0;
                int cbReturn = 0;
                Byte[] sendbuffer = new Byte[BufferSize];
                memset(ref sendbuffer, 0, BufferSize);

                tempStream.InternalPosition = 0;
                BytesRead = tempStream.InternalRead(sendbuffer, 0, BufferSize);
                if (BytesRead != BufferSize)
                    throw new ApplicationException("Error: data read error when uploading data!");
                BytesSend = Accept.Send(sendbuffer, BytesRead, 0);
                if (BytesSend != BytesRead)
                    throw new ApplicationException("Error: send and read bytes number mismatch in sending data!");
                cbReturn += BytesSend;

                // 完成后自动关闭上传数据流
                SafeRelease(ref tempStream);

                return cbReturn;
            }
        }
    }
}