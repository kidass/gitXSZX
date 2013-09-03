Imports System.Web.Security

Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.web
    ' 类名    ：xtgl_rzgl_jcrz
    ' 
    ' 调用性质：
    '     可调用其他模块
    '
    ' 功能描述： 
    '   　处理查询用户进出系统日志任务
    '
    ' 接口参数：
    '
    '----------------------------------------------------------------

    Partial Public Class xtgl_rzgl_jcrz
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
        Private m_cstrPrevilegeParamPrefix As String = "xtgl_rzgl_jcrz_previlege_param"
        Private m_blnPrevilegeParams(4) As Boolean

        '----------------------------------------------------------------
        '模块现场保留参数，恢复完成后立即释放session资源
        '----------------------------------------------------------------
        Private m_objSaveScence As Xydc.Platform.BusinessFacade.IMXtglRzglJcrz
        Private m_blnSaveScence As Boolean

        '----------------------------------------------------------------
        '模块接口参数
        '----------------------------------------------------------------
        Private m_blnInterface As Boolean

        '----------------------------------------------------------------
        '与数据网格grdJCRZ相关的参数
        '----------------------------------------------------------------
        '网格中模板列中的控件ID
        Private Const m_cstrCheckBoxIdInDataGrid_JCRZ As String = "chkJCRZ"
        '包含网格的DIV对象ID
        Private Const m_cstrDataGridInDIV_JCRZ As String = "divJCRZ"
        '网格要锁定的列数
        Private m_intFixedColumns_JCRZ As Integer

        '----------------------------------------------------------------
        '要访问的数据
        '----------------------------------------------------------------
        Private m_objDataSet_JCRZ As Xydc.Platform.Common.Data.CustomerData
        Private m_strQuery_JCRZ As String '记录m_objDataSet_JCRZ搜索串
        Private m_intRows_JCRZ As Integer '记录m_objDataSet_JCRZ的DefaultView记录数








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
                    Me.htxtJCRZQuery.Value = .htxtJCRZQuery
                    Me.htxtJCRZRows.Value = .htxtJCRZRows
                    Me.htxtJCRZSort.Value = .htxtJCRZSort
                    Me.htxtJCRZSortColumnIndex.Value = .htxtJCRZSortColumnIndex
                    Me.htxtJCRZSortType.Value = .htxtJCRZSortType

                    Me.htxtDivLeftBody.Value = .htxtDivLeftBody
                    Me.htxtDivTopBody.Value = .htxtDivTopBody
                    Me.htxtDivLeftJCRZ.Value = .htxtDivLeftJCRZ
                    Me.htxtDivTopJCRZ.Value = .htxtDivTopJCRZ

                    Me.htxtSessionIdQuery.Value = .htxtSessionIdQuery

                    Me.txtJCRZPageIndex.Text = .txtJCRZPageIndex
                    Me.txtJCRZPageSize.Text = .txtJCRZPageSize

                    Me.txtJCRZSearch_YHBS.Text = .txtJCRZSearch_YHBS
                    Me.txtJCRZSearch_YHMC.Text = .txtJCRZSearch_YHMC
                    Me.txtJCRZSearch_JQDZ.Text = .txtJCRZSearch_JQDZ
                    Me.txtJCRZSearch_CZSJMin.Text = .txtJCRZSearch_CZSJMin
                    Me.txtJCRZSearch_CZSJMax.Text = .txtJCRZSearch_CZSJMax
                    Try
                        Me.ddlJCRZSearch_CZLX.SelectedIndex = .ddlJCRZSearch_CZLX_SelectedIndex
                    Catch ex As Exception
                    End Try

                    Me.txtJCRZ_QSRQ.Text = .txtJCRZ_QSRQ
                    Me.txtJCRZ_ZZRQ.Text = .txtJCRZ_ZZRQ

                    Try
                        Me.grdJCRZ.PageSize = .grdJCRZPageSize
                    Catch ex As Exception
                    End Try
                    Try
                        Me.grdJCRZ.CurrentPageIndex = .grdJCRZCurrentPageIndex
                    Catch ex As Exception
                    End Try
                    Try
                        Me.grdJCRZ.SelectedIndex = .grdJCRZSelectedIndex
                    Catch ex As Exception
                    End Try

                End With

                '释放资源
                Session.Remove(strSessionId)
                Me.m_objSaveScence.Dispose()
                Me.m_objSaveScence = Nothing

            Catch ex As Exception
            End Try

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
                If strSessionId = "" Then Exit Try

                '创建对象
                Me.m_objSaveScence = New Xydc.Platform.BusinessFacade.IMXtglRzglJcrz

                '保存现场信息
                With Me.m_objSaveScence
                    .htxtJCRZQuery = Me.htxtJCRZQuery.Value
                    .htxtJCRZRows = Me.htxtJCRZRows.Value
                    .htxtJCRZSort = Me.htxtJCRZSort.Value
                    .htxtJCRZSortColumnIndex = Me.htxtJCRZSortColumnIndex.Value
                    .htxtJCRZSortType = Me.htxtJCRZSortType.Value

                    .htxtDivLeftBody = Me.htxtDivLeftBody.Value
                    .htxtDivTopBody = Me.htxtDivTopBody.Value
                    .htxtDivLeftJCRZ = Me.htxtDivLeftJCRZ.Value
                    .htxtDivTopJCRZ = Me.htxtDivTopJCRZ.Value

                    .htxtSessionIdQuery = Me.htxtSessionIdQuery.Value

                    .txtJCRZPageIndex = Me.txtJCRZPageIndex.Text
                    .txtJCRZPageSize = Me.txtJCRZPageSize.Text

                    .txtJCRZSearch_YHBS = Me.txtJCRZSearch_YHBS.Text
                    .txtJCRZSearch_YHMC = Me.txtJCRZSearch_YHMC.Text
                    .txtJCRZSearch_JQDZ = Me.txtJCRZSearch_JQDZ.Text
                    .txtJCRZSearch_CZSJMin = Me.txtJCRZSearch_CZSJMin.Text
                    .txtJCRZSearch_CZSJMax = Me.txtJCRZSearch_CZSJMax.Text
                    .ddlJCRZSearch_CZLX_SelectedIndex = Me.ddlJCRZSearch_CZLX.SelectedIndex

                    .txtJCRZ_QSRQ = Me.txtJCRZ_QSRQ.Text
                    .txtJCRZ_ZZRQ = Me.txtJCRZ_ZZRQ.Text

                    .grdJCRZPageSize = Me.grdJCRZ.PageSize
                    .grdJCRZCurrentPageIndex = Me.grdJCRZ.CurrentPageIndex
                    .grdJCRZSelectedIndex = Me.grdJCRZ.SelectedIndex

                End With

                '缓存对象
                Session.Add(strSessionId, Me.m_objSaveScence)

            Catch ex As Exception
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            saveModuleInformation = strSessionId

        End Function

        '----------------------------------------------------------------
        ' 从调用模块中获取数据
        '----------------------------------------------------------------
        Private Function getDataFromCallModule(ByRef strErrMsg As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            Try
                If Me.IsPostBack = True Then Exit Try

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
                        Me.htxtJCRZQuery.Value = objISjcxCxtj.oQueryString
                        If Me.htxtSessionIdQuery.Value.Trim = "" Then
                            Me.htxtSessionIdQuery.Value = objPulicParameters.getNewGuid()
                        Else
                            Try
                                objQueryData = CType(Session(Me.htxtSessionIdQuery.Value), Xydc.Platform.Common.Data.QueryData)
                            Catch ex As Exception
                                objQueryData = Nothing
                            End Try
                            If Not (objQueryData Is Nothing) Then
                                objQueryData.Dispose()
                                objQueryData = Nothing
                            End If
                        End If
                        Session.Add(Me.htxtSessionIdQuery.Value, objISjcxCxtj.oDataSetTJ)
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
                        Me.m_objSaveScence = CType(Session.Item(strSessionId), Xydc.Platform.BusinessFacade.IMXtglRzglJcrz)
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
                Me.m_intFixedColumns_JCRZ = objPulicParameters.getObjectValue(Me.htxtJCRZFixed.Value, 0)
                Me.m_intRows_JCRZ = objPulicParameters.getObjectValue(Me.htxtJCRZRows.Value, 0)
                Me.m_strQuery_JCRZ = Me.htxtJCRZQuery.Value

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
                    If Not (objQueryData Is Nothing) Then
                        objQueryData.Dispose()
                        objQueryData = Nothing
                    End If
                    Session.Remove(Me.htxtSessionIdQuery.Value)
                End If
            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' 获取grdJCRZ的搜索条件(默认表前缀a.)
        '     strErrMsg      ：返回错误信息
        '     strQuery       ：返回的搜索条件
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function getQueryString_JCRZ( _
            ByRef strErrMsg As String, _
            ByRef strQuery As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            getQueryString_JCRZ = False
            strQuery = ""

            Try
                '按“用户标识”搜索
                Dim strYHBS As String
                strYHBS = "a." + Xydc.Platform.Common.Data.CustomerData.FIELD_GL_B_XITONGJINCHURIZHI_CZR
                If Me.txtJCRZSearch_YHBS.Text.Length > 0 Then Me.txtJCRZSearch_YHBS.Text = Me.txtJCRZSearch_YHBS.Text.Trim()
                If Me.txtJCRZSearch_YHBS.Text <> "" Then
                    Me.txtJCRZSearch_YHBS.Text = objPulicParameters.getNewSearchString(Me.txtJCRZSearch_YHBS.Text)
                    If strQuery = "" Then
                        strQuery = strYHBS + " like '" + Me.txtJCRZSearch_YHBS.Text + "%'"
                    Else
                        strQuery = strQuery + " and " + strYHBS + " like '" + Me.txtJCRZSearch_YHBS.Text + "%'"
                    End If
                End If

                '按“用户名称”搜索
                Dim strYHMC As String
                strYHMC = "a." + Xydc.Platform.Common.Data.CustomerData.FIELD_GL_B_XITONGJINCHURIZHI_CZRMC
                If Me.txtJCRZSearch_YHMC.Text.Length > 0 Then Me.txtJCRZSearch_YHMC.Text = Me.txtJCRZSearch_YHMC.Text.Trim()
                If Me.txtJCRZSearch_YHMC.Text <> "" Then
                    Me.txtJCRZSearch_YHMC.Text = objPulicParameters.getNewSearchString(Me.txtJCRZSearch_YHMC.Text)
                    If strQuery = "" Then
                        strQuery = strYHMC + " like '" + Me.txtJCRZSearch_YHMC.Text + "%'"
                    Else
                        strQuery = strQuery + " and " + strYHMC + " like '" + Me.txtJCRZSearch_YHMC.Text + "%'"
                    End If
                End If

                '按“机器地址”搜索
                Dim strJQDZ As String
                strJQDZ = "a." + Xydc.Platform.Common.Data.CustomerData.FIELD_GL_B_XITONGJINCHURIZHI_JQDZ
                If Me.txtJCRZSearch_JQDZ.Text.Length > 0 Then Me.txtJCRZSearch_JQDZ.Text = Me.txtJCRZSearch_JQDZ.Text.Trim()
                If Me.txtJCRZSearch_JQDZ.Text <> "" Then
                    Me.txtJCRZSearch_JQDZ.Text = objPulicParameters.getNewSearchString(Me.txtJCRZSearch_JQDZ.Text)
                    If strQuery = "" Then
                        strQuery = strJQDZ + " like '" + Me.txtJCRZSearch_JQDZ.Text + "%'"
                    Else
                        strQuery = strQuery + " and " + strJQDZ + " like '" + Me.txtJCRZSearch_JQDZ.Text + "%'"
                    End If
                End If

                '按“操作类型”搜索
                Dim strCZLX As String
                strCZLX = "a." + Xydc.Platform.Common.Data.CustomerData.FIELD_GL_B_XITONGJINCHURIZHI_CZLX
                Select Case Me.ddlJCRZSearch_CZLX.SelectedIndex
                    Case 1, 2
                        If strQuery = "" Then
                            strQuery = strCZLX + " = '" + Me.ddlJCRZSearch_CZLX.SelectedItem.Value + "'"
                        Else
                            strQuery = strQuery + " and " + strCZLX + " = '" + Me.ddlJCRZSearch_CZLX.SelectedItem.Value + "'"
                        End If
                    Case Else
                End Select

                '按“操作时间”搜索
                Dim dateMin As System.DateTime
                Dim dateMax As System.DateTime
                Dim strCZSJ As String
                strCZSJ = "convert(varchar(10),a." + Xydc.Platform.Common.Data.CustomerData.FIELD_GL_B_XITONGJINCHURIZHI_CZSJ + ",120)"
                If Me.txtJCRZSearch_CZSJMin.Text.Length > 0 Then Me.txtJCRZSearch_CZSJMin.Text = Me.txtJCRZSearch_CZSJMin.Text.Trim()
                If Me.txtJCRZSearch_CZSJMax.Text.Length > 0 Then Me.txtJCRZSearch_CZSJMax.Text = Me.txtJCRZSearch_CZSJMax.Text.Trim()
                If Me.txtJCRZSearch_CZSJMin.Text <> "" And Me.txtJCRZSearch_CZSJMax.Text <> "" Then
                    Try
                        dateMin = CType(Me.txtJCRZSearch_CZSJMin.Text, System.DateTime)
                    Catch ex As Exception
                        strErrMsg = "错误：无效的日期！"
                        GoTo errProc
                    End Try
                    Try
                        dateMax = CType(Me.txtJCRZSearch_CZSJMax.Text, System.DateTime)
                    Catch ex As Exception
                        strErrMsg = "错误：无效的日期！"
                        GoTo errProc
                    End Try
                    If dateMin > dateMax Then
                        Me.txtJCRZSearch_CZSJMin.Text = Format(dateMax, "yyyy-MM-dd")
                        Me.txtJCRZSearch_CZSJMax.Text = Format(dateMin, "yyyy-MM-dd")
                    Else
                        Me.txtJCRZSearch_CZSJMin.Text = Format(dateMin, "yyyy-MM-dd")
                        Me.txtJCRZSearch_CZSJMax.Text = Format(dateMax, "yyyy-MM-dd")
                    End If
                    If strQuery = "" Then
                        strQuery = strCZSJ + " between '" + Me.txtJCRZSearch_CZSJMin.Text + "' and '" + Me.txtJCRZSearch_CZSJMax.Text + "'"
                    Else
                        strQuery = strQuery + " and " + strCZSJ + " between '" + Me.txtJCRZSearch_CZSJMin.Text + "' and '" + Me.txtJCRZSearch_CZSJMax.Text + "'"
                    End If
                ElseIf Me.txtJCRZSearch_CZSJMin.Text <> "" Then
                    Try
                        dateMin = CType(Me.txtJCRZSearch_CZSJMin.Text, System.DateTime)
                    Catch ex As Exception
                        strErrMsg = "错误：无效的日期！"
                        GoTo errProc
                    End Try
                    Me.txtJCRZSearch_CZSJMin.Text = Format(dateMin, "yyyy-MM-dd")
                    If strQuery = "" Then
                        strQuery = strCZSJ + " >= '" + Me.txtJCRZSearch_CZSJMin.Text + "'"
                    Else
                        strQuery = strQuery + " and " + strCZSJ + " >= '" + Me.txtJCRZSearch_CZSJMin.Text + "'"
                    End If
                ElseIf Me.txtJCRZSearch_CZSJMax.Text <> "" Then
                    Try
                        dateMax = CType(Me.txtJCRZSearch_CZSJMax.Text, System.DateTime)
                    Catch ex As Exception
                        strErrMsg = "错误：无效的日期！"
                        GoTo errProc
                    End Try
                    Me.txtJCRZSearch_CZSJMax.Text = Format(dateMax, "yyyy-MM-dd")
                    If strQuery = "" Then
                        strQuery = strCZSJ + " <= '" + Me.txtJCRZSearch_CZSJMax.Text + "'"
                    Else
                        strQuery = strQuery + " and " + strCZSJ + " <= '" + Me.txtJCRZSearch_CZSJMax.Text + "'"
                    End If
                Else
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            getQueryString_JCRZ = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取grdJCRZ要显示的数据信息
        '     strErrMsg      ：返回错误信息
        '     strWhere       ：搜索条件
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function getModuleData_JCRZ( _
            ByRef strErrMsg As String, _
            ByVal strWhere As String) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.CustomerData.TABLE_GL_B_XITONGJINCHURIZHI
            Dim objsystemCustomer As New Xydc.Platform.BusinessFacade.systemCustomer

            getModuleData_JCRZ = False

            Try
                '备份Sort字符串
                Dim strSort As String
                strSort = Me.htxtJCRZSort.Value
                If strSort.Length > 0 Then strSort = strSort.Trim

                '释放资源
                If Not (Me.m_objDataSet_JCRZ Is Nothing) Then
                    Me.m_objDataSet_JCRZ.Dispose()
                    Me.m_objDataSet_JCRZ = Nothing
                End If

                '重新检索数据
                If objsystemCustomer.getXitongJinchuRizhiData(strErrMsg, MyBase.UserId, MyBase.UserPassword, strWhere, Me.m_objDataSet_JCRZ) = False Then
                    GoTo errProc
                End If

                '恢复Sort字符串
                With Me.m_objDataSet_JCRZ.Tables(strTable)
                    .DefaultView.Sort = strSort
                End With

                '缓存参数
                With Me.m_objDataSet_JCRZ.Tables(strTable)
                    Me.htxtJCRZRows.Value = .DefaultView.Count.ToString()
                    Me.m_intRows_JCRZ = .DefaultView.Count
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.BusinessFacade.systemCustomer.SafeRelease(objsystemCustomer)

            getModuleData_JCRZ = True
            Exit Function

errProc:
            Xydc.Platform.BusinessFacade.systemCustomer.SafeRelease(objsystemCustomer)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据屏幕搜索条件搜索grdJCRZ数据
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function searchModuleData_JCRZ(ByRef strErrMsg As String) As Boolean

            searchModuleData_JCRZ = False

            Try
                '获取搜索字符串
                Dim strQuery As String
                If Me.getQueryString_JCRZ(strErrMsg, strQuery) = False Then
                    GoTo errProc
                End If

                '搜索数据
                If Me.getModuleData_JCRZ(strErrMsg, strQuery) = False Then
                    GoTo errProc
                End If

                '记录搜索字符串
                Me.m_strQuery_JCRZ = strQuery
                Me.htxtJCRZQuery.Value = Me.m_strQuery_JCRZ

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            searchModuleData_JCRZ = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示grdJCRZ的数据
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showDataGridInfo_JCRZ(ByRef strErrMsg As String) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.CustomerData.TABLE_GL_B_XITONGJINCHURIZHI

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess

            showDataGridInfo_JCRZ = False

            '获取系统保存的网格排序数据
            Dim intSortColumnIndex As Integer
            intSortColumnIndex = objPulicParameters.getObjectValue(Me.htxtJCRZSortColumnIndex.Value, -1)
            Dim objSortType As Xydc.Platform.Common.Utilities.PulicParameters.enumSortType
            Try
                objSortType = CType(Me.htxtJCRZSortType.Value, Xydc.Platform.Common.Utilities.PulicParameters.enumSortType)
            Catch ex As Exception
                objSortType = Xydc.Platform.Common.Utilities.PulicParameters.enumSortType.None
            End Try

            '网格显示处理
            Try
                '在获取数据时已经恢复了RowFilter、Sort的现场
                '设置数据源
                If Me.m_objDataSet_JCRZ Is Nothing Then
                    Me.grdJCRZ.DataSource = Nothing
                Else
                    With Me.m_objDataSet_JCRZ.Tables(strTable)
                        Me.grdJCRZ.DataSource = .DefaultView
                    End With
                End If

                '调整网格参数
                With Me.m_objDataSet_JCRZ.Tables(strTable)
                    If objDataGridProcess.onBeforeDataGridBind(strErrMsg, Me.grdJCRZ, .DefaultView.Count) = False Then
                        GoTo errProc
                    End If
                End With

                '恢复列标题中的排序信息
                If intSortColumnIndex >= 0 Then
                    objDataGridProcess.doClearSortCharInDataGridHead(Me.grdJCRZ)
                    With Me.grdJCRZ.Columns(intSortColumnIndex)
                        .HeaderText = objDataGridProcess.getColumnSortHeadString(.HeaderText, objSortType)
                    End With
                End If

                '绑定数据
                Me.grdJCRZ.DataBind()

                '----------------------------------------------------------------
                '因为这些信息是非绑定的，所以下面的操作必须等绑定完成后执行！！！
                '一旦在后续处理中执行了DataBind，则信息会丢失！！！
                '----------------------------------------------------------------
                '恢复网格中的CheckBox状态
                If objDataGridProcess.doRestoreDataGridCheckBoxStatus(strErrMsg, Me.grdJCRZ, Request, 0, Me.m_cstrCheckBoxIdInDataGrid_JCRZ) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)

            showDataGridInfo_JCRZ = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示grdJCRZ及相关信息
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showModuleData_JCRZ(ByRef strErrMsg As String) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.CustomerData.TABLE_GL_B_XITONGJINCHURIZHI

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess

            showModuleData_JCRZ = False

            Try
                '显示网格信息
                If Me.showDataGridInfo_JCRZ(strErrMsg) = False Then
                    GoTo errProc
                End If

                '显示与网格紧密相关的操作或信息提示
                With Me.m_objDataSet_JCRZ.Tables(strTable).DefaultView
                    '显示网格位置信息
                    Me.lblJCRZGridLocInfo.Text = objDataGridProcess.getDataGridLocation(Me.grdJCRZ, .Count)

                    '显示页面浏览功能
                    Me.lnkCZJCRZMoveFirst.Enabled = objDataGridProcess.canDoMoveFirstPage(Me.grdJCRZ, .Count)
                    Me.lnkCZJCRZMoveLast.Enabled = objDataGridProcess.canDoMoveLastPage(Me.grdJCRZ, .Count)
                    Me.lnkCZJCRZMovePrev.Enabled = objDataGridProcess.canDoMovePreviousPage(Me.grdJCRZ, .Count)
                    Me.lnkCZJCRZMoveNext.Enabled = objDataGridProcess.canDoMoveNextPage(Me.grdJCRZ, .Count)

                    '显示相关操作
                    Dim blnEnabled As Boolean
                    If .Count < 1 Then
                        blnEnabled = False
                    Else
                        blnEnabled = True
                    End If
                    Me.lnkCZJCRZDeSelectAll.Enabled = blnEnabled
                    Me.lnkCZJCRZSelectAll.Enabled = blnEnabled
                    Me.lnkCZJCRZGotoPage.Enabled = blnEnabled
                    Me.lnkCZJCRZSetPageSize.Enabled = blnEnabled
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)

            showModuleData_JCRZ = True
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
                Me.btnClearAll.Enabled = Me.m_blnPrevilegeParams(1)
                Me.btnDeleteSelect.Enabled = Me.m_blnPrevilegeParams(2)
                Me.btnDeleteInterval.Enabled = Me.m_blnPrevilegeParams(3)
                Me.btnPrint.Enabled = Me.m_blnPrevilegeParams(4)
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
                    objControlProcess.doTranslateKey(Me.txtJCRZPageIndex)
                    objControlProcess.doTranslateKey(Me.txtJCRZPageSize)

                    objControlProcess.doTranslateKey(Me.txtJCRZSearch_YHMC)
                    objControlProcess.doTranslateKey(Me.txtJCRZSearch_JQDZ)
                    objControlProcess.doTranslateKey(Me.txtJCRZSearch_YHBS)
                    objControlProcess.doTranslateKey(Me.txtJCRZSearch_CZSJMin)
                    objControlProcess.doTranslateKey(Me.txtJCRZSearch_CZSJMax)
                    objControlProcess.doTranslateKey(Me.ddlJCRZSearch_CZLX)
                    objControlProcess.doTranslateKey(Me.txtJCRZSearch_YHBS)

                    objControlProcess.doTranslateKey(Me.txtJCRZ_QSRQ)
                    objControlProcess.doTranslateKey(Me.txtJCRZ_ZZRQ)

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '设置初始值
                If Me.m_blnSaveScence = False Then
                    Me.txtJCRZSearch_CZSJMin.Text = Format(Now, "yyyy-MM-dd")
                End If

                '显示模块级操作
                If Me.showModuleData_MAIN(strErrMsg) = False Then
                    GoTo errProc
                End If

                '显示数据
                If Me.searchModuleData_JCRZ(strErrMsg) = False Then
                    GoTo errProc
                End If
                If Me.showModuleData_JCRZ(strErrMsg) = False Then
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
                    Xydc.Platform.SystemFramework.ApplicationLog.WriteAuditAQInfo(Request.UserHostAddress, Request.UserHostName, "[" + MyBase.UserId + "]访问了[用户进出日志]！")
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
        '实现对grdJCRZ网格行、列的固定
        Sub grdJCRZ_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles grdJCRZ.ItemDataBound

            Dim intCells As Integer
            Dim i As Integer

            Try
                If e.Item.ItemIndex < 0 Then
                    '标题行,输出标题锁定一般属性
                    intCells = e.Item.Cells.Count
                    For i = 0 To intCells - 1 Step 1
                        e.Item.Cells(i).Attributes.CssStyle.Add("top", "expression(" + Me.m_cstrDataGridInDIV_JCRZ + ".scrollTop)")
                    Next
                End If
                If Me.m_intFixedColumns_JCRZ > 0 Then
                    '锁定列
                    For i = 0 To Me.m_intFixedColumns_JCRZ - 1 Step 1
                        e.Item.Cells(i).CssClass = Me.grdJCRZ.ID + "Locked"
                    Next
                End If
            Catch ex As Exception
            End Try

        End Sub

        Private Sub grdJCRZ_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdJCRZ.SelectedIndexChanged

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '显示记录位置
                Me.lblJCRZGridLocInfo.Text = objDataGridProcess.getDataGridLocation(Me.grdJCRZ, Me.m_intRows_JCRZ)
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

        Private Sub grdJCRZ_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles grdJCRZ.SortCommand

            Dim strTable As String = Xydc.Platform.Common.Data.CustomerData.TABLE_GL_B_XITONGJINCHURIZHI

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
                If Me.getModuleData_JCRZ(strErrMsg, Me.m_strQuery_JCRZ) = False Then
                    GoTo errProc
                End If

                '获取原排序命令
                strOldCommand = Me.m_objDataSet_JCRZ.Tables(strTable).DefaultView.Sort

                '获取要执行的排序命令
                objDataGridProcess.getColumnSortCommand(strErrMsg, strOldCommand, e.SortExpression, strFinalCommand, objenumSortType)
                If strErrMsg <> "" Then
                    GoTo errProc
                End If

                '执行排序
                Me.m_objDataSet_JCRZ.Tables(strTable).DefaultView.Sort = strFinalCommand

                '获取排序列的列索引
                objDataGridItem = CType(e.CommandSource, System.Web.UI.WebControls.DataGridItem)
                strUniqueId = Request.Form("__EVENTTARGET")
                intColumnIndex = objDataGridProcess.getColumnIndexByUniqueIdInRow(objDataGridItem, strUniqueId)

                '保存排序信息
                Me.htxtJCRZSortColumnIndex.Value = intColumnIndex.ToString()
                Me.htxtJCRZSortType.Value = CType(objenumSortType, Integer).ToString()
                Me.htxtJCRZSort.Value = strFinalCommand

                '重新显示数据
                If Me.showModuleData_JCRZ(strErrMsg) = False Then
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




        Private Sub doMoveFirst_JCRZ(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_JCRZ(strErrMsg, Me.m_strQuery_JCRZ) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(0, Me.grdJCRZ.PageCount)
                Me.grdJCRZ.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_JCRZ(strErrMsg) = False Then
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

        Private Sub doMoveLast_JCRZ(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_JCRZ(strErrMsg, Me.m_strQuery_JCRZ) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(Me.grdJCRZ.PageCount - 1, Me.grdJCRZ.PageCount)
                Me.grdJCRZ.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_JCRZ(strErrMsg) = False Then
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

        Private Sub doMoveNext_JCRZ(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_JCRZ(strErrMsg, Me.m_strQuery_JCRZ) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(Me.grdJCRZ.CurrentPageIndex + 1, Me.grdJCRZ.PageCount)
                Me.grdJCRZ.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_JCRZ(strErrMsg) = False Then
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

        Private Sub doMovePrevious_JCRZ(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_JCRZ(strErrMsg, Me.m_strQuery_JCRZ) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(Me.grdJCRZ.CurrentPageIndex - 1, Me.grdJCRZ.PageCount)
                Me.grdJCRZ.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_JCRZ(strErrMsg) = False Then
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

        Private Sub doGotoPage_JCRZ(ByVal strControlId As String)

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            '获取新的页大小
            Dim intPageIndex As Integer
            intPageIndex = objPulicParameters.getObjectValue(Me.txtJCRZPageIndex.Text, 0)
            If intPageIndex <= 0 Then
                intPageIndex = 0
            Else
                intPageIndex -= 1
            End If

            Try
                '获取数据
                If Me.getModuleData_JCRZ(strErrMsg, Me.m_strQuery_JCRZ) = False Then
                    GoTo errProc
                End If

                '设置新的页
                Me.grdJCRZ.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_JCRZ(strErrMsg) = False Then
                    GoTo errProc
                End If

                '信息同步
                Me.txtJCRZPageIndex.Text = (Me.grdJCRZ.CurrentPageIndex + 1).ToString()

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

        Private Sub doSetPageSize_JCRZ(ByVal strControlId As String)

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            '获取新的页大小
            Dim intPageSize As Integer
            intPageSize = objPulicParameters.getObjectValue(Me.txtJCRZPageSize.Text, 100)
            If intPageSize <= 0 Then intPageSize = 100

            Try
                '获取数据
                If Me.getModuleData_JCRZ(strErrMsg, Me.m_strQuery_JCRZ) = False Then
                    GoTo errProc
                End If

                '设置新的页大小
                Me.grdJCRZ.PageSize = intPageSize

                '刷新网格显示
                If Me.showModuleData_JCRZ(strErrMsg) = False Then
                    GoTo errProc
                End If

                '信息同步
                Me.txtJCRZPageSize.Text = (Me.grdJCRZ.PageSize).ToString()

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

        Private Sub doSelectAll_JCRZ(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                If objDataGridProcess.doCheckedDataGridCheckBox(strErrMsg, Me.grdJCRZ, 0, Me.m_cstrCheckBoxIdInDataGrid_JCRZ, True) = False Then
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

        Private Sub doDeSelectAll_JCRZ(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                If objDataGridProcess.doCheckedDataGridCheckBox(strErrMsg, Me.grdJCRZ, 0, Me.m_cstrCheckBoxIdInDataGrid_JCRZ, False) = False Then
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

        Private Sub doSearch_JCRZ(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '搜索数据
                If Me.searchModuleData_JCRZ(strErrMsg) = False Then
                    GoTo errProc
                End If

                '刷新网格显示
                If Me.showModuleData_JCRZ(strErrMsg) = False Then
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

        Private Sub lnkCZJCRZMoveFirst_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZJCRZMoveFirst.Click
            Me.doMoveFirst_JCRZ("lnkCZJCRZMoveFirst")
        End Sub

        Private Sub lnkCZJCRZMoveLast_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZJCRZMoveLast.Click
            Me.doMoveLast_JCRZ("lnkCZJCRZMoveLast")
        End Sub

        Private Sub lnkCZJCRZMoveNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZJCRZMoveNext.Click
            Me.doMoveNext_JCRZ("lnkCZJCRZMoveNext")
        End Sub

        Private Sub lnkCZJCRZMovePrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZJCRZMovePrev.Click
            Me.doMovePrevious_JCRZ("lnkCZJCRZMovePrev")
        End Sub

        Private Sub lnkCZJCRZGotoPage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZJCRZGotoPage.Click
            Me.doGotoPage_JCRZ("lnkCZJCRZGotoPage")
        End Sub

        Private Sub lnkCZJCRZSetPageSize_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZJCRZSetPageSize.Click
            Me.doSetPageSize_JCRZ("lnkCZJCRZSetPageSize")
        End Sub

        Private Sub lnkCZJCRZSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZJCRZSelectAll.Click
            Me.doSelectAll_JCRZ("lnkCZJCRZSelectAll")
        End Sub

        Private Sub lnkCZJCRZDeSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZJCRZDeSelectAll.Click
            Me.doDeSelectAll_JCRZ("lnkCZJCRZDeSelectAll")
        End Sub

        Private Sub btnJCRZSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnJCRZSearch.Click
            Me.doSearch_JCRZ("btnJCRZSearch")
        End Sub



        '----------------------------------------------------------------
        '模块特殊操作处理器
        '----------------------------------------------------------------
        Private Function doRefresh(ByRef strErrMsg As String) As Boolean

            doRefresh = False

            Try
                '搜索数据
                If Me.getModuleData_JCRZ(strErrMsg, Me.m_strQuery_JCRZ) = False Then
                    GoTo errProc
                End If

                '刷新网格显示
                If Me.showModuleData_JCRZ(strErrMsg) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doRefresh = True
            Exit Function

errProc:
            Exit Function

        End Function

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

        Private Sub doSearchFull(ByVal strControlId As String)

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim objISjcxCxtj As Xydc.Platform.BusinessFacade.ISjcxCxtj
            Dim strNewSessionId As String
            Dim strSessionId As String

            Dim strTable As String = Xydc.Platform.Common.Data.CustomerData.TABLE_GL_B_XITONGJINCHURIZHI

            Try
                '获取数据
                If Me.getModuleData_JCRZ(strErrMsg, Me.m_strQuery_JCRZ) = False Then
                    GoTo errProc
                End If

                '备份现场参数
                strSessionId = Me.saveModuleInformation()
                If strSessionId = "" Then
                    strErrMsg = "错误：不能保存现场信息！"
                    GoTo errProc
                End If

                '准备调用接口
                Dim strUrl As String
                objISjcxCxtj = New Xydc.Platform.BusinessFacade.ISjcxCxtj
                With objISjcxCxtj
                    If Me.htxtSessionIdQuery.Value.Trim <> "" Then
                        .iDataSetTJ = CType(Session(Me.htxtSessionIdQuery.Value), Xydc.Platform.Common.Data.QueryData)
                    Else
                        .iDataSetTJ = Nothing
                    End If
                    .iQueryTable = Me.m_objDataSet_JCRZ.Tables(strTable)
                    .iFixQuery = ""

                    .iSourceControlId = strControlId
                    If Me.m_blnInterface = True Then
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
                    Else
                        strUrl = ""
                        strUrl += Request.Url.AbsolutePath
                        strUrl += "?"
                        strUrl += Xydc.Platform.Common.Utilities.PulicParameters.MSessionId
                        strUrl += "="
                        strUrl += strSessionId
                    End If
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

        Private Sub doPrint(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim strTable As String = Xydc.Platform.Common.Data.CustomerData.TABLE_GL_B_XITONGJINCHURIZHI
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objsystemCustomer As New Xydc.Platform.BusinessFacade.systemCustomer

            Try
                '获取数据集
                If Me.getModuleData_JCRZ(strErrMsg, Me.m_strQuery_JCRZ) = False Then
                    GoTo errProc
                End If
                If Me.m_objDataSet_JCRZ.Tables(strTable) Is Nothing Then
                    strErrMsg = "错误：还未获取数据！"
                    GoTo errProc
                End If
                With Me.m_objDataSet_JCRZ.Tables(strTable)
                    If .Rows.Count < 1 Then
                        strErrMsg = "错误：没有数据！"
                        GoTo errProc
                    End If
                End With

                '检查模版文件
                Dim strMBURL As String = Request.ApplicationPath + Me.m_cstrExcelMBRelativePathToAppRoot + "管理_日志_用户进出系统日志.xls"
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
                If objsystemCustomer.doExportToExcel(strErrMsg, Me.m_objDataSet_JCRZ, strTempSpec) = False Then
                    GoTo errProc
                End If

                '显示Excel
                Dim strTempUrl As String = Request.ApplicationPath + Me.m_cstrPrintCacheRelativePathToAppRoot + strTempFile
                objMessageProcess.doOpenUrl(Me.popMessageObject, strTempUrl, "_blank", "titlebar=yes,menubar=yes,resizable=yes,scrollbars=yes,status=yes")

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.BusinessFacade.systemCustomer.SafeRelease(objsystemCustomer)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.BusinessFacade.systemCustomer.SafeRelease(objsystemCustomer)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub doDeleteSelect(ByVal strControlId As String)

            Dim objsystemCustomer As New Xydc.Platform.BusinessFacade.systemCustomer
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
                intRows = Me.grdJCRZ.Items.Count
                If objMessageProcess.isExecuteCode(Request, Me.popMessageObject.UniqueID, intStep) = True Then
                    For i = 0 To intRows - 1 Step 1
                        If objDataGridProcess.isDataGridItemChecked(Me.grdJCRZ.Items(i), 0, Me.m_cstrCheckBoxIdInDataGrid_JCRZ) = True Then
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
                    Dim intColIndex As Integer
                    intColIndex = objDataGridProcess.getDataGridColumnIndex(Me.grdJCRZ, Xydc.Platform.Common.Data.CustomerData.FIELD_GL_B_XITONGJINCHURIZHI_XH)
                    Dim strXH As String
                    Dim intXH As Integer
                    For i = intRows - 1 To 0 Step -1
                        If objDataGridProcess.isDataGridItemChecked(Me.grdJCRZ.Items(i), 0, Me.m_cstrCheckBoxIdInDataGrid_JCRZ) = True Then
                            '获取模块代码
                            strXH = objDataGridProcess.getDataGridCellValue(Me.grdJCRZ.Items(i), intColIndex)
                            intXH = CType(strXH, Integer)

                            '删除处理
                            If objsystemCustomer.doDeleteXitongJinchuRizhi(strErrMsg, MyBase.UserId, MyBase.UserPassword, intXH) = False Then
                                GoTo errProc
                            End If

                            '记录审计日志
                            Xydc.Platform.SystemFramework.ApplicationLog.WriteAuditAQInfo(Request.UserHostAddress, Request.UserHostName, "[" + MyBase.UserId + "]删除了[用户进出日志]！")
                        End If
                    Next

                    '刷新显示
                    If Me.doRefresh(strErrMsg) = False Then
                        GoTo errProc
                    End If
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.BusinessFacade.systemCustomer.SafeRelease(objsystemCustomer)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.BusinessFacade.systemCustomer.SafeRelease(objsystemCustomer)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub doDeleteInterval(ByVal strControlId As String)

            Dim objsystemCustomer As New Xydc.Platform.BusinessFacade.systemCustomer
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String
            Dim intStep As Integer

            Try
                '检查输入
                Dim objDateMin As System.DateTime
                Dim objDateMax As System.DateTime
                intStep = 1
                If objMessageProcess.isExecuteCode(Request, Me.popMessageObject.UniqueID, intStep) = True Then
                    Me.txtJCRZ_QSRQ.Text = Me.txtJCRZ_QSRQ.Text.Trim
                    Me.txtJCRZ_ZZRQ.Text = Me.txtJCRZ_ZZRQ.Text.Trim
                    If Me.txtJCRZ_QSRQ.Text <> "" And Me.txtJCRZ_ZZRQ.Text <> "" Then
                        Try
                            objDateMin = CType(Me.txtJCRZ_QSRQ.Text, System.DateTime)
                        Catch ex As Exception
                            strErrMsg = "错误：无效的[清理开始时间]！"
                            GoTo errProc
                        End Try
                        Try
                            objDateMax = CType(Me.txtJCRZ_ZZRQ.Text, System.DateTime)
                        Catch ex As Exception
                            strErrMsg = "错误：无效的[清理结束时间]！"
                            GoTo errProc
                        End Try
                        If objDateMin > objDateMax Then
                            Me.txtJCRZ_QSRQ.Text = Format(objDateMax, "yyyy-MM-dd")
                            Me.txtJCRZ_ZZRQ.Text = Format(objDateMin, "yyyy-MM-dd")
                        End If
                    Else
                        strErrMsg = "错误：没有输入[清理开始时间]或[清理结束时间]！"
                        GoTo errProc
                    End If
                End If

                '询问
                intStep = 2
                If objMessageProcess.isExecuteCode(Request, Me.popMessageObject.UniqueID, intStep) = True Then
                    objMessageProcess.doConfirmMessage(Me.popMessageObject, "提示：您确实准备删除[" + Me.txtJCRZ_QSRQ.Text + "]-[" + Me.txtJCRZ_ZZRQ.Text + "]之间发生的日志吗（是/否）？", strControlId, intStep)
                    Exit Try
                Else
                    objMessageProcess.doResetPopMessage(Me.popMessageObject)
                End If

                '提示后回答“是”接着处理
                intStep = 3
                If objMessageProcess.isExecuteCode(Request, Me.popMessageObject.UniqueID, intStep) = True Then
                    '删除处理
                    If objsystemCustomer.doDeleteXitongJinchuRizhi(strErrMsg, MyBase.UserId, MyBase.UserPassword, Me.txtJCRZ_QSRQ.Text, Me.txtJCRZ_ZZRQ.Text) = False Then
                        GoTo errProc
                    End If

                    '记录审计日志
                    Xydc.Platform.SystemFramework.ApplicationLog.WriteAuditAQInfo(Request.UserHostAddress, Request.UserHostName, "[" + MyBase.UserId + "]删除了[用户进出日志]！")

                    '刷新显示
                    If Me.doRefresh(strErrMsg) = False Then
                        GoTo errProc
                    End If
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.BusinessFacade.systemCustomer.SafeRelease(objsystemCustomer)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.BusinessFacade.systemCustomer.SafeRelease(objsystemCustomer)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub doDeleteAll(ByVal strControlId As String)

            Dim objsystemCustomer As New Xydc.Platform.BusinessFacade.systemCustomer
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String
            Dim intStep As Integer

            Try
                '询问
                intStep = 1
                If objMessageProcess.isExecuteCode(Request, Me.popMessageObject.UniqueID, intStep) = True Then
                    objMessageProcess.doConfirmMessage(Me.popMessageObject, "提示：您确实准备清空日志吗（是/否）？", strControlId, intStep)
                    Exit Try
                Else
                    objMessageProcess.doResetPopMessage(Me.popMessageObject)
                End If

                '提示后回答“是”接着处理
                intStep = 2
                If objMessageProcess.isExecuteCode(Request, Me.popMessageObject.UniqueID, intStep) = True Then
                    '删除处理
                    If objsystemCustomer.doDeleteXitongJinchuRizhi(strErrMsg, MyBase.UserId, MyBase.UserPassword) = False Then
                        GoTo errProc
                    End If

                    '记录审计日志
                    Xydc.Platform.SystemFramework.ApplicationLog.WriteAuditAQInfo(Request.UserHostAddress, Request.UserHostName, "[" + MyBase.UserId + "]删除了全部[用户进出日志]！")

                    '刷新显示
                    If Me.doRefresh(strErrMsg) = False Then
                        GoTo errProc
                    End If
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.BusinessFacade.systemCustomer.SafeRelease(objsystemCustomer)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.BusinessFacade.systemCustomer.SafeRelease(objsystemCustomer)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
            Me.doClose("btnClose")
        End Sub

        Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            Me.doSearchFull("btnSearch")
        End Sub

        Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
            Me.doPrint("btnPrint")
        End Sub

        Private Sub btnDeleteSelect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteSelect.Click
            Me.doDeleteSelect("btnDeleteSelect")
        End Sub

        Private Sub btnDeleteInterval_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteInterval.Click
            Me.doDeleteInterval("btnDeleteInterval")
        End Sub

        Private Sub btnClearAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearAll.Click
            Me.doDeleteAll("btnClearAll")
        End Sub

    End Class
End Namespace
