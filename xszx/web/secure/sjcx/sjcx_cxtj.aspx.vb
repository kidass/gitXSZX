Imports System.Web.Security
Imports System.Type

Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.web
    ' 类名    ：jyyw_wjdj
    ' 
    '     可被其他模块调用，本身也不调用其他模块
    '
    ' 功能描述： 
    '   　处理查询条件的查看与编辑操作
    '
    ' 接口参数：
    '     参见接口类iSjcxCxtj描述
    '----------------------------------------------------------------


    Partial Public Class sjcx_cxtj
        Inherits Xydc.Platform.web.PageBase

        '----------------------------------------------------------------
        '模块私用参数
        '----------------------------------------------------------------
        '本模块相对image的相对路径
        Private m_cstrRelativePathToImage As String = "../../"

        '----------------------------------------------------------------
        '模块授权参数
        '----------------------------------------------------------------

        '----------------------------------------------------------------
        '模块现场保留参数，恢复完成后立即释放session资源
        '----------------------------------------------------------------
        Private m_objSaveScence As Xydc.Platform.BusinessFacade.IMSjcxCxtj
        Private m_blnSaveScence As Boolean '是否有恢复现场

        '----------------------------------------------------------------
        '模块接口参数
        '----------------------------------------------------------------
        Private m_objInterface As Xydc.Platform.BusinessFacade.ISjcxCxtj
        Private m_blnInterface As Boolean

        '----------------------------------------------------------------
        '与数据网格grdTJ相关的参数
        '----------------------------------------------------------------
        '网格中模板列中的控件ID
        Private Const m_cstrCheckBoxIdInDataGrid_TJ As String = "chkTJ"
        '包含网格的DIV对象ID
        Private Const m_cstrDataGridInDIV_TJ As String = "divTJ"
        '网格要锁定的列数
        Private m_intFixedColumns_TJ As Integer

        '----------------------------------------------------------------
        '模块访问数据参数
        '----------------------------------------------------------------
        Private m_objDataSet_TJ As Xydc.Platform.Common.Data.QueryData








        '----------------------------------------------------------------
        ' 复原模块现场信息并释放相应的资源
        '----------------------------------------------------------------
        Private Sub restoreModuleInformation(ByVal strSessionId As String)

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            Try
                If Me.m_objSaveScence Is Nothing Then Exit Try

                With Me.m_objSaveScence
                    Me.txtZKHZ.Text = .txtZKHZ
                    Me.txtVal1.Text = .txtVal1
                    Me.txtVal2.Text = .txtVal2
                    Me.txtYKHZ.Text = .txtYKHZ

                    Me.htxtSessionIDTJ.Value = .htxtSessionIDTJ
                    Me.htxtTJSort.Value = .htxtTJSort
                    Me.htxtTJSortColumnIndex.Value = .htxtTJSortColumnIndex
                    Me.htxtTJSortType.Value = .htxtTJSortType

                    Me.htxtDivLeftBody.Value = .htxtDivLeftBody
                    Me.htxtDivTopBody.Value = .htxtDivTopBody
                    Me.htxtDivLeftTJ.Value = .htxtDivLeftTJ
                    Me.htxtDivTopTJ.Value = .htxtDivTopTJ

                    Try
                        Me.rblBJF.SelectedIndex = .rblBJF_SelectedIndex
                    Catch ex As Exception
                    End Try

                    Try
                        Me.rblLJF.SelectedIndex = .rblLJF_SelectedIndex
                    Catch ex As Exception
                    End Try

                    Try
                        Me.lstField.SelectedIndex = .lstField_SelectedIndex
                    Catch ex As Exception
                    End Try

                    Try
                        Me.grdTJ.CurrentPageIndex = .grdTJ_CurrentPageIndex
                        Me.grdTJ.SelectedIndex = .grdTJ_SelectedIndex
                        Me.grdTJ.PageSize = .grdTJ_PageSize
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
                Me.m_objSaveScence = New Xydc.Platform.BusinessFacade.IMSjcxCxtj

                '保存现场信息
                With Me.m_objSaveScence
                    .txtZKHZ = Me.txtZKHZ.Text
                    .txtVal1 = Me.txtVal1.Text
                    .txtVal2 = Me.txtVal2.Text
                    .txtYKHZ = Me.txtYKHZ.Text

                    .htxtSessionIDTJ = Me.htxtSessionIDTJ.Value
                    .htxtTJSort = Me.htxtTJSort.Value
                    .htxtTJSortColumnIndex = Me.htxtTJSortColumnIndex.Value
                    .htxtTJSortType = Me.htxtTJSortType.Value

                    .htxtDivLeftBody = Me.htxtDivLeftBody.Value
                    .htxtDivTopBody = Me.htxtDivTopBody.Value
                    .htxtDivLeftTJ = Me.htxtDivLeftTJ.Value
                    .htxtDivTopTJ = Me.htxtDivTopTJ.Value

                    .rblBJF_SelectedIndex = Me.rblBJF.SelectedIndex
                    .rblLJF_SelectedIndex = Me.rblLJF.SelectedIndex
                    .lstField_SelectedIndex = Me.lstField.SelectedIndex

                    .grdTJ_CurrentPageIndex = Me.grdTJ.CurrentPageIndex
                    .grdTJ_SelectedIndex = Me.grdTJ.SelectedIndex
                    .grdTJ_PageSize = Me.grdTJ.PageSize
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

            Try
                If Me.IsPostBack = True Then Exit Try

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
                        '释放Session
                        Session.Remove(Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.ISessionId))
                        '释放对象
                        Me.m_objInterface.Dispose()
                        Me.m_objInterface = Nothing
                    End If
                End If
            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' 获取接口参数(没有接口参数则显示错误信息页面)
        '----------------------------------------------------------------
        Private Function getInterfaceParameters( _
            ByRef strErrMsg As String, _
            ByRef blnContinueDo As Boolean) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            getInterfaceParameters = False
            blnContinueDo = True

            Try
                '从QueryString中解析接口参数(不论是否回发)
                Dim objTemp As Object
                Try
                    objTemp = Session.Item(Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.ISessionId))
                    m_objInterface = CType(objTemp, Xydc.Platform.BusinessFacade.ISjcxCxtj)
                Catch ex As Exception
                    m_objInterface = Nothing
                End Try

                '必须有接口参数
                Me.m_blnInterface = False
                If m_objInterface Is Nothing Then
                    '显示错误信息
                    Me.panelError.Visible = True
                    Me.panelMain.Visible = Not Me.panelError.Visible
                    Me.lblMessage.Text = "本模块必须提供输入接口参数！"
                    blnContinueDo = False
                    Exit Try
                End If
                Me.m_blnInterface = True

                '获取恢复现场参数
                Me.m_blnSaveScence = False
                If Me.IsPostBack = False Then
                    Dim strSessionId As String
                    strSessionId = objPulicParameters.getObjectValue(Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.MSessionId), "")
                    Try
                        Me.m_objSaveScence = CType(Session.Item(strSessionId), Xydc.Platform.BusinessFacade.IMSjcxCxtj)
                    Catch ex As Exception
                        Me.m_objSaveScence = Nothing
                    End Try
                    If Me.m_objSaveScence Is Nothing Then
                        m_blnSaveScence = False
                    Else
                        m_blnSaveScence = True
                    End If

                    '恢复现场参数后释放该资源
                    Me.restoreModuleInformation(strSessionId)

                    '处理调用模块返回后的信息并同时释放相应资源
                    If Me.getDataFromCallModule(strErrMsg) = False Then
                        GoTo errProc
                    End If
                End If

                '设置模块其他参数

                '获取局部接口参数
                Me.m_intFixedColumns_TJ = objPulicParameters.getObjectValue(Me.htxtTJFixed.Value, 0)

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

            Dim strErrMsg As String
            Try
                '删除模块用Session
                Dim objQueryData As Xydc.Platform.Common.Data.QueryData
                If Me.htxtSessionIDTJ.Value.Trim <> "" Then
                    objQueryData = CType(Session(Me.htxtSessionIDTJ.Value), Xydc.Platform.Common.Data.QueryData)
                    If Not (objQueryData Is Nothing) Then
                        objQueryData.Dispose()
                        objQueryData = Nothing
                    End If
                    Session.Remove(Me.htxtSessionIDTJ.Value)
                    Me.htxtSessionIDTJ.Value = ""
                End If
            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' 获取模块要显示的查询条件信息
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function getModuleData_TJ(ByRef strErrMsg As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            getModuleData_TJ = False

            Try
                '获取数据
                If Me.htxtSessionIDTJ.Value.Trim <> "" Then
                    '从缓存中获取数据
                    Try
                        Me.m_objDataSet_TJ = CType(Session(Me.htxtSessionIDTJ.Value), Xydc.Platform.Common.Data.QueryData)
                    Catch ex As Exception
                        strErrMsg = ex.Message
                        GoTo errProc
                    End Try
                Else
                    '释放资源
                    If Not (Me.m_objDataSet_TJ Is Nothing) Then
                        Me.m_objDataSet_TJ.Dispose()
                        Me.m_objDataSet_TJ = Nothing
                    End If

                    '从输入接口中获取数据
                    If Not (Me.m_objInterface.iDataSetTJ Is Nothing) Then
                        Me.m_objDataSet_TJ = CType(Me.m_objInterface.iDataSetTJ.Copy(), Xydc.Platform.Common.Data.QueryData)
                    Else
                        Me.m_objDataSet_TJ = New Xydc.Platform.Common.Data.QueryData(Xydc.Platform.Common.Data.QueryData.enumTableType.CX_B_CHAXUNTIAOJIAN)
                    End If

                    '缓存数据
                    If Me.htxtSessionIDTJ.Value.Trim = "" Then
                        Me.htxtSessionIDTJ.Value = objPulicParameters.getNewGuid()
                    End If
                    Session.Add(Me.htxtSessionIDTJ.Value, Me.m_objDataSet_TJ)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            getModuleData_TJ = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示条件输入和编辑窗的数据
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showEditPanelInfo_TJ(ByRef strErrMsg As String) As Boolean

            showEditPanelInfo_TJ = False

            Try
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            showEditPanelInfo_TJ = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示grdTJ的数据
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showDataGridInfo_TJ(ByRef strErrMsg As String) As Boolean

            Dim strTableName As String = Xydc.Platform.Common.Data.QueryData.TABLE_CX_B_CHAXUNTIAOJIAN
            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess

            showDataGridInfo_TJ = False

            '获取系统保存的网格排序数据
            Dim intSortColumnIndex As Integer
            intSortColumnIndex = objPulicParameters.getObjectValue(Me.htxtTJSortColumnIndex.Value, -1)
            Dim objSortType As Xydc.Platform.Common.Utilities.PulicParameters.enumSortType
            Try
                objSortType = CType(Me.htxtTJSortType.Value, Xydc.Platform.Common.Utilities.PulicParameters.enumSortType)
            Catch ex As Exception
                objSortType = Xydc.Platform.Common.Utilities.PulicParameters.enumSortType.None
            End Try

            '网格显示处理
            Try
                '在获取数据时已经恢复了RowFilter、Sort的现场
                '设置数据源
                If Me.m_objDataSet_TJ Is Nothing Then
                    Me.grdTJ.DataSource = Nothing
                Else
                    With Me.m_objDataSet_TJ.Tables(strTableName)
                        Me.grdTJ.DataSource = .DefaultView
                    End With
                End If

                '调整网格参数
                Dim intCount As Integer
                Try
                    With Me.m_objDataSet_TJ.Tables(strTableName)
                        intCount = .DefaultView.Count
                    End With
                Catch ex As Exception
                    intCount = 0
                End Try
                If objDataGridProcess.onBeforeDataGridBind(strErrMsg, Me.grdTJ, intCount) = False Then
                    GoTo errProc
                End If

                '恢复列标题中的排序信息
                If intSortColumnIndex >= 0 Then
                    objDataGridProcess.doClearSortCharInDataGridHead(Me.grdTJ)
                    With Me.grdTJ.Columns(intSortColumnIndex)
                        .HeaderText = objDataGridProcess.getColumnSortHeadString(.HeaderText, objSortType)
                    End With
                End If

                '绑定数据
                Me.grdTJ.DataBind()

                '----------------------------------------------------------------
                '因为这些信息是非绑定的，所以下面的操作必须等绑定完成后执行！！！
                '一旦在后续处理中执行了DataBind，则信息会丢失！！！
                '----------------------------------------------------------------
                '恢复网格中的CheckBox状态
                If objDataGridProcess.doRestoreDataGridCheckBoxStatus(strErrMsg, Me.grdTJ, Request, 0, Me.m_cstrCheckBoxIdInDataGrid_TJ) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)

            showDataGridInfo_TJ = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示模块的现有查询条件信息
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showModuleData_TJ(ByRef strErrMsg As String) As Boolean

            showModuleData_TJ = False

            Try
                If Me.showDataGridInfo_TJ(strErrMsg) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            showModuleData_TJ = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 初始化查询条件
        '----------------------------------------------------------------
        Private Sub doInitControls()

            Try
                Me.txtZKHZ.Text = ""
                Me.txtVal1.Text = ""
                Me.txtVal2.Text = ""
                Me.txtYKHZ.Text = ""
                Me.rblBJF.SelectedIndex = -1
                Me.rblLJF.SelectedIndex = -1

                Me.txtZKHZ.Enabled = False
                Me.txtVal1.Enabled = False
                Me.txtVal2.Enabled = False
                Me.txtYKHZ.Enabled = False
                Me.rblBJF.Enabled = False
                Me.rblLJF.Enabled = False
            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' 根据选定的字段设置操作控件
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showModuleData_MAIN(ByRef strErrMsg As String) As Boolean

            Dim objRadioButtonListProcess As New Xydc.Platform.web.RadioButtonListProcess

            showModuleData_MAIN = False

            Try
                '清除条件值
                Me.doInitControls()

                '没有字段
                If (Me.lstField.SelectedIndex < 0) Then
                    Exit Try
                End If
                Dim strField As String = Me.lstField.Items(Me.lstField.SelectedIndex).Value.Trim
                Dim objDataColumn As System.Data.DataColumn
                objDataColumn = Me.m_objInterface.iQueryTable.Columns(strField)
                If objDataColumn Is Nothing Then
                    Exit Try
                End If

                '重建比较运算符
                If Me.doSetBJFList(strErrMsg) = False Then
                    GoTo errProc
                End If

                '根据选定字段设置
                Select Case System.Type.GetTypeCode(objDataColumn.DataType)
                    Case System.TypeCode.String, System.TypeCode.Char
                        Me.txtZKHZ.Enabled = True
                        Me.txtVal1.Enabled = False
                        Me.txtVal2.Enabled = False
                        Me.txtYKHZ.Enabled = True
                        Me.rblBJF.Enabled = True
                        Me.rblLJF.Enabled = True
                        Me.rblBJF.Items.RemoveAt(8)
                        Me.rblBJF.Items.RemoveAt(5)
                        Me.rblBJF.Items.RemoveAt(4)
                        Me.rblBJF.Items.RemoveAt(3)
                        Me.rblBJF.Items.RemoveAt(2)
                    Case System.TypeCode.DateTime
                        Me.txtZKHZ.Enabled = True
                        Me.txtVal1.Enabled = False
                        Me.txtVal2.Enabled = False
                        Me.txtYKHZ.Enabled = True
                        Me.rblBJF.Enabled = True
                        Me.rblLJF.Enabled = True
                        Me.rblBJF.Items.RemoveAt(7)
                        Me.rblBJF.Items.RemoveAt(6)
                    Case System.TypeCode.Byte, _
                        System.TypeCode.Int16, System.TypeCode.Int32, System.TypeCode.Int64, _
                        System.TypeCode.UInt16, System.TypeCode.UInt32, System.TypeCode.UInt64, _
                        System.TypeCode.Decimal, System.TypeCode.Double, System.TypeCode.Single
                        Me.txtZKHZ.Enabled = True
                        Me.txtVal1.Enabled = False
                        Me.txtVal2.Enabled = False
                        Me.txtYKHZ.Enabled = True
                        Me.rblBJF.Enabled = True
                        Me.rblLJF.Enabled = True
                        Me.rblBJF.Items.RemoveAt(7)
                        Me.rblBJF.Items.RemoveAt(6)
                    Case Else
                        strErrMsg = "错误：无效的数据类型！"
                        GoTo errProc
                End Select

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            '如果没有设定，则缺省
            If objRadioButtonListProcess.doSetDefaultSelectedIndex(strErrMsg, Me.rblLJF) = False Then
                '可以不成功
            End If

            Xydc.Platform.web.RadioButtonListProcess.SafeRelease(objRadioButtonListProcess)

            showModuleData_MAIN = True
            Exit Function

