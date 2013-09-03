Imports System

Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.web
    ' ����    ��DataGridProcess
    '
    ' ����������
    '     DataGrid������йش���
    '----------------------------------------------------------------

    Public Class DataGridProcess
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
        ' ���PageIndex,RowIndex�Ƿ��ǺϷ��Ĳ���
        '     objDataGrid   ��DataGrid����
        '     intRows       ��������
        '     intPageIndex  ��׼��Ҫ��ʾ��ҳ
        '     intRowIndex   ��׼��Ҫ��ʾ����
        '----------------------------------------------------------------
        Public Sub doCheckDataGridIndex( _
            ByVal objDataGrid As System.Web.UI.WebControls.DataGrid, _
            ByVal intRows As Integer, _
            ByRef intPageIndex As Integer, _
            ByRef intRowIndex As Integer)

            Try
                '���PageIndex
                If intPageIndex >= objDataGrid.PageCount Then
                    intPageIndex = objDataGrid.PageCount - 1
                End If
                If intPageIndex < 0 Then
                    intPageIndex = 0
                End If

                '���RowIndex
                If intRowIndex >= objDataGrid.PageSize Then
                    intRowIndex = objDataGrid.PageSize - 1
                End If
                If intRowIndex < 0 Then
                    intRowIndex = 0
                End If
                '���1ҳ
                If intPageIndex = objDataGrid.PageCount - 1 Then
                    '����ʣ������
                    Dim intHas As Integer
                    intHas = intRows - intPageIndex * objDataGrid.PageSize
                    If intRowIndex >= intHas Then
                        intRowIndex = intHas - 1
                    End If
                End If
                'û�м�¼
                If intRows = 0 Then
                    intRowIndex = -1
                End If
            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' ���PageIndex,RowIndex�Ƿ��ǺϷ��Ĳ���
        '     intRows       ��������
        '     blnAllowPaging�������ҳ
        '     intPageSize   ��ҳ���С
        '     intPageIndex  ��׼��Ҫ��ʾ��ҳ
        '     intRowIndex   ��׼��Ҫ��ʾ����
        '----------------------------------------------------------------
        Public Sub doCheckDataGridIndex( _
            ByVal intRows As Integer, _
            ByVal blnAllowPaging As Boolean, _
            ByVal intPageSize As Integer, _
            ByRef intPageIndex As Integer, _
            ByRef intRowIndex As Integer)

            Dim intPageCount As Integer
            Try
                'û�м�¼
                If intRows = 0 Then
                    intPageIndex = 0
                    intRowIndex = -1
                    Exit Try
                End If

                '��ȡҳ����
                If blnAllowPaging = False Then
                    intPageSize = intRows
                End If
                If (intRows Mod intPageSize) = 0 Then
                    intPageCount = CType(Fix(intRows / intPageSize), Integer)
                Else
                    intPageCount = CType(Fix(intRows / intPageSize), Integer) + 1
                End If

                '���PageIndex
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

                '���RowIndex
                'û�м�¼
                If intRowIndex >= intPageSize Then
                    intRowIndex = intPageSize - 1
                End If
                If intRowIndex < 0 Then
                    intRowIndex = 0
                End If

                '���1ҳ
                If intPageIndex = intPageCount - 1 Then
                    '����ʣ������
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
        ' ��ȡDataGrid�Ķ�λ��Ϣ�ַ���
        '     objDataGrid   ��DataGrid����
        '     intRows       ��������
        '----------------------------------------------------------------
        Public Function getDataGridLocation( _
            ByVal objDataGrid As System.Web.UI.WebControls.DataGrid, _
            ByVal intRows As Integer) As String

            Dim strValue As String = ""
            Try
                If intRows = 0 Then
                    getDataGridLocation = "N/Nҳ N/N��"
                Else
                    strValue += (objDataGrid.CurrentPageIndex + 1).ToString()
                    strValue += "/"
                    strValue += (objDataGrid.PageCount).ToString()
                    strValue += "ҳ "
                    strValue += (objDataGrid.CurrentPageIndex * objDataGrid.PageSize + objDataGrid.SelectedIndex + 1).ToString()
                    strValue += "/"
                    strValue += (intRows).ToString()
                    strValue += "��"
                    getDataGridLocation = strValue
                End If
            Catch ex As Exception
                getDataGridLocation = "N/Nҳ N/N��"
            End Try

        End Function

        '----------------------------------------------------------------
        ' System.Web.UI.WebControls.ButtonColumn �汾
        ' ����DataTable������Ϣ�Զ�����DataGrid��ButtonColumns����Ϣ
        ' ������ӷ�ʽ�������������
        '     strErrMsg      �����ش�����Ϣ
        '     objDataGrid    ��DataGrid����
        '     objDataTable   ��DataTable����
        '     objButtonColumn����ӵ���ΪSystem.Web.UI.WebControls.ButtonColumn
        '     strCommandName ���е�CommandName(select,etc)
        ' ����
        '     True           ���ɹ�
        '     False          ��ʧ��
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
        ' System.Web.UI.WebControls.BoundColumn �汾
        ' ����DataTable������Ϣ�Զ�����DataGrid��ButtonColumns����Ϣ
        ' ������ӷ�ʽ�������������
        '     strErrMsg      �����ش�����Ϣ
        '     objDataGrid    ��DataGrid����
        '     objDataTable   ��DataTable����
        '     objButtonColumn����ӵ���ΪSystem.Web.UI.WebControls.ButtonColumn
        '     blnReadOnly    ����ֻ��
        ' ����
        '     True           ���ɹ�
        '     False          ��ʧ��
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
        ' ���ݸ������п�����ַ������������е��п���ָ���п�ʼ
        '     strErrMsg     �����ش�����Ϣ
        '     objDataGrid   ��DataGrid����
        '     strColWidth   ���п�������ñ�׼�ָ����ָ�(32px,30%,etc)
        '     intStartCol   ����ʼ�����У�ȱʡ=0
        ' ����
        '     True          ���ɹ�
        '     False         ��ʧ��
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
                    'ָ���п�
                    Dim strWidth() As String
                    strWidth = strColWidth.Split(Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate.ToCharArray())
                    For i = intStartCol To intCols - 1 Step 1
                        objDataGrid.Columns(i).HeaderStyle.Width = New System.Web.UI.WebControls.Unit(strWidth(i - intStartCol))
                    Next
                Else
                    '�Զ��п�
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
        ' ��ȡ���������ָ��
        '     strErrMsg        �����ش�����Ϣ
        '     strOldCommand    ������ǰ����ָ��
        '     strNewCommand    ������Ҫִ�е�����ָ��
        '     strFinalCommand  �����յ�����ָ�����
        '     objSortType      ������ָ������
        ' ����
        '     True             ���ɹ�
        '     False            ��ʧ��
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
                    '׼����������
                    strFinalCommand = strNewCommand + " Asc"
                    objSortType = Xydc.Platform.Common.Utilities.PulicParameters.enumSortType.Asc
                Else
                    If strOldCommand.IndexOf(strNewCommand) >= 0 Then
                        If strOldCommand.IndexOf(" Asc") >= 0 Then
                            '׼����������
                            strFinalCommand = strNewCommand + " Desc"
                            objSortType = Xydc.Platform.Common.Utilities.PulicParameters.enumSortType.Desc
                        ElseIf strOldCommand.IndexOf(" Desc") >= 0 Then
                            '׼����ԭ����
                            strFinalCommand = ""
                            objSortType = Xydc.Platform.Common.Utilities.PulicParameters.enumSortType.None
                        Else
                            '׼����������
                            strFinalCommand = strNewCommand + " Asc"
                            objSortType = Xydc.Platform.Common.Utilities.PulicParameters.enumSortType.Asc
                        End If
                    Else
                        '׼����������
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
        ' ���������ͷ�������ַ�
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
        ' ���ݿؼ���UniqueId�ڵ�ǰ�������м����������е�������
        '     objDataGridItem  �����ڵ����������
        '     strUniqueId      �����ڵ���Ŀؼ���UniqueId
        ' ����
        '                      ���ҵ�����������δ�ҵ������=-1
        '----------------------------------------------------------------
        Public Function getColumnIndexByUniqueIdInRow( _
            ByVal objDataGridItem As System.Web.UI.WebControls.DataGridItem, _
            ByVal strUniqueId As String) As Integer

            getColumnIndexByUniqueIdInRow = -1

            '��ʼ��
            If strUniqueId.Length > 0 Then strUniqueId = strUniqueId.Trim()

            '����
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
        ' ����objSortType�����������ʾ�õ��ַ���
        '     strOldHead       �������б���
        '     objSortType      ������ָ������
        ' ����
        '                      �����������ʶ���б���
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
        ' ���ݿؼ���UniqueId�ڵ�ǰ�������м����������е�������
        '     strErrMsg        �����ش�����Ϣ
        '     objDataGrid      ��DataGrid����
        '     objDataGridItem  �����ڵ����������
        '     strUniqueId      �����ڵ���Ŀؼ���UniqueId
        '     objSortType      ������ָ������
        '     intColIndex      �����������е�������
        ' ����
        '     True             ���ɹ�
        '     False            ��ʧ��
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

            '���
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
                '��ȡ��ǰ������
                intTempColIndex = getColumnIndexByUniqueIdInRow(objDataGridItem, strUniqueId)
                If intTempColIndex < 0 Then
                    GoTo normExit
                End If

                '����������ͷ
                Me.doClearSortCharInDataGridHead(objDataGrid)

                '���������������
                With objDataGrid.Columns(intTempColIndex)
                    .HeaderText = getColumnSortHeadString(.HeaderText, objSortType)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            '����������
            intColIndex = intTempColIndex
