Imports System

Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.web
    ' 类名    ：RepeaterProcess
    '
    ' 功能描述：
    '     System.Web.UI.WebControls.Repeater对象的有关处理
    '----------------------------------------------------------------

    Public Class RepeaterProcess
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.web.RepeaterProcess)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub










        '----------------------------------------------------------------
        ' 计算总页数
        ' 返回
        '     True             ：成功
        '     False            ：失败
        '----------------------------------------------------------------
        Public Function getPageCount( _
            ByVal intRowCount As Integer, _
            ByVal intPageSize As Integer, _
            ByRef intPageCount As Integer) As Boolean

            getPageCount = False
            Try
                If (intRowCount Mod intPageSize) = 0 Then
                    intPageCount = CType(Fix(intRowCount / intPageSize), Integer)
                Else
                    intPageCount = CType(Fix(intRowCount / intPageSize), Integer) + 1
                End If
                getPageCount = True
            Catch ex As Exception
                getPageCount = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 校验并调整页面索引
        ' 返回
        '     True             ：成功
        '     False            ：失败
        '----------------------------------------------------------------
        Public Function doValidPageIndex( _
            ByVal intPageCount As Integer, _
            ByRef intPageIndex As Integer) As Boolean

            doValidPageIndex = False
            Try
                If intPageIndex >= intPageCount Then
                    intPageIndex = intPageCount - 1
                End If
                If intPageIndex <= 0 Then
                    intPageIndex = 0
                End If
                doValidPageIndex = True
            Catch ex As Exception
                doValidPageIndex = False
            End Try

        End Function





        '----------------------------------------------------------------
        ' 根据参数判断是否可以进行“首页”操作
        ' 返回
        '     True             ：能
        '     False            ：不能
        '----------------------------------------------------------------
        Public Function canDoMoveFirstPage( _
            ByVal intPageCount As Integer, _
            ByVal intPageIndex As Integer, _
            ByVal intPageSize As Integer, _
            ByVal intRowCount As Integer) As Boolean

            canDoMoveFirstPage = False

            Try
                '没有记录
                If intRowCount < 1 Then
                    Exit Try
                End If
                '仅有1页
                If intPageCount <= 1 Then
                    Exit Try
                End If
                '是首页
                If intPageIndex <= 0 Then
                    Exit Try
                End If
                '其他都可以
                canDoMoveFirstPage = True
            Catch ex As Exception
                canDoMoveFirstPage = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据参数判断是否可以进行“尾页”操作
        ' 返回
        '     True             ：能
        '     False            ：不能
        '----------------------------------------------------------------
        Public Function canDoMoveLastPage( _
            ByVal intPageCount As Integer, _
            ByVal intPageIndex As Integer, _
            ByVal intPageSize As Integer, _
            ByVal intRowCount As Integer) As Boolean

            canDoMoveLastPage = False

            Try
                '没有记录
                If intRowCount < 1 Then
                    Exit Try
                End If
                '仅有1页
                If intPageCount <= 1 Then
                    Exit Try
                End If
                '是尾页
                If intPageIndex >= intPageCount - 1 Then
                    Exit Try
                End If
                '其他都可以
                canDoMoveLastPage = True
            Catch ex As Exception
                canDoMoveLastPage = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据参数判断是否可以进行“上页”操作
        ' 返回
        '     True             ：能
        '     False            ：不能
        '----------------------------------------------------------------
        Public Function canDoMovePreviousPage( _
            ByVal intPageCount As Integer, _
            ByVal intPageIndex As Integer, _
            ByVal intPageSize As Integer, _
            ByVal intRowCount As Integer) As Boolean

            canDoMovePreviousPage = False

            Try
                '没有记录
                If intRowCount < 1 Then
                    Exit Try
                End If
                '仅有1页
                If intPageCount <= 1 Then
                    Exit Try
                End If
                '是首页
                If intPageIndex <= 0 Then
                    Exit Try
                End If
                '其他都可以
                canDoMovePreviousPage = True
            Catch ex As Exception
                canDoMovePreviousPage = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据参数判断是否可以进行“下页”操作
        ' 返回
        '     True             ：能
        '     False            ：不能
        '----------------------------------------------------------------
        Public Function canDoMoveNextPage( _
            ByVal intPageCount As Integer, _
            ByVal intPageIndex As Integer, _
            ByVal intPageSize As Integer, _
            ByVal intRowCount As Integer) As Boolean

            canDoMoveNextPage = False

            Try
                '没有记录
                If intRowCount < 1 Then
                    Exit Try
                End If
                '仅有1页
                If intPageCount <= 1 Then
                    Exit Try
                End If
                '是尾页
                If intPageIndex >= intPageCount - 1 Then
                    Exit Try
                End If
                '其他都可以
                canDoMoveNextPage = True
            Catch ex As Exception
                canDoMoveNextPage = False
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
        ' 根据当前行intRowIndex、当前页intPageIndex、页记录数intPageSize
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
        ' 复原Repeater的列头显示
        '     strErrMsg      ：返回错误信息
        '     objRepeater    ：System.Web.UI.WebControls.Repeater
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Public Function doResetHeader( _
            ByRef strErrMsg As String, _
            ByVal objRepeater As System.Web.UI.WebControls.Repeater) As Boolean

            Dim strDesc As String = Xydc.Platform.Common.Utilities.PulicParameters.CharDesc
            Dim strAsc As String = Xydc.Platform.Common.Utilities.PulicParameters.CharAsc

            doResetHeader = False

            Try
                '检查
                If objRepeater Is Nothing Then
                    Exit Try
                End If

                '获取列头
                If objRepeater.Controls.Count < 1 Then
                    Exit Try
                End If

                '检测LinkButton
                Dim objLinkButton As System.Web.UI.WebControls.LinkButton
                Dim objControl As System.Web.UI.Control
                Dim intCount As Integer
                Dim i As Integer
                Dim j As Integer
                intCount = objRepeater.Controls(0).Controls.Count
                For i = 0 To intCount - 1 Step 1
                    objControl = objRepeater.Controls(0).Controls(i)
                    Try
                        objLinkButton = CType(objControl, System.Web.UI.WebControls.LinkButton)
                    Catch ex As Exception
                        objControl = Nothing
                    End Try
                    If Not (objLinkButton Is Nothing) Then
                        objLinkButton.Text = objLinkButton.Text.Replace(strDesc, "")
                        objLinkButton.Text = objLinkButton.Text.Replace(strAsc, "")
                        objLinkButton.Text = objLinkButton.Text.Trim
                    End If
                Next

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doResetHeader = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 按指定Repeater的列头排序
        '     strErrMsg      ：返回错误信息
        '     objRepeater    ：System.Web.UI.WebControls.Repeater
        '     strColumnId    ：排序列
        '     objSortType    ：排序类型
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Public Function doSortHeader( _
            ByRef strErrMsg As String, _
            ByVal objRepeater As System.Web.UI.WebControls.Repeater, _
            ByVal strColumnId As String, _
            ByVal objSortType As Xydc.Platform.Common.Utilities.PulicParameters.enumSortType) As Boolean

            Dim strDesc As String = Xydc.Platform.Common.Utilities.PulicParameters.CharDesc
            Dim strAsc As String = Xydc.Platform.Common.Utilities.PulicParameters.CharAsc

            doSortHeader = False

            Try
                '检查
                If objRepeater Is Nothing Then
                    Exit Try
                End If
                If strColumnId Is Nothing Then strColumnId = ""
                strColumnId = strColumnId.Trim
                If strColumnId = "" Then
                    Exit Try
                End If

                '获取列头
                If objRepeater.Controls.Count < 1 Then
                    Exit Try
                End If

                '检测LinkButton
                Dim objLinkButton As System.Web.UI.WebControls.LinkButton
                Dim objControl As System.Web.UI.Control
                objControl = objRepeater.Controls(0).FindControl(strColumnId)
                Try
                    objLinkButton = CType(objControl, System.Web.UI.WebControls.LinkButton)
                Catch ex As Exception
                    objControl = Nothing
                End Try
                If Not (objLinkButton Is Nothing) Then
                    objLinkButton.Text = objLinkButton.Text.Replace(strDesc, "")
                    objLinkButton.Text = objLinkButton.Text.Replace(strAsc, "")
                    objLinkButton.Text = objLinkButton.Text.Trim

                    Select Case objSortType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumSortType.Asc
                            objLinkButton.Text = objLinkButton.Text + " " + strAsc
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumSortType.Desc
                            objLinkButton.Text = objLinkButton.Text + " " + strDesc
                        Case Else
                    End Select
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doSortHeader = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示列头信息
        '     strErrMsg      ：返回错误信息
        '     objRepeater    ：System.Web.UI.WebControls.Repeater
        '     strColumnId    ：排序列
        '     objSortType    ：排序类型
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Public Function doDisplayHeadInfo( _
            ByRef strErrMsg As String, _
            ByVal objRepeater As System.Web.UI.WebControls.Repeater, _
            ByVal strColumnId As String, _
            ByVal objSortType As Xydc.Platform.Common.Utilities.PulicParameters.enumSortType) As Boolean

            doDisplayHeadInfo = False

            Try
                '复原列头
                If Me.doResetHeader(strErrMsg, objRepeater) = False Then
                    GoTo errProc
                End If

                '设置列头
                If Me.doSortHeader(strErrMsg, objRepeater, strColumnId, objSortType) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doDisplayHeadInfo = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取新的排序信息
        '     strErrMsg      ：返回错误信息
        '     strOldColumnId ：原排序列
        '     objOldSortType ：原排序类型
        '     strOldColumnId ：新排序列
        '     objOldSortType ：（返回）新排序类型
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Public Function getNewSortParam( _
            ByRef strErrMsg As String, _
            ByVal strOldColumnId As String, _
            ByVal objOldSortType As Xydc.Platform.Common.Utilities.PulicParameters.enumSortType, _
            ByVal strNewColumnId As String, _
            ByRef objNewSortType As Xydc.Platform.Common.Utilities.PulicParameters.enumSortType) As Boolean

            getNewSortParam = False

            Try
                If strNewColumnId Is Nothing Then strNewColumnId = ""
                strNewColumnId = strNewColumnId.Trim
                If strNewColumnId = "" Then
                    Exit Try
                End If
                If strOldColumnId Is Nothing Then strOldColumnId = ""
                strOldColumnId = strOldColumnId.Trim

                '计算
                If strNewColumnId.ToUpper <> strOldColumnId.ToUpper Then
                    objNewSortType = Xydc.Platform.Common.Utilities.PulicParameters.enumSortType.Asc
                    Exit Try
                End If

                Select Case objOldSortType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumSortType.Asc
                        objNewSortType = Xydc.Platform.Common.Utilities.PulicParameters.enumSortType.Desc
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumSortType.Desc
                        objNewSortType = Xydc.Platform.Common.Utilities.PulicParameters.enumSortType.None
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumSortType.None
                        objNewSortType = Xydc.Platform.Common.Utilities.PulicParameters.enumSortType.Asc
                End Select

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getNewSortParam = True
            Exit Function