errProc:
            Xydc.Platform.web.RadioButtonListProcess.SafeRelease(objRadioButtonListProcess)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据选定的查询设置操作控件
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showModuleData_ITEM(ByRef strErrMsg As String) As Boolean

            Dim objRadioButtonListProcess As New Xydc.Platform.web.RadioButtonListProcess
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objListBoxProcess As New Xydc.Platform.web.ListBoxProcess

            showModuleData_ITEM = False

            Try
                '清除条件值
                Me.doInitControls()

                '没有字段
                If (Me.grdTJ.SelectedIndex < 0) Then
                    Exit Try
                End If

                '获取当前行的字段名
                Dim strField As String = ""
                Dim intIndex(10) As Integer
                intIndex(0) = objDataGridProcess.getDataGridColumnIndex(Me.grdTJ, Xydc.Platform.Common.Data.QueryData.FIELD_CX_B_CHAXUNTIAOJIAN_ZDMC)
                strField = objDataGridProcess.getDataGridCellValue(Me.grdTJ.Items(Me.grdTJ.SelectedIndex), intIndex(0))
                Me.lstField.SelectedIndex = objListBoxProcess.getSelectedItem(Me.lstField, strField)
                If Me.lstField.SelectedIndex < 0 Then
                    Exit Try
                End If
                Dim objDataColumn As System.Data.DataColumn
                objDataColumn = Me.m_objInterface.iQueryTable.Columns(strField)
                If objDataColumn Is Nothing Then
                    Exit Try
                End If

                '重建比较运算符
                If Me.doSetBJFList(strErrMsg) = False Then
                    GoTo errProc
                End If

                '根据字段类型处理
                Select Case System.Type.GetTypeCode(objDataColumn.DataType)
                    Case System.TypeCode.String, System.TypeCode.Char
                        Me.txtZKHZ.Enabled = True
                        Me.txtVal1.Enabled = False
                        Me.txtVal2.Enabled = False
                        Me.txtYKHZ.Enabled = True
                        Me.rblBJF.Enabled = True
                        Me.rblLJF.Enabled = True
                        Me.rblBJF.Items.RemoveAt(8)
                        Me.rblBJF.Items.RemoveAt(5)
                        Me.rblBJF.Items.RemoveAt(4)
                        Me.rblBJF.Items.RemoveAt(3)
                        Me.rblBJF.Items.RemoveAt(2)
                    Case System.TypeCode.DateTime
                        Me.txtZKHZ.Enabled = True
                        Me.txtVal1.Enabled = False
                        Me.txtVal2.Enabled = False
                        Me.txtYKHZ.Enabled = True
                        Me.rblBJF.Enabled = True
                        Me.rblLJF.Enabled = True
                        Me.rblBJF.Items.RemoveAt(7)
                        Me.rblBJF.Items.RemoveAt(6)
                    Case System.TypeCode.Byte, _
                        System.TypeCode.Int16, System.TypeCode.Int32, System.TypeCode.Int64, _
                        System.TypeCode.UInt16, System.TypeCode.UInt32, System.TypeCode.UInt64, _
                        System.TypeCode.Decimal, System.TypeCode.Double, System.TypeCode.Single
                        Me.txtZKHZ.Enabled = True
                        Me.txtVal1.Enabled = False
                        Me.txtVal2.Enabled = False
                        Me.txtYKHZ.Enabled = True
                        Me.rblBJF.Enabled = True
                        Me.rblLJF.Enabled = True
                        Me.rblBJF.Items.RemoveAt(7)
                        Me.rblBJF.Items.RemoveAt(6)
                    Case Else
                        strErrMsg = "错误：无效的数据类型！"
                        GoTo errProc
                End Select

                '显示条件值
                Dim strValue As String
                Dim intValue As Integer
                '==========================================================================================================
                intIndex(1) = objDataGridProcess.getDataGridColumnIndex(Me.grdTJ, Xydc.Platform.Common.Data.QueryData.FIELD_CX_B_CHAXUNTIAOJIAN_ZKHZ)
                Me.txtZKHZ.Text = objDataGridProcess.getDataGridCellValue(Me.grdTJ.Items(Me.grdTJ.SelectedIndex), intIndex(1))
                '==========================================================================================================
                intIndex(2) = objDataGridProcess.getDataGridColumnIndex(Me.grdTJ, Xydc.Platform.Common.Data.QueryData.FIELD_CX_B_CHAXUNTIAOJIAN_YKHZ)
                Me.txtYKHZ.Text = objDataGridProcess.getDataGridCellValue(Me.grdTJ.Items(Me.grdTJ.SelectedIndex), intIndex(2))
                '==========================================================================================================
                intIndex(3) = objDataGridProcess.getDataGridColumnIndex(Me.grdTJ, Xydc.Platform.Common.Data.QueryData.FIELD_CX_B_CHAXUNTIAOJIAN_VAL1)
                Me.txtVal1.Text = objDataGridProcess.getDataGridCellValue(Me.grdTJ.Items(Me.grdTJ.SelectedIndex), intIndex(3))
                '==========================================================================================================
                intIndex(4) = objDataGridProcess.getDataGridColumnIndex(Me.grdTJ, Xydc.Platform.Common.Data.QueryData.FIELD_CX_B_CHAXUNTIAOJIAN_VAL2)
                Me.txtVal2.Text = objDataGridProcess.getDataGridCellValue(Me.grdTJ.Items(Me.grdTJ.SelectedIndex), intIndex(4))
                '==========================================================================================================
                intIndex(5) = objDataGridProcess.getDataGridColumnIndex(Me.grdTJ, Xydc.Platform.Common.Data.QueryData.FIELD_CX_B_CHAXUNTIAOJIAN_BJFZ)
                strValue = objDataGridProcess.getDataGridCellValue(Me.grdTJ.Items(Me.grdTJ.SelectedIndex), intIndex(5))
                Me.rblBJF.SelectedIndex = objRadioButtonListProcess.getCheckedItem(Me.rblBJF, strValue)
                '==========================================================================================================
                intIndex(6) = objDataGridProcess.getDataGridColumnIndex(Me.grdTJ, Xydc.Platform.Common.Data.QueryData.FIELD_CX_B_CHAXUNTIAOJIAN_LJFZ)
                strValue = objDataGridProcess.getDataGridCellValue(Me.grdTJ.Items(Me.grdTJ.SelectedIndex), intIndex(6))
                Me.rblLJF.SelectedIndex = objRadioButtonListProcess.getCheckedItem(Me.rblLJF, strValue)
                '==========================================================================================================

                '根据比较符条件使能txtVal1和txtVal2
                If Me.showModuleData_BJF(strErrMsg) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.RadioButtonListProcess.SafeRelease(objRadioButtonListProcess)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.ListBoxProcess.SafeRelease(objListBoxProcess)

            showModuleData_ITEM = True
            Exit Function

