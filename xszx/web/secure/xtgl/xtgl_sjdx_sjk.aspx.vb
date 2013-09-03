Imports System.Web.Security

Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.web
    ' 类名    ：xtgl_sjdx_sjk
    ' 
    ' 调用性质：
    '     可被其他模块调用，本身也调用其他模块
    '
    ' 功能描述： 
    '   　服务器信息编辑处理
    '
    ' 接口参数：
    '     参见IXtglSjdxSjk接口类描述
    '----------------------------------------------------------------

    Partial Public Class xtgl_sjdx_sjk
        Inherits Xydc.Platform.web.PageBase


        '----------------------------------------------------------------
        '模块私用参数
        '----------------------------------------------------------------
        '本模块相对image的相对路径
        Private m_cstrRelativePathToImage As String = "../../"

        '----------------------------------------------------------------
        '模块授权参数
        '----------------------------------------------------------------

        '----------------------------------------------------------------
        '模块现场保留参数，恢复完成后立即释放session资源
        '----------------------------------------------------------------
        Private m_objSaveScence As Xydc.Platform.BusinessFacade.IMXtglSjdxSjk
        Private m_blnSaveScence As Boolean

        '----------------------------------------------------------------
        '模块接口参数
        '----------------------------------------------------------------
        Private m_objInterface As Xydc.Platform.BusinessFacade.IXtglSjdxSjk
        Private m_blnInterface As Boolean

        '----------------------------------------------------------------
        '模块访问数据参数
        '----------------------------------------------------------------
        '显示用数据
        Private m_objDataSet As Xydc.Platform.Common.Data.AppManagerData
        '编辑用数据
        Private m_objDataSet_Edit As Xydc.Platform.Common.Data.AppManagerData

        '----------------------------------------------------------------
        '模块其他参数
        '----------------------------------------------------------------
        Private m_blnEditMode As Boolean '编辑模式
        Private m_objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType '具体操作模式










        '----------------------------------------------------------------
        ' 复原模块现场信息并释放相应的资源
        '----------------------------------------------------------------
        Private Sub restoreModuleInformation(ByVal strSessionId As String)

            Try
                If Me.m_objSaveScence Is Nothing Then Exit Try

                With Me.m_objSaveScence
                    Me.txtFWQMC.Text = .txtFWQMC
                    Me.txtSJKMC.Text = .txtSJKMC
                    Me.txtSJKZWM.Text = .txtSJKZWM
                    Me.txtSJKSM.Text = .txtSJKSM
                End With

                '释放资源
                Session.Remove(strSessionId)
                Me.m_objSaveScence.Dispose()
                Me.m_objSaveScence = Nothing

            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' 保存模块现场信息并返回相应的SessionId
        '----------------------------------------------------------------
        Private Function saveModuleInformation() As String

            Dim strSessionId As String = ""

            saveModuleInformation = ""

            Try
                '创建SessionId
                With New Xydc.Platform.Common.Utilities.PulicParameters
                    strSessionId = .getNewGuid()
                End With
                If strSessionId = "" Then Exit Try

                '创建对象
                Me.m_objSaveScence = New Xydc.Platform.BusinessFacade.IMXtglSjdxSjk

                '保存现场信息
                With Me.m_objSaveScence
                    .txtFWQMC = Me.txtFWQMC.Text
                    .txtSJKMC = Me.txtSJKMC.Text
                    .txtSJKZWM = Me.txtSJKZWM.Text
                    .txtSJKSM = Me.txtSJKSM.Text
                End With

                '缓存对象
                Session.Add(strSessionId, Me.m_objSaveScence)

            Catch ex As Exception
            End Try

            saveModuleInformation = strSessionId

        End Function

        '----------------------------------------------------------------
        ' 从调用模块中获取数据
        '----------------------------------------------------------------
        Private Function getDataFromCallModule( _
            ByRef strErrMsg As String) As Boolean

            Try
                If Me.IsPostBack = True Then Exit Try
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
        ' 释放接口参数
        '----------------------------------------------------------------
        Private Sub releaseInterfaceParameters()

            Try
                If Not (Me.m_objInterface Is Nothing) Then
                    If Me.m_objInterface.iInterfaceType = Xydc.Platform.BusinessFacade.ICallInterface.enumInterfaceType.InputOnly Then
                        '释放Session
                        Session.Remove(Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.ISessionId))
                        '释放对象
                        Me.m_objInterface.Dispose()
                        Me.m_objInterface = Nothing
                    End If
                End If
            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' 获取接口参数(没有接口参数则显示错误信息页面)
        '----------------------------------------------------------------
        Private Function getInterfaceParameters(ByRef strErrMsg As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            getInterfaceParameters = False

            Try
                '从QueryString中解析接口参数(不论是否回发)
                Dim objTemp As Object
                Try
                    objTemp = Session.Item(Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.ISessionId))
                    m_objInterface = CType(objTemp, Xydc.Platform.BusinessFacade.IXtglSjdxSjk)
                Catch ex As Exception
                    m_objInterface = Nothing
                End Try

                '必须有接口参数
                Me.m_blnInterface = False
                If m_objInterface Is Nothing Then
                    '显示错误信息
                    Me.panelError.Visible = True
                    Me.panelMain.Visible = Not Me.panelError.Visible
                    strErrMsg = "本模块必须提供输入接口参数！"
                    GoTo errProc
                End If
                Me.m_blnInterface = True

                '获取恢复现场参数
                Me.m_blnSaveScence = False
                If Me.IsPostBack = False Then
                    Dim strSessionId As String
                    strSessionId = objPulicParameters.getObjectValue(Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.MSessionId), "")
                    Try
                        Me.m_objSaveScence = CType(Session.Item(strSessionId), Xydc.Platform.BusinessFacade.IMXtglSjdxSjk)
                    Catch ex As Exception
                        Me.m_objSaveScence = Nothing
                    End Try
                    If Me.m_objSaveScence Is Nothing Then
                        Me.m_blnSaveScence = False
                    Else
                        Me.m_blnSaveScence = True
                    End If

                    '恢复现场参数后释放该资源
                    Me.restoreModuleInformation(strSessionId)

                    '处理调用模块返回后的信息并同时释放相应资源
                    If Me.getDataFromCallModule(strErrMsg) = False Then
                        GoTo errProc
                    End If
                End If

                '设置模块其他参数
                Me.m_objenumEditType = Me.m_objInterface.iEditMode
                Select Case Me.m_objInterface.iEditMode
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                        Me.m_blnEditMode = True
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eCpyNew
                        Me.m_blnEditMode = True
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eUpdate
                        Me.m_blnEditMode = True
                    Case Else
                        Me.m_blnEditMode = False
                End Select

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            getInterfaceParameters = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 释放本模块缓存的参数
        '----------------------------------------------------------------
        Private Sub releaseModuleParameters()
        End Sub

        '----------------------------------------------------------------
        ' 获取模块要显示的数据信息
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function getModuleData( _
            ByRef strErrMsg As String) As Boolean

            Dim objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty
            Dim objsystemAppManager As New Xydc.Platform.BusinessFacade.systemAppManager

            getModuleData = False

            Try
                Dim strServerName As String
                Dim strDBName As String
                strServerName = Me.m_objInterface.iFWQMC
                strDBName = Me.m_objInterface.iSJKMC

                '释放资源
                Xydc.Platform.Common.Data.AppManagerData.SafeRelease(Me.m_objDataSet)

                '根据输入参数获取信息
                If objsystemAppManager.getServerConnectionProperty(strErrMsg, MyBase.UserId, MyBase.UserPassword, Me.m_objInterface.iFWQMC, objConnectionProperty) = False Then
                    GoTo errProc
                End If
                objConnectionProperty.InitialCatalog = strDBName

                '重新检索数据
                Dim strWhere As String
                strWhere = "a." + Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_SHUJUKU_SJKM + " = '" + strDBName + "'"
                If objsystemAppManager.getShujukuData(strErrMsg, objConnectionProperty, strWhere, Me.m_objDataSet) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ConnectionProperty.SafeRelease(objConnectionProperty)
            Xydc.Platform.BusinessFacade.systemAppManager.SafeRelease(objsystemAppManager)

            getModuleData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ConnectionProperty.SafeRelease(objConnectionProperty)
            Xydc.Platform.BusinessFacade.systemAppManager.SafeRelease(objsystemAppManager)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取模块要显示的数据信息
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function getModuleData_Edit( _
            ByRef strErrMsg As String) As Boolean

            Dim objsystemAppManager As New Xydc.Platform.BusinessFacade.systemAppManager

            getModuleData_Edit = False

            Try
                Dim strServerName As String
                Dim strDBName As String
                strServerName = Me.m_objInterface.iFWQMC
                strDBName = Me.m_objInterface.iSJKMC

                '释放资源
                If Not (Me.m_objDataSet_Edit Is Nothing) Then
                    Me.m_objDataSet_Edit.Dispose()
                    Me.m_objDataSet_Edit = Nothing
                End If

                '重新检索数据
                If objsystemAppManager.getShujukuData(strErrMsg, MyBase.UserId, MyBase.UserPassword, strServerName, strDBName, "", Me.m_objDataSet_Edit) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.BusinessFacade.systemAppManager.SafeRelease(objsystemAppManager)

            getModuleData_Edit = True
            Exit Function

