Namespace Xydc.Platform.web
    '----------------------------------------------------------------
    ' 命名空间： Xydc.Platform.web
    ' 类名    ：areaFoot
    '
    ' 功能描述： 
    '   　页脚区模块
    '----------------------------------------------------------------

    Partial Public Class areaFoot
        Inherits Xydc.Platform.web.PageBase

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Try
                Me.lblFootMessage.Text = ""
                Me.lblFootMessage.Text += "建议在1024×768分辨率和小字体环境下使用"
            Catch ex As Exception
            End Try
        End Sub

    End Class
End Namespace