Imports System.Web.Security

Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.web
    ' 类名    ：logout
    '
    ' 功能描述： 
    '     用户登录检查模块。
    '----------------------------------------------------------------
    Partial Public Class logout
        Inherits Xydc.Platform.web.PageBase

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            Dim objsystemEditWorkFlow As New Xydc.Platform.BusinessFacade.systemEditWorkFlow


            Dim objsystemCustomer As New Xydc.Platform.BusinessFacade.systemCustomer
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String
            Dim strUrl As String

            '预处理
            If MyBase.doPagePreprocess(False, False) = True Then
                Exit Sub
            End If

            '已经登录
            If Not (MyBase.Customer Is Nothing) Then
                '记录进出日志
                If objsystemCustomer.doWriteXitongJinchuRizhi(strErrMsg, MyBase.UserId, MyBase.UserPassword, Xydc.Platform.Common.Data.CustomerData.STATUS_LOGOUT, Request.UserHostAddress) = False Then
                    '可以不成功！
                End If
                '记录在线用户
                If objsystemCustomer.doDeleteZaixianYonghu(strErrMsg, MyBase.UserId, MyBase.UserPassword) = False Then
                    '可以不成功！
                End If

                '清除自己当前对所有公文的编辑封锁
                If objsystemEditWorkFlow.doUnLockAll(strErrMsg, MyBase.UserId, MyBase.UserPassword, MyBase.UserId) = False Then
                    GoTo errProc
                End If

                '清除会话数据 
                Session.Clear()
            End If

            Xydc.Platform.BusinessFacade.systemCustomer.SafeRelease(objsystemCustomer)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.BusinessFacade.systemCustomer.SafeRelease(objsystemCustomer)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

      
    End Class
End Namespace