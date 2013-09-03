Imports System.Web.Security

Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.web
    ' 类名    ：login
    '
    ' 功能描述： 
    '   　'     用户登录检查模块。
    '----------------------------------------------------------------


    Partial Public Class login
        Inherits Xydc.Platform.web.PageBase
        '----------------------------------------------------------------
        '模块私用参数
        '----------------------------------------------------------------
        '本模块相对image的相对路径
        Private m_cstrRelativePathToImage As String = "../"
        Private m_firstEnter As Boolean = False









        '----------------------------------------------------------------
        ' 初始化控件
        '----------------------------------------------------------------
        Private Function initializeControls(ByRef strErrMsg As String) As Boolean

            Dim objControlProcess As New Xydc.Platform.web.ControlProcess

            initializeControls = False
            strErrMsg = ""

            Try
                '执行键转译(不论是否是“回发”)
                If Me.IsPostBack = False Then
                    objControlProcess.doTranslateKey(Me.txtUserPwd)
                    objControlProcess.doTranslateKey(Me.txtUserId)
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.ControlProcess.SafeRelease(objControlProcess)

            initializeControls = True
            Exit Function

errProc:
            Xydc.Platform.web.ControlProcess.SafeRelease(objControlProcess)
            Exit Function

        End Function

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String = ""

            Try
                '控件初始化
                If Me.initializeControls(strErrMsg) = False Then
                    GoTo errProc
                End If

                '设置鉴别尝试最大次数！
                Me.htxtMaxTryCount.Value = Xydc.Platform.Common.jsoaConfiguration.LoginTryCount.ToString

                '设置被锁定的时间
                Me.htxtLockTime.Value = Xydc.Platform.Common.jsoaConfiguration.DeadAccountLock.ToString

                '清空注册信息
                MyBase.UserId = ""
                MyBase.UserPassword = ""
                MyBase.UserEnterTime = ""
                MyBase.LastScanTime_Chat = ""
                MyBase.LastScanTime_Notice = ""
                MyBase.Customer = Nothing
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub
errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub












        Private Sub doLogin(ByVal strControlId As String)

            Dim objsystemCustomer As Xydc.Platform.BusinessFacade.systemCustomer = Nothing
            Dim objCustomerData As Xydc.Platform.Common.Data.CustomerData = Nothing
            Dim objPersonConfig As Xydc.Platform.web.PersonConfig = Nothing
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String = ""
            Dim strUserId As String = ""

            Try
                '获取输入信息
                Dim strNewPassword As String = ""
                Dim strPassword As String = ""
                strPassword = Me.txtUserPwd.Value.Trim
                strUserId = Me.txtUserId.Value.Trim
                If strUserId = "" Then
                    strErrMsg = "错误：没有输入[用户标识]！"
                    GoTo errProc
                End If

                '验证
                objsystemCustomer = New Xydc.Platform.BusinessFacade.systemCustomer
                If objsystemCustomer.doVerifyUserPassword(strErrMsg, strUserId, strPassword, strNewPassword) = False Then
                    Me.htxtTryCount.Value = (CType(Me.htxtTryCount.Value, System.Int32) + 1).ToString
                    GoTo writeLog
                End If

                '是否被锁定？
                Dim strLocktime As String = ""
                Dim blnLocked As Boolean = False
                If MyBase.isAccountLocked(strErrMsg, strUserId, blnLocked, strLocktime) = False Then
                    GoTo errProc
                Else
                    If blnLocked = True Then
                        strErrMsg = "错误：账户[" + strUserId + "]已经被锁定，开始锁定时间为[" + strLocktime + "]，锁定持续[" + Xydc.Platform.Common.jsoaConfiguration.DeadAccountLock.ToString + "]分钟！"
                        GoTo errProc
                    End If
                End If

                '清空尝试次数
                Me.htxtTryCount.Value = "0"

                '获取用户信息
                If objsystemCustomer.getRenyuanData(strErrMsg, strUserId, strNewPassword, "0011", objCustomerData) = False Then
                    GoTo errProc
                End If
                '如果不是管理员，则必须有人员信息
                If strUserId.ToUpper() <> "SA" Then
                    If objCustomerData Is Nothing Then
                        strErrMsg = "错误：没有登记人员信息！"
                        GoTo errProc
                    End If
                    If objCustomerData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN).Rows.Count < 1 Then
                        strErrMsg = "错误：没有登记人员信息！"
                        GoTo errProc
                    End If
                End If

                '记录进出日志
                If objsystemCustomer.doWriteXitongJinchuRizhi(strErrMsg, strUserId, strNewPassword, Xydc.Platform.Common.Data.CustomerData.STATUS_LOGIN, Request.UserHostAddress) = False Then
                    '可以不成功！
                End If
                '记录在线用户
                If objsystemCustomer.doWriteZaixianYonghu(strErrMsg, strUserId, strNewPassword) = False Then
                    '可以不成功！
                End If

                '记录凭证
                System.Web.Security.FormsAuthentication.SetAuthCookie("*", False)

                '缓存用户信息
                MyBase.Customer = objCustomerData
                MyBase.UserId = strUserId
                MyBase.UserOrgPassword = strPassword
                MyBase.UserPassword = strNewPassword
                MyBase.UserEnterTime = Format(Now, "yyyy-MM-dd HH:mm:ss")
                MyBase.LastScanTime_Chat = ""
                MyBase.LastScanTime_Notice = ""

                '检查密码长度
                If MyBase.doCheckPassword() = True Then
                    Exit Sub
                End If


                '保存配置文件
                Try
                    Dim strPath As String = Server.MapPath(Request.ApplicationPath + "\profile\")
                    objPersonConfig = New Xydc.Platform.web.PersonConfig(Me.txtUserId.Value, strPath)
                    Select Case Me.rblJRLX.SelectedIndex
                        Case 1
                            objPersonConfig.propStartupOption = 1
                        Case Else
                            objPersonConfig.propStartupOption = 0
                    End Select
                    objPersonConfig.doSave()
                Catch ex As Exception
                End Try



                Dim strLX As String = Me.rblJRLX.SelectedItem.Value
                Select Case strLX
                    Case "1"
                        Dim strUrl As String = "./grsw/grsw_wdsy.aspx"
                        Response.Redirect(strUrl)
                    Case Else
                        '执行重定向到安全访问页
                        System.Web.Security.FormsAuthentication.RedirectFromLoginPage("*", False)
                End Select
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.BusinessFacade.systemCustomer.SafeRelease(objsystemCustomer)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Xydc.Platform.web.PersonConfig.SafeRelease(objPersonConfig)
            Exit Sub
writeLog:
            Xydc.Platform.SystemFramework.ApplicationLog.WriteInfo(Request.UserHostAddress, Request.UserHostName, "[" + strUserId + "]第[" + Me.htxtTryCount.Value + "]次尝试登陆不成功！")
            GoTo errProc
errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            System.Web.Security.FormsAuthentication.SignOut()
            MyBase.Customer = Nothing
            MyBase.UserId = ""
            MyBase.UserPassword = ""
            MyBase.UserOrgPassword = ""
            MyBase.UserEnterTime = ""
            MyBase.LastScanTime_Chat = ""
            MyBase.LastScanTime_Notice = ""
            Xydc.Platform.BusinessFacade.systemCustomer.SafeRelease(objsystemCustomer)
            Xydc.Platform.Common.Data.CustomerData.SafeRelease(objCustomerData)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Xydc.Platform.web.PersonConfig.SafeRelease(objPersonConfig)
            Exit Sub

        End Sub


        Private Sub doModifyPassword(ByVal strControlId As String)

            Dim objsystemCustomer As Xydc.Platform.BusinessFacade.systemCustomer = Nothing
            Dim objCustomerData As Xydc.Platform.Common.Data.CustomerData = Nothing
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String = ""
            Dim strUserId As String = ""

            Try
                '获取输入信息
                Dim strNewPassword As String = ""
                Dim strPassword As String = ""
                strPassword = Me.txtUserPwd.Value.Trim
                strUserId = Me.txtUserId.Value.Trim
                If strUserId = "" Then
                    strErrMsg = "错误：没有输入[用户标识]！"
                    GoTo errProc
                End If

                '验证
                objsystemCustomer = New Xydc.Platform.BusinessFacade.systemCustomer
                If objsystemCustomer.doVerifyUserPassword(strErrMsg, strUserId, strPassword, strNewPassword) = False Then
                    Me.htxtTryCount.Value = (CType(Me.htxtTryCount.Value, System.Int32) + 1).ToString
                    GoTo writeLog
                End If

                '是否被锁定？
                Dim strLocktime As String = ""
                Dim blnLocked As Boolean = False
                If MyBase.isAccountLocked(strErrMsg, strUserId, blnLocked, strLocktime) = False Then
                    GoTo errProc
                Else
                    If blnLocked = True Then
                        strErrMsg = "错误：账户[" + strUserId + "]已经被锁定，开始锁定时间为[" + strLocktime + "]，锁定持续[" + Xydc.Platform.Common.jsoaConfiguration.DeadAccountLock.ToString + "]分钟！"
                        GoTo errProc
                    End If
                End If

                '清空尝试次数
                Me.htxtTryCount.Value = "0"

                '获取用户信息
                If objsystemCustomer.getRenyuanData(strErrMsg, strUserId, strNewPassword, "0011", objCustomerData) = False Then
                    GoTo errProc
                End If
                '如果不是管理员，则必须有人员信息
                If strUserId.ToUpper() <> "SA" Then
                    If objCustomerData Is Nothing Then
                        strErrMsg = "错误：没有登记人员信息！"
                        GoTo errProc
                    End If
                    If objCustomerData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN).Rows.Count < 1 Then
                        strErrMsg = "错误：没有登记人员信息！"
                        GoTo errProc
                    End If
                End If

                '记录进出日志
                If objsystemCustomer.doWriteXitongJinchuRizhi(strErrMsg, strUserId, strNewPassword, Xydc.Platform.Common.Data.CustomerData.STATUS_LOGIN, Request.UserHostAddress) = False Then
                    '可以不成功！
                End If
                '记录在线用户
                If objsystemCustomer.doWriteZaixianYonghu(strErrMsg, strUserId, strNewPassword) = False Then
                    '可以不成功！
                End If

                '记录凭证
                System.Web.Security.FormsAuthentication.SetAuthCookie("*", False)

                '缓存用户信息
                MyBase.Customer = objCustomerData
                MyBase.UserId = strUserId
                MyBase.UserOrgPassword = strPassword
                MyBase.UserPassword = strNewPassword
                MyBase.UserEnterTime = Format(Now, "yyyy-MM-dd HH:mm:ss")
                MyBase.LastScanTime_Chat = ""
                MyBase.LastScanTime_Notice = ""

                '检查密码长度
                If MyBase.doCheckPassword() = True Then
                    Exit Sub
                End If

                '执行重定向到安全访问页
                Dim strUrl As String = "modifypwd.aspx"
                Response.Redirect(strUrl)
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.BusinessFacade.systemCustomer.SafeRelease(objsystemCustomer)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub
writeLog:
            Xydc.Platform.SystemFramework.ApplicationLog.WriteInfo(Request.UserHostAddress, Request.UserHostName, "[" + strUserId + "]第[" + Me.htxtTryCount.Value + "]次尝试登陆不成功！")
            GoTo errProc
errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            System.Web.Security.FormsAuthentication.SignOut()
            MyBase.Customer = Nothing
            MyBase.UserId = ""
            MyBase.UserPassword = ""
            MyBase.UserOrgPassword = ""
            MyBase.UserEnterTime = ""
            MyBase.LastScanTime_Chat = ""
            MyBase.LastScanTime_Notice = ""
            Xydc.Platform.BusinessFacade.systemCustomer.SafeRelease(objsystemCustomer)
            Xydc.Platform.Common.Data.CustomerData.SafeRelease(objCustomerData)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub


        Private Sub doStartupOption(ByVal strControlId As String)

            Dim objPersonConfig As Xydc.Platform.web.PersonConfig = Nothing

            Try
                If Me.txtUserId.Value.Trim <> "" Then
                    Dim strPath As String = Server.MapPath(Request.ApplicationPath + "\profile\")
                    objPersonConfig = New Xydc.Platform.web.PersonConfig(Me.txtUserId.Value.Trim, strPath)
                    Select Case objPersonConfig.propStartupOption
                        Case 1
                            Me.rblJRLX.SelectedIndex = 1
                        Case Else
                            Me.rblJRLX.SelectedIndex = 0
                    End Select
                End If
            Catch ex As Exception
            End Try

            Xydc.Platform.web.PersonConfig.SafeRelease(objPersonConfig)
            Exit Sub

        End Sub


        Private Sub lnkLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkLogin.Click
            Me.doLogin("lnkLogin")
        End Sub


        Private Sub lnkModifyPassword_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkModifyPassword.Click
            Me.doModifyPassword("lnkLogin")
        End Sub

        Private Sub lnkStartupOption_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkStartupOption.Click
            Me.doStartupOption("lnkStartupOption")
        End Sub

    End Class
End Namespace