errProc:
            Xydc.Platform.BusinessFacade.systemAppManager.SafeRelease(objsystemAppManager)
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

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objsystemAppManager As New Xydc.Platform.BusinessFacade.systemAppManager

            showEditPanelInfo = False

            Try
                If Me.IsPostBack = False Then
                    '获取现场信息
                    Dim strSessionId As String
                    strSessionId = Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.MSessionId)
                    If strSessionId Is Nothing Then strSessionId = ""
                    strSessionId = strSessionId.Trim()

                    If strSessionId = "" Then
                        '不是恢复现场时
                        With Me.m_objDataSet.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_SHUJUKU)
                            If .Rows.Count < 1 Then
                                Me.txtFWQMC.Text = ""
                                Me.txtSJKMC.Text = ""
                                Me.txtSJKZWM.Text = ""
                                Me.txtSJKSM.Text = ""
                            Else
                                Me.txtFWQMC.Text = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_SHUJUKU_FWQM), "")
                                Me.txtSJKMC.Text = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_SHUJUKU_SJKM), "")
                                Me.txtSJKZWM.Text = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_SHUJUKU_SJKZWM), "")
                                Me.txtSJKSM.Text = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_SHUJUKU_SM), "")
                            End If
                        End With
                    Else
                        '已经通过现场恢复获取控件值
                    End If
                Else
                    '自动恢复数据
                End If

                '使能控件
                With New Xydc.Platform.web.ControlProcess
                    .doEnabledControl(Me.txtFWQMC, False)
                    .doEnabledControl(Me.txtSJKMC, False)
                    .doEnabledControl(Me.txtSJKZWM, blnEditMode)
                    .doEnabledControl(Me.txtSJKSM, blnEditMode)
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.BusinessFacade.systemAppManager.SafeRelease(objsystemAppManager)

            showEditPanelInfo = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.BusinessFacade.systemAppManager.SafeRelease(objsystemAppManager)
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
        Private Function showModuleData( _
            ByRef strErrMsg As String, _
            ByVal blnEditMode As Boolean) As Boolean

            Dim objControlProcess As New Xydc.Platform.web.ControlProcess

            showModuleData = False

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

            Xydc.Platform.web.ControlProcess.SafeRelease(objControlProcess)

            showModuleData = True
            Exit Function

