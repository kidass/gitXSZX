Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IMGrswWtxl
    '
    ' 功能描述： 
    '     grsw_tongxinlu.aspx模块本身恢复现场需要的信息
    '----------------------------------------------------------------
    <Serializable()> Public Class IMGrswWtxl
        Implements IDisposable

        '----------------------------------------------------------------
        'hidden textbox
        '----------------------------------------------------------------
        Private m_strhtxtEditMode As String                      'htxtEditMode
        Private m_strhtxtEditType As String                      'htxtEditType
        Private m_strhtxtCurrentPage As String                   'htxtCurrentPage
        Private m_strhtxtCurrentRow As String                    'htxtCurrentRow
        Private m_strhtxtPXH As String                           'htxtPXH 

        Private m_strhtxtWTXLQuery As String                      'htxtWTXLQuery
        Private m_strhtxtWTXLRows As String                       'htxtWTXLRows
        Private m_strhtxtWTXLSort As String                       'htxtWTXLSort
        Private m_strhtxtWTXLSortColumnIndex As String            'htxtWTXLSortColumnIndex
        Private m_strhtxtWTXLSortType As String                   'htxtWTXLSortType
        Private m_strhtxtDivLeftGRXX As String                    'htxtDivLeftGRXX
        Private m_strhtxtDivTopGRXX As String                     'htxtDivTopGRXX
        Private m_strhtxtDivLeftWTXL As String                    'htxtDivLeftWTXL
        Private m_strhtxtDivTopWTXL As String                     'htxtDivTopWTXL
        Private m_strhtxtDivLeftBody As String                    'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String                     'htxtDivTopBody

        Private m_strhtxtSessionIdQuery As String                 'htxtSessionIdQuery

        '----------------------------------------------------------------
        'asp:textbox
        '----------------------------------------------------------------


        Private m_strtxtWTXLPageIndex As String                  'txtWTXLPageIndex
        Private m_strtxtWTXLPageSize As String                   'txtWTXLPageSize

        Private m_strtxtSearch_XM As String                      'txtSearch_XM
        Private m_strtxtSearch_DWMC As String                    'txtSearch_DWMC
        Private m_strtxtSearch_DZ As String                      'txtSearch_DZ
        Private m_strtxtSearch_DH As String                      'txtSearch_DH
        Private m_strtxtSearch_DZYJ As String                    'txtSearch_DZYJ
        Private m_strtxtSearch_YZBM As String                    'txtSearch_YZBM

        '----------------------------------------------------------------
        'asp:datagrid - grdWTXL
        '----------------------------------------------------------------
        Private m_intPageSize_grdWTXL As Integer
        Private m_intSelectedIndex_grdWTXL As Integer
        Private m_intCurrentPageIndex_grdWTXL As Integer












        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            'hidden
            m_strhtxtWTXLQuery = ""
            m_strhtxtWTXLRows = ""
            m_strhtxtWTXLSort = ""
            m_strhtxtWTXLSortColumnIndex = ""
            m_strhtxtWTXLSortType = ""
            m_strhtxtDivLeftGRXX = ""
            m_strhtxtDivTopGRXX = ""
            m_strhtxtDivLeftWTXL = ""
            m_strhtxtDivTopWTXL = ""
            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""

            m_strhtxtSessionIdQuery = ""

            'textbox
            m_strtxtWTXLPageIndex = ""
            m_strtxtWTXLPageSize = ""


            m_strhtxtCurrentPage = ""
            m_strhtxtCurrentRow = ""
            m_strhtxtEditMode = ""
            m_strhtxtEditType = ""
            m_strhtxtPXH = ""


            m_strtxtSearch_XM = ""
            m_strtxtSearch_DWMC = ""
            m_strtxtSearch_DZ = ""
            m_strtxtSearch_DH = ""
            m_strtxtSearch_DZYJ = ""
            m_strtxtSearch_YZBM = ""

            'datagrid
            m_intPageSize_grdWTXL = 0
            m_intCurrentPageIndex_grdWTXL = 0
            m_intSelectedIndex_grdWTXL = -1

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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMGrswWtxl)
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
        ' htxtWTXLQuery属性
        '----------------------------------------------------------------
        Public Property htxtWTXLQuery() As String
            Get
                htxtWTXLQuery = m_strhtxtWTXLQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtWTXLQuery = Value
                Catch ex As Exception
                    m_strhtxtWTXLQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtWTXLRows属性
        '----------------------------------------------------------------
        Public Property htxtWTXLRows() As String
            Get
                htxtWTXLRows = m_strhtxtWTXLRows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtWTXLRows = Value
                Catch ex As Exception
                    m_strhtxtWTXLRows = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtWTXLSort属性
        '----------------------------------------------------------------
        Public Property htxtWTXLSort() As String
            Get
                htxtWTXLSort = m_strhtxtWTXLSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtWTXLSort = Value
                Catch ex As Exception
                    m_strhtxtWTXLSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtWTXLSortColumnIndex属性
        '----------------------------------------------------------------
        Public Property htxtWTXLSortColumnIndex() As String
            Get
                htxtWTXLSortColumnIndex = m_strhtxtWTXLSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtWTXLSortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtWTXLSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtWTXLSortType属性
        '----------------------------------------------------------------
        Public Property htxtWTXLSortType() As String
            Get
                htxtWTXLSortType = m_strhtxtWTXLSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtWTXLSortType = Value
                Catch ex As Exception
                    m_strhtxtWTXLSortType = ""
                End Try
            End Set
        End Property





        '----------------------------------------------------------------
        ' htxtDivLeftGRXX属性
        '----------------------------------------------------------------
        Public Property htxtDivLeftGRXX() As String
            Get
                htxtDivLeftGRXX = m_strhtxtDivLeftGRXX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftGRXX = Value
                Catch ex As Exception
                    m_strhtxtDivLeftGRXX = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopGRXX属性
        '----------------------------------------------------------------
        Public Property htxtDivTopGRXX() As String
            Get
                htxtDivTopGRXX = m_strhtxtDivTopGRXX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopGRXX = Value
                Catch ex As Exception
                    m_strhtxtDivTopGRXX = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivLeftWTXL属性
        '----------------------------------------------------------------
        Public Property htxtDivLeftWTXL() As String
            Get
                htxtDivLeftWTXL = m_strhtxtDivLeftWTXL
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftWTXL = Value
                Catch ex As Exception
                    m_strhtxtDivLeftWTXL = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopWTXL属性
        '----------------------------------------------------------------
        Public Property htxtDivTopWTXL() As String
            Get
                htxtDivTopWTXL = m_strhtxtDivTopWTXL
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopWTXL = Value
                Catch ex As Exception
                    m_strhtxtDivTopWTXL = ""
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
        ' htxtSessionIdQuery属性
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
        ' txtWTXLPageIndex属性
        '----------------------------------------------------------------
        Public Property txtWTXLPageIndex() As String
            Get
                txtWTXLPageIndex = m_strtxtWTXLPageIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtWTXLPageIndex = Value
                Catch ex As Exception
                    m_strtxtWTXLPageIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtWTXLPageSize属性
        '----------------------------------------------------------------
        Public Property txtWTXLPageSize() As String
            Get
                txtWTXLPageSize = m_strtxtWTXLPageSize
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtWTXLPageSize = Value
                Catch ex As Exception
                    m_strtxtWTXLPageSize = ""
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
        ' htxtPXH属性
        '----------------------------------------------------------------
        Public Property htxtPXH() As String
            Get
                htxtPXH = m_strhtxtPXH
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtPXH = Value
                Catch ex As Exception
                    m_strhtxtPXH = ""
                End Try
            End Set
        End Property



        '----------------------------------------------------------------
        ' txtSearch_XM属性
        '----------------------------------------------------------------
        Public Property txtSearch_XM() As String
            Get
                txtSearch_XM = m_strtxtSearch_XM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSearch_XM = Value
                Catch ex As Exception
                    m_strtxtSearch_XM = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtSearch_DH属性
        '----------------------------------------------------------------
        Public Property txtSearch_DH() As String
            Get
                txtSearch_DH = m_strtxtSearch_DH
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSearch_DH = Value
                Catch ex As Exception
                    m_strtxtSearch_DH = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtSearch_DZ属性
        '----------------------------------------------------------------
        Public Property txtSearch_DZ() As String
            Get
                txtSearch_DZ = m_strtxtSearch_DZ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSearch_DZ = Value
                Catch ex As Exception
                    m_strtxtSearch_DZ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtSearch_DWMC属性
        '----------------------------------------------------------------
        Public Property txtSearch_DWMC() As String
            Get
                txtSearch_DWMC = m_strtxtSearch_DWMC
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSearch_DWMC = Value
                Catch ex As Exception
                    m_strtxtSearch_DWMC = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtSearch_DZYJ属性
        '----------------------------------------------------------------
        Public Property txtSearch_DZYJ() As String
            Get
                txtSearch_DZYJ = m_strtxtSearch_DZYJ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSearch_DZYJ = Value
                Catch ex As Exception
                    m_strtxtSearch_DZYJ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtSearch_YZBM属性
        '----------------------------------------------------------------
        Public Property txtSearch_YZBM() As String
            Get
                txtSearch_YZBM = m_strtxtSearch_YZBM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSearch_YZBM = Value
                Catch ex As Exception
                    m_strtxtSearch_YZBM = ""
                End Try
            End Set
        End Property





        '----------------------------------------------------------------
        ' grdWTXLPageSize属性
        '----------------------------------------------------------------
        Public Property grdWTXLPageSize() As Integer
            Get
                grdWTXLPageSize = m_intPageSize_grdWTXL
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_grdWTXL = Value
                Catch ex As Exception
                    m_intPageSize_grdWTXL = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdWTXLCurrentPageIndex属性
        '----------------------------------------------------------------
        Public Property grdWTXLCurrentPageIndex() As Integer
            Get
                grdWTXLCurrentPageIndex = m_intCurrentPageIndex_grdWTXL
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intCurrentPageIndex_grdWTXL = Value
                Catch ex As Exception
                    m_intCurrentPageIndex_grdWTXL = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdWTXLSelectedIndex属性
        '----------------------------------------------------------------
        Public Property grdWTXLSelectedIndex() As Integer
            Get
                grdWTXLSelectedIndex = m_intSelectedIndex_grdWTXL
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_grdWTXL = Value
                Catch ex As Exception
                    m_intSelectedIndex_grdWTXL = -1
                End Try
            End Set
        End Property

    End Class

End Namespace
