Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��ICallInterface
    '
    ' ���������� 
    '     ģ����ýӿڵĸ���
    '----------------------------------------------------------------
    <Serializable()> Public Class ICallInterface
        Implements IDisposable

        '�ӿڷ�ʽ����
        Public Enum enumInterfaceType
            InputOnly = 1        'ֻ�ṩ����ӿڣ��������Ϣ
            InputAndOutput = 2   '�ṩ���롢����ӿ�
        End Enum

        '----------------------------------------------------------------
        '˽�в���
        '----------------------------------------------------------------
        Private m_enumInterfaceType As enumInterfaceType   '�ӿ�����
        Private m_strSourceControlId As String             '����ÿؼ����뱾ģ��
        Private m_intExecutePoint As Integer               'm_strSourceControlId��������е��ñ�ģ��ĳ���ִ�е�
        Private m_strReturnUrl As String                   'ģ�鷵��ʱ��Url
        Private m_blnNewWindow As Boolean                  '��ʾ���µ����Ĵ�����








        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            '����ȱʡֵ
            m_enumInterfaceType = enumInterfaceType.InputAndOutput
            m_strSourceControlId = ""
            m_intExecutePoint = -1
            m_blnNewWindow = False
            m_strReturnUrl = ""

        End Sub

        '----------------------------------------------------------------
        ' ������������
        '----------------------------------------------------------------
        Public Overridable Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(True)
        End Sub

        '----------------------------------------------------------------
        ' ������������
        '----------------------------------------------------------------
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If (Not disposing) Then
                Exit Sub
            End If
        End Sub

        '----------------------------------------------------------------
        ' ��ȫ�ͷű�����Դ
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.ICallInterface)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub










        '----------------------------------------------------------------
        ' iInterfaceType����
        '----------------------------------------------------------------
        Public Property iInterfaceType() As enumInterfaceType
            Get
                iInterfaceType = m_enumInterfaceType
            End Get
            Set(ByVal Value As enumInterfaceType)
                Try
                    m_enumInterfaceType = Value
                Catch ex As Exception
                    m_enumInterfaceType = enumInterfaceType.InputOnly
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iReturnUrl����
        '----------------------------------------------------------------
        Public Property iReturnUrl() As String
            Get
                iReturnUrl = m_strReturnUrl
            End Get
            Set(ByVal Value As String)
                Try
                    m_strReturnUrl = Value
                Catch ex As Exception
                    m_strReturnUrl = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iSourceControlId����
        '----------------------------------------------------------------
        Public Property iSourceControlId() As String
            Get
                iSourceControlId = m_strSourceControlId
            End Get
            Set(ByVal Value As String)
                Try
                    m_strSourceControlId = Value
                Catch ex As Exception
                    m_strSourceControlId = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iExecutePoint����
        '----------------------------------------------------------------
        Public Property iExecutePoint() As Integer
            Get
                iExecutePoint = m_intExecutePoint
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intExecutePoint = Value
                Catch ex As Exception
                    m_intExecutePoint = -1
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iNewWindow����
        '----------------------------------------------------------------
        Public Property iNewWindow() As Boolean
            Get
                iNewWindow = m_blnNewWindow
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnNewWindow = Value
                Catch ex As Exception
                    m_blnNewWindow = False
                End Try
            End Set
        End Property








        '----------------------------------------------------------------
        ' getReturnUrl����
        ' ��strSessionName��strSessionValue���ӵ�returnUrl��
        ' querystring�У��������µ�Url
        '     objHttpServer    ��server
        '     strSessionName   ��Ҫ���ص�querystring��name
        '     strSessionValue  ��Ҫ���ص�querystring��value
        ' ����
        '                      ���ϳɺ��Url
        '----------------------------------------------------------------
        Public Function getReturnUrl( _
            ByVal objHttpServer As System.Web.HttpServerUtility, _
            ByVal strSessionName As String, _
            ByVal strSessionValue As String) As String

            Dim strUrl As String = ""

            Try
                If iReturnUrl.IndexOf("?") < 0 Then
                    strUrl = ""
                    strUrl += iReturnUrl
                    strUrl += "?"
                    strUrl += strSessionName
                    strUrl += "="
                    strUrl += objHttpServer.UrlEncode(strSessionValue)
                Else
                    strUrl = ""
                    strUrl += iReturnUrl
                    strUrl += "&"
                    strUrl += strSessionName
                    strUrl += "="
                    strUrl += objHttpServer.UrlEncode(strSessionValue)
                End If
            Catch ex As Exception
                strUrl = iReturnUrl()
            End Try

            getReturnUrl = strUrl

        End Function

    End Class

End Namespace
