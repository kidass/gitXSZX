'----------------------------------------------------------------
' Copyright (C) 2006-2016 Josco Software Corporation
' All rights reserved.
'
' This source code is intended only as a supplement to Microsoft
' Development Tools and/or on-line documentation. See these other
' materials for detailed information regarding Microsoft code samples.
'
' THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY 
' OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT 
' LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR 
' FITNESS FOR A PARTICULAR PURPOSE.
'----------------------------------------------------------------
Option Strict On
Option Explicit On 

Imports Microsoft.VisualBasic

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient

Imports Xydc.Platform.Common
Imports Xydc.Platform.Common.Data
Imports Xydc.Platform.SystemFramework

Namespace Xydc.Platform.DataAccess

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.DataAccess
    ' 类名    ：dacMyTask
    '
    ' 功能描述：
    '     提供对“我的事宜”模块涉及的数据层操作
    '----------------------------------------------------------------

    Public Class dacMyTask
        Implements IDisposable

        Private m_objSqlDataAdapter As System.Data.SqlClient.SqlDataAdapter








        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
            m_objSqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter
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
            If Not m_objSqlDataAdapter Is Nothing Then
                m_objSqlDataAdapter.Dispose()
                m_objSqlDataAdapter = Nothing
            End If
        End Sub

        '----------------------------------------------------------------
        ' 安全释放本身资源
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.DataAccess.dacMyTask)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub










        '----------------------------------------------------------------
        ' SqlDataAdapter属性
        '----------------------------------------------------------------
        Protected ReadOnly Property SqlDataAdapter() As System.Data.SqlClient.SqlDataAdapter
            Get
                SqlDataAdapter = m_objSqlDataAdapter
            End Get
        End Property









        '----------------------------------------------------------------
        ' 获取“个人_B_我的事宜_节点”的工作流节点的数据集
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserId              ：用户标识
        '     strPassword            ：用户密码
        '     objParent              ：上级节点数据
        '     intParentLevel         ：上级节点级别(1,...)
        '     objgrswMyTaskData      ：信息数据集
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Private Function getMyTaskNodeDataFlow( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objParent As System.Data.DataRow, _
            ByVal intParentLevel As Integer, _
            ByRef objgrswMyTaskData As Xydc.Platform.Common.Data.grswMyTaskData) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objFlowTypeName As System.Collections.Specialized.NameValueCollection
            Dim objFlowTypeBLLX As System.Collections.Specialized.NameValueCollection

            getMyTaskNodeDataFlow = False

            Try
                Dim objDataRow As System.Data.DataRow
                Dim strPrevCode As String
                Dim strKSSJ As String
                Dim strJSSJ As String
                Dim strCode As String

                '获取上级信息
                strPrevCode = objPulicParameters.getObjectValue(objParent.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_CODE), "")
                strKSSJ = objPulicParameters.getObjectValue(objParent.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ), "")
                strJSSJ = objPulicParameters.getObjectValue(objParent.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ), "")

                '获取工作流类型集合(0,...)
                objFlowTypeName = Xydc.Platform.DataAccess.FlowObject.FlowTypeNameCollection
                objFlowTypeBLLX = Xydc.Platform.DataAccess.FlowObject.FlowTypeBLLXCollection

                '逐个加入
                Dim intCount As Integer
                Dim i As Integer
                With objgrswMyTaskData.Tables(Xydc.Platform.Common.Data.grswMyTaskData.TABLE_GR_B_MYTASK_NODE)
                    intCount = objFlowTypeName.Count
                    For i = 1 To intCount Step 1
                        strCode = Right("00" + i.ToString(), Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(intParentLevel) - Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(intParentLevel - 1))
                        strCode = strPrevCode + strCode

                        objDataRow = .NewRow()

                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_CODE) = strCode
                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = objFlowTypeName(i - 1)
                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_WJLX) = objFlowTypeName(i - 1)
                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_BLLX) = objFlowTypeBLLX(i - 1)
                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = strKSSJ
                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = strJSSJ

                        .Rows.Add(objDataRow)
                    Next
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFlowTypeName)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFlowTypeBLLX)

            getMyTaskNodeDataFlow = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFlowTypeName)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFlowTypeBLLX)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取“个人_B_我的事宜_节点”的数据集
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserId              ：用户标识
        '     strPassword            ：用户密码
        '     objgrswMyTaskData      ：信息数据集
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function getMyTaskNodeData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef objgrswMyTaskData As Xydc.Platform.Common.Data.grswMyTaskData) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objTempgrswMyTaskData As Xydc.Platform.Common.Data.grswMyTaskData

            '初始化
            getMyTaskNodeData = False
            objgrswMyTaskData = Nothing
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()

                '检查
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If

                '获取数据
                Try
                    '创建数据集
                    objTempgrswMyTaskData = New Xydc.Platform.Common.Data.grswMyTaskData(Xydc.Platform.Common.Data.grswMyTaskData.enumTableType.GR_B_MYTASK_NODE)

                    '增加数据
                    Dim objDataRow As System.Data.DataRow
                    With objTempgrswMyTaskData.Tables(Xydc.Platform.Common.Data.grswMyTaskData.TABLE_GR_B_MYTASK_NODE)
                        Dim objMonthStart As System.DateTime
                        Dim objMonthEnd As System.DateTime
                        Dim objWeekStart As System.DateTime
                        Dim objWeekEnd As System.DateTime
                        Dim strCode As String
                        Dim i As Integer
                        Dim j As Integer
                        For i = 1 To 11 Step 1
                            objDataRow = .NewRow()
                            strCode = Right("00" + i.ToString(), Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(0))
                            objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_CODE) = strCode
                            objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = ""
                            objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = ""
                            objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_WJLX) = ""
                            objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_BLLX) = ""

                            Select Case i
                                Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.DBSY
                                    '顶层
                                    objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "我的未办事宜"
                                    .Rows.Add(objDataRow)

                                    If objPulicParameters.getMonthStartAndEndDay(strErrMsg, Now, objMonthStart, objMonthEnd) = False Then
                                        GoTo errProc
                                    End If
                                    If objPulicParameters.getWeekStartAndEndDay(strErrMsg, Now, objWeekStart, objWeekEnd) = False Then
                                        GoTo errProc
                                    End If

                                    For j = 1 To 4 Step 1
                                        '二级
                                        objDataRow = .NewRow()
                                        strCode = Right("00" + i.ToString(), Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(0)) + Right("00" + j.ToString(), Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(1) - Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(0))
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_CODE) = strCode
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_WJLX) = ""
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_BLLX) = ""
                                        Select Case j
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.JINTIAN
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "今天收到的"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Now.ToString("yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Now.ToString("yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENZHOU
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "本周收到的"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Format(objWeekStart, "yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Format(objWeekEnd, "yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENYUEN
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "本月收到的"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Format(objMonthStart, "yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Format(objMonthEnd, "yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENYUES
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "本月以前收到的"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = ""
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Format(objMonthStart, "yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                        End Select

                                        '三级
                                        If getMyTaskNodeDataFlow(strErrMsg, strUserId, strPassword, objDataRow, 2, objTempgrswMyTaskData) = False Then
                                            GoTo errProc
                                        End If
                                    Next

                                Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.DPWJ
                                    objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "我的待批文件"
                                    .Rows.Add(objDataRow)

                                    If objPulicParameters.getMonthStartAndEndDay(strErrMsg, Now, objMonthStart, objMonthEnd) = False Then
                                        GoTo errProc
                                    End If
                                    If objPulicParameters.getWeekStartAndEndDay(strErrMsg, Now, objWeekStart, objWeekEnd) = False Then
                                        GoTo errProc
                                    End If

                                    For j = 1 To 4 Step 1
                                        '二级
                                        objDataRow = .NewRow()
                                        strCode = Right("00" + i.ToString(), Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(0)) + Right("00" + j.ToString(), Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(1) - Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(0))
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_CODE) = strCode
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_WJLX) = ""
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_BLLX) = ""
                                        Select Case j
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.JINTIAN
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "今天送出的"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Now.ToString("yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Now.ToString("yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENZHOU
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "本周送出的"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Format(objWeekStart, "yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Format(objWeekEnd, "yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENYUEN
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "本月送出的"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Format(objMonthStart, "yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Format(objMonthEnd, "yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENYUES
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "本月以前送出的"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = ""
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Format(objMonthStart, "yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                        End Select

                                        '三级
                                        If getMyTaskNodeDataFlow(strErrMsg, strUserId, strPassword, objDataRow, 2, objTempgrswMyTaskData) = False Then
                                            GoTo errProc
                                        End If
                                    Next

                                Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.HBWJ
                                    objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "我的缓办文件"
                                    .Rows.Add(objDataRow)

                                    If objPulicParameters.getMonthStartAndEndDay(strErrMsg, Now, objMonthStart, objMonthEnd) = False Then
                                        GoTo errProc
                                    End If
                                    If objPulicParameters.getWeekStartAndEndDay(strErrMsg, Now, objWeekStart, objWeekEnd) = False Then
                                        GoTo errProc
                                    End If

                                    For j = 1 To 4 Step 1
                                        '二级
                                        objDataRow = .NewRow()
                                        strCode = Right("00" + i.ToString(), Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(0)) + Right("00" + j.ToString(), Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(1) - Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(0))
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_CODE) = strCode
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_WJLX) = ""
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_BLLX) = ""
                                        Select Case j
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.JINTIAN
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "今天缓办的"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Now.ToString("yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Now.ToString("yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENZHOU
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "本周缓办的"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Format(objWeekStart, "yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Format(objWeekEnd, "yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENYUEN
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "本月缓办的"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Format(objMonthStart, "yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Format(objMonthEnd, "yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENYUES
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "本月以前缓办的"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = ""
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Format(objMonthStart, "yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                        End Select

                                        '三级
                                        If getMyTaskNodeDataFlow(strErrMsg, strUserId, strPassword, objDataRow, 2, objTempgrswMyTaskData) = False Then
                                            GoTo errProc
                                        End If
                                    Next

                                Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.YBSY
                                    objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "我的已办事宜"
                                    .Rows.Add(objDataRow)

                                    If objPulicParameters.getMonthStartAndEndDay(strErrMsg, Now, objMonthStart, objMonthEnd) = False Then
                                        GoTo errProc
                                    End If
                                    If objPulicParameters.getWeekStartAndEndDay(strErrMsg, Now, objWeekStart, objWeekEnd) = False Then
                                        GoTo errProc
                                    End If

                                    For j = 1 To 4 Step 1
                                        '二级
                                        objDataRow = .NewRow()
                                        strCode = Right("00" + i.ToString(), Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(0)) + Right("00" + j.ToString(), Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(1) - Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(0))
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_CODE) = strCode
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_WJLX) = ""
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_BLLX) = ""
                                        Select Case j
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.JINTIAN
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "今天办完的"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Now.ToString("yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Now.ToString("yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENZHOU
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "本周办完的"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Format(objWeekStart, "yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Format(objWeekEnd, "yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENYUEN
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "本月办完的"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Format(objMonthStart, "yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Format(objMonthEnd, "yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENYUES
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "本月以前办完的"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = ""
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Format(objMonthStart, "yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                        End Select

                                        '三级
                                        If getMyTaskNodeDataFlow(strErrMsg, strUserId, strPassword, objDataRow, 2, objTempgrswMyTaskData) = False Then
                                            GoTo errProc
                                        End If
                                    Next

                                Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.GQSY
                                    objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "我的过期事宜"
                                    .Rows.Add(objDataRow)

                                    If objPulicParameters.getMonthStartAndEndDay(strErrMsg, Now, objMonthStart) = False Then
                                        GoTo errProc
                                    End If
                                    If objPulicParameters.getWeekStartAndEndDay(strErrMsg, Now, objWeekStart) = False Then
                                        GoTo errProc
                                    End If

                                    For j = 1 To 4 Step 1
                                        '
                                        'KSSJ<JSSJ：(now - 办理期限) <= (JSSJ-KSSJ) and now >= 办理期限
                                        'KSSJ>JSSJ：(now - 办理期限) >  (KSSJ-JSSJ) and now >= 办理期限
                                        '
                                        '二级
                                        objDataRow = .NewRow()
                                        strCode = Right("00" + i.ToString(), Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(0)) + Right("00" + j.ToString(), Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(1) - Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(0))
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_CODE) = strCode
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_WJLX) = ""
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_BLLX) = ""
                                        Select Case j
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.JINTIAN
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "今天刚过期的"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Format(Now.AddDays(-1), "yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Now.ToString("yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENZHOU
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "过期不到一周的"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Format(Now.AddDays(-7), "yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Now.ToString("yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENYUEN
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "过期不到一月的"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Format(Now.AddDays(-30), "yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Now.ToString("yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENYUES
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "过期一月以上的"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Now.ToString("yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Format(Now.AddDays(-30), "yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                        End Select

                                        '三级
                                        If getMyTaskNodeDataFlow(strErrMsg, strUserId, strPassword, objDataRow, 2, objTempgrswMyTaskData) = False Then
                                            GoTo errProc
                                        End If
                                    Next

                                Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.CBSY
                                    objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "我催办的事宜"
                                    .Rows.Add(objDataRow)

                                    If objPulicParameters.getMonthStartAndEndDay(strErrMsg, Now, objMonthStart) = False Then
                                        GoTo errProc
                                    End If
                                    If objPulicParameters.getWeekStartAndEndDay(strErrMsg, Now, objWeekStart) = False Then
                                        GoTo errProc
                                    End If

                                    For j = 1 To 4 Step 1
                                        '
                                        'KSSJ<JSSJ：(now - 催办日期) <= (JSSJ-KSSJ) and now >= 催办日期
                                        'KSSJ>JSSJ：(now - 催办日期) >  (KSSJ-JSSJ) and now >= 催办日期
                                        '
                                        '二级
                                        objDataRow = .NewRow()
                                        strCode = Right("00" + i.ToString(), Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(0)) + Right("00" + j.ToString(), Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(1) - Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(0))
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_CODE) = strCode
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_WJLX) = ""
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_BLLX) = ""
                                        Select Case j
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.JINTIAN
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "今天催办的"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Format(Now.AddDays(-1), "yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Now.ToString("yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENZHOU
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "催办不到一周的"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Format(Now.AddDays(-7), "yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Now.ToString("yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENYUEN
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "催办不到一月的"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Format(Now.AddDays(-30), "yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Now.ToString("yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENYUES
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "催办一月以上的"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Now.ToString("yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Format(Now.AddDays(-30), "yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                        End Select

                                        '三级
                                        If getMyTaskNodeDataFlow(strErrMsg, strUserId, strPassword, objDataRow, 2, objTempgrswMyTaskData) = False Then
                                            GoTo errProc
                                        End If
                                    Next

                                Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.BCSY
                                    objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "我被催的事宜"
                                    .Rows.Add(objDataRow)

                                    If objPulicParameters.getMonthStartAndEndDay(strErrMsg, Now, objMonthStart) = False Then
                                        GoTo errProc
                                    End If
                                    If objPulicParameters.getWeekStartAndEndDay(strErrMsg, Now, objWeekStart) = False Then
                                        GoTo errProc
                                    End If

                                    For j = 1 To 4 Step 1
                                        '
                                        'KSSJ<JSSJ：(now - 催办日期) <= (JSSJ-KSSJ) and now >= 催办日期
                                        'KSSJ>JSSJ：(now - 催办日期) >  (KSSJ-JSSJ) and now >= 催办日期
                                        '
                                        '二级
                                        objDataRow = .NewRow()
                                        strCode = Right("00" + i.ToString(), Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(0)) + Right("00" + j.ToString(), Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(1) - Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(0))
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_CODE) = strCode
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_WJLX) = ""
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_BLLX) = ""
                                        Select Case j
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.JINTIAN
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "今天被催办的"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Format(Now.AddDays(-1), "yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Now.ToString("yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENZHOU
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "被催办不到一周的"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Format(Now.AddDays(-7), "yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Now.ToString("yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENYUEN
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "被催办不到一月的"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Format(Now.AddDays(-30), "yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Now.ToString("yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENYUES
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "被催办一月以上的"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Now.ToString("yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Format(Now.AddDays(-30), "yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                        End Select

                                        '三级
                                        If getMyTaskNodeDataFlow(strErrMsg, strUserId, strPassword, objDataRow, 2, objTempgrswMyTaskData) = False Then
                                            GoTo errProc
                                        End If
                                    Next

                                Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.DBWJ
                                    objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "我督办的事宜"
                                    .Rows.Add(objDataRow)

                                    If objPulicParameters.getMonthStartAndEndDay(strErrMsg, Now, objMonthStart) = False Then
                                        GoTo errProc
                                    End If
                                    If objPulicParameters.getWeekStartAndEndDay(strErrMsg, Now, objWeekStart) = False Then
                                        GoTo errProc
                                    End If

                                    For j = 1 To 4 Step 1
                                        '
                                        'KSSJ<JSSJ：(now - 督办日期) <= (JSSJ-KSSJ) and now >= 督办日期
                                        'KSSJ>JSSJ：(now - 督办日期) >  (KSSJ-JSSJ) and now >= 督办日期
                                        '
                                        '二级
                                        objDataRow = .NewRow()
                                        strCode = Right("00" + i.ToString(), Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(0)) + Right("00" + j.ToString(), Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(1) - Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(0))
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_CODE) = strCode
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_WJLX) = ""
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_BLLX) = ""
                                        Select Case j
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.JINTIAN
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "今天督办的"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Format(Now.AddDays(-1), "yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Now.ToString("yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENZHOU
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "督办不到一周的"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Format(Now.AddDays(-7), "yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Now.ToString("yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENYUEN
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "督办不到一月的"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Format(Now.AddDays(-30), "yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Now.ToString("yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENYUES
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "督办一月以上的"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Now.ToString("yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Format(Now.AddDays(-30), "yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                        End Select

                                        '三级
                                        If getMyTaskNodeDataFlow(strErrMsg, strUserId, strPassword, objDataRow, 2, objTempgrswMyTaskData) = False Then
                                            GoTo errProc
                                        End If
                                    Next

                                Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.BDWJ
                                    objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "我被督的事宜"
                                    .Rows.Add(objDataRow)

                                    If objPulicParameters.getMonthStartAndEndDay(strErrMsg, Now, objMonthStart) = False Then
                                        GoTo errProc
                                    End If
                                    If objPulicParameters.getWeekStartAndEndDay(strErrMsg, Now, objWeekStart) = False Then
                                        GoTo errProc
                                    End If

                                    For j = 1 To 4 Step 1
                                        '
                                        'KSSJ<JSSJ：(now - 督办日期) <= (JSSJ-KSSJ) and now >= 督办日期
                                        'KSSJ>JSSJ：(now - 督办日期) >  (KSSJ-JSSJ) and now >= 督办日期
                                        '
                                        '二级
                                        objDataRow = .NewRow()
                                        strCode = Right("00" + i.ToString(), Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(0)) + Right("00" + j.ToString(), Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(1) - Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(0))
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_CODE) = strCode
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_WJLX) = ""
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_BLLX) = ""
                                        Select Case j
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.JINTIAN
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "今天被督办的"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Format(Now.AddDays(-1), "yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Now.ToString("yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENZHOU
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "被督办不到一周的"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Format(Now.AddDays(-7), "yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Now.ToString("yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENYUEN
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "被督办不到一月的"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Format(Now.AddDays(-30), "yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Now.ToString("yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENYUES
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "被督办一月以上的"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Now.ToString("yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Format(Now.AddDays(-30), "yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                        End Select

                                        '三级
                                        If getMyTaskNodeDataFlow(strErrMsg, strUserId, strPassword, objDataRow, 2, objTempgrswMyTaskData) = False Then
                                            GoTo errProc
                                        End If
                                    Next

                                Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.QBSY
                                    objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "我的全部事宜"
                                    .Rows.Add(objDataRow)

                                    '二级
                                    If getMyTaskNodeDataFlow(strErrMsg, strUserId, strPassword, objDataRow, 1, objTempgrswMyTaskData) = False Then
                                        GoTo errProc
                                    End If

                                Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.BWTX
                                    objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "我的备忘提醒"
                                    .Rows.Add(objDataRow)

                                    '二级
                                    If getMyTaskNodeDataFlow(strErrMsg, strUserId, strPassword, objDataRow, 1, objTempgrswMyTaskData) = False Then
                                        GoTo errProc
                                    End If
                            End Select
                        Next
                    End With

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempgrswMyTaskData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.grswMyTaskData.SafeRelease(objTempgrswMyTaskData)
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            '返回
            objgrswMyTaskData = objTempgrswMyTaskData
            getMyTaskNodeData = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Data.grswMyTaskData.SafeRelease(objTempgrswMyTaskData)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据给定代码获取对应的数据行数据
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strCode                ：给定节点代码(唯一性保证)
        '     objgrswMyTaskData      ：节点信息数据集
        '     objNodeData            ：(返回)指定节点的数据行数据
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function getMyTaskNodeData( _
            ByRef strErrMsg As String, _
            ByVal strCode As String, _
            ByVal objgrswMyTaskData As Xydc.Platform.Common.Data.grswMyTaskData, _
            ByRef objNodeData As System.Data.DataRow) As Boolean

            getMyTaskNodeData = False
            objNodeData = Nothing

            Try
                With objgrswMyTaskData.Tables(Xydc.Platform.Common.Data.grswMyTaskData.TABLE_GR_B_MYTASK_NODE)
                    .DefaultView.RowFilter = Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_CODE + " = '" + strCode + "'"
                    If .DefaultView.Count > 0 Then
                        objNodeData = .DefaultView.Item(0).Row
                    End If
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getMyTaskNodeData = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据文件类型、开始日期、结束日期计算未办事宜的文件搜索SQL
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserXM              ：当前操作人员名称
        '     strBLLX                ：办理类型
        '     strWJLX                ：文件类型-工作流类型
        '     strQSRQ                ：开始日期
        '     strZZRQ                ：结束日期
        '     strWhere               ：搜索条件
        '     strSQL                 ：(返回)文件搜索SQL
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Private Function getSQLDBSY_FILE( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByVal strBLLX As String, _
            ByVal strWJLX As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String, _
            ByVal strWhere As String, _
            ByRef strSQL As String) As Boolean

            getSQLDBSY_FILE = False
            strSQL = ""

            Try
                Dim strFileAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.FileStatusAllYWCList
                Dim strTaskAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TaskStatusAllYWCList
                Dim strLF As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhLf
                Dim strRT As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhRt

                '初始化日期
                If strQSRQ Is Nothing Then strQSRQ = ""
                strQSRQ = strQSRQ.Trim
                If strQSRQ <> "" Then
                    If strQSRQ.IndexOf(" ") < 0 Then
                        strQSRQ = strQSRQ + " 00:00:00"
                    End If
                End If
                If strZZRQ Is Nothing Then strZZRQ = ""
                strZZRQ = strZZRQ.Trim
                If strZZRQ <> "" Then
                    If strZZRQ.IndexOf(" ") < 0 Then
                        strZZRQ = strZZRQ + " 23:59:59"
                    End If
                End If

                '我的文件
                strSQL = ""
                strSQL = strSQL + " select a.*" + vbCr
                strSQL = strSQL + " from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select" + vbCr
                strSQL = strSQL + "     a.文件标识, a.流水号  ," + vbCr
                strSQL = strSQL + "     a.办理类型, a.办理状态, a.文件子类, a.文件类型," + vbCr
                strSQL = strSQL + "     a.文件标题, a.主送单位, a.文件字号, a.紧急程度, a.秘密等级," + vbCr
                strSQL = strSQL + "     a.机关代字, a.文件年份, a.文件序号," + vbCr
                strSQL = strSQL + "     a.主题词  , a.主办单位, a.拟稿人  , a.拟稿日期,a.快速收文," + vbCr
                strSQL = strSQL + "     发送日期 = max(a.发送日期)," + vbCr
                strSQL = strSQL + "     办理期限 = max(a.办理期限)," + vbCr
                strSQL = strSQL + "     完成日期 = max(a.完成日期)," + vbCr
                strSQL = strSQL + "     a.备忘提醒" + vbCr
                strSQL = strSQL + "   from" + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select" + vbCr
                strSQL = strSQL + "       a.文件标识, b.流水号  ," + vbCr
                strSQL = strSQL + "       a.办理类型, b.办理状态, b.文件子类, b.文件类型," + vbCr
                strSQL = strSQL + "       b.文件标题, b.主送单位, b.文件字号, b.紧急程度, b.秘密等级," + vbCr
                strSQL = strSQL + "       b.机关代字, b.文件年份, b.文件序号," + vbCr
                strSQL = strSQL + "       b.主题词  , b.主办单位, b.拟稿人  , b.拟稿日期," + vbCr
                strSQL = strSQL + "       a.发送日期, a.办理期限, a.完成日期, b.快速收文," + vbCr
                strSQL = strSQL + "       备忘提醒 = case when c.备忘提醒 is null then '×' else c.备忘提醒 end" + vbCr
                strSQL = strSQL + "     from" + vbCr

                '获取主表记录
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select a.文件标识,a.办理类型,a.交接标识," + vbCr
                strSQL = strSQL + "         发送日期 = max(a.发送日期)," + vbCr
                strSQL = strSQL + "         办理期限 = max(a.办理最后期限)," + vbCr
                strSQL = strSQL + "         完成日期 = max(a.完成日期)" + vbCr
                strSQL = strSQL + "       from" + vbCr
                strSQL = strSQL + "       (" + vbCr
                strSQL = strSQL + "         select" + vbCr
                strSQL = strSQL + "           文件标识, 办理类型,发送日期,办理最后期限,完成日期," + vbCr
                strSQL = strSQL + "           交接标识 = case when 交接标识 like '_____1%' then '1' else '0' end" + vbCr
                strSQL = strSQL + "         from 公文_B_交接" + vbCr
                strSQL = strSQL + "         where 接收人   =    '" + Trim(strUserXM) + "'" + vbCr              '我要做
                strSQL = strSQL + "         and   交接标识 like '__1%'" + vbCr                                 '我可见
                strSQL = strSQL + "         and   办理状态 not in (" + strTaskAllYWCList + ")" + vbCr          '没有办完
                strSQL = strSQL + "         and   发送日期 is not null" + vbCr
                If strBLLX <> "" Then
                    strSQL = strSQL + "         and 办理类型 = '" + strBLLX + "'" + vbCr                       '指定类型
                End If
                If strQSRQ <> "" And strZZRQ <> "" Then                                                        '指定日期
                    strSQL = strSQL + "         and 发送日期 between '" + strQSRQ + "' and '" + strZZRQ + "' " + vbCr
                ElseIf strQSRQ <> "" Then
                    strSQL = strSQL + "         and 发送日期 >= '" + strQSRQ + "'" + vbCr
                ElseIf strZZRQ <> "" Then
                    strSQL = strSQL + "         and 发送日期 <= '" + strZZRQ + "'" + vbCr
                Else
                End If
                strSQL = strSQL + "       ) a" + vbCr
                strSQL = strSQL + "       group by a.文件标识,a.办理类型,a.交接标识" + vbCr
                strSQL = strSQL + "     ) a " + vbCr
                '获取主表记录

                '获取备忘提醒
                strSQL = strSQL + "     left join" + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select 文件标识,备忘提醒 = case when max(备忘提醒) = 1 then '√' else '×' end" + vbCr
                strSQL = strSQL + "       from 公文_B_交接" + vbCr
                strSQL = strSQL + "       where 接收人 = '" + strUserXM + "'" + vbCr
                strSQL = strSQL + "       group by 文件标识" + vbCr
                strSQL = strSQL + "     ) c on a.文件标识 = c.文件标识" + vbCr
                '获取备忘提醒


                '获取文件信息
                strSQL = strSQL + "     left join " + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select " + vbCr
                strSQL = strSQL + "         a.文件标识, a.流水号  , " + vbCr
                strSQL = strSQL + "         a.办理类型, a.办理状态, a.文件子类, a.文件类型," + vbCr
                strSQL = strSQL + "         a.文件标题, a.主送单位, " + vbCr
                strSQL = strSQL + "         a.文件字号, a.紧急程度, a.秘密等级," + vbCr
                strSQL = strSQL + "         a.机关代字, a.文件年份, a.文件序号," + vbCr
                strSQL = strSQL + "         a.主题词  , a.主办单位, a.拟稿人  , a.拟稿日期," + vbCr
                strSQL = strSQL + "         a.快速收文" + vbCr
                strSQL = strSQL + "       from 公文_V_全部审批文件新 a" + vbCr
                strSQL = strSQL + "       left join" + vbCr
                strSQL = strSQL + "       (" + vbCr
                strSQL = strSQL + "           select 文件标识" + vbCr
                strSQL = strSQL + "           from 公文_B_交接" + vbCr
                strSQL = strSQL + "           where 接收人   =    '" + Trim(strUserXM) + "'" + vbCr              '我要做
                strSQL = strSQL + "           and   交接标识 like '__1%'" + vbCr                                 '我可见
                strSQL = strSQL + "           and   办理状态 not in (" + strTaskAllYWCList + ")" + vbCr          '没有办完
                strSQL = strSQL + "           and   发送日期 is not null" + vbCr
                If strBLLX <> "" Then
                    strSQL = strSQL + "           and 办理类型 = '" + strBLLX + "'" + vbCr                       '指定类型
                End If
                If strQSRQ <> "" And strZZRQ <> "" Then                                                          '指定日期
                    strSQL = strSQL + "           and 发送日期 between '" + strQSRQ + "' and '" + strZZRQ + "' " + vbCr
                ElseIf strQSRQ <> "" Then
                    strSQL = strSQL + "           and 发送日期 >= '" + strQSRQ + "'" + vbCr
                ElseIf strZZRQ <> "" Then
                    strSQL = strSQL + "           and 发送日期 <= '" + strZZRQ + "'" + vbCr
                Else
                End If
                strSQL = strSQL + "           group by 文件标识" + vbCr
                strSQL = strSQL + "       ) b on a.文件标识 = b.文件标识" + vbCr
                strSQL = strSQL + "       where b.文件标识 is not null" + vbCr
                If strWJLX <> "" Then
                    strSQL = strSQL + "       and a.文件类型 = '" + strWJLX + "'" + vbCr
                End If
                strSQL = strSQL + "     ) b on a.文件标识 = b.文件标识 " + vbCr
                '获取文件信息


                strSQL = strSQL + "     where b.文件标识 Is Not Null " + vbCr
                strSQL = strSQL + "     and (" + vbCr
                strSQL = strSQL + "       (a.交接标识 = '1')" + vbCr                                                            '通知类消息
                strSQL = strSQL + "       or " + vbCr
                strSQL = strSQL + "       (b.快速收文 =   1)" + vbCr                                                            '快速收文
                strSQL = strSQL + "       or " + vbCr
                strSQL = strSQL + "       (b.办理状态 not in (" + strFileAllYWCList + ")) " + vbCr                              '文件未办完
                strSQL = strSQL + "     ) " + vbCr
                strSQL = strSQL + "   ) a " + vbCr
                strSQL = strSQL + "   group by " + vbCr
                strSQL = strSQL + "     a.文件标识, a.流水号  ," + vbCr
                strSQL = strSQL + "     a.办理类型, a.办理状态, a.文件子类, a.文件类型," + vbCr
                strSQL = strSQL + "     a.文件标题, a.主送单位, a.文件字号, a.紧急程度, a.秘密等级," + vbCr
                strSQL = strSQL + "     a.机关代字, a.文件年份, a.文件序号," + vbCr
                strSQL = strSQL + "     a.主题词  , a.主办单位, a.拟稿人  , a.拟稿日期, a.快速收文, a.备忘提醒" + vbCr
                strSQL = strSQL + " ) a" + vbCr
                If strWhere <> "" Then
                    strSQL = strSQL + " where " + strWhere + vbCr
                End If
                strSQL = strSQL + " order by a.发送日期 desc, a.文件年份 desc, a.机关代字, a.文件序号 desc" + vbCr
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getSQLDBSY_FILE = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据文件类型、开始日期、结束日期计算未办事宜的任务搜索SQL
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strWJBS                ：要查看的文件标识
        '     strUserXM              ：当前操作人员名称
        '     strBLLX                ：办理类型
        '     strWJLX                ：文件类型-工作流类型
        '     strQSRQ                ：开始日期
        '     strZZRQ                ：结束日期
        '     strWhere               ：搜索条件
        '     strSQL                 ：(返回)任务搜索SQL
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Private Function getSQLDBSY_TASK( _
            ByRef strErrMsg As String, _
            ByVal strWJBS As String, _
            ByVal strUserXM As String, _
            ByVal strBLLX As String, _
            ByVal strWJLX As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String, _
            ByVal strWhere As String, _
            ByRef strSQL As String) As Boolean

            getSQLDBSY_TASK = False
            strSQL = ""

            Try
                Dim strFileAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.FileStatusAllYWCList
                Dim strTaskAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TaskStatusAllYWCList
                Dim strLF As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhLf
                Dim strRT As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhRt
                Dim strGWTHCL As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_THCL
                Dim strGWSHCL As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_SHCL
                Dim strGWHFCL As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_HFCL

                '初始化日期
                If strQSRQ Is Nothing Then strQSRQ = ""
                strQSRQ = strQSRQ.Trim
                If strQSRQ <> "" Then
                    If strQSRQ.IndexOf(" ") < 0 Then
                        strQSRQ = strQSRQ + " 00:00:00"
                    End If
                End If
                If strZZRQ Is Nothing Then strZZRQ = ""
                strZZRQ = strZZRQ.Trim
                If strZZRQ <> "" Then
                    If strZZRQ.IndexOf(" ") < 0 Then
                        strZZRQ = strZZRQ + " 23:59:59"
                    End If
                End If

                '我的文件
                strSQL = ""
                strSQL = strSQL + " select * from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select" + vbCr
                strSQL = strSQL + "     a.文件标识, b.流水号  ," + vbCr
                strSQL = strSQL + "     a.办理类型, a.办理状态, a.办理子类, b.文件子类, b.文件类型," + vbCr
                strSQL = strSQL + "     b.文件标题, b.机关代字, b.文件年份, b.文件序号, b.主办单位," + vbCr
                strSQL = strSQL + "     a.发送人  , a.接收人  , a.委托人  , a.交接说明" + vbCr
                strSQL = strSQL + "   from" + vbCr

                '获取主表记录
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select" + vbCr
                strSQL = strSQL + "       文件标识, 办理类型, 办理状态," + vbCr
                strSQL = strSQL + "       办理子类 = case " + vbCr
                strSQL = strSQL + "         when 交接标识 like '___1%'    then '" + strGWTHCL + "'" + vbCr
                strSQL = strSQL + "         when 交接标识 like '____1%'   then '" + strGWSHCL + "'" + vbCr
                strSQL = strSQL + "         when 交接标识 like '______1%' then '" + strGWHFCL + "'" + vbCr
                strSQL = strSQL + "         else 办理子类 end," + vbCr
                strSQL = strSQL + "       发送人, 接收人, 委托人, 交接标识, 交接说明 " + vbCr
                strSQL = strSQL + "     from 公文_B_交接" + vbCr
                strSQL = strSQL + "     where 文件标识 = '" + strWJBS + "'" + vbCr                         '指定文件
                strSQL = strSQL + "     and   接收人   = '" + Trim(strUserXM) + "'" + vbCr                 '我要做
                strSQL = strSQL + "     and   交接标识 like '__1%'" + vbCr                                 '我可见
                strSQL = strSQL + "     and   办理状态 not in (" + strTaskAllYWCList + ")" + vbCr          '没有办完
                strSQL = strSQL + "     and   发送日期 is not null" + vbCr
                If strBLLX <> "" Then
                    strSQL = strSQL + "     and 办理类型 = '" + strBLLX + "'" + vbCr                       '指定类型
                End If
                If strQSRQ <> "" And strZZRQ <> "" Then                                                    '指定日期
                    strSQL = strSQL + "     and 发送日期 between '" + strQSRQ + "' and '" + strZZRQ + "' " + vbCr
                ElseIf strQSRQ <> "" Then
                    strSQL = strSQL + "     and 发送日期 >= '" + strQSRQ + "'" + vbCr
                ElseIf strZZRQ <> "" Then
                    strSQL = strSQL + "     and 发送日期 <= '" + strZZRQ + "'" + vbCr
                Else
                End If
                strSQL = strSQL + "   ) a " + vbCr
                '获取主表记录


                '获取文件信息
                strSQL = strSQL + "   left join " + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select " + vbCr
                strSQL = strSQL + "       文件标识, 流水号  , " + vbCr
                strSQL = strSQL + "       办理类型, 办理状态, 文件子类, 文件类型," + vbCr
                strSQL = strSQL + "       文件标题, 主送单位, " + vbCr
                strSQL = strSQL + "       文件字号, 紧急程度, 秘密等级," + vbCr
                strSQL = strSQL + "       机关代字, 文件年份, 文件序号," + vbCr
                strSQL = strSQL + "       主题词  , 主办单位, 拟稿人  , 拟稿日期," + vbCr
                strSQL = strSQL + "       快速收文" + vbCr
                strSQL = strSQL + "     from 公文_V_全部审批文件新 " + vbCr
                strSQL = strSQL + "     where 文件标识 = '" + strWJBS + "'" + vbCr                                            '指定文件
                If strWJLX <> "" Then
                    strSQL = strSQL + "     and   文件类型 = '" + strWJLX + "'" + vbCr                                        '工作流类型=文件具体类型
                End If
                strSQL = strSQL + "   ) b on a.文件标识 = b.文件标识 " + vbCr
                '获取文件信息


                strSQL = strSQL + "   where b.文件标识 Is Not Null " + vbCr
                strSQL = strSQL + "   and (" + vbCr
                strSQL = strSQL + "     (a.交接标识 like '_____1%')" + vbCr                                                   '通知类消息
                strSQL = strSQL + "     or " + vbCr
                strSQL = strSQL + "     (b.快速收文 =   1)" + vbCr                                                            '快速收文
                strSQL = strSQL + "     or " + vbCr
                strSQL = strSQL + "     (b.办理状态 not in (" + strFileAllYWCList + ")) " + vbCr                              '文件未办完
                strSQL = strSQL + "   ) " + vbCr
                strSQL = strSQL + " ) a " + vbCr
                If strWhere <> "" Then
                    strSQL = strSQL + " where " + strWhere + vbCr
                End If
                strSQL = strSQL + " group by " + vbCr
                strSQL = strSQL + "   a.文件标识, a.流水号  ," + vbCr
                strSQL = strSQL + "   a.办理类型, a.办理状态, a.办理子类, a.文件子类, a.文件类型," + vbCr
                strSQL = strSQL + "   a.文件标题, a.机关代字, a.文件年份, a.文件序号, a.主办单位," + vbCr
                strSQL = strSQL + "   a.发送人  , a.接收人  , a.委托人  , a.交接说明" + vbCr
                strSQL = strSQL + " order by a.文件年份 desc, a.机关代字, a.文件序号 desc" + vbCr
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getSQLDBSY_TASK = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据文件类型、开始日期、结束日期计算待批文件的文件搜索SQL
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserXM              ：当前操作人员名称
        '     strBLLX                ：办理类型
        '     strWJLX                ：文件类型-工作流类型
        '     strQSRQ                ：开始日期
        '     strZZRQ                ：结束日期
        '     strWhere               ：搜索条件
        '     strSQL                 ：(返回)文件搜索SQL
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Private Function getSQLDPWJ_FILE( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByVal strBLLX As String, _
            ByVal strWJLX As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String, _
            ByVal strWhere As String, _
            ByRef strSQL As String) As Boolean

            getSQLDPWJ_FILE = False
            strSQL = ""

            Try
                Dim strFileAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.FileStatusAllYWCList
                Dim strTaskAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TaskStatusAllYWCList
                Dim strLF As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhLf
                Dim strRT As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhRt

                '初始化日期
                If strQSRQ Is Nothing Then strQSRQ = ""
                strQSRQ = strQSRQ.Trim
                If strQSRQ <> "" Then
                    If strQSRQ.IndexOf(" ") < 0 Then
                        strQSRQ = strQSRQ + " 00:00:00"
                    End If
                End If
                If strZZRQ Is Nothing Then strZZRQ = ""
                strZZRQ = strZZRQ.Trim
                If strZZRQ <> "" Then
                    If strZZRQ.IndexOf(" ") < 0 Then
                        strZZRQ = strZZRQ + " 23:59:59"
                    End If
                End If

                '我的文件
                strSQL = ""
                strSQL = strSQL + " select a.*" + vbCr
                strSQL = strSQL + " from" + vbCr
                strSQL = strSQL + " ("
                strSQL = strSQL + "   select" + vbCr
                strSQL = strSQL + "     a.文件标识, a.流水号  ," + vbCr
                strSQL = strSQL + "     a.办理类型, a.办理状态, a.文件子类, a.文件类型," + vbCr
                strSQL = strSQL + "     a.文件标题, a.主送单位, a.文件字号, a.紧急程度, a.秘密等级," + vbCr
                strSQL = strSQL + "     a.机关代字, a.文件年份, a.文件序号," + vbCr
                strSQL = strSQL + "     a.主题词  , a.主办单位, a.拟稿人  , a.拟稿日期,a.快速收文," + vbCr
                strSQL = strSQL + "     发送日期 = max(a.发送日期)," + vbCr
                strSQL = strSQL + "     办理期限 = max(a.办理期限)," + vbCr
                strSQL = strSQL + "     完成日期 = max(a.完成日期)," + vbCr
                strSQL = strSQL + "     a.备忘提醒" + vbCr
                strSQL = strSQL + "   from" + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select" + vbCr
                strSQL = strSQL + "       a.文件标识, b.流水号  ," + vbCr
                strSQL = strSQL + "       a.办理类型, b.办理状态, b.文件子类, b.文件类型," + vbCr
                strSQL = strSQL + "       b.文件标题, b.主送单位, b.文件字号, b.紧急程度, b.秘密等级," + vbCr
                strSQL = strSQL + "       b.机关代字, b.文件年份, b.文件序号," + vbCr
                strSQL = strSQL + "       b.主题词  , b.主办单位, b.拟稿人  , b.拟稿日期," + vbCr
                strSQL = strSQL + "       a.发送日期, a.办理期限, a.完成日期, b.快速收文," + vbCr
                strSQL = strSQL + "       备忘提醒 = case when c.备忘提醒 is null then '×' else c.备忘提醒 end" + vbCr
                strSQL = strSQL + "     from" + vbCr

                '获取主表记录
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select" + vbCr
                strSQL = strSQL + "         文件标识, 办理类型," + vbCr
                strSQL = strSQL + "         发送日期 = max(发送日期)," + vbCr
                strSQL = strSQL + "         办理期限 = max(办理最后期限)," + vbCr
                strSQL = strSQL + "         完成日期 = max(完成日期)" + vbCr
                strSQL = strSQL + "       from 公文_B_交接" + vbCr
                strSQL = strSQL + "       where 发送人   =    '" + Trim(strUserXM) + "'" + vbCr              '我送走
                strSQL = strSQL + "       and   交接标识 like '11_0000%'" + vbCr                             '审批事宜
                strSQL = strSQL + "       and   办理状态 not in (" + strTaskAllYWCList + ")" + vbCr          '没有办完
                strSQL = strSQL + "       and   发送日期 is not null" + vbCr
                If strBLLX <> "" Then
                    strSQL = strSQL + "       and 办理类型 = '" + strBLLX + "'" + vbCr                       '指定类型
                End If
                If strQSRQ <> "" And strZZRQ <> "" Then                                                      '指定日期
                    strSQL = strSQL + "       and 发送日期 between '" + strQSRQ + "' and '" + strZZRQ + "' " + vbCr
                ElseIf strQSRQ <> "" Then
                    strSQL = strSQL + "       and 发送日期 >= '" + strQSRQ + "'" + vbCr
                ElseIf strZZRQ <> "" Then
                    strSQL = strSQL + "       and 发送日期 <= '" + strZZRQ + "'" + vbCr
                Else
                End If
                strSQL = strSQL + "       group by 文件标识,办理类型" + vbCr
                strSQL = strSQL + "     ) a " + vbCr
                '获取主表记录

                '获取备忘提醒
                strSQL = strSQL + "     left join" + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select 文件标识,备忘提醒 = case when max(备忘提醒) = 1 then '√' else '×' end" + vbCr
                strSQL = strSQL + "       from 公文_B_交接" + vbCr
                strSQL = strSQL + "       where 接收人 = '" + strUserXM + "'" + vbCr
                strSQL = strSQL + "       group by 文件标识" + vbCr
                strSQL = strSQL + "     ) c on a.文件标识 = c.文件标识" + vbCr
                '获取备忘提醒


                '获取文件信息
                strSQL = strSQL + "     left join " + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select " + vbCr
                strSQL = strSQL + "         a.文件标识, a.流水号  , " + vbCr
                strSQL = strSQL + "         a.办理类型, a.办理状态, a.文件子类, a.文件类型," + vbCr
                strSQL = strSQL + "         a.文件标题, a.主送单位, " + vbCr
                strSQL = strSQL + "         a.文件字号, a.紧急程度, a.秘密等级," + vbCr
                strSQL = strSQL + "         a.机关代字, a.文件年份, a.文件序号," + vbCr
                strSQL = strSQL + "         a.主题词  , a.主办单位, a.拟稿人  , a.拟稿日期," + vbCr
                strSQL = strSQL + "         a.快速收文" + vbCr
                strSQL = strSQL + "       from 公文_V_全部审批文件新 a" + vbCr
                strSQL = strSQL + "       left join" + vbCr
                strSQL = strSQL + "       (" + vbCr
                strSQL = strSQL + "         select 文件标识" + vbCr
                strSQL = strSQL + "         from 公文_B_交接" + vbCr
                strSQL = strSQL + "         where 发送人   =    '" + Trim(strUserXM) + "'" + vbCr              '我送走
                strSQL = strSQL + "         and   交接标识 like '11_0000%'" + vbCr                             '审批事宜
                strSQL = strSQL + "         and   办理状态 not in (" + strTaskAllYWCList + ")" + vbCr          '没有办完
                strSQL = strSQL + "         and   发送日期 is not null" + vbCr
                If strBLLX <> "" Then
                    strSQL = strSQL + "         and 办理类型 = '" + strBLLX + "'" + vbCr                       '指定类型
                End If
                If strQSRQ <> "" And strZZRQ <> "" Then                                                        '指定日期
                    strSQL = strSQL + "         and 发送日期 between '" + strQSRQ + "' and '" + strZZRQ + "' " + vbCr
                ElseIf strQSRQ <> "" Then
                    strSQL = strSQL + "         and 发送日期 >= '" + strQSRQ + "'" + vbCr
                ElseIf strZZRQ <> "" Then
                    strSQL = strSQL + "         and 发送日期 <= '" + strZZRQ + "'" + vbCr
                Else
                End If
                strSQL = strSQL + "         group by 文件标识" + vbCr
                strSQL = strSQL + "       ) b on a.文件标识 = b.文件标识" + vbCr
                strSQL = strSQL + "       where b.文件标识 is not null" + vbCr
                If strWJLX <> "" Then
                    strSQL = strSQL + "       and a.文件类型 = '" + strWJLX + "'" + vbCr
                End If
                strSQL = strSQL + "     ) b on a.文件标识 = b.文件标识 " + vbCr
                '获取文件信息


                strSQL = strSQL + "     where b.文件标识 Is Not Null " + vbCr
                strSQL = strSQL + "     and (" + vbCr
                strSQL = strSQL + "       (b.快速收文 =   1)" + vbCr                                                            '快速收文
                strSQL = strSQL + "       or " + vbCr
                strSQL = strSQL + "       (b.办理状态 not in (" + strFileAllYWCList + ")) " + vbCr                              '文件未办完
                strSQL = strSQL + "     ) " + vbCr
                strSQL = strSQL + "   ) a " + vbCr
                strSQL = strSQL + "   group by " + vbCr
                strSQL = strSQL + "     a.文件标识, a.流水号  ," + vbCr
                strSQL = strSQL + "     a.办理类型, a.办理状态, a.文件子类, a.文件类型," + vbCr
                strSQL = strSQL + "     a.文件标题, a.主送单位, a.文件字号, a.紧急程度, a.秘密等级," + vbCr
                strSQL = strSQL + "     a.机关代字, a.文件年份, a.文件序号," + vbCr
                strSQL = strSQL + "     a.主题词  , a.主办单位, a.拟稿人  , a.拟稿日期, a.快速收文, a.备忘提醒" + vbCr
                strSQL = strSQL + " ) a" + vbCr
                If strWhere <> "" Then
                    strSQL = strSQL + " where " + strWhere + vbCr
                End If
                strSQL = strSQL + " order by a.发送日期 desc, a.文件年份 desc, a.机关代字, a.文件序号 desc" + vbCr
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getSQLDPWJ_FILE = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据文件类型、开始日期、结束日期计算待批文件的任务搜索SQL
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strWJBS                ：要查看的文件标识
        '     strUserXM              ：当前操作人员名称
        '     strBLLX                ：办理类型
        '     strWJLX                ：文件类型-工作流类型
        '     strQSRQ                ：开始日期
        '     strZZRQ                ：结束日期
        '     strWhere               ：搜索条件
        '     strSQL                 ：(返回)任务搜索SQL
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Private Function getSQLDPWJ_TASK( _
            ByRef strErrMsg As String, _
            ByVal strWJBS As String, _
            ByVal strUserXM As String, _
            ByVal strBLLX As String, _
            ByVal strWJLX As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String, _
            ByVal strWhere As String, _
            ByRef strSQL As String) As Boolean

            getSQLDPWJ_TASK = False
            strSQL = ""

            Try
                Dim strFileAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.FileStatusAllYWCList
                Dim strTaskAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TaskStatusAllYWCList
                Dim strLF As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhLf
                Dim strRT As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhRt
                Dim strGWTHCL As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_THCL
                Dim strGWSHCL As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_SHCL
                Dim strGWHFCL As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_HFCL

                '初始化日期
                If strQSRQ Is Nothing Then strQSRQ = ""
                strQSRQ = strQSRQ.Trim
                If strQSRQ <> "" Then
                    If strQSRQ.IndexOf(" ") < 0 Then
                        strQSRQ = strQSRQ + " 00:00:00"
                    End If
                End If
                If strZZRQ Is Nothing Then strZZRQ = ""
                strZZRQ = strZZRQ.Trim
                If strZZRQ <> "" Then
                    If strZZRQ.IndexOf(" ") < 0 Then
                        strZZRQ = strZZRQ + " 23:59:59"
                    End If
                End If

                '我的文件
                strSQL = ""
                strSQL = strSQL + " select * from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select" + vbCr
                strSQL = strSQL + "     a.文件标识, b.流水号  ," + vbCr
                strSQL = strSQL + "     a.办理类型, a.办理状态, a.办理子类, b.文件子类, b.文件类型," + vbCr
                strSQL = strSQL + "     b.文件标题, b.机关代字, b.文件年份, b.文件序号, b.主办单位," + vbCr
                strSQL = strSQL + "     a.发送人  , a.接收人  , a.委托人  , a.交接说明" + vbCr
                strSQL = strSQL + "   from" + vbCr

                '获取主表记录
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select" + vbCr
                strSQL = strSQL + "      文件标识, 办理类型, 办理状态," + vbCr
                strSQL = strSQL + "      办理子类 = case " + vbCr
                strSQL = strSQL + "        when 交接标识 like '___1%'    then '" + strGWTHCL + "' " + vbCr
                strSQL = strSQL + "        when 交接标识 like '____1%'   then '" + strGWSHCL + "' " + vbCr
                strSQL = strSQL + "        when 交接标识 like '______1%' then '" + strGWHFCL + "' " + vbCr
                strSQL = strSQL + "        else 办理子类 end," + vbCr
                strSQL = strSQL + "      发送人, 接收人, 委托人, 交接标识, 交接说明 " + vbCr
                strSQL = strSQL + "     from 公文_B_交接" + vbCr
                strSQL = strSQL + "     where 文件标识 = '" + strWJBS + "'" + vbCr                         '指定文件
                strSQL = strSQL + "     and   发送人   = '" + Trim(strUserXM) + "'" + vbCr                 '我送走
                strSQL = strSQL + "     and   交接标识 like '11_0000%'" + vbCr                             '审批事宜
                strSQL = strSQL + "     and   办理状态 not in (" + strTaskAllYWCList + ")" + vbCr          '没有办完
                strSQL = strSQL + "     and   发送日期 is not null" + vbCr
                If strBLLX <> "" Then
                    strSQL = strSQL + "     and 办理类型 = '" + strBLLX + "'" + vbCr                       '指定类型
                End If
                If strQSRQ <> "" And strZZRQ <> "" Then                                                    '指定日期
                    strSQL = strSQL + "     and 发送日期 between '" + strQSRQ + "' and '" + strZZRQ + "' " + vbCr
                ElseIf strQSRQ <> "" Then
                    strSQL = strSQL + "     and 发送日期 >= '" + strQSRQ + "'" + vbCr
                ElseIf strZZRQ <> "" Then
                    strSQL = strSQL + "     and 发送日期 <= '" + strZZRQ + "'" + vbCr
                Else
                End If
                strSQL = strSQL + "   ) a " + vbCr
                '获取主表记录


                '获取文件信息
                strSQL = strSQL + "   left join " + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select " + vbCr
                strSQL = strSQL + "       文件标识, 流水号  , " + vbCr
                strSQL = strSQL + "       办理类型, 办理状态, 文件子类, 文件类型," + vbCr
                strSQL = strSQL + "       文件标题, 主送单位, " + vbCr
                strSQL = strSQL + "       文件字号, 紧急程度, 秘密等级," + vbCr
                strSQL = strSQL + "       机关代字, 文件年份, 文件序号," + vbCr
                strSQL = strSQL + "       主题词  , 主办单位, 拟稿人  , 拟稿日期," + vbCr
                strSQL = strSQL + "       快速收文" + vbCr
                strSQL = strSQL + "     from 公文_V_全部审批文件新 " + vbCr
                strSQL = strSQL + "     where 文件标识 = '" + strWJBS + "'" + vbCr                                            '指定文件
                If strWJLX <> "" Then
                    strSQL = strSQL + "     and   文件类型 = '" + strWJLX + "'" + vbCr                                        '工作流类型=文件具体类型
                End If
                strSQL = strSQL + "   ) b on a.文件标识 = b.文件标识 " + vbCr
                '获取文件信息


                strSQL = strSQL + "   where b.文件标识 Is Not Null " + vbCr
                strSQL = strSQL + "   and (" + vbCr
                strSQL = strSQL + "     (b.快速收文 =   1)" + vbCr                                                            '快速收文
                strSQL = strSQL + "     or " + vbCr
                strSQL = strSQL + "     (b.办理状态 not in (" + strFileAllYWCList + ")) " + vbCr                              '文件未办完
                strSQL = strSQL + "   ) " + vbCr
                strSQL = strSQL + " ) a " + vbCr
                If strWhere <> "" Then
                    strSQL = strSQL + " where " + strWhere + vbCr
                End If
                strSQL = strSQL + " group by " + vbCr
                strSQL = strSQL + "   a.文件标识, a.流水号  ," + vbCr
                strSQL = strSQL + "   a.办理类型, a.办理状态, a.办理子类, a.文件子类, a.文件类型," + vbCr
                strSQL = strSQL + "   a.文件标题, a.机关代字, a.文件年份, a.文件序号, a.主办单位," + vbCr
                strSQL = strSQL + "   a.发送人  , a.接收人  , a.委托人  , a.交接说明" + vbCr
                strSQL = strSQL + " order by a.文件年份 desc, a.机关代字, a.文件序号 desc" + vbCr
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getSQLDPWJ_TASK = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据文件类型、开始日期、结束日期计算缓办文件的文件搜索SQL
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserXM              ：当前操作人员名称
        '     strBLLX                ：办理类型
        '     strWJLX                ：文件类型-工作流类型
        '     strQSRQ                ：开始日期
        '     strZZRQ                ：结束日期
        '     strWhere               ：搜索条件
        '     strSQL                 ：(返回)文件搜索SQL
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Private Function getSQLHBWJ_FILE( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByVal strBLLX As String, _
            ByVal strWJLX As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String, _
            ByVal strWhere As String, _
            ByRef strSQL As String) As Boolean

            getSQLHBWJ_FILE = False
            strSQL = ""

            Try
                Dim strFileAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.FileStatusAllYWCList
                Dim strTaskYTBList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TaskStatusYTBList
                Dim strLF As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhLf
                Dim strRT As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhRt

                '初始化日期
                If strQSRQ Is Nothing Then strQSRQ = ""
                strQSRQ = strQSRQ.Trim
                If strQSRQ <> "" Then
                    If strQSRQ.IndexOf(" ") < 0 Then
                        strQSRQ = strQSRQ + " 00:00:00"
                    End If
                End If
                If strZZRQ Is Nothing Then strZZRQ = ""
                strZZRQ = strZZRQ.Trim
                If strZZRQ <> "" Then
                    If strZZRQ.IndexOf(" ") < 0 Then
                        strZZRQ = strZZRQ + " 23:59:59"
                    End If
                End If

                '我的文件
                strSQL = ""
                strSQL = strSQL + " select a.*" + vbCr
                strSQL = strSQL + " from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select" + vbCr
                strSQL = strSQL + "     a.文件标识, a.流水号  ," + vbCr
                strSQL = strSQL + "     a.办理类型, a.办理状态, a.文件子类, a.文件类型," + vbCr
                strSQL = strSQL + "     a.文件标题, a.主送单位, a.文件字号, a.紧急程度, a.秘密等级," + vbCr
                strSQL = strSQL + "     a.机关代字, a.文件年份, a.文件序号," + vbCr
                strSQL = strSQL + "     a.主题词  , a.主办单位, a.拟稿人  , a.拟稿日期,a.快速收文," + vbCr
                strSQL = strSQL + "     发送日期 = max(a.发送日期)," + vbCr
                strSQL = strSQL + "     办理期限 = max(a.办理期限)," + vbCr
                strSQL = strSQL + "     完成日期 = max(a.完成日期)," + vbCr
                strSQL = strSQL + "     a.备忘提醒" + vbCr
                strSQL = strSQL + "   from" + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select" + vbCr
                strSQL = strSQL + "       a.文件标识, b.流水号  ," + vbCr
                strSQL = strSQL + "       a.办理类型, b.办理状态, b.文件子类, b.文件类型," + vbCr
                strSQL = strSQL + "       b.文件标题, b.主送单位, b.文件字号, b.紧急程度, b.秘密等级," + vbCr
                strSQL = strSQL + "       b.机关代字, b.文件年份, b.文件序号," + vbCr
                strSQL = strSQL + "       b.主题词  , b.主办单位, b.拟稿人  , b.拟稿日期," + vbCr
                strSQL = strSQL + "       a.发送日期, a.办理期限, a.完成日期, b.快速收文," + vbCr
                strSQL = strSQL + "       备忘提醒 = case when c.备忘提醒 is null then '×' else c.备忘提醒 end" + vbCr
                strSQL = strSQL + "     from" + vbCr

                '获取主表记录
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select" + vbCr
                strSQL = strSQL + "         文件标识, 办理类型," + vbCr
                strSQL = strSQL + "         发送日期 = max(发送日期)," + vbCr
                strSQL = strSQL + "         办理期限 = max(办理最后期限)," + vbCr
                strSQL = strSQL + "         完成日期 = max(完成日期)" + vbCr
                strSQL = strSQL + "       from 公文_B_交接" + vbCr
                strSQL = strSQL + "       where 接收人   =    '" + Trim(strUserXM) + "'" + vbCr              '我做的
                strSQL = strSQL + "       and   交接标识 like '__1%'" + vbCr                                 '我可见
                strSQL = strSQL + "       and   办理状态 in (" + strTaskYTBList + ")" + vbCr                 '已停办
                strSQL = strSQL + "       and   完成日期 is not null" + vbCr
                If strBLLX <> "" Then
                    strSQL = strSQL + "       and 办理类型 = '" + strBLLX + "'" + vbCr                       '指定类型
                End If
                If strQSRQ <> "" And strZZRQ <> "" Then                                                      '指定日期
                    strSQL = strSQL + "       and 完成日期 between '" + strQSRQ + "' and '" + strZZRQ + "' " + vbCr
                ElseIf strQSRQ <> "" Then
                    strSQL = strSQL + "       and 完成日期 >= '" + strQSRQ + "'" + vbCr
                ElseIf strZZRQ <> "" Then
                    strSQL = strSQL + "       and 完成日期 <= '" + strZZRQ + "'" + vbCr
                Else
                End If
                strSQL = strSQL + "       group by 文件标识,办理类型" + vbCr
                strSQL = strSQL + "     ) a " + vbCr
                '获取主表记录

                '获取备忘提醒
                strSQL = strSQL + "     left join" + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select 文件标识,备忘提醒 = case when max(备忘提醒) = 1 then '√' else '×' end" + vbCr
                strSQL = strSQL + "       from 公文_B_交接" + vbCr
                strSQL = strSQL + "       where 接收人 = '" + strUserXM + "'" + vbCr
                strSQL = strSQL + "       group by 文件标识" + vbCr
                strSQL = strSQL + "     ) c on a.文件标识 = c.文件标识" + vbCr
                '获取备忘提醒


                '获取文件信息
                strSQL = strSQL + "     left join " + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select " + vbCr
                strSQL = strSQL + "         a.文件标识, a.流水号  , " + vbCr
                strSQL = strSQL + "         a.办理类型, a.办理状态, a.文件子类, a.文件类型," + vbCr
                strSQL = strSQL + "         a.文件标题, a.主送单位, " + vbCr
                strSQL = strSQL + "         a.文件字号, a.紧急程度, a.秘密等级," + vbCr
                strSQL = strSQL + "         a.机关代字, a.文件年份, a.文件序号," + vbCr
                strSQL = strSQL + "         a.主题词  , a.主办单位, a.拟稿人  , a.拟稿日期," + vbCr
                strSQL = strSQL + "         a.快速收文" + vbCr
                strSQL = strSQL + "       from 公文_V_全部审批文件新 a" + vbCr
                strSQL = strSQL + "       left join" + vbCr
                strSQL = strSQL + "       (" + vbCr
                strSQL = strSQL + "         select 文件标识" + vbCr
                strSQL = strSQL + "         from 公文_B_交接" + vbCr
                strSQL = strSQL + "         where 接收人   =    '" + Trim(strUserXM) + "'" + vbCr              '我做的
                strSQL = strSQL + "         and   交接标识 like '__1%'" + vbCr                                 '我可见
                strSQL = strSQL + "         and   办理状态 in (" + strTaskYTBList + ")" + vbCr                 '已停办
                strSQL = strSQL + "         and   完成日期 is not null" + vbCr
                If strBLLX <> "" Then
                    strSQL = strSQL + "         and 办理类型 = '" + strBLLX + "'" + vbCr                       '指定类型
                End If
                If strQSRQ <> "" And strZZRQ <> "" Then                                                        '指定日期
                    strSQL = strSQL + "         and 完成日期 between '" + strQSRQ + "' and '" + strZZRQ + "' " + vbCr
                ElseIf strQSRQ <> "" Then
                    strSQL = strSQL + "         and 完成日期 >= '" + strQSRQ + "'" + vbCr
                ElseIf strZZRQ <> "" Then
                    strSQL = strSQL + "         and 完成日期 <= '" + strZZRQ + "'" + vbCr
                Else
                End If
                strSQL = strSQL + "         group by 文件标识" + vbCr
                strSQL = strSQL + "       ) b on a.文件标识 = b.文件标识" + vbCr
                strSQL = strSQL + "       where b.文件标识 is not null" + vbCr
                If strWJLX <> "" Then
                    strSQL = strSQL + "       and a.文件类型 = '" + strWJLX + "'" + vbCr
                End If
                strSQL = strSQL + "     ) b on a.文件标识 = b.文件标识 " + vbCr
                '获取文件信息


                strSQL = strSQL + "     where b.文件标识 Is Not Null " + vbCr
                strSQL = strSQL + "   ) a " + vbCr
                strSQL = strSQL + "   group by " + vbCr
                strSQL = strSQL + "     a.文件标识, a.流水号  ," + vbCr
                strSQL = strSQL + "     a.办理类型, a.办理状态, a.文件子类, a.文件类型," + vbCr
                strSQL = strSQL + "     a.文件标题, a.主送单位, a.文件字号, a.紧急程度, a.秘密等级," + vbCr
                strSQL = strSQL + "     a.机关代字, a.文件年份, a.文件序号," + vbCr
                strSQL = strSQL + "     a.主题词  , a.主办单位, a.拟稿人  , a.拟稿日期, a.快速收文, a.备忘提醒" + vbCr
                strSQL = strSQL + " ) a" + vbCr
                If strWhere <> "" Then
                    strSQL = strSQL + " where " + strWhere + vbCr
                End If
                strSQL = strSQL + " order by a.发送日期 desc, a.文件年份 desc, a.机关代字, a.文件序号 desc" + vbCr
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getSQLHBWJ_FILE = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据文件类型、开始日期、结束日期计算缓办文件的任务搜索SQL
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strWJBS                ：要查看的文件标识
        '     strUserXM              ：当前操作人员名称
        '     strBLLX                ：办理类型
        '     strWJLX                ：文件类型-工作流类型
        '     strQSRQ                ：开始日期
        '     strZZRQ                ：结束日期
        '     strWhere               ：搜索条件
        '     strSQL                 ：(返回)任务搜索SQL
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Private Function getSQLHBWJ_TASK( _
            ByRef strErrMsg As String, _
            ByVal strWJBS As String, _
            ByVal strUserXM As String, _
            ByVal strBLLX As String, _
            ByVal strWJLX As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String, _
            ByVal strWhere As String, _
            ByRef strSQL As String) As Boolean

            getSQLHBWJ_TASK = False
            strSQL = ""

            Try
                Dim strFileAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.FileStatusAllYWCList
                Dim strTaskYTBList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TaskStatusYTBList
                Dim strLF As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhLf
                Dim strRT As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhRt
                Dim strGWTHCL As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_THCL
                Dim strGWSHCL As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_SHCL
                Dim strGWHFCL As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_HFCL

                '初始化日期
                If strQSRQ Is Nothing Then strQSRQ = ""
                strQSRQ = strQSRQ.Trim
                If strQSRQ <> "" Then
                    If strQSRQ.IndexOf(" ") < 0 Then
                        strQSRQ = strQSRQ + " 00:00:00"
                    End If
                End If
                If strZZRQ Is Nothing Then strZZRQ = ""
                strZZRQ = strZZRQ.Trim
                If strZZRQ <> "" Then
                    If strZZRQ.IndexOf(" ") < 0 Then
                        strZZRQ = strZZRQ + " 23:59:59"
                    End If
                End If

                '我的文件
                strSQL = ""
                strSQL = strSQL + " select * from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select" + vbCr
                strSQL = strSQL + "     a.文件标识, b.流水号  ," + vbCr
                strSQL = strSQL + "     a.办理类型, a.办理状态, a.办理子类, b.文件子类, b.文件类型," + vbCr
                strSQL = strSQL + "     b.文件标题, b.机关代字, b.文件年份, b.文件序号, b.主办单位," + vbCr
                strSQL = strSQL + "     a.发送人  , a.接收人  , a.委托人  , a.交接说明" + vbCr
                strSQL = strSQL + "   from" + vbCr

                '获取主表记录
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select" + vbCr
                strSQL = strSQL + "       文件标识, 办理类型, 办理状态," + vbCr
                strSQL = strSQL + "       办理子类 = case " + vbCr
                strSQL = strSQL + "         when 交接标识 like '___1%'    then '" + strGWTHCL + "' " + vbCr
                strSQL = strSQL + "         when 交接标识 like '____1%'   then '" + strGWSHCL + "' " + vbCr
                strSQL = strSQL + "         when 交接标识 like '______1%' then '" + strGWHFCL + "' " + vbCr
                strSQL = strSQL + "         else 办理子类 end," + vbCr
                strSQL = strSQL + "       发送人, 接收人, 委托人, 交接标识, 交接说明 " + vbCr
                strSQL = strSQL + "     from 公文_B_交接" + vbCr
                strSQL = strSQL + "     where 文件标识 = '" + strWJBS + "'" + vbCr                         '指定文件
                strSQL = strSQL + "     and   接收人   = '" + Trim(strUserXM) + "'" + vbCr                 '我做的
                strSQL = strSQL + "     and   交接标识 like '__1%'" + vbCr                                 '我可见
                strSQL = strSQL + "     and   办理状态 in (" + strTaskYTBList + ")" + vbCr                 '已停办
                strSQL = strSQL + "     and   完成日期 is not null" + vbCr
                If strBLLX <> "" Then
                    strSQL = strSQL + "     and 办理类型 = '" + strBLLX + "'" + vbCr                       '指定类型
                End If
                If strQSRQ <> "" And strZZRQ <> "" Then                                                    '指定日期
                    strSQL = strSQL + "     and 完成日期 between '" + strQSRQ + "' and '" + strZZRQ + "' " + vbCr
                ElseIf strQSRQ <> "" Then
                    strSQL = strSQL + "     and 完成日期 >= '" + strQSRQ + "'" + vbCr
                ElseIf strZZRQ <> "" Then
                    strSQL = strSQL + "     and 完成日期 <= '" + strZZRQ + "'" + vbCr
                Else
                End If
                strSQL = strSQL + "   ) a " + vbCr
                '获取主表记录


                '获取文件信息
                strSQL = strSQL + "   left join " + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select " + vbCr
                strSQL = strSQL + "       文件标识, 流水号  , " + vbCr
                strSQL = strSQL + "       办理类型, 办理状态, 文件子类, 文件类型," + vbCr
                strSQL = strSQL + "       文件标题, 主送单位, " + vbCr
                strSQL = strSQL + "       文件字号, 紧急程度, 秘密等级," + vbCr
                strSQL = strSQL + "       机关代字, 文件年份, 文件序号," + vbCr
                strSQL = strSQL + "       主题词  , 主办单位, 拟稿人  , 拟稿日期," + vbCr
                strSQL = strSQL + "       快速收文" + vbCr
                strSQL = strSQL + "     from 公文_V_全部审批文件新 " + vbCr
                strSQL = strSQL + "     where 文件标识 = '" + strWJBS + "'" + vbCr                                            '指定文件
                If strWJLX <> "" Then
                    strSQL = strSQL + "     and   文件类型 = '" + strWJLX + "'" + vbCr                                        '工作流类型=文件具体类型
                End If
                strSQL = strSQL + "   ) b on a.文件标识 = b.文件标识 " + vbCr
                '获取文件信息


                strSQL = strSQL + "   where b.文件标识 Is Not Null " + vbCr
                strSQL = strSQL + " ) a " + vbCr
                If strWhere <> "" Then
                    strSQL = strSQL + " where " + strWhere + vbCr
                End If
                strSQL = strSQL + " group by " + vbCr
                strSQL = strSQL + "   a.文件标识, a.流水号  ," + vbCr
                strSQL = strSQL + "   a.办理类型, a.办理状态, a.办理子类, a.文件子类, a.文件类型," + vbCr
                strSQL = strSQL + "   a.文件标题, a.机关代字, a.文件年份, a.文件序号, a.主办单位," + vbCr
                strSQL = strSQL + "   a.发送人  , a.接收人  , a.委托人  , a.交接说明" + vbCr
                strSQL = strSQL + " order by a.文件年份 desc, a.机关代字, a.文件序号 desc" + vbCr
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getSQLHBWJ_TASK = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据文件类型、开始日期、结束日期计算已办文件的文件搜索SQL
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserXM              ：当前操作人员名称
        '     strBLLX                ：办理类型
        '     strWJLX                ：文件类型-工作流类型
        '     strQSRQ                ：开始日期
        '     strZZRQ                ：结束日期
        '     strWhere               ：搜索条件
        '     strSQL                 ：(返回)文件搜索SQL
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Private Function getSQLYBSY_FILE( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByVal strBLLX As String, _
            ByVal strWJLX As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String, _
            ByVal strWhere As String, _
            ByRef strSQL As String) As Boolean

            getSQLYBSY_FILE = False
            strSQL = ""

            Try
                Dim strFileAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.FileStatusAllYWCList
                Dim strTaskAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TaskStatusAllYWCList
                Dim strLF As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhLf
                Dim strRT As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhRt

                '初始化日期
                If strQSRQ Is Nothing Then strQSRQ = ""
                strQSRQ = strQSRQ.Trim
                If strQSRQ <> "" Then
                    If strQSRQ.IndexOf(" ") < 0 Then
                        strQSRQ = strQSRQ + " 00:00:00"
                    End If
                End If
                If strZZRQ Is Nothing Then strZZRQ = ""
                strZZRQ = strZZRQ.Trim
                If strZZRQ <> "" Then
                    If strZZRQ.IndexOf(" ") < 0 Then
                        strZZRQ = strZZRQ + " 23:59:59"
                    End If
                End If

                '我的文件
                strSQL = ""
                strSQL = strSQL + " select a.*" + vbCr
                strSQL = strSQL + " from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select" + vbCr
                strSQL = strSQL + "     a.文件标识, a.流水号  ," + vbCr
                strSQL = strSQL + "     a.办理类型, a.办理状态, a.文件子类, a.文件类型," + vbCr
                strSQL = strSQL + "     a.文件标题, a.主送单位, a.文件字号, a.紧急程度, a.秘密等级," + vbCr
                strSQL = strSQL + "     a.机关代字, a.文件年份, a.文件序号," + vbCr
                strSQL = strSQL + "     a.主题词  , a.主办单位, a.拟稿人  , a.拟稿日期,a.快速收文," + vbCr
                strSQL = strSQL + "     发送日期 = max(a.发送日期)," + vbCr
                strSQL = strSQL + "     办理期限 = max(a.办理期限)," + vbCr
                strSQL = strSQL + "     完成日期 = max(a.完成日期)," + vbCr
                strSQL = strSQL + "     a.备忘提醒" + vbCr
                strSQL = strSQL + "   from" + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select" + vbCr
                strSQL = strSQL + "       a.文件标识, b.流水号  ," + vbCr
                strSQL = strSQL + "       a.办理类型, b.办理状态, b.文件子类, b.文件类型," + vbCr
                strSQL = strSQL + "       b.文件标题, b.主送单位, b.文件字号, b.紧急程度, b.秘密等级," + vbCr
                strSQL = strSQL + "       b.机关代字, b.文件年份, b.文件序号," + vbCr
                strSQL = strSQL + "       b.主题词  , b.主办单位, b.拟稿人  , b.拟稿日期," + vbCr
                strSQL = strSQL + "       a.发送日期, a.办理期限, a.完成日期, b.快速收文," + vbCr
                strSQL = strSQL + "       备忘提醒 = case when c.备忘提醒 is null then '×' else c.备忘提醒 end" + vbCr
                strSQL = strSQL + "     from" + vbCr

                '获取主表记录
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select" + vbCr
                strSQL = strSQL + "         文件标识, 办理类型," + vbCr
                strSQL = strSQL + "         发送日期 = max(发送日期)," + vbCr
                strSQL = strSQL + "         办理期限 = max(办理最后期限)," + vbCr
                strSQL = strSQL + "         完成日期 = max(完成日期)" + vbCr
                strSQL = strSQL + "       from 公文_B_交接" + vbCr
                strSQL = strSQL + "       where 接收人   =    '" + Trim(strUserXM) + "'" + vbCr              '我做的
                strSQL = strSQL + "       and   交接标识 like '__1%'" + vbCr                                 '我可见
                strSQL = strSQL + "       and   办理状态 in (" + strTaskAllYWCList + ")" + vbCr              '已办完
                If strBLLX <> "" Then
                    strSQL = strSQL + "       and 办理类型 = '" + strBLLX + "'" + vbCr                       '指定类型
                End If
                If strQSRQ <> "" And strZZRQ <> "" Then                                                      '指定日期
                    strSQL = strSQL + "       and 完成日期 between '" + strQSRQ + "' and '" + strZZRQ + "' " + vbCr
                ElseIf strQSRQ <> "" Then
                    strSQL = strSQL + "       and 完成日期 >= '" + strQSRQ + "'" + vbCr
                ElseIf strZZRQ <> "" Then
                    strSQL = strSQL + "       and 完成日期 <= '" + strZZRQ + "'" + vbCr
                Else
                End If
                strSQL = strSQL + "       group by 文件标识, 办理类型" + vbCr
                strSQL = strSQL + "     ) a " + vbCr
                '获取主表记录

                '获取备忘提醒
                strSQL = strSQL + "     left join" + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select 文件标识,备忘提醒 = case when max(备忘提醒) = 1 then '√' else '×' end" + vbCr
                strSQL = strSQL + "       from 公文_B_交接" + vbCr
                strSQL = strSQL + "       where 接收人 = '" + strUserXM + "'" + vbCr
                strSQL = strSQL + "       group by 文件标识" + vbCr
                strSQL = strSQL + "     ) c on a.文件标识 = c.文件标识" + vbCr
                '获取备忘提醒


                '获取文件信息
                strSQL = strSQL + "     left join " + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select " + vbCr
                strSQL = strSQL + "         a.文件标识, a.流水号  , " + vbCr
                strSQL = strSQL + "         a.办理类型, a.办理状态, a.文件子类, a.文件类型," + vbCr
                strSQL = strSQL + "         a.文件标题, a.主送单位, " + vbCr
                strSQL = strSQL + "         a.文件字号, a.紧急程度, a.秘密等级," + vbCr
                strSQL = strSQL + "         a.机关代字, a.文件年份, a.文件序号," + vbCr
                strSQL = strSQL + "         a.主题词  , a.主办单位, a.拟稿人  , a.拟稿日期," + vbCr
                strSQL = strSQL + "         a.快速收文" + vbCr
                strSQL = strSQL + "       from 公文_V_全部审批文件新 a" + vbCr
                strSQL = strSQL + "       left join" + vbCr
                strSQL = strSQL + "       (" + vbCr
                strSQL = strSQL + "         select 文件标识" + vbCr
                strSQL = strSQL + "         from 公文_B_交接" + vbCr
                strSQL = strSQL + "         where 接收人   =    '" + Trim(strUserXM) + "'" + vbCr              '我做的
                strSQL = strSQL + "         and   交接标识 like '__1%'" + vbCr                                 '我可见
                strSQL = strSQL + "         and   办理状态 in (" + strTaskAllYWCList + ")" + vbCr              '已办完
                If strBLLX <> "" Then
                    strSQL = strSQL + "         and 办理类型 = '" + strBLLX + "'" + vbCr                       '指定类型
                End If
                If strQSRQ <> "" And strZZRQ <> "" Then                                                        '指定日期
                    strSQL = strSQL + "         and 完成日期 between '" + strQSRQ + "' and '" + strZZRQ + "' " + vbCr
                ElseIf strQSRQ <> "" Then
                    strSQL = strSQL + "         and 完成日期 >= '" + strQSRQ + "'" + vbCr
                ElseIf strZZRQ <> "" Then
                    strSQL = strSQL + "         and 完成日期 <= '" + strZZRQ + "'" + vbCr
                Else
                End If
                strSQL = strSQL + "         group by 文件标识" + vbCr
                strSQL = strSQL + "       ) b on a.文件标识 = b.文件标识" + vbCr
                strSQL = strSQL + "       where b.文件标识 is not null" + vbCr
                If strWJLX <> "" Then
                    strSQL = strSQL + "       and a.文件类型 = '" + strWJLX + "'" + vbCr
                End If
                strSQL = strSQL + "     ) b on a.文件标识 = b.文件标识 " + vbCr
                '获取文件信息


                strSQL = strSQL + "     where b.文件标识 Is Not Null " + vbCr
                strSQL = strSQL + "   ) a " + vbCr
                strSQL = strSQL + "   group by " + vbCr
                strSQL = strSQL + "     a.文件标识, a.流水号  ," + vbCr
                strSQL = strSQL + "     a.办理类型, a.办理状态, a.文件子类, a.文件类型," + vbCr
                strSQL = strSQL + "     a.文件标题, a.主送单位, a.文件字号, a.紧急程度, a.秘密等级," + vbCr
                strSQL = strSQL + "     a.机关代字, a.文件年份, a.文件序号," + vbCr
                strSQL = strSQL + "     a.主题词  , a.主办单位, a.拟稿人  , a.拟稿日期, a.快速收文, a.备忘提醒" + vbCr
                strSQL = strSQL + " ) a" + vbCr
                If strWhere <> "" Then
                    strSQL = strSQL + " where " + strWhere + vbCr
                End If
                strSQL = strSQL + " order by a.发送日期 desc, a.文件年份 desc, a.机关代字, a.文件序号 desc" + vbCr
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getSQLYBSY_FILE = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据文件类型、开始日期、结束日期计算已办事宜的任务搜索SQL
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strWJBS                ：要查看的文件标识
        '     strUserXM              ：当前操作人员名称
        '     strBLLX                ：办理类型
        '     strWJLX                ：文件类型-工作流类型
        '     strQSRQ                ：开始日期
        '     strZZRQ                ：结束日期
        '     strWhere               ：搜索条件
        '     strSQL                 ：(返回)任务搜索SQL
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Private Function getSQLYBSY_TASK( _
            ByRef strErrMsg As String, _
            ByVal strWJBS As String, _
            ByVal strUserXM As String, _
            ByVal strBLLX As String, _
            ByVal strWJLX As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String, _
            ByVal strWhere As String, _
            ByRef strSQL As String) As Boolean

            getSQLYBSY_TASK = False
            strSQL = ""

            Try
                Dim strFileAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.FileStatusAllYWCList
                Dim strTaskAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TaskStatusAllYWCList
                Dim strLF As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhLf
                Dim strRT As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhRt
                Dim strGWTHCL As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_THCL
                Dim strGWSHCL As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_SHCL
                Dim strGWHFCL As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_HFCL

                '初始化日期
                If strQSRQ Is Nothing Then strQSRQ = ""
                strQSRQ = strQSRQ.Trim
                If strQSRQ <> "" Then
                    If strQSRQ.IndexOf(" ") < 0 Then
                        strQSRQ = strQSRQ + " 00:00:00"
                    End If
                End If
                If strZZRQ Is Nothing Then strZZRQ = ""
                strZZRQ = strZZRQ.Trim
                If strZZRQ <> "" Then
                    If strZZRQ.IndexOf(" ") < 0 Then
                        strZZRQ = strZZRQ + " 23:59:59"
                    End If
                End If

                '我的文件
                strSQL = ""
                strSQL = strSQL + " select * from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select" + vbCr
                strSQL = strSQL + "     a.文件标识, b.流水号  ," + vbCr
                strSQL = strSQL + "     a.办理类型, a.办理状态, a.办理子类, b.文件子类, b.文件类型," + vbCr
                strSQL = strSQL + "     b.文件标题, b.机关代字, b.文件年份, b.文件序号, b.主办单位," + vbCr
                strSQL = strSQL + "     a.发送人  , a.接收人  , a.委托人  , a.交接说明" + vbCr
                strSQL = strSQL + "   from" + vbCr

                '获取主表记录
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select" + vbCr
                strSQL = strSQL + "       文件标识, 办理类型, 办理状态," + vbCr
                strSQL = strSQL + "       办理子类 = case " + vbCr
                strSQL = strSQL + "         when 交接标识 like '___1%'    then '" + strGWTHCL + "' " + vbCr
                strSQL = strSQL + "         when 交接标识 like '____1%'   then '" + strGWSHCL + "' " + vbCr
                strSQL = strSQL + "         when 交接标识 like '______1%' then '" + strGWHFCL + "' " + vbCr
                strSQL = strSQL + "         else 办理子类 end," + vbCr
                strSQL = strSQL + "       发送人, 接收人, 委托人, 交接标识, 交接说明 " + vbCr
                strSQL = strSQL + "     from 公文_B_交接" + vbCr
                strSQL = strSQL + "     where 文件标识 = '" + strWJBS + "'" + vbCr                         '指定文件
                strSQL = strSQL + "     and   接收人   = '" + Trim(strUserXM) + "'" + vbCr                 '我做的
                strSQL = strSQL + "     and   交接标识 like '__1%'" + vbCr                                 '我可见
                strSQL = strSQL + "     and   办理状态 in (" + strTaskAllYWCList + ")" + vbCr              '已办完
                If strBLLX <> "" Then
                    strSQL = strSQL + "     and 办理类型 = '" + strBLLX + "'" + vbCr                       '指定类型
                End If
                If strQSRQ <> "" And strZZRQ <> "" Then                                                    '指定日期
                    strSQL = strSQL + "     and 完成日期 between '" + strQSRQ + "' and '" + strZZRQ + "' " + vbCr
                ElseIf strQSRQ <> "" Then
                    strSQL = strSQL + "     and 完成日期 >= '" + strQSRQ + "'" + vbCr
                ElseIf strZZRQ <> "" Then
                    strSQL = strSQL + "     and 完成日期 <= '" + strZZRQ + "'" + vbCr
                Else
                End If
                strSQL = strSQL + "   ) a " + vbCr
                '获取主表记录


                '获取文件信息
                strSQL = strSQL + "   left join " + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select " + vbCr
                strSQL = strSQL + "       文件标识, 流水号  , " + vbCr
                strSQL = strSQL + "       办理类型, 办理状态, 文件子类, 文件类型," + vbCr
                strSQL = strSQL + "       文件标题, 主送单位, " + vbCr
                strSQL = strSQL + "       文件字号, 紧急程度, 秘密等级," + vbCr
                strSQL = strSQL + "       机关代字, 文件年份, 文件序号," + vbCr
                strSQL = strSQL + "       主题词  , 主办单位, 拟稿人  , 拟稿日期," + vbCr
                strSQL = strSQL + "       快速收文" + vbCr
                strSQL = strSQL + "     from 公文_V_全部审批文件新 " + vbCr
                strSQL = strSQL + "     where 文件标识 = '" + strWJBS + "'" + vbCr                                            '指定文件
                If strWJLX <> "" Then
                    strSQL = strSQL + "     and   文件类型 = '" + strWJLX + "'" + vbCr                                        '工作流类型=文件具体类型
                End If
                strSQL = strSQL + "   ) b on a.文件标识 = b.文件标识 " + vbCr
                '获取文件信息


                strSQL = strSQL + "   where b.文件标识 Is Not Null " + vbCr
                strSQL = strSQL + " ) a " + vbCr
                If strWhere <> "" Then
                    strSQL = strSQL + " where " + strWhere + vbCr
                End If
                strSQL = strSQL + " group by " + vbCr
                strSQL = strSQL + "   a.文件标识, a.流水号  ," + vbCr
                strSQL = strSQL + "   a.办理类型, a.办理状态, a.办理子类, a.文件子类, a.文件类型," + vbCr
                strSQL = strSQL + "   a.文件标题, a.机关代字, a.文件年份, a.文件序号, a.主办单位," + vbCr
                strSQL = strSQL + "   a.发送人  , a.接收人  , a.委托人  , a.交接说明" + vbCr
                strSQL = strSQL + " order by a.文件年份 desc, a.机关代字, a.文件序号 desc" + vbCr
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getSQLYBSY_TASK = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据文件类型、开始日期、结束日期计算过期文件的文件搜索SQL
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserXM              ：当前操作人员名称
        '     strBLLX                ：办理类型
        '     strWJLX                ：文件类型-工作流类型
        '     strQSRQ                ：开始日期
        '     strZZRQ                ：结束日期
        '     strWhere               ：搜索条件
        '     strSQL                 ：(返回)文件搜索SQL
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Private Function getSQLGQSY_FILE( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByVal strBLLX As String, _
            ByVal strWJLX As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String, _
            ByVal strWhere As String, _
            ByRef strSQL As String) As Boolean

            getSQLGQSY_FILE = False
            strSQL = ""

            Try
                Dim strFileAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.FileStatusAllYWCList
                Dim strTaskAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TaskStatusAllYWCList
                Dim strLF As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhLf
                Dim strRT As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhRt

                '检查日期间隔
                Dim lngDays As Long
                Dim strOP As String
                If strQSRQ <> "" And strZZRQ <> "" Then
                    Dim objDate(2) As System.DateTime
                    objDate(0) = CType(strQSRQ, System.DateTime)
                    objDate(1) = CType(strZZRQ, System.DateTime)
                    If objDate(0) <= objDate(1) Then
                        strOP = "<="
                    Else
                        strOP = ">"
                    End If
                    lngDays = DateDiff(DateInterval.Day, objDate(0), objDate(1))
                    lngDays = Math.Abs(lngDays)
                Else
                    strOP = ""
                End If

                '我的文件
                strSQL = ""
                strSQL = strSQL + " select a.*" + vbCr
                strSQL = strSQL + " from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select" + vbCr
                strSQL = strSQL + "     a.文件标识, a.流水号  ," + vbCr
                strSQL = strSQL + "     a.办理类型, a.办理状态, a.文件子类, a.文件类型," + vbCr
                strSQL = strSQL + "     a.文件标题, a.主送单位, a.文件字号, a.紧急程度, a.秘密等级," + vbCr
                strSQL = strSQL + "     a.机关代字, a.文件年份, a.文件序号," + vbCr
                strSQL = strSQL + "     a.主题词  , a.主办单位, a.拟稿人  , a.拟稿日期,a.快速收文," + vbCr
                strSQL = strSQL + "     发送日期 = max(a.发送日期)," + vbCr
                strSQL = strSQL + "     办理期限 = max(a.办理期限)," + vbCr
                strSQL = strSQL + "     完成日期 = max(a.完成日期)," + vbCr
                strSQL = strSQL + "     a.备忘提醒" + vbCr
                strSQL = strSQL + "   from" + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select" + vbCr
                strSQL = strSQL + "       a.文件标识, b.流水号  ," + vbCr
                strSQL = strSQL + "       a.办理类型, b.办理状态, b.文件子类, b.文件类型," + vbCr
                strSQL = strSQL + "       b.文件标题, b.主送单位, b.文件字号, b.紧急程度, b.秘密等级," + vbCr
                strSQL = strSQL + "       b.机关代字, b.文件年份, b.文件序号," + vbCr
                strSQL = strSQL + "       b.主题词  , b.主办单位, b.拟稿人  , b.拟稿日期," + vbCr
                strSQL = strSQL + "       a.发送日期, a.办理期限, a.完成日期, b.快速收文," + vbCr
                strSQL = strSQL + "       备忘提醒 = case when c.备忘提醒 is null then '×' else c.备忘提醒 end" + vbCr
                strSQL = strSQL + "     from" + vbCr

                '获取主表记录
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select" + vbCr
                strSQL = strSQL + "         文件标识, 办理类型," + vbCr
                strSQL = strSQL + "         发送日期 = max(发送日期)," + vbCr
                strSQL = strSQL + "         办理期限 = max(办理最后期限)," + vbCr
                strSQL = strSQL + "         完成日期 = max(完成日期)" + vbCr
                strSQL = strSQL + "       from 公文_B_交接" + vbCr
                strSQL = strSQL + "       where 接收人   =    '" + Trim(strUserXM) + "'" + vbCr              '我做的
                strSQL = strSQL + "       and   交接标识 like '__1%'" + vbCr                                 '我可见
                strSQL = strSQL + "       and   办理状态 not in (" + strTaskAllYWCList + ")" + vbCr          '未办完
                strSQL = strSQL + "       and   办理最后期限 <= '" + Now.ToString("yyyy-MM-dd") + "'" + vbCr '超过期限
                strSQL = strSQL + "       and   办理最后期限 is not null" + vbCr
                If strBLLX <> "" Then
                    strSQL = strSQL + "       and 办理类型 = '" + strBLLX + "'" + vbCr                       '指定类型
                End If
                If strOP <> "" Then                                                                          '指定日期
                    strSQL = strSQL + "       and datediff(d, 办理最后期限, '" + Now.ToString("yyyy-MM-dd") + "') " + strOP + " " + lngDays.ToString() + vbCr
                End If
                strSQL = strSQL + "       group by 文件标识,办理类型" + vbCr
                strSQL = strSQL + "     ) a " + vbCr
                '获取主表记录

                '获取备忘提醒
                strSQL = strSQL + "     left join" + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select 文件标识,备忘提醒 = case when max(备忘提醒) = 1 then '√' else '×' end" + vbCr
                strSQL = strSQL + "       from 公文_B_交接" + vbCr
                strSQL = strSQL + "       where 接收人 = '" + strUserXM + "'" + vbCr
                strSQL = strSQL + "       group by 文件标识" + vbCr
                strSQL = strSQL + "     ) c on a.文件标识 = c.文件标识" + vbCr
                '获取备忘提醒


                '获取文件信息
                strSQL = strSQL + "     left join " + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select " + vbCr
                strSQL = strSQL + "         a.文件标识, a.流水号  , " + vbCr
                strSQL = strSQL + "         a.办理类型, a.办理状态, a.文件子类, a.文件类型," + vbCr
                strSQL = strSQL + "         a.文件标题, a.主送单位, " + vbCr
                strSQL = strSQL + "         a.文件字号, a.紧急程度, a.秘密等级," + vbCr
                strSQL = strSQL + "         a.机关代字, a.文件年份, a.文件序号," + vbCr
                strSQL = strSQL + "         a.主题词  , a.主办单位, a.拟稿人  , a.拟稿日期," + vbCr
                strSQL = strSQL + "         a.快速收文" + vbCr
                strSQL = strSQL + "       from 公文_V_全部审批文件新 a" + vbCr
                strSQL = strSQL + "       left join" + vbCr
                strSQL = strSQL + "       (" + vbCr
                strSQL = strSQL + "         select 文件标识" + vbCr
                strSQL = strSQL + "         from 公文_B_交接" + vbCr
                strSQL = strSQL + "         where 接收人   =    '" + Trim(strUserXM) + "'" + vbCr              '我做的
                strSQL = strSQL + "         and   交接标识 like '__1%'" + vbCr                                 '我可见
                strSQL = strSQL + "         and   办理状态 not in (" + strTaskAllYWCList + ")" + vbCr          '未办完
                strSQL = strSQL + "         and   办理最后期限 <= '" + Now.ToString("yyyy-MM-dd") + "'" + vbCr '超过期限
                strSQL = strSQL + "         and   办理最后期限 is not null" + vbCr
                If strBLLX <> "" Then
                    strSQL = strSQL + "         and 办理类型 = '" + strBLLX + "'" + vbCr                       '指定类型
                End If
                If strOP <> "" Then                                                                            '指定日期
                    strSQL = strSQL + "         and datediff(d, 办理最后期限, '" + Now.ToString("yyyy-MM-dd") + "') " + strOP + " " + lngDays.ToString() + vbCr
                End If
                strSQL = strSQL + "         group by 文件标识" + vbCr
                strSQL = strSQL + "       ) b on a.文件标识 = b.文件标识" + vbCr
                strSQL = strSQL + "       where b.文件标识 is not null" + vbCr
                If strWJLX <> "" Then
                    strSQL = strSQL + "       and a.文件类型 = '" + strWJLX + "'" + vbCr
                End If
                strSQL = strSQL + "     ) b on a.文件标识 = b.文件标识 " + vbCr
                '获取文件信息


                strSQL = strSQL + "     where b.文件标识 Is Not Null " + vbCr
                strSQL = strSQL + "   ) a " + vbCr
                strSQL = strSQL + "   group by " + vbCr
                strSQL = strSQL + "     a.文件标识, a.流水号  ," + vbCr
                strSQL = strSQL + "     a.办理类型, a.办理状态, a.文件子类, a.文件类型," + vbCr
                strSQL = strSQL + "     a.文件标题, a.主送单位, a.文件字号, a.紧急程度, a.秘密等级," + vbCr
                strSQL = strSQL + "     a.机关代字, a.文件年份, a.文件序号," + vbCr
                strSQL = strSQL + "     a.主题词  , a.主办单位, a.拟稿人  , a.拟稿日期, a.快速收文, a.备忘提醒" + vbCr
                strSQL = strSQL + " ) a" + vbCr
                If strWhere <> "" Then
                    strSQL = strSQL + " where " + strWhere + vbCr
                End If
                strSQL = strSQL + " order by a.发送日期 desc, a.文件年份 desc, a.机关代字, a.文件序号 desc" + vbCr
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getSQLGQSY_FILE = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据文件类型、开始日期、结束日期计算过期事宜的任务搜索SQL
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strWJBS                ：要查看的文件标识
        '     strUserXM              ：当前操作人员名称
        '     strBLLX                ：办理类型
        '     strWJLX                ：文件类型-工作流类型
        '     strQSRQ                ：开始日期
        '     strZZRQ                ：结束日期
        '     strWhere               ：搜索条件
        '     strSQL                 ：(返回)任务搜索SQL
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Private Function getSQLGQSY_TASK( _
            ByRef strErrMsg As String, _
            ByVal strWJBS As String, _
            ByVal strUserXM As String, _
            ByVal strBLLX As String, _
            ByVal strWJLX As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String, _
            ByVal strWhere As String, _
            ByRef strSQL As String) As Boolean

            getSQLGQSY_TASK = False
            strSQL = ""

            Try
                Dim strFileAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.FileStatusAllYWCList
                Dim strTaskAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TaskStatusAllYWCList
                Dim strLF As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhLf
                Dim strRT As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhRt
                Dim strGWTHCL As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_THCL
                Dim strGWSHCL As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_SHCL
                Dim strGWHFCL As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_HFCL

                '检查日期间隔
                Dim lngDays As Long
                Dim strOP As String
                If strQSRQ <> "" And strZZRQ <> "" Then
                    Dim objDate(2) As System.DateTime
                    objDate(0) = CType(strQSRQ, System.DateTime)
                    objDate(1) = CType(strZZRQ, System.DateTime)
                    If objDate(0) <= objDate(1) Then
                        strOP = "<="
                    Else
                        strOP = ">"
                    End If
                    lngDays = DateDiff(DateInterval.Day, objDate(0), objDate(1))
                    lngDays = Math.Abs(lngDays)
                Else
                    strOP = ""
                End If

                '我的文件
                strSQL = ""
                strSQL = strSQL + " select * from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select" + vbCr
                strSQL = strSQL + "     a.文件标识, b.流水号  ," + vbCr
                strSQL = strSQL + "     a.办理类型, a.办理状态, a.办理子类, b.文件子类, b.文件类型," + vbCr
                strSQL = strSQL + "     b.文件标题, b.机关代字, b.文件年份, b.文件序号, b.主办单位," + vbCr
                strSQL = strSQL + "     a.发送人  , a.接收人  , a.委托人  , a.交接说明" + vbCr
                strSQL = strSQL + "   from" + vbCr

                '获取主表记录
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select" + vbCr
                strSQL = strSQL + "       文件标识, 办理类型, 办理状态," + vbCr
                strSQL = strSQL + "       办理子类 = case " + vbCr
                strSQL = strSQL + "         when 交接标识 like '___1%'    then '" + strGWTHCL + "' " + vbCr
                strSQL = strSQL + "         when 交接标识 like '____1%'   then '" + strGWSHCL + "' " + vbCr
                strSQL = strSQL + "         when 交接标识 like '______1%' then '" + strGWHFCL + "' " + vbCr
                strSQL = strSQL + "         else 办理子类 end," + vbCr
                strSQL = strSQL + "       发送人, 接收人, 委托人, 交接标识, 交接说明 " + vbCr
                strSQL = strSQL + "     from 公文_B_交接" + vbCr
                strSQL = strSQL + "     where 文件标识 = '" + strWJBS + "'" + vbCr                         '指定文件
                strSQL = strSQL + "     and   接收人   = '" + Trim(strUserXM) + "'" + vbCr                 '我做的
                strSQL = strSQL + "     and   交接标识 like '__1%'" + vbCr                                 '我可见
                strSQL = strSQL + "     and   办理状态 not in (" + strTaskAllYWCList + ")" + vbCr          '未办完
                strSQL = strSQL + "     and   办理最后期限 <= '" + Now.ToString("yyyy-MM-dd") + "'" + vbCr '超过期限
                strSQL = strSQL + "     and   办理最后期限 is not null" + vbCr
                If strBLLX <> "" Then
                    strSQL = strSQL + "     and 办理类型 = '" + strBLLX + "'" + vbCr                       '指定类型
                End If
                If strOP <> "" Then                                                                        '指定日期
                    strSQL = strSQL + "     and datediff(d, 办理最后期限, '" + Now.ToString("yyyy-MM-dd") + "') " + strOP + " " + lngDays.ToString() + vbCr
                End If
                strSQL = strSQL + "   ) a " + vbCr
                '获取主表记录


                '获取文件信息
                strSQL = strSQL + "   left join " + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select " + vbCr
                strSQL = strSQL + "       文件标识, 流水号  , " + vbCr
                strSQL = strSQL + "       办理类型, 办理状态, 文件子类, 文件类型," + vbCr
                strSQL = strSQL + "       文件标题, 主送单位, " + vbCr
                strSQL = strSQL + "       文件字号, 紧急程度, 秘密等级," + vbCr
                strSQL = strSQL + "       机关代字, 文件年份, 文件序号," + vbCr
                strSQL = strSQL + "       主题词  , 主办单位, 拟稿人  , 拟稿日期," + vbCr
                strSQL = strSQL + "       快速收文" + vbCr
                strSQL = strSQL + "     from 公文_V_全部审批文件新 " + vbCr
                strSQL = strSQL + "     where 文件标识 = '" + strWJBS + "'" + vbCr                                            '指定文件
                If strWJLX <> "" Then
                    strSQL = strSQL + "     and   文件类型 = '" + strWJLX + "'" + vbCr                                        '工作流类型=文件具体类型
                End If
                strSQL = strSQL + "   ) b on a.文件标识 = b.文件标识 " + vbCr
                '获取文件信息


                strSQL = strSQL + "   where b.文件标识 Is Not Null " + vbCr
                strSQL = strSQL + " ) a " + vbCr
                If strWhere <> "" Then
                    strSQL = strSQL + " where " + strWhere + vbCr
                End If
                strSQL = strSQL + " group by " + vbCr
                strSQL = strSQL + "   a.文件标识, a.流水号  ," + vbCr
                strSQL = strSQL + "   a.办理类型, a.办理状态, a.办理子类, a.文件子类, a.文件类型," + vbCr
                strSQL = strSQL + "   a.文件标题, a.机关代字, a.文件年份, a.文件序号, a.主办单位," + vbCr
                strSQL = strSQL + "   a.发送人  , a.接收人  , a.委托人  , a.交接说明" + vbCr
                strSQL = strSQL + " order by a.文件年份 desc, a.机关代字, a.文件序号 desc" + vbCr
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getSQLGQSY_TASK = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据文件类型、开始日期、结束日期计算催办文件的文件搜索SQL
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserXM              ：当前操作人员名称
        '     strBLLX                ：办理类型
        '     strWJLX                ：文件类型-工作流类型
        '     strQSRQ                ：开始日期
        '     strZZRQ                ：结束日期
        '     strWhere               ：搜索条件
        '     strSQL                 ：(返回)文件搜索SQL
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Private Function getSQLCBSY_FILE( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByVal strBLLX As String, _
            ByVal strWJLX As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String, _
            ByVal strWhere As String, _
            ByRef strSQL As String) As Boolean

            getSQLCBSY_FILE = False
            strSQL = ""

            Try
                Dim strTASK_CBWJ As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_CBWJ
                Dim strLF As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhLf
                Dim strRT As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhRt

                '检查日期间隔
                Dim lngDays As Long
                Dim strOP As String
                If strQSRQ <> "" And strZZRQ <> "" Then
                    Dim objDate(2) As System.DateTime
                    objDate(0) = CType(strQSRQ, System.DateTime)
                    objDate(1) = CType(strZZRQ, System.DateTime)
                    If objDate(0) <= objDate(1) Then
                        strOP = "<="
                    Else
                        strOP = ">"
                    End If
                    lngDays = DateDiff(DateInterval.Day, objDate(0), objDate(1))
                    lngDays = Math.Abs(lngDays)
                Else
                    strOP = ""
                End If

                '我的文件
                strSQL = ""
                strSQL = strSQL + " select a.*" + vbCr
                strSQL = strSQL + " from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select" + vbCr
                strSQL = strSQL + "     a.文件标识, a.流水号  ," + vbCr
                strSQL = strSQL + "     a.办理类型, a.办理状态, a.文件子类, a.文件类型," + vbCr
                strSQL = strSQL + "     a.文件标题, a.主送单位, a.文件字号, a.紧急程度, a.秘密等级," + vbCr
                strSQL = strSQL + "     a.机关代字, a.文件年份, a.文件序号," + vbCr
                strSQL = strSQL + "     a.主题词  , a.主办单位, a.拟稿人  , a.拟稿日期,a.快速收文," + vbCr
                strSQL = strSQL + "     发送日期 = max(a.发送日期)," + vbCr
                strSQL = strSQL + "     办理期限 = NULL," + vbCr
                strSQL = strSQL + "     完成日期 = NULL," + vbCr
                strSQL = strSQL + "     a.备忘提醒" + vbCr
                strSQL = strSQL + "   from" + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select" + vbCr
                strSQL = strSQL + "       a.文件标识, b.流水号  ," + vbCr
                strSQL = strSQL + "       b.办理类型, b.办理状态, b.文件子类, b.文件类型," + vbCr
                strSQL = strSQL + "       b.文件标题, b.主送单位, b.文件字号, b.紧急程度, b.秘密等级," + vbCr
                strSQL = strSQL + "       b.机关代字, b.文件年份, b.文件序号," + vbCr
                strSQL = strSQL + "       b.主题词  , b.主办单位, b.拟稿人  , b.拟稿日期," + vbCr
                strSQL = strSQL + "       a.发送日期, a.办理期限, a.完成日期, b.快速收文," + vbCr
                strSQL = strSQL + "       备忘提醒 = case when c.备忘提醒 is null then '×' else c.备忘提醒 end" + vbCr
                strSQL = strSQL + "     from" + vbCr

                '获取主表记录
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select" + vbCr
                strSQL = strSQL + "         文件标识," + vbCr
                strSQL = strSQL + "         发送日期 = min(催办日期)," + vbCr
                strSQL = strSQL + "         办理期限 = NULL," + vbCr
                strSQL = strSQL + "         完成日期 = NULL" + vbCr
                strSQL = strSQL + "       from 公文_B_催办" + vbCr
                strSQL = strSQL + "       where 催办人 = '" + Trim(strUserXM) + "'" + vbCr                   '我催办
                strSQL = strSQL + "       and   催办日期 is not null" + vbCr
                If strOP <> "" Then                                                                          '指定日期
                    strSQL = strSQL + "       and abs(datediff(d, 催办日期, '" + Now.ToString("yyyy-MM-dd") + "')) " + strOP + " " + lngDays.ToString() + vbCr
                End If
                strSQL = strSQL + "       group by 文件标识" + vbCr
                strSQL = strSQL + "     ) a " + vbCr
                '获取主表记录

                '获取备忘提醒
                strSQL = strSQL + "     left join" + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select 文件标识,备忘提醒 = case when max(备忘提醒) = 1 then '√' else '×' end" + vbCr
                strSQL = strSQL + "       from 公文_B_交接" + vbCr
                strSQL = strSQL + "       where 接收人 = '" + strUserXM + "'" + vbCr
                strSQL = strSQL + "       group by 文件标识" + vbCr
                strSQL = strSQL + "     ) c on a.文件标识 = c.文件标识" + vbCr
                '获取备忘提醒


                '获取文件信息
                strSQL = strSQL + "     left join " + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select " + vbCr
                strSQL = strSQL + "         a.文件标识, a.流水号  , " + vbCr
                strSQL = strSQL + "         a.办理类型, a.办理状态, a.文件子类, 办理子类 = '" + strTASK_CBWJ + "', a.文件类型," + vbCr
                strSQL = strSQL + "         a.文件标题, a.主送单位, " + vbCr
                strSQL = strSQL + "         a.文件字号, a.紧急程度, a.秘密等级," + vbCr
                strSQL = strSQL + "         a.机关代字, a.文件年份, a.文件序号," + vbCr
                strSQL = strSQL + "         a.主题词  , a.主办单位, a.拟稿人  , a.拟稿日期," + vbCr
                strSQL = strSQL + "         a.快速收文" + vbCr
                strSQL = strSQL + "       from 公文_V_全部审批文件新 a" + vbCr
                strSQL = strSQL + "       left join" + vbCr
                strSQL = strSQL + "       (" + vbCr
                strSQL = strSQL + "         select 文件标识" + vbCr
                strSQL = strSQL + "         from 公文_B_催办" + vbCr
                strSQL = strSQL + "         where 催办人 = '" + Trim(strUserXM) + "'" + vbCr                   '我催办
                strSQL = strSQL + "         and   催办日期 is not null" + vbCr
                If strOP <> "" Then                                                                            '指定日期
                    strSQL = strSQL + "         and abs(datediff(d, 催办日期, '" + Now.ToString("yyyy-MM-dd") + "')) " + strOP + " " + lngDays.ToString() + vbCr
                End If
                strSQL = strSQL + "         group by 文件标识" + vbCr
                strSQL = strSQL + "       ) b on a.文件标识 = b.文件标识" + vbCr
                strSQL = strSQL + "       where b.文件标识 is not null" + vbCr
                If strWJLX <> "" Then
                    strSQL = strSQL + "       and a.文件类型 = '" + strWJLX + "'" + vbCr
                End If
                strSQL = strSQL + "     ) b on a.文件标识 = b.文件标识 " + vbCr
                '获取文件信息


                strSQL = strSQL + "     where b.文件标识 Is Not Null " + vbCr
                strSQL = strSQL + "   ) a " + vbCr
                strSQL = strSQL + "   group by " + vbCr
                strSQL = strSQL + "     a.文件标识, a.流水号  ," + vbCr
                strSQL = strSQL + "     a.办理类型, a.办理状态, a.文件子类, a.文件类型," + vbCr
                strSQL = strSQL + "     a.文件标题, a.主送单位, a.文件字号, a.紧急程度, a.秘密等级," + vbCr
                strSQL = strSQL + "     a.机关代字, a.文件年份, a.文件序号," + vbCr
                strSQL = strSQL + "     a.主题词  , a.主办单位, a.拟稿人  , a.拟稿日期, a.快速收文, a.备忘提醒" + vbCr
                strSQL = strSQL + " ) a" + vbCr
                If strWhere <> "" Then
                    strSQL = strSQL + " where " + strWhere + vbCr
                End If
                strSQL = strSQL + " order by a.发送日期 desc, a.文件年份 desc, a.机关代字, a.文件序号 desc" + vbCr
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getSQLCBSY_FILE = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据文件类型、开始日期、结束日期计算催办文件的任务搜索SQL
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strWJBS                ：要查看的文件标识
        '     strUserXM              ：当前操作人员名称
        '     strBLLX                ：办理类型
        '     strWJLX                ：文件类型-工作流类型
        '     strQSRQ                ：开始日期
        '     strZZRQ                ：结束日期
        '     strWhere               ：搜索条件
        '     strSQL                 ：(返回)任务搜索SQL
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Private Function getSQLCBSY_TASK( _
            ByRef strErrMsg As String, _
            ByVal strWJBS As String, _
            ByVal strUserXM As String, _
            ByVal strBLLX As String, _
            ByVal strWJLX As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String, _
            ByVal strWhere As String, _
            ByRef strSQL As String) As Boolean

            getSQLCBSY_TASK = False
            strSQL = ""

            Try
                Dim strTASKSTATUS_ZJB As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASKSTATUS_ZJB
                Dim strTASK_CBWJ As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_CBWJ
                Dim strLF As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhLf
                Dim strRT As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhRt

                '检查日期间隔
                Dim lngDays As Long
                Dim strOP As String
                If strQSRQ <> "" And strZZRQ <> "" Then
                    Dim objDate(2) As System.DateTime
                    objDate(0) = CType(strQSRQ, System.DateTime)
                    objDate(1) = CType(strZZRQ, System.DateTime)
                    If objDate(0) <= objDate(1) Then
                        strOP = "<="
                    Else
                        strOP = ">"
                    End If
                    lngDays = DateDiff(DateInterval.Day, objDate(0), objDate(1))
                    lngDays = Math.Abs(lngDays)
                Else
                    strOP = ""
                End If

                '我的文件
                strSQL = ""
                strSQL = strSQL + " select * from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select" + vbCr
                strSQL = strSQL + "     a.文件标识, b.流水号  ," + vbCr
                strSQL = strSQL + "     b.办理类型, a.办理状态, a.办理子类, b.文件子类, b.文件类型," + vbCr
                strSQL = strSQL + "     b.文件标题, b.机关代字, b.文件年份, b.文件序号, b.主办单位," + vbCr
                strSQL = strSQL + "     a.发送人  , a.接收人  , a.委托人  , a.交接说明" + vbCr
                strSQL = strSQL + "   from" + vbCr

                '获取主表记录
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select" + vbCr
                strSQL = strSQL + "      文件标识, 办理状态 = '" + strTASKSTATUS_ZJB + "', 办理子类 = '" + strTASK_CBWJ + "',"
                strSQL = strSQL + "      发送人 = 催办人, 接收人 = 被催办人, 委托人 = ' ', 交接说明 = 催办说明"
                strSQL = strSQL + "     from 公文_B_催办" + vbCr
                strSQL = strSQL + "     where 文件标识 = '" + strWJBS + "'" + vbCr                         '指定文件
                strSQL = strSQL + "     and   催办人   = '" + Trim(strUserXM) + "'" + vbCr                 '我催办
                strSQL = strSQL + "     and   催办日期 is not null" + vbCr
                If strOP <> "" Then                                                                        '指定日期
                    strSQL = strSQL + "     and abs(datediff(d, 催办日期, '" + Now.ToString("yyyy-MM-dd") + "')) " + strOP + " " + lngDays.ToString() + vbCr
                End If
                strSQL = strSQL + "   ) a " + vbCr
                '获取主表记录


                '获取文件信息
                strSQL = strSQL + "   left join " + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select " + vbCr
                strSQL = strSQL + "       文件标识, 流水号  , " + vbCr
                strSQL = strSQL + "       办理类型, 办理状态, 文件子类, 文件类型," + vbCr
                strSQL = strSQL + "       文件标题, 主送单位, " + vbCr
                strSQL = strSQL + "       文件字号, 紧急程度, 秘密等级," + vbCr
                strSQL = strSQL + "       机关代字, 文件年份, 文件序号," + vbCr
                strSQL = strSQL + "       主题词  , 主办单位, 拟稿人  , 拟稿日期," + vbCr
                strSQL = strSQL + "       快速收文" + vbCr
                strSQL = strSQL + "     from 公文_V_全部审批文件新 " + vbCr
                strSQL = strSQL + "     where 文件标识 = '" + strWJBS + "'" + vbCr                                            '指定文件
                If strWJLX <> "" Then
                    strSQL = strSQL + "     and   文件类型 = '" + strWJLX + "'" + vbCr                                        '工作流类型=文件具体类型
                End If
                strSQL = strSQL + "   ) b on a.文件标识 = b.文件标识 " + vbCr
                '获取文件信息


                strSQL = strSQL + "   where b.文件标识 Is Not Null " + vbCr
                strSQL = strSQL + " ) a " + vbCr
                If strWhere <> "" Then
                    strSQL = strSQL + " where " + strWhere + vbCr
                End If
                strSQL = strSQL + " group by " + vbCr
                strSQL = strSQL + "   a.文件标识, a.流水号  ," + vbCr
                strSQL = strSQL + "   a.办理类型, a.办理状态, a.办理子类, a.文件子类, a.文件类型," + vbCr
                strSQL = strSQL + "   a.文件标题, a.机关代字, a.文件年份, a.文件序号, a.主办单位," + vbCr
                strSQL = strSQL + "   a.发送人  , a.接收人  , a.委托人  , a.交接说明" + vbCr
                strSQL = strSQL + " order by a.文件年份 desc, a.机关代字, a.文件序号 desc" + vbCr
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getSQLCBSY_TASK = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据文件类型、开始日期、结束日期计算被催办文件的文件搜索SQL
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserXM              ：当前操作人员名称
        '     strBLLX                ：办理类型
        '     strWJLX                ：文件类型-工作流类型
        '     strQSRQ                ：开始日期
        '     strZZRQ                ：结束日期
        '     strWhere               ：搜索条件
        '     strSQL                 ：(返回)文件搜索SQL
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Private Function getSQLBCSY_FILE( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByVal strBLLX As String, _
            ByVal strWJLX As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String, _
            ByVal strWhere As String, _
            ByRef strSQL As String) As Boolean

            getSQLBCSY_FILE = False
            strSQL = ""

            Try
                Dim strTASK_CBWJ As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_CBWJ
                Dim strLF As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhLf
                Dim strRT As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhRt

                '检查日期间隔
                Dim lngDays As Long
                Dim strOP As String
                If strQSRQ <> "" And strZZRQ <> "" Then
                    Dim objDate(2) As System.DateTime
                    objDate(0) = CType(strQSRQ, System.DateTime)
                    objDate(1) = CType(strZZRQ, System.DateTime)
                    If objDate(0) <= objDate(1) Then
                        strOP = "<="
                    Else
                        strOP = ">"
                    End If
                    lngDays = DateDiff(DateInterval.Day, objDate(0), objDate(1))
                    lngDays = Math.Abs(lngDays)
                Else
                    strOP = ""
                End If

                '我的文件
                strSQL = ""
                strSQL = strSQL + " select a.*" + vbCr
                strSQL = strSQL + " from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select" + vbCr
                strSQL = strSQL + "     a.文件标识, a.流水号  ," + vbCr
                strSQL = strSQL + "     a.办理类型, a.办理状态, a.文件子类, a.文件类型," + vbCr
                strSQL = strSQL + "     a.文件标题, a.主送单位, a.文件字号, a.紧急程度, a.秘密等级," + vbCr
                strSQL = strSQL + "     a.机关代字, a.文件年份, a.文件序号," + vbCr
                strSQL = strSQL + "     a.主题词  , a.主办单位, a.拟稿人  , a.拟稿日期,a.快速收文," + vbCr
                strSQL = strSQL + "     发送日期 = max(a.发送日期)," + vbCr
                strSQL = strSQL + "     办理期限 = NULL," + vbCr
                strSQL = strSQL + "     完成日期 = NULL," + vbCr
                strSQL = strSQL + "     a.备忘提醒" + vbCr
                strSQL = strSQL + "   from" + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select" + vbCr
                strSQL = strSQL + "       a.文件标识, b.流水号  ," + vbCr
                strSQL = strSQL + "       b.办理类型, b.办理状态, b.文件子类, b.文件类型," + vbCr
                strSQL = strSQL + "       b.文件标题, b.主送单位, b.文件字号, b.紧急程度, b.秘密等级," + vbCr
                strSQL = strSQL + "       b.机关代字, b.文件年份, b.文件序号," + vbCr
                strSQL = strSQL + "       b.主题词  , b.主办单位, b.拟稿人  , b.拟稿日期," + vbCr
                strSQL = strSQL + "       a.发送日期, a.办理期限, a.完成日期, b.快速收文," + vbCr
                strSQL = strSQL + "       备忘提醒 = case when c.备忘提醒 is null then '×' else c.备忘提醒 end" + vbCr
                strSQL = strSQL + "     from" + vbCr

                '获取主表记录
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select" + vbCr
                strSQL = strSQL + "         文件标识," + vbCr
                strSQL = strSQL + "         发送日期 = min(催办日期)," + vbCr
                strSQL = strSQL + "         办理期限 = NULL," + vbCr
                strSQL = strSQL + "         完成日期 = NULL" + vbCr
                strSQL = strSQL + "       from 公文_B_催办" + vbCr
                strSQL = strSQL + "       where 被催办人 = '" + Trim(strUserXM) + "'" + vbCr                 '我被催办
                strSQL = strSQL + "       and   催办日期 is not null" + vbCr
                If strOP <> "" Then                                                                          '指定日期
                    strSQL = strSQL + "       and abs(datediff(d, 催办日期, '" + Now.ToString("yyyy-MM-dd") + "')) " + strOP + " " + lngDays.ToString() + vbCr
                End If
                strSQL = strSQL + "       group by 文件标识" + vbCr
                strSQL = strSQL + "     ) a " + vbCr
                '获取主表记录

                '获取备忘提醒
                strSQL = strSQL + "     left join" + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select 文件标识,备忘提醒 = case when max(备忘提醒) = 1 then '√' else '×' end" + vbCr
                strSQL = strSQL + "       from 公文_B_交接" + vbCr
                strSQL = strSQL + "       where 接收人 = '" + strUserXM + "'" + vbCr
                strSQL = strSQL + "       group by 文件标识" + vbCr
                strSQL = strSQL + "     ) c on a.文件标识 = c.文件标识" + vbCr
                '获取备忘提醒


                '获取文件信息
                strSQL = strSQL + "     left join " + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select " + vbCr
                strSQL = strSQL + "         a.文件标识, a.流水号  , " + vbCr
                strSQL = strSQL + "         a.办理类型, a.办理状态, a.文件子类, 办理子类 = '" + strTASK_CBWJ + "', a.文件类型," + vbCr
                strSQL = strSQL + "         a.文件标题, a.主送单位, " + vbCr
                strSQL = strSQL + "         a.文件字号, a.紧急程度, a.秘密等级," + vbCr
                strSQL = strSQL + "         a.机关代字, a.文件年份, a.文件序号," + vbCr
                strSQL = strSQL + "         a.主题词  , a.主办单位, a.拟稿人  , a.拟稿日期," + vbCr
                strSQL = strSQL + "         a.快速收文" + vbCr
                strSQL = strSQL + "       from 公文_V_全部审批文件新 a" + vbCr
                strSQL = strSQL + "       left join" + vbCr
                strSQL = strSQL + "       (" + vbCr
                strSQL = strSQL + "         select 文件标识" + vbCr
                strSQL = strSQL + "         from 公文_B_催办" + vbCr
                strSQL = strSQL + "         where 被催办人 = '" + Trim(strUserXM) + "'" + vbCr                 '我被催办
                strSQL = strSQL + "         and   催办日期 is not null" + vbCr
                If strOP <> "" Then                                                                            '指定日期
                    strSQL = strSQL + "         and abs(datediff(d, 催办日期, '" + Now.ToString("yyyy-MM-dd") + "')) " + strOP + " " + lngDays.ToString() + vbCr
                End If
                strSQL = strSQL + "         group by 文件标识" + vbCr
                strSQL = strSQL + "       ) b on a.文件标识 = b.文件标识" + vbCr
                strSQL = strSQL + "       where b.文件标识 is not null" + vbCr
                If strWJLX <> "" Then
                    strSQL = strSQL + "       and a.文件类型 = '" + strWJLX + "'" + vbCr
                End If
                strSQL = strSQL + "     ) b on a.文件标识 = b.文件标识 " + vbCr
                '获取文件信息


                strSQL = strSQL + "     where b.文件标识 Is Not Null " + vbCr
                strSQL = strSQL + "   ) a " + vbCr
                strSQL = strSQL + "   group by " + vbCr
                strSQL = strSQL + "     a.文件标识, a.流水号  ," + vbCr
                strSQL = strSQL + "     a.办理类型, a.办理状态, a.文件子类, a.文件类型," + vbCr
                strSQL = strSQL + "     a.文件标题, a.主送单位, a.文件字号, a.紧急程度, a.秘密等级," + vbCr
                strSQL = strSQL + "     a.机关代字, a.文件年份, a.文件序号," + vbCr
                strSQL = strSQL + "     a.主题词  , a.主办单位, a.拟稿人  , a.拟稿日期, a.快速收文, a.备忘提醒" + vbCr
                strSQL = strSQL + " ) a" + vbCr
                If strWhere <> "" Then
                    strSQL = strSQL + " where " + strWhere + vbCr
                End If
                strSQL = strSQL + " order by a.发送日期 desc, a.文件年份 desc, a.机关代字, a.文件序号 desc" + vbCr
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getSQLBCSY_FILE = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据文件类型、开始日期、结束日期计算被催办文件的任务搜索SQL
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strWJBS                ：要查看的文件标识
        '     strUserXM              ：当前操作人员名称
        '     strBLLX                ：办理类型
        '     strWJLX                ：文件类型-工作流类型
        '     strQSRQ                ：开始日期
        '     strZZRQ                ：结束日期
        '     strWhere               ：搜索条件
        '     strSQL                 ：(返回)任务搜索SQL
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Private Function getSQLBCSY_TASK( _
            ByRef strErrMsg As String, _
            ByVal strWJBS As String, _
            ByVal strUserXM As String, _
            ByVal strBLLX As String, _
            ByVal strWJLX As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String, _
            ByVal strWhere As String, _
            ByRef strSQL As String) As Boolean

            getSQLBCSY_TASK = False
            strSQL = ""

            Try
                Dim strTASKSTATUS_ZJB As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASKSTATUS_ZJB
                Dim strTASK_CBWJ As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_CBWJ
                Dim strLF As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhLf
                Dim strRT As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhRt

                '检查日期间隔
                Dim lngDays As Long
                Dim strOP As String
                If strQSRQ <> "" And strZZRQ <> "" Then
                    Dim objDate(2) As System.DateTime
                    objDate(0) = CType(strQSRQ, System.DateTime)
                    objDate(1) = CType(strZZRQ, System.DateTime)
                    If objDate(0) <= objDate(1) Then
                        strOP = "<="
                    Else
                        strOP = ">"
                    End If
                    lngDays = DateDiff(DateInterval.Day, objDate(0), objDate(1))
                    lngDays = Math.Abs(lngDays)
                Else
                    strOP = ""
                End If

                '我的文件
                strSQL = ""
                strSQL = strSQL + " select * from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select" + vbCr
                strSQL = strSQL + "     a.文件标识, b.流水号  ," + vbCr
                strSQL = strSQL + "     b.办理类型, a.办理状态, a.办理子类, b.文件子类, b.文件类型," + vbCr
                strSQL = strSQL + "     b.文件标题, b.机关代字, b.文件年份, b.文件序号, b.主办单位," + vbCr
                strSQL = strSQL + "     a.发送人  , a.接收人  , a.委托人  , a.交接说明" + vbCr
                strSQL = strSQL + "   from" + vbCr

                '获取主表记录
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select" + vbCr
                strSQL = strSQL + "      文件标识, 办理状态 = '" + strTASKSTATUS_ZJB + "', 办理子类 = '" + strTASK_CBWJ + "',"
                strSQL = strSQL + "      发送人 = 催办人, 接收人 = 被催办人, 委托人 = ' ', 交接说明 = 催办说明"
                strSQL = strSQL + "     from 公文_B_催办" + vbCr
                strSQL = strSQL + "     where 文件标识 = '" + strWJBS + "'" + vbCr                         '指定文件
                strSQL = strSQL + "     and   被催办人 = '" + Trim(strUserXM) + "'" + vbCr                 '我被催办
                strSQL = strSQL + "     and   催办日期 is not null" + vbCr
                If strOP <> "" Then                                                                        '指定日期
                    strSQL = strSQL + "       and abs(datediff(d, 催办日期, '" + Now.ToString("yyyy-MM-dd") + "')) " + strOP + " " + lngDays.ToString() + vbCr
                End If
                strSQL = strSQL + "   ) a " + vbCr
                '获取主表记录


                '获取文件信息
                strSQL = strSQL + "   left join " + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select " + vbCr
                strSQL = strSQL + "       文件标识, 流水号  , " + vbCr
                strSQL = strSQL + "       办理类型, 办理状态, 文件子类, 文件类型," + vbCr
                strSQL = strSQL + "       文件标题, 主送单位, " + vbCr
                strSQL = strSQL + "       文件字号, 紧急程度, 秘密等级," + vbCr
                strSQL = strSQL + "       机关代字, 文件年份, 文件序号," + vbCr
                strSQL = strSQL + "       主题词  , 主办单位, 拟稿人  , 拟稿日期," + vbCr
                strSQL = strSQL + "       快速收文" + vbCr
                strSQL = strSQL + "     from 公文_V_全部审批文件新 " + vbCr
                strSQL = strSQL + "     where 文件标识 = '" + strWJBS + "'" + vbCr                                            '指定文件
                If strWJLX <> "" Then
                    strSQL = strSQL + "     and   文件类型 = '" + strWJLX + "'" + vbCr                                        '工作流类型=文件具体类型
                End If
                strSQL = strSQL + "   ) b on a.文件标识 = b.文件标识 " + vbCr
                '获取文件信息


                strSQL = strSQL + "   where b.文件标识 Is Not Null " + vbCr
                strSQL = strSQL + " ) a " + vbCr
                If strWhere <> "" Then
                    strSQL = strSQL + " where " + strWhere + vbCr
                End If
                strSQL = strSQL + " group by " + vbCr
                strSQL = strSQL + "   a.文件标识, a.流水号  ," + vbCr
                strSQL = strSQL + "   a.办理类型, a.办理状态, a.办理子类, a.文件子类, a.文件类型," + vbCr
                strSQL = strSQL + "   a.文件标题, a.机关代字, a.文件年份, a.文件序号, a.主办单位," + vbCr
                strSQL = strSQL + "   a.发送人  , a.接收人  , a.委托人  , a.交接说明" + vbCr
                strSQL = strSQL + " order by a.文件年份 desc, a.机关代字, a.文件序号 desc" + vbCr
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getSQLBCSY_TASK = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据文件类型、开始日期、结束日期计算督办文件的文件搜索SQL
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserXM              ：当前操作人员名称
        '     strBLLX                ：办理类型
        '     strWJLX                ：文件类型-工作流类型
        '     strQSRQ                ：开始日期
        '     strZZRQ                ：结束日期
        '     strWhere               ：搜索条件
        '     strSQL                 ：(返回)文件搜索SQL
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Private Function getSQLDBWJ_FILE( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByVal strBLLX As String, _
            ByVal strWJLX As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String, _
            ByVal strWhere As String, _
            ByRef strSQL As String) As Boolean

            getSQLDBWJ_FILE = False
            strSQL = ""

            Try
                Dim strTASK_DBWJ As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_DBWJ
                Dim strLF As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhLf
                Dim strRT As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhRt

                '检查日期间隔
                Dim lngDays As Long
                Dim strOP As String
                If strQSRQ <> "" And strZZRQ <> "" Then
                    Dim objDate(2) As System.DateTime
                    objDate(0) = CType(strQSRQ, System.DateTime)
                    objDate(1) = CType(strZZRQ, System.DateTime)
                    If objDate(0) <= objDate(1) Then
                        strOP = "<="
                    Else
                        strOP = ">"
                    End If
                    lngDays = DateDiff(DateInterval.Day, objDate(0), objDate(1))
                    lngDays = Math.Abs(lngDays)
                Else
                    strOP = ""
                End If

                '我的文件
                strSQL = ""
                strSQL = strSQL + " select a.*" + vbCr
                strSQL = strSQL + " from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select" + vbCr
                strSQL = strSQL + "     a.文件标识, a.流水号  ," + vbCr
                strSQL = strSQL + "     a.办理类型, a.办理状态, a.文件子类, a.文件类型," + vbCr
                strSQL = strSQL + "     a.文件标题, a.主送单位, a.文件字号, a.紧急程度, a.秘密等级," + vbCr
                strSQL = strSQL + "     a.机关代字, a.文件年份, a.文件序号," + vbCr
                strSQL = strSQL + "     a.主题词  , a.主办单位, a.拟稿人  , a.拟稿日期,a.快速收文," + vbCr
                strSQL = strSQL + "     发送日期 = max(a.发送日期)," + vbCr
                strSQL = strSQL + "     办理期限 = NULL," + vbCr
                strSQL = strSQL + "     完成日期 = NULL," + vbCr
                strSQL = strSQL + "     a.备忘提醒" + vbCr
                strSQL = strSQL + "   from" + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select" + vbCr
                strSQL = strSQL + "       a.文件标识, b.流水号  ," + vbCr
                strSQL = strSQL + "       b.办理类型, b.办理状态, b.文件子类, b.文件类型," + vbCr
                strSQL = strSQL + "       b.文件标题, b.主送单位, b.文件字号, b.紧急程度, b.秘密等级," + vbCr
                strSQL = strSQL + "       b.机关代字, b.文件年份, b.文件序号," + vbCr
                strSQL = strSQL + "       b.主题词  , b.主办单位, b.拟稿人  , b.拟稿日期," + vbCr
                strSQL = strSQL + "       a.发送日期, a.办理期限, a.完成日期, b.快速收文," + vbCr
                strSQL = strSQL + "       备忘提醒 = case when c.备忘提醒 is null then '×' else c.备忘提醒 end" + vbCr
                strSQL = strSQL + "     from" + vbCr

                '获取主表记录
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select" + vbCr
                strSQL = strSQL + "         文件标识," + vbCr
                strSQL = strSQL + "         发送日期 = min(督办日期)," + vbCr
                strSQL = strSQL + "         办理期限 = NULL," + vbCr
                strSQL = strSQL + "         完成日期 = NULL" + vbCr
                strSQL = strSQL + "       from 公文_B_督办" + vbCr
                strSQL = strSQL + "       where 督办人 = '" + Trim(strUserXM) + "'" + vbCr                   '我督办
                strSQL = strSQL + "       and   督办日期 is not null" + vbCr
                If strOP <> "" Then                                                                          '指定日期
                    strSQL = strSQL + "       and abs(datediff(d, 督办日期, '" + Now.ToString("yyyy-MM-dd") + "')) " + strOP + " " + lngDays.ToString() + vbCr
                End If
                strSQL = strSQL + "       group by 文件标识" + vbCr
                strSQL = strSQL + "     ) a " + vbCr
                '获取主表记录

                '获取备忘提醒
                strSQL = strSQL + "     left join" + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select 文件标识,备忘提醒 = case when max(备忘提醒) = 1 then '√' else '×' end" + vbCr
                strSQL = strSQL + "       from 公文_B_交接" + vbCr
                strSQL = strSQL + "       where 接收人 = '" + strUserXM + "'" + vbCr
                strSQL = strSQL + "       group by 文件标识" + vbCr
                strSQL = strSQL + "     ) c on a.文件标识 = c.文件标识" + vbCr
                '获取备忘提醒


                '获取文件信息
                strSQL = strSQL + "     left join " + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select " + vbCr
                strSQL = strSQL + "         a.文件标识, a.流水号  , " + vbCr
                strSQL = strSQL + "         a.办理类型, a.办理状态, a.文件子类, 办理子类 = '" + strTASK_DBWJ + "', a.文件类型," + vbCr
                strSQL = strSQL + "         a.文件标题, a.主送单位, " + vbCr
                strSQL = strSQL + "         a.文件字号, a.紧急程度, a.秘密等级," + vbCr
                strSQL = strSQL + "         a.机关代字, a.文件年份, a.文件序号," + vbCr
                strSQL = strSQL + "         a.主题词  , a.主办单位, a.拟稿人  , a.拟稿日期," + vbCr
                strSQL = strSQL + "         a.快速收文" + vbCr
                strSQL = strSQL + "       from 公文_V_全部审批文件新 a" + vbCr
                strSQL = strSQL + "       left join" + vbCr
                strSQL = strSQL + "       (" + vbCr
                strSQL = strSQL + "         select 文件标识" + vbCr
                strSQL = strSQL + "         from 公文_B_督办" + vbCr
                strSQL = strSQL + "         where 督办人 = '" + Trim(strUserXM) + "'" + vbCr                   '我督办
                strSQL = strSQL + "         and   督办日期 is not null" + vbCr
                If strOP <> "" Then                                                                            '指定日期
                    strSQL = strSQL + "         and abs(datediff(d, 督办日期, '" + Now.ToString("yyyy-MM-dd") + "')) " + strOP + " " + lngDays.ToString() + vbCr
                End If
                strSQL = strSQL + "         group by 文件标识" + vbCr
                strSQL = strSQL + "       ) b on a.文件标识 = b.文件标识" + vbCr
                strSQL = strSQL + "       where b.文件标识 is not null" + vbCr
                If strWJLX <> "" Then
                    strSQL = strSQL + "       and a.文件类型 = '" + strWJLX + "'" + vbCr
                End If
                strSQL = strSQL + "     ) b on a.文件标识 = b.文件标识 " + vbCr
                '获取文件信息


                strSQL = strSQL + "     where b.文件标识 Is Not Null " + vbCr
                strSQL = strSQL + "   ) a " + vbCr
                strSQL = strSQL + "   group by " + vbCr
                strSQL = strSQL + "     a.文件标识, a.流水号  ," + vbCr
                strSQL = strSQL + "     a.办理类型, a.办理状态, a.文件子类, a.文件类型," + vbCr
                strSQL = strSQL + "     a.文件标题, a.主送单位, a.文件字号, a.紧急程度, a.秘密等级," + vbCr
                strSQL = strSQL + "     a.机关代字, a.文件年份, a.文件序号," + vbCr
                strSQL = strSQL + "     a.主题词  , a.主办单位, a.拟稿人  , a.拟稿日期, a.快速收文, a.备忘提醒" + vbCr
                strSQL = strSQL + " ) a" + vbCr
                If strWhere <> "" Then
                    strSQL = strSQL + " where " + strWhere + vbCr
                End If
                strSQL = strSQL + " order by a.发送日期 desc, a.文件年份 desc, a.机关代字, a.文件序号 desc" + vbCr
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getSQLDBWJ_FILE = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据文件类型、开始日期、结束日期计算督办文件的任务搜索SQL
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strWJBS                ：要查看的文件标识
        '     strUserXM              ：当前操作人员名称
        '     strBLLX                ：办理类型
        '     strWJLX                ：文件类型-工作流类型
        '     strQSRQ                ：开始日期
        '     strZZRQ                ：结束日期
        '     strWhere               ：搜索条件
        '     strSQL                 ：(返回)任务搜索SQL
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Private Function getSQLDBWJ_TASK( _
            ByRef strErrMsg As String, _
            ByVal strWJBS As String, _
            ByVal strUserXM As String, _
            ByVal strBLLX As String, _
            ByVal strWJLX As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String, _
            ByVal strWhere As String, _
            ByRef strSQL As String) As Boolean

            getSQLDBWJ_TASK = False
            strSQL = ""

            Try
                Dim strTASKSTATUS_ZJB As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASKSTATUS_ZJB
                Dim strTASK_DBWJ As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_DBWJ
                Dim strLF As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhLf
                Dim strRT As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhRt

                '检查日期间隔
                Dim lngDays As Long
                Dim strOP As String
                If strQSRQ <> "" And strZZRQ <> "" Then
                    Dim objDate(2) As System.DateTime
                    objDate(0) = CType(strQSRQ, System.DateTime)
                    objDate(1) = CType(strZZRQ, System.DateTime)
                    If objDate(0) <= objDate(1) Then
                        strOP = "<="
                    Else
                        strOP = ">"
                    End If
                    lngDays = DateDiff(DateInterval.Day, objDate(0), objDate(1))
                    lngDays = Math.Abs(lngDays)
                Else
                    strOP = ""
                End If

                '我的文件
                strSQL = ""
                strSQL = strSQL + " select * from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select" + vbCr
                strSQL = strSQL + "     a.文件标识, b.流水号  ," + vbCr
                strSQL = strSQL + "     b.办理类型, a.办理状态, a.办理子类, b.文件子类, b.文件类型," + vbCr
                strSQL = strSQL + "     b.文件标题, b.机关代字, b.文件年份, b.文件序号, b.主办单位," + vbCr
                strSQL = strSQL + "     a.发送人  , a.接收人  , a.委托人  , a.交接说明" + vbCr
                strSQL = strSQL + "   from" + vbCr

                '获取主表记录
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select" + vbCr
                strSQL = strSQL + "      文件标识, 办理状态 = '" + strTASKSTATUS_ZJB + "', 办理子类 = '" + strTASK_DBWJ + "',"
                strSQL = strSQL + "      发送人 = 督办人, 接收人 = 被督办人, 委托人 = ' ', 交接说明 = 督办要求"
                strSQL = strSQL + "     from 公文_B_督办" + vbCr
                strSQL = strSQL + "     where 文件标识 = '" + strWJBS + "'" + vbCr                         '指定文件
                strSQL = strSQL + "     and   督办人   = '" + Trim(strUserXM) + "'" + vbCr                 '我督办
                strSQL = strSQL + "     and   督办日期 is not null" + vbCr
                If strOP <> "" Then                                                                        '指定日期
                    strSQL = strSQL + "       and abs(datediff(d, 督办日期, '" + Now.ToString("yyyy-MM-dd") + "')) " + strOP + " " + lngDays.ToString() + vbCr
                End If
                strSQL = strSQL + "   ) a " + vbCr
                '获取主表记录


                '获取文件信息
                strSQL = strSQL + "   left join " + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select " + vbCr
                strSQL = strSQL + "       文件标识, 流水号  , " + vbCr
                strSQL = strSQL + "       办理类型, 办理状态, 文件子类, 文件类型," + vbCr
                strSQL = strSQL + "       文件标题, 主送单位, " + vbCr
                strSQL = strSQL + "       文件字号, 紧急程度, 秘密等级," + vbCr
                strSQL = strSQL + "       机关代字, 文件年份, 文件序号," + vbCr
                strSQL = strSQL + "       主题词  , 主办单位, 拟稿人  , 拟稿日期," + vbCr
                strSQL = strSQL + "       快速收文" + vbCr
                strSQL = strSQL + "     from 公文_V_全部审批文件新 " + vbCr
                strSQL = strSQL + "     where 文件标识 = '" + strWJBS + "'" + vbCr                                            '指定文件
                If strWJLX <> "" Then
                    strSQL = strSQL + "     and   文件类型 = '" + strWJLX + "'" + vbCr                                        '工作流类型=文件具体类型
                End If
                strSQL = strSQL + "   ) b on a.文件标识 = b.文件标识 " + vbCr
                '获取文件信息


                strSQL = strSQL + "   where b.文件标识 Is Not Null " + vbCr
                strSQL = strSQL + " ) a " + vbCr
                If strWhere <> "" Then
                    strSQL = strSQL + " where " + strWhere + vbCr
                End If
                strSQL = strSQL + " group by " + vbCr
                strSQL = strSQL + "   a.文件标识, a.流水号  ," + vbCr
                strSQL = strSQL + "   a.办理类型, a.办理状态, a.办理子类, a.文件子类, a.文件类型," + vbCr
                strSQL = strSQL + "   a.文件标题, a.机关代字, a.文件年份, a.文件序号, a.主办单位," + vbCr
                strSQL = strSQL + "   a.发送人  , a.接收人  , a.委托人  , a.交接说明" + vbCr
                strSQL = strSQL + " order by a.文件年份 desc, a.机关代字, a.文件序号 desc" + vbCr
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getSQLDBWJ_TASK = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据文件类型、开始日期、结束日期计算被督办文件的文件搜索SQL
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserXM              ：当前操作人员名称
        '     strBLLX                ：办理类型
        '     strWJLX                ：文件类型-工作流类型
        '     strQSRQ                ：开始日期
        '     strZZRQ                ：结束日期
        '     strWhere               ：搜索条件
        '     strSQL                 ：(返回)文件搜索SQL
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Private Function getSQLBDWJ_FILE( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByVal strBLLX As String, _
            ByVal strWJLX As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String, _
            ByVal strWhere As String, _
            ByRef strSQL As String) As Boolean

            getSQLBDWJ_FILE = False
            strSQL = ""

            Try
                Dim strTASK_DBWJ As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_DBWJ
                Dim strLF As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhLf
                Dim strRT As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhRt

                '检查日期间隔
                Dim lngDays As Long
                Dim strOP As String
                If strQSRQ <> "" And strZZRQ <> "" Then
                    Dim objDate(2) As System.DateTime
                    objDate(0) = CType(strQSRQ, System.DateTime)
                    objDate(1) = CType(strZZRQ, System.DateTime)
                    If objDate(0) <= objDate(1) Then
                        strOP = "<="
                    Else
                        strOP = ">"
                    End If
                    lngDays = DateDiff(DateInterval.Day, objDate(0), objDate(1))
                    lngDays = Math.Abs(lngDays)
                Else
                    strOP = ""
                End If

                '我的文件
                strSQL = ""
                strSQL = strSQL + " select a.*" + vbCr
                strSQL = strSQL + " from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select" + vbCr
                strSQL = strSQL + "     a.文件标识, a.流水号  ," + vbCr
                strSQL = strSQL + "     a.办理类型, a.办理状态, a.文件子类, a.文件类型," + vbCr
                strSQL = strSQL + "     a.文件标题, a.主送单位, a.文件字号, a.紧急程度, a.秘密等级," + vbCr
                strSQL = strSQL + "     a.机关代字, a.文件年份, a.文件序号," + vbCr
                strSQL = strSQL + "     a.主题词  , a.主办单位, a.拟稿人  , a.拟稿日期,a.快速收文," + vbCr
                strSQL = strSQL + "     发送日期 = max(a.发送日期)," + vbCr
                strSQL = strSQL + "     办理期限 = NULL," + vbCr
                strSQL = strSQL + "     完成日期 = NULL," + vbCr
                strSQL = strSQL + "     a.备忘提醒" + vbCr
                strSQL = strSQL + "   from" + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select" + vbCr
                strSQL = strSQL + "       a.文件标识, b.流水号  ," + vbCr
                strSQL = strSQL + "       b.办理类型, b.办理状态, b.文件子类, b.文件类型," + vbCr
                strSQL = strSQL + "       b.文件标题, b.主送单位, b.文件字号, b.紧急程度, b.秘密等级," + vbCr
                strSQL = strSQL + "       b.机关代字, b.文件年份, b.文件序号," + vbCr
                strSQL = strSQL + "       b.主题词  , b.主办单位, b.拟稿人  , b.拟稿日期," + vbCr
                strSQL = strSQL + "       a.发送日期, a.办理期限, a.完成日期, b.快速收文," + vbCr
                strSQL = strSQL + "       备忘提醒 = case when c.备忘提醒 is null then '×' else c.备忘提醒 end" + vbCr
                strSQL = strSQL + "     from" + vbCr

                '获取主表记录
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select" + vbCr
                strSQL = strSQL + "         文件标识," + vbCr
                strSQL = strSQL + "         发送日期 = min(督办日期)," + vbCr
                strSQL = strSQL + "         办理期限 = NULL," + vbCr
                strSQL = strSQL + "         完成日期 = NULL" + vbCr
                strSQL = strSQL + "       from 公文_B_督办" + vbCr
                strSQL = strSQL + "       where 被督办人 = '" + Trim(strUserXM) + "'" + vbCr                 '我被督办
                strSQL = strSQL + "       and   督办日期 is not null" + vbCr
                If strOP <> "" Then                                                                          '指定日期
                    strSQL = strSQL + "       and abs(datediff(d, 督办日期, '" + Now.ToString("yyyy-MM-dd") + "')) " + strOP + " " + lngDays.ToString() + vbCr
                End If
                strSQL = strSQL + "       group by 文件标识" + vbCr
                strSQL = strSQL + "     ) a " + vbCr
                '获取主表记录

                '获取备忘提醒
                strSQL = strSQL + "     left join" + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select 文件标识,备忘提醒 = case when max(备忘提醒) = 1 then '√' else '×' end" + vbCr
                strSQL = strSQL + "       from 公文_B_交接" + vbCr
                strSQL = strSQL + "       where 接收人 = '" + strUserXM + "'" + vbCr
                strSQL = strSQL + "       group by 文件标识" + vbCr
                strSQL = strSQL + "     ) c on a.文件标识 = c.文件标识" + vbCr
                '获取备忘提醒


                '获取文件信息
                strSQL = strSQL + "     left join " + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select " + vbCr
                strSQL = strSQL + "         a.文件标识, a.流水号  , " + vbCr
                strSQL = strSQL + "         a.办理类型, a.办理状态, a.文件子类, 办理子类 = '" + strTASK_DBWJ + "', a.文件类型," + vbCr
                strSQL = strSQL + "         a.文件标题, a.主送单位, " + vbCr
                strSQL = strSQL + "         a.文件字号, a.紧急程度, a.秘密等级," + vbCr
                strSQL = strSQL + "         a.机关代字, a.文件年份, a.文件序号," + vbCr
                strSQL = strSQL + "         a.主题词  , a.主办单位, a.拟稿人  , a.拟稿日期," + vbCr
                strSQL = strSQL + "         a.快速收文" + vbCr
                strSQL = strSQL + "       from 公文_V_全部审批文件新 a" + vbCr
                strSQL = strSQL + "       left join" + vbCr
                strSQL = strSQL + "       (" + vbCr
                strSQL = strSQL + "         select 文件标识" + vbCr
                strSQL = strSQL + "         from 公文_B_督办" + vbCr
                strSQL = strSQL + "         where 被督办人 = '" + Trim(strUserXM) + "'" + vbCr                 '我被督办
                strSQL = strSQL + "         and   督办日期 is not null" + vbCr
                If strOP <> "" Then                                                                            '指定日期
                    strSQL = strSQL + "         and abs(datediff(d, 督办日期, '" + Now.ToString("yyyy-MM-dd") + "')) " + strOP + " " + lngDays.ToString() + vbCr
                End If
                strSQL = strSQL + "         group by 文件标识" + vbCr
                strSQL = strSQL + "       ) b on a.文件标识 = b.文件标识" + vbCr
                strSQL = strSQL + "       where b.文件标识 is not null" + vbCr
                If strWJLX <> "" Then
                    strSQL = strSQL + "       and a.文件类型 = '" + strWJLX + "'" + vbCr
                End If
                strSQL = strSQL + "     ) b on a.文件标识 = b.文件标识 " + vbCr
                '获取文件信息


                strSQL = strSQL + "     where b.文件标识 Is Not Null " + vbCr
                strSQL = strSQL + "   ) a " + vbCr
                strSQL = strSQL + "   group by " + vbCr
                strSQL = strSQL + "     a.文件标识, a.流水号  ," + vbCr
                strSQL = strSQL + "     a.办理类型, a.办理状态, a.文件子类, a.文件类型," + vbCr
                strSQL = strSQL + "     a.文件标题, a.主送单位, a.文件字号, a.紧急程度, a.秘密等级," + vbCr
                strSQL = strSQL + "     a.机关代字, a.文件年份, a.文件序号," + vbCr
                strSQL = strSQL + "     a.主题词  , a.主办单位, a.拟稿人  , a.拟稿日期, a.快速收文, a.备忘提醒" + vbCr
                strSQL = strSQL + " ) a" + vbCr
                If strWhere <> "" Then
                    strSQL = strSQL + " where " + strWhere + vbCr
                End If
                strSQL = strSQL + " order by a.发送日期 desc, a.文件年份 desc, a.机关代字, a.文件序号 desc" + vbCr
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getSQLBDWJ_FILE = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据文件类型、开始日期、结束日期计算被督办文件的任务搜索SQL
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strWJBS                ：要查看的文件标识
        '     strUserXM              ：当前操作人员名称
        '     strBLLX                ：办理类型
        '     strWJLX                ：文件类型-工作流类型
        '     strQSRQ                ：开始日期
        '     strZZRQ                ：结束日期
        '     strWhere               ：搜索条件
        '     strSQL                 ：(返回)任务搜索SQL
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Private Function getSQLBDWJ_TASK( _
            ByRef strErrMsg As String, _
            ByVal strWJBS As String, _
            ByVal strUserXM As String, _
            ByVal strBLLX As String, _
            ByVal strWJLX As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String, _
            ByVal strWhere As String, _
            ByRef strSQL As String) As Boolean

            getSQLBDWJ_TASK = False
            strSQL = ""

            Try
                Dim strTASKSTATUS_ZJB As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASKSTATUS_ZJB
                Dim strTASK_DBWJ As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_DBWJ
                Dim strLF As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhLf
                Dim strRT As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhRt

                '检查日期间隔
                Dim lngDays As Long
                Dim strOP As String
                If strQSRQ <> "" And strZZRQ <> "" Then
                    Dim objDate(2) As System.DateTime
                    objDate(0) = CType(strQSRQ, System.DateTime)
                    objDate(1) = CType(strZZRQ, System.DateTime)
                    If objDate(0) <= objDate(1) Then
                        strOP = "<="
                    Else
                        strOP = ">"
                    End If
                    lngDays = DateDiff(DateInterval.Day, objDate(0), objDate(1))
                    lngDays = Math.Abs(lngDays)
                Else
                    strOP = ""
                End If

                '我的文件
                strSQL = ""
                strSQL = strSQL + " select * from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select" + vbCr
                strSQL = strSQL + "     a.文件标识, b.流水号  ," + vbCr
                strSQL = strSQL + "     b.办理类型, a.办理状态, a.办理子类, b.文件子类, b.文件类型," + vbCr
                strSQL = strSQL + "     b.文件标题, b.机关代字, b.文件年份, b.文件序号, b.主办单位," + vbCr
                strSQL = strSQL + "     a.发送人  , a.接收人  , a.委托人  , a.交接说明" + vbCr
                strSQL = strSQL + "   from" + vbCr

                '获取主表记录
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select" + vbCr
                strSQL = strSQL + "      文件标识, 办理状态 = '" + strTASKSTATUS_ZJB + "', 办理子类 = '" + strTASK_DBWJ + "',"
                strSQL = strSQL + "      发送人 = 督办人, 接收人 = 被督办人, 委托人 = ' ', 交接说明 = 督办要求"
                strSQL = strSQL + "     from 公文_B_督办" + vbCr
                strSQL = strSQL + "     where 文件标识 = '" + strWJBS + "'" + vbCr                         '指定文件
                strSQL = strSQL + "     and   被督办人 = '" + Trim(strUserXM) + "'" + vbCr                 '我被督办
                strSQL = strSQL + "     and   督办日期 is not null" + vbCr
                If strOP <> "" Then                                                                        '指定日期
                    strSQL = strSQL + "       and abs(datediff(d, 督办日期, '" + Now.ToString("yyyy-MM-dd") + "')) " + strOP + " " + lngDays.ToString() + vbCr
                End If
                strSQL = strSQL + "   ) a " + vbCr
                '获取主表记录


                '获取文件信息
                strSQL = strSQL + "   left join " + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select " + vbCr
                strSQL = strSQL + "       文件标识, 流水号  , " + vbCr
                strSQL = strSQL + "       办理类型, 办理状态, 文件子类, 文件类型," + vbCr
                strSQL = strSQL + "       文件标题, 主送单位, " + vbCr
                strSQL = strSQL + "       文件字号, 紧急程度, 秘密等级," + vbCr
                strSQL = strSQL + "       机关代字, 文件年份, 文件序号," + vbCr
                strSQL = strSQL + "       主题词  , 主办单位, 拟稿人  , 拟稿日期," + vbCr
                strSQL = strSQL + "       快速收文" + vbCr
                strSQL = strSQL + "     from 公文_V_全部审批文件新 " + vbCr
                strSQL = strSQL + "     where 文件标识 = '" + strWJBS + "'" + vbCr                                            '指定文件
                If strWJLX <> "" Then
                    strSQL = strSQL + "     and   文件类型 = '" + strWJLX + "'" + vbCr                                        '工作流类型=文件具体类型
                End If
                strSQL = strSQL + "   ) b on a.文件标识 = b.文件标识 " + vbCr
                '获取文件信息


                strSQL = strSQL + "   where b.文件标识 Is Not Null " + vbCr
                strSQL = strSQL + " ) a " + vbCr
                If strWhere <> "" Then
                    strSQL = strSQL + " where " + strWhere + vbCr
                End If
                strSQL = strSQL + " group by " + vbCr
                strSQL = strSQL + "   a.文件标识, a.流水号  ," + vbCr
                strSQL = strSQL + "   a.办理类型, a.办理状态, a.办理子类, a.文件子类, a.文件类型," + vbCr
                strSQL = strSQL + "   a.文件标题, a.机关代字, a.文件年份, a.文件序号, a.主办单位," + vbCr
                strSQL = strSQL + "   a.发送人  , a.接收人  , a.委托人  , a.交接说明" + vbCr
                strSQL = strSQL + " order by a.文件年份 desc, a.机关代字, a.文件序号 desc" + vbCr
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getSQLBDWJ_TASK = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据文件类型、开始日期、结束日期计算全部事宜的文件搜索SQL
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserXM              ：当前操作人员名称
        '     strBLLX                ：办理类型
        '     strWJLX                ：文件类型-工作流类型
        '     strQSRQ                ：开始日期
        '     strZZRQ                ：结束日期
        '     strWhere               ：搜索条件
        '     strSQL                 ：(返回)文件搜索SQL
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Private Function getSQLQBSY_FILE( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByVal strBLLX As String, _
            ByVal strWJLX As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String, _
            ByVal strWhere As String, _
            ByRef strSQL As String) As Boolean

            getSQLQBSY_FILE = False
            strSQL = ""

            Try
                Dim strFileAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.FileStatusAllYWCList
                Dim strTaskAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TaskStatusAllYWCList
                Dim strLF As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhLf
                Dim strRT As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhRt

                '初始化日期
                If strQSRQ Is Nothing Then strQSRQ = ""
                strQSRQ = strQSRQ.Trim
                If strQSRQ <> "" Then
                    If strQSRQ.IndexOf(" ") < 0 Then
                        strQSRQ = strQSRQ + " 00:00:00"
                    End If
                End If
                If strZZRQ Is Nothing Then strZZRQ = ""
                strZZRQ = strZZRQ.Trim
                If strZZRQ <> "" Then
                    If strZZRQ.IndexOf(" ") < 0 Then
                        strZZRQ = strZZRQ + " 23:59:59"
                    End If
                End If

                '我的文件
                strSQL = ""
                strSQL = strSQL + " select a.*" + vbCr
                strSQL = strSQL + " from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select" + vbCr
                strSQL = strSQL + "     a.文件标识, a.流水号  ," + vbCr
                strSQL = strSQL + "     a.办理类型, a.办理状态, a.文件子类, a.文件类型," + vbCr
                strSQL = strSQL + "     a.文件标题, a.主送单位, a.文件字号, a.紧急程度, a.秘密等级," + vbCr
                strSQL = strSQL + "     a.机关代字, a.文件年份, a.文件序号," + vbCr
                strSQL = strSQL + "     a.主题词  , a.主办单位, a.拟稿人  , a.拟稿日期,a.快速收文," + vbCr
                strSQL = strSQL + "     发送日期 = max(a.发送日期)," + vbCr
                strSQL = strSQL + "     办理期限 = max(a.办理期限)," + vbCr
                strSQL = strSQL + "     完成日期 = max(a.完成日期)," + vbCr
                strSQL = strSQL + "     a.备忘提醒" + vbCr
                strSQL = strSQL + "   from" + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select" + vbCr
                strSQL = strSQL + "       a.文件标识, b.流水号  ," + vbCr
                strSQL = strSQL + "       a.办理类型, b.办理状态, b.文件子类, b.文件类型," + vbCr
                strSQL = strSQL + "       b.文件标题, b.主送单位, b.文件字号, b.紧急程度, b.秘密等级," + vbCr
                strSQL = strSQL + "       b.机关代字, b.文件年份, b.文件序号," + vbCr
                strSQL = strSQL + "       b.主题词  , b.主办单位, b.拟稿人  , b.拟稿日期," + vbCr
                strSQL = strSQL + "       a.发送日期, a.办理期限, a.完成日期, b.快速收文," + vbCr
                strSQL = strSQL + "       备忘提醒 = case when c.备忘提醒 is null then '×' else c.备忘提醒 end" + vbCr
                strSQL = strSQL + "     from" + vbCr

                '获取主表记录
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select" + vbCr
                strSQL = strSQL + "         文件标识, 办理类型," + vbCr
                strSQL = strSQL + "         发送日期 = max(发送日期)," + vbCr
                strSQL = strSQL + "         办理期限 = max(办理最后期限)," + vbCr
                strSQL = strSQL + "         完成日期 = max(完成日期)" + vbCr
                strSQL = strSQL + "       from 公文_B_交接" + vbCr
                strSQL = strSQL + "       where ((接收人 = '" + Trim(strUserXM) + "' and 交接标识 like '__1%')" + vbCr              '我收到的
                strSQL = strSQL + "       or     (发送人 = '" + Trim(strUserXM) + "' and 交接标识 like '_1%'))" + vbCr              '我送出的
                If strBLLX <> "" Then
                    strSQL = strSQL + "       and 办理类型 = '" + strBLLX + "'" + vbCr                                              '指定类型
                End If
                If strQSRQ <> "" And strZZRQ <> "" Then                                                                             '指定日期
                    strSQL = strSQL + "       and 发送日期 between '" + strQSRQ + "' and '" + strZZRQ + "' " + vbCr
                ElseIf strQSRQ <> "" Then
                    strSQL = strSQL + "       and 发送日期 >= '" + strQSRQ + "'" + vbCr
                ElseIf strZZRQ <> "" Then
                    strSQL = strSQL + "       and 发送日期 <= '" + strZZRQ + "'" + vbCr
                Else
                End If
                strSQL = strSQL + "       group by 文件标识, 办理类型" + vbCr
                strSQL = strSQL + "     ) a " + vbCr
                '获取主表记录

                '获取备忘提醒
                strSQL = strSQL + "     left join" + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select 文件标识,备忘提醒 = case when max(备忘提醒) = 1 then '√' else '×' end" + vbCr
                strSQL = strSQL + "       from 公文_B_交接" + vbCr
                strSQL = strSQL + "       where 接收人 = '" + strUserXM + "'" + vbCr
                strSQL = strSQL + "       group by 文件标识" + vbCr
                strSQL = strSQL + "     ) c on a.文件标识 = c.文件标识" + vbCr
                '获取备忘提醒


                '获取文件信息
                strSQL = strSQL + "     left join " + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select " + vbCr
                strSQL = strSQL + "         a.文件标识, a.流水号  , " + vbCr
                strSQL = strSQL + "         a.办理类型, a.办理状态, a.文件子类, a.文件类型," + vbCr
                strSQL = strSQL + "         a.文件标题, a.主送单位, " + vbCr
                strSQL = strSQL + "         a.文件字号, a.紧急程度, a.秘密等级," + vbCr
                strSQL = strSQL + "         a.机关代字, a.文件年份, a.文件序号," + vbCr
                strSQL = strSQL + "         a.主题词  , a.主办单位, a.拟稿人  , a.拟稿日期," + vbCr
                strSQL = strSQL + "         a.快速收文" + vbCr
                strSQL = strSQL + "       from 公文_V_全部审批文件新 a" + vbCr
                strSQL = strSQL + "       left join" + vbCr
                strSQL = strSQL + "       (" + vbCr
                strSQL = strSQL + "         select 文件标识" + vbCr
                strSQL = strSQL + "         from 公文_B_交接" + vbCr
                strSQL = strSQL + "         where ((接收人 = '" + Trim(strUserXM) + "' and 交接标识 like '__1%')" + vbCr              '我收到的
                strSQL = strSQL + "         or     (发送人 = '" + Trim(strUserXM) + "' and 交接标识 like '_1%'))" + vbCr              '我送出的
                If strBLLX <> "" Then
                    strSQL = strSQL + "         and 办理类型 = '" + strBLLX + "'" + vbCr                                              '指定类型
                End If
                If strQSRQ <> "" And strZZRQ <> "" Then                                                                               '指定日期
                    strSQL = strSQL + "         and 发送日期 between '" + strQSRQ + "' and '" + strZZRQ + "' " + vbCr
                ElseIf strQSRQ <> "" Then
                    strSQL = strSQL + "         and 发送日期 >= '" + strQSRQ + "'" + vbCr
                ElseIf strZZRQ <> "" Then
                    strSQL = strSQL + "         and 发送日期 <= '" + strZZRQ + "'" + vbCr
                Else
                End If
                strSQL = strSQL + "         group by 文件标识" + vbCr
                strSQL = strSQL + "       ) b on a.文件标识 = b.文件标识" + vbCr
                strSQL = strSQL + "       where b.文件标识 is not null" + vbCr
                If strWJLX <> "" Then
                    strSQL = strSQL + "       and a.文件类型 = '" + strWJLX + "'" + vbCr
                End If
                strSQL = strSQL + "     ) b on a.文件标识 = b.文件标识 " + vbCr
                '获取文件信息


                strSQL = strSQL + "     where b.文件标识 Is Not Null " + vbCr
                strSQL = strSQL + "   ) a " + vbCr
                strSQL = strSQL + "   group by " + vbCr
                strSQL = strSQL + "     a.文件标识, a.流水号  ," + vbCr
                strSQL = strSQL + "     a.办理类型, a.办理状态, a.文件子类, a.文件类型," + vbCr
                strSQL = strSQL + "     a.文件标题, a.主送单位, a.文件字号, a.紧急程度, a.秘密等级," + vbCr
                strSQL = strSQL + "     a.机关代字, a.文件年份, a.文件序号," + vbCr
                strSQL = strSQL + "     a.主题词  , a.主办单位, a.拟稿人  , a.拟稿日期, a.快速收文, a.备忘提醒" + vbCr
                strSQL = strSQL + " ) a" + vbCr
                If strWhere <> "" Then
                    strSQL = strSQL + " where " + strWhere + vbCr
                End If
                strSQL = strSQL + " order by a.发送日期 desc, a.文件年份 desc, a.机关代字, a.文件序号 desc" + vbCr
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getSQLQBSY_FILE = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据文件类型、开始日期、结束日期计算全部事宜的任务搜索SQL
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strWJBS                ：要查看的文件标识
        '     strUserXM              ：当前操作人员名称
        '     strBLLX                ：办理类型
        '     strWJLX                ：文件类型-工作流类型
        '     strQSRQ                ：开始日期
        '     strZZRQ                ：结束日期
        '     strWhere               ：搜索条件
        '     strSQL                 ：(返回)任务搜索SQL
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Private Function getSQLQBSY_TASK( _
            ByRef strErrMsg As String, _
            ByVal strWJBS As String, _
            ByVal strUserXM As String, _
            ByVal strBLLX As String, _
            ByVal strWJLX As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String, _
            ByVal strWhere As String, _
            ByRef strSQL As String) As Boolean

            getSQLQBSY_TASK = False
            strSQL = ""

            Try
                Dim strFileAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.FileStatusAllYWCList
                Dim strTaskAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TaskStatusAllYWCList
                Dim strLF As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhLf
                Dim strRT As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhRt
                Dim strGWTHCL As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_THCL
                Dim strGWSHCL As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_SHCL
                Dim strGWHFCL As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_HFCL

                '初始化日期
                If strQSRQ Is Nothing Then strQSRQ = ""
                strQSRQ = strQSRQ.Trim
                If strQSRQ <> "" Then
                    If strQSRQ.IndexOf(" ") < 0 Then
                        strQSRQ = strQSRQ + " 00:00:00"
                    End If
                End If
                If strZZRQ Is Nothing Then strZZRQ = ""
                strZZRQ = strZZRQ.Trim
                If strZZRQ <> "" Then
                    If strZZRQ.IndexOf(" ") < 0 Then
                        strZZRQ = strZZRQ + " 23:59:59"
                    End If
                End If

                '我的文件
                strSQL = ""
                strSQL = strSQL + " select * from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select" + vbCr
                strSQL = strSQL + "     a.文件标识, b.流水号  ," + vbCr
                strSQL = strSQL + "     a.办理类型, a.办理状态, a.办理子类, b.文件子类, b.文件类型," + vbCr
                strSQL = strSQL + "     b.文件标题, b.机关代字, b.文件年份, b.文件序号, b.主办单位," + vbCr
                strSQL = strSQL + "     a.发送人  , a.接收人  , a.委托人  , a.交接说明" + vbCr
                strSQL = strSQL + "   from" + vbCr

                '获取主表记录
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select" + vbCr
                strSQL = strSQL + "       文件标识, 办理类型, 办理状态," + vbCr
                strSQL = strSQL + "       办理子类 = case " + vbCr
                strSQL = strSQL + "         when 交接标识 like '___1%'    then '" + strGWTHCL + "' " + vbCr
                strSQL = strSQL + "         when 交接标识 like '____1%'   then '" + strGWSHCL + "' " + vbCr
                strSQL = strSQL + "         when 交接标识 like '______1%' then '" + strGWHFCL + "' " + vbCr
                strSQL = strSQL + "         else 办理子类 end," + vbCr
                strSQL = strSQL + "       发送人, 接收人, 委托人, 交接标识, 交接说明 " + vbCr
                strSQL = strSQL + "     from 公文_B_交接" + vbCr
                strSQL = strSQL + "     where   文件标识 = '" + strWJBS + "'" + vbCr                                     '指定文件
                strSQL = strSQL + "     and   ((接收人   = '" + Trim(strUserXM) + "' and 交接标识 like '__1%')" + vbCr   '我收到的
                strSQL = strSQL + "     or     (发送人   = '" + Trim(strUserXM) + "' and 交接标识 like '_1%'))" + vbCr   '我送出的                              '我可见
                If strBLLX <> "" Then
                    strSQL = strSQL + "     and 办理类型 = '" + strBLLX + "'" + vbCr                                     '指定类型
                End If
                If strQSRQ <> "" And strZZRQ <> "" Then                                                                  '指定日期
                    strSQL = strSQL + "     and 发送日期 between '" + strQSRQ + "' and '" + strZZRQ + "' " + vbCr
                ElseIf strQSRQ <> "" Then
                    strSQL = strSQL + "     and 发送日期 >= '" + strQSRQ + "'" + vbCr
                ElseIf strZZRQ <> "" Then
                    strSQL = strSQL + "     and 发送日期 <= '" + strZZRQ + "'" + vbCr
                Else
                End If
                strSQL = strSQL + "   ) a " + vbCr
                '获取主表记录


                '获取文件信息
                strSQL = strSQL + "   left join " + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select " + vbCr
                strSQL = strSQL + "       文件标识, 流水号  , " + vbCr
                strSQL = strSQL + "       办理类型, 办理状态, 文件子类, 文件类型," + vbCr
                strSQL = strSQL + "       文件标题, 主送单位, " + vbCr
                strSQL = strSQL + "       文件字号, 紧急程度, 秘密等级," + vbCr
                strSQL = strSQL + "       机关代字, 文件年份, 文件序号," + vbCr
                strSQL = strSQL + "       主题词  , 主办单位, 拟稿人  , 拟稿日期," + vbCr
                strSQL = strSQL + "       快速收文" + vbCr
                strSQL = strSQL + "     from 公文_V_全部审批文件新 " + vbCr
                strSQL = strSQL + "     where 文件标识 = '" + strWJBS + "'" + vbCr                                            '指定文件
                If strWJLX <> "" Then
                    strSQL = strSQL + "     and   文件类型 = '" + strWJLX + "'" + vbCr                                        '工作流类型=文件具体类型
                End If
                strSQL = strSQL + "   ) b on a.文件标识 = b.文件标识 " + vbCr
                '获取文件信息


                strSQL = strSQL + "   where b.文件标识 Is Not Null " + vbCr
                strSQL = strSQL + " ) a " + vbCr
                If strWhere <> "" Then
                    strSQL = strSQL + " where " + strWhere + vbCr
                End If
                strSQL = strSQL + " group by " + vbCr
                strSQL = strSQL + "   a.文件标识, a.流水号  ," + vbCr
                strSQL = strSQL + "   a.办理类型, a.办理状态, a.办理子类, a.文件子类, a.文件类型," + vbCr
                strSQL = strSQL + "   a.文件标题, a.机关代字, a.文件年份, a.文件序号, a.主办单位," + vbCr
                strSQL = strSQL + "   a.发送人  , a.接收人  , a.委托人  , a.交接说明" + vbCr
                strSQL = strSQL + " order by a.文件年份 desc, a.机关代字, a.文件序号 desc" + vbCr
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getSQLQBSY_TASK = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据文件类型、开始日期、结束日期计算需要备忘提醒的文件搜索SQL
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserXM              ：当前操作人员名称
        '     strBLLX                ：办理类型
        '     strWJLX                ：文件类型-工作流类型
        '     strQSRQ                ：开始日期
        '     strZZRQ                ：结束日期
        '     strWhere               ：搜索条件
        '     strSQL                 ：(返回)文件搜索SQL
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Private Function getSQLBWTX_FILE( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByVal strBLLX As String, _
            ByVal strWJLX As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String, _
            ByVal strWhere As String, _
            ByRef strSQL As String) As Boolean

            getSQLBWTX_FILE = False
            strSQL = ""

            Try
                Dim strFileAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.FileStatusAllYWCList
                Dim strTaskAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TaskStatusAllYWCList
                Dim strLF As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhLf
                Dim strRT As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhRt

                '初始化日期
                If strQSRQ Is Nothing Then strQSRQ = ""
                strQSRQ = strQSRQ.Trim
                If strQSRQ <> "" Then
                    If strQSRQ.IndexOf(" ") < 0 Then
                        strQSRQ = strQSRQ + " 00:00:00"
                    End If
                End If
                If strZZRQ Is Nothing Then strZZRQ = ""
                strZZRQ = strZZRQ.Trim
                If strZZRQ <> "" Then
                    If strZZRQ.IndexOf(" ") < 0 Then
                        strZZRQ = strZZRQ + " 23:59:59"
                    End If
                End If

                '我的文件
                strSQL = ""
                strSQL = strSQL + " select a.*" + vbCr
                strSQL = strSQL + " from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select" + vbCr
                strSQL = strSQL + "     a.文件标识, a.流水号," + vbCr
                strSQL = strSQL + "     a.办理类型, a.办理状态, a.文件子类, a.文件类型," + vbCr
                strSQL = strSQL + "     a.文件标题, a.主送单位, a.文件字号, a.紧急程度, a.秘密等级," + vbCr
                strSQL = strSQL + "     a.机关代字, a.文件年份, a.文件序号," + vbCr
                strSQL = strSQL + "     a.主题词  , a.主办单位, a.拟稿人  , a.拟稿日期,a.快速收文," + vbCr
                strSQL = strSQL + "     发送日期 = max(a.发送日期)," + vbCr
                strSQL = strSQL + "     办理期限 = max(a.办理期限)," + vbCr
                strSQL = strSQL + "     完成日期 = max(a.完成日期)," + vbCr
                strSQL = strSQL + "     a.备忘提醒" + vbCr
                strSQL = strSQL + "   from" + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select" + vbCr
                strSQL = strSQL + "       a.文件标识, b.流水号  ," + vbCr
                strSQL = strSQL + "       a.办理类型, b.办理状态, b.文件子类, b.文件类型," + vbCr
                strSQL = strSQL + "       b.文件标题, b.主送单位, b.文件字号, b.紧急程度, b.秘密等级," + vbCr
                strSQL = strSQL + "       b.机关代字, b.文件年份, b.文件序号," + vbCr
                strSQL = strSQL + "       b.主题词  , b.主办单位, b.拟稿人  , b.拟稿日期," + vbCr
                strSQL = strSQL + "       a.发送日期, a.办理期限, a.完成日期, b.快速收文," + vbCr
                strSQL = strSQL + "       备忘提醒 = case when c.备忘提醒 is null then '×' else c.备忘提醒 end" + vbCr
                strSQL = strSQL + "     from" + vbCr

                '获取主表记录
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select a.文件标识,a.办理类型,a.交接标识," + vbCr
                strSQL = strSQL + "         发送日期 = max(a.发送日期)," + vbCr
                strSQL = strSQL + "         办理期限 = max(a.办理最后期限)," + vbCr
                strSQL = strSQL + "         完成日期 = max(a.完成日期)" + vbCr
                strSQL = strSQL + "       from" + vbCr
                strSQL = strSQL + "       (" + vbCr
                strSQL = strSQL + "         select" + vbCr
                strSQL = strSQL + "           文件标识,办理类型," + vbCr
                strSQL = strSQL + "           交接标识 = case when 交接标识 like '_____1%' then '1' else '0' end," + vbCr
                strSQL = strSQL + "           发送日期," + vbCr
                strSQL = strSQL + "           办理最后期限," + vbCr
                strSQL = strSQL + "           完成日期" + vbCr
                strSQL = strSQL + "         from 公文_B_交接" + vbCr
                strSQL = strSQL + "         where 接收人 = '" + Trim(strUserXM) + "'" + vbCr                   '我要做
                strSQL = strSQL + "         and   交接标识 like '__1%'" + vbCr                                 '我可见
                strSQL = strSQL + "         and   isnull(备忘提醒,0) = 1" + vbCr                               '需要备忘提醒
                If strBLLX <> "" Then
                    strSQL = strSQL + "         and 办理类型 = '" + strBLLX + "'" + vbCr                       '指定类型
                End If
                If strQSRQ <> "" And strZZRQ <> "" Then                                                        '指定日期
                    strSQL = strSQL + "         and 发送日期 between '" + strQSRQ + "' and '" + strZZRQ + "' " + vbCr
                ElseIf strQSRQ <> "" Then
                    strSQL = strSQL + "         and 发送日期 >= '" + strQSRQ + "'" + vbCr
                ElseIf strZZRQ <> "" Then
                    strSQL = strSQL + "         and 发送日期 <= '" + strZZRQ + "'" + vbCr
                Else
                End If
                strSQL = strSQL + "       ) a" + vbCr
                strSQL = strSQL + "       group by a.文件标识,a.办理类型,a.交接标识" + vbCr
                strSQL = strSQL + "     ) a " + vbCr
                '获取主表记录

                '获取备忘提醒
                strSQL = strSQL + "     left join" + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select 文件标识,备忘提醒 = case when max(备忘提醒) = 1 then '√' else '×' end" + vbCr
                strSQL = strSQL + "       from 公文_B_交接" + vbCr
                strSQL = strSQL + "       where 接收人 = '" + strUserXM + "'" + vbCr
                strSQL = strSQL + "       group by 文件标识" + vbCr
                strSQL = strSQL + "     ) c on a.文件标识 = c.文件标识" + vbCr
                '获取备忘提醒


                '获取文件信息
                strSQL = strSQL + "     left join " + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select " + vbCr
                strSQL = strSQL + "         a.文件标识, a.流水号  , " + vbCr
                strSQL = strSQL + "         a.办理类型, a.办理状态, a.文件子类, a.文件类型," + vbCr
                strSQL = strSQL + "         a.文件标题, a.主送单位, " + vbCr
                strSQL = strSQL + "         a.文件字号, a.紧急程度, a.秘密等级," + vbCr
                strSQL = strSQL + "         a.机关代字, a.文件年份, a.文件序号," + vbCr
                strSQL = strSQL + "         a.主题词  , a.主办单位, a.拟稿人  , a.拟稿日期," + vbCr
                strSQL = strSQL + "         a.快速收文" + vbCr
                strSQL = strSQL + "       from 公文_V_全部审批文件新 a" + vbCr
                strSQL = strSQL + "       left join" + vbCr
                strSQL = strSQL + "       (" + vbCr
                strSQL = strSQL + "         select 文件标识" + vbCr
                strSQL = strSQL + "         from 公文_B_交接" + vbCr
                strSQL = strSQL + "         where 接收人 = '" + Trim(strUserXM) + "'" + vbCr                   '我要做
                strSQL = strSQL + "         and   交接标识 like '__1%'" + vbCr                                 '我可见
                strSQL = strSQL + "         and   isnull(备忘提醒,0) = 1" + vbCr                               '需要备忘提醒
                If strBLLX <> "" Then
                    strSQL = strSQL + "         and 办理类型 = '" + strBLLX + "'" + vbCr                       '指定类型
                End If
                If strQSRQ <> "" And strZZRQ <> "" Then                                                        '指定日期
                    strSQL = strSQL + "         and 发送日期 between '" + strQSRQ + "' and '" + strZZRQ + "' " + vbCr
                ElseIf strQSRQ <> "" Then
                    strSQL = strSQL + "         and 发送日期 >= '" + strQSRQ + "'" + vbCr
                ElseIf strZZRQ <> "" Then
                    strSQL = strSQL + "         and 发送日期 <= '" + strZZRQ + "'" + vbCr
                Else
                End If
                strSQL = strSQL + "         group by 文件标识" + vbCr
                strSQL = strSQL + "       ) b on a.文件标识 = b.文件标识" + vbCr
                strSQL = strSQL + "       where b.文件标识 is not null" + vbCr
                strSQL = strSQL + "       and a.办理状态 not in (" + strFileAllYWCList + ")" + vbCr                             '文件未办完
                If strWJLX <> "" Then
                    strSQL = strSQL + "       and a.文件类型 = '" + strWJLX + "'" + vbCr
                End If
                strSQL = strSQL + "     ) b on a.文件标识 = b.文件标识 " + vbCr
                '获取文件信息


                strSQL = strSQL + "     where b.文件标识 Is Not Null " + vbCr
                strSQL = strSQL + "     and (" + vbCr
                strSQL = strSQL + "       (a.交接标识 = '1')" + vbCr                                                            '通知类消息
                strSQL = strSQL + "       or " + vbCr
                strSQL = strSQL + "       (b.快速收文 =   1)" + vbCr                                                            '快速收文
                strSQL = strSQL + "       or " + vbCr
                strSQL = strSQL + "       (b.办理状态 not in (" + strFileAllYWCList + ")) " + vbCr                              '文件未办完
                strSQL = strSQL + "     ) " + vbCr
                strSQL = strSQL + "   ) a " + vbCr
                strSQL = strSQL + "   group by " + vbCr
                strSQL = strSQL + "     a.文件标识, a.流水号  ," + vbCr
                strSQL = strSQL + "     a.办理类型, a.办理状态, a.文件子类, a.文件类型," + vbCr
                strSQL = strSQL + "     a.文件标题, a.主送单位, a.文件字号, a.紧急程度, a.秘密等级," + vbCr
                strSQL = strSQL + "     a.机关代字, a.文件年份, a.文件序号," + vbCr
                strSQL = strSQL + "     a.主题词  , a.主办单位, a.拟稿人  , a.拟稿日期, a.快速收文, a.备忘提醒" + vbCr
                strSQL = strSQL + " ) a" + vbCr
                If strWhere <> "" Then
                    strSQL = strSQL + " where " + strWhere + vbCr
                End If
                strSQL = strSQL + " order by a.发送日期 desc, a.文件年份 desc, a.机关代字, a.文件序号 desc" + vbCr
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getSQLBWTX_FILE = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据文件类型、开始日期、结束日期计算备忘提醒的任务搜索SQL
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strWJBS                ：要查看的文件标识
        '     strUserXM              ：当前操作人员名称
        '     strBLLX                ：办理类型
        '     strWJLX                ：文件类型-工作流类型
        '     strQSRQ                ：开始日期
        '     strZZRQ                ：结束日期
        '     strWhere               ：搜索条件
        '     strSQL                 ：(返回)任务搜索SQL
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Private Function getSQLBWTX_TASK( _
            ByRef strErrMsg As String, _
            ByVal strWJBS As String, _
            ByVal strUserXM As String, _
            ByVal strBLLX As String, _
            ByVal strWJLX As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String, _
            ByVal strWhere As String, _
            ByRef strSQL As String) As Boolean

            getSQLBWTX_TASK = False
            strSQL = ""

            Try
                Dim strFileAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.FileStatusAllYWCList
                Dim strTaskAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TaskStatusAllYWCList
                Dim strLF As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhLf
                Dim strRT As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhRt
                Dim strGWTHCL As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_THCL
                Dim strGWSHCL As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_SHCL
                Dim strGWHFCL As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_HFCL

                '初始化日期
                If strQSRQ Is Nothing Then strQSRQ = ""
                strQSRQ = strQSRQ.Trim
                If strQSRQ <> "" Then
                    If strQSRQ.IndexOf(" ") < 0 Then
                        strQSRQ = strQSRQ + " 00:00:00"
                    End If
                End If
                If strZZRQ Is Nothing Then strZZRQ = ""
                strZZRQ = strZZRQ.Trim
                If strZZRQ <> "" Then
                    If strZZRQ.IndexOf(" ") < 0 Then
                        strZZRQ = strZZRQ + " 23:59:59"
                    End If
                End If

                '我的文件
                strSQL = ""
                strSQL = strSQL + " select * from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select" + vbCr
                strSQL = strSQL + "     a.文件标识, b.流水号  ," + vbCr
                strSQL = strSQL + "     a.办理类型, a.办理状态, a.办理子类, b.文件子类, b.文件类型," + vbCr
                strSQL = strSQL + "     b.文件标题, b.机关代字, b.文件年份, b.文件序号, b.主办单位," + vbCr
                strSQL = strSQL + "     a.发送人  , a.接收人  , a.委托人  , a.交接说明" + vbCr
                strSQL = strSQL + "   from" + vbCr

                '获取主表记录
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select" + vbCr
                strSQL = strSQL + "       文件标识, 办理类型, 办理状态," + vbCr
                strSQL = strSQL + "       办理子类 = case " + vbCr
                strSQL = strSQL + "         when 交接标识 like '___1%'    then '" + strGWTHCL + "' " + vbCr
                strSQL = strSQL + "         when 交接标识 like '____1%'   then '" + strGWSHCL + "' " + vbCr
                strSQL = strSQL + "         when 交接标识 like '______1%' then '" + strGWHFCL + "' " + vbCr
                strSQL = strSQL + "         else 办理子类 end," + vbCr
                strSQL = strSQL + "       发送人, 接收人, 委托人, 交接标识, 交接说明 " + vbCr
                strSQL = strSQL + "     from 公文_B_交接" + vbCr
                strSQL = strSQL + "     where   文件标识 = '" + strWJBS + "'" + vbCr                                     '指定文件
                strSQL = strSQL + "     and   ((接收人   = '" + Trim(strUserXM) + "' and 交接标识 like '__1%')" + vbCr   '我收到的
                strSQL = strSQL + "     or     (发送人   = '" + Trim(strUserXM) + "' and 交接标识 like '_1%'))" + vbCr   '我送出的                              '我可见
                strSQL = strSQL + "     and   isnull(备忘提醒,0) = 1" + vbCr                                             '需要备忘提醒
                If strBLLX <> "" Then
                    strSQL = strSQL + "     and 办理类型 = '" + strBLLX + "'" + vbCr                                     '指定类型
                End If
                If strQSRQ <> "" And strZZRQ <> "" Then                                                                  '指定日期
                    strSQL = strSQL + "     and 发送日期 between '" + strQSRQ + "' and '" + strZZRQ + "' " + vbCr
                ElseIf strQSRQ <> "" Then
                    strSQL = strSQL + "     and 发送日期 >= '" + strQSRQ + "'" + vbCr
                ElseIf strZZRQ <> "" Then
                    strSQL = strSQL + "     and 发送日期 <= '" + strZZRQ + "'" + vbCr
                Else
                End If
                strSQL = strSQL + "   ) a " + vbCr
                '获取主表记录


                '获取文件信息
                strSQL = strSQL + "   left join " + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select " + vbCr
                strSQL = strSQL + "       文件标识, 流水号  , " + vbCr
                strSQL = strSQL + "       办理类型, 办理状态, 文件子类, 文件类型," + vbCr
                strSQL = strSQL + "       文件标题, 主送单位, " + vbCr
                strSQL = strSQL + "       文件字号, 紧急程度, 秘密等级," + vbCr
                strSQL = strSQL + "       机关代字, 文件年份, 文件序号," + vbCr
                strSQL = strSQL + "       主题词  , 主办单位, 拟稿人  , 拟稿日期," + vbCr
                strSQL = strSQL + "       快速收文" + vbCr
                strSQL = strSQL + "     from 公文_V_全部审批文件新 " + vbCr
                strSQL = strSQL + "     where 文件标识 = '" + strWJBS + "'" + vbCr                                            '指定文件
                If strWJLX <> "" Then
                    strSQL = strSQL + "     and   文件类型 = '" + strWJLX + "'" + vbCr                                        '工作流类型=文件具体类型
                End If
                strSQL = strSQL + "   ) b on a.文件标识 = b.文件标识 " + vbCr
                '获取文件信息


                strSQL = strSQL + "   where b.文件标识 Is Not Null " + vbCr
                strSQL = strSQL + " ) a " + vbCr
                If strWhere <> "" Then
                    strSQL = strSQL + " where " + strWhere + vbCr
                End If
                strSQL = strSQL + " group by " + vbCr
                strSQL = strSQL + "   a.文件标识, a.流水号  ," + vbCr
                strSQL = strSQL + "   a.办理类型, a.办理状态, a.办理子类, a.文件子类, a.文件类型," + vbCr
                strSQL = strSQL + "   a.文件标题, a.机关代字, a.文件年份, a.文件序号, a.主办单位," + vbCr
                strSQL = strSQL + "   a.发送人  , a.接收人  , a.委托人  , a.交接说明" + vbCr
                strSQL = strSQL + " order by a.文件年份 desc, a.机关代字, a.文件序号 desc" + vbCr
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getSQLBWTX_TASK = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据文件类型、开始日期、结束日期计算指定时间后收到的文件搜索SQL
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserXM              ：当前操作人员名称
        '     strBLLX                ：办理类型
        '     strWJLX                ：文件类型-工作流类型
        '     strQSRQ                ：开始日期
        '     strZZRQ                ：结束日期
        '     strWhere               ：搜索条件
        '     strSQL                 ：(返回)文件搜索SQL
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Private Function getSQLRecv_FILE( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByVal strBLLX As String, _
            ByVal strWJLX As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String, _
            ByVal strWhere As String, _
            ByRef strSQL As String) As Boolean

            getSQLRecv_FILE = False
            strSQL = ""

            Try
                Dim strFileAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.FileStatusAllYWCList
                Dim strTaskAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TaskStatusAllYWCList
                Dim strLF As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhLf
                Dim strRT As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhRt

                '初始化日期
                If strQSRQ Is Nothing Then strQSRQ = ""
                strQSRQ = strQSRQ.Trim
                If strQSRQ <> "" Then
                    If strQSRQ.IndexOf(" ") < 0 Then
                        strQSRQ = strQSRQ + " 00:00:00"
                    End If
                End If
                If strZZRQ Is Nothing Then strZZRQ = ""
                strZZRQ = strZZRQ.Trim
                If strZZRQ <> "" Then
                    If strZZRQ.IndexOf(" ") < 0 Then
                        strZZRQ = strZZRQ + " 23:59:59"
                    End If
                End If

                '我的文件
                strSQL = ""
                strSQL = strSQL + " select a.*" + vbCr
                strSQL = strSQL + " from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select" + vbCr
                strSQL = strSQL + "     a.文件标识, a.流水号  ," + vbCr
                strSQL = strSQL + "     a.办理类型, a.办理状态, a.文件子类, a.文件类型," + vbCr
                strSQL = strSQL + "     a.文件标题, a.主送单位, a.文件字号, a.紧急程度, a.秘密等级," + vbCr
                strSQL = strSQL + "     a.机关代字, a.文件年份, a.文件序号," + vbCr
                strSQL = strSQL + "     a.主题词  , a.主办单位, a.拟稿人  , a.拟稿日期,a.快速收文," + vbCr
                strSQL = strSQL + "     发送日期 = max(a.发送日期)," + vbCr
                strSQL = strSQL + "     办理期限 = max(a.办理期限)," + vbCr
                strSQL = strSQL + "     完成日期 = max(a.完成日期)," + vbCr
                strSQL = strSQL + "     a.备忘提醒" + vbCr
                strSQL = strSQL + "   from" + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select" + vbCr
                strSQL = strSQL + "       a.文件标识, b.流水号  ," + vbCr
                strSQL = strSQL + "       a.办理类型, b.办理状态, b.文件子类, b.文件类型," + vbCr
                strSQL = strSQL + "       b.文件标题, b.主送单位, b.文件字号, b.紧急程度, b.秘密等级," + vbCr
                strSQL = strSQL + "       b.机关代字, b.文件年份, b.文件序号," + vbCr
                strSQL = strSQL + "       b.主题词  , b.主办单位, b.拟稿人  , b.拟稿日期," + vbCr
                strSQL = strSQL + "       a.发送日期, a.办理期限, a.完成日期, b.快速收文," + vbCr
                strSQL = strSQL + "       备忘提醒 = case when c.备忘提醒 is null then '×' else c.备忘提醒 end" + vbCr
                strSQL = strSQL + "     from" + vbCr

                '获取主表记录
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select a.文件标识,a.办理类型,a.交接标识," + vbCr
                strSQL = strSQL + "         发送日期 = max(a.发送日期)," + vbCr
                strSQL = strSQL + "         办理期限 = max(a.办理最后期限)," + vbCr
                strSQL = strSQL + "         完成日期 = max(a.完成日期)" + vbCr
                strSQL = strSQL + "       from" + vbCr
                strSQL = strSQL + "       (" + vbCr
                strSQL = strSQL + "         select" + vbCr
                strSQL = strSQL + "           文件标识, 办理类型," + vbCr
                strSQL = strSQL + "           交接标识 = case when 交接标识 like '_____1%' then '1' else '0' end," + vbCr
                strSQL = strSQL + "           发送日期," + vbCr
                strSQL = strSQL + "           办理最后期限," + vbCr
                strSQL = strSQL + "           完成日期" + vbCr
                strSQL = strSQL + "         from 公文_B_交接" + vbCr
                strSQL = strSQL + "         where 接收人   =    '" + Trim(strUserXM) + "'" + vbCr              '我要做
                strSQL = strSQL + "         and   交接标识 like '__1%'" + vbCr                                 '我可见
                strSQL = strSQL + "         and   办理状态 not in (" + strTaskAllYWCList + ")" + vbCr          '没有办完
                If strBLLX <> "" Then
                    strSQL = strSQL + "         and 办理类型 = '" + strBLLX + "'" + vbCr                       '指定类型
                End If
                If strQSRQ <> "" And strZZRQ <> "" Then                                                        '指定日期
                    strSQL = strSQL + "         and 发送日期 between '" + strQSRQ + "' and '" + strZZRQ + "' " + vbCr
                ElseIf strQSRQ <> "" Then
                    strSQL = strSQL + "         and 发送日期 >= '" + strQSRQ + "'" + vbCr
                ElseIf strZZRQ <> "" Then
                    strSQL = strSQL + "         and 发送日期 <= '" + strZZRQ + "'" + vbCr
                Else
                End If
                strSQL = strSQL + "       ) a" + vbCr
                strSQL = strSQL + "       group by a.文件标识,a.办理类型,a.交接标识" + vbCr
                strSQL = strSQL + "     ) a " + vbCr
                '获取主表记录

                '获取备忘提醒
                strSQL = strSQL + "     left join" + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select 文件标识,备忘提醒 = case when max(备忘提醒) = 1 then '√' else '×' end" + vbCr
                strSQL = strSQL + "       from 公文_B_交接" + vbCr
                strSQL = strSQL + "       where 接收人 = '" + strUserXM + "'" + vbCr
                strSQL = strSQL + "       group by 文件标识" + vbCr
                strSQL = strSQL + "     ) c on a.文件标识 = c.文件标识" + vbCr
                '获取备忘提醒


                '获取文件信息
                strSQL = strSQL + "     left join " + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select " + vbCr
                strSQL = strSQL + "         a.文件标识, a.流水号  , " + vbCr
                strSQL = strSQL + "         a.办理类型, a.办理状态, a.文件子类, a.文件类型," + vbCr
                strSQL = strSQL + "         a.文件标题, a.主送单位, " + vbCr
                strSQL = strSQL + "         a.文件字号, a.紧急程度, a.秘密等级," + vbCr
                strSQL = strSQL + "         a.机关代字, a.文件年份, a.文件序号," + vbCr
                strSQL = strSQL + "         a.主题词  , a.主办单位, a.拟稿人  , a.拟稿日期," + vbCr
                strSQL = strSQL + "         a.快速收文" + vbCr
                strSQL = strSQL + "       from 公文_V_全部审批文件新 a" + vbCr
                strSQL = strSQL + "       left join" + vbCr
                strSQL = strSQL + "       (" + vbCr
                strSQL = strSQL + "         select 文件标识" + vbCr
                strSQL = strSQL + "         from 公文_B_交接" + vbCr
                strSQL = strSQL + "         where 接收人   =    '" + Trim(strUserXM) + "'" + vbCr              '我要做
                strSQL = strSQL + "         and   交接标识 like '__1%'" + vbCr                                 '我可见
                strSQL = strSQL + "         and   办理状态 not in (" + strTaskAllYWCList + ")" + vbCr          '没有办完
                If strBLLX <> "" Then
                    strSQL = strSQL + "         and 办理类型 = '" + strBLLX + "'" + vbCr                       '指定类型
                End If
                If strQSRQ <> "" And strZZRQ <> "" Then                                                        '指定日期
                    strSQL = strSQL + "         and 发送日期 between '" + strQSRQ + "' and '" + strZZRQ + "' " + vbCr
                ElseIf strQSRQ <> "" Then
                    strSQL = strSQL + "         and 发送日期 >= '" + strQSRQ + "'" + vbCr
                ElseIf strZZRQ <> "" Then
                    strSQL = strSQL + "         and 发送日期 <= '" + strZZRQ + "'" + vbCr
                Else
                End If
                strSQL = strSQL + "         group by 文件标识" + vbCr
                strSQL = strSQL + "       ) b on a.文件标识 = b.文件标识" + vbCr
                strSQL = strSQL + "       where b.文件标识 is not null" + vbCr
                If strWJLX <> "" Then
                    strSQL = strSQL + "       and a.文件类型 = '" + strWJLX + "'" + vbCr
                End If
                strSQL = strSQL + "     ) b on a.文件标识 = b.文件标识 " + vbCr
                '获取文件信息


                strSQL = strSQL + "     where b.文件标识 Is Not Null " + vbCr
                strSQL = strSQL + "     and (" + vbCr
                strSQL = strSQL + "       (a.交接标识 = '1')" + vbCr                                                            '通知类消息
                strSQL = strSQL + "       or " + vbCr
                strSQL = strSQL + "       (b.快速收文 =   1)" + vbCr                                                            '快速收文
                strSQL = strSQL + "       or " + vbCr
                strSQL = strSQL + "       (b.办理状态 not in (" + strFileAllYWCList + ")) " + vbCr                              '文件未办完
                strSQL = strSQL + "     ) " + vbCr
                strSQL = strSQL + "   ) a " + vbCr
                strSQL = strSQL + "   group by " + vbCr
                strSQL = strSQL + "     a.文件标识, a.流水号  ," + vbCr
                strSQL = strSQL + "     a.办理类型, a.办理状态, a.文件子类, a.文件类型," + vbCr
                strSQL = strSQL + "     a.文件标题, a.主送单位, a.文件字号, a.紧急程度, a.秘密等级," + vbCr
                strSQL = strSQL + "     a.机关代字, a.文件年份, a.文件序号," + vbCr
                strSQL = strSQL + "     a.主题词  , a.主办单位, a.拟稿人  , a.拟稿日期, a.快速收文, a.备忘提醒" + vbCr
                strSQL = strSQL + " ) a" + vbCr
                If strWhere <> "" Then
                    strSQL = strSQL + " where " + strWhere + vbCr
                End If
                strSQL = strSQL + " order by a.发送日期 desc, a.文件年份 desc, a.机关代字, a.文件序号 desc" + vbCr
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getSQLRecv_FILE = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据当前选定的任务、搜索条件获取当前用户的要查看的文件数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strUserXM            ：用户名称
        '     objNodeData          ：当前任务节点数据行
        '     strWhere             ：当前搜索条件(a.)
        '     objFileData          ：返回要查看的文件数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getMyTaskFileData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByVal objNodeData As System.Data.DataRow, _
            ByVal strWhere As String, _
            ByRef objFileData As Xydc.Platform.Common.Data.grswMyTaskData) As Boolean

            Dim objTempFileData As Xydc.Platform.Common.Data.grswMyTaskData

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            getMyTaskFileData = False
            objFileData = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strUserXM Is Nothing Then strUserXM = ""
                If strWhere Is Nothing Then strWhere = ""
                strUserId = strUserId.Trim()
                strUserXM = strUserXM.Trim()
                strWhere = strWhere.Trim()
                If strUserId = "" Then
                    strErrMsg = "错误：未指定连接用户！"
                    GoTo errProc
                End If
                If strUserXM = "" Then
                    strErrMsg = "错误：未指定当前用户！"
                    GoTo errProc
                End If
                If objNodeData Is Nothing Then
                    strErrMsg = "错误：未选择任务！"
                    GoTo errProc
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '创建数据集
                objTempFileData = New Xydc.Platform.Common.Data.grswMyTaskData(Xydc.Platform.Common.Data.grswMyTaskData.enumTableType.GR_B_MYTASK_FILE)

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '获取任务信息
                Dim strCode As String
                Dim strWJLX As String
                Dim strBLLX As String
                Dim strQSRQ As String
                Dim strZZRQ As String
                strCode = objPulicParameters.getObjectValue(objNodeData.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_CODE), "")
                strWJLX = objPulicParameters.getObjectValue(objNodeData.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_WJLX), "")
                strBLLX = objPulicParameters.getObjectValue(objNodeData.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_BLLX), "")
                strQSRQ = objPulicParameters.getObjectValue(objNodeData.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ), "")
                strZZRQ = objPulicParameters.getObjectValue(objNodeData.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ), "")

                '执行检索
                With Me.SqlDataAdapter
                    '计算SQL
                    Dim intType As Integer = CType(strCode.Substring(0, Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(0)), Integer)
                    Select Case intType
                        Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.DBSY  '待办事宜
                            If Me.getSQLDBSY_FILE(strErrMsg, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, strWhere, strSQL) = False Then
                                GoTo errProc
                            End If

                        Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.DPWJ  '待批文件
                            If Me.getSQLDPWJ_FILE(strErrMsg, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, strWhere, strSQL) = False Then
                                GoTo errProc
                            End If

                        Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.HBWJ  '缓办文件
                            If Me.getSQLHBWJ_FILE(strErrMsg, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, strWhere, strSQL) = False Then
                                GoTo errProc
                            End If

                        Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.YBSY  '已办事宜
                            If Me.getSQLYBSY_FILE(strErrMsg, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, strWhere, strSQL) = False Then
                                GoTo errProc
                            End If

                        Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.GQSY  '过期事宜
                            If Me.getSQLGQSY_FILE(strErrMsg, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, strWhere, strSQL) = False Then
                                GoTo errProc
                            End If

                        Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.CBSY  '催办事宜
                            If Me.getSQLCBSY_FILE(strErrMsg, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, strWhere, strSQL) = False Then
                                GoTo errProc
                            End If

                        Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.BCSY  '被催事宜
                            If Me.getSQLBCSY_FILE(strErrMsg, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, strWhere, strSQL) = False Then
                                GoTo errProc
                            End If

                        Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.DBWJ  '督办文件
                            If Me.getSQLDBWJ_FILE(strErrMsg, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, strWhere, strSQL) = False Then
                                GoTo errProc
                            End If

                        Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.BDWJ  '被督文件
                            If Me.getSQLBDWJ_FILE(strErrMsg, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, strWhere, strSQL) = False Then
                                GoTo errProc
                            End If

                        Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.QBSY  '全部事宜
                            If Me.getSQLQBSY_FILE(strErrMsg, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, strWhere, strSQL) = False Then
                                GoTo errProc
                            End If

                        Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.BWTX  '备忘提醒
                            If Me.getSQLBWTX_FILE(strErrMsg, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, strWhere, strSQL) = False Then
                                GoTo errProc
                            End If

                    End Select

                    '设置参数
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    '执行操作
                    .Fill(objTempFileData.Tables(Xydc.Platform.Common.Data.grswMyTaskData.TABLE_GR_B_MYTASK_FILE))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            objFileData = objTempFileData
            getMyTaskFileData = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.grswMyTaskData.SafeRelease(objTempFileData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据当前选定的任务、搜索条件获取当前用户的要查看的任务数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWJBS              ：要查看的文件标识
        '     strUserXM            ：用户名称
        '     objNodeData          ：当前任务节点数据行
        '     strWhere             ：当前搜索条件(a.)
        '     objTaskData          ：返回要查看的任务数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getMyTaskTaskData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWJBS As String, _
            ByVal strUserXM As String, _
            ByVal objNodeData As System.Data.DataRow, _
            ByVal strWhere As String, _
            ByRef objTaskData As Xydc.Platform.Common.Data.grswMyTaskData) As Boolean

            Dim objTempTaskData As Xydc.Platform.Common.Data.grswMyTaskData

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            getMyTaskTaskData = False
            objTaskData = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strUserXM Is Nothing Then strUserXM = ""
                If strWJBS Is Nothing Then strWJBS = ""
                If strWhere Is Nothing Then strWhere = ""
                strUserId = strUserId.Trim()
                strUserXM = strUserXM.Trim()
                strWJBS = strWJBS.Trim()
                strWhere = strWhere.Trim()
                If strUserId = "" Then
                    strErrMsg = "错误：未指定连接用户！"
                    GoTo errProc
                End If
                If strUserXM = "" Then
                    strErrMsg = "错误：未指定当前用户！"
                    GoTo errProc
                End If
                If objNodeData Is Nothing Then
                    strErrMsg = "错误：未选择任务！"
                    GoTo errProc
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '创建数据集
                objTempTaskData = New Xydc.Platform.Common.Data.grswMyTaskData(Xydc.Platform.Common.Data.grswMyTaskData.enumTableType.GR_B_MYTASK_TASK)

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '获取任务信息
                Dim strCode As String
                Dim strWJLX As String
                Dim strBLLX As String
                Dim strQSRQ As String
                Dim strZZRQ As String
                strCode = objPulicParameters.getObjectValue(objNodeData.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_CODE), "")
                strWJLX = objPulicParameters.getObjectValue(objNodeData.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_WJLX), "")
                strBLLX = objPulicParameters.getObjectValue(objNodeData.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_BLLX), "")
                strQSRQ = objPulicParameters.getObjectValue(objNodeData.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ), "")
                strZZRQ = objPulicParameters.getObjectValue(objNodeData.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ), "")

                '执行检索
                With Me.SqlDataAdapter
                    '计算SQL
                    Dim intType As Integer = CType(strCode.Substring(0, Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(0)), Integer)
                    Select Case intType
                        Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.DBSY  '待办事宜
                            If Me.getSQLDBSY_TASK(strErrMsg, strWJBS, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, strWhere, strSQL) = False Then
                                GoTo errProc
                            End If

                        Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.DPWJ  '待批文件
                            If Me.getSQLDPWJ_TASK(strErrMsg, strWJBS, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, strWhere, strSQL) = False Then
                                GoTo errProc
                            End If

                        Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.HBWJ  '缓办文件
                            If Me.getSQLHBWJ_TASK(strErrMsg, strWJBS, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, strWhere, strSQL) = False Then
                                GoTo errProc
                            End If

                        Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.YBSY  '已办事宜
                            If Me.getSQLYBSY_TASK(strErrMsg, strWJBS, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, strWhere, strSQL) = False Then
                                GoTo errProc
                            End If

                        Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.GQSY  '过期事宜
                            If Me.getSQLGQSY_TASK(strErrMsg, strWJBS, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, strWhere, strSQL) = False Then
                                GoTo errProc
                            End If

                        Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.CBSY  '催办事宜
                            If Me.getSQLCBSY_TASK(strErrMsg, strWJBS, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, strWhere, strSQL) = False Then
                                GoTo errProc
                            End If

                        Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.BCSY  '被催事宜
                            If Me.getSQLBCSY_TASK(strErrMsg, strWJBS, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, strWhere, strSQL) = False Then
                                GoTo errProc
                            End If

                        Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.DBWJ  '督办文件
                            If Me.getSQLDBWJ_TASK(strErrMsg, strWJBS, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, strWhere, strSQL) = False Then
                                GoTo errProc
                            End If

                        Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.BDWJ  '被督文件
                            If Me.getSQLBDWJ_TASK(strErrMsg, strWJBS, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, strWhere, strSQL) = False Then
                                GoTo errProc
                            End If

                        Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.QBSY  '全部事宜
                            If Me.getSQLQBSY_TASK(strErrMsg, strWJBS, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, strWhere, strSQL) = False Then
                                GoTo errProc
                            End If

                        Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.BWTX  '备忘提醒
                            If Me.getSQLBWTX_TASK(strErrMsg, strWJBS, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, strWhere, strSQL) = False Then
                                GoTo errProc
                            End If

                    End Select

                    '设置参数
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    '执行操作
                    .Fill(objTempTaskData.Tables(Xydc.Platform.Common.Data.grswMyTaskData.TABLE_GR_B_MYTASK_TASK))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            objTaskData = objTempTaskData
            getMyTaskTaskData = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.grswMyTaskData.SafeRelease(objTempTaskData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取我的未办事宜数据集
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserId              ：用户标识
        '     strPassword            ：用户密码
        '     strUserXM              ：用户名称
        '     objDataSetDBSY         ：未办事宜数据集
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function getDataSetDBSY( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByRef objDataSetDBSY As Xydc.Platform.Common.Data.grswMyTaskData) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objDataSet As Xydc.Platform.Common.Data.grswMyTaskData
            Dim strSQL As String

            getDataSetDBSY = False
            objDataSetDBSY = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim()
                If strUserId = "" Then
                    strErrMsg = "错误：未指定连接用户！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()
                If strUserXM = "" Then
                    strErrMsg = "错误：未指定当前用户！"
                    GoTo errProc
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '创建数据集
                objDataSet = New Xydc.Platform.Common.Data.grswMyTaskData(Xydc.Platform.Common.Data.grswMyTaskData.enumTableType.GR_B_MYTASK_FILE)

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '获取任务信息
                Dim strWJLX As String = ""
                Dim strBLLX As String = ""
                Dim strQSRQ As String = ""
                Dim strZZRQ As String = ""

                '执行检索
                With Me.SqlDataAdapter
                    '计算SQL
                    If Me.getSQLDBSY_FILE(strErrMsg, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, "", strSQL) = False Then
                        GoTo errProc
                    End If

                    '设置参数
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    '执行操作
                    .Fill(objDataSet.Tables(Xydc.Platform.Common.Data.grswMyTaskData.TABLE_GR_B_MYTASK_FILE))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            objDataSetDBSY = objDataSet
            getDataSetDBSY = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.grswMyTaskData.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取我的已经过期文件+今天要过期数据集
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserId              ：用户标识
        '     strPassword            ：用户密码
        '     strUserXM              ：用户名称
        '     objDataSetGQSY         ：已经过期文件+今天要过期数据集
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function getDataSetGQSY( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByRef objDataSetGQSY As Xydc.Platform.Common.Data.grswMyTaskData) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objDataSet As Xydc.Platform.Common.Data.grswMyTaskData
            Dim strSQL As String

            getDataSetGQSY = False
            objDataSetGQSY = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim()
                If strUserId = "" Then
                    strErrMsg = "错误：未指定连接用户！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()
                If strUserXM = "" Then
                    strErrMsg = "错误：未指定当前用户！"
                    GoTo errProc
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '创建数据集
                objDataSet = New Xydc.Platform.Common.Data.grswMyTaskData(Xydc.Platform.Common.Data.grswMyTaskData.enumTableType.GR_B_MYTASK_FILE)

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '获取任务信息
                Dim strWJLX As String = ""
                Dim strBLLX As String = ""
                Dim strQSRQ As String = ""
                Dim strZZRQ As String = Now.ToString("yyyy-MM-dd")

                '执行检索
                With Me.SqlDataAdapter
                    '计算SQL
                    If Me.getSQLGQSY_FILE(strErrMsg, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, "", strSQL) = False Then
                        GoTo errProc
                    End If

                    '设置参数
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    '执行操作
                    .Fill(objDataSet.Tables(Xydc.Platform.Common.Data.grswMyTaskData.TABLE_GR_B_MYTASK_FILE))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            objDataSetGQSY = objDataSet
            getDataSetGQSY = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.grswMyTaskData.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取我的备忘提醒数据集
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserId              ：用户标识
        '     strPassword            ：用户密码
        '     strUserXM              ：用户名称
        '     objDataSetBWTX         ：备忘提醒数据集
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function getDataSetBWTX( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByRef objDataSetBWTX As Xydc.Platform.Common.Data.grswMyTaskData) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objDataSet As Xydc.Platform.Common.Data.grswMyTaskData
            Dim strSQL As String

            getDataSetBWTX = False
            objDataSetBWTX = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim()
                If strUserId = "" Then
                    strErrMsg = "错误：未指定连接用户！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()
                If strUserXM = "" Then
                    strErrMsg = "错误：未指定当前用户！"
                    GoTo errProc
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '创建数据集
                objDataSet = New Xydc.Platform.Common.Data.grswMyTaskData(Xydc.Platform.Common.Data.grswMyTaskData.enumTableType.GR_B_MYTASK_FILE)

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '获取任务信息
                Dim strWJLX As String = ""
                Dim strBLLX As String = ""
                Dim strQSRQ As String = ""
                Dim strZZRQ As String = ""

                '执行检索
                With Me.SqlDataAdapter
                    '计算SQL
                    If Me.getSQLBWTX_FILE(strErrMsg, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, "", strSQL) = False Then
                        GoTo errProc
                    End If

                    '设置参数
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    '执行操作
                    .Fill(objDataSet.Tables(Xydc.Platform.Common.Data.grswMyTaskData.TABLE_GR_B_MYTASK_FILE))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            objDataSetBWTX = objDataSet
            getDataSetBWTX = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.grswMyTaskData.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取我的未办事宜数目
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserId              ：用户标识
        '     strPassword            ：用户密码
        '     strUserXM              ：用户名称
        '     intCountDBSY           ：未办事宜数目
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function getCountDBSY( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByRef intCountDBSY As Integer) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            getCountDBSY = False
            intCountDBSY = 0
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim()
                If strUserId = "" Then
                    strErrMsg = "错误：未指定连接用户！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()
                If strUserXM = "" Then
                    strErrMsg = "错误：未指定当前用户！"
                    GoTo errProc
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '创建数据集
                objDataSet = New System.Data.DataSet

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '获取任务信息
                Dim strWJLX As String = ""
                Dim strBLLX As String = ""
                Dim strQSRQ As String = ""
                Dim strZZRQ As String = ""

                '执行检索
                With Me.SqlDataAdapter
                    '计算SQL
                    If Me.getSQLDBSY_FILE(strErrMsg, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, "", strSQL) = False Then
                        GoTo errProc
                    End If

                    '重建SQL
                    Dim strTempSQL As String
                    Dim intEnd As Integer
                    strSQL = strSQL.Trim
                    intEnd = strSQL.IndexOf("order by ")
                    strTempSQL = strSQL.Substring(0, intEnd)
                    strSQL = ""
                    strSQL = strSQL + " select count(*)" + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   " + strTempSQL + vbCr
                    strSQL = strSQL + " ) a" + vbCr

                    '设置参数
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    '执行操作
                    .Fill(objDataSet)
                End With

                '返回信息
                If Not (objDataSet Is Nothing) Then
                    If Not (objDataSet.Tables(0) Is Nothing) Then
                        intCountDBSY = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item(0), 0)
                    End If
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getCountDBSY = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取我的已经过期文件+今天要过期文件数目
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserId              ：用户标识
        '     strPassword            ：用户密码
        '     strUserXM              ：用户名称
        '     intCountGQSY           ：已经过期文件+今天要过期文件数目
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function getCountGQSY( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByRef intCountGQSY As Integer) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            getCountGQSY = False
            intCountGQSY = 0
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim()
                If strUserId = "" Then
                    strErrMsg = "错误：未指定连接用户！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()
                If strUserXM = "" Then
                    strErrMsg = "错误：未指定当前用户！"
                    GoTo errProc
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '创建数据集
                objDataSet = New System.Data.DataSet

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '获取任务信息
                Dim strWJLX As String = ""
                Dim strBLLX As String = ""
                Dim strQSRQ As String = ""
                Dim strZZRQ As String = Now.ToString("yyyy-MM-dd")

                '执行检索
                With Me.SqlDataAdapter
                    '计算SQL
                    If Me.getSQLGQSY_FILE(strErrMsg, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, "", strSQL) = False Then
                        GoTo errProc
                    End If

                    '重建SQL
                    Dim strTempSQL As String
                    Dim intEnd As Integer
                    strSQL = strSQL.Trim
                    intEnd = strSQL.IndexOf("order by ")
                    strTempSQL = strSQL.Substring(0, intEnd)
                    strSQL = ""
                    strSQL = strSQL + " select count(*)" + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   " + strTempSQL + vbCr
                    strSQL = strSQL + " ) a" + vbCr

                    '设置参数
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    '执行操作
                    .Fill(objDataSet)
                End With

                '返回信息
                If Not (objDataSet Is Nothing) Then
                    If Not (objDataSet.Tables(0) Is Nothing) Then
                        intCountGQSY = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item(0), 0)
                    End If
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getCountGQSY = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取我的备忘提醒文件数目
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserId              ：用户标识
        '     strPassword            ：用户密码
        '     strUserXM              ：用户名称
        '     intCountBWTX           ：备忘提醒文件数目
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function getCountBWTX( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByRef intCountBWTX As Integer) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            getCountBWTX = False
            intCountBWTX = 0
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim()
                If strUserId = "" Then
                    strErrMsg = "错误：未指定连接用户！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()
                If strUserXM = "" Then
                    strErrMsg = "错误：未指定当前用户！"
                    GoTo errProc
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '创建数据集
                objDataSet = New System.Data.DataSet

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '获取任务信息
                Dim strWJLX As String = ""
                Dim strBLLX As String = ""
                Dim strQSRQ As String = ""
                Dim strZZRQ As String = ""

                '执行检索
                With Me.SqlDataAdapter
                    '计算SQL
                    If Me.getSQLBWTX_FILE(strErrMsg, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, "", strSQL) = False Then
                        GoTo errProc
                    End If

                    '重建SQL
                    Dim strTempSQL As String
                    Dim intEnd As Integer
                    strSQL = strSQL.Trim
                    intEnd = strSQL.IndexOf("order by ")
                    strTempSQL = strSQL.Substring(0, intEnd)
                    strSQL = ""
                    strSQL = strSQL + " select count(*)" + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   " + strTempSQL + vbCr
                    strSQL = strSQL + " ) a" + vbCr

                    '设置参数
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    '执行操作
                    .Fill(objDataSet)
                End With

                '返回信息
                If Not (objDataSet Is Nothing) Then
                    If Not (objDataSet.Tables(0) Is Nothing) Then
                        intCountBWTX = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item(0), 0)
                    End If
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getCountBWTX = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取指定时间后收到的文件数目
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserId              ：用户标识
        '     strPassword            ：用户密码
        '     strUserXM              ：用户名称
        '     strZDSJ                ：指定时间(日期+时间格式)
        '     intCountRecv           ：文件数目
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function getCountRecv( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByVal strZDSJ As String, _
            ByRef intCountRecv As Integer) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            getCountRecv = False
            intCountRecv = 0
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim()
                If strUserId = "" Then
                    strErrMsg = "错误：未指定连接用户！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()
                If strUserXM = "" Then
                    strErrMsg = "错误：未指定当前用户！"
                    GoTo errProc
                End If
                If strZDSJ Is Nothing Then strZDSJ = ""
                strZDSJ = strZDSJ.Trim
                If strZDSJ = "" Then
                    strErrMsg = "错误：未指定时间！"
                    GoTo errProc
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '创建数据集
                objDataSet = New System.Data.DataSet

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '获取任务信息
                Dim strWJLX As String = ""
                Dim strBLLX As String = ""
                Dim strQSRQ As String = strZDSJ
                Dim strZZRQ As String = ""

                '执行检索
                With Me.SqlDataAdapter
                    '计算SQL
                    If Me.getSQLRecv_FILE(strErrMsg, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, "", strSQL) = False Then
                        GoTo errProc
                    End If

                    '重建SQL
                    Dim strTempSQL As String
                    Dim intEnd As Integer
                    strSQL = strSQL.Trim
                    intEnd = strSQL.IndexOf("order by ")
                    strTempSQL = strSQL.Substring(0, intEnd)
                    strSQL = ""
                    strSQL = strSQL + " select count(*)" + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   " + strTempSQL + vbCr
                    strSQL = strSQL + " ) a" + vbCr

                    '设置参数
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    '执行操作
                    .Fill(objDataSet)
                End With

                '返回信息
                If Not (objDataSet Is Nothing) Then
                    If Not (objDataSet.Tables(0) Is Nothing) Then
                        intCountRecv = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item(0), 0)
                    End If
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getCountRecv = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

    End Class

End Namespace
