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
    ' 类名    ：dacCommon
    '
    ' 功能描述：
    '     提供通用方式访问数据库的处理
    '----------------------------------------------------------------
    Public Class dacCommon
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.DataAccess.dacCommon)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub









        '----------------------------------------------------------------
        ' 获取记录集
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strTable             ：表名
        '     strWhere             : 条件
        '     strOrderby           : 排序
        '     objDataSet           ：信息数据集 
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strTable As String, _
            ByVal strWhere As String, _
            ByVal strOrderby As String, _
            ByRef objDataSet As System.Data.DataSet) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objTempDataSet As System.Data.DataSet

            '初始化
            getDataSet = False
            objDataSet = Nothing
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
                If getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                Dim strSQL As String
                Try

                    '创建数据集
                    objTempDataSet = New System.Data.DataSet

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select * " + vbCr
                        strSQL = strSQL + " from " + vbCr
                        strSQL = strSQL + strTable + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        If strOrderby <> "" Then
                            strSQL = strSQL + " order by " + strOrderby + vbCr
                        End If

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand
                        .Fill(objTempDataSet)
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDataSet.Tables.Count < 1 Then
                    Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objTempDataSet)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
           
            '返回
            objDataSet = objTempDataSet
            getDataSet = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objTempDataSet)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Exit Function

        End Function



        '----------------------------------------------------------------
        ' 保存数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strTable             ：表名
        '     strWhere             : 条件
        '     objType              ：true-字段本身没有带类型，有自定义；FALSE-字段本身的首字母就是自带类型
        '                          'C=字符型，i=数字型，d=日期           
        '     objNewData           ：新数据
        '     objenumEditType      ：编辑类型
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doSaveData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strTable As String, _
            ByVal strWhere As String, _
            ByVal objType As Boolean, _
            ByVal objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim intID As Integer

            '初始化
            doSaveData = False
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
              

                '获取连接
                If getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
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
                                If objType = False Then
                                    If strFields = "" Then
                                        '去掉字段前面带的类型标识符
                                        strFields = objNewData.GetKey(i)
                                    Else
                                        strFields = strFields + "," + objNewData.GetKey(i)
                                    End If
                                Else
                                    If strFields = "" Then
                                        '去掉字段前面带的类型标识符
                                        strFields = Mid(objNewData.GetKey(i), 2)
                                    Else
                                        strFields = strFields + "," + Mid(objNewData.GetKey(i), 2)
                                    End If
                                  
                                End If
                                If strValues = "" Then
                                    strValues = "@A" + i.ToString()
                                Else
                                    strValues = strValues + "," + "@A" + i.ToString()
                                End If
                            Next

                            strSQL = ""
                            strSQL = strSQL + " insert into " + strTable + " (" + strFields + ")"
                            strSQL = strSQL + " values (" + strValues + ")"
                            objSqlCommand.Parameters.Clear()
                            For i = 0 To intCount - 1 Step 1
                                strValue = objNewData.Item(i).Trim()
                                Select Case Left(objNewData.GetKey(i), 1)
                                    Case "i"
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), 0)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(objNewData.Item(i), Integer))
                                        End If
                                    Case "d"
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), System.DBNull.Value)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(objNewData.Item(i), System.DateTime))
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
                                If objType = False Then
                                    If strFields = "" Then
                                        strFields = objNewData.GetKey(i) + " = @A" + i.ToString()
                                    Else
                                        strFields = strFields + "," + objNewData.GetKey(i) + " = @A" + i.ToString()
                                    End If
                                Else
                                    If strFields = "" Then
                                        strFields = Mid(objNewData.GetKey(i), 2) + " = @A" + i.ToString()
                                    Else
                                        strFields = strFields + "," + Mid(objNewData.GetKey(i), 2) + " = @A" + i.ToString()
                                    End If
                                End If
                               
                            Next
                            strSQL = ""
                            strSQL = strSQL + " update " + strTable + "   set "
                            strSQL = strSQL + " " + strFields + " "
                            strSQL = strSQL + " where " + strWhere

                            objSqlCommand.Parameters.Clear()
                            For i = 0 To intCount - 1 Step 1
                                strValue = objNewData.Item(i).Trim()
                                Select Case Left(objNewData.GetKey(i), 1)
                                    Case "i"
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), 0)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(objNewData.Item(i), Integer))
                                        End If
                                    Case "d"
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), System.DBNull.Value)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(objNewData.Item(i), System.DateTime))
                                        End If
                                    Case Else
                                        If strValue = "" Then strValue = " "
                                        objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), strValue)
                                End Select
                            Next

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


            '返回
            doSaveData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 删除数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strTable             ：表名
        '     strWhere             : 条件
        '     objOldData           ：旧数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strTable As String, _
            ByVal strWhere As String, _
            ByVal objOldData As System.Data.DataRow) As Boolean


            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand


            '初始化
            doDeleteData = False
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
                If getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
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

                    strSQL = strSQL + " delete from " + strTable
                    strSQL = strSQL + " where " + strWhere
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

            '返回
            doDeleteData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Exit Function
        End Function



        '----------------------------------------------------------------
        ' 根据select,from,where,orderby获取SQL语句
        '     strSelect            ：select
        '     strFrom              ：from
        '     strWhere             ：where
        '     strOrderBy           ：order by
        ' 返回
        '                          ：合成后的SQL
        '----------------------------------------------------------------
        Public Function getSqlString( _
            ByVal strSelect As String, _
            ByVal strFrom As String, _
            ByVal strWhere As String, _
            ByVal strOrderBy As String) As String

            Dim strSQL As String

            '初始化
            getSqlString = ""

            Try
                If strSelect.Length > 0 Then strSelect = strSelect.Trim()
                If strFrom.Length > 0 Then strFrom = strFrom.Trim()
                If strWhere.Length > 0 Then strWhere = strWhere.Trim()
                If strOrderBy.Length > 0 Then strOrderBy = strOrderBy.Trim()

                '检查
                If strSelect.Length < 1 Then GoTo errProc
                If strFrom.Length < 1 Then GoTo errProc

                '合成
                strSQL = ""
                strSQL = strSQL + " select " + strSelect
                strSQL = strSQL + " from " + strFrom
                If strWhere.Length > 0 Then
                    strSQL = strSQL + " where " + strWhere
                End If
                If strOrderBy.Length > 0 Then
                    strSQL = strSQL + " order by " + strOrderBy
                End If
            Catch ex As Exception
                strSQL = ""
            End Try

            '返回
            getSqlString = strSQL
            Exit Function

