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
    ' 类名    ：dacSunshine
    '
    ' 功能描述：
    '     提供对楼盘匹配数据相关的数据层操作    
    '----------------------------------------------------------------

    Public Class dacSunshine_x2
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.DataAccess.dacSunshine)
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
                doExportToExcel = .doExport(strErrMsg, objDataSet, strExcelFile, strMacroName, strMacroValue)

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
        ' 获取日楼盘数据最新更新时间
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：连接用户标识
        '     strPassword          ：连接用户密码
        '     objDate              ：返回日楼盘数据最新更新时间
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDayHouseMatchCount( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef objCount As Integer) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String


            getDayHouseMatchCount = False
            objCount = Nothing

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "错误：未指定数据库连接用户！"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '总体解析
                strSQL = strSQL + " select count(*) "
                strSQL = strSQL + " from"
                strSQL = strSQL + " ("
                strSQL = strSQL + " select  [C_XZQh] as 'C_XZQY',[C_XM_NAME],[C_XM_ID],[C_XM_ADDRESS]  from T_HOUSE_INFO a"
                strSQL = strSQL + " where not exists (select [C_XZQh],[C_XM_NAME],[C_XM_ID],[C_XM_ADDRESS] from [T_HOUSE_MATCH_XMID] b where b.[C_XM_ID] = a.[C_XM_ID]  and a.[C_XM_NAME]=B.[C_XM_NAME] and a.[C_XM_ADDRESS]=b.[C_XM_ADDRESS])"
                strSQL = strSQL + " and a.C_TIME > =(select convert(varchar(10),max(C_TIME),120) from T_HOUSE_INFO) "
                strSQL = strSQL + " ) a "
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    Exit Try
                End If

                '重新合成
                Dim objTemp As Integer
                objTemp = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item(0), objTemp)

                '返回
                objCount = objTemp

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getDayHouseMatchCount = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function


        '----------------------------------------------------------------
        ' 查询是否存在该楼盘
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：连接用户标识
        '     strPassword          ：连接用户密码
        '     objDate              ：返回日楼盘数据最新更新时间
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getHouseMatchXmidCount( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef strHouse As String, _
            ByRef objCount As Integer) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String


            getHouseMatchXmidCount = False
            objCount = Nothing

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "错误：未指定数据库连接用户！"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '总体解析
                strSQL = strSQL + " select count(*) "
                strSQL = strSQL + " from"
                strSQL = strSQL + " t_house_match_xmid where c_house='"
                strSQL = strSQL + strHouse
                strSQL = strSQL + "' "
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    Exit Try
                End If

                '重新合成
                Dim objTemp As Integer
                objTemp = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item(0), objTemp)

                '返回
                objCount = objTemp

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getHouseMatchXmidCount = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function


        '----------------------------------------------------------------
        ' 查询是否存在该楼盘
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：连接用户标识
        '     strPassword          ：连接用户密码
        '     objDate              ：返回日楼盘数据最新更新时间
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDayHouseDetailCount( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef strHouse As String, _
            ByRef objCount As Integer) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String


            getDayHouseDetailCount = False
            objCount = Nothing

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "错误：未指定数据库连接用户！"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '总体解析
                strSQL = strSQL + " select count(*) "
                strSQL = strSQL + " from"
                strSQL = strSQL + " t_day_house_detail where 楼盘名称='"
                strSQL = strSQL + strHouse
                strSQL = strSQL + "' "
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    Exit Try
                End If

                '重新合成
                Dim objTemp As Integer
                objTemp = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item(0), objTemp)

                '返回
                objCount = objTemp

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getDayHouseDetailCount = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function




        '----------------------------------------------------------------
        ' 获取日楼盘数据最新更新时间
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：连接用户标识
        '     strPassword          ：连接用户密码
        '     objDate              ：返回日楼盘数据最新更新时间
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDayHouseDataTime( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef objDate As System.DateTime) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String


            getDayHouseDataTime = False
            objDate = Nothing

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "错误：未指定数据库连接用户！"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '总体解析
                strSQL = "select max(日期) from  t_day_house_detail "
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    Exit Try
                End If

                '重新合成
                Dim objTemp As System.DateTime
                objTemp = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item(0), objTemp)

                '返回
                objDate = objTemp

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getDayHouseDataTime = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function



        '----------------------------------------------------------------
        ' 获取日楼盘数据最新更新时间
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：连接用户标识
        '     strPassword          ：连接用户密码
        '     objDate              ：返回日楼盘数据最新更新时间
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDayHouseDate( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef objDate As System.DateTime) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String


            getDayHouseDate = False
            objDate = Nothing

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "错误：未指定数据库连接用户！"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '总体解析
                strSQL = "select max(日期) from  t_day_house_info "
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    Exit Try
                End If

                '重新合成
                Dim objTemp As System.DateTime
                objTemp = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item(0), objTemp)

                '返回
                objDate = objTemp

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getDayHouseDate = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function





        '----------------------------------------------------------------
        ' 获取楼盘匹配的数据集(以代码升序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWhere             ：搜索字符串
        '     objSunshineData          ：信息数据集 
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getHouseMatch( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objSunshineData As Xydc.Platform.Common.Data.SunshineData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempSunshineData As Xydc.Platform.Common.Data.SunshineData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getHouseMatch = False
            objSunshineData = Nothing
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
                    objTempSunshineData = New Xydc.Platform.Common.Data.SunshineData(Xydc.Platform.Common.Data.SunshineData.enumTableType.Sunshine_B_HOUSE_MATCH_XMID)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.*,TYPENAME=case when c_type='1' then '别墅' else '洋房' end " + vbCr
                        strSQL = strSQL + " from " + vbCr
                        strSQL = strSQL + "  T_HOUSE_MATCH_XMID a "
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.C_ID desc" + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand
                        .Fill(objTempSunshineData.Tables(Xydc.Platform.Common.Data.SunshineData.TABLE_Sunshine_B_HOUSE_MATCH_XMID))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempSunshineData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempSunshineData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objSunshineData = objTempSunshineData
            getHouseMatch = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempSunshineData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function
        End Function



        '----------------------------------------------------------------
        ' 获取楼盘匹配的数据集(以代码升序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWhere             ：搜索字符串
        '     objSunshineData          ：信息数据集 
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getSunshineHouseMatch( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objSunshineData As Xydc.Platform.Common.Data.SunshineData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempSunshineData As Xydc.Platform.Common.Data.SunshineData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getSunshineHouseMatch = False
            objSunshineData = Nothing
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
                    objTempSunshineData = New Xydc.Platform.Common.Data.SunshineData(Xydc.Platform.Common.Data.SunshineData.enumTableType.Sunshine_B_HOUSE_MATCH_XMID)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* "
                        strSQL = strSQL + " from"
                        strSQL = strSQL + " ("
                        strSQL = strSQL + " select  [C_XZQh] as 'C_XZQY',[C_XM_NAME],[C_XM_ID],[C_XM_ADDRESS]  from T_HOUSE_INFO a"
                        strSQL = strSQL + " where not exists (select [C_XZQh],[C_XM_NAME],[C_XM_ID],[C_XM_ADDRESS] from [T_HOUSE_MATCH_XMID] b where b.[C_XM_ID] = a.[C_XM_ID]  and a.[C_XM_NAME]=B.[C_XM_NAME] and a.[C_XM_ADDRESS]=b.[C_XM_ADDRESS])"
                        strSQL = strSQL + " and a.C_TIME > =(select convert(varchar(10),max(C_TIME),120) from T_HOUSE_INFO) "
                        strSQL = strSQL + " ) a "
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.C_XZQY " + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand
                        .Fill(objTempSunshineData.Tables(Xydc.Platform.Common.Data.SunshineData.TABLE_Sunshine_B_HOUSE_MATCH_XMID))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempSunshineData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempSunshineData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objSunshineData = objTempSunshineData
            getSunshineHouseMatch = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempSunshineData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function
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

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim intID As Integer

            '初始化
            doSaveSunshineHouseMatch = False
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
                        intID = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.SunshineData.FIELD_Sunshine_B_HOUSE_MATCH_ID), 0)
                        '计算SQL
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
                    Dim strFields As String
                    Dim strValues As String
                    Dim intCount As Integer
                    Dim strValue As String
                    Dim i As Integer



                    intCount = objNewData.Count
                    'strID = objNewData.Item(Xydc.Platform.Common.Data.SunshineData.FIELD_Sunshine_B_HOUSE_MATCH_ID)

                    Select Case objenumEditType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                            strFields = ""
                            strValues = ""
                            For i = 0 To intCount - 1 Step 1
                                If strFields = "" Then
                                    strFields = objNewData.GetKey(i)
                                Else
                                    strFields = strFields + "," + objNewData.GetKey(i)
                                End If
                                If strValues = "" Then
                                    strValues = "@A" + i.ToString()
                                Else
                                    strValues = strValues + "," + "@A" + i.ToString()
                                End If
                            Next

                            strSQL = ""
                            strSQL = strSQL + " insert into T_HOUSE_MATCH_XMID (" + strFields + ")"
                            strSQL = strSQL + " values (" + strValues + ")"
                            objSqlCommand.Parameters.Clear()
                            For i = 0 To intCount - 1 Step 1
                                strValue = objNewData.Item(i).Trim()
                                Select Case objNewData.GetKey(i)
                                    Case Else
                                        If strValue = "" Then strValue = " "
                                        objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), strValue)
                                End Select
                            Next
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()

                        Case Else
                            strFields = ""
                            For i = 0 To intCount - 1 Step 1
                                If strFields = "" Then
                                    strFields = objNewData.GetKey(i) + " = @A" + i.ToString()
                                Else
                                    strFields = strFields + "," + objNewData.GetKey(i) + " = @A" + i.ToString()
                                End If
                            Next
                            strSQL = ""
                            strSQL = strSQL + " update T_HOUSE_MATCH_XMID  set "
                            strSQL = strSQL + " " + strFields + " "
                            strSQL = strSQL + " where C_ID = @C_ID"

                            objSqlCommand.Parameters.Clear()
                            For i = 0 To intCount - 1 Step 1
                                strValue = objNewData.Item(i).Trim()
                                Select Case objNewData.GetKey(i)

                                    Case Else
                                        If strValue = "" Then strValue = " "
                                        objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), strValue)
                                End Select
                            Next
                            objSqlCommand.Parameters.AddWithValue("@C_ID", intID)
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()
                    End Select

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
            doSaveSunshineHouseMatch = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function
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

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            doDeleteSunshineHouseMatch = False
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

                    With New Xydc.Platform.Common.Utilities.PulicParameters
                        strOldDM = .getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.SunshineData.FIELD_Sunshine_B_HOUSE_MATCH_ID), "")
                    End With
                    strSQL = strSQL + " delete from T_HOUSE_MATCH_XMID"
                    strSQL = strSQL + " where C_ID = @C_ID"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@C_ID", strOldDM)

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
            doDeleteSunshineHouseMatch = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function




        '----------------------------------------------------------------
        ' 根据“查询条件”获取“阳光家缘楼盘信息数据分析”完全数据的数据集
        '     strErrMsg                  ：如果错误，则返回错误信息
        '     strSQL                     ：返回的SQL语句
        '     strType                    ：选择楼盘的类型。0-全部；1-商业；2-住宅；3-办公
        '     strWhere                   ：搜索字符串      
        ' 返回
        '     True                       ：成功
        '     False                      ：失败
        '----------------------------------------------------------------
        Public Function getSql_BuildingCompute( _
             ByRef strErrMsg As String, _
             ByRef strSQL As String, _
             ByVal strWhere As String) As Boolean

            getSql_BuildingCompute = False

            strSQL = strSQL + " select  a.Region as '行政区域',a.c_house as '楼盘名称',a.HouseType as '项目类型',退楼数,网签数,网签面积,网签均价,网签总额,b.合共成交,未售套数 from ("
            strSQL = strSQL + " select a.Region,c.c_house,a.HouseType,"
            strSQL = strSQL + " 退楼数=sum(case when datediff(dd,startDate,endDate)>1 then  ComputeReturn else ReturnNumer_Day end),"
            strSQL = strSQL + " 网签数=sum(case when datediff(dd,startDate,endDate)>1 then  ComputeDeal else DealNumer_Day end),"
            strSQL = strSQL + " 网签面积=sum(case when datediff(dd,startDate,endDate)>1 then  Computearea else DealArea_Day end),"
            strSQL = strSQL + " 网签总额=sum(TotalSum_Day),网签均价=case when sum(case when datediff(dd,startDate,endDate)>1 then  Computearea else DealArea_Day end)=0 then 0"
            strSQL = strSQL + " else sum(TotalSum_Day)/sum(case when datediff(dd,startDate,endDate)>1 then  Computearea else DealArea_Day end) end"
            strSQL = strSQL + " from ("
            strSQL = strSQL + "  select * from House_B_Day_Info "
            If strWhere <> "" Then
                strSQL = strSQL + "where " + strWhere
            End If
            strSQL = strSQL + " ) a "
            strSQL = strSQL + " left join T_HOUSE_MATCH_XMID c on a.C_XM_ID=c.C_XM_ID and a.C_XM_NAME=c.C_XM_NAME and a.C_XM_ADDRESS=c.C_XM_ADDRESS"
            strSQL = strSQL + " group by  a.Region,c.c_house,a.HouseType"
            strSQL = strSQL + " )a"
            strSQL = strSQL + " Left Join"
            strSQL = strSQL + " ("
            strSQL = strSQL + " select c_house,合共成交=sum(DealNumer),未售套数=sum(UnsoldNumer) from House_B_Day_Info a  left join T_HOUSE_MATCH_XMID b  on a.C_XM_ID=b.C_XM_ID and a.C_XM_NAME=b.C_XM_NAME and a.C_XM_ADDRESS=b.C_XM_ADDRESS "
            strSQL = strSQL + " where SignDate=(select max(SignDate) from House_B_Day_Info where "
            If strWhere <> "" Then
                strSQL = strSQL + "where " + strWhere
            End If
            strSQL = strSQL + " group by c_house"
            strSQL = strSQL + " )b on a.c_house=b.c_house "

            getSql_BuildingCompute = True
