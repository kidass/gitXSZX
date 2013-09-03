Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IMXtglRzglJcrz
    '
    ' 功能描述： 
    '     xtgl_rzgl_jcrz.aspx模块本身恢复现场需要的信息
    '----------------------------------------------------------------
    <Serializable()> Public Class IMXtglRzglJcrz
        Implements IDisposable

        '----------------------------------------------------------------
        'hidden textbox
        '----------------------------------------------------------------
        Private m_strhtxtJCRZQuery As String                      'htxtJCRZQuery
        Private m_strhtxtJCRZRows As String                       'htxtJCRZRows
        Private m_strhtxtJCRZSort As String                       'htxtJCRZSort
        Private m_strhtxtJCRZSortColumnIndex As String            'htxtJCRZSortColumnIndex
        Private m_strhtxtJCRZSortType As String                   'htxtJCRZSortType
        Private m_strhtxtDivLeftJCRZ As String                    'htxtDivLeftJCRZ
        Private m_strhtxtDivTopJCRZ As String                     'htxtDivTopJCRZ
        Private m_strhtxtDivLeftBody As String                    'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String                     'htxtDivTopBody

        Private m_strhtxtSessionIdQuery As String                 'htxtSessionIdQuery

        '----------------------------------------------------------------
        'asp:textbox
        '----------------------------------------------------------------
        Private m_strtxtJCRZPageIndex As String                  'txtJCRZPageIndex
        Private m_strtxtJCRZPageSize As String                   'txtJCRZPageSize
        Private m_strtxtJCRZSearch_YHBS As String                'txtJCRZSearch_YHBS
        Private m_strtxtJCRZSearch_YHMC As String                'txtJCRZSearch_YHMC
        Private m_strtxtJCRZSearch_JQDZ As String                'txtJCRZSearch_JQDZ
        Private m_strtxtJCRZSearch_CZSJMin As String             'txtJCRZSearch_CZSJMin
        Private m_strtxtJCRZSearch_CZSJMax As String             'txtJCRZSearch_CZSJMax
        Private m_strtxtJCRZ_QSRQ As String                      'txtJCRZ_QSRQ
        Private m_strtxtJCRZ_ZZRQ As String                      'txtJCRZ_ZZRQ

        Private m_intSelectedIndex_ddlJCRZSearch_CZLX As Integer 'ddlJCRZSearch_CZLX

        '----------------------------------------------------------------
        'asp:datagrid - grdJCRZ
        '----------------------------------------------------------------
        Private m_intPageSize_grdJCRZ As Integer
        Private m_intSelectedIndex_grdJCRZ As Integer
        Private m_intCurrentPageIndex_grdJCRZ As Integer











        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            'hidden
            m_strhtxtJCRZQuery = ""
            m_strhtxtJCRZRows = ""
            m_strhtxtJCRZSort = ""
            m_strhtxtJCRZSortColumnIndex = ""
            m_strhtxtJCRZSortType = ""
            m_strhtxtDivLeftJCRZ = ""
            m_strhtxtDivTopJCRZ = ""
            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""

            m_strhtxtSessionIdQuery = ""

            'textbox
            m_strtxtJCRZPageIndex = ""
            m_strtxtJCRZPageSize = ""
            m_strtxtJCRZSearch_YHBS = ""
            m_strtxtJCRZSearch_YHMC = ""
            m_strtxtJCRZSearch_JQDZ = ""
            m_strtxtJCRZ_QSRQ = ""
            m_strtxtJCRZ_ZZRQ = ""

            'datagrid
            m_intPageSize_grdJCRZ = 0
            m_intCurrentPageIndex_grdJCRZ = 0
            m_intSelectedIndex_grdJCRZ = -1

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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMXtglRzglJcrz)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub













        '----------------------------------------------------------------
        ' htxtJCRZQuery属性
        '----------------------------------------------------------------
        Public Property htxtJCRZQuery() As String
            Get
                htxtJCRZQuery = m_strhtxtJCRZQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtJCRZQuery = Value
                Catch ex As Exception
                    m_strhtxtJCRZQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtJCRZRows属性
        '----------------------------------------------------------------
        Public Property htxtJCRZRows() As String
            Get
                htxtJCRZRows = m_strhtxtJCRZRows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtJCRZRows = Value
                Catch ex As Exception
                    m_strhtxtJCRZRows = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtJCRZSort属性
        '----------------------------------------------------------------
        Public Property htxtJCRZSort() As String
            Get
                htxtJCRZSort = m_strhtxtJCRZSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtJCRZSort = Value
                Catch ex As Exception
                    m_strhtxtJCRZSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtJCRZSortColumnIndex属性
        '----------------------------------------------------------------
        Public Property htxtJCRZSortColumnIndex() As String
            Get
                htxtJCRZSortColumnIndex = m_strhtxtJCRZSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtJCRZSortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtJCRZSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtJCRZSortType属性
        '----------------------------------------------------------------
        Public Property htxtJCRZSortType() As String
            Get
                htxtJCRZSortType = m_strhtxtJCRZSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtJCRZSortType = Value
                Catch ex As Exception
                    m_strhtxtJCRZSortType = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' htxtDivLeftJCRZ属性
        '----------------------------------------------------------------
        Public Property htxtDivLeftJCRZ() As String
            Get
                htxtDivLeftJCRZ = m_strhtxtDivLeftJCRZ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftJCRZ = Value
                Catch ex As Exception
                    m_strhtxtDivLeftJCRZ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopJCRZ属性
        '----------------------------------------------------------------
        Public Property htxtDivTopJCRZ() As String
            Get
                htxtDivTopJCRZ = m_strhtxtDivTopJCRZ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopJCRZ = Value
                Catch ex As Exception
                    m_strhtxtDivTopJCRZ = ""
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
        ' txtJCRZPageIndex属性
        '----------------------------------------------------------------
        Public Property txtJCRZPageIndex() As String
            Get
                txtJCRZPageIndex = m_strtxtJCRZPageIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtJCRZPageIndex = Value
                Catch ex As Exception
                    m_strtxtJCRZPageIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtJCRZPageSize属性
        '----------------------------------------------------------------
        Public Property txtJCRZPageSize() As String
            Get
                txtJCRZPageSize = m_strtxtJCRZPageSize
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtJCRZPageSize = Value
                Catch ex As Exception
                    m_strtxtJCRZPageSize = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' txtJCRZSearch_YHBS属性
        '----------------------------------------------------------------
        Public Property txtJCRZSearch_YHBS() As String
            Get
                txtJCRZSearch_YHBS = m_strtxtJCRZSearch_YHBS
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtJCRZSearch_YHBS = Value
                Catch ex As Exception
                    m_strtxtJCRZSearch_YHBS = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtJCRZSearch_YHMC属性
        '----------------------------------------------------------------
        Public Property txtJCRZSearch_YHMC() As String
            Get
                txtJCRZSearch_YHMC = m_strtxtJCRZSearch_YHMC
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtJCRZSearch_YHMC = Value
                Catch ex As Exception
                    m_strtxtJCRZSearch_YHMC = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtJCRZSearch_JQDZ属性
        '----------------------------------------------------------------
        Public Property txtJCRZSearch_JQDZ() As String
            Get
                txtJCRZSearch_JQDZ = m_strtxtJCRZSearch_JQDZ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtJCRZSearch_JQDZ = Value
                Catch ex As Exception
                    m_strtxtJCRZSearch_JQDZ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtJCRZSearch_CZSJMin属性
        '----------------------------------------------------------------
        Public Property txtJCRZSearch_CZSJMin() As String
            Get
                txtJCRZSearch_CZSJMin = m_strtxtJCRZSearch_CZSJMin
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtJCRZSearch_CZSJMin = Value
                Catch ex As Exception
                    m_strtxtJCRZSearch_CZSJMin = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtJCRZSearch_CZSJMax属性
        '----------------------------------------------------------------
        Public Property txtJCRZSearch_CZSJMax() As String
            Get
                txtJCRZSearch_CZSJMax = m_strtxtJCRZSearch_CZSJMax
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtJCRZSearch_CZSJMax = Value
                Catch ex As Exception
                    m_strtxtJCRZSearch_CZSJMax = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' ddlJCRZSearch_CZLX_SelectedIndex属性
        '----------------------------------------------------------------
        Public Property ddlJCRZSearch_CZLX_SelectedIndex() As Integer
            Get
                ddlJCRZSearch_CZLX_SelectedIndex = m_intSelectedIndex_ddlJCRZSearch_CZLX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_ddlJCRZSearch_CZLX = Value
                Catch ex As Exception
                    m_intSelectedIndex_ddlJCRZSearch_CZLX = -1
                End Try
            End Set
        End Property



        '----------------------------------------------------------------
        ' txtJCRZ_QSRQ属性
        '----------------------------------------------------------------
        Public Property txtJCRZ_QSRQ() As String
            Get
                txtJCRZ_QSRQ = m_strtxtJCRZ_QSRQ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtJCRZ_QSRQ = Value
                Catch ex As Exception
                    m_strtxtJCRZ_QSRQ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtJCRZ_ZZRQ属性
        '----------------------------------------------------------------
        Public Property txtJCRZ_ZZRQ() As String
            Get
                txtJCRZ_ZZRQ = m_strtxtJCRZ_ZZRQ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtJCRZ_ZZRQ = Value
                Catch ex As Exception
                    m_strtxtJCRZ_ZZRQ = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' grdJCRZPageSize属性
        '----------------------------------------------------------------
        Public Property grdJCRZPageSize() As Integer
            Get
                grdJCRZPageSize = m_intPageSize_grdJCRZ
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_grdJCRZ = Value
                Catch ex As Exception
                    m_intPageSize_grdJCRZ = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdJCRZCurrentPageIndex属性
        '----------------------------------------------------------------
        Public Property grdJCRZCurrentPageIndex() As Integer
            Get
                grdJCRZCurrentPageIndex = m_intCurrentPageIndex_grdJCRZ
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intCurrentPageIndex_grdJCRZ = Value
                Catch ex As Exception
                    m_intCurrentPageIndex_grdJCRZ = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdJCRZSelectedIndex属性
        '----------------------------------------------------------------
        Public Property grdJCRZSelectedIndex() As Integer
            Get
                grdJCRZSelectedIndex = m_intSelectedIndex_grdJCRZ
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_grdJCRZ = Value
                Catch ex As Exception
                    m_intSelectedIndex_grdJCRZ = -1
                End Try
            End Set
        End Property

    End Class

End Namespace
