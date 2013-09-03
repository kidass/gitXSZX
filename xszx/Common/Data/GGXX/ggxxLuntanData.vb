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
    ' ����    ��ggxxLuntanData
    '
    ' ����������
    '     ���塰�ڲ���̳���йص����ݷ��ʸ�ʽ
    '----------------------------------------------------------------
    <System.ComponentModel.DesignerCategory("Code"), SerializableAttribute()> Public Class ggxxLuntanData
        Inherits System.Data.DataSet

        '������_B_�����û�������Ϣ����
        '������
        Public Const TABLE_GR_B_JIAOLIUYONGHU As String = "����_B_�����û�"
        '�ֶ�����
        Public Const FIELD_GR_B_JIAOLIUYONGHU_RYDM As String = "��Ա����"
        Public Const FIELD_GR_B_JIAOLIUYONGHU_RYNC As String = "��Ա�ǳ�"
        Public Const FIELD_GR_B_JIAOLIUYONGHU_SFYX As String = "�Ƿ���Ч"
        '�����ֶ�
        Public Const FIELD_GR_B_JIAOLIUYONGHU_ZZDM As String = "��֯����"
        Public Const FIELD_GR_B_JIAOLIUYONGHU_RYXH As String = "��Ա���"
        Public Const FIELD_GR_B_JIAOLIUYONGHU_RYMC As String = "��Ա����"
        Public Const FIELD_GR_B_JIAOLIUYONGHU_YXMS As String = "��Ч����"
        Public Const FIELD_GR_B_JIAOLIUYONGHU_ZCMS As String = "ע������"
        'Լ��������Ϣ




        '������_B_������¼������Ϣ����
        '������
        Public Const TABLE_GR_B_JIAOLIUJILU As String = "����_B_������¼"
        '�ֶ�����
        Public Const FIELD_GR_B_JIAOLIUJILU_JLBH As String = "�������"
        Public Const FIELD_GR_B_JIAOLIUJILU_RYDM As String = "��Ա����"
        Public Const FIELD_GR_B_JIAOLIUJILU_JLZT As String = "��������"
        Public Const FIELD_GR_B_JIAOLIUJILU_FBRQ As String = "��������"
        Public Const FIELD_GR_B_JIAOLIUJILU_JLJB As String = "��������"
        Public Const FIELD_GR_B_JIAOLIUJILU_SJBH As String = "�ϼ����"
        Public Const FIELD_GR_B_JIAOLIUJILU_JLNR As String = "��������"
        '�����ֶ�
        Public Const FIELD_GR_B_JIAOLIUJILU_RYMC As String = "��Ա����"
        Public Const FIELD_GR_B_JIAOLIUJILU_RYNC As String = "��Ա�ǳ�"
        Public Const FIELD_GR_B_JIAOLIUJILU_JLSM As String = "������Ŀ"
        'Լ��������Ϣ




        '�����ʼ��������enum
        Public Enum enumTableType
            GR_B_JIAOLIUYONGHU = 1
            GR_B_JIAOLIUJILU = 2
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.Common.Data.ggxxLuntanData)
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
                Case enumTableType.GR_B_JIAOLIUYONGHU
                    table = createDataTables_JiaoliuYonghu(strErrMsg)

                Case enumTableType.GR_B_JIAOLIUJILU
                    table = createDataTables_JiaoliuJilu(strErrMsg)

                Case Else
                    strErrMsg = "��Ч�ı����ͣ�"
                    table = Nothing
            End Select

            createDataTables = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GR_B_JIAOLIUYONGHU
        '----------------------------------------------------------------
        Private Function createDataTables_JiaoliuYonghu(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GR_B_JIAOLIUYONGHU)
                With table.Columns
                    .Add(FIELD_GR_B_JIAOLIUYONGHU_RYDM, GetType(System.String))
                    .Add(FIELD_GR_B_JIAOLIUYONGHU_RYNC, GetType(System.String))
                    .Add(FIELD_GR_B_JIAOLIUYONGHU_SFYX, GetType(System.Int32))

                    .Add(FIELD_GR_B_JIAOLIUYONGHU_ZZDM, GetType(System.String))
                    .Add(FIELD_GR_B_JIAOLIUYONGHU_RYXH, GetType(System.Int32))
                    .Add(FIELD_GR_B_JIAOLIUYONGHU_RYMC, GetType(System.String))
                    .Add(FIELD_GR_B_JIAOLIUYONGHU_YXMS, GetType(System.String))
                    .Add(FIELD_GR_B_JIAOLIUYONGHU_ZCMS, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_JiaoliuYonghu = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GR_B_JIAOLIUJILU
        '----------------------------------------------------------------
        Private Function createDataTables_JiaoliuJilu(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GR_B_JIAOLIUJILU)
                With table.Columns
                    .Add(FIELD_GR_B_JIAOLIUJILU_JLBH, GetType(System.Int32))
                    .Add(FIELD_GR_B_JIAOLIUJILU_RYDM, GetType(System.String))
                    .Add(FIELD_GR_B_JIAOLIUJILU_JLZT, GetType(System.String))
                    .Add(FIELD_GR_B_JIAOLIUJILU_FBRQ, GetType(System.DateTime))
                    .Add(FIELD_GR_B_JIAOLIUJILU_JLJB, GetType(System.Int32))
                    .Add(FIELD_GR_B_JIAOLIUJILU_SJBH, GetType(System.Int32))
                    .Add(FIELD_GR_B_JIAOLIUJILU_JLNR, GetType(System.String))

                    .Add(FIELD_GR_B_JIAOLIUJILU_RYMC, GetType(System.String))
                    .Add(FIELD_GR_B_JIAOLIUJILU_RYNC, GetType(System.String))
                    .Add(FIELD_GR_B_JIAOLIUJILU_JLSM, GetType(System.Int32))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_JiaoliuJilu = table

        End Function

    End Class 'ggxxLuntanData

End Namespace 'Xydc.Platform.Common.Data
