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
    ' 类名    ：ggxxLingdaoanpaiData
    '
    ' 功能描述：
    '     定义“领导活动安排”有关的数据访问格式
    '----------------------------------------------------------------
    <System.ComponentModel.DesignerCategory("Code"), SerializableAttribute()> Public Class ggxxLingdaoanpaiData
        Inherits System.Data.DataSet

        '“个人_B_领导活动安排”表信息定义
        '表名称
        Public Const TABLE_GR_B_LINGDAOHUODONGANPAI As String = "个人_B_领导活动安排"
        '字段序列
        Public Const FIELD_GR_B_LINGDAOHUODONGANPAI_XH As String = "序号"
        Public Const FIELD_GR_B_LINGDAOHUODONGANPAI_RQ As String = "日期"
        Public Const FIELD_GR_B_LINGDAOHUODONGANPAI_SJ As String = "时间"
        Public Const FIELD_GR_B_LINGDAOHUODONGANPAI_DD As String = "地点"
        Public Const FIELD_GR_B_LINGDAOHUODONGANPAI_CJLD As String = "参加领导"
        Public Const FIELD_GR_B_LINGDAOHUODONGANPAI_HDNR As String = "活动内容"
        Public Const FIELD_GR_B_LINGDAOHUODONGANPAI_PX As String = "排序"
        Public Const FIELD_GR_B_LINGDAOHUODONGANPAI_BZ As String = "备注"
        '计算字段
        Public Const FIELD_GR_B_LINGDAOHUODONGANPAI_XQ As String = "星期"
        '显示字段
        Public Const FIELD_GR_B_LINGDAOHUODONGANPAI_RC As String = "日程"
        '约束错误信息




        '“个人_B_领导活动安排_打印01”表信息定义
        '表名称
        Public Const TABLE_GR_B_LINGDAOHUODONGANPAI_DAYIN01 As String = "个人_B_领导活动安排_打印01"
        '字段序列
        Public Const FIELD_GR_B_LINGDAOHUODONGANPAI_DAYIN01_RQ As String = "日期"
        Public Const FIELD_GR_B_LINGDAOHUODONGANPAI_DAYIN01_XQ As String = "星期"
        Public Const FIELD_GR_B_LINGDAOHUODONGANPAI_DAYIN01_CJLD As String = "参加领导"
        Public Const FIELD_GR_B_LINGDAOHUODONGANPAI_DAYIN01_ZZDM As String = "组织代码"
        Public Const FIELD_GR_B_LINGDAOHUODONGANPAI_DAYIN01_PX As String = "排序"
        Public Const FIELD_GR_B_LINGDAOHUODONGANPAI_DAYIN01_SW As String = "上午"
        Public Const FIELD_GR_B_LINGDAOHUODONGANPAI_DAYIN01_XW As String = "下午"
        '约束错误信息




        '定义初始化表类型enum
        Public Enum enumTableType
            GR_B_LINGDAOHUODONGANPAI = 1
            GR_B_LINGDAOHUODONGANPAI_DAYIN01 = 2
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
                Case enumTableType.GR_B_LINGDAOHUODONGANPAI
                    table = createDataTables_LingdaoHuodongAnpai(strErrMsg)

                Case enumTableType.GR_B_LINGDAOHUODONGANPAI_DAYIN01
                    table = createDataTables_LingdaoHuodongAnpai_Dayin01(strErrMsg)

                Case Else
                    strErrMsg = "无效的表类型！"
                    table = Nothing
            End Select

            createDataTables = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GR_B_LINGDAOHUODONGANPAI
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
        '创建TABLE_GR_B_LINGDAOHUODONGANPAI_DAYIN01
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