errProc:

            Exit Function
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

            Dim objTempDeepdata As Xydc.Platform.Common.Data.SunshineData = Nothing
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Nothing
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand = Nothing
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim strSQL As String = ""
            Dim strSQL_0 As String = ""
            Dim strSQL_Total As String = ""

            '初始化
            getDataSet_BuildingCompute = False
            objSunshine = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strWhere.Length > 0 Then strWhere = strWhere.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：[阳光家缘楼盘信息数据分析]未指定[连接用户]！"
                    GoTo errProc
                End If
        
                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If


                '获取数据
                Try
                    '创建数据集
                    objTempDeepdata = New Xydc.Platform.Common.Data.SunshineData(Xydc.Platform.Common.Data.SunshineData.enumTableType.Sunshine_V_Houseinfo_Statistics)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter

                        '获取查询语句
                        If getSql_BuildingCompute(strErrMsg, strSQL, strWhere) = False Then
                            GoTo errProc
                        End If

                        strSQL_0 = ""
                        strSQL_0 = strSQL_0 + " select * from "
                        strSQL_0 = strSQL_0 + " ("
                        strSQL_0 = strSQL_0 + " select 行政区域,楼盘名称,网签数,网签均价,合共成交,未售套数,网签总额,网签面积,类型=1,排序=1 from ( "
                        strSQL_0 = strSQL_0 + strSQL
                        strSQL_0 = strSQL_0 + " )A "
                        strSQL_0 = strSQL_0 + " union"
                        strSQL_0 = strSQL_0 + " select 行政区域,楼盘名称='汇总',网签数=sum(case when 网签数<0 then 0 else  网签数 end),网签均价=case when sum(case when 网签面积<0 then 0 else  网签面积 end)<1 then 0 else sum(case when 网签总额<0 then 0 else  网签总额 end)/sum(case when 网签面积<0 then 0 else  网签面积 end) end,合共成交=sum(case when 合共成交<0 then 0 else  合共成交 end), "
                        strSQL_0 = strSQL_0 + " 未售套数=sum(case when 未售套数<0 then 0 else  未售套数 end),网签总额=sum(case when 网签总额<0 then 0 else  网签总额 end),网签面积=sum(case when 网签面积<0 then 0 else  网签面积 end),类型=2,排序=1  from ( "
                        strSQL_0 = strSQL_0 + strSQL
                        strSQL_0 = strSQL_0 + " )A group by 行政区域"
                        strSQL_0 = strSQL_0 + " union"
                        strSQL_0 = strSQL_0 + " select 行政区域,楼盘名称='退房汇总',网签数=sum(case when 退楼数<0 then 退楼数 else  0 end),网签均价=0 ,合共成交=0, "
                        strSQL_0 = strSQL_0 + " 未售套数=0,网签总额=0,网签面积=0,类型=3,排序=1  from ( "
                        strSQL_0 = strSQL_0 + strSQL
                        strSQL_0 = strSQL_0 + " )A group by 行政区域"
                        strSQL_0 = strSQL_0 + " union"
                        strSQL_0 = strSQL_0 + " select 行政区域='合计',楼盘名称='',网签数=sum(case when 网签数<0 then 0 else  网签数 end),网签均价=case when sum(case when 网签面积<0 then 0 else  网签面积 end)<1 then 0 else sum(case when 网签总额<0 then 0 else  网签总额 end)/sum(case when 网签面积<0 then 0 else  网签面积 end) end,合共成交=sum(case when 合共成交<0 then 0 else  合共成交 end), "
                        strSQL_0 = strSQL_0 + " 未售套数=sum(case when 未售套数<0 then 0 else  未售套数 end),网签总额=sum(case when 网签总额<0 then 0 else  网签总额 end),网签面积=sum(case when 网签面积<0 then 0 else  网签面积 end),类型=3,排序=2  from ( "
                        strSQL_0 = strSQL_0 + strSQL
                        strSQL_0 = strSQL_0 + " )A "
                        strSQL_0 = strSQL_0 + " union"
                        strSQL_0 = strSQL_0 + " select 行政区域='退房合计',楼盘名称='',网签数=sum(case when 退楼数<0 then 退楼数 else  0 end),网签均价=0 ,合共成交=0, "
                        strSQL_0 = strSQL_0 + " 未售套数=0,网签总额=0,网签面积=0,类型=4,排序=2  from ( "
                        strSQL_0 = strSQL_0 + strSQL
                        strSQL_0 = strSQL_0 + " )A "
                        strSQL_0 = strSQL_0 + " )A "
                        strSQL_0 = strSQL_0 + " order by A.排序,A.行政区域,A.类型,A.楼盘名称"

                        objSqlCommand.CommandText = strSQL_0
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempDeepdata.Tables(Xydc.Platform.Common.Data.SunshineData.TABLE_Sunshine_V_Houseinfo_Statistics))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDeepdata.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempDeepdata)
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objSunshine = objTempDeepdata
            getDataSet_BuildingCompute = True
            Exit Function
