Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IChatLsxx
    '
    ' ���������� 
    '     chat_lsxx.aspxģ����ýӿڵĶ����봦��
    '----------------------------------------------------------------
    <Serializable()> Public Class IChatLsxx
        Inherits Xydc.Platform.BusinessFacade.ICallInterface

        '----------------------------------------------------------------
        '�������
        '----------------------------------------------------------------
        Private m_blnDifferentFrame_I As Boolean       '����֡�뵱ǰ֡��ͬ����֡���ã�
        Private m_strUserXM_I As String                'Ҫ������û�����

        '----------------------------------------------------------------
        '�������
        '----------------------------------------------------------------
        Private m_blnExitMode_O As Boolean             '�˳���ʽ��True-ȷ����False-ȡ��









        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            '��ʼ���������
            MyBase.iInterfaceType = ICallInterface.enumInterfaceType.InputAndOutput

            '��ʼ���������
            m_blnDifferentFrame_I = False
            m_strUserXM_I = ""

            '��ʼ���������
            m_blnExitMode_O = False

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
        Public Overloads Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IChatLsxx)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub











        '----------------------------------------------------------------
        ' iDifferentFrame����
        '----------------------------------------------------------------
        Public Property iDifferentFrame() As Boolean
            Get
                iDifferentFrame = m_blnDifferentFrame_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnDifferentFrame_I = Value
                Catch ex As Exception
                    m_blnDifferentFrame_I = False
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iUserXM����
        '----------------------------------------------------------------
        Public Property iUserXM() As String
            Get
                iUserXM = m_strUserXM_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strUserXM_I = Value
                Catch ex As Exception
                    m_strUserXM_I = ""
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

    End Class

End Namespace
