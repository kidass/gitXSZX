Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.web
    ' 类名    ：main
    '
    ' 功能描述： 
    '     用户登录检查模块。
    '----------------------------------------------------------------
    Partial Public Class modifypwd
        Inherits Xydc.Platform.web.PageBase
        '----------------------------------------------------------------
        '模块私用参数
        '----------------------------------------------------------------
        '本模块相对image的相对路径
        Private m_cstrRelativePathToImage As String = "../"

        '----------------------------------------------------------------
        '模块接口参数
        '----------------------------------------------------------------
        Private m_objInterface As Xydc.Platform.BusinessFacade.IModifyPwd








        '----------------------------------------------------------------
        ' 释放接口参数
        '----------------------------------------------------------------
        Private Sub releaseInterfaceParameters()

            Try
                If Not (Me.m_objInterface Is Nothing) Then
                    If Me.m_objInterface.iInterfaceType = Xydc.Platform.BusinessFacade.ICallInterface.enumInterfaceType.InputOnly Then
                        Session.Remove(Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.ISessionId))
                        Me.m_objInterface.Dispose()
                        Me.m_objInterface = Nothing
                    End If
                End If
            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' 获取接口参数
        '----------------------------------------------------------------
        Private Function getInterfaceParameters(ByRef strErrMsg As String) As Boolean

            getInterfaceParameters = False

            '从QueryString中解析接口参数(不论是否回发)
            Dim objTemp As Object
            Try
                objTemp = Session.Item(Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.ISessionId))
                m_objInterface = CType(objTemp, Xydc.Platform.BusinessFacade.IModifyPwd)
            Catch ex As Exception
                m_objInterface = Nothing
            End Try

            getInterfaceParameters = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 初始化控件
        '----------------------------------------------------------------
        Private Function initializeControls(ByRef strErrMsg As String) As Boolean

            initializeControls = False

            '仅在第一次调用页面时执行
            If Me.IsPostBack = False Then
                Try
                    '初始化静态控件
                    If Me.m_objInterface Is Nothing Then
                        Me.txtUserId.Value = MyBase.UserId
                    Else
                        Me.txtUserId.Value = m_objInterface.iUserId
                    End If

                    '显示Pannel(不论是否回调，始终显示panelModifyPwd)
                    Me.panelModifyPwd.Visible = True
                    Me.panelInformation.Visible = Not Me.panelModifyPwd.Visible

                   
                    Me.btnReset.Visible = False


                    '不允许修改
                    Me.txtUserId.Disabled = True

                    '执行键转译(不论是否是“回发”)
                    With New Xydc.Platform.web.ControlProcess
                        .doTranslateKey(Me.txtUserId)
                        .doTranslateKey(Me.txtNewUserPwd)
                        .doTranslateKey(Me.txtNewUserPwdQR)
                    End With

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
            End If

            initializeControls = True
            Exit Function

