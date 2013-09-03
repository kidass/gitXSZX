Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IMGrswWtxlInfo
    '
    ' ���������� 
    '     grsw_tongxinlu_info.aspxģ�鱾��ָ��ֳ���Ҫ����Ϣ
    '----------------------------------------------------------------
    <Serializable()> Public Class IMGrswWtxlInfo
        Implements IDisposable

        '----------------------------------------------------------------
        ' ģ������
        '----------------------------------------------------------------
        Private m_strtxtXH As String                        'txtXH
        Private m_strtxtPX As String                        'txtPX
        Private m_strtxtXM As String                        'txtXM
        Private m_strtxtYDDH As String                      'txtYDDH
        Private m_strtxtDZYJ As String                      'txtDZYJ
        Private m_strtxtGRWY As String                      'txtGRWY
        Private m_strtxtXHJ As String                       'txtXHJ
        Private m_strtxtDWMC As String                      'txtDWMC
        Private m_strtxtDWDZ As String                      'txtDWDZ
        Private m_strtxtBM As String                        'txtBM
        Private m_strtxtZW As String                        'txtZW
        Private m_strtxtBGDH As String                      'txtBGDH
        Private m_strtxtYWCZ As String                      'txtYWCZ
        Private m_strtxtDWYB As String                      'txtDWYB
        Private m_strtxtDWZY As String                      'txtDWZY
        Private m_strtxtBGS As String                       'txtBGS
        Private m_strtxtJTDZ As String                      'txtJTDZ
        Private m_strtxtZZDH As String                      'txtZZDH
        Private m_strtxtJTYB As String                      'txtJTYB

        Private m_strhtxtSYZ As String                      'htxtSYZ

        Private m_strhtxtDivLeftBody As String              'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String               'htxtDivTopBody
        Private m_strhtxtDivLeftMain As String              'htxtDivLeftMain
        Private m_strhtxtDivTopMain As String               'htxtDivTopMain













        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            m_strtxtXH = ""
            m_strtxtPX = ""
            m_strtxtXM = ""
            m_strtxtYDDH = ""
            m_strtxtDZYJ = ""
            m_strtxtGRWY = ""
            m_strtxtXHJ = ""
            m_strtxtDWMC = ""
            m_strtxtDWDZ = ""
            m_strtxtBM = ""
            m_strtxtZW = ""
            m_strtxtBGDH = ""
            m_strtxtYWCZ = ""
            m_strtxtDWYB = ""
            m_strtxtDWZY = ""
            m_strtxtBGS = ""
            m_strtxtJTDZ = ""
            m_strtxtZZDH = ""
            m_strtxtJTYB = ""

            m_strhtxtSYZ = ""

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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMGrswWtxlInfo)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub














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
        ' txtXM����
        '----------------------------------------------------------------
        Public Property txtXM() As String
            Get
                txtXM = m_strtxtXM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtXM = Value
                Catch ex As Exception
                    m_strtxtXM = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtDZYJ����
        '----------------------------------------------------------------
        Public Property txtDZYJ() As String
            Get
                txtDZYJ = m_strtxtDZYJ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtDZYJ = Value
                Catch ex As Exception
                    m_strtxtDZYJ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtYDDH����
        '----------------------------------------------------------------
        Public Property txtYDDH() As String
            Get
                txtYDDH = m_strtxtYDDH
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtYDDH = Value
                Catch ex As Exception
                    m_strtxtYDDH = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtGRWY����
        '----------------------------------------------------------------
        Public Property txtGRWY() As String
            Get
                txtGRWY = m_strtxtGRWY
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtGRWY = Value
                Catch ex As Exception
                    m_strtxtGRWY = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtXHJ����
        '----------------------------------------------------------------
        Public Property txtXHJ() As String
            Get
                txtXHJ = m_strtxtXHJ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtXHJ = Value
                Catch ex As Exception
                    m_strtxtXHJ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtDWMC����
        '----------------------------------------------------------------
        Public Property txtDWMC() As String
            Get
                txtDWMC = m_strtxtDWMC
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtDWMC = Value
                Catch ex As Exception
                    m_strtxtDWMC = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtDWDZ����
        '----------------------------------------------------------------
        Public Property txtDWDZ() As String
            Get
                txtDWDZ = m_strtxtDWDZ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtDWDZ = Value
                Catch ex As Exception
                    m_strtxtDWDZ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtBM����
        '----------------------------------------------------------------
        Public Property txtBM() As String
            Get
                txtBM = m_strtxtBM
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtBM = Value
                Catch ex As Exception
                    m_strtxtBM = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtZW����
        '----------------------------------------------------------------
        Public Property txtZW() As String
            Get
                txtZW = m_strtxtZW
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtZW = Value
                Catch ex As Exception
                    m_strtxtZW = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtBGDH����
        '----------------------------------------------------------------
        Public Property txtBGDH() As String
            Get
                txtBGDH = m_strtxtBGDH
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtBGDH = Value
                Catch ex As Exception
                    m_strtxtBGDH = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtYWCZ����
        '----------------------------------------------------------------
        Public Property txtYWCZ() As String
            Get
                txtYWCZ = m_strtxtYWCZ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtYWCZ = Value
                Catch ex As Exception
                    m_strtxtYWCZ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtDWYB����
        '----------------------------------------------------------------
        Public Property txtDWYB() As String
            Get
                txtDWYB = m_strtxtDWYB
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtDWYB = Value
                Catch ex As Exception
                    m_strtxtDWYB = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtDWZY����
        '----------------------------------------------------------------
        Public Property txtDWZY() As String
            Get
                txtDWZY = m_strtxtDWZY
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtDWZY = Value
                Catch ex As Exception
                    m_strtxtDWZY = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtBGS����
        '----------------------------------------------------------------
        Public Property txtBGS() As String
            Get
                txtBGS = m_strtxtBGS
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtBGS = Value
                Catch ex As Exception
                    m_strtxtBGS = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtJTDZ����
        '----------------------------------------------------------------
        Public Property txtJTDZ() As String
            Get
                txtJTDZ = m_strtxtJTDZ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtJTDZ = Value
                Catch ex As Exception
                    m_strtxtJTDZ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtZZDH����
        '----------------------------------------------------------------
        Public Property txtZZDH() As String
            Get
                txtZZDH = m_strtxtZZDH
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtZZDH = Value
                Catch ex As Exception
                    m_strtxtZZDH = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtJTYB����
        '----------------------------------------------------------------
        Public Property txtJTYB() As String
            Get
                txtJTYB = m_strtxtJTYB
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtJTYB = Value
                Catch ex As Exception
                    m_strtxtJTYB = ""
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
