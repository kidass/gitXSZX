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
    ' 类名    ：dacDianzigonggao
    '
    ' 功能描述：
    '     提供对“电子公告”模块涉及的数据层操作
    '----------------------------------------------------------------

    Public Class dacDianzigonggao
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.DataAccess.dacDianzigonggao)
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
        ' 获取[操作员代码=strCzydm]的电子公告数据（按“日期”降序），即
        ' 我负责发布的电子公告数据
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     strCzydm                    ：当前操作员标识
        '     strWhere                    ：搜索字符串
        '     objDianzigonggaoData        ：信息数据集
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strCzydm As String, _
            ByVal strWhere As String, _
            ByRef objDianzigonggaoData As Xydc.Platform.Common.Data.ggxxDianzigonggaoData) As Boolean

            Dim objTempDianzigonggaoData As Xydc.Platform.Common.Data.ggxxDianzigonggaoData
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            Dim objdacCustomer As New Xydc.Platform.DataAccess.dacCustomer
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '初始化
            getDataSet = False
            objDianzigonggaoData = Nothing
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
                If strCzydm Is Nothing Then strCzydm = ""
                strCzydm = strCzydm.Trim
                If strCzydm = "" Then
                    strErrMsg = "错误：未指定[发布人]！"
                    GoTo errProc
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取“人员名称”
                Dim strUserXM As String
                If objdacCustomer.getRymcByRydm(strErrMsg, objSqlConnection, strUserId, strUserXM) = False Then
                    GoTo errProc
                End If
                If strUserXM = "" Then
                    strErrMsg = "错误：发布人[" + strUserId + "]的标识不存在！"
                    GoTo errProc
                End If

                '获取数据
                Try
                    '创建数据集
                    objTempDianzigonggaoData = New Xydc.Platform.Common.Data.ggxxDianzigonggaoData(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.enumTableType.GR_B_GONGGAOLAN)

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
                        strSQL = strSQL + "   select a.*," + vbCr
                        strSQL = strSQL + "     是否阅读 = case when b.操作员代码 is null then '" + strFalse + "' else '" + strTrue + "' end," + vbCr
                        strSQL = strSQL + "     发布描述 = case when isnull(a.发布标识,0) = 0 then '" + strFalse + "' else '" + strTrue + "' end" + vbCr
                        strSQL = strSQL + "   from" + vbCr
                        strSQL = strSQL + "   ("
                        strSQL = strSQL + "     select *" + vbCr
                        strSQL = strSQL + "     from 个人_B_公告栏" + vbCr
                        strSQL = strSQL + "     where 操作员代码 = @czydm" + vbCr
                        strSQL = strSQL + "   ) a" + vbCr
                        strSQL = strSQL + "   left join " + vbCr
                        strSQL = strSQL + "   (" + vbCr
                        strSQL = strSQL + "     select *" + vbCr
                        strSQL = strSQL + "     from 个人_B_公告栏阅读情况" + vbCr
                        strSQL = strSQL + "     where 阅读人员 = @ydry" + vbCr
                        strSQL = strSQL + "   ) b on a.操作员代码 = b.操作员代码 and a.序号 = b.序号" + vbCr
                        strSQL = strSQL + " ) a" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.日期 desc " + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@czydm", strCzydm)
                        objSqlCommand.Parameters.AddWithValue("@ydry", strUserXM)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempDianzigonggaoData.Tables(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.TABLE_GR_B_GONGGAOLAN))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDianzigonggaoData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.ggxxDianzigonggaoData.SafeRelease(objTempDianzigonggaoData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objDianzigonggaoData = objTempDianzigonggaoData
            getDataSet = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.ggxxDianzigonggaoData.SafeRelease(objTempDianzigonggaoData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取[操作员代码=strCzydm、序号=intXH]的电子公告数据
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     strCzydm                    ：当前操作员标识
        '     intXH                       ：公告序号
        '     objDianzigonggaoData        ：信息数据集
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strCzydm As String, _
            ByVal intXH As Integer, _
            ByRef objDianzigonggaoData As Xydc.Platform.Common.Data.ggxxDianzigonggaoData) As Boolean

            Dim objTempDianzigonggaoData As Xydc.Platform.Common.Data.ggxxDianzigonggaoData
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            Dim objdacCustomer As New Xydc.Platform.DataAccess.dacCustomer
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '初始化
            getDataSet = False
            objDianzigonggaoData = Nothing
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
                If strCzydm Is Nothing Then strCzydm = ""
                strCzydm = strCzydm.Trim

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取“人员名称”
                Dim strUserXM As String
                If objdacCustomer.getRymcByRydm(strErrMsg, objSqlConnection, strUserId, strUserXM) = False Then
                    GoTo errProc
                End If
                If strUserXM = "" Then
                    strErrMsg = "错误：发布人[" + strUserId + "]的标识不存在！"
                    GoTo errProc
                End If

                '获取数据
                Try
                    '创建数据集
                    objTempDianzigonggaoData = New Xydc.Platform.Common.Data.ggxxDianzigonggaoData(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.enumTableType.GR_B_GONGGAOLAN)

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
                        strSQL = strSQL + "   select a.*," + vbCr
                        strSQL = strSQL + "     是否阅读 = case when b.操作员代码 is null then '" + strFalse + "' else '" + strTrue + "' end," + vbCr
                        strSQL = strSQL + "     发布描述 = case when isnull(a.发布标识,0) = 0 then '" + strFalse + "' else '" + strTrue + "' end" + vbCr
                        strSQL = strSQL + "   from" + vbCr
                        strSQL = strSQL + "   ("
                        strSQL = strSQL + "     select *" + vbCr
                        strSQL = strSQL + "     from 个人_B_公告栏" + vbCr
                        strSQL = strSQL + "     where 操作员代码 = @czydm" + vbCr
                        strSQL = strSQL + "     and   序号 = @xh" + vbCr
                        strSQL = strSQL + "   ) a" + vbCr
                        strSQL = strSQL + "   left join " + vbCr
                        strSQL = strSQL + "   (" + vbCr
                        strSQL = strSQL + "     select *" + vbCr
                        strSQL = strSQL + "     from 个人_B_公告栏阅读情况" + vbCr
                        strSQL = strSQL + "     where 操作员代码 = @czydm" + vbCr
                        strSQL = strSQL + "     and   序号 = @xh" + vbCr
                        strSQL = strSQL + "     and   阅读人员 = @ydry" + vbCr
                        strSQL = strSQL + "   ) b on a.操作员代码 = b.操作员代码 and a.序号 = b.序号" + vbCr
                        strSQL = strSQL + " ) a" + vbCr
                        strSQL = strSQL + " order by a.日期 desc " + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@czydm", strCzydm)
                        objSqlCommand.Parameters.AddWithValue("@xh", intXH)
                        objSqlCommand.Parameters.AddWithValue("@ydry", strUserXM)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempDianzigonggaoData.Tables(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.TABLE_GR_B_GONGGAOLAN))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDianzigonggaoData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.ggxxDianzigonggaoData.SafeRelease(objTempDianzigonggaoData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objDianzigonggaoData = objTempDianzigonggaoData
            getDataSet = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.ggxxDianzigonggaoData.SafeRelease(objTempDianzigonggaoData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取strUserId的能够阅读的已发布的电子公告数据（按“日期”降序），即
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     strWhere                    ：搜索字符串
        '     objDianzigonggaoData        ：信息数据集
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objDianzigonggaoData As Xydc.Platform.Common.Data.ggxxDianzigonggaoData) As Boolean

            Dim objTempDianzigonggaoData As Xydc.Platform.Common.Data.ggxxDianzigonggaoData
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            Dim objdacCustomer As New Xydc.Platform.DataAccess.dacCustomer
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '初始化
            getDataSet = False
            objDianzigonggaoData = Nothing
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

                '获取“人员名称”
                Dim strUserXM As String
                If objdacCustomer.getRymcByRydm(strErrMsg, objSqlConnection, strUserId, strUserXM) = False Then
                    GoTo errProc
                End If
                If strUserXM = "" Then
                    strErrMsg = "错误：发布人[" + strUserId + "]的标识不存在！"
                    GoTo errProc
                End If

                '获取数据
                Try
                    '创建数据集
                    objTempDianzigonggaoData = New Xydc.Platform.Common.Data.ggxxDianzigonggaoData(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.enumTableType.GR_B_GONGGAOLAN)

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
                        strSQL = strSQL + "   select a.*," + vbCr
                        strSQL = strSQL + "     是否阅读 = case when b.操作员代码 is null then '" + strFalse + "' else '" + strTrue + "' end," + vbCr
                        strSQL = strSQL + "     发布描述 = case when isnull(a.发布标识,0) = 0 then '" + strFalse + "' else '" + strTrue + "' end" + vbCr
                        strSQL = strSQL + "   from" + vbCr
                        strSQL = strSQL + "   ("
                        strSQL = strSQL + "     select *" + vbCr
                        strSQL = strSQL + "     from 个人_B_公告栏" + vbCr
                        strSQL = strSQL + "     where 发布标识 = 1" + vbCr  '已发布
                        strSQL = strSQL + "   ) a" + vbCr
                        strSQL = strSQL + "   left join " + vbCr
                        strSQL = strSQL + "   (" + vbCr
                        strSQL = strSQL + "     select *" + vbCr
                        strSQL = strSQL + "     from 个人_B_公告栏阅读情况" + vbCr
                        strSQL = strSQL + "     where 阅读人员 = @ydry" + vbCr
                        strSQL = strSQL + "   ) b on a.操作员代码 = b.操作员代码 and a.序号 = b.序号" + vbCr
                        strSQL = strSQL + "   left join " + vbCr
                        strSQL = strSQL + "   (" + vbCr
                        strSQL = strSQL + "     select *" + vbCr
                        strSQL = strSQL + "     from 个人_B_公告栏阅读范围" + vbCr
                        strSQL = strSQL + "     where 阅读人员 = @ydry" + vbCr
                        strSQL = strSQL + "   ) c on a.操作员代码 = c.操作员代码 and a.序号 = c.序号" + vbCr
                        strSQL = strSQL + "   where ((isnull(a.阅读控制,0) = 0) or (isnull(a.阅读控制,0) = 1 and c.操作员代码 is not null) or (a.操作员 = '" + strUserXM + "'))" '能阅读
                        strSQL = strSQL + " ) a" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.日期 desc " + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@ydry", strUserXM)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempDianzigonggaoData.Tables(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.TABLE_GR_B_GONGGAOLAN))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDianzigonggaoData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.ggxxDianzigonggaoData.SafeRelease(objTempDianzigonggaoData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objDianzigonggaoData = objTempDianzigonggaoData
            getDataSet = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.ggxxDianzigonggaoData.SafeRelease(objTempDianzigonggaoData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取[操作员代码=strCzydm、序号=intXH]的电子公告的限制阅读人员数据
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     strCzydm                    ：当前操作员标识
        '     intXH                       ：公告序号
        '     strYDRY                     ：（返回）限制阅读人员数据
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getKeYueduRenyuan( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strCzydm As String, _
            ByVal intXH As Integer, _
            ByRef strYDRY As String) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim strSQL As String

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet

            '初始化
            getKeYueduRenyuan = False
            strErrMsg = ""
            strYDRY = ""

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
                If strCzydm Is Nothing Then strCzydm = ""
                strCzydm = strCzydm.Trim
                If strCzydm = "" Then
                    strErrMsg = "错误：未指定[发布人]！"
                    GoTo errProc
                End If
                If intXH <= 0 Then
                    strErrMsg = "错误：未指定有效的[公告序号]！"
                    GoTo errProc
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据集
                strSQL = ""
                strSQL = strSQL + " select * from 个人_B_公告栏阅读范围" + vbCr
                strSQL = strSQL + " where 操作员代码 = '" + strCzydm + "'" + vbCr
                strSQL = strSQL + " and   序号       =  " + intXH.ToString + "" + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If

                '计算
                If objDataSet.Tables.Count > 0 Then
                    If Not (objDataSet.Tables(0) Is Nothing) Then
                        Dim strTemp As String = ""
                        Dim intCount As Integer
                        Dim i As Integer
                        With objDataSet.Tables(0)
                            intCount = .Rows.Count
                            For i = 0 To intCount - 1 Step 1
                                strTemp = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_YUEDUFANWEI_YDRY), "")
                                If strTemp <> "" Then
                                    If strYDRY = "" Then
                                        strYDRY = strTemp
                                    Else
                                        strYDRY = strYDRY + objPulicParameters.CharSeparate + strTemp
                                    End If
                                End If
                            Next
                        End With
                    End If
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            getKeYueduRenyuan = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function





        '----------------------------------------------------------------
        ' 取消已发布的电子公告 或 发布电子公告
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strCzydm             ：发布人代码
        '     intXH                ：公告序号
        '     blnFabu              ：True-发布，False-取消发布
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doFabu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strCzydm As String, _
            ByVal intXH As Integer, _
            ByVal blnFabu As Boolean) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '初始化
            doFabu = False
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
                If strCzydm Is Nothing Then strCzydm = ""
                strCzydm = strCzydm.Trim
                If strCzydm = "" Then
                    strErrMsg = "错误：未指定[发布人]！"
                    GoTo errProc
                End If
                If intXH <= 0 Then
                    strErrMsg = "错误：未指定[公告序号]！"
                    GoTo errProc
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '开始事务
                objSqlTransaction = objSqlConnection.BeginTransaction

                '发布/取消发布
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '计算SQL
                    objSqlCommand.Parameters.Clear()
                    If blnFabu = True Then
                        strSQL = ""
                        strSQL = strSQL + " update 个人_B_公告栏 set" + vbCr
                        strSQL = strSQL + "   发布标识 = 1," + vbCr
                        strSQL = strSQL + "   日期 = @rq" + vbCr
                        strSQL = strSQL + " where 操作员代码 = @czydm" + vbCr
                        strSQL = strSQL + " and   序号 = @xh" + vbCr
                        strSQL = strSQL + " and   发布标识 <> 1" + vbCr
                        objSqlCommand.Parameters.AddWithValue("@rq", Now)
                        objSqlCommand.Parameters.AddWithValue("@czydm", strCzydm)
                        objSqlCommand.Parameters.AddWithValue("@xh", intXH)
                    Else
                        strSQL = ""
                        strSQL = strSQL + " update 个人_B_公告栏 set" + vbCr
                        strSQL = strSQL + "   发布标识 = 0" + vbCr
                        strSQL = strSQL + " where 操作员代码 = @czydm" + vbCr
                        strSQL = strSQL + " and   序号 = @xh" + vbCr
                        strSQL = strSQL + " and   发布标识 <> 0" + vbCr
                        objSqlCommand.Parameters.AddWithValue("@czydm", strCzydm)
                        objSqlCommand.Parameters.AddWithValue("@xh", intXH)
                    End If

                    '执行
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
            doFabu = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 设置“已经阅读”
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strCzydm             ：发布人代码
        '     intXH                ：公告序号
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doSetHasRead( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strCzydm As String, _
            ByVal intXH As Integer) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCustomer As New Xydc.Platform.DataAccess.dacCustomer
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '初始化
            doSetHasRead = False
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
                If strCzydm Is Nothing Then strCzydm = ""
                strCzydm = strCzydm.Trim
                If strCzydm = "" Then
                    strErrMsg = "错误：未指定[发布人]！"
                    GoTo errProc
                End If
                If intXH <= 0 Then
                    strErrMsg = "错误：未指定[公告序号]！"
                    GoTo errProc
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取“人员名称”
                Dim strUserXM As String
                If objdacCustomer.getRymcByRydm(strErrMsg, objSqlConnection, strUserId, strUserXM) = False Then
                    GoTo errProc
                End If
                If strUserXM = "" Then
                    strErrMsg = "错误：发布人[" + strUserId + "]的标识不存在！"
                    GoTo errProc
                End If

                '开始事务
                objSqlTransaction = objSqlConnection.BeginTransaction

                '设置已经阅读
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '清除阅读记录
                    strSQL = ""
                    strSQL = strSQL + " delete from 个人_B_公告栏阅读情况" + vbCr
                    strSQL = strSQL + " where 操作员代码 = @czydm" + vbCr
                    strSQL = strSQL + " and   序号       = @xh" + vbCr
                    strSQL = strSQL + " and   阅读人员   = @ydry" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@czydm", strCzydm)
                    objSqlCommand.Parameters.AddWithValue("@xh", intXH)
                    objSqlCommand.Parameters.AddWithValue("@ydry", strUserXM)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '设置阅读记录
                    strSQL = ""
                    strSQL = strSQL + " insert into 个人_B_公告栏阅读情况 (" + vbCr
                    strSQL = strSQL + "   操作员代码,序号,阅读人员" + vbCr
                    strSQL = strSQL + " ) values (" + vbCr
                    strSQL = strSQL + "   @czydm,@xh,@ydry" + vbCr
                    strSQL = strSQL + " )" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@czydm", strCzydm)
                    objSqlCommand.Parameters.AddWithValue("@xh", intXH)
                    objSqlCommand.Parameters.AddWithValue("@ydry", strUserXM)
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
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doSetHasRead = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 删除电子公告
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strCzydm             ：发布人代码
        '     intXH                ：公告序号
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDelete( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strCzydm As String, _
            ByVal intXH As Integer) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseFTP As New Xydc.Platform.Common.Utilities.BaseFTP
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objdacXitongpeizhi As New Xydc.Platform.DataAccess.dacXitongpeizhi
            Dim objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            Dim objDataSet As Xydc.Platform.Common.Data.ggxxDianzigonggaoData
            Dim objDataSet_FJ As System.Data.DataSet
            Dim strZWNR As String = ""
            Dim strSQL As String
            Dim strWJBS As String
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
                strCzydm = strCzydm.Trim
                If strCzydm = "" Then
                    strErrMsg = "错误：未指定[发布人]！"
                    GoTo errProc
                End If
                If intXH <= 0 Then
                    strErrMsg = "错误：未指定[公告序号]！"
                    GoTo errProc
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取公告数据


                If Me.getDataSet(strErrMsg, strUserId, strPassword, strCzydm, intXH, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables.Count < 1 Then
                    strErrMsg = "错误：无法获取公告数据！"
                    GoTo errProc
                End If
                If objDataSet.Tables(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.TABLE_GR_B_GONGGAOLAN) Is Nothing Then
                    strErrMsg = "错误：无法获取公告数据！"
                    GoTo errProc
                End If
                If objDataSet.Tables(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.TABLE_GR_B_GONGGAOLAN).Rows.Count < 1 Then
                    strErrMsg = "错误：无法获取公告数据！"
                    GoTo errProc
                End If
                With objDataSet.Tables(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.TABLE_GR_B_GONGGAOLAN).Rows(0)
                    strZWNR = objPulicParameters.getObjectValue(.Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_ZWNR), "")
                    strWJBS = objPulicParameters.getObjectValue(.Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_WJBS), "")
                End With
                If Not (objDataSet Is Nothing) Then
                    Xydc.Platform.Common.Data.ggxxDianzigonggaoData.SafeRelease(objDataSet)
                End If

                '获取附件列表
                strSQL = "select * from 电子公告_B_附件 where 文件标识 = '" & strWJBS & "'"
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet_FJ) = False Then
                    GoTo errProc
                End If

                '获取FTP连接参数
                If objdacXitongpeizhi.getFtpServerParam(strErrMsg, objSqlConnection, objFTPProperty) = False Then
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

                    '删除“个人_B_公告栏阅读范围”信息
                    strSQL = ""
                    strSQL = strSQL + " delete from 个人_B_公告栏阅读范围 " + vbCr
                    strSQL = strSQL + " where 操作员代码 = @czydm" + vbCr
                    strSQL = strSQL + " and   序号       = @xh" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@czydm", strCzydm)
                    objSqlCommand.Parameters.AddWithValue("@xh", intXH)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '删除“个人_B_公告栏阅读情况”信息
                    strSQL = ""
                    strSQL = strSQL + " delete from 个人_B_公告栏阅读情况 " + vbCr
                    strSQL = strSQL + " where 操作员代码 = @czydm" + vbCr
                    strSQL = strSQL + " and   序号       = @xh" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@czydm", strCzydm)
                    objSqlCommand.Parameters.AddWithValue("@xh", intXH)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '删除“个人_B_公告栏”信息
                    strSQL = ""
                    strSQL = strSQL + " delete from 个人_B_公告栏 " + vbCr
                    strSQL = strSQL + " where 操作员代码 = @czydm" + vbCr
                    strSQL = strSQL + " and   序号       = @xh" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@czydm", strCzydm)
                    objSqlCommand.Parameters.AddWithValue("@xh", intXH)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '删除“正文内容”对应文件数据
                    Dim strFilePath As String
                    Dim strUrl As String
                    strFilePath = strZWNR
                    If strFilePath <> "" Then
                        With objFTPProperty
                            strUrl = .getUrl(strFilePath)
                            If objBaseFTP.doDeleteFile(strErrMsg, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword) = False Then
                                '可以不成功，形成垃圾文件！
                            End If
                        End With
                    End If

                    '删除附件信息
                    '删除对应的FTP文件
                    Dim intcount As Integer
                    Dim i As Integer
                    With objDataSet_FJ.Tables(0)
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
                    objDataSet_FJ.Dispose()
                    objDataSet_FJ = Nothing

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
            Xydc.Platform.Common.Data.ggxxDianzigonggaoData.SafeRelease(objDataSet)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doDelete = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacXitongpeizhi.SafeRelease(objdacXitongpeizhi)
            Xydc.Platform.Common.Utilities.FTPProperty.SafeRelease(objFTPProperty)
            Xydc.Platform.Common.Data.ggxxDianzigonggaoData.SafeRelease(objDataSet)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 检查“个人_B_公告栏”的数据的合法性
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
                strSQL = "select top 0 * from 个人_B_公告栏"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, strUserId, strPassword, strSQL, "个人_B_公告栏", objDataSet) = False Then
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
                        Case Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_SFYD, _
                            Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_FBMS
                            '计算列

                        Case Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_WJBS
                            '系统自动给定值
                            If strValue = "" Then
                                If objdacCommon.getNewGUID(strErrMsg, strUserId, strPassword, strValue) = False Then
                                    GoTo errProc
                                End If
                            End If

                        Case Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_XH
                            '随后检查

                        Case Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_RQ
                            If strValue = "" Then
                                strValue = Format(Now, "yyyy-MM-dd HH:mm:ss")
                            End If
                            If objPulicParameters.isDatetimeString(strValue) = False Then
                                strErrMsg = "错误：[" + strField + "]输入无效的日期！"
                                GoTo errProc
                            End If
                            strValue = Format(CType(strValue, System.DateTime), "yyyy-MM-dd HH:mm:ss")

                        Case Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_BLRQ
                            If strValue <> "" Then
                                If objPulicParameters.isDatetimeString(strValue) = False Then
                                    strErrMsg = "错误：[" + strField + "]输入无效的日期！"
                                    GoTo errProc
                                End If
                                strValue = Format(CType(strValue, System.DateTime), "yyyy-MM-dd HH:mm:ss")
                            End If

                        Case Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_CZYDM, _
                            Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_ZZDM, _
                            Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_BT, _
                            Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_ZZMC

                            If strValue = "" Then
                                If strField = Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_ZZMC Then
                                    strErrMsg = "错误：[发布单位]不能为空！"
                                    GoTo errProc
                                Else
                                    strErrMsg = "错误：[" + strField + "]不能为空！"
                                    GoTo errProc
                                End If

                            End If
                            With objDataSet.Tables(0).Columns(strField)
                                intLen = objPulicParameters.getStringLength(strValue)
                                If intLen > .MaxLength Then
                                    strErrMsg = "错误：[" + strField + "]长度不能超过[" + .MaxLength.ToString() + "]，实际有[" + intLen.ToString() + "]！"
                                    GoTo errProc
                                End If
                            End With

                        Case Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_NR
                            If strValue = "" Then
                                strErrMsg = "错误：[" + strField + "]不能为空！"
                                GoTo errProc
                            End If
                            strValue = objNewData.Item(i).TrimEnd(" ".ToCharArray)
                            With objDataSet.Tables(0).Columns(strField)
                                intLen = objPulicParameters.getStringLength(strValue)
                                If intLen > .MaxLength Then
                                    strErrMsg = "错误：[" + strField + "]长度不能超过[" + .MaxLength.ToString() + "]，实际有[" + intLen.ToString() + "]！"
                                    GoTo errProc
                                End If
                            End With

                        Case Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_FBBS, _
                            Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_YDKZ
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

                '检查“序号”
                Dim strCZYDM As String
                Dim strXH As String
                strCZYDM = objNewData.Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_CZYDM).Trim()
                strXH = objNewData.Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_XH).Trim()
                If strXH = "" Then
                    '自动产生序号
                    If objdacCommon.getNewCode(strErrMsg, objSqlConnection, "序号", "操作员代码", strCZYDM, "个人_B_公告栏", True, strXH) = False Then
                        GoTo errProc
                    End If
                    objNewData.Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_XH) = strXH
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
        ' 保存“个人_B_公告栏”的数据(现有事务)
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
                                    Case Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_SFYD, _
                                        Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_FBMS
                                        '计算列
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
                            strSQL = strSQL + " insert into 个人_B_公告栏 (" + strFileds + ")"
                            strSQL = strSQL + " values (" + strValues + ")"
                            '准备参数
                            objSqlCommand.Parameters.Clear()
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_SFYD, _
                                        Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_FBMS
                                        '计算列
                                    Case Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_RQ, _
                                        Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_BLRQ
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), System.DBNull.Value)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(objNewData.Item(i), System.DateTime))
                                        End If
                                    Case Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_XH, _
                                        Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_FBBS
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), 0)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(objNewData.Item(i), System.Int32))
                                        End If
                                    Case Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_YDKZ
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), "0")
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), objNewData.Item(i))
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
                            Dim strOldCZYDM As String
                            Dim intOldXH As Integer
                            strOldCZYDM = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_CZYDM), "")
                            intOldXH = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_XH), 0)
                            '计算更新字段列表
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_SFYD, _
                                        Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_FBMS
                                        '计算列
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
                            strSQL = strSQL + " update 个人_B_公告栏 set " + vbCr
                            strSQL = strSQL + "   " + strFileds + vbCr
                            strSQL = strSQL + " where 操作员代码 = @oldczydm" + vbCr
                            strSQL = strSQL + " and   序号       = @oldxh" + vbCr
                            '准备参数
                            objSqlCommand.Parameters.Clear()
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_SFYD, _
                                        Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_FBMS
                                        '计算列
                                    Case Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_RQ, _
                                        Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_BLRQ
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), System.DBNull.Value)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(objNewData.Item(i), System.DateTime))
                                        End If
                                    Case Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_XH, _
                                        Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_FBBS
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), 0)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(objNewData.Item(i), System.Int32))
                                        End If
                                    Case Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_YDKZ
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), "0")
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), objNewData.Item(i))
                                        End If
                                    Case Else
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), " ")
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), objNewData.Item(i))
                                        End If
                                End Select
                            Next
                            objSqlCommand.Parameters.AddWithValue("@oldczydm", strOldCZYDM)
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
        ' 保存“个人_B_公告栏”的阅读范围数据(现有事务)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objSqlTransaction    ：现有事务
        '     objOldData           ：旧数据
        '     objNewData           ：新数据
        '     strFBFW              ：发布范围(范围、组织、人员)
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
            ByVal strFBFW As String, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            Dim objNewSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objdacCustomer As New Xydc.Platform.DataAccess.dacCustomer
            Dim strRYLIST As String

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
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                    Case Else
                        If objOldData Is Nothing Then
                            strErrMsg = "错误：未传入旧的数据！"
                            GoTo errProc
                        End If
                End Select
                If strFBFW Is Nothing Then strFBFW = ""
                strFBFW = strFBFW.Trim

                '获取连接
                objSqlConnection = objSqlTransaction.Connection

                '解析strFBFW
                If strFBFW = "" Then
                    strRYLIST = ""
                Else
                    '创建临时连接
                    objNewSqlConnection = New System.Data.SqlClient.SqlConnection(objSqlConnection.ConnectionString)
                    objNewSqlConnection.Open()
                    '解析
                    If objdacCustomer.getRenyuanList(strErrMsg, objNewSqlConnection, strFBFW, objPulicParameters.CharSeparate, strRYLIST) = False Then
                        GoTo errProc
                    End If
                End If

                '保存数据
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '删除原有数据
                    Select Case objenumEditType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                        Case Else
                            Dim strOldCZYDM As String
                            Dim intOldXH As Integer
                            strOldCZYDM = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_CZYDM), "")
                            intOldXH = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_XH), 0)
                            strSQL = ""
                            strSQL = strSQL + " delete from 个人_B_公告栏阅读范围" + vbCr
                            strSQL = strSQL + " where 操作员代码 = @czydm" + vbCr
                            strSQL = strSQL + " and   序号       = @xh" + vbCr
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@czydm", strOldCZYDM)
                            objSqlCommand.Parameters.AddWithValue("@xh", intOldXH)
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()
                    End Select

                    '保存现有数据
                    If strRYLIST <> "" Then
                        Dim strNewCZYDM As String
                        Dim intNewXH As Integer
                        strNewCZYDM = objPulicParameters.getObjectValue(objNewData.Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_CZYDM), "")
                        intNewXH = objPulicParameters.getObjectValue(objNewData.Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_XH), 0)

                        Dim strArray() As String
                        Dim intCount As Integer
                        Dim i As Integer
                        strArray = strRYLIST.Split(Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate.ToCharArray)
                        intCount = strArray.Length
                        For i = 0 To intCount - 1 Step 1
                            strSQL = ""
                            strSQL = strSQL + " insert into 个人_B_公告栏阅读范围 (" + vbCr
                            strSQL = strSQL + "   操作员代码,序号,阅读人员" + vbCr
                            strSQL = strSQL + " ) values (" + vbCr
                            strSQL = strSQL + "   @czydm, @xh, @ydry"
                            strSQL = strSQL + " )"
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@czydm", strNewCZYDM)
                            objSqlCommand.Parameters.AddWithValue("@xh", intNewXH)
                            objSqlCommand.Parameters.AddWithValue("@ydry", strArray(i))
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()
                        Next
                    End If

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objNewSqlConnection)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doSave = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objNewSqlConnection)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 保存电子公告数据记录(整个事务完成)
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserId              ：用户标识
        '     strPassword            ：用户密码
        '     objNewData             ：记录新值(返回保存后的新值)
        '     objOldData             ：记录旧值
        '     strFBFW                ：发布范围
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
            ByVal strFBFW As String, _
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
                If strFBFW Is Nothing Then strFBFW = ""
                strFBFW = strFBFW.Trim

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
                    '自动设置“阅读控制”
                    objNewData.Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_YDKZ) = "0"
                    If strFBFW <> "" Then
                        objNewData.Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_YDKZ) = "1"
                    End If

                    '保存主记录
                    If Me.doSave(strErrMsg, objSqlTransaction, objOldData, objNewData, objenumEditType) = False Then
                        GoTo rollDatabase
                    End If

                    '保存现“阅读范围”
                    If Me.doSave(strErrMsg, objSqlTransaction, objOldData, objNewData, strFBFW, objenumEditType) = False Then
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
        ' 保存电子公告数据记录(整个事务完成)
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserId              ：用户标识
        '     strPassword            ：用户密码
        '     objNewData             ：记录新值(返回保存后的新值)
        '     objOldData             ：记录旧值
        '     strFBFW                ：发布范围
        '     objenumEditType        ：编辑类型
        '     objDataSet_FJ          ：附件数据集
        '     objFTPProperty         ：FTP参数
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
            ByVal strFBFW As String, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType, _
            ByVal objDataSet_FJ As Xydc.Platform.Common.Data.ggxxDianzigonggaoData, _
            ByVal objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objOldFJData As Xydc.Platform.Common.Data.ggxxDianzigonggaoData

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim strCZSM As String = Xydc.Platform.Common.Workflow.BaseFlowObject.LOGO_QXBJ
            Dim intWJND As Integer = Year(Now)
            Dim strOldZWNR As String
            Dim strWJBS As String
            Dim strSQL As String

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
                If strFBFW Is Nothing Then strFBFW = ""
                strFBFW = strFBFW.Trim

                '检查主记录
                If Me.doVerify(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType) = False Then
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
                        strWJBS = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_WJBS), "")
                        If Me.getFujianData(strErrMsg, objSqlConnection, strWJBS, objOldFJData) = False Then
                            GoTo errProc
                        End If
                End Select

                '开始事务
                objSqlTransaction = objSqlConnection.BeginTransaction

                '执行事务
                Try
                    '自动设置“阅读控制”
                    objNewData.Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_YDKZ) = "0"
                    If strFBFW <> "" Then
                        objNewData.Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_YDKZ) = "1"
                    End If

                    '保存主记录
                    If Me.doSave(strErrMsg, objSqlTransaction, objOldData, objNewData, objenumEditType) = False Then
                        GoTo rollDatabase
                    End If

                    '保存现“阅读范围”
                    If Me.doSave(strErrMsg, objSqlTransaction, objOldData, objNewData, strFBFW, objenumEditType) = False Then
                        GoTo rollDatabase
                    End If

                    '设置新文件标识
                    strWJBS = objNewData(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_WJBS)

                    '保存附件文件
                    If Me.doSaveFujian(strErrMsg, strWJBS, intWJND, objSqlTransaction, objFTPProperty, objDataSet_FJ, objOldFJData) = False Then
                        GoTo rollGJAndFJFile
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
            Xydc.Platform.Common.Data.ggxxDianzigonggaoData.SafeRelease(objOldFJData)

            doSave = True
            Exit Function

