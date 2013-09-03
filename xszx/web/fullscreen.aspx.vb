Imports System.Web.Security

Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.web
    ' 类名　　：fullscreen
    ' 
    ' 调用性质：
    '     独立模块
    '
    ' 功能描述：
    '     设置窗口的全屏或正常显示状态
    '
    ' QueryString参数：
    '     FullScreen：= 0 正常，=1 全屏
    '----------------------------------------------------------------
    Partial Public Class fullscreen
        Inherits Xydc.Platform.web.PageBase

        Private Const m_cstrParamFullScreen As String = "FullScreen"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            '获取QueryString参数
            Dim strFullScreen As String
            Try
                strFullScreen = CType(Request.QueryString(Me.m_cstrParamFullScreen), String)
            Catch ex As Exception
                strFullScreen = "0"
            End Try

            If strFullScreen = "1" Then
                MyBase.FullScreen = True
            Else
                MyBase.FullScreen = False
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