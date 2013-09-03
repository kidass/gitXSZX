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
Imports System.IO
Imports System.Xml
Imports System.Web
Imports System.Web.UI
Imports System.Security
Imports System.ComponentModel
Imports System.Data
Imports Microsoft.VisualBasic

Imports Xydc.Platform.Common
Imports Xydc.Platform.Common.Data
Imports Xydc.Platform.SystemFramework

Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.web
    ' 类名    ：PageBase
    '
    ' 功能描述： 
    '   　所有页面的父类
    '----------------------------------------------------------------
    Public Class PageBase
        Inherits System.Web.UI.Page

        '
        ' Exception Logging constant
        '
        Private Const UNHANDLED_EXCEPTION As String = "Unhandled Exception:"

        '
        ' Session Key Constants
        '
        Private Const KEY_CACHECUSTOMER_DATASET As String = "Cache:Customer:DataSet"
        Private Const KEY_CACHECUSTOMER_USERID As String = "Cache:Customer:UserId"
        Private Const KEY_CACHECUSTOMER_USERPWD As String = "Cache:Customer:UserPwd"
        Private Const KEY_CACHECUSTOMER_USERORGPWD As String = "Cache:Customer:UserOrgPwd"
        Private Const KEY_CACHECUSTOMER_ENTERTIME As String = "Cache:Customer:EnterTime"
        Private Const KEY_CACHECUSTOMER_APPLOCKED As String = "Cache:Customer:AppLocked"
        Private Const KEY_CACHECUSTOMER_FULLSCREEN As String = "Cache:Customer:FullScreen"
         Private Const KEY_CACHECUSTOMER_LASTSCANTIME_CHAT As String = "Cache:Customer:LastScanTime:Chat"
        Private Const KEY_CACHECUSTOMER_LASTSCANTIME_NOTICE As String = "Cache:Customer:LastScanTime:Notice"


        Private Shared ReadOnly Property UrlSuffix() As String

            Get
                UrlSuffix = HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath
            End Get

        End Property


        '----------------------------------------------------------------
        ' Property SecureUrlBase:
        '   Retrieves the Prefix for URLs in the Secure directory.
        '----------------------------------------------------------------
        Public Shared ReadOnly Property SecureUrlBase() As String

            Get
                If jsoaConfiguration.EnableSsl Then
                    SecureUrlBase = "https://"
                Else
                    SecureUrlBase = "http://"
                End If
                SecureUrlBase = SecureUrlBase + UrlSuffix
            End Get

        End Property

        '----------------------------------------------------------------
        ' Property UrlHost:
        '   Retrieves the Prefix for URLs.
        '----------------------------------------------------------------
        Public Shared ReadOnly Property UrlHost() As String

            Get
                UrlHost = "http://" + HttpContext.Current.Request.Url.Host
            End Get

        End Property

        '----------------------------------------------------------------
        ' Property UrlBase:
        '   Retrieves the Prefix for URLs.
        '----------------------------------------------------------------
        Public Shared ReadOnly Property UrlBase() As String

            Get
                UrlBase = "http://" + UrlSuffix
            End Get

        End Property

        '----------------------------------------------------------------
        ' 登录用户信息数据集
        '----------------------------------------------------------------
        Public Property Customer() As System.Data.DataSet

            Get
                Try
                    Customer = CType(Session.Item(KEY_CACHECUSTOMER_DATASET), System.Data.DataSet)
                Catch
                    Customer = Nothing
                End Try
            End Get

            Set(ByVal Value As System.Data.DataSet)
                If Value Is Nothing Then
                    Dim objDataSet As System.Data.DataSet = Nothing
                    Try
                        objDataSet = CType(Session.Item(KEY_CACHECUSTOMER_DATASET), System.Data.DataSet)
                    Catch
                        objDataSet = Nothing
                    End Try
                    Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                    Session.Remove(KEY_CACHECUSTOMER_DATASET)
                Else
                    Session.Item(KEY_CACHECUSTOMER_DATASET) = Value
                End If
            End Set

        End Property

        '----------------------------------------------------------------
        ' 登录用户的ID
        '----------------------------------------------------------------
        Public Property UserId() As String

            Get
                Try
                    UserId = CType(Session.Item(KEY_CACHECUSTOMER_USERID), String)
                Catch
                    UserId = ""
                End Try
                If UserId Is Nothing Then UserId = ""
            End Get

            Set(ByVal Value As String)
                If Value = "" Then
                    Session.Remove(KEY_CACHECUSTOMER_USERID)
                Else
                    Session.Item(KEY_CACHECUSTOMER_USERID) = Value
                End If
            End Set

        End Property

        '----------------------------------------------------------------
        ' 登录用户的密码(原始密码)
        '----------------------------------------------------------------
        Public Property UserOrgPassword() As String

            Get
                Try
                    UserOrgPassword = CType(Session.Item(KEY_CACHECUSTOMER_USERORGPWD), String)
                Catch
                    UserOrgPassword = ""
                End Try
                If UserOrgPassword Is Nothing Then UserOrgPassword = ""
            End Get

            Set(ByVal Value As String)
                If Value = "" Then
                    Session.Remove(KEY_CACHECUSTOMER_USERORGPWD)
                Else
                    Session.Item(KEY_CACHECUSTOMER_USERORGPWD) = Value
                End If
            End Set

        End Property

        '----------------------------------------------------------------
        ' 登录用户的密码(验证后的密码)
        '----------------------------------------------------------------
        Public Property UserPassword() As String

            Get
                Try
                    UserPassword = CType(Session.Item(KEY_CACHECUSTOMER_USERPWD), String)
                Catch
                    UserPassword = ""
                End Try
                If UserPassword Is Nothing Then UserPassword = ""
            End Get

            Set(ByVal Value As String)
                If Value = "" Then
                    Session.Remove(KEY_CACHECUSTOMER_USERPWD)
                Else
                    Session.Item(KEY_CACHECUSTOMER_USERPWD) = Value
                End If
            End Set

        End Property

        '----------------------------------------------------------------
        ' 登录用户的进入时间
        '----------------------------------------------------------------
        Public Property UserEnterTime() As String

            Get
                Try
                    UserEnterTime = CType(Session.Item(KEY_CACHECUSTOMER_ENTERTIME), String)
                Catch
                    UserEnterTime = ""
                End Try
                If UserEnterTime Is Nothing Then UserEnterTime = ""
            End Get

            Set(ByVal Value As String)
                If Value = "" Then
                    Session.Remove(KEY_CACHECUSTOMER_ENTERTIME)
                Else
                    Session.Item(KEY_CACHECUSTOMER_ENTERTIME) = Value
                End If
            End Set

        End Property

        '----------------------------------------------------------------
        ' 登录用户的通知信息定时检查器上次检查时间
        '----------------------------------------------------------------
        Public Property LastScanTime_Notice() As String

            Get
                Try
                    LastScanTime_Notice = CType(Session.Item(KEY_CACHECUSTOMER_LASTSCANTIME_NOTICE), String)
                Catch
                    LastScanTime_Notice = ""
                End Try
                If LastScanTime_Notice Is Nothing Then LastScanTime_Notice = ""
            End Get

            Set(ByVal Value As String)
                If Value = "" Then
                    Session.Remove(KEY_CACHECUSTOMER_LASTSCANTIME_NOTICE)
                Else
                    Session.Item(KEY_CACHECUSTOMER_LASTSCANTIME_NOTICE) = Value
                End If
            End Set

        End Property

        '----------------------------------------------------------------
        ' 登录用户的即时交流定时检查器上次检查时间
        '----------------------------------------------------------------
        Public Property LastScanTime_Chat() As String

            Get
                Try
                    LastScanTime_Chat = CType(Session.Item(KEY_CACHECUSTOMER_LASTSCANTIME_CHAT), String)
                Catch
                    LastScanTime_Chat = ""
                End Try
                If LastScanTime_Chat Is Nothing Then LastScanTime_Chat = ""
            End Get

            Set(ByVal Value As String)
                If Value = "" Then
                    Session.Remove(KEY_CACHECUSTOMER_LASTSCANTIME_CHAT)
                Else
                    Session.Item(KEY_CACHECUSTOMER_LASTSCANTIME_CHAT) = Value
                End If
            End Set

        End Property

        '----------------------------------------------------------------
        ' 登录用户名称
        '----------------------------------------------------------------
        Public ReadOnly Property UserXM() As String

            Get
                Try
                    If Not (Me.Customer Is Nothing) Then
                        If Not (Me.Customer.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN) Is Nothing) Then
                            With Me.Customer.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN)
                                If .Rows.Count > 0 Then
                                    UserXM = CType(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYMC), String)
                                Else
                                    UserXM = ""
                                End If
                            End With
                        End If
                    End If
                Catch
                    UserXM = ""
                End Try
                If UserXM Is Nothing Then UserXM = ""
                UserXM = UserXM.Trim
            End Get

        End Property

        '----------------------------------------------------------------
        ' 登录用户单位代码
        '----------------------------------------------------------------
        Public ReadOnly Property UserBmdm() As String

            Get
                Try
                    If Not (Me.Customer Is Nothing) Then
                        If Not (Me.Customer.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN) Is Nothing) Then
                            With Me.Customer.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN)
                                If .Rows.Count > 0 Then
                                    UserBmdm = CType(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_ZZDM), String)
                                Else
                                    UserBmdm = ""
                                End If
                            End With
                        End If
                    End If
                Catch
                    UserBmdm = ""
                End Try
                If UserBmdm Is Nothing Then UserBmdm = ""
                UserBmdm = UserBmdm.Trim
            End Get

        End Property

        '----------------------------------------------------------------
        ' 登录用户单位名称
        '----------------------------------------------------------------
        Public ReadOnly Property UserBmmc() As String

            Get
                Try
                    If Not (Me.Customer Is Nothing) Then
                        If Not (Me.Customer.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN) Is Nothing) Then
                            With Me.Customer.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN)
                                If .Rows.Count > 0 Then
                                    UserBmmc = CType(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_ZZMC), String)
                                Else
                                    UserBmmc = ""
                                End If
                            End With
                        End If
                    End If
                Catch
                    UserBmmc = ""
                End Try
                If UserBmmc Is Nothing Then UserBmmc = ""
                UserBmmc = UserBmmc.Trim
            End Get

        End Property


        '----------------------------------------------------------------
        ' 用户是否将应用锁定？
        '----------------------------------------------------------------
        Public Property AppLocked() As Boolean

            Get
                Try
                    AppLocked = CType(Session.Item(KEY_CACHECUSTOMER_APPLOCKED), Boolean)
                Catch
                    AppLocked = False
                End Try
            End Get

            Set(ByVal Value As Boolean)
                If Value = False Then
                    Session.Remove(KEY_CACHECUSTOMER_APPLOCKED)
                Else
                    Session.Item(KEY_CACHECUSTOMER_APPLOCKED) = Value
                End If
            End Set

        End Property
       
        '----------------------------------------------------------------
        ' 窗口的全屏或正常显示状态
        '----------------------------------------------------------------
        Public Property FullScreen() As Boolean

            Get
                Try
                    FullScreen = CType(Session.Item(KEY_CACHECUSTOMER_FULLSCREEN), Boolean)
                Catch
                    FullScreen = False
                End Try
            End Get

            Set(ByVal Value As Boolean)
                If Value = False Then
                    Session.Remove(KEY_CACHECUSTOMER_FULLSCREEN)
                Else
                    Session.Item(KEY_CACHECUSTOMER_FULLSCREEN) = Value
                End If
            End Set

        End Property










        '----------------------------------------------------------------
        ' Sub Page_Error:
        '   Handles errors that may be encountered when displaying this page.
        '----------------------------------------------------------------
        Protected Overrides Sub OnError(ByVal e As EventArgs)

            'ApplicationLog.WriteError(ApplicationLog.FormatException(Server.GetLastError(), UNHANDLED_EXCEPTION))
            MyBase.OnError(e)

        End Sub

        '----------------------------------------------------------------
        '检查密码长度，如果不满足要求，则强制到修改密码Url
        '返回：
        '    True  - 不再继续执行当前页面程序
        '    False - 继续执行当前页面程序
        '----------------------------------------------------------------
        Public Function doCheckPassword() As Boolean

            Dim strUrl As String

            doCheckPassword = False
            Try
                If Xydc.Platform.Common.jsoaConfiguration.CheckPassword = True Then
                    'If Me.UserOrgPassword.Length < Xydc.Platform.Common.jsoaConfiguration.MinPasswordLength Then
                    '    strUrl = Me.UrlBase + "/secure/modifypwd.aspx"
                    '    doCheckPassword = True
                    '    Response.Redirect(strUrl)
                    'End If
                    If Me.doValidPassword(Me.UserOrgPassword) = False Then
                        strUrl = Me.UrlBase + "/secure/modifypwd.aspx"
                        doCheckPassword = True
                        Response.Redirect(strUrl)
                    End If
                End If
            Catch ex As Exception
            End Try

            Exit Function

        End Function

        '----------------------------------------------------------------
        '检查密码长度是否符合长度和强度要求
        '输入：
        '    strPassword：要检查的密码
        '返回：
        '    True  - 符合
        '    False - 不符合
        '修改记录：
        '----------------------------------------------------------------
        Public Function doValidPassword(ByVal strPassword As String) As Boolean

            Dim intLevel As Integer = 0

            doValidPassword = False
            Try
                strPassword = Me.UserOrgPassword

                If Xydc.Platform.Common.jsoaConfiguration.CheckPassword = True Then
                    If strPassword.Length < Xydc.Platform.Common.jsoaConfiguration.MinPasswordLength Then
                        '不符合长度要求！
                        Exit Function
                    End If

                    '密码强度检查
                    Dim blnFoundSign As Boolean = False
                    Dim blnFoundLCap As Boolean = False
                    Dim blnFoundUCap As Boolean = False
                    Dim blnFoundNum As Boolean = False
                    Dim objBytes() As Char

                    objBytes = strPassword.ToCharArray()
                    Dim intCount As Integer
                    Dim i As Integer
                    intCount = objBytes.Length
                    For i = 0 To intCount - 1 Step 1
                        If Char.IsDigit(objBytes(i)) = True Then
                            blnFoundNum = True
                        End If
                        If Char.IsLetter(objBytes(i)) = True And Char.IsLower(objBytes(i)) = True Then
                            blnFoundLCap = True
                        End If
                        If Char.IsLetter(objBytes(i)) = True And Char.IsUpper(objBytes(i)) = True Then
                            blnFoundUCap = True
                        End If
                        If Char.IsPunctuation(objBytes(i)) = True Then
                            blnFoundSign = True
                        End If
                    Next
                    If blnFoundNum = True Then
                        intLevel += 1
                    End If
                    If blnFoundLCap = True Then
                        intLevel += 1
                    End If
                    If blnFoundUCap = True Then
                        intLevel += 1
                    End If
                    If blnFoundSign = True Then
                        intLevel += 1
                    End If
                    If intLevel < Xydc.Platform.Common.jsoaConfiguration.PasswordLevel Then
                        '不符合强度要求！
                        Exit Function
                    End If
                End If
            Catch ex As Exception
                Exit Function
            End Try

            doValidPassword = True
            Exit Function

        End Function

        '----------------------------------------------------------------
        '将用户锁定记录从DataSet写入到XML文件中
        '    strErrMsg   ：返回错误信息
        '    objDataSet  ：要写的数据集
        '    strXmlFile  ：待写入的XML文件本地完成路径
        '返回：
        '    True        ：成功
        '    False       ：失败
        '----------------------------------------------------------------
        Private Function doWriteXml( _
            ByRef strErrMsg As String, _
            ByVal objDataSet As System.Data.DataSet, _
            ByVal strXmlFile As String) As Boolean

            doWriteXml = False
            strErrMsg = ""

            Try
                '检查
                If objDataSet Is Nothing Then
                    Exit Try
                End If
                If strXmlFile Is Nothing Then strXmlFile = ""
                strXmlFile = strXmlFile.Trim
                If strXmlFile = "" Then
                    Exit Try
                End If

                '保存
                objDataSet.WriteXml(strXmlFile, System.Data.XmlWriteMode.WriteSchema)

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doWriteXml = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        '锁定用户
        '    strErrMsg   ：返回错误信息
        '    strUserId   ：用户标识
        '返回：
        '    True        ：成功
        '    False       ：失败
        '----------------------------------------------------------------
        Public Function doLockAccount( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String) As Boolean

            Dim strXmlFile As String = Xydc.Platform.Common.jsoaConfiguration.AccountLockDataFile
            Dim strField_LockTime As String = "locktime"
            Dim strField_Valid As String = "valid"
            Dim strField_Name As String = "name"

            Dim objDataSet As System.Data.DataSet

            doLockAccount = False
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    Exit Try
                End If
                strXmlFile = Server.MapPath(Request.ApplicationPath + strXmlFile)

                '获取用户锁定数据
                objDataSet = New System.Data.DataSet
                objDataSet.ReadXmlSchema(strXmlFile)
                objDataSet.ReadXml(strXmlFile)

                '存在？
                Dim strFilter As String = strField_Name + " = '" + strUserId + "'"
                Dim blnFound As Boolean = False
                With objDataSet.Tables(0)
                    .DefaultView.RowFilter = strFilter
                    If .DefaultView.Count > 0 Then
                        blnFound = True
                    Else
                        .DefaultView.RowFilter = ""
                    End If
                End With

                '锁定
                Dim objDataRow As System.Data.DataRow
                If blnFound = False Then
                    With objDataSet.Tables(0)
                        objDataRow = .NewRow

                        objDataRow.Item(strField_Name) = strUserId
                        objDataRow.Item(strField_LockTime) = Now.ToString("yyyy-MM-dd HH:mm:ss")
                        objDataRow.Item(strField_Valid) = CType(1, Integer)

                        .Rows.Add(objDataRow)
                    End With
                Else
                    With objDataSet.Tables(0)
                        objDataRow = .DefaultView.Item(0).Row

                        objDataRow.Item(strField_Name) = strUserId
                        objDataRow.Item(strField_LockTime) = Now.ToString("yyyy-MM-dd HH:mm:ss")
                        objDataRow.Item(strField_Valid) = CType(1, Integer)

                        .DefaultView.RowFilter = ""
                    End With
                End If

                '保存
                If Me.doWriteXml(strErrMsg, objDataSet, strXmlFile) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)

            doLockAccount = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Exit Function

        End Function

        '----------------------------------------------------------------
        '解锁用户
        '    strErrMsg   ：返回错误信息
        '    strUserId   ：用户标识
        '返回：
        '    True        ：成功
        '    False       ：失败
        '----------------------------------------------------------------
        Public Function doUnlockAccount( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String) As Boolean

            Dim strXmlFile As String = Xydc.Platform.Common.jsoaConfiguration.AccountLockDataFile
            Dim strField_LockTime As String = "locktime"
            Dim strField_Valid As String = "valid"
            Dim strField_Name As String = "name"

            Dim objDataSet As System.Data.DataSet

            doUnlockAccount = False
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    Exit Try
                End If
                strXmlFile = Server.MapPath(Request.ApplicationPath + strXmlFile)

                '获取用户锁定数据
                objDataSet = New System.Data.DataSet
                objDataSet.ReadXmlSchema(strXmlFile)
                objDataSet.ReadXml(strXmlFile)

                '失效锁定
                Dim blnChanged As Boolean = False
                Dim intCount As Integer
                Dim i As Integer
                With objDataSet.Tables(0)
                    .DefaultView.RowFilter = strField_Name + " = '" + strUserId + "'"
                    intCount = .DefaultView.Count
                    For i = 0 To intCount - 1 Step 1
                        .DefaultView.Item(i).Item(strField_Valid) = CType(0, Integer)
                        blnChanged = True
                    Next
                    .DefaultView.RowFilter = ""
                End With

                '保存
                If blnChanged = True Then
                    If Me.doWriteXml(strErrMsg, objDataSet, strXmlFile) = False Then
                        GoTo errProc
                    End If
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)

            doUnlockAccount = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Exit Function

        End Function

        '----------------------------------------------------------------
        '判断用户是否被锁定？
        '    strErrMsg   ：返回错误信息
        '    strUserId   ：用户标识
        '    blnLocked   ：返回True/False
        '    strLockTime ：返回开始锁定时间
        '返回：
        '    True        ：成功
        '    False       ：失败
        '----------------------------------------------------------------
        Public Function isAccountLocked( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByRef blnLocked As Boolean, _
            ByRef strLockTime As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objDataSet As System.Data.DataSet

            Dim strXmlFile As String = Xydc.Platform.Common.jsoaConfiguration.AccountLockDataFile
            Dim strField_LockTime As String = "locktime"
            Dim strField_Valid As String = "valid"
            Dim strField_Name As String = "name"

            isAccountLocked = False
            blnLocked = False
            strLockTime = ""
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    Exit Try
                End If
                strXmlFile = Server.MapPath(Request.ApplicationPath + strXmlFile)

                '获取用户锁定数据
                objDataSet = New System.Data.DataSet
                objDataSet.ReadXmlSchema(strXmlFile)
                objDataSet.ReadXml(strXmlFile)

                '检索数据
                Dim strFilter As String
                strFilter = strField_Name + " = '" + strUserId + "' and " + strField_Valid + " = 1"
                objDataSet.Tables(0).DefaultView.RowFilter = strFilter

                '返回
                Dim strTime As String
                If objDataSet.Tables(0).DefaultView.Count > 0 Then
                    '是否超过锁定时间？
                    With objDataSet.Tables(0).DefaultView
                        strTime = objPulicParameters.getObjectValue(.Item(0).Item(strField_LockTime), "")
                    End With
                    If objPulicParameters.isDatetimeString(strTime) = True Then
                        Dim objTime As System.DateTime
                        objTime = CType(strTime, System.DateTime)
                        objTime = objTime.AddMinutes(Xydc.Platform.Common.jsoaConfiguration.DeadAccountLock)
                        If objTime > Now Then
                            '仍处于锁定
                            strLockTime = strTime
                            blnLocked = True
                            Exit Try
                        End If
                    End If

                    '解除锁定(设置为无效valid=0)
                    If Me.doUnlockAccount(strErrMsg, strUserId) = False Then
                        GoTo errProc
                    End If
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)

            isAccountLocked = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Exit Function

        End Function

        'Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        '    ''不进行本地缓存
        '    Try
        '        'set cache-control = no-cache
        '        Response.CacheControl = "No-Cache"
        '        'set Pragma = no-cache
        '        Response.AddHeader("Pragma", "No-Cache")
        '        'set Expires = -1
        '        Response.Expires = -1
        '    Catch ex As Exception
        '    End Try

        'End Sub


        '----------------------------------------------------------------
        ' 登录用户真名
        '----------------------------------------------------------------
        Public ReadOnly Property UserZM() As String

            Get
                Try
                    If Not (Me.Customer Is Nothing) Then
                        If Not (Me.Customer.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN) Is Nothing) Then
                            With Me.Customer.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN)
                                If .Rows.Count > 0 Then
                                    UserZM = CType(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYZM), String)
                                Else
                                    UserZM = ""
                                End If
                            End With
                        End If
                    End If
                Catch
                    UserZM = ""
                End Try
                If UserZM Is Nothing Then UserZM = ""
                If UserZM = "" Then UserZM = UserXM
                UserZM = UserZM.Trim
            End Get

        End Property
       
        '----------------------------------------------------------------
        'Page预处理-执行登录检查和密码检测等安全域的基本处理
        '输入：
        '    blnCheckPassword - True检查密码,False不检查密码
        '    blnSaveAccessLog - True记录访问日志,False不记录访问日志
        '返回：
        '    True  - 不再继续执行当前页面程序
        '    False - 继续执行当前页面程序
        '----------------------------------------------------------------
        Public Function doPagePreprocess( _
            ByVal blnCheckPassword As Boolean, _
            ByVal blnSaveAccessLog As Boolean) As Boolean

            Dim strUrl As String

            doPagePreprocess = False
            Try
                '检查登录凭证？

                If Me.Customer Is Nothing Then
                    '没有登录，则定向到登录页面


                    '清除凭证 - 强制要求重新登录！
                    System.Web.Security.FormsAuthentication.SignOut()
                    Me.Customer = Nothing
                    Me.UserId = ""
                    Me.UserPassword = ""

                    '重新访问本页
                    strUrl = Request.Url.PathAndQuery
                    doPagePreprocess = True
                    Response.Redirect(strUrl)
                    Exit Function
                Else
                    '已登录，根据需要验证密码要求！
                    If blnCheckPassword = True Then
                        If Xydc.Platform.Common.jsoaConfiguration.CheckPassword = True Then

                            'If Me.UserOrgPassword.Length < Xydc.Platform.Common.jsoaConfiguration.MinPasswordLength Then
                            '    strUrl = Me.UrlBase + "/secure/modifypwd.aspx"
                            '    doPagePreprocess = True
                            '    Response.Redirect(strUrl)
                            '    Exit Function
                            'End If
                            If Me.doValidPassword(Me.UserOrgPassword) = False Then
                                strUrl = Me.UrlBase + "/secure/modifypwd.aspx"
                                doPagePreprocess = True
                                Response.Redirect(strUrl)
                                Exit Function
                            End If

                        End If
                    End If

                    '是否记录访问日志
                    If blnSaveAccessLog = True Then
                        Xydc.Platform.SystemFramework.ApplicationLog.WriteInfo(Request.UserHostAddress, Request.UserHostName, "[" + Me.UserId + "]访问了[" + Request.Url.AbsoluteUri + "]！")
                    End If
                End If
            Catch ex As Exception
                '忽略错误！
            End Try

            Exit Function

        End Function


    End Class

End Namespace
