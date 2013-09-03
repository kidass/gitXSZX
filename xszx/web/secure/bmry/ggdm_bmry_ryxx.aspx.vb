Imports System.Web.Security

Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.web
    ' 类名    ：ggdm_bmry_ryxx
    ' 
    ' 调用性质：
    '     可被其他模块调用，本身也调用其他模块
    '
    ' 功能描述： 
    '   　基础代码选择处理模块。
    '
    ' 接口参数：
    '     参见IGgdmBmryRyxx接口类描述
    '----------------------------------------------------------------

    Partial Public Class ggdm_bmry_ryxx
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
        Private m_objSaveScence As Xydc.Platform.BusinessFacade.IMGgdmBmryRyxx
        Private m_blnSaveScence As Boolean

        '----------------------------------------------------------------
        '模块接口参数
        '----------------------------------------------------------------
        Private m_objInterface As Xydc.Platform.BusinessFacade.IGgdmBmryRyxx
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

        Private m_strQuery_ZW As String '记录m_objDataSet_ZW搜索串
        Private m_intRows_ZW As Integer '记录m_objDataSet_ZW的DefaultView记录数
        Private m_objDataSet_ZW As Xydc.Platform.Common.Data.CustomerData



        '----------------------------------------------------------------
        ' 显示现有工作岗位列表
        '----------------------------------------------------------------
        Private Function showGongzuogangweiData(ByRef strErrMsg As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objsystemGongzuogangwei As Xydc.Platform.BusinessFacade.systemGongzuogangwei
            Dim objGongzuogangweiData As Xydc.Platform.Common.Data.GongzuogangweiData

            showGongzuogangweiData = False

            Try
                '显示所有岗位信息
                Dim objListItem As System.Web.UI.WebControls.ListItem
                objsystemGongzuogangwei = New Xydc.Platform.BusinessFacade.systemGongzuogangwei
                With objsystemGongzuogangwei
                    .getGangweiData(strErrMsg, MyBase.UserId, MyBase.UserPassword, "", objGongzuogangweiData)
                    If strErrMsg <> "" Then
                        GoTo errProc
                    End If
                End With
                Dim intCount As Integer
                Dim i As Integer
                With objGongzuogangweiData.Tables(Xydc.Platform.Common.Data.GongzuogangweiData.TABLE_GG_B_GONGZUOGANGWEI)
                    intCount = .Rows.Count
                    For i = 0 To intCount - 1 Step 1
                        objListItem = New System.Web.UI.WebControls.ListItem
                        objListItem.Text = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.GongzuogangweiData.FIELD_GG_B_GONGZUOGANGWEI_GWMC), " ")
                        objListItem.Value = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.GongzuogangweiData.FIELD_GG_B_GONGZUOGANGWEI_GWDM), " ")
                        Me.cblDRZW.Items.Add(objListItem)
                        objListItem = Nothing
                    Next
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Data.GongzuogangweiData.SafeRelease(objGongzuogangweiData)
            Xydc.Platform.BusinessFacade.systemGongzuogangwei.SafeRelease(objsystemGongzuogangwei)

            showGongzuogangweiData = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Data.GongzuogangweiData.SafeRelease(objGongzuogangweiData)
            Xydc.Platform.BusinessFacade.systemGongzuogangwei.SafeRelease(objsystemGongzuogangwei)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 复原模块现场信息并释放相应的资源
        '----------------------------------------------------------------
        Private Sub restoreModuleInformation(ByVal strSessionId As String)

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            Try
                If Me.m_objSaveScence Is Nothing Then Exit Try

                With Me.m_objSaveScence
                    Me.txtRYDM.Text = .txtRYDM
                    Me.txtRYMC.Text = .txtRYMC
                    Me.txtZZMC.Text = .txtZZMC
                    Me.htxtZZDM.Value = .htxtZZDM
                    Me.txtRYXH.Text = .txtRYXH
                    Me.txtJBMC.Text = .txtJBMC
                    Me.htxtJBDM.Value = .htxtJBDM
                    Me.txtMSMC.Text = .txtMSMC
                    Me.htxtMSDM.Value = .htxtMSDM
                    Me.txtLXDH.Text = .txtLXDH
                    Me.txtSJHM.Text = .txtSJHM
                    Me.txtFTPDZ.Text = .txtFTPDZ
                    Me.txtYXDZ.Text = .txtYXDZ
                    Me.chkZDQS.Checked = CType(.chkZDQS, Boolean)
                    Me.txtKZSRY.Text = .txtKZSRY
                    Me.txtQTYZS.Text = .txtQTYZS
                    Me.htxtQTYZS.Value = .htxtQTYZS
                    Me.txtKCKXM.Text = .txtKCKXM
                    Me.txtJJXSMC.Text = .txtJJXSMC

                    Me.txtRYZM.Text = .txtRYZM



                    htxtBH.Value = .htxtBH
                    Me.htxtTASKQuery.Value = .htxtTASKQuery
                    Me.htxtTASKRows.Value = .htxtTASKRows
                    Me.htxtTASKSort.Value = .htxtTASKSort
                    Me.htxtTASKSortColumnIndex.Value = .htxtTASKSortColumnIndex
                    Me.htxtTASKSortType.Value = .htxtTASKSortType

                    Me.htxtDivLeftTASK.Value = .htxtDivLeftTASK
                    Me.htxtDivTopTASK.Value = .htxtDivTopTASK

                    Try
                        Me.grdRY.PageSize = .grdRY_PageSize
                    Catch ex As Exception
                    End Try
                    Try
                        Me.grdRY.CurrentPageIndex = .grdRY_CurrentPageIndex
                    Catch ex As Exception
                    End Try
                    Try
                        Me.grdRY.SelectedIndex = .grdRY_SelectedIndex
                    Catch ex As Exception
                    End Try


                    Me.htxtDivLeftBody.Value = .htxtDivLeftBody
                    Me.htxtDivTopBody.Value = .htxtDivTopBody
                    Me.htxtDivLeftMain.Value = .htxtDivLeftMain
                    Me.htxtDivTopMain.Value = .htxtDivTopMain

                    If Not (.cblDRZW Is Nothing) Then
                        Dim objGongzuogangweiData As Xydc.Platform.Common.Data.GongzuogangweiData
                        Dim objListItem As System.Web.UI.WebControls.ListItem
                        Dim intCount As Integer
                        Dim i As Integer
                        objGongzuogangweiData = CType(.cblDRZW, Xydc.Platform.Common.Data.GongzuogangweiData)
                        With objGongzuogangweiData.Tables(Xydc.Platform.Common.Data.GongzuogangweiData.TABLE_GG_B_GONGZUOGANGWEI)
                            intCount = .Rows.Count
                            For i = 0 To intCount - 1 Step 1
                                objListItem = Me.cblDRZW.Items.FindByValue(objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.GongzuogangweiData.FIELD_GG_B_GONGZUOGANGWEI_GWDM), ""))
                                If Not (objListItem Is Nothing) Then
                                    objListItem.Selected = True
                                End If
                            Next
                        End With
                    End If
                End With

                '释放资源
                Session.Remove(strSessionId)
                Me.m_objSaveScence.Dispose()
                Me.m_objSaveScence = Nothing

            Catch ex As Exception
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

        End Sub

        '----------------------------------------------------------------
        ' 保存模块现场信息并返回相应的SessionId
        '----------------------------------------------------------------
        Private Function saveModuleInformation() As String

            Dim strSessionId As String = ""
            Dim strErrMsg As String

            saveModuleInformation = ""

            Try
                '创建SessionId
                With New Xydc.Platform.Common.Utilities.PulicParameters
                    strSessionId = .getNewGuid()
                End With
                If strSessionId = "" Then Exit Try

                '创建对象
                Me.m_objSaveScence = New Xydc.Platform.BusinessFacade.IMGgdmBmryRyxx

                '保存现场信息
                With Me.m_objSaveScence
                    .txtRYDM = Me.txtRYDM.Text
                    .txtRYMC = Me.txtRYMC.Text
                    .txtZZMC = Me.txtZZMC.Text
                    .htxtZZDM = Me.htxtZZDM.Value
                    .txtRYXH = Me.txtRYXH.Text
                    .txtJBMC = Me.txtJBMC.Text
                    .htxtJBDM = Me.htxtJBDM.Value
                    .txtMSMC = Me.txtMSMC.Text
                    .htxtMSDM = Me.htxtMSDM.Value
                    .txtLXDH = Me.txtLXDH.Text
                    .txtSJHM = Me.txtSJHM.Text
                    .txtFTPDZ = Me.txtFTPDZ.Text
                    .txtYXDZ = Me.txtYXDZ.Text
                    .chkZDQS = Me.chkZDQS.Checked.ToString()
                    .txtKZSRY = Me.txtKZSRY.Text
                    .txtQTYZS = Me.txtQTYZS.Text
                    .htxtQTYZS = Me.htxtQTYZS.Value
                    .txtKCKXM = Me.txtKCKXM.Text
                    .txtJJXSMC = Me.txtJJXSMC.Text

                    .txtRYZM = Me.txtRYZM.Text



                    .htxtBH = Me.htxtBH.Value
                    .htxtTASKQuery = Me.htxtTASKQuery.Value
                    .htxtTASKRows = Me.htxtTASKRows.Value
                    .htxtTASKSort = Me.htxtTASKSort.Value
                    .htxtTASKSortColumnIndex = Me.htxtTASKSortColumnIndex.Value
                    .htxtTASKSortType = Me.htxtTASKSortType.Value

                    .htxtDivLeftTASK = Me.htxtDivLeftTASK.Value
                    .htxtDivTopTASK = Me.htxtDivTopTASK.Value

                    .grdRY_PageSize = Me.grdRY.PageSize
                    .grdRY_CurrentPageIndex = Me.grdRY.CurrentPageIndex
                    .grdRY_SelectedIndex = Me.grdRY.SelectedIndex

                    .htxtDivLeftBody = Me.htxtDivLeftBody.Value
                    .htxtDivTopBody = Me.htxtDivTopBody.Value
                    .htxtDivLeftMain = Me.htxtDivLeftMain.Value
                    .htxtDivTopMain = Me.htxtDivTopMain.Value

                    Dim objGongzuogangweiData As Xydc.Platform.Common.Data.GongzuogangweiData
                    Dim objDataRow As System.Data.DataRow
                    Dim intSelected As Integer
                    Dim intCount As Integer
                    Dim i As Integer
                    intCount = Me.cblDRZW.Items.Count
                    intSelected = 0
                    For i = 0 To intCount - 1 Step 1
                        If Me.cblDRZW.Items(i).Selected = True Then
                            If intSelected = 0 Then
                                objGongzuogangweiData = New Xydc.Platform.Common.Data.GongzuogangweiData(Xydc.Platform.Common.Data.GongzuogangweiData.enumTableType.GG_B_GONGZUOGANGWEI)
                            End If
                            With objGongzuogangweiData.Tables(Xydc.Platform.Common.Data.GongzuogangweiData.TABLE_GG_B_GONGZUOGANGWEI)
                                objDataRow = .NewRow()
                                objDataRow.Item(Xydc.Platform.Common.Data.GongzuogangweiData.FIELD_GG_B_GONGZUOGANGWEI_GWDM) = Me.cblDRZW.Items(i).Value
                                objDataRow.Item(Xydc.Platform.Common.Data.GongzuogangweiData.FIELD_GG_B_GONGZUOGANGWEI_GWMC) = Me.cblDRZW.Items(i).Text
                                .Rows.Add(objDataRow)
                            End With
                            intSelected += 1
                        End If
                    Next
                    If intSelected > 0 Then
                        .cblDRZW = objGongzuogangweiData
                    End If
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
                Dim objIDmxzZzjg As Xydc.Platform.BusinessFacade.IDmxzZzjg
                Try
                    objIDmxzZzjg = CType(Session.Item(Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.OSessionId)), Xydc.Platform.BusinessFacade.IDmxzZzjg)
                Catch ex As Exception
                    objIDmxzZzjg = Nothing
                End Try
                If Not (objIDmxzZzjg Is Nothing) Then
                    '返回值处理
                    Select Case objIDmxzZzjg.iSourceControlId.ToUpper()
                        Case "btnSelectZZDM".ToUpper()
                            '处理btnSelectZZDM返回
                            If objIDmxzZzjg.oExitMode = True Then
                                Me.txtZZMC.Text = objIDmxzZzjg.oBumenList
                                Me.txtZZMC.Text = Me.txtZZMC.Text.Trim()
                                If Me.txtZZMC.Text <> "" Then
                                    '根据单位名称获取单位代码
                                    objsystemCustomer.getZzdmByZzmc(strErrMsg, MyBase.UserId, MyBase.UserPassword, Me.txtZZMC.Text, strCode)
                                    Me.htxtZZDM.Value = strCode
                                Else
                                    Me.htxtZZDM.Value = ""
                                End If
                            End If
                        Case Else
                    End Select
                    '释放资源
                    Session.Remove(Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.OSessionId))
                    objIDmxzZzjg.Dispose()
                    objIDmxzZzjg = Nothing
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
                        Case "btnSelectKZSRY".ToUpper()
                            '处理btnSelectKZSRY返回
                            If objIDmxzZzry.oExitMode = True Then
                                Me.txtKZSRY.Text = objIDmxzZzry.oRenyuanList
                            End If
                        Case "btnSelectQTYZS".ToUpper()
                            '处理btnSelectQTYZS返回
                            If objIDmxzZzry.oExitMode = True Then
                                Me.txtQTYZS.Text = objIDmxzZzry.oRenyuanList
                                Me.txtQTYZS.Text = Me.txtQTYZS.Text.Trim()
                                If Me.txtQTYZS.Text <> "" Then
                                    '根据名称获取人员代码
                                    objsystemCustomer.getRydmByRymc(strErrMsg, MyBase.UserId, MyBase.UserPassword, Me.txtQTYZS.Text, strCode)
                                    Me.htxtQTYZS.Value = strCode
                                Else
                                    Me.htxtQTYZS.Value = ""
                                End If
                            End If
                        Case "btnSelectKCKXM".ToUpper()
                            '处理btnSelectKCKXM返回
                            If objIDmxzZzry.oExitMode = True Then
                                Me.txtKCKXM.Text = objIDmxzZzry.oRenyuanList
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
                    m_objInterface = CType(objTemp, Xydc.Platform.BusinessFacade.IGgdmBmryRyxx)
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
                        Me.m_objSaveScence = CType(Session.Item(strSessionId), Xydc.Platform.BusinessFacade.IMGgdmBmryRyxx)
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
        '     strRYDM        ：要获取的人员代码

        '     strZZDM        ：要获取的组织代码

        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function getModuleData( _
            ByRef strErrMsg As String, _
            ByVal strRYDM As String, _
            ByVal strZZDM As String) As Boolean

            Dim blnuser As Boolean
            getModuleData = False

            Try
                '释放资源
                Xydc.Platform.Common.Data.CustomerData.SafeRelease(Me.m_objDataSet)

                '重新检索数据
                With New Xydc.Platform.BusinessFacade.systemCustomer
                    'If .getRenyuanData(strErrMsg, MyBase.UserId, MyBase.UserPassword, strRYDM, "0001", Me.m_objDataSet) = False Then
                    '    GoTo errProc
                    'End If

                    Select Case Me.m_objenumEditType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                            strZZDM = ""
                        Case Else
                    End Select

                    If .getRenyuanData(strErrMsg, MyBase.UserId, MyBase.UserPassword, strRYDM, strZZDM, "0001", blnuser, Me.m_objDataSet) = False Then
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
            Dim objsystemCustomer As New Xydc.Platform.BusinessFacade.systemCustomer

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
                        With Me.m_objDataSet.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN)
                            If .Rows.Count < 1 Then
                                Me.txtRYDM.Text = ""
                                Me.txtRYMC.Text = ""
                                Me.txtZZMC.Text = ""
                                Me.htxtZZDM.Value = ""
                                Me.txtRYXH.Text = ""
                                Me.txtJBMC.Text = ""
                                Me.htxtJBDM.Value = ""
                                Me.txtMSMC.Text = ""
                                Me.htxtMSDM.Value = ""
                                Me.txtLXDH.Text = ""
                                Me.txtSJHM.Text = ""
                                Me.txtFTPDZ.Text = ""
                                Me.txtYXDZ.Text = ""
                                Me.chkZDQS.Checked = False
                                Me.txtKZSRY.Text = ""
                                Me.txtQTYZS.Text = ""
                                Me.htxtQTYZS.Value = ""
                                Me.txtKCKXM.Text = ""
                                Me.txtJJXSMC.Text = ""

                                Me.txtRYZM.Text = ""


                                htxtBH.Value = ""

                            Else

                                htxtBH.Value = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_FULLJOIN_BH), "")

                                Me.txtRYDM.Text = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYDM), "")
                                Me.txtRYMC.Text = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYMC), "")
                                Me.txtZZMC.Text = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_ZZMC), "")
                                Me.htxtZZDM.Value = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_ZZDM), "")
                                Me.txtRYXH.Text = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYXH), "")
                                Me.txtJBMC.Text = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.XingzhengjibieData.FIELD_GG_B_XINGZHENGJIBIE_JBMC), "")
                                Me.htxtJBDM.Value = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_JBDM), "")
                                Me.txtMSMC.Text = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_FULLJOIN_MSMC), "")
                                Me.htxtMSDM.Value = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_MSDM), "")
                                Me.txtLXDH.Text = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_LXDH), "")
                                Me.txtSJHM.Text = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SJHM), "")
                                Me.txtFTPDZ.Text = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_FTPDZ), "")
                                Me.txtYXDZ.Text = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_YXDZ), "")
                                If objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_ZDQS), "") = Xydc.Platform.Common.Utilities.PulicParameters.CharTrue Then
                                    Me.chkZDQS.Checked = True
                                Else
                                    Me.chkZDQS.Checked = False
                                End If
                                Me.txtKZSRY.Text = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_KZSRY), "")
                                Me.txtQTYZS.Text = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_FULLJOIN_QTYZSMC), "")
                                Me.htxtQTYZS.Value = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_QTYZS), "")
                                Me.txtKCKXM.Text = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_KCKXM), "")
                                Me.txtJJXSMC.Text = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_JJXSMC), "")

                                Me.txtRYZM.Text = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYZM), "")


                                '显示实际聘用信息
                                Dim objCustomerData As Xydc.Platform.Common.Data.CustomerData
                                Dim objListItem As System.Web.UI.WebControls.ListItem
                                Dim strValue As String
                                Dim intCount As Integer
                                Dim i As Integer
                                If objsystemCustomer.getRenyuanData(strErrMsg, MyBase.UserId, MyBase.UserPassword, Me.txtRYDM.Text, "0010", objCustomerData) = False Then
                                    GoTo errProc
                                End If
                                With objCustomerData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_SHANGGANG)
                                    intCount = .Rows.Count
                                    For i = 0 To intCount - 1 Step 1
                                        strValue = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_SHANGGANG_GWDM), " ")
                                        objListItem = Nothing
                                        objListItem = Me.cblDRZW.Items.FindByValue(strValue)
                                        If Not (objListItem Is Nothing) Then
                                            objListItem.Selected = True
                                        End If
                                    Next
                                End With
                                objCustomerData.Dispose()
                                objCustomerData = Nothing
                            End If
                            Select Case Me.m_objenumEditType
                                Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew, _
                                    Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eCpyNew
                                    Dim strZZMC As String
                                    '设置初始值
                                    Me.htxtZZDM.Value = Me.m_objInterface.iZZDM
                                    Me.htxtZZDM.Value = Me.htxtZZDM.Value.Trim()
                                    If Me.htxtZZDM.Value <> "" Then
                                        objsystemCustomer.getZzmcByZzdm(strErrMsg, MyBase.UserId, MyBase.UserPassword, Me.htxtZZDM.Value, strZZMC)
                                        Me.txtZZMC.Text = strZZMC
                                    Else
                                        Me.txtZZMC.Text = ""
                                    End If
                                    '自动生成人员序号
                                    Dim strRYXH As String
                                    objsystemCustomer.getNewRYXH(strErrMsg, MyBase.UserId, MyBase.UserPassword, Me.htxtZZDM.Value, strRYXH)
                                    Me.txtRYXH.Text = strRYXH
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
                    .doEnabledControl(Me.txtRYDM, blnEditMode)
                    .doEnabledControl(Me.txtRYMC, blnEditMode)
                    .doEnabledControl(Me.txtZZMC, False)
                    .doEnabledControl(Me.txtRYXH, blnEditMode)
                    .doEnabledControl(Me.txtJBMC, False)
                    .doEnabledControl(Me.txtMSMC, False)
                    .doEnabledControl(Me.txtLXDH, blnEditMode)
                    .doEnabledControl(Me.txtSJHM, blnEditMode)
                    .doEnabledControl(Me.txtFTPDZ, blnEditMode)
                    .doEnabledControl(Me.txtYXDZ, blnEditMode)
                    .doEnabledControl(Me.chkZDQS, blnEditMode)
                    .doEnabledControl(Me.txtKZSRY, blnEditMode)
                    .doEnabledControl(Me.txtQTYZS, False)
                    .doEnabledControl(Me.txtKCKXM, blnEditMode)
                    .doEnabledControl(Me.txtJJXSMC, blnEditMode)

                    .doEnabledControl(Me.txtRYZM, blnEditMode)


                    .doEnabledControl(Me.btnSelectZZDM, blnEditMode)
                    .doEnabledControl(Me.btnSelectJBDM, blnEditMode)
                    .doEnabledControl(Me.btnSelectMSDM, blnEditMode)
                    .doEnabledControl(Me.btnSelectKZSRY, blnEditMode)
                    .doEnabledControl(Me.btnSelectQTYZS, blnEditMode)
                    .doEnabledControl(Me.btnSelectKCKXM, blnEditMode)

                    .doEnabledControl(Me.cblDRZW, blnEditMode)
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.BusinessFacade.systemCustomer.SafeRelease(objsystemCustomer)

            showEditPanelInfo = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.BusinessFacade.systemCustomer.SafeRelease(objsystemCustomer)
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
                        .doTranslateKey(Me.txtRYDM)
                        .doTranslateKey(Me.txtRYMC)
                        .doTranslateKey(Me.txtZZMC)
                        .doTranslateKey(Me.txtRYXH)
                        .doTranslateKey(Me.txtJBMC)
                        .doTranslateKey(Me.txtMSMC)
                        .doTranslateKey(Me.txtLXDH)
                        .doTranslateKey(Me.txtSJHM)
                        .doTranslateKey(Me.txtFTPDZ)
                        .doTranslateKey(Me.txtYXDZ)
                        .doTranslateKey(Me.txtKZSRY)
                        .doTranslateKey(Me.txtQTYZS)
                        .doTranslateKey(Me.txtKCKXM)
                        .doTranslateKey(Me.txtJJXSMC)

                        .doTranslateKey(Me.txtRYZM)

                    End With

                    '获取数据
                    If Me.getModuleData(strErrMsg, Me.m_objInterface.iRYDM, Me.m_objInterface.iZZDM) = False Then
                        GoTo errProc
                    End If
                    If Me.showModuleData(strErrMsg, Me.m_blnEditMode) = False Then
                        GoTo errProc
                    End If


                    Select Case Me.m_objenumEditType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew, _
                            Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eCpyNew
                        Case Else
                            If Me.getModuleData_ZW(strErrMsg, Me.m_objInterface.iRYDM) = False Then
                                GoTo errProc
                            End If
                            If Me.showDataGridInfo_ZW(strErrMsg) = False Then
                                GoTo errProc
                            End If
                    End Select

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

            '显示岗位列表
            If Me.IsPostBack = False Then
                If Me.showGongzuogangweiData(strErrMsg) = False Then
                    GoTo errProc
                End If
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
                            Xydc.Platform.SystemFramework.ApplicationLog.WriteAuditPZInfo(Request.UserHostAddress, Request.UserHostName, "[" + MyBase.UserId + "]访问了[" + Me.txtRYDM.Text + "]账户！")
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
        ' 获取文件对应的事宜信息
        '     strErrMsg      ：返回错误信息
        '     strWhere       ：搜索条件
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function getModuleData_ZW( _
            ByRef strErrMsg As String, _
            ByVal strRYDM As String) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN
            getModuleData_ZW = False

            Try
                '备份Sort字符串
                Dim strSort As String
                strSort = Me.htxtTASKSort.Value
                If strSort.Length > 0 Then strSort = strSort.Trim

                '释放资源
                If Not (Me.m_objDataSet_ZW Is Nothing) Then
                    Me.m_objDataSet_ZW.Dispose()
                    Me.m_objDataSet_ZW = Nothing
                End If

                '重新检索数据
                With New Xydc.Platform.BusinessFacade.systemCustomer
                    Dim blnuser As Boolean

                    If .getRenyuanData(strErrMsg, MyBase.UserId, MyBase.UserPassword, strRYDM, "", "0001", blnuser, Me.m_objDataSet_ZW) = False Then
                        GoTo errProc
                    End If

                End With

                '恢复Sort字符串
                With Me.m_objDataSet_ZW.Tables(strTable)
                    .DefaultView.Sort = strSort
                End With

                '缓存参数
                With Me.m_objDataSet_ZW.Tables(strTable)
                    Me.htxtTASKRows.Value = .DefaultView.Count.ToString()
                    Me.m_intRows_ZW = .DefaultView.Count
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try



            getModuleData_ZW = True
            Exit Function

