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
Imports System.Runtime.Serialization

Namespace Xydc.Platform.Common.Data

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.Common.Data
    ' 类名    ：sunshineData
    '
    ' 功能描述：
    '     定义“阳光家缘的数据”有关的数据访问格式
    '----------------------------------------------------------------
    <System.ComponentModel.DesignerCategory("SUNSHINE"), SerializableAttribute()> Public Class SunshineData
        Inherits System.Data.DataSet

        '“房地产项目信息楼盘匹配表”表信息定义
        '表名称
        '-2012-10-25 
        Public Const TABLE_Sunshine_B_HOUSE_MATCH As String = "T_HOUSE_MATCH"
        '字段序列
        Public Const FIELD_Sunshine_B_HOUSE_MATCH_ID As String = "C_ID"
        Public Const FIELD_Sunshine_B_HOUSE_MATCH_XZQY As String = "C_XZQY"
        Public Const FIELD_Sunshine_B_HOUSE_MATCH_HOUSE As String = "C_HOUSE"
        Public Const FIELD_Sunshine_B_HOUSE_MATCH_XM_NAME As String = "C_XM_NAME"

        '“房地产项目信息楼盘匹配表X2”表信息定义
        '表名称
        Public Const TABLE_Sunshine_B_HOUSE_MATCH_XMID As String = "T_HOUSE_MATCH_XM_ID"
        Public Const FIELD_Sunshine_B_HOUSE_MATCH_XM_ID As String = "C_XM_ID"
        Public Const FIELD_Sunshine_B_HOUSE_MATCH_XM_ADDRESS As String = "C_XM_ADDRESS"
        Public Const FIELD_Sunshine_B_HOUSE_MATCH_XM_TYPE As String = "C_TYPE"
        'Public Const FIELD_Sunshine_B_HOUSE_MATCH_XM_TIME As String = "C_TIME"


        '“房地产项目信息楼盘匹配表X2”表信息定义
        '表名称
        Public Const TABLE_Sunshine_B_HOUSE_MATCH_PRICE As String = "T_HOUSE_MATCH_PRICE"

        '2013-02-28
        '"房地产楼盘均价"表信息定义
        Public Const TABLE_Sunshine_V_House_Average_Price As String = "Sunshine_V_House_Average_Price"
        Public Const FIELD_Sunshine_V_house_AveragePrice As String = "均价"

        '显示字段
        Public Const FIELD_Sunshine_B_HOUSE_MATCH_XM_TYPENAME As String = "TYPENAME"

        '“房地产项目信息统计表”表信息定义
        '表名称
        Public Const TABLE_Sunshine_V_Houseinfo_Statistics As String = "Sunshine_V_Houseinfo_Statistics"
        '字段序列
        Public Const FIELD_Sunshine_V_Houseinfo_Statistics_StartTime As String = "开始日期"
        Public Const FIELD_Sunshine_V_Houseinfo_Statistics_EndTime As String = "结束日期"
        Public Const FIELD_Sunshine_V_Houseinfo_Statistics_Region As String = "行政区域"
        Public Const FIELD_Sunshine_V_Houseinfo_Statistics_BuildingName As String = "楼盘名称"

        Public Const FIELD_Sunshine_V_Houseinfo_Statistics_Project As String = "项目名称"
        Public Const FIELD_Sunshine_V_Houseinfo_Statistics_Presellid As String = "预售证"

        Public Const FIELD_Sunshine_V_Houseinfo_Statistics_BuildingType As String = "项目类型"
        Public Const FIELD_Sunshine_V_Houseinfo_Statistics_NetAutograph As String = "网签数"
        Public Const FIELD_Sunshine_V_Houseinfo_Statistics_NetAutographPrice As String = "网签均价"
        Public Const FIELD_Sunshine_V_Houseinfo_Statistics_TotalTurnover As String = "合共成交"
        Public Const FIELD_Sunshine_V_Houseinfo_Statistics_NetAutographSumPrice As String = "网签总额"
        Public Const FIELD_Sunshine_V_Houseinfo_Statistics_NetAutographArea As String = "网签面积"

        Public Const FIELD_Sunshine_V_Houseinfo_Statistics_FrontNetAutograph As String = "上周网签数"
        Public Const FIELD_Sunshine_V_Houseinfo_Statistics_FrontNetAutographPrice As String = "上周网签均价"
        Public Const FIELD_Sunshine_V_Houseinfo_Statistics_Time As String = "日期"
        Public Const FIELD_Sunshine_V_Houseinfo_Statistics_TotalAutographPrice As String = "合共均价"

        '“房地产项目周明细信息统计表”表信息定义
        '表名称
        Public Const TABLE_Sunshine_V_WeekInfo_Statistics As String = "Sunshine_V_WeekInfo_Statistics"


        '“房地产项目周区域信息统计表”表信息定义
        '表名称
        Public Const TABLE_Sunshine_V_WeekRegion_Statistics As String = "Sunshine_V_WeekRegion_Statistics"
        Public Const FIELD_Sunshine_V_WeekRegion_Statistics_NetAutographChain As String = "网签数环比"
        Public Const FIELD_Sunshine_V_WeekRegion_Statistics_NetAutographPriceChain As String = "网签均价环比"

        '“房地产项目分区域信息统计表”表信息定义
        '表名称
        Public Const TABLE_Sunshine_V_Region_Statistics As String = "Sunshine_V_Region_Statistics"
        Public Const FIELD_Sunshine_V_Region_Statistics_CenterSixRegion As String = "中心六区"
        Public Const FIELD_Sunshine_V_Region_Statistics_TenRegion As String = "十区"
        Public Const FIELD_Sunshine_V_Region_Statistics_TenRegionTwoCity As String = "十区两市"

        '“房地产项目周楼盘匹配表”表信息定义
        '表名称
        Public Const TABLE_Sunshine_B_WeekMonitoringHouse As String = "Sunshine_B_WeekMonitoringHouse"
        Public Const FIELD_Sunshine_B_WeekMonitoringHouse_ID As String = "MonitoringID"
        Public Const FIELD_Sunshine_B_WeekMonitoringHouse_NAME As String = "BuildingName"
        Public Const FIELD_Sunshine_B_WeekMonitoringHouse_Region As String = "Region"
        Public Const FIELD_Sunshine_B_WeekMonitoringHouse_Type As String = "Type"
        Public Const FIELD_Sunshine_B_WeekMonitoringHouse_SellingHouse As String = "SellingHouse"

        '“房地产项目月楼盘匹配表”表信息定义
        '表名称
        Public Const TABLE_Sunshine_B_MonthMonitoringHouse As String = "Sunshine_B_MonthMonitoringHouse"

        '“房地产项目N时间段的统计信息”表信息定义
        '表名称
        Public Const TABLE_Sunshine_V_NWeek_Statistics As String = "Sunshine_V_NWeek_Statistics"
        Public Const TABLE_Sunshine_V_NWeek_Statistics_IntervalDate As String = "日期段"
        Public Const TABLE_Sunshine_V_NWeek_Statistics_SixRegionNetAutograph As String = "六区网签数"
        Public Const TABLE_Sunshine_V_NWeek_Statistics_SixRegionPrice As String = "六区网签均价"
        Public Const TABLE_Sunshine_V_NWeek_Statistics_TenRegionNetAutograph As String = "十区网签数"
        Public Const TABLE_Sunshine_V_NWeek_Statistics_TenRegionPrice As String = "十区网签均价"
        Public Const TABLE_Sunshine_V_NWeek_Statistics_AllNetAutograph As String = "十区两市网签数"
        Public Const TABLE_Sunshine_V_NWeek_Statistics_AllPrice As String = "十区两市网签均价"
        Public Const TABLE_Sunshine_V_NWeek_Statistics_SerialNumber As String = "序号"


        '“房地产项目楼盘排序”表信息定义
        '表名称
        Public Const TABLE_Sunshine_B_HOUSEMATCHSORT As String = "T_HOUSE_MATCH_SORT"

        '“房地产项目N时间段的统计信息”表信息定义
        '表名称
        Public Const FIELD_Sunshine_B_HOUSEMATCHSORT_ID As String = "I_ID"
        Public Const FIELD_Sunshine_B_HOUSEMATCHSORT_NAME As String = "C_NAME"
        Public Const FIELD_Sunshine_B_HOUSEMATCHSORT_Sort As String = "I_Sort"
        Public Const FIELD_Sunshine_B_HOUSEMATCHSORT_Type As String = "I_Type"
        '显示字段
        Public Const FIELD_Sunshine_V_HOUSEMATCHSORT_Type As String = "S_Type"

    
        '定义初始化表类型enum
        Public Enum enumTableType
            Sunshine_B_HOUSE_MATCH = 1
            Sunshine_V_Houseinfo_Statistics = 2
            Sunshine_V_WeekInfo_Statistics = 3
            Sunshine_V_WeekRegion_Statistics = 4
            Sunshine_V_Region_Statistics = 5
            Sunshine_B_WeekMonitoringHouse = 6
            Sunshine_B_MonthMonitoringHouse = 7
            Sunshine_B_HOUSE_MATCH_XMID = 8
            Sunshine_V_NWeek_Statistics = 9
            Sunshine_B_HOUSEMATCHSORT = 10
            Sunshine_V_House_Average_Price = 11
            Sunshine_B_HOUSE_MATCH_PRICE = 12

        End Enum

        ' 构造函数
        '----------------------------------------------------------------
        Private Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
            MyBase.New(info, context)
        End Sub

        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
        End Sub

        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New(ByVal objenumTableType As enumTableType)
            MyBase.New()
            Try
                Dim objDataTable As System.Data.DataTable
                Dim strErrMsg As String
                objDataTable = Me.createDataTables(strErrMsg, objenumTableType)
                If Not (objDataTable Is Nothing) Then
                    Me.Tables.Add(objDataTable)
                End If
            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' 安全释放本身资源
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.Common.Data.SunshineData)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub


        '----------------------------------------------------------------
        '将给定DataTable加入到DataSet中
        '----------------------------------------------------------------
        Public Function appendDataTable(ByVal table As System.Data.DataTable) As String

            Dim strErrMsg As String = ""

            Try
                Me.Tables.Add(table)
            Catch ex As Exception
                strErrMsg = ex.Message
            End Try

            appendDataTable = strErrMsg

        End Function

        '----------------------------------------------------------------
        '根据指定类型创建dataTable
        '----------------------------------------------------------------
        Public Function createDataTables( _
            ByRef strErrMsg As String, _
            ByVal enumType As enumTableType) As System.Data.DataTable

            Dim table As System.Data.DataTable


            Select Case enumType
                Case enumTableType.Sunshine_B_HOUSE_MATCH
                    table = createDataTables_HOUSE_MATCH(strErrMsg)

                Case enumTableType.Sunshine_B_HOUSE_MATCH_XMID
                    table = createDataTables_HOUSE_MATCH_XMID(strErrMsg)

                Case enumTableType.Sunshine_V_Houseinfo_Statistics
                    table = createDataTables_Sunshine_V_Houseinfo_Statistics(strErrMsg)

                Case enumTableType.Sunshine_V_WeekInfo_Statistics
                    table = createDataTables_Sunshine_V_WeekInfo_Statistics(strErrMsg)

                Case enumTableType.Sunshine_V_WeekRegion_Statistics
                    table = createDataTables_Sunshine_V_WeekRegion_Statistics(strErrMsg)

                Case enumTableType.Sunshine_V_Region_Statistics
                    table = createDataTables_Sunshine_V_Region_Statistics(strErrMsg)

                Case enumTableType.Sunshine_B_WeekMonitoringHouse
                    table = createDataTables_Sunshine_B_WeekMonitoringHouse(strErrMsg)

                Case enumTableType.Sunshine_B_MonthMonitoringHouse
                    table = createDataTables_Sunshine_B_MonthMonitoringHouse(strErrMsg)

                Case enumTableType.Sunshine_V_NWeek_Statistics
                    table = createDataTables_Sunshine_V_NWeek_Statistics(strErrMsg)

                Case enumTableType.Sunshine_B_HOUSEMATCHSORT
                    table = createDataTables_Sunshine_B_HOUSEMATCHSORT(strErrMsg)

                Case enumTableType.Sunshine_V_House_Average_Price
                    table = createDataTables_Sunshine_V_House_Average_Price(strErrMsg)

                Case enumTableType.Sunshine_B_HOUSE_MATCH_PRICE
                    table = createDataTables_HOUSE_MATCH_PRICE(strErrMsg)

                Case Else
                    strErrMsg = "无效的表类型！"
                    table = Nothing
            End Select
          
            createDataTables = table
        End Function


        '----------------------------------------------------------------
        '创建TABLE_Sunshine_B_HOUSEMATCHSORT
        '----------------------------------------------------------------
        Private Function createDataTables_Sunshine_B_HOUSEMATCHSORT(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_Sunshine_B_HOUSEMATCHSORT)
                With table.Columns

                    .Add(FIELD_Sunshine_B_HOUSEMATCHSORT_ID, GetType(System.Int32))
                    .Add(FIELD_Sunshine_B_HOUSEMATCHSORT_NAME, GetType(System.String))
                    .Add(FIELD_Sunshine_B_HOUSEMATCHSORT_Sort, GetType(System.Int32))
                    .Add(FIELD_Sunshine_B_HOUSEMATCHSORT_Type, GetType(System.Int32))
                    .Add(FIELD_Sunshine_V_HOUSEMATCHSORT_Type, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Sunshine_B_HOUSEMATCHSORT = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_Sunshine_V_NWeek_Statistics
        '----------------------------------------------------------------
        Private Function createDataTables_Sunshine_V_NWeek_Statistics(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_Sunshine_V_NWeek_Statistics)
                With table.Columns

                    .Add(TABLE_Sunshine_V_NWeek_Statistics_IntervalDate, GetType(System.String))
                    .Add(TABLE_Sunshine_V_NWeek_Statistics_SixRegionNetAutograph, GetType(System.Double))
                    .Add(TABLE_Sunshine_V_NWeek_Statistics_SixRegionPrice, GetType(System.Double))
                    .Add(TABLE_Sunshine_V_NWeek_Statistics_TenRegionNetAutograph, GetType(System.Double))
                    .Add(TABLE_Sunshine_V_NWeek_Statistics_TenRegionPrice, GetType(System.Double))
                    .Add(TABLE_Sunshine_V_NWeek_Statistics_AllNetAutograph, GetType(System.Double))
                    .Add(TABLE_Sunshine_V_NWeek_Statistics_AllPrice, GetType(System.Double))
                    .Add(TABLE_Sunshine_V_NWeek_Statistics_SerialNumber, GetType(System.Int32))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Sunshine_V_NWeek_Statistics = table

        End Function


        '----------------------------------------------------------------
        '创建TABLE_Sunshine_B_MonthMonitoringHouse
        '----------------------------------------------------------------
        Private Function createDataTables_Sunshine_B_MonthMonitoringHouse(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_Sunshine_B_MonthMonitoringHouse)
                With table.Columns

                    .Add(FIELD_Sunshine_B_WeekMonitoringHouse_ID, GetType(System.Int32))
                    .Add(FIELD_Sunshine_B_WeekMonitoringHouse_NAME, GetType(System.String))
                    .Add(FIELD_Sunshine_B_WeekMonitoringHouse_Region, GetType(System.String))
                    .Add(FIELD_Sunshine_B_WeekMonitoringHouse_Type, GetType(System.String))

                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Sunshine_B_MonthMonitoringHouse = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_Sunshine_B_WeekMonitoringHouse
        '----------------------------------------------------------------
        Private Function createDataTables_Sunshine_B_WeekMonitoringHouse(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_Sunshine_B_WeekMonitoringHouse)
                With table.Columns

                    .Add(FIELD_Sunshine_B_WeekMonitoringHouse_ID, GetType(System.Int32))
                    .Add(FIELD_Sunshine_B_WeekMonitoringHouse_NAME, GetType(System.String))
                    .Add(FIELD_Sunshine_B_WeekMonitoringHouse_Region, GetType(System.String))
                    .Add(FIELD_Sunshine_B_WeekMonitoringHouse_Type, GetType(System.String))
                    .Add(FIELD_Sunshine_B_WeekMonitoringHouse_SellingHouse, GetType(System.String))

                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Sunshine_B_WeekMonitoringHouse = table

        End Function


        '----------------------------------------------------------------
        '创建TABLE_Sunshine_V_WeekRegion_Statistics
        '----------------------------------------------------------------
        Private Function createDataTables_Sunshine_V_Region_Statistics(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_Sunshine_V_WeekRegion_Statistics)
                With table.Columns

                    .Add(FIELD_Sunshine_V_Houseinfo_Statistics_Time, GetType(System.String))
                    .Add(FIELD_Sunshine_V_Region_Statistics_CenterSixRegion, GetType(System.String))
                    .Add(FIELD_Sunshine_V_Region_Statistics_TenRegion, GetType(System.String))
                    .Add(FIELD_Sunshine_V_Region_Statistics_TenRegionTwoCity, GetType(System.String))

                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Sunshine_V_Region_Statistics = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_Sunshine_V_WeekRegion_Statistics
        '----------------------------------------------------------------
        Private Function createDataTables_Sunshine_V_WeekRegion_Statistics(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_Sunshine_V_WeekRegion_Statistics)
                With table.Columns

                    .Add(FIELD_Sunshine_V_Houseinfo_Statistics_Time, GetType(System.String))
                    .Add(FIELD_Sunshine_V_Houseinfo_Statistics_Region, GetType(System.String))
                    .Add(FIELD_Sunshine_V_Houseinfo_Statistics_FrontNetAutograph, GetType(System.Double))
                    .Add(FIELD_Sunshine_V_Houseinfo_Statistics_NetAutograph, GetType(System.Double))
                    .Add(FIELD_Sunshine_V_WeekRegion_Statistics_NetAutographChain, GetType(System.Double))
                    .Add(FIELD_Sunshine_V_Houseinfo_Statistics_NetAutographPrice, GetType(System.Double))
                    .Add(FIELD_Sunshine_V_Houseinfo_Statistics_FrontNetAutographPrice, GetType(System.Double))
                    .Add(FIELD_Sunshine_V_WeekRegion_Statistics_NetAutographPriceChain, GetType(System.Double))
                    .Add(FIELD_Sunshine_V_Houseinfo_Statistics_NetAutographSumPrice, GetType(System.Double))
                    .Add(FIELD_Sunshine_V_Houseinfo_Statistics_NetAutographArea, GetType(System.Double))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Sunshine_V_WeekRegion_Statistics = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_Sunshine_V_WeekInfo_Statistics
        '----------------------------------------------------------------
        Private Function createDataTables_Sunshine_V_WeekInfo_Statistics(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_Sunshine_V_WeekInfo_Statistics)
                With table.Columns

                    .Add(FIELD_Sunshine_V_Houseinfo_Statistics_StartTime, GetType(System.String))
                    .Add(FIELD_Sunshine_V_Houseinfo_Statistics_EndTime, GetType(System.String))

                    .Add(FIELD_Sunshine_V_Houseinfo_Statistics_Region, GetType(System.String))
                    .Add(FIELD_Sunshine_V_Houseinfo_Statistics_BuildingName, GetType(System.String))
                    .Add(FIELD_Sunshine_V_Houseinfo_Statistics_BuildingType, GetType(System.String))
                    .Add(FIELD_Sunshine_V_Houseinfo_Statistics_NetAutograph, GetType(System.Double))

                    .Add(FIELD_Sunshine_V_Houseinfo_Statistics_NetAutographPrice, GetType(System.Double))
                    .Add(FIELD_Sunshine_V_Houseinfo_Statistics_TotalTurnover, GetType(System.Double))
                    .Add(FIELD_Sunshine_V_Houseinfo_Statistics_NetAutographSumPrice, GetType(System.Double))
                    .Add(FIELD_Sunshine_V_Houseinfo_Statistics_NetAutographArea, GetType(System.Double))
                    .Add(FIELD_Sunshine_V_Houseinfo_Statistics_TotalAutographPrice, GetType(System.Double))

                    .Add(FIELD_Sunshine_V_Houseinfo_Statistics_FrontNetAutograph, GetType(System.Double))

                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Sunshine_V_WeekInfo_Statistics = table

        End Function


        '----------------------------------------------------------------
        '创建TABLE_Sunshine_V_Houseinfo_Statistics
        '----------------------------------------------------------------
        Private Function createDataTables_Sunshine_V_Houseinfo_Statistics(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_Sunshine_V_Houseinfo_Statistics)
                With table.Columns

                    .Add(FIELD_Sunshine_V_Houseinfo_Statistics_StartTime, GetType(System.String))
                    .Add(FIELD_Sunshine_V_Houseinfo_Statistics_EndTime, GetType(System.String))

                    .Add(FIELD_Sunshine_V_Houseinfo_Statistics_Region, GetType(System.String))
                    .Add(FIELD_Sunshine_V_Houseinfo_Statistics_BuildingName, GetType(System.String))

                    .Add(FIELD_Sunshine_V_Houseinfo_Statistics_Project, GetType(System.String))
                    .Add(FIELD_Sunshine_V_Houseinfo_Statistics_Presellid, GetType(System.String))

                    .Add(FIELD_Sunshine_V_Houseinfo_Statistics_BuildingType, GetType(System.String))
                    .Add(FIELD_Sunshine_V_Houseinfo_Statistics_NetAutograph, GetType(System.Double))

                    .Add(FIELD_Sunshine_V_Houseinfo_Statistics_NetAutographPrice, GetType(System.Double))


                    .Add(FIELD_Sunshine_V_Houseinfo_Statistics_TotalTurnover, GetType(System.Double))
                    .Add(FIELD_Sunshine_V_Houseinfo_Statistics_NetAutographSumPrice, GetType(System.Double))
                    .Add(FIELD_Sunshine_V_Houseinfo_Statistics_NetAutographArea, GetType(System.Double))

                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Sunshine_V_Houseinfo_Statistics = table

        End Function


        '----------------------------------------------------------------
        '创建TABLE_Sunshine_B_HOUSE_MATCH
        '----------------------------------------------------------------
        Private Function createDataTables_HOUSE_MATCH(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_Sunshine_B_HOUSE_MATCH)
                With table.Columns
                    .Add(FIELD_Sunshine_B_HOUSE_MATCH_ID, GetType(System.Int32))
                    .Add(FIELD_Sunshine_B_HOUSE_MATCH_XZQY, GetType(System.String))
                    .Add(FIELD_Sunshine_B_HOUSE_MATCH_HOUSE, GetType(System.String))
                    .Add(FIELD_Sunshine_B_HOUSE_MATCH_XM_NAME, GetType(System.String))

                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_HOUSE_MATCH = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_Sunshine_B_HOUSE_MATCH_XMID
        '----------------------------------------------------------------
        Private Function createDataTables_HOUSE_MATCH_XMID(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_Sunshine_B_HOUSE_MATCH_XMID)
                With table.Columns
                    .Add(FIELD_Sunshine_B_HOUSE_MATCH_ID, GetType(System.Int32))
                    .Add(FIELD_Sunshine_B_HOUSE_MATCH_XZQY, GetType(System.String))
                    .Add(FIELD_Sunshine_B_HOUSE_MATCH_HOUSE, GetType(System.String))
                    .Add(FIELD_Sunshine_B_HOUSE_MATCH_XM_NAME, GetType(System.String))

                    .Add(FIELD_Sunshine_B_HOUSE_MATCH_XM_ID, GetType(System.String))
                    .Add(FIELD_Sunshine_B_HOUSE_MATCH_XM_ADDRESS, GetType(System.String))
                    .Add(FIELD_Sunshine_B_HOUSE_MATCH_XM_TYPE, GetType(System.String))
                    '.Add(FIELD_Sunshine_B_HOUSE_MATCH_XM_TIME, GetType(System.String))

                    '显示字段
                    .Add(FIELD_Sunshine_B_HOUSE_MATCH_XM_TYPENAME, GetType(System.String))


                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_HOUSE_MATCH_XMID = table

        End Function


        '----------------------------------------------------------------
        '创建TABLE_Sunshine_B_HOUSE_MATCH_PRICE
        '----------------------------------------------------------------
        Private Function createDataTables_HOUSE_MATCH_PRICE(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_Sunshine_B_HOUSE_MATCH_PRICE)
                With table.Columns
                    .Add(FIELD_Sunshine_B_HOUSE_MATCH_ID, GetType(System.Int32))
                    .Add(FIELD_Sunshine_B_HOUSE_MATCH_XZQY, GetType(System.String))
                    .Add(FIELD_Sunshine_B_HOUSE_MATCH_HOUSE, GetType(System.String))
                    .Add(FIELD_Sunshine_B_HOUSE_MATCH_XM_NAME, GetType(System.String))

                    .Add(FIELD_Sunshine_B_HOUSE_MATCH_XM_ID, GetType(System.String))
                    .Add(FIELD_Sunshine_B_HOUSE_MATCH_XM_ADDRESS, GetType(System.String))
                    .Add(FIELD_Sunshine_B_HOUSE_MATCH_XM_TYPE, GetType(System.String))

                    '显示字段
                    .Add(FIELD_Sunshine_B_HOUSE_MATCH_XM_TYPENAME, GetType(System.String))

                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_HOUSE_MATCH_PRICE = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_Sunshine_V_House_Average_Price
        '----------------------------------------------------------------
        Private Function createDataTables_Sunshine_V_House_Average_Price(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_Sunshine_V_House_Average_Price)
                With table.Columns
                    .Add(FIELD_Sunshine_V_house_AveragePrice, GetType(System.Double))

                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Sunshine_V_House_Average_Price = table

        End Function

    End Class
End Namespace

