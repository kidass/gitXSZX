Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IMFlowFujian
    '
    ' 功能描述： 
    '     flow_fujian.aspx模块本身恢复现场需要的信息
    '----------------------------------------------------------------
    <Serializable()> Public Class IMFlowFujian
        Implements IDisposable

        '----------------------------------------------------------------
        'textbox
        '----------------------------------------------------------------
        Private m_strtxtFJPageIndex As String                         'txtFJPageIndex
        Private m_strtxtFJPageSize As String                          'txtFJPageSize

        '----------------------------------------------------------------
        'hidden textbox
        '----------------------------------------------------------------
        Private m_strhtxtFJQuery As String                            'htxtFJQuery
        Private m_strhtxtFJRows As String                             'htxtFJRows
        Private m_strhtxtFJSort As String                             'htxtFJSort
        Private m_strhtxtFJSortColumnIndex As String                  'htxtFJSortColumnIndex
        Private m_strhtxtFJSortType As String                         'htxtFJSortType
        Private m_strhtxtDivLeftFJ As String                          'htxtDivLeftFJ
        Private m_strhtxtDivTopFJ As String                           'htxtDivTopFJ
        Private m_strhtxtDivLeftBody As String                        'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String                         'htxtDivTopBody

        '----------------------------------------------------------------
        'grdFJ paramters
        '----------------------------------------------------------------
        Private m_objDataSet_FJ As Xydc.Platform.Common.Data.FlowData    '附件数据
        Private m_intPageSize_grdFJ As Integer                        'grdFJ的页大小
        Private m_intSelectedIndex_grdFJ As Integer                   'grdFJ的当前页号












        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()

            MyBase.New()

            m_strtxtFJPageIndex = ""
            m_strtxtFJPageSize = ""

            m_strhtxtFJQuery = ""
            m_strhtxtFJRows = ""
            m_strhtxtFJSort = ""
            m_strhtxtFJSortColumnIndex = ""
            m_strhtxtFJSortType = ""

            m_strhtxtDivLeftFJ = ""
            m_strhtxtDivTopFJ = ""

            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""

            m_objDataSet_FJ = Nothing

            m_intPageSize_grdFJ = 100
            m_intSelectedIndex_grdFJ = -1

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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMFlowFujian)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub













        '----------------------------------------------------------------
        ' txtFJPageIndex属性
        '----------------------------------------------------------------
        Public Property txtFJPageIndex() As String
            Get
                txtFJPageIndex = m_strtxtFJPageIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtFJPageIndex = Value
                Catch ex As Exception
                    m_strtxtFJPageIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtFJPageSize属性
        '----------------------------------------------------------------
        Public Property txtFJPageSize() As String
            Get
                txtFJPageSize = m_strtxtFJPageSize
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtFJPageSize = Value
                Catch ex As Exception
                    m_strtxtFJPageSize = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' htxtFJSort属性
        '----------------------------------------------------------------
        Public Property htxtFJSort() As String
            Get
                htxtFJSort = m_strhtxtFJSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtFJSort = Value
                Catch ex As Exception
                    m_strhtxtFJSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtFJRows属性
        '----------------------------------------------------------------
        Public Property htxtFJRows() As String
            Get
                htxtFJRows = m_strhtxtFJRows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtFJRows = Value
                Catch ex As Exception
                    m_strhtxtFJRows = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtFJSortColumnIndex属性
        '----------------------------------------------------------------
        Public Property htxtFJSortColumnIndex() As String
            Get
                htxtFJSortColumnIndex = m_strhtxtFJSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtFJSortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtFJSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtFJQuery属性
        '----------------------------------------------------------------
        Public Property htxtFJQuery() As String
            Get
                htxtFJQuery = m_strhtxtFJQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtFJQuery = Value
                Catch ex As Exception
                    m_strhtxtFJQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtFJSortType属性
        '----------------------------------------------------------------
        Public Property htxtFJSortType() As String
            Get
                htxtFJSortType = m_strhtxtFJSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtFJSortType = Value
                Catch ex As Exception
                    m_strhtxtFJSortType = ""
                End Try
            End Set
        End Property



        '----------------------------------------------------------------
        ' htxtDivLeftFJ属性
        '----------------------------------------------------------------
        Public Property htxtDivLeftFJ() As String
            Get
                htxtDivLeftFJ = m_strhtxtDivLeftFJ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftFJ = Value
                Catch ex As Exception
                    m_strhtxtDivLeftFJ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopFJ属性
        '----------------------------------------------------------------
        Public Property htxtDivTopFJ() As String
            Get
                htxtDivTopFJ = m_strhtxtDivTopFJ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopFJ = Value
                Catch ex As Exception
                    m_strhtxtDivTopFJ = ""
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
        ' objDataSet_FJ属性
        '----------------------------------------------------------------
        Public Property objDataSet_FJ() As Xydc.Platform.Common.Data.FlowData
            Get
                objDataSet_FJ = m_objDataSet_FJ
            End Get
            Set(ByVal Value As Xydc.Platform.Common.Data.FlowData)
                Try
                    m_objDataSet_FJ = Value
                Catch ex As Exception
                    m_objDataSet_FJ = Nothing
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdFJ_PageSize属性
        '----------------------------------------------------------------
        Public Property grdFJ_PageSize() As Integer
            Get
                grdFJ_PageSize = m_intPageSize_grdFJ
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_grdFJ = Value
                Catch ex As Exception
                    m_intPageSize_grdFJ = 100
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdFJ_SelectedIndex属性
        '----------------------------------------------------------------
        Public Property grdFJ_SelectedIndex() As Integer
            Get
                grdFJ_SelectedIndex = m_intSelectedIndex_grdFJ
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_grdFJ = Value
                Catch ex As Exception
                    m_intSelectedIndex_grdFJ = -1
                End Try
            End Set
        End Property

    End Class

End Namespace
