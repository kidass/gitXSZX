Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IMGgxxNbltLtzy
    '
    ' ���������� 
    '     ggxx_nblt_ltzy.aspxģ�鱾��ָ��ֳ���Ҫ����Ϣ
    '----------------------------------------------------------------
    <Serializable()> Public Class IMGgxxNbltLtzy
        Implements IDisposable

        '----------------------------------------------------------------
        'hidden textbox
        '----------------------------------------------------------------
        Private m_strhtxtDivLeftLTZY As String                    'htxtDivLeftLTZY
        Private m_strhtxtDivTopLTZY As String                     'htxtDivTopLTZY
        Private m_strhtxtDivLeftBody As String                    'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String                     'htxtDivTopBody

        Private m_strhtxtSessionIdQuery As String                 'htxtSessionIdQuery
        Private m_strhtxtLTZYQuery As String                      'htxtLTZYQuery
        Private m_strhtxtLTZYRows As String                       'htxtLTZYRows
        Private m_strhtxtLTZYSort As String                       'htxtLTZYSort
        Private m_strhtxtLTZYSortColumnIndex As String            'htxtLTZYSortColumnIndex
        Private m_strhtxtLTZYSortType As String                   'htxtLTZYSortType

        Private m_strhtxtPageCount As String                      'htxtPageCount
        Private m_strhtxtPageSize As String                       'htxtPageSize
        Private m_strhtxtCurrentPageIndex As String               'htxtCurrentPageIndex

        '----------------------------------------------------------------
        'asp:textbox
        '----------------------------------------------------------------
        Private m_strtxtLTZYPageIndex As String                  'txtLTZYPageIndex
        Private m_strtxtLTZYPageSize As String                   'txtLTZYPageSize
        Private m_strtxtLTZYSearch_RYDM As String                'txtLTZYSearch_RYDM
        Private m_strtxtLTZYSearch_RYNC As String                'txtLTZYSearch_RYNC
        Private m_strtxtLTZYSearch_LTZT As String                'txtLTZYSearch_LTZT
        Private m_strtxtLTZYSearch_FBRQMin As String             'txtLTZYSearch_FBRQMin
        Private m_strtxtLTZYSearch_FBRQMax As String             'txtLTZYSearch_FBRQMax











        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            'hidden
            m_strhtxtDivLeftLTZY = ""
            m_strhtxtDivTopLTZY = ""
            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""

            m_strhtxtSessionIdQuery = ""
            m_strhtxtLTZYQuery = ""
            m_strhtxtLTZYRows = "0"
            m_strhtxtLTZYSort = ""
            m_strhtxtLTZYSortColumnIndex = ""
            m_strhtxtLTZYSortType = ""

            m_strhtxtPageCount = "1"
            m_strhtxtPageSize = "0"
            m_strhtxtCurrentPageIndex = "-1"

            'textbox
            m_strtxtLTZYPageIndex = ""
            m_strtxtLTZYPageSize = ""
            m_strtxtLTZYSearch_RYDM = ""
            m_strtxtLTZYSearch_RYNC = ""
            m_strtxtLTZYSearch_LTZT = ""
            m_strtxtLTZYSearch_FBRQMin = ""
            m_strtxtLTZYSearch_FBRQMax = ""

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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMGgxxNbltLtzy)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub














        '----------------------------------------------------------------
        ' htxtDivLeftLTZY����
        '----------------------------------------------------------------
        Public Property htxtDivLeftLTZY() As String
            Get
                htxtDivLeftLTZY = m_strhtxtDivLeftLTZY
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftLTZY = Value
                Catch ex As Exception
                    m_strhtxtDivLeftLTZY = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopLTZY����
        '----------------------------------------------------------------
        Public Property htxtDivTopLTZY() As String
            Get
                htxtDivTopLTZY = m_strhtxtDivTopLTZY
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopLTZY = Value
                Catch ex As Exception
                    m_strhtxtDivTopLTZY = ""
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
        ' htxtLTZYQuery����
        '----------------------------------------------------------------
        Public Property htxtLTZYQuery() As String
            Get
                htxtLTZYQuery = m_strhtxtLTZYQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtLTZYQuery = Value
                Catch ex As Exception
                    m_strhtxtLTZYQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtLTZYRows����
        '----------------------------------------------------------------
        Public Property htxtLTZYRows() As String
            Get
                htxtLTZYRows = m_strhtxtLTZYRows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtLTZYRows = Value
                Catch ex As Exception
                    m_strhtxtLTZYRows = "0"
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtLTZYSort����
        '----------------------------------------------------------------
        Public Property htxtLTZYSort() As String
            Get
                htxtLTZYSort = m_strhtxtLTZYSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtLTZYSort = Value
                Catch ex As Exception
                    m_strhtxtLTZYSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtLTZYSortColumnIndex����
        '----------------------------------------------------------------
        Public Property htxtLTZYSortColumnIndex() As String
            Get
                htxtLTZYSortColumnIndex = m_strhtxtLTZYSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtLTZYSortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtLTZYSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtLTZYSortType����
        '----------------------------------------------------------------
        Public Property htxtLTZYSortType() As String
            Get
                htxtLTZYSortType = m_strhtxtLTZYSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtLTZYSortType = Value
                Catch ex As Exception
                    m_strhtxtLTZYSortType = ""
                End Try
            End Set
        End Property





        '----------------------------------------------------------------
        ' htxtCurrentPageIndex����
        '----------------------------------------------------------------
        Public Property htxtCurrentPageIndex() As String
            Get
                htxtCurrentPageIndex = m_strhtxtCurrentPageIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtCurrentPageIndex = Value
                Catch ex As Exception
                    m_strhtxtCurrentPageIndex = "-1"
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtPageSize����
        '----------------------------------------------------------------
        Public Property htxtPageSize() As String
            Get
                htxtPageSize = m_strhtxtPageSize
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtPageSize = Value
                Catch ex As Exception
                    m_strhtxtPageSize = "0"
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtPageCount����
        '----------------------------------------------------------------
        Public Property htxtPageCount() As String
            Get
                htxtPageCount = m_strhtxtPageCount
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtPageCount = Value
                Catch ex As Exception
                    m_strhtxtPageCount = "1"
                End Try
            End Set
        End Property






        '----------------------------------------------------------------
        ' txtLTZYPageIndex����
        '----------------------------------------------------------------
        Public Property txtLTZYPageIndex() As String
            Get
                txtLTZYPageIndex = m_strtxtLTZYPageIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtLTZYPageIndex = Value
                Catch ex As Exception
                    m_strtxtLTZYPageIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtLTZYPageSize����
        '----------------------------------------------------------------
        Public Property txtLTZYPageSize() As String
            Get
                txtLTZYPageSize = m_strtxtLTZYPageSize
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtLTZYPageSize = Value
                Catch ex As Exception
                    m_strtxtLTZYPageSize = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' txtLTZYSearch_RYDM����
        '----------------------------------------------------------------
        Public Property txtLTZYSearch_RYDM() As String
            Get
                txtLTZYSearch_RYDM = m_strtxtLTZYSearch_RYDM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtLTZYSearch_RYDM = Value
                Catch ex As Exception
                    m_strtxtLTZYSearch_RYDM = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtLTZYSearch_RYNC����
        '----------------------------------------------------------------
        Public Property txtLTZYSearch_RYNC() As String
            Get
                txtLTZYSearch_RYNC = m_strtxtLTZYSearch_RYNC
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtLTZYSearch_RYNC = Value
                Catch ex As Exception
                    m_strtxtLTZYSearch_RYNC = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtLTZYSearch_LTZT����
        '----------------------------------------------------------------
        Public Property txtLTZYSearch_LTZT() As String
            Get
                txtLTZYSearch_LTZT = m_strtxtLTZYSearch_LTZT
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtLTZYSearch_LTZT = Value
                Catch ex As Exception
                    m_strtxtLTZYSearch_LTZT = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtLTZYSearch_FBRQMin����
        '----------------------------------------------------------------
        Public Property txtLTZYSearch_FBRQMin() As String
            Get
                txtLTZYSearch_FBRQMin = m_strtxtLTZYSearch_FBRQMin
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtLTZYSearch_FBRQMin = Value
                Catch ex As Exception
                    m_strtxtLTZYSearch_FBRQMin = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtLTZYSearch_FBRQMax����
        '----------------------------------------------------------------
        Public Property txtLTZYSearch_FBRQMax() As String
            Get
                txtLTZYSearch_FBRQMax = m_strtxtLTZYSearch_FBRQMax
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtLTZYSearch_FBRQMax = Value
                Catch ex As Exception
                    m_strtxtLTZYSearch_FBRQMax = ""
                End Try
            End Set
        End Property

    End Class

End Namespace
