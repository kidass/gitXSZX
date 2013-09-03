Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IMGgxxGgzyFabuInfo
    '
    ' ���������� 
    '     ggxx_ggzy_fabu_info.aspxģ�鱾��ָ��ֳ���Ҫ����Ϣ
    '----------------------------------------------------------------
    <Serializable()> Public Class IMGgxxGgzyFabuInfo
        Implements IDisposable

        '----------------------------------------------------------------
        ' ģ������
        '----------------------------------------------------------------
        Private m_strtxtLMMC As String                      'txtLMMC
        Private m_strtxtZZMC As String                      'txtZZMC
        Private m_strtxtBT As String                        'txtBT
        Private m_strtxtRYMC As String                      'txtRYMC
        Private m_strtxtNR As String                        'txtNR
        Private m_strtxtXH As String                        'txtXH
        Private m_strtxtBLRQ As String                      'txtBLRQ
        Private m_strtxtFBRQ As String                      'txtFBRQ
        Private m_strtxtFBFW As String                      'txtFBFW

        Private m_strhtxtZYBS As String                     'htxtZYBS
        Private m_strhtxtLMBS As String                     'htxtLMBS
        Private m_strhtxtZZDM As String                     'htxtZZDM
        Private m_strhtxtRYDM As String                     'htxtRYDM

        Private m_intSelectedIndex_rblNRLX As Integer       'rblNRLX
        Private m_intSelectedIndex_rblYDBS As Integer       'rblYDBS
        Private m_intSelectedIndex_rblFBBS As Integer       'rblFBBS
        Private m_intSelectedIndex_rblFBXZ As Integer       'rblFBXZ

        Private m_strhtxtDivLeftBody As String              'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String               'htxtDivTopBody
        Private m_strhtxtDivLeftMain As String              'htxtDivLeftMain
        Private m_strhtxtDivTopMain As String               'htxtDivTopMain
        Private m_strhtxtDisplayFile As String              'htxtDisplayFile
        Private m_strhtxtUploadFile As String               'htxtUploadFile










        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            m_strtxtLMMC = ""
            m_strtxtZZMC = ""
            m_strtxtBT = ""
            m_strtxtRYMC = ""
            m_strtxtNR = ""
            m_strtxtXH = ""
            m_strtxtBLRQ = ""
            m_strtxtFBRQ = ""
            m_strtxtFBFW = ""

            m_strhtxtZYBS = ""
            m_strhtxtLMBS = ""
            m_strhtxtZZDM = ""
            m_strhtxtRYDM = ""

            m_intSelectedIndex_rblNRLX = -1
            m_intSelectedIndex_rblYDBS = -1
            m_intSelectedIndex_rblFBBS = -1
            m_intSelectedIndex_rblFBXZ = -1

            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""
            m_strhtxtDivLeftMain = ""
            m_strhtxtDivTopMain = ""
            m_strhtxtDisplayFile = ""
            m_strhtxtUploadFile = ""

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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMGgxxGgzyFabuInfo)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub












        '----------------------------------------------------------------
        ' txtLMMC����
        '----------------------------------------------------------------
        Public Property txtLMMC() As String
            Get
                txtLMMC = m_strtxtLMMC
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtLMMC = Value
                Catch ex As Exception
                    m_strtxtLMMC = ""
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
        ' txtFBFW����
        '----------------------------------------------------------------
        Public Property txtFBFW() As String
            Get
                txtFBFW = m_strtxtFBFW
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtFBFW = Value
                Catch ex As Exception
                    m_strtxtFBFW = ""
                End Try
            End Set
        End Property





        '----------------------------------------------------------------
        ' htxtZYBS����
        '----------------------------------------------------------------
        Public Property htxtZYBS() As String
            Get
                htxtZYBS = m_strhtxtZYBS
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtZYBS = Value
                Catch ex As Exception
                    m_strhtxtZYBS = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtLMBS����
        '----------------------------------------------------------------
        Public Property htxtLMBS() As String
            Get
                htxtLMBS = m_strhtxtLMBS
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtLMBS = Value
                Catch ex As Exception
                    m_strhtxtLMBS = ""
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
        ' htxtRYDM����
        '----------------------------------------------------------------
        Public Property htxtRYDM() As String
            Get
                htxtRYDM = m_strhtxtRYDM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtRYDM = Value
                Catch ex As Exception
                    m_strhtxtRYDM = ""
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
        ' htxtDisplayFile����
        '----------------------------------------------------------------
        Public Property htxtDisplayFile() As String
            Get
                htxtDisplayFile = m_strhtxtDisplayFile
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDisplayFile = Value
                Catch ex As Exception
                    m_strhtxtDisplayFile = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtUploadFile����
        '----------------------------------------------------------------
        Public Property htxtUploadFile() As String
            Get
                htxtUploadFile = m_strhtxtUploadFile
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtUploadFile = Value
                Catch ex As Exception
                    m_strhtxtUploadFile = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' rblNRLX_SelectedIndex����
        '----------------------------------------------------------------
        Public Property rblNRLX_SelectedIndex() As Integer
            Get
                rblNRLX_SelectedIndex = m_intSelectedIndex_rblNRLX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_rblNRLX = Value
                Catch ex As Exception
                    m_intSelectedIndex_rblNRLX = -1
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

    End Class

End Namespace
