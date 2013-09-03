Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IGgxxNbltLtzyInfo
    '
    ' ���������� 
    '     ggxx_nblt_ltzy_info.aspxģ����ýӿڵĶ����봦��
    '----------------------------------------------------------------
    <Serializable()> Public Class IGgxxNbltLtzyInfo
        Inherits Xydc.Platform.BusinessFacade.ICallInterface

        '----------------------------------------------------------------
        'QueryString Parameters
        '----------------------------------------------------------------
        Public Const qspJLBH As String = "JLBH"

        '----------------------------------------------------------------
        '�������
        '----------------------------------------------------------------
        Private m_objEditMode_I As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType  '�༭ģʽ
        Private m_strJLBH_I As String                                                        '�޸ġ�����ʱ�Ľ������
        Private m_strSJBH_I As String                                                        '���ӡ�����ʱ���ϼ����

        '----------------------------------------------------------------
        '�������
        '----------------------------------------------------------------
        Private m_blnExitMode_O As Boolean                 '���ط�ʽ��True-ȷ����False-ȡ��
        Private m_strJLBH_O As String                      '�������ڴ���Ľ������









        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            '��ʼ���������
            MyBase.iInterfaceType = ICallInterface.enumInterfaceType.InputAndOutput

            '��ʼ���������
            m_objEditMode_I = Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eSelect
            m_strJLBH_I = ""
            m_strSJBH_I = ""

            '��ʼ���������
            m_blnExitMode_O = False
            m_strJLBH_O = ""

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
        Public Overloads Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IGgxxNbltLtzyInfo)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub













        '----------------------------------------------------------------
        ' iEditMode����
        '----------------------------------------------------------------
        Public Property iEditMode() As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType
            Get
                iEditMode = m_objEditMode_I
            End Get
            Set(ByVal Value As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType)
                Try
                    m_objEditMode_I = Value
                Catch ex As Exception
                    m_objEditMode_I = Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eSelect
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iJLBH����
        '----------------------------------------------------------------
        Public Property iJLBH() As String
            Get
                iJLBH = m_strJLBH_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strJLBH_I = Value
                Catch ex As Exception
                    m_strJLBH_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iSJBH����
        '----------------------------------------------------------------
        Public Property iSJBH() As String
            Get
                iSJBH = m_strSJBH_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strSJBH_I = Value
                Catch ex As Exception
                    m_strSJBH_I = ""
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
        ' oJLBH����
        '----------------------------------------------------------------
        Public Property oJLBH() As String
            Get
                oJLBH = m_strJLBH_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strJLBH_O = Value
                Catch ex As Exception
                    m_strJLBH_O = ""
                End Try
            End Set
        End Property

    End Class

End Namespace
