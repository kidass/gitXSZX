Imports System

Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.web
    ' 类名    ：DropDownListProcess
    '
    ' 功能描述：
    '     处理DropDownList控件相关的操作
    '----------------------------------------------------------------

    Public Class DropDownListProcess
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.web.DropDownListProcess)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub








        '----------------------------------------------------------------
        ' 获取DropDownList中选中的ListItem
        '     objDropDownList    ：DropDownList对象
        '返回
        '                        ：选中的ListItem
        '----------------------------------------------------------------
        Public Function getSelectedItem(ByVal objDropDownList As System.Web.UI.WebControls.DropDownList) As System.Web.UI.WebControls.ListItem

            getSelectedItem = Nothing

            Try
                If objDropDownList.SelectedIndex < 0 Then
                    Exit Try
                End If
                getSelectedItem = objDropDownList.Items(objDropDownList.SelectedIndex)
            Catch ex As Exception
                getSelectedItem = Nothing
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据给定值获取DropDownList中的ListItem的Index
        '     objDropDownList    ：DropDownList对象
        ' 返回
        '                        ：ListItem的Index
        '----------------------------------------------------------------
        Public Function getSelectedItem( _
            ByVal objDropDownList As System.Web.UI.WebControls.DropDownList, _
            ByVal strItemValue As String) As Integer

            getSelectedItem = -1

            Try
                If strItemValue Is Nothing Then strItemValue = ""
                strItemValue = strItemValue.Trim
                If strItemValue = "" Then Exit Try
                strItemValue = strItemValue.ToUpper

                Dim intCount As Integer
                Dim i As Integer
                intCount = objDropDownList.Items.Count
                For i = 0 To intCount - 1 Step 1
                    If objDropDownList.Items(i).Value.ToUpper = strItemValue Then
                        getSelectedItem = i
                        Exit Function
                    End If
                Next
            Catch ex As Exception
                getSelectedItem = -1
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据给定值获取DropDownList中的ListItem的Index
        '     objDropDownList    ：DropDownList对象
        ' 返回
        '                        ：ListItem的Index
        '----------------------------------------------------------------
        Public Function getSelectedItem( _
            ByVal objDropDownList As System.Web.UI.WebControls.DropDownList, _
            ByVal strItemText As String, _
            ByVal blnUnused As Boolean) As Integer

            getSelectedItem = -1

            Try
                If strItemText Is Nothing Then strItemText = ""
                strItemText = strItemText.Trim
                If strItemText = "" Then
                    Exit Try
                End If
                strItemText = strItemText.ToUpper

                Dim intCount As Integer
                Dim i As Integer
                intCount = objDropDownList.Items.Count
                For i = 0 To intCount - 1 Step 1
                    If objDropDownList.Items(i).Text.ToUpper = strItemText Then
                        getSelectedItem = i
                        Exit Function
                    End If
                Next
            Catch ex As Exception
                getSelectedItem = -1
            End Try

        End Function








        '----------------------------------------------------------------
        ' 根据给定objList值填充下拉列表
        '     strErrMsg          ：返回错误信息
        '     ddlControl         ：DropDownList
        '     objList            ：NameValueCollection
        '     blnClear           ：清除现有项
        ' 返回
        '     True               ：成功
        '     False              ：失败
        ' 备注
        '      2008-06-25 加参数“Optional ByVal blnAddBlank As Boolean = False”
        '----------------------------------------------------------------
        Public Function doFillData( _
            ByRef strErrMsg As String, _
            ByVal ddlControl As System.Web.UI.WebControls.DropDownList, _
            ByVal objList As System.Collections.Specialized.NameValueCollection, _
            Optional ByVal blnClear As Boolean = True, _
            Optional ByVal blnAddBlank As Boolean = False) As Boolean

            doFillData = False
            strErrMsg = ""

            Try
                '检查
                If ddlControl Is Nothing Then
                    Exit Try
                End If
                If objList Is Nothing Then
                    Exit Try
                End If
                If objList.Count < 1 Then
                    ddlControl.SelectedIndex = -1
                    If blnClear = True Then
                        ddlControl.Items.Clear()
                    End If
                    Exit Try
                End If

                '保存索引
                Dim intOldSelectedIndex As Integer
                intOldSelectedIndex = ddlControl.SelectedIndex

                '清空
                ddlControl.SelectedIndex = -1
                If blnClear = True Then
                    ddlControl.Items.Clear()
                End If

                ' 2008-06-25
                '加空项
                If blnAddBlank = True Then
                    ddlControl.Items.Add("")
                End If
                ' 2008-06-25

                '填充
                Dim intCount As Integer
                Dim i As Integer
                intCount = objList.Count
                For i = 0 To intCount - 1 Step 1
                    ddlControl.Items.Add(objList(i))
                Next

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doFillData = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据给定strList值填充下拉列表
        '     strErrMsg          ：返回错误信息
        '     ddlControl         ：DropDownList
        '     strList            ：字符串列表
        ' 返回
        '     True               ：成功
        '     False              ：失败
        '----------------------------------------------------------------
        Public Function doFillData( _
            ByRef strErrMsg As String, _
            ByVal ddlControl As System.Web.UI.WebControls.DropDownList, _
            ByVal strList As String) As Boolean

            doFillData = False
            strErrMsg = ""

            Try
                '检查
                If ddlControl Is Nothing Then
                    Exit Try
                End If
                If strList Is Nothing Then
                    Exit Try
                End If
                strList = strList.Trim
                If strList = "" Then
                    ddlControl.SelectedIndex = -1
                    ddlControl.Items.Clear()
                    Exit Try
                End If

                '保存索引
                Dim intOldSelectedIndex As Integer
                intOldSelectedIndex = ddlControl.SelectedIndex

                '清空
                ddlControl.SelectedIndex = -1
                ddlControl.Items.Clear()

                '分隔
                Dim strArray() As String
                strArray = strList.Split(Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate.ToCharArray)

                '填充
                Dim intCount As Integer
                Dim i As Integer
                intCount = strArray.Length
                For i = 0 To intCount - 1 Step 1
                    ddlControl.Items.Add(strArray(i))
                Next

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doFillData = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据给定strList值填充下拉列表
        '     strErrMsg          ：返回错误信息
        '     ddlControl         ：DropDownList
        '     objDataTable       ：System.Data.DataTable
        '     strField           ：列名
        ' 返回
        '     True               ：成功
        '     False              ：失败
        '----------------------------------------------------------------
        Public Function doFillData( _
            ByRef strErrMsg As String, _
            ByVal ddlControl As System.Web.UI.WebControls.DropDownList, _
            ByVal objDataTable As System.Data.DataTable, _
            ByVal strField As String, _
            Optional ByVal blnClear As Boolean = True) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            doFillData = False
            strErrMsg = ""

            Try
                '检查
                If ddlControl Is Nothing Then
                    Exit Try
                End If
                If objDataTable Is Nothing Then
                    Exit Try
                End If
                If strField Is Nothing Then strField = ""
                strField = strField.Trim
                If strField = "" Then
                    Exit Try
                End If

                '保存索引
                Dim intOldSelectedIndex As Integer
                intOldSelectedIndex = ddlControl.SelectedIndex

                '清空
                If blnClear = True Then
                    ddlControl.SelectedIndex = -1
                    ddlControl.Items.Clear()
                End If

                '填充
                Dim intCount As Integer
                Dim i As Integer
                intCount = objDataTable.DefaultView.Count
                For i = 0 To intCount - 1 Step 1
                    ddlControl.Items.Add(objPulicParameters.getObjectValue(objDataTable.DefaultView.Item(i).Item(strField), ""))
                Next

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            doFillData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function

    End Class

End Namespace
