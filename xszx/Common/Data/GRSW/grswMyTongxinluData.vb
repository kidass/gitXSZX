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
    ' ����    ��grswMyTongxinluData
    '
    ' ����������
    '     ���塰����_B_ͨѶ¼�����йص����ݷ��ʸ�ʽ
    '----------------------------------------------------------------
    <System.ComponentModel.DesignerCategory("Code"), SerializableAttribute()> Public Class grswMyTongxinluData
        Inherits System.Data.DataSet

        '����

        '������_B_ͨѶ¼������Ϣ����
        '������
        Public Const TABLE_GR_B_TONGXINLU As String = "����_B_ͨѶ¼"
        '�ֶ�����
        Public Const FIELD_GR_B_TONGXINLU_XH As String = "���"
        Public Const FIELD_GR_B_TONGXINLU_SYZ As String = "������"
        Public Const FIELD_GR_B_TONGXINLU_PX As String = "����"
        Public Const FIELD_GR_B_TONGXINLU_XM As String = "����"
        Public Const FIELD_GR_B_TONGXINLU_DZYJ As String = "�����ʼ�"
        Public Const FIELD_GR_B_TONGXINLU_YDDH As String = "�ƶ��绰"
        Public Const FIELD_GR_B_TONGXINLU_XHJ As String = "Ѱ����"
        Public Const FIELD_GR_B_TONGXINLU_GRWY As String = "������ҳ"
        Public Const FIELD_GR_B_TONGXINLU_JTDZ As String = "��ͥ��ַ"
        Public Const FIELD_GR_B_TONGXINLU_ZZDH As String = "סլ�绰"
        Public Const FIELD_GR_B_TONGXINLU_JTYB As String = "��ͥ�ʱ�"
        Public Const FIELD_GR_B_TONGXINLU_DWMC As String = "��λ����"
        Public Const FIELD_GR_B_TONGXINLU_DWDZ As String = "��λ��ַ"
        Public Const FIELD_GR_B_TONGXINLU_DWYB As String = "��λ�ʱ�"
        Public Const FIELD_GR_B_TONGXINLU_BGDH As String = "�칫�绰"
        Public Const FIELD_GR_B_TONGXINLU_YWCZ As String = "ҵ����"
        Public Const FIELD_GR_B_TONGXINLU_ZW As String = "ְ��"
        Public Const FIELD_GR_B_TONGXINLU_BM As String = "����"
        Public Const FIELD_GR_B_TONGXINLU_BGS As String = "�칫��"
        Public Const FIELD_GR_B_TONGXINLU_DWWY As String = "��λ��ҳ"
        '��ʾ�ֶ�����
        'Լ��������Ϣ









        '�����ʼ��������enum
        Public Enum enumTableType
            GR_B_TONGXINLU = 1
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.Common.Data.grswMyTongxinluData)
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
                Case enumTableType.GR_B_TONGXINLU
                    table = createDataTables_MyTongxinlu(strErrMsg)
                Case Else
                    strErrMsg = "��Ч�ı����ͣ�"
                    table = Nothing
            End Select

            createDataTables = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GR_B_TONGXINLU
        '----------------------------------------------------------------
        Private Function createDataTables_MyTongxinlu(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GR_B_TONGXINLU)
                With table.Columns
                    .Add(FIELD_GR_B_TONGXINLU_XH, GetType(System.Int32))
                    .Add(FIELD_GR_B_TONGXINLU_SYZ, GetType(System.String))
                    .Add(FIELD_GR_B_TONGXINLU_PX, GetType(System.Int32))

                    .Add(FIELD_GR_B_TONGXINLU_XM, GetType(System.String))
                    .Add(FIELD_GR_B_TONGXINLU_DZYJ, GetType(System.String))
                    .Add(FIELD_GR_B_TONGXINLU_YDDH, GetType(System.String))
                    .Add(FIELD_GR_B_TONGXINLU_XHJ, GetType(System.String))
                    .Add(FIELD_GR_B_TONGXINLU_GRWY, GetType(System.String))

                    .Add(FIELD_GR_B_TONGXINLU_JTDZ, GetType(System.String))
                    .Add(FIELD_GR_B_TONGXINLU_ZZDH, GetType(System.String))
                    .Add(FIELD_GR_B_TONGXINLU_JTYB, GetType(System.String))

                    .Add(FIELD_GR_B_TONGXINLU_DWMC, GetType(System.String))
                    .Add(FIELD_GR_B_TONGXINLU_DWDZ, GetType(System.String))
                    .Add(FIELD_GR_B_TONGXINLU_DWYB, GetType(System.String))
                    .Add(FIELD_GR_B_TONGXINLU_BGDH, GetType(System.String))
                    .Add(FIELD_GR_B_TONGXINLU_YWCZ, GetType(System.String))
                    .Add(FIELD_GR_B_TONGXINLU_ZW, GetType(System.String))
                    .Add(FIELD_GR_B_TONGXINLU_BM, GetType(System.String))
                    .Add(FIELD_GR_B_TONGXINLU_BGS, GetType(System.String))
                    .Add(FIELD_GR_B_TONGXINLU_DWWY, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_MyTongxinlu = table

        End Function

    End Class 'grswMyTongxinluData

End Namespace 'Xydc.Platform.Common.Data
