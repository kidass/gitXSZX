Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IMGgxxNbltHtgl
    '
    ' ���������� 
    '     ggxx_nblt_htgl.aspxģ�鱾��ָ��ֳ���Ҫ����Ϣ
    '----------------------------------------------------------------
    <Serializable()> Public Class IMGgxxNbltHtgl
        Implements IDisposable

        '----------------------------------------------------------------
        'hidden textbox
        '----------------------------------------------------------------
        Private m_strhtxtNBLTQuery As String                      'htxtNBLTQuery
        Private m_strhtxtNBLTRows As String                       'htxtNBLTRows
        Private m_strhtxtNBLTSort As String                       'htxtNBLTSort
        Private m_strhtxtNBLTSortColumnIndex As String            'htxtNBLTSortColumnIndex
        Private m_strhtxtNBLTSortType As String                   'htxtNBLTSortType
        Private m_strhtxtDivLeftNBLT As String                    'htxtDivLeftNBLT
        Private m_strhtxtDivTopNBLT As String                     'htxtDivTopNBLT
        Private m_strhtxtDivLeftBody As String                    'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String                     'htxtDivTopBody

        Private m_strhtxtSessionIdQuery As String                 'htxtSessionIdQuery

        '----------------------------------------------------------------
        'asp:textbox
        '----------------------------------------------------------------
        Private m_strtxtNBLTPageIndex As String                  'txtNBLTPageIndex
        Private m_strtxtNBLTPageSize As String                   'txtNBLTPageSize
        Private m_strtxtNBLTSearch_RYDM As String                'txtNBLTSearch_RYDM
        Private m_strtxtNBLTSearch_RYMC As String                'txtNBLTSearch_RYMC
        Private m_intSelectedIndex_ddlNBLTSearch_SFZC As Integer 'ddlNBLTSearch_SFZC
        Private m_intSelectedIndex_ddlNBLTSearch_SFTY As Integer 'ddlNBLTSearch_SFTY

        Private m_strtxtQSRQ As String                           'txtQSRQ
        Private m_strtxtJSRQ As String                           'txtJSRQ

        '----------------------------------------------------------------
        'asp:datagrid - grdNBLT
        '----------------------------------------------------------------
        Private m_intPageSize_grdNBLT As Integer
        Private m_intSelectedIndex_grdNBLT As Integer
        Private m_intCurrentPageIndex_grdNBLT As Integer











        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            'hidden
            m_strhtxtNBLTQuery = ""
            m_strhtxtNBLTRows = ""
            m_strhtxtNBLTSort = ""
            m_strhtxtNBLTSortColumnIndex = ""
            m_strhtxtNBLTSortType = ""
            m_strhtxtDivLeftNBLT = ""
            m_strhtxtDivTopNBLT = ""
            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""

            m_strhtxtSessionIdQuery = ""

            'textbox
            m_strtxtNBLTPageIndex = ""
            m_strtxtNBLTPageSize = ""
            m_strtxtNBLTSearch_RYDM = ""
            m_strtxtNBLTSearch_RYMC = ""
            m_intSelectedIndex_ddlNBLTSearch_SFZC = -1
            m_intSelectedIndex_ddlNBLTSearch_SFTY = -1

            m_strtxtQSRQ = ""
            m_strtxtJSRQ = ""

            'datagrid
            m_intPageSize_grdNBLT = 0
            m_intCurrentPageIndex_grdNBLT = 0
            m_intSelectedIndex_grdNBLT = -1

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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMGgxxNbltHtgl)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub













        '----------------------------------------------------------------
        ' htxtNBLTQuery����
        '----------------------------------------------------------------
        Public Property htxtNBLTQuery() As String
            Get
                htxtNBLTQuery = m_strhtxtNBLTQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtNBLTQuery = Value
                Catch ex As Exception
                    m_strhtxtNBLTQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtNBLTRows����
        '----------------------------------------------------------------
        Public Property htxtNBLTRows() As String
            Get
                htxtNBLTRows = m_strhtxtNBLTRows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtNBLTRows = Value
                Catch ex As Exception
                    m_strhtxtNBLTRows = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtNBLTSort����
        '----------------------------------------------------------------
        Public Property htxtNBLTSort() As String
            Get
                htxtNBLTSort = m_strhtxtNBLTSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtNBLTSort = Value
                Catch ex As Exception
                    m_strhtxtNBLTSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtNBLTSortColumnIndex����
        '----------------------------------------------------------------
        Public Property htxtNBLTSortColumnIndex() As String
            Get
                htxtNBLTSortColumnIndex = m_strhtxtNBLTSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtNBLTSortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtNBLTSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtNBLTSortType����
        '----------------------------------------------------------------
        Public Property htxtNBLTSortType() As String
            Get
                htxtNBLTSortType = m_strhtxtNBLTSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtNBLTSortType = Value
                Catch ex As Exception
                    m_strhtxtNBLTSortType = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' htxtDivLeftNBLT����
        '----------------------------------------------------------------
        Public Property htxtDivLeftNBLT() As String
            Get
                htxtDivLeftNBLT = m_strhtxtDivLeftNBLT
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftNBLT = Value
                Catch ex As Exception
                    m_strhtxtDivLeftNBLT = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopNBLT����
        '----------------------------------------------------------------
        Public Property htxtDivTopNBLT() As String
            Get
                htxtDivTopNBLT = m_strhtxtDivTopNBLT
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopNBLT = Value
                Catch ex As Exception
                    m_strhtxtDivTopNBLT = ""
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
        ' txtNBLTPageIndex����
        '----------------------------------------------------------------
        Public Property txtNBLTPageIndex() As String
            Get
                txtNBLTPageIndex = m_strtxtNBLTPageIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtNBLTPageIndex = Value
                Catch ex As Exception
                    m_strtxtNBLTPageIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtNBLTPageSize����
        '----------------------------------------------------------------
        Public Property txtNBLTPageSize() As String
            Get
                txtNBLTPageSize = m_strtxtNBLTPageSize
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtNBLTPageSize = Value
                Catch ex As Exception
                    m_strtxtNBLTPageSize = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' txtNBLTSearch_RYDM����
        '----------------------------------------------------------------
        Public Property txtNBLTSearch_RYDM() As String
            Get
                txtNBLTSearch_RYDM = m_strtxtNBLTSearch_RYDM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtNBLTSearch_RYDM = Value
                Catch ex As Exception
                    m_strtxtNBLTSearch_RYDM = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtNBLTSearch_RYMC����
        '----------------------------------------------------------------
        Public Property txtNBLTSearch_RYMC() As String
            Get
                txtNBLTSearch_RYMC = m_strtxtNBLTSearch_RYMC
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtNBLTSearch_RYMC = Value
                Catch ex As Exception
                    m_strtxtNBLTSearch_RYMC = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' ddlNBLTSearch_SFZC_SelectedIndex����
        '----------------------------------------------------------------
        Public Property ddlNBLTSearch_SFZC_SelectedIndex() As Integer
            Get
                ddlNBLTSearch_SFZC_SelectedIndex = m_intSelectedIndex_ddlNBLTSearch_SFZC
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_ddlNBLTSearch_SFZC = Value
                Catch ex As Exception
                    m_intSelectedIndex_ddlNBLTSearch_SFZC = -1
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' ddlNBLTSearch_SFTY_SelectedIndex����
        '----------------------------------------------------------------
        Public Property ddlNBLTSearch_SFTY_SelectedIndex() As Integer
            Get
                ddlNBLTSearch_SFTY_SelectedIndex = m_intSelectedIndex_ddlNBLTSearch_SFTY
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_ddlNBLTSearch_SFTY = Value
                Catch ex As Exception
                    m_intSelectedIndex_ddlNBLTSearch_SFTY = -1
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' grdNBLTPageSize����
        '----------------------------------------------------------------
        Public Property grdNBLTPageSize() As Integer
            Get
                grdNBLTPageSize = m_intPageSize_grdNBLT
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_grdNBLT = Value
                Catch ex As Exception
                    m_intPageSize_grdNBLT = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdNBLTCurrentPageIndex����
        '----------------------------------------------------------------
        Public Property grdNBLTCurrentPageIndex() As Integer
            Get
                grdNBLTCurrentPageIndex = m_intCurrentPageIndex_grdNBLT
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intCurrentPageIndex_grdNBLT = Value
                Catch ex As Exception
                    m_intCurrentPageIndex_grdNBLT = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdNBLTSelectedIndex����
        '----------------------------------------------------------------
        Public Property grdNBLTSelectedIndex() As Integer
            Get
                grdNBLTSelectedIndex = m_intSelectedIndex_grdNBLT
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_grdNBLT = Value
                Catch ex As Exception
                    m_intSelectedIndex_grdNBLT = -1
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' txtQSRQ����
        '----------------------------------------------------------------
        Public Property txtQSRQ() As String
            Get
                txtQSRQ = m_strtxtQSRQ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtQSRQ = Value
                Catch ex As Exception
                    m_strtxtQSRQ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtJSRQ����
        '----------------------------------------------------------------
        Public Property txtJSRQ() As String
            Get
                txtJSRQ = m_strtxtJSRQ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtJSRQ = Value
                Catch ex As Exception
                    m_strtxtJSRQ = ""
                End Try
            End Set
        End Property

    End Class

End Namespace
