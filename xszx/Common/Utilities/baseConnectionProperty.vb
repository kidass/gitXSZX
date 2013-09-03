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
Imports System.Data
Imports System.Runtime.Serialization

Namespace Xydc.Platform.Common.Utilities

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.Common.Utilities
    ' 类名    ：ConnectionProperty
    '
    ' 功能描述： 
    '     定义数据库连接字符串相关的参数
    '----------------------------------------------------------------
    Public Class ConnectionProperty
        Implements IDisposable

        Private Const m_cstrPersistSecurityInfo As String = "PersistSecurityInfo"
        Private Const m_cstrIntegratedSecurity As String = "IntegratedSecurity"
        Private Const m_cstrConnectTimeout As String = "ConnectTimeout"
        Private Const m_cstrInitialCatalog As String = "InitialCatalog"
        Private Const m_cstrDataSource As String = "DataSource"
        Private Const m_cstrPassword As String = "Password"
        Private Const m_cstrUserID As String = "UserID"

        Private m_strPersistSecurityInfo As String        'Persist Security Info
        Private m_strIntegratedSecurity As String         'Integrated Security
        Private m_strConnectTimeout As String             'Connect Timeout
        Private m_strInitialCatalog As String             'Initial Catalog
        Private m_strDataSource As String                 'Data Source
        Private m_strPassword As String                   'Password
        Private m_strUserID As String                     'User ID







        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
            m_strDataSource = ""
            m_strInitialCatalog = ""
            m_strUserID = ""
            m_strPassword = ""
            m_strPersistSecurityInfo = ""
            m_strConnectTimeout = ""
            m_strIntegratedSecurity = ""
        End Sub

        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New(ByVal strConnectionString As String)

            '初始化
            MyBase.New()
            m_strDataSource = ""
            m_strInitialCatalog = ""
            m_strUserID = ""
            m_strPassword = ""
            m_strPersistSecurityInfo = ""
            m_strConnectTimeout = ""
            m_strIntegratedSecurity = ""

            '从连接字符串中获取相关信息
            Try
                If strConnectionString Is Nothing Then strConnectionString = ""
                strConnectionString = strConnectionString.Trim()

                Dim strParamsB() As String
                Dim strParamsA() As String
                Dim intCountA As Integer
                Dim i As Integer
                strParamsA = strConnectionString.Split(";".ToCharArray())
                intCountA = strParamsA.Length
                For i = 0 To intCountA - 1 Step 1
                    strParamsA(i) = strParamsA(i).Trim()

                    strParamsB = strParamsA(i).Split("=".ToCharArray())
                    If strParamsB.Length = 2 Then
                        strParamsB(0) = strParamsB(0).Trim()
                        strParamsB(0) = strParamsB(0).Replace(" ", "")
                        strParamsB(1) = strParamsB(1).Trim()

                        Select Case strParamsB(0).ToUpper()
                            Case Me.m_cstrDataSource.ToUpper()
                                Me.m_strDataSource = strParamsB(1)

                            Case Me.m_cstrInitialCatalog.ToUpper()
                                Me.m_strInitialCatalog = strParamsB(1)

                            Case Me.m_cstrUserID.ToUpper()
                                Me.m_strUserID = strParamsB(1)

                            Case Me.m_cstrPassword.ToUpper()
                                Me.m_strPassword = strParamsB(1)

                            Case Me.m_cstrPersistSecurityInfo.ToUpper()
                                Me.m_strPersistSecurityInfo = strParamsB(1)

                            Case Me.m_cstrIntegratedSecurity.ToUpper()
                                Me.m_strIntegratedSecurity = strParamsB(1)

                            Case Me.m_cstrConnectTimeout.ToUpper()
                                Me.m_strConnectTimeout = strParamsB(1)

                            Case Else
                        End Select
                    End If
                Next
            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' 虚拟析构函数
        '----------------------------------------------------------------
        Public Sub Dispose() Implements System.IDisposable.Dispose
            Dispose(True)
        End Sub

        '----------------------------------------------------------------
        ' 释放本身资源
        '----------------------------------------------------------------
        Protected Sub Dispose(ByVal disposing As Boolean)
            If (Not disposing) Then
                Exit Sub
            End If
        End Sub

        '----------------------------------------------------------------
        ' 安全释放本身资源
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.Common.Utilities.ConnectionProperty)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub










        '----------------------------------------------------------------
        ' DataSource属性
        '----------------------------------------------------------------
        Public Property DataSource() As String
            Get
                DataSource = m_strDataSource
            End Get
            Set(ByVal Value As String)
                Try
                    m_strDataSource = Value
                Catch ex As Exception
                    m_strDataSource = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' InitialCatalog属性
        '----------------------------------------------------------------
        Public Property InitialCatalog() As String
            Get
                InitialCatalog = m_strInitialCatalog
            End Get
            Set(ByVal Value As String)
                Try
                    m_strInitialCatalog = Value
                Catch ex As Exception
                    m_strInitialCatalog = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' UserID属性
        '----------------------------------------------------------------
        Public Property UserID() As String
            Get
                UserID = m_strUserID
            End Get
            Set(ByVal Value As String)
                Try
                    m_strUserID = Value
                Catch ex As Exception
                    m_strUserID = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' Password属性
        '----------------------------------------------------------------
        Public Property Password() As String
            Get
                Password = m_strPassword
            End Get
            Set(ByVal Value As String)
                Try
                    m_strPassword = Value
                Catch ex As Exception
                    m_strPassword = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' PersistSecurityInfo属性
        '----------------------------------------------------------------
        Public Property PersistSecurityInfo() As String
            Get
                PersistSecurityInfo = m_strPersistSecurityInfo
            End Get
            Set(ByVal Value As String)
                Try
                    m_strPersistSecurityInfo = Value
                Catch ex As Exception
                    m_strPersistSecurityInfo = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' ConnectTimeout属性
        '----------------------------------------------------------------
        Public Property ConnectTimeout() As String
            Get
                ConnectTimeout = m_strConnectTimeout
            End Get
            Set(ByVal Value As String)
                Try
                    m_strConnectTimeout = Value
                Catch ex As Exception
                    m_strConnectTimeout = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' IntegratedSecurity属性
        '----------------------------------------------------------------
        Public Property IntegratedSecurity() As String
            Get
                IntegratedSecurity = m_strIntegratedSecurity
            End Get
            Set(ByVal Value As String)
                Try
                    m_strIntegratedSecurity = Value
                Catch ex As Exception
                    m_strIntegratedSecurity = ""
                End Try
            End Set
        End Property

    End Class

End Namespace