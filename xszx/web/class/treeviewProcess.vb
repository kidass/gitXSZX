Imports System

Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.web
    ' ����    ��TreeviewProcess
    '
    ' ����������
    '     treeview������йش���
    '----------------------------------------------------------------

    Public Class TreeviewProcess
        Implements IDisposable

        '���ؼ����ID�ָ���
        Public Const CharTreeNodeIdFGF As String = "$"

        '���ؼ����Index�ָ���
        Public Const CharTreeNodeIndexFGF As String = "."








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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.web.TreeviewProcess)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub









        '----------------------------------------------------------------
        ' ����������ID��ȡ���������еĴ����ֶε�����
        ' ID��ʽ: XXX �ָ��� �ڵ�������(Index) �ָ��� ����ֵ
        '     strFixed      ���׽��ַ���
        '     strNodeIndex  ���ڵ�NodeIndex
        '     strCodevalue  ���ڵ��Ӧ����ֵ
        ' ����
        '                   ���ϳɺ��ID
        '----------------------------------------------------------------
        Public Function getNodeId( _
            ByVal strFixed As String, _
            ByVal strNodeIndex As String, _
            ByVal strCodevalue As String) As String

            Try
                If strFixed.Length > 0 Then strFixed = strFixed.Trim()
                If strNodeIndex.Length > 0 Then strNodeIndex = strNodeIndex.Trim()
                If strCodevalue.Length > 0 Then strCodevalue = strCodevalue.Trim()
                getNodeId = strFixed + CharTreeNodeIdFGF + strNodeIndex + CharTreeNodeIdFGF + strCodevalue
            Catch ex As Exception
                getNodeId = ""
            End Try

        End Function

        '----------------------------------------------------------------
        ' ����������ID��ȡ���������еĴ����ֶε�����
        ' ID��ʽ: XXX �ָ��� �ڵ�������(Index) �ָ��� ����ֵ
        '     strNodeId     ��treeview�ڵ��ID
        ' ����
        '                   ��ID�д洢�Ĵ���ֵ
        '----------------------------------------------------------------
        Public Function getCodeValueFromNodeId( _
            ByVal strNodeId As String) As String

            Dim strCode As String = ""
            Try
                Dim strValue() As String
                strValue = strNodeId.Split(CharTreeNodeIdFGF.ToCharArray())
                strCode = strValue(2)
            Catch ex As Exception
                strCode = ""
            End Try
            If strCode.Length > 0 Then strCode = strCode.Trim()
            getCodeValueFromNodeId = strCode

        End Function

        '----------------------------------------------------------------
        ' ����������ID��ȡ���������е�NodeIndex������
        ' ID��ʽ: XXX �ָ��� �ڵ�������(Index) �ָ��� ����ֵ
        '     strNodeId     ��treeview�ڵ��ID
        ' ����
        '                   ��ID�д洢��NodeIndex
        '----------------------------------------------------------------
        Public Function getNodeIndexFromNodeId( _
            ByVal strNodeId As String) As String

            Dim strCode As String = ""
            Try
                Dim strValue() As String
                strValue = strNodeId.Split(CharTreeNodeIdFGF.ToCharArray())
                strCode = strValue(1)
            Catch ex As Exception
                strCode = ""
            End Try
            If strCode.Length > 0 Then strCode = strCode.Trim()
            getNodeIndexFromNodeId = strCode

        End Function

        '----------------------------------------------------------------
        ' ���ݽڵ������Ż�ȡָ������intLevel��Index(Integer)
        ' �ڵ������Ÿ�ʽ����1������ �� ��2������ �� ��3������ �� ��4������ �� ...
        '     strNodeIndex  ��treeview�ڵ��NodeIndex
        '     intLevel      ��Ҫ��ȡ�Ľڵ㼶��(��1��ʼ)
        ' ����
        '                   ��ָ�������Index(-1��ʾ����)
        '----------------------------------------------------------------
        Public Function getLevelIndexFromNodeIndex( _
            ByVal strNodeIndex As String, _
            ByVal intLevel As Integer) As Integer

            Dim intIndex As Integer = -1
            Try
                Dim strValue() As String
                strValue = strNodeIndex.Split(CharTreeNodeIndexFGF.ToCharArray())
                intIndex = CType(strValue(intLevel - 1), Integer)
            Catch ex As Exception
                intIndex = -1
            End Try
            getLevelIndexFromNodeIndex = intIndex

        End Function

        '----------------------------------------------------------------
        ' ���ݽڵ������Ż�ȡ�ڵ�ļ���
        ' �ڵ������Ÿ�ʽ����1������ �� ��2������ �� ��3������ �� ��4������ �� ...
        '     strNodeIndex  ��treeview�ڵ��NodeIndex
        ' ����
        '                   ���ڵ㼶�𣬴�1��ʼ(-1��ʾ����)
        '----------------------------------------------------------------
        Public Function getLevelIndexFromNodeIndex( _
            ByVal strNodeIndex As String) As Integer

            Dim intLevel As Integer = -1
            Try
                Dim strValue() As String
                strValue = strNodeIndex.Split(CharTreeNodeIndexFGF.ToCharArray())
                intLevel = strValue.Length
            Catch ex As Exception
                intLevel = -1
            End Try
            getLevelIndexFromNodeIndex = intLevel

        End Function

        '----------------------------------------------------------------
        ' ���ݽڵ������Ż�ȡָ������intLevel������������
        ' �ڵ������Ÿ�ʽ����1������ �� ��2������ �� ��3������ �� ��4������ �� ...
        '     strNodeIndex  ��treeview�ڵ��NodeIndex
        '     intLevel      ��Ҫ��ȡ�Ľڵ㼶��(��1��ʼ)
        ' ����
        '                   ��ָ�������������
        '----------------------------------------------------------------
        Public Function getLevelIndexFromNodeIndex( _
            ByVal strNodeIndex As String, _
            ByVal intLevel As Integer, _
            ByVal blnUnused As Boolean) As String

            Dim strIndex As String = ""
            Try
                Dim strValue() As String
                strValue = strNodeIndex.Split(CharTreeNodeIndexFGF.ToCharArray())
                Dim i As Integer
                For i = 0 To intLevel - 1 Step 1
                    If strIndex = "" Then
                        strIndex = strValue(i).Trim()
                    Else
                        strIndex = strIndex + Me.CharTreeNodeIndexFGF + strValue(i).Trim()
                    End If
                Next
            Catch ex As Exception
                strIndex = ""
            End Try
            getLevelIndexFromNodeIndex = strIndex

        End Function

        '----------------------------------------------------------------
        ' ���ݴ���ֵ��TreeView��������Ӧ��treenode
        '     objTreeView    �����ؼ�
        '     strCodeValue   ������ֵ
        ' ����
        '                   ��treenode
        '----------------------------------------------------------------
        Public Function getTreeNodeByValue( _
            ByVal objTreeView As Microsoft.Web.UI.WebControls.TreeView, _
            ByVal strCodeValue As String) As Microsoft.Web.UI.WebControls.TreeNode

            Dim objTreeNode As Microsoft.Web.UI.WebControls.TreeNode

            Try
                Dim strValue As String
                Dim intCount As Integer
                Dim i As Integer
                intCount = objTreeView.Nodes.Count

                '����������
                For i = 0 To intCount - 1 Step 1
                    strValue = Me.getCodeValueFromNodeId(objTreeView.Nodes(i).ID)
                    If strValue = strCodeValue Then
                        objTreeNode = objTreeView.Nodes(i)
                        GoTo lblFound
                    End If
                Next

                '�¼�������
                For i = 0 To intCount - 1 Step 1
                    If objTreeView.Nodes(i).Nodes.Count > 0 Then
                        objTreeNode = Me.getTreeNodeByValue(objTreeView.Nodes(i), strCodeValue)
                        If Not (objTreeNode Is Nothing) Then
                            GoTo lblFound
                        End If
                    End If
                Next
            Catch ex As Exception
                objTreeNode = Nothing
            End Try
