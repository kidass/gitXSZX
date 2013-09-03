Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IMFlowSend
    '
    ' 功能描述： 
    '     flow_send.aspx模块本身恢复现场需要的信息
    '----------------------------------------------------------------
    <Serializable()> Public Class IMFlowSend
        Implements IDisposable

        '----------------------------------------------------------------
        'hidden textbox
        '----------------------------------------------------------------

        Private m_strhtxtEditMode As String                             'htxtEditMode
        Private m_strhtxtXRMode As String                             'htxtXRMode

        Private m_strhtxtWTXXQuery As String                             'htxtWTXXQuery
        Private m_strhtxtWTXXRows As String                              'htxtWTXXRows
        Private m_strhtxtWTXXSort As String                              'htxtWTXXSort
        Private m_strhtxtWTXXSortColumnIndex As String                   'htxtWTXXSortColumnIndex
        Private m_strhtxtWTXXSortType As String                          'htxtWTXXSortType
        Private m_strhtxtJSRXXQuery As String                            'htxtJSRXXQuery
        Private m_strhtxtJSRXXRows As String                             'htxtJSRXXRows
        Private m_strhtxtJSRXXSort As String                             'htxtJSRXXSort
        Private m_strhtxtJSRXXSortColumnIndex As String                  'htxtJSRXXSortColumnIndex
        Private m_strhtxtJSRXXSortType As String                         'htxtJSRXXSortType
        Private m_strhtxtDivLeftWTXX As String                           'htxtDivLeftWTXX
        Private m_strhtxtDivTopWTXX As String                            'htxtDivTopWTXX
        Private m_strhtxtDivLeftJSRXX As String                          'htxtDivLeftJSRXX
        Private m_strhtxtDivTopJSRXX As String                           'htxtDivTopJSRXX
        Private m_strhtxtDivLeftBody As String                           'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String                            'htxtDivTopBody

        '----------------------------------------------------------------
        'grdJSRXX paramters
        '----------------------------------------------------------------
        Private m_strhtxtSessionIdJSRXX As String                        'SessionId
        Private m_intPageSize_JSRXX As Integer                           'grdJSRXX的页大小
        Private m_intSelectedIndex_JSRXX As Integer                      'grdJSRXX的行索引
        Private m_intCurrentPageIndex_JSRXX As Integer                   'grdJSRXX的页索引

        '----------------------------------------------------------------
        'grdWTXX paramters
        '----------------------------------------------------------------
        Private m_intPageSize_WTXX As Integer                            'grdWTXX的页大小
        Private m_intSelectedIndex_WTXX As Integer                       'grdWTXX的行索引
        Private m_intCurrentPageIndex_WTXX As Integer                    'grdWTXX的页索引

        '----------------------------------------------------------------
        'weituo options
        '----------------------------------------------------------------
        Private m_intSelectedIndex_rblWTXX As Integer                    'rblWTXX的当前选定项

        '----------------------------------------------------------------
        'send options
        '----------------------------------------------------------------
        Private m_blnSelected_cblFSXX As Boolean()                       'cblFSXX当前选定项












        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()

            MyBase.New()

            m_strhtxtEditMode = ""
            m_strhtxtXRMode = ""

            m_strhtxtWTXXQuery = ""
            m_strhtxtWTXXRows = ""
            m_strhtxtWTXXSort = ""
            m_strhtxtWTXXSortColumnIndex = ""
            m_strhtxtWTXXSortType = ""

            m_strhtxtJSRXXQuery = ""
            m_strhtxtJSRXXRows = ""
            m_strhtxtJSRXXSort = ""
            m_strhtxtJSRXXSortColumnIndex = ""
            m_strhtxtJSRXXSortType = ""

            m_strhtxtDivLeftWTXX = ""
            m_strhtxtDivTopWTXX = ""

            m_strhtxtDivLeftJSRXX = ""
            m_strhtxtDivTopJSRXX = ""

            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""

            m_strhtxtSessionIdJSRXX = ""
            m_intPageSize_JSRXX = 100
            m_intSelectedIndex_JSRXX = -1
            m_intCurrentPageIndex_JSRXX = 0

            m_intPageSize_WTXX = 100
            m_intSelectedIndex_WTXX = -1
            m_intCurrentPageIndex_WTXX = 0

            m_intSelectedIndex_rblWTXX = -1

            m_blnSelected_cblFSXX = Nothing

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
            m_blnSelected_cblFSXX = Nothing
        End Sub

        '----------------------------------------------------------------
        ' 安全释放本身资源
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMFlowSend)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub











        '----------------------------------------------------------------
        ' htxtEditMode属性
        '----------------------------------------------------------------
        Public Property htxtEditMode() As String
            Get
                htxtEditMode = m_strhtxtEditMode
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtEditMode = Value
                Catch ex As Exception
                    m_strhtxtEditMode = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtXRMode属性
        '----------------------------------------------------------------
        Public Property htxtXRMode() As String
            Get
                htxtXRMode = m_strhtxtXRMode
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtXRMode = Value
                Catch ex As Exception
                    m_strhtxtXRMode = ""
                End Try
            End Set
        End Property



        '----------------------------------------------------------------
        ' htxtWTXXSort属性
        '----------------------------------------------------------------
        Public Property htxtWTXXSort() As String
            Get
                htxtWTXXSort = m_strhtxtWTXXSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtWTXXSort = Value
                Catch ex As Exception
                    m_strhtxtWTXXSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtWTXXRows属性
        '----------------------------------------------------------------
        Public Property htxtWTXXRows() As String
            Get
                htxtWTXXRows = m_strhtxtWTXXRows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtWTXXRows = Value
                Catch ex As Exception
                    m_strhtxtWTXXRows = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtWTXXSortColumnIndex属性
        '----------------------------------------------------------------
        Public Property htxtWTXXSortColumnIndex() As String
            Get
                htxtWTXXSortColumnIndex = m_strhtxtWTXXSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtWTXXSortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtWTXXSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtWTXXQuery属性
        '----------------------------------------------------------------
        Public Property htxtWTXXQuery() As String
            Get
                htxtWTXXQuery = m_strhtxtWTXXQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtWTXXQuery = Value
                Catch ex As Exception
                    m_strhtxtWTXXQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtWTXXSortType属性
        '----------------------------------------------------------------
        Public Property htxtWTXXSortType() As String
            Get
                htxtWTXXSortType = m_strhtxtWTXXSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtWTXXSortType = Value
                Catch ex As Exception
                    m_strhtxtWTXXSortType = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' htxtJSRXXSort属性
        '----------------------------------------------------------------
        Public Property htxtJSRXXSort() As String
            Get
                htxtJSRXXSort = m_strhtxtJSRXXSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtJSRXXSort = Value
                Catch ex As Exception
                    m_strhtxtJSRXXSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtJSRXXRows属性
        '----------------------------------------------------------------
        Public Property htxtJSRXXRows() As String
            Get
                htxtJSRXXRows = m_strhtxtJSRXXRows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtJSRXXRows = Value
                Catch ex As Exception
                    m_strhtxtJSRXXRows = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtJSRXXSortColumnIndex属性
        '----------------------------------------------------------------
        Public Property htxtJSRXXSortColumnIndex() As String
            Get
                htxtJSRXXSortColumnIndex = m_strhtxtJSRXXSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtJSRXXSortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtJSRXXSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtJSRXXQuery属性
        '----------------------------------------------------------------
        Public Property htxtJSRXXQuery() As String
            Get
                htxtJSRXXQuery = m_strhtxtJSRXXQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtJSRXXQuery = Value
                Catch ex As Exception
                    m_strhtxtJSRXXQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtJSRXXSortType属性
        '----------------------------------------------------------------
        Public Property htxtJSRXXSortType() As String
            Get
                htxtJSRXXSortType = m_strhtxtJSRXXSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtJSRXXSortType = Value
                Catch ex As Exception
                    m_strhtxtJSRXXSortType = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' htxtDivLeftWTXX属性
        '----------------------------------------------------------------
        Public Property htxtDivLeftWTXX() As String
            Get
                htxtDivLeftWTXX = m_strhtxtDivLeftWTXX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftWTXX = Value
                Catch ex As Exception
                    m_strhtxtDivLeftWTXX = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopWTXX属性
        '----------------------------------------------------------------
        Public Property htxtDivTopWTXX() As String
            Get
                htxtDivTopWTXX = m_strhtxtDivTopWTXX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopWTXX = Value
                Catch ex As Exception
                    m_strhtxtDivTopWTXX = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivLeftJSRXX属性
        '----------------------------------------------------------------
        Public Property htxtDivLeftJSRXX() As String
            Get
                htxtDivLeftJSRXX = m_strhtxtDivLeftJSRXX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftJSRXX = Value
                Catch ex As Exception
                    m_strhtxtDivLeftJSRXX = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopJSRXX属性
        '----------------------------------------------------------------
        Public Property htxtDivTopJSRXX() As String
            Get
                htxtDivTopJSRXX = m_strhtxtDivTopJSRXX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopJSRXX = Value
                Catch ex As Exception
                    m_strhtxtDivTopJSRXX = ""
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
        ' htxtSessionIdJSRXX属性
        '----------------------------------------------------------------
        Public Property htxtSessionIdJSRXX() As String
            Get
                htxtSessionIdJSRXX = m_strhtxtSessionIdJSRXX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtSessionIdJSRXX = Value
                Catch ex As Exception
                    m_strhtxtSessionIdJSRXX = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdJSRXX_PageSize属性
        '----------------------------------------------------------------
        Public Property grdJSRXX_PageSize() As Integer
            Get
                grdJSRXX_PageSize = m_intPageSize_JSRXX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_JSRXX = Value
                Catch ex As Exception
                    m_intPageSize_JSRXX = 100
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdJSRXX_SelectedIndex属性
        '----------------------------------------------------------------
        Public Property grdJSRXX_SelectedIndex() As Integer
            Get
                grdJSRXX_SelectedIndex = m_intSelectedIndex_JSRXX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_JSRXX = Value
                Catch ex As Exception
                    m_intSelectedIndex_JSRXX = -1
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdJSRXX_CurrentPageIndex属性
        '----------------------------------------------------------------
        Public Property grdJSRXX_CurrentPageIndex() As Integer
            Get
                grdJSRXX_CurrentPageIndex = m_intCurrentPageIndex_JSRXX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intCurrentPageIndex_JSRXX = Value
                Catch ex As Exception
                    m_intCurrentPageIndex_JSRXX = -1
                End Try
            End Set
        End Property



        '----------------------------------------------------------------
        ' grdWTXX_PageSize属性
        '----------------------------------------------------------------
        Public Property grdWTXX_PageSize() As Integer
            Get
                grdWTXX_PageSize = m_intPageSize_WTXX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_WTXX = Value
                Catch ex As Exception
                    m_intPageSize_WTXX = 100
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdWTXX_SelectedIndex属性
        '----------------------------------------------------------------
        Public Property grdWTXX_SelectedIndex() As Integer
            Get
                grdWTXX_SelectedIndex = m_intSelectedIndex_WTXX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_WTXX = Value
                Catch ex As Exception
                    m_intSelectedIndex_WTXX = -1
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdWTXX_CurrentPageIndex属性
        '----------------------------------------------------------------
        Public Property grdWTXX_CurrentPageIndex() As Integer
            Get
                grdWTXX_CurrentPageIndex = m_intCurrentPageIndex_WTXX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intCurrentPageIndex_WTXX = Value
                Catch ex As Exception
                    m_intCurrentPageIndex_WTXX = -1
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' rblWTXX_SelectedIndex属性
        '----------------------------------------------------------------
        Public Property rblWTXX_SelectedIndex() As Integer
            Get
                rblWTXX_SelectedIndex = m_intSelectedIndex_rblWTXX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_rblWTXX = Value
                Catch ex As Exception
                    m_intSelectedIndex_rblWTXX = -1
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' cblFSXX_SelectedItems属性
        '----------------------------------------------------------------
        Public Property cblFSXX_SelectedItems() As Boolean()
            Get
                cblFSXX_SelectedItems = m_blnSelected_cblFSXX
            End Get
            Set(ByVal Value As Boolean())
                Try
                    m_blnSelected_cblFSXX = Value
                Catch ex As Exception
                    m_blnSelected_cblFSXX = Nothing
                End Try
            End Set
        End Property

    End Class

End Namespace
