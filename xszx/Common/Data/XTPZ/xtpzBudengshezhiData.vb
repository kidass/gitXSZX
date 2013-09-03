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
    ' 类名    ：BudengshezhiData
    '
    ' 功能描述：
    '   　定义“管理_B_补登设置”表相关的数据访问格式
    '----------------------------------------------------------------
    <System.ComponentModel.DesignerCategory("XTPZ"), SerializableAttribute()> Public Class BudengshezhiData
        Inherits System.Data.DataSet

        '管理_B_补登设置表信息定义
        '表名称
        Public Const TABLE_GL_B_BUDENGSHEZHI As String = "管理_B_补登设置"
        '字段序列
        Public Const FIELD_GL_B_BUDENGSHEZHI_GWDM As String = "岗位代码"
        Public Const FIELD_GL_B_BUDENGSHEZHI_BDFW As String = "补登范围"
        Public Const FIELD_GL_B_BUDENGSHEZHI_ZWLB As String = "职务列表"
        Public Const FIELD_GL_B_BUDENGSHEZHI_JSXZ As String = "级数限制"
        Public Const FIELD_GL_B_BUDENGSHEZHI_GWMC As String = "岗位名称"
        Public Const FIELD_GL_B_BUDENGSHEZHI_BDFWMC As String = "补登范围名称"
        Public Const FIELD_GL_B_BUDENGSHEZHI_JSXZMC As String = "级数限制名称"
        '约束错误信息








        '定义初始化表类型enum
        Public Enum enumTableType
            GL_B_BUDENGSHEZHI = 1
        End Enum

        '定义补登范围列表
        Public Enum enumBudengfanweiType
            All = 0                 '整个单位(不限制)
            Zhiwu = 1               '可以补登指定职务的所有人
            ZhiwuBumenLevel = 2     '可以补登所在单位指定单位级别以下的指定职务的所有人
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.Common.Data.BudengshezhiData)
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
                Case enumTableType.GL_B_BUDENGSHEZHI
                    table = createDataTables_Budengshezhi(strErrMsg)
                Case Else
                    strErrMsg = "无效的表类型！"
                    table = Nothing
            End Select

            createDataTables = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GL_B_BUDENGSHEZHI
        '----------------------------------------------------------------
        Private Function createDataTables_Budengshezhi(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GL_B_BUDENGSHEZHI)
                With table.Columns
                    .Add(FIELD_GL_B_BUDENGSHEZHI_GWDM, GetType(System.String))
                    .Add(FIELD_GL_B_BUDENGSHEZHI_BDFW, GetType(System.Int32))
                    .Add(FIELD_GL_B_BUDENGSHEZHI_ZWLB, GetType(System.String))
                    .Add(FIELD_GL_B_BUDENGSHEZHI_JSXZ, GetType(System.Int32))

                    .Add(FIELD_GL_B_BUDENGSHEZHI_GWMC, GetType(System.String))
                    .Add(FIELD_GL_B_BUDENGSHEZHI_BDFWMC, GetType(System.String))
                    .Add(FIELD_GL_B_BUDENGSHEZHI_JSXZMC, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Budengshezhi = table

        End Function

    End Class 'BudengshezhiData

End Namespace 'Xydc.Platform.Common.Data