errProc:
            Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempDeepdata)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function
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

            Dim objTempDeepdata As Xydc.Platform.Common.Data.SunshineData = Nothing
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Nothing
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand = Nothing
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim strSQL As String = ""
            Dim strSQL_0 As String = ""
            Dim strSQL_Total As String = ""
            Dim strWhere_0 As String = ""

            '初始化
            getDataSet_WEEKBuildingCompute = False
            objSunshine = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strWhere.Length > 0 Then strWhere = strWhere.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：[阳光家缘楼盘信息数据分析]未指定[连接用户]！"
                    GoTo errProc
                End If

                Dim datStartDate_Start As DateTime
                Dim datStartDate_End As DateTime
                Dim datEndDate_Start As DateTime
                Dim datEndDate_End As DateTime
                Dim frontdatStartDate_Start As DateTime
                Dim frontdatStartDate_End As DateTime
                Dim frontdatEndDate_Start As DateTime
                Dim frontdatEndDate_End As DateTime

                Dim strType As String
                Dim strRegion As String

                strWhere_0 = objParameter("strWhere")

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If


                '获取数据
                Try
                    '创建数据集
                    objTempDeepdata = New Xydc.Platform.Common.Data.SunshineData(Xydc.Platform.Common.Data.SunshineData.enumTableType.Sunshine_V_WeekInfo_Statistics)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter

                        '获取查询语句
                        If getSql_BuildingCompute(strErrMsg, strSQL, strWhere) = False Then
                            GoTo errProc
                        End If

                        '获取查询语句
                        If getSql_BuildingCompute(strErrMsg, strSQL_0, strWhere_0) = False Then
                            GoTo errProc
                        End If

                        strSQL_Total = ""
                        strSQL_Total = strSQL_Total + " select a.* from "
                        strSQL_Total = strSQL_Total + " ("
                        strSQL_Total = strSQL_Total + " select 行政区域,楼盘名称,网签数,网签均价,合共成交,未售套数,网签总额,网签面积,类型=1,排序=1 from ( "
                        strSQL_Total = strSQL_Total + strSQL
                        strSQL_Total = strSQL_Total + " )A "
                        strSQL_Total = strSQL_Total + " union"
                        strSQL_Total = strSQL_Total + " select 行政区域,楼盘名称='汇总',网签数=sum(case when 网签数<0 then 0 else  网签数 end),网签均价=case when sum(case when 网签面积<0 then 0 else  网签面积 end)<1 then 0 else sum(case when 网签总额<0 then 0 else  网签总额 end)/sum(case when 网签面积<0 then 0 else  网签面积 end) end,合共成交=sum(case when 合共成交<0 then 0 else  合共成交 end), "
                        strSQL_Total = strSQL_Total + " 未售套数=sum(case when 未售套数<0 then 0 else  未售套数 end),网签总额=sum(case when 网签总额<0 then 0 else  网签总额 end),网签面积=sum(case when 网签面积<0 then 0 else  网签面积 end),类型=2,排序=1  from ( "
                        strSQL_Total = strSQL_Total + strSQL
                        strSQL_Total = strSQL_Total + " )A group by 行政区域"
                        strSQL_Total = strSQL_Total + " union"
                        strSQL_Total = strSQL_Total + " select 行政区域,楼盘名称='退房汇总',网签数=sum(case when 退楼数<0 then 退楼数 else  0 end),网签均价=0 ,合共成交=0, "
                        strSQL_Total = strSQL_Total + " 未售套数=0,网签总额=0,网签面积=0,类型=3,排序=1  from ( "
                        strSQL_Total = strSQL_Total + strSQL
                        strSQL_Total = strSQL_Total + " )A group by 行政区域"
                        strSQL_Total = strSQL_Total + " union"
                        strSQL_Total = strSQL_Total + " select 行政区域='合计',楼盘名称='',网签数=sum(case when 网签数<0 then 0 else  网签数 end),网签均价=case when sum(case when 网签面积<0 then 0 else  网签面积 end)<1 then 0 else sum(case when 网签总额<0 then 0 else  网签总额 end)/sum(case when 网签面积<0 then 0 else  网签面积 end) end,合共成交=sum(case when 合共成交<0 then 0 else  合共成交 end), "
                        strSQL_Total = strSQL_Total + " 未售套数=sum(case when 未售套数<0 then 0 else  未售套数 end),网签总额=sum(case when 网签总额<0 then 0 else  网签总额 end),网签面积=sum(case when 网签面积<0 then 0 else  网签面积 end),类型=3,排序=2  from ( "
                        strSQL_Total = strSQL_Total + strSQL
                        strSQL_Total = strSQL_Total + " )A "
                        strSQL_Total = strSQL_Total + " union"
                        strSQL_Total = strSQL_Total + " select 行政区域='退房合计',楼盘名称='',网签数=sum(case when 退楼数<0 then 退楼数 else  0 end),网签均价=0 ,合共成交=0, "
                        strSQL_Total = strSQL_Total + " 未售套数=0,网签总额=0,网签面积=0,类型=4,排序=2  from ( "
                        strSQL_Total = strSQL_Total + strSQL
                        strSQL_Total = strSQL_Total + " )A "
                        strSQL_Total = strSQL_Total + " )a "
                        strSQL_Total = strSQL_Total + " left join "
                        strSQL_Total = strSQL_Total + " ("
                        strSQL_Total = strSQL_Total + " select 行政区域,楼盘名称,网签数,网签均价,合共成交,未售套数,网签总额,网签面积,类型=1,排序=1 from ( "
                        strSQL_Total = strSQL_Total + strSQL_0
                        strSQL_Total = strSQL_Total + " )A "
                        strSQL_Total = strSQL_Total + " union"
                        strSQL_Total = strSQL_Total + " select 行政区域,楼盘名称='汇总',网签数=sum(case when 网签数<0 then 0 else  网签数 end),网签均价=case when sum(case when 网签面积<0 then 0 else  网签面积 end)<1 then 0 else sum(case when 网签总额<0 then 0 else  网签总额 end)/sum(case when 网签面积<0 then 0 else  网签面积 end) end,合共成交=sum(case when 合共成交<0 then 0 else  合共成交 end), "
                        strSQL_Total = strSQL_Total + " 未售套数=sum(case when 未售套数<0 then 0 else  未售套数 end),网签总额=sum(case when 网签总额<0 then 0 else  网签总额 end),网签面积=sum(case when 网签面积<0 then 0 else  网签面积 end),类型=2,排序=1  from ( "
                        strSQL_Total = strSQL_Total + strSQL_0
                        strSQL_Total = strSQL_Total + " )A group by 行政区域"
                        strSQL_Total = strSQL_Total + " union"
                        strSQL_Total = strSQL_Total + " select 行政区域,楼盘名称='退房汇总',网签数=sum(case when 退楼数<0 then 退楼数 else  0 end),网签均价=0 ,合共成交=0, "
                        strSQL_Total = strSQL_Total + " 未售套数=0,网签总额=0,网签面积=0,类型=3,排序=1  from ( "
                        strSQL_Total = strSQL_Total + strSQL_0
                        strSQL_Total = strSQL_Total + " )A group by 行政区域"
                        strSQL_Total = strSQL_Total + " union"
                        strSQL_Total = strSQL_Total + " select 行政区域='合计',楼盘名称='',网签数=sum(case when 网签数<0 then 0 else  网签数 end),网签均价=case when sum(case when 网签面积<0 then 0 else  网签面积 end)<1 then 0 else sum(case when 网签总额<0 then 0 else  网签总额 end)/sum(case when 网签面积<0 then 0 else  网签面积 end) end,合共成交=sum(case when 合共成交<0 then 0 else  合共成交 end), "
                        strSQL_Total = strSQL_Total + " 未售套数=sum(case when 未售套数<0 then 0 else  未售套数 end),网签总额=sum(case when 网签总额<0 then 0 else  网签总额 end),网签面积=sum(case when 网签面积<0 then 0 else  网签面积 end),类型=3,排序=2  from ( "
                        strSQL_Total = strSQL_Total + strSQL_0
                        strSQL_Total = strSQL_Total + " )A "
                        strSQL_Total = strSQL_Total + " union"
                        strSQL_Total = strSQL_Total + " select 行政区域='退房合计',楼盘名称='',网签数=sum(case when 退楼数<0 then 退楼数 else  0 end),网签均价=0 ,合共成交=0, "
                        strSQL_Total = strSQL_Total + " 未售套数=0,网签总额=0,网签面积=0,类型=4,排序=2  from ( "
                        strSQL_Total = strSQL_Total + strSQL_0
                        strSQL_Total = strSQL_Total + " )A "
                        strSQL_Total = strSQL_Total + " )b  on a.行政区域=b.行政区域 and a.楼盘名称=b.楼盘名称"
                        strSQL_Total = strSQL_Total + " order by a.排序,a.行政区域,a.类型,a.楼盘名称"

                        strSQL_Total = " "
                        strSQL_Total = strSQL_Total + " select * from ( "
                        strSQL_Total = strSQL_Total + " select a.*,b.网签数 as '上周网签数' from ("
                        strSQL_Total = strSQL_Total + strSQL
                        strSQL_Total = strSQL_Total + " )a"
                        strSQL_Total = strSQL_Total + " left join "
                        strSQL_Total = strSQL_Total + " ("
                        strSQL_Total = strSQL_Total + strSQL_0
                        strSQL_Total = strSQL_Total + " )b on a.行政区域=b.行政区域 and a.楼盘名称=b.楼盘名称 and a.项目类型=b.项目类型"
                        strSQL_Total = strSQL_Total + " )A"
                        strSQL_Total = strSQL_Total + " order by A.行政区域, A.楼盘名称, A.项目类型 "

                        objSqlCommand.CommandText = strSQL_Total
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempDeepdata.Tables(Xydc.Platform.Common.Data.SunshineData.TABLE_Sunshine_V_WeekInfo_Statistics))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDeepdata.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempDeepdata)
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objSunshine = objTempDeepdata
            getDataSet_WEEKBuildingCompute = True
            Exit Function
