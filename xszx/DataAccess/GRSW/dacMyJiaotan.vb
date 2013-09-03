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
    ' 类名    ：dacMyJiaotan
    '
    ' 功能描述：
    '     提供对“公共_B_交谈”模块涉及的数据层操作
    '----------------------------------------------------------------

    Public Class dacMyJiaotan
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.DataAccess.dacMyJiaotan)
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
        ' 输出即时交流数据到Excel
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objDataSet           ：要导出的数据集
        '     strExcelFile         ：导出到WEB服务器中的Excel文件路径
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doExportToExcel( _
            ByRef strErrMsg As String, _
            ByVal objDataSet As System.Data.DataSet, _
            ByVal strExcelFile As String) As Boolean

            doExportToExcel = False
            strErrMsg = ""

            Try
                With New Xydc.Platform.DataAccess.dacExcel
                    If .doExport(strErrMsg, objDataSet, strExcelFile) = False Then
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
        ' 获取[发送人=strUserXM]的交谈数据
        ' 获取“公共_B_交谈”的数据集(以发送时间降序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strUserXM            ：当前操作员名称
        '     strWhere             ：搜索字符串
        '     objJiaotanDataSet    ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByVal strWhere As String, _
            ByRef objJiaotanDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData) As Boolean

            Dim objTempJiaotanDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '初始化
            getDataSet = False
            objJiaotanDataSet = Nothing
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
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                Try
                    '创建数据集
                    objTempJiaotanDataSet = New Xydc.Platform.Common.Data.grswMyJiaotanData(Xydc.Platform.Common.Data.grswMyJiaotanData.enumTableType.GG_B_JIAOTAN)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from 公共_B_交谈 a " + vbCr
                        strSQL = strSQL + " where a.发送人 = '" + strUserXM + "'" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " and " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.发送时间 desc " + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempJiaotanDataSet.Tables(Xydc.Platform.Common.Data.grswMyJiaotanData.TABLE_GG_B_JIAOTAN))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempJiaotanDataSet.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.grswMyJiaotanData.SafeRelease(objTempJiaotanDataSet)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objJiaotanDataSet = objTempJiaotanDataSet
            getDataSet = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.grswMyJiaotanData.SafeRelease(objTempJiaotanDataSet)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取[接收人=strUserXM]的留言数据
        ' 获取“公共_B_交谈”的数据集(以发送时间降序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strUserXM            ：当前操作员名称
        '     strWhere             ：搜索字符串
        '     blnUnused            ：接口重载用
        '     objJiaotanDataSet    ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByVal strWhere As String, _
            ByVal blnUnused As Boolean, _
            ByRef objJiaotanDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData) As Boolean

            Dim objTempJiaotanDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '初始化
            getDataSet = False
            objJiaotanDataSet = Nothing
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
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                Try
                    '创建数据集
                    objTempJiaotanDataSet = New Xydc.Platform.Common.Data.grswMyJiaotanData(Xydc.Platform.Common.Data.grswMyJiaotanData.enumTableType.GG_B_JIAOTAN)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from 公共_B_交谈 a " + vbCr
                        strSQL = strSQL + " where a.接收人 = '" + strUserXM + "'" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " and " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.发送时间 desc " + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempJiaotanDataSet.Tables(Xydc.Platform.Common.Data.grswMyJiaotanData.TABLE_GG_B_JIAOTAN))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempJiaotanDataSet.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.grswMyJiaotanData.SafeRelease(objTempJiaotanDataSet)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objJiaotanDataSet = objTempJiaotanDataSet
            getDataSet = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.grswMyJiaotanData.SafeRelease(objTempJiaotanDataSet)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据流水号获取交谈信息
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strLSH               ：流水号
        '     objJiaotanDataSet    ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strLSH As String, _
            ByRef objJiaotanDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData) As Boolean

            Dim objTempJiaotanDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '初始化
            getDataSet = False
            objJiaotanDataSet = Nothing
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
                If strLSH Is Nothing Then strLSH = ""
                strLSH = strLSH.Trim
                If strLSH = "" Then strLSH = "0"

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                Try
                    '创建数据集
                    objTempJiaotanDataSet = New Xydc.Platform.Common.Data.grswMyJiaotanData(Xydc.Platform.Common.Data.grswMyJiaotanData.enumTableType.GG_B_JIAOTAN)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from 公共_B_交谈 a " + vbCr
                        strSQL = strSQL + " where a.流水号 = " + strLSH + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempJiaotanDataSet.Tables(Xydc.Platform.Common.Data.grswMyJiaotanData.TABLE_GG_B_JIAOTAN))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempJiaotanDataSet.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.grswMyJiaotanData.SafeRelease(objTempJiaotanDataSet)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objJiaotanDataSet = objTempJiaotanDataSet
            getDataSet = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.grswMyJiaotanData.SafeRelease(objTempJiaotanDataSet)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取strUserXM发送或接收的交谈数据(带附件信息,HTML格式)
        ' 获取“公共_B_交谈”的数据集(以发送时间降序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strUserXM            ：当前操作员名称
        '     strWhere             ：搜索字符串
        '     objJiaotanDataSet    ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDataSetHtml( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByVal strWhere As String, _
            ByRef objJiaotanDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            Dim objTempJiaotanDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '初始化
            getDataSetHtml = False
            objJiaotanDataSet = Nothing
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
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                Try
                    '创建数据集
                    objTempJiaotanDataSet = New Xydc.Platform.Common.Data.grswMyJiaotanData(Xydc.Platform.Common.Data.grswMyJiaotanData.enumTableType.GG_B_VT_JIAOTAN_FJXX)

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
                        strSQL = strSQL + "   select a.*," + vbCr
                        strSQL = strSQL + "     附件     = dbo.GetJsjlFjxxHtmlByWJBS(a.唯一标识)," + vbCr
                        strSQL = strSQL + "     已读状态 = case when a.标志 = 1 then @true else @false end" + vbCr
                        strSQL = strSQL + "   from 公共_B_交谈 a" + vbCr
                        strSQL = strSQL + "   where (a.发送人 = @userxm or a.接收人 = @userxm)" + vbCr
                        strSQL = strSQL + " ) a" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.发送时间 desc " + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@true", Xydc.Platform.Common.Utilities.PulicParameters.CharTrue)
                        objSqlCommand.Parameters.AddWithValue("@false", Xydc.Platform.Common.Utilities.PulicParameters.CharFalse)
                        objSqlCommand.Parameters.AddWithValue("@userxm", strUserXM)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempJiaotanDataSet.Tables(Xydc.Platform.Common.Data.grswMyJiaotanData.TABLE_GG_B_VT_JIAOTAN_FJXX))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempJiaotanDataSet.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.grswMyJiaotanData.SafeRelease(objTempJiaotanDataSet)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objJiaotanDataSet = objTempJiaotanDataSet
            getDataSetHtml = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.grswMyJiaotanData.SafeRelease(objTempJiaotanDataSet)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取strUserXM发送或接收的交谈数据(带附件信息,Text格式)
        ' 获取“公共_B_交谈”的数据集(以发送时间降序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strUserXM            ：当前操作员名称
        '     strWhere             ：搜索字符串
        '     objJiaotanDataSet    ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDataSetText( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByVal strWhere As String, _
            ByRef objJiaotanDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            Dim objTempJiaotanDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '初始化
            getDataSetText = False
            objJiaotanDataSet = Nothing
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
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                Try
                    '创建数据集
                    objTempJiaotanDataSet = New Xydc.Platform.Common.Data.grswMyJiaotanData(Xydc.Platform.Common.Data.grswMyJiaotanData.enumTableType.GG_B_VT_JIAOTAN_FJXX)

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
                        strSQL = strSQL + "   select a.*," + vbCr
                        strSQL = strSQL + "     附件     = dbo.GetJsjlFjxxTextByWJBS(a.唯一标识)," + vbCr
                        strSQL = strSQL + "     已读状态 = case when a.标志 = 1 then @true else @false end" + vbCr
                        strSQL = strSQL + "   from 公共_B_交谈 a" + vbCr
                        strSQL = strSQL + "   where (a.发送人 = @userxm or a.接收人 = @userxm)" + vbCr
                        strSQL = strSQL + " ) a" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.发送时间 desc " + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@true", Xydc.Platform.Common.Utilities.PulicParameters.CharTrue)
                        objSqlCommand.Parameters.AddWithValue("@false", Xydc.Platform.Common.Utilities.PulicParameters.CharFalse)
                        objSqlCommand.Parameters.AddWithValue("@userxm", strUserXM)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempJiaotanDataSet.Tables(Xydc.Platform.Common.Data.grswMyJiaotanData.TABLE_GG_B_VT_JIAOTAN_FJXX))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempJiaotanDataSet.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.grswMyJiaotanData.SafeRelease(objTempJiaotanDataSet)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objJiaotanDataSet = objTempJiaotanDataSet
            getDataSetText = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.grswMyJiaotanData.SafeRelease(objTempJiaotanDataSet)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 检查“公共_B_交谈”的数据的合法性
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
        Public Function doVerifyData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByRef objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            doVerifyData = False

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
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                    Case Else
                        If objOldData Is Nothing Then
                            strErrMsg = "错误：未传入旧的数据！"
                            GoTo errProc
                        End If
                End Select
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim

                '获取表结构定义
                strSQL = "select top 0 * from 公共_B_交谈"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, strUserId, strPassword, strSQL, "公共_B_交谈", objDataSet) = False Then
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
                        Case Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_LSH
                            '自动值

                        Case Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_WYBS
                            '系统自动给定值
                            If strValue = "" Then
                                If objdacCommon.getNewGUID(strErrMsg, strUserId, strPassword, strValue) = False Then
                                    GoTo errProc
                                End If
                            End If

                        Case Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_FSSJ
                            If strValue = "" Then
                                strValue = Format(Now, "yyyy-MM-dd HH:mm:ss")
                            End If
                            If objPulicParameters.isDatetimeString(strValue) = False Then
                                strErrMsg = "错误：[" + strField + "]输入无效的日期！"
                                GoTo errProc
                            End If
                            strValue = Format(CType(strValue, System.DateTime), "yyyy-MM-dd HH:mm:ss")

                        Case Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_FSR, _
                            Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_JSR, _
                            Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_XX
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

                        Case Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_BZ
                            If strValue = "" Then
                                strValue = "0"
                            End If
                            If objPulicParameters.isIntegerString(strValue) = False Then
                                strErrMsg = "错误：[" + strField + "]输入无效的数字！"
                                GoTo errProc
                            End If
                            If strValue <> "0" Then
                                strValue = "1"
                            End If

                        Case Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_TS
                            If strValue = "" Then
                                strValue = "0"
                            End If
                            If strValue <> "0" Then
                                strValue = "1"
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

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            doVerifyData = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 保存“公共_B_交谈”的数据
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
        Public Function doSaveData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '初始化
            doSaveData = False
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
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
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim

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
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                            '计算更新字段列表
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_LSH
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
                            strSQL = strSQL + " insert into 公共_B_交谈 (" + strFileds + ")"
                            strSQL = strSQL + " values (" + strValues + ")"
                            '准备参数
                            objSqlCommand.Parameters.Clear()
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_LSH
                                    Case Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_FSSJ
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), System.DBNull.Value)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(objNewData.Item(i), System.DateTime))
                                        End If
                                    Case Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_BZ
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
                            Dim intOldLSH As Integer
                            intOldLSH = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_LSH), 0)
                            '计算更新字段列表
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_LSH
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
                            strSQL = strSQL + " update 公共_B_交谈 set " + vbCr
                            strSQL = strSQL + "   " + strFileds + vbCr
                            strSQL = strSQL + " where 流水号 = @oldlsh" + vbCr
                            '准备参数
                            objSqlCommand.Parameters.Clear()
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_LSH
                                    Case Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_FSSJ
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), System.DBNull.Value)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(objNewData.Item(i), System.DateTime))
                                        End If
                                    Case Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_BZ
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
                            objSqlCommand.Parameters.AddWithValue("@oldlsh", intOldLSH)
                            '执行SQL
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

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doSaveData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 保存“公共_B_交谈”的数据(现有事务)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objSqlTransaction    ：现有事务
        '     objOldData           ：旧数据
        '     objNewData           ：新数据
        '     objenumEditType      ：编辑类型
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doSaveData( _
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
            doSaveData = False
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
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
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
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                            '计算更新字段列表
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_LSH
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
                            strSQL = strSQL + " insert into 公共_B_交谈 (" + strFileds + ")"
                            strSQL = strSQL + " values (" + strValues + ")"
                            '准备参数
                            objSqlCommand.Parameters.Clear()
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_LSH
                                    Case Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_FSSJ
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), System.DBNull.Value)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(objNewData.Item(i), System.DateTime))
                                        End If
                                    Case Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_BZ
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
                            Dim intOldLSH As Integer
                            intOldLSH = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_LSH), 0)
                            '计算更新字段列表
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_LSH
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
                            strSQL = strSQL + " update 公共_B_交谈 set " + vbCr
                            strSQL = strSQL + "   " + strFileds + vbCr
                            strSQL = strSQL + " where 流水号 = @oldlsh" + vbCr
                            '准备参数
                            objSqlCommand.Parameters.Clear()
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_LSH
                                    Case Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_FSSJ
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), System.DBNull.Value)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(objNewData.Item(i), System.DateTime))
                                        End If
                                    Case Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_BZ
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
                            objSqlCommand.Parameters.AddWithValue("@oldlsh", intOldLSH)
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
            doSaveData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 删除“公共_B_交谈”的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：要删除的数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            '初始化
            doDeleteData = False
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If objOldData Is Nothing Then
                    strErrMsg = "错误：未传入要删除的数据！"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim

                '获取“唯一标识”
                Dim strWYBS As String
                strWYBS = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_WYBS), "")

                '删除数据
                If Me.doDeleteData(strErrMsg, strUserId, strPassword, strWYBS) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            '返回
            doDeleteData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 删除指定strWJBS的“公共_B_交谈”的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWJBS              ：唯一标识
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWJBS As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseFTP As New Xydc.Platform.Common.Utilities.BaseFTP
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objdacXitongpeizhi As New Xydc.Platform.DataAccess.dacXitongpeizhi
            Dim objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            Dim objFJDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData

            Dim strSQL As String

            '初始化
            doDeleteData = False
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
                If strWJBS Is Nothing Then strWJBS = ""
                strWJBS = strWJBS.Trim
                If strWJBS = "" Then strWJBS = ""

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取FTP连接参数
                If objdacXitongpeizhi.getFtpServerParam(strErrMsg, objSqlConnection, objFTPProperty) = False Then
                    GoTo errProc
                End If

                '获取附件数据
                If Me.getFujianDataSet(strErrMsg, strUserId, strPassword, strWJBS, objFJDataSet) = False Then
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
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '删除“公共_B_交谈_附件”信息
                    strSQL = ""
                    strSQL = strSQL + " delete from 公共_B_交谈_附件 " + vbCr
                    strSQL = strSQL + " where 文件标识 = @wjbs" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '删除“公共_B_交谈”
                    strSQL = ""
                    strSQL = strSQL + " delete from 公共_B_交谈 " + vbCr
                    strSQL = strSQL + " where 唯一标识 = @wybs" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@wybs", strWJBS)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '删除“公共_B_交谈_附件”对应文件数据
                    Dim strFilePath As String
                    Dim intCount As Integer
                    Dim strUrl As String
                    Dim i As Integer
                    With objFJDataSet.Tables(Xydc.Platform.Common.Data.grswMyJiaotanData.TABLE_GG_B_JIAOTAN_FUJIAN)
                        intCount = .Rows.Count
                        For i = 0 To intCount - 1 Step 1
                            strFilePath = objPulicParameters.getObjectValue(.Rows(i).Item("位置"), "")
                            If strFilePath <> "" Then
                                With objFTPProperty
                                    strUrl = .getUrl(strFilePath)
                                    If objBaseFTP.doDeleteFile(strErrMsg, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword) = False Then
                                        '可以不成功，形成垃圾文件！
                                    End If
                                End With
                            End If
                        Next
                    End With
                    objFJDataSet.Dispose()
                    objFJDataSet = Nothing

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
            Xydc.Platform.DataAccess.dacXitongpeizhi.SafeRelease(objdacXitongpeizhi)
            Xydc.Platform.Common.Utilities.FTPProperty.SafeRelease(objFTPProperty)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doDeleteData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacXitongpeizhi.SafeRelease(objdacXitongpeizhi)
            Xydc.Platform.Common.Utilities.FTPProperty.SafeRelease(objFTPProperty)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取[接收人=strUserXM]的没有阅读的交谈数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strUserXM            ：当前操作员名称
        '     objJiaotanDataSet    ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDataSetWYD( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByRef objJiaotanDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '初始化
            getDataSetWYD = False
            objJiaotanDataSet = Nothing
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
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                Try
                    '创建数据集
                    objDataSet = New Xydc.Platform.Common.Data.grswMyJiaotanData(Xydc.Platform.Common.Data.grswMyJiaotanData.enumTableType.GG_B_JIAOTAN)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from 公共_B_交谈 a " + vbCr
                        strSQL = strSQL + " where (接收人 = '" + strUserXM + "' and isnull(标志,0) = 0) " + vbCr   '送给我+未阅读
                        strSQL = strSQL + " order by a.发送时间" + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objDataSet.Tables(Xydc.Platform.Common.Data.grswMyJiaotanData.TABLE_GG_B_JIAOTAN))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objDataSet.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.grswMyJiaotanData.SafeRelease(objDataSet)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objJiaotanDataSet = objDataSet
            getDataSetWYD = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.grswMyJiaotanData.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取strUserXM在指定之间之后发送或接收的交谈数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strUserXM            ：当前操作员名称
        '     strZDSJ              ：指定时间
        '     objJiaotanDataSet    ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDataSetAfterTime( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByVal strZDSJ As String, _
            ByRef objJiaotanDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '初始化
            getDataSetAfterTime = False
            objJiaotanDataSet = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If strZDSJ Is Nothing Then strZDSJ = ""
                strZDSJ = strZDSJ.Trim
                If strZDSJ = "" Then
                    strErrMsg = "错误：未指定要时间！"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                Try
                    '创建数据集
                    objDataSet = New Xydc.Platform.Common.Data.grswMyJiaotanData(Xydc.Platform.Common.Data.grswMyJiaotanData.enumTableType.GG_B_JIAOTAN)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from 公共_B_交谈 a " + vbCr
                        strSQL = strSQL + " where (接收人 = @userxm or 发送人 = @userxm)" + vbCr  '送给我或我发送
                        strSQL = strSQL + " and   发送时间 >= @zdsj" + vbCr                       '指定时间后发生的
                        strSQL = strSQL + " order by a.发送时间" + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@userxm", strUserXM)
                        objSqlCommand.Parameters.AddWithValue("@zdsj", strZDSJ)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objDataSet.Tables(Xydc.Platform.Common.Data.grswMyJiaotanData.TABLE_GG_B_JIAOTAN))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objDataSet.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.grswMyJiaotanData.SafeRelease(objDataSet)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objJiaotanDataSet = objDataSet
            getDataSetAfterTime = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.grswMyJiaotanData.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 设置我已经阅读strLSH信息
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strUserXM            ：当前操作员名称
        '     strLSH               ：流水号
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doSetReadFlag( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByVal strLSH As String) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '初始化
            doSetReadFlag = False
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If strLSH Is Nothing Then strLSH = ""
                strLSH = strLSH.Trim
                If strLSH = "" Then strLSH = "0"
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                Try
                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '开始事务
                    objSqlTransaction = objSqlConnection.BeginTransaction
                    objSqlCommand.Transaction = objSqlTransaction

                    Try
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " update 公共_B_交谈 set " + vbCr
                        strSQL = strSQL + "   标志 = @bz" + vbCr
                        strSQL = strSQL + " where 流水号 = @lsh" + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@bz", 1)
                        objSqlCommand.Parameters.AddWithValue("@lsh", CType(strLSH, Integer))

                        '执行
                        objSqlCommand.ExecuteNonQuery()

                    Catch ex As Exception
                        objSqlTransaction.Rollback()
                        GoTo errProc
                    End Try

                    '提交事务
                    objSqlTransaction.Commit()

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doSetReadFlag = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 备份附件文件
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objFTPProperty       ：FTP服务器属性
        '     objFJData            ：附件数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doBackupFiles_FJ( _
            ByRef strErrMsg As String, _
            ByVal objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty, _
            ByVal objFJData As Xydc.Platform.Common.Data.grswMyJiaotanData) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.grswMyJiaotanData.TABLE_GG_B_JIAOTAN_FUJIAN
            Dim strBakExt As String = Xydc.Platform.Common.Utilities.PulicParameters.BACKUPFILEEXT

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objBaseFTP As New Xydc.Platform.Common.Utilities.BaseFTP

            doBackupFiles_FJ = False
            strErrMsg = ""

            Try
                If objFTPProperty Is Nothing Then
                    strErrMsg = "错误：未传入FTP服务器参数！"
                    GoTo errProc
                End If
                If objFJData Is Nothing Then
                    Exit Try
                End If
                If objFJData.Tables(strTable) Is Nothing Then
                    Exit Try
                End If

                '备份原文件
                Dim blnExisted As Boolean
                Dim strFileName As String
                Dim strOldFile As String
                Dim strUrl As String
                Dim intCount As Integer
                Dim i As Integer
                With objFJData.Tables(strTable)
                    intCount = .DefaultView.Count
                    For i = intCount - 1 To 0 Step -1
                        strOldFile = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_FUJIAN_WJWZ), "")
                        If strOldFile <> "" Then
                            With objFTPProperty
                                strUrl = .getUrl(strOldFile)
                                If objBaseFTP.isFileExisted(strErrMsg, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword, blnExisted) = False Then
                                    '可以不成功：可能是文件不存在
                                Else
                                    If blnExisted = True Then
                                        strFileName = objBaseLocalFile.getFileName(strOldFile) + strBakExt
                                        If objBaseFTP.doRenameFile(strErrMsg, strUrl, strFileName, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword, True) = False Then
                                            GoTo errProc
                                        End If
                                    End If
                                End If
                            End With
                        End If
                    Next
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)

            doBackupFiles_FJ = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 删除附件的备份文件
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objFTPProperty       ：FTP服务器属性
        '     objFJData            ：附件数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteBackupFiles_FJ( _
            ByRef strErrMsg As String, _
            ByVal objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty, _
            ByVal objFJData As Xydc.Platform.Common.Data.grswMyJiaotanData) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.grswMyJiaotanData.TABLE_GG_B_JIAOTAN_FUJIAN
            Dim strBakExt As String = Xydc.Platform.Common.Utilities.PulicParameters.BACKUPFILEEXT

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseFTP As New Xydc.Platform.Common.Utilities.BaseFTP

            doDeleteBackupFiles_FJ = False
            strErrMsg = ""

            Try
                If objFTPProperty Is Nothing Then
                    strErrMsg = "错误：未传入FTP服务器参数！"
                    GoTo errProc
                End If
                If objFJData Is Nothing Then
                    Exit Try
                End If
                If objFJData.Tables(strTable) Is Nothing Then
                    Exit Try
                End If

                Dim strOldFile As String
                Dim intCount As Integer
                Dim strUrl As String
                Dim i As Integer
                With objFJData.Tables(strTable)
                    intCount = .DefaultView.Count
                    For i = intCount - 1 To 0 Step -1
                        strOldFile = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_FUJIAN_WJWZ), "")
                        If strOldFile <> "" Then
                            With objFTPProperty
                                strUrl = .getUrl(strOldFile + strBakExt)
                                If objBaseFTP.doDeleteFile(strErrMsg, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword) = False Then
                                    '可以不成功,形成垃圾数据
                                End If
                            End With
                        End If
                    Next
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)

            doDeleteBackupFiles_FJ = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 从备份或新命名文件中恢复原附件文件
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strWJBS              ：文件标识
        '     intWJND              ：新文件存放的年度
        '     objFTPProperty       ：FTP服务器属性
        '     objNewData           ：新附件数据
        '     objOldData           ：原附件数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doRestoreFiles_FJ( _
            ByRef strErrMsg As String, _
            ByVal strWJBS As String, _
            ByVal intWJND As Integer, _
            ByVal objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty, _
            ByVal objNewData As Xydc.Platform.Common.Data.grswMyJiaotanData, _
            ByVal objOldData As Xydc.Platform.Common.Data.grswMyJiaotanData) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.grswMyJiaotanData.TABLE_GG_B_JIAOTAN_FUJIAN
            Dim strBakExt As String = Xydc.Platform.Common.Utilities.PulicParameters.BACKUPFILEEXT

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objBaseFTP As New Xydc.Platform.Common.Utilities.BaseFTP

            doRestoreFiles_FJ = False
            strErrMsg = ""

            Try
                If objFTPProperty Is Nothing Then
                    strErrMsg = "错误：未传入FTP服务器参数！"
                    GoTo errProc
                End If
                If objOldData Is Nothing Then
                    Exit Try
                End If
                If objOldData.Tables(strTable) Is Nothing Then
                    Exit Try
                End If

                '优先从备份文件回滚
                Dim strBasePath As String = Xydc.Platform.Common.Data.grswMyJiaotanData.FILEDIR_FJ
                Dim blnExisted As Boolean
                Dim strNewWJWZ As String
                Dim strOldWJWZ As String
                Dim strNewFile As String
                Dim strOldFile As String
                Dim strToUrl As String
                Dim strUrl As String
                Dim blnDo As Boolean
                Dim intCountA As Integer
                Dim intCount As Integer
                Dim i As Integer
                Dim j As Integer
                With objOldData.Tables(strTable)
                    intCount = .DefaultView.Count
                    For i = intCount - 1 To 0 Step -1
                        strOldFile = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_FUJIAN_WJWZ), "")
                        strOldWJWZ = strOldFile.ToUpper
                        If strOldFile <> "" Then
                            With objFTPProperty
                                '先从备份中恢复
                                strUrl = .getUrl(strOldFile + strBakExt)
                                If objBaseFTP.isFileExisted(strErrMsg, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword, blnExisted) = False Then
                                    blnExisted = False
                                End If
                                If blnExisted = True Then
                                    '备份文件存在，则从备份文件中尽可能恢复
                                    strToUrl = .getUrl(strOldFile)
                                    objBaseFTP.doRenameFile(strErrMsg, strUrl, strToUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword)
                                Else
                                    '备份文件不存在，则检查备份文件是否已改名为对应的新文件？
                                    If Not (objNewData Is Nothing) Then
                                        blnDo = False
                                        With objNewData.Tables(strTable)
                                            intCountA = .DefaultView.Count
                                            For j = 0 To intCountA - 1 Step 1
                                                strNewWJWZ = objPulicParameters.getObjectValue(.DefaultView.Item(j).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_WJWZ), "")
                                                If strOldWJWZ = strNewWJWZ.ToUpper Then
                                                    '获取对应的新文件
                                                    If Me.getFTPFileName_FJ(strErrMsg, strOldFile, intWJND, strWJBS, j + 1, strBasePath, strNewFile) = False Then
                                                        blnDo = False
                                                    Else
                                                        blnDo = True
                                                    End If
                                                    Exit For
                                                End If
                                            Next
                                        End With
                                        If blnDo = True Then
                                            strUrl = .getUrl(strNewFile)
                                            If objBaseFTP.isFileExisted(strErrMsg, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword, blnExisted) = False Then
                                                blnExisted = False
                                            End If
                                            If blnExisted = True Then
                                                '已经新文件存在，则执行从新文件中尽可能恢复
                                                strToUrl = .getUrl(strOldFile)
                                                objBaseFTP.doRenameFile(strErrMsg, strUrl, strToUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword)
                                            End If
                                        End If
                                    End If
                                End If
                            End With
                        End If
                    Next
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)

            doRestoreFiles_FJ = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据本地文件获取FTP服务器文件的命名
        ' 文件附件命名方案：文件标识-FJ-序号
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strLocalFile         ：本地文件名
        '     intWJND              ：文件年度
        '     strWJBS              ：文件标识
        '     intXH                ：序号
        '     strBasePath          ：附件目录基本目录
        '     strRemoteFile        ：返回FTP服务器文件路径
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getFTPFileName_FJ( _
            ByRef strErrMsg As String, _
            ByVal strLocalFile As String, _
            ByVal intWJND As Integer, _
            ByVal strWJBS As String, _
            ByVal intXH As Integer, _
            ByVal strBasePath As String, _
            ByRef strRemoteFile As String) As Boolean

            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile

            getFTPFileName_FJ = False
            strRemoteFile = ""

            Try
                '检查
                If strLocalFile Is Nothing Then strLocalFile = ""
                strLocalFile = strLocalFile.Trim()
                If strLocalFile = "" Then
                    Exit Try
                End If
                If strWJBS Is Nothing Then strWJBS = ""
                strWJBS = strWJBS.Trim()
                If strWJBS = "" Then
                    Exit Try
                End If
                If strBasePath Is Nothing Then strBasePath = ""
                strBasePath = strBasePath.Trim

                '获取文件名
                Dim strFileName As String = ""
                Dim strFileExt As String = ""
                strFileExt = objBaseLocalFile.getExtension(strLocalFile)

                '文件附件命名方案：文件标识-FJ-序号
                strFileName = strWJBS + "-FJ-" + intXH.ToString() + strFileExt
                strFileName = objBaseLocalFile.doMakePath(intWJND.ToString(), strFileName)

                '复合目录+文件
                strFileName = objBaseLocalFile.doMakePath(strBasePath, strFileName)

                '返回
                strRemoteFile = strFileName

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)

            getFTPFileName_FJ = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 保存附件数据
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserId              ：用户标识
        '     strPassword            ：用户密码
        '     strWJBS                ：文件标识
        '     intWJND                ：新文件存放的年度
        '     objSqlTransaction      ：现有事务
        '     objFTPProperty         ：FTP服务器属性
        '     objNewData             ：记录新值(返回保存后的新值)
        '     objOldData             ：记录旧值
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function doSaveFujian( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWJBS As String, _
            ByVal intWJND As Integer, _
            ByVal objSqlTransaction As System.Data.SqlClient.SqlTransaction, _
            ByVal objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty, _
            ByRef objNewData As Xydc.Platform.Common.Data.grswMyJiaotanData, _
            ByVal objOldData As Xydc.Platform.Common.Data.grswMyJiaotanData) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            Dim strTable As String = Xydc.Platform.Common.Data.grswMyJiaotanData.TABLE_GG_B_JIAOTAN_FUJIAN
            Dim strBakExt As String = Xydc.Platform.Common.Utilities.PulicParameters.BACKUPFILEEXT
            Dim blnNewTrans As Boolean = False
            Dim strSQL As String

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objBaseFTP As New Xydc.Platform.Common.Utilities.BaseFTP
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '初始化
            doSaveFujian = False
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "错误：未传入连接用户！"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "错误：未传入新的数据！"
                    GoTo errProc
                End If
                If objFTPProperty Is Nothing Then
                    strErrMsg = "错误：未传入FTP服务器参数！"
                    GoTo errProc
                End If
                If strWJBS Is Nothing Then strWJBS = ""
                strWJBS = strWJBS.Trim
                If strWJBS = "" Then
                    Exit Try
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim

                '获取现有信息
                If objSqlTransaction Is Nothing Then
                    If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                        GoTo errProc
                    End If
                Else
                    objSqlConnection = objSqlTransaction.Connection
                End If

                '开始事务
                If objSqlTransaction Is Nothing Then
                    blnNewTrans = True
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Else
                    blnNewTrans = False
                End If

                '保存数据
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '删除“公共_B_交谈_附件”数据
                    strSQL = ""
                    strSQL = strSQL + " delete from 公共_B_交谈_附件 " + vbCr
                    strSQL = strSQL + " where 文件标识 = @wjbs" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    Try
                        '在源文件的同目录中将文件备份
                        If Me.doBackupFiles_FJ(strErrMsg, objFTPProperty, objOldData) = False Then
                            GoTo rollDatabaseAndFile
                        End If

                        '保存新数据
                        Dim strBasePath As String = Xydc.Platform.Common.Data.grswMyJiaotanData.FILEDIR_FJ
                        Dim blnExisted As Boolean
                        Dim strOldFile As String
                        Dim strLocFile As String
                        Dim strNewFile As String
                        Dim strToUrl As String
                        Dim strUrl As String
                        Dim intCount As Integer
                        Dim i As Integer
                        With objNewData.Tables(strTable)
                            intCount = .DefaultView.Count
                            For i = 0 To intCount - 1 Step 1
                                '获取原FTP路径和新本地文件路径
                                strOldFile = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_FUJIAN_WJWZ), "")
                                strLocFile = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_FUJIAN_BDWJ), "")
                                strNewFile = ""
                                '上传文件
                                If strLocFile <> "" Then
                                    '文件存在?
                                    If objBaseLocalFile.doFileExisted(strErrMsg, strLocFile, blnExisted) = False Then
                                        GoTo rollDatabaseAndFile
                                    End If
                                    If blnExisted = True Then
                                        '获取FTP文件路径
                                        If Me.getFTPFileName_FJ(strErrMsg, strLocFile, intWJND, strWJBS, i + 1, strBasePath, strNewFile) = False Then
                                            GoTo rollDatabaseAndFile
                                        End If
                                        '有本地文件，则需要上载
                                        With objFTPProperty
                                            strUrl = .getUrl(strNewFile)
                                            If objBaseFTP.doPutFile(strErrMsg, strLocFile, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword) = False Then
                                                GoTo rollDatabaseAndFile
                                            End If
                                        End With
                                    Else
                                        strErrMsg = "错误：[" + strLocFile + "]不存在！"
                                        GoTo rollDatabaseAndFile
                                    End If
                                Else
                                    If strOldFile <> "" Then
                                        '
                                        '未从FTP服务器下载
                                        '
                                        '从备份文件恢复到当前行的文件
                                        With objFTPProperty
                                            strUrl = .getUrl(strOldFile + strBakExt)
                                            If objBaseFTP.isFileExisted(strErrMsg, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword, blnExisted) = False Then
                                                '可以不成功
                                            Else
                                                If blnExisted = True Then
                                                    '获取FTP文件路径
                                                    If Me.getFTPFileName_FJ(strErrMsg, strOldFile, intWJND, strWJBS, i + 1, strBasePath, strNewFile) = False Then
                                                        GoTo rollDatabaseAndFile
                                                    End If
                                                    strToUrl = .getUrl(strNewFile)
                                                    '更改文件名
                                                    If objBaseFTP.doRenameFile(strErrMsg, strUrl, strToUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword) = False Then
                                                        GoTo rollDatabaseAndFile
                                                    End If
                                                End If
                                            End If
                                        End With
                                    Else
                                        '没有电子文件
                                    End If
                                End If

                                '写数据
                                strSQL = ""
                                strSQL = strSQL + " insert into 公共_B_交谈_附件 (" + vbCr
                                strSQL = strSQL + "   文件标识, 序号, 说明, 页数, 位置" + vbCr
                                strSQL = strSQL + " ) values (" + vbCr
                                strSQL = strSQL + "   @wjbs, @wjxh, @wjsm, @wjys, @wjwz" + vbCr
                                strSQL = strSQL + " )" + vbCr
                                objSqlCommand.Parameters.Clear()
                                objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                                objSqlCommand.Parameters.AddWithValue("@wjxh", (i + 1))
                                objSqlCommand.Parameters.AddWithValue("@wjsm", objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_FUJIAN_WJSM), ""))
                                objSqlCommand.Parameters.AddWithValue("@wjys", objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_FUJIAN_WJYS), 0))
                                objSqlCommand.Parameters.AddWithValue("@wjwz", strNewFile)
                                objSqlCommand.CommandText = strSQL
                                objSqlCommand.ExecuteNonQuery()
                            Next
                        End With

                        '删除所有备份文件
                        If blnNewTrans = True Then
                            If Me.doDeleteBackupFiles_FJ(strErrMsg, objFTPProperty, objOldData) = False Then
                                '可以不成功，形成垃圾文件！
                            End If
                        End If

                    Catch ex As Exception
                        strErrMsg = ex.Message
                        GoTo rollDatabaseAndFile
                    End Try

                Catch ex As Exception
                    GoTo rollDatabase
                End Try

                '提交事务
                If blnNewTrans = True Then
                    objSqlTransaction.Commit()
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            If blnNewTrans = True Then
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            End If

            '返回
            doSaveFujian = True
            Exit Function

