Imports System
Imports System.Net

Namespace Xydc.Platform.Common.Utilities

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.Common.Utilities
    ' ����    ��BaseFTP
    '
    ' ����������
    '     ����ftp
    '----------------------------------------------------------------

    Public Class BaseFTP
        Implements IDisposable

        Public Const BufferSize As Integer = 1024








        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
        End Sub

        '----------------------------------------------------------------
        ' ������������
        '----------------------------------------------------------------
        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(True)
        End Sub

        '----------------------------------------------------------------
        ' ������������
        '----------------------------------------------------------------
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If (Not disposing) Then
                Exit Sub
            End If
        End Sub

        '----------------------------------------------------------------
        ' ��ȫ�ͷű�����Դ
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.Common.Utilities.BaseFTP)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub








        '----------------------------------------------------------------
        ' ��WebӦ�÷����������ļ���FTP������
        ' FTP�������е�Ŀ¼���ļ��Զ������븲�ǣ�����
        '     strErrMsg        �����ش�����Ϣ
        '     strLocalFile     ��Ҫ�ϴ����ļ�(ȫ·��)
        '     strUrl           ���ϴ���FTP��������Url·�����ļ���
        '     strUser          ��FTP��������֤�û�
        '     strPassword      ��FTP��������֤�û�����
        '     strProxyUrl      ��FTP�����Url
        '     strProxyUser     ��FTP������֤�û�
        '     strProxyPassword ��FTP������֤�û�����
        ' ����
        '     True             ���ɹ�
        '     False            ��ʧ��
        '----------------------------------------------------------------
        Public Function doPutFile( _
            ByRef strErrMsg As String, _
            ByVal strLocalFile As String, _
            ByVal strUrl As String, _
            ByVal strUser As String, _
            ByVal strPassword As String, _
            ByVal strProxyUrl As String, _
            ByVal strProxyUser As String, _
            ByVal strProxyPassword As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objFtpWebResponse As Xydc.Net.FtpWebResponse
            Dim objFtpWebRequest As Xydc.Net.FtpWebRequest
            Dim objWebResponse As System.Net.WebResponse
            Dim objWebRequest As System.Net.WebRequest
            Dim objSrcFileStream As System.IO.FileStream
            Dim objRequestStream As System.IO.Stream


            Dim blnDelete As Boolean = False
            Dim strCacheFile As String = ""


            doPutFile = False
            strErrMsg = ""

            Try
                '����
                If strLocalFile Is Nothing Then strLocalFile = ""
                If strUrl Is Nothing Then strUrl = ""
                If strUser Is Nothing Then strUser = ""
                If strPassword Is Nothing Then strPassword = ""
                If strProxyUrl Is Nothing Then strProxyUrl = ""
                If strProxyUser Is Nothing Then strProxyUser = ""
                If strProxyPassword Is Nothing Then strProxyPassword = ""
                strLocalFile = strLocalFile.Trim()
                strUrl = strUrl.Trim()
                strProxyUser = strProxyUser.Trim()
                strPassword = strPassword.Trim()
                strProxyUrl = strProxyUrl.Trim()
                strProxyUser = strProxyUser.Trim()
                strProxyPassword = strProxyPassword.Trim()
                If strLocalFile = "" Or strUrl = "" Or Not (strUser <> "" Or (strProxyUrl <> "" And strProxyUser <> "")) Then
                    strErrMsg = "����[doPutFile]û�д������������"
                    GoTo errProc
                End If

                '����ļ��Ƿ����?
                Dim blnExisted As Boolean
                If objBaseLocalFile.doFileExisted(strErrMsg, strLocalFile, blnExisted) = False Then
                    GoTo errProc
                End If
                If blnExisted = False Then
                    strErrMsg = "����[" + strLocalFile + "]�����ڣ�"
                    GoTo errProc
                End If


                '�����ϴ��ļ����ñ����ļ����м����ϴ���
                Dim strTempPath As String = objBaseLocalFile.getPathName(strLocalFile)
                Dim strTempFile As String = ""
                If objBaseLocalFile.doCopyToTempFile(strErrMsg, strLocalFile, strTempPath, strTempFile) = False Then
                    GoTo errProc
                End If
                blnDelete = True
                Dim strOrgFile As String = strLocalFile
                strLocalFile = objBaseLocalFile.doMakePath(strTempPath, strTempFile)
                strCacheFile = strLocalFile


                '�����ļ�
                If objPulicParameters.doEncryptFile(strErrMsg, strLocalFile) = False Then
                    GoTo errProc
                End If

                '�𼶴���FTPĿ¼
                If Me.doCreateDirectory(strErrMsg, strUrl, strUser, strPassword, strProxyUrl, strProxyUser, strProxyPassword) = False Then
                    GoTo errProc
                End If

                'ע����Э����
                Dim objCreator As Xydc.Net.FtpRequestCreator = New Xydc.Net.FtpRequestCreator
                System.Net.WebRequest.RegisterPrefix("ftp:", objCreator)

                '����FtpWebRequest
                objWebRequest = System.Net.WebRequest.Create(strUrl)
                objWebRequest.Method = "put"    '���ز���
                Try
                    objFtpWebRequest = CType(objWebRequest, Xydc.Net.FtpWebRequest)
                Catch
                    objFtpWebRequest = Nothing
                End Try

                objFtpWebRequest.Passive = Xydc.Platform.Common.jsoaConfiguration.FTPPassiveMode     'Passive
                objFtpWebRequest.ContentType = "binary"       'ascii

                'ͨ��FTP�������FTP������
                If (strProxyUrl <> "") Then
                    Dim objWebProxy As System.Net.WebProxy = New System.Net.WebProxy(strProxyUrl)
                    If (strProxyUser <> "") Then
                        objWebProxy.Credentials = New System.Net.NetworkCredential(strProxyUser, strProxyPassword)
                    End If
                    objFtpWebRequest.Proxy = objWebProxy
                End If

                'ֱ�ӷ���
                If (strUser <> "") Then
                    objFtpWebRequest.Credentials = New System.Net.NetworkCredential(strUser, strPassword)
                End If

                '׼��FTP������Ҫ������(���ϴ��ļ��������뵽�ڴ���)
                '�򿪱����ļ�
                objSrcFileStream = New System.IO.FileStream(strLocalFile, System.IO.FileMode.Open, System.IO.FileAccess.Read)

                '��FTP������
                objRequestStream = objFtpWebRequest.GetRequestStream()

                'д�뵽FTP������
                Dim intLength As Integer = Me.BufferSize
                Dim buffer() As Byte = New Byte(intLength) {}
                Dim intBytesRead As Integer = objSrcFileStream.Read(buffer, 0, intLength)
                While intBytesRead > 0
                    objRequestStream.Write(buffer, 0, intBytesRead)
                    intBytesRead = objSrcFileStream.Read(buffer, 0, intLength)
                End While

                'ִ��FTPָ����������ȡ����������Ӧ����
                objWebResponse = objFtpWebRequest.GetResponse()
                objFtpWebResponse = CType(objWebResponse, Xydc.Net.FtpWebResponse)
                If objFtpWebResponse.Status = -1 Then
                    strErrMsg = objFtpWebResponse.StatusDescription
                    GoTo errProc
                End If


                '�ͷ��ļ���Դ
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFtpWebResponse)
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSrcFileStream)
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objRequestStream)
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFtpWebRequest)


                'ɾ�������ļ�

                If blnDelete = True And strCacheFile.Trim <> "" Then
                    If objBaseLocalFile.doDeleteFile(strErrMsg, strCacheFile) = False Then
                        '����
                    End If
                End If



                'ǿ�Ƶȴ�
                If Xydc.Platform.Common.jsoaConfiguration.FTPPutFileWaitTime >= 0 Then
                    System.Threading.Thread.Sleep(Xydc.Platform.Common.jsoaConfiguration.FTPPutFileWaitTime)
                End If

            Catch ex As Exception

                'strErrMsg = ex.Message
                strErrMsg = "FTP����!����ϵ����Ա!"

                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFtpWebResponse)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSrcFileStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objRequestStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFtpWebRequest)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)

            doPutFile = True
            Exit Function
