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
    ' ����    ��grswMyTaskData
    '
    ' ����������
    '     ���塰����_B_�ҵ����ˡ����йص����ݷ��ʸ�ʽ
    '----------------------------------------------------------------
    <System.ComponentModel.DesignerCategory("Code"), SerializableAttribute()> Public Class grswMyTaskData
        Inherits System.Data.DataSet

        '������_B_�ҵ�����_�ļ�������Ϣ����
        '������
        Public Const TABLE_GR_B_MYTASK_FILE As String = "����_B_�ҵ�����_�ļ�"
        '�ֶ�����
        Public Const FIELD_GR_B_MYTASK_FILE_WJBS As String = "�ļ���ʶ"
        Public Const FIELD_GR_B_MYTASK_FILE_LSH As String = "��ˮ��"
        Public Const FIELD_GR_B_MYTASK_FILE_BLLX As String = "��������"
        Public Const FIELD_GR_B_MYTASK_FILE_BLZT As String = "����״̬"
        Public Const FIELD_GR_B_MYTASK_FILE_WJZL As String = "�ļ�����"
        Public Const FIELD_GR_B_MYTASK_FILE_WJLX As String = "�ļ�����"
        Public Const FIELD_GR_B_MYTASK_FILE_WJBT As String = "�ļ�����"
        Public Const FIELD_GR_B_MYTASK_FILE_ZSDW As String = "���͵�λ"
        Public Const FIELD_GR_B_MYTASK_FILE_WJZH As String = "�ļ��ֺ�"
        Public Const FIELD_GR_B_MYTASK_FILE_MMDJ As String = "���ܵȼ�"
        Public Const FIELD_GR_B_MYTASK_FILE_JJCD As String = "�����̶�"
        Public Const FIELD_GR_B_MYTASK_FILE_JGDZ As String = "���ش���"
        Public Const FIELD_GR_B_MYTASK_FILE_WJNF As String = "�ļ����"
        Public Const FIELD_GR_B_MYTASK_FILE_WJXH As String = "�ļ����"
        Public Const FIELD_GR_B_MYTASK_FILE_ZTC As String = "�����"
        Public Const FIELD_GR_B_MYTASK_FILE_ZBDW As String = "���쵥λ"
        Public Const FIELD_GR_B_MYTASK_FILE_NGR As String = "�����"
        Public Const FIELD_GR_B_MYTASK_FILE_NGRQ As String = "�������"
        Public Const FIELD_GR_B_MYTASK_FILE_FSRQ As String = "��������"
        Public Const FIELD_GR_B_MYTASK_FILE_BLQX As String = "��������"
        Public Const FIELD_GR_B_MYTASK_FILE_WCRQ As String = "�������"
        Public Const FIELD_GR_B_MYTASK_FILE_KSSW As String = "��������"
        Public Const FIELD_GR_B_MYTASK_FILE_BWTX As String = "��������"
        'Լ��������Ϣ

        '������_B_�ҵ�����_���񡱱���Ϣ����
        '������
        Public Const TABLE_GR_B_MYTASK_TASK As String = "����_B_�ҵ�����_����"
        '�ֶ�����
        Public Const FIELD_GR_B_MYTASK_TASK_WJBS As String = "�ļ���ʶ"
        Public Const FIELD_GR_B_MYTASK_TASK_LSH As String = "��ˮ��"
        Public Const FIELD_GR_B_MYTASK_TASK_BLLX As String = "��������"
        Public Const FIELD_GR_B_MYTASK_TASK_BLZT As String = "����״̬"
        Public Const FIELD_GR_B_MYTASK_TASK_WJZL As String = "�ļ�����"
        Public Const FIELD_GR_B_MYTASK_TASK_WJLX As String = "�ļ�����"
        Public Const FIELD_GR_B_MYTASK_TASK_BLZL As String = "��������"
        Public Const FIELD_GR_B_MYTASK_TASK_WJBT As String = "�ļ�����"
        Public Const FIELD_GR_B_MYTASK_TASK_JGDZ As String = "���ش���"
        Public Const FIELD_GR_B_MYTASK_TASK_WJNF As String = "�ļ����"
        Public Const FIELD_GR_B_MYTASK_TASK_WJXH As String = "�ļ����"
        Public Const FIELD_GR_B_MYTASK_TASK_ZBDW As String = "���쵥λ"
        Public Const FIELD_GR_B_MYTASK_TASK_JSR As String = "������"
        Public Const FIELD_GR_B_MYTASK_TASK_FSR As String = "������"
        Public Const FIELD_GR_B_MYTASK_TASK_WTR As String = "ί����"
        Public Const FIELD_GR_B_MYTASK_TASK_JJSM As String = "����˵��"
        'Լ��������Ϣ

        '������_B_�ҵ�����_�ڵ㡱����Ϣ����
        '�ּ���XXX-XXX-XXX-XXX
        '������
        Public Const TABLE_GR_B_MYTASK_NODE As String = "����_B_�ҵ�����_�ڵ�"
        '�ֶ�����
        Public Const FIELD_GR_B_MYTASK_NODE_CODE As String = "�ڵ����"
        Public Const FIELD_GR_B_MYTASK_NODE_NAME As String = "�ڵ�����"
        Public Const FIELD_GR_B_MYTASK_NODE_KSSJ As String = "��ʼʱ��"
        Public Const FIELD_GR_B_MYTASK_NODE_JSSJ As String = "����ʱ��"
        Public Const FIELD_GR_B_MYTASK_NODE_WJLX As String = "�ļ�����"
        Public Const FIELD_GR_B_MYTASK_NODE_BLLX As String = "��������"
        'Լ��������Ϣ

        '�ڵ����ּ�����˵��
        Public Shared intJDDM_FJCDSM() As Integer = {3, 6, 9}








        '�����ʼ��������enum
        Public Enum enumTableType
            GR_B_MYTASK_FILE = 1
            GR_B_MYTASK_TASK = 2
            GR_B_MYTASK_NODE = 3
        End Enum

        Public Enum enumTaskTypeLevel1
            DBSY = 1  '��������
            BWTX = 2  '��������
            DPWJ = 3  '�����ļ�
            HBWJ = 4  '�����ļ�
            YBSY = 5  '�Ѱ�����
            GQSY = 6  '��������
            CBSY = 7  '�߰�����
            BCSY = 8  '��������
            DBWJ = 9  '�����ļ�
            BDWJ = 10 '�����ļ�
            QBSY = 11 'ȫ������
        End Enum

        Public Enum enumTaskTypeLevel2
            JINTIAN = 1 '����
            BENZHOU = 2 '����
            BENYUEN = 3 '����
            BENYUES = 4 '������ǰ/����
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.Common.Data.grswMyTaskData)
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
                Case enumTableType.GR_B_MYTASK_FILE
                    table = createDataTables_MyTask_File(strErrMsg)
                Case enumTableType.GR_B_MYTASK_TASK
                    table = createDataTables_MyTask_Task(strErrMsg)
                Case enumTableType.GR_B_MYTASK_NODE
                    table = createDataTables_MyTask_Node(strErrMsg)
                Case Else
                    strErrMsg = "��Ч�ı����ͣ�"
                    table = Nothing
            End Select

            createDataTables = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GR_B_MYTASK_FILE
        '----------------------------------------------------------------
        Private Function createDataTables_MyTask_File(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GR_B_MYTASK_FILE)
                With table.Columns
                    .Add(FIELD_GR_B_MYTASK_FILE_WJBS, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_FILE_LSH, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_FILE_BLLX, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_FILE_BLZT, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_FILE_WJZL, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_FILE_WJLX, GetType(System.String))

                    .Add(FIELD_GR_B_MYTASK_FILE_WJBT, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_FILE_ZSDW, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_FILE_WJZH, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_FILE_MMDJ, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_FILE_JJCD, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_FILE_JGDZ, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_FILE_WJNF, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_FILE_WJXH, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_FILE_ZTC, GetType(System.String))

                    .Add(FIELD_GR_B_MYTASK_FILE_ZBDW, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_FILE_NGR, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_FILE_NGRQ, GetType(System.DateTime))

                    .Add(FIELD_GR_B_MYTASK_FILE_FSRQ, GetType(System.DateTime))
                    .Add(FIELD_GR_B_MYTASK_FILE_BLQX, GetType(System.DateTime))
                    .Add(FIELD_GR_B_MYTASK_FILE_WCRQ, GetType(System.DateTime))

                    .Add(FIELD_GR_B_MYTASK_FILE_KSSW, GetType(System.Int32))
                    .Add(FIELD_GR_B_MYTASK_FILE_BWTX, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_MyTask_File = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GR_B_MYTASK_TASK
        '----------------------------------------------------------------
        Private Function createDataTables_MyTask_Task(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GR_B_MYTASK_TASK)
                With table.Columns
                    .Add(FIELD_GR_B_MYTASK_TASK_WJBS, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_TASK_LSH, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_TASK_BLLX, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_TASK_BLZL, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_TASK_BLZT, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_TASK_WJZL, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_TASK_WJLX, GetType(System.String))

                    .Add(FIELD_GR_B_MYTASK_TASK_WJBT, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_TASK_JGDZ, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_TASK_WJNF, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_TASK_WJXH, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_TASK_ZBDW, GetType(System.String))

                    .Add(FIELD_GR_B_MYTASK_TASK_FSR, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_TASK_JSR, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_TASK_WTR, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_TASK_JJSM, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_MyTask_Task = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GR_B_MYTASK_NODE
        '----------------------------------------------------------------
        Private Function createDataTables_MyTask_Node(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GR_B_MYTASK_NODE)
                With table.Columns
                    .Add(FIELD_GR_B_MYTASK_NODE_CODE, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_NODE_NAME, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_NODE_KSSJ, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_NODE_JSSJ, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_NODE_WJLX, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_NODE_BLLX, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_MyTask_Node = table

        End Function

    End Class 'grswMyTaskData

End Namespace 'Xydc.Platform.Common.Data
