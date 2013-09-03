Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��ISjcxCxtj
    '
    ' ���������� 
    '     sjcx_cxtj.aspxģ����ýӿڵĶ����봦��
    '
    ' ��ע��Ϣ��
    '     m_objQueryTable_I��Ĭ�ϱ�ǰ׺Ϊ��a.��
    '----------------------------------------------------------------
    <Serializable()> Public Class ISjcxCxtj
        Inherits Xydc.Platform.BusinessFacade.ICallInterface

        '----------------------------------------------------------------
        '�������
        '----------------------------------------------------------------
        Private m_objQueryTable_I As System.Data.DataTable               'Ҫ�����ı����
        Private m_objDataSetTJ_I As Xydc.Platform.Common.Data.QueryData     '���в�ѯ����
        Private m_strFixQuery_I As String                                '�������õĲ�ѯ����
        '----------------------------------------------------------------
        '�������
        '----------------------------------------------------------------
        Private m_blnExitMode_O As Boolean                               '���ط�ʽ��True-ȷ����False-ȡ��
        Private m_objDataSetTJ_O As Xydc.Platform.Common.Data.QueryData     '���ز�ѯ����
        Private m_strQuery_O As String                                   '���ز�ѯ�����ַ���












        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            '��ʼ���������
            MyBase.iInterfaceType = ICallInterface.enumInterfaceType.InputAndOutput

            '��ʼ���������
            m_objQueryTable_I = Nothing
            'm_objDataSetTJ_I = Nothing
            m_strFixQuery_I = ""

            '��ʼ���������
            m_blnExitMode_O = False
            'm_objDataSetTJ_O = Nothing
            m_strQuery_O = ""

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
        Public Overloads Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.ISjcxCxtj)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub













        '----------------------------------------------------------------
        ' iQueryTable����
        '----------------------------------------------------------------
        Public Property iQueryTable() As System.Data.DataTable
            Get
                iQueryTable = m_objQueryTable_I
            End Get
            Set(ByVal Value As System.Data.DataTable)
                Try
                    m_objQueryTable_I = Value
                Catch ex As Exception
                    m_objQueryTable_I = Nothing
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        'iDataSetTJ����
        '----------------------------------------------------------------
        Public Property iDataSetTJ() As Xydc.Platform.Common.Data.QueryData
            Get
                iDataSetTJ = m_objDataSetTJ_I
            End Get
            Set(ByVal Value As Xydc.Platform.Common.Data.QueryData)
                Try
                    m_objDataSetTJ_I = Value
                Catch ex As Exception
                    m_objDataSetTJ_I = Nothing
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iFixQuery����
        '----------------------------------------------------------------
        Public Property iFixQuery() As String
            Get
                iFixQuery = m_strFixQuery_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strFixQuery_I = Value
                Catch ex As Exception
                    m_strFixQuery_I = ""
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
        'oDataSetTJ����
        '----------------------------------------------------------------
        Public Property oDataSetTJ() As Xydc.Platform.Common.Data.QueryData
            Get
                oDataSetTJ = m_objDataSetTJ_O
            End Get
            Set(ByVal Value As Xydc.Platform.Common.Data.QueryData)
                Try
                    m_objDataSetTJ_O = Value
                Catch ex As Exception
                    m_objDataSetTJ_O = Nothing
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' oQueryString����
        '----------------------------------------------------------------
        Public Property oQueryString() As String
            Get
                oQueryString = m_strQuery_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strQuery_O = Value
                Catch ex As Exception
                    m_strQuery_O = ""
                End Try
            End Set
        End Property

    End Class

End Namespace
