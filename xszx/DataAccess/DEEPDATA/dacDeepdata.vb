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

Imports Microsoft.VisualBasic

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient

Imports Xydc.Platform.Common
Imports Xydc.Platform.Common.Data
Imports Xydc.Platform.SystemFramework

Namespace Xydc.Platform.DataAccess

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.DataAccess
    ' 类名    ：dacDeepdata
    '
    ' 功能描述：
    '     提供对月度深层数据相关的数据层操作    

    '----------------------------------------------------------------
    Public Class dacDeepdata
        Implements IDisposable

        Private m_objSqlDataAdapter As System.Data.SqlClient.SqlDataAdapter








        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
            m_objSqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter
        End Sub

        '----------------------------------------------------------------
        ' 虚拟析构函数
        '----------------------------------------------------------------
        Public Sub Dispose() Implements IDisposable.Dispose
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
            If Not m_objSqlDataAdapter Is Nothing Then
                m_objSqlDataAdapter.Dispose()
                m_objSqlDataAdapter = Nothing
            End If
        End Sub

        '----------------------------------------------------------------
        ' 安全释放本身资源
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.DataAccess.dacDeepdata)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
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
            With New Xydc.Platform.DataAccess.dacExcel
                'doExportToExcel = .doExport(strErrMsg, objDataTable, objFields, strExcelFile, strDateFormat)
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
            With New Xydc.Platform.DataAccess.dacExcel
                doExportToExcel = .doExport(strErrMsg, objDataTable, strExcelFile, strSheetName)
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
            With New Xydc.Platform.DataAccess.dacExcel
                doExportToExcel = .doExport(strErrMsg, objDataSet, strExcelFile)
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
            With New Xydc.Platform.DataAccess.dacExcel
                'doExportToExcel = .doExport(strErrMsg, objDataSet, strExcelFile, strColorFieldName, objColors, strMacroName, strMacroValue, strDateFormat)
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
            With New Xydc.Platform.DataAccess.dacExcel
                'doExcelAddCopy = .doSheetAddCopy(strErrMsg, strSrcFile, strSrcSheet, strDesFile, strDesSheet)
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
            With New Xydc.Platform.DataAccess.dacExcel
                'doExcelSheetDelete = .doSheetDelete(strErrMsg, strSrcFile, strSrcSheet)
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

            Dim objTempDeepdata As Xydc.Platform.Common.Data.DeepData = Nothing
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Nothing
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand = Nothing
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim strSQL As String = ""

            '初始化
            getDataSet_monthCompute = False
            objDeepdata = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strWhere.Length > 0 Then strWhere = strWhere.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：[月度深度数据分析]未指定[连接用户]！"
                    GoTo errProc
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                Try
                    '创建数据集
                    objTempDeepdata = New Xydc.Platform.Common.Data.DeepData(Xydc.Platform.Common.Data.DeepData.enumTableType.House_V_Data_Statistics)


                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        Select Case strType
                            Case "0"
                                strSQL = ""
                                strSQL = strSQL + " exec SalesMessage_P_computeAll_V2 " + strWhere + "," + strWhere_0 + ", @strHouseType,@startDate,@endDate" + vbCr
                            Case "1"
                                strSQL = ""
                                strSQL = strSQL + " exec SalesMessage_P_getEveryRegion_V2 " + strWhere + "," + strWhere_0 + ", @strHouseType,@startDate,@endDate" + vbCr

                            Case "2"
                                strSQL = ""
                                strSQL = strSQL + " exec SalesMessage_P_getEveryhouseType_V2 " + strWhere + "," + strWhere_0 + ", @strHouseType,@startDate,@endDate" + vbCr

                            Case "3"
                                strSQL = ""
                                strSQL = strSQL + " exec SalesMessage_P_getEveryHouseTypeCalc_V2 " + strWhere + "," + strWhere_0 + ",@strHouseType,@startDate,@endDate" + vbCr

                            Case "4"
                                strSQL = ""
                                strSQL = strSQL + " exec SalesMessage_P_getEveryBuildingArea_V2 " + strWhere + "," + strWhere_0 + ",@strHouseType,@startDate,@endDate" + vbCr

                            Case "5"
                                strSQL = ""
                                strSQL = strSQL + " exec SalesMessage_P_getEveryFloorArea_V2 " + strWhere + "," + strWhere_0 + ",@strHouseType,@startDate,@endDate" + vbCr

                            Case "6"
                                strSQL = ""
                                strSQL = strSQL + " exec SalesMessage_P_getEveryUnitPrice_V2 " + strWhere + "," + strWhere_0 + ",@strHouseType,@startDate,@endDate" + vbCr

                            Case "7"
                                strSQL = ""
                                strSQL = strSQL + " exec SalesMessage_P_getEveryTotalPrice_V2 " + strWhere + "," + strWhere_0 + ",@strHouseType,@startDate,@endDate" + vbCr

                            Case "8"
                                strSQL = ""
                                strSQL = strSQL + " exec SalesMessage_P_getRegion_V2 " + strWhere + "," + strWhere_0 + ",@strHouseType,@startDate,@endDate" + vbCr

                            Case "9"
                                strSQL = ""
                                strSQL = strSQL + " exec SalesMessage_P_getMainhouse_V2 " + strWhere + "," + strWhere_0 + ",@strHouseType,@startDate,@endDate" + vbCr

                            Case "10"
                                strSQL = ""
                                strSQL = strSQL + " exec SalesMessage_P_computeSixRegion_V2 " + strWhere + "," + strWhere_0 + ", @strHouseType,@startDate,@endDate" + vbCr

                            Case "11"
                                strSQL = ""
                                strSQL = strSQL + " exec SalesMessage_P_computeTenRegion_V2 " + strWhere + "," + strWhere_0 + ", @strHouseType,@startDate,@endDate" + vbCr


                            Case Else

                                strSQL = ""
                                strSQL = strSQL + " exec SalesMessage_P_computeAll_V2 @strHouseType,@startDate,@endDate" + vbCr
                        End Select


                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@startDate", CType(strStartTime, System.DateTime))
                        objSqlCommand.Parameters.AddWithValue("@endDate", CType(strEndTime, System.DateTime))
                        objSqlCommand.Parameters.AddWithValue("@strHouseType", strHouseType)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempDeepdata.Tables(Xydc.Platform.Common.Data.DeepData.TABLE_House_V_Data_Statistics))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDeepdata.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.DeepData.SafeRelease(objTempDeepdata)
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objDeepdata = objTempDeepdata
            getDataSet_monthCompute = True
            Exit Function
