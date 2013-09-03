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
    ' 类名    ：systemFenfafanwei
    '
    ' 功能描述： 
    '   　提供对“公文_B_分发范围”信息处理的表现层支持
    '----------------------------------------------------------------
    Public Class systemFenfafanwei
        Inherits MarshalByRefObject







        '----------------------------------------------------------------
        ' 安全释放本身资源
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.systemFenfafanwei)
            Try
                If Not (obj Is Nothing) Then
                    'obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub









        '----------------------------------------------------------------
        ' 获取“公文_B_分发范围”主记录的数据集(以范围名称升序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWhere             ：搜索条件(默认表前缀a.)
        '     objFenfafanweiData   ：分发范围信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getFenfafanweiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objFenfafanweiData As Xydc.Platform.Common.Data.FenfafanweiData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesFenfafanwei
                    getFenfafanweiData = .getFenfafanweiData(strErrMsg, strUserId, strPassword, strWhere, objFenfafanweiData)
                End With
            Catch ex As Exception
                getFenfafanweiData = False
                strErrMsg = ex.Message
            End Try

        End Function



        '----------------------------------------------------------------
        ' 获取指定成员的加入范围的数据集(以成员位置升序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWhere             ：搜索条件(默认表前缀a.)
        '     objFenfafanweiData   ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败

        '----------------------------------------------------------------
        Public Function getFenfafanweiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByVal strWhere As String, _
            ByRef objFenfafanweiData As Xydc.Platform.Common.Data.FenfafanweiData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesFenfafanwei
                    getFenfafanweiData = .getFenfafanweiData(strErrMsg, strUserId, strPassword, strUserXM, strWhere, objFenfafanweiData)
                End With
            Catch ex As Exception
                getFenfafanweiData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取指定成员的加入范围数据集(以范围名称升序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWhere             ：搜索条件(默认表前缀a.)
        '     objFenfafanweiData   ：信息数据集
        '     blnNone              ：重载用
        ' 返回
        '     True                 ：成功
        '     False                ：失败

        '----------------------------------------------------------------
        Public Function getFenfafanweiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objFenfafanweiData As Xydc.Platform.Common.Data.FenfafanweiData, _
            ByVal blnNone As Boolean) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesFenfafanwei
                    getFenfafanweiData = .getFenfafanweiData(strErrMsg, strUserId, strPassword, strWhere, objFenfafanweiData, blnNone)
                End With
            Catch ex As Exception
                getFenfafanweiData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 保存“公文_B_分发范围”的数据(范围主记录)
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
        Public Function doSaveFenfafanweiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            doSaveFenfafanweiData = False
            Try
                With New Xydc.Platform.BusinessRules.rulesFenfafanwei
                    doSaveFenfafanweiData = .doSaveFenfafanweiData(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 删除“公文_B_分发范围”的数据(范围主记录)，同时删除成员记录
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteFenfafanweiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesFenfafanwei
                    doDeleteFenfafanweiData = .doDeleteFenfafanweiData(strErrMsg, strUserId, strPassword, objOldData)
                End With
            Catch ex As Exception
                doDeleteFenfafanweiData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 保存“公文_B_分发范围”的数据(范围成员记录)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
        '     objNewData           ：新数据
        '     blnIsFWCY            ：仅作接口重载使用
        '     objenumEditType      ：编辑类型
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doSaveFenfafanweiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal blnIsFWCY As Boolean, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            doSaveFenfafanweiData = False
            Try
                With New Xydc.Platform.BusinessRules.rulesFenfafanwei
                    doSaveFenfafanweiData = .doSaveFenfafanweiData(strErrMsg, strUserId, strPassword, objOldData, objNewData, blnIsFWCY, objenumEditType)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 删除“公文_B_分发范围”的数据(范围成员记录)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
        '     blnIsFWCY            ：仅作接口重载使用
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteFenfafanweiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal blnIsFWCY As Boolean) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesFenfafanwei
                    doDeleteFenfafanweiData = .doDeleteFenfafanweiData(strErrMsg, strUserId, strPassword, objOldData, blnIsFWCY)
                End With
            Catch ex As Exception
                doDeleteFenfafanweiData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 保存“公文_B_分发范围”的数据(将成员加入几个常用范围中)
        '     strErrMsg                 ：如果错误，则返回错误信息
        '     strUserId                 ：用户标识
        '     strPassword               ：用户密码
        '     objDataSet_ChoiceCYFW     ：新范围数据
        '     objNewData                ：新成员数据
        '     objOldDataSet_ChoiceCYFW  ：旧范围数据
        ' 返回
        '     True                      ：成功
        '     False                     ：失败

        '----------------------------------------------------------------
        Public Function doSaveFenfafanweiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objDataSet_ChoiceCYFW As Xydc.Platform.Common.Data.FenfafanweiData, _
            ByVal objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objOldDataSet_ChoiceCYFW As Xydc.Platform.Common.Data.FenfafanweiData) As Boolean
            doSaveFenfafanweiData = False
            Try
                With New Xydc.Platform.BusinessRules.rulesFenfafanwei
                    doSaveFenfafanweiData = .doSaveFenfafanweiData(strErrMsg, strUserId, strPassword, objDataSet_ChoiceCYFW, objNewData, objOldDataSet_ChoiceCYFW)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取新的“公文_B_分发范围”的成员位置
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strFWMC              ：当前范围名称
        '     intCYWZ              ：新的成员位置(返回)
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getNewCYWZ( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strFWMC As String, _
            ByRef intCYWZ As Integer) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesFenfafanwei
                    getNewCYWZ = .getNewCYWZ(strErrMsg, strUserId, strPassword, strFWMC, intCYWZ)
                End With
            Catch ex As Exception
                getNewCYWZ = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 将指定范围内的指定成员位置上移
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objChengyuanData     ：成员数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doChengyuanMoveUp( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objChengyuanData As System.Data.DataRow) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesFenfafanwei
                    doChengyuanMoveUp = .doChengyuanMoveUp(strErrMsg, strUserId, strPassword, objChengyuanData)
                End With
            Catch ex As Exception
                doChengyuanMoveUp = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 将指定范围内的指定成员位置下移
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objChengyuanData     ：成员数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doChengyuanMoveDown( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objChengyuanData As System.Data.DataRow) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesFenfafanwei
                    doChengyuanMoveDown = .doChengyuanMoveDown(strErrMsg, strUserId, strPassword, objChengyuanData)
                End With
            Catch ex As Exception
                doChengyuanMoveDown = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 将指定范围内的指定成员objChengyuanData位置移动到objChengyuanDataTo
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objChengyuanData     ：准备移动的成员数据
        '     objChengyuanDataTo   ：移动到的成员数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doChengyuanMoveTo( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objChengyuanData As System.Data.DataRow, _
            ByVal objChengyuanDataTo As System.Data.DataRow) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesFenfafanwei
                    doChengyuanMoveTo = .doChengyuanMoveTo(strErrMsg, strUserId, strPassword, objChengyuanData, objChengyuanDataTo)
                End With
            Catch ex As Exception
                doChengyuanMoveTo = False
                strErrMsg = ex.Message
            End Try

        End Function

    End Class

End Namespace