errProc:
            Exit Function

        End Function




        '----------------------------------------------------------------
        ' 获取应用系统缺省的数据库连接(缺省服务器、数据库)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objSqlConnection     ：返回缺省数据库连接
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getConnection( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef objSqlConnection As System.Data.SqlClient.SqlConnection) As Boolean

            Dim objTempSqlConnection As System.Data.SqlClient.SqlConnection

            '初始化
            getConnection = False
            objSqlConnection = Nothing
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""

                '检查
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If

                '获取连接串
                Dim intConnectionTimeout As Integer
                Dim intCommandTimeout As Integer
                Dim strConnectionString As String
                intConnectionTimeout = Xydc.Platform.Common.jsoaConfiguration.ConnectionTimeout
                intCommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout
                strConnectionString = Xydc.Platform.Common.jsoaConfiguration.getConnectionString(strUserId, strPassword, intConnectionTimeout)

                '创建数据库连接
                Try
                    objTempSqlConnection = New System.Data.SqlClient.SqlConnection(strConnectionString)
                    objTempSqlConnection.Open()
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            '返回
            objSqlConnection = objTempSqlConnection
            getConnection = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objTempSqlConnection)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取应用系统缺省服务器中的指定数据库的连接
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objSqlConnection     ：返回缺省数据库连接
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     intConnectTimeOut    ：连接超时
        '     strDatabase          ：数据库名 
        '     strServer            ：服务器名
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getConnection( _
            ByRef strErrMsg As String, _
            ByRef objSqlConnection As System.Data.SqlClient.SqlConnection, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            Optional ByVal intConnectTimeOut As Integer = -1, _
            Optional ByVal strDatabase As String = "", _
            Optional ByVal strServer As String = "") As Boolean

            Dim objTempSqlConnection As System.Data.SqlClient.SqlConnection

            '初始化
            getConnection = False
            objSqlConnection = Nothing
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""

                '检查
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If

                '获取连接串
                Dim strConnectionString As String
                strConnectionString = Xydc.Platform.Common.jsoaConfiguration.getConnectionString(strUserId, strPassword, intConnectTimeOut, strDatabase, strServer)

                '创建数据库连接
                Try
                    objTempSqlConnection = New System.Data.SqlClient.SqlConnection(strConnectionString)
                    objTempSqlConnection.Open()
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            '返回
            objSqlConnection = objTempSqlConnection
            getConnection = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objTempSqlConnection)
            Exit Function

        End Function




        '----------------------------------------------------------------
        ' 根据SQL语句获取标准的DataSet
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strSQL               ：SQL语句
        '     objDataSet           ：返回数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDataSetBySQL( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strSQL As String, _
            ByRef objDataSet As System.Data.DataSet) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objTempDataSet As System.Data.DataSet

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '初始化
            getDataSetBySQL = False
            objDataSet = Nothing
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strSQL.Length > 0 Then strSQL = strSQL.Trim()

                '检查
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If strSQL.Length < 1 Then
                    strErrMsg = "错误：未指定SQL语句！"
                    GoTo errProc
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                Try
                    '创建数据集
                    objTempDataSet = New System.Data.DataSet

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempDataSet)
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDataSet.Tables.Count < 1 Then
                    Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objTempDataSet)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objDataSet = objTempDataSet
            getDataSetBySQL = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objTempDataSet)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据SQL语句获取标准的DataSet
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objSqlConnection     ：连接对象
        '     strSQL               ：SQL语句
        '     objDataSet           ：返回数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDataSetBySQL( _
            ByRef strErrMsg As String, _
            ByVal objSqlConnection As System.Data.SqlClient.SqlConnection, _
            ByVal strSQL As String, _
            ByRef objDataSet As System.Data.DataSet) As Boolean

            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objTempDataSet As System.Data.DataSet

            '初始化
            getDataSetBySQL = False
            objDataSet = Nothing
            strErrMsg = ""

            Try
                If strSQL.Length > 0 Then strSQL = strSQL.Trim()

                '检查
                If objSqlConnection Is Nothing Then
                    strErrMsg = "错误：未指定连接对象！"
                    GoTo errProc
                End If
                If strSQL.Length < 1 Then
                    strErrMsg = "错误：未指定SQL语句！"
                    GoTo errProc
                End If

                '获取数据
                Try
                    '创建数据集
                    objTempDataSet = New System.Data.DataSet

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempDataSet)
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDataSet.Tables.Count < 1 Then
                    Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objTempDataSet)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            '返回
            objDataSet = objTempDataSet
            getDataSetBySQL = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objTempDataSet)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据SQL语句获取标准的DataSet
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strSQL               ：SQL语句
        '     objParameters        ：SQL语句中包含的参数
        '     objDataSet           ：返回数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDataSetBySQL( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strSQL As String, _
            ByVal objParameters As System.Collections.Specialized.ListDictionary, _
            ByRef objDataSet As System.Data.DataSet) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objTempDataSet As System.Data.DataSet

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '初始化
            getDataSetBySQL = False
            objDataSet = Nothing
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strSQL.Length > 0 Then strSQL = strSQL.Trim()

                '检查
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If strSQL.Length < 1 Then
                    strErrMsg = "错误：未指定SQL语句！"
                    GoTo errProc
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                Try
                    '创建数据集
                    objTempDataSet = New System.Data.DataSet

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        Dim objDictionaryEntry As System.Collections.DictionaryEntry
                        For Each objDictionaryEntry In objParameters
                            objSqlCommand.Parameters.AddWithValue(CType(objDictionaryEntry.Key, String), objDictionaryEntry.Value)
                        Next
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempDataSet)
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDataSet.Tables.Count < 1 Then
                    Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objTempDataSet)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objDataSet = objTempDataSet
            getDataSetBySQL = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objTempDataSet)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据SQL语句获取标准的DataSet
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objSqlConnection     ：连接对象
        '     strSQL               ：SQL语句
        '     objParameters        ：SQL语句中包含的参数
        '     objDataSet           ：返回数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDataSetBySQL( _
            ByRef strErrMsg As String, _
            ByVal objSqlConnection As System.Data.SqlClient.SqlConnection, _
            ByVal strSQL As String, _
            ByVal objParameters As System.Collections.Specialized.ListDictionary, _
            ByRef objDataSet As System.Data.DataSet) As Boolean

            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objTempDataSet As System.Data.DataSet

            '初始化
            getDataSetBySQL = False
            objDataSet = Nothing
            strErrMsg = ""

            Try
                If strSQL.Length > 0 Then strSQL = strSQL.Trim()

                '检查
                If objSqlConnection Is Nothing Then
                    strErrMsg = "错误：未指定连接对象！"
                    GoTo errProc
                End If
                If strSQL.Length < 1 Then
                    strErrMsg = "错误：未指定SQL语句！"
                    GoTo errProc
                End If

                '获取数据
                Try
                    '创建数据集
                    objTempDataSet = New System.Data.DataSet

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        Dim objDictionaryEntry As System.Collections.DictionaryEntry
                        For Each objDictionaryEntry In objParameters
                            objSqlCommand.Parameters.AddWithValue(CType(objDictionaryEntry.Key, String), objDictionaryEntry.Value)
                        Next
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempDataSet)
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDataSet.Tables.Count < 1 Then
                    Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objTempDataSet)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            '返回
            objDataSet = objTempDataSet
            getDataSetBySQL = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objTempDataSet)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据SQL语句获取标准的DataSet(产生表结构信息)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strSQL               ：SQL语句
        '     strSrcTable          ：要获取结构的表名
        '     objDataSet           ：返回数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDataSetWithSchemaBySQL( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strSQL As String, _
            ByVal strSrcTable As String, _
            ByRef objDataSet As System.Data.DataSet) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objTempDataSet As System.Data.DataSet

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '初始化
            getDataSetWithSchemaBySQL = False
            objDataSet = Nothing
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strSQL.Length > 0 Then strSQL = strSQL.Trim()

                '检查
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If strSQL.Length < 1 Then
                    strErrMsg = "错误：未指定SQL语句！"
                    GoTo errProc
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                Try
                    '创建数据集
                    objTempDataSet = New System.Data.DataSet

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .FillSchema(objTempDataSet, SchemaType.Source, strSrcTable)
                        .Fill(objTempDataSet)
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDataSet.Tables.Count < 1 Then
                    Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objTempDataSet)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objDataSet = objTempDataSet
            getDataSetWithSchemaBySQL = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objTempDataSet)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据SQL语句获取标准的DataSet(产生表结构信息)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objSqlConnection     ：连接对象
        '     strSQL               ：SQL语句
        '     strSrcTable          ：要获取结构的表名
        '     objDataSet           ：返回数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDataSetWithSchemaBySQL( _
            ByRef strErrMsg As String, _
            ByVal objSqlConnection As System.Data.SqlClient.SqlConnection, _
            ByVal strSQL As String, _
            ByVal strSrcTable As String, _
            ByRef objDataSet As System.Data.DataSet) As Boolean

            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objTempDataSet As System.Data.DataSet

            '初始化
            getDataSetWithSchemaBySQL = False
            objDataSet = Nothing
            strErrMsg = ""

            Try
                If strSQL.Length > 0 Then strSQL = strSQL.Trim()

                '检查
                If objSqlConnection Is Nothing Then
                    strErrMsg = "错误：未指定连接对象！"
                    GoTo errProc
                End If
                If strSQL.Length < 1 Then
                    strErrMsg = "错误：未指定SQL语句！"
                    GoTo errProc
                End If

                '获取数据
                Try
                    '创建数据集
                    objTempDataSet = New System.Data.DataSet

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .FillSchema(objTempDataSet, SchemaType.Source, strSrcTable)
                        .Fill(objTempDataSet)
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDataSet.Tables.Count < 1 Then
                    Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objTempDataSet)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            '返回
            objDataSet = objTempDataSet
            getDataSetWithSchemaBySQL = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objTempDataSet)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Exit Function

        End Function




        '----------------------------------------------------------------
        ' 在objDataTable的strField列中搜索strValue
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objDataTable         ：在objDataTable内搜索
        '     strField             ：在objDataTable内搜索strField
        '     strValue             ：要搜索的值
        '     blnFound             ：True-存在，False-不存在
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doFindInDataTable( _
            ByRef strErrMsg As String, _
            ByVal objDataTable As System.Data.DataTable, _
            ByVal strField As String, _
            ByVal strValue As String, _
            ByRef blnFound As Boolean) As Boolean

            Dim objTempDataTable As System.Data.DataTable

            doFindInDataTable = False
            blnFound = False

            Try
                '检查
                If objDataTable Is Nothing Then
                    strErrMsg = "错误：未指定DataTable！"
                    GoTo errProc
                End If
                If strField.Length > 0 Then strField = strField.Trim()
                If strValue.Length > 0 Then strValue = strValue.Trim()
                If strField = "" Then
                    strErrMsg = "错误：未指定DataTable内搜索的字段！"
                    GoTo errProc
                End If

                '备份数据
                objTempDataTable = objDataTable.Copy()

                '搜索
                objTempDataTable.DefaultView.RowFilter = strField + " = '" + strValue + "'"
                If objTempDataTable.DefaultView.Count > 0 Then
                    blnFound = True
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            '释放资源
            If Not (objTempDataTable Is Nothing) Then
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objTempDataTable)
                objTempDataTable = Nothing
            End If

            doFindInDataTable = True
            Exit Function
