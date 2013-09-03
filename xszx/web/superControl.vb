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
Imports System.Web
Imports System.Web.UI
Imports System.ComponentModel
Imports System.Data
Imports Microsoft.VisualBasic

Imports Xydc.Platform.Common
Imports Xydc.Platform.Common.Data
Imports Xydc.Platform.SystemFramework

Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.web
    ' 类名    ：ControlBase
    '
    ' 功能描述： 
    '   　用户控件的父类
    '----------------------------------------------------------------
    Public Class ControlBase
        Inherits UserControl

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

        Private m_strBasePathPrefix As String


        '----------------------------------------------------------------
        ' Property PathPrefix:
        '   The file path prefix to be used by the control.
        '----------------------------------------------------------------
        <Browsable(False)> Public Property PathPrefix() As String

            Get
                If m_strBasePathPrefix Is Nothing And Not HttpContext.Current Is Nothing Then
                    m_strBasePathPrefix = PageBase.UrlBase
                End If

                PathPrefix = m_strBasePathPrefix
            End Get

            Set(ByVal Value As String)
                m_strBasePathPrefix = Value
            End Set

        End Property

        '----------------------------------------------------------------
        ' 登录用户信息数据集
        '----------------------------------------------------------------
        Public Property Customer() As System.Data.DataSet

            Get
                Try
                    Customer = CType(Session.Item(KEY_CACHECUSTOMER_DATASET), System.Data.DataSet)
                Catch
                    Customer = Nothing 'For design time
                End Try
            End Get

            Set(ByVal Value As System.Data.DataSet)
                If Value Is Nothing Then
                    Dim objDataSet As System.Data.DataSet
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
                UserId = UserId.Trim
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


    End Class

End Namespace
