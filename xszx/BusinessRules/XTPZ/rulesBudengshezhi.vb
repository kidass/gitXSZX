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
    ' 类名    ：rulesBudengshezhi
    '
    ' 功能描述： 
    '   　提供对“补登配置”相关信息处理的业务规则
    '----------------------------------------------------------------
    Public Class rulesBudengshezhi

        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
        End Sub

        '----------------------------------------------------------------
        ' 安全释放本身资源
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessRules.rulesBudengshezhi)
            Try
                If Not (obj Is Nothing) Then
                    'obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub









        '----------------------------------------------------------------
        ' 获取“管理_B_补登设置”的SQL语句(以岗位代码升序排序)
        ' 返回
        '                          ：SQL
        '----------------------------------------------------------------
        Public Function getMainSQL() As String
            Try
                With New Xydc.Platform.DataAccess.dacBudengshezhi
                    getMainSQL = .getMainSQL()
                End With
            Catch ex As Exception
                getMainSQL = ""
            End Try
        End Function

        '----------------------------------------------------------------
        ' 获取“管理_B_补登设置”的数据集(以岗位代码升序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWhere             ：搜索字符串
        '     objBudengshezhiData   ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objBudengshezhiData As Xydc.Platform.Common.Data.BudengshezhiData) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacBudengshezhi
                    getDataSet = .getDataSet(strErrMsg, strUserId, strPassword, strWhere, objBudengshezhiData)
                End With
            Catch ex As Exception
                getDataSet = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 保存“管理_B_补登设置”的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
        '     objNewData           ：新数据
        '     objenumEditType      ：编辑类型
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doSaveData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.ListDictionary, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacBudengshezhi
                    '检查数据
                    If .doVerifyData(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType) = False Then
                        doSaveData = False
                        Exit Try
                    End If
                    '保存数据
                    doSaveData = .doSaveData(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType)
                End With
            Catch ex As Exception
                doSaveData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 删除“管理_B_补登设置”的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacBudengshezhi
                    doDeleteData = .doDeleteData(strErrMsg, strUserId, strPassword, objOldData)
                End With
            Catch ex As Exception
                doDeleteData = False
                strErrMsg = ex.Message
            End Try

        End Function

    End Class 'rulesBudengshezhi

End Namespace 'Xydc.Platform.BusinessRules
