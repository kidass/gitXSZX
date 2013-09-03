Imports System.Web.Security

Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.web
    ' 类名    ：xtgl_sjdx
    ' 
    ' 调用性质：
    '     独立运行
    '
    ' 功能描述： 
    '   　数据库中服务器、数据库、数据库内对象管理
    '----------------------------------------------------------------


    Partial Public Class xtgl_sjdx
        Inherits Xydc.Platform.web.PageBase


        '----------------------------------------------------------------
        '模块私用参数
        '----------------------------------------------------------------
        '本模块相对image的相对路径
        Private m_cstrRelativePathToImage As String = "../../"

        '----------------------------------------------------------------
        '模块授权参数
        '----------------------------------------------------------------
        Private m_cstrPrevilegeParamPrefix As String = "xtgl_sjdx_previlege_param"
        Private m_blnPrevilegeParams(10) As Boolean

        '----------------------------------------------------------------
        '模块现场保留参数，恢复完成后立即释放session资源
        '----------------------------------------------------------------
        Private m_objSaveScence As Xydc.Platform.BusinessFacade.IMXtglSjdx
        Private m_blnSaveScence As Boolean

        '----------------------------------------------------------------
        '模块接口参数
        '----------------------------------------------------------------
        Private m_objInterface As Xydc.Platform.BusinessFacade.IXtglSjdx
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
                If Me.m_objSaveScence Is Nothing Then Exit Try

                With Me.m_objSaveScence
                    Me.htxtQuery.Value = .htxtQuery
                    Me.htxtRows.Value = .htxtRows
                    Me.htxtSort.Value = .htxtSort
                    Me.htxtSortColumnIndex.Value = .htxtSortColumnIndex
                    Me.htxtSortType.Value = .htxtSortType

                    Me.htxtDivLeftBody.Value = .htxtDivLeftBody
                    Me.htxtDivTopBody.Value = .htxtDivTopBody
                    Me.htxtDivLeftObject.Value = .htxtDivLeftObject
                    Me.htxtDivTopObject.Value = .htxtDivTopObject

                    Me.txtPageIndex.Text = .txtPageIndex
                    Me.txtPageSize.Text = .txtPageSize

                    Me.txtSearchDXM.Text = .txtSearchDXM
                    Me.txtSearchDXZWM.Text = .txtSearchDXZWM
                    Me.txtSearchDXSM.Text = .txtSearchDXSM

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
                If strSessionId = "" Then Exit Try

                '创建对象
                Me.m_objSaveScence = New Xydc.Platform.BusinessFacade.IMXtglSjdx

                '保存现场信息
                With Me.m_objSaveScence
                    .htxtQuery = Me.htxtQuery.Value
                    .htxtRows = Me.htxtRows.Value
                    .htxtSort = Me.htxtSort.Value
                    .htxtSortColumnIndex = Me.htxtSortColumnIndex.Value
                    .htxtSortType = Me.htxtSortType.Value

                    .htxtDivLeftBody = Me.htxtDivLeftBody.Value
                    .htxtDivTopBody = Me.htxtDivTopBody.Value
                    .htxtDivLeftObject = Me.htxtDivLeftObject.Value
                    .htxtDivTopObject = Me.htxtDivTopObject.Value

                    .txtPageIndex = Me.txtPageIndex.Text
                    .txtPageSize = Me.txtPageSize.Text

                    .txtSearchDXM = Me.txtSearchDXM.Text
                    .txtSearchDXZWM = Me.txtSearchDXZWM.Text
                    .txtSearchDXSM = Me.txtSearchDXSM.Text

                    .grdObjectPageSize = Me.grdObject.PageSize
                    .grdObjectCurrentPageIndex = Me.grdObject.CurrentPageIndex
                    .grdObjectSelectedIndex = Me.grdObject.SelectedIndex

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

                '=================================================================
                Dim objIXtglSjdxFwq As Xydc.Platform.BusinessFacade.IXtglSjdxFwq
                Try
                    objIXtglSjdxFwq = CType(Session.Item(Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.OSessionId)), Xydc.Platform.BusinessFacade.IXtglSjdxFwq)
                Catch ex As Exception
                    objIXtglSjdxFwq = Nothing
                End Try
                If Not (objIXtglSjdxFwq Is Nothing) Then
                    '释放资源
                    Session.Remove(Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.OSessionId))
                    objIXtglSjdxFwq.Dispose()
                    objIXtglSjdxFwq = Nothing
                    Exit Try
                End If

                '=================================================================
                Dim objIXtglSjdxSjk As Xydc.Platform.BusinessFacade.IXtglSjdxSjk
                Try
                    objIXtglSjdxSjk = CType(Session.Item(Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.OSessionId)), Xydc.Platform.BusinessFacade.IXtglSjdxSjk)
                Catch ex As Exception
                    objIXtglSjdxSjk = Nothing
                End Try
                If Not (objIXtglSjdxSjk Is Nothing) Then
                    '释放资源
                    Session.Remove(Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.OSessionId))
                    objIXtglSjdxSjk.Dispose()
                    objIXtglSjdxSjk = Nothing
                    Exit Try
                End If

                '=================================================================
                Dim objIXtglSjdxSjkdx As Xydc.Platform.BusinessFacade.IXtglSjdxSjkdx
                Try
                    objIXtglSjdxSjkdx = CType(Session.Item(Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.OSessionId)), Xydc.Platform.BusinessFacade.IXtglSjdxSjkdx)
                Catch ex As Exception
                    objIXtglSjdxSjkdx = Nothing
                End Try
                If Not (objIXtglSjdxSjkdx Is Nothing) Then
                    '释放资源
                    Session.Remove(Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.OSessionId))
                    objIXtglSjdxSjkdx.Dispose()
                    objIXtglSjdxSjkdx = Nothing
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
                    m_objInterface = CType(objTemp, Xydc.Platform.BusinessFacade.IXtglSjdx)
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
                        Me.m_objSaveScence = CType(Session.Item(strSessionId), Xydc.Platform.BusinessFacade.IMXtglSjdx)
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
                    Me.m_intRows_Object = .getObjectValue(Me.htxtRows.Value, 0)
                    Me.m_strQuery_Object = Me.htxtQuery.Value
                    Me.m_intFixedColumns_Object = .getObjectValue(Me.htxtOBJECTFixed.Value, 0)
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
                '按对象名搜索
                Dim strDXM As String
                strDXM = "a." + Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_DUIXIANG_DXMC
                If Me.txtSearchDXM.Text.Length > 0 Then Me.txtSearchDXM.Text = Me.txtSearchDXM.Text.Trim()
                If Me.txtSearchDXM.Text <> "" Then
                    Me.txtSearchDXM.Text = objPulicParameters.getNewSearchString(Me.txtSearchDXM.Text)
                    If strQuery = "" Then
                        strQuery = strDXM + " like '" + Me.txtSearchDXM.Text + "%'"
                    Else
                        strQuery = strQuery + " and " + strDXM + " like '" + Me.txtSearchDXM.Text + "%'"
                    End If
                End If

                '按对象中文名搜索
                Dim strDXZWM As String
                strDXZWM = "a." + Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_DUIXIANG_DXZWM
                If Me.txtSearchDXZWM.Text.Length > 0 Then Me.txtSearchDXZWM.Text = Me.txtSearchDXZWM.Text.Trim()
                If Me.txtSearchDXZWM.Text <> "" Then
                    Me.txtSearchDXZWM.Text = objPulicParameters.getNewSearchString(Me.txtSearchDXZWM.Text)
                    If strQuery = "" Then
                        strQuery = strDXZWM + " like '" + Me.txtSearchDXZWM.Text + "%'"
                    Else
                        strQuery = strQuery + " and " + strDXZWM + " like '" + Me.txtSearchDXZWM.Text + "%'"
                    End If
                End If

                '按对象说明搜索
                Dim strSM As String
                strSM = "a." + Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_DUIXIANG_SM
                If Me.txtSearchDXSM.Text.Length > 0 Then Me.txtSearchDXSM.Text = Me.txtSearchDXSM.Text.Trim()
                If Me.txtSearchDXSM.Text <> "" Then
                    Me.txtSearchDXSM.Text = objPulicParameters.getNewSearchString(Me.txtSearchDXSM.Text)
                    If strQuery = "" Then
                        strQuery = strSM + " like '" + Me.txtSearchDXSM.Text + "%'"
                    Else
                        strQuery = strQuery + " and " + strSM + " like '" + Me.txtSearchDXSM.Text + "%'"
                    End If
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

            Dim strTable As String = Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_DUIXIANG
            Dim objsystemAppManager As New Xydc.Platform.BusinessFacade.systemAppManager

            getModuleData_Object = False

            Try
                '根据当前节点获取服务器参数
                Dim objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty
                If Me.getServerConnectionProperty(strErrMsg, Me.tvwServers.SelectedNodeIndex, objConnectionProperty) = False Then
                    GoTo errProc
                End If

                '备份Sort字符串
                Dim strSort As String
                strSort = Me.htxtSort.Value
                If strSort.Length > 0 Then strSort = strSort.Trim

                '释放资源
                If Not (Me.m_objDataSet_Object Is Nothing) Then
                    Me.m_objDataSet_Object.Dispose()
                    Me.m_objDataSet_Object = Nothing
                End If

                '重新检索数据
                If objsystemAppManager.getDuixiangData(strErrMsg, objConnectionProperty, strWhere, Me.m_objDataSet_Object) = False Then
                    GoTo errProc
                End If

                '恢复Sort字符串
                With Me.m_objDataSet_Object.Tables(strTable)
                    .DefaultView.Sort = strSort
                End With

                '缓存参数
                With Me.m_objDataSet_Object.Tables(strTable)
                    Me.htxtRows.Value = .DefaultView.Count.ToString()
                    Me.m_intRows_Object = .DefaultView.Count
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.BusinessFacade.systemAppManager.SafeRelease(objsystemAppManager)

            getModuleData_Object = True
            Exit Function

