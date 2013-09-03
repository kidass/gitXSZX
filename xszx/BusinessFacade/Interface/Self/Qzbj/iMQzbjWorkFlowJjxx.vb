Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IMQzbjWorkFlowJjxx
    '
    ' ���������� 
    '     qzbj_workflow_jjxx.aspxģ�鱾��ָ��ֳ���Ҫ����Ϣ
    '----------------------------------------------------------------
    <Serializable()> Public Class IMQzbjWorkFlowJjxx
        Implements IDisposable

        Private m_strhtxtDivLeftLZXX As String                    'htxtDivLeftLZXX
        Private m_strhtxtDivTopLZXX As String                     'htxtDivTopLZXX
        Private m_strhtxtDivLeftBody As String                    'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String                     'htxtDivTopBody

        Private m_strhtxtLZXXQuery As String                      'htxtLZXXQuery
        Private m_strhtxtLZXXRows As String                       'htxtLZXXRows
        Private m_strhtxtLZXXSort As String                       'htxtLZXXSort
        Private m_strhtxtLZXXSortColumnIndex As String            'htxtLZXXSortColumnIndex
        Private m_strhtxtLZXXSortType As String                   'htxtLZXXSortType

        Private m_strhtxtLZXXSessionIdQuery As String             'htxtLZXXSessionIdQuery

        Private m_strtxtLZXXPageIndex As String                  'txtLZXXPageIndex
        Private m_strtxtLZXXPageSize As String                   'txtLZXXPageSize
        Private m_strtxtLZXXSearch_FSR As String                 'txtLZXXSearch_FSR
        Private m_strtxtLZXXSearch_JSR As String                 'txtLZXXSearch_JSR
        Private m_strtxtLZXXSearch_BLSY As String                'txtLZXXSearch_BLSY
        Private m_strtxtLZXXSearch_WCRQMin As String             'txtLZXXSearch_WCRQMin
        Private m_strtxtLZXXSearch_WCRQMax As String             'txtLZXXSearch_WCRQMax

        Private m_intPageSize_grdLZXX As Integer
        Private m_intSelectedIndex_grdLZXX As Integer
        Private m_intCurrentPageIndex_grdLZXX As Integer












        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            m_strhtxtLZXXQuery = ""
            m_strhtxtLZXXRows = ""
            m_strhtxtLZXXSort = ""
            m_strhtxtLZXXSortColumnIndex = ""
            m_strhtxtLZXXSortType = ""
            m_strhtxtDivLeftLZXX = ""
            m_strhtxtDivTopLZXX = ""
            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""

            m_strhtxtLZXXSessionIdQuery = ""

            m_strtxtLZXXPageIndex = ""
            m_strtxtLZXXPageSize = ""
            m_strtxtLZXXSearch_FSR = ""
            m_strtxtLZXXSearch_JSR = ""
            m_strtxtLZXXSearch_BLSY = ""
            m_strtxtLZXXSearch_WCRQMin = ""
            m_strtxtLZXXSearch_WCRQMax = ""

            m_intPageSize_grdLZXX = 0
            m_intCurrentPageIndex_grdLZXX = 0
            m_intSelectedIndex_grdLZXX = -1
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMQzbjWorkFlowJjxx)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub











        '----------------------------------------------------------------
        ' htxtLZXXQuery����
        '----------------------------------------------------------------
        Public Property htxtLZXXQuery() As String
            Get
                htxtLZXXQuery = m_strhtxtLZXXQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtLZXXQuery = Value
                Catch ex As Exception
                    m_strhtxtLZXXQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtLZXXRows����
        '----------------------------------------------------------------
        Public Property htxtLZXXRows() As String
            Get
                htxtLZXXRows = m_strhtxtLZXXRows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtLZXXRows = Value
                Catch ex As Exception
                    m_strhtxtLZXXRows = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtLZXXSort����
        '----------------------------------------------------------------
        Public Property htxtLZXXSort() As String
            Get
                htxtLZXXSort = m_strhtxtLZXXSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtLZXXSort = Value
                Catch ex As Exception
                    m_strhtxtLZXXSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtLZXXSortColumnIndex����
        '----------------------------------------------------------------
        Public Property htxtLZXXSortColumnIndex() As String
            Get
                htxtLZXXSortColumnIndex = m_strhtxtLZXXSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtLZXXSortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtLZXXSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtLZXXSortType����
        '----------------------------------------------------------------
        Public Property htxtLZXXSortType() As String
            Get
                htxtLZXXSortType = m_strhtxtLZXXSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtLZXXSortType = Value
                Catch ex As Exception
                    m_strhtxtLZXXSortType = ""
                End Try
            End Set
        End Property













        '----------------------------------------------------------------
        ' htxtDivLeftLZXX����
        '----------------------------------------------------------------
        Public Property htxtDivLeftLZXX() As String
            Get
                htxtDivLeftLZXX = m_strhtxtDivLeftLZXX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftLZXX = Value
                Catch ex As Exception
                    m_strhtxtDivLeftLZXX = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopLZXX����
        '----------------------------------------------------------------
        Public Property htxtDivTopLZXX() As String
            Get
                htxtDivTopLZXX = m_strhtxtDivTopLZXX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopLZXX = Value
                Catch ex As Exception
                    m_strhtxtDivTopLZXX = ""
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
        ' htxtLZXXSessionIdQuery����
        '----------------------------------------------------------------
        Public Property htxtLZXXSessionIdQuery() As String
            Get
                htxtLZXXSessionIdQuery = m_strhtxtLZXXSessionIdQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtLZXXSessionIdQuery = Value
                Catch ex As Exception
                    m_strhtxtLZXXSessionIdQuery = ""
                End Try
            End Set
        End Property















        '----------------------------------------------------------------
        ' txtLZXXPageIndex����
        '----------------------------------------------------------------
        Public Property txtLZXXPageIndex() As String
            Get
                txtLZXXPageIndex = m_strtxtLZXXPageIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtLZXXPageIndex = Value
                Catch ex As Exception
                    m_strtxtLZXXPageIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtLZXXPageSize����
        '----------------------------------------------------------------
        Public Property txtLZXXPageSize() As String
            Get
                txtLZXXPageSize = m_strtxtLZXXPageSize
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtLZXXPageSize = Value
                Catch ex As Exception
                    m_strtxtLZXXPageSize = ""
                End Try
            End Set
        End Property













        '----------------------------------------------------------------
        ' txtLZXXSearch_FSR����
        '----------------------------------------------------------------
        Public Property txtLZXXSearch_FSR() As String
            Get
                txtLZXXSearch_FSR = m_strtxtLZXXSearch_FSR
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtLZXXSearch_FSR = Value
                Catch ex As Exception
                    m_strtxtLZXXSearch_FSR = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtLZXXSearch_JSR����
        '----------------------------------------------------------------
        Public Property txtLZXXSearch_JSR() As String
            Get
                txtLZXXSearch_JSR = m_strtxtLZXXSearch_JSR
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtLZXXSearch_JSR = Value
                Catch ex As Exception
                    m_strtxtLZXXSearch_JSR = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtLZXXSearch_BLSY����
        '----------------------------------------------------------------
        Public Property txtLZXXSearch_BLSY() As String
            Get
                txtLZXXSearch_BLSY = m_strtxtLZXXSearch_BLSY
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtLZXXSearch_BLSY = Value
                Catch ex As Exception
                    m_strtxtLZXXSearch_BLSY = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtLZXXSearch_WCRQMin����
        '----------------------------------------------------------------
        Public Property txtLZXXSearch_WCRQMin() As String
            Get
                txtLZXXSearch_WCRQMin = m_strtxtLZXXSearch_WCRQMin
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtLZXXSearch_WCRQMin = Value
                Catch ex As Exception
                    m_strtxtLZXXSearch_WCRQMin = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtLZXXSearch_WCRQMax����
        '----------------------------------------------------------------
        Public Property txtLZXXSearch_WCRQMax() As String
            Get
                txtLZXXSearch_WCRQMax = m_strtxtLZXXSearch_WCRQMax
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtLZXXSearch_WCRQMax = Value
                Catch ex As Exception
                    m_strtxtLZXXSearch_WCRQMax = ""
                End Try
            End Set
        End Property














        '----------------------------------------------------------------
        ' grdLZXXPageSize����
        '----------------------------------------------------------------
        Public Property grdLZXXPageSize() As Integer
            Get
                grdLZXXPageSize = m_intPageSize_grdLZXX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_grdLZXX = Value
                Catch ex As Exception
                    m_intPageSize_grdLZXX = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdLZXXCurrentPageIndex����
        '----------------------------------------------------------------
        Public Property grdLZXXCurrentPageIndex() As Integer
            Get
                grdLZXXCurrentPageIndex = m_intCurrentPageIndex_grdLZXX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intCurrentPageIndex_grdLZXX = Value
                Catch ex As Exception
                    m_intCurrentPageIndex_grdLZXX = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdLZXXSelectedIndex����
        '----------------------------------------------------------------
        Public Property grdLZXXSelectedIndex() As Integer
            Get
                grdLZXXSelectedIndex = m_intSelectedIndex_grdLZXX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_grdLZXX = Value
                Catch ex As Exception
                    m_intSelectedIndex_grdLZXX = 0
                End Try
            End Set
        End Property

    End Class

End Namespace
