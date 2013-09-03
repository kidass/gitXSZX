Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IFlowSend
    '
    ' ���������� 
    '     flow_send.aspxģ����ýӿڵĶ����봦��
    '----------------------------------------------------------------
    <Serializable()> Public Class IFlowSend
        Inherits Xydc.Platform.BusinessFacade.ICallInterface

        '----------------------------------------------------------------
        '�������
        '----------------------------------------------------------------
        Private m_strFlowTypeName_I As String                        '��������������
        Private m_strWJBS_I As String                                '�ļ���ʶ
        Private m_blnWTFS_I As Boolean                               '׼��ί������
        Private m_strJSR_I As String                                 'ָ���������б�(��׼�ָ����ָ�)
        Private m_strBLR_I As String                                 '��ǰ������
        Private m_strDLR_I As String                                 'ί����

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
            m_blnWTFS_I = False
            m_strWJBS_I = ""
            m_strJSR_I = ""
            m_strBLR_I = ""
            m_strDLR_I = ""

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
        Public Overloads Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IFlowSend)
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
        ' iWTFS����
        '----------------------------------------------------------------
        Public Property iWTFS() As Boolean
            Get
                iWTFS = m_blnWTFS_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnWTFS_I = Value
                Catch ex As Exception
                    m_blnWTFS_I = False
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iJSR����
        '----------------------------------------------------------------
        Public Property iJSR() As String
            Get
                iJSR = m_strJSR_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strJSR_I = Value
                Catch ex As Exception
                    m_strJSR_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iBLR����
        '----------------------------------------------------------------
        Public Property iBLR() As String
            Get
                iBLR = m_strBLR_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strBLR_I = Value
                Catch ex As Exception
                    m_strBLR_I = ""
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
