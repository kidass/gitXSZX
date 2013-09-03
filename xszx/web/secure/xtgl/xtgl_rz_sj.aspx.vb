Imports System.Web.Security

Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.web
    ' 类名    ：xtgl_rz_pz
    ' 
    ' 调用性质：
    '     可调用其他模块
    '
    ' 功能描述： 
    '   　处理查询审计管理员审计日志任务
    '
    ' 接口参数：
    '
    '----------------------------------------------------------------

    Partial Public Class xtgl_rz_sj
        Inherits Xydc.Platform.web.PageBase


        '----------------------------------------------------------------
        '模块私用参数
        '----------------------------------------------------------------
        '本模块相对image的相对路径
        Private m_cstrRelativePathToImage As String = "../../"
        '文件下载后的缓存路径
        Private m_cstrUrlBaseToFileCache As String = "/temp/filecache/"
        '打印模版相对于应用根的路径
        Private m_cstrExcelMBRelativePathToAppRoot As String = "/template/excel/"
        '打印文件缓存目录相对于应用根的路径
        Private m_cstrPrintCacheRelativePathToAppRoot As String = "/temp/printcache/"

        '----------------------------------------------------------------
        '模块授权参数
        '----------------------------------------------------------------
        Private m_cstrPrevilegeParamPrefix As String = "xtgl_rz_sj_previlege_param"
        Private m_blnPrevilegeParams(1) As Boolean

        '----------------------------------------------------------------
        '模块现场保留参数，恢复完成后立即释放session资源
        '----------------------------------------------------------------
        Private m_objSaveScence As Xydc.Platform.BusinessFacade.IMXtglRzSj
        Private m_blnSaveScence As Boolean

        '----------------------------------------------------------------
        '模块接口参数
        '----------------------------------------------------------------
        Private m_blnInterface As Boolean

        '----------------------------------------------------------------
        '与数据网格grdLOG相关的参数
        '----------------------------------------------------------------
        '网格中模板列中的控件ID
        Private Const m_cstrCheckBoxIdInDataGrid_LOG As String = "chkLOG"
        '包含网格的DIV对象ID
        Private Const m_cstrDataGridInDIV_LOG As String = "divLOG"
        '网格要锁定的列数
        Private m_intFixedColumns_LOG As Integer

        '----------------------------------------------------------------
        '要访问的数据
        '----------------------------------------------------------------
        Private m_objDataSet_LOG As Xydc.Platform.Common.Data.AppManagerData
        Private m_strQuery_LOG As String '记录m_objDataSet_LOG搜索串
        Private m_intRows_LOG As Integer '记录m_objDataSet_LOG的DefaultView记录数








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

            Try
                If Me.m_objSaveScence Is Nothing Then
                    Exit Try
                End If

                With Me.m_objSaveScence
                    Me.htxtLOGQuery.Value = .htxtLOGQuery
                    Me.htxtLOGRows.Value = .htxtLOGRows
                    Me.htxtLOGSort.Value = .htxtLOGSort
                    Me.htxtLOGSortColumnIndex.Value = .htxtLOGSortColumnIndex
                    Me.htxtLOGSortType.Value = .htxtLOGSortType

                    Me.htxtDivLeftBody.Value = .htxtDivLeftBody
                    Me.htxtDivTopBody.Value = .htxtDivTopBody
                    Me.htxtDivLeftLOG.Value = .htxtDivLeftLOG
                    Me.htxtDivTopLOG.Value = .htxtDivTopLOG

                    Me.htxtSessionIdQuery.Value = .htxtSessionIdQuery

                    Me.txtLOGPageIndex.Text = .txtLOGPageIndex
                    Me.txtLOGPageSize.Text = .txtLOGPageSize

                    Me.txtLOGSearch_YHBS.Text = .txtLOGSearch_YHBS
                    Me.txtLOGSearch_CZMS.Text = .txtLOGSearch_CZMS
                    Me.txtLOGSearch_CZSJMin.Text = .txtLOGSearch_CZSJMin
                    Me.txtLOGSearch_CZSJMax.Text = .txtLOGSearch_CZSJMax

                    Try
                        Me.grdLOG.PageSize = .grdLOGPageSize
                    Catch ex As Exception
                    End Try
                    Try
                        Me.grdLOG.CurrentPageIndex = .grdLOGCurrentPageIndex
                    Catch ex As Exception
                    End Try
                    Try
                        Me.grdLOG.SelectedIndex = .grdLOGSelectedIndex
                    Catch ex As Exception
                    End Try

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
                Me.m_objSaveScence = New Xydc.Platform.BusinessFacade.IMXtglRzSj

                '保存现场信息
                With Me.m_objSaveScence
                    .htxtLOGQuery = Me.htxtLOGQuery.Value
                    .htxtLOGRows = Me.htxtLOGRows.Value
                    .htxtLOGSort = Me.htxtLOGSort.Value
                    .htxtLOGSortColumnIndex = Me.htxtLOGSortColumnIndex.Value
                    .htxtLOGSortType = Me.htxtLOGSortType.Value

                    .htxtDivLeftBody = Me.htxtDivLeftBody.Value
                    .htxtDivTopBody = Me.htxtDivTopBody.Value
                    .htxtDivLeftLOG = Me.htxtDivLeftLOG.Value
                    .htxtDivTopLOG = Me.htxtDivTopLOG.Value

                    .htxtSessionIdQuery = Me.htxtSessionIdQuery.Value

                    .txtLOGPageIndex = Me.txtLOGPageIndex.Text
                    .txtLOGPageSize = Me.txtLOGPageSize.Text

                    .txtLOGSearch_YHBS = Me.txtLOGSearch_YHBS.Text
                    .txtLOGSearch_CZMS = Me.txtLOGSearch_CZMS.Text
                    .txtLOGSearch_CZSJMin = Me.txtLOGSearch_CZSJMin.Text
                    .txtLOGSearch_CZSJMax = Me.txtLOGSearch_CZSJMax.Text

                    .grdLOGPageSize = Me.grdLOG.PageSize
                    .grdLOGCurrentPageIndex = Me.grdLOG.CurrentPageIndex
                    .grdLOGSelectedIndex = Me.grdLOG.SelectedIndex

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
            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' 获取接口参数
        '----------------------------------------------------------------
        Private Function getInterfaceParameters( _
            ByRef strErrMsg As String, _
            ByRef blnContinue As Boolean) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            getInterfaceParameters = False
            blnContinue = True

            Try
                '从QueryString中解析接口参数(不论是否回发)
                m_blnInterface = False

                '获取恢复现场参数
                Me.m_blnSaveScence = False
                If Me.IsPostBack = False Then
                    Dim strSessionId As String
                    strSessionId = objPulicParameters.getObjectValue(Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.MSessionId), "")
                    Try
                        Me.m_objSaveScence = CType(Session.Item(strSessionId), Xydc.Platform.BusinessFacade.IMXtglRzSj)
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

                '获取局部接口参数
                Me.m_intFixedColumns_LOG = objPulicParameters.getObjectValue(Me.htxtLOGFixed.Value, 0)
                Me.m_intRows_LOG = objPulicParameters.getObjectValue(Me.htxtLOGRows.Value, 0)
                Me.m_strQuery_LOG = Me.htxtLOGQuery.Value

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

        End Sub

        '----------------------------------------------------------------
        ' 获取grdLOG的搜索条件(默认表前缀a.)
        '     strErrMsg      ：返回错误信息
        '     strQuery       ：返回的搜索条件
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function getQueryString_LOG( _
            ByRef strErrMsg As String, _
            ByRef strQuery As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            getQueryString_LOG = False
            strQuery = ""

            Try
                '按“用户标识”搜索
                Dim strYHBS As String
                strYHBS = Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_VT_B_AUDITSJLOG_OPUSER
                If Me.txtLOGSearch_YHBS.Text.Length > 0 Then Me.txtLOGSearch_YHBS.Text = Me.txtLOGSearch_YHBS.Text.Trim()
                If Me.txtLOGSearch_YHBS.Text <> "" Then
                    Me.txtLOGSearch_YHBS.Text = objPulicParameters.getNewSearchString(Me.txtLOGSearch_YHBS.Text)
                    If strQuery = "" Then
                        strQuery = strYHBS + " like '" + Me.txtLOGSearch_YHBS.Text + "%'"
                    Else
                        strQuery = strQuery + " and " + strYHBS + " like '" + Me.txtLOGSearch_YHBS.Text + "%'"
                    End If
                End If

                '按“操作描述”搜索
                Dim strCZMS As String
                strCZMS = Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_VT_B_AUDITSJLOG_OPNOTE
                If Me.txtLOGSearch_CZMS.Text.Length > 0 Then Me.txtLOGSearch_CZMS.Text = Me.txtLOGSearch_CZMS.Text.Trim()
                If Me.txtLOGSearch_CZMS.Text <> "" Then
                    Me.txtLOGSearch_CZMS.Text = objPulicParameters.getNewSearchString(Me.txtLOGSearch_CZMS.Text)
                    If strQuery = "" Then
                        strQuery = strCZMS + " like '" + Me.txtLOGSearch_CZMS.Text + "%'"
                    Else
                        strQuery = strQuery + " and " + strCZMS + " like '" + Me.txtLOGSearch_CZMS.Text + "%'"
                    End If
                End If

                '按“操作时间”搜索
                Dim objTimeMin As System.DateTime
                Dim objTimeMax As System.DateTime
                Dim strCZSJ As String
                strCZSJ = "convert(" + Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_VT_B_AUDITSJLOG_OPTIME + ", 'System.DateTime')"
                Me.txtLOGSearch_CZSJMin.Text = Me.txtLOGSearch_CZSJMin.Text.Trim
                Me.txtLOGSearch_CZSJMax.Text = Me.txtLOGSearch_CZSJMax.Text.Trim
                If Me.txtLOGSearch_CZSJMin.Text <> "" Then
                    If objPulicParameters.isDatetimeString(Me.txtLOGSearch_CZSJMin.Text) = False Then
                        strErrMsg = "错误：无效的日期！"
                        GoTo errProc
                    End If
                    objTimeMin = CType(Me.txtLOGSearch_CZSJMin.Text, System.DateTime)
                    Me.txtLOGSearch_CZSJMin.Text = objTimeMin.ToString("yyyy-MM-dd HH:mm:ss")
                End If
                If Me.txtLOGSearch_CZSJMax.Text <> "" Then
                    If objPulicParameters.isDatetimeString(Me.txtLOGSearch_CZSJMax.Text) = False Then
                        strErrMsg = "错误：无效的日期！"
                        GoTo errProc
                    End If
                    objTimeMax = CType(Me.txtLOGSearch_CZSJMax.Text, System.DateTime)
                    Me.txtLOGSearch_CZSJMax.Text = objTimeMax.ToString("yyyy-MM-dd HH:mm:ss")
                End If
                If Me.txtLOGSearch_CZSJMin.Text <> "" And Me.txtLOGSearch_CZSJMax.Text <> "" Then
                    If objTimeMin > objTimeMax Then
                        Me.txtLOGSearch_CZSJMin.Text = objTimeMax.ToString("yyyy-MM-dd HH:mm:ss")
                        Me.txtLOGSearch_CZSJMax.Text = objTimeMin.ToString("yyyy-MM-dd HH:mm:ss")
                    End If
                    If strQuery = "" Then
                        strQuery = strCZSJ + " >= #" + Me.txtLOGSearch_CZSJMin.Text + "#"
                        strQuery = strQuery + " and " + strCZSJ + " <= #" + Me.txtLOGSearch_CZSJMax.Text + "#"
                    Else
                        strQuery = strQuery + " and " + strCZSJ + " >= #" + Me.txtLOGSearch_CZSJMin.Text + "#"
                        strQuery = strQuery + " and " + strCZSJ + " <= #" + Me.txtLOGSearch_CZSJMax.Text + "#"
                    End If
                ElseIf Me.txtLOGSearch_CZSJMin.Text <> "" Then
                    If strQuery = "" Then
                        strQuery = strCZSJ + " > #" + Me.txtLOGSearch_CZSJMin.Text + "#"
                    Else
                        strQuery = strQuery + " and " + strCZSJ + " > #" + Me.txtLOGSearch_CZSJMin.Text + "#"
                    End If
                ElseIf Me.txtLOGSearch_CZSJMax.Text <> "" Then
                    If strQuery = "" Then
                        strQuery = strCZSJ + " < #" + Me.txtLOGSearch_CZSJMax.Text + "#"
                    Else
                        strQuery = strQuery + " and " + strCZSJ + " > #" + Me.txtLOGSearch_CZSJMax.Text + "#"
                    End If
                Else
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            getQueryString_LOG = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取grdLOG要显示的数据信息
        '     strErrMsg      ：返回错误信息
        '     strWhere       ：搜索条件
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function getModuleData_LOG( _
            ByRef strErrMsg As String, _
            ByVal strWhere As String) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_VT_B_AUDITSJLOG
            Dim objsystemAppManager As New Xydc.Platform.BusinessFacade.systemAppManager

            getModuleData_LOG = False

            Try
                '备份Sort字符串
                Dim strSort As String
                strSort = Me.htxtLOGSort.Value
                If strSort.Length > 0 Then strSort = strSort.Trim

                '释放资源
                If Not (Me.m_objDataSet_LOG Is Nothing) Then
                    Me.m_objDataSet_LOG.Dispose()
                    Me.m_objDataSet_LOG = Nothing
                End If

                '重新检索数据
                Dim strTempPath As String = Request.ApplicationPath + Me.m_cstrUrlBaseToFileCache
                strTempPath = Server.MapPath(strTempPath)
                If objsystemAppManager.getDataSet_AUDITSJLOG(strErrMsg, MyBase.UserId, MyBase.UserPassword, strTempPath, strWhere, Me.m_objDataSet_LOG) = False Then
                    GoTo errProc
                End If

                '恢复Sort字符串
                With Me.m_objDataSet_LOG.Tables(strTable)
                    .DefaultView.Sort = strSort
                End With

                '缓存参数
                With Me.m_objDataSet_LOG.Tables(strTable)
                    Me.htxtLOGRows.Value = .DefaultView.Count.ToString()
                    Me.m_intRows_LOG = .DefaultView.Count
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.BusinessFacade.systemAppManager.SafeRelease(objsystemAppManager)

            getModuleData_LOG = True
            Exit Function

errProc:
            Xydc.Platform.BusinessFacade.systemAppManager.SafeRelease(objsystemAppManager)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据屏幕搜索条件搜索grdLOG数据
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function searchModuleData_LOG(ByRef strErrMsg As String) As Boolean

            searchModuleData_LOG = False

            Try
                '获取搜索字符串
                Dim strQuery As String
                If Me.getQueryString_LOG(strErrMsg, strQuery) = False Then
                    GoTo errProc
                End If

                '搜索数据
                If Me.getModuleData_LOG(strErrMsg, strQuery) = False Then
                    GoTo errProc
                End If

                '记录搜索字符串
                Me.m_strQuery_LOG = strQuery
                Me.htxtLOGQuery.Value = Me.m_strQuery_LOG

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            searchModuleData_LOG = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示grdLOG的数据
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showDataGridInfo_LOG(ByRef strErrMsg As String) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_VT_B_AUDITSJLOG
            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess

            showDataGridInfo_LOG = False

            '获取系统保存的网格排序数据
            Dim intSortColumnIndex As Integer
            intSortColumnIndex = objPulicParameters.getObjectValue(Me.htxtLOGSortColumnIndex.Value, -1)
            Dim objSortType As Xydc.Platform.Common.Utilities.PulicParameters.enumSortType
            Try
                objSortType = CType(Me.htxtLOGSortType.Value, Xydc.Platform.Common.Utilities.PulicParameters.enumSortType)
            Catch ex As Exception
                objSortType = Xydc.Platform.Common.Utilities.PulicParameters.enumSortType.None
            End Try

            '网格显示处理
            Try
                '在获取数据时已经恢复了RowFilter、Sort的现场
                '设置数据源
                If Me.m_objDataSet_LOG Is Nothing Then
                    Me.grdLOG.DataSource = Nothing
                Else
                    With Me.m_objDataSet_LOG.Tables(strTable)
                        Me.grdLOG.DataSource = .DefaultView
                    End With
                End If

                '调整网格参数
                With Me.m_objDataSet_LOG.Tables(strTable)
                    If objDataGridProcess.onBeforeDataGridBind(strErrMsg, Me.grdLOG, .DefaultView.Count) = False Then
                        GoTo errProc
                    End If
                End With

                '恢复列标题中的排序信息
                If intSortColumnIndex >= 0 Then
                    objDataGridProcess.doClearSortCharInDataGridHead(Me.grdLOG)
                    With Me.grdLOG.Columns(intSortColumnIndex)
                        .HeaderText = objDataGridProcess.getColumnSortHeadString(.HeaderText, objSortType)
                    End With
                End If

                '绑定数据
                Me.grdLOG.DataBind()

                '----------------------------------------------------------------
                '因为这些信息是非绑定的，所以下面的操作必须等绑定完成后执行！！！
                '一旦在后续处理中执行了DataBind，则信息会丢失！！！
                '----------------------------------------------------------------
                '恢复网格中的CheckBox状态
                If objDataGridProcess.doRestoreDataGridCheckBoxStatus(strErrMsg, Me.grdLOG, Request, 0, Me.m_cstrCheckBoxIdInDataGrid_LOG) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)

            showDataGridInfo_LOG = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示grdLOG及相关信息
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showModuleData_LOG(ByRef strErrMsg As String) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_VT_B_AUDITSJLOG
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess

            showModuleData_LOG = False

            Try
                '显示网格信息
                If Me.showDataGridInfo_LOG(strErrMsg) = False Then
                    GoTo errProc
                End If

                '显示与网格紧密相关的操作或信息提示
                With Me.m_objDataSet_LOG.Tables(strTable).DefaultView
                    '显示网格位置信息
                    Me.lblLOGGridLocInfo.Text = objDataGridProcess.getDataGridLocation(Me.grdLOG, .Count)

                    '显示页面浏览功能
                    Me.lnkCZLOGMoveFirst.Enabled = objDataGridProcess.canDoMoveFirstPage(Me.grdLOG, .Count)
                    Me.lnkCZLOGMoveLast.Enabled = objDataGridProcess.canDoMoveLastPage(Me.grdLOG, .Count)
                    Me.lnkCZLOGMovePrev.Enabled = objDataGridProcess.canDoMovePreviousPage(Me.grdLOG, .Count)
                    Me.lnkCZLOGMoveNext.Enabled = objDataGridProcess.canDoMoveNextPage(Me.grdLOG, .Count)

                    '显示相关操作
                    Dim blnEnabled As Boolean
                    If .Count < 1 Then
                        blnEnabled = False
                    Else
                        blnEnabled = True
                    End If
                    Me.lnkCZLOGDeSelectAll.Enabled = blnEnabled
                    Me.lnkCZLOGSelectAll.Enabled = blnEnabled
                    Me.lnkCZLOGGotoPage.Enabled = blnEnabled
                    Me.lnkCZLOGSetPageSize.Enabled = blnEnabled
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)

            showModuleData_LOG = True
            Exit Function