rollDatabaseAndFile:
            If blnNewTrans = True Then
                objSqlTransaction.Rollback()
                If Me.doRestoreFiles_FJ(strSQL, strWJBS, intWJND, objFTPProperty, objNewData, objOldData) = False Then
                    '无法恢复成功，尽力了！
                End If
            End If
            GoTo errProc

rollDatabase:
            If blnNewTrans = True Then
                objSqlTransaction.Rollback()
            End If
            GoTo errProc

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            If blnNewTrans = True Then
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            End If
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据文件标识获取交谈的附件信息
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWJBS              ：文件标识
        '     objJiaotanDataSet    ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getFujianDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWJBS As String, _
            ByRef objJiaotanDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objTempJiaotanDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '初始化
            getFujianDataSet = False
            objJiaotanDataSet = Nothing
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
                If strWJBS Is Nothing Then strWJBS = ""
                strWJBS = strWJBS.Trim

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                Try
                    '创建数据集
                    objTempJiaotanDataSet = New Xydc.Platform.Common.Data.grswMyJiaotanData(Xydc.Platform.Common.Data.grswMyJiaotanData.enumTableType.GG_B_JIAOTAN_FUJIAN)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from 公共_B_交谈_附件 a " + vbCr
                        strSQL = strSQL + " where a.文件标识 = '" + strWJBS + "'" + vbCr
                        strSQL = strSQL + " order by a.序号" + vbCr
                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempJiaotanDataSet.Tables(Xydc.Platform.Common.Data.grswMyJiaotanData.TABLE_GG_B_JIAOTAN_FUJIAN))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempJiaotanDataSet.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.grswMyJiaotanData.SafeRelease(objTempJiaotanDataSet)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objJiaotanDataSet = objTempJiaotanDataSet
            getFujianDataSet = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.grswMyJiaotanData.SafeRelease(objTempJiaotanDataSet)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据文件标识、序号获取交谈的附件信息
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWJBS              ：文件标识
        '     strWJXH              ：序号
        '     objJiaotanDataSet    ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getFujianDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWJBS As String, _
            ByVal strWJXH As String, _
            ByRef objJiaotanDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objTempJiaotanDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '初始化
            getFujianDataSet = False
            objJiaotanDataSet = Nothing
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
                If strWJBS Is Nothing Then strWJBS = ""
                strWJBS = strWJBS.Trim
                If strWJXH Is Nothing Then strWJXH = ""
                strWJXH = strWJXH.Trim
                If strWJXH = "" Then strWJXH = "0"

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                Try
                    '创建数据集
                    objTempJiaotanDataSet = New Xydc.Platform.Common.Data.grswMyJiaotanData(Xydc.Platform.Common.Data.grswMyJiaotanData.enumTableType.GG_B_JIAOTAN_FUJIAN)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from 公共_B_交谈_附件 a " + vbCr
                        strSQL = strSQL + " where a.文件标识 = '" + strWJBS + "'" + vbCr
                        strSQL = strSQL + " and   a.序号     =  " + strWJXH + " " + vbCr
                        strSQL = strSQL + " order by a.序号" + vbCr
                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempJiaotanDataSet.Tables(Xydc.Platform.Common.Data.grswMyJiaotanData.TABLE_GG_B_JIAOTAN_FUJIAN))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempJiaotanDataSet.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.grswMyJiaotanData.SafeRelease(objTempJiaotanDataSet)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objJiaotanDataSet = objTempJiaotanDataSet
            getFujianDataSet = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.grswMyJiaotanData.SafeRelease(objTempJiaotanDataSet)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 保存交谈数据记录(整个事务完成)
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserId              ：用户标识
        '     strPassword            ：用户密码
        '     objNewData             ：记录新值(返回保存后的新值)
        '     objOldData             ：记录旧值
        '     objenumEditType        ：编辑类型
        '     objNewFJData           ：要保存的附件数据
        '     objConnectionProperty  ：FTP连接参数
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function doSaveData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType, _
            ByVal objNewFJData As Xydc.Platform.Common.Data.grswMyJiaotanData, _
            ByVal objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection

            Dim objOldFJData As Xydc.Platform.Common.Data.grswMyJiaotanData
            Dim intWJND As Integer = Year(Now)
            Dim strWJBS As String
            Dim strSQL As String

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            doSaveData = False

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
                If objFTPProperty Is Nothing Then
                    strErrMsg = "错误：没有指定FTP连接参数！"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim

                '检查发文主记录
                If Me.doVerifyData(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType) = False Then
                    GoTo errProc
                End If

                '获取连接事务
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取原附件数据
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew, _
                        Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eCpyNew
                        objOldFJData = Nothing
                    Case Else
                        strWJBS = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_WYBS), "")
                        If Me.getFujianDataSet(strErrMsg, strUserId, strPassword, strWJBS, objOldFJData) = False Then
                            GoTo errProc
                        End If
                End Select

                '开始事务
                objSqlTransaction = objSqlConnection.BeginTransaction

                '执行事务
                Try
                    '保存发文主记录
                    If Me.doSaveData(strErrMsg, objSqlTransaction, objOldData, objNewData, objenumEditType) = False Then
                        GoTo rollDatabase
                    End If

                    '设置新文件标识
                    strWJBS = objNewData(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_WYBS)

                    '保存附件文件
                    If Me.doSaveFujian(strErrMsg, strUserId, strPassword, strWJBS, intWJND, objSqlTransaction, objFTPProperty, objNewFJData, objOldFJData) = False Then
                        GoTo rollDatabaseAndFJFile
                    End If

                    '清除备份文件
                    If Me.doDeleteBackupFiles_FJ(strErrMsg, objFTPProperty, objOldFJData) = False Then
                        '可以不成功，形成垃圾文件
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
            Xydc.Platform.Common.Data.grswMyJiaotanData.SafeRelease(objOldFJData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            doSaveData = True
            Exit Function

rollDatabaseAndFJFile:
            objSqlTransaction.Rollback()
            If Me.doRestoreFiles_FJ(strSQL, strWJBS, intWJND, objFTPProperty, objNewFJData, objOldFJData) = False Then
                '已经尽力了！
            End If
            GoTo errProc

rollDatabase:
            objSqlTransaction.Rollback()
            GoTo errProc

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Data.grswMyJiaotanData.SafeRelease(objOldFJData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

    End Class

End Namespace
