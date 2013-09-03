Imports System.Web.Security

Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.web
    ' 类名    ：dmxz_zzjg
    ' 
    ' 调用性质：可被其他模块调用，本身不调用其他模块
    '
    ' 功能描述： 
    '   　选择组织机构。
    '
    ' 接口参数：
    '     参见IDmxzZzjg接口类描述

    Partial Public Class dmxz_zzjg
        Inherits Xydc.Platform.web.PageBase

        '----------------------------------------------------------------
        '模块私用参数
        '----------------------------------------------------------------
        '本模块相对image的相对路径
        Private m_cstrRelativePathToImage As String = "../../"

        '----------------------------------------------------------------
        '与数据网格grdFWLIST相关的参数
        '----------------------------------------------------------------
        '网格中模板列中的控件ID
        Private Const m_cstrCheckBoxIdInDataGrid_FWLIST As String = "chkFWLIST"
        '包含网格的DIV对象ID
        Private Const m_cstrDataGridInDIV_FWLIST As String = "divFWLIST"
        '网格要锁定的列数
        Private m_intFixedColumns_FWLIST As Integer

        '----------------------------------------------------------------
        '与数据网格grdSELBM相关的参数
        '----------------------------------------------------------------
        '网格中模板列中的控件ID
        Private Const m_cstrCheckBoxIdInDataGrid_SELBM As String = "chkSELBM"
        '包含网格的DIV对象ID
        Private Const m_cstrDataGridInDIV_SELBM As String = "divSELBM"
        '网格要锁定的列数
        Private m_intFixedColumns_SELBM As Integer

        '----------------------------------------------------------------
        '模块接口参数
        '----------------------------------------------------------------
        Private m_objIDmxzZzjg As Xydc.Platform.BusinessFacade.IDmxzZzjg

        '----------------------------------------------------------------
        '要访问的数据
        '----------------------------------------------------------------
        Private m_objDataSet_BMXX As Xydc.Platform.Common.Data.CustomerData
        Private m_objDataSet_FWLIST As Xydc.Platform.Common.Data.FenfafanweiData
        Private m_strQuery_FWLIST As String '记录m_objDataSet_FWLIST的搜索串
        Private m_intRows_FWLIST As Integer '记录m_objDataSet_FWLIST的DefaultView记录数
        Private m_objDataSet_SELBM As Xydc.Platform.Common.Data.CustomerData
        Private m_strSessionId_SELBM As String '缓存m_objDataSet_SELBM的SessionId











        '----------------------------------------------------------------
        ' 释放接口参数
        '----------------------------------------------------------------
        Private Sub releaseInterfaceParameters()

            Try
                If Not (Me.m_objIDmxzZzjg Is Nothing) Then
                    If Me.m_objIDmxzZzjg.iInterfaceType = Xydc.Platform.BusinessFacade.ICallInterface.enumInterfaceType.InputOnly Then
                        '释放Session
                        Session.Remove(Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.ISessionId))
                        '释放对象
                        Me.m_objIDmxzZzjg.Dispose()
                        Me.m_objIDmxzZzjg = Nothing
                    End If
                End If
            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' 获取接口参数(没有接口参数则显示错误信息页面)
        '----------------------------------------------------------------
        Private Function getInterfaceParameters(ByRef strErrMsg As String) As Boolean

            getInterfaceParameters = False

            '从QueryString中解析接口参数(不论是否回发)
            Dim objTemp As Object
            Try
                objTemp = Session.Item(Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.ISessionId))
                m_objIDmxzZzjg = CType(objTemp, Xydc.Platform.BusinessFacade.IDmxzZzjg)
            Catch ex As Exception
                m_objIDmxzZzjg = Nothing
            End Try

            '必须有接口参数
            If m_objIDmxzZzjg Is Nothing Then
                '显示错误信息
                Me.panelError.Visible = True
                Me.panelMain.Visible = Not Me.panelError.Visible
                strErrMsg = "本模块必须提供输入接口参数！"
                GoTo errProc
            End If

            '获取局部接口参数
            Me.m_strSessionId_SELBM = Me.htxtSessionIdSELBM.Value
            Me.m_strQuery_FWLIST = Me.htxtFWLISTQuery.Value
            With New Xydc.Platform.Common.Utilities.PulicParameters
                '记录m_objDataSet_FWLIST的DefaultView记录数
                Me.m_intRows_FWLIST = .getObjectValue(Me.htxtFWLISTRows.Value, 0)

                Me.m_intFixedColumns_FWLIST = .getObjectValue(Me.htxtFWLISTFixed.Value, 0)
                Me.m_intFixedColumns_SELBM = .getObjectValue(Me.htxtSELBMFixed.Value, 0)
            End With

            getInterfaceParameters = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 释放本模块缓存的参数
        '----------------------------------------------------------------
        Private Sub releaseModuleParameters()

            'Try
            '    If Not (Me.m_objDataSet_SELBM Is Nothing) Then
            '        '释放Session
            '        Session.Remove(Me.m_strSessionId_SELBM)
            '        '释放对象
            '        '对象用于返回，不能释放
            '    End If
            'Catch ex As Exception
            'End Try
            Try

                If Me.m_strSessionId_SELBM.Trim <> "" Then
                    Dim objTempDataSet As Xydc.Platform.Common.Data.CustomerData = Nothing
                    Try
                        objTempDataSet = CType(Session(Me.m_strSessionId_SELBM), Xydc.Platform.Common.Data.CustomerData)
                    Catch ex As Exception
                        objTempDataSet = Nothing
                    End Try
                    Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempDataSet)
                    Session.Remove(Me.m_strSessionId_SELBM)
                End If

            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' 获取grdFWLIST的搜索条件(默认表前缀a.)
        '     strErrMsg      ：返回错误信息
        '     strQuery       ：返回的搜索条件
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function getQueryString_FWLIST( _
            ByRef strErrMsg As String, _
            ByRef strQuery As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            getQueryString_FWLIST = False
            strQuery = ""

            Try
                '按范围名称搜索
                Dim strFWMC As String = "a." + Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_FWMC
                If Me.txtFWLISTSearch_FWMC.Text.Length > 0 Then Me.txtFWLISTSearch_FWMC.Text = Me.txtFWLISTSearch_FWMC.Text.Trim()
                If Me.txtFWLISTSearch_FWMC.Text <> "" Then
                    Me.txtFWLISTSearch_FWMC.Text = objPulicParameters.getNewSearchString(Me.txtFWLISTSearch_FWMC.Text)
                    If strQuery = "" Then
                        strQuery = strFWMC + " like '" + Me.txtFWLISTSearch_FWMC.Text + "%'"
                    Else
                        strQuery = strQuery + " and " + strFWMC + " like '" + Me.txtFWLISTSearch_FWMC.Text + "%'"
                    End If
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            getQueryString_FWLIST = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取grdSELRY的搜索条件(由于缓存了数据，采用RowFilter方式)
        '     strErrMsg      ：返回错误信息
        '     strQuery       ：返回的搜索条件
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function getQueryString_SELBM( _
            ByRef strErrMsg As String, _
            ByRef strQuery As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            getQueryString_SELBM = False
            strQuery = ""

            Try
                '单位/范围的名称搜索
                Dim strDWMC As String = Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_DWMC
                If Me.txtSELBMSearch_BMMC.Text.Length > 0 Then Me.txtSELBMSearch_BMMC.Text = Me.txtSELBMSearch_BMMC.Text.Trim()
                If Me.txtSELBMSearch_BMMC.Text <> "" Then
                    Me.txtSELBMSearch_BMMC.Text = objPulicParameters.getNewSearchString(Me.txtSELBMSearch_BMMC.Text)
                    If strQuery = "" Then
                        strQuery = strDWMC + " like '" + Me.txtSELBMSearch_BMMC.Text + "%'"
                    Else
                        strQuery = strQuery + " and " + strDWMC + " like '" + Me.txtSELBMSearch_BMMC.Text + "%'"
                    End If
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            getQueryString_SELBM = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取tvwBMLIST要显示的数据信息
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function getModuleData_BMXX( _
            ByRef strErrMsg As String) As Boolean

            Dim objsystemCustomer As New Xydc.Platform.BusinessFacade.systemCustomer

            getModuleData_BMXX = False

            Try
                '释放资源
                If Not (Me.m_objDataSet_BMXX Is Nothing) Then
                    Me.m_objDataSet_BMXX.Dispose()
                    Me.m_objDataSet_BMXX = Nothing
                End If

                '重新检索数据
                If objsystemCustomer.getBumenData(strErrMsg, MyBase.UserId, MyBase.UserPassword, Me.m_objDataSet_BMXX) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.BusinessFacade.systemCustomer.SafeRelease(objsystemCustomer)

            getModuleData_BMXX = True
            Exit Function

errProc:
            Xydc.Platform.BusinessFacade.systemCustomer.SafeRelease(objsystemCustomer)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取grdFWLIST要显示的数据信息
        '     strErrMsg      ：返回错误信息
        '     strWhere       ：搜索条件
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function getModuleData_FWLIST( _
            ByRef strErrMsg As String, _
            ByVal strWhere As String) As Boolean

            Dim objsystemFenfafanwei As New Xydc.Platform.BusinessFacade.systemFenfafanwei

            getModuleData_FWLIST = False

            Try
                '备份Sort字符串
                Dim strSort As String
                strSort = Me.htxtFWLISTSort.Value
                If strSort.Length > 0 Then strSort = strSort.Trim

                '释放资源
                If Not (Me.m_objDataSet_FWLIST Is Nothing) Then
                    Me.m_objDataSet_FWLIST.Dispose()
                    Me.m_objDataSet_FWLIST = Nothing
                End If

                '重新检索数据
                If objsystemFenfafanwei.getFenfafanweiData(strErrMsg, MyBase.UserId, MyBase.UserPassword, strWhere, Me.m_objDataSet_FWLIST) = False Then
                    GoTo errProc
                End If

                '恢复Sort字符串
                With Me.m_objDataSet_FWLIST.Tables(Xydc.Platform.Common.Data.FenfafanweiData.TABLE_GW_B_FENFAFANWEI)
                    .DefaultView.Sort = strSort
                End With

                '缓存参数
                With Me.m_objDataSet_FWLIST.Tables(Xydc.Platform.Common.Data.FenfafanweiData.TABLE_GW_B_FENFAFANWEI)
                    Me.htxtFWLISTRows.Value = .DefaultView.Count.ToString()
                    Me.m_intRows_FWLIST = .DefaultView.Count
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.BusinessFacade.systemFenfafanwei.SafeRelease(objsystemFenfafanwei)

            getModuleData_FWLIST = True
            Exit Function

errProc:
            Xydc.Platform.BusinessFacade.systemFenfafanwei.SafeRelease(objsystemFenfafanwei)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取grdSELBM要显示的数据信息，并进行session缓存
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function getModuleData_SELBM( _
            ByRef strErrMsg As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            getModuleData_SELBM = False

            Dim strGuid As String
            Try
                If Me.IsPostBack = False Then
                    '获取Session的Id
                    strGuid = objPulicParameters.getNewGuid()
                    If strGuid = "" Then
                        strErrMsg = "无法产生GUID！"
                        GoTo errProc
                    End If

                    '初次调用空数据
                    Me.m_objDataSet_SELBM = New Xydc.Platform.Common.Data.CustomerData(Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_ZUZHIJIGOU_SELECT)

                    '根据初始值设置信息
                    If Me.m_objIDmxzZzjg.iBumenList <> "" Then
                        Dim strValue() As String
                        strValue = Me.m_objIDmxzZzjg.iBumenList.Split(Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate.ToCharArray())
                        Dim objDataRow As System.Data.DataRow
                        Dim intCount As Integer
                        Dim i As Integer
                        intCount = strValue.Length
                        For i = 0 To intCount - 1 Step 1
                            With Me.m_objDataSet_SELBM.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU_SELECT)
                                objDataRow = .NewRow()
                                objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_DWMC) = strValue(i)
                                .Rows.Add(objDataRow)
                            End With
                        Next
                    End If

                    '缓存信息
                    Me.m_strSessionId_SELBM = strGuid
                    Session.Add(Me.m_strSessionId_SELBM, Me.m_objDataSet_SELBM)
                    Me.htxtSessionIdSELBM.Value = Me.m_strSessionId_SELBM
                Else
                    '直接引用数据
                    Me.m_objDataSet_SELBM = CType(Session.Item(Me.m_strSessionId_SELBM), Xydc.Platform.Common.Data.CustomerData)
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            getModuleData_SELBM = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据屏幕搜索条件搜索grdFWLIST数据
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function searchModuleData_FWLIST(ByRef strErrMsg As String) As Boolean

            searchModuleData_FWLIST = False

            Try
                '获取搜索字符串
                Dim strQuery As String
                If Me.getQueryString_FWLIST(strErrMsg, strQuery) = False Then
                    GoTo errProc
                End If

                '搜索数据
                If Me.getModuleData_FWLIST(strErrMsg, strQuery) = False Then
                    GoTo errProc
                End If

                '缓存搜索条件
                Me.m_strQuery_FWLIST = strQuery
                Me.htxtFWLISTQuery.Value = Me.m_strQuery_FWLIST

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            searchModuleData_FWLIST = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据屏幕搜索条件搜索grdSELBM数据(RowFilter)
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function searchModuleData_SELBM(ByRef strErrMsg As String) As Boolean

            searchModuleData_SELBM = False

            Try
                '获取搜索字符串
                Dim strQuery As String
                If Me.getQueryString_SELBM(strErrMsg, strQuery) = False Then
                    GoTo errProc
                End If

                '搜索数据
                If Me.getModuleData_SELBM(strErrMsg) = False Then
                    GoTo errProc
                End If

                '设置新的搜索字符串
                Me.m_objDataSet_SELBM.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU_SELECT).DefaultView.RowFilter = strQuery
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            searchModuleData_SELBM = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示tvwBMLIST的数据
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showTreeViewInfo_BMXX( _
            ByRef strErrMsg As String) As Boolean

            Dim objTreeviewProcess As New Xydc.Platform.web.TreeviewProcess

            showTreeViewInfo_BMXX = False

            'TreeView显示处理
            Try
                '初始化tvwBMLIST
                If objTreeviewProcess.doDisplayTreeViewAll(strErrMsg, Me.tvwBMLIST, _
                    Me.m_objDataSet_BMXX.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU), _
                    Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_ZZDM, _
                    Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_ZZMC, _
                    True, True, Xydc.Platform.Common.Data.CustomerData.intZZDM_FJCDSM) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.TreeviewProcess.SafeRelease(objTreeviewProcess)

            showTreeViewInfo_BMXX = True
            Exit Function

