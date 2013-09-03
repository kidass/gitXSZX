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
    ' 类名    ：grswMyCalenderData
    '
    ' 功能描述：
    '     定义“个人_B_个人日志”表有关的数据访问格式
    '----------------------------------------------------------------
    <System.ComponentModel.DesignerCategory("Code"), SerializableAttribute()> Public Class grswMyCalenderData
        Inherits System.Data.DataSet

        '常量
        Public Const JJ_TEJI As String = "特急"
        Public Const JJ_JI As String = "急"
        Public Const JJ_YIBAN As String = "一般"

        Public Const WC_WC As String = "完成"
        Public Const WC_ZAIBAN As String = "未办"

        '“个人_B_个人日志”表信息定义
        '表名称
        Public Const TABLE_GR_B_GERENRIZHI As String = "个人_B_个人日志"
        '字段序列
        Public Const FIELD_GR_B_GERENRIZHI_BH As String = "编号"
        Public Const FIELD_GR_B_GERENRIZHI_SYZ As String = "所有者"
        Public Const FIELD_GR_B_GERENRIZHI_PX As String = "排序"
        Public Const FIELD_GR_B_GERENRIZHI_KSSJ As String = "开始时间"
        Public Const FIELD_GR_B_GERENRIZHI_JSSJ As String = "结束时间"
        Public Const FIELD_GR_B_GERENRIZHI_ZT As String = "主题"
        Public Const FIELD_GR_B_GERENRIZHI_DD As String = "地点"
        Public Const FIELD_GR_B_GERENRIZHI_RY As String = "人员"
        Public Const FIELD_GR_B_GERENRIZHI_NR As String = "内容"
        Public Const FIELD_GR_B_GERENRIZHI_JJ As String = "紧急"
        Public Const FIELD_GR_B_GERENRIZHI_WC As String = "完成"
        Public Const FIELD_GR_B_GERENRIZHI_TX As String = "提醒"
        Public Const FIELD_GR_B_GERENRIZHI_XS As String = "小时"
        Public Const FIELD_GR_B_GERENRIZHI_FZ As String = "分钟"
        '显示字段序列
        Public Const FIELD_GR_B_GERENRIZHI_TXMS As String = "提醒描述"
        '约束错误信息








        '定义初始化表类型enum
        Public Enum enumTableType
            GR_B_GERENRIZHI = 1
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
                Case enumTableType.GR_B_GERENRIZHI
                    table = createDataTables_MyGerenRizhi(strErrMsg)
                Case Else
                    strErrMsg = "无效的表类型！"
                    table = Nothing
            End Select

            createDataTables = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GR_B_GERENRIZHI
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