errProc:
            '释放资源
            If Not (objTempDataTable Is Nothing) Then
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objTempDataTable)
                objTempDataTable = Nothing
            End If
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 在objDataTable的strField列中搜索intValue
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objDataTable         ：在objDataTable内搜索
        '     strField             ：在objDataTable内搜索strField
        '     intValue             ：要搜索的值
        '     blnFound             ：True-存在，False-不存在
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doFindInDataTable( _
            ByRef strErrMsg As String, _
            ByVal objDataTable As System.Data.DataTable, _
            ByVal strField As String, _
            ByVal intValue As Integer, _
            ByRef blnFound As Boolean) As Boolean

            Dim objTempDataTable As System.Data.DataTable

            doFindInDataTable = False
            blnFound = False

            Try
                '检查
                If objDataTable Is Nothing Then
                    strErrMsg = "错误：未指定DataTable！"
                    GoTo errProc
                End If
                If strField.Length > 0 Then strField = strField.Trim()
                If strField = "" Then
                    strErrMsg = "错误：未指定DataTable内搜索的字段！"
                    GoTo errProc
                End If

                '备份数据
                objTempDataTable = objDataTable.Copy()

                '搜索
                objTempDataTable.DefaultView.RowFilter = strField + " = " + intValue.ToString()
                If objTempDataTable.DefaultView.Count > 0 Then
                    blnFound = True
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objTempDataTable)

            doFindInDataTable = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objTempDataTable)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 在objDataTable的strField列中搜索dblValue
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objDataTable         ：在objDataTable内搜索
        '     strField             ：在objDataTable内搜索strField
        '     dblValue             ：要搜索的值
        '     blnFound             ：True-存在，False-不存在
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doFindInDataTable( _
            ByRef strErrMsg As String, _
            ByVal objDataTable As System.Data.DataTable, _
            ByVal strField As String, _
            ByVal dblValue As Double, _
            ByRef blnFound As Boolean) As Boolean

            Dim objTempDataTable As System.Data.DataTable

            doFindInDataTable = False
            blnFound = False

            Try
                '检查
                If objDataTable Is Nothing Then
                    strErrMsg = "错误：未指定DataTable！"
                    GoTo errProc
                End If
                If strField.Length > 0 Then strField = strField.Trim()
                If strField = "" Then
                    strErrMsg = "错误：未指定DataTable内搜索的字段！"
                    GoTo errProc
                End If

                '备份数据
                objTempDataTable = objDataTable.Copy()

                '搜索
                objTempDataTable.DefaultView.RowFilter = strField + " = " + dblValue.ToString()
                If objTempDataTable.DefaultView.Count > 0 Then
                    blnFound = True
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objTempDataTable)

            doFindInDataTable = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objTempDataTable)
            Exit Function

        End Function




        '----------------------------------------------------------------
        ' 获取新的唯一码(字段值必须是数字型值)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objSqlConnection     ：连接对象
        '     strFieldName         ：要检索的字段名
        '     strTableName         ：要检索的表名
        '     blnMaxNo             ：是否获取最大序号
        '     strNewCode           ：新的唯一码
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getNewCode( _
            ByRef strErrMsg As String, _
            ByVal objSqlConnection As System.Data.SqlClient.SqlConnection, _
            ByVal strFieldName As String, _
            ByVal strTableName As String, _
            ByVal blnMaxNo As Boolean, _
            ByRef strNewCode As String) As Boolean

            Dim objDataSet As System.Data.DataSet
            Dim intPosStart As Integer
            Dim intPosEnd As Integer
            Dim intPos As Integer
            Dim intStart As Integer
            Dim intEnd As Integer
            Dim intMid As Integer
            Dim strSQL As String

            getNewCode = False
            strNewCode = ""

            Try
                '检查
                If objSqlConnection Is Nothing Then
                    strErrMsg = "错误：未指定连接！"
                    GoTo errProc
                End If
                If strFieldName Is Nothing Then strFieldName = ""
                If strTableName Is Nothing Then strTableName = ""
                strFieldName = strFieldName.Trim()
                strTableName = strTableName.Trim()
                If strFieldName = "" Then
                    strErrMsg = "错误：未指定字段！"
                    GoTo errProc
                End If
                If strTableName = "" Then
                    strErrMsg = "错误：未指定表名！"
                    GoTo errProc
                End If

                '计算SQL语句
                strSQL = ""
                strSQL = strSQL + " select a." + strFieldName + vbCr
                strSQL = strSQL + " from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select " + strFieldName + " = convert(Integer, " + strFieldName + ")" + vbCr
                strSQL = strSQL + "   from " + strTableName + vbCr
                strSQL = strSQL + " ) a" + vbCr
                strSQL = strSQL + " group by a." + strFieldName + vbCr
                strSQL = strSQL + " order by a." + strFieldName + vbCr

                '获取所有代码
                If Me.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If

                '计算新代码
                With objDataSet.Tables(0)
                    '无记录
                    If .Rows.Count < 1 Then
                        strNewCode = "1"
                        GoTo normExit
                    End If

                    '检测是否最大号+1
                    intEnd = CType(.Rows(.Rows.Count - 1).Item(strFieldName), Integer)
                    If blnMaxNo = True Then
                        strNewCode = (intEnd + 1).ToString()
                        GoTo normExit
                    End If

                    '头部有空号
                    intStart = CType(.Rows(0).Item(strFieldName), Integer)
                    If intStart > 1 Then
                        strNewCode = (intStart - 1).ToString()
                        GoTo normExit
                    End If

                    '中间无空号
                    If (intEnd - intStart + 1) <= .Rows.Count Then
                        strNewCode = (intEnd + 1).ToString()
                        GoTo normExit
                    End If

                    '中间有空号
                    intPosStart = 0
                    intPosEnd = .Rows.Count - 1
                    Do While True
                        '获取中间位置的实际序号
                        intPos = CType(Fix((intPosStart + intPosEnd) / 2), Integer)
                        intMid = CType(.Rows(intPos).Item(strFieldName), Integer)

                        If (intMid - intStart + 1) <= (intPos - intPosStart + 1) Then '中间->尾有空号
                            intStart = intMid
                            intPosStart = intPos
                        Else                                                          '头->中间有空号
                            intEnd = intMid
                            intPosEnd = intPos
                        End If

                        If (intPosEnd - intPosStart) = 1 Then                         '找到空号区间
                            intStart = CType(.Rows(intPosStart).Item(strFieldName), Integer)
                            strNewCode = (intStart + 1).ToString()
                            GoTo normExit
                        End If
                    Loop
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

normExit:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)

            getNewCode = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取新的复合唯一码(字段值必须是数字型值)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objSqlConnection     ：连接对象
        '     strFieldName         ：要检索的字段名
        '     strRelaFields        ：复合唯一的其他字段名
        '     strRelaFieldsValue   ：复合唯一的其他字段值
        '     strTableName         ：要检索的表名
        '     blnMaxNo             ：是否获取最大序号
        '     strNewCode           ：新的唯一码
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getNewCode( _
            ByRef strErrMsg As String, _
            ByVal objSqlConnection As System.Data.SqlClient.SqlConnection, _
            ByVal strFieldName As String, _
            ByVal strRelaFields As String, _
            ByVal strRelaFieldsValue As String, _
            ByVal strTableName As String, _
            ByVal blnMaxNo As Boolean, _
            ByRef strNewCode As String) As Boolean

            Dim objDataSet As System.Data.DataSet
            Dim intPosStart As Integer
            Dim intPosEnd As Integer
            Dim intPos As Integer
            Dim intStart As Integer
            Dim intEnd As Integer
            Dim intMid As Integer
            Dim strSQL As String

            getNewCode = False
            strNewCode = ""

            Try
                '检查
                If objSqlConnection Is Nothing Then
                    strErrMsg = "错误：未指定连接！"
                    GoTo errProc
                End If
                If strFieldName Is Nothing Then strFieldName = ""
                If strTableName Is Nothing Then strTableName = ""
                If strRelaFields Is Nothing Then strRelaFields = ""
                If strRelaFieldsValue Is Nothing Then strRelaFieldsValue = ""
                strFieldName = strFieldName.Trim()
                strTableName = strTableName.Trim()
                strRelaFields = strRelaFields.Trim()
                strRelaFieldsValue = strRelaFieldsValue.Trim()
                If strFieldName = "" Then
                    strErrMsg = "错误：未指定字段！"
                    GoTo errProc
                End If
                If strTableName = "" Then
                    strErrMsg = "错误：未指定表名！"
                    GoTo errProc
                End If

                '单项唯一
                If strRelaFields = "" Then
                    If Me.getNewCode(strErrMsg, objSqlConnection, strFieldName, strTableName, blnMaxNo, strNewCode) = False Then
                        GoTo errProc
                    Else
                        GoTo normExit
                    End If
                End If

                '计算SQL语句
                Dim strRelaFieldName() As String
                strRelaFieldName = strRelaFields.Split(Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate.ToCharArray())
                Dim strRelaFieldValue() As String
                strRelaFieldValue = strRelaFieldsValue.Split(Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate.ToCharArray())
                If strRelaFieldName.Length <> strRelaFieldValue.Length Then
                    strErrMsg = "错误：字段名与字段值的数目不匹配！"
                    GoTo errProc
                End If
                Dim strWhere As String = ""
                Dim intCount As Integer
                Dim i As Integer
                intCount = strRelaFieldName.Length
                For i = 0 To intCount - 1 Step 1
                    If strWhere = "" Then
                        strWhere = strRelaFieldName(i) + " = '" + strRelaFieldValue(i) + "'"
                    Else
                        strWhere = strWhere + " and " + strRelaFieldName(i) + " = '" + strRelaFieldValue(i) + "'"
                    End If
                Next
                strSQL = ""
                strSQL = strSQL + " select a." + strFieldName + vbCr
                strSQL = strSQL + " from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select " + strFieldName + " = convert(Integer, " + strFieldName + ")" + vbCr
                strSQL = strSQL + "   from " + strTableName + vbCr
                If strWhere <> "" Then
                    strSQL = strSQL + "   where " + strWhere + vbCr
                End If
                strSQL = strSQL + " ) a" + vbCr
                strSQL = strSQL + " group by a." + strFieldName + vbCr
                strSQL = strSQL + " order by a." + strFieldName + vbCr

                '获取所有代码
                If Me.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If

                '计算新代码
                With objDataSet.Tables(0)
                    '无记录
                    If .Rows.Count < 1 Then
                        strNewCode = "1"
                        GoTo normExit
                    End If

                    '检测是否最大号+1
                    intEnd = CType(.Rows(.Rows.Count - 1).Item(strFieldName), Integer)
                    If blnMaxNo = True Then
                        strNewCode = (intEnd + 1).ToString()
                        GoTo normExit
                    End If

                    '头部有空号
                    intStart = CType(.Rows(0).Item(strFieldName), Integer)
                    If intStart > 1 Then
                        strNewCode = (intStart - 1).ToString()
                        GoTo normExit
                    End If

                    '中间无空号
                    If (intEnd - intStart + 1) <= .Rows.Count Then
                        strNewCode = (intEnd + 1).ToString()
                        GoTo normExit
                    End If

                    '中间有空号
                    intPosStart = 0
                    intPosEnd = .Rows.Count - 1
                    Do While True
                        '获取中间位置的实际序号
                        intPos = CType(Fix((intPosStart + intPosEnd) / 2), Integer)
                        intMid = CType(.Rows(intPos).Item(strFieldName), Integer)

                        If (intMid - intStart + 1) <= (intPos - intPosStart + 1) Then '中间->尾有空号
                            intStart = intMid
                            intPosStart = intPos
                        Else                                                          '头->中间有空号
                            intEnd = intMid
                            intPosEnd = intPos
                        End If

                        If (intPosEnd - intPosStart) = 1 Then                         '找到空号区间
                            intStart = CType(.Rows(intPosStart).Item(strFieldName), Integer)
                            strNewCode = (intStart + 1).ToString()
                            GoTo normExit
                        End If
                    Loop
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

