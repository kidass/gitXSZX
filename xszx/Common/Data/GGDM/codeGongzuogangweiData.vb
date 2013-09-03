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
    ' 类名    ：GongzuogangweiData
    '
    ' 功能描述：
    '   　定义“公共_B_工作岗位”表相关的数据访问格式
    '----------------------------------------------------------------
    <System.ComponentModel.DesignerCategory("GGDM"), SerializableAttribute()> Public Class GongzuogangweiData
        Inherits System.Data.DataSet

        '“公共_B_工作岗位”表信息定义
        '表名称
        Public Const TABLE_GG_B_GONGZUOGANGWEI As String = "公共_B_工作岗位"
        '字段序列
        Public Const FIELD_GG_B_GONGZUOGANGWEI_GWDM As String = "岗位代码"
        Public Const FIELD_GG_B_GONGZUOGANGWEI_GWMC As String = "岗位名称"
        '约束错误信息

        '“公共_B_VT_选定工作岗位”虚拟表信息定义
        '表名称
        Public Const TABLE_GG_B_VT_SELGONGZUOGANGWEI As String = "公共_B_VT_选定工作岗位"
        '字段序列
        Public Const FIELD_GG_B_VT_SELGONGZUOGANGWEI_GWMC As String = "岗位名称"
        '约束错误信息








        '定义初始化表类型enum
        Public Enum enumTableType
            GG_B_GONGZUOGANGWEI = 1
            GG_B_VT_SELGONGZUOGANGWEI = 2
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.Common.Data.GongzuogangweiData)
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
                Case enumTableType.GG_B_GONGZUOGANGWEI
                    table = createDataTables_Gongzuogangwei(strErrMsg)
                Case enumTableType.GG_B_VT_SELGONGZUOGANGWEI
                    table = createDataTables_SelGongzuogangwei(strErrMsg)
                Case Else
                    strErrMsg = "无效的表类型！"
                    table = Nothing
            End Select

            createDataTables = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GG_B_GONGZUOGANGWEI
        '----------------------------------------------------------------
        Private Function createDataTables_Gongzuogangwei(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GG_B_GONGZUOGANGWEI)
                With table.Columns
                    .Add(FIELD_GG_B_GONGZUOGANGWEI_GWDM, GetType(System.String))
                    .Add(FIELD_GG_B_GONGZUOGANGWEI_GWMC, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Gongzuogangwei = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GG_B_VT_SELGONGZUOGANGWEI
        '----------------------------------------------------------------
        Private Function createDataTables_SelGongzuogangwei(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GG_B_VT_SELGONGZUOGANGWEI)
                With table.Columns
                    .Add(FIELD_GG_B_VT_SELGONGZUOGANGWEI_GWMC, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_SelGongzuogangwei = table

        End Function

    End Class 'GongzuogangweiData

End Namespace 'Xydc.Platform.Common.Data
