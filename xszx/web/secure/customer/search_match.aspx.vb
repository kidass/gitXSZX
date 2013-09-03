Imports System.Web.Security

Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform
    ' 类名    ：search_match
    ' 
    ' 调用性质：
    '     I/O
    '
    ' 功能描述： 
    '   　“阳光家缘房产项目信息数据分析”处理模块
    '----------------------------------------------------------------

    Partial Public Class search_match
        Inherits Xydc.Platform.web.PageBase

        '----------------------------------------------------------------
        '模块私用参数
        '----------------------------------------------------------------
        '本模块相对image的相对路径
        Private m_cstrRelativePathToImage As String = "../../"

        '----------------------------------------------------------------
        '模块授权参数
        '----------------------------------------------------------------
        Private m_cstrPrevilegeParamPrefix As String = "customer_deep_previlege_param"
        Private m_blnPrevilegeParams(4) As Boolean

        '----------------------------------------------------------------
        '模块现场保留参数，恢复完成后立即释放session资源
        '----------------------------------------------------------------
        Private m_blnSaveScence As Boolean

        '----------------------------------------------------------------
        '模块接口参数
        '----------------------------------------------------------------
        Private m_objInterface As Xydc.Platform.BusinessFacade.IDeepData_monthCompute
        Private m_blnInterface As Boolean

        '----------------------------------------------------------------
        '与数据网格grdObjects相关的参数
        '----------------------------------------------------------------
        '网格中模板列中的控件ID
        Private Const m_cstrCheckBoxIdInDataGrid As String = "chkObjects"
        '包含网格的DIV对象ID
        Private Const m_cstrDataGridInDIV As String = "divObjects"
        Private Const m_cstrDataGridInDIV_HOUSEMATCH As String = "divHOUSEMATCH"
        '网格要锁定的列数
        Private m_intFixedColumns As Integer
        Private m_intFixedColumns_HOUSEMATCH As Integer

        '----------------------------------------------------------------
        '当前处理的数据集
        '----------------------------------------------------------------
        Private m_objDeepDataSet As Xydc.Platform.Common.Data.DeepData
        Private m_objDeepDataSet_HOUSEMATCH As Xydc.Platform.Common.Data.DeepData
        Private m_strQuery As String '记录m_objDeepDataSet的搜索串
        Private m_intRows As Integer '记录m_objDeepDataSet的DefaultView记录数

        '----------------------------------------------------------------
        '其他模块私用参数
        '----------------------------------------------------------------
        '详细编辑模式
        Private m_objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType
        '是否为编辑状态
        Private m_blnEditMode As Boolean
        '进入编辑模式前记录的页位置
        Private m_intCurrentPageIndex As Integer
        '进入编辑模式前记录的行位置
        Private m_intCurrentSelectIndex As Integer







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
                'Me.m_blnPrevilegeParams(0) = True
                'Me.m_blnPrevilegeParams(1) = True
                'Me.m_blnPrevilegeParams(2) = True
                'Me.m_blnPrevilegeParams(3) = True
                'blnContinueExecute = True
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

            Try

            Catch ex As Exception

            End Try

            Exit Sub

        End Sub

        '----------------------------------------------------------------
        ' 保存模块现场信息并返回相应的SessionId
        '----------------------------------------------------------------
        Private Function saveModuleInformation() As String

            Try

            Catch ex As Exception

            End Try

            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 从调用模块中获取数据
        '----------------------------------------------------------------
        Private Function getDataFromCallModule(ByRef strErrMsg As String) As Boolean

            Try
                If Me.IsPostBack = True Then
                    Exit Try
                End If

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
            strErrMsg = ""

            Try
                '从QueryString中解析接口参数(不论是否回发)
                Dim objTemp As Object
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
                Me.m_blnSaveScence = False
                If Me.IsPostBack = False Then
                    Dim strSessionId As String
                    strSessionId = objPulicParameters.getObjectValue(Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.MSessionId), "")

                    '恢复现场参数后释放该资源
                    Me.restoreModuleInformation(strSessionId)

                    '处理调用模块返回后的信息并同时释放相应资源
                    If Me.getDataFromCallModule(strErrMsg) = False Then
                        GoTo errProc
                    End If
                End If

                With objPulicParameters
                    '是否处于编辑状态
                    Me.m_blnEditMode = .getObjectValue(Me.htxtEditMode.Value, False)

                    '进入编辑模式前记录的页位置
                    Me.m_intCurrentPageIndex = .getObjectValue(Me.htxtCurrentPage.Value, 0)

                    '进入编辑模式前记录的行位置
                    Me.m_intCurrentSelectIndex = .getObjectValue(Me.htxtCurrentRow.Value, -1)

                    '当前编辑模式
                    Dim intEditType As Integer
                    intEditType = .getObjectValue(Me.htxtEditType.Value, 0)
                    Try
                        Me.m_objenumEditType = CType(intEditType, Xydc.Platform.Common.Utilities.PulicParameters.enumEditType)
                    Catch ex As Exception
                        Me.m_objenumEditType = Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eSelect
                    End Try

                    'm_objDeepDataSet的搜索串
                    Me.m_strQuery = Me.htxtQuery.Value

                    '记录m_objDeepDataSet的DefaultView记录数
                    Me.m_intRows = .getObjectValue(Me.htxtRows.Value, 0)

                    Me.m_intFixedColumns = .getObjectValue(Me.htxtOBJECTSFixed.Value, 0)
                End With

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
        Private Function getQueryString( _
            ByRef strErrMsg As String, _
            ByRef strQuery As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim strTxtTemp As String
            Dim strSearchFied As String

            getQueryString = False
            strQuery = ""

            Try
                '按“成交日期”搜索
                Dim strFixtureDate As String
                Dim dateMin As System.DateTime
                Dim dateMax As System.DateTime

                strFixtureDate = "a." + Xydc.Platform.Common.Data.DeepData.FIELD_House_B_SalesMessage_FixtureDate
                If Me.txtStartDate.Text.Length > 0 Then Me.txtStartDate.Text = Me.txtStartDate.Text.Trim()
                If Me.txtEndDate.Text.Length > 0 Then Me.txtEndDate.Text = Me.txtEndDate.Text.Trim()
                If Me.txtStartDate.Text <> "" And Me.txtEndDate.Text <> "" Then
                    Try
                        dateMin = CType(Me.txtStartDate.Text, System.DateTime)
                    Catch ex As Exception
                        strErrMsg = "错误：无效的日期！"
                        GoTo errProc
                    End Try
                    Try
                        dateMax = CType(Me.txtEndDate.Text, System.DateTime)
                    Catch ex As Exception
                        strErrMsg = "错误：无效的日期！"
                        GoTo errProc
                    End Try
                    If dateMin > dateMax Then
                        Me.txtStartDate.Text = Format(dateMax, "yyyy-MM-dd")
                        Me.txtEndDate.Text = Format(dateMin, "yyyy-MM-dd")
                    Else
                        Me.txtStartDate.Text = Format(dateMin, "yyyy-MM-dd")
                        Me.txtEndDate.Text = Format(dateMax, "yyyy-MM-dd")
                    End If
                    If strQuery = "" Then
                        strQuery = strFixtureDate + " between '" + Me.txtStartDate.Text + "' and '" + Me.txtEndDate.Text + "'"
                    Else
                        strQuery = strQuery + " and " + strFixtureDate + " between '" + Me.txtStartDate.Text + "' and '" + Me.txtEndDate.Text + "'"
                    End If
                ElseIf Me.txtStartDate.Text <> "" Then
                    Try
                        dateMin = CType(Me.txtStartDate.Text, System.DateTime)
                    Catch ex As Exception
                        strErrMsg = "错误：无效的日期！"
                        GoTo errProc
                    End Try
                    Me.txtStartDate.Text = Format(dateMin, "yyyy-MM-dd")
                    If strQuery = "" Then
                        strQuery = strFixtureDate + " >= '" + Me.txtStartDate.Text + "'"
                    Else
                        strQuery = strQuery + " and " + strFixtureDate + " >= '" + Me.txtStartDate.Text + "'"
                    End If
                ElseIf Me.txtEndDate.Text <> "" Then
                    Try
                        dateMax = CType(Me.txtEndDate.Text, System.DateTime)
                    Catch ex As Exception
                        strErrMsg = "错误：无效的日期！"
                        GoTo errProc
                    End Try
                    Me.txtEndDate.Text = Format(dateMax, "yyyy-MM-dd")
                    If strQuery = "" Then
                        strQuery = strFixtureDate + " <= '" + Me.txtEndDate.Text + "'"
                    Else
                        strQuery = strQuery + " and " + strFixtureDate + " <= '" + Me.txtEndDate.Text + "'"
                    End If
                Else
                End If

                '按“行政区域”搜索
                strSearchFied = ""
                strTxtTemp = ""
                strSearchFied = "a." + Xydc.Platform.Common.Data.DeepData.FIELD_House_B_SalesMessage_Region
                strTxtTemp = Me.ddlRegion.SelectedValue
                If strTxtTemp <> "" And strTxtTemp <> "0" Then
                    strTxtTemp = objPulicParameters.getNewSearchString(strTxtTemp)
                    If strQuery = "" Then
                        strQuery = strSearchFied + " like '" + strTxtTemp + "%'"
                    Else
                        strQuery = strQuery + " and " + strSearchFied + " like '" + strTxtTemp + "%'"
                    End If
                End If


                '按“楼盘地址”搜索
                strSearchFied = ""
                strTxtTemp = ""
                strSearchFied = "a." + Xydc.Platform.Common.Data.DeepData.FIELD_House_B_SalesMessage_HouseAddress
                strTxtTemp = Me.txtMailAddress.Text.Trim
                If strTxtTemp <> "" Then
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

            getQueryString = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取模块要显示的数据信息
        '     strErrMsg      ：返回错误信息
        '     strWhere       ：搜索字符串
        '     blnEditMode    ：当前编辑状态
        '     objenumEditType：详细操作模式
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function getModuleData( _
            ByRef strErrMsg As String, _
            ByVal strWhere As String, _
            ByVal blnEditMode As Boolean, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.DeepData.TABLE_House_B_SalesMessageCustomer

            getModuleData = False

            Try
                '备份Sort字符串
                Dim strSort As String
                strSort = Me.htxtSort.Value
                If strSort.Length > 0 Then strSort = strSort.Trim

                '释放资源
                If Not (Me.m_objDeepDataSet Is Nothing) Then
                    Me.m_objDeepDataSet.Dispose()
                    Me.m_objDeepDataSet = Nothing
                End If

                '重新检索数据
                With New Xydc.Platform.BusinessFacade.systemDeepdata
                    If .getDataSet_Detail_Customer(strErrMsg, MyBase.UserId, MyBase.UserPassword, strWhere, Me.m_objDeepDataSet) = False Then
                        GoTo errProc
                    End If
                End With

                '恢复Sort字符串
                With Me.m_objDeepDataSet.Tables(strTable)
                    .DefaultView.Sort = strSort
                End With

                If blnEditMode = False Then '查看模式
                    With Me.m_objDeepDataSet.Tables(strTable)
                        .DefaultView.AllowNew = False
                    End With
                Else '编辑模式
                    Select Case objenumEditType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                            '增加1条空记录
                            With Me.m_objDeepDataSet.Tables(strTable)
                                .DefaultView.AllowNew = True
                                .DefaultView.AddNew()
                            End With

                        Case Else
                            With Me.m_objDeepDataSet.Tables(strTable)
                                .DefaultView.AllowNew = False
                            End With
                    End Select
                End If

                '缓存参数
                With Me.m_objDeepDataSet.Tables(strTable)
                    Me.htxtRows.Value = .DefaultView.Count.ToString()
                    Me.m_intRows = .DefaultView.Count
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
        ' 获取模块要显示的数据信息
        '     strErrMsg      ：返回错误信息
        '     strWhere       ：搜索字符串
        '     blnEditMode    ：当前编辑状态
        '     objenumEditType：详细操作模式
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function getModuleData_HOUSEMATCH( _
            ByRef strErrMsg As String, _
            ByVal strWhere As String, _
            ByVal blnEditMode As Boolean, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.DeepData.TABLE_Customer_B_Search_Gather

            getModuleData_HOUSEMATCH = False

            Try
                '备份Sort字符串
                Dim strSort As String
                strSort = Me.htxtSort.Value
                If strSort.Length > 0 Then strSort = strSort.Trim

                '释放资源
                If Not (Me.m_objDeepDataSet_HOUSEMATCH Is Nothing) Then
                    Me.m_objDeepDataSet_HOUSEMATCH.Dispose()
                    Me.m_objDeepDataSet_HOUSEMATCH = Nothing
                End If

                '重新检索数据
                With New Xydc.Platform.DataAccess.dacDeepdata
                    If .getDataSet_SearchContent(strErrMsg, MyBase.UserId, MyBase.UserPassword, strWhere, Me.m_objDeepDataSet_HOUSEMATCH) = False Then
                        GoTo errProc
                    End If
                End With

                '恢复Sort字符串
                With Me.m_objDeepDataSet_HOUSEMATCH.Tables(strTable)
                    .DefaultView.Sort = strSort
                End With


                If blnEditMode = False Then '查看模式
                    With Me.m_objDeepDataSet_HOUSEMATCH.Tables(strTable)
                        .DefaultView.AllowNew = False
                    End With
                Else '编辑模式
                    Select Case objenumEditType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                            '增加1条空记录
                            With Me.m_objDeepDataSet_HOUSEMATCH.Tables(strTable)
                                .DefaultView.AllowNew = True
                                .DefaultView.AddNew()
                            End With

                        Case Else
                            With Me.m_objDeepDataSet_HOUSEMATCH.Tables(strTable)
                                .DefaultView.AllowNew = False
                            End With
                    End Select
                End If

                '缓存参数
                With Me.m_objDeepDataSet_HOUSEMATCH.Tables(strTable)
                    Me.htxtRows.Value = .DefaultView.Count.ToString()
                    Me.m_intRows = .DefaultView.Count
                End With


            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getModuleData_HOUSEMATCH = True
            Exit Function

errProc:
            Exit Function

        End Function


        '----------------------------------------------------------------
        ' 根据屏幕搜索条件搜索数据
        '     strErrMsg      ：返回错误信息
        '     blnEditMode    ：当前编辑状态
        '     objenumEditType：详细操作模式
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function searchModuleData( _
            ByRef strErrMsg As String, _
            ByVal blnEditMode As Boolean, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            searchModuleData = False

            Try
                '获取搜索字符串
                Dim strQuery As String
                If Me.getQueryString(strErrMsg, strQuery) = False Then
                    GoTo errProc
                End If

                '搜索数据
                If Me.getModuleData(strErrMsg, strQuery, blnEditMode, objenumEditType) = False Then
                    GoTo errProc
                End If

                '记录搜索字符串
                Me.m_strQuery = strQuery
                Me.htxtQuery.Value = Me.m_strQuery

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            searchModuleData = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示DataGrid的数据
        '     strErrMsg      ：返回错误信息
        '     blnEditMode    ：当前编辑状态
        '     objenumEditType：详细操作模式
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showDataGridInfo( _
            ByRef strErrMsg As String, _
            ByVal blnEditMode As Boolean, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.DeepData.TABLE_House_B_SalesMessageCustomer
            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess

            showDataGridInfo = False

            '获取系统保存的网格排序数据
            Dim intSortColumnIndex As Integer
            intSortColumnIndex = objPulicParameters.getObjectValue(Me.htxtSortColumnIndex.Value, -1)
            Dim objSortType As Xydc.Platform.Common.Utilities.PulicParameters.enumSortType
            Try
                objSortType = CType(Me.htxtSortType.Value, Xydc.Platform.Common.Utilities.PulicParameters.enumSortType)
            Catch ex As Exception
                objSortType = Xydc.Platform.Common.Utilities.PulicParameters.enumSortType.None
            End Try

            '网格显示处理
            Try
                '在获取数据时已经恢复了RowFilter、Sort的现场
                '设置数据源
                If Me.m_objDeepDataSet Is Nothing Then
                    Me.grdObjects.DataSource = Nothing
                Else
                    With Me.m_objDeepDataSet.Tables(strTable)
                        Me.grdObjects.DataSource = .DefaultView
                    End With
                End If

                '调整网格参数
                With Me.m_objDeepDataSet.Tables(strTable)
                    If objDataGridProcess.onBeforeDataGridBind(strErrMsg, Me.grdObjects, .DefaultView.Count) = False Then
                        GoTo errProc
                    End If
                End With

                '如果是编辑模式
                If blnEditMode = True Then
                    '移动到最后记录
                    Select Case objenumEditType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                            With Me.m_objDeepDataSet.Tables(strTable)
                                Dim intPageIndex As Integer
                                Dim intSelectIndex As Integer
                                If objDataGridProcess.doMoveToRecord(Me.grdObjects.AllowPaging, Me.grdObjects.PageSize, .DefaultView.Count - 1, intPageIndex, intSelectIndex) = False Then
                                    strErrMsg = "错误：无法移动到最后！"
                                    GoTo errProc
                                End If
                                Try
                                    Me.grdObjects.CurrentPageIndex = intPageIndex
                                    Me.grdObjects.SelectedIndex = intSelectIndex
                                Catch ex As Exception
                                End Try
                            End With

                        Case Else
                    End Select
                End If

                '允许列排序？
                Me.grdObjects.AllowSorting = Not blnEditMode

                '恢复列标题中的排序信息
                If intSortColumnIndex >= 0 Then
                    objDataGridProcess.doClearSortCharInDataGridHead(Me.grdObjects)
                    With Me.grdObjects.Columns(intSortColumnIndex)
                        .HeaderText = objDataGridProcess.getColumnSortHeadString(.HeaderText, objSortType)
                    End With
                End If

                '绑定数据
                Me.grdObjects.DataBind()

                '----------------------------------------------------------------
                '因为这些信息是非绑定的，所以下面的操作必须等绑定完成后执行！！！
                '一旦在后续处理中执行了DataBind，则信息会丢失！！！
                '----------------------------------------------------------------
                '恢复网格中的CheckBox状态
                If objDataGridProcess.doRestoreDataGridCheckBoxStatus(strErrMsg, Me.grdObjects, Request, 0, Me.m_cstrCheckBoxIdInDataGrid) = False Then
                    GoTo errProc
                End If

                '如果是编辑模式
                If blnEditMode = True Then
                    '使能网格
                    If objDataGridProcess.doEnabledDataGrid(strErrMsg, Me.grdObjects, Not blnEditMode) = False Then
                        GoTo errProc
                    End If
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)

            showDataGridInfo = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示DataGrid的数据
        '     strErrMsg      ：返回错误信息
        '     blnEditMode    ：当前编辑状态
        '     objenumEditType：详细操作模式
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showDataGridInfo_HOUSEMATCH( _
            ByRef strErrMsg As String, _
            ByVal blnEditMode As Boolean, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.DeepData.TABLE_Customer_B_Search_Gather
            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess

            showDataGridInfo_HOUSEMATCH = False

            '获取系统保存的网格排序数据
            Dim intSortColumnIndex As Integer
            intSortColumnIndex = objPulicParameters.getObjectValue(Me.htxtSortColumnIndex.Value, -1)
            Dim objSortType As Xydc.Platform.Common.Utilities.PulicParameters.enumSortType
            Try
                objSortType = CType(Me.htxtSortType.Value, Xydc.Platform.Common.Utilities.PulicParameters.enumSortType)
            Catch ex As Exception
                objSortType = Xydc.Platform.Common.Utilities.PulicParameters.enumSortType.None
            End Try

            '网格显示处理
            Try
                '在获取数据时已经恢复了RowFilter、Sort的现场
                '设置数据源
                If Me.m_objDeepDataSet_HOUSEMATCH Is Nothing Then
                    Me.grdHOUSEMATCH.DataSource = Nothing
                Else
                    With Me.m_objDeepDataSet_HOUSEMATCH.Tables(strTable)
                        Me.grdHOUSEMATCH.DataSource = .DefaultView
                    End With
                End If

                '调整网格参数
                With Me.m_objDeepDataSet_HOUSEMATCH.Tables(strTable)
                    If objDataGridProcess.onBeforeDataGridBind(strErrMsg, Me.grdHOUSEMATCH, .DefaultView.Count) = False Then
                        GoTo errProc
                    End If
                End With

                '允许列排序？
                Me.grdHOUSEMATCH.AllowSorting = True


                '恢复列标题中的排序信息
                If intSortColumnIndex >= 0 Then
                    objDataGridProcess.doClearSortCharInDataGridHead(Me.grdObjects)
                    With Me.grdObjects.Columns(intSortColumnIndex)
                        .HeaderText = objDataGridProcess.getColumnSortHeadString(.HeaderText, objSortType)
                    End With
                End If

                '绑定数据
                Me.grdHOUSEMATCH.DataBind()



            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)

            showDataGridInfo_HOUSEMATCH = True
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
            Dim objRadioButtonListProcess As New Xydc.Platform.web.RadioButtonListProcess

            Dim strDDL As String
            showEditPanelInfo = False

            Try
                

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)

            showEditPanelInfo = True
            Exit Function

errProc:
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
        Private Function showEditPanelInfo_HOUSEMATCH( _
            ByRef strErrMsg As String, _
            ByVal blnEditMode As Boolean) As Boolean

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess

            showEditPanelInfo_HOUSEMATCH = False

            Try
                

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)

            showEditPanelInfo_HOUSEMATCH = True
            Exit Function

errProc:
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示整个模块的信息
        '     strErrMsg      ：返回错误信息
        '     blnEditMode    ：当前编辑状态
        '     objenumEditType：详细操作模式
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showModuleData( _
            ByRef strErrMsg As String, _
            ByVal blnEditMode As Boolean, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.DeepData.TABLE_House_B_SalesMessageCustomer
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objControlProcess As New Xydc.Platform.web.ControlProcess

            showModuleData = False

            Try
                '显示网格信息
                If Me.showDataGridInfo(strErrMsg, blnEditMode, objenumEditType) = False Then
                    GoTo errProc
                End If

                ''显示与网格紧密相关的操作或信息提示
                'With Me.m_objDeepDataSet.Tables(strTable).DefaultView
                '    '显示网格位置信息
                '    Me.lblGridLocInfo.Text = objDataGridProcess.getDataGridLocation(Me.grdObjects, .Count)
                '    '显示页面浏览功能
                '    Me.lnkCZMoveFirst.Enabled = objDataGridProcess.canDoMoveFirstPage(Me.grdObjects, .Count) And (Not blnEditMode)
                '    Me.lnkCZMoveLast.Enabled = objDataGridProcess.canDoMoveLastPage(Me.grdObjects, .Count) And (Not blnEditMode)
                '    Me.lnkCZMovePrev.Enabled = objDataGridProcess.canDoMovePreviousPage(Me.grdObjects, .Count) And (Not blnEditMode)
                '    Me.lnkCZMoveNext.Enabled = objDataGridProcess.canDoMoveNextPage(Me.grdObjects, .Count) And (Not blnEditMode)
                '    '显示相关操作
                '    Dim blnEnabled As Boolean
                '    If .Count < 1 Then
                '        blnEnabled = False
                '    Else
                '        blnEnabled = True
                '    End If
                '    'Me.lnkCZDeSelectAll.Enabled = blnEnabled And (Not blnEditMode)
                '    'Me.lnkCZSelectAll.Enabled = blnEnabled And (Not blnEditMode)
                '    Me.lnkCZGotoPage.Enabled = blnEnabled And (Not blnEditMode)
                '    Me.lnkCZSetPageSize.Enabled = blnEnabled And (Not blnEditMode)
                '    With objControlProcess
                '        .doEnabledControl(Me.txtPageSize, Not blnEditMode)
                '        .doEnabledControl(Me.txtPageIndex, Not blnEditMode)
                '    End With


                'End With

                '显示输入窗信息
                If Me.showEditPanelInfo(strErrMsg, blnEditMode) = False Then
                    GoTo errProc
                End If

                '显示操作命令
             

                Me.btnCancel.Enabled = blnEditMode

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
        ' 显示整个模块的信息
        '     strErrMsg      ：返回错误信息
        '     blnEditMode    ：当前编辑状态
        '     objenumEditType：详细操作模式
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showModuleData_HOUSEMATCH( _
            ByRef strErrMsg As String, _
            ByVal blnEditMode As Boolean, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.DeepData.TABLE_Customer_B_Search_Gather
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objControlProcess As New Xydc.Platform.web.ControlProcess

            showModuleData_HOUSEMATCH = False

            Try
                '显示网格信息
                If Me.showDataGridInfo_HOUSEMATCH(strErrMsg, blnEditMode, objenumEditType) = False Then
                    GoTo errProc
                End If

                '显示与网格紧密相关的操作或信息提示
                With Me.m_objDeepDataSet_HOUSEMATCH.Tables(strTable).DefaultView
                    '显示网格位置信息
                    Me.lblGridLocInfo.Text = objDataGridProcess.getDataGridLocation(Me.grdHOUSEMATCH, .Count)
                    '显示页面浏览功能
                    Me.lnkCZMoveFirst.Enabled = objDataGridProcess.canDoMoveFirstPage(Me.grdHOUSEMATCH, .Count) And (Not blnEditMode)
                    Me.lnkCZMoveLast.Enabled = objDataGridProcess.canDoMoveLastPage(Me.grdHOUSEMATCH, .Count) And (Not blnEditMode)
                    Me.lnkCZMovePrev.Enabled = objDataGridProcess.canDoMovePreviousPage(Me.grdHOUSEMATCH, .Count) And (Not blnEditMode)
                    Me.lnkCZMoveNext.Enabled = objDataGridProcess.canDoMoveNextPage(Me.grdHOUSEMATCH, .Count) And (Not blnEditMode)
                    '显示相关操作
                    Dim blnEnabled As Boolean
                    If .Count < 1 Then
                        blnEnabled = False
                    Else
                        blnEnabled = True
                    End If
                    'Me.lnkCZDeSelectAll.Enabled = blnEnabled And (Not blnEditMode)
                    'Me.lnkCZSelectAll.Enabled = blnEnabled And (Not blnEditMode)
                    Me.lnkCZGotoPage.Enabled = blnEnabled And (Not blnEditMode)
                    Me.lnkCZSetPageSize.Enabled = blnEnabled And (Not blnEditMode)
                    With objControlProcess
                        .doEnabledControl(Me.txtPageSize, Not blnEditMode)
                        .doEnabledControl(Me.txtPageIndex, Not blnEditMode)
                    End With
                End With


                '显示输入窗信息
                'If Me.showEditPanelInfo_HOUSEMATCH(strErrMsg, blnEditMode) = False Then
                '    GoTo errProc
                'End If

                '显示操作命令
              

                Me.btnCancel.Enabled = blnEditMode
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.ControlProcess.SafeRelease(objControlProcess)

            showModuleData_HOUSEMATCH = True
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
                        .doTranslateKey(Me.txtPageIndex)
                        .doTranslateKey(Me.txtPageSize)

                    
                    End With

                    '楼盘排序数据
                    '获取数据
                    m_strQuery = Xydc.Platform.Common.Data.DeepData.FIELD_House_B_SalesMessageCustomer_MailRegion + "='' or " + Xydc.Platform.Common.Data.DeepData.FIELD_House_B_SalesMessageCustomer_MailRegion + " is null"
                    If Me.getModuleData(strErrMsg, Me.m_strQuery, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                        GoTo errProc
                    End If
                    '显示数据
                    If Me.showModuleData(strErrMsg, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                        GoTo errProc
                    End If

                    '楼盘匹配数据
                    '获取数据
                    m_strQuery = ""
                    If Me.getModuleData_HOUSEMATCH(strErrMsg, Me.m_strQuery, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                        GoTo errProc
                    End If
                    '显示数据
                    If Me.showModuleData_HOUSEMATCH(strErrMsg, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
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

            '记录审计日志
            If Me.IsPostBack = False Then
                If Me.m_blnSaveScence = False Then
                    Xydc.Platform.SystemFramework.ApplicationLog.WriteAuditPZInfo(Request.UserHostAddress, Request.UserHostName, "[" + MyBase.UserId + "]访问了[楼盘匹配]字典！")
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
        '实现对网格行、列的固定
        Sub grdObjects_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles grdObjects.ItemDataBound

            Dim intCells As Integer
            Dim i As Integer

            Try
                If e.Item.ItemIndex < 0 Then
                    '标题行,输出标题锁定一般属性
                    intCells = e.Item.Cells.Count
                    For i = 0 To intCells - 1 Step 1
                        e.Item.Cells(i).Attributes.CssStyle.Add("top", "expression(" + Me.m_cstrDataGridInDIV + ".scrollTop)")
                    Next
                End If

                If Me.m_intFixedColumns > 0 Then
                    '锁定列
                    For i = 0 To Me.m_intFixedColumns - 1 Step 1
                        e.Item.Cells(i).CssClass = Me.grdObjects.ID + "Locked"
                    Next
                End If

            Catch ex As Exception
            End Try

            Exit Sub

        End Sub

        Private Sub grdObjects_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdObjects.SelectedIndexChanged

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try

                '同步显示编辑窗信息
                If Me.showEditPanelInfo(strErrMsg, Me.m_blnEditMode) = False Then
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

        Private Sub grdHOUSEMATCH_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdHOUSEMATCH.ItemCommand

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim objNewData As System.Collections.Specialized.NameValueCollection
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim strErrMsg As String
            Dim i As Integer
            Dim j As Integer
            Dim intColIndex(5) As Integer
            Dim strRegion As String
            Dim strSourceContent As String
            Dim strSearchContent As String
            Dim strSourceTable As String
            Dim strMatchType As String
            Dim strSalesMessageID As String
            Try
                '获取匹配信息
                i = e.Item.ItemIndex
                j = Me.grdObjects.SelectedIndex
                With New Xydc.Platform.web.DataGridProcess
                    intColIndex(0) = .getDataGridColumnIndex(Me.grdHOUSEMATCH, Xydc.Platform.Common.Data.DeepData.FIELD_Customer_B_Search_Gather_Region)
                    intColIndex(1) = .getDataGridColumnIndex(Me.grdHOUSEMATCH, Xydc.Platform.Common.Data.DeepData.FIELD_Customer_B_Search_Gather_SourceContent)
                    intColIndex(2) = .getDataGridColumnIndex(Me.grdHOUSEMATCH, Xydc.Platform.Common.Data.DeepData.FIELD_Customer_B_Search_Gather_SearchContent)
                    intColIndex(3) = .getDataGridColumnIndex(Me.grdHOUSEMATCH, Xydc.Platform.Common.Data.DeepData.FIELD_Customer_B_Search_Gather_SourceTable)
                    intColIndex(4) = .getDataGridColumnIndex(Me.grdObjects, Xydc.Platform.Common.Data.DeepData.FIELD_House_B_SalesMessage_ID)
                    strRegion = .getDataGridCellValue(Me.grdHOUSEMATCH.Items(i), intColIndex(0))
                    strSourceContent = .getDataGridCellValue(Me.grdHOUSEMATCH.Items(i), intColIndex(1))
                    strSearchContent = .getDataGridCellValue(Me.grdHOUSEMATCH.Items(i), intColIndex(2))
                    strSourceTable = .getDataGridCellValue(Me.grdHOUSEMATCH.Items(i), intColIndex(3))
                    strSalesMessageID = .getDataGridCellValue(Me.grdObjects.Items(j), intColIndex(4))

                    objNewData = New System.Collections.Specialized.NameValueCollection
                    objNewData.Clear()
                    objNewData.Add(Xydc.Platform.Common.Data.DeepData.FIELD_Customer_B_Search_Gather_Region, strRegion)
                    objNewData.Add(Xydc.Platform.Common.Data.DeepData.FIELD_Customer_B_Search_Gather_SourceContent, strRegion)
                    objNewData.Add(Xydc.Platform.Common.Data.DeepData.FIELD_Customer_B_Search_Gather_SearchContent, strRegion)
                    objNewData.Add(Xydc.Platform.Common.Data.DeepData.FIELD_Customer_B_Search_Gather_SourceTable, strRegion)
                    
                End With
                Select Case e.CommandName
                    Case "lnkSingleMatch"
                        '单个匹配
                        strMatchType = "0"
                    Case "lnkMultiMatch"
                        '多个匹配
                        strMatchType = "1"
                End Select
               
                '插入匹配记录
                If objdacCommon.doSaveData(strErrMsg, MyBase.UserId, MyBase.UserPassword, Xydc.Platform.Common.Data.DeepData.TABLE_Customer_B_Search_Gather, _
                                           "", False, objNewData, Common.Utilities.PulicParameters.enumEditType.eAddNew) = False Then
                    GoTo errProc
                End If

                '更新客户信息
                Dim strWhere As String
                If strMatchType = "0" Then
                    strWhere = " SalesMessageID=convert(integer,'" + strSalesMessageID + "')"
                Else
                    strWhere = " (MailRegion='' or  MailRegion is null) and charindex('" + strSearchContent + "',[MailAddress])>0 "
                End If
                objNewData.Clear()
                objNewData.Add(Xydc.Platform.Common.Data.DeepData.FIELD_House_B_SalesMessageCustomer_MailRegion, strRegion)
                '更新客户记录
                If objdacCommon.doSaveData(strErrMsg, MyBase.UserId, MyBase.UserPassword, Xydc.Platform.Common.Data.DeepData.TABLE_House_B_SalesMessageCustomer, _
                                           strWhere, False, objNewData, Common.Utilities.PulicParameters.enumEditType.eUpdate) = False Then
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


        Private Sub grdHOUSEMATCH_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdHOUSEMATCH.SelectedIndexChanged
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '显示记录位置
                With New Xydc.Platform.web.DataGridProcess
                    Me.lblGridLocInfo.Text = .getDataGridLocation(Me.grdObjects, Me.m_intRows)
                End With

                '同步显示编辑窗信息
                If Me.showEditPanelInfo_HOUSEMATCH(strErrMsg, Me.m_blnEditMode) = False Then
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

        Private Sub grdObjects_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles grdObjects.SortCommand

            Dim strTable As String = Xydc.Platform.Common.Data.DeepData.TABLE_House_B_SalesMessageCustomer
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
                If Me.getModuleData(strErrMsg, Me.m_strQuery, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If

                '获取原排序命令
                With Me.m_objDeepDataSet.Tables(strTable)
                    strOldCommand = .DefaultView.Sort
                End With

                '获取要执行的排序命令
                objDataGridProcess.getColumnSortCommand(strErrMsg, strOldCommand, e.SortExpression, strFinalCommand, objenumSortType)
                If strErrMsg <> "" Then
                    GoTo errProc
                End If

                '执行排序
                With Me.m_objDeepDataSet.Tables(strTable)
                    .DefaultView.Sort = strFinalCommand
                End With

                '获取排序列的列索引
                objDataGridItem = CType(e.CommandSource, System.Web.UI.WebControls.DataGridItem)
                strUniqueId = Request.Form("__EVENTTARGET")
                intColumnIndex = objDataGridProcess.getColumnIndexByUniqueIdInRow(objDataGridItem, strUniqueId)

                '保存排序信息
                Me.htxtSortColumnIndex.Value = intColumnIndex.ToString()
                Me.htxtSortType.Value = CType(objenumSortType, Integer).ToString()
                Me.htxtSort.Value = strFinalCommand

                '重新显示数据
                If Me.showModuleData(strErrMsg, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
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

        Private Sub doMoveFirst(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_HOUSEMATCH(strErrMsg, Me.m_strQuery, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(0, Me.grdHOUSEMATCH.PageCount)
                Me.grdHOUSEMATCH.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_HOUSEMATCH(strErrMsg, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
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

        Private Sub doMoveLast(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_HOUSEMATCH(strErrMsg, Me.m_strQuery, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(Me.grdHOUSEMATCH.PageCount - 1, Me.grdHOUSEMATCH.PageCount)
                Me.grdHOUSEMATCH.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_HOUSEMATCH(strErrMsg, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
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

        Private Sub doMoveNext(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_HOUSEMATCH(strErrMsg, Me.m_strQuery, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(Me.grdHOUSEMATCH.CurrentPageIndex + 1, Me.grdHOUSEMATCH.PageCount)
                Me.grdHOUSEMATCH.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_HOUSEMATCH(strErrMsg, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
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

        Private Sub doMovePrevious(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_HOUSEMATCH(strErrMsg, Me.m_strQuery, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(Me.grdHOUSEMATCH.CurrentPageIndex - 1, Me.grdHOUSEMATCH.PageCount)
                Me.grdHOUSEMATCH.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_HOUSEMATCH(strErrMsg, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
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

        Private Sub doGotoPage(ByVal strControlId As String)

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            '获取新的页大小
            Dim intPageIndex As Integer
            intPageIndex = objPulicParameters.getObjectValue(Me.txtPageIndex.Text, 0)
            If intPageIndex <= 0 Then
                intPageIndex = 0
            Else
                intPageIndex -= 1
            End If

            Try
                '获取数据
                If Me.getModuleData_HOUSEMATCH(strErrMsg, Me.m_strQuery, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If

                '设置新的页
                Me.grdHOUSEMATCH.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_HOUSEMATCH(strErrMsg, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If

                '信息同步
                Me.txtPageIndex.Text = (Me.grdHOUSEMATCH.CurrentPageIndex + 1).ToString()

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

        Private Sub doSetPageSize(ByVal strControlId As String)

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            '获取新的页大小
            Dim intPageSize As Integer
            intPageSize = objPulicParameters.getObjectValue(Me.txtPageSize.Text, 100)
            If intPageSize <= 0 Then intPageSize = 100

            Try
                '获取数据
                If Me.getModuleData_HOUSEMATCH(strErrMsg, Me.m_strQuery, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If

                '设置新的页大小
                Me.grdHOUSEMATCH.PageSize = intPageSize

                '刷新网格显示
                If Me.showModuleData_HOUSEMATCH(strErrMsg, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If

                '信息同步
                Me.txtPageSize.Text = (Me.grdHOUSEMATCH.PageSize).ToString()

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

        Private Sub doSelectAll(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                If objDataGridProcess.doCheckedDataGridCheckBox(strErrMsg, Me.grdHOUSEMATCH, 0, Me.m_cstrCheckBoxIdInDataGrid, True) = False Then
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

        Private Sub doDeSelectAll(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                If objDataGridProcess.doCheckedDataGridCheckBox(strErrMsg, Me.grdHOUSEMATCH, 0, Me.m_cstrCheckBoxIdInDataGrid, False) = False Then
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

        Private Sub doSearch(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '搜索数据
                If Me.searchModuleData(strErrMsg, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If

                '刷新网格显示
                If Me.showModuleData(strErrMsg, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
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

        Private Sub lnkCZMoveFirst_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZMoveFirst.Click
            Me.doMoveFirst("lnkCZMoveFirst")
        End Sub

        Private Sub lnkCZMoveLast_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZMoveLast.Click
            Me.doMoveLast("lnkCZMoveLast")
        End Sub

        Private Sub lnkCZMoveNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZMoveNext.Click
            Me.doMoveNext("lnkCZMoveNext")
        End Sub

        Private Sub lnkCZMovePrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZMovePrev.Click
            Me.doMovePrevious("lnkCZMovePrev")
        End Sub

        Private Sub lnkCZGotoPage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZGotoPage.Click
            Me.doGotoPage("lnkCZGotoPage")
        End Sub

        Private Sub lnkCZSetPageSize_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZSetPageSize.Click
            Me.doSetPageSize("lnkCZSetPageSize")
        End Sub


        'Private Sub lnkCZSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZSelectAll.Click
        '    Me.doSelectAll("lnkCZSelectAll")
        'End Sub

        'Private Sub lnkCZDeSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZDeSelectAll.Click
        '    Me.doDeSelectAll("lnkCZDeSelectAll")
        'End Sub












        '----------------------------------------------------------------
        '模块特殊操作处理器
        '----------------------------------------------------------------
        Private Sub doAddNew(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '设置编辑模式
                Me.m_blnEditMode = True
                Me.m_objenumEditType = Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                Me.m_intCurrentPageIndex = Me.grdObjects.CurrentPageIndex
                Me.m_intCurrentSelectIndex = Me.grdObjects.SelectedIndex

                '保存相关信息
                Me.htxtEditMode.Value = Me.m_blnEditMode.ToString()
                Me.htxtEditType.Value = CType(Me.m_objenumEditType, Integer).ToString()
                Me.htxtCurrentPage.Value = Me.m_intCurrentPageIndex.ToString()
                Me.htxtCurrentRow.Value = Me.m_intCurrentSelectIndex.ToString()

                '进入编辑状态
                If Me.getModuleData(strErrMsg, Me.m_strQuery, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If

                '显示信息
                If Me.showModuleData(strErrMsg, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
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

        Private Sub doUpdate(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '设置编辑模式
                Me.m_blnEditMode = True
                Me.m_objenumEditType = Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eUpdate
                Me.m_intCurrentPageIndex = Me.grdHOUSEMATCH.CurrentPageIndex
                Me.m_intCurrentSelectIndex = Me.grdHOUSEMATCH.SelectedIndex

                '保存相关信息
                Me.htxtEditMode.Value = Me.m_blnEditMode.ToString()
                Me.htxtEditType.Value = CType(Me.m_objenumEditType, Integer).ToString()
                Me.htxtCurrentPage.Value = Me.m_intCurrentPageIndex.ToString()
                Me.htxtCurrentRow.Value = Me.m_intCurrentSelectIndex.ToString()

                '进入编辑状态
                If Me.getModuleData(strErrMsg, Me.m_strQuery, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If

                '显示信息
                If Me.showModuleData(strErrMsg, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
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

            Dim strTable As String = Xydc.Platform.Common.Data.DeepData.TABLE_House_B_SalesMessageCustomer
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String
            Dim strWhere As String = ""
            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            Try





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
                        Me.grdObjects.CurrentPageIndex = Me.m_intCurrentPageIndex
                        Me.grdObjects.SelectedIndex = Me.m_intCurrentSelectIndex
                    Catch ex As Exception
                    End Try

                    '进入非编辑状态
                    If Me.getModuleData(strErrMsg, Me.m_strQuery, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                        GoTo errProc
                    End If

                    '显示信息
                    If Me.showModuleData(strErrMsg, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
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

        Private Sub doDelete(ByVal strControlId As String)

            Dim strTable As String = Xydc.Platform.Common.Data.DeepData.TABLE_House_B_SalesMessageCustomer
            Dim objsystemCommon As New Xydc.Platform.BusinessFacade.systemCommon
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String
            Dim intStep As Integer
            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            Try


            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.BusinessFacade.systemCommon.SafeRelease(objsystemCommon)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.BusinessFacade.systemCommon.SafeRelease(objsystemCommon)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub doRefresh(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '搜索数据
                If Me.getModuleData(strErrMsg, Me.m_strQuery, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If

                '刷新网格显示
                If Me.showModuleData(strErrMsg, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If

                '搜索数据
                If Me.getModuleData_HOUSEMATCH(strErrMsg, Me.m_strQuery, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If

                '刷新网格显示
                If Me.showModuleData_HOUSEMATCH(strErrMsg, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
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

        Private Sub doSeek(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String
            Dim strWhere As String

            Try
                strWhere = ""
                If Me.txtSearchContent.Text <> "" Then
                    strWhere = Me.txtSearchContent.Text
                End If


                If Me.getModuleData_HOUSEMATCH(strErrMsg, strWhere, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If

                '刷新网格显示
                If Me.showModuleData_HOUSEMATCH(strErrMsg, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
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

        Private Sub doSeekSort(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String
            Dim strWhere As String

            Try
                strWhere = ""


                If Me.getModuleData(strErrMsg, strWhere, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If

                '刷新网格显示
                If Me.showModuleData(strErrMsg, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
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



        Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
            Me.doClose("btnCancel")
        End Sub


        Private Sub LnkMLSeek_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LnkMLSeek.Click
            doSeek("LnkMLSeek")
        End Sub

        Private Sub lnkSearchSort_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSearchSort.Click
            doSeekSort("lnkSearchSort")
        End Sub



    End Class
End Namespace