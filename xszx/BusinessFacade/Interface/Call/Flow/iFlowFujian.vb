Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IFlowFujian
    '
    ' ���������� 
    '     flow_fujian.aspxģ����ýӿڵĶ����봦��
    '----------------------------------------------------------------
    <Serializable()> Public Class IFlowFujian
        Inherits Xydc.Platform.BusinessFacade.ICallInterface

        '----------------------------------------------------------------
        '�������
        '----------------------------------------------------------------
        Private m_strFlowTypeName_I As String                        '��������������
        Private m_strWJBS_I As String                                '�ļ���ʶ
        Private m_objDataSet_FJ_I As Xydc.Platform.Common.Data.FlowData '��������
        Private m_blnEditMode_I As Boolean                           '�༭ģʽ
        Private m_blnTrackRevisions_I As Boolean                     '�ļ�֧�ֺۼ���¼?
        Private m_blnAutoSave_I As Boolean                           '�˳�ʱ�Զ����渽�������ݿ�
        Private m_blnEnforeEdit_I As Boolean                         '�Ƿ񶨸���޸�?

        '----------------------------------------------------------------
        '�������
        '----------------------------------------------------------------
        '����䶯��ĸ������� = m_objDataSet_FJ_I









        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            '��ʼ���������
            MyBase.iInterfaceType = ICallInterface.enumInterfaceType.InputAndOutput

            '��ʼ���������
            m_blnEditMode_I = False
            m_strWJBS_I = ""
            m_objDataSet_FJ_I = Nothing
            m_blnTrackRevisions_I = False
            m_blnAutoSave_I = False
            m_blnEnforeEdit_I = False

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
        Public Overloads Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IFlowFujian)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub











        '----------------------------------------------------------------
        ' iFlowTypeName����
        '----------------------------------------------------------------
        Public Property iFlowTypeName() As String
            Get
                iFlowTypeName = m_strFlowTypeName_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strFlowTypeName_I = Value
                Catch ex As Exception
                    m_strFlowTypeName_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iWJBS����
        '----------------------------------------------------------------
        Public Property iWJBS() As String
            Get
                iWJBS = m_strWJBS_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strWJBS_I = Value
                Catch ex As Exception
                    m_strWJBS_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iEditMode����
        '----------------------------------------------------------------
        Public Property iEditMode() As Boolean
            Get
                iEditMode = m_blnEditMode_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnEditMode_I = Value
                Catch ex As Exception
                    m_blnEditMode_I = False
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iTrackRevisions����
        '----------------------------------------------------------------
        Public Property iTrackRevisions() As Boolean
            Get
                iTrackRevisions = m_blnTrackRevisions_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnTrackRevisions_I = Value
                Catch ex As Exception
                    m_blnTrackRevisions_I = False
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iAutoSave����
        '----------------------------------------------------------------
        Public Property iAutoSave() As Boolean
            Get
                iAutoSave = m_blnAutoSave_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnAutoSave_I = Value
                Catch ex As Exception
                    m_blnAutoSave_I = False
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iEnforeEdit����
        '----------------------------------------------------------------
        Public Property iEnforeEdit() As Boolean
            Get
                iEnforeEdit = m_blnEnforeEdit_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnEnforeEdit_I = Value
                Catch ex As Exception
                    m_blnEnforeEdit_I = False
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iDataSet_FJ����
        '----------------------------------------------------------------
        Public Property iDataSet_FJ() As Xydc.Platform.Common.Data.FlowData
            Get
                iDataSet_FJ = m_objDataSet_FJ_I
            End Get
            Set(ByVal Value As Xydc.Platform.Common.Data.FlowData)
                Try
                    m_objDataSet_FJ_I = Value
                Catch ex As Exception
                    m_objDataSet_FJ_I = Nothing
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' oDataSet_FJ����
        '----------------------------------------------------------------
        Public Property oDataSet_FJ() As Xydc.Platform.Common.Data.FlowData
            Get
                oDataSet_FJ = m_objDataSet_FJ_I
            End Get
            Set(ByVal Value As Xydc.Platform.Common.Data.FlowData)
                Try
                    m_objDataSet_FJ_I = Value
                Catch ex As Exception
                    m_objDataSet_FJ_I = Nothing
                End Try
            End Set
        End Property

    End Class

End Namespace
