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
    ' 类名    ：systemXitongpeizhi
    '
    ' 功能描述： 
    '   　提供对“系统配置”相关信息处理的表现层支持
    '----------------------------------------------------------------
    Public Class systemXitongpeizhi
        Inherits MarshalByRefObject








        '----------------------------------------------------------------
        ' 安全释放本身资源
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.systemXitongpeizhi)
            Try
                If Not (obj Is Nothing) Then
                    'obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub









        '----------------------------------------------------------------
        ' 获取“管理_B_系统参数”的SQL语句(以标识升序排序)
        ' 返回
        '                          ：SQL
        '----------------------------------------------------------------
        Public Function getXitongcanshuSQL() As String
            Try
                With New Xydc.Platform.BusinessRules.rulesXitongpeizhi
                    getXitongcanshuSQL = .getXitongcanshuSQL()
                End With
            Catch ex As Exception
                getXitongcanshuSQL = ""
            End Try
        End Function

        '----------------------------------------------------------------
        ' 获取“管理_B_系统参数”的数据集(以标识升序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWhere             ：搜索字符串
        '     objXitongcanshuData  ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getXitongcanshuData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objXitongcanshuData As Xydc.Platform.Common.Data.XitongcanshuData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesXitongpeizhi
                    getXitongcanshuData = .getXitongcanshuData(strErrMsg, strUserId, strPassword, strWhere, objXitongcanshuData)
                End With
            Catch ex As Exception
                getXitongcanshuData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 保存“管理_B_系统参数”的数据
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
        Public Function doSaveXitongcanshuData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.ListDictionary, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesXitongpeizhi
                    doSaveXitongcanshuData = .doSaveXitongcanshuData(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType)
                End With
            Catch ex As Exception
                doSaveXitongcanshuData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 删除“管理_B_系统参数”的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteXitongcanshuData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesXitongpeizhi
                    doDeleteXitongcanshuData = .doDeleteXitongcanshuData(strErrMsg, strUserId, strPassword, objOldData)
                End With
            Catch ex As Exception
                doDeleteXitongcanshuData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取系统配置中的FTP服务器参数信息
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objFTPProperty       ：FTP服务器参数(返回)
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getFtpServerParam( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesXitongpeizhi
                    getFtpServerParam = .getFtpServerParam(strErrMsg, strUserId, strPassword, objFTPProperty)
                End With
            Catch ex As Exception
                getFtpServerParam = False
                strErrMsg = ex.Message
            End Try

        End Function

    End Class

End Namespace
