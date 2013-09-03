Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IGgdmBmryBmxx
    '
    ' ���������� 
    '     ggdm_bmry_bmxx.aspxģ����ýӿڵĶ����봦��
    '----------------------------------------------------------------
    <Serializable()> Public Class IGgdmBmryBmxx
        Inherits Xydc.Platform.BusinessFacade.ICallInterface

        '----------------------------------------------------------------
        '�������
        '----------------------------------------------------------------
        Private m_objEditMode_I As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType  '�༭ģʽ
        Private m_strPrevZZDM_I As String                 '���ӡ�����ʱ���ϼ�����
        Private m_strZZDM_I As String                     '�鿴���༭������ʱ�õ���֯����

        '----------------------------------------------------------------
        '�������
        '----------------------------------------------------------------
        Private m_blnExitMode_O As Boolean                '���ط�ʽ��True-ȷ����False-ȡ��
        Private m_strZZDM_O As String                     '�������ڴ������֯����
        Private m_strZZMC_O As String                     '�������ڴ������֯����









        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            '��ʼ���������
            MyBase.iInterfaceType = ICallInterface.enumInterfaceType.InputAndOutput

            '��ʼ���������
            m_objEditMode_I = Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eSelect
            m_strZZDM_I = ""
            m_strPrevZZDM_I = ""

            '��ʼ���������
            m_blnExitMode_O = False
            m_strZZDM_O = ""
            m_strZZMC_O = ""

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
        Public Overloads Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IGgdmBmryBmxx)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub










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
        ' iPrevZZDM����
        '----------------------------------------------------------------
        Public Property iPrevZZDM() As String
            Get
                iPrevZZDM = m_strPrevZZDM_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strPrevZZDM_I = Value
                Catch ex As Exception
                    m_strPrevZZDM_I = ""
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
        ' oZZDM����
        '----------------------------------------------------------------
        Public Property oZZDM() As String
            Get
                oZZDM = m_strZZDM_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strZZDM_O = Value
                Catch ex As Exception
                    m_strZZDM_O = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' oZZMC����
        '----------------------------------------------------------------
        Public Property oZZMC() As String
            Get
                oZZMC = m_strZZMC_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strZZMC_O = Value
                Catch ex As Exception
                    m_strZZMC_O = ""
                End Try
            End Set
        End Property

    End Class

End Namespace
