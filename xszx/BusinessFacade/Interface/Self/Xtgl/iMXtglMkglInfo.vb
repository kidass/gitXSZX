Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IMXtglMkglInfo
    '
    ' ���������� 
    '     xtgl_mkgl_info.aspxģ�鱾��ָ��ֳ���Ҫ����Ϣ
    '----------------------------------------------------------------
    <Serializable()> Public Class IMXtglMkglInfo
        Implements IDisposable

        '----------------------------------------------------------------
        ' ģ������
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
        ' ���캯��
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
        ' txtMKDM����
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
        ' txtMKMC����
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
        ' txtMKJB����
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
        ' txtMKSM����
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
        ' htxtMKBS����
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
        ' htxtMKBJDM����
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
        ' htxtSJMKDM����
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
        ' htxtDJMKDM����
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