errProc:
            Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempDeepdata)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function
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

            Dim objTempDeepdata As Xydc.Platform.Common.Data.SunshineData = Nothing
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Nothing
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand = Nothing
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim strSQL As String = ""
            Dim strSQL_0 As String = ""
            Dim strSQL_Total As String = ""

            '初始化
            getDataSet_RegionBuildingCompute = False
            objSunshine = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strWhere.Length > 0 Then strWhere = strWhere.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：[阳光家缘楼盘信息数据分析]未指定[连接用户]！"
                    GoTo errProc
                End If

                Dim datStartDate_Start As DateTime
                Dim datStartDate_End As DateTime
                Dim datEndDate_Start As DateTime
                Dim datEndDate_End As DateTime
                Dim frontdatStartDate_Start As DateTime
                Dim frontdatStartDate_End As DateTime
                Dim frontdatEndDate_Start As DateTime
                Dim frontdatEndDate_End As DateTime

                Dim strType As String
                Dim strRegion As String

                datStartDate_Start = CType(objParameter("StartDate"), System.DateTime)
                datStartDate_End = datStartDate_Start.AddDays(1)
                datEndDate_Start = CType(objParameter("EndDate"), System.DateTime)
                datEndDate_End = datEndDate_Start.AddDays(1)

                frontdatStartDate_Start = datStartDate_Start.AddDays(-7)
                frontdatStartDate_End = datStartDate_End.AddDays(-7)
                frontdatEndDate_Start = datEndDate_Start.AddDays(-7)
                frontdatEndDate_End = datEndDate_End.AddDays(-7)

                strType = objParameter("type")

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If



                '获取数据
                Try
                    '创建数据集
                    objTempDeepdata = New Xydc.Platform.Common.Data.SunshineData(Xydc.Platform.Common.Data.SunshineData.enumTableType.Sunshine_V_WeekInfo_Statistics)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter

                        '获取查询语句
                        If getSql_BuildingCompute(strErrMsg, strSQL, strWhere) = False Then
                            GoTo errProc
                        End If

                        strSQL_Total = " "
                        strSQL_Total = strSQL_Total + " select * from ( "
                        strSQL_Total = strSQL_Total + " select a.行政区域,a.网签数,b.网签数 as '上周网签数',a.网签均价,b.网签均价 as '上周网签均价',网签数环比=(a.网签数-b.网签数)/b.网签数*100,网签均价环比=(a.网签均价-b.网签均价)/b.网签均价*100  from ("
                        strSQL_Total = strSQL_Total + " select a.行政区域,网签数=sum(网签数),网签均价=sum(网签总额)/sum(网签面积) from ("
                        strSQL_Total = strSQL_Total + strSQL
                        strSQL_Total = strSQL_Total + " )a group by 行政区域"
                        strSQL_Total = strSQL_Total + " )a"
                        strSQL_Total = strSQL_Total + " left join "
                        strSQL_Total = strSQL_Total + " ("
                        strSQL_Total = strSQL_Total + " select a.行政区域,a.网签数=sum(网签数),网签均价=sum(网签总额)/sum(网签面积) from ("
                        strSQL_Total = strSQL_Total + strSQL_0
                        strSQL_Total = strSQL_Total + " )a group by 行政区域"
                        strSQL_Total = strSQL_Total + " )b on a.行政区域=b.行政区域"
                        strSQL_Total = strSQL_Total + " )A"
                        strSQL_Total = strSQL_Total + " order by A.行政区域 "
                        objSqlCommand.CommandText = strSQL_Total
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempDeepdata.Tables(Xydc.Platform.Common.Data.SunshineData.TABLE_Sunshine_V_WeekInfo_Statistics))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDeepdata.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempDeepdata)
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objSunshine = objTempDeepdata
            getDataSet_RegionBuildingCompute = True
            Exit Function
