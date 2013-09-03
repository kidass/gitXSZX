Imports System.Web.Security

Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.web
    ' 类名    ：xtgl_mkgl
    ' 
    ' 调用性质：
    '     独立运行
    '
    ' 功能描述： 
    '   　应用模块管理
    '----------------------------------------------------------------

    Partial Public Class xtgl_mkgl
        Inherits Xydc.Platform.web.PageBase


        '----------------------------------------------------------------
        '模块私用参数
        '----------------------------------------------------------------
        '本模块相对image的相对路径
        Private m_cstrRelativePathToImage As String = "../../"

        '----------------------------------------------------------------
        '模块授权参数
        '----------------------------------------------------------------
        Private m_cstrPrevilegeParamPrefix As String = "xtgl_mkgl_previlege_param"
        Private m_blnPrevilegeParams(6) As Boolean

        '----------------------------------------------------------------
        '模块现场保留参数，恢复完成后立即释放session资源
        '----------------------------------------------------------------
        Private m_objSaveScence As Xydc.Platform.BusinessFacade.IMXtglMkgl
        Private m_blnSaveScence As Boolean

        '----------------------------------------------------------------
        '模块接口参数
        '----------------------------------------------------------------
        Private m_objInterface As Xydc.Platform.BusinessFacade.IXtglMkgl
        Private m_blnInterface As Boolean

        '----------------------------------------------------------------
        '与数据网格grdObject相关的参数
        '----------------------------------------------------------------
        '网格中模板列中的控件ID
        Private Const m_cstrCheckBoxIdInDataGrid_Object As String = "chkObject"
        '包含网格的DIV对象ID
        Private Const m_cstrDataGridInDIV_Object As String = "divObject"
        '网格要锁定的列数
        Private m_intFixedColumns_Object As Integer

        '----------------------------------------------------------------
        '要访问的数据
        '----------------------------------------------------------------
        Private m_objDataSet_TreeView As Xydc.Platform.Common.Data.AppManagerData
        Private m_objDataSet_Object As Xydc.Platform.Common.Data.AppManagerData
        Private m_strQuery_Object As String '记录m_objDataSet_Object搜索串
        Private m_intRows_Object As Integer '记录m_objDataSet_Object的DefaultView记录数







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

            Xydc.Platform.BusinessFacade.systemAppManager.SafeRelease(objsystemAppManager)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objMokuaiQXData)

            getPrevilegeParams = True
            Exit Function
