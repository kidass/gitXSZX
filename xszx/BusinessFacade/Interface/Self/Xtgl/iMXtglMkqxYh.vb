Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IMXtglMkqxYh
    '
    ' 功能描述： 
    '     xtgl_mkqx_yh.aspx模块本身恢复现场需要的信息
    '----------------------------------------------------------------
    <Serializable()> Public Class IMXtglMkqxYh
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
        Private m_strtxtSearchMKDM As String              'txtSearchMKDM
        Private m_strtxtSearchMKMC As String              'txtSearchMKMC
        Private m_strtxtSearchMKSM As String              'txtSearchMKSM

        '----------------------------------------------------------------
        'asp:datagrid - grdObject
        '----------------------------------------------------------------
        Private m_intPageSize_grdObject As Integer
        Private m_intSelectedIndex_grdObject As Integer
        Private m_intCurrentPageIndex_grdObject As Integer

        '----------------------------------------------------------------
        'treeview - tvwServers
        '----------------------------------------------------------------
        Private m_strSelectedNodeIndex_tvwServers As String










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
            m_strtxtSearchMKDM = ""
            m_strtxtSearchMKMC = ""
            m_strtxtSearchMKSM = ""

            'datagrid
            m_intPageSize_grdObject = 0
            m_intCurrentPageIndex_grdObject = 0
            m_intSelectedIndex_grdObject = -1

            'treeview
            m_strSelectedNodeIndex_tvwServers = ""
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMXtglMkqxYh)
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
        ' txtSearchMKDM属性
        '----------------------------------------------------------------
        Public Property txtSearchMKDM() As String
            Get
                txtSearchMKDM = m_strtxtSearchMKDM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSearchMKDM = Value
                Catch ex As Exception
                    m_strtxtSearchMKDM = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtSearchMKMC属性
        '----------------------------------------------------------------
        Public Property txtSearchMKMC() As String
            Get
                txtSearchMKMC = m_strtxtSearchMKMC
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSearchMKMC = Value
                Catch ex As Exception
                    m_strtxtSearchMKMC = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtSearchMKSM属性
        '----------------------------------------------------------------
        Public Property txtSearchMKSM() As String
            Get
                txtSearchMKSM = m_strtxtSearchMKSM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSearchMKSM = Value
                Catch ex As Exception
                    m_strtxtSearchMKSM = ""
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
                SelectedNodeIndex = m_strSelectedNodeIndex_tvwServers
            End Get
            Set(ByVal Value As String)
                Try
                    m_strSelectedNodeIndex_tvwServers = Value
                Catch ex As Exception
                    m_strSelectedNodeIndex_tvwServers = ""
                End Try
            End Set
        End Property

    End Class

End Namespace
