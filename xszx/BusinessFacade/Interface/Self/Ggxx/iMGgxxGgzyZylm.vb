Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IMGgxxGgzyZylm
    '
    ' 功能描述： 
    '     ggxx_ggzy_zylm.aspx模块本身恢复现场需要的信息
    '----------------------------------------------------------------
    <Serializable()> Public Class IMGgxxGgzyZylm
        Implements IDisposable

        '----------------------------------------------------------------
        'hidden textbox
        '----------------------------------------------------------------
        Private m_strhtxtObjectQuery As String            'htxtObjectQuery
        Private m_strhtxtObjectRows As String             'htxtObjectRows
        Private m_strhtxtObjectSort As String             'htxtObjectSort
        Private m_strhtxtObjectSortColumnIndex As String  'htxtObjectSortColumnIndex
        Private m_strhtxtObjectSortType As String         'htxtObjectSortType
        Private m_strhtxtDivLeftObject As String          'htxtDivLeftObject
        Private m_strhtxtDivTopObject As String           'htxtDivTopObject
        Private m_strhtxtDivLeftBody As String            'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String             'htxtDivTopBody

        '----------------------------------------------------------------
        'asp:textbox
        '----------------------------------------------------------------
        Private m_strtxtPageIndex As String               'txtPageIndex
        Private m_strtxtPageSize As String                'txtPageSize
        Private m_strtxtSearchDM As String                'txtSearchDM
        Private m_strtxtSearchMC As String                'txtSearchMC
        Private m_strtxtSearchSM As String                'txtSearchSM
        Private m_strtxtSearchJBMin As String             'txtSearchJBMin
        Private m_strtxtSearchJBMax As String             'txtSearchJBMax

        '----------------------------------------------------------------
        'asp:datagrid - grdObject
        '----------------------------------------------------------------
        Private m_intPageSize_grdObject As Integer
        Private m_intSelectedIndex_grdObject As Integer
        Private m_intCurrentPageIndex_grdObject As Integer

        '----------------------------------------------------------------
        'treeview - tvwObject
        '----------------------------------------------------------------
        Private m_strSelectedNodeIndex_tvwObject As String  'SelectedNodeIndex











        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            'hidden
            m_strhtxtObjectQuery = ""
            m_strhtxtObjectRows = ""
            m_strhtxtObjectSort = ""
            m_strhtxtObjectSortColumnIndex = ""
            m_strhtxtObjectSortType = ""
            m_strhtxtDivLeftObject = ""
            m_strhtxtDivTopObject = ""
            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""

            'textbox
            m_strtxtPageIndex = ""
            m_strtxtPageSize = ""
            m_strtxtSearchDM = ""
            m_strtxtSearchMC = ""
            m_strtxtSearchSM = ""
            m_strtxtSearchJBMin = ""
            m_strtxtSearchJBMax = ""

            'datagrid
            m_intPageSize_grdObject = 0
            m_intCurrentPageIndex_grdObject = 0
            m_intSelectedIndex_grdObject = -1

            'treeview
            m_strSelectedNodeIndex_tvwObject = ""

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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMGgxxGgzyZylm)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub












        '----------------------------------------------------------------
        ' htxtObjectQuery属性
        '----------------------------------------------------------------
        Public Property htxtObjectQuery() As String
            Get
                htxtObjectQuery = m_strhtxtObjectQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtObjectQuery = Value
                Catch ex As Exception
                    m_strhtxtObjectQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtObjectRows属性
        '----------------------------------------------------------------
        Public Property htxtObjectRows() As String
            Get
                htxtObjectRows = m_strhtxtObjectRows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtObjectRows = Value
                Catch ex As Exception
                    m_strhtxtObjectRows = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtObjectSort属性
        '----------------------------------------------------------------
        Public Property htxtObjectSort() As String
            Get
                htxtObjectSort = m_strhtxtObjectSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtObjectSort = Value
                Catch ex As Exception
                    m_strhtxtObjectSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtObjectSortColumnIndex属性
        '----------------------------------------------------------------
        Public Property htxtObjectSortColumnIndex() As String
            Get
                htxtObjectSortColumnIndex = m_strhtxtObjectSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtObjectSortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtObjectSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtObjectSortType属性
        '----------------------------------------------------------------
        Public Property htxtObjectSortType() As String
            Get
                htxtObjectSortType = m_strhtxtObjectSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtObjectSortType = Value
                Catch ex As Exception
                    m_strhtxtObjectSortType = ""
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
        ' txtSearchDM属性
        '----------------------------------------------------------------
        Public Property txtSearchDM() As String
            Get
                txtSearchDM = m_strtxtSearchDM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSearchDM = Value
                Catch ex As Exception
                    m_strtxtSearchDM = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtSearchMC属性
        '----------------------------------------------------------------
        Public Property txtSearchMC() As String
            Get
                txtSearchMC = m_strtxtSearchMC
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSearchMC = Value
                Catch ex As Exception
                    m_strtxtSearchMC = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtSearchSM属性
        '----------------------------------------------------------------
        Public Property txtSearchSM() As String
            Get
                txtSearchSM = m_strtxtSearchSM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSearchSM = Value
                Catch ex As Exception
                    m_strtxtSearchSM = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtSearchJBMin属性
        '----------------------------------------------------------------
        Public Property txtSearchJBMin() As String
            Get
                txtSearchJBMin = m_strtxtSearchJBMin
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSearchJBMin = Value
                Catch ex As Exception
                    m_strtxtSearchJBMin = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtSearchJBMax属性
        '----------------------------------------------------------------
        Public Property txtSearchJBMax() As String
            Get
                txtSearchJBMax = m_strtxtSearchJBMax
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSearchJBMax = Value
                Catch ex As Exception
                    m_strtxtSearchJBMax = ""
                End Try
            End Set
        End Property



        '----------------------------------------------------------------
        ' grdObjectPageSize属性
        '----------------------------------------------------------------
        Public Property grdObjectPageSize() As Integer
            Get
                grdObjectPageSize = m_intPageSize_grdObject
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_grdObject = Value
                Catch ex As Exception
                    m_intPageSize_grdObject = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdObjectCurrentPageIndex属性
        '----------------------------------------------------------------
        Public Property grdObjectCurrentPageIndex() As Integer
            Get
                grdObjectCurrentPageIndex = m_intCurrentPageIndex_grdObject
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intCurrentPageIndex_grdObject = Value
                Catch ex As Exception
                    m_intCurrentPageIndex_grdObject = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdObjectSelectedIndex属性
        '----------------------------------------------------------------
        Public Property grdObjectSelectedIndex() As Integer
            Get
                grdObjectSelectedIndex = m_intSelectedIndex_grdObject
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_grdObject = Value
                Catch ex As Exception
                    m_intSelectedIndex_grdObject = 0
                End Try
            End Set
        End Property



        '----------------------------------------------------------------
        ' SelectedNodeIndex属性
        '----------------------------------------------------------------
        Public Property SelectedNodeIndex() As String
            Get
                SelectedNodeIndex = m_strSelectedNodeIndex_tvwObject
            End Get
            Set(ByVal Value As String)
                Try
                    m_strSelectedNodeIndex_tvwObject = Value
                Catch ex As Exception
                    m_strSelectedNodeIndex_tvwObject = ""
                End Try
            End Set
        End Property

    End Class

End Namespace