errProc:
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示模块级的操作状态
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showModuleData_MAIN(ByRef strErrMsg As String) As Boolean

            showModuleData_MAIN = False

            Try
                Me.btnPrint.Enabled = Me.m_blnPrevilegeParams(1)
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            showModuleData_MAIN = True
            Exit Function

errProc:
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
                '显示Pannel
                Me.panelMain.Visible = True
                Me.panelError.Visible = Not Me.panelMain.Visible

                '执行键转译(不论是否是“回发”)
                Try
                    objControlProcess.doTranslateKey(Me.txtLOGPageIndex)
                    objControlProcess.doTranslateKey(Me.txtLOGPageSize)
                    objControlProcess.doTranslateKey(Me.txtLOGSearch_YHBS)
                    objControlProcess.doTranslateKey(Me.txtLOGSearch_CZMS)
                    objControlProcess.doTranslateKey(Me.txtLOGSearch_CZSJMin)
                    objControlProcess.doTranslateKey(Me.txtLOGSearch_CZSJMax)
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '初始化
                If Me.m_blnSaveScence = False Then
                    Me.txtLOGSearch_CZSJMin.Text = Now.ToString("yyyy-MM-dd")
                End If

                '显示模块级操作
                If Me.showModuleData_MAIN(strErrMsg) = False Then
                    GoTo errProc
                End If

                '显示数据
                If Me.m_blnSaveScence = False Then
                    If Me.searchModuleData_LOG(strErrMsg) = False Then
                        GoTo errProc
                    End If
                Else
                    If Me.getModuleData_LOG(strErrMsg, Me.m_strQuery_LOG) = False Then
                        GoTo errProc
                    End If
                End If
                If Me.showModuleData_LOG(strErrMsg) = False Then
                    GoTo errProc
                End If
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
            If blnDo = False Then
                GoTo normExit
            End If

            '获取接口参数
            If Me.getInterfaceParameters(strErrMsg, blnDo) = False Then
                GoTo errProc
            End If
            If blnDo = False Then
                GoTo normExit
            End If

            '控件初始化
            If Me.initializeControls(strErrMsg) = False Then
                GoTo errProc
            End If

            '记录审计日志
            If Me.IsPostBack = False Then
                If Me.m_blnSaveScence = False Then
                    Xydc.Platform.SystemFramework.ApplicationLog.WriteAuditSJInfo(Request.UserHostAddress, Request.UserHostName, "[" + MyBase.UserId + "]访问了[审计管理员审计日志]！")
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









        Sub grdLOG_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles grdLOG.ItemDataBound

            Dim intCells As Integer
            Dim i As Integer

            Try
                If e.Item.ItemIndex < 0 Then
                    '标题行,输出标题锁定一般属性
                    intCells = e.Item.Cells.Count
                    For i = 0 To intCells - 1 Step 1
                        e.Item.Cells(i).Attributes.CssStyle.Add("top", "expression(" + Me.m_cstrDataGridInDIV_LOG + ".scrollTop)")
                    Next
                End If
                If Me.m_intFixedColumns_LOG > 0 Then
                    '锁定列
                    For i = 0 To Me.m_intFixedColumns_LOG - 1 Step 1
                        e.Item.Cells(i).CssClass = Me.grdLOG.ID + "Locked"
                    Next
                End If
            Catch ex As Exception
            End Try

        End Sub

        Private Sub grdLOG_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdLOG.SelectedIndexChanged

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '显示记录位置
                Me.lblLOGGridLocInfo.Text = objDataGridProcess.getDataGridLocation(Me.grdLOG, Me.m_intRows_LOG)
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

        Private Sub grdLOG_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles grdLOG.SortCommand

            Dim strTable As String = Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_VT_B_AUDITSJLOG
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
                If Me.getModuleData_LOG(strErrMsg, Me.m_strQuery_LOG) = False Then
                    GoTo errProc
                End If

                '获取原排序命令
                strOldCommand = Me.m_objDataSet_LOG.Tables(strTable).DefaultView.Sort

                '获取要执行的排序命令
                objDataGridProcess.getColumnSortCommand(strErrMsg, strOldCommand, e.SortExpression, strFinalCommand, objenumSortType)
                If strErrMsg <> "" Then
                    GoTo errProc
                End If

                '执行排序
                Me.m_objDataSet_LOG.Tables(strTable).DefaultView.Sort = strFinalCommand

                '获取排序列的列索引
                objDataGridItem = CType(e.CommandSource, System.Web.UI.WebControls.DataGridItem)
                strUniqueId = Request.Form("__EVENTTARGET")
                intColumnIndex = objDataGridProcess.getColumnIndexByUniqueIdInRow(objDataGridItem, strUniqueId)

                '保存排序信息
                Me.htxtLOGSortColumnIndex.Value = intColumnIndex.ToString()
                Me.htxtLOGSortType.Value = CType(objenumSortType, Integer).ToString()
                Me.htxtLOGSort.Value = strFinalCommand

                '重新显示数据
                If Me.showModuleData_LOG(strErrMsg) = False Then
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










        Private Sub doMoveFirst_LOG(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_LOG(strErrMsg, Me.m_strQuery_LOG) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(0, Me.grdLOG.PageCount)
                Me.grdLOG.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_LOG(strErrMsg) = False Then
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

        Private Sub doMoveLast_LOG(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_LOG(strErrMsg, Me.m_strQuery_LOG) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(Me.grdLOG.PageCount - 1, Me.grdLOG.PageCount)
                Me.grdLOG.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_LOG(strErrMsg) = False Then
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

        Private Sub doMoveNext_LOG(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_LOG(strErrMsg, Me.m_strQuery_LOG) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(Me.grdLOG.CurrentPageIndex + 1, Me.grdLOG.PageCount)
                Me.grdLOG.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_LOG(strErrMsg) = False Then
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

        Private Sub doMovePrevious_LOG(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_LOG(strErrMsg, Me.m_strQuery_LOG) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(Me.grdLOG.CurrentPageIndex - 1, Me.grdLOG.PageCount)
                Me.grdLOG.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_LOG(strErrMsg) = False Then
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

        Private Sub doGotoPage_LOG(ByVal strControlId As String)

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            '获取新的页大小
            Dim intPageIndex As Integer
            intPageIndex = objPulicParameters.getObjectValue(Me.txtLOGPageIndex.Text, 0)
            If intPageIndex <= 0 Then
                intPageIndex = 0
            Else
                intPageIndex -= 1
            End If

            Try
                '获取数据
                If Me.getModuleData_LOG(strErrMsg, Me.m_strQuery_LOG) = False Then
                    GoTo errProc
                End If

                '设置新的页
                Me.grdLOG.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_LOG(strErrMsg) = False Then
                    GoTo errProc
                End If

                '信息同步
                Me.txtLOGPageIndex.Text = (Me.grdLOG.CurrentPageIndex + 1).ToString()

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

        Private Sub doSetPageSize_LOG(ByVal strControlId As String)

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            '获取新的页大小
            Dim intPageSize As Integer
            intPageSize = objPulicParameters.getObjectValue(Me.txtLOGPageSize.Text, 100)
            If intPageSize <= 0 Then intPageSize = 100

            Try
                '获取数据
                If Me.getModuleData_LOG(strErrMsg, Me.m_strQuery_LOG) = False Then
                    GoTo errProc
                End If

                '设置新的页大小
                Me.grdLOG.PageSize = intPageSize

                '刷新网格显示
                If Me.showModuleData_LOG(strErrMsg) = False Then
                    GoTo errProc
                End If

                '信息同步
                Me.txtLOGPageSize.Text = (Me.grdLOG.PageSize).ToString()

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

        Private Sub doSelectAll_LOG(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                If objDataGridProcess.doCheckedDataGridCheckBox(strErrMsg, Me.grdLOG, 0, Me.m_cstrCheckBoxIdInDataGrid_LOG, True) = False Then
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

        Private Sub doDeSelectAll_LOG(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                If objDataGridProcess.doCheckedDataGridCheckBox(strErrMsg, Me.grdLOG, 0, Me.m_cstrCheckBoxIdInDataGrid_LOG, False) = False Then
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

        Private Sub doSearch_LOG(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '搜索数据
                If Me.searchModuleData_LOG(strErrMsg) = False Then
                    GoTo errProc
                End If

                '刷新网格显示
                If Me.showModuleData_LOG(strErrMsg) = False Then
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

        Private Sub lnkCZLOGMoveFirst_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZLOGMoveFirst.Click
            Me.doMoveFirst_LOG("lnkCZLOGMoveFirst")
        End Sub

        Private Sub lnkCZLOGMoveLast_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZLOGMoveLast.Click
            Me.doMoveLast_LOG("lnkCZLOGMoveLast")
        End Sub

        Private Sub lnkCZLOGMoveNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZLOGMoveNext.Click
            Me.doMoveNext_LOG("lnkCZLOGMoveNext")
        End Sub

        Private Sub lnkCZLOGMovePrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZLOGMovePrev.Click
            Me.doMovePrevious_LOG("lnkCZLOGMovePrev")
        End Sub

        Private Sub lnkCZLOGGotoPage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZLOGGotoPage.Click
            Me.doGotoPage_LOG("lnkCZLOGGotoPage")
        End Sub

        Private Sub lnkCZLOGSetPageSize_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZLOGSetPageSize.Click
            Me.doSetPageSize_LOG("lnkCZLOGSetPageSize")
        End Sub

        Private Sub lnkCZLOGSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZLOGSelectAll.Click
            Me.doSelectAll_LOG("lnkCZLOGSelectAll")
        End Sub

        Private Sub lnkCZLOGDeSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZLOGDeSelectAll.Click
            Me.doDeSelectAll_LOG("lnkCZLOGDeSelectAll")
        End Sub

        Private Sub btnLOGSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLOGSearch.Click
            Me.doSearch_LOG("btnLOGSearch")
        End Sub









        Private Sub doClose(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                Dim strSessionId As String
                Dim strUrl As String
                If Me.m_blnInterface = True Then
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

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim strTable As String = Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_VT_B_AUDITSJLOG
            Dim objsystemAppManager As New Xydc.Platform.BusinessFacade.systemAppManager
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile

            Try
                '获取数据集
                If Me.getModuleData_LOG(strErrMsg, Me.m_strQuery_LOG) = False Then
                    GoTo errProc
                End If
                If Me.m_objDataSet_LOG.Tables(strTable) Is Nothing Then
                    strErrMsg = "错误：还未获取数据！"
                    GoTo errProc
                End If
                With Me.m_objDataSet_LOG.Tables(strTable)
                    If .Rows.Count < 1 Then
                        strErrMsg = "错误：没有数据！"
                        GoTo errProc
                    End If
                End With

                '检查模版文件
                Dim strMBURL As String = Request.ApplicationPath + Me.m_cstrExcelMBRelativePathToAppRoot + "管理_日志_审计管理员审计日志一览表.xls"
                Dim strMBLOC As String = Server.MapPath(strMBURL)
                Dim blnFound As Boolean
                If objBaseLocalFile.doFileExisted(strErrMsg, strMBLOC, blnFound) = False Then
                    GoTo errProc
                End If
                If blnFound = False Then
                    strErrMsg = "错误：[" + strMBLOC + "]不存在！"
                    GoTo errProc
                End If

                '备份模版文件到缓存目录
                Dim strTempPath As String = Request.ApplicationPath + Me.m_cstrPrintCacheRelativePathToAppRoot
                Dim strTempFile As String
                strTempPath = Server.MapPath(strTempPath)
                If objBaseLocalFile.doCopyToTempFile(strErrMsg, strMBLOC, strTempPath, strTempFile) = False Then
                    GoTo errProc
                End If
                Dim strTempSpec As String
                strTempSpec = objBaseLocalFile.doMakePath(strTempPath, strTempFile)

                '输出数据
                If objsystemAppManager.doExportToExcel(strErrMsg, Me.m_objDataSet_LOG, strTempSpec) = False Then
                    GoTo errProc
                End If

                '显示Excel
                Dim strTempUrl As String = Request.ApplicationPath + Me.m_cstrPrintCacheRelativePathToAppRoot + strTempFile
                objMessageProcess.doOpenUrl(Me.popMessageObject, strTempUrl, "_blank", "titlebar=yes,menubar=yes,resizable=yes,scrollbars=yes,status=yes")

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.BusinessFacade.systemAppManager.SafeRelease(objsystemAppManager)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.BusinessFacade.systemAppManager.SafeRelease(objsystemAppManager)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
            Me.doClose("btnClose")
        End Sub

        Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
            Me.doPrint("btnPrint")
        End Sub

    End Class
End Namespace