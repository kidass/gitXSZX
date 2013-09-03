Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IMGrswRcapMonth
    '
    ' 功能描述： 
    '     grsw_rcap_month.aspx模块本身恢复现场需要的信息
    '----------------------------------------------------------------
    <Serializable()> Public Class IMGrswRcapMonth
        Implements IDisposable

        '----------------------------------------------------------------
        'hidden textbox
        '----------------------------------------------------------------
        Private m_strhtxtRCAPQuery As String                      'htxtRCAPQuery
        Private m_strhtxtRCAPRows As String                       'htxtRCAPRows
        Private m_strhtxtDivLeftMain As String                    'htxtDivLeftMain
        Private m_strhtxtDivTopMain As String                     'htxtDivTopMain
        Private m_strhtxtDivLeftBody As String                    'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String                     'htxtDivTopBody
        Private m_strhtxtSessionIdQuery As String                 'htxtSessionIdQuery

        '----------------------------------------------------------------
        'hidden textbox
        '----------------------------------------------------------------
        Private m_strhtxtYear As String                           'htxtYear
        Private m_strhtxtMonth As String                          'htxtMonth
        Private m_strtxtYear As String                            'txtYear
        Private m_intSelectedIndex_ddlMonth As Integer            'ddlMonth_SelectedIndex

        '----------------------------------------------------------------
        'asp:textbox
        '----------------------------------------------------------------
        Private m_strtxtSearch_ZT As String                      'txtSearch_ZT
        Private m_strtxtSearch_KSSJ As String                    'txtSearch_KSSJ
        Private m_strtxtSearch_JSSJ As String                    'txtSearch_JSSJ
        Private m_intSelectedIndex_ddlSearch_JJ As Integer       'ddlSearch_JJ_SelectedIndex
        Private m_intSelectedIndex_ddlSearch_WC As Integer       'ddlSearch_WC_SelectedIndex
        Private m_intSelectedIndex_ddlSearch_TX As Integer       'ddlSearch_TX_SelectedIndex












        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            'hidden
            m_strhtxtRCAPQuery = ""
            m_strhtxtRCAPRows = ""
            m_strhtxtDivLeftMain = ""
            m_strhtxtDivTopMain = ""
            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""
            m_strhtxtSessionIdQuery = ""

            'hidden
            m_strhtxtYear = ""
            m_strhtxtMonth = ""
            m_strtxtYear = ""
            m_intSelectedIndex_ddlMonth = -1

            'textbox
            m_strtxtSearch_ZT = ""
            m_strtxtSearch_KSSJ = ""
            m_strtxtSearch_JSSJ = ""
            m_intSelectedIndex_ddlSearch_JJ = -1
            m_intSelectedIndex_ddlSearch_WC = -1
            m_intSelectedIndex_ddlSearch_TX = -1

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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMGrswRcapMonth)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub













        '----------------------------------------------------------------
        ' htxtRCAPQuery属性
        '----------------------------------------------------------------
        Public Property htxtRCAPQuery() As String
            Get
                htxtRCAPQuery = m_strhtxtRCAPQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtRCAPQuery = Value
                Catch ex As Exception
                    m_strhtxtRCAPQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtRCAPRows属性
        '----------------------------------------------------------------
        Public Property htxtRCAPRows() As String
            Get
                htxtRCAPRows = m_strhtxtRCAPRows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtRCAPRows = Value
                Catch ex As Exception
                    m_strhtxtRCAPRows = ""
                End Try
            End Set
        End Property






        '----------------------------------------------------------------
        ' htxtDivLeftMain属性
        '----------------------------------------------------------------
        Public Property htxtDivLeftMain() As String
            Get
                htxtDivLeftMain = m_strhtxtDivLeftMain
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftMain = Value
                Catch ex As Exception
                    m_strhtxtDivLeftMain = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopMain属性
        '----------------------------------------------------------------
        Public Property htxtDivTopMain() As String
            Get
                htxtDivTopMain = m_strhtxtDivTopMain
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopMain = Value
                Catch ex As Exception
                    m_strhtxtDivTopMain = ""
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
        ' htxtYear属性
        '----------------------------------------------------------------
        Public Property htxtYear() As String
            Get
                htxtYear = m_strhtxtYear
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtYear = Value
                Catch ex As Exception
                    m_strhtxtYear = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtMonth属性
        '----------------------------------------------------------------
        Public Property htxtMonth() As String
            Get
                htxtMonth = m_strhtxtMonth
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtMonth = Value
                Catch ex As Exception
                    m_strhtxtMonth = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtYear属性
        '----------------------------------------------------------------
        Public Property txtYear() As String
            Get
                txtYear = m_strtxtYear
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtYear = Value
                Catch ex As Exception
                    m_strtxtYear = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' ddlMonth_SelectedIndex属性
        '----------------------------------------------------------------
        Public Property ddlMonth_SelectedIndex() As Integer
            Get
                ddlMonth_SelectedIndex = m_intSelectedIndex_ddlMonth
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_ddlMonth = Value
                Catch ex As Exception
                    m_intSelectedIndex_ddlMonth = -1
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
        ' txtSearch_ZT属性
        '----------------------------------------------------------------
        Public Property txtSearch_ZT() As String
            Get
                txtSearch_ZT = m_strtxtSearch_ZT
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSearch_ZT = Value
                Catch ex As Exception
                    m_strtxtSearch_ZT = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtSearch_KSSJ属性
        '----------------------------------------------------------------
        Public Property txtSearch_KSSJ() As String
            Get
                txtSearch_KSSJ = m_strtxtSearch_KSSJ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSearch_KSSJ = Value
                Catch ex As Exception
                    m_strtxtSearch_KSSJ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtSearch_JSSJ属性
        '----------------------------------------------------------------
        Public Property txtSearch_JSSJ() As String
            Get
                txtSearch_JSSJ = m_strtxtSearch_JSSJ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSearch_JSSJ = Value
                Catch ex As Exception
                    m_strtxtSearch_JSSJ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' ddlSearch_JJ_SelectedIndex属性
        '----------------------------------------------------------------
        Public Property ddlSearch_JJ_SelectedIndex() As Integer
            Get
                ddlSearch_JJ_SelectedIndex = m_intSelectedIndex_ddlSearch_JJ
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_ddlSearch_JJ = Value
                Catch ex As Exception
                    m_intSelectedIndex_ddlSearch_JJ = -1
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' ddlSearch_WC_SelectedIndex属性
        '----------------------------------------------------------------
        Public Property ddlSearch_WC_SelectedIndex() As Integer
            Get
                ddlSearch_WC_SelectedIndex = m_intSelectedIndex_ddlSearch_WC
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_ddlSearch_WC = Value
                Catch ex As Exception
                    m_intSelectedIndex_ddlSearch_WC = -1
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' ddlSearch_TX_SelectedIndex属性
        '----------------------------------------------------------------
        Public Property ddlSearch_TX_SelectedIndex() As Integer
            Get
                ddlSearch_TX_SelectedIndex = m_intSelectedIndex_ddlSearch_TX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_ddlSearch_TX = Value
                Catch ex As Exception
                    m_intSelectedIndex_ddlSearch_TX = -1
                End Try
            End Set
        End Property

    End Class

End Namespace
