
Imports System.Web.Security

Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform
    ' 类名    ：sunshineData_RegionInfo_compute
    ' 
    ' 调用性质：
    '     I/O
    '
    ' 功能描述： 
    '   　“阳光家缘房产项目信息数据分析”处理模块
    '----------------------------------------------------------------


    Partial Public Class sunshineData_RegionInfo_compute_v3
        Inherits Xydc.Platform.web.PageBase


        '----------------------------------------------------------------
        '模块私用参数
        '----------------------------------------------------------------
        '本模块相对image的相对路径
        Private m_cstrRelativePathToImage As String = "../../../"
        '打印模版相对于应用根的路径
        Private m_cstrExcelMBRelativePathToAppRoot As String = "/template/excel/"
        '打印文件缓存目录相对于应用根的路径
        Private m_cstrPrintCacheRelativePathToAppRoot As String = "/temp/printcache/"


        '----------------------------------------------------------------
        '模块授权参数
        '----------------------------------------------------------------
        Private m_cstrPrevilegeParamPrefix As String = "sunshineData_houseInfo_compute_previlege_param"
        Private m_blnPrevilegeParams(3) As Boolean

        '----------------------------------------------------------------
        '模块现场保留参数，恢复完成后立即释放session资源
        '----------------------------------------------------------------
        Private m_objSaveScence As Xydc.Platform.BusinessFacade.IMDeepData_monthCompute
        Private m_blnSaveScence As Boolean

        '----------------------------------------------------------------
        '模块接口参数
        '----------------------------------------------------------------
        Private m_objInterface As Xydc.Platform.BusinessFacade.IDeepData_monthCompute
        Private m_blnInterface As Boolean


        '----------------------------------------------------------------
        '与数据网格grdCompute相关的参数
        '----------------------------------------------------------------
        Private Const m_cstrCheckBoxIdInDataGrid_Compute As String = "chkCompute"
        Private Const m_cstrDataGridInDIV_Compute As String = "divCompute"
        Private m_intFixedColumns_Compute As Integer

        '----------------------------------------------------------------
        '当前处理的数据集
        '----------------------------------------------------------------
        Private m_objDataSet_Compute As Xydc.Platform.Common.Data.SunshineData
        Private m_strQuery_Compute As String
        Private m_intRows_Compute As Integer

        '----------------------------------------------------------------
        '其他模块私用参数
        '----------------------------------------------------------------
        Private m_blnQxControl As Boolean


        '定义日楼盘数据时间
        Private dayHouseDataTime As DateTime



        Public ReadOnly Property propRYMC() As String
            Get
                propRYMC = MyBase.UserZM
            End Get
        End Property



        Private Sub doGoBack(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                Dim strSessionId As String
                Dim strUrl As String
                strSessionId = Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.ISessionId)
                If strSessionId Is Nothing Then strSessionId = ""
                If strSessionId <> "" Then
                    Try
                        Me.m_objInterface = CType(Session(strSessionId), Xydc.Platform.BusinessFacade.IDeepData_monthCompute)
                    Catch ex As Exception
                        Me.m_objInterface = Nothing
                    End Try
                Else
                    Me.m_objInterface = Nothing
                End If
                If Not (Me.m_objInterface Is Nothing) Then
                    '设置返回参数
                    '返回到调用模块，并附加返回参数
                    '要返回的SessionId
                    strSessionId = Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.ISessionId)
                    'SessionId附加到返回的Url
                    strUrl = Me.m_objInterface.getReturnUrl(Server, Xydc.Platform.Common.Utilities.PulicParameters.OSessionId, strSessionId)
                Else
                    strUrl = Xydc.Platform.Common.jsoaConfiguration.GeneralReturnUrl
                End If
                '释放模块资源
                Me.releaseModuleParameters()
                Me.releaseInterfaceParameters()
                '返回
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



        Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
            Me.doGoBack("btnGoBack")
        End Sub

        Private Sub lnkExportData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkExportData.Click
            Me.doPrint("lnkExportData")
        End Sub

        Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try

                If Me.searchModuleData_Compute(strErrMsg, True) = False Then
                    GoTo errProc
                End If

                If Me.showModuleData(strErrMsg) = False Then
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
                Me.m_blnPrevilegeParams(0) = True
                Me.m_blnPrevilegeParams(1) = True
                Me.m_blnPrevilegeParams(2) = True
                Me.m_blnPrevilegeParams(3) = True
                blnContinueExecute = True
                'If Me.m_blnPrevilegeParams(0) = True Then
                '    blnContinueExecute = True
                'Else
                '    Me.panelError.Visible = True
                '    Me.lblMessage.Text = "错误：您没有[" + strFirstParamValue + "]的执行权限，请与系统管理员联系，谢谢！"
                '    Me.panelMain.Visible = Not Me.panelError.Visible
                'End If
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

            Try
                If Me.m_objSaveScence Is Nothing Then
                    Exit Try
                End If

                With Me.m_objSaveScence

                    Me.htxtComputeQuery.Value = .htxtComputeQuery
                    Me.htxtType.Value = .htxtType
                    Me.htxtSessionIdQuery.Value = .htxtSessionIdQuery
                    Me.htxtStartDate.Value = .htxtStartDate
                    Me.htxtEndDate.Value = .htxtEndDate

                End With

                '释放资源
                Session.Remove(strSessionId)
                Me.m_objSaveScence.Dispose()
                Me.m_objSaveScence = Nothing
            Catch ex As Exception
            End Try

            Exit Sub

        End Sub

        '----------------------------------------------------------------
        ' 保存模块现场信息并返回相应的SessionId
        '----------------------------------------------------------------
        Private Function saveModuleInformation() As String

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            Dim strSessionId As String = ""

            saveModuleInformation = ""

            Try
                '创建SessionId
                strSessionId = objPulicParameters.getNewGuid()
                If strSessionId = "" Then
                    Exit Try
                End If

                '创建对象
                Me.m_objSaveScence = New Xydc.Platform.BusinessFacade.IMDeepData_monthCompute

                '保存现场信息
                With Me.m_objSaveScence

                    .htxtComputeQuery = Me.htxtComputeQuery.Value
                    .htxtType = Me.htxtType.Value
                    .htxtSessionIdQuery = Me.htxtSessionIdQuery.Value
                    .htxtStartDate = Me.htxtStartDate.Value
                    .htxtEndDate = Me.htxtEndDate.Value
                End With

                '缓存对象
                Session.Add(strSessionId, Me.m_objSaveScence)
            Catch ex As Exception
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            saveModuleInformation = strSessionId
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 从调用模块中获取数据
        '----------------------------------------------------------------
        Private Function getDataFromCallModule(ByRef strErrMsg As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objsystemCustomer As New Xydc.Platform.BusinessFacade.systemCustomer

            getDataFromCallModule = False

            Try
                If Me.IsPostBack = True Then
                    Exit Try
                End If

                '==========================================================================================================================================================
                Dim objISjcxCxtj As Xydc.Platform.BusinessFacade.ISjcxCxtj
                Try
                    objISjcxCxtj = CType(Session.Item(Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.OSessionId)), Xydc.Platform.BusinessFacade.ISjcxCxtj)
                Catch ex As Exception
                    objISjcxCxtj = Nothing
                End Try
                If Not (objISjcxCxtj Is Nothing) Then
                    If objISjcxCxtj.oExitMode = True Then
                        Dim objQueryData As Xydc.Platform.Common.Data.QueryData
                        Me.htxtComputeQuery.Value = objISjcxCxtj.oQueryString
                        If Me.htxtSessionIdQuery.Value.Trim = "" Then
                            Me.htxtSessionIdQuery.Value = objPulicParameters.getNewGuid()
                        Else
                            Try
                                objQueryData = CType(Session(Me.htxtSessionIdQuery.Value), Xydc.Platform.Common.Data.QueryData)
                            Catch ex As Exception
                                objQueryData = Nothing
                            End Try
                            Xydc.Platform.Common.Data.QueryData.SafeRelease(objQueryData)
                        End If
                        Session.Add(Me.htxtSessionIdQuery.Value, objISjcxCxtj.oDataSetTJ)
                    End If
                    Session.Remove(Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.OSessionId))
                    Xydc.Platform.BusinessFacade.ISjcxCxtj.SafeRelease(objISjcxCxtj)
                    Exit Try
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.BusinessFacade.systemCustomer.SafeRelease(objsystemCustomer)

            getDataFromCallModule = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
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

            Exit Sub

        End Sub

        '----------------------------------------------------------------
        ' 获取接口参数
        '----------------------------------------------------------------
        Private Function getInterfaceParameters(ByRef strErrMsg As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            getInterfaceParameters = False

            Try
                '从QueryString中解析接口参数(不论是否回发)
                Dim objTemp As Object = Nothing
                Try
                    objTemp = Session.Item(Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.ISessionId))
                    m_objInterface = CType(objTemp, Xydc.Platform.BusinessFacade.IDeepData_monthCompute)
                Catch ex As Exception
                    m_objInterface = Nothing
                End Try
                If m_objInterface Is Nothing Then
                    Me.m_blnInterface = False
                    '没有有接口参数
                Else
                    Me.m_blnInterface = True
                    '有接口参数
                End If



                '获取恢复现场参数
                If Me.IsPostBack = False Then
                    Dim strSessionId As String
                    strSessionId = objPulicParameters.getObjectValue(Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.MSessionId), "")
                    Try
                        Me.m_objSaveScence = CType(Session.Item(strSessionId), Xydc.Platform.BusinessFacade.IMDeepData_monthCompute)
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

                Me.m_strQuery_Compute = Me.htxtComputeQuery.Value
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

            Try
                If Me.htxtSessionIdQuery.Value.Trim <> "" Then
                    Dim objQueryData As Xydc.Platform.Common.Data.QueryData
                    Try
                        objQueryData = CType(Session(Me.htxtSessionIdQuery.Value), Xydc.Platform.Common.Data.QueryData)
                    Catch ex As Exception
                        objQueryData = Nothing
                    End Try
                    Xydc.Platform.Common.Data.QueryData.SafeRelease(objQueryData)
                    Session.Remove(Me.htxtSessionIdQuery.Value)
                End If
            Catch ex As Exception
            End Try

            Exit Sub

        End Sub

        '----------------------------------------------------------------
        ' 获取模块搜索条件(默认表前缀a.)
        '     strErrMsg      ：返回错误信息
        '     strQuery       ：返回的搜索条件
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function getQueryString_Compute( _
            ByRef strErrMsg As String, _
            ByRef strQuery As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim strTxtTemp As String
            Dim strSearchFied As String

            getQueryString_Compute = False
            strQuery = ""

            Try
                '按“成交日期”搜索
                Dim strFixtureDate As String
                Dim dateMin As System.DateTime
                Dim dateMax As System.DateTime

                If Me.txtStartDate.Text.Length > 0 Then Me.txtStartDate.Text = Me.txtStartDate.Text.Trim()
                If Me.txtEndDate.Text.Length > 0 Then Me.txtEndDate.Text = Me.txtEndDate.Text.Trim()
                If Me.txtStartDate.Text <> "" And Me.txtEndDate.Text <> "" Then
                    Try
                        dateMin = CType(Me.txtStartDate.Text, System.DateTime)
                    Catch ex As Exception
                        strErrMsg = "错误：无效的起始日期！"
                        GoTo errProc
                    End Try
                    Try
                        dateMax = CType(Me.txtEndDate.Text, System.DateTime)
                    Catch ex As Exception
                        strErrMsg = "错误：无效的结束日期！"
                        GoTo errProc
                    End Try
                    If dateMin > dateMax Then
                        Me.txtStartDate.Text = Format(dateMax, "yyyy-MM-dd")
                        Me.txtEndDate.Text = Format(dateMin, "yyyy-MM-dd")
                    Else
                        Me.txtStartDate.Text = Format(dateMin, "yyyy-MM-dd")
                        Me.txtEndDate.Text = Format(dateMax, "yyyy-MM-dd")
                    End If
                ElseIf Me.txtStartDate.Text = "" Then
                    strErrMsg = "错误：无效的起始日期！"
                    GoTo errProc
                ElseIf Me.txtEndDate.Text <> "" Then
                    strErrMsg = "错误：无效的结束日期！"
                    GoTo errProc
                End If

                '按“行政区域”搜索
                strSearchFied = ""
                strTxtTemp = ""
                strSearchFied = "a.行政区域"
                strTxtTemp = Me.ddlRegion.SelectedValue
                If strTxtTemp <> "" And strTxtTemp <> "0" Then
                    strTxtTemp = objPulicParameters.getNewSearchString(strTxtTemp)
                    If strQuery = "" Then
                        strQuery = strSearchFied + " like '" + strTxtTemp + "%'"
                    Else
                        strQuery = strQuery + " and " + strSearchFied + " like '" + strTxtTemp + "%'"
                    End If
                End If

                '按楼盘名称
                strSearchFied = ""
                strTxtTemp = ""
                strSearchFied = "a.楼盘名称"
                strTxtTemp = Me.txtBuildingName.Text
                If strTxtTemp <> "" And strTxtTemp <> "0" Then
                    strTxtTemp = objPulicParameters.getNewSearchString(strTxtTemp)
                    If strQuery = "" Then
                        strQuery = strSearchFied + " like '" + strTxtTemp + "%'"
                    Else
                        strQuery = strQuery + " and " + strSearchFied + " like '" + strTxtTemp + "%'"
                    End If
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            getQueryString_Compute = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取grdCompute要显示的数据信息
        '     strErrMsg      ：返回错误信息
        '     strWhere       ：搜索字符串
        '     blnControl     ：特殊权限控制
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function getModuleData_Compute( _
            ByRef strErrMsg As String, _
            ByVal strWhere As String, _
            ByVal blnControl As Boolean) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.SunshineData.TABLE_Sunshine_V_WeekRegion_Statistics
            Dim objsystemSunshine As New Xydc.Platform.BusinessFacade.systemSunshine

            getModuleData_Compute = False

            Try
                '备份Sort字符串
                Dim strSort As String = ""
                strSort = Me.htxtComputeSort.Value
                If strSort.Length > 0 Then strSort = strSort.Trim

                '释放资源
                Xydc.Platform.Common.Data.SunshineData.SafeRelease(Me.m_objDataSet_Compute)

                '重新检索数据
                Dim strType As String
                Dim intTop As Integer = 0
                Dim strOrderBy As String = ""
                Dim objParameter As New System.Collections.Specialized.NameValueCollection
                Dim strStartDate As String
                Dim datStartDate As System.DateTime
                Dim strEndDate As String
                Dim datEndDate As System.DateTime

                '计算日期实际是从上周六到本周五
                '由于截数日期是然后一天的
                '等于用本周六的截接下来的数-上周六截下来的数
                '网页显示的周日期为上周六到本周五 因此本周五天数+1

                'datStartDate = CDate(Me.txtStartDate.Text)
                'datStartDate = datStartDate.AddDays(-1)
                'strStartDate = CStr(datStartDate)

                'datEndDate = CDate(Me.txtEndDate.Text)
                'datEndDate = datEndDate.AddDays(1)
                'strEndDate = CStr(datEndDate)

                objParameter.Clear()
                'objParameter.Add("StartDate", strStartDate)
                'objParameter.Add("EndDate", Me.txtEndDate.Text)
                objParameter.Add("StartDate", Me.txtStartDate.Text)
                'objParameter.Add("EndDate", strEndDate)
                objParameter.Add("EndDate", Me.txtEndDate.Text)
                objParameter.Add("Type", Me.ddlHouseType.SelectedValue)

                If Me.ddlHouseType.SelectedValue = "0" Then
                    strErrMsg = "错误：请选择一种物业类型！"
                    GoTo errProc
                End If
                'If objsystemSunshine.getDataSet_RegionCompute(strErrMsg, MyBase.UserId, MyBase.UserPassword, strWhere, objParameter, Me.m_objDataSet_Compute) = False Then
                '    GoTo errProc
                'End If

                If objsystemSunshine.getDataSet_RegionBuildingCompute_v3(strErrMsg, MyBase.UserId, MyBase.UserPassword, strWhere, objParameter, Me.m_objDataSet_Compute) = False Then
                    GoTo errProc
                End If

                '恢复Sort字符串
                'With Me.m_objDataSet_Compute.Tables(strTable)
                '    .DefaultView.Sort = strSort
                'End With

                '缓存参数
                With Me.m_objDataSet_Compute.Tables(strTable)
                    Me.htxtComputeRows.Value = .DefaultView.Count.ToString()
                    Me.m_intRows_Compute = .DefaultView.Count
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.BusinessFacade.systemSunshine.SafeRelease(objsystemSunshine)

            getModuleData_Compute = True
            Exit Function
errProc:
            Xydc.Platform.BusinessFacade.systemSunshine.SafeRelease(objsystemSunshine)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据屏幕搜索条件搜索grdCompute数据
        '     strErrMsg      ：返回错误信息
        '     blnControl     ：特殊权限控制
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function searchModuleData_Compute( _
            ByRef strErrMsg As String, _
            ByVal blnControl As Boolean) As Boolean

            searchModuleData_Compute = False

            Try
                '获取搜索字符串
                Dim strQuery As String
                If Me.getQueryString_Compute(strErrMsg, strQuery) = False Then
                    GoTo errProc
                End If

                '搜索数据
                If Me.getModuleData_Compute(strErrMsg, strQuery, blnControl) = False Then
                    GoTo errProc
                End If

                '记录搜索字符串
                Me.m_strQuery_Compute = strQuery
                Me.htxtComputeQuery.Value = Me.m_strQuery_Compute

                Me.htxtStartDate.Value = Me.txtStartDate.Text
                Me.htxtEndDate.Value = Me.txtEndDate.Text
                Me.htxtType.Value = Me.ddlHouseType.SelectedValue

                '记录日志
                With New Xydc.Platform.DataAccess.dacSystemOperate
                    If .doSaveOperateLogData(strErrMsg, MyBase.UserId, MyBase.UserPassword, Request.UserHostAddress, Request.UserHostName, _
                        Xydc.Platform.Common.Data.LogData.OperateType_select, Xydc.Platform.Common.Data.SunshineData.TABLE_Sunshine_V_WeekRegion_Statistics, strQuery) = False Then
                        GoTo errProc
                    End If
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            searchModuleData_Compute = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示grdCompute的数据
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showDataGridInfo_Compute( _
            ByRef strErrMsg As String) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.SunshineData.TABLE_Sunshine_V_WeekRegion_Statistics
            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess

            showDataGridInfo_Compute = False

            '获取系统保存的网格排序数据
            Dim intSortColumnIndex As Integer = -1
            intSortColumnIndex = objPulicParameters.getObjectValue(Me.htxtComputeSortColumnIndex.Value, -1)
            Dim objSortType As Xydc.Platform.Common.Utilities.PulicParameters.enumSortType
            Try
                objSortType = CType(Me.htxtComputeSortType.Value, Xydc.Platform.Common.Utilities.PulicParameters.enumSortType)
            Catch ex As Exception
                objSortType = Xydc.Platform.Common.Utilities.PulicParameters.enumSortType.None
            End Try

            '网格显示处理
            Try
                '在获取数据时已经恢复了RowFilter、Sort的现场
                '设置数据源
                If Me.m_objDataSet_Compute Is Nothing Then
                    Me.grdCompute.DataSource = Nothing
                Else
                    With Me.m_objDataSet_Compute.Tables(strTable)
                        Me.grdCompute.DataSource = .DefaultView
                    End With
                End If

                '调整网格参数
                With Me.m_objDataSet_Compute.Tables(strTable)
                    If objDataGridProcess.onBeforeDataGridBind(strErrMsg, Me.grdCompute, .DefaultView.Count) = False Then
                        GoTo errProc
                    End If
                End With

                '恢复列标题中的排序信息
                If intSortColumnIndex >= 0 Then
                    objDataGridProcess.doClearSortCharInDataGridHead(Me.grdCompute)
                    With Me.grdCompute.Columns(intSortColumnIndex)
                        .HeaderText = objDataGridProcess.getColumnSortHeadString(.HeaderText, objSortType)
                    End With
                End If

                '绑定数据
                Me.grdCompute.DataBind()

                '----------------------------------------------------------------
                '因为这些信息是非绑定的，所以下面的操作必须等绑定完成后执行！！！
                '一旦在后续处理中执行了DataBind，则信息会丢失！！！
                '----------------------------------------------------------------
                ''恢复网格中的CheckBox状态
                'If objDataGridProcess.doRestoreDataGridCheckBoxStatus(strErrMsg, Me.grdCompute, Request, 0, Me.m_cstrCheckBoxIdInDataGrid_Compute) = False Then
                '    GoTo errProc
                'End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)

            showDataGridInfo_Compute = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示整个模块的信息
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showModuleData( _
            ByRef strErrMsg As String) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.SunshineData.TABLE_Sunshine_V_WeekRegion_Statistics
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objControlProcess As New Xydc.Platform.web.ControlProcess

            showModuleData = False

            Try
                '显示网格信息
                If Me.showDataGridInfo_Compute(strErrMsg) = False Then
                    GoTo errProc
                End If

                '显示与网格紧密相关的操作或信息提示
                With Me.m_objDataSet_Compute.Tables(strTable).DefaultView
                    '显示网格位置信息
                    Me.lblBMRYGridLocInfo.Text = objDataGridProcess.getDataGridLocation(Me.grdCompute, .Count)
                    '显示页面浏览功能
                    Me.lnkCZBMRYMoveFirst.Enabled = objDataGridProcess.canDoMoveFirstPage(Me.grdCompute, .Count)
                    Me.lnkCZBMRYMoveLast.Enabled = objDataGridProcess.canDoMoveLastPage(Me.grdCompute, .Count)
                    Me.lnkCZBMRYMovePrev.Enabled = objDataGridProcess.canDoMovePreviousPage(Me.grdCompute, .Count)
                    Me.lnkCZBMRYMoveNext.Enabled = objDataGridProcess.canDoMoveNextPage(Me.grdCompute, .Count)
                    '显示相关操作
                    Dim blnEnabled As Boolean
                    If .Count < 1 Then
                        blnEnabled = False
                    Else
                        blnEnabled = True
                    End If

                    Me.lnkCZBMRYGotoPage.Enabled = blnEnabled
                    Me.lnkCZBMRYSetPageSize.Enabled = blnEnabled
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.ControlProcess.SafeRelease(objControlProcess)

            showModuleData = True
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
            Dim datStartDate As System.DateTime

            initializeControls = False

            '仅在第一次调用页面时执行
            If Me.IsPostBack = False Then
                Try
                    '显示Pannel(不论是否回调，始终显示panelMain)
                    Me.panelMain.Visible = True
                    Me.panelError.Visible = Not Me.panelMain.Visible

                    '执行键转译(不论是否是“回发”)
                    '********************************************************
                    objControlProcess.doTranslateKey(Me.txtEndDate)
                    objControlProcess.doTranslateKey(Me.txtStartDate)
                    '********************************************************

                    '设置默认时间，开始日期为每月1日，结束日期为数据库最新记录日期
                    Me.txtStartDate.Text = DateTime.Now.AddDays(-DateTime.Now.Day + 1).ToShortDateString()
                    'Me.txtEndDate.Text = DateTime.Now.AddMonths(1).AddDays(-DateTime.Now.Day).ToShortDateString()

                    With New Xydc.Platform.BusinessFacade.systemSunshine_x2
                        If .getDayHouseDataTime(strErrMsg, MyBase.UserId, MyBase.UserPassword, dayHouseDataTime) = False Then
                            GoTo errProc
                        End If
                    End With
                    Me.txtEndDate.Text = dayHouseDataTime.ToShortDateString

                    '********************************************************

                    If Me.m_blnSaveScence = False Then
                        If Me.searchModuleData_Compute(strErrMsg, Me.m_blnQxControl) = False Then
                            GoTo errProc
                        End If
                    Else
                        If Me.getModuleData_Compute(strErrMsg, Me.m_strQuery_Compute, Me.m_blnQxControl) = False Then
                            GoTo errProc
                        End If
                    End If
                    If Me.showModuleData(strErrMsg) = False Then
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



        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
            If blnDo = False Then
                GoTo normExit
            End If

            '获取接口参数
            If Me.getInterfaceParameters(strErrMsg) = False Then
                GoTo errProc
            End If

            '控件初始化
            If Me.initializeControls(strErrMsg) = False Then
                GoTo errProc
            End If

            '访问日志
            If Me.IsPostBack = False Then
                If Me.m_blnSaveScence = False Then
                    With New Xydc.Platform.DataAccess.dacSystemOperate
                        If .doSaveVisitLogData(strErrMsg, MyBase.UserId, MyBase.UserPassword, Request.UserHostAddress, Request.UserHostName, "sunshineData_RegionInfo_compute_v3.aspx", "项目信息区域数据查询") = False Then
                            GoTo errProc
                        End If
                    End With
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

        Private Sub doClose(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                Dim strSessionId As String
                Dim strUrl As String
                If Me.m_blnInterface = True Then
                    '设置返回参数
                    '返回到调用模块，并附加返回参数
                    '要返回的SessionId
                    strSessionId = Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.ISessionId)
                    'SessionId附加到返回的Url
                    strUrl = Me.m_objInterface.getReturnUrl(Server, Xydc.Platform.Common.Utilities.PulicParameters.OSessionId, strSessionId)
                Else
                    strUrl = Xydc.Platform.Common.jsoaConfiguration.GeneralReturnUrl
                End If
                '释放模块资源
                Me.releaseModuleParameters()
                Me.releaseInterfaceParameters()
                '返回
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

        Private Sub doPrint(ByVal strControlId As String)

            Dim strTable As String = Xydc.Platform.Common.Data.SunshineData.TABLE_Sunshine_V_WeekRegion_Statistics
            Dim objsystemSunshine As New Xydc.Platform.BusinessFacade.systemSunshine
            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String = ""

            Try

                '获取打印数据   
                Dim objParameter As New System.Collections.Specialized.NameValueCollection
                Dim strStartDate As String
                Dim datStartDate As System.DateTime
                Dim strEndDate As String
                Dim datEndDate As System.DateTime

                '计算日期是从上周六到本周五
                datEndDate = CDate(Me.htxtEndDate.Value)
                'datEndDate = datEndDate.AddDays(1)
                strEndDate = CStr(datEndDate)

                'datStartDate = CDate(Me.htxtStartDate.Value)
                'datStartDate = datStartDate.AddDays(-1)
                'strStartDate = CStr(datStartDate)

                objParameter.Clear()
                'objParameter.Add("StartDate", strStartDate)
                'objParameter.Add("EndDate", Me.htxtEndDate.Value)
                objParameter.Add("StartDate", Me.htxtStartDate.Value)
                objParameter.Add("EndDate", strEndDate)
                objParameter.Add("Type", Me.htxtType.Value)



                'If objsystemSunshine.getDataSet_BuildingCompute(strErrMsg, MyBase.UserId, MyBase.UserPassword, Me.m_strQuery_Compute, objParameter, Me.m_objDataSet_Compute) = False Then
                '    GoTo errProc
                'End If

                If objsystemSunshine.getDataSet_RegionBuildingCompute_v3(strErrMsg, MyBase.UserId, MyBase.UserPassword, Me.m_strQuery_Compute, objParameter, Me.m_objDataSet_Compute) = False Then
                    GoTo errProc
                End If


                If Me.m_objDataSet_Compute.Tables(strTable) Is Nothing Then
                    strErrMsg = "错误：还未获取数据！"
                    GoTo errProc
                End If
                With Me.m_objDataSet_Compute.Tables(strTable)
                    If .Rows.Count < 1 Then
                        strErrMsg = "错误：没有数据！"
                        GoTo errProc
                    End If
                End With

                '计算宏值
                Dim strSep As String = objPulicParameters.CharSeparate
                Dim objDate As System.DateTime
                Dim strMacroValue As String
                Dim strMacroName As String

                strMacroValue = Me.htxtStartDate.Value + "-" + Me.htxtEndDate.Value
                Select Case Me.htxtType.Value
                    Case "0"
                        strMacroValue = strMacroValue + "[全部]"
                    Case "1"
                        strMacroValue = strMacroValue + "[商业]"
                    Case "2"
                        strMacroValue = strMacroValue + "[住宅]"
                    Case "3"
                        strMacroValue = strMacroValue + "[办公]"
                End Select
                strMacroName = "$Macro$DayInterval$"

                '准备Excel文件
                Dim strDesExcelPath As String = Request.ApplicationPath + Me.m_cstrPrintCacheRelativePathToAppRoot
                Dim strSrcExcelSpec As String = Request.ApplicationPath + Me.m_cstrExcelMBRelativePathToAppRoot + "区域信息表.xls"
                Dim strDesExcelFile As String = ""
                Dim strDesExcelSpec As String = ""
                strDesExcelPath = Server.MapPath(strDesExcelPath)
                strSrcExcelSpec = Server.MapPath(strSrcExcelSpec)
                If objBaseLocalFile.doCopyToTempFile(strErrMsg, strSrcExcelSpec, strDesExcelPath, strDesExcelFile) = False Then
                    GoTo errProc
                End If
                strDesExcelSpec = objBaseLocalFile.doMakePath(strDesExcelPath, strDesExcelFile)


                '输出数据
                If objsystemSunshine.doExportToExcel(strErrMsg, m_objDataSet_Compute, strDesExcelSpec, strMacroName, strMacroValue) = False Then
                    GoTo errProc
                End If

                'If objsystemSunshine.doExportToExcel(strErrMsg, m_objDataSet_Compute, strDesExcelSpec) = False Then
                '    GoTo errProc
                'End If

                '打开临时Excel文件
                Dim strUrl As String = Request.ApplicationPath + Me.m_cstrPrintCacheRelativePathToAppRoot + strDesExcelFile
                objMessageProcess.doOpenUrl(Me.popMessageObject, strUrl, "_blank", "titlebar=yes,menubar=yes,resizable=yes,scrollbars=yes,status=yes")
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.BusinessFacade.systemSunshine.SafeRelease(objsystemSunshine)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub
errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.BusinessFacade.systemSunshine.SafeRelease(objsystemSunshine)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub
        End Sub


        Private Sub doMoveFirst_BMRY(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_Compute(strErrMsg, Me.m_strQuery_Compute, True) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(0, Me.grdCompute.PageCount)
                Me.grdCompute.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData(strErrMsg) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Exit Sub

        End Sub

        Private Sub doMoveLast_BMRY(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_Compute(strErrMsg, Me.m_strQuery_Compute, True) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(Me.grdCompute.PageCount - 1, Me.grdCompute.PageCount)
                Me.grdCompute.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData(strErrMsg) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Exit Sub

        End Sub

        Private Sub doMoveNext_BMRY(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_Compute(strErrMsg, Me.m_strQuery_Compute, True) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(Me.grdCompute.CurrentPageIndex + 1, Me.grdCompute.PageCount)
                Me.grdCompute.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData(strErrMsg) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Exit Sub

        End Sub

        Private Sub doMovePrevious_BMRY(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_Compute(strErrMsg, Me.m_strQuery_Compute, True) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(Me.grdCompute.CurrentPageIndex - 1, Me.grdCompute.PageCount)
                Me.grdCompute.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData(strErrMsg) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Exit Sub

        End Sub

        Private Sub doGotoPage_BMRY(ByVal strControlId As String)

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            '获取新的页大小
            Dim intPageIndex As Integer
            intPageIndex = objPulicParameters.getObjectValue(Me.txtBMRYPageIndex.Text, 0)
            If intPageIndex <= 0 Then
                intPageIndex = 0
            Else
                intPageIndex -= 1
            End If

            Try
                '获取数据
                If Me.getModuleData_Compute(strErrMsg, Me.m_strQuery_Compute, True) = False Then
                    GoTo errProc
                End If

                '设置新的页
                Me.grdCompute.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData(strErrMsg) = False Then
                    GoTo errProc
                End If

                '信息同步
                Me.txtBMRYPageIndex.Text = (Me.grdCompute.CurrentPageIndex + 1).ToString()

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

        Private Sub doSetPageSize_BMRY(ByVal strControlId As String)

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            '获取新的页大小
            Dim intPageSize As Integer
            intPageSize = objPulicParameters.getObjectValue(Me.txtBMRYPageSize.Text, 100)
            If intPageSize <= 0 Then intPageSize = 100

            Try
                '获取数据
                If Me.getModuleData_Compute(strErrMsg, Me.m_strQuery_Compute, True) = False Then
                    GoTo errProc
                End If

                '设置新的页大小
                Me.grdCompute.PageSize = intPageSize

                '刷新网格显示
                If Me.showModuleData(strErrMsg) = False Then
                    GoTo errProc
                End If

                '信息同步
                Me.txtBMRYPageSize.Text = (Me.grdCompute.PageSize).ToString()

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

        Private Sub lnkCZBMRYMoveFirst_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZBMRYMoveFirst.Click
            Me.doMoveFirst_BMRY("lnkCZBMRYMoveFirst")
        End Sub

        Private Sub lnkCZBMRYMoveLast_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZBMRYMoveLast.Click
            Me.doMoveLast_BMRY("lnkCZBMRYMoveLast")
        End Sub

        Private Sub lnkCZBMRYMoveNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZBMRYMoveNext.Click
            Me.doMoveNext_BMRY("lnkCZBMRYMoveNext")
        End Sub

        Private Sub lnkCZBMRYMovePrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZBMRYMovePrev.Click
            Me.doMovePrevious_BMRY("lnkCZBMRYMovePrev")
        End Sub

        Private Sub lnkCZBMRYGotoPage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZBMRYGotoPage.Click
            Me.doGotoPage_BMRY("lnkCZBMRYGotoPage")
        End Sub

        Private Sub lnkCZBMRYSetPageSize_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZBMRYSetPageSize.Click
            Me.doSetPageSize_BMRY("lnkCZBMRYSetPageSize")
        End Sub

        Private Sub lnkRq_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkRq.Click
            Dim date1 As System.DateTime
            Dim date2 As System.DateTime
            Dim dateNow As System.DateTime
            Dim strTemp As String
            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim strErrmsg As String
            dateNow = CType(Me.txtRQ.Text, System.DateTime)
            strTemp = ""
            If objPulicParameters.getWeekStartAndEndDay(strErrmsg, dateNow, date1, date2, "") = True Then
                Me.txtStartDate.Text = date1.ToString("yyyy-MM-dd")
                Me.txtEndDate.Text = date2.ToString("yyyy-MM-dd")
            End If
        End Sub

    End Class

End Namespace