Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IMGgxxGgzyRead
    '
    ' ���������� 
    '     ggxx_ggzy_read.aspxģ�鱾��ָ��ֳ���Ҫ����Ϣ
    '----------------------------------------------------------------
    <Serializable()> Public Class IMGgxxGgzyRead
        Implements IDisposable

        '----------------------------------------------------------------
        'hidden textbox
        '----------------------------------------------------------------
        Private m_strhtxtGGZYQuery As String                      'htxtGGZYQuery
        Private m_strhtxtGGZYRows As String                       'htxtGGZYRows
        Private m_strhtxtGGZYSort As String                       'htxtGGZYSort
        Private m_strhtxtGGZYSortColumnIndex As String            'htxtGGZYSortColumnIndex
        Private m_strhtxtGGZYSortType As String                   'htxtGGZYSortType
        Private m_strhtxtDivLeftGGZY As String                    'htxtDivLeftGGZY
        Private m_strhtxtDivTopGGZY As String                     'htxtDivTopGGZY
        Private m_strhtxtDivLeftBody As String                    'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String                     'htxtDivTopBody

        Private m_strhtxtSessionIdQuery As String                 'htxtSessionIdQuery

        '----------------------------------------------------------------
        'asp:textbox
        '----------------------------------------------------------------
        Private m_strtxtGGZYPageIndex As String                  'txtGGZYPageIndex
        Private m_strtxtGGZYPageSize As String                   'txtGGZYPageSize
        Private m_strtxtGGZYSearch_BT As String                  'txtGGZYSearch_BT
        Private m_strtxtGGZYSearch_RQMin As String               'txtGGZYSearch_RQMin
        Private m_strtxtGGZYSearch_RQMax As String               'txtGGZYSearch_RQMax

        Private m_intSelectedIndex_ddlGGZYSearch_FBBS As Integer 'ddlGGZYSearch_FBBS
        Private m_intSelectedIndex_ddlGGZYSearch_YDBS As Integer 'ddlGGZYSearch_YDBS

        '----------------------------------------------------------------
        'asp:datagrid - grdGGZY
        '----------------------------------------------------------------
        Private m_intPageSize_grdGGZY As Integer
        Private m_intSelectedIndex_grdGGZY As Integer
        Private m_intCurrentPageIndex_grdGGZY As Integer

        '----------------------------------------------------------------
        'Microsoft.Web.UI.WebControls.TreeView - tvwLMLIST
        '----------------------------------------------------------------
        Private m_strSelectedNodexIndex_tvwLMLIST As String











        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            'hidden
            m_strhtxtGGZYQuery = ""
            m_strhtxtGGZYRows = ""
            m_strhtxtGGZYSort = ""
            m_strhtxtGGZYSortColumnIndex = ""
            m_strhtxtGGZYSortType = ""
            m_strhtxtDivLeftGGZY = ""
            m_strhtxtDivTopGGZY = ""
            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""

            m_strhtxtSessionIdQuery = ""

            'textbox
            m_strtxtGGZYPageIndex = ""
            m_strtxtGGZYPageSize = ""
            m_strtxtGGZYSearch_BT = ""
            m_strtxtGGZYSearch_RQMin = ""
            m_strtxtGGZYSearch_RQMax = ""
            m_intSelectedIndex_ddlGGZYSearch_FBBS = -1
            m_intSelectedIndex_ddlGGZYSearch_YDBS = -1

            'datagrid
            m_intPageSize_grdGGZY = 0
            m_intCurrentPageIndex_grdGGZY = 0
            m_intSelectedIndex_grdGGZY = -1

            m_strSelectedNodexIndex_tvwLMLIST = ""

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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMGgxxGgzyRead)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub














        '----------------------------------------------------------------
        ' htxtGGZYQuery����
        '----------------------------------------------------------------
        Public Property htxtGGZYQuery() As String
            Get
                htxtGGZYQuery = m_strhtxtGGZYQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtGGZYQuery = Value
                Catch ex As Exception
                    m_strhtxtGGZYQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtGGZYRows����
        '----------------------------------------------------------------
        Public Property htxtGGZYRows() As String
            Get
                htxtGGZYRows = m_strhtxtGGZYRows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtGGZYRows = Value
                Catch ex As Exception
                    m_strhtxtGGZYRows = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtGGZYSort����
        '----------------------------------------------------------------
        Public Property htxtGGZYSort() As String
            Get
                htxtGGZYSort = m_strhtxtGGZYSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtGGZYSort = Value
                Catch ex As Exception
                    m_strhtxtGGZYSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtGGZYSortColumnIndex����
        '----------------------------------------------------------------
        Public Property htxtGGZYSortColumnIndex() As String
            Get
                htxtGGZYSortColumnIndex = m_strhtxtGGZYSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtGGZYSortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtGGZYSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtGGZYSortType����
        '----------------------------------------------------------------
        Public Property htxtGGZYSortType() As String
            Get
                htxtGGZYSortType = m_strhtxtGGZYSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtGGZYSortType = Value
                Catch ex As Exception
                    m_strhtxtGGZYSortType = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' htxtDivLeftGGZY����
        '----------------------------------------------------------------
        Public Property htxtDivLeftGGZY() As String
            Get
                htxtDivLeftGGZY = m_strhtxtDivLeftGGZY
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftGGZY = Value
                Catch ex As Exception
                    m_strhtxtDivLeftGGZY = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopGGZY����
        '----------------------------------------------------------------
        Public Property htxtDivTopGGZY() As String
            Get
                htxtDivTopGGZY = m_strhtxtDivTopGGZY
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopGGZY = Value
                Catch ex As Exception
                    m_strhtxtDivTopGGZY = ""
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
        ' txtGGZYPageIndex����
        '----------------------------------------------------------------
        Public Property txtGGZYPageIndex() As String
            Get
                txtGGZYPageIndex = m_strtxtGGZYPageIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtGGZYPageIndex = Value
                Catch ex As Exception
                    m_strtxtGGZYPageIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtGGZYPageSize����
        '----------------------------------------------------------------
        Public Property txtGGZYPageSize() As String
            Get
                txtGGZYPageSize = m_strtxtGGZYPageSize
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtGGZYPageSize = Value
                Catch ex As Exception
                    m_strtxtGGZYPageSize = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' txtGGZYSearch_BT����
        '----------------------------------------------------------------
        Public Property txtGGZYSearch_BT() As String
            Get
                txtGGZYSearch_BT = m_strtxtGGZYSearch_BT
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtGGZYSearch_BT = Value
                Catch ex As Exception
                    m_strtxtGGZYSearch_BT = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtGGZYSearch_RQMin����
        '----------------------------------------------------------------
        Public Property txtGGZYSearch_RQMin() As String
            Get
                txtGGZYSearch_RQMin = m_strtxtGGZYSearch_RQMin
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtGGZYSearch_RQMin = Value
                Catch ex As Exception
                    m_strtxtGGZYSearch_RQMin = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtGGZYSearch_RQMax����
        '----------------------------------------------------------------
        Public Property txtGGZYSearch_RQMax() As String
            Get
                txtGGZYSearch_RQMax = m_strtxtGGZYSearch_RQMax
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtGGZYSearch_RQMax = Value
                Catch ex As Exception
                    m_strtxtGGZYSearch_RQMax = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' ddlGGZYSearch_FBBS_SelectedIndex����
        '----------------------------------------------------------------
        Public Property ddlGGZYSearch_FBBS_SelectedIndex() As Integer
            Get
                ddlGGZYSearch_FBBS_SelectedIndex = m_intSelectedIndex_ddlGGZYSearch_FBBS
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_ddlGGZYSearch_FBBS = Value
                Catch ex As Exception
                    m_intSelectedIndex_ddlGGZYSearch_FBBS = -1
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' ddlGGZYSearch_YDBS_SelectedIndex����
        '----------------------------------------------------------------
        Public Property ddlGGZYSearch_YDBS_SelectedIndex() As Integer
            Get
                ddlGGZYSearch_YDBS_SelectedIndex = m_intSelectedIndex_ddlGGZYSearch_YDBS
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_ddlGGZYSearch_YDBS = Value
                Catch ex As Exception
                    m_intSelectedIndex_ddlGGZYSearch_YDBS = -1
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' grdGGZYPageSize����
        '----------------------------------------------------------------
        Public Property grdGGZYPageSize() As Integer
            Get
                grdGGZYPageSize = m_intPageSize_grdGGZY
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_grdGGZY = Value
                Catch ex As Exception
                    m_intPageSize_grdGGZY = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdGGZYCurrentPageIndex����
        '----------------------------------------------------------------
        Public Property grdGGZYCurrentPageIndex() As Integer
            Get
                grdGGZYCurrentPageIndex = m_intCurrentPageIndex_grdGGZY
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intCurrentPageIndex_grdGGZY = Value
                Catch ex As Exception
                    m_intCurrentPageIndex_grdGGZY = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdGGZYSelectedIndex����
        '----------------------------------------------------------------
        Public Property grdGGZYSelectedIndex() As Integer
            Get
                grdGGZYSelectedIndex = m_intSelectedIndex_grdGGZY
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_grdGGZY = Value
                Catch ex As Exception
                    m_intSelectedIndex_grdGGZY = -1
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' tvwLMLISTSelectedNodeIndex����
        '----------------------------------------------------------------
        Public Property tvwLMLISTSelectedNodeIndex() As String
            Get
                tvwLMLISTSelectedNodeIndex = m_strSelectedNodexIndex_tvwLMLIST
            End Get
            Set(ByVal Value As String)
                Try
                    m_strSelectedNodexIndex_tvwLMLIST = Value
                Catch ex As Exception
                    m_strSelectedNodexIndex_tvwLMLIST = ""
                End Try
            End Set
        End Property

    End Class

End Namespace
