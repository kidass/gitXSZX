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
    ' 类名    ：ggxxGuizhangzhiduData
    '
    ' 功能描述：
    '     定义“规章制度”有关的数据访问格式
    '----------------------------------------------------------------
    <System.ComponentModel.DesignerCategory("Code"), SerializableAttribute()> Public Class ggxxGuizhangzhiduData
        Inherits System.Data.DataSet

        '“个人_B_制度”表信息定义
        '表名称
        Public Const TABLE_GR_B_ZHIDU As String = "个人_B_制度"
        '字段序列
        Public Const FIELD_GR_B_ZHIDU_BH As String = "编号"
        Public Const FIELD_GR_B_ZHIDU_JB As String = "级别"
        Public Const FIELD_GR_B_ZHIDU_BT As String = "标题"
        Public Const FIELD_GR_B_ZHIDU_NR As String = "内容"
        Public Const FIELD_GR_B_ZHIDU_PXH As String = "排序号"
        Public Const FIELD_GR_B_ZHIDU_SJBH As String = "上级编号"
        Public Const FIELD_GR_B_ZHIDU_FBRQ As String = "发布日期"
        Public Const FIELD_GR_B_ZHIDU_FBDW As String = "发布单位"
        Public Const FIELD_GR_B_ZHIDU_WYBS As String = "唯一标识"
        '计算字段
        '约束错误信息




        '“个人_B_制度”树结构信息定义
        '表名称
        Public Const TABLE_GR_B_ZHIDU_TREE As String = "个人_B_制度_树"
        '字段序列
        Public Const FIELD_GR_B_ZHIDU_TREE_BH As String = "编号"
        Public Const FIELD_GR_B_ZHIDU_TREE_JB As String = "级别"
        Public Const FIELD_GR_B_ZHIDU_TREE_BT As String = "标题"
        Public Const FIELD_GR_B_ZHIDU_TREE_PXH As String = "排序号"
        Public Const FIELD_GR_B_ZHIDU_TREE_SJBH As String = "上级编号"
        '计算字段
        '约束错误信息





        '定义初始化表类型enum
        Public Enum enumTableType
            GR_B_ZHIDU = 1
            GR_B_ZHIDU_TREE = 2
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.Common.Data.ggxxGuizhangzhiduData)
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
                Case enumTableType.GR_B_ZHIDU
                    table = createDataTables_Zhidu(strErrMsg)
                Case enumTableType.GR_B_ZHIDU_TREE
                    table = createDataTables_Zhidu_Tree(strErrMsg)
                Case Else
                    strErrMsg = "无效的表类型！"
                    table = Nothing
            End Select

            createDataTables = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GR_B_ZHIDU
        '----------------------------------------------------------------
        Private Function createDataTables_Zhidu(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GR_B_ZHIDU)
                With table.Columns
                    .Add(FIELD_GR_B_ZHIDU_BH, GetType(System.Int32))
                    .Add(FIELD_GR_B_ZHIDU_JB, GetType(System.Int32))
                    .Add(FIELD_GR_B_ZHIDU_BT, GetType(System.String))
                    .Add(FIELD_GR_B_ZHIDU_NR, GetType(System.String))
                    .Add(FIELD_GR_B_ZHIDU_PXH, GetType(System.Int32))
                    .Add(FIELD_GR_B_ZHIDU_SJBH, GetType(System.Int32))
                    .Add(FIELD_GR_B_ZHIDU_FBRQ, GetType(System.DateTime))
                    .Add(FIELD_GR_B_ZHIDU_FBDW, GetType(System.String))
                    .Add(FIELD_GR_B_ZHIDU_WYBS, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Zhidu = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GR_B_ZHIDU_TREE
        '----------------------------------------------------------------
        Private Function createDataTables_Zhidu_Tree(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GR_B_ZHIDU_TREE)
                With table.Columns
                    .Add(FIELD_GR_B_ZHIDU_TREE_BH, GetType(System.Int32))
                    .Add(FIELD_GR_B_ZHIDU_TREE_JB, GetType(System.Int32))
                    .Add(FIELD_GR_B_ZHIDU_TREE_BT, GetType(System.String))
                    .Add(FIELD_GR_B_ZHIDU_TREE_PXH, GetType(System.Int32))
                    .Add(FIELD_GR_B_ZHIDU_TREE_SJBH, GetType(System.Int32))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Zhidu_Tree = table

        End Function

    End Class 'ggxxGuizhangzhiduData

End Namespace 'Xydc.Platform.Common.Data