errProc:
            Xydc.Platform.web.RadioButtonListProcess.SafeRelease(objRadioButtonListProcess)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.ListBoxProcess.SafeRelease(objListBoxProcess)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据选定的比较符设置操作控件
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showModuleData_BJF(ByRef strErrMsg As String) As Boolean

            showModuleData_BJF = False

            Try
                Select Case Me.rblBJF.SelectedValue.ToLower
                    Case Xydc.Platform.Common.Data.QueryData.COMPARESIGN_EQ, _
                        Xydc.Platform.Common.Data.QueryData.COMPARESIGN_NOTEQ, _
                        Xydc.Platform.Common.Data.QueryData.COMPARESIGN_LT, _
                        Xydc.Platform.Common.Data.QueryData.COMPARESIGN_LET, _
                        Xydc.Platform.Common.Data.QueryData.COMPARESIGN_GT, _
                        Xydc.Platform.Common.Data.QueryData.COMPARESIGN_GET, _
                        Xydc.Platform.Common.Data.QueryData.COMPARESIGN_LIKE, _
                        Xydc.Platform.Common.Data.QueryData.COMPARESIGN_NOTLIKE
                        Me.txtVal1.Enabled = True
                        Me.txtVal2.Text = ""
                        Me.txtVal2.Enabled = False
                    Case Xydc.Platform.Common.Data.QueryData.COMPARESIGN_BETWEEN
                        Me.txtVal1.Enabled = True
                        Me.txtVal2.Enabled = True
                    Case Else
                        Me.txtVal1.Text = ""
                        Me.txtVal2.Text = ""
                        Me.txtVal1.Enabled = True
                        Me.txtVal2.Enabled = True
                End Select

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            showModuleData_BJF = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示比较运算符列表
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function doSetBJFList(ByRef strErrMsg As String) As Boolean

            doSetBJFList = False

            Try
                '备份
                Dim intIndex As Integer = Me.rblBJF.SelectedIndex

                '清空
                Me.rblBJF.Items.Clear()

                '加入
                Dim objListItem As System.Web.UI.WebControls.ListItem
                '==========================================================================================================
                objListItem = New System.Web.UI.WebControls.ListItem(Xydc.Platform.Common.Data.QueryData.COMPARESIGN_NAME_EQ, Xydc.Platform.Common.Data.QueryData.COMPARESIGN_EQ)
                Me.rblBJF.Items.Add(objListItem)
                '==========================================================================================================
                objListItem = New System.Web.UI.WebControls.ListItem(Xydc.Platform.Common.Data.QueryData.COMPARESIGN_NAME_NOTEQ, Xydc.Platform.Common.Data.QueryData.COMPARESIGN_NOTEQ)
                Me.rblBJF.Items.Add(objListItem)
                '==========================================================================================================
                objListItem = New System.Web.UI.WebControls.ListItem(Xydc.Platform.Common.Data.QueryData.COMPARESIGN_NAME_LT, Xydc.Platform.Common.Data.QueryData.COMPARESIGN_LT)
                Me.rblBJF.Items.Add(objListItem)
                '==========================================================================================================
                objListItem = New System.Web.UI.WebControls.ListItem(Xydc.Platform.Common.Data.QueryData.COMPARESIGN_NAME_LET, Xydc.Platform.Common.Data.QueryData.COMPARESIGN_LET)
                Me.rblBJF.Items.Add(objListItem)
                '==========================================================================================================
                objListItem = New System.Web.UI.WebControls.ListItem(Xydc.Platform.Common.Data.QueryData.COMPARESIGN_NAME_GT, Xydc.Platform.Common.Data.QueryData.COMPARESIGN_GT)
                Me.rblBJF.Items.Add(objListItem)
                '==========================================================================================================
                objListItem = New System.Web.UI.WebControls.ListItem(Xydc.Platform.Common.Data.QueryData.COMPARESIGN_NAME_GET, Xydc.Platform.Common.Data.QueryData.COMPARESIGN_GET)
                Me.rblBJF.Items.Add(objListItem)
                '==========================================================================================================
                objListItem = New System.Web.UI.WebControls.ListItem(Xydc.Platform.Common.Data.QueryData.COMPARESIGN_NAME_LIKE, Xydc.Platform.Common.Data.QueryData.COMPARESIGN_LIKE)
                Me.rblBJF.Items.Add(objListItem)
                '==========================================================================================================
                objListItem = New System.Web.UI.WebControls.ListItem(Xydc.Platform.Common.Data.QueryData.COMPARESIGN_NAME_NOTLIKE, Xydc.Platform.Common.Data.QueryData.COMPARESIGN_NOTLIKE)
                Me.rblBJF.Items.Add(objListItem)
                '==========================================================================================================
                objListItem = New System.Web.UI.WebControls.ListItem(Xydc.Platform.Common.Data.QueryData.COMPARESIGN_NAME_BETWEEN, Xydc.Platform.Common.Data.QueryData.COMPARESIGN_BETWEEN)
                Me.rblBJF.Items.Add(objListItem)
                '==========================================================================================================

                '恢复
                Try
                    Me.rblBJF.SelectedIndex = intIndex
                Catch ex As Exception
                    Me.rblBJF.SelectedIndex = -1
                End Try

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doSetBJFList = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示条件连接符列表
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function doSetLJFList(ByRef strErrMsg As String) As Boolean

            doSetLJFList = False

            Try
                '备份
                Dim intIndex As Integer = Me.rblLJF.SelectedIndex

                '清空
                Me.rblLJF.Items.Clear()

                '加入
                Dim objListItem As System.Web.UI.WebControls.ListItem
                '==========================================================================================================
                objListItem = New System.Web.UI.WebControls.ListItem(Xydc.Platform.Common.Data.QueryData.JOINSIGN_NAME_AND, Xydc.Platform.Common.Data.QueryData.JOINSIGN_AND)
                Me.rblLJF.Items.Add(objListItem)
                '==========================================================================================================
                objListItem = New System.Web.UI.WebControls.ListItem(Xydc.Platform.Common.Data.QueryData.JOINSIGN_NAME_OR, Xydc.Platform.Common.Data.QueryData.JOINSIGN_OR)
                Me.rblLJF.Items.Add(objListItem)
                '==========================================================================================================

                '恢复
                Try
                    Me.rblLJF.SelectedIndex = intIndex
                Catch ex As Exception
                    Me.rblLJF.SelectedIndex = -1
                End Try

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doSetLJFList = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据接口参数显示查询字段列表
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function doSetFieldList(ByRef strErrMsg As String) As Boolean

            doSetFieldList = False

            Try
                '备份
                Dim intIndex As Integer = Me.lstField.SelectedIndex

                '清空
                Me.lstField.Items.Clear()

                '加入
                Dim objListItem As System.Web.UI.WebControls.ListItem
                Dim intCount As Integer
                Dim i As Integer
                If Not (Me.m_objInterface.iQueryTable Is Nothing) Then
                    With Me.m_objInterface.iQueryTable
                        intCount = .Columns.Count
                        For i = 0 To intCount - 1 Step 1
                            objListItem = New System.Web.UI.WebControls.ListItem(.Columns(i).ColumnName, .Columns(i).ColumnName)
                            Me.lstField.Items.Add(objListItem)
                        Next
                    End With
                End If

                '恢复
                If intIndex < 0 Then intIndex = 0
                Try
                    Me.lstField.SelectedIndex = intIndex
                Catch ex As Exception
                    Me.lstField.SelectedIndex = -1
                End Try

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doSetFieldList = True
            Exit Function