errProc:
            Xydc.Platform.Common.Data.DeepData.SafeRelease(objTempDeepdata)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function
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

            Dim objTempDeepdata As Xydc.Platform.Common.Data.DeepData = Nothing
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Nothing
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand = Nothing
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim strSQL As String = ""

            '初始化
            getDataSet_DetailCompute = False
            objDeepdata = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strWhere.Length > 0 Then strWhere = strWhere.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：[明细深度数据分析]未指定[连接用户]！"
                    GoTo errProc
                End If
                Dim strTop As String
                If intTop = 0 Then
                    strTop = ""
                Else
                    strTop = CStr(intTop)
                End If
                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                Try
                    '创建数据集
                    objTempDeepdata = New Xydc.Platform.Common.Data.DeepData(Xydc.Platform.Common.Data.DeepData.enumTableType.House_V_Data_DetailStatistics)


                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " exec  dbo.SalesMessage_P_ComputeDetail @intTop,@strOrderBy,@strWhere,@strWhere_1"

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@intTop", strTop)
                        objSqlCommand.Parameters.AddWithValue("@strOrderBy", strOrderBy)
                        objSqlCommand.Parameters.AddWithValue("@strWhere", strWhere)
                        objSqlCommand.Parameters.AddWithValue("@strWhere_1", strWhere_1)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempDeepdata.Tables(Xydc.Platform.Common.Data.DeepData.TABLE_House_V_Data_DetailStatistics))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDeepdata.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.DeepData.SafeRelease(objTempDeepdata)
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objDeepdata = objTempDeepdata
            getDataSet_DetailCompute = True
            Exit Function
