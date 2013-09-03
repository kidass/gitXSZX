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
Imports System.Text.RegularExpressions
Imports Microsoft.VisualBasic

Imports Xydc.Platform.SystemFramework
Imports Xydc.Platform.Common
Imports Xydc.Platform.Common.Data
Imports Xydc.Platform.DataAccess

Namespace Xydc.Platform.BusinessRules

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessRules
    ' 类名    ：rulesMyTask
    '
    ' 功能描述： 
    '     提供对“我的事宜”模块涉及的业务逻辑层操作
    '----------------------------------------------------------------
    Public Class rulesMyTask

        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
        End Sub

        '----------------------------------------------------------------
        ' 安全释放本身资源
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessRules.rulesMyTask)
            Try
                If Not (obj Is Nothing) Then
                    'obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub









        '----------------------------------------------------------------
        ' 获取“个人_B_我的事宜_节点”的数据集
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserId              ：用户标识
        '     strPassword            ：用户密码
        '     objgrswMyTaskData      ：信息数据集
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function getMyTaskNodeData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef objgrswMyTaskData As Xydc.Platform.Common.Data.grswMyTaskData) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacMyTask
                    getMyTaskNodeData = .getMyTaskNodeData(strErrMsg, strUserId, strPassword, objgrswMyTaskData)
                End With
            Catch ex As Exception
                getMyTaskNodeData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据给定代码获取对应的数据行数据
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strCode                ：给定节点代码(唯一性保证)
        '     objgrswMyTaskData      ：节点信息数据集
        '     objNodeData            ：(返回)指定节点的数据行数据
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function getMyTaskNodeData( _
            ByRef strErrMsg As String, _
            ByVal strCode As String, _
            ByVal objgrswMyTaskData As Xydc.Platform.Common.Data.grswMyTaskData, _
            ByRef objNodeData As System.Data.DataRow) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacMyTask
                    getMyTaskNodeData = .getMyTaskNodeData(strErrMsg, strCode, objgrswMyTaskData, objNodeData)
                End With
            Catch ex As Exception
                getMyTaskNodeData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据当前选定的任务、搜索条件获取当前用户的要查看的文件数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strUserXM            ：用户名称
        '     objNodeData          ：当前任务节点数据行
        '     strWhere             ：当前搜索条件(a.)
        '     objFileData          ：返回要查看的文件数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getMyTaskFileData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByVal objNodeData As System.Data.DataRow, _
            ByVal strWhere As String, _
            ByRef objFileData As Xydc.Platform.Common.Data.grswMyTaskData) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacMyTask
                    getMyTaskFileData = .getMyTaskFileData(strErrMsg, strUserId, strPassword, strUserXM, objNodeData, strWhere, objFileData)
                End With
            Catch ex As Exception
                getMyTaskFileData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据当前选定的任务、搜索条件获取当前用户的要查看的任务数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWJBS              ：要查看的文件标识
        '     strUserXM            ：用户名称
        '     objNodeData          ：当前任务节点数据行
        '     strWhere             ：当前搜索条件(a.)
        '     objTaskData          ：返回要查看的任务数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getMyTaskTaskData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWJBS As String, _
            ByVal strUserXM As String, _
            ByVal objNodeData As System.Data.DataRow, _
            ByVal strWhere As String, _
            ByRef objTaskData As Xydc.Platform.Common.Data.grswMyTaskData) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacMyTask
                    getMyTaskTaskData = .getMyTaskTaskData(strErrMsg, strUserId, strPassword, strWJBS, strUserXM, objNodeData, strWhere, objTaskData)
                End With
            Catch ex As Exception
                getMyTaskTaskData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取我的未办事宜数据集
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserId              ：用户标识
        '     strPassword            ：用户密码
        '     strUserXM              ：用户名称
        '     objDataSetDBSY         ：未办事宜数据集
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function getDataSetDBSY( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByRef objDataSetDBSY As Xydc.Platform.Common.Data.grswMyTaskData) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacMyTask
                    getDataSetDBSY = .getDataSetDBSY(strErrMsg, strUserId, strPassword, strUserXM, objDataSetDBSY)
                End With
            Catch ex As Exception
                getDataSetDBSY = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取我的已经过期文件+今天要过期数据集
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserId              ：用户标识
        '     strPassword            ：用户密码
        '     strUserXM              ：用户名称
        '     objDataSetGQSY         ：已经过期文件+今天要过期数据集
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function getDataSetGQSY( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByRef objDataSetGQSY As Xydc.Platform.Common.Data.grswMyTaskData) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacMyTask
                    getDataSetGQSY = .getDataSetGQSY(strErrMsg, strUserId, strPassword, strUserXM, objDataSetGQSY)
                End With
            Catch ex As Exception
                getDataSetGQSY = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取我的备忘提醒数据集
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserId              ：用户标识
        '     strPassword            ：用户密码
        '     strUserXM              ：用户名称
        '     objDataSetBWTX         ：备忘提醒数据集
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function getDataSetBWTX( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByRef objDataSetBWTX As Xydc.Platform.Common.Data.grswMyTaskData) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacMyTask
                    getDataSetBWTX = .getDataSetBWTX(strErrMsg, strUserId, strPassword, strUserXM, objDataSetBWTX)
                End With
            Catch ex As Exception
                getDataSetBWTX = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取我的未办事宜数目
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserId              ：用户标识
        '     strPassword            ：用户密码
        '     strUserXM              ：用户名称
        '     intCountDBSY           ：未办事宜数目
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function getCountDBSY( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByRef intCountDBSY As Integer) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacMyTask
                    getCountDBSY = .getCountDBSY(strErrMsg, strUserId, strPassword, strUserXM, intCountDBSY)
                End With
            Catch ex As Exception
                getCountDBSY = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取我的已经过期文件+今天要过期文件数目
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserId              ：用户标识
        '     strPassword            ：用户密码
        '     strUserXM              ：用户名称
        '     intCountGQSY           ：已经过期文件+今天要过期文件数目
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function getCountGQSY( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByRef intCountGQSY As Integer) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacMyTask
                    getCountGQSY = .getCountGQSY(strErrMsg, strUserId, strPassword, strUserXM, intCountGQSY)
                End With
            Catch ex As Exception
                getCountGQSY = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取我的备忘提醒文件数目
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserId              ：用户标识
        '     strPassword            ：用户密码
        '     strUserXM              ：用户名称
        '     intCountBWTX           ：备忘提醒文件数目
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function getCountBWTX( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByRef intCountBWTX As Integer) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacMyTask
                    getCountBWTX = .getCountBWTX(strErrMsg, strUserId, strPassword, strUserXM, intCountBWTX)
                End With
            Catch ex As Exception
                getCountBWTX = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取指定时间后收到的文件数目
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserId              ：用户标识
        '     strPassword            ：用户密码
        '     strUserXM              ：用户名称
        '     strZDSJ                ：指定时间(日期+时间格式)
        '     intCountRecv           ：文件数目
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function getCountRecv( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByVal strZDSJ As String, _
            ByRef intCountRecv As Integer) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacMyTask
                    getCountRecv = .getCountRecv(strErrMsg, strUserId, strPassword, strUserXM, strZDSJ, intCountRecv)
                End With
            Catch ex As Exception
                getCountRecv = False
                strErrMsg = ex.Message
            End Try

        End Function

    End Class 'rulesMyTask

End Namespace 'Xydc.Platform.BusinessRules
