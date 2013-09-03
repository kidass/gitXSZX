Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IMGwdmJgdz
    '
    ' 功能描述： 
    '     gwdm_jgdz.aspx模块本身恢复现场需要的信息
    '----------------------------------------------------------------
    <Serializable()> Public Class IMGwdmJgdz
        Implements IDisposable

        '----------------------------------------------------------------
        'hidden textbox
        '----------------------------------------------------------------
        Private m_strhtxtCurrentPage As String                'htxtCurrentPage
        Private m_strhtxtCurrentRow As String                 'htxtCurrentRow
        Private m_strhtxtEditMode As String                   'htxtEditMode
        Private m_strhtxtEditType As String                   'htxtEditType
        Private m_strhtxtQuery As String                      'htxtQuery
        Private m_strhtxtRows As String                       'htxtRows
        Private m_strhtxtSort As String                       'htxtSort
        Private m_strhtxtSortColumnIndex As String            'htxtSortColumnIndex
        Private m_strhtxtSortType As String                   'htxtSortType
        Private m_strhtxtDivLeftObject As String              'htxtDivLeftObject
        Private m_strhtxtDivTopObject As String               'htxtDivTopObject
        Private m_strhtxtDivLeftBody As String                'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String                 'htxtDivTopBody

        '----------------------------------------------------------------
        'asp:textbox
        '----------------------------------------------------------------
        Private m_strtxtPageIndex As String                  'txtPageIndex
        Private m_strtxtPageSize As String                   'txtPageSize
        Private m_strtxtSearch_DZDM As String                'txtSearch_DZDM
        Private m_strtxtSearch_JGDZ As String                'txtSearch_JGDZ
        Private m_strtxtDZDM As String                       'txtDZDM
        Private m_strtxtJGDZ As String                       'txtJGDZ
        Private m_strtxtYZMC As String                       'txtYZMC
        Private m_strtxtXLMC As String                       'txtXLMC
        Private m_strtxtXLPC As String                       'txtXLPC
        Private m_strtxtFFBM As String                       'txtFFBM
        Private m_strtxtFFRY As String                       'txtFFRY

        '----------------------------------------------------------------
        'asp:datagrid - grdObjects
        '----------------------------------------------------------------
        Private m_intPageSize_grdObjects As Integer
        Private m_intSelectedIndex_grdObjects As Integer
        Private m_intCurrentPageIndex_grdObjects As Integer










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
            m_strhtxtQuery = ""
            m_strhtxtRows = ""
            m_strhtxtSort = ""
            m_strhtxtSortColumnIndex = ""
            m_strhtxtSortType = ""
            m_strhtxtDivLeftObject = ""
            m_strhtxtDivTopObject = ""
            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""

            'textbox
            m_strtxtPageIndex = ""
            m_strtxtPageSize = ""
            m_strtxtSearch_DZDM = ""
            m_strtxtSearch_JGDZ = ""
            m_strtxtDZDM = ""
            m_strtxtJGDZ = ""
            m_strtxtYZMC = ""
            m_strtxtXLMC = ""
            m_strtxtXLPC = ""
            m_strtxtFFBM = ""
            m_strtxtFFRY = ""

            'datagrid
            m_intPageSize_grdObjects = 0
            m_intCurrentPageIndex_grdObjects = 0
            m_intSelectedIndex_grdObjects = -1

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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMGwdmJgdz)
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
        ' htxtQuery属性
        '----------------------------------------------------------------
        Public Property htxtQuery() As String
            Get
                htxtQuery = m_strhtxtQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtQuery = Value
                Catch ex As Exception
                    m_strhtxtQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtRows属性
        '----------------------------------------------------------------
        Public Property htxtRows() As String
            Get
                htxtRows = m_strhtxtRows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtRows = Value
                Catch ex As Exception
                    m_strhtxtRows = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtSort属性
        '----------------------------------------------------------------
        Public Property htxtSort() As String
            Get
                htxtSort = m_strhtxtSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtSort = Value
                Catch ex As Exception
                    m_strhtxtSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtSortColumnIndex属性
        '----------------------------------------------------------------
        Public Property htxtSortColumnIndex() As String
            Get
                htxtSortColumnIndex = m_strhtxtSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtSortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtSortType属性
        '----------------------------------------------------------------
        Public Property htxtSortType() As String
            Get
                htxtSortType = m_strhtxtSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtSortType = Value
                Catch ex As Exception
                    m_strhtxtSortType = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivLeftObject属性
        '----------------------------------------------------------------
        Public Property htxtDivLeftObject() As String
            Get
                htxtDivLeftObject = m_strhtxtDivLeftObject
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftObject = Value
                Catch ex As Exception
                    m_strhtxtDivLeftObject = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopObject属性
        '----------------------------------------------------------------
        Public Property htxtDivTopObject() As String
            Get
                htxtDivTopObject = m_strhtxtDivTopObject
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopObject = Value
                Catch ex As Exception
                    m_strhtxtDivTopObject = ""
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
        ' txtPageIndex属性
        '----------------------------------------------------------------
        Public Property txtPageIndex() As String
            Get
                txtPageIndex = m_strtxtPageIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtPageIndex = Value
                Catch ex As Exception
                    m_strtxtPageIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtPageSize属性
        '----------------------------------------------------------------
        Public Property txtPageSize() As String
            Get
                txtPageSize = m_strtxtPageSize
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtPageSize = Value
                Catch ex As Exception
                    m_strtxtPageSize = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtSearchDZDM属性
        '----------------------------------------------------------------
        Public Property txtSearchDZDM() As String
            Get
                txtSearchDZDM = m_strtxtSearch_DZDM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSearch_DZDM = Value
                Catch ex As Exception
                    m_strtxtSearch_DZDM = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtSearchJGDZ属性
        '----------------------------------------------------------------
        Public Property txtSearchJGDZ() As String
            Get
                txtSearchJGDZ = m_strtxtSearch_JGDZ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSearch_JGDZ = Value
                Catch ex As Exception
                    m_strtxtSearch_JGDZ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtDZDM属性
        '----------------------------------------------------------------
        Public Property txtDZDM() As String
            Get
                txtDZDM = m_strtxtDZDM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtDZDM = Value
                Catch ex As Exception
                    m_strtxtDZDM = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtJGDZ属性
        '----------------------------------------------------------------
        Public Property txtJGDZ() As String
            Get
                txtJGDZ = m_strtxtJGDZ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtJGDZ = Value
                Catch ex As Exception
                    m_strtxtJGDZ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtYZMC属性
        '----------------------------------------------------------------
        Public Property txtYZMC() As String
            Get
                txtYZMC = m_strtxtYZMC
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtYZMC = Value
                Catch ex As Exception
                    m_strtxtYZMC = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtXLMC属性
        '----------------------------------------------------------------
        Public Property txtXLMC() As String
            Get
                txtXLMC = m_strtxtXLMC
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtXLMC = Value
                Catch ex As Exception
                    m_strtxtXLMC = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtXLPC属性
        '----------------------------------------------------------------
        Public Property txtXLPC() As String
            Get
                txtXLPC = m_strtxtXLPC
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtXLPC = Value
                Catch ex As Exception
                    m_strtxtXLPC = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtFFBM属性
        '----------------------------------------------------------------
        Public Property txtFFBM() As String
            Get
                txtFFBM = m_strtxtFFBM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtFFBM = Value
                Catch ex As Exception
                    m_strtxtFFBM = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtFFRY属性
        '----------------------------------------------------------------
        Public Property txtFFRY() As String
            Get
                txtFFRY = m_strtxtFFRY
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtFFRY = Value
                Catch ex As Exception
                    m_strtxtFFRY = ""
                End Try
            End Set
        End Property



        '----------------------------------------------------------------
        ' grdObjectsPageSize属性
        '----------------------------------------------------------------
        Public Property grdObjectsPageSize() As Integer
            Get
                grdObjectsPageSize = m_intPageSize_grdObjects
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_grdObjects = Value
                Catch ex As Exception
                    m_intPageSize_grdObjects = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdObjectsCurrentPageIndex属性
        '----------------------------------------------------------------
        Public Property grdObjectsCurrentPageIndex() As Integer
            Get
                grdObjectsCurrentPageIndex = m_intCurrentPageIndex_grdObjects
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intCurrentPageIndex_grdObjects = Value
                Catch ex As Exception
                    m_intCurrentPageIndex_grdObjects = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdObjectsSelectedIndex属性
        '----------------------------------------------------------------
        Public Property grdObjectsSelectedIndex() As Integer
            Get
                grdObjectsSelectedIndex = m_intSelectedIndex_grdObjects
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_grdObjects = Value
                Catch ex As Exception
                    m_intSelectedIndex_grdObjects = 0
                End Try
            End Set
        End Property

    End Class

End Namespace
