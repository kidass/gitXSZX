Imports System.Web.Security

Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform
    ' 类名    ：customer_medium_typechoice
    ' 
    ' 调用性质：
    '     I/O
    '
    ' 功能描述： 
    '   　“二手客户人员类型选择”处理模块
    '----------------------------------------------------------------
    Partial Public Class customer_medium_typechoice
        Inherits Xydc.Platform.web.PageBase



        '----------------------------------------------------------------
        '模块私用参数
        '----------------------------------------------------------------
        '本模块相对image的相对路径
        Private m_cstrRelativePathToImage As String = "../../"

        '----------------------------------------------------------------
        '与数据网格grdSELBM相关的参数
        '----------------------------------------------------------------
        '网格中模板列中的控件ID
        Private Const m_cstrCheckBoxIdInDataGrid_SELBM As String = "chkSELBM"
        '包含网格的DIV对象ID
        Private Const m_cstrDataGridInDIV_SELBM As String = "divSELBM"
        '网格要锁定的列数
        Private m_intFixedColumns_SELBM As Integer

        Private objDataSet As System.Data.DataSet
        Private m_strSessionId_SELBM As String '缓存objDataSet的SessionId


        '通用修改参数
        Private strTable As String = Xydc.Platform.Common.Data.CustomerMediumData.TABLE_House_B_MediumCustomer
        Private strSQL As String = "select distinct 人员类型  from " + Xydc.Platform.Common.Data.CustomerMediumData.TABLE_House_B_MediumCustomer
        Private strDataFieldName As String = Xydc.Platform.Common.Data.CustomerMediumData.FIELD_House_B_MediumCustomer_CustomerType




        '----------------------------------------------------------------
        ' 释放接口参数
        '----------------------------------------------------------------
        Private Sub releaseInterfaceParameters()

            Try

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


            '获取局部接口参数
            Me.m_strSessionId_SELBM = Me.htxtSessionIdSELBM.Value
            With New Xydc.Platform.Common.Utilities.PulicParameters
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
            Try

            Catch ex As Exception
            End Try

        End Sub

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
            Dim objsystemCommon As New Xydc.Platform.BusinessFacade.systemCommon
            getModuleData_SELBM = False

            Dim strGuid As String
            Try

                '释放资源
                If Not (Me.objDataSet Is Nothing) Then
                    Me.objDataSet.Dispose()
                    Me.objDataSet = Nothing
                End If

                '获取Session的Id
                strGuid = objPulicParameters.getNewGuid()
                If strGuid = "" Then
                    strErrMsg = "无法产生GUID！"
                    GoTo errProc
                End If

                '检索数据
                If objsystemCommon.getDataSetBySQL(strErrMsg, MyBase.UserId, MyBase.UserPassword, strSQL, Me.objDataSet) = False Then
                    GoTo errProc
                End If


                '缓存信息
                Me.m_strSessionId_SELBM = strGuid
                Me.htxtSessionIdSELBM.Value = Me.m_strSessionId_SELBM
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
                If Me.objDataSet Is Nothing Then
                    Me.grdSELBM.DataSource = Nothing
                Else
                    With Me.objDataSet.Tables(0)
                        Me.grdSELBM.DataSource = .DefaultView
                    End With
                    'Me.grdSELBM.DataSource=Me.objDataSet.Tables(0).
                End If

                '调整网格参数
                With Me.objDataSet.Tables(0)
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
                With Me.objDataSet.Tables(0).DefaultView
                    '显示网格位置信息
                    Me.BMlSELBMGridLocInfo.Text = objDataGridProcess.getDataGridLocation(Me.grdSELBM, .Count)
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
                    'Me.btnSELBMDelete.Enabled = blnEnabled
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

                Catch ex As Exception
                End Try

                '显示Pannel
                Me.panelMain.Visible = True
                Me.panelError.Visible = Not Me.panelMain.Visible

                '执行键转译(不论是否是“回发”)
                Try
                    With New Xydc.Platform.web.ControlProcess
                        .doTranslateKey(Me.txtSELBMPageIndex)
                        .doTranslateKey(Me.txtSELBMPageSize)
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
            End If

            If Me.IsPostBack = False Then
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


        '----------------------------------------------------------------
        '模块特殊操作处理器
        '----------------------------------------------------------------
        '处理“取消”按钮
        Private Sub doCancel(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            '释放模块资源
            Me.releaseModuleParameters()
            Me.releaseInterfaceParameters()

            '返回到调用模块，并附加返回参数
            '要返回的SessionId
            Dim strSessionId As String
            strSessionId = Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.ISessionId)
            'SessionId附加到返回的Url
            Dim strUrl As String
            '返回
            Page.ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script> window.returnValue='';window.close();</script>")



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
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim strErrMsg As String
            Dim intStep As Integer

            Dim strReturnValue As String = ""
            Try
                Dim intSelected As Integer = 0
                Dim blnSelected As Boolean
                Dim intCount As Integer
                Dim i As Integer
                Dim intColIndex As Integer
                Dim intRecPos As Integer
                Dim strValue As String
                Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate

                intCount = Me.grdSELBM.Items.Count
                For i = 0 To intCount - 1 Step 1
                    blnSelected = objDataGridProcess.isDataGridItemChecked(Me.grdSELBM.Items(i), 0, Me.m_cstrCheckBoxIdInDataGrid_SELBM)
                    If blnSelected = True Then
                        intSelected += 1
                    End If
                Next

                If intSelected > 0 Then
                    '获取数据
                    If Me.getModuleData_SELBM(strErrMsg) = False Then
                        GoTo errProc
                    End If
                    intColIndex = objDataGridProcess.getDataGridColumnIndex(Me.grdSELBM, strDataFieldName)
                    intCount = Me.grdSELBM.Items.Count
                    For i = intCount - 1 To 0 Step -1
                        blnSelected = objDataGridProcess.isDataGridItemChecked(Me.grdSELBM.Items(i), 0, Me.m_cstrCheckBoxIdInDataGrid_SELBM)
                        If blnSelected = True Then
                            strValue = objDataGridProcess.getDataGridCellValue(Me.grdSELBM.Items(i), intColIndex)
                            If strValue <> "" Then
                                If strReturnValue <> "" Then
                                    strReturnValue = strReturnValue + strSep + strValue
                                Else
                                    strReturnValue = strValue
                                End If
                            End If

                        End If
                    Next
                End If


                '设置返回值                
                Me.htxtReturnValue.Value = strReturnValue
                If Me.m_strSessionId_SELBM.Trim <> "" Then
                    Try
                        Session.Remove(Me.m_strSessionId_SELBM)
                    Catch ex As Exception
                    End Try
                    Me.m_strSessionId_SELBM = ""
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try


            '释放模块资源
            Me.releaseModuleParameters()
            Me.releaseInterfaceParameters()

            '返回到调用模块，并附加返回参数
            '要返回的SessionId
            Dim strSessionId As String
            strSessionId = Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.ISessionId)
            'SessionId附加到返回的Url
            Dim strUrl As String
            Page.ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script> window.returnValue='" + strReturnValue + "';window.close();</script>")

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


        Private Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.Click
            Me.doConfirm("btnOK")
        End Sub

    End Class
End Namespace
