Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IMFlowReceive
    '
    ' ���������� 
    '     flow_receive.aspxģ�鱾��ָ��ֳ���Ҫ����Ϣ
    '----------------------------------------------------------------
    <Serializable()> Public Class IMFlowReceive
        Implements IDisposable

        '----------------------------------------------------------------
        'hidden textbox
        '----------------------------------------------------------------
        Private m_strhtxtFSRXXQuery As String                            'htxtFSRXXQuery
        Private m_strhtxtFSRXXRows As String                             'htxtFSRXXRows
        Private m_strhtxtFSRXXSort As String                             'htxtFSRXXSort
        Private m_strhtxtFSRXXSortColumnIndex As String                  'htxtFSRXXSortColumnIndex
        Private m_strhtxtFSRXXSortType As String                         'htxtFSRXXSortType
        Private m_strhtxtDivLeftFSRXX As String                          'htxtDivLeftFSRXX
        Private m_strhtxtDivTopFSRXX As String                           'htxtDivTopFSRXX
        Private m_strhtxtDivLeftBody As String                           'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String                            'htxtDivTopBody

        '----------------------------------------------------------------
        'grdFSRXX paramters
        '----------------------------------------------------------------
        Private m_strhtxtSessionIdFSRXX As String                        'SessionId
        Private m_intPageSize_FSRXX As Integer                           'grdFSRXX��ҳ��С
        Private m_intSelectedIndex_FSRXX As Integer                      'grdFSRXX��������
        Private m_intCurrentPageIndex_FSRXX As Integer                   'grdFSRXX��ҳ����













        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()

            MyBase.New()

            m_strhtxtFSRXXQuery = ""
            m_strhtxtFSRXXRows = ""
            m_strhtxtFSRXXSort = ""
            m_strhtxtFSRXXSortColumnIndex = ""
            m_strhtxtFSRXXSortType = ""

            m_strhtxtDivLeftFSRXX = ""
            m_strhtxtDivTopFSRXX = ""
            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""

            m_strhtxtSessionIdFSRXX = ""
            m_intPageSize_FSRXX = 100
            m_intSelectedIndex_FSRXX = -1
            m_intCurrentPageIndex_FSRXX = 0

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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMFlowReceive)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub












        '----------------------------------------------------------------
        ' htxtFSRXXSort����
        '----------------------------------------------------------------
        Public Property htxtFSRXXSort() As String
            Get
                htxtFSRXXSort = m_strhtxtFSRXXSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtFSRXXSort = Value
                Catch ex As Exception
                    m_strhtxtFSRXXSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtFSRXXRows����
        '----------------------------------------------------------------
        Public Property htxtFSRXXRows() As String
            Get
                htxtFSRXXRows = m_strhtxtFSRXXRows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtFSRXXRows = Value
                Catch ex As Exception
                    m_strhtxtFSRXXRows = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtFSRXXSortColumnIndex����
        '----------------------------------------------------------------
        Public Property htxtFSRXXSortColumnIndex() As String
            Get
                htxtFSRXXSortColumnIndex = m_strhtxtFSRXXSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtFSRXXSortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtFSRXXSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtFSRXXQuery����
        '----------------------------------------------------------------
        Public Property htxtFSRXXQuery() As String
            Get
                htxtFSRXXQuery = m_strhtxtFSRXXQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtFSRXXQuery = Value
                Catch ex As Exception
                    m_strhtxtFSRXXQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtFSRXXSortType����
        '----------------------------------------------------------------
        Public Property htxtFSRXXSortType() As String
            Get
                htxtFSRXXSortType = m_strhtxtFSRXXSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtFSRXXSortType = Value
                Catch ex As Exception
                    m_strhtxtFSRXXSortType = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' htxtDivLeftFSRXX����
        '----------------------------------------------------------------
        Public Property htxtDivLeftFSRXX() As String
            Get
                htxtDivLeftFSRXX = m_strhtxtDivLeftFSRXX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftFSRXX = Value
                Catch ex As Exception
                    m_strhtxtDivLeftFSRXX = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopFSRXX����
        '----------------------------------------------------------------
        Public Property htxtDivTopFSRXX() As String
            Get
                htxtDivTopFSRXX = m_strhtxtDivTopFSRXX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopFSRXX = Value
                Catch ex As Exception
                    m_strhtxtDivTopFSRXX = ""
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
        ' htxtSessionIdFSRXX����
        '----------------------------------------------------------------
        Public Property htxtSessionIdFSRXX() As String
            Get
                htxtSessionIdFSRXX = m_strhtxtSessionIdFSRXX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtSessionIdFSRXX = Value
                Catch ex As Exception
                    m_strhtxtSessionIdFSRXX = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdFSRXX_PageSize����
        '----------------------------------------------------------------
        Public Property grdFSRXX_PageSize() As Integer
            Get
                grdFSRXX_PageSize = m_intPageSize_FSRXX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_FSRXX = Value
                Catch ex As Exception
                    m_intPageSize_FSRXX = 100
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdFSRXX_SelectedIndex����
        '----------------------------------------------------------------
        Public Property grdFSRXX_SelectedIndex() As Integer
            Get
                grdFSRXX_SelectedIndex = m_intSelectedIndex_FSRXX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_FSRXX = Value
                Catch ex As Exception
                    m_intSelectedIndex_FSRXX = -1
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdFSRXX_CurrentPageIndex����
        '----------------------------------------------------------------
        Public Property grdFSRXX_CurrentPageIndex() As Integer
            Get
                grdFSRXX_CurrentPageIndex = m_intCurrentPageIndex_FSRXX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intCurrentPageIndex_FSRXX = Value
                Catch ex As Exception
                    m_intCurrentPageIndex_FSRXX = -1
                End Try
            End Set
        End Property

    End Class

End Namespace