rollGJAndFJFile:
            objSqlTransaction.Rollback()
            If Me.doRestoreFiles_FJ(strSQL, strWJBS, intWJND, objFTPProperty, objDataSet_FJ, objOldFJData) = False Then
                '已经尽力了！
            End If
            GoTo errProc

rollDatabase:
            objSqlTransaction.Rollback()
            GoTo errProc

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Xydc.Platform.Common.Data.ggxxDianzigonggaoData.SafeRelease(objOldFJData)
            Exit Function

        End Function








        '----------------------------------------------------------------
        ' 判断strUserId是否能够阅读的已发布strZcydm+intXH的电子公告数据
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     strCzydm                    ：操作员代码
        '     intXH                       ：公告序号
        '     blnYuedu                    ：（返回）True-能，False-不能
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function isCanRead( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strCzydm As String, _
            ByVal intXH As Integer, _
            ByRef blnYuedu As Boolean) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            Dim objdacCustomer As New Xydc.Platform.DataAccess.dacCustomer
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '初始化
            isCanRead = False
            blnYuedu = False
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
                If strCzydm Is Nothing Then strCzydm = ""
                strCzydm = strCzydm.Trim
                If strCzydm = "" Then
                    Exit Try
                End If
                If intXH < 0 Then
                    Exit Try
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取“人员名称”
                Dim strUserXM As String
                If objdacCustomer.getRymcByRydm(strErrMsg, objSqlConnection, strUserId, strUserXM) = False Then
                    GoTo errProc
                End If
                If strUserXM = "" Then
                    strErrMsg = "错误：发布人[" + strUserId + "]的标识不存在！"
                    GoTo errProc
                End If

                '获取数据
                Dim strFalse As String = Xydc.Platform.Common.Utilities.PulicParameters.CharFalse
                Dim strTrue As String = Xydc.Platform.Common.Utilities.PulicParameters.CharTrue
                '准备SQL
                strSQL = ""
                strSQL = strSQL + " select a.*" + vbCr
                strSQL = strSQL + " from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select a.*," + vbCr
                strSQL = strSQL + "     是否阅读 = case when b.操作员代码 is null then '" + strFalse + "' else '" + strTrue + "' end," + vbCr
                strSQL = strSQL + "     发布描述 = case when isnull(a.发布标识,0) = 0 then '" + strFalse + "' else '" + strTrue + "' end" + vbCr
                strSQL = strSQL + "   from" + vbCr
                strSQL = strSQL + "   ("
                strSQL = strSQL + "     select *" + vbCr
                strSQL = strSQL + "     from 个人_B_公告栏" + vbCr
                strSQL = strSQL + "     where 操作员代码 = '" + strCzydm + "'" + vbCr
                strSQL = strSQL + "     and   序号       =  " + intXH.ToString + vbCr
                strSQL = strSQL + "   ) a" + vbCr
                strSQL = strSQL + "   left join " + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select *" + vbCr
                strSQL = strSQL + "     from 个人_B_公告栏阅读情况" + vbCr
                strSQL = strSQL + "     where 阅读人员 = '" + strUserXM + "'" + vbCr
                strSQL = strSQL + "   ) b on a.操作员代码 = b.操作员代码 and a.序号 = b.序号" + vbCr
                strSQL = strSQL + "   left join " + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select *" + vbCr
                strSQL = strSQL + "     from 个人_B_公告栏阅读范围" + vbCr
                strSQL = strSQL + "     where 阅读人员 = '" + strUserXM + "'" + vbCr
                strSQL = strSQL + "   ) c on a.操作员代码 = c.操作员代码 and a.序号 = c.序号" + vbCr
                strSQL = strSQL + "   where (a.发布标识 = 1 and ((isnull(a.阅读控制,0) = 0) or (isnull(a.阅读控制,0) = 1 and c.操作员代码 is not null))) or (a.操作员 = '" + strUserXM + "')"
                strSQL = strSQL + " ) a" + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    blnYuedu = True
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            isCanRead = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function


        '----------------------------------------------------------------
        ' 根据strWJBSH获取“电子公告_B_附件”的数据集
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     strWJBS                    ：操作员代码
        '     objFujianData        ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getFujianData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWJBS As String, _
            ByRef objFujianData As Xydc.Platform.Common.Data.ggxxDianzigonggaoData) As Boolean

            Dim objTempFujianData As Xydc.Platform.Common.Data.ggxxDianzigonggaoData
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            Dim objdacCustomer As New Xydc.Platform.DataAccess.dacCustomer
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '初始化
            getFujianData = False
            objFujianData = Nothing
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

                '获取“人员名称”
                Dim strUserXM As String
                If objdacCustomer.getRymcByRydm(strErrMsg, objSqlConnection, strUserId, strUserXM) = False Then
                    GoTo errProc
                End If
                If strUserXM = "" Then
                    strErrMsg = "错误：发布人[" + strUserId + "]的标识不存在！"
                    GoTo errProc
                End If

                '获取数据
                Dim strFalse As String = Xydc.Platform.Common.Utilities.PulicParameters.CharFalse
                Dim strTrue As String = Xydc.Platform.Common.Utilities.PulicParameters.CharTrue


                '创建数据集
                objTempFujianData = New Xydc.Platform.Common.Data.ggxxDianzigonggaoData(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.enumTableType.DZGG_B_FUJIAN)

                If strWJBS = "" Then Exit Try

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '执行检索
                With Me.m_objSqlDataAdapter
                    '获取附件数据
                    strSQL = ""
                    strSQL = strSQL + " select a.*" + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select *,"
                    strSQL = strSQL + "     显示序号 = 序号,"
                    strSQL = strSQL + "     本地文件 = '',"
                    strSQL = strSQL + "     下载标志 = 0 " + vbCr
                    strSQL = strSQL + "   from 电子公告_B_附件 " + vbCr
                    strSQL = strSQL + "   where  文件标识 = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + " ) a" + vbCr
                    strSQL = strSQL + " order by a.显示序号" + vbCr

                    '设置参数
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    '执行操作
                    .Fill(objTempFujianData.Tables(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.TABLE_DZGG_B_FUJIAN))
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objFujianData = objTempFujianData
            getFujianData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Xydc.Platform.Common.Data.ggxxDianzigonggaoData.SafeRelease(objTempFujianData)
            Exit Function

        End Function


        '----------------------------------------------------------------
        ' 获取文件的附件信息
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objFujianData        ：返回数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getFujianData( _
            ByRef strErrMsg As String, _
            ByVal objSqlConnection As System.Data.SqlClient.SqlConnection, _
            ByVal strWJBS As String, _
            ByRef objFujianData As Xydc.Platform.Common.Data.ggxxDianzigonggaoData) As Boolean

            Dim objTempFujianData As Xydc.Platform.Common.Data.ggxxDianzigonggaoData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            getFujianData = False
            objFujianData = Nothing
            strErrMsg = ""

            Try
                '获取文件标识


                '创建数据集
                objTempFujianData = New Xydc.Platform.Common.Data.ggxxDianzigonggaoData(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.enumTableType.DZGG_B_FUJIAN)
                If strWJBS = "" Then Exit Try

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '执行检索
                With Me.m_objSqlDataAdapter
                    '获取附件数据
                    strSQL = ""
                    strSQL = strSQL + " select a.*" + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select *,"
                    strSQL = strSQL + "     显示序号 = 序号,"
                    strSQL = strSQL + "     本地文件 = '',"
                    strSQL = strSQL + "     下载标志 = 0 " + vbCr
                    strSQL = strSQL + "   from 电子公告_B_附件 " + vbCr
                    strSQL = strSQL + "   where 文件标识 = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + " ) a" + vbCr
                    strSQL = strSQL + " order by a.显示序号" + vbCr

                    '设置参数
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    '执行操作
                    .Fill(objTempFujianData.Tables(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.TABLE_DZGG_B_FUJIAN))
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            objFujianData = objTempFujianData
            getFujianData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.ggxxDianzigonggaoData.SafeRelease(objTempFujianData)
            Exit Function

        End Function


        '----------------------------------------------------------------
        ' 判断附件记录数据是否有效？
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objNewData           ：记录新值(返回推荐值)
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doVerifyFujian( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef objNewData As System.Collections.Specialized.NameValueCollection) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            Dim objdacCustomer As New Xydc.Platform.DataAccess.dacCustomer
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '初始化
            doVerifyFujian = False
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
                If objNewData Is Nothing Then
                    strErrMsg = "错误：未传入新的数据！"
                    GoTo errProc
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取表结构定义
                strSQL = "select top 0 * from 电子公告_B_附件"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, objSqlConnection, strSQL, "电子公告_B_附件", objDataSet) = False Then
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
                        Case Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_DZGG_B_FUJIAN_BDWJ, _
                            Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_DZGG_B_FUJIAN_XZBZ, _
                            Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_DZGG_B_FUJIAN_XSXH
                            '显示字段，不用处理

                        Case Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_DZGG_B_FUJIAN_WJXH
                            If strValue = "" Then
                                strErrMsg = "错误：[" + strField + "]不能为空！"
                                GoTo errProc
                            End If
                            If objPulicParameters.isIntegerString(strValue) = False Then
                                strErrMsg = "错误：[" + strField + "]必须是数字！"
                                GoTo errProc
                            End If
                            intLen = CType(strValue, Integer)
                            If intLen < 1 Or intLen > 999999 Then
                                strErrMsg = "错误：[" + strField + "]必须是[1,999999]！"
                                GoTo errProc
                            End If
                            strValue = intLen.ToString()

                        Case Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_DZGG_B_FUJIAN_WJSM
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

                        Case Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_DZGG_B_FUJIAN_WJYS
                            If strValue = "" Then strValue = "1"
                            If objPulicParameters.isIntegerString(strValue) = False Then
                                strErrMsg = "错误：[" + strField + "]必须是数字！"
                                GoTo errProc
                            End If
                            intLen = CType(strValue, Integer)
                            If intLen < 1 Or intLen > 999999 Then
                                strErrMsg = "错误：[" + strField + "]必须是[1,999999]！"
                                GoTo errProc
                            End If
                            strValue = intLen.ToString()

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

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doVerifyFujian = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function


        '----------------------------------------------------------------
        ' 保存附件数据
        '     strErrMsg              ：如果错误，则返回错误信息
        '     blnEnforeEdit          ：是否强制修改
        '     strUserId              ：用户标识
        '     strPassword            ：用户密码
        '     strUserXM              ：操作员名称
        '     strWJBS                : 文件标识
        '     objNewData             ：记录新值(返回保存后的新值)
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function doSaveFujian( _
            ByRef strErrMsg As String, _
            ByVal blnEnforeEdit As Boolean, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByVal strWJBS As String, _
            ByRef objNewData As Xydc.Platform.Common.Data.ggxxDianzigonggaoData) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objOldData As Xydc.Platform.Common.Data.ggxxDianzigonggaoData
            Dim objDataSet As System.Data.DataSet

            Dim objdacCustomer As New Xydc.Platform.DataAccess.dacCustomer
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon


            Dim strBakExt As String = Xydc.Platform.Common.Utilities.PulicParameters.BACKUPFILEEXT
            Dim strTable As String = Xydc.Platform.Common.Data.ggxxDianzigonggaoData.TABLE_DZGG_B_FUJIAN
            Dim intWJND As Integer = Year(Now)
            Dim strSQL As String

            Dim objdacXitongpeizhi As New Xydc.Platform.DataAccess.dacXitongpeizhi
            Dim objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objBaseFTP As New Xydc.Platform.Common.Utilities.BaseFTP

            Dim objFlowObject As Xydc.Platform.DataAccess.FlowObject

            '初始化
            doSaveFujian = False
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

                '获取原附件数据
                If Me.getFujianData(strErrMsg, objSqlConnection, strWJBS, objOldData) = False Then
                    GoTo errProc
                End If

                '获取FTP连接参数
                If objdacXitongpeizhi.getFtpServerParam(strErrMsg, objSqlConnection, objFTPProperty) = False Then
                    GoTo errProc
                End If

                '开始事务
                objSqlTransaction = objSqlConnection.BeginTransaction()

                '保存数据
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '删除“公文_B_附件”数据
                    strSQL = ""
                    strSQL = strSQL + " delete from 电子公告_B_附件 " + vbCr
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
                        Dim strBasePath As String = Me.getBasePath_FJ
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
                                strOldFile = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_DZGG_B_FUJIAN_WJWZ), "")
                                strLocFile = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_DZGG_B_FUJIAN_BDWJ), "")
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
                                strSQL = strSQL + " insert into 电子公告_B_附件 (" + vbCr
                                strSQL = strSQL + "   文件标识, 序号, 说明, 页数, 位置" + vbCr
                                strSQL = strSQL + " ) values (" + vbCr
                                strSQL = strSQL + "   @wjbs, @wjxh, @wjsm, @wjys, @wjwz" + vbCr
                                strSQL = strSQL + " )" + vbCr
                                objSqlCommand.Parameters.Clear()
                                objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                                objSqlCommand.Parameters.AddWithValue("@wjxh", (i + 1))
                                objSqlCommand.Parameters.AddWithValue("@wjsm", objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_DZGG_B_FUJIAN_WJSM), ""))
                                objSqlCommand.Parameters.AddWithValue("@wjys", objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_DZGG_B_FUJIAN_WJYS), 0))
                                objSqlCommand.Parameters.AddWithValue("@wjwz", strNewFile)
                                objSqlCommand.CommandText = strSQL
                                objSqlCommand.ExecuteNonQuery()
                            Next
                        End With


                        '删除所有备份文件
                        If Me.doDeleteBackupFiles_FJ(strErrMsg, objFTPProperty, objOldData) = False Then
                            '可以不成功，形成垃圾文件！
                        End If

                    Catch ex As Exception
                        strErrMsg = ex.Message
                        GoTo rollDatabaseAndFile
                    End Try

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
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacXitongpeizhi.SafeRelease(objdacXitongpeizhi)
            Xydc.Platform.Common.Utilities.FTPProperty.SafeRelease(objFTPProperty)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)
            Xydc.Platform.Common.Data.ggxxDianzigonggaoData.SafeRelease(objOldData)
            Xydc.Platform.DataAccess.FlowObject.SafeRelease(objFlowObject)
            '返回
            doSaveFujian = True
            Exit Function

