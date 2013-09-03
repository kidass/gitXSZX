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
Imports System.Security.Cryptography
Imports Microsoft.VisualBasic

Imports Xydc.Platform.SystemFramework
Imports Xydc.Platform.Common.Data
Imports Xydc.Platform.BusinessRules

Namespace Xydc.Platform.BusinessFacade
    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：systemXingzhengjibie
    '
    ' 功能描述： 
    '   　提供对“公共_B_行政级别”信息处理的表现层支持
    '----------------------------------------------------------------
    Public Class systemXingzhengjibie
        Inherits MarshalByRefObject

        '----------------------------------------------------------------
        ' 安全释放本身资源
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.systemXingzhengjibie)
            Try
                If Not (obj Is Nothing) Then
                    'obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub











        '----------------------------------------------------------------
        ' 获取“公共_B_行政级别”的SQL语句(以级别代码升序排序)
        ' 返回
        '                          ：SQL
        '----------------------------------------------------------------
        Public Function getXingzhengjibieSQL() As String
            Try
                With New Xydc.Platform.BusinessRules.rulesXingzhengjibie
                    getXingzhengjibieSQL = .getXingzhengjibieSQL()
                End With
            Catch ex As Exception
                getXingzhengjibieSQL = ""
            End Try
        End Function

        '----------------------------------------------------------------
        ' 根据级别代码获取“公共_B_行政级别”的数据集
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserId              ：用户标识
        '     strPassword            ：用户密码
        '     strJBDM                ：级别代码
        '     blnUnused              ：重载用
        '     objXingzhengjibieData  ：信息数据集
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function getXingzhengjibieData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strJBDM As String, _
            ByVal blnUnused As Boolean, _
            ByRef objXingzhengjibieData As Xydc.Platform.Common.Data.XingzhengjibieData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesXingzhengjibie
                    getXingzhengjibieData = .getXingzhengjibieData(strErrMsg, strUserId, strPassword, strJBDM, blnUnused, objXingzhengjibieData)
                End With
            Catch ex As Exception
                getXingzhengjibieData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据级别名称获取“公共_B_行政级别”的数据集
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserId              ：用户标识
        '     strPassword            ：用户密码
        '     blnUnused              ：重载用
        '     strJBMC                ：级别名称
        '     objXingzhengjibieData  ：信息数据集
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function getXingzhengjibieData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal blnUnused As Boolean, _
            ByVal strJBMC As String, _
            ByRef objXingzhengjibieData As Xydc.Platform.Common.Data.XingzhengjibieData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesXingzhengjibie
                    getXingzhengjibieData = .getXingzhengjibieData(strErrMsg, strUserId, strPassword, blnUnused, strJBMC, objXingzhengjibieData)
                End With
            Catch ex As Exception
                getXingzhengjibieData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取“公共_B_行政级别”的数据集(以代码升序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWhere             ：搜索字符串
        '     objXingzhengjibieData：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getXingzhengjibieData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objXingzhengjibieData As Xydc.Platform.Common.Data.XingzhengjibieData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesXingzhengjibie
                    getXingzhengjibieData = .getXingzhengjibieData(strErrMsg, strUserId, strPassword, strWhere, objXingzhengjibieData)
                End With
            Catch ex As Exception
                getXingzhengjibieData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 保存“公共_B_行政级别”的数据
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
        Public Function doSaveXingzhengjibieData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesXingzhengjibie
                    doSaveXingzhengjibieData = .doSaveXingzhengjibieData(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType)
                End With
            Catch ex As Exception
                doSaveXingzhengjibieData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 删除“公共_B_行政级别”的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteXingzhengjibieData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesXingzhengjibie
                    doDeleteXingzhengjibieData = .doDeleteXingzhengjibieData(strErrMsg, strUserId, strPassword, objOldData)
                End With
            Catch ex As Exception
                doDeleteXingzhengjibieData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据级别名称获取级别代码
        '     strErrMsg     ：如果错误，则返回错误信息
        '     strUserId     ：用户标识
        '     strPassword   ：用户密码
        '     strJBMC       ：级别名称
        '     strJBDM       ：级别代码(返回)
        ' 返回
        '     True          ：成功
        '     False         ：失败
        '----------------------------------------------------------------
        Public Function getJbdmByJbmc( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strJBMC As String, _
            ByRef strJBDM As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesXingzhengjibie
                    getJbdmByJbmc = .getJbdmByJbmc(strErrMsg, strUserId, strPassword, strJBMC, strJBDM)
                End With
            Catch ex As Exception
                getJbdmByJbmc = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据级别代码获取级别名称
        '     strErrMsg     ：如果错误，则返回错误信息
        '     strUserId     ：用户标识
        '     strPassword   ：用户密码
        '     strRYDM       ：级别代码
        '     strRYMC       ：级别名称(返回)
        ' 返回
        '     True          ：成功
        '     False         ：失败
        '----------------------------------------------------------------
        Public Function getJbmcByJbdm( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strJBDM As String, _
            ByRef strJBMC As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesXingzhengjibie
                    getJbmcByJbdm = .getJbmcByJbdm(strErrMsg, strUserId, strPassword, strJBDM, strJBMC)
                End With
            Catch ex As Exception
                getJbmcByJbdm = False
                strErrMsg = ex.Message
            End Try

        End Function

    End Class

End Namespace
