Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IGgxxGgzyZylmInfo
    '
    ' ���������� 
    '     ggxx_ggzy_zylm_info.aspx���ýӿڵĶ����봦��
    '----------------------------------------------------------------
    <Serializable()> Public Class IGgxxGgzyZylmInfo
        Inherits Xydc.Platform.BusinessFacade.ICallInterface

        '----------------------------------------------------------------
        '�������
        '----------------------------------------------------------------
        Private m_objEditMode_I As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType  '�༭ģʽ
        Private m_intLMBS_I As Integer                                                       '�鿴���༭������ʱ�õ���Ŀ��ʶ
        Private m_strLMDM_I As String                                                        '�鿴���༭������ʱ�õ���Ŀ����
        Private m_strSJDM_I As String                                                        '���ӡ������õ��ϼ�����

        '----------------------------------------------------------------
        '�������
        '----------------------------------------------------------------
        Private m_blnExitMode_O As Boolean                '���ط�ʽ��True-ȷ����False-ȡ��
        Private m_intLMBS_O As Integer                    '�������ڴ������Ŀ��ʶ
        Private m_strLMDM_O As String                     '�������ڴ������Ŀ����
        Private m_strSJDM_O As String                     '�������ڴ�����ϼ�����









        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            '��ʼ���������
            MyBase.iInterfaceType = ICallInterface.enumInterfaceType.InputAndOutput

            '��ʼ���������
            m_objEditMode_I = Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eSelect
            m_intLMBS_I = 0
            m_strLMDM_I = ""
            m_strSJDM_I = ""

            '��ʼ���������
            m_blnExitMode_O = False
            m_intLMBS_O = 0
            m_strLMDM_O = ""
            m_strSJDM_O = ""

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
        Public Overloads Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IGgxxGgzyZylmInfo)
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
        ' iLMBS����
        '----------------------------------------------------------------
        Public Property iLMBS() As Integer
            Get
                iLMBS = m_intLMBS_I
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intLMBS_I = Value
                Catch ex As Exception
                    m_intLMBS_I = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iLMDM����
        '----------------------------------------------------------------
        Public Property iLMDM() As String
            Get
                iLMDM = m_strLMDM_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strLMDM_I = Value
                Catch ex As Exception
                    m_strLMDM_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iSJDM����
        '----------------------------------------------------------------
        Public Property iSJDM() As String
            Get
                iSJDM = m_strSJDM_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strSJDM_I = Value
                Catch ex As Exception
                    m_strSJDM_I = ""
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
        ' oLMBS����
        '----------------------------------------------------------------
        Public Property oLMBS() As Integer
            Get
                oLMBS = m_intLMBS_O
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intLMBS_O = Value
                Catch ex As Exception
                    m_intLMBS_O = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' oLMDM����
        '----------------------------------------------------------------
        Public Property oLMDM() As String
            Get
                oLMDM = m_strLMDM_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strLMDM_O = Value
                Catch ex As Exception
                    m_strLMDM_O = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' oSJDM����
        '----------------------------------------------------------------
        Public Property oSJDM() As String
            Get
                oSJDM = m_strSJDM_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strSJDM_O = Value
                Catch ex As Exception
                    m_strSJDM_O = ""
                End Try
            End Set
        End Property

    End Class

End Namespace