errProc:
            'ɾ�������ļ�
            Dim strErrMsgA As String = ""
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFtpWebResponse)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSrcFileStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objRequestStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFtpWebRequest)

            If blnDelete = True And strCacheFile.Trim <> "" Then
                If objBaseLocalFile.doDeleteFile(strErrMsgA, strCacheFile) = False Then
                    '����
                End If
            End If

            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��FTP�����������ļ���WebӦ�÷�����
        ' ����Ŀ¼���ļ����Զ������븲�ǣ�����
        '     strErrMsg        �����ش�����Ϣ
        '     strLocalFile     ��Ҫ������ļ�(ȫ·��)
        '     strUrl           ��Ҫ���ص�FTP��������Url·�����ļ���
        '     strUser          ��FTP��������֤�û�
        '     strPassword      ��FTP��������֤�û�����
        '     strProxyUrl      ��FTP�����Url
        '     strProxyUser     ��FTP������֤�û�
        '     strProxyPassword ��FTP������֤�û�����
        ' ����
        '     True             ���ɹ�
        '     False            ��ʧ��
        '----------------------------------------------------------------
        Public Function doGetFile( _
            ByRef strErrMsg As String, _
            ByVal strLocalFile As String, _
            ByVal strUrl As String, _
            ByVal strUser As String, _
            ByVal strPassword As String, _
            ByVal strProxyUrl As String, _
            ByVal strProxyUser As String, _
            ByVal strProxyPassword As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objFtpWebResponse As Xydc.Net.FtpWebResponse
            Dim objFtpWebRequest As Xydc.Net.FtpWebRequest
            Dim objWebResponse As System.Net.WebResponse
            Dim objWebRequest As System.Net.WebRequest
            Dim objSrcFileStream As System.IO.FileStream
            Dim objDownloadStream As System.IO.Stream

            doGetFile = False
            strErrMsg = ""

            Try
                '����
                If strLocalFile Is Nothing Then strLocalFile = ""
                If strUrl Is Nothing Then strUrl = ""
                If strUser Is Nothing Then strUser = ""
                If strPassword Is Nothing Then strPassword = ""
                If strProxyUrl Is Nothing Then strProxyUrl = ""
                If strProxyUser Is Nothing Then strProxyUser = ""
                If strProxyPassword Is Nothing Then strProxyPassword = ""
                strLocalFile = strLocalFile.Trim()
                strUrl = strUrl.Trim()
                strProxyUser = strProxyUser.Trim()
                strPassword = strPassword.Trim()
                strProxyUrl = strProxyUrl.Trim()
                strProxyUser = strProxyUser.Trim()
                strProxyPassword = strProxyPassword.Trim()
                If strLocalFile = "" Or strUrl = "" Or Not (strUser <> "" Or (strProxyUrl <> "" And strProxyUser <> "")) Then
                    strErrMsg = "����[doGetFile]û�д������������"
                    GoTo errProc
                End If

                '�Զ���������Ŀ¼
                If objBaseLocalFile.doCreateDirectory(strErrMsg, strLocalFile) = False Then
                    GoTo errProc
                End If

                'ע����Э����
                Dim objCreator As Xydc.Net.FtpRequestCreator = New Xydc.Net.FtpRequestCreator
                System.Net.WebRequest.RegisterPrefix("ftp:", objCreator)

                '����FtpWebRequest
                objWebRequest = System.Net.WebRequest.Create(strUrl)
                objWebRequest.Method = "get" '���ز���
                Try
                    objFtpWebRequest = CType(objWebRequest, Xydc.Net.FtpWebRequest)
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                objFtpWebRequest.Passive = Xydc.Platform.Common.jsoaConfiguration.FTPPassiveMode  'Passive
                objFtpWebRequest.ContentType = "binary"                                        'ascii

                'ͨ��FTP�������FTP������
                If (strProxyUrl <> "") Then
                    Dim objWebProxy As System.Net.WebProxy = New System.Net.WebProxy(strProxyUrl)
                    If (strProxyUser <> "") Then
                        objWebProxy.Credentials = New System.Net.NetworkCredential(strProxyUser, strProxyPassword)
                    End If
                    objFtpWebRequest.Proxy = objWebProxy
                End If

                'ֱ�ӷ���
                If (strUser <> "") Then
                    objFtpWebRequest.Credentials = New System.Net.NetworkCredential(strUser, strPassword)
                End If

                'ִ��FTPָ����������ȡ����������Ӧ����
                objWebResponse = objFtpWebRequest.GetResponse()
                objFtpWebResponse = CType(objWebResponse, Xydc.Net.FtpWebResponse)
                If objFtpWebResponse.Status = -1 Then
                    strErrMsg = objFtpWebResponse.StatusDescription
                    GoTo errProc
                End If
                objDownloadStream = objFtpWebResponse.GetResponseStream()
                If objDownloadStream Is Nothing Then
                    strErrMsg = "����δ�ܴ�[" + strUrl + "]��ȡ�����ݣ�"
                    GoTo errProc
                End If

                '�򿪱����ļ�
                objSrcFileStream = New System.IO.FileStream(strLocalFile, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write)

                'д�뵽�ļ���
                Dim intLength As Integer = Me.BufferSize
                Dim buffer() As Byte = New Byte(intLength) {}
                Dim intBytesRead As Integer = objDownloadStream.Read(buffer, 0, intLength)
                While intBytesRead > 0
                    objSrcFileStream.Write(buffer, 0, intBytesRead)
                    intBytesRead = objDownloadStream.Read(buffer, 0, intLength)
                End While


                '�ͷ��ļ���Դ
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFtpWebResponse)
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDownloadStream)
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSrcFileStream)
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFtpWebRequest)


                '�����ļ�
                If objPulicParameters.doDecryptFile(strErrMsg, strLocalFile) = False Then
                    GoTo errProc
                End If


                ''�����ļ�
                'Dim blnTestOK As Boolean = False
                'If objBaseLocalFile.doTestFile(strErrMsg, strLocalFile, blnTestOK) = False Then
                '    GoTo errProc
                'End If
                'If blnTestOK = False Then
                '    strErrMsg = "����[" + strUrl + "]δ�����سɹ���"
                '    GoTo errProc
                'End If
                'ǿ�Ƶȴ�
                If Xydc.Platform.Common.jsoaConfiguration.FTPGetFileWaitTime >= 0 Then
                    System.Threading.Thread.Sleep(Xydc.Platform.Common.jsoaConfiguration.FTPGetFileWaitTime)
                End If

            Catch ex As Exception

                'strErrMsg = ex.Message
                strErrMsg = "FTP����!����ϵ����Ա!"

                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFtpWebResponse)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDownloadStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSrcFileStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFtpWebRequest)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)

            doGetFile = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFtpWebResponse)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDownloadStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSrcFileStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFtpWebRequest)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��FTP������ɾ��ָ�����ļ�
        '     strErrMsg        �����ش�����Ϣ
        '     strUrl           ��Ҫɾ����FTP��������Url·�����ļ���
        '     strUser          ��FTP��������֤�û�
        '     strPassword      ��FTP��������֤�û�����
        '     strProxyUrl      ��FTP�����Url
        '     strProxyUser     ��FTP������֤�û�
        '     strProxyPassword ��FTP������֤�û�����
        ' ����
        '     True             ���ɹ�
        '     False            ��ʧ��
        '----------------------------------------------------------------
        Public Function doDeleteFile( _
            ByRef strErrMsg As String, _
            ByVal strUrl As String, _
            ByVal strUser As String, _
            ByVal strPassword As String, _
            ByVal strProxyUrl As String, _
            ByVal strProxyUser As String, _
            ByVal strProxyPassword As String) As Boolean

            Dim objFtpWebResponse As Xydc.Net.FtpWebResponse
            Dim objFtpWebRequest As Xydc.Net.FtpWebRequest
            Dim objWebResponse As System.Net.WebResponse
            Dim objWebRequest As System.Net.WebRequest

            doDeleteFile = False
            strErrMsg = ""

            Try
                '����
                If strUrl Is Nothing Then strUrl = ""
                If strUser Is Nothing Then strUser = ""
                If strPassword Is Nothing Then strPassword = ""
                If strProxyUrl Is Nothing Then strProxyUrl = ""
                If strProxyUser Is Nothing Then strProxyUser = ""
                If strProxyPassword Is Nothing Then strProxyPassword = ""
                strUrl = strUrl.Trim()
                strProxyUser = strProxyUser.Trim()
                strPassword = strPassword.Trim()
                strProxyUrl = strProxyUrl.Trim()
                strProxyUser = strProxyUser.Trim()
                strProxyPassword = strProxyPassword.Trim()
                If strUrl = "" Or Not (strUser <> "" Or (strProxyUrl <> "" And strProxyUser <> "")) Then
                    strErrMsg = "����[doDeleteFile]û�д������������"
                    GoTo errProc
                End If

                'ע����Э����
                Dim objCreator As Xydc.Net.FtpRequestCreator = New Xydc.Net.FtpRequestCreator
                System.Net.WebRequest.RegisterPrefix("ftp:", objCreator)

                '����FtpWebRequest
                objWebRequest = System.Net.WebRequest.Create(strUrl)
                objWebRequest.Method = "delete" 'ɾ������
                Try
                    objFtpWebRequest = CType(objWebRequest, Xydc.Net.FtpWebRequest)
                Catch
                    objFtpWebRequest = Nothing
                End Try
                objFtpWebRequest.Passive = Xydc.Platform.Common.jsoaConfiguration.FTPPassiveMode  'Passive
                objFtpWebRequest.ContentType = "binary"                                        'ascii

                'ͨ��FTP�������FTP������
                If (strProxyUrl <> "") Then
                    Dim objWebProxy As System.Net.WebProxy = New System.Net.WebProxy(strProxyUrl)
                    If (strProxyUser <> "") Then
                        objWebProxy.Credentials = New System.Net.NetworkCredential(strProxyUser, strProxyPassword)
                    End If
                    objFtpWebRequest.Proxy = objWebProxy
                End If

                'ֱ�ӷ���
                If (strUser <> "") Then
                    objFtpWebRequest.Credentials = New System.Net.NetworkCredential(strUser, strPassword)
                End If

                'ִ��FTPָ����������ȡ����������Ӧ����
                objWebResponse = objFtpWebRequest.GetResponse()
                objFtpWebResponse = CType(objWebResponse, Xydc.Net.FtpWebResponse)
                If objFtpWebResponse.Status = -1 Then
                    strErrMsg = objFtpWebResponse.StatusDescription
                    GoTo errProc
                End If
                'objFtpWebResponse.Status=550 ��ʾ�ļ�������
                System.Threading.Thread.Sleep(15)
            Catch ex As Exception

                'strErrMsg = ex.Message
                strErrMsg = "FTP����!����ϵ����Ա!"

                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFtpWebResponse)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFtpWebRequest)

            doDeleteFile = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFtpWebResponse)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFtpWebRequest)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��FTP��������Ŀ¼��λ��strUrlָ����λ��
        '     strErrMsg        �����ش�����Ϣ
        '     strUrl           ��FTP��������Url·�����ļ���
        '     strUser          ��FTP��������֤�û�
        '     strPassword      ��FTP��������֤�û�����
        '     strProxyUrl      ��FTP�����Url
        '     strProxyUser     ��FTP������֤�û�
        '     strProxyPassword ��FTP������֤�û�����
        '     blnExisted       ��Ŀ¼����=True
        ' ����
        '     True             ���ɹ�
        '     False            ��ʧ��
        '----------------------------------------------------------------
        Public Function doChangeDirectory( _
            ByRef strErrMsg As String, _
            ByVal strUrl As String, _
            ByVal strUser As String, _
            ByVal strPassword As String, _
            ByVal strProxyUrl As String, _
            ByVal strProxyUser As String, _
            ByVal strProxyPassword As String, _
            ByRef blnExisted As Boolean) As Boolean

            Dim objBaseURI As New Xydc.Platform.Common.Utilities.BaseURI
            Dim objFtpWebResponse As Xydc.Net.FtpWebResponse
            Dim objFtpWebRequest As Xydc.Net.FtpWebRequest
            Dim objWebResponse As System.Net.WebResponse
            Dim objWebRequest As System.Net.WebRequest

            doChangeDirectory = False
            strErrMsg = ""
            blnExisted = False

            Try
                '����
                If strUrl Is Nothing Then strUrl = ""
                If strUser Is Nothing Then strUser = ""
                If strPassword Is Nothing Then strPassword = ""
                If strProxyUrl Is Nothing Then strProxyUrl = ""
                If strProxyUser Is Nothing Then strProxyUser = ""
                If strProxyPassword Is Nothing Then strProxyPassword = ""
                strUrl = strUrl.Trim()
                strProxyUser = strProxyUser.Trim()
                strPassword = strPassword.Trim()
                strProxyUrl = strProxyUrl.Trim()
                strProxyUser = strProxyUser.Trim()
                strProxyPassword = strProxyPassword.Trim()
                If strUrl = "" Or Not (strUser <> "" Or (strProxyUrl <> "" And strProxyUser <> "")) Then
                    strErrMsg = "����[doChangeDirectory]û�д������������"
                    GoTo errProc
                End If

                '��Url�л�ȡ·��
                Dim objUri As New System.Uri(strUrl)
                Dim strPath As String
                strPath = objBaseURI.getPathName(strUrl)
                With objUri
                    strUrl = objBaseURI.getAbsolutePath(.Scheme, .Host, .Port, strPath)
                End With

                'ע����Э����
                Dim objCreator As Xydc.Net.FtpRequestCreator = New Xydc.Net.FtpRequestCreator
                System.Net.WebRequest.RegisterPrefix("ftp:", objCreator)

                '����FtpWebRequest
                objWebRequest = System.Net.WebRequest.Create(strUrl)
                objWebRequest.Method = "cd" '���õ�ǰĿ¼
                Try
                    objFtpWebRequest = CType(objWebRequest, Xydc.Net.FtpWebRequest)
                Catch
                    objFtpWebRequest = Nothing
                End Try
                objFtpWebRequest.Passive = Xydc.Platform.Common.jsoaConfiguration.FTPPassiveMode 'Passive
                objFtpWebRequest.ContentType = "binary"                                       'ascii

                'ͨ��FTP�������FTP������
                If (strProxyUrl <> "") Then
                    Dim objWebProxy As System.Net.WebProxy = New System.Net.WebProxy(strProxyUrl)
                    If (strProxyUser <> "") Then
                        objWebProxy.Credentials = New System.Net.NetworkCredential(strProxyUser, strProxyPassword)
                    End If
                    objFtpWebRequest.Proxy = objWebProxy
                End If

                'ֱ�ӷ���
                If (strUser <> "") Then
                    objFtpWebRequest.Credentials = New System.Net.NetworkCredential(strUser, strPassword)
                End If

                'ִ��FTPָ����������ȡ����������Ӧ����
                objWebResponse = objFtpWebRequest.GetResponse()
                objFtpWebResponse = CType(objWebResponse, Xydc.Net.FtpWebResponse)
                If objFtpWebResponse.Status = -1 Then
                    strErrMsg = objFtpWebResponse.StatusDescription
                    GoTo errProc
                End If
                If objFtpWebResponse.Status <> 550 Then
                    blnExisted = True
                End If
                'objFtpWebResponse.Status=550 ��ʾĿ¼������
            Catch ex As Exception

                'strErrMsg = ex.Message
                strErrMsg = "FTP����!����ϵ����Ա!"

                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFtpWebResponse)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFtpWebRequest)
            Xydc.Platform.Common.Utilities.BaseURI.SafeRelease(objBaseURI)

            doChangeDirectory = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFtpWebResponse)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFtpWebRequest)
            Xydc.Platform.Common.Utilities.BaseURI.SafeRelease(objBaseURI)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��FTP�������д���ָ��Ŀ¼(ֻ�������1��Ŀ¼)
        '     strErrMsg        �����ش�����Ϣ
        '     strUrl           ����������Url·��
        '     strUser          ��FTP��������֤�û�
        '     strPassword      ��FTP��������֤�û�����
        '     strProxyUrl      ��FTP�����Url
        '     strProxyUser     ��FTP������֤�û�
        '     strProxyPassword ��FTP������֤�û�����
        '     blnUnused        ��������
        ' ����
        '     True             ���ɹ�
        '     False            ��ʧ��
        '----------------------------------------------------------------
        Private Function doCreateDirectory( _
            ByRef strErrMsg As String, _
            ByVal strUrl As String, _
            ByVal strUser As String, _
            ByVal strPassword As String, _
            ByVal strProxyUrl As String, _
            ByVal strProxyUser As String, _
            ByVal strProxyPassword As String, _
            ByVal blnUnused As Boolean) As Boolean

            Dim objFtpWebResponse As Xydc.Net.FtpWebResponse
            Dim objFtpWebRequest As Xydc.Net.FtpWebRequest
            Dim objWebResponse As System.Net.WebResponse
            Dim objWebRequest As System.Net.WebRequest

            doCreateDirectory = False
            strErrMsg = ""

            Try
                '����
                If strUrl Is Nothing Then strUrl = ""
                If strUser Is Nothing Then strUser = ""
                If strPassword Is Nothing Then strPassword = ""
                If strProxyUrl Is Nothing Then strProxyUrl = ""
                If strProxyUser Is Nothing Then strProxyUser = ""
                If strProxyPassword Is Nothing Then strProxyPassword = ""
                strUrl = strUrl.Trim()
                strProxyUser = strProxyUser.Trim()
                strPassword = strPassword.Trim()
                strProxyUrl = strProxyUrl.Trim()
                strProxyUser = strProxyUser.Trim()
                strProxyPassword = strProxyPassword.Trim()
                If strUrl = "" Or Not (strUser <> "" Or (strProxyUrl <> "" And strProxyUser <> "")) Then
                    strErrMsg = "����[doCreateDirectory]û�д������������"
                    GoTo errProc
                End If

                'ע����Э����
                Dim objCreator As Xydc.Net.FtpRequestCreator = New Xydc.Net.FtpRequestCreator
                System.Net.WebRequest.RegisterPrefix("ftp:", objCreator)

                '����FtpWebRequest
                objWebRequest = System.Net.WebRequest.Create(strUrl)
                objWebRequest.Method = "md" '����Ŀ¼
                Try
                    objFtpWebRequest = CType(objWebRequest, Xydc.Net.FtpWebRequest)
                Catch
                    objFtpWebRequest = Nothing
                End Try
                objFtpWebRequest.Passive = Xydc.Platform.Common.jsoaConfiguration.FTPPassiveMode  'Passive
                objFtpWebRequest.ContentType = "binary"                                        'ascii

                'ͨ��FTP�������FTP������
                If (strProxyUrl <> "") Then
                    Dim objWebProxy As System.Net.WebProxy = New System.Net.WebProxy(strProxyUrl)
                    If (strProxyUser <> "") Then
                        objWebProxy.Credentials = New System.Net.NetworkCredential(strProxyUser, strProxyPassword)
                    End If
                    objFtpWebRequest.Proxy = objWebProxy
                End If

                'ֱ�ӷ���
                If (strUser <> "") Then
                    objFtpWebRequest.Credentials = New System.Net.NetworkCredential(strUser, strPassword)
                End If

                'ִ��FTPָ����������ȡ����������Ӧ����
                objWebResponse = objFtpWebRequest.GetResponse()
                objFtpWebResponse = CType(objWebResponse, Xydc.Net.FtpWebResponse)
                If objFtpWebResponse.Status = -1 Then
                    strErrMsg = objFtpWebResponse.StatusDescription
                    GoTo errProc
                End If
                System.Threading.Thread.Sleep(15)
            Catch ex As Exception

                'strErrMsg = ex.Message
                strErrMsg = "FTP����!����ϵ����Ա!"

                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFtpWebResponse)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFtpWebRequest)

            doCreateDirectory = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFtpWebResponse)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFtpWebRequest)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��FTP�������д���ָ��Ŀ¼(����Ŀ¼���˳�򴴽�)
        '     strErrMsg        �����ش�����Ϣ
        '     strUrl           ����������Url·�����ļ���
        '     strUser          ��FTP��������֤�û�
        '     strPassword      ��FTP��������֤�û�����
        '     strProxyUrl      ��FTP�����Url
        '     strProxyUser     ��FTP������֤�û�
        '     strProxyPassword ��FTP������֤�û�����
        ' ����
        '     True             ���ɹ�
        '     False            ��ʧ��
        '----------------------------------------------------------------
        Public Function doCreateDirectory( _
            ByRef strErrMsg As String, _
            ByVal strUrl As String, _
            ByVal strUser As String, _
            ByVal strPassword As String, _
            ByVal strProxyUrl As String, _
            ByVal strProxyUser As String, _
            ByVal strProxyPassword As String) As Boolean

            Dim objBaseURI As New Xydc.Platform.Common.Utilities.BaseURI
            Dim objFtpWebResponse As Xydc.Net.FtpWebResponse
            Dim objFtpWebRequest As Xydc.Net.FtpWebRequest
            Dim objWebResponse As System.Net.WebResponse
            Dim objWebRequest As System.Net.WebRequest

            doCreateDirectory = False
            strErrMsg = ""

            Try
                '����
                If strUrl Is Nothing Then strUrl = ""
                If strUser Is Nothing Then strUser = ""
                If strPassword Is Nothing Then strPassword = ""
                If strProxyUrl Is Nothing Then strProxyUrl = ""
                If strProxyUser Is Nothing Then strProxyUser = ""
                If strProxyPassword Is Nothing Then strProxyPassword = ""
                strUrl = strUrl.Trim()
                strProxyUser = strProxyUser.Trim()
                strPassword = strPassword.Trim()
                strProxyUrl = strProxyUrl.Trim()
                strProxyUser = strProxyUser.Trim()
                strProxyPassword = strProxyPassword.Trim()
                If strUrl = "" Or Not (strUser <> "" Or (strProxyUrl <> "" And strProxyUser <> "")) Then
                    strErrMsg = "����[doCreateDirectory]û�д������������"
                    GoTo errProc
                End If

                '��ȡĿ¼����
                Dim objUri As New System.Uri(strUrl)
                Dim intLevel As Integer
                intLevel = objBaseURI.getPathLevel(strUrl)

                '�𼶴���
                Dim blnExisted As Boolean
                Dim strUrlCache As String
                Dim strPath As String
                Dim i As Integer
                For i = 1 To intLevel Step 1
                    strPath = objBaseURI.getPathName(strUrl, i)
                    With objUri
                        strUrlCache = objBaseURI.getAbsolutePath(.Scheme, .Host, .Port, strPath)
                    End With
                    If Me.doChangeDirectory(strErrMsg, strUrlCache, strUser, strPassword, strProxyUrl, strProxyUser, strProxyPassword, blnExisted) = False Then
                        GoTo errProc
                    End If
                    If blnExisted = False Then
                        '����Ŀ¼
                        If Me.doCreateDirectory(strErrMsg, strUrlCache, strUser, strPassword, strProxyUrl, strProxyUser, strProxyPassword, True) = False Then
                            GoTo errProc
                        End If
                    End If
                Next
            Catch ex As Exception

                'strErrMsg = ex.Message
                strErrMsg = "FTP����!����ϵ����Ա!"

                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFtpWebResponse)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFtpWebRequest)
            Xydc.Platform.Common.Utilities.BaseURI.SafeRelease(objBaseURI)

            doCreateDirectory = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFtpWebResponse)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFtpWebRequest)
            Xydc.Platform.Common.Utilities.BaseURI.SafeRelease(objBaseURI)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��FTP�������н�ָ���ļ����ĵ����ļ���(Ŀ��·��������Դ·����ͬ)
        '     strErrMsg        �����ش�����Ϣ
        '     strFromUrl       ��Դ�ļ��������FTP��·��
        '     strToUrl         �����ļ��������FTP��·��
        '     strUser          ��FTP��������֤�û�
        '     strPassword      ��FTP��������֤�û�����
        '     strProxyUrl      ��FTP�����Url
        '     strProxyUser     ��FTP������֤�û�
        '     strProxyPassword ��FTP������֤�û�����
        ' ����
        '     True             ���ɹ�
        '     False            ��ʧ��
        '----------------------------------------------------------------
        Public Function doRenameFile( _
            ByRef strErrMsg As String, _
            ByVal strFromUrl As String, _
            ByVal strToUrl As String, _
            ByVal strUser As String, _
            ByVal strPassword As String, _
            ByVal strProxyUrl As String, _
            ByVal strProxyUser As String, _
            ByVal strProxyPassword As String) As Boolean

            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objBaseURI As New Xydc.Platform.Common.Utilities.BaseURI
            Dim objFtpWebResponse As Xydc.Net.FtpWebResponse
            Dim objFtpWebRequest As Xydc.Net.FtpWebRequest
            Dim objWebResponse As System.Net.WebResponse
            Dim objWebRequest As System.Net.WebRequest

            doRenameFile = False
            strErrMsg = ""

            Try
                '����
                If strFromUrl Is Nothing Then strFromUrl = ""
                If strUser Is Nothing Then strUser = ""
                If strPassword Is Nothing Then strPassword = ""
                If strProxyUrl Is Nothing Then strProxyUrl = ""
                If strProxyUser Is Nothing Then strProxyUser = ""
                If strProxyPassword Is Nothing Then strProxyPassword = ""
                strFromUrl = strFromUrl.Trim()
                strProxyUser = strProxyUser.Trim()
                strPassword = strPassword.Trim()
                strProxyUrl = strProxyUrl.Trim()
                strProxyUser = strProxyUser.Trim()
                strProxyPassword = strProxyPassword.Trim()
                If strFromUrl = "" Or Not (strUser <> "" Or (strProxyUrl <> "" And strProxyUser <> "")) Or strToUrl = "" Then
                    strErrMsg = "����[doRenameFile]û�д������������"
                    GoTo errProc
                End If

                'ɾ��Ҫ�������ļ������������ɾ����
                If Me.doDeleteFile(strErrMsg, strToUrl, strUser, strPassword, strProxyUrl, strProxyUser, strProxyPassword) = False Then
                    '���Բ��ɹ�����Ϊ�ļ���Ŀ¼���ܲ����ڡ�
                End If
                '����Ŀ��Ŀ¼
                If Me.doCreateDirectory(strErrMsg, strToUrl, strUser, strPassword, strProxyUrl, strProxyUser, strProxyPassword) = False Then
                    GoTo errProc
                End If
                '��ȡĿ���ļ������FTP��Ŀ¼��·�����ļ���
                Dim strToSpec As String
                Dim strToFile As String
                Dim strToPath As String
                strToFile = objBaseURI.getFileName(strToUrl)
                strToPath = objBaseURI.getPathName(strToUrl, True)
                strToPath = objBaseURI.doConvertToLocalPath(strToPath)
                strToSpec = objBaseLocalFile.doMakePath(strToPath, strToFile)

                '��ȡԴ�ļ���·�����ļ���
                Dim objFromUri As New System.Uri(strFromUrl)
                Dim strFromFile As String
                Dim strFromPath As String
                strFromFile = objBaseURI.getFileName(strFromUrl)
                strFromPath = objBaseURI.getPathName(strFromUrl)
                strFromPath = objBaseURI.getAbsolutePath(objFromUri.Scheme, objFromUri.Host, objFromUri.Port, strFromPath)

                'ע����Э����
                Dim objCreator As Xydc.Net.FtpRequestCreator = New Xydc.Net.FtpRequestCreator
                System.Net.WebRequest.RegisterPrefix("ftp:", objCreator)

                '����FtpWebRequest
                objWebRequest = System.Net.WebRequest.Create(strFromPath) 'Դ�ļ���FTP����·��(�����ļ���)
                objWebRequest.Method = "ren" '�����ļ���
                Try
                    objFtpWebRequest = CType(objWebRequest, Xydc.Net.FtpWebRequest)
                Catch
                    objFtpWebRequest = Nothing
                End Try
                objFtpWebRequest.FromFile = strFromFile 'Դ�ļ���
                objFtpWebRequest.ToFile = strToSpec     'Ŀ���ļ������FTP��Ŀ¼��·�����ļ���
                objFtpWebRequest.Passive = Xydc.Platform.Common.jsoaConfiguration.FTPPassiveMode  'Passive
                objFtpWebRequest.ContentType = "binary"                                        'ascii

                'ͨ��FTP�������FTP������
                If (strProxyUrl <> "") Then
                    Dim objWebProxy As System.Net.WebProxy = New System.Net.WebProxy(strProxyUrl)
                    If (strProxyUser <> "") Then
                        objWebProxy.Credentials = New System.Net.NetworkCredential(strProxyUser, strProxyPassword)
                    End If
                    objFtpWebRequest.Proxy = objWebProxy
                End If

                'ֱ�ӷ���
                If (strUser <> "") Then
                    objFtpWebRequest.Credentials = New System.Net.NetworkCredential(strUser, strPassword)
                End If

                'ִ��FTPָ����������ȡ����������Ӧ����
                objWebResponse = objFtpWebRequest.GetResponse()
                objFtpWebResponse = CType(objWebResponse, Xydc.Net.FtpWebResponse)
                If objFtpWebResponse.Status = -1 Then
                    strErrMsg = objFtpWebResponse.StatusDescription
                    GoTo errProc
                End If
                If objFtpWebResponse.Status = 550 Then
                    strErrMsg = objFtpWebResponse.StatusDescription
                    GoTo errProc
                End If
                System.Threading.Thread.Sleep(15)
            Catch ex As Exception

                'strErrMsg = ex.Message
                strErrMsg = "FTP����!����ϵ����Ա!"

                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFtpWebResponse)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFtpWebRequest)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.BaseURI.SafeRelease(objBaseURI)

            doRenameFile = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFtpWebResponse)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFtpWebRequest)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.BaseURI.SafeRelease(objBaseURI)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��FTP�������н�ָ���ļ����ĵ����ļ���(Ŀ��·��������Դ·����ͬ)
        '     strErrMsg        �����ش�����Ϣ
        '     strFromUrl       ��Դ�ļ��������FTP��·��
        '     strToFile        �����ļ���
        '     strUser          ��FTP��������֤�û�
        '     strPassword      ��FTP��������֤�û�����
        '     strProxyUrl      ��FTP�����Url
        '     strProxyUser     ��FTP������֤�û�
        '     strProxyPassword ��FTP������֤�û�����
        ' ����
        '     True             ���ɹ�
        '     False            ��ʧ��
        '----------------------------------------------------------------
        Public Function doRenameFile( _
            ByRef strErrMsg As String, _
            ByVal strFromUrl As String, _
            ByVal strToFile As String, _
            ByVal strUser As String, _
            ByVal strPassword As String, _
            ByVal strProxyUrl As String, _
            ByVal strProxyUser As String, _
            ByVal strProxyPassword As String, _
            ByVal blnSamePath As Boolean) As Boolean

            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objBaseURI As New Xydc.Platform.Common.Utilities.BaseURI
            Dim objFtpWebResponse As Xydc.Net.FtpWebResponse
            Dim objFtpWebRequest As Xydc.Net.FtpWebRequest
            Dim objWebResponse As System.Net.WebResponse
            Dim objWebRequest As System.Net.WebRequest

            doRenameFile = False
            strErrMsg = ""

            Try
                '����
                If strFromUrl Is Nothing Then strFromUrl = ""
                If strUser Is Nothing Then strUser = ""
                If strPassword Is Nothing Then strPassword = ""
                If strProxyUrl Is Nothing Then strProxyUrl = ""
                If strProxyUser Is Nothing Then strProxyUser = ""
                If strProxyPassword Is Nothing Then strProxyPassword = ""
                strFromUrl = strFromUrl.Trim()
                strProxyUser = strProxyUser.Trim()
                strPassword = strPassword.Trim()
                strProxyUrl = strProxyUrl.Trim()
                strProxyUser = strProxyUser.Trim()
                strProxyPassword = strProxyPassword.Trim()
                If strFromUrl = "" Or Not (strUser <> "" Or (strProxyUrl <> "" And strProxyUser <> "")) Or strToFile = "" Then
                    strErrMsg = "����[doRenameFile]û�д������������"
                    GoTo errProc
                End If

                '��ȡĿ���ļ��ľ���Url
                Dim objFromUri As New System.Uri(strFromUrl)
                Dim strToUrl As String
                strToUrl = objBaseURI.getPathName(strFromUrl)
                strToUrl = objBaseURI.getAbsolutePath(objFromUri.Scheme, objFromUri.Host, objFromUri.Port, strToUrl, strToFile)

                'ɾ��Ҫ�������ļ������������ɾ����
                If Me.doDeleteFile(strErrMsg, strToUrl, strUser, strPassword, strProxyUrl, strProxyUser, strProxyPassword) = False Then
                    '���Բ��ɹ�����Ϊ�ļ���Ŀ¼���ܲ����ڡ�
                End If

                '��ȡԴ�ļ���·�����ļ���
                Dim strFromFile As String
                Dim strFromPath As String
                strFromFile = objBaseURI.getFileName(strFromUrl)
                strFromPath = objBaseURI.getPathName(strFromUrl)
                strFromPath = objBaseURI.getAbsolutePath(objFromUri.Scheme, objFromUri.Host, objFromUri.Port, strFromPath)

                'ע����Э����
                Dim objCreator As Xydc.Net.FtpRequestCreator = New Xydc.Net.FtpRequestCreator
                System.Net.WebRequest.RegisterPrefix("ftp:", objCreator)

                '����FtpWebRequest
                objWebRequest = System.Net.WebRequest.Create(strFromPath) 'Դ�ļ���FTP����·��(�����ļ���)
                objWebRequest.Method = "ren" '�����ļ���
                Try
                    objFtpWebRequest = CType(objWebRequest, Xydc.Net.FtpWebRequest)
                Catch
                    objFtpWebRequest = Nothing
                End Try
                objFtpWebRequest.FromFile = strFromFile 'Դ�ļ���
                objFtpWebRequest.ToFile = strToFile     'Ŀ���ļ���
                objFtpWebRequest.Passive = Xydc.Platform.Common.jsoaConfiguration.FTPPassiveMode  'Passive
                objFtpWebRequest.ContentType = "binary"                                        'ascii

                'ͨ��FTP�������FTP������
                If (strProxyUrl <> "") Then
                    Dim objWebProxy As System.Net.WebProxy = New System.Net.WebProxy(strProxyUrl)
                    If (strProxyUser <> "") Then
                        objWebProxy.Credentials = New System.Net.NetworkCredential(strProxyUser, strProxyPassword)
                    End If
                    objFtpWebRequest.Proxy = objWebProxy
                End If

                'ֱ�ӷ���
                If (strUser <> "") Then
                    objFtpWebRequest.Credentials = New System.Net.NetworkCredential(strUser, strPassword)
                End If

                'ִ��FTPָ����������ȡ����������Ӧ����
                objWebResponse = objFtpWebRequest.GetResponse()
                objFtpWebResponse = CType(objWebResponse, Xydc.Net.FtpWebResponse)
                If objFtpWebResponse.Status = -1 Then
                    strErrMsg = objFtpWebResponse.StatusDescription
                    GoTo errProc
                End If
                If objFtpWebResponse.Status = 550 Then
                    strErrMsg = objFtpWebResponse.StatusDescription
                    GoTo errProc
                End If
                System.Threading.Thread.Sleep(15)
            Catch ex As Exception

                'strErrMsg = ex.Message
                strErrMsg = "FTP����!����ϵ����Ա!"

                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFtpWebResponse)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFtpWebRequest)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.BaseURI.SafeRelease(objBaseURI)

            doRenameFile = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFtpWebResponse)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFtpWebRequest)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.BaseURI.SafeRelease(objBaseURI)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �ж�ָ��strUrl���ļ��Ƿ���ڣ�
        '     strErrMsg        �����ش�����Ϣ
        '     strUrl           ��FTP��������Url·�����ļ���
        '     strUser          ��FTP��������֤�û�
        '     strPassword      ��FTP��������֤�û�����
        '     strProxyUrl      ��FTP�����Url
        '     strProxyUser     ��FTP������֤�û�
        '     strProxyPassword ��FTP������֤�û�����
        '     blnExisted       ���ļ�����=True
        ' ����
        '     True             ���ɹ�
        '     False            ��ʧ��
        '----------------------------------------------------------------
        Public Function isFileExisted( _
            ByRef strErrMsg As String, _
            ByVal strUrl As String, _
            ByVal strUser As String, _
            ByVal strPassword As String, _
            ByVal strProxyUrl As String, _
            ByVal strProxyUser As String, _
            ByVal strProxyPassword As String, _
            ByRef blnExisted As Boolean) As Boolean

            Dim objBaseURI As New Xydc.Platform.Common.Utilities.BaseURI
            Dim objFtpWebResponse As Xydc.Net.FtpWebResponse
            Dim objFtpWebRequest As Xydc.Net.FtpWebRequest
            Dim objWebResponse As System.Net.WebResponse
            Dim objWebRequest As System.Net.WebRequest

            isFileExisted = False
            strErrMsg = ""
            blnExisted = False

            Try
                '����
                If strUrl Is Nothing Then strUrl = ""
                If strUser Is Nothing Then strUser = ""
                If strPassword Is Nothing Then strPassword = ""
                If strProxyUrl Is Nothing Then strProxyUrl = ""
                If strProxyUser Is Nothing Then strProxyUser = ""
                If strProxyPassword Is Nothing Then strProxyPassword = ""
                strUrl = strUrl.Trim()
                strProxyUser = strProxyUser.Trim()
                strPassword = strPassword.Trim()
                strProxyUrl = strProxyUrl.Trim()
                strProxyUser = strProxyUser.Trim()
                strProxyPassword = strProxyPassword.Trim()
                If strUrl = "" Or Not (strUser <> "" Or (strProxyUrl <> "" And strProxyUser <> "")) Then
                    strErrMsg = "����[isFileExisted]û�д������������"
                    GoTo errProc
                End If

                'ע����Э����
                Dim objCreator As Xydc.Net.FtpRequestCreator = New Xydc.Net.FtpRequestCreator
                System.Net.WebRequest.RegisterPrefix("ftp:", objCreator)

                '����FtpWebRequest
                objWebRequest = System.Net.WebRequest.Create(strUrl)
                objWebRequest.Method = "dir" '�����ļ�
                Try
                    objFtpWebRequest = CType(objWebRequest, Xydc.Net.FtpWebRequest)
                Catch
                    objFtpWebRequest = Nothing
                End Try
                objFtpWebRequest.Passive = Xydc.Platform.Common.jsoaConfiguration.FTPPassiveMode  'Passive
                objFtpWebRequest.ContentType = "binary"                                        'ascii

                'ͨ��FTP�������FTP������
                If (strProxyUrl <> "") Then
                    Dim objWebProxy As System.Net.WebProxy = New System.Net.WebProxy(strProxyUrl)
                    If (strProxyUser <> "") Then
                        objWebProxy.Credentials = New System.Net.NetworkCredential(strProxyUser, strProxyPassword)
                    End If
                    objFtpWebRequest.Proxy = objWebProxy
                End If

                'ֱ�ӷ���
                If (strUser <> "") Then
                    objFtpWebRequest.Credentials = New System.Net.NetworkCredential(strUser, strPassword)
                End If

                'ִ��FTPָ����������ȡ����������Ӧ����
                objWebResponse = objFtpWebRequest.GetResponse()
                objFtpWebResponse = CType(objWebResponse, Xydc.Net.FtpWebResponse)
                If objFtpWebResponse.Status = -1 Then
                    strErrMsg = objFtpWebResponse.StatusDescription
                    GoTo errProc
                End If
                If objFtpWebResponse.Status <> 550 Then
                    blnExisted = True
                End If
                'objFtpWebResponse.Status=550 ��ʾ�ļ�������
            Catch ex As Exception

                'strErrMsg = ex.Message
                strErrMsg = "FTP����!����ϵ����Ա!"

                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFtpWebResponse)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFtpWebRequest)
            Xydc.Platform.Common.Utilities.BaseURI.SafeRelease(objBaseURI)

            isFileExisted = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFtpWebResponse)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFtpWebRequest)
            Xydc.Platform.Common.Utilities.BaseURI.SafeRelease(objBaseURI)
            Exit Function

        End Function















        '----------------------------------------------------------------
        ' ����FTP�ļ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objFTPProperty       ��FTP����������
        '     strFTPFile           ��FTP�ļ�·��(��Ը�Ŀ¼)
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doBackupFile( _
            ByRef strErrMsg As String, _
            ByVal objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty, _
            ByVal strFTPFile As String) As Boolean

            Dim strBakExt As String = Xydc.Platform.Common.Utilities.PulicParameters.BACKUPFILEEXT
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile

            doBackupFile = False
            strErrMsg = ""

            Try
                If strFTPFile Is Nothing Then strFTPFile = ""
                strFTPFile = strFTPFile.Trim
                If strFTPFile = "" Then
                    Exit Try
                End If
                If objFTPProperty Is Nothing Then
                    strErrMsg = "����δ����FTP������������"
                    GoTo errProc
                End If

                '����ԭ�ļ�
                Dim blnExisted As Boolean
                Dim strFileName As String
                Dim strUrl As String
                With objFTPProperty
                    strUrl = .getUrl(strFTPFile)
                    If Me.isFileExisted(strErrMsg, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword, blnExisted) = False Then
                        '���Բ��ɹ����������ļ�������
                    Else
                        If blnExisted = True Then
                            strFileName = objBaseLocalFile.getFileName(strFTPFile) + strBakExt
                            If Me.doRenameFile(strErrMsg, strUrl, strFileName, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword, True) = False Then
                                GoTo errProc
                            End If
                        End If
                    End If
                End With
            Catch ex As Exception

                'strErrMsg = ex.Message
                strErrMsg = "FTP����!����ϵ����Ա!"

                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)

            doBackupFile = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �ӱ����лָ��ļ�
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     objFTPProperty         ��FTP���Ӳ���
        '     strFTPFile             ��FTP�ļ�·��(��Ը�Ŀ¼)
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Function doRestoreFile( _
            ByRef strErrMsg As String, _
            ByVal objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty, _
            ByVal strFTPFile As String) As Boolean

            Dim strBakExt As String = Xydc.Platform.Common.Utilities.PulicParameters.BACKUPFILEEXT

            doRestoreFile = False
            strErrMsg = ""

            Try
                '���
                If strFTPFile Is Nothing Then strFTPFile = ""
                strFTPFile = strFTPFile.Trim
                If strFTPFile = "" Then
                    Exit Try
                End If
                If objFTPProperty Is Nothing Then
                    strErrMsg = "����δָ��FTP���������Ӳ�����"
                    GoTo errProc
                End If

                '����
                Dim strOldFile As String = strFTPFile
                Dim blnExisted As Boolean
                Dim strToUrl As String
                Dim strUrl As String
                If strOldFile <> "" Then
                    With objFTPProperty
                        strUrl = .getUrl(strOldFile + strBakExt)
                        If Me.isFileExisted(strErrMsg, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword, blnExisted) = False Then
                            '���Բ��ɹ����������ļ�������
                        Else
                            If blnExisted = True Then
                                strToUrl = .getUrl(strOldFile)
                                Me.doRenameFile(strErrMsg, strUrl, strToUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword)
                            End If
                        End If
                    End With
                End If
            Catch ex As Exception

                'strErrMsg = ex.Message
                strErrMsg = "FTP����!����ϵ����Ա!"

                GoTo errProc
            End Try

            doRestoreFile = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ɾ�������ļ�
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     objFTPProperty         ��FTP���Ӳ���
        '     strFTPFile             ��FTP�ļ�·��(��Ը�Ŀ¼)
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Function doDeleteBackupFile( _
            ByRef strErrMsg As String, _
            ByVal objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty, _
            ByVal strFTPFile As String) As Boolean

            Dim strBakExt As String = Xydc.Platform.Common.Utilities.PulicParameters.BACKUPFILEEXT

            doDeleteBackupFile = False
            strErrMsg = ""

            Try
                '���
                If strFTPFile Is Nothing Then strFTPFile = ""
                strFTPFile = strFTPFile.Trim
                If strFTPFile = "" Then
                    Exit Try
                End If
                If objFTPProperty Is Nothing Then
                    strErrMsg = "����δָ��FTP���������Ӳ�����"
                    GoTo errProc
                End If

                'ɾ������
                Dim strOldFile As String = strFTPFile
                Dim strUrl As String
                If strOldFile <> "" Then
                    With objFTPProperty
                        strUrl = .getUrl(strOldFile + strBakExt)
                        If Me.doDeleteFile(strErrMsg, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword) = False Then
                            '���Բ��ɹ�,�γ���������
                        End If
                    End With
                End If
            Catch ex As Exception

                'strErrMsg = ex.Message
                strErrMsg = "FTP����!����ϵ����Ա!"

                GoTo errProc
            End Try

            doDeleteBackupFile = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �ϴ��ļ�
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     objFTPProperty         ��FTP���Ӳ���
        '     strLocalFile           ��׼���ϴ��ı����ļ�
        '     strFTPFile             ��Ŀ��FTP·��
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        ' ��ע
        '     strFTPFile��ʼ���벻����չ����·����
        '     ����strFTPFile + strLocalFile����չ��
        '----------------------------------------------------------------
        Public Function doUploadFile( _
            ByRef strErrMsg As String, _
            ByVal objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty, _
            ByVal strLocalFile As String, _
            ByRef strFTPFile As String) As Boolean

            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile

            doUploadFile = False
            strErrMsg = ""

            Try
                '���
                If strLocalFile Is Nothing Then strLocalFile = ""
                strLocalFile = strLocalFile.Trim
                If strLocalFile = "" Then
                    Exit Try
                End If
                If strFTPFile Is Nothing Then strFTPFile = ""
                strFTPFile = strFTPFile.Trim
                If strFTPFile = "" Then
                    Exit Try
                End If
                If objFTPProperty Is Nothing Then
                    strErrMsg = "����δָ��FTP���������Ӳ�����"
                    GoTo errProc
                End If

                '��ȡ����FTP·��
                Dim strExt As String
                strExt = objBaseLocalFile.getExtension(strLocalFile)
                strFTPFile = strFTPFile + strExt

                '�ϴ�
                Dim strUrl As String
                With objFTPProperty
                    strUrl = .getUrl(strFTPFile)
                    If Me.doPutFile(strErrMsg, strLocalFile, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword) = False Then
                        GoTo errProc
                    End If
                End With
            Catch ex As Exception

                'strErrMsg = ex.Message
                strErrMsg = "FTP����!����ϵ����Ա!"

                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)

            doUploadFile = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Exit Function

        End Function

    End Class

End Namespace
