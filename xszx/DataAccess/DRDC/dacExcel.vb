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

Imports GemBox.ExcelLite
Imports Xydc.Platform.Common
Imports Xydc.Platform.Common.Data
Imports Xydc.Platform.SystemFramework

Namespace Xydc.Platform.DataAccess

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.DataAccess
    ' 类名    ：dacExcel
    '
    ' 功能描述：
    '     提供数据集/表与Excel之间的数据导入与导出处理
    '----------------------------------------------------------------

    Public Class dacExcel
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.DataAccess.dacExcel)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub











        '----------------------------------------------------------------
        ' 将数据从DataTable导出到Excel(不支持事务处理)
        '     strErrMsg              ：如果错误，则返回错误信息
        '     objDataTable           ：要导出的数据
        '     strExcelFile           ：导出到WEB服务器中的Excel文件路径
        '     strSheetName           ：数据导出到strSheetName
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        ' ODBC连接串参数
        '     Driver={Microsoft Excel Driver (*.xls)};
        '     DBQ=xxx.xls;
        '     ReadOnly=0;
        '     UID=admin;
        '     SafeTransactions=0;
        '     DriverId=790;
        '     FIL=excel 8.0;'
        '     MaxBufferSize=2048;
        '     MaxScanRows=8;
        '     PageTimeout=5;
        '     Threads=3;
        '     UserCommitSync=Yes;
        '----------------------------------------------------------------
        Public Function doExport( _
            ByRef strErrMsg As String, _
            ByVal objDataTable As System.Data.DataTable, _
            ByVal strExcelFile As String, _
            ByVal strSheetName As String) As Boolean

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
                If strExcelFile Is Nothing Then strExcelFile = ""
                strExcelFile = strExcelFile.Trim
                If strSheetName Is Nothing Then strSheetName = ""
                strSheetName = strSheetName.Trim
                If objDataTable Is Nothing Then
                    strErrMsg = "错误：未指定要导出的数据！"
                    GoTo errProc
                End If
                If strExcelFile = "" Then
                    strErrMsg = "错误：未指定的Excel文件！"
                    GoTo errProc
                End If
                Dim blnExisted As Boolean
                If objBaseLocalFile.doFileExisted(strErrMsg, strExcelFile, blnExisted) = False Then
                    GoTo errProc
                End If
                If blnExisted = False Then
                    strErrMsg = "错误：Excel文件[" + strExcelFile + "]不存在！"
                    GoTo errProc
                End If
                If strSheetName = "" Then
                    strErrMsg = "错误：未指定Excel文件的Sheet名！"
                    GoTo errProc
                End If

                '没有数据
                If objDataTable.DefaultView.Count < 1 Then
                    Exit Try
                End If

                '检查匹配字段
                objDataSet = New System.Data.DataSet
                '只读打开Excel
                objOdbcConnection = New System.Data.Odbc.OdbcConnection
                objOdbcConnection.ConnectionTimeout = Xydc.Platform.Common.jsoaConfiguration.ConnectionTimeout
                objOdbcConnection.ConnectionString = "Driver={Microsoft Excel Driver (*.xls)};DBQ=" + strExcelFile + ";ReadOnly=1"
                objOdbcConnection.Open()
                '准备OdbcCommand
                objOdbcCommand = New System.Data.Odbc.OdbcCommand
                objOdbcCommand.Connection = objOdbcConnection
                objOdbcCommand.CommandType = CommandType.Text
                objOdbcCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout
                With Me.m_objOdbcDataAdapter
                    strSQL = ""
                    strSQL = strSQL & " select top 1 * from [" + strSheetName + "$]"
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
                        strErrMsg = "错误：[" + strExcelFile + "]文件中没有要导出的列！"
                        GoTo errProc
                    End If
                End With
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing
                objOdbcConnection.Close()

                '写打开Excel
                objOdbcConnection.ConnectionTimeout = Xydc.Platform.Common.jsoaConfiguration.ConnectionTimeout
                objOdbcConnection.ConnectionString = "Driver={Microsoft Excel Driver (*.xls)};DBQ=" + strExcelFile + ";ReadOnly=0"
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
                    strSQL = "insert into [" + strSheetName + "$] (" + strFieldList + ") values (" + strValueList + ")"
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
        ' 获取模版Excel中格式Sheet中的有关数据Sheet的有关参数
        '     strErrMsg              ：如果错误，则返回错误信息
        '     objFormatSheet         ：包含数据Sheet的有关参数的Sheet
        '     intSheetCount          ：Workbook中的Sheet数目
        '     objParamDataSet        ：返回数据集
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Private Function getSheetParamDataSet( _
            ByRef strErrMsg As String, _
            ByVal objFormatSheet As GemBox.ExcelLite.ExcelWorksheet, _
            ByVal intSheetCount As Integer, _
            ByRef objParamDataSet As Xydc.Platform.Common.Data.DrdcData) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            getSheetParamDataSet = False
            objParamDataSet = Nothing
            strErrMsg = ""

            Try
                '创建数据集
                objParamDataSet = New Xydc.Platform.Common.Data.DrdcData(Xydc.Platform.Common.Data.DrdcData.enumTableType.TY_B_DRDC_EXCELFORMAT)

                '分析Excel与数据集对应关系
                Dim objDataRow As System.Data.DataRow
                Dim strHeadIndex(3) As String
                Dim strCellValue As String
                Dim intCellValue As Integer
                Dim i As Integer
                Dim j As Integer
                For j = 0 To 2 Step 1
                    strCellValue = objPulicParameters.getObjectValue(objFormatSheet.Cells(0, j).Value, "")
                    strCellValue = strCellValue.ToUpper
                    Select Case strCellValue
                        Case Xydc.Platform.Common.Data.DrdcData.FIELD_TY_B_DRDC_EXCELFORMAT_DATASHEETNAME.ToUpper, _
                            Xydc.Platform.Common.Data.DrdcData.FIELD_TY_B_DRDC_EXCELFORMAT_TITLEROWS.ToUpper, _
                            Xydc.Platform.Common.Data.DrdcData.FIELD_TY_B_DRDC_EXCELFORMAT_DATACOLS.ToUpper
                            strHeadIndex(j) = strCellValue
                        Case Else
                            strHeadIndex(j) = ""
                    End Select
                Next

                '写入数据集数据
                For i = 0 To intSheetCount - 2 Step 1
                    With objParamDataSet.Tables(Xydc.Platform.Common.Data.DrdcData.TABLE_TY_B_DRDC_EXCELFORMAT)
                        objDataRow = .NewRow()
                    End With
                    For j = 0 To 2 Step 1
                        Select Case strHeadIndex(j)
                            Case Xydc.Platform.Common.Data.DrdcData.FIELD_TY_B_DRDC_EXCELFORMAT_DATASHEETNAME.ToUpper
                                strCellValue = objPulicParameters.getObjectValue(objFormatSheet.Cells(i + 1, j).Value, "")
                                objDataRow.Item(strHeadIndex(j)) = strCellValue
                            Case Xydc.Platform.Common.Data.DrdcData.FIELD_TY_B_DRDC_EXCELFORMAT_TITLEROWS.ToUpper
                                intCellValue = objPulicParameters.getObjectValue(objFormatSheet.Cells(i + 1, j).Value, 0)
                                objDataRow.Item(strHeadIndex(j)) = intCellValue
                            Case Xydc.Platform.Common.Data.DrdcData.FIELD_TY_B_DRDC_EXCELFORMAT_DATACOLS.ToUpper
                                intCellValue = objPulicParameters.getObjectValue(objFormatSheet.Cells(i + 1, j).Value, 0)
                                objDataRow.Item(strHeadIndex(j)) = intCellValue
                            Case Else
                        End Select
                    Next
                    With objParamDataSet.Tables(Xydc.Platform.Common.Data.DrdcData.TABLE_TY_B_DRDC_EXCELFORMAT)
                        .Rows.Add(objDataRow)
                    End With
                Next

                '检查
                With objParamDataSet.Tables(Xydc.Platform.Common.Data.DrdcData.TABLE_TY_B_DRDC_EXCELFORMAT)
                    If .Rows.Count < 1 Then
                        strErrMsg = "错误：Excel模版中没有定义数据Sheet的有关参数！"
                        GoTo errProc
                    End If
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            getSheetParamDataSet = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Data.DrdcData.SafeRelease(objParamDataSet)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 解析Cell中定义的字段参数
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strCellValue           ：Cell值
        '     objDataSet             ：要输出的数据集
        '     strField               ：返回字段名参数
        '     intTableIndex          ：返回字段所属数据表索引
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Private Function getFieldParam( _
            ByRef strErrMsg As String, _
            ByVal strCellValue As String, _
            ByVal objDataSet As System.Data.DataSet, _
            ByRef strField As String, _
            ByRef intTableIndex As Integer) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            getFieldParam = False
            strErrMsg = ""
            strField = ""
            intTableIndex = -1

            Try
                '分析element
                Dim strElements() As String
                strElements = strCellValue.Split(Xydc.Platform.Common.Data.DrdcData.MACRO_ELEMSEP.ToCharArray)
                If strElements.Length < 3 Then
                    Exit Try
                End If

                '分析字段属性
                Select Case strElements(1).ToUpper
                    Case Xydc.Platform.Common.Data.DrdcData.MACRO_FIELD.ToUpper
                        '是字段！
                        Dim strProperties() As String
                        strElements(2) = strElements(2).Trim
                        strProperties = strElements(2).Split(Xydc.Platform.Common.Data.DrdcData.MACRO_PROPSEP.ToCharArray)
                        If strProperties.Length < 2 Then
                            intTableIndex = 0
                        Else
                            '定义索引从1,2,3,...
                            '实际索引从0,1,2,...
                            intTableIndex = objPulicParameters.getObjectValue(strProperties(0), 0) - 1
                            strField = objPulicParameters.getObjectValue(strProperties(1), "")
                        End If

                    Case Else
                End Select

                '校验字段
                strField = strField.Trim
                If intTableIndex >= 0 And strField <> "" Then
                    With objDataSet.Tables(intTableIndex)
                        If .Columns(strField) Is Nothing Then
                            '无效字段
                            intTableIndex = -1
                            strField = ""
                        End If
                    End With
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            getFieldParam = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 将数据从DataSet导出到Excel
        '     strErrMsg              ：如果错误，则返回错误信息
        '     objDataSet             ：要导出的数据集
        '     strExcelFile           ：导出到WEB服务器中的Excel文件路径
        '     strMacroName           ：宏名列表
        '     strMacroValue          ：宏值列表
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function doExport( _
            ByRef strErrMsg As String, _
            ByVal objDataSet As System.Data.DataSet, _
            ByVal strExcelFile As String, _
            Optional ByVal strMacroName As String = "", _
            Optional ByVal strMacroValue As String = "") As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objParamDataSet As Xydc.Platform.Common.Data.DrdcData

            Dim objExcelFile As New GemBox.ExcelLite.ExcelFile

            doExport = False
            strErrMsg = ""

            Try
                '检查
                If strExcelFile Is Nothing Then strExcelFile = ""
                strExcelFile = strExcelFile.Trim
                If objDataSet Is Nothing Then
                    strErrMsg = "错误：未指定要导出的数据！"
                    GoTo errProc
                End If
                If strExcelFile = "" Then
                    strErrMsg = "错误：未指定的Excel文件！"
                    GoTo errProc
                End If
                Dim blnExisted As Boolean
                If objBaseLocalFile.doFileExisted(strErrMsg, strExcelFile, blnExisted) = False Then
                    GoTo errProc
                End If
                If blnExisted = False Then
                    strErrMsg = "错误：Excel文件[" + strExcelFile + "]不存在！"
                    GoTo errProc
                End If

                '没有数据
                If objDataSet.Tables.Count < 1 Then
                    Exit Try
                End If

                '装载Excel
                objExcelFile.LoadXls(strExcelFile)

                '获取模版Sheet参数信息
                Dim objFormatSheet As GemBox.ExcelLite.ExcelWorksheet
                Dim intSheetCount As Integer
                intSheetCount = objExcelFile.Worksheets.Count
                If intSheetCount < 2 Then
                    strErrMsg = "错误：[" + strExcelFile + "]文件中至少有[2]个Sheet！"
                    GoTo errProc
                End If
                objFormatSheet = objExcelFile.Worksheets(intSheetCount - 1)  '0,1,...
                If Me.getSheetParamDataSet(strErrMsg, objFormatSheet, intSheetCount, objParamDataSet) = False Then
                    GoTo errProc
                End If

                '逐个处理数据Sheet
                Dim objDataSheet As GemBox.ExcelLite.ExcelWorksheet
                Dim strCellValue As String
                Dim strSheetName As String
                Dim intSheetIndex As Integer
                Dim intTitleRows As Integer
                Dim intDataCols As Integer
                Dim strTemp As String
                Dim i As Integer
                Dim j As Integer
                Dim k As Integer
                For intSheetIndex = 0 To intSheetCount - 2 Step 1
                    objDataSheet = objExcelFile.Worksheets(intSheetIndex)

                    '获取数据列
                    intTitleRows = 0
                    intDataCols = 0
                    With objParamDataSet.Tables(Xydc.Platform.Common.Data.DrdcData.TABLE_TY_B_DRDC_EXCELFORMAT)
                        For i = 0 To .Rows.Count - 1 Step 1
                            strSheetName = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.DrdcData.FIELD_TY_B_DRDC_EXCELFORMAT_DATASHEETNAME), "")
                            strSheetName = strSheetName.ToUpper
                            If strSheetName = objDataSheet.Name.ToUpper Then
                                intTitleRows = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.DrdcData.FIELD_TY_B_DRDC_EXCELFORMAT_TITLEROWS), 0)
                                intDataCols = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.DrdcData.FIELD_TY_B_DRDC_EXCELFORMAT_DATACOLS), 0)
                                Exit For
                            End If
                        Next
                    End With
                    If intTitleRows <= 0 Or intDataCols <= 0 Then
                        strErrMsg = "错误：格式Sheet中关于数据Sheet的参数设置不正确！"
                        GoTo errProc
                    End If

                    '解析标题区
                    If strMacroName <> "" Then
                        Dim strMacroNameArray() As String = strMacroName.Split(Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate.ToCharArray)
                        Dim strMacroValueArray() As String = strMacroValue.Split(Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate.ToCharArray)
                        For i = 0 To intTitleRows - 1 Step 1
                            For j = 0 To intDataCols - 1 Step 1
                                For k = 0 To strMacroNameArray.Length - 1 Step 1
                                    strTemp = objPulicParameters.getObjectValue(objDataSheet.Cells(i, j).Value, "")
                                    If strTemp <> "" Then
                                        objDataSheet.Cells(i, j).Value = strTemp.Replace(strMacroNameArray(k), strMacroValueArray(k))
                                    End If
                                Next
                            Next
                        Next
                    End If

                    '解析数据列
                    Dim strFields(intDataCols) As String
                    Dim intTables(intDataCols) As Integer
                    i = intTitleRows
                    For j = 0 To intDataCols - 1 Step 1
                        strCellValue = objPulicParameters.getObjectValue(objDataSheet.Cells(i, j).Value, "")
                        If Me.getFieldParam(strErrMsg, strCellValue, objDataSet, strFields(j), intTables(j)) = False Then
                            GoTo errProc
                        End If
                    Next
                    '必须是同一数据表
                    Dim intCurrentIndex As Integer
                    intCurrentIndex = -1
                    For j = 0 To intDataCols - 1 Step 1
                        If intTables(j) >= 0 Then
                            If intCurrentIndex < 0 Then
                                intCurrentIndex = intTables(j)
                            Else
                                If intTables(j) <> intCurrentIndex Then
                                    strErrMsg = "错误：表号必须一致！"
                                    GoTo errProc
                                End If
                            End If
                        End If
                    Next
                    If intCurrentIndex >= objDataSet.Tables.Count Then
                        strErrMsg = "错误：表号超出范围！"
                        GoTo errProc
                    End If

                    '输出数据
                    With objDataSet.Tables(intCurrentIndex).DefaultView
                        '复制指定记录数目的行
                        objDataSheet.Rows(intTitleRows).InsertCopy(.Count - 1, objDataSheet.Rows(intTitleRows))

                        '输出数据
                        For i = 0 To .Count - 1 Step 1
                            '输出数值
                            For j = 0 To intDataCols - 1 Step 1
                                If strFields(j) <> "" Then
                                    Select Case .Item(i).Item(strFields(j)).GetType.FullName.ToString.ToUpper
                                        Case "System.DateTime".ToUpper
                                            objDataSheet.Cells(intTitleRows + i, j).Value = objPulicParameters.getObjectValue(.Item(i).Item(strFields(j)), "", "yyyy-MM-dd HH:mm:ss")
                                        Case Else
                                            objDataSheet.Cells(intTitleRows + i, j).Value = .Item(i).Item(strFields(j))
                                    End Select
                                End If
                            Next
                        Next
                    End With
                Next

                '保存Excel
                objExcelFile.SaveXls(strExcelFile)

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Data.DrdcData.SafeRelease(objParamDataSet)
            objExcelFile = Nothing

            doExport = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Data.DrdcData.SafeRelease(objParamDataSet)
            objExcelFile = Nothing
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 解析Excel中的宏数据
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strExcelFile           ：导出到WEB服务器中的Excel文件路径
        '     strMacroName           ：宏名列表
        '     strMacroValue          ：宏值列表
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function doExport( _
            ByRef strErrMsg As String, _
            ByVal strExcelFile As String, _
            Optional ByVal strMacroName As String = "", _
            Optional ByVal strMacroValue As String = "") As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objParamDataSet As Xydc.Platform.Common.Data.DrdcData

            Dim objExcelFile As New GemBox.ExcelLite.ExcelFile

            doExport = False
            strErrMsg = ""

            Try
                '检查
                If strExcelFile Is Nothing Then strExcelFile = ""
                strExcelFile = strExcelFile.Trim
                If strExcelFile = "" Then
                    strErrMsg = "错误：未指定的Excel文件！"
                    GoTo errProc
                End If
                Dim blnExisted As Boolean
                If objBaseLocalFile.doFileExisted(strErrMsg, strExcelFile, blnExisted) = False Then
                    GoTo errProc
                End If
                If blnExisted = False Then
                    strErrMsg = "错误：Excel文件[" + strExcelFile + "]不存在！"
                    GoTo errProc
                End If

                '装载Excel
                objExcelFile.LoadXls(strExcelFile)

                '获取模版Sheet参数信息
                Dim objFormatSheet As GemBox.ExcelLite.ExcelWorksheet
                Dim intSheetCount As Integer
                intSheetCount = objExcelFile.Worksheets.Count
                If intSheetCount < 2 Then
                    strErrMsg = "错误：[" + strExcelFile + "]文件中至少有[2]个Sheet！"
                    GoTo errProc
                End If
                objFormatSheet = objExcelFile.Worksheets(intSheetCount - 1)  '0,1,...
                If Me.getSheetParamDataSet(strErrMsg, objFormatSheet, intSheetCount, objParamDataSet) = False Then
                    GoTo errProc
                End If

                '逐个处理数据Sheet
                Dim objDataSheet As GemBox.ExcelLite.ExcelWorksheet
                Dim strCellValue As String
                Dim strSheetName As String
                Dim intSheetIndex As Integer
                Dim intTitleRows As Integer
                Dim intDataCols As Integer
                Dim strTemp As String
                Dim i As Integer
                Dim j As Integer
                Dim k As Integer
                For intSheetIndex = 0 To intSheetCount - 2 Step 1
                    objDataSheet = objExcelFile.Worksheets(intSheetIndex)

                    '获取数据列
                    intTitleRows = 0
                    intDataCols = 0
                    With objParamDataSet.Tables(Xydc.Platform.Common.Data.DrdcData.TABLE_TY_B_DRDC_EXCELFORMAT)
                        For i = 0 To .Rows.Count - 1 Step 1
                            strSheetName = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.DrdcData.FIELD_TY_B_DRDC_EXCELFORMAT_DATASHEETNAME), "")
                            strSheetName = strSheetName.ToUpper
                            If strSheetName = objDataSheet.Name.ToUpper Then
                                intTitleRows = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.DrdcData.FIELD_TY_B_DRDC_EXCELFORMAT_TITLEROWS), 0)
                                intDataCols = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.DrdcData.FIELD_TY_B_DRDC_EXCELFORMAT_DATACOLS), 0)
                                Exit For
                            End If
                        Next
                    End With
                    If intTitleRows <= 0 Or intDataCols <= 0 Then
                        strErrMsg = "错误：格式Sheet中关于数据Sheet的参数设置不正确！"
                        GoTo errProc
                    End If

                    '解析标题区
                    If strMacroName <> "" Then
                        Dim strMacroNameArray() As String = strMacroName.Split(Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate.ToCharArray)
                        Dim strMacroValueArray() As String = strMacroValue.Split(Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate.ToCharArray)
                        For i = 0 To intTitleRows - 1 Step 1
                            For j = 0 To intDataCols - 1 Step 1
                                For k = 0 To strMacroNameArray.Length - 1 Step 1
                                    strTemp = objPulicParameters.getObjectValue(objDataSheet.Cells(i, j).Value, "")
                                    If strTemp <> "" Then
                                        objDataSheet.Cells(i, j).Value = strTemp.Replace(strMacroNameArray(k), strMacroValueArray(k))
                                    End If
                                Next
                            Next
                        Next
                    End If
                Next

                '保存Excel
                objExcelFile.SaveXls(strExcelFile)

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Data.DrdcData.SafeRelease(objParamDataSet)
            objExcelFile = Nothing

            doExport = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Data.DrdcData.SafeRelease(objParamDataSet)
            objExcelFile = Nothing
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 将数据从Excel导入到DataTable(不支持事务处理)
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strExcelFile           ：导入数据的WEB服务器中的Excel文件路径
        '     strSheetName           ：导入数据的strSheetName
        '     objDataTable           ：返回的Excel数据
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        ' ODBC连接串参数
        '     Driver={Microsoft Excel Driver (*.xls)};
        '     DBQ=xxx.xls;
        '     ReadOnly=0;
        '     UID=admin;
        '     SafeTransactions=0;
        '     DriverId=790;
        '     FIL=excel 8.0;'
        '     MaxBufferSize=2048;
        '     MaxScanRows=8;
        '     PageTimeout=5;
        '     Threads=3;
        '     UserCommitSync=Yes;
        '----------------------------------------------------------------
        Public Function doImport( _
            ByRef strErrMsg As String, _
            ByVal strExcelFile As String, _
            ByVal strSheetName As String, _
            ByRef objDataTable As System.Data.DataTable) As Boolean

            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile

            Dim objOdbcConnection As System.Data.Odbc.OdbcConnection
            Dim objOdbcCommand As System.Data.Odbc.OdbcCommand
            Dim strSQL As String

            doImport = False
            strErrMsg = ""

            Try
                '检查
                If strExcelFile Is Nothing Then strExcelFile = ""
                strExcelFile = strExcelFile.Trim
                If strSheetName Is Nothing Then strSheetName = ""
                strSheetName = strSheetName.Trim
                If objDataTable Is Nothing Then
                    strErrMsg = "错误：未指定要导出的数据！"
                    GoTo errProc
                End If
                If strExcelFile = "" Then
                    strErrMsg = "错误：未指定的Excel文件！"
                    GoTo errProc
                End If
                Dim blnExisted As Boolean
                If objBaseLocalFile.doFileExisted(strErrMsg, strExcelFile, blnExisted) = False Then
                    GoTo errProc
                End If
                If blnExisted = False Then
                    strErrMsg = "错误：Excel文件[" + strExcelFile + "]不存在！"
                    GoTo errProc
                End If
                If strSheetName = "" Then
                    strErrMsg = "错误：未指定Excel文件的Sheet名！"
                    GoTo errProc
                End If
                If objDataTable Is Nothing Then
                    strErrMsg = "错误：未指定目标数据格式！"
                    GoTo errProc
                End If

                '读入Excel数据
                '只读打开Excel
                objOdbcConnection = New System.Data.Odbc.OdbcConnection
                objOdbcConnection.ConnectionTimeout = Xydc.Platform.Common.jsoaConfiguration.ConnectionTimeout
                objOdbcConnection.ConnectionString = "Driver={Microsoft Excel Driver (*.xls)};DBQ=" + strExcelFile + ";ReadOnly=1"
                objOdbcConnection.Open()
                '准备OdbcCommand
                objOdbcCommand = New System.Data.Odbc.OdbcCommand
                objOdbcCommand.Connection = objOdbcConnection
                objOdbcCommand.CommandType = CommandType.Text
                objOdbcCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout
                With Me.m_objOdbcDataAdapter
                    '准备SQL
                    strSQL = ""
                    strSQL = strSQL & " select * from [" + strSheetName + "$]"
                    objOdbcCommand.CommandText = strSQL
                    .SelectCommand = objOdbcCommand
                    '获取Excel数据
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
