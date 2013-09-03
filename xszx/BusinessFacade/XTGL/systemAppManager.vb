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
    ' 类名    ：systemAppManager
    '
    ' 功能描述： 
    '     提供对应用系统管理功能的表现层支持
    '----------------------------------------------------------------
    Public Class systemAppManager
        Inherits MarshalByRefObject








        '----------------------------------------------------------------
        ' 安全释放本身资源
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.systemAppManager)
            Try
                If Not (obj Is Nothing) Then
                    'obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub








        '----------------------------------------------------------------
        ' 输出数据到Excel
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objDataSet           ：要导出的数据集
        '     strExcelFile         ：导出到WEB服务器中的Excel文件路径
        '     strMacroName         ：宏名列表
        '     strMacroValue        ：宏值列表
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doExportToExcel( _
            ByRef strErrMsg As String, _
            ByVal objDataSet As System.Data.DataSet, _
            ByVal strExcelFile As String, _
            Optional ByVal strMacroName As String = "", _
            Optional ByVal strMacroValue As String = "") As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doExportToExcel = .doExportToExcel(strErrMsg, objDataSet, strExcelFile, strMacroName, strMacroValue)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doExportToExcel = False
            End Try

        End Function









        '----------------------------------------------------------------
        ' 获取人员申请ID情况的数据集(以组织代码、人员序号升序排序)
        ' 含人员的全部连接数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWhere             ：搜索字符串(默认表前缀a.)
        '     objRenyuanData       ：指定组织机构下的人员信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getRenyuanApplyIdData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objRenyuanData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getRenyuanApplyIdData = .getRenyuanApplyIdData(strErrMsg, strUserId, strPassword, strWhere, objRenyuanData)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getRenyuanApplyIdData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 申请Login
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strLoginId           ：要申请的loginId
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doApplyId( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strLoginId As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doApplyId = .doApplyId(strErrMsg, strUserId, strPassword, strLoginId)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doApplyId = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 注销Login
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strLoginId           ：要注销的loginId
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDropId( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strLoginId As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doDropId = .doDropId(strErrMsg, strUserId, strPassword, strLoginId)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doDropId = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 检查Login
        '     strErrMsg            ：如果错误，则返回错误信息
        '     blnISNull            ：TRUE-已申请，FALSE-未申请
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strLoginId           ：要检查的loginId
        ' 返回
        '     True                 ：已申请
        '     False                ：未申请

        '----------------------------------------------------------------
        Public Function doCheckId( _
            ByRef strErrMsg As String, _
            ByRef blnISNull As Boolean, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strLoginId As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doCheckId = .doCheckId(strErrMsg, blnISNull, strUserId, strPassword, strLoginId)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doCheckId = False
            End Try

        End Function


        '----------------------------------------------------------------
        ' 获取“管理_B_数据库_服务器”的数据集(以名称升序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWhere             ：搜索字符串(默认表前缀a.)
        '     objFuwuqiData        ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getFuwuqiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objFuwuqiData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getFuwuqiData = .getFuwuqiData(strErrMsg, strUserId, strPassword, strWhere, objFuwuqiData)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getFuwuqiData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据服务器名获取“管理_B_数据库_服务器”的数据集(以名称升序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strServerName        ：服务器名
        '     strWhere             ：搜索字符串(默认表前缀a.)
        '     objFuwuqiData        ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getFuwuqiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strServerName As String, _
            ByVal strWhere As String, _
            ByRef objFuwuqiData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getFuwuqiData = .getFuwuqiData(strErrMsg, strUserId, strPassword, strServerName, strWhere, objFuwuqiData)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getFuwuqiData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据加密连接串获取连接参数
        '     strErrMsg             ：如果错误，则返回错误信息
        '     objConnectionProperty ：用户标识
        '     value                 ：连接字符串的加密数据
        ' 返回
        '     True                  ：成功
        '     False                 ：失败
        '----------------------------------------------------------------
        Public Function getServerConnectionProperty( _
            ByRef strErrMsg As String, _
            ByRef objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal value As Object) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getServerConnectionProperty = .getServerConnectionProperty(strErrMsg, objConnectionProperty, value)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getServerConnectionProperty = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据服务器名获取连接参数
        '     strErrMsg             ：如果错误，则返回错误信息
        '     strUserId             ：用户标识
        '     strPassword           ：用户密码
        '     strServerName         ：服务器名
        '     objConnectionProperty ：返回连接参数
        ' 返回
        '     True                  ：成功
        '     False                 ：失败
        '----------------------------------------------------------------
        Public Function getServerConnectionProperty( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strServerName As String, _
            ByRef objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getServerConnectionProperty = .getServerConnectionProperty(strErrMsg, strUserId, strPassword, strServerName, objConnectionProperty)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getServerConnectionProperty = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取“管理_B_数据库_数据库”的数据集(以服务器名、数据库名升序排序)
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     objConnectionProperty ：服务器信息
        '     strWhere                    ：搜索字符串(默认表前缀a.)
        '     objShujukuData              ：信息数据集
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getShujukuData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strWhere As String, _
            ByRef objShujukuData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getShujukuData = .getShujukuData(strErrMsg, objConnectionProperty, strWhere, objShujukuData)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getShujukuData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取“管理_B_数据库_对象”的数据集(以数据库名升序排序)
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     objConnectionProperty ：服务器信息
        '     strWhere                    ：搜索字符串(默认表前缀a.)
        '     objDuixiangData             ：信息数据集
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getDuixiangData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strWhere As String, _
            ByRef objDuixiangData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getDuixiangData = .getDuixiangData(strErrMsg, objConnectionProperty, strWhere, objDuixiangData)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getDuixiangData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 保存“管理_B_数据库_服务器”的数据
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
        Public Function doSaveFuwuqiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.ListDictionary, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doSaveFuwuqiData = .doSaveFuwuqiData(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doSaveFuwuqiData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 删除“管理_B_数据库_服务器”的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strServerName        ：服务器名
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteFuwuqiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strServerName As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doDeleteFuwuqiData = .doDeleteFuwuqiData(strErrMsg, strUserId, strPassword, strServerName)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doDeleteFuwuqiData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据服务器名、数据库名获取“管理_B_数据库_数据库”的数据集
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strServerName        ：服务器名
        '     strDBName            ：数据库名
        '     strWhere             ：搜索字符串(默认表前缀a.)
        '     objShujukuData       ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getShujukuData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strServerName As String, _
            ByVal strDBName As String, _
            ByVal strWhere As String, _
            ByRef objShujukuData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getShujukuData = .getShujukuData(strErrMsg, strUserId, strPassword, strServerName, strDBName, strWhere, objShujukuData)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getShujukuData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据服务器名、数据库名、对象名称、对象类型
        ' 获取“管理_B_数据库_对象”的数据集
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strServerName        ：服务器名
        '     strDBName            ：数据库名
        '     strDXLX              ：数据库对象类型
        '     strDXMC              ：数据库对象名
        '     strWhere             ：搜索字符串(默认表前缀a.)
        '     objDuixiangData      ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDuixiangData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strServerName As String, _
            ByVal strDBName As String, _
            ByVal strDXLX As String, _
            ByVal strDXMC As String, _
            ByVal strWhere As String, _
            ByRef objDuixiangData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getDuixiangData = .getDuixiangData(strErrMsg, strUserId, strPassword, strServerName, strDBName, strDXLX, strDXMC, strWhere, objDuixiangData)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getDuixiangData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据对象标识获取“管理_B_数据库_对象”的数据集
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     intDXBS              ：对象标识
        '     strWhere             ：搜索字符串(默认表前缀a.)
        '     objDuixiangData      ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDuixiangData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intDXBS As Integer, _
            ByVal strWhere As String, _
            ByRef objDuixiangData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getDuixiangData = .getDuixiangData(strErrMsg, strUserId, strPassword, intDXBS, strWhere, objDuixiangData)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getDuixiangData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 保存“管理_B_数据库_数据库”的数据
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
        Public Function doSaveShujukuData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.ListDictionary, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doSaveShujukuData = .doSaveShujukuData(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doSaveShujukuData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 保存“管理_B_数据库_对象”的数据
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
        Public Function doSaveDuixiangData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.ListDictionary, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doSaveDuixiangData = .doSaveDuixiangData(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doSaveDuixiangData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 删除“管理_B_数据库_数据库”的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strServerName        ：服务器名
        '     strDBName            ：数据库名
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteShujukuData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strServerName As String, _
            ByVal strDBName As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doDeleteShujukuData = .doDeleteShujukuData(strErrMsg, strUserId, strPassword, strServerName, strDBName)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doDeleteShujukuData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 删除“管理_B_数据库_对象”的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strServerName        ：服务器名
        '     strDBName            ：数据库名
        '     strDXLX              ：对象类型
        '     strDXMC              ：对象名称
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteDuixiangData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strServerName As String, _
            ByVal strDBName As String, _
            ByVal strDXLX As String, _
            ByVal strDXMC As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doDeleteDuixiangData = .doDeleteDuixiangData(strErrMsg, strUserId, strPassword, strServerName, strDBName, strDXLX, strDXMC)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doDeleteDuixiangData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 删除“管理_B_数据库_对象”的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     intDXBS              ：对象标识
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteDuixiangData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intDXBS As Integer) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doDeleteDuixiangData = .doDeleteDuixiangData(strErrMsg, strUserId, strPassword, intDXBS)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doDeleteDuixiangData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 自动清除管理_B_数据库_数据库、管理_B_数据库_对象中的无效数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doAutoCleanManageData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doAutoCleanManageData = .doAutoCleanManageData(strErrMsg, strUserId, strPassword)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doAutoCleanManageData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取指定objConnectionProperty中的数据库角色
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     objConnectionProperty ：服务器信息
        '     strWhere                    ：搜索字符串(默认表前缀a.)
        '     objRoleData                 ：信息数据集
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getRoleData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strWhere As String, _
            ByRef objRoleData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getRoleData = .getRoleData(strErrMsg, objConnectionProperty, strWhere, objRoleData)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getRoleData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取已经加入到角色strRoleName的人员列表(含人员的全部连接数据)
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     objConnectionProperty       ：服务器信息
        '     strRoleName                 ：角色名
        '     strWhere                    ：搜索字符串(默认表前缀a.)
        '     objRenyuanData              ：指定组织机构下的人员信息数据集
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getRenyuanInRoleData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strRoleName As String, _
            ByVal strWhere As String, _
            ByRef objRenyuanData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getRenyuanInRoleData = .getRenyuanInRoleData(strErrMsg, objConnectionProperty, strRoleName, strWhere, objRenyuanData)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getRenyuanInRoleData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取没有加入到角色strRoleName的人员列表(含人员的全部连接数据)
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     objConnectionProperty       ：服务器信息
        '     strRoleName                 ：角色名
        '     strWhere                    ：搜索字符串(默认表前缀a.)
        '     objRenyuanData              ：指定组织机构下的人员信息数据集
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getRenyuanNotInRoleData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strRoleName As String, _
            ByVal strWhere As String, _
            ByRef objRenyuanData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getRenyuanNotInRoleData = .getRenyuanNotInRoleData(strErrMsg, objConnectionProperty, strRoleName, strWhere, objRenyuanData)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getRenyuanNotInRoleData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 在指定服务器objConnectionProperty中创建角色strRoleName
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     objConnectionProperty       ：服务器信息
        '     strRoleName                 ：角色名
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function doAddRole( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strRoleName As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doAddRole = .doAddRole(strErrMsg, objConnectionProperty, strRoleName)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doAddRole = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 在指定服务器objConnectionProperty中删除角色strRoleName
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     objConnectionProperty       ：服务器信息
        '     strRoleName                 ：角色名
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function doDropRole( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strRoleName As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doDropRole = .doDropRole(strErrMsg, objConnectionProperty, strRoleName)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doDropRole = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 在指定服务器objConnectionProperty指定角色strRoleName中加入成员
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     objConnectionProperty       ：服务器信息
        '     strRoleName                 ：角色名
        '     strMemberName               ：成员名
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function doAddRoleMember( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strRoleName As String, _
            ByVal strMemberName As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doAddRoleMember = .doAddRoleMember(strErrMsg, objConnectionProperty, strRoleName, strMemberName)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doAddRoleMember = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 在指定服务器objConnectionProperty指定角色strRoleName中删除成员
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     objConnectionProperty       ：服务器信息
        '     strRoleName                 ：角色名
        '     strMemberName               ：成员名
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function doDropRoleMember( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strRoleName As String, _
            ByVal strMemberName As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doDropRoleMember = .doDropRoleMember(strErrMsg, objConnectionProperty, strRoleName, strMemberName)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doDropRoleMember = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取角色的权限设置数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objConnectionProperty：连接参数
        '     strRoleName          ：角色名
        '     strWhere             ：搜索字符串(默认表前缀a.)
        '     objRoleQXData        ：角色权限数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getRolePermissionsData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strRoleName As String, _
            ByVal strWhere As String, _
            ByRef objRoleQXData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getRolePermissionsData = .getRolePermissionsData(strErrMsg, objConnectionProperty, strRoleName, strWhere, objRoleQXData)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getRolePermissionsData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 给角色strRoleName授予指定对象strObjectName的权限objOptions
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objConnectionProperty：连接参数
        '     strRoleName          ：角色名
        '     strObjectName        ：对象名
        '     strObjectType        ：对象类型
        '     objOptions           ：角色权限数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doGrantRole( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strRoleName As String, _
            ByVal strObjectName As String, _
            ByVal strObjectType As String, _
            ByVal objOptions As System.Collections.Specialized.ListDictionary) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doGrantRole = .doGrantRole(strErrMsg, objConnectionProperty, strRoleName, strObjectName, strObjectType, objOptions)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doGrantRole = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 从角色strRoleName回收指定对象strObjectName的权限objOptions
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objConnectionProperty：连接参数
        '     strRoleName          ：角色名
        '     strObjectName        ：对象名
        '     strObjectType        ：对象类型
        '     objOptions           ：角色权限数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doRevokeRole( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strRoleName As String, _
            ByVal strObjectName As String, _
            ByVal strObjectType As String, _
            ByVal objOptions As System.Collections.Specialized.ListDictionary) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doRevokeRole = .doRevokeRole(strErrMsg, objConnectionProperty, strRoleName, strObjectName, strObjectType, objOptions)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doRevokeRole = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取对指定数据库有存取权限的人员情况的数据集
        ' 以组织代码、人员序号升序排序
        ' 含人员的全部连接数据
        '     strErrMsg             ：如果错误，则返回错误信息
        '     objConnectionProperty ：连接参数
        '     strWhere              ：搜索字符串(默认表前缀a.)
        '     objRenyuanGrantedData ：指定组织机构下的人员信息数据集
        ' 返回
        '     True                  ：成功
        '     False                 ：失败
        '----------------------------------------------------------------
        Public Function getRenyuanGrantedData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strWhere As String, _
            ByRef objRenyuanGrantedData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getRenyuanGrantedData = .getRenyuanGrantedData(strErrMsg, objConnectionProperty, strWhere, objRenyuanGrantedData)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getRenyuanGrantedData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取对指定数据库没有存取权限的人员情况的数据集
        ' 以组织代码、人员序号升序排序
        ' 含人员的全部连接数据
        '     strErrMsg               ：如果错误，则返回错误信息
        '     objConnectionProperty   ：连接参数
        '     strWhere                ：搜索字符串(默认表前缀a.)
        '     objRenyuanUngrantedData ：指定组织机构下的人员信息数据集
        ' 返回
        '     True                    ：成功
        '     False                   ：失败
        '----------------------------------------------------------------
        Public Function getRenyuanUngrantedData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strWhere As String, _
            ByRef objRenyuanUngrantedData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getRenyuanUngrantedData = .getRenyuanUngrantedData(strErrMsg, objConnectionProperty, strWhere, objRenyuanUngrantedData)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getRenyuanUngrantedData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 给strLoginName授予存取数据库
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objConnectionProperty：连接参数
        '     strLoginName         ：角色名
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doGrantDatabase( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strLoginName As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doGrantDatabase = .doGrantDatabase(strErrMsg, objConnectionProperty, strLoginName)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doGrantDatabase = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 给strLoginName取消存取数据库
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objConnectionProperty：连接参数
        '     strLoginName         ：角色名
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doRevokeDatabase( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strLoginName As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doRevokeDatabase = .doRevokeDatabase(strErrMsg, objConnectionProperty, strLoginName)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doRevokeDatabase = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取指定objConnectionProperty中的数据库的用户
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     objConnectionProperty       ：服务器信息
        '     strWhere                    ：搜索字符串(默认表前缀a.)
        '     objDBUserData               ：信息数据集
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getDBUserData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strWhere As String, _
            ByRef objDBUserData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getDBUserData = .getDBUserData(strErrMsg, objConnectionProperty, strWhere, objDBUserData)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getDBUserData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取角色的权限设置数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objConnectionProperty：连接参数
        '     strDBUserName        ：用户名
        '     strWhere             ：搜索字符串(默认表前缀a.)
        '     objDBUserQXData      ：角色权限数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDBUserPermissionsData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strDBUserName As String, _
            ByVal strWhere As String, _
            ByRef objDBUserQXData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getDBUserPermissionsData = .getDBUserPermissionsData(strErrMsg, objConnectionProperty, strDBUserName, strWhere, objDBUserQXData)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getDBUserPermissionsData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 给用户strDBUserName授予指定对象strObjectName的权限objOptions
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objConnectionProperty：连接参数
        '     strDBUserName        ：用户名
        '     strObjectName        ：对象名
        '     strObjectType        ：对象类型
        '     objOptions           ：角色权限数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doGrantDBUser( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strDBUserName As String, _
            ByVal strObjectName As String, _
            ByVal strObjectType As String, _
            ByVal objOptions As System.Collections.Specialized.ListDictionary) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doGrantDBUser = .doGrantDBUser(strErrMsg, objConnectionProperty, strDBUserName, strObjectName, strObjectType, objOptions)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doGrantDBUser = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 从用户strDBUserName回收指定对象strObjectName的权限objOptions
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objConnectionProperty：连接参数
        '     strDBUserName        ：用户名
        '     strObjectName        ：对象名
        '     strObjectType        ：对象类型
        '     objOptions           ：角色权限数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doRevokeDBUser( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strDBUserName As String, _
            ByVal strObjectName As String, _
            ByVal strObjectType As String, _
            ByVal objOptions As System.Collections.Specialized.ListDictionary) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doRevokeDBUser = .doRevokeDBUser(strErrMsg, objConnectionProperty, strDBUserName, strObjectName, strObjectType, objOptions)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doRevokeDBUser = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取“管理_B_应用系统_模块”的数据集(以模块代码升序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWhere             ：搜索字符串(默认表前缀a.)
        '     objMokuaiData        ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getMokuaiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objMokuaiData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getMokuaiData = .getMokuaiData(strErrMsg, strUserId, strPassword, strWhere, objMokuaiData)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getMokuaiData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取指定strMKDM下级的“管理_B_应用系统_模块”的数据集(以模块代码升序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strMKDM              ：模块代码
        '     strWhere             ：搜索字符串(默认表前缀a.)
        '     objMokuaiData        ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getMokuaiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strMKDM As String, _
            ByVal strWhere As String, _
            ByRef objMokuaiData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getMokuaiData = .getMokuaiData(strErrMsg, strUserId, strPassword, strMKDM, strWhere, objMokuaiData)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getMokuaiData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据指定strMKDM获取“管理_B_应用系统_模块”的数据集
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strMKDM              ：模块代码
        '     blnUnused            ：重载用
        '     objMokuaiData        ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getMokuaiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strMKDM As String, _
            ByVal blnUnused As Boolean, _
            ByRef objMokuaiData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getMokuaiData = .getMokuaiData(strErrMsg, strUserId, strPassword, strMKDM, blnUnused, objMokuaiData)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getMokuaiData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据指定strMKDM获取“管理_B_应用系统_模块”的数据集
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     intMKBS              ：模块标识
        '     blnUnused            ：重载用
        '     objMokuaiData        ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getMokuaiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intMKBS As Integer, _
            ByVal blnUnused As Boolean, _
            ByRef objMokuaiData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getMokuaiData = .getMokuaiData(strErrMsg, strUserId, strPassword, intMKBS, blnUnused, objMokuaiData)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getMokuaiData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据上级模块代码获取下级的模块代码
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strPrevMKDM          ：上级模块代码
        '     strNewMKDM           ：新模块代码(返回)
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getNewMKDM( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strPrevMKDM As String, _
            ByRef strNewMKDM As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getNewMKDM = .getNewMKDM(strErrMsg, strUserId, strPassword, strPrevMKDM, strNewMKDM)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getNewMKDM = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 保存“管理_B_应用系统_模块”的数据
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
        Public Function doSaveMokuaiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.ListDictionary, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doSaveMokuaiData = .doSaveMokuaiData(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doSaveMokuaiData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据模块代码删除“管理_B_应用系统_模块”的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strMKDM              ：模块代码
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteMokuaiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strMKDM As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doDeleteMokuaiData = .doDeleteMokuaiData(strErrMsg, strUserId, strPassword, strMKDM)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doDeleteMokuaiData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取角色的模块权限设置数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objConnectionProperty：连接参数
        '     strRoleName          ：角色名
        '     strWhere             ：搜索字符串(默认表前缀a.)
        '     objRoleMKQXData      ：角色权限数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getRoleMokuaiQXData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strRoleName As String, _
            ByVal strWhere As String, _
            ByRef objRoleMKQXData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getRoleMokuaiQXData = .getRoleMokuaiQXData(strErrMsg, objConnectionProperty, strRoleName, strWhere, objRoleMKQXData)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getRoleMokuaiQXData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取用户的模块权限设置数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objConnectionProperty：连接参数
        '     strDBUserName        ：用户名
        '     strWhere             ：搜索字符串(默认表前缀a.)
        '     objDBUserMKQXData    ：角色权限数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDBUserMokuaiQXData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strDBUserName As String, _
            ByVal strWhere As String, _
            ByRef objDBUserMKQXData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getDBUserMokuaiQXData = .getDBUserMokuaiQXData(strErrMsg, objConnectionProperty, strDBUserName, strWhere, objDBUserMKQXData)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getDBUserMokuaiQXData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 给角色strRoleName授予指定模块strMKBS的权限
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strRoleName          ：角色名
        '     strMKBS              ：模块标识
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doGrantRoleMokuaiQX( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRoleName As String, _
            ByVal strMKBS As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doGrantRoleMokuaiQX = .doGrantRoleMokuaiQX(strErrMsg, strUserId, strPassword, strRoleName, strMKBS)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doGrantRoleMokuaiQX = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 从角色strRoleName回收指定模块strMKBS的权限
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strRoleName          ：角色名
        '     strMKBS              ：模块标识
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doRevokeRoleMokuaiQX( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRoleName As String, _
            ByVal strMKBS As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doRevokeRoleMokuaiQX = .doRevokeRoleMokuaiQX(strErrMsg, strUserId, strPassword, strRoleName, strMKBS)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doRevokeRoleMokuaiQX = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 给用户strDBUserName授予指定模块strMKBS的权限
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strDBUserName        ：用户名
        '     strMKBS              ：模块标识
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doGrantDBuserMokuaiQX( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strDBUserName As String, _
            ByVal strMKBS As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doGrantDBuserMokuaiQX = .doGrantDBuserMokuaiQX(strErrMsg, strUserId, strPassword, strDBUserName, strMKBS)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doGrantDBuserMokuaiQX = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 从用户strDBUserName回收指定模块strMKBS的权限
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strDBUserName        ：用户名
        '     strMKBS              ：模块标识
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doRevokeDBUserMokuaiQX( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strDBUserName As String, _
            ByVal strMKBS As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doRevokeDBUserMokuaiQX = .doRevokeDBUserMokuaiQX(strErrMsg, strUserId, strPassword, strDBUserName, strMKBS)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doRevokeDBUserMokuaiQX = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取用户的模块权限设置数据(同时检查用户所属角色的权限设置)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strDBUserName        ：用户名
        '     objDBUserMKQXData    ：角色权限数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDBUserMokuaiQXData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strDBUserName As String, _
            ByRef objDBUserMKQXData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getDBUserMokuaiQXData = .getDBUserMokuaiQXData(strErrMsg, strUserId, strPassword, strDBUserName, objDBUserMKQXData)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getDBUserMokuaiQXData = False
            End Try

        End Function







        '----------------------------------------------------------------
        ' 获取一般用户操作日志
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strTempPath          ：临时文件目录
        '     strWhere             ：搜索字符串(数据集搜索字符串)
        '     objLogDataSet        ：返回数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_JSOALOG( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strTempPath As String, _
            ByVal strWhere As String, _
            ByRef objLogDataSet As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getDataSet_JSOALOG = .getDataSet_JSOALOG(strErrMsg, strUserId, strPassword, strTempPath, strWhere, objLogDataSet)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getDataSet_JSOALOG = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取配置管理员操作日志
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strTempPath          ：临时文件目录
        '     strWhere             ：搜索字符串(数据集搜索字符串)
        '     objLogDataSet        ：返回数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_AUDITPZLOG( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strTempPath As String, _
            ByVal strWhere As String, _
            ByRef objLogDataSet As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getDataSet_AUDITPZLOG = .getDataSet_AUDITPZLOG(strErrMsg, strUserId, strPassword, strTempPath, strWhere, objLogDataSet)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getDataSet_AUDITPZLOG = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取安全管理员操作日志
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strTempPath          ：临时文件目录
        '     strWhere             ：搜索字符串(数据集搜索字符串)
        '     objLogDataSet        ：返回数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_AUDITAQLOG( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strTempPath As String, _
            ByVal strWhere As String, _
            ByRef objLogDataSet As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getDataSet_AUDITAQLOG = .getDataSet_AUDITAQLOG(strErrMsg, strUserId, strPassword, strTempPath, strWhere, objLogDataSet)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getDataSet_AUDITAQLOG = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取审计管理员操作日志
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strTempPath          ：临时文件目录
        '     strWhere             ：搜索字符串(数据集搜索字符串)
        '     objLogDataSet        ：返回数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_AUDITSJLOG( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strTempPath As String, _
            ByVal strWhere As String, _
            ByRef objLogDataSet As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getDataSet_AUDITSJLOG = .getDataSet_AUDITSJLOG(strErrMsg, strUserId, strPassword, strTempPath, strWhere, objLogDataSet)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getDataSet_AUDITSJLOG = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取人员已经加入到角色strRoleName的列表
        '----------------------------------------------------------------
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     objConnectionProperty       ：服务器信息
        '     strWhere                    ：搜索字符串(默认表前缀a.)
        '     objRoleData                 ：信息数据集
        '     blnNone                     ：重载
        ' 返回
        '     True                        ：成功
        '     False                       ：失败

        '----------------------------------------------------------------
        Public Function getRoleData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strWhere As String, _
            ByRef objRoleData As Xydc.Platform.Common.Data.AppManagerData, _
            ByVal blnNone As Boolean) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getRoleData = .getRoleData(strErrMsg, objConnectionProperty, strWhere, objRoleData, blnNone)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getRoleData = False
            End Try

        End Function

        '-------------------------------------------------------------------------------------------
        ' 在指定服务器objConnectionProperty指定成员strUserId加入角色(m_objNewDataSet_ChoiceRole)中
        '-------------------------------------------------------------------------------------------
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     objConnectionProperty       ：服务器信息
        '     strUserId                   ：指定成员
        '     m_objNewDataSet_ChoiceRole  ：更新角色数据集
        '     m_objOldDataSet_ChoiceRole  ：原角色数据集
        ' 返回
        '     True                        ：成功
        '     False                       ：失败

        '----------------------------------------------------------------
        Public Function doAddRoleMember( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strUserId As String, _
            ByVal m_objNewDataSet_ChoiceRole As Xydc.Platform.Common.Data.AppManagerData, _
            ByVal m_objOldDataSet_ChoiceRole As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doAddRoleMember = .doAddRoleMember(strErrMsg, objConnectionProperty, strUserId, m_objNewDataSet_ChoiceRole, m_objOldDataSet_ChoiceRole)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doAddRoleMember = False
            End Try

        End Function


    End Class

End Namespace
