Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IMXtglSjdx
    '
    ' 功能描述： 
    '     xtgl_sjdx.aspx模块本身恢复现场需要的信息
    '----------------------------------------------------------------
    <Serializable()> Public Class IMXtglSjdx
        Implements IDisposable

        '----------------------------------------------------------------
        'hidden textbox
        '----------------------------------------------------------------
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
        Private m_strtxtSearchDXM As String                  'txtSearchDXM
        Private m_strtxtSearchDXZWM As String                'txtSearchDXZWM
        Private m_strtxtSearchDXSM As String                 'txtSearchDXSM

        '----------------------------------------------------------------
        'asp:datagrid - grdObject
        '----------------------------------------------------------------
        Private m_intPageSize_grdObject As Integer
        Private m_intSelectedIndex_grdObject As Integer
        Private m_intCurrentPageIndex_grdObject As Integer

        '----------------------------------------------------------------
        'treeview - tvwServers
        '----------------------------------------------------------------
        Private m_strSelectedNodeIndex_tvwServers As String 'SelectedNodeIndex











        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
            'hidden
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
            m_strtxtSearchDXM = ""
            m_strtxtSearchDXZWM = ""
            m_strtxtSearchDXSM = ""
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMXtglSjdx)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub












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
        ' txtSearchDXM属性
        '----------------------------------------------------------------
        Public Property txtSearchDXM() As String
            Get
                txtSearchDXM = m_strtxtSearchDXM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSearchDXM = Value
                Catch ex As Exception
                    m_strtxtSearchDXM = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtSearchDXZWM属性
        '----------------------------------------------------------------
        Public Property txtSearchDXZWM() As String
            Get
                txtSearchDXZWM = m_strtxtSearchDXZWM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSearchDXZWM = Value
                Catch ex As Exception
                    m_strtxtSearchDXZWM = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtSearchDXSM属性
        '----------------------------------------------------------------
        Public Property txtSearchDXSM() As String
            Get
                txtSearchDXSM = m_strtxtSearchDXSM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSearchDXSM = Value
                Catch ex As Exception
                    m_strtxtSearchDXSM = ""
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
