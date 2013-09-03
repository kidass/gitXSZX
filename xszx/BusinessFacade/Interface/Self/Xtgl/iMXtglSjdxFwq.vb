Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IMXtglSjdxFwq
    '
    ' 功能描述： 
    '     xtgl_sjdx_fwq.aspx模块本身恢复现场需要的信息
    '----------------------------------------------------------------
    <Serializable()> Public Class IMXtglSjdxFwq
        Implements IDisposable

        '----------------------------------------------------------------
        ' 模块属性
        '----------------------------------------------------------------
        Private m_strtxtFWQMC As String                    'txtFWQMC
        Private m_strtxtFWQLX As String                    'txtFWQLX
        Private m_strtxtFWQTGZ As String                   'txtFWQTGZ
        Private m_strtxtSJKMC As String                    'txtSJKMC
        Private m_strtxtUserId As String                   'txtUserId
        Private m_strtxtUserPwd As String                  'txtUserPwd
        Private m_strtxtFWQSM As String                    'txtFWQSM











        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
            m_strtxtFWQMC = ""
            m_strtxtFWQLX = ""
            m_strtxtFWQTGZ = ""
            m_strtxtSJKMC = ""
            m_strtxtUserId = ""
            m_strtxtUserPwd = ""
            m_strtxtFWQSM = ""
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMXtglSjdxFwq)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub













        '----------------------------------------------------------------
        ' txtFWQMC属性
        '----------------------------------------------------------------
        Public Property txtFWQMC() As String
            Get
                txtFWQMC = m_strtxtFWQMC
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtFWQMC = Value
                Catch ex As Exception
                    m_strtxtFWQMC = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtFWQLX属性
        '----------------------------------------------------------------
        Public Property txtFWQLX() As String
            Get
                txtFWQLX = m_strtxtFWQLX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtFWQLX = Value
                Catch ex As Exception
                    m_strtxtFWQLX = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtFWQTGZ属性
        '----------------------------------------------------------------
        Public Property txtFWQTGZ() As String
            Get
                txtFWQTGZ = m_strtxtFWQTGZ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtFWQTGZ = Value
                Catch ex As Exception
                    m_strtxtFWQTGZ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtSJKMC属性
        '----------------------------------------------------------------
        Public Property txtSJKMC() As String
            Get
                txtSJKMC = m_strtxtSJKMC
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSJKMC = Value
                Catch ex As Exception
                    m_strtxtSJKMC = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtUserId属性
        '----------------------------------------------------------------
        Public Property txtUserId() As String
            Get
                txtUserId = m_strtxtUserId
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtUserId = Value
                Catch ex As Exception
                    m_strtxtUserId = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtUserPwd属性
        '----------------------------------------------------------------
        Public Property txtUserPwd() As String
            Get
                txtUserPwd = m_strtxtUserPwd
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtUserPwd = Value
                Catch ex As Exception
                    m_strtxtUserPwd = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtFWQSM属性
        '----------------------------------------------------------------
        Public Property txtFWQSM() As String
            Get
                txtFWQSM = m_strtxtFWQSM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtFWQSM = Value
                Catch ex As Exception
                    m_strtxtFWQSM = ""
                End Try
            End Set
        End Property

    End Class

End Namespace