rollDatabaseAndFile:
            objSqlTransaction.Rollback()
            If Me.doRestoreFiles_FJ(strSQL, strWJBS, intWJND, objFTPProperty, objNewData, objOldData) = False Then
                '无法恢复成功，尽力了！
            End If
            GoTo errProc

rollDatabase:
            objSqlTransaction.Rollback()
            GoTo errProc

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacXitongpeizhi.SafeRelease(objdacXitongpeizhi)
            Xydc.Platform.Common.Utilities.FTPProperty.SafeRelease(objFTPProperty)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)
            Xydc.Platform.Common.Data.ggxxDianzigonggaoData.SafeRelease(objOldData)
            Xydc.Platform.DataAccess.FlowObject.SafeRelease(objFlowObject)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 保存附件数据
        '     strErrMsg              ：如果错误，则返回错误信息
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
            ByVal strWJBS As String, _
            ByVal intWJND As Integer, _
            ByVal objSqlTransaction As System.Data.SqlClient.SqlTransaction, _
            ByVal objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty, _
            ByRef objNewData As Xydc.Platform.Common.Data.ggxxDianzigonggaoData, _
            ByVal objOldData As Xydc.Platform.Common.Data.ggxxDianzigonggaoData) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            Dim strBakExt As String = Xydc.Platform.Common.Utilities.PulicParameters.BACKUPFILEEXT
            Dim strTable As String = Xydc.Platform.Common.Data.ggxxDianzigonggaoData.TABLE_DZGG_B_FUJIAN
            Dim blnNewTrans As Boolean = False
            Dim strSQL As String

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objBaseFTP As New Xydc.Platform.Common.Utilities.BaseFTP
            Dim objFlowObject As Xydc.Platform.DataAccess.FlowObject

            '初始化
            doSaveFujian = False
            strErrMsg = ""

            Try
                '检查
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

                '获取现有信息               
                objSqlConnection = objSqlTransaction.Connection


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

                    '删除“公文_B_附件”数据
                    strSQL = ""
                    strSQL = strSQL + " delete from 电子公告_B_附件 " + vbCr
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
                        Dim strBasePath As String = Me.getBasePath_FJ
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
                                strOldFile = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_DZGG_B_FUJIAN_WJWZ), "")
                                strLocFile = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_DZGG_B_FUJIAN_BDWJ), "")
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
                                strSQL = strSQL + " insert into 电子公告_B_附件 (" + vbCr
                                strSQL = strSQL + "   文件标识, 序号, 说明, 页数, 位置" + vbCr
                                strSQL = strSQL + " ) values (" + vbCr
                                strSQL = strSQL + "   @wjbs, @wjxh, @wjsm, @wjys, @wjwz" + vbCr
                                strSQL = strSQL + " )" + vbCr
                                objSqlCommand.Parameters.Clear()
                                objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                                objSqlCommand.Parameters.AddWithValue("@wjxh", (i + 1))
                                objSqlCommand.Parameters.AddWithValue("@wjsm", objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_DZGG_B_FUJIAN_WJSM), ""))
                                objSqlCommand.Parameters.AddWithValue("@wjys", objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_DZGG_B_FUJIAN_WJYS), 0))
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
                    strErrMsg = ex.Message
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
            Xydc.Platform.DataAccess.FlowObject.SafeRelease(objFlowObject)

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
            Xydc.Platform.DataAccess.FlowObject.SafeRelease(objFlowObject)
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
        ' 获取工作流附件的基本目录
        '----------------------------------------------------------------
        Public Function getBasePath_FJ() As String
            getBasePath_FJ = Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FILEDIR_FJ
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
            ByVal objFJData As Xydc.Platform.Common.Data.ggxxDianzigonggaoData) As Boolean

            Dim strBakExt As String = Xydc.Platform.Common.Utilities.PulicParameters.BACKUPFILEEXT
            Dim strTable As String = Xydc.Platform.Common.Data.ggxxDianzigonggaoData.TABLE_DZGG_B_FUJIAN

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
                        strOldFile = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_DZGG_B_FUJIAN_WJWZ), "")
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
            ByVal objFJData As Xydc.Platform.Common.Data.ggxxDianzigonggaoData) As Boolean

            Dim strBakExt As String = Xydc.Platform.Common.Utilities.PulicParameters.BACKUPFILEEXT
            Dim strTable As String = Xydc.Platform.Common.Data.ggxxDianzigonggaoData.TABLE_DZGG_B_FUJIAN

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
                        strOldFile = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_DZGG_B_FUJIAN_WJWZ), "")
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
            ByVal objNewData As Xydc.Platform.Common.Data.ggxxDianzigonggaoData, _
            ByVal objOldData As Xydc.Platform.Common.Data.ggxxDianzigonggaoData) As Boolean

            Dim strBakExt As String = Xydc.Platform.Common.Utilities.PulicParameters.BACKUPFILEEXT
            Dim strTable As String = Xydc.Platform.Common.Data.ggxxDianzigonggaoData.TABLE_DZGG_B_FUJIAN

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objBaseFTP As New Xydc.Platform.Common.Utilities.BaseFTP
            Dim objFlowObject As Xydc.Platform.DataAccess.FlowObject
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
                Dim strBasePath As String = Me.getBasePath_FJ()
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
                        strOldFile = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_DZGG_B_FUJIAN_WJWZ), "")
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
                                                strNewWJWZ = objPulicParameters.getObjectValue(.DefaultView.Item(j).Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_DZGG_B_FUJIAN_WJWZ), "")
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
            Xydc.Platform.DataAccess.FlowObject.SafeRelease(objFlowObject)

            doRestoreFiles_FJ = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)
            Xydc.Platform.DataAccess.FlowObject.SafeRelease(objFlowObject)
            Exit Function

        End Function


        '----------------------------------------------------------------
        ' 在附件缓存数据中删除“公文_B_附件”的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objOldData           ：旧数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteData_FJ( _
            ByRef strErrMsg As String, _
            ByVal objOldData As System.Data.DataRow) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile

            '初始化
            doDeleteData_FJ = False
            strErrMsg = ""

            Try
                '检查
                If objOldData Is Nothing Then
                    strErrMsg = "错误：未传入要删除的数据！"
                    GoTo errProc
                End If

                '备份临时文件
                Dim strTempFile As String = ""
                strTempFile = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_DZGG_B_FUJIAN_BDWJ), "")

                '删除数据
                objOldData.Delete()

                '删除临时文件
                If strTempFile <> "" Then
                    If objBaseLocalFile.doDeleteFile(strErrMsg, strTempFile) = False Then
                        '形成垃圾文件
                    End If
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)

            '返回
            doDeleteData_FJ = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 在附件缓存数据中自动调整显示序号=数据集中的行序号+1
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objFJData            ：缓存数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doAutoAdjustXSXH_FJ( _
            ByRef strErrMsg As String, _
            ByRef objFJData As Xydc.Platform.Common.Data.ggxxDianzigonggaoData) As Boolean

            '初始化
            doAutoAdjustXSXH_FJ = False
            strErrMsg = ""

            Try
                '检查
                If objFJData Is Nothing Then
                    strErrMsg = "错误：未传入文件数据！"
                    GoTo errProc
                End If

                '自动设置序号
                Dim strField As String = Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_DZGG_B_FUJIAN_XSXH
                Dim objTemp As Object
                Dim intCount As Integer
                Dim i As Integer
                With objFJData.Tables(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.TABLE_DZGG_B_FUJIAN)
                    intCount = .DefaultView.Count
                    For i = 0 To intCount - 1 Step 1
                        .DefaultView.Item(i).Item(strField) = i + 1
                    Next
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            '返回
            doAutoAdjustXSXH_FJ = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 在附件缓存数据中将指定行objSrcData移动到指定行objDesData
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objSrcData           ：要移动的数据
        '     objDesData           ：要移动到的数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doMoveTo_FJ( _
            ByRef strErrMsg As String, _
            ByRef objSrcData As System.Data.DataRow, _
            ByRef objDesData As System.Data.DataRow) As Boolean

            '初始化
            doMoveTo_FJ = False
            strErrMsg = ""

            Try
                '检查
                If objSrcData Is Nothing Then
                    strErrMsg = "错误：未传入要移动的数据！"
                    GoTo errProc
                End If
                If objDesData Is Nothing Then
                    strErrMsg = "错误：未传入要移动到的数据！"
                    GoTo errProc
                End If

                '移动
                Dim strField As String = Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_DZGG_B_FUJIAN_XSXH
                Dim objTemp As Object
                objTemp = objSrcData.Item(strField)
                objSrcData.Item(strField) = objDesData.Item(strField)
                objDesData.Item(strField) = objTemp

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            '返回
            doMoveTo_FJ = True
            Exit Function

errProc:
            Exit Function

        End Function

    End Class

End Namespace
