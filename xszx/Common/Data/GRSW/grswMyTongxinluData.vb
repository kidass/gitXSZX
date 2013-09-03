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
    ' 类名    ：grswMyTongxinluData
    '
    ' 功能描述：
    '     定义“个人_B_通讯录”表有关的数据访问格式
    '----------------------------------------------------------------
    <System.ComponentModel.DesignerCategory("Code"), SerializableAttribute()> Public Class grswMyTongxinluData
        Inherits System.Data.DataSet

        '常量

        '“个人_B_通讯录”表信息定义
        '表名称
        Public Const TABLE_GR_B_TONGXINLU As String = "个人_B_通讯录"
        '字段序列
        Public Const FIELD_GR_B_TONGXINLU_XH As String = "序号"
        Public Const FIELD_GR_B_TONGXINLU_SYZ As String = "所有者"
        Public Const FIELD_GR_B_TONGXINLU_PX As String = "排序"
        Public Const FIELD_GR_B_TONGXINLU_XM As String = "姓名"
        Public Const FIELD_GR_B_TONGXINLU_DZYJ As String = "电子邮件"
        Public Const FIELD_GR_B_TONGXINLU_YDDH As String = "移动电话"
        Public Const FIELD_GR_B_TONGXINLU_XHJ As String = "寻呼机"
        Public Const FIELD_GR_B_TONGXINLU_GRWY As String = "个人网页"
        Public Const FIELD_GR_B_TONGXINLU_JTDZ As String = "家庭地址"
        Public Const FIELD_GR_B_TONGXINLU_ZZDH As String = "住宅电话"
        Public Const FIELD_GR_B_TONGXINLU_JTYB As String = "家庭邮编"
        Public Const FIELD_GR_B_TONGXINLU_DWMC As String = "单位名称"
        Public Const FIELD_GR_B_TONGXINLU_DWDZ As String = "单位地址"
        Public Const FIELD_GR_B_TONGXINLU_DWYB As String = "单位邮编"
        Public Const FIELD_GR_B_TONGXINLU_BGDH As String = "办公电话"
        Public Const FIELD_GR_B_TONGXINLU_YWCZ As String = "业务传真"
        Public Const FIELD_GR_B_TONGXINLU_ZW As String = "职务"
        Public Const FIELD_GR_B_TONGXINLU_BM As String = "部门"
        Public Const FIELD_GR_B_TONGXINLU_BGS As String = "办公室"
        Public Const FIELD_GR_B_TONGXINLU_DWWY As String = "单位网页"
        '显示字段序列
        '约束错误信息









        '定义初始化表类型enum
        Public Enum enumTableType
            GR_B_TONGXINLU = 1
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.Common.Data.grswMyTongxinluData)
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
                Case enumTableType.GR_B_TONGXINLU
                    table = createDataTables_MyTongxinlu(strErrMsg)
                Case Else
                    strErrMsg = "无效的表类型！"
                    table = Nothing
            End Select

            createDataTables = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GR_B_TONGXINLU
        '----------------------------------------------------------------
        Private Function createDataTables_MyTongxinlu(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GR_B_TONGXINLU)
                With table.Columns
                    .Add(FIELD_GR_B_TONGXINLU_XH, GetType(System.Int32))
                    .Add(FIELD_GR_B_TONGXINLU_SYZ, GetType(System.String))
                    .Add(FIELD_GR_B_TONGXINLU_PX, GetType(System.Int32))

                    .Add(FIELD_GR_B_TONGXINLU_XM, GetType(System.String))
                    .Add(FIELD_GR_B_TONGXINLU_DZYJ, GetType(System.String))
                    .Add(FIELD_GR_B_TONGXINLU_YDDH, GetType(System.String))
                    .Add(FIELD_GR_B_TONGXINLU_XHJ, GetType(System.String))
                    .Add(FIELD_GR_B_TONGXINLU_GRWY, GetType(System.String))

                    .Add(FIELD_GR_B_TONGXINLU_JTDZ, GetType(System.String))
                    .Add(FIELD_GR_B_TONGXINLU_ZZDH, GetType(System.String))
                    .Add(FIELD_GR_B_TONGXINLU_JTYB, GetType(System.String))

                    .Add(FIELD_GR_B_TONGXINLU_DWMC, GetType(System.String))
                    .Add(FIELD_GR_B_TONGXINLU_DWDZ, GetType(System.String))
                    .Add(FIELD_GR_B_TONGXINLU_DWYB, GetType(System.String))
                    .Add(FIELD_GR_B_TONGXINLU_BGDH, GetType(System.String))
                    .Add(FIELD_GR_B_TONGXINLU_YWCZ, GetType(System.String))
                    .Add(FIELD_GR_B_TONGXINLU_ZW, GetType(System.String))
                    .Add(FIELD_GR_B_TONGXINLU_BM, GetType(System.String))
                    .Add(FIELD_GR_B_TONGXINLU_BGS, GetType(System.String))
                    .Add(FIELD_GR_B_TONGXINLU_DWWY, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_MyTongxinlu = table

        End Function

    End Class 'grswMyTongxinluData

End Namespace 'Xydc.Platform.Common.Data