errProc:
            Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempDeepdata)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function
        End Function

        '----------------------------------------------------------------
        ' 根据“查询条件”获取“阳光家缘楼盘区域信息数据分析”完全数据的数据集
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

            Dim objTempDeepdata As Xydc.Platform.Common.Data.SunshineData = Nothing
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Nothing
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand = Nothing
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim strSQL As String = ""
            Dim strSQL_0 As String = ""
            Dim strSQL_Total As String = ""

            '初始化
            getDataSet_RegionCompute = False
            objSunshine = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strWhere.Length > 0 Then strWhere = strWhere.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：[阳光家缘楼盘信息数据分析]未指定[连接用户]！"
                    GoTo errProc
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If



                '获取数据
                Try
                    '创建数据集
                    objTempDeepdata = New Xydc.Platform.Common.Data.SunshineData(Xydc.Platform.Common.Data.SunshineData.enumTableType.Sunshine_V_WeekRegion_Statistics)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter

                        '获取查询语句
                        If getSql_BuildingCompute(strErrMsg, strSQL, strWhere) = False Then
                            GoTo errProc
                        End If

                        strSQL_Total = strSQL_Total + " select a.行政区域,a.网签数,0 as '上周网签数',a.网签均价,0 as '上周网签均价',网签数环比=0,网签均价环比=0  from ("
                        strSQL_Total = strSQL_Total + " select a.行政区域,网签数=sum(网签数),网签均价=sum(网签总额)/sum(网签面积) from ("
                        strSQL_Total = strSQL_Total + strSQL
                        strSQL_Total = strSQL_Total + " )a group by a.行政区域"
                        strSQL_Total = strSQL_Total + " )a"
                        'strSQL_Total = strSQL_Total + " order by a.行政区域 "
                        objSqlCommand.CommandText = strSQL_Total
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempDeepdata.Tables(Xydc.Platform.Common.Data.SunshineData.TABLE_Sunshine_V_WeekRegion_Statistics))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDeepdata.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempDeepdata)
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objSunshine = objTempDeepdata
            getDataSet_RegionCompute = True
            Exit Function
