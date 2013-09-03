Imports System.Web.Security

Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.web
    ' 类名    ：xtgl_sjqx_js
    ' 
    ' 调用性质：
    '     独立运行
    '
    ' 功能描述： 
    '   　基于角色的授权处理
    '----------------------------------------------------------------

    Partial Public Class xtgl_sjqx_js
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
        Private m_objSaveScence As Xydc.Platform.BusinessFacade.IMXtglSjqxJs
        Private m_blnSaveScence As Boolean

        '----------------------------------------------------------------
        '模块接口参数
        '----------------------------------------------------------------
        Private m_objInterface As Xydc.Platform.BusinessFacade.IXtglSjqxJs
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

                    Me.txtSearchDXM.Text = .txtSearchDXM
                    Me.txtSearchDXZWM.Text = .txtSearchDXZWM
                    Me.txtSearchDXLX.Text = .txtSearchDXLX

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
                                    False, Microsoft.Web.UI.WebControls.ExpandableValue.CheckOnce, True) = False Then
                                    Exit For
                                End If

                            Case 3 '展开角色
                                objTreeNode = Me.tvwServers.GetNodeFromIndex(strIndex)
                                If objTreeNode Is Nothing Then Exit For
                                If objTreeviewProcess.doShowTreeNodeChildren(strErrMsg, objTreeNode, _
                                    objAppManagerData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_JIAOSE), _
                                    Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_JIAOSE_UID, _
                                    Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_JIAOSE_NAME, _
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
                Me.m_objSaveScence = New Xydc.Platform.BusinessFacade.IMXtglSjqxJs

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

                    .txtSearchDXM = Me.txtSearchDXM.Text
                    .txtSearchDXZWM = Me.txtSearchDXZWM.Text
                    .txtSearchDXLX = Me.txtSearchDXLX.Text

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
                    m_objInterface = CType(objTemp, Xydc.Platform.BusinessFacade.IXtglSjqxJs)
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
                        Me.m_objSaveScence = CType(Session.Item(strSessionId), Xydc.Platform.BusinessFacade.IMXtglSjqxJs)
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
                    '记录m_objDataSet_Object的DefaultView记录数
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
                '按对象名称搜索
                Dim strDXMC As String
                strDXMC = "a." + Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_DUIXIANGQX_DXMC
                If Me.txtSearchDXM.Text.Length > 0 Then Me.txtSearchDXM.Text = Me.txtSearchDXM.Text.Trim()
                If Me.txtSearchDXM.Text <> "" Then
                    Me.txtSearchDXM.Text = objPulicParameters.getNewSearchString(Me.txtSearchDXM.Text)
                    If strQuery = "" Then
                        strQuery = strDXMC + " like '" + Me.txtSearchDXM.Text + "%'"
                    Else
                        strQuery = strQuery + " and " + strDXMC + " like '" + Me.txtSearchDXM.Text + "%'"
                    End If
                End If

                '按对象中文名搜索
                Dim strDXZWM As String
                strDXZWM = "a." + Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_DUIXIANGQX_DXZWM
                If Me.txtSearchDXZWM.Text.Length > 0 Then Me.txtSearchDXZWM.Text = Me.txtSearchDXZWM.Text.Trim()
                If Me.txtSearchDXZWM.Text <> "" Then
                    Me.txtSearchDXZWM.Text = objPulicParameters.getNewSearchString(Me.txtSearchDXZWM.Text)
                    If strQuery = "" Then
                        strQuery = strDXZWM + " like '" + Me.txtSearchDXZWM.Text + "%'"
                    Else
                        strQuery = strQuery + " and " + strDXZWM + " like '" + Me.txtSearchDXZWM.Text + "%'"
                    End If
                End If

                '按对象类型搜索
                Dim strDXLX As String
                strDXLX = "a." + Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_DUIXIANGQX_DXLX
                If Me.txtSearchDXLX.Text.Length > 0 Then Me.txtSearchDXLX.Text = Me.txtSearchDXLX.Text.Trim()
                If Me.txtSearchDXLX.Text <> "" Then
                    If strQuery = "" Then
                        strQuery = strDXLX + " = '" + Me.txtSearchDXLX.Text + "'"
                    Else
                        strQuery = strQuery + " and " + strDXLX + " = '" + Me.txtSearchDXLX.Text + "'"
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
        ' 从tvwServers中指定的索引strNodeIndex获取当前服务器的参数
        '     strErrMsg             ：返回错误信息
        '     strNodeIndex          ：tvwServers的索引号
        '     objConnectionProperty ：返回服务器参数
        '     strRoleName           ：返回角色名
        ' 返回
        '     True                  ：成功
        '     False                 ：失败
        '----------------------------------------------------------------
        Private Function getServerConnectionProperty( _
            ByRef strErrMsg As String, _
            ByVal strNodeIndex As String, _
            ByRef objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByRef strRoleName As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objsystemAppManager As New Xydc.Platform.BusinessFacade.systemAppManager
            Dim objTreeviewProcess As New Xydc.Platform.web.TreeviewProcess
            Dim objAppManagerData As Xydc.Platform.Common.Data.AppManagerData

            getServerConnectionProperty = False
            If Not (objConnectionProperty Is Nothing) Then
                Xydc.Platform.Common.Utilities.ConnectionProperty.SafeRelease(objConnectionProperty)
            End If
            objConnectionProperty = Nothing
            strRoleName = ""

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

                    Case 4    '角色层
                        strRoleName = objTreeNode.Text

                        '获取服务器名
                        Dim objTreeNodeDB As Microsoft.Web.UI.WebControls.TreeNode
                        Dim strIndexDB As String
                        strIndexDB = objTreeviewProcess.getLevelIndexFromNodeIndex(objTreeNode.GetNodeIndex, intLevel - 2, True)
                        objTreeNodeDB = Me.tvwServers.GetNodeFromIndex(strIndexDB)
                        If objTreeNodeDB Is Nothing Then
                            Exit Try
                        End If
                        strServerName = objTreeNodeDB.Text

                        '获取数据库名
                        strIndexDB = objTreeviewProcess.getLevelIndexFromNodeIndex(objTreeNode.GetNodeIndex, intLevel - 1, True)
                        objTreeNodeDB = Me.tvwServers.GetNodeFromIndex(strIndexDB)
                        If objTreeNodeDB Is Nothing Then
                            Exit Try
                        End If
                        strDBName = objTreeNodeDB.Text

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
        ' 获取grdObject要显示的数据信息
        '     strErrMsg      ：返回错误信息
        '     strWhere       ：搜索条件
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function getModuleData_Object( _
            ByRef strErrMsg As String, _
            ByVal strWhere As String) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_DUIXIANGQX
            Dim objsystemAppManager As New Xydc.Platform.BusinessFacade.systemAppManager
            Dim objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty

            getModuleData_Object = False

            Try
                '从tvwServers中获取信息
                Dim strRoleName As String
                If Me.getServerConnectionProperty(strErrMsg, Me.tvwServers.SelectedNodeIndex, objConnectionProperty, strRoleName) = False Then
                    GoTo errProc
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
                If objsystemAppManager.getRolePermissionsData(strErrMsg, objConnectionProperty, strRoleName, strWhere, Me.m_objDataSet_Object) = False Then
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

            Xydc.Platform.Common.Utilities.ConnectionProperty.SafeRelease(objConnectionProperty)
            Xydc.Platform.BusinessFacade.systemAppManager.SafeRelease(objsystemAppManager)

            getModuleData_Object = True
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
                        If Me.getServerConnectionProperty(strErrMsg, strNodeIndex, objConnectionProperty, strRoleName) = False Then
                            GoTo errProc
                        End If
                        If objsystemAppManager.getShujukuData(strErrMsg, objConnectionProperty, "", objAppManagerData) = False Then
                            GoTo errProc
                        End If

                    Case 3    '准备获取角色数据
                        Dim objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty
                        Dim strRoleName As String
                        If Me.getServerConnectionProperty(strErrMsg, strNodeIndex, objConnectionProperty, strRoleName) = False Then
                            GoTo errProc
                        End If
                        If objsystemAppManager.getRoleData(strErrMsg, objConnectionProperty, "", objAppManagerData) = False Then
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
        ' 根据屏幕搜索条件搜索grdObject数据
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function searchModuleData_Object(ByRef strErrMsg As String) As Boolean

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

            Dim strTable As String = Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_DUIXIANGQX
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
                With Me.m_objDataSet_Object.Tables(strTable)
                    If objDataGridProcess.onBeforeDataGridBind(strErrMsg, Me.grdObject, .DefaultView.Count) = False Then
                        GoTo errProc
                    End If
                End With

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
        Private Function showModuleData_Object(ByRef strErrMsg As String) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_DUIXIANGQX
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess

            showModuleData_Object = False

            Try
                '显示网格信息
                If Me.showDataGridInfo_Object(strErrMsg) = False Then
                    GoTo errProc
                End If

                '显示与网格紧密相关的操作或信息提示
                With Me.m_objDataSet_Object.Tables(strTable).DefaultView
                    '显示网格位置信息
                    Me.lblGridLocInfo.Text = objDataGridProcess.getDataGridLocation(Me.grdObject, .Count)

                    '显示页面浏览功能
                    Me.lnkCZMoveFirst.Enabled = objDataGridProcess.canDoMoveFirstPage(Me.grdObject, .Count)
                    Me.lnkCZMoveLast.Enabled = objDataGridProcess.canDoMoveLastPage(Me.grdObject, .Count)
                    Me.lnkCZMovePrev.Enabled = objDataGridProcess.canDoMovePreviousPage(Me.grdObject, .Count)
                    Me.lnkCZMoveNext.Enabled = objDataGridProcess.canDoMoveNextPage(Me.grdObject, .Count)

                    '显示相关操作
                    Dim blnEnabled As Boolean
                    If .Count < 1 Then
                        blnEnabled = False
                    Else
                        blnEnabled = True
                    End If
                    Me.lnkCZDeSelectAll.Enabled = blnEnabled
                    Me.lnkCZSelectAll.Enabled = blnEnabled
                    Me.lnkCZGotoPage.Enabled = blnEnabled
                    Me.lnkCZSetPageSize.Enabled = blnEnabled
                End With

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
        ' 显示模块级操作状态
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showModuleData_MAIN(ByRef strErrMsg As String) As Boolean

            showModuleData_MAIN = False

            Try
                Me.lnkMLAccsQX.Enabled = Me.m_blnPrevilegeParams(1)
                Me.lnkMLUserQX.Enabled = Me.m_blnPrevilegeParams(7)
                Me.lnkMLGrant.Enabled = Me.m_blnPrevilegeParams(5)
                Me.lnkMLRevoke.Enabled = Me.m_blnPrevilegeParams(6)

                Me.lnkMLClose.Enabled = True

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
                        .doTranslateKey(Me.txtPageIndex)
                        .doTranslateKey(Me.txtPageSize)

                        .doTranslateKey(Me.txtSearchDXM)
                        .doTranslateKey(Me.txtSearchDXZWM)
                        .doTranslateKey(Me.txtSearchDXLX)
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '显示模块级操作
                If Me.showModuleData_MAIN(strErrMsg) = False Then
                    GoTo errProc
                End If

                '显示授权数据
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

            '记录访问日志
            If Me.IsPostBack = False Then
                If Me.m_blnSaveScence = False Then
                    Xydc.Platform.SystemFramework.ApplicationLog.WriteAuditAQInfo(Request.UserHostAddress, Request.UserHostName, "[" + MyBase.UserId + "]访问了[角色的数据库对象授权数据]！")
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
                    Exit Try '已经展开过
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
                            False, Microsoft.Web.UI.WebControls.ExpandableValue.CheckOnce, False) = False Then
                            GoTo errProc
                        End If

                    Case 3 '展开角色
                        If objTreeviewProcess.doShowTreeNodeChildren(strErrMsg, objTreeNode, _
                            objAppManagerData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_JIAOSE), _
                            Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_JIAOSE_UID, _
                            Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_JIAOSE_NAME, _
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

        '实现对grdObject网格行、列的固定
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

            Dim strTable As String = Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_DUIXIANGQX
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
        Private Sub doGrant(ByVal strControlId As String)

            Dim objsystemAppManager As New Xydc.Platform.BusinessFacade.systemAppManager
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objTreeviewProcess As New Xydc.Platform.web.TreeviewProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim objAppManagerData As New Xydc.Platform.Common.Data.AppManagerData
            Dim objOptions As New System.Collections.Specialized.ListDictionary
            Dim strErrMsg As String

            Try
                '检查
                Dim intLevel As Integer
                intLevel = objTreeviewProcess.getLevelIndexFromNodeIndex(Me.tvwServers.SelectedNodeIndex)
                If intLevel < 4 Then
                    strErrMsg = "错误：未选择角色！"
                    GoTo errProc
                End If
                Dim objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty
                Dim strRoleName As String
                If Me.getServerConnectionProperty(strErrMsg, Me.tvwServers.SelectedNodeIndex, objConnectionProperty, strRoleName) = False Then
                    GoTo errProc
                End If
                Dim intSelected As Integer = 0
                Dim blnSelected As Boolean
                Dim intCount As Integer
                Dim i As Integer
                intCount = Me.grdObject.Items.Count
                For i = 0 To intCount - 1 Step 1
                    blnSelected = objDataGridProcess.isDataGridItemChecked(Me.grdObject.Items(i), 0, Me.m_cstrCheckBoxIdInDataGrid_Object)
                    If blnSelected = True Then
                        intSelected += 1
                    End If
                Next
                If intSelected < 1 Then
                    strErrMsg = "错误：未选定要授予权限的对象！"
                    GoTo errProc
                End If
                Dim objenumPermissionType As Xydc.Platform.Common.Data.AppManagerData.enumPermissionType
                intCount = Me.cblObjectQX.Items.Count
                For i = 0 To intCount - 1 Step 1
                    If Me.cblObjectQX.Items(i).Selected = True Then
                        objenumPermissionType = objAppManagerData.getPermissionType(Me.cblObjectQX.Items(i).Value)
                        objOptions.Add(objenumPermissionType, True)
                    End If
                Next
                If objOptions.Count < 1 Then
                    strErrMsg = "错误：未选定授予权限选项！"
                    GoTo errProc
                End If

                '逐个授权
                Dim intColIndex(2) As Integer
                Dim strObjectName As String
                Dim strObjectType As String
                intColIndex(0) = objDataGridProcess.getDataGridColumnIndex(Me.grdObject, Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_DUIXIANG_DXMC)
                intColIndex(1) = objDataGridProcess.getDataGridColumnIndex(Me.grdObject, Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_DUIXIANG_DXLX)
                intCount = Me.grdObject.Items.Count
                For i = 0 To intCount - 1 Step 1
                    blnSelected = objDataGridProcess.isDataGridItemChecked(Me.grdObject.Items(i), 0, Me.m_cstrCheckBoxIdInDataGrid_Object)
                    If blnSelected = True Then
                        strObjectName = objDataGridProcess.getDataGridCellValue(Me.grdObject.Items(i), intColIndex(0))
                        strObjectType = objDataGridProcess.getDataGridCellValue(Me.grdObject.Items(i), intColIndex(1))
                        If objsystemAppManager.doGrantRole(strErrMsg, objConnectionProperty, strRoleName, strObjectName, strObjectType, objOptions) = False Then
                            GoTo errProc
                        End If

                        '记录审计日志
                        Xydc.Platform.SystemFramework.ApplicationLog.WriteAuditAQInfo(Request.UserHostAddress, Request.UserHostName, "[" + MyBase.UserId + "]向[" + strRoleName + "]角色授予[" + strObjectName + "]的相关权限！")
                    End If
                Next

                '刷新显示
                If Me.getModuleData_Object(strErrMsg, Me.m_strQuery_Object) = False Then
                    GoTo errProc
                End If
                If Me.showModuleData_Object(strErrMsg) = False Then
                    GoTo errProc
                End If

                '提示成功
                objMessageProcess.doAlertMessage(Me.popMessageObject, "提示：选定[" + intSelected.ToString() + "]个对象已经成功授予权限！")

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.BusinessFacade.systemAppManager.SafeRelease(objsystemAppManager)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objAppManagerData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objOptions)
            Xydc.Platform.web.TreeviewProcess.SafeRelease(objTreeviewProcess)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.BusinessFacade.systemAppManager.SafeRelease(objsystemAppManager)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objAppManagerData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objOptions)
            Xydc.Platform.web.TreeviewProcess.SafeRelease(objTreeviewProcess)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub doRevoke(ByVal strControlId As String)

            Dim objsystemAppManager As New Xydc.Platform.BusinessFacade.systemAppManager
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objTreeviewProcess As New Xydc.Platform.web.TreeviewProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim objAppManagerData As New Xydc.Platform.Common.Data.AppManagerData
            Dim objOptions As New System.Collections.Specialized.ListDictionary
            Dim strErrMsg As String

            Try
                '检查
                Dim intLevel As Integer
                intLevel = objTreeviewProcess.getLevelIndexFromNodeIndex(Me.tvwServers.SelectedNodeIndex)
                If intLevel < 4 Then
                    strErrMsg = "错误：未选择角色！"
                    GoTo errProc
                End If
                Dim objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty
                Dim strRoleName As String
                If Me.getServerConnectionProperty(strErrMsg, Me.tvwServers.SelectedNodeIndex, objConnectionProperty, strRoleName) = False Then
                    GoTo errProc
                End If
                Dim intSelected As Integer = 0
                Dim blnSelected As Boolean
                Dim intCount As Integer
                Dim i As Integer
                intCount = Me.grdObject.Items.Count
                For i = 0 To intCount - 1 Step 1
                    blnSelected = objDataGridProcess.isDataGridItemChecked(Me.grdObject.Items(i), 0, Me.m_cstrCheckBoxIdInDataGrid_Object)
                    If blnSelected = True Then
                        intSelected += 1
                    End If
                Next
                If intSelected < 1 Then
                    strErrMsg = "错误：未选定要回收权限的对象！"
                    GoTo errProc
                End If
                Dim objenumPermissionType As Xydc.Platform.Common.Data.AppManagerData.enumPermissionType
                intCount = Me.cblObjectQX.Items.Count
                For i = 0 To intCount - 1 Step 1
                    If Me.cblObjectQX.Items(i).Selected = True Then
                        objenumPermissionType = objAppManagerData.getPermissionType(Me.cblObjectQX.Items(i).Value)
                        objOptions.Add(objenumPermissionType, True)
                    End If
                Next
                If objOptions.Count < 1 Then
                    strErrMsg = "错误：未选定回收权限选项！"
                    GoTo errProc
                End If

                '逐个授权
                Dim intColIndex(2) As Integer
                Dim strObjectName As String
                Dim strObjectType As String
                intColIndex(0) = objDataGridProcess.getDataGridColumnIndex(Me.grdObject, Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_DUIXIANG_DXMC)
                intColIndex(1) = objDataGridProcess.getDataGridColumnIndex(Me.grdObject, Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_DUIXIANG_DXLX)
                intCount = Me.grdObject.Items.Count
                For i = 0 To intCount - 1 Step 1
                    blnSelected = objDataGridProcess.isDataGridItemChecked(Me.grdObject.Items(i), 0, Me.m_cstrCheckBoxIdInDataGrid_Object)
                    If blnSelected = True Then
                        strObjectName = objDataGridProcess.getDataGridCellValue(Me.grdObject.Items(i), intColIndex(0))
                        strObjectType = objDataGridProcess.getDataGridCellValue(Me.grdObject.Items(i), intColIndex(1))
                        If objsystemAppManager.doRevokeRole(strErrMsg, objConnectionProperty, strRoleName, strObjectName, strObjectType, objOptions) = False Then
                            GoTo errProc
                        End If

                        '记录审计日志
                        Xydc.Platform.SystemFramework.ApplicationLog.WriteAuditAQInfo(Request.UserHostAddress, Request.UserHostName, "[" + MyBase.UserId + "]从[" + strRoleName + "]角色回收[" + strObjectName + "]的相关权限！")
                    End If
                Next

                '刷新显示
                If Me.getModuleData_Object(strErrMsg, Me.m_strQuery_Object) = False Then
                    GoTo errProc
                End If
                If Me.showModuleData_Object(strErrMsg) = False Then
                    GoTo errProc
                End If

                '提示成功
                objMessageProcess.doAlertMessage(Me.popMessageObject, "提示：选定[" + intSelected.ToString() + "]个对象已经成功回收权限！")

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.BusinessFacade.systemAppManager.SafeRelease(objsystemAppManager)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objAppManagerData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objOptions)
            Xydc.Platform.web.TreeviewProcess.SafeRelease(objTreeviewProcess)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.BusinessFacade.systemAppManager.SafeRelease(objsystemAppManager)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objAppManagerData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objOptions)
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
            Me.doGrant("lnkMLGrant")
        End Sub

        Private Sub lnkMLRevoke_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkMLRevoke.Click
            Me.doRevoke("lnkMLRevoke")
        End Sub

        Private Sub lnkMLAccsQX_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkMLAccsQX.Click

            Me.releaseModuleParameters()
            Me.releaseInterfaceParameters()
            Dim strUrl As String = "xtgl_sjqx_cq.aspx"
            Response.Redirect(strUrl)

        End Sub

        Private Sub lnkMLUserQX_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkMLUserQX.Click

            Me.releaseModuleParameters()
            Me.releaseInterfaceParameters()
            Dim strUrl As String = "xtgl_sjqx_yh.aspx"
            Response.Redirect(strUrl)

        End Sub

        Private Sub lnkMLClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkMLClose.Click
            Me.doClose("lnkMLClose")
        End Sub
    End Class
End Namespace
