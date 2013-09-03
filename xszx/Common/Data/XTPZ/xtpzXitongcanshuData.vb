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
    ' ����    ��XitongcanshuData
    '
    ' ����������
    '   �����塰����_B_ϵͳ����������ص����ݷ��ʸ�ʽ
    '----------------------------------------------------------------
    <System.ComponentModel.DesignerCategory("XTPZ"), SerializableAttribute()> Public Class XitongcanshuData
        Inherits System.Data.DataSet

        '������_B_ϵͳ����������Ϣ����
        '������
        Public Const TABLE_GL_B_XITONGCANSHU As String = "����_B_ϵͳ����"
        '�ֶ�����
        Public Const FIELD_GL_B_XITONGCANSHU_BS As String = "��ʶ"
        Public Const FIELD_GL_B_XITONGCANSHU_ZNBZYWZ As String = "���ڲ���ҳλ��"
        Public Const FIELD_GL_B_XITONGCANSHU_ZFTPFWQ As String = "��FTP������"
        Public Const FIELD_GL_B_XITONGCANSHU_ZFTPDK As String = "��FTP�˿�"
        Public Const FIELD_GL_B_XITONGCANSHU_ZFTPYH As String = "��FTP�û�"
        Public Const FIELD_GL_B_XITONGCANSHU_ZFTPMM As String = "��FTP�û�����"
        Public Const FIELD_GL_B_XITONGCANSHU_CNBZYWZ As String = "���ڲ���ҳλ��"
        Public Const FIELD_GL_B_XITONGCANSHU_CFTPFWQ As String = "��FTP������"
        Public Const FIELD_GL_B_XITONGCANSHU_CFTPDK As String = "��FTP�˿�"
        Public Const FIELD_GL_B_XITONGCANSHU_CFTPYH As String = "��FTP�û�"
        Public Const FIELD_GL_B_XITONGCANSHU_CFTPMM As String = "��FTP�û�����"
        Public Const FIELD_GL_B_XITONGCANSHU_SFJM As String = "�Ƿ����"
        Public Const FIELD_GL_B_XITONGCANSHU_ZFTPMMJM As String = "��FTP�û��������"
        Public Const FIELD_GL_B_XITONGCANSHU_CFTPMMJM As String = "��FTP�û��������"
        'Լ��������Ϣ








        '�����ʼ��������enum
        Public Enum enumTableType
            GL_B_XITONGCANSHU = 1
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.Common.Data.XitongcanshuData)
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
                Case enumTableType.GL_B_XITONGCANSHU
                    table = createDataTables_Xitongcanshu(strErrMsg)
                Case Else
                    strErrMsg = "��Ч�ı����ͣ�"
                    table = Nothing
            End Select

            createDataTables = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GL_B_XITONGCANSHU
        '----------------------------------------------------------------
        Private Function createDataTables_Xitongcanshu(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GL_B_XITONGCANSHU)
                With table.Columns
                    .Add(FIELD_GL_B_XITONGCANSHU_BS, GetType(System.Int32))
                    .Add(FIELD_GL_B_XITONGCANSHU_ZNBZYWZ, GetType(System.String))
                    .Add(FIELD_GL_B_XITONGCANSHU_ZFTPFWQ, GetType(System.String))
                    .Add(FIELD_GL_B_XITONGCANSHU_ZFTPDK, GetType(System.String))
                    .Add(FIELD_GL_B_XITONGCANSHU_ZFTPYH, GetType(System.String))
                    .Add(FIELD_GL_B_XITONGCANSHU_ZFTPMM, GetType(System.String))
                    .Add(FIELD_GL_B_XITONGCANSHU_CNBZYWZ, GetType(System.String))
                    .Add(FIELD_GL_B_XITONGCANSHU_CFTPFWQ, GetType(System.String))
                    .Add(FIELD_GL_B_XITONGCANSHU_CFTPDK, GetType(System.String))
                    .Add(FIELD_GL_B_XITONGCANSHU_CFTPYH, GetType(System.String))
                    .Add(FIELD_GL_B_XITONGCANSHU_CFTPMM, GetType(System.String))
                    .Add(FIELD_GL_B_XITONGCANSHU_SFJM, GetType(System.Int32))
                    .Add(FIELD_GL_B_XITONGCANSHU_ZFTPMMJM, GetType(System.Byte()))
                    .Add(FIELD_GL_B_XITONGCANSHU_CFTPMMJM, GetType(System.Byte()))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Xitongcanshu = table

        End Function

    End Class 'XitongcanshuData

End Namespace 'Xydc.Platform.Common.Data
