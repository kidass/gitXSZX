Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IMGgxxGgzyZylmInfo
    '
    ' 功能描述： 
    '     ggxx_ggzy_zylm_info.aspx模块本身恢复现场需要的信息
    '----------------------------------------------------------------
    <Serializable()> Public Class IMGgxxGgzyZylmInfo
        Implements IDisposable

        '----------------------------------------------------------------
        ' 模块属性
        '----------------------------------------------------------------
        Private m_strtxtLMDM As String                    'txtLMDM
        Private m_strtxtLMMC As String                    'txtLMMC
        Private m_strtxtLMJB As String                    'txtLMJB
        Private m_strtxtLMSM As String                    'txtLMSM
        Private m_strhtxtLMBS As String                   'htxtLMBS
        Private m_strhtxtLMBJDM As String                 'htxtLMBJDM
        Private m_strhtxtSJLMDM As String                 'htxtSJLMDM
        Private m_strhtxtDJLMDM As String                 'htxtDJLMDM











        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()

            MyBase.New()

            m_strtxtLMDM = ""
            m_strtxtLMMC = ""
            m_strtxtLMJB = ""
            m_strtxtLMSM = ""

            m_strhtxtLMBS = ""
            m_strhtxtLMBJDM = ""
            m_strhtxtSJLMDM = ""
            m_strhtxtDJLMDM = ""

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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMGgxxGgzyZylmInfo)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub












        '----------------------------------------------------------------
        ' txtLMDM属性
        '----------------------------------------------------------------
        Public Property txtLMDM() As String
            Get
                txtLMDM = m_strtxtLMDM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtLMDM = Value
                Catch ex As Exception
                    m_strtxtLMDM = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtLMMC属性
        '----------------------------------------------------------------
        Public Property txtLMMC() As String
            Get
                txtLMMC = m_strtxtLMMC
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtLMMC = Value
                Catch ex As Exception
                    m_strtxtLMMC = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtLMJB属性
        '----------------------------------------------------------------
        Public Property txtLMJB() As String
            Get
                txtLMJB = m_strtxtLMJB
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtLMJB = Value
                Catch ex As Exception
                    m_strtxtLMJB = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtLMSM属性
        '----------------------------------------------------------------
        Public Property txtLMSM() As String
            Get
                txtLMSM = m_strtxtLMSM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtLMSM = Value
                Catch ex As Exception
                    m_strtxtLMSM = ""
                End Try
            End Set
        End Property





        '----------------------------------------------------------------
        ' htxtLMBS属性
        '----------------------------------------------------------------
        Public Property htxtLMBS() As String
            Get
                htxtLMBS = m_strhtxtLMBS
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtLMBS = Value
                Catch ex As Exception
                    m_strhtxtLMBS = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtLMBJDM属性
        '----------------------------------------------------------------
        Public Property htxtLMBJDM() As String
            Get
                htxtLMBJDM = m_strhtxtLMBJDM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtLMBJDM = Value
                Catch ex As Exception
                    m_strhtxtLMBJDM = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtSJLMDM属性
        '----------------------------------------------------------------
        Public Property htxtSJLMDM() As String
            Get
                htxtSJLMDM = m_strhtxtSJLMDM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtSJLMDM = Value
                Catch ex As Exception
                    m_strhtxtSJLMDM = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDJLMDM属性
        '----------------------------------------------------------------
        Public Property htxtDJLMDM() As String
            Get
                htxtDJLMDM = m_strhtxtDJLMDM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDJLMDM = Value
                Catch ex As Exception
                    m_strhtxtDJLMDM = ""
                End Try
            End Set
        End Property

    End Class

End Namespace
