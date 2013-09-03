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
    ' 类名    ：dacFenfafanwei
    '
    ' 功能描述：
    '     提供对“公文_B_分发范围”数据的增加、修改、删除、检索等操作
    '----------------------------------------------------------------

    Public Class dacFenfafanwei
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.DataAccess.dacFenfafanwei)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub










        '----------------------------------------------------------------
        ' 获取全部的范围主记录的数据集(以范围名称升序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWhere             ：搜索条件(默认表前缀a.)
        '     objFenfafanweiData   ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getFenfafanweiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objFenfafanweiData As Xydc.Platform.Common.Data.FenfafanweiData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempFenfafanweiData As Xydc.Platform.Common.Data.FenfafanweiData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getFenfafanweiData = False
            objFenfafanweiData = Nothing
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
                    objTempFenfafanweiData = New Xydc.Platform.Common.Data.FenfafanweiData(Xydc.Platform.Common.Data.FenfafanweiData.enumTableType.GW_B_FENFAFANWEI)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from 公文_B_分发范围 a " + vbCr
                        strSQL = strSQL + " where a.范围标志 = @fwbz " + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " and " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.范围名称 " + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@fwbz", CType(Xydc.Platform.Common.Data.FenfafanweiData.enumFWBZ.MAIN, Integer).ToString())
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempFenfafanweiData.Tables(Xydc.Platform.Common.Data.FenfafanweiData.TABLE_GW_B_FENFAFANWEI))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempFenfafanweiData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.FenfafanweiData.SafeRelease(objTempFenfafanweiData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objFenfafanweiData = objTempFenfafanweiData
            getFenfafanweiData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.FenfafanweiData.SafeRelease(objTempFenfafanweiData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取指定范围内的成员的数据集(以成员位置升序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strFWMC              ：范围名称
        '     strWhere             ：搜索条件(默认表前缀a.)
        '     objFenfafanweiData   ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getFenfafanweiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strFWMC As String, _
            ByVal strWhere As String, _
            ByRef objFenfafanweiData As Xydc.Platform.Common.Data.FenfafanweiData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempFenfafanweiData As Xydc.Platform.Common.Data.FenfafanweiData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getFenfafanweiData = False
            objFenfafanweiData = Nothing
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strWhere.Length > 0 Then strWhere = strWhere.Trim()
                If strFWMC.Length > 0 Then strFWMC = strFWMC.Trim()

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
                    objTempFenfafanweiData = New Xydc.Platform.Common.Data.FenfafanweiData(Xydc.Platform.Common.Data.FenfafanweiData.enumTableType.GW_B_FENFAFANWEI)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from 公文_B_分发范围 a " + vbCr
                        strSQL = strSQL + " where a.范围标志 = @fwbz " + vbCr
                        strSQL = strSQL + " and   a.范围名称 = @fwmc " + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " and " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.成员位置 " + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@fwbz", CType(Xydc.Platform.Common.Data.FenfafanweiData.enumFWBZ.CHENGYUAN, Integer).ToString())
                        objSqlCommand.Parameters.AddWithValue("@fwmc", strFWMC)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempFenfafanweiData.Tables(Xydc.Platform.Common.Data.FenfafanweiData.TABLE_GW_B_FENFAFANWEI))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempFenfafanweiData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.FenfafanweiData.SafeRelease(objTempFenfafanweiData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objFenfafanweiData = objTempFenfafanweiData
            getFenfafanweiData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.FenfafanweiData.SafeRelease(objTempFenfafanweiData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function


        '----------------------------------------------------------------
        ' 获取全部的范围主记录的数据集(以范围名称升序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWhere             ：搜索条件(默认表前缀a.)
        '     objFenfafanweiData   ：信息数据集
        '     blnNone              ：重载用
        ' 返回
        '     True                 ：成功
        '     False                ：失败

        '----------------------------------------------------------------
        Public Function getFenfafanweiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objFenfafanweiData As Xydc.Platform.Common.Data.FenfafanweiData, _
            ByVal blnNone As Boolean) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempFenfafanweiData As Xydc.Platform.Common.Data.FenfafanweiData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getFenfafanweiData = False
            objFenfafanweiData = Nothing
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
                    objTempFenfafanweiData = New Xydc.Platform.Common.Data.FenfafanweiData(Xydc.Platform.Common.Data.FenfafanweiData.enumTableType.GW_B_FENFAFANWEI)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from 公文_B_分发范围 a " + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.范围名称 " + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempFenfafanweiData.Tables(Xydc.Platform.Common.Data.FenfafanweiData.TABLE_GW_B_FENFAFANWEI))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempFenfafanweiData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.FenfafanweiData.SafeRelease(objTempFenfafanweiData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objFenfafanweiData = objTempFenfafanweiData
            getFenfafanweiData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.FenfafanweiData.SafeRelease(objTempFenfafanweiData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 检查“公文_B_分发范围”的数据的合法性(范围主记录)
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
        Public Function doVerifyFenfafanweiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim objListDictionary As System.Collections.Specialized.ListDictionary

            doVerifyFenfafanweiData = False

            Try
                Dim intOldLSH As Integer
                Dim strFWMC As String
                Dim intLen As Integer
                Dim strSQL As String

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
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                    Case Else
                        If objOldData Is Nothing Then
                            strErrMsg = "错误：未传入旧的数据！"
                            GoTo errProc
                        End If
                        intOldLSH = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_LSH), 0)
                End Select

                '获取表结构定义
                strSQL = "select top 0 * from 公文_B_分发范围"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, strUserId, strPassword, strSQL, "公文_B_分发范围", objDataSet) = False Then
                    GoTo errProc
                End If

                '检查数据长度
                strFWMC = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_FWMC), "")
                If strFWMC = "" Then
                    strErrMsg = "错误：[范围名称]不能为空！"
                    GoTo errProc
                End If
                With objDataSet.Tables(0).Columns(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_FWMC)
                    intLen = objPulicParameters.getStringLength(strFWMC)
                    If intLen > .MaxLength Then
                        strErrMsg = "错误：[范围名称]长度不能超过[" + .MaxLength.ToString() + "]，实际有[" + intLen.ToString() + "]！"
                        GoTo errProc
                    End If
                End With

                Dim strFWBZ As String = CType(Xydc.Platform.Common.Data.FenfafanweiData.enumFWBZ.MAIN, Integer).ToString()

                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '检查约束
                objListDictionary = New System.Collections.Specialized.ListDictionary
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                        strSQL = ""
                        strSQL = strSQL + " select 实体代码,实体名称 from 公共_V_全部实体名称" + vbCr
                        strSQL = strSQL + " where 实体名称 = @fwmc" + vbCr
                        objListDictionary.Add("@fwmc", strFWMC)
                    Case Else
                        strSQL = ""
                        strSQL = strSQL + " select 实体代码 = convert(varchar(36),流水号),实体名称=范围名称 from 公文_B_分发范围" + vbCr
                        strSQL = strSQL + " where 范围名称 = @fwmc" + vbCr
                        strSQL = strSQL + " and   范围标志 = @fwbz" + vbCr
                        strSQL = strSQL + " and   流水号   <> @oldlsh" + vbCr
                        strSQL = strSQL + " union" + vbCr
                        strSQL = strSQL + " select 实体代码,实体名称 from 公共_V_全部实体名称" + vbCr
                        strSQL = strSQL + " where 实体名称 = @fwmc" + vbCr
                        strSQL = strSQL + " and   实体代码 <> @oldlshs" + vbCr
                        objListDictionary.Add("@fwmc", strFWMC)
                        objListDictionary.Add("@fwbz", strFWBZ)
                        objListDictionary.Add("@oldlsh", intOldLSH)
                        objListDictionary.Add("@oldlshs", intOldLSH.ToString)
                End Select
                If objdacCommon.getDataSetBySQL(strErrMsg, strUserId, strPassword, strSQL, objListDictionary, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strErrMsg = "错误：[" + strFWMC + "]已经存在！"
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

            doVerifyFenfafanweiData = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objListDictionary)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 保存“公文_B_分发范围”的数据(范围主记录)
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
        Public Function doSaveFenfafanweiData( _
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
            doSaveFenfafanweiData = False
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
                    Dim strOldFWMC As String
                    Dim intOldLSH As Integer
                    Dim strFWMC As String
                    Dim strFWBZ As String
                    Dim strCYLX As String
                    Dim strCYMC As String
                    Dim intCYWZ As Integer
                    With objPulicParameters
                        strFWMC = .getObjectValue(objNewData(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_FWMC), "")
                        strFWBZ = CType(Xydc.Platform.Common.Data.FenfafanweiData.enumFWBZ.MAIN, Integer).ToString()
                        strCYLX = " "
                        strCYMC = " "
                        intCYWZ = 0
                    End With
                    Select Case objenumEditType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                            strSQL = ""
                            strSQL = strSQL + " insert into 公文_B_分发范围 (范围名称,范围标志,成员类型,成员名称,成员位置)"
                            strSQL = strSQL + " values (@fwmc, @fwbz, @cylx, @cymc, @cywz)"
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@fwmc", strFWMC)
                            objSqlCommand.Parameters.AddWithValue("@fwbz", strFWBZ)
                            objSqlCommand.Parameters.AddWithValue("@cylx", strCYLX)
                            objSqlCommand.Parameters.AddWithValue("@cymc", strCYMC)
                            objSqlCommand.Parameters.AddWithValue("@cywz", intCYWZ)
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()
                        Case Else
                            With objPulicParameters
                                intOldLSH = .getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_LSH), 0)
                                strOldFWMC = .getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_FWMC), "")
                            End With
                            strSQL = ""
                            strSQL = strSQL + " update 公文_B_分发范围 set "
                            strSQL = strSQL + "   范围名称 = @fwmc,"
                            strSQL = strSQL + "   范围标志 = @fwbz,"
                            strSQL = strSQL + "   成员类型 = @cylx,"
                            strSQL = strSQL + "   成员名称 = @cymc,"
                            strSQL = strSQL + "   成员位置 = @cywz "
                            strSQL = strSQL + " where 流水号 = @oldlsh"
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@fwmc", strFWMC)
                            objSqlCommand.Parameters.AddWithValue("@fwbz", strFWBZ)
                            objSqlCommand.Parameters.AddWithValue("@cylx", strCYLX)
                            objSqlCommand.Parameters.AddWithValue("@cymc", strCYMC)
                            objSqlCommand.Parameters.AddWithValue("@cywz", intCYWZ)
                            objSqlCommand.Parameters.AddWithValue("@oldlsh", intOldLSH)
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()

                            If strOldFWMC <> strFWMC Then
                                '更改相关的范围成员
                                strSQL = ""
                                strSQL = strSQL + " update 公文_B_分发范围 set "
                                strSQL = strSQL + "   范围名称 = @fwmc "
                                strSQL = strSQL + " where 范围名称 = @oldfwmc"
                                objSqlCommand.Parameters.Clear()
                                objSqlCommand.Parameters.AddWithValue("@fwmc", strFWMC)
                                objSqlCommand.Parameters.AddWithValue("@oldfwmc", strOldFWMC)
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
            doSaveFenfafanweiData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 保存“公文_B_分发范围”的数据(将成员加入几个常用范围中)
        '     strErrMsg                 ：如果错误，则返回错误信息
        '     strUserId                 ：用户标识
        '     strPassword               ：用户密码
        '     objDataSet_ChoiceCYFW     ：新范围数据
        '     objNewData                ：新成员数据
        '     objOldDataSet_ChoiceCYFW  ：旧范围数据
        ' 返回
        '     True                      ：成功
        '     False                     ：失败

        '----------------------------------------------------------------
        Public Function doSaveFenfafanweiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objDataSet_ChoiceCYFW As Xydc.Platform.Common.Data.FenfafanweiData, _
            ByVal objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objOldDataSet_ChoiceCYFW As Xydc.Platform.Common.Data.FenfafanweiData) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            doSaveFenfafanweiData = False
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

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                ''开始事务
                'Try
                '    objSqlTransaction = objSqlConnection.BeginTransaction()
                'Catch ex As Exception
                '    strErrMsg = ex.Message
                '    GoTo errProc
                'End Try

                '保存数据
                Dim strSQL As String
                Dim i As Integer
                Dim intOldCount As Integer
                Dim intNewCount As Integer
                Dim intLSH As Integer
                Dim strFWMC As String = ""
                Dim strNewCode As String
                Dim intNewCYWZ As Integer
                Dim strFWBZ As String
                Dim strCYLX As String
                Dim strCYMC As String
                Dim strLXDH As String
                Dim strSJHM As String
                Dim strFTPDZ As String
                Dim strYXDZ As String
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行删除操作
                    With objOldDataSet_ChoiceCYFW.Tables(Xydc.Platform.Common.Data.FenfafanweiData.TABLE_GW_B_FENFAFANWEI)
                        intOldCount = .Rows.Count
                        For i = 0 To intOldCount - 1 Step 1
                            strFWMC = ""
                            strFWMC = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_FWMC), " ")
                            intLSH = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_LSH), 0)

                            strSQL = ""
                            strSQL = strSQL + " delete from 公文_B_分发范围 "
                            strSQL = strSQL + " where 流水号 = @lsh"
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@lsh", intLSH)
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()
                        Next i
                    End With

                    '执行加入操作
                    With objDataSet_ChoiceCYFW.Tables(Xydc.Platform.Common.Data.FenfafanweiData.TABLE_GW_B_FENFAFANWEI)
                        intNewCount = .Rows.Count
                        For i = 0 To intNewCount - 1 Step 1
                            If .Rows(i).RowState <> DataRowState.Deleted Then
                                strFWMC = ""
                                strFWMC = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_FWMC), " ")


                                If objdacCommon.getNewCode(strErrMsg, objSqlConnection, "成员位置", "范围名称", strFWMC, "公文_B_分发范围", True, strNewCode) = False Then
                                    GoTo errProc
                                End If

                                intNewCYWZ = CType(strNewCode, Integer)

                                With objPulicParameters
                                    strFWBZ = CType(Xydc.Platform.Common.Data.FenfafanweiData.enumFWBZ.CHENGYUAN, Integer).ToString()
                                    strCYLX = .getObjectValue(objNewData(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_CYLX), " ")
                                    strCYMC = .getObjectValue(objNewData(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_CYMC), " ")
                                    strLXDH = .getObjectValue(objNewData(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_LXDH), " ")
                                    strSJHM = .getObjectValue(objNewData(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_SJHM), " ")
                                    strFTPDZ = .getObjectValue(objNewData(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_FTPDZ), " ")
                                    strYXDZ = .getObjectValue(objNewData(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_YXDZ), " ")
                                End With

                                strSQL = ""
                                strSQL = strSQL + " insert into 公文_B_分发范围 (范围名称,范围标志,成员类型,成员名称,成员位置,联系电话,手机号码,FTP地址,邮箱地址)"
                                strSQL = strSQL + " values (@fwmc, @fwbz, @cylx, @cymc, @cywz, @lxdh, @sjhm, @ftpdz, @yxdz)"
                                objSqlCommand.Parameters.Clear()
                                objSqlCommand.Parameters.AddWithValue("@fwmc", strFWMC)
                                objSqlCommand.Parameters.AddWithValue("@fwbz", strFWBZ)
                                objSqlCommand.Parameters.AddWithValue("@cylx", strCYLX)
                                objSqlCommand.Parameters.AddWithValue("@cymc", strCYMC)
                                objSqlCommand.Parameters.AddWithValue("@cywz", intNewCYWZ)
                                objSqlCommand.Parameters.AddWithValue("@lxdh", strLXDH)
                                objSqlCommand.Parameters.AddWithValue("@sjhm", strSJHM)
                                objSqlCommand.Parameters.AddWithValue("@ftpdz", strFTPDZ)
                                objSqlCommand.Parameters.AddWithValue("@yxdz", strYXDZ)
                                objSqlCommand.CommandText = strSQL
                                objSqlCommand.ExecuteNonQuery()
                            End If
                        Next i
                    End With

                Catch ex As Exception
                    'objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '提交事务
                'objSqlTransaction.Commit()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doSaveFenfafanweiData = True
            'Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 删除“公文_B_分发范围”的数据(范围主记录)，同时删除成员记录
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteFenfafanweiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            doDeleteFenfafanweiData = False
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
                    Dim strFWMC As String
                    With New Xydc.Platform.Common.Utilities.PulicParameters
                        strFWMC = .getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_FWMC), "")
                    End With
                    strSQL = ""
                    strSQL = strSQL + " delete from 公文_B_分发范围 "
                    strSQL = strSQL + " where 范围名称 = @fwmc"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@fwmc", strFWMC)

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
            doDeleteFenfafanweiData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 检查“公文_B_分发范围”的数据的合法性(范围成员记录)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
        '     objNewData           ：新数据
        '     blnIsFWCY            ：仅作接口重载使用
        '     objenumEditType      ：编辑类型

        ' 返回
        '     True                 ：合法
        '     False                ：不合法或其他程序错误
        '----------------------------------------------------------------
        Public Function doVerifyFenfafanweiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal blnIsFWCY As Boolean, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim objListDictionary As System.Collections.Specialized.ListDictionary

            doVerifyFenfafanweiData = False

            Try
                Dim intLen As Integer
                Dim strSQL As String

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
                Dim intOldLSH As Integer
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                    Case Else
                        If objOldData Is Nothing Then
                            strErrMsg = "错误：未传入旧的数据！"
                            GoTo errProc
                        End If
                        intOldLSH = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_LSH), 0)
                End Select

                '获取表结构定义
                strSQL = "select top 0 * from 公文_B_分发范围"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, strUserId, strPassword, strSQL, "公文_B_分发范围", objDataSet) = False Then
                    GoTo errProc
                End If

                '检查数据长度
                Dim strFWMC As String
                strFWMC = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_FWMC), "")
                If strFWMC = "" Then
                    strErrMsg = "错误：[范围名称]不能为空！"
                    GoTo errProc
                End If
                With objDataSet.Tables(0).Columns(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_FWMC)
                    intLen = objPulicParameters.getStringLength(strFWMC)
                    If intLen > .MaxLength Then
                        strErrMsg = "错误：[范围名称]长度不能超过[" + .MaxLength.ToString() + "]，实际有[" + intLen.ToString() + "]！"
                        GoTo errProc
                    End If
                End With

                Dim strCYLX As String
                strCYLX = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_CYLX), "")
                If strCYLX = "" Then
                    strErrMsg = "错误：[成员类型]不能为空！"
                    GoTo errProc
                End If
                With objDataSet.Tables(0).Columns(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_CYLX)
                    intLen = objPulicParameters.getStringLength(strCYLX)
                    If intLen > .MaxLength Then
                        strErrMsg = "错误：[成员类型]长度不能超过[" + .MaxLength.ToString() + "]，实际有[" + intLen.ToString() + "]！"
                        GoTo errProc
                    End If
                End With

                Dim strCYMC As String
                strCYMC = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_CYMC), "")
                If strCYMC = "" Then
                    strErrMsg = "错误：[成员名称]不能为空！"
                    GoTo errProc
                End If
                With objDataSet.Tables(0).Columns(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_CYMC)
                    intLen = objPulicParameters.getStringLength(strCYMC)
                    If intLen > .MaxLength Then
                        strErrMsg = "错误：[成员名称]长度不能超过[" + .MaxLength.ToString() + "]，实际有[" + intLen.ToString() + "]！"
                        GoTo errProc
                    End If
                End With

                Dim intCYWZ As Integer
                Try
                    intCYWZ = CType(objNewData(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_CYWZ), Integer)
                Catch ex As Exception
                    strErrMsg = "错误：无效的成员序号！"
                    GoTo errProc
                End Try
                If intCYWZ < 1 Or intCYWZ > 999999 Then
                    strErrMsg = "错误：成员序号必须在[1,999999]！"
                    GoTo errProc
                End If

                Dim strLXDH As String
                strLXDH = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_LXDH), "")
                If strLXDH = "" Then strLXDH = " "
                With objDataSet.Tables(0).Columns(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_LXDH)
                    intLen = objPulicParameters.getStringLength(strLXDH)
                    If intLen > .MaxLength Then
                        strErrMsg = "错误：[联系电话]长度不能超过[" + .MaxLength.ToString() + "]，实际有[" + intLen.ToString() + "]！"
                        GoTo errProc
                    End If
                End With

                Dim strSJHM As String
                strSJHM = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_SJHM), "")
                If strSJHM = "" Then strSJHM = " "
                With objDataSet.Tables(0).Columns(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_SJHM)
                    intLen = objPulicParameters.getStringLength(strSJHM)
                    If intLen > .MaxLength Then
                        strErrMsg = "错误：[手机号码]长度不能超过[" + .MaxLength.ToString() + "]，实际有[" + intLen.ToString() + "]！"
                        GoTo errProc
                    End If
                End With

                Dim strFTPDZ As String
                strFTPDZ = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_FTPDZ), "")
                If strFTPDZ = "" Then strFTPDZ = " "
                With objDataSet.Tables(0).Columns(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_FTPDZ)
                    intLen = objPulicParameters.getStringLength(strFTPDZ)
                    If intLen > .MaxLength Then
                        strErrMsg = "错误：[FTP地址]长度不能超过[" + .MaxLength.ToString() + "]，实际有[" + intLen.ToString() + "]！"
                        GoTo errProc
                    End If
                End With

                Dim strYXDZ As String
                strYXDZ = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_YXDZ), "")
                If strYXDZ = "" Then strYXDZ = " "
                With objDataSet.Tables(0).Columns(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_YXDZ)
                    intLen = objPulicParameters.getStringLength(strYXDZ)
                    If intLen > .MaxLength Then
                        strErrMsg = "错误：[邮箱地址]长度不能超过[" + .MaxLength.ToString() + "]，实际有[" + intLen.ToString() + "]！"
                        GoTo errProc
                    End If
                End With

                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '检查约束
                objListDictionary = New System.Collections.Specialized.ListDictionary
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                        strSQL = ""
                        strSQL = strSQL + " select * from 公文_B_分发范围 "
                        strSQL = strSQL + " where 范围名称 = @fwmc "
                        strSQL = strSQL + " and   成员名称 = @cymc"
                        objListDictionary.Add("@fwmc", strFWMC)
                        objListDictionary.Add("@cymc", strCYMC)
                    Case Else
                        strSQL = ""
                        strSQL = strSQL + " select * from 公文_B_分发范围 "
                        strSQL = strSQL + " where 范围名称 = @fwmc "
                        strSQL = strSQL + " and   成员名称 = @cymc"
                        strSQL = strSQL + " and   流水号   <> @oldlsh"
                        objListDictionary.Add("@fwmc", strFWMC)
                        objListDictionary.Add("@cymc", strCYMC)
                        objListDictionary.Add("@oldlsh", intOldLSH)
                End Select
                If objdacCommon.getDataSetBySQL(strErrMsg, strUserId, strPassword, strSQL, objListDictionary, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strErrMsg = "错误：[" + strCYMC + "]已经存在！"
                    GoTo errProc
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing
                objListDictionary.Clear()

                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                        strSQL = ""
                        strSQL = strSQL + " select * from 公文_B_分发范围 "
                        strSQL = strSQL + " where 范围名称 = @fwmc "
                        strSQL = strSQL + " and   成员位置 = @cywz"
                        objListDictionary.Add("@fwmc", strFWMC)
                        objListDictionary.Add("@cywz", intCYWZ)
                    Case Else
                        strSQL = ""
                        strSQL = strSQL + " select * from 公文_B_分发范围 "
                        strSQL = strSQL + " where 范围名称 = @fwmc "
                        strSQL = strSQL + " and   成员位置 = @cywz"
                        strSQL = strSQL + " and   流水号   <> @oldlsh"
                        objListDictionary.Add("@fwmc", strFWMC)
                        objListDictionary.Add("@cywz", intCYWZ)
                        objListDictionary.Add("@oldlsh", intOldLSH)
                End Select
                If objdacCommon.getDataSetBySQL(strErrMsg, strUserId, strPassword, strSQL, objListDictionary, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strErrMsg = "错误：[" + intCYWZ.ToString() + "]已经存在！"
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

            doVerifyFenfafanweiData = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objListDictionary)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 保存“公文_B_分发范围”的数据(范围成员记录)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
        '     objNewData           ：新数据
        '     blnIsFWCY            ：仅作接口重载使用
        '     objenumEditType      ：编辑类型
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doSaveFenfafanweiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal blnIsFWCY As Boolean, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            doSaveFenfafanweiData = False
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
                    Dim intOldLSH As Integer
                    Dim strFWMC As String
                    Dim strFWBZ As String
                    Dim strCYLX As String
                    Dim strCYMC As String
                    Dim intCYWZ As Integer
                    Dim strLXDH As String
                    Dim strSJHM As String
                    Dim strFTPDZ As String
                    Dim strYXDZ As String
                    With objPulicParameters
                        strFWMC = .getObjectValue(objNewData(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_FWMC), "")
                        strFWBZ = CType(Xydc.Platform.Common.Data.FenfafanweiData.enumFWBZ.CHENGYUAN, Integer).ToString()
                        strCYLX = .getObjectValue(objNewData(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_CYLX), " ")
                        strCYMC = .getObjectValue(objNewData(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_CYMC), " ")
                        intCYWZ = .getObjectValue(objNewData(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_CYWZ), 0)
                        strLXDH = .getObjectValue(objNewData(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_LXDH), " ")
                        strSJHM = .getObjectValue(objNewData(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_SJHM), " ")
                        strFTPDZ = .getObjectValue(objNewData(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_FTPDZ), " ")
                        strYXDZ = .getObjectValue(objNewData(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_YXDZ), " ")
                    End With
                    Select Case objenumEditType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                            strSQL = ""
                            strSQL = strSQL + " insert into 公文_B_分发范围 (范围名称,范围标志,成员类型,成员名称,成员位置,联系电话,手机号码,FTP地址,邮箱地址)"
                            strSQL = strSQL + " values (@fwmc, @fwbz, @cylx, @cymc, @cywz, @lxdh, @sjhm, @ftpdz, @yxdz)"
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@fwmc", strFWMC)
                            objSqlCommand.Parameters.AddWithValue("@fwbz", strFWBZ)
                            objSqlCommand.Parameters.AddWithValue("@cylx", strCYLX)
                            objSqlCommand.Parameters.AddWithValue("@cymc", strCYMC)
                            objSqlCommand.Parameters.AddWithValue("@cywz", intCYWZ)
                            objSqlCommand.Parameters.AddWithValue("@lxdh", strLXDH)
                            objSqlCommand.Parameters.AddWithValue("@sjhm", strSJHM)
                            objSqlCommand.Parameters.AddWithValue("@ftpdz", strFTPDZ)
                            objSqlCommand.Parameters.AddWithValue("@yxdz", strYXDZ)
                        Case Else
                            With objPulicParameters
                                intOldLSH = .getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_LSH), 0)
                            End With
                            strSQL = ""
                            strSQL = strSQL + " update 公文_B_分发范围 set "
                            strSQL = strSQL + "   范围名称 = @fwmc,"
                            strSQL = strSQL + "   范围标志 = @fwbz,"
                            strSQL = strSQL + "   成员类型 = @cylx,"
                            strSQL = strSQL + "   成员名称 = @cymc,"
                            strSQL = strSQL + "   成员位置 = @cywz,"
                            strSQL = strSQL + "   联系电话 = @lxdh,"
                            strSQL = strSQL + "   手机号码 = @sjhm,"
                            strSQL = strSQL + "   FTP地址  = @ftpdz,"
                            strSQL = strSQL + "   邮箱地址 = @yxdz "
                            strSQL = strSQL + " where 流水号 = @oldlsh"
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@fwmc", strFWMC)
                            objSqlCommand.Parameters.AddWithValue("@fwbz", strFWBZ)
                            objSqlCommand.Parameters.AddWithValue("@cylx", strCYLX)
                            objSqlCommand.Parameters.AddWithValue("@cymc", strCYMC)
                            objSqlCommand.Parameters.AddWithValue("@cywz", intCYWZ)
                            objSqlCommand.Parameters.AddWithValue("@lxdh", strLXDH)
                            objSqlCommand.Parameters.AddWithValue("@sjhm", strSJHM)
                            objSqlCommand.Parameters.AddWithValue("@ftpdz", strFTPDZ)
                            objSqlCommand.Parameters.AddWithValue("@yxdz", strYXDZ)
                            objSqlCommand.Parameters.AddWithValue("@oldlsh", intOldLSH)
                    End Select

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

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doSaveFenfafanweiData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 删除“公文_B_分发范围”的数据(范围成员记录)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
        '     blnIsFWCY            ：仅作接口重载使用
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteFenfafanweiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal blnIsFWCY As Boolean) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            doDeleteFenfafanweiData = False
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
                    Dim intLSH As Integer
                    With New Xydc.Platform.Common.Utilities.PulicParameters
                        intLSH = .getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_LSH), 0)
                    End With
                    strSQL = ""
                    strSQL = strSQL + " delete from 公文_B_分发范围 "
                    strSQL = strSQL + " where 流水号 = @lsh"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@lsh", intLSH)

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
            doDeleteFenfafanweiData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取新的“公文_B_分发范围”的成员位置
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strFWMC              ：当前范围名称
        '     intCYWZ              ：新的成员位置(返回)
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getNewCYWZ( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strFWMC As String, _
            ByRef intCYWZ As Integer) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection

            getNewCYWZ = False
            intCYWZ = -1

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strFWMC Is Nothing Then strFWMC = ""
                strFWMC = strFWMC.Trim()
                If strFWMC = "" Then
                    strErrMsg = "错误：未指定范围名称！"
                    GoTo errProc
                End If

                '打开连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取新代码
                Dim strNewCode As String
                If objdacCommon.getNewCode(strErrMsg, objSqlConnection, "成员位置", "范围名称", strFWMC, "公文_B_分发范围", True, strNewCode) = False Then
                    GoTo errProc
                End If

                '返回
                intCYWZ = CType(strNewCode, Integer)

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getNewCYWZ = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 将指定范围内的指定成员位置上移
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objChengyuanData     ：成员数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doChengyuanMoveUp( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objChengyuanData As System.Data.DataRow) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            Dim objListDictionary As System.Collections.Specialized.ListDictionary
            Dim objDataSet As System.Data.DataSet

            '初始化
            doChengyuanMoveUp = False
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()

                '检查
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If objChengyuanData Is Nothing Then
                    strErrMsg = "错误：未传入成员数据！"
                    GoTo errProc
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取信息
                Dim strFWMC As String
                Dim intCYWZ As Integer
                Dim strFWBZ As String
                Dim intLSH As Integer
                With objPulicParameters
                    intLSH = .getObjectValue(objChengyuanData.Item(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_LSH), 0)
                    strFWMC = .getObjectValue(objChengyuanData.Item(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_FWMC), "")
                    intCYWZ = .getObjectValue(objChengyuanData.Item(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_CYWZ), 0)
                    strFWBZ = CType(Xydc.Platform.Common.Data.FenfafanweiData.enumFWBZ.CHENGYUAN, Integer).ToString()
                End With

                '获取上条数据
                Dim strSQL As String
                strSQL = ""
                strSQL = strSQL + " select * from 公文_B_分发范围 "
                strSQL = strSQL + " where 范围名称 = @fwmc"
                strSQL = strSQL + " and   范围标志 = @fwbz"
                strSQL = strSQL + " and   成员位置 < @cywz"
                strSQL = strSQL + " order by 成员位置 desc"
                objListDictionary = New System.Collections.Specialized.ListDictionary
                objListDictionary.Clear()
                objListDictionary.Add("@fwmc", strFWMC)
                objListDictionary.Add("@fwbz", strFWBZ)
                objListDictionary.Add("@cywz", intCYWZ)
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objListDictionary, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    strErrMsg = "错误：已经是第1条！"
                    GoTo errProc
                End If
                Dim intCYWZA As Integer
                Dim intLSHA As Integer
                With objPulicParameters
                    intLSHA = .getObjectValue(objDataSet.Tables(0).Rows(0).Item(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_LSH), 0)
                    intCYWZA = .getObjectValue(objDataSet.Tables(0).Rows(0).Item(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_CYWZ), 0)
                End With
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing
                objListDictionary.Clear()
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objListDictionary)

                '获取临时代码
                Dim intMaxId As Integer
                If Me.getNewCYWZ(strErrMsg, strUserId, strPassword, strFWMC, intMaxId) = False Then
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

                    'intLSHA更改到intMaxId
                    strSQL = ""
                    strSQL = strSQL + " update 公文_B_分发范围 set "
                    strSQL = strSQL + "   成员位置 = @cywz"
                    strSQL = strSQL + " where 流水号 = @lsh"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@cywz", intMaxId)
                    objSqlCommand.Parameters.AddWithValue("@lsh", intLSHA)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    'intLSH更改到intCYWZA
                    strSQL = ""
                    strSQL = strSQL + " update 公文_B_分发范围 set "
                    strSQL = strSQL + "   成员位置 = @cywz"
                    strSQL = strSQL + " where 流水号 = @lsh"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@cywz", intCYWZA)
                    objSqlCommand.Parameters.AddWithValue("@lsh", intLSH)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    'intLSHA更改到intCYWZ
                    strSQL = ""
                    strSQL = strSQL + " update 公文_B_分发范围 set "
                    strSQL = strSQL + "   成员位置 = @cywz"
                    strSQL = strSQL + " where 流水号 = @lsh"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@cywz", intCYWZ)
                    objSqlCommand.Parameters.AddWithValue("@lsh", intLSHA)
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
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objListDictionary)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doChengyuanMoveUp = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objListDictionary)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 将指定范围内的指定成员位置下移
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objChengyuanData     ：成员数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doChengyuanMoveDown( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objChengyuanData As System.Data.DataRow) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            Dim objListDictionary As System.Collections.Specialized.ListDictionary
            Dim objDataSet As System.Data.DataSet

            '初始化
            doChengyuanMoveDown = False
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()

                '检查
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If objChengyuanData Is Nothing Then
                    strErrMsg = "错误：未传入成员数据！"
                    GoTo errProc
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取信息
                Dim strFWMC As String
                Dim intCYWZ As Integer
                Dim strFWBZ As String
                Dim intLSH As Integer
                With objPulicParameters
                    intLSH = .getObjectValue(objChengyuanData.Item(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_LSH), 0)
                    strFWMC = .getObjectValue(objChengyuanData.Item(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_FWMC), "")
                    intCYWZ = .getObjectValue(objChengyuanData.Item(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_CYWZ), 0)
                    strFWBZ = CType(Xydc.Platform.Common.Data.FenfafanweiData.enumFWBZ.CHENGYUAN, Integer).ToString()
                End With

                '获取下条数据
                Dim strSQL As String
                strSQL = ""
                strSQL = strSQL + " select * from 公文_B_分发范围 "
                strSQL = strSQL + " where 范围名称 = @fwmc"
                strSQL = strSQL + " and   范围标志 = @fwbz"
                strSQL = strSQL + " and   成员位置 > @cywz"
                strSQL = strSQL + " order by 成员位置"
                objListDictionary = New System.Collections.Specialized.ListDictionary
                objListDictionary.Clear()
                objListDictionary.Add("@fwmc", strFWMC)
                objListDictionary.Add("@fwbz", strFWBZ)
                objListDictionary.Add("@cywz", intCYWZ)
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objListDictionary, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    strErrMsg = "错误：已经是最后1条！"
                    GoTo errProc
                End If
                Dim intCYWZTo As Integer
                Dim intLSHTo As Integer
                With objPulicParameters
                    intLSHTo = .getObjectValue(objDataSet.Tables(0).Rows(0).Item(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_LSH), 0)
                    intCYWZTo = .getObjectValue(objDataSet.Tables(0).Rows(0).Item(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_CYWZ), 0)
                End With
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing
                objListDictionary.Clear()
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objListDictionary)

                '获取临时代码
                Dim intMaxId As Integer
                If Me.getNewCYWZ(strErrMsg, strUserId, strPassword, strFWMC, intMaxId) = False Then
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

                    'intLSHTo更改到intMaxId
                    strSQL = ""
                    strSQL = strSQL + " update 公文_B_分发范围 set "
                    strSQL = strSQL + "   成员位置 = @cywz"
                    strSQL = strSQL + " where 流水号 = @lsh"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@cywz", intMaxId)
                    objSqlCommand.Parameters.AddWithValue("@lsh", intLSHTo)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    'intLSH更改到intCYWZTo
                    strSQL = ""
                    strSQL = strSQL + " update 公文_B_分发范围 set "
                    strSQL = strSQL + "   成员位置 = @cywz"
                    strSQL = strSQL + " where 流水号 = @lsh"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@cywz", intCYWZTo)
                    objSqlCommand.Parameters.AddWithValue("@lsh", intLSH)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    'intLSHTo更改到intCYWZ
                    strSQL = ""
                    strSQL = strSQL + " update 公文_B_分发范围 set "
                    strSQL = strSQL + "   成员位置 = @cywz"
                    strSQL = strSQL + " where 流水号 = @lsh"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@cywz", intCYWZ)
                    objSqlCommand.Parameters.AddWithValue("@lsh", intLSHTo)
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
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objListDictionary)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doChengyuanMoveDown = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objListDictionary)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 将指定范围内的指定成员objChengyuanData位置移动到objChengyuanDataTo
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objChengyuanData     ：准备移动的成员数据
        '     objChengyuanDataTo   ：移动到的成员数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doChengyuanMoveTo( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objChengyuanData As System.Data.DataRow, _
            ByVal objChengyuanDataTo As System.Data.DataRow) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            doChengyuanMoveTo = False
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()

                '检查
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If objChengyuanData Is Nothing Then
                    strErrMsg = "错误：未传入成员数据！"
                    GoTo errProc
                End If
                If objChengyuanDataTo Is Nothing Then
                    strErrMsg = "错误：未传入成员数据！"
                    GoTo errProc
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取信息
                Dim strFWMC As String
                Dim intCYWZ As Integer
                Dim intLSH As Integer
                With objPulicParameters
                    intLSH = .getObjectValue(objChengyuanData.Item(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_LSH), 0)
                    strFWMC = .getObjectValue(objChengyuanData.Item(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_FWMC), "")
                    intCYWZ = .getObjectValue(objChengyuanData.Item(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_CYWZ), 0)
                End With

                '获取下条数据
                Dim strSQL As String
                Dim intCYWZTo As Integer
                Dim intLSHTo As Integer
                With objPulicParameters
                    intLSHTo = .getObjectValue(objChengyuanDataTo.Item(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_LSH), 0)
                    intCYWZTo = .getObjectValue(objChengyuanDataTo.Item(Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_CYWZ), 0)
                End With

                '获取临时代码
                Dim intMaxId As Integer
                If Me.getNewCYWZ(strErrMsg, strUserId, strPassword, strFWMC, intMaxId) = False Then
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

                    'intLSHTo更改到intMaxId
                    strSQL = ""
                    strSQL = strSQL + " update 公文_B_分发范围 set "
                    strSQL = strSQL + "   成员位置 = @cywz"
                    strSQL = strSQL + " where 流水号 = @lsh"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@cywz", intMaxId)
                    objSqlCommand.Parameters.AddWithValue("@lsh", intLSHTo)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    'intLSH更改到intCYWZTo
                    strSQL = ""
                    strSQL = strSQL + " update 公文_B_分发范围 set "
                    strSQL = strSQL + "   成员位置 = @cywz"
                    strSQL = strSQL + " where 流水号 = @lsh"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@cywz", intCYWZTo)
                    objSqlCommand.Parameters.AddWithValue("@lsh", intLSH)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    'intLSHTo更改到intCYWZ
                    strSQL = ""
                    strSQL = strSQL + " update 公文_B_分发范围 set "
                    strSQL = strSQL + "   成员位置 = @cywz"
                    strSQL = strSQL + " where 流水号 = @lsh"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@cywz", intCYWZ)
                    objSqlCommand.Parameters.AddWithValue("@lsh", intLSHTo)
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
            doChengyuanMoveTo = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

    End Class

End Namespace
