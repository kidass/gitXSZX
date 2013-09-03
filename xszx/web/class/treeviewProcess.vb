Imports System

Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.web
    ' 类名    ：TreeviewProcess
    '
    ' 功能描述：
    '     treeview对象的有关处理
    '----------------------------------------------------------------

    Public Class TreeviewProcess
        Implements IDisposable

        '树控件结点ID分隔符
        Public Const CharTreeNodeIdFGF As String = "$"

        '树控件结点Index分隔符
        Public Const CharTreeNodeIndexFGF As String = "."








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
        ' 根据树结点的ID获取储存在其中的代码字段的内容
        ' ID格式: XXX 分隔符 节点索引号(Index) 分隔符 代码值
        '     strFixed      ：首节字符串
        '     strNodeIndex  ：节点NodeIndex
        '     strCodevalue  ：节点对应代码值
        ' 返回
        '                   ：合成后的ID
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
        ' 根据树结点的ID获取储存在其中的代码字段的内容
        ' ID格式: XXX 分隔符 节点索引号(Index) 分隔符 代码值
        '     strNodeId     ：treeview节点的ID
        ' 返回
        '                   ：ID中存储的代码值
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
        ' 根据树结点的ID获取储存在其中的NodeIndex的内容
        ' ID格式: XXX 分隔符 节点索引号(Index) 分隔符 代码值
        '     strNodeId     ：treeview节点的ID
        ' 返回
        '                   ：ID中存储的NodeIndex
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
        ' 根据节点索引号获取指定级别intLevel的Index(Integer)
        ' 节点索引号格式：第1级索引 点 第2级索引 点 第3级索引 点 第4级索引 点 ...
        '     strNodeIndex  ：treeview节点的NodeIndex
        '     intLevel      ：要获取的节点级别(从1开始)
        ' 返回
        '                   ：指定级别的Index(-1表示错误)
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
        ' 根据节点索引号获取节点的级别
        ' 节点索引号格式：第1级索引 点 第2级索引 点 第3级索引 点 第4级索引 点 ...
        '     strNodeIndex  ：treeview节点的NodeIndex
        ' 返回
        '                   ：节点级别，从1开始(-1表示错误)
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
        ' 根据节点索引号获取指定级别intLevel的完整索引号
        ' 节点索引号格式：第1级索引 点 第2级索引 点 第3级索引 点 第4级索引 点 ...
        '     strNodeIndex  ：treeview节点的NodeIndex
        '     intLevel      ：要获取的节点级别(从1开始)
        ' 返回
        '                   ：指定级别的索引号
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
        ' 根据代码值在TreeView中搜索对应的treenode
        '     objTreeView    ：树控件
        '     strCodeValue   ：代码值
        ' 返回
        '                   ：treenode
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

                '本级中搜索
                For i = 0 To intCount - 1 Step 1
                    strValue = Me.getCodeValueFromNodeId(objTreeView.Nodes(i).ID)
                    If strValue = strCodeValue Then
                        objTreeNode = objTreeView.Nodes(i)
                        GoTo lblFound
                    End If
                Next

                '下级中搜索
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
        ' 根据代码值在当前TreeNode中搜索对应的treenode
        '     objParent      ：当前节点
        '     strCodeValue   ：代码值
        ' 返回
        '                   ：treenode
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

                '本级中搜索
                For i = 0 To intCount - 1 Step 1
                    strValue = Me.getCodeValueFromNodeId(objParent.Nodes(i).ID)
                    If strValue = strCodeValue Then
                        objTreeNode = objParent.Nodes(i)
                        GoTo lblFound
                    End If
                Next

                '下级中搜索
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
        ' 将给定的数据完整显示到treeview中
        '     strErrMsg      ：如果出错则返回错误信息
        '     objTreeView    ：树控件
        '     objDataTable   ：要显示的数据
        '     strCodeField   ：用以检索的分级字段名（XXX-XXXX-XXXXX-...）
        '     strNameField   ：节点要显示出的内容
        '     blnChecked     ：是否显示CheckBox
        '     blnClear       ：强制重建节点
        '     intFJCDSM      ：字段分级长度说明(第1级总长度,第2级总长度,第3级总长度,...)
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

            '检查
            If objDataTable Is Nothing Then
                strErrMsg = "错误：没有数据！"
                GoTo errProc
            End If
            If intFJCDSM.Length < 1 Then
                strErrMsg = "错误：未指定的代码分级长度！"
                GoTo errProc
            End If

            '清除
            Dim strNodeIndex As String
            strNodeIndex = objTreeView.SelectedNodeIndex
            If strNodeIndex.Length > 0 Then strNodeIndex = strNodeIndex.Trim()
            If blnClear = True Then
                objTreeView.Nodes.Clear()
            Else
                '不用重建
                If objTreeView.Nodes.Count > 0 Then
                    GoTo normExit
                End If
            End If

            '克隆
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

            '加入到TreeView
            Dim objNode As Microsoft.Web.UI.WebControls.TreeNode
            Dim intCount As Integer
            Dim i As Integer
            Dim strDM As String
            Dim strMC As String
            intCount = objTopDataTable.DefaultView.Count
            For i = 0 To intCount - 1 Step 1
                Try
                    '获取信息
                    With objTopDataTable.DefaultView
                        strDM = objPulicParameters.getObjectValue(.Item(i).Item(strCodeField), "")
                        strMC = objPulicParameters.getObjectValue(.Item(i).Item(strNameField), "")
                    End With

                    '定义顶层节点
                    objNode = Nothing
                    objNode = New Microsoft.Web.UI.WebControls.TreeNode
                    objNode.CheckBox = blnChecked
                    objNode.Text = strMC
                    objNode.Expanded = True

                    '添加顶层节点
                    objTreeView.Nodes.Add(objNode)

                    '记录NodeIndex,strDM
                    objNode.ID = Me.getNodeId("A", objNode.GetNodeIndex(), strDM)

                    '设置是否存在下级节点
                    If 1 < intFJCDSM.Length Then

                        With objXJDataTable.DefaultView
                            .RowFilter = strCodeField + " like '" + strDM + "%' and len(trim(" + strCodeField + ")) = " + intFJCDSM(1).ToString()
                            If .Count > 0 Then
                                '显示下级节点
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

            '设置缺省节点
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
        ' 将给定的数据完整显示到objNode的下级中
        '     strErrMsg      ：如果出错则显示错误信息
        '     objParent      ：父节点
        '     objDataTable   ：要显示的数据
        '     strCodeField   ：用以检索的分级字段名（XXX-XXXX-XXXXX-...）
        '     strNameField   ：节点要显示出的内容
        '     blnChecked     ：是否显示CheckBox
        '     intFJCDSM      ：字段分级长度说明(第1级总长度,第2级总长度,第3级总长度,...)
        '     intLevel       ：objDataTable显示的级别
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

            '检查
            If objDataTable Is Nothing Then
                strErrMsg = "错误：没有数据！"
                GoTo errProc
            End If
            If intFJCDSM.Length < 1 Then
                strErrMsg = "错误：未指定的代码分级长度！"
                GoTo errProc
            End If

            '克隆
            Try
                objXJDataTable = objDataTable.Copy()
                objXJDataTable.DefaultView.RowFilter = objDataTable.DefaultView.RowFilter
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            '加入到TreeView
            Dim objNode As Microsoft.Web.UI.WebControls.TreeNode
            Dim intCount As Integer
            Dim i As Integer
            Dim strDM As String
            Dim strMC As String
            intCount = objDataTable.DefaultView.Count
            For i = 0 To intCount - 1 Step 1
                Try
                    '获取信息
                    With objDataTable.DefaultView
                        strDM = objPulicParameters.getObjectValue(.Item(i).Item(strCodeField), "")
                        strMC = objPulicParameters.getObjectValue(.Item(i).Item(strNameField), "")
                    End With

                    '定义节点
                    objNode = Nothing
                    objNode = New Microsoft.Web.UI.WebControls.TreeNode
                    objNode.CheckBox = blnChecked
                    objNode.Text = strMC
                    objNode.Expanded = True

                    '添加节点
                    objParent.Nodes.Add(objNode)

                    '记录NodeIndex,strDM
                    objNode.ID = Me.getNodeId("A", objNode.GetNodeIndex(), strDM)

                    '设置是否存在下级节点
                    If intLevel + 1 < intFJCDSM.Length Then
                        With objXJDataTable.DefaultView
                            .RowFilter = strCodeField + " like '" + strDM + "%' and len(trim(" + strCodeField + ")) = " + intFJCDSM(intLevel + 1).ToString()
                            If .Count > 0 Then
                                '显示下级节点
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
        ' 将给定的数据完整显示到objNode的下级中
        '     strErrMsg         ：如果出错则显示错误信息
        '     objParent         ：父节点
        '     objDataTable      ：要显示的数据
        '     strCodeField      ：代码字段名
        '     strNameField      ：节点要显示出的内容
        '     blnChecked        ：是否显示CheckBox
        '     objExpandableValue：展开模式
        '     blnExpanded       ：是否展开
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

            '检查
            If objDataTable Is Nothing Then
                strErrMsg = "错误：没有数据！"
                GoTo errProc
            End If

            '克隆
            Try
                objXJDataTable = objDataTable.Copy()
                objXJDataTable.DefaultView.RowFilter = objDataTable.DefaultView.RowFilter
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            '加入到TreeView
            Dim objNode As Microsoft.Web.UI.WebControls.TreeNode
            Dim intCount As Integer
            Dim i As Integer
            Dim strDM As String
            Dim strMC As String
            intCount = objDataTable.DefaultView.Count
            For i = 0 To intCount - 1 Step 1
                Try
                    '获取信息
                    With objDataTable.DefaultView
                        strDM = objPulicParameters.getObjectValue(.Item(i).Item(strCodeField), "")
                        strMC = objPulicParameters.getObjectValue(.Item(i).Item(strNameField), "")
                    End With

                    '定义节点
                    objNode = Nothing
                    objNode = New Microsoft.Web.UI.WebControls.TreeNode
                    objNode.CheckBox = blnChecked
                    objNode.Text = strMC
                    objNode.Expanded = blnExpanded
                    objNode.Expandable = objExpandableValue

                    '添加节点
                    objParent.Nodes.Add(objNode)

                    '记录NodeIndex,strDM
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
        ' 将给定的数据完整显示到treeview中
        '     strErrMsg      ：如果出错则返回错误信息
        '     objTreeView    ：树控件
        '     objDataTable   ：要显示的数据
        '     strCodeField   ：字段名（XXX.XXX.XXX.XXX...）
        '     strNameField   ：节点要显示出的内容
        '     strDmjbField   ：代码级别字段
        '     blnChecked     ：是否显示CheckBox
        '     blnClear       ：强制重建节点
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

            '检查
            If objDataTable Is Nothing Then
                strErrMsg = "错误：没有数据！"
                GoTo errProc
            End If
            If strCodeField Is Nothing Then strCodeField = ""
            If strNameField Is Nothing Then strNameField = ""
            If strDmjbField Is Nothing Then strDmjbField = ""
            strCodeField = strCodeField.Trim()
            strNameField = strNameField.Trim()
            strDmjbField = strDmjbField.Trim()
            If strCodeField = "" Or strNameField = "" Or strDmjbField = "" Then
                strErrMsg = "错误：接口参数没有输入！"
                GoTo errProc
            End If

            '清除
            Dim strNodeIndex As String
            strNodeIndex = objTreeView.SelectedNodeIndex
            If strNodeIndex.Length > 0 Then strNodeIndex = strNodeIndex.Trim()
            If blnClear = True Then
                objTreeView.Nodes.Clear()
            Else
                '不用重建
                If objTreeView.Nodes.Count > 0 Then
                    GoTo normExit
                End If
            End If

            '克隆
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

            '加入到TreeView
            Dim objNode As Microsoft.Web.UI.WebControls.TreeNode
            Dim intCount As Integer
            Dim i As Integer
            Dim strDM As String
            Dim strMC As String
            intCount = objTopDataTable.DefaultView.Count
            For i = 0 To intCount - 1 Step 1
                Try
                    '获取信息
                    With objTopDataTable.DefaultView
                        strDM = objPulicParameters.getObjectValue(.Item(i).Item(strCodeField), "")
                        strMC = objPulicParameters.getObjectValue(.Item(i).Item(strNameField), "")
                    End With

                    '定义顶层节点
                    objNode = Nothing
                    objNode = New Microsoft.Web.UI.WebControls.TreeNode
                    objNode.CheckBox = blnChecked
                    objNode.Text = strMC
                    objNode.Expanded = True

                    '添加顶层节点
                    objTreeView.Nodes.Add(objNode)

                    '记录NodeIndex,strDM
                    objNode.ID = Me.getNodeId("A", objNode.GetNodeIndex(), strDM)

                    '设置是否存在下级节点
                    With objXJDataTable.DefaultView
                        .RowFilter = strCodeField + " like '" + strDM + Xydc.Platform.Common.Utilities.PulicParameters.CharFjdmSeparate + "%' and " + strDmjbField + " = 2"
                        If .Count > 0 Then
                            '显示下级节点
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

            '设置缺省节点
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
        ' 将给定的数据完整显示到objNode的下级中
        '     strErrMsg      ：如果出错则显示错误信息
        '     objParent      ：父节点
        '     objDataTable   ：要显示的数据
        '     strCodeField   ：用以检索的分级字段名(XXX.XXX.XXX.XXX...)
        '     strNameField   ：节点要显示出的内容
        '     strDmjbField   ：代码级别字段
        '     blnChecked     ：是否显示CheckBox
        '     intLevel       ：objDataTable显示的级别
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

            '检查
            If objDataTable Is Nothing Then
                strErrMsg = "错误：没有数据！"
                GoTo errProc
            End If

            '克隆
            Try
                objXJDataTable = objDataTable.Copy()
                objXJDataTable.DefaultView.RowFilter = objDataTable.DefaultView.RowFilter
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            '加入到TreeView
            Dim objNode As Microsoft.Web.UI.WebControls.TreeNode
            Dim intCount As Integer
            Dim i As Integer
            Dim strDM As String
            Dim strMC As String
            intCount = objDataTable.DefaultView.Count
            For i = 0 To intCount - 1 Step 1
                Try
                    '获取信息
                    With objDataTable.DefaultView
                        strDM = objPulicParameters.getObjectValue(.Item(i).Item(strCodeField), "")
                        strMC = objPulicParameters.getObjectValue(.Item(i).Item(strNameField), "")
                    End With

                    '定义节点
                    objNode = Nothing
                    objNode = New Microsoft.Web.UI.WebControls.TreeNode
                    objNode.CheckBox = blnChecked
                    objNode.Text = strMC
                    objNode.Expanded = True

                    '添加节点
                    objParent.Nodes.Add(objNode)

                    '记录NodeIndex,strDM
                    objNode.ID = Me.getNodeId("A", objNode.GetNodeIndex(), strDM)

                    '设置是否存在下级节点
                    With objXJDataTable.DefaultView
                        .RowFilter = strCodeField + " like '" + strDM + Xydc.Platform.Common.Utilities.PulicParameters.CharFjdmSeparate + "%' and " + strDmjbField + " = " + (intLevel + 1).ToString()
                        If .Count > 0 Then
                            '显示下级节点
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
        ' 将给定的数据显示到treeview顶级
        '     strErrMsg      ：如果出错则返回错误信息
        '     objTreeView    ：树控件
        '     objDataTable   ：要显示的数据
        '     strCodeField   ：代码字段
        '     strNameField   ：名称字段
        '     blnChecked     ：是否显示CheckBox
        '     blnClear       ：强制重建节点
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

            '检查
            If objTreeView Is Nothing Then
                strErrMsg = "错误：没有指定TreeView控件！"
                GoTo errProc
            End If
            If objDataTable Is Nothing Then
                strErrMsg = "错误：没有数据！"
                GoTo errProc
            End If
            If strCodeField Is Nothing Then strCodeField = ""
            If strNameField Is Nothing Then strNameField = ""
            strCodeField = strCodeField.Trim()
            strNameField = strNameField.Trim()
            If strCodeField = "" Or strNameField = "" Then
                strErrMsg = "错误：接口参数没有输入！"
                GoTo errProc
            End If

            '清除
            Dim strNodeIndex As String
            strNodeIndex = objTreeView.SelectedNodeIndex
            If strNodeIndex.Length > 0 Then strNodeIndex = strNodeIndex.Trim()
            If blnClear = True Then
                objTreeView.Nodes.Clear()
            Else
                '不用重建
                If objTreeView.Nodes.Count > 0 Then
                    GoTo normExit
                End If
            End If

            '加入到TreeView
            Dim objNode As Microsoft.Web.UI.WebControls.TreeNode
            Dim intCount As Integer
            Dim i As Integer
            Dim strDM As String
            Dim strMC As String
            intCount = objDataTable.DefaultView.Count
            For i = 0 To intCount - 1 Step 1
                Try
                    '获取信息
                    With objDataTable.DefaultView
                        strDM = objPulicParameters.getObjectValue(.Item(i).Item(strCodeField), "")
                        strMC = objPulicParameters.getObjectValue(.Item(i).Item(strNameField), "")
                    End With

                    '定义节点
                    objNode = Nothing
                    objNode = New Microsoft.Web.UI.WebControls.TreeNode
                    objNode.CheckBox = blnChecked
                    objNode.Text = strMC
                    objNode.Expandable = Microsoft.Web.UI.WebControls.ExpandableValue.CheckOnce

                    '添加节点
                    objTreeView.Nodes.Add(objNode)

                    '记录NodeIndex,strDM
                    objNode.ID = Me.getNodeId("A", objNode.GetNodeIndex(), strDM)

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
            Next

            '设置缺省节点
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
        ' 将给定的数据显示到指定节点objTreeNode的下级
        '     strErrMsg      ：如果出错则返回错误信息
        '     objTreeNode    ：指定节点
        '     objDataTable   ：要显示的数据
        '     strCodeField   ：代码字段
        '     strNameField   ：名称字段
        '     blnChecked     ：是否显示CheckBox
        '     blnClear       ：强制重建节点
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

            '检查
            If objDataTable Is Nothing Then
                strErrMsg = "错误：没有数据！"
                GoTo errProc
            End If
            If objTreeNode Is Nothing Then
                strErrMsg = "错误：没有指定父节点！"
                GoTo errProc
            End If
            If strCodeField Is Nothing Then strCodeField = ""
            If strNameField Is Nothing Then strNameField = ""
            strCodeField = strCodeField.Trim()
            strNameField = strNameField.Trim()
            If strCodeField = "" Or strNameField = "" Then
                strErrMsg = "错误：接口参数没有输入！"
                GoTo errProc
            End If

            '清除
            If blnClear = True Then
                objTreeNode.Nodes.Clear()
            Else
                '不用重建
                If objTreeNode.Nodes.Count > 0 Then
                    GoTo normExit
                End If
            End If

            '加入到TreeView
            Dim objNode As Microsoft.Web.UI.WebControls.TreeNode
            Dim intCount As Integer
            Dim i As Integer
            Dim strDM As String
            Dim strMC As String
            intCount = objDataTable.DefaultView.Count
            For i = 0 To intCount - 1 Step 1
                Try
                    '获取信息
                    With objDataTable.DefaultView
                        strDM = objPulicParameters.getObjectValue(.Item(i).Item(strCodeField), "")
                        strMC = objPulicParameters.getObjectValue(.Item(i).Item(strNameField), "")
                    End With

                    '定义节点
                    objNode = Nothing
                    objNode = New Microsoft.Web.UI.WebControls.TreeNode
                    objNode.CheckBox = blnChecked
                    objNode.Text = strMC
                    objNode.Expandable = Microsoft.Web.UI.WebControls.ExpandableValue.CheckOnce

                    '添加节点
                    objTreeNode.Nodes.Add(objNode)

                    '记录NodeIndex,strDM
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
        ' 展开到objTreeNode
        '     strErrMsg      ：返回错误信息
        '     objTreeView    ：Microsoft.Web.UI.WebControls.TreeView
        '     objTreeNode    ：Microsoft.Web.UI.WebControls.TreeNode
        ' 返回
        '     True           ：成功
        '     False          ：失败
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
        ' 将给定的数据完整显示到objNode的下级中
        '     strErrMsg         ：如果出错则显示错误信息
        '     objParent         ：父节点
        '     objDataTable      ：要显示的数据
        '     strCodeField      ：代码字段名
        '     strNameField      ：节点要显示出的内容
        '     blnChecked        ：是否显示CheckBox
        '     objExpandableValue：展开模式
        '     blnExpanded       ：是否展开
        '     strPrefix         ：前缀字符
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

            '检查
            If objDataTable Is Nothing Then
                strErrMsg = "错误：没有数据！"
                GoTo errProc
            End If

            '克隆
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

            '加入到TreeView
            Dim objNode As Microsoft.Web.UI.WebControls.TreeNode
            Dim intCount As Integer
            Dim i As Integer
            Dim strDM As String
            Dim strMC As String
            intCount = objDataTable.DefaultView.Count
            For i = 0 To intCount - 1 Step 1
                Try
                    '获取信息
                    With objDataTable.DefaultView
                        strDM = objPulicParameters.getObjectValue(.Item(i).Item(strCodeField), "")
                        strMC = objPulicParameters.getObjectValue(.Item(i).Item(strNameField), "")
                    End With

                    '定义节点
                    objNode = Nothing
                    objNode = New Microsoft.Web.UI.WebControls.TreeNode
                    objNode.CheckBox = blnChecked
                    objNode.Text = strMC
                    objNode.Expanded = blnExpanded
                    objNode.Expandable = objExpandableValue

                    '添加节点
                    objParent.Nodes.Add(objNode)

                    '记录NodeIndex,strDM
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
