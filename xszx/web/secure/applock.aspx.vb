Imports System.Web.Security

Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.web
    ' 类名    ：applock
    '
    ' 功能描述： 
    '   　处理个人的运行参数配置
    '----------------------------------------------------------------
    Partial Public Class applock
        Inherits Xydc.Platform.web.PageBase

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String
            Dim strUrl As String

            '预处理
            If MyBase.doPagePreprocess(False, False) = True Then
                Exit Sub
            End If

            '设置锁定标记
            MyBase.AppLocked = True

            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

    End Class
End Namespace