errProc:
            Exit Function

        End Function




        '----------------------------------------------------------------
        ' 根据Request中的信息恢复指定列中的CheckBox状态
        '     strErrMsg        ：返回错误信息
        '     objRepeater      ：System.Web.UI.WebControls.Repeater
        '     objHttpRequest   ：当前HttpRequest
        '     strCheckBoxId    ：CheckBox控件ID
        ' 返回
        '     True             ：成功
        '     False            ：失败
        '----------------------------------------------------------------
        Public Function doRestoreCheckBoxStatus( _
            ByRef strErrMsg As String, _
            ByVal objRepeater As System.Web.UI.WebControls.Repeater, _
            ByVal objHttpRequest As System.Web.HttpRequest, _
            ByVal strCheckBoxId As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objCheckBox As System.Web.UI.WebControls.CheckBox
            Dim objControl As System.Web.UI.Control
            Dim intRowCount As Integer
            Dim blnSelect As Boolean
            Dim i As Integer

            doRestoreCheckBoxStatus = False

            Try
                intRowCount = objRepeater.Items.Count
                For i = 0 To intRowCount - 1 Step 1
                    objControl = Nothing
                    objControl = objRepeater.Items(i).FindControl(strCheckBoxId)
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

            doRestoreCheckBoxStatus = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据Request中的信息恢复指定列中的CheckBox状态
        '     strErrMsg        ：返回错误信息
        '     objRepeater      ：System.Web.UI.WebControls.Repeater
        '     strCheckBoxId    ：CheckBox控件ID
        '     blnChecked       ：行CheckBox状态
        ' 返回
        '     True             ：成功
        '     False            ：失败
        '----------------------------------------------------------------
        Public Function doRestoreCheckBoxStatus( _
            ByRef strErrMsg As String, _
            ByVal objRepeater As System.Web.UI.WebControls.Repeater, _
            ByVal strCheckBoxId As String, _
            ByVal blnChecked() As Boolean) As Boolean

            Dim objCheckBox As System.Web.UI.WebControls.CheckBox
            Dim objControl As System.Web.UI.Control
            Dim intRowCount As Integer
            Dim blnSelect As Boolean
            Dim i As Integer

            doRestoreCheckBoxStatus = False

            Try
                intRowCount = objRepeater.Items.Count
                For i = 0 To intRowCount - 1 Step 1
                    objControl = Nothing
                    objControl = objRepeater.Items(i).FindControl(strCheckBoxId)
                    If Not (objControl Is Nothing) Then
                        objCheckBox = Nothing
                        objCheckBox = CType(objControl, System.Web.UI.WebControls.CheckBox)
                        If Not (objCheckBox Is Nothing) Then
                            objCheckBox.Checked = blnChecked(i)
                        End If
                    End If
                Next
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doRestoreCheckBoxStatus = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 设置指定列中的CheckBox的Checked状态
        '     strErrMsg        ：返回错误信息
        '     objRepeater      ：System.Web.UI.WebControls.Repeater
        '     strCheckBoxId    ：CheckBox控件ID
        '     blnChecked       ：Checked
        ' 返回
        '     True             ：成功
        '     False            ：失败
        '----------------------------------------------------------------
        Public Function doSetCheckBoxValue( _
            ByRef strErrMsg As String, _
            ByVal objRepeater As System.Web.UI.WebControls.Repeater, _
            ByVal strCheckBoxId As String, _
            ByVal blnChecked As Boolean) As Boolean

            Dim objCheckBox As System.Web.UI.WebControls.CheckBox
            Dim objControl As System.Web.UI.Control
            Dim intRowCount As Integer
            Dim i As Integer

            doSetCheckBoxValue = False

            Try
                intRowCount = objRepeater.Items.Count
                For i = 0 To intRowCount - 1 Step 1
                    objControl = Nothing
                    objControl = objRepeater.Items(i).FindControl(strCheckBoxId)
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

            doSetCheckBoxValue = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 检查指定行中的CheckBox的Checked状态
        '     objRepeaterItem  ：System.Web.UI.WebControls.RepeaterItem
        '     strCheckBoxId    ：CheckBox控件ID
        ' 返回
        '     True             ：Checked
        '     False            ：Unchecked
        '----------------------------------------------------------------
        Public Function isItemChecked( _
            ByVal objRepeaterItem As System.Web.UI.WebControls.RepeaterItem, _
            ByVal strCheckBoxId As String) As Boolean

            Dim objCheckBox As System.Web.UI.WebControls.CheckBox
            Dim objControl As System.Web.UI.Control

            isItemChecked = False
            Try
                objControl = Nothing
                objControl = objRepeaterItem.FindControl(strCheckBoxId)
                If Not (objControl Is Nothing) Then
                    objCheckBox = Nothing
                    objCheckBox = CType(objControl, System.Web.UI.WebControls.CheckBox)
                    If Not (objCheckBox Is Nothing) Then
                        isItemChecked = objCheckBox.Checked
                    End If
                End If
            Catch ex As Exception
            End Try

        End Function



        '----------------------------------------------------------------
        ' 获取指定行中的strControlId的值
        '     objRepeaterItem  ：System.Web.UI.WebControls.RepeaterItem
        '     strControlId     ：控件ID
        ' 返回
        '     True             ：Checked
        '     False            ：Unchecked
        '----------------------------------------------------------------
        Public Function getControlValue( _
            ByVal objRepeaterItem As System.Web.UI.WebControls.RepeaterItem, _
            ByVal strControlId As String) As String

            Dim objHtmlInputHidden As System.Web.UI.HtmlControls.HtmlInputHidden

            Dim objDataBoundLiteralControl As System.Web.UI.DataBoundLiteralControl
            Dim objLinkButton As System.Web.UI.WebControls.LinkButton
            Dim objLiteral As System.Web.UI.WebControls.Literal
            Dim objControl As System.Web.UI.Control

            getControlValue = ""
            Try
                objControl = Nothing
                objControl = objRepeaterItem.FindControl(strControlId)
                If Not (objControl Is Nothing) Then
                    Try
                        objLinkButton = CType(objControl, System.Web.UI.WebControls.LinkButton)
                    Catch ex As Exception
                        objLinkButton = Nothing
                    End Try
                    If Not (objLinkButton Is Nothing) Then
                        getControlValue = objLinkButton.Text.Trim
                        Exit Try
                    End If

                    Try
                        objHtmlInputHidden = CType(objControl, System.Web.UI.HtmlControls.HtmlInputHidden)
                    Catch ex As Exception
                        objHtmlInputHidden = Nothing
                    End Try
                    If Not (objHtmlInputHidden Is Nothing) Then
                        getControlValue = objHtmlInputHidden.Value.Trim
                        Exit Try
                    End If

                    Try
                        objLiteral = CType(objControl, System.Web.UI.WebControls.Literal)
                    Catch ex As Exception
                        objLiteral = Nothing
                    End Try
                    If Not (objLiteral Is Nothing) Then
                        getControlValue = objLiteral.Text.Trim
                        Exit Try
                    End If

                    Try
                        objDataBoundLiteralControl = CType(objControl, System.Web.UI.DataBoundLiteralControl)
                    Catch ex As Exception
                        objDataBoundLiteralControl = Nothing
                    End Try
                    If Not (objDataBoundLiteralControl Is Nothing) Then
                        getControlValue = objDataBoundLiteralControl.Text.Trim
                        Exit Try
                    End If
                End If
            Catch ex As Exception
            End Try

        End Function






        '----------------------------------------------------------------
        ' 获取指定行中的strControlId的Postback的首参数
        '     objRepeaterItem  ：System.Web.UI.WebControls.RepeaterItem
        '     strControlId     ：控件ID
        ' 返回
        '                      ：Postback的首参数
        '----------------------------------------------------------------
        Public Function getPostbackControlId( _
            ByVal objRepeaterItem As System.Web.UI.WebControls.RepeaterItem, _
            ByVal strControlId As String) As String

            Dim objLinkButton As System.Web.UI.WebControls.LinkButton
            Dim objButton As System.Web.UI.WebControls.Button
            Dim objControl As System.Web.UI.Control

            getPostbackControlId = ""
            Try
                objControl = Nothing
                objControl = objRepeaterItem.FindControl(strControlId)
                If Not (objControl Is Nothing) Then
                    Try
                        objLinkButton = CType(objControl, System.Web.UI.WebControls.LinkButton)
                    Catch ex As Exception
                        objLinkButton = Nothing
                    End Try
                    If Not (objLinkButton Is Nothing) Then
                        getPostbackControlId = objLinkButton.UniqueID.Replace(":", "$")
                        Exit Try
                    End If

                    Try
                        objButton = CType(objControl, System.Web.UI.WebControls.Button)
                    Catch ex As Exception
                        objButton = Nothing
                    End Try
                    If Not (objButton Is Nothing) Then
                        getPostbackControlId = objButton.UniqueID.Replace(":", "$")
                        Exit Try
                    End If
                End If
            Catch ex As Exception
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
