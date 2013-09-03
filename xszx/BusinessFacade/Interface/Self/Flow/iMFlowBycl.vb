Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IMFlowBycl
    '
    ' ���������� 
    '     flow_bycl.aspxģ�鱾��ָ��ֳ���Ҫ����Ϣ
    '----------------------------------------------------------------
    <Serializable()> Public Class IMFlowBycl
        Implements IDisposable

        '----------------------------------------------------------------
        'hidden textbox
        '----------------------------------------------------------------
        Private m_strhtxtSGWXXQuery As String                            'htxtSGWXXQuery
        Private m_strhtxtSGWXXRows As String                             'htxtSGWXXRows
        Private m_strhtxtSGWXXSort As String                             'htxtSGWXXSort
        Private m_strhtxtSGWXXSortColumnIndex As String                  'htxtSGWXXSortColumnIndex
        Private m_strhtxtSGWXXSortType As String                         'htxtSGWXXSortType
        Private m_strhtxtWSCXXQuery As String                            'htxtWSCXXQuery
        Private m_strhtxtWSCXXRows As String                             'htxtWSCXXRows
        Private m_strhtxtWSCXXSort As String                             'htxtWSCXXSort
        Private m_strhtxtWSCXXSortColumnIndex As String                  'htxtWSCXXSortColumnIndex
        Private m_strhtxtWSCXXSortType As String                         'htxtWSCXXSortType
        Private m_strhtxtDivLeftSGWXX As String                          'htxtDivLeftSGWXX
        Private m_strhtxtDivTopSGWXX As String                           'htxtDivTopSGWXX
        Private m_strhtxtDivLeftWSCXX As String                          'htxtDivLeftWSCXX
        Private m_strhtxtDivTopWSCXX As String                           'htxtDivTopWSCXX
        Private m_strhtxtDivLeftBody As String                           'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String                            'htxtDivTopBody

        Private m_strhtxtValueA As String                                'htxtValueA
        Private m_strhtxtValueB As String                                'htxtValueB

        '----------------------------------------------------------------
        'grdWSCXX paramters
        '----------------------------------------------------------------
        Private m_intPageSize_WSCXX As Integer                           'grdWSCXX��ҳ��С
        Private m_intSelectedIndex_WSCXX As Integer                      'grdWSCXX��������
        Private m_intCurrentPageIndex_WSCXX As Integer                   'grdWSCXX��ҳ����

        '----------------------------------------------------------------
        'grdSGWXX paramters
        '----------------------------------------------------------------
        Private m_intPageSize_SGWXX As Integer                            'grdSGWXX��ҳ��С
        Private m_intSelectedIndex_SGWXX As Integer                       'grdSGWXX��������
        Private m_intCurrentPageIndex_SGWXX As Integer                    'grdSGWXX��ҳ����











        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()

            MyBase.New()

            m_strhtxtSGWXXQuery = ""
            m_strhtxtSGWXXRows = ""
            m_strhtxtSGWXXSort = ""
            m_strhtxtSGWXXSortColumnIndex = ""
            m_strhtxtSGWXXSortType = ""

            m_strhtxtWSCXXQuery = ""
            m_strhtxtWSCXXRows = ""
            m_strhtxtWSCXXSort = ""
            m_strhtxtWSCXXSortColumnIndex = ""
            m_strhtxtWSCXXSortType = ""

            m_strhtxtDivLeftSGWXX = ""
            m_strhtxtDivTopSGWXX = ""

            m_strhtxtDivLeftWSCXX = ""
            m_strhtxtDivTopWSCXX = ""

            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""

            m_intPageSize_WSCXX = 100
            m_intSelectedIndex_WSCXX = -1
            m_intCurrentPageIndex_WSCXX = 0

            m_intPageSize_SGWXX = 100
            m_intSelectedIndex_SGWXX = -1
            m_intCurrentPageIndex_SGWXX = 0

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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMFlowBycl)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub











        '----------------------------------------------------------------
        ' htxtSGWXXSort����
        '----------------------------------------------------------------
        Public Property htxtSGWXXSort() As String
            Get
                htxtSGWXXSort = m_strhtxtSGWXXSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtSGWXXSort = Value
                Catch ex As Exception
                    m_strhtxtSGWXXSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtSGWXXRows����
        '----------------------------------------------------------------
        Public Property htxtSGWXXRows() As String
            Get
                htxtSGWXXRows = m_strhtxtSGWXXRows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtSGWXXRows = Value
                Catch ex As Exception
                    m_strhtxtSGWXXRows = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtSGWXXSortColumnIndex����
        '----------------------------------------------------------------
        Public Property htxtSGWXXSortColumnIndex() As String
            Get
                htxtSGWXXSortColumnIndex = m_strhtxtSGWXXSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtSGWXXSortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtSGWXXSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtSGWXXQuery����
        '----------------------------------------------------------------
        Public Property htxtSGWXXQuery() As String
            Get
                htxtSGWXXQuery = m_strhtxtSGWXXQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtSGWXXQuery = Value
                Catch ex As Exception
                    m_strhtxtSGWXXQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtSGWXXSortType����
        '----------------------------------------------------------------
        Public Property htxtSGWXXSortType() As String
            Get
                htxtSGWXXSortType = m_strhtxtSGWXXSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtSGWXXSortType = Value
                Catch ex As Exception
                    m_strhtxtSGWXXSortType = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' htxtWSCXXSort����
        '----------------------------------------------------------------
        Public Property htxtWSCXXSort() As String
            Get
                htxtWSCXXSort = m_strhtxtWSCXXSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtWSCXXSort = Value
                Catch ex As Exception
                    m_strhtxtWSCXXSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtWSCXXRows����
        '----------------------------------------------------------------
        Public Property htxtWSCXXRows() As String
            Get
                htxtWSCXXRows = m_strhtxtWSCXXRows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtWSCXXRows = Value
                Catch ex As Exception
                    m_strhtxtWSCXXRows = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtWSCXXSortColumnIndex����
        '----------------------------------------------------------------
        Public Property htxtWSCXXSortColumnIndex() As String
            Get
                htxtWSCXXSortColumnIndex = m_strhtxtWSCXXSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtWSCXXSortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtWSCXXSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtWSCXXQuery����
        '----------------------------------------------------------------
        Public Property htxtWSCXXQuery() As String
            Get
                htxtWSCXXQuery = m_strhtxtWSCXXQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtWSCXXQuery = Value
                Catch ex As Exception
                    m_strhtxtWSCXXQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtWSCXXSortType����
        '----------------------------------------------------------------
        Public Property htxtWSCXXSortType() As String
            Get
                htxtWSCXXSortType = m_strhtxtWSCXXSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtWSCXXSortType = Value
                Catch ex As Exception
                    m_strhtxtWSCXXSortType = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' htxtDivLeftSGWXX����
        '----------------------------------------------------------------
        Public Property htxtDivLeftSGWXX() As String
            Get
                htxtDivLeftSGWXX = m_strhtxtDivLeftSGWXX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftSGWXX = Value
                Catch ex As Exception
                    m_strhtxtDivLeftSGWXX = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopSGWXX����
        '----------------------------------------------------------------
        Public Property htxtDivTopSGWXX() As String
            Get
                htxtDivTopSGWXX = m_strhtxtDivTopSGWXX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopSGWXX = Value
                Catch ex As Exception
                    m_strhtxtDivTopSGWXX = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivLeftWSCXX����
        '----------------------------------------------------------------
        Public Property htxtDivLeftWSCXX() As String
            Get
                htxtDivLeftWSCXX = m_strhtxtDivLeftWSCXX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftWSCXX = Value
                Catch ex As Exception
                    m_strhtxtDivLeftWSCXX = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopWSCXX����
        '----------------------------------------------------------------
        Public Property htxtDivTopWSCXX() As String
            Get
                htxtDivTopWSCXX = m_strhtxtDivTopWSCXX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopWSCXX = Value
                Catch ex As Exception
                    m_strhtxtDivTopWSCXX = ""
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
        ' htxtValueA����
        '----------------------------------------------------------------
        Public Property htxtValueA() As String
            Get
                htxtValueA = m_strhtxtValueA
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtValueA = Value
                Catch ex As Exception
                    m_strhtxtValueA = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtValueB����
        '----------------------------------------------------------------
        Public Property htxtValueB() As String
            Get
                htxtValueB = m_strhtxtValueB
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtValueB = Value
                Catch ex As Exception
                    m_strhtxtValueB = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' grdWSCXX_PageSize����
        '----------------------------------------------------------------
        Public Property grdWSCXX_PageSize() As Integer
            Get
                grdWSCXX_PageSize = m_intPageSize_WSCXX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_WSCXX = Value
                Catch ex As Exception
                    m_intPageSize_WSCXX = 100
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdWSCXX_SelectedIndex����
        '----------------------------------------------------------------
        Public Property grdWSCXX_SelectedIndex() As Integer
            Get
                grdWSCXX_SelectedIndex = m_intSelectedIndex_WSCXX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_WSCXX = Value
                Catch ex As Exception
                    m_intSelectedIndex_WSCXX = -1
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdWSCXX_CurrentPageIndex����
        '----------------------------------------------------------------
        Public Property grdWSCXX_CurrentPageIndex() As Integer
            Get
                grdWSCXX_CurrentPageIndex = m_intCurrentPageIndex_WSCXX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intCurrentPageIndex_WSCXX = Value
                Catch ex As Exception
                    m_intCurrentPageIndex_WSCXX = -1
                End Try
            End Set
        End Property



        '----------------------------------------------------------------
        ' grdSGWXX_PageSize����
        '----------------------------------------------------------------
        Public Property grdSGWXX_PageSize() As Integer
            Get
                grdSGWXX_PageSize = m_intPageSize_SGWXX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_SGWXX = Value
                Catch ex As Exception
                    m_intPageSize_SGWXX = 100
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdSGWXX_SelectedIndex����
        '----------------------------------------------------------------
        Public Property grdSGWXX_SelectedIndex() As Integer
            Get
                grdSGWXX_SelectedIndex = m_intSelectedIndex_SGWXX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_SGWXX = Value
                Catch ex As Exception
                    m_intSelectedIndex_SGWXX = -1
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdSGWXX_CurrentPageIndex����
        '----------------------------------------------------------------
        Public Property grdSGWXX_CurrentPageIndex() As Integer
            Get
                grdSGWXX_CurrentPageIndex = m_intCurrentPageIndex_SGWXX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intCurrentPageIndex_SGWXX = Value
                Catch ex As Exception
                    m_intCurrentPageIndex_SGWXX = -1
                End Try
            End Set
        End Property

    End Class

End Namespace
