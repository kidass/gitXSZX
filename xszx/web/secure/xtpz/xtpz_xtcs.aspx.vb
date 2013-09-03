Imports System.Web.Security

Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.web
    ' 类名    ：xtpz_xtcs
    ' 
    ' 调用性质：
    '     独立运行
    '
    ' 功能描述： 
    '   　系统参数设置模块
    '
    ' 接口参数：
    '     无
    '----------------------------------------------------------------

    Partial Public Class xtpz_xtcs
        Inherits Xydc.Platform.web.PageBase

        '----------------------------------------------------------------
        '模块私用参数
        '----------------------------------------------------------------
        '本模块相对image的相对路径
        Private m_cstrRelativePathToImage As String = "../../"

        '----------------------------------------------------------------
        '模块授权参数
        '----------------------------------------------------------------
        Private m_cstrPrevilegeParamPrefix As String = "xtpz_xtcs_previlege_param"
        Private m_blnPrevilegeParams(1) As Boolean

        '----------------------------------------------------------------
        '模块现场保留参数，恢复完成后立即释放session资源
        '----------------------------------------------------------------
        Private m_blnSaveScence As Boolean

        '----------------------------------------------------------------
        '模块接口参数
        '----------------------------------------------------------------
        Private m_blnInterface As Boolean

        '----------------------------------------------------------------
        '模块访问数据参数
        '----------------------------------------------------------------
        Private m_objDataSet_Main As Xydc.Platform.Common.Data.XitongcanshuData

        '----------------------------------------------------------------
        '模块其他参数
        '----------------------------------------------------------------
        Private m_strhtxtSessionIdZFTPMM As String
        Private m_strhtxtSessionIdCFTPMM As String
        Private m_blnEditMode As Boolean









        '----------------------------------------------------------------
        ' 获取权限参数
        '     strErrMsg          ：返回错误信息
        '     blnContinueExecute ：是否继续执行后续程序？
        ' 返回
        '     True               ：成功
        '     False              ：失败
        '----------------------------------------------------------------
        Private Function getPrevilegeParams( _
            ByRef strErrMsg As String, _
            ByRef blnContinueExecute As Boolean) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objsystemAppManager As New Xydc.Platform.BusinessFacade.systemAppManager
            Dim objMokuaiQXData As Xydc.Platform.Common.Data.AppManagerData

            getPrevilegeParams = False
            blnContinueExecute = False

            Try
                Dim intCount As Integer
                Dim i As Integer

                '根据登录用户获取模块权限数据
                If MyBase.UserId.ToUpper() = "SA" Then
                    '管理员权限
                    intCount = Me.m_blnPrevilegeParams.Length
                    For i = 0 To intCount - 1 Step 1
                        Me.m_blnPrevilegeParams(i) = True
                    Next
                    blnContinueExecute = True
                    Exit Try
                Else
                    '普通用户权限
                    If objsystemAppManager.getDBUserMokuaiQXData(strErrMsg, MyBase.UserId, MyBase.UserPassword, MyBase.UserId, objMokuaiQXData) = False Then
                        GoTo errProc
                    End If
                End If

                '检查权限
                Dim strFirstParamValue As String
                Dim strParamValue As String
                Dim strParamName As String
                Dim strMKMC As String
                Dim strFilter As String
                strMKMC = Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAIQX_MKMC
                With objMokuaiQXData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_YINGYONGXITONG_MOKUAIQX)
                    intCount = Me.m_blnPrevilegeParams.Length
                    For i = 0 To intCount - 1 Step 1
                        '计算参数名
                        strParamName = i.ToString()
                        If strParamName.Length < 2 Then strParamName = "0" + strParamName
                        strParamName = Me.m_cstrPrevilegeParamPrefix + strParamName

                        '获取参数值
                        With objPulicParameters
                            strParamValue = .getObjectValue(System.Configuration.ConfigurationManager.AppSettings(strParamName), "")
                        End With
                        If i = 0 Then strFirstParamValue = strParamValue

                        '获取参数对应的权限
                        strFilter = strMKMC + " = '" + strParamValue + "'"
                        .DefaultView.RowFilter = strFilter
                        If .DefaultView.Count > 0 Then
                            Me.m_blnPrevilegeParams(i) = True
                        Else
                            Me.m_blnPrevilegeParams(i) = False
                        End If
                    Next

                End With

                '是否继续执行
                If Me.m_blnPrevilegeParams(0) = True Then
                    blnContinueExecute = True
                Else
                    Me.panelError.Visible = True
                    Me.lblMessage.Text = "错误：您没有[" + strFirstParamValue + "]的执行权限，请与系统管理员联系，谢谢！"
                    Me.panelMain.Visible = Not Me.panelError.Visible
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.BusinessFacade.systemAppManager.SafeRelease(objsystemAppManager)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objMokuaiQXData)

            getPrevilegeParams = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.BusinessFacade.systemAppManager.SafeRelease(objsystemAppManager)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objMokuaiQXData)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 复原模块现场信息并释放相应的资源
        '----------------------------------------------------------------
        Private Sub restoreModuleInformation(ByVal strSessionId As String)
        End Sub

        '----------------------------------------------------------------
        ' 保存模块现场信息并返回相应的SessionId
        '----------------------------------------------------------------
        Private Function saveModuleInformation() As String

            Dim strSessionId As String = ""

            saveModuleInformation = ""

            Try
            Catch ex As Exception
            End Try

            saveModuleInformation = strSessionId

        End Function

        '----------------------------------------------------------------
        ' 释放接口参数
        '----------------------------------------------------------------
        Private Sub releaseInterfaceParameters()

        End Sub

        '----------------------------------------------------------------
        ' 从调用模块中获取数据
        '----------------------------------------------------------------
        Private Function getDataFromCallModule(ByRef strErrMsg As String) As Boolean

            Try
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getDataFromCallModule = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取接口参数(没有接口参数则显示错误信息页面)
        '----------------------------------------------------------------
        Private Function getInterfaceParameters(ByRef strErrMsg As String) As Boolean

            getInterfaceParameters = False

            Try
                Me.m_blnSaveScence = False
                Me.m_blnInterface = False
                Me.m_blnEditMode = Me.m_blnPrevilegeParams(1)
                Me.m_strhtxtSessionIdZFTPMM = Me.htxtSessionIdZFTPMM.Value
                Me.m_strhtxtSessionIdCFTPMM = Me.htxtSessionIdCFTPMM.Value
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getInterfaceParameters = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 释放本模块缓存的参数
        '----------------------------------------------------------------
        Private Sub releaseModuleParameters()

            Try
                If Me.m_strhtxtSessionIdZFTPMM <> "" Then
                    Session.Remove(Me.m_strhtxtSessionIdZFTPMM)
                End If
                If Me.m_strhtxtSessionIdCFTPMM <> "" Then
                    Session.Remove(Me.m_strhtxtSessionIdCFTPMM)
                End If
            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' 获取模块要显示的数据信息
        '     strErrMsg      ：返回错误信息
        '     strWhere       ：搜索条件(默认表前缀a)
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function getModuleData_Main( _
            ByRef strErrMsg As String, _
            ByVal strWhere As String) As Boolean

            getModuleData_Main = False

            Try
                '释放资源
                If Not (Me.m_objDataSet_Main Is Nothing) Then
                    Me.m_objDataSet_Main.Dispose()
                    Me.m_objDataSet_Main = Nothing
                End If

                '重新检索数据
                With New Xydc.Platform.BusinessFacade.systemXitongpeizhi
                    If .getXitongcanshuData(strErrMsg, MyBase.UserId, MyBase.UserPassword, strWhere, Me.m_objDataSet_Main) = False Then
                        GoTo errProc
                    End If
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getModuleData_Main = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示编辑窗的数据
        '     strErrMsg      ：返回错误信息
        '     blnEditMode    ：当前编辑状态
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showEditPanelInfo( _
            ByRef strErrMsg As String, _
            ByVal blnEditMode As Boolean) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.XitongcanshuData.TABLE_GL_B_XITONGCANSHU
            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            showEditPanelInfo = False

            Try
                If Me.IsPostBack = False Then
                    With Me.m_objDataSet_Main.Tables(strTable)
                        If .Rows.Count < 1 Then
                            Me.txtZNBZYWZ.Text = ""
                            Me.txtZFTPFWQ.Text = ""
                            Me.txtZFTPDK.Text = ""
                            Me.txtZFTPYH.Text = ""
                            Me.txtZFTPMM.Value = ""

                            Me.txtCNBZYWZ.Text = ""
                            Me.txtCFTPFWQ.Text = ""
                            Me.txtCFTPDK.Text = ""
                            Me.txtCFTPYH.Text = ""
                            Me.txtCFTPMM.Value = ""

                            Me.htxtBS.Value = ""
                            Me.htxtSFJM.Value = ""
                        Else
                            Me.txtZNBZYWZ.Text = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_ZNBZYWZ), "")
                            Me.txtZFTPFWQ.Text = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_ZFTPFWQ), "")
                            Me.txtZFTPDK.Text = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_ZFTPDK), "")
                            Me.txtZFTPYH.Text = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_ZFTPYH), "")
                            Me.txtZFTPMM.Value = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_ZFTPMM), "")

                            Me.txtCNBZYWZ.Text = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_CNBZYWZ), "")
                            Me.txtCFTPFWQ.Text = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_CFTPFWQ), "")
                            Me.txtCFTPDK.Text = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_CFTPDK), "")
                            Me.txtCFTPYH.Text = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_CFTPYH), "")
                            Me.txtCFTPMM.Value = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_CFTPMM), "")

                            Me.htxtBS.Value = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_BS), "")
                            Me.htxtSFJM.Value = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_SFJM), "0")

                            If Me.htxtSFJM.Value = "1" Then
                                '从加密数据中获取密码
                                Dim strPassword As String
                                Dim bData As Byte()

                                strPassword = ""
                                Try
                                    bData = CType(.Rows(0).Item(Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_ZFTPMMJM), Byte())
                                    objPulicParameters.doDecryptString(strErrMsg, bData, strPassword)
                                Catch ex As Exception
                                End Try
                                Me.txtZFTPMM.Value = strPassword

                                strPassword = ""
                                Try
                                    bData = CType(.Rows(0).Item(Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_CFTPMMJM), Byte())
                                    objPulicParameters.doDecryptString(strErrMsg, bData, strPassword)
                                Catch ex As Exception
                                End Try
                                Me.txtCFTPMM.Value = strPassword
                            End If
                        End If
                    End With

                    '缓存密码数据
                    Me.m_strhtxtSessionIdZFTPMM = objPulicParameters.getNewGuid()
                    Me.htxtSessionIdZFTPMM.Value = Me.m_strhtxtSessionIdZFTPMM
                    Session.Add(Me.m_strhtxtSessionIdZFTPMM, Me.txtZFTPMM.Value)

                    Me.m_strhtxtSessionIdCFTPMM = objPulicParameters.getNewGuid()
                    Me.htxtSessionIdCFTPMM.Value = Me.m_strhtxtSessionIdCFTPMM
                    Session.Add(Me.m_strhtxtSessionIdCFTPMM, Me.txtCFTPMM.Value)
                Else
                    '自动恢复数据,密码数据不能自动恢复！
                End If

                '使能控件
                With New Xydc.Platform.web.ControlProcess
                    .doEnabledControl(Me.txtZNBZYWZ, blnEditMode)
                    .doEnabledControl(Me.txtZFTPFWQ, blnEditMode)
                    .doEnabledControl(Me.txtZFTPDK, blnEditMode)
                    .doEnabledControl(Me.txtZFTPYH, blnEditMode)
                    .doEnabledControl(Me.txtZFTPMM, blnEditMode)

                    .doEnabledControl(Me.txtCNBZYWZ, blnEditMode)
                    .doEnabledControl(Me.txtCFTPFWQ, blnEditMode)
                    .doEnabledControl(Me.txtCFTPDK, blnEditMode)
                    .doEnabledControl(Me.txtCFTPYH, blnEditMode)
                    .doEnabledControl(Me.txtCFTPMM, blnEditMode)
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            showEditPanelInfo = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示整个模块的信息
        '     strErrMsg      ：返回错误信息
        '     blnEditMode    ：当前编辑状态
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showModuleData_Main( _
            ByRef strErrMsg As String, _
            ByVal blnEditMode As Boolean) As Boolean

            showModuleData_Main = False

            Try
                '显示输入窗信息
                If Me.showEditPanelInfo(strErrMsg, blnEditMode) = False Then
                    GoTo errProc
                End If

                '显示操作命令
                Me.btnOK.Visible = blnEditMode
                Me.btnCancel.Visible = blnEditMode
                Me.btnClose.Visible = Not blnEditMode

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            showModuleData_Main = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 初始化控件
        '----------------------------------------------------------------
        Private Function initializeControls(ByRef strErrMsg As String) As Boolean

            initializeControls = False

            Try
                If Me.IsPostBack = False Then
                    '仅在第一次调用页面时执行

                    '设置初始显示的静态信息

                    '根据接口参数设置不受数据影响的操作的状态

                    '显示Pannel(不论是否回调，始终显示panelMain)
                    Me.panelMain.Visible = True
                    Me.panelError.Visible = Not Me.panelMain.Visible

                    '执行键转译(不论是否是“回发”)
                    With New Xydc.Platform.web.ControlProcess
                        .doTranslateKey(Me.txtZNBZYWZ)
                        .doTranslateKey(Me.txtZFTPFWQ)
                        .doTranslateKey(Me.txtZFTPDK)
                        .doTranslateKey(Me.txtZFTPYH)
                        .doTranslateKey(Me.txtZFTPMM)

                        .doTranslateKey(Me.txtCNBZYWZ)
                        .doTranslateKey(Me.txtCFTPFWQ)
                        .doTranslateKey(Me.txtCFTPDK)
                        .doTranslateKey(Me.txtCFTPYH)
                        .doTranslateKey(Me.txtCFTPMM)
                    End With

                    '获取数据
                    If Me.getModuleData_Main(strErrMsg, "") = False Then
                        GoTo errProc
                    End If
                    If Me.showModuleData_Main(strErrMsg, Me.m_blnEditMode) = False Then
                        GoTo errProc
                    End If
                Else
                    If Me.doRestorePassword(strErrMsg) = False Then
                        GoTo errProc
                    End If
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            initializeControls = True
            Exit Function

