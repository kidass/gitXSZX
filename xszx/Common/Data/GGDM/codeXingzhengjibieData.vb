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
    ' 类名    ：XingzhengjibieData
    '
    ' 功能描述：
    '     定义“公共_B_行政级别”表有关的数据访问格式
    '----------------------------------------------------------------
    <System.ComponentModel.DesignerCategory("GGDM"), SerializableAttribute()> Public Class XingzhengjibieData
        Inherits System.Data.DataSet

        '公共_B_行政级别表信息定义
        '表名称
        Public Const TABLE_GG_B_XINGZHENGJIBIE As String = "公共_B_行政级别"
        '字段序列
        Public Const FIELD_GG_B_XINGZHENGJIBIE_JBDM As String = "级别代码"
        Public Const FIELD_GG_B_XINGZHENGJIBIE_JBMC As String = "级别名称"
        Public Const FIELD_GG_B_XINGZHENGJIBIE_XZJB As String = "行政级别"
        '约束错误信息








        '定义初始化表类型enum
        Public Enum enumTableType
            GG_B_XINGZHENGJIBIE = 1
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.Common.Data.XingzhengjibieData)
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
                Case enumTableType.GG_B_XINGZHENGJIBIE
                    table = createDataTables_Xingzhengjibie(strErrMsg)
                Case Else
                    strErrMsg = "无效的表类型！"
                    table = Nothing
            End Select

            createDataTables = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GG_B_XINGZHENGJIBIE
        '----------------------------------------------------------------
        Private Function createDataTables_Xingzhengjibie(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GG_B_XINGZHENGJIBIE)
                With table.Columns
                    .Add(FIELD_GG_B_XINGZHENGJIBIE_JBDM, GetType(System.String))
                    .Add(FIELD_GG_B_XINGZHENGJIBIE_JBMC, GetType(System.String))
                    .Add(FIELD_GG_B_XINGZHENGJIBIE_XZJB, GetType(System.Int32))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Xingzhengjibie = table

        End Function

    End Class 'XingzhengjibieData

End Namespace 'Xydc.Platform.Common.Data
