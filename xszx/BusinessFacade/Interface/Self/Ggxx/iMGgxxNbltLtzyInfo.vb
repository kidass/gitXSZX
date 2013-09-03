Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IMGgxxNbltLtzyInfo
    '
    ' ���������� 
    '     ggxx_nblt_ltzy_info.aspxģ�鱾��ָ��ֳ���Ҫ����Ϣ
    '----------------------------------------------------------------
    <Serializable()> Public Class IMGgxxNbltLtzyInfo
        Implements IDisposable

        '----------------------------------------------------------------
        ' ģ������
        '----------------------------------------------------------------
        Private m_strtxtJLZT As String                      'txtJLZT
        Private m_strtxtRYNC As String                      'txtRYNC
        Private m_strtxtJLNR As String                      'txtJLNR
        Private m_strtxtJLBH As String                      'txtJLBH
        Private m_strtxtFBRQ As String                      'txtFBRQ

        Private m_strhtxtJLJB As String                     'htxtJLJB
        Private m_strhtxtSJBH As String                     'htxtSJBH
        Private m_strhtxtRYDM As String                     'htxtRYDM

        Private m_strhtxtDivLeftBody As String              'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String               'htxtDivTopBody
        Private m_strhtxtDivLeftMain As String              'htxtDivLeftMain
        Private m_strhtxtDivTopMain As String               'htxtDivTopMain










        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            m_strtxtJLZT = ""
            m_strtxtRYNC = ""
            m_strtxtJLNR = ""
            m_strtxtJLBH = ""
            m_strtxtFBRQ = ""

            m_strhtxtJLJB = ""
            m_strhtxtSJBH = ""
            m_strhtxtRYDM = ""

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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMGgxxNbltLtzyInfo)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub












        '----------------------------------------------------------------
        ' txtJLZT����
        '----------------------------------------------------------------
        Public Property txtJLZT() As String
            Get
                txtJLZT = m_strtxtJLZT
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtJLZT = Value
                Catch ex As Exception
                    m_strtxtJLZT = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtRYNC����
        '----------------------------------------------------------------
        Public Property txtRYNC() As String
            Get
                txtRYNC = m_strtxtRYNC
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtRYNC = Value
                Catch ex As Exception
                    m_strtxtRYNC = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtJLNR����
        '----------------------------------------------------------------
        Public Property txtJLNR() As String
            Get
                txtJLNR = m_strtxtJLNR
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtJLNR = Value
                Catch ex As Exception
                    m_strtxtJLNR = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtJLBH����
        '----------------------------------------------------------------
        Public Property txtJLBH() As String
            Get
                txtJLBH = m_strtxtJLBH
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtJLBH = Value
                Catch ex As Exception
                    m_strtxtJLBH = ""
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
        ' htxtJLJB����
        '----------------------------------------------------------------
        Public Property htxtJLJB() As String
            Get
                htxtJLJB = m_strhtxtJLJB
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtJLJB = Value
                Catch ex As Exception
                    m_strhtxtJLJB = ""
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

    End Class

End Namespace
