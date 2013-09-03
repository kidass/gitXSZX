Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IMXtglSjdxFwq
    '
    ' ���������� 
    '     xtgl_sjdx_fwq.aspxģ�鱾��ָ��ֳ���Ҫ����Ϣ
    '----------------------------------------------------------------
    <Serializable()> Public Class IMXtglSjdxFwq
        Implements IDisposable

        '----------------------------------------------------------------
        ' ģ������
        '----------------------------------------------------------------
        Private m_strtxtFWQMC As String                    'txtFWQMC
        Private m_strtxtFWQLX As String                    'txtFWQLX
        Private m_strtxtFWQTGZ As String                   'txtFWQTGZ
        Private m_strtxtSJKMC As String                    'txtSJKMC
        Private m_strtxtUserId As String                   'txtUserId
        Private m_strtxtUserPwd As String                  'txtUserPwd
        Private m_strtxtFWQSM As String                    'txtFWQSM











        '----------------------------------------------------------------
        ' ���캯��
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
        ' txtFWQMC����
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
        ' txtFWQLX����
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
        ' txtFWQTGZ����
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
        ' txtSJKMC����
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
        ' txtUserId����
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
        ' txtUserPwd����
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
        ' txtFWQSM����
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