lblFound:
            getTreeNodeByValue = objTreeNode

        End Function

        '----------------------------------------------------------------
        ' ���ݴ���ֵ�ڵ�ǰTreeNode��������Ӧ��treenode
        '     objParent      ����ǰ�ڵ�
        '     strCodeValue   ������ֵ
        ' ����
        '                   ��treenode
        '----------------------------------------------------------------
        Public Function getTreeNodeByValue( _
            ByVal objParent As Microsoft.Web.UI.WebControls.TreeNode, _
            ByVal strCodeValue As String) As Microsoft.Web.UI.WebControls.TreeNode

            Dim objTreeNode As Microsoft.Web.UI.WebControls.TreeNode

            Try
                Dim strValue As String
                Dim intCount As Integer
                Dim i As Integer
                intCount = objParent.Nodes.Count

                '����������
                For i = 0 To intCount - 1 Step 1
                    strValue = Me.getCodeValueFromNodeId(objParent.Nodes(i).ID)
                    If strValue = strCodeValue Then
                        objTreeNode = objParent.Nodes(i)
                        GoTo lblFound
                    End If
                Next

                '�¼�������
                For i = 0 To intCount - 1 Step 1
                    If objParent.Nodes(i).Nodes.Count > 0 Then
                        objTreeNode = Me.getTreeNodeByValue(objParent.Nodes(i), strCodeValue)
                        If Not (objTreeNode Is Nothing) Then
                            GoTo lblFound
                        End If
                    End If
                Next
            Catch ex As Exception
                objTreeNode = Nothing
            End Try
