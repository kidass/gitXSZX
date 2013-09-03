Imports System

Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.web
    ' 类名    ：DataGridProcess
    '
    ' 功能描述：
    '     DataGrid对象的有关处理
    '----------------------------------------------------------------

    Public Class DataGridProcess
        Implements IDisposable









        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
        End Sub

        '----------------------------------------------------------------
        ' 虚拟析构函数
        '----------------------------------------------------------------
        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(True)
        End Sub

        '----------------------------------------------------------------
        ' 析构函数重载
        '----------------------------------------------------------------
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If (Not disposing) Then
                Exit Sub
            End If
        End Sub

        '----------------------------------------------------------------
        ' 安全释放本身资源
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.web.DataGridProcess)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub









        '----------------------------------------------------------------
        ' 检查PageIndex,RowIndex是否是合法的参数
        '     objDataGrid   ：DataGrid对象
        '     intRows       ：总行数
        '     intPageIndex  ：准备要显示的页
        '     intRowIndex   ：准备要显示的行
        '----------------------------------------------------------------
        Public Sub doCheckDataGridIndex( _
            ByVal objDataGrid As System.Web.UI.WebControls.DataGrid, _
            ByVal intRows As Integer, _
            ByRef intPageIndex As Integer, _
            ByRef intRowIndex As Integer)

            Try
                '检查PageIndex
                If intPageIndex >= objDataGrid.PageCount Then
                    intPageIndex = objDataGrid.PageCount - 1
                End If
                If intPageIndex < 0 Then
                    intPageIndex = 0
                End If

                '检查RowIndex
                If intRowIndex >= objDataGrid.PageSize Then
                    intRowIndex = objDataGrid.PageSize - 1
                End If
                If intRowIndex < 0 Then
                    intRowIndex = 0
                End If
                '最后1页
                If intPageIndex = objDataGrid.PageCount - 1 Then
                    '计算剩余行数
                    Dim intHas As Integer
                    intHas = intRows - intPageIndex * objDataGrid.PageSize
                    If intRowIndex >= intHas Then
                        intRowIndex = intHas - 1
                    End If
                End If
                '没有记录
                If intRows = 0 Then
                    intRowIndex = -1
                End If
            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' 检查PageIndex,RowIndex是否是合法的参数
        '     intRows       ：总行数
        '     blnAllowPaging：允许分页
        '     intPageSize   ：页面大小
        '     intPageIndex  ：准备要显示的页
        '     intRowIndex   ：准备要显示的行
        '----------------------------------------------------------------
        Public Sub doCheckDataGridIndex( _
            ByVal intRows As Integer, _
            ByVal blnAllowPaging As Boolean, _
            ByVal intPageSize As Integer, _
            ByRef intPageIndex As Integer, _
            ByRef intRowIndex As Integer)

            Dim intPageCount As Integer
            Try
                '没有记录
                If intRows = 0 Then
                    intPageIndex = 0
                    intRowIndex = -1
                    Exit Try
                End If

                '获取页面数
                If blnAllowPaging = False Then
                    intPageSize = intRows
                End If
                If (intRows Mod intPageSize) = 0 Then
                    intPageCount = CType(Fix(intRows / intPageSize), Integer)
                Else
                    intPageCount = CType(Fix(intRows / intPageSize), Integer) + 1
                End If

                '检查PageIndex
                If intPageCount = 0 Then
                    intPageIndex = 0
                Else
                    If intPageIndex >= intPageCount Then
                        intPageIndex = intPageCount - 1
                    End If
                    If intPageIndex < 0 Then
                        intPageIndex = 0
                    End If
                End If

                '检查RowIndex
                '没有记录
                If intRowIndex >= intPageSize Then
                    intRowIndex = intPageSize - 1
                End If
                If intRowIndex < 0 Then
                    intRowIndex = 0
                End If

                '最后1页
                If intPageIndex = intPageCount - 1 Then
                    '计算剩余行数
                    Dim intHas As Integer
                    intHas = intRows - intPageIndex * intPageSize
                    If intRowIndex >= intHas Then
                        intRowIndex = intHas - 1
                    End If
                End If
            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' 获取DataGrid的定位信息字符串
        '     objDataGrid   ：DataGrid对象
        '     intRows       ：总行数
        '----------------------------------------------------------------
        Public Function getDataGridLocation( _
            ByVal objDataGrid As System.Web.UI.WebControls.DataGrid, _
            ByVal intRows As Integer) As String

            Dim strValue As String = ""
            Try
                If intRows = 0 Then
                    getDataGridLocation = "N/N页 N/N行"
                Else
                    strValue += (objDataGrid.CurrentPageIndex + 1).ToString()
                    strValue += "/"
                    strValue += (objDataGrid.PageCount).ToString()
                    strValue += "页 "
                    strValue += (objDataGrid.CurrentPageIndex * objDataGrid.PageSize + objDataGrid.SelectedIndex + 1).ToString()
                    strValue += "/"
                    strValue += (intRows).ToString()
                    strValue += "行"
                    getDataGridLocation = strValue
                End If
            Catch ex As Exception
                getDataGridLocation = "N/N页 N/N行"
            End Try

        End Function

        '----------------------------------------------------------------
        ' System.Web.UI.WebControls.ButtonColumn 版本
        ' 根据DataTable的列信息自动生成DataGrid的ButtonColumns列信息
        ' 采用添加方式，不清除现有列
        '     strErrMsg      ：返回错误信息
        '     objDataGrid    ：DataGrid对象
        '     objDataTable   ：DataTable对象
        '     objButtonColumn：添加的列为System.Web.UI.WebControls.ButtonColumn
        '     strCommandName ：列的CommandName(select,etc)
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Public Function doGenrateDataGridColumns( _
            ByRef strErrMsg As String, _
            ByVal objDataGrid As System.Web.UI.WebControls.DataGrid, _
            ByVal objDataTable As System.Data.DataTable, _
            ByVal objButtonColumn As System.Web.UI.WebControls.ButtonColumn, _
            ByVal strCommandName As String) As Boolean

            doGenrateDataGridColumns = False

            Try
                Dim intCount As Integer
                Dim i As Integer
                intCount = objDataTable.Columns.Count
                For i = 0 To intCount - 1 Step 1
                    objButtonColumn = New System.Web.UI.WebControls.ButtonColumn
                    With objButtonColumn
                        .ButtonType = ButtonColumnType.LinkButton
                        .CommandName = strCommandName
                        .DataTextField = objDataTable.Columns(i).ColumnName
                        .HeaderText = objDataTable.Columns(i).ColumnName
                        .SortExpression = objDataTable.Columns(i).ColumnName
                    End With
                    objDataGrid.Columns.Add(objButtonColumn)
                Next
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doGenrateDataGridColumns = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' System.Web.UI.WebControls.BoundColumn 版本
        ' 根据DataTable的列信息自动生成DataGrid的ButtonColumns列信息
        ' 采用添加方式，不清除现有列
        '     strErrMsg      ：返回错误信息
        '     objDataGrid    ：DataGrid对象
        '     objDataTable   ：DataTable对象
        '     objButtonColumn：添加的列为System.Web.UI.WebControls.ButtonColumn
        '     blnReadOnly    ：列只读
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Public Function doGenrateDataGridColumns( _
            ByRef strErrMsg As String, _
            ByVal objDataGrid As System.Web.UI.WebControls.DataGrid, _
            ByVal objDataTable As System.Data.DataTable, _
            ByVal objBoundColumn As System.Web.UI.WebControls.BoundColumn, _
            ByVal blnReadOnly As Boolean) As Boolean

            doGenrateDataGridColumns = False

            Try
                Dim intCount As Integer
                Dim i As Integer
                intCount = objDataTable.Columns.Count
                For i = 0 To intCount - 1 Step 1
                    objBoundColumn = New System.Web.UI.WebControls.BoundColumn
                    With objBoundColumn
                        .ReadOnly = blnReadOnly
                        .DataField = objDataTable.Columns(i).ColumnName
                        .HeaderText = objDataTable.Columns(i).ColumnName
                        .SortExpression = objDataTable.Columns(i).ColumnName
                    End With
                    objDataGrid.Columns.Add(objBoundColumn)
                Next
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doGenrateDataGridColumns = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据给定的列宽参数字符串设置网格列的列宽，从指定列开始
        '     strErrMsg     ：返回错误信息
        '     objDataGrid   ：DataGrid对象
        '     strColWidth   ：列宽参数，用标准分隔符分隔(32px,30%,etc)
        '     intStartCol   ：开始处理列，缺省=0
        ' 返回
        '     True          ：成功
        '     False         ：失败
        '----------------------------------------------------------------
        Public Function doSetColumnWidth( _
            ByRef strErrMsg As String, _
            ByVal objDataGrid As System.Web.UI.WebControls.DataGrid, _
            ByVal strColWidth As String, _
            ByVal intStartCol As Integer) As Boolean

            doSetColumnWidth = False

            Try
                Dim intCols As Integer
                Dim i As Integer
                intCols = objDataGrid.Columns.Count
                If strColWidth <> "" Then
                    '指定列宽
                    Dim strWidth() As String
                    strWidth = strColWidth.Split(Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate.ToCharArray())
                    For i = intStartCol To intCols - 1 Step 1
                        objDataGrid.Columns(i).HeaderStyle.Width = New System.Web.UI.WebControls.Unit(strWidth(i - intStartCol))
                    Next
                Else
                    '自动列宽
                    For i = intStartCol To intCols - 1 Step 1
                        objDataGrid.Columns(i).HeaderStyle.Width = New System.Web.UI.WebControls.Unit((100 / (intCols - intStartCol)).ToString() + "%")
                    Next
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doSetColumnWidth = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取网格排序的指令
        '     strErrMsg        ：返回错误信息
        '     strOldCommand    ：网格当前排序指令
        '     strNewCommand    ：本次要执行的排序指令
        '     strFinalCommand  ：最终的排序指令输出
        '     objSortType      ：排序指令类型
        ' 返回
        '     True             ：成功
        '     False            ：失败
        '----------------------------------------------------------------
        Public Function getColumnSortCommand( _
            ByRef strErrMsg As String, _
            ByVal strOldCommand As String, _
            ByVal strNewCommand As String, _
            ByRef strFinalCommand As String, _
            ByRef objSortType As Xydc.Platform.Common.Utilities.PulicParameters.enumSortType) As Boolean

            getColumnSortCommand = False

            Try
                If strOldCommand = "" Then
                    '准备升序排列
                    strFinalCommand = strNewCommand + " Asc"
                    objSortType = Xydc.Platform.Common.Utilities.PulicParameters.enumSortType.Asc
                Else
                    If strOldCommand.IndexOf(strNewCommand) >= 0 Then
                        If strOldCommand.IndexOf(" Asc") >= 0 Then
                            '准备降序排列
                            strFinalCommand = strNewCommand + " Desc"
                            objSortType = Xydc.Platform.Common.Utilities.PulicParameters.enumSortType.Desc
                        ElseIf strOldCommand.IndexOf(" Desc") >= 0 Then
                            '准备复原排序
                            strFinalCommand = ""
                            objSortType = Xydc.Platform.Common.Utilities.PulicParameters.enumSortType.None
                        Else
                            '准备升序排列
                            strFinalCommand = strNewCommand + " Asc"
                            objSortType = Xydc.Platform.Common.Utilities.PulicParameters.enumSortType.Asc
                        End If
                    Else
                        '准备升序排列
                        strFinalCommand = strNewCommand + " Asc"
                        objSortType = Xydc.Platform.Common.Utilities.PulicParameters.enumSortType.Asc
                    End If
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getColumnSortCommand = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 清除网格列头的排序字符
        '----------------------------------------------------------------
        Public Sub doClearSortCharInDataGridHead( _
            ByVal objDataGrid As System.Web.UI.WebControls.DataGrid)

            Dim intCount As Integer
            Dim i As Integer

            Try
                intCount = objDataGrid.Columns.Count
                For i = 0 To intCount - 1 Step 1
                    With objDataGrid.Columns(i)
                        .HeaderText = .HeaderText.Replace(Xydc.Platform.Common.Utilities.PulicParameters.CharAsc, "")
                        .HeaderText = .HeaderText.Replace(Xydc.Platform.Common.Utilities.PulicParameters.CharDesc, "")
                        If .HeaderText.Length > 0 Then
                            .HeaderText = .HeaderText.Trim()
                        End If
                    End With
                Next
            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' 根据控件的UniqueId在当前网格行中检索出所在列的列索引
        '     objDataGridItem  ：正在点击的网格行
        '     strUniqueId      ：正在点击的控件的UniqueId
        ' 返回
        '                      ：找到的列索引，未找到或错误=-1
        '----------------------------------------------------------------
        Public Function getColumnIndexByUniqueIdInRow( _
            ByVal objDataGridItem As System.Web.UI.WebControls.DataGridItem, _
            ByVal strUniqueId As String) As Integer

            getColumnIndexByUniqueIdInRow = -1

            '初始化
            If strUniqueId.Length > 0 Then strUniqueId = strUniqueId.Trim()

            '检索
            Try
                Dim intColCount As Integer
                Dim i As Integer
                Dim intCtlCount As Integer
                Dim j As Integer
                intColCount = objDataGridItem.Cells.Count
                For i = 0 To intColCount - 1 Step 1
                    intCtlCount = objDataGridItem.Cells(i).Controls.Count
                    For j = 0 To intCtlCount - 1 Step 1
                        If objDataGridItem.Cells(i).Controls(j).UniqueID = strUniqueId Then
                            getColumnIndexByUniqueIdInRow = i
                            Exit Function
                        End If
                    Next
                Next
            Catch ex As Exception
                getColumnIndexByUniqueIdInRow = -1
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据objSortType计算出排序显示用的字符串
        '     strOldHead       ：现有列标题
        '     objSortType      ：排序指令类型
        ' 返回
        '                      ：带有排序标识的列标题
        '----------------------------------------------------------------
        Public Function getColumnSortHeadString( _
            ByVal strOldHead As String, _
            ByVal objSortType As Xydc.Platform.Common.Utilities.PulicParameters.enumSortType) As String

            Try
                Select Case objSortType
                    Case Common.Utilities.PulicParameters.enumSortType.Asc
                        strOldHead += (" " + Xydc.Platform.Common.Utilities.PulicParameters.CharAsc)
                    Case Common.Utilities.PulicParameters.enumSortType.Desc
                        strOldHead += (" " + Xydc.Platform.Common.Utilities.PulicParameters.CharDesc)
                    Case Common.Utilities.PulicParameters.enumSortType.None
                End Select
                getColumnSortHeadString = strOldHead
            Catch ex As Exception
                getColumnSortHeadString = strOldHead
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据控件的UniqueId在当前网格行中检索出所在列的列索引
        '     strErrMsg        ：返回错误信息
        '     objDataGrid      ：DataGrid对象
        '     objDataGridItem  ：正在点击的网格行
        '     strUniqueId      ：正在点击的控件的UniqueId
        '     objSortType      ：排序指令类型
        '     intColIndex      ：返回设置列的列索引
        ' 返回
        '     True             ：成功
        '     False            ：失败
        '----------------------------------------------------------------
        Public Function doSetSortCharInDataGridHead( _
            ByRef strErrMsg As String, _
            ByVal objDataGrid As System.Web.UI.WebControls.DataGrid, _
            ByVal objDataGridItem As System.Web.UI.WebControls.DataGridItem, _
            ByVal strUniqueId As String, _
            ByVal objSortType As Xydc.Platform.Common.Utilities.PulicParameters.enumSortType, _
            ByRef intColIndex As Integer) As Boolean

            doSetSortCharInDataGridHead = False
            intColIndex = -1

            '检查
            If objDataGrid Is Nothing Then
                GoTo normExit
            End If
            If objDataGridItem Is Nothing Then
                GoTo normExit
            End If
            If strUniqueId.Length > 0 Then strUniqueId = strUniqueId.Trim()
            If strUniqueId = "" Then
                GoTo normExit
            End If

            Dim intTempColIndex As Integer
            Try
                '获取当前列索引
                intTempColIndex = getColumnIndexByUniqueIdInRow(objDataGridItem, strUniqueId)
                If intTempColIndex < 0 Then
                    GoTo normExit
                End If

                '重置网格列头
                Me.doClearSortCharInDataGridHead(objDataGrid)

                '根据排序进行设置
                With objDataGrid.Columns(intTempColIndex)
                    .HeaderText = getColumnSortHeadString(.HeaderText, objSortType)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            '返回列索引
            intColIndex = intTempColIndex
