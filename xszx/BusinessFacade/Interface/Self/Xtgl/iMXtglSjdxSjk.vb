Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IMXtglSjdxSjk
    '
    ' 功能描述： 
    '     xtgl_sjdx_sjk.aspx模块本身恢复现场需要的信息
    '----------------------------------------------------------------
    <Serializable()> Public Class IMXtglSjdxSjk
        Implements IDisposable

        '----------------------------------------------------------------
        ' 模块属性
        '----------------------------------------------------------------
        Private m_strtxtFWQMC As String                    'txtFWQMC
        Private m_strtxtSJKMC As String                    'txtSJKMC
        Private m_strtxtSJKZWM As String                   'txtSJKZWM
        Private m_strtxtSJKSM As String                    'txtSJKSM













        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
            m_strtxtFWQMC = ""
            m_strtxtSJKMC = ""
            m_strtxtSJKZWM = ""
            m_strtxtSJKSM = ""
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMXtglSjdxSjk)
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
        ' txtSJKZWM属性
        '----------------------------------------------------------------
        Public Property txtSJKZWM() As String
            Get
                txtSJKZWM = m_strtxtSJKZWM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSJKZWM = Value
                Catch ex As Exception
                    m_strtxtSJKZWM = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtSJKSM属性
        '----------------------------------------------------------------
        Public Property txtSJKSM() As String
            Get
                txtSJKSM = m_strtxtSJKSM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSJKSM = Value
                Catch ex As Exception
                    m_strtxtSJKSM = ""
                End Try
            End Set
        End Property

    End Class

End Namespace