lblFound:
            getTreeNodeByValue = objTreeNode

        End Function

        '----------------------------------------------------------------
        ' ������������������ʾ��treeview��
        '     strErrMsg      ����������򷵻ش�����Ϣ
        '     objTreeView    �����ؼ�
        '     objDataTable   ��Ҫ��ʾ������
        '     strCodeField   �����Լ����ķּ��ֶ�����XXX-XXXX-XXXXX-...��
        '     strNameField   ���ڵ�Ҫ��ʾ��������
        '     blnChecked     ���Ƿ���ʾCheckBox
        '     blnClear       ��ǿ���ؽ��ڵ�
        '     intFJCDSM      ���ֶηּ�����˵��(��1���ܳ���,��2���ܳ���,��3���ܳ���,...)
        '----------------------------------------------------------------
        Public Function doDisplayTreeViewAll( _
            ByRef strErrMsg As String, _
            ByRef objTreeView As Microsoft.Web.UI.WebControls.TreeView, _
            ByVal objDataTable As System.Data.DataTable, _
            ByVal strCodeField As String, _
            ByVal strNameField As String, _
            ByVal blnChecked As Boolean, _
            ByVal blnClear As Boolean, _
            ByVal intFJCDSM() As Integer) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objTopDataTable As System.Data.DataTable
            Dim objXJDataTable As System.Data.DataTable

            doDisplayTreeViewAll = False

            '���
            If objDataTable Is Nothing Then
                strErrMsg = "����û�����ݣ�"
                GoTo errProc
            End If
            If intFJCDSM.Length < 1 Then
                strErrMsg = "����δָ���Ĵ���ּ����ȣ�"
                GoTo errProc
            End If

            '���
            Dim strNodeIndex As String
            strNodeIndex = objTreeView.SelectedNodeIndex
            If strNodeIndex.Length > 0 Then strNodeIndex = strNodeIndex.Trim()
            If blnClear = True Then
                objTreeView.Nodes.Clear()
            Else
                '�����ؽ�
                If objTreeView.Nodes.Count > 0 Then
                    GoTo normExit
                End If
            End If

            '��¡
            Try
                objTopDataTable = objDataTable.Copy()
                objXJDataTable = objDataTable.Copy()
                With objTopDataTable.DefaultView
                    .RowFilter = "len(trim(" + strCodeField + ")) = " + intFJCDSM(0).ToString()
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            '���뵽TreeView
            Dim objNode As Microsoft.Web.UI.WebControls.TreeNode
            Dim intCount As Integer
            Dim i As Integer
            Dim strDM As String
            Dim strMC As String
            intCount = objTopDataTable.DefaultView.Count
            For i = 0 To intCount - 1 Step 1
                Try
                    '��ȡ��Ϣ
                    With objTopDataTable.DefaultView
                        strDM = objPulicParameters.getObjectValue(.Item(i).Item(strCodeField), "")
                        strMC = objPulicParameters.getObjectValue(.Item(i).Item(strNameField), "")
                    End With

                    '���嶥��ڵ�
                    objNode = Nothing
                    objNode = New Microsoft.Web.UI.WebControls.TreeNode
                    objNode.CheckBox = blnChecked
                    objNode.Text = strMC
                    objNode.Expanded = True

                    '��Ӷ���ڵ�
                    objTreeView.Nodes.Add(objNode)

                    '��¼NodeIndex,strDM
                    objNode.ID = Me.getNodeId("A", objNode.GetNodeIndex(), strDM)

                    '�����Ƿ�����¼��ڵ�
                    If 1 < intFJCDSM.Length Then

                        With objXJDataTable.DefaultView
                            .RowFilter = strCodeField + " like '" + strDM + "%' and len(trim(" + strCodeField + ")) = " + intFJCDSM(1).ToString()
                            If .Count > 0 Then
                                '��ʾ�¼��ڵ�
                                If Me.doDisplayTreeViewChild(strErrMsg, objNode, objXJDataTable, strCodeField, strNameField, blnChecked, intFJCDSM, 1) = False Then
                                    GoTo errProc
                                End If
                            End If
                        End With
                    End If
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
            Next

            '����ȱʡ�ڵ�
            If strNodeIndex <> "" Then
                objNode = objTreeView.GetNodeFromIndex(strNodeIndex)
                If objNode Is Nothing Then
                    If objTreeView.Nodes.Count > 0 Then
                        objTreeView.SelectedNodeIndex = objTreeView.Nodes(0).GetNodeIndex()
                    End If
                Else
                    objTreeView.SelectedNodeIndex = strNodeIndex
                End If
            Else
                If objTreeView.Nodes.Count > 0 Then
                    objTreeView.SelectedNodeIndex = objTreeView.Nodes(0).GetNodeIndex()
                End If
            End If