errProc:
            Xydc.Platform.BusinessFacade.systemAppManager.SafeRelease(objsystemAppManager)
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
                Me.htxtQuery.Value = Me.m_strQuery_Object

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

            Dim strTable As String = Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_DUIXIANG
            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess

            showDataGridInfo_Object = False

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

            Dim strTable As String = Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_DUIXIANG
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

                '显示操作命令
                Me.lnkMLAutoReg.Enabled = Me.m_blnPrevilegeParams(1)
                Me.lnkMLUserReg.Enabled = Me.m_blnPrevilegeParams(2)
                Me.lnkMLUpdateReg.Enabled = Me.m_blnPrevilegeParams(3)
                Me.lnkMLDeleteReg.Enabled = Me.m_blnPrevilegeParams(4)
                Me.lnkMLSelectReg.Enabled = Me.m_blnPrevilegeParams(5)
                Me.lnkMLUpdateDB.Enabled = Me.m_blnPrevilegeParams(6)
                Me.lnkMLSelectDB.Enabled = Me.m_blnPrevilegeParams(7)
                Me.lnkMLUpdateDX.Enabled = Me.m_blnPrevilegeParams(8)
                Me.lnkMLSelectDX.Enabled = Me.m_blnPrevilegeParams(9)
                Me.lnkMLClearData.Enabled = Me.m_blnPrevilegeParams(10)
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
                        .doTranslateKey(Me.txtSearchDXM)
                        .doTranslateKey(Me.txtSearchDXZWM)
                        .doTranslateKey(Me.txtSearchDXSM)
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

        '----------------------------------------------------------------
        ' 从tvwServers中指定的索引strNodeIndex获取当前服务器的参数
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
            If Not (objConnectionProperty Is Nothing) Then
                Xydc.Platform.Common.Utilities.ConnectionProperty.SafeRelease(objConnectionProperty)
            End If
            objConnectionProperty = Nothing

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
                    Xydc.Platform.SystemFramework.ApplicationLog.WriteAuditAQInfo(Request.UserHostAddress, Request.UserHostName, "[" + MyBase.UserId + "]访问了[服务器、数据库、数据库对象注册信息]！")
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
                Me.tvwServers.SelectedNodeIndex = e.NewNode

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

            Dim strTable As String = Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_DUIXIANG
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
                Me.htxtSortColumnIndex.Value = intColumnIndex.ToString()
                Me.htxtSortType.Value = CType(objenumSortType, Integer).ToString()
                Me.htxtSort.Value = strFinalCommand

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
        Private Sub doAutoRegister(ByVal strControlId As String)

            Dim objsystemAppManager As New Xydc.Platform.BusinessFacade.systemAppManager
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '获取连接串加密数据
                Dim strConnect As String
                With New Xydc.Platform.Common.jsoaConfiguration
                    strConnect = .getConnectionString(MyBase.UserId, MyBase.UserPassword)
                End With
                Dim bData() As Byte
                With New Xydc.Platform.Common.Utilities.PulicParameters
                    If .doEncryptString(strErrMsg, strConnect, bData) = False Then
                        GoTo errProc
                    End If
                End With

                '准备输入参数
                Dim objNewData As New System.Collections.Specialized.ListDictionary
                objNewData.Add(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_FUWUQI_MC, Xydc.Platform.Common.jsoaConfiguration.DatabaseServerName)
                objNewData.Add(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_FUWUQI_LX, "SQL Server")
                objNewData.Add(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_FUWUQI_TGZ, "SQLOLEDB")
                objNewData.Add(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_FUWUQI_LJC, bData)
                objNewData.Add(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_FUWUQI_SM, " ")

                '注册
                If objsystemAppManager.doSaveFuwuqiData(strErrMsg, MyBase.UserId, MyBase.UserPassword, Nothing, objNewData, Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew) = False Then
                    GoTo errProc
                End If

                '记录审计日志
                Xydc.Platform.SystemFramework.ApplicationLog.WriteAuditAQInfo(Request.UserHostAddress, Request.UserHostName, "[" + MyBase.UserId + "]注册了[" + Xydc.Platform.Common.jsoaConfiguration.DatabaseServerName + "]服务器！")

                '显示信息
                objMessageProcess.doAlertMessage(Me.popMessageObject, "提示：当前服务器已经自动注册成功！")

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.BusinessFacade.systemAppManager.SafeRelease(objsystemAppManager)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.BusinessFacade.systemAppManager.SafeRelease(objsystemAppManager)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub doAddNewServer(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '获取服务器参数
                Dim objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty
                Me.getServerConnectionProperty(strErrMsg, Me.tvwServers.SelectedNodeIndex, objConnectionProperty)

                '备份现场参数
                Dim strSessionId As String
                strSessionId = Me.saveModuleInformation()
                If strSessionId = "" Then
                    strErrMsg = "错误：不能保存现场信息！"
                    GoTo errProc
                End If

                '准备调用接口
                Dim objIXtglSjdxFwq As Xydc.Platform.BusinessFacade.IXtglSjdxFwq
                Dim strUrl As String
                objIXtglSjdxFwq = New Xydc.Platform.BusinessFacade.IXtglSjdxFwq
                With objIXtglSjdxFwq
                    .iEditMode = Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                    If objConnectionProperty Is Nothing Then
                        .iFWQMC = ""
                    Else
                        .iFWQMC = objConnectionProperty.DataSource
                    End If

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
                Session.Add(strNewSessionId, objIXtglSjdxFwq)

                strUrl = ""
                strUrl += "xtgl_sjdx_fwq.aspx"
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

        Private Sub doUpdateServer(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '获取服务器参数
                Dim objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty
                Me.getServerConnectionProperty(strErrMsg, Me.tvwServers.SelectedNodeIndex, objConnectionProperty)
                If objConnectionProperty Is Nothing Then
                    strErrMsg = "错误：未选择服务器！"
                    GoTo errProc
                End If

                '备份现场参数
                Dim strSessionId As String
                strSessionId = Me.saveModuleInformation()
                If strSessionId = "" Then
                    strErrMsg = "错误：不能保存现场信息！"
                    GoTo errProc
                End If

                '准备调用接口
                Dim objIXtglSjdxFwq As Xydc.Platform.BusinessFacade.IXtglSjdxFwq
                Dim strUrl As String
                objIXtglSjdxFwq = New Xydc.Platform.BusinessFacade.IXtglSjdxFwq
                With objIXtglSjdxFwq
                    .iEditMode = Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eUpdate
                    .iFWQMC = objConnectionProperty.DataSource

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
                Session.Add(strNewSessionId, objIXtglSjdxFwq)

                strUrl = ""
                strUrl += "xtgl_sjdx_fwq.aspx"
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

        Private Sub doOpenServer(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '获取服务器参数
                Dim objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty
                Me.getServerConnectionProperty(strErrMsg, Me.tvwServers.SelectedNodeIndex, objConnectionProperty)
                If objConnectionProperty Is Nothing Then
                    strErrMsg = "错误：未选择服务器！"
                    GoTo errProc
                End If

                '备份现场参数
                Dim strSessionId As String
                strSessionId = Me.saveModuleInformation()
                If strSessionId = "" Then
                    strErrMsg = "错误：不能保存现场信息！"
                    GoTo errProc
                End If

                '准备调用接口
                Dim objIXtglSjdxFwq As Xydc.Platform.BusinessFacade.IXtglSjdxFwq
                Dim strUrl As String
                objIXtglSjdxFwq = New Xydc.Platform.BusinessFacade.IXtglSjdxFwq
                With objIXtglSjdxFwq
                    .iEditMode = Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eSelect
                    .iFWQMC = objConnectionProperty.DataSource

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
                Session.Add(strNewSessionId, objIXtglSjdxFwq)

                strUrl = ""
                strUrl += "xtgl_sjdx_fwq.aspx"
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

        Private Sub doDeleteServer(ByVal strControlId As String)

            Dim objTreeviewProcess As New Xydc.Platform.web.TreeviewProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String
            Dim intStep As Integer

            Try
                Dim strZZDM As String

                '检查
                intStep = 1
                If Me.tvwServers.SelectedNodeIndex = "" Then
                    strErrMsg = "错误：没有选定服务器！"
                    GoTo errProc
                End If
                Dim objTreeNode As Microsoft.Web.UI.WebControls.TreeNode
                objTreeNode = Me.tvwServers.GetNodeFromIndex(Me.tvwServers.SelectedNodeIndex)
                If objTreeNode Is Nothing Then
                    strErrMsg = "错误：没有选定服务器！"
                    GoTo errProc
                End If
                Dim intLevel As Integer
                intLevel = objTreeviewProcess.getLevelIndexFromNodeIndex(Me.tvwServers.SelectedNodeIndex)
                If intLevel < 2 Then
                    strErrMsg = "错误：没有选定服务器！"
                    GoTo errProc
                End If
                Dim strNodeIndex As String
                strNodeIndex = objTreeviewProcess.getLevelIndexFromNodeIndex(Me.tvwServers.SelectedNodeIndex, 2, True)
                objTreeNode = Me.tvwServers.GetNodeFromIndex(strNodeIndex)
                If objTreeNode Is Nothing Then
                    strErrMsg = "错误：没有选定服务器！"
                    GoTo errProc
                End If
                Dim strServerName As String = objTreeNode.Text

                '询问
                intStep = 2
                If objMessageProcess.isExecuteCode(Request, Me.popMessageObject.UniqueID, intStep) = True Then
                    objMessageProcess.doConfirmMessage(Me.popMessageObject, "警告：删除当前服务器将同时删除相关的数据库、数据库内对象的信息，您确定要删除吗（是/否）？", strControlId, intStep)
                    Exit Try
                Else
                    objMessageProcess.doResetPopMessage(Me.popMessageObject)
                End If

                '删除处理
                intStep = 3
                If objMessageProcess.isExecuteCode(Request, Me.popMessageObject.UniqueID, intStep) = True Then
                    '删除
                    With New Xydc.Platform.BusinessFacade.systemAppManager
                        If .doDeleteFuwuqiData(strErrMsg, MyBase.UserId, MyBase.UserPassword, strServerName) = False Then
                            GoTo errProc
                        End If
                    End With

                    '记录审计日志
                    Xydc.Platform.SystemFramework.ApplicationLog.WriteAuditAQInfo(Request.UserHostAddress, Request.UserHostName, "[" + MyBase.UserId + "]删除了[" + strServerName + "]服务器注册信息！")

                    '刷新数据
                    Me.tvwServers.Nodes.Clear()
                    If Me.showTreeViewInfo_Server(strErrMsg) = False Then
                        GoTo errProc
                    End If
                    If Me.getModuleData_Object(strErrMsg, Me.m_strQuery_Object) = False Then
                        GoTo errProc
                    End If
                    If Me.showModuleData_Object(strErrMsg) = False Then
                        GoTo errProc
                    End If
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

        Private Sub doOpenDatabase(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '获取服务器参数
                Dim intLevel As Integer
                With New Xydc.Platform.web.TreeviewProcess
                    intLevel = .getLevelIndexFromNodeIndex(Me.tvwServers.SelectedNodeIndex)
                End With
                If intLevel < 3 Then
                    strErrMsg = "错误：未选择数据库！"
                    GoTo errProc
                End If
                Dim objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty
                Me.getServerConnectionProperty(strErrMsg, Me.tvwServers.SelectedNodeIndex, objConnectionProperty)
                If objConnectionProperty Is Nothing Then
                    strErrMsg = "错误：未选择数据库！"
                    GoTo errProc
                End If

                '备份现场参数
                Dim strSessionId As String
                strSessionId = Me.saveModuleInformation()
                If strSessionId = "" Then
                    strErrMsg = "错误：不能保存现场信息！"
                    GoTo errProc
                End If

                '准备调用接口
                Dim objIXtglSjdxSjk As Xydc.Platform.BusinessFacade.IXtglSjdxSjk
                Dim strUrl As String
                objIXtglSjdxSjk = New Xydc.Platform.BusinessFacade.IXtglSjdxSjk
                With objIXtglSjdxSjk
                    .iEditMode = Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eSelect
                    .iFWQMC = objConnectionProperty.DataSource
                    .iSJKMC = objConnectionProperty.InitialCatalog

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
                Session.Add(strNewSessionId, objIXtglSjdxSjk)

                strUrl = ""
                strUrl += "xtgl_sjdx_sjk.aspx"
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

        Private Sub doUpdateDatabase(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '获取服务器参数
                Dim intLevel As Integer
                With New Xydc.Platform.web.TreeviewProcess
                    intLevel = .getLevelIndexFromNodeIndex(Me.tvwServers.SelectedNodeIndex)
                End With
                If intLevel < 3 Then
                    strErrMsg = "错误：未选择数据库！"
                    GoTo errProc
                End If
                Dim objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty
                Me.getServerConnectionProperty(strErrMsg, Me.tvwServers.SelectedNodeIndex, objConnectionProperty)
                If objConnectionProperty Is Nothing Then
                    strErrMsg = "错误：未选择数据库！"
                    GoTo errProc
                End If

                '备份现场参数
                Dim strSessionId As String
                strSessionId = Me.saveModuleInformation()
                If strSessionId = "" Then
                    strErrMsg = "错误：不能保存现场信息！"
                    GoTo errProc
                End If

                '准备调用接口
                Dim objIXtglSjdxSjk As Xydc.Platform.BusinessFacade.IXtglSjdxSjk
                Dim strUrl As String
                objIXtglSjdxSjk = New Xydc.Platform.BusinessFacade.IXtglSjdxSjk
                With objIXtglSjdxSjk
                    .iEditMode = Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eUpdate
                    .iFWQMC = objConnectionProperty.DataSource
                    .iSJKMC = objConnectionProperty.InitialCatalog

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
                Session.Add(strNewSessionId, objIXtglSjdxSjk)

                strUrl = ""
                strUrl += "xtgl_sjdx_sjk.aspx"
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

        Private Sub doOpenObject(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '检查
                If Me.grdObject.SelectedIndex < 0 Then
                    strErrMsg = "错误：没有选择对象！"
                    GoTo errProc
                End If
                Dim intIndex As Integer = Me.grdObject.SelectedIndex
                Dim intColIndex As Integer
                Dim strFWQMC As String
                Dim strSJKMC As String
                Dim strDXLX As String
                Dim strDXMC As String
                intColIndex = objDataGridProcess.getDataGridColumnIndex(Me.grdObject, Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_DUIXIANG_FWQM)
                strFWQMC = objDataGridProcess.getDataGridCellValue(Me.grdObject.Items(intIndex), intColIndex)
                intColIndex = objDataGridProcess.getDataGridColumnIndex(Me.grdObject, Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_DUIXIANG_SJKM)
                strSJKMC = objDataGridProcess.getDataGridCellValue(Me.grdObject.Items(intIndex), intColIndex)
                intColIndex = objDataGridProcess.getDataGridColumnIndex(Me.grdObject, Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_DUIXIANG_DXLX)
                strDXLX = objDataGridProcess.getDataGridCellValue(Me.grdObject.Items(intIndex), intColIndex)
                intColIndex = objDataGridProcess.getDataGridColumnIndex(Me.grdObject, Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_DUIXIANG_DXMC)
                strDXMC = objDataGridProcess.getDataGridCellValue(Me.grdObject.Items(intIndex), intColIndex)

                '备份现场参数
                Dim strSessionId As String
                strSessionId = Me.saveModuleInformation()
                If strSessionId = "" Then
                    strErrMsg = "错误：不能保存现场信息！"
                    GoTo errProc
                End If

                '准备调用接口
                Dim objIXtglSjdxSjkdx As Xydc.Platform.BusinessFacade.IXtglSjdxSjkdx
                Dim strUrl As String
                objIXtglSjdxSjkdx = New Xydc.Platform.BusinessFacade.IXtglSjdxSjkdx
                With objIXtglSjdxSjkdx
                    .iEditMode = Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eSelect
                    .iFWQMC = strFWQMC
                    .iSJKMC = strSJKMC
                    .iDXLX = strDXLX
                    .iDXMC = strDXMC

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
                Session.Add(strNewSessionId, objIXtglSjdxSjkdx)

                strUrl = ""
                strUrl += "xtgl_sjdx_sjkdx.aspx"
                strUrl += "?"
                strUrl += Xydc.Platform.Common.Utilities.PulicParameters.ISessionId
                strUrl += "="
                strUrl += strNewSessionId
                Response.Redirect(strUrl)

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

        Private Sub doUpdateObject(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '检查
                If Me.grdObject.SelectedIndex < 0 Then
                    strErrMsg = "错误：没有选择对象！"
                    GoTo errProc
                End If
                Dim intIndex As Integer = Me.grdObject.SelectedIndex
                Dim intColIndex As Integer
                Dim strFWQMC As String
                Dim strSJKMC As String
                Dim strDXLX As String
                Dim strDXMC As String
                intColIndex = objDataGridProcess.getDataGridColumnIndex(Me.grdObject, Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_DUIXIANG_FWQM)
                strFWQMC = objDataGridProcess.getDataGridCellValue(Me.grdObject.Items(intIndex), intColIndex)
                intColIndex = objDataGridProcess.getDataGridColumnIndex(Me.grdObject, Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_DUIXIANG_SJKM)
                strSJKMC = objDataGridProcess.getDataGridCellValue(Me.grdObject.Items(intIndex), intColIndex)
                intColIndex = objDataGridProcess.getDataGridColumnIndex(Me.grdObject, Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_DUIXIANG_DXLX)
                strDXLX = objDataGridProcess.getDataGridCellValue(Me.grdObject.Items(intIndex), intColIndex)
                intColIndex = objDataGridProcess.getDataGridColumnIndex(Me.grdObject, Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_DUIXIANG_DXMC)
                strDXMC = objDataGridProcess.getDataGridCellValue(Me.grdObject.Items(intIndex), intColIndex)

                '备份现场参数
                Dim strSessionId As String
                strSessionId = Me.saveModuleInformation()
                If strSessionId = "" Then
                    strErrMsg = "错误：不能保存现场信息！"
                    GoTo errProc
                End If

                '准备调用接口
                Dim objIXtglSjdxSjkdx As Xydc.Platform.BusinessFacade.IXtglSjdxSjkdx
                Dim strUrl As String
                objIXtglSjdxSjkdx = New Xydc.Platform.BusinessFacade.IXtglSjdxSjkdx
                With objIXtglSjdxSjkdx
                    .iEditMode = Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eUpdate
                    .iFWQMC = strFWQMC
                    .iSJKMC = strSJKMC
                    .iDXLX = strDXLX
                    .iDXMC = strDXMC

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
                Session.Add(strNewSessionId, objIXtglSjdxSjkdx)

                strUrl = ""
                strUrl += "xtgl_sjdx_sjkdx.aspx"
                strUrl += "?"
                strUrl += Xydc.Platform.Common.Utilities.PulicParameters.ISessionId
                strUrl += "="
                strUrl += strNewSessionId
                Response.Redirect(strUrl)

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

        Private Sub doCleanData(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String
            Dim intStep As Integer

            Try
                '询问
                intStep = 1
                If objMessageProcess.isExecuteCode(Request, Me.popMessageObject.UniqueID, intStep) = True Then
                    objMessageProcess.doConfirmMessage(Me.popMessageObject, "警告：您确定要清理无效的数据吗（是/否）？", strControlId, intStep)
                    Exit Try
                Else
                    objMessageProcess.doResetPopMessage(Me.popMessageObject)
                End If

                '执行清理
                intStep = 2
                If objMessageProcess.isExecuteCode(Request, Me.popMessageObject.UniqueID, intStep) = True Then
                    With New Xydc.Platform.BusinessFacade.systemAppManager
                        If .doAutoCleanManageData(strErrMsg, MyBase.UserId, MyBase.UserPassword) = False Then
                            GoTo errProc
                        End If
                    End With
                    objMessageProcess.doAlertMessage(Me.popMessageObject, "提示：成功清理无效数据！")
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

        Private Sub lnkMLAutoReg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkMLAutoReg.Click
            Me.doAutoRegister("lnkMLAutoReg")
        End Sub

        Private Sub lnkMLUserReg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkMLUserReg.Click
            Me.doAddNewServer("lnkMLUserReg")
        End Sub

        Private Sub lnkMLUpdateReg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkMLUpdateReg.Click
            Me.doUpdateServer("lnkMLUpdateReg")
        End Sub

        Private Sub lnkMLSelectReg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkMLSelectReg.Click
            Me.doOpenServer("lnkMLSelectReg")
        End Sub

        Private Sub lnkMLDeleteReg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkMLDeleteReg.Click
            Me.doDeleteServer("lnkMLDeleteReg")
        End Sub

        Private Sub lnkMLSelectDB_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkMLSelectDB.Click
            Me.doOpenDatabase("lnkMLSelectDB")
        End Sub

        Private Sub lnkMLUpdateDB_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkMLUpdateDB.Click
            Me.doUpdateDatabase("lnkMLUpdateDB")
        End Sub

        Private Sub lnkMLSelectDX_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkMLSelectDX.Click
            Me.doOpenObject("lnkMLSelectDX")
        End Sub

        Private Sub lnkMLUpdateDX_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkMLUpdateDX.Click
            Me.doUpdateObject("lnkMLUpdateDX")
        End Sub

        Private Sub lnkMLClearData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkMLClearData.Click
            Me.doCleanData("lnkMLClearData")
        End Sub

        Private Sub lnkMLClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkMLClose.Click
            Me.doClose("lnkMLClose")
        End Sub


    End Class
End Namespace
