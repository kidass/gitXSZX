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
    ' 类名    ：dacMyTongxinlu
    '
    ' 功能描述：
    '     提供对“我的通信录”模块涉及的数据层操作
    '----------------------------------------------------------------

    Public Class dacMyTongxinlu
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.DataAccess.dacMyTongxinlu)
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
        ' 输出数据到Excel
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objDataSet           ：要导出的数据集
        '     strExcelFile         ：导出到WEB服务器中的Excel文件路径
        '     strMacroName         ：宏名列表
        '     strMacroValue        ：宏值列表
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doExportToExcel( _
            ByRef strErrMsg As String, _
            ByVal objDataSet As System.Data.DataSet, _
            ByVal strExcelFile As String, _
            Optional ByVal strMacroName As String = "", _
            Optional ByVal strMacroValue As String = "") As Boolean

            doExportToExcel = False
            strErrMsg = ""

            Try
                With New Xydc.Platform.DataAccess.dacExcel
                    If .doExport(strErrMsg, objDataSet, strExcelFile, strMacroName, strMacroValue) = False Then
                        GoTo errProc
                    End If
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doExportToExcel = True
            Exit Function
errProc:
            Exit Function

        End Function











        '----------------------------------------------------------------
        ' 获取给定人员的通信录信息
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strSYZ               ：所有者（人员代码）
        '     strWhere             ：搜索条件
        '     objTongxinluData     ：返回数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strSYZ As String, _
            ByVal strWhere As String, _
            ByRef objTongxinluData As Xydc.Platform.Common.Data.grswMyTongxinluData) As Boolean

            Dim objTempTongxinluData As Xydc.Platform.Common.Data.grswMyTongxinluData

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            getDataSet = False
            objTongxinluData = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim()
                If strUserId = "" Then
                    strErrMsg = "错误：未指定连接用户！"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim
                If strSYZ Is Nothing Then strSYZ = ""
                strSYZ = strSYZ.Trim
                If strSYZ = "" Then
                    strErrMsg = "错误：未指定所有者！"
                    GoTo errProc
                End If
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '创建数据集
                objTempTongxinluData = New Xydc.Platform.Common.Data.grswMyTongxinluData(Xydc.Platform.Common.Data.grswMyTongxinluData.enumTableType.GR_B_TONGXINLU)

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '执行检索
                With Me.SqlDataAdapter
                    '计算SQL

                    'strSQL = ""
                    'strSQL = strSQL + " select a.* " + vbCr
                    'strSQL = strSQL + " from" + vbCr
                    'strSQL = strSQL + " (" + vbCr
                    'strSQL = strSQL + "   select a.*" + vbCr
                    'strSQL = strSQL + "   from 个人_B_通讯录 a" + vbCr
                    'strSQL = strSQL + "   where a.所有者 = @syz" + vbCr
                    'strSQL = strSQL + " ) a" + vbCr
                    strSQL = ""
                    strSQL = strSQL + " select a.* " + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select a.* " + vbCr
                    strSQL = strSQL + "   from 个人_B_通讯录 a" + vbCr
                    strSQL = strSQL + "   where a.所有者 = @syz" + vbCr
                    strSQL = strSQL + " ) a" + vbCr

                    If strWhere <> "" Then
                        strSQL = strSQL + " where " + strWhere + vbCr
                    End If
                    strSQL = strSQL + " order by a.排序,a.姓名" + vbCr

                    '设置参数
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@syz", strSYZ)
                    .SelectCommand = objSqlCommand

                    '执行操作
                    .Fill(objTempTongxinluData.Tables(Xydc.Platform.Common.Data.grswMyTongxinluData.TABLE_GR_B_TONGXINLU))
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            objTongxinluData = objTempTongxinluData
            getDataSet = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.grswMyTongxinluData.SafeRelease(objTempTongxinluData)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取给定序号的通信录信息
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     intXH                ：序号
        '     objTongxinluData     ：返回数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intXH As Integer, _
            ByRef objTongxinluData As Xydc.Platform.Common.Data.grswMyTongxinluData) As Boolean

            Dim objTempTongxinluData As Xydc.Platform.Common.Data.grswMyTongxinluData

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            getDataSet = False
            objTongxinluData = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim()
                If strUserId = "" Then
                    strErrMsg = "错误：未指定连接用户！"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '创建数据集
                objTempTongxinluData = New Xydc.Platform.Common.Data.grswMyTongxinluData(Xydc.Platform.Common.Data.grswMyTongxinluData.enumTableType.GR_B_TONGXINLU)

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '执行检索
                With Me.SqlDataAdapter
                    '计算SQL
                    strSQL = ""
                    strSQL = strSQL + " select a.* " + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select a.*" + vbCr
                    strSQL = strSQL + "   from 个人_B_通讯录 a" + vbCr
                    strSQL = strSQL + "   where a.序号 = @xh" + vbCr
                    strSQL = strSQL + " ) a" + vbCr
                    strSQL = strSQL + " order by a.排序,a.姓名" + vbCr

                    '设置参数
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@xh", intXH)
                    .SelectCommand = objSqlCommand

                    '执行操作
                    .Fill(objTempTongxinluData.Tables(Xydc.Platform.Common.Data.grswMyTongxinluData.TABLE_GR_B_TONGXINLU))
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            objTongxinluData = objTempTongxinluData
            getDataSet = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.grswMyTongxinluData.SafeRelease(objTempTongxinluData)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function






        '----------------------------------------------------------------
        ' 检查“个人_B_通讯录”的数据的合法性
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
        '     objNewData           ：(返回)新数据
        '     objenumEditType      ：编辑类型

        ' 返回
        '     True                 ：合法
        '     False                ：不合法或其他程序错误
        '----------------------------------------------------------------
        Public Function doVerify( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByRef objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            doVerify = False

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "错误：未传入新的数据！"
                    GoTo errProc
                End If
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                    Case Else
                        If objOldData Is Nothing Then
                            strErrMsg = "错误：未传入旧的数据！"
                            GoTo errProc
                        End If
                End Select
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim

                '获取表结构定义
                strSQL = "select top 0 * from 个人_B_通讯录"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, strUserId, strPassword, strSQL, "个人_B_通讯录", objDataSet) = False Then
                    GoTo errProc
                End If

                '检查数据长度
                Dim intCount As Integer = objNewData.Count
                Dim intValue As Integer
                Dim strField As String
                Dim strValue As String
                Dim intLen As Integer
                Dim i As Integer
                For i = 0 To intCount - 1 Step 1
                    strField = objNewData.GetKey(i).Trim()
                    strValue = objNewData.Item(i).Trim()
                    Select Case strField
                        Case Xydc.Platform.Common.Data.grswMyTongxinluData.FIELD_GR_B_TONGXINLU_XH
                            '自动列

                        Case Xydc.Platform.Common.Data.grswMyTongxinluData.FIELD_GR_B_TONGXINLU_PX
                            If strValue = "" Then
                                strErrMsg = "错误：[" + strField + "]没有输入！"
                                GoTo errProc
                            End If
                            If objPulicParameters.isIntegerString(strValue) = False Then
                                strErrMsg = "错误：[" + strField + "]输入无效的数字！"
                                GoTo errProc
                            End If
                            strValue = CType(strValue, Integer).ToString

                        Case Xydc.Platform.Common.Data.grswMyTongxinluData.FIELD_GR_B_TONGXINLU_SYZ, _
                            Xydc.Platform.Common.Data.grswMyTongxinluData.FIELD_GR_B_TONGXINLU_XM
                            If strValue = "" Then
                                strErrMsg = "错误：[" + strField + "]不能为空！"
                                GoTo errProc
                            End If
                            With objDataSet.Tables(0).Columns(strField)
                                intLen = objPulicParameters.getStringLength(strValue)
                                If intLen > .MaxLength Then
                                    strErrMsg = "错误：[" + strField + "]长度不能超过[" + .MaxLength.ToString() + "]，实际有[" + intLen.ToString() + "]！"
                                    GoTo errProc
                                End If
                            End With

                        Case Else
                            If strValue <> "" Then
                                With objDataSet.Tables(0).Columns(strField)
                                    intLen = objPulicParameters.getStringLength(strValue)
                                    If intLen > .MaxLength Then
                                        strErrMsg = "错误：[" + strField + "]长度不能超过[" + .MaxLength.ToString() + "]，实际有[" + intLen.ToString() + "]！"
                                        GoTo errProc
                                    End If
                                End With
                            End If
                    End Select

                    objNewData(strField) = strValue
                Next
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            doVerify = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 保存“个人_B_通讯录”的数据(现有事务)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objSqlTransaction    ：现有事务
        '     objOldData           ：旧数据
        '     objNewData           ：新数据
        '     objenumEditType      ：编辑类型
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doSave( _
            ByRef strErrMsg As String, _
            ByVal objSqlTransaction As System.Data.SqlClient.SqlTransaction, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '初始化
            doSave = False
            strErrMsg = ""

            Try
                '检查
                If objSqlTransaction Is Nothing Then
                    strErrMsg = "错误：未传入现有事务！"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "错误：未传入新的数据！"
                    GoTo errProc
                End If
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                    Case Else
                        If objOldData Is Nothing Then
                            strErrMsg = "错误：未传入旧的数据！"
                            GoTo errProc
                        End If
                End Select

                '获取连接
                objSqlConnection = objSqlTransaction.Connection

                '保存数据
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '计算SQL
                    Dim strFileds As String = ""
                    Dim strValues As String = ""
                    Dim strField As String
                    Dim intCount As Integer
                    Dim i As Integer = 0
                    Select Case objenumEditType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                            '计算更新字段列表
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.grswMyTongxinluData.FIELD_GR_B_TONGXINLU_XH
                                        '自动列
                                    Case Else
                                        If strFileds = "" Then
                                            strFileds = objNewData.GetKey(i)
                                        Else
                                            strFileds = strFileds + "," + objNewData.GetKey(i)
                                        End If
                                        If strValues = "" Then
                                            strValues = "@A" + i.ToString()
                                        Else
                                            strValues = strValues + "," + "@A" + i.ToString()
                                        End If
                                End Select
                            Next
                            '准备SQL
                            strSQL = ""
                            strSQL = strSQL + " insert into 个人_B_通讯录 (" + strFileds + ")"
                            strSQL = strSQL + " values (" + strValues + ")"
                            '准备参数
                            objSqlCommand.Parameters.Clear()
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.grswMyTongxinluData.FIELD_GR_B_TONGXINLU_XH
                                        '自动列
                                    Case Xydc.Platform.Common.Data.grswMyTongxinluData.FIELD_GR_B_TONGXINLU_PX
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), 0)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(objNewData.Item(i), System.Int32))
                                        End If
                                    Case Else
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), " ")
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), objNewData.Item(i))
                                        End If
                                End Select
                            Next
                            '执行SQL
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()

                        Case Else
                            '获取原“序号”
                            Dim intOldXH As Integer
                            intOldXH = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.grswMyTongxinluData.FIELD_GR_B_TONGXINLU_XH), 0)
                            '计算更新字段列表
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.grswMyTongxinluData.FIELD_GR_B_TONGXINLU_XH
                                        '自动列
                                    Case Else
                                        If strFileds = "" Then
                                            strFileds = objNewData.GetKey(i) + " = @A" + i.ToString()
                                        Else
                                            strFileds = strFileds + "," + objNewData.GetKey(i) + " = @A" + i.ToString()
                                        End If
                                End Select
                            Next
                            '准备SQL
                            strSQL = ""
                            strSQL = strSQL + " update 个人_B_通讯录 set " + vbCr
                            strSQL = strSQL + "   " + strFileds + vbCr
                            strSQL = strSQL + " where 序号 = @oldxh" + vbCr
                            '准备参数
                            objSqlCommand.Parameters.Clear()
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.grswMyTongxinluData.FIELD_GR_B_TONGXINLU_XH
                                        '自动列
                                    Case Xydc.Platform.Common.Data.grswMyTongxinluData.FIELD_GR_B_TONGXINLU_PX
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), 0)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(objNewData.Item(i), System.Int32))
                                        End If
                                    Case Else
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), " ")
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), objNewData.Item(i))
                                        End If
                                End Select
                            Next
                            objSqlCommand.Parameters.AddWithValue("@oldxh", intOldXH)
                            '执行SQL
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()
                    End Select

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doSave = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 保存个人通信录数据记录(整个事务完成)
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserId              ：用户标识
        '     strPassword            ：用户密码
        '     objNewData             ：记录新值(返回保存后的新值)
        '     objOldData             ：记录旧值
        '     objenumEditType        ：编辑类型
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function doSave( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim strSQL As String

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            doSave = False

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "错误：未传入连接用户！"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "错误：没有指定要保存的数据！"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim

                '检查主记录
                If Me.doVerify(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType) = False Then
                    GoTo errProc
                End If

                '获取连接事务
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '开始事务
                objSqlTransaction = objSqlConnection.BeginTransaction

                '执行事务
                Try
                    '保存主记录
                    If Me.doSave(strErrMsg, objSqlTransaction, objOldData, objNewData, objenumEditType) = False Then
                        GoTo rollDatabase
                    End If

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo rollDatabase
                End Try

                '提交事务
                objSqlTransaction.Commit()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            doSave = True
            Exit Function

rollDatabase:
            objSqlTransaction.Rollback()
            GoTo errProc

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 删除“个人_B_通讯录”的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     intXH                ：序号
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDelete( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intXH As Integer) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '初始化
            doDelete = False
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '开始事务
                Try
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '删除数据
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '计算SQL
                    strSQL = ""
                    strSQL = strSQL + " delete from 个人_B_通讯录 " + vbCr
                    strSQL = strSQL + " where 序号 = @xh" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@xh", intXH)

                    '执行SQL
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '提交事务
                objSqlTransaction.Commit()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doDelete = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function






        '----------------------------------------------------------------
        ' 获取新的排序号
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strSYZ               ：所有者
        '     intPXH               ：新排序号
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getNewPXH( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strSYZ As String, _
            ByRef intPXH As Integer) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '初始化
            getNewPXH = False
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If strSYZ Is Nothing Then strSYZ = ""
                strSYZ = strSYZ.Trim
                If strSYZ = "" Then
                    strErrMsg = "错误：未指定所有者！"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '计算
                Dim strNewXH As String
                If objdacCommon.getNewCode(strErrMsg, objSqlConnection, "排序", "所有者", strSYZ, "个人_B_通讯录", True, strNewXH) = False Then
                    GoTo errProc
                End If
                intPXH = objPulicParameters.getObjectValue(strNewXH, 0)

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            getNewPXH = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

    End Class

End Namespace
