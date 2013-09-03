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
    ' ����    ��grswMyCalenderData
    '
    ' ����������
    '     ���塰����_B_������־�����йص����ݷ��ʸ�ʽ
    '----------------------------------------------------------------
    <System.ComponentModel.DesignerCategory("Code"), SerializableAttribute()> Public Class grswMyCalenderData
        Inherits System.Data.DataSet

        '����
        Public Const JJ_TEJI As String = "�ؼ�"
        Public Const JJ_JI As String = "��"
        Public Const JJ_YIBAN As String = "һ��"

        Public Const WC_WC As String = "���"
        Public Const WC_ZAIBAN As String = "δ��"

        '������_B_������־������Ϣ����
        '������
        Public Const TABLE_GR_B_GERENRIZHI As String = "����_B_������־"
        '�ֶ�����
        Public Const FIELD_GR_B_GERENRIZHI_BH As String = "���"
        Public Const FIELD_GR_B_GERENRIZHI_SYZ As String = "������"
        Public Const FIELD_GR_B_GERENRIZHI_PX As String = "����"
        Public Const FIELD_GR_B_GERENRIZHI_KSSJ As String = "��ʼʱ��"
        Public Const FIELD_GR_B_GERENRIZHI_JSSJ As String = "����ʱ��"
        Public Const FIELD_GR_B_GERENRIZHI_ZT As String = "����"
        Public Const FIELD_GR_B_GERENRIZHI_DD As String = "�ص�"
        Public Const FIELD_GR_B_GERENRIZHI_RY As String = "��Ա"
        Public Const FIELD_GR_B_GERENRIZHI_NR As String = "����"
        Public Const FIELD_GR_B_GERENRIZHI_JJ As String = "����"
        Public Const FIELD_GR_B_GERENRIZHI_WC As String = "���"
        Public Const FIELD_GR_B_GERENRIZHI_TX As String = "����"
        Public Const FIELD_GR_B_GERENRIZHI_XS As String = "Сʱ"
        Public Const FIELD_GR_B_GERENRIZHI_FZ As String = "����"
        '��ʾ�ֶ�����
        Public Const FIELD_GR_B_GERENRIZHI_TXMS As String = "��������"
        'Լ��������Ϣ








        '�����ʼ��������enum
        Public Enum enumTableType
            GR_B_GERENRIZHI = 1
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.Common.Data.grswMyCalenderData)
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
                Case enumTableType.GR_B_GERENRIZHI
                    table = createDataTables_MyGerenRizhi(strErrMsg)
                Case Else
                    strErrMsg = "��Ч�ı����ͣ�"
                    table = Nothing
            End Select

            createDataTables = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GR_B_GERENRIZHI
        '----------------------------------------------------------------
        Private Function createDataTables_MyGerenRizhi(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GR_B_GERENRIZHI)
                With table.Columns
                    .Add(FIELD_GR_B_GERENRIZHI_BH, GetType(System.Int32))
                    .Add(FIELD_GR_B_GERENRIZHI_SYZ, GetType(System.String))
                    .Add(FIELD_GR_B_GERENRIZHI_PX, GetType(System.Int32))
                    .Add(FIELD_GR_B_GERENRIZHI_KSSJ, GetType(System.DateTime))
                    .Add(FIELD_GR_B_GERENRIZHI_JSSJ, GetType(System.DateTime))
                    .Add(FIELD_GR_B_GERENRIZHI_ZT, GetType(System.String))
                    .Add(FIELD_GR_B_GERENRIZHI_DD, GetType(System.String))
                    .Add(FIELD_GR_B_GERENRIZHI_RY, GetType(System.String))
                    .Add(FIELD_GR_B_GERENRIZHI_NR, GetType(System.String))
                    .Add(FIELD_GR_B_GERENRIZHI_JJ, GetType(System.String))
                    .Add(FIELD_GR_B_GERENRIZHI_WC, GetType(System.String))
                    .Add(FIELD_GR_B_GERENRIZHI_TX, GetType(System.Int32))
                    .Add(FIELD_GR_B_GERENRIZHI_XS, GetType(System.Int32))
                    .Add(FIELD_GR_B_GERENRIZHI_FZ, GetType(System.Int32))

                    .Add(FIELD_GR_B_GERENRIZHI_TXMS, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_MyGerenRizhi = table

        End Function

    End Class 'grswMyCalenderData

End Namespace 'Xydc.Platform.Common.Data