errProc:
            Xydc.Platform.Common.Data.DeepData.SafeRelease(objTempDeepdata)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function
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
        '明细表数据导出拆分“成交日期” 2013-04-28
        Public Function getDataSet_Detail_Print( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objDeepdata As Xydc.Platform.Common.Data.DeepData) As Boolean

            Dim objTempDeepdata As Xydc.Platform.Common.Data.DeepData = Nothing
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Nothing
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand = Nothing
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim strSQL As String = ""

            '初始化
            getDataSet_Detail_Print = False
            objDeepdata = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strWhere.Length > 0 Then strWhere = strWhere.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：[明细深度数据]未指定[连接用户]！"
                    GoTo errProc
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                Try
                    '创建数据集
                    objTempDeepdata = New Xydc.Platform.Common.Data.DeepData(Xydc.Platform.Common.Data.DeepData.enumTableType.House_B_SalesMessage)


                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL

                        strSQL = ""
                        strSQL = strSQL + " select a.* from ("
                        strSQL = strSQL + " SELECT  a.SalesMessageID,a.MainHouse, a.PartialHouse, a.Region, a.HouseAddress, a.RoomNumber, a.HouseType, "
                        strSQL = strSQL + " a.Floor, a.RoomTypeCalc, a.TotalFloor, a.EnclosedPatio, a.NotEnclosedPatio, a.Washroom, "
                        strSQL = strSQL + " a.BuildingArea, a.FloorArea, a.UnitPrice, a.TotalPrice, convert(varchar(4),year(a.FixtureDate)) as FdYear, "
                        strSQL = strSQL + " convert(varchar(4),month(a.FixtureDate)) as FdMonth, convert(varchar(4),day(a.FixtureDate)) as FdDay, 类型=1 FROM House_B_SalesMessage a " + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere
                        End If
                        strSQL = strSQL + " )a "
                        'strSQL = strSQL + " order by 类型,a.FixtureDate"
                        strSQL = strSQL + " order by 类型,a.FdYear"

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempDeepdata.Tables(Xydc.Platform.Common.Data.DeepData.TABLE_House_B_SalesMessage))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDeepdata.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.DeepData.SafeRelease(objTempDeepdata)
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objDeepdata = objTempDeepdata
            getDataSet_Detail_Print = True
            Exit Function
errProc:
            Xydc.Platform.Common.Data.DeepData.SafeRelease(objTempDeepdata)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function
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

            Dim objTempDeepdata As Xydc.Platform.Common.Data.DeepData = Nothing
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Nothing
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand = Nothing
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim strSQL As String = ""

            '初始化
            getDataSet_Detail = False
            objDeepdata = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strWhere.Length > 0 Then strWhere = strWhere.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：[明细深度数据]未指定[连接用户]！"
                    GoTo errProc
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                Try
                    '创建数据集
                    objTempDeepdata = New Xydc.Platform.Common.Data.DeepData(Xydc.Platform.Common.Data.DeepData.enumTableType.House_B_SalesMessage)


                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL

                        strSQL = ""
                        strSQL = strSQL + " select a.* from ("
                        strSQL = strSQL + " SELECT  a.[SalesMessageID],a.MainHouse, a.PartialHouse, a.Region, a.HouseAddress, a.RoomNumber, a.HouseType, "
                        strSQL = strSQL + " a.[Floor], a.RoomTypeCalc, a.TotalFloor, a.EnclosedPatio, a.NotEnclosedPatio, a.Washroom, "
                        strSQL = strSQL + " a.BuildingArea, a.FloorArea, a.UnitPrice, a.TotalPrice, a.FixtureDate,类型=1  FROM House_B_SalesMessage a " + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere
                        End If
                        strSQL = strSQL + " )a "
                        strSQL = strSQL + " order by 类型,a.FixtureDate"

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempDeepdata.Tables(Xydc.Platform.Common.Data.DeepData.TABLE_House_B_SalesMessage))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDeepdata.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.DeepData.SafeRelease(objTempDeepdata)
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objDeepdata = objTempDeepdata
            getDataSet_Detail = True
            Exit Function