normExit:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)

            getNewCode = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取新的复合唯一码(字段值必须是数字型值)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objSqlConnection     ：连接对象
        '     strFieldName         ：要检索的字段名
        '     strRelaFields        ：复合唯一的其他字段名
        '     strRelaFieldsValue   ：复合唯一的其他字段值
        '     strTableName         ：要检索的表名
        '     intCodeLen           ：要获取的代码长度
        '     strPrevCodeValue     ：上级代码值
        '     blnMaxNo             ：是否获取最大序号
        '     strNewCode           ：新的唯一码
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getNewCode( _
            ByRef strErrMsg As String, _
            ByVal objSqlConnection As System.Data.SqlClient.SqlConnection, _
            ByVal strFieldName As String, _
            ByVal strRelaFields As String, _
            ByVal strRelaFieldsValue As String, _
            ByVal strTableName As String, _
            ByVal intCodeLen As Integer, _
            ByVal strPrevCodeValue As String, _
            ByVal blnMaxNo As Boolean, _
            ByRef strNewCode As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objDataSet As System.Data.DataSet
            Dim intPosStart As Integer
            Dim intPosEnd As Integer
            Dim intPos As Integer
            Dim intStart As Integer
            Dim intEnd As Integer
            Dim intMid As Integer
            Dim strSQL As String

            getNewCode = False
            strNewCode = ""

            Try
                '检查
                If objSqlConnection Is Nothing Then
                    strErrMsg = "错误：未指定连接！"
                    GoTo errProc
                End If
                If strFieldName Is Nothing Then strFieldName = ""
                If strTableName Is Nothing Then strTableName = ""
                If strRelaFields Is Nothing Then strRelaFields = ""
                If strRelaFieldsValue Is Nothing Then strRelaFieldsValue = ""
                strFieldName = strFieldName.Trim()
                strTableName = strTableName.Trim()
                strRelaFields = strRelaFields.Trim()
                strRelaFieldsValue = strRelaFieldsValue.Trim()
                If strFieldName = "" Then
                    strErrMsg = "错误：未指定字段！"
                    GoTo errProc
                End If
                If strTableName = "" Then
                    strErrMsg = "错误：未指定表名！"
                    GoTo errProc
                End If
                If strPrevCodeValue Is Nothing Then strPrevCodeValue = ""
                strPrevCodeValue = strPrevCodeValue.Trim

                '单项唯一
                If strRelaFields = "" Then
                    If Me.getNewCode(strErrMsg, objSqlConnection, strFieldName, strTableName, intCodeLen, strPrevCodeValue, blnMaxNo, strNewCode) = False Then
                        GoTo errProc
                    Else
                        GoTo normExit
                    End If
                End If

                '计算SQL语句
                Dim strRelaFieldName() As String
                strRelaFieldName = strRelaFields.Split(Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate.ToCharArray())
                Dim strRelaFieldValue() As String
                strRelaFieldValue = strRelaFieldsValue.Split(Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate.ToCharArray())
                If strRelaFieldName.Length <> strRelaFieldValue.Length Then
                    strErrMsg = "错误：字段名与字段值的数目不匹配！"
                    GoTo errProc
                End If
                Dim strWhere As String = ""
                Dim intCount As Integer
                Dim i As Integer
                intCount = strRelaFieldName.Length
                For i = 0 To intCount - 1 Step 1
                    If strWhere = "" Then
                        strWhere = strRelaFieldName(i) + " = '" + strRelaFieldValue(i) + "'" + vbCr
                    Else
                        strWhere = strWhere + " and " + strRelaFieldName(i) + " = '" + strRelaFieldValue(i) + "'" + vbCr
                    End If
                Next

                '计算分级代码
                Dim intPreCodeLen As Integer
                Dim intCurCodeLen As Integer
                intPreCodeLen = strPrevCodeValue.Length
                intCurCodeLen = intCodeLen - intPreCodeLen
                If strPrevCodeValue = "" Then
                    strSQL = ""
                    strSQL = strSQL + " select a." + strFieldName + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select " + strFieldName + " = convert(Integer, " + strFieldName + ")" + vbCr
                    strSQL = strSQL + "   from " + strTableName + vbCr
                    strSQL = strSQL + "   where len(ltrim(rtrim(" + strFieldName + "))) = " + intCodeLen.ToString() + vbCr
                    strSQL = strSQL + "   and " + strWhere + vbCr
                    strSQL = strSQL + " ) a" + vbCr
                    strSQL = strSQL + " group by a." + strFieldName + vbCr
                    strSQL = strSQL + " order by a." + strFieldName + vbCr
                Else
                    Dim strField As String = ""
                    strField = "substring(" + strFieldName + "," + (intPreCodeLen + 1).ToString() + "," + intCurCodeLen.ToString() + ")"

                    strSQL = ""
                    strSQL = strSQL + " select a." + strFieldName + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select " + strFieldName + " = convert(Integer," + strField + ")" + vbCr
                    strSQL = strSQL + "   from " + strTableName + vbCr
                    strSQL = strSQL + "   where " + strFieldName + " like '" + strPrevCodeValue + "%' " + vbCr
                    strSQL = strSQL + "   and len(ltrim(rtrim(" + strFieldName + "))) = " + intCodeLen.ToString() + vbCr
                    strSQL = strSQL + "   and " + strWhere + vbCr
                    strSQL = strSQL + " ) a" + vbCr
                    strSQL = strSQL + " group by a." + strFieldName + vbCr
                    strSQL = strSQL + " order by a." + strFieldName + vbCr
                End If

                '获取所有代码
                If Me.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If

                '计算新代码
                With objDataSet.Tables(0)
                    '无记录
                    If .Rows.Count < 1 Then
                        strNewCode = "1"
                        strNewCode = strPrevCodeValue + objPulicParameters.doFillString(strNewCode, intCurCodeLen, "0", True)
                        GoTo normExit
                    End If

                    '检测是否最大号+1
                    intEnd = CType(.Rows(.Rows.Count - 1).Item(strFieldName), Integer)
                    If blnMaxNo = True Then
                        strNewCode = (intEnd + 1).ToString()
                        strNewCode = strPrevCodeValue + objPulicParameters.doFillString(strNewCode, intCurCodeLen, "0", True)
                        GoTo normExit
                    End If

                    '头部有空号
                    intStart = CType(.Rows(0).Item(strFieldName), Integer)
                    If intStart > 1 Then
                        strNewCode = (intStart - 1).ToString()
                        strNewCode = strPrevCodeValue + objPulicParameters.doFillString(strNewCode, intCurCodeLen, "0", True)
                        GoTo normExit
                    End If

                    '中间无空号
                    If (intEnd - intStart + 1) <= .Rows.Count Then
                        strNewCode = (intEnd + 1).ToString()
                        strNewCode = strPrevCodeValue + objPulicParameters.doFillString(strNewCode, intCurCodeLen, "0", True)
                        GoTo normExit
                    End If

                    '中间有空号
                    intPosStart = 0
                    intPosEnd = .Rows.Count - 1
                    Do While True
                        '获取中间位置的实际序号
                        intPos = CType(Fix((intPosStart + intPosEnd) / 2), Integer)
                        intMid = CType(.Rows(intPos).Item(strFieldName), Integer)

                        If (intMid - intStart + 1) <= (intPos - intPosStart + 1) Then '中间->尾有空号
                            intStart = intMid
                            intPosStart = intPos
                        Else                                                          '头->中间有空号
                            intEnd = intMid
                            intPosEnd = intPos
                        End If

                        If (intPosEnd - intPosStart) = 1 Then                         '找到空号区间
                            intStart = CType(.Rows(intPosStart).Item(strFieldName), Integer)
                            strNewCode = (intStart + 1).ToString()
                            strNewCode = strPrevCodeValue + objPulicParameters.doFillString(strNewCode, intCurCodeLen, "0", True)
                            GoTo normExit
                        End If
                    Loop
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

normExit:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)

            getNewCode = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取新的分级代码的唯一码(字段值必须是数字型值)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objSqlConnection     ：连接对象
        '     strFieldName         ：要检索的字段名
        '     strTableName         ：要检索的表名
        '     intCodeLen           ：要获取的代码长度
        '     strPrevCodeValue     ：上级代码值
        '     blnMaxNo             ：是否获取最大序号
        '     strNewCode           ：新的唯一码
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getNewCode( _
            ByRef strErrMsg As String, _
            ByVal objSqlConnection As System.Data.SqlClient.SqlConnection, _
            ByVal strFieldName As String, _
            ByVal strTableName As String, _
            ByVal intCodeLen As Integer, _
            ByVal strPrevCodeValue As String, _
            ByVal blnMaxNo As Boolean, _
            ByRef strNewCode As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objDataSet As System.Data.DataSet
            Dim intPosStart As Integer
            Dim intPosEnd As Integer
            Dim intPos As Integer
            Dim intStart As Integer
            Dim intEnd As Integer
            Dim intMid As Integer
            Dim strSQL As String

            Dim intPreCodeLen As Integer
            Dim intCurCodeLen As Integer

            getNewCode = False
            strNewCode = ""

            Try
                '检查
                If objSqlConnection Is Nothing Then
                    strErrMsg = "错误：未指定连接！"
                    GoTo errProc
                End If
                If strFieldName Is Nothing Then strFieldName = ""
                If strTableName Is Nothing Then strTableName = ""
                If strPrevCodeValue Is Nothing Then strPrevCodeValue = ""
                strFieldName = strFieldName.Trim()
                strTableName = strTableName.Trim()
                strPrevCodeValue = strPrevCodeValue.Trim()
                If strFieldName = "" Then
                    strErrMsg = "错误：未指定字段！"
                    GoTo errProc
                End If
                If strTableName = "" Then
                    strErrMsg = "错误：未指定表名！"
                    GoTo errProc
                End If

                '计算SQL语句
                intPreCodeLen = strPrevCodeValue.Length
                intCurCodeLen = intCodeLen - intPreCodeLen
                If strPrevCodeValue = "" Then
                    strSQL = ""
                    strSQL = strSQL + " select a." + strFieldName + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select " + strFieldName + " = convert(Integer, " + strFieldName + ")" + vbCr
                    strSQL = strSQL + "   from " + strTableName + vbCr
                    strSQL = strSQL + "   where len(ltrim(rtrim(" + strFieldName + "))) = " + intCodeLen.ToString() + vbCr
                    strSQL = strSQL + " ) a" + vbCr
                    strSQL = strSQL + " group by a." + strFieldName + vbCr
                    strSQL = strSQL + " order by a." + strFieldName + vbCr
                Else
                    Dim strField As String
                    strField = "substring(" + strFieldName + "," + (intPreCodeLen + 1).ToString() + "," + intCurCodeLen.ToString() + ")"

                    strSQL = ""
                    strSQL = strSQL + " select a." + strFieldName + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select " + strFieldName + " = convert(Integer," + strField + ")" + vbCr
                    strSQL = strSQL + "   from " + strTableName + vbCr
                    strSQL = strSQL + "   where " + strFieldName + " like '" + strPrevCodeValue + "%' " + vbCr
                    strSQL = strSQL + "   and len(ltrim(rtrim(" + strFieldName + "))) = " + intCodeLen.ToString() + vbCr
                    strSQL = strSQL + " ) a" + vbCr
                    strSQL = strSQL + " group by a." + strFieldName + vbCr
                    strSQL = strSQL + " order by a." + strFieldName + vbCr
                End If

                '获取所有代码
                If Me.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If

                '计算新代码
                With objDataSet.Tables(0)
                    '无记录
                    If .Rows.Count < 1 Then
                        strNewCode = "1"
                        GoTo normExit
                    End If

                    '检测是否最大号+1
                    intEnd = CType(.Rows(.Rows.Count - 1).Item(strFieldName), Integer)
                    If blnMaxNo = True Then
                        strNewCode = (intEnd + 1).ToString()
                        GoTo normExit
                    End If

                    '头部有空号
                    intStart = CType(.Rows(0).Item(strFieldName), Integer)
                    If intStart > 1 Then
                        strNewCode = (intStart - 1).ToString()
                        GoTo normExit
                    End If

                    '中间无空号
                    If (intEnd - intStart + 1) <= .Rows.Count Then
                        strNewCode = (intEnd + 1).ToString()
                        GoTo normExit
                    End If

                    '中间有空号
                    intPosStart = 0
                    intPosEnd = .Rows.Count - 1
                    Do While True
                        '获取中间位置的实际序号
                        intPos = CType(Fix((intPosStart + intPosEnd) / 2), Integer)
                        intMid = CType(.Rows(intPos).Item(strFieldName), Integer)

                        If (intMid - intStart + 1) <= (intPos - intPosStart + 1) Then '中间->尾有空号
                            intStart = intMid
                            intPosStart = intPos
                        Else                                                          '头->中间有空号
                            intEnd = intMid
                            intPosEnd = intPos
                        End If

                        If (intPosEnd - intPosStart) = 1 Then                         '找到空号区间
                            intStart = CType(.Rows(intPosStart).Item(strFieldName), Integer)
                            strNewCode = (intStart + 1).ToString()
                            GoTo normExit
                        End If
                    Loop
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

normExit:
            strNewCode = objPulicParameters.doFillString(strNewCode, intCurCodeLen, "0", True)
            strNewCode = strPrevCodeValue + strNewCode

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)

            getNewCode = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取新的复合分级代码的唯一码(字段值必须是数字型值)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objSqlConnection     ：连接对象
        '     strFieldName         ：要检索的字段名
        '     strTableName         ：要检索的表名
        '     intCodeLen           ：要获取的代码长度
        '     strPrevCodeValue     ：上级代码值
        '     strRelaFields        ：复合唯一的其他字段名
        '     strRelaFieldsValue   ：复合唯一的其他字段值
        '     blnMaxNo             ：是否获取最大序号
        '     strNewCode           ：新的唯一码
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getNewCode( _
            ByRef strErrMsg As String, _
            ByVal objSqlConnection As System.Data.SqlClient.SqlConnection, _
            ByVal strFieldName As String, _
            ByVal strTableName As String, _
            ByVal intCodeLen As Integer, _
            ByVal strPrevCodeValue As String, _
            ByVal strRelaFields As String, _
            ByVal strRelaFieldsValue As String, _
            ByVal blnMaxNo As Boolean, _
            ByRef strNewCode As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objDataSet As System.Data.DataSet
            Dim intPosStart As Integer
            Dim intPosEnd As Integer
            Dim intPos As Integer
            Dim intStart As Integer
            Dim intEnd As Integer
            Dim intMid As Integer
            Dim strSQL As String

            Dim intPreCodeLen As Integer
            Dim intCurCodeLen As Integer

            getNewCode = False
            strNewCode = ""

            Try
                '检查
                If objSqlConnection Is Nothing Then
                    strErrMsg = "错误：未指定连接！"
                    GoTo errProc
                End If
                If strFieldName Is Nothing Then strFieldName = ""
                If strTableName Is Nothing Then strTableName = ""
                If strPrevCodeValue Is Nothing Then strPrevCodeValue = ""
                If strRelaFields Is Nothing Then strRelaFields = ""
                If strRelaFieldsValue Is Nothing Then strRelaFieldsValue = ""
                strFieldName = strFieldName.Trim()
                strTableName = strTableName.Trim()
                strPrevCodeValue = strPrevCodeValue.Trim()
                strRelaFields = strRelaFields.Trim()
                strRelaFieldsValue = strRelaFieldsValue.Trim()
                If strFieldName = "" Then
                    strErrMsg = "错误：未指定字段！"
                    GoTo errProc
                End If
                If strTableName = "" Then
                    strErrMsg = "错误：未指定表名！"
                    GoTo errProc
                End If

                '单项唯一
                If strRelaFields = "" Then
                    If Me.getNewCode(strErrMsg, objSqlConnection, strFieldName, strTableName, intCodeLen, strPrevCodeValue, blnMaxNo, strNewCode) = False Then
                        GoTo errProc
                    Else
                        GoTo normExit
                    End If
                End If

                '计算SQL语句
                Dim strRelaFieldName() As String
                strRelaFieldName = strRelaFields.Split(Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate.ToCharArray())
                Dim strRelaFieldValue() As String
                strRelaFieldValue = strRelaFieldsValue.Split(Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate.ToCharArray())
                If strRelaFieldName.Length <> strRelaFieldValue.Length Then
                    strErrMsg = "错误：字段名与字段值的数目不匹配！"
                    GoTo errProc
                End If
                Dim strWhere As String = ""
                Dim intCount As Integer
                Dim i As Integer
                intCount = strRelaFieldName.Length
                For i = 0 To intCount - 1 Step 1
                    If strWhere = "" Then
                        strWhere = strRelaFieldName(i) + " = '" + strRelaFieldValue(i) + "'"
                    Else
                        strWhere = strWhere + " and " + strRelaFieldName(i) + " = '" + strRelaFieldValue(i) + "'"
                    End If
                Next

                '计算SQL语句
                intPreCodeLen = strPrevCodeValue.Length
                intCurCodeLen = intCodeLen - intPreCodeLen
                If strPrevCodeValue = "" Then
                    strSQL = ""
                    strSQL = strSQL + " select a." + strFieldName + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select " + strFieldName + " = convert(Integer, " + strFieldName + ")" + vbCr
                    strSQL = strSQL + "   from " + strTableName + vbCr
                    strSQL = strSQL + "   where len(ltrim(rtrim(" + strFieldName + "))) = " + intCodeLen.ToString() + vbCr
                    If strWhere <> "" Then
                        strSQL = strSQL + "   and " + strWhere + vbCr
                    End If
                    strSQL = strSQL + " ) a" + vbCr
                    strSQL = strSQL + " group by a." + strFieldName + vbCr
                    strSQL = strSQL + " order by a." + strFieldName + vbCr
                Else
                    Dim strField As String
                    strField = "substring(" + strFieldName + "," + (intPreCodeLen + 1).ToString() + "," + intCurCodeLen.ToString() + ")"

                    strSQL = ""
                    strSQL = strSQL + " select a." + strFieldName + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select " + strFieldName + " = convert(Integer," + strField + ")" + vbCr
                    strSQL = strSQL + "   from " + strTableName + vbCr
                    strSQL = strSQL + "   where " + strFieldName + " like '" + strPrevCodeValue + "%' " + vbCr
                    strSQL = strSQL + "   and len(ltrim(rtrim(" + strFieldName + "))) = " + intCodeLen.ToString() + vbCr
                    If strWhere <> "" Then
                        strSQL = strSQL + "   and " + strWhere + vbCr
                    End If
                    strSQL = strSQL + " ) a" + vbCr
                    strSQL = strSQL + " group by a." + strFieldName + vbCr
                    strSQL = strSQL + " order by a." + strFieldName + vbCr
                End If

                '获取所有代码
                If Me.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If

                '计算新代码
                With objDataSet.Tables(0)
                    '无记录
                    If .Rows.Count < 1 Then
                        strNewCode = "1"
                        GoTo normExit
                    End If

                    '检测是否最大号+1
                    intEnd = CType(.Rows(.Rows.Count - 1).Item(strFieldName), Integer)
                    If blnMaxNo = True Then
                        strNewCode = (intEnd + 1).ToString()
                        GoTo normExit
                    End If

                    '头部有空号
                    intStart = CType(.Rows(0).Item(strFieldName), Integer)
                    If intStart > 1 Then
                        strNewCode = (intStart - 1).ToString()
                        GoTo normExit
                    End If

                    '中间无空号
                    If (intEnd - intStart + 1) <= .Rows.Count Then
                        strNewCode = (intEnd + 1).ToString()
                        GoTo normExit
                    End If

                    '中间有空号
                    intPosStart = 0
                    intPosEnd = .Rows.Count - 1
                    Do While True
                        '获取中间位置的实际序号
                        intPos = CType(Fix((intPosStart + intPosEnd) / 2), Integer)
                        intMid = CType(.Rows(intPos).Item(strFieldName), Integer)

                        If (intMid - intStart + 1) <= (intPos - intPosStart + 1) Then '中间->尾有空号
                            intStart = intMid
                            intPosStart = intPos
                        Else                                                          '头->中间有空号
                            intEnd = intMid
                            intPosEnd = intPos
                        End If

                        If (intPosEnd - intPosStart) = 1 Then                         '找到空号区间
                            intStart = CType(.Rows(intPosStart).Item(strFieldName), Integer)
                            strNewCode = (intStart + 1).ToString()
                            GoTo normExit
                        End If
                    Loop
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

normExit:
            strNewCode = objPulicParameters.doFillString(strNewCode, intCurCodeLen, "0", True)
            strNewCode = strPrevCodeValue + strNewCode

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)

            getNewCode = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取新的分级代码的唯一码(字段值必须是数字型值)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objSqlConnection     ：连接对象
        '     strFieldName         ：要检索的字段名
        '     strTableName         ：要检索的表名
        '     intCodeLen           ：要获取的代码长度
        '     strPrevCodeValue     ：上级代码值
        '     blnMaxNo             ：是否获取最大序号
        '     blnFixLen            ：字段长度为固定长度
        '     strNewCode           ：新的唯一码
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getNewCode( _
            ByRef strErrMsg As String, _
            ByVal objSqlConnection As System.Data.SqlClient.SqlConnection, _
            ByVal strFieldName As String, _
            ByVal strTableName As String, _
            ByVal intCodeLen As Integer, _
            ByVal strPrevCodeValue As String, _
            ByVal blnMaxNo As Boolean, _
            ByVal blnFixLen As Boolean, _
            ByRef strNewCode As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objDataSet As System.Data.DataSet
            Dim intPosStart As Integer
            Dim intPosEnd As Integer
            Dim intPos As Integer
            Dim intStart As Integer
            Dim intEnd As Integer
            Dim intMid As Integer
            Dim strSQL As String

            Dim intPreCodeLen As Integer
            Dim intCurCodeLen As Integer

            getNewCode = False
            strNewCode = ""

            Try
                '检查
                If objSqlConnection Is Nothing Then
                    strErrMsg = "错误：未指定连接！"
                    GoTo errProc
                End If
                If strFieldName Is Nothing Then strFieldName = ""
                If strTableName Is Nothing Then strTableName = ""
                If strPrevCodeValue Is Nothing Then strPrevCodeValue = ""
                strFieldName = strFieldName.Trim()
                strTableName = strTableName.Trim()
                strPrevCodeValue = strPrevCodeValue.Trim()
                If strFieldName = "" Then
                    strErrMsg = "错误：未指定字段！"
                    GoTo errProc
                End If
                If strTableName = "" Then
                    strErrMsg = "错误：未指定表名！"
                    GoTo errProc
                End If

                '计算SQL语句
                Dim strField As String
                intPreCodeLen = strPrevCodeValue.Length
                intCurCodeLen = intCodeLen - intPreCodeLen
                If strPrevCodeValue = "" Then
                    strField = "substring(" + strFieldName + "," + (intPreCodeLen + 1).ToString() + "," + intCurCodeLen.ToString() + ")"
                    strSQL = ""
                    strSQL = strSQL + " select a." + strFieldName + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select " + strFieldName + " = convert(Integer, " + strField + ")" + vbCr
                    strSQL = strSQL + "   from " + strTableName + vbCr
                    strSQL = strSQL + " ) a" + vbCr
                    strSQL = strSQL + " group by a." + strFieldName + vbCr
                    strSQL = strSQL + " order by a." + strFieldName + vbCr
                Else
                    strField = "substring(" + strFieldName + "," + (intPreCodeLen + 1).ToString() + "," + intCurCodeLen.ToString() + ")"
                    strSQL = ""
                    strSQL = strSQL + " select a." + strFieldName + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select " + strFieldName + " = convert(Integer, " + strField + ")" + vbCr
                    strSQL = strSQL + "   from " + strTableName + vbCr
                    strSQL = strSQL + "   where " + strFieldName + " like '" + strPrevCodeValue + "%'" + vbCr
                    strSQL = strSQL + " ) a" + vbCr
                    strSQL = strSQL + " group by a." + strFieldName + vbCr
                    strSQL = strSQL + " order by a." + strFieldName + vbCr
                End If

                '获取所有代码
                If Me.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If

                '计算新代码
                With objDataSet.Tables(0)
                    '无记录
                    If .Rows.Count < 1 Then
                        strNewCode = "1"
                        GoTo normExit
                    End If

                    '检测是否最大号+1
                    intEnd = CType(.Rows(.Rows.Count - 1).Item(strFieldName), Integer)
                    If blnMaxNo = True Then
                        strNewCode = (intEnd + 1).ToString()
                        GoTo normExit
                    End If

                    '头部有空号
                    intStart = CType(.Rows(0).Item(strFieldName), Integer)
                    If intStart > 1 Then
                        strNewCode = (intStart - 1).ToString()
                        GoTo normExit
                    End If

                    '中间无空号
                    If (intEnd - intStart + 1) <= .Rows.Count Then
                        strNewCode = (intEnd + 1).ToString()
                        GoTo normExit
                    End If

                    '中间有空号
                    intPosStart = 0
                    intPosEnd = .Rows.Count - 1
                    Do While True
                        '获取中间位置的实际序号
                        intPos = CType(Fix((intPosStart + intPosEnd) / 2), Integer)
                        intMid = CType(.Rows(intPos).Item(strFieldName), Integer)

                        If (intMid - intStart + 1) <= (intPos - intPosStart + 1) Then '中间->尾有空号
                            intStart = intMid
                            intPosStart = intPos
                        Else                                                          '头->中间有空号
                            intEnd = intMid
                            intPosEnd = intPos
                        End If

                        If (intPosEnd - intPosStart) = 1 Then                         '找到空号区间
                            intStart = CType(.Rows(intPosStart).Item(strFieldName), Integer)
                            strNewCode = (intStart + 1).ToString()
                            GoTo normExit
                        End If
                    Loop
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

normExit:
            strNewCode = objPulicParameters.doFillString(strNewCode, intCurCodeLen, "0", True)
            strNewCode = strPrevCodeValue + strNewCode

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)

            getNewCode = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取新的复合分级代码的唯一码(字段值必须是数字型值)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objSqlConnection     ：连接对象
        '     strFieldName         ：要检索的字段名
        '     strTableName         ：要检索的表名
        '     intCodeLen           ：要获取的代码长度
        '     strPrevCodeValue     ：上级代码值
        '     strRelaFields        ：复合唯一的其他字段名
        '     strRelaFieldsValue   ：复合唯一的其他字段值
        '     blnMaxNo             ：是否获取最大序号
        '     blnFixLen            ：字段长度为固定长度
        '     strNewCode           ：新的唯一码
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getNewCode( _
            ByRef strErrMsg As String, _
            ByVal objSqlConnection As System.Data.SqlClient.SqlConnection, _
            ByVal strFieldName As String, _
            ByVal strTableName As String, _
            ByVal intCodeLen As Integer, _
            ByVal strPrevCodeValue As String, _
            ByVal strRelaFields As String, _
            ByVal strRelaFieldsValue As String, _
            ByVal blnMaxNo As Boolean, _
            ByVal blnFixLen As Boolean, _
            ByRef strNewCode As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objDataSet As System.Data.DataSet
            Dim intPosStart As Integer
            Dim intPosEnd As Integer
            Dim intPos As Integer
            Dim intStart As Integer
            Dim intEnd As Integer
            Dim intMid As Integer
            Dim strSQL As String

            Dim intPreCodeLen As Integer
            Dim intCurCodeLen As Integer

            getNewCode = False
            strNewCode = ""

            Try
                '检查
                If objSqlConnection Is Nothing Then
                    strErrMsg = "错误：未指定连接！"
                    GoTo errProc
                End If
                If strFieldName Is Nothing Then strFieldName = ""
                If strTableName Is Nothing Then strTableName = ""
                If strPrevCodeValue Is Nothing Then strPrevCodeValue = ""
                If strRelaFields Is Nothing Then strRelaFields = ""
                If strRelaFieldsValue Is Nothing Then strRelaFieldsValue = ""
                strFieldName = strFieldName.Trim()
                strTableName = strTableName.Trim()
                strPrevCodeValue = strPrevCodeValue.Trim()
                strRelaFields = strRelaFields.Trim()
                strRelaFieldsValue = strRelaFieldsValue.Trim()
                If strFieldName = "" Then
                    strErrMsg = "错误：未指定字段！"
                    GoTo errProc
                End If
                If strTableName = "" Then
                    strErrMsg = "错误：未指定表名！"
                    GoTo errProc
                End If

                '单项唯一
                If strRelaFields = "" Then
                    If Me.getNewCode(strErrMsg, objSqlConnection, strFieldName, strTableName, intCodeLen, strPrevCodeValue, blnMaxNo, strNewCode) = False Then
                        GoTo errProc
                    Else
                        GoTo normExit
                    End If
                End If

                '计算SQL语句
                Dim strRelaFieldName() As String
                strRelaFieldName = strRelaFields.Split(Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate.ToCharArray())
                Dim strRelaFieldValue() As String
                strRelaFieldValue = strRelaFieldsValue.Split(Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate.ToCharArray())
                If strRelaFieldName.Length <> strRelaFieldValue.Length Then
                    strErrMsg = "错误：字段名与字段值的数目不匹配！"
                    GoTo errProc
                End If
                Dim strWhere As String = ""
                Dim intCount As Integer
                Dim i As Integer
                intCount = strRelaFieldName.Length
                For i = 0 To intCount - 1 Step 1
                    If strWhere = "" Then
                        strWhere = strRelaFieldName(i) + " = '" + strRelaFieldValue(i) + "'"
                    Else
                        strWhere = strWhere + " and " + strRelaFieldName(i) + " = '" + strRelaFieldValue(i) + "'"
                    End If
                Next

                '计算SQL语句
                Dim strField As String
                intPreCodeLen = strPrevCodeValue.Length
                intCurCodeLen = intCodeLen - intPreCodeLen
                If strPrevCodeValue = "" Then
                    strField = "substring(" + strFieldName + "," + (intPreCodeLen + 1).ToString() + "," + intCurCodeLen.ToString() + ")"
                    strSQL = ""
                    strSQL = strSQL + " select a." + strFieldName + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "     select " + strFieldName + " = convert(Integer, " + strField + ")" + vbCr
                    strSQL = strSQL + "     from " + strTableName + vbCr
                    If strWhere <> "" Then
                        strSQL = strSQL + " where " + strWhere + vbCr
                    End If
                    strSQL = strSQL + " ) a" + vbCr
                    strSQL = strSQL + " group by a." + strFieldName + vbCr
                    strSQL = strSQL + " order by a." + strFieldName + vbCr
                Else
                    strField = "substring(" + strFieldName + "," + (intPreCodeLen + 1).ToString() + "," + intCurCodeLen.ToString() + ")"
                    strSQL = ""
                    strSQL = strSQL + " select a." + strFieldName + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "     select " + strFieldName + " = convert(Integer, " + strField + ")" + vbCr
                    strSQL = strSQL + "     from " + strTableName + vbCr
                    strSQL = strSQL + "     where " + strFieldName + " like '" + strPrevCodeValue + "%'" + vbCr
                    If strWhere <> "" Then
                        strSQL = strSQL + " and " + strWhere + vbCr
                    End If
                    strSQL = strSQL + " ) a" + vbCr
                    strSQL = strSQL + " group by a." + strFieldName + vbCr
                    strSQL = strSQL + " order by a." + strFieldName + vbCr
                End If

                '获取所有代码
                If Me.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If

                '计算新代码
                With objDataSet.Tables(0)
                    '无记录
                    If .Rows.Count < 1 Then
                        strNewCode = "1"
                        GoTo normExit
                    End If

                    '检测是否最大号+1
                    intEnd = CType(.Rows(.Rows.Count - 1).Item(strFieldName), Integer)
                    If blnMaxNo = True Then
                        strNewCode = (intEnd + 1).ToString()
                        GoTo normExit
                    End If

                    '头部有空号
                    intStart = CType(.Rows(0).Item(strFieldName), Integer)
                    If intStart > 1 Then
                        strNewCode = (intStart - 1).ToString()
                        GoTo normExit
                    End If

                    '中间无空号
                    If (intEnd - intStart + 1) <= .Rows.Count Then
                        strNewCode = (intEnd + 1).ToString()
                        GoTo normExit
                    End If

                    '中间有空号
                    intPosStart = 0
                    intPosEnd = .Rows.Count - 1
                    Do While True
                        '获取中间位置的实际序号
                        intPos = CType(Fix((intPosStart + intPosEnd) / 2), Integer)
                        intMid = CType(.Rows(intPos).Item(strFieldName), Integer)

                        If (intMid - intStart + 1) <= (intPos - intPosStart + 1) Then '中间->尾有空号
                            intStart = intMid
                            intPosStart = intPos
                        Else                                                          '头->中间有空号
                            intEnd = intMid
                            intPosEnd = intPos
                        End If

                        If (intPosEnd - intPosStart) = 1 Then                         '找到空号区间
                            intStart = CType(.Rows(intPosStart).Item(strFieldName), Integer)
                            strNewCode = (intStart + 1).ToString()
                            GoTo normExit
                        End If
                    Loop
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

normExit:
            strNewCode = objPulicParameters.doFillString(strNewCode, intCurCodeLen, "0", True)
            strNewCode = strPrevCodeValue + strNewCode

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)

            getNewCode = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Exit Function

        End Function




        '----------------------------------------------------------------
        ' 将以strSep分隔的字符串转换为标准SQL的字符值列表
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strSrc               ：源字符串
        '     strSep               ：源字符串的分隔符
        '     strDes               ：返回转换结果
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doConvertToSQLValueList( _
            ByRef strErrMsg As String, _
            ByVal strSrc As String, _
            ByVal strSep As String, _
            ByRef strDes As String) As Boolean

            doConvertToSQLValueList = False
            strDes = strSrc

            Try
                '检查
                If strSrc Is Nothing Then strSrc = ""
                strSrc = strSrc.Trim()
                If strSrc = "" Then Exit Try

                '分隔源字符串
                Dim strValue() As String = strSrc.Split(strSep.ToCharArray())
                If strValue.Length < 1 Then Exit Try

                '计算
                Dim strTemp As String = ""
                Dim intCount As Integer
                Dim i As Integer
                intCount = strValue.Length
                For i = 0 To intCount - 1 Step 1
                    strValue(i) = strValue(i).Trim()
                    If strTemp = "" Then
                        strTemp = "'" + strValue(i) + "'"
                    Else
                        strTemp = strTemp + "," + "'" + strValue(i) + "'"
                    End If
                Next

                '返回
                strDes = strTemp

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doConvertToSQLValueList = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 将字符串数组转换为SQL字符值列表
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strSrc               ：源字符串数组
        '     strDes               ：返回转换结果
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doConvertToSQLValueList( _
            ByRef strErrMsg As String, _
            ByVal strSrc As String(), _
            ByRef strDes As String) As Boolean

            doConvertToSQLValueList = False
            strDes = ""

            Try
                '检查
                If strSrc Is Nothing Then
                    Exit Try
                End If
                If strSrc.Length < 1 Then
                    Exit Try
                End If

                '计算
                Dim strTemp As String = ""
                Dim intCount As Integer
                Dim i As Integer
                intCount = strSrc.Length
                For i = 0 To intCount - 1 Step 1
                    If strTemp = "" Then
                        strTemp = "'" + strSrc(i).Trim + "'"
                    Else
                        strTemp = strTemp + "," + "'" + strSrc(i).Trim + "'"
                    End If
                Next

                '返回
                strDes = strTemp

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doConvertToSQLValueList = True
            Exit Function
