Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IFlowEditWord
    '
    ' ���������� 
    '     flow_editword.aspxģ����ýӿڵĶ����봦��
    '----------------------------------------------------------------
    <Serializable()> Public Class IFlowEditWord
        Inherits Xydc.Platform.BusinessFacade.ICallInterface

        '----------------------------------------------------------------
        '�������
        '----------------------------------------------------------------
        Private m_strFlowTypeName_I As String                                        '��������������
        Private m_strWJBS_I As String                                                '�ļ���ʶ
        Private m_blnEditMode_I As Boolean                                           '�༭ģʽ
        Private m_blnAutoSave_I As Boolean                                           '�Ƿ���Ҫ�Զ�����
        Private m_blnEnforeEdit_I As Boolean                                         '�Ƿ񶨸���޸�?
        Private m_blnTrackRevisions_I As Boolean                                     '�ļ�֧�ֺۼ���¼?
        Private m_strGJFileSpec_I As String                                          '��ǰ���ڱ༭�ĸ���ļ�,û�б༭��=""(���ļ���)
        Private m_objNewData_I As System.Collections.Specialized.NameValueCollection '�������༭ʱ�����ļ�����
        Private m_objDataSet_FJ_I As Xydc.Platform.Common.Data.FlowData                 '�������༭ʱ�ĸ�������
        Private m_objDataSet_XGWJ_I As Xydc.Platform.Common.Data.FlowData               '�������༭ʱ������ļ�����
        Private m_strSPR_I As String                                                 'ǩ��������(���Լ�ǩ��="")
        Private m_strDLR_I As String                                                 '����������
        Private m_strDLRDM_I As String                                               '�����˴���
        Private m_strDLRBMDM_I As String                                             '�����˵�λ����
        Private m_blnHasSendOnce_I As Boolean                                        '�ļ��Ƿ��͹�?
        Private m_blnCanQSYJ_I As Boolean                                            '��ǰ��Ա�Ƿ�ɱ߸ı�ǩ�����?
        Private m_blnCanImportGJ_I As Boolean                                        '�Ƿ�֧�ֵ������ļ�?
        Private m_blnCanExportGJ_I As Boolean                                        '�Ƿ�֧�ֵ�������ļ�?
        Private m_blnCanSelectTGWJ_I As Boolean                                      '�Ƿ�֧��ѡ��Ͷ���ļ�

        '----------------------------------------------------------------
        '�������
        '----------------------------------------------------------------
        '���ط�ʽ��True-ȷ����False-ȡ��
        Private m_blnExitMode_O As Boolean









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
            m_strGJFileSpec_I = ""
            m_strFlowTypeName_I = ""
            m_blnAutoSave_I = False
            m_objNewData_I = Nothing
            m_objDataSet_FJ_I = Nothing
            m_objDataSet_XGWJ_I = Nothing
            m_strSPR_I = ""
            m_strDLR_I = ""
            m_strDLRDM_I = ""
            m_strDLRBMDM_I = ""
            m_blnTrackRevisions_I = False
            m_blnHasSendOnce_I = False
            m_blnCanQSYJ_I = False
            m_blnCanImportGJ_I = False
            m_blnCanExportGJ_I = False
            m_blnCanSelectTGWJ_I = False
            m_blnEnforeEdit_I = False

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
            If Not (m_objNewData_I Is Nothing) Then
                m_objNewData_I.Clear()
                m_objNewData_I = Nothing
            End If
        End Sub

        '----------------------------------------------------------------
        ' ��ȫ�ͷű�����Դ
        '----------------------------------------------------------------
        Public Overloads Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IFlowEditWord)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub















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
        ' iGJFileSpec����
        '----------------------------------------------------------------
        Public Property iGJFileSpec() As String
            Get
                iGJFileSpec = m_strGJFileSpec_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strGJFileSpec_I = Value
                Catch ex As Exception
                    m_strGJFileSpec_I = ""
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
        ' iobjNewData����
        '----------------------------------------------------------------
        Public Property iobjNewData() As System.Collections.Specialized.NameValueCollection
            Get
                iobjNewData = m_objNewData_I
            End Get
            Set(ByVal Value As System.Collections.Specialized.NameValueCollection)
                Try
                    m_objNewData_I = Value
                Catch ex As Exception
                    m_objNewData_I = Nothing
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iobjDataSet_FJ����
        '----------------------------------------------------------------
        Public Property iobjDataSet_FJ() As Xydc.Platform.Common.Data.FlowData
            Get
                iobjDataSet_FJ = m_objDataSet_FJ_I
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
        ' iobjDataSet_XGWJ����
        '----------------------------------------------------------------
        Public Property iobjDataSet_XGWJ() As Xydc.Platform.Common.Data.FlowData
            Get
                iobjDataSet_XGWJ = m_objDataSet_XGWJ_I
            End Get
            Set(ByVal Value As Xydc.Platform.Common.Data.FlowData)
                Try
                    m_objDataSet_XGWJ_I = Value
                Catch ex As Exception
                    m_objDataSet_XGWJ_I = Nothing
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iSPR����
        '----------------------------------------------------------------
        Public Property iSPR() As String
            Get
                iSPR = m_strSPR_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strSPR_I = Value
                Catch ex As Exception
                    m_strSPR_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iDLR����
        '----------------------------------------------------------------
        Public Property iDLR() As String
            Get
                iDLR = m_strDLR_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strDLR_I = Value
                Catch ex As Exception
                    m_strDLR_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iDLRDM����
        '----------------------------------------------------------------
        Public Property iDLRDM() As String
            Get
                iDLRDM = m_strDLRDM_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strDLRDM_I = Value
                Catch ex As Exception
                    m_strDLRDM_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iDLRBMDM����
        '----------------------------------------------------------------
        Public Property iDLRBMDM() As String
            Get
                iDLRBMDM = m_strDLRBMDM_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strDLRBMDM_I = Value
                Catch ex As Exception
                    m_strDLRBMDM_I = ""
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
        ' iHasSendOnce����
        '----------------------------------------------------------------
        Public Property iHasSendOnce() As Boolean
            Get
                iHasSendOnce = m_blnHasSendOnce_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnHasSendOnce_I = Value
                Catch ex As Exception
                    m_blnHasSendOnce_I = False
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iCanQSYJ����
        '----------------------------------------------------------------
        Public Property iCanQSYJ() As Boolean
            Get
                iCanQSYJ = m_blnCanQSYJ_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnCanQSYJ_I = Value
                Catch ex As Exception
                    m_blnCanQSYJ_I = False
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iCanImportGJ����
        '----------------------------------------------------------------
        Public Property iCanImportGJ() As Boolean
            Get
                iCanImportGJ = m_blnCanImportGJ_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnCanImportGJ_I = Value
                Catch ex As Exception
                    m_blnCanImportGJ_I = False
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iCanExportGJ����
        '----------------------------------------------------------------
        Public Property iCanExportGJ() As Boolean
            Get
                iCanExportGJ = m_blnCanExportGJ_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnCanExportGJ_I = Value
                Catch ex As Exception
                    m_blnCanExportGJ_I = False
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iCanSelectTGWJ����
        '----------------------------------------------------------------
        Public Property iCanSelectTGWJ() As Boolean
            Get
                iCanSelectTGWJ = m_blnCanSelectTGWJ_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnCanSelectTGWJ_I = Value
                Catch ex As Exception
                    m_blnCanSelectTGWJ_I = False
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