normExit:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objTopDataTable)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objXJDataTable)

            doDisplayTreeViewAll = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objTopDataTable)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objXJDataTable)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ������������������ʾ��objNode���¼���
        '     strErrMsg      �������������ʾ������Ϣ
        '     objParent      �����ڵ�
        '     objDataTable   ��Ҫ��ʾ������
        '     strCodeField   �����Լ����ķּ��ֶ�����XXX-XXXX-XXXXX-...��
        '     strNameField   ���ڵ�Ҫ��ʾ��������
        '     blnChecked     ���Ƿ���ʾCheckBox
        '     intFJCDSM      ���ֶηּ�����˵��(��1���ܳ���,��2���ܳ���,��3���ܳ���,...)
        '     intLevel       ��objDataTable��ʾ�ļ���
        '----------------------------------------------------------------
        Private Function doDisplayTreeViewChild( _
            ByRef strErrMsg As String, _
            ByRef objParent As Microsoft.Web.UI.WebControls.TreeNode, _
            ByVal objDataTable As System.Data.DataTable, _
            ByVal strCodeField As String, _
            ByVal strNameField As String, _
            ByVal blnChecked As Boolean, _
            ByVal intFJCDSM() As Integer, _
            ByVal intLevel As Integer) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objXJDataTable As System.Data.DataTable

            doDisplayTreeViewChild = False

            '���
            If objDataTable Is Nothing Then
                strErrMsg = "����û�����ݣ�"
                GoTo errProc
            End If
            If intFJCDSM.Length < 1 Then
                strErrMsg = "����δָ���Ĵ���ּ����ȣ�"
                GoTo errProc
            End If

            '��¡
            Try
                objXJDataTable = objDataTable.Copy()
                objXJDataTable.DefaultView.RowFilter = objDataTable.DefaultView.RowFilter
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            '���뵽TreeView
            Dim objNode As Microsoft.Web.UI.WebControls.TreeNode
            Dim intCount As Integer
            Dim i As Integer
            Dim strDM As String
            Dim strMC As String
            intCount = objDataTable.DefaultView.Count
            For i = 0 To intCount - 1 Step 1
                Try
                    '��ȡ��Ϣ
                    With objDataTable.DefaultView
                        strDM = objPulicParameters.getObjectValue(.Item(i).Item(strCodeField), "")
                        strMC = objPulicParameters.getObjectValue(.Item(i).Item(strNameField), "")
                    End With

                    '����ڵ�
                    objNode = Nothing
                    objNode = New Microsoft.Web.UI.WebControls.TreeNode
                    objNode.CheckBox = blnChecked
                    objNode.Text = strMC
                    objNode.Expanded = True

                    '��ӽڵ�
                    objParent.Nodes.Add(objNode)

                    '��¼NodeIndex,strDM
                    objNode.ID = Me.getNodeId("A", objNode.GetNodeIndex(), strDM)

                    '�����Ƿ�����¼��ڵ�
                    If intLevel + 1 < intFJCDSM.Length Then
                        With objXJDataTable.DefaultView
                            .RowFilter = strCodeField + " like '" + strDM + "%' and len(trim(" + strCodeField + ")) = " + intFJCDSM(intLevel + 1).ToString()
                            If .Count > 0 Then
                                '��ʾ�¼��ڵ�
                                If Me.doDisplayTreeViewChild(strErrMsg, objNode, objXJDataTable, strCodeField, strNameField, blnChecked, intFJCDSM, intLevel + 1) = False Then
                                    GoTo errProc
                                End If
                            End If
                        End With
                    End If
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
            Next

