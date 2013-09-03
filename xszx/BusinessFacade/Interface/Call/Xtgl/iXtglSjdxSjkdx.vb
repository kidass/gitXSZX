Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IXtglSjdxSjkdx
    '
    ' ���������� 
    '     xtgl_sjdx_sjkdx.aspxģ����ýӿڵĶ����봦��
    '----------------------------------------------------------------
    <Serializable()> Public Class IXtglSjdxSjkdx
        Inherits Xydc.Platform.BusinessFacade.ICallInterface

        '----------------------------------------------------------------
        '�������
        '----------------------------------------------------------------
        Private m_objEditMode_I As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType  '�༭ģʽ
        Private m_strDXBS_I As String                     '�鿴���༭������ʱ�õĶ����ʶ
        Private m_strFWQMC_I As String                    '�鿴���༭������ʱ�õķ���������
        Private m_strSJKMC_I As String                    '�鿴���༭������ʱ�õ����ݿ�����
        Private m_strDXLX_I As String                     '�鿴���༭������ʱ�õĶ�������
        Private m_strDXMC_I As String                     '�鿴���༭������ʱ�õĶ�������

        '----------------------------------------------------------------
        '�������
        '----------------------------------------------------------------
        Private m_blnExitMode_O As Boolean                '���ط�ʽ��True-ȷ����False-ȡ��
        Private m_strDXBS_O As String                     '�������ڴ���Ķ����ʶ
        Private m_strFWQMC_O As String                    '�������ڴ���ķ���������
        Private m_strSJKMC_O As String                    '�������ڴ�������ݿ�����
        Private m_strDXLX_O As String                     '�������ڴ���Ķ�������
        Private m_strDXMC_O As String                     '�������ڴ���Ķ�������










        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            '��ʼ���������
            MyBase.iInterfaceType = ICallInterface.enumInterfaceType.InputAndOutput

            '��ʼ���������
            m_objEditMode_I = Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eSelect
            m_strDXBS_I = ""
            m_strFWQMC_I = ""
            m_strSJKMC_I = ""
            m_strDXLX_I = ""
            m_strDXMC_I = ""

            '��ʼ���������
            m_blnExitMode_O = False
            m_strDXBS_O = ""
            m_strFWQMC_O = ""
            m_strSJKMC_O = ""
            m_strDXLX_O = ""
            m_strDXMC_O = ""

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
        Public Overloads Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IXtglSjdxSjkdx)
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
        ' iDXBS����
        '----------------------------------------------------------------
        Public Property iDXBS() As String
            Get
                iDXBS = m_strDXBS_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strDXBS_I = Value
                Catch ex As Exception
                    m_strDXBS_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iFWQMC����
        '----------------------------------------------------------------
        Public Property iFWQMC() As String
            Get
                iFWQMC = m_strFWQMC_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strFWQMC_I = Value
                Catch ex As Exception
                    m_strFWQMC_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iSJKMC����
        '----------------------------------------------------------------
        Public Property iSJKMC() As String
            Get
                iSJKMC = m_strSJKMC_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strSJKMC_I = Value
                Catch ex As Exception
                    m_strSJKMC_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iDXLX����
        '----------------------------------------------------------------
        Public Property iDXLX() As String
            Get
                iDXLX = m_strDXLX_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strDXLX_I = Value
                Catch ex As Exception
                    m_strDXLX_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iDXMC����
        '----------------------------------------------------------------
        Public Property iDXMC() As String
            Get
                iDXMC = m_strDXMC_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strDXMC_I = Value
                Catch ex As Exception
                    m_strDXMC_I = ""
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
        ' oDXBS����
        '----------------------------------------------------------------
        Public Property oDXBS() As String
            Get
                oDXBS = m_strDXBS_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strDXBS_O = Value
                Catch ex As Exception
                    m_strDXBS_O = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' oFWQMC����
        '----------------------------------------------------------------
        Public Property oFWQMC() As String
            Get
                oFWQMC = m_strFWQMC_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strFWQMC_O = Value
                Catch ex As Exception
                    m_strFWQMC_O = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' oSJKMC����
        '----------------------------------------------------------------
        Public Property oSJKMC() As String
            Get
                oSJKMC = m_strSJKMC_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strSJKMC_O = Value
                Catch ex As Exception
                    m_strSJKMC_O = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' oDXLX����
        '----------------------------------------------------------------
        Public Property oDXLX() As String
            Get
                oDXLX = m_strDXLX_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strDXLX_O = Value
                Catch ex As Exception
                    m_strDXLX_O = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' oDXMC����
        '----------------------------------------------------------------
        Public Property oDXMC() As String
            Get
                oDXMC = m_strDXMC_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strDXMC_O = Value
                Catch ex As Exception
                    m_strDXMC_O = ""
                End Try
            End Set
        End Property

    End Class

End Namespace