errProc:
            Xydc.Platform.BusinessFacade.systemAppManager.SafeRelease(objsystemAppManager)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
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
                    Me.htxtObjectQuery.Value = .htxtObjectQuery
                    Me.htxtObjectRows.Value = .htxtObjectRows
                    Me.htxtObjectSort.Value = .htxtObjectSort
                    Me.htxtObjectSortColumnIndex.Value = .htxtObjectSortColumnIndex
                    Me.htxtObjectSortType.Value = .htxtObjectSortType

                    Me.htxtDivLeftBody.Value = .htxtDivLeftBody
                    Me.htxtDivTopBody.Value = .htxtDivTopBody
                    Me.htxtDivLeftObject.Value = .htxtDivLeftObject
                    Me.htxtDivTopObject.Value = .htxtDivTopObject

                    Me.txtPageIndex.Text = .txtPageIndex
                    Me.txtPageSize.Text = .txtPageSize

                    Me.txtSearchDM.Text = .txtSearchDM
                    Me.txtSearchMC.Text = .txtSearchMC
                    Me.txtSearchSM.Text = .txtSearchSM
                    Me.txtSearchJBMin.Text = .txtSearchJBMin
                    Me.txtSearchJBMax.Text = .txtSearchJBMax

                    Try
                        Me.grdObject.PageSize = .grdObjectPageSize
                    Catch ex As Exception
                    End Try
                    Try
                        Me.grdObject.CurrentPageIndex = .grdObjectCurrentPageIndex
                    Catch ex As Exception
                    End Try
                    Try
                        Me.grdObject.SelectedIndex = .grdObjectSelectedIndex
                    Catch ex As Exception
                    End Try

                    '恢复tvwObject
                    Dim objTreeNode As Microsoft.Web.UI.WebControls.TreeNode
                    Dim strNodeIndex As String = .SelectedNodeIndex
                    strNodeIndex = strNodeIndex.Trim()
                    objTreeNode = Me.tvwObject.GetNodeFromIndex(strNodeIndex)
                    If Not (objTreeNode Is Nothing) Then
                        Me.tvwObject.SelectedNodeIndex = strNodeIndex
                    End If
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

            Dim objRadioButtonListProcess As New Xydc.Platform.web.RadioButtonListProcess

            Dim strSessionId As String = ""

            saveModuleInformation = ""

            Try
                '创建SessionId
                With New Xydc.Platform.Common.Utilities.PulicParameters
                    strSessionId = .getNewGuid()
                End With
                If strSessionId = "" Then
                    Exit Try
                End If

                '创建对象
                Me.m_objSaveScence = New Xydc.Platform.BusinessFacade.IMXtglMkgl

                '保存现场信息
                With Me.m_objSaveScence
                    .htxtObjectQuery = Me.htxtObjectQuery.Value
                    .htxtObjectRows = Me.htxtObjectRows.Value
                    .htxtObjectSort = Me.htxtObjectSort.Value
                    .htxtObjectSortColumnIndex = Me.htxtObjectSortColumnIndex.Value
                    .htxtObjectSortType = Me.htxtObjectSortType.Value

                    .htxtDivLeftBody = Me.htxtDivLeftBody.Value
                    .htxtDivTopBody = Me.htxtDivTopBody.Value
                    .htxtDivLeftObject = Me.htxtDivLeftObject.Value
                    .htxtDivTopObject = Me.htxtDivTopObject.Value

                    .txtPageIndex = Me.txtPageIndex.Text
                    .txtPageSize = Me.txtPageSize.Text

                    .txtSearchDM = Me.txtSearchDM.Text
                    .txtSearchMC = Me.txtSearchMC.Text
                    .txtSearchSM = Me.txtSearchSM.Text
                    .txtSearchJBMin = Me.txtSearchJBMin.Text
                    .txtSearchJBMax = Me.txtSearchJBMax.Text

                    .grdObjectPageSize = Me.grdObject.PageSize
                    .grdObjectCurrentPageIndex = Me.grdObject.CurrentPageIndex
                    .grdObjectSelectedIndex = Me.grdObject.SelectedIndex

                    .SelectedNodeIndex = Me.tvwObject.SelectedNodeIndex
                End With

                '缓存对象
                Session.Add(strSessionId, Me.m_objSaveScence)

            Catch ex As Exception
            End Try

            Xydc.Platform.web.RadioButtonListProcess.SafeRelease(objRadioButtonListProcess)

            saveModuleInformation = strSessionId
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 从调用模块中获取数据
        '----------------------------------------------------------------
        Private Function getDataFromCallModule( _
            ByRef strErrMsg As String) As Boolean

            Try
                If Me.IsPostBack = True Then
                    Exit Try
                End If

                '=================================================================
                Dim objIXtglMkglInfo As Xydc.Platform.BusinessFacade.IXtglMkglInfo
                Try
                    objIXtglMkglInfo = CType(Session.Item(Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.OSessionId)), Xydc.Platform.BusinessFacade.IXtglMkglInfo)
                Catch ex As Exception
                    objIXtglMkglInfo = Nothing
                End Try
                If Not (objIXtglMkglInfo Is Nothing) Then
                    '释放资源
                    Session.Remove(Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.OSessionId))
                    objIXtglMkglInfo.Dispose()
                    objIXtglMkglInfo = Nothing
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
                    m_objInterface = CType(objTemp, Xydc.Platform.BusinessFacade.IXtglMkgl)
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
                    Try
                        Me.m_objSaveScence = CType(Session.Item(strSessionId), Xydc.Platform.BusinessFacade.IMXtglMkgl)
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
                With objPulicParameters
                    '记录m_objDataSet_的DefaultView记录数
                    Me.m_intRows_Object = .getObjectValue(Me.htxtObjectRows.Value, 0)
                    Me.m_strQuery_Object = Me.htxtObjectQuery.Value
                    Me.m_intFixedColumns_Object = .getObjectValue(Me.htxtObjectFixed.Value, 0)
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
        ' 获取grdObject的搜索条件(默认表前缀a.)
        '     strErrMsg      ：返回错误信息
        '     strQuery       ：返回的搜索条件
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function getQueryString_Object( _
            ByRef strErrMsg As String, _
            ByRef strQuery As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            getQueryString_Object = False
            strQuery = ""

            Try
                '按模块代码搜索
                Dim strDM As String
                strDM = "a." + Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKDM
                If Me.txtSearchDM.Text.Length > 0 Then Me.txtSearchDM.Text = Me.txtSearchDM.Text.Trim()
                If Me.txtSearchDM.Text <> "" Then
                    If strQuery = "" Then
                        strQuery = strDM + " like '" + Me.txtSearchDM.Text + "%'"
                    Else
                        strQuery = strQuery + " and " + strDM + " like '" + Me.txtSearchDM.Text + "%'"
                    End If
                End If

                '按模块名称搜索
                Dim strMC As String
                strMC = "a." + Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKMC
                If Me.txtSearchMC.Text.Length > 0 Then Me.txtSearchMC.Text = Me.txtSearchMC.Text.Trim()
                If Me.txtSearchMC.Text <> "" Then
                    Me.txtSearchMC.Text = objPulicParameters.getNewSearchString(Me.txtSearchMC.Text)
                    If strQuery = "" Then
                        strQuery = strMC + " like '" + Me.txtSearchMC.Text + "%'"
                    Else
                        strQuery = strQuery + " and " + strMC + " like '" + Me.txtSearchMC.Text + "%'"
                    End If
                End If

                '按模块说明搜索
                Dim strSM As String
                strSM = "a." + Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKSM
                If Me.txtSearchSM.Text.Length > 0 Then Me.txtSearchSM.Text = Me.txtSearchSM.Text.Trim()
                If Me.txtSearchSM.Text <> "" Then
                    Me.txtSearchSM.Text = objPulicParameters.getNewSearchString(Me.txtSearchSM.Text)
                    If strQuery = "" Then
                        strQuery = strSM + " like '" + Me.txtSearchSM.Text + "%'"
                    Else
                        strQuery = strQuery + " and " + strSM + " like '" + Me.txtSearchSM.Text + "%'"
                    End If
                End If

                '按人员序号搜索
                Dim strMKJB As String
                Dim intMin As Integer
                Dim intMax As Integer
                strMKJB = "a." + Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKJB
                If Me.txtSearchJBMin.Text.Length > 0 Then Me.txtSearchJBMin.Text = Me.txtSearchJBMin.Text.Trim()
                If Me.txtSearchJBMax.Text.Length > 0 Then Me.txtSearchJBMax.Text = Me.txtSearchJBMax.Text.Trim()
                If Me.txtSearchJBMin.Text <> "" And Me.txtSearchJBMax.Text <> "" Then
                    intMin = objPulicParameters.getObjectValue(Me.txtSearchJBMin.Text, 1)
                    intMax = objPulicParameters.getObjectValue(Me.txtSearchJBMax.Text, 1)
                    If intMin > intMax Then
                        Me.txtSearchJBMin.Text = intMax.ToString()
                        Me.txtSearchJBMax.Text = intMin.ToString()
                    Else
                        Me.txtSearchJBMin.Text = intMin.ToString()
                        Me.txtSearchJBMax.Text = intMax.ToString()
                    End If
                    If strQuery = "" Then
                        strQuery = strMKJB + " between " + Me.txtSearchJBMin.Text + " and " + Me.txtSearchJBMax.Text
                    Else
                        strQuery = strQuery + " and " + strMKJB + " between " + Me.txtSearchJBMin.Text + " and " + Me.txtSearchJBMax.Text
                    End If
                ElseIf Me.txtSearchJBMin.Text <> "" Then
                    intMin = objPulicParameters.getObjectValue(Me.txtSearchJBMin.Text, 1)
                    Me.txtSearchJBMin.Text = intMin.ToString()
                    If strQuery = "" Then
                        strQuery = strMKJB + " >= " + Me.txtSearchJBMin.Text
                    Else
                        strQuery = strQuery + " and " + strMKJB + " >= " + Me.txtSearchJBMin.Text
                    End If
                ElseIf Me.txtSearchJBMax.Text <> "" Then
                    intMax = objPulicParameters.getObjectValue(Me.txtSearchJBMax.Text, 1)
                    Me.txtSearchJBMax.Text = intMax.ToString()
                    If strQuery = "" Then
                        strQuery = strMKJB + " <= " + Me.txtSearchJBMax.Text
                    Else
                        strQuery = strQuery + " and " + strMKJB + " <= " + Me.txtSearchJBMax.Text
                    End If
                Else
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            getQueryString_Object = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取grdObject要显示的数据信息
        '     strErrMsg             ：返回错误信息
        '     strWhere              ：搜索条件
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function getModuleData_Object( _
            ByRef strErrMsg As String, _
            ByVal strWhere As String) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_YINGYONGXITONG_MOKUAI
            Dim objsystemAppManager As New Xydc.Platform.BusinessFacade.systemAppManager
            Dim objTreeviewProcess As New Xydc.Platform.web.TreeviewProcess

            getModuleData_Object = False

            Try
                '根据tvwObject获取要显示的模块代码
                Dim objTreeNode As Microsoft.Web.UI.WebControls.TreeNode
                Dim strMKDM As String = ""
                objTreeNode = Me.tvwObject.GetNodeFromIndex(Me.tvwObject.SelectedNodeIndex)
                If Not (objTreeNode Is Nothing) Then
                    strMKDM = objTreeviewProcess.getCodeValueFromNodeId(objTreeNode.ID)
                End If

                '备份Sort字符串
                Dim strSort As String
                strSort = Me.htxtObjectSort.Value
                If strSort.Length > 0 Then strSort = strSort.Trim

                '释放资源
                If Not (Me.m_objDataSet_Object Is Nothing) Then
                    Me.m_objDataSet_Object.Dispose()
                    Me.m_objDataSet_Object = Nothing
                End If

                '重新检索数据
                If objsystemAppManager.getMokuaiData(strErrMsg, MyBase.UserId, MyBase.UserPassword, strMKDM, strWhere, Me.m_objDataSet_Object) = False Then
                    GoTo errProc
                End If

                '恢复Sort字符串
                With Me.m_objDataSet_Object.Tables(strTable)
                    .DefaultView.Sort = strSort
                End With

                '缓存参数
                With Me.m_objDataSet_Object.Tables(strTable)
                    Me.htxtObjectRows.Value = .DefaultView.Count.ToString()
                    Me.m_intRows_Object = .DefaultView.Count
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.BusinessFacade.systemAppManager.SafeRelease(objsystemAppManager)
            Xydc.Platform.web.TreeviewProcess.SafeRelease(objTreeviewProcess)

            getModuleData_Object = True
            Exit Function

errProc:
            Xydc.Platform.BusinessFacade.systemAppManager.SafeRelease(objsystemAppManager)
            Xydc.Platform.web.TreeviewProcess.SafeRelease(objTreeviewProcess)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据屏幕搜索条件搜索grdObject数据
        '     strErrMsg             ：返回错误信息
        ' 返回
        '     True                  ：成功
        '     False                 ：失败
        '----------------------------------------------------------------
        Private Function searchModuleData_Object( _
            ByRef strErrMsg As String) As Boolean

            searchModuleData_Object = False

            Try
                '获取搜索字符串
                Dim strQuery As String
                If Me.getQueryString_Object(strErrMsg, strQuery) = False Then
                    GoTo errProc
                End If

                '搜索数据
                If Me.getModuleData_Object(strErrMsg, strQuery) = False Then
                    GoTo errProc
                End If

                '记录搜索字符串
                Me.m_strQuery_Object = strQuery
                Me.htxtObjectQuery.Value = Me.m_strQuery_Object

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            searchModuleData_Object = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示grdObject的数据
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showDataGridInfo_Object( _
            ByRef strErrMsg As String) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_YINGYONGXITONG_MOKUAI
            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess

            showDataGridInfo_Object = False

            '获取系统保存的网格排序数据
            Dim intSortColumnIndex As Integer
            intSortColumnIndex = objPulicParameters.getObjectValue(Me.htxtObjectSortColumnIndex.Value, -1)
            Dim objSortType As Xydc.Platform.Common.Utilities.PulicParameters.enumSortType
            Try
                objSortType = CType(Me.htxtObjectSortType.Value, Xydc.Platform.Common.Utilities.PulicParameters.enumSortType)
            Catch ex As Exception
                objSortType = Xydc.Platform.Common.Utilities.PulicParameters.enumSortType.None
            End Try

            '网格显示处理
            Try
                '在获取数据时已经恢复了RowFilter、Sort的现场
                '设置数据源
                If Me.m_objDataSet_Object Is Nothing Then
                    Me.grdObject.DataSource = Nothing
                Else
                    With Me.m_objDataSet_Object.Tables(strTable)
                        Me.grdObject.DataSource = .DefaultView
                    End With
                End If

                '调整网格参数
                Dim intCount As Integer
                Try
                    With Me.m_objDataSet_Object.Tables(strTable)
                        intCount = .DefaultView.Count
                    End With
                Catch ex As Exception
                    intCount = 0
                End Try
                If objDataGridProcess.onBeforeDataGridBind(strErrMsg, Me.grdObject, intCount) = False Then
                    GoTo errProc
                End If

                '恢复列标题中的排序信息
                If intSortColumnIndex >= 0 Then
                    objDataGridProcess.doClearSortCharInDataGridHead(Me.grdObject)
                    With Me.grdObject.Columns(intSortColumnIndex)
                        .HeaderText = objDataGridProcess.getColumnSortHeadString(.HeaderText, objSortType)
                    End With
                End If

                '绑定数据
                Me.grdObject.DataBind()

                '----------------------------------------------------------------
                '因为这些信息是非绑定的，所以下面的操作必须等绑定完成后执行！！！
                '一旦在后续处理中执行了DataBind，则信息会丢失！！！
                '----------------------------------------------------------------
                '恢复网格中的CheckBox状态
                If objDataGridProcess.doRestoreDataGridCheckBoxStatus(strErrMsg, Me.grdObject, Request, 0, Me.m_cstrCheckBoxIdInDataGrid_Object) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)

            showDataGridInfo_Object = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示grdObject及相关信息
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showModuleData_Object( _
            ByRef strErrMsg As String) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_YINGYONGXITONG_MOKUAI
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess

            showModuleData_Object = False

            Try
                '显示网格信息
                If Me.showDataGridInfo_Object(strErrMsg) = False Then
                    GoTo errProc
                End If

                '显示与网格紧密相关的操作或信息提示
                Dim intCount As Integer
                Try
                    With Me.m_objDataSet_Object.Tables(strTable).DefaultView
                        intCount = .Count
                    End With
                Catch ex As Exception
                    intCount = 0
                End Try
                '显示网格位置信息
                Me.lblGridLocInfo.Text = objDataGridProcess.getDataGridLocation(Me.grdObject, intCount)
                '显示页面浏览功能
                Me.lnkCZMoveFirst.Enabled = objDataGridProcess.canDoMoveFirstPage(Me.grdObject, intCount)
                Me.lnkCZMoveLast.Enabled = objDataGridProcess.canDoMoveLastPage(Me.grdObject, intCount)
                Me.lnkCZMovePrev.Enabled = objDataGridProcess.canDoMovePreviousPage(Me.grdObject, intCount)
                Me.lnkCZMoveNext.Enabled = objDataGridProcess.canDoMoveNextPage(Me.grdObject, intCount)
                '显示相关操作
                Dim blnEnabled As Boolean
                If intCount < 1 Then
                    blnEnabled = False
                Else
                    blnEnabled = True
                End If
                Me.lnkCZDeSelectAll.Enabled = blnEnabled
                Me.lnkCZSelectAll.Enabled = blnEnabled
                Me.lnkCZGotoPage.Enabled = blnEnabled
                Me.lnkCZSetPageSize.Enabled = blnEnabled

                '显示命令
                Me.lnkMLSelect.Enabled = Me.m_blnPrevilegeParams(1)
                Me.lnkMLAddNewTJ.Enabled = Me.m_blnPrevilegeParams(2)
                Me.lnkMLAddNewXJ.Enabled = Me.m_blnPrevilegeParams(3)
                Me.lnkMLUpdate.Enabled = Me.m_blnPrevilegeParams(4)
                Me.lnkMLDelete.Enabled = Me.m_blnPrevilegeParams(5)
                Me.lnkMLRefresh.Enabled = Me.m_blnPrevilegeParams(6)
                Me.lnkMLClose.Enabled = True

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)

            showModuleData_Object = True
            Exit Function

