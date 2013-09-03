Imports System
Imports System.Net

Namespace Xydc.Platform.Common.Utilities

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.Common.Utilities
    ' 类名    ：BaseFTP
    '
    ' 功能描述：
    '     处理ftp
    '----------------------------------------------------------------

    Public Class BaseFTP
        Implements IDisposable

        Public Const BufferSize As Integer = 1024








        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
        End Sub

        '----------------------------------------------------------------
        ' 虚拟析构函数
        '----------------------------------------------------------------
        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(True)
        End Sub

        '----------------------------------------------------------------
        ' 析构函数重载
        '----------------------------------------------------------------
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If (Not disposing) Then
                Exit Sub
            End If
        End Sub

        '----------------------------------------------------------------
        ' 安全释放本身资源
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
        ' 从Web应用服务器上载文件到FTP服务器
        ' FTP服务器中的目录与文件自动创建与覆盖！！！
        '     strErrMsg        ：返回错误信息
        '     strLocalFile     ：要上传的文件(全路径)
        '     strUrl           ：上传到FTP服务器的Url路径与文件名
        '     strUser          ：FTP服务器验证用户
        '     strPassword      ：FTP服务器验证用户密码
        '     strProxyUrl      ：FTP代理的Url
        '     strProxyUser     ：FTP代理验证用户
        '     strProxyPassword ：FTP代理验证用户密码
        ' 返回
        '     True             ：成功
        '     False            ：失败
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
                '检验
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
                    strErrMsg = "错误：[doPutFile]没有传入给定参数！"
                    GoTo errProc
                End If

                '检查文件是否存在?
                Dim blnExisted As Boolean
                If objBaseLocalFile.doFileExisted(strErrMsg, strLocalFile, blnExisted) = False Then
                    GoTo errProc
                End If
                If blnExisted = False Then
                    strErrMsg = "错误：[" + strLocalFile + "]不存在！"
                    GoTo errProc
                End If


                '备份上传文件，用备份文件进行加密上传！
                Dim strTempPath As String = objBaseLocalFile.getPathName(strLocalFile)
                Dim strTempFile As String = ""
                If objBaseLocalFile.doCopyToTempFile(strErrMsg, strLocalFile, strTempPath, strTempFile) = False Then
                    GoTo errProc
                End If
                blnDelete = True
                Dim strOrgFile As String = strLocalFile
                strLocalFile = objBaseLocalFile.doMakePath(strTempPath, strTempFile)
                strCacheFile = strLocalFile


                '加密文件
                If objPulicParameters.doEncryptFile(strErrMsg, strLocalFile) = False Then
                    GoTo errProc
                End If

                '逐级创建FTP目录
                If Me.doCreateDirectory(strErrMsg, strUrl, strUser, strPassword, strProxyUrl, strProxyUser, strProxyPassword) = False Then
                    GoTo errProc
                End If

                '注册插接协议类
                Dim objCreator As Xydc.Net.FtpRequestCreator = New Xydc.Net.FtpRequestCreator
                System.Net.WebRequest.RegisterPrefix("ftp:", objCreator)

                '创建FtpWebRequest
                objWebRequest = System.Net.WebRequest.Create(strUrl)
                objWebRequest.Method = "put"    '上载操作
                Try
                    objFtpWebRequest = CType(objWebRequest, Xydc.Net.FtpWebRequest)
                Catch
                    objFtpWebRequest = Nothing
                End Try

                objFtpWebRequest.Passive = Xydc.Platform.Common.jsoaConfiguration.FTPPassiveMode     'Passive
                objFtpWebRequest.ContentType = "binary"       'ascii

                '通过FTP代理访问FTP服务器
                If (strProxyUrl <> "") Then
                    Dim objWebProxy As System.Net.WebProxy = New System.Net.WebProxy(strProxyUrl)
                    If (strProxyUser <> "") Then
                        objWebProxy.Credentials = New System.Net.NetworkCredential(strProxyUser, strProxyPassword)
                    End If
                    objFtpWebRequest.Proxy = objWebProxy
                End If

                '直接访问
                If (strUser <> "") Then
                    objFtpWebRequest.Credentials = New System.Net.NetworkCredential(strUser, strPassword)
                End If

                '准备FTP操作需要的数据(将上传文件数据输入到内存流)
                '打开本地文件
                objSrcFileStream = New System.IO.FileStream(strLocalFile, System.IO.FileMode.Open, System.IO.FileAccess.Read)

                '打开FTP请求流
                objRequestStream = objFtpWebRequest.GetRequestStream()

                '写入到FTP请求流
                Dim intLength As Integer = Me.BufferSize
                Dim buffer() As Byte = New Byte(intLength) {}
                Dim intBytesRead As Integer = objSrcFileStream.Read(buffer, 0, intLength)
                While intBytesRead > 0
                    objRequestStream.Write(buffer, 0, intBytesRead)
                    intBytesRead = objSrcFileStream.Read(buffer, 0, intLength)
                End While

                '执行FTP指定操作并获取服务器的响应数据
                objWebResponse = objFtpWebRequest.GetResponse()
                objFtpWebResponse = CType(objWebResponse, Xydc.Net.FtpWebResponse)
                If objFtpWebResponse.Status = -1 Then
                    strErrMsg = objFtpWebResponse.StatusDescription
                    GoTo errProc
                End If


                '释放文件资源
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFtpWebResponse)
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSrcFileStream)
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objRequestStream)
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFtpWebRequest)


                '删除缓存文件

                If blnDelete = True And strCacheFile.Trim <> "" Then
                    If objBaseLocalFile.doDeleteFile(strErrMsg, strCacheFile) = False Then
                        '忽略
                    End If
                End If



                '强制等待
                If Xydc.Platform.Common.jsoaConfiguration.FTPPutFileWaitTime >= 0 Then
                    System.Threading.Thread.Sleep(Xydc.Platform.Common.jsoaConfiguration.FTPPutFileWaitTime)
                End If

            Catch ex As Exception

                'strErrMsg = ex.Message
                strErrMsg = "FTP出错!请联系管理员!"

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
            '删除缓存文件
            Dim strErrMsgA As String = ""
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFtpWebResponse)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSrcFileStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objRequestStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFtpWebRequest)

            If blnDelete = True And strCacheFile.Trim <> "" Then
                If objBaseLocalFile.doDeleteFile(strErrMsgA, strCacheFile) = False Then
                    '忽略
                End If
            End If

            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 从FTP服务器下载文件到Web应用服务器
        ' 本地目录与文件均自动创建与覆盖！！！
        '     strErrMsg        ：返回错误信息
        '     strLocalFile     ：要保存的文件(全路径)
        '     strUrl           ：要下载的FTP服务器的Url路径与文件名
        '     strUser          ：FTP服务器验证用户
        '     strPassword      ：FTP服务器验证用户密码
        '     strProxyUrl      ：FTP代理的Url
        '     strProxyUser     ：FTP代理验证用户
        '     strProxyPassword ：FTP代理验证用户密码
        ' 返回
        '     True             ：成功
        '     False            ：失败
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
                '检验
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
                    strErrMsg = "错误：[doGetFile]没有传入给定参数！"
                    GoTo errProc
                End If

                '自动创建本地目录
                If objBaseLocalFile.doCreateDirectory(strErrMsg, strLocalFile) = False Then
                    GoTo errProc
                End If

                '注册插接协议类
                Dim objCreator As Xydc.Net.FtpRequestCreator = New Xydc.Net.FtpRequestCreator
                System.Net.WebRequest.RegisterPrefix("ftp:", objCreator)

                '创建FtpWebRequest
                objWebRequest = System.Net.WebRequest.Create(strUrl)
                objWebRequest.Method = "get" '下载操作
                Try
                    objFtpWebRequest = CType(objWebRequest, Xydc.Net.FtpWebRequest)
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                objFtpWebRequest.Passive = Xydc.Platform.Common.jsoaConfiguration.FTPPassiveMode  'Passive
                objFtpWebRequest.ContentType = "binary"                                        'ascii

                '通过FTP代理访问FTP服务器
                If (strProxyUrl <> "") Then
                    Dim objWebProxy As System.Net.WebProxy = New System.Net.WebProxy(strProxyUrl)
                    If (strProxyUser <> "") Then
                        objWebProxy.Credentials = New System.Net.NetworkCredential(strProxyUser, strProxyPassword)
                    End If
                    objFtpWebRequest.Proxy = objWebProxy
                End If

                '直接访问
                If (strUser <> "") Then
                    objFtpWebRequest.Credentials = New System.Net.NetworkCredential(strUser, strPassword)
                End If

                '执行FTP指定操作并获取服务器的响应数据
                objWebResponse = objFtpWebRequest.GetResponse()
                objFtpWebResponse = CType(objWebResponse, Xydc.Net.FtpWebResponse)
                If objFtpWebResponse.Status = -1 Then
                    strErrMsg = objFtpWebResponse.StatusDescription
                    GoTo errProc
                End If
                objDownloadStream = objFtpWebResponse.GetResponseStream()
                If objDownloadStream Is Nothing Then
                    strErrMsg = "错误：未能从[" + strUrl + "]获取流数据！"
                    GoTo errProc
                End If

                '打开本地文件
                objSrcFileStream = New System.IO.FileStream(strLocalFile, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write)

                '写入到文件流
                Dim intLength As Integer = Me.BufferSize
                Dim buffer() As Byte = New Byte(intLength) {}
                Dim intBytesRead As Integer = objDownloadStream.Read(buffer, 0, intLength)
                While intBytesRead > 0
                    objSrcFileStream.Write(buffer, 0, intBytesRead)
                    intBytesRead = objDownloadStream.Read(buffer, 0, intLength)
                End While


                '释放文件资源
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFtpWebResponse)
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDownloadStream)
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSrcFileStream)
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFtpWebRequest)


                '解密文件
                If objPulicParameters.doDecryptFile(strErrMsg, strLocalFile) = False Then
                    GoTo errProc
                End If


                ''测试文件
                'Dim blnTestOK As Boolean = False
                'If objBaseLocalFile.doTestFile(strErrMsg, strLocalFile, blnTestOK) = False Then
                '    GoTo errProc
                'End If
                'If blnTestOK = False Then
                '    strErrMsg = "错误：[" + strUrl + "]未能下载成功！"
                '    GoTo errProc
                'End If
                '强制等待
                If Xydc.Platform.Common.jsoaConfiguration.FTPGetFileWaitTime >= 0 Then
                    System.Threading.Thread.Sleep(Xydc.Platform.Common.jsoaConfiguration.FTPGetFileWaitTime)
                End If

            Catch ex As Exception

                'strErrMsg = ex.Message
                strErrMsg = "FTP出错!请联系管理员!"

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
        ' 从FTP服务器删除指定的文件
        '     strErrMsg        ：返回错误信息
        '     strUrl           ：要删除的FTP服务器的Url路径与文件名
        '     strUser          ：FTP服务器验证用户
        '     strPassword      ：FTP服务器验证用户密码
        '     strProxyUrl      ：FTP代理的Url
        '     strProxyUser     ：FTP代理验证用户
        '     strProxyPassword ：FTP代理验证用户密码
        ' 返回
        '     True             ：成功
        '     False            ：失败
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
                '检验
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
                    strErrMsg = "错误：[doDeleteFile]没有传入给定参数！"
                    GoTo errProc
                End If

                '注册插接协议类
                Dim objCreator As Xydc.Net.FtpRequestCreator = New Xydc.Net.FtpRequestCreator
                System.Net.WebRequest.RegisterPrefix("ftp:", objCreator)

                '创建FtpWebRequest
                objWebRequest = System.Net.WebRequest.Create(strUrl)
                objWebRequest.Method = "delete" '删除操作
                Try
                    objFtpWebRequest = CType(objWebRequest, Xydc.Net.FtpWebRequest)
                Catch
                    objFtpWebRequest = Nothing
                End Try
                objFtpWebRequest.Passive = Xydc.Platform.Common.jsoaConfiguration.FTPPassiveMode  'Passive
                objFtpWebRequest.ContentType = "binary"                                        'ascii

                '通过FTP代理访问FTP服务器
                If (strProxyUrl <> "") Then
                    Dim objWebProxy As System.Net.WebProxy = New System.Net.WebProxy(strProxyUrl)
                    If (strProxyUser <> "") Then
                        objWebProxy.Credentials = New System.Net.NetworkCredential(strProxyUser, strProxyPassword)
                    End If
                    objFtpWebRequest.Proxy = objWebProxy
                End If

                '直接访问
                If (strUser <> "") Then
                    objFtpWebRequest.Credentials = New System.Net.NetworkCredential(strUser, strPassword)
                End If

                '执行FTP指定操作并获取服务器的响应数据
                objWebResponse = objFtpWebRequest.GetResponse()
                objFtpWebResponse = CType(objWebResponse, Xydc.Net.FtpWebResponse)
                If objFtpWebResponse.Status = -1 Then
                    strErrMsg = objFtpWebResponse.StatusDescription
                    GoTo errProc
                End If
                'objFtpWebResponse.Status=550 表示文件不存在
                System.Threading.Thread.Sleep(15)
            Catch ex As Exception

                'strErrMsg = ex.Message
                strErrMsg = "FTP出错!请联系管理员!"

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
        ' 将FTP服务器的目录定位到strUrl指定的位置
        '     strErrMsg        ：返回错误信息
        '     strUrl           ：FTP服务器的Url路径与文件名
        '     strUser          ：FTP服务器验证用户
        '     strPassword      ：FTP服务器验证用户密码
        '     strProxyUrl      ：FTP代理的Url
        '     strProxyUser     ：FTP代理验证用户
        '     strProxyPassword ：FTP代理验证用户密码
        '     blnExisted       ：目录存在=True
        ' 返回
        '     True             ：成功
        '     False            ：失败
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
                '检验
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
                    strErrMsg = "错误：[doChangeDirectory]没有传入给定参数！"
                    GoTo errProc
                End If

                '从Url中获取路径
                Dim objUri As New System.Uri(strUrl)
                Dim strPath As String
                strPath = objBaseURI.getPathName(strUrl)
                With objUri
                    strUrl = objBaseURI.getAbsolutePath(.Scheme, .Host, .Port, strPath)
                End With

                '注册插接协议类
                Dim objCreator As Xydc.Net.FtpRequestCreator = New Xydc.Net.FtpRequestCreator
                System.Net.WebRequest.RegisterPrefix("ftp:", objCreator)

                '创建FtpWebRequest
                objWebRequest = System.Net.WebRequest.Create(strUrl)
                objWebRequest.Method = "cd" '设置当前目录
                Try
                    objFtpWebRequest = CType(objWebRequest, Xydc.Net.FtpWebRequest)
                Catch
                    objFtpWebRequest = Nothing
                End Try
                objFtpWebRequest.Passive = Xydc.Platform.Common.jsoaConfiguration.FTPPassiveMode 'Passive
                objFtpWebRequest.ContentType = "binary"                                       'ascii

                '通过FTP代理访问FTP服务器
                If (strProxyUrl <> "") Then
                    Dim objWebProxy As System.Net.WebProxy = New System.Net.WebProxy(strProxyUrl)
                    If (strProxyUser <> "") Then
                        objWebProxy.Credentials = New System.Net.NetworkCredential(strProxyUser, strProxyPassword)
                    End If
                    objFtpWebRequest.Proxy = objWebProxy
                End If

                '直接访问
                If (strUser <> "") Then
                    objFtpWebRequest.Credentials = New System.Net.NetworkCredential(strUser, strPassword)
                End If

                '执行FTP指定操作并获取服务器的响应数据
                objWebResponse = objFtpWebRequest.GetResponse()
                objFtpWebResponse = CType(objWebResponse, Xydc.Net.FtpWebResponse)
                If objFtpWebResponse.Status = -1 Then
                    strErrMsg = objFtpWebResponse.StatusDescription
                    GoTo errProc
                End If
                If objFtpWebResponse.Status <> 550 Then
                    blnExisted = True
                End If
                'objFtpWebResponse.Status=550 表示目录不存在
            Catch ex As Exception

                'strErrMsg = ex.Message
                strErrMsg = "FTP出错!请联系管理员!"

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
        ' 在FTP服务器中创建指定目录(只创建最后1级目录)
        '     strErrMsg        ：返回错误信息
        '     strUrl           ：服务器的Url路径
        '     strUser          ：FTP服务器验证用户
        '     strPassword      ：FTP服务器验证用户密码
        '     strProxyUrl      ：FTP代理的Url
        '     strProxyUser     ：FTP代理验证用户
        '     strProxyPassword ：FTP代理验证用户密码
        '     blnUnused        ：重载用
        ' 返回
        '     True             ：成功
        '     False            ：失败
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
                '检验
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
                    strErrMsg = "错误：[doCreateDirectory]没有传入给定参数！"
                    GoTo errProc
                End If

                '注册插接协议类
                Dim objCreator As Xydc.Net.FtpRequestCreator = New Xydc.Net.FtpRequestCreator
                System.Net.WebRequest.RegisterPrefix("ftp:", objCreator)

                '创建FtpWebRequest
                objWebRequest = System.Net.WebRequest.Create(strUrl)
                objWebRequest.Method = "md" '创建目录
                Try
                    objFtpWebRequest = CType(objWebRequest, Xydc.Net.FtpWebRequest)
                Catch
                    objFtpWebRequest = Nothing
                End Try
                objFtpWebRequest.Passive = Xydc.Platform.Common.jsoaConfiguration.FTPPassiveMode  'Passive
                objFtpWebRequest.ContentType = "binary"                                        'ascii

                '通过FTP代理访问FTP服务器
                If (strProxyUrl <> "") Then
                    Dim objWebProxy As System.Net.WebProxy = New System.Net.WebProxy(strProxyUrl)
                    If (strProxyUser <> "") Then
                        objWebProxy.Credentials = New System.Net.NetworkCredential(strProxyUser, strProxyPassword)
                    End If
                    objFtpWebRequest.Proxy = objWebProxy
                End If

                '直接访问
                If (strUser <> "") Then
                    objFtpWebRequest.Credentials = New System.Net.NetworkCredential(strUser, strPassword)
                End If

                '执行FTP指定操作并获取服务器的响应数据
                objWebResponse = objFtpWebRequest.GetResponse()
                objFtpWebResponse = CType(objWebResponse, Xydc.Net.FtpWebResponse)
                If objFtpWebResponse.Status = -1 Then
                    strErrMsg = objFtpWebResponse.StatusDescription
                    GoTo errProc
                End If
                System.Threading.Thread.Sleep(15)
            Catch ex As Exception

                'strErrMsg = ex.Message
                strErrMsg = "FTP出错!请联系管理员!"

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
        ' 在FTP服务器中创建指定目录(整个目录层次顺序创建)
        '     strErrMsg        ：返回错误信息
        '     strUrl           ：服务器的Url路径与文件名
        '     strUser          ：FTP服务器验证用户
        '     strPassword      ：FTP服务器验证用户密码
        '     strProxyUrl      ：FTP代理的Url
        '     strProxyUser     ：FTP代理验证用户
        '     strProxyPassword ：FTP代理验证用户密码
        ' 返回
        '     True             ：成功
        '     False            ：失败
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
                '检验
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
                    strErrMsg = "错误：[doCreateDirectory]没有传入给定参数！"
                    GoTo errProc
                End If

                '获取目录级别
                Dim objUri As New System.Uri(strUrl)
                Dim intLevel As Integer
                intLevel = objBaseURI.getPathLevel(strUrl)

                '逐级创建
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
                        '创建目录
                        If Me.doCreateDirectory(strErrMsg, strUrlCache, strUser, strPassword, strProxyUrl, strProxyUser, strProxyPassword, True) = False Then
                            GoTo errProc
                        End If
                    End If
                Next
            Catch ex As Exception

                'strErrMsg = ex.Message
                strErrMsg = "FTP出错!请联系管理员!"

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
        ' 在FTP服务器中将指定文件更改到新文件名(目标路径可能与源路径不同)
        '     strErrMsg        ：返回错误信息
        '     strFromUrl       ：源文件的相对于FTP根路径
        '     strToUrl         ：新文件的相对于FTP根路径
        '     strUser          ：FTP服务器验证用户
        '     strPassword      ：FTP服务器验证用户密码
        '     strProxyUrl      ：FTP代理的Url
        '     strProxyUser     ：FTP代理验证用户
        '     strProxyPassword ：FTP代理验证用户密码
        ' 返回
        '     True             ：成功
        '     False            ：失败
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
                '检验
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
                    strErrMsg = "错误：[doRenameFile]没有传入给定参数！"
                    GoTo errProc
                End If

                '删除要改名的文件，如果存在则删除！
                If Me.doDeleteFile(strErrMsg, strToUrl, strUser, strPassword, strProxyUrl, strProxyUser, strProxyPassword) = False Then
                    '可以不成功：因为文件或目录可能不存在。
                End If
                '创建目标目录
                If Me.doCreateDirectory(strErrMsg, strToUrl, strUser, strPassword, strProxyUrl, strProxyUser, strProxyPassword) = False Then
                    GoTo errProc
                End If
                '获取目标文件相对于FTP根目录的路径与文件名
                Dim strToSpec As String
                Dim strToFile As String
                Dim strToPath As String
                strToFile = objBaseURI.getFileName(strToUrl)
                strToPath = objBaseURI.getPathName(strToUrl, True)
                strToPath = objBaseURI.doConvertToLocalPath(strToPath)
                strToSpec = objBaseLocalFile.doMakePath(strToPath, strToFile)

                '获取源文件的路径与文件名
                Dim objFromUri As New System.Uri(strFromUrl)
                Dim strFromFile As String
                Dim strFromPath As String
                strFromFile = objBaseURI.getFileName(strFromUrl)
                strFromPath = objBaseURI.getPathName(strFromUrl)
                strFromPath = objBaseURI.getAbsolutePath(objFromUri.Scheme, objFromUri.Host, objFromUri.Port, strFromPath)

                '注册插接协议类
                Dim objCreator As Xydc.Net.FtpRequestCreator = New Xydc.Net.FtpRequestCreator
                System.Net.WebRequest.RegisterPrefix("ftp:", objCreator)

                '创建FtpWebRequest
                objWebRequest = System.Net.WebRequest.Create(strFromPath) '源文件的FTP绝对路径(不含文件名)
                objWebRequest.Method = "ren" '更改文件名
                Try
                    objFtpWebRequest = CType(objWebRequest, Xydc.Net.FtpWebRequest)
                Catch
                    objFtpWebRequest = Nothing
                End Try
                objFtpWebRequest.FromFile = strFromFile '源文件名
                objFtpWebRequest.ToFile = strToSpec     '目标文件相对于FTP根目录的路径与文件名
                objFtpWebRequest.Passive = Xydc.Platform.Common.jsoaConfiguration.FTPPassiveMode  'Passive
                objFtpWebRequest.ContentType = "binary"                                        'ascii

                '通过FTP代理访问FTP服务器
                If (strProxyUrl <> "") Then
                    Dim objWebProxy As System.Net.WebProxy = New System.Net.WebProxy(strProxyUrl)
                    If (strProxyUser <> "") Then
                        objWebProxy.Credentials = New System.Net.NetworkCredential(strProxyUser, strProxyPassword)
                    End If
                    objFtpWebRequest.Proxy = objWebProxy
                End If

                '直接访问
                If (strUser <> "") Then
                    objFtpWebRequest.Credentials = New System.Net.NetworkCredential(strUser, strPassword)
                End If

                '执行FTP指定操作并获取服务器的响应数据
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
                strErrMsg = "FTP出错!请联系管理员!"

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
        ' 在FTP服务器中将指定文件更改到新文件名(目标路径可能与源路径相同)
        '     strErrMsg        ：返回错误信息
        '     strFromUrl       ：源文件的相对于FTP根路径
        '     strToFile        ：新文件名
        '     strUser          ：FTP服务器验证用户
        '     strPassword      ：FTP服务器验证用户密码
        '     strProxyUrl      ：FTP代理的Url
        '     strProxyUser     ：FTP代理验证用户
        '     strProxyPassword ：FTP代理验证用户密码
        ' 返回
        '     True             ：成功
        '     False            ：失败
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
                '检验
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
                    strErrMsg = "错误：[doRenameFile]没有传入给定参数！"
                    GoTo errProc
                End If

                '获取目标文件的绝对Url
                Dim objFromUri As New System.Uri(strFromUrl)
                Dim strToUrl As String
                strToUrl = objBaseURI.getPathName(strFromUrl)
                strToUrl = objBaseURI.getAbsolutePath(objFromUri.Scheme, objFromUri.Host, objFromUri.Port, strToUrl, strToFile)

                '删除要改名的文件，如果存在则删除！
                If Me.doDeleteFile(strErrMsg, strToUrl, strUser, strPassword, strProxyUrl, strProxyUser, strProxyPassword) = False Then
                    '可以不成功：因为文件或目录可能不存在。
                End If

                '获取源文件的路径与文件名
                Dim strFromFile As String
                Dim strFromPath As String
                strFromFile = objBaseURI.getFileName(strFromUrl)
                strFromPath = objBaseURI.getPathName(strFromUrl)
                strFromPath = objBaseURI.getAbsolutePath(objFromUri.Scheme, objFromUri.Host, objFromUri.Port, strFromPath)

                '注册插接协议类
                Dim objCreator As Xydc.Net.FtpRequestCreator = New Xydc.Net.FtpRequestCreator
                System.Net.WebRequest.RegisterPrefix("ftp:", objCreator)

                '创建FtpWebRequest
                objWebRequest = System.Net.WebRequest.Create(strFromPath) '源文件的FTP绝对路径(不含文件名)
                objWebRequest.Method = "ren" '更改文件名
                Try
                    objFtpWebRequest = CType(objWebRequest, Xydc.Net.FtpWebRequest)
                Catch
                    objFtpWebRequest = Nothing
                End Try
                objFtpWebRequest.FromFile = strFromFile '源文件名
                objFtpWebRequest.ToFile = strToFile     '目标文件名
                objFtpWebRequest.Passive = Xydc.Platform.Common.jsoaConfiguration.FTPPassiveMode  'Passive
                objFtpWebRequest.ContentType = "binary"                                        'ascii

                '通过FTP代理访问FTP服务器
                If (strProxyUrl <> "") Then
                    Dim objWebProxy As System.Net.WebProxy = New System.Net.WebProxy(strProxyUrl)
                    If (strProxyUser <> "") Then
                        objWebProxy.Credentials = New System.Net.NetworkCredential(strProxyUser, strProxyPassword)
                    End If
                    objFtpWebRequest.Proxy = objWebProxy
                End If

                '直接访问
                If (strUser <> "") Then
                    objFtpWebRequest.Credentials = New System.Net.NetworkCredential(strUser, strPassword)
                End If

                '执行FTP指定操作并获取服务器的响应数据
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
                strErrMsg = "FTP出错!请联系管理员!"

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
        ' 判断指定strUrl的文件是否存在？
        '     strErrMsg        ：返回错误信息
        '     strUrl           ：FTP服务器的Url路径与文件名
        '     strUser          ：FTP服务器验证用户
        '     strPassword      ：FTP服务器验证用户密码
        '     strProxyUrl      ：FTP代理的Url
        '     strProxyUser     ：FTP代理验证用户
        '     strProxyPassword ：FTP代理验证用户密码
        '     blnExisted       ：文件存在=True
        ' 返回
        '     True             ：成功
        '     False            ：失败
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
                '检验
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
                    strErrMsg = "错误：[isFileExisted]没有传入给定参数！"
                    GoTo errProc
                End If

                '注册插接协议类
                Dim objCreator As Xydc.Net.FtpRequestCreator = New Xydc.Net.FtpRequestCreator
                System.Net.WebRequest.RegisterPrefix("ftp:", objCreator)

                '创建FtpWebRequest
                objWebRequest = System.Net.WebRequest.Create(strUrl)
                objWebRequest.Method = "dir" '查找文件
                Try
                    objFtpWebRequest = CType(objWebRequest, Xydc.Net.FtpWebRequest)
                Catch
                    objFtpWebRequest = Nothing
                End Try
                objFtpWebRequest.Passive = Xydc.Platform.Common.jsoaConfiguration.FTPPassiveMode  'Passive
                objFtpWebRequest.ContentType = "binary"                                        'ascii

                '通过FTP代理访问FTP服务器
                If (strProxyUrl <> "") Then
                    Dim objWebProxy As System.Net.WebProxy = New System.Net.WebProxy(strProxyUrl)
                    If (strProxyUser <> "") Then
                        objWebProxy.Credentials = New System.Net.NetworkCredential(strProxyUser, strProxyPassword)
                    End If
                    objFtpWebRequest.Proxy = objWebProxy
                End If

                '直接访问
                If (strUser <> "") Then
                    objFtpWebRequest.Credentials = New System.Net.NetworkCredential(strUser, strPassword)
                End If

                '执行FTP指定操作并获取服务器的响应数据
                objWebResponse = objFtpWebRequest.GetResponse()
                objFtpWebResponse = CType(objWebResponse, Xydc.Net.FtpWebResponse)
                If objFtpWebResponse.Status = -1 Then
                    strErrMsg = objFtpWebResponse.StatusDescription
                    GoTo errProc
                End If
                If objFtpWebResponse.Status <> 550 Then
                    blnExisted = True
                End If
                'objFtpWebResponse.Status=550 表示文件不存在
            Catch ex As Exception

                'strErrMsg = ex.Message
                strErrMsg = "FTP出错!请联系管理员!"

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
        ' 备份FTP文件
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objFTPProperty       ：FTP服务器属性
        '     strFTPFile           ：FTP文件路径(相对根目录)
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                    strErrMsg = "错误：未传入FTP服务器参数！"
                    GoTo errProc
                End If

                '备份原文件
                Dim blnExisted As Boolean
                Dim strFileName As String
                Dim strUrl As String
                With objFTPProperty
                    strUrl = .getUrl(strFTPFile)
                    If Me.isFileExisted(strErrMsg, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword, blnExisted) = False Then
                        '可以不成功：可能是文件不存在
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
                strErrMsg = "FTP出错!请联系管理员!"

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
        ' 从备份中恢复文件
        '     strErrMsg              ：如果错误，则返回错误信息
        '     objFTPProperty         ：FTP连接参数
        '     strFTPFile             ：FTP文件路径(相对根目录)
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function doRestoreFile( _
            ByRef strErrMsg As String, _
            ByVal objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty, _
            ByVal strFTPFile As String) As Boolean

            Dim strBakExt As String = Xydc.Platform.Common.Utilities.PulicParameters.BACKUPFILEEXT

            doRestoreFile = False
            strErrMsg = ""

            Try
                '检查
                If strFTPFile Is Nothing Then strFTPFile = ""
                strFTPFile = strFTPFile.Trim
                If strFTPFile = "" Then
                    Exit Try
                End If
                If objFTPProperty Is Nothing Then
                    strErrMsg = "错误：未指定FTP服务器连接参数！"
                    GoTo errProc
                End If

                '备份
                Dim strOldFile As String = strFTPFile
                Dim blnExisted As Boolean
                Dim strToUrl As String
                Dim strUrl As String
                If strOldFile <> "" Then
                    With objFTPProperty
                        strUrl = .getUrl(strOldFile + strBakExt)
                        If Me.isFileExisted(strErrMsg, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword, blnExisted) = False Then
                            '可以不成功：可能是文件不存在
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
                strErrMsg = "FTP出错!请联系管理员!"

                GoTo errProc
            End Try

            doRestoreFile = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 删除备份文件
        '     strErrMsg              ：如果错误，则返回错误信息
        '     objFTPProperty         ：FTP连接参数
        '     strFTPFile             ：FTP文件路径(相对根目录)
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function doDeleteBackupFile( _
            ByRef strErrMsg As String, _
            ByVal objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty, _
            ByVal strFTPFile As String) As Boolean

            Dim strBakExt As String = Xydc.Platform.Common.Utilities.PulicParameters.BACKUPFILEEXT

            doDeleteBackupFile = False
            strErrMsg = ""

            Try
                '检查
                If strFTPFile Is Nothing Then strFTPFile = ""
                strFTPFile = strFTPFile.Trim
                If strFTPFile = "" Then
                    Exit Try
                End If
                If objFTPProperty Is Nothing Then
                    strErrMsg = "错误：未指定FTP服务器连接参数！"
                    GoTo errProc
                End If

                '删除备份
                Dim strOldFile As String = strFTPFile
                Dim strUrl As String
                If strOldFile <> "" Then
                    With objFTPProperty
                        strUrl = .getUrl(strOldFile + strBakExt)
                        If Me.doDeleteFile(strErrMsg, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword) = False Then
                            '可以不成功,形成垃圾数据
                        End If
                    End With
                End If
            Catch ex As Exception

                'strErrMsg = ex.Message
                strErrMsg = "FTP出错!请联系管理员!"

                GoTo errProc
            End Try

            doDeleteBackupFile = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 上传文件
        '     strErrMsg              ：如果错误，则返回错误信息
        '     objFTPProperty         ：FTP连接参数
        '     strLocalFile           ：准备上传的本地文件
        '     strFTPFile             ：目标FTP路径
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        ' 备注
        '     strFTPFile开始传入不带扩展名的路径，
        '     返回strFTPFile + strLocalFile的扩展名
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
                '检查
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
                    strErrMsg = "错误：未指定FTP服务器连接参数！"
                    GoTo errProc
                End If

                '获取最终FTP路径
                Dim strExt As String
                strExt = objBaseLocalFile.getExtension(strLocalFile)
                strFTPFile = strFTPFile + strExt

                '上传
                Dim strUrl As String
                With objFTPProperty
                    strUrl = .getUrl(strFTPFile)
                    If Me.doPutFile(strErrMsg, strLocalFile, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword) = False Then
                        GoTo errProc
                    End If
                End With
            Catch ex As Exception

                'strErrMsg = ex.Message
                strErrMsg = "FTP出错!请联系管理员!"

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
