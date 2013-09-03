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
    ' 类名    ：ggxxLuntanData
    '
    ' 功能描述：
    '     定义“内部论坛”有关的数据访问格式
    '----------------------------------------------------------------
    <System.ComponentModel.DesignerCategory("Code"), SerializableAttribute()> Public Class ggxxLuntanData
        Inherits System.Data.DataSet

        '“个人_B_交流用户”表信息定义
        '表名称
        Public Const TABLE_GR_B_JIAOLIUYONGHU As String = "个人_B_交流用户"
        '字段序列
        Public Const FIELD_GR_B_JIAOLIUYONGHU_RYDM As String = "人员代码"
        Public Const FIELD_GR_B_JIAOLIUYONGHU_RYNC As String = "人员昵称"
        Public Const FIELD_GR_B_JIAOLIUYONGHU_SFYX As String = "是否有效"
        '计算字段
        Public Const FIELD_GR_B_JIAOLIUYONGHU_ZZDM As String = "组织代码"
        Public Const FIELD_GR_B_JIAOLIUYONGHU_RYXH As String = "人员序号"
        Public Const FIELD_GR_B_JIAOLIUYONGHU_RYMC As String = "人员名称"
        Public Const FIELD_GR_B_JIAOLIUYONGHU_YXMS As String = "有效描述"
        Public Const FIELD_GR_B_JIAOLIUYONGHU_ZCMS As String = "注册描述"
        '约束错误信息




        '“个人_B_交流记录”表信息定义
        '表名称
        Public Const TABLE_GR_B_JIAOLIUJILU As String = "个人_B_交流记录"
        '字段序列
        Public Const FIELD_GR_B_JIAOLIUJILU_JLBH As String = "交流编号"
        Public Const FIELD_GR_B_JIAOLIUJILU_RYDM As String = "人员代码"
        Public Const FIELD_GR_B_JIAOLIUJILU_JLZT As String = "交流主题"
        Public Const FIELD_GR_B_JIAOLIUJILU_FBRQ As String = "发表日期"
        Public Const FIELD_GR_B_JIAOLIUJILU_JLJB As String = "交流级别"
        Public Const FIELD_GR_B_JIAOLIUJILU_SJBH As String = "上级编号"
        Public Const FIELD_GR_B_JIAOLIUJILU_JLNR As String = "交流内容"
        '计算字段
        Public Const FIELD_GR_B_JIAOLIUJILU_RYMC As String = "人员名称"
        Public Const FIELD_GR_B_JIAOLIUJILU_RYNC As String = "人员昵称"
        Public Const FIELD_GR_B_JIAOLIUJILU_JLSM As String = "交流数目"
        '约束错误信息




        '定义初始化表类型enum
        Public Enum enumTableType
            GR_B_JIAOLIUYONGHU = 1
            GR_B_JIAOLIUJILU = 2
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
                Case enumTableType.GR_B_JIAOLIUYONGHU
                    table = createDataTables_JiaoliuYonghu(strErrMsg)

                Case enumTableType.GR_B_JIAOLIUJILU
                    table = createDataTables_JiaoliuJilu(strErrMsg)

                Case Else
                    strErrMsg = "无效的表类型！"
                    table = Nothing
            End Select

            createDataTables = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GR_B_JIAOLIUYONGHU
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
        '创建TABLE_GR_B_JIAOLIUJILU
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
