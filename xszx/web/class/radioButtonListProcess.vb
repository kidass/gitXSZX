Imports System

Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.web
    ' ����    ��RadioButtonListProcess
    '
    ' ����������
    '     ����radioButtonList�ؼ���صĲ���
    '----------------------------------------------------------------

    Public Class RadioButtonListProcess
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.web.RadioButtonListProcess)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub









        '----------------------------------------------------------------
        ' ��ȡradioButtonList��Checked��ListItem
        '     objRadioButtonList ��RadioButtonList����
        '����
        '                        ��Checked��ListItem
        '----------------------------------------------------------------
        Public Function getCheckedItem(ByVal objRadioButtonList As System.Web.UI.WebControls.RadioButtonList) As System.Web.UI.WebControls.ListItem

            Try
                Dim intCount As Integer
                Dim i As Integer
                intCount = objRadioButtonList.Items.Count
                For i = 0 To intCount - 1 Step 1
                    If objRadioButtonList.Items(i).Selected = True Then
                        getCheckedItem = objRadioButtonList.Items(i)
                        Exit Function
                    End If
                Next
            Catch ex As Exception
                getCheckedItem = Nothing
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���ݸ���ֵ��ȡradioButtonList�е�ListItem��Index
        '     objRadioButtonList ��RadioButtonList����
        '����
        '                        ��Checked��ListItem
        '----------------------------------------------------------------
        Public Function getCheckedItem( _
            ByVal objRadioButtonList As System.Web.UI.WebControls.RadioButtonList, _
            ByVal strItemValue As String) As Integer

            getCheckedItem = -1

            Try
                If strItemValue Is Nothing Then strItemValue = ""
                strItemValue = strItemValue.Trim
                If strItemValue = "" Then Exit Try
                strItemValue = strItemValue.ToUpper

                Dim intCount As Integer
                Dim i As Integer
                intCount = objRadioButtonList.Items.Count
                For i = 0 To intCount - 1 Step 1
                    If objRadioButtonList.Items(i).Value.ToUpper = strItemValue Then
                        getCheckedItem = i
                        Exit Function
                    End If
                Next
            Catch ex As Exception
                getCheckedItem = -1
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���ݸ���objListֵ����б�
        '     strErrMsg          �����ش�����Ϣ
        '     rblControl         ��RadioButtonList
        '     objList            ��NameValueCollection
        ' ����
        '     True               ���ɹ�
        '     False              ��ʧ��
        '----------------------------------------------------------------
        Public Function doFillData( _
            ByRef strErrMsg As String, _
            ByVal rblControl As System.Web.UI.WebControls.RadioButtonList, _
            ByVal objList As System.Collections.Specialized.NameValueCollection) As Boolean

            doFillData = False
            strErrMsg = ""

            Try
                '���
                If rblControl Is Nothing Then
                    Exit Try
                End If
                If objList Is Nothing Then
                    Exit Try
                End If
                If objList.Count < 1 Then
                    rblControl.SelectedIndex = -1
                    rblControl.Items.Clear()
                    Exit Try
                End If

                '��������
                Dim intOldSelectedIndex As Integer
                intOldSelectedIndex = rblControl.SelectedIndex

                '���
                rblControl.SelectedIndex = -1
                rblControl.Items.Clear()

                '���
                Dim objListItem As System.Web.UI.WebControls.ListItem
                Dim intCount As Integer
                Dim i As Integer
                intCount = objList.Count
                For i = 0 To intCount - 1 Step 1
                    objListItem = New System.Web.UI.WebControls.ListItem
                    objListItem.Value = objList.GetKey(i)
                    objListItem.Text = objList.Item(i)
                    rblControl.Items.Add(objListItem)
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
        ' ���ø���rblControl��Ĭ��ѡ��
        '     strErrMsg          �����ش�����Ϣ
        '     rblControl         ��RadioButtonList
        ' ����
        '     True               ���ɹ�
        '     False              ��ʧ��
        '----------------------------------------------------------------
        Public Function doSetDefaultSelectedIndex( _
            ByRef strErrMsg As String, _
            ByVal rblControl As System.Web.UI.WebControls.RadioButtonList) As Boolean

            doSetDefaultSelectedIndex = False

            Try
                '���û���趨����ȱʡ
                Dim blnFound As Boolean = False
                Dim i As Integer
                For i = 0 To rblControl.Items.Count - 1 Step 1
                    If rblControl.Items(i).Selected = True Then
                        blnFound = True
                        Exit For
                    End If
                Next
                If blnFound = False Then
                    If rblControl.Items.Count > 0 Then
                        rblControl.Items(0).Selected = True
                    End If
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doSetDefaultSelectedIndex = True
            Exit Function

errProc:
            Exit Function

        End Function

    End Class

End Namespace
