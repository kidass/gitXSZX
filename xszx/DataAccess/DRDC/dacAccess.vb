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
Imports System.Data.Odbc
Imports System.Type

Imports Xydc.Platform.Common
Imports Xydc.Platform.Common.Data
Imports Xydc.Platform.SystemFramework

Namespace Xydc.Platform.DataAccess

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.DataAccess
    ' 类名    ：dacAccess
    '
    ' 功能描述：
    '     提供数据集/表与Access之间的数据导入与导出处理
    '----------------------------------------------------------------

    Public Class dacAccess
        Implements IDisposable

        Private m_objSqlDataAdapter As System.Data.SqlClient.SqlDataAdapter
        Private m_objOdbcDataAdapter As System.Data.Odbc.OdbcDataAdapter








        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
            m_objSqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter
            m_objOdbcDataAdapter = New System.Data.Odbc.OdbcDataAdapter
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
            If Not (m_objSqlDataAdapter Is Nothing) Then
                m_objSqlDataAdapter.Dispose()
                m_objSqlDataAdapter = Nothing
            End If
            If Not (m_objOdbcDataAdapter Is Nothing) Then
                m_objOdbcDataAdapter.Dispose()
                m_objOdbcDataAdapter = Nothing
            End If
        End Sub

        '----------------------------------------------------------------
        ' 安全释放本身资源
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.DataAccess.dacAccess)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub










        '----------------------------------------------------------------
        ' 将数据从DataTable导出到Access(不支持事务处理)
        '     strErrMsg              ：如果错误，则返回错误信息
        '     objDataTable           ：要导出的数据
        '     strAccessFile          ：导出到WEB服务器中的Access文件路径
        '     strTableName           ：数据导出到strTableName
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        ' ODBC连接串参数
        '     DBQ=Access文件完整路径;
        '     Driver={Microsoft Access Driver (*.mdb)};
        '     DriverId=281;
        '     FIL=MS Access;
        '     MaxBufferSize=2048;
        '     PageTimeout=5;
        '     UID=admin;
        '     PWD=;
        '     ReadOnly=0/1;
        '     SafeTransactions=0;
        '----------------------------------------------------------------
        Public Function doExport( _
            ByRef strErrMsg As String, _
            ByVal objDataTable As System.Data.DataTable, _
            ByVal strAccessFile As String, _
            ByVal strTableName As String) As Boolean

            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile

            Dim objOdbcConnection As System.Data.Odbc.OdbcConnection
            Dim objOdbcCommand As System.Data.Odbc.OdbcCommand
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            Dim objFields As New System.Collections.Specialized.NameValueCollection
            Dim intCountA As Integer
            Dim intCount As Integer
            Dim strFieldA As String
            Dim strField As String
            Dim intFind As Integer
            Dim i As Integer
            Dim j As Integer

            doExport = False
            strErrMsg = ""

            Try
                '检查
                If strAccessFile Is Nothing Then strAccessFile = ""
                strAccessFile = strAccessFile.Trim
                If strTableName Is Nothing Then strTableName = ""
                strTableName = strTableName.Trim
                If objDataTable Is Nothing Then
                    strErrMsg = "错误：未指定要导出的数据！"
                    GoTo errProc
                End If
                If strAccessFile = "" Then
                    strErrMsg = "错误：未指定的Access文件！"
                    GoTo errProc
                End If
                Dim blnExisted As Boolean
                If objBaseLocalFile.doFileExisted(strErrMsg, strAccessFile, blnExisted) = False Then
                    GoTo errProc
                End If
                If blnExisted = False Then
                    strErrMsg = "错误：Access文件[" + strAccessFile + "]不存在！"
                    GoTo errProc
                End If
                If strTableName = "" Then
                    strErrMsg = "错误：未指定Access的表名！"
                    GoTo errProc
                End If

                '没有数据
                If objDataTable.DefaultView.Count < 1 Then
                    Exit Try
                End If

                '检查匹配字段
                objDataSet = New System.Data.DataSet
                '只读打开Access
                objOdbcConnection = New System.Data.Odbc.OdbcConnection
                objOdbcConnection.ConnectionTimeout = Xydc.Platform.Common.jsoaConfiguration.ConnectionTimeout
                objOdbcConnection.ConnectionString = "Driver={Microsoft Access Driver (*.mdb)};DBQ=" + strAccessFile + ";ReadOnly=1;UID=admin;PWD=;"
                objOdbcConnection.Open()
                '准备OdbcCommand
                objOdbcCommand = New System.Data.Odbc.OdbcCommand
                objOdbcCommand.Connection = objOdbcConnection
                objOdbcCommand.CommandType = CommandType.Text
                objOdbcCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout
                With Me.m_objOdbcDataAdapter
                    strSQL = ""
                    strSQL = strSQL & " select top 1 * from " + strTableName + ""
                    objOdbcCommand.CommandText = strSQL
                    .SelectCommand = objOdbcCommand
                    .Fill(objDataSet)
                End With
                With objDataSet.Tables(0)
                    intCount = .Columns.Count
                    intFind = 0
                    For i = 0 To intCount - 1 Step 1
                        strField = .Columns(i).ColumnName.ToUpper
                        intCountA = objDataTable.Columns.Count
                        For j = 0 To intCountA - 1 Step 1
                            strFieldA = objDataTable.Columns(j).ColumnName.ToUpper
                            If strField = strFieldA Then
                                objFields.Add(strField, strField)
                                intFind += 1
                                Exit For
                            End If
                        Next
                    Next
                    If intFind < 1 Then
                        strErrMsg = "错误：[" + strAccessFile + "]文件中没有要导出的列！"
                        GoTo errProc
                    End If
                End With
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing
                objOdbcConnection.Close()

                '写打开Access
                objOdbcConnection.ConnectionTimeout = Xydc.Platform.Common.jsoaConfiguration.ConnectionTimeout
                objOdbcConnection.ConnectionString = "Driver={Microsoft Access Driver (*.mdb)};DBQ=" + strAccessFile + ";ReadOnly=0;UID=admin;PWD=;"
                objOdbcConnection.Open()
                '准备OdbcCommand
                objOdbcCommand.Connection = objOdbcConnection
                objOdbcCommand.CommandType = CommandType.Text
                objOdbcCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout
                '导出数据
                Try
                    '准备SQL
                    intCountA = objFields.Count
                    Dim objOdbcParameter(intCountA) As System.Data.Odbc.OdbcParameter
                    Dim strFieldList As String = ""
                    Dim strValueList As String = ""
                    For j = 0 To intCountA - 1 Step 1
                        If strFieldList = "" Then
                            strFieldList = objFields(j)
                            strValueList = "?"
                        Else
                            strFieldList = strFieldList + "," + objFields(j)
                            strValueList = strValueList + "," + "?"
                        End If
                        objOdbcParameter(j) = New System.Data.Odbc.OdbcParameter
                    Next
                    strSQL = "insert into " + strTableName + " (" + strFieldList + ") values (" + strValueList + ")"
                    '逐条导出数据
                    intCount = objDataTable.DefaultView.Count
                    For i = 0 To intCount - 1 Step 1
                        '准备参数值
                        objOdbcCommand.Parameters.Clear()
                        For j = 0 To intCountA - 1 Step 1
                            objOdbcParameter(j).Value = objDataTable.DefaultView.Item(i).Item(objFields(j))
                            objOdbcCommand.Parameters.Add(objOdbcParameter(j))
                        Next
                        objOdbcCommand.CommandText = strSQL
                        '执行SQL
                        objOdbcCommand.ExecuteNonQuery()
                    Next
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objOdbcConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objOdbcCommand)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFields)

            doExport = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objOdbcConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objOdbcCommand)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFields)
            Exit Function
        End Function

        '----------------------------------------------------------------
        ' 将数据从Access导入到DataTable(不支持事务处理)
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strAccessFile          ：导入数据的WEB服务器中的Access文件路径
        '     strTableName           ：导入数据的strTableName
        '     objDataTable           ：返回的Excel数据
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        ' ODBC连接串参数
        '     DBQ=Access文件完整路径;
        '     Driver={Microsoft Access Driver (*.mdb)};
        '     DriverId=281;
        '     FIL=MS Access;
        '     MaxBufferSize=2048;
        '     PageTimeout=5;
        '     UID=admin;
        '     PWD=;
        '     ReadOnly=0/1;
        '     SafeTransactions=0;
        '----------------------------------------------------------------
        Public Function doImport( _
            ByRef strErrMsg As String, _
            ByVal strAccessFile As String, _
            ByVal strTableName As String, _
            ByRef objDataTable As System.Data.DataTable) As Boolean

            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile

            Dim objOdbcConnection As System.Data.Odbc.OdbcConnection
            Dim objOdbcCommand As System.Data.Odbc.OdbcCommand
            Dim strSQL As String

            doImport = False
            strErrMsg = ""

            Try
                '检查
                If strAccessFile Is Nothing Then strAccessFile = ""
                strAccessFile = strAccessFile.Trim
                If strTableName Is Nothing Then strTableName = ""
                strTableName = strTableName.Trim
                If objDataTable Is Nothing Then
                    strErrMsg = "错误：未指定要导出的数据！"
                    GoTo errProc
                End If
                If strAccessFile = "" Then
                    strErrMsg = "错误：未指定的Access文件！"
                    GoTo errProc
                End If
                Dim blnExisted As Boolean
                If objBaseLocalFile.doFileExisted(strErrMsg, strAccessFile, blnExisted) = False Then
                    GoTo errProc
                End If
                If blnExisted = False Then
                    strErrMsg = "错误：Access文件[" + strAccessFile + "]不存在！"
                    GoTo errProc
                End If
                If strTableName = "" Then
                    strErrMsg = "错误：未指定Access的表名！"
                    GoTo errProc
                End If
                If objDataTable Is Nothing Then
                    strErrMsg = "错误：未指定目标数据格式！"
                    GoTo errProc
                End If

                '读入Access数据
                '只读打开Access
                objOdbcConnection = New System.Data.Odbc.OdbcConnection
                objOdbcConnection.ConnectionTimeout = Xydc.Platform.Common.jsoaConfiguration.ConnectionTimeout
                objOdbcConnection.ConnectionString = "Driver={Microsoft Access Driver (*.mdb)};DBQ=" + strAccessFile + ";ReadOnly=1;UID=admin;PWD=;"
                objOdbcConnection.Open()
                '准备OdbcCommand
                objOdbcCommand = New System.Data.Odbc.OdbcCommand
                objOdbcCommand.Connection = objOdbcConnection
                objOdbcCommand.CommandType = CommandType.Text
                objOdbcCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout
                With Me.m_objOdbcDataAdapter
                    '准备SQL
                    strSQL = ""
                    strSQL = strSQL & " select * from " + strTableName + ""
                    objOdbcCommand.CommandText = strSQL
                    .SelectCommand = objOdbcCommand
                    '获取Access数据
                    .Fill(objDataTable)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objOdbcConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objOdbcCommand)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)

            doImport = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objOdbcConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objOdbcCommand)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Exit Function

        End Function

    End Class

End Namespace