normExit:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objXJDataTable)

            doDisplayTreeViewChild = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objXJDataTable)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ������������������ʾ��objNode���¼���
        '     strErrMsg         �������������ʾ������Ϣ
        '     objParent         �����ڵ�
        '     objDataTable      ��Ҫ��ʾ������
        '     strCodeField      �������ֶ���
        '     strNameField      ���ڵ�Ҫ��ʾ��������
        '     blnChecked        ���Ƿ���ʾCheckBox
        '     objExpandableValue��չ��ģʽ
        '     blnExpanded       ���Ƿ�չ��
        '----------------------------------------------------------------
        Public Function doShowTreeNodeChildren( _
            ByRef strErrMsg As String, _
            ByRef objParent As Microsoft.Web.UI.WebControls.TreeNode, _
            ByVal objDataTable As System.Data.DataTable, _
            ByVal strCodeField As String, _
            ByVal strNameField As String, _
            ByVal blnChecked As Boolean, _
            ByVal objExpandableValue As Microsoft.Web.UI.WebControls.ExpandableValue, _
            ByVal blnExpanded As Boolean) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objXJDataTable As System.Data.DataTable

            doShowTreeNodeChildren = False

            '���
            If objDataTable Is Nothing Then
                strErrMsg = "����û�����ݣ�"
                GoTo errProc
            End If

            '��¡
            Try
                objXJDataTable = objDataTable.Copy()
                objXJDataTable.DefaultView.RowFilter = objDataTable.DefaultView.RowFilter
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            '���뵽TreeView
            Dim objNode As Microsoft.Web.UI.WebControls.TreeNode
            Dim intCount As Integer
            Dim i As Integer
            Dim strDM As String
            Dim strMC As String
            intCount = objDataTable.DefaultView.Count
            For i = 0 To intCount - 1 Step 1
                Try
                    '��ȡ��Ϣ
                    With objDataTable.DefaultView
                        strDM = objPulicParameters.getObjectValue(.Item(i).Item(strCodeField), "")
                        strMC = objPulicParameters.getObjectValue(.Item(i).Item(strNameField), "")
                    End With

                    '����ڵ�
                    objNode = Nothing
                    objNode = New Microsoft.Web.UI.WebControls.TreeNode
                    objNode.CheckBox = blnChecked
                    objNode.Text = strMC
                    objNode.Expanded = blnExpanded
                    objNode.Expandable = objExpandableValue

                    '��ӽڵ�
                    objParent.Nodes.Add(objNode)

                    '��¼NodeIndex,strDM
                    objNode.ID = Me.getNodeId("A", objNode.GetNodeIndex(), strDM)

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
            Next

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objXJDataTable)

            doShowTreeNodeChildren = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objXJDataTable)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ������������������ʾ��treeview��
        '     strErrMsg      ����������򷵻ش�����Ϣ
        '     objTreeView    �����ؼ�
        '     objDataTable   ��Ҫ��ʾ������
        '     strCodeField   ���ֶ�����XXX.XXX.XXX.XXX...��
        '     strNameField   ���ڵ�Ҫ��ʾ��������
        '     strDmjbField   �����뼶���ֶ�
        '     blnChecked     ���Ƿ���ʾCheckBox
        '     blnClear       ��ǿ���ؽ��ڵ�
        '----------------------------------------------------------------
        Public Function doDisplayTreeViewAll( _
            ByRef strErrMsg As String, _
            ByRef objTreeView As Microsoft.Web.UI.WebControls.TreeView, _
            ByVal objDataTable As System.Data.DataTable, _
            ByVal strCodeField As String, _
            ByVal strNameField As String, _
            ByVal strDmjbField As String, _
            ByVal blnChecked As Boolean, _
            ByVal blnClear As Boolean) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objTopDataTable As System.Data.DataTable
            Dim objXJDataTable As System.Data.DataTable

            doDisplayTreeViewAll = False

            '���
            If objDataTable Is Nothing Then
                strErrMsg = "����û�����ݣ�"
                GoTo errProc
            End If
            If strCodeField Is Nothing Then strCodeField = ""
            If strNameField Is Nothing Then strNameField = ""
            If strDmjbField Is Nothing Then strDmjbField = ""
            strCodeField = strCodeField.Trim()
            strNameField = strNameField.Trim()
            strDmjbField = strDmjbField.Trim()
            If strCodeField = "" Or strNameField = "" Or strDmjbField = "" Then
                strErrMsg = "���󣺽ӿڲ���û�����룡"
                GoTo errProc
            End If

            '���
            Dim strNodeIndex As String
            strNodeIndex = objTreeView.SelectedNodeIndex
            If strNodeIndex.Length > 0 Then strNodeIndex = strNodeIndex.Trim()
            If blnClear = True Then
                objTreeView.Nodes.Clear()
            Else
                '�����ؽ�
                If objTreeView.Nodes.Count > 0 Then
                    GoTo normExit
                End If
            End If

            '��¡
            Try
                objTopDataTable = objDataTable.Copy()
                objXJDataTable = objDataTable.Copy()
                With objTopDataTable.DefaultView
                    .RowFilter = strDmjbField + " = 1"
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            '���뵽TreeView
            Dim objNode As Microsoft.Web.UI.WebControls.TreeNode
            Dim intCount As Integer
            Dim i As Integer
            Dim strDM As String
            Dim strMC As String
            intCount = objTopDataTable.DefaultView.Count
            For i = 0 To intCount - 1 Step 1
                Try
                    '��ȡ��Ϣ
                    With objTopDataTable.DefaultView
                        strDM = objPulicParameters.getObjectValue(.Item(i).Item(strCodeField), "")
                        strMC = objPulicParameters.getObjectValue(.Item(i).Item(strNameField), "")
                    End With

                    '���嶥��ڵ�
                    objNode = Nothing
                    objNode = New Microsoft.Web.UI.WebControls.TreeNode
                    objNode.CheckBox = blnChecked
                    objNode.Text = strMC
                    objNode.Expanded = True

                    '��Ӷ���ڵ�
                    objTreeView.Nodes.Add(objNode)

                    '��¼NodeIndex,strDM
                    objNode.ID = Me.getNodeId("A", objNode.GetNodeIndex(), strDM)

                    '�����Ƿ�����¼��ڵ�
                    With objXJDataTable.DefaultView
                        .RowFilter = strCodeField + " like '" + strDM + Xydc.Platform.Common.Utilities.PulicParameters.CharFjdmSeparate + "%' and " + strDmjbField + " = 2"
                        If .Count > 0 Then
                            '��ʾ�¼��ڵ�
                            If Me.doDisplayTreeViewChild(strErrMsg, objNode, objXJDataTable, strCodeField, strNameField, strDmjbField, blnChecked, 2) = False Then
                                GoTo errProc
                            End If
                        End If
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
            Next

            '����ȱʡ�ڵ�
            If strNodeIndex <> "" Then
                objNode = objTreeView.GetNodeFromIndex(strNodeIndex)
                If objNode Is Nothing Then
                    If objTreeView.Nodes.Count > 0 Then
                        objTreeView.SelectedNodeIndex = objTreeView.Nodes(0).GetNodeIndex()
                    End If
                Else
                    objTreeView.SelectedNodeIndex = strNodeIndex
                End If
            Else
                If objTreeView.Nodes.Count > 0 Then
                    objTreeView.SelectedNodeIndex = objTreeView.Nodes(0).GetNodeIndex()
                End If
            End If

