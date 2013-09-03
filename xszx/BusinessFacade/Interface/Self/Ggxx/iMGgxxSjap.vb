Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IMGgxxSjap
    '
    ' 功能描述： 
    '     ggxx_sjap.aspx模块本身恢复现场需要的信息
    '----------------------------------------------------------------
    <Serializable()> Public Class IMGgxxSjap
        Implements IDisposable

        '----------------------------------------------------------------
        'hidden textbox
        '----------------------------------------------------------------
        Private m_strhtxtLDAPQuery As String                      'htxtLDAPQuery
        Private m_strhtxtLDAPRows As String                       'htxtLDAPRows
        Private m_strhtxtLDAPSort As String                       'htxtLDAPSort
        Private m_strhtxtLDAPSortColumnIndex As String            'htxtLDAPSortColumnIndex
        Private m_strhtxtLDAPSortType As String                   'htxtLDAPSortType
        Private m_strhtxtDivLeftLDAP As String                    'htxtDivLeftLDAP
        Private m_strhtxtDivTopLDAP As String                     'htxtDivTopLDAP
        Private m_strhtxtDivLeftBody As String                    'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String                     'htxtDivTopBody

        Private m_strhtxtSessionIdQueryLDAP As String             'htxtSessionIdQueryLDAP

        '----------------------------------------------------------------
        'asp:textbox
        '----------------------------------------------------------------
        Private m_strtxtLDAPPageIndex As String                  'txtLDAPPageIndex
        Private m_strtxtLDAPPageSize As String                   'txtLDAPPageSize
        Private m_strtxtSJAPSearch_APRQMin As String             'txtLDAPSearch_APRQMin
        Private m_strtxtSJAPSearch_APRQMax As String             'txtLDAPSearch_APRQMax
        Private m_strtxtSJAPSearch_RY As String                  'txtSJAPSearch_RY
        Private m_strtxtSJAPSearch_DD As String                  'txtSJAPSearch_DD
        Private m_strtxtSJAPSearch_SY As String                  'txtSJAPSearch_SY
        Private m_strtxtSJAPSearch_DH As String                  'txtSJAPSearch_DH
        Private m_strtxtSJAPSearch_BZ As String                  'txtSJAPSearch_BZ

        '----------------------------------------------------------------
        'asp:datagrid - grdLDAP
        '----------------------------------------------------------------
        Private m_intPageSize_grdLDAP As Integer
        Private m_intSelectedIndex_grdLDAP As Integer
        Private m_intCurrentPageIndex_grdLDAP As Integer

        '----------------------------------------------------------------
        'asp:RadioButtonList - rblLDAPSearchAPRQ
        '----------------------------------------------------------------
        'Private m_intSelectedIndex_rblLDAPSearchAPRQ As Integer












        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            'hidden
            m_strhtxtLDAPQuery = ""
            m_strhtxtLDAPRows = ""
            m_strhtxtLDAPSort = ""
            m_strhtxtLDAPSortColumnIndex = ""
            m_strhtxtLDAPSortType = ""
            m_strhtxtDivLeftLDAP = ""
            m_strhtxtDivTopLDAP = ""
            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""

            m_strhtxtSessionIdQueryLDAP = ""

            'textbox
            m_strtxtLDAPPageIndex = ""
            m_strtxtLDAPPageSize = ""
            m_strtxtSJAPSearch_RY = ""
            m_strtxtSJAPSearch_DD = ""
            m_strtxtSJAPSearch_SY = ""
            m_strtxtSJAPSearch_DH = ""
            m_strtxtSJAPSearch_BZ = ""
            m_strtxtSJAPSearch_APRQMin = ""
            m_strtxtSJAPSearch_APRQMax = ""

            'datagrid
            m_intPageSize_grdLDAP = 0
            m_intCurrentPageIndex_grdLDAP = 0
            m_intSelectedIndex_grdLDAP = -1

            'RadioButtonList
            'm_intSelectedIndex_rblLDAPSearchAPRQ = -1

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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMGgxxLdap)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub














        '----------------------------------------------------------------
        ' htxtLDAPQuery属性
        '----------------------------------------------------------------
        Public Property htxtLDAPQuery() As String
            Get
                htxtLDAPQuery = m_strhtxtLDAPQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtLDAPQuery = Value
                Catch ex As Exception
                    m_strhtxtLDAPQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtLDAPRows属性
        '----------------------------------------------------------------
        Public Property htxtLDAPRows() As String
            Get
                htxtLDAPRows = m_strhtxtLDAPRows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtLDAPRows = Value
                Catch ex As Exception
                    m_strhtxtLDAPRows = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtLDAPSort属性
        '----------------------------------------------------------------
        Public Property htxtLDAPSort() As String
            Get
                htxtLDAPSort = m_strhtxtLDAPSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtLDAPSort = Value
                Catch ex As Exception
                    m_strhtxtLDAPSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtLDAPSortColumnIndex属性
        '----------------------------------------------------------------
        Public Property htxtLDAPSortColumnIndex() As String
            Get
                htxtLDAPSortColumnIndex = m_strhtxtLDAPSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtLDAPSortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtLDAPSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtLDAPSortType属性
        '----------------------------------------------------------------
        Public Property htxtLDAPSortType() As String
            Get
                htxtLDAPSortType = m_strhtxtLDAPSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtLDAPSortType = Value
                Catch ex As Exception
                    m_strhtxtLDAPSortType = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' htxtDivLeftLDAP属性
        '----------------------------------------------------------------
        Public Property htxtDivLeftLDAP() As String
            Get
                htxtDivLeftLDAP = m_strhtxtDivLeftLDAP
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftLDAP = Value
                Catch ex As Exception
                    m_strhtxtDivLeftLDAP = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopLDAP属性
        '----------------------------------------------------------------
        Public Property htxtDivTopLDAP() As String
            Get
                htxtDivTopLDAP = m_strhtxtDivTopLDAP
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopLDAP = Value
                Catch ex As Exception
                    m_strhtxtDivTopLDAP = ""
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
        ' htxtSessionIdQueryLDAP属性
        '----------------------------------------------------------------
        Public Property htxtSessionIdQueryLDAP() As String
            Get
                htxtSessionIdQueryLDAP = m_strhtxtSessionIdQueryLDAP
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtSessionIdQueryLDAP = Value
                Catch ex As Exception
                    m_strhtxtSessionIdQueryLDAP = ""
                End Try
            End Set
        End Property





        '----------------------------------------------------------------
        ' txtLDAPPageIndex属性
        '----------------------------------------------------------------
        Public Property txtLDAPPageIndex() As String
            Get
                txtLDAPPageIndex = m_strtxtLDAPPageIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtLDAPPageIndex = Value
                Catch ex As Exception
                    m_strtxtLDAPPageIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtLDAPPageSize属性
        '----------------------------------------------------------------
        Public Property txtLDAPPageSize() As String
            Get
                txtLDAPPageSize = m_strtxtLDAPPageSize
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtLDAPPageSize = Value
                Catch ex As Exception
                    m_strtxtLDAPPageSize = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' txtSJAPSearch_RY属性
        '----------------------------------------------------------------
        Public Property txtSJAPSearch_RY() As String
            Get
                txtSJAPSearch_RY = m_strtxtSJAPSearch_RY
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSJAPSearch_RY = Value
                Catch ex As Exception
                    m_strtxtSJAPSearch_RY = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtSJAPSearch_DD属性
        '----------------------------------------------------------------
        Public Property txtSJAPSearch_DD() As String
            Get
                txtSJAPSearch_DD = m_strtxtSJAPSearch_DD
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSJAPSearch_DD = Value
                Catch ex As Exception
                    m_strtxtSJAPSearch_DD = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtSJAPSearch_SY属性
        '----------------------------------------------------------------
        Public Property txtSJAPSearch_SY() As String
            Get
                txtSJAPSearch_SY = m_strtxtSJAPSearch_SY
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSJAPSearch_SY = Value
                Catch ex As Exception
                    m_strtxtSJAPSearch_SY = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtSJAPSearch_DH属性
        '----------------------------------------------------------------
        Public Property txtSJAPSearch_DH() As String
            Get
                txtSJAPSearch_DH = m_strtxtSJAPSearch_DH
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSJAPSearch_DH = Value
                Catch ex As Exception
                    m_strtxtSJAPSearch_DH = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtSJAPSearch_BZ属性
        '----------------------------------------------------------------
        Public Property txtSJAPSearch_BZ() As String
            Get
                txtSJAPSearch_BZ = m_strtxtSJAPSearch_BZ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSJAPSearch_BZ = Value
                Catch ex As Exception
                    m_strtxtSJAPSearch_BZ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtSJAPSearch_APRQMin属性
        '----------------------------------------------------------------
        Public Property txtSJAPSearch_APRQMin() As String
            Get
                txtSJAPSearch_APRQMin = m_strtxtSJAPSearch_APRQMin
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSJAPSearch_APRQMin = Value
                Catch ex As Exception
                    m_strtxtSJAPSearch_APRQMin = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtSJAPSearch_APRQMax属性
        '----------------------------------------------------------------
        Public Property txtSJAPSearch_APRQMax() As String
            Get
                txtSJAPSearch_APRQMax = m_strtxtSJAPSearch_APRQMax
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSJAPSearch_APRQMax = Value
                Catch ex As Exception
                    m_strtxtSJAPSearch_APRQMax = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' grdLDAPPageSize属性
        '----------------------------------------------------------------
        Public Property grdLDAPPageSize() As Integer
            Get
                grdLDAPPageSize = m_intPageSize_grdLDAP
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_grdLDAP = Value
                Catch ex As Exception
                    m_intPageSize_grdLDAP = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdLDAPCurrentPageIndex属性
        '----------------------------------------------------------------
        Public Property grdLDAPCurrentPageIndex() As Integer
            Get
                grdLDAPCurrentPageIndex = m_intCurrentPageIndex_grdLDAP
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intCurrentPageIndex_grdLDAP = Value
                Catch ex As Exception
                    m_intCurrentPageIndex_grdLDAP = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdLDAPSelectedIndex属性
        '----------------------------------------------------------------
        Public Property grdLDAPSelectedIndex() As Integer
            Get
                grdLDAPSelectedIndex = m_intSelectedIndex_grdLDAP
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_grdLDAP = Value
                Catch ex As Exception
                    m_intSelectedIndex_grdLDAP = -1
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' rblLDAPSearchAPRQSelectedIndex属性
        '----------------------------------------------------------------
        'Public Property rblLDAPSearchAPRQSelectedIndex() As Integer
        '    Get
        '        rblLDAPSearchAPRQSelectedIndex = m_intSelectedIndex_rblLDAPSearchAPRQ
        '    End Get
        '    Set(ByVal Value As Integer)
        '        Try
        '            m_intSelectedIndex_rblLDAPSearchAPRQ = Value
        '        Catch ex As Exception
        '            m_intSelectedIndex_rblLDAPSearchAPRQ = -1
        '        End Try
        '    End Set
        'End Property

    End Class

End Namespace

