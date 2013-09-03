Imports System

Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.web
    ' 类名    ：ControlProcess
    '
    ' 功能描述：
    '     执行对控件的一般操作处理：使能、键转译等
    '----------------------------------------------------------------
    Public Class ControlProcess
        Implements IDisposable








        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
        End Sub

        '----------------------------------------------------------------
        ' 虚拟析构函数
        '----------------------------------------------------------------
        Public Sub Dispose() Implements IDisposable.Dispose
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.web.ControlProcess)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub










        '----------------------------------------------------------------
        ' 转译System.Web.UI.WebControls.TextBox的键
        ' System.Web.UI.WebControls.TextBox 版本
        '----------------------------------------------------------------
        Public Sub doTranslateKey(ByVal objControl As System.Web.UI.WebControls.TextBox)
            Try
                objControl.Attributes("onKeyDown") = "TranslateKeys()"
            Catch ex As Exception
            End Try
        End Sub

        '----------------------------------------------------------------
        ' 转译System.Web.UI.HtmlControls.HtmlInputText的键
        ' System.Web.UI.HtmlControls.HtmlInputText 版本
        '----------------------------------------------------------------
        Public Sub doTranslateKey(ByVal objControl As System.Web.UI.HtmlControls.HtmlInputText)
            Try
                objControl.Attributes("onKeyDown") = "TranslateKeys()"
            Catch ex As Exception
            End Try
        End Sub

        '----------------------------------------------------------------
        ' 转译System.Web.UI.HtmlControls.HtmlInputFile的键
        ' System.Web.UI.HtmlControls.HtmlInputFile 版本
        '----------------------------------------------------------------
        Public Sub doTranslateKey(ByVal objControl As System.Web.UI.HtmlControls.HtmlInputFile)
            Try
                objControl.Attributes("onKeyDown") = "TranslateKeys()"
            Catch ex As Exception
            End Try
        End Sub

        '----------------------------------------------------------------
        ' 转译System.Web.UI.WebControls.DropDownList的键
        ' System.Web.UI.WebControls.DropDownList 版本
        '----------------------------------------------------------------
        Public Sub doTranslateKey(ByVal objControl As System.Web.UI.WebControls.DropDownList)
            Try
                objControl.Attributes("onKeyDown") = "TranslateKeys()"
            Catch ex As Exception
            End Try
        End Sub

        '----------------------------------------------------------------
        ' 转译System.Web.UI.HtmlControls.HtmlTextArea的键
        ' System.Web.UI.HtmlControls.HtmlTextArea 版本
        '----------------------------------------------------------------
        Public Sub doTranslateKey(ByVal objControl As System.Web.UI.HtmlControls.HtmlTextArea)
            Try
                objControl.Attributes("onKeyDown") = "TranslateKeys()"
            Catch ex As Exception
            End Try
        End Sub




        '----------------------------------------------------------------
        ' 使能System.Web.UI.WebControls.TextBox
        '----------------------------------------------------------------
        Public Sub doEnabledControl( _
            ByVal objControl As System.Web.UI.WebControls.TextBox, _
            ByVal blnEnabled As Boolean)
            If blnEnabled = True Then
                objControl.Attributes.Remove("readOnly")
            Else
                objControl.Attributes.Add("readOnly", "true")
            End If
        End Sub

        '----------------------------------------------------------------
        ' 使能System.Web.UI.HtmlControls.HtmlTextArea
        '----------------------------------------------------------------
        Public Sub doEnabledControl( _
            ByVal objControl As System.Web.UI.HtmlControls.HtmlTextArea, _
            ByVal blnEnabled As Boolean)
            Try
                If blnEnabled = True Then
                    objControl.Attributes.Remove("readOnly")
                Else
                    objControl.Attributes.Add("readOnly", "true")
                End If
            Catch ex As Exception
            End Try
        End Sub

        '----------------------------------------------------------------
        ' 使能System.Web.UI.HtmlControls.HtmlInputText
        '----------------------------------------------------------------
        Public Sub doEnabledControl( _
            ByVal objControl As System.Web.UI.HtmlControls.HtmlInputText, _
            ByVal blnEnabled As Boolean)
            Try
                If blnEnabled = True Then
                    objControl.Attributes.Remove("readOnly")
                Else
                    objControl.Attributes.Add("readOnly", "true")
                End If
            Catch ex As Exception
            End Try
        End Sub

        '----------------------------------------------------------------
        ' 使能System.Web.UI.HtmlControls.HtmlInputFile
        '----------------------------------------------------------------
        Public Sub doEnabledControl( _
            ByVal objControl As System.Web.UI.HtmlControls.HtmlInputFile, _
            ByVal blnEnabled As Boolean)
            Try
                If blnEnabled = True Then
                    objControl.Attributes.Remove("readOnly")
                Else
                    objControl.Attributes.Add("readOnly", "true")
                End If
            Catch ex As Exception
            End Try
        End Sub

        '----------------------------------------------------------------
        ' 使能System.Web.UI.WebControls.Button
        '----------------------------------------------------------------
        Public Sub doEnabledControl( _
            ByVal objControl As System.Web.UI.WebControls.Button, _
            ByVal blnEnabled As Boolean)
            Try
                objControl.Enabled = blnEnabled
            Catch ex As Exception
            End Try
        End Sub

        '----------------------------------------------------------------
        ' 使能System.Web.UI.WebControls.RadioButtonList
        '----------------------------------------------------------------
        Public Sub doEnabledControl( _
            ByVal objControl As System.Web.UI.WebControls.RadioButtonList, _
            ByVal blnEnabled As Boolean)
            Try
                objControl.Enabled = blnEnabled
            Catch ex As Exception
            End Try
        End Sub

        '----------------------------------------------------------------
        ' 使能System.Web.UI.WebControls.CheckBox
        '----------------------------------------------------------------
        Public Sub doEnabledControl( _
            ByVal objControl As System.Web.UI.WebControls.CheckBox, _
            ByVal blnEnabled As Boolean)
            Try
                objControl.Enabled = blnEnabled
            Catch ex As Exception
            End Try
        End Sub

        '----------------------------------------------------------------
        ' 使能System.Web.UI.WebControls.CheckBoxList
        '----------------------------------------------------------------
        Public Sub doEnabledControl( _
            ByVal objControl As System.Web.UI.WebControls.CheckBoxList, _
            ByVal blnEnabled As Boolean)
            Try
                objControl.Enabled = blnEnabled
            Catch ex As Exception
            End Try
        End Sub

        '----------------------------------------------------------------
        ' 使能System.Web.UI.WebControls.DropDownList
        '----------------------------------------------------------------
        Public Sub doEnabledControl( _
            ByVal objControl As System.Web.UI.WebControls.DropDownList, _
            ByVal blnEnabled As Boolean)
            Try
                objControl.Enabled = blnEnabled
            Catch ex As Exception
            End Try
        End Sub

        '----------------------------------------------------------------
        ' 使能System.Web.UI.WebControls.LinkButton
        '----------------------------------------------------------------
        Public Sub doEnabledControl( _
            ByVal objControl As System.Web.UI.WebControls.LinkButton, _
            ByVal blnEnabled As Boolean)
            Try
                objControl.Enabled = blnEnabled
            Catch ex As Exception
            End Try
        End Sub

    End Class

End Namespace