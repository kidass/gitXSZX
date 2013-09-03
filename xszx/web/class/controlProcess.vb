Imports System

Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.web
    ' ����    ��ControlProcess
    '
    ' ����������
    '     ִ�жԿؼ���һ���������ʹ�ܡ���ת���
    '----------------------------------------------------------------
    Public Class ControlProcess
        Implements IDisposable








        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
        End Sub

        '----------------------------------------------------------------
        ' ������������
        '----------------------------------------------------------------
        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(True)
        End Sub

        '----------------------------------------------------------------
        ' ������������
        '----------------------------------------------------------------
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If (Not disposing) Then
                Exit Sub
            End If
        End Sub

        '----------------------------------------------------------------
        ' ��ȫ�ͷű�����Դ
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
        ' ת��System.Web.UI.WebControls.TextBox�ļ�
        ' System.Web.UI.WebControls.TextBox �汾
        '----------------------------------------------------------------
        Public Sub doTranslateKey(ByVal objControl As System.Web.UI.WebControls.TextBox)
            Try
                objControl.Attributes("onKeyDown") = "TranslateKeys()"
            Catch ex As Exception
            End Try
        End Sub

        '----------------------------------------------------------------
        ' ת��System.Web.UI.HtmlControls.HtmlInputText�ļ�
        ' System.Web.UI.HtmlControls.HtmlInputText �汾
        '----------------------------------------------------------------
        Public Sub doTranslateKey(ByVal objControl As System.Web.UI.HtmlControls.HtmlInputText)
            Try
                objControl.Attributes("onKeyDown") = "TranslateKeys()"
            Catch ex As Exception
            End Try
        End Sub

        '----------------------------------------------------------------
        ' ת��System.Web.UI.HtmlControls.HtmlInputFile�ļ�
        ' System.Web.UI.HtmlControls.HtmlInputFile �汾
        '----------------------------------------------------------------
        Public Sub doTranslateKey(ByVal objControl As System.Web.UI.HtmlControls.HtmlInputFile)
            Try
                objControl.Attributes("onKeyDown") = "TranslateKeys()"
            Catch ex As Exception
            End Try
        End Sub

        '----------------------------------------------------------------
        ' ת��System.Web.UI.WebControls.DropDownList�ļ�
        ' System.Web.UI.WebControls.DropDownList �汾
        '----------------------------------------------------------------
        Public Sub doTranslateKey(ByVal objControl As System.Web.UI.WebControls.DropDownList)
            Try
                objControl.Attributes("onKeyDown") = "TranslateKeys()"
            Catch ex As Exception
            End Try
        End Sub

        '----------------------------------------------------------------
        ' ת��System.Web.UI.HtmlControls.HtmlTextArea�ļ�
        ' System.Web.UI.HtmlControls.HtmlTextArea �汾
        '----------------------------------------------------------------
        Public Sub doTranslateKey(ByVal objControl As System.Web.UI.HtmlControls.HtmlTextArea)
            Try
                objControl.Attributes("onKeyDown") = "TranslateKeys()"
            Catch ex As Exception
            End Try
        End Sub




        '----------------------------------------------------------------
        ' ʹ��System.Web.UI.WebControls.TextBox
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
        ' ʹ��System.Web.UI.HtmlControls.HtmlTextArea
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
        ' ʹ��System.Web.UI.HtmlControls.HtmlInputText
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
        ' ʹ��System.Web.UI.HtmlControls.HtmlInputFile
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
        ' ʹ��System.Web.UI.WebControls.Button
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
        ' ʹ��System.Web.UI.WebControls.RadioButtonList
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
        ' ʹ��System.Web.UI.WebControls.CheckBox
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
        ' ʹ��System.Web.UI.WebControls.CheckBoxList
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
        ' ʹ��System.Web.UI.WebControls.DropDownList
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
        ' ʹ��System.Web.UI.WebControls.LinkButton
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