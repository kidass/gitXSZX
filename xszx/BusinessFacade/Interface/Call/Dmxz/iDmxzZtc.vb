Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IDmxzZtc
    '
    ' ���������� 
    '     dmxz_ztc.aspxģ����ýӿڵĶ����봦��
    '----------------------------------------------------------------
    <Serializable()> Public Class IDmxzZtc
        Inherits Xydc.Platform.BusinessFacade.ICallInterface

        '----------------------------------------------------------------
        '�������
        '----------------------------------------------------------------
        Private m_blnMultiSelect_I As Boolean             '�����ѡ(Ĭ��True-����)
        Private m_blnAllowInput_I As Boolean              '�Ƿ������ֹ�����(Ĭ��True-����)
        Private m_blnAllowNull_I As Boolean               '���������(Ĭ��True-����)
        Private m_strZTC_I As String                      '��ǰ�����������Ϣ

        '----------------------------------------------------------------
        '�������
        '----------------------------------------------------------------
        Private m_blnExitMode_O As Boolean                '���ط�ʽ��True-ȷ����False-ȡ��
        Private m_objDataSet_O As System.Data.DataSet     '��ѡʱ���ص����ݼ�
        Private m_strZTC_O As String                      '���������������Ϣ









        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            '��ʼ���������
            MyBase.iInterfaceType = ICallInterface.enumInterfaceType.InputAndOutput

            '��ʼ���������
            m_blnAllowInput_I = True
            m_blnAllowNull_I = True
            m_blnMultiSelect_I = True
            m_strZTC_I = ""

            '��ʼ���������
            m_blnExitMode_O = False
            m_strZTC_O = ""
            m_objDataSet_O = Nothing

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
            If Not (m_objDataSet_O Is Nothing) Then
                m_objDataSet_O.Dispose()
                m_objDataSet_O = Nothing
            End If
        End Sub

        '----------------------------------------------------------------
        ' ��ȫ�ͷű�����Դ
        '----------------------------------------------------------------
        Public Overloads Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IDmxzZtc)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub












        '----------------------------------------------------------------
        ' iAllowInput����
        '----------------------------------------------------------------
        Public Property iAllowInput() As Boolean
            Get
                iAllowInput = m_blnAllowInput_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnAllowInput_I = Value
                Catch ex As Exception
                    m_blnAllowInput_I = False
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iAllowNull����
        '----------------------------------------------------------------
        Public Property iAllowNull() As Boolean
            Get
                iAllowNull = m_blnAllowNull_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnAllowNull_I = Value
                Catch ex As Exception
                    m_blnAllowNull_I = False
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iMultiSelect����
        '----------------------------------------------------------------
        Public Property iMultiSelect() As Boolean
            Get
                iMultiSelect = m_blnMultiSelect_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnMultiSelect_I = Value
                Catch ex As Exception
                    m_blnMultiSelect_I = False
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iZTC����
        '----------------------------------------------------------------
        Public Property iZTC() As String
            Get
                iZTC = m_strZTC_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strZTC_I = Value
                Catch ex As Exception
                    m_strZTC_I = ""
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
        ' oZTC����
        '----------------------------------------------------------------
        Public Property oZTC() As String
            Get
                oZTC = m_strZTC_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strZTC_O = Value
                Catch ex As Exception
                    m_strZTC_O = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' oDataSet����
        '----------------------------------------------------------------
        Public Property oDataSet() As System.Data.DataSet
            Get
                oDataSet = m_objDataSet_O
            End Get
            Set(ByVal Value As System.Data.DataSet)
                Try
                    m_objDataSet_O = Value
                Catch ex As Exception
                    m_objDataSet_O = Nothing
                End Try
            End Set
        End Property

    End Class

End Namespace