errProc:
            Xydc.Platform.Common.Data.DeepData.SafeRelease(objTempDeepdata)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function
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

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempDeepData As Xydc.Platform.Common.Data.DeepData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getDeepdataInterVal = False
            objDeepData = Nothing
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strWhere.Length > 0 Then strWhere = strWhere.Trim()

                '检查
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                Dim strSQL As String
                Try
                    '创建数据集
                    Select Case strType
                        Case "buildingarea"
                            objTempDeepData = New Xydc.Platform.Common.Data.DeepData(Xydc.Platform.Common.Data.DeepData.enumTableType.House_B_BuildingArea_Interval)
                        Case "floorarea"
                            objTempDeepData = New Xydc.Platform.Common.Data.DeepData(Xydc.Platform.Common.Data.DeepData.enumTableType.House_B_FloorArea_Interval)
                        Case "unitprice"
                            objTempDeepData = New Xydc.Platform.Common.Data.DeepData(Xydc.Platform.Common.Data.DeepData.enumTableType.House_B_UnitPrice_Interval)
                        Case "totalprice"
                            objTempDeepData = New Xydc.Platform.Common.Data.DeepData(Xydc.Platform.Common.Data.DeepData.enumTableType.House_B_TotalPrice_Interval)
                        Case Else
                    End Select

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from " + vbCr
                        Select Case strType
                            Case "buildingarea"
                                strSQL = strSQL + " House_B_BuildingArea_Interval" + vbCr
                            Case "floorarea"
                                strSQL = strSQL + " House_B_FloorArea_Interval" + vbCr
                            Case "unitprice"
                                strSQL = strSQL + " House_B_UnitPrice_Interval" + vbCr
                            Case "totalprice"
                                strSQL = strSQL + " House_B_TotalPrice_Interval" + vbCr
                            Case Else

                        End Select
                        strSQL = strSQL + "  a " + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        'strSQL = strSQL + " order by a.代码 " + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        '执行操作
                        Select Case strType
                            Case "buildingarea"
                                .Fill(objTempDeepData.Tables(Xydc.Platform.Common.Data.DeepData.TABLE_House_B_BuildingArea_Interval))
                            Case "floorarea"
                                .Fill(objTempDeepData.Tables(Xydc.Platform.Common.Data.DeepData.TABLE_House_B_FloorArea_Interval))
                            Case "unitprice"
                                .Fill(objTempDeepData.Tables(Xydc.Platform.Common.Data.DeepData.TABLE_House_B_UnitPrice_Interval))
                            Case "totalprice"
                                .Fill(objTempDeepData.Tables(Xydc.Platform.Common.Data.DeepData.TABLE_House_B_TotalPrice_Interval))
                            Case Else
                        End Select

                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDeepData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.DeepData.SafeRelease(objTempDeepData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objDeepData = objTempDeepData
            getDeepdataInterVal = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.DeepData.SafeRelease(objTempDeepData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function
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


            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            doSaveDeepdataInterVal = False
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""

                '检查
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "错误：未传入新的数据！"
                    GoTo errProc
                End If
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                    Case Else
                        If objOldData Is Nothing Then
                            strErrMsg = "错误：未传入旧的数据！"
                            GoTo errProc
                        End If
                End Select

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '开始事务
                Try
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '保存数据
                Dim strSQL As String
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '计算SQL
                    Dim strOldDM As String
                    Dim strDM As String
                    Dim strTable As String
                    Dim strIntervalStart As String
                    Dim strIntervalEnd As String

                    strIntervalStart = objNewData(Xydc.Platform.Common.Data.DeepData.FIELD_House_B_IntervalStart).Trim()
                    strIntervalEnd = objNewData(Xydc.Platform.Common.Data.DeepData.FIELD_House_B_IntervalEnd).Trim()
                    Select Case objenumEditType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                            strSQL = ""
                            Select Case strType
                                Case "buildingarea"
                                    strDM = objNewData(Xydc.Platform.Common.Data.DeepData.FIELD_House_B_BuildingArea_Code).Trim()
                                    strSQL = strSQL + " insert into House_B_BuildingArea_Interval (BuildingAreaCode,IntervalStart,IntervalEnd)"
                                Case "floorarea"
                                    strDM = objNewData(Xydc.Platform.Common.Data.DeepData.FIELD_House_B_FloorArea_Code).Trim()
                                    strSQL = strSQL + " insert into House_B_FloorArea_Interval (FloorAreaCode,IntervalStart,IntervalEnd)"
                                Case "unitprice"
                                    strDM = objNewData(Xydc.Platform.Common.Data.DeepData.FIELD_House_B_UnitPrice_Code).Trim()
                                    strSQL = strSQL + " insert into House_B_UnitPrice_Interval (UnitPriceCode,IntervalStart,IntervalEnd)"
                                Case "totalprice"
                                    strDM = objNewData(Xydc.Platform.Common.Data.DeepData.FIELD_House_B_TotalPrice_Code).Trim()
                                    strSQL = strSQL + " insert into House_B_TotalPrice_Interval (TotalPriceCode,IntervalStart,IntervalEnd)"
                                Case Else

                            End Select

                            strSQL = strSQL + " values (@dm, @intervalstart,@intervalend)"
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@dm", strDM)
                            objSqlCommand.Parameters.AddWithValue("@intervalstart", CType(strIntervalStart, System.Double))
                            objSqlCommand.Parameters.AddWithValue("@intervalend", CType(strIntervalEnd, System.Double))
                        Case Else

                            strSQL = ""
                            Select Case strType
                                Case "buildingarea"
                                    strDM = objNewData(Xydc.Platform.Common.Data.DeepData.FIELD_House_B_BuildingArea_Code).Trim()
                                    With New Xydc.Platform.Common.Utilities.PulicParameters
                                        strOldDM = .getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.DeepData.FIELD_House_B_BuildingArea_Code), "")
                                    End With
                                    strSQL = strSQL + " update House_B_BuildingArea_Interval set"
                                    strSQL = strSQL + "   BuildingAreaCode= @dm,"
                                    strSQL = strSQL + "   IntervalStart = @intervalstart,"
                                    strSQL = strSQL + "   IntervalEnd = @IntervalEnd"
                                    strSQL = strSQL + " where BuildingAreaCode = @olddm"
                                Case "floorarea"
                                    strDM = objNewData(Xydc.Platform.Common.Data.DeepData.FIELD_House_B_FloorArea_Code).Trim()
                                    With New Xydc.Platform.Common.Utilities.PulicParameters
                                        strOldDM = .getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.DeepData.FIELD_House_B_FloorArea_Code), "")
                                    End With
                                    strSQL = strSQL + " update House_B_FloorArea_Interval  set"
                                    strSQL = strSQL + "   FloorAreaCode = @dm,"
                                    strSQL = strSQL + "   IntervalStart = @intervalstart,"
                                    strSQL = strSQL + "   IntervalEnd = @IntervalEnd"
                                    strSQL = strSQL + " where FloorAreaCode = @olddm"
                                Case "unitprice"
                                    strDM = objNewData(Xydc.Platform.Common.Data.DeepData.FIELD_House_B_UnitPrice_Code).Trim()
                                    With New Xydc.Platform.Common.Utilities.PulicParameters
                                        strOldDM = .getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.DeepData.FIELD_House_B_UnitPrice_Code), "")
                                    End With
                                    strSQL = strSQL + " update House_B_UnitPrice_Interval  set"
                                    strSQL = strSQL + "   UnitPriceCode     = @dm,"
                                    strSQL = strSQL + "   IntervalStart = @intervalstart,"
                                    strSQL = strSQL + "   IntervalEnd = @IntervalEnd"
                                    strSQL = strSQL + " where UnitPriceCode = @olddm"

                                Case "totalprice"
                                    strDM = objNewData(Xydc.Platform.Common.Data.DeepData.FIELD_House_B_TotalPrice_Code).Trim()
                                    With New Xydc.Platform.Common.Utilities.PulicParameters
                                        strOldDM = .getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.DeepData.FIELD_House_B_TotalPrice_Code), "")
                                    End With
                                    strSQL = strSQL + " update House_B_TotalPrice_Interval  set"
                                    strSQL = strSQL + "   TotalPriceCode     = @dm,"
                                    strSQL = strSQL + "   IntervalStart = @intervalstart,"
                                    strSQL = strSQL + "   IntervalEnd = @IntervalEnd"
                                    strSQL = strSQL + " where TotalPriceCode = @olddm"
                                Case Else

                            End Select

                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@dm", strDM)
                            objSqlCommand.Parameters.AddWithValue("@intervalstart", CType(strIntervalStart, System.Double))
                            objSqlCommand.Parameters.AddWithValue("@IntervalEnd", CType(strIntervalEnd, System.Double))
                            objSqlCommand.Parameters.AddWithValue("@olddm", strOldDM)
                    End Select

                    '执行SQL
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '提交事务
                objSqlTransaction.Commit()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doSaveDeepdataInterVal = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function
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

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            doDeleteDeepdataInterVal = False
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""

                '检查
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If objOldData Is Nothing Then
                    strErrMsg = "错误：未传入旧的数据！"
                    GoTo errProc
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '开始事务
                Try
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '删除数据
                Dim strSQL As String
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '计算SQL
                    Dim strOldDM As String
                    strSQL = ""
                    Select Case strType
                        Case "buildingarea"
                            With New Xydc.Platform.Common.Utilities.PulicParameters
                                strOldDM = .getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.DeepData.FIELD_House_B_BuildingArea_Code), "")
                            End With
                            strSQL = strSQL + " delete from House_B_BuildingArea_Interval"
                            strSQL = strSQL + " where BuildingAreaCode = @olddm"
                        Case "floorarea"
                            With New Xydc.Platform.Common.Utilities.PulicParameters
                                strOldDM = .getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.DeepData.FIELD_House_B_FloorArea_Code), "")
                            End With
                            strSQL = strSQL + " delete from House_B_FloorArea_Interval "
                            strSQL = strSQL + " where FloorAreaCode = @olddm"
                        Case "unitprice"
                            With New Xydc.Platform.Common.Utilities.PulicParameters
                                strOldDM = .getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.DeepData.FIELD_House_B_UnitPrice_Code), "")
                            End With
                            strSQL = strSQL + " delete from House_B_UnitPrice_Interval "
                            strSQL = strSQL + " where UnitPriceCode = @olddm"

                        Case "totalprice"
                            With New Xydc.Platform.Common.Utilities.PulicParameters
                                strOldDM = .getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.DeepData.FIELD_House_B_TotalPrice_Code), "")
                            End With
                            strSQL = strSQL + " delete from House_B_TotalPrice_Interval "
                            strSQL = strSQL + " where TotalPriceCode = @olddm"
                        Case Else

                    End Select

                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@olddm", strOldDM)

                    '执行SQL
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '提交事务
                objSqlTransaction.Commit()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doDeleteDeepdataInterVal = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

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

            Dim objTempDeepdata As Xydc.Platform.Common.Data.DeepData = Nothing
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Nothing
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand = Nothing
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim strSQL As String = ""

            '初始化
            getDataSet_Detail_Customer = False
            objDeepdata = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strWhere.Length > 0 Then strWhere = strWhere.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：[明细深度数据]未指定[连接用户]！"
                    GoTo errProc
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                Try
                    '创建数据集
                    objTempDeepdata = New Xydc.Platform.Common.Data.DeepData(Xydc.Platform.Common.Data.DeepData.enumTableType.House_B_SalesMessageCustomer)


                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL

                        strSQL = ""
                        strSQL = strSQL + " select a.* from House_B_SalesMessage_Customer a " + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere
                        End If
                        strSQL = strSQL + " order by a.FixtureDate,a.region"

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempDeepdata.Tables(Xydc.Platform.Common.Data.DeepData.TABLE_House_B_SalesMessageCustomer))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDeepdata.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.DeepData.SafeRelease(objTempDeepdata)
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objDeepdata = objTempDeepdata
            getDataSet_Detail_Customer = True
            Exit Function
