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
    ' 类名    ：DeepData
    '
    ' 功能描述：
    '     定义“深度数据”有关的数据访问格式
    '----------------------------------------------------------------
    <System.ComponentModel.DesignerCategory("DEEP"), SerializableAttribute()> Public Class DeepData
        Inherits System.Data.DataSet

        '“楼盘_B_销售信息”表信息定义
        '表名称
        Public Const TABLE_House_B_SalesMessage As String = "House_B_SalesMessage"
        '字段序列
        Public Const FIELD_House_B_SalesMessage_ID As String = "SalesMessageID"
        Public Const FIELD_House_B_SalesMessage_MainHous As String = "MainHouse"
        Public Const FIELD_House_B_SalesMessage_PartialHouse As String = "PartialHouse"

        Public Const FIELD_House_B_SalesMessage_Region As String = "Region"
        Public Const FIELD_House_B_SalesMessage_HouseAddress As String = "HouseAddress"
        Public Const FIELD_House_B_SalesMessage_RoomNumber As String = "RoomNumber"
        Public Const FIELD_House_B_SalesMessage_HouseType As String = "HouseType"
        Public Const FIELD_House_B_SalesMessage_Floor As String = "Floor"

        Public Const FIELD_House_B_SalesMessage_RoomTypeCalc As String = "RoomTypeCalc"
        Public Const FIELD_House_B_SalesMessage_TotalFloor As String = "TotalFloor"
        Public Const FIELD_House_B_SalesMessage_EnclosedPatio As String = "EnclosedPatio"
        Public Const FIELD_House_B_SalesMessage_NotEnclosedPatio As String = "NotEnclosedPatio"
        Public Const FIELD_House_B_SalesMessage_Washroom As String = "Washroom"

        Public Const FIELD_House_B_SalesMessage_BuildingArea As String = "BuildingArea"
        Public Const FIELD_House_B_SalesMessage_FloorArea As String = "FloorArea"
        Public Const FIELD_House_B_SalesMessage_UnitPrice As String = "UnitPrice"
        Public Const FIELD_House_B_SalesMessage_TotalPrice As String = "TotalPrice"
        Public Const FIELD_House_B_SalesMessage_FixtureDate As String = "FixtureDate"

        Public Const FIELD_House_B_SalesMessage_ImportUser As String = "ImportUser"
        Public Const FIELD_House_B_SalesMessage_ImportDate As String = "ImportDate"
        Public Const FIELD_House_B_SalesMessage_HouseTypeCalc As String = "HouseTypeCalc"
        Public Const FIELD_House_B_SalesMessage_Adjustment As String = "Adjustment"
        Public Const FIELD_House_B_SalesMessage_HouseTypeGroup As String = "HouseTypeGroup"

        '约束错误信息

     

        '“楼盘_B_套内面积段”表信息定义
        '表名称
        Public Const TABLE_House_B_FloorArea_Interval As String = "House_B_FloorArea_Interval"
        '字段序列
        Public Const FIELD_House_B_FloorArea_Code As String = "FloorAreaCode"
        Public Const FIELD_House_B_IntervalStart As String = "IntervalStart"
        Public Const FIELD_House_B_IntervalEnd As String = "IntervalEnd"

        '“楼盘_B_建筑面积段”表信息定义
        '表名称
        Public Const TABLE_House_B_BuildingArea_Interval As String = "House_B_BuildingArea_Interval"
        '字段序列
        Public Const FIELD_House_B_BuildingArea_Code As String = "BuildingAreaCode"

        '“楼盘_B_总价段”表信息定义
        '表名称
        Public Const TABLE_House_B_TotalPrice_Interval As String = "House_B_TotalPrice_Interval"
        '字段序列
        Public Const FIELD_House_B_TotalPrice_Code As String = "TotalPriceCode"

        '“楼盘_B_单价段”表信息定义
        '表名称
        Public Const TABLE_House_B_UnitPrice_Interval As String = "House_B_UnitPrice_Interval"
        '字段序列
        Public Const FIELD_House_B_UnitPrice_Code As String = "UnitPriceCode"

        '“楼盘_B_数据统计”表信息定义
        '表名称
        Public Const TABLE_House_V_Data_Statistics As String = "House_V_Data_Statistics"
        '字段序列
        Public Const FIELD_House_V_Data_Statistics_MonthName As String = "月份"
        Public Const FIELD_House_V_Data_Statistics_BuildingArea As String = "建筑面积"
        Public Const FIELD_House_V_Data_Statistics_BuildingAreaChain As String = "建筑面积环比"
        Public Const FIELD_House_V_Data_Statistics_FloorArea As String = "套内面积"
        Public Const FIELD_House_V_Data_Statistics_FloorAreaChain As String = "套内面积环比"
        Public Const FIELD_House_V_Data_Statistics_Volume As String = "成交套数"

        Public Const FIELD_House_V_Data_Statistics_VolumeChain As String = "成交套数环比"
        Public Const FIELD_House_V_Data_Statistics_UnitPrice As String = "成交均价"
        Public Const FIELD_House_V_Data_Statistics_UnitPriceChain As String = "成交均价环比"
        Public Const FIELD_House_V_Data_Statistics_TotalPrice As String = "成交金额"
        Public Const FIELD_House_V_Data_Statistics_TotalPriceChain As String = "成交金额环比"

        Public Const FIELD_House_V_Data_Statistics_Name As String = "统计字段"
        Public Const FIELD_House_V_Data_Statistics_Type As String = "类型"


        '“楼盘_B_数据统计”表信息定义
        '表名称
        Public Const TABLE_House_V_Data_DetailStatistics As String = "House_V_Data_DetailStatistics"
        Public Const FIELD_House_V_SalesMessage_MainHous As String = "项目名称"




        '“楼盘_B_销售_客户信息”表信息定义
        '表名称
        Public Const TABLE_House_B_SalesMessageCustomer As String = "House_B_SalesMessage_Customer"
        '字段序列
        Public Const FIELD_House_B_SalesMessageCustomer_CustomerName As String = "CustomerName"
        Public Const FIELD_House_B_SalesMessageCustomer_Sex As String = "Sex"
        Public Const FIELD_House_B_SalesMessageCustomer_BirthDate As String = "BirthDate"
        Public Const FIELD_House_B_SalesMessageCustomer_CertificateCode As String = "CertificateCode"
        Public Const FIELD_House_B_SalesMessageCustomer_MailAddress As String = "MailAddress"
        Public Const FIELD_House_B_SalesMessageCustomer_MailRegion As String = "MailRegion"
        Public Const FIELD_House_B_SalesMessageCustomer_CodeA As String = "CodeA"
        Public Const FIELD_House_B_SalesMessageCustomer_CodeB As String = "CodeB"
        Public Const FIELD_House_B_SalesMessageCustomer_CodeC As String = "CodeC"
        Public Const FIELD_House_B_SalesMessageCustomer_CodeD As String = "CodeD"
        Public Const FIELD_House_B_SalesMessageCustomer_PhoneNumer As String = "PhoneNumer"
        Public Const FIELD_House_B_SalesMessageCustomer_CountryRegion As String = "CountryRegion"
        Public Const FIELD_House_B_SalesMessageCustomer_StandardBirthDay As String = "StandardBirthDay"
        Public Const FIELD_House_B_SalesMessageCustomer_FixtureAge As String = "FixtureAge"
        Public Const FIELD_House_B_SalesMessageCustomer_CountryProvince As String = "CountryProvince"
        Public Const FIELD_House_B_SalesMessageCustomer_CountryCity As String = "CountryCity"
        Public Const FIELD_House_B_SalesMessageCustomer_CountryArea As String = "CountryArea"


        '“客户_B_年龄比例段”表信息定义
        '表名称
        Public Const TABLE_Customer_B_Age_Interval As String = "Customer_B_Age_Interval"
        '字段序列
        Public Const FIELD_Customer_B_Age_Code As String = "AgeCode"

        '“客户_B_广州区域地址”表信息定义
        '表名称
        Public Const TABLE_Address_B_Region_Guangzhou As String = "Address_B_Region_Guangzhou"
        '字段序列
        Public Const FIELD_Address_B_Region_Guangzhou_Region As String = "Region"
        Public Const FIELD_Address_B_Region_Guangzhou_Postcode As String = "Postcode"
        Public Const FIELD_Address_B_Region_Guangzhou_Address As String = "Address"

        '“客户_B_年龄段比例”表信息定义
        '表名称
        Public Const TABLE_Customer_V_AgeRatio As String = "Customer_V_Age_Ratio"
        '字段序列
        Public Const FIELD_Customer_V_AgeRatio_Interval As String = "年龄段"
        Public Const FIELD_Customer_V_AgeRatio_Ratio As String = "比例"
        Public Const FIELD_Customer_V_AgeRatio_SumAge As String = "人数"
        Public Const FIELD_Customer_V_AgeRatio_TotalAge As String = "总数"

        '“客户通信地址区域匹配查询知识表”表信息定义
        '表名称
        Public Const TABLE_Customer_B_Search_Gather As String = "Customer_B_Search_Gather"
        '字段序列
        Public Const FIELD_Customer_B_Search_Gather_ID As String = "I_GatherID"
        Public Const FIELD_Customer_B_Search_Gather_Region As String = "C_Region"
        Public Const FIELD_Customer_B_Search_Gather_SearchContent As String = "C_SearchContent"
        Public Const FIELD_Customer_B_Search_Gather_SourceTable As String = "C_SourceTable"
        Public Const FIELD_Customer_B_Search_Gather_SourceContent As String = "C_SourceContent"

        '定义初始化表类型enum
        Public Enum enumTableType
            House_B_SalesMessage = 1
            House_B_FloorArea_Interval = 2
            House_B_BuildingArea_Interval = 3
            House_B_TotalPrice_Interval = 4
            House_B_UnitPrice_Interval = 5
            House_V_Data_Statistics = 6
            House_V_Data_DetailStatistics = 7

            House_B_SalesMessageCustomer = 8
            Customer_B_Age_Interval = 9
            Address_B_Region_Guangzhou = 10
            Customer_V_Age_Ratio = 11
            Customer_B_Search_Gather = 12

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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.Common.Data.DeepData)
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
                Case enumTableType.House_B_SalesMessage
                    table = createDataTables_SalesMessage(strErrMsg)

                Case enumTableType.House_B_FloorArea_Interval
                    table = createDataTables_FloorArea_Interval(strErrMsg)

                Case enumTableType.House_B_BuildingArea_Interval
                    table = createDataTables_BuildingArea_Interval(strErrMsg)

                Case enumTableType.House_B_TotalPrice_Interval
                    table = createDataTables_TotalPrice_Interval(strErrMsg)

                Case enumTableType.House_B_UnitPrice_Interval
                    table = createDataTables_UnitPrice_Interval(strErrMsg)

                Case enumTableType.House_V_Data_Statistics
                    table = createDataTables_Data_Statistics(strErrMsg)

                Case enumTableType.House_V_Data_DetailStatistics
                    table = createDataTables_Data_DetailStatistics(strErrMsg)


                Case enumTableType.House_B_SalesMessageCustomer
                    table = createDataTables_Data_SalesMessageCustomer(strErrMsg)
                Case enumTableType.Customer_B_Age_Interval
                    table = createDataTables_Data_Age_Interval(strErrMsg)
                Case enumTableType.Address_B_Region_Guangzhou
                    table = createDataTables_Data_Region_Guangzhou(strErrMsg)
                Case enumTableType.Customer_V_Age_Ratio
                    table = createDataTables_Data_Age_Ratio(strErrMsg)

                Case enumTableType.Customer_B_Search_Gather
                    table = createDataTables_Search_Gather(strErrMsg)
                Case Else
                    strErrMsg = "无效的表类型！"
                    table = Nothing
            End Select



            createDataTables = table

        End Function


        '----------------------------------------------------------------
        '创建TABLE_Customer_B_Search_Gather
        '----------------------------------------------------------------
        Private Function createDataTables_Search_Gather(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_Customer_B_Search_Gather)
                With table.Columns
                    .Add(FIELD_Customer_B_Search_Gather_ID, GetType(System.Int32))
                    .Add(FIELD_Customer_B_Search_Gather_Region, GetType(System.String))
                    .Add(FIELD_Customer_B_Search_Gather_SearchContent, GetType(System.String))
                    .Add(FIELD_Customer_B_Search_Gather_SourceTable, GetType(System.String))
                    .Add(FIELD_Customer_B_Search_Gather_SourceContent, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Search_Gather = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_Customer_V_Age_Ratio
        '----------------------------------------------------------------
        Private Function createDataTables_Data_Age_Ratio(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_Customer_V_AgeRatio)
                With table.Columns
                    .Add(FIELD_Customer_V_AgeRatio_Interval, GetType(System.String))
                    .Add(FIELD_Customer_V_AgeRatio_Ratio, GetType(System.Double))
                    .Add(FIELD_Customer_V_AgeRatio_SumAge, GetType(System.Double))
                    .Add(FIELD_Customer_V_AgeRatio_TotalAge, GetType(System.Double))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Data_Age_Ratio = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_Address_B_Region_Guangzhou
        '----------------------------------------------------------------
        Private Function createDataTables_Data_Region_Guangzhou(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_Address_B_Region_Guangzhou)
                With table.Columns
                    .Add(FIELD_Address_B_Region_Guangzhou_Region, GetType(System.String))
                    .Add(FIELD_Address_B_Region_Guangzhou_Postcode, GetType(System.String))
                    .Add(FIELD_Address_B_Region_Guangzhou_Address, GetType(System.String))

                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Data_Region_Guangzhou = table

        End Function


        '----------------------------------------------------------------
        '创建TABLE_Customer_B_Age_Interval
        '----------------------------------------------------------------
        Private Function createDataTables_Data_Age_Interval(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_Customer_B_Age_Interval)
                With table.Columns
                    .Add(FIELD_Customer_B_Age_Code, GetType(System.String))
                    .Add(FIELD_House_B_IntervalStart, GetType(System.Double))
                    .Add(FIELD_House_B_IntervalEnd, GetType(System.Double))

                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Data_Age_Interval = table

        End Function


        '----------------------------------------------------------------
        '创建TABLE_House_B_SalesMessageCustomer
        '----------------------------------------------------------------
        Private Function createDataTables_Data_SalesMessageCustomer(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_House_B_SalesMessageCustomer)
                With table.Columns
                    .Add(FIELD_House_B_SalesMessage_ID, GetType(System.Int32))
                    .Add(FIELD_House_B_SalesMessage_MainHous, GetType(System.String))
                    .Add(FIELD_House_B_SalesMessage_PartialHouse, GetType(System.String))

                    .Add(FIELD_House_B_SalesMessage_Region, GetType(System.String))
                    .Add(FIELD_House_B_SalesMessage_HouseAddress, GetType(System.String))
                    .Add(FIELD_House_B_SalesMessage_RoomNumber, GetType(System.String))
                    .Add(FIELD_House_B_SalesMessage_HouseType, GetType(System.String))
                    .Add(FIELD_House_B_SalesMessage_Floor, GetType(System.String))

                    .Add(FIELD_House_B_SalesMessage_RoomTypeCalc, GetType(System.String))
                    .Add(FIELD_House_B_SalesMessage_TotalFloor, GetType(System.Int32))
                    .Add(FIELD_House_B_SalesMessage_EnclosedPatio, GetType(System.Int32))
                    .Add(FIELD_House_B_SalesMessage_NotEnclosedPatio, GetType(System.Int32))
                    .Add(FIELD_House_B_SalesMessage_Washroom, GetType(System.Int32))

                    .Add(FIELD_House_B_SalesMessage_BuildingArea, GetType(System.Double))
                    .Add(FIELD_House_B_SalesMessage_FloorArea, GetType(System.Double))
                    .Add(FIELD_House_B_SalesMessage_UnitPrice, GetType(System.Double))
                    .Add(FIELD_House_B_SalesMessage_TotalPrice, GetType(System.Double))
                    .Add(FIELD_House_B_SalesMessage_FixtureDate, GetType(System.DateTime))

                    .Add(FIELD_House_B_SalesMessage_ImportUser, GetType(System.String))
                    .Add(FIELD_House_B_SalesMessage_ImportDate, GetType(System.DateTime))
                    .Add(FIELD_House_B_SalesMessage_HouseTypeCalc, GetType(System.String))
                    .Add(FIELD_House_B_SalesMessage_Adjustment, GetType(System.Int32))
                    .Add(FIELD_House_B_SalesMessage_HouseTypeGroup, GetType(System.String))


                    .Add(FIELD_House_B_SalesMessageCustomer_CustomerName, GetType(System.String))
                    .Add(FIELD_House_B_SalesMessageCustomer_Sex, GetType(System.String))
                    .Add(FIELD_House_B_SalesMessageCustomer_BirthDate, GetType(System.String))
                    .Add(FIELD_House_B_SalesMessageCustomer_CertificateCode, GetType(System.String))
                    .Add(FIELD_House_B_SalesMessageCustomer_MailAddress, GetType(System.String))
                    .Add(FIELD_House_B_SalesMessageCustomer_MailRegion, GetType(System.String))

                    .Add(FIELD_House_B_SalesMessageCustomer_CodeA, GetType(System.String))
                    .Add(FIELD_House_B_SalesMessageCustomer_CodeB, GetType(System.String))
                    .Add(FIELD_House_B_SalesMessageCustomer_CodeC, GetType(System.String))
                    .Add(FIELD_House_B_SalesMessageCustomer_CodeD, GetType(System.String))
                    .Add(FIELD_House_B_SalesMessageCustomer_PhoneNumer, GetType(System.String))

                    .Add(FIELD_House_B_SalesMessageCustomer_CountryRegion, GetType(System.String))
                    .Add(FIELD_House_B_SalesMessageCustomer_StandardBirthDay, GetType(System.DateTime))
                    .Add(FIELD_House_B_SalesMessageCustomer_CountryProvince, GetType(System.String))
                    .Add(FIELD_House_B_SalesMessageCustomer_CountryCity, GetType(System.String))
                    .Add(FIELD_House_B_SalesMessageCustomer_CountryArea, GetType(System.String))

                   
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Data_SalesMessageCustomer = table

        End Function


        '----------------------------------------------------------------
        '创建TABLE_House_B_SalesMessage
        '----------------------------------------------------------------
        Private Function createDataTables_SalesMessage(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_House_B_SalesMessage)
                With table.Columns
                    .Add(FIELD_House_B_SalesMessage_ID, GetType(System.Int32))
                    .Add(FIELD_House_B_SalesMessage_MainHous, GetType(System.String))
                    .Add(FIELD_House_B_SalesMessage_PartialHouse, GetType(System.String))

                    .Add(FIELD_House_B_SalesMessage_Region, GetType(System.String))
                    .Add(FIELD_House_B_SalesMessage_HouseAddress, GetType(System.String))
                    .Add(FIELD_House_B_SalesMessage_RoomNumber, GetType(System.String))
                    .Add(FIELD_House_B_SalesMessage_HouseType, GetType(System.String))
                    .Add(FIELD_House_B_SalesMessage_Floor, GetType(System.String))

                    .Add(FIELD_House_B_SalesMessage_RoomTypeCalc, GetType(System.String))
                    .Add(FIELD_House_B_SalesMessage_TotalFloor, GetType(System.Int32))
                    .Add(FIELD_House_B_SalesMessage_EnclosedPatio, GetType(System.Int32))
                    .Add(FIELD_House_B_SalesMessage_NotEnclosedPatio, GetType(System.Int32))
                    .Add(FIELD_House_B_SalesMessage_Washroom, GetType(System.Int32))

                    .Add(FIELD_House_B_SalesMessage_BuildingArea, GetType(System.Double))
                    .Add(FIELD_House_B_SalesMessage_FloorArea, GetType(System.Double))
                    .Add(FIELD_House_B_SalesMessage_UnitPrice, GetType(System.Double))
                    .Add(FIELD_House_B_SalesMessage_TotalPrice, GetType(System.Double))
                    .Add(FIELD_House_B_SalesMessage_FixtureDate, GetType(System.DateTime))

                    .Add(FIELD_House_B_SalesMessage_ImportUser, GetType(System.String))
                    .Add(FIELD_House_B_SalesMessage_ImportDate, GetType(System.DateTime))
                    .Add(FIELD_House_B_SalesMessage_HouseTypeCalc, GetType(System.String))
                    .Add(FIELD_House_B_SalesMessage_Adjustment, GetType(System.Int32))
                    .Add(FIELD_House_B_SalesMessage_HouseTypeGroup, GetType(System.String))

                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_SalesMessage = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_House_B_FloorArea_Interval
        '----------------------------------------------------------------
        Private Function createDataTables_FloorArea_Interval(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_House_B_FloorArea_Interval)
                With table.Columns
                    .Add(FIELD_House_B_FloorArea_Code, GetType(System.String))
                    .Add(FIELD_House_B_IntervalStart, GetType(System.Double))
                    .Add(FIELD_House_B_IntervalEnd, GetType(System.Double))

                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_FloorArea_Interval = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_House_B_BuildingArea_Interval
        '----------------------------------------------------------------
        Private Function createDataTables_BuildingArea_Interval(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_House_B_BuildingArea_Interval)
                With table.Columns
                    .Add(FIELD_House_B_BuildingArea_Code, GetType(System.String))
                    .Add(FIELD_House_B_IntervalStart, GetType(System.Double))
                    .Add(FIELD_House_B_IntervalEnd, GetType(System.Double))

                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_BuildingArea_Interval = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_House_B_TotalPrice_Interval
        '----------------------------------------------------------------
        Private Function createDataTables_TotalPrice_Interval(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_House_B_TotalPrice_Interval)
                With table.Columns
                    .Add(FIELD_House_B_TotalPrice_Code, GetType(System.String))
                    .Add(FIELD_House_B_IntervalStart, GetType(System.Double))
                    .Add(FIELD_House_B_IntervalEnd, GetType(System.Double))

                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_TotalPrice_Interval = table

        End Function


        '----------------------------------------------------------------
        '创建TABLE_House_B_UnitPrice_Interval
        '----------------------------------------------------------------
        Private Function createDataTables_UnitPrice_Interval(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_House_B_UnitPrice_Interval)
                With table.Columns
                    .Add(FIELD_House_B_UnitPrice_Code, GetType(System.String))
                    .Add(FIELD_House_B_IntervalStart, GetType(System.Double))
                    .Add(FIELD_House_B_IntervalEnd, GetType(System.Double))

                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_UnitPrice_Interval = table

        End Function

       
        '----------------------------------------------------------------
        '创建TABLE_House_V_Data_Statistics
        '----------------------------------------------------------------
        Private Function createDataTables_Data_Statistics(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_House_V_Data_Statistics)
                With table.Columns
                    .Add(FIELD_House_V_Data_Statistics_MonthName, GetType(System.String))
                    .Add(FIELD_House_V_Data_Statistics_BuildingArea, GetType(System.Double))
                    .Add(FIELD_House_V_Data_Statistics_BuildingAreaChain, GetType(System.Double))

                    .Add(FIELD_House_V_Data_Statistics_FloorArea, GetType(System.Double))
                    .Add(FIELD_House_V_Data_Statistics_FloorAreaChain, GetType(System.Double))
                    .Add(FIELD_House_V_Data_Statistics_Volume, GetType(System.Double))

                    .Add(FIELD_House_V_Data_Statistics_VolumeChain, GetType(System.Double))
                    .Add(FIELD_House_V_Data_Statistics_UnitPrice, GetType(System.Double))
                    .Add(FIELD_House_V_Data_Statistics_UnitPriceChain, GetType(System.Double))

                    .Add(FIELD_House_V_Data_Statistics_TotalPrice, GetType(System.Double))
                    .Add(FIELD_House_V_Data_Statistics_TotalPriceChain, GetType(System.Double))

                    .Add(FIELD_House_V_Data_Statistics_Name, GetType(System.String))
                    .Add(FIELD_House_V_Data_Statistics_Type, GetType(System.Int32))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Data_Statistics = table

        End Function


        '----------------------------------------------------------------
        '创建TABLE_House_V_Data_DetailStatistics
        '----------------------------------------------------------------
        Private Function createDataTables_Data_DetailStatistics(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_House_V_Data_DetailStatistics)
                With table.Columns
                    .Add(FIELD_House_V_SalesMessage_MainHous, GetType(System.String))
                    .Add(FIELD_House_V_Data_Statistics_BuildingArea, GetType(System.Double))
                    .Add(FIELD_House_V_Data_Statistics_FloorArea, GetType(System.Double))
                    .Add(FIELD_House_V_Data_Statistics_Volume, GetType(System.Double))
                    .Add(FIELD_House_V_Data_Statistics_UnitPrice, GetType(System.Double))
                    .Add(FIELD_House_V_Data_Statistics_TotalPrice, GetType(System.Double))
                    .Add(FIELD_House_V_Data_Statistics_Name, GetType(System.String))
                    .Add(FIELD_House_V_Data_Statistics_Type, GetType(System.Int32))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Data_DetailStatistics = table

        End Function
    End Class
End Namespace

