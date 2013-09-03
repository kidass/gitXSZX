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
    ' �����ռ䣺Xydc.Platform.Common.Utilities
    ' ����    ��FTPProperty
    '
    ' ���������� 
    '     ����FTP��������صĲ���
    '----------------------------------------------------------------
    Public Class FTPProperty
        Implements IDisposable

        Public Const DEFAULT_PREFIX As String = "ftp:"    'ȱʡǰ׺
        Public Const DEFAULT_ROOTSEP As String = "//"     'Э��ָ���
        Public Const DEFAULT_DIRSEP As String = "/"       'Ŀ¼�ָ���
        Public Const DEFAULT_PORTSEP As String = ":"      '�˿ڷָ���
        Public Const DEFAULT_PORT As Integer = 21         'ȱʡ�˿�

        Private m_strServerName As String                 '��������
        Private m_intPort As Integer                      '���Ӷ˿�
        Private m_strUserID As String                     '�����û�
        Private m_strPassword As String                   '�û�����
        Private m_strProxyUrl As String                   '����Url
        Private m_strProxyUserID As String                '���������û�
        Private m_strProxyPassword As String              '�����û�����








        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()

            MyBase.New()

            m_strServerName = ""
            m_intPort = DEFAULT_PORT
            m_strUserID = ""
            m_strPassword = ""
            m_strProxyUrl = ""
            m_strProxyUserID = ""
            m_strProxyPassword = ""

        End Sub

        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New( _
            ByVal strServer As String, _
            ByVal intPort As Integer, _
            ByVal strUserId As String, _
            ByVal strPassword As String)

            Me.New()

            m_strServerName = strServer
            m_intPort = intPort
            m_strUserID = strUserId
            m_strPassword = strPassword

        End Sub

        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New( _
            ByVal strServer As String, _
            ByVal intPort As Integer, _
            ByVal strProxyUrl As String, _
            ByVal strProxyUserId As String, _
            ByVal strProxyPassword As String)

            Me.New()

            m_strServerName = strServer
            m_intPort = intPort
            m_strProxyUrl = strProxyUrl
            m_strProxyUserID = strProxyUserId
            m_strProxyPassword = strProxyPassword

        End Sub

        '----------------------------------------------------------------
        ' ������������
        '----------------------------------------------------------------
        Public Sub Dispose() Implements System.IDisposable.Dispose
            Dispose(True)
        End Sub

        '----------------------------------------------------------------
        ' �ͷű�����Դ
        '----------------------------------------------------------------
        Protected Sub Dispose(ByVal disposing As Boolean)
            If (Not disposing) Then
                Exit Sub
            End If
        End Sub

        '----------------------------------------------------------------
        ' ��ȫ�ͷű�����Դ
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.Common.Utilities.FTPProperty)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub











        '----------------------------------------------------------------
        ' ServerName����
        '----------------------------------------------------------------
        Public Property ServerName() As String
            Get
                ServerName = m_strServerName
            End Get
            Set(ByVal Value As String)
                Try
                    m_strServerName = Value
                Catch ex As Exception
                    m_strServerName = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' Port����
        '----------------------------------------------------------------
        Public Property Port() As Integer
            Get
                Port = m_intPort
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPort = Value
                Catch ex As Exception
                    m_intPort = DEFAULT_PORT
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' UserID����
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
        ' Password����
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
        ' ProxyUrl����
        '----------------------------------------------------------------
        Public Property ProxyUrl() As String
            Get
                ProxyUrl = m_strProxyUrl
            End Get
            Set(ByVal Value As String)
                Try
                    m_strProxyUrl = Value
                Catch ex As Exception
                    m_strProxyUrl = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' ProxyUserID����
        '----------------------------------------------------------------
        Public Property ProxyUserID() As String
            Get
                ProxyUserID = m_strProxyUserID
            End Get
            Set(ByVal Value As String)
                Try
                    m_strProxyUserID = Value
                Catch ex As Exception
                    m_strProxyUserID = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' ProxyPassword����
        '----------------------------------------------------------------
        Public Property ProxyPassword() As String
            Get
                ProxyPassword = m_strProxyPassword
            End Get
            Set(ByVal Value As String)
                Try
                    m_strProxyPassword = Value
                Catch ex As Exception
                    m_strProxyPassword = ""
                End Try
            End Set
        End Property











        '----------------------------------------------------------------
        ' ��ȡFTP��������·��
        '----------------------------------------------------------------
        Public Function getRootUrl() As String

            Try
                getRootUrl = ""
                getRootUrl = getRootUrl + Me.DEFAULT_PREFIX + DEFAULT_ROOTSEP
                If Me.Port = 21 Then
                    getRootUrl = getRootUrl + Me.ServerName + DEFAULT_DIRSEP
                Else
                    getRootUrl = getRootUrl + Me.ServerName + DEFAULT_PORTSEP
                    getRootUrl = getRootUrl + Me.Port.ToString() + DEFAULT_DIRSEP
                End If
            Catch ex As Exception
                getRootUrl = ""
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡFTP����������·��
        '----------------------------------------------------------------
        Public Function getUrl(ByVal strRelativePath As String) As String
            Try
                Dim strRoot As String

                '��ȡ��·��
                strRoot = Me.getRootUrl()

                '����·���ָ���
                If strRelativePath Is Nothing Then strRelativePath = ""
                strRelativePath = strRelativePath.Trim()
                strRelativePath = strRelativePath.Replace("\", DEFAULT_DIRSEP)

                '�ϲ�·��
                If strRelativePath.Substring(0, 1) = DEFAULT_DIRSEP Then
                    strRelativePath = strRelativePath.Substring(1, strRelativePath.Length - 1)
                End If
                getUrl = strRoot + strRelativePath

            Catch ex As Exception
                getUrl = ""
            End Try

        End Function

    End Class

End Namespace