errProc:
            Xydc.Platform.web.ControlProcess.SafeRelease(objControlProcess)
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
                    '设置初始显示的静态信息

                    '根据接口参数设置不受数据影响的操作的状态

                    '显示Pannel(不论是否回调，始终显示panelMain)
                    Me.panelMain.Visible = True
                    Me.panelError.Visible = Not Me.panelMain.Visible

                    '执行键转译(不论是否是“回发”)
                    With New Xydc.Platform.web.ControlProcess
                        .doTranslateKey(Me.txtFWQMC)
                        .doTranslateKey(Me.txtSJKMC)
                        .doTranslateKey(Me.txtSJKZWM)

                        '.doTranslateKey(Me.txtSJKSM)

                    End With

                    '获取数据
                    If Me.getModuleData(strErrMsg) = False Then
                        GoTo errProc
                    End If
                    If Me.showModuleData(strErrMsg, Me.m_blnEditMode) = False Then
                        GoTo errProc
                    End If

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
            If MyBase.doPagePreprocess(True, Me.IsPostBack And Me.m_blnSaveScence) = True Then
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

            '记录审计日志
            If Me.IsPostBack = False Then
                If Me.m_blnSaveScence = False Then
                    Select Case Me.m_objenumEditType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew, _
                            Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eCpyNew
                        Case Else
                            Xydc.Platform.SystemFramework.ApplicationLog.WriteAuditAQInfo(Request.UserHostAddress, Request.UserHostName, "[" + MyBase.UserId + "]访问了[" + Me.txtSJKMC.Text + "]数据库注册信息！")
                    End Select
                End If
            End If

            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub



        '----------------------------------------------------------------
        '模块特殊操作处理器
        '----------------------------------------------------------------
        '处理“btnCancel”按钮
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
                    '设置返回参数
                    Me.m_objInterface.oExitMode = False

                    '返回到调用模块，并附加返回参数
                    '要返回的SessionId
                    Dim strSessionId As String
                    strSessionId = Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.ISessionId)
                    'SessionId附加到返回的Url
                    Dim strUrl As String
                    strUrl = Me.m_objInterface.getReturnUrl(Server, Xydc.Platform.Common.Utilities.PulicParameters.OSessionId, strSessionId)

                    '释放模块资源
                    Me.releaseModuleParameters()
                    Me.releaseInterfaceParameters()

                    '返回
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

        '处理“btnClose”按钮
        Private Sub doClose(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '设置返回参数
                Me.m_objInterface.oExitMode = False

                '返回到调用模块，并附加返回参数
                '要返回的SessionId
                Dim strSessionId As String
                strSessionId = Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.ISessionId)
                'SessionId附加到返回的Url
                Dim strUrl As String
                strUrl = Me.m_objInterface.getReturnUrl(Server, Xydc.Platform.Common.Utilities.PulicParameters.OSessionId, strSessionId)

                '释放模块资源
                Me.releaseModuleParameters()
                Me.releaseInterfaceParameters()

                '返回
                Response.Redirect(strUrl)

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

        '处理“btnOK”按钮
        Private Sub doConfirm(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '准备保存信息
                Dim objNewData As New System.Collections.Specialized.ListDictionary
                objNewData.Add(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_SHUJUKU_FWQM, Me.txtFWQMC.Text)
                objNewData.Add(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_SHUJUKU_SJKM, Me.txtSJKMC.Text)
                objNewData.Add(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_SHUJUKU_SJKZWM, Me.txtSJKZWM.Text)
                objNewData.Add(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_SHUJUKU_SM, Me.txtSJKSM.Text)

                '保存信息
                Dim objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType
                With New Xydc.Platform.BusinessFacade.systemAppManager
                    '获取现有记录
                    If Me.getModuleData_Edit(strErrMsg) = False Then
                        GoTo errProc
                    End If

                    '判断编辑模式
                    With Me.m_objDataSet_Edit.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_SHUJUKU)
                        If .Rows.Count < 1 Then
                            objenumEditType = Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                        Else
                            objenumEditType = Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eUpdate
                        End If
                    End With

                    '处理
                    Select Case objenumEditType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew, _
                            Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eCpyNew
                            If .doSaveShujukuData(strErrMsg, MyBase.UserId, MyBase.UserPassword, Nothing, objNewData, objenumEditType) = False Then
                                GoTo errProc
                            End If
                        Case Else
                            Dim objOldData As System.Data.DataRow
                            With Me.m_objDataSet_Edit.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_SHUJUKU)
                                If .Rows.Count < 1 Then
                                    strErrMsg = "错误：没有当前记录！"
                                    GoTo errProc
                                End If
                                objOldData = .Rows(0)
                            End With
                            '保存新记录
                            If .doSaveShujukuData(strErrMsg, MyBase.UserId, MyBase.UserPassword, objOldData, objNewData, objenumEditType) = False Then
                                GoTo errProc
                            End If
                    End Select
                End With

                '记录审计日志
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew, _
                        Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eCpyNew
                        Xydc.Platform.SystemFramework.ApplicationLog.WriteAuditAQInfo(Request.UserHostAddress, Request.UserHostName, "[" + MyBase.UserId + "]注册了[" + Me.txtSJKMC.Text + "]数据库！")
                    Case Else
                        Xydc.Platform.SystemFramework.ApplicationLog.WriteAuditAQInfo(Request.UserHostAddress, Request.UserHostName, "[" + MyBase.UserId + "]修改了[" + Me.txtSJKMC.Text + "]数据库注册信息！")
                End Select

                '设置返回参数
                With Me.m_objInterface
                    .oExitMode = True
                    .oFWQMC = Me.txtFWQMC.Text
                    .oSJKMC = Me.txtSJKMC.Text
                End With

                '返回到调用模块，并附加返回参数
                '要返回的SessionId
                Dim strSessionId As String
                strSessionId = Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.ISessionId)
                'SessionId附加到返回的Url
                Dim strUrl As String
                strUrl = Me.m_objInterface.getReturnUrl(Server, Xydc.Platform.Common.Utilities.PulicParameters.OSessionId, strSessionId)

                '释放模块资源
                Me.releaseModuleParameters()
                Me.releaseInterfaceParameters()

                '返回
                Response.Redirect(strUrl)

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

        Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
            Me.doCancel("btnCancel")
        End Sub

        Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
            Me.doClose("btnClose")
        End Sub

        Private Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.Click
            Me.doConfirm("btnOK")
        End Sub

    End Class
End Namespace
