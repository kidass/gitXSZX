Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IMXtpzBdkz
    '
    ' ���������� 
    '     xtpz_bdkz.aspxģ�鱾��ָ��ֳ���Ҫ����Ϣ
    '----------------------------------------------------------------
    <Serializable()> Public Class IMXtpzBdkz
        Implements IDisposable

        '----------------------------------------------------------------
        'hidden textbox
        '----------------------------------------------------------------
        Private m_strhtxtCurrentPage As String               'htxtCurrentPage
        Private m_strhtxtCurrentRow As String                'htxtCurrentRow
        Private m_strhtxtEditMode As String                  'htxtEditMode
        Private m_strhtxtEditType As String                  'htxtEditType

        Private m_strhtxtBDKZQuery As String                 'htxtBDKZQuery
        Private m_strhtxtBDKZRows As String                  'htxtBDKZRows
        Private m_strhtxtBDKZSort As String                  'htxtBDKZSort
        Private m_strhtxtBDKZSortColumnIndex As String       'htxtBDKZSortColumnIndex
        Private m_strhtxtBDKZSortType As String              'htxtBDKZSortType

        Private m_strhtxtDivLeftBDKZ As String               'htxtDivLeftBDKZ
        Private m_strhtxtDivTopBDKZ As String                'htxtDivTopBDKZ
        Private m_strhtxtDivLeftBody As String               'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String                'htxtDivTopBody

        Private m_strhtxtSessionIdBDKZQuery As String        'htxtSessionIdBDKZQuery

        '----------------------------------------------------------------
        'asp:textbox
        '----------------------------------------------------------------
        Private m_strtxtBDKZPageIndex As String               'txtBDKZPageIndex
        Private m_strtxtBDKZPageSize As String                'txtBDKZPageSize
        Private m_strtxtBDKZSearch_ZWMC As String             'txtBDKZSearch_ZWMC
        Private m_strtxtBDKZSearch_BDFW As String             'txtBDKZSearch_BDFW
        Private m_strtxtBDKZSearch_BCSM As String             'txtBDKZSearch_BCSM

        Private m_strtxtZWMC As String                        'txtZWMC
        Private m_strhtxtZWDM As String                       'htxtZWDM
        Private m_strtxtZWLB As String                        'txtZWLB
        Private m_intSelectedIndex_ddlBDFW As Integer         'ddlBDFW
        Private m_intSelectedIndex_ddlBCSM As Integer         'ddlBCSM

        '----------------------------------------------------------------
        'asp:datagrid - grdBDKZ
        '----------------------------------------------------------------
        Private m_intPageSize_grdBDKZ As Integer
        Private m_intSelectedIndex_grdBDKZ As Integer
        Private m_intCurrentPageIndex_grdBDKZ As Integer










        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            'hidden
            m_strhtxtCurrentPage = ""
            m_strhtxtCurrentRow = ""
            m_strhtxtEditMode = ""
            m_strhtxtEditType = ""

            m_strhtxtBDKZQuery = ""
            m_strhtxtBDKZRows = ""
            m_strhtxtBDKZSort = ""
            m_strhtxtBDKZSortColumnIndex = ""
            m_strhtxtBDKZSortType = ""

            m_strhtxtDivLeftBDKZ = ""
            m_strhtxtDivTopBDKZ = ""
            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""

            m_strhtxtSessionIdBDKZQuery = ""

            'textbox
            m_strtxtBDKZPageIndex = ""
            m_strtxtBDKZPageSize = ""
            m_strtxtBDKZSearch_ZWMC = ""
            m_strtxtBDKZSearch_BCSM = ""
            m_strtxtBDKZSearch_BDFW = ""

            m_strtxtZWMC = ""
            m_strhtxtZWDM = ""
            m_strtxtZWLB = ""
            m_intSelectedIndex_ddlBDFW = -1
            m_intSelectedIndex_ddlBCSM = -1

            'datagrid
            m_intPageSize_grdBDKZ = 0
            m_intCurrentPageIndex_grdBDKZ = 0
            m_intSelectedIndex_grdBDKZ = -1

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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMXtpzBdkz)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub














        '----------------------------------------------------------------
        ' htxtCurrentPage����
        '----------------------------------------------------------------
        Public Property htxtCurrentPage() As String
            Get
                htxtCurrentPage = m_strhtxtCurrentPage
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtCurrentPage = Value
                Catch ex As Exception
                    m_strhtxtCurrentPage = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtCurrentRow����
        '----------------------------------------------------------------
        Public Property htxtCurrentRow() As String
            Get
                htxtCurrentRow = m_strhtxtCurrentRow
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtCurrentRow = Value
                Catch ex As Exception
                    m_strhtxtCurrentRow = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtEditMode����
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
        ' htxtEditType����
        '----------------------------------------------------------------
        Public Property htxtEditType() As String
            Get
                htxtEditType = m_strhtxtEditType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtEditType = Value
                Catch ex As Exception
                    m_strhtxtEditType = ""
                End Try
            End Set
        End Property





        '----------------------------------------------------------------
        ' htxtBDKZQuery����
        '----------------------------------------------------------------
        Public Property htxtBDKZQuery() As String
            Get
                htxtBDKZQuery = m_strhtxtBDKZQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtBDKZQuery = Value
                Catch ex As Exception
                    m_strhtxtBDKZQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtBDKZRows����
        '----------------------------------------------------------------
        Public Property htxtBDKZRows() As String
            Get
                htxtBDKZRows = m_strhtxtBDKZRows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtBDKZRows = Value
                Catch ex As Exception
                    m_strhtxtBDKZRows = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtBDKZSort����
        '----------------------------------------------------------------
        Public Property htxtBDKZSort() As String
            Get
                htxtBDKZSort = m_strhtxtBDKZSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtBDKZSort = Value
                Catch ex As Exception
                    m_strhtxtBDKZSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtBDKZSortColumnIndex����
        '----------------------------------------------------------------
        Public Property htxtBDKZSortColumnIndex() As String
            Get
                htxtBDKZSortColumnIndex = m_strhtxtBDKZSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtBDKZSortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtBDKZSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtBDKZSortType����
        '----------------------------------------------------------------
        Public Property htxtBDKZSortType() As String
            Get
                htxtBDKZSortType = m_strhtxtBDKZSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtBDKZSortType = Value
                Catch ex As Exception
                    m_strhtxtBDKZSortType = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' htxtDivLeftBDKZ����
        '----------------------------------------------------------------
        Public Property htxtDivLeftBDKZ() As String
            Get
                htxtDivLeftBDKZ = m_strhtxtDivLeftBDKZ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftBDKZ = Value
                Catch ex As Exception
                    m_strhtxtDivLeftBDKZ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopBDKZ����
        '----------------------------------------------------------------
        Public Property htxtDivTopBDKZ() As String
            Get
                htxtDivTopBDKZ = m_strhtxtDivTopBDKZ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopBDKZ = Value
                Catch ex As Exception
                    m_strhtxtDivTopBDKZ = ""
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
        ' htxtSessionIdBDKZQuery����
        '----------------------------------------------------------------
        Public Property htxtSessionIdBDKZQuery() As String
            Get
                htxtSessionIdBDKZQuery = m_strhtxtSessionIdBDKZQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtSessionIdBDKZQuery = Value
                Catch ex As Exception
                    m_strhtxtSessionIdBDKZQuery = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' txtBDKZPageIndex����
        '----------------------------------------------------------------
        Public Property txtBDKZPageIndex() As String
            Get
                txtBDKZPageIndex = m_strtxtBDKZPageIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtBDKZPageIndex = Value
                Catch ex As Exception
                    m_strtxtBDKZPageIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtBDKZPageSize����
        '----------------------------------------------------------------
        Public Property txtBDKZPageSize() As String
            Get
                txtBDKZPageSize = m_strtxtBDKZPageSize
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtBDKZPageSize = Value
                Catch ex As Exception
                    m_strtxtBDKZPageSize = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtBDKZSearch_ZWMC����
        '----------------------------------------------------------------
        Public Property txtBDKZSearch_ZWMC() As String
            Get
                txtBDKZSearch_ZWMC = m_strtxtBDKZSearch_ZWMC
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtBDKZSearch_ZWMC = Value
                Catch ex As Exception
                    m_strtxtBDKZSearch_ZWMC = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtBDKZSearch_BCSM����
        '----------------------------------------------------------------
        Public Property txtBDKZSearch_BCSM() As String
            Get
                txtBDKZSearch_BCSM = m_strtxtBDKZSearch_BCSM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtBDKZSearch_BCSM = Value
                Catch ex As Exception
                    m_strtxtBDKZSearch_BCSM = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtBDKZSearch_BDFW����
        '----------------------------------------------------------------
        Public Property txtBDKZSearch_BDFW() As String
            Get
                txtBDKZSearch_BDFW = m_strtxtBDKZSearch_BDFW
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtBDKZSearch_BDFW = Value
                Catch ex As Exception
                    m_strtxtBDKZSearch_BDFW = ""
                End Try
            End Set
        End Property





        '----------------------------------------------------------------
        ' grdBDKZ_PageSize����
        '----------------------------------------------------------------
        Public Property grdBDKZ_PageSize() As Integer
            Get
                grdBDKZ_PageSize = m_intPageSize_grdBDKZ
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_grdBDKZ = Value
                Catch ex As Exception
                    m_intPageSize_grdBDKZ = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdBDKZ_CurrentPageIndex����
        '----------------------------------------------------------------
        Public Property grdBDKZ_CurrentPageIndex() As Integer
            Get
                grdBDKZ_CurrentPageIndex = m_intCurrentPageIndex_grdBDKZ
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intCurrentPageIndex_grdBDKZ = Value
                Catch ex As Exception
                    m_intCurrentPageIndex_grdBDKZ = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdBDKZ_SelectedIndex����
        '----------------------------------------------------------------
        Public Property grdBDKZ_SelectedIndex() As Integer
            Get
                grdBDKZ_SelectedIndex = m_intSelectedIndex_grdBDKZ
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_grdBDKZ = Value
                Catch ex As Exception
                    m_intSelectedIndex_grdBDKZ = 0
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' txtZWMC����
        '----------------------------------------------------------------
        Public Property txtZWMC() As String
            Get
                txtZWMC = m_strtxtZWMC
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtZWMC = Value
                Catch ex As Exception
                    m_strtxtZWMC = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtZWDM����
        '----------------------------------------------------------------
        Public Property htxtZWDM() As String
            Get
                htxtZWDM = m_strhtxtZWDM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtZWDM = Value
                Catch ex As Exception
                    m_strhtxtZWDM = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtZWLB����
        '----------------------------------------------------------------
        Public Property txtZWLB() As String
            Get
                txtZWLB = m_strtxtZWLB
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtZWLB = Value
                Catch ex As Exception
                    m_strtxtZWLB = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' ddlBDFW_SelectedIndex����
        '----------------------------------------------------------------
        Public Property ddlBDFW_SelectedIndex() As Integer
            Get
                ddlBDFW_SelectedIndex = m_intSelectedIndex_ddlBDFW
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_ddlBDFW = Value
                Catch ex As Exception
                    m_intSelectedIndex_ddlBDFW = -1
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' ddlBCSM_SelectedIndex����
        '----------------------------------------------------------------
        Public Property ddlBCSM_SelectedIndex() As Integer
            Get
                ddlBCSM_SelectedIndex = m_intSelectedIndex_ddlBCSM
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_ddlBCSM = Value
                Catch ex As Exception
                    m_intSelectedIndex_ddlBCSM = -1
                End Try
            End Set
        End Property

    End Class

End Namespace
