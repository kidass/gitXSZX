Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IMFlowDuban
    '
    ' 功能描述： 
    '     flow_duban.aspx模块本身恢复现场需要的信息
    '----------------------------------------------------------------
    <Serializable()> Public Class IMFlowDuban
        Implements IDisposable

        '----------------------------------------------------------------
        'hidden textbox
        '----------------------------------------------------------------
        Private m_strhtxtYDBXXQuery As String                            'htxtYDBXXQuery
        Private m_strhtxtYDBXXRows As String                             'htxtYDBXXRows
        Private m_strhtxtYDBXXSort As String                             'htxtYDBXXSort
        Private m_strhtxtYDBXXSortColumnIndex As String                  'htxtYDBXXSortColumnIndex
        Private m_strhtxtYDBXXSortType As String                         'htxtYDBXXSortType
        Private m_strhtxtKDBXXQuery As String                            'htxtKDBXXQuery
        Private m_strhtxtKDBXXRows As String                             'htxtKDBXXRows
        Private m_strhtxtKDBXXSort As String                             'htxtKDBXXSort
        Private m_strhtxtKDBXXSortColumnIndex As String                  'htxtKDBXXSortColumnIndex
        Private m_strhtxtKDBXXSortType As String                         'htxtKDBXXSortType
        Private m_strhtxtDivLeftYDBXX As String                          'htxtDivLeftYDBXX
        Private m_strhtxtDivTopYDBXX As String                           'htxtDivTopYDBXX
        Private m_strhtxtDivLeftKDBXX As String                          'htxtDivLeftKDBXX
        Private m_strhtxtDivTopKDBXX As String                           'htxtDivTopKDBXX
        Private m_strhtxtDivLeftBody As String                           'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String                            'htxtDivTopBody

        '----------------------------------------------------------------
        'grdKDBXX paramters
        '----------------------------------------------------------------
        Private m_strhtxtSessionIdKDBXX As String                        'SessionId
        Private m_intPageSize_KDBXX As Integer                           'grdKDBXX的页大小
        Private m_intSelectedIndex_KDBXX As Integer                      'grdKDBXX的行索引
        Private m_intCurrentPageIndex_KDBXX As Integer                   'grdKDBXX的页索引

        '----------------------------------------------------------------
        'grdYDBXX paramters
        '----------------------------------------------------------------
        Private m_intPageSize_YDBXX As Integer                           'grdYDBXX的页大小
        Private m_intSelectedIndex_YDBXX As Integer                      'grdYDBXX的行索引
        Private m_intCurrentPageIndex_YDBXX As Integer                   'grdYDBXX的页索引














        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()

            MyBase.New()

            m_strhtxtYDBXXQuery = ""
            m_strhtxtYDBXXRows = ""
            m_strhtxtYDBXXSort = ""
            m_strhtxtYDBXXSortColumnIndex = ""
            m_strhtxtYDBXXSortType = ""

            m_strhtxtKDBXXQuery = ""
            m_strhtxtKDBXXRows = ""
            m_strhtxtKDBXXSort = ""
            m_strhtxtKDBXXSortColumnIndex = ""
            m_strhtxtKDBXXSortType = ""

            m_strhtxtDivLeftYDBXX = ""
            m_strhtxtDivTopYDBXX = ""

            m_strhtxtDivLeftKDBXX = ""
            m_strhtxtDivTopKDBXX = ""

            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""

            m_strhtxtSessionIdKDBXX = ""
            m_intPageSize_KDBXX = 100
            m_intSelectedIndex_KDBXX = -1
            m_intCurrentPageIndex_KDBXX = 0

            m_intPageSize_YDBXX = 100
            m_intSelectedIndex_YDBXX = -1
            m_intCurrentPageIndex_YDBXX = 0

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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMFlowDuban)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub













        '----------------------------------------------------------------
        ' htxtYDBXXSort属性
        '----------------------------------------------------------------
        Public Property htxtYDBXXSort() As String
            Get
                htxtYDBXXSort = m_strhtxtYDBXXSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtYDBXXSort = Value
                Catch ex As Exception
                    m_strhtxtYDBXXSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtYDBXXRows属性
        '----------------------------------------------------------------
        Public Property htxtYDBXXRows() As String
            Get
                htxtYDBXXRows = m_strhtxtYDBXXRows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtYDBXXRows = Value
                Catch ex As Exception
                    m_strhtxtYDBXXRows = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtYDBXXSortColumnIndex属性
        '----------------------------------------------------------------
        Public Property htxtYDBXXSortColumnIndex() As String
            Get
                htxtYDBXXSortColumnIndex = m_strhtxtYDBXXSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtYDBXXSortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtYDBXXSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtYDBXXQuery属性
        '----------------------------------------------------------------
        Public Property htxtYDBXXQuery() As String
            Get
                htxtYDBXXQuery = m_strhtxtYDBXXQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtYDBXXQuery = Value
                Catch ex As Exception
                    m_strhtxtYDBXXQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtYDBXXSortType属性
        '----------------------------------------------------------------
        Public Property htxtYDBXXSortType() As String
            Get
                htxtYDBXXSortType = m_strhtxtYDBXXSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtYDBXXSortType = Value
                Catch ex As Exception
                    m_strhtxtYDBXXSortType = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' htxtKDBXXSort属性
        '----------------------------------------------------------------
        Public Property htxtKDBXXSort() As String
            Get
                htxtKDBXXSort = m_strhtxtKDBXXSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtKDBXXSort = Value
                Catch ex As Exception
                    m_strhtxtKDBXXSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtKDBXXRows属性
        '----------------------------------------------------------------
        Public Property htxtKDBXXRows() As String
            Get
                htxtKDBXXRows = m_strhtxtKDBXXRows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtKDBXXRows = Value
                Catch ex As Exception
                    m_strhtxtKDBXXRows = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtKDBXXSortColumnIndex属性
        '----------------------------------------------------------------
        Public Property htxtKDBXXSortColumnIndex() As String
            Get
                htxtKDBXXSortColumnIndex = m_strhtxtKDBXXSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtKDBXXSortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtKDBXXSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtKDBXXQuery属性
        '----------------------------------------------------------------
        Public Property htxtKDBXXQuery() As String
            Get
                htxtKDBXXQuery = m_strhtxtKDBXXQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtKDBXXQuery = Value
                Catch ex As Exception
                    m_strhtxtKDBXXQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtKDBXXSortType属性
        '----------------------------------------------------------------
        Public Property htxtKDBXXSortType() As String
            Get
                htxtKDBXXSortType = m_strhtxtKDBXXSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtKDBXXSortType = Value
                Catch ex As Exception
                    m_strhtxtKDBXXSortType = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' htxtDivLeftYDBXX属性
        '----------------------------------------------------------------
        Public Property htxtDivLeftYDBXX() As String
            Get
                htxtDivLeftYDBXX = m_strhtxtDivLeftYDBXX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftYDBXX = Value
                Catch ex As Exception
                    m_strhtxtDivLeftYDBXX = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopYDBXX属性
        '----------------------------------------------------------------
        Public Property htxtDivTopYDBXX() As String
            Get
                htxtDivTopYDBXX = m_strhtxtDivTopYDBXX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopYDBXX = Value
                Catch ex As Exception
                    m_strhtxtDivTopYDBXX = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivLeftKDBXX属性
        '----------------------------------------------------------------
        Public Property htxtDivLeftKDBXX() As String
            Get
                htxtDivLeftKDBXX = m_strhtxtDivLeftKDBXX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftKDBXX = Value
                Catch ex As Exception
                    m_strhtxtDivLeftKDBXX = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopKDBXX属性
        '----------------------------------------------------------------
        Public Property htxtDivTopKDBXX() As String
            Get
                htxtDivTopKDBXX = m_strhtxtDivTopKDBXX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopKDBXX = Value
                Catch ex As Exception
                    m_strhtxtDivTopKDBXX = ""
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
        ' htxtSessionIdKDBXX属性
        '----------------------------------------------------------------
        Public Property htxtSessionIdKDBXX() As String
            Get
                htxtSessionIdKDBXX = m_strhtxtSessionIdKDBXX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtSessionIdKDBXX = Value
                Catch ex As Exception
                    m_strhtxtSessionIdKDBXX = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdKDBXX_PageSize属性
        '----------------------------------------------------------------
        Public Property grdKDBXX_PageSize() As Integer
            Get
                grdKDBXX_PageSize = m_intPageSize_KDBXX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_KDBXX = Value
                Catch ex As Exception
                    m_intPageSize_KDBXX = 100
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdKDBXX_SelectedIndex属性
        '----------------------------------------------------------------
        Public Property grdKDBXX_SelectedIndex() As Integer
            Get
                grdKDBXX_SelectedIndex = m_intSelectedIndex_KDBXX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_KDBXX = Value
                Catch ex As Exception
                    m_intSelectedIndex_KDBXX = -1
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdKDBXX_CurrentPageIndex属性
        '----------------------------------------------------------------
        Public Property grdKDBXX_CurrentPageIndex() As Integer
            Get
                grdKDBXX_CurrentPageIndex = m_intCurrentPageIndex_KDBXX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intCurrentPageIndex_KDBXX = Value
                Catch ex As Exception
                    m_intCurrentPageIndex_KDBXX = -1
                End Try
            End Set
        End Property



        '----------------------------------------------------------------
        ' grdYDBXX_PageSize属性
        '----------------------------------------------------------------
        Public Property grdYDBXX_PageSize() As Integer
            Get
                grdYDBXX_PageSize = m_intPageSize_YDBXX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_YDBXX = Value
                Catch ex As Exception
                    m_intPageSize_YDBXX = 100
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdYDBXX_SelectedIndex属性
        '----------------------------------------------------------------
        Public Property grdYDBXX_SelectedIndex() As Integer
            Get
                grdYDBXX_SelectedIndex = m_intSelectedIndex_YDBXX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_YDBXX = Value
                Catch ex As Exception
                    m_intSelectedIndex_YDBXX = -1
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdYDBXX_CurrentPageIndex属性
        '----------------------------------------------------------------
        Public Property grdYDBXX_CurrentPageIndex() As Integer
            Get
                grdYDBXX_CurrentPageIndex = m_intCurrentPageIndex_YDBXX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intCurrentPageIndex_YDBXX = Value
                Catch ex As Exception
                    m_intCurrentPageIndex_YDBXX = -1
                End Try
            End Set
        End Property
    End Class

End Namespace
