Imports System

Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.web
    ' ����    ��MessageProcess
    '
    ' ����������
    '     �������û��Ľ���
    '----------------------------------------------------------------

    Public Class MessageProcess
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
        ' VB�ַ���ת��Ϊjavascript�ַ���
        '----------------------------------------------------------------
        Private Function doConvertToJavaScriptString(ByVal strMessage As String) As String

            Try
                '��б��
                strMessage = strMessage.Replace("\", "\\")

                '������
                strMessage = strMessage.Replace("'", "��")

                '�н�����
                strMessage = strMessage.Replace(vbLf, "")

                '�س���
                strMessage = strMessage.Replace(vbCr, "\n    ")

                doConvertToJavaScriptString = strMessage
            Catch ex As Exception
                doConvertToJavaScriptString = strMessage
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��PopWindow��ʽ��ʾ������Ϣ
        '     objPopMessage ��PopMessage����
        '     strMessage    ��Ҫ��ʾ����Ϣ
        '     strTitle      �����ڱ���
        '     strImagePath  ��ͼ��Ŀ¼��Ե���Ŀ¼�����·��
        '----------------------------------------------------------------
        Public Sub doPopupMessage( _
            ByRef objPopMessage As Josco.Web.PopMessage, _
            ByVal strMessage As String, _
            ByVal strTitle As String, _
            ByVal strImagePath As String)

            Try
                '���
                If objPopMessage Is Nothing Then Exit Try
                If strMessage Is Nothing Then strMessage = ""
                If strTitle Is Nothing Then strTitle = ""
                If strImagePath Is Nothing Then strImagePath = ""

                '������Ϣ
                If strMessage.Length > 0 Then
                    strMessage = Me.doConvertToJavaScriptString(strMessage)
                End If

                '���ò���

                objPopMessage.PopupWindowType = Josco.Web.PopupWindowTypeEnum.Normal
                objPopMessage.Width = New System.Web.UI.WebControls.Unit(320)
                objPopMessage.Height = New System.Web.UI.WebControls.Unit(160)
                objPopMessage.Title = strTitle
                objPopMessage.Text = strMessage
                objPopMessage.ScrollAmount = 4
                objPopMessage.RelaControlId = ""
                objPopMessage.ExecPoint = 0

                '������ʾ��Ϣ
                Dim strMsg As String
                strMsg = ""
                strMsg = strMsg + "<img src='" + strImagePath + "images/alert.ico' border='0' align='left'>"
                strMsg = strMsg + "<div style='POSITION:absolute; FONT-FAMILY:����; FONT-SIZE:9pt; COLOR:#000099; LEFT:60px; TOP:6px; WIDTH:248px; HEIGHT:160px; VERTICAL-ALIGN:baseline; TEXT-ALIGN:left'>"
                strMsg = strMsg + "<span>" + strMessage + "</span>"
                strMsg = strMsg + "</div>"
                objPopMessage.Message = strMsg

                '��ʾ����
                objPopMessage.Visible = True
            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' ��PopWindow��ʽ��ʾ������Ϣ
        '     objPopMessage ��PopMessage����
        '     strMessage    ��Ҫ��ʾ����Ϣ
        '     strTitle      �����ڱ���
        '     strImagePath  ��ͼ��Ŀ¼��Ե���Ŀ¼�����·��
        '----------------------------------------------------------------
        Public Sub doPopupWarning( _
            ByVal objPopMessage As Josco.Web.PopMessage, _
            ByVal strMessage As String, _
            ByVal strTitle As String, _
            ByVal strImagePath As String)

            Try
                '���
                If objPopMessage Is Nothing Then Exit Try
                If strMessage Is Nothing Then strMessage = ""
                If strTitle Is Nothing Then strTitle = ""
                If strImagePath Is Nothing Then strImagePath = ""

                '������Ϣ
                If strMessage.Length > 0 Then
                    strMessage = Me.doConvertToJavaScriptString(strMessage)
                End If

                '���ò���
                objPopMessage.PopupWindowType = Josco.Web.PopupWindowTypeEnum.Prompt
                objPopMessage.Width = New System.Web.UI.WebControls.Unit(320)
                objPopMessage.Height = New System.Web.UI.WebControls.Unit(160)
                objPopMessage.Title = strTitle
                objPopMessage.Text = strMessage
                objPopMessage.ScrollAmount = 4
                objPopMessage.RelaControlId = ""
                objPopMessage.ExecPoint = 0

                '������ʾ��Ϣ
                Dim strMsg As String
                strMsg = ""
                strMsg = strMsg + "<img src='" + strImagePath + "images/warning.ico' border='0' align='left'>"
                strMsg = strMsg + "<div style='POSITION:absolute; FONT-FAMILY:����; FONT-SIZE:9pt; COLOR:#000099; LEFT:60px; TOP:6px; WIDTH:248px; HEIGHT:160px; VERTICAL-ALIGN:baseline; TEXT-ALIGN:left'>"
                strMsg = strMsg + "<span>" + strMessage + "</span>"
                strMsg = strMsg + "</div>"
                objPopMessage.Message = strMsg

                '��ʾ����
                objPopMessage.Visible = True

            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' ��alert�Ի�����ʾ��Ϣ
        '     objPopMessage    ��PopMessage����
        '     strMessage       ��Ҫ��ʾ����Ϣ
        '----------------------------------------------------------------
        Public Sub doAlertMessage( _
            ByVal objPopMessage As Josco.Web.PopMessage, _
            ByVal strMessage As String)

            Try
                '���
                If objPopMessage Is Nothing Then Exit Try
                If strMessage Is Nothing Then strMessage = ""

                '������Ϣ
                If strMessage.Length > 0 Then
                    strMessage = Me.doConvertToJavaScriptString(strMessage)
                End If

                '���ò���
                objPopMessage.PopupWindowType = Josco.Web.PopupWindowTypeEnum.Alert
                objPopMessage.RelaControlId = ""
                objPopMessage.ExecPoint = 0
                objPopMessage.Text = strMessage

                '��ʾ
                objPopMessage.Visible = True

            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' ��inputbox�����������Ϣ
        '     objPopMessage       ��PopMessage����
        '     strMsg              ��Ҫ��ʾ����Ϣ
        '     strClickedControlId ���ش��ǡ�����ÿؼ��ط��¼�
        '     intPoint            ��ִ�б����̵ĳ���ִ�е�
        '----------------------------------------------------------------
        Public Sub doInputBox( _
            ByVal objPopMessage As Josco.Web.PopMessage, _
            ByVal strTitle As String, _
            ByVal strClickedControlId As String, _
            ByVal intPoint As Integer)

            Try
                '���
                If objPopMessage Is Nothing Then Exit Try
                If strTitle Is Nothing Then strTitle = ""
                If strClickedControlId Is Nothing Then strClickedControlId = ""

                '������Ϣ
                If strTitle.Length > 0 Then
                    strTitle = Me.doConvertToJavaScriptString(strTitle)
                End If

                '���ò���
                objPopMessage.PopupWindowType = Josco.Web.PopupWindowTypeEnum.Prompt
                objPopMessage.RelaControlId = strClickedControlId
                objPopMessage.ExecPoint = CType(intPoint, Short)
                objPopMessage.Text = strTitle

                '��ʾ
                objPopMessage.Visible = True

            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' ��inputbox����ר��
        '     objPopMessage       ��PopMessage����
        '     strMsg              ��Ҫ��ʾ����Ϣ
        '----------------------------------------------------------------
        Public Sub doInputBox( _
            ByVal objPopMessage As Josco.Web.PopMessage, _
            ByVal strTitle As String)

            Try
                '���
                If objPopMessage Is Nothing Then Exit Try
                If strTitle Is Nothing Then strTitle = ""


                '������Ϣ
                If strTitle.Length > 0 Then
                    strTitle = Me.doConvertToJavaScriptString(strTitle)
                End If

                '���ò���
                objPopMessage.PopupWindowType = Josco.Web.PopupWindowTypeEnum.Prompt
                objPopMessage.Text = strTitle

                '��ʾ
                objPopMessage.Visible = True

            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' ��inputbox�����������Ϣ
        '     objPopMessage       ��PopMessage����
        '     strMsg              ��Ҫ��ʾ����Ϣ
        '     strClickedControlId ���ش��ǡ�����ÿؼ��ط��¼�
        '     intPoint            ��ִ�б����̵ĳ���ִ�е�
        '     strDefaultValue     ��ȱʡֵ
        '----------------------------------------------------------------
        Public Sub doInputBox( _
            ByVal objPopMessage As Josco.Web.PopMessage, _
            ByVal strTitle As String, _
            ByVal strClickedControlId As String, _
            ByVal intPoint As Integer, _
            ByVal strDefaultValue As String)

            Try
                '���
                If objPopMessage Is Nothing Then Exit Try
                If strTitle Is Nothing Then strTitle = ""
                If strClickedControlId Is Nothing Then strClickedControlId = ""
                If strDefaultValue Is Nothing Then strDefaultValue = ""

                '������Ϣ
                If strTitle.Length > 0 Then
                    strTitle = Me.doConvertToJavaScriptString(strTitle)
                End If

                '���ò���
                objPopMessage.PopupWindowType = Josco.Web.PopupWindowTypeEnum.Prompt
                objPopMessage.RelaControlId = strClickedControlId
                objPopMessage.ExecPoint = CType(intPoint, Short)
                objPopMessage.DefaultPromptValue = strDefaultValue
                objPopMessage.Text = strTitle

                '��ʾ
                objPopMessage.Visible = True

            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' ��confirm�Ի�����ʾ��Ϣ
        '     objPopMessage       ��PopMessage����
        '     strMsg              ��Ҫ��ʾ����Ϣ
        '     strClickedControlId ���ش��ǡ�����ÿؼ��ط��¼�
        '     intPoint            ��ִ�б����̵ĳ���ִ�е�
        '----------------------------------------------------------------
        Public Sub doConfirmMessage( _
            ByVal objPopMessage As Josco.Web.PopMessage, _
            ByVal strTitle As String, _
            ByVal strClickedControlId As String, _
            ByVal intPoint As Integer)

            Try
                '���
                If objPopMessage Is Nothing Then Exit Try
                If strTitle Is Nothing Then strTitle = ""
                If strClickedControlId Is Nothing Then strClickedControlId = ""

                '������Ϣ
                If strTitle.Length > 0 Then
                    strTitle = Me.doConvertToJavaScriptString(strTitle)
                End If

                '���ò���
                objPopMessage.PopupWindowType = Josco.Web.PopupWindowTypeEnum.Confirm
                objPopMessage.RelaControlId = strClickedControlId
                objPopMessage.ExecPoint = CType(intPoint, Short)
                objPopMessage.CancelPostback = False
                objPopMessage.Text = strTitle

                '��ʾ
                objPopMessage.Visible = True

            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' ��confirm�Ի�����ʾ��Ϣ
        '     objPopMessage          ��PopMessage����
        '     strMsg                 ��Ҫ��ʾ����Ϣ
        '     strClickedControlId    ���ش��ǡ�����ÿؼ��ط��¼�
        '     strClickedControlEvent ���ط��¼����Ƽ������
        '     intPoint               ��ִ�б����̵ĳ���ִ�е�
        '----------------------------------------------------------------
        Public Sub doConfirmMessage( _
            ByVal objPopMessage As Josco.Web.PopMessage, _
            ByVal strTitle As String, _
            ByVal strClickedControlId As String, _
            ByVal strClickedControlEvent As String, _
            ByVal intPoint As Integer)

            Try
                '���
                If objPopMessage Is Nothing Then Exit Try
                If strTitle Is Nothing Then strTitle = ""
                If strClickedControlId Is Nothing Then strClickedControlId = ""
                If strClickedControlEvent Is Nothing Then strClickedControlEvent = ""

                '������Ϣ
                If strTitle.Length > 0 Then
                    strTitle = Me.doConvertToJavaScriptString(strTitle)
                End If

                '���ò���
                objPopMessage.PopupWindowType = Josco.Web.PopupWindowTypeEnum.Confirm
                objPopMessage.RelaControlId = strClickedControlId
                objPopMessage.RelaControlEventParam = strClickedControlEvent
                objPopMessage.ExecPoint = CType(intPoint, Short)
                objPopMessage.CancelPostback = False
                objPopMessage.Text = strTitle

                '��ʾ
                objPopMessage.Visible = True

            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' ��confirm�Ի�����ʾ��Ϣ(CancelҲ�ط���Ϣ)
        '     objPopMessage       ��PopMessage����
        '     strMsg              ��Ҫ��ʾ����Ϣ
        '     strClickedControlId ���ش��ǡ�����ÿؼ��ط��¼�
        '     intPoint            ��ִ�б����̵ĳ���ִ�е�
        '     blnCancelPostBack   ��true - CancelҲ�ط���Ϣ
        '----------------------------------------------------------------
        Public Sub doConfirmMessage( _
            ByVal objPopMessage As Josco.Web.PopMessage, _
            ByVal strTitle As String, _
            ByVal strClickedControlId As String, _
            ByVal intPoint As Integer, _
            ByVal blnCancelPostBack As Boolean)

            Try
                'Cancel���ûط�
                If blnCancelPostBack = False Then
                    Me.doConfirmMessage(objPopMessage, strTitle, strClickedControlId, intPoint)
                    Exit Try
                End If

                '���
                If objPopMessage Is Nothing Then Exit Try
                If strTitle Is Nothing Then strTitle = ""
                If strClickedControlId Is Nothing Then strClickedControlId = ""

                '������Ϣ
                If strTitle.Length > 0 Then
                    strTitle = Me.doConvertToJavaScriptString(strTitle)
                End If

                '���ò���
                objPopMessage.PopupWindowType = Josco.Web.PopupWindowTypeEnum.Confirm
                objPopMessage.RelaControlId = strClickedControlId
                objPopMessage.ExecPoint = CType(intPoint, Short)
                objPopMessage.CancelPostback = True
                objPopMessage.Text = strTitle

                '��ʾ
                objPopMessage.Visible = True

            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' ��confirm�Ի�����ʾ��Ϣ(CancelҲ�ط���Ϣ)
        '     objPopMessage          ��PopMessage����
        '     strMsg                 ��Ҫ��ʾ����Ϣ
        '     strClickedControlId    ���ش��ǡ�����ÿؼ��ط��¼�
        '     strClickedControlEvent ���ط��¼����Ƽ������
        '     intPoint               ��ִ�б����̵ĳ���ִ�е�
        '     blnCancelPostBack      ��true - CancelҲ�ط���Ϣ
        '----------------------------------------------------------------
        Public Sub doConfirmMessage( _
            ByVal objPopMessage As Josco.Web.PopMessage, _
            ByVal strTitle As String, _
            ByVal strClickedControlId As String, _
            ByVal strClickedControlEvent As String, _
            ByVal intPoint As Integer, _
            ByVal blnCancelPostBack As Boolean)

            Try
                'Cancel���ûط�
                If blnCancelPostBack = False Then
                    Me.doConfirmMessage(objPopMessage, strTitle, strClickedControlId, strClickedControlEvent, intPoint)
                    Exit Try
                End If

                '���
                If objPopMessage Is Nothing Then Exit Try
                If strTitle Is Nothing Then strTitle = ""
                If strClickedControlId Is Nothing Then strClickedControlId = ""
                If strClickedControlEvent Is Nothing Then strClickedControlEvent = ""

                '������Ϣ
                If strTitle.Length > 0 Then
                    strTitle = Me.doConvertToJavaScriptString(strTitle)
                End If

                '���ò���
                objPopMessage.PopupWindowType = Josco.Web.PopupWindowTypeEnum.Confirm
                objPopMessage.RelaControlId = strClickedControlId
                objPopMessage.RelaControlEventParam = strClickedControlEvent
                objPopMessage.ExecPoint = CType(intPoint, Short)
                objPopMessage.CancelPostback = True
                objPopMessage.Text = strTitle

                '��ʾ
                objPopMessage.Visible = True

            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' window.open(strUrl, strTarget, strFeatures, True)
        '     objPopMessage    ��PopMessage����
        '     strUrl           ��Ҫ�򿪵�Url
        '     strTarget        ����strTarget�д�
        '     strFeatures      ��OpenUrl����
        '----------------------------------------------------------------
        Public Sub doOpenUrl( _
            ByVal objPopMessage As Josco.Web.PopMessage, _
            ByVal strUrl As String, _
            ByVal strTarget As String, _
            ByVal strFeatures As String)

            Try
                '���
                If objPopMessage Is Nothing Then Exit Try
                If strUrl Is Nothing Then strUrl = ""
                If strTarget Is Nothing Then strTarget = ""
                If strFeatures Is Nothing Then strFeatures = ""

                '���ò���
                objPopMessage.PopupWindowType = Josco.Web.PopupWindowTypeEnum.OpenUrl
                objPopMessage.Link = strUrl
                objPopMessage.LinkTarget = strTarget
                objPopMessage.Text = strFeatures

                '��ʾ
                objPopMessage.Visible = True

            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' ִ�пͻ��˽ű�
        '     objPopMessage    ��PopMessage����
        '     strScript        ��Ҫ�򿪵�Url
        '----------------------------------------------------------------
        Public Sub doExeClientScript( _
            ByVal objPopMessage As Josco.Web.PopMessage, _
            ByVal strScript As String)

            Try
                '���
                If objPopMessage Is Nothing Then Exit Try
                If strScript Is Nothing Then strScript = ""

                '���ò���
                objPopMessage.PopupWindowType = Josco.Web.PopupWindowTypeEnum.ExeScript
                objPopMessage.Message = strScript

                '��ʾ
                objPopMessage.Visible = True

            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' ����PopMessage�ؼ�����
        '     objPopMessage    ��PopMessage����
        '----------------------------------------------------------------
        Public Sub doResetPopMessage(ByVal objPopMessage As Josco.Web.PopMessage)

            Try
                '���
                If objPopMessage Is Nothing Then Exit Try

                '���ò���
                objPopMessage.PopupWindowType = Josco.Web.PopupWindowTypeEnum.Normal
                objPopMessage.RelaControlId = ""
                objPopMessage.ExecPoint = 0
                objPopMessage.Visible = False

            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' ��ʾconfirm�Ի����ط���ԭ���������ж�ĳ�δ����Ƿ�ִ�У�
        '     objHttpRequest         ����ǰHttpRequest
        '     strPopMessageControlId ��PopMessage�����UniqueID
        '     intPoint               ��ִ�б����̵ĳ���ִ�е�
        '----------------------------------------------------------------
        Public Function isExecuteCode( _
            ByVal objHttpRequest As System.Web.HttpRequest, _
            ByVal strPopMessageControlId As String, _
            ByVal intPoint As Integer) As Boolean

            isExecuteCode = False
            Try
                Dim strPopupWindowType As String
                Dim strExecPoint As String

                '���
                If objHttpRequest Is Nothing Then Exit Try

                '��ȡPopMessage�Ļط�����
                strPopupWindowType = objHttpRequest.Form(strPopMessageControlId + "_PopupWindowType")
                strExecPoint = objHttpRequest.Form(strPopMessageControlId + "_ExecPoint")

                'û�лط���˵��δִ��
                If strPopupWindowType Is Nothing Then GoTo normExit
                If strExecPoint Is Nothing Then GoTo normExit

                '��ִ����confirm?
                If strPopupWindowType.Length > 0 Then strPopupWindowType = strPopupWindowType.Trim()
                If strPopupWindowType.ToLower() <> "Confirm".ToLower() And _
                    strPopupWindowType.ToLower() <> "Prompt".ToLower() Then
                    Exit Try
                End If

                '����confirm֮ǰ�Ѿ�ִ��?
                If strExecPoint.Length > 0 Then strExecPoint = strExecPoint.Trim()
                Dim intTemp As Integer
                Try
                    intTemp = CType(strExecPoint, Integer)
                Catch ex As Exception
                    intTemp = -1
                End Try
                If (intPoint <= intTemp) Then
                    '�Ѿ�ִ����
                    Exit Try
                End If

normExit:
                'Ҫִ��
                isExecuteCode = True

            Catch ex As Exception
                isExecuteCode = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡͨ��PopMessage�����ֵ
        '     objHttpRequest         ����ǰHttpRequest
        '     strPopMessageControlId ��PopMessage�����UniqueID
        '     intPoint               ��ִ�б����̵ĳ���ִ�е�
        '----------------------------------------------------------------
        Public Function getInputBoxValue( _
            ByVal objHttpRequest As System.Web.HttpRequest, _
            ByVal strPopMessageControlId As String) As String

            getInputBoxValue = ""

            Try
                Dim strPopupWindowReturnValue As String
                Dim strPopupWindowType As String
                Dim strExecPoint As String

                '���
                If objHttpRequest Is Nothing Then Exit Try

                '��ȡPopMessage�Ļط�����
                strPopupWindowReturnValue = objHttpRequest.Form(strPopMessageControlId + "_PopupWindowReturnValue")
                strPopupWindowType = objHttpRequest.Form(strPopMessageControlId + "_PopupWindowType")
                strExecPoint = objHttpRequest.Form(strPopMessageControlId + "_ExecPoint")

                'û�лط���˵��δִ��
                If strPopupWindowType Is Nothing Then Exit Try
                If strExecPoint Is Nothing Then Exit Try

                '��ִ����Prompt?
                If strPopupWindowType.Length > 0 Then strPopupWindowType = strPopupWindowType.Trim()
                If strPopupWindowType.ToLower() <> "Prompt".ToLower() Then
                    Exit Try
                End If

                'Ҫִ��
                If strPopupWindowReturnValue Is Nothing Then strPopupWindowReturnValue = ""
                strPopupWindowReturnValue = strPopupWindowReturnValue.Trim()
                getInputBoxValue = strPopupWindowReturnValue

            Catch ex As Exception
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡͨ��confirm�����ֵ
        '     objHttpRequest         ����ǰHttpRequest
        '     strPopMessageControlId ��PopMessage�����UniqueID
        '     intPoint               ��ִ�б����̵ĳ���ִ�е�
        '----------------------------------------------------------------
        Public Function getConfirmBoxValue( _
            ByVal objHttpRequest As System.Web.HttpRequest, _
            ByVal strPopMessageControlId As String) As Boolean

            getConfirmBoxValue = False 'Cancel button

            Try
                Dim strPopupWindowReturnValue As String
                Dim strPopupWindowType As String
                Dim strExecPoint As String

                '���
                If objHttpRequest Is Nothing Then Exit Try

                '��ȡPopMessage�Ļط�����
                strPopupWindowReturnValue = objHttpRequest.Form(strPopMessageControlId + "_PopupWindowReturnValue")
                strPopupWindowType = objHttpRequest.Form(strPopMessageControlId + "_PopupWindowType")
                strExecPoint = objHttpRequest.Form(strPopMessageControlId + "_ExecPoint")

                'û�лط���˵��δִ��
                If strPopupWindowType Is Nothing Then Exit Try
                If strExecPoint Is Nothing Then Exit Try

                '��ִ����Confirm?
                If strPopupWindowType.Length > 0 Then strPopupWindowType = strPopupWindowType.Trim()
                If strPopupWindowType.ToLower() <> "Confirm".ToLower() Then
                    Exit Try
                End If

                'Ҫִ��
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
