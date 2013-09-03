Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IMGgxxGzzdInfo
    '
    ' ���������� 
    '     ggxx_gzzd_info.aspxģ�鱾��ָ��ֳ���Ҫ����Ϣ
    '----------------------------------------------------------------
    <Serializable()> Public Class IMGgxxGzzdInfo
        Implements IDisposable

        '----------------------------------------------------------------
        ' ģ������
        '----------------------------------------------------------------
        Private m_strtxtBT As String                        'txtBT
        Private m_strtxtFBDW As String                      'txtFBDW
        Private m_strtxtNR As String                        'txtNR
        Private m_strtxtBH As String                        'txtBH
        Private m_strtxtFBRQ As String                      'txtFBRQ
        Private m_strtxtPXH As String                       'txtPXH

        Private m_strhtxtJB As String                       'htxtJB
        Private m_strhtxtSJBH As String                     'htxtSJBH
        Private m_strhtxtWYBS As String                     'htxtWYBS

        Private m_strhtxtDivLeftBody As String              'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String               'htxtDivTopBody
        Private m_strhtxtDivLeftMain As String              'htxtDivLeftMain
        Private m_strhtxtDivTopMain As String               'htxtDivTopMain













        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            m_strtxtBT = ""
            m_strtxtFBDW = ""
            m_strtxtNR = ""
            m_strtxtBH = ""
            m_strtxtFBRQ = ""
            m_strtxtPXH = ""

            m_strhtxtJB = ""
            m_strhtxtSJBH = ""
            m_strhtxtWYBS = ""

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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMGgxxGzzdInfo)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub











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
        ' txtFBDW����
        '----------------------------------------------------------------
        Public Property txtFBDW() As String
            Get
                txtFBDW = m_strtxtFBDW
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtFBDW = Value
                Catch ex As Exception
                    m_strtxtFBDW = ""
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
        ' txtPXH����
        '----------------------------------------------------------------
        Public Property txtPXH() As String
            Get
                txtPXH = m_strtxtPXH
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtPXH = Value
                Catch ex As Exception
                    m_strtxtPXH = ""
                End Try
            End Set
        End Property






        '----------------------------------------------------------------
        ' htxtJB����
        '----------------------------------------------------------------
        Public Property htxtJB() As String
            Get
                htxtJB = m_strhtxtJB
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtJB = Value
                Catch ex As Exception
                    m_strhtxtJB = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtSJBH����
        '----------------------------------------------------------------
        Public Property htxtSJBH() As String
            Get
                htxtSJBH = m_strhtxtSJBH
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtSJBH = Value
                Catch ex As Exception
                    m_strhtxtSJBH = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtWYBS����
        '----------------------------------------------------------------
        Public Property htxtWYBS() As String
            Get
                htxtWYBS = m_strhtxtWYBS
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtWYBS = Value
                Catch ex As Exception
                    m_strhtxtWYBS = ""
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
