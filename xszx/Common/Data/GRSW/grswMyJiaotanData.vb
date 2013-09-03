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
    ' 类名    ：grswMyJiaotanData
    '
    ' 功能描述：
    '     定义“公共_B_交谈”表有关的数据访问格式
    '----------------------------------------------------------------
    <System.ComponentModel.DesignerCategory("Code"), SerializableAttribute()> Public Class grswMyJiaotanData
        Inherits System.Data.DataSet

        '“公共_B_交谈”表信息定义
        '表名称
        Public Const TABLE_GG_B_JIAOTAN As String = "公共_B_交谈"
        '字段序列
        Public Const FIELD_GG_B_JIAOTAN_LSH As String = "流水号"
        Public Const FIELD_GG_B_JIAOTAN_FSR As String = "发送人"
        Public Const FIELD_GG_B_JIAOTAN_JSR As String = "接收人"
        Public Const FIELD_GG_B_JIAOTAN_XX As String = "信息"
        Public Const FIELD_GG_B_JIAOTAN_BZ As String = "标志"
        Public Const FIELD_GG_B_JIAOTAN_TS As String = "提示"
        Public Const FIELD_GG_B_JIAOTAN_FSSJ As String = "发送时间"
        Public Const FIELD_GG_B_JIAOTAN_WYBS As String = "唯一标识"
        '约束错误信息

        Public Enum enumFileDownloadStatus
            NotDownload = 0 '没有下载
            HasDownload = 1 '已经下载
        End Enum

        '目录设定
        Public Const FILEDIR_FJ As String = "JT\FJ"          '交谈附件目录

        '“公共_B_交谈_附件”表信息定义
        '表名称
        Public Const TABLE_GG_B_JIAOTAN_FUJIAN As String = "公共_B_交谈_附件"
        '字段序列
        Public Const FIELD_GG_B_JIAOTAN_FUJIAN_WJBS As String = "文件标识"
        Public Const FIELD_GG_B_JIAOTAN_FUJIAN_WJXH As String = "序号"
        Public Const FIELD_GG_B_JIAOTAN_FUJIAN_WJSM As String = "说明"
        Public Const FIELD_GG_B_JIAOTAN_FUJIAN_WJYS As String = "页数"
        Public Const FIELD_GG_B_JIAOTAN_FUJIAN_WJWZ As String = "位置"        '服务器文件位置(相对于FTP根的路径)
        '附加信息(显示/编辑时用)
        Public Const FIELD_GG_B_JIAOTAN_FUJIAN_XSXH As String = "显示序号"
        Public Const FIELD_GG_B_JIAOTAN_FUJIAN_BDWJ As String = "本地文件"    '下载后的文件位置(完整路径)
        Public Const FIELD_GG_B_JIAOTAN_FUJIAN_XZBZ As String = "下载标志"    '是否下载?
        '约束错误信息

        '“公共_B_交谈_带附件描述”虚拟表信息定义
        '表名称
        Public Const TABLE_GG_B_VT_JIAOTAN_FJXX As String = "公共_B_交谈_带附件描述"
        '字段序列
        Public Const FIELD_GG_B_VT_JIAOTAN_FJXX_LSH As String = "流水号"
        Public Const FIELD_GG_B_VT_JIAOTAN_FJXX_FSR As String = "发送人"
        Public Const FIELD_GG_B_VT_JIAOTAN_FJXX_JSR As String = "接收人"
        Public Const FIELD_GG_B_VT_JIAOTAN_FJXX_XX As String = "信息"
        Public Const FIELD_GG_B_VT_JIAOTAN_FJXX_BZ As String = "标志"
        Public Const FIELD_GG_B_VT_JIAOTAN_FJXX_TS As String = "提示"
        Public Const FIELD_GG_B_VT_JIAOTAN_FJXX_FSSJ As String = "发送时间"
        Public Const FIELD_GG_B_VT_JIAOTAN_FJXX_WYBS As String = "唯一标识"
        Public Const FIELD_GG_B_VT_JIAOTAN_FJXX_FJXX As String = "附件"
        Public Const FIELD_GG_B_VT_JIAOTAN_FJXX_YDZT As String = "已读状态"
        '约束错误信息









        '定义初始化表类型enum
        Public Enum enumTableType
            GG_B_JIAOTAN = 1
            GG_B_JIAOTAN_FUJIAN = 2
            GG_B_VT_JIAOTAN_FJXX = 3
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
                Case enumTableType.GG_B_JIAOTAN
                    table = createDataTables_MyJiaotan(strErrMsg)

                Case enumTableType.GG_B_JIAOTAN_FUJIAN
                    table = createDataTables_MyJiaotanFujian(strErrMsg)

                Case enumTableType.GG_B_VT_JIAOTAN_FJXX
                    table = createDataTables_MyJiaotanFjxx(strErrMsg)

                Case Else
                    strErrMsg = "无效的表类型！"
                    table = Nothing
            End Select

            createDataTables = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GG_B_JIAOTAN
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
        '创建TABLE_GG_B_JIAOTAN_FUJIAN
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
        '创建TABLE_GG_B_VT_JIAOTAN_FJXX
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