normExit:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objTopDataTable)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objXJDataTable)

            doDisplayTreeViewAll = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objTopDataTable)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objXJDataTable)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ������������������ʾ��objNode���¼���
        '     strErrMsg      �������������ʾ������Ϣ
        '     objParent      �����ڵ�
        '     objDataTable   ��Ҫ��ʾ������
        '     strCodeField   �����Լ����ķּ��ֶ���(XXX.XXX.XXX.XXX...)
        '     strNameField   ���ڵ�Ҫ��ʾ��������
        '     strDmjbField   �����뼶���ֶ�
        '     blnChecked     ���Ƿ���ʾCheckBox
        '     intLevel       ��objDataTable��ʾ�ļ���
        '----------------------------------------------------------------
        Private Function doDisplayTreeViewChild( _
            ByRef strErrMsg As String, _
            ByRef objParent As Microsoft.Web.UI.WebControls.TreeNode, _
            ByVal objDataTable As System.Data.DataTable, _
            ByVal strCodeField As String, _
            ByVal strNameField As String, _
            ByVal strDmjbField As String, _
            ByVal blnChecked As Boolean, _
            ByVal intLevel As Integer) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objXJDataTable As System.Data.DataTable

            doDisplayTreeViewChild = False

            '���
            If objDataTable Is Nothing Then
                strErrMsg = "����û�����ݣ�"
                GoTo errProc
            End If

            '��¡
            Try
                objXJDataTable = objDataTable.Copy()
                objXJDataTable.DefaultView.RowFilter = objDataTable.DefaultView.RowFilter
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            '���뵽TreeView
            Dim objNode As Microsoft.Web.UI.WebControls.TreeNode
            Dim intCount As Integer
            Dim i As Integer
            Dim strDM As String
            Dim strMC As String
            intCount = objDataTable.DefaultView.Count
            For i = 0 To intCount - 1 Step 1
                Try
                    '��ȡ��Ϣ
                    With objDataTable.DefaultView
                        strDM = objPulicParameters.getObjectValue(.Item(i).Item(strCodeField), "")
                        strMC = objPulicParameters.getObjectValue(.Item(i).Item(strNameField), "")
                    End With

                    '����ڵ�
                    objNode = Nothing
                    objNode = New Microsoft.Web.UI.WebControls.TreeNode
                    objNode.CheckBox = blnChecked
                    objNode.Text = strMC
                    objNode.Expanded = True

                    '��ӽڵ�
                    objParent.Nodes.Add(objNode)

                    '��¼NodeIndex,strDM
                    objNode.ID = Me.getNodeId("A", objNode.GetNodeIndex(), strDM)

                    '�����Ƿ�����¼��ڵ�
                    With objXJDataTable.DefaultView
                        .RowFilter = strCodeField + " like '" + strDM + Xydc.Platform.Common.Utilities.PulicParameters.CharFjdmSeparate + "%' and " + strDmjbField + " = " + (intLevel + 1).ToString()
                        If .Count > 0 Then
                            '��ʾ�¼��ڵ�
                            If Me.doDisplayTreeViewChild(strErrMsg, objNode, objXJDataTable, strCodeField, strNameField, strDmjbField, blnChecked, intLevel + 1) = False Then
                                GoTo errProc
                            End If
                        End If
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
            Next

