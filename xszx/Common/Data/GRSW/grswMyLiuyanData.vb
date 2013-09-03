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
    ' ����    ��grswMyLiuyanData
    '
    ' ����������
    '     ���塰����_B_�ҵ����ˡ����йص����ݷ��ʸ�ʽ
    '----------------------------------------------------------------
    <System.ComponentModel.DesignerCategory("Code"), SerializableAttribute()> Public Class grswMyLiuyanData
        Inherits System.Data.DataSet

        '������_B_�뿪���ԡ�����Ϣ����
        '������
        Public Const TABLE_GR_B_LIKAILIUYAN As String = "����_B_�뿪����"
        '�ֶ�����
        Public Const FIELD_GR_B_LIKAILIUYAN_BS As String = "��ʶ"
        Public Const FIELD_GR_B_LIKAILIUYAN_LYR As String = "������"
        Public Const FIELD_GR_B_LIKAILIUYAN_LYRQ As String = "��������"
        Public Const FIELD_GR_B_LIKAILIUYAN_SXRQ As String = "��Ч����"
        Public Const FIELD_GR_B_LIKAILIUYAN_ZFRQ As String = "ʧЧ����"
        Public Const FIELD_GR_B_LIKAILIUYAN_WTDLR As String = "ί�д�����"
        Public Const FIELD_GR_B_LIKAILIUYAN_LYNR As String = "��������"
        'Լ��������Ϣ








        '�����ʼ��������enum
        Public Enum enumTableType
            GR_B_LIKAILIUYAN = 1
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.Common.Data.grswMyLiuyanData)
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
                Case enumTableType.GR_B_LIKAILIUYAN
                    table = createDataTables_MyLikailiuyan(strErrMsg)
                Case Else
                    strErrMsg = "��Ч�ı����ͣ�"
                    table = Nothing
            End Select

            createDataTables = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GR_B_LIKAILIUYAN
        '----------------------------------------------------------------
        Private Function createDataTables_MyLikailiuyan(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GR_B_LIKAILIUYAN)
                With table.Columns
                    .Add(FIELD_GR_B_LIKAILIUYAN_BS, GetType(System.Int32))
                    .Add(FIELD_GR_B_LIKAILIUYAN_LYR, GetType(System.String))
                    .Add(FIELD_GR_B_LIKAILIUYAN_LYRQ, GetType(System.DateTime))
                    .Add(FIELD_GR_B_LIKAILIUYAN_SXRQ, GetType(System.DateTime))
                    .Add(FIELD_GR_B_LIKAILIUYAN_ZFRQ, GetType(System.DateTime))
                    .Add(FIELD_GR_B_LIKAILIUYAN_WTDLR, GetType(System.String))
                    .Add(FIELD_GR_B_LIKAILIUYAN_LYNR, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_MyLikailiuyan = table

        End Function

    End Class 'grswMyLiuyanData

End Namespace 'Xydc.Platform.Common.Data
