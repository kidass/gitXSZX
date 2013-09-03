Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IMGgxxDzggFabu
    '
    ' 功能描述： 
    '     ggxx_dzgg_fabu.aspx模块本身恢复现场需要的信息
    '----------------------------------------------------------------
    <Serializable()> Public Class IMGgxxDzggFabu
        Implements IDisposable

        '----------------------------------------------------------------
        'hidden textbox
        '----------------------------------------------------------------
        Private m_strhtxtDZGGQuery As String                      'htxtDZGGQuery
        Private m_strhtxtDZGGRows As String                       'htxtDZGGRows
        Private m_strhtxtDZGGSort As String                       'htxtDZGGSort
        Private m_strhtxtDZGGSortColumnIndex As String            'htxtDZGGSortColumnIndex
        Private m_strhtxtDZGGSortType As String                   'htxtDZGGSortType
        Private m_strhtxtDivLeftDZGG As String                    'htxtDivLeftDZGG
        Private m_strhtxtDivTopDZGG As String                     'htxtDivTopDZGG
        Private m_strhtxtDivLeftBody As String                    'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String                     'htxtDivTopBody

        Private m_strhtxtSessionIdQuery As String                 'htxtSessionIdQuery

        '----------------------------------------------------------------
        'asp:textbox
        '----------------------------------------------------------------
        Private m_strtxtDZGGPageIndex As String                  'txtDZGGPageIndex
        Private m_strtxtDZGGPageSize As String                   'txtDZGGPageSize
        Private m_strtxtDZGGSearch_BT As String                  'txtDZGGSearch_BT
        Private m_strtxtDZGGSearch_RQMin As String               'txtDZGGSearch_RQMin
        Private m_strtxtDZGGSearch_RQMax As String               'txtDZGGSearch_RQMax

        Private m_intSelectedIndex_ddlDZGGSearch_FBBS As Integer 'ddlDZGGSearch_FBBS
        Private m_intSelectedIndex_ddlDZGGSearch_YDBS As Integer 'ddlDZGGSearch_YDBS

        '----------------------------------------------------------------
        'asp:datagrid - grdDZGG
        '----------------------------------------------------------------
        Private m_intPageSize_grdDZGG As Integer
        Private m_intSelectedIndex_grdDZGG As Integer
        Private m_intCurrentPageIndex_grdDZGG As Integer












        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            'hidden
            m_strhtxtDZGGQuery = ""
            m_strhtxtDZGGRows = ""
            m_strhtxtDZGGSort = ""
            m_strhtxtDZGGSortColumnIndex = ""
            m_strhtxtDZGGSortType = ""
            m_strhtxtDivLeftDZGG = ""
            m_strhtxtDivTopDZGG = ""
            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""

            m_strhtxtSessionIdQuery = ""

            'textbox
            m_strtxtDZGGPageIndex = ""
            m_strtxtDZGGPageSize = ""
            m_strtxtDZGGSearch_BT = ""
            m_strtxtDZGGSearch_RQMin = ""
            m_strtxtDZGGSearch_RQMax = ""
            m_intSelectedIndex_ddlDZGGSearch_FBBS = -1
            m_intSelectedIndex_ddlDZGGSearch_YDBS = -1

            'datagrid
            m_intPageSize_grdDZGG = 0
            m_intCurrentPageIndex_grdDZGG = 0
            m_intSelectedIndex_grdDZGG = -1

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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMGgxxDzggFabu)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub













        '----------------------------------------------------------------
        ' htxtDZGGQuery属性
        '----------------------------------------------------------------
        Public Property htxtDZGGQuery() As String
            Get
                htxtDZGGQuery = m_strhtxtDZGGQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDZGGQuery = Value
                Catch ex As Exception
                    m_strhtxtDZGGQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDZGGRows属性
        '----------------------------------------------------------------
        Public Property htxtDZGGRows() As String
            Get
                htxtDZGGRows = m_strhtxtDZGGRows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDZGGRows = Value
                Catch ex As Exception
                    m_strhtxtDZGGRows = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDZGGSort属性
        '----------------------------------------------------------------
        Public Property htxtDZGGSort() As String
            Get
                htxtDZGGSort = m_strhtxtDZGGSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDZGGSort = Value
                Catch ex As Exception
                    m_strhtxtDZGGSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDZGGSortColumnIndex属性
        '----------------------------------------------------------------
        Public Property htxtDZGGSortColumnIndex() As String
            Get
                htxtDZGGSortColumnIndex = m_strhtxtDZGGSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDZGGSortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtDZGGSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDZGGSortType属性
        '----------------------------------------------------------------
        Public Property htxtDZGGSortType() As String
            Get
                htxtDZGGSortType = m_strhtxtDZGGSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDZGGSortType = Value
                Catch ex As Exception
                    m_strhtxtDZGGSortType = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' htxtDivLeftDZGG属性
        '----------------------------------------------------------------
        Public Property htxtDivLeftDZGG() As String
            Get
                htxtDivLeftDZGG = m_strhtxtDivLeftDZGG
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftDZGG = Value
                Catch ex As Exception
                    m_strhtxtDivLeftDZGG = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopDZGG属性
        '----------------------------------------------------------------
        Public Property htxtDivTopDZGG() As String
            Get
                htxtDivTopDZGG = m_strhtxtDivTopDZGG
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopDZGG = Value
                Catch ex As Exception
                    m_strhtxtDivTopDZGG = ""
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
        ' htxtSessionIdQuery属性
        '----------------------------------------------------------------
        Public Property htxtSessionIdQuery() As String
            Get
                htxtSessionIdQuery = m_strhtxtSessionIdQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtSessionIdQuery = Value
                Catch ex As Exception
                    m_strhtxtSessionIdQuery = ""
                End Try
            End Set
        End Property





        '----------------------------------------------------------------
        ' txtDZGGPageIndex属性
        '----------------------------------------------------------------
        Public Property txtDZGGPageIndex() As String
            Get
                txtDZGGPageIndex = m_strtxtDZGGPageIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtDZGGPageIndex = Value
                Catch ex As Exception
                    m_strtxtDZGGPageIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtDZGGPageSize属性
        '----------------------------------------------------------------
        Public Property txtDZGGPageSize() As String
            Get
                txtDZGGPageSize = m_strtxtDZGGPageSize
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtDZGGPageSize = Value
                Catch ex As Exception
                    m_strtxtDZGGPageSize = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' txtDZGGSearch_BT属性
        '----------------------------------------------------------------
        Public Property txtDZGGSearch_BT() As String
            Get
                txtDZGGSearch_BT = m_strtxtDZGGSearch_BT
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtDZGGSearch_BT = Value
                Catch ex As Exception
                    m_strtxtDZGGSearch_BT = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtDZGGSearch_RQMin属性
        '----------------------------------------------------------------
        Public Property txtDZGGSearch_RQMin() As String
            Get
                txtDZGGSearch_RQMin = m_strtxtDZGGSearch_RQMin
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtDZGGSearch_RQMin = Value
                Catch ex As Exception
                    m_strtxtDZGGSearch_RQMin = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtDZGGSearch_RQMax属性
        '----------------------------------------------------------------
        Public Property txtDZGGSearch_RQMax() As String
            Get
                txtDZGGSearch_RQMax = m_strtxtDZGGSearch_RQMax
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtDZGGSearch_RQMax = Value
                Catch ex As Exception
                    m_strtxtDZGGSearch_RQMax = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' ddlDZGGSearch_FBBS_SelectedIndex属性
        '----------------------------------------------------------------
        Public Property ddlDZGGSearch_FBBS_SelectedIndex() As Integer
            Get
                ddlDZGGSearch_FBBS_SelectedIndex = m_intSelectedIndex_ddlDZGGSearch_FBBS
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_ddlDZGGSearch_FBBS = Value
                Catch ex As Exception
                    m_intSelectedIndex_ddlDZGGSearch_FBBS = -1
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' ddlDZGGSearch_YDBS_SelectedIndex属性
        '----------------------------------------------------------------
        Public Property ddlDZGGSearch_YDBS_SelectedIndex() As Integer
            Get
                ddlDZGGSearch_YDBS_SelectedIndex = m_intSelectedIndex_ddlDZGGSearch_YDBS
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_ddlDZGGSearch_YDBS = Value
                Catch ex As Exception
                    m_intSelectedIndex_ddlDZGGSearch_YDBS = -1
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' grdDZGGPageSize属性
        '----------------------------------------------------------------
        Public Property grdDZGGPageSize() As Integer
            Get
                grdDZGGPageSize = m_intPageSize_grdDZGG
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_grdDZGG = Value
                Catch ex As Exception
                    m_intPageSize_grdDZGG = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdDZGGCurrentPageIndex属性
        '----------------------------------------------------------------
        Public Property grdDZGGCurrentPageIndex() As Integer
            Get
                grdDZGGCurrentPageIndex = m_intCurrentPageIndex_grdDZGG
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intCurrentPageIndex_grdDZGG = Value
                Catch ex As Exception
                    m_intCurrentPageIndex_grdDZGG = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdDZGGSelectedIndex属性
        '----------------------------------------------------------------
        Public Property grdDZGGSelectedIndex() As Integer
            Get
                grdDZGGSelectedIndex = m_intSelectedIndex_grdDZGG
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_grdDZGG = Value
                Catch ex As Exception
                    m_intSelectedIndex_grdDZGG = -1
                End Try
            End Set
        End Property

    End Class

End Namespace
