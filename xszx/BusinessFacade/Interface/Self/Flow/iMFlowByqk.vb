Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IMFlowByqk
    '
    ' ���������� 
    '     flow_byqk.aspxģ�鱾��ָ��ֳ���Ҫ����Ϣ
    '----------------------------------------------------------------
    <Serializable()> Public Class IMFlowByqk
        Implements IDisposable

        '----------------------------------------------------------------
        'hidden textbox
        '----------------------------------------------------------------
        Private m_strhtxtBYQKQuery As String                      'htxtBYQKQuery
        Private m_strhtxtBYQKRows As String                       'htxtBYQKRows
        Private m_strhtxtBYQKSort As String                       'htxtBYQKSort
        Private m_strhtxtBYQKSortColumnIndex As String            'htxtBYQKSortColumnIndex
        Private m_strhtxtBYQKSortType As String                   'htxtBYQKSortType
        Private m_strhtxtDivLeftBYQK As String                    'htxtDivLeftBYQK
        Private m_strhtxtDivTopBYQK As String                     'htxtDivTopBYQK
        Private m_strhtxtDivLeftBody As String                    'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String                     'htxtDivTopBody

        Private m_strhtxtSessionIdQuery As String                 'htxtSessionIdQuery

        '----------------------------------------------------------------
        'asp:textbox
        '----------------------------------------------------------------
        Private m_strtxtBYQKPageIndex As String                  'txtBYQKPageIndex
        Private m_strtxtBYQKPageSize As String                   'txtBYQKPageSize
        Private m_strtxtBYQKSearch_FSR As String                 'txtBYQKSearch_FSR
        Private m_strtxtBYQKSearch_JSR As String                 'txtBYQKSearch_JSR
        Private m_strtxtBYQKSearch_BLSY As String                'txtBYQKSearch_BLSY
        Private m_strtxtBYQKSearch_WCRQMin As String             'txtBYQKSearch_WCRQMin
        Private m_strtxtBYQKSearch_WCRQMax As String             'txtBYQKSearch_WCRQMax

        '----------------------------------------------------------------
        'asp:datagrid - grdBYQK
        '----------------------------------------------------------------
        Private m_intPageSize_grdBYQK As Integer
        Private m_intSelectedIndex_grdBYQK As Integer
        Private m_intCurrentPageIndex_grdBYQK As Integer













        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            'hidden
            m_strhtxtBYQKQuery = ""
            m_strhtxtBYQKRows = ""
            m_strhtxtBYQKSort = ""
            m_strhtxtBYQKSortColumnIndex = ""
            m_strhtxtBYQKSortType = ""
            m_strhtxtDivLeftBYQK = ""
            m_strhtxtDivTopBYQK = ""
            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""

            m_strhtxtSessionIdQuery = ""

            'textbox
            m_strtxtBYQKPageIndex = ""
            m_strtxtBYQKPageSize = ""
            m_strtxtBYQKSearch_FSR = ""
            m_strtxtBYQKSearch_JSR = ""
            m_strtxtBYQKSearch_BLSY = ""
            m_strtxtBYQKSearch_WCRQMin = ""
            m_strtxtBYQKSearch_WCRQMax = ""

            'datagrid
            m_intPageSize_grdBYQK = 0
            m_intCurrentPageIndex_grdBYQK = 0
            m_intSelectedIndex_grdBYQK = -1

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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMFlowByqk)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub













        '----------------------------------------------------------------
        ' htxtBYQKQuery����
        '----------------------------------------------------------------
        Public Property htxtBYQKQuery() As String
            Get
                htxtBYQKQuery = m_strhtxtBYQKQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtBYQKQuery = Value
                Catch ex As Exception
                    m_strhtxtBYQKQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtBYQKRows����
        '----------------------------------------------------------------
        Public Property htxtBYQKRows() As String
            Get
                htxtBYQKRows = m_strhtxtBYQKRows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtBYQKRows = Value
                Catch ex As Exception
                    m_strhtxtBYQKRows = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtBYQKSort����
        '----------------------------------------------------------------
        Public Property htxtBYQKSort() As String
            Get
                htxtBYQKSort = m_strhtxtBYQKSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtBYQKSort = Value
                Catch ex As Exception
                    m_strhtxtBYQKSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtBYQKSortColumnIndex����
        '----------------------------------------------------------------
        Public Property htxtBYQKSortColumnIndex() As String
            Get
                htxtBYQKSortColumnIndex = m_strhtxtBYQKSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtBYQKSortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtBYQKSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtBYQKSortType����
        '----------------------------------------------------------------
        Public Property htxtBYQKSortType() As String
            Get
                htxtBYQKSortType = m_strhtxtBYQKSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtBYQKSortType = Value
                Catch ex As Exception
                    m_strhtxtBYQKSortType = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' htxtDivLeftBYQK����
        '----------------------------------------------------------------
        Public Property htxtDivLeftBYQK() As String
            Get
                htxtDivLeftBYQK = m_strhtxtDivLeftBYQK
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftBYQK = Value
                Catch ex As Exception
                    m_strhtxtDivLeftBYQK = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopBYQK����
        '----------------------------------------------------------------
        Public Property htxtDivTopBYQK() As String
            Get
                htxtDivTopBYQK = m_strhtxtDivTopBYQK
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopBYQK = Value
                Catch ex As Exception
                    m_strhtxtDivTopBYQK = ""
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
        ' htxtSessionIdQuery����
        '----------------------------------------------------------------
        Public Property htxtSessionIdQuery() As String
            Get
                htxtSessionIdQuery = m_strhtxtSessionIdQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtSessionIdQuery = Value
                Catch ex As Exception
                    m_strhtxtSessionIdQuery = ""
                End Try
            End Set
        End Property





        '----------------------------------------------------------------
        ' txtBYQKPageIndex����
        '----------------------------------------------------------------
        Public Property txtBYQKPageIndex() As String
            Get
                txtBYQKPageIndex = m_strtxtBYQKPageIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtBYQKPageIndex = Value
                Catch ex As Exception
                    m_strtxtBYQKPageIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtBYQKPageSize����
        '----------------------------------------------------------------
        Public Property txtBYQKPageSize() As String
            Get
                txtBYQKPageSize = m_strtxtBYQKPageSize
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtBYQKPageSize = Value
                Catch ex As Exception
                    m_strtxtBYQKPageSize = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' txtBYQKSearch_FSR����
        '----------------------------------------------------------------
        Public Property txtBYQKSearch_FSR() As String
            Get
                txtBYQKSearch_FSR = m_strtxtBYQKSearch_FSR
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtBYQKSearch_FSR = Value
                Catch ex As Exception
                    m_strtxtBYQKSearch_FSR = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtBYQKSearch_JSR����
        '----------------------------------------------------------------
        Public Property txtBYQKSearch_JSR() As String
            Get
                txtBYQKSearch_JSR = m_strtxtBYQKSearch_JSR
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtBYQKSearch_JSR = Value
                Catch ex As Exception
                    m_strtxtBYQKSearch_JSR = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtBYQKSearch_BLSY����
        '----------------------------------------------------------------
        Public Property txtBYQKSearch_BLSY() As String
            Get
                txtBYQKSearch_BLSY = m_strtxtBYQKSearch_BLSY
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtBYQKSearch_BLSY = Value
                Catch ex As Exception
                    m_strtxtBYQKSearch_BLSY = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtBYQKSearch_WCRQMin����
        '----------------------------------------------------------------
        Public Property txtBYQKSearch_WCRQMin() As String
            Get
                txtBYQKSearch_WCRQMin = m_strtxtBYQKSearch_WCRQMin
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtBYQKSearch_WCRQMin = Value
                Catch ex As Exception
                    m_strtxtBYQKSearch_WCRQMin = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtBYQKSearch_WCRQMax����
        '----------------------------------------------------------------
        Public Property txtBYQKSearch_WCRQMax() As String
            Get
                txtBYQKSearch_WCRQMax = m_strtxtBYQKSearch_WCRQMax
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtBYQKSearch_WCRQMax = Value
                Catch ex As Exception
                    m_strtxtBYQKSearch_WCRQMax = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' grdBYQKPageSize����
        '----------------------------------------------------------------
        Public Property grdBYQKPageSize() As Integer
            Get
                grdBYQKPageSize = m_intPageSize_grdBYQK
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_grdBYQK = Value
                Catch ex As Exception
                    m_intPageSize_grdBYQK = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdBYQKCurrentPageIndex����
        '----------------------------------------------------------------
        Public Property grdBYQKCurrentPageIndex() As Integer
            Get
                grdBYQKCurrentPageIndex = m_intCurrentPageIndex_grdBYQK
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intCurrentPageIndex_grdBYQK = Value
                Catch ex As Exception
                    m_intCurrentPageIndex_grdBYQK = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdBYQKSelectedIndex����
        '----------------------------------------------------------------
        Public Property grdBYQKSelectedIndex() As Integer
            Get
                grdBYQKSelectedIndex = m_intSelectedIndex_grdBYQK
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_grdBYQK = Value
                Catch ex As Exception
                    m_intSelectedIndex_grdBYQK = 0
                End Try
            End Set
        End Property

    End Class

End Namespace
