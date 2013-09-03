Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IMFlowFujianInfo
    '
    ' ���������� 
    '     flow_fujian_info.aspxģ�鱾��ָ��ֳ���Ҫ����Ϣ
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
        ' ���캯��
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
        ' txtWJXH����
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
        ' txtWJWZ����
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
        ' txtWJSM����
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
        ' txtWJYS����
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
        ' txtWEBURL����
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
        ' htxtWEBLOC����
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
        ' htxtDivLeftBody����
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
        ' htxtDivTopBody����
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
