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
    ' 类名    ：DubanshezhiData
    '
    ' 功能描述：
    '   　定义“管理_B_督办设置”表相关的数据访问格式
    '----------------------------------------------------------------
    <System.ComponentModel.DesignerCategory("XTPZ"), SerializableAttribute()> Public Class DubanshezhiData
        Inherits System.Data.DataSet

        '管理_B_督办设置表信息定义
        '表名称
        Public Const TABLE_GL_B_DUBANSHEZHI As String = "管理_B_督办设置"
        '字段序列
        Public Const FIELD_GL_B_DUBANSHEZHI_GWDM As String = "岗位代码"
        Public Const FIELD_GL_B_DUBANSHEZHI_DBFW As String = "督办范围"
        Public Const FIELD_GL_B_DUBANSHEZHI_JSXZ As String = "级数限制"
        '显示字段
        Public Const FIELD_GL_B_DUBANSHEZHI_GWMC As String = "岗位名称"
        Public Const FIELD_GL_B_DUBANSHEZHI_DBFWMC As String = "督办范围名称"
        Public Const FIELD_GL_B_DUBANSHEZHI_JSXZMC As String = "级数限制名称"
        '约束错误信息








        '定义初始化表类型enum
        Public Enum enumTableType
            GL_B_DUBANSHEZHI = 1
        End Enum

        '定义督办范围列表
        Public Enum enumDubanfanweiType
            All = 0            '整个单位(不限制)
            Level = 1          '指定级别以下单位均可
            BumenLevel = 2     '所在部门指定级别以下的单位均可
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.Common.Data.DubanshezhiData)
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
                Case enumTableType.GL_B_DUBANSHEZHI
                    table = createDataTables_Dubanshezhi(strErrMsg)
                Case Else
                    strErrMsg = "无效的表类型！"
                    table = Nothing
            End Select

            createDataTables = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GL_B_DUBANSHEZHI
        '----------------------------------------------------------------
        Private Function createDataTables_Dubanshezhi(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GL_B_DUBANSHEZHI)
                With table.Columns
                    .Add(FIELD_GL_B_DUBANSHEZHI_GWDM, GetType(System.String))
                    .Add(FIELD_GL_B_DUBANSHEZHI_DBFW, GetType(System.Int32))
                    .Add(FIELD_GL_B_DUBANSHEZHI_JSXZ, GetType(System.Int32))

                    .Add(FIELD_GL_B_DUBANSHEZHI_GWMC, GetType(System.String))
                    .Add(FIELD_GL_B_DUBANSHEZHI_DBFWMC, GetType(System.String))
                    .Add(FIELD_GL_B_DUBANSHEZHI_JSXZMC, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Dubanshezhi = table

        End Function

    End Class 'DubanshezhiData

End Namespace 'Xydc.Platform.Common.Data
