Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IMXtglRzglZxyh
    '
    ' ���������� 
    '     xtgl_rzgl_zxyh.aspxģ�鱾��ָ��ֳ���Ҫ����Ϣ
    '----------------------------------------------------------------
    <Serializable()> Public Class IMXtglRzglZxyh
        Implements IDisposable

        '----------------------------------------------------------------
        'hidden textbox
        '----------------------------------------------------------------
        Private m_strhtxtZXYHQuery As String                      'htxtZXYHQuery
        Private m_strhtxtZXYHRows As String                       'htxtZXYHRows
        Private m_strhtxtZXYHSort As String                       'htxtZXYHSort
        Private m_strhtxtZXYHSortColumnIndex As String            'htxtZXYHSortColumnIndex
        Private m_strhtxtZXYHSortType As String                   'htxtZXYHSortType
        Private m_strhtxtDivLeftZXYH As String                    'htxtDivLeftZXYH
        Private m_strhtxtDivTopZXYH As String                     'htxtDivTopZXYH
        Private m_strhtxtDivLeftBody As String                    'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String                     'htxtDivTopBody

        Private m_strhtxtSessionIdQuery As String                 'htxtSessionIdQuery

        '----------------------------------------------------------------
        'asp:textbox
        '----------------------------------------------------------------
        Private m_strtxtZXYHPageIndex As String                  'txtZXYHPageIndex
        Private m_strtxtZXYHPageSize As String                   'txtZXYHPageSize
        Private m_strtxtZXYHSearch_YHBS As String                'txtZXYHSearch_YHBS
        Private m_strtxtZXYHSearch_YHMC As String                'txtZXYHSearch_YHMC

        '----------------------------------------------------------------
        'asp:datagrid - grdZXYH
        '----------------------------------------------------------------
        Private m_intPageSize_grdZXYH As Integer
        Private m_intSelectedIndex_grdZXYH As Integer
        Private m_intCurrentPageIndex_grdZXYH As Integer












        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            'hidden
            m_strhtxtZXYHQuery = ""
            m_strhtxtZXYHRows = ""
            m_strhtxtZXYHSort = ""
            m_strhtxtZXYHSortColumnIndex = ""
            m_strhtxtZXYHSortType = ""
            m_strhtxtDivLeftZXYH = ""
            m_strhtxtDivTopZXYH = ""
            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""

            m_strhtxtSessionIdQuery = ""

            'textbox
            m_strtxtZXYHPageIndex = ""
            m_strtxtZXYHPageSize = ""
            m_strtxtZXYHSearch_YHBS = ""
            m_strtxtZXYHSearch_YHMC = ""

            'datagrid
            m_intPageSize_grdZXYH = 0
            m_intCurrentPageIndex_grdZXYH = 0
            m_intSelectedIndex_grdZXYH = -1

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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMXtglRzglZxyh)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub











        '----------------------------------------------------------------
        ' htxtZXYHQuery����
        '----------------------------------------------------------------
        Public Property htxtZXYHQuery() As String
            Get
                htxtZXYHQuery = m_strhtxtZXYHQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtZXYHQuery = Value
                Catch ex As Exception
                    m_strhtxtZXYHQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtZXYHRows����
        '----------------------------------------------------------------
        Public Property htxtZXYHRows() As String
            Get
                htxtZXYHRows = m_strhtxtZXYHRows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtZXYHRows = Value
                Catch ex As Exception
                    m_strhtxtZXYHRows = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtZXYHSort����
        '----------------------------------------------------------------
        Public Property htxtZXYHSort() As String
            Get
                htxtZXYHSort = m_strhtxtZXYHSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtZXYHSort = Value
                Catch ex As Exception
                    m_strhtxtZXYHSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtZXYHSortColumnIndex����
        '----------------------------------------------------------------
        Public Property htxtZXYHSortColumnIndex() As String
            Get
                htxtZXYHSortColumnIndex = m_strhtxtZXYHSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtZXYHSortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtZXYHSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtZXYHSortType����
        '----------------------------------------------------------------
        Public Property htxtZXYHSortType() As String
            Get
                htxtZXYHSortType = m_strhtxtZXYHSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtZXYHSortType = Value
                Catch ex As Exception
                    m_strhtxtZXYHSortType = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' htxtDivLeftZXYH����
        '----------------------------------------------------------------
        Public Property htxtDivLeftZXYH() As String
            Get
                htxtDivLeftZXYH = m_strhtxtDivLeftZXYH
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftZXYH = Value
                Catch ex As Exception
                    m_strhtxtDivLeftZXYH = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopZXYH����
        '----------------------------------------------------------------
        Public Property htxtDivTopZXYH() As String
            Get
                htxtDivTopZXYH = m_strhtxtDivTopZXYH
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopZXYH = Value
                Catch ex As Exception
                    m_strhtxtDivTopZXYH = ""
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
        ' txtZXYHPageIndex����
        '----------------------------------------------------------------
        Public Property txtZXYHPageIndex() As String
            Get
                txtZXYHPageIndex = m_strtxtZXYHPageIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtZXYHPageIndex = Value
                Catch ex As Exception
                    m_strtxtZXYHPageIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtZXYHPageSize����
        '----------------------------------------------------------------
        Public Property txtZXYHPageSize() As String
            Get
                txtZXYHPageSize = m_strtxtZXYHPageSize
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtZXYHPageSize = Value
                Catch ex As Exception
                    m_strtxtZXYHPageSize = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' txtZXYHSearch_YHBS����
        '----------------------------------------------------------------
        Public Property txtZXYHSearch_YHBS() As String
            Get
                txtZXYHSearch_YHBS = m_strtxtZXYHSearch_YHBS
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtZXYHSearch_YHBS = Value
                Catch ex As Exception
                    m_strtxtZXYHSearch_YHBS = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtZXYHSearch_YHMC����
        '----------------------------------------------------------------
        Public Property txtZXYHSearch_YHMC() As String
            Get
                txtZXYHSearch_YHMC = m_strtxtZXYHSearch_YHMC
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtZXYHSearch_YHMC = Value
                Catch ex As Exception
                    m_strtxtZXYHSearch_YHMC = ""
                End Try
            End Set
        End Property





        '----------------------------------------------------------------
        ' grdZXYHPageSize����
        '----------------------------------------------------------------
        Public Property grdZXYHPageSize() As Integer
            Get
                grdZXYHPageSize = m_intPageSize_grdZXYH
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_grdZXYH = Value
                Catch ex As Exception
                    m_intPageSize_grdZXYH = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdZXYHCurrentPageIndex����
        '----------------------------------------------------------------
        Public Property grdZXYHCurrentPageIndex() As Integer
            Get
                grdZXYHCurrentPageIndex = m_intCurrentPageIndex_grdZXYH
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intCurrentPageIndex_grdZXYH = Value
                Catch ex As Exception
                    m_intCurrentPageIndex_grdZXYH = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdZXYHSelectedIndex����
        '----------------------------------------------------------------
        Public Property grdZXYHSelectedIndex() As Integer
            Get
                grdZXYHSelectedIndex = m_intSelectedIndex_grdZXYH
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_grdZXYH = Value
                Catch ex As Exception
                    m_intSelectedIndex_grdZXYH = -1
                End Try
            End Set
        End Property

    End Class

End Namespace
