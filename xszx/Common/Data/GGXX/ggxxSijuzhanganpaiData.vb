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
    ' 类名    ：ggxxSijuzhanganpaiData
    '
    ' 功能描述：
    '     定义“司局长京外活动安排”有关的数据访问格式
    '----------------------------------------------------------------
    <System.ComponentModel.DesignerCategory("Code"), SerializableAttribute()> Public Class ggxxSijuzhanganpaiData
        Inherits System.Data.DataSet

        '“个人_B_司局长活动安排”表信息定义
        '表名称
        Public Const TABLE_GR_B_SIJUZHANGANPAI As String = "个人_B_司局长活动安排"
        '字段序列
        Public Const FIELD_GR_B_SIJUZHANGANPAI_XH As String = "序号"
        Public Const FIELD_GR_B_SIJUZHANGANPAI_KSRQ As String = "开始日期"
        Public Const FIELD_GR_B_SIJUZHANGANPAI_JSRQ As String = "结束日期"
        Public Const FIELD_GR_B_SIJUZHANGANPAI_RY As String = "人员"
        Public Const FIELD_GR_B_SIJUZHANGANPAI_DJR As String = "登记人"
        Public Const FIELD_GR_B_SIJUZHANGANPAI_DD As String = "地点"
        Public Const FIELD_GR_B_SIJUZHANGANPAI_DH As String = "电话"
        Public Const FIELD_GR_B_SIJUZHANGANPAI_SY As String = "事由"
        Public Const FIELD_GR_B_SIJUZHANGANPAI_PX As String = "排序"
        Public Const FIELD_GR_B_SIJUZHANGANPAI_BZ As String = "备注"
        '约束错误信息




        '“个人_B_领导活动安排_打印01”表信息定义
        '表名称
        Public Const TABLE_GR_B_SIJUZHANGANPAI_DAYIN01 As String = "个人_B_司局长活动安排_打印01"
        '字段序列
        Public Const FIELD_GR_B_SIJUZHANGANPAI_DAYIN01_ZZDM As String = "组织代码"
        Public Const FIELD_GR_B_SIJUZHANGANPAI_DAYIN01_KSRQ As String = "开始日期"
        Public Const FIELD_GR_B_SIJUZHANGANPAI_DAYIN01_JSRQ As String = "结束日期"
        Public Const FIELD_GR_B_SIJUZHANGANPAI_DAYIN01_RY As String = "人员"
        Public Const FIELD_GR_B_SIJUZHANGANPAI_DAYIN01_DH As String = "电话"
        Public Const FIELD_GR_B_SIJUZHANGANPAI_DAYIN01_DD As String = "地点"
        Public Const FIELD_GR_B_SIJUZHANGANPAI_DAYIN01_SY As String = "事由"
        Public Const FIELD_GR_B_SIJUZHANGANPAI_DAYIN01_BZ As String = "备注"
        Public Const FIELD_GR_B_SIJUZHANGANPAI_DAYIN01_PX As String = "排序"
        Public Const FIELD_GR_B_SIJUZHANGANPAI_DAYIN01_DJR As String = "登记人"
        '约束错误信息




        '定义初始化表类型enum
        Public Enum enumTableType
            GR_B_SIJUZHANGANPAI = 1
            GR_B_SIJUZHANGANPAI_DAYIN01 = 2
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.Common.Data.ggxxSijuzhanganpaiData)
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
                Case enumTableType.GR_B_SIJUZHANGANPAI
                    table = createDataTables_SIJUZHANGANPAI(strErrMsg)

                Case enumTableType.GR_B_SIJUZHANGANPAI_DAYIN01
                    table = createDataTables_SIJUZHANGANPAI_Dayin01(strErrMsg)

                Case Else
                    strErrMsg = "无效的表类型！"
                    table = Nothing
            End Select

            createDataTables = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GR_B_SIJUZHANGANPAI
        '----------------------------------------------------------------
        Private Function createDataTables_SIJUZHANGANPAI(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GR_B_SIJUZHANGANPAI)
                With table.Columns
                    .Add(FIELD_GR_B_SIJUZHANGANPAI_XH, GetType(System.Int32))
                    .Add(FIELD_GR_B_SIJUZHANGANPAI_KSRQ, GetType(System.DateTime))
                    .Add(FIELD_GR_B_SIJUZHANGANPAI_JSRQ, GetType(System.DateTime))
                    .Add(FIELD_GR_B_SIJUZHANGANPAI_RY, GetType(System.String))
                    .Add(FIELD_GR_B_SIJUZHANGANPAI_DJR, GetType(System.String))
                    .Add(FIELD_GR_B_SIJUZHANGANPAI_DD, GetType(System.String))
                    .Add(FIELD_GR_B_SIJUZHANGANPAI_DH, GetType(System.String))
                    .Add(FIELD_GR_B_SIJUZHANGANPAI_SY, GetType(System.String))
                    .Add(FIELD_GR_B_SIJUZHANGANPAI_PX, GetType(System.Int32))
                    .Add(FIELD_GR_B_SIJUZHANGANPAI_BZ, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_SIJUZHANGANPAI = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GR_B_SIJUZHANGANPAI_DAYIN01
        '----------------------------------------------------------------
        Private Function createDataTables_SIJUZHANGANPAI_Dayin01(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GR_B_SIJUZHANGANPAI_DAYIN01)
                With table.Columns
                    .Add(FIELD_GR_B_SIJUZHANGANPAI_DAYIN01_KSRQ, GetType(System.DateTime))
                    .Add(FIELD_GR_B_SIJUZHANGANPAI_DAYIN01_JSRQ, GetType(System.DateTime))
                    .Add(FIELD_GR_B_SIJUZHANGANPAI_DAYIN01_RY, GetType(System.String))
                    .Add(FIELD_GR_B_SIJUZHANGANPAI_DAYIN01_DH, GetType(System.String))
                    .Add(FIELD_GR_B_SIJUZHANGANPAI_DAYIN01_PX, GetType(System.Int32))
                    .Add(FIELD_GR_B_SIJUZHANGANPAI_DAYIN01_DD, GetType(System.String))
                    .Add(FIELD_GR_B_SIJUZHANGANPAI_DAYIN01_BZ, GetType(System.String))
                    .Add(FIELD_GR_B_SIJUZHANGANPAI_DAYIN01_SY, GetType(System.String))
                    .Add(FIELD_GR_B_SIJUZHANGANPAI_DAYIN01_DJR, GetType(System.String))
                    .Add(FIELD_GR_B_SIJUZHANGANPAI_DAYIN01_ZZDM, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_SIJUZHANGANPAI_Dayin01 = table

        End Function

    End Class 'ggxxsijuzhanganpaiData

End Namespace 'Xydc.Platform.Common.Data