errProc:
            Xydc.Platform.web.TreeviewProcess.SafeRelease(objTreeviewProcess)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示grdFWLIST的数据
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showDataGridInfo_FWLIST( _
            ByRef strErrMsg As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess

            showDataGridInfo_FWLIST = False

            '获取系统保存的网格排序数据
            Dim intSortColumnIndex As Integer
            intSortColumnIndex = objPulicParameters.getObjectValue(Me.htxtFWLISTSortColumnIndex.Value, -1)
            Dim objSortType As Xydc.Platform.Common.Utilities.PulicParameters.enumSortType
            Try
                objSortType = CType(Me.htxtFWLISTSortType.Value, Xydc.Platform.Common.Utilities.PulicParameters.enumSortType)
            Catch ex As Exception
                objSortType = Xydc.Platform.Common.Utilities.PulicParameters.enumSortType.None
            End Try

            '网格显示处理
            Try
                '在获取数据时已经恢复了RowFilter、Sort的现场
                '设置数据源
                If Me.m_objDataSet_FWLIST Is Nothing Then
                    Me.grdFWLIST.DataSource = Nothing
                Else
                    With Me.m_objDataSet_FWLIST.Tables(Xydc.Platform.Common.Data.FenfafanweiData.TABLE_GW_B_FENFAFANWEI)
                        Me.grdFWLIST.DataSource = .DefaultView
                    End With
                End If

                '调整网格参数
                With Me.m_objDataSet_FWLIST.Tables(Xydc.Platform.Common.Data.FenfafanweiData.TABLE_GW_B_FENFAFANWEI)
                    If objDataGridProcess.onBeforeDataGridBind(strErrMsg, Me.grdFWLIST, .DefaultView.Count) = False Then
                        GoTo errProc
                    End If
                End With

                '恢复列标题中的排序信息
                If intSortColumnIndex >= 0 Then
                    objDataGridProcess.doClearSortCharInDataGridHead(Me.grdFWLIST)
                    With Me.grdFWLIST.Columns(intSortColumnIndex)
                        .HeaderText = objDataGridProcess.getColumnSortHeadString(.HeaderText, objSortType)
                    End With
                End If

                '绑定数据
                Me.grdFWLIST.DataBind()

                '----------------------------------------------------------------
                '因为这些信息是非绑定的，所以下面的操作必须等绑定完成后执行！！！
                '一旦在后续处理中执行了DataBind，则信息会丢失！！！
                '----------------------------------------------------------------
                '恢复网格中的CheckBox状态
                If objDataGridProcess.doRestoreDataGridCheckBoxStatus(strErrMsg, Me.grdFWLIST, Request, 0, Me.m_cstrCheckBoxIdInDataGrid_FWLIST) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)

            showDataGridInfo_FWLIST = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示grdSELBM的数据
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showDataGridInfo_SELBM( _
            ByRef strErrMsg As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess

            showDataGridInfo_SELBM = False

            '获取系统保存的网格排序数据
            Dim intSortColumnIndex As Integer
            intSortColumnIndex = objPulicParameters.getObjectValue(Me.htxtSELBMSortColumnIndex.Value, -1)
            Dim objSortType As Xydc.Platform.Common.Utilities.PulicParameters.enumSortType
            Try
                objSortType = CType(Me.htxtSELBMSortType.Value, Xydc.Platform.Common.Utilities.PulicParameters.enumSortType)
            Catch ex As Exception
                objSortType = Xydc.Platform.Common.Utilities.PulicParameters.enumSortType.None
            End Try

            '网格显示处理
            Try
                '在获取数据时已经恢复了RowFilter、Sort的现场
                '设置数据源
                If Me.m_objDataSet_SELBM Is Nothing Then
                    Me.grdSELBM.DataSource = Nothing
                Else
                    With Me.m_objDataSet_SELBM.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU_SELECT)
                        Me.grdSELBM.DataSource = .DefaultView
                    End With
                End If

                '调整网格参数
                With Me.m_objDataSet_SELBM.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU_SELECT)
                    If objDataGridProcess.onBeforeDataGridBind(strErrMsg, Me.grdSELBM, .DefaultView.Count) = False Then
                        GoTo errProc
                    End If
                End With

                '恢复列标题中的排序信息
                If intSortColumnIndex >= 0 Then
                    objDataGridProcess.doClearSortCharInDataGridHead(Me.grdSELBM)
                    With Me.grdSELBM.Columns(intSortColumnIndex)
                        .HeaderText = objDataGridProcess.getColumnSortHeadString(.HeaderText, objSortType)
                    End With
                End If

                '绑定数据
                Me.grdSELBM.DataBind()

                '----------------------------------------------------------------
                '因为这些信息是非绑定的，所以下面的操作必须等绑定完成后执行！！！
                '一旦在后续处理中执行了DataBind，则信息会丢失！！！
                '----------------------------------------------------------------
                '恢复网格中的CheckBox状态
                If objDataGridProcess.doRestoreDataGridCheckBoxStatus(strErrMsg, Me.grdSELBM, Request, 0, Me.m_cstrCheckBoxIdInDataGrid_SELBM) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)

            showDataGridInfo_SELBM = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示grdFWLIST及相关信息
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showModuleData_FWLIST( _
            ByRef strErrMsg As String) As Boolean

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess

            showModuleData_FWLIST = False

            Try
                '显示网格信息
                If Me.showDataGridInfo_FWLIST(strErrMsg) = False Then
                    GoTo errProc
                End If

                '显示与网格紧密相关的操作或信息提示
                With Me.m_objDataSet_FWLIST.Tables(Xydc.Platform.Common.Data.FenfafanweiData.TABLE_GW_B_FENFAFANWEI).DefaultView
                    '显示网格位置信息
                    Me.lblFWLISTGridLocInfo.Text = objDataGridProcess.getDataGridLocation(Me.grdFWLIST, .Count)
                    '显示页面浏览功能
                    Me.lnkCZFWLISTMoveFirst.Enabled = objDataGridProcess.canDoMoveFirstPage(Me.grdFWLIST, .Count)
                    Me.lnkCZFWLISTMoveLast.Enabled = objDataGridProcess.canDoMoveLastPage(Me.grdFWLIST, .Count)
                    Me.lnkCZFWLISTMovePrev.Enabled = objDataGridProcess.canDoMovePreviousPage(Me.grdFWLIST, .Count)
                    Me.lnkCZFWLISTMoveNext.Enabled = objDataGridProcess.canDoMoveNextPage(Me.grdFWLIST, .Count)
                    '显示相关操作
                    Dim blnEnabled As Boolean
                    If .Count < 1 Then
                        blnEnabled = False
                    Else
                        blnEnabled = True
                    End If
                    Me.lnkCZFWLISTDeSelectAll.Enabled = blnEnabled
                    Me.lnkCZFWLISTSelectAll.Enabled = blnEnabled
                    Me.lnkCZFWLISTGotoPage.Enabled = blnEnabled
                    Me.lnkCZFWLISTSetPageSize.Enabled = blnEnabled
                    Me.btnFWLISTAdd.Enabled = blnEnabled
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)

            showModuleData_FWLIST = True
            Exit Function

