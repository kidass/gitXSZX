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
    ' 类名    ：dacCustomerMediumData
    '
    ' 功能描述：
    '     提供对月度深层数据相关的数据层操作    

    '----------------------------------------------------------------
    Public Class dacCustomer_operation
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.DataAccess.dacCustomer_operation)
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
        ' 根据“查询条件”获取“二手客户明细数据”完全数据的数据集
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strUserId                  ：用户标识
        '     strPassword                ：用户密码
        '     strWhere                   ：搜索字符串       
        '     objCustomerMediumData                ：信息数据集
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_MediumCustomerDetail( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objCustomerMediumData As Xydc.Platform.Common.Data.CustomerMediumData) As Boolean

            Dim objTempCustomerMediumData As Xydc.Platform.Common.Data.CustomerMediumData = Nothing
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Nothing
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand = Nothing
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim strSQL As String = ""

            '初始化
            getDataSet_MediumCustomerDetail = False
            objCustomerMediumData = Nothing
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
                    objTempCustomerMediumData = New Xydc.Platform.Common.Data.CustomerMediumData(Xydc.Platform.Common.Data.CustomerMediumData.enumTableType.House_B_MediumCustomer)


                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL

                        strSQL = ""
                        strSQL = strSQL + " select  [序号], [公司名称], left([法定代表人],1) as 法定代表人, [法人称呼], [电话], "
                        strSQL = strSQL + " [传真], [移动电话], left([联系人一],1) as 联系人一, left([联系人二],1) as 联系人二, [称呼], [职务], [地址], "
                        strSQL = strSQL + " [邮编], [企业类型], [邮箱], [注册资本], [经营模式], [主营产品或服务], "
                        strSQL = strSQL + " [主营行业], [公司介绍], [员工人数], [年营业额], [公司成立时间], [公司主页], "
                        strSQL = strSQL + " [所属行业], [经营品牌], [主要客户], [经营地点], [主要市场], [开户银行], [银行帐号], "
                        strSQL = strSQL + " [是否提供OEM代加工], [质量控制], [年进口额], [年出口额], [厂房面积], [月产量], [人员类型], "
                        strSQL = strSQL + " [车辆品牌], [车字段1], [车字段2], [车字段3], [车字段4], [车辆类型], [使用类型], [获取手段], [车辆识别码],"
                        strSQL = strSQL + "  [发动机码], [车辆颜色], [第一次注册日期], [证书名], [国产或进口], [制造国], [制造厂名称], [使用性质], [使用期],"
                        strSQL = strSQL + "  [状态], [出厂日期] FROM [Customer_B_Medium] " + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere
                        Else
                            strSQL = strSQL + " where 序号=1"
                        End If
                        strSQL = strSQL + " order by 人员类型"

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempCustomerMediumData.Tables(Xydc.Platform.Common.Data.CustomerMediumData.TABLE_House_B_MediumCustomer))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempCustomerMediumData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.CustomerMediumData.SafeRelease(objTempCustomerMediumData)
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objCustomerMediumData = objTempCustomerMediumData
            getDataSet_MediumCustomerDetail = True
            Exit Function
errProc:
            Xydc.Platform.Common.Data.CustomerMediumData.SafeRelease(objTempCustomerMediumData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function
        End Function



    End Class
End Namespace

