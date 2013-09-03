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
    ' �����ռ䣺Xydc.Platform.DataAccess
    ' ����    ��dacAccess
    '
    ' ����������
    '     �ṩ���ݼ�/����Access֮������ݵ����뵼������
    '----------------------------------------------------------------

    Public Class dacAccess
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
        ' �����ݴ�DataTable������Access(��֧��������)
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     objDataTable           ��Ҫ����������
        '     strAccessFile          ��������WEB�������е�Access�ļ�·��
        '     strTableName           �����ݵ�����strTableName
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        ' ODBC���Ӵ�����
        '     DBQ=Access�ļ�����·��;
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
                '���
                If strAccessFile Is Nothing Then strAccessFile = ""
                strAccessFile = strAccessFile.Trim
                If strTableName Is Nothing Then strTableName = ""
                strTableName = strTableName.Trim
                If objDataTable Is Nothing Then
                    strErrMsg = "����δָ��Ҫ���������ݣ�"
                    GoTo errProc
                End If
                If strAccessFile = "" Then
                    strErrMsg = "����δָ����Access�ļ���"
                    GoTo errProc
                End If
                Dim blnExisted As Boolean
                If objBaseLocalFile.doFileExisted(strErrMsg, strAccessFile, blnExisted) = False Then
                    GoTo errProc
                End If
                If blnExisted = False Then
                    strErrMsg = "����Access�ļ�[" + strAccessFile + "]�����ڣ�"
                    GoTo errProc
                End If
                If strTableName = "" Then
                    strErrMsg = "����δָ��Access�ı�����"
                    GoTo errProc
                End If

                'û������
                If objDataTable.DefaultView.Count < 1 Then
                    Exit Try
                End If

                '���ƥ���ֶ�
                objDataSet = New System.Data.DataSet
                'ֻ����Access
                objOdbcConnection = New System.Data.Odbc.OdbcConnection
                objOdbcConnection.ConnectionTimeout = Xydc.Platform.Common.jsoaConfiguration.ConnectionTimeout
                objOdbcConnection.ConnectionString = "Driver={Microsoft Access Driver (*.mdb)};DBQ=" + strAccessFile + ";ReadOnly=1;UID=admin;PWD=;"
                objOdbcConnection.Open()
                '׼��OdbcCommand
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
                        strErrMsg = "����[" + strAccessFile + "]�ļ���û��Ҫ�������У�"
                        GoTo errProc
                    End If
                End With
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing
                objOdbcConnection.Close()

                'д��Access
                objOdbcConnection.ConnectionTimeout = Xydc.Platform.Common.jsoaConfiguration.ConnectionTimeout
                objOdbcConnection.ConnectionString = "Driver={Microsoft Access Driver (*.mdb)};DBQ=" + strAccessFile + ";ReadOnly=0;UID=admin;PWD=;"
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
                    strSQL = "insert into " + strTableName + " (" + strFieldList + ") values (" + strValueList + ")"
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
        ' �����ݴ�Access���뵽DataTable(��֧��������)
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strAccessFile          ���������ݵ�WEB�������е�Access�ļ�·��
        '     strTableName           ���������ݵ�strTableName
        '     objDataTable           �����ص�Excel����
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        ' ODBC���Ӵ�����
        '     DBQ=Access�ļ�����·��;
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
                '���
                If strAccessFile Is Nothing Then strAccessFile = ""
                strAccessFile = strAccessFile.Trim
                If strTableName Is Nothing Then strTableName = ""
                strTableName = strTableName.Trim
                If objDataTable Is Nothing Then
                    strErrMsg = "����δָ��Ҫ���������ݣ�"
                    GoTo errProc
                End If
                If strAccessFile = "" Then
                    strErrMsg = "����δָ����Access�ļ���"
                    GoTo errProc
                End If
                Dim blnExisted As Boolean
                If objBaseLocalFile.doFileExisted(strErrMsg, strAccessFile, blnExisted) = False Then
                    GoTo errProc
                End If
                If blnExisted = False Then
                    strErrMsg = "����Access�ļ�[" + strAccessFile + "]�����ڣ�"
                    GoTo errProc
                End If
                If strTableName = "" Then
                    strErrMsg = "����δָ��Access�ı�����"
                    GoTo errProc
                End If
                If objDataTable Is Nothing Then
                    strErrMsg = "����δָ��Ŀ�����ݸ�ʽ��"
                    GoTo errProc
                End If

                '����Access����
                'ֻ����Access
                objOdbcConnection = New System.Data.Odbc.OdbcConnection
                objOdbcConnection.ConnectionTimeout = Xydc.Platform.Common.jsoaConfiguration.ConnectionTimeout
                objOdbcConnection.ConnectionString = "Driver={Microsoft Access Driver (*.mdb)};DBQ=" + strAccessFile + ";ReadOnly=1;UID=admin;PWD=;"
                objOdbcConnection.Open()
                '׼��OdbcCommand
                objOdbcCommand = New System.Data.Odbc.OdbcCommand
                objOdbcCommand.Connection = objOdbcConnection
                objOdbcCommand.CommandType = CommandType.Text
                objOdbcCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout
                With Me.m_objOdbcDataAdapter
                    '׼��SQL
                    strSQL = ""
                    strSQL = strSQL & " select * from " + strTableName + ""
                    objOdbcCommand.CommandText = strSQL
                    .SelectCommand = objOdbcCommand
                    '��ȡAccess����
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
