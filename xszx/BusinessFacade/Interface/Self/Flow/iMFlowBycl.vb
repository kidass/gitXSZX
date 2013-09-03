Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IMFlowBycl
    '
    ' 功能描述： 
    '     flow_bycl.aspx模块本身恢复现场需要的信息
    '----------------------------------------------------------------
    <Serializable()> Public Class IMFlowBycl
        Implements IDisposable

        '----------------------------------------------------------------
        'hidden textbox
        '----------------------------------------------------------------
        Private m_strhtxtSGWXXQuery As String                            'htxtSGWXXQuery
        Private m_strhtxtSGWXXRows As String                             'htxtSGWXXRows
        Private m_strhtxtSGWXXSort As String                             'htxtSGWXXSort
        Private m_strhtxtSGWXXSortColumnIndex As String                  'htxtSGWXXSortColumnIndex
        Private m_strhtxtSGWXXSortType As String                         'htxtSGWXXSortType
        Private m_strhtxtWSCXXQuery As String                            'htxtWSCXXQuery
        Private m_strhtxtWSCXXRows As String                             'htxtWSCXXRows
        Private m_strhtxtWSCXXSort As String                             'htxtWSCXXSort
        Private m_strhtxtWSCXXSortColumnIndex As String                  'htxtWSCXXSortColumnIndex
        Private m_strhtxtWSCXXSortType As String                         'htxtWSCXXSortType
        Private m_strhtxtDivLeftSGWXX As String                          'htxtDivLeftSGWXX
        Private m_strhtxtDivTopSGWXX As String                           'htxtDivTopSGWXX
        Private m_strhtxtDivLeftWSCXX As String                          'htxtDivLeftWSCXX
        Private m_strhtxtDivTopWSCXX As String                           'htxtDivTopWSCXX
        Private m_strhtxtDivLeftBody As String                           'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String                            'htxtDivTopBody

        Private m_strhtxtValueA As String                                'htxtValueA
        Private m_strhtxtValueB As String                                'htxtValueB

        '----------------------------------------------------------------
        'grdWSCXX paramters
        '----------------------------------------------------------------
        Private m_intPageSize_WSCXX As Integer                           'grdWSCXX的页大小
        Private m_intSelectedIndex_WSCXX As Integer                      'grdWSCXX的行索引
        Private m_intCurrentPageIndex_WSCXX As Integer                   'grdWSCXX的页索引

        '----------------------------------------------------------------
        'grdSGWXX paramters
        '----------------------------------------------------------------
        Private m_intPageSize_SGWXX As Integer                            'grdSGWXX的页大小
        Private m_intSelectedIndex_SGWXX As Integer                       'grdSGWXX的行索引
        Private m_intCurrentPageIndex_SGWXX As Integer                    'grdSGWXX的页索引











        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()

            MyBase.New()

            m_strhtxtSGWXXQuery = ""
            m_strhtxtSGWXXRows = ""
            m_strhtxtSGWXXSort = ""
            m_strhtxtSGWXXSortColumnIndex = ""
            m_strhtxtSGWXXSortType = ""

            m_strhtxtWSCXXQuery = ""
            m_strhtxtWSCXXRows = ""
            m_strhtxtWSCXXSort = ""
            m_strhtxtWSCXXSortColumnIndex = ""
            m_strhtxtWSCXXSortType = ""

            m_strhtxtDivLeftSGWXX = ""
            m_strhtxtDivTopSGWXX = ""

            m_strhtxtDivLeftWSCXX = ""
            m_strhtxtDivTopWSCXX = ""

            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""

            m_intPageSize_WSCXX = 100
            m_intSelectedIndex_WSCXX = -1
            m_intCurrentPageIndex_WSCXX = 0

            m_intPageSize_SGWXX = 100
            m_intSelectedIndex_SGWXX = -1
            m_intCurrentPageIndex_SGWXX = 0

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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMFlowBycl)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub











        '----------------------------------------------------------------
        ' htxtSGWXXSort属性
        '----------------------------------------------------------------
        Public Property htxtSGWXXSort() As String
            Get
                htxtSGWXXSort = m_strhtxtSGWXXSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtSGWXXSort = Value
                Catch ex As Exception
                    m_strhtxtSGWXXSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtSGWXXRows属性
        '----------------------------------------------------------------
        Public Property htxtSGWXXRows() As String
            Get
                htxtSGWXXRows = m_strhtxtSGWXXRows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtSGWXXRows = Value
                Catch ex As Exception
                    m_strhtxtSGWXXRows = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtSGWXXSortColumnIndex属性
        '----------------------------------------------------------------
        Public Property htxtSGWXXSortColumnIndex() As String
            Get
                htxtSGWXXSortColumnIndex = m_strhtxtSGWXXSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtSGWXXSortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtSGWXXSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtSGWXXQuery属性
        '----------------------------------------------------------------
        Public Property htxtSGWXXQuery() As String
            Get
                htxtSGWXXQuery = m_strhtxtSGWXXQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtSGWXXQuery = Value
                Catch ex As Exception
                    m_strhtxtSGWXXQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtSGWXXSortType属性
        '----------------------------------------------------------------
        Public Property htxtSGWXXSortType() As String
            Get
                htxtSGWXXSortType = m_strhtxtSGWXXSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtSGWXXSortType = Value
                Catch ex As Exception
                    m_strhtxtSGWXXSortType = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' htxtWSCXXSort属性
        '----------------------------------------------------------------
        Public Property htxtWSCXXSort() As String
            Get
                htxtWSCXXSort = m_strhtxtWSCXXSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtWSCXXSort = Value
                Catch ex As Exception
                    m_strhtxtWSCXXSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtWSCXXRows属性
        '----------------------------------------------------------------
        Public Property htxtWSCXXRows() As String
            Get
                htxtWSCXXRows = m_strhtxtWSCXXRows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtWSCXXRows = Value
                Catch ex As Exception
                    m_strhtxtWSCXXRows = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtWSCXXSortColumnIndex属性
        '----------------------------------------------------------------
        Public Property htxtWSCXXSortColumnIndex() As String
            Get
                htxtWSCXXSortColumnIndex = m_strhtxtWSCXXSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtWSCXXSortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtWSCXXSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtWSCXXQuery属性
        '----------------------------------------------------------------
        Public Property htxtWSCXXQuery() As String
            Get
                htxtWSCXXQuery = m_strhtxtWSCXXQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtWSCXXQuery = Value
                Catch ex As Exception
                    m_strhtxtWSCXXQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtWSCXXSortType属性
        '----------------------------------------------------------------
        Public Property htxtWSCXXSortType() As String
            Get
                htxtWSCXXSortType = m_strhtxtWSCXXSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtWSCXXSortType = Value
                Catch ex As Exception
                    m_strhtxtWSCXXSortType = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' htxtDivLeftSGWXX属性
        '----------------------------------------------------------------
        Public Property htxtDivLeftSGWXX() As String
            Get
                htxtDivLeftSGWXX = m_strhtxtDivLeftSGWXX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftSGWXX = Value
                Catch ex As Exception
                    m_strhtxtDivLeftSGWXX = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopSGWXX属性
        '----------------------------------------------------------------
        Public Property htxtDivTopSGWXX() As String
            Get
                htxtDivTopSGWXX = m_strhtxtDivTopSGWXX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopSGWXX = Value
                Catch ex As Exception
                    m_strhtxtDivTopSGWXX = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivLeftWSCXX属性
        '----------------------------------------------------------------
        Public Property htxtDivLeftWSCXX() As String
            Get
                htxtDivLeftWSCXX = m_strhtxtDivLeftWSCXX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftWSCXX = Value
                Catch ex As Exception
                    m_strhtxtDivLeftWSCXX = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopWSCXX属性
        '----------------------------------------------------------------
        Public Property htxtDivTopWSCXX() As String
            Get
                htxtDivTopWSCXX = m_strhtxtDivTopWSCXX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopWSCXX = Value
                Catch ex As Exception
                    m_strhtxtDivTopWSCXX = ""
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
        ' htxtValueA属性
        '----------------------------------------------------------------
        Public Property htxtValueA() As String
            Get
                htxtValueA = m_strhtxtValueA
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtValueA = Value
                Catch ex As Exception
                    m_strhtxtValueA = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtValueB属性
        '----------------------------------------------------------------
        Public Property htxtValueB() As String
            Get
                htxtValueB = m_strhtxtValueB
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtValueB = Value
                Catch ex As Exception
                    m_strhtxtValueB = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' grdWSCXX_PageSize属性
        '----------------------------------------------------------------
        Public Property grdWSCXX_PageSize() As Integer
            Get
                grdWSCXX_PageSize = m_intPageSize_WSCXX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_WSCXX = Value
                Catch ex As Exception
                    m_intPageSize_WSCXX = 100
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdWSCXX_SelectedIndex属性
        '----------------------------------------------------------------
        Public Property grdWSCXX_SelectedIndex() As Integer
            Get
                grdWSCXX_SelectedIndex = m_intSelectedIndex_WSCXX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_WSCXX = Value
                Catch ex As Exception
                    m_intSelectedIndex_WSCXX = -1
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdWSCXX_CurrentPageIndex属性
        '----------------------------------------------------------------
        Public Property grdWSCXX_CurrentPageIndex() As Integer
            Get
                grdWSCXX_CurrentPageIndex = m_intCurrentPageIndex_WSCXX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intCurrentPageIndex_WSCXX = Value
                Catch ex As Exception
                    m_intCurrentPageIndex_WSCXX = -1
                End Try
            End Set
        End Property



        '----------------------------------------------------------------
        ' grdSGWXX_PageSize属性
        '----------------------------------------------------------------
        Public Property grdSGWXX_PageSize() As Integer
            Get
                grdSGWXX_PageSize = m_intPageSize_SGWXX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_SGWXX = Value
                Catch ex As Exception
                    m_intPageSize_SGWXX = 100
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdSGWXX_SelectedIndex属性
        '----------------------------------------------------------------
        Public Property grdSGWXX_SelectedIndex() As Integer
            Get
                grdSGWXX_SelectedIndex = m_intSelectedIndex_SGWXX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_SGWXX = Value
                Catch ex As Exception
                    m_intSelectedIndex_SGWXX = -1
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdSGWXX_CurrentPageIndex属性
        '----------------------------------------------------------------
        Public Property grdSGWXX_CurrentPageIndex() As Integer
            Get
                grdSGWXX_CurrentPageIndex = m_intCurrentPageIndex_SGWXX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intCurrentPageIndex_SGWXX = Value
                Catch ex As Exception
                    m_intCurrentPageIndex_SGWXX = -1
                End Try
            End Set
        End Property

    End Class

End Namespace
