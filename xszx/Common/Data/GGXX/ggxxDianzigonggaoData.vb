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
    ' 类名    ：ggxxDianzigonggaoData
    '
    ' 功能描述：
    '     定义“电子公告”有关的数据访问格式
    '----------------------------------------------------------------
    <System.ComponentModel.DesignerCategory("Code"), SerializableAttribute()> Public Class ggxxDianzigonggaoData
        Inherits System.Data.DataSet

        '“个人_B_公告栏”表信息定义
        '表名称
        Public Const TABLE_GR_B_GONGGAOLAN As String = "个人_B_公告栏"
        '字段序列
        Public Const FIELD_GR_B_GONGGAOLAN_CZYDM As String = "操作员代码"
        Public Const FIELD_GR_B_GONGGAOLAN_XH As String = "序号"
        Public Const FIELD_GR_B_GONGGAOLAN_WJBS As String = "文件标识"
        Public Const FIELD_GR_B_GONGGAOLAN_ZZDM As String = "组织代码"
        Public Const FIELD_GR_B_GONGGAOLAN_ZZMC As String = "组织名称"
        Public Const FIELD_GR_B_GONGGAOLAN_CZY As String = "操作员"
        Public Const FIELD_GR_B_GONGGAOLAN_RQ As String = "日期"
        Public Const FIELD_GR_B_GONGGAOLAN_BT As String = "标题"
        Public Const FIELD_GR_B_GONGGAOLAN_NR As String = "内容"
        Public Const FIELD_GR_B_GONGGAOLAN_ZWNR As String = "正文内容"
        Public Const FIELD_GR_B_GONGGAOLAN_BLRQ As String = "保留日期"
        Public Const FIELD_GR_B_GONGGAOLAN_FBBS As String = "发布标识"
        Public Const FIELD_GR_B_GONGGAOLAN_YDKZ As String = "阅读控制"
        Public Const FIELD_GR_B_GONGGAOLAN_YDFW As String = "阅读范围"
        '计算字段
        Public Const FIELD_GR_B_GONGGAOLAN_SFYD As String = "是否阅读"
        Public Const FIELD_GR_B_GONGGAOLAN_FBMS As String = "发布描述"
        '约束错误信息

        Public Enum enumFileDownloadStatus
            NotDownload = 0 '没有下载
            HasDownload = 1 '已经下载
        End Enum



        '“电子公告_B_附件”表信息定义
        '表名称
        Public Const TABLE_DZGG_B_FUJIAN As String = "电子公告_B_附件"
        '字段序列
        Public Const FIELD_DZGG_B_FUJIAN_WJBS As String = "文件标识"
        Public Const FIELD_DZGG_B_FUJIAN_WJXH As String = "序号"
        Public Const FIELD_DZGG_B_FUJIAN_WJSM As String = "说明"
        Public Const FIELD_DZGG_B_FUJIAN_WJYS As String = "页数"
        Public Const FIELD_DZGG_B_FUJIAN_WJWZ As String = "位置"        '服务器文件位置(相对于FTP根的路径)
        '附加信息(显示/编辑时用)
        Public Const FIELD_DZGG_B_FUJIAN_XSXH As String = "显示序号"
        Public Const FIELD_DZGG_B_FUJIAN_BDWJ As String = "本地文件"    '下载后的文件位置(完整路径)
        Public Const FIELD_DZGG_B_FUJIAN_XZBZ As String = "下载标志"    '是否下载?
        '约束错误信息


        '目录设定
        Public Const FILEDIR_GJ As String = "DZGG\GJ"          '电子公告正文内容目录
        Public Const FILEDIR_HJ As String = "DZGG\HJ"          '电子公告痕迹文件目录

        Public Const FILEDIR_FJ As String = "DZGG\FJ"          '电子公告附件目录


        '“个人_B_公告栏阅读情况”表信息定义
        '表名称
        Public Const TABLE_GR_B_GONGGAOLAN_YUEDUQINGKUANG As String = "个人_B_公告栏阅读情况"
        '字段序列
        Public Const FIELD_GR_B_GONGGAOLAN_YUEDUQINGKUANG_CZYDM As String = "操作员代码"
        Public Const FIELD_GR_B_GONGGAOLAN_YUEDUQINGKUANG_XH As String = "序号"
        Public Const FIELD_GR_B_GONGGAOLAN_YUEDUQINGKUANG_YDRY As String = "阅读人员"
        '附加信息(显示/编辑时用)
        '约束错误信息

        '“个人_B_公告栏阅读范围”表信息定义
        '表名称
        Public Const TABLE_GR_B_GONGGAOLAN_YUEDUFANWEI As String = "个人_B_公告栏阅读范围"
        '字段序列
        Public Const FIELD_GR_B_GONGGAOLAN_YUEDUFANWEI_CZYDM As String = "操作员代码"
        Public Const FIELD_GR_B_GONGGAOLAN_YUEDUFANWEI_XH As String = "序号"
        Public Const FIELD_GR_B_GONGGAOLAN_YUEDUFANWEI_YDRY As String = "阅读人员"
        '附加信息(显示/编辑时用)
        '约束错误信息




        '定义初始化表类型enum
        Public Enum enumTableType
            GR_B_GONGGAOLAN = 1
            GR_B_GONGGAOLAN_YUEDUQINGKUANG = 2
            GR_B_GONGGAOLAN_YUEDUFANWEI = 3
            DZGG_B_FUJIAN = 4
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.Common.Data.ggxxDianzigonggaoData)
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
                Case enumTableType.GR_B_GONGGAOLAN
                    table = createDataTables_Gonggaolan(strErrMsg)

                Case enumTableType.GR_B_GONGGAOLAN_YUEDUQINGKUANG
                    table = createDataTables_Gonggaolan_YueduQingkuang(strErrMsg)

                Case enumTableType.GR_B_GONGGAOLAN_YUEDUFANWEI
                    table = createDataTables_Gonggaolan_YueduFanwei(strErrMsg)

                Case enumTableType.DZGG_B_FUJIAN
                    table = createDataTables_DZGG_FUJIAN(strErrMsg)
                Case Else
                    strErrMsg = "无效的表类型！"
                    table = Nothing
            End Select

            createDataTables = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_DZGG_B_FUJIAN
        '----------------------------------------------------------------
        Private Function createDataTables_DZGG_FUJIAN(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_DZGG_B_FUJIAN)
                With table.Columns
                    .Add(FIELD_DZGG_B_FUJIAN_WJBS, GetType(System.String))
                    .Add(FIELD_DZGG_B_FUJIAN_WJXH, GetType(System.Int32))

                    .Add(FIELD_DZGG_B_FUJIAN_WJSM, GetType(System.String))
                    .Add(FIELD_DZGG_B_FUJIAN_WJYS, GetType(System.Int32))
                    .Add(FIELD_DZGG_B_FUJIAN_WJWZ, GetType(System.String))

                    .Add(FIELD_DZGG_B_FUJIAN_XSXH, GetType(System.Int32))
                    .Add(FIELD_DZGG_B_FUJIAN_BDWJ, GetType(System.String))
                    .Add(FIELD_DZGG_B_FUJIAN_XZBZ, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_DZGG_FUJIAN = table

        End Function


        '----------------------------------------------------------------
        '创建TABLE_GR_B_GONGGAOLAN
        '----------------------------------------------------------------
        Private Function createDataTables_Gonggaolan(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GR_B_GONGGAOLAN)
                With table.Columns
                    .Add(FIELD_GR_B_GONGGAOLAN_CZYDM, GetType(System.String))
                    .Add(FIELD_GR_B_GONGGAOLAN_XH, GetType(System.Int32))
                    .Add(FIELD_GR_B_GONGGAOLAN_WJBS, GetType(System.String))
                    .Add(FIELD_GR_B_GONGGAOLAN_ZZDM, GetType(System.String))
                    .Add(FIELD_GR_B_GONGGAOLAN_ZZMC, GetType(System.String))
                    .Add(FIELD_GR_B_GONGGAOLAN_CZY, GetType(System.String))
                    .Add(FIELD_GR_B_GONGGAOLAN_RQ, GetType(System.DateTime))
                    .Add(FIELD_GR_B_GONGGAOLAN_BT, GetType(System.String))
                    .Add(FIELD_GR_B_GONGGAOLAN_NR, GetType(System.String))
                    .Add(FIELD_GR_B_GONGGAOLAN_ZWNR, GetType(System.String))
                    .Add(FIELD_GR_B_GONGGAOLAN_BLRQ, GetType(System.DateTime))
                    .Add(FIELD_GR_B_GONGGAOLAN_FBBS, GetType(System.Int32))
                    .Add(FIELD_GR_B_GONGGAOLAN_YDKZ, GetType(System.String))
                    .Add(FIELD_GR_B_GONGGAOLAN_YDFW, GetType(System.String))

                    .Add(FIELD_GR_B_GONGGAOLAN_SFYD, GetType(System.String))
                    .Add(FIELD_GR_B_GONGGAOLAN_FBMS, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Gonggaolan = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GR_B_GONGGAOLAN_YUEDUQINGKUANG
        '----------------------------------------------------------------
        Private Function createDataTables_Gonggaolan_YueduQingkuang(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GR_B_GONGGAOLAN_YUEDUQINGKUANG)
                With table.Columns
                    .Add(FIELD_GR_B_GONGGAOLAN_YUEDUQINGKUANG_CZYDM, GetType(System.String))
                    .Add(FIELD_GR_B_GONGGAOLAN_YUEDUQINGKUANG_XH, GetType(System.Int32))
                    .Add(FIELD_GR_B_GONGGAOLAN_YUEDUQINGKUANG_YDRY, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Gonggaolan_YueduQingkuang = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GR_B_GONGGAOLAN_YUEDUFANWEI
        '----------------------------------------------------------------
        Private Function createDataTables_Gonggaolan_YueduFanwei(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GR_B_GONGGAOLAN_YUEDUFANWEI)
                With table.Columns
                    .Add(FIELD_GR_B_GONGGAOLAN_YUEDUQINGKUANG_CZYDM, GetType(System.String))
                    .Add(FIELD_GR_B_GONGGAOLAN_YUEDUQINGKUANG_XH, GetType(System.Int32))
                    .Add(FIELD_GR_B_GONGGAOLAN_YUEDUQINGKUANG_YDRY, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Gonggaolan_YueduFanwei = table

        End Function

    End Class 'ggxxDianzigonggaoData

End Namespace 'Xydc.Platform.Common.Data
