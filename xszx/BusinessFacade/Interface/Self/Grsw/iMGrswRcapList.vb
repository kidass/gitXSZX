Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IMGrswRcapList
    '
    ' ���������� 
    '     grsw_rcap_list.aspxģ�鱾��ָ��ֳ���Ҫ����Ϣ
    '----------------------------------------------------------------
    <Serializable()> Public Class IMGrswRcapList
        Implements IDisposable

        '----------------------------------------------------------------
        'hidden textbox
        '----------------------------------------------------------------
        Private m_strhtxtRCAPQuery As String                      'htxtRCAPQuery
        Private m_strhtxtRCAPRows As String                       'htxtRCAPRows
        Private m_strhtxtRCAPSort As String                       'htxtRCAPSort
        Private m_strhtxtRCAPSortColumnIndex As String            'htxtRCAPSortColumnIndex
        Private m_strhtxtRCAPSortType As String                   'htxtRCAPSortType
        Private m_strhtxtDivLeftRCAP As String                    'htxtDivLeftRCAP
        Private m_strhtxtDivTopRCAP As String                     'htxtDivTopRCAP
        Private m_strhtxtDivLeftBody As String                    'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String                     'htxtDivTopBody

        Private m_strhtxtSessionIdQuery As String                 'htxtSessionIdQuery

        '----------------------------------------------------------------
        'asp:textbox
        '----------------------------------------------------------------
        Private m_strtxtRCAPPageIndex As String                  'txtRCAPPageIndex
        Private m_strtxtRCAPPageSize As String                   'txtRCAPPageSize

        Private m_strtxtSearch_ZT As String                      'txtSearch_ZT
        Private m_strtxtSearch_KSSJ As String                    'txtSearch_KSSJ
        Private m_strtxtSearch_JSSJ As String                    'txtSearch_JSSJ
        Private m_intSelectedIndex_ddlSearch_JJ As Integer       'ddlSearch_JJ_SelectedIndex
        Private m_intSelectedIndex_ddlSearch_WC As Integer       'ddlSearch_WC_SelectedIndex
        Private m_intSelectedIndex_ddlSearch_TX As Integer       'ddlSearch_TX_SelectedIndex

        '----------------------------------------------------------------
        'asp:datagrid - grdRCAP
        '----------------------------------------------------------------
        Private m_intPageSize_grdRCAP As Integer
        Private m_intSelectedIndex_grdRCAP As Integer
        Private m_intCurrentPageIndex_grdRCAP As Integer












        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            'hidden
            m_strhtxtRCAPQuery = ""
            m_strhtxtRCAPRows = ""
            m_strhtxtRCAPSort = ""
            m_strhtxtRCAPSortColumnIndex = ""
            m_strhtxtRCAPSortType = ""
            m_strhtxtDivLeftRCAP = ""
            m_strhtxtDivTopRCAP = ""
            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""

            m_strhtxtSessionIdQuery = ""

            'textbox
            m_strtxtRCAPPageIndex = ""
            m_strtxtRCAPPageSize = ""

            m_strtxtSearch_ZT = ""
            m_strtxtSearch_KSSJ = ""
            m_strtxtSearch_JSSJ = ""
            m_intSelectedIndex_ddlSearch_JJ = -1
            m_intSelectedIndex_ddlSearch_WC = -1
            m_intSelectedIndex_ddlSearch_TX = -1

            'datagrid
            m_intPageSize_grdRCAP = 0
            m_intCurrentPageIndex_grdRCAP = 0
            m_intSelectedIndex_grdRCAP = -1

        End Sub

        '----------------------------------------------------------------
        ' ������������
        '----------------------------------------------------------------
        Public Sub Dispose() Implements System.IDisposable.Dispose
            Dispose(True)
        End Sub

        '----------------------------------------------------------------
        ' �ͷű�����Դ
        '----------------------------------------------------------------
        Protected Sub Dispose(ByVal disposing As Boolean)
            If (Not disposing) Then
                Exit Sub
            End If
        End Sub

        '----------------------------------------------------------------
        ' ��ȫ�ͷű�����Դ
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMGrswRcapList)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub












        '----------------------------------------------------------------
        ' htxtRCAPQuery����
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
        ' htxtRCAPRows����
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
        ' htxtRCAPSort����
        '----------------------------------------------------------------
        Public Property htxtRCAPSort() As String
            Get
                htxtRCAPSort = m_strhtxtRCAPSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtRCAPSort = Value
                Catch ex As Exception
                    m_strhtxtRCAPSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtRCAPSortColumnIndex����
        '----------------------------------------------------------------
        Public Property htxtRCAPSortColumnIndex() As String
            Get
                htxtRCAPSortColumnIndex = m_strhtxtRCAPSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtRCAPSortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtRCAPSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtRCAPSortType����
        '----------------------------------------------------------------
        Public Property htxtRCAPSortType() As String
            Get
                htxtRCAPSortType = m_strhtxtRCAPSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtRCAPSortType = Value
                Catch ex As Exception
                    m_strhtxtRCAPSortType = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' htxtDivLeftRCAP����
        '----------------------------------------------------------------
        Public Property htxtDivLeftRCAP() As String
            Get
                htxtDivLeftRCAP = m_strhtxtDivLeftRCAP
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftRCAP = Value
                Catch ex As Exception
                    m_strhtxtDivLeftRCAP = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopRCAP����
        '----------------------------------------------------------------
        Public Property htxtDivTopRCAP() As String
            Get
                htxtDivTopRCAP = m_strhtxtDivTopRCAP
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopRCAP = Value
                Catch ex As Exception
                    m_strhtxtDivTopRCAP = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivLeftBody����
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
        ' htxtDivTopBody����
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
        ' htxtSessionIdQuery����
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
        ' txtRCAPPageIndex����
        '----------------------------------------------------------------
        Public Property txtRCAPPageIndex() As String
            Get
                txtRCAPPageIndex = m_strtxtRCAPPageIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtRCAPPageIndex = Value
                Catch ex As Exception
                    m_strtxtRCAPPageIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtRCAPPageSize����
        '----------------------------------------------------------------
        Public Property txtRCAPPageSize() As String
            Get
                txtRCAPPageSize = m_strtxtRCAPPageSize
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtRCAPPageSize = Value
                Catch ex As Exception
                    m_strtxtRCAPPageSize = ""
                End Try
            End Set
        End Property





        '----------------------------------------------------------------
        ' txtSearch_ZT����
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
        ' txtSearch_KSSJ����
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
        ' txtSearch_JSSJ����
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
        ' ddlSearch_JJ_SelectedIndex����
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
        ' ddlSearch_WC_SelectedIndex����
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
        ' ddlSearch_TX_SelectedIndex����
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




        '----------------------------------------------------------------
        ' grdRCAPPageSize����
        '----------------------------------------------------------------
        Public Property grdRCAPPageSize() As Integer
            Get
                grdRCAPPageSize = m_intPageSize_grdRCAP
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_grdRCAP = Value
                Catch ex As Exception
                    m_intPageSize_grdRCAP = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdRCAPCurrentPageIndex����
        '----------------------------------------------------------------
        Public Property grdRCAPCurrentPageIndex() As Integer
            Get
                grdRCAPCurrentPageIndex = m_intCurrentPageIndex_grdRCAP
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intCurrentPageIndex_grdRCAP = Value
                Catch ex As Exception
                    m_intCurrentPageIndex_grdRCAP = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdRCAPSelectedIndex����
        '----------------------------------------------------------------
        Public Property grdRCAPSelectedIndex() As Integer
            Get
                grdRCAPSelectedIndex = m_intSelectedIndex_grdRCAP
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_grdRCAP = Value
                Catch ex As Exception
                    m_intSelectedIndex_grdRCAP = -1
                End Try
            End Set
        End Property

    End Class

End Namespace
