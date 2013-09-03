Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IMXtglYhglYh
    '
    ' 功能描述： 
    '     xtgl_yhgl_yh.aspx模块本身恢复现场需要的信息
    '----------------------------------------------------------------
    <Serializable()> Public Class IMXtglYhglYh
        Implements IDisposable

        '----------------------------------------------------------------
        'hidden textbox
        '----------------------------------------------------------------
        Private m_strhtxtBMRYQuery As String                      'htxtBMRYQuery
        Private m_strhtxtBMRYRows As String                       'htxtBMRYRows
        Private m_strhtxtBMRYSort As String                       'htxtBMRYSort
        Private m_strhtxtBMRYSortColumnIndex As String            'htxtBMRYSortColumnIndex
        Private m_strhtxtBMRYSortType As String                   'htxtBMRYSortType
        Private m_strhtxtDivLeftBMRY As String                    'htxtDivLeftBMRY
        Private m_strhtxtDivTopBMRY As String                     'htxtDivTopBMRY
        Private m_strhtxtDivLeftBody As String                    'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String                     'htxtDivTopBody

        '----------------------------------------------------------------
        'asp:textbox
        '----------------------------------------------------------------
        Private m_strtxtBMRYPageIndex As String                  'txtBMRYPageIndex
        Private m_strtxtBMRYPageSize As String                   'txtBMRYPageSize
        Private m_strtxtBMRYSearch_RYDM As String                'txtBMRYSearch_RYDM
        Private m_strtxtBMRYSearch_RYMC As String                'txtBMRYSearch_RYMC
        Private m_strtxtBMRYSearch_ZZMC As String                'txtBMRYSearch_ZZMC
        Private m_strtxtBMRYSearch_RYXHMin As String             'txtBMRYSearch_RYXHMin
        Private m_strtxtBMRYSearch_RYXHMax As String             'txtBMRYSearch_RYXHMax
        Private m_strtxtBMRYSearch_RYJBMC As String              'txtBMRYSearch_RYJBMC
        Private m_strtxtBMRYSearch_RYDRZW As String              'txtBMRYSearch_RYDRZW
        Private m_strSearchRYSFSQ As String                      'rblApply

        '----------------------------------------------------------------
        'asp:datagrid - grdBMRY
        '----------------------------------------------------------------
        Private m_intPageSize_grdBMRY As Integer
        Private m_intSelectedIndex_grdBMRY As Integer
        Private m_intCurrentPageIndex_grdBMRY As Integer













        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
            'hidden
            m_strhtxtBMRYQuery = ""
            m_strhtxtBMRYRows = ""
            m_strhtxtBMRYSort = ""
            m_strhtxtBMRYSortColumnIndex = ""
            m_strhtxtBMRYSortType = ""
            m_strhtxtDivLeftBMRY = ""
            m_strhtxtDivTopBMRY = ""
            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""
            'textbox
            m_strtxtBMRYPageIndex = ""
            m_strtxtBMRYPageSize = ""
            m_strtxtBMRYSearch_RYDM = ""
            m_strtxtBMRYSearch_RYMC = ""
            m_strtxtBMRYSearch_ZZMC = ""
            m_strtxtBMRYSearch_RYXHMin = ""
            m_strtxtBMRYSearch_RYXHMax = ""
            m_strtxtBMRYSearch_RYJBMC = ""
            m_strtxtBMRYSearch_RYDRZW = ""
            'datagrid
            m_intPageSize_grdBMRY = 0
            m_intCurrentPageIndex_grdBMRY = 0
            m_intSelectedIndex_grdBMRY = -1
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMXtglYhglYh)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub














        '----------------------------------------------------------------
        ' htxtBMRYQuery属性
        '----------------------------------------------------------------
        Public Property htxtBMRYQuery() As String
            Get
                htxtBMRYQuery = m_strhtxtBMRYQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtBMRYQuery = Value
                Catch ex As Exception
                    m_strhtxtBMRYQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtBMRYRows属性
        '----------------------------------------------------------------
        Public Property htxtBMRYRows() As String
            Get
                htxtBMRYRows = m_strhtxtBMRYRows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtBMRYRows = Value
                Catch ex As Exception
                    m_strhtxtBMRYRows = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtBMRYSort属性
        '----------------------------------------------------------------
        Public Property htxtBMRYSort() As String
            Get
                htxtBMRYSort = m_strhtxtBMRYSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtBMRYSort = Value
                Catch ex As Exception
                    m_strhtxtBMRYSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtBMRYSortColumnIndex属性
        '----------------------------------------------------------------
        Public Property htxtBMRYSortColumnIndex() As String
            Get
                htxtBMRYSortColumnIndex = m_strhtxtBMRYSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtBMRYSortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtBMRYSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtBMRYSortType属性
        '----------------------------------------------------------------
        Public Property htxtBMRYSortType() As String
            Get
                htxtBMRYSortType = m_strhtxtBMRYSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtBMRYSortType = Value
                Catch ex As Exception
                    m_strhtxtBMRYSortType = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivLeftBMRY属性
        '----------------------------------------------------------------
        Public Property htxtDivLeftBMRY() As String
            Get
                htxtDivLeftBMRY = m_strhtxtDivLeftBMRY
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftBMRY = Value
                Catch ex As Exception
                    m_strhtxtDivLeftBMRY = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopBMRY属性
        '----------------------------------------------------------------
        Public Property htxtDivTopBMRY() As String
            Get
                htxtDivTopBMRY = m_strhtxtDivTopBMRY
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopBMRY = Value
                Catch ex As Exception
                    m_strhtxtDivTopBMRY = ""
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
        ' txtBMRYPageIndex属性
        '----------------------------------------------------------------
        Public Property txtBMRYPageIndex() As String
            Get
                txtBMRYPageIndex = m_strtxtBMRYPageIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtBMRYPageIndex = Value
                Catch ex As Exception
                    m_strtxtBMRYPageIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtBMRYPageSize属性
        '----------------------------------------------------------------
        Public Property txtBMRYPageSize() As String
            Get
                txtBMRYPageSize = m_strtxtBMRYPageSize
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtBMRYPageSize = Value
                Catch ex As Exception
                    m_strtxtBMRYPageSize = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtBMRYSearch_RYDM属性
        '----------------------------------------------------------------
        Public Property txtBMRYSearch_RYDM() As String
            Get
                txtBMRYSearch_RYDM = m_strtxtBMRYSearch_RYDM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtBMRYSearch_RYDM = Value
                Catch ex As Exception
                    m_strtxtBMRYSearch_RYDM = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtBMRYSearch_RYMC属性
        '----------------------------------------------------------------
        Public Property txtBMRYSearch_RYMC() As String
            Get
                txtBMRYSearch_RYMC = m_strtxtBMRYSearch_RYMC
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtBMRYSearch_RYMC = Value
                Catch ex As Exception
                    m_strtxtBMRYSearch_RYMC = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtBMRYSearch_ZZMC属性
        '----------------------------------------------------------------
        Public Property txtBMRYSearch_ZZMC() As String
            Get
                txtBMRYSearch_ZZMC = m_strtxtBMRYSearch_ZZMC
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtBMRYSearch_ZZMC = Value
                Catch ex As Exception
                    m_strtxtBMRYSearch_ZZMC = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtBMRYSearch_RYXHMin属性
        '----------------------------------------------------------------
        Public Property txtBMRYSearch_RYXHMin() As String
            Get
                txtBMRYSearch_RYXHMin = m_strtxtBMRYSearch_RYXHMin
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtBMRYSearch_RYXHMin = Value
                Catch ex As Exception
                    m_strtxtBMRYSearch_RYXHMin = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtBMRYSearch_RYXHMax属性
        '----------------------------------------------------------------
        Public Property txtBMRYSearch_RYXHMax() As String
            Get
                txtBMRYSearch_RYXHMax = m_strtxtBMRYSearch_RYXHMax
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtBMRYSearch_RYXHMax = Value
                Catch ex As Exception
                    m_strtxtBMRYSearch_RYXHMax = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtBMRYSearch_RYJBMC属性
        '----------------------------------------------------------------
        Public Property txtBMRYSearch_RYJBMC() As String
            Get
                txtBMRYSearch_RYJBMC = m_strtxtBMRYSearch_RYJBMC
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtBMRYSearch_RYJBMC = Value
                Catch ex As Exception
                    m_strtxtBMRYSearch_RYJBMC = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtBMRYSearch_RYDRZW属性
        '----------------------------------------------------------------
        Public Property txtBMRYSearch_RYDRZW() As String
            Get
                txtBMRYSearch_RYDRZW = m_strtxtBMRYSearch_RYDRZW
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtBMRYSearch_RYDRZW = Value
                Catch ex As Exception
                    m_strtxtBMRYSearch_RYDRZW = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' rblApply属性
        '----------------------------------------------------------------
        Public Property rblApply() As String
            Get
                rblApply = m_strSearchRYSFSQ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strSearchRYSFSQ = Value
                Catch ex As Exception
                    m_strSearchRYSFSQ = ""
                End Try
            End Set
        End Property



        '----------------------------------------------------------------
        ' grdBMRYPageSize属性
        '----------------------------------------------------------------
        Public Property grdBMRYPageSize() As Integer
            Get
                grdBMRYPageSize = m_intPageSize_grdBMRY
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_grdBMRY = Value
                Catch ex As Exception
                    m_intPageSize_grdBMRY = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdBMRYCurrentPageIndex属性
        '----------------------------------------------------------------
        Public Property grdBMRYCurrentPageIndex() As Integer
            Get
                grdBMRYCurrentPageIndex = m_intCurrentPageIndex_grdBMRY
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intCurrentPageIndex_grdBMRY = Value
                Catch ex As Exception
                    m_intCurrentPageIndex_grdBMRY = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdBMRYSelectedIndex属性
        '----------------------------------------------------------------
        Public Property grdBMRYSelectedIndex() As Integer
            Get
                grdBMRYSelectedIndex = m_intSelectedIndex_grdBMRY
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_grdBMRY = Value
                Catch ex As Exception
                    m_intSelectedIndex_grdBMRY = 0
                End Try
            End Set
        End Property

    End Class

End Namespace
