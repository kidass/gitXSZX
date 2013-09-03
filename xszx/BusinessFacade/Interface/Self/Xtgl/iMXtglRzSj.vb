Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IMXtglRzSj
    '
    ' 功能描述： 
    '     xtgl_rz_sj.aspx模块本身恢复现场需要的信息
    '----------------------------------------------------------------
    <Serializable()> Public Class IMXtglRzSj
        Implements IDisposable

        '----------------------------------------------------------------
        'hidden textbox
        '----------------------------------------------------------------
        Private m_strhtxtLOGQuery As String                       'htxtLOGQuery
        Private m_strhtxtLOGRows As String                        'htxtLOGRows
        Private m_strhtxtLOGSort As String                        'htxtLOGSort
        Private m_strhtxtLOGSortColumnIndex As String             'htxtLOGSortColumnIndex
        Private m_strhtxtLOGSortType As String                    'htxtLOGSortType
        Private m_strhtxtDivLeftLOG As String                     'htxtDivLeftLOG
        Private m_strhtxtDivTopLOG As String                      'htxtDivTopLOG
        Private m_strhtxtDivLeftBody As String                    'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String                     'htxtDivTopBody

        Private m_strhtxtSessionIdQuery As String                 'htxtSessionIdQuery

        '----------------------------------------------------------------
        'asp:textbox
        '----------------------------------------------------------------
        Private m_strtxtLOGPageIndex As String                  'txtLOGPageIndex
        Private m_strtxtLOGPageSize As String                   'txtLOGPageSize
        Private m_strtxtLOGSearch_YHBS As String                'txtLOGSearch_YHBS
        Private m_strtxtLOGSearch_CZMS As String                'txtLOGSearch_CZMS
        Private m_strtxtLOGSearch_CZSJMin As String             'txtLOGSearch_CZSJMin
        Private m_strtxtLOGSearch_CZSJMax As String             'txtLOGSearch_CZSJMax

        '----------------------------------------------------------------
        'asp:datagrid - grdLOG
        '----------------------------------------------------------------
        Private m_intPageSize_grdLOG As Integer
        Private m_intSelectedIndex_grdLOG As Integer
        Private m_intCurrentPageIndex_grdLOG As Integer












        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            'hidden
            m_strhtxtLOGQuery = ""
            m_strhtxtLOGRows = ""
            m_strhtxtLOGSort = ""
            m_strhtxtLOGSortColumnIndex = ""
            m_strhtxtLOGSortType = ""
            m_strhtxtDivLeftLOG = ""
            m_strhtxtDivTopLOG = ""
            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""

            m_strhtxtSessionIdQuery = ""

            'textbox
            m_strtxtLOGPageIndex = ""
            m_strtxtLOGPageSize = ""
            m_strtxtLOGSearch_YHBS = ""
            m_strtxtLOGSearch_CZMS = ""
            m_strtxtLOGSearch_CZSJMin = ""
            m_strtxtLOGSearch_CZSJMax = ""

            'datagrid
            m_intPageSize_grdLOG = 0
            m_intCurrentPageIndex_grdLOG = 0
            m_intSelectedIndex_grdLOG = -1

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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMXtglRzSj)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub













        '----------------------------------------------------------------
        ' htxtLOGQuery属性
        '----------------------------------------------------------------
        Public Property htxtLOGQuery() As String
            Get
                htxtLOGQuery = m_strhtxtLOGQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtLOGQuery = Value
                Catch ex As Exception
                    m_strhtxtLOGQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtLOGRows属性
        '----------------------------------------------------------------
        Public Property htxtLOGRows() As String
            Get
                htxtLOGRows = m_strhtxtLOGRows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtLOGRows = Value
                Catch ex As Exception
                    m_strhtxtLOGRows = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtLOGSort属性
        '----------------------------------------------------------------
        Public Property htxtLOGSort() As String
            Get
                htxtLOGSort = m_strhtxtLOGSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtLOGSort = Value
                Catch ex As Exception
                    m_strhtxtLOGSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtLOGSortColumnIndex属性
        '----------------------------------------------------------------
        Public Property htxtLOGSortColumnIndex() As String
            Get
                htxtLOGSortColumnIndex = m_strhtxtLOGSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtLOGSortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtLOGSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtLOGSortType属性
        '----------------------------------------------------------------
        Public Property htxtLOGSortType() As String
            Get
                htxtLOGSortType = m_strhtxtLOGSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtLOGSortType = Value
                Catch ex As Exception
                    m_strhtxtLOGSortType = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' htxtDivLeftLOG属性
        '----------------------------------------------------------------
        Public Property htxtDivLeftLOG() As String
            Get
                htxtDivLeftLOG = m_strhtxtDivLeftLOG
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftLOG = Value
                Catch ex As Exception
                    m_strhtxtDivLeftLOG = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopLOG属性
        '----------------------------------------------------------------
        Public Property htxtDivTopLOG() As String
            Get
                htxtDivTopLOG = m_strhtxtDivTopLOG
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopLOG = Value
                Catch ex As Exception
                    m_strhtxtDivTopLOG = ""
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
        ' txtLOGPageIndex属性
        '----------------------------------------------------------------
        Public Property txtLOGPageIndex() As String
            Get
                txtLOGPageIndex = m_strtxtLOGPageIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtLOGPageIndex = Value
                Catch ex As Exception
                    m_strtxtLOGPageIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtLOGPageSize属性
        '----------------------------------------------------------------
        Public Property txtLOGPageSize() As String
            Get
                txtLOGPageSize = m_strtxtLOGPageSize
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtLOGPageSize = Value
                Catch ex As Exception
                    m_strtxtLOGPageSize = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' txtLOGSearch_YHBS属性
        '----------------------------------------------------------------
        Public Property txtLOGSearch_YHBS() As String
            Get
                txtLOGSearch_YHBS = m_strtxtLOGSearch_YHBS
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtLOGSearch_YHBS = Value
                Catch ex As Exception
                    m_strtxtLOGSearch_YHBS = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtLOGSearch_CZMS属性
        '----------------------------------------------------------------
        Public Property txtLOGSearch_CZMS() As String
            Get
                txtLOGSearch_CZMS = m_strtxtLOGSearch_CZMS
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtLOGSearch_CZMS = Value
                Catch ex As Exception
                    m_strtxtLOGSearch_CZMS = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtLOGSearch_CZSJMin属性
        '----------------------------------------------------------------
        Public Property txtLOGSearch_CZSJMin() As String
            Get
                txtLOGSearch_CZSJMin = m_strtxtLOGSearch_CZSJMin
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtLOGSearch_CZSJMin = Value
                Catch ex As Exception
                    m_strtxtLOGSearch_CZSJMin = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtLOGSearch_CZSJMax属性
        '----------------------------------------------------------------
        Public Property txtLOGSearch_CZSJMax() As String
            Get
                txtLOGSearch_CZSJMax = m_strtxtLOGSearch_CZSJMax
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtLOGSearch_CZSJMax = Value
                Catch ex As Exception
                    m_strtxtLOGSearch_CZSJMax = ""
                End Try
            End Set
        End Property





        '----------------------------------------------------------------
        ' grdLOGPageSize属性
        '----------------------------------------------------------------
        Public Property grdLOGPageSize() As Integer
            Get
                grdLOGPageSize = m_intPageSize_grdLOG
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_grdLOG = Value
                Catch ex As Exception
                    m_intPageSize_grdLOG = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdLOGCurrentPageIndex属性
        '----------------------------------------------------------------
        Public Property grdLOGCurrentPageIndex() As Integer
            Get
                grdLOGCurrentPageIndex = m_intCurrentPageIndex_grdLOG
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intCurrentPageIndex_grdLOG = Value
                Catch ex As Exception
                    m_intCurrentPageIndex_grdLOG = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdLOGSelectedIndex属性
        '----------------------------------------------------------------
        Public Property grdLOGSelectedIndex() As Integer
            Get
                grdLOGSelectedIndex = m_intSelectedIndex_grdLOG
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_grdLOG = Value
                Catch ex As Exception
                    m_intSelectedIndex_grdLOG = -1
                End Try
            End Set
        End Property

    End Class

End Namespace
