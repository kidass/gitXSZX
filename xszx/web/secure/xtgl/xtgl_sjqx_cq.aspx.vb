Imports System.Web.Security

Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.web
    ' 类名    ：xtgl_sjqx_cq
    ' 
    ' 调用性质：
    '     独立运行
    '
    ' 功能描述： 
    '   　数据库用户的存取控制处理
    '----------------------------------------------------------------

    Partial Public Class xtgl_sjqx_cq
        Inherits Xydc.Platform.web.PageBase

        '----------------------------------------------------------------
        '模块私用参数
        '----------------------------------------------------------------
        '本模块相对image的相对路径
        Private m_cstrRelativePathToImage As String = "../../"

        '----------------------------------------------------------------
        '模块授权参数
        '----------------------------------------------------------------
        Private m_cstrPrevilegeParamPrefix As String = "xtgl_sjqx_previlege_param"
        Private m_blnPrevilegeParams(9) As Boolean

        '----------------------------------------------------------------
        '模块现场保留参数，恢复完成后立即释放session资源
        '----------------------------------------------------------------
        Private m_objSaveScence As Xydc.Platform.BusinessFacade.IMXtglSjqxCq
        Private m_blnSaveScence As Boolean

        '----------------------------------------------------------------
        '模块接口参数
        '----------------------------------------------------------------
        Private m_objInterface As Xydc.Platform.BusinessFacade.IXtglSjqxCq
        Private m_blnInterface As Boolean

        '----------------------------------------------------------------
        '与数据网格grdYR相关的参数
        '----------------------------------------------------------------
        '网格中模板列中的控件ID
        Private Const m_cstrCheckBoxIdInDataGrid_YR As String = "chkYR"
        '包含网格的DIV对象ID
        Private Const m_cstrDataGridInDIV_YR As String = "divYR"
        '网格要锁定的列数
        Private m_intFixedColumns_YR As Integer

        '----------------------------------------------------------------
        '与数据网格grdWR相关的参数
        '----------------------------------------------------------------
        '网格中模板列中的控件ID
        Private Const m_cstrCheckBoxIdInDataGrid_WR As String = "chkWR"
        '包含网格的DIV对象ID
        Private Const m_cstrDataGridInDIV_WR As String = "divWR"
        '网格要锁定的列数
        Private m_intFixedColumns_WR As Integer

        '----------------------------------------------------------------
        '要访问的数据
        '----------------------------------------------------------------
        Private m_objDataSet_YR As Xydc.Platform.Common.Data.CustomerData
        Private m_strQuery_YR As String '记录m_objDataSet_YR搜索串
        Private m_intRows_YR As Integer '记录m_objDataSet_YR的DefaultView记录数
        Private m_objDataSet_WR As Xydc.Platform.Common.Data.CustomerData
        Private m_strQuery_WR As String '记录m_objDataSet_WR搜索串
        Private m_intRows_WR As Integer '记录m_objDataSet_WR的DefaultView记录数







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
                    Me.htxtYRQuery.Value = .htxtYRQuery
                    Me.htxtYRRows.Value = .htxtYRRows
                    Me.htxtYRSort.Value = .htxtYRSort
                    Me.htxtYRSortColumnIndex.Value = .htxtYRSortColumnIndex
                    Me.htxtYRSortType.Value = .htxtYRSortType

                    Me.htxtWRQuery.Value = .htxtWRQuery
                    Me.htxtWRRows.Value = .htxtWRRows
                    Me.htxtWRSort.Value = .htxtWRSort
                    Me.htxtWRSortColumnIndex.Value = .htxtWRSortColumnIndex
                    Me.htxtWRSortType.Value = .htxtWRSortType

                    Me.htxtDivLeftBody.Value = .htxtDivLeftBody
                    Me.htxtDivTopBody.Value = .htxtDivTopBody

                    Me.htxtDivLeftYR.Value = .htxtDivLeftYR
                    Me.htxtDivTopYR.Value = .htxtDivTopYR

                    Me.htxtDivLeftWR.Value = .htxtDivLeftWR
                    Me.htxtDivTopWR.Value = .htxtDivTopWR

                    Me.txtYRPageIndex.Text = .txtYRPageIndex
                    Me.txtYRPageSize.Text = .txtYRPageSize

                    Me.txtWRPageIndex.Text = .txtWRPageIndex
                    Me.txtWRPageSize.Text = .txtWRPageSize

                    Me.txtYRSearchRYDM.Text = .txtYRSearchRYDM
                    Me.txtYRSearchRYMC.Text = .txtYRSearchRYMC
                    Me.txtYRSearchZZMC.Text = .txtYRSearchZZMC
                    Me.txtYRSearchJBMC.Text = .txtYRSearchJBMC
                    Me.txtYRSearchGWLB.Text = .txtYRSearchGWLB

                    Me.txtWRSearchRYDM.Text = .txtWRSearchRYDM
                    Me.txtWRSearchRYMC.Text = .txtWRSearchRYMC
                    Me.txtWRSearchZZMC.Text = .txtWRSearchZZMC
                    Me.txtWRSearchJBMC.Text = .txtWRSearchJBMC
                    Me.txtWRSearchGWLB.Text = .txtWRSearchGWLB

                    Try
                        Me.grdYR.PageSize = .grdYRPageSize
                    Catch ex As Exception
                    End Try
                    Try
                        Me.grdYR.CurrentPageIndex = .grdYRCurrentPageIndex
                    Catch ex As Exception
                    End Try
                    Try
                        Me.grdYR.SelectedIndex = .grdYRSelectedIndex
                    Catch ex As Exception
                    End Try

                    Try
                        Me.grdWR.PageSize = .grdWRPageSize
                    Catch ex As Exception
                    End Try
                    Try
                        Me.grdWR.CurrentPageIndex = .grdWRCurrentPageIndex
                    Catch ex As Exception
                    End Try
                    Try
                        Me.grdWR.SelectedIndex = .grdWRSelectedIndex
                    Catch ex As Exception
                    End Try

                    '恢复tvwServers
                    Dim objAppManagerData As Xydc.Platform.Common.Data.AppManagerData
                    Dim objTreeviewProcess As New Xydc.Platform.web.TreeviewProcess
                    Dim objTreeNode As Microsoft.Web.UI.WebControls.TreeNode
                    Dim strNodeIndex As String = .SelectedNodeIndex
                    Dim strErrMsg As String
                    Dim strIndex As String
                    Dim intCount As Integer
                    Dim i As Integer
                    intCount = objTreeviewProcess.getLevelIndexFromNodeIndex(strNodeIndex)
                    strNodeIndex = strNodeIndex.Trim()
                    For i = 1 To intCount - 1 Step 1
                        strIndex = objTreeviewProcess.getLevelIndexFromNodeIndex(strNodeIndex, i, True)
                        If Me.getModuleData_Server(strErrMsg, strIndex, objAppManagerData) = False Then
                            Exit For
                        End If
                        If objAppManagerData Is Nothing Then Exit For
                        Select Case i
                            Case 1 '展开服务器
                                objTreeNode = Me.tvwServers.Nodes(0)
                                objTreeNode.Expanded = True
                                If objTreeviewProcess.doShowTreeNodeChildren(strErrMsg, objTreeNode, _
                                    objAppManagerData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_FUWUQI), _
                                    Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_FUWUQI_MC, _
                                    Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_FUWUQI_MC, _
                                    False, Microsoft.Web.UI.WebControls.ExpandableValue.CheckOnce, True) = False Then
                                    Exit For
                                End If

                            Case 2 '展开数据库
                                objTreeNode = Me.tvwServers.GetNodeFromIndex(strIndex)
                                If objTreeNode Is Nothing Then Exit For
                                If objTreeviewProcess.doShowTreeNodeChildren(strErrMsg, objTreeNode, _
                                    objAppManagerData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_SHUJUKU), _
                                    Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_SHUJUKU_SJKM, _
                                    Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_SHUJUKU_SJKM, _
                                    False, Microsoft.Web.UI.WebControls.ExpandableValue.Auto, False) = False Then
                                    Exit For
                                End If

                            Case Else
                        End Select
                    Next
                    objTreeNode = Me.tvwServers.GetNodeFromIndex(strNodeIndex)
                    If Not (objTreeNode Is Nothing) Then
                        Me.tvwServers.SelectedNodeIndex = strNodeIndex
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
                Me.m_objSaveScence = New Xydc.Platform.BusinessFacade.IMXtglSjqxCq

                '保存现场信息
                With Me.m_objSaveScence
                    .htxtYRQuery = Me.htxtYRQuery.Value
                    .htxtYRRows = Me.htxtYRRows.Value
                    .htxtYRSort = Me.htxtYRSort.Value
                    .htxtYRSortColumnIndex = Me.htxtYRSortColumnIndex.Value
                    .htxtYRSortType = Me.htxtYRSortType.Value

                    .htxtWRQuery = Me.htxtWRQuery.Value
                    .htxtWRRows = Me.htxtWRRows.Value
                    .htxtWRSort = Me.htxtWRSort.Value
                    .htxtWRSortColumnIndex = Me.htxtWRSortColumnIndex.Value
                    .htxtWRSortType = Me.htxtWRSortType.Value

                    .htxtDivLeftBody = Me.htxtDivLeftBody.Value
                    .htxtDivTopBody = Me.htxtDivTopBody.Value

                    .htxtDivLeftYR = Me.htxtDivLeftYR.Value
                    .htxtDivTopYR = Me.htxtDivTopYR.Value

                    .htxtDivLeftWR = Me.htxtDivLeftWR.Value
                    .htxtDivTopWR = Me.htxtDivTopWR.Value

                    .txtYRPageIndex = Me.txtYRPageIndex.Text
                    .txtYRPageSize = Me.txtYRPageSize.Text

                    .txtWRPageIndex = Me.txtWRPageIndex.Text
                    .txtWRPageSize = Me.txtWRPageSize.Text

                    .txtYRSearchRYDM = Me.txtYRSearchRYDM.Text
                    .txtYRSearchRYMC = Me.txtYRSearchRYMC.Text
                    .txtYRSearchZZMC = Me.txtYRSearchZZMC.Text
                    .txtYRSearchJBMC = Me.txtYRSearchJBMC.Text
                    .txtYRSearchGWLB = Me.txtYRSearchGWLB.Text

                    .txtWRSearchRYDM = Me.txtWRSearchRYDM.Text
                    .txtWRSearchRYMC = Me.txtWRSearchRYMC.Text
                    .txtWRSearchZZMC = Me.txtWRSearchZZMC.Text
                    .txtWRSearchJBMC = Me.txtWRSearchJBMC.Text
                    .txtWRSearchGWLB = Me.txtWRSearchGWLB.Text

                    .grdYRPageSize = Me.grdYR.PageSize
                    .grdYRCurrentPageIndex = Me.grdYR.CurrentPageIndex
                    .grdYRSelectedIndex = Me.grdYR.SelectedIndex

                    .grdWRPageSize = Me.grdWR.PageSize
                    .grdWRCurrentPageIndex = Me.grdWR.CurrentPageIndex
                    .grdWRSelectedIndex = Me.grdWR.SelectedIndex

                    .SelectedNodeIndex = Me.tvwServers.SelectedNodeIndex
                End With

                '缓存对象
                Session.Add(strSessionId, Me.m_objSaveScence)

            Catch ex As Exception
            End Try

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
                    m_objInterface = CType(objTemp, Xydc.Platform.BusinessFacade.IXtglSjqxCq)
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
                        Me.m_objSaveScence = CType(Session.Item(strSessionId), Xydc.Platform.BusinessFacade.IMXtglSjqxCq)
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
                    '记录m_objDataSet_YR的DefaultView记录数
                    Me.m_intRows_YR = .getObjectValue(Me.htxtYRRows.Value, 0)
                    Me.m_strQuery_YR = Me.htxtYRQuery.Value
                    Me.m_intFixedColumns_YR = .getObjectValue(Me.htxtYRFixed.Value, 0)

                    Me.m_intRows_WR = .getObjectValue(Me.htxtWRRows.Value, 0)
                    Me.m_strQuery_WR = Me.htxtWRQuery.Value
                    Me.m_intFixedColumns_WR = .getObjectValue(Me.htxtWRFixed.Value, 0)
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
        ' 获取grdYR的搜索条件(默认表前缀a.)
        '     strErrMsg      ：返回错误信息
        '     strQuery       ：返回的搜索条件
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function getQueryString_YR( _
            ByRef strErrMsg As String, _
            ByRef strQuery As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            getQueryString_YR = False
            strQuery = ""

            Try
                '按人员代码搜索
                Dim strRYDM As String
                strRYDM = "a." + Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYDM
                If Me.txtYRSearchRYDM.Text.Length > 0 Then Me.txtYRSearchRYDM.Text = Me.txtYRSearchRYDM.Text.Trim()
                If Me.txtYRSearchRYDM.Text <> "" Then
                    Me.txtYRSearchRYDM.Text = objPulicParameters.getNewSearchString(Me.txtYRSearchRYDM.Text)
                    If strQuery = "" Then
                        strQuery = strRYDM + " like '" + Me.txtYRSearchRYDM.Text + "%'"
                    Else
                        strQuery = strQuery + " and " + strRYDM + " like '" + Me.txtYRSearchRYDM.Text + "%'"
                    End If
                End If

                '按人员名称搜索
                Dim strRYMC As String
                strRYMC = "a." + Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYMC
                If Me.txtYRSearchRYMC.Text.Length > 0 Then Me.txtYRSearchRYMC.Text = Me.txtYRSearchRYMC.Text.Trim()
                If Me.txtYRSearchRYMC.Text <> "" Then
                    Me.txtYRSearchRYMC.Text = objPulicParameters.getNewSearchString(Me.txtYRSearchRYMC.Text)
                    If strQuery = "" Then
                        strQuery = strRYMC + " like '" + Me.txtYRSearchRYMC.Text + "%'"
                    Else
                        strQuery = strQuery + " and " + strRYMC + " like '" + Me.txtYRSearchRYMC.Text + "%'"
                    End If
                End If

                '按人员名称搜索
                Dim strZZMC As String
                strZZMC = "a." + Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_ZZMC
                If Me.txtYRSearchZZMC.Text.Length > 0 Then Me.txtYRSearchZZMC.Text = Me.txtYRSearchZZMC.Text.Trim()
                If Me.txtYRSearchZZMC.Text <> "" Then
                    Me.txtYRSearchZZMC.Text = objPulicParameters.getNewSearchString(Me.txtYRSearchZZMC.Text)
                    If strQuery = "" Then
                        strQuery = strZZMC + " like '" + Me.txtYRSearchZZMC.Text + "%'"
                    Else
                        strQuery = strQuery + " and " + strZZMC + " like '" + Me.txtYRSearchZZMC.Text + "%'"
                    End If
                End If

                '按行政级别搜索
                Dim strJBMC As String
                strJBMC = "a." + Xydc.Platform.Common.Data.XingzhengjibieData.FIELD_GG_B_XINGZHENGJIBIE_JBMC
                If Me.txtYRSearchJBMC.Text.Length > 0 Then Me.txtYRSearchJBMC.Text = Me.txtYRSearchJBMC.Text.Trim()
                If Me.txtYRSearchJBMC.Text <> "" Then
                    Me.txtYRSearchJBMC.Text = objPulicParameters.getNewSearchString(Me.txtYRSearchJBMC.Text)
                    If strQuery = "" Then
                        strQuery = strJBMC + " like '" + Me.txtYRSearchJBMC.Text + "%'"
                    Else
                        strQuery = strQuery + " and " + strJBMC + " like '" + Me.txtYRSearchJBMC.Text + "%'"
                    End If
                End If

                '按担任职务搜索
                Dim strGWLB As String
                strGWLB = "a." + Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_FULLJOIN_GWLB
                If Me.txtYRSearchGWLB.Text.Length > 0 Then Me.txtYRSearchGWLB.Text = Me.txtYRSearchGWLB.Text.Trim()
                If Me.txtYRSearchGWLB.Text <> "" Then
                    Me.txtYRSearchGWLB.Text = objPulicParameters.getNewSearchString(Me.txtYRSearchGWLB.Text)
                    If strQuery = "" Then
                        strQuery = strGWLB + " like '" + Me.txtYRSearchGWLB.Text + "%'"
                    Else
                        strQuery = strQuery + " and " + strGWLB + " like '" + Me.txtYRSearchGWLB.Text + "%'"
                    End If
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            getQueryString_YR = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取grdWR的搜索条件(默认表前缀a.)
        '     strErrMsg      ：返回错误信息
        '     strQuery       ：返回的搜索条件
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function getQueryString_WR( _
            ByRef strErrMsg As String, _
            ByRef strQuery As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            getQueryString_WR = False
            strQuery = ""

            Try
                '按人员代码搜索
                Dim strRYDM As String
                strRYDM = "a." + Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYDM
                If Me.txtWRSearchRYDM.Text.Length > 0 Then Me.txtWRSearchRYDM.Text = Me.txtWRSearchRYDM.Text.Trim()
                If Me.txtWRSearchRYDM.Text <> "" Then
                    Me.txtWRSearchRYDM.Text = objPulicParameters.getNewSearchString(Me.txtWRSearchRYDM.Text)
                    If strQuery = "" Then
                        strQuery = strRYDM + " like '" + Me.txtWRSearchRYDM.Text + "%'"
                    Else
                        strQuery = strQuery + " and " + strRYDM + " like '" + Me.txtWRSearchRYDM.Text + "%'"
                    End If
                End If

                '按人员名称搜索
                Dim strRYMC As String
                strRYMC = "a." + Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYMC
                If Me.txtWRSearchRYMC.Text.Length > 0 Then Me.txtWRSearchRYMC.Text = Me.txtWRSearchRYMC.Text.Trim()
                If Me.txtWRSearchRYMC.Text <> "" Then
                    Me.txtWRSearchRYMC.Text = objPulicParameters.getNewSearchString(Me.txtWRSearchRYMC.Text)
                    If strQuery = "" Then
                        strQuery = strRYMC + " like '" + Me.txtWRSearchRYMC.Text + "%'"
                    Else
                        strQuery = strQuery + " and " + strRYMC + " like '" + Me.txtWRSearchRYMC.Text + "%'"
                    End If
                End If

                '按组织名称搜索
                Dim strZZMC As String
                strZZMC = "a." + Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_ZZMC
                If Me.txtWRSearchZZMC.Text.Length > 0 Then Me.txtWRSearchZZMC.Text = Me.txtWRSearchZZMC.Text.Trim()
                If Me.txtWRSearchZZMC.Text <> "" Then
                    Me.txtWRSearchZZMC.Text = objPulicParameters.getNewSearchString(Me.txtWRSearchZZMC.Text)
                    If strQuery = "" Then
                        strQuery = strZZMC + " like '" + Me.txtWRSearchZZMC.Text + "%'"
                    Else
                        strQuery = strQuery + " and " + strZZMC + " like '" + Me.txtWRSearchZZMC.Text + "%'"
                    End If
                End If

                '按行政级别搜索
                Dim strJBMC As String
                strJBMC = "a." + Xydc.Platform.Common.Data.XingzhengjibieData.FIELD_GG_B_XINGZHENGJIBIE_JBMC
                If Me.txtWRSearchJBMC.Text.Length > 0 Then Me.txtWRSearchJBMC.Text = Me.txtWRSearchJBMC.Text.Trim()
                If Me.txtWRSearchJBMC.Text <> "" Then
                    Me.txtWRSearchJBMC.Text = objPulicParameters.getNewSearchString(Me.txtWRSearchJBMC.Text)
                    If strQuery = "" Then
                        strQuery = strJBMC + " like '" + Me.txtWRSearchJBMC.Text + "%'"
                    Else
                        strQuery = strQuery + " and " + strJBMC + " like '" + Me.txtWRSearchJBMC.Text + "%'"
                    End If
                End If

                '按担任职务搜索
                Dim strGWLB As String
                strGWLB = "a." + Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_FULLJOIN_GWLB
                If Me.txtWRSearchGWLB.Text.Length > 0 Then Me.txtWRSearchGWLB.Text = Me.txtWRSearchGWLB.Text.Trim()
                If Me.txtWRSearchGWLB.Text <> "" Then
                    Me.txtWRSearchGWLB.Text = objPulicParameters.getNewSearchString(Me.txtWRSearchGWLB.Text)
                    If strQuery = "" Then
                        strQuery = strGWLB + " like '" + Me.txtWRSearchGWLB.Text + "%'"
                    Else
                        strQuery = strQuery + " and " + strGWLB + " like '" + Me.txtWRSearchGWLB.Text + "%'"
                    End If
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            getQueryString_WR = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 从tvwServers中指定的索引strNodeIndex获取当前服务器的参数
        '     strErrMsg             ：返回错误信息
        '     strNodeIndex          ：tvwServers的索引号
        '     objConnectionProperty ：返回服务器参数
        ' 返回
        '     True                  ：成功
        '     False                 ：失败
        '----------------------------------------------------------------
        Private Function getServerConnectionProperty( _
            ByRef strErrMsg As String, _
            ByVal strNodeIndex As String, _
            ByRef objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objsystemAppManager As New Xydc.Platform.BusinessFacade.systemAppManager
            Dim objTreeviewProcess As New Xydc.Platform.web.TreeviewProcess
            Dim objAppManagerData As Xydc.Platform.Common.Data.AppManagerData

            getServerConnectionProperty = False
            Xydc.Platform.Common.Utilities.ConnectionProperty.SafeRelease(objConnectionProperty)

            Try
                '检查
                If strNodeIndex Is Nothing Then strNodeIndex = ""
                strNodeIndex = strNodeIndex.Trim()
                If strNodeIndex = "" Then
                    Exit Try
                End If

                '获取节点
                Dim objTreeNode As Microsoft.Web.UI.WebControls.TreeNode
                objTreeNode = Me.tvwServers.GetNodeFromIndex(strNodeIndex)
                If objTreeNode Is Nothing Then
                    Exit Try
                End If

                '判断级别
                Dim strServerName As String = ""
                Dim strDBName As String = ""
                Dim intLevel As Integer
                intLevel = objTreeviewProcess.getLevelIndexFromNodeIndex(objTreeNode.GetNodeIndex())
                Select Case intLevel
                    Case 1    '全部服务器
                        Exit Try

                    Case 2    '服务器层
                        strServerName = objTreeNode.Text

                    Case 3    '数据库层
                        strDBName = objTreeNode.Text

                        '获取服务器名
                        Dim objTreeNodeDB As Microsoft.Web.UI.WebControls.TreeNode
                        Dim strIndexDB As String
                        strIndexDB = objTreeviewProcess.getLevelIndexFromNodeIndex(objTreeNode.GetNodeIndex, intLevel - 1, True)
                        objTreeNodeDB = Me.tvwServers.GetNodeFromIndex(strIndexDB)
                        If objTreeNodeDB Is Nothing Then
                            Exit Try
                        End If
                        strServerName = objTreeNodeDB.Text

                    Case Else '无效层
                        Exit Try
                End Select

                '获取服务器信息
                If objsystemAppManager.getServerConnectionProperty(strErrMsg, MyBase.UserId, MyBase.UserPassword, strServerName, objConnectionProperty) = False Then
                    GoTo errProc
                End If
                '设置到定位数据库
                If strDBName <> "" Then
                    objConnectionProperty.InitialCatalog = strDBName
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.BusinessFacade.systemAppManager.SafeRelease(objsystemAppManager)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objAppManagerData)
            Xydc.Platform.web.TreeviewProcess.SafeRelease(objTreeviewProcess)

            getServerConnectionProperty = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.BusinessFacade.systemAppManager.SafeRelease(objsystemAppManager)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objAppManagerData)
            Xydc.Platform.web.TreeviewProcess.SafeRelease(objTreeviewProcess)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取grdYR要显示的数据信息
        '     strErrMsg      ：返回错误信息
        '     strWhere       ：搜索条件
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function getModuleData_YR( _
            ByRef strErrMsg As String, _
            ByVal strWhere As String) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN
            Dim objsystemAppManager As New Xydc.Platform.BusinessFacade.systemAppManager
            Dim objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty

            getModuleData_YR = False

            Try
                '从tvwServers中获取信息
                If Me.getServerConnectionProperty(strErrMsg, Me.tvwServers.SelectedNodeIndex, objConnectionProperty) = False Then
                    GoTo errProc
                End If

                '备份Sort字符串
                Dim strSort As String
                strSort = Me.htxtYRSort.Value
                If strSort.Length > 0 Then strSort = strSort.Trim

                '释放资源
                If Not (Me.m_objDataSet_YR Is Nothing) Then
                    Me.m_objDataSet_YR.Dispose()
                    Me.m_objDataSet_YR = Nothing
                End If

                '重新检索数据
                If objsystemAppManager.getRenyuanGrantedData(strErrMsg, objConnectionProperty, strWhere, Me.m_objDataSet_YR) = False Then
                    GoTo errProc
                End If

                '恢复Sort字符串
                With Me.m_objDataSet_YR.Tables(strTable)
                    .DefaultView.Sort = strSort
                End With

                '缓存参数
                With Me.m_objDataSet_YR.Tables(strTable)
                    Me.htxtYRRows.Value = .DefaultView.Count.ToString()
                    Me.m_intRows_YR = .DefaultView.Count
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ConnectionProperty.SafeRelease(objConnectionProperty)
            Xydc.Platform.BusinessFacade.systemAppManager.SafeRelease(objsystemAppManager)

            getModuleData_YR = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ConnectionProperty.SafeRelease(objConnectionProperty)
            Xydc.Platform.BusinessFacade.systemAppManager.SafeRelease(objsystemAppManager)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取grdWR要显示的数据信息
        '     strErrMsg      ：返回错误信息
        '     strWhere       ：搜索条件
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function getModuleData_WR( _
            ByRef strErrMsg As String, _
            ByVal strWhere As String) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN
            Dim objsystemAppManager As New Xydc.Platform.BusinessFacade.systemAppManager
            Dim objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty

            getModuleData_WR = False

            Try
                '从tvwServers中获取信息
                If Me.getServerConnectionProperty(strErrMsg, Me.tvwServers.SelectedNodeIndex, objConnectionProperty) = False Then
                    GoTo errProc
                End If

                '备份Sort字符串
                Dim strSort As String
                strSort = Me.htxtWRSort.Value
                If strSort.Length > 0 Then strSort = strSort.Trim

                '释放资源
                If Not (Me.m_objDataSet_WR Is Nothing) Then
                    Me.m_objDataSet_WR.Dispose()
                    Me.m_objDataSet_WR = Nothing
                End If

                '重新检索数据
                If objsystemAppManager.getRenyuanUngrantedData(strErrMsg, objConnectionProperty, strWhere, Me.m_objDataSet_WR) = False Then
                    GoTo errProc
                End If

                '恢复Sort字符串
                With Me.m_objDataSet_WR.Tables(strTable)
                    .DefaultView.Sort = strSort
                End With

                '缓存参数
                With Me.m_objDataSet_WR.Tables(strTable)
                    Me.htxtWRRows.Value = .DefaultView.Count.ToString()
                    Me.m_intRows_WR = .DefaultView.Count
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ConnectionProperty.SafeRelease(objConnectionProperty)
            Xydc.Platform.BusinessFacade.systemAppManager.SafeRelease(objsystemAppManager)

            getModuleData_WR = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ConnectionProperty.SafeRelease(objConnectionProperty)
            Xydc.Platform.BusinessFacade.systemAppManager.SafeRelease(objsystemAppManager)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 从tvwServers中指定的索引strNodeIndex获取对应下级的数据
        '----------------------------------------------------------------
        Private Function getModuleData_Server( _
            ByRef strErrMsg As String, _
            ByVal strNodeIndex As String, _
            ByRef objAppManagerData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Dim objsystemAppManager As New Xydc.Platform.BusinessFacade.systemAppManager
            Dim objTreeviewProcess As New Xydc.Platform.web.TreeviewProcess

            getModuleData_Server = False
            objAppManagerData = Nothing

            Try
                '检查
                If strNodeIndex Is Nothing Then strNodeIndex = ""
                strNodeIndex = strNodeIndex.Trim()
                If strNodeIndex = "" Then
                    Exit Try
                End If

                '获取节点
                Dim objTreeNode As Microsoft.Web.UI.WebControls.TreeNode
                objTreeNode = Me.tvwServers.GetNodeFromIndex(strNodeIndex)
                If objTreeNode Is Nothing Then
                    Exit Try
                End If

                '判断级别
                Dim strServerName As String = ""
                Dim strDBName As String = ""
                Dim intLevel As Integer
                intLevel = objTreeviewProcess.getLevelIndexFromNodeIndex(objTreeNode.GetNodeIndex())
                Select Case intLevel
                    Case 1    '准备获取服务器数据
                        If objsystemAppManager.getFuwuqiData(strErrMsg, MyBase.UserId, MyBase.UserPassword, "", objAppManagerData) = False Then
                            GoTo errProc
                        End If

                    Case 2    '准备获取数据库数据
                        Dim objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty
                        Dim strRoleName As String
                        If Me.getServerConnectionProperty(strErrMsg, strNodeIndex, objConnectionProperty) = False Then
                            GoTo errProc
                        End If
                        If objsystemAppManager.getShujukuData(strErrMsg, objConnectionProperty, "", objAppManagerData) = False Then
                            GoTo errProc
                        End If

                    Case Else
                End Select

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.BusinessFacade.systemAppManager.SafeRelease(objsystemAppManager)
            Xydc.Platform.web.TreeviewProcess.SafeRelease(objTreeviewProcess)

            getModuleData_Server = True
            Exit Function
errProc:
            Xydc.Platform.BusinessFacade.systemAppManager.SafeRelease(objsystemAppManager)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objAppManagerData)
            Xydc.Platform.web.TreeviewProcess.SafeRelease(objTreeviewProcess)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据屏幕搜索条件搜索grdYR数据
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function searchModuleData_YR(ByRef strErrMsg As String) As Boolean

            searchModuleData_YR = False

            Try
                '获取搜索字符串
                Dim strQuery As String
                If Me.getQueryString_YR(strErrMsg, strQuery) = False Then
                    GoTo errProc
                End If

                '搜索数据
                If Me.getModuleData_YR(strErrMsg, strQuery) = False Then
                    GoTo errProc
                End If

                '记录搜索字符串
                Me.m_strQuery_YR = strQuery
                Me.htxtYRQuery.Value = Me.m_strQuery_YR

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            searchModuleData_YR = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据屏幕搜索条件搜索grdWR数据
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function searchModuleData_WR(ByRef strErrMsg As String) As Boolean

            searchModuleData_WR = False

            Try
                '获取搜索字符串
                Dim strQuery As String
                If Me.getQueryString_WR(strErrMsg, strQuery) = False Then
                    GoTo errProc
                End If

                '搜索数据
                If Me.getModuleData_WR(strErrMsg, strQuery) = False Then
                    GoTo errProc
                End If

                '记录搜索字符串
                Me.m_strQuery_WR = strQuery
                Me.htxtWRQuery.Value = Me.m_strQuery_WR

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            searchModuleData_WR = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示grdYR的数据
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showDataGridInfo_YR( _
            ByRef strErrMsg As String) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN
            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess

            showDataGridInfo_YR = False

            '获取系统保存的网格排序数据
            Dim intSortColumnIndex As Integer
            intSortColumnIndex = objPulicParameters.getObjectValue(Me.htxtYRSortColumnIndex.Value, -1)
            Dim objSortType As Xydc.Platform.Common.Utilities.PulicParameters.enumSortType
            Try
                objSortType = CType(Me.htxtYRSortType.Value, Xydc.Platform.Common.Utilities.PulicParameters.enumSortType)
            Catch ex As Exception
                objSortType = Xydc.Platform.Common.Utilities.PulicParameters.enumSortType.None
            End Try

            '网格显示处理
            Try
                '在获取数据时已经恢复了RowFilter、Sort的现场
                '设置数据源
                If Me.m_objDataSet_YR Is Nothing Then
                    Me.grdYR.DataSource = Nothing
                Else
                    With Me.m_objDataSet_YR.Tables(strTable)
                        Me.grdYR.DataSource = .DefaultView
                    End With
                End If

                '调整网格参数
                With Me.m_objDataSet_YR.Tables(strTable)
                    If objDataGridProcess.onBeforeDataGridBind(strErrMsg, Me.grdYR, .DefaultView.Count) = False Then
                        GoTo errProc
                    End If
                End With

                '恢复列标题中的排序信息
                If intSortColumnIndex >= 0 Then
                    objDataGridProcess.doClearSortCharInDataGridHead(Me.grdYR)
                    With Me.grdYR.Columns(intSortColumnIndex)
                        .HeaderText = objDataGridProcess.getColumnSortHeadString(.HeaderText, objSortType)
                    End With
                End If

                '绑定数据
                Me.grdYR.DataBind()

                '----------------------------------------------------------------
                '因为这些信息是非绑定的，所以下面的操作必须等绑定完成后执行！！！
                '一旦在后续处理中执行了DataBind，则信息会丢失！！！
                '----------------------------------------------------------------
                '恢复网格中的CheckBox状态
                If objDataGridProcess.doRestoreDataGridCheckBoxStatus(strErrMsg, Me.grdYR, Request, 0, Me.m_cstrCheckBoxIdInDataGrid_YR) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)

            showDataGridInfo_YR = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示grdWR的数据
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showDataGridInfo_WR( _
            ByRef strErrMsg As String) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN
            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess

            showDataGridInfo_WR = False

            '获取系统保存的网格排序数据
            Dim intSortColumnIndex As Integer
            intSortColumnIndex = objPulicParameters.getObjectValue(Me.htxtWRSortColumnIndex.Value, -1)
            Dim objSortType As Xydc.Platform.Common.Utilities.PulicParameters.enumSortType
            Try
                objSortType = CType(Me.htxtWRSortType.Value, Xydc.Platform.Common.Utilities.PulicParameters.enumSortType)
            Catch ex As Exception
                objSortType = Xydc.Platform.Common.Utilities.PulicParameters.enumSortType.None
            End Try

            '网格显示处理
            Try
                '在获取数据时已经恢复了RowFilter、Sort的现场
                '设置数据源
                If Me.m_objDataSet_WR Is Nothing Then
                    Me.grdWR.DataSource = Nothing
                Else
                    With Me.m_objDataSet_WR.Tables(strTable)
                        Me.grdWR.DataSource = .DefaultView
                    End With
                End If

                '调整网格参数
                With Me.m_objDataSet_WR.Tables(strTable)
                    If objDataGridProcess.onBeforeDataGridBind(strErrMsg, Me.grdWR, .DefaultView.Count) = False Then
                        GoTo errProc
                    End If
                End With

                '恢复列标题中的排序信息
                If intSortColumnIndex >= 0 Then
                    objDataGridProcess.doClearSortCharInDataGridHead(Me.grdWR)
                    With Me.grdWR.Columns(intSortColumnIndex)
                        .HeaderText = objDataGridProcess.getColumnSortHeadString(.HeaderText, objSortType)
                    End With
                End If

                '绑定数据
                Me.grdWR.DataBind()

                '----------------------------------------------------------------
                '因为这些信息是非绑定的，所以下面的操作必须等绑定完成后执行！！！
                '一旦在后续处理中执行了DataBind，则信息会丢失！！！
                '----------------------------------------------------------------
                '恢复网格中的CheckBox状态
                If objDataGridProcess.doRestoreDataGridCheckBoxStatus(strErrMsg, Me.grdWR, Request, 0, Me.m_cstrCheckBoxIdInDataGrid_WR) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)

            showDataGridInfo_WR = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示grdYR及相关信息
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showModuleData_YR(ByRef strErrMsg As String) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess

            showModuleData_YR = False

            Try
                '显示网格信息
                If Me.showDataGridInfo_YR(strErrMsg) = False Then
                    GoTo errProc
                End If

                '显示与网格紧密相关的操作或信息提示
                With Me.m_objDataSet_YR.Tables(strTable).DefaultView
                    '显示网格位置信息
                    Me.lblYRGridLocInfo.Text = objDataGridProcess.getDataGridLocation(Me.grdYR, .Count)

                    '显示页面浏览功能
                    Me.lnkCZYRMoveFirst.Enabled = objDataGridProcess.canDoMoveFirstPage(Me.grdYR, .Count)
                    Me.lnkCZYRMoveLast.Enabled = objDataGridProcess.canDoMoveLastPage(Me.grdYR, .Count)
                    Me.lnkCZYRMovePrev.Enabled = objDataGridProcess.canDoMovePreviousPage(Me.grdYR, .Count)
                    Me.lnkCZYRMoveNext.Enabled = objDataGridProcess.canDoMoveNextPage(Me.grdYR, .Count)

                    '显示相关操作
                    Dim blnEnabled As Boolean
                    If .Count < 1 Then
                        blnEnabled = False
                    Else
                        blnEnabled = True
                    End If
                    Me.lnkCZYRDeSelectAll.Enabled = blnEnabled
                    Me.lnkCZYRSelectAll.Enabled = blnEnabled
                    Me.lnkCZYRGotoPage.Enabled = blnEnabled
                    Me.lnkCZYRSetPageSize.Enabled = blnEnabled
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)

            showModuleData_YR = True
            Exit Function