errProc:
            Exit Function

        End Function

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String
            Dim strUrl As String

            '预处理
            If MyBase.doPagePreprocess(False, Not Me.IsPostBack) = True Then
                Exit Sub
            End If

            '获取接口参数
            If Me.getInterfaceParameters(strErrMsg) = False Then
                GoTo errProc
            End If

            '控件初始化
            If Me.initializeControls(strErrMsg) = False Then
                GoTo errProc
            End If

            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub doModifyPassword(ByVal strControlId As String)

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objsystemCustomer As New Xydc.Platform.BusinessFacade.systemCustomer
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            '获取输入参数
            Dim strCzyId As String
            Dim strUserId As String
            Dim strPassword As String
            Dim strPassword1 As String
            Dim strPassword2 As String
            Dim strNewPassword As String
            strCzyId = MyBase.UserId
            strUserId = Me.txtUserId.Value
            strPassword = MyBase.UserPassword
            strPassword1 = Me.txtNewUserPwd.Value
            strPassword2 = Me.txtNewUserPwdQR.Value
            If strPassword1 <> strPassword2 Then
                strErrMsg = "错误：两次输入的密码不一致！"
                GoTo errProc
            End If

            '检查长度
            Dim intLevel As Integer = 0
            If Xydc.Platform.Common.jsoaConfiguration.CheckPassword = True Then
                Dim intMinLen As Integer = Xydc.Platform.Common.jsoaConfiguration.MinPasswordLength
                If strPassword1.Length < intMinLen Then
                    strErrMsg = "错误：密码长度至少[" + intMinLen.ToString + "]个字符！"
                    GoTo errProc
                End If
                '密码强度检查
                Dim blnFoundSign As Boolean = False
                Dim blnFoundLCap As Boolean = False
                Dim blnFoundUCap As Boolean = False
                Dim blnFoundNum As Boolean = False
                Dim objBytes() As Char
                objBytes = strPassword1.ToCharArray()
                Dim intCount As Integer
                Dim i As Integer
                intCount = objBytes.Length
                For i = 0 To intCount - 1 Step 1
                    If Char.IsDigit(objBytes(i)) = True Then
                        blnFoundNum = True
                    End If
                    If Char.IsLetter(objBytes(i)) = True And Char.IsLower(objBytes(i)) = True Then
                        blnFoundLCap = True
                    End If
                    If Char.IsLetter(objBytes(i)) = True And Char.IsUpper(objBytes(i)) = True Then
                        blnFoundUCap = True
                    End If
                    If Char.IsPunctuation(objBytes(i)) = True Then
                        blnFoundSign = True
                    End If
                Next
                If blnFoundNum = True Then
                    intLevel += 1
                End If
                If blnFoundLCap = True Then
                    intLevel += 1
                End If
                If blnFoundUCap = True Then
                    intLevel += 1
                End If
                If blnFoundSign = True Then
                    intLevel += 1
                End If
                If intLevel < Xydc.Platform.Common.jsoaConfiguration.PasswordLevel Then
                    strErrMsg = "错误：密码强度不够，必须有大写字母、小写字母、数字、特殊字符四种类型中的[" + Xydc.Platform.Common.jsoaConfiguration.PasswordLevel.ToString + "]种！"
                    GoTo errProc
                End If
            End If

            '修改密码处理
            Try
                With objsystemCustomer
                    .doModifyPassword(strErrMsg, strCzyId, strPassword, strUserId, strPassword1, strPassword2, strNewPassword)
                    If strErrMsg <> "" Then
                        GoTo errProc
                    End If
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            '如果修改的是当前用户，则
            If strUserId = MyBase.UserId Then
                '更新密码缓存
                MyBase.UserPassword = strNewPassword
                MyBase.UserOrgPassword = strPassword1
            Else
                '记录审计日志
                Xydc.Platform.SystemFramework.ApplicationLog.WriteAuditAQInfo(Request.UserHostAddress, Request.UserHostName, "[" + MyBase.UserId + "]修改了[" + strUserId + "]用户标识的密码！")
            End If

            '释放资源
            Xydc.Platform.BusinessFacade.systemCustomer.SafeRelease(objsystemCustomer)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)

            '显示成功信息
            If Me.m_objInterface Is Nothing Then

                '独立调用
                Me.releaseInterfaceParameters()
                Response.Redirect(Xydc.Platform.Common.jsoaConfiguration.GeneralReturnUrl)

            Else
                '因为不返回任何参数，释放接口资源
                Dim strUrl As String
                strUrl = Me.m_objInterface.iReturnUrl
                Me.releaseInterfaceParameters()
                '返回调用模块
                Response.Redirect(strUrl)
            End If

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.BusinessFacade.systemCustomer.SafeRelease(objsystemCustomer)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub doCancel(ByVal strControlId As String)

            Dim strUrl As String = ""

            If Not (Me.m_objInterface Is Nothing) Then
                '返回调用模块
                strUrl = Me.m_objInterface.iReturnUrl
            Else
                strUrl = Platform.Common.jsoaConfiguration.GeneralReturnUrl
            End If
            If strUrl <> "" Then
                Response.Redirect(strUrl)
            End If


        End Sub

        Private Sub btnModify_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModify.ServerClick
            Me.doModifyPassword("btnModify")
        End Sub

        Private Sub btnCancel_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.ServerClick
            Me.doCancel("btnCancel")
        End Sub
    End Class
End Namespace