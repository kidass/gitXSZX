Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IDmxzZclb
    '
    ' ���������� 
    '     dmxz_zclb.aspxģ����ýӿڵĶ����봦��
    '----------------------------------------------------------------
    <Serializable()> Public Class IDmxzZclb
        Inherits Xydc.Platform.BusinessFacade.ICallInterface

        '----------------------------------------------------------------
        '�������
        '----------------------------------------------------------------
        Private m_strInputList_I As String            '��������б�
        Private m_blnMultiSelect_I As Boolean         '���Զ���ѡ��?false-����,true-��(ȱʡ)
        Private m_blnSelectFFFW_I As Boolean          '����ѡ��Χ?false-����,true-��(ȱʡ)
        Private m_blnAllowInput_I As Boolean          '�����ֹ�����?false-����,true-��(ȱʡ)
        Private m_blnAllowNull_I As Boolean           '���������?false-����,true-��(ȱʡ)

        '----------------------------------------------------------------
        '�������
        '----------------------------------------------------------------
        Private m_blnExitMode_O As Boolean            '�˳���ʽ��True-ȷ����False-ȡ��
        Private m_strOutputList_O As String           '��ѡ�������б�,��ϵͳָ���ķָ����ָ�(CharSeparate)
        Private m_objDataSet_O As System.Data.DataSet '��ѡʱ���ص����ݼ�









        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            '��ʼ���������
            MyBase.iInterfaceType = ICallInterface.enumInterfaceType.InputAndOutput

            '��ʼ���������
            m_strInputList_I = ""
            m_blnMultiSelect_I = True
            m_blnSelectFFFW_I = True
            m_blnAllowInput_I = True
            m_blnAllowNull_I = True

            '��ʼ���������
            m_blnExitMode_O = False
            m_strOutputList_O = ""
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
            Try
                If Not (m_objDataSet_O Is Nothing) Then
                    m_objDataSet_O.Dispose()
                    m_objDataSet_O = Nothing
                End If
            Catch ex As Exception
            End Try
        End Sub

        '----------------------------------------------------------------
        ' ��ȫ�ͷű�����Դ
        '----------------------------------------------------------------
        Public Overloads Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IDmxzZclb)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub











        '----------------------------------------------------------------
        ' iLeibieList����
        '----------------------------------------------------------------
        Public Property iLeibieList() As String
            Get
                iLeibieList = m_strInputList_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strInputList_I = Value
                Catch ex As Exception
                    m_strInputList_I = ""
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
        ' iSelectFFFW����
        '----------------------------------------------------------------
        Public Property iSelectFFFW() As Boolean
            Get
                iSelectFFFW = m_blnSelectFFFW_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnSelectFFFW_I = Value
                Catch ex As Exception
                    m_blnSelectFFFW_I = False
                End Try
            End Set
        End Property

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
        ' oLeibieList����
        '----------------------------------------------------------------
        Public Property oLeibieList() As String
            Get
                oLeibieList = m_strOutputList_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strOutputList_O = Value
                Catch ex As Exception
                    m_strOutputList_O = ""
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
