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
    ' 类名    ：systemJingchanglianxiren
    '
    ' 功能描述： 
    '   　提供对“公文_B_经常联系人”信息处理的表现层支持
    '----------------------------------------------------------------
    Public Class systemJingchanglianxiren
        Inherits MarshalByRefObject







        '----------------------------------------------------------------
        ' 安全释放本身资源
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.systemJingchanglianxiren)
            Try
                If Not (obj Is Nothing) Then
                    'obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub








        '----------------------------------------------------------------
        ' 根据指定人员获取他的经常联系人信息的数据集(人员信息完全连接)
        ' 以组织代码、人员序号升序排序
        '     strErrMsg                ：如果错误，则返回错误信息
        '     strUserId                ：用户标识
        '     strPassword              ：用户密码
        '     strRYDM                  ：指定人员代码
        '     strWhere                 ：搜索串(默认表前缀a.)
        '     objJingchanglianxirenData：岗位代码信息数据集
        ' 返回
        '     True                     ：成功
        '     False                    ：失败
        '----------------------------------------------------------------
        Public Function getJclxrData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRYDM As String, _
            ByVal strWhere As String, _
            ByRef objJingchanglianxirenData As Xydc.Platform.Common.Data.JingchanglianxirenData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesJingchanglianxiren
                    getJclxrData = .getJclxrData(strErrMsg, strUserId, strPassword, strRYDM, strWhere, objJingchanglianxirenData)
                End With
            Catch ex As Exception
                getJclxrData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取给定人员的经常联系人数据
        '     strErrMsg                ：如果错误，则返回错误信息
        '     strUserId                ：用户标识
        '     strPassword              ：用户密码
        '     strCzyId                 ：操作员标识
        '     objJinchanglianxirenData ：返回数据
        ' 返回
        '     True                     ：成功
        '     False                    ：失败
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strCzyId As String, _
            ByRef objJinchanglianxirenData As Xydc.Platform.Common.Data.JingchanglianxirenData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesJingchanglianxiren
                    getDataSet = .getDataSet(strErrMsg, strUserId, strPassword, strCzyId, objJinchanglianxirenData)
                End With
            Catch ex As Exception
                getDataSet = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 检查“公文_B_经常联系人”的数据的合法性
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
        '     objNewData           ：(返回)新数据
        '     objenumEditType      ：编辑类型

        ' 返回
        '     True                 ：合法
        '     False                ：不合法或其他程序错误
        '----------------------------------------------------------------
        Public Function doVerifyData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByRef objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesJingchanglianxiren
                    doVerifyData = .doVerifyData(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType)
                End With
            Catch ex As Exception
                doVerifyData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 保存“公文_B_经常联系人”的数据
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
                With New Xydc.Platform.BusinessRules.rulesJingchanglianxiren
                    doSaveData = .doSaveData(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType)
                End With
            Catch ex As Exception
                doSaveData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 删除“公文_B_经常联系人”的数据
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
                With New Xydc.Platform.BusinessRules.rulesJingchanglianxiren
                    doDeleteData = .doDeleteData(strErrMsg, strUserId, strPassword, objOldData)
                End With
            Catch ex As Exception
                doDeleteData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 增加“公文_B_经常联系人”的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strRYDM              ：人员代码
        '     strLXRDM             ：联系人代码
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doAddJclxr( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRYDM As String, _
            ByVal strLXRDM As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesJingchanglianxiren
                    doAddJclxr = .doAddJclxr(strErrMsg, strUserId, strPassword, strRYDM, strLXRDM)
                End With
            Catch ex As Exception
                doAddJclxr = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 删除“公文_B_经常联系人”的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strRYDM              ：人员代码
        '     strLXRDM             ：联系人代码
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteJclxr( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRYDM As String, _
            ByVal strLXRDM As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesJingchanglianxiren
                    doDeleteJclxr = .doDeleteJclxr(strErrMsg, strUserId, strPassword, strRYDM, strLXRDM)
                End With
            Catch ex As Exception
                doDeleteJclxr = False
                strErrMsg = ex.Message
            End Try

        End Function


    End Class

End Namespace
