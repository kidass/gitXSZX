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
    ' 类名    ：dacSunshine
    '
    ' 功能描述：
    '     提供对楼盘匹配数据相关的数据层操作    
    '----------------------------------------------------------------

    Public Class dacSunshine
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.DataAccess.dacSunshine)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub










        '----------------------------------------------------------------
        ' 将objDataTable中的选定列导出到Excel的当前活动Sheet并自动写标题行
        '     strErrMsg              ：如果错误，则返回错误信息
        '     objDataTable           ：要导出的数据表
        '     objFields              ：要导出的列
        '     strExcelFile           ：导出到WEB服务器中的Excel文件路径
        '     strDateFormat          ：日期格式字符串
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        ' 更改
        '----------------------------------------------------------------
        Public Function doExportToExcel( _
            ByRef strErrMsg As String, _
            ByVal objDataTable As System.Data.DataTable, _
            ByVal objFields As System.Collections.Specialized.NameValueCollection, _
            ByVal strExcelFile As String, _
            ByVal strDateFormat As String) As Boolean
            With New Xydc.Platform.DataAccess.dacExcel
                'doExportToExcel = .doExport(strErrMsg, objDataTable, objFields, strExcelFile, strDateFormat)
            End With
        End Function

        '----------------------------------------------------------------
        ' 将数据从DataSet导出到Excel
        '     strErrMsg              ：如果错误，则返回错误信息
        '     objDataTable           ：要导出的数据
        '     strExcelFile           ：导出到WEB服务器中的Excel文件路径
        '     strSheetName           ：数据导出到strSheetName
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function doExportToExcel( _
            ByRef strErrMsg As String, _
            ByVal objDataTable As System.Data.DataTable, _
            ByVal strExcelFile As String, _
            ByVal strSheetName As String) As Boolean
            With New Xydc.Platform.DataAccess.dacExcel
                doExportToExcel = .doExport(strErrMsg, objDataTable, strExcelFile, strSheetName)
            End With
        End Function

        '----------------------------------------------------------------
        ' 将数据从DataSet导出到Excel
        '     strErrMsg              ：如果错误，则返回错误信息
        '     objDataSet             ：要导出的数据集
        '     strExcelFile           ：导出到WEB服务器中的Excel文件路径
        '     strMacroName           ：宏名列表
        '     strMacroValue          ：宏值列表
        '     strDateFormat          ：日期格式字符串
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function doExportToExcel( _
            ByRef strErrMsg As String, _
            ByVal objDataSet As System.Data.DataSet, _
            ByVal strExcelFile As String, _
            Optional ByVal strMacroName As String = "", _
            Optional ByVal strMacroValue As String = "", _
            Optional ByVal strDateFormat As String = "") As Boolean
            With New Xydc.Platform.DataAccess.dacExcel
                doExportToExcel = .doExport(strErrMsg, objDataSet, strExcelFile, strMacroName, strMacroValue)
               
            End With
        End Function

        '----------------------------------------------------------------
        ' 将数据从DataSet导出到Excel
        '     strErrMsg              ：如果错误，则返回错误信息
        '     objDataSet             ：要导出的数据集
        '     strExcelFile           ：导出到WEB服务器中的Excel文件路径
        '     strColorFieldName      ：用来确定行颜色的字段名
        '     objColors              ：字段值对应的颜色集合
        '     strMacroName           ：宏名列表
        '     strMacroValue          ：宏值列表
        '     strDateFormat          ：日期格式字符串
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function doExportToExcel( _
            ByRef strErrMsg As String, _
            ByVal objDataSet As System.Data.DataSet, _
            ByVal strExcelFile As String, _
            ByVal strColorFieldName As String, _
            ByVal objColors As System.Collections.Specialized.ListDictionary, _
            Optional ByVal strMacroName As String = "", _
            Optional ByVal strMacroValue As String = "", _
            Optional ByVal strDateFormat As String = "") As Boolean
            With New Xydc.Platform.DataAccess.dacExcel
                'doExportToExcel = .doExport(strErrMsg, objDataSet, strExcelFile, strColorFieldName, objColors, strMacroName, strMacroValue, strDateFormat)
            End With
        End Function

        '----------------------------------------------------------------
        ' 将strSrcFile中的strSrcSheet复制到strDesFile中的strDesSheet
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strSrcFile             ：源文件的完整路径
        '     strSrcSheet            ：源文件的sheet名
        '     strDesFile             ：目标文件的完整路径
        '     strDesSheet            ：目标文件的sheet名
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        ' 更改描述
        '----------------------------------------------------------------
        Public Function doExcelAddCopy( _
            ByRef strErrMsg As String, _
            ByVal strSrcFile As String, _
            ByVal strSrcSheet As String, _
            ByVal strDesFile As String, _
            ByVal strDesSheet As String) As Boolean
            With New Xydc.Platform.DataAccess.dacExcel
                'doExcelAddCopy = .doSheetAddCopy(strErrMsg, strSrcFile, strSrcSheet, strDesFile, strDesSheet)
            End With
        End Function

        '----------------------------------------------------------------
        ' 删除strSrcFile中的strSrcSheet
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strSrcFile             ：源文件的完整路径
        '     strSrcSheet            ：源文件的sheet名
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        ' 更改描述
        '----------------------------------------------------------------
        Public Function doExcelSheetDelete( _
            ByRef strErrMsg As String, _
            ByVal strSrcFile As String, _
            ByVal strSrcSheet As String) As Boolean
            With New Xydc.Platform.DataAccess.dacExcel
                'doExcelSheetDelete = .doSheetDelete(strErrMsg, strSrcFile, strSrcSheet)
            End With
        End Function











        '----------------------------------------------------------------
        ' 获取楼盘匹配的数据集(以代码升序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWhere             ：搜索字符串
        '     objSunshineData          ：信息数据集 
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getHouseMatch( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objSunshineData As Xydc.Platform.Common.Data.SunshineData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempSunshineData As Xydc.Platform.Common.Data.SunshineData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getHouseMatch = False
            objSunshineData = Nothing
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strWhere.Length > 0 Then strWhere = strWhere.Trim()

                '检查
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                Dim strSQL As String
                Try

                    '创建数据集
                    objTempSunshineData = New Xydc.Platform.Common.Data.SunshineData(Xydc.Platform.Common.Data.SunshineData.enumTableType.Sunshine_B_HOUSE_MATCH)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from " + vbCr
                        strSQL = strSQL + "  T_HOUSE_MATCH_XMID a "
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.C_ID desc" + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand
                        .Fill(objTempSunshineData.Tables(Xydc.Platform.Common.Data.SunshineData.TABLE_Sunshine_B_HOUSE_MATCH))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempSunshineData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempSunshineData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objSunshineData = objTempSunshineData
            getHouseMatch = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempSunshineData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function
        End Function



        '----------------------------------------------------------------
        ' 获取楼盘匹配的数据集(以代码升序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWhere             ：搜索字符串
        '     objSunshineData          ：信息数据集 
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getSunshineHouseMatch( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objSunshineData As Xydc.Platform.Common.Data.SunshineData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempSunshineData As Xydc.Platform.Common.Data.SunshineData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getSunshineHouseMatch = False
            objSunshineData = Nothing
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strWhere.Length > 0 Then strWhere = strWhere.Trim()

                '检查
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                Dim strSQL As String
                Try

                    '创建数据集
                    objTempSunshineData = New Xydc.Platform.Common.Data.SunshineData(Xydc.Platform.Common.Data.SunshineData.enumTableType.Sunshine_B_HOUSE_MATCH)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from " + vbCr
                        strSQL = strSQL + " ("

                        strSQL = strSQL + " select   distinct a.C_XZQH as 'C_XZQY',a.C_XM_NAME from T_HOUSE_INFO a"
                        strSQL = strSQL + " where not exists (select * from T_HOUSE_MATCH b where b.C_XM_NAME = a.C_XM_NAME and a.C_XZQH=B.C_XZQY)"
                        strSQL = strSQL + " and a.C_TIME > =(select convert(varchar(10),max(C_TIME),120) from T_HOUSE_INFO) "
                        strSQL = strSQL + " ) a " + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        'strSQL = strSQL + " order by a.C_XZQY " + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand
                        .Fill(objTempSunshineData.Tables(Xydc.Platform.Common.Data.SunshineData.TABLE_Sunshine_B_HOUSE_MATCH))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempSunshineData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempSunshineData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objSunshineData = objTempSunshineData
            getSunshineHouseMatch = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempSunshineData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function
        End Function


        '----------------------------------------------------------------
        ' 保存楼盘匹配的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
        '     objNewData           ：新数据
        '     objenumEditType      ：编辑类型
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doSaveSunshineHouseMatch( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim intID As Integer

            '初始化
            doSaveSunshineHouseMatch = False
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""

                '检查
                If strUserId.Trim = "" Then
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
                        intID = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.SunshineData.FIELD_Sunshine_B_HOUSE_MATCH_ID), 0)
                        '计算SQL
                End Select

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

                '保存数据
                Dim strSQL As String
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '计算SQL
                    Dim strFields As String
                    Dim strValues As String
                    Dim intCount As Integer
                    Dim strValue As String
                    Dim i As Integer



                    intCount = objNewData.Count
                    'strID = objNewData.Item(Xydc.Platform.Common.Data.SunshineData.FIELD_Sunshine_B_HOUSE_MATCH_ID)
                   
                    Select Case objenumEditType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                            strFields = ""
                            strValues = ""
                            For i = 0 To intCount - 1 Step 1
                                If strFields = "" Then
                                    strFields = objNewData.GetKey(i)
                                Else
                                    strFields = strFields + "," + objNewData.GetKey(i)
                                End If
                                If strValues = "" Then
                                    strValues = "@A" + i.ToString()
                                Else
                                    strValues = strValues + "," + "@A" + i.ToString()
                                End If
                            Next

                            strSQL = ""
                            strSQL = strSQL + " insert into T_HOUSE_MATCH (" + strFields + ")"
                            strSQL = strSQL + " values (" + strValues + ")"
                            objSqlCommand.Parameters.Clear()
                            For i = 0 To intCount - 1 Step 1
                                strValue = objNewData.Item(i).Trim()
                                Select Case objNewData.GetKey(i)
                                    Case Else
                                        If strValue = "" Then strValue = " "
                                        objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), strValue)
                                End Select
                            Next
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()

                        Case Else
                            strFields = ""
                            For i = 0 To intCount - 1 Step 1
                                If strFields = "" Then
                                    strFields = objNewData.GetKey(i) + " = @A" + i.ToString()
                                Else
                                    strFields = strFields + "," + objNewData.GetKey(i) + " = @A" + i.ToString()
                                End If
                            Next
                            strSQL = ""
                            strSQL = strSQL + " update T_HOUSE_MATCH  set "
                            strSQL = strSQL + " " + strFields + " "
                            strSQL = strSQL + " where C_ID = @C_ID"

                            objSqlCommand.Parameters.Clear()
                            For i = 0 To intCount - 1 Step 1
                                strValue = objNewData.Item(i).Trim()
                                Select Case objNewData.GetKey(i)

                                    Case Else
                                        If strValue = "" Then strValue = " "
                                        objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), strValue)
                                End Select
                            Next
                            objSqlCommand.Parameters.AddWithValue("@C_ID", intID)
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()
                    End Select

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

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doSaveSunshineHouseMatch = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function
        End Function

        '----------------------------------------------------------------
        ' 删除楼盘匹配的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteSunshineHouseMatch( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            doDeleteSunshineHouseMatch = False
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""

                '检查
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If objOldData Is Nothing Then
                    strErrMsg = "错误：未传入旧的数据！"
                    GoTo errProc
                End If

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
                Dim strSQL As String
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '计算SQL
                    Dim strOldDM As String
                    strSQL = ""

                    With New Xydc.Platform.Common.Utilities.PulicParameters
                        strOldDM = .getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.SunshineData.FIELD_Sunshine_B_HOUSE_MATCH_ID), "")
                    End With
                    strSQL = strSQL + " delete from T_HOUSE_MATCH"
                    strSQL = strSQL + " where C_ID = @C_ID"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@C_ID", strOldDM)

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

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doDeleteSunshineHouseMatch = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function


        '----------------------------------------------------------------
        ' 生成单个日楼盘数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doExecHouseDataProcedure( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef strHouse As String) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            doExecHouseDataProcedure = False
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""

                '检查
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If

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

                '执行“生成单个日楼盘数据”存储过程
                Dim strSQL As String
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '计算SQL
                    strSQL = ""

                    strSQL = strSQL + " exec dbo.Sunshine_P_Day_SingleCompute_HouseDetail @House "
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@House", strHouse)


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

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doExecHouseDataProcedure = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function



        '----------------------------------------------------------------
        ' 删除单个日楼盘数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteHouseDataProcedure( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef strHouse As String) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            doDeleteHouseDataProcedure = False
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""

                '检查
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If

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

                '执行“删除单个日楼盘数据”存储过程
                Dim strSQL As String
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '计算SQL
                    strSQL = ""

                    strSQL = strSQL + " exec dbo.Sunshine_P_Day_SingleDelete_HouseDetail @House "
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@House", strHouse)

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

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doDeleteHouseDataProcedure = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function



        '----------------------------------------------------------------
        ' 生成日楼盘数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doExecProcedureHouseData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            doExecProcedureHouseData = False
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""

                '检查
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If

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

                '执行“生成日楼盘数据”存储过程
                Dim strSQL As String
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '计算SQL
                    strSQL = ""

                    strSQL = strSQL + " exec dbo.Sunshine_P_Day_AllCompute_HouseDetail "
                    objSqlCommand.Parameters.Clear()

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

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doExecProcedureHouseData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function





        '----------------------------------------------------------------
        ' 根据“查询条件”获取“阳光家缘楼盘信息数据分析”完全数据的数据集
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strSQL                     ：返回的SQL语句
        '     strType                    ：选择楼盘的类型。0-全部；1-商业；2-住宅；3-办公
        '     strWhere                   ：搜索字符串      
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getSql_BuildingCompute( _
             ByRef strErrMsg As String, _
             ByRef strSQL As String, _
             ByVal strWhere As String, _
             ByRef strType As String) As Boolean

            getSql_BuildingCompute = False

            strSQL = ""
            Select Case strType
                Case "0"
                    strSQL = strSQL + " select * from ("
                    strSQL = strSQL + " select "
                    strSQL = strSQL + " A.行政区域 as 行政区域, "
                    strSQL = strSQL + " A.楼盘名称 as 楼盘名称, "
                    strSQL = strSQL + " A.项目类型 as 项目类型, "
                    strSQL = strSQL + " sum(A.签约套数) as 网签数, "
                    strSQL = strSQL + "  case when sum(A.签约套数) = 0 then 0 else cast(round(sum(A.签约总额)/sum(A.签约面积),2) as numeric(16,2)) end as 网签均价, "
                    strSQL = strSQL + "  sum(A.累计已售套数2) as 合共成交, "
                    strSQL = strSQL + "  sum(A.未售套数2) as 未售套数, "
                    strSQL = strSQL + "  cast(round(sum(A.签约总额),2) as numeric(16,2)) as 网签总额, "
                    strSQL = strSQL + "  cast(round(sum(A.签约面积),2) as numeric(16,2)) as 网签面积 "
                    strSQL = strSQL + "  from( "
                    strSQL = strSQL + "  select "
                    strSQL = strSQL + " b.C_ID as 序号,  "
                    strSQL = strSQL + " b.C_XZQH as 行政区域,   "
                    strSQL = strSQL + " b.C_HOUSE as 楼盘名称,  "
                    strSQL = strSQL + " b.C_XM_NAME as 项目名称,  "
                    strSQL = strSQL + " b.C_XM_ID as 预售证,    "
                    strSQL = strSQL + " '住宅' as 项目类型, "
                    strSQL = strSQL + " b.C_ZZ_YSTS_LJ as 累计预售套数1,  "
                    strSQL = strSQL + " b.C_ZZ_YSMJ_LJ as 累计预售面积1, "
                    strSQL = strSQL + " b.C_ZZ_JJ_LJ as 累计均价1,  "
                    strSQL = strSQL + " b.C_ZZ_TS_WS as 未售套数1,  "
                    strSQL = strSQL + " b.C_ZZ_MJ_WS as 未售面积1,  "
                    strSQL = strSQL + " b.C_ZZ_YSTS_LJ as 累计已售套数2,   "
                    strSQL = strSQL + " b.C_ZZ_YSMJ_LJ as 累计已售面积2,  "
                    strSQL = strSQL + " b.C_ZZ_JJ_LJ as 累计均价2, "
                    strSQL = strSQL + " b.C_ZZ_TS_WS as 未售套数2,   "
                    strSQL = strSQL + " b.C_ZZ_MJ_WS as 未售面积2,  "
                    strSQL = strSQL + " 签约套数=case 	when a.C_ZZ_YSTS_LJ is null then b.C_ZZ_YSTS_LJ"
                    strSQL = strSQL + "   		when a.C_ZZ_YSTS_LJ=0 then  b.C_ZZ_YSTS_LJ"
                    strSQL = strSQL + " 		else   b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ  end , "
                    strSQL = strSQL + "  签约面积=case when a.C_ZZ_YSMJ_LJ is null or a.C_ZZ_YSMJ_LJ=0 then b.C_ZZ_YSMJ_LJ"
                    strSQL = strSQL + "   		when b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else    b.C_ZZ_YSMJ_LJ-a.C_ZZ_YSMJ_LJ  end ,"
                    strSQL = strSQL + " 套数核对=case when a.C_ZZ_YSTS_LJ is null or a.C_ZZ_TS_WS is null or a.C_ZZ_YSTS_LJ=0 or a.C_ZZ_TS_WS=0  then b.C_ZZ_YSTS_LJ+b.C_ZZ_TS_WS"
                    strSQL = strSQL + "   		when b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else b.C_ZZ_YSTS_LJ+b.C_ZZ_TS_WS-a.C_ZZ_YSTS_LJ-a.C_ZZ_TS_WS  end ,"
                    strSQL = strSQL + "  面积核对=case when a.C_ZZ_YSMJ_LJ is null or a.C_ZZ_MJ_WS is null or a.C_ZZ_YSMJ_LJ=0 or a.C_ZZ_MJ_WS=0 then b.C_ZZ_YSMJ_LJ+b.C_ZZ_MJ_WS"
                    strSQL = strSQL + "   		when b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else  b.C_ZZ_YSMJ_LJ+b.C_ZZ_MJ_WS-a.C_ZZ_YSMJ_LJ-a.C_ZZ_MJ_WS  end ,"
                    strSQL = strSQL + "  签约总额=case when a.C_ZZ_YSMJ_LJ is null or a.C_ZZ_JJ_LJ is null  then b.C_ZZ_YSMJ_LJ*b.C_ZZ_JJ_LJ"
                    strSQL = strSQL + "   		when a.C_ZZ_YSMJ_LJ=0 or a.C_ZZ_JJ_LJ=0  then b.C_ZZ_YSMJ_LJ*b.C_ZZ_JJ_LJ"
                    strSQL = strSQL + " 		when b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else   b.C_ZZ_YSMJ_LJ*b.C_ZZ_JJ_LJ-a.C_ZZ_YSMJ_LJ*a.C_ZZ_JJ_LJ  end , "
                    strSQL = strSQL + "  case when b.C_ZZ_YSMJ_LJ-a.C_ZZ_YSMJ_LJ=0 or a.C_ZZ_YSMJ_LJ=0 or a.C_ZZ_YSMJ_LJ is null then 0 "
                    strSQL = strSQL + " else (b.C_ZZ_YSMJ_LJ*b.C_ZZ_JJ_LJ-a.C_ZZ_YSMJ_LJ*a.C_ZZ_JJ_LJ)/(b.C_ZZ_YSMJ_LJ-a.C_ZZ_YSMJ_LJ) end as 签约均价,"
                    strSQL = strSQL + "    b.C_ZZ_YSMJ_LJ * b.C_ZZ_JJ_LJ as 累计签约总额   "
                    strSQL = strSQL + "  from "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select b.*,c.c_house from T_HOUSE_INFO b left join T_HOUSE_MATCH c on b.C_XM_NAME=c.C_XM_NAME and b.C_XZQH=c.C_XZQY  "
                    strSQL = strSQL + "  where b.C_TIME  between @endDate_start and @endDate_end "
                    strSQL = strSQL + "  )b"
                    strSQL = strSQL + "  Left Join "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select a.*,c.c_house from T_HOUSE_INFO a left join T_HOUSE_MATCH c on a.C_XM_NAME=c.C_XM_NAME and a.C_XZQH=c.C_XZQY "
                    strSQL = strSQL + "  where a.C_TIME between @startDate_start and @startDate_end "
                    strSQL = strSQL + "  )a  on  a.C_XM_ID=b.C_XM_ID and a.C_XM_NAME=b.C_XM_NAME and a.C_XZQH=b.C_XZQH and a.C_XM_ADDRESS=b.C_XM_ADDRESS"
                    strSQL = strSQL + "   ) A "
                    If strWhere <> "" Then
                        strSQL = strSQL + "where " + strWhere
                    End If
                    strSQL = strSQL + " group by A.行政区域, A.楼盘名称, A.项目类型 "
                    strSQL = strSQL + " union "
                    strSQL = strSQL + " select "
                    strSQL = strSQL + " A.行政区域 as 行政区域, "
                    strSQL = strSQL + " A.楼盘名称 as 楼盘名称, "
                    strSQL = strSQL + " A.项目类型 as 项目类型, "
                    strSQL = strSQL + " sum(A.签约套数) as 网签数, "
                    strSQL = strSQL + " case when sum(A.签约套数) = 0 then 0 else cast(round(sum(A.签约总额)/sum(A.签约面积),2) as numeric(16,2)) end as 网签均价, "
                    strSQL = strSQL + " sum(A.累计已售套数2) as 合共成交, "
                    strSQL = strSQL + " sum(A.未售套数2) as 未售套数, "
                    strSQL = strSQL + " cast(round(sum(A.签约总额),2) as numeric(16,2)) as 网签总额, "
                    strSQL = strSQL + " cast(round(sum(A.签约面积),2) as numeric(16,2)) as 网签面积 "
                    strSQL = strSQL + " from( "
                    strSQL = strSQL + "  select "
                    strSQL = strSQL + " b.C_ID as 序号, "
                    strSQL = strSQL + " b.C_XZQH as 行政区域,   "
                    strSQL = strSQL + " b.C_HOUSE as 楼盘名称,  "
                    strSQL = strSQL + " b.C_XM_NAME as 项目名称,  "
                    strSQL = strSQL + " b.C_XM_ID as 预售证,    "
                    strSQL = strSQL + " '办公' as 项目类型, "
                    strSQL = strSQL + " b.C_BG_YSTS_LJ as 累计预售套数1,  "
                    strSQL = strSQL + " b.C_BG_YSMJ_LJ as 累计预售面积1, "
                    strSQL = strSQL + " b.C_BG_JJ_LJ as 累计均价1,  "
                    strSQL = strSQL + " b.C_BG_TS_WS as 未售套数1,  "
                    strSQL = strSQL + " b.C_BG_MJ_WS as 未售面积1,  "
                    strSQL = strSQL + " b.C_BG_YSTS_LJ as 累计已售套数2,   "
                    strSQL = strSQL + " b.C_BG_YSMJ_LJ as 累计已售面积2,  "
                    strSQL = strSQL + " b.C_BG_JJ_LJ as 累计均价2, "
                    strSQL = strSQL + " b.C_BG_TS_WS as 未售套数2,   "
                    strSQL = strSQL + " b.C_BG_MJ_WS as 未售面积2,  "
                    strSQL = strSQL + " 签约套数=case 	when a.C_BG_YSTS_LJ is null then b.C_BG_YSTS_LJ"
                    strSQL = strSQL + "   		when a.C_BG_YSTS_LJ=0 then  b.C_BG_YSTS_LJ"
                    strSQL = strSQL + " 		else   b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ  end , "
                    strSQL = strSQL + "  签约面积=case when a.C_BG_YSMJ_LJ is null or a.C_BG_YSMJ_LJ=0 then b.C_BG_YSMJ_LJ"
                    strSQL = strSQL + "   		when b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else    b.C_BG_YSMJ_LJ-a.C_BG_YSMJ_LJ  end ,"
                    strSQL = strSQL + " 套数核对=case when a.C_BG_YSTS_LJ is null or a.C_BG_TS_WS is null or a.C_BG_YSTS_LJ=0 or a.C_BG_TS_WS=0  then b.C_BG_YSTS_LJ+b.C_BG_TS_WS"
                    strSQL = strSQL + "   		when b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else b.C_BG_YSTS_LJ+b.C_BG_TS_WS-a.C_BG_YSTS_LJ-a.C_BG_TS_WS  end ,"
                    strSQL = strSQL + "  面积核对=case when a.C_BG_YSMJ_LJ is null or a.C_BG_MJ_WS is null or a.C_BG_YSMJ_LJ=0 or a.C_BG_MJ_WS=0 then b.C_BG_YSMJ_LJ+b.C_BG_MJ_WS"
                    strSQL = strSQL + "   		when b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else  b.C_BG_YSMJ_LJ+b.C_BG_MJ_WS-a.C_BG_YSMJ_LJ-a.C_BG_MJ_WS  end ,"
                    strSQL = strSQL + "  签约总额=case when a.C_BG_YSMJ_LJ is null or a.C_BG_JJ_LJ is null  then b.C_BG_YSMJ_LJ*b.C_BG_JJ_LJ"
                    strSQL = strSQL + "   		when a.C_BG_YSMJ_LJ=0 or a.C_BG_JJ_LJ=0  then b.C_BG_YSMJ_LJ*b.C_BG_JJ_LJ"
                    strSQL = strSQL + " 		when b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else   b.C_BG_YSMJ_LJ*b.C_BG_JJ_LJ-a.C_BG_YSMJ_LJ*a.C_BG_JJ_LJ  end , "
                    strSQL = strSQL + "  case when b.C_BG_YSMJ_LJ-a.C_BG_YSMJ_LJ=0 or a.C_BG_YSMJ_LJ=0 or a.C_BG_YSMJ_LJ is null then 0 "
                    strSQL = strSQL + " else (b.C_BG_YSMJ_LJ*b.C_BG_JJ_LJ-a.C_BG_YSMJ_LJ*a.C_BG_JJ_LJ)/(b.C_BG_YSMJ_LJ-a.C_BG_YSMJ_LJ) end as 签约均价,"
                    strSQL = strSQL + "    b.C_BG_YSMJ_LJ * b.C_BG_JJ_LJ as 累计签约总额   "
                    strSQL = strSQL + "  from "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select b.*,c.c_house from T_HOUSE_INFO b left join T_HOUSE_MATCH c on b.C_XM_NAME=c.C_XM_NAME and b.C_XZQH=c.C_XZQY  "
                    strSQL = strSQL + "  where b.C_TIME  between @endDate_start and @endDate_end "
                    strSQL = strSQL + "  )b"
                    strSQL = strSQL + "  Left Join "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select a.*,c.c_house from T_HOUSE_INFO a left join T_HOUSE_MATCH c on a.C_XM_NAME=c.C_XM_NAME and a.C_XZQH=c.C_XZQY "
                    strSQL = strSQL + "  where a.C_TIME between @startDate_start and @startDate_end "
                    strSQL = strSQL + "  )a  on  a.C_XM_ID=b.C_XM_ID and a.C_XM_NAME=b.C_XM_NAME and a.C_XZQH=b.C_XZQH and a.C_XM_ADDRESS=b.C_XM_ADDRESS"
                    strSQL = strSQL + "  ) A "
                    If strWhere <> "" Then
                        strSQL = strSQL + "where " + strWhere
                    End If
                    strSQL = strSQL + " group by A.行政区域, A.楼盘名称, A.项目类型 "
                    strSQL = strSQL + " union "
                    strSQL = strSQL + " select "
                    strSQL = strSQL + " A.行政区域 as 行政区域, "
                    strSQL = strSQL + " A.楼盘名称 as 楼盘名称, "
                    strSQL = strSQL + " A.项目类型 as 项目类型, "
                    strSQL = strSQL + " sum(A.签约套数) as 网签数, "
                    strSQL = strSQL + " case when sum(A.签约套数) = 0 then 0 else cast(round(sum(A.签约总额)/sum(A.签约面积),2) as numeric(16,2)) end as 网签均价, "
                    strSQL = strSQL + " sum(A.累计已售套数2) as 合共成交, "
                    strSQL = strSQL + " sum(A.未售套数2) as 未售套数, "
                    strSQL = strSQL + " cast(round(sum(A.签约总额),2) as numeric(16,2)) as 网签总额, "
                    strSQL = strSQL + " cast(round(sum(A.签约面积),2) as numeric(16,2)) as 网签面积 "
                    strSQL = strSQL + " from( "
                    strSQL = strSQL + "  select "
                    strSQL = strSQL + " b.C_ID as 序号,    "
                    strSQL = strSQL + " b.C_XZQH as 行政区域,   "
                    strSQL = strSQL + " b.C_HOUSE as 楼盘名称,  "
                    strSQL = strSQL + " b.C_XM_NAME as 项目名称,  "
                    strSQL = strSQL + " b.C_XM_ID as 预售证,    "
                    strSQL = strSQL + " '商业' as 项目类型, "
                    strSQL = strSQL + " b.C_SY_YSTS_LJ as 累计预售套数1,  "
                    strSQL = strSQL + " b.C_SY_YSMJ_LJ as 累计预售面积1, "
                    strSQL = strSQL + " b.C_SY_JJ_LJ as 累计均价1,  "
                    strSQL = strSQL + " b.C_SY_TS_WS as 未售套数1,  "
                    strSQL = strSQL + " b.C_SY_MJ_WS as 未售面积1,  "
                    strSQL = strSQL + " b.C_SY_YSTS_LJ as 累计已售套数2,   "
                    strSQL = strSQL + " b.C_SY_YSMJ_LJ as 累计已售面积2,  "
                    strSQL = strSQL + " b.C_SY_JJ_LJ as 累计均价2, "
                    strSQL = strSQL + " b.C_SY_TS_WS as 未售套数2,   "
                    strSQL = strSQL + " b.C_SY_MJ_WS as 未售面积2,  "
                    strSQL = strSQL + " 签约套数=case 	when a.C_SY_YSTS_LJ is null then b.C_SY_YSTS_LJ"
                    strSQL = strSQL + "   		when a.C_SY_YSTS_LJ=0 then  b.C_SY_YSTS_LJ"
                    strSQL = strSQL + " 		else   b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ  end , "
                    strSQL = strSQL + "  签约面积=case when a.C_SY_YSMJ_LJ is null or a.C_SY_YSMJ_LJ=0 then b.C_SY_YSMJ_LJ"
                    strSQL = strSQL + "   		when b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else    b.C_SY_YSMJ_LJ-a.C_SY_YSMJ_LJ  end ,"
                    strSQL = strSQL + " 套数核对=case when a.C_SY_YSTS_LJ is null or a.C_SY_TS_WS is null or a.C_SY_YSTS_LJ=0 or a.C_SY_TS_WS=0  then b.C_SY_YSTS_LJ+b.C_SY_TS_WS"
                    strSQL = strSQL + "   		when b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else b.C_SY_YSTS_LJ+b.C_SY_TS_WS-a.C_SY_YSTS_LJ-a.C_SY_TS_WS  end ,"
                    strSQL = strSQL + "  面积核对=case when a.C_SY_YSMJ_LJ is null or a.C_SY_MJ_WS is null or a.C_SY_YSMJ_LJ=0 or a.C_SY_MJ_WS=0 then b.C_SY_YSMJ_LJ+b.C_SY_MJ_WS"
                    strSQL = strSQL + "   		when b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else  b.C_SY_YSMJ_LJ+b.C_SY_MJ_WS-a.C_SY_YSMJ_LJ-a.C_SY_MJ_WS  end ,"
                    strSQL = strSQL + "  签约总额=case when a.C_SY_YSMJ_LJ is null or a.C_SY_JJ_LJ is null  then b.C_SY_YSMJ_LJ*b.C_SY_JJ_LJ"
                    strSQL = strSQL + "   		when a.C_SY_YSMJ_LJ=0 or a.C_SY_JJ_LJ=0  then b.C_SY_YSMJ_LJ*b.C_SY_JJ_LJ"
                    strSQL = strSQL + " 		when b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else   b.C_SY_YSMJ_LJ*b.C_SY_JJ_LJ-a.C_SY_YSMJ_LJ*a.C_SY_JJ_LJ  end , "
                    strSQL = strSQL + "  case when b.C_SY_YSMJ_LJ-a.C_SY_YSMJ_LJ=0 or a.C_SY_YSMJ_LJ=0 or a.C_SY_YSMJ_LJ is null then 0 "
                    strSQL = strSQL + " else (b.C_SY_YSMJ_LJ*b.C_SY_JJ_LJ-a.C_SY_YSMJ_LJ*a.C_SY_JJ_LJ)/(b.C_SY_YSMJ_LJ-a.C_SY_YSMJ_LJ) end as 签约均价,"
                    strSQL = strSQL + "    b.C_SY_YSMJ_LJ * b.C_SY_JJ_LJ as 累计签约总额   "
                    strSQL = strSQL + "  from "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select b.*,c.c_house from T_HOUSE_INFO b left join T_HOUSE_MATCH c on b.C_XM_NAME=c.C_XM_NAME and b.C_XZQH=c.C_XZQY  "
                    strSQL = strSQL + "  where b.C_TIME  between @endDate_start and @endDate_end "
                    strSQL = strSQL + "  )b"
                    strSQL = strSQL + "  Left Join "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select a.*,c.c_house from T_HOUSE_INFO a left join T_HOUSE_MATCH c on a.C_XM_NAME=c.C_XM_NAME and a.C_XZQH=c.C_XZQY "
                    strSQL = strSQL + "  where a.C_TIME between @startDate_start and @startDate_end "
                    strSQL = strSQL + "  )a  on  a.C_XM_ID=b.C_XM_ID and a.C_XM_NAME=b.C_XM_NAME and a.C_XZQH=b.C_XZQH and a.C_XM_ADDRESS=b.C_XM_ADDRESS"
                    strSQL = strSQL + "  ) A "
                    If strWhere <> "" Then
                        strSQL = strSQL + " where " + strWhere
                    End If
                    strSQL = strSQL + " group by A.行政区域, A.楼盘名称, A.项目类型 "
                    strSQL = strSQL + " )A"

                Case "2"
                    strSQL = strSQL + " select  "
                    strSQL = strSQL + " A.行政区域 as 行政区域, "
                    strSQL = strSQL + " A.楼盘名称 as 楼盘名称, "
                    strSQL = strSQL + " A.项目类型 as 项目类型, "
                    strSQL = strSQL + " sum(A.签约套数) as 网签数, "
                    strSQL = strSQL + "  case when sum(A.签约套数) = 0 then 0 else cast(round(sum(A.签约总额)/sum(A.签约面积),2) as numeric(16,2)) end as 网签均价, "
                    strSQL = strSQL + "  sum(A.累计已售套数2) as 合共成交, "
                    strSQL = strSQL + "  sum(A.未售套数2) as 未售套数, "
                    strSQL = strSQL + "  cast(round(sum(A.签约总额),2) as numeric(16,2)) as 网签总额, "
                    strSQL = strSQL + "  cast(round(sum(A.签约面积),2) as numeric(16,2)) as 网签面积 "
                    strSQL = strSQL + "  from( "
                     strSQL = strSQL + "  select "
                    strSQL = strSQL + " b.C_ID as 序号, "
                    strSQL = strSQL + " b.C_XZQH as 行政区域,   "
                    strSQL = strSQL + " b.C_HOUSE as 楼盘名称,  "
                    strSQL = strSQL + " b.C_XM_NAME as 项目名称,  "
                    strSQL = strSQL + " b.C_XM_ID as 预售证,    "
                    strSQL = strSQL + " '住宅' as 项目类型, "
                    strSQL = strSQL + " b.C_ZZ_YSTS_LJ as 累计预售套数1,  "
                    strSQL = strSQL + " b.C_ZZ_YSMJ_LJ as 累计预售面积1, "
                    strSQL = strSQL + " b.C_ZZ_JJ_LJ as 累计均价1,  "
                    strSQL = strSQL + " b.C_ZZ_TS_WS as 未售套数1,  "
                    strSQL = strSQL + " b.C_ZZ_MJ_WS as 未售面积1,  "
                    strSQL = strSQL + " b.C_ZZ_YSTS_LJ as 累计已售套数2,   "
                    strSQL = strSQL + " b.C_ZZ_YSMJ_LJ as 累计已售面积2,  "
                    strSQL = strSQL + " b.C_ZZ_JJ_LJ as 累计均价2, "
                    strSQL = strSQL + " b.C_ZZ_TS_WS as 未售套数2,   "
                    strSQL = strSQL + " b.C_ZZ_MJ_WS as 未售面积2,  "
                    strSQL = strSQL + " 签约套数=case 	when a.C_ZZ_YSTS_LJ is null then b.C_ZZ_YSTS_LJ"
                    strSQL = strSQL + "   		when a.C_ZZ_YSTS_LJ=0 then  b.C_ZZ_YSTS_LJ"
                    strSQL = strSQL + " 		else   b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ  end , "
                    strSQL = strSQL + "  签约面积=case when a.C_ZZ_YSMJ_LJ is null or a.C_ZZ_YSMJ_LJ=0 then b.C_ZZ_YSMJ_LJ"
                    strSQL = strSQL + "   		when b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else    b.C_ZZ_YSMJ_LJ-a.C_ZZ_YSMJ_LJ  end ,"
                    strSQL = strSQL + " 套数核对=case when a.C_ZZ_YSTS_LJ is null or a.C_ZZ_TS_WS is null or a.C_ZZ_YSTS_LJ=0 or a.C_ZZ_TS_WS=0  then b.C_ZZ_YSTS_LJ+b.C_ZZ_TS_WS"
                    strSQL = strSQL + "   		when b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else b.C_ZZ_YSTS_LJ+b.C_ZZ_TS_WS-a.C_ZZ_YSTS_LJ-a.C_ZZ_TS_WS  end ,"
                    strSQL = strSQL + "  面积核对=case when a.C_ZZ_YSMJ_LJ is null or a.C_ZZ_MJ_WS is null or a.C_ZZ_YSMJ_LJ=0 or a.C_ZZ_MJ_WS=0 then b.C_ZZ_YSMJ_LJ+b.C_ZZ_MJ_WS"
                    strSQL = strSQL + "   		when b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else  b.C_ZZ_YSMJ_LJ+b.C_ZZ_MJ_WS-a.C_ZZ_YSMJ_LJ-a.C_ZZ_MJ_WS  end ,"
                    strSQL = strSQL + "  签约总额=case when a.C_ZZ_YSMJ_LJ is null or a.C_ZZ_JJ_LJ is null  then b.C_ZZ_YSMJ_LJ*b.C_ZZ_JJ_LJ"
                    strSQL = strSQL + "   		when a.C_ZZ_YSMJ_LJ=0 or a.C_ZZ_JJ_LJ=0  then b.C_ZZ_YSMJ_LJ*b.C_ZZ_JJ_LJ"
                    strSQL = strSQL + " 		when b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else   b.C_ZZ_YSMJ_LJ*b.C_ZZ_JJ_LJ-a.C_ZZ_YSMJ_LJ*a.C_ZZ_JJ_LJ  end , "
                    strSQL = strSQL + "  case when b.C_ZZ_YSMJ_LJ-a.C_ZZ_YSMJ_LJ=0 or a.C_ZZ_YSMJ_LJ=0 or a.C_ZZ_YSMJ_LJ is null then 0 "
                    strSQL = strSQL + " else (b.C_ZZ_YSMJ_LJ*b.C_ZZ_JJ_LJ-a.C_ZZ_YSMJ_LJ*a.C_ZZ_JJ_LJ)/(b.C_ZZ_YSMJ_LJ-a.C_ZZ_YSMJ_LJ) end as 签约均价,"
                    strSQL = strSQL + "    b.C_ZZ_YSMJ_LJ * b.C_ZZ_JJ_LJ as 累计签约总额   "
                    strSQL = strSQL + "  from "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select b.*,c.c_house from T_HOUSE_INFO b left join T_HOUSE_MATCH c on b.C_XM_NAME=c.C_XM_NAME and b.C_XZQH=c.C_XZQY  "
                    strSQL = strSQL + "  where b.C_TIME  between @endDate_start and @endDate_end "
                    strSQL = strSQL + "  )b"
                    strSQL = strSQL + "  Left Join "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select a.*,c.c_house from T_HOUSE_INFO a left join T_HOUSE_MATCH c on a.C_XM_NAME=c.C_XM_NAME and a.C_XZQH=c.C_XZQY "
                    strSQL = strSQL + "  where a.C_TIME between @startDate_start and @startDate_end "
                    strSQL = strSQL + "  )a  on  a.C_XM_ID=b.C_XM_ID and a.C_XM_NAME=b.C_XM_NAME and a.C_XZQH=b.C_XZQH and a.C_XM_ADDRESS=b.C_XM_ADDRESS"
                    strSQL = strSQL + "   ) A "
                    If strWhere <> "" Then
                        strSQL = strSQL + "where " + strWhere
                    End If
                    strSQL = strSQL + " group by A.行政区域, A.楼盘名称, A.项目类型 "

                Case "3"
                    strSQL = strSQL + " select "
                    strSQL = strSQL + " A.行政区域 as 行政区域, "
                    strSQL = strSQL + " A.楼盘名称 as 楼盘名称, "
                    strSQL = strSQL + " A.项目类型 as 项目类型, "
                    strSQL = strSQL + " sum(A.签约套数) as 网签数, "
                    strSQL = strSQL + " case when sum(A.签约套数) = 0 then 0 else cast(round(sum(A.签约总额)/sum(A.签约面积),2) as numeric(16,2)) end as 网签均价, "
                    strSQL = strSQL + " sum(A.累计已售套数2) as 合共成交, "
                    strSQL = strSQL + " sum(A.未售套数2) as 未售套数, "
                    strSQL = strSQL + " cast(round(sum(A.签约总额),2) as numeric(16,2)) as 网签总额, "
                    strSQL = strSQL + " cast(round(sum(A.签约面积),2) as numeric(16,2)) as 网签面积 "
                    strSQL = strSQL + " from( "
                    strSQL = strSQL + "  select "
                    strSQL = strSQL + " b.C_ID as 序号,  "
                    strSQL = strSQL + " b.C_XZQH as 行政区域,   "
                    strSQL = strSQL + " b.C_HOUSE as 楼盘名称,  "
                    strSQL = strSQL + " b.C_XM_NAME as 项目名称,  "
                    strSQL = strSQL + " b.C_XM_ID as 预售证,    "
                    strSQL = strSQL + " '办公' as 项目类型, "
                    strSQL = strSQL + " b.C_BG_YSTS_LJ as 累计预售套数1,  "
                    strSQL = strSQL + " b.C_BG_YSMJ_LJ as 累计预售面积1, "
                    strSQL = strSQL + " b.C_BG_JJ_LJ as 累计均价1,  "
                    strSQL = strSQL + " b.C_BG_TS_WS as 未售套数1,  "
                    strSQL = strSQL + " b.C_BG_MJ_WS as 未售面积1,  "
                    strSQL = strSQL + " b.C_BG_YSTS_LJ as 累计已售套数2,   "
                    strSQL = strSQL + " b.C_BG_YSMJ_LJ as 累计已售面积2,  "
                    strSQL = strSQL + " b.C_BG_JJ_LJ as 累计均价2, "
                    strSQL = strSQL + " b.C_BG_TS_WS as 未售套数2,   "
                    strSQL = strSQL + " b.C_BG_MJ_WS as 未售面积2,  "
                    strSQL = strSQL + " 签约套数=case 	when a.C_BG_YSTS_LJ is null then b.C_BG_YSTS_LJ"
                    strSQL = strSQL + "   		when a.C_BG_YSTS_LJ=0 then  b.C_BG_YSTS_LJ"
                    strSQL = strSQL + " 		else   b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ  end , "
                    strSQL = strSQL + "  签约面积=case when a.C_BG_YSMJ_LJ is null or a.C_BG_YSMJ_LJ=0 then b.C_BG_YSMJ_LJ"
                    strSQL = strSQL + "   		when b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else    b.C_BG_YSMJ_LJ-a.C_BG_YSMJ_LJ  end ,"
                    strSQL = strSQL + " 套数核对=case when a.C_BG_YSTS_LJ is null or a.C_BG_TS_WS is null or a.C_BG_YSTS_LJ=0 or a.C_BG_TS_WS=0  then b.C_BG_YSTS_LJ+b.C_BG_TS_WS"
                    strSQL = strSQL + "   		when b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else b.C_BG_YSTS_LJ+b.C_BG_TS_WS-a.C_BG_YSTS_LJ-a.C_BG_TS_WS  end ,"
                    strSQL = strSQL + "  面积核对=case when a.C_BG_YSMJ_LJ is null or a.C_BG_MJ_WS is null or a.C_BG_YSMJ_LJ=0 or a.C_BG_MJ_WS=0 then b.C_BG_YSMJ_LJ+b.C_BG_MJ_WS"
                    strSQL = strSQL + "   		when b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else  b.C_BG_YSMJ_LJ+b.C_BG_MJ_WS-a.C_BG_YSMJ_LJ-a.C_BG_MJ_WS  end ,"
                    strSQL = strSQL + "  签约总额=case when a.C_BG_YSMJ_LJ is null or a.C_BG_JJ_LJ is null  then b.C_BG_YSMJ_LJ*b.C_BG_JJ_LJ"
                    strSQL = strSQL + "   		when a.C_BG_YSMJ_LJ=0 or a.C_BG_JJ_LJ=0  then b.C_BG_YSMJ_LJ*b.C_BG_JJ_LJ"
                    strSQL = strSQL + " 		when b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else   b.C_BG_YSMJ_LJ*b.C_BG_JJ_LJ-a.C_BG_YSMJ_LJ*a.C_BG_JJ_LJ  end , "
                    strSQL = strSQL + "  case when b.C_BG_YSMJ_LJ-a.C_BG_YSMJ_LJ=0 or a.C_BG_YSMJ_LJ=0 or a.C_BG_YSMJ_LJ is null then 0 "
                    strSQL = strSQL + " else (b.C_BG_YSMJ_LJ*b.C_BG_JJ_LJ-a.C_BG_YSMJ_LJ*a.C_BG_JJ_LJ)/(b.C_BG_YSMJ_LJ-a.C_BG_YSMJ_LJ) end as 签约均价,"
                    strSQL = strSQL + "    b.C_BG_YSMJ_LJ * b.C_BG_JJ_LJ as 累计签约总额   "
                    strSQL = strSQL + "  from "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select b.*,c.c_house from T_HOUSE_INFO b left join T_HOUSE_MATCH c on b.C_XM_NAME=c.C_XM_NAME and b.C_XZQH=c.C_XZQY  "
                    strSQL = strSQL + "  where b.C_TIME  between @endDate_start and @endDate_end "
                    strSQL = strSQL + "  )b"
                    strSQL = strSQL + "  Left Join "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select a.*,c.c_house from T_HOUSE_INFO a left join T_HOUSE_MATCH c on a.C_XM_NAME=c.C_XM_NAME and a.C_XZQH=c.C_XZQY "
                    strSQL = strSQL + "  where a.C_TIME between @startDate_start and @startDate_end "
                    strSQL = strSQL + "  )a  on  a.C_XM_ID=b.C_XM_ID and a.C_XM_NAME=b.C_XM_NAME and a.C_XZQH=b.C_XZQH and a.C_XM_ADDRESS=b.C_XM_ADDRESS"
                    strSQL = strSQL + "  ) A "
                    If strWhere <> "" Then
                        strSQL = strSQL + "where " + strWhere
                    End If
                    strSQL = strSQL + " group by A.行政区域, A.楼盘名称, A.项目类型"

                Case "1"
                    strSQL = strSQL + " select  "
                    strSQL = strSQL + " A.行政区域 as 行政区域, "
                    strSQL = strSQL + " A.楼盘名称 as 楼盘名称, "
                    strSQL = strSQL + " A.项目类型 as 项目类型, "
                    strSQL = strSQL + " sum(A.签约套数) as 网签数, "
                    strSQL = strSQL + " case when sum(A.签约套数) = 0 then 0 else cast(round(sum(A.签约总额)/sum(A.签约面积),2) as numeric(16,2)) end as 网签均价, "
                    strSQL = strSQL + " sum(A.累计已售套数2) as 合共成交, "
                    strSQL = strSQL + " sum(A.未售套数2) as 未售套数, "
                    strSQL = strSQL + " cast(round(sum(A.签约总额),2) as numeric(16,2)) as 网签总额, "
                    strSQL = strSQL + " cast(round(sum(A.签约面积),2) as numeric(16,2)) as 网签面积 "
                    strSQL = strSQL + " from( "
                    strSQL = strSQL + "  select "
                    strSQL = strSQL + " b.C_ID as 序号,   "
                    strSQL = strSQL + " b.C_XZQH as 行政区域,   "
                    strSQL = strSQL + " b.C_HOUSE as 楼盘名称,  "
                    strSQL = strSQL + " b.C_XM_NAME as 项目名称,  "
                    strSQL = strSQL + " b.C_XM_ID as 预售证,    "
                    strSQL = strSQL + " '商业' as 项目类型, "
                    strSQL = strSQL + " b.C_SY_YSTS_LJ as 累计预售套数1,  "
                    strSQL = strSQL + " b.C_SY_YSMJ_LJ as 累计预售面积1, "
                    strSQL = strSQL + " b.C_SY_JJ_LJ as 累计均价1,  "
                    strSQL = strSQL + " b.C_SY_TS_WS as 未售套数1,  "
                    strSQL = strSQL + " b.C_SY_MJ_WS as 未售面积1,  "
                    strSQL = strSQL + " b.C_SY_YSTS_LJ as 累计已售套数2,   "
                    strSQL = strSQL + " b.C_SY_YSMJ_LJ as 累计已售面积2,  "
                    strSQL = strSQL + " b.C_SY_JJ_LJ as 累计均价2, "
                    strSQL = strSQL + " b.C_SY_TS_WS as 未售套数2,   "
                    strSQL = strSQL + " b.C_SY_MJ_WS as 未售面积2,  "
                    strSQL = strSQL + " 签约套数=case 	when a.C_SY_YSTS_LJ is null then b.C_SY_YSTS_LJ"
                    strSQL = strSQL + "   		when a.C_SY_YSTS_LJ=0 then  b.C_SY_YSTS_LJ"
                    strSQL = strSQL + " 		else   b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ  end , "
                    strSQL = strSQL + "  签约面积=case when a.C_SY_YSMJ_LJ is null or a.C_SY_YSMJ_LJ=0 then b.C_SY_YSMJ_LJ"
                    strSQL = strSQL + "   		when b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else    b.C_SY_YSMJ_LJ-a.C_SY_YSMJ_LJ  end ,"
                    strSQL = strSQL + " 套数核对=case when a.C_SY_YSTS_LJ is null or a.C_SY_TS_WS is null or a.C_SY_YSTS_LJ=0 or a.C_SY_TS_WS=0  then b.C_SY_YSTS_LJ+b.C_SY_TS_WS"
                    strSQL = strSQL + "   		when b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else b.C_SY_YSTS_LJ+b.C_SY_TS_WS-a.C_SY_YSTS_LJ-a.C_SY_TS_WS  end ,"
                    strSQL = strSQL + "  面积核对=case when a.C_SY_YSMJ_LJ is null or a.C_SY_MJ_WS is null or a.C_SY_YSMJ_LJ=0 or a.C_SY_MJ_WS=0 then b.C_SY_YSMJ_LJ+b.C_SY_MJ_WS"
                    strSQL = strSQL + "   		when b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else  b.C_SY_YSMJ_LJ+b.C_SY_MJ_WS-a.C_SY_YSMJ_LJ-a.C_SY_MJ_WS  end ,"
                    strSQL = strSQL + "  签约总额=case when a.C_SY_YSMJ_LJ is null or a.C_SY_JJ_LJ is null  then b.C_SY_YSMJ_LJ*b.C_SY_JJ_LJ"
                    strSQL = strSQL + "   		when a.C_SY_YSMJ_LJ=0 or a.C_SY_JJ_LJ=0  then b.C_SY_YSMJ_LJ*b.C_SY_JJ_LJ"
                    strSQL = strSQL + " 		when b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else   b.C_SY_YSMJ_LJ*b.C_SY_JJ_LJ-a.C_SY_YSMJ_LJ*a.C_SY_JJ_LJ  end , "
                    strSQL = strSQL + "  case when b.C_SY_YSMJ_LJ-a.C_SY_YSMJ_LJ=0 or a.C_SY_YSMJ_LJ=0 or a.C_SY_YSMJ_LJ is null then 0 "
                    strSQL = strSQL + " else (b.C_SY_YSMJ_LJ*b.C_SY_JJ_LJ-a.C_SY_YSMJ_LJ*a.C_SY_JJ_LJ)/(b.C_SY_YSMJ_LJ-a.C_SY_YSMJ_LJ) end as 签约均价,"
                    strSQL = strSQL + "    b.C_SY_YSMJ_LJ * b.C_SY_JJ_LJ as 累计签约总额   "
                    strSQL = strSQL + "  from "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select b.*,c.c_house from T_HOUSE_INFO b left join T_HOUSE_MATCH c on b.C_XM_NAME=c.C_XM_NAME and b.C_XZQH=c.C_XZQY  "
                    strSQL = strSQL + "  where b.C_TIME  between @endDate_start and @endDate_end "
                    strSQL = strSQL + "  )b"
                    strSQL = strSQL + "  Left Join "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select a.*,c.c_house from T_HOUSE_INFO a left join T_HOUSE_MATCH c on a.C_XM_NAME=c.C_XM_NAME and a.C_XZQH=c.C_XZQY "
                    strSQL = strSQL + "  where a.C_TIME between @startDate_start and @startDate_end "
                    strSQL = strSQL + "  )a  on  a.C_XM_ID=b.C_XM_ID and a.C_XM_NAME=b.C_XM_NAME and a.C_XZQH=b.C_XZQH and a.C_XM_ADDRESS=b.C_XM_ADDRESS"
                    strSQL = strSQL + "  ) A "
                    If strWhere <> "" Then
                        strSQL = strSQL + "where " + strWhere
                    End If
                    strSQL = strSQL + " group by A.行政区域, A.楼盘名称, A.项目类型"

                Case Else

            End Select

            getSql_BuildingCompute = True
errProc:

            Exit Function
        End Function


        '----------------------------------------------------------------
        ' 根据“查询条件”获取“阳光家缘楼盘信息数据分析”完全数据的数据集
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strSQL                     ：返回的SQL语句
        '     strType                    ：选择楼盘的类型。0-全部；1-商业；2-住宅；3-办公
        '     strWhere                   ：搜索字符串      
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getSql_BuildingCompute_x2( _
             ByRef strErrMsg As String, _
             ByRef strSQL As String, _
             ByVal strWhere As String, _
             ByRef strType As String) As Boolean

            getSql_BuildingCompute_x2 = False

            strSQL = ""
            Select Case strType
                Case "0"
                    strSQL = strSQL + " select * from ("
                    strSQL = strSQL + " select "
                    strSQL = strSQL + " A.行政区域, "
                    strSQL = strSQL + " A.楼盘名称, "
                    strSQL = strSQL + " case when 房屋类型='1' then '别墅' else '洋房' end as 项目类型, "
                    strSQL = strSQL + " sum(A.签约套数) as 网签数, "
                    strSQL = strSQL + "  case when sum(A.签约套数) = 0 then 0 else cast(round(sum(A.签约总额)/sum(A.签约面积),2) as numeric(16,2)) end as 网签均价, "
                    strSQL = strSQL + "  sum(A.累计已售套数2) as 合共成交, "
                    strSQL = strSQL + "  sum(A.未售套数2) as 未售套数, "
                    strSQL = strSQL + "  cast(round(sum(A.签约总额),2) as numeric(16,2)) as 网签总额, "
                    strSQL = strSQL + "  cast(round(sum(A.签约面积),2) as numeric(16,2)) as 网签面积 "
                    strSQL = strSQL + "  from( "
                    strSQL = strSQL + "  select "
                    strSQL = strSQL + " b.C_ID as 序号,  "
                    strSQL = strSQL + " b.C_XZQH as 行政区域,   "
                    strSQL = strSQL + " b.C_HOUSE as 楼盘名称,  "
                    strSQL = strSQL + " b.C_XM_NAME as 项目名称,  "
                    strSQL = strSQL + " b.C_XM_ID as 预售证,    "
                    strSQL = strSQL + "  isnull(b.c_type,0) as 房屋类型,    "
                    strSQL = strSQL + " b.C_ZZ_YSTS_LJ as 累计预售套数1,  "
                    strSQL = strSQL + " b.C_ZZ_YSMJ_LJ as 累计预售面积1, "
                    strSQL = strSQL + " b.C_ZZ_JJ_LJ as 累计均价1,  "
                    strSQL = strSQL + " b.C_ZZ_TS_WS as 未售套数1,  "
                    strSQL = strSQL + " b.C_ZZ_MJ_WS as 未售面积1,  "
                    strSQL = strSQL + " b.C_ZZ_YSTS_LJ as 累计已售套数2,   "
                    strSQL = strSQL + " b.C_ZZ_YSMJ_LJ as 累计已售面积2,  "
                    strSQL = strSQL + " b.C_ZZ_JJ_LJ as 累计均价2, "
                    strSQL = strSQL + " b.C_ZZ_TS_WS as 未售套数2,   "
                    strSQL = strSQL + " b.C_ZZ_MJ_WS as 未售面积2,  "
                    strSQL = strSQL + " 签约套数=case 	when a.C_ZZ_YSTS_LJ is null then b.C_ZZ_YSTS_LJ"
                    strSQL = strSQL + "   		when a.C_ZZ_YSTS_LJ=0 then  b.C_ZZ_YSTS_LJ"
                    strSQL = strSQL + " 		else   b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ  end , "
                    strSQL = strSQL + "  签约面积=case when a.C_ZZ_YSMJ_LJ is null or a.C_ZZ_YSMJ_LJ=0 then b.C_ZZ_YSMJ_LJ"
                    strSQL = strSQL + "   		when b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else    b.C_ZZ_YSMJ_LJ-a.C_ZZ_YSMJ_LJ  end ,"
                    strSQL = strSQL + " 套数核对=case when a.C_ZZ_YSTS_LJ is null or a.C_ZZ_TS_WS is null or a.C_ZZ_YSTS_LJ=0 or a.C_ZZ_TS_WS=0  then b.C_ZZ_YSTS_LJ+b.C_ZZ_TS_WS"
                    strSQL = strSQL + "   		when b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else b.C_ZZ_YSTS_LJ+b.C_ZZ_TS_WS-a.C_ZZ_YSTS_LJ-a.C_ZZ_TS_WS  end ,"
                    strSQL = strSQL + "  面积核对=case when a.C_ZZ_YSMJ_LJ is null or a.C_ZZ_MJ_WS is null or a.C_ZZ_YSMJ_LJ=0 or a.C_ZZ_MJ_WS=0 then b.C_ZZ_YSMJ_LJ+b.C_ZZ_MJ_WS"
                    strSQL = strSQL + "   		when b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else  b.C_ZZ_YSMJ_LJ+b.C_ZZ_MJ_WS-a.C_ZZ_YSMJ_LJ-a.C_ZZ_MJ_WS  end ,"
                    strSQL = strSQL + "  签约总额=case when a.C_ZZ_YSMJ_LJ is null or a.C_ZZ_JJ_LJ is null  then b.C_ZZ_YSMJ_LJ*b.C_ZZ_JJ_LJ"
                    strSQL = strSQL + "   		when a.C_ZZ_YSMJ_LJ=0 or a.C_ZZ_JJ_LJ=0  then b.C_ZZ_YSMJ_LJ*b.C_ZZ_JJ_LJ"
                    strSQL = strSQL + " 		when b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else   b.C_ZZ_YSMJ_LJ*b.C_ZZ_JJ_LJ-a.C_ZZ_YSMJ_LJ*a.C_ZZ_JJ_LJ  end , "
                    strSQL = strSQL + "  case when b.C_ZZ_YSMJ_LJ-a.C_ZZ_YSMJ_LJ=0 or a.C_ZZ_YSMJ_LJ=0 or a.C_ZZ_YSMJ_LJ is null then 0 "
                    strSQL = strSQL + " else (b.C_ZZ_YSMJ_LJ*b.C_ZZ_JJ_LJ-a.C_ZZ_YSMJ_LJ*a.C_ZZ_JJ_LJ)/(b.C_ZZ_YSMJ_LJ-a.C_ZZ_YSMJ_LJ) end as 签约均价,"
                    strSQL = strSQL + "    b.C_ZZ_YSMJ_LJ * b.C_ZZ_JJ_LJ as 累计签约总额   "
                    strSQL = strSQL + "  from "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select b.*,c.c_house,c.c_type from T_HOUSE_INFO b left join T_HOUSE_MATCH_XMID c on b.C_XM_ID=c.C_XM_ID and b.C_XM_NAME=c.C_XM_NAME "
                    strSQL = strSQL + "  where b.C_TIME  between @endDate_start and @endDate_end "
                    strSQL = strSQL + "  )b"
                    strSQL = strSQL + "  Left Join "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select a.*,c.c_house,c.c_type from T_HOUSE_INFO a left join T_HOUSE_MATCH_XMID c on a.C_XM_ID=c.C_XM_ID and a.C_XM_NAME=c.C_XM_NAME "
                    strSQL = strSQL + "  where a.C_TIME between @startDate_start and @startDate_end "
                    strSQL = strSQL + "  )a  on  a.C_XM_ID=b.C_XM_ID and a.C_XM_NAME=b.C_XM_NAME and a.C_XZQH=b.C_XZQH and a.C_XM_ADDRESS=b.C_XM_ADDRESS and a.c_type=b.c_type"
                    strSQL = strSQL + "   ) A "
                    If strWhere <> "" Then
                        strSQL = strSQL + "where " + strWhere
                    End If
                    strSQL = strSQL + " group by A.行政区域, A.楼盘名称, A.房屋类型 "
                    strSQL = strSQL + " union "
                    strSQL = strSQL + " select "
                    strSQL = strSQL + " A.行政区域 as 行政区域, "
                    strSQL = strSQL + " A.楼盘名称 as 楼盘名称, "
                    strSQL = strSQL + " A.项目类型 as 项目类型, "
                    strSQL = strSQL + " sum(A.签约套数) as 网签数, "
                    strSQL = strSQL + " case when sum(A.签约套数) = 0 then 0 else cast(round(sum(A.签约总额)/sum(A.签约面积),2) as numeric(16,2)) end as 网签均价, "
                    strSQL = strSQL + " sum(A.累计已售套数2) as 合共成交, "
                    strSQL = strSQL + " sum(A.未售套数2) as 未售套数, "
                    strSQL = strSQL + " cast(round(sum(A.签约总额),2) as numeric(16,2)) as 网签总额, "
                    strSQL = strSQL + " cast(round(sum(A.签约面积),2) as numeric(16,2)) as 网签面积 "
                    strSQL = strSQL + " from( "
                    strSQL = strSQL + "  select "
                    strSQL = strSQL + " b.C_ID as 序号, "
                    strSQL = strSQL + " b.C_XZQH as 行政区域,   "
                    strSQL = strSQL + " b.C_HOUSE as 楼盘名称,  "
                    strSQL = strSQL + " b.C_XM_NAME as 项目名称,  "
                    strSQL = strSQL + " b.C_XM_ID as 预售证,    "
                    strSQL = strSQL + " '办公' as 项目类型, "
                    strSQL = strSQL + " b.C_BG_YSTS_LJ as 累计预售套数1,  "
                    strSQL = strSQL + " b.C_BG_YSMJ_LJ as 累计预售面积1, "
                    strSQL = strSQL + " b.C_BG_JJ_LJ as 累计均价1,  "
                    strSQL = strSQL + " b.C_BG_TS_WS as 未售套数1,  "
                    strSQL = strSQL + " b.C_BG_MJ_WS as 未售面积1,  "
                    strSQL = strSQL + " b.C_BG_YSTS_LJ as 累计已售套数2,   "
                    strSQL = strSQL + " b.C_BG_YSMJ_LJ as 累计已售面积2,  "
                    strSQL = strSQL + " b.C_BG_JJ_LJ as 累计均价2, "
                    strSQL = strSQL + " b.C_BG_TS_WS as 未售套数2,   "
                    strSQL = strSQL + " b.C_BG_MJ_WS as 未售面积2,  "
                    strSQL = strSQL + " 签约套数=case 	when a.C_BG_YSTS_LJ is null then b.C_BG_YSTS_LJ"
                    strSQL = strSQL + "   		when a.C_BG_YSTS_LJ=0 then  b.C_BG_YSTS_LJ"
                    strSQL = strSQL + " 		else   b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ  end , "
                    strSQL = strSQL + "  签约面积=case when a.C_BG_YSMJ_LJ is null or a.C_BG_YSMJ_LJ=0 then b.C_BG_YSMJ_LJ"
                    strSQL = strSQL + "   		when b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else    b.C_BG_YSMJ_LJ-a.C_BG_YSMJ_LJ  end ,"
                    strSQL = strSQL + " 套数核对=case when a.C_BG_YSTS_LJ is null or a.C_BG_TS_WS is null or a.C_BG_YSTS_LJ=0 or a.C_BG_TS_WS=0  then b.C_BG_YSTS_LJ+b.C_BG_TS_WS"
                    strSQL = strSQL + "   		when b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else b.C_BG_YSTS_LJ+b.C_BG_TS_WS-a.C_BG_YSTS_LJ-a.C_BG_TS_WS  end ,"
                    strSQL = strSQL + "  面积核对=case when a.C_BG_YSMJ_LJ is null or a.C_BG_MJ_WS is null or a.C_BG_YSMJ_LJ=0 or a.C_BG_MJ_WS=0 then b.C_BG_YSMJ_LJ+b.C_BG_MJ_WS"
                    strSQL = strSQL + "   		when b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else  b.C_BG_YSMJ_LJ+b.C_BG_MJ_WS-a.C_BG_YSMJ_LJ-a.C_BG_MJ_WS  end ,"
                    strSQL = strSQL + "  签约总额=case when a.C_BG_YSMJ_LJ is null or a.C_BG_JJ_LJ is null  then b.C_BG_YSMJ_LJ*b.C_BG_JJ_LJ"
                    strSQL = strSQL + "   		when a.C_BG_YSMJ_LJ=0 or a.C_BG_JJ_LJ=0  then b.C_BG_YSMJ_LJ*b.C_BG_JJ_LJ"
                    strSQL = strSQL + " 		when b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else   b.C_BG_YSMJ_LJ*b.C_BG_JJ_LJ-a.C_BG_YSMJ_LJ*a.C_BG_JJ_LJ  end , "
                    strSQL = strSQL + "  case when b.C_BG_YSMJ_LJ-a.C_BG_YSMJ_LJ=0 or a.C_BG_YSMJ_LJ=0 or a.C_BG_YSMJ_LJ is null then 0 "
                    strSQL = strSQL + " else (b.C_BG_YSMJ_LJ*b.C_BG_JJ_LJ-a.C_BG_YSMJ_LJ*a.C_BG_JJ_LJ)/(b.C_BG_YSMJ_LJ-a.C_BG_YSMJ_LJ) end as 签约均价,"
                    strSQL = strSQL + "    b.C_BG_YSMJ_LJ * b.C_BG_JJ_LJ as 累计签约总额   "
                    strSQL = strSQL + "  from "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select b.*,c.c_house from T_HOUSE_INFO b left join T_HOUSE_MATCH c on b.C_XM_NAME=c.C_XM_NAME and b.C_XZQH=c.C_XZQY  "
                    strSQL = strSQL + "  where b.C_TIME  between @endDate_start and @endDate_end "
                    strSQL = strSQL + "  )b"
                    strSQL = strSQL + "  Left Join "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select a.*,c.c_house from T_HOUSE_INFO a left join T_HOUSE_MATCH c on a.C_XM_NAME=c.C_XM_NAME and a.C_XZQH=c.C_XZQY "
                    strSQL = strSQL + "  where a.C_TIME between @startDate_start and @startDate_end "
                    strSQL = strSQL + "  )a  on  a.C_XM_ID=b.C_XM_ID and a.C_XM_NAME=b.C_XM_NAME and a.C_XZQH=b.C_XZQH and a.C_XM_ADDRESS=b.C_XM_ADDRESS"
                    strSQL = strSQL + "  ) A "
                    If strWhere <> "" Then
                        strSQL = strSQL + "where " + strWhere
                    End If
                    strSQL = strSQL + " group by A.行政区域, A.楼盘名称, A.项目类型 "
                    strSQL = strSQL + " union "
                    strSQL = strSQL + " select "
                    strSQL = strSQL + " A.行政区域 as 行政区域, "
                    strSQL = strSQL + " A.楼盘名称 as 楼盘名称, "
                    strSQL = strSQL + " A.项目类型 as 项目类型, "
                    strSQL = strSQL + " sum(A.签约套数) as 网签数, "
                    strSQL = strSQL + " case when sum(A.签约套数) = 0 then 0 else cast(round(sum(A.签约总额)/sum(A.签约面积),2) as numeric(16,2)) end as 网签均价, "
                    strSQL = strSQL + " sum(A.累计已售套数2) as 合共成交, "
                    strSQL = strSQL + " sum(A.未售套数2) as 未售套数, "
                    strSQL = strSQL + " cast(round(sum(A.签约总额),2) as numeric(16,2)) as 网签总额, "
                    strSQL = strSQL + " cast(round(sum(A.签约面积),2) as numeric(16,2)) as 网签面积 "
                    strSQL = strSQL + " from( "
                    strSQL = strSQL + "  select "
                    strSQL = strSQL + " b.C_ID as 序号,    "
                    strSQL = strSQL + " b.C_XZQH as 行政区域,   "
                    strSQL = strSQL + " b.C_HOUSE as 楼盘名称,  "
                    strSQL = strSQL + " b.C_XM_NAME as 项目名称,  "
                    strSQL = strSQL + " b.C_XM_ID as 预售证,    "
                    strSQL = strSQL + " '商业' as 项目类型, "
                    strSQL = strSQL + " b.C_SY_YSTS_LJ as 累计预售套数1,  "
                    strSQL = strSQL + " b.C_SY_YSMJ_LJ as 累计预售面积1, "
                    strSQL = strSQL + " b.C_SY_JJ_LJ as 累计均价1,  "
                    strSQL = strSQL + " b.C_SY_TS_WS as 未售套数1,  "
                    strSQL = strSQL + " b.C_SY_MJ_WS as 未售面积1,  "
                    strSQL = strSQL + " b.C_SY_YSTS_LJ as 累计已售套数2,   "
                    strSQL = strSQL + " b.C_SY_YSMJ_LJ as 累计已售面积2,  "
                    strSQL = strSQL + " b.C_SY_JJ_LJ as 累计均价2, "
                    strSQL = strSQL + " b.C_SY_TS_WS as 未售套数2,   "
                    strSQL = strSQL + " b.C_SY_MJ_WS as 未售面积2,  "
                    strSQL = strSQL + " 签约套数=case 	when a.C_SY_YSTS_LJ is null then b.C_SY_YSTS_LJ"
                    strSQL = strSQL + "   		when a.C_SY_YSTS_LJ=0 then  b.C_SY_YSTS_LJ"
                    strSQL = strSQL + " 		else   b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ  end , "
                    strSQL = strSQL + "  签约面积=case when a.C_SY_YSMJ_LJ is null or a.C_SY_YSMJ_LJ=0 then b.C_SY_YSMJ_LJ"
                    strSQL = strSQL + "   		when b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else    b.C_SY_YSMJ_LJ-a.C_SY_YSMJ_LJ  end ,"
                    strSQL = strSQL + " 套数核对=case when a.C_SY_YSTS_LJ is null or a.C_SY_TS_WS is null or a.C_SY_YSTS_LJ=0 or a.C_SY_TS_WS=0  then b.C_SY_YSTS_LJ+b.C_SY_TS_WS"
                    strSQL = strSQL + "   		when b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else b.C_SY_YSTS_LJ+b.C_SY_TS_WS-a.C_SY_YSTS_LJ-a.C_SY_TS_WS  end ,"
                    strSQL = strSQL + "  面积核对=case when a.C_SY_YSMJ_LJ is null or a.C_SY_MJ_WS is null or a.C_SY_YSMJ_LJ=0 or a.C_SY_MJ_WS=0 then b.C_SY_YSMJ_LJ+b.C_SY_MJ_WS"
                    strSQL = strSQL + "   		when b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else  b.C_SY_YSMJ_LJ+b.C_SY_MJ_WS-a.C_SY_YSMJ_LJ-a.C_SY_MJ_WS  end ,"
                    strSQL = strSQL + "  签约总额=case when a.C_SY_YSMJ_LJ is null or a.C_SY_JJ_LJ is null  then b.C_SY_YSMJ_LJ*b.C_SY_JJ_LJ"
                    strSQL = strSQL + "   		when a.C_SY_YSMJ_LJ=0 or a.C_SY_JJ_LJ=0  then b.C_SY_YSMJ_LJ*b.C_SY_JJ_LJ"
                    strSQL = strSQL + " 		when b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else   b.C_SY_YSMJ_LJ*b.C_SY_JJ_LJ-a.C_SY_YSMJ_LJ*a.C_SY_JJ_LJ  end , "
                    strSQL = strSQL + "  case when b.C_SY_YSMJ_LJ-a.C_SY_YSMJ_LJ=0 or a.C_SY_YSMJ_LJ=0 or a.C_SY_YSMJ_LJ is null then 0 "
                    strSQL = strSQL + " else (b.C_SY_YSMJ_LJ*b.C_SY_JJ_LJ-a.C_SY_YSMJ_LJ*a.C_SY_JJ_LJ)/(b.C_SY_YSMJ_LJ-a.C_SY_YSMJ_LJ) end as 签约均价,"
                    strSQL = strSQL + "    b.C_SY_YSMJ_LJ * b.C_SY_JJ_LJ as 累计签约总额   "
                    strSQL = strSQL + "  from "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select b.*,c.c_house from T_HOUSE_INFO b left join T_HOUSE_MATCH c on b.C_XM_NAME=c.C_XM_NAME and b.C_XZQH=c.C_XZQY  "
                    strSQL = strSQL + "  where b.C_TIME  between @endDate_start and @endDate_end "
                    strSQL = strSQL + "  )b"
                    strSQL = strSQL + "  Left Join "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select a.*,c.c_house from T_HOUSE_INFO a left join T_HOUSE_MATCH c on a.C_XM_NAME=c.C_XM_NAME and a.C_XZQH=c.C_XZQY "
                    strSQL = strSQL + "  where a.C_TIME between @startDate_start and @startDate_end "
                    strSQL = strSQL + "  )a  on  a.C_XM_ID=b.C_XM_ID and a.C_XM_NAME=b.C_XM_NAME and a.C_XZQH=b.C_XZQH and a.C_XM_ADDRESS=b.C_XM_ADDRESS"
                    strSQL = strSQL + "  ) A "
                    If strWhere <> "" Then
                        strSQL = strSQL + " where " + strWhere
                    End If
                    strSQL = strSQL + " group by A.行政区域, A.楼盘名称, A.项目类型 "
                    strSQL = strSQL + " )A"

                Case "2"
                    strSQL = strSQL + " select a.*,isnull(b.i_sort,100000) as '楼盘排序' from "
                    strSQL = strSQL + " (	"
                    strSQL = strSQL + " select "
                    strSQL = strSQL + " A.行政区域, "
                    strSQL = strSQL + " A.楼盘名称, "
                    strSQL = strSQL + " case when 房屋类型='1' then '别墅' else '洋房' end as 项目类型, "
                    strSQL = strSQL + " sum(A.签约套数) as 网签数, "
                    strSQL = strSQL + "  case when sum(A.签约套数) = 0 then 0 else cast(round(sum(A.签约总额)/sum(A.签约面积),2) as numeric(16,2)) end as 网签均价, "
                    strSQL = strSQL + "  sum(A.累计已售套数2) as 合共成交, "
                    strSQL = strSQL + "  sum(A.未售套数2) as 未售套数, "
                    strSQL = strSQL + "  cast(round(sum(A.签约总额),2) as numeric(16,2)) as 网签总额, "
                    strSQL = strSQL + "  cast(round(sum(A.签约面积),2) as numeric(16,2)) as 网签面积 "
                    strSQL = strSQL + "  from( "
                    strSQL = strSQL + "  select "
                    strSQL = strSQL + " b.C_ID as 序号,  "
                    strSQL = strSQL + " b.C_XZQH as 行政区域,   "
                    strSQL = strSQL + " b.C_HOUSE as 楼盘名称,  "
                    strSQL = strSQL + " b.C_XM_NAME as 项目名称,  "
                    strSQL = strSQL + " b.C_XM_ID as 预售证,    "
                    strSQL = strSQL + "  isnull(b.c_type,0) as 房屋类型,    "
                    strSQL = strSQL + " b.C_ZZ_YSTS_LJ as 累计预售套数1,  "
                    strSQL = strSQL + " b.C_ZZ_YSMJ_LJ as 累计预售面积1, "
                    strSQL = strSQL + " b.C_ZZ_JJ_LJ as 累计均价1,  "
                    strSQL = strSQL + " b.C_ZZ_TS_WS as 未售套数1,  "
                    strSQL = strSQL + " b.C_ZZ_MJ_WS as 未售面积1,  "
                    strSQL = strSQL + " b.C_ZZ_YSTS_LJ as 累计已售套数2,   "
                    strSQL = strSQL + " b.C_ZZ_YSMJ_LJ as 累计已售面积2,  "
                    strSQL = strSQL + " b.C_ZZ_JJ_LJ as 累计均价2, "
                    strSQL = strSQL + " b.C_ZZ_TS_WS as 未售套数2,   "
                    strSQL = strSQL + " b.C_ZZ_MJ_WS as 未售面积2,  "
                    strSQL = strSQL + " 签约套数=case 	when a.C_ZZ_YSTS_LJ is null then b.C_ZZ_YSTS_LJ"
                    strSQL = strSQL + "   		when a.C_ZZ_YSTS_LJ=0 then  b.C_ZZ_YSTS_LJ"
                    strSQL = strSQL + " 		else   b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ  end , "
                    strSQL = strSQL + "  签约面积=case when a.C_ZZ_YSMJ_LJ is null or a.C_ZZ_YSMJ_LJ=0 then b.C_ZZ_YSMJ_LJ"
                    strSQL = strSQL + "   		when b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else    b.C_ZZ_YSMJ_LJ-a.C_ZZ_YSMJ_LJ  end ,"
                    strSQL = strSQL + " 套数核对=case when a.C_ZZ_YSTS_LJ is null or a.C_ZZ_TS_WS is null or a.C_ZZ_YSTS_LJ=0 or a.C_ZZ_TS_WS=0  then b.C_ZZ_YSTS_LJ+b.C_ZZ_TS_WS"
                    strSQL = strSQL + "   		when b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else b.C_ZZ_YSTS_LJ+b.C_ZZ_TS_WS-a.C_ZZ_YSTS_LJ-a.C_ZZ_TS_WS  end ,"
                    strSQL = strSQL + "  面积核对=case when a.C_ZZ_YSMJ_LJ is null or a.C_ZZ_MJ_WS is null or a.C_ZZ_YSMJ_LJ=0 or a.C_ZZ_MJ_WS=0 then b.C_ZZ_YSMJ_LJ+b.C_ZZ_MJ_WS"
                    strSQL = strSQL + "   		when b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else  b.C_ZZ_YSMJ_LJ+b.C_ZZ_MJ_WS-a.C_ZZ_YSMJ_LJ-a.C_ZZ_MJ_WS  end ,"
                    strSQL = strSQL + "  签约总额=case when a.C_ZZ_YSMJ_LJ is null or a.C_ZZ_JJ_LJ is null  then b.C_ZZ_YSMJ_LJ*b.C_ZZ_JJ_LJ"
                    strSQL = strSQL + "   		when a.C_ZZ_YSMJ_LJ=0 or a.C_ZZ_JJ_LJ=0  then b.C_ZZ_YSMJ_LJ*b.C_ZZ_JJ_LJ"
                    strSQL = strSQL + " 		when b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else   b.C_ZZ_YSMJ_LJ*b.C_ZZ_JJ_LJ-a.C_ZZ_YSMJ_LJ*a.C_ZZ_JJ_LJ  end , "
                    strSQL = strSQL + "  case when b.C_ZZ_YSMJ_LJ-a.C_ZZ_YSMJ_LJ=0 or a.C_ZZ_YSMJ_LJ=0 or a.C_ZZ_YSMJ_LJ is null then 0 "
                    strSQL = strSQL + " else (b.C_ZZ_YSMJ_LJ*b.C_ZZ_JJ_LJ-a.C_ZZ_YSMJ_LJ*a.C_ZZ_JJ_LJ)/(b.C_ZZ_YSMJ_LJ-a.C_ZZ_YSMJ_LJ) end as 签约均价,"
                    strSQL = strSQL + "    b.C_ZZ_YSMJ_LJ * b.C_ZZ_JJ_LJ as 累计签约总额   "
                    strSQL = strSQL + "  from "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select b.*,c.c_house,c.c_type from T_HOUSE_INFO b left join T_HOUSE_MATCH_XMID c on b.C_XM_ID=c.C_XM_ID and b.C_XM_NAME=c.C_XM_NAME"
                    strSQL = strSQL + "  where b.C_TIME  between @endDate_start and @endDate_end "
                    strSQL = strSQL + "  )b"
                    strSQL = strSQL + "  Left Join "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select a.*,c.c_house,c.c_type from T_HOUSE_INFO a left join T_HOUSE_MATCH_XMID c on a.C_XM_ID=c.C_XM_ID and a.C_XM_NAME=c.C_XM_NAME"
                    strSQL = strSQL + "  where a.C_TIME between @startDate_start and @startDate_end "
                    strSQL = strSQL + "  )a  on  a.C_XM_ID=b.C_XM_ID and a.C_XM_NAME=b.C_XM_NAME and a.C_XZQH=b.C_XZQH and a.C_XM_ADDRESS=b.C_XM_ADDRESS and a.c_type=b.c_type"
                    strSQL = strSQL + "   ) A "
                    If strWhere <> "" Then
                        strSQL = strSQL + "where " + strWhere
                    End If
                    strSQL = strSQL + " group by A.行政区域, A.楼盘名称, A.房屋类型 "
                    strSQL = strSQL + " )a left join (select * from T_HOUSE_MATCH_SORT where i_type=convert(integer,'" + strType + "'))b on b.c_NAME=a.楼盘名称  "

                Case "3"
                    strSQL = strSQL + " select a.*,isnull(b.i_sort,100000) as '楼盘排序' from "
                    strSQL = strSQL + " (	"
                    strSQL = strSQL + " select "
                    strSQL = strSQL + " A.行政区域 as 行政区域, "
                    strSQL = strSQL + " A.楼盘名称 as 楼盘名称, "
                    strSQL = strSQL + " A.项目类型 as 项目类型, "
                    strSQL = strSQL + " sum(A.签约套数) as 网签数, "
                    strSQL = strSQL + " case when sum(A.签约套数) = 0 then 0 else cast(round(sum(A.签约总额)/sum(A.签约面积),2) as numeric(16,2)) end as 网签均价, "
                    strSQL = strSQL + " sum(A.累计已售套数2) as 合共成交, "
                    strSQL = strSQL + " sum(A.未售套数2) as 未售套数, "
                    strSQL = strSQL + " cast(round(sum(A.签约总额),2) as numeric(16,2)) as 网签总额, "
                    strSQL = strSQL + " cast(round(sum(A.签约面积),2) as numeric(16,2)) as 网签面积 "
                    strSQL = strSQL + " from( "
                    strSQL = strSQL + "  select "
                    strSQL = strSQL + " b.C_ID as 序号,  "
                    strSQL = strSQL + " b.C_XZQH as 行政区域,   "
                    strSQL = strSQL + " b.C_HOUSE as 楼盘名称,  "
                    strSQL = strSQL + " b.C_XM_NAME as 项目名称,  "
                    strSQL = strSQL + " b.C_XM_ID as 预售证,    "
                    strSQL = strSQL + " '办公' as 项目类型, "
                    strSQL = strSQL + " b.C_BG_YSTS_LJ as 累计预售套数1,  "
                    strSQL = strSQL + " b.C_BG_YSMJ_LJ as 累计预售面积1, "
                    strSQL = strSQL + " b.C_BG_JJ_LJ as 累计均价1,  "
                    strSQL = strSQL + " b.C_BG_TS_WS as 未售套数1,  "
                    strSQL = strSQL + " b.C_BG_MJ_WS as 未售面积1,  "
                    strSQL = strSQL + " b.C_BG_YSTS_LJ as 累计已售套数2,   "
                    strSQL = strSQL + " b.C_BG_YSMJ_LJ as 累计已售面积2,  "
                    strSQL = strSQL + " b.C_BG_JJ_LJ as 累计均价2, "
                    strSQL = strSQL + " b.C_BG_TS_WS as 未售套数2,   "
                    strSQL = strSQL + " b.C_BG_MJ_WS as 未售面积2,  "
                    strSQL = strSQL + " 签约套数=case 	when a.C_BG_YSTS_LJ is null then b.C_BG_YSTS_LJ"
                    strSQL = strSQL + "   		when a.C_BG_YSTS_LJ=0 then  b.C_BG_YSTS_LJ"
                    strSQL = strSQL + " 		else   b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ  end , "
                    strSQL = strSQL + "  签约面积=case when a.C_BG_YSMJ_LJ is null or a.C_BG_YSMJ_LJ=0 then b.C_BG_YSMJ_LJ"
                    strSQL = strSQL + "   		when b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else    b.C_BG_YSMJ_LJ-a.C_BG_YSMJ_LJ  end ,"
                    strSQL = strSQL + " 套数核对=case when a.C_BG_YSTS_LJ is null or a.C_BG_TS_WS is null or a.C_BG_YSTS_LJ=0 or a.C_BG_TS_WS=0  then b.C_BG_YSTS_LJ+b.C_BG_TS_WS"
                    strSQL = strSQL + "   		when b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else b.C_BG_YSTS_LJ+b.C_BG_TS_WS-a.C_BG_YSTS_LJ-a.C_BG_TS_WS  end ,"
                    strSQL = strSQL + "  面积核对=case when a.C_BG_YSMJ_LJ is null or a.C_BG_MJ_WS is null or a.C_BG_YSMJ_LJ=0 or a.C_BG_MJ_WS=0 then b.C_BG_YSMJ_LJ+b.C_BG_MJ_WS"
                    strSQL = strSQL + "   		when b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else  b.C_BG_YSMJ_LJ+b.C_BG_MJ_WS-a.C_BG_YSMJ_LJ-a.C_BG_MJ_WS  end ,"
                    strSQL = strSQL + "  签约总额=case when a.C_BG_YSMJ_LJ is null or a.C_BG_JJ_LJ is null  then b.C_BG_YSMJ_LJ*b.C_BG_JJ_LJ"
                    strSQL = strSQL + "   		when a.C_BG_YSMJ_LJ=0 or a.C_BG_JJ_LJ=0  then b.C_BG_YSMJ_LJ*b.C_BG_JJ_LJ"
                    strSQL = strSQL + " 		when b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else   b.C_BG_YSMJ_LJ*b.C_BG_JJ_LJ-a.C_BG_YSMJ_LJ*a.C_BG_JJ_LJ  end , "
                    strSQL = strSQL + "  case when b.C_BG_YSMJ_LJ-a.C_BG_YSMJ_LJ=0 or a.C_BG_YSMJ_LJ=0 or a.C_BG_YSMJ_LJ is null then 0 "
                    strSQL = strSQL + " else (b.C_BG_YSMJ_LJ*b.C_BG_JJ_LJ-a.C_BG_YSMJ_LJ*a.C_BG_JJ_LJ)/(b.C_BG_YSMJ_LJ-a.C_BG_YSMJ_LJ) end as 签约均价,"
                    strSQL = strSQL + "    b.C_BG_YSMJ_LJ * b.C_BG_JJ_LJ as 累计签约总额   "
                    strSQL = strSQL + "  from "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select b.*,c.c_house from T_HOUSE_INFO b left join T_HOUSE_MATCH_XMID c on b.C_XM_NAME=c.C_XM_NAME and b.C_XM_ID=c.C_XM_ID  "
                    strSQL = strSQL + "  where b.C_TIME  between @endDate_start and @endDate_end "
                    strSQL = strSQL + "  )b"
                    strSQL = strSQL + "  Left Join "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select a.*,c.c_house from T_HOUSE_INFO a left join T_HOUSE_MATCH_XMID c on a.C_XM_NAME=c.C_XM_NAME and a.C_XM_ID=c.C_XM_ID "
                    strSQL = strSQL + "  where a.C_TIME between @startDate_start and @startDate_end "
                    strSQL = strSQL + "  )a  on  a.C_XM_ID=b.C_XM_ID and a.C_XM_NAME=b.C_XM_NAME and a.C_XZQH=b.C_XZQH and a.C_XM_ADDRESS=b.C_XM_ADDRESS"
                    strSQL = strSQL + "  ) A "
                    If strWhere <> "" Then
                        strSQL = strSQL + "where " + strWhere
                    End If
                    strSQL = strSQL + " group by A.行政区域, A.楼盘名称, A.项目类型"
                    strSQL = strSQL + " )a left join (select * from T_HOUSE_MATCH_SORT where i_type=convert(integer,'" + strType + "')) b on b.c_NAME=a.楼盘名称 "
                Case "1"
                    strSQL = strSQL + " select a.*,isnull(b.i_sort,100000) as '楼盘排序' from "
                    strSQL = strSQL + " (	"
                    strSQL = strSQL + " select  "
                    strSQL = strSQL + " A.行政区域 as 行政区域, "
                    strSQL = strSQL + " A.楼盘名称 as 楼盘名称, "
                    strSQL = strSQL + " A.项目类型 as 项目类型, "
                    strSQL = strSQL + " sum(A.签约套数) as 网签数, "
                    strSQL = strSQL + " case when sum(A.签约套数) = 0 then 0 else cast(round(sum(A.签约总额)/sum(A.签约面积),2) as numeric(16,2)) end as 网签均价, "
                    strSQL = strSQL + " sum(A.累计已售套数2) as 合共成交, "
                    strSQL = strSQL + " sum(A.未售套数2) as 未售套数, "
                    strSQL = strSQL + " cast(round(sum(A.签约总额),2) as numeric(16,2)) as 网签总额, "
                    strSQL = strSQL + " cast(round(sum(A.签约面积),2) as numeric(16,2)) as 网签面积 "
                    strSQL = strSQL + " from( "
                    strSQL = strSQL + "  select "
                    strSQL = strSQL + " b.C_ID as 序号,   "
                    strSQL = strSQL + " b.C_XZQH as 行政区域,   "
                    strSQL = strSQL + " b.C_HOUSE as 楼盘名称,  "
                    strSQL = strSQL + " b.C_XM_NAME as 项目名称,  "
                    strSQL = strSQL + " b.C_XM_ID as 预售证,    "
                    strSQL = strSQL + " '商业' as 项目类型, "
                    strSQL = strSQL + " b.C_SY_YSTS_LJ as 累计预售套数1,  "
                    strSQL = strSQL + " b.C_SY_YSMJ_LJ as 累计预售面积1, "
                    strSQL = strSQL + " b.C_SY_JJ_LJ as 累计均价1,  "
                    strSQL = strSQL + " b.C_SY_TS_WS as 未售套数1,  "
                    strSQL = strSQL + " b.C_SY_MJ_WS as 未售面积1,  "
                    strSQL = strSQL + " b.C_SY_YSTS_LJ as 累计已售套数2,   "
                    strSQL = strSQL + " b.C_SY_YSMJ_LJ as 累计已售面积2,  "
                    strSQL = strSQL + " b.C_SY_JJ_LJ as 累计均价2, "
                    strSQL = strSQL + " b.C_SY_TS_WS as 未售套数2,   "
                    strSQL = strSQL + " b.C_SY_MJ_WS as 未售面积2,  "
                    strSQL = strSQL + " 签约套数=case 	when a.C_SY_YSTS_LJ is null then b.C_SY_YSTS_LJ"
                    strSQL = strSQL + "   		when a.C_SY_YSTS_LJ=0 then  b.C_SY_YSTS_LJ"
                    strSQL = strSQL + " 		else   b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ  end , "
                    strSQL = strSQL + "  签约面积=case when a.C_SY_YSMJ_LJ is null or a.C_SY_YSMJ_LJ=0 then b.C_SY_YSMJ_LJ"
                    strSQL = strSQL + "   		when b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else    b.C_SY_YSMJ_LJ-a.C_SY_YSMJ_LJ  end ,"
                    strSQL = strSQL + " 套数核对=case when a.C_SY_YSTS_LJ is null or a.C_SY_TS_WS is null or a.C_SY_YSTS_LJ=0 or a.C_SY_TS_WS=0  then b.C_SY_YSTS_LJ+b.C_SY_TS_WS"
                    strSQL = strSQL + "   		when b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else b.C_SY_YSTS_LJ+b.C_SY_TS_WS-a.C_SY_YSTS_LJ-a.C_SY_TS_WS  end ,"
                    strSQL = strSQL + "  面积核对=case when a.C_SY_YSMJ_LJ is null or a.C_SY_MJ_WS is null or a.C_SY_YSMJ_LJ=0 or a.C_SY_MJ_WS=0 then b.C_SY_YSMJ_LJ+b.C_SY_MJ_WS"
                    strSQL = strSQL + "   		when b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else  b.C_SY_YSMJ_LJ+b.C_SY_MJ_WS-a.C_SY_YSMJ_LJ-a.C_SY_MJ_WS  end ,"
                    strSQL = strSQL + "  签约总额=case when a.C_SY_YSMJ_LJ is null or a.C_SY_JJ_LJ is null  then b.C_SY_YSMJ_LJ*b.C_SY_JJ_LJ"
                    strSQL = strSQL + "   		when a.C_SY_YSMJ_LJ=0 or a.C_SY_JJ_LJ=0  then b.C_SY_YSMJ_LJ*b.C_SY_JJ_LJ"
                    strSQL = strSQL + " 		when b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else   b.C_SY_YSMJ_LJ*b.C_SY_JJ_LJ-a.C_SY_YSMJ_LJ*a.C_SY_JJ_LJ  end , "
                    strSQL = strSQL + "  case when b.C_SY_YSMJ_LJ-a.C_SY_YSMJ_LJ=0 or a.C_SY_YSMJ_LJ=0 or a.C_SY_YSMJ_LJ is null then 0 "
                    strSQL = strSQL + " else (b.C_SY_YSMJ_LJ*b.C_SY_JJ_LJ-a.C_SY_YSMJ_LJ*a.C_SY_JJ_LJ)/(b.C_SY_YSMJ_LJ-a.C_SY_YSMJ_LJ) end as 签约均价,"
                    strSQL = strSQL + "    b.C_SY_YSMJ_LJ * b.C_SY_JJ_LJ as 累计签约总额   "
                    strSQL = strSQL + "  from "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select b.*,c.c_house from T_HOUSE_INFO b left join T_HOUSE_MATCH_XMID c on b.C_XM_NAME=c.C_XM_NAME and b.C_XM_ID=c.C_XM_ID  "
                    strSQL = strSQL + "  where b.C_TIME  between @endDate_start and @endDate_end "
                    strSQL = strSQL + "  )b"
                    strSQL = strSQL + "  Left Join "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select a.*,c.c_house from T_HOUSE_INFO a left join T_HOUSE_MATCH_XMID c on a.C_XM_NAME=c.C_XM_NAME and a.C_XM_ID=c.C_XM_ID "
                    strSQL = strSQL + "  where a.C_TIME between @startDate_start and @startDate_end "
                    strSQL = strSQL + "  )a  on  a.C_XM_ID=b.C_XM_ID and a.C_XM_NAME=b.C_XM_NAME and a.C_XZQH=b.C_XZQH and a.C_XM_ADDRESS=b.C_XM_ADDRESS"
                    strSQL = strSQL + "  ) A "
                    If strWhere <> "" Then
                        strSQL = strSQL + "where " + strWhere
                    End If
                    strSQL = strSQL + " group by A.行政区域, A.楼盘名称, A.项目类型"
                    strSQL = strSQL + " )a left join (select * from T_HOUSE_MATCH_SORT where i_type=convert(integer,'" + strType + "')) b on b.c_NAME=a.楼盘名称 "
                Case Else

            End Select

            getSql_BuildingCompute_x2 = True
errProc:

            Exit Function
        End Function


        '----------------------------------------------------------------
        ' 根据“查询条件”获取“阳光家缘楼盘信息数据分析”完全数据的数据集
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strSQL                     ：返回的SQL语句
        '     strType                    ：选择楼盘的类型。0-全部；1-商业；2-住宅；3-办公
        '     strWhere                   ：搜索字符串      
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getSql_BuildingCompute_x3( _
             ByRef strErrMsg As String, _
             ByRef strSQL As String, _
             ByVal strWhere As String, _
             ByRef strType As String) As Boolean

            getSql_BuildingCompute_x3 = False

            strSQL = ""
            Select Case strType

                Case "2"
                    strSQL = strSQL + "  select a.*,isnull(b.i_sort,100000) as '楼盘排序' from "
                    strSQL = strSQL + "  (	"
                    strSQL = strSQL + "     select b.行政区域,b.楼盘名称,case when b.房屋类型='1' then '别墅' else '洋房' end as 项目类型, "
                    strSQL = strSQL + "        a.累计已售套数 as 累计已售套数1,  "
                    strSQL = strSQL + "        a.累计已售面积 as 累计已售面积1, "
                    strSQL = strSQL + "        a.未售套数 as 未售套数1,  "
                    strSQL = strSQL + "        a.未售面积 as 未售面积1,  "
                    strSQL = strSQL + "        b.累计已售套数 as 合共成交,   "
                    strSQL = strSQL + "        b.累计已售面积 as 累计已售面积, "
                    strSQL = strSQL + "        b.未售套数 as 未售套数,   "
                    strSQL = strSQL + "        b.未售面积 as 未售面积,  "
                    strSQL = strSQL + "        网签数=b.累计已售套数-isnull(a.累计已售套数,0), "
                    strSQL = strSQL + "        网签面积=case when b.累计已售套数-isnull(a.累计已售套数,0)=0 then 0 else  cast(round(b.累计已售面积-isnull(a.累计已售面积,0),2) as numeric(16,2)) end, "
                    strSQL = strSQL + "        网签总额=case when b.累计已售套数-isnull(a.累计已售套数,0)=0 then 0 else cast(round(b.签约总额-isnull(a.签约总额,0),2) as numeric(16,2)) end, "
                    'strSQL = strSQL + "        签约均价=case when b.累计已售面积-a.累计已售面积=0 or b.累计已售套数-a.累计已售套数=0 then 0 else  cast(round((b.签约总额-a.签约总额)/(b.累计已售面积-a.累计已售面积),2) as numeric(16,2))  end,"
                    'strSQL = strSQL + "        签约均价=dbo.Sunshine_F_getAveragePrice_nomatch(b.楼盘名称,@startDate_start,@endDate_start,b.房屋类型),"
                    strSQL = strSQL + "        签约均价=case when b.累计已售面积-a.累计已售面积=0 or b.累计已售套数-a.累计已售套数=0 then dbo.Sunshine_F_getAveragePrice_nomatch(b.楼盘名称,@startDate_start,@endDate_start,b.房屋类型) else  cast(round((b.签约总额-a.签约总额)/(b.累计已售面积-a.累计已售面积),2) as numeric(16,2))  end,"
                    strSQL = strSQL + "        累计均价=case when b.累计已售面积=0 or b.累计已售套数=0 then 0 else  cast(round((b.签约总额)/(b.累计已售面积),2) as numeric(16,2))  end"
                    strSQL = strSQL + "         from "
                    strSQL = strSQL + "         ( "
                    strSQL = strSQL + "         select  C_XZQH as 行政区域,C_HOUSE as 楼盘名称, isnull(c_type,0) as 房屋类型, "
                    strSQL = strSQL + "               sum(C_ZZ_YSTS_LJ) as 累计已售套数,   "
                    strSQL = strSQL + "               sum(C_ZZ_YSMJ_LJ) as 累计已售面积, "
                    strSQL = strSQL + "               sum(C_ZZ_TS_WS) as 未售套数,   "
                    strSQL = strSQL + "               sum(C_ZZ_MJ_WS) as 未售面积,"
                    strSQL = strSQL + "               sum(C_ZZ_YSMJ_LJ*C_ZZ_JJ_LJ) as 签约总额 from "
                    strSQL = strSQL + "                   ("
                    strSQL = strSQL + "                   select a.*,c.c_house,c.c_type from T_HOUSE_INFO a left join T_HOUSE_MATCH_XMID c on a.C_XM_NAME=c.C_XM_NAME and a.C_XM_ID=c.C_XM_ID"
                    strSQL = strSQL + "                     where a.C_TIME  between @endDate_start and @endDate_end   "
                    strSQL = strSQL + "                   )a  group by C_XZQH,C_HOUSE,c_type  "
                    strSQL = strSQL + "          )b "
                    strSQL = strSQL + "          left  Join "
                    strSQL = strSQL + "         ("
                    strSQL = strSQL + "             select  a.C_XZQH as 行政区域,a.C_HOUSE as 楼盘名称, isnull(c_type,0) as 房屋类型, "
                    strSQL = strSQL + "                 sum(C_ZZ_YSTS_LJ) as 累计已售套数,   "
                    strSQL = strSQL + "                 sum(C_ZZ_YSMJ_LJ) as 累计已售面积, "
                    strSQL = strSQL + "                 sum(C_ZZ_TS_WS) as 未售套数,   "
                    strSQL = strSQL + "                 sum(C_ZZ_MJ_WS) as 未售面积,"
                    strSQL = strSQL + "                 sum(C_ZZ_YSMJ_LJ*C_ZZ_JJ_LJ) as 签约总额 from"
                    strSQL = strSQL + "                   ("
                    strSQL = strSQL + "                   select a.*,c.c_house,c.c_type from T_HOUSE_INFO a left join T_HOUSE_MATCH_XMID c on a.C_XM_NAME=c.C_XM_NAME and a.C_XM_ID=c.C_XM_ID "
                    strSQL = strSQL + "                    where a.C_TIME between @startDate_start and @startDate_end  "
                    strSQL = strSQL + "                   )a  group by C_XZQH,C_HOUSE,c_type  "
                    strSQL = strSQL + "         )a on b.楼盘名称=a.楼盘名称 and b.房屋类型=a.房屋类型 "
                    strSQL = strSQL + "     ) A left join (select * from T_HOUSE_MATCH_SORT where i_type=convert(integer,'" + strType + "')) b on b.c_NAME=a.楼盘名称 "
                    If strWhere <> "" Then
                        strSQL = strSQL + "where " + strWhere
                    End If

                Case "3"
                    strSQL = strSQL + "  select a.*,isnull(b.i_sort,100000) as '楼盘排序' from "
                    strSQL = strSQL + "  (	"
                    strSQL = strSQL + "     select b.行政区域,b.楼盘名称,b.项目类型, "
                    strSQL = strSQL + "        a.累计已售套数 as 累计已售套数1,  "
                    strSQL = strSQL + "        a.累计已售面积 as 累计已售面积1, "
                    strSQL = strSQL + "        a.未售套数 as 未售套数1,  "
                    strSQL = strSQL + "        a.未售面积 as 未售面积1,  "
                    strSQL = strSQL + "        b.累计已售套数 as 合共成交,   "
                    strSQL = strSQL + "        b.累计已售面积 as 累计已售面积, "
                    strSQL = strSQL + "        b.未售套数 as 未售套数,   "
                    strSQL = strSQL + "        b.未售面积 as 未售面积,  "
                    strSQL = strSQL + "        网签数=b.累计已售套数-isnull(a.累计已售套数,0), "
                    strSQL = strSQL + "        网签面积=case when b.累计已售套数-isnull(a.累计已售套数,0)=0 then 0 else  cast(round(b.累计已售面积-isnull(a.累计已售面积,0),2) as numeric(16,2)) end, "
                    strSQL = strSQL + "        网签总额=case when b.累计已售套数-isnull(a.累计已售套数,0)=0 then 0 else cast(round(b.签约总额-isnull(a.签约总额,0),2) as numeric(16,2)) end, "
                    'strSQL = strSQL + "        签约均价=case when b.累计已售面积-a.累计已售面积=0 or b.累计已售套数-a.累计已售套数=0 then 0 else  cast(round((b.签约总额-a.签约总额)/(b.累计已售面积-a.累计已售面积),2) as numeric(16,2))  end,"
                    'strSQL = strSQL + "        签约均价=dbo.Sunshine_F_getAveragePrice_nomatch_BG(b.楼盘名称,@startDate_start,@endDate_start,0),"
                    strSQL = strSQL + "        签约均价=case when b.累计已售面积-a.累计已售面积=0 or b.累计已售套数-a.累计已售套数=0 then dbo.Sunshine_F_getAveragePrice_nomatch_BG(b.楼盘名称,@startDate_start,@endDate_start,0) else  cast(round((b.签约总额-a.签约总额)/(b.累计已售面积-a.累计已售面积),2) as numeric(16,2))  end,"
                    strSQL = strSQL + "        累计均价=case when b.累计已售面积=0 or b.累计已售套数=0 then 0 else  cast(round((b.签约总额)/(b.累计已售面积),2) as numeric(16,2))  end"
                    strSQL = strSQL + "         from "
                    strSQL = strSQL + "         ( "
                    strSQL = strSQL + "         select  C_XZQH as 行政区域,C_HOUSE as 楼盘名称, '办公' as 项目类型, "
                    strSQL = strSQL + "               sum(C_BG_YSTS_LJ) as 累计已售套数,   "
                    strSQL = strSQL + "               sum(C_BG_YSMJ_LJ) as 累计已售面积, "
                    strSQL = strSQL + "               sum(C_BG_TS_WS) as 未售套数,   "
                    strSQL = strSQL + "               sum(C_BG_MJ_WS) as 未售面积,"
                    strSQL = strSQL + "               sum(C_BG_YSMJ_LJ*C_BG_JJ_LJ) as 签约总额 from "
                    strSQL = strSQL + "                   ("
                    strSQL = strSQL + "                   select a.*,c.c_house from T_HOUSE_INFO a left join T_HOUSE_MATCH_XMID c on a.C_XM_NAME=c.C_XM_NAME and a.C_XM_ID=c.C_XM_ID"
                    strSQL = strSQL + "                     where a.C_TIME  between @endDate_start and @endDate_end   "
                    strSQL = strSQL + "                   )a  group by C_XZQH,C_HOUSE  "
                    strSQL = strSQL + "          )b "
                    strSQL = strSQL + "          left  Join "
                    strSQL = strSQL + "         ("
                    strSQL = strSQL + "             select  a.C_XZQH as 行政区域,a.C_HOUSE as 楼盘名称, '办公' as 项目类型, "
                    strSQL = strSQL + "                 sum(C_BG_YSTS_LJ) as 累计已售套数,   "
                    strSQL = strSQL + "                 sum(C_BG_YSMJ_LJ) as 累计已售面积, "
                    strSQL = strSQL + "                 sum(C_BG_TS_WS) as 未售套数,   "
                    strSQL = strSQL + "                 sum(C_BG_MJ_WS) as 未售面积,"
                    strSQL = strSQL + "                 sum(C_BG_YSMJ_LJ*C_BG_JJ_LJ) as 签约总额 from"
                    strSQL = strSQL + "                   ("
                    strSQL = strSQL + "                   select a.*,c.c_house from T_HOUSE_INFO a left join T_HOUSE_MATCH_XMID c on a.C_XM_NAME=c.C_XM_NAME and a.C_XM_ID=c.C_XM_ID "
                    strSQL = strSQL + "                    where a.C_TIME between @startDate_start and @startDate_end  "
                    strSQL = strSQL + "                   )a  group by C_XZQH,C_HOUSE  "
                    strSQL = strSQL + "         )a on b.楼盘名称=a.楼盘名称 "
                    strSQL = strSQL + "     ) A left join (select * from T_HOUSE_MATCH_SORT where i_type=convert(integer,'" + strType + "')) b on b.c_NAME=a.楼盘名称 "
                    If strWhere <> "" Then
                        strSQL = strSQL + "where " + strWhere
                    End If
                Case "1"
                    strSQL = strSQL + "  select a.*,isnull(b.i_sort,100000) as '楼盘排序' from "
                    strSQL = strSQL + "  (	"
                    strSQL = strSQL + "     select b.行政区域,b.楼盘名称,b.项目类型, "
                    strSQL = strSQL + "        a.累计已售套数 as 累计已售套数1,  "
                    strSQL = strSQL + "        a.累计已售面积 as 累计已售面积1, "
                    strSQL = strSQL + "        a.未售套数 as 未售套数1,  "
                    strSQL = strSQL + "        a.未售面积 as 未售面积1,  "
                    strSQL = strSQL + "        b.累计已售套数 as 合共成交,   "
                    strSQL = strSQL + "        b.累计已售面积 as 累计已售面积, "
                    strSQL = strSQL + "        b.未售套数 as 未售套数,   "
                    strSQL = strSQL + "        b.未售面积 as 未售面积,  "
                    strSQL = strSQL + "        网签数=b.累计已售套数-isnull(a.累计已售套数,0), "
                    strSQL = strSQL + "        网签面积=case when b.累计已售套数-isnull(a.累计已售套数,0)=0 then 0 else  cast(round(b.累计已售面积-isnull(a.累计已售面积,0),2) as numeric(16,2)) end, "
                    strSQL = strSQL + "        网签总额=case when b.累计已售套数-isnull(a.累计已售套数,0)=0 then 0 else cast(round(b.签约总额-isnull(a.签约总额,0),2) as numeric(16,2)) end, "
                    'strSQL = strSQL + "        签约均价=case when b.累计已售面积-a.累计已售面积=0 or b.累计已售套数-a.累计已售套数=0 then 0 else  cast(round((b.签约总额-a.签约总额)/(b.累计已售面积-a.累计已售面积),2) as numeric(16,2))  end,"
                    'strSQL = strSQL + "        签约均价=dbo.Sunshine_F_getAveragePrice_nomatch_SY(b.楼盘名称,@startDate_start,@endDate_start,0),"
                    strSQL = strSQL + "        签约均价=case when b.累计已售面积-a.累计已售面积=0 or b.累计已售套数-a.累计已售套数=0 then dbo.Sunshine_F_getAveragePrice_nomatch_SY(b.楼盘名称,@startDate_start,@endDate_start,0) else  cast(round((b.签约总额-a.签约总额)/(b.累计已售面积-a.累计已售面积),2) as numeric(16,2))  end,"
                    strSQL = strSQL + "        累计均价=case when b.累计已售面积=0 or b.累计已售套数=0 then 0 else  cast(round((b.签约总额)/(b.累计已售面积),2) as numeric(16,2))  end"
                    strSQL = strSQL + "         from "
                    strSQL = strSQL + "         ( "
                    strSQL = strSQL + "         select  C_XZQH as 行政区域,C_HOUSE as 楼盘名称, '商业' as 项目类型, "
                    strSQL = strSQL + "               sum(C_SY_YSTS_LJ) as 累计已售套数,   "
                    strSQL = strSQL + "               sum(C_SY_YSMJ_LJ) as 累计已售面积, "
                    strSQL = strSQL + "               sum(C_SY_TS_WS) as 未售套数,   "
                    strSQL = strSQL + "               sum(C_SY_MJ_WS) as 未售面积,"
                    strSQL = strSQL + "               sum(C_SY_YSMJ_LJ*C_SY_JJ_LJ) as 签约总额 from "
                    strSQL = strSQL + "                   ("
                    strSQL = strSQL + "                   select a.*,c.c_house from T_HOUSE_INFO a left join T_HOUSE_MATCH_XMID c on a.C_XM_NAME=c.C_XM_NAME and a.C_XM_ID=c.C_XM_ID"
                    strSQL = strSQL + "                     where a.C_TIME  between @endDate_start and @endDate_end   "
                    strSQL = strSQL + "                   )a  group by C_XZQH,C_HOUSE  "
                    strSQL = strSQL + "          )b "
                    strSQL = strSQL + "          left  Join "
                    strSQL = strSQL + "         ("
                    strSQL = strSQL + "             select  a.C_XZQH as 行政区域,a.C_HOUSE as 楼盘名称, '商业' as 项目类型, "
                    strSQL = strSQL + "                 sum(C_SY_YSTS_LJ) as 累计已售套数,   "
                    strSQL = strSQL + "                 sum(C_SY_YSMJ_LJ) as 累计已售面积, "
                    strSQL = strSQL + "                 sum(C_SY_TS_WS) as 未售套数,   "
                    strSQL = strSQL + "                 sum(C_SY_MJ_WS) as 未售面积,"
                    strSQL = strSQL + "                 sum(C_SY_YSMJ_LJ*C_SY_JJ_LJ) as 签约总额 from"
                    strSQL = strSQL + "                   ("
                    strSQL = strSQL + "                   select a.*,c.c_house from T_HOUSE_INFO a left join T_HOUSE_MATCH_XMID c on a.C_XM_NAME=c.C_XM_NAME and a.C_XM_ID=c.C_XM_ID "
                    strSQL = strSQL + "                    where a.C_TIME between @startDate_start and @startDate_end  "
                    strSQL = strSQL + "                   )a  group by C_XZQH,C_HOUSE  "
                    strSQL = strSQL + "         )a on b.楼盘名称=a.楼盘名称 "
                    strSQL = strSQL + "     ) A left join (select * from T_HOUSE_MATCH_SORT where i_type=convert(integer,'" + strType + "')) b on b.c_NAME=a.楼盘名称 "
                    If strWhere <> "" Then
                        strSQL = strSQL + "where " + strWhere
                    End If
                Case Else

            End Select

            getSql_BuildingCompute_x3 = True
errProc:

            Exit Function
        End Function



        '----------------------------------------------------------------
        ' 根据“查询条件”获取“阳光家缘楼盘信息数据分析”完全数据的数据集
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strSQL                     ：返回的SQL语句
        '     strType                    ：选择楼盘的类型。0-全部；1-商业；2-住宅；3-办公
        '     strWhere                   ：搜索字符串      
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getSql_BuildingCompute_XMID( _
             ByRef strErrMsg As String, _
             ByRef strSQL As String, _
             ByVal strWhere As String, _
             ByRef strType As String) As Boolean

            getSql_BuildingCompute_XMID = False

            strSQL = ""
            Select Case strType
                Case "0"
                    strSQL = strSQL + " select * from ("
                    strSQL = strSQL + " select "
                    strSQL = strSQL + " A.行政区域 as 行政区域, "
                    strSQL = strSQL + " A.楼盘名称 as 楼盘名称, "
                    strSQL = strSQL + " A.项目类型 as 项目类型, "
                    strSQL = strSQL + " sum(A.签约套数) as 网签数, "
                    strSQL = strSQL + "  case when sum(A.签约套数) = 0 then 0 else cast(round(sum(A.签约总额)/sum(A.签约面积),2) as numeric(16,2)) end as 网签均价, "
                    strSQL = strSQL + "  sum(A.累计已售套数2) as 合共成交, "
                    strSQL = strSQL + "  sum(A.未售套数2) as 未售套数, "
                    strSQL = strSQL + "  cast(round(sum(A.签约总额),2) as numeric(16,2)) as 网签总额, "
                    strSQL = strSQL + "  cast(round(sum(A.签约面积),2) as numeric(16,2)) as 网签面积 "
                    strSQL = strSQL + "  from( "
                    strSQL = strSQL + "  select "
                    strSQL = strSQL + " b.C_ID as 序号,  "
                    strSQL = strSQL + " b.C_XZQH as 行政区域,   "
                    strSQL = strSQL + " b.C_HOUSE as 楼盘名称,  "
                    strSQL = strSQL + " b.C_XM_NAME as 项目名称,  "
                    strSQL = strSQL + " b.C_XM_ID as 预售证,    "
                    strSQL = strSQL + " '住宅' as 项目类型, "
                    strSQL = strSQL + " b.C_ZZ_YSTS_LJ as 累计预售套数1,  "
                    strSQL = strSQL + " b.C_ZZ_YSMJ_LJ as 累计预售面积1, "
                    strSQL = strSQL + " b.C_ZZ_JJ_LJ as 累计均价1,  "
                    strSQL = strSQL + " b.C_ZZ_TS_WS as 未售套数1,  "
                    strSQL = strSQL + " b.C_ZZ_MJ_WS as 未售面积1,  "
                    strSQL = strSQL + " b.C_ZZ_YSTS_LJ as 累计已售套数2,   "
                    strSQL = strSQL + " b.C_ZZ_YSMJ_LJ as 累计已售面积2,  "
                    strSQL = strSQL + " b.C_ZZ_JJ_LJ as 累计均价2, "
                    strSQL = strSQL + " b.C_ZZ_TS_WS as 未售套数2,   "
                    strSQL = strSQL + " b.C_ZZ_MJ_WS as 未售面积2,  "
                    strSQL = strSQL + " 签约套数=case 	when a.C_ZZ_YSTS_LJ is null then b.C_ZZ_YSTS_LJ"
                    strSQL = strSQL + "   		when a.C_ZZ_YSTS_LJ=0 then  b.C_ZZ_YSTS_LJ"
                    strSQL = strSQL + " 		else   b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ  end , "
                    strSQL = strSQL + "  签约面积=case when a.C_ZZ_YSMJ_LJ is null or a.C_ZZ_YSMJ_LJ=0 then b.C_ZZ_YSMJ_LJ"
                    strSQL = strSQL + "   		when b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else    b.C_ZZ_YSMJ_LJ-a.C_ZZ_YSMJ_LJ  end ,"
                    strSQL = strSQL + " 套数核对=case when a.C_ZZ_YSTS_LJ is null or a.C_ZZ_TS_WS is null or a.C_ZZ_YSTS_LJ=0 or a.C_ZZ_TS_WS=0  then b.C_ZZ_YSTS_LJ+b.C_ZZ_TS_WS"
                    strSQL = strSQL + "   		when b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else b.C_ZZ_YSTS_LJ+b.C_ZZ_TS_WS-a.C_ZZ_YSTS_LJ-a.C_ZZ_TS_WS  end ,"
                    strSQL = strSQL + "  面积核对=case when a.C_ZZ_YSMJ_LJ is null or a.C_ZZ_MJ_WS is null or a.C_ZZ_YSMJ_LJ=0 or a.C_ZZ_MJ_WS=0 then b.C_ZZ_YSMJ_LJ+b.C_ZZ_MJ_WS"
                    strSQL = strSQL + "   		when b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else  b.C_ZZ_YSMJ_LJ+b.C_ZZ_MJ_WS-a.C_ZZ_YSMJ_LJ-a.C_ZZ_MJ_WS  end ,"
                    strSQL = strSQL + "  签约总额=case when a.C_ZZ_YSMJ_LJ is null or a.C_ZZ_JJ_LJ is null  then b.C_ZZ_YSMJ_LJ*b.C_ZZ_JJ_LJ"
                    strSQL = strSQL + "   		when a.C_ZZ_YSMJ_LJ=0 or a.C_ZZ_JJ_LJ=0  then b.C_ZZ_YSMJ_LJ*b.C_ZZ_JJ_LJ"
                    strSQL = strSQL + " 		when b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else   b.C_ZZ_YSMJ_LJ*b.C_ZZ_JJ_LJ-a.C_ZZ_YSMJ_LJ*a.C_ZZ_JJ_LJ  end , "
                    strSQL = strSQL + "  case when b.C_ZZ_YSMJ_LJ-a.C_ZZ_YSMJ_LJ=0 or a.C_ZZ_YSMJ_LJ=0 or a.C_ZZ_YSMJ_LJ is null then 0 "
                    strSQL = strSQL + " else (b.C_ZZ_YSMJ_LJ*b.C_ZZ_JJ_LJ-a.C_ZZ_YSMJ_LJ*a.C_ZZ_JJ_LJ)/(b.C_ZZ_YSMJ_LJ-a.C_ZZ_YSMJ_LJ) end as 签约均价,"
                    strSQL = strSQL + "    b.C_ZZ_YSMJ_LJ * b.C_ZZ_JJ_LJ as 累计签约总额   "
                    strSQL = strSQL + "  from "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select b.*,c.c_house from T_HOUSE_INFO b left join T_HOUSE_MATCH c on b.C_XM_NAME=c.C_XM_NAME and b.C_XZQH=c.C_XZQY  "
                    strSQL = strSQL + "  where b.C_TIME  between @endDate_start and @endDate_end "
                    strSQL = strSQL + "  )b"
                    strSQL = strSQL + "  Left Join "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select a.*,c.c_house from T_HOUSE_INFO a left join T_HOUSE_MATCH c on a.C_XM_NAME=c.C_XM_NAME and a.C_XZQH=c.C_XZQY "
                    strSQL = strSQL + "  where a.C_TIME between @startDate_start and @startDate_end "
                    strSQL = strSQL + "  )a  on  a.C_XM_ID=b.C_XM_ID and a.C_XM_NAME=b.C_XM_NAME and a.C_XZQH=b.C_XZQH and a.C_XM_ADDRESS=b.C_XM_ADDRESS"
                    strSQL = strSQL + "   ) A "
                    If strWhere <> "" Then
                        strSQL = strSQL + "where " + strWhere
                    End If
                    strSQL = strSQL + " group by A.行政区域, A.楼盘名称, A.项目类型 "
                    strSQL = strSQL + " union "
                    strSQL = strSQL + " select "
                    strSQL = strSQL + " A.行政区域 as 行政区域, "
                    strSQL = strSQL + " A.楼盘名称 as 楼盘名称, "
                    strSQL = strSQL + " A.项目类型 as 项目类型, "
                    strSQL = strSQL + " sum(A.签约套数) as 网签数, "
                    strSQL = strSQL + " case when sum(A.签约套数) = 0 then 0 else cast(round(sum(A.签约总额)/sum(A.签约面积),2) as numeric(16,2)) end as 网签均价, "
                    strSQL = strSQL + " sum(A.累计已售套数2) as 合共成交, "
                    strSQL = strSQL + " sum(A.未售套数2) as 未售套数, "
                    strSQL = strSQL + " cast(round(sum(A.签约总额),2) as numeric(16,2)) as 网签总额, "
                    strSQL = strSQL + " cast(round(sum(A.签约面积),2) as numeric(16,2)) as 网签面积 "
                    strSQL = strSQL + " from( "
                    strSQL = strSQL + "  select "
                    strSQL = strSQL + " b.C_ID as 序号, "
                    strSQL = strSQL + " b.C_XZQH as 行政区域,   "
                    strSQL = strSQL + " b.C_HOUSE as 楼盘名称,  "
                    strSQL = strSQL + " b.C_XM_NAME as 项目名称,  "
                    strSQL = strSQL + " b.C_XM_ID as 预售证,    "
                    strSQL = strSQL + " '办公' as 项目类型, "
                    strSQL = strSQL + " b.C_BG_YSTS_LJ as 累计预售套数1,  "
                    strSQL = strSQL + " b.C_BG_YSMJ_LJ as 累计预售面积1, "
                    strSQL = strSQL + " b.C_BG_JJ_LJ as 累计均价1,  "
                    strSQL = strSQL + " b.C_BG_TS_WS as 未售套数1,  "
                    strSQL = strSQL + " b.C_BG_MJ_WS as 未售面积1,  "
                    strSQL = strSQL + " b.C_BG_YSTS_LJ as 累计已售套数2,   "
                    strSQL = strSQL + " b.C_BG_YSMJ_LJ as 累计已售面积2,  "
                    strSQL = strSQL + " b.C_BG_JJ_LJ as 累计均价2, "
                    strSQL = strSQL + " b.C_BG_TS_WS as 未售套数2,   "
                    strSQL = strSQL + " b.C_BG_MJ_WS as 未售面积2,  "
                    strSQL = strSQL + " 签约套数=case 	when a.C_BG_YSTS_LJ is null then b.C_BG_YSTS_LJ"
                    strSQL = strSQL + "   		when a.C_BG_YSTS_LJ=0 then  b.C_BG_YSTS_LJ"
                    strSQL = strSQL + " 		else   b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ  end , "
                    strSQL = strSQL + "  签约面积=case when a.C_BG_YSMJ_LJ is null or a.C_BG_YSMJ_LJ=0 then b.C_BG_YSMJ_LJ"
                    strSQL = strSQL + "   		when b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else    b.C_BG_YSMJ_LJ-a.C_BG_YSMJ_LJ  end ,"
                    strSQL = strSQL + " 套数核对=case when a.C_BG_YSTS_LJ is null or a.C_BG_TS_WS is null or a.C_BG_YSTS_LJ=0 or a.C_BG_TS_WS=0  then b.C_BG_YSTS_LJ+b.C_BG_TS_WS"
                    strSQL = strSQL + "   		when b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else b.C_BG_YSTS_LJ+b.C_BG_TS_WS-a.C_BG_YSTS_LJ-a.C_BG_TS_WS  end ,"
                    strSQL = strSQL + "  面积核对=case when a.C_BG_YSMJ_LJ is null or a.C_BG_MJ_WS is null or a.C_BG_YSMJ_LJ=0 or a.C_BG_MJ_WS=0 then b.C_BG_YSMJ_LJ+b.C_BG_MJ_WS"
                    strSQL = strSQL + "   		when b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else  b.C_BG_YSMJ_LJ+b.C_BG_MJ_WS-a.C_BG_YSMJ_LJ-a.C_BG_MJ_WS  end ,"
                    strSQL = strSQL + "  签约总额=case when a.C_BG_YSMJ_LJ is null or a.C_BG_JJ_LJ is null  then b.C_BG_YSMJ_LJ*b.C_BG_JJ_LJ"
                    strSQL = strSQL + "   		when a.C_BG_YSMJ_LJ=0 or a.C_BG_JJ_LJ=0  then b.C_BG_YSMJ_LJ*b.C_BG_JJ_LJ"
                    strSQL = strSQL + " 		when b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else   b.C_BG_YSMJ_LJ*b.C_BG_JJ_LJ-a.C_BG_YSMJ_LJ*a.C_BG_JJ_LJ  end , "
                    strSQL = strSQL + "  case when b.C_BG_YSMJ_LJ-a.C_BG_YSMJ_LJ=0 or a.C_BG_YSMJ_LJ=0 or a.C_BG_YSMJ_LJ is null then 0 "
                    strSQL = strSQL + " else (b.C_BG_YSMJ_LJ*b.C_BG_JJ_LJ-a.C_BG_YSMJ_LJ*a.C_BG_JJ_LJ)/(b.C_BG_YSMJ_LJ-a.C_BG_YSMJ_LJ) end as 签约均价,"
                    strSQL = strSQL + "    b.C_BG_YSMJ_LJ * b.C_BG_JJ_LJ as 累计签约总额   "
                    strSQL = strSQL + "  from "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select b.*,c.c_house from T_HOUSE_INFO b left join T_HOUSE_MATCH c on b.C_XM_NAME=c.C_XM_NAME and b.C_XZQH=c.C_XZQY  "
                    strSQL = strSQL + "  where b.C_TIME  between @endDate_start and @endDate_end "
                    strSQL = strSQL + "  )b"
                    strSQL = strSQL + "  Left Join "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select a.*,c.c_house from T_HOUSE_INFO a left join T_HOUSE_MATCH c on a.C_XM_NAME=c.C_XM_NAME and a.C_XZQH=c.C_XZQY "
                    strSQL = strSQL + "  where a.C_TIME between @startDate_start and @startDate_end "
                    strSQL = strSQL + "  )a  on  a.C_XM_ID=b.C_XM_ID and a.C_XM_NAME=b.C_XM_NAME and a.C_XZQH=b.C_XZQH and a.C_XM_ADDRESS=b.C_XM_ADDRESS"
                    strSQL = strSQL + "  ) A "
                    If strWhere <> "" Then
                        strSQL = strSQL + "where " + strWhere
                    End If
                    strSQL = strSQL + " group by A.行政区域, A.楼盘名称, A.项目类型 "
                    strSQL = strSQL + " union "
                    strSQL = strSQL + " select "
                    strSQL = strSQL + " A.行政区域 as 行政区域, "
                    strSQL = strSQL + " A.楼盘名称 as 楼盘名称, "
                    strSQL = strSQL + " A.项目类型 as 项目类型, "
                    strSQL = strSQL + " sum(A.签约套数) as 网签数, "
                    strSQL = strSQL + " case when sum(A.签约套数) = 0 then 0 else cast(round(sum(A.签约总额)/sum(A.签约面积),2) as numeric(16,2)) end as 网签均价, "
                    strSQL = strSQL + " sum(A.累计已售套数2) as 合共成交, "
                    strSQL = strSQL + " sum(A.未售套数2) as 未售套数, "
                    strSQL = strSQL + " cast(round(sum(A.签约总额),2) as numeric(16,2)) as 网签总额, "
                    strSQL = strSQL + " cast(round(sum(A.签约面积),2) as numeric(16,2)) as 网签面积 "
                    strSQL = strSQL + " from( "
                    strSQL = strSQL + "  select "
                    strSQL = strSQL + " b.C_ID as 序号,    "
                    strSQL = strSQL + " b.C_XZQH as 行政区域,   "
                    strSQL = strSQL + " b.C_HOUSE as 楼盘名称,  "
                    strSQL = strSQL + " b.C_XM_NAME as 项目名称,  "
                    strSQL = strSQL + " b.C_XM_ID as 预售证,    "
                    strSQL = strSQL + " '商业' as 项目类型, "
                    strSQL = strSQL + " b.C_SY_YSTS_LJ as 累计预售套数1,  "
                    strSQL = strSQL + " b.C_SY_YSMJ_LJ as 累计预售面积1, "
                    strSQL = strSQL + " b.C_SY_JJ_LJ as 累计均价1,  "
                    strSQL = strSQL + " b.C_SY_TS_WS as 未售套数1,  "
                    strSQL = strSQL + " b.C_SY_MJ_WS as 未售面积1,  "
                    strSQL = strSQL + " b.C_SY_YSTS_LJ as 累计已售套数2,   "
                    strSQL = strSQL + " b.C_SY_YSMJ_LJ as 累计已售面积2,  "
                    strSQL = strSQL + " b.C_SY_JJ_LJ as 累计均价2, "
                    strSQL = strSQL + " b.C_SY_TS_WS as 未售套数2,   "
                    strSQL = strSQL + " b.C_SY_MJ_WS as 未售面积2,  "
                    strSQL = strSQL + " 签约套数=case 	when a.C_SY_YSTS_LJ is null then b.C_SY_YSTS_LJ"
                    strSQL = strSQL + "   		when a.C_SY_YSTS_LJ=0 then  b.C_SY_YSTS_LJ"
                    strSQL = strSQL + " 		else   b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ  end , "
                    strSQL = strSQL + "  签约面积=case when a.C_SY_YSMJ_LJ is null or a.C_SY_YSMJ_LJ=0 then b.C_SY_YSMJ_LJ"
                    strSQL = strSQL + "   		when b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else    b.C_SY_YSMJ_LJ-a.C_SY_YSMJ_LJ  end ,"
                    strSQL = strSQL + " 套数核对=case when a.C_SY_YSTS_LJ is null or a.C_SY_TS_WS is null or a.C_SY_YSTS_LJ=0 or a.C_SY_TS_WS=0  then b.C_SY_YSTS_LJ+b.C_SY_TS_WS"
                    strSQL = strSQL + "   		when b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else b.C_SY_YSTS_LJ+b.C_SY_TS_WS-a.C_SY_YSTS_LJ-a.C_SY_TS_WS  end ,"
                    strSQL = strSQL + "  面积核对=case when a.C_SY_YSMJ_LJ is null or a.C_SY_MJ_WS is null or a.C_SY_YSMJ_LJ=0 or a.C_SY_MJ_WS=0 then b.C_SY_YSMJ_LJ+b.C_SY_MJ_WS"
                    strSQL = strSQL + "   		when b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else  b.C_SY_YSMJ_LJ+b.C_SY_MJ_WS-a.C_SY_YSMJ_LJ-a.C_SY_MJ_WS  end ,"
                    strSQL = strSQL + "  签约总额=case when a.C_SY_YSMJ_LJ is null or a.C_SY_JJ_LJ is null  then b.C_SY_YSMJ_LJ*b.C_SY_JJ_LJ"
                    strSQL = strSQL + "   		when a.C_SY_YSMJ_LJ=0 or a.C_SY_JJ_LJ=0  then b.C_SY_YSMJ_LJ*b.C_SY_JJ_LJ"
                    strSQL = strSQL + " 		when b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else   b.C_SY_YSMJ_LJ*b.C_SY_JJ_LJ-a.C_SY_YSMJ_LJ*a.C_SY_JJ_LJ  end , "
                    strSQL = strSQL + "  case when b.C_SY_YSMJ_LJ-a.C_SY_YSMJ_LJ=0 or a.C_SY_YSMJ_LJ=0 or a.C_SY_YSMJ_LJ is null then 0 "
                    strSQL = strSQL + " else (b.C_SY_YSMJ_LJ*b.C_SY_JJ_LJ-a.C_SY_YSMJ_LJ*a.C_SY_JJ_LJ)/(b.C_SY_YSMJ_LJ-a.C_SY_YSMJ_LJ) end as 签约均价,"
                    strSQL = strSQL + "    b.C_SY_YSMJ_LJ * b.C_SY_JJ_LJ as 累计签约总额   "
                    strSQL = strSQL + "  from "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select b.*,c.c_house from T_HOUSE_INFO b left join T_HOUSE_MATCH c on b.C_XM_NAME=c.C_XM_NAME and b.C_XZQH=c.C_XZQY  "
                    strSQL = strSQL + "  where b.C_TIME  between @endDate_start and @endDate_end "
                    strSQL = strSQL + "  )b"
                    strSQL = strSQL + "  Left Join "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select a.*,c.c_house from T_HOUSE_INFO a left join T_HOUSE_MATCH c on a.C_XM_NAME=c.C_XM_NAME and a.C_XZQH=c.C_XZQY "
                    strSQL = strSQL + "  where a.C_TIME between @startDate_start and @startDate_end "
                    strSQL = strSQL + "  )a  on  a.C_XM_ID=b.C_XM_ID and a.C_XM_NAME=b.C_XM_NAME and a.C_XZQH=b.C_XZQH and a.C_XM_ADDRESS=b.C_XM_ADDRESS"
                    strSQL = strSQL + "  ) A "
                    If strWhere <> "" Then
                        strSQL = strSQL + " where " + strWhere
                    End If
                    strSQL = strSQL + " group by A.行政区域, A.楼盘名称, A.项目类型 "
                    strSQL = strSQL + " )A"

                Case "2"
                    strSQL = strSQL + " select  "
                    strSQL = strSQL + " A.行政区域 as 行政区域, "
                    strSQL = strSQL + " A.楼盘名称 as 楼盘名称, "
                    strSQL = strSQL + " A.项目类型 as 项目类型, "
                    strSQL = strSQL + " sum(A.签约套数) as 网签数, "
                    strSQL = strSQL + "  case when sum(A.签约套数) = 0 then 0 else cast(round(sum(A.签约总额)/sum(A.签约面积),2) as numeric(16,2)) end as 网签均价, "
                    strSQL = strSQL + "  sum(A.累计已售套数2) as 合共成交, "
                    strSQL = strSQL + "  sum(A.未售套数2) as 未售套数, "
                    strSQL = strSQL + "  cast(round(sum(A.签约总额),2) as numeric(16,2)) as 网签总额, "
                    strSQL = strSQL + "  cast(round(sum(A.签约面积),2) as numeric(16,2)) as 网签面积 "
                    strSQL = strSQL + "  from( "
                    strSQL = strSQL + "  select "
                    strSQL = strSQL + " b.C_ID as 序号, "
                    strSQL = strSQL + " b.C_XZQH as 行政区域,   "
                    strSQL = strSQL + " b.C_HOUSE as 楼盘名称,  "
                    strSQL = strSQL + " b.C_XM_NAME as 项目名称,  "
                    strSQL = strSQL + " b.C_XM_ID as 预售证,    "
                    strSQL = strSQL + " '住宅' as 项目类型, "
                    strSQL = strSQL + " b.C_ZZ_YSTS_LJ as 累计预售套数1,  "
                    strSQL = strSQL + " b.C_ZZ_YSMJ_LJ as 累计预售面积1, "
                    strSQL = strSQL + " b.C_ZZ_JJ_LJ as 累计均价1,  "
                    strSQL = strSQL + " b.C_ZZ_TS_WS as 未售套数1,  "
                    strSQL = strSQL + " b.C_ZZ_MJ_WS as 未售面积1,  "
                    strSQL = strSQL + " b.C_ZZ_YSTS_LJ as 累计已售套数2,   "
                    strSQL = strSQL + " b.C_ZZ_YSMJ_LJ as 累计已售面积2,  "
                    strSQL = strSQL + " b.C_ZZ_JJ_LJ as 累计均价2, "
                    strSQL = strSQL + " b.C_ZZ_TS_WS as 未售套数2,   "
                    strSQL = strSQL + " b.C_ZZ_MJ_WS as 未售面积2,  "
                    strSQL = strSQL + " 签约套数=case 	when a.C_ZZ_YSTS_LJ is null then b.C_ZZ_YSTS_LJ"
                    strSQL = strSQL + "   		when a.C_ZZ_YSTS_LJ=0 then  b.C_ZZ_YSTS_LJ"
                    strSQL = strSQL + " 		else   b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ  end , "
                    strSQL = strSQL + "  签约面积=case when a.C_ZZ_YSMJ_LJ is null or a.C_ZZ_YSMJ_LJ=0 then b.C_ZZ_YSMJ_LJ"
                    strSQL = strSQL + "   		when b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else    b.C_ZZ_YSMJ_LJ-a.C_ZZ_YSMJ_LJ  end ,"
                    strSQL = strSQL + " 套数核对=case when a.C_ZZ_YSTS_LJ is null or a.C_ZZ_TS_WS is null or a.C_ZZ_YSTS_LJ=0 or a.C_ZZ_TS_WS=0  then b.C_ZZ_YSTS_LJ+b.C_ZZ_TS_WS"
                    strSQL = strSQL + "   		when b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else b.C_ZZ_YSTS_LJ+b.C_ZZ_TS_WS-a.C_ZZ_YSTS_LJ-a.C_ZZ_TS_WS  end ,"
                    strSQL = strSQL + "  面积核对=case when a.C_ZZ_YSMJ_LJ is null or a.C_ZZ_MJ_WS is null or a.C_ZZ_YSMJ_LJ=0 or a.C_ZZ_MJ_WS=0 then b.C_ZZ_YSMJ_LJ+b.C_ZZ_MJ_WS"
                    strSQL = strSQL + "   		when b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else  b.C_ZZ_YSMJ_LJ+b.C_ZZ_MJ_WS-a.C_ZZ_YSMJ_LJ-a.C_ZZ_MJ_WS  end ,"
                    strSQL = strSQL + "  签约总额=case when a.C_ZZ_YSMJ_LJ is null or a.C_ZZ_JJ_LJ is null  then b.C_ZZ_YSMJ_LJ*b.C_ZZ_JJ_LJ"
                    strSQL = strSQL + "   		when a.C_ZZ_YSMJ_LJ=0 or a.C_ZZ_JJ_LJ=0  then b.C_ZZ_YSMJ_LJ*b.C_ZZ_JJ_LJ"
                    strSQL = strSQL + " 		when b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else   b.C_ZZ_YSMJ_LJ*b.C_ZZ_JJ_LJ-a.C_ZZ_YSMJ_LJ*a.C_ZZ_JJ_LJ  end , "
                    strSQL = strSQL + "  case when b.C_ZZ_YSMJ_LJ-a.C_ZZ_YSMJ_LJ=0 or a.C_ZZ_YSMJ_LJ=0 or a.C_ZZ_YSMJ_LJ is null then 0 "
                    strSQL = strSQL + " else (b.C_ZZ_YSMJ_LJ*b.C_ZZ_JJ_LJ-a.C_ZZ_YSMJ_LJ*a.C_ZZ_JJ_LJ)/(b.C_ZZ_YSMJ_LJ-a.C_ZZ_YSMJ_LJ) end as 签约均价,"
                    strSQL = strSQL + "    b.C_ZZ_YSMJ_LJ * b.C_ZZ_JJ_LJ as 累计签约总额   "
                    strSQL = strSQL + "  from "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select b.*,c.c_house from T_HOUSE_INFO b left join T_HOUSE_MATCH c on b.C_XM_NAME=c.C_XM_NAME and b.C_XZQH=c.C_XZQY  "
                    strSQL = strSQL + "  where b.C_TIME  between @endDate_start and @endDate_end "
                    strSQL = strSQL + "  )b"
                    strSQL = strSQL + "  Left Join "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select a.*,c.c_house from T_HOUSE_INFO a left join T_HOUSE_MATCH c on a.C_XM_NAME=c.C_XM_NAME and a.C_XZQH=c.C_XZQY "
                    strSQL = strSQL + "  where a.C_TIME between @startDate_start and @startDate_end "
                    strSQL = strSQL + "  )a  on  a.C_XM_ID=b.C_XM_ID and a.C_XM_NAME=b.C_XM_NAME and a.C_XZQH=b.C_XZQH and a.C_XM_ADDRESS=b.C_XM_ADDRESS"
                    strSQL = strSQL + "   ) A "
                    If strWhere <> "" Then
                        strSQL = strSQL + "where " + strWhere
                    End If
                    strSQL = strSQL + " group by A.行政区域, A.楼盘名称, A.项目类型 "

                Case "3"
                    strSQL = strSQL + " select "
                    strSQL = strSQL + " A.行政区域 as 行政区域, "
                    strSQL = strSQL + " A.楼盘名称 as 楼盘名称, "
                    strSQL = strSQL + " A.项目类型 as 项目类型, "
                    strSQL = strSQL + " sum(A.签约套数) as 网签数, "
                    strSQL = strSQL + " case when sum(A.签约套数) = 0 then 0 else cast(round(sum(A.签约总额)/sum(A.签约面积),2) as numeric(16,2)) end as 网签均价, "
                    strSQL = strSQL + " sum(A.累计已售套数2) as 合共成交, "
                    strSQL = strSQL + " sum(A.未售套数2) as 未售套数, "
                    strSQL = strSQL + " cast(round(sum(A.签约总额),2) as numeric(16,2)) as 网签总额, "
                    strSQL = strSQL + " cast(round(sum(A.签约面积),2) as numeric(16,2)) as 网签面积 "
                    strSQL = strSQL + " from( "
                    strSQL = strSQL + "  select "
                    strSQL = strSQL + " b.C_ID as 序号,  "
                    strSQL = strSQL + " b.C_XZQH as 行政区域,   "
                    strSQL = strSQL + " b.C_HOUSE as 楼盘名称,  "
                    strSQL = strSQL + " b.C_XM_NAME as 项目名称,  "
                    strSQL = strSQL + " b.C_XM_ID as 预售证,    "
                    strSQL = strSQL + " '办公' as 项目类型, "
                    strSQL = strSQL + " b.C_BG_YSTS_LJ as 累计预售套数1,  "
                    strSQL = strSQL + " b.C_BG_YSMJ_LJ as 累计预售面积1, "
                    strSQL = strSQL + " b.C_BG_JJ_LJ as 累计均价1,  "
                    strSQL = strSQL + " b.C_BG_TS_WS as 未售套数1,  "
                    strSQL = strSQL + " b.C_BG_MJ_WS as 未售面积1,  "
                    strSQL = strSQL + " b.C_BG_YSTS_LJ as 累计已售套数2,   "
                    strSQL = strSQL + " b.C_BG_YSMJ_LJ as 累计已售面积2,  "
                    strSQL = strSQL + " b.C_BG_JJ_LJ as 累计均价2, "
                    strSQL = strSQL + " b.C_BG_TS_WS as 未售套数2,   "
                    strSQL = strSQL + " b.C_BG_MJ_WS as 未售面积2,  "
                    strSQL = strSQL + " 签约套数=case 	when a.C_BG_YSTS_LJ is null then b.C_BG_YSTS_LJ"
                    strSQL = strSQL + "   		when a.C_BG_YSTS_LJ=0 then  b.C_BG_YSTS_LJ"
                    strSQL = strSQL + " 		else   b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ  end , "
                    strSQL = strSQL + "  签约面积=case when a.C_BG_YSMJ_LJ is null or a.C_BG_YSMJ_LJ=0 then b.C_BG_YSMJ_LJ"
                    strSQL = strSQL + "   		when b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else    b.C_BG_YSMJ_LJ-a.C_BG_YSMJ_LJ  end ,"
                    strSQL = strSQL + " 套数核对=case when a.C_BG_YSTS_LJ is null or a.C_BG_TS_WS is null or a.C_BG_YSTS_LJ=0 or a.C_BG_TS_WS=0  then b.C_BG_YSTS_LJ+b.C_BG_TS_WS"
                    strSQL = strSQL + "   		when b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else b.C_BG_YSTS_LJ+b.C_BG_TS_WS-a.C_BG_YSTS_LJ-a.C_BG_TS_WS  end ,"
                    strSQL = strSQL + "  面积核对=case when a.C_BG_YSMJ_LJ is null or a.C_BG_MJ_WS is null or a.C_BG_YSMJ_LJ=0 or a.C_BG_MJ_WS=0 then b.C_BG_YSMJ_LJ+b.C_BG_MJ_WS"
                    strSQL = strSQL + "   		when b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else  b.C_BG_YSMJ_LJ+b.C_BG_MJ_WS-a.C_BG_YSMJ_LJ-a.C_BG_MJ_WS  end ,"
                    strSQL = strSQL + "  签约总额=case when a.C_BG_YSMJ_LJ is null or a.C_BG_JJ_LJ is null  then b.C_BG_YSMJ_LJ*b.C_BG_JJ_LJ"
                    strSQL = strSQL + "   		when a.C_BG_YSMJ_LJ=0 or a.C_BG_JJ_LJ=0  then b.C_BG_YSMJ_LJ*b.C_BG_JJ_LJ"
                    strSQL = strSQL + " 		when b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else   b.C_BG_YSMJ_LJ*b.C_BG_JJ_LJ-a.C_BG_YSMJ_LJ*a.C_BG_JJ_LJ  end , "
                    strSQL = strSQL + "  case when b.C_BG_YSMJ_LJ-a.C_BG_YSMJ_LJ=0 or a.C_BG_YSMJ_LJ=0 or a.C_BG_YSMJ_LJ is null then 0 "
                    strSQL = strSQL + " else (b.C_BG_YSMJ_LJ*b.C_BG_JJ_LJ-a.C_BG_YSMJ_LJ*a.C_BG_JJ_LJ)/(b.C_BG_YSMJ_LJ-a.C_BG_YSMJ_LJ) end as 签约均价,"
                    strSQL = strSQL + "    b.C_BG_YSMJ_LJ * b.C_BG_JJ_LJ as 累计签约总额   "
                    strSQL = strSQL + "  from "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select b.*,c.c_house from T_HOUSE_INFO b left join T_HOUSE_MATCH c on b.C_XM_NAME=c.C_XM_NAME and b.C_XZQH=c.C_XZQY  "
                    strSQL = strSQL + "  where b.C_TIME  between @endDate_start and @endDate_end "
                    strSQL = strSQL + "  )b"
                    strSQL = strSQL + "  Left Join "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select a.*,c.c_house from T_HOUSE_INFO a left join T_HOUSE_MATCH c on a.C_XM_NAME=c.C_XM_NAME and a.C_XZQH=c.C_XZQY "
                    strSQL = strSQL + "  where a.C_TIME between @startDate_start and @startDate_end "
                    strSQL = strSQL + "  )a  on  a.C_XM_ID=b.C_XM_ID and a.C_XM_NAME=b.C_XM_NAME and a.C_XZQH=b.C_XZQH and a.C_XM_ADDRESS=b.C_XM_ADDRESS"
                    strSQL = strSQL + "  ) A "
                    If strWhere <> "" Then
                        strSQL = strSQL + "where " + strWhere
                    End If
                    strSQL = strSQL + " group by A.行政区域, A.楼盘名称, A.项目类型"

                Case "1"
                    strSQL = strSQL + " select  "
                    strSQL = strSQL + " A.行政区域 as 行政区域, "
                    strSQL = strSQL + " A.楼盘名称 as 楼盘名称, "
                    strSQL = strSQL + " A.项目类型 as 项目类型, "
                    strSQL = strSQL + " sum(A.签约套数) as 网签数, "
                    strSQL = strSQL + " case when sum(A.签约套数) = 0 then 0 else cast(round(sum(A.签约总额)/sum(A.签约面积),2) as numeric(16,2)) end as 网签均价, "
                    strSQL = strSQL + " sum(A.累计已售套数2) as 合共成交, "
                    strSQL = strSQL + " sum(A.未售套数2) as 未售套数, "
                    strSQL = strSQL + " cast(round(sum(A.签约总额),2) as numeric(16,2)) as 网签总额, "
                    strSQL = strSQL + " cast(round(sum(A.签约面积),2) as numeric(16,2)) as 网签面积 "
                    strSQL = strSQL + " from( "
                    strSQL = strSQL + "  select "
                    strSQL = strSQL + " b.C_ID as 序号,   "
                    strSQL = strSQL + " b.C_XZQH as 行政区域,   "
                    strSQL = strSQL + " b.C_HOUSE as 楼盘名称,  "
                    strSQL = strSQL + " b.C_XM_NAME as 项目名称,  "
                    strSQL = strSQL + " b.C_XM_ID as 预售证,    "
                    strSQL = strSQL + " '商业' as 项目类型, "
                    strSQL = strSQL + " b.C_SY_YSTS_LJ as 累计预售套数1,  "
                    strSQL = strSQL + " b.C_SY_YSMJ_LJ as 累计预售面积1, "
                    strSQL = strSQL + " b.C_SY_JJ_LJ as 累计均价1,  "
                    strSQL = strSQL + " b.C_SY_TS_WS as 未售套数1,  "
                    strSQL = strSQL + " b.C_SY_MJ_WS as 未售面积1,  "
                    strSQL = strSQL + " b.C_SY_YSTS_LJ as 累计已售套数2,   "
                    strSQL = strSQL + " b.C_SY_YSMJ_LJ as 累计已售面积2,  "
                    strSQL = strSQL + " b.C_SY_JJ_LJ as 累计均价2, "
                    strSQL = strSQL + " b.C_SY_TS_WS as 未售套数2,   "
                    strSQL = strSQL + " b.C_SY_MJ_WS as 未售面积2,  "
                    strSQL = strSQL + " 签约套数=case 	when a.C_SY_YSTS_LJ is null then b.C_SY_YSTS_LJ"
                    strSQL = strSQL + "   		when a.C_SY_YSTS_LJ=0 then  b.C_SY_YSTS_LJ"
                    strSQL = strSQL + " 		else   b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ  end , "
                    strSQL = strSQL + "  签约面积=case when a.C_SY_YSMJ_LJ is null or a.C_SY_YSMJ_LJ=0 then b.C_SY_YSMJ_LJ"
                    strSQL = strSQL + "   		when b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else    b.C_SY_YSMJ_LJ-a.C_SY_YSMJ_LJ  end ,"
                    strSQL = strSQL + " 套数核对=case when a.C_SY_YSTS_LJ is null or a.C_SY_TS_WS is null or a.C_SY_YSTS_LJ=0 or a.C_SY_TS_WS=0  then b.C_SY_YSTS_LJ+b.C_SY_TS_WS"
                    strSQL = strSQL + "   		when b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else b.C_SY_YSTS_LJ+b.C_SY_TS_WS-a.C_SY_YSTS_LJ-a.C_SY_TS_WS  end ,"
                    strSQL = strSQL + "  面积核对=case when a.C_SY_YSMJ_LJ is null or a.C_SY_MJ_WS is null or a.C_SY_YSMJ_LJ=0 or a.C_SY_MJ_WS=0 then b.C_SY_YSMJ_LJ+b.C_SY_MJ_WS"
                    strSQL = strSQL + "   		when b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else  b.C_SY_YSMJ_LJ+b.C_SY_MJ_WS-a.C_SY_YSMJ_LJ-a.C_SY_MJ_WS  end ,"
                    strSQL = strSQL + "  签约总额=case when a.C_SY_YSMJ_LJ is null or a.C_SY_JJ_LJ is null  then b.C_SY_YSMJ_LJ*b.C_SY_JJ_LJ"
                    strSQL = strSQL + "   		when a.C_SY_YSMJ_LJ=0 or a.C_SY_JJ_LJ=0  then b.C_SY_YSMJ_LJ*b.C_SY_JJ_LJ"
                    strSQL = strSQL + " 		when b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else   b.C_SY_YSMJ_LJ*b.C_SY_JJ_LJ-a.C_SY_YSMJ_LJ*a.C_SY_JJ_LJ  end , "
                    strSQL = strSQL + "  case when b.C_SY_YSMJ_LJ-a.C_SY_YSMJ_LJ=0 or a.C_SY_YSMJ_LJ=0 or a.C_SY_YSMJ_LJ is null then 0 "
                    strSQL = strSQL + " else (b.C_SY_YSMJ_LJ*b.C_SY_JJ_LJ-a.C_SY_YSMJ_LJ*a.C_SY_JJ_LJ)/(b.C_SY_YSMJ_LJ-a.C_SY_YSMJ_LJ) end as 签约均价,"
                    strSQL = strSQL + "    b.C_SY_YSMJ_LJ * b.C_SY_JJ_LJ as 累计签约总额   "
                    strSQL = strSQL + "  from "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select b.*,c.c_house from T_HOUSE_INFO b left join T_HOUSE_MATCH c on b.C_XM_NAME=c.C_XM_NAME and b.C_XZQH=c.C_XZQY  "
                    strSQL = strSQL + "  where b.C_TIME  between @endDate_start and @endDate_end "
                    strSQL = strSQL + "  )b"
                    strSQL = strSQL + "  Left Join "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select a.*,c.c_house from T_HOUSE_INFO a left join T_HOUSE_MATCH c on a.C_XM_NAME=c.C_XM_NAME and a.C_XZQH=c.C_XZQY "
                    strSQL = strSQL + "  where a.C_TIME between @startDate_start and @startDate_end "
                    strSQL = strSQL + "  )a  on  a.C_XM_ID=b.C_XM_ID and a.C_XM_NAME=b.C_XM_NAME and a.C_XZQH=b.C_XZQH and a.C_XM_ADDRESS=b.C_XM_ADDRESS"
                    strSQL = strSQL + "  ) A "
                    If strWhere <> "" Then
                        strSQL = strSQL + "where " + strWhere
                    End If
                    strSQL = strSQL + " group by A.行政区域, A.楼盘名称, A.项目类型"

                Case Else

            End Select

            getSql_BuildingCompute_XMID = True
errProc:

            Exit Function
        End Function

        '----------------------------------------------------------------
        ' 根据“查询条件”获取“阳光家缘楼盘信息数据分析”完全数据的数据集
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strSQL                     ：返回的SQL语句
        '     strType                    ：选择楼盘的类型
        '     strWhere                   ：搜索字符串      
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getSql_FrontBuildingCompute( _
             ByRef strErrMsg As String, _
             ByRef strSQL As String, _
             ByVal strWhere As String, _
             ByRef strType As String) As Boolean

            getSql_FrontBuildingCompute = False

            strSQL = ""
            Select Case strType
                Case "0"
                    strSQL = strSQL + " select * from ("
                    strSQL = strSQL + " select "
                    strSQL = strSQL + " A.行政区域 as 行政区域, "
                    strSQL = strSQL + " A.楼盘名称 as 楼盘名称, "
                    strSQL = strSQL + " A.项目类型 as 项目类型, "
                    strSQL = strSQL + " sum(A.签约套数) as 网签数, "
                    strSQL = strSQL + "  case when sum(A.签约套数) = 0 then 0 else cast(round(sum(A.签约总额)/sum(A.签约面积),2) as numeric(16,2)) end as 网签均价, "
                    strSQL = strSQL + "  sum(A.累计已售套数2) as 合共成交, "
                    strSQL = strSQL + "  sum(A.未售套数2) as 未售套数, "
                    strSQL = strSQL + "  cast(round(sum(A.签约总额),2) as numeric(16,2)) as 网签总额, "
                    strSQL = strSQL + "  cast(round(sum(A.签约面积),2) as numeric(16,2)) as 网签面积 "
                    strSQL = strSQL + "  from( "
                    strSQL = strSQL + "  select "
                    strSQL = strSQL + " b.C_ID as 序号,  "
                    strSQL = strSQL + " b.C_XZQH as 行政区域,   "
                    strSQL = strSQL + " b.C_HOUSE as 楼盘名称,  "
                    strSQL = strSQL + " b.C_XM_NAME as 项目名称,  "
                    strSQL = strSQL + " b.C_XM_ID as 预售证,    "
                    strSQL = strSQL + " '住宅' as 项目类型, "
                    strSQL = strSQL + " b.C_ZZ_YSTS_LJ as 累计预售套数1,  "
                    strSQL = strSQL + " b.C_ZZ_YSMJ_LJ as 累计预售面积1, "
                    strSQL = strSQL + " b.C_ZZ_JJ_LJ as 累计均价1,  "
                    strSQL = strSQL + " b.C_ZZ_TS_WS as 未售套数1,  "
                    strSQL = strSQL + " b.C_ZZ_MJ_WS as 未售面积1,  "
                    strSQL = strSQL + " b.C_ZZ_YSTS_LJ as 累计已售套数2,   "
                    strSQL = strSQL + " b.C_ZZ_YSMJ_LJ as 累计已售面积2,  "
                    strSQL = strSQL + " b.C_ZZ_JJ_LJ as 累计均价2, "
                    strSQL = strSQL + " b.C_ZZ_TS_WS as 未售套数2,   "
                    strSQL = strSQL + " b.C_ZZ_MJ_WS as 未售面积2,  "
                    strSQL = strSQL + " 签约套数=case 	when a.C_ZZ_YSTS_LJ is null then b.C_ZZ_YSTS_LJ"
                    strSQL = strSQL + "   		when a.C_ZZ_YSTS_LJ=0 then  b.C_ZZ_YSTS_LJ"
                    strSQL = strSQL + " 		else   b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ  end , "
                    strSQL = strSQL + "  签约面积=case when a.C_ZZ_YSMJ_LJ is null or a.C_ZZ_YSMJ_LJ=0 then b.C_ZZ_YSMJ_LJ"
                    strSQL = strSQL + "   		when b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else    b.C_ZZ_YSMJ_LJ-a.C_ZZ_YSMJ_LJ  end ,"
                    strSQL = strSQL + " 套数核对=case when a.C_ZZ_YSTS_LJ is null or a.C_ZZ_TS_WS is null or a.C_ZZ_YSTS_LJ=0 or a.C_ZZ_TS_WS=0  then b.C_ZZ_YSTS_LJ+b.C_ZZ_TS_WS"
                    strSQL = strSQL + "   		when b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else b.C_ZZ_YSTS_LJ+b.C_ZZ_TS_WS-a.C_ZZ_YSTS_LJ-a.C_ZZ_TS_WS  end ,"
                    strSQL = strSQL + "  面积核对=case when a.C_ZZ_YSMJ_LJ is null or a.C_ZZ_MJ_WS is null or a.C_ZZ_YSMJ_LJ=0 or a.C_ZZ_MJ_WS=0 then b.C_ZZ_YSMJ_LJ+b.C_ZZ_MJ_WS"
                    strSQL = strSQL + "   		when b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else  b.C_ZZ_YSMJ_LJ+b.C_ZZ_MJ_WS-a.C_ZZ_YSMJ_LJ-a.C_ZZ_MJ_WS  end ,"
                    strSQL = strSQL + "  签约总额=case when a.C_ZZ_YSMJ_LJ is null or a.C_ZZ_JJ_LJ is null  then b.C_ZZ_YSMJ_LJ*b.C_ZZ_JJ_LJ"
                    strSQL = strSQL + "   		when a.C_ZZ_YSMJ_LJ=0 or a.C_ZZ_JJ_LJ=0  then b.C_ZZ_YSMJ_LJ*b.C_ZZ_JJ_LJ"
                    strSQL = strSQL + " 		when b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else   b.C_ZZ_YSMJ_LJ*b.C_ZZ_JJ_LJ-a.C_ZZ_YSMJ_LJ*a.C_ZZ_JJ_LJ  end , "
                    strSQL = strSQL + "  case when b.C_ZZ_YSMJ_LJ-a.C_ZZ_YSMJ_LJ=0 or a.C_ZZ_YSMJ_LJ=0 or a.C_ZZ_YSMJ_LJ is null then 0 "
                    strSQL = strSQL + " else (b.C_ZZ_YSMJ_LJ*b.C_ZZ_JJ_LJ-a.C_ZZ_YSMJ_LJ*a.C_ZZ_JJ_LJ)/(b.C_ZZ_YSMJ_LJ-a.C_ZZ_YSMJ_LJ) end as 签约均价,"
                    strSQL = strSQL + "    b.C_ZZ_YSMJ_LJ * b.C_ZZ_JJ_LJ as 累计签约总额   "
                    strSQL = strSQL + "  from "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select b.*,c.c_house from T_HOUSE_INFO b left join T_HOUSE_MATCH c on b.C_XM_NAME=c.C_XM_NAME and b.C_XZQH=c.C_XZQY  "
                    strSQL = strSQL + "  where b.C_TIME between @frontendDate_start and @frontendDate_end "
                    strSQL = strSQL + "  )b"
                    strSQL = strSQL + "  Left Join "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select a.*,c.c_house from T_HOUSE_INFO a left join T_HOUSE_MATCH c on a.C_XM_NAME=c.C_XM_NAME and a.C_XZQH=c.C_XZQY "
                    strSQL = strSQL + "  where a.C_TIME between @frontstartDate_start and @frontstartDate_end "
                    strSQL = strSQL + "  )a  on  a.C_XM_ID=b.C_XM_ID and a.C_XM_NAME=b.C_XM_NAME and a.C_XZQH=b.C_XZQH and a.C_XM_ADDRESS=b.C_XM_ADDRESS"
                    strSQL = strSQL + "   ) A "
                    If strWhere <> "" Then
                        strSQL = strSQL + "where " + strWhere
                    End If
                    strSQL = strSQL + " group by A.行政区域, A.楼盘名称, A.项目类型 "
                    strSQL = strSQL + " union "
                    strSQL = strSQL + " select "
                    strSQL = strSQL + " A.行政区域 as 行政区域, "
                    strSQL = strSQL + " A.楼盘名称 as 楼盘名称, "
                    strSQL = strSQL + " A.项目类型 as 项目类型, "
                    strSQL = strSQL + " sum(A.签约套数) as 网签数, "
                    strSQL = strSQL + " case when sum(A.签约套数) = 0 then 0 else cast(round(sum(A.签约总额)/sum(A.签约面积),2) as numeric(16,2)) end as 网签均价, "
                    strSQL = strSQL + " sum(A.累计已售套数2) as 合共成交, "
                    strSQL = strSQL + " sum(A.未售套数2) as 未售套数, "
                    strSQL = strSQL + " cast(round(sum(A.签约总额),2) as numeric(16,2)) as 网签总额, "
                    strSQL = strSQL + " cast(round(sum(A.签约面积),2) as numeric(16,2)) as 网签面积 "
                    strSQL = strSQL + " from( "
                    strSQL = strSQL + "  select "
                    strSQL = strSQL + " b.C_ID as 序号, "
                    strSQL = strSQL + " b.C_XZQH as 行政区域,   "
                    strSQL = strSQL + " b.C_HOUSE as 楼盘名称,  "
                    strSQL = strSQL + " b.C_XM_NAME as 项目名称,  "
                    strSQL = strSQL + " b.C_XM_ID as 预售证,    "
                    strSQL = strSQL + " '办公' as 项目类型, "
                    strSQL = strSQL + " b.C_BG_YSTS_LJ as 累计预售套数1,  "
                    strSQL = strSQL + " b.C_BG_YSMJ_LJ as 累计预售面积1, "
                    strSQL = strSQL + " b.C_BG_JJ_LJ as 累计均价1,  "
                    strSQL = strSQL + " b.C_BG_TS_WS as 未售套数1,  "
                    strSQL = strSQL + " b.C_BG_MJ_WS as 未售面积1,  "
                    strSQL = strSQL + " b.C_BG_YSTS_LJ as 累计已售套数2,   "
                    strSQL = strSQL + " b.C_BG_YSMJ_LJ as 累计已售面积2,  "
                    strSQL = strSQL + " b.C_BG_JJ_LJ as 累计均价2, "
                    strSQL = strSQL + " b.C_BG_TS_WS as 未售套数2,   "
                    strSQL = strSQL + " b.C_BG_MJ_WS as 未售面积2,  "
                    strSQL = strSQL + " 签约套数=case 	when a.C_BG_YSTS_LJ is null then b.C_BG_YSTS_LJ"
                    strSQL = strSQL + "   		when a.C_BG_YSTS_LJ=0 then  b.C_BG_YSTS_LJ"
                    strSQL = strSQL + " 		else   b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ  end , "
                    strSQL = strSQL + "  签约面积=case when a.C_BG_YSMJ_LJ is null or a.C_BG_YSMJ_LJ=0 then b.C_BG_YSMJ_LJ"
                    strSQL = strSQL + "   		when b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else    b.C_BG_YSMJ_LJ-a.C_BG_YSMJ_LJ  end ,"
                    strSQL = strSQL + " 套数核对=case when a.C_BG_YSTS_LJ is null or a.C_BG_TS_WS is null or a.C_BG_YSTS_LJ=0 or a.C_BG_TS_WS=0  then b.C_BG_YSTS_LJ+b.C_BG_TS_WS"
                    strSQL = strSQL + "   		when b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else b.C_BG_YSTS_LJ+b.C_BG_TS_WS-a.C_BG_YSTS_LJ-a.C_BG_TS_WS  end ,"
                    strSQL = strSQL + "  面积核对=case when a.C_BG_YSMJ_LJ is null or a.C_BG_MJ_WS is null or a.C_BG_YSMJ_LJ=0 or a.C_BG_MJ_WS=0 then b.C_BG_YSMJ_LJ+b.C_BG_MJ_WS"
                    strSQL = strSQL + "   		when b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else  b.C_BG_YSMJ_LJ+b.C_BG_MJ_WS-a.C_BG_YSMJ_LJ-a.C_BG_MJ_WS  end ,"
                    strSQL = strSQL + "  签约总额=case when a.C_BG_YSMJ_LJ is null or a.C_BG_JJ_LJ is null  then b.C_BG_YSMJ_LJ*b.C_BG_JJ_LJ"
                    strSQL = strSQL + "   		when a.C_BG_YSMJ_LJ=0 or a.C_BG_JJ_LJ=0  then b.C_BG_YSMJ_LJ*b.C_BG_JJ_LJ"
                    strSQL = strSQL + " 		when b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else   b.C_BG_YSMJ_LJ*b.C_BG_JJ_LJ-a.C_BG_YSMJ_LJ*a.C_BG_JJ_LJ  end , "
                    strSQL = strSQL + "  case when b.C_BG_YSMJ_LJ-a.C_BG_YSMJ_LJ=0 or a.C_BG_YSMJ_LJ=0 or a.C_BG_YSMJ_LJ is null then 0 "
                    strSQL = strSQL + " else (b.C_BG_YSMJ_LJ*b.C_BG_JJ_LJ-a.C_BG_YSMJ_LJ*a.C_BG_JJ_LJ)/(b.C_BG_YSMJ_LJ-a.C_BG_YSMJ_LJ) end as 签约均价,"
                    strSQL = strSQL + "    b.C_BG_YSMJ_LJ * b.C_BG_JJ_LJ as 累计签约总额   "
                    strSQL = strSQL + "  from "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select b.*,c.c_house from T_HOUSE_INFO b left join T_HOUSE_MATCH c on b.C_XM_NAME=c.C_XM_NAME and b.C_XZQH=c.C_XZQY  "
                    strSQL = strSQL + "  where b.C_TIME between @frontendDate_start and @frontendDate_end "
                    strSQL = strSQL + "  )b"
                    strSQL = strSQL + "  Left Join "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select a.*,c.c_house from T_HOUSE_INFO a left join T_HOUSE_MATCH c on a.C_XM_NAME=c.C_XM_NAME and a.C_XZQH=c.C_XZQY "
                    strSQL = strSQL + "  where a.C_TIME between @frontstartDate_start and @frontstartDate_end "
                    strSQL = strSQL + "  )a  on  a.C_XM_ID=b.C_XM_ID and a.C_XM_NAME=b.C_XM_NAME and a.C_XZQH=b.C_XZQH and a.C_XM_ADDRESS=b.C_XM_ADDRESS"
                    strSQL = strSQL + "  ) A "
                    If strWhere <> "" Then
                        strSQL = strSQL + "where " + strWhere
                    End If
                    strSQL = strSQL + " group by A.行政区域, A.楼盘名称, A.项目类型 "
                    strSQL = strSQL + " union "
                    strSQL = strSQL + " select "
                    strSQL = strSQL + " A.行政区域 as 行政区域, "
                    strSQL = strSQL + " A.楼盘名称 as 楼盘名称, "
                    strSQL = strSQL + " A.项目类型 as 项目类型, "
                    strSQL = strSQL + " sum(A.签约套数) as 网签数, "
                    strSQL = strSQL + " case when sum(A.签约套数) = 0 then 0 else cast(round(sum(A.签约总额)/sum(A.签约面积),2) as numeric(16,2)) end as 网签均价, "
                    strSQL = strSQL + " sum(A.累计已售套数2) as 合共成交, "
                    strSQL = strSQL + " sum(A.未售套数2) as 未售套数, "
                    strSQL = strSQL + " cast(round(sum(A.签约总额),2) as numeric(16,2)) as 网签总额, "
                    strSQL = strSQL + " cast(round(sum(A.签约面积),2) as numeric(16,2)) as 网签面积 "
                    strSQL = strSQL + " from( "
                    strSQL = strSQL + "  select "
                    strSQL = strSQL + " b.C_ID as 序号,    "
                    strSQL = strSQL + " b.C_XZQH as 行政区域,   "
                    strSQL = strSQL + " b.C_HOUSE as 楼盘名称,  "
                    strSQL = strSQL + " b.C_XM_NAME as 项目名称,  "
                    strSQL = strSQL + " b.C_XM_ID as 预售证,    "
                    strSQL = strSQL + " '商业' as 项目类型, "
                    strSQL = strSQL + " b.C_SY_YSTS_LJ as 累计预售套数1,  "
                    strSQL = strSQL + " b.C_SY_YSMJ_LJ as 累计预售面积1, "
                    strSQL = strSQL + " b.C_SY_JJ_LJ as 累计均价1,  "
                    strSQL = strSQL + " b.C_SY_TS_WS as 未售套数1,  "
                    strSQL = strSQL + " b.C_SY_MJ_WS as 未售面积1,  "
                    strSQL = strSQL + " b.C_SY_YSTS_LJ as 累计已售套数2,   "
                    strSQL = strSQL + " b.C_SY_YSMJ_LJ as 累计已售面积2,  "
                    strSQL = strSQL + " b.C_SY_JJ_LJ as 累计均价2, "
                    strSQL = strSQL + " b.C_SY_TS_WS as 未售套数2,   "
                    strSQL = strSQL + " b.C_SY_MJ_WS as 未售面积2,  "
                    strSQL = strSQL + " 签约套数=case 	when a.C_SY_YSTS_LJ is null then b.C_SY_YSTS_LJ"
                    strSQL = strSQL + "   		when a.C_SY_YSTS_LJ=0 then  b.C_SY_YSTS_LJ"
                    strSQL = strSQL + " 		else   b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ  end , "
                    strSQL = strSQL + "  签约面积=case when a.C_SY_YSMJ_LJ is null or a.C_SY_YSMJ_LJ=0 then b.C_SY_YSMJ_LJ"
                    strSQL = strSQL + "   		when b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else    b.C_SY_YSMJ_LJ-a.C_SY_YSMJ_LJ  end ,"
                    strSQL = strSQL + " 套数核对=case when a.C_SY_YSTS_LJ is null or a.C_SY_TS_WS is null or a.C_SY_YSTS_LJ=0 or a.C_SY_TS_WS=0  then b.C_SY_YSTS_LJ+b.C_SY_TS_WS"
                    strSQL = strSQL + "   		when b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else b.C_SY_YSTS_LJ+b.C_SY_TS_WS-a.C_SY_YSTS_LJ-a.C_SY_TS_WS  end ,"
                    strSQL = strSQL + "  面积核对=case when a.C_SY_YSMJ_LJ is null or a.C_SY_MJ_WS is null or a.C_SY_YSMJ_LJ=0 or a.C_SY_MJ_WS=0 then b.C_SY_YSMJ_LJ+b.C_SY_MJ_WS"
                    strSQL = strSQL + "   		when b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else  b.C_SY_YSMJ_LJ+b.C_SY_MJ_WS-a.C_SY_YSMJ_LJ-a.C_SY_MJ_WS  end ,"
                    strSQL = strSQL + "  签约总额=case when a.C_SY_YSMJ_LJ is null or a.C_SY_JJ_LJ is null  then b.C_SY_YSMJ_LJ*b.C_SY_JJ_LJ"
                    strSQL = strSQL + "   		when a.C_SY_YSMJ_LJ=0 or a.C_SY_JJ_LJ=0  then b.C_SY_YSMJ_LJ*b.C_SY_JJ_LJ"
                    strSQL = strSQL + " 		when b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else   b.C_SY_YSMJ_LJ*b.C_SY_JJ_LJ-a.C_SY_YSMJ_LJ*a.C_SY_JJ_LJ  end , "
                    strSQL = strSQL + "  case when b.C_SY_YSMJ_LJ-a.C_SY_YSMJ_LJ=0 or a.C_SY_YSMJ_LJ=0 or a.C_SY_YSMJ_LJ is null then 0 "
                    strSQL = strSQL + " else (b.C_SY_YSMJ_LJ*b.C_SY_JJ_LJ-a.C_SY_YSMJ_LJ*a.C_SY_JJ_LJ)/(b.C_SY_YSMJ_LJ-a.C_SY_YSMJ_LJ) end as 签约均价,"
                    strSQL = strSQL + "    b.C_SY_YSMJ_LJ * b.C_SY_JJ_LJ as 累计签约总额   "
                    strSQL = strSQL + "  from "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select b.*,c.c_house from T_HOUSE_INFO b left join T_HOUSE_MATCH c on b.C_XM_NAME=c.C_XM_NAME and b.C_XZQH=c.C_XZQY  "
                    strSQL = strSQL + "  where  b.C_TIME between @frontendDate_start and @frontendDate_end "
                    strSQL = strSQL + "  )b"
                    strSQL = strSQL + "  Left Join "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select a.*,c.c_house from T_HOUSE_INFO a left join T_HOUSE_MATCH c on a.C_XM_NAME=c.C_XM_NAME and a.C_XZQH=c.C_XZQY "
                    strSQL = strSQL + "  where a.C_TIME between @frontstartDate_start and @frontstartDate_end "
                    strSQL = strSQL + "  )a  on  a.C_XM_ID=b.C_XM_ID and a.C_XM_NAME=b.C_XM_NAME and a.C_XZQH=b.C_XZQH and a.C_XM_ADDRESS=b.C_XM_ADDRESS"
                    strSQL = strSQL + "  ) A "
                    If strWhere <> "" Then
                        strSQL = strSQL + " where " + strWhere
                    End If
                    strSQL = strSQL + " group by A.行政区域, A.楼盘名称, A.项目类型 "
                    strSQL = strSQL + " )A"

                Case "2"
                    strSQL = strSQL + " select  "
                    strSQL = strSQL + " A.行政区域 as 行政区域, "
                    strSQL = strSQL + " A.楼盘名称 as 楼盘名称, "
                    strSQL = strSQL + " A.项目类型 as 项目类型, "
                    strSQL = strSQL + " sum(A.签约套数) as 网签数, "
                    strSQL = strSQL + "  case when sum(A.签约套数) = 0 then 0 else cast(round(sum(A.签约总额)/sum(A.签约面积),2) as numeric(16,2)) end as 网签均价, "
                    strSQL = strSQL + "  sum(A.累计已售套数2) as 合共成交, "
                    strSQL = strSQL + "  sum(A.未售套数2) as 未售套数, "
                    strSQL = strSQL + "  cast(round(sum(A.签约总额),2) as numeric(16,2)) as 网签总额, "
                    strSQL = strSQL + "  cast(round(sum(A.签约面积),2) as numeric(16,2)) as 网签面积 "
                    strSQL = strSQL + "  from( "
                    strSQL = strSQL + "  select "
                    strSQL = strSQL + " b.C_ID as 序号, "
                    strSQL = strSQL + " b.C_XZQH as 行政区域,   "
                    strSQL = strSQL + " b.C_HOUSE as 楼盘名称,  "
                    strSQL = strSQL + " b.C_XM_NAME as 项目名称,  "
                    strSQL = strSQL + " b.C_XM_ID as 预售证,    "
                    strSQL = strSQL + " '住宅' as 项目类型, "
                    strSQL = strSQL + " b.C_ZZ_YSTS_LJ as 累计预售套数1,  "
                    strSQL = strSQL + " b.C_ZZ_YSMJ_LJ as 累计预售面积1, "
                    strSQL = strSQL + " b.C_ZZ_JJ_LJ as 累计均价1,  "
                    strSQL = strSQL + " b.C_ZZ_TS_WS as 未售套数1,  "
                    strSQL = strSQL + " b.C_ZZ_MJ_WS as 未售面积1,  "
                    strSQL = strSQL + " b.C_ZZ_YSTS_LJ as 累计已售套数2,   "
                    strSQL = strSQL + " b.C_ZZ_YSMJ_LJ as 累计已售面积2,  "
                    strSQL = strSQL + " b.C_ZZ_JJ_LJ as 累计均价2, "
                    strSQL = strSQL + " b.C_ZZ_TS_WS as 未售套数2,   "
                    strSQL = strSQL + " b.C_ZZ_MJ_WS as 未售面积2,  "
                    strSQL = strSQL + " 签约套数=case 	when a.C_ZZ_YSTS_LJ is null then b.C_ZZ_YSTS_LJ"
                    strSQL = strSQL + "   		when a.C_ZZ_YSTS_LJ=0 then  b.C_ZZ_YSTS_LJ"
                    strSQL = strSQL + " 		else   b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ  end , "
                    strSQL = strSQL + "  签约面积=case when a.C_ZZ_YSMJ_LJ is null or a.C_ZZ_YSMJ_LJ=0 then b.C_ZZ_YSMJ_LJ"
                    strSQL = strSQL + "   		when b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else    b.C_ZZ_YSMJ_LJ-a.C_ZZ_YSMJ_LJ  end ,"
                    strSQL = strSQL + " 套数核对=case when a.C_ZZ_YSTS_LJ is null or a.C_ZZ_TS_WS is null or a.C_ZZ_YSTS_LJ=0 or a.C_ZZ_TS_WS=0  then b.C_ZZ_YSTS_LJ+b.C_ZZ_TS_WS"
                    strSQL = strSQL + "   		when b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else b.C_ZZ_YSTS_LJ+b.C_ZZ_TS_WS-a.C_ZZ_YSTS_LJ-a.C_ZZ_TS_WS  end ,"
                    strSQL = strSQL + "  面积核对=case when a.C_ZZ_YSMJ_LJ is null or a.C_ZZ_MJ_WS is null or a.C_ZZ_YSMJ_LJ=0 or a.C_ZZ_MJ_WS=0 then b.C_ZZ_YSMJ_LJ+b.C_ZZ_MJ_WS"
                    strSQL = strSQL + "   		when b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else  b.C_ZZ_YSMJ_LJ+b.C_ZZ_MJ_WS-a.C_ZZ_YSMJ_LJ-a.C_ZZ_MJ_WS  end ,"
                    strSQL = strSQL + "  签约总额=case when a.C_ZZ_YSMJ_LJ is null or a.C_ZZ_JJ_LJ is null  then b.C_ZZ_YSMJ_LJ*b.C_ZZ_JJ_LJ"
                    strSQL = strSQL + "   		when a.C_ZZ_YSMJ_LJ=0 or a.C_ZZ_JJ_LJ=0  then b.C_ZZ_YSMJ_LJ*b.C_ZZ_JJ_LJ"
                    strSQL = strSQL + " 		when b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else   b.C_ZZ_YSMJ_LJ*b.C_ZZ_JJ_LJ-a.C_ZZ_YSMJ_LJ*a.C_ZZ_JJ_LJ  end , "
                    strSQL = strSQL + "  case when b.C_ZZ_YSMJ_LJ-a.C_ZZ_YSMJ_LJ=0 or a.C_ZZ_YSMJ_LJ=0 or a.C_ZZ_YSMJ_LJ is null then 0 "
                    strSQL = strSQL + " else (b.C_ZZ_YSMJ_LJ*b.C_ZZ_JJ_LJ-a.C_ZZ_YSMJ_LJ*a.C_ZZ_JJ_LJ)/(b.C_ZZ_YSMJ_LJ-a.C_ZZ_YSMJ_LJ) end as 签约均价,"
                    strSQL = strSQL + "    b.C_ZZ_YSMJ_LJ * b.C_ZZ_JJ_LJ as 累计签约总额   "
                    strSQL = strSQL + "  from "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select b.*,c.c_house from T_HOUSE_INFO b left join T_HOUSE_MATCH c on b.C_XM_NAME=c.C_XM_NAME and b.C_XZQH=c.C_XZQY  "
                    strSQL = strSQL + "  where  b.C_TIME between @frontendDate_start and @frontendDate_end "
                    strSQL = strSQL + "  )b"
                    strSQL = strSQL + "  Left Join "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select a.*,c.c_house from T_HOUSE_INFO a left join T_HOUSE_MATCH c on a.C_XM_NAME=c.C_XM_NAME and a.C_XZQH=c.C_XZQY "
                    strSQL = strSQL + "  where a.C_TIME between @frontstartDate_start and @frontstartDate_end "
                    strSQL = strSQL + "  )a  on  a.C_XM_ID=b.C_XM_ID and a.C_XM_NAME=b.C_XM_NAME and a.C_XZQH=b.C_XZQH and a.C_XM_ADDRESS=b.C_XM_ADDRESS"
                    strSQL = strSQL + "   ) A "
                    If strWhere <> "" Then
                        strSQL = strSQL + "where " + strWhere
                    End If
                    strSQL = strSQL + " group by A.行政区域, A.楼盘名称, A.项目类型 "

                Case "3"
                    strSQL = strSQL + " select "
                    strSQL = strSQL + " A.行政区域 as 行政区域, "
                    strSQL = strSQL + " A.楼盘名称 as 楼盘名称, "
                    strSQL = strSQL + " A.项目类型 as 项目类型, "
                    strSQL = strSQL + " sum(A.签约套数) as 网签数, "
                    strSQL = strSQL + " case when sum(A.签约套数) = 0 then 0 else cast(round(sum(A.签约总额)/sum(A.签约面积),2) as numeric(16,2)) end as 网签均价, "
                    strSQL = strSQL + " sum(A.累计已售套数2) as 合共成交, "
                    strSQL = strSQL + " sum(A.未售套数2) as 未售套数, "
                    strSQL = strSQL + " cast(round(sum(A.签约总额),2) as numeric(16,2)) as 网签总额, "
                    strSQL = strSQL + " cast(round(sum(A.签约面积),2) as numeric(16,2)) as 网签面积 "
                    strSQL = strSQL + " from( "
                    strSQL = strSQL + "  select "
                    strSQL = strSQL + " b.C_ID as 序号,  "
                    strSQL = strSQL + " b.C_XZQH as 行政区域,   "
                    strSQL = strSQL + " b.C_HOUSE as 楼盘名称,  "
                    strSQL = strSQL + " b.C_XM_NAME as 项目名称,  "
                    strSQL = strSQL + " b.C_XM_ID as 预售证,    "
                    strSQL = strSQL + " '办公' as 项目类型, "
                    strSQL = strSQL + " b.C_BG_YSTS_LJ as 累计预售套数1,  "
                    strSQL = strSQL + " b.C_BG_YSMJ_LJ as 累计预售面积1, "
                    strSQL = strSQL + " b.C_BG_JJ_LJ as 累计均价1,  "
                    strSQL = strSQL + " b.C_BG_TS_WS as 未售套数1,  "
                    strSQL = strSQL + " b.C_BG_MJ_WS as 未售面积1,  "
                    strSQL = strSQL + " b.C_BG_YSTS_LJ as 累计已售套数2,   "
                    strSQL = strSQL + " b.C_BG_YSMJ_LJ as 累计已售面积2,  "
                    strSQL = strSQL + " b.C_BG_JJ_LJ as 累计均价2, "
                    strSQL = strSQL + " b.C_BG_TS_WS as 未售套数2,   "
                    strSQL = strSQL + " b.C_BG_MJ_WS as 未售面积2,  "
                    strSQL = strSQL + " 签约套数=case 	when a.C_BG_YSTS_LJ is null then b.C_BG_YSTS_LJ"
                    strSQL = strSQL + "   		when a.C_BG_YSTS_LJ=0 then  b.C_BG_YSTS_LJ"
                    strSQL = strSQL + " 		else   b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ  end , "
                    strSQL = strSQL + "  签约面积=case when a.C_BG_YSMJ_LJ is null or a.C_BG_YSMJ_LJ=0 then b.C_BG_YSMJ_LJ"
                    strSQL = strSQL + "   		when b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else    b.C_BG_YSMJ_LJ-a.C_BG_YSMJ_LJ  end ,"
                    strSQL = strSQL + " 套数核对=case when a.C_BG_YSTS_LJ is null or a.C_BG_TS_WS is null or a.C_BG_YSTS_LJ=0 or a.C_BG_TS_WS=0  then b.C_BG_YSTS_LJ+b.C_BG_TS_WS"
                    strSQL = strSQL + "   		when b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else b.C_BG_YSTS_LJ+b.C_BG_TS_WS-a.C_BG_YSTS_LJ-a.C_BG_TS_WS  end ,"
                    strSQL = strSQL + "  面积核对=case when a.C_BG_YSMJ_LJ is null or a.C_BG_MJ_WS is null or a.C_BG_YSMJ_LJ=0 or a.C_BG_MJ_WS=0 then b.C_BG_YSMJ_LJ+b.C_BG_MJ_WS"
                    strSQL = strSQL + "   		when b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else  b.C_BG_YSMJ_LJ+b.C_BG_MJ_WS-a.C_BG_YSMJ_LJ-a.C_BG_MJ_WS  end ,"
                    strSQL = strSQL + "  签约总额=case when a.C_BG_YSMJ_LJ is null or a.C_BG_JJ_LJ is null  then b.C_BG_YSMJ_LJ*b.C_BG_JJ_LJ"
                    strSQL = strSQL + "   		when a.C_BG_YSMJ_LJ=0 or a.C_BG_JJ_LJ=0  then b.C_BG_YSMJ_LJ*b.C_BG_JJ_LJ"
                    strSQL = strSQL + " 		when b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else   b.C_BG_YSMJ_LJ*b.C_BG_JJ_LJ-a.C_BG_YSMJ_LJ*a.C_BG_JJ_LJ  end , "
                    strSQL = strSQL + "  case when b.C_BG_YSMJ_LJ-a.C_BG_YSMJ_LJ=0 or a.C_BG_YSMJ_LJ=0 or a.C_BG_YSMJ_LJ is null then 0 "
                    strSQL = strSQL + " else (b.C_BG_YSMJ_LJ*b.C_BG_JJ_LJ-a.C_BG_YSMJ_LJ*a.C_BG_JJ_LJ)/(b.C_BG_YSMJ_LJ-a.C_BG_YSMJ_LJ) end as 签约均价,"
                    strSQL = strSQL + "    b.C_BG_YSMJ_LJ * b.C_BG_JJ_LJ as 累计签约总额   "
                    strSQL = strSQL + "  from "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select b.*,c.c_house from T_HOUSE_INFO b left join T_HOUSE_MATCH c on b.C_XM_NAME=c.C_XM_NAME and b.C_XZQH=c.C_XZQY  "
                    strSQL = strSQL + "  where b.C_TIME between @frontendDate_start and @frontendDate_end "
                    strSQL = strSQL + "  )b"
                    strSQL = strSQL + "  Left Join "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select a.*,c.c_house from T_HOUSE_INFO a left join T_HOUSE_MATCH c on a.C_XM_NAME=c.C_XM_NAME and a.C_XZQH=c.C_XZQY "
                    strSQL = strSQL + "  where a.C_TIME between @frontstartDate_start and @frontstartDate_end "
                    strSQL = strSQL + "  )a  on  a.C_XM_ID=b.C_XM_ID and a.C_XM_NAME=b.C_XM_NAME and a.C_XZQH=b.C_XZQH and a.C_XM_ADDRESS=b.C_XM_ADDRESS"
                    strSQL = strSQL + "  ) A "
                    If strWhere <> "" Then
                        strSQL = strSQL + "where " + strWhere
                    End If
                    strSQL = strSQL + " group by A.行政区域, A.楼盘名称, A.项目类型"

                Case "1"
                    strSQL = strSQL + " select  "
                    strSQL = strSQL + " A.行政区域 as 行政区域, "
                    strSQL = strSQL + " A.楼盘名称 as 楼盘名称, "
                    strSQL = strSQL + " A.项目类型 as 项目类型, "
                    strSQL = strSQL + " sum(A.签约套数) as 网签数, "
                    strSQL = strSQL + " case when sum(A.签约套数) = 0 then 0 else cast(round(sum(A.签约总额)/sum(A.签约面积),2) as numeric(16,2)) end as 网签均价, "
                    strSQL = strSQL + " sum(A.累计已售套数2) as 合共成交, "
                    strSQL = strSQL + " sum(A.未售套数2) as 未售套数, "
                    strSQL = strSQL + " cast(round(sum(A.签约总额),2) as numeric(16,2)) as 网签总额, "
                    strSQL = strSQL + " cast(round(sum(A.签约面积),2) as numeric(16,2)) as 网签面积 "
                    strSQL = strSQL + " from( "
                    strSQL = strSQL + "  select "
                    strSQL = strSQL + " b.C_ID as 序号,   "
                    strSQL = strSQL + " b.C_XZQH as 行政区域,   "
                    strSQL = strSQL + " b.C_HOUSE as 楼盘名称,  "
                    strSQL = strSQL + " b.C_XM_NAME as 项目名称,  "
                    strSQL = strSQL + " b.C_XM_ID as 预售证,    "
                    strSQL = strSQL + " '商业' as 项目类型, "
                    strSQL = strSQL + " b.C_SY_YSTS_LJ as 累计预售套数1,  "
                    strSQL = strSQL + " b.C_SY_YSMJ_LJ as 累计预售面积1, "
                    strSQL = strSQL + " b.C_SY_JJ_LJ as 累计均价1,  "
                    strSQL = strSQL + " b.C_SY_TS_WS as 未售套数1,  "
                    strSQL = strSQL + " b.C_SY_MJ_WS as 未售面积1,  "
                    strSQL = strSQL + " b.C_SY_YSTS_LJ as 累计已售套数2,   "
                    strSQL = strSQL + " b.C_SY_YSMJ_LJ as 累计已售面积2,  "
                    strSQL = strSQL + " b.C_SY_JJ_LJ as 累计均价2, "
                    strSQL = strSQL + " b.C_SY_TS_WS as 未售套数2,   "
                    strSQL = strSQL + " b.C_SY_MJ_WS as 未售面积2,  "
                    strSQL = strSQL + " 签约套数=case 	when a.C_SY_YSTS_LJ is null then b.C_SY_YSTS_LJ"
                    strSQL = strSQL + "   		when a.C_SY_YSTS_LJ=0 then  b.C_SY_YSTS_LJ"
                    strSQL = strSQL + " 		else   b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ  end , "
                    strSQL = strSQL + "  签约面积=case when a.C_SY_YSMJ_LJ is null or a.C_SY_YSMJ_LJ=0 then b.C_SY_YSMJ_LJ"
                    strSQL = strSQL + "   		when b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else    b.C_SY_YSMJ_LJ-a.C_SY_YSMJ_LJ  end ,"
                    strSQL = strSQL + " 套数核对=case when a.C_SY_YSTS_LJ is null or a.C_SY_TS_WS is null or a.C_SY_YSTS_LJ=0 or a.C_SY_TS_WS=0  then b.C_SY_YSTS_LJ+b.C_SY_TS_WS"
                    strSQL = strSQL + "   		when b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else b.C_SY_YSTS_LJ+b.C_SY_TS_WS-a.C_SY_YSTS_LJ-a.C_SY_TS_WS  end ,"
                    strSQL = strSQL + "  面积核对=case when a.C_SY_YSMJ_LJ is null or a.C_SY_MJ_WS is null or a.C_SY_YSMJ_LJ=0 or a.C_SY_MJ_WS=0 then b.C_SY_YSMJ_LJ+b.C_SY_MJ_WS"
                    strSQL = strSQL + "   		when b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else  b.C_SY_YSMJ_LJ+b.C_SY_MJ_WS-a.C_SY_YSMJ_LJ-a.C_SY_MJ_WS  end ,"
                    strSQL = strSQL + "  签约总额=case when a.C_SY_YSMJ_LJ is null or a.C_SY_JJ_LJ is null  then b.C_SY_YSMJ_LJ*b.C_SY_JJ_LJ"
                    strSQL = strSQL + "   		when a.C_SY_YSMJ_LJ=0 or a.C_SY_JJ_LJ=0  then b.C_SY_YSMJ_LJ*b.C_SY_JJ_LJ"
                    strSQL = strSQL + " 		when b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else   b.C_SY_YSMJ_LJ*b.C_SY_JJ_LJ-a.C_SY_YSMJ_LJ*a.C_SY_JJ_LJ  end , "
                    strSQL = strSQL + "  case when b.C_SY_YSMJ_LJ-a.C_SY_YSMJ_LJ=0 or a.C_SY_YSMJ_LJ=0 or a.C_SY_YSMJ_LJ is null then 0 "
                    strSQL = strSQL + " else (b.C_SY_YSMJ_LJ*b.C_SY_JJ_LJ-a.C_SY_YSMJ_LJ*a.C_SY_JJ_LJ)/(b.C_SY_YSMJ_LJ-a.C_SY_YSMJ_LJ) end as 签约均价,"
                    strSQL = strSQL + "    b.C_SY_YSMJ_LJ * b.C_SY_JJ_LJ as 累计签约总额   "
                    strSQL = strSQL + "  from "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select b.*,c.c_house from T_HOUSE_INFO b left join T_HOUSE_MATCH c on b.C_XM_NAME=c.C_XM_NAME and b.C_XZQH=c.C_XZQY  "
                    strSQL = strSQL + "  where b.C_TIME between @frontendDate_start and @frontendDate_end "
                    strSQL = strSQL + "  )b"
                    strSQL = strSQL + "  Left Join "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select a.*,c.c_house from T_HOUSE_INFO a left join T_HOUSE_MATCH c on a.C_XM_NAME=c.C_XM_NAME and a.C_XZQH=c.C_XZQY "
                    strSQL = strSQL + "  where a.C_TIME between @frontstartDate_start and @frontstartDate_end "
                    strSQL = strSQL + "  )a  on  a.C_XM_ID=b.C_XM_ID and a.C_XM_NAME=b.C_XM_NAME and a.C_XZQH=b.C_XZQH and a.C_XM_ADDRESS=b.C_XM_ADDRESS"
                    strSQL = strSQL + "  ) A "
                    If strWhere <> "" Then
                        strSQL = strSQL + "where " + strWhere
                    End If
                    strSQL = strSQL + " group by A.行政区域, A.楼盘名称, A.项目类型"

                Case Else


            End Select

            getSql_FrontBuildingCompute = True
errProc:

            Exit Function
        End Function

        '----------------------------------------------------------------
        ' 根据“查询条件”获取“阳光家缘楼盘信息数据分析”完全数据的数据集
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strSQL                     ：返回的SQL语句
        '     strType                    ：选择楼盘的类型
        '     strWhere                   ：搜索字符串      
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getSql_FrontBuildingCompute_x2( _
             ByRef strErrMsg As String, _
             ByRef strSQL As String, _
             ByVal strWhere As String, _
             ByRef strType As String) As Boolean

            getSql_FrontBuildingCompute_x2 = False

            strSQL = ""
            Select Case strType
                Case "0"
                    strSQL = strSQL + " select * from ("
                    strSQL = strSQL + " select "
                    strSQL = strSQL + " A.行政区域, "
                    strSQL = strSQL + " A.楼盘名称, "
                    strSQL = strSQL + " case when 房屋类型='1' then '别墅' else '洋房' end as 项目类型, "
                    strSQL = strSQL + " sum(A.签约套数) as 网签数, "
                    strSQL = strSQL + "  case when sum(A.签约套数) = 0 then 0 else cast(round(sum(A.签约总额)/sum(A.签约面积),2) as numeric(16,2)) end as 网签均价, "
                    strSQL = strSQL + "  sum(A.累计已售套数2) as 合共成交, "
                    strSQL = strSQL + "  sum(A.未售套数2) as 未售套数, "
                    strSQL = strSQL + "  cast(round(sum(A.签约总额),2) as numeric(16,2)) as 网签总额, "
                    strSQL = strSQL + "  cast(round(sum(A.签约面积),2) as numeric(16,2)) as 网签面积 "
                    strSQL = strSQL + "  from( "
                    strSQL = strSQL + "  select "
                    strSQL = strSQL + " b.C_ID as 序号,  "
                    strSQL = strSQL + " b.C_XZQH as 行政区域,   "
                    strSQL = strSQL + " b.C_HOUSE as 楼盘名称,  "
                    strSQL = strSQL + " b.C_XM_NAME as 项目名称,  "
                    strSQL = strSQL + " b.C_XM_ID as 预售证,    "
                    strSQL = strSQL + "  isnull(b.c_type,0) as 房屋类型,    "
                    strSQL = strSQL + " b.C_ZZ_YSTS_LJ as 累计预售套数1,  "
                    strSQL = strSQL + " b.C_ZZ_YSMJ_LJ as 累计预售面积1, "
                    strSQL = strSQL + " b.C_ZZ_JJ_LJ as 累计均价1,  "
                    strSQL = strSQL + " b.C_ZZ_TS_WS as 未售套数1,  "
                    strSQL = strSQL + " b.C_ZZ_MJ_WS as 未售面积1,  "
                    strSQL = strSQL + " b.C_ZZ_YSTS_LJ as 累计已售套数2,   "
                    strSQL = strSQL + " b.C_ZZ_YSMJ_LJ as 累计已售面积2,  "
                    strSQL = strSQL + " b.C_ZZ_JJ_LJ as 累计均价2, "
                    strSQL = strSQL + " b.C_ZZ_TS_WS as 未售套数2,   "
                    strSQL = strSQL + " b.C_ZZ_MJ_WS as 未售面积2,  "
                    strSQL = strSQL + " 签约套数=case 	when a.C_ZZ_YSTS_LJ is null then b.C_ZZ_YSTS_LJ"
                    strSQL = strSQL + "   		when a.C_ZZ_YSTS_LJ=0 then  b.C_ZZ_YSTS_LJ"
                    strSQL = strSQL + " 		else   b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ  end , "
                    strSQL = strSQL + "  签约面积=case when a.C_ZZ_YSMJ_LJ is null or a.C_ZZ_YSMJ_LJ=0 then b.C_ZZ_YSMJ_LJ"
                    strSQL = strSQL + "   		when b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else    b.C_ZZ_YSMJ_LJ-a.C_ZZ_YSMJ_LJ  end ,"
                    strSQL = strSQL + " 套数核对=case when a.C_ZZ_YSTS_LJ is null or a.C_ZZ_TS_WS is null or a.C_ZZ_YSTS_LJ=0 or a.C_ZZ_TS_WS=0  then b.C_ZZ_YSTS_LJ+b.C_ZZ_TS_WS"
                    strSQL = strSQL + "   		when b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else b.C_ZZ_YSTS_LJ+b.C_ZZ_TS_WS-a.C_ZZ_YSTS_LJ-a.C_ZZ_TS_WS  end ,"
                    strSQL = strSQL + "  面积核对=case when a.C_ZZ_YSMJ_LJ is null or a.C_ZZ_MJ_WS is null or a.C_ZZ_YSMJ_LJ=0 or a.C_ZZ_MJ_WS=0 then b.C_ZZ_YSMJ_LJ+b.C_ZZ_MJ_WS"
                    strSQL = strSQL + "   		when b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else  b.C_ZZ_YSMJ_LJ+b.C_ZZ_MJ_WS-a.C_ZZ_YSMJ_LJ-a.C_ZZ_MJ_WS  end ,"
                    strSQL = strSQL + "  签约总额=case when a.C_ZZ_YSMJ_LJ is null or a.C_ZZ_JJ_LJ is null  then b.C_ZZ_YSMJ_LJ*b.C_ZZ_JJ_LJ"
                    strSQL = strSQL + "   		when a.C_ZZ_YSMJ_LJ=0 or a.C_ZZ_JJ_LJ=0  then b.C_ZZ_YSMJ_LJ*b.C_ZZ_JJ_LJ"
                    strSQL = strSQL + " 		when b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else   b.C_ZZ_YSMJ_LJ*b.C_ZZ_JJ_LJ-a.C_ZZ_YSMJ_LJ*a.C_ZZ_JJ_LJ  end , "
                    strSQL = strSQL + "  case when b.C_ZZ_YSMJ_LJ-a.C_ZZ_YSMJ_LJ=0 or a.C_ZZ_YSMJ_LJ=0 or a.C_ZZ_YSMJ_LJ is null then 0 "
                    strSQL = strSQL + " else (b.C_ZZ_YSMJ_LJ*b.C_ZZ_JJ_LJ-a.C_ZZ_YSMJ_LJ*a.C_ZZ_JJ_LJ)/(b.C_ZZ_YSMJ_LJ-a.C_ZZ_YSMJ_LJ) end as 签约均价,"
                    strSQL = strSQL + "    b.C_ZZ_YSMJ_LJ * b.C_ZZ_JJ_LJ as 累计签约总额   "
                    strSQL = strSQL + "  from "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select b.*,c.c_house,c.c_type from T_HOUSE_INFO b left join T_HOUSE_MATCH_XMID c on b.C_XM_ID=c.C_XM_ID and b.C_XM_NAME=c.C_XM_NAME and b.C_XM_ADDRESS=c.C_XM_ADDRESS "
                    strSQL = strSQL + "  where b.C_TIME  between @frontendDate_start and @frontendDate_end "
                    strSQL = strSQL + "  )b"
                    strSQL = strSQL + "  Left Join "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select a.*,c.c_house,c.c_type from T_HOUSE_INFO a left join T_HOUSE_MATCH_XMID c on a.C_XM_ID=c.C_XM_ID and a.C_XM_NAME=c.C_XM_NAME and a.C_XM_ADDRESS=c.C_XM_ADDRESS "
                    strSQL = strSQL + "  where a.C_TIME between @frontstartDate_start and @frontstartDate_end  "
                    strSQL = strSQL + "  )a  on  a.C_XM_ID=b.C_XM_ID and a.C_XM_NAME=b.C_XM_NAME and a.C_XZQH=b.C_XZQH and a.C_XM_ADDRESS=b.C_XM_ADDRESS and a.c_type=b.c_type"
                    strSQL = strSQL + "   ) A "
                    If strWhere <> "" Then
                        strSQL = strSQL + "where " + strWhere
                    End If
                    strSQL = strSQL + " group by A.行政区域, A.楼盘名称, A.房屋类型 "
                    strSQL = strSQL + " union "
                    strSQL = strSQL + " select "
                    strSQL = strSQL + " A.行政区域 as 行政区域, "
                    strSQL = strSQL + " A.楼盘名称 as 楼盘名称, "
                    strSQL = strSQL + " A.项目类型 as 项目类型, "
                    strSQL = strSQL + " sum(A.签约套数) as 网签数, "
                    strSQL = strSQL + " case when sum(A.签约套数) = 0 then 0 else cast(round(sum(A.签约总额)/sum(A.签约面积),2) as numeric(16,2)) end as 网签均价, "
                    strSQL = strSQL + " sum(A.累计已售套数2) as 合共成交, "
                    strSQL = strSQL + " sum(A.未售套数2) as 未售套数, "
                    strSQL = strSQL + " cast(round(sum(A.签约总额),2) as numeric(16,2)) as 网签总额, "
                    strSQL = strSQL + " cast(round(sum(A.签约面积),2) as numeric(16,2)) as 网签面积 "
                    strSQL = strSQL + " from( "
                    strSQL = strSQL + "  select "
                    strSQL = strSQL + " b.C_ID as 序号, "
                    strSQL = strSQL + " b.C_XZQH as 行政区域,   "
                    strSQL = strSQL + " b.C_HOUSE as 楼盘名称,  "
                    strSQL = strSQL + " b.C_XM_NAME as 项目名称,  "
                    strSQL = strSQL + " b.C_XM_ID as 预售证,    "
                    strSQL = strSQL + " '办公' as 项目类型, "
                    strSQL = strSQL + " b.C_BG_YSTS_LJ as 累计预售套数1,  "
                    strSQL = strSQL + " b.C_BG_YSMJ_LJ as 累计预售面积1, "
                    strSQL = strSQL + " b.C_BG_JJ_LJ as 累计均价1,  "
                    strSQL = strSQL + " b.C_BG_TS_WS as 未售套数1,  "
                    strSQL = strSQL + " b.C_BG_MJ_WS as 未售面积1,  "
                    strSQL = strSQL + " b.C_BG_YSTS_LJ as 累计已售套数2,   "
                    strSQL = strSQL + " b.C_BG_YSMJ_LJ as 累计已售面积2,  "
                    strSQL = strSQL + " b.C_BG_JJ_LJ as 累计均价2, "
                    strSQL = strSQL + " b.C_BG_TS_WS as 未售套数2,   "
                    strSQL = strSQL + " b.C_BG_MJ_WS as 未售面积2,  "
                    strSQL = strSQL + " 签约套数=case 	when a.C_BG_YSTS_LJ is null then b.C_BG_YSTS_LJ"
                    strSQL = strSQL + "   		when a.C_BG_YSTS_LJ=0 then  b.C_BG_YSTS_LJ"
                    strSQL = strSQL + " 		else   b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ  end , "
                    strSQL = strSQL + "  签约面积=case when a.C_BG_YSMJ_LJ is null or a.C_BG_YSMJ_LJ=0 then b.C_BG_YSMJ_LJ"
                    strSQL = strSQL + "   		when b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else    b.C_BG_YSMJ_LJ-a.C_BG_YSMJ_LJ  end ,"
                    strSQL = strSQL + " 套数核对=case when a.C_BG_YSTS_LJ is null or a.C_BG_TS_WS is null or a.C_BG_YSTS_LJ=0 or a.C_BG_TS_WS=0  then b.C_BG_YSTS_LJ+b.C_BG_TS_WS"
                    strSQL = strSQL + "   		when b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else b.C_BG_YSTS_LJ+b.C_BG_TS_WS-a.C_BG_YSTS_LJ-a.C_BG_TS_WS  end ,"
                    strSQL = strSQL + "  面积核对=case when a.C_BG_YSMJ_LJ is null or a.C_BG_MJ_WS is null or a.C_BG_YSMJ_LJ=0 or a.C_BG_MJ_WS=0 then b.C_BG_YSMJ_LJ+b.C_BG_MJ_WS"
                    strSQL = strSQL + "   		when b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else  b.C_BG_YSMJ_LJ+b.C_BG_MJ_WS-a.C_BG_YSMJ_LJ-a.C_BG_MJ_WS  end ,"
                    strSQL = strSQL + "  签约总额=case when a.C_BG_YSMJ_LJ is null or a.C_BG_JJ_LJ is null  then b.C_BG_YSMJ_LJ*b.C_BG_JJ_LJ"
                    strSQL = strSQL + "   		when a.C_BG_YSMJ_LJ=0 or a.C_BG_JJ_LJ=0  then b.C_BG_YSMJ_LJ*b.C_BG_JJ_LJ"
                    strSQL = strSQL + " 		when b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else   b.C_BG_YSMJ_LJ*b.C_BG_JJ_LJ-a.C_BG_YSMJ_LJ*a.C_BG_JJ_LJ  end , "
                    strSQL = strSQL + "  case when b.C_BG_YSMJ_LJ-a.C_BG_YSMJ_LJ=0 or a.C_BG_YSMJ_LJ=0 or a.C_BG_YSMJ_LJ is null then 0 "
                    strSQL = strSQL + " else (b.C_BG_YSMJ_LJ*b.C_BG_JJ_LJ-a.C_BG_YSMJ_LJ*a.C_BG_JJ_LJ)/(b.C_BG_YSMJ_LJ-a.C_BG_YSMJ_LJ) end as 签约均价,"
                    strSQL = strSQL + "    b.C_BG_YSMJ_LJ * b.C_BG_JJ_LJ as 累计签约总额   "
                    strSQL = strSQL + "  from "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select b.*,c.c_house from T_HOUSE_INFO b left join T_HOUSE_MATCH c on b.C_XM_NAME=c.C_XM_NAME and b.C_XZQH=c.C_XZQY  "
                    strSQL = strSQL + "  where b.C_TIME between @frontendDate_start and @frontendDate_end "
                    strSQL = strSQL + "  )b"
                    strSQL = strSQL + "  Left Join "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select a.*,c.c_house from T_HOUSE_INFO a left join T_HOUSE_MATCH c on a.C_XM_NAME=c.C_XM_NAME and a.C_XZQH=c.C_XZQY "
                    strSQL = strSQL + "  where a.C_TIME between @frontstartDate_start and @frontstartDate_end "
                    strSQL = strSQL + "  )a  on  a.C_XM_ID=b.C_XM_ID and a.C_XM_NAME=b.C_XM_NAME and a.C_XZQH=b.C_XZQH and a.C_XM_ADDRESS=b.C_XM_ADDRESS"
                    strSQL = strSQL + "  ) A "
                    If strWhere <> "" Then
                        strSQL = strSQL + "where " + strWhere
                    End If
                    strSQL = strSQL + " group by A.行政区域, A.楼盘名称, A.项目类型 "
                    strSQL = strSQL + " union "
                    strSQL = strSQL + " select "
                    strSQL = strSQL + " A.行政区域 as 行政区域, "
                    strSQL = strSQL + " A.楼盘名称 as 楼盘名称, "
                    strSQL = strSQL + " A.项目类型 as 项目类型, "
                    strSQL = strSQL + " sum(A.签约套数) as 网签数, "
                    strSQL = strSQL + " case when sum(A.签约套数) = 0 then 0 else cast(round(sum(A.签约总额)/sum(A.签约面积),2) as numeric(16,2)) end as 网签均价, "
                    strSQL = strSQL + " sum(A.累计已售套数2) as 合共成交, "
                    strSQL = strSQL + " sum(A.未售套数2) as 未售套数, "
                    strSQL = strSQL + " cast(round(sum(A.签约总额),2) as numeric(16,2)) as 网签总额, "
                    strSQL = strSQL + " cast(round(sum(A.签约面积),2) as numeric(16,2)) as 网签面积 "
                    strSQL = strSQL + " from( "
                    strSQL = strSQL + "  select "
                    strSQL = strSQL + " b.C_ID as 序号,    "
                    strSQL = strSQL + " b.C_XZQH as 行政区域,   "
                    strSQL = strSQL + " b.C_HOUSE as 楼盘名称,  "
                    strSQL = strSQL + " b.C_XM_NAME as 项目名称,  "
                    strSQL = strSQL + " b.C_XM_ID as 预售证,    "
                    strSQL = strSQL + " '商业' as 项目类型, "
                    strSQL = strSQL + " b.C_SY_YSTS_LJ as 累计预售套数1,  "
                    strSQL = strSQL + " b.C_SY_YSMJ_LJ as 累计预售面积1, "
                    strSQL = strSQL + " b.C_SY_JJ_LJ as 累计均价1,  "
                    strSQL = strSQL + " b.C_SY_TS_WS as 未售套数1,  "
                    strSQL = strSQL + " b.C_SY_MJ_WS as 未售面积1,  "
                    strSQL = strSQL + " b.C_SY_YSTS_LJ as 累计已售套数2,   "
                    strSQL = strSQL + " b.C_SY_YSMJ_LJ as 累计已售面积2,  "
                    strSQL = strSQL + " b.C_SY_JJ_LJ as 累计均价2, "
                    strSQL = strSQL + " b.C_SY_TS_WS as 未售套数2,   "
                    strSQL = strSQL + " b.C_SY_MJ_WS as 未售面积2,  "
                    strSQL = strSQL + " 签约套数=case 	when a.C_SY_YSTS_LJ is null then b.C_SY_YSTS_LJ"
                    strSQL = strSQL + "   		when a.C_SY_YSTS_LJ=0 then  b.C_SY_YSTS_LJ"
                    strSQL = strSQL + " 		else   b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ  end , "
                    strSQL = strSQL + "  签约面积=case when a.C_SY_YSMJ_LJ is null or a.C_SY_YSMJ_LJ=0 then b.C_SY_YSMJ_LJ"
                    strSQL = strSQL + "   		when b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else    b.C_SY_YSMJ_LJ-a.C_SY_YSMJ_LJ  end ,"
                    strSQL = strSQL + " 套数核对=case when a.C_SY_YSTS_LJ is null or a.C_SY_TS_WS is null or a.C_SY_YSTS_LJ=0 or a.C_SY_TS_WS=0  then b.C_SY_YSTS_LJ+b.C_SY_TS_WS"
                    strSQL = strSQL + "   		when b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else b.C_SY_YSTS_LJ+b.C_SY_TS_WS-a.C_SY_YSTS_LJ-a.C_SY_TS_WS  end ,"
                    strSQL = strSQL + "  面积核对=case when a.C_SY_YSMJ_LJ is null or a.C_SY_MJ_WS is null or a.C_SY_YSMJ_LJ=0 or a.C_SY_MJ_WS=0 then b.C_SY_YSMJ_LJ+b.C_SY_MJ_WS"
                    strSQL = strSQL + "   		when b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else  b.C_SY_YSMJ_LJ+b.C_SY_MJ_WS-a.C_SY_YSMJ_LJ-a.C_SY_MJ_WS  end ,"
                    strSQL = strSQL + "  签约总额=case when a.C_SY_YSMJ_LJ is null or a.C_SY_JJ_LJ is null  then b.C_SY_YSMJ_LJ*b.C_SY_JJ_LJ"
                    strSQL = strSQL + "   		when a.C_SY_YSMJ_LJ=0 or a.C_SY_JJ_LJ=0  then b.C_SY_YSMJ_LJ*b.C_SY_JJ_LJ"
                    strSQL = strSQL + " 		when b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else   b.C_SY_YSMJ_LJ*b.C_SY_JJ_LJ-a.C_SY_YSMJ_LJ*a.C_SY_JJ_LJ  end , "
                    strSQL = strSQL + "  case when b.C_SY_YSMJ_LJ-a.C_SY_YSMJ_LJ=0 or a.C_SY_YSMJ_LJ=0 or a.C_SY_YSMJ_LJ is null then 0 "
                    strSQL = strSQL + " else (b.C_SY_YSMJ_LJ*b.C_SY_JJ_LJ-a.C_SY_YSMJ_LJ*a.C_SY_JJ_LJ)/(b.C_SY_YSMJ_LJ-a.C_SY_YSMJ_LJ) end as 签约均价,"
                    strSQL = strSQL + "    b.C_SY_YSMJ_LJ * b.C_SY_JJ_LJ as 累计签约总额   "
                    strSQL = strSQL + "  from "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select b.*,c.c_house from T_HOUSE_INFO b left join T_HOUSE_MATCH c on b.C_XM_NAME=c.C_XM_NAME and b.C_XZQH=c.C_XZQY  "
                    strSQL = strSQL + "  where  b.C_TIME between @frontendDate_start and @frontendDate_end "
                    strSQL = strSQL + "  )b"
                    strSQL = strSQL + "  Left Join "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select a.*,c.c_house from T_HOUSE_INFO a left join T_HOUSE_MATCH c on a.C_XM_NAME=c.C_XM_NAME and a.C_XZQH=c.C_XZQY "
                    strSQL = strSQL + "  where a.C_TIME between @frontstartDate_start and @frontstartDate_end "
                    strSQL = strSQL + "  )a  on  a.C_XM_ID=b.C_XM_ID and a.C_XM_NAME=b.C_XM_NAME and a.C_XZQH=b.C_XZQH and a.C_XM_ADDRESS=b.C_XM_ADDRESS"
                    strSQL = strSQL + "  ) A "
                    If strWhere <> "" Then
                        strSQL = strSQL + " where " + strWhere
                    End If
                    strSQL = strSQL + " group by A.行政区域, A.楼盘名称, A.项目类型 "
                    strSQL = strSQL + " )A"

                Case "2"
                    strSQL = strSQL + " select a.*,isnull(b.i_sort,100000) as '楼盘排序' from "
                    strSQL = strSQL + " (	"
                    strSQL = strSQL + " select "
                    strSQL = strSQL + " A.行政区域, "
                    strSQL = strSQL + " A.楼盘名称, "
                    strSQL = strSQL + " case when 房屋类型='1' then '别墅' else '洋房' end as 项目类型, "
                    strSQL = strSQL + " sum(A.签约套数) as 网签数, "
                    strSQL = strSQL + "  case when sum(A.签约套数) = 0 then 0 else cast(round(sum(A.签约总额)/sum(A.签约面积),2) as numeric(16,2)) end as 网签均价, "
                    strSQL = strSQL + "  sum(A.累计已售套数2) as 合共成交, "
                    strSQL = strSQL + "  sum(A.未售套数2) as 未售套数, "
                    strSQL = strSQL + "  cast(round(sum(A.签约总额),2) as numeric(16,2)) as 网签总额, "
                    strSQL = strSQL + "  cast(round(sum(A.签约面积),2) as numeric(16,2)) as 网签面积 "
                    strSQL = strSQL + "  from( "
                    strSQL = strSQL + "  select "
                    strSQL = strSQL + " b.C_ID as 序号,  "
                    strSQL = strSQL + " b.C_XZQH as 行政区域,   "
                    strSQL = strSQL + " b.C_HOUSE as 楼盘名称,  "
                    strSQL = strSQL + " b.C_XM_NAME as 项目名称,  "
                    strSQL = strSQL + " b.C_XM_ID as 预售证,    "
                    strSQL = strSQL + "  isnull(b.c_type,0) as 房屋类型,    "
                    strSQL = strSQL + " b.C_ZZ_YSTS_LJ as 累计预售套数1,  "
                    strSQL = strSQL + " b.C_ZZ_YSMJ_LJ as 累计预售面积1, "
                    strSQL = strSQL + " b.C_ZZ_JJ_LJ as 累计均价1,  "
                    strSQL = strSQL + " b.C_ZZ_TS_WS as 未售套数1,  "
                    strSQL = strSQL + " b.C_ZZ_MJ_WS as 未售面积1,  "
                    strSQL = strSQL + " b.C_ZZ_YSTS_LJ as 累计已售套数2,   "
                    strSQL = strSQL + " b.C_ZZ_YSMJ_LJ as 累计已售面积2,  "
                    strSQL = strSQL + " b.C_ZZ_JJ_LJ as 累计均价2, "
                    strSQL = strSQL + " b.C_ZZ_TS_WS as 未售套数2,   "
                    strSQL = strSQL + " b.C_ZZ_MJ_WS as 未售面积2,  "
                    strSQL = strSQL + " 签约套数=case 	when a.C_ZZ_YSTS_LJ is null then b.C_ZZ_YSTS_LJ"
                    strSQL = strSQL + "   		when a.C_ZZ_YSTS_LJ=0 then  b.C_ZZ_YSTS_LJ"
                    strSQL = strSQL + " 		else   b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ  end , "
                    strSQL = strSQL + "  签约面积=case when a.C_ZZ_YSMJ_LJ is null or a.C_ZZ_YSMJ_LJ=0 then b.C_ZZ_YSMJ_LJ"
                    strSQL = strSQL + "   		when b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else    b.C_ZZ_YSMJ_LJ-a.C_ZZ_YSMJ_LJ  end ,"
                    strSQL = strSQL + " 套数核对=case when a.C_ZZ_YSTS_LJ is null or a.C_ZZ_TS_WS is null or a.C_ZZ_YSTS_LJ=0 or a.C_ZZ_TS_WS=0  then b.C_ZZ_YSTS_LJ+b.C_ZZ_TS_WS"
                    strSQL = strSQL + "   		when b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else b.C_ZZ_YSTS_LJ+b.C_ZZ_TS_WS-a.C_ZZ_YSTS_LJ-a.C_ZZ_TS_WS  end ,"
                    strSQL = strSQL + "  面积核对=case when a.C_ZZ_YSMJ_LJ is null or a.C_ZZ_MJ_WS is null or a.C_ZZ_YSMJ_LJ=0 or a.C_ZZ_MJ_WS=0 then b.C_ZZ_YSMJ_LJ+b.C_ZZ_MJ_WS"
                    strSQL = strSQL + "   		when b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else  b.C_ZZ_YSMJ_LJ+b.C_ZZ_MJ_WS-a.C_ZZ_YSMJ_LJ-a.C_ZZ_MJ_WS  end ,"
                    strSQL = strSQL + "  签约总额=case when a.C_ZZ_YSMJ_LJ is null or a.C_ZZ_JJ_LJ is null  then b.C_ZZ_YSMJ_LJ*b.C_ZZ_JJ_LJ"
                    strSQL = strSQL + "   		when a.C_ZZ_YSMJ_LJ=0 or a.C_ZZ_JJ_LJ=0  then b.C_ZZ_YSMJ_LJ*b.C_ZZ_JJ_LJ"
                    strSQL = strSQL + " 		when b.C_ZZ_YSTS_LJ-a.C_ZZ_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else   b.C_ZZ_YSMJ_LJ*b.C_ZZ_JJ_LJ-a.C_ZZ_YSMJ_LJ*a.C_ZZ_JJ_LJ  end , "
                    strSQL = strSQL + "  case when b.C_ZZ_YSMJ_LJ-a.C_ZZ_YSMJ_LJ=0 or a.C_ZZ_YSMJ_LJ=0 or a.C_ZZ_YSMJ_LJ is null then 0 "
                    strSQL = strSQL + " else (b.C_ZZ_YSMJ_LJ*b.C_ZZ_JJ_LJ-a.C_ZZ_YSMJ_LJ*a.C_ZZ_JJ_LJ)/(b.C_ZZ_YSMJ_LJ-a.C_ZZ_YSMJ_LJ) end as 签约均价,"
                    strSQL = strSQL + "    b.C_ZZ_YSMJ_LJ * b.C_ZZ_JJ_LJ as 累计签约总额   "
                    strSQL = strSQL + "  from "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select b.*,c.c_house,c.c_type from T_HOUSE_INFO b left join T_HOUSE_MATCH_XMID c on b.C_XM_ID=c.C_XM_ID and b.C_XM_NAME=c.C_XM_NAME"
                    strSQL = strSQL + "  where b.C_TIME  between @frontendDate_start and @frontendDate_end "
                    strSQL = strSQL + "  )b"
                    strSQL = strSQL + "  Left Join "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select a.*,c.c_house,c.c_type from T_HOUSE_INFO a left join T_HOUSE_MATCH_XMID c on a.C_XM_ID=c.C_XM_ID and a.C_XM_NAME=c.C_XM_NAME"
                    strSQL = strSQL + "  where a.C_TIME between @frontstartDate_start and @frontstartDate_end  "
                    strSQL = strSQL + "  )a  on  a.C_XM_ID=b.C_XM_ID and a.C_XM_NAME=b.C_XM_NAME and a.C_XZQH=b.C_XZQH and a.C_XM_ADDRESS=b.C_XM_ADDRESS and a.c_type=b.c_type"
                    strSQL = strSQL + "   ) A "
                    If strWhere <> "" Then
                        strSQL = strSQL + "where " + strWhere
                    End If
                    strSQL = strSQL + " group by A.行政区域, A.楼盘名称, A.房屋类型 "
                    strSQL = strSQL + " )a left join (select * from T_HOUSE_MATCH_SORT where i_type=convert(integer,'" + strType + "'))b on b.c_NAME=a.楼盘名称  "

                Case "3"
                    strSQL = strSQL + " select a.*,isnull(b.i_sort,100000) as '楼盘排序' from "
                    strSQL = strSQL + " (	"
                    strSQL = strSQL + " select "
                    strSQL = strSQL + " A.行政区域 as 行政区域, "
                    strSQL = strSQL + " A.楼盘名称 as 楼盘名称, "
                    strSQL = strSQL + " A.项目类型 as 项目类型, "
                    strSQL = strSQL + " sum(A.签约套数) as 网签数, "
                    strSQL = strSQL + " case when sum(A.签约套数) = 0 then 0 else cast(round(sum(A.签约总额)/sum(A.签约面积),2) as numeric(16,2)) end as 网签均价, "
                    strSQL = strSQL + " sum(A.累计已售套数2) as 合共成交, "
                    strSQL = strSQL + " sum(A.未售套数2) as 未售套数, "
                    strSQL = strSQL + " cast(round(sum(A.签约总额),2) as numeric(16,2)) as 网签总额, "
                    strSQL = strSQL + " cast(round(sum(A.签约面积),2) as numeric(16,2)) as 网签面积 "
                    strSQL = strSQL + " from( "
                    strSQL = strSQL + "  select "
                    strSQL = strSQL + " b.C_ID as 序号,  "
                    strSQL = strSQL + " b.C_XZQH as 行政区域,   "
                    strSQL = strSQL + " b.C_HOUSE as 楼盘名称,  "
                    strSQL = strSQL + " b.C_XM_NAME as 项目名称,  "
                    strSQL = strSQL + " b.C_XM_ID as 预售证,    "
                    strSQL = strSQL + " '办公' as 项目类型, "
                    strSQL = strSQL + " b.C_BG_YSTS_LJ as 累计预售套数1,  "
                    strSQL = strSQL + " b.C_BG_YSMJ_LJ as 累计预售面积1, "
                    strSQL = strSQL + " b.C_BG_JJ_LJ as 累计均价1,  "
                    strSQL = strSQL + " b.C_BG_TS_WS as 未售套数1,  "
                    strSQL = strSQL + " b.C_BG_MJ_WS as 未售面积1,  "
                    strSQL = strSQL + " b.C_BG_YSTS_LJ as 累计已售套数2,   "
                    strSQL = strSQL + " b.C_BG_YSMJ_LJ as 累计已售面积2,  "
                    strSQL = strSQL + " b.C_BG_JJ_LJ as 累计均价2, "
                    strSQL = strSQL + " b.C_BG_TS_WS as 未售套数2,   "
                    strSQL = strSQL + " b.C_BG_MJ_WS as 未售面积2,  "
                    strSQL = strSQL + " 签约套数=case 	when a.C_BG_YSTS_LJ is null then b.C_BG_YSTS_LJ"
                    strSQL = strSQL + "   		when a.C_BG_YSTS_LJ=0 then  b.C_BG_YSTS_LJ"
                    strSQL = strSQL + " 		else   b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ  end , "
                    strSQL = strSQL + "  签约面积=case when a.C_BG_YSMJ_LJ is null or a.C_BG_YSMJ_LJ=0 then b.C_BG_YSMJ_LJ"
                    strSQL = strSQL + "   		when b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else    b.C_BG_YSMJ_LJ-a.C_BG_YSMJ_LJ  end ,"
                    strSQL = strSQL + " 套数核对=case when a.C_BG_YSTS_LJ is null or a.C_BG_TS_WS is null or a.C_BG_YSTS_LJ=0 or a.C_BG_TS_WS=0  then b.C_BG_YSTS_LJ+b.C_BG_TS_WS"
                    strSQL = strSQL + "   		when b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else b.C_BG_YSTS_LJ+b.C_BG_TS_WS-a.C_BG_YSTS_LJ-a.C_BG_TS_WS  end ,"
                    strSQL = strSQL + "  面积核对=case when a.C_BG_YSMJ_LJ is null or a.C_BG_MJ_WS is null or a.C_BG_YSMJ_LJ=0 or a.C_BG_MJ_WS=0 then b.C_BG_YSMJ_LJ+b.C_BG_MJ_WS"
                    strSQL = strSQL + "   		when b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else  b.C_BG_YSMJ_LJ+b.C_BG_MJ_WS-a.C_BG_YSMJ_LJ-a.C_BG_MJ_WS  end ,"
                    strSQL = strSQL + "  签约总额=case when a.C_BG_YSMJ_LJ is null or a.C_BG_JJ_LJ is null  then b.C_BG_YSMJ_LJ*b.C_BG_JJ_LJ"
                    strSQL = strSQL + "   		when a.C_BG_YSMJ_LJ=0 or a.C_BG_JJ_LJ=0  then b.C_BG_YSMJ_LJ*b.C_BG_JJ_LJ"
                    strSQL = strSQL + " 		when b.C_BG_YSTS_LJ-a.C_BG_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else   b.C_BG_YSMJ_LJ*b.C_BG_JJ_LJ-a.C_BG_YSMJ_LJ*a.C_BG_JJ_LJ  end , "
                    strSQL = strSQL + "  case when b.C_BG_YSMJ_LJ-a.C_BG_YSMJ_LJ=0 or a.C_BG_YSMJ_LJ=0 or a.C_BG_YSMJ_LJ is null then 0 "
                    strSQL = strSQL + " else (b.C_BG_YSMJ_LJ*b.C_BG_JJ_LJ-a.C_BG_YSMJ_LJ*a.C_BG_JJ_LJ)/(b.C_BG_YSMJ_LJ-a.C_BG_YSMJ_LJ) end as 签约均价,"
                    strSQL = strSQL + "    b.C_BG_YSMJ_LJ * b.C_BG_JJ_LJ as 累计签约总额   "
                    strSQL = strSQL + "  from "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select b.*,c.c_house from T_HOUSE_INFO b left join T_HOUSE_MATCH_XMID c on b.C_XM_NAME=c.C_XM_NAME and b.C_XM_ID=c.C_XM_ID  "
                    strSQL = strSQL + "  where b.C_TIME  between @frontendDate_start and @frontendDate_end "
                    strSQL = strSQL + "  )b"
                    strSQL = strSQL + "  Left Join "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select a.*,c.c_house from T_HOUSE_INFO a left join T_HOUSE_MATCH_XMID c on a.C_XM_NAME=c.C_XM_NAME and a.C_XM_ID=c.C_XM_ID "
                    strSQL = strSQL + "  where a.C_TIME between @frontstartDate_start and @frontstartDate_end  "
                    strSQL = strSQL + "  )a  on  a.C_XM_ID=b.C_XM_ID and a.C_XM_NAME=b.C_XM_NAME and a.C_XZQH=b.C_XZQH and a.C_XM_ADDRESS=b.C_XM_ADDRESS"
                    strSQL = strSQL + "  ) A "
                    If strWhere <> "" Then
                        strSQL = strSQL + "where " + strWhere
                    End If
                    strSQL = strSQL + " group by A.行政区域, A.楼盘名称, A.项目类型"
                    strSQL = strSQL + " )a left join (select * from T_HOUSE_MATCH_SORT where i_type=convert(integer,'" + strType + "')) b on b.c_NAME=a.楼盘名称 "
                Case "1"
                    strSQL = strSQL + " select a.*,isnull(b.i_sort,100000) as '楼盘排序' from "
                    strSQL = strSQL + " (	"
                    strSQL = strSQL + " select  "
                    strSQL = strSQL + " A.行政区域 as 行政区域, "
                    strSQL = strSQL + " A.楼盘名称 as 楼盘名称, "
                    strSQL = strSQL + " A.项目类型 as 项目类型, "
                    strSQL = strSQL + " sum(A.签约套数) as 网签数, "
                    strSQL = strSQL + " case when sum(A.签约套数) = 0 then 0 else cast(round(sum(A.签约总额)/sum(A.签约面积),2) as numeric(16,2)) end as 网签均价, "
                    strSQL = strSQL + " sum(A.累计已售套数2) as 合共成交, "
                    strSQL = strSQL + " sum(A.未售套数2) as 未售套数, "
                    strSQL = strSQL + " cast(round(sum(A.签约总额),2) as numeric(16,2)) as 网签总额, "
                    strSQL = strSQL + " cast(round(sum(A.签约面积),2) as numeric(16,2)) as 网签面积 "
                    strSQL = strSQL + " from( "
                    strSQL = strSQL + "  select "
                    strSQL = strSQL + " b.C_ID as 序号,   "
                    strSQL = strSQL + " b.C_XZQH as 行政区域,   "
                    strSQL = strSQL + " b.C_HOUSE as 楼盘名称,  "
                    strSQL = strSQL + " b.C_XM_NAME as 项目名称,  "
                    strSQL = strSQL + " b.C_XM_ID as 预售证,    "
                    strSQL = strSQL + " '商业' as 项目类型, "
                    strSQL = strSQL + " b.C_SY_YSTS_LJ as 累计预售套数1,  "
                    strSQL = strSQL + " b.C_SY_YSMJ_LJ as 累计预售面积1, "
                    strSQL = strSQL + " b.C_SY_JJ_LJ as 累计均价1,  "
                    strSQL = strSQL + " b.C_SY_TS_WS as 未售套数1,  "
                    strSQL = strSQL + " b.C_SY_MJ_WS as 未售面积1,  "
                    strSQL = strSQL + " b.C_SY_YSTS_LJ as 累计已售套数2,   "
                    strSQL = strSQL + " b.C_SY_YSMJ_LJ as 累计已售面积2,  "
                    strSQL = strSQL + " b.C_SY_JJ_LJ as 累计均价2, "
                    strSQL = strSQL + " b.C_SY_TS_WS as 未售套数2,   "
                    strSQL = strSQL + " b.C_SY_MJ_WS as 未售面积2,  "
                    strSQL = strSQL + " 签约套数=case 	when a.C_SY_YSTS_LJ is null then b.C_SY_YSTS_LJ"
                    strSQL = strSQL + "   		when a.C_SY_YSTS_LJ=0 then  b.C_SY_YSTS_LJ"
                    strSQL = strSQL + " 		else   b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ  end , "
                    strSQL = strSQL + "  签约面积=case when a.C_SY_YSMJ_LJ is null or a.C_SY_YSMJ_LJ=0 then b.C_SY_YSMJ_LJ"
                    strSQL = strSQL + "   		when b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else    b.C_SY_YSMJ_LJ-a.C_SY_YSMJ_LJ  end ,"
                    strSQL = strSQL + " 套数核对=case when a.C_SY_YSTS_LJ is null or a.C_SY_TS_WS is null or a.C_SY_YSTS_LJ=0 or a.C_SY_TS_WS=0  then b.C_SY_YSTS_LJ+b.C_SY_TS_WS"
                    strSQL = strSQL + "   		when b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else b.C_SY_YSTS_LJ+b.C_SY_TS_WS-a.C_SY_YSTS_LJ-a.C_SY_TS_WS  end ,"
                    strSQL = strSQL + "  面积核对=case when a.C_SY_YSMJ_LJ is null or a.C_SY_MJ_WS is null or a.C_SY_YSMJ_LJ=0 or a.C_SY_MJ_WS=0 then b.C_SY_YSMJ_LJ+b.C_SY_MJ_WS"
                    strSQL = strSQL + "   		when b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else  b.C_SY_YSMJ_LJ+b.C_SY_MJ_WS-a.C_SY_YSMJ_LJ-a.C_SY_MJ_WS  end ,"
                    strSQL = strSQL + "  签约总额=case when a.C_SY_YSMJ_LJ is null or a.C_SY_JJ_LJ is null  then b.C_SY_YSMJ_LJ*b.C_SY_JJ_LJ"
                    strSQL = strSQL + "   		when a.C_SY_YSMJ_LJ=0 or a.C_SY_JJ_LJ=0  then b.C_SY_YSMJ_LJ*b.C_SY_JJ_LJ"
                    strSQL = strSQL + " 		when b.C_SY_YSTS_LJ-a.C_SY_YSTS_LJ=0 then  0"
                    strSQL = strSQL + " 		else   b.C_SY_YSMJ_LJ*b.C_SY_JJ_LJ-a.C_SY_YSMJ_LJ*a.C_SY_JJ_LJ  end , "
                    strSQL = strSQL + "  case when b.C_SY_YSMJ_LJ-a.C_SY_YSMJ_LJ=0 or a.C_SY_YSMJ_LJ=0 or a.C_SY_YSMJ_LJ is null then 0 "
                    strSQL = strSQL + " else (b.C_SY_YSMJ_LJ*b.C_SY_JJ_LJ-a.C_SY_YSMJ_LJ*a.C_SY_JJ_LJ)/(b.C_SY_YSMJ_LJ-a.C_SY_YSMJ_LJ) end as 签约均价,"
                    strSQL = strSQL + "    b.C_SY_YSMJ_LJ * b.C_SY_JJ_LJ as 累计签约总额   "
                    strSQL = strSQL + "  from "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select b.*,c.c_house from T_HOUSE_INFO b left join T_HOUSE_MATCH_XMID c on b.C_XM_NAME=c.C_XM_NAME and b.C_XM_ID=c.C_XM_ID  "
                    strSQL = strSQL + "  where b.C_TIME  between @frontendDate_start and @frontendDate_end "
                    strSQL = strSQL + "  )b"
                    strSQL = strSQL + "  Left Join "
                    strSQL = strSQL + "  ("
                    strSQL = strSQL + "  select a.*,c.c_house from T_HOUSE_INFO a left join T_HOUSE_MATCH_XMID c on a.C_XM_NAME=c.C_XM_NAME and a.C_XM_ID=c.C_XM_ID "
                    strSQL = strSQL + "  where a.C_TIME between @frontstartDate_start and @frontstartDate_end  "
                    strSQL = strSQL + "  )a  on  a.C_XM_ID=b.C_XM_ID and a.C_XM_NAME=b.C_XM_NAME and a.C_XZQH=b.C_XZQH and a.C_XM_ADDRESS=b.C_XM_ADDRESS"
                    strSQL = strSQL + "  ) A "
                    If strWhere <> "" Then
                        strSQL = strSQL + "where " + strWhere
                    End If
                    strSQL = strSQL + " group by A.行政区域, A.楼盘名称, A.项目类型"
                    strSQL = strSQL + " )a left join (select * from T_HOUSE_MATCH_SORT where i_type=convert(integer,'" + strType + "')) b on b.c_NAME=a.楼盘名称 "

                Case Else


            End Select

            getSql_FrontBuildingCompute_x2 = True
errProc:

            Exit Function
        End Function

        '----------------------------------------------------------------
        ' 根据“查询条件”获取“阳光家缘楼盘信息数据分析”完全数据的数据集
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strSQL                     ：返回的SQL语句
        '     strType                    ：选择楼盘的类型。0-全部；1-商业；2-住宅；3-办公
        '     strWhere                   ：搜索字符串      
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getSql_FrontBuildingCompute_x3( _
             ByRef strErrMsg As String, _
             ByRef strSQL As String, _
             ByVal strWhere As String, _
             ByRef strType As String) As Boolean

            getSql_FrontBuildingCompute_x3 = False

            strSQL = ""
            Select Case strType

                Case "2"
                    strSQL = strSQL + "  select a.*,isnull(b.i_sort,100000) as '楼盘排序' from "
                    strSQL = strSQL + "  (	"
                    strSQL = strSQL + "     select b.行政区域,b.楼盘名称,case when b.房屋类型='1' then '别墅' else '洋房' end as 项目类型, "
                    strSQL = strSQL + "        a.累计已售套数 as 累计已售套数1,  "
                    strSQL = strSQL + "        a.累计已售面积 as 累计已售面积1, "
                    strSQL = strSQL + "        a.未售套数 as 未售套数1,  "
                    strSQL = strSQL + "        a.未售面积 as 未售面积1,  "
                    strSQL = strSQL + "        b.累计已售套数 as 合共成交,   "
                    strSQL = strSQL + "        b.累计已售面积 as 累计已售面积, "
                    strSQL = strSQL + "        b.未售套数 as 未售套数,   "
                    strSQL = strSQL + "        b.未售面积 as 未售面积,  "
                    strSQL = strSQL + "        网签数=b.累计已售套数-isnull(a.累计已售套数,0), "
                    strSQL = strSQL + "        网签面积=case when b.累计已售套数-isnull(a.累计已售套数,0)=0 then 0 else  cast(round(b.累计已售面积-isnull(a.累计已售面积,0),2) as numeric(16,2)) end, "
                    strSQL = strSQL + "        网签总额=case when b.累计已售套数-isnull(a.累计已售套数,0)=0 then 0 else cast(round(b.签约总额-isnull(a.签约总额,0),2) as numeric(16,2)) end, "
                    'strSQL = strSQL + "        签约均价=case when b.累计已售面积-a.累计已售面积=0 or b.累计已售套数-a.累计已售套数=0 then 0 else  cast(round((b.签约总额-a.签约总额)/(b.累计已售面积-a.累计已售面积),2) as numeric(16,2))  end,"
                    'strSQL = strSQL + "        签约均价=dbo.Sunshine_F_getAveragePrice_nomatch(b.楼盘名称,@startDate_start,@endDate_start,b.房屋类型),"
                    strSQL = strSQL + "        签约均价=case when b.累计已售面积-a.累计已售面积=0 or b.累计已售套数-a.累计已售套数=0 then dbo.Sunshine_F_getAveragePrice_nomatch(b.楼盘名称,@startDate_start,@endDate_start,b.房屋类型) else  cast(round((b.签约总额-a.签约总额)/(b.累计已售面积-a.累计已售面积),2) as numeric(16,2))  end,"
                    strSQL = strSQL + "        累计均价=case when b.累计已售面积=0 or b.累计已售套数=0 then 0 else  cast(round((b.签约总额)/(b.累计已售面积),2) as numeric(16,2))  end"
                    strSQL = strSQL + "         from "
                    strSQL = strSQL + "         ( "
                    strSQL = strSQL + "         select  C_XZQH as 行政区域,C_HOUSE as 楼盘名称, isnull(c_type,0) as 房屋类型, "
                    strSQL = strSQL + "               sum(C_ZZ_YSTS_LJ) as 累计已售套数,   "
                    strSQL = strSQL + "               sum(C_ZZ_YSMJ_LJ) as 累计已售面积, "
                    strSQL = strSQL + "               sum(C_ZZ_TS_WS) as 未售套数,   "
                    strSQL = strSQL + "               sum(C_ZZ_MJ_WS) as 未售面积,"
                    strSQL = strSQL + "               sum(C_ZZ_YSMJ_LJ*C_ZZ_JJ_LJ) as 签约总额 from "
                    strSQL = strSQL + "                   ("
                    strSQL = strSQL + "                   select a.*,c.c_house,c.c_type from T_HOUSE_INFO a left join T_HOUSE_MATCH_XMID c on a.C_XM_NAME=c.C_XM_NAME and a.C_XM_ID=c.C_XM_ID"
                    strSQL = strSQL + "                     where a.C_TIME  between @frontendDate_start and @frontendDate_end    "
                    strSQL = strSQL + "                   )a  group by C_XZQH,C_HOUSE,c_type  "
                    strSQL = strSQL + "          )b "
                    strSQL = strSQL + "          left  Join "
                    strSQL = strSQL + "         ("
                    strSQL = strSQL + "             select  a.C_XZQH as 行政区域,a.C_HOUSE as 楼盘名称, isnull(c_type,0) as 房屋类型, "
                    strSQL = strSQL + "                 sum(C_ZZ_YSTS_LJ) as 累计已售套数,   "
                    strSQL = strSQL + "                 sum(C_ZZ_YSMJ_LJ) as 累计已售面积, "
                    strSQL = strSQL + "                 sum(C_ZZ_TS_WS) as 未售套数,   "
                    strSQL = strSQL + "                 sum(C_ZZ_MJ_WS) as 未售面积,"
                    strSQL = strSQL + "                 sum(C_ZZ_YSMJ_LJ*C_ZZ_JJ_LJ) as 签约总额 from"
                    strSQL = strSQL + "                   ("
                    strSQL = strSQL + "                   select a.*,c.c_house,c.c_type from T_HOUSE_INFO a left join T_HOUSE_MATCH_XMID c on a.C_XM_NAME=c.C_XM_NAME and a.C_XM_ID=c.C_XM_ID "
                    strSQL = strSQL + "                    where a.C_TIME between @frontstartDate_start and @frontstartDate_end "
                    strSQL = strSQL + "                   )a  group by C_XZQH,C_HOUSE,c_type  "
                    strSQL = strSQL + "         )a on b.楼盘名称=a.楼盘名称 and b.房屋类型=a.房屋类型 "
                    strSQL = strSQL + "     ) A left join (select * from T_HOUSE_MATCH_SORT where i_type=convert(integer,'" + strType + "')) b on b.c_NAME=a.楼盘名称 "
                    If strWhere <> "" Then
                        strSQL = strSQL + "where " + strWhere
                    End If

                Case "3"
                    strSQL = strSQL + "  select a.*,isnull(b.i_sort,100000) as '楼盘排序' from "
                    strSQL = strSQL + "  (	"
                    strSQL = strSQL + "     select b.行政区域,b.楼盘名称,b.项目类型, "
                    strSQL = strSQL + "        a.累计已售套数 as 累计已售套数1,  "
                    strSQL = strSQL + "        a.累计已售面积 as 累计已售面积1, "
                    strSQL = strSQL + "        a.未售套数 as 未售套数1,  "
                    strSQL = strSQL + "        a.未售面积 as 未售面积1,  "
                    strSQL = strSQL + "        b.累计已售套数 as 合共成交,   "
                    strSQL = strSQL + "        b.累计已售面积 as 累计已售面积, "
                    strSQL = strSQL + "        b.未售套数 as 未售套数,   "
                    strSQL = strSQL + "        b.未售面积 as 未售面积,  "
                    strSQL = strSQL + "        网签数=b.累计已售套数-isnull(a.累计已售套数,0), "
                    strSQL = strSQL + "        网签面积=case when b.累计已售套数-isnull(a.累计已售套数,0)=0 then 0 else  cast(round(b.累计已售面积-isnull(a.累计已售面积,0),2) as numeric(16,2)) end, "
                    strSQL = strSQL + "        网签总额=case when b.累计已售套数-isnull(a.累计已售套数,0)=0 then 0 else cast(round(b.签约总额-isnull(a.签约总额,0),2) as numeric(16,2)) end, "
                    'strSQL = strSQL + "        签约均价=case when b.累计已售面积-a.累计已售面积=0 or b.累计已售套数-a.累计已售套数=0 then 0 else  cast(round((b.签约总额-a.签约总额)/(b.累计已售面积-a.累计已售面积),2) as numeric(16,2))  end,"
                    'strSQL = strSQL + "        签约均价=dbo.Sunshine_F_getAveragePrice_nomatch_BG(b.楼盘名称,@startDate_start,@endDate_start,0),"
                    strSQL = strSQL + "        签约均价=case when b.累计已售面积-a.累计已售面积=0 or b.累计已售套数-a.累计已售套数=0 then dbo.Sunshine_F_getAveragePrice_nomatch_BG(b.楼盘名称,@startDate_start,@endDate_start,0) else  cast(round((b.签约总额-a.签约总额)/(b.累计已售面积-a.累计已售面积),2) as numeric(16,2))  end,"
                    strSQL = strSQL + "        累计均价=case when b.累计已售面积=0 or b.累计已售套数=0 then 0 else  cast(round((b.签约总额)/(b.累计已售面积),2) as numeric(16,2))  end"
                    strSQL = strSQL + "         from "
                    strSQL = strSQL + "         ( "
                    strSQL = strSQL + "         select  C_XZQH as 行政区域,C_HOUSE as 楼盘名称, '办公' as 项目类型, "
                    strSQL = strSQL + "               sum(C_BG_YSTS_LJ) as 累计已售套数,   "
                    strSQL = strSQL + "               sum(C_BG_YSMJ_LJ) as 累计已售面积, "
                    strSQL = strSQL + "               sum(C_BG_TS_WS) as 未售套数,   "
                    strSQL = strSQL + "               sum(C_BG_MJ_WS) as 未售面积,"
                    strSQL = strSQL + "               sum(C_BG_YSMJ_LJ*C_BG_JJ_LJ) as 签约总额 from "
                    strSQL = strSQL + "                   ("
                    strSQL = strSQL + "                   select a.*,c.c_house from T_HOUSE_INFO a left join T_HOUSE_MATCH_XMID c on a.C_XM_NAME=c.C_XM_NAME and a.C_XM_ID=c.C_XM_ID"
                    strSQL = strSQL + "                     where a.C_TIME  between @frontendDate_start and @frontendDate_end  "
                    strSQL = strSQL + "                   )a  group by C_XZQH,C_HOUSE  "
                    strSQL = strSQL + "          )b "
                    strSQL = strSQL + "          left  Join "
                    strSQL = strSQL + "         ("
                    strSQL = strSQL + "             select  a.C_XZQH as 行政区域,a.C_HOUSE as 楼盘名称, '办公' as 项目类型, "
                    strSQL = strSQL + "                 sum(C_BG_YSTS_LJ) as 累计已售套数,   "
                    strSQL = strSQL + "                 sum(C_BG_YSMJ_LJ) as 累计已售面积, "
                    strSQL = strSQL + "                 sum(C_BG_TS_WS) as 未售套数,   "
                    strSQL = strSQL + "                 sum(C_BG_MJ_WS) as 未售面积,"
                    strSQL = strSQL + "                 sum(C_BG_YSMJ_LJ*C_BG_JJ_LJ) as 签约总额 from"
                    strSQL = strSQL + "                   ("
                    strSQL = strSQL + "                   select a.*,c.c_house from T_HOUSE_INFO a left join T_HOUSE_MATCH_XMID c on a.C_XM_NAME=c.C_XM_NAME and a.C_XM_ID=c.C_XM_ID "
                    strSQL = strSQL + "                    where a.C_TIME between @frontstartDate_start and @frontstartDate_end "
                    strSQL = strSQL + "                   )a  group by C_XZQH,C_HOUSE  "
                    strSQL = strSQL + "         )a on b.楼盘名称=a.楼盘名称 "
                    strSQL = strSQL + "     ) A left join (select * from T_HOUSE_MATCH_SORT where i_type=convert(integer,'" + strType + "')) b on b.c_NAME=a.楼盘名称 "
                    If strWhere <> "" Then
                        strSQL = strSQL + "where " + strWhere
                    End If
                Case "1"
                    strSQL = strSQL + "  select a.*,isnull(b.i_sort,100000) as '楼盘排序' from "
                    strSQL = strSQL + "  (	"
                    strSQL = strSQL + "     select b.行政区域,b.楼盘名称,b.项目类型, "
                    strSQL = strSQL + "        a.累计已售套数 as 累计已售套数1,  "
                    strSQL = strSQL + "        a.累计已售面积 as 累计已售面积1, "
                    strSQL = strSQL + "        a.未售套数 as 未售套数1,  "
                    strSQL = strSQL + "        a.未售面积 as 未售面积1,  "
                    strSQL = strSQL + "        b.累计已售套数 as 合共成交,   "
                    strSQL = strSQL + "        b.累计已售面积 as 累计已售面积, "
                    strSQL = strSQL + "        b.未售套数 as 未售套数,   "
                    strSQL = strSQL + "        b.未售面积 as 未售面积,  "
                    strSQL = strSQL + "        网签数=b.累计已售套数-isnull(a.累计已售套数,0), "
                    strSQL = strSQL + "        网签面积=case when b.累计已售套数-isnull(a.累计已售套数,0)=0 then 0 else  cast(round(b.累计已售面积-isnull(a.累计已售面积,0),2) as numeric(16,2)) end, "
                    strSQL = strSQL + "        网签总额=case when b.累计已售套数-isnull(a.累计已售套数,0)=0 then 0 else cast(round(b.签约总额-isnull(a.签约总额,0),2) as numeric(16,2)) end, "
                    'strSQL = strSQL + "        签约均价=case when b.累计已售面积-a.累计已售面积=0 or b.累计已售套数-a.累计已售套数=0 then 0 else  cast(round((b.签约总额-a.签约总额)/(b.累计已售面积-a.累计已售面积),2) as numeric(16,2))  end,"
                    'strSQL = strSQL + "        签约均价=dbo.Sunshine_F_getAveragePrice_nomatch_SY(b.楼盘名称,@startDate_start,@endDate_start,0),"
                    strSQL = strSQL + "        签约均价=case when b.累计已售面积-a.累计已售面积=0 or b.累计已售套数-a.累计已售套数=0 then dbo.Sunshine_F_getAveragePrice_nomatch_SY(b.楼盘名称,@startDate_start,@endDate_start,0) else  cast(round((b.签约总额-a.签约总额)/(b.累计已售面积-a.累计已售面积),2) as numeric(16,2))  end,"
                    strSQL = strSQL + "        累计均价=case when b.累计已售面积=0 or b.累计已售套数=0 then 0 else  cast(round((b.签约总额)/(b.累计已售面积),2) as numeric(16,2))  end"
                    strSQL = strSQL + "         from "
                    strSQL = strSQL + "         ( "
                    strSQL = strSQL + "         select  C_XZQH as 行政区域,C_HOUSE as 楼盘名称, '商业' as 项目类型, "
                    strSQL = strSQL + "               sum(C_SY_YSTS_LJ) as 累计已售套数,   "
                    strSQL = strSQL + "               sum(C_SY_YSMJ_LJ) as 累计已售面积, "
                    strSQL = strSQL + "               sum(C_SY_TS_WS) as 未售套数,   "
                    strSQL = strSQL + "               sum(C_SY_MJ_WS) as 未售面积,"
                    strSQL = strSQL + "               sum(C_SY_YSMJ_LJ*C_SY_JJ_LJ) as 签约总额 from "
                    strSQL = strSQL + "                   ("
                    strSQL = strSQL + "                   select a.*,c.c_house from T_HOUSE_INFO a left join T_HOUSE_MATCH_XMID c on a.C_XM_NAME=c.C_XM_NAME and a.C_XM_ID=c.C_XM_ID"
                    strSQL = strSQL + "                     where a.C_TIME  between @frontendDate_start and @frontendDate_end    "
                    strSQL = strSQL + "                   )a  group by C_XZQH,C_HOUSE  "
                    strSQL = strSQL + "          )b "
                    strSQL = strSQL + "          left  Join "
                    strSQL = strSQL + "         ("
                    strSQL = strSQL + "             select  a.C_XZQH as 行政区域,a.C_HOUSE as 楼盘名称, '商业' as 项目类型, "
                    strSQL = strSQL + "                 sum(C_SY_YSTS_LJ) as 累计已售套数,   "
                    strSQL = strSQL + "                 sum(C_SY_YSMJ_LJ) as 累计已售面积, "
                    strSQL = strSQL + "                 sum(C_SY_TS_WS) as 未售套数,   "
                    strSQL = strSQL + "                 sum(C_SY_MJ_WS) as 未售面积,"
                    strSQL = strSQL + "                 sum(C_SY_YSMJ_LJ*C_SY_JJ_LJ) as 签约总额 from"
                    strSQL = strSQL + "                   ("
                    strSQL = strSQL + "                   select a.*,c.c_house from T_HOUSE_INFO a left join T_HOUSE_MATCH_XMID c on a.C_XM_NAME=c.C_XM_NAME and a.C_XM_ID=c.C_XM_ID "
                    strSQL = strSQL + "                    where a.C_TIME between @frontstartDate_start and @frontstartDate_end  "
                    strSQL = strSQL + "                   )a  group by C_XZQH,C_HOUSE  "
                    strSQL = strSQL + "         )a on b.楼盘名称=a.楼盘名称 "
                    strSQL = strSQL + "     ) A left join (select * from T_HOUSE_MATCH_SORT where i_type=convert(integer,'" + strType + "')) b on b.c_NAME=a.楼盘名称 "
                    If strWhere <> "" Then
                        strSQL = strSQL + "where " + strWhere
                    End If
                Case Else

            End Select

            getSql_FrontBuildingCompute_x3 = True
errProc:

            Exit Function
        End Function

        '----------------------------------------------------------------
        ' 根据“查询条件”获取“阳光家缘楼盘信息数据分析”完全数据的数据集
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strUserId                  ：用户标识
        '     strPassword                ：用户密码
        '     strWhere                   ：搜索字符串 
        '     objParameter               : 查询条件参数
        '     objSunshine                ：信息数据集
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_BuildingCompute( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByVal objParameter As System.Collections.Specialized.NameValueCollection, _
            ByRef objSunshine As Xydc.Platform.Common.Data.SunshineData) As Boolean

            Dim objTempDeepdata As Xydc.Platform.Common.Data.SunshineData = Nothing
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Nothing
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand = Nothing
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim strSQL As String = ""
            Dim strSQL_0 As String = ""
            Dim strSQL_Total As String = ""

            '初始化
            getDataSet_BuildingCompute = False
            objSunshine = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strWhere.Length > 0 Then strWhere = strWhere.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：[阳光家缘楼盘信息数据分析]未指定[连接用户]！"
                    GoTo errProc
                End If

                Dim datStartDate_Start As DateTime
                Dim datStartDate_End As DateTime
                Dim datEndDate_Start As DateTime
                Dim datEndDate_End As DateTime
                Dim strType As String
                Dim strRegion As String

                datStartDate_Start = CType(objParameter("StartDate"), System.DateTime)
                datStartDate_End = datStartDate_Start.AddDays(1)
                datEndDate_Start = CType(objParameter("EndDate"), System.DateTime)
                datEndDate_End = datEndDate_Start.AddDays(1)
                strType = objParameter("type")

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

              

                '获取数据
                Try
                    '创建数据集
                    objTempDeepdata = New Xydc.Platform.Common.Data.SunshineData(Xydc.Platform.Common.Data.SunshineData.enumTableType.Sunshine_V_Houseinfo_Statistics)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter

                        '获取查询语句                       
                        If getSql_BuildingCompute_x3(strErrMsg, strSQL, strWhere, strType) = False Then
                            GoTo errProc
                        End If

                        strSQL_0 = ""
                        strSQL_0 = strSQL_0 + " select * from "
                        strSQL_0 = strSQL_0 + " ("
                        strSQL_0 = strSQL_0 + " select 行政区域,楼盘名称,项目类型,网签数,签约均价 as 网签均价,累计均价 as '合共均价',合共成交,未售套数,网签总额,网签面积,楼盘排序,类型=1,排序=1 from ( "
                        strSQL_0 = strSQL_0 + strSQL
                        strSQL_0 = strSQL_0 + " )A "
                        strSQL_0 = strSQL_0 + " union"
                        strSQL_0 = strSQL_0 + " select 行政区域,楼盘名称='汇总',项目类型='',网签数=sum(case when 网签数<0 then 0 else  网签数 end),网签均价=case when sum(case when 网签面积<0 then 0 else  网签面积 end)<1 then 0 else sum(case when 网签总额<0 then 0 else  网签总额 end)/sum(case when 网签面积<0 then 0 else  网签面积 end) end,合共均价=0,合共成交=sum(case when 合共成交<0 then 0 else  合共成交 end),"
                        strSQL_0 = strSQL_0 + " 未售套数=sum(case when 未售套数<0 then 0 else  未售套数 end),网签总额=sum(case when 网签总额<0 then 0 else  网签总额 end),网签面积=sum(case when 网签面积<0 then 0 else  网签面积 end),楼盘排序='',类型=2,排序=1  from ( "
                        strSQL_0 = strSQL_0 + strSQL
                        strSQL_0 = strSQL_0 + " )A group by 行政区域"
                        strSQL_0 = strSQL_0 + " union"
                        strSQL_0 = strSQL_0 + " select 行政区域,楼盘名称='退房汇总',项目类型='',网签数=sum(case when 网签数<0 then 网签数 else  0 end),网签均价=0 ,合共均价=0, 合共成交=0,"
                        strSQL_0 = strSQL_0 + " 未售套数=0,网签总额=0,网签面积=0,楼盘排序='',类型=3,排序=1  from ( "
                        strSQL_0 = strSQL_0 + strSQL
                        strSQL_0 = strSQL_0 + " )A group by 行政区域"
                        strSQL_0 = strSQL_0 + " union"
                        strSQL_0 = strSQL_0 + " select 行政区域='合计',楼盘名称='',项目类型='',网签数=sum(case when 网签数<0 then 0 else  网签数 end),网签均价=case when sum(case when 网签面积<0 then 0 else  网签面积 end)<1 then 0 else sum(case when 网签总额<0 then 0 else  网签总额 end)/sum(case when 网签面积<0 then 0 else  网签面积 end) end,合共均价=0,合共成交=sum(case when 合共成交<0 then 0 else  合共成交 end), "
                        strSQL_0 = strSQL_0 + " 未售套数=sum(case when 未售套数<0 then 0 else  未售套数 end),网签总额=sum(case when 网签总额<0 then 0 else  网签总额 end),网签面积=sum(case when 网签面积<0 then 0 else  网签面积 end),楼盘排序='',类型=3,排序=2  from ( "
                        strSQL_0 = strSQL_0 + strSQL
                        strSQL_0 = strSQL_0 + " )A "
                        strSQL_0 = strSQL_0 + " union"
                        strSQL_0 = strSQL_0 + " select 行政区域='退房合计',楼盘名称='',项目类型='',网签数=sum(case when 网签数<0 then 网签数 else  0 end),网签均价=0 ,合共均价=0,合共成交=0,"
                        strSQL_0 = strSQL_0 + " 未售套数=0,网签总额=0,网签面积=0,楼盘排序='',类型=4,排序=2  from ( "
                        strSQL_0 = strSQL_0 + strSQL
                        strSQL_0 = strSQL_0 + " )A "
                        strSQL_0 = strSQL_0 + " )A "
                        strSQL_0 = strSQL_0 + " order by A.排序,A.行政区域,A.类型,A.楼盘排序,A.楼盘名称,A.项目类型"

                        objSqlCommand.CommandText = strSQL_0
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@startDate_start", datStartDate_Start)
                        objSqlCommand.Parameters.AddWithValue("@startDate_end", datStartDate_End)
                        objSqlCommand.Parameters.AddWithValue("@endDate_start", datEndDate_Start)
                        objSqlCommand.Parameters.AddWithValue("@endDate_end", datEndDate_End)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempDeepdata.Tables(Xydc.Platform.Common.Data.SunshineData.TABLE_Sunshine_V_Houseinfo_Statistics))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDeepdata.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempDeepdata)
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objSunshine = objTempDeepdata
            getDataSet_BuildingCompute = True
            Exit Function
errProc:
            Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempDeepdata)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function
        End Function

        '----------------------------------------------------------------
        ' 根据“查询条件”获取“阳光家缘楼盘信息数据分析”完全数据的数据集
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strUserId                  ：用户标识
        '     strPassword                ：用户密码
        '     strWhere                   ：搜索字符串 
        '     objParameter               : 查询条件参数
        '     objSunshine                ：信息数据集
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_WEEKBuildingCompute( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByVal objParameter As System.Collections.Specialized.NameValueCollection, _
            ByRef objSunshine As Xydc.Platform.Common.Data.SunshineData) As Boolean

            Dim objTempDeepdata As Xydc.Platform.Common.Data.SunshineData = Nothing
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Nothing
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand = Nothing
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim strSQL As String = ""
            Dim strSQL_0 As String = ""
            Dim strSQL_Total As String = ""

            '初始化
            getDataSet_WEEKBuildingCompute = False
            objSunshine = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strWhere.Length > 0 Then strWhere = strWhere.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：[阳光家缘楼盘信息数据分析]未指定[连接用户]！"
                    GoTo errProc
                End If

                Dim datStartDate_Start As DateTime
                Dim datStartDate_End As DateTime
                Dim datEndDate_Start As DateTime
                Dim datEndDate_End As DateTime
                Dim frontdatStartDate_Start As DateTime
                Dim frontdatStartDate_End As DateTime
                Dim frontdatEndDate_Start As DateTime
                Dim frontdatEndDate_End As DateTime

                Dim strType As String
                Dim strRegion As String

                datStartDate_Start = CType(objParameter("StartDate"), System.DateTime)
                datStartDate_End = datStartDate_Start.AddDays(1)
                datEndDate_Start = CType(objParameter("EndDate"), System.DateTime)
                datEndDate_End = datEndDate_Start.AddDays(1)

                frontdatStartDate_Start = datStartDate_Start.AddDays(-7)
                frontdatStartDate_End = datStartDate_End.AddDays(-7)
                frontdatEndDate_Start = datEndDate_Start.AddDays(-7)
                frontdatEndDate_End = datEndDate_End.AddDays(-7)

                strType = objParameter("type")

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If



                '获取数据
                Try
                    '创建数据集
                    objTempDeepdata = New Xydc.Platform.Common.Data.SunshineData(Xydc.Platform.Common.Data.SunshineData.enumTableType.Sunshine_V_WeekInfo_Statistics)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter

                        '获取查询语句
                        If getSql_BuildingCompute_x3(strErrMsg, strSQL, strWhere, strType) = False Then
                            GoTo errProc
                        End If

                        If getSql_FrontBuildingCompute_x3(strErrMsg, strSQL_0, strWhere, strType) = False Then
                            GoTo errProc
                        End If

                        strSQL_Total = " "
                        strSQL_Total = strSQL_Total + " select * from ( "
                        strSQL_Total = strSQL_Total + " select a.*,b.网签数 as '上周网签数',网签均价=case when a.签约均价>0 then a.签约均价 "
                        strSQL_Total = strSQL_Total + " when a.签约均价=0 and b.签约均价>0 then b.签约均价 else a.累计均价 end  from ("
                        strSQL_Total = strSQL_Total + strSQL
                        strSQL_Total = strSQL_Total + " )a"
                        strSQL_Total = strSQL_Total + " left join "
                        strSQL_Total = strSQL_Total + " ("
                        strSQL_Total = strSQL_Total + strSQL_0
                        strSQL_Total = strSQL_Total + " )b on a.行政区域=b.行政区域 and a.楼盘名称=b.楼盘名称 and a.项目类型=b.项目类型"
                        strSQL_Total = strSQL_Total + " )A"
                        strSQL_Total = strSQL_Total + " order by A.楼盘排序,A.行政区域, A.楼盘名称, A.项目类型 "

                        objSqlCommand.CommandText = strSQL_Total
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@startDate_start", datStartDate_Start)
                        objSqlCommand.Parameters.AddWithValue("@startDate_end", datStartDate_End)
                        objSqlCommand.Parameters.AddWithValue("@endDate_start", datEndDate_Start)
                        objSqlCommand.Parameters.AddWithValue("@endDate_end", datEndDate_End)
                        objSqlCommand.Parameters.AddWithValue("@frontstartDate_start", frontdatStartDate_Start)
                        objSqlCommand.Parameters.AddWithValue("@frontstartDate_end", frontdatStartDate_End)
                        objSqlCommand.Parameters.AddWithValue("@frontendDate_start", frontdatEndDate_Start)
                        objSqlCommand.Parameters.AddWithValue("@frontendDate_end", frontdatEndDate_End)

                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempDeepdata.Tables(Xydc.Platform.Common.Data.SunshineData.TABLE_Sunshine_V_WeekInfo_Statistics))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDeepdata.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempDeepdata)
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objSunshine = objTempDeepdata
            getDataSet_WEEKBuildingCompute = True
            Exit Function
errProc:
            Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempDeepdata)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function
        End Function

        '----------------------------------------------------------------
        ' 根据“查询条件”获取“阳光家缘楼盘信息周区域数据分析”完全数据的数据集
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strUserId                  ：用户标识
        '     strPassword                ：用户密码
        '     strWhere                   ：搜索字符串 
        '     objParameter               : 查询条件参数
        '     objSunshine                ：信息数据集
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_RegionBuildingCompute( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByVal objParameter As System.Collections.Specialized.NameValueCollection, _
            ByRef objSunshine As Xydc.Platform.Common.Data.SunshineData) As Boolean

            Dim objTempDeepdata As Xydc.Platform.Common.Data.SunshineData = Nothing
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Nothing
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand = Nothing
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim strSQL As String = ""
            Dim strSQL_0 As String = ""
            Dim strSQL_Total As String = ""

            '初始化
            getDataSet_RegionBuildingCompute = False
            objSunshine = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strWhere.Length > 0 Then strWhere = strWhere.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：[阳光家缘楼盘信息数据分析]未指定[连接用户]！"
                    GoTo errProc
                End If

                Dim datStartDate_Start As DateTime
                Dim datStartDate_End As DateTime
                Dim datEndDate_Start As DateTime
                Dim datEndDate_End As DateTime
                Dim frontdatStartDate_Start As DateTime
                Dim frontdatStartDate_End As DateTime
                Dim frontdatEndDate_Start As DateTime
                Dim frontdatEndDate_End As DateTime

                Dim strType As String
                Dim strRegion As String

                datStartDate_Start = CType(objParameter("StartDate"), System.DateTime)
                datStartDate_End = datStartDate_Start.AddDays(1)
                datEndDate_Start = CType(objParameter("EndDate"), System.DateTime)
                datEndDate_End = datEndDate_Start.AddDays(1)

                frontdatStartDate_Start = datStartDate_Start.AddDays(-7)
                frontdatStartDate_End = datStartDate_End.AddDays(-7)
                frontdatEndDate_Start = datEndDate_Start.AddDays(-7)
                frontdatEndDate_End = datEndDate_End.AddDays(-7)

                strType = objParameter("type")

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If



                '获取数据
                Try
                    '创建数据集
                    objTempDeepdata = New Xydc.Platform.Common.Data.SunshineData(Xydc.Platform.Common.Data.SunshineData.enumTableType.Sunshine_V_WeekRegion_Statistics)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter

                        '获取查询语句
                        If getSql_BuildingCompute_x3(strErrMsg, strSQL, strWhere, strType) = False Then
                            GoTo errProc
                        End If

                        '获取查询语句
                        If getSql_FrontBuildingCompute_x3(strErrMsg, strSQL_0, strWhere, strType) = False Then
                            GoTo errProc
                        End If

                        strSQL_Total = " "
                        strSQL_Total = strSQL_Total + " select * from ( "
                        '各区域
                        strSQL_Total = strSQL_Total + " select a.行政区域,a.网签数,b.网签数 as '上周网签数',cast(a.网签均价 as numeric(16,2)) as 网签均价,cast(b.网签均价 as numeric(16,2)) as  '上周网签均价',a.网签总额,a.网签面积,网签数环比=case when b.网签数<1 then 0 else cast((a.网签数-b.网签数)/b.网签数*100 as numeric(16,2)) end,网签均价环比=case when b.网签均价<1 then 0 else cast((a.网签均价-b.网签均价)/b.网签均价*100 as numeric(16,2)) end,序号=1  from ("
                        strSQL_Total = strSQL_Total + " select 行政区域,网签数=sum(case when 网签数<0 then 0 else  网签数 end),网签均价=case when sum(case when 网签面积<0 then 0 else  网签面积 end)<1 then 0 else sum(case when 网签总额<0 then 0 else  网签总额 end)/sum(case when 网签面积<0 then 0 else  网签面积 end) end, "
                        strSQL_Total = strSQL_Total + " 网签总额=sum(case when 网签总额<0 then 0 else  网签总额 end),网签面积=sum(case when 网签面积<0 then 0 else  网签面积 end) from ( "
                        strSQL_Total = strSQL_Total + strSQL
                        strSQL_Total = strSQL_Total + " )a group by 行政区域"
                        strSQL_Total = strSQL_Total + " )a"
                        strSQL_Total = strSQL_Total + " left join "
                        strSQL_Total = strSQL_Total + " ("
                        strSQL_Total = strSQL_Total + " select 行政区域,网签数=sum(case when 网签数<0 then 0 else  网签数 end),网签均价=case when sum(case when 网签面积<0 then 0 else  网签面积 end)<1 then 0 else sum(case when 网签总额<0 then 0 else  网签总额 end)/sum(case when 网签面积<0 then 0 else  网签面积 end) end, "
                        strSQL_Total = strSQL_Total + " 网签总额=sum(case when 网签总额<0 then 0 else  网签总额 end),网签面积=sum(case when 网签面积<0 then 0 else  网签面积 end) from ( "
                        strSQL_Total = strSQL_Total + strSQL_0
                        strSQL_Total = strSQL_Total + " )a group by 行政区域"
                        strSQL_Total = strSQL_Total + " )b on a.行政区域=b.行政区域"
                        '六区
                        strSQL_Total = strSQL_Total + " union"
                        strSQL_Total = strSQL_Total + " select a.行政区域,a.网签数,b.网签数 as '上周网签数',cast(a.网签均价 as numeric(16,2)) as 网签均价,cast(b.网签均价 as numeric(16,2)) as  '上周网签均价',a.网签总额,a.网签面积,网签数环比=case when b.网签数<1 then 0 else cast((a.网签数-b.网签数)/b.网签数*100 as numeric(16,2)) end,网签均价环比=case when b.网签均价<1 then 0 else cast((a.网签均价-b.网签均价)/b.网签均价*100 as numeric(16,2)) end,序号=2  from ("
                        strSQL_Total = strSQL_Total + " select '六区' as '行政区域',网签数=sum(case when 网签数<0 then 0 else  网签数 end),网签均价=case when sum(case when 网签面积<0 then 0 else  网签面积 end)<1 then 0 else sum(case when 网签总额<0 then 0 else  网签总额 end)/sum(case when 网签面积<0 then 0 else  网签面积 end) end, "
                        strSQL_Total = strSQL_Total + " 网签总额=sum(case when 网签总额<0 then 0 else  网签总额 end),网签面积=sum(case when 网签面积<0 then 0 else  网签面积 end) from ( "
                        strSQL_Total = strSQL_Total + strSQL
                        strSQL_Total = strSQL_Total + " )a  where 行政区域 in ('白云区','天河区','越秀区','荔湾区','海珠区','黄埔区')"
                        strSQL_Total = strSQL_Total + " )a"
                        strSQL_Total = strSQL_Total + " left join "
                        strSQL_Total = strSQL_Total + " ("
                        strSQL_Total = strSQL_Total + " select '六区' as '行政区域',网签数=sum(case when 网签数<0 then 0 else  网签数 end),网签均价=case when sum(case when 网签面积<0 then 0 else  网签面积 end)<1 then 0 else sum(case when 网签总额<0 then 0 else  网签总额 end)/sum(case when 网签面积<0 then 0 else  网签面积 end) end, "
                        strSQL_Total = strSQL_Total + " 网签总额=sum(case when 网签总额<0 then 0 else  网签总额 end),网签面积=sum(case when 网签面积<0 then 0 else  网签面积 end) from ( "
                        strSQL_Total = strSQL_Total + strSQL_0
                        strSQL_Total = strSQL_Total + " )a  where 行政区域 in ('白云区','天河区','越秀区','荔湾区','海珠区','黄埔区')"
                        strSQL_Total = strSQL_Total + " )b on a.行政区域=b.行政区域"

                        '十区
                        strSQL_Total = strSQL_Total + " union"
                        strSQL_Total = strSQL_Total + " select a.行政区域,a.网签数,b.网签数 as '上周网签数',cast(a.网签均价 as numeric(16,2)) as 网签均价,cast(b.网签均价 as numeric(16,2)) as  '上周网签均价',a.网签总额,a.网签面积,网签数环比=case when b.网签数<1 then 0 else cast((a.网签数-b.网签数)/b.网签数*100 as numeric(16,2)) end,网签均价环比=case when b.网签均价<1 then 0 else cast((a.网签均价-b.网签均价)/b.网签均价*100 as numeric(16,2)) end,序号=3  from ("
                        strSQL_Total = strSQL_Total + " select '十区' as '行政区域',网签数=sum(case when 网签数<0 then 0 else  网签数 end),网签均价=case when sum(case when 网签面积<0 then 0 else  网签面积 end)<1 then 0 else sum(case when 网签总额<0 then 0 else  网签总额 end)/sum(case when 网签面积<0 then 0 else  网签面积 end) end, "
                        strSQL_Total = strSQL_Total + " 网签总额=sum(case when 网签总额<0 then 0 else  网签总额 end),网签面积=sum(case when 网签面积<0 then 0 else  网签面积 end) from ( "
                        strSQL_Total = strSQL_Total + strSQL
                        strSQL_Total = strSQL_Total + " )a  where 行政区域 not in ('增城市','从化市')"
                        strSQL_Total = strSQL_Total + " )a"
                        strSQL_Total = strSQL_Total + " left join "
                        strSQL_Total = strSQL_Total + " ("
                        strSQL_Total = strSQL_Total + " select  '十区' as '行政区域',网签数=sum(case when 网签数<0 then 0 else  网签数 end),网签均价=case when sum(case when 网签面积<0 then 0 else  网签面积 end)<1 then 0 else sum(case when 网签总额<0 then 0 else  网签总额 end)/sum(case when 网签面积<0 then 0 else  网签面积 end) end, "
                        strSQL_Total = strSQL_Total + " 网签总额=sum(case when 网签总额<0 then 0 else  网签总额 end),网签面积=sum(case when 网签面积<0 then 0 else  网签面积 end) from ( "
                        strSQL_Total = strSQL_Total + strSQL_0
                        strSQL_Total = strSQL_Total + " )a  where 行政区域 not in ('增城市','从化市')"
                        strSQL_Total = strSQL_Total + " )b on a.行政区域=b.行政区域"

                        '十区两市
                        strSQL_Total = strSQL_Total + " union"
                        strSQL_Total = strSQL_Total + " select a.行政区域,a.网签数,b.网签数 as '上周网签数',cast(a.网签均价 as numeric(16,2)) as 网签均价,cast(b.网签均价 as numeric(16,2)) as  '上周网签均价',a.网签总额,a.网签面积,网签数环比=case when b.网签数<1 then 0 else cast((a.网签数-b.网签数)/b.网签数*100 as numeric(16,2)) end,网签均价环比=case when b.网签均价<1 then 0 else cast((a.网签均价-b.网签均价)/b.网签均价*100 as numeric(16,2)) end,序号=4  from ("
                        strSQL_Total = strSQL_Total + " select '十区两市' as '行政区域',网签数=sum(case when 网签数<0 then 0 else  网签数 end),网签均价=case when sum(case when 网签面积<0 then 0 else  网签面积 end)<1 then 0 else sum(case when 网签总额<0 then 0 else  网签总额 end)/sum(case when 网签面积<0 then 0 else  网签面积 end) end, "
                        strSQL_Total = strSQL_Total + " 网签总额=sum(case when 网签总额<0 then 0 else  网签总额 end),网签面积=sum(case when 网签面积<0 then 0 else  网签面积 end) from ( "
                        strSQL_Total = strSQL_Total + strSQL
                        strSQL_Total = strSQL_Total + " )a  "
                        strSQL_Total = strSQL_Total + " )a"
                        strSQL_Total = strSQL_Total + " left join "
                        strSQL_Total = strSQL_Total + " ("
                        strSQL_Total = strSQL_Total + " select  '十区两市' as '行政区域',网签数=sum(case when 网签数<0 then 0 else  网签数 end),网签均价=case when sum(case when 网签面积<0 then 0 else  网签面积 end)<1 then 0 else sum(case when 网签总额<0 then 0 else  网签总额 end)/sum(case when 网签面积<0 then 0 else  网签面积 end) end, "
                        strSQL_Total = strSQL_Total + " 网签总额=sum(case when 网签总额<0 then 0 else  网签总额 end),网签面积=sum(case when 网签面积<0 then 0 else  网签面积 end) from ( "
                        strSQL_Total = strSQL_Total + strSQL_0
                        strSQL_Total = strSQL_Total + " )a "
                        strSQL_Total = strSQL_Total + " )b on a.行政区域=b.行政区域"

                        strSQL_Total = strSQL_Total + " )A"
                        strSQL_Total = strSQL_Total + " order by 序号,A.行政区域 "
                        objSqlCommand.CommandText = strSQL_Total
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@startDate_start", datStartDate_Start)
                        objSqlCommand.Parameters.AddWithValue("@startDate_end", datStartDate_End)
                        objSqlCommand.Parameters.AddWithValue("@endDate_start", datEndDate_Start)
                        objSqlCommand.Parameters.AddWithValue("@endDate_end", datEndDate_End)
                        objSqlCommand.Parameters.AddWithValue("@frontstartDate_start", frontdatStartDate_Start)
                        objSqlCommand.Parameters.AddWithValue("@frontstartDate_end", frontdatStartDate_End)
                        objSqlCommand.Parameters.AddWithValue("@frontendDate_start", frontdatEndDate_Start)
                        objSqlCommand.Parameters.AddWithValue("@frontendDate_end", frontdatEndDate_End)

                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempDeepdata.Tables(Xydc.Platform.Common.Data.SunshineData.TABLE_Sunshine_V_WeekRegion_Statistics))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDeepdata.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempDeepdata)
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objSunshine = objTempDeepdata
            getDataSet_RegionBuildingCompute = True
            Exit Function
errProc:
            Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempDeepdata)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function
        End Function

        '----------------------------------------------------------------
        ' 根据“查询条件”获取“阳光家缘楼盘区域信息数据分析”完全数据的数据集
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strUserId                  ：用户标识
        '     strPassword                ：用户密码
        '     strWhere                   ：搜索字符串 
        '     objParameter               : 查询条件参数
        '     objSunshine                ：信息数据集
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_RegionCompute( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByVal objParameter As System.Collections.Specialized.NameValueCollection, _
            ByRef objSunshine As Xydc.Platform.Common.Data.SunshineData) As Boolean

            Dim objTempDeepdata As Xydc.Platform.Common.Data.SunshineData = Nothing
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Nothing
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand = Nothing
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim strSQL As String = ""
            Dim strSQL_0 As String = ""
            Dim strSQL_Total As String = ""

            '初始化
            getDataSet_RegionCompute = False
            objSunshine = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strWhere.Length > 0 Then strWhere = strWhere.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：[阳光家缘楼盘信息数据分析]未指定[连接用户]！"
                    GoTo errProc
                End If

                Dim datStartDate_Start As DateTime
                Dim datStartDate_End As DateTime
                Dim datEndDate_Start As DateTime
                Dim datEndDate_End As DateTime
                Dim strType As String
                Dim strRegion As String

                datStartDate_Start = CType(objParameter("StartDate"), System.DateTime)
                datStartDate_End = datStartDate_Start.AddDays(1)
                datEndDate_Start = CType(objParameter("EndDate"), System.DateTime)
                datEndDate_End = datEndDate_Start.AddDays(1)
                strType = objParameter("type")

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If



                '获取数据
                Try
                    '创建数据集
                    objTempDeepdata = New Xydc.Platform.Common.Data.SunshineData(Xydc.Platform.Common.Data.SunshineData.enumTableType.Sunshine_V_WeekRegion_Statistics)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter

                        '获取查询语句
                        If getSql_BuildingCompute_x3(strErrMsg, strSQL, strWhere, strType) = False Then
                            GoTo errProc
                        End If

                        strSQL_Total = strSQL_Total + " select a.行政区域,a.网签数,0 as '上周网签数',a.网签均价,0 as '上周网签均价',网签数环比=0,网签均价环比=0  from ("
                        strSQL_Total = strSQL_Total + " select a.行政区域,网签数=sum(网签数),网签均价=sum(网签总额)/sum(网签面积) from ("
                        strSQL_Total = strSQL_Total + strSQL
                        strSQL_Total = strSQL_Total + " )a group by a.行政区域"
                        strSQL_Total = strSQL_Total + " )a"
                        'strSQL_Total = strSQL_Total + " order by a.行政区域 "
                        objSqlCommand.CommandText = strSQL_Total
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@startDate_start", datStartDate_Start)
                        objSqlCommand.Parameters.AddWithValue("@startDate_end", datStartDate_End)
                        objSqlCommand.Parameters.AddWithValue("@endDate_start", datEndDate_Start)
                        objSqlCommand.Parameters.AddWithValue("@endDate_end", datEndDate_End)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempDeepdata.Tables(Xydc.Platform.Common.Data.SunshineData.TABLE_Sunshine_V_WeekRegion_Statistics))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDeepdata.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempDeepdata)
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objSunshine = objTempDeepdata
            getDataSet_RegionCompute = True
            Exit Function
errProc:
            Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempDeepdata)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function
        End Function

        '----------------------------------------------------------------
        ' 根据“查询条件”获取“N个星期的价格和套数”
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strUserId                  ：用户标识
        '     strPassword                ：用户密码
        '     strWhere                   ：搜索字符串 
        '     objParameter               : 查询条件参数
        '     objSunshine                ：信息数据集
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_NWeek_Compute( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByVal objParameter As System.Collections.Specialized.NameValueCollection, _
            ByRef objSunshine As Xydc.Platform.Common.Data.SunshineData) As Boolean

            Dim objTempDeepdata As Xydc.Platform.Common.Data.SunshineData = Nothing
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Nothing
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand = Nothing
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim strSQL As String = ""
            Dim strSQL_0 As String = ""
            Dim strSQL_Total As String = ""

            '初始化
            getDataSet_NWeek_Compute = False
            objSunshine = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strWhere.Length > 0 Then strWhere = strWhere.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：[阳光家缘楼盘信息数据分析]未指定[连接用户]！"
                    GoTo errProc
                End If

                Dim datStartDate_Start As DateTime
                Dim datStartDate_End As DateTime
                Dim datEndDate_Start As DateTime
                Dim datEndDate_End As DateTime
                Dim strType As String
                Dim strRegion As String

                datStartDate_Start = CType(objParameter("StartDate"), System.DateTime)
                datEndDate_Start = CType(objParameter("EndDate"), System.DateTime)
                strType = objParameter("type")

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                Try
                    '创建数据集
                    objTempDeepdata = New Xydc.Platform.Common.Data.SunshineData(Xydc.Platform.Common.Data.SunshineData.enumTableType.Sunshine_V_NWeek_Statistics)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter

                        strSQL = ""
                        Select Case strType
                            Case "1"
                                strSQL = strSQL + " exec dbo.Sunshine_P_getWeekStatistics_business @FirstStartDate,@FinalStartDate "

                            Case "2"
                                strSQL = strSQL + " exec dbo.Sunshine_P_getWeekStatistics @FirstStartDate,@FinalStartDate "

                            Case "3"
                                strSQL = strSQL + " exec dbo.Sunshine_P_getWeekStatistics_office @FirstStartDate,@FinalStartDate "

                        End Select
                      
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@FirstStartDate", datStartDate_Start)
                        objSqlCommand.Parameters.AddWithValue("@FinalStartDate", datEndDate_Start)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempDeepdata.Tables(Xydc.Platform.Common.Data.SunshineData.TABLE_Sunshine_V_NWeek_Statistics))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDeepdata.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempDeepdata)
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objSunshine = objTempDeepdata
            getDataSet_NWeek_Compute = True
            Exit Function
errProc:
            Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempDeepdata)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function
        End Function





        '----------------------------------------------------------------
        ' 获取周楼盘匹配的数据集(以代码升序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWhere             ：搜索字符串
        '     objSunshine          ：信息数据集 
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getWeekMonitoringHouse( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objSunshine As Xydc.Platform.Common.Data.SunshineData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempSunshineData As Xydc.Platform.Common.Data.SunshineData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getWeekMonitoringHouse = False
            objSunshine = Nothing
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strWhere.Length > 0 Then strWhere = strWhere.Trim()

                '检查
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                Dim strSQL As String
                Try

                    '创建数据集
                    objTempSunshineData = New Xydc.Platform.Common.Data.SunshineData(Xydc.Platform.Common.Data.SunshineData.enumTableType.Sunshine_B_WeekMonitoringHouse)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from " + vbCr
                        strSQL = strSQL + " Sunshine_B_WeekMonitoringHouse a " + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.Region asc,a.MonitoringID desc" + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand
                        .Fill(objTempSunshineData.Tables(Xydc.Platform.Common.Data.SunshineData.TABLE_Sunshine_B_WeekMonitoringHouse))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempSunshineData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempSunshineData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objSunshine = objTempSunshineData
            getWeekMonitoringHouse = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempSunshineData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 保存周楼盘匹配的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
        '     objNewData           ：新数据
        '     objenumEditType      ：编辑类型
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doSaveSunshineWeekMonitoringHouse( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean


            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim intID As Integer

            '初始化
            doSaveSunshineWeekMonitoringHouse = False
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""

                '检查
                If strUserId.Trim = "" Then
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
                        intID = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.SunshineData.FIELD_Sunshine_B_WeekMonitoringHouse_ID), 0)
                        '计算SQL
                End Select

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

                '保存数据
                Dim strSQL As String
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '计算SQL
                    Dim strFields As String
                    Dim strValues As String
                    Dim intCount As Integer
                    Dim strValue As String
                    Dim i As Integer

                    intCount = objNewData.Count
                   
                    Select Case objenumEditType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                            strFields = ""
                            strValues = ""
                            For i = 0 To intCount - 1 Step 1
                                If strFields = "" Then
                                    strFields = objNewData.GetKey(i)
                                Else
                                    strFields = strFields + "," + objNewData.GetKey(i)
                                End If
                                If strValues = "" Then
                                    strValues = "@A" + i.ToString()
                                Else
                                    strValues = strValues + "," + "@A" + i.ToString()
                                End If
                            Next

                            strSQL = ""
                            strSQL = strSQL + " insert into Sunshine_B_WeekMonitoringHouse (" + strFields + ")"
                            strSQL = strSQL + " values (" + strValues + ")"
                            objSqlCommand.Parameters.Clear()
                            For i = 0 To intCount - 1 Step 1
                                strValue = objNewData.Item(i).Trim()
                                Select Case objNewData.GetKey(i)

                                    Case Else
                                        If strValue = "" Then strValue = " "
                                        objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), strValue)
                                End Select
                            Next
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()

                        Case Else
                            strFields = ""
                            For i = 0 To intCount - 1 Step 1
                                If strFields = "" Then
                                    strFields = objNewData.GetKey(i) + " = @A" + i.ToString()
                                Else
                                    strFields = strFields + "," + objNewData.GetKey(i) + " = @A" + i.ToString()
                                End If
                            Next
                            strSQL = ""
                            strSQL = strSQL + " update Sunshine_B_WeekMonitoringHouse  set "
                            strSQL = strSQL + " " + strFields + " "
                            strSQL = strSQL + " where MonitoringID = @C_ID"

                            objSqlCommand.Parameters.Clear()
                            For i = 0 To intCount - 1 Step 1
                                strValue = objNewData.Item(i).Trim()
                                Select Case objNewData.GetKey(i)

                                    Case Else
                                        If strValue = "" Then strValue = " "
                                        objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), strValue)
                                End Select
                            Next
                            objSqlCommand.Parameters.AddWithValue("@C_ID", intID)
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()
                    End Select

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

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doSaveSunshineWeekMonitoringHouse = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 删除楼盘匹配的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteSunshineWeekMonitoringHouse( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow) As Boolean


            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            doDeleteSunshineWeekMonitoringHouse = False
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""

                '检查
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If objOldData Is Nothing Then
                    strErrMsg = "错误：未传入旧的数据！"
                    GoTo errProc
                End If

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
                Dim strSQL As String
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '计算SQL
                    Dim strOldDM As String
                    strSQL = ""

                    With New Xydc.Platform.Common.Utilities.PulicParameters
                        strOldDM = .getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.SunshineData.FIELD_Sunshine_B_WeekMonitoringHouse_ID), "")
                    End With
                    strSQL = strSQL + " delete from Sunshine_B_WeekMonitoringHouse"
                    strSQL = strSQL + " where MonitoringID = @C_ID"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@C_ID", strOldDM)

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

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doDeleteSunshineWeekMonitoringHouse = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function
        End Function




        '----------------------------------------------------------------
        ' 获取月楼盘匹配的数据集(以代码升序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWhere             ：搜索字符串
        '     objSunshine          ：信息数据集 
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getHouseSort( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objSunshine As Xydc.Platform.Common.Data.SunshineData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempSunshineData As Xydc.Platform.Common.Data.SunshineData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getHouseSort = False
            objSunshine = Nothing
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strWhere.Length > 0 Then strWhere = strWhere.Trim()

                '检查
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                Dim strSQL As String
                Try

                    '创建数据集
                    objTempSunshineData = New Xydc.Platform.Common.Data.SunshineData(Xydc.Platform.Common.Data.SunshineData.enumTableType.Sunshine_B_HOUSEMATCHSORT)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.*, " + vbCr
                        strSQL = strSQL + " s_type=case when I_type=1 then '商业' when I_type=2 then '住宅' when I_type=3 then '办公' else '住宅' end "
                        strSQL = strSQL + " from " + vbCr
                        strSQL = strSQL + " T_HOUSE_MATCH_SORT a " + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by I_Type,I_Sort " + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand
                        .Fill(objTempSunshineData.Tables(Xydc.Platform.Common.Data.SunshineData.TABLE_Sunshine_B_HOUSEMATCHSORT))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempSunshineData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempSunshineData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objSunshine = objTempSunshineData
            getHouseSort = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempSunshineData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function


        '----------------------------------------------------------------
        ' 获取月楼盘匹配的数据集(以代码升序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWhere             ：搜索字符串
        '     objSunshine          ：信息数据集 
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getMonthMonitoringHouse( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objSunshine As Xydc.Platform.Common.Data.SunshineData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempSunshineData As Xydc.Platform.Common.Data.SunshineData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getMonthMonitoringHouse = False
            objSunshine = Nothing
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strWhere.Length > 0 Then strWhere = strWhere.Trim()

                '检查
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                Dim strSQL As String
                Try

                    '创建数据集
                    objTempSunshineData = New Xydc.Platform.Common.Data.SunshineData(Xydc.Platform.Common.Data.SunshineData.enumTableType.Sunshine_B_MonthMonitoringHouse)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from " + vbCr
                        strSQL = strSQL + " Sunshine_B_MonthMonitoringHouse a " + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.Region asc,a.MonitoringID desc" + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand
                        .Fill(objTempSunshineData.Tables(Xydc.Platform.Common.Data.SunshineData.TABLE_Sunshine_B_MonthMonitoringHouse))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempSunshineData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempSunshineData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objSunshine = objTempSunshineData
            getMonthMonitoringHouse = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempSunshineData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 保存月楼盘匹配的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
        '     objNewData           ：新数据
        '     objenumEditType      ：编辑类型
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doSaveSunshineMonthMonitoringHouse( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean


            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim intID As Integer

            '初始化
            doSaveSunshineMonthMonitoringHouse = False
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""

                '检查
                If strUserId.Trim = "" Then
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
                        intID = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.SunshineData.FIELD_Sunshine_B_WeekMonitoringHouse_ID), 0)
                        '计算SQL
                End Select

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

                '保存数据
                Dim strSQL As String
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '计算SQL
                    Dim strFields As String
                    Dim strValues As String
                    Dim intCount As Integer
                    Dim strValue As String
                    Dim i As Integer



                    intCount = objNewData.Count
                    'strID = objNewData.Item(Xydc.Platform.Common.Data.SunshineData.FIELD_Sunshine_B_HOUSE_MATCH_ID)

                    Select Case objenumEditType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                            strFields = ""
                            strValues = ""
                            For i = 0 To intCount - 1 Step 1
                                If strFields = "" Then
                                    strFields = objNewData.GetKey(i)
                                Else
                                    strFields = strFields + "," + objNewData.GetKey(i)
                                End If
                                If strValues = "" Then
                                    strValues = "@A" + i.ToString()
                                Else
                                    strValues = strValues + "," + "@A" + i.ToString()
                                End If
                            Next

                            strSQL = ""
                            strSQL = strSQL + " insert into Sunshine_B_MonthMonitoringHouse (" + strFields + ")"
                            strSQL = strSQL + " values (" + strValues + ")"
                            objSqlCommand.Parameters.Clear()
                            For i = 0 To intCount - 1 Step 1
                                strValue = objNewData.Item(i).Trim()
                                Select Case objNewData.GetKey(i)
                                    Case Else
                                        If strValue = "" Then strValue = " "
                                        objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), strValue)
                                End Select
                            Next
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()

                        Case Else
                            strFields = ""
                            For i = 0 To intCount - 1 Step 1
                                If strFields = "" Then
                                    strFields = objNewData.GetKey(i) + " = @A" + i.ToString()
                                Else
                                    strFields = strFields + "," + objNewData.GetKey(i) + " = @A" + i.ToString()
                                End If
                            Next
                            strSQL = ""
                            strSQL = strSQL + " update Sunshine_B_MonthMonitoringHouse  set "
                            strSQL = strSQL + " " + strFields + " "
                            strSQL = strSQL + " where MonitoringID = @C_ID"

                            objSqlCommand.Parameters.Clear()
                            For i = 0 To intCount - 1 Step 1
                                strValue = objNewData.Item(i).Trim()
                                Select Case objNewData.GetKey(i)

                                    Case Else
                                        If strValue = "" Then strValue = " "
                                        objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), strValue)
                                End Select
                            Next
                            objSqlCommand.Parameters.AddWithValue("@C_ID", intID)
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()
                    End Select

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

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doSaveSunshineMonthMonitoringHouse = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 删除月楼盘匹配的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteSunshineMonthMonitoringHouse( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow) As Boolean


            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            doDeleteSunshineMonthMonitoringHouse = False
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""

                '检查
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If objOldData Is Nothing Then
                    strErrMsg = "错误：未传入旧的数据！"
                    GoTo errProc
                End If

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
                Dim strSQL As String
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '计算SQL
                    Dim strOldDM As String
                    strSQL = ""

                    With New Xydc.Platform.Common.Utilities.PulicParameters
                        strOldDM = .getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.SunshineData.FIELD_Sunshine_B_WeekMonitoringHouse_ID), "")
                    End With
                    strSQL = strSQL + " delete from Sunshine_B_MonthMonitoringHouse"
                    strSQL = strSQL + " where MonitoringID = @C_ID"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@C_ID", strOldDM)

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

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doDeleteSunshineMonthMonitoringHouse = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function
        End Function




        '----------------------------------------------------------------
        ' 检查排序号是否存在
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     Isort                ：序号
        '     Itype                ：类型1-商业；2-住宅；3-办公 
        '     blnExist             ：是否已存在
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function checkSort( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal Isort As String, _
            ByVal Itype As String, _
            ByRef blnExist As Boolean) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempSunshineData As Xydc.Platform.Common.Data.SunshineData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objDataSet As System.Data.DataSet = Nothing

            '初始化
            checkSort = False
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
              
                '检查
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                Dim strSQL As String

                strSQL = ""
                strSQL = strSQL + " select * from T_HOUSE_MATCH_SORT " + vbCr
                strSQL = strSQL + " where I_Type = convert(integer," + Itype + ") and  I_Sort = convert(integer," + Isort + ")" + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If

                '返回
                If objDataSet.Tables(0) Is Nothing Then
                    Exit Try
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    Exit Try
                Else
                    blnExist = True
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try
           

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            checkSort = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempSunshineData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function


        '----------------------------------------------------------------
        ' 保存楼盘排序，序号插入中间，其他序号+1的情况
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objNewData           ：新数据
        '     objenumEditType      ：编辑类型
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doSaveExistSort( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean


            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim intID As Integer
            Dim strType As String
            Dim strSort As String

            '初始化
            doSaveExistSort = False
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""

                '检查
                If strUserId.Trim = "" Then
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
                        intID = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.SunshineData.FIELD_Sunshine_B_HOUSEMATCHSORT_ID), 0)
                        '计算SQL
                End Select


                strSort = objNewData.Item(1).Trim()
                strType = objNewData.Item(2).Trim()

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

                '保存数据
                Dim strSQL As String
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout


                    '计算SQL
                    Dim strFields As String
                    Dim strValues As String
                    Dim intCount As Integer
                    Dim strValue As String
                    Dim i As Integer

                    '先把原来的序号更新
                    strSQL = ""
                    strSQL = strSQL + " update T_HOUSE_MATCH_SORT  set I_Sort=I_Sort+1"
                    strSQL = strSQL + " where I_Type = convert(integer," + strType + ") and  I_Sort >= convert(integer," + strSort + ")" + vbCr

                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '插入新的排序
                    intCount = objNewData.Count
                    'strID = objNewData.Item(Xydc.Platform.Common.Data.SunshineData.FIELD_Sunshine_B_HOUSE_MATCH_ID)

                    Select Case objenumEditType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                            strFields = ""
                            strValues = ""
                            For i = 0 To intCount - 1 Step 1
                                If strFields = "" Then
                                    strFields = objNewData.GetKey(i)
                                Else
                                    strFields = strFields + "," + objNewData.GetKey(i)
                                End If
                                If strValues = "" Then
                                    strValues = "@A" + i.ToString()
                                Else
                                    strValues = strValues + "," + "@A" + i.ToString()
                                End If
                            Next

                            strSQL = ""
                            strSQL = strSQL + " insert into T_HOUSE_MATCH_SORT (" + strFields + ")"
                            strSQL = strSQL + " values (" + strValues + ")"
                            objSqlCommand.Parameters.Clear()
                            For i = 0 To intCount - 1 Step 1
                                strValue = objNewData.Item(i).Trim()
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.SunshineData.FIELD_Sunshine_B_HOUSEMATCHSORT_Sort, _
                                        Xydc.Platform.Common.Data.SunshineData.FIELD_Sunshine_B_HOUSEMATCHSORT_Type

                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), 0)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(objNewData.Item(i), Integer))
                                        End If
                                    Case Else
                                        If strValue = "" Then strValue = " "
                                        objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), strValue)
                                End Select
                            Next
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()

                        Case Else
                            strFields = ""
                            For i = 0 To intCount - 1 Step 1
                                If strFields = "" Then
                                    strFields = objNewData.GetKey(i) + " = @A" + i.ToString()
                                Else
                                    strFields = strFields + "," + objNewData.GetKey(i) + " = @A" + i.ToString()
                                End If
                            Next
                            strSQL = ""
                            strSQL = strSQL + " update T_HOUSE_MATCH_SORT  set "
                            strSQL = strSQL + " " + strFields + " "
                            strSQL = strSQL + " where I_ID = @C_ID"

                            objSqlCommand.Parameters.Clear()
                            For i = 0 To intCount - 1 Step 1
                                strValue = objNewData.Item(i).Trim()
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.SunshineData.FIELD_Sunshine_B_HOUSEMATCHSORT_Sort, _
                                        Xydc.Platform.Common.Data.SunshineData.FIELD_Sunshine_B_HOUSEMATCHSORT_Type

                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), 0)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(objNewData.Item(i), Integer))
                                        End If
                                    Case Else
                                        If strValue = "" Then strValue = " "
                                        objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), strValue)
                                End Select
                            Next

                            objSqlCommand.Parameters.AddWithValue("@C_ID", intID)
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()
                    End Select

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

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doSaveExistSort = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function




        '----------------------------------------------------------------
        ' 根据“查询条件”获取“阳光家缘楼盘信息数据分析”完全数据的数据集
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strSQL                     ：返回的SQL语句
        '     strType                    ：选择楼盘的类型。0-全部；1-商业；2-住宅；3-办公
        '     strWhere                   ：搜索字符串      
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getSql_BuildingCompute_x3_v2( _
             ByRef strErrMsg As String, _
             ByRef strSQL As String, _
             ByVal strWhere As String, _
             ByRef strType As String) As Boolean

            getSql_BuildingCompute_x3_v2 = False

            strSQL = ""
            Select Case strType

                Case "2"
                    strSQL = strSQL + "  select a.*,isnull(b.i_sort,100000) as '楼盘排序' from ( "
                    strSQL = strSQL + "       select b.行政区域,b.楼盘名称,case when b.房屋类型='1' then '别墅' else '洋房' end as 项目类型,  "
                    strSQL = strSQL + "          a.累计已售套数 as 合共成交,   "
                    strSQL = strSQL + "          a.累计已售面积,  "
                    strSQL = strSQL + "          a.未售套数,   "
                    strSQL = strSQL + "          a.未售面积,   "
                    strSQL = strSQL + "          退房数,  "
                    strSQL = strSQL + "          网签数,  "
                    strSQL = strSQL + "          网签面积,  "
                    strSQL = strSQL + "          网签总额,  "
                    strSQL = strSQL + "          签约均价=case when b.网签面积=0 or b.网签数=0 then dbo.Sunshine_F_getDayHousePrice(b.楼盘名称,@endDate,b.房屋类型) "
                    strSQL = strSQL + "                         else  cast(round((b.网签总额)/(b.网签面积),2) as numeric(16,2))  end, "
                    strSQL = strSQL + "          累计均价=case when a.累计已售面积=0 or a.累计已售套数=0 then 0 else  cast(round((a.累计签约总额)/(a.累计已售面积),2) as numeric(16,2))  end "
                    strSQL = strSQL + "           from  "
                    strSQL = strSQL + "           (  "
                    strSQL = strSQL + "           select  行政区域,C_HOUSE as 楼盘名称, isnull(c_type,0) as 房屋类型,  "
                    strSQL = strSQL + "                   sum(当日退房套数) as 退房数, "
                    strSQL = strSQL + "                   sum(当日签约套数) as 网签数, "
                    strSQL = strSQL + "                   sum(当日签约面积) as 网签面积, "
                    strSQL = strSQL + "                   sum(当日签约总额) as 网签总额 from  "
                    strSQL = strSQL + "                     ( "
                    strSQL = strSQL + "                     select a.*,c.c_house,c.c_type from T_DAY_HOUSE_INFO a left join T_HOUSE_MATCH_XMID c on a.项目名称=c.C_XM_NAME and a.预售证=c.C_XM_ID "
                    strSQL = strSQL + "                       where a.日期  between @startDate and @endDate  "
                    strSQL = strSQL + "                     )a  group by 行政区域,C_HOUSE,c_type   "
                    strSQL = strSQL + "            )b  "
                    strSQL = strSQL + "            left  Join  "
                    strSQL = strSQL + "           ("
                    strSQL = strSQL + "               select  行政区域,a.C_HOUSE as 楼盘名称, isnull(c_type,0) as 房屋类型,  "
                    strSQL = strSQL + "                   sum(累计已售套数) as 累计已售套数,    "
                    strSQL = strSQL + "                   sum(累计已售面积) as 累计已售面积,  "
                    strSQL = strSQL + "                   sum(未售套数) as 未售套数,    "
                    strSQL = strSQL + "                   sum(未售面积) as 未售面积, "
                    strSQL = strSQL + "                   sum(累计已售面积*累计已售均价) as 累计签约总额 from "
                    strSQL = strSQL + "                     ( "
                    strSQL = strSQL + "                     select a.*,c.c_house,c.c_type from T_DAY_HOUSE_INFO a left join T_HOUSE_MATCH_XMID c on a.项目名称=c.C_XM_NAME and a.预售证=c.C_XM_ID  "
                    strSQL = strSQL + "                      where a.日期=@endDate  "
                    strSQL = strSQL + "                     )a  group by 行政区域,C_HOUSE,c_type   "
                    strSQL = strSQL + "           )a on b.楼盘名称=a.楼盘名称 and b.房屋类型=a.房屋类型  "
                    strSQL = strSQL + "  ) A left join (select * from T_HOUSE_MATCH_SORT where i_type=convert(integer,'" + strType + "')) b on b.c_NAME=a.楼盘名称 "
                    If strWhere <> "" Then
                        strSQL = strSQL + "where " + strWhere
                    End If

                Case "3"
                    strSQL = strSQL + "  select a.*,isnull(b.i_sort,100000) as '楼盘排序' from "
                    strSQL = strSQL + "  (	"
                    strSQL = strSQL + "     select b.行政区域,b.楼盘名称,b.项目类型, "
                    strSQL = strSQL + "          a.累计已售套数 as 合共成交,   "
                    strSQL = strSQL + "          a.累计已售面积,  "
                    strSQL = strSQL + "          a.未售套数,   "
                    strSQL = strSQL + "          a.未售面积,   "
                    strSQL = strSQL + "          退房数,  "
                    strSQL = strSQL + "          网签数,  "
                    strSQL = strSQL + "          网签面积,  "
                    strSQL = strSQL + "          网签总额,  "
                    strSQL = strSQL + "          签约均价=case when b.网签面积=0 or b.网签数=0 then dbo.Sunshine_F_getDayOfficePrice(b.楼盘名称,@endDate) "
                    strSQL = strSQL + "                         else  cast(round((b.网签总额)/(b.网签面积),2) as numeric(16,2))  end, "
                    strSQL = strSQL + "          累计均价=case when a.累计已售面积=0 or a.累计已售套数=0 then 0 else  cast(round((a.累计签约总额)/(a.累计已售面积),2) as numeric(16,2))  end "
                    strSQL = strSQL + "           from  "
                    strSQL = strSQL + "           (  "
                    strSQL = strSQL + "         select  行政区域,C_HOUSE as 楼盘名称, '办公' as 项目类型, "
                    strSQL = strSQL + "                   sum(当日退房套数) as 退房数, "
                    strSQL = strSQL + "                   sum(当日签约套数) as 网签数, "
                    strSQL = strSQL + "                   sum(当日签约面积) as 网签面积, "
                    strSQL = strSQL + "                   sum(当日签约总额) as 网签总额 from  "
                    strSQL = strSQL + "                     ( "
                    strSQL = strSQL + "                     select a.*,c.c_house from T_DAY_Office_INFO a left join T_HOUSE_MATCH_XMID c on a.项目名称=c.C_XM_NAME and a.预售证=c.C_XM_ID "
                    strSQL = strSQL + "                       where a.日期  between @startDate and @endDate  "
                    strSQL = strSQL + "                     )a  group by 行政区域,C_HOUSE  "
                    strSQL = strSQL + "          )b "
                    strSQL = strSQL + "          left  Join "
                    strSQL = strSQL + "         ("
                    strSQL = strSQL + "             select  行政区域,a.C_HOUSE as 楼盘名称, '办公' as 项目类型, "
                    strSQL = strSQL + "                   sum(累计已售套数) as 累计已售套数,    "
                    strSQL = strSQL + "                   sum(累计已售面积) as 累计已售面积,  "
                    strSQL = strSQL + "                   sum(未售套数) as 未售套数,    "
                    strSQL = strSQL + "                   sum(未售面积) as 未售面积, "
                    strSQL = strSQL + "                   sum(累计已售面积*累计已售均价) as 累计签约总额 from "
                    strSQL = strSQL + "                     ( "
                    strSQL = strSQL + "                     select a.*,c.c_house from T_DAY_Office_INFO a left join T_HOUSE_MATCH_XMID c on a.项目名称=c.C_XM_NAME and a.预售证=c.C_XM_ID  "
                    strSQL = strSQL + "                      where a.日期=@endDate  "
                    strSQL = strSQL + "                     )a  group by 行政区域,C_HOUSE  "
                    strSQL = strSQL + "         )a on b.楼盘名称=a.楼盘名称 "
                    strSQL = strSQL + "     ) A left join (select * from T_HOUSE_MATCH_SORT where i_type=convert(integer,'" + strType + "')) b on b.c_NAME=a.楼盘名称 "
                    If strWhere <> "" Then
                        strSQL = strSQL + "where " + strWhere
                    End If
                Case "1"
                    strSQL = strSQL + "  select a.*,isnull(b.i_sort,100000) as '楼盘排序' from "
                    strSQL = strSQL + "  (	"
                    strSQL = strSQL + "     select b.行政区域,b.楼盘名称,b.项目类型, "
                    strSQL = strSQL + "          a.累计已售套数 as 合共成交,   "
                    strSQL = strSQL + "          a.累计已售面积,  "
                    strSQL = strSQL + "          a.未售套数,   "
                    strSQL = strSQL + "          a.未售面积,   "
                    strSQL = strSQL + "          退房数,  "
                    strSQL = strSQL + "          网签数,  "
                    strSQL = strSQL + "          网签面积,  "
                    strSQL = strSQL + "          网签总额,  "
                    strSQL = strSQL + "          签约均价=case when b.网签面积=0 or b.网签数=0 then dbo.Sunshine_F_getDayBusinessPrice(b.楼盘名称,@endDate) "
                    strSQL = strSQL + "                         else  cast(round((b.网签总额)/(b.网签面积),2) as numeric(16,2))  end, "
                    strSQL = strSQL + "          累计均价=case when a.累计已售面积=0 or a.累计已售套数=0 then 0 else  cast(round((a.累计签约总额)/(a.累计已售面积),2) as numeric(16,2))  end "
                    strSQL = strSQL + "           from  "
                    strSQL = strSQL + "         ( "
                    strSQL = strSQL + "         select  行政区域,C_HOUSE as 楼盘名称, '商业' as 项目类型, "
                    strSQL = strSQL + "                   sum(当日退房套数) as 退房数, "
                    strSQL = strSQL + "                   sum(当日签约套数) as 网签数, "
                    strSQL = strSQL + "                   sum(当日签约面积) as 网签面积, "
                    strSQL = strSQL + "                   sum(当日签约总额) as 网签总额 from  "
                    strSQL = strSQL + "                     ( "
                    strSQL = strSQL + "                     select a.*,c.c_house from T_DAY_Business_INFO a left join T_HOUSE_MATCH_XMID c on a.项目名称=c.C_XM_NAME and a.预售证=c.C_XM_ID "
                    strSQL = strSQL + "                       where a.日期  between @startDate and @endDate  "
                    strSQL = strSQL + "                     )a  group by 行政区域,C_HOUSE "
                    strSQL = strSQL + "          )b "
                    strSQL = strSQL + "          left  Join "
                    strSQL = strSQL + "         ("
                    strSQL = strSQL + "             select  行政区域,a.C_HOUSE as 楼盘名称, '商业' as 项目类型, "
                    strSQL = strSQL + "                   sum(累计已售套数) as 累计已售套数,    "
                    strSQL = strSQL + "                   sum(累计已售面积) as 累计已售面积,  "
                    strSQL = strSQL + "                   sum(未售套数) as 未售套数,    "
                    strSQL = strSQL + "                   sum(未售面积) as 未售面积, "
                    strSQL = strSQL + "                   sum(累计已售面积*累计已售均价) as 累计签约总额 from "
                    strSQL = strSQL + "                     ( "
                    strSQL = strSQL + "                     select a.*,c.c_house from T_DAY_Business_INFO a left join T_HOUSE_MATCH_XMID c on a.项目名称=c.C_XM_NAME and a.预售证=c.C_XM_ID  "
                    strSQL = strSQL + "                      where a.日期=@endDate  "
                    strSQL = strSQL + "                     )a  group by 行政区域,C_HOUSE "
                    strSQL = strSQL + "         )a on b.楼盘名称=a.楼盘名称 "
                    strSQL = strSQL + "     ) A left join (select * from T_HOUSE_MATCH_SORT where i_type=convert(integer,'" + strType + "')) b on b.c_NAME=a.楼盘名称 "
                    If strWhere <> "" Then
                        strSQL = strSQL + "where " + strWhere
                    End If
                Case Else

            End Select

            getSql_BuildingCompute_x3_v2 = True
errProc:

            Exit Function
        End Function


        '----------------------------------------------------------------
        ' 根据“查询条件”获取“阳光家缘楼盘信息数据分析”完全数据的数据集
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strSQL                     ：返回的SQL语句
        '     strType                    ：选择楼盘的类型。0-全部；1-商业；2-住宅；3-办公
        '     strWhere                   ：搜索字符串      
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getSql_BuildingCompute_x3_v3( _
             ByRef strErrMsg As String, _
             ByRef strSQL As String, _
             ByVal strWhere As String, _
             ByRef strType As String) As Boolean

            getSql_BuildingCompute_x3_v3 = False

            strSQL = ""
            Select Case strType

                Case "2"
                    strSQL = strSQL + "  select a.*,isnull(b.i_sort,100000) as '楼盘排序' from "
                    strSQL = strSQL + "  (	"
                    strSQL = strSQL + "     select 行政区域,楼盘名称,项目类型,退房数,网签数,网签面积,网签总额,合共成交,累计已售面积,未售套数,未售面积,签约均价=case when 网签面积=0 then cast(round(网签均价,2) as numeric(16,2)) else cast(round(网签总额/网签面积,2) as numeric(16,2)) end,累计均价 from	"
                    strSQL = strSQL + "     (  "
                    strSQL = strSQL + "        select * from "
                    strSQL = strSQL + "            (  "
                    strSQL = strSQL + "               select 行政区域,楼盘名称,项目类型, sum(退房数) as 退房数,sum(网签数) as 网签数, sum(网签面积) as 网签面积, sum(网签总额) as 网签总额  from t_day_house_detail  "
                    strSQL = strSQL + "   	          where 日期 between @startDate  and  @endDate  group by 行政区域,楼盘名称,项目类型 "
                    strSQL = strSQL + "   	        )b  "
                    strSQL = strSQL + "             left join  "
                    strSQL = strSQL + "            (  "
                    strSQL = strSQL + "   	          select 行政区域 as 行政区域1,楼盘名称 as 楼盘名称1,项目类型 as 项目类型1,合共成交,累计已售面积,未售套数,未售面积,签约均价 as 网签均价,累计均价 from t_day_house_detail where 日期=@endDate  "
                    strSQL = strSQL + "   	        )a  "
                    strSQL = strSQL + "   	       on b.楼盘名称=a.楼盘名称1 and b.项目类型=a.项目类型1  "
                    strSQL = strSQL + "   	)a  "
                    strSQL = strSQL + "  ) A left join (select * from T_HOUSE_MATCH_SORT where i_type=convert(integer,'" + strType + "')) b on b.c_NAME=a.楼盘名称 "
                    If strWhere <> "" Then
                        strSQL = strSQL + "where " + strWhere
                    End If

                Case "3"
                    strSQL = strSQL + "  select a.*,isnull(b.i_sort,100000) as '楼盘排序' from "
                    strSQL = strSQL + "  (	"
                    strSQL = strSQL + "     select 行政区域,楼盘名称,项目类型,退房数,网签数,网签面积,网签总额,合共成交,累计已售面积,未售套数,未售面积,签约均价=case when 网签面积=0 then cast(round(网签均价,2) as numeric(16,2)) else cast(round(网签总额/网签面积,2) as numeric(16,2)) end,累计均价 from	"
                    strSQL = strSQL + "     (  "
                    strSQL = strSQL + "        select * from "
                    strSQL = strSQL + "            (  "
                    strSQL = strSQL + "               select 行政区域,楼盘名称,sum(退房数) as 退房数,sum(网签数) as 网签数, sum(网签面积) as 网签面积, sum(网签总额) as 网签总额  from t_day_office_detail  "
                    strSQL = strSQL + "   	          where 日期 between @startDate  and  @endDate  group by 行政区域,楼盘名称  "
                    strSQL = strSQL + "   	        )b  "
                    strSQL = strSQL + "             left join  "
                    strSQL = strSQL + "            (  "
                    strSQL = strSQL + "   	          select 行政区域 as 行政区域1,楼盘名称 as 楼盘名称1,项目类型,合共成交,累计已售面积,未售套数,未售面积,签约均价 as 网签均价,累计均价 from t_day_office_detail where 日期=@endDate  "
                    strSQL = strSQL + "   	        )a  "
                    strSQL = strSQL + "   	       on b.楼盘名称=a.楼盘名称1  "
                    strSQL = strSQL + "   	)a  "
                    strSQL = strSQL + "     ) A left join (select * from T_HOUSE_MATCH_SORT where i_type=convert(integer,'" + strType + "')) b on b.c_NAME=a.楼盘名称 "
                    If strWhere <> "" Then
                        strSQL = strSQL + "where " + strWhere
                    End If
                Case "1"
                    strSQL = strSQL + "  select a.*,isnull(b.i_sort,100000) as '楼盘排序' from "
                    strSQL = strSQL + "  (	"
                    strSQL = strSQL + "     select 行政区域,楼盘名称,项目类型,退房数,网签数,网签面积,网签总额,合共成交,累计已售面积,未售套数,未售面积,签约均价=case when 网签面积=0 then cast(round(网签均价,2) as numeric(16,2)) else cast(round(网签总额/网签面积,2) as numeric(16,2)) end,累计均价 from	"
                    strSQL = strSQL + "     (  "
                    strSQL = strSQL + "        select * from "
                    strSQL = strSQL + "            (  "
                    strSQL = strSQL + "               select 行政区域,楼盘名称,sum(退房数) as 退房数,sum(网签数) as 网签数, sum(网签面积) as 网签面积, sum(网签总额) as 网签总额  from t_day_business_detail  "
                    strSQL = strSQL + "   	          where 日期 between @startDate  and  @endDate  group by 行政区域,楼盘名称  "
                    strSQL = strSQL + "   	        )b  "
                    strSQL = strSQL + "             left join  "
                    strSQL = strSQL + "            (  "
                    strSQL = strSQL + "   	          select 行政区域 as 行政区域1,楼盘名称 as 楼盘名称1,项目类型,合共成交,累计已售面积,未售套数,未售面积,签约均价 as 网签均价,累计均价 from t_day_business_detail where 日期=@endDate  "
                    strSQL = strSQL + "   	        )a  "
                    strSQL = strSQL + "   	       on b.楼盘名称=a.楼盘名称1  "
                    strSQL = strSQL + "   	)a  "
                    strSQL = strSQL + "     ) A left join (select * from T_HOUSE_MATCH_SORT where i_type=convert(integer,'" + strType + "')) b on b.c_NAME=a.楼盘名称 "
                    If strWhere <> "" Then
                        strSQL = strSQL + "where " + strWhere
                    End If
                Case Else

            End Select

            getSql_BuildingCompute_x3_v3 = True
errProc:

            Exit Function
        End Function


        '----------------------------------------------------------------
        ' 根据“查询条件”获取“阳光家缘楼盘信息数据分析”完全数据的数据集
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strSQL                     ：返回的SQL语句
        '     strType                    ：选择楼盘的类型。0-全部；1-商业；2-住宅；3-办公
        '     strWhere                   ：搜索字符串      
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getSql_FrontBuildingCompute_x3_v3( _
             ByRef strErrMsg As String, _
             ByRef strSQL As String, _
             ByVal strWhere As String, _
             ByRef strType As String) As Boolean

            getSql_FrontBuildingCompute_x3_v3 = False

            strSQL = ""
            Select Case strType

                Case "2"
                    strSQL = strSQL + "  select a.*,isnull(b.i_sort,100000) as '楼盘排序' from "
                    strSQL = strSQL + "  (	"
                    strSQL = strSQL + "     select 行政区域,楼盘名称,项目类型,退房数,网签数,网签面积,网签总额,合共成交,累计已售面积,未售套数,未售面积,签约均价=case when 网签面积=0 then cast(round(网签均价,2) as numeric(16,2)) else cast(round(网签总额/网签面积,2) as numeric(16,2)) end,累计均价 from	"
                    strSQL = strSQL + "     (  "
                    strSQL = strSQL + "        select * from "
                    strSQL = strSQL + "            (  "
                    strSQL = strSQL + "               select 行政区域,楼盘名称,项目类型, sum(退房数) as 退房数,sum(网签数) as 网签数, sum(网签面积) as 网签面积, sum(网签总额) as 网签总额  from t_day_house_detail  "
                    strSQL = strSQL + "   	          where 日期 between @frontStartDate and @frontEndDate  group by 行政区域,楼盘名称,项目类型 "
                    strSQL = strSQL + "   	        )b  "
                    strSQL = strSQL + "             left join  "
                    strSQL = strSQL + "            (  "
                    strSQL = strSQL + "   	          select 行政区域 as 行政区域1,楼盘名称 as 楼盘名称1,项目类型 as 项目类型1,合共成交,累计已售面积,未售套数,未售面积,签约均价 as 网签均价,累计均价 from t_day_house_detail where 日期=@frontEndDate  "
                    strSQL = strSQL + "   	        )a  "
                    strSQL = strSQL + "   	       on b.楼盘名称=a.楼盘名称1 and b.项目类型=a.项目类型1  "
                    strSQL = strSQL + "   	)a  "
                    strSQL = strSQL + "  ) A left join (select * from T_HOUSE_MATCH_SORT where i_type=convert(integer,'" + strType + "')) b on b.c_NAME=a.楼盘名称 "
                    If strWhere <> "" Then
                        strSQL = strSQL + "where " + strWhere
                    End If

                Case "3"
                    strSQL = strSQL + "  select a.*,isnull(b.i_sort,100000) as '楼盘排序' from "
                    strSQL = strSQL + "  (	"
                    strSQL = strSQL + "     select 行政区域,楼盘名称,项目类型,退房数,网签数,网签面积,网签总额,合共成交,累计已售面积,未售套数,未售面积,签约均价=case when 网签面积=0 then cast(round(网签均价,2) as numeric(16,2)) else cast(round(网签总额/网签面积,2) as numeric(16,2)) end,累计均价 from	"
                    strSQL = strSQL + "     (  "
                    strSQL = strSQL + "        select * from "
                    strSQL = strSQL + "            (  "
                    strSQL = strSQL + "               select 行政区域,楼盘名称,sum(退房数) as 退房数,sum(网签数) as 网签数, sum(网签面积) as 网签面积, sum(网签总额) as 网签总额  from t_day_office_detail  "
                    strSQL = strSQL + "   	          where 日期 between @frontStartDate and @frontEndDate  group by 行政区域,楼盘名称  "
                    strSQL = strSQL + "   	        )b  "
                    strSQL = strSQL + "             left join  "
                    strSQL = strSQL + "            (  "
                    strSQL = strSQL + "   	          select 行政区域 as 行政区域1,楼盘名称 as 楼盘名称1,项目类型,合共成交,累计已售面积,未售套数,未售面积,签约均价 as 网签均价,累计均价 from t_day_office_detail where 日期=@frontEndDate  "
                    strSQL = strSQL + "   	        )a  "
                    strSQL = strSQL + "   	       on b.楼盘名称=a.楼盘名称1  "
                    strSQL = strSQL + "   	)a  "
                    strSQL = strSQL + "     ) A left join (select * from T_HOUSE_MATCH_SORT where i_type=convert(integer,'" + strType + "')) b on b.c_NAME=a.楼盘名称 "
                    If strWhere <> "" Then
                        strSQL = strSQL + "where " + strWhere
                    End If
                Case "1"
                    strSQL = strSQL + "  select a.*,isnull(b.i_sort,100000) as '楼盘排序' from "
                    strSQL = strSQL + "  (	"
                    strSQL = strSQL + "     select 行政区域,楼盘名称,项目类型,退房数,网签数,网签面积,网签总额,合共成交,累计已售面积,未售套数,未售面积,签约均价=case when 网签面积=0 then cast(round(网签均价,2) as numeric(16,2)) else cast(round(网签总额/网签面积,2) as numeric(16,2)) end,累计均价 from	"
                    strSQL = strSQL + "     (  "
                    strSQL = strSQL + "        select * from "
                    strSQL = strSQL + "            (  "
                    strSQL = strSQL + "               select 行政区域,楼盘名称,sum(退房数) as 退房数,sum(网签数) as 网签数, sum(网签面积) as 网签面积, sum(网签总额) as 网签总额  from t_day_business_detail  "
                    strSQL = strSQL + "   	          where 日期 between @frontStartDate and @frontEndDate  group by 行政区域,楼盘名称  "
                    strSQL = strSQL + "   	        )b  "
                    strSQL = strSQL + "             left join  "
                    strSQL = strSQL + "            (  "
                    strSQL = strSQL + "   	          select 行政区域 as 行政区域1,楼盘名称 as 楼盘名称1,项目类型,合共成交,累计已售面积,未售套数,未售面积,签约均价 as 网签均价,累计均价 from t_day_business_detail where 日期=@frontEndDate  "
                    strSQL = strSQL + "   	        )a  "
                    strSQL = strSQL + "   	       on b.楼盘名称=a.楼盘名称1  "
                    strSQL = strSQL + "   	)a  "
                    strSQL = strSQL + "     ) A left join (select * from T_HOUSE_MATCH_SORT where i_type=convert(integer,'" + strType + "')) b on b.c_NAME=a.楼盘名称 "
                    If strWhere <> "" Then
                        strSQL = strSQL + "where " + strWhere
                    End If
                Case Else

            End Select

            getSql_FrontBuildingCompute_x3_v3 = True
errProc:

            Exit Function
        End Function






        '----------------------------------------------------------------
        ' 根据“查询条件”获取“阳光家缘楼盘信息数据分析”完全数据的数据集
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strSQL                     ：返回的SQL语句
        '     strType                    ：选择楼盘的类型。0-全部；1-商业；2-住宅；3-办公
        '     strWhere                   ：搜索字符串      
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getSql_FrontBuildingCompute_x3_v2( _
             ByRef strErrMsg As String, _
             ByRef strSQL As String, _
             ByVal strWhere As String, _
             ByRef strType As String) As Boolean

            getSql_FrontBuildingCompute_x3_v2 = False

            strSQL = ""
            Select Case strType

                Case "2"
                    strSQL = strSQL + "  select a.*,isnull(b.i_sort,100000) as '楼盘排序' from ( "
                    strSQL = strSQL + "       select b.行政区域,b.楼盘名称,case when b.房屋类型='1' then '别墅' else '洋房' end as 项目类型,  "
                    strSQL = strSQL + "          a.累计已售套数 as 合共成交,   "
                    strSQL = strSQL + "          a.累计已售面积,  "
                    strSQL = strSQL + "          a.未售套数,   "
                    strSQL = strSQL + "          a.未售面积,   "
                    strSQL = strSQL + "          退房数,  "
                    strSQL = strSQL + "          网签数,  "
                    strSQL = strSQL + "          网签面积,  "
                    strSQL = strSQL + "          网签总额,  "
                    'strSQL = strSQL + "          签约均价=case when b.网签面积=0 or b.网签数=0 then dbo.Sunshine_F_getDayHousePrice(b.楼盘名称,@frontEndDate,b.房屋类型) "
                    strSQL = strSQL + "          签约均价=case when b.网签面积=0 or b.网签数=0 then 0 "
                    strSQL = strSQL + "                         else  cast(round((b.网签总额)/(b.网签面积),2) as numeric(16,2))  end, "
                    strSQL = strSQL + "          累计均价=case when a.累计已售面积=0 or a.累计已售套数=0 then 0 else  cast(round((a.累计签约总额)/(a.累计已售面积),2) as numeric(16,2))  end "
                    strSQL = strSQL + "           from  "
                    strSQL = strSQL + "           (  "
                    strSQL = strSQL + "           select  行政区域,C_HOUSE as 楼盘名称, isnull(c_type,0) as 房屋类型,  "
                    strSQL = strSQL + "                   sum(当日退房套数) as 退房数, "
                    strSQL = strSQL + "                   sum(当日签约套数) as 网签数, "
                    strSQL = strSQL + "                   sum(当日签约面积) as 网签面积, "
                    strSQL = strSQL + "                   sum(当日签约总额) as 网签总额 from  "
                    strSQL = strSQL + "                     ( "
                    strSQL = strSQL + "                     select a.*,c.c_house,c.c_type from T_DAY_HOUSE_INFO a left join T_HOUSE_MATCH_XMID c on a.项目名称=c.C_XM_NAME and a.预售证=c.C_XM_ID "
                    strSQL = strSQL + "                       where a.日期  between @frontStartDate and @frontEndDate  "
                    strSQL = strSQL + "                     )a  group by 行政区域,C_HOUSE,c_type   "
                    strSQL = strSQL + "            )b  "
                    strSQL = strSQL + "            left  Join  "
                    strSQL = strSQL + "           ("
                    strSQL = strSQL + "               select  行政区域,a.C_HOUSE as 楼盘名称, isnull(c_type,0) as 房屋类型,  "
                    strSQL = strSQL + "                   sum(累计已售套数) as 累计已售套数,    "
                    strSQL = strSQL + "                   sum(累计已售面积) as 累计已售面积,  "
                    strSQL = strSQL + "                   sum(未售套数) as 未售套数,    "
                    strSQL = strSQL + "                   sum(未售面积) as 未售面积, "
                    strSQL = strSQL + "                   sum(累计已售面积*累计已售均价) as 累计签约总额 from "
                    strSQL = strSQL + "                     ( "
                    strSQL = strSQL + "                     select a.*,c.c_house,c.c_type from T_DAY_HOUSE_INFO a left join T_HOUSE_MATCH_XMID c on a.项目名称=c.C_XM_NAME and a.预售证=c.C_XM_ID  "
                    strSQL = strSQL + "                      where a.日期=@frontEndDate  "
                    strSQL = strSQL + "                     )a  group by 行政区域,C_HOUSE,c_type   "
                    strSQL = strSQL + "           )a on b.楼盘名称=a.楼盘名称 and b.房屋类型=a.房屋类型  "
                    strSQL = strSQL + "  ) A left join (select * from T_HOUSE_MATCH_SORT where i_type=convert(integer,'" + strType + "')) b on b.c_NAME=a.楼盘名称 "
                    If strWhere <> "" Then
                        strSQL = strSQL + "where " + strWhere
                    End If

                Case "3"
                    strSQL = strSQL + "  select a.*,isnull(b.i_sort,100000) as '楼盘排序' from "
                    strSQL = strSQL + "  (	"
                    strSQL = strSQL + "     select b.行政区域,b.楼盘名称,b.项目类型, "
                    strSQL = strSQL + "          a.累计已售套数 as 合共成交,   "
                    strSQL = strSQL + "          a.累计已售面积,  "
                    strSQL = strSQL + "          a.未售套数,   "
                    strSQL = strSQL + "          a.未售面积,   "
                    strSQL = strSQL + "          退房数,  "
                    strSQL = strSQL + "          网签数,  "
                    strSQL = strSQL + "          网签面积,  "
                    strSQL = strSQL + "          网签总额,  "
                    strSQL = strSQL + "          签约均价=case when b.网签面积=0 or b.网签数=0 then dbo.Sunshine_F_getDayOfficePrice(b.楼盘名称,@frontEndDate) "
                    strSQL = strSQL + "                         else  cast(round((b.网签总额)/(b.网签面积),2) as numeric(16,2))  end, "
                    strSQL = strSQL + "          累计均价=case when a.累计已售面积=0 or a.累计已售套数=0 then 0 else  cast(round((a.累计签约总额)/(a.累计已售面积),2) as numeric(16,2))  end "
                    strSQL = strSQL + "           from  "
                    strSQL = strSQL + "         ( "
                    strSQL = strSQL + "         select  行政区域,C_HOUSE as 楼盘名称, '办公' as 项目类型, "
                    strSQL = strSQL + "                   sum(当日退房套数) as 退房数, "
                    strSQL = strSQL + "                   sum(当日签约套数) as 网签数, "
                    strSQL = strSQL + "                   sum(当日签约面积) as 网签面积, "
                    strSQL = strSQL + "                   sum(当日签约总额) as 网签总额 from  "
                    strSQL = strSQL + "                     ( "
                    strSQL = strSQL + "                     select a.*,c.c_house from T_DAY_Office_INFO a left join T_HOUSE_MATCH_XMID c on a.项目名称=c.C_XM_NAME and a.预售证=c.C_XM_ID "
                    strSQL = strSQL + "                       where a.日期  between @frontStartDate and @frontEndDate  "
                    strSQL = strSQL + "                     )a  group by 行政区域,C_HOUSE "
                    strSQL = strSQL + "          )b "
                    strSQL = strSQL + "          left  Join "
                    strSQL = strSQL + "         ("
                    strSQL = strSQL + "             select  行政区域,a.C_HOUSE as 楼盘名称, '办公' as 项目类型, "
                    strSQL = strSQL + "                   sum(累计已售套数) as 累计已售套数,    "
                    strSQL = strSQL + "                   sum(累计已售面积) as 累计已售面积,  "
                    strSQL = strSQL + "                   sum(未售套数) as 未售套数,    "
                    strSQL = strSQL + "                   sum(未售面积) as 未售面积, "
                    strSQL = strSQL + "                   sum(累计已售面积*累计已售均价) as 累计签约总额 from "
                    strSQL = strSQL + "                     ( "
                    strSQL = strSQL + "                     select a.*,c.c_house from T_DAY_Office_INFO a left join T_HOUSE_MATCH_XMID c on a.项目名称=c.C_XM_NAME and a.预售证=c.C_XM_ID  "
                    strSQL = strSQL + "                      where a.日期=@frontEndDate  "
                    strSQL = strSQL + "                     )a  group by 行政区域,C_HOUSE "
                    strSQL = strSQL + "         )a on b.楼盘名称=a.楼盘名称 "
                    strSQL = strSQL + "     ) A left join (select * from T_HOUSE_MATCH_SORT where i_type=convert(integer,'" + strType + "')) b on b.c_NAME=a.楼盘名称 "
                    If strWhere <> "" Then
                        strSQL = strSQL + "where " + strWhere
                    End If
                Case "1"
                    strSQL = strSQL + "  select a.*,isnull(b.i_sort,100000) as '楼盘排序' from "
                    strSQL = strSQL + "  (	"
                    strSQL = strSQL + "     select b.行政区域,b.楼盘名称,b.项目类型, "
                    strSQL = strSQL + "          a.累计已售套数 as 合共成交,   "
                    strSQL = strSQL + "          a.累计已售面积,  "
                    strSQL = strSQL + "          a.未售套数,   "
                    strSQL = strSQL + "          a.未售面积,   "
                    strSQL = strSQL + "          退房数,  "
                    strSQL = strSQL + "          网签数,  "
                    strSQL = strSQL + "          网签面积,  "
                    strSQL = strSQL + "          网签总额,  "
                    strSQL = strSQL + "          签约均价=case when b.网签面积=0 or b.网签数=0 then dbo.Sunshine_F_getDayBusinessPrice(b.楼盘名称,@frontEndDate) "
                    strSQL = strSQL + "                         else  cast(round((b.网签总额)/(b.网签面积),2) as numeric(16,2))  end, "
                    strSQL = strSQL + "          累计均价=case when a.累计已售面积=0 or a.累计已售套数=0 then 0 else  cast(round((a.累计签约总额)/(a.累计已售面积),2) as numeric(16,2))  end "
                    strSQL = strSQL + "           from  "
                    strSQL = strSQL + "         ( "
                    strSQL = strSQL + "         select  行政区域,C_HOUSE as 楼盘名称, '商业' as 项目类型, "
                    strSQL = strSQL + "                   sum(当日退房套数) as 退房数, "
                    strSQL = strSQL + "                   sum(当日签约套数) as 网签数, "
                    strSQL = strSQL + "                   sum(当日签约面积) as 网签面积, "
                    strSQL = strSQL + "                   sum(当日签约总额) as 网签总额 from  "
                    strSQL = strSQL + "                     ( "
                    strSQL = strSQL + "                     select a.*,c.c_house from T_DAY_Business_INFO a left join T_HOUSE_MATCH_XMID c on a.项目名称=c.C_XM_NAME and a.预售证=c.C_XM_ID "
                    strSQL = strSQL + "                       where a.日期  between @frontStartDate and @frontEndDate  "
                    strSQL = strSQL + "                     )a  group by 行政区域,C_HOUSE  "
                    strSQL = strSQL + "          )b "
                    strSQL = strSQL + "          left  Join "
                    strSQL = strSQL + "         ("
                    strSQL = strSQL + "             select  行政区域,a.C_HOUSE as 楼盘名称, '商业' as 项目类型, "
                    strSQL = strSQL + "                   sum(累计已售套数) as 累计已售套数,    "
                    strSQL = strSQL + "                   sum(累计已售面积) as 累计已售面积,  "
                    strSQL = strSQL + "                   sum(未售套数) as 未售套数,    "
                    strSQL = strSQL + "                   sum(未售面积) as 未售面积, "
                    strSQL = strSQL + "                   sum(累计已售面积*累计已售均价) as 累计签约总额 from "
                    strSQL = strSQL + "                     ( "
                    strSQL = strSQL + "                     select a.*,c.c_house from T_DAY_Business_INFO a left join T_HOUSE_MATCH_XMID c on a.项目名称=c.C_XM_NAME and a.预售证=c.C_XM_ID  "
                    strSQL = strSQL + "                      where a.日期=@frontEndDate  "
                    strSQL = strSQL + "                     )a  group by 行政区域,C_HOUSE  "
                    strSQL = strSQL + "         )a on b.楼盘名称=a.楼盘名称 "
                    strSQL = strSQL + "     ) A left join (select * from T_HOUSE_MATCH_SORT where i_type=convert(integer,'" + strType + "')) b on b.c_NAME=a.楼盘名称 "
                    If strWhere <> "" Then
                        strSQL = strSQL + "where " + strWhere
                    End If
                Case Else

            End Select

            getSql_FrontBuildingCompute_x3_v2 = True
errProc:

            Exit Function
        End Function



        '----------------------------------------------------------------
        ' 根据“查询条件”获取“阳光家缘楼盘信息数据分析”完全数据的数据集
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strUserId                  ：用户标识
        '     strPassword                ：用户密码
        '     strWhere                   ：搜索字符串 
        '     objParameter               : 查询条件参数
        '     objSunshine                ：信息数据集
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_WEEKBuildingCompute_v2( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByVal objParameter As System.Collections.Specialized.NameValueCollection, _
            ByRef objSunshine As Xydc.Platform.Common.Data.SunshineData) As Boolean

            Dim objTempDeepdata As Xydc.Platform.Common.Data.SunshineData = Nothing
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Nothing
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand = Nothing
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim strSQL As String = ""
            Dim strSQL_0 As String = ""
            Dim strSQL_Total As String = ""

            '初始化
            getDataSet_WEEKBuildingCompute_v2 = False
            objSunshine = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strWhere.Length > 0 Then strWhere = strWhere.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：[阳光家缘楼盘信息数据分析]未指定[连接用户]！"
                    GoTo errProc
                End If

                Dim datStartDate As DateTime
                'Dim datMidDate As DateTime
                Dim datEndDate As DateTime

                Dim frontdatStartDate As DateTime
                'Dim frontdatMidDate As DateTime
                Dim frontdatEndDate As DateTime
                'Dim ddtt As DateTime

                Dim strType As String
                Dim strRegion As String

                datStartDate = CType(objParameter("StartDate"), System.DateTime)
                datEndDate = CType(objParameter("EndDate"), System.DateTime)
                'datMidDate = datEndDate.AddDays(-1)


                frontdatStartDate = datStartDate.AddDays(-7)
                '2013-07-19修改,结束日期大于当日
                frontdatEndDate = datStartDate.AddDays(-1)
                'ddtt = datEndDate.AddDays(-7)
                'frontdatEndDate = datEndDate.AddDays(-7)
                'frontdatMidDate = datMidDate.AddDays(-7)

                strType = objParameter("type")

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If



                '获取数据
                Try
                    '创建数据集
                    objTempDeepdata = New Xydc.Platform.Common.Data.SunshineData(Xydc.Platform.Common.Data.SunshineData.enumTableType.Sunshine_V_WeekInfo_Statistics)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter

                        '获取查询语句
                        If getSql_BuildingCompute_x3_v2(strErrMsg, strSQL, strWhere, strType) = False Then
                            GoTo errProc
                        End If

                        If getSql_FrontBuildingCompute_x3_v2(strErrMsg, strSQL_0, strWhere, strType) = False Then
                            GoTo errProc
                        End If

                        strSQL_Total = " "
                        strSQL_Total = strSQL_Total + " select * from ( "
                        strSQL_Total = strSQL_Total + " select a.*,b.网签数 as '上周网签数',网签均价=case when a.签约均价>0 then a.签约均价 "
                        strSQL_Total = strSQL_Total + " when a.签约均价=0 and b.签约均价>0 then b.签约均价 else a.累计均价 end  from ("
                        strSQL_Total = strSQL_Total + strSQL
                        strSQL_Total = strSQL_Total + " )a"
                        strSQL_Total = strSQL_Total + " left join "
                        strSQL_Total = strSQL_Total + " ("
                        strSQL_Total = strSQL_Total + strSQL_0
                        strSQL_Total = strSQL_Total + " )b on a.行政区域=b.行政区域 and a.楼盘名称=b.楼盘名称 and a.项目类型=b.项目类型"
                        strSQL_Total = strSQL_Total + " )A"
                        strSQL_Total = strSQL_Total + " order by A.楼盘排序,A.行政区域, A.楼盘名称, A.项目类型 "

                        objSqlCommand.CommandText = strSQL_Total
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@startDate", datStartDate)
                        objSqlCommand.Parameters.AddWithValue("@endDate", datEndDate)
                        'objSqlCommand.Parameters.AddWithValue("@midDate", datMidDate)
                        'objSqlCommand.Parameters.AddWithValue("@midEndDate", datEndDate)
                        objSqlCommand.Parameters.AddWithValue("@frontStartDate", frontdatStartDate)
                        objSqlCommand.Parameters.AddWithValue("@frontEndDate", frontdatEndDate)
                        'objSqlCommand.Parameters.AddWithValue("@frontMidDate", frontdatMidDate)
                        'objSqlCommand.Parameters.AddWithValue("@frontMidEndDate", frontdatEndDate)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempDeepdata.Tables(Xydc.Platform.Common.Data.SunshineData.TABLE_Sunshine_V_WeekInfo_Statistics))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDeepdata.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempDeepdata)
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objSunshine = objTempDeepdata
            getDataSet_WEEKBuildingCompute_v2 = True
            Exit Function
errProc:
            Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempDeepdata)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function
        End Function




        '----------------------------------------------------------------
        ' 根据“查询条件”获取“阳光家缘楼盘信息数据分析”完全数据的数据集
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strUserId                  ：用户标识
        '     strPassword                ：用户密码
        '     strWhere                   ：搜索字符串 
        '     objParameter               : 查询条件参数
        '     objSunshine                ：信息数据集
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_BuildingCompute_v2( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByVal objParameter As System.Collections.Specialized.NameValueCollection, _
            ByRef objSunshine As Xydc.Platform.Common.Data.SunshineData) As Boolean

            Dim objTempDeepdata As Xydc.Platform.Common.Data.SunshineData = Nothing
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Nothing
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand = Nothing
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim strSQL As String = ""
            Dim strSQL_0 As String = ""
            Dim strSQL_Total As String = ""

            '初始化
            getDataSet_BuildingCompute_v2 = False
            objSunshine = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strWhere.Length > 0 Then strWhere = strWhere.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：[阳光家缘楼盘信息数据分析]未指定[连接用户]！"
                    GoTo errProc
                End If

                Dim datStartDate As DateTime
                Dim datEndDate As DateTime
                Dim strType As String
                Dim strRegion As String

                datStartDate = CType(objParameter("StartDate"), System.DateTime)
                datEndDate = CType(objParameter("EndDate"), System.DateTime)
                strType = objParameter("type")

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If



                '获取数据
                Try
                    '创建数据集
                    objTempDeepdata = New Xydc.Platform.Common.Data.SunshineData(Xydc.Platform.Common.Data.SunshineData.enumTableType.Sunshine_V_Houseinfo_Statistics)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter

                        '获取查询语句      
                        If getSql_BuildingCompute_x3_v2(strErrMsg, strSQL, strWhere, strType) = False Then
                            GoTo errProc
                        End If

                        strSQL_0 = ""
                        strSQL_0 = strSQL_0 + " select * from "
                        strSQL_0 = strSQL_0 + " ("
                        strSQL_0 = strSQL_0 + " select 行政区域,楼盘名称,项目类型,网签数,签约均价 as 网签均价,累计均价 as '合共均价',合共成交,退房数,未售套数,网签总额,网签面积,楼盘排序,类型=1,排序=1 from ( "
                        strSQL_0 = strSQL_0 + strSQL
                        strSQL_0 = strSQL_0 + " )A "
                        strSQL_0 = strSQL_0 + " union"
                        strSQL_0 = strSQL_0 + " select 行政区域,楼盘名称='汇总',项目类型='',网签数=sum(case when 网签数<0 then 0 else  网签数 end),网签均价=case when sum(case when 网签面积<0 then 0 else  网签面积 end)<1 then 0 else cast(round(sum(case when 网签总额<0 then 0 else  网签总额 end)/sum(case when 网签面积<0 then 0 else  网签面积 end),2) as numeric(16,2)) end,合共均价=0,合共成交=sum(case when 合共成交<0 then 0 else  合共成交 end),"
                        strSQL_0 = strSQL_0 + " sum(退房数),未售套数=sum(case when 未售套数<0 then 0 else  未售套数 end),网签总额=sum(case when 网签总额<0 then 0 else  网签总额 end),网签面积=sum(case when 网签面积<0 then 0 else  网签面积 end),楼盘排序='',类型=2,排序=1  from ( "
                        strSQL_0 = strSQL_0 + strSQL
                        strSQL_0 = strSQL_0 + " )A group by 行政区域"
                        strSQL_0 = strSQL_0 + " union"
                        'strSQL_0 = strSQL_0 + " select 行政区域,楼盘名称='退房汇总',项目类型='',网签数=sum(case when 网签数<0 then 网签数 else  0 end),网签均价=0 ,合共均价=0, 合共成交=0,"
                        'strSQL_0 = strSQL_0 + " 未售套数=0,网签总额=0,网签面积=0,楼盘排序='',类型=3,排序=1  from ( "
                        'strSQL_0 = strSQL_0 + strSQL
                        'strSQL_0 = strSQL_0 + " )A group by 行政区域"
                        'strSQL_0 = strSQL_0 + " union"
                        strSQL_0 = strSQL_0 + " select 行政区域='合计',楼盘名称='',项目类型='',网签数=sum(case when 网签数<0 then 0 else  网签数 end),网签均价=case when sum(case when 网签面积<0 then 0 else  网签面积 end)<1 then 0 else cast(round(sum(case when 网签总额<0 then 0 else  网签总额 end)/sum(case when 网签面积<0 then 0 else  网签面积 end),2) as numeric(16,2)) end,合共均价=0,合共成交=sum(case when 合共成交<0 then 0 else  合共成交 end), "
                        strSQL_0 = strSQL_0 + " sum(退房数),未售套数=sum(case when 未售套数<0 then 0 else  未售套数 end),网签总额=sum(case when 网签总额<0 then 0 else  网签总额 end),网签面积=sum(case when 网签面积<0 then 0 else  网签面积 end),楼盘排序='',类型=3,排序=2  from ( "
                        strSQL_0 = strSQL_0 + strSQL
                        strSQL_0 = strSQL_0 + " )A "
                        'strSQL_0 = strSQL_0 + " union"
                        'strSQL_0 = strSQL_0 + " select 行政区域='退房合计',楼盘名称='',项目类型='',网签数=sum(case when 网签数<0 then 网签数 else  0 end),网签均价=0 ,合共均价=0,合共成交=0,"
                        'strSQL_0 = strSQL_0 + " 未售套数=0,网签总额=0,网签面积=0,楼盘排序='',类型=4,排序=2  from ( "
                        'strSQL_0 = strSQL_0 + strSQL
                        'strSQL_0 = strSQL_0 + " )A "
                        strSQL_0 = strSQL_0 + " )A "
                        strSQL_0 = strSQL_0 + " order by A.排序,A.行政区域,A.类型,A.楼盘排序,A.楼盘名称,A.项目类型"

                        objSqlCommand.CommandText = strSQL_0
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@startDate", datStartDate)
                        objSqlCommand.Parameters.AddWithValue("@endDate", datEndDate)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempDeepdata.Tables(Xydc.Platform.Common.Data.SunshineData.TABLE_Sunshine_V_Houseinfo_Statistics))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDeepdata.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempDeepdata)
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objSunshine = objTempDeepdata
            getDataSet_BuildingCompute_v2 = True
            Exit Function
errProc:
            Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempDeepdata)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function
        End Function


        '----------------------------------------------------------------
        ' 根据“查询条件”获取“阳光家缘楼盘信息周区域数据分析”完全数据的数据集
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strUserId                  ：用户标识
        '     strPassword                ：用户密码
        '     strWhere                   ：搜索字符串 
        '     objParameter               : 查询条件参数
        '     objSunshine                ：信息数据集
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_RegionBuildingCompute_v2( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByVal objParameter As System.Collections.Specialized.NameValueCollection, _
            ByRef objSunshine As Xydc.Platform.Common.Data.SunshineData) As Boolean

            Dim objTempDeepdata As Xydc.Platform.Common.Data.SunshineData = Nothing
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Nothing
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand = Nothing
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim strSQL As String = ""
            Dim strSQL_0 As String = ""
            Dim strSQL_Total As String = ""

            '初始化
            getDataSet_RegionBuildingCompute_v2 = False
            objSunshine = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strWhere.Length > 0 Then strWhere = strWhere.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：[阳光家缘楼盘信息数据分析]未指定[连接用户]！"
                    GoTo errProc
                End If

                Dim datStartDate As DateTime
                Dim datEndDate As DateTime

                Dim frontdatStartDate As DateTime
                Dim frontdatEndDate As DateTime
                'Dim ddtt As DateTime

                Dim strType As String
                Dim strRegion As String

                datStartDate = CType(objParameter("StartDate"), System.DateTime)
                datEndDate = CType(objParameter("EndDate"), System.DateTime)

                frontdatStartDate = datStartDate.AddDays(-7)
                '2013-07-19修改,结束日期大于当日
                frontdatEndDate = datStartDate.AddDays(-1)
                'ddtt = datEndDate.AddDays(-7)
                'frontdatEndDate = datEndDate.AddDays(-7)

                strType = objParameter("type")

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If



                '获取数据
                Try
                    '创建数据集
                    objTempDeepdata = New Xydc.Platform.Common.Data.SunshineData(Xydc.Platform.Common.Data.SunshineData.enumTableType.Sunshine_V_WeekRegion_Statistics)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter

                        '获取查询语句
                        If getSql_BuildingCompute_x3_v2(strErrMsg, strSQL, strWhere, strType) = False Then
                            GoTo errProc
                        End If

                        '获取查询语句
                        If getSql_FrontBuildingCompute_x3_v2(strErrMsg, strSQL_0, strWhere, strType) = False Then
                            GoTo errProc
                        End If

                        strSQL_Total = " "
                        strSQL_Total = strSQL_Total + " select * from ( "
                        '各区域
                        strSQL_Total = strSQL_Total + " select a.行政区域,a.网签数,b.网签数 as '上周网签数',cast(a.网签均价 as numeric(16,2)) as 网签均价,cast(b.网签均价 as numeric(16,2)) as  '上周网签均价',a.网签总额,a.网签面积,网签数环比=case when b.网签数<1 then 0 else cast((a.网签数*1.0-b.网签数*1.0)/b.网签数*100.0 as numeric(16,2)) end,网签均价环比=case when b.网签均价<1 then 0 else cast((a.网签均价-b.网签均价)/b.网签均价*100 as numeric(16,2)) end,序号=1  from ("
                        strSQL_Total = strSQL_Total + " select 行政区域,网签数=sum(case when 网签数<0 then 0 else  网签数 end),网签均价=case when sum(case when 网签面积<0 then 0 else  网签面积 end)<1 then 0 else sum(case when 网签总额<0 then 0 else  网签总额 end)/sum(case when 网签面积<0 then 0 else  网签面积 end) end, "
                        strSQL_Total = strSQL_Total + " 网签总额=sum(case when 网签总额<0 then 0 else  网签总额 end),网签面积=sum(case when 网签面积<0 then 0 else  网签面积 end) from ( "
                        strSQL_Total = strSQL_Total + strSQL
                        strSQL_Total = strSQL_Total + " )a group by 行政区域"
                        strSQL_Total = strSQL_Total + " )a"
                        strSQL_Total = strSQL_Total + " left join "
                        strSQL_Total = strSQL_Total + " ("
                        strSQL_Total = strSQL_Total + " select 行政区域,网签数=sum(case when 网签数<0 then 0 else  网签数 end),网签均价=case when sum(case when 网签面积<0 then 0 else  网签面积 end)<1 then 0 else sum(case when 网签总额<0 then 0 else  网签总额 end)/sum(case when 网签面积<0 then 0 else  网签面积 end) end, "
                        strSQL_Total = strSQL_Total + " 网签总额=sum(case when 网签总额<0 then 0 else  网签总额 end),网签面积=sum(case when 网签面积<0 then 0 else  网签面积 end) from ( "
                        strSQL_Total = strSQL_Total + strSQL_0
                        strSQL_Total = strSQL_Total + " )a group by 行政区域"
                        strSQL_Total = strSQL_Total + " )b on a.行政区域=b.行政区域"
                        '六区
                        strSQL_Total = strSQL_Total + " union"
                        strSQL_Total = strSQL_Total + " select a.行政区域,a.网签数,b.网签数 as '上周网签数',cast(a.网签均价 as numeric(16,2)) as 网签均价,cast(b.网签均价 as numeric(16,2)) as  '上周网签均价',a.网签总额,a.网签面积,网签数环比=case when b.网签数<1 then 0 else cast((a.网签数*1.0-b.网签数*1.0)/b.网签数*100.0 as numeric(16,2)) end,网签均价环比=case when b.网签均价<1 then 0 else cast((a.网签均价-b.网签均价)/b.网签均价*100 as numeric(16,2)) end,序号=2  from ("
                        strSQL_Total = strSQL_Total + " select '六区' as '行政区域',网签数=sum(case when 网签数<0 then 0 else  网签数 end),网签均价=case when sum(case when 网签面积<0 then 0 else  网签面积 end)<1 then 0 else sum(case when 网签总额<0 then 0 else  网签总额 end)/sum(case when 网签面积<0 then 0 else  网签面积 end) end, "
                        strSQL_Total = strSQL_Total + " 网签总额=sum(case when 网签总额<0 then 0 else  网签总额 end),网签面积=sum(case when 网签面积<0 then 0 else  网签面积 end) from ( "
                        strSQL_Total = strSQL_Total + strSQL
                        strSQL_Total = strSQL_Total + " )a  where 行政区域 in ('白云区','天河区','越秀区','荔湾区','海珠区','黄埔区')"
                        strSQL_Total = strSQL_Total + " )a"
                        strSQL_Total = strSQL_Total + " left join "
                        strSQL_Total = strSQL_Total + " ("
                        strSQL_Total = strSQL_Total + " select '六区' as '行政区域',网签数=sum(case when 网签数<0 then 0 else  网签数 end),网签均价=case when sum(case when 网签面积<0 then 0 else  网签面积 end)<1 then 0 else sum(case when 网签总额<0 then 0 else  网签总额 end)/sum(case when 网签面积<0 then 0 else  网签面积 end) end, "
                        strSQL_Total = strSQL_Total + " 网签总额=sum(case when 网签总额<0 then 0 else  网签总额 end),网签面积=sum(case when 网签面积<0 then 0 else  网签面积 end) from ( "
                        strSQL_Total = strSQL_Total + strSQL_0
                        strSQL_Total = strSQL_Total + " )a  where 行政区域 in ('白云区','天河区','越秀区','荔湾区','海珠区','黄埔区')"
                        strSQL_Total = strSQL_Total + " )b on a.行政区域=b.行政区域"

                        '十区
                        strSQL_Total = strSQL_Total + " union"
                        strSQL_Total = strSQL_Total + " select a.行政区域,a.网签数,b.网签数 as '上周网签数',cast(a.网签均价 as numeric(16,2)) as 网签均价,cast(b.网签均价 as numeric(16,2)) as  '上周网签均价',a.网签总额,a.网签面积,网签数环比=case when b.网签数<1 then 0 else cast((a.网签数*1.0-b.网签数*1.0)/b.网签数*100.0 as numeric(16,2)) end,网签均价环比=case when b.网签均价<1 then 0 else cast((a.网签均价-b.网签均价)/b.网签均价*100 as numeric(16,2)) end,序号=3  from ("
                        strSQL_Total = strSQL_Total + " select '十区' as '行政区域',网签数=sum(case when 网签数<0 then 0 else  网签数 end),网签均价=case when sum(case when 网签面积<0 then 0 else  网签面积 end)<1 then 0 else sum(case when 网签总额<0 then 0 else  网签总额 end)/sum(case when 网签面积<0 then 0 else  网签面积 end) end, "
                        strSQL_Total = strSQL_Total + " 网签总额=sum(case when 网签总额<0 then 0 else  网签总额 end),网签面积=sum(case when 网签面积<0 then 0 else  网签面积 end) from ( "
                        strSQL_Total = strSQL_Total + strSQL
                        strSQL_Total = strSQL_Total + " )a  where 行政区域 not in ('增城市','从化市')"
                        strSQL_Total = strSQL_Total + " )a"
                        strSQL_Total = strSQL_Total + " left join "
                        strSQL_Total = strSQL_Total + " ("
                        strSQL_Total = strSQL_Total + " select  '十区' as '行政区域',网签数=sum(case when 网签数<0 then 0 else  网签数 end),网签均价=case when sum(case when 网签面积<0 then 0 else  网签面积 end)<1 then 0 else sum(case when 网签总额<0 then 0 else  网签总额 end)/sum(case when 网签面积<0 then 0 else  网签面积 end) end, "
                        strSQL_Total = strSQL_Total + " 网签总额=sum(case when 网签总额<0 then 0 else  网签总额 end),网签面积=sum(case when 网签面积<0 then 0 else  网签面积 end) from ( "
                        strSQL_Total = strSQL_Total + strSQL_0
                        strSQL_Total = strSQL_Total + " )a  where 行政区域 not in ('增城市','从化市')"
                        strSQL_Total = strSQL_Total + " )b on a.行政区域=b.行政区域"

                        '十区两市
                        strSQL_Total = strSQL_Total + " union"
                        strSQL_Total = strSQL_Total + " select a.行政区域,a.网签数,b.网签数 as '上周网签数',cast(a.网签均价 as numeric(16,2)) as 网签均价,cast(b.网签均价 as numeric(16,2)) as  '上周网签均价',a.网签总额,a.网签面积,网签数环比=case when b.网签数<1 then 0 else cast((a.网签数*1.0-b.网签数*1.0)/b.网签数*100.0 as numeric(16,2)) end,网签均价环比=case when b.网签均价<1 then 0 else cast((a.网签均价-b.网签均价)/b.网签均价*100 as numeric(16,2)) end,序号=4  from ("
                        strSQL_Total = strSQL_Total + " select '十区两市' as '行政区域',网签数=sum(case when 网签数<0 then 0 else  网签数 end),网签均价=case when sum(case when 网签面积<0 then 0 else  网签面积 end)<1 then 0 else sum(case when 网签总额<0 then 0 else  网签总额 end)/sum(case when 网签面积<0 then 0 else  网签面积 end) end, "
                        strSQL_Total = strSQL_Total + " 网签总额=sum(case when 网签总额<0 then 0 else  网签总额 end),网签面积=sum(case when 网签面积<0 then 0 else  网签面积 end) from ( "
                        strSQL_Total = strSQL_Total + strSQL
                        strSQL_Total = strSQL_Total + " )a  "
                        strSQL_Total = strSQL_Total + " )a"
                        strSQL_Total = strSQL_Total + " left join "
                        strSQL_Total = strSQL_Total + " ("
                        strSQL_Total = strSQL_Total + " select  '十区两市' as '行政区域',网签数=sum(case when 网签数<0 then 0 else  网签数 end),网签均价=case when sum(case when 网签面积<0 then 0 else  网签面积 end)<1 then 0 else sum(case when 网签总额<0 then 0 else  网签总额 end)/sum(case when 网签面积<0 then 0 else  网签面积 end) end, "
                        strSQL_Total = strSQL_Total + " 网签总额=sum(case when 网签总额<0 then 0 else  网签总额 end),网签面积=sum(case when 网签面积<0 then 0 else  网签面积 end) from ( "
                        strSQL_Total = strSQL_Total + strSQL_0
                        strSQL_Total = strSQL_Total + " )a "
                        strSQL_Total = strSQL_Total + " )b on a.行政区域=b.行政区域"

                        strSQL_Total = strSQL_Total + " )A"
                        strSQL_Total = strSQL_Total + " order by 序号,A.行政区域 "
                        objSqlCommand.CommandText = strSQL_Total
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@startDate", datStartDate)
                        objSqlCommand.Parameters.AddWithValue("@endDate", datEndDate)
                        objSqlCommand.Parameters.AddWithValue("@frontstartDate", frontdatStartDate)
                        objSqlCommand.Parameters.AddWithValue("@frontendDate", frontdatEndDate)

                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempDeepdata.Tables(Xydc.Platform.Common.Data.SunshineData.TABLE_Sunshine_V_WeekRegion_Statistics))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDeepdata.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempDeepdata)
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objSunshine = objTempDeepdata
            getDataSet_RegionBuildingCompute_v2 = True
            Exit Function
errProc:
            Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempDeepdata)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function
        End Function


        '----------------------------------------------------------------
        ' 根据“查询条件”获取“N个星期的价格和套数”
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strUserId                  ：用户标识
        '     strPassword                ：用户密码
        '     strWhere                   ：搜索字符串 
        '     objParameter               : 查询条件参数
        '     objSunshine                ：信息数据集
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_NWeek_Compute_v2( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByVal objParameter As System.Collections.Specialized.NameValueCollection, _
            ByRef objSunshine As Xydc.Platform.Common.Data.SunshineData) As Boolean

            Dim objTempDeepdata As Xydc.Platform.Common.Data.SunshineData = Nothing
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Nothing
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand = Nothing
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim strSQL As String = ""
            Dim strSQL_0 As String = ""
            Dim strSQL_Total As String = ""

            '初始化
            getDataSet_NWeek_Compute_v2 = False
            objSunshine = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strWhere.Length > 0 Then strWhere = strWhere.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：[阳光家缘楼盘信息数据分析]未指定[连接用户]！"
                    GoTo errProc
                End If

                Dim datStartDate_Start As DateTime
                Dim datStartDate_End As DateTime
                Dim datEndDate_Start As DateTime
                Dim datEndDate_End As DateTime
                Dim strType As String
                Dim strRegion As String

                datStartDate_Start = CType(objParameter("StartDate"), System.DateTime)
                datEndDate_Start = CType(objParameter("EndDate"), System.DateTime)
                strType = objParameter("type")

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                Try
                    '创建数据集
                    objTempDeepdata = New Xydc.Platform.Common.Data.SunshineData(Xydc.Platform.Common.Data.SunshineData.enumTableType.Sunshine_V_NWeek_Statistics)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter

                        strSQL = ""
                        Select Case strType
                            Case "1"
                                strSQL = strSQL + " exec dbo.Sunshine_P_getWeekStatistics_business_V2 @FirstStartDate,@FinalStartDate "

                            Case "2"
                                strSQL = strSQL + " exec dbo.Sunshine_P_getWeekStatistics_V2 @FirstStartDate,@FinalStartDate "

                            Case "3"
                                strSQL = strSQL + " exec dbo.Sunshine_P_getWeekStatistics_office_V2 @FirstStartDate,@FinalStartDate "

                        End Select

                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@FirstStartDate", datStartDate_Start)
                        objSqlCommand.Parameters.AddWithValue("@FinalStartDate", datEndDate_Start)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempDeepdata.Tables(Xydc.Platform.Common.Data.SunshineData.TABLE_Sunshine_V_NWeek_Statistics))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDeepdata.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempDeepdata)
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objSunshine = objTempDeepdata
            getDataSet_NWeek_Compute_v2 = True
            Exit Function
errProc:
            Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempDeepdata)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function
        End Function



        '第三版，日楼盘数据
        '----------------------------------------------------------------
        ' 根据“查询条件”获取“阳光家缘楼盘信息数据分析”完全数据的数据集
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strUserId                  ：用户标识
        '     strPassword                ：用户密码
        '     strWhere                   ：搜索字符串 
        '     objParameter               : 查询条件参数
        '     objSunshine                ：信息数据集
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_WEEKBuildingCompute_v3( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByVal objParameter As System.Collections.Specialized.NameValueCollection, _
            ByRef objSunshine As Xydc.Platform.Common.Data.SunshineData) As Boolean

            Dim objTempDeepdata As Xydc.Platform.Common.Data.SunshineData = Nothing
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Nothing
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand = Nothing
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim strSQL As String = ""
            Dim strSQL_0 As String = ""
            Dim strSQL_Total As String = ""

            '初始化
            getDataSet_WEEKBuildingCompute_v3 = False
            objSunshine = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strWhere.Length > 0 Then strWhere = strWhere.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：[阳光家缘楼盘信息数据分析]未指定[连接用户]！"
                    GoTo errProc
                End If

                Dim datStartDate As DateTime
                'Dim datMidDate As DateTime
                Dim datEndDate As DateTime

                Dim frontdatStartDate As DateTime
                'Dim frontdatMidDate As DateTime
                Dim frontdatEndDate As DateTime
                'Dim ddtt As DateTime

                Dim strType As String
                Dim strRegion As String

                datStartDate = CType(objParameter("StartDate"), System.DateTime)
                datEndDate = CType(objParameter("EndDate"), System.DateTime)
                'datMidDate = datEndDate.AddDays(-1)


                frontdatStartDate = datStartDate.AddDays(-7)
                '2013-07-19修改,结束日期大于当日
                frontdatEndDate = datStartDate.AddDays(-1)
                'ddtt = datEndDate.AddDays(-7)
                'frontdatEndDate = datEndDate.AddDays(-7)
                'frontdatMidDate = datMidDate.AddDays(-7)

                strType = objParameter("type")

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If



                '获取数据
                Try
                    '创建数据集
                    objTempDeepdata = New Xydc.Platform.Common.Data.SunshineData(Xydc.Platform.Common.Data.SunshineData.enumTableType.Sunshine_V_WeekInfo_Statistics)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter

                        '获取查询语句
                        If getSql_BuildingCompute_x3_v3(strErrMsg, strSQL, strWhere, strType) = False Then
                            GoTo errProc
                        End If

                        If getSql_FrontBuildingCompute_x3_v3(strErrMsg, strSQL_0, strWhere, strType) = False Then
                            GoTo errProc
                        End If

                        If strType = "2" Then
                            strSQL_Total = " "
                            strSQL_Total = strSQL_Total + " select * from ( "
                            strSQL_Total = strSQL_Total + " select a.*,b.网签数 as '上周网签数',网签均价=case when a.签约均价>0 then a.签约均价 "
                            strSQL_Total = strSQL_Total + " when a.签约均价=0 and b.签约均价>0 then b.签约均价 else a.累计均价 end  from ("
                            strSQL_Total = strSQL_Total + strSQL
                            strSQL_Total = strSQL_Total + " )a"
                            strSQL_Total = strSQL_Total + " left join "
                            strSQL_Total = strSQL_Total + " ("
                            strSQL_Total = strSQL_Total + strSQL_0
                            strSQL_Total = strSQL_Total + " )b on a.行政区域=b.行政区域 and a.楼盘名称=b.楼盘名称 and a.项目类型=b.项目类型"
                            strSQL_Total = strSQL_Total + " )A"
                            strSQL_Total = strSQL_Total + " order by A.楼盘排序,A.行政区域, A.楼盘名称, A.项目类型 "
                        Else
                            strSQL_Total = " "
                            strSQL_Total = strSQL_Total + " select * from ( "
                            strSQL_Total = strSQL_Total + " select a.*,b.网签数 as '上周网签数',网签均价=case when a.签约均价>0 then a.签约均价 "
                            strSQL_Total = strSQL_Total + " when a.签约均价=0 and b.签约均价>0 then b.签约均价 else a.累计均价 end  from ("
                            strSQL_Total = strSQL_Total + strSQL
                            strSQL_Total = strSQL_Total + " )a"
                            strSQL_Total = strSQL_Total + " left join "
                            strSQL_Total = strSQL_Total + " ("
                            strSQL_Total = strSQL_Total + strSQL_0
                            strSQL_Total = strSQL_Total + " )b on a.行政区域=b.行政区域 and a.楼盘名称=b.楼盘名称 "
                            strSQL_Total = strSQL_Total + " )A"
                            strSQL_Total = strSQL_Total + " order by A.楼盘排序,A.行政区域, A.楼盘名称, A.项目类型 "

                        End If

                        'strSQL_Total = " "
                        'strSQL_Total = strSQL_Total + " select * from ( "
                        'strSQL_Total = strSQL_Total + " select a.*,b.网签数 as '上周网签数',网签均价=case when a.签约均价>0 then a.签约均价 "
                        'strSQL_Total = strSQL_Total + " when a.签约均价=0 and b.签约均价>0 then b.签约均价 else a.累计均价 end  from ("
                        'strSQL_Total = strSQL_Total + strSQL
                        'strSQL_Total = strSQL_Total + " )a"
                        'strSQL_Total = strSQL_Total + " left join "
                        'strSQL_Total = strSQL_Total + " ("
                        'strSQL_Total = strSQL_Total + strSQL_0
                        'strSQL_Total = strSQL_Total + " )b on a.行政区域=b.行政区域 and a.楼盘名称=b.楼盘名称 and a.项目类型=b.项目类型"
                        'strSQL_Total = strSQL_Total + " )A"
                        'strSQL_Total = strSQL_Total + " order by A.楼盘排序,A.行政区域, A.楼盘名称, A.项目类型 "

                        objSqlCommand.CommandText = strSQL_Total
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@startDate", datStartDate)
                        objSqlCommand.Parameters.AddWithValue("@endDate", datEndDate)
                        'objSqlCommand.Parameters.AddWithValue("@midDate", datMidDate)
                        'objSqlCommand.Parameters.AddWithValue("@midEndDate", datEndDate)
                        objSqlCommand.Parameters.AddWithValue("@frontStartDate", frontdatStartDate)
                        objSqlCommand.Parameters.AddWithValue("@frontEndDate", frontdatEndDate)
                        'objSqlCommand.Parameters.AddWithValue("@frontMidDate", frontdatMidDate)
                        'objSqlCommand.Parameters.AddWithValue("@frontMidEndDate", frontdatEndDate)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempDeepdata.Tables(Xydc.Platform.Common.Data.SunshineData.TABLE_Sunshine_V_WeekInfo_Statistics))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDeepdata.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempDeepdata)
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objSunshine = objTempDeepdata
            getDataSet_WEEKBuildingCompute_v3 = True
            Exit Function
errProc:
            Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempDeepdata)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function
        End Function




        '----------------------------------------------------------------
        ' 根据“查询条件”获取“阳光家缘楼盘信息数据分析”完全数据的数据集
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strUserId                  ：用户标识
        '     strPassword                ：用户密码
        '     strWhere                   ：搜索字符串 
        '     objParameter               : 查询条件参数
        '     objSunshine                ：信息数据集
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_BuildingCompute_v3( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByVal objParameter As System.Collections.Specialized.NameValueCollection, _
            ByRef objSunshine As Xydc.Platform.Common.Data.SunshineData) As Boolean

            Dim objTempDeepdata As Xydc.Platform.Common.Data.SunshineData = Nothing
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Nothing
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand = Nothing
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim strSQL As String = ""
            Dim strSQL_0 As String = ""
            Dim strSQL_Total As String = ""

            '初始化
            getDataSet_BuildingCompute_v3 = False
            objSunshine = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strWhere.Length > 0 Then strWhere = strWhere.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：[阳光家缘楼盘信息数据分析]未指定[连接用户]！"
                    GoTo errProc
                End If

                Dim datStartDate As DateTime
                Dim datEndDate As DateTime
                Dim strType As String
                Dim strRegion As String

                datStartDate = CType(objParameter("StartDate"), System.DateTime)
                datEndDate = CType(objParameter("EndDate"), System.DateTime)
                strType = objParameter("type")

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If



                '获取数据
                Try
                    '创建数据集
                    objTempDeepdata = New Xydc.Platform.Common.Data.SunshineData(Xydc.Platform.Common.Data.SunshineData.enumTableType.Sunshine_V_Houseinfo_Statistics)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter

                        '获取查询语句      
                        If getSql_BuildingCompute_x3_v3(strErrMsg, strSQL, strWhere, strType) = False Then
                            GoTo errProc
                        End If

                        strSQL_0 = ""
                        strSQL_0 = strSQL_0 + " select * from "
                        strSQL_0 = strSQL_0 + " ("
                        strSQL_0 = strSQL_0 + " select 行政区域,楼盘名称,项目类型,网签数,签约均价 as 网签均价,累计均价 as '合共均价',合共成交,退房数,未售套数,网签总额,网签面积,楼盘排序,类型=1,排序=1 from ( "
                        strSQL_0 = strSQL_0 + strSQL
                        strSQL_0 = strSQL_0 + " )A "
                        strSQL_0 = strSQL_0 + " union"
                        strSQL_0 = strSQL_0 + " select 行政区域,楼盘名称='汇总',项目类型='',网签数=sum(case when 网签数<0 then 0 else  网签数 end),网签均价=case when sum(case when 网签面积<0 then 0 else  网签面积 end)<1 then 0 else cast(round(sum(case when 网签总额<0 then 0 else  网签总额 end)/sum(case when 网签面积<0 then 0 else  网签面积 end),2) as numeric(16,2)) end,合共均价=0,合共成交=sum(case when 合共成交<0 then 0 else  合共成交 end),"
                        strSQL_0 = strSQL_0 + " sum(退房数),未售套数=sum(case when 未售套数<0 then 0 else  未售套数 end),网签总额=sum(case when 网签总额<0 then 0 else  网签总额 end),网签面积=sum(case when 网签面积<0 then 0 else  网签面积 end),楼盘排序='',类型=2,排序=1  from ( "
                        strSQL_0 = strSQL_0 + strSQL
                        strSQL_0 = strSQL_0 + " )A group by 行政区域"
                        strSQL_0 = strSQL_0 + " union"
                        'strSQL_0 = strSQL_0 + " select 行政区域,楼盘名称='退房汇总',项目类型='',网签数=sum(case when 网签数<0 then 网签数 else  0 end),网签均价=0 ,合共均价=0, 合共成交=0,"
                        'strSQL_0 = strSQL_0 + " 未售套数=0,网签总额=0,网签面积=0,楼盘排序='',类型=3,排序=1  from ( "
                        'strSQL_0 = strSQL_0 + strSQL
                        'strSQL_0 = strSQL_0 + " )A group by 行政区域"
                        'strSQL_0 = strSQL_0 + " union"
                        strSQL_0 = strSQL_0 + " select 行政区域='合计',楼盘名称='',项目类型='',网签数=sum(case when 网签数<0 then 0 else  网签数 end),网签均价=case when sum(case when 网签面积<0 then 0 else  网签面积 end)<1 then 0 else cast(round(sum(case when 网签总额<0 then 0 else  网签总额 end)/sum(case when 网签面积<0 then 0 else  网签面积 end),2) as numeric(16,2)) end,合共均价=0,合共成交=sum(case when 合共成交<0 then 0 else  合共成交 end), "
                        strSQL_0 = strSQL_0 + " sum(退房数),未售套数=sum(case when 未售套数<0 then 0 else  未售套数 end),网签总额=sum(case when 网签总额<0 then 0 else  网签总额 end),网签面积=sum(case when 网签面积<0 then 0 else  网签面积 end),楼盘排序='',类型=3,排序=2  from ( "
                        strSQL_0 = strSQL_0 + strSQL
                        strSQL_0 = strSQL_0 + " )A "
                        'strSQL_0 = strSQL_0 + " union"
                        'strSQL_0 = strSQL_0 + " select 行政区域='退房合计',楼盘名称='',项目类型='',网签数=sum(case when 网签数<0 then 网签数 else  0 end),网签均价=0 ,合共均价=0,合共成交=0,"
                        'strSQL_0 = strSQL_0 + " 未售套数=0,网签总额=0,网签面积=0,楼盘排序='',类型=4,排序=2  from ( "
                        'strSQL_0 = strSQL_0 + strSQL
                        'strSQL_0 = strSQL_0 + " )A "
                        strSQL_0 = strSQL_0 + " )A "
                        strSQL_0 = strSQL_0 + " order by A.排序,A.行政区域,A.类型,A.楼盘排序,A.楼盘名称,A.项目类型"

                        objSqlCommand.CommandText = strSQL_0
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@startDate", datStartDate)
                        objSqlCommand.Parameters.AddWithValue("@endDate", datEndDate)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempDeepdata.Tables(Xydc.Platform.Common.Data.SunshineData.TABLE_Sunshine_V_Houseinfo_Statistics))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDeepdata.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempDeepdata)
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objSunshine = objTempDeepdata
            getDataSet_BuildingCompute_v3 = True
            Exit Function
errProc:
            Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempDeepdata)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function
        End Function


        '----------------------------------------------------------------
        ' 根据“查询条件”获取“阳光家缘楼盘信息周区域数据分析”完全数据的数据集
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strUserId                  ：用户标识
        '     strPassword                ：用户密码
        '     strWhere                   ：搜索字符串 
        '     objParameter               : 查询条件参数
        '     objSunshine                ：信息数据集
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_RegionBuildingCompute_v3( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByVal objParameter As System.Collections.Specialized.NameValueCollection, _
            ByRef objSunshine As Xydc.Platform.Common.Data.SunshineData) As Boolean

            Dim objTempDeepdata As Xydc.Platform.Common.Data.SunshineData = Nothing
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Nothing
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand = Nothing
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim strSQL As String = ""
            Dim strSQL_0 As String = ""
            Dim strSQL_Total As String = ""

            '初始化
            getDataSet_RegionBuildingCompute_v3 = False
            objSunshine = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strWhere.Length > 0 Then strWhere = strWhere.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：[阳光家缘楼盘信息数据分析]未指定[连接用户]！"
                    GoTo errProc
                End If

                Dim datStartDate As DateTime
                Dim datEndDate As DateTime

                Dim frontdatStartDate As DateTime
                Dim frontdatEndDate As DateTime
                'Dim ddtt As DateTime

                Dim strType As String
                Dim strRegion As String

                datStartDate = CType(objParameter("StartDate"), System.DateTime)
                datEndDate = CType(objParameter("EndDate"), System.DateTime)

                frontdatStartDate = datStartDate.AddDays(-7)
                '2013-07-19修改,结束日期大于当日
                frontdatEndDate = datStartDate.AddDays(-1)
                'ddtt = datEndDate.AddDays(-7)
                'frontdatEndDate = datEndDate.AddDays(-7)

                strType = objParameter("type")

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If



                '获取数据
                Try
                    '创建数据集
                    objTempDeepdata = New Xydc.Platform.Common.Data.SunshineData(Xydc.Platform.Common.Data.SunshineData.enumTableType.Sunshine_V_WeekRegion_Statistics)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter

                        '获取查询语句
                        If getSql_BuildingCompute_x3_v3(strErrMsg, strSQL, strWhere, strType) = False Then
                            GoTo errProc
                        End If

                        '获取查询语句
                        If getSql_FrontBuildingCompute_x3_v3(strErrMsg, strSQL_0, strWhere, strType) = False Then
                            GoTo errProc
                        End If

                        strSQL_Total = " "
                        strSQL_Total = strSQL_Total + " select * from ( "
                        '各区域
                        strSQL_Total = strSQL_Total + " select a.行政区域,a.网签数,b.网签数 as '上周网签数',cast(a.网签均价 as numeric(16,2)) as 网签均价,cast(b.网签均价 as numeric(16,2)) as  '上周网签均价',a.网签总额,a.网签面积,网签数环比=case when b.网签数<1 then 0 else cast((a.网签数*1.0-b.网签数*1.0)/b.网签数*100.0 as numeric(16,2)) end,网签均价环比=case when b.网签均价<1 then 0 else cast((a.网签均价-b.网签均价)/b.网签均价*100 as numeric(16,2)) end,序号=1  from ("
                        strSQL_Total = strSQL_Total + " select 行政区域,网签数=sum(case when 网签数<0 then 0 else  网签数 end),网签均价=case when sum(case when 网签面积<0 then 0 else  网签面积 end)<1 then 0 else sum(case when 网签总额<0 then 0 else  网签总额 end)/sum(case when 网签面积<0 then 0 else  网签面积 end) end, "
                        strSQL_Total = strSQL_Total + " 网签总额=sum(case when 网签总额<0 then 0 else  网签总额 end),网签面积=sum(case when 网签面积<0 then 0 else  网签面积 end) from ( "
                        strSQL_Total = strSQL_Total + strSQL
                        strSQL_Total = strSQL_Total + " )a group by 行政区域"
                        strSQL_Total = strSQL_Total + " )a"
                        strSQL_Total = strSQL_Total + " left join "
                        strSQL_Total = strSQL_Total + " ("
                        strSQL_Total = strSQL_Total + " select 行政区域,网签数=sum(case when 网签数<0 then 0 else  网签数 end),网签均价=case when sum(case when 网签面积<0 then 0 else  网签面积 end)<1 then 0 else sum(case when 网签总额<0 then 0 else  网签总额 end)/sum(case when 网签面积<0 then 0 else  网签面积 end) end, "
                        strSQL_Total = strSQL_Total + " 网签总额=sum(case when 网签总额<0 then 0 else  网签总额 end),网签面积=sum(case when 网签面积<0 then 0 else  网签面积 end) from ( "
                        strSQL_Total = strSQL_Total + strSQL_0
                        strSQL_Total = strSQL_Total + " )a group by 行政区域"
                        strSQL_Total = strSQL_Total + " )b on a.行政区域=b.行政区域"
                        '六区
                        strSQL_Total = strSQL_Total + " union"
                        strSQL_Total = strSQL_Total + " select a.行政区域,a.网签数,b.网签数 as '上周网签数',cast(a.网签均价 as numeric(16,2)) as 网签均价,cast(b.网签均价 as numeric(16,2)) as  '上周网签均价',a.网签总额,a.网签面积,网签数环比=case when b.网签数<1 then 0 else cast((a.网签数*1.0-b.网签数*1.0)/b.网签数*100.0 as numeric(16,2)) end,网签均价环比=case when b.网签均价<1 then 0 else cast((a.网签均价-b.网签均价)/b.网签均价*100 as numeric(16,2)) end,序号=2  from ("
                        strSQL_Total = strSQL_Total + " select '六区' as '行政区域',网签数=sum(case when 网签数<0 then 0 else  网签数 end),网签均价=case when sum(case when 网签面积<0 then 0 else  网签面积 end)<1 then 0 else sum(case when 网签总额<0 then 0 else  网签总额 end)/sum(case when 网签面积<0 then 0 else  网签面积 end) end, "
                        strSQL_Total = strSQL_Total + " 网签总额=sum(case when 网签总额<0 then 0 else  网签总额 end),网签面积=sum(case when 网签面积<0 then 0 else  网签面积 end) from ( "
                        strSQL_Total = strSQL_Total + strSQL
                        strSQL_Total = strSQL_Total + " )a  where 行政区域 in ('白云区','天河区','越秀区','荔湾区','海珠区','黄埔区')"
                        strSQL_Total = strSQL_Total + " )a"
                        strSQL_Total = strSQL_Total + " left join "
                        strSQL_Total = strSQL_Total + " ("
                        strSQL_Total = strSQL_Total + " select '六区' as '行政区域',网签数=sum(case when 网签数<0 then 0 else  网签数 end),网签均价=case when sum(case when 网签面积<0 then 0 else  网签面积 end)<1 then 0 else sum(case when 网签总额<0 then 0 else  网签总额 end)/sum(case when 网签面积<0 then 0 else  网签面积 end) end, "
                        strSQL_Total = strSQL_Total + " 网签总额=sum(case when 网签总额<0 then 0 else  网签总额 end),网签面积=sum(case when 网签面积<0 then 0 else  网签面积 end) from ( "
                        strSQL_Total = strSQL_Total + strSQL_0
                        strSQL_Total = strSQL_Total + " )a  where 行政区域 in ('白云区','天河区','越秀区','荔湾区','海珠区','黄埔区')"
                        strSQL_Total = strSQL_Total + " )b on a.行政区域=b.行政区域"

                        '十区
                        strSQL_Total = strSQL_Total + " union"
                        strSQL_Total = strSQL_Total + " select a.行政区域,a.网签数,b.网签数 as '上周网签数',cast(a.网签均价 as numeric(16,2)) as 网签均价,cast(b.网签均价 as numeric(16,2)) as  '上周网签均价',a.网签总额,a.网签面积,网签数环比=case when b.网签数<1 then 0 else cast((a.网签数*1.0-b.网签数*1.0)/b.网签数*100.0 as numeric(16,2)) end,网签均价环比=case when b.网签均价<1 then 0 else cast((a.网签均价-b.网签均价)/b.网签均价*100 as numeric(16,2)) end,序号=3  from ("
                        strSQL_Total = strSQL_Total + " select '十区' as '行政区域',网签数=sum(case when 网签数<0 then 0 else  网签数 end),网签均价=case when sum(case when 网签面积<0 then 0 else  网签面积 end)<1 then 0 else sum(case when 网签总额<0 then 0 else  网签总额 end)/sum(case when 网签面积<0 then 0 else  网签面积 end) end, "
                        strSQL_Total = strSQL_Total + " 网签总额=sum(case when 网签总额<0 then 0 else  网签总额 end),网签面积=sum(case when 网签面积<0 then 0 else  网签面积 end) from ( "
                        strSQL_Total = strSQL_Total + strSQL
                        strSQL_Total = strSQL_Total + " )a  where 行政区域 not in ('增城市','从化市')"
                        strSQL_Total = strSQL_Total + " )a"
                        strSQL_Total = strSQL_Total + " left join "
                        strSQL_Total = strSQL_Total + " ("
                        strSQL_Total = strSQL_Total + " select  '十区' as '行政区域',网签数=sum(case when 网签数<0 then 0 else  网签数 end),网签均价=case when sum(case when 网签面积<0 then 0 else  网签面积 end)<1 then 0 else sum(case when 网签总额<0 then 0 else  网签总额 end)/sum(case when 网签面积<0 then 0 else  网签面积 end) end, "
                        strSQL_Total = strSQL_Total + " 网签总额=sum(case when 网签总额<0 then 0 else  网签总额 end),网签面积=sum(case when 网签面积<0 then 0 else  网签面积 end) from ( "
                        strSQL_Total = strSQL_Total + strSQL_0
                        strSQL_Total = strSQL_Total + " )a  where 行政区域 not in ('增城市','从化市')"
                        strSQL_Total = strSQL_Total + " )b on a.行政区域=b.行政区域"

                        '十区两市
                        strSQL_Total = strSQL_Total + " union"
                        strSQL_Total = strSQL_Total + " select a.行政区域,a.网签数,b.网签数 as '上周网签数',cast(a.网签均价 as numeric(16,2)) as 网签均价,cast(b.网签均价 as numeric(16,2)) as  '上周网签均价',a.网签总额,a.网签面积,网签数环比=case when b.网签数<1 then 0 else cast((a.网签数*1.0-b.网签数*1.0)/b.网签数*100.0 as numeric(16,2)) end,网签均价环比=case when b.网签均价<1 then 0 else cast((a.网签均价-b.网签均价)/b.网签均价*100 as numeric(16,2)) end,序号=4  from ("
                        strSQL_Total = strSQL_Total + " select '十区两市' as '行政区域',网签数=sum(case when 网签数<0 then 0 else  网签数 end),网签均价=case when sum(case when 网签面积<0 then 0 else  网签面积 end)<1 then 0 else sum(case when 网签总额<0 then 0 else  网签总额 end)/sum(case when 网签面积<0 then 0 else  网签面积 end) end, "
                        strSQL_Total = strSQL_Total + " 网签总额=sum(case when 网签总额<0 then 0 else  网签总额 end),网签面积=sum(case when 网签面积<0 then 0 else  网签面积 end) from ( "
                        strSQL_Total = strSQL_Total + strSQL
                        strSQL_Total = strSQL_Total + " )a  "
                        strSQL_Total = strSQL_Total + " )a"
                        strSQL_Total = strSQL_Total + " left join "
                        strSQL_Total = strSQL_Total + " ("
                        strSQL_Total = strSQL_Total + " select  '十区两市' as '行政区域',网签数=sum(case when 网签数<0 then 0 else  网签数 end),网签均价=case when sum(case when 网签面积<0 then 0 else  网签面积 end)<1 then 0 else sum(case when 网签总额<0 then 0 else  网签总额 end)/sum(case when 网签面积<0 then 0 else  网签面积 end) end, "
                        strSQL_Total = strSQL_Total + " 网签总额=sum(case when 网签总额<0 then 0 else  网签总额 end),网签面积=sum(case when 网签面积<0 then 0 else  网签面积 end) from ( "
                        strSQL_Total = strSQL_Total + strSQL_0
                        strSQL_Total = strSQL_Total + " )a "
                        strSQL_Total = strSQL_Total + " )b on a.行政区域=b.行政区域"

                        strSQL_Total = strSQL_Total + " )A"
                        strSQL_Total = strSQL_Total + " order by 序号,A.行政区域 "
                        objSqlCommand.CommandText = strSQL_Total
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@startDate", datStartDate)
                        objSqlCommand.Parameters.AddWithValue("@endDate", datEndDate)
                        objSqlCommand.Parameters.AddWithValue("@frontstartDate", frontdatStartDate)
                        objSqlCommand.Parameters.AddWithValue("@frontendDate", frontdatEndDate)

                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempDeepdata.Tables(Xydc.Platform.Common.Data.SunshineData.TABLE_Sunshine_V_WeekRegion_Statistics))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDeepdata.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempDeepdata)
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objSunshine = objTempDeepdata
            getDataSet_RegionBuildingCompute_v3 = True
            Exit Function
errProc:
            Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempDeepdata)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function
        End Function


        '----------------------------------------------------------------
        ' 根据“查询条件”获取“N个星期的价格和套数”
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strUserId                  ：用户标识
        '     strPassword                ：用户密码
        '     strWhere                   ：搜索字符串 
        '     objParameter               : 查询条件参数
        '     objSunshine                ：信息数据集
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_NWeek_Compute_v3( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByVal objParameter As System.Collections.Specialized.NameValueCollection, _
            ByRef objSunshine As Xydc.Platform.Common.Data.SunshineData) As Boolean

            Dim objTempDeepdata As Xydc.Platform.Common.Data.SunshineData = Nothing
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Nothing
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand = Nothing
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim strSQL As String = ""
            Dim strSQL_0 As String = ""
            Dim strSQL_Total As String = ""

            '初始化
            getDataSet_NWeek_Compute_v3 = False
            objSunshine = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strWhere.Length > 0 Then strWhere = strWhere.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：[阳光家缘楼盘信息数据分析]未指定[连接用户]！"
                    GoTo errProc
                End If

                Dim datStartDate_Start As DateTime
                Dim datStartDate_End As DateTime
                Dim datEndDate_Start As DateTime
                Dim datEndDate_End As DateTime
                Dim strType As String
                Dim strRegion As String

                datStartDate_Start = CType(objParameter("StartDate"), System.DateTime)
                datEndDate_Start = CType(objParameter("EndDate"), System.DateTime)
                strType = objParameter("type")

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                Try
                    '创建数据集
                    objTempDeepdata = New Xydc.Platform.Common.Data.SunshineData(Xydc.Platform.Common.Data.SunshineData.enumTableType.Sunshine_V_NWeek_Statistics)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter

                        strSQL = ""
                        Select Case strType
                            Case "1"
                                strSQL = strSQL + " exec dbo.Sunshine_P_getWeekStatistics_business_V3 @FirstStartDate,@FinalStartDate "

                            Case "2"
                                strSQL = strSQL + " exec dbo.Sunshine_P_getWeekStatistics_V3 @FirstStartDate,@FinalStartDate "

                            Case "3"
                                strSQL = strSQL + " exec dbo.Sunshine_P_getWeekStatistics_office_V3 @FirstStartDate,@FinalStartDate "

                        End Select

                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@FirstStartDate", datStartDate_Start)
                        objSqlCommand.Parameters.AddWithValue("@FinalStartDate", datEndDate_Start)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempDeepdata.Tables(Xydc.Platform.Common.Data.SunshineData.TABLE_Sunshine_V_NWeek_Statistics))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDeepdata.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempDeepdata)
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objSunshine = objTempDeepdata
            getDataSet_NWeek_Compute_v3 = True
            Exit Function
errProc:
            Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempDeepdata)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function
        End Function



        '楼盘明细查询 2013-07-30
        '----------------------------------------------------------------
        ' 根据“查询条件”获取“阳光家缘楼盘信息数据分析”完全数据的数据集
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strSQL                     ：返回的SQL语句
        '     strType                    ：选择楼盘的类型。1-商业；2-住宅；3-办公
        '     strWhere                   ：搜索字符串      
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getSql_BuildingDetailCompute( _
             ByRef strErrMsg As String, _
             ByRef strSQL As String, _
             ByVal strWhere As String, _
             ByRef strType As String) As Boolean

            getSql_BuildingDetailCompute = False

            strSQL = ""
            Select Case strType

                Case "2"

                    strSQL = strSQL + "select a.*,isnull(b.i_sort,100000) as '楼盘排序' from ( "
                    strSQL = strSQL + " select b.行政区域,b.楼盘名称, b.项目名称, b.预售证,b.项目地址, case when b.房屋类型='1' then '别墅' else '洋房' end as 项目类型,  "
                    strSQL = strSQL + "        a.累计已售套数 as 合共成交, a.累计已售面积, a.未售套数, a.未售面积, 退房数, 网签数, 网签面积, 网签总额,  "
                    strSQL = strSQL + "        签约均价=case when b.网签面积=0 or b.网签数=0 then dbo.Sunshine_F_getDayHousePresellidPrice(b.项目名称,@endDate) "
                    strSQL = strSQL + "                      else  cast(round((b.网签总额)/(b.网签面积),2) as numeric(16,2))  end, "
                    strSQL = strSQL + "        累计均价=case when a.累计已售面积=0 or a.累计已售套数=0 then 0 else  cast(round((a.累计签约总额)/(a.累计已售面积),2) as numeric(16,2))  end "
                    strSQL = strSQL + " from "
                    strSQL = strSQL + "	( "
                    strSQL = strSQL + "	    select  行政区域,C_HOUSE as 楼盘名称, 项目名称, 预售证,项目地址, isnull(a.c_type,0) as 房屋类型,  "
                    strSQL = strSQL + "			   sum(当日退房套数) as 退房数, sum(当日签约套数) as 网签数, sum(当日签约面积) as 网签面积, "
                    strSQL = strSQL + "			   sum(当日签约总额) as 网签总额 from  "
                    strSQL = strSQL + "			   ( "
                    strSQL = strSQL + " 			  select * from "
                    strSQL = strSQL + " 			  ( "
                    strSQL = strSQL + "						select * from "
                    strSQL = strSQL + "                     ("
                    strSQL = strSQL + "							 select a.*,c.c_house,c.c_type from T_DAY_HOUSE_INFO a  left join T_HOUSE_MATCH_XMID c on a.项目名称=c.C_XM_NAME and a.预售证=c.C_XM_ID  and a.项目地址=c.c_xm_address "
                    strSQL = strSQL + "							   where a.日期  between @startDate  and  @endDate  "
                    strSQL = strSQL + "                      ) a   "
                    strSQL = strSQL + "					    where a.c_house is not null "
                    strSQL = strSQL + "				  ) a"
                    strSQL = strSQL + "               union "
                    strSQL = strSQL + "              select * from "
                    strSQL = strSQL + "               ( "
                    strSQL = strSQL + "                   select a.序号,a.日期,a.项目名称,a.预售证,a.项目地址,a.开发商,a.行政区域,a.当日签约套数,a.当日签约面积, "
                    strSQL = strSQL + "                          a.当日签约总额,a.当日签约均价,a.当日退房套数,a.当日退房面积,a.累计已售面积,a.累计已售套数, "
                    strSQL = strSQL + "                          a.累计已售均价,a.未售套数,a.未售面积,c.c_house, c.c_type from "
                    strSQL = strSQL + "                   ( "
                    strSQL = strSQL + "                       select a.*,c.c_house,c.c_type from T_DAY_HOUSE_INFO a  left join T_HOUSE_MATCH_XMID c on a.项目名称=c.C_XM_NAME and a.预售证=c.C_XM_ID  and a.项目地址=c.c_xm_address "
                    strSQL = strSQL + "                       where a.日期  between @startDate  and  @endDate  "
                    strSQL = strSQL + "                    ) a  join T_HOUSE_MATCH_XMID c on a.项目名称= c.c_xm_name  and a.项目地址=c.c_xm_address "
                    strSQL = strSQL + "                   where a.c_house is null "
                    strSQL = strSQL + "                ) b "
                    strSQL = strSQL + "             )a  group by 行政区域,C_HOUSE,项目名称,预售证,项目地址,a.c_type   "
                    strSQL = strSQL + " )b  "
                    strSQL = strSQL + " left join "
                    strSQL = strSQL + " ( "
                    strSQL = strSQL + "    select * from "
                    strSQL = strSQL + "    ( "
                    strSQL = strSQL + "        select 行政区域,C_HOUSE as 楼盘名称, 项目名称, 预售证, 项目地址, isnull(c_type,0) as 房屋类型,  "
                    strSQL = strSQL + "               累计已售套数 as 累计已售套数, 累计已售面积 as 累计已售面积, 未售套数 as 未售套数, "
                    strSQL = strSQL + "               未售面积 as 未售面积, 累计已售面积*累计已售均价 as 累计签约总额 from  "
                    strSQL = strSQL + "        ( "
                    strSQL = strSQL + "             select a.*,c.c_house,c.c_type from T_DAY_HOUSE_INFO a  left join T_HOUSE_MATCH_XMID c on a.项目名称=c.C_XM_NAME and a.预售证=c.C_XM_ID and a.项目地址=c.c_xm_address "
                    strSQL = strSQL + "             where a.日期=@endDate  "
                    strSQL = strSQL + "         ) a  "
                    strSQL = strSQL + "        where a.c_house is not null "
                    strSQL = strSQL + "     ) a"
                    strSQL = strSQL + "    union "
                    strSQL = strSQL + "    select * from "
                    strSQL = strSQL + "    ( "
                    strSQL = strSQL + "        select 行政区域, c.C_HOUSE as 楼盘名称, 项目名称, 预售证, 项目地址, isnull( c.c_type,0) as 房屋类型,  "
                    strSQL = strSQL + "               累计已售套数 as 累计已售套数, 累计已售面积 as 累计已售面积, 未售套数 as 未售套数, "
                    strSQL = strSQL + "               未售面积 as 未售面积, 累计已售面积*累计已售均价 as 累计签约总额  from "
                    strSQL = strSQL + "        ( "
                    strSQL = strSQL + "             select a.*,c.c_house,c.c_type from T_DAY_HOUSE_INFO a  left join T_HOUSE_MATCH_XMID c on a.项目名称=c.C_XM_NAME and a.预售证=c.c_xm_id and a.项目地址=c.c_xm_address "
                    strSQL = strSQL + "             where a.日期=@endDate  "
                    strSQL = strSQL + "        ) a  join  T_HOUSE_MATCH_XMID c on a.项目名称=c.C_XM_NAME  and a.项目地址=c.c_xm_address "
                    strSQL = strSQL + "        where a.c_house is null "
                    strSQL = strSQL + "    ) b"
                    strSQL = strSQL + " )a "
                    strSQL = strSQL + " on a.楼盘名称=b.楼盘名称 and b.项目名称=a.项目名称 and b.预售证=a.预售证 and a.项目地址=b.项目地址 and b.房屋类型=a.房屋类型 "
                    'strSQL = strSQL + " order by b.楼盘名称"
                    strSQL = strSQL + "  ) A left join (select * from T_HOUSE_MATCH_SORT where i_type=convert(integer,'" + strType + "')) b on b.c_NAME=a.楼盘名称 "
                    If strWhere <> "" Then
                        strSQL = strSQL + "where " + strWhere
                    End If

                Case "3"
                    strSQL = strSQL + " select a.*,isnull(b.i_sort,100000) as '楼盘排序' from ( "
                    strSQL = strSQL + " select b.行政区域,b.楼盘名称, b.项目名称, b.预售证,b.项目地址, b.项目类型,  "
                    strSQL = strSQL + "        a.累计已售套数 as 合共成交, a.累计已售面积, a.未售套数, a.未售面积, 退房数, 网签数, 网签面积, 网签总额,  "
                    strSQL = strSQL + "        签约均价=case when b.网签面积=0 or b.网签数=0 then dbo.Sunshine_F_getDayOfficePresellidPrice(b.项目名称,@endDate) "
                    strSQL = strSQL + "                      else  cast(round((b.网签总额)/(b.网签面积),2) as numeric(16,2))  end, "
                    strSQL = strSQL + "        累计均价=case when a.累计已售面积=0 or a.累计已售套数=0 then 0 else  cast(round((a.累计签约总额)/(a.累计已售面积),2) as numeric(16,2))  end "
                    strSQL = strSQL + " from "
                    strSQL = strSQL + "	( "
                    strSQL = strSQL + "	    select  行政区域,C_HOUSE as 楼盘名称, 项目名称, 预售证,项目地址, '办公' as 项目类型, "
                    strSQL = strSQL + "			   sum(当日退房套数) as 退房数, sum(当日签约套数) as 网签数, sum(当日签约面积) as 网签面积, "
                    strSQL = strSQL + "			   sum(当日签约总额) as 网签总额 from  "
                    strSQL = strSQL + "			   ( "
                    strSQL = strSQL + " 			  select * from "
                    strSQL = strSQL + " 			  ( "
                    strSQL = strSQL + "						select * from "
                    strSQL = strSQL + "                     ("
                    strSQL = strSQL + "							 select a.*,c.c_house from T_DAY_OFFICE_INFO a  left join T_HOUSE_MATCH_XMID c on a.项目名称=c.C_XM_NAME and a.预售证=c.C_XM_ID  and a.项目地址=c.c_xm_address "
                    strSQL = strSQL + "							   where a.日期  between @startDate  and  @endDate  "
                    strSQL = strSQL + "                      ) a   "
                    strSQL = strSQL + "					    where a.c_house is not null "
                    strSQL = strSQL + "				  ) a"
                    strSQL = strSQL + "               union "
                    strSQL = strSQL + "              select * from "
                    strSQL = strSQL + "               ( "
                    strSQL = strSQL + "                   select a.序号,a.日期,a.项目名称,a.预售证,a.项目地址,a.开发商,a.行政区域,a.当日签约套数,a.当日签约面积, "
                    strSQL = strSQL + "                          a.当日签约总额,a.当日签约均价,a.当日退房套数,a.当日退房面积,a.累计已售面积,a.累计已售套数, "
                    strSQL = strSQL + "                          a.累计已售均价,a.未售套数,a.未售面积,c.c_house from "
                    strSQL = strSQL + "                   ( "
                    strSQL = strSQL + "                       select a.*,c.c_house from T_DAY_OFFICE_INFO a  left join T_HOUSE_MATCH_XMID c on a.项目名称=c.C_XM_NAME and a.预售证=c.C_XM_ID  and a.项目地址=c.c_xm_address "
                    strSQL = strSQL + "                       where a.日期  between @startDate  and  @endDate  "
                    strSQL = strSQL + "                    ) a  join T_HOUSE_MATCH_XMID c on a.项目名称= c.c_xm_name  and a.项目地址=c.c_xm_address "
                    strSQL = strSQL + "                   where a.c_house is null "
                    strSQL = strSQL + "                ) b "
                    strSQL = strSQL + "             )a  group by 行政区域,C_HOUSE,项目名称,预售证,项目地址  "
                    strSQL = strSQL + " )b  "
                    strSQL = strSQL + " left join "
                    strSQL = strSQL + " ( "
                    strSQL = strSQL + "    select * from "
                    strSQL = strSQL + "    ( "
                    strSQL = strSQL + "        select 行政区域,C_HOUSE as 楼盘名称, 项目名称, 预售证, 项目地址, '办公' as 项目类型, "
                    strSQL = strSQL + "               累计已售套数 as 累计已售套数, 累计已售面积 as 累计已售面积, 未售套数 as 未售套数, "
                    strSQL = strSQL + "               未售面积 as 未售面积, 累计已售面积*累计已售均价 as 累计签约总额 from  "
                    strSQL = strSQL + "        ( "
                    strSQL = strSQL + "             select a.*,c.c_house from T_DAY_OFFICE_INFO a  left join T_HOUSE_MATCH_XMID c on a.项目名称=c.C_XM_NAME and a.预售证=c.C_XM_ID and a.项目地址=c.c_xm_address "
                    strSQL = strSQL + "             where a.日期=@endDate  "
                    strSQL = strSQL + "         ) a  "
                    strSQL = strSQL + "        where a.c_house is not null "
                    strSQL = strSQL + "     ) a"
                    strSQL = strSQL + "    union "
                    strSQL = strSQL + "    select * from "
                    strSQL = strSQL + "    ( "
                    strSQL = strSQL + "        select 行政区域, c.C_HOUSE as 楼盘名称, 项目名称, 预售证, 项目地址, '办公' as 项目类型, "
                    strSQL = strSQL + "               累计已售套数 as 累计已售套数, 累计已售面积 as 累计已售面积, 未售套数 as 未售套数, "
                    strSQL = strSQL + "               未售面积 as 未售面积, 累计已售面积*累计已售均价 as 累计签约总额  from "
                    strSQL = strSQL + "        ( "
                    strSQL = strSQL + "             select a.*,c.c_house from T_DAY_OFFICE_INFO a  left join T_HOUSE_MATCH_XMID c on a.项目名称=c.C_XM_NAME and a.预售证=c.c_xm_id and a.项目地址=c.c_xm_address "
                    strSQL = strSQL + "             where a.日期=@endDate  "
                    strSQL = strSQL + "        ) a  join  T_HOUSE_MATCH_XMID c on a.项目名称=c.C_XM_NAME  and a.项目地址=c.c_xm_address "
                    strSQL = strSQL + "        where a.c_house is null "
                    strSQL = strSQL + "    ) b"
                    strSQL = strSQL + " )a "
                    strSQL = strSQL + " on a.楼盘名称=b.楼盘名称 and b.项目名称=a.项目名称 and b.预售证=a.预售证 and a.项目地址=b.项目地址  "
                    'strSQL = strSQL + " order by b.楼盘名称"
                    strSQL = strSQL + "  ) A left join (select * from T_HOUSE_MATCH_SORT where i_type=convert(integer,'" + strType + "')) b on b.c_NAME=a.楼盘名称 "
                    If strWhere <> "" Then
                        strSQL = strSQL + " where " + strWhere
                    End If

                Case "1"

                    strSQL = strSQL + " select a.*,isnull(b.i_sort,100000) as '楼盘排序' from ( "
                    strSQL = strSQL + " select b.行政区域,b.楼盘名称, b.项目名称, b.预售证,b.项目地址, b.项目类型,  "
                    strSQL = strSQL + "        a.累计已售套数 as 合共成交, a.累计已售面积, a.未售套数, a.未售面积, 退房数, 网签数, 网签面积, 网签总额,  "
                    strSQL = strSQL + "        签约均价=case when b.网签面积=0 or b.网签数=0 then dbo.Sunshine_F_getDayBusinessPresellidPrice(b.项目名称,@endDate) "
                    strSQL = strSQL + "                      else  cast(round((b.网签总额)/(b.网签面积),2) as numeric(16,2))  end, "
                    strSQL = strSQL + "        累计均价=case when a.累计已售面积=0 or a.累计已售套数=0 then 0 else  cast(round((a.累计签约总额)/(a.累计已售面积),2) as numeric(16,2))  end "
                    strSQL = strSQL + " from "
                    strSQL = strSQL + "	( "
                    strSQL = strSQL + "	    select  行政区域,C_HOUSE as 楼盘名称, 项目名称, 预售证,项目地址, '商业' as 项目类型,  "
                    strSQL = strSQL + "			   sum(当日退房套数) as 退房数, sum(当日签约套数) as 网签数, sum(当日签约面积) as 网签面积, "
                    strSQL = strSQL + "			   sum(当日签约总额) as 网签总额 from  "
                    strSQL = strSQL + "			   ( "
                    strSQL = strSQL + " 			  select * from "
                    strSQL = strSQL + " 			  ( "
                    strSQL = strSQL + "						select * from "
                    strSQL = strSQL + "                     ("
                    strSQL = strSQL + "							 select a.*,c.c_house from T_DAY_BUSINESS_INFO a  left join T_HOUSE_MATCH_XMID c on a.项目名称=c.C_XM_NAME and a.预售证=c.C_XM_ID  and a.项目地址=c.c_xm_address "
                    strSQL = strSQL + "							   where a.日期  between @startDate  and  @endDate  "
                    strSQL = strSQL + "                      ) a   "
                    strSQL = strSQL + "					    where a.c_house is not null "
                    strSQL = strSQL + "				  ) a"
                    strSQL = strSQL + "               union "
                    strSQL = strSQL + "              select * from "
                    strSQL = strSQL + "               ( "
                    strSQL = strSQL + "                   select a.序号,a.日期,a.项目名称,a.预售证,a.项目地址,a.开发商,a.行政区域,a.当日签约套数,a.当日签约面积, "
                    strSQL = strSQL + "                          a.当日签约总额,a.当日签约均价,a.当日退房套数,a.当日退房面积,a.累计已售面积,a.累计已售套数, "
                    strSQL = strSQL + "                          a.累计已售均价,a.未售套数,a.未售面积,c.c_house from "
                    strSQL = strSQL + "                   ( "
                    strSQL = strSQL + "                       select a.*,c.c_house from T_DAY_BUSINESS_INFO a  left join T_HOUSE_MATCH_XMID c on a.项目名称=c.C_XM_NAME and a.预售证=c.C_XM_ID  and a.项目地址=c.c_xm_address "
                    strSQL = strSQL + "                       where a.日期  between @startDate  and  @endDate  "
                    strSQL = strSQL + "                    ) a  join T_HOUSE_MATCH_XMID c on a.项目名称= c.c_xm_name  and a.项目地址=c.c_xm_address "
                    strSQL = strSQL + "                   where a.c_house is null "
                    strSQL = strSQL + "                ) b "
                    strSQL = strSQL + "             )a  group by 行政区域,C_HOUSE,项目名称,预售证,项目地址  "
                    strSQL = strSQL + " )b  "
                    strSQL = strSQL + " left join "
                    strSQL = strSQL + " ( "
                    strSQL = strSQL + "    select * from "
                    strSQL = strSQL + "    ( "
                    strSQL = strSQL + "        select 行政区域,C_HOUSE as 楼盘名称, 项目名称, 预售证, 项目地址, '商业' as 项目类型, "
                    strSQL = strSQL + "               累计已售套数 as 累计已售套数, 累计已售面积 as 累计已售面积, 未售套数 as 未售套数, "
                    strSQL = strSQL + "               未售面积 as 未售面积, 累计已售面积*累计已售均价 as 累计签约总额 from  "
                    strSQL = strSQL + "        ( "
                    strSQL = strSQL + "             select a.*,c.c_house from T_DAY_BUSINESS_INFO a  left join T_HOUSE_MATCH_XMID c on a.项目名称=c.C_XM_NAME and a.预售证=c.C_XM_ID and a.项目地址=c.c_xm_address "
                    strSQL = strSQL + "             where a.日期=@endDate  "
                    strSQL = strSQL + "         ) a  "
                    strSQL = strSQL + "        where a.c_house is not null "
                    strSQL = strSQL + "     ) a"
                    strSQL = strSQL + "    union "
                    strSQL = strSQL + "    select * from "
                    strSQL = strSQL + "    ( "
                    strSQL = strSQL + "        select 行政区域, c.C_HOUSE as 楼盘名称, 项目名称, 预售证, 项目地址, '商业' as 项目类型, "
                    strSQL = strSQL + "               累计已售套数 as 累计已售套数, 累计已售面积 as 累计已售面积, 未售套数 as 未售套数, "
                    strSQL = strSQL + "               未售面积 as 未售面积, 累计已售面积*累计已售均价 as 累计签约总额  from "
                    strSQL = strSQL + "        ( "
                    strSQL = strSQL + "             select a.*,c.c_house from T_DAY_BUSINESS_INFO a  left join T_HOUSE_MATCH_XMID c on a.项目名称=c.C_XM_NAME and a.预售证=c.c_xm_id and a.项目地址=c.c_xm_address "
                    strSQL = strSQL + "             where a.日期=@endDate  "
                    strSQL = strSQL + "        ) a  join  T_HOUSE_MATCH_XMID c on a.项目名称=c.C_XM_NAME  and a.项目地址=c.c_xm_address "
                    strSQL = strSQL + "        where a.c_house is null "
                    strSQL = strSQL + "    ) b"
                    strSQL = strSQL + " )a "
                    strSQL = strSQL + " on a.楼盘名称=b.楼盘名称 and b.项目名称=a.项目名称 and b.预售证=a.预售证 and a.项目地址=b.项目地址  "
                    'strSQL = strSQL + " order by b.楼盘名称"
                    strSQL = strSQL + "  ) A left join (select * from T_HOUSE_MATCH_SORT where i_type=convert(integer,'" + strType + "')) b on b.c_NAME=a.楼盘名称 "
                    If strWhere <> "" Then
                        strSQL = strSQL + " where " + strWhere
                    End If

                Case Else

            End Select

            getSql_BuildingDetailCompute = True
errProc:

            Exit Function
        End Function





        '----------------------------------------------------------------
        ' 根据“查询条件”获取“阳光家缘楼盘信息数据分析”完全数据的数据集
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strUserId                  ：用户标识
        '     strPassword                ：用户密码
        '     strWhere                   ：搜索字符串 
        '     objParameter               : 查询条件参数
        '     objSunshine                ：信息数据集
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_BuildingDetailCompute( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByVal objParameter As System.Collections.Specialized.NameValueCollection, _
            ByRef objSunshine As Xydc.Platform.Common.Data.SunshineData) As Boolean

            Dim objTempDeepdata As Xydc.Platform.Common.Data.SunshineData = Nothing
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Nothing
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand = Nothing
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim strSQL As String = ""
            Dim strSQL_0 As String = ""
            Dim strSQL_Total As String = ""

            '初始化
            getDataSet_BuildingDetailCompute = False
            objSunshine = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strWhere.Length > 0 Then strWhere = strWhere.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：[阳光家缘楼盘信息数据分析]未指定[连接用户]！"
                    GoTo errProc
                End If

                Dim datStartDate As DateTime
                Dim datEndDate As DateTime
                Dim strType As String
                Dim strRegion As String

                datStartDate = CType(objParameter("StartDate"), System.DateTime)
                datEndDate = CType(objParameter("EndDate"), System.DateTime)
                strType = objParameter("type")

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If



                '获取数据
                Try
                    '创建数据集
                    objTempDeepdata = New Xydc.Platform.Common.Data.SunshineData(Xydc.Platform.Common.Data.SunshineData.enumTableType.Sunshine_V_Houseinfo_Statistics)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter

                        '获取查询语句      
                        If getSql_BuildingDetailCompute(strErrMsg, strSQL, strWhere, strType) = False Then
                            GoTo errProc
                        End If

                        strSQL_0 = ""
                        strSQL_0 = strSQL_0 + " select * from "
                        strSQL_0 = strSQL_0 + " ("
                        strSQL_0 = strSQL_0 + " select 行政区域,楼盘名称,项目名称,项目类型,预售证,网签数,签约均价 as 网签均价,累计均价 as '合共均价',合共成交,退房数,未售套数,网签总额,网签面积,楼盘排序,类型=1,排序=1 from ( "
                        strSQL_0 = strSQL_0 + strSQL
                        strSQL_0 = strSQL_0 + " )A "
                        strSQL_0 = strSQL_0 + " union"
                        strSQL_0 = strSQL_0 + " select 行政区域,楼盘名称='汇总',项目名称='',项目类型='',预售证='',网签数=sum(case when 网签数<0 then 0 else  网签数 end),网签均价=case when sum(case when 网签面积<0 then 0 else  网签面积 end)<1 then 0 else cast(round(sum(case when 网签总额<0 then 0 else  网签总额 end)/sum(case when 网签面积<0 then 0 else  网签面积 end),2) as numeric(16,2)) end,合共均价=0,合共成交=sum(case when 合共成交<0 then 0 else  合共成交 end),"
                        strSQL_0 = strSQL_0 + " sum(退房数),未售套数=sum(case when 未售套数<0 then 0 else  未售套数 end),网签总额=sum(case when 网签总额<0 then 0 else  网签总额 end),网签面积=sum(case when 网签面积<0 then 0 else  网签面积 end),楼盘排序='',类型=2,排序=1  from ( "
                        strSQL_0 = strSQL_0 + strSQL
                        strSQL_0 = strSQL_0 + " )A group by 行政区域"
                        strSQL_0 = strSQL_0 + " union"
                        strSQL_0 = strSQL_0 + " select 行政区域='合计',楼盘名称='',项目名称='',项目类型='',预售证='',网签数=sum(case when 网签数<0 then 0 else  网签数 end),网签均价=case when sum(case when 网签面积<0 then 0 else  网签面积 end)<1 then 0 else cast(round(sum(case when 网签总额<0 then 0 else  网签总额 end)/sum(case when 网签面积<0 then 0 else  网签面积 end),2) as numeric(16,2)) end,合共均价=0,合共成交=sum(case when 合共成交<0 then 0 else  合共成交 end), "
                        strSQL_0 = strSQL_0 + " sum(退房数),未售套数=sum(case when 未售套数<0 then 0 else  未售套数 end),网签总额=sum(case when 网签总额<0 then 0 else  网签总额 end),网签面积=sum(case when 网签面积<0 then 0 else  网签面积 end),楼盘排序='',类型=3,排序=2  from ( "
                        strSQL_0 = strSQL_0 + strSQL
                        strSQL_0 = strSQL_0 + " )A "
                        strSQL_0 = strSQL_0 + " )A "
                        strSQL_0 = strSQL_0 + " order by A.排序,A.行政区域,A.类型,A.楼盘排序,A.楼盘名称,A.项目类型"

                        objSqlCommand.CommandText = strSQL_0
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@startDate", datStartDate)
                        objSqlCommand.Parameters.AddWithValue("@endDate", datEndDate)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempDeepdata.Tables(Xydc.Platform.Common.Data.SunshineData.TABLE_Sunshine_V_Houseinfo_Statistics))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDeepdata.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempDeepdata)
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objSunshine = objTempDeepdata
            getDataSet_BuildingDetailCompute = True
            Exit Function
errProc:
            Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempDeepdata)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function
        End Function








    End Class
End Namespace