errProc:
            Exit Function

        End Function




        '----------------------------------------------------------------
        ' 获取数据库服务器的时间
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objSqlConnection     ：连接对象
        '     objDate              ：返回数据库服务器的时间
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getServerTime( _
            ByRef strErrMsg As String, _
            ByVal objSqlConnection As System.Data.SqlClient.SqlConnection, _
            ByRef objDate As System.DateTime) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            getServerTime = False
            objDate = Nothing

            Try
                '检查
                If objSqlConnection Is Nothing Then
                    strErrMsg = "错误：未指定数据库连接！"
                    GoTo errProc
                End If

                '总体解析
                strSQL = "select getdate() "
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    Exit Try
                End If

                '重新合成
                Dim objTemp As System.DateTime
                objTemp = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item(0), objTemp)

                '返回
                objDate = objTemp

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getServerTime = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取数据库服务器的时间
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：连接用户标识
        '     strPassword          ：连接用户密码
        '     objDate              ：返回数据库服务器的时间
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getServerTime( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef objDate As System.DateTime) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            getServerTime = False
            objDate = Nothing

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "错误：未指定数据库连接用户！"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim

                '获取连接
                If Me.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '总体解析
                strSQL = "select getdate() "
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    Exit Try
                End If

                '重新合成
                Dim objTemp As System.DateTime
                objTemp = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item(0), objTemp)

                '返回
                objDate = objTemp

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getServerTime = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取数据库的GUID
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objSqlConnection     ：连接对象
        '     strGUID              ：返回GUID
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getNewGUID( _
            ByRef strErrMsg As String, _
            ByVal objSqlConnection As System.Data.SqlClient.SqlConnection, _
            ByRef strGUID As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            getNewGUID = False
            strGUID = ""

            Try
                '检查
                If objSqlConnection Is Nothing Then
                    strErrMsg = "错误：未指定数据库连接！"
                    GoTo errProc
                End If

                '总体解析
                strSQL = "select newid()"
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    strErrMsg = "错误：无法获取GUID！"
                    GoTo errProc
                End If

                '返回
                Dim objGuid As System.Guid
                Dim strTemp As String
                objGuid = CType(objDataSet.Tables(0).Rows(0).Item(0), System.Guid)
                strTemp = objGuid.ToString().ToUpper()
                strTemp = strTemp.Replace("{", "")
                strTemp = strTemp.Replace("}", "")
                strTemp = strTemp.Trim()

                strGUID = strTemp

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getNewGUID = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取数据库的GUID
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：连接用户标识
        '     strPassword          ：连接用户密码
        '     strGUID              ：返回GUID
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getNewGUID( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef strGUID As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            getNewGUID = False
            strGUID = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "错误：未指定数据库连接用户！"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim

                '获取连接
                If Me.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '总体解析
                strSQL = "select newid()"
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    strErrMsg = "错误：无法获取GUID！"
                    GoTo errProc
                End If

                '返回
                Dim objGuid As System.Guid
                Dim strTemp As String
                objGuid = CType(objDataSet.Tables(0).Rows(0).Item(0), System.Guid)
                strTemp = objGuid.ToString().ToUpper()
                strTemp = strTemp.Replace("{", "")
                strTemp = strTemp.Replace("}", "")
                strTemp = strTemp.Trim()

                strGUID = strTemp

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getNewGUID = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function




        '----------------------------------------------------------------
        ' 从指定FTP位置下载文件到指定的WEB服务器目录下的文件中
        ' 如果指定了strDesSpec，则可不输入strDesPath、strDesFile
        ' 如果未指定strDesSpec，则必须输入strDesPath
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strFTPPath           ：指定FTP位置(路径与文件名)
        '     strDesSpec           ：现有WEB服务器目录+文件(返回)
        '     strDesPath           ：WEB服务器目录(返回)
        '     strDesFile           ：WEB服务器目录下临时文件名(返回)
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doFTPDownLoadFile( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strFTPPath As String, _
            ByRef strDesSpec As String, _
            ByRef strDesPath As String, _
            ByRef strDesFile As String) As Boolean

            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objdacXitongpeizhi As New Xydc.Platform.DataAccess.dacXitongpeizhi
            Dim objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty
            Dim objBaseFTP As New Xydc.Platform.Common.Utilities.BaseFTP

            Dim strTempSpec As String
            Dim strTempPath As String
            Dim strTempFile As String

            doFTPDownLoadFile = False
            strTempFile = ""
            strDesFile = ""

            Try
                '检查
                If strFTPPath Is Nothing Then strFTPPath = ""
                strFTPPath = strFTPPath.Trim
                If strDesSpec Is Nothing Then strDesSpec = ""
                strDesSpec = strDesSpec.Trim
                If strDesPath Is Nothing Then strDesPath = ""
                strDesPath = strDesPath.Trim
                If strFTPPath = "" Then
                    strErrMsg = "错误：未指定要下载的文件！"
                    GoTo errProc
                End If
                If strDesSpec = "" And strDesPath = "" Then
                    strErrMsg = "错误：未指定文件要下载到的目录！"
                    GoTo errProc
                End If

                '获取FTP参数
                If objdacXitongpeizhi.getFtpServerParam(strErrMsg, strUserId, strPassword, objFTPProperty) = False Then
                    GoTo errProc
                End If

                '根据不同模式处理
                If strDesSpec <> "" Then
                    '从文件路径中获取文件名
                    strTempFile = objBaseLocalFile.getFileName(strDesSpec)
                    strTempPath = objBaseLocalFile.getPathName(strDesSpec)
                    strTempSpec = strDesSpec
                Else
                    '创建临时文件
                    If objBaseLocalFile.doCreateTempFile(strErrMsg, strFTPPath, True, strTempFile) = False Then
                        GoTo errProc
                    End If
                    strTempSpec = objBaseLocalFile.doMakePath(strDesPath, strTempFile)
                    strTempPath = strDesPath
                End If

                '下载处理
                Dim strUrl As String
                With objFTPProperty
                    strUrl = .getUrl(strFTPPath)
                    If objBaseFTP.doGetFile(strErrMsg, strTempSpec, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword) = False Then
                        GoTo errProc
                    End If
                End With

                '设置返回值
                If strDesSpec <> "" Then
                    strDesFile = strTempFile
                    strDesPath = strTempPath
                Else
                    strDesFile = strTempFile
                    strDesSpec = strTempSpec
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.DataAccess.dacXitongpeizhi.SafeRelease(objdacXitongpeizhi)
            Xydc.Platform.Common.Utilities.FTPProperty.SafeRelease(objFTPProperty)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)

            doFTPDownLoadFile = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.DataAccess.dacXitongpeizhi.SafeRelease(objdacXitongpeizhi)
            Xydc.Platform.Common.Utilities.FTPProperty.SafeRelease(objFTPProperty)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 将指定的本地文件上传到FTP指定文件
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strLocalFile         ：本地文件
        '     strFtpUrl            ：FTP指定文件
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doFTPUploadFile( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strLocalFile As String, _
            ByVal strFtpUrl As String) As Boolean

            Dim objdacXitongpeizhi As New Xydc.Platform.DataAccess.dacXitongpeizhi
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty
            Dim objBaseFTP As New Xydc.Platform.Common.Utilities.BaseFTP

            doFTPUploadFile = False

            Try
                '检查
                If strLocalFile Is Nothing Then strLocalFile = ""
                strLocalFile = strLocalFile.Trim
                If strFtpUrl Is Nothing Then strFtpUrl = ""
                strFtpUrl = strFtpUrl.Trim
                If strLocalFile = "" Then
                    Exit Try
                End If
                If strFtpUrl = "" Then
                    strErrMsg = "错误：未指定目标位置！"
                    GoTo errProc
                End If
                Dim blnDo As Boolean
                If objBaseLocalFile.doFileExisted(strErrMsg, strLocalFile, blnDo) = False Then
                    GoTo errProc
                End If
                If blnDo = False Then
                    strErrMsg = "错误：文件[" + strLocalFile + "]不存在！"
                    GoTo errProc
                End If

                '获取FTP参数
                If objdacXitongpeizhi.getFtpServerParam(strErrMsg, strUserId, strPassword, objFTPProperty) = False Then
                    GoTo errProc
                End If

                '上载文件
                Dim strUrl As String
                With objFTPProperty
                    strUrl = .getUrl(strFtpUrl)
                    If objBaseFTP.doPutFile(strErrMsg, strLocalFile, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword) = False Then
                        GoTo errProc
                    End If
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.DataAccess.dacXitongpeizhi.SafeRelease(objdacXitongpeizhi)
            Xydc.Platform.Common.Utilities.FTPProperty.SafeRelease(objFTPProperty)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)

            doFTPUploadFile = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.DataAccess.dacXitongpeizhi.SafeRelease(objdacXitongpeizhi)
            Xydc.Platform.Common.Utilities.FTPProperty.SafeRelease(objFTPProperty)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)
            Exit Function

        End Function

    End Class

End Namespace
