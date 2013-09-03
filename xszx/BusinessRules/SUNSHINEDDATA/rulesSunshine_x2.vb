Option Strict On
Option Explicit On

Imports System
Imports System.Data
Imports System.Security.Cryptography
Imports Microsoft.VisualBasic

Imports Xydc.Platform.SystemFramework
Imports Xydc.Platform.Common.Data
Imports Xydc.Platform.DataAccess

Namespace Xydc.Platform.BusinessRules
    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：rulesSunshine
    '
    ' 功能描述： 
    '   　提供对阳光家缘数据的逻辑层支持
    '----------------------------------------------------------------
    Public Class rulesSunshine_x2
        Implements System.IDisposable

        Private m_objdacSunshine As Xydc.Platform.DataAccess.dacSunshine_x2

        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
            m_objdacSunshine = New Xydc.Platform.DataAccess.dacSunshine_x2
        End Sub

        '----------------------------------------------------------------
        ' 安全释放本身资源
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessRules.rulesSunshine_x2)
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
            If Not (m_objdacSunshine Is Nothing) Then
                m_objdacSunshine.Dispose()
                m_objdacSunshine = Nothing
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
            With Me.m_objdacSunshine
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
            With Me.m_objdacSunshine
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
            With Me.m_objdacSunshine
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
            With Me.m_objdacSunshine
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
            With Me.m_objdacSunshine
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
            With Me.m_objdacSunshine
                doExcelSheetDelete = .doExcelSheetDelete(strErrMsg, strSrcFile, strSrcSheet)
            End With
        End Function


        '----------------------------------------------------------------
        ' 获取系统是否存在尚未匹配的记录，返回记录数
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：连接用户标识
        '     strPassword          ：连接用户密码
        '     objCount              ：返回记录数
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDayHouseMatchCount( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef objCount As Integer) As Boolean

            Try
                With m_objdacSunshine
                    getDayHouseMatchCount = .getDayHouseMatchCount(strErrMsg, strUserId, strPassword, objCount)
                End With
            Catch ex As Exception
                getDayHouseMatchCount = False
                strErrMsg = ex.Message
            End Try

        End Function


        '----------------------------------------------------------------
        ' 获取系统是否存在尚未匹配的记录，返回记录数
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：连接用户标识
        '     strPassword          ：连接用户密码
        '     objCount              ：返回记录数
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

            Try
                With m_objdacSunshine
                    getHouseMatchXmidCount = .getHouseMatchXmidCount(strErrMsg, strUserId, strPassword, strHouse, objCount)
                End With
            Catch ex As Exception
                getHouseMatchXmidCount = False
                strErrMsg = ex.Message
            End Try

        End Function


        '----------------------------------------------------------------
        ' 获取系统是否存在尚未匹配的记录，返回记录数
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：连接用户标识
        '     strPassword          ：连接用户密码
        '     objCount              ：返回记录数
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

            Try
                With m_objdacSunshine
                    getDayHouseDetailCount = .getDayHouseDetailCount(strErrMsg, strUserId, strPassword, strHouse, objCount)
                End With
            Catch ex As Exception
                getDayHouseDetailCount = False
                strErrMsg = ex.Message
            End Try

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

            Try
                With m_objdacSunshine
                    getDayHouseDataTime = .getDayHouseDataTime(strErrMsg, strUserId, strPassword, objDate)
                End With
            Catch ex As Exception
                getDayHouseDataTime = False
                strErrMsg = ex.Message
            End Try

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

            Try
                With m_objdacSunshine
                    getDayHouseDate = .getDayHouseDate(strErrMsg, strUserId, strPassword, objDate)
                End With
            Catch ex As Exception
                getDayHouseDate = False
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
        Public Function getHouseMatch( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objSunshine As Xydc.Platform.Common.Data.SunshineData) As Boolean

            Try
                With m_objdacSunshine
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
                With m_objdacSunshine
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
                With m_objdacSunshine
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
                With m_objdacSunshine
                    doDeleteSunshineHouseMatch = .doDeleteSunshineHouseMatch(strErrMsg, strUserId, strPassword, objOldData)
                End With
            Catch ex As Exception
                doDeleteSunshineHouseMatch = False
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

            With m_objdacSunshine
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
        Public Function getDataSet_WEEKBuildingCompute( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByVal objParameter As System.Collections.Specialized.NameValueCollection, _
            ByRef objSunshine As Xydc.Platform.Common.Data.SunshineData) As Boolean

            With m_objdacSunshine
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
        Public Function getDataSet_RegionBuildingCompute( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByVal objParameter As System.Collections.Specialized.NameValueCollection, _
            ByRef objSunshine As Xydc.Platform.Common.Data.SunshineData) As Boolean

            With m_objdacSunshine
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
        Public Function getDataSet_RegionCompute( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByVal objParameter As System.Collections.Specialized.NameValueCollection, _
            ByRef objSunshine As Xydc.Platform.Common.Data.SunshineData) As Boolean

            With m_objdacSunshine
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
                With m_objdacSunshine
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
                With m_objdacSunshine
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
                With m_objdacSunshine
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
                With m_objdacSunshine
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
                With m_objdacSunshine
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
                With m_objdacSunshine
                    doDeleteSunshineMonthMonitoringHouse = .doDeleteSunshineMonthMonitoringHouse(strErrMsg, strUserId, strPassword, objOldData)
                End With
            Catch ex As Exception
                doDeleteSunshineMonthMonitoringHouse = False
                strErrMsg = ex.Message
            End Try
        End Function

        '----------------------------------------------------------------
        ' 删除均价楼盘匹配的数据
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

            Try
                With m_objdacSunshine
                    doDeleteSunshineHouseMatchPrice = .doDeleteSunshineHouseMatchPrice(strErrMsg, strUserId, strPassword, objOldData)
                End With
            Catch ex As Exception
                doDeleteSunshineHouseMatchPrice = False
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
        Public Function doSaveSunshineHouseMatchPrice( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Try
                With m_objdacSunshine
                    doSaveSunshineHouseMatchPrice = .doSaveSunshineHouseMatchPrice(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType)
                End With
            Catch ex As Exception
                doSaveSunshineHouseMatchPrice = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取楼盘均价匹配的数据集(以代码升序排序)
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
        Public Function getHouseMatchPrice( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objSunshine As Xydc.Platform.Common.Data.SunshineData) As Boolean

            Try
                With m_objdacSunshine
                    getHouseMatchPrice = .getHouseMatchPrice(strErrMsg, strUserId, strPassword, strWhere, objSunshine)
                End With
            Catch ex As Exception
                getHouseMatchPrice = False
                strErrMsg = ex.Message
            End Try

        End Function

    End Class
End Namespace