Option Strict On
Option Explicit On

Imports System
Imports System.Data
Imports System.Runtime.Serialization

Namespace Xydc.Platform.Common.Data

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.Common.Data
    ' 类名    ：customer_mediumData
    '
    ' 功能描述：
    '     定义“深度数据”有关的数据访问格式
    '----------------------------------------------------------------
    <System.ComponentModel.DesignerCategory("Customer"), SerializableAttribute()> Public Class CustomerMediumData
        Inherits System.Data.DataSet

        '“楼盘_B_销售_客户信息”表信息定义
        '表名称
        Public Const TABLE_House_B_MediumCustomer As String = "Customer_B_Medium"
        '字段序列
        Public Const FIELD_House_B_MediumCustomer_ID As String = "序号"
        Public Const FIELD_House_B_MediumCustomer_CompanyName As String = "公司名称"
        Public Const FIELD_House_B_MediumCustomer_LegalRepresentative As String = "法定代表人"
        Public Const FIELD_House_B_MediumCustomer_Phone As String = "电话"
        Public Const FIELD_House_B_MediumCustomer_MobilePhone As String = "移动电话"
        Public Const FIELD_House_B_MediumCustomer_ContactPerson As String = "联系人一"
        Public Const FIELD_House_B_MediumCustomer_ContactPerson_another As String = "联系人二"
        Public Const FIELD_House_B_MediumCustomer_Sex As String = "称呼"
        Public Const FIELD_House_B_MediumCustomer_Position As String = "职务"
        Public Const FIELD_House_B_MediumCustomer_Adress As String = "地址"
        Public Const FIELD_House_B_MediumCustomer_RegisteredCapitas As String = "注册资本"
        Public Const FIELD_House_B_MediumCustomer_AnnualTurnover As String = "年营业额"
        Public Const FIELD_House_B_MediumCustomer_CustomerType As String = "人员类型"
        Public Const FIELD_House_B_MediumCustomer_CarBrand As String = "车辆品牌"

        '定义初始化表类型enum
        Public Enum enumTableType
            House_B_MediumCustomer = 1
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.Common.Data.CustomerMediumData)
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
                Case enumTableType.House_B_MediumCustomer
                    table = createDataTables_MediumCustomer(strErrMsg)

                Case Else
                    strErrMsg = "无效的表类型！"
                    table = Nothing
            End Select

            createDataTables = table

        End Function


        '----------------------------------------------------------------
        '创建TABLE_House_B_MediumCustomer
        '----------------------------------------------------------------
        Private Function createDataTables_MediumCustomer(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_House_B_MediumCustomer)
                With table.Columns

                    .Add(FIELD_House_B_MediumCustomer_ID, GetType(System.Int32))
                    .Add(FIELD_House_B_MediumCustomer_CompanyName, GetType(System.String))
                    .Add(FIELD_House_B_MediumCustomer_LegalRepresentative, GetType(System.String))

                    .Add(FIELD_House_B_MediumCustomer_Phone, GetType(System.String))
                    .Add(FIELD_House_B_MediumCustomer_MobilePhone, GetType(System.String))
                    .Add(FIELD_House_B_MediumCustomer_ContactPerson, GetType(System.String))
                    .Add(FIELD_House_B_MediumCustomer_ContactPerson_another, GetType(System.String))
                    .Add(FIELD_House_B_MediumCustomer_Sex, GetType(System.String))

                    .Add(FIELD_House_B_MediumCustomer_Position, GetType(System.String))
                    .Add(FIELD_House_B_MediumCustomer_Adress, GetType(System.String))
                    .Add(FIELD_House_B_MediumCustomer_RegisteredCapitas, GetType(System.String))
                    .Add(FIELD_House_B_MediumCustomer_AnnualTurnover, GetType(System.String))
                    .Add(FIELD_House_B_MediumCustomer_CustomerType, GetType(System.String))
                    .Add(FIELD_House_B_MediumCustomer_CarBrand, GetType(System.String))


                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_MediumCustomer = table

        End Function


    End Class
End Namespace

