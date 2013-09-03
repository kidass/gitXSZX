Imports System

Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.web
    ' ����    ��ListBoxProcess
    '
    ' ����������
    '     ����listBox�ؼ���صĲ���
    '----------------------------------------------------------------

    Public Class ListBoxProcess
        Implements IDisposable








        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
        End Sub

        '----------------------------------------------------------------
        ' ������������
        '----------------------------------------------------------------
        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(True)
        End Sub

        '----------------------------------------------------------------
        ' ������������
        '----------------------------------------------------------------
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If (Not disposing) Then
                Exit Sub
            End If
        End Sub

        '----------------------------------------------------------------
        ' ��ȫ�ͷű�����Դ
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
        ' ��ȡlistBox��ѡ�е�ListItem
        '     objListBox         ��ListBox����
        '����
        '                        ��ѡ�е�ListItem
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
        ' ���ݸ���ֵ��ȡlistBox�е�ListItem��Index
        '     objListBox         ��ListBox����
        '����
        '                        ��ListItem��Index
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