errProc:
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取tvwObject要显示的数据信息
        '     strErrMsg             ：返回错误信息
        '     strWhere              ：搜索条件
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function getModuleData_TreeView( _
            ByRef strErrMsg As String, _
            ByVal strWhere As String) As Boolean

            Dim objsystemAppManager As New Xydc.Platform.BusinessFacade.systemAppManager

            getModuleData_TreeView = False

            Try
                '释放资源
                If Not (Me.m_objDataSet_TreeView Is Nothing) Then
                    Me.m_objDataSet_TreeView.Dispose()
                    Me.m_objDataSet_TreeView = Nothing
                End If

                '重新检索数据
                If objsystemAppManager.getMokuaiData(strErrMsg, MyBase.UserId, MyBase.UserPassword, strWhere, Me.m_objDataSet_TreeView) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.BusinessFacade.systemAppManager.SafeRelease(objsystemAppManager)

            getModuleData_TreeView = True
            Exit Function

errProc:
            Xydc.Platform.BusinessFacade.systemAppManager.SafeRelease(objsystemAppManager)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示tvwObject的数据
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showTreeViewInfo_Object( _
            ByRef strErrMsg As String) As Boolean

            Dim objTreeviewProcess As New Xydc.Platform.web.TreeviewProcess

            showTreeViewInfo_Object = False

            Try
                If objTreeviewProcess.doDisplayTreeViewAll(strErrMsg, Me.tvwObject, _
                    Me.m_objDataSet_TreeView.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_YINGYONGXITONG_MOKUAI), _
                    Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKDM, _
                    Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKMC, _
                    Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKJB, _
                    False, True) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.TreeviewProcess.SafeRelease(objTreeviewProcess)

            showTreeViewInfo_Object = True
            Exit Function

