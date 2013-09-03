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
    ' 类名    ：grswMyChangyongyijianData
    '
    ' 功能描述：
    '     定义“个人_B_常用意见”表有关的数据访问格式
    '----------------------------------------------------------------
    <System.ComponentModel.DesignerCategory("Code"), SerializableAttribute()> Public Class grswMyChangyongyijianData
        Inherits System.Data.DataSet

        '“个人_B_常用意见”表信息定义
        '表名称
        Public Const TABLE_GR_B_CHANGYONGYIJIAN As String = "个人_B_常用意见"
        '字段序列
        Public Const FIELD_GR_B_CHANGYONGYIJIAN_XH As String = "序号"
        Public Const FIELD_GR_B_CHANGYONGYIJIAN_RYDM As String = "人员代码"
        Public Const FIELD_GR_B_CHANGYONGYIJIAN_YJLX As String = "意见类型"
        Public Const FIELD_GR_B_CHANGYONGYIJIAN_YJNR As String = "意见内容"
        '约束错误信息








        '定义初始化表类型enum
        Public Enum enumTableType
            GR_B_CHANGYONGYIJIAN = 1
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.Common.Data.grswMyChangyongyijianData)
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
                Case enumTableType.GR_B_CHANGYONGYIJIAN
                    table = createDataTables_MyChangyongyijian(strErrMsg)
                Case Else
                    strErrMsg = "无效的表类型！"
                    table = Nothing
            End Select

            createDataTables = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GR_B_CHANGYONGYIJIAN
        '----------------------------------------------------------------
        Private Function createDataTables_MyChangyongyijian(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GR_B_CHANGYONGYIJIAN)
                With table.Columns
                    .Add(FIELD_GR_B_CHANGYONGYIJIAN_XH, GetType(System.Int32))
                    .Add(FIELD_GR_B_CHANGYONGYIJIAN_RYDM, GetType(System.String))
                    .Add(FIELD_GR_B_CHANGYONGYIJIAN_YJLX, GetType(System.String))
                    .Add(FIELD_GR_B_CHANGYONGYIJIAN_YJNR, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_MyChangyongyijian = table

        End Function

    End Class 'grswMyChangyongyijianData

End Namespace 'Xydc.Platform.Common.Data
