Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IMXtglSjdxSjkdx
    '
    ' 功能描述： 
    '     xtgl_sjdx_sjkdx.aspx模块本身恢复现场需要的信息
    '----------------------------------------------------------------
    <Serializable()> Public Class IMXtglSjdxSjkdx
        Implements IDisposable

        '----------------------------------------------------------------
        ' 模块属性
        '----------------------------------------------------------------
        Private m_strtxtFWQMC As String                    'txtFWQMC
        Private m_strhtxtDXBS As String                    'htxtDXBS
        Private m_strtxtSJKMC As String                    'txtSJKMC
        Private m_strtxtDXMC As String                     'txtDXMC
        Private m_strtxtDXLX As String                     'txtDXLX
        Private m_strtxtDXZWM As String                    'txtDXZWM
        Private m_strtxtDXSM As String                     'txtDXSM











        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
            m_strtxtFWQMC = ""
            m_strhtxtDXBS = ""
            m_strtxtSJKMC = ""
            m_strtxtDXMC = ""
            m_strtxtDXLX = ""
            m_strtxtDXZWM = ""
            m_strtxtDXSM = ""
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMXtglSjdxSjkdx)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub













        '----------------------------------------------------------------
        ' htxtDXBS属性
        '----------------------------------------------------------------
        Public Property htxtDXBS() As String
            Get
                htxtDXBS = m_strhtxtDXBS
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDXBS = Value
                Catch ex As Exception
                    m_strhtxtDXBS = ""
                End Try
            End Set
        End Property

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
        ' txtDXMC属性
        '----------------------------------------------------------------
        Public Property txtDXMC() As String
            Get
                txtDXMC = m_strtxtDXMC
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtDXMC = Value
                Catch ex As Exception
                    m_strtxtDXMC = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtDXLX属性
        '----------------------------------------------------------------
        Public Property txtDXLX() As String
            Get
                txtDXLX = m_strtxtDXLX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtDXLX = Value
                Catch ex As Exception
                    m_strtxtDXLX = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtDXZWM属性
        '----------------------------------------------------------------
        Public Property txtDXZWM() As String
            Get
                txtDXZWM = m_strtxtDXZWM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtDXZWM = Value
                Catch ex As Exception
                    m_strtxtDXZWM = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtDXSM属性
        '----------------------------------------------------------------
        Public Property txtDXSM() As String
            Get
                txtDXSM = m_strtxtDXSM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtDXSM = Value
                Catch ex As Exception
                    m_strtxtDXSM = ""
                End Try
            End Set
        End Property

    End Class

End Namespace
