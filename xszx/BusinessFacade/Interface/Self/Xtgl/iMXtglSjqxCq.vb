Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IMXtglSjqxCq
    '
    ' ���������� 
    '     xtgl_sjqx_cq.aspxģ�鱾��ָ��ֳ���Ҫ����Ϣ
    '----------------------------------------------------------------
    <Serializable()> Public Class IMXtglSjqxCq
        Implements IDisposable

        '----------------------------------------------------------------
        'hidden textbox
        '----------------------------------------------------------------
        Private m_strhtxtYRQuery As String                'htxtYRQuery
        Private m_strhtxtYRRows As String                 'htxtYRRows
        Private m_strhtxtYRSort As String                 'htxtYRSort
        Private m_strhtxtYRSortColumnIndex As String      'htxtYRSortColumnIndex
        Private m_strhtxtYRSortType As String             'htxtYRSortType

        Private m_strhtxtWRQuery As String                'htxtWRQuery
        Private m_strhtxtWRRows As String                 'htxtWRRows
        Private m_strhtxtWRSort As String                 'htxtWRSort
        Private m_strhtxtWRSortColumnIndex As String      'htxtWRSortColumnIndex
        Private m_strhtxtWRSortType As String             'htxtWRSortType

        Private m_strhtxtDivLeftYR As String              'htxtDivLeftYR
        Private m_strhtxtDivTopYR As String               'htxtDivTopYR
        Private m_strhtxtDivLeftWR As String              'htxtDivLeftWR
        Private m_strhtxtDivTopWR As String               'htxtDivTopWR
        Private m_strhtxtDivLeftBody As String            'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String             'htxtDivTopBody

        '----------------------------------------------------------------
        'asp:textbox
        '----------------------------------------------------------------
        Private m_strtxtYRPageIndex As String             'txtYRPageIndex
        Private m_strtxtYRPageSize As String              'txtYRPageSize
        Private m_strtxtYRSearchRYDM As String            'txtYRSearchRYDM
        Private m_strtxtYRSearchRYMC As String            'txtYRSearchRYMC
        Private m_strtxtYRSearchZZMC As String            'txtYRSearchZZMC
        Private m_strtxtYRSearchJBMC As String            'txtYRSearchJBMC
        Private m_strtxtYRSearchGWLB As String            'txtYRSearchGWLB

        Private m_strtxtWRPageIndex As String             'txtWRPageIndex
        Private m_strtxtWRPageSize As String              'txtWRPageSize
        Private m_strtxtWRSearchRYDM As String            'txtWRSearchRYDM
        Private m_strtxtWRSearchRYMC As String            'txtWRSearchRYMC
        Private m_strtxtWRSearchZZMC As String            'txtWRSearchZZMC
        Private m_strtxtWRSearchJBMC As String            'txtWRSearchJBMC
        Private m_strtxtWRSearchGWLB As String            'txtWRSearchGWLB

        '----------------------------------------------------------------
        'asp:datagrid - grdYR
        '----------------------------------------------------------------
        Private m_intPageSize_grdYR As Integer
        Private m_intSelectedIndex_grdYR As Integer
        Private m_intCurrentPageIndex_grdYR As Integer

        Private m_intPageSize_grdWR As Integer
        Private m_intSelectedIndex_grdWR As Integer
        Private m_intCurrentPageIndex_grdWR As Integer

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
            m_strhtxtYRQuery = ""
            m_strhtxtYRRows = ""
            m_strhtxtYRSort = ""
            m_strhtxtYRSortColumnIndex = ""
            m_strhtxtYRSortType = ""

            m_strhtxtWRQuery = ""
            m_strhtxtWRRows = ""
            m_strhtxtWRSort = ""
            m_strhtxtWRSortColumnIndex = ""
            m_strhtxtWRSortType = ""

            m_strhtxtDivLeftYR = ""
            m_strhtxtDivTopYR = ""

            m_strhtxtDivLeftWR = ""
            m_strhtxtDivTopWR = ""

            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""

            'textbox
            m_strtxtYRPageIndex = ""
            m_strtxtYRPageSize = ""
            m_strtxtYRSearchRYDM = ""
            m_strtxtYRSearchRYMC = ""
            m_strtxtYRSearchZZMC = ""
            m_strtxtYRSearchJBMC = ""
            m_strtxtYRSearchGWLB = ""

            m_strtxtWRPageIndex = ""
            m_strtxtWRPageSize = ""
            m_strtxtWRSearchRYDM = ""
            m_strtxtWRSearchRYMC = ""
            m_strtxtWRSearchZZMC = ""
            m_strtxtWRSearchJBMC = ""
            m_strtxtWRSearchGWLB = ""

            'datagrid
            m_intPageSize_grdYR = 0
            m_intCurrentPageIndex_grdYR = 0
            m_intSelectedIndex_grdYR = -1

            m_intPageSize_grdWR = 0
            m_intCurrentPageIndex_grdWR = 0
            m_intSelectedIndex_grdWR = -1

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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMXtglSjqxCq)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub















        '----------------------------------------------------------------
        ' htxtYRQuery����
        '----------------------------------------------------------------
        Public Property htxtYRQuery() As String
            Get
                htxtYRQuery = m_strhtxtYRQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtYRQuery = Value
                Catch ex As Exception
                    m_strhtxtYRQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtYRRows����
        '----------------------------------------------------------------
        Public Property htxtYRRows() As String
            Get
                htxtYRRows = m_strhtxtYRRows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtYRRows = Value
                Catch ex As Exception
                    m_strhtxtYRRows = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtYRSort����
        '----------------------------------------------------------------
        Public Property htxtYRSort() As String
            Get
                htxtYRSort = m_strhtxtYRSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtYRSort = Value
                Catch ex As Exception
                    m_strhtxtYRSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtYRSortColumnIndex����
        '----------------------------------------------------------------
        Public Property htxtYRSortColumnIndex() As String
            Get
                htxtYRSortColumnIndex = m_strhtxtYRSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtYRSortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtYRSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtYRSortType����
        '----------------------------------------------------------------
        Public Property htxtYRSortType() As String
            Get
                htxtYRSortType = m_strhtxtYRSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtYRSortType = Value
                Catch ex As Exception
                    m_strhtxtYRSortType = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtWRQuery����
        '----------------------------------------------------------------
        Public Property htxtWRQuery() As String
            Get
                htxtWRQuery = m_strhtxtWRQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtWRQuery = Value
                Catch ex As Exception
                    m_strhtxtWRQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtWRRows����
        '----------------------------------------------------------------
        Public Property htxtWRRows() As String
            Get
                htxtWRRows = m_strhtxtWRRows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtWRRows = Value
                Catch ex As Exception
                    m_strhtxtWRRows = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtWRSort����
        '----------------------------------------------------------------
        Public Property htxtWRSort() As String
            Get
                htxtWRSort = m_strhtxtWRSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtWRSort = Value
                Catch ex As Exception
                    m_strhtxtWRSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtWRSortColumnIndex����
        '----------------------------------------------------------------
        Public Property htxtWRSortColumnIndex() As String
            Get
                htxtWRSortColumnIndex = m_strhtxtWRSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtWRSortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtWRSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtWRSortType����
        '----------------------------------------------------------------
        Public Property htxtWRSortType() As String
            Get
                htxtWRSortType = m_strhtxtWRSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtWRSortType = Value
                Catch ex As Exception
                    m_strhtxtWRSortType = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivLeftYR����
        '----------------------------------------------------------------
        Public Property htxtDivLeftYR() As String
            Get
                htxtDivLeftYR = m_strhtxtDivLeftYR
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftYR = Value
                Catch ex As Exception
                    m_strhtxtDivLeftYR = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopYR����
        '----------------------------------------------------------------
        Public Property htxtDivTopYR() As String
            Get
                htxtDivTopYR = m_strhtxtDivTopYR
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopYR = Value
                Catch ex As Exception
                    m_strhtxtDivTopYR = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivLeftWR����
        '----------------------------------------------------------------
        Public Property htxtDivLeftWR() As String
            Get
                htxtDivLeftWR = m_strhtxtDivLeftWR
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftWR = Value
                Catch ex As Exception
                    m_strhtxtDivLeftWR = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopWR����
        '----------------------------------------------------------------
        Public Property htxtDivTopWR() As String
            Get
                htxtDivTopWR = m_strhtxtDivTopWR
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopWR = Value
                Catch ex As Exception
                    m_strhtxtDivTopWR = ""
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
        ' txtYRPageIndex����
        '----------------------------------------------------------------
        Public Property txtYRPageIndex() As String
            Get
                txtYRPageIndex = m_strtxtYRPageIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtYRPageIndex = Value
                Catch ex As Exception
                    m_strtxtYRPageIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtYRPageSize����
        '----------------------------------------------------------------
        Public Property txtYRPageSize() As String
            Get
                txtYRPageSize = m_strtxtYRPageSize
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtYRPageSize = Value
                Catch ex As Exception
                    m_strtxtYRPageSize = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtYRSearchRYDM����
        '----------------------------------------------------------------
        Public Property txtYRSearchRYDM() As String
            Get
                txtYRSearchRYDM = m_strtxtYRSearchRYDM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtYRSearchRYDM = Value
                Catch ex As Exception
                    m_strtxtYRSearchRYDM = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtYRSearchRYMC����
        '----------------------------------------------------------------
        Public Property txtYRSearchRYMC() As String
            Get
                txtYRSearchRYMC = m_strtxtYRSearchRYMC
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtYRSearchRYMC = Value
                Catch ex As Exception
                    m_strtxtYRSearchRYMC = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtYRSearchZZMC����
        '----------------------------------------------------------------
        Public Property txtYRSearchZZMC() As String
            Get
                txtYRSearchZZMC = m_strtxtYRSearchZZMC
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtYRSearchZZMC = Value
                Catch ex As Exception
                    m_strtxtYRSearchZZMC = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtYRSearchJBMC����
        '----------------------------------------------------------------
        Public Property txtYRSearchJBMC() As String
            Get
                txtYRSearchJBMC = m_strtxtYRSearchJBMC
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtYRSearchJBMC = Value
                Catch ex As Exception
                    m_strtxtYRSearchJBMC = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtYRSearchGWLB����
        '----------------------------------------------------------------
        Public Property txtYRSearchGWLB() As String
            Get
                txtYRSearchGWLB = m_strtxtYRSearchGWLB
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtYRSearchGWLB = Value
                Catch ex As Exception
                    m_strtxtYRSearchGWLB = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtWRPageIndex����
        '----------------------------------------------------------------
        Public Property txtWRPageIndex() As String
            Get
                txtWRPageIndex = m_strtxtWRPageIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtWRPageIndex = Value
                Catch ex As Exception
                    m_strtxtWRPageIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtWRPageSize����
        '----------------------------------------------------------------
        Public Property txtWRPageSize() As String
            Get
                txtWRPageSize = m_strtxtWRPageSize
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtWRPageSize = Value
                Catch ex As Exception
                    m_strtxtWRPageSize = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtWRSearchRYDM����
        '----------------------------------------------------------------
        Public Property txtWRSearchRYDM() As String
            Get
                txtWRSearchRYDM = m_strtxtWRSearchRYDM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtWRSearchRYDM = Value
                Catch ex As Exception
                    m_strtxtWRSearchRYDM = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtWRSearchRYMC����
        '----------------------------------------------------------------
        Public Property txtWRSearchRYMC() As String
            Get
                txtWRSearchRYMC = m_strtxtWRSearchRYMC
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtWRSearchRYMC = Value
                Catch ex As Exception
                    m_strtxtWRSearchRYMC = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtWRSearchZZMC����
        '----------------------------------------------------------------
        Public Property txtWRSearchZZMC() As String
            Get
                txtWRSearchZZMC = m_strtxtWRSearchZZMC
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtWRSearchZZMC = Value
                Catch ex As Exception
                    m_strtxtWRSearchZZMC = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtWRSearchJBMC����
        '----------------------------------------------------------------
        Public Property txtWRSearchJBMC() As String
            Get
                txtWRSearchJBMC = m_strtxtWRSearchJBMC
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtWRSearchJBMC = Value
                Catch ex As Exception
                    m_strtxtWRSearchJBMC = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtWRSearchGWLB����
        '----------------------------------------------------------------
        Public Property txtWRSearchGWLB() As String
            Get
                txtWRSearchGWLB = m_strtxtWRSearchGWLB
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtWRSearchGWLB = Value
                Catch ex As Exception
                    m_strtxtWRSearchGWLB = ""
                End Try
            End Set
        End Property



        '----------------------------------------------------------------
        ' grdYRPageSize����
        '----------------------------------------------------------------
        Public Property grdYRPageSize() As Integer
            Get
                grdYRPageSize = m_intPageSize_grdYR
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_grdYR = Value
                Catch ex As Exception
                    m_intPageSize_grdYR = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdYRCurrentPageIndex����
        '----------------------------------------------------------------
        Public Property grdYRCurrentPageIndex() As Integer
            Get
                grdYRCurrentPageIndex = m_intCurrentPageIndex_grdYR
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intCurrentPageIndex_grdYR = Value
                Catch ex As Exception
                    m_intCurrentPageIndex_grdYR = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdYRSelectedIndex����
        '----------------------------------------------------------------
        Public Property grdYRSelectedIndex() As Integer
            Get
                grdYRSelectedIndex = m_intSelectedIndex_grdYR
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_grdYR = Value
                Catch ex As Exception
                    m_intSelectedIndex_grdYR = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdWRPageSize����
        '----------------------------------------------------------------
        Public Property grdWRPageSize() As Integer
            Get
                grdWRPageSize = m_intPageSize_grdWR
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_grdWR = Value
                Catch ex As Exception
                    m_intPageSize_grdWR = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdWRCurrentPageIndex����
        '----------------------------------------------------------------
        Public Property grdWRCurrentPageIndex() As Integer
            Get
                grdWRCurrentPageIndex = m_intCurrentPageIndex_grdWR
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intCurrentPageIndex_grdWR = Value
                Catch ex As Exception
                    m_intCurrentPageIndex_grdWR = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdWRSelectedIndex����
        '----------------------------------------------------------------
        Public Property grdWRSelectedIndex() As Integer
            Get
                grdWRSelectedIndex = m_intSelectedIndex_grdWR
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_grdWR = Value
                Catch ex As Exception
                    m_intSelectedIndex_grdWR = 0
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