normExit:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objXJDataTable)

            doDisplayTreeViewChild = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objXJDataTable)
            Exit Function

        End Function






        '----------------------------------------------------------------
        ' ��������������ʾ��treeview����
        '     strErrMsg      ����������򷵻ش�����Ϣ
        '     objTreeView    �����ؼ�
        '     objDataTable   ��Ҫ��ʾ������
        '     strCodeField   �������ֶ�
        '     strNameField   �������ֶ�
        '     blnChecked     ���Ƿ���ʾCheckBox
        '     blnClear       ��ǿ���ؽ��ڵ�
        '----------------------------------------------------------------
        Public Function doDisplayTreeView( _
            ByRef strErrMsg As String, _
            ByRef objTreeView As Microsoft.Web.UI.WebControls.TreeView, _
            ByVal objDataTable As System.Data.DataTable, _
            ByVal strCodeField As String, _
            ByVal strNameField As String, _
            ByVal blnChecked As Boolean, _
            ByVal blnClear As Boolean) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            doDisplayTreeView = False

            '���
            If objTreeView Is Nothing Then
                strErrMsg = "����û��ָ��TreeView�ؼ���"
                GoTo errProc
            End If
            If objDataTable Is Nothing Then
                strErrMsg = "����û�����ݣ�"
                GoTo errProc
            End If
            If strCodeField Is Nothing Then strCodeField = ""
            If strNameField Is Nothing Then strNameField = ""
            strCodeField = strCodeField.Trim()
            strNameField = strNameField.Trim()
            If strCodeField = "" Or strNameField = "" Then
                strErrMsg = "���󣺽ӿڲ���û�����룡"
                GoTo errProc
            End If

            '���
            Dim strNodeIndex As String
            strNodeIndex = objTreeView.SelectedNodeIndex
            If strNodeIndex.Length > 0 Then strNodeIndex = strNodeIndex.Trim()
            If blnClear = True Then
                objTreeView.Nodes.Clear()
            Else
                '�����ؽ�
                If objTreeView.Nodes.Count > 0 Then
                    GoTo normExit
                End If
            End If

            '���뵽TreeView
            Dim objNode As Microsoft.Web.UI.WebControls.TreeNode
            Dim intCount As Integer
            Dim i As Integer
            Dim strDM As String
            Dim strMC As String
            intCount = objDataTable.DefaultView.Count
            For i = 0 To intCount - 1 Step 1
                Try
                    '��ȡ��Ϣ
                    With objDataTable.DefaultView
                        strDM = objPulicParameters.getObjectValue(.Item(i).Item(strCodeField), "")
                        strMC = objPulicParameters.getObjectValue(.Item(i).Item(strNameField), "")
                    End With

                    '����ڵ�
                    objNode = Nothing
                    objNode = New Microsoft.Web.UI.WebControls.TreeNode
                    objNode.CheckBox = blnChecked
                    objNode.Text = strMC
                    objNode.Expandable = Microsoft.Web.UI.WebControls.ExpandableValue.CheckOnce

                    '��ӽڵ�
                    objTreeView.Nodes.Add(objNode)

                    '��¼NodeIndex,strDM
                    objNode.ID = Me.getNodeId("A", objNode.GetNodeIndex(), strDM)

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
            Next

            '����ȱʡ�ڵ�
            If strNodeIndex <> "" Then
                objNode = objTreeView.GetNodeFromIndex(strNodeIndex)
                If objNode Is Nothing Then
                    If objTreeView.Nodes.Count > 0 Then
                        objTreeView.SelectedNodeIndex = objTreeView.Nodes(0).GetNodeIndex()
                    End If
                Else
                    objTreeView.SelectedNodeIndex = strNodeIndex
                End If
            Else
                If objTreeView.Nodes.Count > 0 Then
                    objTreeView.SelectedNodeIndex = objTreeView.Nodes(0).GetNodeIndex()
                End If
            End If

normExit:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            doDisplayTreeView = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��������������ʾ��ָ���ڵ�objTreeNode���¼�
        '     strErrMsg      ����������򷵻ش�����Ϣ
        '     objTreeNode    ��ָ���ڵ�
        '     objDataTable   ��Ҫ��ʾ������
        '     strCodeField   �������ֶ�
        '     strNameField   �������ֶ�
        '     blnChecked     ���Ƿ���ʾCheckBox
        '     blnClear       ��ǿ���ؽ��ڵ�
        '----------------------------------------------------------------
        Public Function doDisplayTreeView( _
            ByRef strErrMsg As String, _
            ByRef objTreeNode As Microsoft.Web.UI.WebControls.TreeNode, _
            ByVal objDataTable As System.Data.DataTable, _
            ByVal strCodeField As String, _
            ByVal strNameField As String, _
            ByVal blnChecked As Boolean, _
            ByVal blnClear As Boolean) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            doDisplayTreeView = False

            '���
            If objDataTable Is Nothing Then
                strErrMsg = "����û�����ݣ�"
                GoTo errProc
            End If
            If objTreeNode Is Nothing Then
                strErrMsg = "����û��ָ�����ڵ㣡"
                GoTo errProc
            End If
            If strCodeField Is Nothing Then strCodeField = ""
            If strNameField Is Nothing Then strNameField = ""
            strCodeField = strCodeField.Trim()
            strNameField = strNameField.Trim()
            If strCodeField = "" Or strNameField = "" Then
                strErrMsg = "���󣺽ӿڲ���û�����룡"
                GoTo errProc
            End If

            '���
            If blnClear = True Then
                objTreeNode.Nodes.Clear()
            Else
                '�����ؽ�
                If objTreeNode.Nodes.Count > 0 Then
                    GoTo normExit
                End If
            End If

            '���뵽TreeView
            Dim objNode As Microsoft.Web.UI.WebControls.TreeNode
            Dim intCount As Integer
            Dim i As Integer
            Dim strDM As String
            Dim strMC As String
            intCount = objDataTable.DefaultView.Count
            For i = 0 To intCount - 1 Step 1
                Try
                    '��ȡ��Ϣ
                    With objDataTable.DefaultView
                        strDM = objPulicParameters.getObjectValue(.Item(i).Item(strCodeField), "")
                        strMC = objPulicParameters.getObjectValue(.Item(i).Item(strNameField), "")
                    End With

                    '����ڵ�
                    objNode = Nothing
                    objNode = New Microsoft.Web.UI.WebControls.TreeNode
                    objNode.CheckBox = blnChecked
                    objNode.Text = strMC
                    objNode.Expandable = Microsoft.Web.UI.WebControls.ExpandableValue.CheckOnce

                    '��ӽڵ�
                    objTreeNode.Nodes.Add(objNode)

                    '��¼NodeIndex,strDM
                    objNode.ID = Me.getNodeId("A", objNode.GetNodeIndex(), strDM)

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
            Next

