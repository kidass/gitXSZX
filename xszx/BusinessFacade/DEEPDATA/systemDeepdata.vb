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
    ' 类名    ：systemDeepdata
    '
    ' 功能描述： 
    '   　提供对月度深层数据的表现层支持
    '----------------------------------------------------------------
    Public Class systemDeepdata
        Implements System.IDisposable
        Private m_objrulesDeepdata As Xydc.Platform.BusinessRules.rulesDeepdata

        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
            m_objrulesDeepdata = New Xydc.Platform.BusinessRules.rulesDeepdata
        End Sub

        '----------------------------------------------------------------
        ' 安全释放本身资源
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.systemDeepdata)
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
            If Not (m_objrulesDeepdata Is Nothing) Then
                m_objrulesDeepdata.Dispose()
                m_objrulesDeepdata = Nothing
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
            With Me.m_objrulesDeepdata
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
            With Me.m_objrulesDeepdata
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
            With Me.m_objrulesDeepdata
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
            With Me.m_objrulesDeepdata
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
            With Me.m_objrulesDeepdata
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
            With Me.m_objrulesDeepdata
                doExcelSheetDelete = .doExcelSheetDelete(strErrMsg, strSrcFile, strSrcSheet)
            End With
        End Function


        '----------------------------------------------------------------
        ' 根据“日期、计算类型”获取“月度深度数据分析”完全数据的数据集
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strUserId                  ：用户标识
        '     strPassword                ：用户密码
        '     strWhere                   ：搜索字符串
        '     strType                    ：计算类型
        '     strHouseType               : 物业类型：商业，写字楼，住宅
        '     strStartTime               ：开始日期
        '     strEndTime                 ：结束日期
        '     objDeepdata                ：信息数据集
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_monthCompute( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByVal strWhere_0 As String, _
            ByVal strType As String, _
            ByVal strHouseType As String, _
            ByVal strStartTime As String, _
            ByVal strEndTime As String, _
            ByRef objDeepdata As Xydc.Platform.Common.Data.DeepData) As Boolean

            With m_objrulesDeepdata
                getDataSet_monthCompute = .getDataSet_monthCompute(strErrMsg, strUserId, strPassword, strWhere, strWhere_0, strType, strHouseType, strStartTime, strEndTime, objDeepdata)
            End With
        End Function


        '----------------------------------------------------------------
        ' 根据“查询条件”获取“明细深度数据分析”完全数据的数据集
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strUserId                  ：用户标识
        '     strPassword                ：用户密码
        '     strWhere                   ：搜索字符串
        '     strWhere_1                 ：统计搜索字符串
        '     intTop                     : TOP 的数量
        '     strOrderBy                 : 排序的字段
        '     objDeepdata                ：信息数据集
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_DetailCompute( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByVal strWhere_1 As String, _
            ByVal intTop As Integer, _
            ByVal strOrderBy As String, _
            ByRef objDeepdata As Xydc.Platform.Common.Data.DeepData) As Boolean

            With m_objrulesDeepdata
                getDataSet_DetailCompute = .getDataSet_DetailCompute(strErrMsg, strUserId, strPassword, strWhere, strWhere_1, intTop, strOrderBy, objDeepdata)
            End With
        End Function

        '----------------------------------------------------------------
        ' 根据“查询条件”获取“明细深度数据”完全数据的数据集
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strUserId                  ：用户标识
        '     strPassword                ：用户密码
        '     strWhere                   ：搜索字符串       
        '     objDeepdata                ：信息数据集
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_Detail( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objDeepdata As Xydc.Platform.Common.Data.DeepData) As Boolean

            With m_objrulesDeepdata
                getDataSet_Detail = .getDataSet_Detail(strErrMsg, strUserId, strPassword, strWhere, objDeepdata)
            End With
        End Function

        '明细表导出时使用
        '----------------------------------------------------------------
        ' 根据“查询条件”获取“明细深度数据”完全数据的数据集
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strUserId                  ：用户标识
        '     strPassword                ：用户密码
        '     strWhere                   ：搜索字符串       
        '     objDeepdata                ：信息数据集
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_Detail_Print( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objDeepdata As Xydc.Platform.Common.Data.DeepData) As Boolean

            With m_objrulesDeepdata
                getDataSet_Detail_Print = .getDataSet_Detail_Print(strErrMsg, strUserId, strPassword, strWhere, objDeepdata)
            End With
        End Function



        '----------------------------------------------------------------
        ' 获取各面积段的数据集(以代码升序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWhere             ：搜索字符串
        '     objDeepData          ：信息数据集 
        '     strType              ：buildingarea,floorarea,unitprice,totalprice
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDeepdataInterVal( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objDeepData As Xydc.Platform.Common.Data.DeepData, _
            ByVal strType As String) As Boolean

            Try
                With m_objrulesDeepdata
                    getDeepdataInterVal = .getDeepdataInterVal(strErrMsg, strUserId, strPassword, strWhere, objDeepData, strType)
                End With
            Catch ex As Exception
                getDeepdataInterVal = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 保存各面积段的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
        '     objNewData           ：新数据
        '     objenumEditType      ：编辑类型
        '     strType              ：buildingarea,floorarea,unitprice,totalprice
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doSaveDeepdataInterVal( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType, _
            ByVal strType As String) As Boolean

            Try
                With m_objrulesDeepdata
                    doSaveDeepdataInterVal = .doSaveDeepdataInterVal(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType, strType)
                End With
            Catch ex As Exception
                doSaveDeepdataInterVal = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 删除各面积段的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
        '     strType              ：buildingarea,floorarea,unitprice,totalprice
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteDeepdataInterVal( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal strType As String) As Boolean

            Try
                With m_objrulesDeepdata
                    doDeleteDeepdataInterVal = .doDeleteDeepdataInterVal(strErrMsg, strUserId, strPassword, objOldData, strType)
                End With
            Catch ex As Exception
                doDeleteDeepdataInterVal = False
                strErrMsg = ex.Message
            End Try
        End Function



        '----------------------------------------------------------------
        ' 根据“查询条件”获取“客户明细深度数据”完全数据的数据集
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strUserId                  ：用户标识
        '     strPassword                ：用户密码
        '     strWhere                   ：搜索字符串       
        '     objDeepdata                ：信息数据集
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_Detail_Customer( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objDeepdata As Xydc.Platform.Common.Data.DeepData) As Boolean

            With m_objrulesDeepdata
                getDataSet_Detail_Customer = .getDataSet_Detail_Customer(strErrMsg, strUserId, strPassword, strWhere, objDeepdata)
            End With
        End Function

        '----------------------------------------------------------------
        ' 根据“查询条件”获取“客户比例分析”完全数据的数据集
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strUserId                  ：用户标识
        '     strPassword                ：用户密码
        '     strWhere                   ：搜索字符串       
        '     objDeepdata                ：信息数据集
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_AgeRatio( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objDeepdata As Xydc.Platform.Common.Data.DeepData) As Boolean

            With m_objrulesDeepdata
                getDataSet_AgeRatio = .getDataSet_AgeRatio(strErrMsg, strUserId, strPassword, strWhere, objDeepdata)
            End With
        End Function


    End Class
End Namespace
