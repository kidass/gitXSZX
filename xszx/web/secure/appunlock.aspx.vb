Imports System.Web.Security

Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.web
    ' 类名    ：applock
    '
    ' 功能描述： 
    '   　处理个人的运行参数配置
    '----------------------------------------------------------------

    Partial Public Class appunlock
        Inherits Xydc.Platform.web.PageBase

        '----------------------------------------------------------------
        '模块私用参数
        '----------------------------------------------------------------
        '本模块相对image的相对路径
        Private m_cstrRelativePathToImage As String = "../"










        '----------------------------------------------------------------
        ' 初始化控件
        '----------------------------------------------------------------
        Private Function initializeControls(ByRef strErrMsg As String) As Boolean

            initializeControls = False

            '执行键转译(不论是否是“回发”)
            Try
                With New Xydc.Platform.web.ControlProcess
                    .doTranslateKey(Me.txtUserPwd)
                    .doTranslateKey(Me.txtUserId)
                End With

                '锁定用户标识
                Me.txtUserId.Value = MyBase.UserId

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            initializeControls = True
            Exit Function

errProc:
            Exit Function

        End Function

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String
            Dim strUrl As String

            Try

                '预处理
                If MyBase.doPagePreprocess(False, False) = True Then
                    Exit Sub
                End If

                '控件初始化
                If Me.initializeControls(strErrMsg) = False Then
                    GoTo errProc
                End If

                '设置鉴别尝试最大次数！
                Me.htxtMaxTryCount.Value = Xydc.Platform.Common.jsoaConfiguration.LoginTryCount.ToString

                '设置被锁定的时间
                Me.htxtLockTime.Value = Xydc.Platform.Common.jsoaConfiguration.DeadAccountLock.ToString

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

            Dim objsystemCustomer As Xydc.Platform.BusinessFacade.systemCustomer
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim objCustomerData As Xydc.Platform.Common.Data.CustomerData
            Dim strErrMsg As String
            Dim strUserId As String

            Try
                '获取输入信息
                Dim strNewPassword As String
                Dim strPassword As String
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
                Dim strLocktime As String
                Dim blnLocked As Boolean
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

                '释放现有用户数据集
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(MyBase.Customer)

                '缓存用户信息
                MyBase.Customer = objCustomerData
                MyBase.UserId = strUserId
                MyBase.UserOrgPassword = strPassword
                MyBase.UserPassword = strNewPassword

                '检查密码长度
                If MyBase.doCheckPassword() = True Then
                    Exit Sub
                End If

                '解除锁定
                MyBase.AppLocked = False

                '定向到启动页面
                Response.Redirect("appunlocksuccess.aspx", False)

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
            Xydc.Platform.BusinessFacade.systemCustomer.SafeRelease(objsystemCustomer)
            Xydc.Platform.Common.Data.CustomerData.SafeRelease(objCustomerData)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub lnkLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkLogin.Click
            Me.doLogin("lnkLogin")
        End Sub


    End Class
End Namespace