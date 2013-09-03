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

Imports System
Imports System.Data
Imports System.Runtime.Serialization

Namespace Xydc.Platform.Common.Data

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.Common.Data
    ' ����    ��DrdcData
    '
    ' ����������
    '   ���������Excel�ļ��йظ�ʽ
    '----------------------------------------------------------------
    <System.ComponentModel.DesignerCategory("DRDC"), SerializableAttribute()> Public Class DrdcData
        Inherits System.Data.DataSet

        '�Զ��屨���йز���
        Public Const MACRO_PROPSEP As String = ":"
        Public Const MACRO_ELEMSEP As String = "$"
        Public Const MACRO_FIELD As String = "FIELD"

        '���塰ͨ��_B_���뵼��_EXCEL��ʽ���ݡ�
        '������
        Public Const TABLE_TY_B_DRDC_EXCELFORMAT As String = "ͨ��_B_���뵼��_EXCEL��ʽ����"
        '�ֶ�����
        '==============================================================================
        Public Const FIELD_TY_B_DRDC_EXCELFORMAT_DATASHEETNAME As String = "����Sheet��"
        Public Const FIELD_TY_B_DRDC_EXCELFORMAT_TITLEROWS As String = "����������"
        Public Const FIELD_TY_B_DRDC_EXCELFORMAT_DATACOLS As String = "��������"
        '==============================================================================
        'Լ��������Ϣ








        '�����ʼ��������enum
        Public Enum enumTableType
            TY_B_DRDC_EXCELFORMAT = 1
        End Enum








        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Private Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
            MyBase.New(info, context)
        End Sub

        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
        End Sub

        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New(ByVal objenumTableType As enumTableType)
            MyBase.New()
            Try
                Dim objDataTable As System.Data.DataTable
                Dim strErrMsg As String
                objDataTable = Me.createDataTables(strErrMsg, objenumTableType)
                If Not (objDataTable Is Nothing) Then
                    Me.Tables.Add(objDataTable)
                End If
            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' ��ȫ�ͷű�����Դ
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.Common.Data.DrdcData)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub









        '----------------------------------------------------------------
        '������DataTable���뵽DataSet��
        '----------------------------------------------------------------
        Public Function appendDataTable(ByVal table As System.Data.DataTable) As String

            Dim strErrMsg As String = ""

            Try
                Me.Tables.Add(table)
            Catch ex As Exception
                strErrMsg = ex.Message
            End Try

            appendDataTable = strErrMsg

        End Function

        '----------------------------------------------------------------
        '����ָ�����ʹ���dataTable
        '----------------------------------------------------------------
        Public Function createDataTables( _
            ByRef strErrMsg As String, _
            ByVal enumType As enumTableType) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Select Case enumType
                Case enumTableType.TY_B_DRDC_EXCELFORMAT
                    table = createDataTables_EXCELFORMAT(strErrMsg)
                Case Else
                    strErrMsg = "��Ч�ı����ͣ�"
                    table = Nothing
            End Select

            createDataTables = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_TY_B_DRDC_EXCELFORMAT
        '----------------------------------------------------------------
        Private Function createDataTables_EXCELFORMAT(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_TY_B_DRDC_EXCELFORMAT)
                With table.Columns
                    .Add(FIELD_TY_B_DRDC_EXCELFORMAT_DATASHEETNAME, GetType(System.String))
                    .Add(FIELD_TY_B_DRDC_EXCELFORMAT_TITLEROWS, GetType(System.Int32))
                    .Add(FIELD_TY_B_DRDC_EXCELFORMAT_DATACOLS, GetType(System.Int32))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_EXCELFORMAT = table

        End Function

    End Class 'DrdcData

End Namespace 'Xydc.Platform.Common.Data
