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
    ' 类名    ：systemCommon
    '
    ' 功能描述： 
    '   　提供对通用数据信息处理的表现层支持
    '----------------------------------------------------------------
    Public Class systemCommon
        Inherits MarshalByRefObject

        '----------------------------------------------------------------
        ' 安全释放本身资源
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.systemCommon)
            Try
                If Not (obj Is Nothing) Then
                    'obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub









        '----------------------------------------------------------------
        ' 获取记录集
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strTable             ：表名
        '     strWhere             : 条件
        '     strOrderby           : 排序
        '     objDataSet           ：信息数据集 
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strTable As String, _
            ByVal strWhere As String, _
            ByVal strOrderby As String, _
            ByRef objDataSet As System.Data.DataSet) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCommon
                    getDataSet = .getDataSet(strErrMsg, strUserId, strPassword, strTable, strWhere, strOrderby, objDataSet)
                End With
            Catch ex As Exception
                getDataSet = False
                strErrMsg = ex.Message
            End Try

        End Function




        '----------------------------------------------------------------
        ' 保存数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strTable             ：表名
        '     strWhere             : 条件
        '     objType              ：true-字段本身没有带类型，有自定义；FALSE-字段本身的首字母就是自带类型
        '                          'C=字符型，i=数字型，d=日期           
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
            ByVal strTable As String, _
            ByVal strWhere As String, _
            ByVal objType As Boolean, _
            ByVal objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCommon
                    doSaveData = .doSaveData(strErrMsg, strUserId, strPassword, strTable, strWhere, objType, objNewData, objenumEditType)
                End With
            Catch ex As Exception
                doSaveData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 删除数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strTable             ：表名
        '     strWhere             : 条件
        '     objOldData           ：旧数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strTable As String, _
            ByVal strWhere As String, _
            ByVal objOldData As System.Data.DataRow) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCommon
                    doDeleteData = .doDeleteData(strErrMsg, strUserId, strPassword, strTable, strWhere, objOldData)
                End With
            Catch ex As Exception
                doDeleteData = False
                strErrMsg = ex.Message
            End Try
        End Function



        '----------------------------------------------------------------
        ' 根据select,from,where,orderby获取SQL语句
        '     strSelect            ：select
        '     strFrom              ：from
        '     strWhere             ：where
        '     strOrderBy           ：order by
        ' 返回
        '                          ：合成后的SQL
        '----------------------------------------------------------------
        Public Function getSqlString( _
            ByVal strSelect As String, _
            ByVal strFrom As String, _
            ByVal strWhere As String, _
            ByVal strOrderBy As String) As String

            Try
                With New Xydc.Platform.BusinessRules.rulesCommon
                    getSqlString = .getSqlString(strSelect, strFrom, strWhere, strOrderBy)
                End With
            Catch ex As Exception
                getSqlString = ""
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据SQL语句获取标准的DataSet
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strSQL               ：SQL语句
        '     objDataSet           ：返回数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDataSetBySQL( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strSQL As String, _
            ByRef objDataSet As System.Data.DataSet) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCommon
                    getDataSetBySQL = .getDataSetBySQL(strErrMsg, strUserId, strPassword, strSQL, objDataSet)
                End With
            Catch ex As Exception
                getDataSetBySQL = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 在objDataTable的strField列中搜索strValue
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objDataTable         ：在objDataTable内搜索
        '     strField             ：在objDataTable内搜索strField
        '     strValue             ：要搜索的值
        '     blnFound             ：True-存在，False-不存在
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doFindInDataTable( _
            ByRef strErrMsg As String, _
            ByVal objDataTable As System.Data.DataTable, _
            ByVal strField As String, _
            ByVal strValue As String, _
            ByRef blnFound As Boolean) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCommon
                    doFindInDataTable = .doFindInDataTable(strErrMsg, objDataTable, strField, strValue, blnFound)
                End With
            Catch ex As Exception
                doFindInDataTable = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 在objDataTable的strField列中搜索intValue
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objDataTable         ：在objDataTable内搜索
        '     strField             ：在objDataTable内搜索strField
        '     intValue             ：要搜索的值
        '     blnFound             ：True-存在，False-不存在
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doFindInDataTable( _
            ByRef strErrMsg As String, _
            ByVal objDataTable As System.Data.DataTable, _
            ByVal strField As String, _
            ByVal intValue As Integer, _
            ByRef blnFound As Boolean) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCommon
                    doFindInDataTable = .doFindInDataTable(strErrMsg, objDataTable, strField, intValue, blnFound)
                End With
            Catch ex As Exception
                doFindInDataTable = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 在objDataTable的strField列中搜索dblValue
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objDataTable         ：在objDataTable内搜索
        '     strField             ：在objDataTable内搜索strField
        '     dblValue             ：要搜索的值
        '     blnFound             ：True-存在，False-不存在
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doFindInDataTable( _
            ByRef strErrMsg As String, _
            ByVal objDataTable As System.Data.DataTable, _
            ByVal strField As String, _
            ByVal dblValue As Double, _
            ByRef blnFound As Boolean) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCommon
                    doFindInDataTable = .doFindInDataTable(strErrMsg, objDataTable, strField, dblValue, blnFound)
                End With
            Catch ex As Exception
                doFindInDataTable = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 从指定FTP位置下载文件到指定的WEB服务器目录下的文件中
        ' 如果指定了strDesSpec，则可不输入strDesPath、strDesFile
        ' 如果未指定strDesSpec，则必须输入strDesPath
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strFTPPath           ：指定FTP位置(路径与文件名)
        '     strDesSpec           ：现有WEB服务器目录+文件(返回)
        '     strDesPath           ：WEB服务器目录(返回)
        '     strDesFile           ：WEB服务器目录下临时文件名(返回)
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doFTPDownLoadFile( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strFTPPath As String, _
            ByRef strDesSpec As String, _
            ByRef strDesPath As String, _
            ByRef strDesFile As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCommon
                    doFTPDownLoadFile = .doFTPDownLoadFile(strErrMsg, strUserId, strPassword, strFTPPath, strDesSpec, strDesPath, strDesFile)
                End With
            Catch ex As Exception
                doFTPDownLoadFile = False
                strErrMsg = ex.Message
            End Try

        End Function

    End Class

End Namespace
