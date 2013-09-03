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
    ' 类名    ：dacSijuzhanganpai
    '
    ' 功能描述：
    '     提供对“司局长活动安排”模块涉及的数据层操作
    '----------------------------------------------------------------

    Public Class dacSijuzhanganpai
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.DataAccess.dacSijuzhanganpai)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub










        '----------------------------------------------------------------
        ' SqlDataAdapter属性
        '----------------------------------------------------------------
        Protected ReadOnly Property SqlDataAdapter() As System.Data.SqlClient.SqlDataAdapter
            Get
                SqlDataAdapter = m_objSqlDataAdapter
            End Get
        End Property










        '----------------------------------------------------------------
        ' 输出数据到Excel
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objDataSet           ：要导出的数据集
        '     strExcelFile         ：导出到WEB服务器中的Excel文件路径
        '     strMacroName         ：宏名列表
        '     strMacroValue        ：宏值列表
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doExportToExcel( _
            ByRef strErrMsg As String, _
            ByVal objDataSet As System.Data.DataSet, _
            ByVal strExcelFile As String, _
            Optional ByVal strMacroName As String = "", _
            Optional ByVal strMacroValue As String = "") As Boolean

            doExportToExcel = False
            strErrMsg = ""

            Try
                With New Xydc.Platform.DataAccess.dacExcel
                    If .doExport(strErrMsg, objDataSet, strExcelFile, strMacroName, strMacroValue) = False Then
                        GoTo errProc
                    End If
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doExportToExcel = True
            Exit Function
