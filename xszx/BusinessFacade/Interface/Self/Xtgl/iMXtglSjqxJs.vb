Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IMXtglSjqxJs
    '
    ' ���������� 
    '     xtgl_sjqx_js.aspxģ�鱾��ָ��ֳ���Ҫ����Ϣ
    '----------------------------------------------------------------
    <Serializable()> Public Class IMXtglSjqxJs
        Implements IDisposable

        '----------------------------------------------------------------
        'hidden textbox
        '----------------------------------------------------------------
        Private m_strhtxtObjectQuery As String            'htxtObjectQuery
        Private m_strhtxtObjectRows As String             'htxtObjectRows
        Private m_strhtxtObjectSort As String             'htxtObjectSort
        Private m_strhtxtObjectSortColumnIndex As String  'htxtObjectSortColumnIndex
        Private m_strhtxtObjectSortType As String         'htxtObjectSortType

        Private m_strhtxtDivLeftObject As String          'htxtDivLeftObject
        Private m_strhtxtDivTopObject As String           'htxtDivTopObject
        Private m_strhtxtDivLeftBody As String            'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String             'htxtDivTopBody

        '----------------------------------------------------------------
        'asp:textbox
        '----------------------------------------------------------------
        Private m_strtxtPageIndex As String               'txtPageIndex
        Private m_strtxtPageSize As String                'txtPageSize
        Private m_strtxtSearchDXM As String               'txtSearchDXM
        Private m_strtxtSearchDXZWM As String             'txtSearchDXZWM
        Private m_strtxtSearchDXLX As String              'txtSearchDXLX

        '----------------------------------------------------------------
        'asp:datagrid - grdObject
        '----------------------------------------------------------------
        Private m_intPageSize_grdObject As Integer
        Private m_intSelectedIndex_grdObject As Integer
        Private m_intCurrentPageIndex_grdObject As Integer

        '----------------------------------------------------------------
        'treeview - tvwServers
        '----------------------------------------------------------------
        Private m_strSelectedNodeIndex_tvwServers As String












        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
            'hidden
            m_strhtxtObjectQuery = ""
            m_strhtxtObjectRows = ""
            m_strhtxtObjectSort = ""
            m_strhtxtObjectSortColumnIndex = ""
            m_strhtxtObjectSortType = ""

            m_strhtxtDivLeftObject = ""
            m_strhtxtDivTopObject = ""

            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""

            'textbox
            m_strtxtPageIndex = ""
            m_strtxtPageSize = ""
            m_strtxtSearchDXM = ""
            m_strtxtSearchDXZWM = ""
            m_strtxtSearchDXLX = ""

            'datagrid
            m_intPageSize_grdObject = 0
            m_intCurrentPageIndex_grdObject = 0
            m_intSelectedIndex_grdObject = -1

            'treeview
            m_strSelectedNodeIndex_tvwServers = ""
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMXtglSjqxJs)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub













        '----------------------------------------------------------------
        ' htxtObjectQuery����
        '----------------------------------------------------------------
        Public Property htxtObjectQuery() As String
            Get
                htxtObjectQuery = m_strhtxtObjectQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtObjectQuery = Value
                Catch ex As Exception
                    m_strhtxtObjectQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtObjectRows����
        '----------------------------------------------------------------
        Public Property htxtObjectRows() As String
            Get
                htxtObjectRows = m_strhtxtObjectRows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtObjectRows = Value
                Catch ex As Exception
                    m_strhtxtObjectRows = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtObjectSort����
        '----------------------------------------------------------------
        Public Property htxtObjectSort() As String
            Get
                htxtObjectSort = m_strhtxtObjectSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtObjectSort = Value
                Catch ex As Exception
                    m_strhtxtObjectSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtObjectSortColumnIndex����
        '----------------------------------------------------------------
        Public Property htxtObjectSortColumnIndex() As String
            Get
                htxtObjectSortColumnIndex = m_strhtxtObjectSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtObjectSortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtObjectSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtObjectSortType����
        '----------------------------------------------------------------
        Public Property htxtObjectSortType() As String
            Get
                htxtObjectSortType = m_strhtxtObjectSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtObjectSortType = Value
                Catch ex As Exception
                    m_strhtxtObjectSortType = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivLeftObject����
        '----------------------------------------------------------------
        Public Property htxtDivLeftObject() As String
            Get
                htxtDivLeftObject = m_strhtxtDivLeftObject
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftObject = Value
                Catch ex As Exception
                    m_strhtxtDivLeftObject = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopObject����
        '----------------------------------------------------------------
        Public Property htxtDivTopObject() As String
            Get
                htxtDivTopObject = m_strhtxtDivTopObject
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopObject = Value
                Catch ex As Exception
                    m_strhtxtDivTopObject = ""
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
        ' txtPageIndex����
        '----------------------------------------------------------------
        Public Property txtPageIndex() As String
            Get
                txtPageIndex = m_strtxtPageIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtPageIndex = Value
                Catch ex As Exception
                    m_strtxtPageIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtPageSize����
        '----------------------------------------------------------------
        Public Property txtPageSize() As String
            Get
                txtPageSize = m_strtxtPageSize
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtPageSize = Value
                Catch ex As Exception
                    m_strtxtPageSize = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtSearchDXM����
        '----------------------------------------------------------------
        Public Property txtSearchDXM() As String
            Get
                txtSearchDXM = m_strtxtSearchDXM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSearchDXM = Value
                Catch ex As Exception
                    m_strtxtSearchDXM = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtSearchDXZWM����
        '----------------------------------------------------------------
        Public Property txtSearchDXZWM() As String
            Get
                txtSearchDXZWM = m_strtxtSearchDXZWM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSearchDXZWM = Value
                Catch ex As Exception
                    m_strtxtSearchDXZWM = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtSearchDXLX����
        '----------------------------------------------------------------
        Public Property txtSearchDXLX() As String
            Get
                txtSearchDXLX = m_strtxtSearchDXLX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSearchDXLX = Value
                Catch ex As Exception
                    m_strtxtSearchDXLX = ""
                End Try
            End Set
        End Property



        '----------------------------------------------------------------
        ' grdObjectPageSize����
        '----------------------------------------------------------------
        Public Property grdObjectPageSize() As Integer
            Get
                grdObjectPageSize = m_intPageSize_grdObject
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_grdObject = Value
                Catch ex As Exception
                    m_intPageSize_grdObject = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdObjectCurrentPageIndex����
        '----------------------------------------------------------------
        Public Property grdObjectCurrentPageIndex() As Integer
            Get
                grdObjectCurrentPageIndex = m_intCurrentPageIndex_grdObject
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intCurrentPageIndex_grdObject = Value
                Catch ex As Exception
                    m_intCurrentPageIndex_grdObject = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdObjectSelectedIndex����
        '----------------------------------------------------------------
        Public Property grdObjectSelectedIndex() As Integer
            Get
                grdObjectSelectedIndex = m_intSelectedIndex_grdObject
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_grdObject = Value
                Catch ex As Exception
                    m_intSelectedIndex_grdObject = 0
                End Try
            End Set
        End Property



        '----------------------------------------------------------------
        ' SelectedNodeIndex����
        '----------------------------------------------------------------
        Public Property SelectedNodeIndex() As String
            Get
                SelectedNodeIndex = m_strSelectedNodeIndex_tvwServers
            End Get
            Set(ByVal Value As String)
                Try
                    m_strSelectedNodeIndex_tvwServers = Value
                Catch ex As Exception
                    m_strSelectedNodeIndex_tvwServers = ""
                End Try
            End Set
        End Property

    End Class

End Namespace
