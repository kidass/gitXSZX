Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IMFlowXgwj
    '
    ' 功能描述： 
    '     flow_xgwj.aspx模块本身恢复现场需要的信息
    '----------------------------------------------------------------
    <Serializable()> Public Class IMFlowXgwj
        Implements IDisposable

        '----------------------------------------------------------------
        'textbox
        '----------------------------------------------------------------
        Private m_strtxtXGWJPageIndex As String                         'txtXGWJPageIndex
        Private m_strtxtXGWJPageSize As String                          'txtXGWJPageSize

        '----------------------------------------------------------------
        'hidden textbox
        '----------------------------------------------------------------
        Private m_strhtxtXGWJQuery As String                            'htxtXGWJQuery
        Private m_strhtxtXGWJRows As String                             'htxtXGWJRows
        Private m_strhtxtXGWJSort As String                             'htxtXGWJSort
        Private m_strhtxtXGWJSortColumnIndex As String                  'htxtXGWJSortColumnIndex
        Private m_strhtxtXGWJSortType As String                         'htxtXGWJSortType
        Private m_strhtxtDivLeftXGWJ As String                          'htxtDivLeftXGWJ
        Private m_strhtxtDivTopXGWJ As String                           'htxtDivTopXGWJ
        Private m_strhtxtDivLeftBody As String                          'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String                           'htxtDivTopBody

        '----------------------------------------------------------------
        'grdXGWJ parameters
        '----------------------------------------------------------------
        Private m_objDataSet_XGWJ As Xydc.Platform.Common.Data.FlowData    '相关文件数据集
        Private m_intPageSize_grdXGWJ As Integer                        'grdXGWJ的页大小
        Private m_intSelectedIndex_grdXGWJ As Integer                   'grdXGWJ的当前页号










        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()

            MyBase.New()

            m_strtxtXGWJPageIndex = ""
            m_strtxtXGWJPageSize = ""

            m_strhtxtXGWJQuery = ""
            m_strhtxtXGWJRows = ""
            m_strhtxtXGWJSort = ""
            m_strhtxtXGWJSortColumnIndex = ""
            m_strhtxtXGWJSortType = ""

            m_strhtxtDivLeftXGWJ = ""
            m_strhtxtDivTopXGWJ = ""

            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""

            m_objDataSet_XGWJ = Nothing

            m_intPageSize_grdXGWJ = 100
            m_intSelectedIndex_grdXGWJ = -1

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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMFlowXgwj)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub












        '----------------------------------------------------------------
        ' txtXGWJPageIndex属性
        '----------------------------------------------------------------
        Public Property txtXGWJPageIndex() As String
            Get
                txtXGWJPageIndex = m_strtxtXGWJPageIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtXGWJPageIndex = Value
                Catch ex As Exception
                    m_strtxtXGWJPageIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtXGWJPageSize属性
        '----------------------------------------------------------------
        Public Property txtXGWJPageSize() As String
            Get
                txtXGWJPageSize = m_strtxtXGWJPageSize
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtXGWJPageSize = Value
                Catch ex As Exception
                    m_strtxtXGWJPageSize = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' htxtXGWJSort属性
        '----------------------------------------------------------------
        Public Property htxtXGWJSort() As String
            Get
                htxtXGWJSort = m_strhtxtXGWJSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtXGWJSort = Value
                Catch ex As Exception
                    m_strhtxtXGWJSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtXGWJRows属性
        '----------------------------------------------------------------
        Public Property htxtXGWJRows() As String
            Get
                htxtXGWJRows = m_strhtxtXGWJRows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtXGWJRows = Value
                Catch ex As Exception
                    m_strhtxtXGWJRows = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtXGWJSortColumnIndex属性
        '----------------------------------------------------------------
        Public Property htxtXGWJSortColumnIndex() As String
            Get
                htxtXGWJSortColumnIndex = m_strhtxtXGWJSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtXGWJSortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtXGWJSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtXGWJQuery属性
        '----------------------------------------------------------------
        Public Property htxtXGWJQuery() As String
            Get
                htxtXGWJQuery = m_strhtxtXGWJQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtXGWJQuery = Value
                Catch ex As Exception
                    m_strhtxtXGWJQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtXGWJSortType属性
        '----------------------------------------------------------------
        Public Property htxtXGWJSortType() As String
            Get
                htxtXGWJSortType = m_strhtxtXGWJSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtXGWJSortType = Value
                Catch ex As Exception
                    m_strhtxtXGWJSortType = ""
                End Try
            End Set
        End Property



        '----------------------------------------------------------------
        ' htxtDivLeftXGWJ属性
        '----------------------------------------------------------------
        Public Property htxtDivLeftXGWJ() As String
            Get
                htxtDivLeftXGWJ = m_strhtxtDivLeftXGWJ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftXGWJ = Value
                Catch ex As Exception
                    m_strhtxtDivLeftXGWJ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopXGWJ属性
        '----------------------------------------------------------------
        Public Property htxtDivTopXGWJ() As String
            Get
                htxtDivTopXGWJ = m_strhtxtDivTopXGWJ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopXGWJ = Value
                Catch ex As Exception
                    m_strhtxtDivTopXGWJ = ""
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
        ' objDataSet_XGWJ属性
        '----------------------------------------------------------------
        Public Property objDataSet_XGWJ() As Xydc.Platform.Common.Data.FlowData
            Get
                objDataSet_XGWJ = m_objDataSet_XGWJ
            End Get
            Set(ByVal Value As Xydc.Platform.Common.Data.FlowData)
                Try
                    m_objDataSet_XGWJ = Value
                Catch ex As Exception
                    m_objDataSet_XGWJ = Nothing
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdXGWJ_PageSize属性
        '----------------------------------------------------------------
        Public Property grdXGWJ_PageSize() As Integer
            Get
                grdXGWJ_PageSize = m_intPageSize_grdXGWJ
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_grdXGWJ = Value
                Catch ex As Exception
                    m_intPageSize_grdXGWJ = 100
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdXGWJ_SelectedIndex属性
        '----------------------------------------------------------------
        Public Property grdXGWJ_SelectedIndex() As Integer
            Get
                grdXGWJ_SelectedIndex = m_intSelectedIndex_grdXGWJ
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_grdXGWJ = Value
                Catch ex As Exception
                    m_intSelectedIndex_grdXGWJ = -1
                End Try
            End Set
        End Property

    End Class

End Namespace
