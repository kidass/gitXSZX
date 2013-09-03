Imports System

Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.web
    ' 类名    ：MessageProcess
    '
    ' 功能描述：
    '     处理与用户的交互
    '----------------------------------------------------------------

    Public Class MessageProcess
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.web.MessageProcess)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub









        '----------------------------------------------------------------
        ' VB字符串转换为javascript字符串
        '----------------------------------------------------------------
        Private Function doConvertToJavaScriptString(ByVal strMessage As String) As String

            Try
                '反斜线
                strMessage = strMessage.Replace("\", "\\")

                '单引号
                strMessage = strMessage.Replace("'", "’")

                '行结束符
                strMessage = strMessage.Replace(vbLf, "")

                '回车符
                strMessage = strMessage.Replace(vbCr, "\n    ")

                doConvertToJavaScriptString = strMessage
            Catch ex As Exception
                doConvertToJavaScriptString = strMessage
            End Try

        End Function

        '----------------------------------------------------------------
        ' 用PopWindow方式显示错误信息
        '     objPopMessage ：PopMessage对象
        '     strMessage    ：要显示的信息
        '     strTitle      ：窗口标题
        '     strImagePath  ：图像目录相对调用目录的相对路径
        '----------------------------------------------------------------
        Public Sub doPopupMessage( _
            ByRef objPopMessage As Josco.Web.PopMessage, _
            ByVal strMessage As String, _
            ByVal strTitle As String, _
            ByVal strImagePath As String)

            Try
                '检查
                If objPopMessage Is Nothing Then Exit Try
                If strMessage Is Nothing Then strMessage = ""
                If strTitle Is Nothing Then strTitle = ""
                If strImagePath Is Nothing Then strImagePath = ""

                '正则化信息
                If strMessage.Length > 0 Then
                    strMessage = Me.doConvertToJavaScriptString(strMessage)
                End If

                '设置参数

                objPopMessage.PopupWindowType = Josco.Web.PopupWindowTypeEnum.Normal
                objPopMessage.Width = New System.Web.UI.WebControls.Unit(320)
                objPopMessage.Height = New System.Web.UI.WebControls.Unit(160)
                objPopMessage.Title = strTitle
                objPopMessage.Text = strMessage
                objPopMessage.ScrollAmount = 4
                objPopMessage.RelaControlId = ""
                objPopMessage.ExecPoint = 0

                '设置提示信息
                Dim strMsg As String
                strMsg = ""
                strMsg = strMsg + "<img src='" + strImagePath + "images/alert.ico' border='0' align='left'>"
                strMsg = strMsg + "<div style='POSITION:absolute; FONT-FAMILY:宋体; FONT-SIZE:9pt; COLOR:#000099; LEFT:60px; TOP:6px; WIDTH:248px; HEIGHT:160px; VERTICAL-ALIGN:baseline; TEXT-ALIGN:left'>"
                strMsg = strMsg + "<span>" + strMessage + "</span>"
                strMsg = strMsg + "</div>"
                objPopMessage.Message = strMsg

                '显示错误
                objPopMessage.Visible = True
            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' 用PopWindow方式显示警告信息
        '     objPopMessage ：PopMessage对象
        '     strMessage    ：要显示的信息
        '     strTitle      ：窗口标题
        '     strImagePath  ：图像目录相对调用目录的相对路径
        '----------------------------------------------------------------
        Public Sub doPopupWarning( _
            ByVal objPopMessage As Josco.Web.PopMessage, _
            ByVal strMessage As String, _
            ByVal strTitle As String, _
            ByVal strImagePath As String)

            Try
                '检查
                If objPopMessage Is Nothing Then Exit Try
                If strMessage Is Nothing Then strMessage = ""
                If strTitle Is Nothing Then strTitle = ""
                If strImagePath Is Nothing Then strImagePath = ""

                '正则化信息
                If strMessage.Length > 0 Then
                    strMessage = Me.doConvertToJavaScriptString(strMessage)
                End If

                '设置参数
                objPopMessage.PopupWindowType = Josco.Web.PopupWindowTypeEnum.Prompt
                objPopMessage.Width = New System.Web.UI.WebControls.Unit(320)
                objPopMessage.Height = New System.Web.UI.WebControls.Unit(160)
                objPopMessage.Title = strTitle
                objPopMessage.Text = strMessage
                objPopMessage.ScrollAmount = 4
                objPopMessage.RelaControlId = ""
                objPopMessage.ExecPoint = 0

                '设置提示信息
                Dim strMsg As String
                strMsg = ""
                strMsg = strMsg + "<img src='" + strImagePath + "images/warning.ico' border='0' align='left'>"
                strMsg = strMsg + "<div style='POSITION:absolute; FONT-FAMILY:宋体; FONT-SIZE:9pt; COLOR:#000099; LEFT:60px; TOP:6px; WIDTH:248px; HEIGHT:160px; VERTICAL-ALIGN:baseline; TEXT-ALIGN:left'>"
                strMsg = strMsg + "<span>" + strMessage + "</span>"
                strMsg = strMsg + "</div>"
                objPopMessage.Message = strMsg

                '显示错误
                objPopMessage.Visible = True

            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' 用alert对话框显示信息
        '     objPopMessage    ：PopMessage对象
        '     strMessage       ：要显示的信息
        '----------------------------------------------------------------
        Public Sub doAlertMessage( _
            ByVal objPopMessage As Josco.Web.PopMessage, _
            ByVal strMessage As String)

            Try
                '检查
                If objPopMessage Is Nothing Then Exit Try
                If strMessage Is Nothing Then strMessage = ""

                '正则化信息
                If strMessage.Length > 0 Then
                    strMessage = Me.doConvertToJavaScriptString(strMessage)
                End If

                '设置参数
                objPopMessage.PopupWindowType = Josco.Web.PopupWindowTypeEnum.Alert
                objPopMessage.RelaControlId = ""
                objPopMessage.ExecPoint = 0
                objPopMessage.Text = strMessage

                '显示
                objPopMessage.Visible = True

            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' 用inputbox输入框输入信息
        '     objPopMessage       ：PopMessage对象
        '     strMsg              ：要显示的信息
        '     strClickedControlId ：回答“是”后向该控件回发事件
        '     intPoint            ：执行本过程的程序执行点
        '----------------------------------------------------------------
        Public Sub doInputBox( _
            ByVal objPopMessage As Josco.Web.PopMessage, _
            ByVal strTitle As String, _
            ByVal strClickedControlId As String, _
            ByVal intPoint As Integer)

            Try
                '检查
                If objPopMessage Is Nothing Then Exit Try
                If strTitle Is Nothing Then strTitle = ""
                If strClickedControlId Is Nothing Then strClickedControlId = ""

                '正则化信息
                If strTitle.Length > 0 Then
                    strTitle = Me.doConvertToJavaScriptString(strTitle)
                End If

                '设置参数
                objPopMessage.PopupWindowType = Josco.Web.PopupWindowTypeEnum.Prompt
                objPopMessage.RelaControlId = strClickedControlId
                objPopMessage.ExecPoint = CType(intPoint, Short)
                objPopMessage.Text = strTitle

                '显示
                objPopMessage.Visible = True

            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' 用inputbox调试专用
        '     objPopMessage       ：PopMessage对象
        '     strMsg              ：要显示的信息
        '----------------------------------------------------------------
        Public Sub doInputBox( _
            ByVal objPopMessage As Josco.Web.PopMessage, _
            ByVal strTitle As String)

            Try
                '检查
                If objPopMessage Is Nothing Then Exit Try
                If strTitle Is Nothing Then strTitle = ""


                '正则化信息
                If strTitle.Length > 0 Then
                    strTitle = Me.doConvertToJavaScriptString(strTitle)
                End If

                '设置参数
                objPopMessage.PopupWindowType = Josco.Web.PopupWindowTypeEnum.Prompt
                objPopMessage.Text = strTitle

                '显示
                objPopMessage.Visible = True

            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' 用inputbox输入框输入信息
        '     objPopMessage       ：PopMessage对象
        '     strMsg              ：要显示的信息
        '     strClickedControlId ：回答“是”后向该控件回发事件
        '     intPoint            ：执行本过程的程序执行点
        '     strDefaultValue     ：缺省值
        '----------------------------------------------------------------
        Public Sub doInputBox( _
            ByVal objPopMessage As Josco.Web.PopMessage, _
            ByVal strTitle As String, _
            ByVal strClickedControlId As String, _
            ByVal intPoint As Integer, _
            ByVal strDefaultValue As String)

            Try
                '检查
                If objPopMessage Is Nothing Then Exit Try
                If strTitle Is Nothing Then strTitle = ""
                If strClickedControlId Is Nothing Then strClickedControlId = ""
                If strDefaultValue Is Nothing Then strDefaultValue = ""

                '正则化信息
                If strTitle.Length > 0 Then
                    strTitle = Me.doConvertToJavaScriptString(strTitle)
                End If

                '设置参数
                objPopMessage.PopupWindowType = Josco.Web.PopupWindowTypeEnum.Prompt
                objPopMessage.RelaControlId = strClickedControlId
                objPopMessage.ExecPoint = CType(intPoint, Short)
                objPopMessage.DefaultPromptValue = strDefaultValue
                objPopMessage.Text = strTitle

                '显示
                objPopMessage.Visible = True

            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' 用confirm对话框显示信息
        '     objPopMessage       ：PopMessage对象
        '     strMsg              ：要显示的信息
        '     strClickedControlId ：回答“是”后向该控件回发事件
        '     intPoint            ：执行本过程的程序执行点
        '----------------------------------------------------------------
        Public Sub doConfirmMessage( _
            ByVal objPopMessage As Josco.Web.PopMessage, _
            ByVal strTitle As String, _
            ByVal strClickedControlId As String, _
            ByVal intPoint As Integer)

            Try
                '检查
                If objPopMessage Is Nothing Then Exit Try
                If strTitle Is Nothing Then strTitle = ""
                If strClickedControlId Is Nothing Then strClickedControlId = ""

                '正则化信息
                If strTitle.Length > 0 Then
                    strTitle = Me.doConvertToJavaScriptString(strTitle)
                End If

                '设置参数
                objPopMessage.PopupWindowType = Josco.Web.PopupWindowTypeEnum.Confirm
                objPopMessage.RelaControlId = strClickedControlId
                objPopMessage.ExecPoint = CType(intPoint, Short)
                objPopMessage.CancelPostback = False
                objPopMessage.Text = strTitle

                '显示
                objPopMessage.Visible = True

            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' 用confirm对话框显示信息
        '     objPopMessage          ：PopMessage对象
        '     strMsg                 ：要显示的信息
        '     strClickedControlId    ：回答“是”后向该控件回发事件
        '     strClickedControlEvent ：回发事件名称及其参数
        '     intPoint               ：执行本过程的程序执行点
        '----------------------------------------------------------------
        Public Sub doConfirmMessage( _
            ByVal objPopMessage As Josco.Web.PopMessage, _
            ByVal strTitle As String, _
            ByVal strClickedControlId As String, _
            ByVal strClickedControlEvent As String, _
            ByVal intPoint As Integer)

            Try
                '检查
                If objPopMessage Is Nothing Then Exit Try
                If strTitle Is Nothing Then strTitle = ""
                If strClickedControlId Is Nothing Then strClickedControlId = ""
                If strClickedControlEvent Is Nothing Then strClickedControlEvent = ""

                '正则化信息
                If strTitle.Length > 0 Then
                    strTitle = Me.doConvertToJavaScriptString(strTitle)
                End If

                '设置参数
                objPopMessage.PopupWindowType = Josco.Web.PopupWindowTypeEnum.Confirm
                objPopMessage.RelaControlId = strClickedControlId
                objPopMessage.RelaControlEventParam = strClickedControlEvent
                objPopMessage.ExecPoint = CType(intPoint, Short)
                objPopMessage.CancelPostback = False
                objPopMessage.Text = strTitle

                '显示
                objPopMessage.Visible = True

            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' 用confirm对话框显示信息(Cancel也回发信息)
        '     objPopMessage       ：PopMessage对象
        '     strMsg              ：要显示的信息
        '     strClickedControlId ：回答“是”后向该控件回发事件
        '     intPoint            ：执行本过程的程序执行点
        '     blnCancelPostBack   ：true - Cancel也回发信息
        '----------------------------------------------------------------
        Public Sub doConfirmMessage( _
            ByVal objPopMessage As Josco.Web.PopMessage, _
            ByVal strTitle As String, _
            ByVal strClickedControlId As String, _
            ByVal intPoint As Integer, _
            ByVal blnCancelPostBack As Boolean)

            Try
                'Cancel不用回发
                If blnCancelPostBack = False Then
                    Me.doConfirmMessage(objPopMessage, strTitle, strClickedControlId, intPoint)
                    Exit Try
                End If

                '检查
                If objPopMessage Is Nothing Then Exit Try
                If strTitle Is Nothing Then strTitle = ""
                If strClickedControlId Is Nothing Then strClickedControlId = ""

                '正则化信息
                If strTitle.Length > 0 Then
                    strTitle = Me.doConvertToJavaScriptString(strTitle)
                End If

                '设置参数
                objPopMessage.PopupWindowType = Josco.Web.PopupWindowTypeEnum.Confirm
                objPopMessage.RelaControlId = strClickedControlId
                objPopMessage.ExecPoint = CType(intPoint, Short)
                objPopMessage.CancelPostback = True
                objPopMessage.Text = strTitle

                '显示
                objPopMessage.Visible = True

            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' 用confirm对话框显示信息(Cancel也回发信息)
        '     objPopMessage          ：PopMessage对象
        '     strMsg                 ：要显示的信息
        '     strClickedControlId    ：回答“是”后向该控件回发事件
        '     strClickedControlEvent ：回发事件名称及其参数
        '     intPoint               ：执行本过程的程序执行点
        '     blnCancelPostBack      ：true - Cancel也回发信息
        '----------------------------------------------------------------
        Public Sub doConfirmMessage( _
            ByVal objPopMessage As Josco.Web.PopMessage, _
            ByVal strTitle As String, _
            ByVal strClickedControlId As String, _
            ByVal strClickedControlEvent As String, _
            ByVal intPoint As Integer, _
            ByVal blnCancelPostBack As Boolean)

            Try
                'Cancel不用回发
                If blnCancelPostBack = False Then
                    Me.doConfirmMessage(objPopMessage, strTitle, strClickedControlId, strClickedControlEvent, intPoint)
                    Exit Try
                End If

                '检查
                If objPopMessage Is Nothing Then Exit Try
                If strTitle Is Nothing Then strTitle = ""
                If strClickedControlId Is Nothing Then strClickedControlId = ""
                If strClickedControlEvent Is Nothing Then strClickedControlEvent = ""

                '正则化信息
                If strTitle.Length > 0 Then
                    strTitle = Me.doConvertToJavaScriptString(strTitle)
                End If

                '设置参数
                objPopMessage.PopupWindowType = Josco.Web.PopupWindowTypeEnum.Confirm
                objPopMessage.RelaControlId = strClickedControlId
                objPopMessage.RelaControlEventParam = strClickedControlEvent
                objPopMessage.ExecPoint = CType(intPoint, Short)
                objPopMessage.CancelPostback = True
                objPopMessage.Text = strTitle

                '显示
                objPopMessage.Visible = True

            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' window.open(strUrl, strTarget, strFeatures, True)
        '     objPopMessage    ：PopMessage对象
        '     strUrl           ：要打开的Url
        '     strTarget        ：在strTarget中打开
        '     strFeatures      ：OpenUrl参数
        '----------------------------------------------------------------
        Public Sub doOpenUrl( _
            ByVal objPopMessage As Josco.Web.PopMessage, _
            ByVal strUrl As String, _
            ByVal strTarget As String, _
            ByVal strFeatures As String)

            Try
                '检查
                If objPopMessage Is Nothing Then Exit Try
                If strUrl Is Nothing Then strUrl = ""
                If strTarget Is Nothing Then strTarget = ""
                If strFeatures Is Nothing Then strFeatures = ""

                '设置参数
                objPopMessage.PopupWindowType = Josco.Web.PopupWindowTypeEnum.OpenUrl
                objPopMessage.Link = strUrl
                objPopMessage.LinkTarget = strTarget
                objPopMessage.Text = strFeatures

                '显示
                objPopMessage.Visible = True

            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' 执行客户端脚本
        '     objPopMessage    ：PopMessage对象
        '     strScript        ：要打开的Url
        '----------------------------------------------------------------
        Public Sub doExeClientScript( _
            ByVal objPopMessage As Josco.Web.PopMessage, _
            ByVal strScript As String)

            Try
                '检查
                If objPopMessage Is Nothing Then Exit Try
                If strScript Is Nothing Then strScript = ""

                '设置参数
                objPopMessage.PopupWindowType = Josco.Web.PopupWindowTypeEnum.ExeScript
                objPopMessage.Message = strScript

                '显示
                objPopMessage.Visible = True

            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' 重置PopMessage控件参数
        '     objPopMessage    ：PopMessage对象
        '----------------------------------------------------------------
        Public Sub doResetPopMessage(ByVal objPopMessage As Josco.Web.PopMessage)

            Try
                '检查
                If objPopMessage Is Nothing Then Exit Try

                '设置参数
                objPopMessage.PopupWindowType = Josco.Web.PopupWindowTypeEnum.Normal
                objPopMessage.RelaControlId = ""
                objPopMessage.ExecPoint = 0
                objPopMessage.Visible = False

            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' 显示confirm对话框后回发到原处理程序后，判断某段代码是否执行？
        '     objHttpRequest         ：当前HttpRequest
        '     strPopMessageControlId ：PopMessage对象的UniqueID
        '     intPoint               ：执行本过程的程序执行点
        '----------------------------------------------------------------
        Public Function isExecuteCode( _
            ByVal objHttpRequest As System.Web.HttpRequest, _
            ByVal strPopMessageControlId As String, _
            ByVal intPoint As Integer) As Boolean

            isExecuteCode = False
            Try
                Dim strPopupWindowType As String
                Dim strExecPoint As String

                '检查
                If objHttpRequest Is Nothing Then Exit Try

                '获取PopMessage的回发参数
                strPopupWindowType = objHttpRequest.Form(strPopMessageControlId + "_PopupWindowType")
                strExecPoint = objHttpRequest.Form(strPopMessageControlId + "_ExecPoint")

                '没有回发，说明未执行
                If strPopupWindowType Is Nothing Then GoTo normExit
                If strExecPoint Is Nothing Then GoTo normExit

                '是执行了confirm?
                If strPopupWindowType.Length > 0 Then strPopupWindowType = strPopupWindowType.Trim()
                If strPopupWindowType.ToLower() <> "Confirm".ToLower() And _
                    strPopupWindowType.ToLower() <> "Prompt".ToLower() Then
                    Exit Try
                End If

                '弹出confirm之前已经执行?
                If strExecPoint.Length > 0 Then strExecPoint = strExecPoint.Trim()
                Dim intTemp As Integer
                Try
                    intTemp = CType(strExecPoint, Integer)
                Catch ex As Exception
                    intTemp = -1
                End Try
                If (intPoint <= intTemp) Then
                    '已经执行了
                    Exit Try
                End If

normExit:
                '要执行
                isExecuteCode = True

            Catch ex As Exception
                isExecuteCode = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取通过PopMessage输入的值
        '     objHttpRequest         ：当前HttpRequest
        '     strPopMessageControlId ：PopMessage对象的UniqueID
        '     intPoint               ：执行本过程的程序执行点
        '----------------------------------------------------------------
        Public Function getInputBoxValue( _
            ByVal objHttpRequest As System.Web.HttpRequest, _
            ByVal strPopMessageControlId As String) As String

            getInputBoxValue = ""

            Try
                Dim strPopupWindowReturnValue As String
                Dim strPopupWindowType As String
                Dim strExecPoint As String

                '检查
                If objHttpRequest Is Nothing Then Exit Try

                '获取PopMessage的回发参数
                strPopupWindowReturnValue = objHttpRequest.Form(strPopMessageControlId + "_PopupWindowReturnValue")
                strPopupWindowType = objHttpRequest.Form(strPopMessageControlId + "_PopupWindowType")
                strExecPoint = objHttpRequest.Form(strPopMessageControlId + "_ExecPoint")

                '没有回发，说明未执行
                If strPopupWindowType Is Nothing Then Exit Try
                If strExecPoint Is Nothing Then Exit Try

                '是执行了Prompt?
                If strPopupWindowType.Length > 0 Then strPopupWindowType = strPopupWindowType.Trim()
                If strPopupWindowType.ToLower() <> "Prompt".ToLower() Then
                    Exit Try
                End If

                '要执行
                If strPopupWindowReturnValue Is Nothing Then strPopupWindowReturnValue = ""
                strPopupWindowReturnValue = strPopupWindowReturnValue.Trim()
                getInputBoxValue = strPopupWindowReturnValue

            Catch ex As Exception
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取通过confirm输入的值
        '     objHttpRequest         ：当前HttpRequest
        '     strPopMessageControlId ：PopMessage对象的UniqueID
        '     intPoint               ：执行本过程的程序执行点
        '----------------------------------------------------------------
        Public Function getConfirmBoxValue( _
            ByVal objHttpRequest As System.Web.HttpRequest, _
            ByVal strPopMessageControlId As String) As Boolean

            getConfirmBoxValue = False 'Cancel button

            Try
                Dim strPopupWindowReturnValue As String
                Dim strPopupWindowType As String
                Dim strExecPoint As String

                '检查
                If objHttpRequest Is Nothing Then Exit Try

                '获取PopMessage的回发参数
                strPopupWindowReturnValue = objHttpRequest.Form(strPopMessageControlId + "_PopupWindowReturnValue")
                strPopupWindowType = objHttpRequest.Form(strPopMessageControlId + "_PopupWindowType")
                strExecPoint = objHttpRequest.Form(strPopMessageControlId + "_ExecPoint")

                '没有回发，说明未执行
                If strPopupWindowType Is Nothing Then Exit Try
                If strExecPoint Is Nothing Then Exit Try

                '是执行了Confirm?
                If strPopupWindowType.Length > 0 Then strPopupWindowType = strPopupWindowType.Trim()
                If strPopupWindowType.ToLower() <> "Confirm".ToLower() Then
                    Exit Try
                End If

                '要执行
                If strPopupWindowReturnValue Is Nothing Then strPopupWindowReturnValue = ""
                strPopupWindowReturnValue = strPopupWindowReturnValue.Trim()
                Try
                    getConfirmBoxValue = CType(strPopupWindowReturnValue, Boolean)
                Catch ex As Exception
                End Try

            Catch ex As Exception
            End Try

        End Function

    End Class

End Namespace
