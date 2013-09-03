Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IMGgxxGgzyZylmInfo
    '
    ' ���������� 
    '     ggxx_ggzy_zylm_info.aspxģ�鱾��ָ��ֳ���Ҫ����Ϣ
    '----------------------------------------------------------------
    <Serializable()> Public Class IMGgxxGgzyZylmInfo
        Implements IDisposable

        '----------------------------------------------------------------
        ' ģ������
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
        ' ���캯��
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
        ' txtLMDM����
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
        ' txtLMMC����
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
        ' txtLMJB����
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
        ' txtLMSM����
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
        ' htxtLMBS����
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
        ' htxtLMBJDM����
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
        ' htxtSJLMDM����
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
        ' htxtDJLMDM����
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
