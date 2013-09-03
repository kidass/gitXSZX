Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IMFlowDubanjg
    '
    ' ���������� 
    '     flow_dubanjg.aspxģ�鱾��ָ��ֳ���Ҫ����Ϣ
    '----------------------------------------------------------------
    <Serializable()> Public Class IMFlowDubanjg
        Implements IDisposable

        '----------------------------------------------------------------
        'hidden textbox
        '----------------------------------------------------------------
        Private m_strhtxtBDBXXQuery As String                            'htxtBDBXXQuery
        Private m_strhtxtBDBXXRows As String                             'htxtBDBXXRows
        Private m_strhtxtBDBXXSort As String                             'htxtBDBXXSort
        Private m_strhtxtBDBXXSortColumnIndex As String                  'htxtBDBXXSortColumnIndex
        Private m_strhtxtBDBXXSortType As String                         'htxtBDBXXSortType
        Private m_strhtxtDivLeftBDBXX As String                          'htxtDivLeftBDBXX
        Private m_strhtxtDivTopBDBXX As String                           'htxtDivTopBDBXX
        Private m_strhtxtDivLeftBody As String                           'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String                            'htxtDivTopBody

        '----------------------------------------------------------------
        'grdBDBXX paramters
        '----------------------------------------------------------------
        Private m_strhtxtSessionIdBDBXX As String                        'SessionId
        Private m_intPageSize_BDBXX As Integer                           'grdBDBXX��ҳ��С
        Private m_intSelectedIndex_BDBXX As Integer                      'grdBDBXX��������
        Private m_intCurrentPageIndex_BDBXX As Integer                   'grdBDBXX��ҳ����

        '----------------------------------------------------------------
        'textarea paramters
        '----------------------------------------------------------------
        Private m_strtextareaQBJG As String                              'textareaQBJG
        Private m_strtextareaBCJG As String                              'textareaBCJG











        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()

            MyBase.New()

            m_strhtxtBDBXXQuery = ""
            m_strhtxtBDBXXRows = ""
            m_strhtxtBDBXXSort = ""
            m_strhtxtBDBXXSortColumnIndex = ""
            m_strhtxtBDBXXSortType = ""

            m_strhtxtDivLeftBDBXX = ""
            m_strhtxtDivTopBDBXX = ""
            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""

            m_strhtxtSessionIdBDBXX = ""
            m_intPageSize_BDBXX = 100
            m_intSelectedIndex_BDBXX = -1
            m_intCurrentPageIndex_BDBXX = 0

            m_strtextareaQBJG = ""
            m_strtextareaBCJG = ""

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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMFlowDubanjg)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub













        '----------------------------------------------------------------
        ' htxtBDBXXSort����
        '----------------------------------------------------------------
        Public Property htxtBDBXXSort() As String
            Get
                htxtBDBXXSort = m_strhtxtBDBXXSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtBDBXXSort = Value
                Catch ex As Exception
                    m_strhtxtBDBXXSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtBDBXXRows����
        '----------------------------------------------------------------
        Public Property htxtBDBXXRows() As String
            Get
                htxtBDBXXRows = m_strhtxtBDBXXRows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtBDBXXRows = Value
                Catch ex As Exception
                    m_strhtxtBDBXXRows = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtBDBXXSortColumnIndex����
        '----------------------------------------------------------------
        Public Property htxtBDBXXSortColumnIndex() As String
            Get
                htxtBDBXXSortColumnIndex = m_strhtxtBDBXXSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtBDBXXSortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtBDBXXSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtBDBXXQuery����
        '----------------------------------------------------------------
        Public Property htxtBDBXXQuery() As String
            Get
                htxtBDBXXQuery = m_strhtxtBDBXXQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtBDBXXQuery = Value
                Catch ex As Exception
                    m_strhtxtBDBXXQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtBDBXXSortType����
        '----------------------------------------------------------------
        Public Property htxtBDBXXSortType() As String
            Get
                htxtBDBXXSortType = m_strhtxtBDBXXSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtBDBXXSortType = Value
                Catch ex As Exception
                    m_strhtxtBDBXXSortType = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' htxtDivLeftBDBXX����
        '----------------------------------------------------------------
        Public Property htxtDivLeftBDBXX() As String
            Get
                htxtDivLeftBDBXX = m_strhtxtDivLeftBDBXX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftBDBXX = Value
                Catch ex As Exception
                    m_strhtxtDivLeftBDBXX = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopBDBXX����
        '----------------------------------------------------------------
        Public Property htxtDivTopBDBXX() As String
            Get
                htxtDivTopBDBXX = m_strhtxtDivTopBDBXX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopBDBXX = Value
                Catch ex As Exception
                    m_strhtxtDivTopBDBXX = ""
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
        ' htxtSessionIdBDBXX����
        '----------------------------------------------------------------
        Public Property htxtSessionIdBDBXX() As String
            Get
                htxtSessionIdBDBXX = m_strhtxtSessionIdBDBXX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtSessionIdBDBXX = Value
                Catch ex As Exception
                    m_strhtxtSessionIdBDBXX = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdBDBXX_PageSize����
        '----------------------------------------------------------------
        Public Property grdBDBXX_PageSize() As Integer
            Get
                grdBDBXX_PageSize = m_intPageSize_BDBXX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_BDBXX = Value
                Catch ex As Exception
                    m_intPageSize_BDBXX = 100
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdBDBXX_SelectedIndex����
        '----------------------------------------------------------------
        Public Property grdBDBXX_SelectedIndex() As Integer
            Get
                grdBDBXX_SelectedIndex = m_intSelectedIndex_BDBXX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_BDBXX = Value
                Catch ex As Exception
                    m_intSelectedIndex_BDBXX = -1
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdBDBXX_CurrentPageIndex����
        '----------------------------------------------------------------
        Public Property grdBDBXX_CurrentPageIndex() As Integer
            Get
                grdBDBXX_CurrentPageIndex = m_intCurrentPageIndex_BDBXX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intCurrentPageIndex_BDBXX = Value
                Catch ex As Exception
                    m_intCurrentPageIndex_BDBXX = -1
                End Try
            End Set
        End Property



        '----------------------------------------------------------------
        ' textareaQBJG����
        '----------------------------------------------------------------
        Public Property textareaQBJG() As String
            Get
                textareaQBJG = m_strtextareaQBJG
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtextareaQBJG = Value
                Catch ex As Exception
                    m_strtextareaQBJG = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' textareaBCJG����
        '----------------------------------------------------------------
        Public Property textareaBCJG() As String
            Get
                textareaBCJG = m_strtextareaBCJG
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtextareaBCJG = Value
                Catch ex As Exception
                    m_strtextareaBCJG = ""
                End Try
            End Set
        End Property

    End Class

End Namespace
