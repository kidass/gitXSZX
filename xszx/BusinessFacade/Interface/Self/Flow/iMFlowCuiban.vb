Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IMFlowCuiban
    '
    ' 功能描述： 
    '     flow_cuiban.aspx模块本身恢复现场需要的信息
    '----------------------------------------------------------------
    <Serializable()> Public Class IMFlowCuiban
        Implements IDisposable

        '----------------------------------------------------------------
        'hidden textbox
        '----------------------------------------------------------------
        Private m_strhtxtYCBXXQuery As String                            'htxtYCBXXQuery
        Private m_strhtxtYCBXXRows As String                             'htxtYCBXXRows
        Private m_strhtxtYCBXXSort As String                             'htxtYCBXXSort
        Private m_strhtxtYCBXXSortColumnIndex As String                  'htxtYCBXXSortColumnIndex
        Private m_strhtxtYCBXXSortType As String                         'htxtYCBXXSortType
        Private m_strhtxtKCBXXQuery As String                            'htxtKCBXXQuery
        Private m_strhtxtKCBXXRows As String                             'htxtKCBXXRows
        Private m_strhtxtKCBXXSort As String                             'htxtKCBXXSort
        Private m_strhtxtKCBXXSortColumnIndex As String                  'htxtKCBXXSortColumnIndex
        Private m_strhtxtKCBXXSortType As String                         'htxtKCBXXSortType
        Private m_strhtxtDivLeftYCBXX As String                          'htxtDivLeftYCBXX
        Private m_strhtxtDivTopYCBXX As String                           'htxtDivTopYCBXX
        Private m_strhtxtDivLeftKCBXX As String                          'htxtDivLeftKCBXX
        Private m_strhtxtDivTopKCBXX As String                           'htxtDivTopKCBXX
        Private m_strhtxtDivLeftBody As String                           'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String                            'htxtDivTopBody

        '----------------------------------------------------------------
        'grdKCBXX paramters
        '----------------------------------------------------------------
        Private m_strhtxtSessionIdKCBXX As String                        'SessionId
        Private m_intPageSize_KCBXX As Integer                           'grdKCBXX的页大小
        Private m_intSelectedIndex_KCBXX As Integer                      'grdKCBXX的行索引
        Private m_intCurrentPageIndex_KCBXX As Integer                   'grdKCBXX的页索引

        '----------------------------------------------------------------
        'grdYCBXX paramters
        '----------------------------------------------------------------
        Private m_intPageSize_YCBXX As Integer                           'grdYCBXX的页大小
        Private m_intSelectedIndex_YCBXX As Integer                      'grdYCBXX的行索引
        Private m_intCurrentPageIndex_YCBXX As Integer                   'grdYCBXX的页索引













        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()

            MyBase.New()

            m_strhtxtYCBXXQuery = ""
            m_strhtxtYCBXXRows = ""
            m_strhtxtYCBXXSort = ""
            m_strhtxtYCBXXSortColumnIndex = ""
            m_strhtxtYCBXXSortType = ""

            m_strhtxtKCBXXQuery = ""
            m_strhtxtKCBXXRows = ""
            m_strhtxtKCBXXSort = ""
            m_strhtxtKCBXXSortColumnIndex = ""
            m_strhtxtKCBXXSortType = ""

            m_strhtxtDivLeftYCBXX = ""
            m_strhtxtDivTopYCBXX = ""

            m_strhtxtDivLeftKCBXX = ""
            m_strhtxtDivTopKCBXX = ""

            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""

            m_strhtxtSessionIdKCBXX = ""
            m_intPageSize_KCBXX = 100
            m_intSelectedIndex_KCBXX = -1
            m_intCurrentPageIndex_KCBXX = 0

            m_intPageSize_YCBXX = 100
            m_intSelectedIndex_YCBXX = -1
            m_intCurrentPageIndex_YCBXX = 0

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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMFlowCuiban)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub














        '----------------------------------------------------------------
        ' htxtYCBXXSort属性
        '----------------------------------------------------------------
        Public Property htxtYCBXXSort() As String
            Get
                htxtYCBXXSort = m_strhtxtYCBXXSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtYCBXXSort = Value
                Catch ex As Exception
                    m_strhtxtYCBXXSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtYCBXXRows属性
        '----------------------------------------------------------------
        Public Property htxtYCBXXRows() As String
            Get
                htxtYCBXXRows = m_strhtxtYCBXXRows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtYCBXXRows = Value
                Catch ex As Exception
                    m_strhtxtYCBXXRows = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtYCBXXSortColumnIndex属性
        '----------------------------------------------------------------
        Public Property htxtYCBXXSortColumnIndex() As String
            Get
                htxtYCBXXSortColumnIndex = m_strhtxtYCBXXSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtYCBXXSortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtYCBXXSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtYCBXXQuery属性
        '----------------------------------------------------------------
        Public Property htxtYCBXXQuery() As String
            Get
                htxtYCBXXQuery = m_strhtxtYCBXXQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtYCBXXQuery = Value
                Catch ex As Exception
                    m_strhtxtYCBXXQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtYCBXXSortType属性
        '----------------------------------------------------------------
        Public Property htxtYCBXXSortType() As String
            Get
                htxtYCBXXSortType = m_strhtxtYCBXXSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtYCBXXSortType = Value
                Catch ex As Exception
                    m_strhtxtYCBXXSortType = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' htxtKCBXXSort属性
        '----------------------------------------------------------------
        Public Property htxtKCBXXSort() As String
            Get
                htxtKCBXXSort = m_strhtxtKCBXXSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtKCBXXSort = Value
                Catch ex As Exception
                    m_strhtxtKCBXXSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtKCBXXRows属性
        '----------------------------------------------------------------
        Public Property htxtKCBXXRows() As String
            Get
                htxtKCBXXRows = m_strhtxtKCBXXRows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtKCBXXRows = Value
                Catch ex As Exception
                    m_strhtxtKCBXXRows = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtKCBXXSortColumnIndex属性
        '----------------------------------------------------------------
        Public Property htxtKCBXXSortColumnIndex() As String
            Get
                htxtKCBXXSortColumnIndex = m_strhtxtKCBXXSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtKCBXXSortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtKCBXXSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtKCBXXQuery属性
        '----------------------------------------------------------------
        Public Property htxtKCBXXQuery() As String
            Get
                htxtKCBXXQuery = m_strhtxtKCBXXQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtKCBXXQuery = Value
                Catch ex As Exception
                    m_strhtxtKCBXXQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtKCBXXSortType属性
        '----------------------------------------------------------------
        Public Property htxtKCBXXSortType() As String
            Get
                htxtKCBXXSortType = m_strhtxtKCBXXSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtKCBXXSortType = Value
                Catch ex As Exception
                    m_strhtxtKCBXXSortType = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' htxtDivLeftYCBXX属性
        '----------------------------------------------------------------
        Public Property htxtDivLeftYCBXX() As String
            Get
                htxtDivLeftYCBXX = m_strhtxtDivLeftYCBXX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftYCBXX = Value
                Catch ex As Exception
                    m_strhtxtDivLeftYCBXX = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopYCBXX属性
        '----------------------------------------------------------------
        Public Property htxtDivTopYCBXX() As String
            Get
                htxtDivTopYCBXX = m_strhtxtDivTopYCBXX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopYCBXX = Value
                Catch ex As Exception
                    m_strhtxtDivTopYCBXX = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivLeftKCBXX属性
        '----------------------------------------------------------------
        Public Property htxtDivLeftKCBXX() As String
            Get
                htxtDivLeftKCBXX = m_strhtxtDivLeftKCBXX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftKCBXX = Value
                Catch ex As Exception
                    m_strhtxtDivLeftKCBXX = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopKCBXX属性
        '----------------------------------------------------------------
        Public Property htxtDivTopKCBXX() As String
            Get
                htxtDivTopKCBXX = m_strhtxtDivTopKCBXX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopKCBXX = Value
                Catch ex As Exception
                    m_strhtxtDivTopKCBXX = ""
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
        ' htxtSessionIdKCBXX属性
        '----------------------------------------------------------------
        Public Property htxtSessionIdKCBXX() As String
            Get
                htxtSessionIdKCBXX = m_strhtxtSessionIdKCBXX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtSessionIdKCBXX = Value
                Catch ex As Exception
                    m_strhtxtSessionIdKCBXX = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdKCBXX_PageSize属性
        '----------------------------------------------------------------
        Public Property grdKCBXX_PageSize() As Integer
            Get
                grdKCBXX_PageSize = m_intPageSize_KCBXX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_KCBXX = Value
                Catch ex As Exception
                    m_intPageSize_KCBXX = 100
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdKCBXX_SelectedIndex属性
        '----------------------------------------------------------------
        Public Property grdKCBXX_SelectedIndex() As Integer
            Get
                grdKCBXX_SelectedIndex = m_intSelectedIndex_KCBXX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_KCBXX = Value
                Catch ex As Exception
                    m_intSelectedIndex_KCBXX = -1
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdKCBXX_CurrentPageIndex属性
        '----------------------------------------------------------------
        Public Property grdKCBXX_CurrentPageIndex() As Integer
            Get
                grdKCBXX_CurrentPageIndex = m_intCurrentPageIndex_KCBXX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intCurrentPageIndex_KCBXX = Value
                Catch ex As Exception
                    m_intCurrentPageIndex_KCBXX = -1
                End Try
            End Set
        End Property



        '----------------------------------------------------------------
        ' grdYCBXX_PageSize属性
        '----------------------------------------------------------------
        Public Property grdYCBXX_PageSize() As Integer
            Get
                grdYCBXX_PageSize = m_intPageSize_YCBXX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_YCBXX = Value
                Catch ex As Exception
                    m_intPageSize_YCBXX = 100
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdYCBXX_SelectedIndex属性
        '----------------------------------------------------------------
        Public Property grdYCBXX_SelectedIndex() As Integer
            Get
                grdYCBXX_SelectedIndex = m_intSelectedIndex_YCBXX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_YCBXX = Value
                Catch ex As Exception
                    m_intSelectedIndex_YCBXX = -1
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdYCBXX_CurrentPageIndex属性
        '----------------------------------------------------------------
        Public Property grdYCBXX_CurrentPageIndex() As Integer
            Get
                grdYCBXX_CurrentPageIndex = m_intCurrentPageIndex_YCBXX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intCurrentPageIndex_YCBXX = Value
                Catch ex As Exception
                    m_intCurrentPageIndex_YCBXX = -1
                End Try
            End Set
        End Property
    End Class

End Namespace
