Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IMXtpzDbsz
    '
    ' 功能描述： 
    '     xtpz_dbsz.aspx模块本身恢复现场需要的信息
    '----------------------------------------------------------------
    <Serializable()> Public Class IMXtpzDbsz
        Implements IDisposable

        '----------------------------------------------------------------
        'hidden textbox
        '----------------------------------------------------------------
        Private m_strhtxtCurrentPage As String               'htxtCurrentPage
        Private m_strhtxtCurrentRow As String                'htxtCurrentRow
        Private m_strhtxtEditMode As String                  'htxtEditMode
        Private m_strhtxtEditType As String                  'htxtEditType

        Private m_strhtxtDBSZQuery As String                 'htxtDBSZQuery
        Private m_strhtxtDBSZRows As String                  'htxtDBSZRows
        Private m_strhtxtDBSZSort As String                  'htxtDBSZSort
        Private m_strhtxtDBSZSortColumnIndex As String       'htxtDBSZSortColumnIndex
        Private m_strhtxtDBSZSortType As String              'htxtDBSZSortType

        Private m_strhtxtDivLeftDBSZ As String               'htxtDivLeftDBSZ
        Private m_strhtxtDivTopDBSZ As String                'htxtDivTopDBSZ
        Private m_strhtxtDivLeftBody As String               'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String                'htxtDivTopBody

        Private m_strhtxtSessionIdDBSZQuery As String        'htxtSessionIdDBSZQuery

        '----------------------------------------------------------------
        'asp:textbox
        '----------------------------------------------------------------
        Private m_strtxtDBSZPageIndex As String               'txtDBSZPageIndex
        Private m_strtxtDBSZPageSize As String                'txtDBSZPageSize
        Private m_strtxtDBSZSearch_ZWMC As String             'txtDBSZSearch_ZWMC
        Private m_strtxtDBSZSearch_DBFW As String             'txtDBSZSearch_DBFW
        Private m_strtxtDBSZSearch_BCSM As String             'txtDBSZSearch_BCSM

        Private m_strtxtZWMC As String                        'txtZWMC
        Private m_strhtxtZWDM As String                       'htxtZWDM
        Private m_intSelectedIndex_ddlDBFW As Integer         'ddlDBFW
        Private m_intSelectedIndex_ddlBCSM As Integer         'ddlBCSM

        '----------------------------------------------------------------
        'asp:datagrid - grdDBSZ
        '----------------------------------------------------------------
        Private m_intPageSize_grdDBSZ As Integer
        Private m_intSelectedIndex_grdDBSZ As Integer
        Private m_intCurrentPageIndex_grdDBSZ As Integer










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

            m_strhtxtDBSZQuery = ""
            m_strhtxtDBSZRows = ""
            m_strhtxtDBSZSort = ""
            m_strhtxtDBSZSortColumnIndex = ""
            m_strhtxtDBSZSortType = ""

            m_strhtxtDivLeftDBSZ = ""
            m_strhtxtDivTopDBSZ = ""
            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""

            m_strhtxtSessionIdDBSZQuery = ""

            'textbox
            m_strtxtDBSZPageIndex = ""
            m_strtxtDBSZPageSize = ""
            m_strtxtDBSZSearch_ZWMC = ""
            m_strtxtDBSZSearch_BCSM = ""
            m_strtxtDBSZSearch_DBFW = ""

            m_strtxtZWMC = ""
            m_strhtxtZWDM = ""
            m_intSelectedIndex_ddlDBFW = -1
            m_intSelectedIndex_ddlBCSM = -1

            'datagrid
            m_intPageSize_grdDBSZ = 0
            m_intCurrentPageIndex_grdDBSZ = 0
            m_intSelectedIndex_grdDBSZ = -1

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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMXtpzDbsz)
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
        ' htxtDBSZQuery属性
        '----------------------------------------------------------------
        Public Property htxtDBSZQuery() As String
            Get
                htxtDBSZQuery = m_strhtxtDBSZQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDBSZQuery = Value
                Catch ex As Exception
                    m_strhtxtDBSZQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDBSZRows属性
        '----------------------------------------------------------------
        Public Property htxtDBSZRows() As String
            Get
                htxtDBSZRows = m_strhtxtDBSZRows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDBSZRows = Value
                Catch ex As Exception
                    m_strhtxtDBSZRows = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDBSZSort属性
        '----------------------------------------------------------------
        Public Property htxtDBSZSort() As String
            Get
                htxtDBSZSort = m_strhtxtDBSZSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDBSZSort = Value
                Catch ex As Exception
                    m_strhtxtDBSZSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDBSZSortColumnIndex属性
        '----------------------------------------------------------------
        Public Property htxtDBSZSortColumnIndex() As String
            Get
                htxtDBSZSortColumnIndex = m_strhtxtDBSZSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDBSZSortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtDBSZSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDBSZSortType属性
        '----------------------------------------------------------------
        Public Property htxtDBSZSortType() As String
            Get
                htxtDBSZSortType = m_strhtxtDBSZSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDBSZSortType = Value
                Catch ex As Exception
                    m_strhtxtDBSZSortType = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' htxtDivLeftDBSZ属性
        '----------------------------------------------------------------
        Public Property htxtDivLeftDBSZ() As String
            Get
                htxtDivLeftDBSZ = m_strhtxtDivLeftDBSZ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftDBSZ = Value
                Catch ex As Exception
                    m_strhtxtDivLeftDBSZ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopDBSZ属性
        '----------------------------------------------------------------
        Public Property htxtDivTopDBSZ() As String
            Get
                htxtDivTopDBSZ = m_strhtxtDivTopDBSZ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopDBSZ = Value
                Catch ex As Exception
                    m_strhtxtDivTopDBSZ = ""
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
        ' htxtSessionIdDBSZQuery属性
        '----------------------------------------------------------------
        Public Property htxtSessionIdDBSZQuery() As String
            Get
                htxtSessionIdDBSZQuery = m_strhtxtSessionIdDBSZQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtSessionIdDBSZQuery = Value
                Catch ex As Exception
                    m_strhtxtSessionIdDBSZQuery = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' txtDBSZPageIndex属性
        '----------------------------------------------------------------
        Public Property txtDBSZPageIndex() As String
            Get
                txtDBSZPageIndex = m_strtxtDBSZPageIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtDBSZPageIndex = Value
                Catch ex As Exception
                    m_strtxtDBSZPageIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtDBSZPageSize属性
        '----------------------------------------------------------------
        Public Property txtDBSZPageSize() As String
            Get
                txtDBSZPageSize = m_strtxtDBSZPageSize
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtDBSZPageSize = Value
                Catch ex As Exception
                    m_strtxtDBSZPageSize = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtDBSZSearch_ZWMC属性
        '----------------------------------------------------------------
        Public Property txtDBSZSearch_ZWMC() As String
            Get
                txtDBSZSearch_ZWMC = m_strtxtDBSZSearch_ZWMC
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtDBSZSearch_ZWMC = Value
                Catch ex As Exception
                    m_strtxtDBSZSearch_ZWMC = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtDBSZSearch_BCSM属性
        '----------------------------------------------------------------
        Public Property txtDBSZSearch_BCSM() As String
            Get
                txtDBSZSearch_BCSM = m_strtxtDBSZSearch_BCSM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtDBSZSearch_BCSM = Value
                Catch ex As Exception
                    m_strtxtDBSZSearch_BCSM = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtDBSZSearch_DBFW属性
        '----------------------------------------------------------------
        Public Property txtDBSZSearch_DBFW() As String
            Get
                txtDBSZSearch_DBFW = m_strtxtDBSZSearch_DBFW
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtDBSZSearch_DBFW = Value
                Catch ex As Exception
                    m_strtxtDBSZSearch_DBFW = ""
                End Try
            End Set
        End Property





        '----------------------------------------------------------------
        ' grdDBSZ_PageSize属性
        '----------------------------------------------------------------
        Public Property grdDBSZ_PageSize() As Integer
            Get
                grdDBSZ_PageSize = m_intPageSize_grdDBSZ
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_grdDBSZ = Value
                Catch ex As Exception
                    m_intPageSize_grdDBSZ = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdDBSZ_CurrentPageIndex属性
        '----------------------------------------------------------------
        Public Property grdDBSZ_CurrentPageIndex() As Integer
            Get
                grdDBSZ_CurrentPageIndex = m_intCurrentPageIndex_grdDBSZ
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intCurrentPageIndex_grdDBSZ = Value
                Catch ex As Exception
                    m_intCurrentPageIndex_grdDBSZ = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdDBSZ_SelectedIndex属性
        '----------------------------------------------------------------
        Public Property grdDBSZ_SelectedIndex() As Integer
            Get
                grdDBSZ_SelectedIndex = m_intSelectedIndex_grdDBSZ
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_grdDBSZ = Value
                Catch ex As Exception
                    m_intSelectedIndex_grdDBSZ = 0
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' txtZWMC属性
        '----------------------------------------------------------------
        Public Property txtZWMC() As String
            Get
                txtZWMC = m_strtxtZWMC
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtZWMC = Value
                Catch ex As Exception
                    m_strtxtZWMC = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtZWDM属性
        '----------------------------------------------------------------
        Public Property htxtZWDM() As String
            Get
                htxtZWDM = m_strhtxtZWDM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtZWDM = Value
                Catch ex As Exception
                    m_strhtxtZWDM = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' ddlDBFW_SelectedIndex属性
        '----------------------------------------------------------------
        Public Property ddlDBFW_SelectedIndex() As Integer
            Get
                ddlDBFW_SelectedIndex = m_intSelectedIndex_ddlDBFW
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_ddlDBFW = Value
                Catch ex As Exception
                    m_intSelectedIndex_ddlDBFW = -1
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' ddlBCSM_SelectedIndex属性
        '----------------------------------------------------------------
        Public Property ddlBCSM_SelectedIndex() As Integer
            Get
                ddlBCSM_SelectedIndex = m_intSelectedIndex_ddlBCSM
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_ddlBCSM = Value
                Catch ex As Exception
                    m_intSelectedIndex_ddlBCSM = -1
                End Try
            End Set
        End Property

    End Class

End Namespace