errProc:
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示grdSELBM及相关信息
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showModuleData_SELBM( _
            ByRef strErrMsg As String) As Boolean

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess

            showModuleData_SELBM = False

            Try
                '显示网格信息
                If Me.showDataGridInfo_SELBM(strErrMsg) = False Then
                    GoTo errProc
                End If

                '显示与网格紧密相关的操作或信息提示
                With Me.m_objDataSet_SELBM.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU_SELECT).DefaultView
                    '显示网格位置信息
                    Me.lblSELBMGridLocInfo.Text = objDataGridProcess.getDataGridLocation(Me.grdSELBM, .Count)
                    '显示页面浏览功能
                    Me.lnkCZSELBMMoveFirst.Enabled = objDataGridProcess.canDoMoveFirstPage(Me.grdSELBM, .Count)
                    Me.lnkCZSELBMMoveLast.Enabled = objDataGridProcess.canDoMoveLastPage(Me.grdSELBM, .Count)
                    Me.lnkCZSELBMMovePrev.Enabled = objDataGridProcess.canDoMovePreviousPage(Me.grdSELBM, .Count)
                    Me.lnkCZSELBMMoveNext.Enabled = objDataGridProcess.canDoMoveNextPage(Me.grdSELBM, .Count)
                    '显示相关操作
                    Dim blnEnabled As Boolean
                    If .Count < 1 Then
                        blnEnabled = False
                    Else
                        blnEnabled = True
                    End If
                    Me.lnkCZSELBMDeSelectAll.Enabled = blnEnabled
                    Me.lnkCZSELBMSelectAll.Enabled = blnEnabled
                    Me.lnkCZSELBMGotoPage.Enabled = blnEnabled
                    Me.lnkCZSELBMSetPageSize.Enabled = blnEnabled
                    Me.btnSELBMDelete.Enabled = blnEnabled
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)

            showModuleData_SELBM = True
            Exit Function

