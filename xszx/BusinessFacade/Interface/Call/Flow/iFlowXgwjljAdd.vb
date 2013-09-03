Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IFlowXgwjljAdd
    '
    ' ���������� 
    '     flow_xgwjlj_add.aspxģ����ýӿڵĶ����봦��
    '----------------------------------------------------------------
    <Serializable()> Public Class IFlowXgwjljAdd
        Inherits Xydc.Platform.BusinessFacade.ICallInterface

        '----------------------------------------------------------------
        '�������
        '----------------------------------------------------------------
        Private m_objDataSet_XGWJ_I As Xydc.Platform.Common.Data.FlowData '����ļ�����
        Private m_strFlowTypeName_I As String                          '��������������
        Private m_strWJBS_I As String                                  '�ļ���ʶ

        '----------------------------------------------------------------
        '�������
        '----------------------------------------------------------------
        '����䶯��ĸ������� = m_objDataSet_XGWJ_I









        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            '��ʼ���������
            MyBase.iInterfaceType = ICallInterface.enumInterfaceType.InputAndOutput

            '��ʼ���������
            m_strFlowTypeName_I = ""
            m_objDataSet_XGWJ_I = Nothing
            m_strWJBS_I = ""

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
        Public Overloads Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IFlowXgwjljAdd)
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
        ' iDataSet_XGWJ����
        '----------------------------------------------------------------
        Public Property iDataSet_XGWJ() As Xydc.Platform.Common.Data.FlowData
            Get
                iDataSet_XGWJ = m_objDataSet_XGWJ_I
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
        ' oDataSet_XGWJ����
        '----------------------------------------------------------------
        Public Property oDataSet_XGWJ() As Xydc.Platform.Common.Data.FlowData
            Get
                oDataSet_XGWJ = m_objDataSet_XGWJ_I
            End Get
            Set(ByVal Value As Xydc.Platform.Common.Data.FlowData)
                Try
                    m_objDataSet_XGWJ_I = Value
                Catch ex As Exception
                    m_objDataSet_XGWJ_I = Nothing
                End Try
            End Set
        End Property

    End Class

End Namespace
