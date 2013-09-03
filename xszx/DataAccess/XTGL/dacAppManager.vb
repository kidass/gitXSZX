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
Imports System.IO
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient

Imports Xydc.Platform.Common
Imports Xydc.Platform.Common.Data
Imports Xydc.Platform.SystemFramework

Namespace Xydc.Platform.DataAccess

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.DataAccess
    ' 类名    ：dacAppManager
    '
    ' 功能描述：
    '     提供对应用系统管理功能的数据访问层支持
    '----------------------------------------------------------------

    Public Class dacAppManager
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.DataAccess.dacAppManager)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub










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
        ' 获取人员申请ID情况的数据集(以组织代码、人员序号升序排序)
        ' 含人员的全部连接数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWhere             ：搜索字符串(默认表前缀a.)
        '     objRenyuanData       ：指定组织机构下的人员信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getRenyuanApplyIdData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objRenyuanData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempRenyuanData As Xydc.Platform.Common.Data.CustomerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getRenyuanApplyIdData = False
            objRenyuanData = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId is nothing Then strUserId = ""
                If strPassword is nothing Then strPassword = ""
                If strWhere.Length > 0 Then strWhere = strWhere.Trim()
                If strUserId.trim = "" Then
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
                    objTempRenyuanData = New Xydc.Platform.Common.Data.CustomerData(Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_RENYUAN_FULLJOIN)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* from ("
                        strSQL = strSQL + "   select a.*," + vbCr
                        strSQL = strSQL + "     b.组织名称,b.组织别名," + vbCr
                        strSQL = strSQL + "     岗位列表 = dbo.GetGWMCByRydm(a.人员代码,@separate)," + vbCr
                        strSQL = strSQL + "     c.级别名称,c.行政级别," + vbCr
                        strSQL = strSQL + "     秘书名称 = d.人员名称," + vbCr
                        strSQL = strSQL + "     其他由转送名称 = e.人员名称," + vbCr
                        strSQL = strSQL + "     是否申请 = case when f.name is null then @charfalse else @chartrue end " + vbCr
                        strSQL = strSQL + "   from 公共_B_人员 a " + vbCr
                        strSQL = strSQL + "   left join 公共_B_组织机构 b on a.组织代码   = b.组织代码 " + vbCr
                        strSQL = strSQL + "   left join 公共_B_行政级别 c on a.级别代码   = c.级别代码 " + vbCr
                        strSQL = strSQL + "   left join 公共_B_人员     d on a.秘书代码   = d.人员代码 " + vbCr
                        strSQL = strSQL + "   left join 公共_B_人员     e on a.其他由转送 = e.人员代码 " + vbCr
                        strSQL = strSQL + "   left join" + vbCr
                        strSQL = strSQL + "   (" + vbCr
                        strSQL = strSQL + "     select name from master.dbo.syslogins"
                        strSQL = strSQL + "   ) f on a.人员代码 = f.name" + vbCr
                        strSQL = strSQL + " ) a "
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.组织代码, cast(a.人员序号 as integer)"

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@separate", Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate)
                        objSqlCommand.Parameters.AddWithValue("@charfalse", Xydc.Platform.Common.Utilities.PulicParameters.CharFalse)
                        objSqlCommand.Parameters.AddWithValue("@chartrue", Xydc.Platform.Common.Utilities.PulicParameters.CharTrue)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempRenyuanData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempRenyuanData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempRenyuanData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objRenyuanData = objTempRenyuanData
            getRenyuanApplyIdData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempRenyuanData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 申请Login
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strLoginId           ：要申请的loginId
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doApplyId( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strLoginId As String) As Boolean

            Dim objdacCustomer As New Xydc.Platform.DataAccess.dacCustomer
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            doApplyId = False
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strLoginId Is Nothing Then strLoginId = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                strLoginId = strLoginId.Trim()
                If strUserId.trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If strLoginId = "" Then
                    strErrMsg = "错误：未指定要创建的Login！"
                    GoTo errProc
                End If

                '获取加密密码
                Dim strNewPassword As String
                strNewPassword = objdacCustomer.doEncryptPassowrd("")

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                Dim strSQL As String
                Try
                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '设置参数
                    strSQL = "exec sp_addlogin @loginid, @password, @defdb"
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@loginid", strLoginId)
                    objSqlCommand.Parameters.AddWithValue("@password", strNewPassword)
                    objSqlCommand.Parameters.AddWithValue("@defdb", "master")
                    objSqlCommand.ExecuteNonQuery()
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
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doApplyId = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 注销Login
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strLoginId           ：要注销的loginId
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDropId( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strLoginId As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objDataSetUser As System.Data.DataSet
            Dim objDataSetDB As System.Data.DataSet
            Dim strSQL As String

            '初始化
            doDropId = False
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strLoginId Is Nothing Then strLoginId = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                strLoginId = strLoginId.Trim()
                If strUserId.trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If strLoginId = "" Then
                    strErrMsg = "错误：未指定要注销的Login！"
                    GoTo errProc
                End If
                If strLoginId.ToUpper() = "SA" Then
                    '不能删除
                    Exit Try
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取现有数据库
                strSQL = "select name from master.dbo.sysdatabases where name <> 'tempdb'"
                If objdacCommon.getDataSetBySQL(strErrMsg, strUserId, strPassword, strSQL, objDataSetDB) = False Then
                    GoTo errProc
                End If

                '获取数据
                Try
                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '逐个数据库删除user
                    Dim strDBName As String
                    Dim intCount As Integer
                    Dim i As Integer
                    With objDataSetDB.Tables(0)
                        intCount = .Rows.Count
                        For i = 0 To intCount - 1 Step 1
                            strDBName = objPulicParameters.getObjectValue(.Rows(i).Item("name"), "")
                            strSQL = ""
                            strSQL = strSQL + " use " + strDBName + vbCr
                            strSQL = strSQL + " select name from sysusers where issqluser = 1 and name = '" + strLoginId + "'" + vbCr
                            If objdacCommon.getDataSetBySQL(strErrMsg, strUserId, strPassword, strSQL, objDataSetUser) = False Then
                                GoTo errProc
                            End If
                            If objDataSetUser.Tables(0).Rows.Count > 0 Then
                                strSQL = ""
                                strSQL = strSQL + " use " + strDBName + vbCr
                                strSQL = strSQL + " exec sp_dropuser '" + strLoginId + "'"
                                objSqlCommand.CommandText = strSQL
                                objSqlCommand.Parameters.Clear()
                                objSqlCommand.ExecuteNonQuery()
                            End If
                            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSetUser)
                            objDataSetUser = Nothing
                        Next
                    End With
                    Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSetDB)
                    objDataSetDB = Nothing

                    '删除login
                    strSQL = ""
                    strSQL = strSQL + " use master" + vbCr
                    strSQL = strSQL + " exec sp_droplogin '" + strLoginId + "'"
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSetUser)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSetDB)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doDropId = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSetUser)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSetDB)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function


        '----------------------------------------------------------------
        ' 检查Login
        '     strErrMsg            ：如果错误，则返回错误信息
        '     blnISNull            ：TRUE-已申请，FALSE-未申请
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strLoginId           ：要检查的loginId
        ' 返回
        '     True                 ：已申请
        '     False                ：未申请

        '----------------------------------------------------------------
        Public Function doCheckId( _
            ByRef strErrMsg As String, _
            ByRef blnISNull As Boolean, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strLoginId As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            '初始化
            doCheckId = False
            strErrMsg = ""
            blnISNull = False

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strLoginId Is Nothing Then strLoginId = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                strLoginId = strLoginId.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If strLoginId = "" Then
                    strErrMsg = "错误：未指定要注销的Login！"
                    GoTo errProc
                End If
                If strLoginId.ToUpper() = "SA" Then
                    blnISNull = True
                    Exit Try
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '检查login
                strSQL = ""
                strSQL = strSQL + " select * from master.dbo.syslogins where name='" + strLoginId + "'" + vbCr

                If objdacCommon.getDataSetBySQL(strErrMsg, strUserId, strPassword, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If

                If objDataSet.Tables(0).Rows.Count > 0 Then
                    blnISNull = True
                Else
                    blnISNull = False
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doCheckId = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取“管理_B_数据库_服务器”的数据集(以名称升序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWhere             ：搜索字符串(默认表前缀a.)
        '     objFuwuqiData        ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getFuwuqiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objFuwuqiData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempFuwuqiData As Xydc.Platform.Common.Data.AppManagerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getFuwuqiData = False
            objFuwuqiData = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strWhere Is Nothing Then strWhere = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                strWhere = strWhere.Trim()
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
                    objTempFuwuqiData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_SHUJUKU_FUWUQI)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from 管理_B_数据库_服务器 a " + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.名称 " + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempFuwuqiData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_FUWUQI))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempFuwuqiData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempFuwuqiData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objFuwuqiData = objTempFuwuqiData
            getFuwuqiData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempFuwuqiData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据服务器名获取“管理_B_数据库_服务器”的数据集(以名称升序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strServerName        ：服务器名
        '     strWhere             ：搜索字符串(默认表前缀a.)
        '     objFuwuqiData        ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getFuwuqiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strServerName As String, _
            ByVal strWhere As String, _
            ByRef objFuwuqiData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempFuwuqiData As Xydc.Platform.Common.Data.AppManagerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getFuwuqiData = False
            objFuwuqiData = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strServerName Is Nothing Then strServerName = ""
                If strWhere Is Nothing Then strWhere = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                strServerName = strServerName.Trim()
                strWhere = strWhere.Trim()
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
                    objTempFuwuqiData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_SHUJUKU_FUWUQI)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from 管理_B_数据库_服务器 a " + vbCr
                        strSQL = strSQL + " where a.名称 = @fwqm" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " and " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.名称 " + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@fwqm", strServerName)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempFuwuqiData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_FUWUQI))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempFuwuqiData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempFuwuqiData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objFuwuqiData = objTempFuwuqiData
            getFuwuqiData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempFuwuqiData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据加密连接串获取连接参数
        '     strErrMsg             ：如果错误，则返回错误信息
        '     objConnectionProperty ：用户标识
        '     value                 ：连接字符串的加密数据
        ' 返回
        '     True                  ：成功
        '     False                 ：失败
        '----------------------------------------------------------------
        Public Function getServerConnectionProperty( _
            ByRef strErrMsg As String, _
            ByRef objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal value As Object) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            getServerConnectionProperty = False
            objConnectionProperty = Nothing

            Try
                '获取加密字节数据
                Dim bData() As Byte
                bData = objPulicParameters.getObjectValue(value, New Byte(0) {})
                If bData.Length < 1 Then
                    strErrMsg = "错误：没有数据！"
                    GoTo errProc
                End If

                '解密数据
                Dim strConnection As String
                If objPulicParameters.doDecryptString(strErrMsg, bData, strConnection) = False Then
                    GoTo errProc
                End If

                '获取ConnectionProperty
                objConnectionProperty = New Xydc.Platform.Common.Utilities.ConnectionProperty(strConnection)

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            getServerConnectionProperty = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据服务器名获取连接参数
        '     strErrMsg             ：如果错误，则返回错误信息
        '     strUserId             ：用户标识
        '     strPassword           ：用户密码
        '     strServerName         ：服务器名
        '     objConnectionProperty ：返回连接参数
        ' 返回
        '     True                  ：成功
        '     False                 ：失败
        '----------------------------------------------------------------
        Public Function getServerConnectionProperty( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strServerName As String, _
            ByRef objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objAppManagerData As Xydc.Platform.Common.Data.AppManagerData

            getServerConnectionProperty = False
            objConnectionProperty = Nothing

            Try
                '根据服务器名获取记录
                Dim bData() As Byte
                If Me.getFuwuqiData(strErrMsg, strUserId, strPassword, strServerName, "", objAppManagerData) = False Then
                    GoTo errProc
                End If
                If objAppManagerData.Tables.Count < 1 Then
                    strErrMsg = "错误：没有数据！"
                    GoTo errProc
                End If
                If objAppManagerData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_FUWUQI) Is Nothing Then
                    strErrMsg = "错误：没有数据！"
                    GoTo errProc
                End If
                With objAppManagerData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_FUWUQI)
                    If .Rows.Count < 1 Then
                        strErrMsg = "错误：没有数据！"
                        GoTo errProc
                    End If

                    '获取加密字节数据
                    bData = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_FUWUQI_LJC), New Byte(0) {})
                    If bData.Length < 1 Then
                        strErrMsg = "错误：没有数据！"
                        GoTo errProc
                    End If
                End With
                Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objAppManagerData)
                objAppManagerData = Nothing

                '解密数据
                Dim strConnection As String
                If objPulicParameters.doDecryptString(strErrMsg, bData, strConnection) = False Then
                    GoTo errProc
                End If

                '获取ConnectionProperty
                objConnectionProperty = New Xydc.Platform.Common.Utilities.ConnectionProperty(strConnection)

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objAppManagerData)

            getServerConnectionProperty = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objAppManagerData)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取“管理_B_数据库_数据库”的数据集(以服务器名、数据库名升序排序)
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     objConnectionProperty ：服务器信息
        '     strWhere                    ：搜索字符串(默认表前缀a.)
        '     objShujukuData              ：信息数据集
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getShujukuData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strWhere As String, _
            ByRef objShujukuData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempShujukuData As Xydc.Platform.Common.Data.AppManagerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getShujukuData = False
            objShujukuData = Nothing
            strErrMsg = ""

            Try
                '检查
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim()
                If objConnectionProperty Is Nothing Then
                    '创建数据集
                    objTempShujukuData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_SHUJUKU_SHUJUKU)
                    Exit Try
                End If

                '获取连接
                With objConnectionProperty

                    'If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, -1, .InitialCatalog, .DataSource) = False Then
                    '    GoTo errProc
                    'End If
                    If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, Platform.Common.jsoaConfiguration.ConnectionTestTimeout, .InitialCatalog, .DataSource) = False Then
                        GoTo errProc
                    End If

                End With

                '获取数据
                Dim strSQL As String
                Try
                    '创建数据集
                    objTempShujukuData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_SHUJUKU_SHUJUKU)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        If objConnectionProperty.DataSource.ToUpper() = Xydc.Platform.Common.jsoaConfiguration.DatabaseServerName.ToUpper() Then
                            '同服务器
                            Dim strDefDB As String = Xydc.Platform.Common.jsoaConfiguration.DatabaseServerUserDB
                            '准备SQL
                            strSQL = ""
                            strSQL = strSQL + " select a.* from (" + vbCr
                            strSQL = strSQL + "   select a.服务器名,a.数据库名," + vbCr
                            strSQL = strSQL + "     数据库中文名=case when b.服务器名 is null then a.数据库中文名 else b.数据库中文名 end," + vbCr
                            strSQL = strSQL + "     说明=case when b.服务器名 is null then a.说明 else b.说明 end" + vbCr
                            strSQL = strSQL + "   from (" + vbCr
                            strSQL = strSQL + "     select 服务器名=@fwqm,数据库名=name,数据库中文名=name,说明=@sm " + vbCr
                            strSQL = strSQL + "     from master.dbo.sysdatabases" + vbCr
                            strSQL = strSQL + "     where name <> 'tempdb'" + vbCr
                            strSQL = strSQL + "   ) a " + vbCr
                            strSQL = strSQL + "   left join " + strDefDB + ".dbo.管理_B_数据库_数据库 b on a.服务器名 = b.服务器名 and a.数据库名=b.数据库名 "
                            strSQL = strSQL + " ) a" + vbCr
                            If strWhere <> "" Then
                                strSQL = strSQL + " where " + strWhere + vbCr
                            End If
                            strSQL = strSQL + " order by a.服务器名,a.数据库名" + vbCr
                            '设置参数
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@fwqm", objConnectionProperty.DataSource)
                            objSqlCommand.Parameters.AddWithValue("@sm", " ")
                            .SelectCommand = objSqlCommand
                        Else
                            '不同服务器
                            '准备SQL
                            strSQL = ""
                            strSQL = strSQL + " select a.* " + vbCr
                            strSQL = strSQL + " from (" + vbCr
                            strSQL = strSQL + "   select 服务器名=@fwqm,数据库名=name,数据库中文名=name,说明=@sm " + vbCr
                            strSQL = strSQL + "   from master.dbo.sysdatabases" + vbCr
                            strSQL = strSQL + "   where name <> 'tempdb'" + vbCr
                            strSQL = strSQL + " ) a " + vbCr
                            If strWhere <> "" Then
                                strSQL = strSQL + " where " + strWhere + vbCr
                            End If
                            strSQL = strSQL + " order by a.服务器名,a.数据库名" + vbCr
                            '设置参数
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@fwqm", objConnectionProperty.DataSource)
                            objSqlCommand.Parameters.AddWithValue("@sm", " ")
                            .SelectCommand = objSqlCommand
                        End If

                        '执行操作
                        .Fill(objTempShujukuData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_SHUJUKU))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempShujukuData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempShujukuData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objShujukuData = objTempShujukuData
            getShujukuData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempShujukuData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function


        '----------------------------------------------------------------
        ' 获取“管理_B_数据库_对象”的数据集(以数据库名升序排序)
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     objConnectionProperty ：服务器信息
        '     strWhere                    ：搜索字符串(默认表前缀a.)
        '     objDuixiangData             ：信息数据集
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getDuixiangData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strWhere As String, _
            ByRef objDuixiangData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempDuixiangData As Xydc.Platform.Common.Data.AppManagerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getDuixiangData = False
            objDuixiangData = Nothing
            strErrMsg = ""

            Try
                '检查
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim()

                '返回空数据
                If objConnectionProperty Is Nothing Then
                    '创建数据集
                    objTempDuixiangData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_SHUJUKU_DUIXIANG)
                    Exit Try
                End If

                '获取连接
                With objConnectionProperty

                    'If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, -1, .InitialCatalog, .DataSource) = False Then
                    '    GoTo errProc
                    'End If
                    If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, Platform.Common.jsoaConfiguration.ConnectionTestTimeout, .InitialCatalog, .DataSource) = False Then
                        GoTo errProc
                    End If

                End With

                '获取数据
                Dim strSQL As String
                Try
                    '创建数据集
                    objTempDuixiangData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_SHUJUKU_DUIXIANG)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        Dim strCurDB As String = objConnectionProperty.InitialCatalog
                        Dim strXType As String = Xydc.Platform.Common.Data.AppManagerData.OBJECTTYPELIST
                        If objConnectionProperty.DataSource.ToUpper() = Xydc.Platform.Common.jsoaConfiguration.DatabaseServerName.ToUpper() Then
                            '同服务器
                            Dim strDefDB As String = Xydc.Platform.Common.jsoaConfiguration.DatabaseServerUserDB
                            '准备SQL
                            strSQL = ""
                            strSQL = strSQL + " select a.* from (" + vbCr
                            strSQL = strSQL + "   select a.服务器名,a.数据库名,a.对象名称,a.对象类型," + vbCr
                            strSQL = strSQL + "     对象中文名=case when b.服务器名 is null then a.对象中文名 else b.对象中文名 end," + vbCr
                            strSQL = strSQL + "     说明=case when b.服务器名 is null then a.说明 else b.说明 end," + vbCr
                            strSQL = strSQL + "     b.对象标识" + vbCr
                            strSQL = strSQL + "   from (" + vbCr
                            strSQL = strSQL + "     select 服务器名=@fwqm,数据库名=@sjkm,对象名称=name,对象类型=xtype,对象中文名=name,说明=@sm " + vbCr
                            strSQL = strSQL + "     from " + strCurDB + ".dbo.sysobjects" + vbCr
                            strSQL = strSQL + "     where xtype in (" + strXType + ")" + vbCr '确定要处理的对象
                            strSQL = strSQL + "     and   status > 0" + vbCr                  '排除系统对象
                            strSQL = strSQL + "   ) a " + vbCr
                            strSQL = strSQL + "   left join " + strDefDB + ".dbo.管理_B_数据库_对象 b on a.服务器名 = b.服务器名 and a.数据库名=b.数据库名 and a.对象名称=b.对象名称 and a.对象类型=b.对象类型 "
                            strSQL = strSQL + " ) a" + vbCr
                            If strWhere <> "" Then
                                strSQL = strSQL + " where " + strWhere + vbCr
                            End If
                            strSQL = strSQL + " order by a.服务器名,a.数据库名,a.对象类型,a.对象名称" + vbCr
                            '设置参数
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@fwqm", objConnectionProperty.DataSource)
                            objSqlCommand.Parameters.AddWithValue("@sjkm", strCurDB)
                            objSqlCommand.Parameters.AddWithValue("@sm", " ")
                            .SelectCommand = objSqlCommand
                        Else
                            '不同服务器
                            '准备SQL
                            strSQL = ""
                            strSQL = strSQL + " select a.* " + vbCr
                            strSQL = strSQL + " from (" + vbCr
                            strSQL = strSQL + "   select 服务器名=@fwqm,数据库名=@sjkm,对象名称=name,对象类型=xtype,对象中文名=name,说明=@sm " + vbCr
                            strSQL = strSQL + "   from " + strCurDB + ".dbo.sysobjects" + vbCr
                            strSQL = strSQL + "   where xtype in (" + strXType + ")" + vbCr '确定要处理的对象
                            strSQL = strSQL + "   and   status > 0" + vbCr                  '排除系统对象
                            strSQL = strSQL + " ) a " + vbCr
                            If strWhere <> "" Then
                                strSQL = strSQL + " where " + strWhere + vbCr
                            End If
                            strSQL = strSQL + " order by a.服务器名,a.数据库名,a.对象类型,a.对象名称" + vbCr
                            '设置参数
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@fwqm", objConnectionProperty.DataSource)
                            objSqlCommand.Parameters.AddWithValue("@sjkm", strCurDB)
                            objSqlCommand.Parameters.AddWithValue("@sm", " ")
                            .SelectCommand = objSqlCommand
                        End If

                        '执行操作
                        .Fill(objTempDuixiangData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_DUIXIANG))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDuixiangData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempDuixiangData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objDuixiangData = objTempDuixiangData
            getDuixiangData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempDuixiangData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 检查“管理_B_数据库_服务器”的数据的合法性
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
        '     objNewData           ：新数据
        '     objenumEditType      ：编辑类型

        ' 返回
        '     True                 ：合法
        '     False                ：不合法或其他程序错误
        '----------------------------------------------------------------
        Public Function doVerifyFuwuqiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.ListDictionary, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objListDictionary As New System.Collections.Specialized.ListDictionary
            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            doVerifyFuwuqiData = False

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "错误：未传入新的数据！"
                    GoTo errProc
                End If
                Dim strOldMC As String
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                    Case Else
                        If objOldData Is Nothing Then
                            strErrMsg = "错误：未传入旧的数据！"
                            GoTo errProc
                        End If
                        strOldMC = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_FUWUQI_MC), "")
                End Select

                '获取表结构定义
                strSQL = "select top 0 * from 管理_B_数据库_服务器"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, strUserId, strPassword, strSQL, "管理_B_数据库_服务器", objDataSet) = False Then
                    GoTo errProc
                End If

                '检查数据长度
                Dim objDictionaryEntry As System.Collections.DictionaryEntry
                Dim strField As String
                Dim intLen As Integer
                For Each objDictionaryEntry In objNewData
                    strField = objPulicParameters.getObjectValue(objDictionaryEntry.Key, "")
                    Select Case strField
                        Case Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_FUWUQI_LJC
                            Dim bData() As Byte
                            bData = objPulicParameters.getObjectValue(objDictionaryEntry.Value, New Byte(0) {})
                            If bData.Length < 1 Then
                                strErrMsg = "错误：[" + strField + "]不能为空！"
                                GoTo errProc
                            End If
                            Exit Select

                        Case Else
                            Dim strValue As String
                            strValue = objPulicParameters.getObjectValue(objDictionaryEntry.Value, "")
                            If strValue = "" Then
                                Select Case strField
                                    Case Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_FUWUQI_MC, _
                                        Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_FUWUQI_LX, _
                                        Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_FUWUQI_TGZ
                                        strErrMsg = "错误：[" + strField + "]不能为空！"
                                        GoTo errProc
                                End Select
                            End If
                            With objDataSet.Tables(0).Columns(strField)
                                intLen = objPulicParameters.getStringLength(strValue)
                                If intLen > .MaxLength Then
                                    strErrMsg = "错误：[" + strField + "]长度不能超过[" + .MaxLength.ToString() + "]，实际有[" + intLen.ToString() + "]！"
                                    GoTo errProc
                                End If
                            End With
                    End Select
                Next
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '检查名称
                Dim strMC As String
                strMC = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_FUWUQI_MC), "")
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                        strSQL = "select * from 管理_B_数据库_服务器 where 名称 = @mc"
                        objListDictionary.Add("@mc", strMC)
                    Case Else
                        strSQL = "select * from 管理_B_数据库_服务器 where 名称 = @mc and 名称 <> @oldmc"
                        objListDictionary.Add("@mc", strMC)
                        objListDictionary.Add("@oldmc", strOldMC)
                End Select
                If objdacCommon.getDataSetBySQL(strErrMsg, strUserId, strPassword, strSQL, objListDictionary, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strErrMsg = "错误：[" + strMC + "]已经存在！"
                    GoTo errProc
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing
                objListDictionary.Clear()
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objListDictionary)

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objListDictionary)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            doVerifyFuwuqiData = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objListDictionary)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 保存“管理_B_数据库_服务器”的数据
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
        Public Function doSaveFuwuqiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.ListDictionary, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '初始化
            doSaveFuwuqiData = False
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "错误：未传入新的数据！"
                    GoTo errProc
                End If
                Dim strOldFWQMC As String
                Dim strNewFWQMC As String
                strNewFWQMC = objPulicParameters.getObjectValue(objNewData.Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_FUWUQI_MC), "")
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                    Case Else
                        If objOldData Is Nothing Then
                            strErrMsg = "错误：未传入旧的数据！"
                            GoTo errProc
                        End If
                        strOldFWQMC = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_FUWUQI_MC), "")
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
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '计算SQL
                    Dim objDictionaryEntry As System.Collections.DictionaryEntry
                    Dim strFileds As String = ""
                    Dim strValues As String = ""
                    Dim strField As String
                    Dim i As Integer = 0
                    Select Case objenumEditType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                            For Each objDictionaryEntry In objNewData
                                If strFileds = "" Then
                                    strFileds = objPulicParameters.getObjectValue(objDictionaryEntry.Key, "")
                                Else
                                    strFileds = strFileds + "," + objPulicParameters.getObjectValue(objDictionaryEntry.Key, "")
                                End If
                                If strValues = "" Then
                                    strValues = "@A" + i.ToString()
                                Else
                                    strValues = strValues + "," + "@A" + i.ToString()
                                End If
                                i += 1
                            Next
                            strSQL = ""
                            strSQL = strSQL + " insert into 管理_B_数据库_服务器 (" + strFileds + ")"
                            strSQL = strSQL + " values (" + strValues + ")"
                            objSqlCommand.Parameters.Clear()
                            i = 0
                            For Each objDictionaryEntry In objNewData
                                objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), objDictionaryEntry.Value)
                                i += 1
                            Next
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()

                        Case Else
                            For Each objDictionaryEntry In objNewData
                                If strFileds = "" Then
                                    strFileds = objPulicParameters.getObjectValue(objDictionaryEntry.Key, "") + " = @A" + i.ToString()
                                Else
                                    strFileds = strFileds + "," + objPulicParameters.getObjectValue(objDictionaryEntry.Key, "") + " = @A" + i.ToString()
                                End If
                                i += 1
                            Next
                            strSQL = ""
                            strSQL = strSQL + " update 管理_B_数据库_服务器 set "
                            strSQL = strSQL + "   " + strFileds
                            strSQL = strSQL + " where 名称 = @oldfwqm"
                            objSqlCommand.Parameters.Clear()
                            i = 0
                            For Each objDictionaryEntry In objNewData
                                objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), objDictionaryEntry.Value)
                                i += 1
                            Next
                            objSqlCommand.Parameters.AddWithValue("@oldfwqm", strOldFWQMC)
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()

                            If strNewFWQMC.ToUpper() <> strOldFWQMC.ToUpper() Then
                                strSQL = ""
                                strSQL = strSQL + " update 管理_B_数据库_数据库 set "
                                strSQL = strSQL + "   服务器名 = @newfwqm"
                                strSQL = strSQL + " where 服务器名 = @oldfwqm"
                                objSqlCommand.Parameters.Clear()
                                objSqlCommand.Parameters.AddWithValue("@newfwqm", strNewFWQMC)
                                objSqlCommand.Parameters.AddWithValue("@oldfwqm", strOldFWQMC)
                                objSqlCommand.CommandText = strSQL
                                objSqlCommand.ExecuteNonQuery()

                                strSQL = ""
                                strSQL = strSQL + " update 管理_B_数据库_对象 set "
                                strSQL = strSQL + "   服务器名 = @newfwqm"
                                strSQL = strSQL + " where 服务器名 = @oldfwqm"
                                objSqlCommand.Parameters.Clear()
                                objSqlCommand.Parameters.AddWithValue("@newfwqm", strNewFWQMC)
                                objSqlCommand.Parameters.AddWithValue("@oldfwqm", strOldFWQMC)
                                objSqlCommand.CommandText = strSQL
                                objSqlCommand.ExecuteNonQuery()
                            End If
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
            doSaveFuwuqiData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 删除“管理_B_数据库_服务器”的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strServerName        ：服务器名
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteFuwuqiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strServerName As String) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            doDeleteFuwuqiData = False
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strServerName Is Nothing Then strServerName = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                strServerName = strServerName.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
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

                    '删除管理_B_数据库_对象
                    strSQL = ""
                    strSQL = strSQL + " delete from 管理_B_数据库_对象 "
                    strSQL = strSQL + " where 服务器名 = @fwqm"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@fwqm", strServerName)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '删除管理_B_数据库_数据库
                    strSQL = ""
                    strSQL = strSQL + " delete from 管理_B_数据库_数据库 "
                    strSQL = strSQL + " where 服务器名 = @fwqm"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@fwqm", strServerName)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '删除管理_B_数据库_服务器
                    strSQL = ""
                    strSQL = strSQL + " delete from 管理_B_数据库_服务器 "
                    strSQL = strSQL + " where 名称 = @fwqm"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@fwqm", strServerName)
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
            doDeleteFuwuqiData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据服务器名、数据库名获取“管理_B_数据库_数据库”的数据集
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strServerName        ：服务器名
        '     strDBName            ：数据库名
        '     strWhere             ：搜索字符串(默认表前缀a.)
        '     objShujukuData       ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getShujukuData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strServerName As String, _
            ByVal strDBName As String, _
            ByVal strWhere As String, _
            ByRef objShujukuData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempShujukuData As Xydc.Platform.Common.Data.AppManagerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getShujukuData = False
            objShujukuData = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strServerName Is Nothing Then strServerName = ""
                If strDBName Is Nothing Then strDBName = ""
                If strWhere Is Nothing Then strWhere = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                strServerName = strServerName.Trim()
                strDBName = strDBName.Trim()
                strWhere = strWhere.Trim()
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
                    objTempShujukuData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_SHUJUKU_SHUJUKU)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from 管理_B_数据库_数据库 a " + vbCr
                        strSQL = strSQL + " where a.服务器名 = @fwqm" + vbCr
                        strSQL = strSQL + " and   a.数据库名 = @sjkm" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " and " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.服务器名,a.数据库名 " + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@fwqm", strServerName)
                        objSqlCommand.Parameters.AddWithValue("@sjkm", strDBName)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempShujukuData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_SHUJUKU))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempShujukuData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempShujukuData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objShujukuData = objTempShujukuData
            getShujukuData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempShujukuData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据服务器名、数据库名、对象名称、对象类型
        ' 获取“管理_B_数据库_对象”的数据集
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strServerName        ：服务器名
        '     strDBName            ：数据库名
        '     strDXLX              ：数据库对象类型
        '     strDXMC              ：数据库对象名
        '     strWhere             ：搜索字符串(默认表前缀a.)
        '     objDuixiangData      ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDuixiangData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strServerName As String, _
            ByVal strDBName As String, _
            ByVal strDXLX As String, _
            ByVal strDXMC As String, _
            ByVal strWhere As String, _
            ByRef objDuixiangData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempDuixiangData As Xydc.Platform.Common.Data.AppManagerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getDuixiangData = False
            objDuixiangData = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strServerName Is Nothing Then strServerName = ""
                If strDBName Is Nothing Then strDBName = ""
                If strDXMC Is Nothing Then strDXMC = ""
                If strDXLX Is Nothing Then strDXLX = ""
                If strWhere Is Nothing Then strWhere = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                strServerName = strServerName.Trim()
                strDBName = strDBName.Trim()
                strDXMC = strDXMC.Trim()
                strDXLX = strDXLX.Trim()
                strWhere = strWhere.Trim()
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
                    objTempDuixiangData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_SHUJUKU_DUIXIANG)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from 管理_B_数据库_对象 a " + vbCr
                        strSQL = strSQL + " where a.服务器名 = @fwqm" + vbCr
                        strSQL = strSQL + " and   a.数据库名 = @sjkm" + vbCr
                        strSQL = strSQL + " and   a.对象名称 = @dxmc" + vbCr
                        strSQL = strSQL + " and   a.对象类型 = @dxlx" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " and " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.服务器名,a.数据库名,a.对象类型,a.对象名称 " + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@fwqm", strServerName)
                        objSqlCommand.Parameters.AddWithValue("@sjkm", strDBName)
                        objSqlCommand.Parameters.AddWithValue("@dxmc", strDXMC)
                        objSqlCommand.Parameters.AddWithValue("@dxlx", strDXLX)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempDuixiangData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_DUIXIANG))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDuixiangData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempDuixiangData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objDuixiangData = objTempDuixiangData
            getDuixiangData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempDuixiangData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据对象标识获取“管理_B_数据库_对象”的数据集
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     intDXBS              ：对象标识
        '     strWhere             ：搜索字符串(默认表前缀a.)
        '     objDuixiangData      ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDuixiangData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intDXBS As Integer, _
            ByVal strWhere As String, _
            ByRef objDuixiangData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempDuixiangData As Xydc.Platform.Common.Data.AppManagerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getDuixiangData = False
            objDuixiangData = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strWhere Is Nothing Then strWhere = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                strWhere = strWhere.Trim()
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
                    objTempDuixiangData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_SHUJUKU_DUIXIANG)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from 管理_B_数据库_对象 a " + vbCr
                        strSQL = strSQL + " where a.对象标识 = @dxbs" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " and " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.服务器名,a.数据库名,a.对象类型,a.对象名称 " + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@dxbs", intDXBS)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempDuixiangData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_DUIXIANG))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDuixiangData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempDuixiangData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objDuixiangData = objTempDuixiangData
            getDuixiangData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempDuixiangData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 检查“管理_B_数据库_数据库”的数据的合法性
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
        '     objNewData           ：新数据
        '     objenumEditType      ：编辑类型

        ' 返回
        '     True                 ：合法
        '     False                ：不合法或其他程序错误
        '----------------------------------------------------------------
        Public Function doVerifyShujukuData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.ListDictionary, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objListDictionary As New System.Collections.Specialized.ListDictionary
            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            doVerifyShujukuData = False

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "错误：未传入新的数据！"
                    GoTo errProc
                End If
                Dim strOldFWQMC As String
                Dim strOldSJKMC As String
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                    Case Else
                        If objOldData Is Nothing Then
                            strErrMsg = "错误：未传入旧的数据！"
                            GoTo errProc
                        End If
                        strOldFWQMC = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_SHUJUKU_FWQM), "")
                        strOldSJKMC = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_SHUJUKU_SJKM), "")
                End Select

                '获取表结构定义
                strSQL = "select top 0 * from 管理_B_数据库_数据库"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, strUserId, strPassword, strSQL, "管理_B_数据库_数据库", objDataSet) = False Then
                    GoTo errProc
                End If

                '检查数据长度
                Dim objDictionaryEntry As System.Collections.DictionaryEntry
                Dim strField As String
                Dim intLen As Integer
                For Each objDictionaryEntry In objNewData
                    strField = objPulicParameters.getObjectValue(objDictionaryEntry.Key, "")
                    Select Case strField
                        Case Else
                            Dim strValue As String
                            strValue = objPulicParameters.getObjectValue(objDictionaryEntry.Value, "")
                            If strValue = "" Then
                                Select Case strField
                                    Case Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_SHUJUKU_SM
                                    Case Else
                                        strErrMsg = "错误：[" + strField + "]不能为空！"
                                        GoTo errProc
                                End Select
                            End If
                            With objDataSet.Tables(0).Columns(strField)
                                intLen = objPulicParameters.getStringLength(strValue)
                                If intLen > .MaxLength Then
                                    strErrMsg = "错误：[" + strField + "]长度不能超过[" + .MaxLength.ToString() + "]，实际有[" + intLen.ToString() + "]！"
                                    GoTo errProc
                                End If
                            End With
                    End Select
                Next
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '检查：服务器名+数据库名
                Dim strFWQMC As String
                Dim strSJKMC As String
                strFWQMC = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_SHUJUKU_FWQM), "")
                strSJKMC = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_SHUJUKU_SJKM), "")
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                        strSQL = ""
                        strSQL = strSQL + " select * from 管理_B_数据库_数据库 "
                        strSQL = strSQL + " where 服务器名 = @fwqm and 数据库名=@sjkm"
                        objListDictionary.Add("@fwqm", strFWQMC)
                        objListDictionary.Add("@sjkm", strSJKMC)
                    Case Else
                        strSQL = ""
                        strSQL = strSQL + " select * from 管理_B_数据库_数据库 "
                        strSQL = strSQL + " where 服务器名 = @fwqm and 数据库名=@sjkm "
                        strSQL = strSQL + " and   not (服务器名 = @oldfwqm and 数据库名=@oldsjkm) "
                        objListDictionary.Add("@fwqm", strFWQMC)
                        objListDictionary.Add("@sjkm", strSJKMC)
                        objListDictionary.Add("@oldfwqm", strOldFWQMC)
                        objListDictionary.Add("@oldsjkm", strOldSJKMC)
                End Select
                If objdacCommon.getDataSetBySQL(strErrMsg, strUserId, strPassword, strSQL, objListDictionary, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strErrMsg = "错误：[" + strFWQMC + "+" + strSJKMC + "]已经存在！"
                    GoTo errProc
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing
                objListDictionary.Clear()
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objListDictionary)

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objListDictionary)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            doVerifyShujukuData = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objListDictionary)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 检查“管理_B_数据库_对象”的数据的合法性
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
        '     objNewData           ：新数据
        '     objenumEditType      ：编辑类型

        ' 返回
        '     True                 ：合法
        '     False                ：不合法或其他程序错误
        '----------------------------------------------------------------
        Public Function doVerifyDuixiangData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.ListDictionary, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objListDictionary As New System.Collections.Specialized.ListDictionary
            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            doVerifyDuixiangData = False

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "错误：未传入新的数据！"
                    GoTo errProc
                End If
                Dim intOldDXBS As Integer
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                    Case Else
                        If objOldData Is Nothing Then
                            strErrMsg = "错误：未传入旧的数据！"
                            GoTo errProc
                        End If
                        intOldDXBS = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_DUIXIANG_DXBS), 0)
                End Select

                '获取表结构定义
                strSQL = "select top 0 * from 管理_B_数据库_对象"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, strUserId, strPassword, strSQL, "管理_B_数据库_对象", objDataSet) = False Then
                    GoTo errProc
                End If

                '检查数据长度
                Dim objDictionaryEntry As System.Collections.DictionaryEntry
                Dim strField As String
                Dim intLen As Integer
                For Each objDictionaryEntry In objNewData
                    strField = objPulicParameters.getObjectValue(objDictionaryEntry.Key, "")
                    Select Case strField
                        Case Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_DUIXIANG_DXBS
                            '自动值，不检查
                        Case Else
                            Dim strValue As String
                            strValue = objPulicParameters.getObjectValue(objDictionaryEntry.Value, "")
                            If strValue = "" Then
                                Select Case strField
                                    Case Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_DUIXIANG_SM
                                    Case Else
                                        strErrMsg = "错误：[" + strField + "]不能为空！"
                                        GoTo errProc
                                End Select
                            End If
                            With objDataSet.Tables(0).Columns(strField)
                                intLen = objPulicParameters.getStringLength(strValue)
                                If intLen > .MaxLength Then
                                    strErrMsg = "错误：[" + strField + "]长度不能超过[" + .MaxLength.ToString() + "]，实际有[" + intLen.ToString() + "]！"
                                    GoTo errProc
                                End If
                            End With
                    End Select
                Next
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '检查：服务器名+数据库名
                Dim strFWQMC As String
                Dim strSJKMC As String
                Dim strDXMC As String
                Dim strDXLX As String
                strFWQMC = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_DUIXIANG_FWQM), "")
                strSJKMC = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_DUIXIANG_SJKM), "")
                strDXMC = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_DUIXIANG_DXMC), "")
                strDXLX = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_DUIXIANG_DXLX), "")
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                        strSQL = ""
                        strSQL = strSQL + " select * from 管理_B_数据库_对象 "
                        strSQL = strSQL + " where 服务器名 = @fwqm "
                        strSQL = strSQL + " and   数据库名 = @sjkm"
                        strSQL = strSQL + " and   对象类型 = @dxlx"
                        strSQL = strSQL + " and   对象名称 = @dxmc"
                        objListDictionary.Add("@fwqm", strFWQMC)
                        objListDictionary.Add("@sjkm", strSJKMC)
                        objListDictionary.Add("@dxlx", strDXLX)
                        objListDictionary.Add("@dxmc", strDXMC)
                    Case Else
                        strSQL = ""
                        strSQL = strSQL + " select * from 管理_B_数据库_对象 "
                        strSQL = strSQL + " where 服务器名 = @fwqm "
                        strSQL = strSQL + " and   数据库名 = @sjkm"
                        strSQL = strSQL + " and   对象类型 = @dxlx"
                        strSQL = strSQL + " and   对象名称 = @dxmc"
                        strSQL = strSQL + " and   对象标识 <> @olddxbs "
                        objListDictionary.Add("@fwqm", strFWQMC)
                        objListDictionary.Add("@sjkm", strSJKMC)
                        objListDictionary.Add("@dxlx", strDXLX)
                        objListDictionary.Add("@dxmc", strDXMC)
                        objListDictionary.Add("@olddxbs", intOldDXBS)
                End Select
                If objdacCommon.getDataSetBySQL(strErrMsg, strUserId, strPassword, strSQL, objListDictionary, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strErrMsg = "错误：[" + strFWQMC + "+" + strSJKMC + "+" + strDXLX + "]+" + strDXMC + "已经存在！"
                    GoTo errProc
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing
                objListDictionary.Clear()
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objListDictionary)

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objListDictionary)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            doVerifyDuixiangData = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objListDictionary)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 保存“管理_B_数据库_数据库”的数据
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
        Public Function doSaveShujukuData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.ListDictionary, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '初始化
            doSaveShujukuData = False
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "错误：未传入新的数据！"
                    GoTo errProc
                End If
                Dim strOldFWQMC As String
                Dim strOldSJKMC As String
                Dim strNewFWQMC As String
                Dim strNewSJKMC As String
                strNewFWQMC = objPulicParameters.getObjectValue(objNewData.Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_SHUJUKU_FWQM), "")
                strNewSJKMC = objPulicParameters.getObjectValue(objNewData.Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_SHUJUKU_SJKM), "")
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                    Case Else
                        If objOldData Is Nothing Then
                            strErrMsg = "错误：未传入旧的数据！"
                            GoTo errProc
                        End If
                        strOldFWQMC = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_SHUJUKU_FWQM), "")
                        strOldSJKMC = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_SHUJUKU_SJKM), "")
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
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '计算SQL
                    Dim objDictionaryEntry As System.Collections.DictionaryEntry
                    Dim strFileds As String = ""
                    Dim strValues As String = ""
                    Dim strField As String
                    Dim i As Integer = 0
                    Select Case objenumEditType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                            For Each objDictionaryEntry In objNewData
                                If strFileds = "" Then
                                    strFileds = objPulicParameters.getObjectValue(objDictionaryEntry.Key, "")
                                Else
                                    strFileds = strFileds + "," + objPulicParameters.getObjectValue(objDictionaryEntry.Key, "")
                                End If
                                If strValues = "" Then
                                    strValues = "@A" + i.ToString()
                                Else
                                    strValues = strValues + "," + "@A" + i.ToString()
                                End If
                                i += 1
                            Next
                            strSQL = ""
                            strSQL = strSQL + " insert into 管理_B_数据库_数据库 (" + strFileds + ")"
                            strSQL = strSQL + " values (" + strValues + ")"
                            objSqlCommand.Parameters.Clear()
                            i = 0
                            For Each objDictionaryEntry In objNewData
                                objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), objDictionaryEntry.Value)
                                i += 1
                            Next
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()

                        Case Else
                            For Each objDictionaryEntry In objNewData
                                If strFileds = "" Then
                                    strFileds = objPulicParameters.getObjectValue(objDictionaryEntry.Key, "") + " = @A" + i.ToString()
                                Else
                                    strFileds = strFileds + "," + objPulicParameters.getObjectValue(objDictionaryEntry.Key, "") + " = @A" + i.ToString()
                                End If
                                i += 1
                            Next
                            strSQL = ""
                            strSQL = strSQL + " update 管理_B_数据库_数据库 set "
                            strSQL = strSQL + "   " + strFileds
                            strSQL = strSQL + " where 服务器名 = @oldfwqm"
                            strSQL = strSQL + " and   数据库名 = @oldsjkm"
                            objSqlCommand.Parameters.Clear()
                            i = 0
                            For Each objDictionaryEntry In objNewData
                                objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), objDictionaryEntry.Value)
                                i += 1
                            Next
                            objSqlCommand.Parameters.AddWithValue("@oldfwqm", strOldFWQMC)
                            objSqlCommand.Parameters.AddWithValue("@oldsjkm", strOldSJKMC)
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()

                            If (strNewFWQMC.ToUpper() = strOldFWQMC.ToUpper() And strNewSJKMC.ToUpper() = strOldSJKMC.ToUpper()) = False Then
                                strSQL = ""
                                strSQL = strSQL + " update 管理_B_数据库_对象 set "
                                strSQL = strSQL + "   服务器名 = @newfwqm,"
                                strSQL = strSQL + "   数据库名 = @newsjkm "
                                strSQL = strSQL + " where 服务器名 = @oldfwqm"
                                strSQL = strSQL + " and   数据库名 = @oldsjkm"
                                objSqlCommand.Parameters.Clear()
                                objSqlCommand.Parameters.AddWithValue("@newfwqm", strNewFWQMC)
                                objSqlCommand.Parameters.AddWithValue("@newsjkm", strNewSJKMC)
                                objSqlCommand.Parameters.AddWithValue("@oldfwqm", strOldFWQMC)
                                objSqlCommand.Parameters.AddWithValue("@oldsjkm", strOldSJKMC)
                                objSqlCommand.CommandText = strSQL
                                objSqlCommand.ExecuteNonQuery()
                            End If
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
            doSaveShujukuData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 保存“管理_B_数据库_对象”的数据
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
        Public Function doSaveDuixiangData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.ListDictionary, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '初始化
            doSaveDuixiangData = False
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "错误：未传入新的数据！"
                    GoTo errProc
                End If
                Dim intOldDXBS As Integer
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                    Case Else
                        If objOldData Is Nothing Then
                            strErrMsg = "错误：未传入旧的数据！"
                            GoTo errProc
                        End If
                        intOldDXBS = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_DUIXIANG_DXBS), 0)
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
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '计算SQL
                    Dim objDictionaryEntry As System.Collections.DictionaryEntry
                    Dim strFileds As String = ""
                    Dim strValues As String = ""
                    Dim strField As String
                    Dim i As Integer = 0
                    Select Case objenumEditType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                            For Each objDictionaryEntry In objNewData
                                Select Case CType(objDictionaryEntry.Key, String)
                                    Case Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_DUIXIANG_DXBS
                                        '自动值
                                    Case Else
                                        If strFileds = "" Then
                                            strFileds = objPulicParameters.getObjectValue(objDictionaryEntry.Key, "")
                                        Else
                                            strFileds = strFileds + "," + objPulicParameters.getObjectValue(objDictionaryEntry.Key, "")
                                        End If
                                        If strValues = "" Then
                                            strValues = "@A" + i.ToString()
                                        Else
                                            strValues = strValues + "," + "@A" + i.ToString()
                                        End If
                                        i += 1
                                End Select
                            Next
                            strSQL = ""
                            strSQL = strSQL + " insert into 管理_B_数据库_对象 (" + strFileds + ")"
                            strSQL = strSQL + " values (" + strValues + ")"
                            objSqlCommand.Parameters.Clear()
                            i = 0
                            For Each objDictionaryEntry In objNewData
                                Select Case CType(objDictionaryEntry.Key, String)
                                    Case Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_DUIXIANG_DXBS
                                    Case Else
                                        objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), objDictionaryEntry.Value)
                                        i += 1
                                End Select
                            Next
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()

                        Case Else
                            For Each objDictionaryEntry In objNewData
                                Select Case CType(objDictionaryEntry.Key, String)
                                    Case Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_DUIXIANG_DXBS
                                    Case Else
                                        If strFileds = "" Then
                                            strFileds = objPulicParameters.getObjectValue(objDictionaryEntry.Key, "") + " = @A" + i.ToString()
                                        Else
                                            strFileds = strFileds + "," + objPulicParameters.getObjectValue(objDictionaryEntry.Key, "") + " = @A" + i.ToString()
                                        End If
                                        i += 1
                                End Select
                            Next
                            strSQL = ""
                            strSQL = strSQL + " update 管理_B_数据库_对象 set "
                            strSQL = strSQL + "   " + strFileds
                            strSQL = strSQL + " where 对象标识 = @oldDXBS"
                            objSqlCommand.Parameters.Clear()
                            i = 0
                            For Each objDictionaryEntry In objNewData
                                Select Case CType(objDictionaryEntry.Key, String)
                                    Case Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_DUIXIANG_DXBS
                                    Case Else
                                        objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), objDictionaryEntry.Value)
                                        i += 1
                                End Select
                            Next
                            objSqlCommand.Parameters.AddWithValue("@oldDXBS", intOldDXBS)
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
            doSaveDuixiangData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 删除“管理_B_数据库_数据库”的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strServerName        ：服务器名
        '     strDBName            ：数据库名
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteShujukuData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strServerName As String, _
            ByVal strDBName As String) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            doDeleteShujukuData = False
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strServerName Is Nothing Then strServerName = ""
                If strDBName Is Nothing Then strDBName = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                strServerName = strServerName.Trim()
                strDBName = strDBName.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
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

                    '删除管理_B_数据库_对象
                    strSQL = ""
                    strSQL = strSQL + " delete from 管理_B_数据库_对象 "
                    strSQL = strSQL + " where 服务器名 = @fwqm"
                    strSQL = strSQL + " and   数据库名 = @sjkm"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@fwqm", strServerName)
                    objSqlCommand.Parameters.AddWithValue("@sjkm", strDBName)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '删除管理_B_数据库_数据库
                    strSQL = ""
                    strSQL = strSQL + " delete from 管理_B_数据库_数据库 "
                    strSQL = strSQL + " where 服务器名 = @fwqm"
                    strSQL = strSQL + " and   数据库名 = @sjkm"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@fwqm", strServerName)
                    objSqlCommand.Parameters.AddWithValue("@sjkm", strDBName)
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
            doDeleteShujukuData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 删除“管理_B_数据库_对象”的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strServerName        ：服务器名
        '     strDBName            ：数据库名
        '     strDXLX              ：对象类型
        '     strDXMC              ：对象名称
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteDuixiangData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strServerName As String, _
            ByVal strDBName As String, _
            ByVal strDXLX As String, _
            ByVal strDXMC As String) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            doDeleteDuixiangData = False
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strServerName Is Nothing Then strServerName = ""
                If strDBName Is Nothing Then strDBName = ""
                If strDXLX Is Nothing Then strDXLX = ""
                If strDXMC Is Nothing Then strDXMC = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                strServerName = strServerName.Trim()
                strDBName = strDBName.Trim()
                strDXLX = strDXLX.Trim()
                strDXMC = strDXMC.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
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

                    '删除管理_B_数据库_对象
                    strSQL = ""
                    strSQL = strSQL + " delete from 管理_B_数据库_对象 "
                    strSQL = strSQL + " where 服务器名 = @fwqm"
                    strSQL = strSQL + " and   数据库名 = @sjkm"
                    strSQL = strSQL + " and   对象类型 = @dxlx"
                    strSQL = strSQL + " and   对象名称 = @dxmc"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@fwqm", strServerName)
                    objSqlCommand.Parameters.AddWithValue("@sjkm", strDBName)
                    objSqlCommand.Parameters.AddWithValue("@dxlx", strDXLX)
                    objSqlCommand.Parameters.AddWithValue("@dxmc", strDXMC)
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
            doDeleteDuixiangData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 删除“管理_B_数据库_对象”的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     intDXBS              ：对象标识
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteDuixiangData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intDXBS As Integer) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            doDeleteDuixiangData = False
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
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

                    '删除管理_B_数据库_对象
                    strSQL = ""
                    strSQL = strSQL + " delete from 管理_B_数据库_对象 "
                    strSQL = strSQL + " where 对象标识 = @dxbs"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@dxbs", intDXBS)
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
            doDeleteDuixiangData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 自动清除管理_B_数据库_数据库、管理_B_数据库_对象中的无效数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doAutoCleanManageData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            doAutoCleanManageData = False
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
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

                '清理数据
                Dim strSQL As String
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '删除管理_B_数据库_对象
                    strSQL = ""
                    strSQL = strSQL + " delete 管理_B_数据库_对象 " + vbCr
                    strSQL = strSQL + " from 管理_B_数据库_对象 a " + vbCr
                    strSQL = strSQL + " left join " + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select 服务器名=名称 " + vbCr
                    strSQL = strSQL + "   from 管理_B_数据库_服务器 " + vbCr
                    strSQL = strSQL + "   group by 名称" + vbCr
                    strSQL = strSQL + " ) b on a.服务器名 = b.服务器名 " + vbCr
                    strSQL = strSQL + " where b.服务器名 is null " + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    strSQL = ""
                    strSQL = strSQL + " delete 管理_B_数据库_对象 " + vbCr
                    strSQL = strSQL + " from 管理_B_数据库_对象 a " + vbCr
                    strSQL = strSQL + " left join " + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select 服务器名,数据库名 " + vbCr
                    strSQL = strSQL + "   from 管理_B_数据库_数据库 " + vbCr
                    strSQL = strSQL + "   group by 服务器名,数据库名" + vbCr
                    strSQL = strSQL + " ) b on a.服务器名 = b.服务器名 and a.数据库名=b.数据库名 " + vbCr
                    strSQL = strSQL + " where b.服务器名 is null " + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '删除管理_B_数据库_对象
                    strSQL = ""
                    strSQL = strSQL + " delete 管理_B_数据库_数据库 " + vbCr
                    strSQL = strSQL + " from 管理_B_数据库_数据库 a " + vbCr
                    strSQL = strSQL + " left join " + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select 服务器名=名称 " + vbCr
                    strSQL = strSQL + "   from 管理_B_数据库_服务器 " + vbCr
                    strSQL = strSQL + "   group by 名称" + vbCr
                    strSQL = strSQL + " ) b on a.服务器名 = b.服务器名 " + vbCr
                    strSQL = strSQL + " where b.服务器名 is null " + vbCr
                    objSqlCommand.Parameters.Clear()
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
            doAutoCleanManageData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取指定objConnectionProperty中的数据库角色
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     objConnectionProperty ：服务器信息
        '     strWhere                    ：搜索字符串(默认表前缀a.)
        '     objRoleData                 ：信息数据集
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getRoleData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strWhere As String, _
            ByRef objRoleData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempRoleData As Xydc.Platform.Common.Data.AppManagerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getRoleData = False
            objRoleData = Nothing
            strErrMsg = ""

            Try
                '检查
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim()
                If objConnectionProperty Is Nothing Then
                    '创建数据集
                    objTempRoleData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_SHUJUKU_JIAOSE)
                    Exit Try
                End If

                '获取连接
                With objConnectionProperty

                    'If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, -1, .InitialCatalog, .DataSource) = False Then
                    '    GoTo errProc
                    'End If
                    If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, Platform.Common.jsoaConfiguration.ConnectionTestTimeout, .InitialCatalog, .DataSource) = False Then
                        GoTo errProc
                    End If

                End With

                '获取数据
                Dim strSQL As String
                Try
                    '创建数据集
                    objTempRoleData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_SHUJUKU_JIAOSE)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.uid,a.name " + vbCr
                        strSQL = strSQL + " from " + objConnectionProperty.InitialCatalog + ".dbo.sysusers a" + vbCr
                        strSQL = strSQL + " where issqlrole = 1" + vbCr   '角色
                        strSQL = strSQL + " and gid > 0" + vbCr           '非系统角色
                        If strWhere <> "" Then
                            strSQL = strSQL + " and " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.name"

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempRoleData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_JIAOSE))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempRoleData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempRoleData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objRoleData = objTempRoleData
            getRoleData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempRoleData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取人员已经加入到角色strRoleName的列表
        '----------------------------------------------------------------
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     objConnectionProperty       ：服务器信息
        '     strWhere                    ：搜索字符串(默认表前缀a.)
        '     objRoleData                 ：信息数据集
        '     blnNone                     ：重载
        ' 返回
        '     True                        ：成功
        '     False                       ：失败

        '----------------------------------------------------------------
        Public Function getRoleData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strWhere As String, _
            ByRef objRoleData As Xydc.Platform.Common.Data.AppManagerData, _
            ByVal blnNone As Boolean) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempRoleData As Xydc.Platform.Common.Data.AppManagerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getRoleData = False
            objRoleData = Nothing
            strErrMsg = ""

            Try
                '检查
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim()
                If objConnectionProperty Is Nothing Then
                    '创建数据集
                    objTempRoleData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_SHUJUKU_JIAOSE)
                    Exit Try
                End If

                '获取连接
                With objConnectionProperty

                    'If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, -1, .InitialCatalog, .DataSource) = False Then
                    '    GoTo errProc
                    'End If
                    If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, Platform.Common.jsoaConfiguration.ConnectionTestTimeout, .InitialCatalog, .DataSource) = False Then
                        GoTo errProc
                    End If

                End With

                '获取数据
                Dim strSQL As String
                Try
                    '创建数据集
                    objTempRoleData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_SHUJUKU_JIAOSE)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        Dim strDefDB As String = Xydc.Platform.Common.jsoaConfiguration.DatabaseServerUserDB
                        Dim strDatabase As String = objConnectionProperty.InitialCatalog

                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.gid as 'UID',a.rollname as 'NAME' from ( " + vbCr
                        strSQL = strSQL + " select a.*,b.*,c.name  from  " + strDatabase + ".dbo.sysmembers a " + vbCr
                        strSQL = strSQL + " Left Join  " + vbCr
                        strSQL = strSQL + " ( " + vbCr
                        strSQL = strSQL + " select gid,name as 'rollname' from  " + strDatabase + ".dbo.sysusers " + vbCr
                        strSQL = strSQL + " where(issqlrole = 1 And gid > 0) " + vbCr
                        strSQL = strSQL + " ) b on a.groupuid = b.gid " + vbCr
                        strSQL = strSQL + " left join  " + strDatabase + ".dbo.sysusers c on a.memberuid = c.uid " + vbCr
                        strSQL = strSQL + " where(b.gid Is Not null) " + vbCr
                        strSQL = strSQL + " and c.uid is not null " + vbCr
                        strSQL = strSQL + " ) a "
                        If strWhere <> "" Then
                            strSQL = strSQL + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.name"

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempRoleData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_JIAOSE))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempRoleData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempRoleData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objRoleData = objTempRoleData
            getRoleData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempRoleData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取已经加入到角色strRoleName的人员列表(含人员的全部连接数据)
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     objConnectionProperty       ：服务器信息
        '     strRoleName                 ：角色名
        '     strWhere                    ：搜索字符串(默认表前缀a.)
        '     objRenyuanData              ：指定组织机构下的人员信息数据集
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getRenyuanInRoleData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strRoleName As String, _
            ByVal strWhere As String, _
            ByRef objRenyuanData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempRenyuanData As Xydc.Platform.Common.Data.CustomerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getRenyuanInRoleData = False
            objRenyuanData = Nothing
            strErrMsg = ""

            Try
                '检查
                If strRoleName Is Nothing Then strRoleName = ""
                If strWhere Is Nothing Then strWhere = ""
                strRoleName = strRoleName.Trim()
                strWhere = strWhere.Trim()
                If objConnectionProperty Is Nothing Then
                    '创建数据集
                    objTempRenyuanData = New Xydc.Platform.Common.Data.CustomerData(Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_RENYUAN_FULLJOIN)
                    Exit Try
                End If

                '不同服务器
                If objConnectionProperty.DataSource.ToUpper() <> Xydc.Platform.Common.jsoaConfiguration.DatabaseServerName.ToUpper() Then
                    '创建数据集
                    objTempRenyuanData = New Xydc.Platform.Common.Data.CustomerData(Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_RENYUAN_FULLJOIN)
                    Exit Try
                End If

                '获取连接
                With objConnectionProperty

                    'If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, -1, .InitialCatalog, .DataSource) = False Then
                    '    GoTo errProc
                    'End If
                    If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, Platform.Common.jsoaConfiguration.ConnectionTestTimeout, .InitialCatalog, .DataSource) = False Then
                        GoTo errProc
                    End If

                End With

                '获取数据
                Dim strSQL As String
                Try
                    '创建数据集
                    objTempRenyuanData = New Xydc.Platform.Common.Data.CustomerData(Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_RENYUAN_FULLJOIN)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        Dim strDefDB As String = Xydc.Platform.Common.jsoaConfiguration.DatabaseServerUserDB
                        Dim strDatabase As String = objConnectionProperty.InitialCatalog

                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* from ("
                        strSQL = strSQL + "   select a.*," + vbCr
                        strSQL = strSQL + "     b.组织名称,b.组织别名," + vbCr
                        strSQL = strSQL + "     岗位列表 = " + strDefDB + ".dbo.GetGWMCByRydm(a.人员代码,@separate)," + vbCr
                        strSQL = strSQL + "     c.级别名称,c.行政级别," + vbCr
                        strSQL = strSQL + "     秘书名称 = d.人员名称," + vbCr
                        strSQL = strSQL + "     其他由转送名称 = e.人员名称," + vbCr
                        strSQL = strSQL + "     是否申请 = @charfalse" + vbCr
                        strSQL = strSQL + "   from " + strDefDB + ".dbo.公共_B_人员 a " + vbCr
                        strSQL = strSQL + "   left join " + strDefDB + ".dbo.公共_B_组织机构 b on a.组织代码   = b.组织代码 " + vbCr
                        strSQL = strSQL + "   left join " + strDefDB + ".dbo.公共_B_行政级别 c on a.级别代码   = c.级别代码 " + vbCr
                        strSQL = strSQL + "   left join " + strDefDB + ".dbo.公共_B_人员     d on a.秘书代码   = d.人员代码 " + vbCr
                        strSQL = strSQL + "   left join " + strDefDB + ".dbo.公共_B_人员     e on a.其他由转送 = e.人员代码 " + vbCr
                        strSQL = strSQL + "   left join" + vbCr
                        strSQL = strSQL + "   (" + vbCr
                        strSQL = strSQL + "     select c.name" + vbCr
                        strSQL = strSQL + "     from " + strDatabase + ".dbo.sysmembers a " + vbCr
                        strSQL = strSQL + "     left join " + vbCr
                        strSQL = strSQL + "     (" + vbCr
                        strSQL = strSQL + "       select gid from " + strDatabase + ".dbo.sysusers " + vbCr
                        strSQL = strSQL + "       where issqlrole=1 and gid>0" + vbCr
                        strSQL = strSQL + "       and name = @rolename" + vbCr
                        strSQL = strSQL + "     ) b on a.groupuid = b.gid" + vbCr
                        strSQL = strSQL + "     left join " + strDatabase + ".dbo.sysusers c on a.memberuid = c.uid" + vbCr
                        strSQL = strSQL + "     where b.gid is not null" + vbCr
                        strSQL = strSQL + "     and c.uid is not null" + vbCr
                        strSQL = strSQL + "   ) f on a.人员代码 = f.name" + vbCr
                        strSQL = strSQL + "   where f.name is not null" + vbCr        '角色内
                        strSQL = strSQL + " ) a " + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.组织代码, cast(a.人员序号 as integer)"

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@separate", Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate)
                        objSqlCommand.Parameters.AddWithValue("@charfalse", Xydc.Platform.Common.Utilities.PulicParameters.CharFalse)
                        objSqlCommand.Parameters.AddWithValue("@rolename", strRoleName)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempRenyuanData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempRenyuanData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempRenyuanData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objRenyuanData = objTempRenyuanData
            getRenyuanInRoleData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempRenyuanData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取没有加入到角色strRoleName的人员列表(含人员的全部连接数据)
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     objConnectionProperty       ：服务器信息
        '     strRoleName                 ：角色名
        '     strWhere                    ：搜索字符串(默认表前缀a.)
        '     objRenyuanData              ：指定组织机构下的人员信息数据集
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getRenyuanNotInRoleData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strRoleName As String, _
            ByVal strWhere As String, _
            ByRef objRenyuanData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempRenyuanData As Xydc.Platform.Common.Data.CustomerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getRenyuanNotInRoleData = False
            objRenyuanData = Nothing
            strErrMsg = ""

            Try
                '检查
                If strRoleName Is Nothing Then strRoleName = ""
                If strWhere Is Nothing Then strWhere = ""
                strRoleName = strRoleName.Trim()
                strWhere = strWhere.Trim()
                If objConnectionProperty Is Nothing Then
                    '创建数据集
                    objTempRenyuanData = New Xydc.Platform.Common.Data.CustomerData(Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_RENYUAN_FULLJOIN)
                    Exit Try
                End If

                '不同服务器
                If objConnectionProperty.DataSource.ToUpper() <> Xydc.Platform.Common.jsoaConfiguration.DatabaseServerName.ToUpper() Then
                    '创建数据集
                    objTempRenyuanData = New Xydc.Platform.Common.Data.CustomerData(Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_RENYUAN_FULLJOIN)
                    Exit Try
                End If

                '获取连接
                With objConnectionProperty

                    'If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, -1, .InitialCatalog, .DataSource) = False Then
                    '    GoTo errProc
                    'End If
                    If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, Platform.Common.jsoaConfiguration.ConnectionTestTimeout, .InitialCatalog, .DataSource) = False Then
                        GoTo errProc
                    End If

                End With

                '获取数据
                Dim strSQL As String
                Try
                    '创建数据集
                    objTempRenyuanData = New Xydc.Platform.Common.Data.CustomerData(Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_RENYUAN_FULLJOIN)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        Dim strDefDB As String = Xydc.Platform.Common.jsoaConfiguration.DatabaseServerUserDB
                        Dim strDatabase As String = objConnectionProperty.InitialCatalog

                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* from ("
                        strSQL = strSQL + "   select a.*," + vbCr
                        strSQL = strSQL + "     b.组织名称,b.组织别名," + vbCr
                        strSQL = strSQL + "     岗位列表 = " + strDefDB + ".dbo.GetGWMCByRydm(a.人员代码,@separate)," + vbCr
                        strSQL = strSQL + "     c.级别名称,c.行政级别," + vbCr
                        strSQL = strSQL + "     秘书名称 = d.人员名称," + vbCr
                        strSQL = strSQL + "     其他由转送名称 = e.人员名称," + vbCr
                        strSQL = strSQL + "     是否申请 = @charfalse" + vbCr
                        strSQL = strSQL + "   from " + strDefDB + ".dbo.公共_B_人员 a " + vbCr
                        strSQL = strSQL + "   left join " + strDefDB + ".dbo.公共_B_组织机构 b on a.组织代码   = b.组织代码 " + vbCr
                        strSQL = strSQL + "   left join " + strDefDB + ".dbo.公共_B_行政级别 c on a.级别代码   = c.级别代码 " + vbCr
                        strSQL = strSQL + "   left join " + strDefDB + ".dbo.公共_B_人员     d on a.秘书代码   = d.人员代码 " + vbCr
                        strSQL = strSQL + "   left join " + strDefDB + ".dbo.公共_B_人员     e on a.其他由转送 = e.人员代码 " + vbCr
                        strSQL = strSQL + "   left join" + vbCr
                        strSQL = strSQL + "   (" + vbCr
                        strSQL = strSQL + "     select c.name" + vbCr
                        strSQL = strSQL + "     from " + strDatabase + ".dbo.sysmembers a " + vbCr
                        strSQL = strSQL + "     left join " + vbCr
                        strSQL = strSQL + "     (" + vbCr
                        strSQL = strSQL + "       select gid from " + strDatabase + ".dbo.sysusers " + vbCr
                        strSQL = strSQL + "       where issqlrole=1 and gid>0" + vbCr
                        strSQL = strSQL + "       and name = @rolename" + vbCr
                        strSQL = strSQL + "     ) b on a.groupuid = b.gid" + vbCr
                        strSQL = strSQL + "     left join " + strDatabase + ".dbo.sysusers c on a.memberuid = c.uid" + vbCr
                        strSQL = strSQL + "     where b.gid is not null" + vbCr
                        strSQL = strSQL + "     and c.uid is not null" + vbCr
                        strSQL = strSQL + "   ) f on a.人员代码 = f.name" + vbCr
                        strSQL = strSQL + "   left join " + strDatabase + ".dbo.sysusers g on a.人员代码 = g.name" + vbCr
                        strSQL = strSQL + "   where f.name is null" + vbCr             '不在角色内
                        strSQL = strSQL + "   and   g.name is not null" + vbCr         '已经授权存取的人员
                        strSQL = strSQL + " ) a " + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.组织代码, cast(a.人员序号 as integer)"

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@separate", Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate)
                        objSqlCommand.Parameters.AddWithValue("@charfalse", Xydc.Platform.Common.Utilities.PulicParameters.CharFalse)
                        objSqlCommand.Parameters.AddWithValue("@rolename", strRoleName)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempRenyuanData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempRenyuanData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempRenyuanData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objRenyuanData = objTempRenyuanData
            getRenyuanNotInRoleData = True
            Exit Function

            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempRenyuanData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 在指定服务器objConnectionProperty中创建角色strRoleName
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     objConnectionProperty       ：服务器信息
        '     strRoleName                 ：角色名
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function doAddRole( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strRoleName As String) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            doAddRole = False
            strErrMsg = ""

            Try
                '检查
                If strRoleName Is Nothing Then strRoleName = ""
                strRoleName = strRoleName.Trim()
                If objConnectionProperty Is Nothing Then
                    strErrMsg = "错误：未指定服务器参数！"
                    GoTo errProc
                End If

                '获取连接
                With objConnectionProperty

                    'If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, -1, .InitialCatalog, .DataSource) = False Then
                    '    GoTo errProc
                    'End If
                    If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, Platform.Common.jsoaConfiguration.ConnectionTestTimeout, .InitialCatalog, .DataSource) = False Then
                        GoTo errProc
                    End If

                End With

                '获取数据
                Dim strSQL As String
                Try
                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行操作
                    strSQL = "exec sp_addrole @rolename"
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@rolename", strRoleName)
                    objSqlCommand.ExecuteNonQuery()

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
            doAddRole = True
            Exit Function

            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 在指定服务器objConnectionProperty中删除角色strRoleName
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     objConnectionProperty       ：服务器信息
        '     strRoleName                 ：角色名
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function doDropRole( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strRoleName As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            doDropRole = False
            strErrMsg = ""

            Try
                '检查
                If strRoleName Is Nothing Then strRoleName = ""
                strRoleName = strRoleName.Trim()
                If objConnectionProperty Is Nothing Then
                    strErrMsg = "错误：未指定服务器参数！"
                    GoTo errProc
                End If

                '获取连接
                With objConnectionProperty

                    'If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, -1, .InitialCatalog, .DataSource) = False Then
                    '    GoTo errProc
                    'End If
                    If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, Platform.Common.jsoaConfiguration.ConnectionTestTimeout, .InitialCatalog, .DataSource) = False Then
                        GoTo errProc
                    End If

                End With

                '获取数据
                Dim strSQL As String
                Try
                    Dim strDBName As String = objConnectionProperty.InitialCatalog

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '获取角色成员
                    strSQL = ""
                    strSQL = strSQL + " select c.name" + vbCr
                    strSQL = strSQL + " from " + strDBName + ".dbo.sysmembers a" + vbCr
                    strSQL = strSQL + " left join " + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select uid" + vbCr
                    strSQL = strSQL + "   from " + strDBName + ".dbo.sysusers" + vbCr
                    strSQL = strSQL + "   where issqlrole = 1 " + vbCr
                    strSQL = strSQL + "   and   gid > 0" + vbCr
                    strSQL = strSQL + "   and   name = @rolename" + vbCr
                    strSQL = strSQL + " ) b on a.groupuid = b.uid" + vbCr
                    strSQL = strSQL + " left join " + strDBName + ".dbo.sysusers c on a.memberuid = c.uid" + vbCr
                    strSQL = strSQL + " where b.uid is not null" + vbCr
                    strSQL = strSQL + " and   c.uid is not null" + vbCr
                    Dim objListDictionary As New System.Collections.Specialized.ListDictionary
                    Dim objDataSet As System.Data.DataSet
                    objListDictionary.Clear()
                    objListDictionary.Add("@rolename", strRoleName)
                    If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objListDictionary, objDataSet) = False Then
                        GoTo errProc
                    End If

                    '逐个删除角色成员
                    With objDataSet.Tables(0)
                        Dim intCount As Integer = .Rows.Count
                        Dim strName As String
                        Dim i As Integer
                        For i = 0 To intCount - 1 Step 1
                            strName = objPulicParameters.getObjectValue(.Rows(i).Item("name"), "")
                            strSQL = "exec sp_droprolemember @rolename, @membername"
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@rolename", strRoleName)
                            objSqlCommand.Parameters.AddWithValue("@membername", strName)
                            objSqlCommand.ExecuteNonQuery()
                        Next
                    End With
                    objListDictionary.Clear()
                    Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objListDictionary)
                    Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                    objDataSet = Nothing

                    '删除角色
                    strSQL = "exec sp_droprole @rolename"
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@rolename", strRoleName)
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doDropRole = True
            Exit Function

            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 在指定服务器objConnectionProperty指定角色strRoleName中加入成员
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     objConnectionProperty       ：服务器信息
        '     strRoleName                 ：角色名
        '     strMemberName               ：成员名
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function doAddRoleMember( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strRoleName As String, _
            ByVal strMemberName As String) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            doAddRoleMember = False
            strErrMsg = ""

            Try
                '检查
                If strRoleName Is Nothing Then strRoleName = ""
                If strMemberName Is Nothing Then strMemberName = ""
                strRoleName = strRoleName.Trim()
                strMemberName = strMemberName.Trim()
                If objConnectionProperty Is Nothing Then
                    strErrMsg = "错误：未指定服务器参数！"
                    GoTo errProc
                End If

                '获取连接
                With objConnectionProperty

                    'If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, -1, .InitialCatalog, .DataSource) = False Then
                    '    GoTo errProc
                    'End If
                    If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, Platform.Common.jsoaConfiguration.ConnectionTestTimeout, .InitialCatalog, .DataSource) = False Then
                        GoTo errProc
                    End If

                End With

                '获取数据
                Dim strSQL As String
                Try
                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行操作
                    strSQL = "exec sp_addrolemember @rolename, @membername"
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@rolename", strRoleName)
                    objSqlCommand.Parameters.AddWithValue("@membername", strMemberName)
                    objSqlCommand.ExecuteNonQuery()

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
            doAddRoleMember = True
            Exit Function

            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '-------------------------------------------------------------------------------------------
        ' 在指定服务器objConnectionProperty指定成员strUserId加入角色(m_objNewDataSet_ChoiceRole)中
        '-------------------------------------------------------------------------------------------
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     objConnectionProperty       ：服务器信息
        '     strUserId                   ：指定成员
        '     m_objNewDataSet_ChoiceRole  ：更新角色数据集
        '     m_objOldDataSet_ChoiceRole  ：原角色数据集
        ' 返回
        '     True                        ：成功
        '     False                       ：失败

        '----------------------------------------------------------------
        Public Function doAddRoleMember( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strUserId As String, _
            ByVal m_objNewDataSet_ChoiceRole As Xydc.Platform.Common.Data.AppManagerData, _
            ByVal m_objOldDataSet_ChoiceRole As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim strRoleName As String

            '初始化
            doAddRoleMember = False
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim()
                If objConnectionProperty Is Nothing Then
                    strErrMsg = "错误：未指定服务器参数！"
                    GoTo errProc
                End If

                '获取连接
                With objConnectionProperty

                    'If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, -1, .InitialCatalog, .DataSource) = False Then
                    '    GoTo errProc
                    'End If
                    If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, Platform.Common.jsoaConfiguration.ConnectionTestTimeout, .InitialCatalog, .DataSource) = False Then
                        GoTo errProc
                    End If

                End With

                '获取数据
                Dim strSQL As String
                Dim intNewCount As Integer
                Dim intOldCount As Integer
                Dim i As Integer
                Try
                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行删除操作
                    With m_objOldDataSet_ChoiceRole.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_JIAOSE)
                        intOldCount = .Rows.Count
                        For i = 0 To intOldCount - 1 Step 1
                            strRoleName = ""
                            strRoleName = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_JIAOSE_NAME), " ")
                            strSQL = "exec sp_droprolemember @rolename, @membername"
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@rolename", strRoleName)
                            objSqlCommand.Parameters.AddWithValue("@membername", strUserId)
                            objSqlCommand.ExecuteNonQuery()
                        Next i
                    End With

                    '执行加入操作
                    With m_objNewDataSet_ChoiceRole.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_JIAOSE)
                        intNewCount = .Rows.Count
                        For i = 0 To intNewCount - 1 Step 1
                            If .Rows(i).RowState <> DataRowState.Deleted Then
                                strRoleName = ""
                                strRoleName = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_JIAOSE_NAME), " ")


                                strSQL = "exec sp_addrolemember @rolename, @membername"
                                objSqlCommand.CommandText = strSQL
                                objSqlCommand.Parameters.Clear()
                                objSqlCommand.Parameters.AddWithValue("@rolename", strRoleName)
                                objSqlCommand.Parameters.AddWithValue("@membername", strUserId)
                                objSqlCommand.ExecuteNonQuery()
                            End If
                        Next i
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doAddRoleMember = True
            Exit Function

            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 在指定服务器objConnectionProperty指定角色strRoleName中删除成员
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     objConnectionProperty       ：服务器信息
        '     strRoleName                 ：角色名
        '     strMemberName               ：成员名
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function doDropRoleMember( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strRoleName As String, _
            ByVal strMemberName As String) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            doDropRoleMember = False
            strErrMsg = ""

            Try
                '检查
                If strRoleName Is Nothing Then strRoleName = ""
                If strMemberName Is Nothing Then strMemberName = ""
                strRoleName = strRoleName.Trim()
                strMemberName = strMemberName.Trim()
                If objConnectionProperty Is Nothing Then
                    strErrMsg = "错误：未指定服务器参数！"
                    GoTo errProc
                End If

                '获取连接
                With objConnectionProperty

                    'If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, -1, .InitialCatalog, .DataSource) = False Then
                    '    GoTo errProc
                    'End If
                    If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, Platform.Common.jsoaConfiguration.ConnectionTestTimeout, .InitialCatalog, .DataSource) = False Then
                        GoTo errProc
                    End If

                End With

                '获取数据
                Dim strSQL As String
                Try
                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行操作
                    strSQL = "exec sp_droprolemember @rolename, @membername"
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@rolename", strRoleName)
                    objSqlCommand.Parameters.AddWithValue("@membername", strMemberName)
                    objSqlCommand.ExecuteNonQuery()

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
            doDropRoleMember = True
            Exit Function

            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取角色的权限设置数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objConnectionProperty：连接参数
        '     strRoleName          ：角色名
        '     strWhere             ：搜索字符串(默认表前缀a.)
        '     objRoleQXData        ：角色权限数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getRolePermissionsData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strRoleName As String, _
            ByVal strWhere As String, _
            ByRef objRoleQXData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempRoleQXData As Xydc.Platform.Common.Data.AppManagerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getRolePermissionsData = False
            objRoleQXData = Nothing
            strErrMsg = ""

            Try
                '检查
                If strRoleName Is Nothing Then strRoleName = ""
                If strWhere Is Nothing Then strWhere = ""
                strRoleName = strRoleName.Trim()
                strWhere = strWhere.Trim()
                If objConnectionProperty Is Nothing Then
                    objTempRoleQXData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_SHUJUKU_DUIXIANGQX)
                    Exit Try
                End If

                '获取连接
                With objConnectionProperty

                    'If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, -1, .InitialCatalog, .DataSource) = False Then
                    '    GoTo errProc
                    'End If
                    If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, Platform.Common.jsoaConfiguration.ConnectionTestTimeout, .InitialCatalog, .DataSource) = False Then
                        GoTo errProc
                    End If

                End With

                '获取数据
                Dim strSQL As String
                Try
                    '创建数据集
                    objTempRoleQXData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_SHUJUKU_DUIXIANGQX)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        Dim strDefServer As String = Xydc.Platform.Common.jsoaConfiguration.DatabaseServerName
                        Dim strDefDB As String = Xydc.Platform.Common.jsoaConfiguration.DatabaseServerUserDB
                        Dim strCurServer As String = objConnectionProperty.DataSource
                        Dim strCurDB As String = objConnectionProperty.InitialCatalog
                        Dim strXType As String = Xydc.Platform.Common.Data.AppManagerData.OBJECTTYPELIST

                        If strCurServer.ToUpper() = strDefServer.ToUpper() Then
                            '同一服务器

                            '准备SQL
                            strSQL = ""
                            strSQL = strSQL + " select a.*" + vbCr
                            strSQL = strSQL + " from" + vbCr
                            strSQL = strSQL + " (" + vbCr
                            strSQL = strSQL + "   select " + vbCr
                            strSQL = strSQL + "     a.对象名称,a.对象类型," + vbCr
                            strSQL = strSQL + "     对象中文名 = case when c.对象名称 is null then a.对象名称 else c.对象中文名 end," + vbCr
                            strSQL = strSQL + "     选择权 = case when b.选择权=1 then @True else @False end,"
                            strSQL = strSQL + "     编辑权 = case when b.编辑权=1 then @True else @False end,"
                            strSQL = strSQL + "     增加权 = case when b.增加权=1 then @True else @False end,"
                            strSQL = strSQL + "     删除权 = case when b.删除权=1 then @True else @False end,"
                            strSQL = strSQL + "     执行权 = case when b.执行权=1 then @True else @False end "
                            strSQL = strSQL + "   from " + vbCr
                            strSQL = strSQL + "   (  " + vbCr
                            strSQL = strSQL + "     select 对象名称=name,对象类型=xtype" + vbCr
                            strSQL = strSQL + "     from " + strCurDB + ".dbo.sysobjects " + vbCr
                            strSQL = strSQL + "     where xtype in (" + strXType + ")" + vbCr
                            strSQL = strSQL + "   ) a " + vbCr
                            strSQL = strSQL + "   left join " + vbCr
                            strSQL = strSQL + "   (" + vbCr
                            strSQL = strSQL + " select 对象名称,对象类型,选择权=sum(选择权),编辑权=sum(编辑权),增加权=sum(增加权),删除权=sum(删除权),执行权=sum(执行权) from"
                            strSQL = strSQL + "  ("
                            strSQL = strSQL + "     select "
                            strSQL = strSQL + "     对象名称 = b.name,"
                            strSQL = strSQL + "     对象类型 = b.xtype,"
                            strSQL = strSQL + "     选择权   = case when a.type='SL' then 1 else 0 end,"
                            strSQL = strSQL + "     编辑权   = case when a.type='UP' then 1 else 0 end,"
                            strSQL = strSQL + "     增加权   = case when a.type='IN' then 1 else 0 end,"
                            strSQL = strSQL + "     删除权   = case when a.type='DL' then 1 else 0 end,"
                            strSQL = strSQL + "     执行权   = case when a.type='EX' then 1 else 0 end "
                            strSQL = strSQL + "     from " + strCurDB + ".sys.database_permissions a " + vbCr
                            strSQL = strSQL + "     left join " + strCurDB + ".dbo.sysobjects b on a.major_id=b.id " + vbCr
                            strSQL = strSQL + "     left join " + strCurDB + ".dbo.sysusers   c on a.grantee_principal_id=c.uid" + vbCr
                            strSQL = strSQL + "     where c.issqlrole = 1 " + vbCr
                            strSQL = strSQL + "     and   c.gid > 0" + vbCr
                            strSQL = strSQL + "     and   c.name = @rolename" + vbCr
                            strSQL = strSQL + "     )a group by a.对象类型,a.对象名称"
                            strSQL = strSQL + "   ) b on a.对象名称=b.对象名称 and a.对象类型=b.对象类型" + vbCr
                            strSQL = strSQL + "   left join" + vbCr
                            strSQL = strSQL + "   (" + vbCr
                            strSQL = strSQL + "     select * from " + strDefDB + ".dbo.管理_B_数据库_对象" + vbCr
                            strSQL = strSQL + "     where 服务器名 = @server" + vbCr
                            strSQL = strSQL + "     and   数据库名 = @dbname" + vbCr
                            strSQL = strSQL + "   ) c on a.对象名称=c.对象名称 and a.对象类型=c.对象类型" + vbCr
                            strSQL = strSQL + " ) a" + vbCr
                            If strWhere <> "" Then
                                strSQL = strSQL + " where " + strWhere + vbCr
                            End If
                            strSQL = strSQL + " order by a.对象类型,a.对象名称" + vbCr

                            '设置参数
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@False", Xydc.Platform.Common.Utilities.PulicParameters.CharFalse)
                            objSqlCommand.Parameters.AddWithValue("@True", Xydc.Platform.Common.Utilities.PulicParameters.CharTrue)
                            objSqlCommand.Parameters.AddWithValue("@rolename", strRoleName)
                            objSqlCommand.Parameters.AddWithValue("@server", strCurServer)
                            objSqlCommand.Parameters.AddWithValue("@dbname", strCurDB)
                            .SelectCommand = objSqlCommand
                        Else
                            '不同服务器

                            '准备SQL
                            strSQL = ""
                            strSQL = strSQL + " select a.*" + vbCr
                            strSQL = strSQL + " from" + vbCr
                            strSQL = strSQL + " (" + vbCr
                            strSQL = strSQL + "   select " + vbCr
                            strSQL = strSQL + "     a.对象名称,a.对象类型," + vbCr
                            strSQL = strSQL + "     对象中文名=a.对象名称," + vbCr
                            strSQL = strSQL + "     选择权 = case when b.选择权=1 then @True else @False end,"
                            strSQL = strSQL + "     编辑权 = case when b.编辑权=1 then @True else @False end,"
                            strSQL = strSQL + "     增加权 = case when b.增加权=1 then @True else @False end,"
                            strSQL = strSQL + "     删除权 = case when b.删除权=1 then @True else @False end,"
                            strSQL = strSQL + "     执行权 = case when b.执行权=1 then @True else @False end "
                            strSQL = strSQL + "   from " + vbCr
                            strSQL = strSQL + "   (  " + vbCr
                            strSQL = strSQL + "     select 对象名称=name,对象类型=xtype" + vbCr
                            strSQL = strSQL + "     from " + strCurDB + ".dbo.sysobjects " + vbCr
                            strSQL = strSQL + "     where xtype in (" + strXType + ")" + vbCr
                            strSQL = strSQL + "   ) a " + vbCr
                            strSQL = strSQL + "   left join " + vbCr
                            strSQL = strSQL + "   (" + vbCr
                            strSQL = strSQL + " select 对象名称,对象类型,选择权=sum(选择权),编辑权=sum(编辑权),增加权=sum(增加权),删除权=sum(删除权),执行权=sum(执行权) from"
                            strSQL = strSQL + "  ("
                            strSQL = strSQL + "     select "
                            strSQL = strSQL + "     对象名称 = b.name,"
                            strSQL = strSQL + "     对象类型 = b.xtype,"
                            strSQL = strSQL + "     选择权   = case when a.type='SL' then 1 else 0 end,"
                            strSQL = strSQL + "     编辑权   = case when a.type='UP' then 1 else 0 end,"
                            strSQL = strSQL + "     增加权   = case when a.type='IN' then 1 else 0 end,"
                            strSQL = strSQL + "     删除权   = case when a.type='DL' then 1 else 0 end,"
                            strSQL = strSQL + "     执行权   = case when a.type='EX' then 1 else 0 end "
                            strSQL = strSQL + "     from " + strCurDB + ".sys.database_permissions a " + vbCr
                            strSQL = strSQL + "     left join " + strCurDB + ".dbo.sysobjects b on a.major_id=b.id " + vbCr
                            strSQL = strSQL + "     left join " + strCurDB + ".dbo.sysusers   c on a.grantee_principal_id=c.uid" + vbCr
                            strSQL = strSQL + "     where c.issqlrole = 1 " + vbCr
                            strSQL = strSQL + "     and   c.gid > 0" + vbCr
                            strSQL = strSQL + "     and   c.name = @rolename" + vbCr
                            strSQL = strSQL + "     )a group by a.对象类型,a.对象名称"
                            strSQL = strSQL + "   ) b on a.对象名称=b.对象名称 and a.对象类型=b.对象类型" + vbCr
                            strSQL = strSQL + " ) a" + vbCr
                            If strWhere <> "" Then
                                strSQL = strSQL + " where " + strWhere + vbCr
                            End If
                            strSQL = strSQL + " order by a.对象类型,a.对象名称" + vbCr

                            '设置参数
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@False", Xydc.Platform.Common.Utilities.PulicParameters.CharFalse)
                            objSqlCommand.Parameters.AddWithValue("@True", Xydc.Platform.Common.Utilities.PulicParameters.CharTrue)
                            objSqlCommand.Parameters.AddWithValue("@rolename", strRoleName)
                            .SelectCommand = objSqlCommand
                        End If

                        '执行操作
                        .Fill(objTempRoleQXData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_DUIXIANGQX))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempRoleQXData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempRoleQXData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objRoleQXData = objTempRoleQXData
            getRolePermissionsData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempRoleQXData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 给角色strRoleName授予指定对象strObjectName的权限objOptions
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objConnectionProperty：连接参数
        '     strRoleName          ：角色名
        '     strObjectName        ：对象名
        '     strObjectType        ：对象类型
        '     objOptions           ：角色权限数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doGrantRole( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strRoleName As String, _
            ByVal strObjectName As String, _
            ByVal strObjectType As String, _
            ByVal objOptions As System.Collections.Specialized.ListDictionary) As Boolean

            Dim objAppManagerData As New Xydc.Platform.Common.Data.AppManagerData
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            doGrantRole = False
            strErrMsg = ""

            Try
                '检查
                If strRoleName Is Nothing Then strRoleName = ""
                If strObjectName Is Nothing Then strObjectName = ""
                If strObjectType Is Nothing Then strObjectType = ""
                strRoleName = strRoleName.Trim()
                strObjectName = strObjectName.Trim()
                strObjectType = strObjectType.Trim()
                If objConnectionProperty Is Nothing Then
                    strErrMsg = "错误：没有指定服务器参数！"
                    GoTo errProc
                End If
                If objOptions Is Nothing Then
                    strErrMsg = "错误：没有指定权限参数！"
                    GoTo errProc
                End If

                '获取连接
                With objConnectionProperty

                    'If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, -1, .InitialCatalog, .DataSource) = False Then
                    '    GoTo errProc
                    'End If
                    If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, Platform.Common.jsoaConfiguration.ConnectionTestTimeout, .InitialCatalog, .DataSource) = False Then
                        GoTo errProc
                    End If

                End With

                '获取数据
                Dim strGrant As String = ""
                Dim strSQL As String = ""
                Try
                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '授权
                    Dim strU As String = objAppManagerData.getDatabaseObjectTypeString(Xydc.Platform.Common.Data.AppManagerData.enumDatabaseObjectType.U)
                    Dim strV As String = objAppManagerData.getDatabaseObjectTypeString(Xydc.Platform.Common.Data.AppManagerData.enumDatabaseObjectType.V)
                    Dim strP As String = objAppManagerData.getDatabaseObjectTypeString(Xydc.Platform.Common.Data.AppManagerData.enumDatabaseObjectType.P)
                    Dim strFN As String = objAppManagerData.getDatabaseObjectTypeString(Xydc.Platform.Common.Data.AppManagerData.enumDatabaseObjectType.FN)
                    Dim strIF As String = objAppManagerData.getDatabaseObjectTypeString(Xydc.Platform.Common.Data.AppManagerData.enumDatabaseObjectType.FIF)
                    Dim strTF As String = objAppManagerData.getDatabaseObjectTypeString(Xydc.Platform.Common.Data.AppManagerData.enumDatabaseObjectType.TF)
                    If strObjectType = strU Or strObjectType = strV Or strObjectType = strIF Or strObjectType = strTF Then
                        '表、视图、内嵌函数
                        Dim objenumPermissionType As Xydc.Platform.Common.Data.AppManagerData.enumPermissionType
                        Dim objDictionaryEntry As System.Collections.DictionaryEntry
                        Dim strValue As String
                        Dim i As Integer
                        For Each objDictionaryEntry In objOptions
                            strValue = ""
                            Try
                                objenumPermissionType = CType(objDictionaryEntry.Key, Xydc.Platform.Common.Data.AppManagerData.enumPermissionType)
                            Catch ex As Exception
                                objenumPermissionType = Nothing
                            End Try
                            Select Case objenumPermissionType
                                Case Xydc.Platform.Common.Data.AppManagerData.enumPermissionType.GrantSelect
                                    strValue = objAppManagerData.getPermissionTypeString(objenumPermissionType)
                                Case Xydc.Platform.Common.Data.AppManagerData.enumPermissionType.GrantUpdate
                                    strValue = objAppManagerData.getPermissionTypeString(objenumPermissionType)
                                Case Xydc.Platform.Common.Data.AppManagerData.enumPermissionType.GrantInsert
                                    strValue = objAppManagerData.getPermissionTypeString(objenumPermissionType)
                                Case Xydc.Platform.Common.Data.AppManagerData.enumPermissionType.GrantDelete
                                    strValue = objAppManagerData.getPermissionTypeString(objenumPermissionType)
                                Case Else
                            End Select
                            If strValue <> "" Then
                                If strGrant = "" Then
                                    strGrant = strValue
                                Else
                                    strGrant = strGrant + "," + strValue
                                End If
                            End If
                        Next
                        If strGrant <> "" Then
                            strSQL = "grant " + strGrant + " on " + strObjectName + " to " + strRoleName
                        End If

                    ElseIf strObjectType = strP Or strObjectType = strFN Then
                        '存储过程、函数
                        Dim objenumPermissionType As Xydc.Platform.Common.Data.AppManagerData.enumPermissionType
                        Dim objDictionaryEntry As System.Collections.DictionaryEntry
                        Dim strValue As String
                        Dim i As Integer
                        For Each objDictionaryEntry In objOptions
                            strValue = ""
                            Try
                                objenumPermissionType = CType(objDictionaryEntry.Key, Xydc.Platform.Common.Data.AppManagerData.enumPermissionType)
                            Catch ex As Exception
                                objenumPermissionType = Nothing
                            End Try
                            Select Case objenumPermissionType
                                Case Xydc.Platform.Common.Data.AppManagerData.enumPermissionType.GrantExecute
                                    strValue = objAppManagerData.getPermissionTypeString(objenumPermissionType)
                                Case Else
                            End Select
                            If strValue <> "" Then
                                If strGrant = "" Then
                                    strGrant = strValue
                                Else
                                    strGrant = strGrant + "," + strValue
                                End If
                            End If
                        Next
                        If strGrant <> "" Then
                            strSQL = "grant " + strGrant + " on " + strObjectName + " to " + strRoleName
                        End If

                    Else
                    End If

                    If strSQL <> "" Then
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.ExecuteNonQuery()
                    End If
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
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objAppManagerData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doGrantRole = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objAppManagerData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 从角色strRoleName回收指定对象strObjectName的权限objOptions
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objConnectionProperty：连接参数
        '     strRoleName          ：角色名
        '     strObjectName        ：对象名
        '     strObjectType        ：对象类型
        '     objOptions           ：角色权限数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doRevokeRole( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strRoleName As String, _
            ByVal strObjectName As String, _
            ByVal strObjectType As String, _
            ByVal objOptions As System.Collections.Specialized.ListDictionary) As Boolean

            Dim objAppManagerData As New Xydc.Platform.Common.Data.AppManagerData
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            doRevokeRole = False
            strErrMsg = ""

            Try
                '检查
                If strRoleName Is Nothing Then strRoleName = ""
                If strObjectName Is Nothing Then strObjectName = ""
                If strObjectType Is Nothing Then strObjectType = ""
                strRoleName = strRoleName.Trim()
                strObjectName = strObjectName.Trim()
                strObjectType = strObjectType.Trim()
                If objConnectionProperty Is Nothing Then
                    strErrMsg = "错误：没有指定服务器参数！"
                    GoTo errProc
                End If
                If objOptions Is Nothing Then
                    strErrMsg = "错误：没有指定权限参数！"
                    GoTo errProc
                End If

                '获取连接
                With objConnectionProperty

                    'If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, -1, .InitialCatalog, .DataSource) = False Then
                    '    GoTo errProc
                    'End If
                    If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, Platform.Common.jsoaConfiguration.ConnectionTestTimeout, .InitialCatalog, .DataSource) = False Then
                        GoTo errProc
                    End If

                End With

                '获取数据
                Dim strGrant As String = ""
                Dim strSQL As String = ""
                Try
                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '授权
                    Dim strU As String = objAppManagerData.getDatabaseObjectTypeString(Xydc.Platform.Common.Data.AppManagerData.enumDatabaseObjectType.U)
                    Dim strV As String = objAppManagerData.getDatabaseObjectTypeString(Xydc.Platform.Common.Data.AppManagerData.enumDatabaseObjectType.V)
                    Dim strP As String = objAppManagerData.getDatabaseObjectTypeString(Xydc.Platform.Common.Data.AppManagerData.enumDatabaseObjectType.P)
                    Dim strFN As String = objAppManagerData.getDatabaseObjectTypeString(Xydc.Platform.Common.Data.AppManagerData.enumDatabaseObjectType.FN)
                    Dim strIF As String = objAppManagerData.getDatabaseObjectTypeString(Xydc.Platform.Common.Data.AppManagerData.enumDatabaseObjectType.FIF)
                    Dim strTF As String = objAppManagerData.getDatabaseObjectTypeString(Xydc.Platform.Common.Data.AppManagerData.enumDatabaseObjectType.TF)
                    If strObjectType = strU Or strObjectType = strV Or strObjectType = strIF Or strObjectType = strTF Then
                        '表、视图、内嵌函数
                        Dim objenumPermissionType As Xydc.Platform.Common.Data.AppManagerData.enumPermissionType
                        Dim objDictionaryEntry As System.Collections.DictionaryEntry
                        Dim strValue As String
                        Dim i As Integer
                        For Each objDictionaryEntry In objOptions
                            strValue = ""
                            Try
                                objenumPermissionType = CType(objDictionaryEntry.Key, Xydc.Platform.Common.Data.AppManagerData.enumPermissionType)
                            Catch ex As Exception
                                objenumPermissionType = Nothing
                            End Try
                            Select Case objenumPermissionType
                                Case Xydc.Platform.Common.Data.AppManagerData.enumPermissionType.GrantSelect
                                    strValue = objAppManagerData.getPermissionTypeString(objenumPermissionType)
                                Case Xydc.Platform.Common.Data.AppManagerData.enumPermissionType.GrantUpdate
                                    strValue = objAppManagerData.getPermissionTypeString(objenumPermissionType)
                                Case Xydc.Platform.Common.Data.AppManagerData.enumPermissionType.GrantInsert
                                    strValue = objAppManagerData.getPermissionTypeString(objenumPermissionType)
                                Case Xydc.Platform.Common.Data.AppManagerData.enumPermissionType.GrantDelete
                                    strValue = objAppManagerData.getPermissionTypeString(objenumPermissionType)
                                Case Else
                            End Select
                            If strValue <> "" Then
                                If strGrant = "" Then
                                    strGrant = strValue
                                Else
                                    strGrant = strGrant + "," + strValue
                                End If
                            End If
                        Next
                        If strGrant <> "" Then
                            strSQL = "revoke " + strGrant + " on " + strObjectName + " from " + strRoleName
                        End If

                    ElseIf strObjectType = strP Or strObjectType = strFN Then
                        '存储过程、函数
                        Dim objenumPermissionType As Xydc.Platform.Common.Data.AppManagerData.enumPermissionType
                        Dim objDictionaryEntry As System.Collections.DictionaryEntry
                        Dim strValue As String
                        Dim i As Integer
                        For Each objDictionaryEntry In objOptions
                            strValue = ""
                            Try
                                objenumPermissionType = CType(objDictionaryEntry.Key, Xydc.Platform.Common.Data.AppManagerData.enumPermissionType)
                            Catch ex As Exception
                                objenumPermissionType = Nothing
                            End Try
                            Select Case objenumPermissionType
                                Case Xydc.Platform.Common.Data.AppManagerData.enumPermissionType.GrantExecute
                                    strValue = objAppManagerData.getPermissionTypeString(objenumPermissionType)
                                Case Else
                            End Select
                            If strValue <> "" Then
                                If strGrant = "" Then
                                    strGrant = strValue
                                Else
                                    strGrant = strGrant + "," + strValue
                                End If
                            End If
                        Next
                        If strGrant <> "" Then
                            strSQL = "revoke " + strGrant + " on " + strObjectName + " from " + strRoleName
                        End If

                    Else
                    End If

                    If strSQL <> "" Then
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.ExecuteNonQuery()
                    End If
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
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objAppManagerData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doRevokeRole = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objAppManagerData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取对指定数据库有存取权限的人员情况的数据集
        ' 以组织代码、人员序号升序排序
        ' 含人员的全部连接数据
        '     strErrMsg             ：如果错误，则返回错误信息
        '     objConnectionProperty ：连接参数
        '     strWhere              ：搜索字符串(默认表前缀a.)
        '     objRenyuanGrantedData ：指定组织机构下的人员信息数据集
        ' 返回
        '     True                  ：成功
        '     False                 ：失败
        '----------------------------------------------------------------
        Public Function getRenyuanGrantedData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strWhere As String, _
            ByRef objRenyuanGrantedData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempRenyuanGrantedData As Xydc.Platform.Common.Data.CustomerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getRenyuanGrantedData = False
            objRenyuanGrantedData = Nothing
            strErrMsg = ""

            Try
                '检查
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim()
                If objConnectionProperty Is Nothing Then
                    objTempRenyuanGrantedData = New Xydc.Platform.Common.Data.CustomerData(Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_RENYUAN_FULLJOIN)
                    Exit Try
                End If

                '不同服务器
                If objConnectionProperty.DataSource.ToUpper() <> Xydc.Platform.Common.jsoaConfiguration.DatabaseServerName.ToUpper() Then
                    objTempRenyuanGrantedData = New Xydc.Platform.Common.Data.CustomerData(Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_RENYUAN_FULLJOIN)
                    Exit Try
                End If

                '获取连接
                With objConnectionProperty

                    'If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, -1, .InitialCatalog, .DataSource) = False Then
                    '    GoTo errProc
                    'End If
                    If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, Platform.Common.jsoaConfiguration.ConnectionTestTimeout, .InitialCatalog, .DataSource) = False Then
                        GoTo errProc
                    End If

                End With

                '获取数据
                Dim strSQL As String
                Try
                    '创建数据集
                    objTempRenyuanGrantedData = New Xydc.Platform.Common.Data.CustomerData(Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_RENYUAN_FULLJOIN)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        Dim strDefDB As String = Xydc.Platform.Common.jsoaConfiguration.DatabaseServerUserDB
                        Dim strCurDB As String = objConnectionProperty.InitialCatalog

                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* from ("
                        strSQL = strSQL + "   select a.*," + vbCr
                        strSQL = strSQL + "     b.组织名称,b.组织别名," + vbCr
                        strSQL = strSQL + "     岗位列表 = " + strDefDB + ".dbo.GetGWMCByRydm(a.人员代码,@separate)," + vbCr
                        strSQL = strSQL + "     c.级别名称,c.行政级别," + vbCr
                        strSQL = strSQL + "     秘书名称 = d.人员名称," + vbCr
                        strSQL = strSQL + "     其他由转送名称 = e.人员名称," + vbCr
                        strSQL = strSQL + "     是否申请 = @charfalse" + vbCr
                        strSQL = strSQL + "   from      " + strDefDB + ".dbo.公共_B_人员     a " + vbCr
                        strSQL = strSQL + "   left join " + strDefDB + ".dbo.公共_B_组织机构 b on a.组织代码   = b.组织代码 " + vbCr
                        strSQL = strSQL + "   left join " + strDefDB + ".dbo.公共_B_行政级别 c on a.级别代码   = c.级别代码 " + vbCr
                        strSQL = strSQL + "   left join " + strDefDB + ".dbo.公共_B_人员     d on a.秘书代码   = d.人员代码 " + vbCr
                        strSQL = strSQL + "   left join " + strDefDB + ".dbo.公共_B_人员     e on a.其他由转送 = e.人员代码 " + vbCr
                        strSQL = strSQL + "   left join " + strCurDB + ".dbo.sysusers        f on a.人员代码   = f.name " + vbCr
                        strSQL = strSQL + "   left join           master.dbo.syslogins       g on a.人员代码   = g.name " + vbCr
                        strSQL = strSQL + "   where ((f.name is not null and f.issqluser = 1) or (a.人员代码='sa'))" + vbCr    'Login有User
                        strSQL = strSQL + "   and   g.name is not null " + vbCr                                                '必须有Login
                        strSQL = strSQL + " ) a "
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.组织代码, cast(a.人员序号 as integer)"

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@separate", Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate)
                        objSqlCommand.Parameters.AddWithValue("@charfalse", Xydc.Platform.Common.Utilities.PulicParameters.CharFalse)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempRenyuanGrantedData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempRenyuanGrantedData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempRenyuanGrantedData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objRenyuanGrantedData = objTempRenyuanGrantedData
            getRenyuanGrantedData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempRenyuanGrantedData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取对指定数据库没有存取权限的人员情况的数据集
        ' 以组织代码、人员序号升序排序
        ' 含人员的全部连接数据
        '     strErrMsg               ：如果错误，则返回错误信息
        '     objConnectionProperty   ：连接参数
        '     strWhere                ：搜索字符串(默认表前缀a.)
        '     objRenyuanUngrantedData ：指定组织机构下的人员信息数据集
        ' 返回
        '     True                    ：成功
        '     False                   ：失败
        '----------------------------------------------------------------
        Public Function getRenyuanUngrantedData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strWhere As String, _
            ByRef objRenyuanUngrantedData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempRenyuanUngrantedData As Xydc.Platform.Common.Data.CustomerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getRenyuanUngrantedData = False
            objRenyuanUngrantedData = Nothing
            strErrMsg = ""

            Try
                '检查
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim()
                If objConnectionProperty Is Nothing Then
                    objTempRenyuanUngrantedData = New Xydc.Platform.Common.Data.CustomerData(Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_RENYUAN_FULLJOIN)
                    Exit Try
                End If

                '不同服务器
                If objConnectionProperty.DataSource.ToUpper() <> Xydc.Platform.Common.jsoaConfiguration.DatabaseServerName.ToUpper() Then
                    objTempRenyuanUngrantedData = New Xydc.Platform.Common.Data.CustomerData(Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_RENYUAN_FULLJOIN)
                    Exit Try
                End If

                '获取连接
                With objConnectionProperty

                    'If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, -1, .InitialCatalog, .DataSource) = False Then
                    '    GoTo errProc
                    'End If
                    If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, Platform.Common.jsoaConfiguration.ConnectionTestTimeout, .InitialCatalog, .DataSource) = False Then
                        GoTo errProc
                    End If

                End With

                '获取数据
                Dim strSQL As String
                Try
                    '创建数据集
                    objTempRenyuanUngrantedData = New Xydc.Platform.Common.Data.CustomerData(Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_RENYUAN_FULLJOIN)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        Dim strDefDB As String = Xydc.Platform.Common.jsoaConfiguration.DatabaseServerUserDB
                        Dim strCurDB As String = objConnectionProperty.InitialCatalog

                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* from ("
                        strSQL = strSQL + "   select a.*," + vbCr
                        strSQL = strSQL + "     b.组织名称,b.组织别名," + vbCr
                        strSQL = strSQL + "     岗位列表 = " + strDefDB + ".dbo.GetGWMCByRydm(a.人员代码,@separate)," + vbCr
                        strSQL = strSQL + "     c.级别名称,c.行政级别," + vbCr
                        strSQL = strSQL + "     秘书名称 = d.人员名称," + vbCr
                        strSQL = strSQL + "     其他由转送名称 = e.人员名称," + vbCr
                        strSQL = strSQL + "     是否申请 = @charfalse" + vbCr
                        strSQL = strSQL + "   from      " + strDefDB + ".dbo.公共_B_人员     a " + vbCr
                        strSQL = strSQL + "   left join " + strDefDB + ".dbo.公共_B_组织机构 b on a.组织代码   = b.组织代码 " + vbCr
                        strSQL = strSQL + "   left join " + strDefDB + ".dbo.公共_B_行政级别 c on a.级别代码   = c.级别代码 " + vbCr
                        strSQL = strSQL + "   left join " + strDefDB + ".dbo.公共_B_人员     d on a.秘书代码   = d.人员代码 " + vbCr
                        strSQL = strSQL + "   left join " + strDefDB + ".dbo.公共_B_人员     e on a.其他由转送 = e.人员代码 " + vbCr
                        strSQL = strSQL + "   left join " + strCurDB + ".dbo.sysusers        f on a.人员代码   = f.name " + vbCr
                        strSQL = strSQL + "   left join           master.dbo.syslogins       g on a.人员代码   = g.name " + vbCr
                        strSQL = strSQL + "   where not ((f.name is not null and f.issqluser = 1) or (a.人员代码='sa'))" + vbCr    'Login没有User
                        strSQL = strSQL + "   and   g.name is not null " + vbCr                                                    '必须有Login
                        strSQL = strSQL + " ) a "
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.组织代码, cast(a.人员序号 as integer)"

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@separate", Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate)
                        objSqlCommand.Parameters.AddWithValue("@charfalse", Xydc.Platform.Common.Utilities.PulicParameters.CharFalse)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempRenyuanUngrantedData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempRenyuanUngrantedData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempRenyuanUngrantedData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objRenyuanUngrantedData = objTempRenyuanUngrantedData
            getRenyuanUngrantedData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempRenyuanUngrantedData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 给strLoginName授予存取数据库
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objConnectionProperty：连接参数
        '     strLoginName         ：角色名
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doGrantDatabase( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strLoginName As String) As Boolean

            Dim objAppManagerData As New Xydc.Platform.Common.Data.AppManagerData
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            doGrantDatabase = False
            strErrMsg = ""

            Try
                '检查
                If strLoginName Is Nothing Then strLoginName = ""
                strLoginName = strLoginName.Trim()
                If objConnectionProperty Is Nothing Then
                    strErrMsg = "错误：没有指定服务器参数！"
                    GoTo errProc
                End If
                If strLoginName.ToUpper() = "SA" Then
                    Exit Try
                End If

                '获取连接
                With objConnectionProperty

                    'If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, -1, .InitialCatalog, .DataSource) = False Then
                    '    GoTo errProc
                    'End If
                    If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, Platform.Common.jsoaConfiguration.ConnectionTestTimeout, .InitialCatalog, .DataSource) = False Then
                        GoTo errProc
                    End If

                End With

                '获取数据
                Dim strGrant As String = ""
                Dim strSQL As String = ""
                Try
                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '授权
                    strSQL = "exec sp_grantdbaccess @loginname, @username"
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@loginname", strLoginName)
                    objSqlCommand.Parameters.AddWithValue("@username", strLoginName)
                    objSqlCommand.ExecuteNonQuery()
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
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objAppManagerData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doGrantDatabase = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objAppManagerData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 给strLoginName取消存取数据库
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objConnectionProperty：连接参数
        '     strLoginName         ：角色名
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doRevokeDatabase( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strLoginName As String) As Boolean

            Dim objAppManagerData As New Xydc.Platform.Common.Data.AppManagerData
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            doRevokeDatabase = False
            strErrMsg = ""

            Try
                '检查
                If strLoginName Is Nothing Then strLoginName = ""
                strLoginName = strLoginName.Trim()
                If objConnectionProperty Is Nothing Then
                    strErrMsg = "错误：没有指定服务器参数！"
                    GoTo errProc
                End If
                If strLoginName.ToUpper() = "SA" Then
                    Exit Try
                End If

                '获取连接
                With objConnectionProperty

                    'If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, -1, .InitialCatalog, .DataSource) = False Then
                    '    GoTo errProc
                    'End If
                    If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, Platform.Common.jsoaConfiguration.ConnectionTestTimeout, .InitialCatalog, .DataSource) = False Then
                        GoTo errProc
                    End If

                End With

                '获取数据
                Dim strGrant As String = ""
                Dim strSQL As String = ""
                Try
                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '授权
                    Dim strDBName As String = objConnectionProperty.InitialCatalog
                    strSQL = "exec sp_revokedbaccess @loginname"
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@loginname", strLoginName)
                    objSqlCommand.ExecuteNonQuery()
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
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objAppManagerData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doRevokeDatabase = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objAppManagerData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取指定objConnectionProperty中的数据库的用户
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     objConnectionProperty       ：服务器信息
        '     strWhere                    ：搜索字符串(默认表前缀a.)
        '     objDBUserData               ：信息数据集
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getDBUserData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strWhere As String, _
            ByRef objDBUserData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempDBUserData As Xydc.Platform.Common.Data.AppManagerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getDBUserData = False
            objDBUserData = Nothing
            strErrMsg = ""

            Try
                '检查
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim()
                If objConnectionProperty Is Nothing Then
                    '创建数据集
                    objTempDBUserData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_SHUJUKU_DBUSER)
                    Exit Try
                End If

                '获取连接
                With objConnectionProperty

                    'If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, -1, .InitialCatalog, .DataSource) = False Then
                    '    GoTo errProc
                    'End If
                    If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, Platform.Common.jsoaConfiguration.ConnectionTestTimeout, .InitialCatalog, .DataSource) = False Then
                        GoTo errProc
                    End If

                End With

                '获取数据
                Dim strSQL As String
                Try
                    '创建数据集
                    objTempDBUserData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_SHUJUKU_DBUSER)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.uid,a.name " + vbCr
                        strSQL = strSQL + " from " + objConnectionProperty.InitialCatalog + ".dbo.sysusers a" + vbCr
                        strSQL = strSQL + " where issqluser = 1" + vbCr             '用户
                        strSQL = strSQL + " and   name <> 'guest'" + vbCr           '非guest
                        If strWhere <> "" Then
                            strSQL = strSQL + " and " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.name"

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempDBUserData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_DBUSER))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDBUserData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempDBUserData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objDBUserData = objTempDBUserData
            getDBUserData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempDBUserData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取角色的权限设置数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objConnectionProperty：连接参数
        '     strDBUserName        ：用户名
        '     strWhere             ：搜索字符串(默认表前缀a.)
        '     objDBUserQXData      ：角色权限数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDBUserPermissionsData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strDBUserName As String, _
            ByVal strWhere As String, _
            ByRef objDBUserQXData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempDBUserQXData As Xydc.Platform.Common.Data.AppManagerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getDBUserPermissionsData = False
            objDBUserQXData = Nothing
            strErrMsg = ""

            Try
                '检查
                If strDBUserName Is Nothing Then strDBUserName = ""
                If strWhere Is Nothing Then strWhere = ""
                strDBUserName = strDBUserName.Trim()
                strWhere = strWhere.Trim()
                If objConnectionProperty Is Nothing Then
                    objTempDBUserQXData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_SHUJUKU_DUIXIANGQX)
                    Exit Try
                End If

                '获取连接
                With objConnectionProperty

                    'If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, -1, .InitialCatalog, .DataSource) = False Then
                    '    GoTo errProc
                    'End If
                    If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, Platform.Common.jsoaConfiguration.ConnectionTestTimeout, .InitialCatalog, .DataSource) = False Then
                        GoTo errProc
                    End If

                End With

                '获取数据
                Dim strSQL As String
                Try
                    '创建数据集
                    objTempDBUserQXData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_SHUJUKU_DUIXIANGQX)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        Dim strDefServer As String = Xydc.Platform.Common.jsoaConfiguration.DatabaseServerName
                        Dim strDefDB As String = Xydc.Platform.Common.jsoaConfiguration.DatabaseServerUserDB
                        Dim strCurServer As String = objConnectionProperty.DataSource
                        Dim strCurDB As String = objConnectionProperty.InitialCatalog
                        Dim strXType As String = Xydc.Platform.Common.Data.AppManagerData.OBJECTTYPELIST

                        If strCurServer.ToUpper() = strDefServer.ToUpper() Then
                            '同一服务器

                            '准备SQL
                            strSQL = ""
                            strSQL = strSQL + " select a.*" + vbCr
                            strSQL = strSQL + " from" + vbCr
                            strSQL = strSQL + " (" + vbCr
                            strSQL = strSQL + "   select " + vbCr
                            strSQL = strSQL + "     a.对象名称,a.对象类型," + vbCr
                            strSQL = strSQL + "     对象中文名 = case when c.对象名称 is null then a.对象名称 else c.对象中文名 end," + vbCr
                            strSQL = strSQL + "     选择权     = case when b.对象名称 is null then @False else b.选择权 end," + vbCr
                            strSQL = strSQL + "     编辑权     = case when b.对象名称 is null then @False else b.编辑权 end," + vbCr
                            strSQL = strSQL + "     增加权     = case when b.对象名称 is null then @False else b.增加权 end," + vbCr
                            strSQL = strSQL + "     删除权     = case when b.对象名称 is null then @False else b.删除权 end," + vbCr
                            strSQL = strSQL + "     执行权     = case when b.对象名称 is null then @False else b.执行权 end " + vbCr
                            strSQL = strSQL + "   from " + vbCr
                            strSQL = strSQL + "   (  " + vbCr
                            strSQL = strSQL + "     select 对象名称=name,对象类型=xtype" + vbCr
                            strSQL = strSQL + "     from " + strCurDB + ".dbo.sysobjects " + vbCr
                            strSQL = strSQL + "     where xtype in (" + strXType + ")" + vbCr
                            strSQL = strSQL + "     and status > 0" + vbCr
                            strSQL = strSQL + "   ) a " + vbCr
                            strSQL = strSQL + "   left join " + vbCr
                            strSQL = strSQL + "   (" + vbCr
                            strSQL = strSQL + "     select " + vbCr
                            strSQL = strSQL + "       对象名称 = b.name," + vbCr
                            strSQL = strSQL + "       对象类型 = b.xtype," + vbCr
                            strSQL = strSQL + "       选择权   = case when a.actadd&1  > 0 then @True else @False end," + vbCr
                            strSQL = strSQL + "       编辑权   = case when a.actadd&2  > 0 then @True else @False end," + vbCr
                            strSQL = strSQL + "       增加权   = case when a.actadd&8  > 0 then @True else @False end," + vbCr
                            strSQL = strSQL + "       删除权   = case when a.actadd&16 > 0 then @True else @False end," + vbCr
                            strSQL = strSQL + "       执行权   = case when a.actadd&32 > 0 then @True else @False end " + vbCr
                            strSQL = strSQL + "     from " + strCurDB + ".dbo.syspermissions a " + vbCr
                            strSQL = strSQL + "     left join " + strCurDB + ".dbo.sysobjects b on a.id      = b.id" + vbCr
                            strSQL = strSQL + "     left join " + strCurDB + ".dbo.sysusers   c on a.grantee = c.uid" + vbCr
                            strSQL = strSQL + "     where c.issqluser = 1 " + vbCr
                            strSQL = strSQL + "     and   c.name <> 'guest'" + vbCr
                            strSQL = strSQL + "     and   c.name = @username" + vbCr
                            strSQL = strSQL + "   ) b on a.对象名称=b.对象名称 and a.对象类型=b.对象类型" + vbCr
                            strSQL = strSQL + "   left join" + vbCr
                            strSQL = strSQL + "   (" + vbCr
                            strSQL = strSQL + "     select * from " + strDefDB + ".dbo.管理_B_数据库_对象" + vbCr
                            strSQL = strSQL + "     where 服务器名 = @server" + vbCr
                            strSQL = strSQL + "     and   数据库名 = @dbname" + vbCr
                            strSQL = strSQL + "   ) c on a.对象名称=c.对象名称 and a.对象类型=c.对象类型" + vbCr
                            strSQL = strSQL + " ) a" + vbCr
                            If strWhere <> "" Then
                                strSQL = strSQL + " where " + strWhere + vbCr
                            End If
                            strSQL = strSQL + " order by a.对象类型,a.对象名称" + vbCr

                            '设置参数
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@False", Xydc.Platform.Common.Utilities.PulicParameters.CharFalse)
                            objSqlCommand.Parameters.AddWithValue("@True", Xydc.Platform.Common.Utilities.PulicParameters.CharTrue)
                            objSqlCommand.Parameters.AddWithValue("@username", strDBUserName)
                            objSqlCommand.Parameters.AddWithValue("@server", strCurServer)
                            objSqlCommand.Parameters.AddWithValue("@dbname", strCurDB)
                            .SelectCommand = objSqlCommand
                        Else
                            '不同服务器

                            '准备SQL
                            strSQL = ""
                            strSQL = strSQL + " select a.*" + vbCr
                            strSQL = strSQL + " from" + vbCr
                            strSQL = strSQL + " (" + vbCr
                            strSQL = strSQL + "   select " + vbCr
                            strSQL = strSQL + "     a.对象名称,a.对象类型," + vbCr
                            strSQL = strSQL + "     对象中文名=a.对象名称," + vbCr
                            strSQL = strSQL + "     选择权 = case when b.对象名称 is null then @False else b.选择权 end," + vbCr
                            strSQL = strSQL + "     编辑权 = case when b.对象名称 is null then @False else b.编辑权 end," + vbCr
                            strSQL = strSQL + "     增加权 = case when b.对象名称 is null then @False else b.增加权 end," + vbCr
                            strSQL = strSQL + "     删除权 = case when b.对象名称 is null then @False else b.删除权 end," + vbCr
                            strSQL = strSQL + "     执行权 = case when b.对象名称 is null then @False else b.执行权 end " + vbCr
                            strSQL = strSQL + "   from " + vbCr
                            strSQL = strSQL + "   (  " + vbCr
                            strSQL = strSQL + "     select 对象名称=name,对象类型=xtype" + vbCr
                            strSQL = strSQL + "     from " + strCurDB + ".dbo.sysobjects " + vbCr
                            strSQL = strSQL + "     where xtype in (" + strXType + ")" + vbCr
                            strSQL = strSQL + "     and status > 0" + vbCr
                            strSQL = strSQL + "   ) a " + vbCr
                            strSQL = strSQL + "   left join " + vbCr
                            strSQL = strSQL + "   (" + vbCr
                            strSQL = strSQL + "     select " + vbCr
                            strSQL = strSQL + "       对象名称 = b.name," + vbCr
                            strSQL = strSQL + "       对象类型 = b.xtype," + vbCr
                            strSQL = strSQL + "       选择权   = case when a.actadd&1  > 0 then @True else @False end," + vbCr
                            strSQL = strSQL + "       编辑权   = case when a.actadd&2  > 0 then @True else @False end," + vbCr
                            strSQL = strSQL + "       增加权   = case when a.actadd&8  > 0 then @True else @False end," + vbCr
                            strSQL = strSQL + "       删除权   = case when a.actadd&16 > 0 then @True else @False end," + vbCr
                            strSQL = strSQL + "       执行权   = case when a.actadd&32 > 0 then @True else @False end " + vbCr
                            strSQL = strSQL + "     from " + strCurDB + ".dbo.syspermissions a " + vbCr
                            strSQL = strSQL + "     left join " + strCurDB + ".dbo.sysobjects b on a.id      = b.id" + vbCr
                            strSQL = strSQL + "     left join " + strCurDB + ".dbo.sysusers   c on a.grantee = c.uid" + vbCr
                            strSQL = strSQL + "     where c.issqluser = 1 " + vbCr
                            strSQL = strSQL + "     and   c.name <> 'guest'" + vbCr
                            strSQL = strSQL + "     and   c.name = @username" + vbCr
                            strSQL = strSQL + "   ) b on a.对象名称=b.对象名称 and a.对象类型=b.对象类型" + vbCr
                            strSQL = strSQL + " ) a" + vbCr
                            If strWhere <> "" Then
                                strSQL = strSQL + " where " + strWhere + vbCr
                            End If
                            strSQL = strSQL + " order by a.对象类型,a.对象名称" + vbCr

                            '设置参数
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@False", Xydc.Platform.Common.Utilities.PulicParameters.CharFalse)
                            objSqlCommand.Parameters.AddWithValue("@True", Xydc.Platform.Common.Utilities.PulicParameters.CharTrue)
                            objSqlCommand.Parameters.AddWithValue("@username", strDBUserName)
                            .SelectCommand = objSqlCommand
                        End If

                        '执行操作
                        .Fill(objTempDBUserQXData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_DUIXIANGQX))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDBUserQXData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempDBUserQXData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objDBUserQXData = objTempDBUserQXData
            getDBUserPermissionsData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempDBUserQXData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 给用户strDBUserName授予指定对象strObjectName的权限objOptions
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objConnectionProperty：连接参数
        '     strDBUserName        ：用户名
        '     strObjectName        ：对象名
        '     strObjectType        ：对象类型
        '     objOptions           ：角色权限数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doGrantDBUser( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strDBUserName As String, _
            ByVal strObjectName As String, _
            ByVal strObjectType As String, _
            ByVal objOptions As System.Collections.Specialized.ListDictionary) As Boolean

            Dim objAppManagerData As New Xydc.Platform.Common.Data.AppManagerData
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            doGrantDBUser = False
            strErrMsg = ""

            Try
                '检查
                If strDBUserName Is Nothing Then strDBUserName = ""
                If strObjectName Is Nothing Then strObjectName = ""
                If strObjectType Is Nothing Then strObjectType = ""
                strDBUserName = strDBUserName.Trim()
                strObjectName = strObjectName.Trim()
                strObjectType = strObjectType.Trim()
                If objConnectionProperty Is Nothing Then
                    strErrMsg = "错误：没有指定服务器参数！"
                    GoTo errProc
                End If
                If objOptions Is Nothing Then
                    strErrMsg = "错误：没有指定权限参数！"
                    GoTo errProc
                End If

                '获取连接
                With objConnectionProperty

                    'If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, -1, .InitialCatalog, .DataSource) = False Then
                    '    GoTo errProc
                    'End If
                    If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, Platform.Common.jsoaConfiguration.ConnectionTestTimeout, .InitialCatalog, .DataSource) = False Then
                        GoTo errProc
                    End If

                End With

                '获取数据
                Dim strGrant As String = ""
                Dim strSQL As String = ""
                Try
                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '授权
                    Dim strU As String = objAppManagerData.getDatabaseObjectTypeString(Xydc.Platform.Common.Data.AppManagerData.enumDatabaseObjectType.U)
                    Dim strV As String = objAppManagerData.getDatabaseObjectTypeString(Xydc.Platform.Common.Data.AppManagerData.enumDatabaseObjectType.V)
                    Dim strP As String = objAppManagerData.getDatabaseObjectTypeString(Xydc.Platform.Common.Data.AppManagerData.enumDatabaseObjectType.P)
                    Dim strFN As String = objAppManagerData.getDatabaseObjectTypeString(Xydc.Platform.Common.Data.AppManagerData.enumDatabaseObjectType.FN)
                    Dim strIF As String = objAppManagerData.getDatabaseObjectTypeString(Xydc.Platform.Common.Data.AppManagerData.enumDatabaseObjectType.FIF)
                    Dim strTF As String = objAppManagerData.getDatabaseObjectTypeString(Xydc.Platform.Common.Data.AppManagerData.enumDatabaseObjectType.TF)
                    If strObjectType = strU Or strObjectType = strV Or strObjectType = strIF Or strObjectType = strTF Then
                        '表、视图、内嵌函数
                        Dim objenumPermissionType As Xydc.Platform.Common.Data.AppManagerData.enumPermissionType
                        Dim objDictionaryEntry As System.Collections.DictionaryEntry
                        Dim strValue As String
                        Dim i As Integer
                        For Each objDictionaryEntry In objOptions
                            strValue = ""
                            Try
                                objenumPermissionType = CType(objDictionaryEntry.Key, Xydc.Platform.Common.Data.AppManagerData.enumPermissionType)
                            Catch ex As Exception
                                objenumPermissionType = Nothing
                            End Try
                            Select Case objenumPermissionType
                                Case Xydc.Platform.Common.Data.AppManagerData.enumPermissionType.GrantSelect
                                    strValue = objAppManagerData.getPermissionTypeString(objenumPermissionType)
                                Case Xydc.Platform.Common.Data.AppManagerData.enumPermissionType.GrantUpdate
                                    strValue = objAppManagerData.getPermissionTypeString(objenumPermissionType)
                                Case Xydc.Platform.Common.Data.AppManagerData.enumPermissionType.GrantInsert
                                    strValue = objAppManagerData.getPermissionTypeString(objenumPermissionType)
                                Case Xydc.Platform.Common.Data.AppManagerData.enumPermissionType.GrantDelete
                                    strValue = objAppManagerData.getPermissionTypeString(objenumPermissionType)
                                Case Else
                            End Select
                            If strValue <> "" Then
                                If strGrant = "" Then
                                    strGrant = strValue
                                Else
                                    strGrant = strGrant + "," + strValue
                                End If
                            End If
                        Next
                        If strGrant <> "" Then
                            strSQL = "grant " + strGrant + " on " + strObjectName + " to " + strDBUserName
                        End If

                    ElseIf strObjectType = strP Or strObjectType = strFN Then
                        '存储过程、函数
                        Dim objenumPermissionType As Xydc.Platform.Common.Data.AppManagerData.enumPermissionType
                        Dim objDictionaryEntry As System.Collections.DictionaryEntry
                        Dim strValue As String
                        Dim i As Integer
                        For Each objDictionaryEntry In objOptions
                            strValue = ""
                            Try
                                objenumPermissionType = CType(objDictionaryEntry.Key, Xydc.Platform.Common.Data.AppManagerData.enumPermissionType)
                            Catch ex As Exception
                                objenumPermissionType = Nothing
                            End Try
                            Select Case objenumPermissionType
                                Case Xydc.Platform.Common.Data.AppManagerData.enumPermissionType.GrantExecute
                                    strValue = objAppManagerData.getPermissionTypeString(objenumPermissionType)
                                Case Else
                            End Select
                            If strValue <> "" Then
                                If strGrant = "" Then
                                    strGrant = strValue
                                Else
                                    strGrant = strGrant + "," + strValue
                                End If
                            End If
                        Next
                        If strGrant <> "" Then
                            strSQL = "grant " + strGrant + " on " + strObjectName + " to " + strDBUserName
                        End If

                    Else
                    End If

                    If strSQL <> "" Then
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.ExecuteNonQuery()
                    End If
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
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objAppManagerData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doGrantDBUser = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objAppManagerData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 从用户strDBUserName回收指定对象strObjectName的权限objOptions
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objConnectionProperty：连接参数
        '     strDBUserName        ：用户名
        '     strObjectName        ：对象名
        '     strObjectType        ：对象类型
        '     objOptions           ：角色权限数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doRevokeDBUser( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strDBUserName As String, _
            ByVal strObjectName As String, _
            ByVal strObjectType As String, _
            ByVal objOptions As System.Collections.Specialized.ListDictionary) As Boolean

            Dim objAppManagerData As New Xydc.Platform.Common.Data.AppManagerData
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            doRevokeDBUser = False
            strErrMsg = ""

            Try
                '检查
                If strDBUserName Is Nothing Then strDBUserName = ""
                If strObjectName Is Nothing Then strObjectName = ""
                If strObjectType Is Nothing Then strObjectType = ""
                strDBUserName = strDBUserName.Trim()
                strObjectName = strObjectName.Trim()
                strObjectType = strObjectType.Trim()
                If objConnectionProperty Is Nothing Then
                    strErrMsg = "错误：没有指定服务器参数！"
                    GoTo errProc
                End If
                If objOptions Is Nothing Then
                    strErrMsg = "错误：没有指定权限参数！"
                    GoTo errProc
                End If

                '获取连接
                With objConnectionProperty

                    'If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, -1, .InitialCatalog, .DataSource) = False Then
                    '    GoTo errProc
                    'End If
                    If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, Platform.Common.jsoaConfiguration.ConnectionTestTimeout, .InitialCatalog, .DataSource) = False Then
                        GoTo errProc
                    End If

                End With

                '获取数据
                Dim strGrant As String = ""
                Dim strSQL As String = ""
                Try
                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '授权
                    Dim strU As String = objAppManagerData.getDatabaseObjectTypeString(Xydc.Platform.Common.Data.AppManagerData.enumDatabaseObjectType.U)
                    Dim strV As String = objAppManagerData.getDatabaseObjectTypeString(Xydc.Platform.Common.Data.AppManagerData.enumDatabaseObjectType.V)
                    Dim strP As String = objAppManagerData.getDatabaseObjectTypeString(Xydc.Platform.Common.Data.AppManagerData.enumDatabaseObjectType.P)
                    Dim strFN As String = objAppManagerData.getDatabaseObjectTypeString(Xydc.Platform.Common.Data.AppManagerData.enumDatabaseObjectType.FN)
                    Dim strIF As String = objAppManagerData.getDatabaseObjectTypeString(Xydc.Platform.Common.Data.AppManagerData.enumDatabaseObjectType.FIF)
                    Dim strTF As String = objAppManagerData.getDatabaseObjectTypeString(Xydc.Platform.Common.Data.AppManagerData.enumDatabaseObjectType.TF)
                    If strObjectType = strU Or strObjectType = strV Or strObjectType = strIF Or strObjectType = strTF Then
                        '表、视图、内嵌函数
                        Dim objenumPermissionType As Xydc.Platform.Common.Data.AppManagerData.enumPermissionType
                        Dim objDictionaryEntry As System.Collections.DictionaryEntry
                        Dim strValue As String
                        Dim i As Integer
                        For Each objDictionaryEntry In objOptions
                            strValue = ""
                            Try
                                objenumPermissionType = CType(objDictionaryEntry.Key, Xydc.Platform.Common.Data.AppManagerData.enumPermissionType)
                            Catch ex As Exception
                                objenumPermissionType = Nothing
                            End Try
                            Select Case objenumPermissionType
                                Case Xydc.Platform.Common.Data.AppManagerData.enumPermissionType.GrantSelect
                                    strValue = objAppManagerData.getPermissionTypeString(objenumPermissionType)
                                Case Xydc.Platform.Common.Data.AppManagerData.enumPermissionType.GrantUpdate
                                    strValue = objAppManagerData.getPermissionTypeString(objenumPermissionType)
                                Case Xydc.Platform.Common.Data.AppManagerData.enumPermissionType.GrantInsert
                                    strValue = objAppManagerData.getPermissionTypeString(objenumPermissionType)
                                Case Xydc.Platform.Common.Data.AppManagerData.enumPermissionType.GrantDelete
                                    strValue = objAppManagerData.getPermissionTypeString(objenumPermissionType)
                                Case Else
                            End Select
                            If strValue <> "" Then
                                If strGrant = "" Then
                                    strGrant = strValue
                                Else
                                    strGrant = strGrant + "," + strValue
                                End If
                            End If
                        Next
                        If strGrant <> "" Then
                            strSQL = "revoke " + strGrant + " on " + strObjectName + " from " + strDBUserName
                        End If

                    ElseIf strObjectType = strP Or strObjectType = strFN Then
                        '存储过程、函数
                        Dim objenumPermissionType As Xydc.Platform.Common.Data.AppManagerData.enumPermissionType
                        Dim objDictionaryEntry As System.Collections.DictionaryEntry
                        Dim strValue As String
                        Dim i As Integer
                        For Each objDictionaryEntry In objOptions
                            strValue = ""
                            Try
                                objenumPermissionType = CType(objDictionaryEntry.Key, Xydc.Platform.Common.Data.AppManagerData.enumPermissionType)
                            Catch ex As Exception
                                objenumPermissionType = Nothing
                            End Try
                            Select Case objenumPermissionType
                                Case Xydc.Platform.Common.Data.AppManagerData.enumPermissionType.GrantExecute
                                    strValue = objAppManagerData.getPermissionTypeString(objenumPermissionType)
                                Case Else
                            End Select
                            If strValue <> "" Then
                                If strGrant = "" Then
                                    strGrant = strValue
                                Else
                                    strGrant = strGrant + "," + strValue
                                End If
                            End If
                        Next
                        If strGrant <> "" Then
                            strSQL = "revoke " + strGrant + " on " + strObjectName + " from " + strDBUserName
                        End If

                    Else
                    End If

                    If strSQL <> "" Then
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.ExecuteNonQuery()
                    End If
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
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objAppManagerData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doRevokeDBUser = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objAppManagerData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取“管理_B_应用系统_模块”的数据集(以模块代码升序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWhere             ：搜索字符串(默认表前缀a.)
        '     objMokuaiData        ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getMokuaiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objMokuaiData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempMokuaiData As Xydc.Platform.Common.Data.AppManagerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getMokuaiData = False
            objMokuaiData = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strWhere Is Nothing Then strWhere = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                strWhere = strWhere.Trim()
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
                    objTempMokuaiData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_YINGYONGXITONG_MOKUAI)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from 管理_B_应用系统_模块 a " + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.模块代码 " + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempMokuaiData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_YINGYONGXITONG_MOKUAI))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempMokuaiData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempMokuaiData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objMokuaiData = objTempMokuaiData
            getMokuaiData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempMokuaiData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取指定strMKDM下级的“管理_B_应用系统_模块”的数据集(以模块代码升序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strMKDM              ：模块代码
        '     strWhere             ：搜索字符串(默认表前缀a.)
        '     objMokuaiData        ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getMokuaiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strMKDM As String, _
            ByVal strWhere As String, _
            ByRef objMokuaiData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempMokuaiData As Xydc.Platform.Common.Data.AppManagerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getMokuaiData = False
            objMokuaiData = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strMKDM Is Nothing Then strMKDM = ""
                If strWhere Is Nothing Then strWhere = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                strMKDM = strMKDM.Trim()
                strWhere = strWhere.Trim()
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
                    objTempMokuaiData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_YINGYONGXITONG_MOKUAI)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharFjdmSeparate
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from 管理_B_应用系统_模块 a " + vbCr
                        strSQL = strSQL + " where (a.模块代码 like @mkdm + '" + strSep + "%' or a.模块代码 = @mkdm)" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " and " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.模块代码 " + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@mkdm", strMKDM)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempMokuaiData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_YINGYONGXITONG_MOKUAI))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempMokuaiData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempMokuaiData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objMokuaiData = objTempMokuaiData
            getMokuaiData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempMokuaiData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据指定strMKDM获取“管理_B_应用系统_模块”的数据集
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strMKDM              ：模块代码
        '     blnUnused            ：重载用
        '     objMokuaiData        ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getMokuaiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strMKDM As String, _
            ByVal blnUnused As Boolean, _
            ByRef objMokuaiData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempMokuaiData As Xydc.Platform.Common.Data.AppManagerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getMokuaiData = False
            objMokuaiData = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strMKDM Is Nothing Then strMKDM = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                strMKDM = strMKDM.Trim()
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
                    objTempMokuaiData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_YINGYONGXITONG_MOKUAI)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharFjdmSeparate
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from 管理_B_应用系统_模块 a " + vbCr
                        strSQL = strSQL + " where a.模块代码 = @mkdm" + vbCr
                        strSQL = strSQL + " order by a.模块代码 " + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@mkdm", strMKDM)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempMokuaiData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_YINGYONGXITONG_MOKUAI))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempMokuaiData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempMokuaiData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objMokuaiData = objTempMokuaiData
            getMokuaiData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempMokuaiData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据指定strMKDM获取“管理_B_应用系统_模块”的数据集
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     intMKBS              ：模块标识
        '     blnUnused            ：重载用
        '     objMokuaiData        ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getMokuaiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intMKBS As Integer, _
            ByVal blnUnused As Boolean, _
            ByRef objMokuaiData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempMokuaiData As Xydc.Platform.Common.Data.AppManagerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getMokuaiData = False
            objMokuaiData = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
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
                    objTempMokuaiData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_YINGYONGXITONG_MOKUAI)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharFjdmSeparate
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from 管理_B_应用系统_模块 a " + vbCr
                        strSQL = strSQL + " where a.模块标识 = @mkbs" + vbCr
                        strSQL = strSQL + " order by a.模块代码 " + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@mkbs", intMKBS)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempMokuaiData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_YINGYONGXITONG_MOKUAI))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempMokuaiData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempMokuaiData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objMokuaiData = objTempMokuaiData
            getMokuaiData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempMokuaiData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据上级模块代码获取下级的模块代码
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strPrevMKDM          ：上级模块代码
        '     strNewMKDM           ：新模块代码(返回)
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getNewMKDM( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strPrevMKDM As String, _
            ByRef strNewMKDM As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            '初始化
            getNewMKDM = False
            strNewMKDM = ""
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strPrevMKDM Is Nothing Then strPrevMKDM = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                strPrevMKDM = strPrevMKDM.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取上级模块级别
                Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharFjdmSeparate
                Dim intLevel As Integer = objPulicParameters.getCodeLevel(strPrevMKDM, strSep)
                If intLevel < 0 Then intLevel = 0

                '获取数据
                strSQL = ""
                strSQL = strSQL + " select max(本级代码) " + vbCr
                strSQL = strSQL + " from 管理_B_应用系统_模块 " + vbCr
                strSQL = strSQL + " where 模块级别 = " + (intLevel + 1).ToString() + vbCr         '直接下级
                If strPrevMKDM <> "" Then
                    strSQL = strSQL + " and 模块代码 like '" + strPrevMKDM + strSep + "%'" + vbCr '下级
                End If
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    If strPrevMKDM = "" Then
                        strNewMKDM = "1"
                    Else
                        strNewMKDM = strPrevMKDM + strSep + "1"
                    End If
                Else
                    Dim intValue As Integer
                    With objDataSet.Tables(0).Rows(0)
                        intValue = objPulicParameters.getObjectValue(.Item(0), 0)
                    End With
                    intValue += 1
                    If strPrevMKDM = "" Then
                        strNewMKDM = intValue.ToString()
                    Else
                        strNewMKDM = strPrevMKDM + strSep + intValue.ToString()
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
            getNewMKDM = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取新的模块标识
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strNewMKBS           ：新模块标识(返回)
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getNewMKBS( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef strNewMKBS As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            '初始化
            getNewMKBS = False
            strNewMKBS = ""
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                If objdacCommon.getNewCode(strErrMsg, objSqlConnection, "模块标识", "管理_B_应用系统_模块", True, strNewMKBS) = False Then
                    GoTo errProc
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
            getNewMKBS = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据现有新值计算其他系统自动计算的值
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objNewData           ：新数据(返回)
        '     objenumEditType      ：编辑类型
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getMokuaiDefaultValue( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef objNewData As System.Collections.Specialized.ListDictionary, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            getMokuaiDefaultValue = False

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If

                '获取模块标识
                Dim strMKBS As String
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                        If Me.getNewMKBS(strErrMsg, strUserId, strPassword, strMKBS) = False Then
                            GoTo errProc
                        End If
                        objNewData(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKBS) = objPulicParameters.getObjectValue(strMKBS, 0)
                    Case Else
                End Select

                '获取模块代码
                Dim strMKDM As String
                strMKDM = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKDM), "")
                strMKDM = strMKDM.Trim()
                If strMKDM = "" Then
                    strErrMsg = "错误：[模块代码]不能为空！"
                    GoTo errProc
                End If
                Dim strTemp As String = strMKDM
                strTemp = strTemp.Replace(Xydc.Platform.Common.Utilities.PulicParameters.CharFjdmSeparate, "")
                If objPulicParameters.isNumericString(strTemp) = False Then
                    strErrMsg = "错误：[模块代码]中存在非法字符！"
                    GoTo errProc
                End If

                '根据模块代码获取模块级别
                Dim intLevel As Integer
                intLevel = objPulicParameters.getCodeLevel(strMKDM, Xydc.Platform.Common.Utilities.PulicParameters.CharFjdmSeparate)
                objNewData(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKJB) = intLevel

                '根据模块代码获取本级代码
                Dim strBJDM As String
                strBJDM = objPulicParameters.getCodeValue(strMKDM, Xydc.Platform.Common.Utilities.PulicParameters.CharFjdmSeparate, intLevel)
                objNewData(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_BJDM) = objPulicParameters.getObjectValue(strBJDM, 0)

                '根据模块代码获取顶级模块
                Dim objAppManagerData As Xydc.Platform.Common.Data.AppManagerData
                If intLevel <= 1 Then
                    objNewData(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_DJMK) = objNewData(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKBS)
                Else
                    Dim strDJMK As String
                    strDJMK = objPulicParameters.getCodeValue(strMKDM, Xydc.Platform.Common.Utilities.PulicParameters.CharFjdmSeparate, 1, True)

                    '根据顶级模块代码获取顶级模块标识
                    If Me.getMokuaiData(strErrMsg, strUserId, strPassword, strDJMK, True, objAppManagerData) = False Then
                        GoTo errProc
                    End If
                    With objAppManagerData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_YINGYONGXITONG_MOKUAI)
                        If .Rows.Count < 1 Then
                            strErrMsg = "错误：[" + strDJMK + "]不存在！"
                            GoTo errProc
                        Else
                            objNewData(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_DJMK) = .Rows(0).Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKBS)
                        End If
                    End With
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objAppManagerData)
                    objAppManagerData = Nothing
                End If

                '根据模块代码获取上级模块
                If intLevel <= 1 Then
                    objNewData(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_SJMK) = objNewData(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_DJMK)
                Else
                    Dim strSJMK As String
                    strSJMK = objPulicParameters.getCodeValue(strMKDM, Xydc.Platform.Common.Utilities.PulicParameters.CharFjdmSeparate, intLevel - 1, True)

                    '根据顶级模块代码获取上级模块标识
                    If Me.getMokuaiData(strErrMsg, strUserId, strPassword, strSJMK, True, objAppManagerData) = False Then
                        GoTo errProc
                    End If
                    With objAppManagerData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_YINGYONGXITONG_MOKUAI)
                        If .Rows.Count < 1 Then
                            strErrMsg = "错误：[" + strSJMK + "]不存在！"
                            GoTo errProc
                        Else
                            objNewData(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_SJMK) = .Rows(0).Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKBS)
                        End If
                    End With
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objAppManagerData)
                    objAppManagerData = Nothing
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            getMokuaiDefaultValue = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 检查“管理_B_应用系统_模块”的数据的合法性
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
        '     objNewData           ：新数据
        '     objenumEditType      ：编辑类型
        ' 返回
        '     True                 ：合法
        '     False                ：不合法或其他程序错误
        '----------------------------------------------------------------
        Public Function doVerifyMokuaiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByRef objNewData As System.Collections.Specialized.ListDictionary, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objListDictionary As New System.Collections.Specialized.ListDictionary
            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            doVerifyMokuaiData = False

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "错误：未传入新的数据！"
                    GoTo errProc
                End If
                Dim intOldMKBS As Integer
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                    Case Else
                        If objOldData Is Nothing Then
                            strErrMsg = "错误：未传入旧的数据！"
                            GoTo errProc
                        End If
                        intOldMKBS = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKBS), 0)
                End Select

                '根据输入值计算其他自动值，并校验顶级、上级代码
                If Me.getMokuaiDefaultValue(strErrMsg, strUserId, strPassword, objNewData, objenumEditType) = False Then
                    GoTo errProc
                End If

                '获取表结构定义
                strSQL = "select top 0 * from 管理_B_应用系统_模块"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, strUserId, strPassword, strSQL, "管理_B_应用系统_模块", objDataSet) = False Then
                    GoTo errProc
                End If

                '检查数据长度
                Dim objDictionaryEntry As System.Collections.DictionaryEntry
                Dim strField As String
                Dim intLen As Integer
                For Each objDictionaryEntry In objNewData
                    strField = objPulicParameters.getObjectValue(objDictionaryEntry.Key, "")
                    Select Case strField
                        Case Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKBS, _
                            Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKJB, _
                            Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_BJDM, _
                            Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_DJMK, _
                            Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_SJMK

                        Case Else
                            Dim strValue As String
                            strValue = objPulicParameters.getObjectValue(objDictionaryEntry.Value, "")
                            If strValue = "" Then
                                Select Case strField
                                    Case Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKSM
                                    Case Else
                                        strErrMsg = "错误：[" + strField + "]不能为空！"
                                        GoTo errProc
                                End Select
                            End If
                            With objDataSet.Tables(0).Columns(strField)
                                intLen = objPulicParameters.getStringLength(strValue)
                                If intLen > .MaxLength Then
                                    strErrMsg = "错误：[" + strField + "]长度不能超过[" + .MaxLength.ToString() + "]，实际有[" + intLen.ToString() + "]！"
                                    GoTo errProc
                                End If
                            End With
                    End Select
                Next
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '检查：模块标识
                Dim intNewMKBS As Integer
                intNewMKBS = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKBS), 0)
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                        strSQL = ""
                        strSQL = strSQL + " select * from 管理_B_应用系统_模块 "
                        strSQL = strSQL + " where 模块标识 = @newmkbs"
                        objListDictionary.Add("@newmkbs", intNewMKBS)
                    Case Else
                        strSQL = ""
                        strSQL = strSQL + " select * from 管理_B_应用系统_模块 "
                        strSQL = strSQL + " where 模块标识 =  @newmkbs"
                        strSQL = strSQL + " and   模块标识 <> @oldmkbs"
                        objListDictionary.Add("@newmkbs", intNewMKBS)
                        objListDictionary.Add("@oldmkbs", intOldMKBS)
                End Select
                If objdacCommon.getDataSetBySQL(strErrMsg, strUserId, strPassword, strSQL, objListDictionary, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strErrMsg = "错误：[" + intNewMKBS.ToString() + "]已经存在！"
                    GoTo errProc
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing
                objListDictionary.Clear()

                '检查：模块代码
                Dim strNewMKDM As String
                strNewMKDM = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKDM), "")
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                        strSQL = ""
                        strSQL = strSQL + " select * from 管理_B_应用系统_模块 "
                        strSQL = strSQL + " where 模块代码 = @newmkdm"
                        objListDictionary.Add("@newmkdm", strNewMKDM)
                    Case Else
                        strSQL = ""
                        strSQL = strSQL + " select * from 管理_B_应用系统_模块 "
                        strSQL = strSQL + " where 模块代码 =  @newmkdm"
                        strSQL = strSQL + " and   模块标识 <> @oldmkbs"
                        objListDictionary.Add("@newmkdm", strNewMKDM)
                        objListDictionary.Add("@oldmkbs", intOldMKBS)
                End Select
                If objdacCommon.getDataSetBySQL(strErrMsg, strUserId, strPassword, strSQL, objListDictionary, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strErrMsg = "错误：[" + strNewMKDM.ToString() + "]已经存在！"
                    GoTo errProc
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing
                objListDictionary.Clear()

                '检查：模块名称
                Dim strNewMKMC As String
                strNewMKMC = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKMC), "")
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                        strSQL = ""
                        strSQL = strSQL + " select * from 管理_B_应用系统_模块 "
                        strSQL = strSQL + " where 模块名称 = @newmkmc"
                        objListDictionary.Add("@newmkmc", strNewMKMC)
                    Case Else
                        strSQL = ""
                        strSQL = strSQL + " select * from 管理_B_应用系统_模块 "
                        strSQL = strSQL + " where 模块名称 =  @newmkmc"
                        strSQL = strSQL + " and   模块标识 <> @oldmkbs"
                        objListDictionary.Add("@newmkmc", strNewMKMC)
                        objListDictionary.Add("@oldmkbs", intOldMKBS)
                End Select
                If objdacCommon.getDataSetBySQL(strErrMsg, strUserId, strPassword, strSQL, objListDictionary, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strErrMsg = "错误：[" + strNewMKMC.ToString() + "]已经存在！"
                    GoTo errProc
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing
                objListDictionary.Clear()

                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objListDictionary)

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objListDictionary)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            doVerifyMokuaiData = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objListDictionary)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 保存“管理_B_应用系统_模块”的数据
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
        Public Function doSaveMokuaiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.ListDictionary, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '初始化
            doSaveMokuaiData = False
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "错误：未传入新的数据！"
                    GoTo errProc
                End If
                Dim strOldMKDM As String
                Dim intOldMKBS As Integer
                Dim strNewMKDM As String
                Dim intNewMKBS As Integer
                intNewMKBS = objPulicParameters.getObjectValue(objNewData.Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKBS), 0)
                strNewMKDM = objPulicParameters.getObjectValue(objNewData.Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKDM), "")
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                    Case Else
                        If objOldData Is Nothing Then
                            strErrMsg = "错误：未传入旧的数据！"
                            GoTo errProc
                        End If
                        intOldMKBS = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKBS), 0)
                        strOldMKDM = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKDM), "")
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
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '计算SQL
                    Dim objDictionaryEntry As System.Collections.DictionaryEntry
                    Dim strFileds As String = ""
                    Dim strValues As String = ""
                    Dim strField As String
                    Dim i As Integer = 0
                    Select Case objenumEditType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                            For Each objDictionaryEntry In objNewData
                                If strFileds = "" Then
                                    strFileds = objPulicParameters.getObjectValue(objDictionaryEntry.Key, "")
                                Else
                                    strFileds = strFileds + "," + objPulicParameters.getObjectValue(objDictionaryEntry.Key, "")
                                End If
                                If strValues = "" Then
                                    strValues = "@A" + i.ToString()
                                Else
                                    strValues = strValues + "," + "@A" + i.ToString()
                                End If
                                i += 1
                            Next
                            strSQL = ""
                            strSQL = strSQL + " insert into 管理_B_应用系统_模块 (" + strFileds + ")"
                            strSQL = strSQL + " values (" + strValues + ")"
                            objSqlCommand.Parameters.Clear()
                            i = 0
                            For Each objDictionaryEntry In objNewData
                                objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), objDictionaryEntry.Value)
                                i += 1
                            Next
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()

                        Case Else
                            For Each objDictionaryEntry In objNewData
                                If strFileds = "" Then
                                    strFileds = objPulicParameters.getObjectValue(objDictionaryEntry.Key, "") + " = @A" + i.ToString()
                                Else
                                    strFileds = strFileds + "," + objPulicParameters.getObjectValue(objDictionaryEntry.Key, "") + " = @A" + i.ToString()
                                End If
                                i += 1
                            Next
                            strSQL = ""
                            strSQL = strSQL + " update 管理_B_应用系统_模块 set "
                            strSQL = strSQL + "   " + strFileds
                            strSQL = strSQL + " where 模块标识 = @oldmkbs"
                            objSqlCommand.Parameters.Clear()
                            i = 0
                            For Each objDictionaryEntry In objNewData
                                objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), objDictionaryEntry.Value)
                                i += 1
                            Next
                            objSqlCommand.Parameters.AddWithValue("@oldmkbs", intOldMKBS)
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()

                            If strNewMKDM.ToUpper() <> strOldMKDM.ToUpper() Then
                                Dim intOldMKJB As Integer
                                intOldMKJB = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKJB), 0)
                                Dim intNewMKJB As Integer
                                intNewMKJB = objPulicParameters.getObjectValue(objNewData.Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKJB), 0)
                                Dim intNewDJMK As Integer
                                intNewDJMK = objPulicParameters.getObjectValue(objNewData.Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_DJMK), 0)

                                '更改原下级的代码
                                strSQL = ""
                                strSQL = strSQL + " update 管理_B_应用系统_模块 set "
                                strSQL = strSQL + "   模块代码 = @newmkdm + substring(模块代码, @oldmkdmlen + 1, len(模块代码) - @oldmkdmlen),"
                                strSQL = strSQL + "   模块级别 = @newmkjb + 模块级别 - @oldmkjb,"
                                strSQL = strSQL + "   顶级模块 = @newdjmk "
                                strSQL = strSQL + " where 模块代码 like @oldmkdm + @sep + '%'" '本模块的下级
                                objSqlCommand.Parameters.Clear()
                                objSqlCommand.Parameters.AddWithValue("@newmkdm", strNewMKDM)
                                objSqlCommand.Parameters.AddWithValue("@oldmkdmlen", strOldMKDM.Length)
                                objSqlCommand.Parameters.AddWithValue("@newmkjb", intNewMKJB)
                                objSqlCommand.Parameters.AddWithValue("@oldmkjb", intOldMKJB)
                                objSqlCommand.Parameters.AddWithValue("@newdjmk", intNewDJMK)
                                objSqlCommand.Parameters.AddWithValue("@newmkbs", intNewMKBS)
                                objSqlCommand.Parameters.AddWithValue("@oldmkdm", strOldMKDM)
                                objSqlCommand.Parameters.AddWithValue("@sep", Xydc.Platform.Common.Utilities.PulicParameters.CharFjdmSeparate)
                                objSqlCommand.CommandText = strSQL
                                objSqlCommand.ExecuteNonQuery()
                            End If
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
            doSaveMokuaiData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据模块代码删除“管理_B_应用系统_模块”的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strMKDM              ：模块代码
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteMokuaiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strMKDM As String) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            doDeleteMokuaiData = False
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strMKDM Is Nothing Then strMKDM = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                strMKDM = strMKDM.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
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

                    '删除管理_B_应用系统_模块
                    strSQL = ""
                    strSQL = strSQL + " delete from 管理_B_应用系统_模块 "
                    strSQL = strSQL + " where 模块代码 like @mkdm + @sep +'%' "
                    strSQL = strSQL + " or    模块代码 = @mkdm"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@mkdm", strMKDM)
                    objSqlCommand.Parameters.AddWithValue("@sep", Xydc.Platform.Common.Utilities.PulicParameters.CharFjdmSeparate)
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
            doDeleteMokuaiData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取角色的模块权限设置数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objConnectionProperty：连接参数
        '     strRoleName          ：角色名
        '     strWhere             ：搜索字符串(默认表前缀a.)
        '     objRoleMKQXData      ：角色权限数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getRoleMokuaiQXData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strRoleName As String, _
            ByVal strWhere As String, _
            ByRef objRoleMKQXData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempRoleMKQXData As Xydc.Platform.Common.Data.AppManagerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getRoleMokuaiQXData = False
            objRoleMKQXData = Nothing
            strErrMsg = ""

            Try
                '检查
                If strRoleName Is Nothing Then strRoleName = ""
                If strWhere Is Nothing Then strWhere = ""
                strRoleName = strRoleName.Trim()
                strWhere = strWhere.Trim()
                If objConnectionProperty Is Nothing Then
                    objTempRoleMKQXData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_YINGYONGXITONG_MOKUAIQX)
                    Exit Try
                End If

                '不同服务器
                If objConnectionProperty.DataSource.ToUpper() <> Xydc.Platform.Common.jsoaConfiguration.DatabaseServerName.ToUpper() Then
                    objTempRoleMKQXData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_YINGYONGXITONG_MOKUAIQX)
                    Exit Try
                End If

                '获取连接
                With objConnectionProperty

                    'If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, -1, .InitialCatalog, .DataSource) = False Then
                    '    GoTo errProc
                    'End If
                    If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, Platform.Common.jsoaConfiguration.ConnectionTestTimeout, .InitialCatalog, .DataSource) = False Then
                        GoTo errProc
                    End If

                End With

                '获取数据
                Dim strSQL As String
                Try
                    '创建数据集
                    objTempRoleMKQXData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_YINGYONGXITONG_MOKUAIQX)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        Dim strDefDB As String = Xydc.Platform.Common.jsoaConfiguration.DatabaseServerUserDB
                        Dim strCurDB As String = objConnectionProperty.InitialCatalog
                        Dim intUserType As Integer = Xydc.Platform.Common.Data.AppManagerData.enumUserType.isSqlRole

                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.*" + vbCr
                        strSQL = strSQL + " from (" + vbCr
                        strSQL = strSQL + "   select " + vbCr
                        strSQL = strSQL + "     a.模块标识, a.模块代码, a.模块名称, a.说明," + vbCr
                        strSQL = strSQL + "     b.权限代码, b.用户标识, b.用户类型," + vbCr
                        strSQL = strSQL + "     执行权 = case when b.权限代码 is null then @False else @True end" + vbCr
                        strSQL = strSQL + "   from " + strDefDB + ".dbo.管理_B_应用系统_模块 a" + vbCr
                        strSQL = strSQL + "   left join (" + vbCr
                        strSQL = strSQL + "     select 权限代码,用户标识,用户类型,模块标识" + vbCr
                        strSQL = strSQL + "     from " + strDefDB + ".dbo.管理_B_应用系统_模块权限" + vbCr
                        strSQL = strSQL + "     where 用户标识 = @rolename" + vbCr
                        strSQL = strSQL + "     and   用户类型 = @usertype" + vbCr
                        strSQL = strSQL + "   ) b on a.模块标识 = b.模块标识 " + vbCr
                        strSQL = strSQL + " ) a" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.模块代码" + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@False", Xydc.Platform.Common.Utilities.PulicParameters.CharFalse)
                        objSqlCommand.Parameters.AddWithValue("@True", Xydc.Platform.Common.Utilities.PulicParameters.CharTrue)
                        objSqlCommand.Parameters.AddWithValue("@rolename", strRoleName)
                        objSqlCommand.Parameters.AddWithValue("@usertype", intUserType)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempRoleMKQXData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_YINGYONGXITONG_MOKUAIQX))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempRoleMKQXData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempRoleMKQXData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objRoleMKQXData = objTempRoleMKQXData
            getRoleMokuaiQXData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempRoleMKQXData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取用户的模块权限设置数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objConnectionProperty：连接参数
        '     strDBUserName        ：用户名
        '     strWhere             ：搜索字符串(默认表前缀a.)
        '     objDBUserMKQXData    ：角色权限数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDBUserMokuaiQXData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strDBUserName As String, _
            ByVal strWhere As String, _
            ByRef objDBUserMKQXData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempDBUserMKQXData As Xydc.Platform.Common.Data.AppManagerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getDBUserMokuaiQXData = False
            objDBUserMKQXData = Nothing
            strErrMsg = ""

            Try
                '检查
                If strDBUserName Is Nothing Then strDBUserName = ""
                If strWhere Is Nothing Then strWhere = ""
                strDBUserName = strDBUserName.Trim()
                strWhere = strWhere.Trim()
                If objConnectionProperty Is Nothing Then
                    objTempDBUserMKQXData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_YINGYONGXITONG_MOKUAIQX)
                    Exit Try
                End If

                '不同服务器
                If objConnectionProperty.DataSource.ToUpper() <> Xydc.Platform.Common.jsoaConfiguration.DatabaseServerName.ToUpper() Then
                    objTempDBUserMKQXData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_YINGYONGXITONG_MOKUAIQX)
                    Exit Try
                End If

                '获取连接
                With objConnectionProperty

                    'If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, -1, .InitialCatalog, .DataSource) = False Then
                    '    GoTo errProc
                    'End If
                    If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, Platform.Common.jsoaConfiguration.ConnectionTestTimeout, .InitialCatalog, .DataSource) = False Then
                        GoTo errProc
                    End If

                End With

                '获取数据
                Dim strSQL As String
                Try
                    '创建数据集
                    objTempDBUserMKQXData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_YINGYONGXITONG_MOKUAIQX)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        Dim strDefDB As String = Xydc.Platform.Common.jsoaConfiguration.DatabaseServerUserDB
                        Dim strCurDB As String = objConnectionProperty.InitialCatalog
                        Dim intUserType As Integer = Xydc.Platform.Common.Data.AppManagerData.enumUserType.isSqlUser

                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.*" + vbCr
                        strSQL = strSQL + " from (" + vbCr
                        strSQL = strSQL + "   select " + vbCr
                        strSQL = strSQL + "     a.模块标识, a.模块代码, a.模块名称, a.说明," + vbCr
                        strSQL = strSQL + "     b.权限代码, b.用户标识, b.用户类型," + vbCr
                        strSQL = strSQL + "     执行权 = case when b.权限代码 is null then @False else @True end" + vbCr
                        strSQL = strSQL + "   from " + strDefDB + ".dbo.管理_B_应用系统_模块 a" + vbCr
                        strSQL = strSQL + "   left join (" + vbCr
                        strSQL = strSQL + "     select 权限代码,用户标识,用户类型,模块标识" + vbCr
                        strSQL = strSQL + "     from " + strDefDB + ".dbo.管理_B_应用系统_模块权限" + vbCr
                        strSQL = strSQL + "     where 用户标识 = @dbusername" + vbCr
                        strSQL = strSQL + "     and   用户类型 = @usertype" + vbCr
                        strSQL = strSQL + "   ) b on a.模块标识 = b.模块标识 " + vbCr
                        strSQL = strSQL + " ) a" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.模块代码" + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@False", Xydc.Platform.Common.Utilities.PulicParameters.CharFalse)
                        objSqlCommand.Parameters.AddWithValue("@True", Xydc.Platform.Common.Utilities.PulicParameters.CharTrue)
                        objSqlCommand.Parameters.AddWithValue("@dbusername", strDBUserName)
                        objSqlCommand.Parameters.AddWithValue("@usertype", intUserType)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempDBUserMKQXData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_YINGYONGXITONG_MOKUAIQX))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDBUserMKQXData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempDBUserMKQXData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objDBUserMKQXData = objTempDBUserMKQXData
            getDBUserMokuaiQXData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempDBUserMKQXData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 给角色strRoleName授予指定模块strMKBS的权限
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strRoleName          ：角色名
        '     strMKBS              ：模块标识
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doGrantRoleMokuaiQX( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRoleName As String, _
            ByVal strMKBS As String) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            doGrantRoleMokuaiQX = False
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strRoleName Is Nothing Then strRoleName = ""
                If strMKBS Is Nothing Then strMKBS = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                strRoleName = strRoleName.Trim()
                strMKBS = strMKBS.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
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

                '获取数据
                Dim strGrant As String = ""
                Dim strSQL As String = ""
                Try
                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '计算
                    Dim intUserType As Integer = Xydc.Platform.Common.Data.AppManagerData.enumUserType.isSqlRole
                    strSQL = ""
                    strSQL = strSQL + " delete from 管理_B_应用系统_模块权限 " + vbCr
                    strSQL = strSQL + " where 用户标识 = @rolename " + vbCr
                    strSQL = strSQL + " and   用户类型 = @usertype " + vbCr
                    strSQL = strSQL + " and   模块标识 = @mkbs" + vbCr
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@rolename", strRoleName)
                    objSqlCommand.Parameters.AddWithValue("@usertype", intUserType)
                    objSqlCommand.Parameters.AddWithValue("@mkbs", strMKBS)
                    objSqlCommand.ExecuteNonQuery()

                    strSQL = ""
                    strSQL = strSQL + " insert into 管理_B_应用系统_模块权限(用户标识,用户类型,模块标识,执行权) " + vbCr
                    strSQL = strSQL + " values(@rolename,@usertype,@mkbs,@execute) " + vbCr
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@rolename", strRoleName)
                    objSqlCommand.Parameters.AddWithValue("@usertype", intUserType)
                    objSqlCommand.Parameters.AddWithValue("@mkbs", strMKBS)
                    objSqlCommand.Parameters.AddWithValue("@execute", 1)
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
            doGrantRoleMokuaiQX = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 从角色strRoleName回收指定模块strMKBS的权限
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strRoleName          ：角色名
        '     strMKBS              ：模块标识
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doRevokeRoleMokuaiQX( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRoleName As String, _
            ByVal strMKBS As String) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            doRevokeRoleMokuaiQX = False
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strRoleName Is Nothing Then strRoleName = ""
                If strMKBS Is Nothing Then strMKBS = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                strRoleName = strRoleName.Trim()
                strMKBS = strMKBS.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
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

                '获取数据
                Dim strGrant As String = ""
                Dim strSQL As String = ""
                Try
                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '计算
                    Dim intUserType As Integer = Xydc.Platform.Common.Data.AppManagerData.enumUserType.isSqlRole
                    strSQL = ""
                    strSQL = strSQL + " delete from 管理_B_应用系统_模块权限 " + vbCr
                    strSQL = strSQL + " where 用户标识 = @rolename " + vbCr
                    strSQL = strSQL + " and   用户类型 = @usertype " + vbCr
                    strSQL = strSQL + " and   模块标识 = @mkbs" + vbCr
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@rolename", strRoleName)
                    objSqlCommand.Parameters.AddWithValue("@usertype", intUserType)
                    objSqlCommand.Parameters.AddWithValue("@mkbs", strMKBS)
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
            doRevokeRoleMokuaiQX = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 给用户strDBUserName授予指定模块strMKBS的权限
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strDBUserName        ：用户名
        '     strMKBS              ：模块标识
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doGrantDBuserMokuaiQX( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strDBUserName As String, _
            ByVal strMKBS As String) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            doGrantDBuserMokuaiQX = False
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strDBUserName Is Nothing Then strDBUserName = ""
                If strMKBS Is Nothing Then strMKBS = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                strDBUserName = strDBUserName.Trim()
                strMKBS = strMKBS.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
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

                '获取数据
                Dim strGrant As String = ""
                Dim strSQL As String = ""
                Try
                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '计算
                    Dim intUserType As Integer = Xydc.Platform.Common.Data.AppManagerData.enumUserType.isSqlUser
                    strSQL = ""
                    strSQL = strSQL + " delete from 管理_B_应用系统_模块权限 " + vbCr
                    strSQL = strSQL + " where 用户标识 = @dbusername " + vbCr
                    strSQL = strSQL + " and   用户类型 = @usertype " + vbCr
                    strSQL = strSQL + " and   模块标识 = @mkbs" + vbCr
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@dbusername", strDBUserName)
                    objSqlCommand.Parameters.AddWithValue("@usertype", intUserType)
                    objSqlCommand.Parameters.AddWithValue("@mkbs", strMKBS)
                    objSqlCommand.ExecuteNonQuery()

                    strSQL = ""
                    strSQL = strSQL + " insert into 管理_B_应用系统_模块权限(用户标识,用户类型,模块标识,执行权) " + vbCr
                    strSQL = strSQL + " values(@dbusername,@usertype,@mkbs,@execute) " + vbCr
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@dbusername", strDBUserName)
                    objSqlCommand.Parameters.AddWithValue("@usertype", intUserType)
                    objSqlCommand.Parameters.AddWithValue("@mkbs", strMKBS)
                    objSqlCommand.Parameters.AddWithValue("@execute", 1)
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
            doGrantDBuserMokuaiQX = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 从用户strDBUserName回收指定模块strMKBS的权限
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strDBUserName        ：用户名
        '     strMKBS              ：模块标识
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doRevokeDBUserMokuaiQX( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strDBUserName As String, _
            ByVal strMKBS As String) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            doRevokeDBUserMokuaiQX = False
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strDBUserName Is Nothing Then strDBUserName = ""
                If strMKBS Is Nothing Then strMKBS = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                strDBUserName = strDBUserName.Trim()
                strMKBS = strMKBS.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
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

                '获取数据
                Dim strGrant As String = ""
                Dim strSQL As String = ""
                Try
                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '计算
                    Dim intUserType As Integer = Xydc.Platform.Common.Data.AppManagerData.enumUserType.isSqlUser
                    strSQL = ""
                    strSQL = strSQL + " delete from 管理_B_应用系统_模块权限 " + vbCr
                    strSQL = strSQL + " where 用户标识 = @dbusername " + vbCr
                    strSQL = strSQL + " and   用户类型 = @usertype " + vbCr
                    strSQL = strSQL + " and   模块标识 = @mkbs" + vbCr
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@dbusername", strDBUserName)
                    objSqlCommand.Parameters.AddWithValue("@usertype", intUserType)
                    objSqlCommand.Parameters.AddWithValue("@mkbs", strMKBS)
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
            doRevokeDBUserMokuaiQX = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取用户的模块权限设置数据(同时检查用户所属角色的权限设置)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strDBUserName        ：用户名
        '     objDBUserMKQXData    ：角色权限数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDBUserMokuaiQXData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strDBUserName As String, _
            ByRef objDBUserMKQXData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempDBUserMKQXData As Xydc.Platform.Common.Data.AppManagerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getDBUserMokuaiQXData = False
            objDBUserMKQXData = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strDBUserName Is Nothing Then strDBUserName = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                strDBUserName = strDBUserName.Trim()
                If strUserId = "" Then
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
                    objTempDBUserMKQXData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_YINGYONGXITONG_MOKUAIQX)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        Dim strDefDB As String = Xydc.Platform.Common.jsoaConfiguration.DatabaseServerUserDB
                        Dim intUserType As Integer = Xydc.Platform.Common.Data.AppManagerData.enumUserType.isSqlUser
                        Dim intRoleType As Integer = Xydc.Platform.Common.Data.AppManagerData.enumUserType.isSqlRole

                        '准备SQL
                        If strDBUserName.ToUpper = "SA" Then
                            '全部权限！！！
                            strSQL = ""
                            strSQL = strSQL + " select a.*" + vbCr
                            strSQL = strSQL + " from (" + vbCr
                            strSQL = strSQL + "   select " + vbCr
                            strSQL = strSQL + "     权限代码=0,a.用户标识=@dbusername,a.用户类型=@usertype,a.模块标识," + vbCr
                            strSQL = strSQL + "     a.模块代码,a.模块名称,a.说明," + vbCr
                            strSQL = strSQL + "     执行权=@True" + vbCr
                            strSQL = strSQL + "   from " + strDefDB + ".dbo.管理_B_应用系统_模块 a " + vbCr
                            strSQL = strSQL + " ) a " + vbCr
                            strSQL = strSQL + " order by a.模块代码" + vbCr

                            '设置参数
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@True", Xydc.Platform.Common.Utilities.PulicParameters.CharTrue)
                            objSqlCommand.Parameters.AddWithValue("@dbusername", strDBUserName)
                            objSqlCommand.Parameters.AddWithValue("@usertype", intUserType)
                            .SelectCommand = objSqlCommand
                        Else
                            strSQL = ""
                            strSQL = strSQL + " select a.*" + vbCr
                            strSQL = strSQL + " from (" + vbCr
                            strSQL = strSQL + "   select " + vbCr
                            strSQL = strSQL + "     a.权限代码,a.用户标识,a.用户类型,a.模块标识," + vbCr
                            strSQL = strSQL + "     b.模块代码,b.模块名称,b.说明," + vbCr
                            strSQL = strSQL + "     执行权=@True" + vbCr
                            strSQL = strSQL + "   from " + strDefDB + ".dbo.管理_B_应用系统_模块权限 a " + vbCr
                            strSQL = strSQL + "   left join " + strDefDB + ".dbo.管理_B_应用系统_模块 b on a.模块标识 = b.模块标识 " + vbCr
                            strSQL = strSQL + "   left join (" + vbCr                                               '用户所属角色
                            strSQL = strSQL + "     select 用户标识=c.name, 用户类型=@roletype" + vbCr
                            strSQL = strSQL + "     from " + strDefDB + ".dbo.sysmembers a " + vbCr
                            strSQL = strSQL + "     left join " + strDefDB + ".dbo.sysusers b on a.memberuid = b.uid" + vbCr
                            strSQL = strSQL + "     left join " + strDefDB + ".dbo.sysusers c on a.groupuid  = c.uid" + vbCr
                            strSQL = strSQL + "     where b.name = @dbusername" + vbCr
                            strSQL = strSQL + "     group by c.name" + vbCr
                            strSQL = strSQL + "   ) c on a.用户标识=c.用户标识 and a.用户类型=c.用户类型" + vbCr
                            strSQL = strSQL + "   where b.模块代码 is not null" + vbCr                             '模块存在
                            strSQL = strSQL + "   and ((a.用户标识=@dbusername and a.用户类型=@usertype) " + vbCr  '用户授权
                            strSQL = strSQL + "   or   (c.用户标识 is not null)) " + vbCr                          '角色授权
                            strSQL = strSQL + " ) a " + vbCr
                            strSQL = strSQL + " order by a.模块代码" + vbCr

                            '设置参数
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@True", Xydc.Platform.Common.Utilities.PulicParameters.CharTrue)
                            objSqlCommand.Parameters.AddWithValue("@roletype", intRoleType)
                            objSqlCommand.Parameters.AddWithValue("@dbusername", strDBUserName)
                            objSqlCommand.Parameters.AddWithValue("@usertype", intUserType)
                            .SelectCommand = objSqlCommand
                        End If

                        '执行操作
                        .Fill(objTempDBUserMKQXData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_YINGYONGXITONG_MOKUAIQX))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDBUserMKQXData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempDBUserMKQXData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objDBUserMKQXData = objTempDBUserMKQXData
            getDBUserMokuaiQXData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempDBUserMKQXData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function








        '----------------------------------------------------------------
        ' 获取一般用户操作日志
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strTempPath          ：临时文件目录
        '     strWhere             ：搜索字符串(数据集搜索字符串)
        '     objLogDataSet        ：返回数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_JSOALOG( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strTempPath As String, _
            ByVal strWhere As String, _
            ByRef objLogDataSet As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objTempLogDataSet As Xydc.Platform.Common.Data.AppManagerData
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '初始化
            getDataSet_JSOALOG = False
            objLogDataSet = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "错误：[getDataSet_JSOALOG]未指定连接用户！"
                    GoTo errProc
                End If
                If strTempPath Is Nothing Then strTempPath = ""
                strTempPath = strTempPath.Trim
                If strTempPath = "" Then
                    strErrMsg = "错误：[getDataSet_JSOALOG]未指定临时目录！"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim

                '获取数据
                Try
                    '创建数据集
                    objTempLogDataSet = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_VT_B_JSOALOG)

                    '获取XML文件
                    Dim strXMLFile As String = Xydc.Platform.SystemFramework.ApplicationConfiguration.TracingTraceFile

                    '复制到临时文件
                    Dim strFileName As String
                    If objBaseLocalFile.doCopyToTempFile(strErrMsg, strXMLFile, strTempPath, strFileName) = False Then
                        GoTo errProc
                    End If
                    strFileName = objBaseLocalFile.doMakePath(strTempPath, strFileName)

                    '写XML文件结束标志
                    Dim objFileInfo As New System.IO.FileInfo(strFileName)
                    Dim objFileStream As System.IO.FileStream
                    objFileStream = objFileInfo.Open(FileMode.Append, FileAccess.Write, FileShare.ReadWrite)
                    Dim objStreamWriter As System.IO.StreamWriter
                    objStreamWriter = New System.IO.StreamWriter(objFileStream)
                    objStreamWriter.WriteLine("</jsoalog>")
                    objStreamWriter.Flush()
                    objStreamWriter.Close()
                    objFileStream.Close()
                    objStreamWriter = Nothing
                    objFileStream = Nothing
                    objFileInfo = Nothing

                    '从XML读入数据
                    objTempLogDataSet.ReadXml(strFileName)

                    '设置过滤条件
                    With objTempLogDataSet.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_VT_B_JSOALOG)
                        .DefaultView.RowFilter = strWhere
                    End With

                    '删除临时文件
                    objBaseLocalFile.doDeleteFile(strErrMsg, strFileName)

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempLogDataSet.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempLogDataSet)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objLogDataSet = objTempLogDataSet
            getDataSet_JSOALOG = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取配置管理员操作日志
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strTempPath          ：临时文件目录
        '     strWhere             ：搜索字符串(数据集搜索字符串)
        '     objLogDataSet        ：返回数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_AUDITPZLOG( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strTempPath As String, _
            ByVal strWhere As String, _
            ByRef objLogDataSet As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objTempLogDataSet As Xydc.Platform.Common.Data.AppManagerData
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '初始化
            getDataSet_AUDITPZLOG = False
            objLogDataSet = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "错误：[getDataSet_AUDITPZLOG]未指定连接用户！"
                    GoTo errProc
                End If
                If strTempPath Is Nothing Then strTempPath = ""
                strTempPath = strTempPath.Trim
                If strTempPath = "" Then
                    strErrMsg = "错误：[getDataSet_AUDITPZLOG]未指定临时目录！"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim

                '获取数据
                Try
                    '创建数据集
                    objTempLogDataSet = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_VT_B_AUDITPZLOG)

                    '获取XML文件
                    Dim strXMLFile As String = Xydc.Platform.SystemFramework.ApplicationConfiguration.TracingAuditPZFile

                    '复制到临时文件
                    Dim strFileName As String
                    If objBaseLocalFile.doCopyToTempFile(strErrMsg, strXMLFile, strTempPath, strFileName) = False Then
                        GoTo errProc
                    End If
                    strFileName = objBaseLocalFile.doMakePath(strTempPath, strFileName)

                    '写XML文件结束标志
                    Dim objFileInfo As New System.IO.FileInfo(strFileName)
                    Dim objFileStream As System.IO.FileStream
                    objFileStream = objFileInfo.Open(FileMode.Append, FileAccess.Write, FileShare.ReadWrite)
                    Dim objStreamWriter As System.IO.StreamWriter
                    objStreamWriter = New System.IO.StreamWriter(objFileStream)
                    objStreamWriter.WriteLine("</auditpzlog>")
                    objStreamWriter.Flush()
                    objStreamWriter.Close()
                    objFileStream.Close()
                    objStreamWriter = Nothing
                    objFileStream = Nothing
                    objFileInfo = Nothing

                    '从XML读入数据
                    objTempLogDataSet.ReadXml(strFileName)

                    '设置过滤条件
                    With objTempLogDataSet.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_VT_B_AUDITPZLOG)
                        .DefaultView.RowFilter = strWhere
                    End With

                    '删除临时文件
                    objBaseLocalFile.doDeleteFile(strErrMsg, strFileName)

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempLogDataSet.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempLogDataSet)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objLogDataSet = objTempLogDataSet
            getDataSet_AUDITPZLOG = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取安全管理员操作日志
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strTempPath          ：临时文件目录
        '     strWhere             ：搜索字符串(数据集搜索字符串)
        '     objLogDataSet        ：返回数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_AUDITAQLOG( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strTempPath As String, _
            ByVal strWhere As String, _
            ByRef objLogDataSet As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objTempLogDataSet As Xydc.Platform.Common.Data.AppManagerData
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '初始化
            getDataSet_AUDITAQLOG = False
            objLogDataSet = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "错误：[getDataSet_AUDITAQLOG]未指定连接用户！"
                    GoTo errProc
                End If
                If strTempPath Is Nothing Then strTempPath = ""
                strTempPath = strTempPath.Trim
                If strTempPath = "" Then
                    strErrMsg = "错误：[getDataSet_AUDITAQLOG]未指定临时目录！"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim

                '获取数据
                Try
                    '创建数据集
                    objTempLogDataSet = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_VT_B_AUDITAQLOG)

                    '获取XML文件
                    Dim strXMLFile As String = Xydc.Platform.SystemFramework.ApplicationConfiguration.TracingAuditAQFile

                    '复制到临时文件
                    Dim strFileName As String
                    If objBaseLocalFile.doCopyToTempFile(strErrMsg, strXMLFile, strTempPath, strFileName) = False Then
                        GoTo errProc
                    End If
                    strFileName = objBaseLocalFile.doMakePath(strTempPath, strFileName)

                    '写XML文件结束标志
                    Dim objFileInfo As New System.IO.FileInfo(strFileName)
                    Dim objFileStream As System.IO.FileStream
                    objFileStream = objFileInfo.Open(FileMode.Append, FileAccess.Write, FileShare.ReadWrite)
                    Dim objStreamWriter As System.IO.StreamWriter
                    objStreamWriter = New System.IO.StreamWriter(objFileStream)
                    objStreamWriter.WriteLine("</auditaqlog>")
                    objStreamWriter.Flush()
                    objStreamWriter.Close()
                    objFileStream.Close()
                    objStreamWriter = Nothing
                    objFileStream = Nothing
                    objFileInfo = Nothing

                    '从XML读入数据
                    objTempLogDataSet.ReadXml(strFileName)

                    '设置过滤条件
                    With objTempLogDataSet.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_VT_B_AUDITAQLOG)
                        .DefaultView.RowFilter = strWhere
                    End With

                    '删除临时文件
                    objBaseLocalFile.doDeleteFile(strErrMsg, strFileName)

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempLogDataSet.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempLogDataSet)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objLogDataSet = objTempLogDataSet
            getDataSet_AUDITAQLOG = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取审计管理员操作日志
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strTempPath          ：临时文件目录
        '     strWhere             ：搜索字符串(数据集搜索字符串)
        '     objLogDataSet        ：返回数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_AUDITSJLOG( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strTempPath As String, _
            ByVal strWhere As String, _
            ByRef objLogDataSet As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objTempLogDataSet As Xydc.Platform.Common.Data.AppManagerData
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '初始化
            getDataSet_AUDITSJLOG = False
            objLogDataSet = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "错误：[getDataSet_AUDITSJLOG]未指定连接用户！"
                    GoTo errProc
                End If
                If strTempPath Is Nothing Then strTempPath = ""
                strTempPath = strTempPath.Trim
                If strTempPath = "" Then
                    strErrMsg = "错误：[getDataSet_AUDITSJLOG]未指定临时目录！"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim

                '获取数据
                Try
                    '创建数据集
                    objTempLogDataSet = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_VT_B_AUDITSJLOG)

                    '获取XML文件
                    Dim strXMLFile As String = Xydc.Platform.SystemFramework.ApplicationConfiguration.TracingAuditSJFile

                    '复制到临时文件
                    Dim strFileName As String
                    If objBaseLocalFile.doCopyToTempFile(strErrMsg, strXMLFile, strTempPath, strFileName) = False Then
                        GoTo errProc
                    End If
                    strFileName = objBaseLocalFile.doMakePath(strTempPath, strFileName)

                    '写XML文件结束标志
                    Dim objFileInfo As New System.IO.FileInfo(strFileName)
                    Dim objFileStream As System.IO.FileStream
                    objFileStream = objFileInfo.Open(FileMode.Append, FileAccess.Write, FileShare.ReadWrite)
                    Dim objStreamWriter As System.IO.StreamWriter
                    objStreamWriter = New System.IO.StreamWriter(objFileStream)
                    objStreamWriter.WriteLine("</auditsjlog>")
                    objStreamWriter.Flush()
                    objStreamWriter.Close()
                    objFileStream.Close()
                    objStreamWriter = Nothing
                    objFileStream = Nothing
                    objFileInfo = Nothing

                    '从XML读入数据
                    objTempLogDataSet.ReadXml(strFileName)

                    '设置过滤条件
                    With objTempLogDataSet.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_VT_B_AUDITSJLOG)
                        .DefaultView.RowFilter = strWhere
                    End With

                    '删除临时文件
                    objBaseLocalFile.doDeleteFile(strErrMsg, strFileName)

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempLogDataSet.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempLogDataSet)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objLogDataSet = objTempLogDataSet
            getDataSet_AUDITSJLOG = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

       
    End Class

End Namespace