errProc:
            Exit Function

        End Function

        Private Function doBufferPassword(ByRef strErrMsg As String) As Boolean

            doBufferPassword = False
            strErrMsg = ""

            Try
                Session.Item(Me.m_strhtxtSessionIdZFTPMM) = Me.txtZFTPMM.Value
                Session.Item(Me.m_strhtxtSessionIdCFTPMM) = Me.txtCFTPMM.Value
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doBufferPassword = True
errProc:
            Exit Function

        End Function

        Private Function doRestorePassword(ByRef strErrMsg As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            doRestorePassword = False
            strErrMsg = ""

            Try
                If Me.txtZFTPMM.Value.Trim = "" Then
                    Me.txtZFTPMM.Value = objPulicParameters.getObjectValue(Session.Item(Me.m_strhtxtSessionIdZFTPMM), "")
                Else
                    '缓存密码数据
                    Session.Item(Me.m_strhtxtSessionIdZFTPMM) = Me.txtZFTPMM.Value.Trim
                End If
                If Me.txtCFTPMM.Value.Trim = "" Then
                    Me.txtCFTPMM.Value = objPulicParameters.getObjectValue(Session.Item(Me.m_strhtxtSessionIdCFTPMM), "")
                Else
                    '缓存密码数据
                    Session.Item(Me.m_strhtxtSessionIdCFTPMM) = Me.txtCFTPMM.Value.Trim
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            doRestorePassword = True
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String
            Dim strUrl As String

            Try
                '预处理
                If MyBase.doPagePreprocess(True, Me.IsPostBack And Me.m_blnSaveScence) = True Then
                    Exit Sub
                End If

                '检查权限(不论是否回发！)
                Dim blnDo As Boolean
                If Me.getPrevilegeParams(strErrMsg, blnDo) = False Then
                    GoTo errProc
                End If
                If blnDo = False Then Exit Try

                '获取接口参数
                If Me.getInterfaceParameters(strErrMsg) = False Then
                    GoTo errProc
                End If

                '控件初始化
                If Me.initializeControls(strErrMsg) = False Then
                    GoTo errProc
                End If

                '具体审计日志
                If Me.IsPostBack = False Then
                    If Me.m_blnSaveScence = False Then
                        Xydc.Platform.SystemFramework.ApplicationLog.WriteAuditPZInfo(Request.UserHostAddress, Request.UserHostName, "[" + MyBase.UserId + "]访问了[系统运行配置参数]信息！")
                    End If
                End If

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



        Private Sub doCancel(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String
            Dim intStep As Integer

            Try
                '询问
                intStep = 1
                If objMessageProcess.isExecuteCode(Request, Me.popMessageObject.UniqueID, intStep) = True Then
                    objMessageProcess.doConfirmMessage(Me.popMessageObject, "警告：您确定要取消录入的内容吗（是/否）？", strControlId, intStep)
                    Exit Try
                Else
                    objMessageProcess.doResetPopMessage(Me.popMessageObject)
                End If

                '返回处理
                intStep = 2
                If objMessageProcess.isExecuteCode(Request, Me.popMessageObject.UniqueID, intStep) = True Then
                    '释放模块资源
                    Me.releaseModuleParameters()
                    Me.releaseInterfaceParameters()

                    '返回到欢迎页面
                    Dim strUrl As String = Xydc.Platform.Common.jsoaConfiguration.GeneralReturnUrl
                    If strUrl <> "" Then
                        Response.Redirect(strUrl)
                    End If
                End If

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

        Private Sub doClose(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '释放模块资源
                Me.releaseModuleParameters()
                Me.releaseInterfaceParameters()

                '返回到欢迎页面
                Dim strUrl As String = Xydc.Platform.Common.jsoaConfiguration.GeneralReturnUrl
                If strUrl <> "" Then
                    Response.Redirect(strUrl)
                End If

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

        Private Sub doConfirm(ByVal strControlId As String)

            Dim objsystemXitongpeizhi As New Xydc.Platform.BusinessFacade.systemXitongpeizhi
            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '缓存密码数据
                If Me.doBufferPassword(strErrMsg) = False Then
                    GoTo errProc
                End If

                '加密密码数据
                Dim bDataZ As Byte()
                If objPulicParameters.doEncryptString(strErrMsg, Me.txtZFTPMM.Value, bDataZ) = False Then
                    GoTo errProc
                End If
                Dim bDataC As Byte()
                If objPulicParameters.doEncryptString(strErrMsg, Me.txtCFTPMM.Value, bDataC) = False Then
                    GoTo errProc
                End If

                '获取数据
                Dim strWhere As String
                Dim strBS As String
                strBS = Me.htxtBS.Value.Trim()
                If strBS = "" Then strBS = "-1"
                strWhere = "a." + Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_BS + " = " + strBS
                If Me.getModuleData_Main(strErrMsg, strWhere) = False Then
                    GoTo errProc
                End If
                Dim objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType
                Dim objOldData As System.Data.DataRow
                objenumEditType = Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                Try
                    With Me.m_objDataSet_Main.Tables(Xydc.Platform.Common.Data.XitongcanshuData.TABLE_GL_B_XITONGCANSHU)
                        If .Rows.Count > 0 Then
                            objenumEditType = Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eUpdate
                            objOldData = .Rows(0)
                        End If
                    End With
                Catch ex As Exception
                End Try

                '准备保存的数据
                Dim objNewData As New System.Collections.Specialized.ListDictionary
                objNewData.Add(Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_SFJM, "1")
                '主服务器信息
                objNewData.Add(Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_ZNBZYWZ, Me.txtZNBZYWZ.Text)
                objNewData.Add(Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_ZFTPFWQ, Me.txtZFTPFWQ.Text)
                objNewData.Add(Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_ZFTPDK, Me.txtZFTPDK.Text)
                objNewData.Add(Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_ZFTPYH, Me.txtZFTPYH.Text)
                objNewData.Add(Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_ZFTPMM, Me.txtZFTPMM.Value)
                '备用服务器信息
                objNewData.Add(Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_CNBZYWZ, Me.txtCNBZYWZ.Text)
                objNewData.Add(Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_CFTPFWQ, Me.txtCFTPFWQ.Text)
                objNewData.Add(Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_CFTPDK, Me.txtCFTPDK.Text)
                objNewData.Add(Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_CFTPYH, Me.txtCFTPYH.Text)
                objNewData.Add(Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_CFTPMM, Me.txtCFTPMM.Value)
                '密码加密信息
                objNewData.Add(Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_ZFTPMMJM, bDataZ)
                objNewData.Add(Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_CFTPMMJM, bDataC)

                '保存数据
                If objsystemXitongpeizhi.doSaveXitongcanshuData(strErrMsg, MyBase.UserId, MyBase.UserPassword, objOldData, objNewData, objenumEditType) = False Then
                    GoTo errProc
                End If

                '记录审计日志
                Xydc.Platform.SystemFramework.ApplicationLog.WriteAuditPZInfo(Request.UserHostAddress, Request.UserHostName, "[" + MyBase.UserId + "]修改了[系统运行配置参数]！")

                '释放模块资源
                Me.releaseModuleParameters()
                Me.releaseInterfaceParameters()

                '返回到欢迎页面
                Dim strUrl As String = Xydc.Platform.Common.jsoaConfiguration.GeneralReturnUrl
                If strUrl <> "" Then
                    Response.Redirect(strUrl)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.BusinessFacade.systemXitongpeizhi.SafeRelease(objsystemXitongpeizhi)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.BusinessFacade.systemXitongpeizhi.SafeRelease(objsystemXitongpeizhi)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
            Me.doClose("btnClose")
        End Sub

        Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
            Me.doCancel("btnCancel")
        End Sub

        Private Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.Click
            Me.doConfirm("btnOK")
        End Sub

    End Class
End Namespace
