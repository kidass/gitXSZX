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
    ' 类名    ：FenfafanweiData
    '
    ' 功能描述：
    '     定义“公文_B_分发范围”表有关的数据访问格式
    '----------------------------------------------------------------
    <System.ComponentModel.DesignerCategory("GWDM"), SerializableAttribute()> Public Class FenfafanweiData
        Inherits System.Data.DataSet

        '公文_B_分发范围表信息定义
        '表名称
        Public Const TABLE_GW_B_FENFAFANWEI As String = "公文_B_分发范围"
        '字段序列
        Public Const FIELD_GW_B_FENFAFANWEI_LSH As String = "流水号"
        Public Const FIELD_GW_B_FENFAFANWEI_FWMC As String = "范围名称"
        Public Const FIELD_GW_B_FENFAFANWEI_FWBZ As String = "范围标志"
        Public Const FIELD_GW_B_FENFAFANWEI_CYLX As String = "成员类型"
        Public Const FIELD_GW_B_FENFAFANWEI_CYMC As String = "成员名称"
        Public Const FIELD_GW_B_FENFAFANWEI_CYWZ As String = "成员位置"
        Public Const FIELD_GW_B_FENFAFANWEI_LXDH As String = "联系电话"
        Public Const FIELD_GW_B_FENFAFANWEI_SJHM As String = "手机号码"
        Public Const FIELD_GW_B_FENFAFANWEI_FTPDZ As String = "FTP地址"
        Public Const FIELD_GW_B_FENFAFANWEI_YXDZ As String = "邮箱地址"
        '约束错误信息








        '定义初始化表类型enum
        Public Enum enumTableType
            GW_B_FENFAFANWEI = 1
        End Enum

        '定义范围标志enum
        Public Enum enumFWBZ
            MAIN = 0        '范围主记录
            CHENGYUAN = 1   '范围成员
        End Enum

        '定义成员类型：个人/单位
        Public Const CYLX_GEREN As String = "个人"
        Public Const CYLX_DANWEI As String = "单位"
        Public Const CYLX_FANWEI As String = "范围"








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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.Common.Data.FenfafanweiData)
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
                Case enumTableType.GW_B_FENFAFANWEI
                    table = createDataTables_Fenfafanwei(strErrMsg)
                Case Else
                    strErrMsg = "无效的表类型！"
                    table = Nothing
            End Select

            createDataTables = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GW_B_FENFAFANWEI
        '----------------------------------------------------------------
        Private Function createDataTables_Fenfafanwei(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GW_B_FENFAFANWEI)
                With table.Columns
                    .Add(FIELD_GW_B_FENFAFANWEI_LSH, GetType(System.Int32))
                    .Add(FIELD_GW_B_FENFAFANWEI_FWMC, GetType(System.String))
                    .Add(FIELD_GW_B_FENFAFANWEI_FWBZ, GetType(System.String))
                    .Add(FIELD_GW_B_FENFAFANWEI_CYLX, GetType(System.String))
                    .Add(FIELD_GW_B_FENFAFANWEI_CYMC, GetType(System.String))
                    .Add(FIELD_GW_B_FENFAFANWEI_CYWZ, GetType(System.Int32))
                    .Add(FIELD_GW_B_FENFAFANWEI_LXDH, GetType(System.String))
                    .Add(FIELD_GW_B_FENFAFANWEI_SJHM, GetType(System.String))
                    .Add(FIELD_GW_B_FENFAFANWEI_FTPDZ, GetType(System.String))
                    .Add(FIELD_GW_B_FENFAFANWEI_YXDZ, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Fenfafanwei = table

        End Function

    End Class 'FenfafanweiData

End Namespace 'Xydc.Platform.Common.Data
