Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IModifyPwd
    '
    ' 功能描述： 
    '     modifypwd.aspx模块调用接口的定义与处理
    '----------------------------------------------------------------
    <Serializable()> Public Class IModifyPwd
        Inherits Xydc.Platform.BusinessFacade.ICallInterface

        '----------------------------------------------------------------
        '输入参数
        '----------------------------------------------------------------
        Private m_strUserId_I As String     '要更改密码的用户标识











        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            '初始化父类参数
            MyBase.iInterfaceType = ICallInterface.enumInterfaceType.InputOnly

            '初始化输入参数
            m_strUserId_I = ""

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
        Public Overloads Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IModifyPwd)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub











        '----------------------------------------------------------------
        ' iUserId属性
        '----------------------------------------------------------------
        Public Property iUserId() As String
            Get
                iUserId = m_strUserId_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strUserId_I = Value
                Catch ex As Exception
                    m_strUserId_I = ""
                End Try
            End Set
        End Property

    End Class

End Namespace
