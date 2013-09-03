Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.web
    ' 类名    ：BannerTitle
    '
    ' 功能描述： 
    '   　banner 用户控件模块
    '----------------------------------------------------------------

    Partial Public Class bannerTitle
        Inherits Xydc.Platform.web.ControlBase
        '----------------------------------------------------------------
        ' PathBannerSlice_Left:
        '   获取banner slice左侧的图像路径
        ' Returns:
        '   String
        '----------------------------------------------------------------
        Public Function PathBannerSlice_Left() As String

            PathBannerSlice_Left = Me.PathPrefix + "/images/bannerslice_left.jpg"

        End Function

        '----------------------------------------------------------------
        ' PathBannerSlice_Right:
        '   获取banner slice右侧的图像路径
        ' Returns:
        '   String
        '----------------------------------------------------------------
        Public Function PathBannerSlice_Right() As String

            PathBannerSlice_Right = Me.PathPrefix + "/images/bannerslice_right.jpg"

        End Function

        '----------------------------------------------------------------
        ' PathBannerLogo:
        '   获取banner logo的图像路径
        ' Returns:
        '   String
        '----------------------------------------------------------------
        Public Function PathBannerLogo() As String

            PathBannerLogo = Me.PathPrefix + "/images/bannerlogo.jpg"

        End Function

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            Dim objsystemMyCalender As New Xydc.Platform.BusinessFacade.systemMyCalender
            Dim objsystemFlowObject As Xydc.Platform.BusinessFacade.systemFlowObject
            Dim objsystemMyTask As New Xydc.Platform.BusinessFacade.systemMyTask
          
            Dim objPersonConfig As Xydc.Platform.web.PersonConfig = Nothing
            Dim intAutoRefreshTime As Integer = 0
            Dim blnAutoRefresh As Boolean = False
            Dim intAllTXNum As Integer = 0
            Dim strErrMsg As String

            Try

                '设置当前窗口显示模式
                If MyBase.FullScreen = True Then
                    Me.htxtFullScreen.Value = "1"
                Else
                    Me.htxtFullScreen.Value = "0"
                End If
                '设置系统锁定状态
                If MyBase.AppLocked = True Then
                    Me.htxtLockApp.Value = "1"
                Else
                    Me.htxtLockApp.Value = "0"
                End If


                '登录后的参数检查
                If MyBase.UserId <> "" Then
                    Dim intCountDBSY As Integer = 0
                    Dim intCountGQSY As Integer = 0
                    Dim intCountBWTX As Integer = 0
                    Dim intCountTXRC As Integer = 0
                    Dim intCountNBLW As Integer = 0
                    Dim intCountHYWB As Integer = 0
                    Dim intCountKWSM As Integer = 0

                    '获取个人配置
                    objPersonConfig = New Xydc.Platform.web.PersonConfig(MyBase.UserId, Server.MapPath(Request.ApplicationPath + "\profile\"))
                    If objPersonConfig.propStatusRefreshSwitch = True Then
                        Me.htxtAutoRefreshEnabled.Value = "1"
                    Else
                        Me.htxtAutoRefreshEnabled.Value = "0"
                    End If
                    Me.htxtAutoRefreshTime.Value = objPersonConfig.propStatusRefreshTime.ToString

                    '是否需要自动刷新？
                    If objPersonConfig.propStatusRefreshSwitch = True Then
                        '获取待办事宜
                        If objsystemMyTask.getCountDBSY(strErrMsg, MyBase.UserId, MyBase.UserPassword, MyBase.UserXM, intCountDBSY) = False Then
                        End If
                        intAllTXNum += intCountDBSY

                        '获取过期或今天即将过期事宜
                        'If objsystemMyTask.getCountGQSY(strErrMsg, MyBase.UserId, MyBase.UserPassword, MyBase.UserXM, intCountGQSY) = False Then
                        'End If
                        'intAllTXNum += intCountGQSY

                        '获取备忘提醒文件
                        If objsystemMyTask.getCountBWTX(strErrMsg, MyBase.UserId, MyBase.UserPassword, MyBase.UserXM, intCountBWTX) = False Then
                        End If
                        intAllTXNum += intCountBWTX

                        '获取没有阅读的内部来文
                        Dim strUserArray() As String = MyBase.UserXM.Split(Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate.ToCharArray)
                        Dim strFlowType As String = ""
                        Dim strWJBS As String = ""
                        'strFlowType = Xydc.Platform.BusinessFacade.systemFlowObject.getFlowType(strFlowTypeName)
                        'objsystemFlowObject = Xydc.Platform.BusinessFacade.systemFlowObject.Create(strFlowType, strFlowTypeName)
                        'objsystemFlowObjectFawen = CType(objsystemFlowObject, Xydc.Platform.BusinessFacade.systemFlowObjectFawen)
                        'If objsystemFlowObjectFawen.doInitialize(strErrMsg, MyBase.UserId, MyBase.UserPassword, strWJBS, True) = True Then
                        '    If objsystemFlowObjectFawen.getFawenFenfaFileCount(strErrMsg, strUserArray, "", intCountNBLW) = False Then
                        '    End If
                        'End If
                        'intAllTXNum += intCountNBLW

                        '获取需要提醒的日程安排
                        'If objsystemMyCalender.getCountTXSY(strErrMsg, MyBase.UserId, MyBase.UserPassword, MyBase.UserId, intCountTXRC) = False Then
                        'End If
                        'intAllTXNum += intCountTXRC

                        '获取需要提醒的会议邀请或通知
                        'If objsystemHuiyi.getCount_TongzhiJiaojie(strErrMsg, MyBase.UserId, MyBase.UserPassword, MyBase.UserXM, 0, intCountHYWB) = False Then
                        'End If
                        'intAllTXNum += intCountHYWB

                        '获取需要提醒的会议邀请或通知
                        'If objsystemXxcbCommon.getCount_Kanwu_Fabu(strErrMsg, MyBase.UserId, MyBase.UserPassword, MyBase.UserXM, "", intCountKWSM) = False Then
                        'End If
                        'intAllTXNum += intCountKWSM
                    Else
                        Me.lblRealtimeMessage.Text = "警告：您已经禁止自动刷新本栏目！"
                        intAllTXNum += 1
                    End If

                    If intAllTXNum > 0 Then
                        Me.lblRealtimeMessage.Text = ""
                        If intCountDBSY > 0 Then
                            Me.lblRealtimeMessage.Text += "共有[" + intCountDBSY.ToString + "]个文件等待您处理......"
                        End If
                        If intCountGQSY > 0 Then
                            Me.lblRealtimeMessage.Text += "共有[" + intCountGQSY.ToString + "]个文件今天即将到期或已经过期......"
                        End If
                        If intCountBWTX > 0 Then
                            Me.lblRealtimeMessage.Text += "共有[" + intCountBWTX.ToString + "]个文件提醒您注意文件办理情况......"
                        End If
                        If intCountNBLW > 0 Then
                            Me.lblRealtimeMessage.Text += "共有[" + intCountNBLW.ToString + "]个单位内部下发的文件您没有阅读......"
                        End If
                        If intCountTXRC > 0 Then
                            Me.lblRealtimeMessage.Text += "共有[" + intCountTXRC.ToString + "]个日程安排需要提醒您......"
                        End If
                        If intCountHYWB > 0 Then
                            Me.lblRealtimeMessage.Text += "共有[" + intCountHYWB.ToString + "]个会议邀请您参加......"
                        End If
                        If intCountKWSM > 0 Then
                            Me.lblRealtimeMessage.Text += "共有[" + intCountKWSM.ToString + "]个单位内部下发的刊物您没有阅读......"
                        End If
                    Else
                        Me.lblRealtimeMessage.Text = ""
                        Me.lblRealtimeMessage.Text += "欢迎使用&nbsp;&nbsp;"
                        Me.lblRealtimeMessage.Text += System.Configuration.ConfigurationManager.AppSettings("ApplicationName")
                        Me.lblRealtimeMessage.Text += "V"
                        Me.lblRealtimeMessage.Text += System.Configuration.ConfigurationManager.AppSettings("ApplicationVersion")
                        Me.lblRealtimeMessage.Text += "&nbsp;&nbsp;版权所有&copy;"
                        Me.lblRealtimeMessage.Text += System.Configuration.ConfigurationManager.AppSettings("CopyRights")
                        Me.lblRealtimeMessage.Text += "&nbsp;&nbsp;"
                        Me.lblRealtimeMessage.Text += System.Configuration.ConfigurationManager.AppSettings("DeveloperName")
                    End If

                    Me.lblUserXM.Text = ""
                    Me.lblUserXM.Text += "|"

                    'Me.lblUserXM.Text += MyBase.UserXM
                    Me.lblUserXM.Text += MyBase.UserZM

                    Me.lblUserBMMC.Text = ""
                    Me.lblUserBMMC.Text += "|"
                    Me.lblUserBMMC.Text += MyBase.UserBmmc
                    Dim objDateTime As System.DateTime
                    objDateTime = CType(MyBase.UserEnterTime, System.DateTime)
                    Dim objTimeSpan As System.TimeSpan
                    objTimeSpan = Now.Subtract(objDateTime)
                    Me.lblUserEnterTime.Text = ""
                    Me.lblUserEnterTime.Text += Right("00" + objTimeSpan.Hours.ToString(), 2) + ":"
                    Me.lblUserEnterTime.Text += Right("00" + objTimeSpan.Minutes.ToString(), 2) + ":"
                    Me.lblUserEnterTime.Text += Right("00" + objTimeSpan.Seconds.ToString(), 2)
                    Me.htxtUserEnterTime.Value = Format(objDateTime, "MM/dd/yyyy HH:mm:ss")
                Else
                    Me.lblRealtimeMessage.Text = ""
                    Me.lblRealtimeMessage.Text += "欢迎使用&nbsp;&nbsp;"
                    Me.lblRealtimeMessage.Text += System.Configuration.ConfigurationManager.AppSettings("ApplicationName")
                    Me.lblRealtimeMessage.Text += "V"
                    Me.lblRealtimeMessage.Text += System.Configuration.ConfigurationManager.AppSettings("ApplicationVersion")
                    Me.lblRealtimeMessage.Text += "&nbsp;&nbsp;版权所有&copy;"
                    Me.lblRealtimeMessage.Text += System.Configuration.ConfigurationManager.AppSettings("CopyRights")
                    Me.lblRealtimeMessage.Text += "&nbsp;&nbsp;"
                    Me.lblRealtimeMessage.Text += System.Configuration.ConfigurationManager.AppSettings("DeveloperName")

                    Me.lblUserXM.Text = ""
                    Me.lblUserBMMC.Text = ""
                    Me.lblUserEnterTime.Text = ""
                    Me.htxtUserEnterTime.Value = ""
                End If

                Me.htxtInfoCount.Value = intAllTXNum.ToString
            Catch ex As Exception
            End Try

            Xydc.Platform.BusinessFacade.systemMyCalender.SafeRelease(objsystemMyCalender)
            Xydc.Platform.BusinessFacade.systemMyTask.SafeRelease(objsystemMyTask)
            Xydc.Platform.web.PersonConfig.SafeRelease(objPersonConfig)
            Exit Sub

        End Sub

    End Class
End Namespace