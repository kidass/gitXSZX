Imports System.Web.Security

Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.web
    ' 类名    ：dmxz_jbdm
    ' 
    ' 调用性质：
    '     可被其他模块调用，本身不调用其他模块
    '
    ' 功能描述： 
    '   　基础代码选择处理模块。
    '
    ' 接口参数：
    '     参见IDmxzJbdm接口类描述
    '----------------------------------------------------------------

    Partial Public Class dmxz_jbdm
        Inherits Xydc.Platform.web.PageBase

        '----------------------------------------------------------------
        '模块私用参数
        '----------------------------------------------------------------
        '本模块相对image的相对路径
        Private m_cstrRelativePathToImage As String = "../../"

        '----------------------------------------------------------------
        '与数据网格grdCodeData相关的参数
        '----------------------------------------------------------------
        '网格中模板列中的控件ID
        Private Const m_cstrCheckBoxIdInDataGrid As String = "chkCodeData"
        '包含网格的DIV对象ID
        Private Const m_cstrDataGridInDIV As String = "divCodeData"
        '包含网格的DIV对象的宽度(px)
        Private Const m_cintMaxWidthDataGridInDIV As Integer = 740
        '网格要锁定的列数
        Private m_intFixedColumns As Integer

        '----------------------------------------------------------------
        '模块接口参数
        '----------------------------------------------------------------
        Private m_objIDmxzJbdm As Xydc.Platform.BusinessFacade.IDmxzJbdm

        '----------------------------------------------------------------
        '因为数据量不大，缓存代码数据的数据集。
        '这意味着同时保存了数据集的SORT、ROWFILTER特性
        '    m_strSessionId_CodeData保存m_objDataSet_CodeData缓存在session中的name，
        '    模块退出时释放
        '----------------------------------------------------------------
        Private m_objDataSet_CodeData As System.Data.DataSet
        Private m_strSessionId_CodeData As String









        '----------------------------------------------------------------
        ' 释放接口参数
        '----------------------------------------------------------------
        Private Sub releaseInterfaceParameters()

            Try
                If Not (Me.m_objIDmxzJbdm Is Nothing) Then
                    If Me.m_objIDmxzJbdm.iInterfaceType = Xydc.Platform.BusinessFacade.ICallInterface.enumInterfaceType.InputOnly Then
                        '释放Session
                        Session.Remove(Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.ISessionId))
                        '释放对象
                        Me.m_objIDmxzJbdm.Dispose()
                        Me.m_objIDmxzJbdm = Nothing
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
                m_objIDmxzJbdm = CType(objTemp, Xydc.Platform.BusinessFacade.IDmxzJbdm)
            Catch ex As Exception
                m_objIDmxzJbdm = Nothing
            End Try

            '必须有接口参数
            If m_objIDmxzJbdm Is Nothing Then
                '显示错误信息
                Me.panelError.Visible = True
                Me.panelMain.Visible = Not Me.panelError.Visible
                strErrMsg = "本模块必须提供输入接口参数！"
                GoTo errProc
            End If

            '获取局部接口参数
            Me.m_strSessionId_CodeData = Me.htxtLocalSessionId.Value

            With New Xydc.Platform.Common.Utilities.PulicParameters
                Me.m_intFixedColumns = .getObjectValue(Me.htxtCODEDATAFixed.Value, 0)
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
            '    If Not (Me.m_objDataSet_CodeData Is Nothing) Then
            '        '释放Session
            '        Session.Remove(Me.m_strSessionId_CodeData)
            '        '释放对象
            '        Me.m_objDataSet_CodeData.Dispose()
            '        Me.m_objDataSet_CodeData = Nothing
            '    End If
            'Catch ex As Exception
            'End Try
            Try

                If Me.m_strSessionId_CodeData.Trim <> "" Then
                    Dim objTempDataSet As System.Data.DataSet = Nothing
                    Try
                        objTempDataSet = CType(Session(Me.m_strSessionId_CodeData), Xydc.Platform.Common.Data.CustomerData)
                    Catch ex As Exception
                        objTempDataSet = Nothing
                    End Try
                    Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objTempDataSet)
                    Session.Remove(Me.m_strSessionId_CodeData)
                End If

            Catch ex As Exception
            End Try
        End Sub

        '----------------------------------------------------------------
        ' 获取模块搜索条件(直接对数据集进行过滤操作)
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

            getQueryString = False
            strQuery = ""

            Try
                '按代码搜索
                Dim strDM As String = "Convert(" + Me.m_objIDmxzJbdm.iCodeField + ", 'System.String')"
                If Me.txtSearch_DM.Text.Length > 0 Then Me.txtSearch_DM.Text = Me.txtSearch_DM.Text.Trim()
                If Me.txtSearch_DM.Text <> "" Then
                    If strQuery = "" Then
                        strQuery = strDM + " like '" + Me.txtSearch_DM.Text + "%'"
                    Else
                        strQuery = strQuery + " and " + strDM + " like '" + Me.txtSearch_DM.Text + "%'"
                    End If
                End If

                '按名称搜索
                Dim strMC As String = Me.m_objIDmxzJbdm.iNameField
                If Me.txtSearch_MC.Text.Length > 0 Then Me.txtSearch_MC.Text = Me.txtSearch_MC.Text.Trim()
                If Me.txtSearch_MC.Text <> "" Then
                    Me.txtSearch_MC.Text = objPulicParameters.getNewSearchString(Me.txtSearch_MC.Text)
                    If strQuery = "" Then
                        strQuery = strMC + " like '" + Me.txtSearch_MC.Text + "%'"
                    Else
                        strQuery = strQuery + " and " + strMC + " like '" + Me.txtSearch_MC.Text + "%'"
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
        ' 获取模块要显示的数据信息，并进行session缓存
        '     strErrMsg      ：返回错误信息
        '     blnGetFromDB   ：回发处理时是否要重新从数据库获取数据
        '     strSQL         ：获取数据用的SQL语句
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function getModuleData( _
            ByRef strErrMsg As String, _
            ByVal blnGetFromDB As Boolean, _
            ByVal strSQL As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objsystemCommon As New Xydc.Platform.BusinessFacade.systemCommon

            getModuleData = False

            Dim strGuid As String
            Try
                If Me.IsPostBack = False Then
                    '获取Session的Id
                    strGuid = objPulicParameters.getNewGuid()
                    If strGuid = "" Then
                        strErrMsg = "无法产生GUID！"
                        GoTo errProc
                    End If
                    '初次调用，生成数据
                    objsystemCommon.getDataSetBySQL(strErrMsg, MyBase.UserId, MyBase.UserPassword, strSQL, Me.m_objDataSet_CodeData)
                    If strErrMsg <> "" Then
                        GoTo errProc
                    End If
                    '缓存信息
                    Me.m_strSessionId_CodeData = strGuid
                    Session.Add(Me.m_strSessionId_CodeData, Me.m_objDataSet_CodeData)
                    Me.htxtLocalSessionId.Value = Me.m_strSessionId_CodeData
                Else
                    '直接引用数据
                    Me.m_objDataSet_CodeData = CType(Session.Item(Me.m_strSessionId_CodeData), System.Data.DataSet)
                    If blnGetFromDB = True Then
                        '备份Sort字符串以及RowFilter字符串
                        Dim strSort As String
                        strSort = Me.m_objDataSet_CodeData.Tables(0).DefaultView.Sort
                        Dim strFilter As String
                        strFilter = Me.m_objDataSet_CodeData.Tables(0).DefaultView.RowFilter

                        '释放资源
                        If Not (Me.m_objDataSet_CodeData Is Nothing) Then
                            Me.m_objDataSet_CodeData.Dispose()
                            Me.m_objDataSet_CodeData = Nothing
                        End If

                        '重新检索数据
                        objsystemCommon.getDataSetBySQL(strErrMsg, MyBase.UserId, MyBase.UserPassword, strSQL, Me.m_objDataSet_CodeData)
                        If strErrMsg <> "" Then
                            GoTo errProc
                        End If

                        '缓存信息
                        Session.Item(Me.m_strSessionId_CodeData) = Me.m_objDataSet_CodeData

                        '恢复Sort字符串以及RowFilter字符串
                        Me.m_objDataSet_CodeData.Tables(0).DefaultView.Sort = strSort
                        Me.m_objDataSet_CodeData.Tables(0).DefaultView.RowFilter = strFilter
                    End If
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.BusinessFacade.systemCommon.SafeRelease(objsystemCommon)

            getModuleData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.BusinessFacade.systemCommon.SafeRelease(objsystemCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据屏幕搜索条件搜索数据
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function searchModuleData( _
            ByRef strErrMsg As String) As Boolean

            searchModuleData = False

            Try
                '获取搜索字符串
                Dim strQuery As String
                If Me.getQueryString(strErrMsg, strQuery) = False Then
                    GoTo errProc
                End If

                '搜索数据
                If Me.getModuleData(strErrMsg, True, Me.m_objIDmxzJbdm.iRowSourceSQL) = False Then
                    GoTo errProc
                End If

                '设置新的搜索串
                Me.m_objDataSet_CodeData.Tables(0).DefaultView.RowFilter = strQuery

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
        '     blnOnlyRefresh ：近重新显示网格，不用初始化网格列
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showDataGridInfo( _
            ByRef strErrMsg As String, _
            ByVal blnOnlyRefresh As Boolean) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objButtonColumn As System.Web.UI.WebControls.ButtonColumn

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
                '因为是动态网格，所有不论是否回发都要设置
                If blnOnlyRefresh = False Then
                    '自动生成列
                    objDataGridProcess.doGenrateDataGridColumns(strErrMsg, Me.grdCodeData, Me.m_objDataSet_CodeData.Tables(0), objButtonColumn, "select")
                    If strErrMsg <> "" Then
                        GoTo errProc
                    End If

                    '设置列宽
                    objDataGridProcess.doSetColumnWidth(strErrMsg, Me.grdCodeData, Me.m_objIDmxzJbdm.iColWidth, 1)
                    If strErrMsg <> "" Then
                        GoTo errProc
                    End If

                    '修改网格的宽度
                    Dim intTotal As Integer
                    intTotal = objDataGridProcess.getDataGridWidthPixels(Me.grdCodeData)
                    If intTotal > Me.m_cintMaxWidthDataGridInDIV Then
                        Me.grdCodeData.Width = New System.Web.UI.WebControls.Unit(intTotal)
                    Else
                        Me.grdCodeData.Width = New System.Web.UI.WebControls.Unit("100%")
                    End If
                End If

                '在获取数据时已经恢复了RowFilter、Sort的现场
                '设置数据源
                If Me.m_objDataSet_CodeData Is Nothing Then
                    Me.grdCodeData.DataSource = Nothing
                Else
                    Me.grdCodeData.DataSource = Me.m_objDataSet_CodeData.Tables(0).DefaultView
                End If

                '调整网格参数
                With Me.m_objDataSet_CodeData.Tables(0)
                    If objDataGridProcess.onBeforeDataGridBind(strErrMsg, Me.grdCodeData, .DefaultView.Count) = False Then
                        GoTo errProc
                    End If
                End With

                '恢复列标题中的排序信息
                If intSortColumnIndex >= 0 Then
                    objDataGridProcess.doClearSortCharInDataGridHead(Me.grdCodeData)
                    With Me.grdCodeData.Columns(intSortColumnIndex)
                        .HeaderText = objDataGridProcess.getColumnSortHeadString(.HeaderText, objSortType)
                    End With
                End If

                '绑定数据
                Me.grdCodeData.DataBind()

                '----------------------------------------------------------------
                '因为这些信息是非绑定的，所以下面的操作必须等绑定完成后执行！！！
                '一旦在后续处理中执行了DataBind，则信息会丢失！！！
                '----------------------------------------------------------------
                '如果是单选，则禁止CheckBox(多重选择)
                If Me.m_objIDmxzJbdm.iMultiSelect = False Then
                    objDataGridProcess.doEnableDataGridCheckBox(strErrMsg, Me.grdCodeData, 0, Me.m_cstrCheckBoxIdInDataGrid, False)
                Else
                    objDataGridProcess.doEnableDataGridCheckBox(strErrMsg, Me.grdCodeData, 0, Me.m_cstrCheckBoxIdInDataGrid, True)
                End If
                '恢复网格中的CheckBox状态
                If objDataGridProcess.doRestoreDataGridCheckBoxStatus(strErrMsg, Me.grdCodeData, Request, 0, Me.m_cstrCheckBoxIdInDataGrid) = False Then
                    GoTo errProc
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
        ' 显示整个模块的信息
        '     strErrMsg      ：返回错误信息
        '     blnOnlyRefresh ：近重新显示网格，不用初始化网格列
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showModuleData( _
            ByRef strErrMsg As String, _
            ByVal blnOnlyRefresh As Boolean) As Boolean

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess

            showModuleData = False

            Try
                '显示网格信息
                If Me.showDataGridInfo(strErrMsg, blnOnlyRefresh) = False Then
                    GoTo errProc
                End If

                '显示与网格紧密相关的操作或信息提示
                With Me.m_objDataSet_CodeData.Tables(0).DefaultView
                    '显示网格位置信息
                    Me.lblGridLocInfo.Text = objDataGridProcess.getDataGridLocation(Me.grdCodeData, .Count)

                    '显示页面浏览功能
                    Me.lnkCZMoveFrst.Enabled = objDataGridProcess.canDoMoveFirstPage(Me.grdCodeData, .Count)
                    Me.lnkCZMoveLast.Enabled = objDataGridProcess.canDoMoveLastPage(Me.grdCodeData, .Count)
                    Me.lnkCZMovePrev.Enabled = objDataGridProcess.canDoMovePreviousPage(Me.grdCodeData, .Count)
                    Me.lnkCZMoveNext.Enabled = objDataGridProcess.canDoMoveNextPage(Me.grdCodeData, .Count)

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

            showModuleData = True
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
                    '设置初始显示的静态信息
                    Me.lblTitle.Text = Me.m_objIDmxzJbdm.iTitle
                    If Me.m_objIDmxzJbdm.iMultiSelect = True Then
                        Me.lblTitle.Text += "[多选]"
                    Else
                        Me.lblTitle.Text += "[单选]"
                    End If
                    Me.lblSearch_DM.Text = Me.m_objIDmxzJbdm.iCodeField
                    Me.lblSearch_MC.Text = Me.m_objIDmxzJbdm.iNameField
                    Me.txtNewDM.Text = Me.m_objIDmxzJbdm.iInitValue

                    '根据接口参数设置不受数据影响的操作的状态
                    Me.btnOKNull.Enabled = Me.m_objIDmxzJbdm.iAllowNull
                    Me.txtNewDM.Enabled = Me.m_objIDmxzJbdm.iAllowInput
                    Me.btnAddNew.Enabled = Me.txtNewDM.Enabled
                    Me.lnkCZSelectAll.Enabled = Me.m_objIDmxzJbdm.iMultiSelect
                    Me.lnkCZDeSelectAll.Enabled = Me.lnkCZSelectAll.Enabled

                    '显示Pannel(不论是否回调，始终显示panelMain)
                    Me.panelMain.Visible = True
                    Me.panelError.Visible = Not Me.panelMain.Visible

                    '执行键转译(不论是否是“回发”)
                    With New Xydc.Platform.web.ControlProcess
                        .doTranslateKey(Me.txtPageIndex)
                        .doTranslateKey(Me.txtPageSize)
                        .doTranslateKey(Me.txtSearch_DM)
                        .doTranslateKey(Me.txtSearch_MC)
                        .doTranslateKey(Me.txtNewDM)
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
            End If

            '因为是动态网格，所以不论是否回发都要重建网格的显示
            '获取数据
            If Me.getModuleData(strErrMsg, False, Me.m_objIDmxzJbdm.iRowSourceSQL) = False Then
                GoTo errProc
            End If
            '显示数据
            If Me.showModuleData(strErrMsg, False) = False Then
                GoTo errProc
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
        '实现对网格行、列的固定
        Sub grdCodeData_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles grdCodeData.ItemDataBound

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
                        e.Item.Cells(i).CssClass = Me.grdCodeData.ID + "Locked"
                    Next
                End If

            Catch ex As Exception
            End Try

        End Sub

        Private Sub grdCodeData_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdCodeData.SelectedIndexChanged

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '修改定位信息
                With Me.m_objDataSet_CodeData.Tables(0)
                    Me.lblGridLocInfo.Text = objDataGridProcess.getDataGridLocation(Me.grdCodeData, .DefaultView.Count)
                End With
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

        Private Sub grdCodeData_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles grdCodeData.SortCommand

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

                '获取原排序
                strOldCommand = Me.m_objDataSet_CodeData.Tables(0).DefaultView.Sort

                '获取要执行的排序命令
                objDataGridProcess.getColumnSortCommand(strErrMsg, strOldCommand, e.SortExpression, strFinalCommand, objenumSortType)
                If strErrMsg <> "" Then
                    GoTo errProc
                End If

                '执行排序
                Me.m_objDataSet_CodeData.Tables(0).DefaultView.Sort = strFinalCommand

                '获取排序列的列索引
                objDataGridItem = CType(e.CommandSource, System.Web.UI.WebControls.DataGridItem)
                strUniqueId = Request.Form("__EVENTTARGET")
                intColumnIndex = objDataGridProcess.getColumnIndexByUniqueIdInRow(objDataGridItem, strUniqueId)

                '保存排序信息
                Me.htxtSortColumnIndex.Value = intColumnIndex.ToString()
                Me.htxtSortType.Value = CType(objenumSortType, Integer).ToString()

                '重新显示数据
                If Me.showModuleData(strErrMsg, True) = False Then
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

        '----------------------------------------------------------------
        '模块一般操作处理器
        '----------------------------------------------------------------
        '设置网格页面显示的记录数
        Private Sub doSetPageSize(ByVal strControlId As String)

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            '获取新的页大小
            Dim intPageSize As Integer
            intPageSize = objPulicParameters.getObjectValue(Me.txtPageSize.Text, 100)
            If intPageSize <= 0 Then intPageSize = 100

            Try
                '设置新的页大小
                Me.grdCodeData.PageSize = intPageSize

                '刷新网格显示
                If Me.showModuleData(strErrMsg, True) = False Then
                    GoTo errProc
                End If

                '信息同步
                Me.txtPageSize.Text = (Me.grdCodeData.PageSize).ToString()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        '将网格移动到指定页面
        Private Sub doGotoPage(ByVal strControlId As String)

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
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
                '设置新的页
                Me.grdCodeData.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData(strErrMsg, True) = False Then
                    GoTo errProc
                End If

                '信息同步
                Me.txtPageIndex.Text = (Me.grdCodeData.CurrentPageIndex + 1).ToString()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        '选择全部的记录
        Private Sub doSelectAll(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                If objDataGridProcess.doCheckedDataGridCheckBox(strErrMsg, Me.grdCodeData, 0, Me.m_cstrCheckBoxIdInDataGrid, True) = False Then
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

        '不选全部记录
        Private Sub doDeSelectAll(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                If objDataGridProcess.doCheckedDataGridCheckBox(strErrMsg, Me.grdCodeData, 0, Me.m_cstrCheckBoxIdInDataGrid, False) = False Then
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

        '移动到第1页
        Private Sub doMoveFirst(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(0, Me.grdCodeData.PageCount)
                Me.grdCodeData.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData(strErrMsg, True) = False Then
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

        '移动到最后1页
        Private Sub doMoveLast(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(Me.grdCodeData.PageCount - 1, Me.grdCodeData.PageCount)
                Me.grdCodeData.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData(strErrMsg, True) = False Then
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

        '移动到下页
        Private Sub doMoveNext(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(Me.grdCodeData.CurrentPageIndex + 1, Me.grdCodeData.PageCount)
                Me.grdCodeData.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData(strErrMsg, True) = False Then
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

        '移动到上页
        Private Sub doMovePrevious(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(Me.grdCodeData.CurrentPageIndex - 1, Me.grdCodeData.PageCount)
                Me.grdCodeData.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData(strErrMsg, True) = False Then
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

        '搜索数据
        Private Sub doSearch(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '搜索数据
                If Me.searchModuleData(strErrMsg) = False Then
                    GoTo errProc
                End If

                '刷新网格显示
                If Me.showModuleData(strErrMsg, True) = False Then
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

        Private Sub lnkCZSetPageSize_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZSetPageSize.Click
            Me.doSetPageSize("lnkCZSetPageSize")
        End Sub

        Private Sub lnkCZGotoPage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZGotoPage.Click
            Me.doGotoPage("lnkCZGotoPage")
        End Sub

        Private Sub lnkCZSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZSelectAll.Click
            Me.doSelectAll("lnkCZSelectAll")
        End Sub

        Private Sub lnkCZDeSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZDeSelectAll.Click
            Me.doDeSelectAll("lnkCZDeSelectAll")
        End Sub

        Private Sub lnkCZMoveFrst_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZMoveFrst.Click
            Me.doMoveFirst("lnkCZMoveFrst")
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

        Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            Me.doSearch("btnSearch")
        End Sub

        '----------------------------------------------------------------
        '模块特殊操作处理器
        '----------------------------------------------------------------
        '处理“btnOK”按钮
        Private Sub doConfirm(ByVal strControlId As String)

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim objTempDataSet As System.Data.DataSet
            Dim strErrMsg As String

            '获取返回的数据信息
            Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate
            Dim strReturnCodeValue As String = ""
            Dim strReturnNameValue As String = ""
            Dim intRecPos As Integer
            Dim strValue As String
            Try
                If Me.m_objIDmxzJbdm.iMultiSelect = True Then
                    '多选：返回选择数据集
                    Dim objDataRow As System.Data.DataRow
                    Dim intRowCount As Integer
                    Dim blnSelect As Boolean
                    Dim i As Integer
                    intRowCount = Me.grdCodeData.Items.Count
                    blnSelect = False
                    For i = 0 To intRowCount - 1 Step 1
                        If objDataGridProcess.isDataGridItemChecked(Me.grdCodeData.Items(i), 0, Me.m_cstrCheckBoxIdInDataGrid) = True Then
                            '选择了
                            If blnSelect = False Then
                                blnSelect = True
                                '根据源数据集创建新数据集
                                objTempDataSet = Me.m_objDataSet_CodeData.Clone()
                                If objTempDataSet.Tables(0).Rows.Count > 0 Then
                                    objTempDataSet.Tables(0).Rows.Clear()
                                End If
                            End If

                            '复制数据
                            With Me.grdCodeData
                                intRecPos = objDataGridProcess.getRecordPosition(i, .CurrentPageIndex, .PageSize)
                            End With
                            Dim intColCount As Integer
                            Dim j As Integer
                            objDataRow = objTempDataSet.Tables(0).NewRow()
                            intColCount = Me.m_objDataSet_CodeData.Tables(0).Columns.Count
                            For j = 0 To intColCount - 1 Step 1
                                objDataRow.Item(j) = Me.m_objDataSet_CodeData.Tables(0).DefaultView.Item(intRecPos).Row.Item(j)
                            Next
                            objTempDataSet.Tables(0).Rows.Add(objDataRow)

                            With Me.m_objDataSet_CodeData.Tables(0).DefaultView.Item(intRecPos).Row
                                If strReturnCodeValue = "" Then
                                    strReturnCodeValue = objPulicParameters.getObjectValue(.Item(Me.m_objIDmxzJbdm.iCodeField), "")
                                Else
                                    strReturnCodeValue = strReturnCodeValue + strSep + objPulicParameters.getObjectValue(.Item(Me.m_objIDmxzJbdm.iCodeField), "")
                                End If

                                If strReturnNameValue = "" Then
                                    strReturnNameValue = objPulicParameters.getObjectValue(.Item(Me.m_objIDmxzJbdm.iNameField), "")
                                Else
                                    strReturnNameValue = strReturnNameValue + strSep + objPulicParameters.getObjectValue(.Item(Me.m_objIDmxzJbdm.iNameField), "")
                                End If
                            End With
                        End If
                    Next
                    If blnSelect = False Then
                        strErrMsg = "错误：没有打钩！"
                        GoTo errProc
                    End If

                    '返回objTempDataSet
                    With Me.m_objIDmxzJbdm
                        .oDataSet = objTempDataSet
                        .oCodeValue = strReturnCodeValue
                        .oNameValue = strReturnNameValue
                    End With
                Else
                    '单选：获取当前行数据
                    If Me.grdCodeData.SelectedIndex < 0 Then
                        strErrMsg = "错误：没有选择行！"
                        GoTo errProc
                    End If
                    With Me.grdCodeData
                        intRecPos = objDataGridProcess.getRecordPosition(.SelectedIndex, .CurrentPageIndex, .PageSize)
                    End With

                    '返回数据
                    With Me.m_objDataSet_CodeData.Tables(0).DefaultView.Item(intRecPos)
                        Me.m_objIDmxzJbdm.oCodeValue = objPulicParameters.getObjectValue(.Item(Me.m_objIDmxzJbdm.iCodeField), "")
                        Me.m_objIDmxzJbdm.oNameValue = objPulicParameters.getObjectValue(.Item(Me.m_objIDmxzJbdm.iNameField), "")
                    End With
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Try
                '设置返回参数
                Me.m_objIDmxzJbdm.oExitMode = True
                Me.m_objIDmxzJbdm.oSelectMode = Xydc.Platform.BusinessFacade.IDmxzJbdm.enumCodeInputType.ByDataGrid

                '释放模块资源
                Me.releaseModuleParameters()
                Me.releaseInterfaceParameters()

                '返回到调用模块，并附加返回参数
                '要返回的SessionId
                Dim strSessionId As String
                strSessionId = Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.ISessionId)
                'SessionId附加到返回的Url
                Dim strUrl As String
                strUrl = Me.m_objIDmxzJbdm.getReturnUrl(Server, Xydc.Platform.Common.Utilities.PulicParameters.OSessionId, strSessionId)
                '返回
                Response.Redirect(strUrl)
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objTempDataSet)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        '处理“btnOKNull”按钮
        Private Sub doConfirmNull(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            '获取返回的数据信息
            Try
                With Me.m_objIDmxzJbdm
                    .oCodeValue = ""
                    .oNameValue = ""
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Try
                '设置返回参数
                Me.m_objIDmxzJbdm.oExitMode = True
                Me.m_objIDmxzJbdm.oSelectMode = Xydc.Platform.BusinessFacade.IDmxzJbdm.enumCodeInputType.ByInput

                '释放模块资源
                Me.releaseModuleParameters()
                Me.releaseInterfaceParameters()

                '返回到调用模块，并附加返回参数
                '要返回的SessionId
                Dim strSessionId As String
                strSessionId = Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.ISessionId)
                'SessionId附加到返回的Url
                Dim strUrl As String
                strUrl = Me.m_objIDmxzJbdm.getReturnUrl(Server, Xydc.Platform.Common.Utilities.PulicParameters.OSessionId, strSessionId)
                '返回
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

        '处理“btnCancel”按钮
        Private Sub doCancel(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '设置返回参数
                Me.m_objIDmxzJbdm.oExitMode = False

                '释放模块资源
                Me.releaseModuleParameters()
                Me.releaseInterfaceParameters()

                '返回到调用模块，并附加返回参数
                '要返回的SessionId
                Dim strSessionId As String
                strSessionId = Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.ISessionId)
                'SessionId附加到返回的Url
                Dim strUrl As String
                strUrl = Me.m_objIDmxzJbdm.getReturnUrl(Server, Xydc.Platform.Common.Utilities.PulicParameters.OSessionId, strSessionId)
                '返回
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

        Private Sub doAddNewDM(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '获取返回的数据信息
                If Me.txtNewDM.Text.Length > 0 Then Me.txtNewDM.Text = Me.txtNewDM.Text.Trim()
                If Me.txtNewDM.Text = "" Then
                    strErrMsg = "错误：没有输入代码值！"
                    GoTo errProc
                End If

                '设置返回参数
                Me.m_objIDmxzJbdm.oExitMode = True
                Me.m_objIDmxzJbdm.oSelectMode = Xydc.Platform.BusinessFacade.IDmxzJbdm.enumCodeInputType.ByInput
                Me.m_objIDmxzJbdm.oCodeValue = Me.txtNewDM.Text
                Me.m_objIDmxzJbdm.oNameValue = Me.txtNewDM.Text

                '释放模块资源
                Me.releaseModuleParameters()
                Me.releaseInterfaceParameters()

                '返回到调用模块，并附加返回参数
                '要返回的SessionId
                Dim strSessionId As String
                strSessionId = Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.ISessionId)
                'SessionId附加到返回的Url
                Dim strUrl As String
                strUrl = Me.m_objIDmxzJbdm.getReturnUrl(Server, Xydc.Platform.Common.Utilities.PulicParameters.OSessionId, strSessionId)
                '返回
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

        Private Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.Click
            Me.doConfirm("btnOK")
        End Sub

        Private Sub btnOKNull_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOKNull.Click
            Me.doConfirmNull("btnOKNull")
        End Sub

        Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
            Me.doCancel("btnCancel")
        End Sub

        Private Sub btnAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
            Me.doAddNewDM("btnAddNew")
        End Sub


    End Class
End Namespace