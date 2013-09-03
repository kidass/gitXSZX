Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IMXtglYhglYh
    '
    ' ���������� 
    '     xtgl_yhgl_yh.aspxģ�鱾��ָ��ֳ���Ҫ����Ϣ
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
        ' ���캯��
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
        ' htxtBMRYQuery����
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
        ' htxtBMRYRows����
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
        ' htxtBMRYSort����
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
        ' htxtBMRYSortColumnIndex����
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
        ' htxtBMRYSortType����
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
        ' htxtDivLeftBMRY����
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
        ' htxtDivTopBMRY����
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
        ' txtBMRYPageIndex����
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
        ' txtBMRYPageSize����
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
        ' txtBMRYSearch_RYDM����
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
        ' txtBMRYSearch_RYMC����
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
        ' txtBMRYSearch_ZZMC����
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
        ' txtBMRYSearch_RYXHMin����
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
        ' txtBMRYSearch_RYXHMax����
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
        ' txtBMRYSearch_RYJBMC����
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
        ' txtBMRYSearch_RYDRZW����
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
        ' rblApply����
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
        ' grdBMRYPageSize����
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
        ' grdBMRYCurrentPageIndex����
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
        ' grdBMRYSelectedIndex����
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