errProc:
            Xydc.Platform.Common.Data.DeepData.SafeRelease(objTempDeepdata)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function
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

            Dim objTempDeepdata As Xydc.Platform.Common.Data.DeepData = Nothing
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Nothing
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand = Nothing
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim strSQL As String = ""

            '初始化
            getDataSet_AgeRatio = False
            objDeepdata = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strWhere.Length > 0 Then strWhere = strWhere.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：[明细深度数据]未指定[连接用户]！"
                    GoTo errProc
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                Try
                    '创建数据集
                    objTempDeepdata = New Xydc.Platform.Common.Data.DeepData(Xydc.Platform.Common.Data.DeepData.enumTableType.Customer_V_Age_Ratio)


                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL

                        strSQL = ""
                        strSQL = strSQL + " exec dbo.Customer_P_getEveryAge  @strWhere " + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@strWhere", strWhere)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempDeepdata.Tables(Xydc.Platform.Common.Data.DeepData.TABLE_Customer_V_AgeRatio))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDeepdata.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.DeepData.SafeRelease(objTempDeepdata)
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objDeepdata = objTempDeepdata
            getDataSet_AgeRatio = True
            Exit Function
errProc:
            Xydc.Platform.Common.Data.DeepData.SafeRelease(objTempDeepdata)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function
        End Function


        '----------------------------------------------------------------
        ' 根据查询内容从楼盘匹配表，广州街道-区域表，销售楼盘表，已匹配过的字段等获取匹配的数据集
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strUserId                  ：用户标识
        '     strPassword                ：用户密码
        '     strContent                 ：搜索字符串       
        '     objDeepdata                ：信息数据集
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_SearchContent( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strContent As String, _
            ByRef objDeepdata As Xydc.Platform.Common.Data.DeepData) As Boolean

            Dim objTempDeepdata As Xydc.Platform.Common.Data.DeepData = Nothing
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Nothing
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand = Nothing
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim strSQL As String = ""

            '初始化
            getDataSet_SearchContent = False
            objDeepdata = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strContent.Length > 0 Then strContent = strContent.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：[明细深度数据]未指定[连接用户]！"
                    GoTo errProc
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                Try
                    '创建数据集
                    objTempDeepdata = New Xydc.Platform.Common.Data.DeepData(Xydc.Platform.Common.Data.DeepData.enumTableType.Customer_B_Search_Gather)


                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL


                        strSQL = ""
                        If strContent <> "" Then
                            strSQL = strSQL + " select '" + strContent + "' as C_SearchContent,C_SourceContent,C_Region,C_SourceTable from MailAddress_V_Region " + vbCr
                            strSQL = strSQL + " where charindex('" + strContent + "',C_SourceContent)>0  "
                        Else
                            strSQL = strSQL + " SELECT *  FROM xszxDB.dbo.Customer_B_Search_Gather where 1=1"
                        End If

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempDeepdata.Tables(Xydc.Platform.Common.Data.DeepData.TABLE_Customer_B_Search_Gather))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDeepdata.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.DeepData.SafeRelease(objTempDeepdata)
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objDeepdata = objTempDeepdata
            getDataSet_SearchContent = True
            Exit Function
errProc:
            Xydc.Platform.Common.Data.DeepData.SafeRelease(objTempDeepdata)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function
        End Function

    End Class

End Namespace