normExit:
            doSetSortCharInDataGridHead = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���������ֲ����ж��Ƿ���Խ��С���ҳ������
        '     objDataGrid      ��DataGrid����
        '     intRowCount      ���������ݵ�������
        ' ����
        '     True             ����
        '     False            ������
        '----------------------------------------------------------------
        Public Function canDoMoveFirstPage( _
            ByVal objDataGrid As System.Web.UI.WebControls.DataGrid, _
            ByVal intRowCount As Integer) As Boolean

            canDoMoveFirstPage = False
            Try
                'û�м�¼
                If intRowCount < 1 Then
                    Exit Try
                End If
                '����1ҳ
                If objDataGrid.PageCount = 1 Then
                    Exit Try
                End If
                '����ҳ
                If objDataGrid.CurrentPageIndex = 0 Then
                    Exit Try
                End If
                '����������
                canDoMoveFirstPage = True
            Catch ex As Exception
                canDoMoveFirstPage = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���������ֲ����ж��Ƿ���Խ��С�βҳ������
        '     objDataGrid      ��DataGrid����
        '     intRowCount      ���������ݵ�������
        ' ����
        '     True             ����
        '     False            ������
        '----------------------------------------------------------------
        Public Function canDoMoveLastPage( _
            ByVal objDataGrid As System.Web.UI.WebControls.DataGrid, _
            ByVal intRowCount As Integer) As Boolean

            canDoMoveLastPage = False
            Try
                'û�м�¼
                If intRowCount < 1 Then
                    Exit Try
                End If
                '����1ҳ
                If objDataGrid.PageCount = 1 Then
                    Exit Try
                End If
                '��βҳ
                If objDataGrid.CurrentPageIndex = objDataGrid.PageCount - 1 Then
                    Exit Try
                End If
                '����������
                canDoMoveLastPage = True
            Catch ex As Exception
                canDoMoveLastPage = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���������ֲ����ж��Ƿ���Խ��С���ҳ������
        '     objDataGrid      ��DataGrid����
        '     intRowCount      ���������ݵ�������
        ' ����
        '     True             ����
        '     False            ������
        '----------------------------------------------------------------
        Public Function canDoMovePreviousPage( _
            ByVal objDataGrid As System.Web.UI.WebControls.DataGrid, _
            ByVal intRowCount As Integer) As Boolean

            canDoMovePreviousPage = False
            Try
                'û�м�¼
                If intRowCount < 1 Then
                    Exit Try
                End If
                '����1ҳ
                If objDataGrid.PageCount = 1 Then
                    Exit Try
                End If
                '����ҳ
                If objDataGrid.CurrentPageIndex = 0 Then
                    Exit Try
                End If
                '����������
                canDoMovePreviousPage = True
            Catch ex As Exception
                canDoMovePreviousPage = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' �ƶ���ָ��ҳ��������Чҳ����
        '     intToPage        ��׼����ʾ��ҳ
        '     intTotalPages    ����ҳ��
        ' ����
        '                      ����Чҳ����
        '----------------------------------------------------------------
        Public Function doMoveToPage( _
            ByVal intToPage As Integer, _
            ByVal intTotalPages As Integer) As Integer

            doMoveToPage = 0
            Try
                '�����1ҳ
                If intToPage < 0 Then
                    doMoveToPage = intTotalPages - 1
                    Exit Try
                End If

                '����1ҳ
                If intToPage >= intTotalPages Then
                    doMoveToPage = 0
                    Exit Try
                End If

                '��ָ��ҳ
                doMoveToPage = intToPage

            Catch ex As Exception
                doMoveToPage = 0
            End Try

        End Function

        '----------------------------------------------------------------
        ' �ƶ���ָ����¼��������Чҳ������������
        '     blnAllowPaging   �������ҳ
        '     intPageSize      ��ҳ���С
        '     intRecordNo      ����¼��(��0��ʼ)
        '     intPageIndex     ������ҳ����
        '     intSelectIndex   ������������
        ' ����
        '     True             ���ɹ�
        '     False            ��ʧ��
        '----------------------------------------------------------------
        Public Function doMoveToRecord( _
            ByVal blnAllowPaging As Boolean, _
            ByVal intPageSize As Integer, _
            ByVal intRecordNo As Integer, _
            ByRef intPageIndex As Integer, _
            ByRef intSelectIndex As Integer) As Boolean

            Try
                If blnAllowPaging = False Then
                    '����ҳ
                    intPageIndex = 0
                    intSelectIndex = intRecordNo
                Else
                    '��ҳ
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
        ' ���������ֲ����ж��Ƿ���Խ��С���ҳ������
        '     objDataGrid      ��DataGrid����
        '     intRowCount      ���������ݵ�������
        ' ����
        '     True             ����
        '     False            ������
        '----------------------------------------------------------------
        Public Function canDoMoveNextPage( _
            ByVal objDataGrid As System.Web.UI.WebControls.DataGrid, _
            ByVal intRowCount As Integer) As Boolean

            canDoMoveNextPage = False
            Try
                'û�м�¼
                If intRowCount < 1 Then
                    Exit Try
                End If
                '����1ҳ
                If objDataGrid.PageCount = 1 Then
                    Exit Try
                End If
                '��βҳ
                If objDataGrid.CurrentPageIndex = objDataGrid.PageCount - 1 Then
                    Exit Try
                End If
                '����������
                canDoMoveNextPage = True
            Catch ex As Exception
                canDoMoveNextPage = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ����Request�е���Ϣ�ָ�����ָ�����е�CheckBox״̬
        '     strErrMsg        �����ش�����Ϣ
        '     objDataGrid      ��DataGrid����
        '     objHttpRequest   ����ǰHttpRequest
        '     intColIndex      ��CheckBox������
        '     strCheckBoxId    ��CheckBox�ؼ�ID
        ' ����
        '     True             ���ɹ�
        '     False            ��ʧ��
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
                            'checkboxѡ��״̬��¼��objHttpRequestform�У�ֵon=checked
                            'ÿѡ��һ�Σ���������ͻ��˵�ǰ���ڷ�������Ϣ
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
        ' ����Request�е���Ϣ�ָ�����ָ�����е�CheckBox״̬
        '     strErrMsg        �����ش�����Ϣ
        '     objDataGrid      ��DataGrid����
        '     intColIndex      ��CheckBox������
        '     strCheckBoxId    ��CheckBox�ؼ�ID
        '     blnChecked       ����CheckBox״̬
        ' ����
        '     True             ���ɹ�
        '     False            ��ʧ��
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
        ' ��������ָ�����е�CheckBox״̬
        '     strErrMsg        �����ش�����Ϣ
        '     objDataGrid      ��DataGrid����
        '     intColIndex      ��CheckBox������
        '     strCheckBoxId    ��CheckBox�ؼ�ID
        '     blnChecked       ��(����)��CheckBox״̬
        ' ����
        '     True             ���ɹ�
        '     False            ��ʧ��
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
        ' ʹ������ָ�����е�CheckBox��ʹ��״̬
        '     strErrMsg        �����ش�����Ϣ
        '     objDataGrid      ��DataGrid����
        '     intColIndex      ��CheckBox������
        '     strCheckBoxId    ��CheckBox�ؼ�ID
        '     blnEnabled       ��Enabled
        ' ����
        '     True             ���ɹ�
        '     False            ��ʧ��
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
        ' ��������ָ�����е�CheckBox��Checked״̬
        '     strErrMsg        �����ش�����Ϣ
        '     objDataGrid      ��DataGrid����
        '     intColIndex      ��CheckBox������
        '     strCheckBoxId    ��CheckBox�ؼ�ID
        '     blnChecked       ��Checked
        ' ����
        '     True             ���ɹ�
        '     False            ��ʧ��
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
        ' ��������ָ�����е�CheckBox��Checked״̬
        '     strErrMsg        �����ش�����Ϣ
        '     objDataGrid      ��DataGrid����
        '     intRowIndex      ��CheckBox������
        '     intColIndex      ��CheckBox������
        '     strCheckBoxId    ��CheckBox�ؼ�ID
        '     blnChecked       ��Checked
        ' ����
        '     True             ���ɹ�
        '     False            ��ʧ��

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
        ' ���ָ�������е�ָ�����е�CheckBox��Checked״̬
        '     objDataGridItem  ����ǰ��DataGridItem
        '     intColIndex      ��CheckBox������
        '     strCheckBoxId    ��CheckBox�ؼ�ID
        ' ����
        '     True             ��Checked
        '     False            ��Unchecked
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
        ' ����ָ�������е�ָ�����е�CheckBox��Checked״̬
        '     objDataGridItem  ����ǰ��DataGridItem
        '     intColIndex      ��CheckBox������
        '     strCheckBoxId    ��CheckBox�ؼ�ID
        ' ����
        '     True             ���ɹ�
        '     False            ��ʧ��
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
        ' ��������ǰ��intRowIndex����ǰҳintPageIndex��ҳ��¼��intPageSize
        ' �����Ӧ��DataView�еļ�¼λ��
        '     intRowIndex      ������ǰ��
        '     intPageIndex     ����ǰҳ
        '     intPageSize      ��ҳ��¼��
        ' ����
        '                      ����Ӧ��DataView�еļ�¼λ��
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
        ' ִ�����������Ԥ�󶨣���ȷ�����ݵ��������������ǰ�Ĳ�����Ч����
        ' �õ�����
        '     strErrMsg        �����ش�����Ϣ
        '     objDataGrid      ��DataGrid����
        '     intRowCount      ����Ӧ����Դ�еļ�¼��
        ' ����
        '     True             ���ɹ�
        '     False            ��ʧ��
        '----------------------------------------------------------------
        Public Function onBeforeDataGridBind( _
            ByRef strErrMsg As String, _
            ByVal objDataGrid As System.Web.UI.WebControls.DataGrid, _
            ByVal intRowCount As Integer) As Boolean

            onBeforeDataGridBind = False

            Try
                'Ϊ��ֹ����������������ʧ�ܣ�������Ϊȱʡ״̬
                '������������
                Dim intPageIndex As Integer
                intPageIndex = objDataGrid.CurrentPageIndex
                Dim intSelectedIndex As Integer
                intSelectedIndex = objDataGrid.SelectedIndex

                '������������
                doCheckDataGridIndex(intRowCount, objDataGrid.AllowPaging, objDataGrid.PageSize, intPageIndex, intSelectedIndex)

                '������������
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
        ' �����������п�(����ָ����pixel��ȵ��еĿ�Ⱥ�)
        '     objDataGrid    ��DataGrid����
        ' ����
        '                    ������ָ����pixel��ȵ��еĿ�Ⱥ�
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
        ' ��ȡ��������ָ�������е�������(BoundColumn��ButtonColumn)
        '     objDataGrid      ��DataGrid
        '     strDataFieldName ����������
        ' ����
        '                      ������������
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
                    '����BoundColumn
                    Try
                        objBoundColumn = CType(objDataGrid.Columns(i), System.Web.UI.WebControls.BoundColumn)
                        If objBoundColumn.DataField = strDataFieldName Then
                            getDataGridColumnIndex = i
                            Exit Function
                        End If
                    Catch ex As Exception
                        objBoundColumn = Nothing
                    End Try

                    '����ButtonColumn
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
        ' ��ȡ��������ָ�������е�Stringֵ(BoundColumn��ButtonColumn)
        '     objDataGrid      ��DataGrid
        '     objDataGridItem  ��DataGrid�е�DataGridItem
        '     strDataFieldName ����������
        ' ����
        '                      ��������ֵ
        '----------------------------------------------------------------
        Public Function getDataGridCellValue( _
            ByVal objDataGrid As System.Web.UI.WebControls.DataGrid, _
            ByVal objDataGridItem As System.Web.UI.WebControls.DataGridItem, _
            ByVal strDataFieldName As String) As String

            Try
                '����strDataFieldName����������
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
        ' ��ȡ��������ָ�������е�Stringֵ
        '     objDataGridItem  ��DataGrid�е�DataGridItem
        '     intColIndex      ������������
        ' ����
        '                      ��������ֵ
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
        ' ʹ��DataGrid(��ʹ����ͷ)
        '     strErrMsg        �����ش�����Ϣ
        '     objDataGrid      ��DataGrid
        '     blnEnabled       ��ʹ�ܿ���
        ' ����
        '                      ��������ֵ
        '----------------------------------------------------------------
        Public Function doEnabledDataGrid( _
            ByRef strErrMsg As String, _
            ByVal objDataGrid As System.Web.UI.WebControls.DataGrid, _
            ByVal blnEnabled As Boolean) As Boolean

            Try
                Dim intStart As Integer
                intStart = 0

                '��ȡ�����С�����
                Dim intRows As Integer
                intRows = objDataGrid.Items.Count
                Dim intCols As Integer
                intCols = objDataGrid.Columns.Count

                '����ʹ����������
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
        ' ��ȡָ�����е�strControlId��Postback���ײ���
        '     objCommandSource ��Object
        ' ����
        '                      ��Postback���ײ���
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
