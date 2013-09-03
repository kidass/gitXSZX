Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IMGgdmBmryRyxx
    '
    ' ���������� 
    '     ggdm_bmry_bmry.aspxģ�鱾��ָ��ֳ���Ҫ����Ϣ
    '----------------------------------------------------------------
    <Serializable()> Public Class IMGgdmBmryRyxx
        Implements IDisposable

        '----------------------------------------------------------------
        ' ģ������
        '----------------------------------------------------------------
        Private m_strtxtRYDM As String                    'txtRYDM
        Private m_strtxtRYMC As String                    'txtRYMC
        Private m_strtxtZZMC As String                    'txtZZMC
        Private m_strhtxtZZDM As String                   'htxtZZDM
        Private m_strtxtRYXH As String                    'txtRYXH
        Private m_strtxtJBMC As String                    'txtJBMC
        Private m_strhtxtJBDM As String                   'htxtJBDM
        Private m_strtxtMSMC As String                    'txtMSMC
        Private m_strhtxtMSDM As String                   'htxtMSDM
        Private m_strtxtLXDH As String                    'txtLXDH
        Private m_strtxtSJHM As String                    'txtSJHM
        Private m_strtxtFTPDZ As String                   'txtFTPDZ
        Private m_strtxtYXDZ As String                    'txtYXDZ
        Private m_strchkZDQS As String                    'chkZDQS
        Private m_strtxtKZSRY As String                   'txtKZSRY
        Private m_strtxtQTYZS As String                   'txtQTYZS
        Private m_strhtxtQTYZS As String                  'htxtQTYZS
        Private m_strtxtKCKXM As String                   'txtKCKXM
        Private m_strtxtJJXSMC As String                  'txtJJXSMC
        Private m_objcblDRZW As System.Data.DataSet       'cblDRZW

        Private m_strtxtRYZM As String                    'txtRYZM



        Private m_htxtBH As String                      'htxtBH
        Private m_htxtTASKQuery As String               'htxtTASKQuery
        Private m_htxtTASKRows As String                'htxtTASKRows
        Private m_htxtTASKSort As String                'htxtTASKSort
        Private m_htxtTASKSortColumnIndex As String     'htxtTASKSortColumnIndex
        Private m_htxtTASKSortType As String            'htxtTASKSortType

        Private m_htxtDivLeftTASK As String             'htxtDivLeftTASK
        Private m_htxtDivTopTASK As String              'htxtDivTopTASK

        '----------------------------------------------------------------
        'asp:datagrid - grdRY
        '----------------------------------------------------------------
        Private m_grdRY_PageSize As Integer
        Private m_grdRY_SelectedIndex As Integer
        Private m_grdRY_CurrentPageIndex As Integer



        Private m_strhtxtDivLeftBody As String            'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String             'htxtDivTopBody
        Private m_strhtxtDivLeftMain As String            'htxtDivLeftMain
        Private m_strhtxtDivTopMain As String             'htxtDivTopMain












        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            m_strtxtRYDM = ""
            m_strtxtRYMC = ""
            m_strtxtZZMC = ""
            m_strhtxtZZDM = ""
            m_strtxtRYXH = ""
            m_strtxtJBMC = ""
            m_strhtxtJBDM = ""
            m_strtxtMSMC = ""
            m_strhtxtMSDM = ""
            m_strtxtLXDH = ""
            m_strtxtSJHM = ""
            m_strtxtFTPDZ = ""
            m_strtxtYXDZ = ""
            m_strchkZDQS = ""
            m_strtxtKZSRY = ""
            m_strtxtQTYZS = ""
            m_strhtxtQTYZS = ""
            m_strtxtKCKXM = ""
            m_strtxtJJXSMC = ""
            m_objcblDRZW = Nothing

            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""
            m_strhtxtDivLeftMain = ""
            m_strhtxtDivTopMain = ""


            m_strtxtRYZM = ""



            m_htxtBH = ""
            m_htxtTASKQuery = ""
            m_htxtTASKRows = ""
            m_htxtTASKSort = ""
            m_htxtTASKSortColumnIndex = ""
            m_htxtTASKSortType = ""

            m_htxtDivLeftTASK = ""
            m_htxtDivTopTASK = ""

            m_grdRY_PageSize = 0
            m_grdRY_SelectedIndex = 0
            m_grdRY_CurrentPageIndex = -1


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
            If Not (m_objcblDRZW Is Nothing) Then
                m_objcblDRZW.Dispose()
                m_objcblDRZW = Nothing
            End If
        End Sub

        '----------------------------------------------------------------
        ' ��ȫ�ͷű�����Դ
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMGgdmBmryRyxx)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub









        '----------------------------------------------------------------
        ' htxtBH����
        '----------------------------------------------------------------
        Public Property htxtBH() As String
            Get
                htxtBH = m_htxtBH
            End Get
            Set(ByVal Value As String)
                Try
                    m_htxtBH = Value
                Catch ex As Exception
                    m_htxtBH = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtTASKQuery����
        '----------------------------------------------------------------
        Public Property htxtTASKQuery() As String
            Get
                htxtTASKQuery = m_htxtTASKQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_htxtTASKQuery = Value
                Catch ex As Exception
                    m_htxtTASKQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtTASKRows����
        '----------------------------------------------------------------
        Public Property htxtTASKRows() As String
            Get
                htxtTASKRows = m_htxtTASKRows
            End Get
            Set(ByVal Value As String)
                Try
                    m_htxtTASKRows = Value
                Catch ex As Exception
                    m_htxtTASKRows = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtTASKSort����
        '----------------------------------------------------------------
        Public Property htxtTASKSort() As String
            Get
                htxtTASKSort = m_htxtTASKSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_htxtTASKSort = Value
                Catch ex As Exception
                    m_htxtTASKSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtTASKSortColumnIndex����
        '----------------------------------------------------------------
        Public Property htxtTASKSortColumnIndex() As String
            Get
                htxtTASKSortColumnIndex = m_htxtTASKSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_htxtTASKSortColumnIndex = Value
                Catch ex As Exception
                    m_htxtTASKSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtTASKSortType����
        '----------------------------------------------------------------
        Public Property htxtTASKSortType() As String
            Get
                htxtTASKSortType = m_htxtTASKSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_htxtTASKSortType = Value
                Catch ex As Exception
                    m_htxtTASKSortType = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' htxtDivLeftTASK����
        '----------------------------------------------------------------
        Public Property htxtDivLeftTASK() As String
            Get
                htxtDivLeftTASK = m_htxtDivLeftTASK
            End Get
            Set(ByVal Value As String)
                Try
                    m_htxtDivLeftTASK = Value
                Catch ex As Exception
                    m_htxtDivLeftTASK = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopTASK����
        '----------------------------------------------------------------
        Public Property htxtDivTopTASK() As String
            Get
                htxtDivTopTASK = m_htxtDivTopTASK
            End Get
            Set(ByVal Value As String)
                Try
                    m_htxtDivTopTASK = Value
                Catch ex As Exception
                    m_htxtDivTopTASK = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdRY_PageSize����
        '----------------------------------------------------------------
        Public Property grdRY_PageSize() As Integer
            Get
                grdRY_PageSize = m_grdRY_PageSize
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_grdRY_PageSize = Value
                Catch ex As Exception
                    m_grdRY_PageSize = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdRY_SelectedIndex����
        '----------------------------------------------------------------
        Public Property grdRY_SelectedIndex() As Integer
            Get
                grdRY_SelectedIndex = m_grdRY_SelectedIndex
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_grdRY_SelectedIndex = Value
                Catch ex As Exception
                    m_grdRY_SelectedIndex = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdRY_CurrentPageIndex����
        '----------------------------------------------------------------
        Public Property grdRY_CurrentPageIndex() As Integer
            Get
                grdRY_CurrentPageIndex = m_grdRY_CurrentPageIndex
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_grdRY_CurrentPageIndex = Value
                Catch ex As Exception
                    m_grdRY_CurrentPageIndex = -1
                End Try
            End Set
        End Property



        '----------------------------------------------------------------
        ' txtRYDM����
        '----------------------------------------------------------------
        Public Property txtRYDM() As String
            Get
                txtRYDM = m_strtxtRYDM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtRYDM = Value
                Catch ex As Exception
                    m_strtxtRYDM = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtRYMC����
        '----------------------------------------------------------------
        Public Property txtRYMC() As String
            Get
                txtRYMC = m_strtxtRYMC
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtRYMC = Value
                Catch ex As Exception
                    m_strtxtRYMC = ""
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
        ' txtRYXH����
        '----------------------------------------------------------------
        Public Property txtRYXH() As String
            Get
                txtRYXH = m_strtxtRYXH
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtRYXH = Value
                Catch ex As Exception
                    m_strtxtRYXH = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtJBMC����
        '----------------------------------------------------------------
        Public Property txtJBMC() As String
            Get
                txtJBMC = m_strtxtJBMC
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtJBMC = Value
                Catch ex As Exception
                    m_strtxtJBMC = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtJBDM����
        '----------------------------------------------------------------
        Public Property htxtJBDM() As String
            Get
                htxtJBDM = m_strhtxtJBDM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtJBDM = Value
                Catch ex As Exception
                    m_strhtxtJBDM = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtMSMC����
        '----------------------------------------------------------------
        Public Property txtMSMC() As String
            Get
                txtMSMC = m_strtxtMSMC
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtMSMC = Value
                Catch ex As Exception
                    m_strtxtMSMC = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtMSDM����
        '----------------------------------------------------------------
        Public Property htxtMSDM() As String
            Get
                htxtMSDM = m_strhtxtMSDM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtMSDM = Value
                Catch ex As Exception
                    m_strhtxtMSDM = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtLXDH����
        '----------------------------------------------------------------
        Public Property txtLXDH() As String
            Get
                txtLXDH = m_strtxtLXDH
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtLXDH = Value
                Catch ex As Exception
                    m_strtxtLXDH = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtSJHM����
        '----------------------------------------------------------------
        Public Property txtSJHM() As String
            Get
                txtSJHM = m_strtxtSJHM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSJHM = Value
                Catch ex As Exception
                    m_strtxtSJHM = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtFTPDZ����
        '----------------------------------------------------------------
        Public Property txtFTPDZ() As String
            Get
                txtFTPDZ = m_strtxtFTPDZ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtFTPDZ = Value
                Catch ex As Exception
                    m_strtxtFTPDZ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtYXDZ����
        '----------------------------------------------------------------
        Public Property txtYXDZ() As String
            Get
                txtYXDZ = m_strtxtYXDZ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtYXDZ = Value
                Catch ex As Exception
                    m_strtxtYXDZ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' chkZDQS����
        '----------------------------------------------------------------
        Public Property chkZDQS() As String
            Get
                chkZDQS = m_strchkZDQS
            End Get
            Set(ByVal Value As String)
                Try
                    m_strchkZDQS = Value
                Catch ex As Exception
                    m_strchkZDQS = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtKZSRY����
        '----------------------------------------------------------------
        Public Property txtKZSRY() As String
            Get
                txtKZSRY = m_strtxtKZSRY
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtKZSRY = Value
                Catch ex As Exception
                    m_strtxtKZSRY = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtQTYZS����
        '----------------------------------------------------------------
        Public Property txtQTYZS() As String
            Get
                txtQTYZS = m_strtxtQTYZS
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtQTYZS = Value
                Catch ex As Exception
                    m_strtxtQTYZS = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtQTYZS����
        '----------------------------------------------------------------
        Public Property htxtQTYZS() As String
            Get
                htxtQTYZS = m_strhtxtQTYZS
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtQTYZS = Value
                Catch ex As Exception
                    m_strhtxtQTYZS = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtKCKXM����
        '----------------------------------------------------------------
        Public Property txtKCKXM() As String
            Get
                txtKCKXM = m_strtxtKCKXM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtKCKXM = Value
                Catch ex As Exception
                    m_strtxtKCKXM = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtJJXSMC����
        '----------------------------------------------------------------
        Public Property txtJJXSMC() As String
            Get
                txtJJXSMC = m_strtxtJJXSMC
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtJJXSMC = Value
                Catch ex As Exception
                    m_strtxtJJXSMC = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' cblDRZW����
        '----------------------------------------------------------------
        Public Property cblDRZW() As System.Data.DataSet
            Get
                cblDRZW = m_objcblDRZW
            End Get
            Set(ByVal Value As System.Data.DataSet)
                Try
                    m_objcblDRZW = Value
                Catch ex As Exception
                    m_objcblDRZW = Nothing
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
        ' txtRYZM����
        '----------------------------------------------------------------
        Public Property txtRYZM() As String
            Get
                txtRYZM = m_strtxtRYZM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtRYZM = Value
                Catch ex As Exception
                    m_strtxtRYZM = ""
                End Try
            End Set
        End Property


    End Class

End Namespace
