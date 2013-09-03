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
    ' 类名    ：JingchanglianxirenData
    '
    ' 功能描述：
    '     定义“公文_B_经常联系人”表有关的数据访问格式
    '----------------------------------------------------------------
    <System.ComponentModel.DesignerCategory("GWDM"), SerializableAttribute()> Public Class JingchanglianxirenData
        Inherits System.Data.DataSet

        '公文_B_经常联系人表信息定义
        '表名称
        Public Const TABLE_GW_B_JINGCHANGLIANXIREN As String = "公文_B_经常联系人"
        '字段序列
        Public Const FIELD_GW_B_JINGCHANGLIANXIREN_LXRDM As String = "联系人代码"
        Public Const FIELD_GW_B_JINGCHANGLIANXIREN_RYDM As String = "人员代码"
        '其他与人员相关的字段引用CustomerData中的定义
        '约束错误信息








        '定义初始化表类型enum
        Public Enum enumTableType
            GW_B_JINGCHANGLIANXIREN = 1
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.Common.Data.JingchanglianxirenData)
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
                Case enumTableType.GW_B_JINGCHANGLIANXIREN
                    table = createDataTables_Jingchanglianxiren(strErrMsg)
                Case Else
                    strErrMsg = "无效的表类型！"
                    table = Nothing
            End Select

            createDataTables = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GW_B_JINGCHANGLIANXIREN
        '----------------------------------------------------------------
        Private Function createDataTables_Jingchanglianxiren(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GW_B_JINGCHANGLIANXIREN)
                With table.Columns
                    '本表字段
                    .Add(FIELD_GW_B_JINGCHANGLIANXIREN_LXRDM, GetType(System.String))
                    .Add(FIELD_GW_B_JINGCHANGLIANXIREN_RYDM, GetType(System.String))

                    '公共_B_人员全部字段
                    .Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYMC, GetType(System.String))
                    .Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYXH, GetType(System.String))
                    .Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_ZZDM, GetType(System.String))
                    .Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_JBDM, GetType(System.String))
                    .Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_MSDM, GetType(System.String))
                    .Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_LXDH, GetType(System.String))
                    .Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SJHM, GetType(System.String))
                    .Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_FTPDZ, GetType(System.String))
                    .Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_YXDZ, GetType(System.String))
                    .Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_ZDQS, GetType(System.String))
                    .Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_JJXSMC, GetType(System.String))
                    .Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_KCKXM, GetType(System.String))
                    .Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_KZSRY, GetType(System.String))
                    .Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_QTYZS, GetType(System.String))
                    .Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SFJM, GetType(System.Int32))

                    '公共_B_组织机构表的组织名称、组织别名
                    .Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_ZZMC, GetType(System.String))
                    .Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_ZZBM, GetType(System.String))

                    '公共_B_上岗表对应的公共_B_工作岗位中的岗位名称集合（分号分隔）
                    .Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_FULLJOIN_GWLB, GetType(System.String))

                    '公共_B_行政级别中的级别名称、行政级别
                    .Add(Xydc.Platform.Common.Data.XingzhengjibieData.FIELD_GG_B_XINGZHENGJIBIE_JBMC, GetType(System.String))
                    .Add(Xydc.Platform.Common.Data.XingzhengjibieData.FIELD_GG_B_XINGZHENGJIBIE_XZJB, GetType(System.Int32))

                    '公共_B_人员中检索出的秘书名称
                    .Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_FULLJOIN_MSMC, GetType(System.String))

                    '是否申请ID?
                    .Add(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_FULLJOIN_SFSQ, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Jingchanglianxiren = table

        End Function

    End Class 'JingchanglianxirenData

End Namespace 'Xydc.Platform.Common.Data
