Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IMGgxxDzggFabuInfo
    '
    ' ���������� 
    '     ggxx_dzgg_fabu_info.aspxģ�鱾��ָ��ֳ���Ҫ����Ϣ
    '----------------------------------------------------------------
    <Serializable()> Public Class IMGgxxDzggFabuInfo
        Implements IDisposable

        '----------------------------------------------------------------
        ' ģ������
        '----------------------------------------------------------------
        Private m_strtxtZZMC As String                      'txtZZMC
        Private m_strtxtBT As String                        'txtBT
        Private m_strtxtCZY As String                       'txtCZY
        Private m_strtxtNR As String                        'txtNR
        Private m_strtxtXH As String                        'txtXH
        Private m_strtxtBLRQ As String                      'txtBLRQ
        Private m_strtxtFBRQ As String                      'txtFBRQ
        Private m_strtxtYDRY As String                      'txtYDRY

        Private m_strhtxtWJBS As String                     'htxtWJBS
        Private m_strhtxtZZDM As String                     'htxtZZDM
        Private m_strhtxtCZYDM As String                    'htxtCZYDM

        Private m_intSelectedIndex_rblYDBS As Integer       'rblYDBS
        Private m_intSelectedIndex_rblFBBS As Integer       'rblFBBS
        Private m_intSelectedIndex_rblFBXZ As Integer       'rblFBXZ   


        Private m_objDataSet_FJ As Xydc.Platform.Common.Data.ggxxDianzigonggaoData
        Private m_strhtxtSessionIDFJ As String            'htxtSessionIDFJ
        Private m_strhtxtFJSort As String                 'htxtFJSort
        Private m_strhtxtFJSortColumnIndex As String      'htxtFJSortColumnIndex
        Private m_strhtxtFJSortType As String             'htxtFJSortType
        Private m_intPageSize_grdFJ As Integer            'grdFJ_PageSize
        Private m_intSelectedIndex_grdFJ As Integer       'grdFJ_SelectedIndex
        Private m_intCurrentPageIndex_grdFJ As Integer    'grdFJ_CurrentPageIndex
        Private m_strhtxtDivLeftFJ As String                'htxtDivLeftFJ
        Private m_strhtxtDivTopFJ As String                 'htxtDivTopFJ


        Private m_strhtxtDivLeftBody As String              'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String               'htxtDivTopBody        
        Private m_strhtxtDivLeftMain As String              'htxtDivLeftMain
        Private m_strhtxtDivTopMain As String               'htxtDivTopMain










        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            m_strtxtZZMC = ""
            m_strtxtBT = ""
            m_strtxtCZY = ""
            m_strtxtNR = ""
            m_strtxtXH = ""
            m_strtxtBLRQ = ""
            m_strtxtFBRQ = ""
            m_strtxtYDRY = ""

            m_strhtxtWJBS = ""
            m_strhtxtZZDM = ""
            m_strhtxtCZYDM = ""

            m_intSelectedIndex_rblYDBS = -1
            m_intSelectedIndex_rblFBBS = -1
            m_intSelectedIndex_rblFBXZ = -1

            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""
            m_strhtxtDivLeftMain = ""
            m_strhtxtDivTopMain = ""


            m_strhtxtSessionIDFJ = ""
            m_strhtxtDivLeftFJ = ""
            m_strhtxtDivTopFJ = ""
            m_strhtxtFJSort = ""
            m_strhtxtFJSortColumnIndex = ""
            m_strhtxtFJSortType = ""
            m_intSelectedIndex_grdFJ = -1
            m_intPageSize_grdFJ = 100
            m_intCurrentPageIndex_grdFJ = 0

            m_objDataSet_FJ = Nothing


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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMGgxxDzggFabuInfo)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub






        '----------------------------------------------------------------
        ' htxtSessionIDFJ����
        '----------------------------------------------------------------
        Public Property htxtSessionIDFJ() As String
            Get
                htxtSessionIDFJ = m_strhtxtSessionIDFJ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtSessionIDFJ = Value
                Catch ex As Exception
                    m_strhtxtSessionIDFJ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtFJSort����
        '----------------------------------------------------------------
        Public Property htxtFJSort() As String
            Get
                htxtFJSort = m_strhtxtFJSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtFJSort = Value
                Catch ex As Exception
                    m_strhtxtFJSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtFJSortColumnIndex����
        '----------------------------------------------------------------
        Public Property htxtFJSortColumnIndex() As String
            Get
                htxtFJSortColumnIndex = m_strhtxtFJSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtFJSortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtFJSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtFJSortType����
        '----------------------------------------------------------------
        Public Property htxtFJSortType() As String
            Get
                htxtFJSortType = m_strhtxtFJSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtFJSortType = Value
                Catch ex As Exception
                    m_strhtxtFJSortType = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdFJ_SelectedIndex����
        '----------------------------------------------------------------
        Public Property grdFJ_SelectedIndex() As Integer
            Get
                grdFJ_SelectedIndex = m_intSelectedIndex_grdFJ
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_grdFJ = Value
                Catch ex As Exception
                    m_intSelectedIndex_grdFJ = -1
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdFJ_PageSize����
        '----------------------------------------------------------------
        Public Property grdFJ_PageSize() As Integer
            Get
                grdFJ_PageSize = m_intPageSize_grdFJ
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_grdFJ = Value
                Catch ex As Exception
                    m_intPageSize_grdFJ = -1
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdFJ_CurrentPageIndex����
        '----------------------------------------------------------------
        Public Property grdFJ_CurrentPageIndex() As Integer
            Get
                grdFJ_CurrentPageIndex = m_intCurrentPageIndex_grdFJ
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intCurrentPageIndex_grdFJ = Value
                Catch ex As Exception
                    m_intCurrentPageIndex_grdFJ = -1
                End Try
            End Set
        End Property





        '----------------------------------------------------------------
        ' txtZZMC����
        '----------------------------------------------------------------
        Public Property txtZZMC() As String
            Get
                txtZZMC = m_strtxtZZMC
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtZZMC = Value
                Catch ex As Exception
                    m_strtxtZZMC = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtBT����
        '----------------------------------------------------------------
        Public Property txtBT() As String
            Get
                txtBT = m_strtxtBT
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtBT = Value
                Catch ex As Exception
                    m_strtxtBT = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtCZY����
        '----------------------------------------------------------------
        Public Property txtCZY() As String
            Get
                txtCZY = m_strtxtCZY
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtCZY = Value
                Catch ex As Exception
                    m_strtxtCZY = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtNR����
        '----------------------------------------------------------------
        Public Property txtNR() As String
            Get
                txtNR = m_strtxtNR
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtNR = Value
                Catch ex As Exception
                    m_strtxtNR = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtXH����
        '----------------------------------------------------------------
        Public Property txtXH() As String
            Get
                txtXH = m_strtxtXH
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtXH = Value
                Catch ex As Exception
                    m_strtxtXH = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtBLRQ����
        '----------------------------------------------------------------
        Public Property txtBLRQ() As String
            Get
                txtBLRQ = m_strtxtBLRQ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtBLRQ = Value
                Catch ex As Exception
                    m_strtxtBLRQ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtFBRQ����
        '----------------------------------------------------------------
        Public Property txtFBRQ() As String
            Get
                txtFBRQ = m_strtxtFBRQ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtFBRQ = Value
                Catch ex As Exception
                    m_strtxtFBRQ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtYDRY����
        '----------------------------------------------------------------
        Public Property txtYDRY() As String
            Get
                txtYDRY = m_strtxtYDRY
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtYDRY = Value
                Catch ex As Exception
                    m_strtxtYDRY = ""
                End Try
            End Set
        End Property





        '----------------------------------------------------------------
        ' htxtWJBS����
        '----------------------------------------------------------------
        Public Property htxtWJBS() As String
            Get
                htxtWJBS = m_strhtxtWJBS
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtWJBS = Value
                Catch ex As Exception
                    m_strhtxtWJBS = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtZZDM����
        '----------------------------------------------------------------
        Public Property htxtZZDM() As String
            Get
                htxtZZDM = m_strhtxtZZDM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtZZDM = Value
                Catch ex As Exception
                    m_strhtxtZZDM = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtCZYDM����
        '----------------------------------------------------------------
        Public Property htxtCZYDM() As String
            Get
                htxtCZYDM = m_strhtxtCZYDM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtCZYDM = Value
                Catch ex As Exception
                    m_strhtxtCZYDM = ""
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
        ' htxtDivLeftMain����
        '----------------------------------------------------------------
        Public Property htxtDivLeftMain() As String
            Get
                htxtDivLeftMain = m_strhtxtDivLeftMain
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftMain = Value
                Catch ex As Exception
                    m_strhtxtDivLeftMain = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopMain����
        '----------------------------------------------------------------
        Public Property htxtDivTopMain() As String
            Get
                htxtDivTopMain = m_strhtxtDivTopMain
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopMain = Value
                Catch ex As Exception
                    m_strhtxtDivTopMain = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivLeftFJ����
        '----------------------------------------------------------------
        Public Property htxtDivLeftFJ() As String
            Get
                htxtDivLeftFJ = m_strhtxtDivLeftFJ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftFJ = Value
                Catch ex As Exception
                    m_strhtxtDivLeftFJ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopFJ����
        '----------------------------------------------------------------
        Public Property htxtDivTopFJ() As String
            Get
                htxtDivTopFJ = m_strhtxtDivTopFJ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopFJ = Value
                Catch ex As Exception
                    m_strhtxtDivTopFJ = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' rblYDBS_SelectedIndex����
        '----------------------------------------------------------------
        Public Property rblYDBS_SelectedIndex() As Integer
            Get
                rblYDBS_SelectedIndex = m_intSelectedIndex_rblYDBS
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_rblYDBS = Value
                Catch ex As Exception
                    m_intSelectedIndex_rblYDBS = -1
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' rblFBBS_SelectedIndex����
        '----------------------------------------------------------------
        Public Property rblFBBS_SelectedIndex() As Integer
            Get
                rblFBBS_SelectedIndex = m_intSelectedIndex_rblFBBS
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_rblFBBS = Value
                Catch ex As Exception
                    m_intSelectedIndex_rblFBBS = -1
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' rblFBXZ_SelectedIndex����
        '----------------------------------------------------------------
        Public Property rblFBXZ_SelectedIndex() As Integer
            Get
                rblFBXZ_SelectedIndex = m_intSelectedIndex_rblFBXZ
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_rblFBXZ = Value
                Catch ex As Exception
                    m_intSelectedIndex_rblFBXZ = -1
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' objDataSet_FJ����
        '----------------------------------------------------------------
        Public Property objDataSet_FJ() As Xydc.Platform.Common.Data.ggxxDianzigonggaoData
            Get
                objDataSet_FJ = m_objDataSet_FJ
            End Get
            Set(ByVal Value As Xydc.Platform.Common.Data.ggxxDianzigonggaoData)
                Try
                    m_objDataSet_FJ = Value
                Catch ex As Exception
                    m_objDataSet_FJ = Nothing
                End Try
            End Set
        End Property

    End Class

End Namespace
