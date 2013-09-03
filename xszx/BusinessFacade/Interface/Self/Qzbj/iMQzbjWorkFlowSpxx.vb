Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IMQzbjWorkFlowSpxx
    '
    ' 功能描述： 
    '     qzbj_workflow_spxx.aspx模块本身恢复现场需要的信息
    '----------------------------------------------------------------
    <Serializable()> Public Class IMQzbjWorkFlowSpxx
        Implements IDisposable

        Private m_strhtxtSPYJQuery As String                      'htxtSPYJQuery
        Private m_strhtxtSPYJRows As String                       'htxtSPYJRows
        Private m_strhtxtSPYJSort As String                       'htxtSPYJSort
        Private m_strhtxtSPYJSortColumnIndex As String            'htxtSPYJSortColumnIndex
        Private m_strhtxtSPYJSortType As String                   'htxtSPYJSortType
        Private m_strhtxtDivLeftSPYJ As String                    'htxtDivLeftSPYJ
        Private m_strhtxtDivTopSPYJ As String                     'htxtDivTopSPYJ
        Private m_strhtxtDivLeftBody As String                    'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String                     'htxtDivTopBody

        Private m_strhtxtSPYJSessionIdQuery As String             'htxtSPYJSessionIdQuery

        Private m_strtxtSPYJPageIndex As String                  'txtSPYJPageIndex
        Private m_strtxtSPYJPageSize As String                   'txtSPYJPageSize
        Private m_strtxtSPYJSearch_JSR As String                 'txtSPYJSearch_JSR
        Private m_strtxtSPYJSearch_DLR As String                 'txtSPYJSearch_DLR
        Private m_strtxtSPYJSearch_BLSY As String                'txtSPYJSearch_BLSY
        Private m_strtxtSPYJSearch_QPRQMin As String             'txtSPYJSearch_QPRQMin
        Private m_strtxtSPYJSearch_QPRQMax As String             'txtSPYJSearch_QPRQMax

        Private m_intPageSize_grdSPYJ As Integer
        Private m_intSelectedIndex_grdSPYJ As Integer
        Private m_intCurrentPageIndex_grdSPYJ As Integer










        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            m_strhtxtSPYJQuery = ""
            m_strhtxtSPYJRows = ""
            m_strhtxtSPYJSort = ""
            m_strhtxtSPYJSortColumnIndex = ""
            m_strhtxtSPYJSortType = ""
            m_strhtxtDivLeftSPYJ = ""
            m_strhtxtDivTopSPYJ = ""
            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""

            m_strhtxtSPYJSessionIdQuery = ""

            m_strtxtSPYJPageIndex = ""
            m_strtxtSPYJPageSize = ""
            m_strtxtSPYJSearch_JSR = ""
            m_strtxtSPYJSearch_DLR = ""
            m_strtxtSPYJSearch_BLSY = ""
            m_strtxtSPYJSearch_QPRQMin = ""
            m_strtxtSPYJSearch_QPRQMax = ""

            m_intPageSize_grdSPYJ = 0
            m_intCurrentPageIndex_grdSPYJ = 0
            m_intSelectedIndex_grdSPYJ = -1
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMQzbjWorkFlowSpxx)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub














        '----------------------------------------------------------------
        ' htxtSPYJQuery属性
        '----------------------------------------------------------------
        Public Property htxtSPYJQuery() As String
            Get
                htxtSPYJQuery = m_strhtxtSPYJQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtSPYJQuery = Value
                Catch ex As Exception
                    m_strhtxtSPYJQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtSPYJRows属性
        '----------------------------------------------------------------
        Public Property htxtSPYJRows() As String
            Get
                htxtSPYJRows = m_strhtxtSPYJRows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtSPYJRows = Value
                Catch ex As Exception
                    m_strhtxtSPYJRows = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtSPYJSort属性
        '----------------------------------------------------------------
        Public Property htxtSPYJSort() As String
            Get
                htxtSPYJSort = m_strhtxtSPYJSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtSPYJSort = Value
                Catch ex As Exception
                    m_strhtxtSPYJSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtSPYJSortColumnIndex属性
        '----------------------------------------------------------------
        Public Property htxtSPYJSortColumnIndex() As String
            Get
                htxtSPYJSortColumnIndex = m_strhtxtSPYJSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtSPYJSortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtSPYJSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtSPYJSortType属性
        '----------------------------------------------------------------
        Public Property htxtSPYJSortType() As String
            Get
                htxtSPYJSortType = m_strhtxtSPYJSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtSPYJSortType = Value
                Catch ex As Exception
                    m_strhtxtSPYJSortType = ""
                End Try
            End Set
        End Property














        '----------------------------------------------------------------
        ' htxtDivLeftSPYJ属性
        '----------------------------------------------------------------
        Public Property htxtDivLeftSPYJ() As String
            Get
                htxtDivLeftSPYJ = m_strhtxtDivLeftSPYJ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftSPYJ = Value
                Catch ex As Exception
                    m_strhtxtDivLeftSPYJ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopSPYJ属性
        '----------------------------------------------------------------
        Public Property htxtDivTopSPYJ() As String
            Get
                htxtDivTopSPYJ = m_strhtxtDivTopSPYJ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopSPYJ = Value
                Catch ex As Exception
                    m_strhtxtDivTopSPYJ = ""
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
        ' htxtSPYJSessionIdQuery属性
        '----------------------------------------------------------------
        Public Property htxtSPYJSessionIdQuery() As String
            Get
                htxtSPYJSessionIdQuery = m_strhtxtSPYJSessionIdQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtSPYJSessionIdQuery = Value
                Catch ex As Exception
                    m_strhtxtSPYJSessionIdQuery = ""
                End Try
            End Set
        End Property














        '----------------------------------------------------------------
        ' txtSPYJPageIndex属性
        '----------------------------------------------------------------
        Public Property txtSPYJPageIndex() As String
            Get
                txtSPYJPageIndex = m_strtxtSPYJPageIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSPYJPageIndex = Value
                Catch ex As Exception
                    m_strtxtSPYJPageIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtSPYJPageSize属性
        '----------------------------------------------------------------
        Public Property txtSPYJPageSize() As String
            Get
                txtSPYJPageSize = m_strtxtSPYJPageSize
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSPYJPageSize = Value
                Catch ex As Exception
                    m_strtxtSPYJPageSize = ""
                End Try
            End Set
        End Property
















        '----------------------------------------------------------------
        ' txtSPYJSearch_JSR属性
        '----------------------------------------------------------------
        Public Property txtSPYJSearch_JSR() As String
            Get
                txtSPYJSearch_JSR = m_strtxtSPYJSearch_JSR
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSPYJSearch_JSR = Value
                Catch ex As Exception
                    m_strtxtSPYJSearch_JSR = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtSPYJSearch_DLR属性
        '----------------------------------------------------------------
        Public Property txtSPYJSearch_DLR() As String
            Get
                txtSPYJSearch_DLR = m_strtxtSPYJSearch_DLR
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSPYJSearch_DLR = Value
                Catch ex As Exception
                    m_strtxtSPYJSearch_DLR = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtSPYJSearch_BLSY属性
        '----------------------------------------------------------------
        Public Property txtSPYJSearch_BLSY() As String
            Get
                txtSPYJSearch_BLSY = m_strtxtSPYJSearch_BLSY
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSPYJSearch_BLSY = Value
                Catch ex As Exception
                    m_strtxtSPYJSearch_BLSY = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtSPYJSearch_QPRQMin属性
        '----------------------------------------------------------------
        Public Property txtSPYJSearch_QPRQMin() As String
            Get
                txtSPYJSearch_QPRQMin = m_strtxtSPYJSearch_QPRQMin
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSPYJSearch_QPRQMin = Value
                Catch ex As Exception
                    m_strtxtSPYJSearch_QPRQMin = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtSPYJSearch_QPRQMax属性
        '----------------------------------------------------------------
        Public Property txtSPYJSearch_QPRQMax() As String
            Get
                txtSPYJSearch_QPRQMax = m_strtxtSPYJSearch_QPRQMax
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSPYJSearch_QPRQMax = Value
                Catch ex As Exception
                    m_strtxtSPYJSearch_QPRQMax = ""
                End Try
            End Set
        End Property
















        '----------------------------------------------------------------
        ' grdSPYJPageSize属性
        '----------------------------------------------------------------
        Public Property grdSPYJPageSize() As Integer
            Get
                grdSPYJPageSize = m_intPageSize_grdSPYJ
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_grdSPYJ = Value
                Catch ex As Exception
                    m_intPageSize_grdSPYJ = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdSPYJCurrentPageIndex属性
        '----------------------------------------------------------------
        Public Property grdSPYJCurrentPageIndex() As Integer
            Get
                grdSPYJCurrentPageIndex = m_intCurrentPageIndex_grdSPYJ
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intCurrentPageIndex_grdSPYJ = Value
                Catch ex As Exception
                    m_intCurrentPageIndex_grdSPYJ = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdSPYJSelectedIndex属性
        '----------------------------------------------------------------
        Public Property grdSPYJSelectedIndex() As Integer
            Get
                grdSPYJSelectedIndex = m_intSelectedIndex_grdSPYJ
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_grdSPYJ = Value
                Catch ex As Exception
                    m_intSelectedIndex_grdSPYJ = 0
                End Try
            End Set
        End Property

    End Class

End Namespace
