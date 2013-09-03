Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IFlowSpyjtx
    '
    ' ���������� 
    '     flow_spyjtx.aspxģ����ýӿڵĶ����봦��
    '----------------------------------------------------------------
    <Serializable()> Public Class IFlowSpyjtx
        Inherits Xydc.Platform.BusinessFacade.ICallInterface

        '----------------------------------------------------------------
        '�������
        '----------------------------------------------------------------
        Private m_strFlowTypeName_I As String                        '��������������
        Private m_strWJBS_I As String                                '�ļ���ʶ
        Private m_strSPR_I As String                                 '������
        Private m_strDLR_I As String                                 '������
        Private m_strInitYjlx_I As String                            '��ʼ�������
        Private m_strPromptInfo_I As String                          '��ʾ��Ϣ
        Private m_blnYjlxEnabled() As Boolean                        '��ǩ����Щ���
        Private m_blnDisplayXBBZ_I As Boolean                        '�Ƿ���ʾЭ���־
        Private m_blnXBBZ_I As Boolean                               '�Ƿ�ΪЭ��

        '----------------------------------------------------------------
        '�������
        '----------------------------------------------------------------
        Private m_blnExitMode_O As Boolean                           'True-ȷ��,False-ȡ��









        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            '��ʼ���������
            MyBase.iInterfaceType = ICallInterface.enumInterfaceType.InputAndOutput

            '��ʼ���������
            m_strFlowTypeName_I = ""
            m_strWJBS_I = ""
            m_strSPR_I = ""
            m_strDLR_I = ""
            m_strInitYjlx_I = ""
            m_strPromptInfo_I = ""
            m_blnYjlxEnabled = Nothing
            m_blnDisplayXBBZ_I = False
            m_blnXBBZ_I = False

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
        Public Overloads Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IFlowSpyjtx)
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
        ' iInitYjlx����
        '----------------------------------------------------------------
        Public Property iInitYjlx() As String
            Get
                iInitYjlx = m_strInitYjlx_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strInitYjlx_I = Value
                Catch ex As Exception
                    m_strInitYjlx_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iPromptInfo����
        '----------------------------------------------------------------
        Public Property iPromptInfo() As String
            Get
                iPromptInfo = m_strPromptInfo_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strPromptInfo_I = Value
                Catch ex As Exception
                    m_strPromptInfo_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iYjlxEnabled����
        '----------------------------------------------------------------
        Public Property iYjlxEnabled() As Boolean()
            Get
                iYjlxEnabled = m_blnYjlxEnabled
            End Get
            Set(ByVal Value As Boolean())
                Try
                    m_blnYjlxEnabled = Value
                Catch ex As Exception
                    m_blnYjlxEnabled = Nothing
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iDisplayXBBZ����
        '----------------------------------------------------------------
        Public Property iDisplayXBBZ() As Boolean
            Get
                iDisplayXBBZ = m_blnDisplayXBBZ_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnDisplayXBBZ_I = Value
                Catch ex As Exception
                    m_blnDisplayXBBZ_I = False
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iXBBZ����
        '----------------------------------------------------------------
        Public Property iXBBZ() As Boolean
            Get
                iXBBZ = m_blnXBBZ_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnXBBZ_I = Value
                Catch ex As Exception
                    m_blnXBBZ_I = False
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
