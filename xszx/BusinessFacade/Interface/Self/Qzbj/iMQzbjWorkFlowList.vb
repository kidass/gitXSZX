Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IMQzbjWorkFlowList
    '
    ' 功能描述： 
    '     qzbj_workflow_list.aspx模块本身恢复现场需要的信息
    '----------------------------------------------------------------
    <Serializable()> Public Class IMQzbjWorkFlowList
        Implements IDisposable

        Private m_strhtxtWFLISTQuery As String                      'htxtWFLISTQuery
        Private m_strhtxtWFLISTRows As String                       'htxtWFLISTRows
        Private m_strhtxtWFLISTSort As String                       'htxtWFLISTSort
        Private m_strhtxtWFLISTSortColumnIndex As String            'htxtWFLISTSortColumnIndex
        Private m_strhtxtWFLISTSortType As String                   'htxtWFLISTSortType
        Private m_strhtxtDivLeftWFLIST As String                    'htxtDivLeftWFLIST
        Private m_strhtxtDivTopWFLIST As String                     'htxtDivTopWFLIST
        Private m_strhtxtDivLeftBody As String                      'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String                       'htxtDivTopBody

        Private m_strhtxtWFLISTSessionIdQuery As String             'htxtWFLISTSessionIdQuery

        Private m_strtxtWFLISTPageIndex As String                  'txtWFLISTPageIndex
        Private m_strtxtWFLISTPageSize As String                   'txtWFLISTPageSize

        Private m_strtxtWFLISTSearch_WJBT As String                'txtWFLISTSearch_WJBT
        Private m_strtxtWFLISTSearch_LSH As String                 'txtWFLISTSearch_LSH
        Private m_strtxtWFLISTSearch_WJZH As String                'txtWFLISTSearch_WJZH
        Private m_strtxtWFLISTSearch_ZBDW As String                'txtWFLISTSearch_ZBDW
        Private m_strtxtWFLISTSearch_WJRQMin As String             'txtWFLISTSearch_WJRQMin
        Private m_strtxtWFLISTSearch_WJRQMax As String             'txtWFLISTSearch_WJRQMax
        Private m_intSelectedIndex_ddlWFLISTSearch_WJLX As Integer 'ddlWFLISTSearch_WJLX

        Private m_intPageSize_grdWFLIST As Integer
        Private m_intSelectedIndex_grdWFLIST As Integer
        Private m_intCurrentPageIndex_grdWFLIST As Integer











        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            m_strhtxtWFLISTQuery = ""
            m_strhtxtWFLISTRows = ""
            m_strhtxtWFLISTSort = ""
            m_strhtxtWFLISTSortColumnIndex = ""
            m_strhtxtWFLISTSortType = ""
            m_strhtxtDivLeftWFLIST = ""
            m_strhtxtDivTopWFLIST = ""
            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""

            m_strhtxtWFLISTSessionIdQuery = ""

            m_strtxtWFLISTPageIndex = ""
            m_strtxtWFLISTPageSize = ""

            m_strtxtWFLISTSearch_WJBT = ""
            m_strtxtWFLISTSearch_LSH = ""
            m_strtxtWFLISTSearch_WJZH = ""
            m_strtxtWFLISTSearch_ZBDW = ""
            m_strtxtWFLISTSearch_WJRQMin = ""
            m_strtxtWFLISTSearch_WJRQMax = ""
            m_intSelectedIndex_ddlWFLISTSearch_WJLX = -1

            m_intPageSize_grdWFLIST = 0
            m_intCurrentPageIndex_grdWFLIST = 0
            m_intSelectedIndex_grdWFLIST = -1
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMQzbjWorkFlowList)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub
















        '----------------------------------------------------------------
        ' htxtWFLISTQuery属性
        '----------------------------------------------------------------
        Public Property htxtWFLISTQuery() As String
            Get
                htxtWFLISTQuery = m_strhtxtWFLISTQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtWFLISTQuery = Value
                Catch ex As Exception
                    m_strhtxtWFLISTQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtWFLISTRows属性
        '----------------------------------------------------------------
        Public Property htxtWFLISTRows() As String
            Get
                htxtWFLISTRows = m_strhtxtWFLISTRows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtWFLISTRows = Value
                Catch ex As Exception
                    m_strhtxtWFLISTRows = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtWFLISTSort属性
        '----------------------------------------------------------------
        Public Property htxtWFLISTSort() As String
            Get
                htxtWFLISTSort = m_strhtxtWFLISTSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtWFLISTSort = Value
                Catch ex As Exception
                    m_strhtxtWFLISTSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtWFLISTSortColumnIndex属性
        '----------------------------------------------------------------
        Public Property htxtWFLISTSortColumnIndex() As String
            Get
                htxtWFLISTSortColumnIndex = m_strhtxtWFLISTSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtWFLISTSortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtWFLISTSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtWFLISTSortType属性
        '----------------------------------------------------------------
        Public Property htxtWFLISTSortType() As String
            Get
                htxtWFLISTSortType = m_strhtxtWFLISTSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtWFLISTSortType = Value
                Catch ex As Exception
                    m_strhtxtWFLISTSortType = ""
                End Try
            End Set
        End Property












        '----------------------------------------------------------------
        ' htxtDivLeftWFLIST属性
        '----------------------------------------------------------------
        Public Property htxtDivLeftWFLIST() As String
            Get
                htxtDivLeftWFLIST = m_strhtxtDivLeftWFLIST
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftWFLIST = Value
                Catch ex As Exception
                    m_strhtxtDivLeftWFLIST = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopWFLIST属性
        '----------------------------------------------------------------
        Public Property htxtDivTopWFLIST() As String
            Get
                htxtDivTopWFLIST = m_strhtxtDivTopWFLIST
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopWFLIST = Value
                Catch ex As Exception
                    m_strhtxtDivTopWFLIST = ""
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
        ' htxtWFLISTSessionIdQuery属性
        '----------------------------------------------------------------
        Public Property htxtWFLISTSessionIdQuery() As String
            Get
                htxtWFLISTSessionIdQuery = m_strhtxtWFLISTSessionIdQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtWFLISTSessionIdQuery = Value
                Catch ex As Exception
                    m_strhtxtWFLISTSessionIdQuery = ""
                End Try
            End Set
        End Property













        '----------------------------------------------------------------
        ' txtWFLISTPageIndex属性
        '----------------------------------------------------------------
        Public Property txtWFLISTPageIndex() As String
            Get
                txtWFLISTPageIndex = m_strtxtWFLISTPageIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtWFLISTPageIndex = Value
                Catch ex As Exception
                    m_strtxtWFLISTPageIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtWFLISTPageSize属性
        '----------------------------------------------------------------
        Public Property txtWFLISTPageSize() As String
            Get
                txtWFLISTPageSize = m_strtxtWFLISTPageSize
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtWFLISTPageSize = Value
                Catch ex As Exception
                    m_strtxtWFLISTPageSize = ""
                End Try
            End Set
        End Property














        '----------------------------------------------------------------
        ' txtWFLISTSearch_WJBT属性
        '----------------------------------------------------------------
        Public Property txtWFLISTSearch_WJBT() As String
            Get
                txtWFLISTSearch_WJBT = m_strtxtWFLISTSearch_WJBT
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtWFLISTSearch_WJBT = Value
                Catch ex As Exception
                    m_strtxtWFLISTSearch_WJBT = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtWFLISTSearch_LSH属性
        '----------------------------------------------------------------
        Public Property txtWFLISTSearch_LSH() As String
            Get
                txtWFLISTSearch_LSH = m_strtxtWFLISTSearch_LSH
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtWFLISTSearch_LSH = Value
                Catch ex As Exception
                    m_strtxtWFLISTSearch_LSH = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtWFLISTSearch_WJZH属性
        '----------------------------------------------------------------
        Public Property txtWFLISTSearch_WJZH() As String
            Get
                txtWFLISTSearch_WJZH = m_strtxtWFLISTSearch_WJZH
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtWFLISTSearch_WJZH = Value
                Catch ex As Exception
                    m_strtxtWFLISTSearch_WJZH = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtWFLISTSearch_ZBDW属性
        '----------------------------------------------------------------
        Public Property txtWFLISTSearch_ZBDW() As String
            Get
                txtWFLISTSearch_ZBDW = m_strtxtWFLISTSearch_ZBDW
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtWFLISTSearch_ZBDW = Value
                Catch ex As Exception
                    m_strtxtWFLISTSearch_ZBDW = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtWFLISTSearch_WJRQMin属性
        '----------------------------------------------------------------
        Public Property txtWFLISTSearch_WJRQMin() As String
            Get
                txtWFLISTSearch_WJRQMin = m_strtxtWFLISTSearch_WJRQMin
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtWFLISTSearch_WJRQMin = Value
                Catch ex As Exception
                    m_strtxtWFLISTSearch_WJRQMin = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtWFLISTSearch_WJRQMax属性
        '----------------------------------------------------------------
        Public Property txtWFLISTSearch_WJRQMax() As String
            Get
                txtWFLISTSearch_WJRQMax = m_strtxtWFLISTSearch_WJRQMax
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtWFLISTSearch_WJRQMax = Value
                Catch ex As Exception
                    m_strtxtWFLISTSearch_WJRQMax = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' ddlWFLISTSearch_WJLX_SelectedIndex属性
        '----------------------------------------------------------------
        Public Property ddlWFLISTSearch_WJLX_SelectedIndex() As Integer
            Get
                ddlWFLISTSearch_WJLX_SelectedIndex = m_intSelectedIndex_ddlWFLISTSearch_WJLX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_ddlWFLISTSearch_WJLX = Value
                Catch ex As Exception
                    m_intSelectedIndex_ddlWFLISTSearch_WJLX = -1
                End Try
            End Set
        End Property














        '----------------------------------------------------------------
        ' grdWFLISTPageSize属性
        '----------------------------------------------------------------
        Public Property grdWFLISTPageSize() As Integer
            Get
                grdWFLISTPageSize = m_intPageSize_grdWFLIST
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_grdWFLIST = Value
                Catch ex As Exception
                    m_intPageSize_grdWFLIST = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdWFLISTCurrentPageIndex属性
        '----------------------------------------------------------------
        Public Property grdWFLISTCurrentPageIndex() As Integer
            Get
                grdWFLISTCurrentPageIndex = m_intCurrentPageIndex_grdWFLIST
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intCurrentPageIndex_grdWFLIST = Value
                Catch ex As Exception
                    m_intCurrentPageIndex_grdWFLIST = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdWFLISTSelectedIndex属性
        '----------------------------------------------------------------
        Public Property grdWFLISTSelectedIndex() As Integer
            Get
                grdWFLISTSelectedIndex = m_intSelectedIndex_grdWFLIST
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_grdWFLIST = Value
                Catch ex As Exception
                    m_intSelectedIndex_grdWFLIST = -1
                End Try
            End Set
        End Property

    End Class

End Namespace
