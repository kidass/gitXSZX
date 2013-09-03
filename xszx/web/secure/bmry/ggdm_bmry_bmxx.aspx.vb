Imports System.Web.Security

Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.web
    ' 类名    ：ggdm_bmry_bmxx
    ' 
    ' 调用性质：
    '     可被其他模块调用，本身也调用其他模块
    '
    ' 功能描述： 
    '   　基础代码选择处理模块。
    '
    ' 接口参数：
    '     参见IGgdmBmryBmxx接口类描述
    '----------------------------------------------------------------

    Partial Public Class ggdm_bmry_bmxx
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
        Private m_objSaveScence As Xydc.Platform.BusinessFacade.IMGgdmBmryBmxx
        Private m_blnSaveScence As Boolean

        '----------------------------------------------------------------
        '模块接口参数
        '----------------------------------------------------------------
        Private m_objInterface As Xydc.Platform.BusinessFacade.IGgdmBmryBmxx
        Private m_blnInterface As Boolean

        '----------------------------------------------------------------
        '模块访问数据参数
        '----------------------------------------------------------------
        Private m_objDataSet As Xydc.Platform.Common.Data.CustomerData

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
                    Me.txtZZDM.Text = .txtZZDM
                    Me.txtZZMC.Text = .txtZZMC
                    Me.txtZZBM.Text = .txtZZBM
                    Me.txtJBMC.Text = .txtJBMC
                    Me.htxtJBDM.Value = .htxtJBDM
                    Me.txtMSMC.Text = .txtMSMC
                    Me.htxtMSDM.Value = .htxtMSDM
                    Me.txtLXDH.Text = .txtLXDH
                    Me.txtSJHM.Text = .txtSJHM
                    Me.txtFTPDZ.Text = .txtFTPDZ
                    Me.txtYXDZ.Text = .txtYXDZ
                    Me.txtLXDZ.Text = .txtLXDZ
                    Me.txtYZBM.Text = .txtYZBM
                    Me.txtLXR.Text = .txtLXR
                    Me.htxtLXRDM.Value = .htxtLXRDM
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
                Me.m_objSaveScence = New Xydc.Platform.BusinessFacade.IMGgdmBmryBmxx

                '保存现场信息
                With Me.m_objSaveScence
                    .txtZZDM = Me.txtZZDM.Text
                    .txtZZMC = Me.txtZZMC.Text
                    .txtZZBM = Me.txtZZBM.Text
                    .txtJBMC = Me.txtJBMC.Text
                    .htxtJBDM = Me.htxtJBDM.Value
                    .txtMSMC = Me.txtMSMC.Text
                    .htxtMSDM = Me.htxtMSDM.Value
                    .txtLXDH = Me.txtLXDH.Text
                    .txtSJHM = Me.txtSJHM.Text
                    .txtFTPDZ = Me.txtFTPDZ.Text
                    .txtYXDZ = Me.txtYXDZ.Text
                    .txtLXDZ = Me.txtLXDZ.Text
                    .txtYZBM = Me.txtYZBM.Text
                    .txtLXR = Me.txtLXR.Text
                    .htxtLXRDM = Me.htxtLXRDM.Value
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

            Dim objsystemXingzhengjibie As New Xydc.Platform.BusinessFacade.systemXingzhengjibie
            Dim objsystemCustomer As New Xydc.Platform.BusinessFacade.systemCustomer

            Try
                Dim strCode As String

                If Me.IsPostBack = True Then Exit Try

                '=================================================================
                Dim objIDmxzJbdm As Xydc.Platform.BusinessFacade.IDmxzJbdm
                Try
                    objIDmxzJbdm = CType(Session.Item(Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.OSessionId)), Xydc.Platform.BusinessFacade.IDmxzJbdm)
                Catch ex As Exception
                    objIDmxzJbdm = Nothing
                End Try
                If Not (objIDmxzJbdm Is Nothing) Then
                    '返回值处理
                    Select Case objIDmxzJbdm.iSourceControlId.ToUpper()
                        Case "btnSelectJBDM".ToUpper()
                            '处理btnSelectJBDM返回
                            If objIDmxzJbdm.oExitMode = True Then
                                Me.txtJBMC.Text = objIDmxzJbdm.oNameValue
                                Me.txtJBMC.Text = Me.txtJBMC.Text.Trim()
                                If Me.txtJBMC.Text <> "" Then
                                    '根据名称获取级别代码
                                    objsystemXingzhengjibie.getJbdmByJbmc(strErrMsg, MyBase.UserId, MyBase.UserPassword, Me.txtJBMC.Text, strCode)
                                    Me.htxtJBDM.Value = strCode
                                Else
                                    Me.htxtJBDM.Value = ""
                                End If
                            End If
                        Case Else
                    End Select
                    '释放资源
                    Session.Remove(Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.OSessionId))
                    objIDmxzJbdm.Dispose()
                    objIDmxzJbdm = Nothing
                    Exit Try
                End If

                '=================================================================
                Dim objIDmxzZzry As Xydc.Platform.BusinessFacade.IDmxzZzry
                Try
                    objIDmxzZzry = CType(Session.Item(Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.OSessionId)), Xydc.Platform.BusinessFacade.IDmxzZzry)
                Catch ex As Exception
                    objIDmxzZzry = Nothing
                End Try
                If Not (objIDmxzZzry Is Nothing) Then
                    '返回值处理
                    Select Case objIDmxzZzry.iSourceControlId.ToUpper()
                        Case "btnSelectMSDM".ToUpper()
                            '处理btnSelectMSDM返回
                            If objIDmxzZzry.oExitMode = True Then
                                Me.txtMSMC.Text = objIDmxzZzry.oRenyuanList
                                Me.txtMSMC.Text = Me.txtMSMC.Text.Trim()
                                If Me.txtMSMC.Text <> "" Then
                                    '根据名称获取人员代码
                                    objsystemCustomer.getRydmByRymc(strErrMsg, MyBase.UserId, MyBase.UserPassword, Me.txtMSMC.Text, strCode)
                                    Me.htxtMSDM.Value = strCode
                                Else
                                    Me.htxtMSDM.Value = ""
                                End If
                            End If
                        Case "btnSelectLXR".ToUpper()
                            '处理btnSelectLXR返回
                            If objIDmxzZzry.oExitMode = True Then
                                Me.txtLXR.Text = objIDmxzZzry.oRenyuanList
                                Me.txtLXR.Text = Me.txtLXR.Text.Trim()
                                If Me.txtLXR.Text <> "" Then
                                    '根据名称获取人员代码
                                    objsystemCustomer.getRydmByRymc(strErrMsg, MyBase.UserId, MyBase.UserPassword, Me.txtLXR.Text, strCode)
                                    Me.htxtLXRDM.Value = strCode
                                Else
                                    Me.htxtLXRDM.Value = ""
                                End If
                            End If
                        Case Else
                    End Select
                    '释放资源
                    Session.Remove(Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.OSessionId))
                    objIDmxzZzry.Dispose()
                    objIDmxzZzry = Nothing
                    Exit Try
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.BusinessFacade.systemXingzhengjibie.SafeRelease(objsystemXingzhengjibie)
            Xydc.Platform.BusinessFacade.systemCustomer.SafeRelease(objsystemCustomer)

            getDataFromCallModule = True
            Exit Function