errProc:
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示模块级操作状态
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showModuleData_MAIN(ByRef strErrMsg As String) As Boolean

            showModuleData_MAIN = False

            Try
                Me.lnkMLRoleQX.Enabled = Me.m_blnPrevilegeParams(4)
                Me.lnkMLRoleQXB.Enabled = Me.m_blnPrevilegeParams(4)

                Me.lnkMLUserQX.Enabled = Me.m_blnPrevilegeParams(7)
                Me.lnkMLUserQXB.Enabled = Me.m_blnPrevilegeParams(7)

                Me.lnkMLGrant.Enabled = Me.m_blnPrevilegeParams(2)
                Me.lnkMLGrantB.Enabled = Me.m_blnPrevilegeParams(2)
                Me.lnkMLRevoke.Enabled = Me.m_blnPrevilegeParams(3)
                Me.lnkMLRevokeB.Enabled = Me.m_blnPrevilegeParams(3)

                Me.lnkMLClose.Enabled = True
                Me.lnkMLCloseB.Enabled = True

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
        ' 显示grdWR及相关信息
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showModuleData_WR(ByRef strErrMsg As String) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess

            showModuleData_WR = False

            Try
                '显示网格信息
                If Me.showDataGridInfo_WR(strErrMsg) = False Then
                    GoTo errProc
                End If

                '显示与网格紧密相关的操作或信息提示
                With Me.m_objDataSet_WR.Tables(strTable).DefaultView
                    '显示网格位置信息
                    Me.lblWRGridLocInfo.Text = objDataGridProcess.getDataGridLocation(Me.grdWR, .Count)

                    '显示页面浏览功能
                    Me.lnkCZWRMoveFirst.Enabled = objDataGridProcess.canDoMoveFirstPage(Me.grdWR, .Count)
                    Me.lnkCZWRMoveLast.Enabled = objDataGridProcess.canDoMoveLastPage(Me.grdWR, .Count)
                    Me.lnkCZWRMovePrev.Enabled = objDataGridProcess.canDoMovePreviousPage(Me.grdWR, .Count)
                    Me.lnkCZWRMoveNext.Enabled = objDataGridProcess.canDoMoveNextPage(Me.grdWR, .Count)

                    '显示相关操作
                    Dim blnEnabled As Boolean
                    If .Count < 1 Then
                        blnEnabled = False
                    Else
                        blnEnabled = True
                    End If
                    Me.lnkCZWRDeSelectAll.Enabled = blnEnabled
                    Me.lnkCZWRSelectAll.Enabled = blnEnabled
                    Me.lnkCZWRGotoPage.Enabled = blnEnabled
                    Me.lnkCZWRSetPageSize.Enabled = blnEnabled
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)

            showModuleData_WR = True
            Exit Function

