Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IMGrswRcapInfo
    '
    ' ���������� 
    '     grsw_rcap_info.aspxģ�鱾��ָ��ֳ���Ҫ����Ϣ
    '----------------------------------------------------------------
    <Serializable()> Public Class IMGrswRcapInfo
        Implements IDisposable

        '----------------------------------------------------------------
        ' ģ������
        '----------------------------------------------------------------
        Private m_strtxtZT As String                        'txtZT
        Private m_strtxtKSSJ As String                      'txtKSSJ
        Private m_strtxtJSSJ As String                      'txtJSSJ
        Private m_strtxtXS As String                        'txtXS
        Private m_strtxtFZ As String                        'txtFZ
        Private m_strtxtDD As String                        'txtDD
        Private m_strtxtRY As String                        'txtRY
        Private m_strtxtNR As String                        'txtNR
        Private m_strtxtBH As String                        'txtBH
        Private m_strtxtPX As String                        'txtPX

        Private m_strhtxtSYZ As String                      'htxtSYZ

        Private m_intSelectedIndex_rblJJ As Integer         'rblJJ_SelectedIndex
        Private m_intSelectedIndex_rblWC As Integer         'rblWC_SelectedIndex
        Private m_intSelectedIndex_rblTX As Integer         'rblTX_SelectedIndex

        Private m_strhtxtDivLeftBody As String              'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String               'htxtDivTopBody
        Private m_strhtxtDivLeftMain As String              'htxtDivLeftMain
        Private m_strhtxtDivTopMain As String               'htxtDivTopMain












        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            m_strtxtZT = ""
            m_strtxtKSSJ = ""
            m_strtxtJSSJ = ""
            m_strtxtXS = ""
            m_strtxtFZ = ""
            m_strtxtDD = ""
            m_strtxtRY = ""
            m_strtxtNR = ""
            m_strtxtBH = ""
            m_strtxtPX = ""

            m_strhtxtSYZ = ""

            m_intSelectedIndex_rblJJ = 0
            m_intSelectedIndex_rblWC = 0
            m_intSelectedIndex_rblTX = 0

            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""
            m_strhtxtDivLeftMain = ""
            m_strhtxtDivTopMain = ""

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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMGrswRcapInfo)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub












        '----------------------------------------------------------------
        ' txtZT����
        '----------------------------------------------------------------
        Public Property txtZT() As String
            Get
                txtZT = m_strtxtZT
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtZT = Value
                Catch ex As Exception
                    m_strtxtZT = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtJSSJ����
        '----------------------------------------------------------------
        Public Property txtJSSJ() As String
            Get
                txtJSSJ = m_strtxtJSSJ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtJSSJ = Value
                Catch ex As Exception
                    m_strtxtJSSJ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtKSSJ����
        '----------------------------------------------------------------
        Public Property txtKSSJ() As String
            Get
                txtKSSJ = m_strtxtKSSJ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtKSSJ = Value
                Catch ex As Exception
                    m_strtxtKSSJ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtXS����
        '----------------------------------------------------------------
        Public Property txtXS() As String
            Get
                txtXS = m_strtxtXS
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtXS = Value
                Catch ex As Exception
                    m_strtxtXS = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtFZ����
        '----------------------------------------------------------------
        Public Property txtFZ() As String
            Get
                txtFZ = m_strtxtFZ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtFZ = Value
                Catch ex As Exception
                    m_strtxtFZ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtDD����
        '----------------------------------------------------------------
        Public Property txtDD() As String
            Get
                txtDD = m_strtxtDD
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtDD = Value
                Catch ex As Exception
                    m_strtxtDD = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtRY����
        '----------------------------------------------------------------
        Public Property txtRY() As String
            Get
                txtRY = m_strtxtRY
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtRY = Value
                Catch ex As Exception
                    m_strtxtRY = ""
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
        ' txtBH����
        '----------------------------------------------------------------
        Public Property txtBH() As String
            Get
                txtBH = m_strtxtBH
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtBH = Value
                Catch ex As Exception
                    m_strtxtBH = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtPX����
        '----------------------------------------------------------------
        Public Property txtPX() As String
            Get
                txtPX = m_strtxtPX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtPX = Value
                Catch ex As Exception
                    m_strtxtPX = ""
                End Try
            End Set
        End Property






        '----------------------------------------------------------------
        ' htxtSYZ����
        '----------------------------------------------------------------
        Public Property htxtSYZ() As String
            Get
                htxtSYZ = m_strhtxtSYZ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtSYZ = Value
                Catch ex As Exception
                    m_strhtxtSYZ = ""
                End Try
            End Set
        End Property








        '----------------------------------------------------------------
        ' rblJJ_SelectedIndex����
        '----------------------------------------------------------------
        Public Property rblJJ_SelectedIndex() As Integer
            Get
                rblJJ_SelectedIndex = m_intSelectedIndex_rblJJ
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_rblJJ = Value
                Catch ex As Exception
                    m_intSelectedIndex_rblJJ = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' rblWC_SelectedIndex����
        '----------------------------------------------------------------
        Public Property rblWC_SelectedIndex() As Integer
            Get
                rblWC_SelectedIndex = m_intSelectedIndex_rblWC
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_rblWC = Value
                Catch ex As Exception
                    m_intSelectedIndex_rblWC = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' rblTX_SelectedIndex����
        '----------------------------------------------------------------
        Public Property rblTX_SelectedIndex() As Integer
            Get
                rblTX_SelectedIndex = m_intSelectedIndex_rblTX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_rblTX = Value
                Catch ex As Exception
                    m_intSelectedIndex_rblTX = 0
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

    End Class

End Namespace