errProc:
            Xydc.Platform.BusinessFacade.systemXingzhengjibie.SafeRelease(objsystemXingzhengjibie)
            Xydc.Platform.BusinessFacade.systemCustomer.SafeRelease(objsystemCustomer)
            Exit Function

        End Function

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
                    m_objInterface = CType(objTemp, Xydc.Platform.BusinessFacade.IGgdmBmryBmxx)
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
                        Me.m_objSaveScence = CType(Session.Item(strSessionId), Xydc.Platform.BusinessFacade.IMGgdmBmryBmxx)
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
        '     strZZDM        ：要获取的组织代码
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function getModuleData( _
            ByRef strErrMsg As String, _
            ByVal strZZDM As String) As Boolean

            getModuleData = False

            Try
                '释放资源
                Xydc.Platform.Common.Data.CustomerData.SafeRelease(Me.m_objDataSet)

                '重新检索数据
                With New Xydc.Platform.BusinessFacade.systemCustomer
                    If .getBumenData(strErrMsg, MyBase.UserId, MyBase.UserPassword, strZZDM, Me.m_objDataSet) = False Then
                        GoTo errProc
                    End If
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getModuleData = True
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

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

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
                        With Me.m_objDataSet.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU_FULLJOIN)
                            If .Rows.Count < 1 Then
                                Me.txtZZDM.Text = ""
                                Me.txtZZMC.Text = ""
                                Me.txtZZBM.Text = ""
                                Me.txtJBMC.Text = ""
                                Me.htxtJBDM.Value = ""
                                Me.txtMSMC.Text = ""
                                Me.htxtMSDM.Value = ""
                                Me.txtLXDH.Text = ""
                                Me.txtSJHM.Text = ""
                                Me.txtFTPDZ.Text = ""
                                Me.txtYXDZ.Text = ""
                                Me.txtLXDZ.Text = ""
                                Me.txtYZBM.Text = ""
                                Me.txtLXR.Text = ""
                                Me.htxtLXRDM.Value = ""
                            Else
                                Me.txtZZDM.Text = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_ZZDM), "")
                                Me.txtZZMC.Text = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_ZZMC), "")
                                Me.txtZZBM.Text = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_ZZBM), "")
                                Me.txtJBMC.Text = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.XingzhengjibieData.FIELD_GG_B_XINGZHENGJIBIE_JBMC), "")
                                Me.htxtJBDM.Value = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_JBDM), "")
                                Me.txtMSMC.Text = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_FULLJOIN_MSMC), "")
                                Me.htxtMSDM.Value = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_MSDM), "")
                                Me.txtLXDH.Text = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_LXDH), "")
                                Me.txtSJHM.Text = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SJHM), "")
                                Me.txtFTPDZ.Text = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_FTPDZ), "")
                                Me.txtYXDZ.Text = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_YXDZ), "")
                                Me.txtLXDZ.Text = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_LXDZ), "")
                                Me.txtYZBM.Text = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_YZBM), "")
                                Me.txtLXR.Text = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_FULLJOIN_LXRMC), "")
                                Me.htxtLXRDM.Value = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_LXR), "")
                            End If
                            Select Case Me.m_objenumEditType
                                Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew, _
                                    Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eCpyNew
                                    '自动生成新代码
                                    Dim strNewZZDM As String
                                    With New Xydc.Platform.BusinessFacade.systemCustomer
                                        .getNewZZDM(strErrMsg, MyBase.UserId, MyBase.UserPassword, Me.m_objInterface.iPrevZZDM, Xydc.Platform.Common.Data.CustomerData.intZZDM_FJCDSM, strNewZZDM)
                                        Me.txtZZDM.Text = strNewZZDM
                                    End With
                                Case Else
                            End Select
                        End With
                    Else
                        '已经通过现场恢复获取控件值
                    End If
                Else
                    '自动恢复数据
                End If

                '使能控件
                With New Xydc.Platform.web.ControlProcess
                    .doEnabledControl(Me.txtZZDM, blnEditMode)
                    .doEnabledControl(Me.txtZZMC, blnEditMode)
                    .doEnabledControl(Me.txtZZBM, blnEditMode)
                    .doEnabledControl(Me.txtJBMC, False)
                    .doEnabledControl(Me.txtMSMC, False)
                    .doEnabledControl(Me.txtLXDH, blnEditMode)
                    .doEnabledControl(Me.txtSJHM, blnEditMode)
                    .doEnabledControl(Me.txtFTPDZ, blnEditMode)
                    .doEnabledControl(Me.txtYXDZ, blnEditMode)
                    .doEnabledControl(Me.txtLXDZ, blnEditMode)
                    .doEnabledControl(Me.txtYZBM, blnEditMode)
                    .doEnabledControl(Me.txtLXR, False)

                    .doEnabledControl(Me.btnSelectJBDM, blnEditMode)
                    .doEnabledControl(Me.btnSelectMSDM, blnEditMode)
                    .doEnabledControl(Me.btnSelectLXR, blnEditMode)
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
                        .doTranslateKey(Me.txtZZDM)
                        .doTranslateKey(Me.txtZZMC)
                        .doTranslateKey(Me.txtZZBM)
                        .doTranslateKey(Me.txtJBMC)
                        .doTranslateKey(Me.txtMSMC)
                        .doTranslateKey(Me.txtLXDH)
                        .doTranslateKey(Me.txtSJHM)
                        .doTranslateKey(Me.txtFTPDZ)
                        .doTranslateKey(Me.txtYXDZ)
                        .doTranslateKey(Me.txtLXDZ)
                        .doTranslateKey(Me.txtYZBM)
                        .doTranslateKey(Me.txtLXR)
                    End With

                    '获取数据
                    If Me.getModuleData(strErrMsg, Me.m_objInterface.iZZDM) = False Then
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

            '具体审计日志
            If Me.IsPostBack = False Then
                If Me.m_blnSaveScence = False Then
                    Select Case Me.m_objenumEditType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew, _
                            Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eCpyNew
                        Case Else
                            Xydc.Platform.SystemFramework.ApplicationLog.WriteAuditPZInfo(Request.UserHostAddress, Request.UserHostName, "[" + MyBase.UserId + "]访问了[" + Me.txtZZMC.Text + "]单位资料！")
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
        '处理“doSelectJBDM”命令
        Private Sub doSelectJBDM(ByVal strControlId As String)

            Dim objsystemXingzhengjibie As New Xydc.Platform.BusinessFacade.systemXingzhengjibie
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '备份现场参数
                Dim strSessionId As String
                strSessionId = Me.saveModuleInformation()
                If strSessionId = "" Then
                    strErrMsg = "错误：不能保存现场信息！"
                    GoTo errProc
                End If

                '准备调用接口
                Dim objIDmxzJbdm As Xydc.Platform.BusinessFacade.IDmxzJbdm
                Dim strUrl As String
                objIDmxzJbdm = New Xydc.Platform.BusinessFacade.IDmxzJbdm
                With objIDmxzJbdm
                    .iTitle = "选择行政级别"
                    .iAllowInput = True
                    .iMultiSelect = False
                    .iInitValue = Me.txtJBMC.Text
                    .iCodeField = Xydc.Platform.Common.Data.XingzhengjibieData.FIELD_GG_B_XINGZHENGJIBIE_JBDM
                    .iNameField = Xydc.Platform.Common.Data.XingzhengjibieData.FIELD_GG_B_XINGZHENGJIBIE_JBMC
                    .iRowSourceSQL = objsystemXingzhengjibie.getXingzhengjibieSQL()

                    .iSourceControlId = strControlId
                    strUrl = ""
                    strUrl += Request.Url.AbsolutePath
                    strUrl += "?"
                    strUrl += Xydc.Platform.Common.Utilities.PulicParameters.ISessionId
                    strUrl += "="
                    strUrl += Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.ISessionId)
                    strUrl += "&"
                    strUrl += Xydc.Platform.Common.Utilities.PulicParameters.MSessionId
                    strUrl += "="
                    strUrl += strSessionId
                    .iReturnUrl = strUrl
                End With

                '调用模块
                Dim strNewSessionId As String
                With New Xydc.Platform.Common.Utilities.PulicParameters
                    strNewSessionId = .getNewGuid()
                End With
                If strNewSessionId = "" Then
                    strErrMsg = "错误：不能初始化调用接口！"
                    GoTo errProc
                End If
                Session.Add(strNewSessionId, objIDmxzJbdm)

                strUrl = ""
                strUrl += "../dmxz/dmxz_jbdm.aspx"
                strUrl += "?"
                strUrl += Xydc.Platform.Common.Utilities.PulicParameters.ISessionId
                strUrl += "="
                strUrl += strNewSessionId
                Response.Redirect(strUrl)

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.BusinessFacade.systemXingzhengjibie.SafeRelease(objsystemXingzhengjibie)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.BusinessFacade.systemXingzhengjibie.SafeRelease(objsystemXingzhengjibie)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        '处理“btnSelectMSDM”命令
        Private Sub doSelectMSDM(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '备份现场参数
                Dim strSessionId As String
                strSessionId = Me.saveModuleInformation()
                If strSessionId = "" Then
                    strErrMsg = "错误：不能保存现场信息！"
                    GoTo errProc
                End If

                '准备调用接口
                Dim objIDmxzZzry As Xydc.Platform.BusinessFacade.IDmxzZzry
                Dim strUrl As String
                objIDmxzZzry = New Xydc.Platform.BusinessFacade.IDmxzZzry
                With objIDmxzZzry
                    .iSelectMode = False
                    .iAllowInput = True
                    .iMultiSelect = False
                    .iSelectBMMC = False
                    .iSelectFFFW = False
                    .iRenyuanList = Me.txtMSMC.Text

                    .iSourceControlId = strControlId
                    strUrl = ""
                    strUrl += Request.Url.AbsolutePath
                    strUrl += "?"
                    strUrl += Xydc.Platform.Common.Utilities.PulicParameters.ISessionId
                    strUrl += "="
                    strUrl += Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.ISessionId)
                    strUrl += "&"
                    strUrl += Xydc.Platform.Common.Utilities.PulicParameters.MSessionId
                    strUrl += "="
                    strUrl += strSessionId
                    .iReturnUrl = strUrl
                End With

                '调用模块
                Dim strNewSessionId As String
                With New Xydc.Platform.Common.Utilities.PulicParameters
                    strNewSessionId = .getNewGuid()
                End With
                If strNewSessionId = "" Then
                    strErrMsg = "错误：不能初始化调用接口！"
                    GoTo errProc
                End If
                Session.Add(strNewSessionId, objIDmxzZzry)

                strUrl = ""
                strUrl += "../dmxz/dmxz_zzry.aspx"
                strUrl += "?"
                strUrl += Xydc.Platform.Common.Utilities.PulicParameters.ISessionId
                strUrl += "="
                strUrl += strNewSessionId
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

        '处理“btnSelectLXR”命令
        Private Sub doSelectLXR(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '备份现场参数
                Dim strSessionId As String
                strSessionId = Me.saveModuleInformation()
                If strSessionId = "" Then
                    strErrMsg = "错误：不能保存现场信息！"
                    GoTo errProc
                End If

                '准备调用接口
                Dim objIDmxzZzry As Xydc.Platform.BusinessFacade.IDmxzZzry
                Dim strUrl As String
                objIDmxzZzry = New Xydc.Platform.BusinessFacade.IDmxzZzry
                With objIDmxzZzry
                    .iSelectMode = False
                    .iAllowInput = True
                    .iMultiSelect = False
                    .iSelectBMMC = False
                    .iSelectFFFW = False
                    .iRenyuanList = Me.txtLXR.Text

                    .iSourceControlId = strControlId
                    strUrl = ""
                    strUrl += Request.Url.AbsolutePath
                    strUrl += "?"
                    strUrl += Xydc.Platform.Common.Utilities.PulicParameters.ISessionId
                    strUrl += "="
                    strUrl += Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.ISessionId)
                    strUrl += "&"
                    strUrl += Xydc.Platform.Common.Utilities.PulicParameters.MSessionId
                    strUrl += "="
                    strUrl += strSessionId
                    .iReturnUrl = strUrl
                End With

                '调用模块
                Dim strNewSessionId As String
                With New Xydc.Platform.Common.Utilities.PulicParameters
                    strNewSessionId = .getNewGuid()
                End With
                If strNewSessionId = "" Then
                    strErrMsg = "错误：不能初始化调用接口！"
                    GoTo errProc
                End If
                Session.Add(strNewSessionId, objIDmxzZzry)

                strUrl = ""
                strUrl += "../dmxz/dmxz_zzry.aspx"
                strUrl += "?"
                strUrl += Xydc.Platform.Common.Utilities.PulicParameters.ISessionId
                strUrl += "="
                strUrl += strNewSessionId
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

                    '释放模块资源
                    Me.releaseModuleParameters()
                    Me.releaseInterfaceParameters()

                    '返回到调用模块，并附加返回参数
                    '要返回的SessionId
                    Dim strSessionId As String
                    strSessionId = Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.ISessionId)

                    'SessionId附加到返回的Url
                    Dim strUrl As String
                    strUrl = Me.m_objInterface.getReturnUrl(Server, Xydc.Platform.Common.Utilities.PulicParameters.OSessionId, strSessionId)

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

                '释放模块资源
                Me.releaseModuleParameters()
                Me.releaseInterfaceParameters()

                '返回到调用模块，并附加返回参数
                '要返回的SessionId
                Dim strSessionId As String
                strSessionId = Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.ISessionId)

                'SessionId附加到返回的Url
                Dim strUrl As String
                strUrl = Me.m_objInterface.getReturnUrl(Server, Xydc.Platform.Common.Utilities.PulicParameters.OSessionId, strSessionId)

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
                Dim objNewData As New System.Collections.Specialized.NameValueCollection
                objNewData.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_ZZDM, Me.txtZZDM.Text)
                objNewData.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_ZZMC, Me.txtZZMC.Text)
                objNewData.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_ZZBM, Me.txtZZBM.Text)
                objNewData.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_JBDM, Me.htxtJBDM.Value)
                objNewData.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_MSDM, Me.htxtMSDM.Value)
                objNewData.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_LXDH, Me.txtLXDH.Text)
                objNewData.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SJHM, Me.txtSJHM.Text)
                objNewData.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_FTPDZ, Me.txtFTPDZ.Text)
                objNewData.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_YXDZ, Me.txtYXDZ.Text)
                objNewData.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_LXDZ, Me.txtLXDZ.Text)
                objNewData.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_YZBM, Me.txtYZBM.Text)
                objNewData.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_LXR, Me.htxtLXRDM.Value)

                '保存信息
                With New Xydc.Platform.BusinessFacade.systemCustomer
                    Select Case Me.m_objenumEditType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew, _
                            Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eCpyNew
                            If .doSaveZuzhijigouData(strErrMsg, MyBase.UserId, MyBase.UserPassword, Nothing, objNewData, Me.m_objenumEditType) = False Then
                                GoTo errProc
                            End If
                        Case Else
                            '获取旧记录
                            If Me.getModuleData(strErrMsg, Me.m_objInterface.iZZDM) = False Then
                                GoTo errProc
                            End If
                            Dim objOldData As System.Data.DataRow
                            With Me.m_objDataSet.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU_FULLJOIN)
                                If .Rows.Count < 1 Then
                                    strErrMsg = "错误：没有当前记录！"
                                    GoTo errProc
                                End If
                                objOldData = .Rows(0)
                            End With
                            '保存新记录
                            If .doSaveZuzhijigouData(strErrMsg, MyBase.UserId, MyBase.UserPassword, objOldData, objNewData, Me.m_objenumEditType) = False Then
                                GoTo errProc
                            End If
                    End Select
                End With

                '记录审计日志
                Select Case Me.m_objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew, _
                        Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eCpyNew
                        Xydc.Platform.SystemFramework.ApplicationLog.WriteAuditPZInfo(Request.UserHostAddress, Request.UserHostName, "[" + MyBase.UserId + "]增加了[" + Me.txtZZMC.Text + "]单位！")
                    Case Else
                        Xydc.Platform.SystemFramework.ApplicationLog.WriteAuditPZInfo(Request.UserHostAddress, Request.UserHostName, "[" + MyBase.UserId + "]修改了[" + Me.txtZZMC.Text + "]单位！")
                End Select

                '设置返回参数
                With Me.m_objInterface
                    .oExitMode = True
                    .oZZDM = Me.txtZZDM.Text
                    .oZZMC = Me.txtZZMC.Text
                End With

                '释放模块资源
                Me.releaseModuleParameters()
                Me.releaseInterfaceParameters()

                '返回到调用模块，并附加返回参数
                '要返回的SessionId
                Dim strSessionId As String
                strSessionId = Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.ISessionId)

                'SessionId附加到返回的Url
                Dim strUrl As String
                strUrl = Me.m_objInterface.getReturnUrl(Server, Xydc.Platform.Common.Utilities.PulicParameters.OSessionId, strSessionId)

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

        Private Sub btnSelectJBDM_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelectJBDM.Click
            Me.doSelectJBDM("btnSelectJBDM")
        End Sub

        Private Sub btnSelectMSDM_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelectMSDM.Click
            Me.doSelectMSDM("btnSelectMSDM")
        End Sub

        Private Sub btnSelectLXR_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelectLXR.Click
            Me.doSelectLXR("btnSelectLXR")
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