errProc:
            Xydc.Platform.web.TreeviewProcess.SafeRelease(objTreeviewProcess)
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
                    '根据接口参数设置
                Catch ex As Exception
                End Try

                '显示Pannel
                Me.panelMain.Visible = True
                Me.panelError.Visible = Not Me.panelMain.Visible

                '执行键转译(不论是否是“回发”)
                Try
                    With New Xydc.Platform.web.ControlProcess
                        .doTranslateKey(Me.txtPageIndex)
                        .doTranslateKey(Me.txtPageSize)
                        .doTranslateKey(Me.txtSearchDM)
                        .doTranslateKey(Me.txtSearchMC)
                        .doTranslateKey(Me.txtSearchSM)
                        .doTranslateKey(Me.txtSearchJBMin)
                        .doTranslateKey(Me.txtSearchJBMax)
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
            End If

            If Me.IsPostBack = False Then
                If Me.getModuleData_Object(strErrMsg, Me.m_strQuery_Object) = False Then
                    GoTo errProc
                End If
                If Me.showModuleData_Object(strErrMsg) = False Then
                    GoTo errProc
                End If
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

            '显示treeview
            If Me.IsPostBack = False Then
                If Me.getModuleData_TreeView(strErrMsg, "") = False Then
                    GoTo errProc
                End If
                If Me.showTreeViewInfo_Object(strErrMsg) = False Then
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
        Private Sub tvwObject_SelectedIndexChange(ByVal sender As Object, ByVal e As Microsoft.Web.UI.WebControls.TreeViewSelectEventArgs) Handles tvwObject.SelectedIndexChange

            Dim objTreeviewProcess As New Xydc.Platform.web.TreeviewProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                Me.tvwObject.SelectedNodeIndex = e.NewNode

                If Me.getModuleData_Object(strErrMsg, Me.m_strQuery_Object) = False Then
                    GoTo errProc
                End If
                If Me.showModuleData_Object(strErrMsg) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.TreeviewProcess.SafeRelease(objTreeviewProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.web.TreeviewProcess.SafeRelease(objTreeviewProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        '实现对grd网格行、列的固定
        Sub grdObject_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles grdObject.ItemDataBound

            Dim intCells As Integer
            Dim i As Integer

            Try
                If e.Item.ItemIndex < 0 Then
                    '标题行,输出标题锁定一般属性
                    intCells = e.Item.Cells.Count
                    For i = 0 To intCells - 1 Step 1
                        e.Item.Cells(i).Attributes.CssStyle.Add("top", "expression(" + Me.m_cstrDataGridInDIV_Object + ".scrollTop)")
                    Next
                End If
                If Me.m_intFixedColumns_Object > 0 Then
                    '锁定列
                    For i = 0 To Me.m_intFixedColumns_Object - 1 Step 1
                        e.Item.Cells(i).CssClass = Me.grdObject.ID + "Locked"
                    Next
                End If
            Catch ex As Exception
            End Try

            Exit Sub

        End Sub

        Private Sub grdObject_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdObject.SelectedIndexChanged

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '显示记录位置
                With New Xydc.Platform.web.DataGridProcess
                    Me.lblGridLocInfo.Text = .getDataGridLocation(Me.grdObject, Me.m_intRows_Object)
                End With
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

        Private Sub grdObject_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles grdObject.SortCommand

            Dim strTable As String = Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_YINGYONGXITONG_MOKUAI
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
                If Me.getModuleData_Object(strErrMsg, Me.m_strQuery_Object) = False Then
                    GoTo errProc
                End If

                '获取原排序命令
                strOldCommand = Me.m_objDataSet_Object.Tables(strTable).DefaultView.Sort

                '获取要执行的排序命令
                objDataGridProcess.getColumnSortCommand(strErrMsg, strOldCommand, e.SortExpression, strFinalCommand, objenumSortType)
                If strErrMsg <> "" Then
                    GoTo errProc
                End If

                '执行排序
                Me.m_objDataSet_Object.Tables(strTable).DefaultView.Sort = strFinalCommand

                '获取排序列的列索引
                objDataGridItem = CType(e.CommandSource, System.Web.UI.WebControls.DataGridItem)
                strUniqueId = Request.Form("__EVENTTARGET")
                intColumnIndex = objDataGridProcess.getColumnIndexByUniqueIdInRow(objDataGridItem, strUniqueId)

                '保存排序信息
                Me.htxtObjectSortColumnIndex.Value = intColumnIndex.ToString()
                Me.htxtObjectSortType.Value = CType(objenumSortType, Integer).ToString()
                Me.htxtObjectSort.Value = strFinalCommand

                '重新显示数据
                If Me.showModuleData_Object(strErrMsg) = False Then
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

        Private Sub doMoveFirst_Object(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_Object(strErrMsg, Me.m_strQuery_Object) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(0, Me.grdObject.PageCount)
                Me.grdObject.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_Object(strErrMsg) = False Then
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

        Private Sub doMoveLast_Object(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_Object(strErrMsg, Me.m_strQuery_Object) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(Me.grdObject.PageCount - 1, Me.grdObject.PageCount)
                Me.grdObject.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_Object(strErrMsg) = False Then
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

        Private Sub doMoveNext_Object(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_Object(strErrMsg, Me.m_strQuery_Object) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(Me.grdObject.CurrentPageIndex + 1, Me.grdObject.PageCount)
                Me.grdObject.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_Object(strErrMsg) = False Then
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

        Private Sub doMovePrevious_Object(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_Object(strErrMsg, Me.m_strQuery_Object) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(Me.grdObject.CurrentPageIndex - 1, Me.grdObject.PageCount)
                Me.grdObject.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_Object(strErrMsg) = False Then
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

        Private Sub doGotoPage_Object(ByVal strControlId As String)

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
                If Me.getModuleData_Object(strErrMsg, Me.m_strQuery_Object) = False Then
                    GoTo errProc
                End If

                '设置新的页
                Me.grdObject.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_Object(strErrMsg) = False Then
                    GoTo errProc
                End If

                '信息同步
                Me.txtPageIndex.Text = (Me.grdObject.CurrentPageIndex + 1).ToString()

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

        Private Sub doSetPageSize_Object(ByVal strControlId As String)

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            '获取新的页大小
            Dim intPageSize As Integer
            intPageSize = objPulicParameters.getObjectValue(Me.txtPageSize.Text, 100)
            If intPageSize <= 0 Then intPageSize = 100

            Try
                '获取数据
                If Me.getModuleData_Object(strErrMsg, Me.m_strQuery_Object) = False Then
                    GoTo errProc
                End If

                '设置新的页大小
                Me.grdObject.PageSize = intPageSize

                '刷新网格显示
                If Me.showModuleData_Object(strErrMsg) = False Then
                    GoTo errProc
                End If

                '信息同步
                Me.txtPageSize.Text = (Me.grdObject.PageSize).ToString()

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

        Private Sub doSelectAll_Object(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                If objDataGridProcess.doCheckedDataGridCheckBox(strErrMsg, Me.grdObject, 0, Me.m_cstrCheckBoxIdInDataGrid_Object, True) = False Then
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

        Private Sub doDeSelectAll_Object(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                If objDataGridProcess.doCheckedDataGridCheckBox(strErrMsg, Me.grdObject, 0, Me.m_cstrCheckBoxIdInDataGrid_Object, False) = False Then
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

        Private Sub doSearch_Object(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '搜索数据
                If Me.searchModuleData_Object(strErrMsg) = False Then
                    GoTo errProc
                End If

                '刷新网格显示
                If Me.showModuleData_Object(strErrMsg) = False Then
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
            Me.doMoveFirst_Object("lnkCZMoveFirst")
        End Sub

        Private Sub lnkCZMoveLast_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZMoveLast.Click
            Me.doMoveLast_Object("lnkCZMoveLast")
        End Sub

        Private Sub lnkCZMoveNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZMoveNext.Click
            Me.doMoveNext_Object("lnkCZMoveNext")
        End Sub

        Private Sub lnkCZMovePrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZMovePrev.Click
            Me.doMovePrevious_Object("lnkCZMovePrev")
        End Sub

        Private Sub lnkCZGotoPage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZGotoPage.Click
            Me.doGotoPage_Object("lnkCZGotoPage")
        End Sub

        Private Sub lnkCZSetPageSize_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZSetPageSize.Click
            Me.doSetPageSize_Object("lnkCZSetPageSize")
        End Sub

        Private Sub lnkCZSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZSelectAll.Click
            Me.doSelectAll_Object("lnkCZSelectAll")
        End Sub

        Private Sub lnkCZDeSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZDeSelectAll.Click
            Me.doDeSelectAll_Object("lnkCZDeSelectAll")
        End Sub

        Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            Me.doSearch_Object("btnSearch")
        End Sub









        '----------------------------------------------------------------
        '模块特殊操作处理器
        '----------------------------------------------------------------
        Private Sub doRefresh(ByVal strControlId As String)
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '显示树
                If Me.getModuleData_TreeView(strErrMsg, "") = False Then
                    GoTo errProc
                End If
                If Me.showTreeViewInfo_Object(strErrMsg) = False Then
                    GoTo errProc
                End If

                '显示网格
                If Me.getModuleData_Object(strErrMsg, Me.m_strQuery_Object) = False Then
                    GoTo errProc
                End If
                If Me.showModuleData_Object(strErrMsg) = False Then
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

        Private Sub doOpenMokuai(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '检查
                Dim strMKDM As String
                If Me.grdObject.SelectedIndex < 0 Then
                    strErrMsg = "错误：未选定模块！"
                    GoTo errProc
                End If
                Dim intColIndex As Integer
                Dim intRow As Integer
                intRow = Me.grdObject.SelectedIndex
                With New Xydc.Platform.web.DataGridProcess
                    intColIndex = .getDataGridColumnIndex(Me.grdObject, Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKDM)
                    strMKDM = .getDataGridCellValue(Me.grdObject.Items(intRow), intColIndex)
                End With

                '备份现场参数
                Dim strSessionId As String
                strSessionId = Me.saveModuleInformation()
                If strSessionId = "" Then
                    strErrMsg = "错误：不能保存现场信息！"
                    GoTo errProc
                End If

                '准备调用接口
                Dim objIXtglMkglInfo As Xydc.Platform.BusinessFacade.IXtglMkglInfo
                Dim strUrl As String
                objIXtglMkglInfo = New Xydc.Platform.BusinessFacade.IXtglMkglInfo
                With objIXtglMkglInfo
                    .iEditMode = Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eSelect
                    .iMKDM = strMKDM

                    .iSourceControlId = strControlId
                    If Me.m_blnInterface = False Then
                        strUrl = ""
                        strUrl += Request.Url.AbsolutePath
                        strUrl += "?"
                        strUrl += Xydc.Platform.Common.Utilities.PulicParameters.MSessionId
                        strUrl += "="
                        strUrl += strSessionId
                    Else
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
                    End If
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
                Session.Add(strNewSessionId, objIXtglMkglInfo)

                strUrl = ""
                strUrl += "xtgl_mkgl_info.aspx"
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

        Private Sub doUpdateMokuai(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '检查
                Dim strMKDM As String
                If Me.grdObject.SelectedIndex < 0 Then
                    strErrMsg = "错误：未选定模块！"
                    GoTo errProc
                End If
                Dim intColIndex As Integer
                Dim intRow As Integer
                intRow = Me.grdObject.SelectedIndex
                With New Xydc.Platform.web.DataGridProcess
                    intColIndex = .getDataGridColumnIndex(Me.grdObject, Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKDM)
                    strMKDM = .getDataGridCellValue(Me.grdObject.Items(intRow), intColIndex)
                End With

                '备份现场参数
                Dim strSessionId As String
                strSessionId = Me.saveModuleInformation()
                If strSessionId = "" Then
                    strErrMsg = "错误：不能保存现场信息！"
                    GoTo errProc
                End If

                '准备调用接口
                Dim objIXtglMkglInfo As Xydc.Platform.BusinessFacade.IXtglMkglInfo
                Dim strUrl As String
                objIXtglMkglInfo = New Xydc.Platform.BusinessFacade.IXtglMkglInfo
                With objIXtglMkglInfo
                    .iEditMode = Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eUpdate
                    .iMKDM = strMKDM

                    .iSourceControlId = strControlId
                    If Me.m_blnInterface = False Then
                        strUrl = ""
                        strUrl += Request.Url.AbsolutePath
                        strUrl += "?"
                        strUrl += Xydc.Platform.Common.Utilities.PulicParameters.MSessionId
                        strUrl += "="
                        strUrl += strSessionId
                    Else
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
                    End If
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
                Session.Add(strNewSessionId, objIXtglMkglInfo)

                strUrl = ""
                strUrl += "xtgl_mkgl_info.aspx"
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

        Private Sub doAddNewMokuaiTJ(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '检查
                Dim strPrevMKDM As String = ""
                Dim strMKDM As String = ""
                If Me.tvwObject.SelectedNodeIndex = "" Then
                    strMKDM = ""
                    strPrevMKDM = ""
                Else
                    Dim objTreeNode As Microsoft.Web.UI.WebControls.TreeNode
                    objTreeNode = Me.tvwObject.GetNodeFromIndex(Me.tvwObject.SelectedNodeIndex)
                    If objTreeNode Is Nothing Then
                        strErrMsg = "错误：没有选定模块！"
                        GoTo errProc
                    End If
                    With New Xydc.Platform.web.TreeviewProcess
                        strMKDM = .getCodeValueFromNodeId(objTreeNode.ID)
                    End With
                    strMKDM = strMKDM.Trim()
                    With New Xydc.Platform.Common.Utilities.PulicParameters
                        strPrevMKDM = .getPrevLevelCode(strMKDM, Xydc.Platform.Common.Utilities.PulicParameters.CharFjdmSeparate)
                    End With
                End If

                '备份现场参数
                Dim strSessionId As String
                strSessionId = Me.saveModuleInformation()
                If strSessionId = "" Then
                    strErrMsg = "错误：不能保存现场信息！"
                    GoTo errProc
                End If

                '准备调用接口
                Dim objIXtglMkglInfo As Xydc.Platform.BusinessFacade.IXtglMkglInfo
                Dim strUrl As String
                objIXtglMkglInfo = New Xydc.Platform.BusinessFacade.IXtglMkglInfo
                With objIXtglMkglInfo
                    .iEditMode = Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                    .iMKDM = strMKDM
                    .iSJDM = strPrevMKDM

                    .iSourceControlId = strControlId
                    If Me.m_blnInterface = False Then
                        strUrl = ""
                        strUrl += Request.Url.AbsolutePath
                        strUrl += "?"
                        strUrl += Xydc.Platform.Common.Utilities.PulicParameters.MSessionId
                        strUrl += "="
                        strUrl += strSessionId
                    Else
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
                    End If
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
                Session.Add(strNewSessionId, objIXtglMkglInfo)

                strUrl = ""
                strUrl += "xtgl_mkgl_info.aspx"
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

        Private Sub doAddNewMokuaiXJ(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '检查
                Dim strPrevMKDM As String = ""
                Dim strMKDM As String = ""
                If Me.tvwObject.SelectedNodeIndex = "" Then
                    strMKDM = ""
                    strPrevMKDM = ""
                Else
                    Dim objTreeNode As Microsoft.Web.UI.WebControls.TreeNode
                    objTreeNode = Me.tvwObject.GetNodeFromIndex(Me.tvwObject.SelectedNodeIndex)
                    If objTreeNode Is Nothing Then
                        strErrMsg = "错误：没有选定模块！"
                        GoTo errProc
                    End If
                    With New Xydc.Platform.web.TreeviewProcess
                        strMKDM = .getCodeValueFromNodeId(objTreeNode.ID)
                    End With
                    strMKDM = strMKDM.Trim()
                    strPrevMKDM = strMKDM
                End If

                '备份现场参数
                Dim strSessionId As String
                strSessionId = Me.saveModuleInformation()
                If strSessionId = "" Then
                    strErrMsg = "错误：不能保存现场信息！"
                    GoTo errProc
                End If

                '准备调用接口
                Dim objIXtglMkglInfo As Xydc.Platform.BusinessFacade.IXtglMkglInfo
                Dim strUrl As String
                objIXtglMkglInfo = New Xydc.Platform.BusinessFacade.IXtglMkglInfo
                With objIXtglMkglInfo
                    .iEditMode = Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                    .iMKDM = strMKDM
                    .iSJDM = strPrevMKDM

                    .iSourceControlId = strControlId
                    If Me.m_blnInterface = False Then
                        strUrl = ""
                        strUrl += Request.Url.AbsolutePath
                        strUrl += "?"
                        strUrl += Xydc.Platform.Common.Utilities.PulicParameters.MSessionId
                        strUrl += "="
                        strUrl += strSessionId
                    Else
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
                    End If
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
                Session.Add(strNewSessionId, objIXtglMkglInfo)

                strUrl = ""
                strUrl += "xtgl_mkgl_info.aspx"
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

        Private Sub doDeleteMokuai(ByVal strControlId As String)

            Dim objsystemAppManager As New Xydc.Platform.BusinessFacade.systemAppManager
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
                intRows = Me.grdObject.Items.Count
                If objMessageProcess.isExecuteCode(Request, Me.popMessageObject.UniqueID, intStep) = True Then
                    For i = 0 To intRows - 1 Step 1
                        If objDataGridProcess.isDataGridItemChecked(Me.grdObject.Items(i), 0, Me.m_cstrCheckBoxIdInDataGrid_Object) = True Then
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
                    '逐个删除
                    Dim intColIndex(2) As Integer
                    Dim strMKDM As String
                    Dim strMKMC As String
                    intColIndex(0) = objDataGridProcess.getDataGridColumnIndex(Me.grdObject, Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKDM)
                    intColIndex(1) = objDataGridProcess.getDataGridColumnIndex(Me.grdObject, Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKMC)
                    For i = intRows - 1 To 0 Step -1
                        If objDataGridProcess.isDataGridItemChecked(Me.grdObject.Items(i), 0, Me.m_cstrCheckBoxIdInDataGrid_Object) = True Then
                            '获取模块代码
                            strMKDM = objDataGridProcess.getDataGridCellValue(Me.grdObject.Items(i), intColIndex(0))
                            strMKMC = objDataGridProcess.getDataGridCellValue(Me.grdObject.Items(i), intColIndex(1))

                            '删除处理
                            If objsystemAppManager.doDeleteMokuaiData(strErrMsg, MyBase.UserId, MyBase.UserPassword, strMKDM) = False Then
                                GoTo errProc
                            End If

                            '记录审计日志
                            Xydc.Platform.SystemFramework.ApplicationLog.WriteAuditAQInfo(Request.UserHostAddress, Request.UserHostName, "[" + MyBase.UserId + "]删除了[" + strMKMC + "]应用模块注册信息！")
                        End If
                    Next

                    '刷新显示
                    Me.doRefresh(strControlId)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.BusinessFacade.systemAppManager.SafeRelease(objsystemAppManager)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.BusinessFacade.systemAppManager.SafeRelease(objsystemAppManager)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
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

        Private Sub lnkMLRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkMLRefresh.Click
            Me.doRefresh("lnkMLRefresh")
        End Sub

        Private Sub lnkMLSelect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkMLSelect.Click
            Me.doOpenMokuai("lnkMLSelect")
        End Sub

        Private Sub lnkMLUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkMLUpdate.Click
            Me.doUpdateMokuai("lnkMLUpdate")
        End Sub

        Private Sub lnkMLAddNewTJ_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkMLAddNewTJ.Click
            Me.doAddNewMokuaiTJ("lnkMLAddNewTJ")
        End Sub

        Private Sub lnkMLAddNewXJ_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkMLAddNewXJ.Click
            Me.doAddNewMokuaiXJ("lnkMLAddNewXJ")
        End Sub

        Private Sub lnkMLDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkMLDelete.Click
            Me.doDeleteMokuai("lnkMLDelete")
        End Sub

        Private Sub lnkMLClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkMLClose.Click
            Me.doClose("lnkMLClose")
        End Sub


    End Class
End Namespace