Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IMFlowCzrz
    '
    ' 功能描述： 
    '     flow_czrz.aspx模块本身恢复现场需要的信息
    '----------------------------------------------------------------
    <Serializable()> Public Class IMFlowCzrz
        Implements IDisposable

        '----------------------------------------------------------------
        'hidden textbox
        '----------------------------------------------------------------
        Private m_strhtxtCZRZQuery As String                      'htxtCZRZQuery
        Private m_strhtxtCZRZRows As String                       'htxtCZRZRows
        Private m_strhtxtCZRZSort As String                       'htxtCZRZSort
        Private m_strhtxtCZRZSortColumnIndex As String            'htxtCZRZSortColumnIndex
        Private m_strhtxtCZRZSortType As String                   'htxtCZRZSortType
        Private m_strhtxtDivLeftCZRZ As String                    'htxtDivLeftCZRZ
        Private m_strhtxtDivTopCZRZ As String                     'htxtDivTopCZRZ
        Private m_strhtxtDivLeftBody As String                    'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String                     'htxtDivTopBody

        Private m_strhtxtSessionIdQuery As String                 'htxtSessionIdQuery

        '----------------------------------------------------------------
        'asp:textbox
        '----------------------------------------------------------------
        Private m_strtxtCZRZPageIndex As String                  'txtCZRZPageIndex
        Private m_strtxtCZRZPageSize As String                   'txtCZRZPageSize
        Private m_strtxtCZRZSearch_CZR As String                 'txtCZRZSearch_CZR
        Private m_strtxtCZRZSearch_CZSM As String                'txtCZRZSearch_CZSM
        Private m_strtxtCZRZSearch_CZSJMin As String             'txtCZRZSearch_CZSJMin
        Private m_strtxtCZRZSearch_CZSJMax As String             'txtCZRZSearch_CZSJMax

        '----------------------------------------------------------------
        'asp:datagrid - grdCZRZ
        '----------------------------------------------------------------
        Private m_intPageSize_grdCZRZ As Integer
        Private m_intSelectedIndex_grdCZRZ As Integer
        Private m_intCurrentPageIndex_grdCZRZ As Integer











        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            'hidden
            m_strhtxtCZRZQuery = ""
            m_strhtxtCZRZRows = ""
            m_strhtxtCZRZSort = ""
            m_strhtxtCZRZSortColumnIndex = ""
            m_strhtxtCZRZSortType = ""
            m_strhtxtDivLeftCZRZ = ""
            m_strhtxtDivTopCZRZ = ""
            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""

            m_strhtxtSessionIdQuery = ""

            'textbox
            m_strtxtCZRZPageIndex = ""
            m_strtxtCZRZPageSize = ""
            m_strtxtCZRZSearch_CZR = ""
            m_strtxtCZRZSearch_CZSM = ""
            m_strtxtCZRZSearch_CZSJMin = ""
            m_strtxtCZRZSearch_CZSJMax = ""

            'datagrid
            m_intPageSize_grdCZRZ = 0
            m_intCurrentPageIndex_grdCZRZ = 0
            m_intSelectedIndex_grdCZRZ = -1

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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMFlowCzrz)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub












        '----------------------------------------------------------------
        ' htxtCZRZQuery属性
        '----------------------------------------------------------------
        Public Property htxtCZRZQuery() As String
            Get
                htxtCZRZQuery = m_strhtxtCZRZQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtCZRZQuery = Value
                Catch ex As Exception
                    m_strhtxtCZRZQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtCZRZRows属性
        '----------------------------------------------------------------
        Public Property htxtCZRZRows() As String
            Get
                htxtCZRZRows = m_strhtxtCZRZRows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtCZRZRows = Value
                Catch ex As Exception
                    m_strhtxtCZRZRows = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtCZRZSort属性
        '----------------------------------------------------------------
        Public Property htxtCZRZSort() As String
            Get
                htxtCZRZSort = m_strhtxtCZRZSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtCZRZSort = Value
                Catch ex As Exception
                    m_strhtxtCZRZSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtCZRZSortColumnIndex属性
        '----------------------------------------------------------------
        Public Property htxtCZRZSortColumnIndex() As String
            Get
                htxtCZRZSortColumnIndex = m_strhtxtCZRZSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtCZRZSortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtCZRZSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtCZRZSortType属性
        '----------------------------------------------------------------
        Public Property htxtCZRZSortType() As String
            Get
                htxtCZRZSortType = m_strhtxtCZRZSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtCZRZSortType = Value
                Catch ex As Exception
                    m_strhtxtCZRZSortType = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' htxtDivLeftCZRZ属性
        '----------------------------------------------------------------
        Public Property htxtDivLeftCZRZ() As String
            Get
                htxtDivLeftCZRZ = m_strhtxtDivLeftCZRZ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftCZRZ = Value
                Catch ex As Exception
                    m_strhtxtDivLeftCZRZ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopCZRZ属性
        '----------------------------------------------------------------
        Public Property htxtDivTopCZRZ() As String
            Get
                htxtDivTopCZRZ = m_strhtxtDivTopCZRZ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopCZRZ = Value
                Catch ex As Exception
                    m_strhtxtDivTopCZRZ = ""
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
        ' txtCZRZPageIndex属性
        '----------------------------------------------------------------
        Public Property txtCZRZPageIndex() As String
            Get
                txtCZRZPageIndex = m_strtxtCZRZPageIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtCZRZPageIndex = Value
                Catch ex As Exception
                    m_strtxtCZRZPageIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtCZRZPageSize属性
        '----------------------------------------------------------------
        Public Property txtCZRZPageSize() As String
            Get
                txtCZRZPageSize = m_strtxtCZRZPageSize
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtCZRZPageSize = Value
                Catch ex As Exception
                    m_strtxtCZRZPageSize = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' txtCZRZSearch_CZR属性
        '----------------------------------------------------------------
        Public Property txtCZRZSearch_CZR() As String
            Get
                txtCZRZSearch_CZR = m_strtxtCZRZSearch_CZR
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtCZRZSearch_CZR = Value
                Catch ex As Exception
                    m_strtxtCZRZSearch_CZR = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtCZRZSearch_CZSM属性
        '----------------------------------------------------------------
        Public Property txtCZRZSearch_CZSM() As String
            Get
                txtCZRZSearch_CZSM = m_strtxtCZRZSearch_CZSM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtCZRZSearch_CZSM = Value
                Catch ex As Exception
                    m_strtxtCZRZSearch_CZSM = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtCZRZSearch_CZSJMin属性
        '----------------------------------------------------------------
        Public Property txtCZRZSearch_CZSJMin() As String
            Get
                txtCZRZSearch_CZSJMin = m_strtxtCZRZSearch_CZSJMin
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtCZRZSearch_CZSJMin = Value
                Catch ex As Exception
                    m_strtxtCZRZSearch_CZSJMin = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtCZRZSearch_CZSJMax属性
        '----------------------------------------------------------------
        Public Property txtCZRZSearch_CZSJMax() As String
            Get
                txtCZRZSearch_CZSJMax = m_strtxtCZRZSearch_CZSJMax
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtCZRZSearch_CZSJMax = Value
                Catch ex As Exception
                    m_strtxtCZRZSearch_CZSJMax = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' grdCZRZPageSize属性
        '----------------------------------------------------------------
        Public Property grdCZRZPageSize() As Integer
            Get
                grdCZRZPageSize = m_intPageSize_grdCZRZ
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_grdCZRZ = Value
                Catch ex As Exception
                    m_intPageSize_grdCZRZ = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdCZRZCurrentPageIndex属性
        '----------------------------------------------------------------
        Public Property grdCZRZCurrentPageIndex() As Integer
            Get
                grdCZRZCurrentPageIndex = m_intCurrentPageIndex_grdCZRZ
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intCurrentPageIndex_grdCZRZ = Value
                Catch ex As Exception
                    m_intCurrentPageIndex_grdCZRZ = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdCZRZSelectedIndex属性
        '----------------------------------------------------------------
        Public Property grdCZRZSelectedIndex() As Integer
            Get
                grdCZRZSelectedIndex = m_intSelectedIndex_grdCZRZ
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_grdCZRZ = Value
                Catch ex As Exception
                    m_intSelectedIndex_grdCZRZ = 0
                End Try
            End Set
        End Property

    End Class

End Namespace
