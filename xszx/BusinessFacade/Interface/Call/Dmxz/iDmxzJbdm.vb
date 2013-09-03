Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IDmxzJbdm
    '
    ' ���������� 
    '     dmxz_zzry.aspxģ����ýӿڵĶ����봦��
    '----------------------------------------------------------------
    <Serializable()> Public Class IDmxzJbdm
        Inherits Xydc.Platform.BusinessFacade.ICallInterface

        '�������뷽ʽ����
        Public Enum enumCodeInputType
            ByDataGrid = 1        '����������
            ByInput = 2           '���û��ֹ�����
        End Enum

        '----------------------------------------------------------------
        '�������
        '----------------------------------------------------------------
        Private m_strTitle_I As String                    'ģ�����
        Private m_strRowSourceSQL_I As String             '�б��õ�SQL�ַ���
        Private m_strInitField_I As String                '��ʼֵ��Ӧ���ֶ���
        Private m_strInitValue_I As String                '��ʼֵ
        Private m_strReturnCodeField_I As String          '���صĴ����ֶ���
        Private m_strReturnNameField_I As String          '���ص������ֶ���
        Private m_blnAllowInput_I As Boolean              '�Ƿ������ֹ�����(Ĭ��True-����)
        Private m_blnAllowNull_I As Boolean               '���������(Ĭ��True-����)
        Private m_blnMultiSelect_I As Boolean             '�����ѡ(Ĭ��True-����)
        Private m_strColWidth_I As String                 '�п�˵��(ϵͳ��׼�ָ����ָ�)

        '----------------------------------------------------------------
        '�������
        '----------------------------------------------------------------
        Private m_blnExitMode_O As Boolean                '���ط�ʽ��True-ȷ����False-ȡ��
        Private m_strReturnCodeValue_O As String          '���صĴ����ֶζ�Ӧ���ֶ�ֵ
        Private m_strReturnNameValue_O As String          '���ص������ֶζ�Ӧ���ֶ�ֵ
        Private m_enumSelectMode_O As enumCodeInputType   'ѡ��ʽ��1-����2-�ֹ�����
        Private m_objDataSet_O As System.Data.DataSet     '��ѡʱ���ص����ݼ�










        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            '��ʼ���������
            MyBase.iInterfaceType = ICallInterface.enumInterfaceType.InputAndOutput

            '��ʼ���������
            m_strTitle_I = ""
            m_strRowSourceSQL_I = ""
            m_strInitField_I = ""
            m_strInitValue_I = ""
            m_strReturnCodeField_I = ""
            m_strReturnNameField_I = ""
            m_blnAllowInput_I = True
            m_blnAllowNull_I = True
            m_blnMultiSelect_I = True
            m_strColWidth_I = ""

            '��ʼ���������
            m_blnExitMode_O = False
            m_strReturnCodeValue_O = ""
            m_strReturnNameValue_O = ""
            m_enumSelectMode_O = enumCodeInputType.ByDataGrid
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
            ''�ͷ���Դ
            'If Not (m_objDataSet_O Is Nothing) Then
            '    m_objDataSet_O.Dispose()
            '    m_objDataSet_O = Nothing
            'End If

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
        Public Overloads Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IDmxzJbdm)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub












        '----------------------------------------------------------------
        ' iTitle����
        '----------------------------------------------------------------
        Public Property iTitle() As String
            Get
                iTitle = m_strTitle_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strTitle_I = Value
                Catch ex As Exception
                    m_strTitle_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iRowSourceSQL����
        '----------------------------------------------------------------
        Public Property iRowSourceSQL() As String
            Get
                iRowSourceSQL = m_strRowSourceSQL_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strRowSourceSQL_I = Value
                Catch ex As Exception
                    m_strRowSourceSQL_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iInitField����
        '----------------------------------------------------------------
        Public Property iInitField() As String
            Get
                iInitField = m_strInitField_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strInitField_I = Value
                Catch ex As Exception
                    m_strInitField_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iInitValue����
        '----------------------------------------------------------------
        Public Property iInitValue() As String
            Get
                iInitValue = m_strInitValue_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strInitValue_I = Value
                Catch ex As Exception
                    m_strInitValue_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iCodeField����
        '----------------------------------------------------------------
        Public Property iCodeField() As String
            Get
                iCodeField = m_strReturnCodeField_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strReturnCodeField_I = Value
                Catch ex As Exception
                    m_strReturnCodeField_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iNameField����
        '----------------------------------------------------------------
        Public Property iNameField() As String
            Get
                iNameField = m_strReturnNameField_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strReturnNameField_I = Value
                Catch ex As Exception
                    m_strReturnNameField_I = ""
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
        ' iColWidth����
        '----------------------------------------------------------------
        Public Property iColWidth() As String
            Get
                iColWidth = m_strColWidth_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strColWidth_I = Value
                Catch ex As Exception
                    m_strColWidth_I = ""
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
        ' oCodeValue����
        '----------------------------------------------------------------
        Public Property oCodeValue() As String
            Get
                oCodeValue = m_strReturnCodeValue_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strReturnCodeValue_O = Value
                Catch ex As Exception
                    m_strReturnCodeValue_O = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' oNameValue����
        '----------------------------------------------------------------
        Public Property oNameValue() As String
            Get
                oNameValue = m_strReturnNameValue_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strReturnNameValue_O = Value
                Catch ex As Exception
                    m_strReturnNameValue_O = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' oSelectMode����
        '----------------------------------------------------------------
        Public Property oSelectMode() As enumCodeInputType
            Get
                oSelectMode = m_enumSelectMode_O
            End Get
            Set(ByVal Value As enumCodeInputType)
                Try
                    m_enumSelectMode_O = Value
                Catch ex As Exception
                    m_enumSelectMode_O = enumCodeInputType.ByDataGrid
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
