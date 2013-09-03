Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IGrswRcapInfo
    '
    ' ���������� 
    '     grsw_rcap_info.aspxģ����ýӿڵĶ����봦��
    '----------------------------------------------------------------
    <Serializable()> Public Class IGrswRcapInfo
        Inherits Xydc.Platform.BusinessFacade.ICallInterface

        '----------------------------------------------------------------
        'QueryString Parameters
        '----------------------------------------------------------------
        Public Const qspBH As String = "BH"

        '----------------------------------------------------------------
        '�������
        '----------------------------------------------------------------
        Private m_objEditMode_I As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType  '�༭ģʽ
        Private m_strBH_I As String                                                          '�޸ġ�����ʱ�ı��
        Private m_strSYZ_I As String                                                         '���ӡ�����ʱ��������
        Private m_strKSSJ_I As String                                                        '���ӡ�����ʱ�ĳ�ʼʱ��

        '----------------------------------------------------------------
        '�������
        '----------------------------------------------------------------
        Private m_blnExitMode_O As Boolean                 '���ط�ʽ��True-ȷ����False-ȡ��
        Private m_strBH_O As String                        '�������ڴ���ı��









        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            '��ʼ���������
            MyBase.iInterfaceType = ICallInterface.enumInterfaceType.InputAndOutput

            '��ʼ���������
            m_objEditMode_I = Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eSelect
            m_strBH_I = ""
            m_strSYZ_I = ""
            m_strKSSJ_I = ""

            '��ʼ���������
            m_blnExitMode_O = False
            m_strBH_O = ""

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
        Public Overloads Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IGrswRcapInfo)
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
        ' iBH����
        '----------------------------------------------------------------
        Public Property iBH() As String
            Get
                iBH = m_strBH_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strBH_I = Value
                Catch ex As Exception
                    m_strBH_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iSYZ����
        '----------------------------------------------------------------
        Public Property iSYZ() As String
            Get
                iSYZ = m_strSYZ_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strSYZ_I = Value
                Catch ex As Exception
                    m_strSYZ_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iKSSJ����
        '----------------------------------------------------------------
        Public Property iKSSJ() As String
            Get
                iKSSJ = m_strKSSJ_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strKSSJ_I = Value
                Catch ex As Exception
                    m_strKSSJ_I = ""
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
        ' oBH����
        '----------------------------------------------------------------
        Public Property oBH() As String
            Get
                oBH = m_strBH_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strBH_O = Value
                Catch ex As Exception
                    m_strBH_O = ""
                End Try
            End Set
        End Property

    End Class

End Namespace