errProc:
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
                    '显示Pannel(不论是否回调，始终显示panelMain)
                    Me.panelMain.Visible = True
                    Me.panelError.Visible = Not Me.panelMain.Visible

                    '执行键转译(不论是否是“回发”)
                    With New Xydc.Platform.web.ControlProcess
                        .doTranslateKey(Me.txtZKHZ)
                        .doTranslateKey(Me.txtVal1)
                        .doTranslateKey(Me.txtVal2)
                        .doTranslateKey(Me.txtYKHZ)
                    End With

                    '显示字段列表
                    If doSetFieldList(strErrMsg) = False Then
                        GoTo errProc
                    End If

                    '根据选定字段显示查询条件输入控制
                    If Me.showModuleData_MAIN(strErrMsg) = False Then
                        GoTo errProc
                    End If

                    '显示现有查询条件
                    If Me.getModuleData_TJ(strErrMsg) = False Then
                        GoTo errProc
                    End If
                    If Me.showModuleData_TJ(strErrMsg) = False Then
                        GoTo errProc
                    End If

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
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

            '初始化列表
            If Me.IsPostBack = False Then
                If Me.doSetBJFList(strErrMsg) = False Then
                    GoTo errProc
                End If
                If Me.doSetLJFList(strErrMsg) = False Then
                    GoTo errProc
                End If
            End If

            '获取接口参数
            Dim blnDo As Boolean
            If Me.getInterfaceParameters(strErrMsg, blnDo) = False Then
                GoTo errProc
            End If
            If blnDo = False Then GoTo normExit

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
        '事件处理器
        '----------------------------------------------------------------
        '实现对grdTJ网格行、列的固定
        Sub grdTJ_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles grdTJ.ItemDataBound

            Dim intCells As Integer
            Dim i As Integer

            Try
                If e.Item.ItemIndex < 0 Then
                    '标题行,输出标题锁定一般属性
                    intCells = e.Item.Cells.Count
                    For i = 0 To intCells - 1 Step 1
                        e.Item.Cells(i).Attributes.CssStyle.Add("top", "expression(" + Me.m_cstrDataGridInDIV_TJ + ".scrollTop)")
                    Next
                End If
                If Me.m_intFixedColumns_TJ > 0 Then
                    '锁定列
                    For i = 0 To Me.m_intFixedColumns_TJ - 1 Step 1
                        e.Item.Cells(i).CssClass = Me.grdTJ.ID + "Locked"
                    Next
                End If
            Catch ex As Exception
            End Try

        End Sub

        Private Sub grdTJ_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdTJ.SelectedIndexChanged

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                If Me.showModuleData_ITEM(strErrMsg) = False Then
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

        Private Sub lstField_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstField.SelectedIndexChanged

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                If Me.showModuleData_MAIN(strErrMsg) = False Then
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

        Private Sub rblBJF_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rblBJF.SelectedIndexChanged

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                If Me.showModuleData_BJF(strErrMsg) = False Then
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




        '----------------------------------------------------------------
        '模块特殊操作处理器
        '----------------------------------------------------------------
        Private Function doValidateInputValue(ByRef strErrMsg As String) As Boolean

            doValidateInputValue = False

            Try
                '检查输入值
                '==========================================================================================================
                Dim strField As String
                If Me.lstField.SelectedIndex < 0 Then
                    strErrMsg = "错误：未指定[字段名]！"
                    GoTo errProc
                End If
                strField = Me.lstField.Items(Me.lstField.SelectedIndex).Value.Trim
                '==========================================================================================================
                If Me.rblBJF.SelectedIndex < 0 Then
                    strErrMsg = "错误：未指定[比较运算符]！"
                    GoTo errProc
                End If
                '==========================================================================================================
                Dim strValue(2) As String
                If Me.txtVal1.Enabled = True Then
                    strValue(0) = Me.txtVal1.Text
                Else
                    strValue(0) = ""
                End If
                If Me.txtVal2.Enabled = True Then
                    strValue(1) = Me.txtVal2.Text
                Else
                    strValue(1) = ""
                End If
                '==========================================================================================================
                If Me.rblLJF.SelectedIndex < 0 Then
                    strErrMsg = "错误：未指定[条件连接符]！"
                    GoTo errProc
                End If
                '==========================================================================================================
                Dim objDataColumn As System.Data.DataColumn
                objDataColumn = Me.m_objInterface.iQueryTable.Columns(strField)
                If objDataColumn Is Nothing Then
                    strErrMsg = "错误：[" + strField + "]不存在！"
                    GoTo errProc
                End If

                '判断值是否有效?
                Dim objDate(2) As DateTime
                Dim dblValue(2) As Double
                Dim intValue(2) As Int64
                '==========================================================================================================
                If Me.txtVal1.Enabled = True Then
                    Select Case System.Type.GetTypeCode(objDataColumn.DataType)
                        Case TypeCode.DateTime
                            Try
                                objDate(0) = CType(strValue(0), DateTime)
                            Catch ex As Exception
                                strErrMsg = "错误：无效的日期！"
                                GoTo errProc
                            End Try
                            Me.txtVal1.Text = Format(objDate(0), "yyyy-MM-dd HH:mm:ss")
                        Case System.TypeCode.Byte, _
                            System.TypeCode.Int16, System.TypeCode.Int32, System.TypeCode.Int64, _
                            System.TypeCode.UInt16, System.TypeCode.UInt32, System.TypeCode.UInt64
                            Try
                                intValue(0) = CType(strValue(0), Int64)
                            Catch ex As Exception
                                strErrMsg = "错误：无效的整数！"
                                GoTo errProc
                            End Try
                            Me.txtVal1.Text = intValue(0).ToString()
                        Case System.TypeCode.Decimal, System.TypeCode.Double, System.TypeCode.Single
                            Try
                                dblValue(0) = CType(strValue(0), Double)
                            Catch ex As Exception
                                strErrMsg = "错误：无效的数值！"
                                GoTo errProc
                            End Try
                            Me.txtVal1.Text = dblValue(0).ToString()
                        Case System.TypeCode.String
                        Case Else
                            strErrMsg = "错误：无效的数据类型！"
                            GoTo errProc
                    End Select
                End If
                '==========================================================================================================
                If Me.txtVal2.Enabled = True Then
                    Select Case System.Type.GetTypeCode(objDataColumn.DataType)
                        Case TypeCode.DateTime
                            Try
                                objDate(1) = CType(strValue(1), DateTime)
                            Catch ex As Exception
                                strErrMsg = "错误：无效的日期！"
                                GoTo errProc
                            End Try
                            If objDate(1) < objDate(0) Then
                                Me.txtVal1.Text = Format(objDate(1), "yyyy-MM-dd HH:mm:ss")
                                Me.txtVal2.Text = Format(objDate(0), "yyyy-MM-dd HH:mm:ss")
                            Else
                                Me.txtVal1.Text = Format(objDate(0), "yyyy-MM-dd HH:mm:ss")
                                Me.txtVal2.Text = Format(objDate(1), "yyyy-MM-dd HH:mm:ss")
                            End If
                        Case System.TypeCode.Byte, _
                            System.TypeCode.Int16, System.TypeCode.Int32, System.TypeCode.Int64, _
                            System.TypeCode.UInt16, System.TypeCode.UInt32, System.TypeCode.UInt64
                            Try
                                intValue(1) = CType(strValue(1), Int64)
                            Catch ex As Exception
                                strErrMsg = "错误：无效的整数！"
                                GoTo errProc
                            End Try
                            If intValue(1) < intValue(0) Then
                                Me.txtVal1.Text = intValue(1).ToString()
                                Me.txtVal2.Text = intValue(0).ToString()
                            Else
                                Me.txtVal1.Text = intValue(0).ToString()
                                Me.txtVal2.Text = intValue(1).ToString()
                            End If
                        Case System.TypeCode.Decimal, System.TypeCode.Double, System.TypeCode.Single
                            Try
                                dblValue(0) = CType(strValue(0), Double)
                            Catch ex As Exception
                                strErrMsg = "错误：无效的数值！"
                                GoTo errProc
                            End Try
                            If dblValue(1) < dblValue(0) Then
                                Me.txtVal1.Text = dblValue(1).ToString()
                                Me.txtVal2.Text = dblValue(0).ToString()
                            Else
                                Me.txtVal1.Text = dblValue(0).ToString()
                                Me.txtVal2.Text = dblValue(1).ToString()
                            End If
                        Case System.TypeCode.String
                        Case Else
                            strErrMsg = "错误：无效的数据类型！"
                            GoTo errProc
                    End Select
                End If
                '==========================================================================================================
                Me.txtZKHZ.Text = Me.txtZKHZ.Text.Trim
                If Me.txtZKHZ.Text = "" Then Me.txtZKHZ.Text = "0"
                If Me.txtZKHZ.Text <> "" Then
                    Try
                        intValue(0) = CType(Me.txtZKHZ.Text, Int64)
                    Catch ex As Exception
                        intValue(0) = 0
                    End Try
                    Me.txtZKHZ.Text = intValue(0).ToString()
                End If
                '==========================================================================================================
                Me.txtYKHZ.Text = Me.txtYKHZ.Text.Trim
                If Me.txtYKHZ.Text = "" Then Me.txtYKHZ.Text = "0"
                If Me.txtYKHZ.Text <> "" Then
                    Try
                        intValue(0) = CType(Me.txtYKHZ.Text, Int64)
                    Catch ex As Exception
                        intValue(0) = 0
                    End Try
                    Me.txtYKHZ.Text = intValue(0).ToString()
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doValidateInputValue = True
            Exit Function