normExit:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            doDisplayTreeView = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function




        '----------------------------------------------------------------
        ' չ����objTreeNode
        '     strErrMsg      �����ش�����Ϣ
        '     objTreeView    ��Microsoft.Web.UI.WebControls.TreeView
        '     objTreeNode    ��Microsoft.Web.UI.WebControls.TreeNode
        ' ����
        '     True           ���ɹ�
        '     False          ��ʧ��
        '----------------------------------------------------------------
        Public Function doExpandToNode( _
            ByRef strErrMsg As String, _
            ByVal objTreeView As Microsoft.Web.UI.WebControls.TreeView, _
            ByVal objTreeNode As Microsoft.Web.UI.WebControls.TreeNode) As Boolean

            doExpandToNode = False

            Try
                If objTreeView Is Nothing Then
                    Exit Try
                End If
                If objTreeNode Is Nothing Then
                    Exit Try
                End If

                Dim strNodeIndex() As String
                strNodeIndex = objTreeNode.GetNodeIndex.Split(Me.CharTreeNodeIndexFGF.ToCharArray)

                Dim objNode As Microsoft.Web.UI.WebControls.TreeNode
                Dim strIndex As String = ""
                Dim intCount As Integer
                Dim i As Integer
                intCount = strNodeIndex.Length
                For i = 0 To intCount - 1 Step 1
                    If strIndex = "" Then
                        strIndex = strNodeIndex(i)
                    Else
                        strIndex = strIndex + Me.CharTreeNodeIndexFGF + strNodeIndex(i)
                    End If

                    objNode = objTreeView.GetNodeFromIndex(strIndex)
                    If Not (objNode Is Nothing) Then
                        objNode.Expanded = True
                    End If
                Next

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doExpandToNode = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ������������������ʾ��objNode���¼���
        '     strErrMsg         �������������ʾ������Ϣ
        '     objParent         �����ڵ�
        '     objDataTable      ��Ҫ��ʾ������
        '     strCodeField      �������ֶ���
        '     strNameField      ���ڵ�Ҫ��ʾ��������
        '     blnChecked        ���Ƿ���ʾCheckBox
        '     objExpandableValue��չ��ģʽ
        '     blnExpanded       ���Ƿ�չ��
        '     strPrefix         ��ǰ׺�ַ�
        '----------------------------------------------------------------
        Public Function doShowTreeNodeChildren( _
            ByRef strErrMsg As String, _
            ByRef objParent As Microsoft.Web.UI.WebControls.TreeNode, _
            ByVal objDataTable As System.Data.DataTable, _
            ByVal strCodeField As String, _
            ByVal strNameField As String, _
            ByVal blnChecked As Boolean, _
            ByVal objExpandableValue As Microsoft.Web.UI.WebControls.ExpandableValue, _
            ByVal blnExpanded As Boolean, _
            ByVal strPrefix As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objXJDataTable As System.Data.DataTable

            doShowTreeNodeChildren = False

            '���
            If objDataTable Is Nothing Then
                strErrMsg = "����û�����ݣ�"
                GoTo errProc
            End If

            '��¡
            Try
                objXJDataTable = objDataTable.Copy()
                objXJDataTable.DefaultView.RowFilter = objDataTable.DefaultView.RowFilter
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try
            If strPrefix Is Nothing Then strPrefix = ""
            strPrefix = strPrefix.Trim
            If strPrefix = "" Then strPrefix = "A"

            '���뵽TreeView
            Dim objNode As Microsoft.Web.UI.WebControls.TreeNode
            Dim intCount As Integer
            Dim i As Integer
            Dim strDM As String
            Dim strMC As String
            intCount = objDataTable.DefaultView.Count
            For i = 0 To intCount - 1 Step 1
                Try
                    '��ȡ��Ϣ
                    With objDataTable.DefaultView
                        strDM = objPulicParameters.getObjectValue(.Item(i).Item(strCodeField), "")
                        strMC = objPulicParameters.getObjectValue(.Item(i).Item(strNameField), "")
                    End With

                    '����ڵ�
                    objNode = Nothing
                    objNode = New Microsoft.Web.UI.WebControls.TreeNode
                    objNode.CheckBox = blnChecked
                    objNode.Text = strMC
                    objNode.Expanded = blnExpanded
                    objNode.Expandable = objExpandableValue

                    '��ӽڵ�
                    objParent.Nodes.Add(objNode)

                    '��¼NodeIndex,strDM
                    objNode.ID = Me.getNodeId(strPrefix, objNode.GetNodeIndex(), strDM)

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
            Next

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objXJDataTable)

            doShowTreeNodeChildren = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objXJDataTable)
            Exit Function

        End Function

    End Class

End Namespace