errProc:
            Exit Function

        End Function





        '----------------------------------------------------------------
        ' 获取司局长活动安排数据（按“排序”升序）- 列表显示模式
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     strWhere                    ：搜索字符串
        '     objSijuzhanganpaiData         ：信息数据集
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objSijuzhanganpaiData As Xydc.Platform.Common.Data.ggxxSijuzhanganpaiData) As Boolean

            Dim objTempSijuzhanganpaiData As Xydc.Platform.Common.Data.ggxxSijuzhanganpaiData
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '初始化
            getDataSet = False
            objSijuzhanganpaiData = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                Try
                    '创建数据集
                    objTempSijuzhanganpaiData = New Xydc.Platform.Common.Data.ggxxSijuzhanganpaiData(Xydc.Platform.Common.Data.ggxxSijuzhanganpaiData.enumTableType.GR_B_SIJUZHANGANPAI)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    Dim strFalse As String = Xydc.Platform.Common.Utilities.PulicParameters.CharFalse
                    Dim strTrue As String = Xydc.Platform.Common.Utilities.PulicParameters.CharTrue
                    With Me.m_objSqlDataAdapter
                        '准备SQL

                        strSQL = ""
                        strSQL = strSQL + "   select * " + vbCr
                        strSQL = strSQL + "   from 个人_B_司局长活动安排 a " + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.开始日期,a.排序" + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempSijuzhanganpaiData.Tables(Xydc.Platform.Common.Data.ggxxSijuzhanganpaiData.TABLE_GR_B_SIJUZHANGANPAI))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempSijuzhanganpaiData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.ggxxSijuzhanganpaiData.SafeRelease(objTempSijuzhanganpaiData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objSijuzhanganpaiData = objTempSijuzhanganpaiData
            getDataSet = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.ggxxSijuzhanganpaiData.SafeRelease(objTempSijuzhanganpaiData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取[序号=intXH]的司局长活动安排数据
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     intXH                       ：公告序号
        '     objSijuzhanganpaiData        ：信息数据集
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intXH As Integer, _
            ByRef objSijuzhanganpaiData As Xydc.Platform.Common.Data.ggxxSijuzhanganpaiData) As Boolean

            Dim objTempSijuzhanganpaiData As Xydc.Platform.Common.Data.ggxxSijuzhanganpaiData
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '初始化
            getDataSet = False
            objSijuzhanganpaiData = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                Try
                    '创建数据集
                    objTempSijuzhanganpaiData = New Xydc.Platform.Common.Data.ggxxSijuzhanganpaiData(Xydc.Platform.Common.Data.ggxxSijuzhanganpaiData.enumTableType.GR_B_SIJUZHANGANPAI)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    Dim strFalse As String = Xydc.Platform.Common.Utilities.PulicParameters.CharFalse
                    Dim strTrue As String = Xydc.Platform.Common.Utilities.PulicParameters.CharTrue
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.*" + vbCr
                        strSQL = strSQL + " from" + vbCr
                        strSQL = strSQL + " (" + vbCr
                        strSQL = strSQL + "   select * " + vbCr
                        strSQL = strSQL + "   from 个人_B_司局长活动安排" + vbCr
                        strSQL = strSQL + "   where 序号 = @xh" + vbCr
                        strSQL = strSQL + " ) a" + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@xh", intXH)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempSijuzhanganpaiData.Tables(Xydc.Platform.Common.Data.ggxxSijuzhanganpaiData.TABLE_GR_B_SIJUZHANGANPAI))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempSijuzhanganpaiData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.ggxxSijuzhanganpaiData.SafeRelease(objTempSijuzhanganpaiData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objSijuzhanganpaiData = objTempSijuzhanganpaiData
            getDataSet = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.ggxxSijuzhanganpaiData.SafeRelease(objTempSijuzhanganpaiData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取司局长活动安排数据（按“组织代码”+“排序”升序）
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     objDate                     ：指定日期
        '     objSijuzhanganpaiData         ：信息数据集
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objDate As System.DateTime, _
            ByRef objSijuzhanganpaiData As Xydc.Platform.Common.Data.ggxxSijuzhanganpaiData) As Boolean

            Dim objTempSijuzhanganpaiData As Xydc.Platform.Common.Data.ggxxSijuzhanganpaiData
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '初始化
            getDataSet = False
            objSijuzhanganpaiData = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                Try
                    '创建数据集
                    objTempSijuzhanganpaiData = New Xydc.Platform.Common.Data.ggxxSijuzhanganpaiData(Xydc.Platform.Common.Data.ggxxSijuzhanganpaiData.enumTableType.GR_B_SIJUZHANGANPAI_DAYIN01)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.*" + vbCr
                        strSQL = strSQL + " from" + vbCr
                        strSQL = strSQL + " (" + vbCr
                        strSQL = strSQL + "   select" + vbCr
                        strSQL = strSQL + "     日期 = @rq," + vbCr
                        strSQL = strSQL + "     星期 = datename(dw,@rq)," + vbCr
                        strSQL = strSQL + "     a.参加司局长," + vbCr
                        strSQL = strSQL + "     组织代码 = b.组织代码," + vbCr
                        strSQL = strSQL + "     排序 = cast(b.人员序号 as integer)," + vbCr
                        strSQL = strSQL + "     上午 = dbo.Ggxx_GetLdap(a.参加司局长, @rq, '上午', @fgf)," + vbCr
                        strSQL = strSQL + "     下午 = dbo.Ggxx_GetLdap(a.参加司局长, @rq, '下午', @fgf) " + vbCr
                        strSQL = strSQL + "   from" + vbCr
                        strSQL = strSQL + "   (" + vbCr
                        strSQL = strSQL + "     select 参加司局长" + vbCr
                        strSQL = strSQL + "     from 个人_B_司局长活动安排" + vbCr
                        strSQL = strSQL + "     where 日期 = @rq" + vbCr
                        strSQL = strSQL + "     group by 参加司局长"
                        strSQL = strSQL + "   ) a" + vbCr
                        strSQL = strSQL + "   left join 公共_B_人员 b on a.参加司局长 = b.人员名称" + vbCr
                        strSQL = strSQL + " ) a" + vbCr
                        strSQL = strSQL + " order by a.组织代码,a.排序" + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@rq", objDate)
                        objSqlCommand.Parameters.AddWithValue("@fgf", Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempSijuzhanganpaiData.Tables(Xydc.Platform.Common.Data.ggxxSijuzhanganpaiData.TABLE_GR_B_SIJUZHANGANPAI_DAYIN01))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempSijuzhanganpaiData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.ggxxSijuzhanganpaiData.SafeRelease(objTempSijuzhanganpaiData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objSijuzhanganpaiData = objTempSijuzhanganpaiData
            getDataSet = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.ggxxSijuzhanganpaiData.SafeRelease(objTempSijuzhanganpaiData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function






        '----------------------------------------------------------------
        ' 删除司局长活动安排
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     intXH                ：公告序号
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDelete( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intXH As Integer) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '初始化
            doDelete = False
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim
                If intXH <= 0 Then
                    strErrMsg = "错误：未指定[序号]！"
                    GoTo errProc
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '开始事务
                objSqlTransaction = objSqlConnection.BeginTransaction()

                '删除数据
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '删除“个人_B_司局长活动安排”信息
                    strSQL = ""
                    strSQL = strSQL + " delete from 个人_B_司局长活动安排 " + vbCr
                    strSQL = strSQL + " where 序号 = @xh" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@xh", intXH)
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

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doDelete = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 复制司局长活动安排，从[strFromRQ]复制到[strToRQ]
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strFromRQ            ：要复制的安排日期
        '     strToRQ              ：复制到的安排日期
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doCopy( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strFromRQ As String, _
            ByVal strToRQ As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '初始化
            doCopy = False
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim

                If strFromRQ Is Nothing Then strFromRQ = ""
                strFromRQ = strFromRQ.Trim
                If strFromRQ = "" Then
                    strErrMsg = "错误：未指定要复制[安排日期]！"
                    GoTo errProc
                End If
                If objPulicParameters.isDatetimeString(strFromRQ) = False Then
                    strErrMsg = "错误：[" + strFromRQ + "]是无效的日期！"
                    GoTo errProc
                End If

                If strToRQ Is Nothing Then strToRQ = ""
                strToRQ = strToRQ.Trim
                If strToRQ = "" Then
                    strErrMsg = "错误：未指定复制到的[安排日期]！"
                    GoTo errProc
                End If
                If objPulicParameters.isDatetimeString(strToRQ) = False Then
                    strErrMsg = "错误：[" + strToRQ + "]是无效的日期！"
                    GoTo errProc
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '开始事务
                objSqlTransaction = objSqlConnection.BeginTransaction()

                '复制数据
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    strSQL = ""
                    strSQL = strSQL + " insert into 个人_B_司局长活动安排 (" + vbCr
                    strSQL = strSQL + "   日期,时间,开始时间,结束时间,地点,参加司局长,活动内容,排序,备注" + vbCr
                    strSQL = strSQL + " )" + vbCr
                    strSQL = strSQL + " select " + vbCr
                    strSQL = strSQL + "   日期 = @torq," + vbCr
                    strSQL = strSQL + "   时间," + vbCr
                    strSQL = strSQL + "   开始时间 = @torq + ' ' + DATENAME(hh, 开始时间) + ':' + DATENAME(mi, 开始时间) + ':' + DATENAME(ss, 开始时间)," + vbCr
                    strSQL = strSQL + "   结束时间 = @torq + ' ' + DATENAME(hh, 结束时间) + ':' + DATENAME(mi, 结束时间) + ':' + DATENAME(ss, 结束时间)," + vbCr
                    strSQL = strSQL + "   地点,参加司局长,活动内容,排序,备注" + vbCr
                    strSQL = strSQL + " from 个人_B_司局长活动安排" + vbCr
                    strSQL = strSQL + " where 日期 = @fromrq" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@fromrq", strFromRQ)
                    objSqlCommand.Parameters.AddWithValue("@torq", strToRQ)
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

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doCopy = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 检查“个人_B_司局长活动安排”的数据的合法性
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
        '     objNewData           ：(返回)新数据
        '     objenumEditType      ：编辑类型

        ' 返回
        '     True                 ：合法
        '     False                ：不合法或其他程序错误
        '----------------------------------------------------------------
        Public Function doVerify( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByRef objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            doVerify = False

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "错误：未传入新的数据！"
                    GoTo errProc
                End If
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew, _
                        Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eCpyNew
                    Case Else
                        If objOldData Is Nothing Then
                            strErrMsg = "错误：未传入旧的数据！"
                            GoTo errProc
                        End If
                End Select
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim

                '获取表结构定义
                strSQL = "select top 0 * from 个人_B_司局长活动安排"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, strUserId, strPassword, strSQL, "个人_B_司局长活动安排", objDataSet) = False Then
                    GoTo errProc
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '检查数据长度
                Dim intCount As Integer = objNewData.Count
                Dim strField As String
                Dim strValue As String
                Dim intLen As Integer
                Dim i As Integer
                For i = 0 To intCount - 1 Step 1
                    strField = objNewData.GetKey(i).Trim()
                    strValue = objNewData.Item(i).Trim()
                    Select Case strField
                        '计算列
                        Case Xydc.Platform.Common.Data.ggxxSijuzhanganpaiData.FIELD_GR_B_SIJUZHANGANPAI_XH
                            '自动列

                        Case Xydc.Platform.Common.Data.ggxxSijuzhanganpaiData.FIELD_GR_B_SIJUZHANGANPAI_KSRQ, _
                            Xydc.Platform.Common.Data.ggxxSijuzhanganpaiData.FIELD_GR_B_SIJUZHANGANPAI_JSRQ
                            If strValue = "" Then
                                strValue = Format(Now, "yyyy-MM-dd")
                            End If
                            If objPulicParameters.isDatetimeString(strValue) = False Then
                                strErrMsg = "错误：[" + strField + "]输入无效的日期！"
                                GoTo errProc
                            End If
                            strValue = Format(CType(strValue, System.DateTime), "yyyy-MM-dd")
                        Case Xydc.Platform.Common.Data.ggxxSijuzhanganpaiData.FIELD_GR_B_SIJUZHANGANPAI_RY
                            If strValue = "" Then
                                strErrMsg = "错误：[" + strField + "]不能为空！"
                                GoTo errProc
                            End If
                            With objDataSet.Tables(0).Columns(strField)
                                intLen = objPulicParameters.getStringLength(strValue)
                                If intLen > .MaxLength Then
                                    strErrMsg = "错误：[" + strField + "]长度不能超过[" + .MaxLength.ToString() + "]，实际有[" + intLen.ToString() + "]！"
                                    GoTo errProc
                                End If
                            End With

                        Case Xydc.Platform.Common.Data.ggxxSijuzhanganpaiData.FIELD_GR_B_SIJUZHANGANPAI_PX
                            If strValue = "" Then
                                strErrMsg = "错误：[" + strField + "]不能为空！"
                                GoTo errProc
                            End If
                            If objPulicParameters.isIntegerString(strValue) = False Then
                                strErrMsg = "错误：[" + strField + "]输入无效的数字！"
                                GoTo errProc
                            End If

                        Case Else
                            If strValue <> "" Then
                                With objDataSet.Tables(0).Columns(strField)
                                    intLen = objPulicParameters.getStringLength(strValue)
                                    If intLen > .MaxLength Then
                                        strErrMsg = "错误：[" + strField + "]长度不能超过[" + .MaxLength.ToString() + "]，实际有[" + intLen.ToString() + "]！"
                                        GoTo errProc
                                    End If
                                End With
                            End If
                    End Select

                    objNewData(strField) = strValue
                Next
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '校验“日期”+“排序”
                Dim strKSRQ As String = objNewData(Xydc.Platform.Common.Data.ggxxSijuzhanganpaiData.FIELD_GR_B_SIJUZHANGANPAI_KSRQ)
                Dim strPX As String = objNewData(Xydc.Platform.Common.Data.ggxxSijuzhanganpaiData.FIELD_GR_B_SIJUZHANGANPAI_PX)
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew, _
                        Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eCpyNew
                        strSQL = ""
                        strSQL = strSQL + " select * from 个人_B_司局长活动安排" + vbCr
                        strSQL = strSQL + " where 开始日期 = '" + strKSRQ + "'" + vbCr
                        strSQL = strSQL + " and   排序 =  " + strPX + vbCr
                    Case Else
                        Dim intOldXH As Integer
                        intOldXH = objPulicParameters.getObjectValue(objOldData(Xydc.Platform.Common.Data.ggxxSijuzhanganpaiData.FIELD_GR_B_SIJUZHANGANPAI_XH), 0)
                        strSQL = ""
                        strSQL = strSQL + " select * from 个人_B_司局长活动安排" + vbCr
                        strSQL = strSQL + " where 开始日期 = '" + strKSRQ + "'" + vbCr
                        strSQL = strSQL + " and   排序 =  " + strPX + vbCr
                        strSQL = strSQL + " and   序号 <> " + intOldXH.ToString + vbCr
                End Select
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    '自动获取序号
                    If objdacCommon.getNewCode(strErrMsg, objSqlConnection, "排序", "开始日期", strKSRQ, "个人_B_司局长活动安排", True, strPX) = False Then
                        GoTo errProc
                    End If
                    objNewData(Xydc.Platform.Common.Data.ggxxSijuzhanganpaiData.FIELD_GR_B_SIJUZHANGANPAI_PX) = strPX
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            doVerify = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 保存“个人_B_司局长活动安排”的数据(现有事务)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objSqlTransaction    ：现有事务
        '     objOldData           ：旧数据
        '     objNewData           ：新数据
        '     objenumEditType      ：编辑类型
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doSave( _
            ByRef strErrMsg As String, _
            ByVal objSqlTransaction As System.Data.SqlClient.SqlTransaction, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '初始化
            doSave = False
            strErrMsg = ""

            Try
                '检查
                If objSqlTransaction Is Nothing Then
                    strErrMsg = "错误：未传入现有事务！"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "错误：未传入新的数据！"
                    GoTo errProc
                End If
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew, _
                        Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eCpyNew
                    Case Else
                        If objOldData Is Nothing Then
                            strErrMsg = "错误：未传入旧的数据！"
                            GoTo errProc
                        End If
                End Select

                '获取连接
                objSqlConnection = objSqlTransaction.Connection

                '保存数据
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '计算SQL
                    Dim strFileds As String = ""
                    Dim strValues As String = ""
                    Dim strField As String
                    Dim intCount As Integer
                    Dim i As Integer = 0
                    Select Case objenumEditType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew, _
                            Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eCpyNew
                            '计算更新字段列表
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    '计算列
                                    Case Xydc.Platform.Common.Data.ggxxSijuzhanganpaiData.FIELD_GR_B_SIJUZHANGANPAI_XH
                                        '自动列
                                    Case Else
                                        If strFileds = "" Then
                                            strFileds = objNewData.GetKey(i)
                                        Else
                                            strFileds = strFileds + "," + objNewData.GetKey(i)
                                        End If
                                        If strValues = "" Then
                                            strValues = "@A" + i.ToString()
                                        Else
                                            strValues = strValues + "," + "@A" + i.ToString()
                                        End If
                                End Select
                            Next
                            '准备SQL
                            strSQL = ""
                            strSQL = strSQL + " insert into 个人_B_司局长活动安排 (" + strFileds + ")"
                            strSQL = strSQL + " values (" + strValues + ")"
                            '准备参数
                            objSqlCommand.Parameters.Clear()
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    '计算列
                                    Case Xydc.Platform.Common.Data.ggxxSijuzhanganpaiData.FIELD_GR_B_SIJUZHANGANPAI_XH
                                        '自动列
                                    Case Xydc.Platform.Common.Data.ggxxSijuzhanganpaiData.FIELD_GR_B_SIJUZHANGANPAI_KSRQ, _
                                        Xydc.Platform.Common.Data.ggxxSijuzhanganpaiData.FIELD_GR_B_SIJUZHANGANPAI_JSRQ
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), System.DBNull.Value)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(objNewData.Item(i), System.DateTime))
                                        End If
                                    Case Xydc.Platform.Common.Data.ggxxSijuzhanganpaiData.FIELD_GR_B_SIJUZHANGANPAI_PX
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), 0)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(objNewData.Item(i), System.Int32))
                                        End If
                                    Case Else
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), " ")
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), objNewData.Item(i))
                                        End If
                                End Select
                            Next
                            '执行SQL
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()

                        Case Else
                            '获取原“标识”
                            Dim intOldXH As Integer
                            intOldXH = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.ggxxSijuzhanganpaiData.FIELD_GR_B_SIJUZHANGANPAI_XH), 0)
                            '计算更新字段列表
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.ggxxSijuzhanganpaiData.FIELD_GR_B_SIJUZHANGANPAI_XH
                                        '自动列
                                    Case Else
                                        If strFileds = "" Then
                                            strFileds = objNewData.GetKey(i) + " = @A" + i.ToString()
                                        Else
                                            strFileds = strFileds + "," + objNewData.GetKey(i) + " = @A" + i.ToString()
                                        End If
                                End Select
                            Next
                            '准备SQL
                            strSQL = ""
                            strSQL = strSQL + " update 个人_B_司局长活动安排 set " + vbCr
                            strSQL = strSQL + "   " + strFileds + vbCr
                            strSQL = strSQL + " where 序号 = @oldxh" + vbCr
                            '准备参数
                            objSqlCommand.Parameters.Clear()
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    '计算列
                                    Case Xydc.Platform.Common.Data.ggxxSijuzhanganpaiData.FIELD_GR_B_SIJUZHANGANPAI_XH
                                        '自动列
                                    Case Xydc.Platform.Common.Data.ggxxSijuzhanganpaiData.FIELD_GR_B_SIJUZHANGANPAI_KSRQ, _
                                        Xydc.Platform.Common.Data.ggxxSijuzhanganpaiData.FIELD_GR_B_SIJUZHANGANPAI_JSRQ
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), System.DBNull.Value)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(objNewData.Item(i), System.DateTime))
                                        End If
                                    Case Xydc.Platform.Common.Data.ggxxSijuzhanganpaiData.FIELD_GR_B_SIJUZHANGANPAI_PX
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), 0)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(objNewData.Item(i), System.Int32))
                                        End If
                                    Case Else
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), " ")
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), objNewData.Item(i))
                                        End If
                                End Select
                            Next
                            objSqlCommand.Parameters.AddWithValue("@oldxh", intOldXH)
                            '执行SQL
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()
                    End Select

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doSave = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 保存司局长活动安排数据记录(整个事务完成)
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserId              ：用户标识
        '     strPassword            ：用户密码
        '     objNewData             ：记录新值(返回保存后的新值)
        '     objOldData             ：记录旧值
        '     objenumEditType        ：编辑类型
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function doSave( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim strSQL As String

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            doSave = False

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "错误：未传入连接用户！"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "错误：没有指定要保存的数据！"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim

                '检查主记录
                If Me.doVerify(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType) = False Then
                    GoTo errProc
                End If

                '获取连接事务
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '开始事务
                objSqlTransaction = objSqlConnection.BeginTransaction

                '执行事务
                Try
                    '保存主记录
                    If Me.doSave(strErrMsg, objSqlTransaction, objOldData, objNewData, objenumEditType) = False Then
                        GoTo rollDatabase
                    End If
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo rollDatabase
                End Try

                '提交事务
                objSqlTransaction.Commit()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            doSave = True
            Exit Function

rollDatabase:
            objSqlTransaction.Rollback()
            GoTo errProc

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function




        '----------------------------------------------------------------
        ' 获取新的排序
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strRQ                ：指定日期
        '     strNewPX             ：(返回)新排序
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getNewPX( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strKSRQ As String, _
            ByRef strNewPX As String) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim strSQL As String

            '初始化
            getNewPX = False
            strNewPX = ""
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim()
                If strUserId = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim()
                If strKSRQ Is Nothing Then strKSRQ = ""
                strKSRQ = strKSRQ.Trim
                If strKSRQ = "" Then
                    strErrMsg = "错误：没有指定[安排日期]！"
                    GoTo errProc
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                If objdacCommon.getNewCode(strErrMsg, objSqlConnection, "排序", "开始日期", strKSRQ, "个人_B_司局长活动安排", True, strNewPX) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            getNewPX = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function




    End Class

End Namespace

