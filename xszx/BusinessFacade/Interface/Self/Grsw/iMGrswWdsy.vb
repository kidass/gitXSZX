Imports System.Runtime.Serialization
Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IMGrswWdsy
    '
    ' 功能描述： 
    '     grsw_wdsy.aspx模块本身恢复现场需要的信息
    '----------------------------------------------------------------
    <Serializable()> Public Class IMGrswWdsy
        Implements IDisposable

        '----------------------------------------------------------------
        'hidden textbox
        '----------------------------------------------------------------
        Private m_strhtxtSessionIdQueryFILE As String     'htxtSessionIdQueryFILE
        Private m_strhtxtPageCloseWindow As String        'htxtPageCloseWindow

        Private m_strhtxtFILEQuery As String              'htxtFILEQuery
        Private m_strhtxtFILERows As String               'htxtFILERows
        Private m_strhtxtFILESort As String               'htxtFILESort
        Private m_strhtxtFILESortColumnIndex As String    'htxtFILESortColumnIndex
        Private m_strhtxtFILESortType As String           'htxtFILESortType

        Private m_strhtxtTASKQuery As String              'htxtTASKQuery
        Private m_strhtxtTASKRows As String               'htxtTASKRows
        Private m_strhtxtTASKSort As String               'htxtTASKSort
        Private m_strhtxtTASKSortColumnIndex As String    'htxtTASKSortColumnIndex
        Private m_strhtxtTASKSortType As String           'htxtTASKSortType

        Private m_strhtxtDivLeftFILE As String            'htxtDivLeftFILE
        Private m_strhtxtDivTopFILE As String             'htxtDivTopFILE
        Private m_strhtxtDivLeftTASK As String            'htxtDivLeftTASK
        Private m_strhtxtDivTopTASK As String             'htxtDivTopTASK
        Private m_strhtxtDivLeftBody As String            'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String             'htxtDivTopBody

        '----------------------------------------------------------------
        'asp:textbox
        '----------------------------------------------------------------
        Private m_strtxtFILEPageIndex As String          'txtFILEPageIndex
        Private m_strtxtFILEPageSize As String           'txtFILEPageSize

        Private m_strFILESearch_WJLX As String           'txtFILESearch_WJLX
        Private m_strFILESearch_WJRQMin As String        'txtFILESearch_WJRQMin
        Private m_strFILESearch_WJRQMax As String        'txtFILESearch_WJRQMax
        Private m_strFILESearch_WJBT As String           'txtFILESearch_WJBT
        Private m_strFILESearch_WJZH As String           'txtFILESearch_WJZH
        Private m_intSelectedIndex_ddlGWJKSearch_WJLX As Integer 'ddlGWJKSearch_WJLX


        '----------------------------------------------------------------
        'asp:datagrid - grdFILE
        '----------------------------------------------------------------
        Private m_intPageSize_grdFILE As Integer
        Private m_intSelectedIndex_grdFILE As Integer
        Private m_intCurrentPageIndex_grdFILE As Integer

        '----------------------------------------------------------------
        'asp:datagrid - grdTASK
        '----------------------------------------------------------------
        Private m_intPageSize_grdTASK As Integer
        Private m_intSelectedIndex_grdTASK As Integer
        Private m_intCurrentPageIndex_grdTASK As Integer

        '----------------------------------------------------------------
        'treeview - tvwTASK
        '----------------------------------------------------------------
        Private m_strSelectedNodeIndex_tvwTASK As String
        Private m_blnExpanded_tvwTASK() As Boolean












        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            m_strhtxtSessionIdQueryFILE = ""
            m_strhtxtPageCloseWindow = ""

            'hidden
            m_strhtxtFILEQuery = ""
            m_strhtxtFILERows = ""
            m_strhtxtFILESort = ""
            m_strhtxtFILESortColumnIndex = ""
            m_strhtxtFILESortType = ""

            m_strhtxtTASKQuery = ""
            m_strhtxtTASKRows = ""
            m_strhtxtTASKSort = ""
            m_strhtxtTASKSortColumnIndex = ""
            m_strhtxtTASKSortType = ""

            m_strhtxtDivLeftFILE = ""
            m_strhtxtDivTopFILE = ""

            m_strhtxtDivLeftTASK = ""
            m_strhtxtDivTopTASK = ""

            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""

            'textbox
            m_strtxtFILEPageIndex = ""
            m_strtxtFILEPageSize = ""

            m_strFILESearch_WJLX = ""
            m_strFILESearch_WJRQMin = ""
            m_strFILESearch_WJRQMax = ""
            m_strFILESearch_WJBT = ""
            m_strFILESearch_WJZH = ""
            m_intSelectedIndex_ddlGWJKSearch_WJLX = -1

            'datagrid
            m_intPageSize_grdFILE = 0
            m_intCurrentPageIndex_grdFILE = 0
            m_intSelectedIndex_grdFILE = -1

            'datagrid
            m_intPageSize_grdTASK = 0
            m_intCurrentPageIndex_grdTASK = 0
            m_intSelectedIndex_grdTASK = -1

            'treeview
            m_strSelectedNodeIndex_tvwTASK = ""
            m_blnExpanded_tvwTASK = Nothing



        End Sub

        '----------------------------------------------------------------
        ' 虚拟析构函数
        '----------------------------------------------------------------
        Public Sub Dispose() Implements System.IDisposable.Dispose
            Dispose(True)
        End Sub

        '----------------------------------------------------------------
        ' 释放本身资源
        '----------------------------------------------------------------
        Protected Sub Dispose(ByVal disposing As Boolean)
            If (Not disposing) Then
                Exit Sub
            End If
            If Not (m_blnExpanded_tvwTASK Is Nothing) Then
                m_blnExpanded_tvwTASK = Nothing
            End If
        End Sub

        '----------------------------------------------------------------
        ' 安全释放本身资源
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMGrswWdsy)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub













        '----------------------------------------------------------------
        ' htxtSessionIdQueryFILE属性
        '----------------------------------------------------------------
        Public Property htxtSessionIdQueryFILE() As String
            Get
                htxtSessionIdQueryFILE = m_strhtxtSessionIdQueryFILE
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtSessionIdQueryFILE = Value
                Catch ex As Exception
                    m_strhtxtSessionIdQueryFILE = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtPageCloseWindow属性
        '----------------------------------------------------------------
        Public Property htxtPageCloseWindow() As String
            Get
                htxtPageCloseWindow = m_strhtxtPageCloseWindow
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtPageCloseWindow = Value
                Catch ex As Exception
                    m_strhtxtPageCloseWindow = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' htxtFILEQuery属性
        '----------------------------------------------------------------
        Public Property htxtFILEQuery() As String
            Get
                htxtFILEQuery = m_strhtxtFILEQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtFILEQuery = Value
                Catch ex As Exception
                    m_strhtxtFILEQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtFILERows属性
        '----------------------------------------------------------------
        Public Property htxtFILERows() As String
            Get
                htxtFILERows = m_strhtxtFILERows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtFILERows = Value
                Catch ex As Exception
                    m_strhtxtFILERows = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtFILESort属性
        '----------------------------------------------------------------
        Public Property htxtFILESort() As String
            Get
                htxtFILESort = m_strhtxtFILESort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtFILESort = Value
                Catch ex As Exception
                    m_strhtxtFILESort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtFILESortColumnIndex属性
        '----------------------------------------------------------------
        Public Property htxtFILESortColumnIndex() As String
            Get
                htxtFILESortColumnIndex = m_strhtxtFILESortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtFILESortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtFILESortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtFILESortType属性
        '----------------------------------------------------------------
        Public Property htxtFILESortType() As String
            Get
                htxtFILESortType = m_strhtxtFILESortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtFILESortType = Value
                Catch ex As Exception
                    m_strhtxtFILESortType = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' htxtTASKQuery属性
        '----------------------------------------------------------------
        Public Property htxtTASKQuery() As String
            Get
                htxtTASKQuery = m_strhtxtTASKQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtTASKQuery = Value
                Catch ex As Exception
                    m_strhtxtTASKQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtTASKRows属性
        '----------------------------------------------------------------
        Public Property htxtTASKRows() As String
            Get
                htxtTASKRows = m_strhtxtTASKRows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtTASKRows = Value
                Catch ex As Exception
                    m_strhtxtTASKRows = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtTASKSort属性
        '----------------------------------------------------------------
        Public Property htxtTASKSort() As String
            Get
                htxtTASKSort = m_strhtxtTASKSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtTASKSort = Value
                Catch ex As Exception
                    m_strhtxtTASKSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtTASKSortColumnIndex属性
        '----------------------------------------------------------------
        Public Property htxtTASKSortColumnIndex() As String
            Get
                htxtTASKSortColumnIndex = m_strhtxtTASKSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtTASKSortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtTASKSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtTASKSortType属性
        '----------------------------------------------------------------
        Public Property htxtTASKSortType() As String
            Get
                htxtTASKSortType = m_strhtxtTASKSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtTASKSortType = Value
                Catch ex As Exception
                    m_strhtxtTASKSortType = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' htxtDivLeftFILE属性
        '----------------------------------------------------------------
        Public Property htxtDivLeftFILE() As String
            Get
                htxtDivLeftFILE = m_strhtxtDivLeftFILE
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftFILE = Value
                Catch ex As Exception
                    m_strhtxtDivLeftFILE = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopFILE属性
        '----------------------------------------------------------------
        Public Property htxtDivTopFILE() As String
            Get
                htxtDivTopFILE = m_strhtxtDivTopFILE
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopFILE = Value
                Catch ex As Exception
                    m_strhtxtDivTopFILE = ""
                End Try
            End Set
        End Property



        '----------------------------------------------------------------
        ' htxtDivLeftTASK属性
        '----------------------------------------------------------------
        Public Property htxtDivLeftTASK() As String
            Get
                htxtDivLeftTASK = m_strhtxtDivLeftTASK
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftTASK = Value
                Catch ex As Exception
                    m_strhtxtDivLeftTASK = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopTASK属性
        '----------------------------------------------------------------
        Public Property htxtDivTopTASK() As String
            Get
                htxtDivTopTASK = m_strhtxtDivTopTASK
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopTASK = Value
                Catch ex As Exception
                    m_strhtxtDivTopTASK = ""
                End Try
            End Set
        End Property



        '----------------------------------------------------------------
        ' htxtDivLeftBody属性
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
        ' htxtDivTopBody属性
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
        ' ddlGWJKSearch_WJLX_SelectedIndex属性
        '----------------------------------------------------------------
        Public Property ddlGWJKSearch_WJLX_SelectedIndex() As Integer
            Get
                ddlGWJKSearch_WJLX_SelectedIndex = m_intSelectedIndex_ddlGWJKSearch_WJLX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_ddlGWJKSearch_WJLX = Value
                Catch ex As Exception
                    m_intSelectedIndex_ddlGWJKSearch_WJLX = -1
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtFILEPageIndex属性
        '----------------------------------------------------------------
        Public Property txtFILEPageIndex() As String
            Get
                txtFILEPageIndex = m_strtxtFILEPageIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtFILEPageIndex = Value
                Catch ex As Exception
                    m_strtxtFILEPageIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtFILEPageSize属性
        '----------------------------------------------------------------
        Public Property txtFILEPageSize() As String
            Get
                txtFILEPageSize = m_strtxtFILEPageSize
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtFILEPageSize = Value
                Catch ex As Exception
                    m_strtxtFILEPageSize = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' txtFILESearch_WJLX属性
        '----------------------------------------------------------------
        Public Property txtFILESearch_WJLX() As String
            Get
                txtFILESearch_WJLX = m_strFILESearch_WJLX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strFILESearch_WJLX = Value
                Catch ex As Exception
                    m_strFILESearch_WJLX = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtFILESearch_WJRQMin属性
        '----------------------------------------------------------------
        Public Property txtFILESearch_WJRQMin() As String
            Get
                txtFILESearch_WJRQMin = m_strFILESearch_WJRQMin
            End Get
            Set(ByVal Value As String)
                Try
                    m_strFILESearch_WJRQMin = Value
                Catch ex As Exception
                    m_strFILESearch_WJRQMin = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtFILESearch_WJRQMax属性
        '----------------------------------------------------------------
        Public Property txtFILESearch_WJRQMax() As String
            Get
                txtFILESearch_WJRQMax = m_strFILESearch_WJRQMax
            End Get
            Set(ByVal Value As String)
                Try
                    m_strFILESearch_WJRQMax = Value
                Catch ex As Exception
                    m_strFILESearch_WJRQMax = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtFILESearch_WJBT属性
        '----------------------------------------------------------------
        Public Property txtFILESearch_WJBT() As String
            Get
                txtFILESearch_WJBT = m_strFILESearch_WJBT
            End Get
            Set(ByVal Value As String)
                Try
                    m_strFILESearch_WJBT = Value
                Catch ex As Exception
                    m_strFILESearch_WJBT = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtFILESearch_WJZH属性
        '----------------------------------------------------------------
        Public Property txtFILESearch_WJZH() As String
            Get
                txtFILESearch_WJZH = m_strFILESearch_WJZH
            End Get
            Set(ByVal Value As String)
                Try
                    m_strFILESearch_WJZH = Value
                Catch ex As Exception
                    m_strFILESearch_WJZH = ""
                End Try
            End Set
        End Property



        '----------------------------------------------------------------
        ' grdFILE_PageSize属性
        '----------------------------------------------------------------
        Public Property grdFILE_PageSize() As Integer
            Get
                grdFILE_PageSize = m_intPageSize_grdFILE
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_grdFILE = Value
                Catch ex As Exception
                    m_intPageSize_grdFILE = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdFILE_CurrentPageIndex属性
        '----------------------------------------------------------------
        Public Property grdFILE_CurrentPageIndex() As Integer
            Get
                grdFILE_CurrentPageIndex = m_intCurrentPageIndex_grdFILE
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intCurrentPageIndex_grdFILE = Value
                Catch ex As Exception
                    m_intCurrentPageIndex_grdFILE = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdFILE_SelectedIndex属性
        '----------------------------------------------------------------
        Public Property grdFILE_SelectedIndex() As Integer
            Get
                grdFILE_SelectedIndex = m_intSelectedIndex_grdFILE
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_grdFILE = Value
                Catch ex As Exception
                    m_intSelectedIndex_grdFILE = 0
                End Try
            End Set
        End Property



        '----------------------------------------------------------------
        ' grdTASK_PageSize属性
        '----------------------------------------------------------------
        Public Property grdTASK_PageSize() As Integer
            Get
                grdTASK_PageSize = m_intPageSize_grdTASK
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_grdTASK = Value
                Catch ex As Exception
                    m_intPageSize_grdTASK = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdTASK_CurrentPageIndex属性
        '----------------------------------------------------------------
        Public Property grdTASK_CurrentPageIndex() As Integer
            Get
                grdTASK_CurrentPageIndex = m_intCurrentPageIndex_grdTASK
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intCurrentPageIndex_grdTASK = Value
                Catch ex As Exception
                    m_intCurrentPageIndex_grdTASK = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdTASK_SelectedIndex属性
        '----------------------------------------------------------------
        Public Property grdTASK_SelectedIndex() As Integer
            Get
                grdTASK_SelectedIndex = m_intSelectedIndex_grdTASK
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_grdTASK = Value
                Catch ex As Exception
                    m_intSelectedIndex_grdTASK = 0
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' tvwTASK_SelectedNodeIndex属性
        '----------------------------------------------------------------
        Public Property tvwTASK_SelectedNodeIndex() As String
            Get
                tvwTASK_SelectedNodeIndex = m_strSelectedNodeIndex_tvwTASK
            End Get
            Set(ByVal Value As String)
                Try
                    m_strSelectedNodeIndex_tvwTASK = Value
                Catch ex As Exception
                    m_strSelectedNodeIndex_tvwTASK = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' tvwTASK_Expanded属性
        '----------------------------------------------------------------
        Public Property tvwTASK_Expanded() As Boolean()
            Get
                tvwTASK_Expanded = m_blnExpanded_tvwTASK
            End Get
            Set(ByVal Value As Boolean())
                Try
                    m_blnExpanded_tvwTASK = Value
                Catch ex As Exception
                    m_blnExpanded_tvwTASK = Nothing
                End Try
            End Set
        End Property

    End Class

End Namespace
