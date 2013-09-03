Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IMFlowXgwjljAdd
    '
    ' ���������� 
    '     flow_xgwjlj_add.aspxģ�鱾��ָ��ֳ���Ҫ����Ϣ
    '----------------------------------------------------------------
    <Serializable()> Public Class IMFlowXgwjljAdd
        Implements IDisposable

        '----------------------------------------------------------------
        'textbox
        '----------------------------------------------------------------
        Private m_strtxtFILEPageIndex As String                         'txtFILEPageIndex
        Private m_strtxtFILEPageSize As String                          'txtFILEPageSize

        '----------------------------------------------------------------
        'search textbox
        '----------------------------------------------------------------
        Private m_strtxtFILESearch_NDMIN As String                      'txtFILESearch_NDMIN
        Private m_strtxtFILESearch_NDMAX As String                      'txtFILESearch_NDMIN
        Private m_strtxtFILESearch_LSH As String                        'txtFILESearch_LSH
        Private m_strtxtFILESearch_WJBT As String                       'txtFILESearch_WJBT
        Private m_strtxtFILESearch_WJZH As String                       'txtFILESearch_WJZH
        Private m_strtxtFILESearch_ZBDW As String                       'txtFILESearch_ZBDW
        Private m_strhtxtSessionIdQuery As String                       'htxtSessionIdQuery

        '----------------------------------------------------------------
        'hidden textbox
        '----------------------------------------------------------------
        Private m_strhtxtFILEQuery As String                            'htxtFILEQuery
        Private m_strhtxtFILERows As String                             'htxtFILERows
        Private m_strhtxtFILESort As String                             'htxtFILESort
        Private m_strhtxtFILESortColumnIndex As String                  'htxtFILESortColumnIndex
        Private m_strhtxtFILESortType As String                         'htxtFILESortType
        Private m_strhtxtDivLeftFILE As String                          'htxtDivLeftFILE
        Private m_strhtxtDivTopFILE As String                           'htxtDivTopFILE
        Private m_strhtxtDivLeftBody As String                          'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String                           'htxtDivTopBody

        '----------------------------------------------------------------
        'grdFILE parameters
        '----------------------------------------------------------------
        Private m_intPageSize_grdFILE As Integer                        'grdFILE��ҳ��С
        Private m_intSelectedIndex_grdFILE As Integer                   'grdFILE��������
        Private m_intCurrentPageIndex_grdFILE As Integer                'grdFILE��ҳ����













        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()

            MyBase.New()

            m_strtxtFILEPageIndex = ""
            m_strtxtFILEPageSize = ""

            m_strtxtFILESearch_NDMIN = ""
            m_strtxtFILESearch_NDMAX = ""
            m_strtxtFILESearch_LSH = ""
            m_strtxtFILESearch_WJBT = ""
            m_strtxtFILESearch_WJZH = ""
            m_strtxtFILESearch_ZBDW = ""
            m_strhtxtSessionIdQuery = ""

            m_strhtxtFILEQuery = ""
            m_strhtxtFILERows = ""
            m_strhtxtFILESort = ""
            m_strhtxtFILESortColumnIndex = ""
            m_strhtxtFILESortType = ""

            m_strhtxtDivLeftFILE = ""
            m_strhtxtDivTopFILE = ""
            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""

            m_intPageSize_grdFILE = 100
            m_intSelectedIndex_grdFILE = -1
            m_intCurrentPageIndex_grdFILE = 0

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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMFlowXgwjljAdd)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub













        '----------------------------------------------------------------
        ' txtFILEPageIndex����
        '----------------------------------------------------------------
        Public Property txtFILEPageIndex() As String
            Get
                txtFILEPageIndex = m_strtxtFILEPageIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtFILEPageIndex = Value
                Catch ex As Exception
                    m_strtxtFILEPageIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtFILEPageSize����
        '----------------------------------------------------------------
        Public Property txtFILEPageSize() As String
            Get
                txtFILEPageSize = m_strtxtFILEPageSize
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtFILEPageSize = Value
                Catch ex As Exception
                    m_strtxtFILEPageSize = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' txtFILESearch_NDMIN����
        '----------------------------------------------------------------
        Public Property txtFILESearch_NDMIN() As String
            Get
                txtFILESearch_NDMIN = m_strtxtFILESearch_NDMIN
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtFILESearch_NDMIN = Value
                Catch ex As Exception
                    m_strtxtFILESearch_NDMIN = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtFILESearch_NDMAX����
        '----------------------------------------------------------------
        Public Property txtFILESearch_NDMAX() As String
            Get
                txtFILESearch_NDMAX = m_strtxtFILESearch_NDMAX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtFILESearch_NDMAX = Value
                Catch ex As Exception
                    m_strtxtFILESearch_NDMAX = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtFILESearch_LSH����
        '----------------------------------------------------------------
        Public Property txtFILESearch_LSH() As String
            Get
                txtFILESearch_LSH = m_strtxtFILESearch_LSH
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtFILESearch_LSH = Value
                Catch ex As Exception
                    m_strtxtFILESearch_LSH = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtFILESearch_WJBT����
        '----------------------------------------------------------------
        Public Property txtFILESearch_WJBT() As String
            Get
                txtFILESearch_WJBT = m_strtxtFILESearch_WJBT
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtFILESearch_WJBT = Value
                Catch ex As Exception
                    m_strtxtFILESearch_WJBT = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtFILESearch_WJZH����
        '----------------------------------------------------------------
        Public Property txtFILESearch_WJZH() As String
            Get
                txtFILESearch_WJZH = m_strtxtFILESearch_WJZH
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtFILESearch_WJZH = Value
                Catch ex As Exception
                    m_strtxtFILESearch_WJZH = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtFILESearch_ZBDW����
        '----------------------------------------------------------------
        Public Property txtFILESearch_ZBDW() As String
            Get
                txtFILESearch_ZBDW = m_strtxtFILESearch_ZBDW
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtFILESearch_ZBDW = Value
                Catch ex As Exception
                    m_strtxtFILESearch_ZBDW = ""
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
        ' htxtFILESort����
        '----------------------------------------------------------------
        Public Property htxtFILESort() As String
            Get
                htxtFILESort = m_strhtxtFILESort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtFILESort = Value
                Catch ex As Exception
                    m_strhtxtFILESort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtFILERows����
        '----------------------------------------------------------------
        Public Property htxtFILERows() As String
            Get
                htxtFILERows = m_strhtxtFILERows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtFILERows = Value
                Catch ex As Exception
                    m_strhtxtFILERows = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtFILESortColumnIndex����
        '----------------------------------------------------------------
        Public Property htxtFILESortColumnIndex() As String
            Get
                htxtFILESortColumnIndex = m_strhtxtFILESortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtFILESortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtFILESortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtFILEQuery����
        '----------------------------------------------------------------
        Public Property htxtFILEQuery() As String
            Get
                htxtFILEQuery = m_strhtxtFILEQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtFILEQuery = Value
                Catch ex As Exception
                    m_strhtxtFILEQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtFILESortType����
        '----------------------------------------------------------------
        Public Property htxtFILESortType() As String
            Get
                htxtFILESortType = m_strhtxtFILESortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtFILESortType = Value
                Catch ex As Exception
                    m_strhtxtFILESortType = ""
                End Try
            End Set
        End Property



        '----------------------------------------------------------------
        ' htxtDivLeftFILE����
        '----------------------------------------------------------------
        Public Property htxtDivLeftFILE() As String
            Get
                htxtDivLeftFILE = m_strhtxtDivLeftFILE
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftFILE = Value
                Catch ex As Exception
                    m_strhtxtDivLeftFILE = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopFILE����
        '----------------------------------------------------------------
        Public Property htxtDivTopFILE() As String
            Get
                htxtDivTopFILE = m_strhtxtDivTopFILE
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopFILE = Value
                Catch ex As Exception
                    m_strhtxtDivTopFILE = ""
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
        ' grdFILE_PageSize����
        '----------------------------------------------------------------
        Public Property grdFILE_PageSize() As Integer
            Get
                grdFILE_PageSize = m_intPageSize_grdFILE
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_grdFILE = Value
                Catch ex As Exception
                    m_intPageSize_grdFILE = 100
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdFILE_SelectedIndex����
        '----------------------------------------------------------------
        Public Property grdFILE_SelectedIndex() As Integer
            Get
                grdFILE_SelectedIndex = m_intSelectedIndex_grdFILE
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_grdFILE = Value
                Catch ex As Exception
                    m_intSelectedIndex_grdFILE = -1
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdFILE_CurrentPageIndex����
        '----------------------------------------------------------------
        Public Property grdFILE_CurrentPageIndex() As Integer
            Get
                grdFILE_CurrentPageIndex = m_intCurrentPageIndex_grdFILE
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intCurrentPageIndex_grdFILE = Value
                Catch ex As Exception
                    m_intCurrentPageIndex_grdFILE = 0
                End Try
            End Set
        End Property

    End Class

End Namespace
