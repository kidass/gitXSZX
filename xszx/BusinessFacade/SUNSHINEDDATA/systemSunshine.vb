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
    ' 类名    ：systemSunshine
    '
    ' 功能描述： 
    '   　提供对阳光家缘数据的表现层支持
    '----------------------------------------------------------------
    Public Class systemSunshine
        Implements System.IDisposable

        Private m_objrulesSunshine As Xydc.Platform.BusinessRules.rulesSunshine

        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
            m_objrulesSunshine = New Xydc.Platform.BusinessRules.rulesSunshine
        End Sub

        '----------------------------------------------------------------
        ' 安全释放本身资源
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.systemSunshine)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub

        '----------------------------------------------------------------
        ' 虚拟析构函数
        '----------------------------------------------------------------
        Public Sub Dispose() Implements System.IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(True)
        End Sub

        '----------------------------------------------------------------
        ' 析构函数重载
        '----------------------------------------------------------------
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If (Not disposing) Then
                Exit Sub
            End If
            If Not (m_objrulesSunshine Is Nothing) Then
                m_objrulesSunshine.Dispose()
                m_objrulesSunshine = Nothing
            End If
        End Sub

        '----------------------------------------------------------------
        ' 将objDataTable中的选定列导出到Excel的当前活动Sheet并自动写标题行
        '     strErrMsg              ：如果错误，则返回错误信息
        '     objDataTable           ：要导出的数据表
        '     objFields              ：要导出的列
        '     strExcelFile           ：导出到WEB服务器中的Excel文件路径
        '     strDateFormat          ：日期格式字符串
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        ' 更改
        '----------------------------------------------------------------
        Public Function doExportToExcel( _
            ByRef strErrMsg As String, _
            ByVal objDataTable As System.Data.DataTable, _
            ByVal objFields As System.Collections.Specialized.NameValueCollection, _
            ByVal strExcelFile As String, _
            ByVal strDateFormat As String) As Boolean
            With Me.m_objrulesSunshine
                doExportToExcel = .doExportToExcel(strErrMsg, objDataTable, objFields, strExcelFile, strDateFormat)
            End With
        End Function

        '----------------------------------------------------------------
        ' 将数据从DataSet导出到Excel
        '     strErrMsg              ：如果错误，则返回错误信息
        '     objDataTable           ：要导出的数据
        '     strExcelFile           ：导出到WEB服务器中的Excel文件路径
        '     strSheetName           ：数据导出到strSheetName
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function doExportToExcel( _
            ByRef strErrMsg As String, _
            ByVal objDataTable As System.Data.DataTable, _
            ByVal strExcelFile As String, _
            ByVal strSheetName As String) As Boolean
            With Me.m_objrulesSunshine
                doExportToExcel = .doExportToExcel(strErrMsg, objDataTable, strExcelFile, strSheetName)
            End With
        End Function

        '----------------------------------------------------------------
        ' 将数据从DataSet导出到Excel
        '     strErrMsg              ：如果错误，则返回错误信息
        '     objDataSet             ：要导出的数据集
        '     strExcelFile           ：导出到WEB服务器中的Excel文件路径
        '     strMacroName           ：宏名列表
        '     strMacroValue          ：宏值列表
        '     strDateFormat          ：日期格式字符串
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function doExportToExcel( _
            ByRef strErrMsg As String, _
            ByVal objDataSet As System.Data.DataSet, _
            ByVal strExcelFile As String, _
            Optional ByVal strMacroName As String = "", _
            Optional ByVal strMacroValue As String = "", _
            Optional ByVal strDateFormat As String = "") As Boolean
            With Me.m_objrulesSunshine
                doExportToExcel = .doExportToExcel(strErrMsg, objDataSet, strExcelFile, strMacroName, strMacroValue, strDateFormat)
            End With
        End Function

        '----------------------------------------------------------------
        ' 将数据从DataSet导出到Excel
        '     strErrMsg              ：如果错误，则返回错误信息
        '     objDataSet             ：要导出的数据集
        '     strExcelFile           ：导出到WEB服务器中的Excel文件路径
        '     strColorFieldName      ：用来确定行颜色的字段名
        '     objColors              ：字段值对应的颜色集合
        '     strMacroName           ：宏名列表
        '     strMacroValue          ：宏值列表
        '     strDateFormat          ：日期格式字符串
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function doExportToExcel( _
            ByRef strErrMsg As String, _
            ByVal objDataSet As System.Data.DataSet, _
            ByVal strExcelFile As String, _
            ByVal strColorFieldName As String, _
            ByVal objColors As System.Collections.Specialized.ListDictionary, _
            Optional ByVal strMacroName As String = "", _
            Optional ByVal strMacroValue As String = "", _
            Optional ByVal strDateFormat As String = "") As Boolean
            With Me.m_objrulesSunshine
                doExportToExcel = .doExportToExcel(strErrMsg, objDataSet, strExcelFile, strColorFieldName, objColors, strMacroName, strMacroValue, strDateFormat)
            End With
        End Function

        '----------------------------------------------------------------
        ' 将strSrcFile中的strSrcSheet复制到strDesFile中的strDesSheet
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strSrcFile             ：源文件的完整路径
        '     strSrcSheet            ：源文件的sheet名
        '     strDesFile             ：目标文件的完整路径
        '     strDesSheet            ：目标文件的sheet名
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        ' 更改描述
        '----------------------------------------------------------------
        Public Function doExcelAddCopy( _
            ByRef strErrMsg As String, _
            ByVal strSrcFile As String, _
            ByVal strSrcSheet As String, _
            ByVal strDesFile As String, _
            ByVal strDesSheet As String) As Boolean
            With Me.m_objrulesSunshine
                doExcelAddCopy = .doExcelAddCopy(strErrMsg, strSrcFile, strSrcSheet, strDesFile, strDesSheet)
            End With
        End Function

        '----------------------------------------------------------------
        ' 删除strSrcFile中的strSrcSheet
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strSrcFile             ：源文件的完整路径
        '     strSrcSheet            ：源文件的sheet名
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        ' 更改描述
        '----------------------------------------------------------------
        Public Function doExcelSheetDelete( _
            ByRef strErrMsg As String, _
            ByVal strSrcFile As String, _
            ByVal strSrcSheet As String) As Boolean
            With Me.m_objrulesSunshine
                doExcelSheetDelete = .doExcelSheetDelete(strErrMsg, strSrcFile, strSrcSheet)
            End With
        End Function







        '----------------------------------------------------------------
        ' 获取楼盘匹配的数据集(以代码升序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWhere             ：搜索字符串
        '     objSunshine          ：信息数据集 
        '     strType              ：buildingarea,floorarea,unitprice,totalprice
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getHouseMatch( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objSunshine As Xydc.Platform.Common.Data.SunshineData) As Boolean

            Try
                With m_objrulesSunshine
                    getHouseMatch = .getHouseMatch(strErrMsg, strUserId, strPassword, strWhere, objSunshine)
                End With
            Catch ex As Exception
                getHouseMatch = False
                strErrMsg = ex.Message
            End Try

        End Function



        '----------------------------------------------------------------
        ' 获取楼盘匹配的数据集(以代码升序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWhere             ：搜索字符串
        '     objSunshine          ：信息数据集 
        '     strType              ：buildingarea,floorarea,unitprice,totalprice
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getSunshineHouseMatch( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objSunshine As Xydc.Platform.Common.Data.SunshineData) As Boolean

            Try
                With m_objrulesSunshine
                    getSunshineHouseMatch = .getSunshineHouseMatch(strErrMsg, strUserId, strPassword, strWhere, objSunshine)
                End With
            Catch ex As Exception
                getSunshineHouseMatch = False
                strErrMsg = ex.Message
            End Try

        End Function



        '----------------------------------------------------------------
        ' 保存楼盘匹配的数据
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
        Public Function doSaveSunshineHouseMatch( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Try
                With m_objrulesSunshine
                    doSaveSunshineHouseMatch = .doSaveSunshineHouseMatch(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType)
                End With
            Catch ex As Exception
                doSaveSunshineHouseMatch = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 删除楼盘匹配的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteSunshineHouseMatch( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow) As Boolean

            Try
                With m_objrulesSunshine
                    doDeleteSunshineHouseMatch = .doDeleteSunshineHouseMatch(strErrMsg, strUserId, strPassword, objOldData)
                End With
            Catch ex As Exception
                doDeleteSunshineHouseMatch = False
                strErrMsg = ex.Message
            End Try
        End Function


        '----------------------------------------------------------------
        ' 生成日楼盘数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doExecProcedureHouseData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String) As Boolean

            Try
                With m_objrulesSunshine
                    doExecProcedureHouseData = .doExecProcedureHouseData(strErrMsg, strUserId, strPassword)
                End With
            Catch ex As Exception
                doExecProcedureHouseData = False
                strErrMsg = ex.Message
            End Try
        End Function


        '----------------------------------------------------------------
        ' 删除单个日楼盘数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteHouseDataProcedure( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef strHouse As String) As Boolean

            Try
                With m_objrulesSunshine
                    doDeleteHouseDataProcedure = .doDeleteHouseDataProcedure(strErrMsg, strUserId, strPassword, strHouse)
                End With
            Catch ex As Exception
                doDeleteHouseDataProcedure = False
                strErrMsg = ex.Message
            End Try
        End Function

        '----------------------------------------------------------------
        ' 生成单个日楼盘数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doExecHouseDataProcedure( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef strHouse As String) As Boolean

            Try
                With m_objrulesSunshine
                    doExecHouseDataProcedure = .doExecHouseDataProcedure(strErrMsg, strUserId, strPassword, strHouse)
                End With
            Catch ex As Exception
                doExecHouseDataProcedure = False
                strErrMsg = ex.Message
            End Try
        End Function


        '----------------------------------------------------------------
        ' 根据“查询条件”获取“阳光家缘楼盘信息数据分析”完全数据的数据集
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strUserId                  ：用户标识
        '     strPassword                ：用户密码
        '     strWhere                   ：搜索字符串 
        '     objParameter               : 查询条件参数
        '     objSunshine                ：信息数据集
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_BuildingCompute( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByVal objParameter As System.Collections.Specialized.NameValueCollection, _
            ByRef objSunshine As Xydc.Platform.Common.Data.SunshineData) As Boolean

            With m_objrulesSunshine
                getDataSet_BuildingCompute = .getDataSet_BuildingCompute(strErrMsg, strUserId, strPassword, strWhere, objParameter, objSunshine)
            End With
        End Function


        '----------------------------------------------------------------
        ' 根据“查询条件”获取“阳光家缘楼盘信息数据分析”完全数据的数据集
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strUserId                  ：用户标识
        '     strPassword                ：用户密码
        '     strWhere                   ：搜索字符串 
        '     objParameter               : 查询条件参数
        '     objSunshine                ：信息数据集
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_BuildingDetailCompute( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByVal objParameter As System.Collections.Specialized.NameValueCollection, _
            ByRef objSunshine As Xydc.Platform.Common.Data.SunshineData) As Boolean

            With m_objrulesSunshine
                getDataSet_BuildingDetailCompute = .getDataSet_BuildingDetailCompute(strErrMsg, strUserId, strPassword, strWhere, objParameter, objSunshine)
            End With
        End Function


        '----------------------------------------------------------------
        ' 根据“查询条件”获取“阳光家缘楼盘信息数据分析”完全数据的数据集
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strUserId                  ：用户标识
        '     strPassword                ：用户密码
        '     strWhere                   ：搜索字符串 
        '     objParameter               : 查询条件参数
        '     objSunshine                ：信息数据集
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_BuildingCompute_v2( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByVal objParameter As System.Collections.Specialized.NameValueCollection, _
            ByRef objSunshine As Xydc.Platform.Common.Data.SunshineData) As Boolean

            With m_objrulesSunshine
                getDataSet_BuildingCompute_v2 = .getDataSet_BuildingCompute_v2(strErrMsg, strUserId, strPassword, strWhere, objParameter, objSunshine)
            End With
        End Function


        '----------------------------------------------------------------
        ' 根据“查询条件”获取“阳光家缘楼盘信息数据分析”完全数据的数据集
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strUserId                  ：用户标识
        '     strPassword                ：用户密码
        '     strWhere                   ：搜索字符串 
        '     objParameter               : 查询条件参数
        '     objSunshine                ：信息数据集
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_BuildingCompute_v3( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByVal objParameter As System.Collections.Specialized.NameValueCollection, _
            ByRef objSunshine As Xydc.Platform.Common.Data.SunshineData) As Boolean

            With m_objrulesSunshine
                getDataSet_BuildingCompute_v3 = .getDataSet_BuildingCompute_v3(strErrMsg, strUserId, strPassword, strWhere, objParameter, objSunshine)
            End With
        End Function


        '----------------------------------------------------------------
        ' 根据“查询条件”获取“阳光家缘楼盘信息数据分析”完全数据的数据集
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strUserId                  ：用户标识
        '     strPassword                ：用户密码
        '     strWhere                   ：搜索字符串 
        '     objParameter               : 查询条件参数
        '     objSunshine                ：信息数据集
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_WEEKBuildingCompute( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByVal objParameter As System.Collections.Specialized.NameValueCollection, _
            ByRef objSunshine As Xydc.Platform.Common.Data.SunshineData) As Boolean

            With m_objrulesSunshine
                getDataSet_WEEKBuildingCompute = .getDataSet_WEEKBuildingCompute(strErrMsg, strUserId, strPassword, strWhere, objParameter, objSunshine)
            End With
        End Function


        '----------------------------------------------------------------
        ' 根据“查询条件”获取“阳光家缘楼盘信息数据分析”完全数据的数据集
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strUserId                  ：用户标识
        '     strPassword                ：用户密码
        '     strWhere                   ：搜索字符串 
        '     objParameter               : 查询条件参数
        '     objSunshine                ：信息数据集
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_WEEKBuildingCompute_v2( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByVal objParameter As System.Collections.Specialized.NameValueCollection, _
            ByRef objSunshine As Xydc.Platform.Common.Data.SunshineData) As Boolean

            With m_objrulesSunshine
                getDataSet_WEEKBuildingCompute_v2 = .getDataSet_WEEKBuildingCompute_v2(strErrMsg, strUserId, strPassword, strWhere, objParameter, objSunshine)
            End With
        End Function


        '----------------------------------------------------------------
        ' 根据“查询条件”获取“阳光家缘楼盘信息数据分析”完全数据的数据集
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strUserId                  ：用户标识
        '     strPassword                ：用户密码
        '     strWhere                   ：搜索字符串 
        '     objParameter               : 查询条件参数
        '     objSunshine                ：信息数据集
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_WEEKBuildingCompute_v3( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByVal objParameter As System.Collections.Specialized.NameValueCollection, _
            ByRef objSunshine As Xydc.Platform.Common.Data.SunshineData) As Boolean

            With m_objrulesSunshine
                getDataSet_WEEKBuildingCompute_v3 = .getDataSet_WEEKBuildingCompute_v3(strErrMsg, strUserId, strPassword, strWhere, objParameter, objSunshine)
            End With
        End Function


        '----------------------------------------------------------------
        ' 根据“查询条件”获取“阳光家缘楼盘信息数据分析”完全数据的数据集
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strUserId                  ：用户标识
        '     strPassword                ：用户密码
        '     strWhere                   ：搜索字符串 
        '     objParameter               : 查询条件参数
        '     objSunshine                ：信息数据集
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_RegionBuildingCompute( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByVal objParameter As System.Collections.Specialized.NameValueCollection, _
            ByRef objSunshine As Xydc.Platform.Common.Data.SunshineData) As Boolean

            With m_objrulesSunshine
                getDataSet_RegionBuildingCompute = .getDataSet_RegionBuildingCompute(strErrMsg, strUserId, strPassword, strWhere, objParameter, objSunshine)
            End With
        End Function


        '----------------------------------------------------------------
        ' 根据“查询条件”获取“阳光家缘楼盘信息数据分析”完全数据的数据集
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strUserId                  ：用户标识
        '     strPassword                ：用户密码
        '     strWhere                   ：搜索字符串 
        '     objParameter               : 查询条件参数
        '     objSunshine                ：信息数据集
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_RegionBuildingCompute_v2( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByVal objParameter As System.Collections.Specialized.NameValueCollection, _
            ByRef objSunshine As Xydc.Platform.Common.Data.SunshineData) As Boolean

            With m_objrulesSunshine
                getDataSet_RegionBuildingCompute_v2 = .getDataSet_RegionBuildingCompute_v2(strErrMsg, strUserId, strPassword, strWhere, objParameter, objSunshine)
            End With
        End Function


        '----------------------------------------------------------------
        ' 根据“查询条件”获取“阳光家缘楼盘信息数据分析”完全数据的数据集
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strUserId                  ：用户标识
        '     strPassword                ：用户密码
        '     strWhere                   ：搜索字符串 
        '     objParameter               : 查询条件参数
        '     objSunshine                ：信息数据集
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_RegionBuildingCompute_v3( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByVal objParameter As System.Collections.Specialized.NameValueCollection, _
            ByRef objSunshine As Xydc.Platform.Common.Data.SunshineData) As Boolean

            With m_objrulesSunshine
                getDataSet_RegionBuildingCompute_v3 = .getDataSet_RegionBuildingCompute_v3(strErrMsg, strUserId, strPassword, strWhere, objParameter, objSunshine)
            End With
        End Function



        '----------------------------------------------------------------
        ' 根据“查询条件”获取“阳光家缘楼盘信息数据分析”完全数据的数据集
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strUserId                  ：用户标识
        '     strPassword                ：用户密码
        '     strWhere                   ：搜索字符串 
        '     objParameter               : 查询条件参数
        '     objSunshine                ：信息数据集
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_RegionCompute( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByVal objParameter As System.Collections.Specialized.NameValueCollection, _
            ByRef objSunshine As Xydc.Platform.Common.Data.SunshineData) As Boolean

            With m_objrulesSunshine
                getDataSet_RegionCompute = .getDataSet_RegionCompute(strErrMsg, strUserId, strPassword, strWhere, objParameter, objSunshine)
            End With
        End Function


        '----------------------------------------------------------------
        ' 获取周楼盘匹配的数据集(以代码升序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWhere             ：搜索字符串
        '     objSunshine          ：信息数据集 
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getWeekMonitoringHouse( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objSunshine As Xydc.Platform.Common.Data.SunshineData) As Boolean

            Try
                With m_objrulesSunshine
                    getWeekMonitoringHouse = .getWeekMonitoringHouse(strErrMsg, strUserId, strPassword, strWhere, objSunshine)
                End With
            Catch ex As Exception
                getWeekMonitoringHouse = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 保存楼盘匹配的数据
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
        Public Function doSaveSunshineWeekMonitoringHouse( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Try
                With m_objrulesSunshine
                    doSaveSunshineWeekMonitoringHouse = .doSaveSunshineWeekMonitoringHouse(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType)
                End With
            Catch ex As Exception
                doSaveSunshineWeekMonitoringHouse = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 删除楼盘匹配的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteSunshineWeekMonitoringHouse( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow) As Boolean

            Try
                With m_objrulesSunshine
                    doDeleteSunshineWeekMonitoringHouse = .doDeleteSunshineWeekMonitoringHouse(strErrMsg, strUserId, strPassword, objOldData)
                End With
            Catch ex As Exception
                doDeleteSunshineWeekMonitoringHouse = False
                strErrMsg = ex.Message
            End Try
        End Function




        '----------------------------------------------------------------
        ' 获取月楼盘匹配的数据集(以代码升序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWhere             ：搜索字符串
        '     objSunshine          ：信息数据集 
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getMonthMonitoringHouse( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objSunshine As Xydc.Platform.Common.Data.SunshineData) As Boolean

            Try
                With m_objrulesSunshine
                    getMonthMonitoringHouse = .getMonthMonitoringHouse(strErrMsg, strUserId, strPassword, strWhere, objSunshine)
                End With
            Catch ex As Exception
                getMonthMonitoringHouse = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 保存月楼盘匹配的数据
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
        Public Function doSaveSunshineMonthMonitoringHouse( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Try
                With m_objrulesSunshine
                    doSaveSunshineMonthMonitoringHouse = .doSaveSunshineMonthMonitoringHouse(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType)
                End With
            Catch ex As Exception
                doSaveSunshineMonthMonitoringHouse = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 删除月楼盘匹配的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteSunshineMonthMonitoringHouse( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow) As Boolean

            Try
                With m_objrulesSunshine
                    doDeleteSunshineMonthMonitoringHouse = .doDeleteSunshineMonthMonitoringHouse(strErrMsg, strUserId, strPassword, objOldData)
                End With
            Catch ex As Exception
                doDeleteSunshineMonthMonitoringHouse = False
                strErrMsg = ex.Message
            End Try
        End Function

        '----------------------------------------------------------------
        ' 根据“查询条件”获取“N个星期的价格和套数”
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strUserId                  ：用户标识
        '     strPassword                ：用户密码
        '     strWhere                   ：搜索字符串 
        '     objParameter               : 查询条件参数
        '     objSunshine                ：信息数据集
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_NWeek_Compute( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByVal objParameter As System.Collections.Specialized.NameValueCollection, _
            ByRef objSunshine As Xydc.Platform.Common.Data.SunshineData) As Boolean
            Try
                With m_objrulesSunshine
                    getDataSet_NWeek_Compute = .getDataSet_NWeek_Compute(strErrMsg, strUserId, strPassword, strWhere, objParameter, objSunshine)
                End With
            Catch ex As Exception
                getDataSet_NWeek_Compute = False
                strErrMsg = ex.Message
            End Try
        End Function


        '----------------------------------------------------------------
        ' 根据“查询条件”获取“N个星期的价格和套数”
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strUserId                  ：用户标识
        '     strPassword                ：用户密码
        '     strWhere                   ：搜索字符串 
        '     objParameter               : 查询条件参数
        '     objSunshine                ：信息数据集
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_NWeek_Compute_v2( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByVal objParameter As System.Collections.Specialized.NameValueCollection, _
            ByRef objSunshine As Xydc.Platform.Common.Data.SunshineData) As Boolean
            Try
                With m_objrulesSunshine
                    getDataSet_NWeek_Compute_v2 = .getDataSet_NWeek_Compute_v2(strErrMsg, strUserId, strPassword, strWhere, objParameter, objSunshine)
                End With
            Catch ex As Exception
                getDataSet_NWeek_Compute_v2 = False
                strErrMsg = ex.Message
            End Try
        End Function


        '----------------------------------------------------------------
        ' 根据“查询条件”获取“N个星期的价格和套数”
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strUserId                  ：用户标识
        '     strPassword                ：用户密码
        '     strWhere                   ：搜索字符串 
        '     objParameter               : 查询条件参数
        '     objSunshine                ：信息数据集
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_NWeek_Compute_v3( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByVal objParameter As System.Collections.Specialized.NameValueCollection, _
            ByRef objSunshine As Xydc.Platform.Common.Data.SunshineData) As Boolean
            Try
                With m_objrulesSunshine
                    getDataSet_NWeek_Compute_v3 = .getDataSet_NWeek_Compute_v3(strErrMsg, strUserId, strPassword, strWhere, objParameter, objSunshine)
                End With
            Catch ex As Exception
                getDataSet_NWeek_Compute_v3 = False
                strErrMsg = ex.Message
            End Try
        End Function


        '----------------------------------------------------------------
        ' 获取楼盘排序的数据集(以代码升序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWhere             ：搜索字符串
        '     objSunshine          ：信息数据集 
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getHouseSort( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objSunshine As Xydc.Platform.Common.Data.SunshineData) As Boolean

            Try
                With m_objrulesSunshine
                    getHouseSort = .getHouseSort(strErrMsg, strUserId, strPassword, strWhere, objSunshine)
                End With
            Catch ex As Exception
                getHouseSort = False
                strErrMsg = ex.Message
            End Try

        End Function

    End Class

End Namespace
