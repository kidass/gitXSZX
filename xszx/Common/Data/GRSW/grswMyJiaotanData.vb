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
    ' ����    ��grswMyJiaotanData
    '
    ' ����������
    '     ���塰����_B_��̸�����йص����ݷ��ʸ�ʽ
    '----------------------------------------------------------------
    <System.ComponentModel.DesignerCategory("Code"), SerializableAttribute()> Public Class grswMyJiaotanData
        Inherits System.Data.DataSet

        '������_B_��̸������Ϣ����
        '������
        Public Const TABLE_GG_B_JIAOTAN As String = "����_B_��̸"
        '�ֶ�����
        Public Const FIELD_GG_B_JIAOTAN_LSH As String = "��ˮ��"
        Public Const FIELD_GG_B_JIAOTAN_FSR As String = "������"
        Public Const FIELD_GG_B_JIAOTAN_JSR As String = "������"
        Public Const FIELD_GG_B_JIAOTAN_XX As String = "��Ϣ"
        Public Const FIELD_GG_B_JIAOTAN_BZ As String = "��־"
        Public Const FIELD_GG_B_JIAOTAN_TS As String = "��ʾ"
        Public Const FIELD_GG_B_JIAOTAN_FSSJ As String = "����ʱ��"
        Public Const FIELD_GG_B_JIAOTAN_WYBS As String = "Ψһ��ʶ"
        'Լ��������Ϣ

        Public Enum enumFileDownloadStatus
            NotDownload = 0 'û������
            HasDownload = 1 '�Ѿ�����
        End Enum

        'Ŀ¼�趨
        Public Const FILEDIR_FJ As String = "JT\FJ"          '��̸����Ŀ¼

        '������_B_��̸_����������Ϣ����
        '������
        Public Const TABLE_GG_B_JIAOTAN_FUJIAN As String = "����_B_��̸_����"
        '�ֶ�����
        Public Const FIELD_GG_B_JIAOTAN_FUJIAN_WJBS As String = "�ļ���ʶ"
        Public Const FIELD_GG_B_JIAOTAN_FUJIAN_WJXH As String = "���"
        Public Const FIELD_GG_B_JIAOTAN_FUJIAN_WJSM As String = "˵��"
        Public Const FIELD_GG_B_JIAOTAN_FUJIAN_WJYS As String = "ҳ��"
        Public Const FIELD_GG_B_JIAOTAN_FUJIAN_WJWZ As String = "λ��"        '�������ļ�λ��(�����FTP����·��)
        '������Ϣ(��ʾ/�༭ʱ��)
        Public Const FIELD_GG_B_JIAOTAN_FUJIAN_XSXH As String = "��ʾ���"
        Public Const FIELD_GG_B_JIAOTAN_FUJIAN_BDWJ As String = "�����ļ�"    '���غ���ļ�λ��(����·��)
        Public Const FIELD_GG_B_JIAOTAN_FUJIAN_XZBZ As String = "���ر�־"    '�Ƿ�����?
        'Լ��������Ϣ

        '������_B_��̸_�������������������Ϣ����
        '������
        Public Const TABLE_GG_B_VT_JIAOTAN_FJXX As String = "����_B_��̸_����������"
        '�ֶ�����
        Public Const FIELD_GG_B_VT_JIAOTAN_FJXX_LSH As String = "��ˮ��"
        Public Const FIELD_GG_B_VT_JIAOTAN_FJXX_FSR As String = "������"
        Public Const FIELD_GG_B_VT_JIAOTAN_FJXX_JSR As String = "������"
        Public Const FIELD_GG_B_VT_JIAOTAN_FJXX_XX As String = "��Ϣ"
        Public Const FIELD_GG_B_VT_JIAOTAN_FJXX_BZ As String = "��־"
        Public Const FIELD_GG_B_VT_JIAOTAN_FJXX_TS As String = "��ʾ"
        Public Const FIELD_GG_B_VT_JIAOTAN_FJXX_FSSJ As String = "����ʱ��"
        Public Const FIELD_GG_B_VT_JIAOTAN_FJXX_WYBS As String = "Ψһ��ʶ"
        Public Const FIELD_GG_B_VT_JIAOTAN_FJXX_FJXX As String = "����"
        Public Const FIELD_GG_B_VT_JIAOTAN_FJXX_YDZT As String = "�Ѷ�״̬"
        'Լ��������Ϣ









        '�����ʼ��������enum
        Public Enum enumTableType
            GG_B_JIAOTAN = 1
            GG_B_JIAOTAN_FUJIAN = 2
            GG_B_VT_JIAOTAN_FJXX = 3
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.Common.Data.grswMyJiaotanData)
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
                Case enumTableType.GG_B_JIAOTAN
                    table = createDataTables_MyJiaotan(strErrMsg)

                Case enumTableType.GG_B_JIAOTAN_FUJIAN
                    table = createDataTables_MyJiaotanFujian(strErrMsg)

                Case enumTableType.GG_B_VT_JIAOTAN_FJXX
                    table = createDataTables_MyJiaotanFjxx(strErrMsg)

                Case Else
                    strErrMsg = "��Ч�ı����ͣ�"
                    table = Nothing
            End Select

            createDataTables = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GG_B_JIAOTAN
        '----------------------------------------------------------------
        Private Function createDataTables_MyJiaotan(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GG_B_JIAOTAN)
                With table.Columns
                    .Add(FIELD_GG_B_JIAOTAN_LSH, GetType(System.Int32))
                    .Add(FIELD_GG_B_JIAOTAN_FSR, GetType(System.String))
                    .Add(FIELD_GG_B_JIAOTAN_JSR, GetType(System.String))
                    .Add(FIELD_GG_B_JIAOTAN_XX, GetType(System.String))
                    .Add(FIELD_GG_B_JIAOTAN_BZ, GetType(System.Int32))
                    .Add(FIELD_GG_B_JIAOTAN_TS, GetType(System.String))
                    .Add(FIELD_GG_B_JIAOTAN_FSSJ, GetType(System.DateTime))
                    .Add(FIELD_GG_B_JIAOTAN_WYBS, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_MyJiaotan = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GG_B_JIAOTAN_FUJIAN
        '----------------------------------------------------------------
        Private Function createDataTables_MyJiaotanFujian(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GG_B_JIAOTAN_FUJIAN)
                With table.Columns
                    .Add(FIELD_GG_B_JIAOTAN_FUJIAN_WJBS, GetType(System.String))
                    .Add(FIELD_GG_B_JIAOTAN_FUJIAN_WJXH, GetType(System.Int32))

                    .Add(FIELD_GG_B_JIAOTAN_FUJIAN_WJSM, GetType(System.String))
                    .Add(FIELD_GG_B_JIAOTAN_FUJIAN_WJYS, GetType(System.Int32))
                    .Add(FIELD_GG_B_JIAOTAN_FUJIAN_WJWZ, GetType(System.String))

                    .Add(FIELD_GG_B_JIAOTAN_FUJIAN_XSXH, GetType(System.Int32))
                    .Add(FIELD_GG_B_JIAOTAN_FUJIAN_BDWJ, GetType(System.String))
                    .Add(FIELD_GG_B_JIAOTAN_FUJIAN_XZBZ, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_MyJiaotanFujian = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GG_B_VT_JIAOTAN_FJXX
        '----------------------------------------------------------------
        Private Function createDataTables_MyJiaotanFjxx(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GG_B_VT_JIAOTAN_FJXX)
                With table.Columns
                    .Add(FIELD_GG_B_VT_JIAOTAN_FJXX_LSH, GetType(System.Int32))
                    .Add(FIELD_GG_B_VT_JIAOTAN_FJXX_FSR, GetType(System.String))
                    .Add(FIELD_GG_B_VT_JIAOTAN_FJXX_JSR, GetType(System.String))
                    .Add(FIELD_GG_B_VT_JIAOTAN_FJXX_XX, GetType(System.String))
                    .Add(FIELD_GG_B_VT_JIAOTAN_FJXX_BZ, GetType(System.Int32))
                    .Add(FIELD_GG_B_VT_JIAOTAN_FJXX_TS, GetType(System.String))
                    .Add(FIELD_GG_B_VT_JIAOTAN_FJXX_FSSJ, GetType(System.DateTime))
                    .Add(FIELD_GG_B_VT_JIAOTAN_FJXX_WYBS, GetType(System.String))

                    .Add(FIELD_GG_B_VT_JIAOTAN_FJXX_FJXX, GetType(System.String))
                    .Add(FIELD_GG_B_VT_JIAOTAN_FJXX_YDZT, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_MyJiaotanFjxx = table

        End Function

    End Class 'grswMyJiaotanData

End Namespace 'Xydc.Platform.Common.Data
