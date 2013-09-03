Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IFlowFujianInfo
    '
    ' ���������� 
    '     flow_fujian_info.aspxģ����ýӿڵĶ����봦��
    '----------------------------------------------------------------
    <Serializable()> Public Class IFlowFujianInfo
        Inherits Xydc.Platform.BusinessFacade.ICallInterface

        '----------------------------------------------------------------
        '�������
        '----------------------------------------------------------------
        Private m_objEditType_I As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType       '����༭����
        Private m_objRow_I As System.Data.DataRow                                                 '���뵱ǰ������
        Private m_strFlowTypeName_I As String                                                     '���빤��������
        Private m_strWJBS_I As String                                                             '�����ļ���ʶ
        Private m_strWJXH_I As String                                                             '�����ļ����
        Private m_strBDWJ_I As String                                                             '�����ļ�λ��(WEB�����ļ�·��)
        Private m_strWJSM_I As String                                                             '�����ļ�˵��
        Private m_strWJYS_I As String                                                             '�����ļ�ҳ��
        Private m_strWJWZ_I As String                                                             '�����ļ�λ��(FTP�ļ�·��)
        Private m_blnTrackRevisions_I As Boolean                                                  '�ļ�֧�ֺۼ���¼?
        Private m_blnAutoSave_I As Boolean                                                        '�˳�ʱ�Զ����渽��
        Private m_blnEnforeEdit_I As Boolean                                                      '�Ƿ񶨸���޸�?



        '----------------------------------------------------------------
        '�������
        '----------------------------------------------------------------
        Private m_blnExitMode_O As Boolean                                                        '���ط�ʽ��true-ȷ��,false-ȡ��
        Private m_strWJXH_O As String                                                             '������
        Private m_strBDWJ_O As String                                                             '����ļ�λ��(WEB�����ļ�·��)
        Private m_strWJSM_O As String                                                             '����ļ�˵��
        Private m_strWJYS_O As String                                                             '����ļ�ҳ��
        Private m_strWJWZ_O As String                                                             '����ļ�λ��(FTP�ļ�·��)










        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            '��ʼ���������
            MyBase.iInterfaceType = ICallInterface.enumInterfaceType.InputAndOutput

            '��ʼ���������
            m_objEditType_I = Common.Utilities.PulicParameters.enumEditType.eSelect
            m_objRow_I = Nothing
            m_strFlowTypeName_I = ""
            m_strWJBS_I = ""
            m_strWJXH_I = ""
            m_strBDWJ_I = ""
            m_strWJSM_I = ""
            m_strWJYS_I = ""
            m_strWJWZ_I = ""
            m_blnTrackRevisions_I = False
            m_blnAutoSave_I = False
            m_blnEnforeEdit_I = False

            '��ʼ���������
            m_blnExitMode_O = False
            m_strWJXH_O = ""
            m_strBDWJ_O = ""
            m_strWJSM_O = ""
            m_strWJYS_O = ""
            m_strWJWZ_O = ""

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
        Public Overloads Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IFlowFujianInfo)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub











        '----------------------------------------------------------------
        ' iEditType����
        '----------------------------------------------------------------
        Public Property iEditType() As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType
            Get
                iEditType = m_objEditType_I
            End Get
            Set(ByVal Value As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType)
                Try
                    m_objEditType_I = Value
                Catch ex As Exception
                    m_objEditType_I = Common.Utilities.PulicParameters.enumEditType.eSelect
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
        ' iRow����
        '----------------------------------------------------------------
        Public Property iRow() As System.Data.DataRow
            Get
                iRow = m_objRow_I
            End Get
            Set(ByVal Value As System.Data.DataRow)
                Try
                    m_objRow_I = Value
                Catch ex As Exception
                    m_objRow_I = Nothing
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iWJXH����
        '----------------------------------------------------------------
        Public Property iWJXH() As String
            Get
                iWJXH = m_strWJXH_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strWJXH_I = Value
                Catch ex As Exception
                    m_strWJXH_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iBDWJ����
        '----------------------------------------------------------------
        Public Property iBDWJ() As String
            Get
                iBDWJ = m_strBDWJ_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strBDWJ_I = Value
                Catch ex As Exception
                    m_strBDWJ_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iWJSM����
        '----------------------------------------------------------------
        Public Property iWJSM() As String
            Get
                iWJSM = m_strWJSM_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strWJSM_I = Value
                Catch ex As Exception
                    m_strWJSM_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iWJYS����
        '----------------------------------------------------------------
        Public Property iWJYS() As String
            Get
                iWJYS = m_strWJYS_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strWJYS_I = Value
                Catch ex As Exception
                    m_strWJYS_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iWJWZ����
        '----------------------------------------------------------------
        Public Property iWJWZ() As String
            Get
                iWJWZ = m_strWJWZ_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strWJWZ_I = Value
                Catch ex As Exception
                    m_strWJWZ_I = ""
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
        ' oWJXH����
        '----------------------------------------------------------------
        Public Property oWJXH() As String
            Get
                oWJXH = m_strWJXH_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strWJXH_O = Value
                Catch ex As Exception
                    m_strWJXH_O = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' oBDWJ����
        '----------------------------------------------------------------
        Public Property oBDWJ() As String
            Get
                oBDWJ = m_strBDWJ_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strBDWJ_O = Value
                Catch ex As Exception
                    m_strBDWJ_O = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' oWJSM����
        '----------------------------------------------------------------
        Public Property oWJSM() As String
            Get
                oWJSM = m_strWJSM_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strWJSM_O = Value
                Catch ex As Exception
                    m_strWJSM_O = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' oWJYS����
        '----------------------------------------------------------------
        Public Property oWJYS() As String
            Get
                oWJYS = m_strWJYS_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strWJYS_O = Value
                Catch ex As Exception
                    m_strWJYS_O = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' oWJWZ����
        '----------------------------------------------------------------
        Public Property oWJWZ() As String
            Get
                oWJWZ = m_strWJWZ_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strWJWZ_O = Value
                Catch ex As Exception
                    m_strWJWZ_O = ""
                End Try
            End Set
        End Property

    End Class

End Namespace
