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
    ' �����ռ䣺Xydc.Platform.DataAccess
    ' ����    ��dacExcel
    '
    ' ����������
    '     �ṩ���ݼ�/����Excel֮������ݵ����뵼������
    '----------------------------------------------------------------

    Public Class dacExcel
        Implements IDisposable

        Private m_objSqlDataAdapter As System.Data.SqlClient.SqlDataAdapter
        Private m_objOdbcDataAdapter As System.Data.Odbc.OdbcDataAdapter








        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
            m_objSqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter
            m_objOdbcDataAdapter = New System.Data.Odbc.OdbcDataAdapter
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
        ' ��ȫ�ͷű�����Դ
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
        ' �����ݴ�DataTable������Excel(��֧��������)
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     objDataTable           ��Ҫ����������
        '     strExcelFile           ��������WEB�������е�Excel�ļ�·��
        '     strSheetName           �����ݵ�����strSheetName
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        ' ODBC���Ӵ�����
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
                '���
                If strExcelFile Is Nothing Then strExcelFile = ""
                strExcelFile = strExcelFile.Trim
                If strSheetName Is Nothing Then strSheetName = ""
                strSheetName = strSheetName.Trim
                If objDataTable Is Nothing Then
                    strErrMsg = "����δָ��Ҫ���������ݣ�"
                    GoTo errProc
                End If
                If strExcelFile = "" Then
                    strErrMsg = "����δָ����Excel�ļ���"
                    GoTo errProc
                End If
                Dim blnExisted As Boolean
                If objBaseLocalFile.doFileExisted(strErrMsg, strExcelFile, blnExisted) = False Then
                    GoTo errProc
                End If
                If blnExisted = False Then
                    strErrMsg = "����Excel�ļ�[" + strExcelFile + "]�����ڣ�"
                    GoTo errProc
                End If
                If strSheetName = "" Then
                    strErrMsg = "����δָ��Excel�ļ���Sheet����"
                    GoTo errProc
                End If

                'û������
                If objDataTable.DefaultView.Count < 1 Then
                    Exit Try
                End If

                '���ƥ���ֶ�
                objDataSet = New System.Data.DataSet
                'ֻ����Excel
                objOdbcConnection = New System.Data.Odbc.OdbcConnection
                objOdbcConnection.ConnectionTimeout = Xydc.Platform.Common.jsoaConfiguration.ConnectionTimeout
                objOdbcConnection.ConnectionString = "Driver={Microsoft Excel Driver (*.xls)};DBQ=" + strExcelFile + ";ReadOnly=1"
                objOdbcConnection.Open()
                '׼��OdbcCommand
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
                        strErrMsg = "����[" + strExcelFile + "]�ļ���û��Ҫ�������У�"
                        GoTo errProc
                    End If
                End With
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing
                objOdbcConnection.Close()

                'д��Excel
                objOdbcConnection.ConnectionTimeout = Xydc.Platform.Common.jsoaConfiguration.ConnectionTimeout
                objOdbcConnection.ConnectionString = "Driver={Microsoft Excel Driver (*.xls)};DBQ=" + strExcelFile + ";ReadOnly=0"
                objOdbcConnection.Open()
                '׼��OdbcCommand
                objOdbcCommand.Connection = objOdbcConnection
                objOdbcCommand.CommandType = CommandType.Text
                objOdbcCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout
                '��������
                Try
                    '׼��SQL
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
                    '������������
                    intCount = objDataTable.DefaultView.Count
                    For i = 0 To intCount - 1 Step 1
                        '׼������ֵ
                        objOdbcCommand.Parameters.Clear()
                        For j = 0 To intCountA - 1 Step 1
                            objOdbcParameter(j).Value = objDataTable.DefaultView.Item(i).Item(objFields(j))
                            objOdbcCommand.Parameters.Add(objOdbcParameter(j))
                        Next
                        objOdbcCommand.CommandText = strSQL
                        'ִ��SQL
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
        ' ��ȡģ��Excel�и�ʽSheet�е��й�����Sheet���йز���
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     objFormatSheet         ����������Sheet���йز�����Sheet
        '     intSheetCount          ��Workbook�е�Sheet��Ŀ
        '     objParamDataSet        ���������ݼ�
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
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
                '�������ݼ�
                objParamDataSet = New Xydc.Platform.Common.Data.DrdcData(Xydc.Platform.Common.Data.DrdcData.enumTableType.TY_B_DRDC_EXCELFORMAT)

                '����Excel�����ݼ���Ӧ��ϵ
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

                'д�����ݼ�����
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

                '���
                With objParamDataSet.Tables(Xydc.Platform.Common.Data.DrdcData.TABLE_TY_B_DRDC_EXCELFORMAT)
                    If .Rows.Count < 1 Then
                        strErrMsg = "����Excelģ����û�ж�������Sheet���йز�����"
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
        ' ����Cell�ж�����ֶβ���
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strCellValue           ��Cellֵ
        '     objDataSet             ��Ҫ��������ݼ�
        '     strField               �������ֶ�������
        '     intTableIndex          �������ֶ��������ݱ�����
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
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
                '����element
                Dim strElements() As String
                strElements = strCellValue.Split(Xydc.Platform.Common.Data.DrdcData.MACRO_ELEMSEP.ToCharArray)
                If strElements.Length < 3 Then
                    Exit Try
                End If

                '�����ֶ�����
                Select Case strElements(1).ToUpper
                    Case Xydc.Platform.Common.Data.DrdcData.MACRO_FIELD.ToUpper
                        '���ֶΣ�
                        Dim strProperties() As String
                        strElements(2) = strElements(2).Trim
                        strProperties = strElements(2).Split(Xydc.Platform.Common.Data.DrdcData.MACRO_PROPSEP.ToCharArray)
                        If strProperties.Length < 2 Then
                            intTableIndex = 0
                        Else
                            '����������1,2,3,...
                            'ʵ��������0,1,2,...
                            intTableIndex = objPulicParameters.getObjectValue(strProperties(0), 0) - 1
                            strField = objPulicParameters.getObjectValue(strProperties(1), "")
                        End If

                    Case Else
                End Select

                'У���ֶ�
                strField = strField.Trim
                If intTableIndex >= 0 And strField <> "" Then
                    With objDataSet.Tables(intTableIndex)
                        If .Columns(strField) Is Nothing Then
                            '��Ч�ֶ�
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
        ' �����ݴ�DataSet������Excel
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     objDataSet             ��Ҫ���������ݼ�
        '     strExcelFile           ��������WEB�������е�Excel�ļ�·��
        '     strMacroName           �������б�
        '     strMacroValue          ����ֵ�б�
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
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
                '���
                If strExcelFile Is Nothing Then strExcelFile = ""
                strExcelFile = strExcelFile.Trim
                If objDataSet Is Nothing Then
                    strErrMsg = "����δָ��Ҫ���������ݣ�"
                    GoTo errProc
                End If
                If strExcelFile = "" Then
                    strErrMsg = "����δָ����Excel�ļ���"
                    GoTo errProc
                End If
                Dim blnExisted As Boolean
                If objBaseLocalFile.doFileExisted(strErrMsg, strExcelFile, blnExisted) = False Then
                    GoTo errProc
                End If
                If blnExisted = False Then
                    strErrMsg = "����Excel�ļ�[" + strExcelFile + "]�����ڣ�"
                    GoTo errProc
                End If

                'û������
                If objDataSet.Tables.Count < 1 Then
                    Exit Try
                End If

                'װ��Excel
                objExcelFile.LoadXls(strExcelFile)

                '��ȡģ��Sheet������Ϣ
                Dim objFormatSheet As GemBox.ExcelLite.ExcelWorksheet
                Dim intSheetCount As Integer
                intSheetCount = objExcelFile.Worksheets.Count
                If intSheetCount < 2 Then
                    strErrMsg = "����[" + strExcelFile + "]�ļ���������[2]��Sheet��"
                    GoTo errProc
                End If
                objFormatSheet = objExcelFile.Worksheets(intSheetCount - 1)  '0,1,...
                If Me.getSheetParamDataSet(strErrMsg, objFormatSheet, intSheetCount, objParamDataSet) = False Then
                    GoTo errProc
                End If

                '�����������Sheet
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

                    '��ȡ������
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
                        strErrMsg = "���󣺸�ʽSheet�й�������Sheet�Ĳ������ò���ȷ��"
                        GoTo errProc
                    End If

                    '����������
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

                    '����������
                    Dim strFields(intDataCols) As String
                    Dim intTables(intDataCols) As Integer
                    i = intTitleRows
                    For j = 0 To intDataCols - 1 Step 1
                        strCellValue = objPulicParameters.getObjectValue(objDataSheet.Cells(i, j).Value, "")
                        If Me.getFieldParam(strErrMsg, strCellValue, objDataSet, strFields(j), intTables(j)) = False Then
                            GoTo errProc
                        End If
                    Next
                    '������ͬһ���ݱ�
                    Dim intCurrentIndex As Integer
                    intCurrentIndex = -1
                    For j = 0 To intDataCols - 1 Step 1
                        If intTables(j) >= 0 Then
                            If intCurrentIndex < 0 Then
                                intCurrentIndex = intTables(j)
                            Else
                                If intTables(j) <> intCurrentIndex Then
                                    strErrMsg = "���󣺱�ű���һ�£�"
                                    GoTo errProc
                                End If
                            End If
                        End If
                    Next
                    If intCurrentIndex >= objDataSet.Tables.Count Then
                        strErrMsg = "���󣺱�ų�����Χ��"
                        GoTo errProc
                    End If

                    '�������
                    With objDataSet.Tables(intCurrentIndex).DefaultView
                        '����ָ����¼��Ŀ����
                        objDataSheet.Rows(intTitleRows).InsertCopy(.Count - 1, objDataSheet.Rows(intTitleRows))

                        '�������
                        For i = 0 To .Count - 1 Step 1
                            '�����ֵ
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

                '����Excel
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
        ' ����Excel�еĺ�����
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strExcelFile           ��������WEB�������е�Excel�ļ�·��
        '     strMacroName           �������б�
        '     strMacroValue          ����ֵ�б�
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
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
                '���
                If strExcelFile Is Nothing Then strExcelFile = ""
                strExcelFile = strExcelFile.Trim
                If strExcelFile = "" Then
                    strErrMsg = "����δָ����Excel�ļ���"
                    GoTo errProc
                End If
                Dim blnExisted As Boolean
                If objBaseLocalFile.doFileExisted(strErrMsg, strExcelFile, blnExisted) = False Then
                    GoTo errProc
                End If
                If blnExisted = False Then
                    strErrMsg = "����Excel�ļ�[" + strExcelFile + "]�����ڣ�"
                    GoTo errProc
                End If

                'װ��Excel
                objExcelFile.LoadXls(strExcelFile)

                '��ȡģ��Sheet������Ϣ
                Dim objFormatSheet As GemBox.ExcelLite.ExcelWorksheet
                Dim intSheetCount As Integer
                intSheetCount = objExcelFile.Worksheets.Count
                If intSheetCount < 2 Then
                    strErrMsg = "����[" + strExcelFile + "]�ļ���������[2]��Sheet��"
                    GoTo errProc
                End If
                objFormatSheet = objExcelFile.Worksheets(intSheetCount - 1)  '0,1,...
                If Me.getSheetParamDataSet(strErrMsg, objFormatSheet, intSheetCount, objParamDataSet) = False Then
                    GoTo errProc
                End If

                '�����������Sheet
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

                    '��ȡ������
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
                        strErrMsg = "���󣺸�ʽSheet�й�������Sheet�Ĳ������ò���ȷ��"
                        GoTo errProc
                    End If

                    '����������
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

                '����Excel
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
        ' �����ݴ�Excel���뵽DataTable(��֧��������)
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strExcelFile           ���������ݵ�WEB�������е�Excel�ļ�·��
        '     strSheetName           ���������ݵ�strSheetName
        '     objDataTable           �����ص�Excel����
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        ' ODBC���Ӵ�����
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
                '���
                If strExcelFile Is Nothing Then strExcelFile = ""
                strExcelFile = strExcelFile.Trim
                If strSheetName Is Nothing Then strSheetName = ""
                strSheetName = strSheetName.Trim
                If objDataTable Is Nothing Then
                    strErrMsg = "����δָ��Ҫ���������ݣ�"
                    GoTo errProc
                End If
                If strExcelFile = "" Then
                    strErrMsg = "����δָ����Excel�ļ���"
                    GoTo errProc
                End If
                Dim blnExisted As Boolean
                If objBaseLocalFile.doFileExisted(strErrMsg, strExcelFile, blnExisted) = False Then
                    GoTo errProc
                End If
                If blnExisted = False Then
                    strErrMsg = "����Excel�ļ�[" + strExcelFile + "]�����ڣ�"
                    GoTo errProc
                End If
                If strSheetName = "" Then
                    strErrMsg = "����δָ��Excel�ļ���Sheet����"
                    GoTo errProc
                End If
                If objDataTable Is Nothing Then
                    strErrMsg = "����δָ��Ŀ�����ݸ�ʽ��"
                    GoTo errProc
                End If

                '����Excel����
                'ֻ����Excel
                objOdbcConnection = New System.Data.Odbc.OdbcConnection
                objOdbcConnection.ConnectionTimeout = Xydc.Platform.Common.jsoaConfiguration.ConnectionTimeout
                objOdbcConnection.ConnectionString = "Driver={Microsoft Excel Driver (*.xls)};DBQ=" + strExcelFile + ";ReadOnly=1"
                objOdbcConnection.Open()
                '׼��OdbcCommand
                objOdbcCommand = New System.Data.Odbc.OdbcCommand
                objOdbcCommand.Connection = objOdbcConnection
                objOdbcCommand.CommandType = CommandType.Text
                objOdbcCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout
                With Me.m_objOdbcDataAdapter
                    '׼��SQL
                    strSQL = ""
                    strSQL = strSQL & " select * from [" + strSheetName + "$]"
                    objOdbcCommand.CommandText = strSQL
                    .SelectCommand = objOdbcCommand
                    '��ȡExcel����
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
