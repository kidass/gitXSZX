Imports System

Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.web
    ' ����    ��DropDownListProcess
    '
    ' ����������
    '     ����DropDownList�ؼ���صĲ���
    '----------------------------------------------------------------

    Public Class DropDownListProcess
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
        ' ��ȡDropDownList��ѡ�е�ListItem
        '     objDropDownList    ��DropDownList����
        '����
        '                        ��ѡ�е�ListItem
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
        ' ���ݸ���ֵ��ȡDropDownList�е�ListItem��Index
        '     objDropDownList    ��DropDownList����
        ' ����
        '                        ��ListItem��Index
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
        ' ���ݸ���ֵ��ȡDropDownList�е�ListItem��Index
        '     objDropDownList    ��DropDownList����
        ' ����
        '                        ��ListItem��Index
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
        ' ���ݸ���objListֵ��������б�
        '     strErrMsg          �����ش�����Ϣ
        '     ddlControl         ��DropDownList
        '     objList            ��NameValueCollection
        '     blnClear           �����������
        ' ����
        '     True               ���ɹ�
        '     False              ��ʧ��
        ' ��ע
        '      2008-06-25 �Ӳ�����Optional ByVal blnAddBlank As Boolean = False��
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
                '���
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

                '��������
                Dim intOldSelectedIndex As Integer
                intOldSelectedIndex = ddlControl.SelectedIndex

                '���
                ddlControl.SelectedIndex = -1
                If blnClear = True Then
                    ddlControl.Items.Clear()
                End If

                ' 2008-06-25
                '�ӿ���
                If blnAddBlank = True Then
                    ddlControl.Items.Add("")
                End If
                ' 2008-06-25

                '���
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
        ' ���ݸ���strListֵ��������б�
        '     strErrMsg          �����ش�����Ϣ
        '     ddlControl         ��DropDownList
        '     strList            ���ַ����б�
        ' ����
        '     True               ���ɹ�
        '     False              ��ʧ��
        '----------------------------------------------------------------
        Public Function doFillData( _
            ByRef strErrMsg As String, _
            ByVal ddlControl As System.Web.UI.WebControls.DropDownList, _
            ByVal strList As String) As Boolean

            doFillData = False
            strErrMsg = ""

            Try
                '���
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

                '��������
                Dim intOldSelectedIndex As Integer
                intOldSelectedIndex = ddlControl.SelectedIndex

                '���
                ddlControl.SelectedIndex = -1
                ddlControl.Items.Clear()

                '�ָ�
                Dim strArray() As String
                strArray = strList.Split(Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate.ToCharArray)

                '���
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
        ' ���ݸ���strListֵ��������б�
        '     strErrMsg          �����ش�����Ϣ
        '     ddlControl         ��DropDownList
        '     objDataTable       ��System.Data.DataTable
        '     strField           ������
        ' ����
        '     True               ���ɹ�
        '     False              ��ʧ��
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
                '���
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

                '��������
                Dim intOldSelectedIndex As Integer
                intOldSelectedIndex = ddlControl.SelectedIndex

                '���
                If blnClear = True Then
                    ddlControl.SelectedIndex = -1
                    ddlControl.Items.Clear()
                End If

                '���
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