errProc:
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
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
                    If Me.m_objIDmxzZzjg.iSelectFFFW = True Then
                        Me.lblTitle.Text = "[选择单位/范围、"
                    Else
                        Me.lblTitle.Text = "[选择单位、"
                    End If
                    If Me.m_objIDmxzZzjg.iMultiSelect = True Then
                        Me.lblTitle.Text += "多选]"
                    Else
                        Me.lblTitle.Text += "单选]"
                    End If
                    '允许手工输入？
                    Me.txtNewDWMC.Enabled = Me.m_objIDmxzZzjg.iAllowInput
                    Me.btnAddNew.Enabled = Me.txtNewDWMC.Enabled
                    Me.rblXZLX.Enabled = Me.txtNewDWMC.Enabled
                Catch ex As Exception
                End Try

                '显示Pannel
                Me.panelMain.Visible = True
                Me.panelError.Visible = Not Me.panelMain.Visible

                '执行键转译(不论是否是“回发”)
                Try
                    With New Xydc.Platform.web.ControlProcess
                        .doTranslateKey(Me.txtFWLISTPageIndex)
                        .doTranslateKey(Me.txtFWLISTPageSize)
                        .doTranslateKey(Me.txtFWLISTSearch_FWMC)
                        .doTranslateKey(Me.txtSELBMPageIndex)
                        .doTranslateKey(Me.txtSELBMPageSize)
                        .doTranslateKey(Me.txtSELBMSearch_BMMC)
                        .doTranslateKey(Me.txtNewDWMC)
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
            End If

            If Me.IsPostBack = False Then
                '获取数据
                If Me.getModuleData_BMXX(strErrMsg) = False Then
                    GoTo errProc
                End If
                '显示数据
                If Me.showTreeViewInfo_BMXX(strErrMsg) = False Then
                    GoTo errProc
                End If

                '获取数据
                If Me.getModuleData_FWLIST(strErrMsg, Me.m_strQuery_FWLIST) = False Then
                    GoTo errProc
                End If
                '显示数据
                If Me.showModuleData_FWLIST(strErrMsg) = False Then
                    GoTo errProc
                End If

                '获取数据
                If Me.getModuleData_SELBM(strErrMsg) = False Then
                    GoTo errProc
                End If
                '显示数据
                If Me.showModuleData_SELBM(strErrMsg) = False Then
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
            If MyBase.doPagePreprocess(True, False) = True Then
                Exit Sub
            End If

            '获取接口参数
            If Me.getInterfaceParameters(strErrMsg) = False Then
                GoTo errProc
            End If

            '控件初始化
            If Me.initializeControls(strErrMsg) = False Then
                GoTo errProc
            End If

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
        '实现对grdFWLIST网格行、列的固定
        Sub grdFWLIST_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles grdFWLIST.ItemDataBound

            Dim intCells As Integer
            Dim i As Integer

            Try
                If e.Item.ItemIndex < 0 Then
                    '标题行,输出标题锁定一般属性
                    intCells = e.Item.Cells.Count
                    For i = 0 To intCells - 1 Step 1
                        e.Item.Cells(i).Attributes.CssStyle.Add("top", "expression(" + Me.m_cstrDataGridInDIV_FWLIST + ".scrollTop)")
                    Next
                End If
                If Me.m_intFixedColumns_FWLIST > 0 Then
                    '锁定列
                    For i = 0 To Me.m_intFixedColumns_FWLIST - 1 Step 1
                        e.Item.Cells(i).CssClass = Me.grdFWLIST.ID + "Locked"
                    Next
                End If
            Catch ex As Exception
            End Try

        End Sub

        '实现对grdSELBM网格行、列的固定
        Sub grdSELBM_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles grdSELBM.ItemDataBound

            Dim intCells As Integer
            Dim i As Integer

            Try
                If e.Item.ItemIndex < 0 Then
                    '标题行,输出标题锁定一般属性
                    intCells = e.Item.Cells.Count
                    For i = 0 To intCells - 1 Step 1
                        e.Item.Cells(i).Attributes.CssStyle.Add("top", "expression(" + Me.m_cstrDataGridInDIV_SELBM + ".scrollTop)")
                    Next
                End If
                If Me.m_intFixedColumns_SELBM > 0 Then
                    '锁定列
                    For i = 0 To Me.m_intFixedColumns_SELBM - 1 Step 1
                        e.Item.Cells(i).CssClass = Me.grdSELBM.ID + "Locked"
                    Next
                End If
            Catch ex As Exception
            End Try

        End Sub

        Private Sub tvwBMLIST_Check(ByVal sender As Object, ByVal e As Microsoft.Web.UI.WebControls.TreeViewClickEventArgs) Handles tvwBMLIST.Check

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objsystemCustomer As New Xydc.Platform.BusinessFacade.systemCustomer
            Dim objsystemCommon As New Xydc.Platform.BusinessFacade.systemCommon
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim objTreeviewProcess As New Xydc.Platform.web.TreeviewProcess
            Dim objBumenData As Xydc.Platform.Common.Data.CustomerData
            Dim strErrMsg As String

            Try
                Dim objDataRow As System.Data.DataRow

                '获取选定节点
                Dim objTreeNode As Microsoft.Web.UI.WebControls.TreeNode
                objTreeNode = Me.tvwBMLIST.GetNodeFromIndex(e.Node)
                If objTreeNode Is Nothing Then
                    strErrMsg = "错误：未给部门打勾！"
                    GoTo errProc
                End If
                If objTreeNode.Checked = False Then
                    GoTo normExit
                End If

                '从节点ID中获取部门代码
                Dim strZZDM As String
                strZZDM = objTreeviewProcess.getCodeValueFromNodeId(objTreeNode.ID)
                If strZZDM = "" Then
                    GoTo normExit
                End If

                '获取部门信息
                If objsystemCustomer.getBumenData(strErrMsg, MyBase.UserId, MyBase.UserPassword, strZZDM, objBumenData) = False Then
                    GoTo errProc
                End If
                With objBumenData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU_FULLJOIN)
                    '没有数据
                    If .Rows.Count < 1 Then
                        GoTo normExit
                    End If
                End With

                '获取SELBM的数据
                If Me.getModuleData_SELBM(strErrMsg) = False Then
                    GoTo errProc
                End If

                '根据接口参数处理
                Dim blnFound As Boolean
                Dim strMC As String
                '检查是否存在
                With objBumenData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU_FULLJOIN)
                    strMC = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_ZZMC), "")
                End With
                If objsystemCommon.doFindInDataTable(strErrMsg, _
                    Me.m_objDataSet_SELBM.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU_SELECT), _
                    Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_DWMC, _
                    strMC, blnFound) = False Then
                    GoTo errProc
                End If
                If blnFound = True Then '存在
                    GoTo normExit
                End If

                '复制到m_objDataSet_SELBM
                objDataRow = Me.m_objDataSet_SELBM.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU_SELECT).NewRow()
                With objBumenData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU_FULLJOIN)
                    '设置数据
                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_DWMC) = .Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_ZZMC)
                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_XZLX) = Xydc.Platform.Common.Data.FenfafanweiData.CYLX_DANWEI
                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_DWQC) = .Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_ZZBM)
                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_DWJB) = .Rows(0).Item(Xydc.Platform.Common.Data.XingzhengjibieData.FIELD_GG_B_XINGZHENGJIBIE_JBMC)
                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_DWMS) = .Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_FULLJOIN_MSMC)
                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_SJHM) = .Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SJHM)
                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_LXDH) = .Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_LXDH)
                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_FTPDZ) = .Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_FTPDZ)
                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_YXDZ) = .Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_YXDZ)
                End With

                '加入表
                With Me.m_objDataSet_SELBM.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU_SELECT)
                    .Rows.Add(objDataRow)
                End With

                '重新显示网格
                If Me.showModuleData_SELBM(strErrMsg) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

