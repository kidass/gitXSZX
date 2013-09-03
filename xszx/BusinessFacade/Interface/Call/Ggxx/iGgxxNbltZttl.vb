Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IGgxxNbltZttl
    '
    ' ���������� 
    '     ggxx_nblt_zttl.aspxģ����ýӿڵĶ����봦��
    '----------------------------------------------------------------
    <Serializable()> Public Class IGgxxNbltZttl
        Inherits Xydc.Platform.BusinessFacade.ICallInterface

        '----------------------------------------------------------------
        '�������
        '----------------------------------------------------------------
        Private m_strJLBH_I As String                      '�������

        '----------------------------------------------------------------
        '�������
        '----------------------------------------------------------------
        Private m_blnExitMode_O As Boolean                 '���ط�ʽ��True-ȷ����False-ȡ��
        Private m_strJLBH_O As String                      '�������ڴ���Ľ������









        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            '��ʼ���������
            MyBase.iInterfaceType = ICallInterface.enumInterfaceType.InputAndOutput

            '��ʼ���������
            m_strJLBH_I = ""

            '��ʼ���������
            m_blnExitMode_O = False
            m_strJLBH_O = ""

        End Sub

        '----------------------------------------------------------------
        ' ���ظ������������
        '----------------------------------------------------------------
        Public Overloads Sub Dispose()
            MyBase.Dispose()
            Dispose(True)
        End Sub

        '----------------------------------------------------------------
        ' �ͷű�����Դ
        '----------------------------------------------------------------
        Protected Overloads Sub Dispose(ByVal disposing As Boolean)
            If (Not disposing) Then
                Exit Sub
            End If
        End Sub

        '----------------------------------------------------------------
        ' ��ȫ�ͷű�����Դ
        '----------------------------------------------------------------
        Public Overloads Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IGgxxNbltZttl)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub












        '----------------------------------------------------------------
        ' iJLBH����
        '----------------------------------------------------------------
        Public Property iJLBH() As String
            Get
                iJLBH = m_strJLBH_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strJLBH_I = Value
                Catch ex As Exception
                    m_strJLBH_I = ""
                End Try
            End Set
        End Property





        '----------------------------------------------------------------
        ' oExitMode����
        '----------------------------------------------------------------
        Public Property oExitMode() As Boolean
            Get
                oExitMode = m_blnExitMode_O
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnExitMode_O = Value
                Catch ex As Exception
                    m_blnExitMode_O = False
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' oJLBH����
        '----------------------------------------------------------------
        Public Property oJLBH() As String
            Get
                oJLBH = m_strJLBH_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strJLBH_O = Value
                Catch ex As Exception
                    m_strJLBH_O = ""
                End Try
            End Set
        End Property

    End Class

End Namespace
