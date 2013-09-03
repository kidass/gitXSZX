Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IMGgxxSjapInfo
    '
    ' ���������� 
    '     ggxx_sjap_info.aspxģ�鱾��ָ��ֳ���Ҫ����Ϣ
    '----------------------------------------------------------------
    <Serializable()> Public Class IMGgxxSjapInfo
        Implements IDisposable

        '----------------------------------------------------------------
        ' ģ������
        '----------------------------------------------------------------
        Private m_strtxtKSRQ As String                      'txtKSRQ
        Private m_strtxtJSRQ As String                      'txtJSRQ
        Private m_strtxtXH As String                        'txtXH
        Private m_strtxtPX As String                        'txtPX
        Private m_strtxtRY As String                        'txtRY       
        Private m_strtxtDD As String                        'txtDD
        Private m_strtxtDH As String                        'txtDH
        Private m_strtxtBZ As String                        'txtBZ
        Private m_strtxtSY As String                        'txtSY
        Private m_strtxtDJR As String                       'txtDJR

        Private m_strhtxtAPSJ As String                     'htxtAPSJ

        Private m_intSelectedIndex_rblSJLX As Integer       'rblSJLX

        Private m_strhtxtDivLeftBody As String              'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String               'htxtDivTopBody
        Private m_strhtxtDivLeftMain As String              'htxtDivLeftMain
        Private m_strhtxtDivTopMain As String               'htxtDivTopMain













        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            m_strtxtKSRQ = ""
            m_strtxtJSRQ = ""
            m_strtxtXH = ""
            m_strtxtPX = ""
            m_strtxtRY = ""
            m_strtxtDH = ""
            m_strtxtSY = ""
            m_strtxtDD = ""
            m_strtxtDJR = ""
            m_strtxtBZ = ""

            m_strhtxtAPSJ = ""

            m_intSelectedIndex_rblSJLX = -1

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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMGgxxSjapInfo)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub















        '----------------------------------------------------------------
        ' txtKSRQ����
        '----------------------------------------------------------------
        Public Property txtKSRQ() As String
            Get
                txtKSRQ = m_strtxtKSRQ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtKSRQ = Value
                Catch ex As Exception
                    m_strtxtKSRQ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtJSRQ����
        '----------------------------------------------------------------
        Public Property txtJSRQ() As String
            Get
                txtJSRQ = m_strtxtJSRQ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtJSRQ = Value
                Catch ex As Exception
                    m_strtxtJSRQ = ""
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
        ' txtDH����
        '----------------------------------------------------------------
        Public Property txtDH() As String
            Get
                txtDH = m_strtxtDH
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtDH = Value
                Catch ex As Exception
                    m_strtxtDH = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtSY����
        '----------------------------------------------------------------
        Public Property txtSY() As String
            Get
                txtSY = m_strtxtSY
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtSY = Value
                Catch ex As Exception
                    m_strtxtSY = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtDJR����
        '----------------------------------------------------------------
        Public Property txtDJR() As String
            Get
                txtDJR = m_strtxtDJR
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtDJR = Value
                Catch ex As Exception
                    m_strtxtDJR = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtBZ����
        '----------------------------------------------------------------
        Public Property txtBZ() As String
            Get
                txtBZ = m_strtxtBZ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtBZ = Value
                Catch ex As Exception
                    m_strtxtBZ = ""
                End Try
            End Set
        End Property





        '----------------------------------------------------------------
        ' htxtAPSJ����
        '----------------------------------------------------------------
        Public Property htxtAPSJ() As String
            Get
                htxtAPSJ = m_strhtxtAPSJ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtAPSJ = Value
                Catch ex As Exception
                    m_strhtxtAPSJ = ""
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
        ' rblSJLX_SelectedIndex����
        '----------------------------------------------------------------
        Public Property rblSJLX_SelectedIndex() As Integer
            Get
                rblSJLX_SelectedIndex = m_intSelectedIndex_rblSJLX
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_rblSJLX = Value
                Catch ex As Exception
                    m_intSelectedIndex_rblSJLX = -1
                End Try
            End Set
        End Property

    End Class

End Namespace

