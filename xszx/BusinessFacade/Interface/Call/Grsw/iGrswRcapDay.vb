Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IGrswRcapDay
    '
    ' 功能描述： 
    '     grsw_rcap_day.aspx模块调用接口的定义与处理
    '----------------------------------------------------------------
    <Serializable()> Public Class IGrswRcapDay
        Inherits Xydc.Platform.BusinessFacade.ICallInterface

        '----------------------------------------------------------------
        'QueryString Parameters
        '----------------------------------------------------------------

        '----------------------------------------------------------------
        '输入参数
        '----------------------------------------------------------------
        Private m_strQueryString_I As String          '搜索字符串
        Private m_strCurrentDay_I As String           '当前日期

        '----------------------------------------------------------------
        '输出参数
        '----------------------------------------------------------------










        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            '初始化父类参数
            MyBase.iInterfaceType = ICallInterface.enumInterfaceType.InputAndOutput

            '初始化输入参数
            m_strQueryString_I = ""
            m_strCurrentDay_I = ""

            '初始化输出参数

        End Sub

        '----------------------------------------------------------------
        ' 重载父类的析构函数
        '----------------------------------------------------------------
        Public Overloads Sub Dispose()
            MyBase.Dispose()
            Dispose(True)
        End Sub

        '----------------------------------------------------------------
        ' 释放本身资源
        '----------------------------------------------------------------
        Protected Overloads Sub Dispose(ByVal disposing As Boolean)
            If (Not disposing) Then
                Exit Sub
            End If
        End Sub

        '----------------------------------------------------------------
        ' 安全释放本身资源
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
        ' iQueryString属性
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
        ' iCurrentDay属性
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
