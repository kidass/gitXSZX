Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IGrswRcapDay
    '
    ' ���������� 
    '     grsw_rcap_day.aspxģ����ýӿڵĶ����봦��
    '----------------------------------------------------------------
    <Serializable()> Public Class IGrswRcapDay
        Inherits Xydc.Platform.BusinessFacade.ICallInterface

        '----------------------------------------------------------------
        'QueryString Parameters
        '----------------------------------------------------------------

        '----------------------------------------------------------------
        '�������
        '----------------------------------------------------------------
        Private m_strQueryString_I As String          '�����ַ���
        Private m_strCurrentDay_I As String           '��ǰ����

        '----------------------------------------------------------------
        '�������
        '----------------------------------------------------------------










        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            '��ʼ���������
            MyBase.iInterfaceType = ICallInterface.enumInterfaceType.InputAndOutput

            '��ʼ���������
            m_strQueryString_I = ""
            m_strCurrentDay_I = ""

            '��ʼ���������

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
        Public Overloads Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IGrswRcapDay)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub












        '----------------------------------------------------------------
        ' iQueryString����
        '----------------------------------------------------------------
        Public Property iQueryString() As String
            Get
                iQueryString = m_strQueryString_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strQueryString_I = Value
                Catch ex As Exception
                    m_strQueryString_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iCurrentDay����
        '----------------------------------------------------------------
        Public Property iCurrentDay() As String
            Get
                iCurrentDay = m_strCurrentDay_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strCurrentDay_I = Value
                Catch ex As Exception
                    m_strCurrentDay_I = ""
                End Try
            End Set
        End Property

    End Class

End Namespace
