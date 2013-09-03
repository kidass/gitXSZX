Imports System.Web.Security

Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.web
    ' 类名    ：dmxz_gzgw
    ' 
    ' 调用性质：
    '     可被其他模块调用，本身不调用其他模块
    '
    ' 功能描述： 
    '   　职务选择处理模块
    '
    ' 接口参数：
    '     参见IDmxzGzgw接口类描述
    '----------------------------------------------------------------

    Partial Public Class dmxz_gzgw
        Inherits Xydc.Platform.web.PageBase
        '----------------------------------------------------------------
        '模块私用参数
        '----------------------------------------------------------------
        '本模块相对image的相对路径
        Private m_cstrRelativePathToImage As String = "../../"

        '----------------------------------------------------------------
        '与数据网格grdZWLIST相关的参数
        '----------------------------------------------------------------
        '网格中模板列中的控件ID
        Private Const m_cstrCheckBoxIdInDataGrid_ZWLIST As String = "chkZWLIST"
        '包含网格的DIV对象ID
        Private Const m_cstrDataGridInDIV_ZWLIST As String = "divZWLIST"
        '网格要锁定的列数
        Private m_intFixedColumns_ZWLIST As Integer

        '----------------------------------------------------------------
        '与数据网格grdZWSEL相关的参数
        '----------------------------------------------------------------
        '网格中模板列中的控件ID
        Private Const m_cstrCheckBoxIdInDataGrid_ZWSEL As String = "chkZWSEL"
        '包含网格的DIV对象ID
        Private Const m_cstrDataGridInDIV_ZWSEL As String = "divZWSEL"
        '网格要锁定的列数
        Private m_intFixedColumns_ZWSEL As Integer

        '----------------------------------------------------------------
        '模块接口参数
        '----------------------------------------------------------------
        Private m_objIDmxzGzgw As Xydc.Platform.BusinessFacade.IDmxzGzgw

        '----------------------------------------------------------------
        '要访问的数据
        '----------------------------------------------------------------
        Private m_objDataSet_ZWLIST As Xydc.Platform.Common.Data.GongzuogangweiData
        Private m_strQuery_ZWLIST As String '记录m_objDataSet_ZWLIST搜索串
        Private m_objDataSet_ZWSEL As Xydc.Platform.Common.Data.GongzuogangweiData
        Private m_strSessionId_ZWSEL As String '缓存m_objDataSet_ZWSEL的SessionId

        '----------------------------------------------------------------
        '其他参数
        '----------------------------------------------------------------











        '----------------------------------------------------------------
        ' 释放接口参数
        '----------------------------------------------------------------
        Private Sub releaseInterfaceParameters()

            Try
                If Not (Me.m_objIDmxzGzgw Is Nothing) Then
                    If Me.m_objIDmxzGzgw.iInterfaceType = Xydc.Platform.BusinessFacade.ICallInterface.enumInterfaceType.InputOnly Then
                        '释放Session
                        Session.Remove(Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.ISessionId))
                        '释放对象
                        Me.m_objIDmxzGzgw.Dispose()
                        Me.m_objIDmxzGzgw = Nothing
                    End If
                End If
            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' 获取接口参数(没有接口参数则显示错误信息页面)
        '----------------------------------------------------------------
        Private Function getInterfaceParameters(ByRef strErrMsg As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            getInterfaceParameters = False

            '从QueryString中解析接口参数(不论是否回发)
            Dim objTemp As Object
            Try
                objTemp = Session.Item(Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.ISessionId))
                m_objIDmxzGzgw = CType(objTemp, Xydc.Platform.BusinessFacade.IDmxzGzgw)
            Catch ex As Exception
                m_objIDmxzGzgw = Nothing
            End Try

            '必须有接口参数
            If m_objIDmxzGzgw Is Nothing Then
                '显示错误信息
                strErrMsg = "本模块必须提供输入接口参数！"
                Me.lblMessage.Text = strErrMsg
                Me.panelError.Visible = True
                Me.panelMain.Visible = Not Me.panelError.Visible
                GoTo errProc
            End If

            '获取局部接口参数
            Me.m_strSessionId_ZWSEL = Me.htxtSessionIdZWSEL.Value
            Me.m_intFixedColumns_ZWLIST = objPulicParameters.getObjectValue(Me.htxtZWLISTFixed.Value, 0)
            Me.m_intFixedColumns_ZWSEL = objPulicParameters.getObjectValue(Me.htxtZWSELFixed.Value, 0)
            Me.m_strQuery_ZWLIST = objPulicParameters.getObjectValue(Me.htxtZWLISTQuery.Value, "")

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

            'Try
            '    If Not (Me.m_objDataSet_ZWSEL Is Nothing) Then
            '        '释放Session
            '        Session.Remove(Me.m_strSessionId_ZWSEL)
            '        '释放对象
            '        '对象用于返回，不能释放
            '    End If
            'Catch ex As Exception
            'End Try
            Try

                If Me.m_strSessionId_ZWSEL.Trim <> "" Then
                    Dim objTempDataSet As Xydc.Platform.Common.Data.GongzuogangweiData = Nothing
                    Try
                        objTempDataSet = CType(Session(Me.m_strSessionId_ZWSEL), Xydc.Platform.Common.Data.GongzuogangweiData)
                    Catch ex As Exception
                        objTempDataSet = Nothing
                    End Try
                    Xydc.Platform.Common.Data.GongzuogangweiData.SafeRelease(objTempDataSet)
                    Session.Remove(Me.m_strSessionId_ZWSEL)
                End If

            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' 获取grdZWLIST的搜索条件(默认表前缀a.)
        '     strErrMsg      ：返回错误信息
        '     strQuery       ：返回的搜索条件
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function getQueryString_ZWLIST( _
            ByRef strErrMsg As String, _
            ByRef strQuery As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            getQueryString_ZWLIST = False
            strQuery = ""

            Try
                '按职务搜索
                Dim strZWMC As String = "a." + Xydc.Platform.Common.Data.GongzuogangweiData.FIELD_GG_B_GONGZUOGANGWEI_GWMC
                If Me.txtSearchZWMC.Text.Length > 0 Then Me.txtSearchZWMC.Text = Me.txtSearchZWMC.Text.Trim()
                If Me.txtSearchZWMC.Text <> "" Then
                    Me.txtSearchZWMC.Text = objPulicParameters.getNewSearchString(Me.txtSearchZWMC.Text)
                    If strQuery = "" Then
                        strQuery = strZWMC + " like '" + Me.txtSearchZWMC.Text + "%'"
                    Else
                        strQuery = strQuery + " and " + strZWMC + " like '" + Me.txtSearchZWMC.Text + "%'"
                    End If
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            getQueryString_ZWLIST = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取grdZWLIST要显示的数据信息
        '     strErrMsg      ：返回错误信息
        '     strWhere       ：搜索条件
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function getModuleData_ZWLIST( _
            ByRef strErrMsg As String, _
            ByVal strWhere As String) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.GongzuogangweiData.TABLE_GG_B_GONGZUOGANGWEI

            Dim objsystemGongzuogangwei As New Xydc.Platform.BusinessFacade.systemGongzuogangwei

            getModuleData_ZWLIST = False

            Try
                '备份Sort字符串
                Dim strSort As String
                strSort = Me.htxtZWLISTSort.Value
                If strSort.Length > 0 Then strSort = strSort.Trim

                '释放资源
                If Not (Me.m_objDataSet_ZWLIST Is Nothing) Then
                    Me.m_objDataSet_ZWLIST.Dispose()
                    Me.m_objDataSet_ZWLIST = Nothing
                End If

                '重新检索数据
                If objsystemGongzuogangwei.getGangweiData(strErrMsg, MyBase.UserId, MyBase.UserPassword, strWhere, Me.m_objDataSet_ZWLIST) = False Then
                    GoTo errProc
                End If

                '恢复Sort字符串
                With Me.m_objDataSet_ZWLIST.Tables(strTable)
                    .DefaultView.Sort = strSort
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.BusinessFacade.systemGongzuogangwei.SafeRelease(objsystemGongzuogangwei)

            getModuleData_ZWLIST = True
            Exit Function

errProc:
            Xydc.Platform.BusinessFacade.systemGongzuogangwei.SafeRelease(objsystemGongzuogangwei)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取grdZWSEL要显示的数据信息，并进行session缓存
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function getModuleData_ZWSEL(ByRef strErrMsg As String) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.GongzuogangweiData.TABLE_GG_B_VT_SELGONGZUOGANGWEI

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            getModuleData_ZWSEL = False

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
                    Me.m_objDataSet_ZWSEL = New Xydc.Platform.Common.Data.GongzuogangweiData(Xydc.Platform.Common.Data.GongzuogangweiData.enumTableType.GG_B_VT_SELGONGZUOGANGWEI)

                    '根据初始值设置信息
                    Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate
                    Dim strZWLIST As String = Me.m_objIDmxzGzgw.iZWLIST
                    If strZWLIST <> "" Then
                        Dim objDataRow As System.Data.DataRow
                        Dim strValue() As String
                        Dim intCount As Integer
                        Dim i As Integer
                        strValue = strZWLIST.Split(strSep.ToCharArray())
                        intCount = strValue.Length
                        For i = 0 To intCount - 1 Step 1
                            With Me.m_objDataSet_ZWSEL.Tables(strTable)
                                objDataRow = .NewRow()
                                objDataRow.Item(Xydc.Platform.Common.Data.GongzuogangweiData.FIELD_GG_B_VT_SELGONGZUOGANGWEI_GWMC) = strValue(i)
                                .Rows.Add(objDataRow)
                            End With
                        Next
                    End If

                    '缓存信息
                    Me.m_strSessionId_ZWSEL = strGuid
                    Session.Add(Me.m_strSessionId_ZWSEL, Me.m_objDataSet_ZWSEL)
                    Me.htxtSessionIdZWSEL.Value = Me.m_strSessionId_ZWSEL
                Else
                    '直接引用数据
                    Me.m_objDataSet_ZWSEL = CType(Session.Item(Me.m_strSessionId_ZWSEL), Xydc.Platform.Common.Data.GongzuogangweiData)
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            getModuleData_ZWSEL = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据屏幕搜索条件搜索grdZWLIST数据
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function searchModuleData_ZWLIST(ByRef strErrMsg As String) As Boolean

            searchModuleData_ZWLIST = False

            Try
                '获取搜索字符串
                Dim strQuery As String
                If Me.getQueryString_ZWLIST(strErrMsg, strQuery) = False Then
                    GoTo errProc
                End If

                '搜索数据
                If Me.getModuleData_ZWLIST(strErrMsg, strQuery) = False Then
                    GoTo errProc
                End If

                '记录搜索字符串
                Me.m_strQuery_ZWLIST = strQuery
                Me.htxtZWLISTQuery.Value = Me.m_strQuery_ZWLIST

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            searchModuleData_ZWLIST = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示grdZWLIST的数据
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showDataGridInfo_ZWLIST(ByRef strErrMsg As String) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.GongzuogangweiData.TABLE_GG_B_GONGZUOGANGWEI

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess

            showDataGridInfo_ZWLIST = False

            '获取系统保存的网格排序数据
            Dim intSortColumnIndex As Integer
            intSortColumnIndex = objPulicParameters.getObjectValue(Me.htxtZWLISTSortColumnIndex.Value, -1)
            Dim objSortType As Xydc.Platform.Common.Utilities.PulicParameters.enumSortType
            Try
                objSortType = CType(Me.htxtZWLISTSortType.Value, Xydc.Platform.Common.Utilities.PulicParameters.enumSortType)
            Catch ex As Exception
                objSortType = Xydc.Platform.Common.Utilities.PulicParameters.enumSortType.None
            End Try

            '网格显示处理
            Try
                '在获取数据时已经恢复了RowFilter、Sort的现场
                '设置数据源
                If Me.m_objDataSet_ZWLIST Is Nothing Then
                    Me.grdZWLIST.DataSource = Nothing
                Else
                    With Me.m_objDataSet_ZWLIST.Tables(strTable)
                        Me.grdZWLIST.DataSource = .DefaultView
                    End With
                End If

                '调整网格参数
                With Me.m_objDataSet_ZWLIST.Tables(strTable)
                    If objDataGridProcess.onBeforeDataGridBind(strErrMsg, Me.grdZWLIST, .DefaultView.Count) = False Then
                        GoTo errProc
                    End If
                End With

                '恢复列标题中的排序信息
                If intSortColumnIndex >= 0 Then
                    objDataGridProcess.doClearSortCharInDataGridHead(Me.grdZWLIST)
                    With Me.grdZWLIST.Columns(intSortColumnIndex)
                        .HeaderText = objDataGridProcess.getColumnSortHeadString(.HeaderText, objSortType)
                    End With
                End If

                '绑定数据
                Me.grdZWLIST.DataBind()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)

            showDataGridInfo_ZWLIST = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示grdZWSEL的数据
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showDataGridInfo_ZWSEL(ByRef strErrMsg As String) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.GongzuogangweiData.TABLE_GG_B_VT_SELGONGZUOGANGWEI

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess

            showDataGridInfo_ZWSEL = False

            '获取系统保存的网格排序数据
            Dim intSortColumnIndex As Integer
            intSortColumnIndex = objPulicParameters.getObjectValue(Me.htxtZWSELSortColumnIndex.Value, -1)
            Dim objSortType As Xydc.Platform.Common.Utilities.PulicParameters.enumSortType
            Try
                objSortType = CType(Me.htxtZWSELSortType.Value, Xydc.Platform.Common.Utilities.PulicParameters.enumSortType)
            Catch ex As Exception
                objSortType = Xydc.Platform.Common.Utilities.PulicParameters.enumSortType.None
            End Try

            '网格显示处理
            Try
                '在获取数据时已经恢复了RowFilter、Sort的现场
                '设置数据源
                If Me.m_objDataSet_ZWSEL Is Nothing Then
                    Me.grdZWSEL.DataSource = Nothing
                Else
                    With Me.m_objDataSet_ZWSEL.Tables(strTable)
                        Me.grdZWSEL.DataSource = .DefaultView
                    End With
                End If

                '调整网格参数
                With Me.m_objDataSet_ZWSEL.Tables(strTable)
                    If objDataGridProcess.onBeforeDataGridBind(strErrMsg, Me.grdZWSEL, .DefaultView.Count) = False Then
                        GoTo errProc
                    End If
                End With

                '恢复列标题中的排序信息
                If intSortColumnIndex >= 0 Then
                    objDataGridProcess.doClearSortCharInDataGridHead(Me.grdZWSEL)
                    With Me.grdZWSEL.Columns(intSortColumnIndex)
                        .HeaderText = objDataGridProcess.getColumnSortHeadString(.HeaderText, objSortType)
                    End With
                End If

                '绑定数据
                Me.grdZWSEL.DataBind()

                '----------------------------------------------------------------
                '因为这些信息是非绑定的，所以下面的操作必须等绑定完成后执行！！！
                '一旦在后续处理中执行了DataBind，则信息会丢失！！！
                '----------------------------------------------------------------
                '恢复网格中的CheckBox状态
                'If objDataGridProcess.doRestoreDataGridCheckBoxStatus(strErrMsg, Me.grdZWSEL, Request, 0, Me.m_cstrCheckBoxIdInDataGrid_ZWSEL) = False Then
                '    GoTo errProc
                'End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)

            showDataGridInfo_ZWSEL = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示grdZWLIST及相关信息
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showModuleData_ZWLIST(ByRef strErrMsg As String) As Boolean

            showModuleData_ZWLIST = False

            Try
                '显示网格信息
                If Me.showDataGridInfo_ZWLIST(strErrMsg) = False Then
                    GoTo errProc
                End If

                '显示与网格紧密相关的操作或信息提示
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            showModuleData_ZWLIST = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示grdZWSEL及相关信息
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showModuleData_ZWSEL(ByRef strErrMsg As String) As Boolean

            showModuleData_ZWSEL = False

            Try
                '显示网格信息
                If Me.showDataGridInfo_ZWSEL(strErrMsg) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            showModuleData_ZWSEL = True
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
                Try
                    '根据接口参数设置
                    If Me.m_objIDmxzGzgw.iMultiSelect = True Then
                        Me.lblTitle.Text += "[多选]"
                    Else
                        Me.lblTitle.Text += "[单选]"
                    End If
                Catch ex As Exception
                End Try

                '显示Pannel
                Me.panelMain.Visible = True
                Me.panelError.Visible = Not Me.panelMain.Visible

                '执行键转译(不论是否是“回发”)
                Try
                    objControlProcess.doTranslateKey(Me.txtSearchZWMC)
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
            End If

            If Me.IsPostBack = False Then
                '获取数据
                If Me.getModuleData_ZWLIST(strErrMsg, "") = False Then
                    GoTo errProc
                End If
                '显示数据
                If Me.showModuleData_ZWLIST(strErrMsg) = False Then
                    GoTo errProc
                End If

                '获取数据
                If Me.getModuleData_ZWSEL(strErrMsg) = False Then
                    GoTo errProc
                End If
                '显示数据
                If Me.showModuleData_ZWSEL(strErrMsg) = False Then
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
        '实现对grdZWLIST网格行、列的固定
        Sub grdZWLIST_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles grdZWLIST.ItemDataBound

            Dim intCells As Integer
            Dim i As Integer

            Try
                If e.Item.ItemIndex < 0 Then
                    '标题行,输出标题锁定一般属性
                    intCells = e.Item.Cells.Count
                    For i = 0 To intCells - 1 Step 1
                        e.Item.Cells(i).Attributes.CssStyle.Add("top", "expression(" + Me.m_cstrDataGridInDIV_ZWLIST + ".scrollTop)")
                    Next
                End If
                If Me.m_intFixedColumns_ZWLIST > 0 Then
                    '锁定列
                    For i = 0 To Me.m_intFixedColumns_ZWLIST - 1 Step 1
                        e.Item.Cells(i).CssClass = Me.grdZWLIST.ID + "Locked"
                    Next
                End If
            Catch ex As Exception
            End Try

        End Sub

        '实现对grdZWSEL网格行、列的固定
        Sub grdZWSEL_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles grdZWSEL.ItemDataBound

            Dim intCells As Integer
            Dim i As Integer

            Try
                If e.Item.ItemIndex < 0 Then
                    '标题行,输出标题锁定一般属性
                    intCells = e.Item.Cells.Count
                    For i = 0 To intCells - 1 Step 1
                        e.Item.Cells(i).Attributes.CssStyle.Add("top", "expression(" + Me.m_cstrDataGridInDIV_ZWSEL + ".scrollTop)")
                    Next
                End If
                If Me.m_intFixedColumns_ZWSEL > 0 Then
                    '锁定列
                    For i = 0 To Me.m_intFixedColumns_ZWSEL - 1 Step 1
                        e.Item.Cells(i).CssClass = Me.grdZWSEL.ID + "Locked"
                    Next
                End If
            Catch ex As Exception
            End Try

        End Sub

        Private Sub grdZWLIST_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdZWLIST.SelectedIndexChanged

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

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

        Private Sub grdZWLIST_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles grdZWLIST.SortCommand

            Dim strTable As String = Xydc.Platform.Common.Data.GongzuogangweiData.TABLE_GG_B_GONGZUOGANGWEI

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
                If Me.getModuleData_ZWLIST(strErrMsg, "") = False Then
                    GoTo errProc
                End If

                '获取原排序命令
                strOldCommand = Me.m_objDataSet_ZWLIST.Tables(strTable).DefaultView.Sort

                '获取要执行的排序命令
                objDataGridProcess.getColumnSortCommand(strErrMsg, strOldCommand, e.SortExpression, strFinalCommand, objenumSortType)
                If strErrMsg <> "" Then
                    GoTo errProc
                End If

                '执行排序
                Me.m_objDataSet_ZWLIST.Tables(strTable).DefaultView.Sort = strFinalCommand

                '获取排序列的列索引
                objDataGridItem = CType(e.CommandSource, System.Web.UI.WebControls.DataGridItem)
                strUniqueId = Request.Form("__EVENTTARGET")
                intColumnIndex = objDataGridProcess.getColumnIndexByUniqueIdInRow(objDataGridItem, strUniqueId)

                '保存排序信息
                Me.htxtZWLISTSortColumnIndex.Value = intColumnIndex.ToString()
                Me.htxtZWLISTSortType.Value = CType(objenumSortType, Integer).ToString()
                Me.htxtZWLISTSort.Value = strFinalCommand

                '重新显示数据
                If Me.showModuleData_ZWLIST(strErrMsg) = False Then
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

        Private Sub grdZWSEL_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles grdZWSEL.SortCommand

            Dim strTable As String = Xydc.Platform.Common.Data.GongzuogangweiData.TABLE_GG_B_VT_SELGONGZUOGANGWEI

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
                If Me.getModuleData_ZWSEL(strErrMsg) = False Then
                    GoTo errProc
                End If

                '获取原排序命令
                strOldCommand = Me.m_objDataSet_ZWSEL.Tables(strTable).DefaultView.Sort

                '获取要执行的排序命令
                objDataGridProcess.getColumnSortCommand(strErrMsg, strOldCommand, e.SortExpression, strFinalCommand, objenumSortType)
                If strErrMsg <> "" Then
                    GoTo errProc
                End If

                '执行排序
                Me.m_objDataSet_ZWSEL.Tables(strTable).DefaultView.Sort = strFinalCommand

                '获取排序列的列索引
                objDataGridItem = CType(e.CommandSource, System.Web.UI.WebControls.DataGridItem)
                strUniqueId = Request.Form("__EVENTTARGET")
                intColumnIndex = objDataGridProcess.getColumnIndexByUniqueIdInRow(objDataGridItem, strUniqueId)

                '保存排序信息
                Me.htxtZWSELSortColumnIndex.Value = intColumnIndex.ToString()
                Me.htxtZWSELSortType.Value = CType(objenumSortType, Integer).ToString()

                '重新显示数据
                If Me.showModuleData_ZWSEL(strErrMsg) = False Then
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
                If Me.searchModuleData_ZWLIST(strErrMsg) = False Then
                    GoTo errProc
                End If

                '刷新网格显示
                If Me.showModuleData_ZWLIST(strErrMsg) = False Then
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

        Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            Me.doSearch("btnSearch")
        End Sub




        '----------------------------------------------------------------
        '模块特殊操作处理器
        '----------------------------------------------------------------
        Private Sub doCancel(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '设置返回参数
                Me.m_objIDmxzGzgw.oExitMode = False

                '释放模块资源
                Me.releaseModuleParameters()
                Me.releaseInterfaceParameters()

                '返回到调用模块，并附加返回参数
                '要返回的SessionId
                Dim strSessionId As String
                strSessionId = Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.ISessionId)
                'SessionId附加到返回的Url
                Dim strUrl As String
                strUrl = Me.m_objIDmxzGzgw.getReturnUrl(Server, Xydc.Platform.Common.Utilities.PulicParameters.OSessionId, strSessionId)
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

        Private Sub doConfirm(ByVal strControlId As String)

            Dim strTable As String = Xydc.Platform.Common.Data.GongzuogangweiData.TABLE_GG_B_VT_SELGONGZUOGANGWEI
            Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                Dim strReturnValue As String = ""

                '获取选择数据
                If Me.getModuleData_ZWSEL(strErrMsg) = False Then
                    GoTo errProc
                End If

                '检查选择数据
                With Me.m_objDataSet_ZWSEL.Tables(strTable)
                    If .Rows.Count < 1 And Me.m_objIDmxzGzgw.iAllowNull = False Then
                        strErrMsg = "错误：没有选择任何内容！"
                        GoTo errProc
                    End If
                    If Me.m_objIDmxzGzgw.iMultiSelect = False Then
                        If .Rows.Count > 1 Then
                            strErrMsg = "错误：只允许选择1条！"
                            GoTo errProc
                        End If
                    End If
                End With

                With Me.m_objDataSet_ZWSEL.Tables(strTable)
                    If .Rows.Count < 1 Then
                        '设置返回值
                        Me.m_objIDmxzGzgw.oZWLIST = ""
                    Else
                        '获取返回参数
                        Dim strValue As String
                        Dim intCount As Integer
                        Dim i As Integer
                        With Me.m_objDataSet_ZWSEL.Tables(strTable)
                            intCount = .Rows.Count
                            For i = 0 To intCount - 1 Step 1
                                strValue = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.GongzuogangweiData.FIELD_GG_B_VT_SELGONGZUOGANGWEI_GWMC), "")
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
                        With Me.m_objDataSet_ZWSEL.Tables(strTable)
                            .DefaultView.RowFilter = ""
                        End With

                        '设置返回值
                        Me.m_objIDmxzGzgw.oZWLIST = strReturnValue
                        Me.m_objIDmxzGzgw.oDataSet = Me.m_objDataSet_ZWSEL


                        If Me.m_strSessionId_ZWSEL.Trim <> "" Then
                            Try
                                Session.Remove(Me.m_strSessionId_ZWSEL)
                            Catch ex As Exception
                            End Try
                            Me.m_strSessionId_ZWSEL = ""
                        End If


                    End If
                End With

                '设置返回参数
                Me.m_objIDmxzGzgw.oExitMode = True

                '释放模块资源
                Me.releaseModuleParameters()
                Me.releaseInterfaceParameters()

                '返回到调用模块，并附加返回参数
                '要返回的SessionId
                Dim strSessionId As String
                strSessionId = Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.ISessionId)
                'SessionId附加到返回的Url
                Dim strUrl As String
                strUrl = Me.m_objIDmxzGzgw.getReturnUrl(Server, Xydc.Platform.Common.Utilities.PulicParameters.OSessionId, strSessionId)
                '返回
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

        Private Sub doSelectOne(ByVal strControlId As String)

            Dim strTable As String = Xydc.Platform.Common.Data.GongzuogangweiData.TABLE_GG_B_VT_SELGONGZUOGANGWEI
            Dim objsystemCommon As New Xydc.Platform.BusinessFacade.systemCommon
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '检查
                Dim intColIndex As Integer
                Dim strZW As String
                If Me.grdZWLIST.SelectedIndex < 0 Then
                    strErrMsg = "错误：没有选择[职务]！"
                    GoTo errProc
                End If
                intColIndex = objDataGridProcess.getDataGridColumnIndex(Me.grdZWLIST, Xydc.Platform.Common.Data.GongzuogangweiData.FIELD_GG_B_GONGZUOGANGWEI_GWMC)
                strZW = objDataGridProcess.getDataGridCellValue(Me.grdZWLIST.Items(Me.grdZWLIST.SelectedIndex), intColIndex)

                '获取数据
                If Me.getModuleData_ZWSEL(strErrMsg) = False Then
                    GoTo errProc
                End If

                '是否存在？
                Dim blnDo As Boolean
                If objsystemCommon.doFindInDataTable(strErrMsg, _
                    Me.m_objDataSet_ZWSEL.Tables(strTable), _
                    Xydc.Platform.Common.Data.GongzuogangweiData.FIELD_GG_B_VT_SELGONGZUOGANGWEI_GWMC, _
                    strZW, blnDo) = False Then
                    GoTo errProc
                End If
                If blnDo = True Then
                    Exit Try
                End If

                '加入选择
                With Me.m_objDataSet_ZWSEL.Tables(strTable)
                    Dim objDataRow As System.Data.DataRow
                    objDataRow = .NewRow()
                    objDataRow.Item(Xydc.Platform.Common.Data.GongzuogangweiData.FIELD_GG_B_VT_SELGONGZUOGANGWEI_GWMC) = strZW
                    .Rows.Add(objDataRow)
                End With

                '刷新显示
                If Me.showModuleData_ZWSEL(strErrMsg) = False Then
                    GoTo errProc
                End If

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

        Private Sub doDeleteOne(ByVal strControlId As String)

            Dim strTable As String = Xydc.Platform.Common.Data.GongzuogangweiData.TABLE_GG_B_VT_SELGONGZUOGANGWEI

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '检查
                Dim intRowIndex As Integer
                If Me.grdZWSEL.SelectedIndex < 0 Then
                    strErrMsg = "错误：没有选择[职务]！"
                    GoTo errProc
                End If
                intRowIndex = Me.grdZWSEL.SelectedIndex

                '获取数据
                If Me.getModuleData_ZWSEL(strErrMsg) = False Then
                    GoTo errProc
                End If

                '删除
                With Me.m_objDataSet_ZWSEL.Tables(strTable)
                    .DefaultView.Delete(intRowIndex)
                End With

                '刷新显示
                If Me.showModuleData_ZWSEL(strErrMsg) = False Then
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

        Private Sub doSelectAll(ByVal strControlId As String)

            Dim strTable As String = Xydc.Platform.Common.Data.GongzuogangweiData.TABLE_GG_B_VT_SELGONGZUOGANGWEI

            Dim objsystemCommon As New Xydc.Platform.BusinessFacade.systemCommon
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '检查
                Dim intColIndex As Integer
                Dim strZW As String
                If Me.grdZWLIST.Items.Count < 1 Then
                    strErrMsg = "错误：没有[职务]！"
                    GoTo errProc
                End If
                intColIndex = objDataGridProcess.getDataGridColumnIndex(Me.grdZWLIST, Xydc.Platform.Common.Data.GongzuogangweiData.FIELD_GG_B_VT_SELGONGZUOGANGWEI_GWMC)

                '获取数据
                If Me.getModuleData_ZWSEL(strErrMsg) = False Then
                    GoTo errProc
                End If

                '逐个加入
                Dim objDataRow As System.Data.DataRow
                Dim intCount As Integer
                Dim blnDo As Boolean
                Dim i As Integer
                intCount = Me.grdZWLIST.Items.Count
                For i = 0 To intCount - 1 Step 1
                    strZW = objDataGridProcess.getDataGridCellValue(Me.grdZWLIST.Items(i), intColIndex)

                    '是否存在？
                    If objsystemCommon.doFindInDataTable(strErrMsg, _
                        Me.m_objDataSet_ZWSEL.Tables(strTable), _
                        Xydc.Platform.Common.Data.GongzuogangweiData.FIELD_GG_B_VT_SELGONGZUOGANGWEI_GWMC, _
                        strZW, blnDo) = False Then
                        GoTo errProc
                    End If
                    If blnDo = False Then
                        '加入选择
                        With Me.m_objDataSet_ZWSEL.Tables(strTable)
                            objDataRow = .NewRow()
                            objDataRow.Item(Xydc.Platform.Common.Data.GongzuogangweiData.FIELD_GG_B_VT_SELGONGZUOGANGWEI_GWMC) = strZW
                            .Rows.Add(objDataRow)
                        End With
                    End If
                Next

                '刷新显示
                If Me.showModuleData_ZWSEL(strErrMsg) = False Then
                    GoTo errProc
                End If

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

        Private Sub doDeleteAll(ByVal strControlId As String)

            Dim strTable As String = Xydc.Platform.Common.Data.GongzuogangweiData.TABLE_GG_B_VT_SELGONGZUOGANGWEI

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '检查
                Dim intRowIndex As Integer
                If Me.grdZWSEL.Items.Count < 1 Then
                    strErrMsg = "错误：没有[职务]！"
                    GoTo errProc
                End If

                '获取数据
                If Me.getModuleData_ZWSEL(strErrMsg) = False Then
                    GoTo errProc
                End If

                '逐个删除
                Dim intCount As Integer
                Dim i As Integer
                intCount = Me.grdZWSEL.Items.Count
                For i = intCount - 1 To 0 Step -1
                    intRowIndex = i

                    '删除
                    With Me.m_objDataSet_ZWSEL.Tables(strTable)
                        .DefaultView.Delete(intRowIndex)
                    End With
                Next

                '刷新显示
                If Me.showModuleData_ZWSEL(strErrMsg) = False Then
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

        Private Sub doMoveUp(ByVal strControlId As String)

            Dim strTable As String = Xydc.Platform.Common.Data.GongzuogangweiData.TABLE_GG_B_VT_SELGONGZUOGANGWEI

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '检查
                Dim intRowIndex As Integer
                If Me.grdZWSEL.SelectedIndex < 0 Then
                    strErrMsg = "错误：没有选择[职务]！"
                    GoTo errProc
                End If
                If Me.grdZWSEL.SelectedIndex = 0 Then
                    strErrMsg = "错误：已经是最前面！"
                    GoTo errProc
                End If
                intRowIndex = Me.grdZWSEL.SelectedIndex

                '获取数据
                If Me.getModuleData_ZWSEL(strErrMsg) = False Then
                    GoTo errProc
                End If

                '上移
                Dim strField As String = Xydc.Platform.Common.Data.GongzuogangweiData.FIELD_GG_B_VT_SELGONGZUOGANGWEI_GWMC
                Dim strZW As String
                With Me.m_objDataSet_ZWSEL.Tables(strTable).DefaultView
                    strZW = objPulicParameters.getObjectValue(.Item(intRowIndex - 1).Item(strField), "")
                    .Item(intRowIndex - 1).Item(strField) = .Item(intRowIndex).Item(strField)
                    .Item(intRowIndex).Item(strField) = strZW
                End With

                '刷新显示
                Me.grdZWSEL.SelectedIndex = intRowIndex - 1
                If Me.showModuleData_ZWSEL(strErrMsg) = False Then
                    GoTo errProc
                End If

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

        Private Sub doMoveDown(ByVal strControlId As String)

            Dim strTable As String = Xydc.Platform.Common.Data.GongzuogangweiData.TABLE_GG_B_VT_SELGONGZUOGANGWEI

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '检查
                Dim intRowIndex As Integer
                If Me.grdZWSEL.SelectedIndex < 0 Then
                    strErrMsg = "错误：没有选择[职务]！"
                    GoTo errProc
                End If
                If Me.grdZWSEL.SelectedIndex = Me.grdZWSEL.Items.Count - 1 Then
                    strErrMsg = "错误：已经是最后！"
                    GoTo errProc
                End If
                intRowIndex = Me.grdZWSEL.SelectedIndex

                '获取数据
                If Me.getModuleData_ZWSEL(strErrMsg) = False Then
                    GoTo errProc
                End If

                '下移
                Dim strField As String = Xydc.Platform.Common.Data.GongzuogangweiData.FIELD_GG_B_VT_SELGONGZUOGANGWEI_GWMC
                Dim strZTC As String
                With Me.m_objDataSet_ZWSEL.Tables(strTable).DefaultView
                    strZTC = objPulicParameters.getObjectValue(.Item(intRowIndex + 1).Item(strField), "")
                    .Item(intRowIndex + 1).Item(strField) = .Item(intRowIndex).Item(strField)
                    .Item(intRowIndex).Item(strField) = strZTC
                End With

                '刷新显示
                Me.grdZWSEL.SelectedIndex = intRowIndex + 1
                If Me.showModuleData_ZWSEL(strErrMsg) = False Then
                    GoTo errProc
                End If

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

        Private Sub btnSelectOne_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelectOne.Click
            Me.doSelectOne("btnSelectOne")
        End Sub

        Private Sub btnSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelectAll.Click
            Me.doSelectAll("btnSelectAll")
        End Sub

        Private Sub btnDeleteOne_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteOne.Click
            Me.doDeleteOne("btnDeleteOne")
        End Sub

        Private Sub btnDeleteAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteAll.Click
            Me.doDeleteAll("btnDeleteAll")
        End Sub

        Private Sub btnMoveUp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMoveUp.Click
            Me.doMoveUp("btnMoveUp")
        End Sub

        Private Sub btnMoveDown_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMoveDown.Click
            Me.doMoveDown("btnMoveDown")
        End Sub

        Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
            Me.doCancel("btnCancel")
        End Sub

        Private Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.Click
            Me.doConfirm("btnOK")
        End Sub

    End Class
End Namespace