normExit:
            doSetSortCharInDataGridHead = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据网格现参数判断是否可以进行“首页”操作
        '     objDataGrid      ：DataGrid对象
        '     intRowCount      ：网格数据的总行数
        ' 返回
        '     True             ：能
        '     False            ：不能
        '----------------------------------------------------------------
        Public Function canDoMoveFirstPage( _
            ByVal objDataGrid As System.Web.UI.WebControls.DataGrid, _
            ByVal intRowCount As Integer) As Boolean

            canDoMoveFirstPage = False
            Try
                '没有记录
                If intRowCount < 1 Then
                    Exit Try
                End If
                '仅有1页
                If objDataGrid.PageCount = 1 Then
                    Exit Try
                End If
                '是首页
                If objDataGrid.CurrentPageIndex = 0 Then
                    Exit Try
                End If
                '其他都可以
                canDoMoveFirstPage = True
            Catch ex As Exception
                canDoMoveFirstPage = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据网格现参数判断是否可以进行“尾页”操作
        '     objDataGrid      ：DataGrid对象
        '     intRowCount      ：网格数据的总行数
        ' 返回
        '     True             ：能
        '     False            ：不能
        '----------------------------------------------------------------
        Public Function canDoMoveLastPage( _
            ByVal objDataGrid As System.Web.UI.WebControls.DataGrid, _
            ByVal intRowCount As Integer) As Boolean

            canDoMoveLastPage = False
            Try
                '没有记录
                If intRowCount < 1 Then
                    Exit Try
                End If
                '仅有1页
                If objDataGrid.PageCount = 1 Then
                    Exit Try
                End If
                '是尾页
                If objDataGrid.CurrentPageIndex = objDataGrid.PageCount - 1 Then
                    Exit Try
                End If
                '其他都可以
                canDoMoveLastPage = True
            Catch ex As Exception
                canDoMoveLastPage = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据网格现参数判断是否可以进行“上页”操作
        '     objDataGrid      ：DataGrid对象
        '     intRowCount      ：网格数据的总行数
        ' 返回
        '     True             ：能
        '     False            ：不能
        '----------------------------------------------------------------
        Public Function canDoMovePreviousPage( _
            ByVal objDataGrid As System.Web.UI.WebControls.DataGrid, _
            ByVal intRowCount As Integer) As Boolean

            canDoMovePreviousPage = False
            Try
                '没有记录
                If intRowCount < 1 Then
                    Exit Try
                End If
                '仅有1页
                If objDataGrid.PageCount = 1 Then
                    Exit Try
                End If
                '是首页
                If objDataGrid.CurrentPageIndex = 0 Then
                    Exit Try
                End If
                '其他都可以
                canDoMovePreviousPage = True
            Catch ex As Exception
                canDoMovePreviousPage = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 移动到指定页，返回有效页索引
        '     intToPage        ：准备显示的页
        '     intTotalPages    ：总页数
        ' 返回
        '                      ：有效页索引
        '----------------------------------------------------------------
        Public Function doMoveToPage( _
            ByVal intToPage As Integer, _
            ByVal intTotalPages As Integer) As Integer

            doMoveToPage = 0
            Try
                '到最后1页
                If intToPage < 0 Then
                    doMoveToPage = intTotalPages - 1
                    Exit Try
                End If

                '到第1页
                If intToPage >= intTotalPages Then
                    doMoveToPage = 0
                    Exit Try
                End If

                '到指定页
                doMoveToPage = intToPage

            Catch ex As Exception
                doMoveToPage = 0
            End Try

        End Function

        '----------------------------------------------------------------
        ' 移动到指定记录，返回有效页索引和行索引
        '     blnAllowPaging   ：允许分页
        '     intPageSize      ：页面大小
        '     intRecordNo      ：记录号(从0开始)
        '     intPageIndex     ：返回页索引
        '     intSelectIndex   ：返回行索引
        ' 返回
        '     True             ：成功
        '     False            ：失败
        '----------------------------------------------------------------
        Public Function doMoveToRecord( _
            ByVal blnAllowPaging As Boolean, _
            ByVal intPageSize As Integer, _
            ByVal intRecordNo As Integer, _
            ByRef intPageIndex As Integer, _
            ByRef intSelectIndex As Integer) As Boolean

            Try
                If blnAllowPaging = False Then
                    '不分页
                    intPageIndex = 0
                    intSelectIndex = intRecordNo
                Else
                    '分页
                    intPageIndex = CType(Fix(intRecordNo / intPageSize), Integer)
                    intSelectIndex = intRecordNo - intPageIndex * intPageSize
                End If

                If intSelectIndex < 0 Then
                    intPageIndex = 0
                    intSelectIndex = -1
                End If
                doMoveToRecord = True
            Catch ex As Exception
                intPageIndex = 0
                intSelectIndex = -1
                doMoveToRecord = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据网格现参数判断是否可以进行“下页”操作
        '     objDataGrid      ：DataGrid对象
        '     intRowCount      ：网格数据的总行数
        ' 返回
        '     True             ：能
        '     False            ：不能
        '----------------------------------------------------------------
        Public Function canDoMoveNextPage( _
            ByVal objDataGrid As System.Web.UI.WebControls.DataGrid, _
            ByVal intRowCount As Integer) As Boolean

            canDoMoveNextPage = False
            Try
                '没有记录
                If intRowCount < 1 Then
                    Exit Try
                End If
                '仅有1页
                If objDataGrid.PageCount = 1 Then
                    Exit Try
                End If
                '是尾页
                If objDataGrid.CurrentPageIndex = objDataGrid.PageCount - 1 Then
                    Exit Try
                End If
                '其他都可以
                canDoMoveNextPage = True
            Catch ex As Exception
                canDoMoveNextPage = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据Request中的信息恢复网格指定列中的CheckBox状态
        '     strErrMsg        ：返回错误信息
        '     objDataGrid      ：DataGrid对象
        '     objHttpRequest   ：当前HttpRequest
        '     intColIndex      ：CheckBox所在列
        '     strCheckBoxId    ：CheckBox控件ID
        ' 返回
        '     True             ：成功
        '     False            ：失败
        '----------------------------------------------------------------
        Public Function doRestoreDataGridCheckBoxStatus( _
            ByRef strErrMsg As String, _
            ByVal objDataGrid As System.Web.UI.WebControls.DataGrid, _
            ByVal objHttpRequest As System.Web.HttpRequest, _
            ByVal intColIndex As Integer, _
            ByVal strCheckBoxId As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objCheckBox As System.Web.UI.WebControls.CheckBox
            Dim objControl As System.Web.UI.Control
            Dim intRowCount As Integer
            Dim blnSelect As Boolean
            Dim i As Integer

            doRestoreDataGridCheckBoxStatus = False

            Try
                intRowCount = objDataGrid.Items.Count
                For i = 0 To intRowCount - 1 Step 1
                    objControl = Nothing
                    objControl = objDataGrid.Items(i).Cells(intColIndex).FindControl(strCheckBoxId)
                    If Not (objControl Is Nothing) Then
                        objCheckBox = Nothing
                        objCheckBox = CType(objControl, System.Web.UI.WebControls.CheckBox)
                        If Not (objCheckBox Is Nothing) Then
                            'checkbox选择状态记录在objHttpRequestform中，值on=checked
                            '每选择一次，服务器向客户端当前窗口发送新信息
                            Dim strControlValue As String
                            strControlValue = objHttpRequest.Form(objControl.UniqueID)
                            If strControlValue = objPulicParameters.CheckBoxCheckedValue Then
                                blnSelect = True
                            Else
                                blnSelect = False
                            End If
                            If blnSelect = True Then
                                objCheckBox.Checked = True
                            End If
                        End If
                    End If
                Next
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            doRestoreDataGridCheckBoxStatus = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据Request中的信息恢复网格指定列中的CheckBox状态
        '     strErrMsg        ：返回错误信息
        '     objDataGrid      ：DataGrid对象
        '     intColIndex      ：CheckBox所在列
        '     strCheckBoxId    ：CheckBox控件ID
        '     blnChecked       ：行CheckBox状态
        ' 返回
        '     True             ：成功
        '     False            ：失败
        '----------------------------------------------------------------
        Public Function doRestoreDataGridCheckBoxStatus( _
            ByRef strErrMsg As String, _
            ByVal objDataGrid As System.Web.UI.WebControls.DataGrid, _
            ByVal intColIndex As Integer, _
            ByVal strCheckBoxId As String, _
            ByVal blnChecked() As Boolean) As Boolean

            Dim objCheckBox As System.Web.UI.WebControls.CheckBox
            Dim objControl As System.Web.UI.Control
            Dim intRowCount As Integer
            Dim blnSelect As Boolean
            Dim i As Integer

            doRestoreDataGridCheckBoxStatus = False

            Try
                If blnChecked Is Nothing Then
                    Exit Try
                End If

                intRowCount = objDataGrid.Items.Count
                For i = 0 To intRowCount - 1 Step 1
                    objControl = Nothing
                    objControl = objDataGrid.Items(i).Cells(intColIndex).FindControl(strCheckBoxId)
                    If Not (objControl Is Nothing) Then
                        objCheckBox = Nothing
                        objCheckBox = CType(objControl, System.Web.UI.WebControls.CheckBox)
                        If Not (objCheckBox Is Nothing) Then
                            If i < blnChecked.Length Then
                                objCheckBox.Checked = blnChecked(i)
                            End If
                        End If
                    End If
                Next
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doRestoreDataGridCheckBoxStatus = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 备份网格指定列中的CheckBox状态
        '     strErrMsg        ：返回错误信息
        '     objDataGrid      ：DataGrid对象
        '     intColIndex      ：CheckBox所在列
        '     strCheckBoxId    ：CheckBox控件ID
        '     blnChecked       ：(返回)行CheckBox状态
        ' 返回
        '     True             ：成功
        '     False            ：失败
        '----------------------------------------------------------------
        Public Function doBackupDataGridCheckBoxStatus( _
            ByRef strErrMsg As String, _
            ByVal objDataGrid As System.Web.UI.WebControls.DataGrid, _
            ByVal intColIndex As Integer, _
            ByVal strCheckBoxId As String, _
            ByRef blnChecked() As Boolean) As Boolean

            Dim objCheckBox As System.Web.UI.WebControls.CheckBox
            Dim objControl As System.Web.UI.Control
            Dim intRowCount As Integer
            Dim blnSelect As Boolean
            Dim i As Integer

            doBackupDataGridCheckBoxStatus = False
            blnChecked = Nothing

            Try
                intRowCount = objDataGrid.Items.Count
                Dim blnValue(intRowCount) As Boolean

                For i = 0 To intRowCount - 1 Step 1
                    objControl = Nothing
                    objControl = objDataGrid.Items(i).Cells(intColIndex).FindControl(strCheckBoxId)
                    If Not (objControl Is Nothing) Then
                        objCheckBox = Nothing
                        objCheckBox = CType(objControl, System.Web.UI.WebControls.CheckBox)
                        If Not (objCheckBox Is Nothing) Then
                            blnValue(i) = objCheckBox.Checked
                        End If
                    End If
                Next

                blnChecked = blnValue

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doBackupDataGridCheckBoxStatus = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 使能网格指定列中的CheckBox的使能状态
        '     strErrMsg        ：返回错误信息
        '     objDataGrid      ：DataGrid对象
        '     intColIndex      ：CheckBox所在列
        '     strCheckBoxId    ：CheckBox控件ID
        '     blnEnabled       ：Enabled
        ' 返回
        '     True             ：成功
        '     False            ：失败
        '----------------------------------------------------------------
        Public Function doEnableDataGridCheckBox( _
            ByRef strErrMsg As String, _
            ByVal objDataGrid As System.Web.UI.WebControls.DataGrid, _
            ByVal intColIndex As Integer, _
            ByVal strCheckBoxId As String, _
            ByVal blnEnabled As Boolean) As Boolean

            Dim objCheckBox As System.Web.UI.WebControls.CheckBox
            Dim objControl As System.Web.UI.Control
            Dim intRowCount As Integer
            Dim i As Integer

            doEnableDataGridCheckBox = False

            Try
                intRowCount = objDataGrid.Items.Count
                For i = 0 To intRowCount - 1 Step 1
                    objControl = Nothing
                    objControl = objDataGrid.Items(i).Cells(intColIndex).FindControl(strCheckBoxId)
                    If Not (objControl Is Nothing) Then
                        objCheckBox = Nothing
                        objCheckBox = CType(objControl, System.Web.UI.WebControls.CheckBox)
                        If Not (objCheckBox Is Nothing) Then
                            objCheckBox.Enabled = blnEnabled
                        End If
                    End If
                Next
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doEnableDataGridCheckBox = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 设置网格指定列中的CheckBox的Checked状态
        '     strErrMsg        ：返回错误信息
        '     objDataGrid      ：DataGrid对象
        '     intColIndex      ：CheckBox所在列
        '     strCheckBoxId    ：CheckBox控件ID
        '     blnChecked       ：Checked
        ' 返回
        '     True             ：成功
        '     False            ：失败
        '----------------------------------------------------------------
        Public Function doCheckedDataGridCheckBox( _
            ByRef strErrMsg As String, _
            ByVal objDataGrid As System.Web.UI.WebControls.DataGrid, _
            ByVal intColIndex As Integer, _
            ByVal strCheckBoxId As String, _
            ByVal blnChecked As Boolean) As Boolean

            Dim objCheckBox As System.Web.UI.WebControls.CheckBox
            Dim objControl As System.Web.UI.Control
            Dim intRowCount As Integer
            Dim i As Integer

            doCheckedDataGridCheckBox = False

            Try
                intRowCount = objDataGrid.Items.Count
                For i = 0 To intRowCount - 1 Step 1
                    objControl = Nothing
                    objControl = objDataGrid.Items(i).Cells(intColIndex).FindControl(strCheckBoxId)
                    If Not (objControl Is Nothing) Then
                        objCheckBox = Nothing
                        objCheckBox = CType(objControl, System.Web.UI.WebControls.CheckBox)
                        If Not (objCheckBox Is Nothing) Then
                            objCheckBox.Checked = blnChecked
                        End If
                    End If
                Next
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doCheckedDataGridCheckBox = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 设置网格指定列中的CheckBox的Checked状态
        '     strErrMsg        ：返回错误信息
        '     objDataGrid      ：DataGrid对象
        '     intRowIndex      ：CheckBox所在列
        '     intColIndex      ：CheckBox所在列
        '     strCheckBoxId    ：CheckBox控件ID
        '     blnChecked       ：Checked
        ' 返回
        '     True             ：成功
        '     False            ：失败

        '----------------------------------------------------------------
        Public Function doCheckedDataGridCheckBox( _
            ByRef strErrMsg As String, _
            ByVal objDataGrid As System.Web.UI.WebControls.DataGrid, _
            ByVal intRowIndex As Integer, _
            ByVal intColIndex As Integer, _
            ByVal strCheckBoxId As String, _
            ByVal blnChecked As Boolean) As Boolean

            Dim objCheckBox As System.Web.UI.WebControls.CheckBox
            Dim objControl As System.Web.UI.Control
            Dim intRowCount As Integer
            Dim i As Integer

            doCheckedDataGridCheckBox = False

            Try
                objControl = Nothing
                objControl = objDataGrid.Items(intRowIndex).Cells(intColIndex).FindControl(strCheckBoxId)
                If Not (objControl Is Nothing) Then
                    objCheckBox = Nothing
                    objCheckBox = CType(objControl, System.Web.UI.WebControls.CheckBox)
                    If Not (objCheckBox Is Nothing) Then
                        objCheckBox.Checked = blnChecked
                    End If
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doCheckedDataGridCheckBox = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 检查指定网格行的指定列中的CheckBox的Checked状态
        '     objDataGridItem  ：当前行DataGridItem
        '     intColIndex      ：CheckBox所在列
        '     strCheckBoxId    ：CheckBox控件ID
        ' 返回
        '     True             ：Checked
        '     False            ：Unchecked
        '----------------------------------------------------------------
        Public Function isDataGridItemChecked( _
            ByVal objDataGridItem As System.Web.UI.WebControls.DataGridItem, _
            ByVal intColIndex As Integer, _
            ByVal strCheckBoxId As String) As Boolean

            Dim objCheckBox As System.Web.UI.WebControls.CheckBox
            Dim objControl As System.Web.UI.Control

            isDataGridItemChecked = False
            Try
                objControl = Nothing
                objControl = objDataGridItem.Cells(intColIndex).FindControl(strCheckBoxId)
                If Not (objControl Is Nothing) Then
                    objCheckBox = Nothing
                    objCheckBox = CType(objControl, System.Web.UI.WebControls.CheckBox)
                    If Not (objCheckBox Is Nothing) Then
                        isDataGridItemChecked = objCheckBox.Checked
                    End If
                End If
            Catch ex As Exception
            End Try

        End Function

        '----------------------------------------------------------------
        ' 设置指定网格行的指定列中的CheckBox的Checked状态
        '     objDataGridItem  ：当前行DataGridItem
        '     intColIndex      ：CheckBox所在列
        '     strCheckBoxId    ：CheckBox控件ID
        ' 返回
        '     True             ：成功
        '     False            ：失败
        '----------------------------------------------------------------
        Public Function doSetDataGridItemChecked( _
            ByVal objDataGridItem As System.Web.UI.WebControls.DataGridItem, _
            ByVal intColIndex As Integer, _
            ByVal strCheckBoxId As String, _
            ByVal blnChecked As Boolean) As Boolean

            Dim objCheckBox As System.Web.UI.WebControls.CheckBox
            Dim objControl As System.Web.UI.Control

            doSetDataGridItemChecked = False

            Try
                objControl = Nothing
                objControl = objDataGridItem.Cells(intColIndex).FindControl(strCheckBoxId)
                If Not (objControl Is Nothing) Then
                    objCheckBox = Nothing
                    objCheckBox = CType(objControl, System.Web.UI.WebControls.CheckBox)
                    If Not (objCheckBox Is Nothing) Then
                        objCheckBox.Checked = blnChecked
                    End If
                End If
            Catch ex As Exception
            End Try

            doSetDataGridItemChecked = True
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据网格当前行intRowIndex、当前页intPageIndex、页记录数intPageSize
        ' 计算对应的DataView中的记录位置
        '     intRowIndex      ：网格当前行
        '     intPageIndex     ：当前页
        '     intPageSize      ：页记录数
        ' 返回
        '                      ：对应的DataView中的记录位置
        '----------------------------------------------------------------
        Public Function getRecordPosition( _
            ByVal intRowIndex As Integer, _
            ByVal intPageIndex As Integer, _
            ByVal intPageSize As Integer) As Integer

            Try
                getRecordPosition = intPageIndex * intPageSize + intRowIndex
            Catch ex As Exception
                getRecordPosition = -1
            End Try

        End Function

        '----------------------------------------------------------------
        ' 执行网格的数据预绑定，以确保数据调整后造成网格早前的参数无效参数
        ' 得到修正
        '     strErrMsg        ：返回错误信息
        '     objDataGrid      ：DataGrid对象
        '     intRowCount      ：对应数据源中的记录数
        ' 返回
        '     True             ：成功
        '     False            ：失败
        '----------------------------------------------------------------
        Public Function onBeforeDataGridBind( _
            ByRef strErrMsg As String, _
            ByVal objDataGrid As System.Web.UI.WebControls.DataGrid, _
            ByVal intRowCount As Integer) As Boolean

            onBeforeDataGridBind = False

            Try
                '为防止按现有索引参数绑定失败，将其设为缺省状态
                '备份网格索引
                Dim intPageIndex As Integer
                intPageIndex = objDataGrid.CurrentPageIndex
                Dim intSelectedIndex As Integer
                intSelectedIndex = objDataGrid.SelectedIndex

                '正则化网格索引
                doCheckDataGridIndex(intRowCount, objDataGrid.AllowPaging, objDataGrid.PageSize, intPageIndex, intSelectedIndex)

                '重设网格索引
                Try
                    objDataGrid.CurrentPageIndex = intPageIndex
                    objDataGrid.SelectedIndex = intSelectedIndex
                Catch ex As Exception
                End Try
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            onBeforeDataGridBind = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 计算网格中列宽(所有指定了pixel宽度的列的宽度和)
        '     objDataGrid    ：DataGrid对象
        ' 返回
        '                    ：所有指定了pixel宽度的列的宽度和
        '----------------------------------------------------------------
        Public Function getDataGridWidthPixels( _
            ByVal objDataGrid As System.Web.UI.WebControls.DataGrid) As Integer

            Dim intTotal As Integer = 0

            Try
                Dim objUnit As System.Web.UI.WebControls.Unit
                Dim intCount As Integer
                Dim i As Integer
                intCount = objDataGrid.Columns.Count
                For i = 0 To intCount - 1 Step 1
                    Try
                        objUnit = objDataGrid.Columns(i).HeaderStyle.Width
                        Select Case objUnit.Type
                            Case UnitType.Pixel
                                intTotal += CType(objUnit.Value, Integer)
                            Case Else
                        End Select
                    Catch ex As Exception
                        objUnit = Nothing
                    End Try
                Next
            Catch ex As Exception
            End Try
            getDataGridWidthPixels = intTotal

        End Function

        '----------------------------------------------------------------
        ' 获取网格行中指定数据列的列索引(BoundColumn或ButtonColumn)
        '     objDataGrid      ：DataGrid
        '     strDataFieldName ：数据列名
        ' 返回
        '                      ：数据列索引
        '----------------------------------------------------------------
        Public Function getDataGridColumnIndex( _
            ByVal objDataGrid As System.Web.UI.WebControls.DataGrid, _
            ByVal strDataFieldName As String) As Integer

            Try
                Dim objButtonColumn As System.Web.UI.WebControls.ButtonColumn
                Dim objBoundColumn As System.Web.UI.WebControls.BoundColumn
                Dim intColCount As Integer
                Dim i As Integer
                intColCount = objDataGrid.Columns.Count
                For i = 0 To intColCount - 1 Step 1
                    '尝试BoundColumn
                    Try
                        objBoundColumn = CType(objDataGrid.Columns(i), System.Web.UI.WebControls.BoundColumn)
                        If objBoundColumn.DataField = strDataFieldName Then
                            getDataGridColumnIndex = i
                            Exit Function
                        End If
                    Catch ex As Exception
                        objBoundColumn = Nothing
                    End Try

                    '尝试ButtonColumn
                    Try
                        objButtonColumn = CType(objDataGrid.Columns(i), System.Web.UI.WebControls.ButtonColumn)
                        If objButtonColumn.DataTextField = strDataFieldName Then
                            getDataGridColumnIndex = i
                            Exit Function
                        End If
                    Catch ex As Exception
                        objButtonColumn = Nothing
                    End Try
                Next
            Catch ex As Exception
                getDataGridColumnIndex = -1
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取网格行中指定数据列的String值(BoundColumn或ButtonColumn)
        '     objDataGrid      ：DataGrid
        '     objDataGridItem  ：DataGrid中的DataGridItem
        '     strDataFieldName ：数据列名
        ' 返回
        '                      ：数据列值
        '----------------------------------------------------------------
        Public Function getDataGridCellValue( _
            ByVal objDataGrid As System.Web.UI.WebControls.DataGrid, _
            ByVal objDataGridItem As System.Web.UI.WebControls.DataGridItem, _
            ByVal strDataFieldName As String) As String

            Try
                '根据strDataFieldName查找列索引
                Dim intColIndex As Integer
                intColIndex = getDataGridColumnIndex(objDataGrid, strDataFieldName)
                If intColIndex = -1 Then
                    getDataGridCellValue = ""
                Else
                    With objDataGridItem.Cells(intColIndex)
                        If .Controls.Count > 0 Then
                            getDataGridCellValue = CType(.Controls(0), System.Web.UI.WebControls.LinkButton).Text
                        Else
                            getDataGridCellValue = .Text
                        End If
                    End With
                End If
            Catch ex As Exception
                getDataGridCellValue = ""
            End Try
            If getDataGridCellValue.Length > 0 Then getDataGridCellValue = getDataGridCellValue.Trim()

        End Function

        '----------------------------------------------------------------
        ' 获取网格行中指定数据列的String值
        '     objDataGridItem  ：DataGrid中的DataGridItem
        '     intColIndex      ：数据列索引
        ' 返回
        '                      ：数据列值
        '----------------------------------------------------------------
        Public Function getDataGridCellValue( _
            ByVal objDataGridItem As System.Web.UI.WebControls.DataGridItem, _
            ByVal intColIndex As Integer) As String

            Try
                With objDataGridItem.Cells(intColIndex)
                    If .Controls.Count > 0 Then
                        getDataGridCellValue = CType(.Controls(0), System.Web.UI.WebControls.LinkButton).Text
                    Else
                        getDataGridCellValue = .Text
                    End If
                End With
            Catch ex As Exception
                getDataGridCellValue = ""
            End Try
            If getDataGridCellValue.Length > 0 Then getDataGridCellValue = getDataGridCellValue.Trim()

        End Function

        '----------------------------------------------------------------
        ' 使能DataGrid(不使能列头)
        '     strErrMsg        ：返回错误信息
        '     objDataGrid      ：DataGrid
        '     blnEnabled       ：使能开关
        ' 返回
        '                      ：数据列值
        '----------------------------------------------------------------
        Public Function doEnabledDataGrid( _
            ByRef strErrMsg As String, _
            ByVal objDataGrid As System.Web.UI.WebControls.DataGrid, _
            ByVal blnEnabled As Boolean) As Boolean

            Try
                Dim intStart As Integer
                intStart = 0

                '获取网格行、列数
                Dim intRows As Integer
                intRows = objDataGrid.Items.Count
                Dim intCols As Integer
                intCols = objDataGrid.Columns.Count

                '逐行使能网格数据
                Dim objLinkButton As System.Web.UI.WebControls.LinkButton
                Dim objCheckBox As System.Web.UI.WebControls.CheckBox
                Dim intControls As Integer
                Dim i As Integer
                Dim j As Integer
                Dim k As Integer
                For i = intStart To intRows - 1 Step 1
                    For j = 0 To intCols - 1 Step 1
                        intControls = objDataGrid.Items(i).Cells(j).Controls.Count
                        If intControls < 1 Then
                            objDataGrid.Items(i).Cells(j).Enabled = blnEnabled
                        Else
                            For k = 0 To intControls - 1 Step 1
                                Try
                                    objLinkButton = CType(objDataGrid.Items(i).Cells(j).Controls(k), System.Web.UI.WebControls.LinkButton)
                                    objLinkButton.Enabled = blnEnabled
                                    GoTo nextControl
                                Catch ex As Exception
                                    objLinkButton = Nothing
                                End Try

                                Try
                                    objCheckBox = CType(objDataGrid.Items(i).Cells(j).Controls(k), System.Web.UI.WebControls.CheckBox)
                                    objCheckBox.Enabled = blnEnabled
                                    GoTo nextControl
                                Catch ex As Exception
                                    objCheckBox = Nothing
                                End Try
nextControl:
                            Next
                        End If
                    Next
                Next

                doEnabledDataGrid = True

            Catch ex As Exception
                doEnabledDataGrid = False
                strErrMsg = ex.Message
            End Try

        End Function





        '----------------------------------------------------------------
        ' 获取指定行中的strControlId的Postback的首参数
        '     objCommandSource ：Object
        ' 返回
        '                      ：Postback的首参数
        '----------------------------------------------------------------
        Public Function getPostbackControlId(ByVal objCommandSource As Object) As String

            Dim objLinkButton As System.Web.UI.WebControls.LinkButton
            Dim objButton As System.Web.UI.WebControls.Button

            getPostbackControlId = ""
            Try
                Try
                    objLinkButton = CType(objCommandSource, System.Web.UI.WebControls.LinkButton)
                Catch ex As Exception
                    objLinkButton = Nothing
                End Try
                If Not (objLinkButton Is Nothing) Then
                    getPostbackControlId = objLinkButton.UniqueID.Replace(":", "$")
                    Exit Try
                End If

                Try
                    objButton = CType(objCommandSource, System.Web.UI.WebControls.Button)
                Catch ex As Exception
                    objButton = Nothing
                End Try
                If Not (objButton Is Nothing) Then
                    getPostbackControlId = objButton.UniqueID.Replace(":", "$")
                    Exit Try
                End If
            Catch ex As Exception
            End Try

        End Function

    End Class

End Namespace
