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
    ' 类名    ：grswMyTaskData
    '
    ' 功能描述：
    '     定义“个人_B_我的事宜”表有关的数据访问格式
    '----------------------------------------------------------------
    <System.ComponentModel.DesignerCategory("Code"), SerializableAttribute()> Public Class grswMyTaskData
        Inherits System.Data.DataSet

        '“个人_B_我的事宜_文件”表信息定义
        '表名称
        Public Const TABLE_GR_B_MYTASK_FILE As String = "个人_B_我的事宜_文件"
        '字段序列
        Public Const FIELD_GR_B_MYTASK_FILE_WJBS As String = "文件标识"
        Public Const FIELD_GR_B_MYTASK_FILE_LSH As String = "流水号"
        Public Const FIELD_GR_B_MYTASK_FILE_BLLX As String = "办理类型"
        Public Const FIELD_GR_B_MYTASK_FILE_BLZT As String = "办理状态"
        Public Const FIELD_GR_B_MYTASK_FILE_WJZL As String = "文件子类"
        Public Const FIELD_GR_B_MYTASK_FILE_WJLX As String = "文件类型"
        Public Const FIELD_GR_B_MYTASK_FILE_WJBT As String = "文件标题"
        Public Const FIELD_GR_B_MYTASK_FILE_ZSDW As String = "主送单位"
        Public Const FIELD_GR_B_MYTASK_FILE_WJZH As String = "文件字号"
        Public Const FIELD_GR_B_MYTASK_FILE_MMDJ As String = "秘密等级"
        Public Const FIELD_GR_B_MYTASK_FILE_JJCD As String = "紧急程度"
        Public Const FIELD_GR_B_MYTASK_FILE_JGDZ As String = "机关代字"
        Public Const FIELD_GR_B_MYTASK_FILE_WJNF As String = "文件年份"
        Public Const FIELD_GR_B_MYTASK_FILE_WJXH As String = "文件序号"
        Public Const FIELD_GR_B_MYTASK_FILE_ZTC As String = "主题词"
        Public Const FIELD_GR_B_MYTASK_FILE_ZBDW As String = "主办单位"
        Public Const FIELD_GR_B_MYTASK_FILE_NGR As String = "拟稿人"
        Public Const FIELD_GR_B_MYTASK_FILE_NGRQ As String = "拟稿日期"
        Public Const FIELD_GR_B_MYTASK_FILE_FSRQ As String = "发送日期"
        Public Const FIELD_GR_B_MYTASK_FILE_BLQX As String = "办理期限"
        Public Const FIELD_GR_B_MYTASK_FILE_WCRQ As String = "完成日期"
        Public Const FIELD_GR_B_MYTASK_FILE_KSSW As String = "快速收文"
        Public Const FIELD_GR_B_MYTASK_FILE_BWTX As String = "备忘提醒"
        '约束错误信息

        '“个人_B_我的事宜_任务”表信息定义
        '表名称
        Public Const TABLE_GR_B_MYTASK_TASK As String = "个人_B_我的事宜_任务"
        '字段序列
        Public Const FIELD_GR_B_MYTASK_TASK_WJBS As String = "文件标识"
        Public Const FIELD_GR_B_MYTASK_TASK_LSH As String = "流水号"
        Public Const FIELD_GR_B_MYTASK_TASK_BLLX As String = "办理类型"
        Public Const FIELD_GR_B_MYTASK_TASK_BLZT As String = "办理状态"
        Public Const FIELD_GR_B_MYTASK_TASK_WJZL As String = "文件子类"
        Public Const FIELD_GR_B_MYTASK_TASK_WJLX As String = "文件类型"
        Public Const FIELD_GR_B_MYTASK_TASK_BLZL As String = "办理子类"
        Public Const FIELD_GR_B_MYTASK_TASK_WJBT As String = "文件标题"
        Public Const FIELD_GR_B_MYTASK_TASK_JGDZ As String = "机关代字"
        Public Const FIELD_GR_B_MYTASK_TASK_WJNF As String = "文件年份"
        Public Const FIELD_GR_B_MYTASK_TASK_WJXH As String = "文件序号"
        Public Const FIELD_GR_B_MYTASK_TASK_ZBDW As String = "主办单位"
        Public Const FIELD_GR_B_MYTASK_TASK_JSR As String = "接收人"
        Public Const FIELD_GR_B_MYTASK_TASK_FSR As String = "发送人"
        Public Const FIELD_GR_B_MYTASK_TASK_WTR As String = "委托人"
        Public Const FIELD_GR_B_MYTASK_TASK_JJSM As String = "交接说明"
        '约束错误信息

        '“个人_B_我的事宜_节点”表信息定义
        '分级表：XXX-XXX-XXX-XXX
        '表名称
        Public Const TABLE_GR_B_MYTASK_NODE As String = "个人_B_我的事宜_节点"
        '字段序列
        Public Const FIELD_GR_B_MYTASK_NODE_CODE As String = "节点代码"
        Public Const FIELD_GR_B_MYTASK_NODE_NAME As String = "节点名称"
        Public Const FIELD_GR_B_MYTASK_NODE_KSSJ As String = "开始时间"
        Public Const FIELD_GR_B_MYTASK_NODE_JSSJ As String = "结束时间"
        Public Const FIELD_GR_B_MYTASK_NODE_WJLX As String = "文件类型"
        Public Const FIELD_GR_B_MYTASK_NODE_BLLX As String = "办理类型"
        '约束错误信息

        '节点代码分级长度说明
        Public Shared intJDDM_FJCDSM() As Integer = {3, 6, 9}








        '定义初始化表类型enum
        Public Enum enumTableType
            GR_B_MYTASK_FILE = 1
            GR_B_MYTASK_TASK = 2
            GR_B_MYTASK_NODE = 3
        End Enum

        Public Enum enumTaskTypeLevel1
            DBSY = 1  '待办事宜
            BWTX = 2  '备忘提醒
            DPWJ = 3  '待批文件
            HBWJ = 4  '缓办文件
            YBSY = 5  '已办事宜
            GQSY = 6  '过期事宜
            CBSY = 7  '催办事宜
            BCSY = 8  '被催事宜
            DBWJ = 9  '督办文件
            BDWJ = 10 '被督文件
            QBSY = 11 '全部事宜
        End Enum

        Public Enum enumTaskTypeLevel2
            JINTIAN = 1 '今天
            BENZHOU = 2 '本周
            BENYUEN = 3 '本月
            BENYUES = 4 '本月以前/以上
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.Common.Data.grswMyTaskData)
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
                Case enumTableType.GR_B_MYTASK_FILE
                    table = createDataTables_MyTask_File(strErrMsg)
                Case enumTableType.GR_B_MYTASK_TASK
                    table = createDataTables_MyTask_Task(strErrMsg)
                Case enumTableType.GR_B_MYTASK_NODE
                    table = createDataTables_MyTask_Node(strErrMsg)
                Case Else
                    strErrMsg = "无效的表类型！"
                    table = Nothing
            End Select

            createDataTables = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GR_B_MYTASK_FILE
        '----------------------------------------------------------------
        Private Function createDataTables_MyTask_File(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GR_B_MYTASK_FILE)
                With table.Columns
                    .Add(FIELD_GR_B_MYTASK_FILE_WJBS, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_FILE_LSH, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_FILE_BLLX, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_FILE_BLZT, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_FILE_WJZL, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_FILE_WJLX, GetType(System.String))

                    .Add(FIELD_GR_B_MYTASK_FILE_WJBT, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_FILE_ZSDW, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_FILE_WJZH, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_FILE_MMDJ, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_FILE_JJCD, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_FILE_JGDZ, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_FILE_WJNF, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_FILE_WJXH, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_FILE_ZTC, GetType(System.String))

                    .Add(FIELD_GR_B_MYTASK_FILE_ZBDW, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_FILE_NGR, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_FILE_NGRQ, GetType(System.DateTime))

                    .Add(FIELD_GR_B_MYTASK_FILE_FSRQ, GetType(System.DateTime))
                    .Add(FIELD_GR_B_MYTASK_FILE_BLQX, GetType(System.DateTime))
                    .Add(FIELD_GR_B_MYTASK_FILE_WCRQ, GetType(System.DateTime))

                    .Add(FIELD_GR_B_MYTASK_FILE_KSSW, GetType(System.Int32))
                    .Add(FIELD_GR_B_MYTASK_FILE_BWTX, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_MyTask_File = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GR_B_MYTASK_TASK
        '----------------------------------------------------------------
        Private Function createDataTables_MyTask_Task(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GR_B_MYTASK_TASK)
                With table.Columns
                    .Add(FIELD_GR_B_MYTASK_TASK_WJBS, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_TASK_LSH, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_TASK_BLLX, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_TASK_BLZL, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_TASK_BLZT, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_TASK_WJZL, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_TASK_WJLX, GetType(System.String))

                    .Add(FIELD_GR_B_MYTASK_TASK_WJBT, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_TASK_JGDZ, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_TASK_WJNF, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_TASK_WJXH, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_TASK_ZBDW, GetType(System.String))

                    .Add(FIELD_GR_B_MYTASK_TASK_FSR, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_TASK_JSR, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_TASK_WTR, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_TASK_JJSM, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_MyTask_Task = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GR_B_MYTASK_NODE
        '----------------------------------------------------------------
        Private Function createDataTables_MyTask_Node(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GR_B_MYTASK_NODE)
                With table.Columns
                    .Add(FIELD_GR_B_MYTASK_NODE_CODE, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_NODE_NAME, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_NODE_KSSJ, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_NODE_JSSJ, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_NODE_WJLX, GetType(System.String))
                    .Add(FIELD_GR_B_MYTASK_NODE_BLLX, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_MyTask_Node = table

        End Function

    End Class 'grswMyTaskData

End Namespace 'Xydc.Platform.Common.Data
