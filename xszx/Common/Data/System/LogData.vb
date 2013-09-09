Option Explicit On

Imports System
Imports System.Data
Imports System.Runtime.Serialization

Namespace Xydc.Platform.Common.Data
    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.Common.Data
    ' 类名    ：system_logData
    '
    ' 功能描述：
    '     定义“LogData”有关的数据访问格式
    '----------------------------------------------------------------
    <System.ComponentModel.DesignerCategory("System"), SerializableAttribute()> Public Class LogData
        Inherits System.Data.DataSet

        'LOG公共字段
        Public Const FIELD_System_B_OperateLog_ID As String = "LogID"
        Public Const FIELD_System_B_OperateLog_UserHostAddress As String = "UserHostAddress"
        Public Const FIELD_System_B_OperateLog_UserHostName As String = "UserHostName"
        Public Const FIELD_System_B_OperateLog_UserID As String = "UserID"
        Public Const FIELD_System_B_OperateLog_OperateTime As String = "OperateTime"



        '“应用操作日志表:[System_B_OperateLog]”表信息定义
        '表名称
        Public Const TABLE_System_B_OperateLog As String = "System_B_OperateLog"
        '字段
        Public Const FIELD_System_B_OperateLog_OperateType As String = "OperateType"
        Public Const FIELD_System_B_OperateLog_OperateContent As String = "OperateContent"
        Public Const FIELD_System_B_OperateLog_OperateTable As String = "OperateTable"

        '“应用操作日志表:[system_B_VisitLog]”表信息定义
        '表名称
        Public Const TABLE_System_B_VisitLog As String = "system_B_VisitLog"
        '字段
        Public Const FIELD_System_B_VisitLog_VisitURL As String = "VisitURL"
        Public Const FIELD_System_B_VisitLog_VisitModel As String = "VisitModel"

        '定义操作
        Public Const OperateType_select As String = "select"
        Public Const OperateType_insert As String = "insert"
        Public Const OperateType_update As String = "update"
        Public Const OperateType_delete As String = "delete"

        '定义初始化表类型enum
        Public Enum enumTableType
            System_B_OperateLog = 1
            System_B_VisitLog = 2
        End Enum


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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.Common.Data.LogData)
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
                Case enumTableType.System_B_OperateLog
                    table = createDataTables_OperateLog(strErrMsg)
                Case enumTableType.System_B_VisitLog
                    table = createDataTables_VisitLog(strErrMsg)

                Case Else
                    strErrMsg = "无效的表类型！"
                    table = Nothing
            End Select

            createDataTables = table

        End Function



        '----------------------------------------------------------------
        '创建TABLE_System_B_VisitLog
        '----------------------------------------------------------------
        Private Function createDataTables_VisitLog(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_System_B_VisitLog)
                With table.Columns

                    .Add(FIELD_System_B_OperateLog_ID, GetType(System.Int32))
                    .Add(FIELD_System_B_OperateLog_UserHostAddress, GetType(System.String))
                    .Add(FIELD_System_B_OperateLog_UserHostName, GetType(System.String))
                    .Add(FIELD_System_B_OperateLog_UserID, GetType(System.String))
                    .Add(FIELD_System_B_OperateLog_OperateTime, GetType(System.DateTime))
                    .Add(FIELD_System_B_VisitLog_VisitURL, GetType(System.String))
                    .Add(FIELD_System_B_VisitLog_VisitModel, GetType(System.String))


                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_VisitLog = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_System_B_OperateLog
        '----------------------------------------------------------------
        Private Function createDataTables_OperateLog(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_System_B_OperateLog)
                With table.Columns

                    .Add(FIELD_System_B_OperateLog_ID, GetType(System.Int32))
                    .Add(FIELD_System_B_OperateLog_UserHostAddress, GetType(System.String))
                    .Add(FIELD_System_B_OperateLog_UserHostName, GetType(System.String))
                    .Add(FIELD_System_B_OperateLog_UserID, GetType(System.String))
                    .Add(FIELD_System_B_OperateLog_OperateTime, GetType(System.DateTime))
                    .Add(FIELD_System_B_OperateLog_OperateType, GetType(System.String))
                    .Add(FIELD_System_B_OperateLog_OperateContent, GetType(System.String))
                    .Add(FIELD_System_B_OperateLog_OperateTable, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_OperateLog = table

        End Function
    End Class
End Namespace
