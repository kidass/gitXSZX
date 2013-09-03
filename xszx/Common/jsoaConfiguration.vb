'----------------------------------------------------------------
' Copyright (C) 2006-2016 Josco Software Corporation
' All rights reserved.
'
' This source code is intended only as a supplement to Microsoft
' Development Tools and/or on-line documentation. See these other
' materials for detailed information regarding Microsoft code samples.
'
' THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY 
' OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT 
' LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR 
' FITNESS FOR A PARTICULAR PURPOSE.                   
'----------------------------------------------------------------
Option Strict On
Option Explicit On

Imports System
Imports System.Collections
Imports System.Configuration

Imports Xydc.Platform.SystemFramework
Imports System.Collections.Specialized

Namespace Xydc.Platform.Common

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.Common
    ' 类名    ：jsoaConfiguration
    '
    ' 功能描述： 
    '   This class handles jsoa-specific configuration settings in
    '     Config.Web.
    '
    ' Special considerations:
    '   The Jsoa application's configuaration settings are kept in 
    '   the jsoaConfiguration section of the Config.web file. A new
    '   instance of this class is created automatically whenever the
    '   settings file changes, so there is no need to cache any values.
    '----------------------------------------------------------------
    Public Class jsoaConfiguration
        Implements IConfigurationSectionHandler
        '
        ' Constant values for all expected entries in the jsoaConfiguration section
        '
        Private Const WEB_PAGECACHEEXPIRESINSECONDS As String = "Xydc.Web.PageCacheExpiresInSeconds"
        Private Const WEB_ENABLEPAGECACHE As String = "Xydc.Web.EnablePageCache"
        Private Const WEB_ENABLESSL As String = "Xydc.Web.EnableSsl"
        Private Const DATAACCESS_CONNECTIONSTRING As String = "Xydc.Web.DataAccess.ConnectionString"
        Private Const DATAACCESS_CONNECTIONTESTTIMEOUT As String = "Xydc.Web.DataAccess.ConnectionTestTimeOut"
        Private Const DATAACCESS_CONNECTIONTIMEOUT As String = "Xydc.Web.DataAccess.ConnectionTimeOut"
        Private Const DATAACCESS_COMMANDTIMEOUT As String = "Xydc.Web.DataAccess.CommandTimeOut"

        '
        ' Static member variables. These contain the application settings
        '   from Config.Web, or the default values.
        '
        Private Shared fieldPageCacheExpiresInSeconds As Integer
        Private Shared fieldEnablePageCache As Boolean
        Private Shared fieldEnableSsl As Boolean
        Private Shared fieldConnectionString As String
        Private Shared fieldConnectionTestTimeout As Integer
        Private Shared fieldConnectionTimeout As Integer
        Private Shared fieldCommandTimeout As Integer

        '
        ' Constant values for all of the default settings.
        '
        Private Const WEB_ENABLEPAGECACHE_DEFAULT As Boolean = True
        Private Const WEB_PAGECACHEEXPIRESINSECONDS_DEFAULT As Integer = 3600
        Private Const WEB_ENABLESSL_DEFAULT As Boolean = False
        Private Const DATAACCESS_CONNECTIONSTRING_DEFAULT As String = ""
        Private Const DATAACCESS_CONNECTIONTESTTIMEOUT_DEFAULT As Integer = 60
        Private Const DATAACCESS_CONNECTIONTIMEOUT_DEFAULT As Integer = 14400
        Private Const DATAACCESS_COMMANDTIMEOUT_DEFAULT As Integer = 14400

        '----------------------------------------------------------------
        ' Function Sub Create:
        '   Called by ASP+ before the application starts to initialize
        '     settings from the Config.Web file(s). The app domain will
        '     restart if these settings change, so there is no reason
        '     to read these values more than once. This function uses the
        '     DictionarySectionHandler base class to generate a hashtable
        '     from the XML, which is then used to store the current settings.
        '     Because all settings are read here, we do not actually store
        '     the generated hashtable object for later retrieval by
        '     Context.GetConfig. The application should use the accessor
        '     functions directly.
        ' Returns:
        '   A ConfigOutput object, which we leave empty because all settings
        '     are stored at this point.
        ' Parameters:
        '   [in] Parent: An object created by processing a section with this name
        '                in a Config.Web file in a parent directory.
        '   [in] ConfigInput: An array of Xml information.
        '   [in] ConfigFile: The Path of the Config.Web file relative to the root
        '                    of the web server.
        '----------------------------------------------------------------
        Public Function Create(ByVal parent As Object, ByVal configContext As Object, ByVal input As Xml.XmlNode) As Object Implements IConfigurationSectionHandler.Create

            Dim settings As NameValueCollection

            Try
                Dim baseHandler As NameValueSectionHandler
                baseHandler = New NameValueSectionHandler
                settings = CType(baseHandler.Create(parent, configContext, input), NameValueCollection)
            Catch
            End Try

            If settings Is Nothing Then
                fieldPageCacheExpiresInSeconds = WEB_PAGECACHEEXPIRESINSECONDS_DEFAULT
                fieldEnablePageCache = WEB_ENABLEPAGECACHE_DEFAULT
                fieldEnableSsl = WEB_ENABLESSL_DEFAULT

                fieldConnectionString = DATAACCESS_CONNECTIONSTRING_DEFAULT
                fieldConnectionTestTimeout = DATAACCESS_CONNECTIONTESTTIMEOUT_DEFAULT
                fieldConnectionTimeout = DATAACCESS_CONNECTIONTIMEOUT_DEFAULT
                fieldCommandTimeout = DATAACCESS_COMMANDTIMEOUT_DEFAULT
            Else
                fieldPageCacheExpiresInSeconds = ApplicationConfiguration.ReadSetting(settings, WEB_PAGECACHEEXPIRESINSECONDS, WEB_PAGECACHEEXPIRESINSECONDS_DEFAULT)
                fieldEnablePageCache = ApplicationConfiguration.ReadSetting(settings, WEB_ENABLEPAGECACHE, WEB_ENABLEPAGECACHE_DEFAULT)
                fieldEnableSsl = ApplicationConfiguration.ReadSetting(settings, WEB_ENABLESSL, WEB_ENABLESSL_DEFAULT)

                fieldConnectionString = ApplicationConfiguration.ReadSetting(settings, DATAACCESS_CONNECTIONSTRING, DATAACCESS_CONNECTIONSTRING_DEFAULT)
                fieldConnectionTestTimeout = ApplicationConfiguration.ReadSetting(settings, DATAACCESS_CONNECTIONTESTTIMEOUT, DATAACCESS_CONNECTIONTESTTIMEOUT_DEFAULT)
                fieldConnectionTimeout = ApplicationConfiguration.ReadSetting(settings, DATAACCESS_CONNECTIONTIMEOUT, DATAACCESS_CONNECTIONTIMEOUT_DEFAULT)
                fieldCommandTimeout = ApplicationConfiguration.ReadSetting(settings, DATAACCESS_COMMANDTIMEOUT, DATAACCESS_COMMANDTIMEOUT_DEFAULT)
            End If

        End Function








        '----------------------------------------------------------------
        ' 通用返回的Url
        '----------------------------------------------------------------
        Public Shared ReadOnly Property GeneralReturnUrl() As String

            Get
                Try
                    GeneralReturnUrl = System.Configuration.ConfigurationManager.AppSettings("GeneralReturnUrl")


                Catch ex As Exception
                    GeneralReturnUrl = ""
                End Try
                If GeneralReturnUrl Is Nothing Then GeneralReturnUrl = ""
            End Get

        End Property

        '----------------------------------------------------------------
        ' FTP Server Passive Mode
        '----------------------------------------------------------------
        Public Shared ReadOnly Property FTPPassiveMode() As Boolean

            Get
                Try
                    FTPPassiveMode = CType(System.Configuration.ConfigurationManager.AppSettings("FTPPassiveMode"), Boolean)
                Catch ex As Exception
                    FTPPassiveMode = False
                End Try
            End Get

        End Property

        '----------------------------------------------------------------
        ' FTPGetFileWaitTime
        '----------------------------------------------------------------
        Public Shared ReadOnly Property FTPGetFileWaitTime() As Integer

            Get
                Try
                    FTPGetFileWaitTime = CType(System.Configuration.ConfigurationManager.AppSettings("FTPGetFileWaitTime"), Integer)
                Catch ex As Exception
                    FTPGetFileWaitTime = 0
                End Try
            End Get

        End Property

        '----------------------------------------------------------------
        ' FTPPutFileWaitTime
        '----------------------------------------------------------------
        Public Shared ReadOnly Property FTPPutFileWaitTime() As Integer

            Get
                Try
                    FTPPutFileWaitTime = CType(System.Configuration.ConfigurationManager.AppSettings("FTPPutFileWaitTime"), Integer)
                Catch ex As Exception
                    FTPPutFileWaitTime = 0
                End Try
            End Get

        End Property

        '----------------------------------------------------------------
        ' DocumentEditorSwitch

        '----------------------------------------------------------------
        Public Shared ReadOnly Property DocumentEditorSwitch() As Boolean

            Get
                Try
                    DocumentEditorSwitch = CType(System.Configuration.ConfigurationManager.AppSettings("DocumentEditorSwitch"), Boolean)
                Catch ex As Exception
                    DocumentEditorSwitch = False
                End Try
            End Get

        End Property

        '----------------------------------------------------------------
        ' DocumentEditorWatchInterval

        '----------------------------------------------------------------
        Public Shared ReadOnly Property DocumentEditorWatchInterval() As Integer

            Get
                Try
                    DocumentEditorWatchInterval = CType(System.Configuration.ConfigurationManager.AppSettings("DocumentEditorWatchInterval"), Integer)
                Catch ex As Exception
                    DocumentEditorWatchInterval = 60
                End Try
            End Get

        End Property

        '----------------------------------------------------------------
        ' 授权使用单位
        '----------------------------------------------------------------
        Public Shared ReadOnly Property LicencingTo() As String

            Get
                Try
                    LicencingTo = System.Configuration.ConfigurationManager.AppSettings("LicencingTo")
                Catch ex As Exception
                    LicencingTo = ""
                End Try
                If LicencingTo Is Nothing Then LicencingTo = ""
            End Get

        End Property

        '----------------------------------------------------------------
        ' 顶级档案管理单位
        '----------------------------------------------------------------
        Public Shared ReadOnly Property TopDanganSSDW() As String

            Get
                Try
                    TopDanganSSDW = System.Configuration.ConfigurationManager.AppSettings("TopDanganSSDW")
                Catch ex As Exception
                    TopDanganSSDW = ""
                End Try
                If TopDanganSSDW Is Nothing Then TopDanganSSDW = ""
            End Get

        End Property

        '----------------------------------------------------------------
        ' 检查密码长度和强度(缺省False)
        '----------------------------------------------------------------
        Public Shared ReadOnly Property CheckPassword() As Boolean

            Get
                Try
                    CheckPassword = CType(System.Configuration.ConfigurationManager.AppSettings("CheckPassword"), Boolean)
                Catch ex As Exception
                    CheckPassword = False
                End Try
            End Get

        End Property

        '----------------------------------------------------------------
        ' 密码级别(缺省1)
        '----------------------------------------------------------------
        Public Shared ReadOnly Property PasswordLevel() As Integer

            Get
                Try
                    PasswordLevel = CType(System.Configuration.ConfigurationManager.AppSettings("PasswordLevel"), Integer)
                Catch ex As Exception
                    PasswordLevel = 1
                End Try
            End Get

        End Property

        '----------------------------------------------------------------
        ' 鉴别次数(缺省5)
        '----------------------------------------------------------------
        Public Shared ReadOnly Property LoginTryCount() As Integer

            Get
                Try
                    LoginTryCount = CType(System.Configuration.ConfigurationManager.AppSettings("LoginTryCount"), Integer)
                Catch ex As Exception
                    LoginTryCount = 5
                End Try
            End Get

        End Property

        '----------------------------------------------------------------
        ' 账户锁定时间(分钟)(缺省15)
        '----------------------------------------------------------------
        Public Shared ReadOnly Property DeadAccountLock() As Integer

            Get
                Try
                    DeadAccountLock = CType(System.Configuration.ConfigurationManager.AppSettings("DeadAccountLock"), Integer)
                Catch ex As Exception
                    DeadAccountLock = 15
                End Try
            End Get

        End Property

        '----------------------------------------------------------------
        ' 账户锁定数据文件(相对于应用根目录)
        '----------------------------------------------------------------
        Public Shared ReadOnly Property AccountLockDataFile() As String

            Get
                Try
                    AccountLockDataFile = System.Configuration.ConfigurationManager.AppSettings("AccountLockDataFile")
                Catch ex As Exception
                    AccountLockDataFile = ""
                End Try
                If AccountLockDataFile Is Nothing Then AccountLockDataFile = ""
            End Get

        End Property

        '----------------------------------------------------------------
        ' 密码最短长度(缺省10)
        '----------------------------------------------------------------
        Public Shared ReadOnly Property MinPasswordLength() As Integer

            Get
                Try
                    MinPasswordLength = CType(System.Configuration.ConfigurationManager.AppSettings("MinPasswordLength"), Integer)
                Catch ex As Exception
                    MinPasswordLength = 10
                End Try
            End Get

        End Property

        '----------------------------------------------------------------
        ' 上传文件大小限制(KB)(缺省20480)
        '----------------------------------------------------------------
        Public Shared ReadOnly Property MaxRequestLength() As Integer

            Get
                Try
                    If System.Configuration.ConfigurationManager.AppSettings("MaxRequestLength") Is Nothing Then
                        MaxRequestLength = 20480 'default=20MB
                    Else
                        MaxRequestLength = CType(System.Configuration.ConfigurationManager.AppSettings("MaxRequestLength"), Integer)
                    End If
                Catch ex As Exception
                    MaxRequestLength = 20480 'default=20MB
                End Try
            End Get

        End Property

        '----------------------------------------------------------------
        ' 从web.config的[jsoaConfiguration]中获取
        ' Jsoa.Web.EnablePageCache
        '----------------------------------------------------------------
        Public Shared ReadOnly Property EnablePageCache() As Boolean

            Get
                EnablePageCache = fieldEnablePageCache
            End Get

        End Property

        '----------------------------------------------------------------
        ' 从web.config的[jsoaConfiguration]中获取
        ' Jsoa.Web.PageCacheExpiresInSeconds
        '----------------------------------------------------------------
        Public Shared ReadOnly Property PageCacheExpiresInSeconds() As Integer

            Get
                PageCacheExpiresInSeconds = fieldPageCacheExpiresInSeconds
            End Get

        End Property

        '----------------------------------------------------------------
        ' 从web.config的[jsoaConfiguration]中获取
        ' Jsoa.Web.EnableSsl
        '----------------------------------------------------------------
        Public Shared ReadOnly Property EnableSsl() As Boolean

            Get
                EnableSsl = fieldEnableSsl
            End Get

        End Property

        '----------------------------------------------------------------
        ' 从web.config的[jsoaConfiguration]中获取
        ' Jsoa.Web.DataAccess.ConnectionString
        '----------------------------------------------------------------
        Public Shared ReadOnly Property ConnectionString() As String

            Get
                ConnectionString = fieldConnectionString
            End Get

        End Property

        '----------------------------------------------------------------
        ' 从web.config的[jsoaConfiguration]中获取
        ' Jsoa.Web.DataAccess.ConnectionTestTimeOut
        '----------------------------------------------------------------
        Public Shared ReadOnly Property ConnectionTestTimeout() As Integer

            Get
                ConnectionTestTimeout = fieldConnectionTestTimeout
            End Get

        End Property

        '----------------------------------------------------------------
        ' 从web.config的[jsoaConfiguration]中获取
        ' Jsoa.Web.DataAccess.ConnectionTimeOut
        '----------------------------------------------------------------
        Public Shared ReadOnly Property ConnectionTimeout() As Integer

            Get
                ConnectionTimeout = fieldConnectionTimeout
            End Get

        End Property

        '----------------------------------------------------------------
        ' 从web.config的[jsoaConfiguration]中获取
        ' Jsoa.Web.DataAccess.CommandTimeOut
        '----------------------------------------------------------------
        Public Shared ReadOnly Property CommandTimeout() As Integer

            Get
                CommandTimeout = fieldCommandTimeout
            End Get

        End Property

        '----------------------------------------------------------------
        ' 从web.config的[appSettings]中获取DatabaseServerName
        '----------------------------------------------------------------
        Public Shared ReadOnly Property DatabaseServerName() As String

            Get
                Try
                    DatabaseServerName = System.Configuration.ConfigurationManager.AppSettings("DatabaseServerName")
                Catch ex As Exception
                    DatabaseServerName = ""
                End Try
                If DatabaseServerName Is Nothing Then DatabaseServerName = ""
            End Get

        End Property

        '----------------------------------------------------------------
        ' 从web.config的[appSettings]中获取DatabaseServerType
        '----------------------------------------------------------------
        Public Shared ReadOnly Property DatabaseServerType() As String

            Get
                Try
                    DatabaseServerType = System.Configuration.ConfigurationManager.AppSettings("DatabaseServerType")
                Catch ex As Exception
                    DatabaseServerType = ""
                End Try
                If DatabaseServerType Is Nothing Then DatabaseServerType = ""
            End Get

        End Property

        '----------------------------------------------------------------
        ' 从web.config的[appSettings]中获取DatabaseServerMasterDB
        '----------------------------------------------------------------
        Public Shared ReadOnly Property DatabaseServerMasterDB() As String

            Get
                Try
                    DatabaseServerMasterDB = System.Configuration.ConfigurationManager.AppSettings("DatabaseServerMasterDB")
                Catch ex As Exception
                    DatabaseServerMasterDB = ""
                End Try
                If DatabaseServerMasterDB Is Nothing Then DatabaseServerMasterDB = ""
            End Get

        End Property

        '----------------------------------------------------------------
        ' 从web.config的[appSettings]中获取DatabaseServerMasterDB
        '----------------------------------------------------------------
        Public Shared ReadOnly Property DatabaseServerUserDB() As String

            Get
                Try
                    DatabaseServerUserDB = System.Configuration.ConfigurationManager.AppSettings("DatabaseServerUserDB")
                Catch ex As Exception
                    DatabaseServerUserDB = ""
                End Try
                If DatabaseServerUserDB Is Nothing Then DatabaseServerUserDB = ""
            End Get

        End Property

        '----------------------------------------------------------------
        ' 根据服务提供者、服务器、数据库、用户ID、用户密码合成连接字符串
        '----------------------------------------------------------------
        Public Shared Function getConnectionString( _
            ByVal strUserId As String, _
            ByVal strUserPwd As String, _
            Optional ByVal intConnectionTimeOut As Integer = -1, _
            Optional ByVal strDatabase As String = "", _
            Optional ByVal strServer As String = "", _
            Optional ByVal strProvider As String = "") As String

            Dim strConnectionString As String = ""

            '获取缺省数据库
            If strDatabase = "" Then
                strDatabase = DatabaseServerUserDB
            End If

            '获取缺省服务器
            If strServer = "" Then
                strServer = DatabaseServerName
            End If

            '获取缺省提供者
            If strProvider = "" Then
                strProvider = "SQLOLEDB"
            End If

            '获取缺省连接超时
            If intConnectionTimeOut = -1 Then
                intConnectionTimeOut = ConnectionTimeout
            End If

            '合成连接串
            strConnectionString += (" User ID=" + strUserId)
            strConnectionString += (";Password=" + strUserPwd)
            strConnectionString += (";Persist Security Info=" + "True")
            strConnectionString += (";Data Source=" + strServer)
            strConnectionString += (";Initial Catalog=" + strDatabase)
            strConnectionString += (";Connect Timeout=" + intConnectionTimeOut.ToString())

            getConnectionString = strConnectionString
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 管理员角色名称

        '----------------------------------------------------------------
        Public Shared ReadOnly Property Administrators() As String

            Get
                Try
                    Administrators = System.Configuration.ConfigurationManager.AppSettings("Administrators")
                Catch ex As Exception
                    Administrators = ""
                End Try
                If Administrators Is Nothing Then Administrators = ""
            End Get

        End Property

        '----------------------------------------------------------------
        '一般用户组 角色名称

        '----------------------------------------------------------------
        Public Shared ReadOnly Property Users() As String

            Get
                Try
                    Users = System.Configuration.ConfigurationManager.AppSettings("Users")
                Catch ex As Exception
                    Users = ""
                End Try
                If Users Is Nothing Then Users = ""
            End Get

        End Property

    End Class 'jsoaConfiguration

End Namespace 'Xydc.Platform.Common
