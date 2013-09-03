Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IMGrswCyyj
    '
    ' 功能描述： 
    '     grsw_cyyj.aspx模块本身恢复现场需要的信息
    '----------------------------------------------------------------
    <Serializable()> Public Class IMGrswCyyj
        Implements IDisposable

        '----------------------------------------------------------------
        'hidden textbox
        '----------------------------------------------------------------
        Private m_strhtxtCurrentPage As String               'htxtCurrentPage
        Private m_strhtxtCurrentRow As String                'htxtCurrentRow
        Private m_strhtxtEditMode As String                  'htxtEditMode
        Private m_strhtxtEditType As String                  'htxtEditType

        Private m_strhtxtCYYJQuery As String                 'htxtCYYJQuery
        Private m_strhtxtCYYJRows As String                  'htxtCYYJRows
        Private m_strhtxtCYYJSort As String                  'htxtCYYJSort
        Private m_strhtxtCYYJSortColumnIndex As String       'htxtCYYJSortColumnIndex
        Private m_strhtxtCYYJSortType As String              'htxtCYYJSortType

        Private m_strhtxtDivLeftCYYJ As String               'htxtDivLeftCYYJ
        Private m_strhtxtDivTopCYYJ As String                'htxtDivTopCYYJ
        Private m_strhtxtDivLeftBody As String               'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String                'htxtDivTopBody

        Private m_strhtxtSessionIdCYYJQuery As String        'htxtSessionIdCYYJQuery

        '----------------------------------------------------------------
        'asp:textbox
        '----------------------------------------------------------------
        Private m_strtxtCYYJPageIndex As String               'txtCYYJPageIndex
        Private m_strtxtCYYJPageSize As String                'txtCYYJPageSize
        Private m_strtxtCYYJSearch_YJNR As String             'txtCYYJSearch_YJNR

        Private m_strtxtYJNR As String                        'txtYJNR

        '----------------------------------------------------------------
        'asp:datagrid - grdCYYJ
        '----------------------------------------------------------------
        Private m_intPageSize_grdCYYJ As Integer
        Private m_intSelectedIndex_grdCYYJ As Integer
        Private m_intCurrentPageIndex_grdCYYJ As Integer











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

            m_strhtxtCYYJQuery = ""
            m_strhtxtCYYJRows = ""
            m_strhtxtCYYJSort = ""
            m_strhtxtCYYJSortColumnIndex = ""
            m_strhtxtCYYJSortType = ""

            m_strhtxtDivLeftCYYJ = ""
            m_strhtxtDivTopCYYJ = ""
            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""

            m_strhtxtSessionIdCYYJQuery = ""

            'textbox
            m_strtxtCYYJPageIndex = ""
            m_strtxtCYYJPageSize = ""
            m_strtxtCYYJSearch_YJNR = ""

            m_strtxtYJNR = ""

            'datagrid
            m_intPageSize_grdCYYJ = 0
            m_intCurrentPageIndex_grdCYYJ = 0
            m_intSelectedIndex_grdCYYJ = -1

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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMGrswCyyj)
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
        ' htxtCYYJQuery属性
        '----------------------------------------------------------------
        Public Property htxtCYYJQuery() As String
            Get
                htxtCYYJQuery = m_strhtxtCYYJQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtCYYJQuery = Value
                Catch ex As Exception
                    m_strhtxtCYYJQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtCYYJRows属性
        '----------------------------------------------------------------
        Public Property htxtCYYJRows() As String
            Get
                htxtCYYJRows = m_strhtxtCYYJRows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtCYYJRows = Value
                Catch ex As Exception
                    m_strhtxtCYYJRows = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtCYYJSort属性
        '----------------------------------------------------------------
        Public Property htxtCYYJSort() As String
            Get
                htxtCYYJSort = m_strhtxtCYYJSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtCYYJSort = Value
                Catch ex As Exception
                    m_strhtxtCYYJSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtCYYJSortColumnIndex属性
        '----------------------------------------------------------------
        Public Property htxtCYYJSortColumnIndex() As String
            Get
                htxtCYYJSortColumnIndex = m_strhtxtCYYJSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtCYYJSortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtCYYJSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtCYYJSortType属性
        '----------------------------------------------------------------
        Public Property htxtCYYJSortType() As String
            Get
                htxtCYYJSortType = m_strhtxtCYYJSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtCYYJSortType = Value
                Catch ex As Exception
                    m_strhtxtCYYJSortType = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' htxtDivLeftCYYJ属性
        '----------------------------------------------------------------
        Public Property htxtDivLeftCYYJ() As String
            Get
                htxtDivLeftCYYJ = m_strhtxtDivLeftCYYJ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftCYYJ = Value
                Catch ex As Exception
                    m_strhtxtDivLeftCYYJ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopCYYJ属性
        '----------------------------------------------------------------
        Public Property htxtDivTopCYYJ() As String
            Get
                htxtDivTopCYYJ = m_strhtxtDivTopCYYJ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopCYYJ = Value
                Catch ex As Exception
                    m_strhtxtDivTopCYYJ = ""
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
        ' htxtSessionIdCYYJQuery属性
        '----------------------------------------------------------------
        Public Property htxtSessionIdCYYJQuery() As String
            Get
                htxtSessionIdCYYJQuery = m_strhtxtSessionIdCYYJQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtSessionIdCYYJQuery = Value
                Catch ex As Exception
                    m_strhtxtSessionIdCYYJQuery = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' txtCYYJPageIndex属性
        '----------------------------------------------------------------
        Public Property txtCYYJPageIndex() As String
            Get
                txtCYYJPageIndex = m_strtxtCYYJPageIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtCYYJPageIndex = Value
                Catch ex As Exception
                    m_strtxtCYYJPageIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtCYYJPageSize属性
        '----------------------------------------------------------------
        Public Property txtCYYJPageSize() As String
            Get
                txtCYYJPageSize = m_strtxtCYYJPageSize
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtCYYJPageSize = Value
                Catch ex As Exception
                    m_strtxtCYYJPageSize = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtCYYJSearch_YJNR属性
        '----------------------------------------------------------------
        Public Property txtCYYJSearch_YJNR() As String
            Get
                txtCYYJSearch_YJNR = m_strtxtCYYJSearch_YJNR
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtCYYJSearch_YJNR = Value
                Catch ex As Exception
                    m_strtxtCYYJSearch_YJNR = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' grdCYYJ_PageSize属性
        '----------------------------------------------------------------
        Public Property grdCYYJ_PageSize() As Integer
            Get
                grdCYYJ_PageSize = m_intPageSize_grdCYYJ
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_grdCYYJ = Value
                Catch ex As Exception
                    m_intPageSize_grdCYYJ = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdCYYJ_CurrentPageIndex属性
        '----------------------------------------------------------------
        Public Property grdCYYJ_CurrentPageIndex() As Integer
            Get
                grdCYYJ_CurrentPageIndex = m_intCurrentPageIndex_grdCYYJ
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intCurrentPageIndex_grdCYYJ = Value
                Catch ex As Exception
                    m_intCurrentPageIndex_grdCYYJ = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdCYYJ_SelectedIndex属性
        '----------------------------------------------------------------
        Public Property grdCYYJ_SelectedIndex() As Integer
            Get
                grdCYYJ_SelectedIndex = m_intSelectedIndex_grdCYYJ
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_grdCYYJ = Value
                Catch ex As Exception
                    m_intSelectedIndex_grdCYYJ = 0
                End Try
            End Set
        End Property



        '----------------------------------------------------------------
        ' txtYJNR属性
        '----------------------------------------------------------------
        Public Property txtYJNR() As String
            Get
                txtYJNR = m_strtxtYJNR
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtYJNR = Value
                Catch ex As Exception
                    m_strtxtYJNR = ""
                End Try
            End Set
        End Property

    End Class

End Namespace
