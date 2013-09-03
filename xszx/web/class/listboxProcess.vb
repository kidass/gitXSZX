Imports System

Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.web
    ' 类名    ：ListBoxProcess
    '
    ' 功能描述：
    '     处理listBox控件相关的操作
    '----------------------------------------------------------------

    Public Class ListBoxProcess
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.web.ListBoxProcess)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub










        '----------------------------------------------------------------
        ' 获取listBox中选中的ListItem
        '     objListBox         ：ListBox对象
        '返回
        '                        ：选中的ListItem
        '----------------------------------------------------------------
        Public Function getSelectedItem(ByVal objListBox As System.Web.UI.WebControls.ListBox) As System.Web.UI.WebControls.ListItem

            getSelectedItem = Nothing

            Try
                If objListBox.SelectedIndex < 0 Then
                    Exit Try
                End If
                getSelectedItem = objListBox.Items(objListBox.SelectedIndex)
            Catch ex As Exception
                getSelectedItem = Nothing
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据给定值获取listBox中的ListItem的Index
        '     objListBox         ：ListBox对象
        '返回
        '                        ：ListItem的Index
        '----------------------------------------------------------------
        Public Function getSelectedItem( _
            ByVal objListBox As System.Web.UI.WebControls.ListBox, _
            ByVal strItemValue As String) As Integer

            getSelectedItem = -1

            Try
                If strItemValue Is Nothing Then strItemValue = ""
                strItemValue = strItemValue.Trim
                If strItemValue = "" Then Exit Try
                strItemValue = strItemValue.ToUpper

                Dim intCount As Integer
                Dim i As Integer
                intCount = objListBox.Items.Count
                For i = 0 To intCount - 1 Step 1
                    If objListBox.Items(i).Value.ToUpper = strItemValue Then
                        getSelectedItem = i
                        Exit Function
                    End If
                Next
            Catch ex As Exception
                getSelectedItem = -1
            End Try

        End Function

    End Class

End Namespace
