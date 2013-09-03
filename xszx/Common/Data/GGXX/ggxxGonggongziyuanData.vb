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
    ' 类名    ：ggxxGonggongziyuanData
    '
    ' 功能描述：
    '     定义“公共资源”有关的数据访问格式
    '----------------------------------------------------------------
    <System.ComponentModel.DesignerCategory("Code"), SerializableAttribute()> Public Class ggxxGonggongziyuanData
        Inherits System.Data.DataSet

        '公共资源类型
        Public Enum enumZiyuanType
            Text = 0       '纯文本
            Image = 1      '图片文件
            Html = 2       'Html文件
            Office = 3     'Office文件
            Media = 4      '媒体文件
            Other = 5      '其他文件
            Tuwen = 6      '图文
        End Enum

        '公共资源文件目录
        Public Const FILEDIR_GGZY_WJ As String = "GGXX\GGZY\WJ"

        '“信息_B_公共资源_栏目”定义
        '表名称
        Public Const TABLE_XX_B_GONGGONGZIYUAN_LANMU As String = "信息_B_公共资源_栏目"
        '字段序列
        Public Const FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMBS As String = "栏目标识"
        Public Const FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMDM As String = "栏目代码"
        Public Const FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMJB As String = "栏目级别"
        Public Const FIELD_XX_B_GONGGONGZIYUAN_LANMU_BJDM As String = "本级代码"
        Public Const FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMMC As String = "栏目名称"
        Public Const FIELD_XX_B_GONGGONGZIYUAN_LANMU_DJLM As String = "顶级栏目"
        Public Const FIELD_XX_B_GONGGONGZIYUAN_LANMU_SJLM As String = "上级栏目"
        Public Const FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMSM As String = "说明"
        '约束错误信息

        '“信息_B_公共资源”定义
        '表名称
        Public Const TABLE_XX_B_GONGGONGZIYUAN As String = "信息_B_公共资源"
        '字段序列
        Public Const FIELD_XX_B_GONGGONGZIYUAN_ZYBS As String = "资源标识"
        Public Const FIELD_XX_B_GONGGONGZIYUAN_ZYXH As String = "资源序号"
        Public Const FIELD_XX_B_GONGGONGZIYUAN_FBRQ As String = "发布日期"
        Public Const FIELD_XX_B_GONGGONGZIYUAN_LMBS As String = "栏目标识"
        Public Const FIELD_XX_B_GONGGONGZIYUAN_RYDM As String = "人员代码"
        Public Const FIELD_XX_B_GONGGONGZIYUAN_ZZDM As String = "组织代码"
        Public Const FIELD_XX_B_GONGGONGZIYUAN_NRLX As String = "内容类型"
        Public Const FIELD_XX_B_GONGGONGZIYUAN_ZYBT As String = "资源标题"
        Public Const FIELD_XX_B_GONGGONGZIYUAN_ZYNR As String = "资源内容"
        Public Const FIELD_XX_B_GONGGONGZIYUAN_WJWZ As String = "文件位置"
        Public Const FIELD_XX_B_GONGGONGZIYUAN_BLRQ As String = "保留日期"
        Public Const FIELD_XX_B_GONGGONGZIYUAN_FBBS As String = "发布标识"
        Public Const FIELD_XX_B_GONGGONGZIYUAN_FBKZ As String = "发布控制"
        Public Const FIELD_XX_B_GONGGONGZIYUAN_FBFW As String = "发布范围"
        '显示字段序列
        Public Const FIELD_XX_B_GONGGONGZIYUAN_LMMC As String = "栏目名称" '栏目标识
        Public Const FIELD_XX_B_GONGGONGZIYUAN_LMDM As String = "栏目代码" '栏目标识
        Public Const FIELD_XX_B_GONGGONGZIYUAN_RYMC As String = "人员名称" '人员代码
        Public Const FIELD_XX_B_GONGGONGZIYUAN_ZZMC As String = "组织名称" '组织代码
        Public Const FIELD_XX_B_GONGGONGZIYUAN_FBMS As String = "发布描述" '发布标识
        Public Const FIELD_XX_B_GONGGONGZIYUAN_KZMS As String = "控制描述" '发布控制
        Public Const FIELD_XX_B_GONGGONGZIYUAN_YDMS As String = "阅读描述"
        '约束错误信息

        '“信息_B_公共资源_阅读情况”定义
        '表名称
        Public Const TABLE_XX_B_GONGGONGZIYUAN_YUEDUQINGKUANG As String = "信息_B_公共资源_阅读情况"
        '字段序列
        Public Const FIELD_XX_B_GONGGONGZIYUAN_YUEDUQINGKUANG_ZYBS As String = "资源标识"
        Public Const FIELD_XX_B_GONGGONGZIYUAN_YUEDUQINGKUANG_RYDM As String = "人员代码"
        '显示字段序列
        Public Const FIELD_XX_B_GONGGONGZIYUAN_YUEDUQINGKUANG_RYMC As String = "人员名称" '人员代码
        '约束错误信息

        '“信息_B_公共资源_阅读范围”定义
        '表名称
        Public Const TABLE_XX_B_GONGGONGZIYUAN_YUEDUFANWEI As String = "信息_B_公共资源_阅读范围"
        '字段序列
        Public Const FIELD_XX_B_GONGGONGZIYUAN_YUEDUFANWEI_ZYBS As String = "资源标识"
        Public Const FIELD_XX_B_GONGGONGZIYUAN_YUEDUFANWEI_RYDM As String = "人员代码"
        '显示字段序列
        Public Const FIELD_XX_B_GONGGONGZIYUAN_YUEDUFANWEI_RYMC As String = "人员名称" '人员代码
        '约束错误信息


        '定义初始化表类型enum
        Public Enum enumTableType
            XX_B_GONGGONGZIYUAN_LANMU = 1
            XX_B_GONGGONGZIYUAN = 2
            XX_B_GONGGONGZIYUAN_YUEDUQINGKUANG = 3
            XX_B_GONGGONGZIYUAN_YUEDUFANWEI = 4
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.Common.Data.ggxxGonggongziyuanData)
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
                Case enumTableType.XX_B_GONGGONGZIYUAN_LANMU
                    table = createDataTables_Lanmu(strErrMsg)

                Case enumTableType.XX_B_GONGGONGZIYUAN
                    table = createDataTables_Ziyuan(strErrMsg)
                Case enumTableType.XX_B_GONGGONGZIYUAN_YUEDUQINGKUANG
                    table = createDataTables_Ziyuan_YueduQingkuang(strErrMsg)
                Case enumTableType.XX_B_GONGGONGZIYUAN_YUEDUFANWEI
                    table = createDataTables_Ziyuan_YueduFanwei(strErrMsg)

                Case Else
                    strErrMsg = "无效的表类型！"
                    table = Nothing
            End Select

            createDataTables = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_XX_B_GONGGONGZIYUAN_LANMU
        '----------------------------------------------------------------
        Private Function createDataTables_Lanmu(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_XX_B_GONGGONGZIYUAN_LANMU)
                With table.Columns
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMBS, GetType(System.Int32))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMDM, GetType(System.String))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMJB, GetType(System.Int32))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_LANMU_BJDM, GetType(System.Int32))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMMC, GetType(System.String))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_LANMU_DJLM, GetType(System.Int32))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_LANMU_SJLM, GetType(System.Int32))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMSM, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Lanmu = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_XX_B_GONGGONGZIYUAN
        '----------------------------------------------------------------
        Private Function createDataTables_Ziyuan(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_XX_B_GONGGONGZIYUAN)
                With table.Columns
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_ZYBS, GetType(System.String))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_ZYXH, GetType(System.Int32))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_FBRQ, GetType(System.DateTime))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_LMBS, GetType(System.String))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_RYDM, GetType(System.String))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_ZZDM, GetType(System.String))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_NRLX, GetType(System.Int32))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_ZYBT, GetType(System.String))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_ZYNR, GetType(System.String))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_WJWZ, GetType(System.String))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_BLRQ, GetType(System.DateTime))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_FBBS, GetType(System.Int32))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_FBKZ, GetType(System.Int32))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_FBFW, GetType(System.String))

                    .Add(FIELD_XX_B_GONGGONGZIYUAN_LMMC, GetType(System.String))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_RYMC, GetType(System.String))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_ZZMC, GetType(System.String))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_FBMS, GetType(System.String))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_KZMS, GetType(System.String))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_YDMS, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Ziyuan = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_XX_B_GONGGONGZIYUAN_YUEDUQINGKUANG
        '----------------------------------------------------------------
        Private Function createDataTables_Ziyuan_YueduQingkuang(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_XX_B_GONGGONGZIYUAN_YUEDUQINGKUANG)
                With table.Columns
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_YUEDUQINGKUANG_ZYBS, GetType(System.String))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_YUEDUQINGKUANG_RYDM, GetType(System.String))

                    .Add(FIELD_XX_B_GONGGONGZIYUAN_YUEDUQINGKUANG_RYMC, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Ziyuan_YueduQingkuang = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_XX_B_GONGGONGZIYUAN_YUEDUFANWEI
        '----------------------------------------------------------------
        Private Function createDataTables_Ziyuan_YueduFanwei(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_XX_B_GONGGONGZIYUAN_YUEDUFANWEI)
                With table.Columns
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_YUEDUFANWEI_ZYBS, GetType(System.String))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_YUEDUFANWEI_RYDM, GetType(System.String))

                    .Add(FIELD_XX_B_GONGGONGZIYUAN_YUEDUFANWEI_RYMC, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Ziyuan_YueduFanwei = table

        End Function

    End Class 'ggxxGonggongziyuanData

End Namespace 'Xydc.Platform.Common.Data
