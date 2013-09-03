Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：ICallInterface
    '
    ' 功能描述： 
    '     模块调用接口的父类
    '----------------------------------------------------------------
    <Serializable()> Public Class ICallInterface
        Implements IDisposable

        '接口方式参数
        Public Enum enumInterfaceType
            InputOnly = 1        '只提供输入接口，不输出信息
            InputAndOutput = 2   '提供输入、输出接口
        End Enum

        '----------------------------------------------------------------
        '私有参数
        '----------------------------------------------------------------
        Private m_enumInterfaceType As enumInterfaceType   '接口类型
        Private m_strSourceControlId As String             '点击该控件进入本模块
        Private m_intExecutePoint As Integer               'm_strSourceControlId处理程序中调用本模块的程序执行点
        Private m_strReturnUrl As String                   '模块返回时的Url
        Private m_blnNewWindow As Boolean                  '显示在新弹出的窗口中








        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            '设置缺省值
            m_enumInterfaceType = enumInterfaceType.InputAndOutput
            m_strSourceControlId = ""
            m_intExecutePoint = -1
            m_blnNewWindow = False
            m_strReturnUrl = ""

        End Sub

        '----------------------------------------------------------------
        ' 虚拟析构函数
        '----------------------------------------------------------------
        Public Overridable Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(True)
        End Sub

        '----------------------------------------------------------------
        ' 析构函数重载
        '----------------------------------------------------------------
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If (Not disposing) Then
                Exit Sub
            End If
        End Sub

        '----------------------------------------------------------------
        ' 安全释放本身资源
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
        ' iInterfaceType属性
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
        ' iReturnUrl属性
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
        ' iSourceControlId属性
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
        ' iExecutePoint属性
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
        ' iNewWindow属性
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
        ' getReturnUrl方法
        ' 将strSessionName、strSessionValue附加到returnUrl的
        ' querystring中，并返回新的Url
        '     objHttpServer    ：server
        '     strSessionName   ：要返回的querystring的name
        '     strSessionValue  ：要返回的querystring的value
        ' 返回
        '                      ：合成后的Url
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
