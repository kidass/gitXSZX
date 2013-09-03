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
    ' 命名空间：Xydc.Platform.Common.Data
    ' 类名    ：XitongcanshuData
    '
    ' 功能描述：
    '   　定义“管理_B_系统参数”表相关的数据访问格式
    '----------------------------------------------------------------
    <System.ComponentModel.DesignerCategory("XTPZ"), SerializableAttribute()> Public Class XitongcanshuData
        Inherits System.Data.DataSet

        '“管理_B_系统参数”表信息定义
        '表名称
        Public Const TABLE_GL_B_XITONGCANSHU As String = "管理_B_系统参数"
        '字段序列
        Public Const FIELD_GL_B_XITONGCANSHU_BS As String = "标识"
        Public Const FIELD_GL_B_XITONGCANSHU_ZNBZYWZ As String = "主内部主页位置"
        Public Const FIELD_GL_B_XITONGCANSHU_ZFTPFWQ As String = "主FTP服务器"
        Public Const FIELD_GL_B_XITONGCANSHU_ZFTPDK As String = "主FTP端口"
        Public Const FIELD_GL_B_XITONGCANSHU_ZFTPYH As String = "主FTP用户"
        Public Const FIELD_GL_B_XITONGCANSHU_ZFTPMM As String = "主FTP用户密码"
        Public Const FIELD_GL_B_XITONGCANSHU_CNBZYWZ As String = "从内部主页位置"
        Public Const FIELD_GL_B_XITONGCANSHU_CFTPFWQ As String = "从FTP服务器"
        Public Const FIELD_GL_B_XITONGCANSHU_CFTPDK As String = "从FTP端口"
        Public Const FIELD_GL_B_XITONGCANSHU_CFTPYH As String = "从FTP用户"
        Public Const FIELD_GL_B_XITONGCANSHU_CFTPMM As String = "从FTP用户密码"
        Public Const FIELD_GL_B_XITONGCANSHU_SFJM As String = "是否加密"
        Public Const FIELD_GL_B_XITONGCANSHU_ZFTPMMJM As String = "主FTP用户密码加密"
        Public Const FIELD_GL_B_XITONGCANSHU_CFTPMMJM As String = "从FTP用户密码加密"
        '约束错误信息








        '定义初始化表类型enum
        Public Enum enumTableType
            GL_B_XITONGCANSHU = 1
        End Enum









        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Private Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
            MyBase.New(info, context)
        End Sub

        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
        End Sub

        '----------------------------------------------------------------
        ' 构造函数
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
        ' 安全释放本身资源
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
        '将给定DataTable加入到DataSet中
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
        '根据指定类型创建dataTable
        '----------------------------------------------------------------
        Public Function createDataTables( _
            ByRef strErrMsg As String, _
            ByVal enumType As enumTableType) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Select Case enumType
                Case enumTableType.GL_B_XITONGCANSHU
                    table = createDataTables_Xitongcanshu(strErrMsg)
                Case Else
                    strErrMsg = "无效的表类型！"
                    table = Nothing
            End Select

            createDataTables = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GL_B_XITONGCANSHU
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
