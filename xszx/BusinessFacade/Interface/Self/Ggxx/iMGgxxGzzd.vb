Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IMGgxxGzzd
    '
    ' ���������� 
    '     ggxx_gzzd.aspxģ�鱾��ָ��ֳ���Ҫ����Ϣ
    '----------------------------------------------------------------
    <Serializable()> Public Class IMGgxxGzzd
        Implements IDisposable

        '----------------------------------------------------------------
        'hidden textbox
        '----------------------------------------------------------------
        Private m_strhtxtDivLeftNR As String                      'htxtDivLeftNR
        Private m_strhtxtDivTopNR As String                       'htxtDivTopNR
        Private m_strhtxtDivLeftBody As String                    'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String                     'htxtDivTopBody
        Private m_strhtxtSpliterX As String                       'htxtSpliterX

        '----------------------------------------------------------------
        'Microsoft.Web.UI.WebControls.TreeView
        '----------------------------------------------------------------
        Private m_strSelectNodeIndex_tvwGZZD As String           'tvwGZZD








        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            'hidden
            m_strhtxtDivLeftNR = ""
            m_strhtxtDivTopNR = ""
            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""
            m_strhtxtSpliterX = ""

            m_strSelectNodeIndex_tvwGZZD = ""

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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMGgxxGzzd)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub













        '----------------------------------------------------------------
        ' htxtDivLeftNR����
        '----------------------------------------------------------------
        Public Property htxtDivLeftNR() As String
            Get
                htxtDivLeftNR = m_strhtxtDivLeftNR
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftNR = Value
                Catch ex As Exception
                    m_strhtxtDivLeftNR = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopNR����
        '----------------------------------------------------------------
        Public Property htxtDivTopNR() As String
            Get
                htxtDivTopNR = m_strhtxtDivTopNR
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopNR = Value
                Catch ex As Exception
                    m_strhtxtDivTopNR = ""
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

        '----------------------------------------------------------------
        ' htxtSpliterX����
        '----------------------------------------------------------------
        Public Property htxtSpliterX() As String
            Get
                htxtSpliterX = m_strhtxtSpliterX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtSpliterX = Value
                Catch ex As Exception
                    m_strhtxtSpliterX = ""
                End Try
            End Set
        End Property





        '----------------------------------------------------------------
        ' tvwGZZD_SelectNodeIndex����
        '----------------------------------------------------------------
        Public Property tvwGZZD_SelectNodeIndex() As String
            Get
                tvwGZZD_SelectNodeIndex = m_strSelectNodeIndex_tvwGZZD
            End Get
            Set(ByVal Value As String)
                Try
                    m_strSelectNodeIndex_tvwGZZD = Value
                Catch ex As Exception
                    m_strSelectNodeIndex_tvwGZZD = ""
                End Try
            End Set
        End Property

    End Class

End Namespace