normExit:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.BusinessFacade.systemCustomer.SafeRelease(objsystemCustomer)
            Xydc.Platform.BusinessFacade.systemCommon.SafeRelease(objsystemCommon)
            Xydc.Platform.Common.Data.CustomerData.SafeRelease(objBumenData)
            Xydc.Platform.web.TreeviewProcess.SafeRelease(objTreeviewProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.BusinessFacade.systemCustomer.SafeRelease(objsystemCustomer)
            Xydc.Platform.BusinessFacade.systemCommon.SafeRelease(objsystemCommon)
            Xydc.Platform.Common.Data.CustomerData.SafeRelease(objBumenData)
            Xydc.Platform.web.TreeviewProcess.SafeRelease(objTreeviewProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub grdFWLIST_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdFWLIST.SelectedIndexChanged

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '显示记录位置
                With New Xydc.Platform.web.DataGridProcess
                    Me.lblFWLISTGridLocInfo.Text = .getDataGridLocation(Me.grdFWLIST, Me.m_intRows_FWLIST)
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

        Private Sub grdSELBM_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSELBM.SelectedIndexChanged

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '获取数据
                If Me.getModuleData_SELBM(strErrMsg) = False Then
                    GoTo errProc
                End If
                '显示数据
                If Me.showModuleData_SELBM(strErrMsg) = False Then
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

        Private Sub grdFWLIST_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles grdFWLIST.SortCommand

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
                If Me.getModuleData_FWLIST(strErrMsg, Me.m_strQuery_FWLIST) = False Then
                    GoTo errProc
                End If

                '获取原排序命令
                strOldCommand = Me.m_objDataSet_FWLIST.Tables(Xydc.Platform.Common.Data.FenfafanweiData.TABLE_GW_B_FENFAFANWEI).DefaultView.Sort

                '获取要执行的排序命令
                objDataGridProcess.getColumnSortCommand(strErrMsg, strOldCommand, e.SortExpression, strFinalCommand, objenumSortType)
                If strErrMsg <> "" Then
                    GoTo errProc
                End If

                '执行排序
                Me.m_objDataSet_FWLIST.Tables(Xydc.Platform.Common.Data.FenfafanweiData.TABLE_GW_B_FENFAFANWEI).DefaultView.Sort = strFinalCommand

                '获取排序列的列索引
                objDataGridItem = CType(e.CommandSource, System.Web.UI.WebControls.DataGridItem)
                strUniqueId = Request.Form("__EVENTTARGET")
                intColumnIndex = objDataGridProcess.getColumnIndexByUniqueIdInRow(objDataGridItem, strUniqueId)

                '保存排序信息
                Me.htxtFWLISTSortColumnIndex.Value = intColumnIndex.ToString()
                Me.htxtFWLISTSortType.Value = CType(objenumSortType, Integer).ToString()
                Me.htxtFWLISTSort.Value = strFinalCommand

                '重新显示数据
                If Me.showModuleData_FWLIST(strErrMsg) = False Then
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

        Private Sub grdSELBM_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles grdSELBM.SortCommand

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
                If Me.getModuleData_SELBM(strErrMsg) = False Then
                    GoTo errProc
                End If

                '获取原排序命令
                strOldCommand = Me.m_objDataSet_SELBM.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU_SELECT).DefaultView.Sort

                '获取要执行的排序命令
                objDataGridProcess.getColumnSortCommand(strErrMsg, strOldCommand, e.SortExpression, strFinalCommand, objenumSortType)
                If strErrMsg <> "" Then
                    GoTo errProc
                End If

                '执行排序
                Me.m_objDataSet_SELBM.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU_SELECT).DefaultView.Sort = strFinalCommand

                '获取排序列的列索引
                objDataGridItem = CType(e.CommandSource, System.Web.UI.WebControls.DataGridItem)
                strUniqueId = Request.Form("__EVENTTARGET")
                intColumnIndex = objDataGridProcess.getColumnIndexByUniqueIdInRow(objDataGridItem, strUniqueId)

                '保存排序信息
                Me.htxtSELBMSortColumnIndex.Value = intColumnIndex.ToString()
                Me.htxtSELBMSortType.Value = CType(objenumSortType, Integer).ToString()

                '重新显示数据
                If Me.showModuleData_SELBM(strErrMsg) = False Then
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

        Private Sub doMoveFirst_FWLIST(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_FWLIST(strErrMsg, Me.m_strQuery_FWLIST) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(0, Me.grdFWLIST.PageCount)
                Me.grdFWLIST.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_FWLIST(strErrMsg) = False Then
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

        Private Sub doMoveLast_FWLIST(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_FWLIST(strErrMsg, Me.m_strQuery_FWLIST) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(Me.grdFWLIST.PageCount - 1, Me.grdFWLIST.PageCount)
                Me.grdFWLIST.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_FWLIST(strErrMsg) = False Then
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

        Private Sub doMoveNext_FWLIST(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_FWLIST(strErrMsg, Me.m_strQuery_FWLIST) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(Me.grdFWLIST.CurrentPageIndex + 1, Me.grdFWLIST.PageCount)
                Me.grdFWLIST.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_FWLIST(strErrMsg) = False Then
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

        Private Sub doMovePrevious_FWLIST(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_FWLIST(strErrMsg, Me.m_strQuery_FWLIST) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(Me.grdFWLIST.CurrentPageIndex - 1, Me.grdFWLIST.PageCount)
                Me.grdFWLIST.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_FWLIST(strErrMsg) = False Then
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

        Private Sub doMoveFirst_SELBM(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_SELBM(strErrMsg) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(0, Me.grdSELBM.PageCount)
                Me.grdSELBM.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_SELBM(strErrMsg) = False Then
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

        Private Sub doMoveLast_SELBM(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_SELBM(strErrMsg) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(Me.grdSELBM.PageCount - 1, Me.grdSELBM.PageCount)
                Me.grdSELBM.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_SELBM(strErrMsg) = False Then
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

        Private Sub doMoveNext_SELBM(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_SELBM(strErrMsg) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(Me.grdSELBM.CurrentPageIndex + 1, Me.grdSELBM.PageCount)
                Me.grdSELBM.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_SELBM(strErrMsg) = False Then
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

        Private Sub doMovePrevious_SELBM(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_SELBM(strErrMsg) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(Me.grdSELBM.CurrentPageIndex - 1, Me.grdSELBM.PageCount)
                Me.grdSELBM.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_SELBM(strErrMsg) = False Then
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

        Private Sub doGotoPage_FWLIST(ByVal strControlId As String)

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            '获取新的页大小
            Dim intPageIndex As Integer
            intPageIndex = objPulicParameters.getObjectValue(Me.txtFWLISTPageIndex.Text, 0)
            If intPageIndex <= 0 Then
                intPageIndex = 0
            Else
                intPageIndex -= 1
            End If

            Try
                '获取数据
                If Me.getModuleData_FWLIST(strErrMsg, Me.m_strQuery_FWLIST) = False Then
                    GoTo errProc
                End If

                '设置新的页
                Me.grdFWLIST.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_FWLIST(strErrMsg) = False Then
                    GoTo errProc
                End If

                '信息同步
                Me.txtFWLISTPageIndex.Text = (Me.grdFWLIST.CurrentPageIndex + 1).ToString()

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

        Private Sub doGotoPage_SELBM(ByVal strControlId As String)

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            '获取新的页大小
            Dim intPageIndex As Integer
            intPageIndex = objPulicParameters.getObjectValue(Me.txtSELBMPageIndex.Text, 0)
            If intPageIndex <= 0 Then
                intPageIndex = 0
            Else
                intPageIndex -= 1
            End If

            Try
                '获取数据
                If Me.getModuleData_SELBM(strErrMsg) = False Then
                    GoTo errProc
                End If

                '设置新的页
                Me.grdSELBM.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_SELBM(strErrMsg) = False Then
                    GoTo errProc
                End If

                '信息同步
                Me.txtSELBMPageIndex.Text = (Me.grdSELBM.CurrentPageIndex + 1).ToString()

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

        Private Sub doSetPageSize_FWLIST(ByVal strControlId As String)

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            '获取新的页大小
            Dim intPageSize As Integer
            intPageSize = objPulicParameters.getObjectValue(Me.txtFWLISTPageSize.Text, 100)
            If intPageSize <= 0 Then intPageSize = 100

            Try
                '获取数据
                If Me.getModuleData_FWLIST(strErrMsg, Me.m_strQuery_FWLIST) = False Then
                    GoTo errProc
                End If

                '设置新的页大小
                Me.grdFWLIST.PageSize = intPageSize

                '刷新网格显示
                If Me.showModuleData_FWLIST(strErrMsg) = False Then
                    GoTo errProc
                End If

                '信息同步
                Me.txtFWLISTPageSize.Text = (Me.grdFWLIST.PageSize).ToString()

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

        Private Sub doSetPageSize_SELBM(ByVal strControlId As String)

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            '获取新的页大小
            Dim intPageSize As Integer
            intPageSize = objPulicParameters.getObjectValue(Me.txtSELBMPageSize.Text, 100)
            If intPageSize <= 0 Then intPageSize = 100

            Try
                '获取数据
                If Me.getModuleData_SELBM(strErrMsg) = False Then
                    GoTo errProc
                End If

                '设置新的页大小
                Me.grdSELBM.PageSize = intPageSize

                '刷新网格显示
                If Me.showModuleData_SELBM(strErrMsg) = False Then
                    GoTo errProc
                End If

                '信息同步
                Me.txtSELBMPageSize.Text = (Me.grdSELBM.PageSize).ToString()

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

        Private Sub doSelectAll_FWLIST(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                If objDataGridProcess.doCheckedDataGridCheckBox(strErrMsg, Me.grdFWLIST, 0, Me.m_cstrCheckBoxIdInDataGrid_FWLIST, True) = False Then
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

        Private Sub doSelectAll_SELBM(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                If objDataGridProcess.doCheckedDataGridCheckBox(strErrMsg, Me.grdSELBM, 0, Me.m_cstrCheckBoxIdInDataGrid_SELBM, True) = False Then
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

        Private Sub doDeSelectAll_FWLIST(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                If objDataGridProcess.doCheckedDataGridCheckBox(strErrMsg, Me.grdFWLIST, 0, Me.m_cstrCheckBoxIdInDataGrid_FWLIST, False) = False Then
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

        Private Sub doDeSelectAll_SELBM(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                If objDataGridProcess.doCheckedDataGridCheckBox(strErrMsg, Me.grdSELBM, 0, Me.m_cstrCheckBoxIdInDataGrid_SELBM, False) = False Then
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

        Private Sub doSearch_FWLIST(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '搜索数据
                If Me.searchModuleData_FWLIST(strErrMsg) = False Then
                    GoTo errProc
                End If

                '刷新网格显示
                If Me.showModuleData_FWLIST(strErrMsg) = False Then
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

        Private Sub doSearch_SELBM(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '搜索数据
                If Me.searchModuleData_SELBM(strErrMsg) = False Then
                    GoTo errProc
                End If

                '刷新网格显示
                If Me.showModuleData_SELBM(strErrMsg) = False Then
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

        Private Sub lnkCZFWLISTMoveFirst_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZFWLISTMoveFirst.Click
            Me.doMoveFirst_FWLIST("lnkCZFWLISTMoveFirst")
        End Sub

        Private Sub lnkCZFWLISTMoveLast_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZFWLISTMoveLast.Click
            Me.doMoveLast_FWLIST("lnkCZFWLISTMoveLast")
        End Sub

        Private Sub lnkCZFWLISTMoveNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZFWLISTMoveNext.Click
            Me.doMoveNext_FWLIST("lnkCZFWLISTMoveNext")
        End Sub

        Private Sub lnkCZFWLISTMovePrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZFWLISTMovePrev.Click
            Me.doMovePrevious_FWLIST("lnkCZFWLISTMovePrev")
        End Sub

        Private Sub lnkCZSELBMMoveFirst_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZSELBMMoveFirst.Click
            Me.doMoveFirst_SELBM("lnkCZSELBMMoveFirst")
        End Sub

        Private Sub lnkCZSELBMMoveLast_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZSELBMMoveLast.Click
            Me.doMoveLast_SELBM("lnkCZSELBMMoveLast")
        End Sub

        Private Sub lnkCZSELBMMoveNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZSELBMMoveNext.Click
            Me.doMoveNext_SELBM("lnkCZSELBMMoveNext")
        End Sub

        Private Sub lnkCZSELBMMovePrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZSELBMMovePrev.Click
            Me.doMovePrevious_SELBM("lnkCZSELBMMovePrev")
        End Sub

        Private Sub lnkCZFWLISTGotoPage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZFWLISTGotoPage.Click
            Me.doGotoPage_FWLIST("lnkCZFWLISTGotoPage")
        End Sub

        Private Sub lnkCZSELBMGotoPage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZSELBMGotoPage.Click
            Me.doGotoPage_SELBM("lnkCZSELBMGotoPage")
        End Sub

        Private Sub lnkCZFWLISTSetPageSize_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZFWLISTSetPageSize.Click
            Me.doSetPageSize_FWLIST("lnkCZFWLISTSetPageSize")
        End Sub

        Private Sub lnkCZSELBMSetPageSize_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZSELBMSetPageSize.Click
            Me.doSetPageSize_SELBM("lnkCZSELBMSetPageSize")
        End Sub

        Private Sub lnkCZFWLISTSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZFWLISTSelectAll.Click
            Me.doSelectAll_FWLIST("lnkCZFWLISTSelectAll")
        End Sub

        Private Sub lnkCZSELBMSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZSELBMSelectAll.Click
            Me.doSelectAll_SELBM("lnkCZSELBMSelectAll")
        End Sub

        Private Sub lnkCZFWLISTDeSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZFWLISTDeSelectAll.Click
            Me.doDeSelectAll_FWLIST("lnkCZFWLISTDeSelectAll")
        End Sub

        Private Sub lnkCZSELBMDeSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZSELBMDeSelectAll.Click
            Me.doDeSelectAll_SELBM("lnkCZSELBMDeSelectAll")
        End Sub

        Private Sub btnFWLISTSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFWLISTSearch.Click
            Me.doSearch_FWLIST("btnFWLISTSearch")
        End Sub

        Private Sub btnSELBMSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSELBMSearch.Click
            Me.doSearch_SELBM("btnSELBMSearch")
        End Sub



        '----------------------------------------------------------------
        '模块特殊操作处理器
        '----------------------------------------------------------------
        '处理“取消”按钮
        Private Sub doCancel(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            '设置返回参数
            Me.m_objIDmxzZzjg.oExitMode = False

            '释放模块资源
            Me.releaseModuleParameters()
            Me.releaseInterfaceParameters()

            '返回到调用模块，并附加返回参数
            '要返回的SessionId
            Dim strSessionId As String
            strSessionId = Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.ISessionId)
            'SessionId附加到返回的Url
            Dim strUrl As String
            strUrl = Me.m_objIDmxzZzjg.getReturnUrl(Server, Xydc.Platform.Common.Utilities.PulicParameters.OSessionId, strSessionId)
            '返回
            Response.Redirect(strUrl)

            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        '处理选择单位“移出”按钮
        Private Sub doDelete_SELBM(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '获取数据
                If Me.getModuleData_SELBM(strErrMsg) = False Then
                    GoTo errProc
                End If

                '检查选择
                Dim blnChecked As Boolean
                Dim intRecPos As Integer
                Dim blnDo As Boolean
                Dim intCount As Integer
                Dim i As Integer
                intCount = Me.grdSELBM.Items.Count
                blnDo = False
                For i = intCount - 1 To 0 Step -1
                    blnChecked = objDataGridProcess.isDataGridItemChecked(Me.grdSELBM.Items(i), 0, Me.m_cstrCheckBoxIdInDataGrid_SELBM)
                    If blnChecked = True Then
                        '获取记录位置
                        intRecPos = objDataGridProcess.getRecordPosition(i, Me.grdSELBM.CurrentPageIndex, Me.grdSELBM.PageSize)

                        '删除
                        With Me.m_objDataSet_SELBM.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU_SELECT)
                            .Rows.Remove(.DefaultView.Item(intRecPos).Row)
                        End With

                        '标志发生修改
                        blnDo = True
                    End If
                Next

                '刷新显示
                If blnDo = True Then
                    If Me.showModuleData_SELBM(strErrMsg) = False Then
                        GoTo errProc
                    End If
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

normExit:
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        '处理我的常用范围“加入”按钮
        Private Sub doAddfromFWLIST_SELBM(ByVal strControlId As String)

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objsystemCustomer As New Xydc.Platform.BusinessFacade.systemCustomer
            Dim objsystemCommon As New Xydc.Platform.BusinessFacade.systemCommon
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '获取数据
                If Me.getModuleData_SELBM(strErrMsg) = False Then
                    GoTo errProc
                End If

                '获取SELBM对应到BMRY表中的列索引
                Dim intColIndex(10) As Integer
                intColIndex(0) = objDataGridProcess.getDataGridColumnIndex(Me.grdFWLIST, Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_FWMC)
                intColIndex(1) = -1
                intColIndex(2) = -1
                intColIndex(3) = -1
                intColIndex(4) = -1
                intColIndex(5) = -1
                intColIndex(6) = objDataGridProcess.getDataGridColumnIndex(Me.grdFWLIST, Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_SJHM)
                intColIndex(7) = objDataGridProcess.getDataGridColumnIndex(Me.grdFWLIST, Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_LXDH)
                intColIndex(8) = objDataGridProcess.getDataGridColumnIndex(Me.grdFWLIST, Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_FTPDZ)
                intColIndex(9) = objDataGridProcess.getDataGridColumnIndex(Me.grdFWLIST, Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_YXDZ)

                '检查选择
                Dim objDataRow As System.Data.DataRow
                Dim blnChecked As Boolean
                Dim blnFound As Boolean
                Dim strFWMC As String
                Dim blnDo As Boolean
                Dim intCount As Integer
                Dim i As Integer
                intCount = Me.grdFWLIST.Items.Count
                blnDo = False
                For i = 0 To intCount - 1 Step 1
                    blnChecked = objDataGridProcess.isDataGridItemChecked(Me.grdFWLIST.Items(i), 0, Me.m_cstrCheckBoxIdInDataGrid_FWLIST)
                    If blnChecked = True Then
                        '获取范围名称
                        strFWMC = objDataGridProcess.getDataGridCellValue(Me.grdFWLIST.Items(i), intColIndex(0))

                        If Me.m_objIDmxzZzjg.iSelectFFFW = True Then '可以直接选择范围
                            '检查是否存在？
                            If objsystemCommon.doFindInDataTable(strErrMsg, _
                                Me.m_objDataSet_SELBM.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU_SELECT), _
                                Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_DWMC, _
                                strFWMC, blnFound) = False Then
                                GoTo errProc
                            End If

                            If blnFound = False Then
                                '加入
                                With Me.m_objDataSet_SELBM.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU_SELECT)
                                    objDataRow = .NewRow()
                                End With
                                With objDataGridProcess
                                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_XZLX) = Xydc.Platform.Common.Data.FenfafanweiData.CYLX_FANWEI
                                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_DWMC) = .getDataGridCellValue(Me.grdFWLIST.Items(i), intColIndex(0))
                                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_DWQC) = ""
                                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_DWJB) = ""
                                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_DWMS) = ""
                                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_SJHM) = .getDataGridCellValue(Me.grdFWLIST.Items(i), intColIndex(6))
                                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_LXDH) = .getDataGridCellValue(Me.grdFWLIST.Items(i), intColIndex(7))
                                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_FTPDZ) = .getDataGridCellValue(Me.grdFWLIST.Items(i), intColIndex(8))
                                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_YXDZ) = .getDataGridCellValue(Me.grdFWLIST.Items(i), intColIndex(9))
                                End With
                                With Me.m_objDataSet_SELBM.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU_SELECT)
                                    .Rows.Add(objDataRow)
                                End With

                                '标志发生修改
                                blnDo = True
                            End If

                        Else '只能选择范围下的部门
                            Dim objBumenDataInFanwei As Xydc.Platform.Common.Data.CustomerData
                            Dim intCYCount As Integer
                            Dim strCYMC As String
                            Dim j As Integer

                            '获取范围内的部门与人员
                            If objsystemCustomer.getBumenInFanweiData(strErrMsg, _
                                MyBase.UserId, MyBase.UserPassword, _
                                strFWMC, "", _
                                objBumenDataInFanwei) = False Then
                                GoTo errProc
                            End If

                            With objBumenDataInFanwei.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU_SELECT)
                                intCYCount = .Rows.Count
                                For j = 0 To intCYCount - 1 Step 1
                                    '计算成员名称
                                    strCYMC = objPulicParameters.getObjectValue(.Rows(j).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_DWMC), "")

                                    '检查是否存在？
                                    If objsystemCommon.doFindInDataTable(strErrMsg, _
                                        Me.m_objDataSet_SELBM.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU_SELECT), _
                                        Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_DWMC, _
                                        strCYMC, blnFound) = False Then
                                        GoTo errProc
                                    End If

                                    If blnFound = False Then
                                        '加入
                                        With Me.m_objDataSet_SELBM.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU_SELECT)
                                            objDataRow = .NewRow()
                                        End With
                                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_XZLX) = .Rows(j).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_XZLX)
                                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_DWMC) = .Rows(j).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_DWMC)
                                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_DWQC) = .Rows(j).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_DWQC)
                                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_DWJB) = .Rows(j).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_DWJB)
                                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_DWMS) = .Rows(j).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_DWMS)
                                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_SJHM) = .Rows(j).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_SJHM)
                                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_LXDH) = .Rows(j).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_LXDH)
                                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_FTPDZ) = .Rows(j).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_FTPDZ)
                                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_YXDZ) = .Rows(j).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_YXDZ)
                                        With Me.m_objDataSet_SELBM.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU_SELECT)
                                            .Rows.Add(objDataRow)
                                        End With

                                        '标志发生修改
                                        blnDo = True
                                    End If
                                Next
                            End With

                            '释放临时资源
                            If Not (objBumenDataInFanwei Is Nothing) Then
                                objBumenDataInFanwei.Dispose()
                                objBumenDataInFanwei = Nothing
                            End If
                        End If
                    End If
                Next

                '刷新显示
                If blnDo = True Then
                    If Me.showModuleData_SELBM(strErrMsg) = False Then
                        GoTo errProc
                    End If
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

