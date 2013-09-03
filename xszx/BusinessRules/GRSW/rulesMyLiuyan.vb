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
    ' 类名    ：rulesMyLiuyan
    '
    ' 功能描述： 
    '     提供对“我的离开留言”模块涉及的业务逻辑层操作
    '----------------------------------------------------------------
    Public Class rulesMyLiuyan

        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
        End Sub

        '----------------------------------------------------------------
        ' 安全释放本身资源
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessRules.rulesMyLiuyan)
            Try
                If Not (obj Is Nothing) Then
                    'obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub










        '----------------------------------------------------------------
        ' 获取给定人员有效的委托留言信息
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strUserXMArray       ：用户名称数组
        '     objLiuyanData        ：返回委托留言数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXMArray As String(), _
            ByRef objLiuyanData As Xydc.Platform.Common.Data.grswMyLiuyanData) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacMyLiuyan
                    getDataSet = .getDataSet(strErrMsg, strUserId, strPassword, strUserXMArray, objLiuyanData)
                End With
            Catch ex As Exception
                getDataSet = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取[留言人=strUserXM]的留言数据
        ' 获取“个人_B_离开留言”完全数据的数据集(以留言日期降序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strUserXM            ：当前操作员名称
        '     strWhere             ：搜索字符串
        '     objLKLYDataSet       ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByVal strWhere As String, _
            ByRef objLKLYDataSet As Xydc.Platform.Common.Data.grswMyLiuyanData) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacMyLiuyan
                    getDataSet = .getDataSet(strErrMsg, strUserId, strPassword, strUserXM, strWhere, objLKLYDataSet)
                End With
            Catch ex As Exception
                getDataSet = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取[委托代理人=strUserXM]的留言数据
        ' 获取“个人_B_离开留言”完全数据的数据集(以留言日期降序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strUserXM            ：当前操作员名称
        '     strWhere             ：搜索字符串
        '     blnUnused            ：接口重载用
        '     objLKLYDataSet       ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByVal strWhere As String, _
            ByVal blnUnused As Boolean, _
            ByRef objLKLYDataSet As Xydc.Platform.Common.Data.grswMyLiuyanData) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacMyLiuyan
                    getDataSet = .getDataSet(strErrMsg, strUserId, strPassword, strUserXM, strWhere, blnUnused, objLKLYDataSet)
                End With
            Catch ex As Exception
                getDataSet = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 保存“个人_B_离开留言”的数据
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
            ByVal objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacMyLiuyan
                    doSaveData = .doVerifyData(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType)
                    If doSaveData = True Then
                        doSaveData = .doSaveData(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType)
                    End If
                End With
            Catch ex As Exception
                doSaveData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 删除“个人_B_离开留言”的数据
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
                With New Xydc.Platform.DataAccess.dacMyLiuyan
                    doDeleteData = .doDeleteData(strErrMsg, strUserId, strPassword, objOldData)
                End With
            Catch ex As Exception
                doDeleteData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 拒绝接受指定委托
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：委托数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doReject( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacMyLiuyan
                    doReject = .doReject(strErrMsg, strUserId, strPassword, objOldData)
                End With
            Catch ex As Exception
                doReject = False
                strErrMsg = ex.Message
            End Try

        End Function

    End Class 'rulesMyLiuyan

End Namespace 'Xydc.Platform.BusinessRules
