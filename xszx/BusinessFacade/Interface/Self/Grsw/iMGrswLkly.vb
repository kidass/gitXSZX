Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IMGrswLkly
    '
    ' 功能描述： 
    '     grsw_lkly.aspx模块本身恢复现场需要的信息
    '----------------------------------------------------------------
    <Serializable()> Public Class IMGrswLkly
        Implements IDisposable

        '----------------------------------------------------------------
        'hidden textbox
        '----------------------------------------------------------------
        Private m_strhtxtCurrentPage As String               'htxtCurrentPage
        Private m_strhtxtCurrentRow As String                'htxtCurrentRow
        Private m_strhtxtEditMode As String                  'htxtEditMode
        Private m_strhtxtEditType As String                  'htxtEditType

        Private m_strhtxtWWTQuery As String                  'htxtWWTQuery
        Private m_strhtxtWWTRows As String                   'htxtWWTRows
        Private m_strhtxtWWTSort As String                   'htxtWWTSort
        Private m_strhtxtWWTSortColumnIndex As String        'htxtWWTSortColumnIndex
        Private m_strhtxtWWTSortType As String               'htxtWWTSortType

        Private m_strhtxtWSTQuery As String                  'htxtWSTQuery
        Private m_strhtxtWSTRows As String                   'htxtWSTRows
        Private m_strhtxtWSTSort As String                   'htxtWSTSort
        Private m_strhtxtWSTSortColumnIndex As String        'htxtWSTSortColumnIndex
        Private m_strhtxtWSTSortType As String               'htxtWSTSortType

        Private m_strhtxtDivLeftWWT As String                'htxtDivLeftWWT
        Private m_strhtxtDivTopWWT As String                 'htxtDivTopWWT
        Private m_strhtxtDivLeftWST As String                'htxtDivLeftWST
        Private m_strhtxtDivTopWST As String                 'htxtDivTopWST
        Private m_strhtxtDivLeftBody As String               'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String                'htxtDivTopBody

        Private m_strhtxtSessionIdWWTQuery As String         'htxtSessionIdWWTQuery
        Private m_strhtxtSessionIdWSTQuery As String         'htxtSessionIdWSTQuery

        '----------------------------------------------------------------
        'asp:textbox
        '----------------------------------------------------------------
        Private m_strtxtWWTPageIndex As String               'txtWWTPageIndex
        Private m_strtxtWWTPageSize As String                'txtWWTPageSize
        Private m_strtxtWWTSearch_STR As String              'txtWWTSearch_STR
        Private m_strtxtWWTSearch_WTRQMin As String          'txtWWTSearch_WTRQMin
        Private m_strtxtWWTSearch_WTRQMax As String          'txtWWTSearch_WTRQMax
        Private m_strtxtWWTSearch_LYNR As String             'txtWWTSearch_LYNR

        Private m_strtxtWSTPageIndex As String               'txtWSTPageIndex
        Private m_strtxtWSTPageSize As String                'txtWSTPageSize
        Private m_strtxtWSTSearch_WTR As String              'txtWSTSearch_WTR
        Private m_strtxtWSTSearch_WTRQMin As String          'txtWSTSearch_WTRQMin
        Private m_strtxtWSTSearch_WTRQMax As String          'txtWSTSearch_WTRQMax
        Private m_strtxtWSTSearch_LYNR As String             'txtWSTSearch_LYNR

        Private m_strtxtWTR As String                        'txtWTR
        Private m_strtxtLYRQ As String                       'txtLYRQ
        Private m_strtxtSXRQ As String                       'txtSXRQ
        Private m_strtxtZFRQ As String                       'txtZFRQ
        Private m_strtxtSTR As String                        'txtSTR
        Private m_strtextareaLYNR As String                  'textareaLYNR

        '----------------------------------------------------------------
        'asp:datagrid - grdWWT
        '----------------------------------------------------------------
        Private m_intPageSize_grdWWT As Integer
        Private m_intSelectedIndex_grdWWT As Integer
        Private m_intCurrentPageIndex_grdWWT As Integer

        '----------------------------------------------------------------
        'asp:datagrid - grdWST
        '----------------------------------------------------------------
        Private m_intPageSize_grdWST As Integer
        Private m_intSelectedIndex_grdWST As Integer
        Private m_intCurrentPageIndex_grdWST As Integer










        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            'hidden
            m_strhtxtCurrentPage = ""
            m_strhtxtCurrentRow = ""
            m_strhtxtEditMode = ""
            m_strhtxtEditType = ""

            m_strhtxtWWTQuery = ""
            m_strhtxtWWTRows = ""
            m_strhtxtWWTSort = ""
            m_strhtxtWWTSortColumnIndex = ""
            m_strhtxtWWTSortType = ""

            m_strhtxtWSTQuery = ""
            m_strhtxtWSTRows = ""
            m_strhtxtWSTSort = ""
            m_strhtxtWSTSortColumnIndex = ""
            m_strhtxtWSTSortType = ""

            m_strhtxtDivLeftWWT = ""
            m_strhtxtDivTopWWT = ""
            m_strhtxtDivLeftWST = ""
            m_strhtxtDivTopWST = ""
            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""

            m_strhtxtSessionIdWWTQuery = ""
            m_strhtxtSessionIdWSTQuery = ""

            'textbox
            m_strtxtWWTPageIndex = ""
            m_strtxtWWTPageSize = ""
            m_strtxtWWTSearch_STR = ""
            m_strtxtWWTSearch_WTRQMin = ""
            m_strtxtWWTSearch_WTRQMax = ""
            m_strtxtWWTSearch_LYNR = ""

            m_strtxtWSTPageIndex = ""
            m_strtxtWSTPageSize = ""
            m_strtxtWSTSearch_WTR = ""
            m_strtxtWSTSearch_WTRQMin = ""
            m_strtxtWSTSearch_WTRQMax = ""
            m_strtxtWSTSearch_LYNR = ""

            m_strtxtWTR = ""
            m_strtxtSTR = ""
            m_strtxtLYRQ = ""
            m_strtxtSXRQ = ""
            m_strtxtZFRQ = ""
            m_strtextareaLYNR = ""

            'datagrid
            m_intPageSize_grdWWT = 0
            m_intCurrentPageIndex_grdWWT = 0
            m_intSelectedIndex_grdWWT = -1

            'datagrid
            m_intPageSize_grdWST = 0
            m_intCurrentPageIndex_grdWST = 0
            m_intSelectedIndex_grdWST = -1

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
        End Sub

        '----------------------------------------------------------------
        ' 安全释放本身资源
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMGrswLkly)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub












        '----------------------------------------------------------------
        ' htxtCurrentPage属性
        '----------------------------------------------------------------
        Public Property htxtCurrentPage() As String
            Get
                htxtCurrentPage = m_strhtxtCurrentPage
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtCurrentPage = Value
                Catch ex As Exception
                    m_strhtxtCurrentPage = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtCurrentRow属性
        '----------------------------------------------------------------
        Public Property htxtCurrentRow() As String
            Get
                htxtCurrentRow = m_strhtxtCurrentRow
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtCurrentRow = Value
                Catch ex As Exception
                    m_strhtxtCurrentRow = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtEditMode属性
        '----------------------------------------------------------------
        Public Property htxtEditMode() As String
            Get
                htxtEditMode = m_strhtxtEditMode
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtEditMode = Value
                Catch ex As Exception
                    m_strhtxtEditMode = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtEditType属性
        '----------------------------------------------------------------
        Public Property htxtEditType() As String
            Get
                htxtEditType = m_strhtxtEditType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtEditType = Value
                Catch ex As Exception
                    m_strhtxtEditType = ""
                End Try
            End Set
        End Property





        '----------------------------------------------------------------
        ' htxtWWTQuery属性
        '----------------------------------------------------------------
        Public Property htxtWWTQuery() As String
            Get
                htxtWWTQuery = m_strhtxtWWTQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtWWTQuery = Value
                Catch ex As Exception
                    m_strhtxtWWTQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtWWTRows属性
        '----------------------------------------------------------------
        Public Property htxtWWTRows() As String
            Get
                htxtWWTRows = m_strhtxtWWTRows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtWWTRows = Value
                Catch ex As Exception
                    m_strhtxtWWTRows = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtWWTSort属性
        '----------------------------------------------------------------
        Public Property htxtWWTSort() As String
            Get
                htxtWWTSort = m_strhtxtWWTSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtWWTSort = Value
                Catch ex As Exception
                    m_strhtxtWWTSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtWWTSortColumnIndex属性
        '----------------------------------------------------------------
        Public Property htxtWWTSortColumnIndex() As String
            Get
                htxtWWTSortColumnIndex = m_strhtxtWWTSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtWWTSortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtWWTSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtWWTSortType属性
        '----------------------------------------------------------------
        Public Property htxtWWTSortType() As String
            Get
                htxtWWTSortType = m_strhtxtWWTSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtWWTSortType = Value
                Catch ex As Exception
                    m_strhtxtWWTSortType = ""
                End Try
            End Set
        End Property





        '----------------------------------------------------------------
        ' htxtWSTQuery属性
        '----------------------------------------------------------------
        Public Property htxtWSTQuery() As String
            Get
                htxtWSTQuery = m_strhtxtWSTQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtWSTQuery = Value
                Catch ex As Exception
                    m_strhtxtWSTQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtWSTRows属性
        '----------------------------------------------------------------
        Public Property htxtWSTRows() As String
            Get
                htxtWSTRows = m_strhtxtWSTRows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtWSTRows = Value
                Catch ex As Exception
                    m_strhtxtWSTRows = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtWSTSort属性
        '----------------------------------------------------------------
        Public Property htxtWSTSort() As String
            Get
                htxtWSTSort = m_strhtxtWSTSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtWSTSort = Value
                Catch ex As Exception
                    m_strhtxtWSTSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtWSTSortColumnIndex属性
        '----------------------------------------------------------------
        Public Property htxtWSTSortColumnIndex() As String
            Get
                htxtWSTSortColumnIndex = m_strhtxtWSTSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtWSTSortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtWSTSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtWSTSortType属性
        '----------------------------------------------------------------
        Public Property htxtWSTSortType() As String
            Get
                htxtWSTSortType = m_strhtxtWSTSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtWSTSortType = Value
                Catch ex As Exception
                    m_strhtxtWSTSortType = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' htxtDivLeftWWT属性
        '----------------------------------------------------------------
        Public Property htxtDivLeftWWT() As String
            Get
                htxtDivLeftWWT = m_strhtxtDivLeftWWT
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftWWT = Value
                Catch ex As Exception
                    m_strhtxtDivLeftWWT = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopWWT属性
        '----------------------------------------------------------------
        Public Property htxtDivTopWWT() As String
            Get
                htxtDivTopWWT = m_strhtxtDivTopWWT
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopWWT = Value
                Catch ex As Exception
                    m_strhtxtDivTopWWT = ""
                End Try
            End Set
        End Property



        '----------------------------------------------------------------
        ' htxtDivLeftWST属性
        '----------------------------------------------------------------
        Public Property htxtDivLeftWST() As String
            Get
                htxtDivLeftWST = m_strhtxtDivLeftWST
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftWST = Value
                Catch ex As Exception
                    m_strhtxtDivLeftWST = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopWST属性
        '----------------------------------------------------------------
        Public Property htxtDivTopWST() As String
            Get
                htxtDivTopWST = m_strhtxtDivTopWST
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopWST = Value
                Catch ex As Exception
                    m_strhtxtDivTopWST = ""
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
        ' htxtSessionIdWWTQuery属性
        '----------------------------------------------------------------
        Public Property htxtSessionIdWWTQuery() As String
            Get
                htxtSessionIdWWTQuery = m_strhtxtSessionIdWWTQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtSessionIdWWTQuery = Value
                Catch ex As Exception
                    m_strhtxtSessionIdWWTQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtSessionIdWSTQuery属性
        '----------------------------------------------------------------
        Public Property htxtSessionIdWSTQuery() As String
            Get
                htxtSessionIdWSTQuery = m_strhtxtSessionIdWSTQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtSessionIdWSTQuery = Value
                Catch ex As Exception
                    m_strhtxtSessionIdWSTQuery = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' txtWWTPageIndex属性
        '----------------------------------------------------------------
        Public Property txtWWTPageIndex() As String
            Get
                txtWWTPageIndex = m_strtxtWWTPageIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtWWTPageIndex = Value
                Catch ex As Exception
                    m_strtxtWWTPageIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtWWTPageSize属性
        '----------------------------------------------------------------
        Public Property txtWWTPageSize() As String
            Get
                txtWWTPageSize = m_strtxtWWTPageSize
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtWWTPageSize = Value
                Catch ex As Exception
                    m_strtxtWWTPageSize = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtWWTSearch_STR属性
        '----------------------------------------------------------------
        Public Property txtWWTSearch_STR() As String
            Get
                txtWWTSearch_STR = m_strtxtWWTSearch_STR
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtWWTSearch_STR = Value
                Catch ex As Exception
                    m_strtxtWWTSearch_STR = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtWWTSearch_WTRQMin属性
        '----------------------------------------------------------------
        Public Property txtWWTSearch_WTRQMin() As String
            Get
                txtWWTSearch_WTRQMin = m_strtxtWWTSearch_WTRQMin
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtWWTSearch_WTRQMin = Value
                Catch ex As Exception
                    m_strtxtWWTSearch_WTRQMin = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtWWTSearch_WTRQMax属性
        '----------------------------------------------------------------
        Public Property txtWWTSearch_WTRQMax() As String
            Get
                txtWWTSearch_WTRQMax = m_strtxtWWTSearch_WTRQMax
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtWWTSearch_WTRQMax = Value
                Catch ex As Exception
                    m_strtxtWWTSearch_WTRQMax = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtWWTSearch_LYNR属性
        '----------------------------------------------------------------
        Public Property txtWWTSearch_LYNR() As String
            Get
                txtWWTSearch_LYNR = m_strtxtWWTSearch_LYNR
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtWWTSearch_LYNR = Value
                Catch ex As Exception
                    m_strtxtWWTSearch_LYNR = ""
                End Try
            End Set
        End Property





        '----------------------------------------------------------------
        ' txtWSTPageIndex属性
        '----------------------------------------------------------------
        Public Property txtWSTPageIndex() As String
            Get
                txtWSTPageIndex = m_strtxtWSTPageIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtWSTPageIndex = Value
                Catch ex As Exception
                    m_strtxtWSTPageIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtWSTPageSize属性
        '----------------------------------------------------------------
        Public Property txtWSTPageSize() As String
            Get
                txtWSTPageSize = m_strtxtWSTPageSize
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtWSTPageSize = Value
                Catch ex As Exception
                    m_strtxtWSTPageSize = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtWSTSearch_WTR属性
        '----------------------------------------------------------------
        Public Property txtWSTSearch_WTR() As String
            Get
                txtWSTSearch_WTR = m_strtxtWSTSearch_WTR
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtWSTSearch_WTR = Value
                Catch ex As Exception
                    m_strtxtWSTSearch_WTR = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtWSTSearch_WTRQMin属性
        '----------------------------------------------------------------
        Public Property txtWSTSearch_WTRQMin() As String
            Get
                txtWSTSearch_WTRQMin = m_strtxtWSTSearch_WTRQMin
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtWSTSearch_WTRQMin = Value
                Catch ex As Exception
                    m_strtxtWSTSearch_WTRQMin = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtWSTSearch_WTRQMax属性
        '----------------------------------------------------------------
        Public Property txtWSTSearch_WTRQMax() As String
            Get
                txtWSTSearch_WTRQMax = m_strtxtWSTSearch_WTRQMax
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtWSTSearch_WTRQMax = Value
                Catch ex As Exception
                    m_strtxtWSTSearch_WTRQMax = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtWSTSearch_LYNR属性
        '----------------------------------------------------------------
        Public Property txtWSTSearch_LYNR() As String
            Get
                txtWSTSearch_LYNR = m_strtxtWSTSearch_LYNR
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtWSTSearch_LYNR = Value
                Catch ex As Exception
                    m_strtxtWSTSearch_LYNR = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' grdWWT_PageSize属性
        '----------------------------------------------------------------
        Public Property grdWWT_PageSize() As Integer
            Get
                grdWWT_PageSize = m_intPageSize_grdWWT
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_grdWWT = Value
                Catch ex As Exception
                    m_intPageSize_grdWWT = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdWWT_CurrentPageIndex属性
        '----------------------------------------------------------------
        Public Property grdWWT_CurrentPageIndex() As Integer
            Get
                grdWWT_CurrentPageIndex = m_intCurrentPageIndex_grdWWT
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intCurrentPageIndex_grdWWT = Value
                Catch ex As Exception
                    m_intCurrentPageIndex_grdWWT = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdWWT_SelectedIndex属性
        '----------------------------------------------------------------
        Public Property grdWWT_SelectedIndex() As Integer
            Get
                grdWWT_SelectedIndex = m_intSelectedIndex_grdWWT
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_grdWWT = Value
                Catch ex As Exception
                    m_intSelectedIndex_grdWWT = 0
                End Try
            End Set
        End Property



        '----------------------------------------------------------------
        ' grdWST_PageSize属性
        '----------------------------------------------------------------
        Public Property grdWST_PageSize() As Integer
            Get
                grdWST_PageSize = m_intPageSize_grdWST
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_grdWST = Value
                Catch ex As Exception
                    m_intPageSize_grdWST = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdWST_CurrentPageIndex属性
        '----------------------------------------------------------------
        Public Property grdWST_CurrentPageIndex() As Integer
            Get
                grdWST_CurrentPageIndex = m_intCurrentPageIndex_grdWST
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intCurrentPageIndex_grdWST = Value
                Catch ex As Exception
                    m_intCurrentPageIndex_grdWST = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdWST_SelectedIndex属性
        '----------------------------------------------------------------
        Public Property grdWST_SelectedIndex() As Integer
            Get
                grdWST_SelectedIndex = m_intSelectedIndex_grdWST
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_grdWST = Value
                Catch ex As Exception
                    m_intSelectedIndex_grdWST = 0
                End Try
            End Set
        End Property



        '----------------------------------------------------------------
        ' txtWTR属性
        '----------------------------------------------------------------
        Public Property txtWTR() As String
            Get
                txtWTR = m_strtxtWTR
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtWTR = Value
                Catch ex As Exception
                    m_strtxtWTR = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtSTR属性
        '----------------------------------------------------------------
        Public Property txtSTR() As String
            Get
                txtSTR = m_strtxtSTR
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSTR = Value
                Catch ex As Exception
                    m_strtxtSTR = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtLYRQ属性
        '----------------------------------------------------------------
        Public Property txtLYRQ() As String
            Get
                txtLYRQ = m_strtxtLYRQ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtLYRQ = Value
                Catch ex As Exception
                    m_strtxtLYRQ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtSXRQ属性
        '----------------------------------------------------------------
        Public Property txtSXRQ() As String
            Get
                txtSXRQ = m_strtxtSXRQ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSXRQ = Value
                Catch ex As Exception
                    m_strtxtSXRQ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtZFRQ属性
        '----------------------------------------------------------------
        Public Property txtZFRQ() As String
            Get
                txtZFRQ = m_strtxtZFRQ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtZFRQ = Value
                Catch ex As Exception
                    m_strtxtZFRQ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' textareaLYNR属性
        '----------------------------------------------------------------
        Public Property textareaLYNR() As String
            Get
                textareaLYNR = m_strtextareaLYNR
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtextareaLYNR = Value
                Catch ex As Exception
                    m_strtextareaLYNR = ""
                End Try
            End Set
        End Property

    End Class

End Namespace
