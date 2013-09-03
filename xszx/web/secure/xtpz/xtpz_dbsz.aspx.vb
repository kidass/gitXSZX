Imports System.Web.Security

Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.web
    ' 类名    ：xtpz_dbsz
    ' 
    ' 调用性质：
    '     不被其他模块调用，本身调用其他模块
    '
    ' 功能描述： 
    '   　督办控制设置处理
    '----------------------------------------------------------------

    Partial Public Class xtpz_dbsz
        Inherits Xydc.Platform.web.PageBase


        '----------------------------------------------------------------
        '模块私用参数
        '----------------------------------------------------------------
        '本模块相对image的相对路径
        Private m_cstrRelativePathToImage As String = "../../"

        '----------------------------------------------------------------
        '模块授权参数
        '----------------------------------------------------------------
        Private m_cstrPrevilegeParamPrefix As String = "xtpz_dbsz_previlege_param"
        Private m_blnPrevilegeParams(4) As Boolean

        '----------------------------------------------------------------
        '模块现场保留参数，恢复完成后立即释放session资源
        '----------------------------------------------------------------
        Private m_objSaveScence As Xydc.Platform.BusinessFacade.IMXtpzDbsz
        '首次进入并调用其他模块返回时=true，其他=false
        Private m_blnSaveScence As Boolean

        '----------------------------------------------------------------
        '模块接口参数
        '----------------------------------------------------------------

        '----------------------------------------------------------------
        '与数据网格grdDBSZ相关的参数
        '----------------------------------------------------------------
        '网格中模板列中的控件ID
        Private Const m_cstrCheckBoxIdInDataGrid_DBSZ As String = "chkDBSZ"
        '包含网格的DIV对象ID
        Private Const m_cstrDataGridInDIV_DBSZ As String = "divDBSZ"
        '网格要锁定的列数
        Private m_intFixedColumns_DBSZ As Integer

        '----------------------------------------------------------------
        '当前处理的数据集
        '----------------------------------------------------------------
        Private m_objDataSet_DBSZ As Xydc.Platform.Common.Data.DubanshezhiData
        Private m_strQuery_DBSZ As String '记录m_objDataSet_DBSZ的搜索串
        Private m_intRows_DBSZ As Integer '记录m_objDataSet_DBSZ的DefaultView记录数

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

                    Me.txtDBSZPageIndex.Text = .txtDBSZPageIndex
                    Me.txtDBSZPageSize.Text = .txtDBSZPageSize

                    Me.txtDBSZSearch_BCSM.Text = .txtDBSZSearch_BCSM
                    Me.txtDBSZSearch_DBFW.Text = .txtDBSZSearch_DBFW
                    Me.txtDBSZSearch_ZWMC.Text = .txtDBSZSearch_ZWMC

                    Me.htxtZWDM.Value = .htxtZWDM
                    Me.txtZWMC.Text = .txtZWMC
                    Try
                        Me.ddlBCSM.SelectedIndex = .ddlBCSM_SelectedIndex
                    Catch ex As Exception
                    End Try
                    Try
                        Me.ddlDBFW.SelectedIndex = .ddlDBFW_SelectedIndex
                    Catch ex As Exception
                    End Try

                    Me.htxtDivLeftBody.Value = .htxtDivLeftBody
                    Me.htxtDivTopBody.Value = .htxtDivTopBody
                    Me.htxtDivLeftDBSZ.Value = .htxtDivLeftDBSZ
                    Me.htxtDivTopDBSZ.Value = .htxtDivTopDBSZ

                    Me.htxtSessionIdDBSZQuery.Value = .htxtSessionIdDBSZQuery

                    Me.htxtDBSZQuery.Value = .htxtDBSZQuery
                    Me.htxtDBSZRows.Value = .htxtDBSZRows
                    Me.htxtDBSZSort.Value = .htxtDBSZSort
                    Me.htxtDBSZSortColumnIndex.Value = .htxtDBSZSortColumnIndex
                    Me.htxtDBSZSortType.Value = .htxtDBSZSortType

                    Try
                        Me.grdDBSZ.SelectedIndex = .grdDBSZ_SelectedIndex
                    Catch ex As Exception
                    End Try
                    Try
                        Me.grdDBSZ.PageSize = .grdDBSZ_PageSize
                    Catch ex As Exception
                    End Try
                    Try
                        Me.grdDBSZ.CurrentPageIndex = .grdDBSZ_CurrentPageIndex
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
                Me.m_objSaveScence = New Xydc.Platform.BusinessFacade.IMXtpzDbsz

                '保存现场信息
                With Me.m_objSaveScence
                    .htxtCurrentPage = Me.htxtCurrentPage.Value
                    .htxtCurrentRow = Me.htxtCurrentRow.Value
                    .htxtEditMode = Me.htxtEditMode.Value
                    .htxtEditType = Me.htxtEditType.Value

                    .txtDBSZPageIndex = Me.txtDBSZPageIndex.Text
                    .txtDBSZPageSize = Me.txtDBSZPageSize.Text

                    .txtDBSZSearch_BCSM = Me.txtDBSZSearch_BCSM.Text
                    .txtDBSZSearch_DBFW = Me.txtDBSZSearch_DBFW.Text
                    .txtDBSZSearch_ZWMC = Me.txtDBSZSearch_ZWMC.Text

                    .htxtZWDM = Me.htxtZWDM.Value
                    .txtZWMC = Me.txtZWMC.Text
                    .ddlBCSM_SelectedIndex = Me.ddlBCSM.SelectedIndex
                    .ddlDBFW_SelectedIndex = Me.ddlDBFW.SelectedIndex

                    .htxtDivLeftBody = Me.htxtDivLeftBody.Value
                    .htxtDivTopBody = Me.htxtDivTopBody.Value
                    .htxtDivLeftDBSZ = Me.htxtDivLeftDBSZ.Value
                    .htxtDivTopDBSZ = Me.htxtDivTopDBSZ.Value

                    .htxtSessionIdDBSZQuery = Me.htxtSessionIdDBSZQuery.Value

                    .htxtDBSZQuery = Me.htxtDBSZQuery.Value
                    .htxtDBSZRows = Me.htxtDBSZRows.Value
                    .htxtDBSZSort = Me.htxtDBSZSort.Value
                    .htxtDBSZSortColumnIndex = Me.htxtDBSZSortColumnIndex.Value
                    .htxtDBSZSortType = Me.htxtDBSZSortType.Value

                    .grdDBSZ_SelectedIndex = Me.grdDBSZ.SelectedIndex
                    .grdDBSZ_PageSize = Me.grdDBSZ.PageSize
                    .grdDBSZ_CurrentPageIndex = Me.grdDBSZ.CurrentPageIndex
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

                '=================================================================
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
                            Case "btnDBSZSearch".ToUpper
                                Me.htxtDBSZQuery.Value = objISjcxCxtj.oQueryString
                                If Me.htxtSessionIdDBSZQuery.Value.Trim = "" Then
                                    Me.htxtSessionIdDBSZQuery.Value = objPulicParameters.getNewGuid()
                                Else
                                    Try
                                        objQueryData = CType(Session(Me.htxtSessionIdDBSZQuery.Value), Xydc.Platform.Common.Data.QueryData)
                                    Catch ex As Exception
                                        objQueryData = Nothing
                                    End Try
                                    If Not (objQueryData Is Nothing) Then
                                        objQueryData.Dispose()
                                        objQueryData = Nothing
                                    End If
                                End If
                                Session.Add(Me.htxtSessionIdDBSZQuery.Value, objISjcxCxtj.oDataSetTJ)
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
                        Me.m_objSaveScence = CType(Session.Item(strSessionId), Xydc.Platform.BusinessFacade.IMXtpzDbsz)
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

                Me.m_strQuery_DBSZ = Me.htxtDBSZQuery.Value
                Me.m_intRows_DBSZ = objPulicParameters.getObjectValue(Me.htxtDBSZRows.Value, 0)
                Me.m_intFixedColumns_DBSZ = objPulicParameters.getObjectValue(Me.htxtDBSZFixed.Value, 0)

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
                Dim objQueryData As Xydc.Platform.Common.Data.QueryData
                If Me.htxtSessionIdDBSZQuery.Value.Trim <> "" Then
                    Try
                        objQueryData = CType(Session(Me.htxtSessionIdDBSZQuery.Value), Xydc.Platform.Common.Data.QueryData)
                    Catch ex As Exception
                        objQueryData = Nothing
                    End Try
                    If Not (objQueryData Is Nothing) Then
                        objQueryData.Dispose()
                        objQueryData = Nothing
                    End If
                    Session.Remove(Me.htxtSessionIdDBSZQuery.Value)
                    Me.htxtSessionIdDBSZQuery.Value = ""
                End If
            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' 获取grdDBSZ搜索条件(默认表前缀a.)
        '     strErrMsg      ：返回错误信息
        '     strQuery       ：返回的搜索条件
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function getQueryString_DBSZ( _
            ByRef strErrMsg As String, _
            ByRef strQuery As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            getQueryString_DBSZ = False
            strErrMsg = ""
            strQuery = ""

            Try
                '按“督办人职务”搜索
                Dim strZWMC As String = "a." + Xydc.Platform.Common.Data.DubanshezhiData.FIELD_GL_B_DUBANSHEZHI_GWMC
                If Me.txtDBSZSearch_ZWMC.Text.Length > 0 Then Me.txtDBSZSearch_ZWMC.Text = Me.txtDBSZSearch_ZWMC.Text.Trim()
                If Me.txtDBSZSearch_ZWMC.Text <> "" Then
                    Me.txtDBSZSearch_ZWMC.Text = objPulicParameters.getNewSearchString(Me.txtDBSZSearch_ZWMC.Text)
                    If strQuery = "" Then
                        strQuery = strZWMC + " like '" + Me.txtDBSZSearch_ZWMC.Text + "%'"
                    Else
                        strQuery = strQuery + " and " + strZWMC + " like '" + Me.txtDBSZSearch_ZWMC.Text + "%'"
                    End If
                End If

                '按“督办范围”搜索
                Dim strDBFW As String = "a." + Xydc.Platform.Common.Data.DubanshezhiData.FIELD_GL_B_DUBANSHEZHI_DBFWMC
                If Me.txtDBSZSearch_DBFW.Text.Length > 0 Then Me.txtDBSZSearch_DBFW.Text = Me.txtDBSZSearch_DBFW.Text.Trim()
                If Me.txtDBSZSearch_DBFW.Text <> "" Then
                    Me.txtDBSZSearch_DBFW.Text = objPulicParameters.getNewSearchString(Me.txtDBSZSearch_DBFW.Text)
                    If strQuery = "" Then
                        strQuery = strDBFW + " like '" + Me.txtDBSZSearch_DBFW.Text + "%'"
                    Else
                        strQuery = strQuery + " and " + strDBFW + " like '" + Me.txtDBSZSearch_DBFW.Text + "%'"
                    End If
                End If

                '按“补充说明”搜索
                Dim strBCSM As String = "a." + Xydc.Platform.Common.Data.DubanshezhiData.FIELD_GL_B_DUBANSHEZHI_JSXZMC
                If Me.txtDBSZSearch_BCSM.Text.Length > 0 Then Me.txtDBSZSearch_BCSM.Text = Me.txtDBSZSearch_BCSM.Text.Trim()
                If Me.txtDBSZSearch_BCSM.Text <> "" Then
                    Me.txtDBSZSearch_BCSM.Text = objPulicParameters.getNewSearchString(Me.txtDBSZSearch_BCSM.Text)
                    If strQuery = "" Then
                        strQuery = strBCSM + " like '" + Me.txtDBSZSearch_BCSM.Text + "%'"
                    Else
                        strQuery = strQuery + " and " + strBCSM + " like '" + Me.txtDBSZSearch_BCSM.Text + "%'"
                    End If
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            getQueryString_DBSZ = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取grdDBSZ要显示的数据信息
        '     strErrMsg      ：返回错误信息
        '     strWhere       ：搜索字符串
        '     blnEditMode    ：当前编辑状态
        '     objenumEditType：详细操作模式
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function getModuleData_DBSZ( _
            ByRef strErrMsg As String, _
            ByVal strWhere As String, _
            ByVal blnEditMode As Boolean, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.DubanshezhiData.TABLE_GL_B_DUBANSHEZHI
            Dim objsystemDubanshezhi As New Xydc.Platform.BusinessFacade.systemDubanshezhi

            getModuleData_DBSZ = False

            Try
                '备份Sort字符串
                Dim strSort As String
                strSort = Me.htxtDBSZSort.Value
                If strSort.Length > 0 Then strSort = strSort.Trim

                '释放资源
                If Not (Me.m_objDataSet_DBSZ Is Nothing) Then
                    Me.m_objDataSet_DBSZ.Dispose()
                    Me.m_objDataSet_DBSZ = Nothing
                End If

                '重新检索数据
                If objsystemDubanshezhi.getDataSet(strErrMsg, MyBase.UserId, MyBase.UserPassword, strWhere, Me.m_objDataSet_DBSZ) = False Then
                    GoTo errProc
                End If

                '恢复Sort字符串
                With Me.m_objDataSet_DBSZ.Tables(strTable)
                    .DefaultView.Sort = strSort
                End With

                If blnEditMode = False Then '查看模式
                    With Me.m_objDataSet_DBSZ.Tables(strTable)
                        .DefaultView.AllowNew = False
                    End With
                Else '编辑模式
                    Select Case objenumEditType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                            '增加1条空记录
                            With Me.m_objDataSet_DBSZ.Tables(strTable)
                                .DefaultView.AllowNew = True
                                .DefaultView.AddNew()
                            End With
                        Case Else
                            With Me.m_objDataSet_DBSZ.Tables(strTable)
                                .DefaultView.AllowNew = False
                            End With
                    End Select
                End If

                '缓存参数
                With Me.m_objDataSet_DBSZ.Tables(strTable)
                    Me.htxtDBSZRows.Value = .DefaultView.Count.ToString()
                    Me.m_intRows_DBSZ = .DefaultView.Count
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.BusinessFacade.systemDubanshezhi.SafeRelease(objsystemDubanshezhi)

            getModuleData_DBSZ = True
            Exit Function

errProc:
            Xydc.Platform.BusinessFacade.systemDubanshezhi.SafeRelease(objsystemDubanshezhi)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据屏幕搜索条件搜索grdDBSZ数据
        '     strErrMsg      ：返回错误信息
        '     blnEditMode    ：当前编辑状态
        '     objenumEditType：详细操作模式
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function searchModuleData_DBSZ( _
            ByRef strErrMsg As String, _
            ByVal blnEditMode As Boolean, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            searchModuleData_DBSZ = False

            Try
                '获取搜索字符串
                Dim strQuery As String
                If Me.getQueryString_DBSZ(strErrMsg, strQuery) = False Then
                    GoTo errProc
                End If

                '搜索数据
                If Me.getModuleData_DBSZ(strErrMsg, strQuery, blnEditMode, objenumEditType) = False Then
                    GoTo errProc
                End If

                '记录搜索字符串
                Me.m_strQuery_DBSZ = strQuery
                Me.htxtDBSZQuery.Value = Me.m_strQuery_DBSZ

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            searchModuleData_DBSZ = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示grdDBSZ的数据
        '     strErrMsg      ：返回错误信息
        '     blnEditMode    ：当前编辑状态
        '     objenumEditType：详细操作模式
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showDataGridInfo_DBSZ( _
            ByRef strErrMsg As String, _
            ByVal blnEditMode As Boolean, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.DubanshezhiData.TABLE_GL_B_DUBANSHEZHI
            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess

            showDataGridInfo_DBSZ = False

            '获取系统保存的网格排序数据
            Dim intSortColumnIndex As Integer
            intSortColumnIndex = objPulicParameters.getObjectValue(Me.htxtDBSZSortColumnIndex.Value, -1)
            Dim objSortType As Xydc.Platform.Common.Utilities.PulicParameters.enumSortType
            Try
                objSortType = CType(Me.htxtDBSZSortType.Value, Xydc.Platform.Common.Utilities.PulicParameters.enumSortType)
            Catch ex As Exception
                objSortType = Xydc.Platform.Common.Utilities.PulicParameters.enumSortType.None
            End Try

            '网格显示处理
            Try
                '在获取数据时已经恢复了RowFilter、Sort的现场
                '设置数据源
                If Me.m_objDataSet_DBSZ Is Nothing Then
                    Me.grdDBSZ.DataSource = Nothing
                Else
                    With Me.m_objDataSet_DBSZ.Tables(strTable)
                        Me.grdDBSZ.DataSource = .DefaultView
                    End With
                End If

                '调整网格参数
                With Me.m_objDataSet_DBSZ.Tables(strTable)
                    If objDataGridProcess.onBeforeDataGridBind(strErrMsg, Me.grdDBSZ, .DefaultView.Count) = False Then
                        GoTo errProc
                    End If
                End With

                '如果是编辑模式
                If blnEditMode = True Then
                    '移动到最后记录
                    Select Case objenumEditType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                            With Me.m_objDataSet_DBSZ.Tables(strTable)
                                Dim intPageIndex As Integer
                                Dim intSelectIndex As Integer
                                If objDataGridProcess.doMoveToRecord(Me.grdDBSZ.AllowPaging, Me.grdDBSZ.PageSize, .DefaultView.Count - 1, intPageIndex, intSelectIndex) = False Then
                                    strErrMsg = "错误：无法移动到最后！"
                                    GoTo errProc
                                End If
                                Try
                                    Me.grdDBSZ.CurrentPageIndex = intPageIndex
                                    Me.grdDBSZ.SelectedIndex = intSelectIndex
                                Catch ex As Exception
                                End Try
                            End With

                        Case Else
                    End Select
                End If

                '允许列排序？
                Me.grdDBSZ.AllowSorting = Not blnEditMode

                '恢复列标题中的排序信息
                If intSortColumnIndex >= 0 Then
                    objDataGridProcess.doClearSortCharInDataGridHead(Me.grdDBSZ)
                    With Me.grdDBSZ.Columns(intSortColumnIndex)
                        .HeaderText = objDataGridProcess.getColumnSortHeadString(.HeaderText, objSortType)
                    End With
                End If

                '绑定数据
                Me.grdDBSZ.DataBind()

                '----------------------------------------------------------------
                '因为这些信息是非绑定的，所以下面的操作必须等绑定完成后执行！！！
                '一旦在后续处理中执行了DataBind，则信息会丢失！！！
                '----------------------------------------------------------------
                '恢复网格中的CheckBox状态
                If objDataGridProcess.doRestoreDataGridCheckBoxStatus(strErrMsg, Me.grdDBSZ, Request, 0, Me.m_cstrCheckBoxIdInDataGrid_DBSZ) = False Then
                    GoTo errProc
                End If

                '如果是编辑模式
                If blnEditMode = True Then
                    '使能网格
                    If objDataGridProcess.doEnabledDataGrid(strErrMsg, Me.grdDBSZ, Not blnEditMode) = False Then
                        GoTo errProc
                    End If
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)

            showDataGridInfo_DBSZ = True
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
                    If Me.grdDBSZ.Items.Count < 1 Or Me.grdDBSZ.SelectedIndex < 0 Then
                        Me.htxtZWDM.Value = ""
                        Me.txtZWMC.Text = ""
                        Me.ddlDBFW.SelectedIndex = -1
                        Me.ddlBCSM.SelectedIndex = -1
                    Else
                        Dim intColIndex(6) As Integer
                        intColIndex(0) = objDataGridProcess.getDataGridColumnIndex(Me.grdDBSZ, Xydc.Platform.Common.Data.DubanshezhiData.FIELD_GL_B_DUBANSHEZHI_GWDM)
                        intColIndex(1) = objDataGridProcess.getDataGridColumnIndex(Me.grdDBSZ, Xydc.Platform.Common.Data.DubanshezhiData.FIELD_GL_B_DUBANSHEZHI_GWMC)
                        intColIndex(2) = objDataGridProcess.getDataGridColumnIndex(Me.grdDBSZ, Xydc.Platform.Common.Data.DubanshezhiData.FIELD_GL_B_DUBANSHEZHI_DBFW)
                        intColIndex(3) = objDataGridProcess.getDataGridColumnIndex(Me.grdDBSZ, Xydc.Platform.Common.Data.DubanshezhiData.FIELD_GL_B_DUBANSHEZHI_DBFWMC)
                        intColIndex(4) = objDataGridProcess.getDataGridColumnIndex(Me.grdDBSZ, Xydc.Platform.Common.Data.DubanshezhiData.FIELD_GL_B_DUBANSHEZHI_JSXZ)
                        intColIndex(5) = objDataGridProcess.getDataGridColumnIndex(Me.grdDBSZ, Xydc.Platform.Common.Data.DubanshezhiData.FIELD_GL_B_DUBANSHEZHI_JSXZMC)
                        Me.htxtZWDM.Value = objDataGridProcess.getDataGridCellValue(Me.grdDBSZ.Items(Me.grdDBSZ.SelectedIndex), intColIndex(0))
                        Me.txtZWMC.Text = objDataGridProcess.getDataGridCellValue(Me.grdDBSZ.Items(Me.grdDBSZ.SelectedIndex), intColIndex(1))
                        Me.ddlDBFW.SelectedIndex = CType(objDataGridProcess.getDataGridCellValue(Me.grdDBSZ.Items(Me.grdDBSZ.SelectedIndex), intColIndex(2)), Integer)
                        Me.ddlBCSM.SelectedIndex = CType(objDataGridProcess.getDataGridCellValue(Me.grdDBSZ.Items(Me.grdDBSZ.SelectedIndex), intColIndex(4)), Integer) - 1
                    End If
                Else
                    '编辑状态
                    '自动恢复数据
                End If

                '使能控件
                objControlProcess.doEnabledControl(Me.txtZWMC, blnEditMode)
                objControlProcess.doEnabledControl(Me.ddlDBFW, blnEditMode)
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
        ' 显示grdDBSZ的信息
        '     strErrMsg      ：返回错误信息
        '     blnEditMode    ：当前编辑状态
        '     objenumEditType：详细操作模式
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showModuleData_DBSZ( _
            ByRef strErrMsg As String, _
            ByVal blnEditMode As Boolean, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.DubanshezhiData.TABLE_GL_B_DUBANSHEZHI
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objControlProcess As New Xydc.Platform.web.ControlProcess

            showModuleData_DBSZ = False

            Try
                '显示网格信息
                If Me.showDataGridInfo_DBSZ(strErrMsg, blnEditMode, objenumEditType) = False Then
                    GoTo errProc
                End If

                '显示与网格紧密相关的操作或信息提示
                With Me.m_objDataSet_DBSZ.Tables(strTable).DefaultView
                    '显示网格位置信息
                    Me.lblDBSZGridLocInfo.Text = objDataGridProcess.getDataGridLocation(Me.grdDBSZ, .Count)

                    '显示页面浏览功能
                    Me.lnkCZDBSZMoveFirst.Enabled = objDataGridProcess.canDoMoveFirstPage(Me.grdDBSZ, .Count) And (Not blnEditMode)
                    Me.lnkCZDBSZMoveLast.Enabled = objDataGridProcess.canDoMoveLastPage(Me.grdDBSZ, .Count) And (Not blnEditMode)
                    Me.lnkCZDBSZMovePrev.Enabled = objDataGridProcess.canDoMovePreviousPage(Me.grdDBSZ, .Count) And (Not blnEditMode)
                    Me.lnkCZDBSZMoveNext.Enabled = objDataGridProcess.canDoMoveNextPage(Me.grdDBSZ, .Count) And (Not blnEditMode)

                    '显示相关操作
                    Dim blnEnabled As Boolean
                    If .Count < 1 Then
                        blnEnabled = False
                    Else
                        blnEnabled = True
                    End If
                    Me.lnkCZDBSZDeSelectAll.Enabled = blnEnabled And (Not blnEditMode)
                    Me.lnkCZDBSZSelectAll.Enabled = blnEnabled And (Not blnEditMode)
                    Me.lnkCZDBSZGotoPage.Enabled = blnEnabled And (Not blnEditMode)
                    Me.lnkCZDBSZSetPageSize.Enabled = blnEnabled And (Not blnEditMode)

                    objControlProcess.doEnabledControl(Me.txtDBSZPageSize, Not blnEditMode)
                    objControlProcess.doEnabledControl(Me.txtDBSZPageIndex, Not blnEditMode)
                    objControlProcess.doEnabledControl(Me.txtDBSZSearch_ZWMC, Not blnEditMode)
                    objControlProcess.doEnabledControl(Me.txtDBSZSearch_DBFW, Not blnEditMode)
                    objControlProcess.doEnabledControl(Me.txtDBSZSearch_BCSM, Not blnEditMode)
                    Me.btnDBSZQuery.Enabled = Not blnEditMode
                End With

                '显示输入窗信息
                If Me.showEditPanelInfo(strErrMsg, blnEditMode) = False Then
                    GoTo errProc
                End If

                '显示操作命令
                Me.btnDBSZAddNew.Enabled = (Not blnEditMode) And Me.m_blnPrevilegeParams(1)
                Me.btnDBSZModify.Enabled = (Not blnEditMode) And Me.m_blnPrevilegeParams(2)
                Me.btnDBSZDelete.Enabled = (Not blnEditMode) And Me.m_blnPrevilegeParams(3)
                Me.btnDBSZSearch.Enabled = (Not blnEditMode) And Me.m_blnPrevilegeParams(4)
                Me.btnClose.Enabled = Not blnEditMode
                Me.btnSave.Enabled = blnEditMode
                Me.btnCancel.Enabled = blnEditMode
                Me.lnkCZSelectZW.Visible = blnEditMode

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.ControlProcess.SafeRelease(objControlProcess)

            showModuleData_DBSZ = True
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
                    objControlProcess.doTranslateKey(Me.ddlDBFW)
                    objControlProcess.doTranslateKey(Me.ddlBCSM)
                    '*************************************************************************
                    objControlProcess.doTranslateKey(Me.txtDBSZPageIndex)
                    objControlProcess.doTranslateKey(Me.txtDBSZPageSize)
                    '*************************************************************************
                    objControlProcess.doTranslateKey(Me.txtDBSZSearch_ZWMC)
                    objControlProcess.doTranslateKey(Me.txtDBSZSearch_DBFW)
                    objControlProcess.doTranslateKey(Me.txtDBSZSearch_BCSM)
                    '*************************************************************************

                    '显示grdDBSZ
                    If Me.getModuleData_DBSZ(strErrMsg, Me.m_strQuery_DBSZ, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                        GoTo errProc
                    End If
                    If Me.showModuleData_DBSZ(strErrMsg, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
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
                    Xydc.Platform.SystemFramework.ApplicationLog.WriteAuditPZInfo(Request.UserHostAddress, Request.UserHostName, "[" + MyBase.UserId + "]访问了[督办控制配置信息]！")
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
        Sub grdDBSZ_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles grdDBSZ.ItemDataBound

            Dim intCells As Integer
            Dim i As Integer

            Try
                If e.Item.ItemIndex < 0 Then
                    '标题行,输出标题锁定一般属性
                    intCells = e.Item.Cells.Count
                    For i = 0 To intCells - 1 Step 1
                        e.Item.Cells(i).Attributes.CssStyle.Add("top", "expression(" + Me.m_cstrDataGridInDIV_DBSZ + ".scrollTop)")
                    Next
                End If

                If Me.m_intFixedColumns_DBSZ > 0 Then
                    '锁定列
                    For i = 0 To Me.m_intFixedColumns_DBSZ - 1 Step 1
                        e.Item.Cells(i).CssClass = Me.grdDBSZ.ID + "Locked"
                    Next
                End If

            Catch ex As Exception
            End Try

        End Sub

        Private Sub grdDBSZ_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdDBSZ.SelectedIndexChanged

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim strErrMsg As String

            Try
                '显示记录位置
                Me.lblDBSZGridLocInfo.Text = objDataGridProcess.getDataGridLocation(Me.grdDBSZ, Me.m_intRows_DBSZ)

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

        Private Sub grdDBSZ_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles grdDBSZ.SortCommand

            Dim strTable As String = Xydc.Platform.Common.Data.DubanshezhiData.TABLE_GL_B_DUBANSHEZHI
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
                If Me.getModuleData_DBSZ(strErrMsg, Me.m_strQuery_DBSZ, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If

                '获取原排序命令
                With Me.m_objDataSet_DBSZ.Tables(strTable)
                    strOldCommand = .DefaultView.Sort
                End With

                '获取要执行的排序命令
                objDataGridProcess.getColumnSortCommand(strErrMsg, strOldCommand, e.SortExpression, strFinalCommand, objenumSortType)
                If strErrMsg <> "" Then
                    GoTo errProc
                End If

                '执行排序
                With Me.m_objDataSet_DBSZ.Tables(strTable)
                    .DefaultView.Sort = strFinalCommand
                End With

                '获取排序列的列索引
                objDataGridItem = CType(e.CommandSource, System.Web.UI.WebControls.DataGridItem)
                strUniqueId = Request.Form("__EVENTTARGET")
                intColumnIndex = objDataGridProcess.getColumnIndexByUniqueIdInRow(objDataGridItem, strUniqueId)

                '保存排序信息
                Me.htxtDBSZSortColumnIndex.Value = intColumnIndex.ToString()
                Me.htxtDBSZSortType.Value = CType(objenumSortType, Integer).ToString()
                Me.htxtDBSZSort.Value = strFinalCommand

                '重新显示数据
                If Me.showModuleData_DBSZ(strErrMsg, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
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




        Private Sub doDBSZMoveFirst(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_DBSZ(strErrMsg, Me.m_strQuery_DBSZ, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(0, Me.grdDBSZ.PageCount)
                Me.grdDBSZ.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_DBSZ(strErrMsg, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
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

        Private Sub doDBSZMoveLast(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_DBSZ(strErrMsg, Me.m_strQuery_DBSZ, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(Me.grdDBSZ.PageCount - 1, Me.grdDBSZ.PageCount)
                Me.grdDBSZ.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_DBSZ(strErrMsg, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
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

        Private Sub doDBSZMoveNext(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_DBSZ(strErrMsg, Me.m_strQuery_DBSZ, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(Me.grdDBSZ.CurrentPageIndex + 1, Me.grdDBSZ.PageCount)
                Me.grdDBSZ.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_DBSZ(strErrMsg, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
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

        Private Sub doDBSZMovePrevious(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_DBSZ(strErrMsg, Me.m_strQuery_DBSZ, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(Me.grdDBSZ.CurrentPageIndex - 1, Me.grdDBSZ.PageCount)
                Me.grdDBSZ.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_DBSZ(strErrMsg, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
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

        Private Sub doDBSZGotoPage(ByVal strControlId As String)

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            '获取新的页大小
            Dim intPageIndex As Integer
            intPageIndex = objPulicParameters.getObjectValue(Me.txtDBSZPageIndex.Text, 0)
            If intPageIndex <= 0 Then
                intPageIndex = 0
            Else
                intPageIndex -= 1
            End If

            Try
                '获取数据
                If Me.getModuleData_DBSZ(strErrMsg, Me.m_strQuery_DBSZ, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If

                '设置新的页
                Me.grdDBSZ.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_DBSZ(strErrMsg, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If

                '信息同步
                Me.txtDBSZPageIndex.Text = (Me.grdDBSZ.CurrentPageIndex + 1).ToString()

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

        Private Sub doDBSZSetPageSize(ByVal strControlId As String)

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            '获取新的页大小
            Dim intPageSize As Integer
            intPageSize = objPulicParameters.getObjectValue(Me.txtDBSZPageSize.Text, 100)
            If intPageSize <= 0 Then intPageSize = 100

            Try
                '获取数据
                If Me.getModuleData_DBSZ(strErrMsg, Me.m_strQuery_DBSZ, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If

                '设置新的页大小
                Me.grdDBSZ.PageSize = intPageSize

                '刷新网格显示
                If Me.showModuleData_DBSZ(strErrMsg, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If

                '信息同步
                Me.txtDBSZPageSize.Text = (Me.grdDBSZ.PageSize).ToString()

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

        Private Sub doDBSZSelectAll(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                If objDataGridProcess.doCheckedDataGridCheckBox(strErrMsg, Me.grdDBSZ, 0, Me.m_cstrCheckBoxIdInDataGrid_DBSZ, True) = False Then
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

        Private Sub doDBSZDeSelectAll(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                If objDataGridProcess.doCheckedDataGridCheckBox(strErrMsg, Me.grdDBSZ, 0, Me.m_cstrCheckBoxIdInDataGrid_DBSZ, False) = False Then
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

        Private Sub doDBSZQuery(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '搜索数据
                If Me.searchModuleData_DBSZ(strErrMsg, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If
                If Me.showModuleData_DBSZ(strErrMsg, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
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

        Private Sub lnkCZDBSZMoveFirst_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZDBSZMoveFirst.Click
            Me.doDBSZMoveFirst("lnkCZDBSZMoveFirst")
        End Sub

        Private Sub lnkCZDBSZMoveLast_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZDBSZMoveLast.Click
            Me.doDBSZMoveLast("lnkCZDBSZMoveLast")
        End Sub

        Private Sub lnkCZDBSZMoveNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZDBSZMoveNext.Click
            Me.doDBSZMoveNext("lnkCZDBSZMoveNext")
        End Sub

        Private Sub lnkCZDBSZMovePrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZDBSZMovePrev.Click
            Me.doDBSZMovePrevious("lnkCZDBSZMovePrev")
        End Sub

        Private Sub lnkCZDBSZGotoPage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZDBSZGotoPage.Click
            Me.doDBSZGotoPage("lnkCZDBSZGotoPage")
        End Sub

        Private Sub lnkCZDBSZSetPageSize_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZDBSZSetPageSize.Click
            Me.doDBSZSetPageSize("lnkCZDBSZSetPageSize")
        End Sub

        Private Sub lnkCZDBSZSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZDBSZSelectAll.Click
            Me.doDBSZSelectAll("lnkCZDBSZSelectAll")
        End Sub

        Private Sub lnkCZDBSZDeSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZDBSZDeSelectAll.Click
            Me.doDBSZDeSelectAll("lnkCZDBSZDeSelectAll")
        End Sub

        Private Sub btnDBSZQuery_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDBSZQuery.Click
            Me.doDBSZQuery("btnDBSZQuery")
        End Sub




        '----------------------------------------------------------------
        '模块特殊操作处理器
        '----------------------------------------------------------------
        Private Sub doDBSZAddNew(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '设置编辑模式
                Me.m_blnEditMode = True
                Me.m_objenumEditType = Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                Me.m_intCurrentPageIndex = Me.grdDBSZ.CurrentPageIndex
                Me.m_intCurrentSelectIndex = Me.grdDBSZ.SelectedIndex

                '保存相关信息
                Me.htxtEditMode.Value = Me.m_blnEditMode.ToString()
                Me.htxtEditType.Value = CType(Me.m_objenumEditType, Integer).ToString()
                Me.htxtCurrentPage.Value = Me.m_intCurrentPageIndex.ToString()
                Me.htxtCurrentRow.Value = Me.m_intCurrentSelectIndex.ToString()

                '进入编辑状态
                If Me.getModuleData_DBSZ(strErrMsg, Me.m_strQuery_DBSZ, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If
                If Me.showModuleData_DBSZ(strErrMsg, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If

                '设置初始值
                Me.htxtZWDM.Value = ""
                Me.txtZWMC.Text = ""
                Me.ddlDBFW.SelectedIndex = -1
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

        Private Sub doDBSZModify(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '检查
                If Me.grdDBSZ.Items.Count < 1 Then
                    strErrMsg = "错误：没有内容可修改！"
                    GoTo errProc
                End If

                '设置编辑模式
                Me.m_blnEditMode = True
                Me.m_objenumEditType = Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eUpdate
                Me.m_intCurrentPageIndex = Me.grdDBSZ.CurrentPageIndex
                Me.m_intCurrentSelectIndex = Me.grdDBSZ.SelectedIndex

                '保存相关信息
                Me.htxtEditMode.Value = Me.m_blnEditMode.ToString()
                Me.htxtEditType.Value = CType(Me.m_objenumEditType, Integer).ToString()
                Me.htxtCurrentPage.Value = Me.m_intCurrentPageIndex.ToString()
                Me.htxtCurrentRow.Value = Me.m_intCurrentSelectIndex.ToString()

                '进入编辑状态
                If Me.getModuleData_DBSZ(strErrMsg, Me.m_strQuery_DBSZ, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If
                If Me.showModuleData_DBSZ(strErrMsg, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
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

            Dim strTable As String = Xydc.Platform.Common.Data.DubanshezhiData.TABLE_GL_B_DUBANSHEZHI
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim objsystemDubanshezhi As New Xydc.Platform.BusinessFacade.systemDubanshezhi
            Dim objNewData As New System.Collections.Specialized.ListDictionary

            Try
                '检查
                If Me.ddlDBFW.SelectedIndex < 0 Then
                    strErrMsg = "错误：没有选择督办范围！"
                    GoTo errProc
                End If
                If Me.ddlBCSM.SelectedIndex < 0 Then
                    strErrMsg = "错误：没有选择督办范围补充说明！"
                    GoTo errProc
                End If

                '获取新信息
                objNewData.Add(Xydc.Platform.Common.Data.DubanshezhiData.FIELD_GL_B_DUBANSHEZHI_GWDM, Me.htxtZWDM.Value)
                objNewData.Add(Xydc.Platform.Common.Data.DubanshezhiData.FIELD_GL_B_DUBANSHEZHI_DBFW, Me.ddlDBFW.SelectedIndex.ToString)
                objNewData.Add(Xydc.Platform.Common.Data.DubanshezhiData.FIELD_GL_B_DUBANSHEZHI_JSXZ, (Me.ddlBCSM.SelectedIndex + 1).ToString)

                '获取旧信息
                Dim objOldData As System.Data.DataRow
                Dim intPos As Integer
                Select Case Me.m_objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                        objOldData = Nothing
                    Case Else
                        '获取数据
                        If Me.getModuleData_DBSZ(strErrMsg, Me.m_strQuery_DBSZ, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                            GoTo errProc
                        End If
                        '获取当前行数据
                        intPos = objDataGridProcess.getRecordPosition(Me.grdDBSZ.SelectedIndex, Me.grdDBSZ.CurrentPageIndex, Me.grdDBSZ.PageSize)
                        With Me.m_objDataSet_DBSZ.Tables(strTable)
                            objOldData = .DefaultView.Item(intPos).Row
                        End With
                End Select

                '保存信息
                If objsystemDubanshezhi.doSaveData(strErrMsg, MyBase.UserId, MyBase.UserPassword, objOldData, objNewData, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If

                '记录审计日志
                Select Case Me.m_objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew, _
                        Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eCpyNew
                        Xydc.Platform.SystemFramework.ApplicationLog.WriteAuditPZInfo(Request.UserHostAddress, Request.UserHostName, "[" + MyBase.UserId + "]增加了[" + Me.txtZWMC.Text + "]的[督办范围]控制信息！")
                    Case Else
                        Xydc.Platform.SystemFramework.ApplicationLog.WriteAuditPZInfo(Request.UserHostAddress, Request.UserHostName, "[" + MyBase.UserId + "]修改了[" + Me.txtZWMC.Text + "]的[督办范围]控制信息！")
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
                If Me.getModuleData_DBSZ(strErrMsg, Me.m_strQuery_DBSZ, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If
                If Me.showModuleData_DBSZ(strErrMsg, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.BusinessFacade.systemDubanshezhi.SafeRelease(objsystemDubanshezhi)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objNewData)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.BusinessFacade.systemDubanshezhi.SafeRelease(objsystemDubanshezhi)
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
                        Me.grdDBSZ.CurrentPageIndex = Me.m_intCurrentPageIndex
                        Me.grdDBSZ.SelectedIndex = Me.m_intCurrentSelectIndex
                    Catch ex As Exception
                    End Try

                    '进入非编辑状态
                    If Me.getModuleData_DBSZ(strErrMsg, Me.m_strQuery_DBSZ, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                        GoTo errProc
                    End If
                    If Me.showModuleData_DBSZ(strErrMsg, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
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

        Private Sub doDBSZDelete(ByVal strControlId As String)

            Dim objsystemDubanshezhi As New Xydc.Platform.BusinessFacade.systemDubanshezhi
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
                intRows = Me.grdDBSZ.Items.Count
                If objMessageProcess.isExecuteCode(Request, Me.popMessageObject.UniqueID, intStep) = True Then
                    For i = 0 To intRows - 1 Step 1
                        If objDataGridProcess.isDataGridItemChecked(Me.grdDBSZ.Items(i), 0, Me.m_cstrCheckBoxIdInDataGrid_DBSZ) = True Then
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
                    If Me.getModuleData_DBSZ(strErrMsg, Me.m_strQuery_DBSZ, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                        GoTo errProc
                    End If

                    '逐个删除
                    Dim objOldData As System.Data.DataRow
                    Dim intPos As Integer
                    Dim intColIndex As Integer
                    Dim strGWMC As String
                    intColIndex = objDataGridProcess.getDataGridColumnIndex(Me.grdDBSZ, Xydc.Platform.Common.Data.DubanshezhiData.FIELD_GL_B_DUBANSHEZHI_GWMC)
                    For i = intRows - 1 To 0 Step -1
                        If objDataGridProcess.isDataGridItemChecked(Me.grdDBSZ.Items(i), 0, Me.m_cstrCheckBoxIdInDataGrid_DBSZ) = True Then
                            strGWMC = objDataGridProcess.getDataGridCellValue(Me.grdDBSZ.Items(i), intColIndex)

                            '获取要删除的数据
                            intPos = objDataGridProcess.getRecordPosition(i, Me.grdDBSZ.CurrentPageIndex, Me.grdDBSZ.PageSize)
                            objOldData = Nothing
                            With Me.m_objDataSet_DBSZ.Tables(Xydc.Platform.Common.Data.DubanshezhiData.TABLE_GL_B_DUBANSHEZHI)
                                objOldData = .DefaultView.Item(intPos).Row
                            End With

                            '删除处理
                            If objsystemDubanshezhi.doDeleteData(strErrMsg, MyBase.UserId, MyBase.UserPassword, objOldData) = False Then
                                GoTo errProc
                            End If

                            '记录审计日志
                            Xydc.Platform.SystemFramework.ApplicationLog.WriteAuditPZInfo(Request.UserHostAddress, Request.UserHostName, "[" + MyBase.UserId + "]删除了[" + strGWMC + "]的[督办范围]配置信息！")
                        End If
                    Next

                    '重新获取数据
                    If Me.getModuleData_DBSZ(strErrMsg, Me.m_strQuery_DBSZ, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                        GoTo errProc
                    End If
                    If Me.showModuleData_DBSZ(strErrMsg, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
                        GoTo errProc
                    End If
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.BusinessFacade.systemDubanshezhi.SafeRelease(objsystemDubanshezhi)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.BusinessFacade.systemDubanshezhi.SafeRelease(objsystemDubanshezhi)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub doDBSZSearch(ByVal strControlId As String)

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim objISjcxCxtj As Xydc.Platform.BusinessFacade.ISjcxCxtj
            Dim strNewSessionId As String
            Dim strMSessionId As String

            Dim strTable As String = Xydc.Platform.Common.Data.DubanshezhiData.TABLE_GL_B_DUBANSHEZHI

            Try
                '获取数据
                If Me.getModuleData_DBSZ(strErrMsg, Me.m_strQuery_DBSZ, Me.m_blnEditMode, Me.m_objenumEditType) = False Then
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
                    If Me.htxtSessionIdDBSZQuery.Value.Trim <> "" Then
                        .iDataSetTJ = CType(Session(Me.htxtSessionIdDBSZQuery.Value), Xydc.Platform.Common.Data.QueryData)
                    Else
                        .iDataSetTJ = Nothing
                    End If
                    .iQueryTable = Me.m_objDataSet_DBSZ.Tables(strTable)
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

        Private Sub btnDBSZSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDBSZSearch.Click
            Me.doDBSZSearch("btnDBSZSearch")
        End Sub

        Private Sub btnDBSZAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDBSZAddNew.Click
            Me.doDBSZAddNew("btnDBSZAddNew")
        End Sub

        Private Sub btnDBSZModify_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDBSZModify.Click
            Me.doDBSZModify("btnDBSZModify")
        End Sub

        Private Sub btnDBSZDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDBSZDelete.Click
            Me.doDBSZDelete("btnDBSZDelete")
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

        Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
            Me.doClose("btnClose")
        End Sub

    End Class
End Namespace
