Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IMXtglMkglInfo
    '
    ' 功能描述： 
    '     xtgl_mkgl_info.aspx模块本身恢复现场需要的信息
    '----------------------------------------------------------------
    <Serializable()> Public Class IMXtglMkglInfo
        Implements IDisposable

        '----------------------------------------------------------------
        ' 模块属性
        '----------------------------------------------------------------
        Private m_strtxtMKDM As String                    'txtMKDM
        Private m_strtxtMKMC As String                    'txtMKMC
        Private m_strtxtMKJB As String                    'txtMKJB
        Private m_strtxtMKSM As String                    'txtMKSM
        Private m_strhtxtMKBS As String                   'htxtMKBS
        Private m_strhtxtMKBJDM As String                 'htxtMKBJDM
        Private m_strhtxtSJMKDM As String                 'htxtSJMKDM
        Private m_strhtxtDJMKDM As String                 'htxtDJMKDM











        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
            m_strtxtMKDM = ""
            m_strtxtMKMC = ""
            m_strtxtMKJB = ""
            m_strtxtMKSM = ""
            m_strhtxtMKBS = ""
            m_strhtxtMKBJDM = ""
            m_strhtxtSJMKDM = ""
            m_strhtxtDJMKDM = ""
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMXtglMkglInfo)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub












        '----------------------------------------------------------------
        ' txtMKDM属性
        '----------------------------------------------------------------
        Public Property txtMKDM() As String
            Get
                txtMKDM = m_strtxtMKDM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtMKDM = Value
                Catch ex As Exception
                    m_strtxtMKDM = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtMKMC属性
        '----------------------------------------------------------------
        Public Property txtMKMC() As String
            Get
                txtMKMC = m_strtxtMKMC
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtMKMC = Value
                Catch ex As Exception
                    m_strtxtMKMC = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtMKJB属性
        '----------------------------------------------------------------
        Public Property txtMKJB() As String
            Get
                txtMKJB = m_strtxtMKJB
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtMKJB = Value
                Catch ex As Exception
                    m_strtxtMKJB = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtMKSM属性
        '----------------------------------------------------------------
        Public Property txtMKSM() As String
            Get
                txtMKSM = m_strtxtMKSM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtMKSM = Value
                Catch ex As Exception
                    m_strtxtMKSM = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtMKBS属性
        '----------------------------------------------------------------
        Public Property htxtMKBS() As String
            Get
                htxtMKBS = m_strhtxtMKBS
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtMKBS = Value
                Catch ex As Exception
                    m_strhtxtMKBS = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtMKBJDM属性
        '----------------------------------------------------------------
        Public Property htxtMKBJDM() As String
            Get
                htxtMKBJDM = m_strhtxtMKBJDM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtMKBJDM = Value
                Catch ex As Exception
                    m_strhtxtMKBJDM = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtSJMKDM属性
        '----------------------------------------------------------------
        Public Property htxtSJMKDM() As String
            Get
                htxtSJMKDM = m_strhtxtSJMKDM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtSJMKDM = Value
                Catch ex As Exception
                    m_strhtxtSJMKDM = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDJMKDM属性
        '----------------------------------------------------------------
        Public Property htxtDJMKDM() As String
            Get
                htxtDJMKDM = m_strhtxtDJMKDM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDJMKDM = Value
                Catch ex As Exception
                    m_strhtxtDJMKDM = ""
                End Try
            End Set
        End Property

    End Class

End Namespace
