Imports System.Web.Security

Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.web
    ' 类名    ：xtpz_bdkz
    ' 
    ' 调用性质：
    '     不被其他模块调用，本身调用其他模块
    '
    ' 功能描述： 
    '   　补登控制设置处理
    '----------------------------------------------------------------

    Partial Public Class xtpz_bdkz
        Inherits Xydc.Platform.web.PageBase

        '----------------------------------------------------------------
        '模块私用参数
        '----------------------------------------------------------------
        '本模块相对image的相对路径
        Private m_cstrRelativePathToImage As String = "../../"

        '----------------------------------------------------------------
        '模块授权参数
        '----------------------------------------------------------------
        Private m_cstrPrevilegeParamPrefix As String = "xtpz_bdkz_previlege_param"
        Private m_blnPrevilegeParams(4) As Boolean

        '----------------------------------------------------------------
        '模块现场保留参数，恢复完成后立即释放session资源
        '----------------------------------------------------------------
        Private m_objSaveScence As Xydc.Platform.BusinessFacade.IMXtpzBdkz
        '首次进入并调用其他模块返回时=true，其他=false
        Private m_blnSaveScence As Boolean

        '----------------------------------------------------------------
        '模块接口参数
        '----------------------------------------------------------------

        '----------------------------------------------------------------
        '与数据网格grdBDKZ相关的参数
        '----------------------------------------------------------------
        '网格中模板列中的控件ID
        Private Const m_cstrCheckBoxIdInDataGrid_BDKZ As String = "chkBDKZ"
        '包含网格的DIV对象ID
        Private Const m_cstrDataGridInDIV_BDKZ As String = "divBDKZ"
        '网格要锁定的列数
        Private m_intFixedColumns_BDKZ As Integer

        '----------------------------------------------------------------
        '当前处理的数据集
        '----------------------------------------------------------------
        Private m_objDataSet_BDKZ As Xydc.Platform.Common.Data.BudengshezhiData
        Private m_strQuery_BDKZ As String '记录m_objDataSet_BDKZ的搜索串
        Private m_intRows_BDKZ As Integer '记录m_objDataSet_BDKZ的DefaultView记录数

        '----------------------------------------------------------------
        '其他模块私用参数
        '----------------------------------------------------------------
        Private m_objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType '详细编辑模式
        Private m_blnEditMode As Boolean '是否为编辑状态
        Private m_intCurrentPageIndex As Integer '进入编辑模式前记录的页位置
        Private m_intCurrentSelectIndex As Integer '进入编辑模式前记录的行位置












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

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            Try
                If Me.m_objSaveScence Is Nothing Then Exit Try

                With Me.m_objSaveScence
                    Me.htxtCurrentPage.Value = .htxtCurrentPage
                    Me.htxtCurrentRow.Value = .htxtCurrentRow
                    Me.htxtEditMode.Value = .htxtEditMode
                    Me.htxtEditType.Value = .htxtEditType

                    Me.txtBDKZPageIndex.Text = .txtBDKZPageIndex
                    Me.txtBDKZPageSize.Text = .txtBDKZPageSize

                    Me.txtBDKZSearch_BCSM.Text = .txtBDKZSearch_BCSM
                    Me.txtBDKZSearch_BDFW.Text = .txtBDKZSearch_BDFW
                    Me.txtBDKZSearch_ZWMC.Text = .txtBDKZSearch_ZWMC

                    Me.htxtZWDM.Value = .htxtZWDM
                    Me.txtZWMC.Text = .txtZWMC
                    Me.txtZWLB.Text = .txtZWLB
                    Try
                        Me.ddlBCSM.SelectedIndex = .ddlBCSM_SelectedIndex
                    Catch ex As Exception
                    End Try
                    Try
                        Me.ddlBDFW.SelectedIndex = .ddlBDFW_SelectedIndex
                    Catch ex As Exception
                    End Try

                    Me.htxtDivLeftBody.Value = .htxtDivLeftBody
                    Me.htxtDivTopBody.Value = .htxtDivTopBody
                    Me.htxtDivLeftBDKZ.Value = .htxtDivLeftBDKZ
                    Me.htxtDivTopBDKZ.Value = .htxtDivTopBDKZ

                    Me.htxtSessionIdBDKZQuery.Value = .htxtSessionIdBDKZQuery

                    Me.htxtBDKZQuery.Value = .htxtBDKZQuery
                    Me.htxtBDKZRows.Value = .htxtBDKZRows
                    Me.htxtBDKZSort.Value = .htxtBDKZSort
                    Me.htxtBDKZSortColumnIndex.Value = .htxtBDKZSortColumnIndex
                    Me.htxtBDKZSortType.Value = .htxtBDKZSortType

                    Try
                        Me.grdBDKZ.SelectedIndex = .grdBDKZ_SelectedIndex
                    Catch ex As Exception
                    End Try
                    Try
                        Me.grdBDKZ.PageSize = .grdBDKZ_PageSize
                    Catch ex As Exception
                    End Try
                    Try
                        Me.grdBDKZ.CurrentPageIndex = .grdBDKZ_CurrentPageIndex
                    Catch ex As Exception
                    End Try
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
                Me.m_objSaveScence = New Xydc.Platform.BusinessFacade.IMXtpzBdkz

                '保存现场信息
                With Me.m_objSaveScence
                    .htxtCurrentPage = Me.htxtCurrentPage.Value
                    .htxtCurrentRow = Me.htxtCurrentRow.Value
                    .htxtEditMode = Me.htxtEditMode.Value
                    .htxtEditType = Me.htxtEditType.Value

                    .txtBDKZPageIndex = Me.txtBDKZPageIndex.Text
                    .txtBDKZPageSize = Me.txtBDKZPageSize.Text

                    .txtBDKZSearch_BCSM = Me.txtBDKZSearch_BCSM.Text
                    .txtBDKZSearch_BDFW = Me.txtBDKZSearch_BDFW.Text
                    .txtBDKZSearch_ZWMC = Me.txtBDKZSearch_ZWMC.Text

                    .htxtZWDM = Me.htxtZWDM.Value
                    .txtZWMC = Me.txtZWMC.Text
                    .txtZWLB = Me.txtZWLB.Text
                    .ddlBCSM_SelectedIndex = Me.ddlBCSM.SelectedIndex
                    .ddlBDFW_SelectedIndex = Me.ddlBDFW.SelectedIndex

                    .htxtDivLeftBody = Me.htxtDivLeftBody.Value
                    .htxtDivTopBody = Me.htxtDivTopBody.Value
                    .htxtDivLeftBDKZ = Me.htxtDivLeftBDKZ.Value
                    .htxtDivTopBDKZ = Me.htxtDivTopBDKZ.Value

                    .htxtSessionIdBDKZQuery = Me.htxtSessionIdBDKZQuery.Value

                    .htxtBDKZQuery = Me.htxtBDKZQuery.Value
                    .htxtBDKZRows = Me.htxtBDKZRows.Value
                    .htxtBDKZSort = Me.htxtBDKZSort.Value
                    .htxtBDKZSortColumnIndex = Me.htxtBDKZSortColumnIndex.Value
                    .htxtBDKZSortType = Me.htxtBDKZSortType.Value

                    .grdBDKZ_SelectedIndex = Me.grdBDKZ.SelectedIndex
                    .grdBDKZ_PageSize = Me.grdBDKZ.PageSize
                    .grdBDKZ_CurrentPageIndex = Me.grdBDKZ.CurrentPageIndex
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
        Private Function getDataFromCallModule(ByRef strErrMsg As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            Try
                If Me.IsPostBack = True Then Exit Try

                '===========================================================================================================================================================
                Dim objIDmxzGzgw As Xydc.Platform.BusinessFacade.IDmxzGzgw
                Try
                    objIDmxzGzgw = CType(Session.Item(Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.OSessionId)), Xydc.Platform.BusinessFacade.IDmxzGzgw)
                Catch ex As Exception
                    objIDmxzGzgw = Nothing
                End Try
                If Not (objIDmxzGzgw Is Nothing) Then
                    '返回值处理
                    Select Case objIDmxzGzgw.iSourceControlId.ToUpper()
                        Case "lnkCZSelectZWLIST".ToUpper()
                            '处理lnkCZSelectZWLIST返回
                            If objIDmxzGzgw.oExitMode = True Then
                                Me.txtZWLB.Text = objIDmxzGzgw.oZWLIST
                            End If
                        Case Else
                    End Select
                    '释放资源
                    Session.Remove(Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.OSessionId))
                    objIDmxzGzgw.Dispose()
                    objIDmxzGzgw = Nothing
                    Exit Try
                End If

                '===========================================================================================================================================================
                Dim objIDmxzJbdm As Xydc.Platform.BusinessFacade.IDmxzJbdm
                Try
                    objIDmxzJbdm = CType(Session.Item(Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.OSessionId)), Xydc.Platform.BusinessFacade.IDmxzJbdm)
                Catch ex As Exception
                    objIDmxzJbdm = Nothing
                End Try
                If Not (objIDmxzJbdm Is Nothing) Then
                    '返回值处理
                    Select Case objIDmxzJbdm.iSourceControlId.ToUpper()
                        Case "lnkCZSelectZW".ToUpper()
                            '处理lnkCZSelectZW返回
                            If objIDmxzJbdm.oExitMode = True Then
                                Me.htxtZWDM.Value = objIDmxzJbdm.oCodeValue
                                Me.txtZWMC.Text = objIDmxzJbdm.oNameValue
                            End If
                        Case Else
                    End Select
                    '释放资源
                    Session.Remove(Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.OSessionId))
                    objIDmxzJbdm.Dispose()
                    objIDmxzJbdm = Nothing
                    Exit Try
                End If

                '===========================================================================================================================================================
                Dim objISjcxCxtj As Xydc.Platform.BusinessFacade.ISjcxCxtj
                Try
                    objISjcxCxtj = CType(Session.Item(Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.OSessionId)), Xydc.Platform.BusinessFacade.ISjcxCxtj)
                Catch ex As Exception
                    objISjcxCxtj = Nothing
                End Try
                If Not (objISjcxCxtj Is Nothing) Then
                    If objISjcxCxtj.oExitMode = True Then
                        Dim objQueryData As Xydc.Platform.Common.Data.QueryData
                        Select Case objISjcxCxtj.iSourceControlId.ToUpper
                            Case "btnBDKZSearch".ToUpper
                                Me.htxtBDKZQuery.Value = objISjcxCxtj.oQueryString
                                If Me.htxtSessionIdBDKZQuery.Value.Trim = "" Then
                                    Me.htxtSessionIdBDKZQuery.Value = objPulicParameters.getNewGuid()
                                Else
                                    Try
                                        objQueryData = CType(Session(Me.htxtSessionIdBDKZQuery.Value), Xydc.Platform.Common.Data.QueryData)
                                    Catch ex As Exception
                                        objQueryData = Nothing
                                    End Try
                                    If Not (objQueryData Is Nothing) Then
                                        objQueryData.Dispose()
                                        objQueryData = Nothing
                                    End If
                                End If
                                Session.Add(Me.htxtSessionIdBDKZQuery.Value, objISjcxCxtj.oDataSetTJ)
                            Case Else
                        End Select
                    End If
                    '释放资源
                    Session.Remove(Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.OSessionId))
                    objISjcxCxtj.Dispose()
                    objISjcxCxtj = Nothing
                    Exit Try
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            getDataFromCallModule = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 释放接口参数(模块无返回数据时用)
        '----------------------------------------------------------------
        Private Sub releaseInterfaceParameters()

            Try
            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' 获取接口参数
        '----------------------------------------------------------------
        Private Function getInterfaceParameters(ByRef strErrMsg As String, ByRef blnContinue As Boolean) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim intEditType As Integer

            getInterfaceParameters = False
            blnContinue = True
            strErrMsg = ""

            Try
                '获取恢复现场参数
                Me.m_blnSaveScence = False
                If Me.IsPostBack = False Then
                    Dim strSessionId As String
                    strSessionId = objPulicParameters.getObjectValue(Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.MSessionId), "")
                    Try
                        Me.m_objSaveScence = CType(Session.Item(strSessionId), Xydc.Platform.BusinessFacade.IMXtpzBdkz)
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

                Me.m_blnEditMode = objPulicParameters.getObjectValue(Me.htxtEditMode.Value, False)
                Me.m_intCurrentPageIndex = objPulicParameters.getObjectValue(Me.htxtCurrentPage.Value, 0)
                Me.m_intCurrentSelectIndex = objPulicParameters.getObjectValue(Me.htxtCurrentRow.Value, -1)
                intEditType = objPulicParameters.getObjectValue(Me.htxtEditType.Value, 0)
                Try
                    Me.m_objenumEditType = CType(intEditType, Xydc.Platform.Common.Utilities.PulicParameters.enumEditType)
                Catch ex As Exception
                    Me.m_objenumEditType = Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eSelect
                End Try

                Me.m_strQuery_BDKZ = Me.htxtBDKZQuery.Value
                Me.m_intRows_BDKZ = objPulicParameters.getObjectValue(Me.htxtBDKZRows.Value, 0)
                Me.m_intFixedColumns_BDKZ = objPulicParameters.getObjectValue(Me.htxtBDKZFixed.Value, 0)

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
                Dim objQueryData As Xydc.Platform.Common.Data.QueryData
                If Me.htxtSessionIdBDKZQuery.Value.Trim <> "" Then
                    Try
                        objQueryData = CType(Session(Me.htxtSessionIdBDKZQuery.Value), Xydc.Platform.Common.Data.QueryData)
                    Catch ex As Exception
                        objQueryData = Nothing
                    End Try
                    If Not (objQueryData Is Nothing) Then
                        objQueryData.Dispose()
                        objQueryData = Nothing
                    End If
                    Session.Remove(Me.htxtSessionIdBDKZQuery.Value)
                    Me.htxtSessionIdBDKZQuery.Value = ""
                End If
            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' 获取grdBDKZ搜索条件(默认表前缀a.)
        '     strErrMsg      ：返回错误信息
        '     strQuery       ：返回的搜索条件
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function getQueryString_BDKZ( _
            ByRef strErrMsg As String, _
            ByRef strQuery As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            getQueryString_BDKZ = False
            strErrMsg = ""
            strQuery = ""

            Try
                '按“补登人职务”搜索
                Dim strZWMC As String = "a." + Xydc.Platform.Common.Data.BudengshezhiData.FIELD_GL_B_BUDENGSHEZHI_GWMC
                If Me.txtBDKZSearch_ZWMC.Text.Length > 0 Then Me.txtBDKZSearch_ZWMC.Text = Me.txtBDKZSearch_ZWMC.Text.Trim()
                If Me.txtBDKZSearch_ZWMC.Text <> "" Then
                    Me.txtBDKZSearch_ZWMC.Text = objPulicParameters.getNewSearchString(Me.txtBDKZSearch_ZWMC.Text)
                    If strQuery = "" Then
                        strQuery = strZWMC + " like '" + Me.txtBDKZSearch_ZWMC.Text + "%'"
                    Else
                        strQuery = strQuery + " and " + strZWMC + " like '" + Me.txtBDKZSearch_ZWMC.Text + "%'"
                    End If
                End If

                '按“补登范围”搜索
                Dim strBDFW As String = "a." + Xydc.Platform.Common.Data.BudengshezhiData.FIELD_GL_B_BUDENGSHEZHI_BDFWMC
                If Me.txtBDKZSearch_BDFW.Text.Length > 0 Then Me.txtBDKZSearch_BDFW.Text = Me.txtBDKZSearch_BDFW.Text.Trim()
                If Me.txtBDKZSearch_BDFW.Text <> "" Then
                    Me.txtBDKZSearch_BDFW.Text = objPulicParameters.getNewSearchString(Me.txtBDKZSearch_BDFW.Text)
                    If strQuery = "" Then
                        strQuery = strBDFW + " like '" + Me.txtBDKZSearch_BDFW.Text + "%'"
                    Else
                        strQuery = strQuery + " and " + strBDFW + " like '" + Me.txtBDKZSearch_BDFW.Text + "%'"
                    End If
                End If

                '按“补充说明”搜索
                Dim strBCSM As String = "a." + Xydc.Platform.Common.Data.BudengshezhiData.FIELD_GL_B_BUDENGSHEZHI_JSXZMC
                If Me.txtBDKZSearch_BCSM.Text.Length > 0 Then Me.txtBDKZSearch_BCSM.Text = Me.txtBDKZSearch_BCSM.Text.Trim()
                If Me.txtBDKZSearch_BCSM.Text <> "" Then
                    Me.txtBDKZSearch_BCSM.Text = objPulicParameters.getNewSearchString(Me.txtBDKZSearch_BCSM.Text)
                    If strQuery = "" Then
                        strQuery = strBCSM + " like '" + Me.txtBDKZSearch_BCSM.Text + "%'"
                    Else
                        strQuery = strQuery + " and " + strBCSM + " like '" + Me.txtBDKZSearch_BCSM.Text + "%'"
                    End If
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            getQueryString_BDKZ = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取grdBDKZ要显示的数据信息
        '     strErrMsg      ：返回错误信息
        '     strWhere       ：搜索字符串
        '     blnEditMode    ：当前编辑状态
        '     objenumEditType：详细操作模式
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function getModuleData_BDKZ( _
            ByRef strErrMsg As String, _
            ByVal strWhere As String, _
            ByVal blnEditMode As Boolean, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.BudengshezhiData.TABLE_GL_B_BUDENGSHEZHI
            Dim objsystemBudengshezhi As New Xydc.Platform.BusinessFacade.systemBudengshezhi

            getModuleData_BDKZ = False

            Try
                '备份Sort字符串
                Dim strSort As String
                strSort = Me.htxtBDKZSort.Value
                If strSort.Length > 0 Then strSort = strSort.Trim

                '释放资源
                If Not (Me.m_objDataSet_BDKZ Is Nothing) Then
                    Me.m_objDataSet_BDKZ.Dispose()
                    Me.m_objDataSet_BDKZ = Nothing
                End If

                '重新检索数据
                If objsystemBudengshezhi.getDataSet(strErrMsg, MyBase.UserId, MyBase.UserPassword, strWhere, Me.m_objDataSet_BDKZ) = False Then
                    GoTo errProc
                End If

                '恢复Sort字符串
                With Me.m_objDataSet_BDKZ.Tables(strTable)
                    .DefaultView.Sort = strSort
                End With

                If blnEditMode = False Then '查看模式
                    With Me.m_objDataSet_BDKZ.Tables(strTable)
                        .DefaultView.AllowNew = False
                    End With
                Else '编辑模式
                    Select Case objenumEditType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                            '增加1条空记录
                            With Me.m_objDataSet_BDKZ.Tables(strTable)
                                .DefaultView.AllowNew = True
                                .DefaultView.AddNew()
                            End With
                        Case Else
                            With Me.m_objDataSet_BDKZ.Tables(strTable)
                                .DefaultView.AllowNew = False
                            End With
                    End Select
                End If

                '缓存参数
                With Me.m_objDataSet_BDKZ.Tables(strTable)
                    Me.htxtBDKZRows.Value = .DefaultView.Count.ToString()
                    Me.m_intRows_BDKZ = .DefaultView.Count
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.BusinessFacade.systemBudengshezhi.SafeRelease(objsystemBudengshezhi)

            getModuleData_BDKZ = True
            Exit Function

errProc:
            Xydc.Platform.BusinessFacade.systemBudengshezhi.SafeRelease(objsystemBudengshezhi)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据屏幕搜索条件搜索grdBDKZ数据
        '     strErrMsg      ：返回错误信息
        '     blnEditMode    ：当前编辑状态
        '     objenumEditType：详细操作模式
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function searchModuleData_BDKZ( _
            ByRef strErrMsg As String, _
            ByVal blnEditMode As Boolean, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            searchModuleData_BDKZ = False

            Try
                '获取搜索字符串
                Dim strQuery As String
                If Me.getQueryString_BDKZ(strErrMsg, strQuery) = False Then
                    GoTo errProc
                End If

                '搜索数据
                If Me.getModuleData_BDKZ(strErrMsg, strQuery, blnEditMode, objenumEditType) = False Then
                    GoTo errProc
                End If

                '记录搜索字符串
                Me.m_strQuery_BDKZ = strQuery
                Me.htxtBDKZQuery.Value = Me.m_strQuery_BDKZ

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            searchModuleData_BDKZ = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示grdBDKZ的数据
        '     strErrMsg      ：返回错误信息
        '     blnEditMode    ：当前编辑状态
        '     objenumEditType：详细操作模式
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showDataGridInfo_BDKZ( _
            ByRef strErrMsg As String, _
            ByVal blnEditMode As Boolean, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.BudengshezhiData.TABLE_GL_B_BUDENGSHEZHI
            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess

            showDataGridInfo_BDKZ = False

            '获取系统保存的网格排序数据
            Dim intSortColumnIndex As Integer
            intSortColumnIndex = objPulicParameters.getObjectValue(Me.htxtBDKZSortColumnIndex.Value, -1)
            Dim objSortType As Xydc.Platform.Common.Utilities.PulicParameters.enumSortType
            Try
                objSortType = CType(Me.htxtBDKZSortType.Value, Xydc.Platform.Common.Utilities.PulicParameters.enumSortType)
            Catch ex As Exception
                objSortType = Xydc.Platform.Common.Utilities.PulicParameters.enumSortType.None
            End Try

            '网格显示处理
            Try
                '在获取数据时已经恢复了RowFilter、Sort的现场
                '设置数据源
                If Me.m_objDataSet_BDKZ Is Nothing Then
                    Me.grdBDKZ.DataSource = Nothing
                Else
                    With Me.m_objDataSet_BDKZ.Tables(strTable)
                        Me.grdBDKZ.DataSource = .DefaultView
                    End With
                End If

                '调整网格参数
                With Me.m_objDataSet_BDKZ.Tables(strTable)
                    If objDataGridProcess.onBeforeDataGridBind(strErrMsg, Me.grdBDKZ, .DefaultView.Count) = False Then
                        GoTo errProc
                    End If
                End With

                '如果是编辑模式
                If blnEditMode = True Then
                    '移动到最后记录
                    Select Case objenumEditType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                            With Me.m_objDataSet_BDKZ.Tables(strTable)
                                Dim intPageIndex As Integer
                                Dim intSelectIndex As Integer
                                If objDataGridProcess.doMoveToRecord(Me.grdBDKZ.AllowPaging, Me.grdBDKZ.PageSize, .DefaultView.Count - 1, intPageIndex, intSelectIndex) = False Then
                                    strErrMsg = "错误：无法移动到最后！"
                                    GoTo errProc
                                End If
                                Try
                                    Me.grdBDKZ.CurrentPageIndex = intPageIndex
                                    Me.grdBDKZ.SelectedIndex = intSelectIndex
                                Catch ex As Exception
                                End Try
                            End With

                        Case Else
                    End Select
                End If

                '允许列排序？
                Me.grdBDKZ.AllowSorting = Not blnEditMode

                '恢复列标题中的排序信息
                If intSortColumnIndex >= 0 Then
                    objDataGridProcess.doClearSortCharInDataGridHead(Me.grdBDKZ)
                    With Me.grdBDKZ.Columns(intSortColumnIndex)
                        .HeaderText = objDataGridProcess.getColumnSortHeadString(.HeaderText, objSortType)
                    End With
                End If

                '绑定数据
                Me.grdBDKZ.DataBind()

                '----------------------------------------------------------------
                '因为这些信息是非绑定的，所以下面的操作必须等绑定完成后执行！！！
                '一旦在后续处理中执行了DataBind，则信息会丢失！！！
                '----------------------------------------------------------------
                '恢复网格中的CheckBox状态
                If objDataGridProcess.doRestoreDataGridCheckBoxStatus(strErrMsg, Me.grdBDKZ, Request, 0, Me.m_cstrCheckBoxIdInDataGrid_BDKZ) = False Then
                    GoTo errProc
                End If

                '如果是编辑模式
                If blnEditMode = True Then
                    '使能网格
                    If objDataGridProcess.doEnabledDataGrid(strErrMsg, Me.grdBDKZ, Not blnEditMode) = False Then
                        GoTo errProc
                    End If
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)

            showDataGridInfo_BDKZ = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示编辑窗的数据(根据网格当前行数据显示)
        '     strErrMsg      ：返回错误信息
        '     blnEditMode    ：当前编辑状态
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showEditPanelInfo( _
            ByRef strErrMsg As String, _
            ByVal blnEditMode As Boolean) As Boolean

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objControlProcess As New Xydc.Platform.web.ControlProcess

            showEditPanelInfo = False

            Try
                If blnEditMode = False Then
                    '查看状态
                    If Me.grdBDKZ.Items.Count < 1 Or Me.grdBDKZ.SelectedIndex < 0 Then
                        Me.htxtZWDM.Value = ""
                        Me.txtZWMC.Text = ""
                        Me.txtZWLB.Text = ""
                        Me.ddlBDFW.SelectedIndex = -1
                        Me.ddlBCSM.SelectedIndex = -1
                    Else
                        Dim intColIndex(7) As Integer
                        intColIndex(0) = objDataGridProcess.getDataGridColumnIndex(Me.grdBDKZ, Xydc.Platform.Common.Data.BudengshezhiData.FIELD_GL_B_BUDENGSHEZHI_GWDM)
                        intColIndex(1) = objDataGridProcess.getDataGridColumnIndex(Me.grdBDKZ, Xydc.Platform.Common.Data.BudengshezhiData.FIELD_GL_B_BUDENGSHEZHI_GWMC)
                        intColIndex(2) = objDataGridProcess.getDataGridColumnIndex(Me.grdBDKZ, Xydc.Platform.Common.Data.BudengshezhiData.FIELD_GL_B_BUDENGSHEZHI_BDFW)
                        intColIndex(3) = objDataGridProcess.getDataGridColumnIndex(Me.grdBDKZ, Xydc.Platform.Common.Data.BudengshezhiData.FIELD_GL_B_BUDENGSHEZHI_BDFWMC)
                        intColIndex(4) = objDataGridProcess.getDataGridColumnIndex(Me.grdBDKZ, Xydc.Platform.Common.Data.BudengshezhiData.FIELD_GL_B_BUDENGSHEZHI_JSXZ)
                        intColIndex(5) = objDataGridProcess.getDataGridColumnIndex(Me.grdBDKZ, Xydc.Platform.Common.Data.BudengshezhiData.FIELD_GL_B_BUDENGSHEZHI_JSXZMC)
                        intColIndex(6) = objDataGridProcess.getDataGridColumnIndex(Me.grdBDKZ, Xydc.Platform.Common.Data.BudengshezhiData.FIELD_GL_B_BUDENGSHEZHI_ZWLB)
                        Me.htxtZWDM.Value = objDataGridProcess.getDataGridCellValue(Me.grdBDKZ.Items(Me.grdBDKZ.SelectedIndex), intColIndex(0))
                        Me.txtZWMC.Text = objDataGridProcess.getDataGridCellValue(Me.grdBDKZ.Items(Me.grdBDKZ.SelectedIndex), intColIndex(1))
                        Me.ddlBDFW.SelectedIndex = CType(objDataGridProcess.getDataGridCellValue(Me.grdBDKZ.Items(Me.grdBDKZ.SelectedIndex), intColIndex(2)), Integer)
                        Me.ddlBCSM.SelectedIndex = CType(objDataGridProcess.getDataGridCellValue(Me.grdBDKZ.Items(Me.grdBDKZ.SelectedIndex), intColIndex(4)), Integer) - 1
                        Me.txtZWLB.Text = objDataGridProcess.getDataGridCellValue(Me.grdBDKZ.Items(Me.grdBDKZ.SelectedIndex), intColIndex(6))
                    End If
                Else
                    '编辑状态
                    '自动恢复数据
                End If

                '使能控件
                objControlProcess.doEnabledControl(Me.txtZWMC, blnEditMode)
                objControlProcess.doEnabledControl(Me.txtZWLB, blnEditMode)
                objControlProcess.doEnabledControl(Me.ddlBDFW, blnEditMode)
                objControlProcess.doEnabledControl(Me.ddlBCSM, blnEditMode)

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.ControlProcess.SafeRelease(objControlProcess)

            showEditPanelInfo = True
            Exit Function

errProc:
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.ControlProcess.SafeRelease(objControlProcess)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示grdBDKZ的信息
        '     strErrMsg      ：返回错误信息
        '     blnEditMode    ：当前编辑状态
        '     objenumEditType：详细操作模式
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showModuleData_BDKZ( _
            ByRef strErrMsg As String, _
            ByVal blnEditMode As Boolean, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.BudengshezhiData.TABLE_GL_B_BUDENGSHEZHI
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objControlProcess As New Xydc.Platform.web.ControlProcess

            showModuleData_BDKZ = False

            Try
                '显示网格信息
                If Me.showDataGridInfo_BDKZ(strErrMsg, blnEditMode, objenumEditType) = False Then
                    GoTo errProc
                End If

                '显示与网格紧密相关的操作或信息提示
                With Me.m_objDataSet_BDKZ.Tables(strTable).DefaultView
                    '显示网格位置信息
                    Me.lblBDKZGridLocInfo.Text = objDataGridProcess.getDataGridLocation(Me.grdBDKZ, .Count)

                    '显示页面浏览功能
                    Me.lnkCZBDKZMoveFirst.Enabled = objDataGridProcess.canDoMoveFirstPage(Me.grdBDKZ, .Count) And (Not blnEditMode)
                    Me.lnkCZBDKZMoveLast.Enabled = objDataGridProcess.canDoMoveLastPage(Me.grdBDKZ, .Count) And (Not blnEditMode)
                    Me.lnkCZBDKZMovePrev.Enabled = objDataGridProcess.canDoMovePreviousPage(Me.grdBDKZ, .Count) And (Not blnEditMode)
                    Me.lnkCZBDKZMoveNext.Enabled = objDataGridProcess.canDoMoveNextPage(Me.grdBDKZ, .Count) And (Not blnEditMode)

                    '显示相关操作
                    Dim blnEnabled As Boolean
                    If .Count < 1 Then
                        blnEnabled = False
                    Else
                        blnEnabled = True
                    End If
                    Me.lnkCZBDKZDeSelectAll.Enabled = blnEnabled And (Not blnEditMode)
                    Me.lnkCZBDKZSelectAll.Enabled = blnEnabled And (Not blnEditMode)
                    Me.lnkCZBDKZGotoPage.Enabled = blnEnabled And (Not blnEditMode)
                    Me.lnkCZBDKZSetPageSize.Enabled = blnEnabled And (Not blnEditMode)

                    objControlProcess.doEnabledControl(Me.txtBDKZPageSize, Not blnEditMode)
                    objControlProcess.doEnabledControl(Me.txtBDKZPageIndex, Not blnEditMode)
                    objControlProcess.doEnabledControl(Me.txtBDKZSearch_ZWMC, Not blnEditMode)
                    objControlProcess.doEnabledControl(Me.txtBDKZSearch_BDFW, Not blnEditMode)
                    objControlProcess.doEnabledControl(Me.txtBDKZSearch_BCSM, Not blnEditMode)
                    Me.btnBDKZQuery.Enabled = Not blnEditMode
                End With

                '显示输入窗信息
                If Me.showEditPanelInfo(strErrMsg, blnEditMode) = False Then
                    GoTo errProc
                End If

                '显示操作命令
                Me.btnBDKZAddNew.Enabled = (Not blnEditMode) And Me.m_blnPrevilegeParams(1)
                Me.btnBDKZModify.Enabled = (Not blnEditMode) And Me.m_blnPrevilegeParams(2)
                Me.btnBDKZDelete.Enabled = (Not blnEditMode) And Me.m_blnPrevilegeParams(3)
                Me.btnBDKZSearch.Enabled = (Not blnEditMode) And Me.m_blnPrevilegeParams(4)
                Me.btnClose.Enabled = Not blnEditMode
                Me.btnSave.Enabled = blnEditMode
                Me.btnCancel.Enabled = blnEditMode
                Me.lnkCZSelectZW.Visible = blnEditMode
                Me.lnkCZSelectZWLIST.Visible = blnEditMode

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.ControlProcess.SafeRelease(objControlProcess)

            showModuleData_BDKZ = True
            Exit Function

errProc:
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.ControlProcess.SafeRelease(objControlProcess)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 初始化控件
        '----------------------------------------------------------------
        Private Function initializeControls(ByRef strErrMsg As String) As Boolean

            Dim objControlProcess As New Xydc.Platform.web.ControlProcess

            initializeControls = False

            '仅在第一次调用页面时执行
            If Me.IsPostBack = False Then
                Try
                    '显示Pannel(不论是否回调，始终显示panelMain)
                    Me.panelMain.Visible = True
                    Me.panelError.Visible = Not Me.panelMain.Visible

                    '执行键转译(不论是否是“回发”)
                    '*************************************************************************
                    objControlProcess.doTranslateKey(Me.txtZWMC)
                    objControlProcess.doTranslateKey(Me.txtZWLB)
                    objControlProcess.doTranslateKey(Me.ddlBDFW)
                    objControlProcess.doTranslateKey(Me.ddlBCSM)
                    '*************************************************************************
                    objControlProcess.doTranslateKey(Me.txtBDKZPageIndex)
                    objControlProcess.doTranslateKey(Me.txtBDKZPageSize)
                    '*************************************************************************
                    objControlProcess.doTranslateKey(Me.txtBDKZSearch_ZWMC)
                    objControlProcess.doTranslateKey(Me.txtBDKZSearch_BDFW)
                    objControlProcess.doTranslateKey(Me.txtBDKZSearch_BCSM)
                    '*************************************************************************

                    '显示grdBDKZ
                    If Me.getModuleData_BDKZ(strErrMsg, Me.m_strQuery_BDKZ, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                        GoTo errProc
                    End If
                    If Me.showModuleData_BDKZ(strErrMsg, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                        GoTo errProc
                    End If

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
            End If

            Xydc.Platform.web.ControlProcess.SafeRelease(objControlProcess)

            initializeControls = True
            Exit Function

errProc:
            Xydc.Platform.web.ControlProcess.SafeRelease(objControlProcess)
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

            '检查权限(不论是否回发！)
            Dim blnDo As Boolean
            If Me.getPrevilegeParams(strErrMsg, blnDo) = False Then
                GoTo errProc
            End If
            If blnDo = False Then GoTo normExit

            '获取接口参数
            If Me.getInterfaceParameters(strErrMsg, blnDo) = False Then
                GoTo errProc
            End If
            If blnDo = False Then GoTo normExit

            '控件初始化
            If Me.initializeControls(strErrMsg) = False Then
                GoTo errProc
            End If

            '具体审计日志
            If Me.IsPostBack = False Then
                If Me.m_blnSaveScence = False Then
                    Xydc.Platform.SystemFramework.ApplicationLog.WriteAuditPZInfo(Request.UserHostAddress, Request.UserHostName, "[" + MyBase.UserId + "]访问了[补登领导批示人员配置信息]！")
                End If
            End If

normExit:
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub




        '----------------------------------------------------------------
        '网格事件处理器
        '----------------------------------------------------------------
        Sub grdBDKZ_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles grdBDKZ.ItemDataBound

            Dim intCells As Integer
            Dim i As Integer

            Try
                If e.Item.ItemIndex < 0 Then
                    '标题行,输出标题锁定一般属性
                    intCells = e.Item.Cells.Count
                    For i = 0 To intCells - 1 Step 1
                        e.Item.Cells(i).Attributes.CssStyle.Add("top", "expression(" + Me.m_cstrDataGridInDIV_BDKZ + ".scrollTop)")
                    Next
                End If

                If Me.m_intFixedColumns_BDKZ > 0 Then
                    '锁定列
                    For i = 0 To Me.m_intFixedColumns_BDKZ - 1 Step 1
                        e.Item.Cells(i).CssClass = Me.grdBDKZ.ID + "Locked"
                    Next
                End If

            Catch ex As Exception
            End Try

        End Sub

        Private Sub grdBDKZ_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdBDKZ.SelectedIndexChanged

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim strErrMsg As String

            Try
                '显示记录位置
                Me.lblBDKZGridLocInfo.Text = objDataGridProcess.getDataGridLocation(Me.grdBDKZ, Me.m_intRows_BDKZ)

                '同步显示编辑窗信息
                If Me.showEditPanelInfo(strErrMsg, Me.m_blnEditMode) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub grdBDKZ_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles grdBDKZ.SortCommand

            Dim strTable As String = Xydc.Platform.Common.Data.BudengshezhiData.TABLE_GL_B_BUDENGSHEZHI
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                Dim objenumSortType As Xydc.Platform.Common.Utilities.PulicParameters.enumSortType
                Dim objDataGridItem As System.Web.UI.WebControls.DataGridItem
                Dim strFinalCommand As String
                Dim strOldCommand As String
                Dim strUniqueId As String
                Dim intColumnIndex As Integer

                '获取数据
                If Me.getModuleData_BDKZ(strErrMsg, Me.m_strQuery_BDKZ, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If

                '获取原排序命令
                With Me.m_objDataSet_BDKZ.Tables(strTable)
                    strOldCommand = .DefaultView.Sort
                End With

                '获取要执行的排序命令
                objDataGridProcess.getColumnSortCommand(strErrMsg, strOldCommand, e.SortExpression, strFinalCommand, objenumSortType)
                If strErrMsg <> "" Then
                    GoTo errProc
                End If

                '执行排序
                With Me.m_objDataSet_BDKZ.Tables(strTable)
                    .DefaultView.Sort = strFinalCommand
                End With

                '获取排序列的列索引
                objDataGridItem = CType(e.CommandSource, System.Web.UI.WebControls.DataGridItem)
                strUniqueId = Request.Form("__EVENTTARGET")
                intColumnIndex = objDataGridProcess.getColumnIndexByUniqueIdInRow(objDataGridItem, strUniqueId)

                '保存排序信息
                Me.htxtBDKZSortColumnIndex.Value = intColumnIndex.ToString()
                Me.htxtBDKZSortType.Value = CType(objenumSortType, Integer).ToString()
                Me.htxtBDKZSort.Value = strFinalCommand

                '重新显示数据
                If Me.showModuleData_BDKZ(strErrMsg, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub




        Private Sub doBDKZMoveFirst(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_BDKZ(strErrMsg, Me.m_strQuery_BDKZ, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(0, Me.grdBDKZ.PageCount)
                Me.grdBDKZ.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_BDKZ(strErrMsg, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub doBDKZMoveLast(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_BDKZ(strErrMsg, Me.m_strQuery_BDKZ, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(Me.grdBDKZ.PageCount - 1, Me.grdBDKZ.PageCount)
                Me.grdBDKZ.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_BDKZ(strErrMsg, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub doBDKZMoveNext(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_BDKZ(strErrMsg, Me.m_strQuery_BDKZ, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(Me.grdBDKZ.CurrentPageIndex + 1, Me.grdBDKZ.PageCount)
                Me.grdBDKZ.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_BDKZ(strErrMsg, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub doBDKZMovePrevious(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_BDKZ(strErrMsg, Me.m_strQuery_BDKZ, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(Me.grdBDKZ.CurrentPageIndex - 1, Me.grdBDKZ.PageCount)
                Me.grdBDKZ.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_BDKZ(strErrMsg, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub doBDKZGotoPage(ByVal strControlId As String)

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            '获取新的页大小
            Dim intPageIndex As Integer
            intPageIndex = objPulicParameters.getObjectValue(Me.txtBDKZPageIndex.Text, 0)
            If intPageIndex <= 0 Then
                intPageIndex = 0
            Else
                intPageIndex -= 1
            End If

            Try
                '获取数据
                If Me.getModuleData_BDKZ(strErrMsg, Me.m_strQuery_BDKZ, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If

                '设置新的页
                Me.grdBDKZ.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_BDKZ(strErrMsg, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If

                '信息同步
                Me.txtBDKZPageIndex.Text = (Me.grdBDKZ.CurrentPageIndex + 1).ToString()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub doBDKZSetPageSize(ByVal strControlId As String)

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            '获取新的页大小
            Dim intPageSize As Integer
            intPageSize = objPulicParameters.getObjectValue(Me.txtBDKZPageSize.Text, 100)
            If intPageSize <= 0 Then intPageSize = 100

            Try
                '获取数据
                If Me.getModuleData_BDKZ(strErrMsg, Me.m_strQuery_BDKZ, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If

                '设置新的页大小
                Me.grdBDKZ.PageSize = intPageSize

                '刷新网格显示
                If Me.showModuleData_BDKZ(strErrMsg, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If

                '信息同步
                Me.txtBDKZPageSize.Text = (Me.grdBDKZ.PageSize).ToString()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub doBDKZSelectAll(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                If objDataGridProcess.doCheckedDataGridCheckBox(strErrMsg, Me.grdBDKZ, 0, Me.m_cstrCheckBoxIdInDataGrid_BDKZ, True) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub doBDKZDeSelectAll(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                If objDataGridProcess.doCheckedDataGridCheckBox(strErrMsg, Me.grdBDKZ, 0, Me.m_cstrCheckBoxIdInDataGrid_BDKZ, False) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub doBDKZQuery(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '搜索数据
                If Me.searchModuleData_BDKZ(strErrMsg, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If
                If Me.showModuleData_BDKZ(strErrMsg, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
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

        Private Sub lnkCZBDKZMoveFirst_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZBDKZMoveFirst.Click
            Me.doBDKZMoveFirst("lnkCZBDKZMoveFirst")
        End Sub

        Private Sub lnkCZBDKZMoveLast_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZBDKZMoveLast.Click
            Me.doBDKZMoveLast("lnkCZBDKZMoveLast")
        End Sub

        Private Sub lnkCZBDKZMoveNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZBDKZMoveNext.Click
            Me.doBDKZMoveNext("lnkCZBDKZMoveNext")
        End Sub

        Private Sub lnkCZBDKZMovePrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZBDKZMovePrev.Click
            Me.doBDKZMovePrevious("lnkCZBDKZMovePrev")
        End Sub

        Private Sub lnkCZBDKZGotoPage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZBDKZGotoPage.Click
            Me.doBDKZGotoPage("lnkCZBDKZGotoPage")
        End Sub

        Private Sub lnkCZBDKZSetPageSize_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZBDKZSetPageSize.Click
            Me.doBDKZSetPageSize("lnkCZBDKZSetPageSize")
        End Sub

        Private Sub lnkCZBDKZSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZBDKZSelectAll.Click
            Me.doBDKZSelectAll("lnkCZBDKZSelectAll")
        End Sub

        Private Sub lnkCZBDKZDeSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZBDKZDeSelectAll.Click
            Me.doBDKZDeSelectAll("lnkCZBDKZDeSelectAll")
        End Sub

        Private Sub btnBDKZQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBDKZQuery.Click
            Me.doBDKZQuery("btnBDKZQuery")
        End Sub




        '----------------------------------------------------------------
        '模块特殊操作处理器
        '----------------------------------------------------------------
        Private Sub doBDKZAddNew(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '设置编辑模式
                Me.m_blnEditMode = True
                Me.m_objenumEditType = Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                Me.m_intCurrentPageIndex = Me.grdBDKZ.CurrentPageIndex
                Me.m_intCurrentSelectIndex = Me.grdBDKZ.SelectedIndex

                '保存相关信息
                Me.htxtEditMode.Value = Me.m_blnEditMode.ToString()
                Me.htxtEditType.Value = CType(Me.m_objenumEditType, Integer).ToString()
                Me.htxtCurrentPage.Value = Me.m_intCurrentPageIndex.ToString()
                Me.htxtCurrentRow.Value = Me.m_intCurrentSelectIndex.ToString()

                '进入编辑状态
                If Me.getModuleData_BDKZ(strErrMsg, Me.m_strQuery_BDKZ, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If
                If Me.showModuleData_BDKZ(strErrMsg, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If

                '设置初始值
                Me.htxtZWDM.Value = ""
                Me.txtZWMC.Text = ""
                Me.txtZWLB.Text = ""
                Me.ddlBDFW.SelectedIndex = -1
                Me.ddlBCSM.SelectedIndex = -1

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

        Private Sub doBDKZModify(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '检查
                If Me.grdBDKZ.Items.Count < 1 Then
                    strErrMsg = "错误：没有内容可修改！"
                    GoTo errProc
                End If

                '设置编辑模式
                Me.m_blnEditMode = True
                Me.m_objenumEditType = Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eUpdate
                Me.m_intCurrentPageIndex = Me.grdBDKZ.CurrentPageIndex
                Me.m_intCurrentSelectIndex = Me.grdBDKZ.SelectedIndex

                '保存相关信息
                Me.htxtEditMode.Value = Me.m_blnEditMode.ToString()
                Me.htxtEditType.Value = CType(Me.m_objenumEditType, Integer).ToString()
                Me.htxtCurrentPage.Value = Me.m_intCurrentPageIndex.ToString()
                Me.htxtCurrentRow.Value = Me.m_intCurrentSelectIndex.ToString()

                '进入编辑状态
                If Me.getModuleData_BDKZ(strErrMsg, Me.m_strQuery_BDKZ, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If
                If Me.showModuleData_BDKZ(strErrMsg, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
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

        Private Sub doSave(ByVal strControlId As String)

            Dim strTable As String = Xydc.Platform.Common.Data.BudengshezhiData.TABLE_GL_B_BUDENGSHEZHI
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim objsystemBudengshezhi As New Xydc.Platform.BusinessFacade.systemBudengshezhi
            Dim objNewData As New System.Collections.Specialized.ListDictionary

            Try
                '检查
                If Me.ddlBDFW.SelectedIndex < 0 Then
                    strErrMsg = "错误：没有选择督办范围！"
                    GoTo errProc
                End If
                If Me.ddlBCSM.SelectedIndex < 0 Then
                    strErrMsg = "错误：没有选择督办范围补充说明！"
                    GoTo errProc
                End If

                '获取新信息
                objNewData.Add(Xydc.Platform.Common.Data.BudengshezhiData.FIELD_GL_B_BUDENGSHEZHI_GWDM, Me.htxtZWDM.Value)
                objNewData.Add(Xydc.Platform.Common.Data.BudengshezhiData.FIELD_GL_B_BUDENGSHEZHI_ZWLB, Me.txtZWLB.Text)
                objNewData.Add(Xydc.Platform.Common.Data.BudengshezhiData.FIELD_GL_B_BUDENGSHEZHI_BDFW, Me.ddlBDFW.SelectedIndex.ToString)
                objNewData.Add(Xydc.Platform.Common.Data.BudengshezhiData.FIELD_GL_B_BUDENGSHEZHI_JSXZ, (Me.ddlBCSM.SelectedIndex + 1).ToString)

                '获取旧信息
                Dim objOldData As System.Data.DataRow
                Dim intPos As Integer
                Select Case Me.m_objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                        objOldData = Nothing
                    Case Else
                        '获取数据
                        If Me.getModuleData_BDKZ(strErrMsg, Me.m_strQuery_BDKZ, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                            GoTo errProc
                        End If
                        '获取当前行数据
                        intPos = objDataGridProcess.getRecordPosition(Me.grdBDKZ.SelectedIndex, Me.grdBDKZ.CurrentPageIndex, Me.grdBDKZ.PageSize)
                        With Me.m_objDataSet_BDKZ.Tables(strTable)
                            objOldData = .DefaultView.Item(intPos).Row
                        End With
                End Select

                '保存信息
                If objsystemBudengshezhi.doSaveData(strErrMsg, MyBase.UserId, MyBase.UserPassword, objOldData, objNewData, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If

                '记录审计日志
                Select Case Me.m_objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew, _
                        Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eCpyNew
                        Xydc.Platform.SystemFramework.ApplicationLog.WriteAuditPZInfo(Request.UserHostAddress, Request.UserHostName, "[" + MyBase.UserId + "]增加了[" + Me.txtZWMC.Text + "]的[补登领导批示配置信息]！")
                    Case Else
                        Xydc.Platform.SystemFramework.ApplicationLog.WriteAuditPZInfo(Request.UserHostAddress, Request.UserHostName, "[" + MyBase.UserId + "]修改了[" + Me.txtZWMC.Text + "]的[补登领导批示配置信息]！")
                End Select

                '最终设置编辑模式
                Me.m_blnEditMode = False
                Me.m_objenumEditType = Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eSelect

                '保存相关信息
                Me.htxtEditMode.Value = Me.m_blnEditMode.ToString()
                Me.htxtEditType.Value = CType(Me.m_objenumEditType, Integer).ToString()

                '设置记录位置
                '保存成功，停留在当前位置

                '重新获取数据
                If Me.getModuleData_BDKZ(strErrMsg, Me.m_strQuery_BDKZ, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If
                If Me.showModuleData_BDKZ(strErrMsg, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.BusinessFacade.systemBudengshezhi.SafeRelease(objsystemBudengshezhi)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objNewData)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.BusinessFacade.systemBudengshezhi.SafeRelease(objsystemBudengshezhi)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objNewData)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub doCancel(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String
            Dim intStep As Integer

            Try
                '提示信息
                intStep = 1
                If objMessageProcess.isExecuteCode(Request, Me.popMessageObject.UniqueID, intStep) = True Then
                    objMessageProcess.doConfirmMessage(Me.popMessageObject, "提示：您确实准备取消吗（是/否）？", strControlId, intStep)
                    Exit Try
                Else
                    objMessageProcess.doResetPopMessage(Me.popMessageObject)
                End If

                '提示后回答“是”接着处理
                intStep = 2
                If objMessageProcess.isExecuteCode(Request, Me.popMessageObject.UniqueID, intStep) = True Then
                    '取消编辑
                    Me.m_blnEditMode = False
                    Me.m_objenumEditType = Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eSelect

                    '保存相关信息
                    Me.htxtEditMode.Value = Me.m_blnEditMode.ToString()
                    Me.htxtEditType.Value = CType(Me.m_objenumEditType, Integer).ToString()

                    '恢复到编辑前的记录位置
                    Try
                        Me.grdBDKZ.CurrentPageIndex = Me.m_intCurrentPageIndex
                        Me.grdBDKZ.SelectedIndex = Me.m_intCurrentSelectIndex
                    Catch ex As Exception
                    End Try

                    '进入非编辑状态
                    If Me.getModuleData_BDKZ(strErrMsg, Me.m_strQuery_BDKZ, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                        GoTo errProc
                    End If
                    If Me.showModuleData_BDKZ(strErrMsg, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                        GoTo errProc
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

        Private Sub doBDKZDelete(ByVal strControlId As String)

            Dim objsystemBudengshezhi As New Xydc.Platform.BusinessFacade.systemBudengshezhi
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String
            Dim intStep As Integer

            Try
                intStep = 1
                '检查选择
                Dim intSelect As Integer = 0
                Dim intRows As Integer
                Dim i As Integer
                intRows = Me.grdBDKZ.Items.Count
                If objMessageProcess.isExecuteCode(Request, Me.popMessageObject.UniqueID, intStep) = True Then
                    For i = 0 To intRows - 1 Step 1
                        If objDataGridProcess.isDataGridItemChecked(Me.grdBDKZ.Items(i), 0, Me.m_cstrCheckBoxIdInDataGrid_BDKZ) = True Then
                            intSelect += 1
                        End If
                    Next
                    If intSelect < 1 Then
                        strErrMsg = "错误：未选择要删除的内容！"
                        GoTo errProc
                    End If
                End If

                '询问
                intStep = 2
                If objMessageProcess.isExecuteCode(Request, Me.popMessageObject.UniqueID, intStep) = True Then
                    objMessageProcess.doConfirmMessage(Me.popMessageObject, "提示：您确实准备删除选定的[" + intSelect.ToString() + "]条内容吗（是/否）？", strControlId, intStep)
                    Exit Try
                Else
                    objMessageProcess.doResetPopMessage(Me.popMessageObject)
                End If

                '提示后回答“是”接着处理
                intStep = 3
                If objMessageProcess.isExecuteCode(Request, Me.popMessageObject.UniqueID, intStep) = True Then
                    '获取数据
                    If Me.getModuleData_BDKZ(strErrMsg, Me.m_strQuery_BDKZ, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                        GoTo errProc
                    End If

                    '逐个删除
                    Dim objOldData As System.Data.DataRow
                    Dim intPos As Integer
                    Dim intColIndex As Integer
                    Dim strGWMC As String
                    intColIndex = objDataGridProcess.getDataGridColumnIndex(Me.grdBDKZ, Xydc.Platform.Common.Data.BudengshezhiData.FIELD_GL_B_BUDENGSHEZHI_GWMC)
                    For i = intRows - 1 To 0 Step -1
                        If objDataGridProcess.isDataGridItemChecked(Me.grdBDKZ.Items(i), 0, Me.m_cstrCheckBoxIdInDataGrid_BDKZ) = True Then
                            strGWMC = objDataGridProcess.getDataGridCellValue(Me.grdBDKZ.Items(i), intColIndex)

                            '获取要删除的数据
                            intPos = objDataGridProcess.getRecordPosition(i, Me.grdBDKZ.CurrentPageIndex, Me.grdBDKZ.PageSize)
                            objOldData = Nothing
                            With Me.m_objDataSet_BDKZ.Tables(Xydc.Platform.Common.Data.BudengshezhiData.TABLE_GL_B_BUDENGSHEZHI)
                                objOldData = .DefaultView.Item(intPos).Row
                            End With

                            '删除处理
                            If objsystemBudengshezhi.doDeleteData(strErrMsg, MyBase.UserId, MyBase.UserPassword, objOldData) = False Then
                                GoTo errProc
                            End If

                            '记录审计日志
                            Xydc.Platform.SystemFramework.ApplicationLog.WriteAuditPZInfo(Request.UserHostAddress, Request.UserHostName, "[" + MyBase.UserId + "]删除了[" + strGWMC + "]的[补登领导批示配置信息]！")
                        End If
                    Next

                    '重新获取数据
                    If Me.getModuleData_BDKZ(strErrMsg, Me.m_strQuery_BDKZ, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                        GoTo errProc
                    End If
                    If Me.showModuleData_BDKZ(strErrMsg, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                        GoTo errProc
                    End If
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.BusinessFacade.systemBudengshezhi.SafeRelease(objsystemBudengshezhi)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.BusinessFacade.systemBudengshezhi.SafeRelease(objsystemBudengshezhi)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub doBDKZSearch(ByVal strControlId As String)

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim objISjcxCxtj As Xydc.Platform.BusinessFacade.ISjcxCxtj
            Dim strNewSessionId As String
            Dim strMSessionId As String

            Dim strTable As String = Xydc.Platform.Common.Data.BudengshezhiData.TABLE_GL_B_BUDENGSHEZHI

            Try
                '获取数据
                If Me.getModuleData_BDKZ(strErrMsg, Me.m_strQuery_BDKZ, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If

                '备份现场参数
                strMSessionId = Me.saveModuleInformation()
                If strMSessionId = "" Then
                    strErrMsg = "错误：不能保存现场信息！"
                    GoTo errProc
                End If

                '准备调用接口
                Dim strUrl As String
                objISjcxCxtj = New Xydc.Platform.BusinessFacade.ISjcxCxtj
                With objISjcxCxtj
                    If Me.htxtSessionIdBDKZQuery.Value.Trim <> "" Then
                        .iDataSetTJ = CType(Session(Me.htxtSessionIdBDKZQuery.Value), Xydc.Platform.Common.Data.QueryData)
                    Else
                        .iDataSetTJ = Nothing
                    End If
                    .iQueryTable = Me.m_objDataSet_BDKZ.Tables(strTable)
                    .iFixQuery = ""

                    .iSourceControlId = strControlId
                    strUrl = ""
                    strUrl += Request.Url.AbsolutePath
                    strUrl += "?"
                    strUrl += Xydc.Platform.Common.Utilities.PulicParameters.MSessionId
                    strUrl += "="
                    strUrl += strMSessionId
                    .iReturnUrl = strUrl
                End With

                '调用模块
                strNewSessionId = objPulicParameters.getNewGuid()
                If strNewSessionId = "" Then
                    strErrMsg = "错误：不能初始化调用接口！"
                    GoTo errProc
                End If
                Session.Add(strNewSessionId, objISjcxCxtj)
                strUrl = ""
                strUrl += "../sjcx/sjcx_cxtj.aspx"
                strUrl += "?"
                strUrl += Xydc.Platform.Common.Utilities.PulicParameters.ISessionId
                strUrl += "="
                strUrl += strNewSessionId
                Response.Redirect(strUrl)

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub doSelectZW(ByVal strControlId As String)

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim objsystemGongzuogangwei As New Xydc.Platform.BusinessFacade.systemGongzuogangwei

            Try
                '备份现场参数
                Dim strMSessionId As String
                strMSessionId = Me.saveModuleInformation()
                If strMSessionId = "" Then
                    strErrMsg = "错误：不能保存现场信息！"
                    GoTo errProc
                End If

                '准备调用接口
                Dim objIDmxzJbdm As Xydc.Platform.BusinessFacade.IDmxzJbdm
                Dim strUrl As String
                objIDmxzJbdm = New Xydc.Platform.BusinessFacade.IDmxzJbdm
                With objIDmxzJbdm
                    .iAllowInput = True
                    .iAllowNull = True
                    .iCodeField = Xydc.Platform.Common.Data.GongzuogangweiData.FIELD_GG_B_GONGZUOGANGWEI_GWDM
                    .iInitField = Xydc.Platform.Common.Data.GongzuogangweiData.FIELD_GG_B_GONGZUOGANGWEI_GWMC
                    .iInitValue = Me.txtZWMC.Text
                    .iMultiSelect = False
                    .iNameField = Xydc.Platform.Common.Data.GongzuogangweiData.FIELD_GG_B_GONGZUOGANGWEI_GWMC
                    .iTitle = "选择职务"
                    .iRowSourceSQL = objsystemGongzuogangwei.getGongzuogangweiSQL()

                    .iSourceControlId = strControlId
                    strUrl = ""
                    strUrl += Request.Url.AbsolutePath
                    strUrl += "?"
                    strUrl += Xydc.Platform.Common.Utilities.PulicParameters.MSessionId
                    strUrl += "="
                    strUrl += strMSessionId
                    .iReturnUrl = strUrl
                End With

                '调用模块
                Dim strNewSessionId As String
                strNewSessionId = objPulicParameters.getNewGuid()
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

            Xydc.Platform.BusinessFacade.systemGongzuogangwei.SafeRelease(objsystemGongzuogangwei)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.BusinessFacade.systemGongzuogangwei.SafeRelease(objsystemGongzuogangwei)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub doSelectZWLIST(ByVal strControlId As String)

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '备份现场参数
                Dim strMSessionId As String
                strMSessionId = Me.saveModuleInformation()
                If strMSessionId = "" Then
                    strErrMsg = "错误：不能保存现场信息！"
                    GoTo errProc
                End If

                '准备调用接口
                Dim objIDmxzGzgw As Xydc.Platform.BusinessFacade.IDmxzGzgw
                Dim strUrl As String
                objIDmxzGzgw = New Xydc.Platform.BusinessFacade.IDmxzGzgw
                With objIDmxzGzgw
                    .iAllowNull = True
                    .iMultiSelect = True
                    .iZWLIST = Me.txtZWLB.Text

                    .iSourceControlId = strControlId
                    strUrl = ""
                    strUrl += Request.Url.AbsolutePath
                    strUrl += "?"
                    strUrl += Xydc.Platform.Common.Utilities.PulicParameters.MSessionId
                    strUrl += "="
                    strUrl += strMSessionId
                    .iReturnUrl = strUrl
                End With

                '调用模块
                Dim strNewSessionId As String
                strNewSessionId = objPulicParameters.getNewGuid()
                If strNewSessionId = "" Then
                    strErrMsg = "错误：不能初始化调用接口！"
                    GoTo errProc
                End If
                Session.Add(strNewSessionId, objIDmxzGzgw)

                strUrl = ""
                strUrl += "../dmxz/dmxz_gzgw.aspx"
                strUrl += "?"
                strUrl += Xydc.Platform.Common.Utilities.PulicParameters.ISessionId
                strUrl += "="
                strUrl += strNewSessionId
                Response.Redirect(strUrl)

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        '----------------------------------------------------------------
        ' 返回上级
        '     strControlId   ：当前操作控件ID
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Sub doClose(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String = ""

            Try
                Dim strUrl As String = Xydc.Platform.Common.jsoaConfiguration.GeneralReturnUrl

                '释放模块资源
                Me.releaseModuleParameters()
                Me.releaseInterfaceParameters()

                '返回到欢迎页面
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

        Private Sub btnBDKZSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBDKZSearch.Click
            Me.doBDKZSearch("btnBDKZSearch")
        End Sub

        Private Sub btnBDKZAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBDKZAddNew.Click
            Me.doBDKZAddNew("btnBDKZAddNew")
        End Sub

        Private Sub btnBDKZModify_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBDKZModify.Click
            Me.doBDKZModify("btnBDKZModify")
        End Sub

        Private Sub btnBDKZDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBDKZDelete.Click
            Me.doBDKZDelete("btnBDKZDelete")
        End Sub

        Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
            Me.doSave("btnSave")
        End Sub

        Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
            Me.doCancel("btnCancel")
        End Sub

        Private Sub lnkCZSelectZW_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZSelectZW.Click
            Me.doSelectZW("lnkCZSelectZW")
        End Sub

        Private Sub lnkCZSelectZWLIST_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZSelectZWLIST.Click
            Me.doSelectZWLIST("lnkCZSelectZWLIST")
        End Sub

        Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
            Me.doClose("btnClose")
        End Sub

    End Class
End Namespace