errProc:

            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示grdRY的信息
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showDataGridInfo_ZW(ByRef strErrMsg As String) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN
            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess

            showDataGridInfo_ZW = False

            '获取系统保存的网格排序数据
            Dim intSortColumnIndex As Integer
            intSortColumnIndex = objPulicParameters.getObjectValue(Me.htxtTASKSortColumnIndex.Value, -1)
            Dim objSortType As Xydc.Platform.Common.Utilities.PulicParameters.enumSortType
            Try
                objSortType = CType(Me.htxtTASKSortType.Value, Xydc.Platform.Common.Utilities.PulicParameters.enumSortType)
            Catch ex As Exception
                objSortType = Xydc.Platform.Common.Utilities.PulicParameters.enumSortType.None
            End Try

            '网格显示处理
            Try
                '在获取数据时已经恢复了RowFilter、Sort的现场
                '设置数据源
                If Me.m_objDataSet_ZW Is Nothing Then
                    Me.grdRY.DataSource = Nothing
                Else
                    With Me.m_objDataSet_ZW.Tables(strTable)
                        Me.grdRY.DataSource = .DefaultView
                    End With
                End If

                '调整网格参数
                With Me.m_objDataSet_ZW.Tables(strTable)
                    If objDataGridProcess.onBeforeDataGridBind(strErrMsg, Me.grdRY, .DefaultView.Count) = False Then
                        GoTo errProc
                    End If
                End With

                '恢复列标题中的排序信息
                If intSortColumnIndex >= 0 Then
                    objDataGridProcess.doClearSortCharInDataGridHead(Me.grdRY)
                    With Me.grdRY.Columns(intSortColumnIndex)
                        .HeaderText = objDataGridProcess.getColumnSortHeadString(.HeaderText, objSortType)
                    End With
                End If

                '绑定数据
                Me.grdRY.DataBind()

                '----------------------------------------------------------------
                '因为这些信息是非绑定的，所以下面的操作必须等绑定完成后执行！！！
                '一旦在后续处理中执行了DataBind，则信息会丢失！！！
                '----------------------------------------------------------------

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)

            showDataGridInfo_ZW = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Exit Function

        End Function


        '----------------------------------------------------------------
        '模块特殊操作处理器
        '----------------------------------------------------------------
        '处理“btnSelectZZDM”命令
        Private Sub doSelectZZDM(ByVal strControlId As String)

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
                Dim objIDmxzZzjg As Xydc.Platform.BusinessFacade.IDmxzZzjg
                Dim strUrl As String
                objIDmxzZzjg = New Xydc.Platform.BusinessFacade.IDmxzZzjg
                With objIDmxzZzjg
                    .iAllowInput = True
                    .iMultiSelect = False
                    .iSelectFFFW = False
                    .iBumenList = Me.txtZZMC.Text

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
                Session.Add(strNewSessionId, objIDmxzZzjg)

                strUrl = ""
                strUrl += "../dmxz/dmxz_zzjg.aspx"
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

        '处理“btnSelectKZSRY”命令
        Private Sub doSelectKZSRY(ByVal strControlId As String)

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
                    .iMultiSelect = True
                    .iSelectBMMC = True
                    .iSelectFFFW = False
                    .iRenyuanList = Me.txtKZSRY.Text

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

        '处理“btnSelectQTYZS”命令
        Private Sub doSelectQTYZS(ByVal strControlId As String)

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
                    .iRenyuanList = Me.txtQTYZS.Text

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

        '处理“btnSelectKCKXM”命令
        Private Sub doSelectKCKXM(ByVal strControlId As String)

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
                    .iMultiSelect = True
                    .iSelectBMMC = True
                    .iSelectFFFW = False
                    .iRenyuanList = Me.txtKCKXM.Text

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

            Dim objsystemCustomer As New Xydc.Platform.BusinessFacade.systemCustomer
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim objCustomerData As Xydc.Platform.Common.Data.CustomerData
            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim intStep As Integer
            Dim strErrMsg As String


            Try
                '准备保存公共_B_人员的信息
                Dim objNewData As New System.Collections.Specialized.NameValueCollection
                Dim objNewData_Temp As New System.Collections.Specialized.NameValueCollection
                Dim objUpdateData As New System.Collections.Specialized.NameValueCollection

                objNewData.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYDM, Me.txtRYDM.Text)
                objNewData.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYMC, Me.txtRYMC.Text)
                objNewData.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_ZZDM, Me.htxtZZDM.Value)
                objNewData.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYXH, Me.txtRYXH.Text)
                objNewData.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_JBDM, Me.htxtJBDM.Value)

                objNewData.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_MSDM, Me.htxtMSDM.Value)
                objNewData.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_LXDH, Me.txtLXDH.Text)
                objNewData.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SJHM, Me.txtSJHM.Text)
                objNewData.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_FTPDZ, Me.txtFTPDZ.Text)
                objNewData.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_YXDZ, Me.txtYXDZ.Text)
                objNewData.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_KZSRY, Me.txtKZSRY.Text)
                objNewData.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_QTYZS, Me.htxtQTYZS.Value)
                objNewData.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_KCKXM, Me.txtKCKXM.Text)
                objNewData.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_JJXSMC, Me.txtJJXSMC.Text)

                If Me.txtRYZM.Text.Trim = "" Then Me.txtRYZM.Text = Me.txtRYMC.Text
                objNewData.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYZM, Me.txtRYZM.Text)

                If Me.chkZDQS.Checked = True Then
                    objNewData.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_ZDQS, Xydc.Platform.Common.Utilities.PulicParameters.CharTrue)
                Else
                    objNewData.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_ZDQS, Xydc.Platform.Common.Utilities.PulicParameters.CharFalse)
                End If


                Dim objNewDataSG As Xydc.Platform.Common.Data.CustomerData
                Dim objDataRow As System.Data.DataRow
                Dim intSelected As Integer = 0
                Dim intCount As Integer
                Dim i As Integer
                intCount = Me.cblDRZW.Items.Count
                objNewDataSG = Nothing
                For i = 0 To intCount - 1 Step 1
                    If Me.cblDRZW.Items(i).Selected = True Then
                        If intSelected = 0 Then
                            objNewDataSG = New Xydc.Platform.Common.Data.CustomerData(Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_SHANGGANG)
                        End If

                        With objNewDataSG.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_SHANGGANG)
                            objDataRow = .NewRow()
                            objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_SHANGGANG_RYDM) = Me.txtRYDM.Text
                            objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_SHANGGANG_GWDM) = Me.cblDRZW.Items(i).Value
                            .Rows.Add(objDataRow)
                        End With

                        intSelected += 1
                    End If
                Next


                '检查用户ID是否已经存在
                Dim intType As Integer = 0
                Dim objDataSet As System.Data.DataSet
                If objsystemCustomer.doVerifyRenyuanData(strErrMsg, MyBase.UserId, MyBase.UserPassword, Me.txtRYDM.Text, Me.htxtZZDM.Value, intType, objCustomerData) = False Then
                    GoTo errProc
                End If

                '准备保存公共_B_上岗的信息
                '保存信息
                With New Xydc.Platform.BusinessFacade.systemCustomer
                    Select Case Me.m_objenumEditType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew, _
                            Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eCpyNew

                            If intType = 1 Then
                                GoTo errProc
                            Else
                                If intType = 0 Then
                                    If objsystemCustomer.doSaveRenyuanData(strErrMsg, MyBase.UserId, MyBase.UserPassword, Nothing, objNewData, Me.m_objenumEditType, objNewDataSG) = False Then
                                        GoTo errProc
                                    End If
                                Else
                                    intStep = 1
                                    If objMessageProcess.isExecuteCode(Request, Me.popMessageObject.UniqueID, intStep) = True Then
                                        '询问
                                        objMessageProcess.doConfirmMessage(Me.popMessageObject, strErrMsg, strControlId, intStep)
                                        Exit Try
                                    Else
                                        objMessageProcess.doResetPopMessage(Me.popMessageObject)
                                    End If

                                    '继续处理
                                    intStep = 2
                                    If objMessageProcess.isExecuteCode(Request, Me.popMessageObject.UniqueID, intStep) = True Then

                                        objNewData_Temp.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYDM, Me.txtRYDM.Text)
                                        objNewData_Temp.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_ZZDM, Me.htxtZZDM.Value)
                                        objNewData_Temp.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYXH, Me.txtRYXH.Text)

                                        With (objCustomerData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN).Rows(0))
                                            objNewData_Temp.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYMC, objPulicParameters.getObjectValue(.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYMC), ""))
                                            objNewData_Temp.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYZM, objPulicParameters.getObjectValue(.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYZM), ""))
                                            objNewData_Temp.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_JBDM, objPulicParameters.getObjectValue(.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_JBDM), ""))
                                            objNewData_Temp.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_MSDM, objPulicParameters.getObjectValue(.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_MSDM), ""))
                                            objNewData_Temp.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_LXDH, objPulicParameters.getObjectValue(.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_LXDH), ""))
                                            objNewData_Temp.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SJHM, objPulicParameters.getObjectValue(.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SJHM), ""))
                                            objNewData_Temp.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_FTPDZ, objPulicParameters.getObjectValue(.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_FTPDZ), ""))
                                            objNewData_Temp.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_YXDZ, objPulicParameters.getObjectValue(.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_YXDZ), ""))
                                            objNewData_Temp.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_KZSRY, objPulicParameters.getObjectValue(.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_KZSRY), ""))
                                            objNewData_Temp.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_QTYZS, objPulicParameters.getObjectValue(.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_QTYZS), ""))
                                            objNewData_Temp.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_KCKXM, objPulicParameters.getObjectValue(.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_KCKXM), ""))
                                            objNewData_Temp.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_JJXSMC, objPulicParameters.getObjectValue(.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_JJXSMC), ""))
                                            objNewData_Temp.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYZM, objPulicParameters.getObjectValue(.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYZM), ""))
                                            objNewData_Temp.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_ZDQS, objPulicParameters.getObjectValue(.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_ZDQS), ""))
                                            objNewData_Temp.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SFJM, objPulicParameters.getObjectValue(.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SFJM), ""))
                                        End With

                                        If intType = 2 Then
                                            If objsystemCustomer.doSaveRenyuanData(strErrMsg, MyBase.UserId, MyBase.UserPassword, Nothing, objNewData_Temp, objUpdateData, Me.m_objenumEditType, objNewDataSG) = False Then
                                                GoTo errProc
                                            End If
                                        Else
                                            If objsystemCustomer.doSaveRenyuanData(strErrMsg, MyBase.UserId, MyBase.UserPassword, Nothing, objNewData_Temp, Me.m_objenumEditType, objNewDataSG) = False Then
                                                GoTo errProc
                                            End If
                                        End If
                                    End If
                                End If
                            End If

                        Case Else

                            '获取旧记录
                            If Me.getModuleData(strErrMsg, Me.m_objInterface.iRYDM, Me.m_objInterface.iZZDM) = False Then
                                GoTo errProc
                            End If
                            Dim objOldData As System.Data.DataRow
                            With Me.m_objDataSet.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN)
                                If .Rows.Count < 1 Then
                                    strErrMsg = "错误：没有当前记录！"
                                    GoTo errProc
                                End If
                                objOldData = .Rows(0)
                            End With
                            Dim intBH As Integer
                            intBH = CType(Me.htxtBH.Value.Trim, Integer)
                            If intBH < 1 Then
                                '保存新记录
                                If objsystemCustomer.doSaveRenyuanData(strErrMsg, MyBase.UserId, MyBase.UserPassword, objOldData, objNewData, Me.m_objenumEditType, objNewDataSG) = False Then
                                    GoTo errProc
                                End If
                            Else
                                'objNewData_Temp.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYDM, Me.txtRYDM.Text)
                                'objNewData_Temp.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYMC, Me.txtRYMC.Text)
                                'objNewData_Temp.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_ZZDM, Me.htxtZZDM.Value)
                                'objNewData_Temp.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYXH, Me.txtRYXH.Text)
                                'objNewData_Temp.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_JBDM, Me.htxtJBDM.Value)

                                objNewData_Temp.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYDM, Me.txtRYDM.Text)
                                objNewData_Temp.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYMC, Me.txtRYMC.Text)
                                objNewData_Temp.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_ZZDM, Me.htxtZZDM.Value)
                                objNewData_Temp.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYXH, Me.txtRYXH.Text)
                                objNewData_Temp.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_JBDM, Me.htxtJBDM.Value)

                                objNewData_Temp.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_MSDM, Me.htxtMSDM.Value)
                                objNewData_Temp.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_LXDH, Me.txtLXDH.Text)
                                objNewData_Temp.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SJHM, Me.txtSJHM.Text)
                                objNewData_Temp.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_FTPDZ, Me.txtFTPDZ.Text)
                                objNewData_Temp.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_YXDZ, Me.txtYXDZ.Text)
                                objNewData_Temp.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_KZSRY, Me.txtKZSRY.Text)
                                objNewData_Temp.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_QTYZS, Me.htxtQTYZS.Value)
                                objNewData_Temp.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_KCKXM, Me.txtKCKXM.Text)
                                objNewData_Temp.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_JJXSMC, Me.txtJJXSMC.Text)
                                objNewData_Temp.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYZM, Me.txtRYZM.Text)
                                If Me.chkZDQS.Checked = True Then
                                    objNewData_Temp.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_ZDQS, Xydc.Platform.Common.Utilities.PulicParameters.CharTrue)
                                Else
                                    objNewData_Temp.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_ZDQS, Xydc.Platform.Common.Utilities.PulicParameters.CharFalse)
                                End If

                                objUpdateData.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYDM, Me.txtRYDM.Text)
                                objUpdateData.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYMC, Me.txtRYMC.Text)
                                objUpdateData.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_JBDM, Me.htxtJBDM.Value)
                                objUpdateData.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_MSDM, Me.htxtMSDM.Value)
                                objUpdateData.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_LXDH, Me.txtLXDH.Text)
                                objUpdateData.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SJHM, Me.txtSJHM.Text)
                                objUpdateData.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_FTPDZ, Me.txtFTPDZ.Text)
                                objUpdateData.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_YXDZ, Me.txtYXDZ.Text)
                                objUpdateData.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_KZSRY, Me.txtKZSRY.Text)
                                objUpdateData.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_QTYZS, Me.htxtQTYZS.Value)
                                objUpdateData.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_KCKXM, Me.txtKCKXM.Text)
                                objUpdateData.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_JJXSMC, Me.txtJJXSMC.Text)
                                objUpdateData.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYZM, Me.txtRYZM.Text)
                                If Me.chkZDQS.Checked = True Then
                                    objUpdateData.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_ZDQS, Xydc.Platform.Common.Utilities.PulicParameters.CharTrue)
                                Else
                                    objUpdateData.Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_ZDQS, Xydc.Platform.Common.Utilities.PulicParameters.CharFalse)
                                End If

                                If objsystemCustomer.doSaveRenyuanData(strErrMsg, MyBase.UserId, MyBase.UserPassword, objOldData, objNewData_Temp, objUpdateData, Me.m_objenumEditType, objNewDataSG) = False Then
                                    GoTo errProc
                                End If
                            End If
                    End Select
                End With

                '记录审计日志
                Select Case Me.m_objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew, _
                        Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eCpyNew
                        Xydc.Platform.SystemFramework.ApplicationLog.WriteAuditPZInfo(Request.UserHostAddress, Request.UserHostName, "[" + MyBase.UserId + "]增加了[" + Me.txtRYDM.Text + "]账户！")
                    Case Else
                        Xydc.Platform.SystemFramework.ApplicationLog.WriteAuditPZInfo(Request.UserHostAddress, Request.UserHostName, "[" + MyBase.UserId + "]修改了[" + Me.txtRYDM.Text + "]账户！")
                End Select

                '设置返回参数
                With Me.m_objInterface
                    .oExitMode = True
                    .oRYDM = Me.txtRYDM.Text
                    .oRYMC = Me.txtRYMC.Text
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

            Xydc.Platform.BusinessFacade.systemCustomer.SafeRelease(objsystemCustomer)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.BusinessFacade.systemCustomer.SafeRelease(objsystemCustomer)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub btnSelectZZDM_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelectZZDM.Click
            Me.doSelectZZDM("btnSelectZZDM")
        End Sub

        Private Sub btnSelectJBDM_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelectJBDM.Click
            Me.doSelectJBDM("btnSelectJBDM")
        End Sub

        Private Sub btnSelectMSDM_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelectMSDM.Click
            Me.doSelectMSDM("btnSelectMSDM")
        End Sub

        Private Sub btnSelectKZSRY_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelectKZSRY.Click
            Me.doSelectKZSRY("btnSelectKZSRY")
        End Sub

        Private Sub btnSelectQTYZS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelectQTYZS.Click
            Me.doSelectQTYZS("btnSelectQTYZS")
        End Sub

        Private Sub btnSelectKCKXM_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelectKCKXM.Click
            Me.doSelectKCKXM("btnSelectKCKXM")
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