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
    ' ����    ��ggxxLingdaoanpaiData
    '
    ' ����������
    '     ���塰�쵼����š��йص����ݷ��ʸ�ʽ
    '----------------------------------------------------------------
    <System.ComponentModel.DesignerCategory("Code"), SerializableAttribute()> Public Class ggxxLingdaoanpaiData
        Inherits System.Data.DataSet

        '������_B_�쵼����š�����Ϣ����
        '������
        Public Const TABLE_GR_B_LINGDAOHUODONGANPAI As String = "����_B_�쵼�����"
        '�ֶ�����
        Public Const FIELD_GR_B_LINGDAOHUODONGANPAI_XH As String = "���"
        Public Const FIELD_GR_B_LINGDAOHUODONGANPAI_RQ As String = "����"
        Public Const FIELD_GR_B_LINGDAOHUODONGANPAI_SJ As String = "ʱ��"
        Public Const FIELD_GR_B_LINGDAOHUODONGANPAI_DD As String = "�ص�"
        Public Const FIELD_GR_B_LINGDAOHUODONGANPAI_CJLD As String = "�μ��쵼"
        Public Const FIELD_GR_B_LINGDAOHUODONGANPAI_HDNR As String = "�����"
        Public Const FIELD_GR_B_LINGDAOHUODONGANPAI_PX As String = "����"
        Public Const FIELD_GR_B_LINGDAOHUODONGANPAI_BZ As String = "��ע"
        '�����ֶ�
        Public Const FIELD_GR_B_LINGDAOHUODONGANPAI_XQ As String = "����"
        '��ʾ�ֶ�
        Public Const FIELD_GR_B_LINGDAOHUODONGANPAI_RC As String = "�ճ�"
        'Լ��������Ϣ




        '������_B_�쵼�����_��ӡ01������Ϣ����
        '������
        Public Const TABLE_GR_B_LINGDAOHUODONGANPAI_DAYIN01 As String = "����_B_�쵼�����_��ӡ01"
        '�ֶ�����
        Public Const FIELD_GR_B_LINGDAOHUODONGANPAI_DAYIN01_RQ As String = "����"
        Public Const FIELD_GR_B_LINGDAOHUODONGANPAI_DAYIN01_XQ As String = "����"
        Public Const FIELD_GR_B_LINGDAOHUODONGANPAI_DAYIN01_CJLD As String = "�μ��쵼"
        Public Const FIELD_GR_B_LINGDAOHUODONGANPAI_DAYIN01_ZZDM As String = "��֯����"
        Public Const FIELD_GR_B_LINGDAOHUODONGANPAI_DAYIN01_PX As String = "����"
        Public Const FIELD_GR_B_LINGDAOHUODONGANPAI_DAYIN01_SW As String = "����"
        Public Const FIELD_GR_B_LINGDAOHUODONGANPAI_DAYIN01_XW As String = "����"
        'Լ��������Ϣ




        '�����ʼ��������enum
        Public Enum enumTableType
            GR_B_LINGDAOHUODONGANPAI = 1
            GR_B_LINGDAOHUODONGANPAI_DAYIN01 = 2
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.Common.Data.ggxxLingdaoanpaiData)
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
                Case enumTableType.GR_B_LINGDAOHUODONGANPAI
                    table = createDataTables_LingdaoHuodongAnpai(strErrMsg)

                Case enumTableType.GR_B_LINGDAOHUODONGANPAI_DAYIN01
                    table = createDataTables_LingdaoHuodongAnpai_Dayin01(strErrMsg)

                Case Else
                    strErrMsg = "��Ч�ı����ͣ�"
                    table = Nothing
            End Select

            createDataTables = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GR_B_LINGDAOHUODONGANPAI
        '----------------------------------------------------------------
        Private Function createDataTables_LingdaoHuodongAnpai(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GR_B_LINGDAOHUODONGANPAI)
                With table.Columns
                    .Add(FIELD_GR_B_LINGDAOHUODONGANPAI_XH, GetType(System.Int32))
                    .Add(FIELD_GR_B_LINGDAOHUODONGANPAI_RQ, GetType(System.DateTime))
                    .Add(FIELD_GR_B_LINGDAOHUODONGANPAI_SJ, GetType(System.String))
                    .Add(FIELD_GR_B_LINGDAOHUODONGANPAI_DD, GetType(System.String))
                    .Add(FIELD_GR_B_LINGDAOHUODONGANPAI_CJLD, GetType(System.String))
                    .Add(FIELD_GR_B_LINGDAOHUODONGANPAI_HDNR, GetType(System.String))
                    .Add(FIELD_GR_B_LINGDAOHUODONGANPAI_PX, GetType(System.Int32))
                    .Add(FIELD_GR_B_LINGDAOHUODONGANPAI_BZ, GetType(System.String))

                    .Add(FIELD_GR_B_LINGDAOHUODONGANPAI_XQ, GetType(System.String))
                    .Add(FIELD_GR_B_LINGDAOHUODONGANPAI_RC, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_LingdaoHuodongAnpai = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GR_B_LINGDAOHUODONGANPAI_DAYIN01
        '----------------------------------------------------------------
        Private Function createDataTables_LingdaoHuodongAnpai_Dayin01(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GR_B_LINGDAOHUODONGANPAI_DAYIN01)
                With table.Columns
                    .Add(FIELD_GR_B_LINGDAOHUODONGANPAI_DAYIN01_RQ, GetType(System.DateTime))
                    .Add(FIELD_GR_B_LINGDAOHUODONGANPAI_DAYIN01_XQ, GetType(System.String))
                    .Add(FIELD_GR_B_LINGDAOHUODONGANPAI_DAYIN01_CJLD, GetType(System.String))
                    .Add(FIELD_GR_B_LINGDAOHUODONGANPAI_DAYIN01_ZZDM, GetType(System.String))
                    .Add(FIELD_GR_B_LINGDAOHUODONGANPAI_DAYIN01_PX, GetType(System.Int32))
                    .Add(FIELD_GR_B_LINGDAOHUODONGANPAI_DAYIN01_SW, GetType(System.String))
                    .Add(FIELD_GR_B_LINGDAOHUODONGANPAI_DAYIN01_XW, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_LingdaoHuodongAnpai_Dayin01 = table

        End Function

    End Class 'ggxxLingdaoanpaiData

End Namespace 'Xydc.Platform.Common.Data