normExit:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.BusinessFacade.systemCustomer.SafeRelease(objsystemCustomer)
            Xydc.Platform.BusinessFacade.systemCommon.SafeRelease(objsystemCommon)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.BusinessFacade.systemCustomer.SafeRelease(objsystemCustomer)
            Xydc.Platform.BusinessFacade.systemCommon.SafeRelease(objsystemCommon)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        '处理手工输入代码的按钮
        Private Sub doAddfromInput_SELBM(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '检查输入
                Dim objListItem As System.Web.UI.WebControls.ListItem
                With New Xydc.Platform.web.RadioButtonListProcess
                    objListItem = .getCheckedItem(Me.rblXZLX)
                End With
                If objListItem Is Nothing Then
                    strErrMsg = "错误：没有指定类型[单位/范围]！"
                    GoTo errProc
                End If
                If Me.txtNewDWMC.Text.Length > 0 Then Me.txtNewDWMC.Text = Me.txtNewDWMC.Text.Trim()
                If Me.txtNewDWMC.Text = "" Then
                    strErrMsg = "错误：没有输入[单位/范围]的值！"
                    GoTo errProc
                End If

                '获取数据
                If Me.getModuleData_SELBM(strErrMsg) = False Then
                    GoTo errProc
                End If

                '检查是否存在？
                Dim blnFound As Boolean
                With New Xydc.Platform.BusinessFacade.systemCommon
                    If .doFindInDataTable(strErrMsg, _
                        Me.m_objDataSet_SELBM.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU_SELECT), _
                        Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_DWMC, _
                        Me.txtNewDWMC.Text, blnFound) = False Then
                        GoTo errProc
                    End If
                End With

                Dim objDataRow As System.Data.DataRow
                Dim blnDo As Boolean = False
                If blnFound = False Then
                    '加入
                    With Me.m_objDataSet_SELBM.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU_SELECT)
                        objDataRow = .NewRow()
                    End With
                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_XZLX) = objListItem.Text
                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_DWMC) = Me.txtNewDWMC.Text
                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_DWQC) = ""
                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_DWJB) = ""
                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_DWMS) = ""
                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_SJHM) = ""
                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_LXDH) = ""
                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_FTPDZ) = ""
                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_YXDZ) = ""
                    With Me.m_objDataSet_SELBM.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU_SELECT)
                        .Rows.Add(objDataRow)
                    End With

                    '标志发生修改
                    blnDo = True
                End If

                '刷新显示
                If blnDo = True Then
                    If Me.showModuleData_SELBM(strErrMsg) = False Then
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

        '处理“确定”按钮
        Private Sub doConfirm(ByVal strControlId As String)

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            '获取选择数据
            If Me.getModuleData_SELBM(strErrMsg) = False Then
                GoTo errProc
            End If

            Dim strReturnValue As String = ""
            Try
                '检查选择数据
                With Me.m_objDataSet_SELBM.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU_SELECT)
                    If .Rows.Count < 1 And Me.m_objIDmxzZzjg.iAllowNull = False Then
                        strErrMsg = "错误：没有选择任何内容！"
                        GoTo errProc
                    End If
                    If Me.m_objIDmxzZzjg.iMultiSelect = False Then
                        If .Rows.Count > 1 Then
                            strErrMsg = "错误：只允许选择1条！"
                            GoTo errProc
                        End If
                    End If
                End With

                With Me.m_objDataSet_SELBM.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU_SELECT)
                    If .Rows.Count < 1 Then
                        '设置返回值
                        Me.m_objIDmxzZzjg.oBumenList = ""
                    Else
                        '获取返回参数
                        Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate
                        Dim strValue As String
                        Dim intCount As Integer
                        Dim i As Integer
                        With Me.m_objDataSet_SELBM.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU_SELECT)
                            intCount = .Rows.Count
                            For i = 0 To intCount - 1 Step 1
                                strValue = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_DWMC), "")
                                If strValue <> "" Then
                                    If strReturnValue <> "" Then
                                        strReturnValue = strReturnValue + strSep + strValue
                                    Else
                                        strReturnValue = strValue
                                    End If
                                End If
                            Next
                        End With

                        '清除所有的RowFilter
                        With Me.m_objDataSet_SELBM.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU_SELECT)
                            .DefaultView.RowFilter = ""
                        End With

                        '设置返回值
                        Me.m_objIDmxzZzjg.oBumenList = strReturnValue
                        Me.m_objIDmxzZzjg.oDataSet = Me.m_objDataSet_SELBM


                        If Me.m_strSessionId_SELBM.Trim <> "" Then
                            Try
                                Session.Remove(Me.m_strSessionId_SELBM)
                            Catch ex As Exception
                            End Try
                            Me.m_strSessionId_SELBM = ""
                        End If

                    End If
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            '设置返回参数
            Me.m_objIDmxzZzjg.oExitMode = True

            '释放模块资源
            Me.releaseModuleParameters()
            Me.releaseInterfaceParameters()

            '返回到调用模块，并附加返回参数
            '要返回的SessionId
            Dim strSessionId As String
            strSessionId = Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.ISessionId)
            'SessionId附加到返回的Url
            Dim strUrl As String
            strUrl = Me.m_objIDmxzZzjg.getReturnUrl(Server, Xydc.Platform.Common.Utilities.PulicParameters.OSessionId, strSessionId)
            '返回
            Response.Redirect(strUrl)

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
            Me.doCancel("btnCancel")
        End Sub

        Private Sub btnSELBMDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSELBMDelete.Click
            Me.doDelete_SELBM("btnSELBMDelete")
        End Sub

        Private Sub btnFWLISTAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFWLISTAdd.Click
            Me.doAddfromFWLIST_SELBM("btnFWLISTAdd")
        End Sub

        Private Sub btnAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
            Me.doAddfromInput_SELBM("btnAddNew")
        End Sub

        Private Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.Click
            Me.doConfirm("btnOK")
        End Sub

        '处理单个我的常用范围“加入”按钮
        Private Function doAddfromFWLIST_SELBM_One(ByRef strErrMsg As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objsystemCustomer As New Xydc.Platform.BusinessFacade.systemCustomer
            Dim objsystemCommon As New Xydc.Platform.BusinessFacade.systemCommon
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess

            doAddfromFWLIST_SELBM_One = False

            Try
                '检查当前行
                If Me.grdFWLIST.Items.Count < 1 Then
                    strErrMsg = "错误：没有数据！"
                    GoTo errProc
                End If
                If Me.grdFWLIST.SelectedIndex < 0 Then
                    strErrMsg = "错误：没有选定数据！"
                    GoTo errProc
                End If

                '获取数据
                If Me.getModuleData_SELBM(strErrMsg) = False Then
                    GoTo errProc
                End If

                '获取SELBM对应到BMRY表中的列索引
                Dim intColIndex(10) As Integer
                intColIndex(0) = objDataGridProcess.getDataGridColumnIndex(Me.grdFWLIST, Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_FWMC)
                intColIndex(1) = -1
                intColIndex(2) = -1
                intColIndex(3) = -1
                intColIndex(4) = -1
                intColIndex(5) = -1
                intColIndex(6) = objDataGridProcess.getDataGridColumnIndex(Me.grdFWLIST, Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_SJHM)
                intColIndex(7) = objDataGridProcess.getDataGridColumnIndex(Me.grdFWLIST, Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_LXDH)
                intColIndex(8) = objDataGridProcess.getDataGridColumnIndex(Me.grdFWLIST, Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_FTPDZ)
                intColIndex(9) = objDataGridProcess.getDataGridColumnIndex(Me.grdFWLIST, Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_YXDZ)

                '检查选择
                Dim objDataRow As System.Data.DataRow
                Dim blnChecked As Boolean
                Dim blnFound As Boolean
                Dim strFWMC As String
                Dim blnDo As Boolean
                Dim i As Integer
                i = Me.grdFWLIST.SelectedIndex
                blnDo = False

                '获取范围名称
                strFWMC = objDataGridProcess.getDataGridCellValue(Me.grdFWLIST.Items(i), intColIndex(0))

                If Me.m_objIDmxzZzjg.iSelectFFFW = True Then '可以直接选择范围
                    '检查是否存在？
                    If objsystemCommon.doFindInDataTable(strErrMsg, _
                        Me.m_objDataSet_SELBM.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU_SELECT), _
                        Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_DWMC, _
                        strFWMC, blnFound) = False Then
                        GoTo errProc
                    End If

                    If blnFound = False Then
                        '加入
                        With Me.m_objDataSet_SELBM.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU_SELECT)
                            objDataRow = .NewRow()
                        End With
                        With objDataGridProcess
                            objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_XZLX) = Xydc.Platform.Common.Data.FenfafanweiData.CYLX_FANWEI
                            objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_DWMC) = .getDataGridCellValue(Me.grdFWLIST.Items(i), intColIndex(0))
                            objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_DWQC) = ""
                            objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_DWJB) = ""
                            objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_DWMS) = ""
                            objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_SJHM) = .getDataGridCellValue(Me.grdFWLIST.Items(i), intColIndex(6))
                            objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_LXDH) = .getDataGridCellValue(Me.grdFWLIST.Items(i), intColIndex(7))
                            objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_FTPDZ) = .getDataGridCellValue(Me.grdFWLIST.Items(i), intColIndex(8))
                            objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_YXDZ) = .getDataGridCellValue(Me.grdFWLIST.Items(i), intColIndex(9))
                        End With
                        With Me.m_objDataSet_SELBM.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU_SELECT)
                            .Rows.Add(objDataRow)
                        End With

                        '标志发生修改
                        blnDo = True
                    End If

                Else '只能选择范围下的部门
                    Dim objBumenDataInFanwei As Xydc.Platform.Common.Data.CustomerData
                    Dim intCYCount As Integer
                    Dim strCYMC As String
                    Dim j As Integer

                    '获取范围内的部门与人员
                    If objsystemCustomer.getBumenInFanweiData(strErrMsg, _
                        MyBase.UserId, MyBase.UserPassword, _
                        strFWMC, "", _
                        objBumenDataInFanwei) = False Then
                        GoTo errProc
                    End If

                    With objBumenDataInFanwei.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU_SELECT)
                        intCYCount = .Rows.Count
                        For j = 0 To intCYCount - 1 Step 1
                            '计算成员名称
                            strCYMC = objPulicParameters.getObjectValue(.Rows(j).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_DWMC), "")

                            '检查是否存在？
                            If objsystemCommon.doFindInDataTable(strErrMsg, _
                                Me.m_objDataSet_SELBM.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU_SELECT), _
                                Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_DWMC, _
                                strCYMC, blnFound) = False Then
                                GoTo errProc
                            End If

                            If blnFound = False Then
                                '加入
                                With Me.m_objDataSet_SELBM.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU_SELECT)
                                    objDataRow = .NewRow()
                                End With
                                objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_XZLX) = .Rows(j).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_XZLX)
                                objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_DWMC) = .Rows(j).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_DWMC)
                                objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_DWQC) = .Rows(j).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_DWQC)
                                objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_DWJB) = .Rows(j).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_DWJB)
                                objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_DWMS) = .Rows(j).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_DWMS)
                                objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_SJHM) = .Rows(j).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_SJHM)
                                objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_LXDH) = .Rows(j).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_LXDH)
                                objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_FTPDZ) = .Rows(j).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_FTPDZ)
                                objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_YXDZ) = .Rows(j).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SELECT_YXDZ)
                                With Me.m_objDataSet_SELBM.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU_SELECT)
                                    .Rows.Add(objDataRow)
                                End With

                                '标志发生修改
                                blnDo = True
                            End If
                        Next
                    End With

                    '释放临时资源
                    If Not (objBumenDataInFanwei Is Nothing) Then
                        objBumenDataInFanwei.Dispose()
                        objBumenDataInFanwei = Nothing
                    End If
                End If

                '刷新显示
                If blnDo = True Then
                    If Me.showModuleData_SELBM(strErrMsg) = False Then
                        GoTo errProc
                    End If
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

