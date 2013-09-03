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
    ' 类名    ：dacCustomer
    '
    ' 功能描述：
    '     提供对系统用户数据的增加、修改、删除、检索、密码校验、
    '     更改密码、创建ID、删除ID等操作
    '----------------------------------------------------------------

    Public Class dacCustomer
        Implements IDisposable

        ' 密码加密字符串
        Private Const m_cstrEncryptString As String = "FDINGWNUEKJYRXZHUXRGSRKRXTGDKJTDSODGNDTVSYSLJAZI"
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.DataAccess.dacCustomer)
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
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Overridable Function doExportToExcel( _
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
        ' 对密码进行加密处理
        '     strPassword：待加密的密码
        ' 返回
        '     加密后的新密码
        '----------------------------------------------------------------
        Public Function doEncryptPassowrd(ByVal strPassword As String) As String

            Dim strTemp As String

            Try
                '初始化
                If strPassword Is Nothing Then strPassword = ""
                strTemp = ""

                '获取加密键值长度
                Dim intKeyLen As Integer
                intKeyLen = m_cstrEncryptString.Length

                '获取现密码长度
                Dim intPwdLen As Integer
                intPwdLen = strPassword.Length

                '加密密码
                strTemp = strPassword
                strTemp = strTemp + m_cstrEncryptString.Substring(intPwdLen)

            Catch ex As Exception
                strTemp = strPassword
            End Try

            '返回
            doEncryptPassowrd = strTemp

        End Function

        '----------------------------------------------------------------
        ' 验证用户与密码是否匹配？
        '     strErrMsg     ：如果错误，则返回错误信息
        '     strUserId     ：要验证的用户标识
        '     strPassword   ：要验证的用户的密码
        ' 返回
        '     True          ：用户与密码一致
        '     False         ：用户与密码不匹配
        '----------------------------------------------------------------
        Public Function doVerifyUserPassword( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection

            '初始化
            doVerifyUserPassword = False
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""

                '检查
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要验证的用户！"
                    GoTo errProc
                End If

                '获取连接串
                Dim intConnectionTestTimeout As Integer
                Dim strConnectionString As String
                intConnectionTestTimeout = Xydc.Platform.Common.jsoaConfiguration.ConnectionTestTimeout
                strConnectionString = Xydc.Platform.Common.jsoaConfiguration.getConnectionString(strUserId, strPassword, intConnectionTestTimeout)

                '创建数据库连接
                Try
                    objSqlConnection = New System.Data.SqlClient.SqlConnection(strConnectionString)
                    objSqlConnection.Open()
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)

            '返回
            doVerifyUserPassword = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 验证数据库连接串
        '     strErrMsg     ：如果错误，则返回错误信息
        '     strConnect    ：要验证的连接串
        ' 返回
        '     True          ：有效
        '     False         ：无效
        '----------------------------------------------------------------
        Public Function doVerifyConnectionString( _
            ByRef strErrMsg As String, _
            ByVal strConnect As String) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection

            '初始化
            doVerifyConnectionString = False
            strErrMsg = ""

            Try
                '检查
                If strConnect Is Nothing Then strConnect = ""
                strConnect = strConnect.Trim()
                If strConnect.Length < 1 Then
                    strErrMsg = "错误：未指定要验证的连接串！"
                    GoTo errProc
                End If

                '获取连接串
                Dim strConnectionString As String
                strConnectionString = strConnect

                '创建数据库连接
                Try
                    objSqlConnection = New System.Data.SqlClient.SqlConnection(strConnectionString)
                    objSqlConnection.Open()
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)

            '返回
            doVerifyConnectionString = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 操作员更改自己的密码或管理员强制更改其他用户的密码
        '     strErrMsg     ：如果错误，则返回错误信息
        '     strCzyId      ：当前操作人员
        '     strCzyPassword：当前操作人员的密码
        '     strUserId     ：要更改密码的用户标识
        '     strNewPassword：要更改密码的用户的新密码
        ' 返回
        '     True          ：更改成功
        '     False         ：更改失败
        '----------------------------------------------------------------
        Public Function doModifyUserPassword( _
            ByRef strErrMsg As String, _
            ByVal strCzyId As String, _
            ByVal strCzyPassword As String, _
            ByVal strUserId As String, _
            ByVal strNewPassword As String) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            doModifyUserPassword = False
            strErrMsg = ""

            Try
                If strCzyPassword.Length > 0 Then strCzyPassword = strCzyPassword.Trim()
                If strNewPassword.Length > 0 Then strNewPassword = strNewPassword.Trim()
                If strCzyId.Length > 0 Then strCzyId = strCzyId.Trim()
                If strUserId Is Nothing Then strUserId = ""

                '检查
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要更改密码的用户！"
                    GoTo errProc
                End If
                Dim blnEnforced As Boolean = False
                If strUserId <> strCzyId Then
                    If strCzyId.ToUpper() = "SA" Then
                        '管理员强制更改别人密码
                        blnEnforced = True
                    Else
                        strErrMsg = "错误：只有管理员能更改别人的密码！"
                        GoTo errProc
                    End If
                Else
                    '操作员更改自己的密码
                End If

                '获取连接串
                Dim intConnectionTestTimeout As Integer
                Dim strConnectionString As String
                Dim intCommandTimeout As Integer
                Dim strDatabase As String
                intConnectionTestTimeout = Xydc.Platform.Common.jsoaConfiguration.ConnectionTestTimeout
                intCommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout
                strDatabase = Xydc.Platform.Common.jsoaConfiguration.DatabaseServerMasterDB
                strConnectionString = Xydc.Platform.Common.jsoaConfiguration.getConnectionString(strCzyId, strCzyPassword, intConnectionTestTimeout, strDatabase)

                '创建数据库连接
                Dim strSQL As String
                Try
                    objSqlConnection = New System.Data.SqlClient.SqlConnection(strConnectionString)
                    With objSqlConnection
                        '打开连接
                        .Open()

                        '准备命令
                        If blnEnforced = True Then
                            '管理员强制更改别人密码
                            If strNewPassword = "" Then
                                strSQL = "sp_password NULL, NULL, '" + strUserId + "'"
                            Else
                                strSQL = "sp_password NULL, '" + strNewPassword + "', '" + strUserId + "'"
                            End If
                        Else
                            '操作员更改自己的密码
                            If strCzyPassword = "" And strNewPassword = "" Then
                                strSQL = ""
                            ElseIf strCzyPassword = "" Then
                                strSQL = "sp_password NULL, '" + strNewPassword + "'"
                            ElseIf strNewPassword = "" Then
                                strSQL = "sp_password '" + strCzyPassword + "', NULL"
                            Else
                                strSQL = "sp_password '" + strCzyPassword + "', '" + strNewPassword + "'"
                            End If
                        End If

                        '执行命令
                        If strSQL.Length > 0 Then
                            objSqlCommand = .CreateCommand()
                            With objSqlCommand
                                .CommandTimeout = intCommandTimeout
                                .CommandText = strSQL
                                .ExecuteNonQuery()
                            End With
                        End If
                    End With
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

            '返回
            doModifyUserPassword = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取完整用户信息数据集
        '     strErrMsg      ：如果错误，则返回错误信息
        '     strUserId      ：用户标识
        '     strPassword    ：用户密码
        '     strWhere       ：搜索条件
        '     blnUnused      ：重载用
        '     objCustomerData：用户信息数据集
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Public Function getRenyuanData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByVal blnUnused As Boolean, _
            ByRef objCustomerData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Dim objTempCustomerData As Xydc.Platform.Common.Data.CustomerData
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objDataTable As System.Data.DataTable
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '初始化
            getRenyuanData = False
            objCustomerData = Nothing
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
                    objTempCustomerData = New Xydc.Platform.Common.Data.CustomerData(Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_RENYUAN_FULLJOIN)

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
                        strSQL = strSQL + "     b.组织名称,b.组织别名," + vbCr
                        strSQL = strSQL + "     岗位列表 = dbo.GetGWMCByRydm(a.人员代码,@separate)," + vbCr
                        strSQL = strSQL + "     c.级别名称,c.行政级别," + vbCr
                        strSQL = strSQL + "     秘书名称 = d.人员名称," + vbCr
                        strSQL = strSQL + "     其他由转送名称 = e.人员名称," + vbCr
                        strSQL = strSQL + "     是否申请 = @charfalse" + vbCr
                        strSQL = strSQL + "   from 公共_B_人员 a" + vbCr
                        strSQL = strSQL + "   left join 公共_B_组织机构 b on a.组织代码   = b.组织代码 " + vbCr
                        strSQL = strSQL + "   left join 公共_B_行政级别 c on a.级别代码   = c.级别代码 " + vbCr
                        strSQL = strSQL + "   left join 公共_B_人员     d on a.秘书代码   = d.人员代码 " + vbCr
                        strSQL = strSQL + "   left join 公共_B_人员     e on a.其他由转送 = e.人员代码 " + vbCr
                        strSQL = strSQL + " ) a" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.组织代码,cast(a.人员序号 as integer)" + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@separate", Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate)
                        objSqlCommand.Parameters.AddWithValue("@charfalse", Xydc.Platform.Common.Utilities.PulicParameters.CharFalse)
                        .SelectCommand = objSqlCommand

                        .Fill(objTempCustomerData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN))
                    End With

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempCustomerData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempCustomerData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataTable)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objCustomerData = objTempCustomerData
            getRenyuanData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataTable)
            Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempCustomerData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据用户Id获取用户信息数据集
        '     strErrMsg      ：如果错误，则返回错误信息
        '     strUserId      ：用户标识
        '     strPassword    ：用户密码
        '     strOptions     ：获取数据选项ABCD
        '                      A=1 获取人员单表数据
        '                      B=1 获取人员的组织机构单表数据
        '                      C=1 获取人员的上岗单表数据
        '                      D=1 获取人员的完全连接的表数据
        '     objCustomerData：用户信息数据集
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Public Function getRenyuanData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strOptions As String, _
            ByRef objCustomerData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempCustomerData As Xydc.Platform.Common.Data.CustomerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objDataTable As System.Data.DataTable

            '初始化
            getRenyuanData = False
            objCustomerData = Nothing
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strOptions.Length > 0 Then strOptions = strOptions.Trim()

                '检查
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If strOptions = "" Then strOptions = "0001"

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                Dim strSQL As String
                Try
                    '创建数据集
                    objTempCustomerData = New Xydc.Platform.Common.Data.CustomerData

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '检索全部连接的人员信息
                        If strOptions.Substring(3, 1) = "1" Then
                            '准备SQL
                            strSQL = ""
                            strSQL = strSQL + " select a.*," + vbCr
                            strSQL = strSQL + "   b.组织名称,b.组织别名," + vbCr
                            strSQL = strSQL + "   岗位列表 = dbo.GetGWMCByRydm(a.人员代码,@separate)," + vbCr
                            strSQL = strSQL + "   c.级别名称,c.行政级别," + vbCr
                            strSQL = strSQL + "   秘书名称 = d.人员名称," + vbCr
                            strSQL = strSQL + "   其他由转送名称 = e.人员名称," + vbCr
                            strSQL = strSQL + "   是否申请 = @charfalse" + vbCr
                            strSQL = strSQL + " from " + vbCr
                            strSQL = strSQL + " (" + vbCr
                            strSQL = strSQL + "   select * from 公共_B_人员 " + vbCr
                            strSQL = strSQL + "   where 人员代码 = @rydm" + vbCr
                            strSQL = strSQL + " ) a " + vbCr
                            strSQL = strSQL + " left join 公共_B_组织机构 b on a.组织代码   = b.组织代码 " + vbCr
                            strSQL = strSQL + " left join 公共_B_行政级别 c on a.级别代码   = c.级别代码 " + vbCr
                            strSQL = strSQL + " left join 公共_B_人员     d on a.秘书代码   = d.人员代码 " + vbCr
                            strSQL = strSQL + " left join 公共_B_人员     e on a.其他由转送 = e.人员代码 " + vbCr

                            '设置参数
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@rydm", strUserId)
                            objSqlCommand.Parameters.AddWithValue("@separate", Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate)
                            objSqlCommand.Parameters.AddWithValue("@charfalse", Xydc.Platform.Common.Utilities.PulicParameters.CharFalse)
                            .SelectCommand = objSqlCommand

                            '执行操作
                            With objTempCustomerData
                                objDataTable = Nothing
                                objDataTable = .createDataTables(strErrMsg, Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_RENYUAN_FULLJOIN)
                                If Not (objDataTable Is Nothing) Then
                                    strErrMsg = .appendDataTable(objDataTable)
                                    If strErrMsg <> "" Then
                                        GoTo errProc
                                    End If
                                Else
                                    GoTo errProc
                                End If
                                objDataTable = Nothing
                            End With
                            .Fill(objTempCustomerData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN))
                        End If

                        '检索人员的上岗信息
                        If strOptions.Substring(2, 1) = "1" Then
                            '准备SQL
                            strSQL = ""
                            strSQL = strSQL + " select a.* " + vbCr
                            strSQL = strSQL + " from 公共_B_上岗 a " + vbCr
                            strSQL = strSQL + " where 人员代码 = @rydm " + vbCr
                            strSQL = strSQL + " order by a.岗位代码"

                            '设置参数
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@rydm", strUserId)
                            .SelectCommand = objSqlCommand

                            '执行操作
                            With objTempCustomerData
                                objDataTable = Nothing
                                objDataTable = .createDataTables(strErrMsg, Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_SHANGGANG)
                                If Not (objDataTable Is Nothing) Then
                                    strErrMsg = .appendDataTable(objDataTable)
                                    If strErrMsg <> "" Then
                                        GoTo errProc
                                    End If
                                Else
                                    GoTo errProc
                                End If
                                objDataTable = Nothing
                            End With
                            .Fill(objTempCustomerData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_SHANGGANG))
                        End If

                        '检索人员的组织机构单表信息
                        If strOptions.Substring(1, 1) = "1" Then
                            '准备SQL
                            strSQL = ""
                            strSQL = strSQL + " select a.* " + vbCr
                            strSQL = strSQL + " from 公共_B_组织机构 a " + vbCr
                            strSQL = strSQL + " left join " + vbCr
                            strSQL = strSQL + " (" + vbCr
                            strSQL = strSQL + "   select 组织代码 " + vbCr
                            strSQL = strSQL + "   from 公共_B_人员 " + vbCr
                            strSQL = strSQL + "   where 人员代码 = @rydm"
                            strSQL = strSQL + " ) b on a.组织代码 = b.组织代码 " + vbCr
                            strSQL = strSQL + " where b.组织代码 is not null " + vbCr

                            '设置参数
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@rydm", strUserId)
                            .SelectCommand = objSqlCommand

                            '执行操作
                            With objTempCustomerData
                                objDataTable = Nothing
                                objDataTable = .createDataTables(strErrMsg, Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_ZUZHIJIGOU)
                                If Not (objDataTable Is Nothing) Then
                                    strErrMsg = .appendDataTable(objDataTable)
                                    If strErrMsg <> "" Then
                                        GoTo errProc
                                    End If
                                Else
                                    GoTo errProc
                                End If
                                objDataTable = Nothing
                            End With
                            .Fill(objTempCustomerData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU))
                        End If

                        '检索人员单表信息
                        If strOptions.Substring(0, 1) = "1" Then
                            '准备SQL
                            strSQL = ""
                            strSQL = strSQL + " select a.* " + vbCr
                            strSQL = strSQL + " from 公共_B_人员 a " + vbCr
                            strSQL = strSQL + " where a.人员代码 = @rydm " + vbCr

                            '设置参数
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@rydm", strUserId)
                            .SelectCommand = objSqlCommand

                            '执行操作
                            With objTempCustomerData
                                objDataTable = Nothing
                                objDataTable = .createDataTables(strErrMsg, Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_RENYUAN)
                                If Not (objDataTable Is Nothing) Then
                                    strErrMsg = .appendDataTable(objDataTable)
                                    If strErrMsg <> "" Then
                                        GoTo errProc
                                    End If
                                Else
                                    GoTo errProc
                                End If
                                objDataTable = Nothing
                            End With
                            .Fill(objTempCustomerData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN))
                        End If
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempCustomerData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempCustomerData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataTable)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objCustomerData = objTempCustomerData
            getRenyuanData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataTable)
            Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempCustomerData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据strRYDM获取用户信息数据集
        '     strErrMsg      ：如果错误，则返回错误信息
        '     strUserId      ：用户标识
        '     strPassword    ：用户密码
        '     strRYDM        ：人员代码
        '     strZZDM        ：要获取的组织代码
        '     strOptions     ：获取数据选项ABCD
        '                      A=1 获取人员单表数据
        '                      B=1 获取人员的组织机构单表数据
        '                      C=1 获取人员的上岗单表数据
        '                      D=1 获取人员的完全连接的表数据
        '     blnUser        ：重载
        '     objCustomerData：用户信息数据集
        ' 返回
        '     True           ：成功
        '     False          ：失败

        '----------------------------------------------------------------
        Public Function getRenyuanData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRYDM As String, _
            ByVal strZZDM As String, _
            ByVal strOptions As String, _
            ByVal blnUser As Boolean, _
            ByRef objCustomerData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempCustomerData As Xydc.Platform.Common.Data.CustomerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objDataTable As System.Data.DataTable

            '初始化
            getRenyuanData = False
            objCustomerData = Nothing
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strOptions.Length > 0 Then strOptions = strOptions.Trim()

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
                    objTempCustomerData = New Xydc.Platform.Common.Data.CustomerData

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '检索全部连接的人员信息

                        '准备SQL
                        strSQL = ""
                        If strZZDM <> "" Then
                            strSQL = strSQL + "  select * from "
                            strSQL = strSQL + " ("
                            'strSQL = strSQL + " select '' as 编号,a.*," + vbCr
                            strSQL = strSQL + "   select '' as 编号, "
                            strSQL = strSQL + " a.人员代码, a.人员名称, a.人员真名, a.人员序号,"
                            strSQL = strSQL + "  a.组织代码, a.级别代码, a.秘书代码, a.联系电话,"
                            strSQL = strSQL + " a.手机号码, a.FTP地址, a.邮箱地址, a.自动签收, "
                            strSQL = strSQL + " a.交接显示名称, a.可查看姓名, a.可直送人员, a.其他由转送, a.是否加密,"
                            strSQL = strSQL + "   b.组织名称,b.组织别名," + vbCr
                            strSQL = strSQL + "   岗位列表 = dbo.GetGWMCByRydm(a.人员代码,@separate)," + vbCr
                            strSQL = strSQL + "   c.级别名称,c.行政级别," + vbCr
                            strSQL = strSQL + "   秘书名称 = d.人员名称," + vbCr
                            strSQL = strSQL + "   其他由转送名称 = e.人员名称," + vbCr
                            strSQL = strSQL + "   是否申请 = @charfalse" + vbCr
                            strSQL = strSQL + " from " + vbCr
                            strSQL = strSQL + " (" + vbCr
                            strSQL = strSQL + "   select * from 公共_B_人员 " + vbCr
                            strSQL = strSQL + "   where 人员代码 = @rydm  " + vbCr
                            strSQL = strSQL + " ) a " + vbCr
                            strSQL = strSQL + " left join 公共_B_组织机构 b on a.组织代码   = b.组织代码 " + vbCr
                            strSQL = strSQL + " left join 公共_B_行政级别 c on a.级别代码   = c.级别代码 " + vbCr
                            strSQL = strSQL + " left join 公共_B_人员     d on a.秘书代码   = d.人员代码 " + vbCr
                            strSQL = strSQL + " left join 公共_B_人员     e on a.其他由转送 = e.人员代码 " + vbCr
                            strSQL = strSQL + "union"
                            'strSQL = strSQL + " select a.*," + vbCr
                            strSQL = strSQL + "   select a.编号, "
                            strSQL = strSQL + " a.人员代码, a.人员名称, a.人员真名, a.人员序号,"
                            strSQL = strSQL + "  a.组织代码, a.级别代码, a.秘书代码, a.联系电话,"
                            strSQL = strSQL + " a.手机号码, a.FTP地址, a.邮箱地址, a.自动签收, "
                            strSQL = strSQL + " a.交接显示名称, a.可查看姓名, a.可直送人员, a.其他由转送, a.是否加密,"
                            strSQL = strSQL + "   b.组织名称,b.组织别名," + vbCr
                            strSQL = strSQL + "   岗位列表 = dbo.GetGWMCByRydm(a.人员代码,@separate)," + vbCr
                            strSQL = strSQL + "   c.级别名称,c.行政级别," + vbCr
                            strSQL = strSQL + "   秘书名称 = d.人员名称," + vbCr
                            strSQL = strSQL + "   其他由转送名称 = e.人员名称," + vbCr
                            strSQL = strSQL + "   是否申请 = @charfalse" + vbCr
                            strSQL = strSQL + " from " + vbCr
                            strSQL = strSQL + " (" + vbCr
                            'strSQL = strSQL + "   select a.* "
                            strSQL = strSQL + "   select a.编号, "
                            strSQL = strSQL + " a.人员代码, a.人员名称, a.人员真名, a.人员序号,"
                            strSQL = strSQL + "  a.组织代码, a.级别代码, a.秘书代码, a.联系电话,"
                            strSQL = strSQL + " a.手机号码, a.FTP地址, a.邮箱地址, a.自动签收, "
                            strSQL = strSQL + " a.交接显示名称, a.可查看姓名, a.可直送人员, a.其他由转送, a.是否加密"
                            strSQL = strSQL + " from 公共_B_人员_兼任 a " + vbCr
                            strSQL = strSQL + "   where a.人员代码 = @rydm " + vbCr
                            strSQL = strSQL + " ) a " + vbCr
                            strSQL = strSQL + " left join 公共_B_组织机构 b on a.组织代码   = b.组织代码 " + vbCr
                            strSQL = strSQL + " left join 公共_B_行政级别 c on a.级别代码   = c.级别代码 " + vbCr
                            strSQL = strSQL + " left join 公共_B_人员_兼任     d on a.秘书代码   = d.人员代码 " + vbCr
                            strSQL = strSQL + " left join 公共_B_人员_兼任     e on a.其他由转送 = e.人员代码 " + vbCr
                            strSQL = strSQL + " )a"
                            strSQL = strSQL + "   where a.组织代码=@zzdm " + vbCr

                            '设置参数
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@rydm", strRYDM)
                            objSqlCommand.Parameters.AddWithValue("@zzdm", strZZDM)
                            objSqlCommand.Parameters.AddWithValue("@separate", Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate)
                            objSqlCommand.Parameters.AddWithValue("@charfalse", Xydc.Platform.Common.Utilities.PulicParameters.CharFalse)
                            .SelectCommand = objSqlCommand
                        Else
                            strSQL = ""
                            'strSQL = strSQL + " select '' as 编号,a.*," + vbCr
                            strSQL = strSQL + "   select '' as 编号, "
                            strSQL = strSQL + " a.人员代码, a.人员名称, a.人员真名, a.人员序号,"
                            strSQL = strSQL + "  a.组织代码, a.级别代码, a.秘书代码, a.联系电话,"
                            strSQL = strSQL + " a.手机号码, a.FTP地址, a.邮箱地址, a.自动签收, "
                            strSQL = strSQL + " a.交接显示名称, a.可查看姓名, a.可直送人员, a.其他由转送, a.是否加密,"
                            strSQL = strSQL + "   b.组织名称,b.组织别名," + vbCr
                            strSQL = strSQL + "   岗位列表 = dbo.GetGWMCByRydm(a.人员代码,@separate)," + vbCr
                            strSQL = strSQL + "   c.级别名称,c.行政级别," + vbCr
                            strSQL = strSQL + "   秘书名称 = d.人员名称," + vbCr
                            strSQL = strSQL + "   其他由转送名称 = e.人员名称," + vbCr
                            strSQL = strSQL + "   是否申请 = @charfalse" + vbCr
                            strSQL = strSQL + " from " + vbCr
                            strSQL = strSQL + " (" + vbCr
                            strSQL = strSQL + "   select * from 公共_B_人员 " + vbCr
                            strSQL = strSQL + "   where 人员代码 = @rydm " + vbCr
                            strSQL = strSQL + " ) a " + vbCr
                            strSQL = strSQL + " left join 公共_B_组织机构 b on a.组织代码   = b.组织代码 " + vbCr
                            strSQL = strSQL + " left join 公共_B_行政级别 c on a.级别代码   = c.级别代码 " + vbCr
                            strSQL = strSQL + " left join 公共_B_人员     d on a.秘书代码   = d.人员代码 " + vbCr
                            strSQL = strSQL + " left join 公共_B_人员     e on a.其他由转送 = e.人员代码 " + vbCr
                            strSQL = strSQL + "union"
                            'strSQL = strSQL + " select a.*," + vbCr
                            strSQL = strSQL + "   select a.编号, "
                            strSQL = strSQL + " a.人员代码, a.人员名称, a.人员真名, a.人员序号,"
                            strSQL = strSQL + "  a.组织代码, a.级别代码, a.秘书代码, a.联系电话,"
                            strSQL = strSQL + " a.手机号码, a.FTP地址, a.邮箱地址, a.自动签收, "
                            strSQL = strSQL + " a.交接显示名称, a.可查看姓名, a.可直送人员, a.其他由转送, a.是否加密,"
                            strSQL = strSQL + "   b.组织名称,b.组织别名," + vbCr
                            strSQL = strSQL + "   岗位列表 = dbo.GetGWMCByRydm(a.人员代码,@separate)," + vbCr
                            strSQL = strSQL + "   c.级别名称,c.行政级别," + vbCr
                            strSQL = strSQL + "   秘书名称 = d.人员名称," + vbCr
                            strSQL = strSQL + "   其他由转送名称 = e.人员名称," + vbCr
                            'strSQL = strSQL + "   ''  as  秘书名称," + vbCr
                            'strSQL = strSQL + "   '' as 其他由转送名称," + vbCr
                            strSQL = strSQL + "   是否申请 = @charfalse" + vbCr
                            strSQL = strSQL + " from " + vbCr
                            strSQL = strSQL + " (" + vbCr
                            'strSQL = strSQL + "   select a.* "
                            strSQL = strSQL + "   select a.编号, "
                            strSQL = strSQL + " a.人员代码, a.人员名称, a.人员真名, a.人员序号,"
                            strSQL = strSQL + "  a.组织代码, a.级别代码, a.秘书代码, a.联系电话,"
                            strSQL = strSQL + " a.手机号码, a.FTP地址, a.邮箱地址, a.自动签收, "
                            strSQL = strSQL + " a.交接显示名称, a.可查看姓名, a.可直送人员, a.其他由转送, a.是否加密"
                            strSQL = strSQL + " from 公共_B_人员_兼任 a " + vbCr
                            strSQL = strSQL + "   where a.人员代码 = @rydm " + vbCr
                            strSQL = strSQL + " ) a " + vbCr
                            strSQL = strSQL + " left join 公共_B_组织机构 b on a.组织代码   = b.组织代码 " + vbCr
                            strSQL = strSQL + " left join 公共_B_行政级别 c on a.级别代码   = c.级别代码 " + vbCr
                            strSQL = strSQL + " left join 公共_B_人员_兼任     d on a.秘书代码   = d.人员代码 " + vbCr
                            strSQL = strSQL + " left join 公共_B_人员_兼任     e on a.其他由转送 = e.人员代码 " + vbCr

                            '设置参数
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@rydm", strRYDM)
                            objSqlCommand.Parameters.AddWithValue("@separate", Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate)
                            objSqlCommand.Parameters.AddWithValue("@charfalse", Xydc.Platform.Common.Utilities.PulicParameters.CharFalse)
                            .SelectCommand = objSqlCommand
                        End If

                        '执行操作
                        With objTempCustomerData
                            objDataTable = Nothing
                            objDataTable = .createDataTables(strErrMsg, Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_RENYUAN_FULLJOIN)
                            If Not (objDataTable Is Nothing) Then
                                strErrMsg = .appendDataTable(objDataTable)
                                If strErrMsg <> "" Then
                                    GoTo errProc
                                End If
                            Else
                                GoTo errProc
                            End If
                            objDataTable = Nothing
                        End With
                        .Fill(objTempCustomerData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempCustomerData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempCustomerData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataTable)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objCustomerData = objTempCustomerData
            getRenyuanData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataTable)
            Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempCustomerData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据strRYDM获取用户信息数据集
        '     strErrMsg      ：如果错误，则返回错误信息
        '     strUserId      ：用户标识
        '     strPassword    ：用户密码
        '     strRYDM        ：人员代码
        '     strOptions     ：获取数据选项ABCD
        '                      A=1 获取人员单表数据
        '                      B=1 获取人员的组织机构单表数据
        '                      C=1 获取人员的上岗单表数据
        '                      D=1 获取人员的完全连接的表数据
        '     objCustomerData：用户信息数据集
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Public Function getRenyuanData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRYDM As String, _
            ByVal strOptions As String, _
            ByRef objCustomerData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempCustomerData As Xydc.Platform.Common.Data.CustomerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objDataTable As System.Data.DataTable

            '初始化
            getRenyuanData = False
            objCustomerData = Nothing
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strRYDM.Length > 0 Then strRYDM = strRYDM.Trim()
                If strOptions.Length > 0 Then strOptions = strOptions.Trim()

                '检查
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If strOptions = "" Then strOptions = "0001"

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                Dim strSQL As String
                Try
                    '创建数据集
                    objTempCustomerData = New Xydc.Platform.Common.Data.CustomerData

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '检索全部连接的人员信息
                        If strOptions.Substring(3, 1) = "1" Then
                            '准备SQL
                            strSQL = ""
                            strSQL = strSQL + " select a.*," + vbCr
                            strSQL = strSQL + "   b.组织名称,b.组织别名," + vbCr
                            strSQL = strSQL + "   岗位列表 = dbo.GetGWMCByRydm(a.人员代码,@separate)," + vbCr
                            strSQL = strSQL + "   c.级别名称,c.行政级别," + vbCr
                            strSQL = strSQL + "   秘书名称 = d.人员名称," + vbCr
                            strSQL = strSQL + "   其他由转送名称 = e.人员名称," + vbCr
                            strSQL = strSQL + "   是否申请 = @charfalse" + vbCr
                            strSQL = strSQL + " from " + vbCr
                            strSQL = strSQL + " (" + vbCr
                            strSQL = strSQL + "   select * from 公共_B_人员 " + vbCr
                            strSQL = strSQL + "   where 人员代码 = @rydm" + vbCr
                            strSQL = strSQL + " ) a " + vbCr
                            strSQL = strSQL + " left join 公共_B_组织机构 b on a.组织代码   = b.组织代码 " + vbCr
                            strSQL = strSQL + " left join 公共_B_行政级别 c on a.级别代码   = c.级别代码 " + vbCr
                            strSQL = strSQL + " left join 公共_B_人员     d on a.秘书代码   = d.人员代码 " + vbCr
                            strSQL = strSQL + " left join 公共_B_人员     e on a.其他由转送 = e.人员代码 " + vbCr

                            '设置参数
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@rydm", strRYDM)
                            objSqlCommand.Parameters.AddWithValue("@separate", Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate)
                            objSqlCommand.Parameters.AddWithValue("@charfalse", Xydc.Platform.Common.Utilities.PulicParameters.CharFalse)
                            .SelectCommand = objSqlCommand

                            '执行操作
                            With objTempCustomerData
                                objDataTable = Nothing
                                objDataTable = .createDataTables(strErrMsg, Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_RENYUAN_FULLJOIN)
                                If Not (objDataTable Is Nothing) Then
                                    strErrMsg = .appendDataTable(objDataTable)
                                    If strErrMsg <> "" Then
                                        GoTo errProc
                                    End If
                                Else
                                    GoTo errProc
                                End If
                                objDataTable = Nothing
                            End With
                            .Fill(objTempCustomerData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN))
                        End If

                        '检索人员的上岗信息
                        If strOptions.Substring(2, 1) = "1" Then
                            '准备SQL
                            strSQL = ""
                            strSQL = strSQL + " select a.* " + vbCr
                            strSQL = strSQL + " from 公共_B_上岗 a " + vbCr
                            strSQL = strSQL + " where 人员代码 = @rydm " + vbCr
                            strSQL = strSQL + " order by a.岗位代码"

                            '设置参数
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@rydm", strRYDM)
                            .SelectCommand = objSqlCommand

                            '执行操作
                            With objTempCustomerData
                                objDataTable = Nothing
                                objDataTable = .createDataTables(strErrMsg, Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_SHANGGANG)
                                If Not (objDataTable Is Nothing) Then
                                    strErrMsg = .appendDataTable(objDataTable)
                                    If strErrMsg <> "" Then
                                        GoTo errProc
                                    End If
                                Else
                                    GoTo errProc
                                End If
                                objDataTable = Nothing
                            End With
                            .Fill(objTempCustomerData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_SHANGGANG))
                        End If

                        '检索人员的组织机构单表信息
                        If strOptions.Substring(1, 1) = "1" Then
                            '准备SQL
                            strSQL = ""
                            strSQL = strSQL + " select a.* " + vbCr
                            strSQL = strSQL + " from 公共_B_组织机构 a " + vbCr
                            strSQL = strSQL + " left join " + vbCr
                            strSQL = strSQL + " (" + vbCr
                            strSQL = strSQL + "   select 组织代码 " + vbCr
                            strSQL = strSQL + "   from 公共_B_人员 " + vbCr
                            strSQL = strSQL + "   where 人员代码 = @rydm"
                            strSQL = strSQL + " ) b on a.组织代码 = b.组织代码 " + vbCr
                            strSQL = strSQL + " where b.组织代码 is not null " + vbCr

                            '设置参数
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@rydm", strRYDM)
                            .SelectCommand = objSqlCommand

                            '执行操作
                            With objTempCustomerData
                                objDataTable = Nothing
                                objDataTable = .createDataTables(strErrMsg, Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_ZUZHIJIGOU)
                                If Not (objDataTable Is Nothing) Then
                                    strErrMsg = .appendDataTable(objDataTable)
                                    If strErrMsg <> "" Then
                                        GoTo errProc
                                    End If
                                Else
                                    GoTo errProc
                                End If
                                objDataTable = Nothing
                            End With
                            .Fill(objTempCustomerData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU))
                        End If

                        '检索人员单表信息
                        If strOptions.Substring(0, 1) = "1" Then
                            '准备SQL
                            strSQL = ""
                            strSQL = strSQL + " select a.* " + vbCr
                            strSQL = strSQL + " from 公共_B_人员 a " + vbCr
                            strSQL = strSQL + " where a.人员代码 = @rydm " + vbCr

                            '设置参数
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@rydm", strRYDM)
                            .SelectCommand = objSqlCommand

                            '执行操作
                            With objTempCustomerData
                                objDataTable = Nothing
                                objDataTable = .createDataTables(strErrMsg, Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_RENYUAN)
                                If Not (objDataTable Is Nothing) Then
                                    strErrMsg = .appendDataTable(objDataTable)
                                    If strErrMsg <> "" Then
                                        GoTo errProc
                                    End If
                                Else
                                    GoTo errProc
                                End If
                                objDataTable = Nothing
                            End With
                            .Fill(objTempCustomerData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN))
                        End If
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempCustomerData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempCustomerData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataTable)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objCustomerData = objTempCustomerData
            getRenyuanData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataTable)
            Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempCustomerData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据strRYMC获取用户信息数据集
        '     strErrMsg      ：如果错误，则返回错误信息
        '     strUserId      ：用户标识
        '     strPassword    ：用户密码
        '     strRYDM        ：人员代码(接口重载用)
        '     strRYMC        ：人员名称
        '     strOptions     ：获取数据选项ABCD
        '                      A=1 获取人员单表数据
        '                      B=1 获取人员的组织机构单表数据
        '                      C=1 获取人员的上岗单表数据
        '                      D=1 获取人员的完全连接的表数据
        '     objCustomerData：用户信息数据集
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Public Function getRenyuanData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRYDM As String, _
            ByVal strRYMC As String, _
            ByVal strOptions As String, _
            ByRef objCustomerData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempCustomerData As Xydc.Platform.Common.Data.CustomerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objDataTable As System.Data.DataTable

            '初始化
            getRenyuanData = False
            objCustomerData = Nothing
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strRYDM.Length > 0 Then strRYDM = strRYDM.Trim()
                If strRYMC.Length > 0 Then strRYMC = strRYMC.Trim()
                If strOptions.Length > 0 Then strOptions = strOptions.Trim()

                '检查
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If strOptions = "" Then strOptions = "0001"

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                Dim strSQL As String
                Try
                    '创建数据集
                    objTempCustomerData = New Xydc.Platform.Common.Data.CustomerData

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '检索全部连接的人员信息
                        If strOptions.Substring(3, 1) = "1" Then
                            '准备SQL
                            strSQL = ""
                            strSQL = strSQL + " select a.*," + vbCr
                            strSQL = strSQL + "   b.组织名称,b.组织别名," + vbCr
                            strSQL = strSQL + "   岗位列表 = dbo.GetGWMCByRydm(a.人员代码,@separate)," + vbCr
                            strSQL = strSQL + "   c.级别名称,c.行政级别," + vbCr
                            strSQL = strSQL + "   秘书名称 = d.人员名称," + vbCr
                            strSQL = strSQL + "   其他由转送名称 = e.人员名称," + vbCr
                            strSQL = strSQL + "   是否申请 = @charfalse" + vbCr
                            strSQL = strSQL + " from " + vbCr
                            strSQL = strSQL + " (" + vbCr
                            strSQL = strSQL + "   select * from 公共_B_人员 " + vbCr
                            strSQL = strSQL + "   where 人员名称 = @rymc" + vbCr
                            strSQL = strSQL + " ) a " + vbCr
                            strSQL = strSQL + " left join 公共_B_组织机构 b on a.组织代码   = b.组织代码 " + vbCr
                            strSQL = strSQL + " left join 公共_B_行政级别 c on a.级别代码   = c.级别代码 " + vbCr
                            strSQL = strSQL + " left join 公共_B_人员     d on a.秘书代码   = d.人员代码 " + vbCr
                            strSQL = strSQL + " left join 公共_B_人员     e on a.其他由转送 = e.人员代码 " + vbCr

                            '设置参数
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@rymc", strRYMC)
                            objSqlCommand.Parameters.AddWithValue("@separate", Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate)
                            objSqlCommand.Parameters.AddWithValue("@charfalse", Xydc.Platform.Common.Utilities.PulicParameters.CharFalse)
                            .SelectCommand = objSqlCommand

                            '执行操作
                            With objTempCustomerData
                                objDataTable = Nothing
                                objDataTable = .createDataTables(strErrMsg, Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_RENYUAN_FULLJOIN)
                                If Not (objDataTable Is Nothing) Then
                                    strErrMsg = .appendDataTable(objDataTable)
                                    If strErrMsg <> "" Then
                                        GoTo errProc
                                    End If
                                Else
                                    GoTo errProc
                                End If
                                objDataTable = Nothing
                            End With
                            .Fill(objTempCustomerData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN))
                        End If

                        '检索人员的上岗信息
                        If strOptions.Substring(2, 1) = "1" Then
                            '准备SQL
                            strSQL = ""
                            strSQL = strSQL + " select a.* " + vbCr
                            strSQL = strSQL + " from 公共_B_上岗 a " + vbCr
                            strSQL = strSQL + " left join 公共_B_人员 b on a.人员代码 = b.人员代码 "
                            strSQL = strSQL + " where b.人员名称 = @rymc " + vbCr
                            strSQL = strSQL + " order by a.岗位代码"

                            '设置参数
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@rymc", strRYMC)
                            .SelectCommand = objSqlCommand

                            '执行操作
                            With objTempCustomerData
                                objDataTable = Nothing
                                objDataTable = .createDataTables(strErrMsg, Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_SHANGGANG)
                                If Not (objDataTable Is Nothing) Then
                                    strErrMsg = .appendDataTable(objDataTable)
                                    If strErrMsg <> "" Then
                                        GoTo errProc
                                    End If
                                Else
                                    GoTo errProc
                                End If
                                objDataTable = Nothing
                            End With
                            .Fill(objTempCustomerData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_SHANGGANG))
                        End If

                        '检索人员的组织机构单表信息
                        If strOptions.Substring(1, 1) = "1" Then
                            '准备SQL
                            strSQL = ""
                            strSQL = strSQL + " select a.* " + vbCr
                            strSQL = strSQL + " from 公共_B_组织机构 a " + vbCr
                            strSQL = strSQL + " left join " + vbCr
                            strSQL = strSQL + " (" + vbCr
                            strSQL = strSQL + "   select 组织代码 " + vbCr
                            strSQL = strSQL + "   from 公共_B_人员 " + vbCr
                            strSQL = strSQL + "   where 人员名称 = @rymc"
                            strSQL = strSQL + " ) b on a.组织代码 = b.组织代码 " + vbCr
                            strSQL = strSQL + " where b.组织代码 is not null " + vbCr

                            '设置参数
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@rymc", strRYMC)
                            .SelectCommand = objSqlCommand

                            '执行操作
                            With objTempCustomerData
                                objDataTable = Nothing
                                objDataTable = .createDataTables(strErrMsg, Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_ZUZHIJIGOU)
                                If Not (objDataTable Is Nothing) Then
                                    strErrMsg = .appendDataTable(objDataTable)
                                    If strErrMsg <> "" Then
                                        GoTo errProc
                                    End If
                                Else
                                    GoTo errProc
                                End If
                                objDataTable = Nothing
                            End With
                            .Fill(objTempCustomerData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU))
                        End If

                        '检索人员单表信息
                        If strOptions.Substring(0, 1) = "1" Then
                            '准备SQL
                            strSQL = ""
                            strSQL = strSQL + " select a.* " + vbCr
                            strSQL = strSQL + " from 公共_B_人员 a " + vbCr
                            strSQL = strSQL + " where a.人员名称 = @rymc " + vbCr

                            '设置参数
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@rymc", strRYMC)
                            .SelectCommand = objSqlCommand

                            '执行操作
                            With objTempCustomerData
                                objDataTable = Nothing
                                objDataTable = .createDataTables(strErrMsg, Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_RENYUAN)
                                If Not (objDataTable Is Nothing) Then
                                    strErrMsg = .appendDataTable(objDataTable)
                                    If strErrMsg <> "" Then
                                        GoTo errProc
                                    End If
                                Else
                                    GoTo errProc
                                End If
                                objDataTable = Nothing
                            End With
                            .Fill(objTempCustomerData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN))
                        End If
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempCustomerData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempCustomerData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataTable)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objCustomerData = objTempCustomerData
            getRenyuanData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataTable)
            Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempCustomerData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取组织机构信息数据集(以组织代码升序排序,不含连接数据)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objBumenData         ：组织机构信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getBumenData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef objBumenData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempBumenData As Xydc.Platform.Common.Data.CustomerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getBumenData = False
            objBumenData = Nothing
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

                '获取数据
                Dim strSQL As String
                Try
                    '创建数据集
                    objTempBumenData = New Xydc.Platform.Common.Data.CustomerData(Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_ZUZHIJIGOU)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select * " + vbCr
                        strSQL = strSQL + " from 公共_B_组织机构 " + vbCr
                        strSQL = strSQL + " order by 组织代码 " + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempBumenData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempBumenData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempBumenData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objBumenData = objTempBumenData
            getBumenData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempBumenData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据组织代码获取组织机构全连接信息数据集
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strZZDM              ：组织代码
        '     objBumenData         ：组织机构信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getBumenData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZZDM As String, _
            ByRef objBumenData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempBumenData As Xydc.Platform.Common.Data.CustomerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getBumenData = False
            objBumenData = Nothing
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strZZDM.Length > 0 Then strZZDM = strZZDM.Trim()

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
                    objTempBumenData = New Xydc.Platform.Common.Data.CustomerData(Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_ZUZHIJIGOU_FULLJOIN)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.*, " + vbCr
                        strSQL = strSQL + "   b.级别名称,b.行政级别, " + vbCr
                        strSQL = strSQL + "   秘书名称 = c.人员名称," + vbCr
                        strSQL = strSQL + "   联系人名称 = d.人员名称 " + vbCr
                        strSQL = strSQL + " from 公共_B_组织机构 a " + vbCr
                        strSQL = strSQL + " left join 公共_B_行政级别 b on a.级别代码 = b.级别代码 " + vbCr
                        strSQL = strSQL + " left join 公共_B_人员     c on a.秘书代码 = c.人员代码 " + vbCr
                        strSQL = strSQL + " left join 公共_B_人员     d on a.联系人   = d.人员代码 " + vbCr
                        strSQL = strSQL + " where a.组织代码 = @zzdm " + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@zzdm", strZZDM)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempBumenData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU_FULLJOIN))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempBumenData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempBumenData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objBumenData = objTempBumenData
            getBumenData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempBumenData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据组织代码获取组织机构全连接信息数据集
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strZZDM              ：组织代码
        '     objBumenData         ：组织机构信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getFWBumenData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZZDM As String, _
            ByRef objBumenData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempBumenData As Xydc.Platform.Common.Data.CustomerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getFWBumenData = False
            objBumenData = Nothing
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strZZDM.Length > 0 Then strZZDM = strZZDM.Trim()

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
                    objTempBumenData = New Xydc.Platform.Common.Data.CustomerData(Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_ZUZHIJIGOU)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from 公共_B_组织机构 a " + vbCr
                        strSQL = strSQL + " where a.组织代码 = @zzdm " + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@zzdm", strZZDM)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempBumenData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempBumenData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempBumenData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objBumenData = objTempBumenData
            getFWBumenData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempBumenData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据组织代码获取组织机构单表信息数据集
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strZZDM              ：组织代码
        '     blnUnused            ：重载用
        '     objBumenData         ：组织机构信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getBumenData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZZDM As String, _
            ByVal blnUnused As Boolean, _
            ByRef objBumenData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempBumenData As Xydc.Platform.Common.Data.CustomerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getBumenData = False
            objBumenData = Nothing
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strZZDM.Length > 0 Then strZZDM = strZZDM.Trim()

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
                    objTempBumenData = New Xydc.Platform.Common.Data.CustomerData(Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_ZUZHIJIGOU)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from 公共_B_组织机构 a " + vbCr
                        strSQL = strSQL + " where a.组织代码 = @zzdm " + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@zzdm", strZZDM)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempBumenData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempBumenData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempBumenData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objBumenData = objTempBumenData
            getBumenData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempBumenData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据组织名称获取组织机构全连接信息数据集
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strZZDM              ：组织代码(接口重载用)
        '     strZZMC              ：组织名称
        '     objBumenData         ：组织机构信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getBumenData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZZDM As String, _
            ByVal strZZMC As String, _
            ByRef objBumenData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempBumenData As Xydc.Platform.Common.Data.CustomerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getBumenData = False
            objBumenData = Nothing
            strErrMsg = ""
            strZZDM = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strZZMC.Length > 0 Then strZZMC = strZZMC.Trim()

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
                    objTempBumenData = New Xydc.Platform.Common.Data.CustomerData(Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_ZUZHIJIGOU_FULLJOIN)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.*, " + vbCr
                        strSQL = strSQL + "   b.级别名称,b.行政级别, " + vbCr
                        strSQL = strSQL + "   秘书名称 = c.人员名称," + vbCr
                        strSQL = strSQL + "   联系人名称 = d.人员名称 " + vbCr
                        strSQL = strSQL + " from 公共_B_组织机构 a " + vbCr
                        strSQL = strSQL + " left join 公共_B_行政级别 b on a.级别代码 = b.级别代码 " + vbCr
                        strSQL = strSQL + " left join 公共_B_人员     c on a.秘书代码 = c.人员代码 " + vbCr
                        strSQL = strSQL + " left join 公共_B_人员     d on a.联系人   = d.人员代码 " + vbCr
                        strSQL = strSQL + " where a.组织名称 = @zzmc " + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@zzmc", strZZMC)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempBumenData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU_FULLJOIN))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempBumenData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempBumenData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objBumenData = objTempBumenData
            getBumenData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempBumenData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据组织名称获取组织机构单表信息数据集
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     blnUnused            ：重载用
        '     strZZMC              ：组织名称
        '     objBumenData         ：组织机构信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getBumenData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal blnUnused As Boolean, _
            ByVal strZZMC As String, _
            ByRef objBumenData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempBumenData As Xydc.Platform.Common.Data.CustomerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getBumenData = False
            objBumenData = Nothing
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strZZMC.Length > 0 Then strZZMC = strZZMC.Trim()

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
                    objTempBumenData = New Xydc.Platform.Common.Data.CustomerData(Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_ZUZHIJIGOU)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from 公共_B_组织机构 a " + vbCr
                        strSQL = strSQL + " where a.组织名称 = @zzmc " + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@zzmc", strZZMC)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempBumenData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempBumenData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempBumenData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objBumenData = objTempBumenData
            getBumenData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempBumenData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取指定组织机构下的人员信息数据集(以组织代码、人员序号升序排序)
        ' 含人员的全部连接数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strZZDM              ：指定组织机构代码
        '     blnBaohanXiaji       ：是否包含下级部门
        '     strWhere             ：搜索字符串(默认表前缀a.)
        '     objRenyuanData       ：指定组织机构下的人员信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getRenyuanInBumenData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZZDM As String, _
            ByVal blnBaohanXiaji As Boolean, _
            ByVal strWhere As String, _
            ByRef objRenyuanData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempRenyuanData As Xydc.Platform.Common.Data.CustomerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getRenyuanInBumenData = False
            objRenyuanData = Nothing
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strZZDM.Length > 0 Then strZZDM = strZZDM.Trim()
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
                        'strSQL = strSQL + "   select ''as 编号,a.*," + vbCr
                        strSQL = strSQL + "   select ''as 编号, "
                        strSQL = strSQL + " a.人员代码, a.人员名称, a.人员真名, a.人员序号,"
                        strSQL = strSQL + "  a.组织代码, a.级别代码, a.秘书代码, a.联系电话,"
                        strSQL = strSQL + " a.手机号码, a.FTP地址, a.邮箱地址, a.自动签收, "
                        strSQL = strSQL + " a.交接显示名称, a.可查看姓名, a.可直送人员, a.其他由转送, a.是否加密,"
                        strSQL = strSQL + "     b.组织名称,b.组织别名," + vbCr
                        strSQL = strSQL + "     岗位列表 = dbo.GetGWMCByRydm(a.人员代码,@separate)," + vbCr
                        strSQL = strSQL + "     c.级别名称,c.行政级别," + vbCr
                        strSQL = strSQL + "     秘书名称 = d.人员名称," + vbCr
                        strSQL = strSQL + "     是否申请 = @charfalse" + vbCr
                        strSQL = strSQL + "   from " + vbCr
                        strSQL = strSQL + "   (" + vbCr
                        strSQL = strSQL + "     select * from 公共_B_人员 " + vbCr
                        If blnBaohanXiaji = True Then
                            strSQL = strSQL + "     where rtrim(组织代码) like @zzdm + '%'" + vbCr
                        Else
                            strSQL = strSQL + "     where 组织代码 = @zzdm" + vbCr
                        End If
                        strSQL = strSQL + "   ) a " + vbCr
                        strSQL = strSQL + "   left join 公共_B_组织机构 b on a.组织代码 = b.组织代码 " + vbCr
                        strSQL = strSQL + "   left join 公共_B_行政级别 c on a.级别代码 = c.级别代码 " + vbCr
                        strSQL = strSQL + "   left join 公共_B_人员     d on a.秘书代码 = d.人员代码 " + vbCr

                        strSQL = strSQL + "   union" + vbCr
                        'strSQL = strSQL + "   select a.*," + vbCr
                        strSQL = strSQL + "   select a.编号, "
                        strSQL = strSQL + " a.人员代码, a.人员名称, a.人员真名, a.人员序号,"
                        strSQL = strSQL + "  a.组织代码, a.级别代码, a.秘书代码, a.联系电话,"
                        strSQL = strSQL + " a.手机号码, a.FTP地址, a.邮箱地址, a.自动签收, "
                        strSQL = strSQL + " a.交接显示名称, a.可查看姓名, a.可直送人员, a.其他由转送, a.是否加密,"
                        strSQL = strSQL + "     b.组织名称,b.组织别名," + vbCr
                        strSQL = strSQL + "     岗位列表 = dbo.GetGWMCByRydm(a.人员代码,@separate)," + vbCr
                        strSQL = strSQL + "     c.级别名称,c.行政级别," + vbCr
                        strSQL = strSQL + "     秘书名称 = d.人员名称," + vbCr
                        strSQL = strSQL + "     是否申请 = @charfalse" + vbCr
                        strSQL = strSQL + "   from " + vbCr
                        strSQL = strSQL + "   (" + vbCr
                        'strSQL = strSQL + "   select a.* "
                        strSQL = strSQL + "   select a.编号, "
                        strSQL = strSQL + " a.人员代码, a.人员名称, a.人员真名, a.人员序号,"
                        strSQL = strSQL + "  a.组织代码, a.级别代码, a.秘书代码, a.联系电话,"
                        strSQL = strSQL + " a.手机号码, a.FTP地址, a.邮箱地址, a.自动签收, "
                        strSQL = strSQL + " a.交接显示名称, a.可查看姓名, a.可直送人员, a.其他由转送, a.是否加密"
                        strSQL = strSQL + " from 公共_B_人员_兼任 a " + vbCr
                        If blnBaohanXiaji = True Then
                            strSQL = strSQL + "     where rtrim(a.组织代码) like @zzdm + '%'" + vbCr
                        Else
                            strSQL = strSQL + "     where a.组织代码 = @zzdm" + vbCr
                        End If
                        strSQL = strSQL + "   ) a " + vbCr
                        strSQL = strSQL + "   left join 公共_B_组织机构 b on a.组织代码 = b.组织代码 " + vbCr
                        strSQL = strSQL + "   left join 公共_B_行政级别 c on a.级别代码 = c.级别代码 " + vbCr
                        strSQL = strSQL + "   left join 公共_B_人员_兼任     d on a.秘书代码 = d.人员代码 " + vbCr


                        strSQL = strSQL + " ) a "
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.组织代码, cast(a.人员序号 as integer)"

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@zzdm", strZZDM)
                        objSqlCommand.Parameters.AddWithValue("@separate", Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate)
                        objSqlCommand.Parameters.AddWithValue("@charfalse", Xydc.Platform.Common.Utilities.PulicParameters.CharFalse)
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
            getRenyuanInBumenData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempRenyuanData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据指定范围名称获取范围下的组织信息或人员信息
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strFWMC              ：指定范围名称
        '     blnAllowBM           ：允许部门信息直接选择
        '     strWhere             ：搜索条件(默认表前缀a.)
        '     objSelectRenyuanData ：指定组织机构下的人员信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getRenyuanOrBumenInFanweiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strFWMC As String, _
            ByVal blnAllowBM As Boolean, _
            ByVal strWhere As String, _
            ByRef objSelectRenyuanData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Dim objTempSelectRenyuanData As Xydc.Platform.Common.Data.CustomerData
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getRenyuanOrBumenInFanweiData = False
            objSelectRenyuanData = Nothing
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strFWMC.Length > 0 Then strFWMC = strFWMC.Trim()
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
                    objTempSelectRenyuanData = New Xydc.Platform.Common.Data.CustomerData(Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_RENYUAN_SELECT)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        If blnAllowBM = False Then
                            '不允许选择部门
                            '范围内的部门下的人员数据
                            strSQL = ""
                            strSQL = strSQL + " select a.* from (" + vbCr
                            strSQL = strSQL + "   select " + vbCr
                            strSQL = strSQL + "     名称 = a.人员名称," + vbCr
                            strSQL = strSQL + "     类型 = @cylx_gr," + vbCr
                            strSQL = strSQL + "     序号 = a.人员序号," + vbCr
                            strSQL = strSQL + "     部门 = c.组织名称," + vbCr
                            strSQL = strSQL + "     职务 = dbo.GetGWMCByRydm(a.人员代码,@separate)," + vbCr
                            strSQL = strSQL + "     级别 = d.级别名称," + vbCr
                            strSQL = strSQL + "     秘书 = e.人员名称," + vbCr
                            strSQL = strSQL + "     a.联系电话,a.手机号码,a.FTP地址,a.邮箱地址" + vbCr
                            strSQL = strSQL + "   from 公共_B_人员 a " + vbCr
                            strSQL = strSQL + "   left join (" + vbCr

                            '范围内单位及下级单位的组织代码
                            strSQL = strSQL + "     select " + vbCr
                            strSQL = strSQL + "       b.组织代码 " + vbCr
                            strSQL = strSQL + "     from (" + vbCr
                            strSQL = strSQL + "       select " + vbCr
                            strSQL = strSQL + "         b.组织代码" + vbCr
                            strSQL = strSQL + "       from (" + vbCr
                            strSQL = strSQL + "         select 成员名称 " + vbCr
                            strSQL = strSQL + "         from 公文_B_分发范围 " + vbCr
                            strSQL = strSQL + "         where 范围标志 = @fwbz " + vbCr
                            strSQL = strSQL + "         and 范围名称 = @fwmc" + vbCr
                            strSQL = strSQL + "         and 成员类型 = @cylx_dw" + vbCr
                            strSQL = strSQL + "       ) a " + vbCr
                            strSQL = strSQL + "       left join 公共_B_组织机构 b on a.成员名称 = b.组织名称 " + vbCr
                            strSQL = strSQL + "       where b.组织代码 is not null" + vbCr
                            strSQL = strSQL + "     ) a " + vbCr
                            strSQL = strSQL + "     left join 公共_B_组织机构 b on b.组织代码 like rtrim(a.组织代码)+'%' " + vbCr
                            strSQL = strSQL + "     group by b.组织代码 " + vbCr
                            '范围内单位及下级单位的组织代码

                            strSQL = strSQL + "   ) b on a.组织代码 = b.组织代码 " + vbCr
                            strSQL = strSQL + "   left join 公共_B_组织机构 c on a.组织代码 = c.组织代码 " + vbCr
                            strSQL = strSQL + "   left join 公共_B_行政级别 d on a.级别代码 = d.级别代码 " + vbCr
                            strSQL = strSQL + "   left join 公共_B_人员     e on a.秘书代码 = e.人员代码 " + vbCr
                            strSQL = strSQL + "   where b.组织代码 is not null " + vbCr

                            strSQL = strSQL + "   union " + vbCr

                            '范围内的人员数据
                            strSQL = strSQL + "   select " + vbCr
                            strSQL = strSQL + "     名称 = a.成员名称," + vbCr
                            strSQL = strSQL + "     类型 = @cylx_gr," + vbCr
                            strSQL = strSQL + "     序号 = b.人员序号," + vbCr
                            strSQL = strSQL + "     部门 = c.组织名称," + vbCr
                            strSQL = strSQL + "     职务 = dbo.GetGWMCByRydm(b.人员代码,@separate)," + vbCr
                            strSQL = strSQL + "     级别 = d.级别名称," + vbCr
                            strSQL = strSQL + "     秘书 = e.人员名称," + vbCr
                            strSQL = strSQL + "     b.联系电话,b.手机号码,b.FTP地址,b.邮箱地址" + vbCr
                            strSQL = strSQL + "   from (" + vbCr
                            strSQL = strSQL + "     select 成员名称 " + vbCr
                            strSQL = strSQL + "     from 公文_B_分发范围 " + vbCr
                            strSQL = strSQL + "     where 范围标志 = @fwbz " + vbCr
                            strSQL = strSQL + "     and 范围名称 = @fwmc" + vbCr
                            strSQL = strSQL + "     and 成员类型 = @cylx_gr" + vbCr
                            strSQL = strSQL + "   ) a " + vbCr
                            strSQL = strSQL + "   left join 公共_B_人员     b on a.成员名称 = b.人员名称 " + vbCr
                            strSQL = strSQL + "   left join 公共_B_组织机构 c on b.组织代码 = c.组织代码 " + vbCr
                            strSQL = strSQL + "   left join 公共_B_行政级别 d on b.级别代码 = d.级别代码 " + vbCr
                            strSQL = strSQL + "   left join 公共_B_人员     e on b.秘书代码 = e.人员代码 " + vbCr
                            strSQL = strSQL + "   where b.人员名称 is not null " + vbCr
                            strSQL = strSQL + " ) a" + vbCr
                            If strWhere <> "" Then
                                strSQL = strSQL + " where " + strWhere + vbCr
                            End If
                            strSQL = strSQL + " order by a.类型,a.部门,a.序号,a.名称"

                            '设置参数
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@cylx_gr", Xydc.Platform.Common.Data.FenfafanweiData.CYLX_GEREN)
                            objSqlCommand.Parameters.AddWithValue("@separate", Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate)
                            objSqlCommand.Parameters.AddWithValue("@fwbz", CType(Xydc.Platform.Common.Data.FenfafanweiData.enumFWBZ.CHENGYUAN, Integer).ToString())
                            objSqlCommand.Parameters.AddWithValue("@fwmc", strFWMC)
                            objSqlCommand.Parameters.AddWithValue("@cylx_dw", Xydc.Platform.Common.Data.FenfafanweiData.CYLX_DANWEI)
                            .SelectCommand = objSqlCommand
                        Else
                            '允许选择部门
                            '范围内的部门数据
                            strSQL = ""
                            strSQL = strSQL + " select a.* from (" + vbCr
                            strSQL = strSQL + "   select " + vbCr
                            strSQL = strSQL + "     名称 = a.成员名称," + vbCr
                            strSQL = strSQL + "     类型 = @cylx_dw," + vbCr
                            strSQL = strSQL + "     序号 = @mrxh," + vbCr
                            strSQL = strSQL + "     部门 = a.成员名称," + vbCr
                            strSQL = strSQL + "     职务 = @mrzw," + vbCr
                            strSQL = strSQL + "     级别 = c.级别名称," + vbCr
                            strSQL = strSQL + "     秘书 = d.人员名称," + vbCr
                            strSQL = strSQL + "     b.联系电话,b.手机号码,b.FTP地址,b.邮箱地址" + vbCr
                            strSQL = strSQL + "   from (" + vbCr
                            strSQL = strSQL + "     select 成员名称 " + vbCr
                            strSQL = strSQL + "     from 公文_B_分发范围 " + vbCr
                            strSQL = strSQL + "     where 范围标志 = @fwbz " + vbCr
                            strSQL = strSQL + "     and 范围名称 = @fwmc" + vbCr
                            strSQL = strSQL + "     and 成员类型 = @cylx_dw" + vbCr
                            strSQL = strSQL + "   ) a " + vbCr
                            strSQL = strSQL + "   left join 公共_B_组织机构 b on a.成员名称 = b.组织名称 " + vbCr
                            strSQL = strSQL + "   left join 公共_B_行政级别 c on b.级别代码 = c.级别代码 " + vbCr
                            strSQL = strSQL + "   left join 公共_B_人员     d on b.秘书代码 = d.人员代码 " + vbCr
                            strSQL = strSQL + "   where b.组织名称 is not null " + vbCr

                            strSQL = strSQL + "   union " + vbCr

                            '范围内的人员数据
                            strSQL = strSQL + "   select " + vbCr
                            strSQL = strSQL + "     名称 = a.成员名称," + vbCr
                            strSQL = strSQL + "     类型 = @cylx_gr," + vbCr
                            strSQL = strSQL + "     序号 = b.人员序号," + vbCr
                            strSQL = strSQL + "     部门 = c.组织名称," + vbCr
                            strSQL = strSQL + "     职务 = dbo.GetGWMCByRydm(b.人员代码,@separate)," + vbCr
                            strSQL = strSQL + "     级别 = d.级别名称," + vbCr
                            strSQL = strSQL + "     秘书 = e.人员名称," + vbCr
                            strSQL = strSQL + "     b.联系电话,b.手机号码,b.FTP地址,b.邮箱地址" + vbCr
                            strSQL = strSQL + "   from (" + vbCr
                            strSQL = strSQL + "     select 成员名称 " + vbCr
                            strSQL = strSQL + "     from 公文_B_分发范围 " + vbCr
                            strSQL = strSQL + "     where 范围标志 = @fwbz " + vbCr
                            strSQL = strSQL + "     and 范围名称 = @fwmc" + vbCr
                            strSQL = strSQL + "     and 成员类型 = @cylx_gr" + vbCr
                            strSQL = strSQL + "   ) a " + vbCr
                            strSQL = strSQL + "   left join 公共_B_人员     b on a.成员名称 = b.人员名称 " + vbCr
                            strSQL = strSQL + "   left join 公共_B_组织机构 c on b.组织代码 = c.组织代码 " + vbCr
                            strSQL = strSQL + "   left join 公共_B_行政级别 d on b.级别代码 = d.级别代码 " + vbCr
                            strSQL = strSQL + "   left join 公共_B_人员     e on b.秘书代码 = e.人员代码 " + vbCr
                            strSQL = strSQL + "   where b.人员名称 is not null "
                            strSQL = strSQL + " ) a" + vbCr
                            If strWhere <> "" Then
                                strSQL = strSQL + " where " + strWhere + vbCr
                            End If
                            strSQL = strSQL + " order by a.类型,a.部门,a.序号,a.名称"

                            '设置参数
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@cylx_dw", Xydc.Platform.Common.Data.FenfafanweiData.CYLX_DANWEI)
                            objSqlCommand.Parameters.AddWithValue("@mrxh", "1")
                            objSqlCommand.Parameters.AddWithValue("@mrzw", " ")
                            objSqlCommand.Parameters.AddWithValue("@fwbz", CType(Xydc.Platform.Common.Data.FenfafanweiData.enumFWBZ.CHENGYUAN, Integer).ToString())
                            objSqlCommand.Parameters.AddWithValue("@fwmc", strFWMC)
                            objSqlCommand.Parameters.AddWithValue("@cylx_gr", Xydc.Platform.Common.Data.FenfafanweiData.CYLX_GEREN)
                            objSqlCommand.Parameters.AddWithValue("@separate", Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate)
                            .SelectCommand = objSqlCommand
                        End If

                        '执行操作
                        .Fill(objTempSelectRenyuanData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempSelectRenyuanData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempSelectRenyuanData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objSelectRenyuanData = objTempSelectRenyuanData
            getRenyuanOrBumenInFanweiData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempSelectRenyuanData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据指定范围名称获取范围下的组织信息
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strFWMC              ：指定范围名称
        '     strWhere             ：搜索条件(默认表前缀a.)
        '     objSelectBumenData   ：指定组织机构下的人员信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getBumenInFanweiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strFWMC As String, _
            ByVal strWhere As String, _
            ByRef objSelectBumenData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Dim objTempSelectBumenData As Xydc.Platform.Common.Data.CustomerData
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getBumenInFanweiData = False
            objSelectBumenData = Nothing
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strFWMC.Length > 0 Then strFWMC = strFWMC.Trim()
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
                    objTempSelectBumenData = New Xydc.Platform.Common.Data.CustomerData(Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_ZUZHIJIGOU_SELECT)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        '范围内的部门数据
                        strSQL = ""
                        strSQL = strSQL + " select a.* from (" + vbCr

                        strSQL = strSQL + "   select " + vbCr
                        strSQL = strSQL + "     单位名称 = a.成员名称," + vbCr
                        strSQL = strSQL + "     选择类型 = @cylx_dw," + vbCr
                        strSQL = strSQL + "     单位全称 = b.组织别名," + vbCr
                        strSQL = strSQL + "     单位级别 = c.级别名称," + vbCr
                        strSQL = strSQL + "     单位秘书 = d.人员名称," + vbCr
                        strSQL = strSQL + "     b.联系电话,b.手机号码,b.FTP地址,b.邮箱地址" + vbCr
                        strSQL = strSQL + "   from (" + vbCr
                        strSQL = strSQL + "     select 成员名称 " + vbCr
                        strSQL = strSQL + "     from 公文_B_分发范围 " + vbCr
                        strSQL = strSQL + "     where 范围标志 = @fwbz " + vbCr
                        strSQL = strSQL + "     and 范围名称 = @fwmc" + vbCr
                        strSQL = strSQL + "     and 成员类型 = @cylx_dw" + vbCr
                        strSQL = strSQL + "   ) a " + vbCr
                        strSQL = strSQL + "   left join 公共_B_组织机构 b on a.成员名称 = b.组织名称 " + vbCr
                        strSQL = strSQL + "   left join 公共_B_行政级别 c on b.级别代码 = c.级别代码 " + vbCr
                        strSQL = strSQL + "   left join 公共_B_人员     d on b.秘书代码 = d.人员代码 " + vbCr
                        strSQL = strSQL + "   where b.组织名称 is not null " + vbCr

                        strSQL = strSQL + " ) a" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.选择类型,a.单位级别,a.单位名称"

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@cylx_dw", Xydc.Platform.Common.Data.FenfafanweiData.CYLX_DANWEI)
                        objSqlCommand.Parameters.AddWithValue("@fwbz", CType(Xydc.Platform.Common.Data.FenfafanweiData.enumFWBZ.CHENGYUAN, Integer).ToString())
                        objSqlCommand.Parameters.AddWithValue("@fwmc", strFWMC)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempSelectBumenData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU_SELECT))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempSelectBumenData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempSelectBumenData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objSelectBumenData = objTempSelectBumenData
            getBumenInFanweiData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempSelectBumenData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取strBLR、strWTR能直接发送的人员代码列表的SQL语句
        '     strBLR               ：当前办理人的名称
        '     strWTRArray          ：strBLR受strWTR委托进行处理
        ' 返回
        '                          ：SQL语句
        '----------------------------------------------------------------
        Public Function getSendRestrictWhere( _
            ByVal strBLR As String, _
            ByVal strWTRArray As String()) As String

            Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim strErrMsg As String = ""
            Dim strSQL As String = ""

            Try
                '初始化
                If strBLR Is Nothing Then strBLR = ""
                If strBLR.Length > 0 Then strBLR = strBLR.Trim()
                If strBLR = "" Then Exit Function

                '获取委托人列表
                Dim strWTR As String
                If objdacCommon.doConvertToSQLValueList(strErrMsg, strWTRArray, strWTR) = False Then
                    strWTR = ""
                End If

                strSQL = strSQL + "   select a.人员代码 from " + vbCr
                strSQL = strSQL + "   (" + vbCr

                'strBLR、strWTR是否能够发送到限制条件为单位的人员
                strSQL = strSQL + "     select " + vbCr
                strSQL = strSQL + "       a.人员代码" + vbCr
                strSQL = strSQL + "     from" + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select " + vbCr
                strSQL = strSQL + "         a.*," + vbCr
                strSQL = strSQL + "         允许发送组织代码 = b.组织代码" + vbCr
                strSQL = strSQL + "       from" + vbCr
                strSQL = strSQL + "       (" + vbCr
                strSQL = strSQL + "         select " + vbCr
                strSQL = strSQL + "           人员代码, 人员名称, 可直送人员" + vbCr
                strSQL = strSQL + "         from 公共_B_人员" + vbCr
                strSQL = strSQL + "         where rtrim(isnull(可直送人员,'')) <> ''" + vbCr
                strSQL = strSQL + "       ) a " + vbCr
                strSQL = strSQL + "       left join 公共_B_组织机构 b on rtrim(a.可直送人员) + '" + strSep + "' like '%' + rtrim(b.组织名称) + '" + strSep + "%'" + vbCr
                strSQL = strSQL + "       where b.组织代码 Is Not null" + vbCr
                strSQL = strSQL + "     ) a " + vbCr
                strSQL = strSQL + "     left join" + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select " + vbCr
                strSQL = strSQL + "         组织代码" + vbCr
                strSQL = strSQL + "       from 公共_B_人员" + vbCr
                strSQL = strSQL + "       where 人员名称 = '" + strBLR + "'" + vbCr
                If strWTR <> "" Then
                    strSQL = strSQL + "       or 人员名称 in (" + strWTR + ")" + vbCr
                End If
                strSQL = strSQL + "     ) b on  b.组织代码 like rtrim(a.允许发送组织代码) + '%'" + vbCr      '允许发送组织代码的本级单位和下级单位的人均可
                strSQL = strSQL + "     where b.组织代码 Is Not null " + vbCr

                strSQL = strSQL + "     union" + vbCr

                'strBLR、strWTR是否能够发送到限制条件为人员的人员
                strSQL = strSQL + "     select " + vbCr
                strSQL = strSQL + "       a.人员代码" + vbCr
                strSQL = strSQL + "     from" + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select " + vbCr
                strSQL = strSQL + "         a.*," + vbCr
                strSQL = strSQL + "         允许发送人员代码 = b.人员代码" + vbCr
                strSQL = strSQL + "       from" + vbCr
                strSQL = strSQL + "       (" + vbCr
                strSQL = strSQL + "         select " + vbCr
                strSQL = strSQL + "           人员代码, 人员名称, 可直送人员" + vbCr
                strSQL = strSQL + "         from 公共_B_人员" + vbCr
                strSQL = strSQL + "         where rtrim(isnull(可直送人员,'')) <> ''" + vbCr
                strSQL = strSQL + "       ) a " + vbCr
                strSQL = strSQL + "       left join 公共_B_人员 b on rtrim(a.可直送人员) + '" + strSep + "' like + '%' + rtrim(b.人员名称) + '" + strSep + "%'" + vbCr
                strSQL = strSQL + "       where b.人员代码 Is Not null " + vbCr
                strSQL = strSQL + "     ) a " + vbCr
                strSQL = strSQL + "     left join" + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select " + vbCr
                strSQL = strSQL + "         人员代码" + vbCr
                strSQL = strSQL + "       from 公共_B_人员" + vbCr
                strSQL = strSQL + "       where 人员名称 = '" + strBLR + "'" + vbCr
                If strWTR <> "" Then
                    strSQL = strSQL + "       or 人员名称 in (" + strWTR + ")" + vbCr
                End If
                strSQL = strSQL + "     ) b on a.允许发送人员代码 = b.人员代码 " + vbCr
                strSQL = strSQL + "     where b.人员代码 Is Not null" + vbCr

                strSQL = strSQL + "    union" + vbCr

                '没有定义发送限制条件的人员
                strSQL = strSQL + "     select " + vbCr
                strSQL = strSQL + "       人员代码" + vbCr
                strSQL = strSQL + "     from 公共_B_人员" + vbCr
                strSQL = strSQL + "     where rtrim(isnull(可直送人员,'')) = ''" + vbCr

                strSQL = strSQL + "   ) a" + vbCr
                strSQL = strSQL + "   group by a.人员代码" + vbCr

            Catch ex As Exception
                strSQL = ""
            End Try

            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            getSendRestrictWhere = strSQL
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取strBLR、strWTR能直接发送的人员代码列表的SQL语句
        '     strBLR               ：当前办理人的名称
        '     strWTR               ：strBLR受strWTR委托进行处理
        ' 返回
        '                          ：SQL语句
        '----------------------------------------------------------------
        Public Function getSendRestrictWhere( _
            ByVal strBLR As String, _
            ByVal strWTR As String) As String

            Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate
            Dim strSQL As String = ""

            Try
                '初始化
                If strBLR Is Nothing Then strBLR = ""
                If strBLR.Length > 0 Then strBLR = strBLR.Trim()
                If strWTR Is Nothing Then strWTR = ""
                If strWTR.Length > 0 Then strWTR = strWTR.Trim()
                If strBLR = "" Then Exit Function

                strSQL = strSQL + "   select a.人员代码 from " + vbCr
                strSQL = strSQL + "   (" + vbCr

                'strBLR、strWTR是否能够发送到限制条件为单位的人员
                strSQL = strSQL + "     select " + vbCr
                strSQL = strSQL + "       a.人员代码" + vbCr
                strSQL = strSQL + "     from" + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select " + vbCr
                strSQL = strSQL + "         a.*," + vbCr
                strSQL = strSQL + "         允许发送组织代码 = b.组织代码" + vbCr
                strSQL = strSQL + "       from" + vbCr
                strSQL = strSQL + "       (" + vbCr
                strSQL = strSQL + "         select " + vbCr
                strSQL = strSQL + "           人员代码, 人员名称, 可直送人员" + vbCr
                strSQL = strSQL + "         from 公共_B_人员" + vbCr
                strSQL = strSQL + "         where rtrim(isnull(可直送人员,'')) <> ''" + vbCr
                strSQL = strSQL + "       ) a " + vbCr
                strSQL = strSQL + "       left join 公共_B_组织机构 b on rtrim(a.可直送人员) + '" + strSep + "' like '%' + rtrim(b.组织名称) + '" + strSep + "%'" + vbCr
                strSQL = strSQL + "       where b.组织代码 Is Not null" + vbCr
                strSQL = strSQL + "     ) a " + vbCr
                strSQL = strSQL + "     left join" + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select " + vbCr
                strSQL = strSQL + "         组织代码" + vbCr
                strSQL = strSQL + "       from 公共_B_人员" + vbCr
                strSQL = strSQL + "       where 人员名称 = '" + strBLR + "'" + vbCr
                If strWTR <> "" Then
                    strSQL = strSQL + "       or 人员名称 = '" + strWTR + "'" + vbCr
                End If
                strSQL = strSQL + "     ) b on b.组织代码 like rtrim(a.允许发送组织代码) + '%'" + vbCr  '允许发送组织代码的本级单位和下级单位的人均可
                strSQL = strSQL + "     where b.组织代码 Is Not null " + vbCr

                strSQL = strSQL + "     union" + vbCr

                'strBLR、strWTR是否能够发送到限制条件为人员的人员
                strSQL = strSQL + "     select " + vbCr
                strSQL = strSQL + "       a.人员代码" + vbCr
                strSQL = strSQL + "     from" + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select " + vbCr
                strSQL = strSQL + "         a.*," + vbCr
                strSQL = strSQL + "         允许发送人员代码 = b.人员代码" + vbCr
                strSQL = strSQL + "       from" + vbCr
                strSQL = strSQL + "       (" + vbCr
                strSQL = strSQL + "         select " + vbCr
                strSQL = strSQL + "           人员代码, 人员名称, 可直送人员" + vbCr
                strSQL = strSQL + "         from 公共_B_人员" + vbCr
                strSQL = strSQL + "         where rtrim(isnull(可直送人员,'')) <> ''" + vbCr
                strSQL = strSQL + "       ) a " + vbCr
                strSQL = strSQL + "       left join 公共_B_人员 b on rtrim(a.可直送人员) + '" + strSep + "' like + '%' + rtrim(b.人员名称) + '" + strSep + "%'" + vbCr
                strSQL = strSQL + "       where b.人员代码 Is Not null " + vbCr
                strSQL = strSQL + "     ) a " + vbCr
                strSQL = strSQL + "     left join" + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select " + vbCr
                strSQL = strSQL + "         人员代码" + vbCr
                strSQL = strSQL + "       from 公共_B_人员" + vbCr
                strSQL = strSQL + "       where 人员名称 = '" + strBLR + "'" + vbCr
                If strWTR <> "" Then
                    strSQL = strSQL + "       or 人员名称 = '" + strWTR + "'" + vbCr
                End If
                strSQL = strSQL + "     ) b on a.允许发送人员代码 = b.人员代码 " + vbCr
                strSQL = strSQL + "     where b.人员代码 Is Not null" + vbCr

                strSQL = strSQL + "    union" + vbCr

                '没有定义发送限制条件的人员
                strSQL = strSQL + "     select " + vbCr
                strSQL = strSQL + "       人员代码" + vbCr
                strSQL = strSQL + "     from 公共_B_人员" + vbCr
                strSQL = strSQL + "     where rtrim(isnull(可直送人员,'')) = ''" + vbCr

                strSQL = strSQL + "   ) a" + vbCr
                strSQL = strSQL + "   group by a.人员代码" + vbCr

            Catch ex As Exception
                strSQL = ""
            End Try

            getSendRestrictWhere = strSQL
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取strBLR、strWTR能直接发送的人员代码列表的SQL语句
        '     strBLR               ：当前办理人的名称
        '     strWTR               ：strBLR受strWTR委托进行处理
        '     blnByRYDM            ：指定的是人员代码
        ' 返回
        '                          ：SQL语句
        '----------------------------------------------------------------
        Public Function getSendRestrictWhere( _
            ByVal strBLR As String, _
            ByVal strWTR As String, _
            ByVal blnByRYDM As Boolean) As String

            Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate
            Dim strSQL As String = ""

            Try
                '初始化
                If strBLR Is Nothing Then strBLR = ""
                If strBLR.Length > 0 Then strBLR = strBLR.Trim()
                If strWTR Is Nothing Then strWTR = ""
                If strWTR.Length > 0 Then strWTR = strWTR.Trim()
                If strBLR = "" Then Exit Function

                strSQL = strSQL + "   select a.人员代码 from " + vbCr
                strSQL = strSQL + "   (" + vbCr

                'strBLR、strWTR是否能够发送到限制条件为单位的人员
                strSQL = strSQL + "     select " + vbCr
                strSQL = strSQL + "       a.人员代码" + vbCr
                strSQL = strSQL + "     from" + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select " + vbCr
                strSQL = strSQL + "         a.*," + vbCr
                strSQL = strSQL + "         允许发送组织代码 = b.组织代码" + vbCr
                strSQL = strSQL + "       from" + vbCr
                strSQL = strSQL + "       (" + vbCr
                strSQL = strSQL + "         select " + vbCr
                strSQL = strSQL + "           人员代码, 人员名称, 可直送人员" + vbCr
                strSQL = strSQL + "         from 公共_B_人员" + vbCr
                strSQL = strSQL + "         where rtrim(isnull(可直送人员,'')) <> ''" + vbCr
                strSQL = strSQL + "       ) a " + vbCr
                strSQL = strSQL + "       left join 公共_B_组织机构 b on rtrim(a.可直送人员) + '" + strSep + "' like '%' + rtrim(b.组织名称) + '" + strSep + "%'" + vbCr
                strSQL = strSQL + "       where b.组织代码 Is Not null" + vbCr
                strSQL = strSQL + "     ) a " + vbCr
                strSQL = strSQL + "     left join" + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select " + vbCr
                strSQL = strSQL + "         组织代码" + vbCr
                strSQL = strSQL + "       from 公共_B_人员" + vbCr
                strSQL = strSQL + "       where 人员代码 = '" + strBLR + "'" + vbCr
                If strWTR <> "" Then
                    strSQL = strSQL + "       or 人员代码 = '" + strWTR + "'" + vbCr
                End If
                strSQL = strSQL + "     ) b on b.组织代码 like rtrim(a.允许发送组织代码) + '%'" + vbCr   '允许发送组织代码的本级单位和下级单位的人均可
                strSQL = strSQL + "     where b.组织代码 Is Not null " + vbCr

                strSQL = strSQL + "     union" + vbCr

                'strBLR、strWTR是否能够发送到限制条件为人员的人员
                strSQL = strSQL + "     select " + vbCr
                strSQL = strSQL + "       a.人员代码" + vbCr
                strSQL = strSQL + "     from" + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select " + vbCr
                strSQL = strSQL + "         a.*," + vbCr
                strSQL = strSQL + "         允许发送人员代码 = b.人员代码" + vbCr
                strSQL = strSQL + "       from" + vbCr
                strSQL = strSQL + "       (" + vbCr
                strSQL = strSQL + "         select " + vbCr
                strSQL = strSQL + "           人员代码, 人员名称, 可直送人员" + vbCr
                strSQL = strSQL + "         from 公共_B_人员" + vbCr
                strSQL = strSQL + "         where rtrim(isnull(可直送人员,'')) <> ''" + vbCr
                strSQL = strSQL + "       ) a " + vbCr
                strSQL = strSQL + "       left join 公共_B_人员 b on rtrim(a.可直送人员) + '" + strSep + "' like + '%' + rtrim(b.人员名称) + '" + strSep + "%'" + vbCr
                strSQL = strSQL + "       where b.人员代码 Is Not null " + vbCr
                strSQL = strSQL + "     ) a " + vbCr
                strSQL = strSQL + "     left join" + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select " + vbCr
                strSQL = strSQL + "         人员代码" + vbCr
                strSQL = strSQL + "       from 公共_B_人员" + vbCr
                strSQL = strSQL + "       where 人员代码 = '" + strBLR + "'" + vbCr
                If strWTR <> "" Then
                    strSQL = strSQL + "       or 人员代码 = '" + strWTR + "'" + vbCr
                End If
                strSQL = strSQL + "     ) b on a.允许发送人员代码 = b.人员代码 " + vbCr
                strSQL = strSQL + "     where b.人员代码 Is Not null" + vbCr

                strSQL = strSQL + "    union" + vbCr

                '没有定义发送限制条件的人员
                strSQL = strSQL + "     select " + vbCr
                strSQL = strSQL + "       人员代码" + vbCr
                strSQL = strSQL + "     from 公共_B_人员" + vbCr
                strSQL = strSQL + "     where rtrim(isnull(可直送人员,'')) = ''" + vbCr

                strSQL = strSQL + "   ) a" + vbCr
                strSQL = strSQL + "   group by a.人员代码" + vbCr

            Catch ex As Exception
                strSQL = ""
            End Try

            getSendRestrictWhere = strSQL
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据指定上级代码获取下级代码值
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strSJDM              ：上级代码
        '     intFJCDSM            ：代码分级长度
        '     strNewZZDM           ：新代码（返回）
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getNewZZDM( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strSJDM As String, _
            ByVal intFJCDSM() As Integer, _
            ByRef strNewZZDM As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection

            getNewZZDM = False
            strNewZZDM = ""

            Try
                '检查
                If strSJDM Is Nothing Then strSJDM = ""
                strSJDM = strSJDM.Trim()

                '获取数据库连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取本级代码长度
                Dim strNewCode As String
                Dim intCodeLen As Integer
                Dim intLevel As Integer
                If strSJDM = "" Then
                    intLevel = 0
                Else
                    intLevel = objPulicParameters.getCodeLevel(intFJCDSM, strSJDM.Length)
                    If intLevel < 1 Then
                        strErrMsg = "错误：无效的代码[" + strSJDM + "]！"
                        GoTo errProc
                    End If
                    If intLevel >= intFJCDSM.Length Then
                        strErrMsg = "错误：代码[" + strSJDM + "]已经是最后1级！"
                        GoTo errProc
                    End If
                End If
                intCodeLen = intFJCDSM(intLevel)

                '获取新代码
                If objdacCommon.getNewCode(strErrMsg, objSqlConnection, "组织代码", "公共_B_组织机构", intCodeLen, strSJDM, True, strNewCode) = False Then
                    GoTo errProc
                End If

                '返回
                strNewZZDM = strNewCode

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getNewZZDM = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 检查“公共_B_组织机构”的数据的合法性
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
        Public Function doVerifyZuzhijigouData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim objListDictionary As New System.Collections.Specialized.ListDictionary

            doVerifyZuzhijigouData = False

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
                If objNewData Is Nothing Then
                    strErrMsg = "错误：未传入新的数据！"
                    GoTo errProc
                End If
                Dim strOldZZDM As String
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                    Case Else
                        If objOldData Is Nothing Then
                            strErrMsg = "错误：未传入旧的数据！"
                            GoTo errProc
                        End If
                        strOldZZDM = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_ZZDM), "")
                End Select

                '获取表结构定义
                Dim strSQL As String
                strSQL = "select top 0 * from 公共_B_组织机构"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, strUserId, strPassword, strSQL, "公共_B_组织机构", objDataSet) = False Then
                    GoTo errProc
                End If

                '检查数据长度及非空特性
                Dim intCount As Integer
                Dim intLen As Integer
                Dim i As Integer
                Dim strFieldName As String
                Dim strFieldValue As String
                intCount = objNewData.Count
                For i = 0 To intCount - 1 Step 1
                    strFieldName = objNewData.GetKey(i)
                    strFieldValue = objNewData.Item(strFieldName)
                    strFieldValue = strFieldValue.Trim()
                    intLen = objPulicParameters.getStringLength(strFieldValue)
                    With objDataSet.Tables(0).Columns(strFieldName)
                        If intLen > .MaxLength Then
                            strErrMsg = "错误：[" + strFieldName + "]值长度超过[" + .MaxLength.ToString() + "]，实际有[" + intLen.ToString() + "]！"
                            GoTo errProc
                        End If
                    End With
                    Select Case strFieldName
                        Case Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_ZZDM, _
                            Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_ZZMC
                            If strFieldValue = "" Then
                                strErrMsg = "错误：[" + strFieldName + "]没有输入内容！"
                                GoTo errProc
                            End If
                    End Select
                Next
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '检验代码长度合法性、是否全数字
                Dim strZZDM As String
                strZZDM = objNewData(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_ZZDM)
                strZZDM = strZZDM.Trim()
                If objPulicParameters.isNumericString(strZZDM) = False Then
                    strErrMsg = "错误：[" + strZZDM + "]中包括非数字字符！"
                    GoTo errProc
                End If
                If objPulicParameters.doVerifyCodeLength(Xydc.Platform.Common.Data.CustomerData.intZZDM_FJCDSM, strZZDM.Length) = False Then
                    strErrMsg = "错误：[" + strZZDM + "]长度不正确！"
                    GoTo errProc
                End If

                '检查组织代码
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                        strSQL = "select * from 公共_B_组织机构 where 组织代码 = @zzdm"
                        objListDictionary.Add("@zzdm", strZZDM)
                    Case Else
                        strSQL = "select * from 公共_B_组织机构 where 组织代码 = @zzdm and 组织代码 <> @oldzzdm"
                        objListDictionary.Add("@zzdm", strZZDM)
                        objListDictionary.Add("@oldzzdm", strOldZZDM)
                End Select
                If objdacCommon.getDataSetBySQL(strErrMsg, strUserId, strPassword, strSQL, objListDictionary, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strErrMsg = "错误：[" + strZZDM + "]已经存在！"
                    GoTo errProc
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing
                objListDictionary.Clear()

                '检查组织名称
                Dim strZZMC As String
                strZZMC = objNewData(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_ZZMC)
                strZZMC = strZZMC.Trim()
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                        strSQL = "select 实体代码,实体名称 from 公共_V_全部实体名称 where 实体名称 = @zzmc"
                        objListDictionary.Add("@zzmc", strZZMC)
                    Case Else
                        strSQL = ""
                        strSQL = strSQL + " select 组织代码,组织名称 from 公共_B_组织机构     where 组织名称 = @zzmc and 组织代码 <> @oldzzdm" + vbCr
                        strSQL = strSQL + " union" + vbCr
                        strSQL = strSQL + " select 实体代码,实体名称 from 公共_V_全部实体名称 where 实体名称 = @zzmc and 实体代码 <> @oldzzdm" + vbCr
                        objListDictionary.Add("@zzmc", strZZMC)
                        objListDictionary.Add("@oldzzdm", strOldZZDM)
                End Select
                If objdacCommon.getDataSetBySQL(strErrMsg, strUserId, strPassword, strSQL, objListDictionary, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strErrMsg = "错误：[" + strZZMC + "]已经存在！"
                    GoTo errProc
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing
                objListDictionary.Clear()

                '检查上级代码是否存在
                Dim strPrevCode As String
                strPrevCode = objPulicParameters.getPrevLevelCode(Xydc.Platform.Common.Data.CustomerData.intZZDM_FJCDSM, strZZDM)
                If strPrevCode <> "" Then
                    strSQL = "select * from 公共_B_组织机构 where 组织代码 = @zzdm"
                    objListDictionary.Add("@zzdm", strPrevCode)
                    If objdacCommon.getDataSetBySQL(strErrMsg, strUserId, strPassword, strSQL, objListDictionary, objDataSet) = False Then
                        GoTo errProc
                    End If
                    If objDataSet.Tables(0).Rows.Count < 1 Then
                        strErrMsg = "错误：[" + strZZDM + "]上级不存在！"
                        GoTo errProc
                    End If
                    Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                    objDataSet = Nothing
                    objListDictionary.Clear()
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objListDictionary)

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objListDictionary)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            doVerifyZuzhijigouData = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objListDictionary)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 保存“公共_B_组织机构”的数据
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
        Public Function doSaveZuzhijigouData( _
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

            '初始化
            doSaveZuzhijigouData = False
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
                If objNewData Is Nothing Then
                    strErrMsg = "错误：未传入新的数据！"
                    GoTo errProc
                End If
                Dim strOldZZDM As String
                Dim strZZDM As String
                strZZDM = objNewData.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_ZZDM)
                strZZDM = strZZDM.Trim()
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                    Case Else
                        If objOldData Is Nothing Then
                            strErrMsg = "错误：未传入旧的数据！"
                            GoTo errProc
                        End If
                        strOldZZDM = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_ZZDM), "")
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
                            strSQL = strSQL + " insert into 公共_B_组织机构 (" + strFields + ")"
                            strSQL = strSQL + " values (" + strValues + ")"
                            objSqlCommand.Parameters.Clear()
                            For i = 0 To intCount - 1 Step 1
                                strValue = objNewData.Item(i).Trim()
                                If strValue = "" Then strValue = " "
                                objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), strValue)
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
                            strSQL = strSQL + " update 公共_B_组织机构 set "
                            strSQL = strSQL + " " + strFields + " "
                            strSQL = strSQL + " where 组织代码 = @oldzzdm"
                            objSqlCommand.Parameters.Clear()
                            For i = 0 To intCount - 1 Step 1
                                strValue = objNewData.Item(i).Trim()
                                If strValue = "" Then strValue = " "
                                objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), strValue)
                            Next
                            objSqlCommand.Parameters.AddWithValue("@oldzzdm", strOldZZDM)
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()

                            '代码发生变化
                            If strZZDM <> strOldZZDM Then
                                '更改相关的下级单位信息
                                intCount = strOldZZDM.Length
                                strSQL = ""
                                strSQL = strSQL + " update 公共_B_组织机构 set "
                                strSQL = strSQL + "   组织代码 = @newzzdm + substring(组织代码," + (intCount + 1).ToString() + ",len(rtrim(ltrim(组织代码)))-" + intCount.ToString() + ") "
                                strSQL = strSQL + " where rtrim(组织代码) like @oldzzdm + '%' "
                                strSQL = strSQL + " and   组织代码 <> @oldzzdm"
                                objSqlCommand.Parameters.Clear()
                                objSqlCommand.Parameters.AddWithValue("@newzzdm", strZZDM)
                                objSqlCommand.Parameters.AddWithValue("@oldzzdm", strOldZZDM)
                                objSqlCommand.CommandText = strSQL
                                objSqlCommand.ExecuteNonQuery()

                                '更改相关的人员信息
                                strSQL = ""
                                strSQL = strSQL + " update 公共_B_人员 set "
                                strSQL = strSQL + "   组织代码 = @newzzdm + substring(组织代码," + (intCount + 1).ToString() + ",len(rtrim(ltrim(组织代码)))-" + intCount.ToString() + ") "
                                strSQL = strSQL + " where rtrim(组织代码) like @oldzzdm + '%' "
                                objSqlCommand.Parameters.Clear()
                                objSqlCommand.Parameters.AddWithValue("@newzzdm", strZZDM)
                                objSqlCommand.Parameters.AddWithValue("@oldzzdm", strOldZZDM)
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
            doSaveZuzhijigouData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 删除“公共_B_组织机构”的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteZuzhijigouData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            doDeleteZuzhijigouData = False
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
                    Dim strOldZZDM As String
                    With New Xydc.Platform.Common.Utilities.PulicParameters
                        strOldZZDM = .getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_ZZDM), "")
                    End With

                    '删除组织机构下的人员的上岗信息
                    strSQL = ""
                    strSQL = strSQL + " delete 公共_B_上岗 "
                    strSQL = strSQL + " from 公共_B_上岗 a "
                    strSQL = strSQL + " left join 公共_B_人员 b on a.人员代码 = b.人员代码 "
                    strSQL = strSQL + " where rtrim(b.组织代码) like @oldzzdm + '%'"
                    strSQL = strSQL + " and   b.人员代码 is not null"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@oldzzdm", strOldZZDM)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '删除组织机构下的人员信息
                    strSQL = ""
                    strSQL = strSQL + " delete from 公共_B_人员 "
                    strSQL = strSQL + " where rtrim(组织代码) like @oldzzdm + '%'"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@oldzzdm", strOldZZDM)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '删除组织机构信息
                    strSQL = ""
                    strSQL = strSQL + " delete from 公共_B_组织机构 "
                    strSQL = strSQL + " where rtrim(组织代码) like @oldzzdm + '%'"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@oldzzdm", strOldZZDM)
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
            doDeleteZuzhijigouData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取新的人员序号
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strZZDM              ：给定组织代码
        '     strNewRYXH           ：新人员序号(返回)
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getNewRYXH( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZZDM As String, _
            ByRef strNewRYXH As String) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            getNewRYXH = False
            strNewRYXH = ""

            Try
                '检查
                If strZZDM Is Nothing Then strZZDM = ""
                strZZDM = strZZDM.Trim()

                '获取数据库连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取新代码
                Dim strNewCode As String
                If objdacCommon.getNewCode(strErrMsg, objSqlConnection, "人员序号", "组织代码", strZZDM, "公共_B_人员", True, strNewCode) = False Then
                    GoTo errProc
                End If

                '返回
                strNewRYXH = strNewCode

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getNewRYXH = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 检查“公共_B_人员”的数据的合法性
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
        Public Function doVerifyRenyuanData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByRef objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim objListDictionary As New System.Collections.Specialized.ListDictionary

            doVerifyRenyuanData = False

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
                If objNewData Is Nothing Then
                    strErrMsg = "错误：未传入新的数据！"
                    GoTo errProc
                End If
                Dim strOldRYDM As String
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                    Case Else
                        If objOldData Is Nothing Then
                            strErrMsg = "错误：未传入旧的数据！"
                            GoTo errProc
                        End If
                        strOldRYDM = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYDM), "")
                End Select

                '获取表结构定义
                Dim strSQL As String
                strSQL = "select top 0 * from 公共_B_人员"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, strUserId, strPassword, strSQL, "公共_B_人员", objDataSet) = False Then
                    GoTo errProc
                End If

                '检查数据长度及非空特性
                Dim intCount As Integer
                Dim intLen As Integer
                Dim i As Integer
                Dim strFieldName As String
                Dim strFieldValue As String
                intCount = objNewData.Count
                For i = 0 To intCount - 1 Step 1
                    strFieldName = objNewData.GetKey(i)
                    strFieldValue = objNewData.Item(strFieldName)
                    strFieldValue = strFieldValue.Trim()
                    intLen = objPulicParameters.getStringLength(strFieldValue)
                    Select Case strFieldName
                        Case Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SFJM
                            If objPulicParameters.isIntegerString(strFieldValue) = False Then
                                strErrMsg = "错误：[" + strFieldValue + "]中包括非数字字符！"
                                GoTo errProc
                            End If
                            Dim intTemp As Integer
                            intTemp = objPulicParameters.getObjectValue(strFieldValue, 0)
                            If intTemp <> 0 And intTemp <> 1 Then
                                strErrMsg = "错误：[" + strFieldName + "]只能是0或1！"
                                GoTo errProc
                            End If
                        Case Else
                            With objDataSet.Tables(0).Columns(strFieldName)
                                If intLen > .MaxLength Then
                                    strErrMsg = "错误：[" + strFieldName + "]值长度超过[" + .MaxLength.ToString() + "]，实际有[" + intLen.ToString() + "]！"
                                    GoTo errProc
                                End If
                            End With
                    End Select

                    'Select Case strFieldName
                    '    Case Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYDM, _
                    '        Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYMC, _
                    '        Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYXH, _
                    '        Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_ZZDM
                    Select Case strFieldName
                        Case Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYDM, _
                        Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYMC, _
                        Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYZM, _
                        Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYXH, _
                        Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_ZZDM
                            If strFieldValue = "" Then
                                strErrMsg = "错误：[" + strFieldName + "]没有输入内容！"
                                GoTo errProc
                            End If
                    End Select

                Next
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '检验是否全数字
                Dim strRYXH As String
                strRYXH = objNewData(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYXH)
                strRYXH = strRYXH.Trim()
                If objPulicParameters.isIntegerString(strRYXH) = False Then
                    strErrMsg = "错误：[" + strRYXH + "]中包括非数字字符！"
                    GoTo errProc
                End If
                strRYXH = CType(strRYXH, Integer).ToString
                objNewData(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYXH) = strRYXH

                '检查人员代码
                Dim strRYDM As String
                strRYDM = objNewData(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYDM)
                strRYDM = strRYDM.Trim()
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew

                        'strSQL = "select * from 公共_B_人员 where 人员代码 = @rydm"
                        'objListDictionary.Add("@rydm", strRYDM)

                    Case Else
                        strSQL = "select * from 公共_B_人员 where 人员代码 = @rydm and 人员代码 <> @oldrydm"
                        objListDictionary.Add("@rydm", strRYDM)
                        objListDictionary.Add("@oldrydm", strOldRYDM)
                End Select
                If objdacCommon.getDataSetBySQL(strErrMsg, strUserId, strPassword, strSQL, objListDictionary, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strErrMsg = "错误：[" + strRYDM + "]已经存在！"
                    GoTo errProc
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing
                objListDictionary.Clear()

                '检查人员名称
                Dim strRYMC As String
                strRYMC = objNewData(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYMC)
                strRYMC = strRYMC.Trim()
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew

                        'strSQL = "select 实体代码,实体名称 from 公共_V_全部实体名称 where 实体名称 = @rymc"
                        'objListDictionary.Add("@rymc", strRYMC)

                    Case Else
                        strSQL = ""
                        strSQL = strSQL + " select 人员代码,人员名称 from 公共_B_人员         where 人员名称 = @rymc and 人员代码 <> @oldrydm" + vbCr
                        strSQL = strSQL + " union" + vbCr
                        strSQL = strSQL + " select 实体代码,实体名称 from 公共_V_全部实体名称 where 实体名称 = @rymc and 实体代码 <> @oldrydm" + vbCr
                        objListDictionary.Add("@rymc", strRYMC)
                        objListDictionary.Add("@oldrydm", strOldRYDM)
                End Select
                If objdacCommon.getDataSetBySQL(strErrMsg, strUserId, strPassword, strSQL, objListDictionary, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strErrMsg = "错误：[" + strRYMC + "]已经存在！"
                    GoTo errProc
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing
                objListDictionary.Clear()

                '检查组织代码+人员序号
                Dim strZZDM As String
                strZZDM = objNewData(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_ZZDM)
                strZZDM = strZZDM.Trim()
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                        strSQL = "select * from 公共_B_人员 where 组织代码 = @zzdm and 人员序号 = @ryxh"
                        objListDictionary.Add("@zzdm", strZZDM)
                        objListDictionary.Add("@ryxh", strRYXH)
                    Case Else
                        strSQL = "select * from 公共_B_人员 where 组织代码 = @zzdm and 人员序号 = @ryxh and 人员代码 <> @oldrydm"
                        objListDictionary.Add("@zzdm", strZZDM)
                        objListDictionary.Add("@ryxh", strRYXH)
                        objListDictionary.Add("@oldrydm", strOldRYDM)
                End Select
                If objdacCommon.getDataSetBySQL(strErrMsg, strUserId, strPassword, strSQL, objListDictionary, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strErrMsg = "错误：[" + strZZDM + "]+[" + strRYXH + "]已经存在！"
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

            doVerifyRenyuanData = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objListDictionary)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 检查“公共_B_人员”的标识是否已存在
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strNewUserId         ：检查的用户标识
        '     strNewUserZZDM       ：检查的用户组织代码
        ' 返回
        '     intType              ：0-不存在，1-同部门添加，2-不同部门添加
        '     objCustomerData      ：如果存在，就返回存在的纪录集
        '     True                 ：合法
        '     False                ：不合法或其他程序错误

        '----------------------------------------------------------------
        Public Function doVerifyRenyuanData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strNewUserId As String, _
            ByVal strNewUserZZDM As String, _
            ByRef intType As Integer, _
            ByRef objCustomerData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objTempCustomerData As Xydc.Platform.Common.Data.CustomerData
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objListDictionary As New System.Collections.Specialized.ListDictionary
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objDataTable As System.Data.DataTable
            Dim objDataSet As System.Data.DataSet


            doVerifyRenyuanData = False

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

                If strNewUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要检查信息的用户！"
                    GoTo errProc
                End If

                '获取表结构定义
                Dim strSQL As String = ""
                Dim strZZDM As String = ""
                Dim strZZMC As String = ""

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '创建数据集
                objTempCustomerData = New Xydc.Platform.Common.Data.CustomerData(Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_RENYUAN_FULLJOIN)

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '执行检索
                With Me.m_objSqlDataAdapter
                    '准备SQL
                    strSQL = ""
                    strSQL = "select  * from 公共_B_人员 where 人员代码='" + strNewUserId + "'"

                    '设置参数
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    .Fill(objTempCustomerData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN))


                    If objTempCustomerData.Tables(0).Rows.Count > 0 Then
                        strZZDM = objPulicParameters.getObjectValue(objTempCustomerData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN).Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_ZZDM), "")
                        Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)

                        If strNewUserZZDM = strZZDM Then
                            strErrMsg = "提示：[" + strNewUserId + "]已经在这个部门,请重新确认部门后再保存！"
                            intType = 1
                        Else
                            strSQL = ""
                            strSQL = "select  * from 公共_B_组织机构 where 组织代码='" + strZZDM + "'"
                            If objdacCommon.getDataSetBySQL(strErrMsg, strUserId, strPassword, strSQL, objDataSet) = False Then
                                GoTo errProc
                            End If
                            strZZMC = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item(1), "")
                            intType = 2
                            strErrMsg = "提示：[" + strNewUserId + "]已经在其他部门！是否在新部门任职此人？"
                        End If
                    Else
                        '准备SQL
                        'objTempCustomerData = Nothing
                        strSQL = ""
                        strSQL = "select  * from 公共_B_人员_兼任 where 人员代码='" + strNewUserId + "'"

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        .Fill(objTempCustomerData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN))
                        If objTempCustomerData.Tables(0).Rows.Count > 0 Then
                            intType = 3
                            strErrMsg = "提示：[" + strNewUserId + "]已经在其他部门！是否在新部门任职此人？"
                        End If
                    End If
                End With

                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataTable)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            objCustomerData = objTempCustomerData
            doVerifyRenyuanData = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataTable)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 保存“公共_B_人员”的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
        '     objNewData           ：新数据
        '     objenumEditType      ：编辑类型
        '     objNewDataSG         ：上岗数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doSaveRenyuanData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType, _
            ByVal objNewDataSG As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            doSaveRenyuanData = False
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
                If objNewData Is Nothing Then
                    strErrMsg = "错误：未传入新的数据！"
                    GoTo errProc
                End If
                Dim strOldRYDM As String
                Dim strRYDM As String
                strRYDM = objNewData.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYDM)
                strRYDM = strRYDM.Trim()
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                    Case Else
                        If objOldData Is Nothing Then
                            strErrMsg = "错误：未传入旧的数据！"
                            GoTo errProc
                        End If
                        strOldRYDM = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYDM), "")
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

                    '保存公共_B_上岗
                    '清除数据1
                    Select Case objenumEditType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                        Case Else
                            strSQL = ""
                            strSQL = strSQL + " delete from 公共_B_上岗 "
                            strSQL = strSQL + " where 人员代码 = @oldrydm"
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@oldrydm", strOldRYDM)
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()
                    End Select
                    '清除数据2
                    strSQL = ""
                    strSQL = strSQL + " delete from 公共_B_上岗 "
                    strSQL = strSQL + " where 人员代码 = @newrydm"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@newrydm", strRYDM)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()
                    '保存数据
                    If Not (objNewDataSG Is Nothing) Then
                        With objNewDataSG.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_SHANGGANG)
                            intCount = .Rows.Count
                            For i = 0 To intCount - 1 Step 1
                                strValue = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_SHANGGANG_GWDM), " ")
                                strSQL = ""
                                strSQL = strSQL + " insert into 公共_B_上岗 (人员代码,岗位代码) values (@rydm,@gwdm)"
                                objSqlCommand.Parameters.Clear()
                                objSqlCommand.Parameters.AddWithValue("@rydm", strRYDM)
                                objSqlCommand.Parameters.AddWithValue("@gwdm", strValue)
                                objSqlCommand.CommandText = strSQL
                                objSqlCommand.ExecuteNonQuery()
                            Next
                        End With
                    End If

                    '保存公共_B_人员
                    intCount = objNewData.Count
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
                            strSQL = strSQL + " insert into 公共_B_人员 (" + strFields + ")"
                            strSQL = strSQL + " values (" + strValues + ")"
                            objSqlCommand.Parameters.Clear()
                            For i = 0 To intCount - 1 Step 1
                                strValue = objNewData.Item(i).Trim()
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SFJM
                                        If strValue = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), 0)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(strValue, Integer))
                                        End If
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
                            strSQL = strSQL + " update 公共_B_人员 set "
                            strSQL = strSQL + " " + strFields + " "
                            strSQL = strSQL + " where 人员代码 = @oldrydm"
                            objSqlCommand.Parameters.Clear()
                            For i = 0 To intCount - 1 Step 1
                                strValue = objNewData.Item(i).Trim()
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SFJM
                                        If strValue = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), 0)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(strValue, Integer))
                                        End If
                                    Case Else
                                        If strValue = "" Then strValue = " "
                                        objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), strValue)
                                End Select
                            Next
                            objSqlCommand.Parameters.AddWithValue("@oldrydm", strOldRYDM)
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()

                            '如果人员代码变化
                            If strRYDM <> strOldRYDM Then
                                strSQL = ""
                                strSQL = strSQL + " update 公共_B_上岗 set "
                                strSQL = strSQL + "   人员代码 = @newrydm "
                                strSQL = strSQL + " where 人员代码 = @oldrydm"
                                objSqlCommand.Parameters.Clear()
                                objSqlCommand.Parameters.AddWithValue("@newrydm", strRYDM)
                                objSqlCommand.Parameters.AddWithValue("@oldrydm", strOldRYDM)
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
            doSaveRenyuanData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 保存“公共_B_人员_兼任”的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
        '     objNewData           ：新数据
        '     objUpdateData        ：更新“公共_B_人员”数据 
        '     objenumEditType      ：编辑类型
        '     objNewDataSG         ：上岗数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败

        '----------------------------------------------------------------
        Public Function doSaveRenyuanData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objUpdateData As System.Collections.Specialized.NameValueCollection, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType, _
            ByVal objNewDataSG As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            doSaveRenyuanData = False
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
                If objNewData Is Nothing Then
                    strErrMsg = "错误：未传入新的数据！"
                    GoTo errProc
                End If
                Dim strOldRYDM As String
                Dim strOldZZDM As String
                Dim strRYDM As String
                strRYDM = objNewData.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYDM)
                strRYDM = strRYDM.Trim()
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                        strOldRYDM = objPulicParameters.getObjectValue(objUpdateData.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYDM), "")
                    Case Else
                        If objOldData Is Nothing Then
                            strErrMsg = "错误：未传入旧的数据！"
                            GoTo errProc
                        End If
                        strOldRYDM = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYDM), "")
                        strOldZZDM = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_ZZDM), "")
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

                    '保存公共_B_上岗
                    '清除数据1
                    Select Case objenumEditType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                        Case Else
                            strSQL = ""
                            strSQL = strSQL + " delete from 公共_B_上岗 "
                            strSQL = strSQL + " where 人员代码 = @oldrydm"
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@oldrydm", strOldRYDM)
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()
                    End Select
                    '清除数据2
                    strSQL = ""
                    strSQL = strSQL + " delete from 公共_B_上岗 "
                    strSQL = strSQL + " where 人员代码 = @newrydm"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@newrydm", strRYDM)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()
                    '保存数据
                    If Not (objNewDataSG Is Nothing) Then
                        With objNewDataSG.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_SHANGGANG)
                            intCount = .Rows.Count
                            For i = 0 To intCount - 1 Step 1
                                strValue = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_SHANGGANG_GWDM), " ")
                                strSQL = ""
                                strSQL = strSQL + " insert into 公共_B_上岗 (人员代码,岗位代码) values (@rydm,@gwdm)"
                                objSqlCommand.Parameters.Clear()
                                objSqlCommand.Parameters.AddWithValue("@rydm", strRYDM)
                                objSqlCommand.Parameters.AddWithValue("@gwdm", strValue)
                                objSqlCommand.CommandText = strSQL
                                objSqlCommand.ExecuteNonQuery()
                            Next
                        End With
                    End If

                    '保存公共_B_人员_兼任
                    intCount = objNewData.Count
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
                            strSQL = strSQL + " insert into 公共_B_人员_兼任 (" + strFields + ")"
                            strSQL = strSQL + " values (" + strValues + ")"
                            objSqlCommand.Parameters.Clear()
                            For i = 0 To intCount - 1 Step 1
                                strValue = objNewData.Item(i).Trim()
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SFJM, _
                                        Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYXH
                                        If strValue = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), 0)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(strValue, Integer))
                                        End If
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
                            strSQL = strSQL + " update 公共_B_人员_兼任 set "
                            strSQL = strSQL + " " + strFields + " "
                            strSQL = strSQL + " where 人员代码 = @oldrydm"
                            strSQL = strSQL + " and 组织代码 = @oldzzdm"
                            objSqlCommand.Parameters.Clear()
                            For i = 0 To intCount - 1 Step 1
                                strValue = objNewData.Item(i).Trim()
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SFJM, _
                                        Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYXH
                                        If strValue = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), 0)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(strValue, Integer))
                                        End If
                                    Case Else
                                        If strValue = "" Then strValue = " "
                                        objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), strValue)
                                End Select
                            Next
                            objSqlCommand.Parameters.AddWithValue("@oldrydm", strOldRYDM)
                            objSqlCommand.Parameters.AddWithValue("@oldzzdm", strOldZZDM)
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()

                            '更新主表“公共_B_人员”的数据
                            strFields = ""
                            strValues = ""
                            intCount = objUpdateData.Count
                            For i = 0 To intCount - 1 Step 1
                                If strFields = "" Then
                                    strFields = objUpdateData.GetKey(i) + " = @A" + i.ToString()
                                Else
                                    strFields = strFields + "," + objUpdateData.GetKey(i) + " = @A" + i.ToString()
                                End If
                            Next
                            strSQL = ""
                            strSQL = strSQL + " update 公共_B_人员 set "
                            strSQL = strSQL + " " + strFields + " "
                            strSQL = strSQL + " where 人员代码 = @oldrydm"
                            objSqlCommand.Parameters.Clear()
                            For i = 0 To intCount - 1 Step 1
                                strValue = objUpdateData.Item(i).Trim()
                                Select Case objUpdateData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SFJM, _
                                        Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYXH
                                        If strValue = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), 0)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(strValue, Integer))
                                        End If
                                    Case Else
                                        If strValue = "" Then strValue = " "
                                        objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), strValue)
                                End Select
                            Next
                            objSqlCommand.Parameters.AddWithValue("@oldrydm", strOldRYDM)
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()

                            '更新副表“公共_B_人员_兼任”的数据
                            strFields = ""
                            strValues = ""
                            For i = 0 To intCount - 1 Step 1
                                If strFields = "" Then
                                    strFields = objUpdateData.GetKey(i) + " = @A" + i.ToString()
                                Else
                                    strFields = strFields + "," + objUpdateData.GetKey(i) + " = @A" + i.ToString()
                                End If
                            Next
                            strSQL = ""
                            strSQL = strSQL + " update 公共_B_人员_兼任 set "
                            strSQL = strSQL + " " + strFields + " "
                            strSQL = strSQL + " where 人员代码 = @oldrydm"
                            objSqlCommand.Parameters.Clear()
                            For i = 0 To intCount - 1 Step 1
                                strValue = objUpdateData.Item(i).Trim()
                                Select Case objUpdateData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SFJM, _
                                        Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYXH
                                        If strValue = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), 0)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(strValue, Integer))
                                        End If
                                    Case Else
                                        If strValue = "" Then strValue = " "
                                        objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), strValue)
                                End Select
                            Next
                            objSqlCommand.Parameters.AddWithValue("@oldrydm", strOldRYDM)
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()


                            '如果人员代码变化
                            If strRYDM <> strOldRYDM Then
                                strSQL = ""
                                strSQL = strSQL + " update 公共_B_上岗 set "
                                strSQL = strSQL + "   人员代码 = @newrydm "
                                strSQL = strSQL + " where 人员代码 = @oldrydm"
                                objSqlCommand.Parameters.Clear()
                                objSqlCommand.Parameters.AddWithValue("@newrydm", strRYDM)
                                objSqlCommand.Parameters.AddWithValue("@oldrydm", strOldRYDM)
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
            doSaveRenyuanData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 删除“公共_B_人员”的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteRenyuanData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            Dim objDataSet As System.Data.DataSet


            '初始化
            doDeleteRenyuanData = False
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
                    Dim strOldRYDM As String
                    Dim strOldZZDM As String
                    strOldRYDM = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYDM), "")

                    strOldZZDM = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_ZZDM), "")


                    strSQL = ""
                    strSQL = " select * from"
                    strSQL = strSQL + " ("
                    strSQL = strSQL + " select  人员代码,组织代码 from 公共_B_人员 where 人员代码='" + strOldRYDM + "' and 组织代码='" + strOldZZDM + "'" + vbCr
                    strSQL = strSQL + " union"
                    strSQL = strSQL + " select  人员代码,组织代码 from 公共_B_人员_兼任 where 人员代码='" + strOldRYDM + "' and 组织代码='" + strOldZZDM + "'" + vbCr
                    strSQL = strSQL + " )a"

                    If objdacCommon.getDataSetBySQL(strErrMsg, strUserId, strPassword, strSQL, objDataSet) = False Then
                        GoTo errProc
                    End If

                    If objDataSet.Tables(0).Rows.Count <= 1 Then
                        '删除人员的上岗信息
                        strSQL = ""
                        strSQL = strSQL + " delete from 公共_B_上岗 "
                        strSQL = strSQL + " where 人员代码 = @rydm"
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@rydm", strOldRYDM)
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.ExecuteNonQuery()
                    End If


                    '删除人员信息
                    strSQL = ""
                    strSQL = strSQL + " delete from 公共_B_人员 "
                    strSQL = strSQL + " where 人员代码 = @rydm"

                    strSQL = strSQL + " and 组织代码 = @zzdm"

                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@rydm", strOldRYDM)

                    objSqlCommand.Parameters.AddWithValue("@zzdm", strOldZZDM)

                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    strSQL = ""
                    strSQL = strSQL + " delete from 公共_B_人员_兼任 "
                    strSQL = strSQL + " where 人员代码 = @rydm"

                    strSQL = strSQL + " and 组织代码 = @zzdm"

                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@rydm", strOldRYDM)

                    objSqlCommand.Parameters.AddWithValue("@zzdm", strOldZZDM)

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
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)

            '返回
            doDeleteRenyuanData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)

            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 将指定人员objRenyuanData位置移动到objRenyuanDataTo
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objRenyuanData       ：准备移动的人员数据
        '     objRenyuanDataTo     ：移动到的人员数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doRenyuanMoveTo( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objRenyuanData As System.Data.DataRow, _
            ByVal objRenyuanDataTo As System.Data.DataRow) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            doRenyuanMoveTo = False
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
                If objRenyuanData Is Nothing Then
                    strErrMsg = "错误：未传入成员数据！"
                    GoTo errProc
                End If
                If objRenyuanDataTo Is Nothing Then
                    strErrMsg = "错误：未传入成员数据！"
                    GoTo errProc
                End If
                Dim strSQL As String

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取信息
                Dim strRYDM As String
                Dim strZZDM As String
                Dim strRYXH As String
                With objPulicParameters
                    strRYDM = .getObjectValue(objRenyuanData.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYDM), "")
                    strZZDM = .getObjectValue(objRenyuanData.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_ZZDM), "")
                    strRYXH = .getObjectValue(objRenyuanData.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYXH), "")
                End With

                '获取下条数据
                Dim strRYDMTo As String
                Dim strZZDMTo As String
                Dim strRYXHTo As String
                With objPulicParameters
                    strRYDMTo = .getObjectValue(objRenyuanDataTo.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYDM), "")
                    strZZDMTo = .getObjectValue(objRenyuanDataTo.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_ZZDM), "")
                    strRYXHTo = .getObjectValue(objRenyuanDataTo.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYXH), "")
                End With

                '检查
                If strZZDM <> strZZDMTo Then
                    strErrMsg = "错误：只能在同一单位下进行序号调整！"
                    GoTo errProc
                End If

                '获取临时代码
                Dim strMaxId As String
                If Me.getNewRYXH(strErrMsg, strUserId, strPassword, strZZDM, strMaxId) = False Then
                    GoTo errProc
                End If

                '开始事务
                Try
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '移动处理
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'strRYDMTo更改到strMaxId
                    strSQL = ""
                    strSQL = strSQL + " update 公共_B_人员 set "
                    strSQL = strSQL + "   人员序号 = @ryxh"
                    strSQL = strSQL + " where 人员代码 = @rydm"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@ryxh", strMaxId)
                    objSqlCommand.Parameters.AddWithValue("@rydm", strRYDMTo)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    'strRYDM更改到strRYXHTO
                    strSQL = ""
                    strSQL = strSQL + " update 公共_B_人员 set "
                    strSQL = strSQL + "   人员序号 = @ryxh"
                    strSQL = strSQL + " where 人员代码 = @rydm"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@ryxh", strRYXHTo)
                    objSqlCommand.Parameters.AddWithValue("@rydm", strRYDM)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    'strRYDMTo更改到strRYXH
                    strSQL = ""
                    strSQL = strSQL + " update 公共_B_人员 set "
                    strSQL = strSQL + "   人员序号 = @ryxh"
                    strSQL = strSQL + " where 人员代码 = @rydm"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@ryxh", strRYXH)
                    objSqlCommand.Parameters.AddWithValue("@rydm", strRYDMTo)
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
            doRenyuanMoveTo = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 对人员列表进行排序(组织代码、人员序号升序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objSqlConnection     ：连接对象
        '     strSrc               ：源字符串
        '     strSep               ：源字符串的分隔符
        '     strDes               ：返回排序后的列表
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doSortRenyuanList( _
            ByRef strErrMsg As String, _
            ByVal objSqlConnection As System.Data.SqlClient.SqlConnection, _
            ByVal strSrc As String, _
            ByVal strSep As String, _
            ByRef strDes As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet

            doSortRenyuanList = False
            strDes = strSrc

            Try
                '检查
                If strSrc Is Nothing Then strSrc = ""
                strSrc = strSrc.Trim()
                If objSqlConnection Is Nothing Then
                    strErrMsg = "错误：未指定数据库连接！"
                    GoTo errProc
                End If
                If strSrc = "" Then Exit Try

                '分隔源字符串
                Dim strValue() As String = strSrc.Split(strSep.ToCharArray())
                If strValue.Length < 1 Then Exit Try

                '排序
                Dim strTemp As String
                Dim strSQL As String
                Dim intCount As Integer
                Dim i As Integer
                intCount = strValue.Length
                strSQL = ""
                strSQL = strSQL + " select a.人员名称, b.组织代码, b.人员序号 " + vbCr
                strSQL = strSQL + " from " + vbCr
                strSQL = strSQL + " (" + vbCr
                strTemp = ""
                For i = 0 To intCount - 1 Step 1
                    If strTemp = "" Then
                        strTemp = "             select '" + strValue(i) + "' as 人员名称" + vbCr
                    Else
                        strTemp = strTemp + "   union " + vbCr
                        strTemp = strTemp + "   select '" + strValue(i) + "' as 人员名称" + vbCr
                    End If
                Next
                strSQL = strSQL + " " + strTemp + vbCr
                strSQL = strSQL + " ) a " + vbCr
                strSQL = strSQL + " left join 公共_B_人员 b on a.人员名称 = b.人员名称 " + vbCr
                strSQL = strSQL + " order by b.组织代码, cast(b.人员序号 as integer)" + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    Exit Try
                End If

                '重新合成
                Dim strName As String
                With objDataSet.Tables(0)
                    intCount = .Rows.Count
                    strTemp = ""
                    For i = 0 To intCount - 1 Step 1
                        strName = objPulicParameters.getObjectValue(.Rows(i).Item("人员名称"), "")
                        If strTemp = "" Then
                            strTemp = strName
                        Else
                            strTemp = strTemp + strSep + strName
                        End If
                    Next
                End With

                '返回
                strDes = strTemp

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            doSortRenyuanList = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 从strSrc中解析出其中包含的人员列表(strSep分隔)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objSqlConnection     ：连接对象
        '     strSrc               ：源字符串
        '     strSep               ：源字符串的分隔符
        '     strDes               ：返回排序后的列表
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getRenyuanList( _
            ByRef strErrMsg As String, _
            ByVal objSqlConnection As System.Data.SqlClient.SqlConnection, _
            ByVal strSrc As String, _
            ByVal strSep As String, _
            ByRef strDes As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            getRenyuanList = False
            strDes = ""

            Try
                '检查
                If strSrc Is Nothing Then strSrc = ""
                strSrc = strSrc.Trim()
                If objSqlConnection Is Nothing Then
                    strErrMsg = "错误：未指定数据库连接！"
                    GoTo errProc
                End If
                If strSrc = "" Then Exit Try

                '转换为SQL列表
                Dim strSrcList As String = ""
                If objdacCommon.doConvertToSQLValueList(strErrMsg, strSrc, strSep, strSrcList) = False Then
                    GoTo errProc
                End If

                '解析范围，计算出范围中包含的部门、人员
                Dim intCYBZ As Integer = Xydc.Platform.Common.Data.FenfafanweiData.enumFWBZ.CHENGYUAN
                Dim strCYLX_DW As String = Xydc.Platform.Common.Data.FenfafanweiData.CYLX_DANWEI
                Dim strCYLX_GR As String = Xydc.Platform.Common.Data.FenfafanweiData.CYLX_GEREN
                strSQL = ""
                strSQL = strSQL + "   select c.人员名称 " + vbCr
                strSQL = strSQL + "   from " + vbCr
                strSQL = strSQL + "   ( " + vbCr
                strSQL = strSQL + "     select a.成员名称 " + vbCr
                strSQL = strSQL + "     from 公文_B_分发范围 a " + vbCr
                strSQL = strSQL + "     where a.范围标志 = " + intCYBZ.ToString() + vbCr
                strSQL = strSQL + "     and   a.成员类型 = '" + strCYLX_DW + "' " + vbCr
                strSQL = strSQL + "     and   a.范围名称 in (" + strSrcList + ") " + vbCr
                strSQL = strSQL + "   ) a " + vbCr
                strSQL = strSQL + "   left join 公共_B_组织机构 b on a.成员名称 = b.组织名称 " + vbCr
                strSQL = strSQL + "   left join 公共_B_人员     c on b.秘书代码 = c.人员代码 " + vbCr
                strSQL = strSQL + "   where c.人员代码 is not null " + vbCr
                strSQL = strSQL + "   union " + vbCr
                strSQL = strSQL + "   select b.人员名称 " + vbCr
                strSQL = strSQL + "   from " + vbCr
                strSQL = strSQL + "   ( " + vbCr
                strSQL = strSQL + "     select a.成员名称 " + vbCr
                strSQL = strSQL + "     from 公文_B_分发范围 a " + vbCr
                strSQL = strSQL + "     where a.范围标志 = " + intCYBZ.ToString() + vbCr
                strSQL = strSQL + "     and   a.成员类型 = '" + strCYLX_GR + "' " + vbCr
                strSQL = strSQL + "     and   a.范围名称 in (" + strSrcList + ") " + vbCr
                strSQL = strSQL + "   ) a " + vbCr
                strSQL = strSQL + "   left join 公共_B_人员 b on a.成员名称 = b.人员名称 " + vbCr
                strSQL = strSQL + "   where b.人员代码 is not null " + vbCr
                '
                strSQL = strSQL + "   union " + vbCr
                '
                '解析部门
                strSQL = strSQL + "   select b.人员名称 " + vbCr
                strSQL = strSQL + "   from " + vbCr
                strSQL = strSQL + "   ( " + vbCr
                strSQL = strSQL + "     select a.秘书代码 " + vbCr
                strSQL = strSQL + "     from 公共_B_组织机构 a " + vbCr
                strSQL = strSQL + "     where a.组织名称 in (" + strSrcList + ") " + vbCr
                strSQL = strSQL + "   ) a " + vbCr
                strSQL = strSQL + "   left join 公共_B_人员 b on a.秘书代码 = b.人员代码 " + vbCr
                strSQL = strSQL + "   where b.人员代码 is not null " + vbCr
                '
                strSQL = strSQL + "   union " + vbCr
                '
                '解析人员
                strSQL = strSQL + "   select a.人员名称 " + vbCr
                strSQL = strSQL + "   from 公共_B_人员 a " + vbCr
                strSQL = strSQL + "   where a.人员名称 in (" + strSrcList + ") " + vbCr

                '总体解析
                Dim strTempSQL As String = strSQL
                strSQL = ""
                strSQL = strSQL + " select * " + vbCr
                strSQL = strSQL + " from " + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + " " + strTempSQL + vbCr
                strSQL = strSQL + " ) a " + vbCr
                strSQL = strSQL + " group by a.人员名称 " + vbCr
                strSQL = strSQL + " order by a.人员名称 " + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    Exit Try
                End If

                '重新合成
                Dim intCount As Integer
                Dim strName As String
                Dim strTemp As String
                Dim i As Integer
                With objDataSet.Tables(0)
                    intCount = .Rows.Count
                    strTemp = ""
                    For i = 0 To intCount - 1 Step 1
                        strName = objPulicParameters.getObjectValue(.Rows(i).Item("人员名称"), "")
                        If strTemp = "" Then
                            strTemp = strName
                        Else
                            strTemp = strTemp + strSep + strName
                        End If
                    Next
                End With

                '返回
                strDes = strTemp

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getRenyuanList = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 从strSrc中解析出其中包含的人员列表(strSep分隔)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objSqlConnection     ：连接对象
        '     strSrc               ：源字符串
        '     strSep               ：源字符串的分隔符
        '     strRymcList          ：返回排序后的人员名称列表
        '     strRydmList          ：返回排序后的人员代码列表
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getRenyuanList( _
            ByRef strErrMsg As String, _
            ByVal objSqlConnection As System.Data.SqlClient.SqlConnection, _
            ByVal strSrc As String, _
            ByVal strSep As String, _
            ByRef strRymcList As String, _
            ByRef strRydmList As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            getRenyuanList = False
            strRymcList = ""
            strRydmList = ""

            Try
                '检查
                If strSrc Is Nothing Then strSrc = ""
                strSrc = strSrc.Trim()
                If objSqlConnection Is Nothing Then
                    strErrMsg = "错误：未指定数据库连接！"
                    GoTo errProc
                End If
                If strSrc = "" Then Exit Try

                '转换为SQL列表
                Dim strSrcList As String = ""
                If objdacCommon.doConvertToSQLValueList(strErrMsg, strSrc, strSep, strSrcList) = False Then
                    GoTo errProc
                End If

                '解析范围，计算出范围中包含的部门、人员
                Dim intCYBZ As Integer = Xydc.Platform.Common.Data.FenfafanweiData.enumFWBZ.CHENGYUAN
                Dim strCYLX_DW As String = Xydc.Platform.Common.Data.FenfafanweiData.CYLX_DANWEI
                Dim strCYLX_GR As String = Xydc.Platform.Common.Data.FenfafanweiData.CYLX_GEREN
                strSQL = ""
                strSQL = strSQL + "   select c.人员名称 " + vbCr
                strSQL = strSQL + "   from " + vbCr
                strSQL = strSQL + "   ( " + vbCr
                strSQL = strSQL + "     select a.成员名称 " + vbCr
                strSQL = strSQL + "     from 公文_B_分发范围 a " + vbCr
                strSQL = strSQL + "     where a.范围标志 = " + intCYBZ.ToString() + vbCr
                strSQL = strSQL + "     and   a.成员类型 = '" + strCYLX_DW + "' " + vbCr
                strSQL = strSQL + "     and   a.范围名称 in (" + strSrcList + ") " + vbCr
                strSQL = strSQL + "   ) a " + vbCr
                strSQL = strSQL + "   left join 公共_B_组织机构 b on a.成员名称 = b.组织名称 " + vbCr
                strSQL = strSQL + "   left join 公共_B_人员     c on b.秘书代码 = c.人员代码 " + vbCr
                strSQL = strSQL + "   where c.人员代码 is not null " + vbCr
                strSQL = strSQL + "   union " + vbCr
                strSQL = strSQL + "   select b.人员名称 " + vbCr
                strSQL = strSQL + "   from " + vbCr
                strSQL = strSQL + "   ( " + vbCr
                strSQL = strSQL + "     select a.成员名称 " + vbCr
                strSQL = strSQL + "     from 公文_B_分发范围 a " + vbCr
                strSQL = strSQL + "     where a.范围标志 = " + intCYBZ.ToString() + vbCr
                strSQL = strSQL + "     and   a.成员类型 = '" + strCYLX_GR + "' " + vbCr
                strSQL = strSQL + "     and   a.范围名称 in (" + strSrcList + ") " + vbCr
                strSQL = strSQL + "   ) a " + vbCr
                strSQL = strSQL + "   left join 公共_B_人员 b on a.成员名称 = b.人员名称 " + vbCr
                strSQL = strSQL + "   where b.人员代码 is not null " + vbCr
                '
                strSQL = strSQL + "   union " + vbCr
                '
                '解析部门
                strSQL = strSQL + "   select b.人员名称 " + vbCr
                strSQL = strSQL + "   from " + vbCr
                strSQL = strSQL + "   ( " + vbCr
                strSQL = strSQL + "     select a.秘书代码 " + vbCr
                strSQL = strSQL + "     from 公共_B_组织机构 a " + vbCr
                strSQL = strSQL + "     where a.组织名称 in (" + strSrcList + ") " + vbCr
                strSQL = strSQL + "   ) a " + vbCr
                strSQL = strSQL + "   left join 公共_B_人员 b on a.秘书代码 = b.人员代码 " + vbCr
                strSQL = strSQL + "   where b.人员代码 is not null " + vbCr
                '
                strSQL = strSQL + "   union " + vbCr
                '
                '解析人员
                strSQL = strSQL + "   select a.人员名称 " + vbCr
                strSQL = strSQL + "   from 公共_B_人员 a " + vbCr
                strSQL = strSQL + "   where a.人员名称 in (" + strSrcList + ") " + vbCr

                '总体解析
                Dim strTempSQL As String = strSQL
                strSQL = ""
                strSQL = strSQL + " select a.*, b.人员代码" + vbCr
                strSQL = strSQL + " from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select * " + vbCr
                strSQL = strSQL + "   from " + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "   " + strTempSQL + vbCr
                strSQL = strSQL + "   ) a " + vbCr
                strSQL = strSQL + "   group by a.人员名称 " + vbCr
                strSQL = strSQL + " ) a" + vbCr
                strSQL = strSQL + " left join 公共_B_人员 b on a.人员名称 = b.人员名称" + vbCr
                strSQL = strSQL + " order by a.人员名称 " + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    Exit Try
                End If

                '重新合成
                Dim strTemp(2) As String
                Dim intCount As Integer
                Dim strName As String
                Dim i As Integer
                With objDataSet.Tables(0)
                    intCount = .Rows.Count
                    strTemp(0) = ""
                    strTemp(1) = ""
                    For i = 0 To intCount - 1 Step 1
                        strName = objPulicParameters.getObjectValue(.Rows(i).Item("人员名称"), "")
                        If strTemp(0) = "" Then
                            strTemp(0) = strName
                        Else
                            strTemp(0) = strTemp(0) + strSep + strName
                        End If

                        strName = objPulicParameters.getObjectValue(.Rows(i).Item("人员代码"), "")
                        If strTemp(1) = "" Then
                            strTemp(1) = strName
                        Else
                            strTemp(1) = strTemp(1) + strSep + strName
                        End If
                    Next
                End With

                '返回
                strRymcList = strTemp(0)
                strRydmList = strTemp(1)

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getRenyuanList = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取人员名称或取人员代码
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objSqlConnection     ：现有连接
        '     strUserXM            ：用户名称
        '     strUserId            ：(返回)用户代码
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getRydmByRymc( _
            ByRef strErrMsg As String, _
            ByVal objSqlConnection As System.Data.SqlClient.SqlConnection, _
            ByVal strUserXM As String, _
            ByRef strUserId As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            getRydmByRymc = False
            strUserId = ""

            Try
                '检查
                If objSqlConnection Is Nothing Then
                    strErrMsg = "错误：连接未打开[getRydmByRymc]！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '获取信息
                strSQL = ""
                strSQL = strSQL + " select 人员代码 from 公共_B_人员" + vbCr
                strSQL = strSQL + " where 人员名称 = '" + strUserXM + "'" + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If

                '返回信息
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strUserId = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item(0), "")
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getRydmByRymc = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取人员代码获取人员名称
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objSqlConnection     ：现有连接
        '     strUserId            ：用户代码
        '     strUserXM            ：(返回)用户名称
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getRymcByRydm( _
            ByRef strErrMsg As String, _
            ByVal objSqlConnection As System.Data.SqlClient.SqlConnection, _
            ByVal strUserId As String, _
            ByRef strUserXM As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            getRymcByRydm = False
            strUserXM = ""

            Try
                '检查
                If objSqlConnection Is Nothing Then
                    strErrMsg = "错误：连接未打开[getRymcByRydm]！"
                    GoTo errProc
                End If
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim()

                '获取信息
                strSQL = ""
                strSQL = strSQL + " select 人员名称 from 公共_B_人员" + vbCr
                strSQL = strSQL + " where 人员代码 = '" + strUserId + "'" + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If

                '返回信息
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strUserXM = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item(0), "")
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getRymcByRydm = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取人员名称或取所在单位代码和单位名称
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objSqlConnection     ：现有连接
        '     strUserXM            ：用户名称
        '     strBmdm              ：(返回)所在单位代码
        '     strBmmc              ：(返回)所在单位名称
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getBmdmAndBmmcByRymc( _
            ByRef strErrMsg As String, _
            ByVal objSqlConnection As System.Data.SqlClient.SqlConnection, _
            ByVal strUserXM As String, _
            ByRef strBmdm As String, _
            ByRef strBmmc As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            getBmdmAndBmmcByRymc = False
            strBmdm = ""
            strBmmc = ""

            Try
                '检查
                If objSqlConnection Is Nothing Then
                    strErrMsg = "错误：连接未打开[getBmdmAndBmmcByRymc]！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '获取信息
                strSQL = ""
                strSQL = strSQL + " select a.组织代码,b.组织名称" + vbCr
                strSQL = strSQL + " from 公共_B_人员 a" + vbCr
                strSQL = strSQL + " left join 公共_B_组织机构 b on a.组织代码 = b.组织代码" + vbCr
                strSQL = strSQL + " where a.人员名称 = '" + strUserXM + "'" + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If

                '返回信息
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strBmdm = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item("组织代码"), "")
                    strBmmc = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item("组织名称"), "")
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getBmdmAndBmmcByRymc = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据人员代码获取所在单位代码和单位名称
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objSqlConnection     ：现有连接
        '     strUserDM            ：用户代码
        '     strBmdm              ：(返回)所在单位代码
        '     strBmmc              ：(返回)所在单位名称
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getBmdmAndBmmcByRydm( _
            ByRef strErrMsg As String, _
            ByVal objSqlConnection As System.Data.SqlClient.SqlConnection, _
            ByVal strUserDM As String, _
            ByRef strBmdm As String, _
            ByRef strBmmc As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            getBmdmAndBmmcByRydm = False
            strBmdm = ""
            strBmmc = ""

            Try
                '检查
                If objSqlConnection Is Nothing Then
                    strErrMsg = "错误：连接未打开[getBmdmAndBmmcByRydm]！"
                    GoTo errProc
                End If
                If strUserDM Is Nothing Then strUserDM = ""
                strUserDM = strUserDM.Trim()

                '获取信息
                strSQL = ""
                strSQL = strSQL + " select a.组织代码,b.组织名称" + vbCr
                strSQL = strSQL + " from 公共_B_人员 a" + vbCr
                strSQL = strSQL + " left join 公共_B_组织机构 b on a.组织代码 = b.组织代码" + vbCr
                strSQL = strSQL + " where a.人员代码 = '" + strUserDM + "'" + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If

                '返回信息
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strBmdm = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item("组织代码"), "")
                    strBmmc = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item("组织名称"), "")
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getBmdmAndBmmcByRydm = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function







        '----------------------------------------------------------------
        ' 获取系统进出日志数据
        '     strErrMsg                ：如果错误，则返回错误信息
        '     strUserId                ：用户标识
        '     strPassword              ：用户密码
        '     strWhere                 ：搜索条件
        '     objXitongJinchuRizhiData ：系统进出日志信息数据集
        ' 返回
        '     True                     ：成功
        '     False                    ：失败
        '----------------------------------------------------------------
        Public Function getXitongJinchuRizhiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objXitongJinchuRizhiData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Dim objTempXitongJinchuRizhiData As Xydc.Platform.Common.Data.CustomerData
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '初始化
            getXitongJinchuRizhiData = False
            objXitongJinchuRizhiData = Nothing
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim

                '检查
                If strUserId = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                Try
                    '创建数据集
                    objTempXitongJinchuRizhiData = New Xydc.Platform.Common.Data.CustomerData(Xydc.Platform.Common.Data.CustomerData.enumTableType.GL_B_XITONGJINCHURIZHI)

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
                        strSQL = strSQL + "   select a.*, 操作人名称 = b.人员名称 " + vbCr
                        strSQL = strSQL + "   from 管理_B_系统进出日志 a " + vbCr
                        strSQL = strSQL + "   left join 公共_B_人员 b on a.操作人 = b.人员代码 " + vbCr
                        strSQL = strSQL + " ) a" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.操作时间 desc"

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempXitongJinchuRizhiData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GL_B_XITONGJINCHURIZHI))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempXitongJinchuRizhiData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempXitongJinchuRizhiData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objXitongJinchuRizhiData = objTempXitongJinchuRizhiData
            getXitongJinchuRizhiData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempXitongJinchuRizhiData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取系统在线用户数据
        '     strErrMsg                ：如果错误，则返回错误信息
        '     strUserId                ：用户标识
        '     strPassword              ：用户密码
        '     strWhere                 ：搜索条件
        '     objZaixianYonghuData     ：在线用户信息数据集
        ' 返回
        '     True                     ：成功
        '     False                    ：失败
        '----------------------------------------------------------------
        Public Function getZaixianYonghuData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objZaixianYonghuData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Dim objTempZaixianYonghuData As Xydc.Platform.Common.Data.CustomerData
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '初始化
            getZaixianYonghuData = False
            objZaixianYonghuData = Nothing
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim

                '检查
                If strUserId = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If


                '自动清理过期数据
                If Me.doDeleteZaixianYonghu(strErrMsg, strUserId, strPassword, True) = False Then
                    '忽略
                End If


                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                Try
                    '创建数据集
                    objTempZaixianYonghuData = New Xydc.Platform.Common.Data.CustomerData(Xydc.Platform.Common.Data.CustomerData.enumTableType.GL_B_ZAIXIANYONGHU)

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
                        strSQL = strSQL + "     操作人名称 = b.人员名称," + vbCr
                        strSQL = strSQL + "     上线时长 = dbo.getDateSubstract(a.上线时间, getdate())" + vbCr
                        strSQL = strSQL + "   from 管理_B_在线用户 a " + vbCr
                        strSQL = strSQL + "   left join 公共_B_人员 b on a.操作人 = b.人员代码 " + vbCr
                        strSQL = strSQL + " ) a" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.操作人 desc"

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempZaixianYonghuData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GL_B_ZAIXIANYONGHU))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempZaixianYonghuData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempZaixianYonghuData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objZaixianYonghuData = objTempZaixianYonghuData
            getZaixianYonghuData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempZaixianYonghuData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 写“系统进出日志”
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strCZLX              ：操作类型
        '     strAddress           ：机器地址
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doWriteXitongJinchuRizhi( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strCZLX As String, _
            ByVal strAddress As String) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '初始化
            doWriteXitongJinchuRizhi = False
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
                If strCZLX Is Nothing Then strCZLX = ""
                strCZLX = strCZLX.Trim
                If strCZLX = "" Then strCZLX = Xydc.Platform.Common.Data.CustomerData.STATUS_LOGIN
                If strAddress Is Nothing Then strAddress = ""
                strAddress = strAddress.Trim

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
                    strSQL = ""
                    strSQL = strSQL + " insert 管理_B_系统进出日志 (" + vbCr
                    strSQL = strSQL + "   操作人,操作时间,操作类型,机器地址" + vbCr
                    strSQL = strSQL + " ) values (" + vbCr
                    strSQL = strSQL + "   @czr, @czsj, @czlx, @jqdz"
                    strSQL = strSQL + " )" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@czr", strUserId)
                    objSqlCommand.Parameters.AddWithValue("@czsj", Now)
                    objSqlCommand.Parameters.AddWithValue("@czlx", strCZLX)
                    objSqlCommand.Parameters.AddWithValue("@jqdz", strAddress)
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
            doWriteXitongJinchuRizhi = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 清除“系统进出日志”
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteXitongJinchuRizhi( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '初始化
            doDeleteXitongJinchuRizhi = False
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

                '保存数据
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '计算SQL
                    strSQL = ""
                    strSQL = strSQL + " delete from 管理_B_系统进出日志" + vbCr
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
            doDeleteXitongJinchuRizhi = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 删除“系统进出日志”
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     intXH                ：要删除的序号
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteXitongJinchuRizhi( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intXH As Integer) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '初始化
            doDeleteXitongJinchuRizhi = False
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

                '保存数据
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '计算SQL
                    strSQL = ""
                    strSQL = strSQL + " delete from 管理_B_系统进出日志" + vbCr
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

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doDeleteXitongJinchuRizhi = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 删除“系统进出日志”
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strQSRQ              ：要删除的开始日期
        '     strZZRQ              ：要删除的结束日期
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteXitongJinchuRizhi( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '初始化
            doDeleteXitongJinchuRizhi = False
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
                If strQSRQ Is Nothing Then strQSRQ = ""
                strQSRQ = strQSRQ.Trim
                If strZZRQ Is Nothing Then strZZRQ = ""
                strZZRQ = strZZRQ.Trim

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
                    strSQL = ""
                    strSQL = strSQL + " delete from 管理_B_系统进出日志" + vbCr
                    strSQL = strSQL + " where convert(varchar(10),操作时间,120) between @qsrq and @zzrq" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@qsrq", strQSRQ)
                    objSqlCommand.Parameters.AddWithValue("@zzrq", strZZRQ)
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
            doDeleteXitongJinchuRizhi = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 写“在线用户”数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doWriteZaixianYonghu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '初始化
            doWriteZaixianYonghu = False
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

                '保存数据
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '清除现有
                    strSQL = ""
                    strSQL = strSQL + " delete from 管理_B_在线用户" + vbCr
                    strSQL = strSQL + " where 操作人 = @czr"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@czr", strUserId)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '写新的数据
                    strSQL = ""
                    strSQL = strSQL + " insert 管理_B_在线用户 (" + vbCr
                    strSQL = strSQL + "   操作人,上线时间" + vbCr
                    strSQL = strSQL + " ) values (" + vbCr
                    strSQL = strSQL + "   @czr, @sxsj"
                    strSQL = strSQL + " )" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@czr", strUserId)
                    objSqlCommand.Parameters.AddWithValue("@sxsj", Now)
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
            doWriteZaixianYonghu = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 删除“在线用户”数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteZaixianYonghu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '初始化
            doDeleteZaixianYonghu = False
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

                '保存数据
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '清除现有
                    strSQL = ""
                    strSQL = strSQL + " delete from 管理_B_在线用户" + vbCr
                    strSQL = strSQL + " where 操作人 = @czr"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@czr", strUserId)
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
            doDeleteZaixianYonghu = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取用户操作日志数据
        '     strErrMsg                ：如果错误，则返回错误信息
        '     strUserId                ：用户标识
        '     strPassword              ：用户密码
        '     strWhere                 ：搜索条件
        '     objLogData               ：(返回)数据集
        ' 返回
        '     True                     ：成功
        '     False                    ：失败
        '----------------------------------------------------------------
        Public Function getYonghuCaozuoRizhiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objLogData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempLogData As Xydc.Platform.Common.Data.CustomerData
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '初始化
            getYonghuCaozuoRizhiData = False
            objLogData = Nothing
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim

                '检查
                If strUserId = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                Try
                    '创建数据集
                    objTempLogData = New Xydc.Platform.Common.Data.CustomerData(Xydc.Platform.Common.Data.CustomerData.enumTableType.GL_B_YONGHUCAOZUORIZHI)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.*" + vbCr
                        strSQL = strSQL + " from 管理_B_用户操作日志 a" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.操作时间 desc"

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempLogData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GL_B_YONGHUCAOZUORIZHI))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempLogData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempLogData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objLogData = objTempLogData
            getYonghuCaozuoRizhiData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempLogData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 写“用户操作日志”
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strAddress           ：机器地址
        '     strCZSM              ：操作说明
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doWriteYonghuCaozuoRizhi( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strAddress As String, _
            ByVal strCZSM As String) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '初始化
            doWriteYonghuCaozuoRizhi = False
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
                If strCZSM Is Nothing Then strCZSM = ""
                strCZSM = strCZSM.Trim
                If strCZSM = "" Then strCZSM = Xydc.Platform.Common.Data.CustomerData.STATUS_LOGIN
                If strAddress Is Nothing Then strAddress = ""
                strAddress = strAddress.Trim

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
                    strSQL = ""
                    strSQL = strSQL + " insert 管理_B_用户操作日志 (" + vbCr
                    strSQL = strSQL + "   操作人,操作时间,机器地址,操作说明" + vbCr
                    strSQL = strSQL + " ) values (" + vbCr
                    strSQL = strSQL + "   @czr, @czsj, @jqdz, @czsm" + vbCr
                    strSQL = strSQL + " )" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@czr", strUserId)
                    objSqlCommand.Parameters.AddWithValue("@czsj", Now)
                    objSqlCommand.Parameters.AddWithValue("@jqdz", strAddress)
                    objSqlCommand.Parameters.AddWithValue("@czsm", strCZSM)
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
            doWriteYonghuCaozuoRizhi = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 写“用户操作日志”
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strAddress           ：机器地址
        '     strMachine           ：机器名称
        '     strCZSM              ：操作说明
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        ' 更改说明：
        '      增加strMachine参数及相关处理
        '----------------------------------------------------------------
        Public Function doWriteYonghuCaozuoRizhi( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strAddress As String, _
            ByVal strMachine As String, _
            ByVal strCZSM As String) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '初始化
            doWriteYonghuCaozuoRizhi = False
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
                If strCZSM Is Nothing Then strCZSM = ""
                strCZSM = strCZSM.Trim
                If strCZSM = "" Then strCZSM = Xydc.Platform.Common.Data.CustomerData.STATUS_LOGIN
                If strAddress Is Nothing Then strAddress = ""
                strAddress = strAddress.Trim

                If strMachine Is Nothing Then strMachine = ""
                strMachine = strMachine.Trim


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
                    strSQL = ""
                    strSQL = strSQL + " insert 管理_B_用户操作日志 (" + vbCr

                    'strSQL = strSQL + "   操作人,操作时间,机器地址,操作说明" + vbCr
                    strSQL = strSQL + "   操作人,操作时间,机器地址,机器名称,操作说明" + vbCr

                    strSQL = strSQL + " ) values (" + vbCr

                    'strSQL = strSQL + "   @czr, @czsj, @jqdz, @czsm" + vbCr
                    strSQL = strSQL + "   @czr, @czsj, @jqdz, @jqmc, @czsm" + vbCr

                    strSQL = strSQL + " )" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@czr", strUserId)
                    objSqlCommand.Parameters.AddWithValue("@czsj", Now)
                    objSqlCommand.Parameters.AddWithValue("@jqdz", strAddress)

                    objSqlCommand.Parameters.AddWithValue("@jqmc", strMachine)

                    objSqlCommand.Parameters.AddWithValue("@czsm", strCZSM)
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
            doWriteYonghuCaozuoRizhi = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 写“系统进出日志”
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strCZLX              ：操作类型
        '     strAddress           ：机器地址
        '     strMachine           ：机器名称
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        ' 更改说明：
        '      增加strMachine参数及相关处理
        '----------------------------------------------------------------
        Public Function doWriteXitongJinchuRizhi( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strCZLX As String, _
            ByVal strAddress As String, _
            ByVal strMachine As String) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '初始化
            doWriteXitongJinchuRizhi = False
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
                If strCZLX Is Nothing Then strCZLX = ""
                strCZLX = strCZLX.Trim
                If strCZLX = "" Then strCZLX = Xydc.Platform.Common.Data.CustomerData.STATUS_LOGIN
                If strAddress Is Nothing Then strAddress = ""
                strAddress = strAddress.Trim

                If strMachine Is Nothing Then strMachine = ""
                strMachine = strMachine.Trim


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
                    strSQL = ""
                    strSQL = strSQL + " insert 管理_B_系统进出日志 (" + vbCr

                    'strSQL = strSQL + "   操作人,操作时间,操作类型,机器地址" + vbCr
                    strSQL = strSQL + "   操作人,操作时间,操作类型,机器地址,机器名称" + vbCr

                    strSQL = strSQL + " ) values (" + vbCr

                    'strSQL = strSQL + "   @czr, @czsj, @czlx, @jqdz"
                    strSQL = strSQL + "   @czr, @czsj, @czlx, @jqdz, @jqmc"

                    strSQL = strSQL + " )" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@czr", strUserId)
                    objSqlCommand.Parameters.AddWithValue("@czsj", Now)
                    objSqlCommand.Parameters.AddWithValue("@czlx", strCZLX)
                    objSqlCommand.Parameters.AddWithValue("@jqdz", strAddress)

                    objSqlCommand.Parameters.AddWithValue("@jqmc", strMachine)

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
            doWriteXitongJinchuRizhi = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 自动清除当前日期之前的“系统进出日志”数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     blnAutoClear         ：接口重载
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        ' 更改描述
        '      创建
        '----------------------------------------------------------------
        Public Function doDeleteZaixianYonghu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal blnAutoClear As Boolean) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '初始化
            doDeleteZaixianYonghu = False
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

                '保存数据
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '计算SQL
                    strSQL = ""
                    strSQL = strSQL + " delete from 管理_B_在线用户" + vbCr
                    strSQL = strSQL + " where 上线时间 < convert(varchar(10),getdate(),120)" + vbCr
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
            doDeleteZaixianYonghu = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

    End Class

End Namespace