errProc:
            Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempDeepdata)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function
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

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempSunshineData As Xydc.Platform.Common.Data.SunshineData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getWeekMonitoringHouse = False
            objSunshine = Nothing
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
                    objTempSunshineData = New Xydc.Platform.Common.Data.SunshineData(Xydc.Platform.Common.Data.SunshineData.enumTableType.Sunshine_B_WeekMonitoringHouse)

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
                        strSQL = strSQL + " Sunshine_B_WeekMonitoringHouse a " + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.Region asc,a.MonitoringID desc" + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand
                        .Fill(objTempSunshineData.Tables(Xydc.Platform.Common.Data.SunshineData.TABLE_Sunshine_B_WeekMonitoringHouse))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempSunshineData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempSunshineData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objSunshine = objTempSunshineData
            getWeekMonitoringHouse = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempSunshineData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 保存周楼盘匹配的数据
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


            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim intID As Integer

            '初始化
            doSaveSunshineWeekMonitoringHouse = False
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
                        intID = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.SunshineData.FIELD_Sunshine_B_WeekMonitoringHouse_ID), 0)
                        '计算SQL
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
                    Dim strFields As String
                    Dim strValues As String
                    Dim intCount As Integer
                    Dim strValue As String
                    Dim i As Integer



                    intCount = objNewData.Count
                    'strID = objNewData.Item(Xydc.Platform.Common.Data.SunshineData.FIELD_Sunshine_B_HOUSE_MATCH_ID)

                    Select Case objenumEditType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                            strFields = ""
                            strValues = ""
                            For i = 0 To intCount - 1 Step 1
                                If strFields = "" Then
                                    strFields = objNewData.GetKey(i)
                                Else
                                    strFields = strFields + "," + objNewData.GetKey(i)
                                End If
                                If strValues = "" Then
                                    strValues = "@A" + i.ToString()
                                Else
                                    strValues = strValues + "," + "@A" + i.ToString()
                                End If
                            Next

                            strSQL = ""
                            strSQL = strSQL + " insert into Sunshine_B_WeekMonitoringHouse (" + strFields + ")"
                            strSQL = strSQL + " values (" + strValues + ")"
                            objSqlCommand.Parameters.Clear()
                            For i = 0 To intCount - 1 Step 1
                                strValue = objNewData.Item(i).Trim()
                                Select Case objNewData.GetKey(i)
                                    Case Else
                                        If strValue = "" Then strValue = " "
                                        objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), strValue)
                                End Select
                            Next
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()

                        Case Else
                            strFields = ""
                            For i = 0 To intCount - 1 Step 1
                                If strFields = "" Then
                                    strFields = objNewData.GetKey(i) + " = @A" + i.ToString()
                                Else
                                    strFields = strFields + "," + objNewData.GetKey(i) + " = @A" + i.ToString()
                                End If
                            Next
                            strSQL = ""
                            strSQL = strSQL + " update Sunshine_B_WeekMonitoringHouse  set "
                            strSQL = strSQL + " " + strFields + " "
                            strSQL = strSQL + " where MonitoringID = @C_ID"

                            objSqlCommand.Parameters.Clear()
                            For i = 0 To intCount - 1 Step 1
                                strValue = objNewData.Item(i).Trim()
                                Select Case objNewData.GetKey(i)

                                    Case Else
                                        If strValue = "" Then strValue = " "
                                        objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), strValue)
                                End Select
                            Next
                            objSqlCommand.Parameters.AddWithValue("@C_ID", intID)
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()
                    End Select

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
            doSaveSunshineWeekMonitoringHouse = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

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


            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            doDeleteSunshineWeekMonitoringHouse = False
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

                    With New Xydc.Platform.Common.Utilities.PulicParameters
                        strOldDM = .getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.SunshineData.FIELD_Sunshine_B_WeekMonitoringHouse_ID), "")
                    End With
                    strSQL = strSQL + " delete from Sunshine_B_WeekMonitoringHouse"
                    strSQL = strSQL + " where MonitoringID = @C_ID"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@C_ID", strOldDM)

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
            doDeleteSunshineWeekMonitoringHouse = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function
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

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempSunshineData As Xydc.Platform.Common.Data.SunshineData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getMonthMonitoringHouse = False
            objSunshine = Nothing
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
                    objTempSunshineData = New Xydc.Platform.Common.Data.SunshineData(Xydc.Platform.Common.Data.SunshineData.enumTableType.Sunshine_B_MonthMonitoringHouse)

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
                        strSQL = strSQL + " Sunshine_B_MonthMonitoringHouse a " + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.Region asc,a.MonitoringID desc" + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand
                        .Fill(objTempSunshineData.Tables(Xydc.Platform.Common.Data.SunshineData.TABLE_Sunshine_B_MonthMonitoringHouse))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempSunshineData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempSunshineData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objSunshine = objTempSunshineData
            getMonthMonitoringHouse = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempSunshineData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

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


            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim intID As Integer

            '初始化
            doSaveSunshineMonthMonitoringHouse = False
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
                        intID = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.SunshineData.FIELD_Sunshine_B_WeekMonitoringHouse_ID), 0)
                        '计算SQL
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
                    Dim strFields As String
                    Dim strValues As String
                    Dim intCount As Integer
                    Dim strValue As String
                    Dim i As Integer



                    intCount = objNewData.Count
                    'strID = objNewData.Item(Xydc.Platform.Common.Data.SunshineData.FIELD_Sunshine_B_HOUSE_MATCH_ID)

                    Select Case objenumEditType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                            strFields = ""
                            strValues = ""
                            For i = 0 To intCount - 1 Step 1
                                If strFields = "" Then
                                    strFields = objNewData.GetKey(i)
                                Else
                                    strFields = strFields + "," + objNewData.GetKey(i)
                                End If
                                If strValues = "" Then
                                    strValues = "@A" + i.ToString()
                                Else
                                    strValues = strValues + "," + "@A" + i.ToString()
                                End If
                            Next

                            strSQL = ""
                            strSQL = strSQL + " insert into Sunshine_B_MonthMonitoringHouse (" + strFields + ")"
                            strSQL = strSQL + " values (" + strValues + ")"
                            objSqlCommand.Parameters.Clear()
                            For i = 0 To intCount - 1 Step 1
                                strValue = objNewData.Item(i).Trim()
                                Select Case objNewData.GetKey(i)
                                    Case Else
                                        If strValue = "" Then strValue = " "
                                        objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), strValue)
                                End Select
                            Next
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()

                        Case Else
                            strFields = ""
                            For i = 0 To intCount - 1 Step 1
                                If strFields = "" Then
                                    strFields = objNewData.GetKey(i) + " = @A" + i.ToString()
                                Else
                                    strFields = strFields + "," + objNewData.GetKey(i) + " = @A" + i.ToString()
                                End If
                            Next
                            strSQL = ""
                            strSQL = strSQL + " update Sunshine_B_MonthMonitoringHouse  set "
                            strSQL = strSQL + " " + strFields + " "
                            strSQL = strSQL + " where MonitoringID = @C_ID"

                            objSqlCommand.Parameters.Clear()
                            For i = 0 To intCount - 1 Step 1
                                strValue = objNewData.Item(i).Trim()
                                Select Case objNewData.GetKey(i)

                                    Case Else
                                        If strValue = "" Then strValue = " "
                                        objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), strValue)
                                End Select
                            Next
                            objSqlCommand.Parameters.AddWithValue("@C_ID", intID)
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()
                    End Select

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
            doSaveSunshineMonthMonitoringHouse = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

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


            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            doDeleteSunshineMonthMonitoringHouse = False
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

                    With New Xydc.Platform.Common.Utilities.PulicParameters
                        strOldDM = .getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.SunshineData.FIELD_Sunshine_B_WeekMonitoringHouse_ID), "")
                    End With
                    strSQL = strSQL + " delete from Sunshine_B_MonthMonitoringHouse"
                    strSQL = strSQL + " where MonitoringID = @C_ID"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@C_ID", strOldDM)

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
            doDeleteSunshineMonthMonitoringHouse = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function
        End Function


        '----------------------------------------------------------------
        ' 删除均价楼盘的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteSunshineHouseMatchPrice( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            doDeleteSunshineHouseMatchPrice = False
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

                    With New Xydc.Platform.Common.Utilities.PulicParameters
                        strOldDM = .getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.SunshineData.FIELD_Sunshine_B_HOUSE_MATCH_ID), "")
                    End With
                    strSQL = strSQL + " delete from T_HOUSE_MATCH_PRICE"
                    strSQL = strSQL + " where C_ID = @C_ID"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@C_ID", strOldDM)

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
            doDeleteSunshineHouseMatchPrice = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 保存均价楼盘匹配的数据
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
        Public Function doSaveSunshineHouseMatchPrice( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim intID As Integer

            '初始化
            doSaveSunshineHouseMatchPrice = False
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
                        intID = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.SunshineData.FIELD_Sunshine_B_HOUSE_MATCH_ID), 0)
                        '计算SQL
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
                    Dim strFields As String
                    Dim strValues As String
                    Dim intCount As Integer
                    Dim strValue As String
                    Dim i As Integer



                    intCount = objNewData.Count
                    'strID = objNewData.Item(Xydc.Platform.Common.Data.SunshineData.FIELD_Sunshine_B_HOUSE_MATCH_ID)

                    Select Case objenumEditType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                            strFields = ""
                            strValues = ""
                            For i = 0 To intCount - 1 Step 1
                                If strFields = "" Then
                                    strFields = objNewData.GetKey(i)
                                Else
                                    strFields = strFields + "," + objNewData.GetKey(i)
                                End If
                                If strValues = "" Then
                                    strValues = "@A" + i.ToString()
                                Else
                                    strValues = strValues + "," + "@A" + i.ToString()
                                End If
                            Next

                            strSQL = ""
                            strSQL = strSQL + " insert into T_HOUSE_MATCH_PRICE (" + strFields + ")"
                            strSQL = strSQL + " values (" + strValues + ")"
                            objSqlCommand.Parameters.Clear()
                            For i = 0 To intCount - 1 Step 1
                                strValue = objNewData.Item(i).Trim()
                                Select Case objNewData.GetKey(i)
                                    Case Else
                                        If strValue = "" Then strValue = " "
                                        objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), strValue)
                                End Select
                            Next
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()

                        Case Else
                            strFields = ""
                            For i = 0 To intCount - 1 Step 1
                                If strFields = "" Then
                                    strFields = objNewData.GetKey(i) + " = @A" + i.ToString()
                                Else
                                    strFields = strFields + "," + objNewData.GetKey(i) + " = @A" + i.ToString()
                                End If
                            Next
                            strSQL = ""
                            strSQL = strSQL + " update T_HOUSE_MATCH_PRICE  set "
                            strSQL = strSQL + " " + strFields + " "
                            strSQL = strSQL + " where C_ID = @C_ID"

                            objSqlCommand.Parameters.Clear()
                            For i = 0 To intCount - 1 Step 1
                                strValue = objNewData.Item(i).Trim()
                                Select Case objNewData.GetKey(i)

                                    Case Else
                                        If strValue = "" Then strValue = " "
                                        objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), strValue)
                                End Select
                            Next
                            objSqlCommand.Parameters.AddWithValue("@C_ID", intID)
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()
                    End Select

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
            doSaveSunshineHouseMatchPrice = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function
        End Function

        '----------------------------------------------------------------
        ' 获取楼盘均价匹配的数据集(以代码升序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWhere             ：搜索字符串
        '     objSunshineData          ：信息数据集 
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getHouseMatchPrice( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objSunshineData As Xydc.Platform.Common.Data.SunshineData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempSunshineData As Xydc.Platform.Common.Data.SunshineData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getHouseMatchPrice = False
            objSunshineData = Nothing
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
                    objTempSunshineData = New Xydc.Platform.Common.Data.SunshineData(Xydc.Platform.Common.Data.SunshineData.enumTableType.Sunshine_B_HOUSE_MATCH_PRICE)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.*,TYPENAME=case when c_type='1' then '别墅' else '洋房' end " + vbCr
                        strSQL = strSQL + " from " + vbCr
                        strSQL = strSQL + "  T_HOUSE_MATCH_PRICE a "
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.C_ID desc" + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand
                        .Fill(objTempSunshineData.Tables(Xydc.Platform.Common.Data.SunshineData.TABLE_Sunshine_B_HOUSE_MATCH_PRICE))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempSunshineData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempSunshineData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objSunshineData = objTempSunshineData
            getHouseMatchPrice = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.SunshineData.SafeRelease(objTempSunshineData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function
        End Function

        '----------------------------------------------------------------
        ' 获取楼盘均价数据集 
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWhere             ：搜索字符串
        '     strPrice             ：均价  
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getHouse_totalAveragePrice( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strHouse As String, _
            ByVal strStartDate As String, _
            ByVal strEndDate As String, _
            ByVal strHouseType As String, _
            ByVal objSqlConnection As System.Data.SqlClient.SqlConnection, _
            ByRef strPrice As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            '初始化
            getHouse_totalAveragePrice = False
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""

                '检查
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If


                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If


                '准备SQL
                strSQL = ""

                'strSQL = strSQL + " exec dbo.Sunshine_P_getWeekStatistics_business @FirstStartDate,@FinalStartDate "
                'strSQL = strSQL + " select 均价 = dbo.Sunshine_F_getAveragePrice(b.楼盘名称,'2013-02-23','2013-03-02', b.房屋类型)" + vbCr
                strSQL = strSQL + " select 均价 = dbo.Sunshine_F_getAveragePrice( " + vbCr
                strSQL = strSQL + "'" + strHouse + "'," + vbCr
                strSQL = strSQL + "'" + strStartDate + "'," + vbCr
                strSQL = strSQL + "'" + strEndDate + "'," + vbCr
                strSQL = strSQL + "'" + strHouseType + "')" + vbCr

                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If

                If objDataSet.Tables(0).Rows.Count < 1 Then
                    Exit Try
                End If

                '返回信息
                With objDataSet.Tables(0).Rows(0)
                    strPrice = objPulicParameters.getObjectValue(.Item("均价"), "")
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try


            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getHouse_totalAveragePrice = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function
    End Class
End Namespace