errProc:
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示tvwServers的数据
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showTreeViewInfo_Server( _
            ByRef strErrMsg As String) As Boolean

            Dim objTreeviewProcess As New Xydc.Platform.web.TreeviewProcess

            showTreeViewInfo_Server = False

            Try
                Dim objTreeNode As Microsoft.Web.UI.WebControls.TreeNode
                objTreeNode = New Microsoft.Web.UI.WebControls.TreeNode
                objTreeNode.Text = "全部服务器"
                objTreeNode.Expandable = Microsoft.Web.UI.WebControls.ExpandableValue.CheckOnce
                objTreeNode.CheckBox = False
                Me.tvwServers.Nodes.Add(objTreeNode)
                objTreeNode.ID = objTreeviewProcess.getNodeId("A", objTreeNode.GetNodeIndex(), "1")
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.TreeviewProcess.SafeRelease(objTreeviewProcess)

            showTreeViewInfo_Server = True
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
                '显示Pannel
                Me.panelMain.Visible = True
                Me.panelError.Visible = Not Me.panelMain.Visible

                '执行键转译(不论是否是“回发”)
                Try
                    With New Xydc.Platform.web.ControlProcess
                        .doTranslateKey(Me.txtYRPageIndex)
                        .doTranslateKey(Me.txtYRPageSize)

                        .doTranslateKey(Me.txtYRSearchRYDM)
                        .doTranslateKey(Me.txtYRSearchRYMC)
                        .doTranslateKey(Me.txtYRSearchZZMC)
                        .doTranslateKey(Me.txtYRSearchJBMC)
                        .doTranslateKey(Me.txtYRSearchGWLB)

                        .doTranslateKey(Me.txtWRPageIndex)
                        .doTranslateKey(Me.txtWRPageSize)

                        .doTranslateKey(Me.txtWRSearchRYDM)
                        .doTranslateKey(Me.txtWRSearchRYMC)
                        .doTranslateKey(Me.txtWRSearchZZMC)
                        .doTranslateKey(Me.txtWRSearchJBMC)
                        .doTranslateKey(Me.txtWRSearchGWLB)
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '显示模块级操作
                If Me.showModuleData_MAIN(strErrMsg) = False Then
                    GoTo errProc
                End If

                '显示已经授权存取的数据
                If Me.getModuleData_YR(strErrMsg, Me.m_strQuery_YR) = False Then
                    GoTo errProc
                End If
                If Me.showModuleData_YR(strErrMsg) = False Then
                    GoTo errProc
                End If

                '显示未授权存取的数据
                If Me.getModuleData_WR(strErrMsg, Me.m_strQuery_WR) = False Then
                    GoTo errProc
                End If
                If Me.showModuleData_WR(strErrMsg) = False Then
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
                If Me.showTreeViewInfo_Server(strErrMsg) = False Then
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

            '记录审计日志
            If Me.IsPostBack = False Then
                If Me.m_blnSaveScence = False Then
                    Xydc.Platform.SystemFramework.ApplicationLog.WriteAuditAQInfo(Request.UserHostAddress, Request.UserHostName, "[" + MyBase.UserId + "]访问了[各个用户标识存取数据库的权限信息]！")
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
        Private Sub tvwServers_Expand(ByVal sender As Object, ByVal e As Microsoft.Web.UI.WebControls.TreeViewClickEventArgs) Handles tvwServers.Expand

            Dim objTreeviewProcess As New Xydc.Platform.web.TreeviewProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '获取节点级别
                Dim strNodeIndex As String = e.Node
                Dim objTreeNode As Microsoft.Web.UI.WebControls.TreeNode
                objTreeNode = Me.tvwServers.GetNodeFromIndex(strNodeIndex)
                If objTreeNode Is Nothing Then Exit Try
                Dim intLevel As Integer
                intLevel = objTreeviewProcess.getLevelIndexFromNodeIndex(strNodeIndex)

                '根据节点展开
                If objTreeNode.Nodes.Count > 0 Then
                    Exit Try '已经展开过
                End If
                Select Case objTreeNode.Expandable
                    Case Microsoft.Web.UI.WebControls.ExpandableValue.CheckOnce
                    Case Else
                        Exit Try
                End Select

                '获取要展开的数据
                Dim objAppManagerData As Xydc.Platform.Common.Data.AppManagerData
                If Me.getModuleData_Server(strErrMsg, strNodeIndex, objAppManagerData) = False Then
                    GoTo errProc
                End If
                If objAppManagerData Is Nothing Then
                    Exit Try
                End If

                '显示数据
                Select Case intLevel
                    Case 1 '展开服务器
                        If objTreeviewProcess.doShowTreeNodeChildren(strErrMsg, objTreeNode, _
                            objAppManagerData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_FUWUQI), _
                            Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_FUWUQI_MC, _
                            Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_FUWUQI_MC, _
                            False, Microsoft.Web.UI.WebControls.ExpandableValue.CheckOnce, False) = False Then
                            GoTo errProc
                        End If

                    Case 2 '展开数据库
                        If objTreeviewProcess.doShowTreeNodeChildren(strErrMsg, objTreeNode, _
                            objAppManagerData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_SHUJUKU), _
                            Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_SHUJUKU_SJKM, _
                            Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_SHUJUKU_SJKM, _
                            False, Microsoft.Web.UI.WebControls.ExpandableValue.Auto, False) = False Then
                            GoTo errProc
                        End If

                    Case Else
                End Select
                objTreeNode.Expandable = Microsoft.Web.UI.WebControls.ExpandableValue.Auto

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

        Private Sub tvwServers_SelectedIndexChange(ByVal sender As Object, ByVal e As Microsoft.Web.UI.WebControls.TreeViewSelectEventArgs) Handles tvwServers.SelectedIndexChange

            Dim objTreeviewProcess As New Xydc.Platform.web.TreeviewProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                If Me.getModuleData_YR(strErrMsg, Me.m_strQuery_YR) = False Then
                    GoTo errProc
                End If
                If Me.showModuleData_YR(strErrMsg) = False Then
                    GoTo errProc
                End If

                If Me.getModuleData_WR(strErrMsg, Me.m_strQuery_WR) = False Then
                    GoTo errProc
                End If
                If Me.showModuleData_WR(strErrMsg) = False Then
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

        '实现对grdYR网格行、列的固定
        Sub grdYR_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles grdYR.ItemDataBound

            Dim intCells As Integer
            Dim i As Integer

            Try
                If e.Item.ItemIndex < 0 Then
                    '标题行,输出标题锁定一般属性
                    intCells = e.Item.Cells.Count
                    For i = 0 To intCells - 1 Step 1
                        e.Item.Cells(i).Attributes.CssStyle.Add("top", "expression(" + Me.m_cstrDataGridInDIV_YR + ".scrollTop)")
                    Next
                End If
                If Me.m_intFixedColumns_YR > 0 Then
                    '锁定列
                    For i = 0 To Me.m_intFixedColumns_YR - 1 Step 1
                        e.Item.Cells(i).CssClass = Me.grdYR.ID + "Locked"
                    Next
                End If
            Catch ex As Exception
            End Try

            Exit Sub

        End Sub

        '实现对grdWR网格行、列的固定
        Sub grdWR_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles grdWR.ItemDataBound

            Dim intCells As Integer
            Dim i As Integer

            Try
                If e.Item.ItemIndex < 0 Then
                    '标题行,输出标题锁定一般属性
                    intCells = e.Item.Cells.Count
                    For i = 0 To intCells - 1 Step 1
                        e.Item.Cells(i).Attributes.CssStyle.Add("top", "expression(" + Me.m_cstrDataGridInDIV_WR + ".scrollTop)")
                    Next
                End If
                If Me.m_intFixedColumns_WR > 0 Then
                    '锁定列
                    For i = 0 To Me.m_intFixedColumns_WR - 1 Step 1
                        e.Item.Cells(i).CssClass = Me.grdWR.ID + "Locked"
                    Next
                End If
            Catch ex As Exception
            End Try

            Exit Sub

        End Sub

        Private Sub grdYR_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdYR.SelectedIndexChanged

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '显示记录位置
                With New Xydc.Platform.web.DataGridProcess
                    Me.lblYRGridLocInfo.Text = .getDataGridLocation(Me.grdYR, Me.m_intRows_YR)
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

        Private Sub grdWR_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdWR.SelectedIndexChanged

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '显示记录位置
                With New Xydc.Platform.web.DataGridProcess
                    Me.lblWRGridLocInfo.Text = .getDataGridLocation(Me.grdWR, Me.m_intRows_WR)
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

        Private Sub grdYR_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles grdYR.SortCommand

            Dim strTable As String = Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN
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
                If Me.getModuleData_YR(strErrMsg, Me.m_strQuery_YR) = False Then
                    GoTo errProc
                End If

                '获取原排序命令
                strOldCommand = Me.m_objDataSet_YR.Tables(strTable).DefaultView.Sort

                '获取要执行的排序命令
                objDataGridProcess.getColumnSortCommand(strErrMsg, strOldCommand, e.SortExpression, strFinalCommand, objenumSortType)
                If strErrMsg <> "" Then
                    GoTo errProc
                End If

                '执行排序
                Me.m_objDataSet_YR.Tables(strTable).DefaultView.Sort = strFinalCommand

                '获取排序列的列索引
                objDataGridItem = CType(e.CommandSource, System.Web.UI.WebControls.DataGridItem)
                strUniqueId = Request.Form("__EVENTTARGET")
                intColumnIndex = objDataGridProcess.getColumnIndexByUniqueIdInRow(objDataGridItem, strUniqueId)

                '保存排序信息
                Me.htxtYRSortColumnIndex.Value = intColumnIndex.ToString()
                Me.htxtYRSortType.Value = CType(objenumSortType, Integer).ToString()
                Me.htxtYRSort.Value = strFinalCommand

                '重新显示数据
                If Me.showModuleData_YR(strErrMsg) = False Then
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

        Private Sub grdWR_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles grdWR.SortCommand

            Dim strTable As String = Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN
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
                If Me.getModuleData_WR(strErrMsg, Me.m_strQuery_WR) = False Then
                    GoTo errProc
                End If

                '获取原排序命令
                strOldCommand = Me.m_objDataSet_WR.Tables(strTable).DefaultView.Sort

                '获取要执行的排序命令
                objDataGridProcess.getColumnSortCommand(strErrMsg, strOldCommand, e.SortExpression, strFinalCommand, objenumSortType)
                If strErrMsg <> "" Then
                    GoTo errProc
                End If

                '执行排序
                Me.m_objDataSet_WR.Tables(strTable).DefaultView.Sort = strFinalCommand

                '获取排序列的列索引
                objDataGridItem = CType(e.CommandSource, System.Web.UI.WebControls.DataGridItem)
                strUniqueId = Request.Form("__EVENTTARGET")
                intColumnIndex = objDataGridProcess.getColumnIndexByUniqueIdInRow(objDataGridItem, strUniqueId)

                '保存排序信息
                Me.htxtWRSortColumnIndex.Value = intColumnIndex.ToString()
                Me.htxtWRSortType.Value = CType(objenumSortType, Integer).ToString()
                Me.htxtWRSort.Value = strFinalCommand

                '重新显示数据
                If Me.showModuleData_WR(strErrMsg) = False Then
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

        Private Sub doMoveFirst_YR(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_YR(strErrMsg, Me.m_strQuery_YR) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(0, Me.grdYR.PageCount)
                Me.grdYR.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_YR(strErrMsg) = False Then
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

        Private Sub doMoveFirst_WR(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_WR(strErrMsg, Me.m_strQuery_WR) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(0, Me.grdWR.PageCount)
                Me.grdWR.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_WR(strErrMsg) = False Then
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

        Private Sub doMoveLast_YR(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_YR(strErrMsg, Me.m_strQuery_YR) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(Me.grdYR.PageCount - 1, Me.grdYR.PageCount)
                Me.grdYR.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_YR(strErrMsg) = False Then
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

        Private Sub doMoveLast_WR(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_WR(strErrMsg, Me.m_strQuery_WR) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(Me.grdWR.PageCount - 1, Me.grdWR.PageCount)
                Me.grdWR.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_WR(strErrMsg) = False Then
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

        Private Sub doMoveNext_YR(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_YR(strErrMsg, Me.m_strQuery_YR) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(Me.grdYR.CurrentPageIndex + 1, Me.grdYR.PageCount)
                Me.grdYR.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_YR(strErrMsg) = False Then
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

        Private Sub doMoveNext_WR(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_WR(strErrMsg, Me.m_strQuery_WR) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(Me.grdWR.CurrentPageIndex + 1, Me.grdWR.PageCount)
                Me.grdWR.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_WR(strErrMsg) = False Then
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

        Private Sub doMovePrevious_YR(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_YR(strErrMsg, Me.m_strQuery_YR) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(Me.grdYR.CurrentPageIndex - 1, Me.grdYR.PageCount)
                Me.grdYR.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_YR(strErrMsg) = False Then
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

        Private Sub doMovePrevious_WR(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_WR(strErrMsg, Me.m_strQuery_WR) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(Me.grdWR.CurrentPageIndex - 1, Me.grdWR.PageCount)
                Me.grdWR.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_WR(strErrMsg) = False Then
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

        Private Sub doGotoPage_YR(ByVal strControlId As String)

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            '获取新的页大小
            Dim intPageIndex As Integer
            intPageIndex = objPulicParameters.getObjectValue(Me.txtYRPageIndex.Text, 0)
            If intPageIndex <= 0 Then
                intPageIndex = 0
            Else
                intPageIndex -= 1
            End If

            Try
                '获取数据
                If Me.getModuleData_YR(strErrMsg, Me.m_strQuery_YR) = False Then
                    GoTo errProc
                End If

                '设置新的页
                Me.grdYR.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_YR(strErrMsg) = False Then
                    GoTo errProc
                End If

                '信息同步
                Me.txtYRPageIndex.Text = (Me.grdYR.CurrentPageIndex + 1).ToString()

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

        Private Sub doGotoPage_WR(ByVal strControlId As String)

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            '获取新的页大小
            Dim intPageIndex As Integer
            intPageIndex = objPulicParameters.getObjectValue(Me.txtWRPageIndex.Text, 0)
            If intPageIndex <= 0 Then
                intPageIndex = 0
            Else
                intPageIndex -= 1
            End If

            Try
                '获取数据
                If Me.getModuleData_WR(strErrMsg, Me.m_strQuery_WR) = False Then
                    GoTo errProc
                End If

                '设置新的页
                Me.grdWR.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_WR(strErrMsg) = False Then
                    GoTo errProc
                End If

                '信息同步
                Me.txtWRPageIndex.Text = (Me.grdWR.CurrentPageIndex + 1).ToString()

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

        Private Sub doSetPageSize_YR(ByVal strControlId As String)

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            '获取新的页大小
            Dim intPageSize As Integer
            intPageSize = objPulicParameters.getObjectValue(Me.txtYRPageSize.Text, 100)
            If intPageSize <= 0 Then intPageSize = 100

            Try
                '获取数据
                If Me.getModuleData_YR(strErrMsg, Me.m_strQuery_YR) = False Then
                    GoTo errProc
                End If

                '设置新的页大小
                Me.grdYR.PageSize = intPageSize

                '刷新网格显示
                If Me.showModuleData_YR(strErrMsg) = False Then
                    GoTo errProc
                End If

                '信息同步
                Me.txtYRPageSize.Text = (Me.grdYR.PageSize).ToString()

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

        Private Sub doSetPageSize_WR(ByVal strControlId As String)

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            '获取新的页大小
            Dim intPageSize As Integer
            intPageSize = objPulicParameters.getObjectValue(Me.txtWRPageSize.Text, 100)
            If intPageSize <= 0 Then intPageSize = 100

            Try
                '获取数据
                If Me.getModuleData_WR(strErrMsg, Me.m_strQuery_WR) = False Then
                    GoTo errProc
                End If

                '设置新的页大小
                Me.grdWR.PageSize = intPageSize

                '刷新网格显示
                If Me.showModuleData_WR(strErrMsg) = False Then
                    GoTo errProc
                End If

                '信息同步
                Me.txtWRPageSize.Text = (Me.grdWR.PageSize).ToString()

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

        Private Sub doSelectAll_YR(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                If objDataGridProcess.doCheckedDataGridCheckBox(strErrMsg, Me.grdYR, 0, Me.m_cstrCheckBoxIdInDataGrid_YR, True) = False Then
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

        Private Sub doSelectAll_WR(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                If objDataGridProcess.doCheckedDataGridCheckBox(strErrMsg, Me.grdWR, 0, Me.m_cstrCheckBoxIdInDataGrid_WR, True) = False Then
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

        Private Sub doDeSelectAll_YR(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                If objDataGridProcess.doCheckedDataGridCheckBox(strErrMsg, Me.grdYR, 0, Me.m_cstrCheckBoxIdInDataGrid_YR, False) = False Then
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

        Private Sub doDeSelectAll_WR(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                If objDataGridProcess.doCheckedDataGridCheckBox(strErrMsg, Me.grdWR, 0, Me.m_cstrCheckBoxIdInDataGrid_WR, False) = False Then
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

        Private Sub doSearch_YR(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '搜索数据
                If Me.searchModuleData_YR(strErrMsg) = False Then
                    GoTo errProc
                End If

                '刷新网格显示
                If Me.showModuleData_YR(strErrMsg) = False Then
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

        Private Sub doSearch_WR(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '搜索数据
                If Me.searchModuleData_WR(strErrMsg) = False Then
                    GoTo errProc
                End If

                '刷新网格显示
                If Me.showModuleData_WR(strErrMsg) = False Then
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

        Private Sub lnkCZYRMoveFirst_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZYRMoveFirst.Click
            Me.doMoveFirst_YR("lnkCZYRMoveFirst")
        End Sub

        Private Sub lnkCZYRMoveLast_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZYRMoveLast.Click
            Me.doMoveLast_YR("lnkCZYRMoveLast")
        End Sub

        Private Sub lnkCZYRMoveNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZYRMoveNext.Click
            Me.doMoveNext_YR("lnkCZYRMoveNext")
        End Sub

        Private Sub lnkCZYRMovePrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZYRMovePrev.Click
            Me.doMovePrevious_YR("lnkCZYRMovePrev")
        End Sub

        Private Sub lnkCZYRGotoPage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZYRGotoPage.Click
            Me.doGotoPage_YR("lnkCZYRGotoPage")
        End Sub

        Private Sub lnkCZYRSetPageSize_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZYRSetPageSize.Click
            Me.doSetPageSize_YR("lnkCZYRSetPageSize")
        End Sub

        Private Sub lnkCZYRSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZYRSelectAll.Click
            Me.doSelectAll_YR("lnkCZYRSelectAll")
        End Sub

        Private Sub lnkCZYRDeSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZYRDeSelectAll.Click
            Me.doDeSelectAll_YR("lnkCZYRDeSelectAll")
        End Sub

        Private Sub btnYRSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnYRSearch.Click
            Me.doSearch_YR("btnYRSearch")
        End Sub

        Private Sub lnkCZWRMoveFirst_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZWRMoveFirst.Click
            Me.doMoveFirst_WR("lnkCZWRMoveFirst")
        End Sub

        Private Sub lnkCZWRMoveLast_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZWRMoveLast.Click
            Me.doMoveLast_WR("lnkCZWRMoveLast")
        End Sub

        Private Sub lnkCZWRMoveNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZWRMoveNext.Click
            Me.doMoveNext_WR("lnkCZWRMoveNext")
        End Sub

        Private Sub lnkCZWRMovePrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZWRMovePrev.Click
            Me.doMovePrevious_WR("lnkCZWRMovePrev")
        End Sub

        Private Sub lnkCZWRGotoPage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZWRGotoPage.Click
            Me.doGotoPage_WR("lnkCZWRGotoPage")
        End Sub

        Private Sub lnkCZWRSetPageSize_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZWRSetPageSize.Click
            Me.doSetPageSize_WR("lnkCZWRSetPageSize")
        End Sub

        Private Sub lnkCZWRSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZWRSelectAll.Click
            Me.doSelectAll_WR("lnkCZWRSelectAll")
        End Sub

        Private Sub lnkCZWRDeSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZWRDeSelectAll.Click
            Me.doDeSelectAll_WR("lnkCZWRDeSelectAll")
        End Sub

        Private Sub btnWRSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnWRSearch.Click
            Me.doSearch_WR("btnWRSearch")
        End Sub









        '----------------------------------------------------------------
        '模块特殊操作处理器
        '----------------------------------------------------------------
        Private Sub doGrantDatabase(ByVal strControlId As String)

            Dim objsystemAppManager As New Xydc.Platform.BusinessFacade.systemAppManager
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objTreeviewProcess As New Xydc.Platform.web.TreeviewProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '检查
                Dim intLevel As Integer
                intLevel = objTreeviewProcess.getLevelIndexFromNodeIndex(Me.tvwServers.SelectedNodeIndex)
                If intLevel < 3 Then
                    strErrMsg = "错误：必须选定数据库！"
                    GoTo errProc
                End If
                Dim objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty
                If Me.getServerConnectionProperty(strErrMsg, Me.tvwServers.SelectedNodeIndex, objConnectionProperty) = False Then
                    GoTo errProc
                End If
                Dim intSelected As Integer = 0
                Dim blnSelected As Boolean
                Dim intCount As Integer
                Dim i As Integer
                intCount = Me.grdWR.Items.Count
                For i = 0 To intCount - 1 Step 1
                    blnSelected = objDataGridProcess.isDataGridItemChecked(Me.grdWR.Items(i), 0, Me.m_cstrCheckBoxIdInDataGrid_WR)
                    If blnSelected = True Then
                        intSelected += 1
                    End If
                Next
                If intSelected < 1 Then
                    strErrMsg = "错误：未从下面的网格中选中要加入的人员！"
                    GoTo errProc
                End If

                '逐个加入成员
                Dim strLoginName As String
                Dim intColIndex As Integer
                intColIndex = objDataGridProcess.getDataGridColumnIndex(Me.grdWR, Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYDM)
                intCount = Me.grdWR.Items.Count
                For i = 0 To intCount - 1 Step 1
                    blnSelected = objDataGridProcess.isDataGridItemChecked(Me.grdWR.Items(i), 0, Me.m_cstrCheckBoxIdInDataGrid_WR)
                    If blnSelected = True Then
                        strLoginName = objDataGridProcess.getDataGridCellValue(Me.grdWR.Items(i), intColIndex)
                        If objsystemAppManager.doGrantDatabase(strErrMsg, objConnectionProperty, strLoginName) = False Then
                            GoTo errProc
                        End If

                        '记录审计日志
                        Xydc.Platform.SystemFramework.ApplicationLog.WriteAuditAQInfo(Request.UserHostAddress, Request.UserHostName, "[" + MyBase.UserId + "]授权[" + strLoginName + "]存取[" + objConnectionProperty.DataSource + "]数据库！")
                    End If
                Next

                '刷新显示
                If Me.getModuleData_YR(strErrMsg, Me.m_strQuery_YR) = False Then
                    GoTo errProc
                End If
                If Me.showModuleData_YR(strErrMsg) = False Then
                    GoTo errProc
                End If
                If Me.getModuleData_WR(strErrMsg, Me.m_strQuery_WR) = False Then
                    GoTo errProc
                End If
                If Me.showModuleData_WR(strErrMsg) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.BusinessFacade.systemAppManager.SafeRelease(objsystemAppManager)
            Xydc.Platform.web.TreeviewProcess.SafeRelease(objTreeviewProcess)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.BusinessFacade.systemAppManager.SafeRelease(objsystemAppManager)
            Xydc.Platform.web.TreeviewProcess.SafeRelease(objTreeviewProcess)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub doRevokeDatabase(ByVal strControlId As String)

            Dim objsystemAppManager As New Xydc.Platform.BusinessFacade.systemAppManager
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objTreeviewProcess As New Xydc.Platform.web.TreeviewProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '检查
                Dim intLevel As Integer
                intLevel = objTreeviewProcess.getLevelIndexFromNodeIndex(Me.tvwServers.SelectedNodeIndex)
                If intLevel < 3 Then
                    strErrMsg = "错误：必须选定数据库！"
                    GoTo errProc
                End If
                Dim objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty
                If Me.getServerConnectionProperty(strErrMsg, Me.tvwServers.SelectedNodeIndex, objConnectionProperty) = False Then
                    GoTo errProc
                End If
                Dim intSelected As Integer = 0
                Dim blnSelected As Boolean
                Dim intCount As Integer
                Dim i As Integer
                intCount = Me.grdYR.Items.Count
                For i = 0 To intCount - 1 Step 1
                    blnSelected = objDataGridProcess.isDataGridItemChecked(Me.grdYR.Items(i), 0, Me.m_cstrCheckBoxIdInDataGrid_YR)
                    If blnSelected = True Then
                        intSelected += 1
                    End If
                Next
                If intSelected < 1 Then
                    strErrMsg = "错误：未从上面的网格中选中要移出的人员！"
                    GoTo errProc
                End If

                '逐个移出成员
                Dim strLoginName As String
                Dim intColIndex As Integer
                intColIndex = objDataGridProcess.getDataGridColumnIndex(Me.grdYR, Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYDM)
                intCount = Me.grdYR.Items.Count
                For i = 0 To intCount - 1 Step 1
                    blnSelected = objDataGridProcess.isDataGridItemChecked(Me.grdYR.Items(i), 0, Me.m_cstrCheckBoxIdInDataGrid_YR)
                    If blnSelected = True Then
                        strLoginName = objDataGridProcess.getDataGridCellValue(Me.grdYR.Items(i), intColIndex)
                        If objsystemAppManager.doRevokeDatabase(strErrMsg, objConnectionProperty, strLoginName) = False Then
                            GoTo errProc
                        End If

                        '记录审计日志
                        Xydc.Platform.SystemFramework.ApplicationLog.WriteAuditAQInfo(Request.UserHostAddress, Request.UserHostName, "[" + MyBase.UserId + "]收回[" + strLoginName + "]存取[" + objConnectionProperty.DataSource + "]数据库的权限！")
                    End If
                Next

                '刷新显示
                If Me.getModuleData_YR(strErrMsg, Me.m_strQuery_YR) = False Then
                    GoTo errProc
                End If
                If Me.showModuleData_YR(strErrMsg) = False Then
                    GoTo errProc
                End If
                If Me.getModuleData_WR(strErrMsg, Me.m_strQuery_WR) = False Then
                    GoTo errProc
                End If
                If Me.showModuleData_WR(strErrMsg) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.BusinessFacade.systemAppManager.SafeRelease(objsystemAppManager)
            Xydc.Platform.web.TreeviewProcess.SafeRelease(objTreeviewProcess)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.BusinessFacade.systemAppManager.SafeRelease(objsystemAppManager)
            Xydc.Platform.web.TreeviewProcess.SafeRelease(objTreeviewProcess)
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

        Private Sub lnkMLGrant_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkMLGrant.Click
            Me.doGrantDatabase("lnkMLGrant")
        End Sub

        Private Sub lnkMLGrantB_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkMLGrantB.Click
            Me.doGrantDatabase("lnkMLGrantB")
        End Sub

        Private Sub lnkMLRevoke_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkMLRevoke.Click
            Me.doRevokeDatabase("lnkMLRevoke")
        End Sub

        Private Sub lnkMLRevokeB_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkMLRevokeB.Click
            Me.doRevokeDatabase("lnkMLRevokeB")
        End Sub

        Private Sub lnkMLRoleQX_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkMLRoleQX.Click

            Me.releaseModuleParameters()
            Me.releaseInterfaceParameters()
            Dim strUrl As String = "xtgl_sjqx_js.aspx"
            Response.Redirect(strUrl)

        End Sub

        Private Sub lnkMLRoleQXB_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkMLRoleQXB.Click

            Me.releaseModuleParameters()
            Me.releaseInterfaceParameters()
            Dim strUrl As String = "xtgl_sjqx_js.aspx"
            Response.Redirect(strUrl)

        End Sub

        Private Sub lnkMLUserQX_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkMLUserQX.Click

            Me.releaseModuleParameters()
            Me.releaseInterfaceParameters()
            Dim strUrl As String = "xtgl_sjqx_yh.aspx"
            Response.Redirect(strUrl)

        End Sub

        Private Sub lnkMLUserQXB_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkMLUserQXB.Click

            Me.releaseModuleParameters()
            Me.releaseInterfaceParameters()
            Dim strUrl As String = "xtgl_sjqx_yh.aspx"
            Response.Redirect(strUrl)

        End Sub

        Private Sub lnkMLClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkMLClose.Click
            Me.doClose("lnkMLClose")
        End Sub

        Private Sub lnkMLCloseB_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkMLCloseB.Click
            Me.doClose("lnkMLCloseB")
        End Sub

    End Class
End Namespace
