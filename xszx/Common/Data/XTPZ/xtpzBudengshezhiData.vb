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
    ' ����    ��BudengshezhiData
    '
    ' ����������
    '   �����塰����_B_�������á�����ص����ݷ��ʸ�ʽ
    '----------------------------------------------------------------
    <System.ComponentModel.DesignerCategory("XTPZ"), SerializableAttribute()> Public Class BudengshezhiData
        Inherits System.Data.DataSet

        '����_B_�������ñ���Ϣ����
        '������
        Public Const TABLE_GL_B_BUDENGSHEZHI As String = "����_B_��������"
        '�ֶ�����
        Public Const FIELD_GL_B_BUDENGSHEZHI_GWDM As String = "��λ����"
        Public Const FIELD_GL_B_BUDENGSHEZHI_BDFW As String = "���Ƿ�Χ"
        Public Const FIELD_GL_B_BUDENGSHEZHI_ZWLB As String = "ְ���б�"
        Public Const FIELD_GL_B_BUDENGSHEZHI_JSXZ As String = "��������"
        Public Const FIELD_GL_B_BUDENGSHEZHI_GWMC As String = "��λ����"
        Public Const FIELD_GL_B_BUDENGSHEZHI_BDFWMC As String = "���Ƿ�Χ����"
        Public Const FIELD_GL_B_BUDENGSHEZHI_JSXZMC As String = "������������"
        'Լ��������Ϣ








        '�����ʼ��������enum
        Public Enum enumTableType
            GL_B_BUDENGSHEZHI = 1
        End Enum

        '���岹�Ƿ�Χ�б�
        Public Enum enumBudengfanweiType
            All = 0                 '������λ(������)
            Zhiwu = 1               '���Բ���ָ��ְ���������
            ZhiwuBumenLevel = 2     '���Բ������ڵ�λָ����λ�������µ�ָ��ְ���������
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.Common.Data.BudengshezhiData)
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
                Case enumTableType.GL_B_BUDENGSHEZHI
                    table = createDataTables_Budengshezhi(strErrMsg)
                Case Else
                    strErrMsg = "��Ч�ı����ͣ�"
                    table = Nothing
            End Select

            createDataTables = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GL_B_BUDENGSHEZHI
        '----------------------------------------------------------------
        Private Function createDataTables_Budengshezhi(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GL_B_BUDENGSHEZHI)
                With table.Columns
                    .Add(FIELD_GL_B_BUDENGSHEZHI_GWDM, GetType(System.String))
                    .Add(FIELD_GL_B_BUDENGSHEZHI_BDFW, GetType(System.Int32))
                    .Add(FIELD_GL_B_BUDENGSHEZHI_ZWLB, GetType(System.String))
                    .Add(FIELD_GL_B_BUDENGSHEZHI_JSXZ, GetType(System.Int32))

                    .Add(FIELD_GL_B_BUDENGSHEZHI_GWMC, GetType(System.String))
                    .Add(FIELD_GL_B_BUDENGSHEZHI_BDFWMC, GetType(System.String))
                    .Add(FIELD_GL_B_BUDENGSHEZHI_JSXZMC, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Budengshezhi = table

        End Function

    End Class 'BudengshezhiData

End Namespace 'Xydc.Platform.Common.Data
