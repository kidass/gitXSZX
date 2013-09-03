Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IMXtglSjdxSjk
    '
    ' ���������� 
    '     xtgl_sjdx_sjk.aspxģ�鱾��ָ��ֳ���Ҫ����Ϣ
    '----------------------------------------------------------------
    <Serializable()> Public Class IMXtglSjdxSjk
        Implements IDisposable

        '----------------------------------------------------------------
        ' ģ������
        '----------------------------------------------------------------
        Private m_strtxtFWQMC As String                    'txtFWQMC
        Private m_strtxtSJKMC As String                    'txtSJKMC
        Private m_strtxtSJKZWM As String                   'txtSJKZWM
        Private m_strtxtSJKSM As String                    'txtSJKSM













        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
            m_strtxtFWQMC = ""
            m_strtxtSJKMC = ""
            m_strtxtSJKZWM = ""
            m_strtxtSJKSM = ""
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
        ' txtSJKZWM����
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
        ' txtSJKSM����
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
