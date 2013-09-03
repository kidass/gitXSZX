Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IMFlowFujianInfo
    '
    ' 功能描述： 
    '     flow_fujian_info.aspx模块本身恢复现场需要的信息
    '----------------------------------------------------------------
    <Serializable()> Public Class IMFlowFujianInfo
        Implements IDisposable

        '----------------------------------------------------------------
        'textbox
        '----------------------------------------------------------------
        Private m_strtxtWJXH As String                                'txtWJXH
        Private m_strtxtWJWZ As String                                'txtWJWZ
        Private m_strtxtWJSM As String                                'txtWJSM
        Private m_strtxtWJYS As String                                'txtWJYS
        Private m_strtxtWEBURL As String                              'txtWEBURL

        '----------------------------------------------------------------
        'hidden textbox
        '----------------------------------------------------------------
        Private m_strhtxtWEBLOC As String                            'htxtWEBLOC

        Private m_strhtxtDivLeftBody As String                       'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String                        'htxtDivTopBody











        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()

            MyBase.New()

            m_strtxtWJXH = ""
            m_strtxtWJWZ = ""
            m_strtxtWJSM = ""
            m_strtxtWJYS = ""
            m_strtxtWEBURL = ""

            m_strhtxtWEBLOC = ""

            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""

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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMFlowFujianInfo)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub













        '----------------------------------------------------------------
        ' txtWJXH属性
        '----------------------------------------------------------------
        Public Property txtWJXH() As String
            Get
                txtWJXH = m_strtxtWJXH
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtWJXH = Value
                Catch ex As Exception
                    m_strtxtWJXH = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtWJWZ属性
        '----------------------------------------------------------------
        Public Property txtWJWZ() As String
            Get
                txtWJWZ = m_strtxtWJWZ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtWJWZ = Value
                Catch ex As Exception
                    m_strtxtWJWZ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtWJSM属性
        '----------------------------------------------------------------
        Public Property txtWJSM() As String
            Get
                txtWJSM = m_strtxtWJSM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtWJSM = Value
                Catch ex As Exception
                    m_strtxtWJSM = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtWJYS属性
        '----------------------------------------------------------------
        Public Property txtWJYS() As String
            Get
                txtWJYS = m_strtxtWJYS
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtWJYS = Value
                Catch ex As Exception
                    m_strtxtWJYS = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtWEBURL属性
        '----------------------------------------------------------------
        Public Property txtWEBURL() As String
            Get
                txtWEBURL = m_strtxtWEBURL
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtWEBURL = Value
                Catch ex As Exception
                    m_strtxtWEBURL = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' htxtWEBLOC属性
        '----------------------------------------------------------------
        Public Property htxtWEBLOC() As String
            Get
                htxtWEBLOC = m_strhtxtWEBLOC
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtWEBLOC = Value
                Catch ex As Exception
                    m_strhtxtWEBLOC = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' htxtDivLeftBody属性
        '----------------------------------------------------------------
        Public Property htxtDivLeftBody() As String
            Get
                htxtDivLeftBody = m_strhtxtDivLeftBody
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftBody = Value
                Catch ex As Exception
                    m_strhtxtDivLeftBody = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopBody属性
        '----------------------------------------------------------------
        Public Property htxtDivTopBody() As String
            Get
                htxtDivTopBody = m_strhtxtDivTopBody
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopBody = Value
                Catch ex As Exception
                    m_strhtxtDivTopBody = ""
                End Try
            End Set
        End Property

    End Class

End Namespace
