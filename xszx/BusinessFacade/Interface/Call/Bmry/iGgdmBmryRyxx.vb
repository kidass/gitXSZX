Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IGgdmBmryRyxx
    '
    ' ���������� 
    '     ggdm_bmry_ryxx.aspxģ����ýӿڵĶ����봦��
    '----------------------------------------------------------------
    <Serializable()> Public Class IGgdmBmryRyxx
        Inherits Xydc.Platform.BusinessFacade.ICallInterface

        '----------------------------------------------------------------
        '�������
        '----------------------------------------------------------------
        Private m_objEditMode_I As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType  '�༭ģʽ
        Private m_strRYDM_I As String                     '�鿴���༭������ʱ�õ���Ա����
        Private m_strZZDM_I As String                     '���ӡ�����ʱ����֯����

        Private m_intExitMode_I As Integer                '1-��Ա��Ϣ��2-�޸����룬3-��ɫ��4-��Χ


        '----------------------------------------------------------------
        '�������
        '----------------------------------------------------------------
        Private m_blnExitMode_O As Boolean                '���ط�ʽ��True-ȷ����False-ȡ��
        Private m_strRYDM_O As String                     '�������ڴ������Ա����
        Private m_strRYMC_O As String                     '�������ڴ������Ա����

        Private m_intExitMode_0 As Integer                '1-��Ա��Ϣ��2-�޸����룬3-��ɫ��4-��Χ










        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            '��ʼ���������
            MyBase.iInterfaceType = ICallInterface.enumInterfaceType.InputAndOutput

            '��ʼ���������
            m_objEditMode_I = Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eSelect

            m_intExitMode_I = 0

            m_strRYDM_I = ""
            m_strZZDM_I = ""

            '��ʼ���������
            m_blnExitMode_O = False

            m_intExitMode_0 = 0

            m_strRYDM_O = ""
            m_strRYMC_O = ""

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
            '�ͷ���Դ
        End Sub

        '----------------------------------------------------------------
        ' ��ȫ�ͷű�����Դ
        '----------------------------------------------------------------
        Public Overloads Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IGgdmBmryRyxx)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub







        '----------------------------------------------------------------
        ' iIntEditMode����

        '----------------------------------------------------------------
        Public Property iIntEditMode() As Integer
            Get
                iIntEditMode = m_intExitMode_I
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intExitMode_I = Value
                Catch ex As Exception
                    m_intExitMode_I = 0
                End Try
            End Set
        End Property


        '----------------------------------------------------------------
        ' iEditMode����
        '----------------------------------------------------------------
        Public Property iEditMode() As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType
            Get
                iEditMode = m_objEditMode_I
            End Get
            Set(ByVal Value As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType)
                Try
                    m_objEditMode_I = Value
                Catch ex As Exception
                    m_objEditMode_I = Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eSelect
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iRYDM����
        '----------------------------------------------------------------
        Public Property iRYDM() As String
            Get
                iRYDM = m_strRYDM_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strRYDM_I = Value
                Catch ex As Exception
                    m_strRYDM_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iZZDM����
        '----------------------------------------------------------------
        Public Property iZZDM() As String
            Get
                iZZDM = m_strZZDM_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strZZDM_I = Value
                Catch ex As Exception
                    m_strZZDM_I = ""
                End Try
            End Set
        End Property


        '----------------------------------------------------------------
        ' oIntExitMode����

        '----------------------------------------------------------------
        Public Property oIntExitMode() As Integer
            Get
                oIntExitMode = m_intExitMode_0
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intExitMode_0 = Value
                Catch ex As Exception
                    m_intExitMode_0 = 0
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
        ' oRYDM����
        '----------------------------------------------------------------
        Public Property oRYDM() As String
            Get
                oRYDM = m_strRYDM_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strRYDM_O = Value
                Catch ex As Exception
                    m_strRYDM_O = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' oRYMC����
        '----------------------------------------------------------------
        Public Property oRYMC() As String
            Get
                oRYMC = m_strRYMC_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strRYMC_O = Value
                Catch ex As Exception
                    m_strRYMC_O = ""
                End Try
            End Set
        End Property

    End Class

End Namespace