errProc:
            Exit Function
        End Function

        Private Sub doAddNew(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '检查输入值
                If Me.doValidateInputValue(strErrMsg) = False Then
                    GoTo errProc
                End If
                Dim objDataColumn As System.Data.DataColumn
                objDataColumn = Me.m_objInterface.iQueryTable.Columns(Me.lstField.Items(Me.lstField.SelectedIndex).Value)

                '获取条件数据
                If Me.getModuleData_TJ(strErrMsg) = False Then
                    GoTo errProc
                End If

                '加入
                Dim objDataRow As System.Data.DataRow
                Dim intTypeCode As Integer
                Dim strValue As String = ""
                Dim intCount As Integer
                Dim i As Integer
                With Me.m_objDataSet_TJ.Tables(Xydc.Platform.Common.Data.QueryData.TABLE_CX_B_CHAXUNTIAOJIAN)
                    objDataRow = .NewRow()
                    '==========================================================================================================
                    intCount = CType(Me.txtZKHZ.Text, Integer)
                    strValue = ""
                    For i = 0 To intCount - 1 Step 1
                        If strValue = "" Then
                            strValue = "("
                        Else
                            strValue += "("
                        End If
                    Next
                    objDataRow.Item(Xydc.Platform.Common.Data.QueryData.FIELD_CX_B_CHAXUNTIAOJIAN_ZKHZ) = Me.txtZKHZ.Text
                    objDataRow.Item(Xydc.Platform.Common.Data.QueryData.FIELD_CX_B_CHAXUNTIAOJIAN_ZKHM) = strValue
                    '==========================================================================================================
                    intCount = CType(Me.txtYKHZ.Text, Integer)
                    strValue = ""
                    For i = 0 To intCount - 1 Step 1
                        If strValue = "" Then
                            strValue = ")"
                        Else
                            strValue += ")"
                        End If
                    Next
                    objDataRow.Item(Xydc.Platform.Common.Data.QueryData.FIELD_CX_B_CHAXUNTIAOJIAN_YKHZ) = Me.txtYKHZ.Text
                    objDataRow.Item(Xydc.Platform.Common.Data.QueryData.FIELD_CX_B_CHAXUNTIAOJIAN_YKHM) = strValue
                    '==========================================================================================================
                    objDataRow.Item(Xydc.Platform.Common.Data.QueryData.FIELD_CX_B_CHAXUNTIAOJIAN_ZDMC) = Me.lstField.Items(Me.lstField.SelectedIndex).Value.Trim
                    objDataRow.Item(Xydc.Platform.Common.Data.QueryData.FIELD_CX_B_CHAXUNTIAOJIAN_VAL1) = Me.txtVal1.Text
                    objDataRow.Item(Xydc.Platform.Common.Data.QueryData.FIELD_CX_B_CHAXUNTIAOJIAN_VAL2) = Me.txtVal2.Text
                    '==========================================================================================================
                    objDataRow.Item(Xydc.Platform.Common.Data.QueryData.FIELD_CX_B_CHAXUNTIAOJIAN_BJFM) = Me.rblBJF.Items(Me.rblBJF.SelectedIndex).Text.Trim
                    objDataRow.Item(Xydc.Platform.Common.Data.QueryData.FIELD_CX_B_CHAXUNTIAOJIAN_BJFZ) = Me.rblBJF.Items(Me.rblBJF.SelectedIndex).Value.Trim
                    '==========================================================================================================
                    objDataRow.Item(Xydc.Platform.Common.Data.QueryData.FIELD_CX_B_CHAXUNTIAOJIAN_LJFM) = Me.rblLJF.Items(Me.rblLJF.SelectedIndex).Text.Trim
                    objDataRow.Item(Xydc.Platform.Common.Data.QueryData.FIELD_CX_B_CHAXUNTIAOJIAN_LJFZ) = Me.rblLJF.Items(Me.rblLJF.SelectedIndex).Value.Trim
                    '==========================================================================================================
                    intTypeCode = System.Type.GetTypeCode(objDataColumn.DataType)
                    objDataRow.Item(Xydc.Platform.Common.Data.QueryData.FIELD_CX_B_CHAXUNTIAOJIAN_ZDLX) = intTypeCode.ToString()
                    '==========================================================================================================
                    .Rows.Add(objDataRow)
                End With

                '重新显示网格
                If Me.showModuleData_TJ(strErrMsg) = False Then
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

        Private Sub doModify(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '检查
                If Me.grdTJ.SelectedIndex < 0 Then
                    Me.doAddNew("strControlId")
                    Exit Try
                End If

                '检查输入值
                If Me.doValidateInputValue(strErrMsg) = False Then
                    GoTo errProc
                End If
                Dim objDataColumn As System.Data.DataColumn
                objDataColumn = Me.m_objInterface.iQueryTable.Columns(Me.lstField.Items(Me.lstField.SelectedIndex).Value)

                '获取条件数据
                If Me.getModuleData_TJ(strErrMsg) = False Then
                    GoTo errProc
                End If

                '更新
                Dim objDataRow As System.Data.DataRow
                Dim intTypeCode As Integer
                Dim strValue As String = ""
                Dim intCount As Integer
                Dim i As Integer
                intCount = objDataGridProcess.getRecordPosition(Me.grdTJ.SelectedIndex, Me.grdTJ.CurrentPageIndex, Me.grdTJ.PageSize)
                With Me.m_objDataSet_TJ.Tables(Xydc.Platform.Common.Data.QueryData.TABLE_CX_B_CHAXUNTIAOJIAN)
                    objDataRow = .DefaultView.Item(intCount).Row
                    '==========================================================================================================
                    intCount = CType(Me.txtZKHZ.Text, Integer)
                    strValue = ""
                    For i = 0 To intCount - 1 Step 1
                        If strValue = "" Then
                            strValue = "("
                        Else
                            strValue += "("
                        End If
                    Next
                    objDataRow.Item(Xydc.Platform.Common.Data.QueryData.FIELD_CX_B_CHAXUNTIAOJIAN_ZKHZ) = Me.txtZKHZ.Text
                    objDataRow.Item(Xydc.Platform.Common.Data.QueryData.FIELD_CX_B_CHAXUNTIAOJIAN_ZKHM) = strValue
                    '==========================================================================================================
                    intCount = CType(Me.txtYKHZ.Text, Integer)
                    strValue = ""
                    For i = 0 To intCount - 1 Step 1
                        If strValue = "" Then
                            strValue = ")"
                        Else
                            strValue += ")"
                        End If
                    Next
                    objDataRow.Item(Xydc.Platform.Common.Data.QueryData.FIELD_CX_B_CHAXUNTIAOJIAN_YKHZ) = Me.txtYKHZ.Text
                    objDataRow.Item(Xydc.Platform.Common.Data.QueryData.FIELD_CX_B_CHAXUNTIAOJIAN_YKHM) = strValue
                    '==========================================================================================================
                    objDataRow.Item(Xydc.Platform.Common.Data.QueryData.FIELD_CX_B_CHAXUNTIAOJIAN_ZDMC) = Me.lstField.Items(Me.lstField.SelectedIndex).Value.Trim
                    objDataRow.Item(Xydc.Platform.Common.Data.QueryData.FIELD_CX_B_CHAXUNTIAOJIAN_VAL1) = Me.txtVal1.Text
                    objDataRow.Item(Xydc.Platform.Common.Data.QueryData.FIELD_CX_B_CHAXUNTIAOJIAN_VAL2) = Me.txtVal2.Text
                    '==========================================================================================================
                    objDataRow.Item(Xydc.Platform.Common.Data.QueryData.FIELD_CX_B_CHAXUNTIAOJIAN_BJFM) = Me.rblBJF.Items(Me.rblBJF.SelectedIndex).Text.Trim
                    objDataRow.Item(Xydc.Platform.Common.Data.QueryData.FIELD_CX_B_CHAXUNTIAOJIAN_BJFZ) = Me.rblBJF.Items(Me.rblBJF.SelectedIndex).Value.Trim
                    '==========================================================================================================
                    objDataRow.Item(Xydc.Platform.Common.Data.QueryData.FIELD_CX_B_CHAXUNTIAOJIAN_LJFM) = Me.rblLJF.Items(Me.rblLJF.SelectedIndex).Text.Trim
                    objDataRow.Item(Xydc.Platform.Common.Data.QueryData.FIELD_CX_B_CHAXUNTIAOJIAN_LJFZ) = Me.rblLJF.Items(Me.rblLJF.SelectedIndex).Value.Trim
                    '==========================================================================================================
                    intTypeCode = System.Type.GetTypeCode(objDataColumn.DataType)
                    objDataRow.Item(Xydc.Platform.Common.Data.QueryData.FIELD_CX_B_CHAXUNTIAOJIAN_ZDLX) = intTypeCode.ToString()
                    '==========================================================================================================
                End With

                '重新显示网格
                If Me.showModuleData_TJ(strErrMsg) = False Then
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

        Private Sub doDelAll(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '获取条件数据
                If Me.getModuleData_TJ(strErrMsg) = False Then
                    GoTo errProc
                End If

                '清除
                With Me.m_objDataSet_TJ.Tables(Xydc.Platform.Common.Data.QueryData.TABLE_CX_B_CHAXUNTIAOJIAN)
                    .Rows.Clear()
                End With

                '重新显示网格
                If Me.showModuleData_TJ(strErrMsg) = False Then
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

        Private Sub doDelete(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '获取条件数据
                If Me.getModuleData_TJ(strErrMsg) = False Then
                    GoTo errProc
                End If

                '逐个删除
                Dim blnSelected As Boolean
                Dim intRecPos As Integer
                Dim intCount As Integer
                Dim i As Integer
                intCount = Me.grdTJ.Items.Count
                For i = intCount - 1 To 0 Step -1
                    blnSelected = objDataGridProcess.isDataGridItemChecked(Me.grdTJ.Items(i), 0, Me.m_cstrCheckBoxIdInDataGrid_TJ)
                    If blnSelected = True Then
                        '定位
                        intRecPos = objDataGridProcess.getRecordPosition(i, Me.grdTJ.CurrentPageIndex, Me.grdTJ.PageSize)
                        With Me.m_objDataSet_TJ.Tables(Xydc.Platform.Common.Data.QueryData.TABLE_CX_B_CHAXUNTIAOJIAN)
                            .DefaultView.Item(intRecPos).Row.Delete()
                        End With
                    End If
                Next

                '重新显示网格
                If Me.showModuleData_TJ(strErrMsg) = False Then
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

        Private Sub doCancel(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String
            Dim intStep As Integer

            Try
                '询问
                intStep = 1
                If objMessageProcess.isExecuteCode(Request, Me.popMessageObject.UniqueID, intStep) = True Then
                    objMessageProcess.doConfirmMessage(Me.popMessageObject, "警告：您确定要取消录入的内容吗（是/否）？", strControlId, intStep)
                    Exit Try
                Else
                    objMessageProcess.doResetPopMessage(Me.popMessageObject)
                End If

                '返回处理
                intStep = 2
                If objMessageProcess.isExecuteCode(Request, Me.popMessageObject.UniqueID, intStep) = True Then
                    '设置返回参数
                    Me.m_objInterface.oExitMode = False

                    '释放模块资源
                    Me.releaseModuleParameters()
                    Me.releaseInterfaceParameters()

                    '返回到调用模块，并附加返回参数
                    '要返回的SessionId
                    Dim strSessionId As String
                    strSessionId = Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.ISessionId)

                    'SessionId附加到返回的Url
                    Dim strUrl As String
                    strUrl = Me.m_objInterface.getReturnUrl(Server, Xydc.Platform.Common.Utilities.PulicParameters.OSessionId, strSessionId)

                    '返回
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

        '----------------------------------------------------------------
        ' 检查输入的查询条件是否合法？
        '     strErrMsg      ：返回错误信息
        '     blnValid       ：=true合法，=false非法
        '     strQuery       ：blnValid=true时返回的查询条件
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function doValidateCondition( _
            ByRef strErrMsg As String, _
            ByRef blnValid As Boolean, _
            ByRef strQuery As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim strWhere As String = ""

            doValidateCondition = False
            blnValid = False
            strQuery = ""

            Try
                '检查
                Dim intCount As Integer = 0
                Dim intVLF As Integer = 0
                Dim intVRT As Integer = 0
                Dim intLF As Integer = 0
                Dim intRT As Integer = 0
                Dim i As Integer
                With Me.m_objDataSet_TJ.Tables(Xydc.Platform.Common.Data.QueryData.TABLE_CX_B_CHAXUNTIAOJIAN)
                    intCount = .Rows.Count
                    For i = 0 To intCount - 1 Step 1
                        intVLF = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.QueryData.FIELD_CX_B_CHAXUNTIAOJIAN_ZKHZ), 0)
                        intVRT = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.QueryData.FIELD_CX_B_CHAXUNTIAOJIAN_YKHZ), 0)
                        intLF += intVLF
                        intRT += intVRT
                    Next
                End With
                If intLF <> intRT Then
                    strErrMsg = "错误：左括弧数目与右括弧数目不匹配！"
                    GoTo errProc
                End If

                '复合条件
                Dim enumFieldType As System.TypeCode
                Dim intFieldType As Integer = 0
                Dim strTempWhere As String = ""
                Dim strLastLJF As String = ""
                Dim strValue1 As String = ""
                Dim strValue2 As String = ""
                Dim strField As String = ""
                Dim strZKH As String = ""
                Dim strYKH As String = ""
                Dim strBJF As String = ""
                Dim strLJF As String = ""
                With Me.m_objDataSet_TJ.Tables(Xydc.Platform.Common.Data.QueryData.TABLE_CX_B_CHAXUNTIAOJIAN)
                    intCount = .Rows.Count
                    For i = 0 To intCount - 1 Step 1
                        strTempWhere = ""
                        '获取条件信息
                        strValue1 = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.QueryData.FIELD_CX_B_CHAXUNTIAOJIAN_VAL1), "", True)
                        strValue2 = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.QueryData.FIELD_CX_B_CHAXUNTIAOJIAN_VAL2), "")
                        strField = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.QueryData.FIELD_CX_B_CHAXUNTIAOJIAN_ZDMC), "")
                        strZKH = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.QueryData.FIELD_CX_B_CHAXUNTIAOJIAN_ZKHM), "")
                        strYKH = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.QueryData.FIELD_CX_B_CHAXUNTIAOJIAN_YKHM), "")
                        strBJF = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.QueryData.FIELD_CX_B_CHAXUNTIAOJIAN_BJFZ), "")
                        strLJF = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.QueryData.FIELD_CX_B_CHAXUNTIAOJIAN_LJFZ), "")
                        intFieldType = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.QueryData.FIELD_CX_B_CHAXUNTIAOJIAN_ZDLX), 0)
                        '获取字段类型码
                        Try
                            enumFieldType = CType(intFieldType, System.TypeCode)
                        Catch ex As Exception
                            strErrMsg = ex.Message
                            GoTo errProc
                        End Try
                        '缺省表前缀为"a."
                        strField = "a." + strField
                        '根据字段类型获取单个查询条件
                        Select Case enumFieldType
                            Case System.TypeCode.String, System.TypeCode.Char
                                Select Case strBJF.ToLower
                                    Case Xydc.Platform.Common.Data.QueryData.COMPARESIGN_LIKE
                                        strValue1 = objPulicParameters.getNewSearchString(strValue1)
                                        strTempWhere = strZKH + strField + " " + strBJF + " '" + strValue1.Trim + "%'" + strYKH
                                    Case Xydc.Platform.Common.Data.QueryData.COMPARESIGN_NOTLIKE
                                        strValue1 = objPulicParameters.getNewSearchString(strValue1)
                                        strTempWhere = strZKH + "not (" + strField + " " + " like " + " '" + strValue1.Trim + "%')" + strYKH
                                    Case Else
                                        strTempWhere = strZKH + strField + " " + strBJF + " '" + strValue1 + "'" + strYKH
                                End Select
                            Case System.TypeCode.DateTime
                                Select Case strBJF.ToLower
                                    Case Xydc.Platform.Common.Data.QueryData.COMPARESIGN_BETWEEN
                                        strTempWhere = strZKH + strField + " " + strBJF + " '" + strValue1.Trim + "' and '" + strValue2.Trim + "'" + strYKH
                                    Case Else
                                        strTempWhere = strZKH + strField + " " + strBJF + " '" + strValue1 + "'" + strYKH
                                End Select
                            Case System.TypeCode.Byte, _
                                System.TypeCode.Int16, System.TypeCode.Int32, System.TypeCode.Int64, _
                                System.TypeCode.UInt16, System.TypeCode.UInt32, System.TypeCode.UInt64, _
                                System.TypeCode.Decimal, System.TypeCode.Double, System.TypeCode.Single
                                Select Case strBJF.ToLower
                                    Case Xydc.Platform.Common.Data.QueryData.COMPARESIGN_BETWEEN
                                        strTempWhere = strZKH + strField + " " + strBJF + " " + strValue1.Trim + " and " + strValue2.Trim + " " + strYKH
                                    Case Else
                                        strTempWhere = strZKH + strField + " " + strBJF + " " + strValue1 + " " + strYKH
                                End Select
                            Case Else
                                strErrMsg = "错误：无效的数据类型！"
                                GoTo errProc
                        End Select
                        '复合
                        If strTempWhere <> "" Then
                            If strWhere = "" Then
                                strWhere = strTempWhere
                                strLastLJF = strLJF
                            Else
                                strWhere = strWhere + " " + strLastLJF + " " + strTempWhere
                                strLastLJF = strLJF
                            End If
                        End If
                    Next
                    '复合固定查询条件
                    If Me.m_objInterface.iFixQuery <> "" Then
                        If strWhere = "" Then
                            strWhere = "(" + Me.m_objInterface.iFixQuery + ")"
                        Else
                            strWhere = "(" + strWhere + ")" + " and (" + Me.m_objInterface.iFixQuery + ")"
                        End If
                    End If
                End With

                '返回
                strQuery = strWhere
                blnValid = True

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            doValidateCondition = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function
        End Function

        Private Sub doConfirm(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '获取条件数据
                If Me.getModuleData_TJ(strErrMsg) = False Then
                    GoTo errProc
                End If

                '检查条件正确性
                Dim blnValid As Boolean
                Dim strQuery As String
                If Me.doValidateCondition(strErrMsg, blnValid, strQuery) = False Then
                    GoTo errProc
                End If

                '设置返回参数
                Session(Me.htxtSessionIDTJ.Value) = Me.m_objInterface.iDataSetTJ '需要释放的资源
                With Me.m_objInterface
                    .oDataSetTJ = Me.m_objDataSet_TJ
                    .oQueryString = strQuery
                    .oExitMode = True
                End With

                '释放模块资源
                Me.releaseModuleParameters()
                Me.releaseInterfaceParameters()

                '返回到调用模块，并附加返回参数
                '要返回的SessionId
                Dim strSessionId As String
                strSessionId = Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.ISessionId)

                'SessionId附加到返回的Url
                Dim strUrl As String
                strUrl = Me.m_objInterface.getReturnUrl(Server, Xydc.Platform.Common.Utilities.PulicParameters.OSessionId, strSessionId)

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

        Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
            Me.doCancel("btnCancel")
        End Sub

        Private Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.Click
            Me.doConfirm("btnOK")
        End Sub

        Private Sub btnAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
            Me.doAddNew("btnAddNew")
        End Sub

        Private Sub btnModify_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnModify.Click
            Me.doModify("btnModify")
        End Sub

        Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
            Me.doDelete("btnDelete")
        End Sub

        Private Sub btnDelAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelAll.Click
            Me.doDelAll("btnDelAll")
        End Sub

    End Class
End Namespace