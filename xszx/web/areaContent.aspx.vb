Namespace Xydc.Platform.web
    '----------------------------------------------------------------
    ' 命名空间： Xydc.Platform.web
    ' 类名    ：areaContent
    '
    ' 功能描述： 
    '   　启动时显示的内容区模块
    '----------------------------------------------------------------
    Partial Public Class areaContent
        Inherits Xydc.Platform.web.PageBase

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            '同时传递QueryString信息
            If Request.QueryString.Count < 1 Then
                Response.Redirect("./secure/main.aspx")
            Else
                Dim objUri As New System.Uri(Request.ApplicationPath)
                Dim strQuery As String = objUri.Query
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objUri)
                Response.Redirect("./secure/main.aspx" + strQuery)
            End If
        End Sub

    End Class
End Namespace