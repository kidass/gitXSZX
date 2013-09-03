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
    ' 类名    ：QueryData
    '
    ' 功能描述：
    '   　定义机要文件管理相关的数据访问格式
    '----------------------------------------------------------------
    <System.ComponentModel.DesignerCategory("SJCX"), SerializableAttribute()> Public Class QueryData
        Inherits System.Data.DataSet

        '比较运算符显示名称
        Public Const COMPARESIGN_NAME_EQ As String = "等于"
        Public Const COMPARESIGN_NAME_NOTEQ As String = "不等于"
        Public Const COMPARESIGN_NAME_GT As String = "大于"
        Public Const COMPARESIGN_NAME_GET As String = "大于等于"
        Public Const COMPARESIGN_NAME_LT As String = "小于"
        Public Const COMPARESIGN_NAME_LET As String = "小于等于"
        Public Const COMPARESIGN_NAME_BETWEEN As String = "两者之间"
        Public Const COMPARESIGN_NAME_LIKE As String = "包含"
        Public Const COMPARESIGN_NAME_NOTLIKE As String = "不包含"

        '比较运算符数据库符号
        Public Const COMPARESIGN_EQ As String = "="
        Public Const COMPARESIGN_NOTEQ As String = "<>"
        Public Const COMPARESIGN_GT As String = ">"
        Public Const COMPARESIGN_GET As String = ">="
        Public Const COMPARESIGN_LT As String = "<"
        Public Const COMPARESIGN_LET As String = "<="
        Public Const COMPARESIGN_BETWEEN As String = "between"
        Public Const COMPARESIGN_LIKE As String = "like"
        Public Const COMPARESIGN_NOTLIKE As String = "not like"

        '连接运算符名称
        Public Const JOINSIGN_NAME_AND As String = "并且"
        Public Const JOINSIGN_NAME_OR As String = "或者"

        '连接运算符数据库符号
        Public Const JOINSIGN_AND As String = "and"
        Public Const JOINSIGN_OR As String = "or"

        '定义“查询_B_查询条件”
        '表名称
        Public Const TABLE_CX_B_CHAXUNTIAOJIAN As String = "查询_B_查询条件"
        '字段序列
        '==============================================================================
        Public Const FIELD_CX_B_CHAXUNTIAOJIAN_ZKHM As String = "左括弧名"
        Public Const FIELD_CX_B_CHAXUNTIAOJIAN_ZKHZ As String = "左括弧值"
        Public Const FIELD_CX_B_CHAXUNTIAOJIAN_ZDMC As String = "字段名"
        Public Const FIELD_CX_B_CHAXUNTIAOJIAN_BJFM As String = "比较符名"
        Public Const FIELD_CX_B_CHAXUNTIAOJIAN_BJFZ As String = "比较符值"
        Public Const FIELD_CX_B_CHAXUNTIAOJIAN_VAL1 As String = "值1"
        Public Const FIELD_CX_B_CHAXUNTIAOJIAN_VAL2 As String = "值2"
        Public Const FIELD_CX_B_CHAXUNTIAOJIAN_YKHM As String = "右括弧名"
        Public Const FIELD_CX_B_CHAXUNTIAOJIAN_YKHZ As String = "右括弧值"
        Public Const FIELD_CX_B_CHAXUNTIAOJIAN_LJFM As String = "连接符名"
        Public Const FIELD_CX_B_CHAXUNTIAOJIAN_LJFZ As String = "连接符值"
        Public Const FIELD_CX_B_CHAXUNTIAOJIAN_ZDLX As String = "字段类型"
        '约束错误信息








        '定义初始化表类型enum
        Public Enum enumTableType
            CX_B_CHAXUNTIAOJIAN = 1
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.Common.Data.QueryData)
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
                Case enumTableType.CX_B_CHAXUNTIAOJIAN
                    table = createDataTables_Chaxuntiaojian(strErrMsg)
                Case Else
                    strErrMsg = "无效的表类型！"
                    table = Nothing
            End Select

            createDataTables = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_CX_B_CHAXUNTIAOJIAN
        '----------------------------------------------------------------
        Private Function createDataTables_Chaxuntiaojian(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_CX_B_CHAXUNTIAOJIAN)
                With table.Columns
                    .Add(FIELD_CX_B_CHAXUNTIAOJIAN_ZKHM, GetType(System.String))
                    .Add(FIELD_CX_B_CHAXUNTIAOJIAN_ZKHZ, GetType(System.String))
                    .Add(FIELD_CX_B_CHAXUNTIAOJIAN_ZDMC, GetType(System.String))
                    .Add(FIELD_CX_B_CHAXUNTIAOJIAN_BJFM, GetType(System.String))
                    .Add(FIELD_CX_B_CHAXUNTIAOJIAN_BJFZ, GetType(System.String))
                    .Add(FIELD_CX_B_CHAXUNTIAOJIAN_VAL1, GetType(System.String))
                    .Add(FIELD_CX_B_CHAXUNTIAOJIAN_VAL2, GetType(System.String))
                    .Add(FIELD_CX_B_CHAXUNTIAOJIAN_YKHM, GetType(System.String))
                    .Add(FIELD_CX_B_CHAXUNTIAOJIAN_YKHZ, GetType(System.String))
                    .Add(FIELD_CX_B_CHAXUNTIAOJIAN_LJFM, GetType(System.String))
                    .Add(FIELD_CX_B_CHAXUNTIAOJIAN_LJFZ, GetType(System.String))
                    .Add(FIELD_CX_B_CHAXUNTIAOJIAN_ZDLX, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Chaxuntiaojian = table

        End Function

    End Class 'QueryData

End Namespace 'Xydc.Platform.Common.Data
