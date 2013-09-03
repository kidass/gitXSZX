Imports System.Web.Security
Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' 命名空间： Xydc.Platform.web
    ' 类名    ：info_button0
    '
    ' 功能描述： 
    '   　命令区模块
    '----------------------------------------------------------------

    Partial Public Class lockuser
        Inherits Xydc.Platform.web.PageBase

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String
            Dim strUrl As String

            '获取用户标识
            Dim strUserId As String
            strUserId = Request.QueryString("UserId")
            If strUserId Is Nothing Then strUserId = ""
            strUserId = strUserId.Trim

            '锁定用户
            If MyBase.doLockAccount(strErrMsg, strUserId) = False Then
                GoTo errProc
            End If

            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

    End Class
End Namespace