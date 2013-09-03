Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IMGrswNbff
    '
    ' 功能描述： 
    '     grsw_nbff.aspx模块本身恢复现场需要的信息
    '----------------------------------------------------------------
    <Serializable()> Public Class IMGrswNbff
        Implements IDisposable

        '----------------------------------------------------------------
        'hidden textbox
        '----------------------------------------------------------------
        Private m_strhtxtNBFFQuery As String                      'htxtNBFFQuery
        Private m_strhtxtNBFFRows As String                       'htxtNBFFRows
        Private m_strhtxtNBFFSort As String                       'htxtNBFFSort
        Private m_strhtxtNBFFSortColumnIndex As String            'htxtNBFFSortColumnIndex
        Private m_strhtxtNBFFSortType As String                   'htxtNBFFSortType
        Private m_strhtxtDivLeftNBFF As String                    'htxtDivLeftNBFF
        Private m_strhtxtDivTopNBFF As String                     'htxtDivTopNBFF
        Private m_strhtxtDivLeftBody As String                    'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String                     'htxtDivTopBody

        Private m_strhtxtSessionIdQuery As String                 'htxtSessionIdQuery

        '----------------------------------------------------------------
        'asp:textbox
        '----------------------------------------------------------------
        Private m_strtxtNBFFPageIndex As String                  'txtNBFFPageIndex
        Private m_strtxtNBFFPageSize As String                   'txtNBFFPageSize
        Private m_strtxtNBFFSearch_WJZH As String                'txtNBFFSearch_WJZH
        Private m_strtxtNBFFSearch_WJBT As String                'txtNBFFSearch_WJBT
        Private m_strtxtNBFFSearch_FFR As String                 'txtNBFFSearch_FFR
        Private m_strtxtNBFFSearch_FFRQMin As String             'txtNBFFSearch_FFRQMin
        Private m_strtxtNBFFSearch_FFRQMax As String             'txtNBFFSearch_FFRQMax

        '----------------------------------------------------------------
        'asp:datagrid - grdNBFF
        '----------------------------------------------------------------
        Private m_intPageSize_grdNBFF As Integer
        Private m_intSelectedIndex_grdNBFF As Integer
        Private m_intCurrentPageIndex_grdNBFF As Integer











        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            'hidden
            m_strhtxtNBFFQuery = ""
            m_strhtxtNBFFRows = ""
            m_strhtxtNBFFSort = ""
            m_strhtxtNBFFSortColumnIndex = ""
            m_strhtxtNBFFSortType = ""
            m_strhtxtDivLeftNBFF = ""
            m_strhtxtDivTopNBFF = ""
            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""

            m_strhtxtSessionIdQuery = ""

            'textbox
            m_strtxtNBFFPageIndex = ""
            m_strtxtNBFFPageSize = ""
            m_strtxtNBFFSearch_WJZH = ""
            m_strtxtNBFFSearch_WJBT = ""
            m_strtxtNBFFSearch_FFR = ""
            m_strtxtNBFFSearch_FFRQMin = ""
            m_strtxtNBFFSearch_FFRQMax = ""

            'datagrid
            m_intPageSize_grdNBFF = 0
            m_intCurrentPageIndex_grdNBFF = 0
            m_intSelectedIndex_grdNBFF = -1

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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMGrswNbff)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub












        '----------------------------------------------------------------
        ' htxtNBFFQuery属性
        '----------------------------------------------------------------
        Public Property htxtNBFFQuery() As String
            Get
                htxtNBFFQuery = m_strhtxtNBFFQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtNBFFQuery = Value
                Catch ex As Exception
                    m_strhtxtNBFFQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtNBFFRows属性
        '----------------------------------------------------------------
        Public Property htxtNBFFRows() As String
            Get
                htxtNBFFRows = m_strhtxtNBFFRows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtNBFFRows = Value
                Catch ex As Exception
                    m_strhtxtNBFFRows = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtNBFFSort属性
        '----------------------------------------------------------------
        Public Property htxtNBFFSort() As String
            Get
                htxtNBFFSort = m_strhtxtNBFFSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtNBFFSort = Value
                Catch ex As Exception
                    m_strhtxtNBFFSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtNBFFSortColumnIndex属性
        '----------------------------------------------------------------
        Public Property htxtNBFFSortColumnIndex() As String
            Get
                htxtNBFFSortColumnIndex = m_strhtxtNBFFSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtNBFFSortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtNBFFSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtNBFFSortType属性
        '----------------------------------------------------------------
        Public Property htxtNBFFSortType() As String
            Get
                htxtNBFFSortType = m_strhtxtNBFFSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtNBFFSortType = Value
                Catch ex As Exception
                    m_strhtxtNBFFSortType = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' htxtDivLeftNBFF属性
        '----------------------------------------------------------------
        Public Property htxtDivLeftNBFF() As String
            Get
                htxtDivLeftNBFF = m_strhtxtDivLeftNBFF
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftNBFF = Value
                Catch ex As Exception
                    m_strhtxtDivLeftNBFF = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopNBFF属性
        '----------------------------------------------------------------
        Public Property htxtDivTopNBFF() As String
            Get
                htxtDivTopNBFF = m_strhtxtDivTopNBFF
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopNBFF = Value
                Catch ex As Exception
                    m_strhtxtDivTopNBFF = ""
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
        ' txtNBFFPageIndex属性
        '----------------------------------------------------------------
        Public Property txtNBFFPageIndex() As String
            Get
                txtNBFFPageIndex = m_strtxtNBFFPageIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtNBFFPageIndex = Value
                Catch ex As Exception
                    m_strtxtNBFFPageIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtNBFFPageSize属性
        '----------------------------------------------------------------
        Public Property txtNBFFPageSize() As String
            Get
                txtNBFFPageSize = m_strtxtNBFFPageSize
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtNBFFPageSize = Value
                Catch ex As Exception
                    m_strtxtNBFFPageSize = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' txtNBFFSearch_WJZH属性
        '----------------------------------------------------------------
        Public Property txtNBFFSearch_WJZH() As String
            Get
                txtNBFFSearch_WJZH = m_strtxtNBFFSearch_WJZH
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtNBFFSearch_WJZH = Value
                Catch ex As Exception
                    m_strtxtNBFFSearch_WJZH = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtNBFFSearch_WJBT属性
        '----------------------------------------------------------------
        Public Property txtNBFFSearch_WJBT() As String
            Get
                txtNBFFSearch_WJBT = m_strtxtNBFFSearch_WJBT
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtNBFFSearch_WJBT = Value
                Catch ex As Exception
                    m_strtxtNBFFSearch_WJBT = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtNBFFSearch_FFR属性
        '----------------------------------------------------------------
        Public Property txtNBFFSearch_FFR() As String
            Get
                txtNBFFSearch_FFR = m_strtxtNBFFSearch_FFR
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtNBFFSearch_FFR = Value
                Catch ex As Exception
                    m_strtxtNBFFSearch_FFR = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtNBFFSearch_FFRQMin属性
        '----------------------------------------------------------------
        Public Property txtNBFFSearch_FFRQMin() As String
            Get
                txtNBFFSearch_FFRQMin = m_strtxtNBFFSearch_FFRQMin
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtNBFFSearch_FFRQMin = Value
                Catch ex As Exception
                    m_strtxtNBFFSearch_FFRQMin = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtNBFFSearch_FFRQMax属性
        '----------------------------------------------------------------
        Public Property txtNBFFSearch_FFRQMax() As String
            Get
                txtNBFFSearch_FFRQMax = m_strtxtNBFFSearch_FFRQMax
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtNBFFSearch_FFRQMax = Value
                Catch ex As Exception
                    m_strtxtNBFFSearch_FFRQMax = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' grdNBFFPageSize属性
        '----------------------------------------------------------------
        Public Property grdNBFFPageSize() As Integer
            Get
                grdNBFFPageSize = m_intPageSize_grdNBFF
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_grdNBFF = Value
                Catch ex As Exception
                    m_intPageSize_grdNBFF = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdNBFFCurrentPageIndex属性
        '----------------------------------------------------------------
        Public Property grdNBFFCurrentPageIndex() As Integer
            Get
                grdNBFFCurrentPageIndex = m_intCurrentPageIndex_grdNBFF
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intCurrentPageIndex_grdNBFF = Value
                Catch ex As Exception
                    m_intCurrentPageIndex_grdNBFF = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdNBFFSelectedIndex属性
        '----------------------------------------------------------------
        Public Property grdNBFFSelectedIndex() As Integer
            Get
                grdNBFFSelectedIndex = m_intSelectedIndex_grdNBFF
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_grdNBFF = Value
                Catch ex As Exception
                    m_intSelectedIndex_grdNBFF = -1
                End Try
            End Set
        End Property

    End Class

End Namespace
