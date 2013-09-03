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
    ' ����    ��GongzuogangweiData
    '
    ' ����������
    '   �����塰����_B_������λ������ص����ݷ��ʸ�ʽ
    '----------------------------------------------------------------
    <System.ComponentModel.DesignerCategory("GGDM"), SerializableAttribute()> Public Class GongzuogangweiData
        Inherits System.Data.DataSet

        '������_B_������λ������Ϣ����
        '������
        Public Const TABLE_GG_B_GONGZUOGANGWEI As String = "����_B_������λ"
        '�ֶ�����
        Public Const FIELD_GG_B_GONGZUOGANGWEI_GWDM As String = "��λ����"
        Public Const FIELD_GG_B_GONGZUOGANGWEI_GWMC As String = "��λ����"
        'Լ��������Ϣ

        '������_B_VT_ѡ��������λ���������Ϣ����
        '������
        Public Const TABLE_GG_B_VT_SELGONGZUOGANGWEI As String = "����_B_VT_ѡ��������λ"
        '�ֶ�����
        Public Const FIELD_GG_B_VT_SELGONGZUOGANGWEI_GWMC As String = "��λ����"
        'Լ��������Ϣ








        '�����ʼ��������enum
        Public Enum enumTableType
            GG_B_GONGZUOGANGWEI = 1
            GG_B_VT_SELGONGZUOGANGWEI = 2
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.Common.Data.GongzuogangweiData)
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
                Case enumTableType.GG_B_GONGZUOGANGWEI
                    table = createDataTables_Gongzuogangwei(strErrMsg)
                Case enumTableType.GG_B_VT_SELGONGZUOGANGWEI
                    table = createDataTables_SelGongzuogangwei(strErrMsg)
                Case Else
                    strErrMsg = "��Ч�ı����ͣ�"
                    table = Nothing
            End Select

            createDataTables = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GG_B_GONGZUOGANGWEI
        '----------------------------------------------------------------
        Private Function createDataTables_Gongzuogangwei(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GG_B_GONGZUOGANGWEI)
                With table.Columns
                    .Add(FIELD_GG_B_GONGZUOGANGWEI_GWDM, GetType(System.String))
                    .Add(FIELD_GG_B_GONGZUOGANGWEI_GWMC, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Gongzuogangwei = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GG_B_VT_SELGONGZUOGANGWEI
        '----------------------------------------------------------------
        Private Function createDataTables_SelGongzuogangwei(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GG_B_VT_SELGONGZUOGANGWEI)
                With table.Columns
                    .Add(FIELD_GG_B_VT_SELGONGZUOGANGWEI_GWMC, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_SelGongzuogangwei = table

        End Function

    End Class 'GongzuogangweiData

End Namespace 'Xydc.Platform.Common.Data