normExit:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.BusinessFacade.systemCustomer.SafeRelease(objsystemCustomer)
            Xydc.Platform.BusinessFacade.systemCommon.SafeRelease(objsystemCommon)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)

            doAddfromFWLIST_SELBM_One = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.BusinessFacade.systemCustomer.SafeRelease(objsystemCustomer)
            Xydc.Platform.BusinessFacade.systemCommon.SafeRelease(objsystemCommon)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Function

        End Function

        Private Sub grdFWLIST_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdFWLIST.ItemCommand

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '修改当前行
                Me.grdFWLIST.SelectedIndex = e.Item.ItemIndex

                '显示记录位置
                With New Xydc.Platform.web.DataGridProcess
                    Me.lblFWLISTGridLocInfo.Text = .getDataGridLocation(Me.grdFWLIST, Me.m_intRows_FWLIST)
                End With

                '处理
                Select Case e.CommandName.ToUpper()
                    Case "AddOneRow".ToUpper()
                        If Me.doAddfromFWLIST_SELBM_One(strErrMsg) = False Then
                            GoTo errProc
                        End If
                    Case Else
                End Select

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

        '处理单个选择单位“移出”按钮
        Private Function doDelete_SELBM_One(ByRef strErrMsg As String) As Boolean

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess

            doDelete_SELBM_One = False

            Try
                '检查当前行
                If Me.grdSELBM.Items.Count < 1 Then
                    strErrMsg = "错误：没有数据！"
                    GoTo errProc
                End If
                If Me.grdSELBM.SelectedIndex < 0 Then
                    strErrMsg = "错误：没有选定数据！"
                    GoTo errProc
                End If

                '获取数据
                If Me.getModuleData_SELBM(strErrMsg) = False Then
                    GoTo errProc
                End If

                '检查选择
                Dim blnChecked As Boolean
                Dim intRecPos As Integer
                Dim blnDo As Boolean
                Dim i As Integer
                i = Me.grdSELBM.SelectedIndex
                blnDo = False

                '获取记录位置
                intRecPos = objDataGridProcess.getRecordPosition(i, Me.grdSELBM.CurrentPageIndex, Me.grdSELBM.PageSize)

                '删除
                With Me.m_objDataSet_SELBM.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU_SELECT)
                    .Rows.Remove(.DefaultView.Item(intRecPos).Row)
                End With

                '标志发生修改
                blnDo = True

                '刷新显示
                If blnDo = True Then
                    If Me.showModuleData_SELBM(strErrMsg) = False Then
                        GoTo errProc
                    End If
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

normExit:
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)

            doDelete_SELBM_One = True
            Exit Function

errProc:
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Function

        End Function

        Private Sub grdSELBM_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdSELBM.ItemCommand

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '修改当前行
                Me.grdSELBM.SelectedIndex = e.Item.ItemIndex

                '处理
                Select Case e.CommandName.ToUpper()
                    Case "DeleteOneRow".ToUpper()
                        If Me.doDelete_SELBM_One(strErrMsg) = False Then
                            GoTo errProc
                        End If
                    Case Else
                End Select

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

    End Class
End Namespace