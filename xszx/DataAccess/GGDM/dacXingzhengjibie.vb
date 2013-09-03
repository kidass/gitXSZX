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
    ' 类名    ：dacXingzhengjibie
    '
    ' 功能描述：
    '     提供对“公共_B_行政级别”数据的增加、修改、删除、检索等操作
    '----------------------------------------------------------------

    Public Class dacXingzhengjibie
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.DataAccess.dacXingzhengjibie)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub









        '----------------------------------------------------------------
        ' 获取“公共_B_行政级别”的SQL语句(以级别代码升序排序)
        ' 返回
        '                          ：SQL
        '----------------------------------------------------------------
        Public Function getXingzhengjibieSQL() As String
            getXingzhengjibieSQL = "select * from 公共_B_行政级别 order by 级别代码"
        End Function

        '----------------------------------------------------------------
        ' 根据级别代码获取“公共_B_行政级别”的数据集
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserId              ：用户标识
        '     strPassword            ：用户密码
        '     strJBDM                ：级别代码
        '     blnUnused              ：重载用
        '     objXingzhengjibieData  ：信息数据集
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function getXingzhengjibieData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strJBDM As String, _
            ByVal blnUnused As Boolean, _
            ByRef objXingzhengjibieData As Xydc.Platform.Common.Data.XingzhengjibieData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempXingzhengjibieData As Xydc.Platform.Common.Data.XingzhengjibieData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getXingzhengjibieData = False
            objXingzhengjibieData = Nothing
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strJBDM Is Nothing Then strJBDM = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                strJBDM = strJBDM.Trim()

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
                    objTempXingzhengjibieData = New Xydc.Platform.Common.Data.XingzhengjibieData(Xydc.Platform.Common.Data.XingzhengjibieData.enumTableType.GG_B_XINGZHENGJIBIE)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from 公共_B_行政级别 a " + vbCr
                        strSQL = strSQL + " where 级别代码 = @jbdm " + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@jbdm", strJBDM)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempXingzhengjibieData.Tables(Xydc.Platform.Common.Data.XingzhengjibieData.TABLE_GG_B_XINGZHENGJIBIE))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempXingzhengjibieData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.XingzhengjibieData.SafeRelease(objTempXingzhengjibieData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objXingzhengjibieData = objTempXingzhengjibieData
            getXingzhengjibieData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.XingzhengjibieData.SafeRelease(objTempXingzhengjibieData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据级别名称获取“公共_B_行政级别”的数据集
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserId              ：用户标识
        '     strPassword            ：用户密码
        '     blnUnused              ：重载用
        '     strJBMC                ：级别名称
        '     objXingzhengjibieData  ：信息数据集
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function getXingzhengjibieData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal blnUnused As Boolean, _
            ByVal strJBMC As String, _
            ByRef objXingzhengjibieData As Xydc.Platform.Common.Data.XingzhengjibieData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempXingzhengjibieData As Xydc.Platform.Common.Data.XingzhengjibieData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getXingzhengjibieData = False
            objXingzhengjibieData = Nothing
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strJBMC Is Nothing Then strJBMC = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                strJBMC = strJBMC.Trim()

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
                    objTempXingzhengjibieData = New Xydc.Platform.Common.Data.XingzhengjibieData(Xydc.Platform.Common.Data.XingzhengjibieData.enumTableType.GG_B_XINGZHENGJIBIE)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from 公共_B_行政级别 a " + vbCr
                        strSQL = strSQL + " where 级别名称 = @jbmc " + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@jbmc", strJBMC)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempXingzhengjibieData.Tables(Xydc.Platform.Common.Data.XingzhengjibieData.TABLE_GG_B_XINGZHENGJIBIE))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempXingzhengjibieData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.XingzhengjibieData.SafeRelease(objTempXingzhengjibieData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objXingzhengjibieData = objTempXingzhengjibieData
            getXingzhengjibieData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.XingzhengjibieData.SafeRelease(objTempXingzhengjibieData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取“公共_B_行政级别”的数据集(以代码升序排序)
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserId              ：用户标识
        '     strPassword            ：用户密码
        '     strWhere               ：搜索字符串
        '     objXingzhengjibieData  ：信息数据集
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function getXingzhengjibieData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objXingzhengjibieData As Xydc.Platform.Common.Data.XingzhengjibieData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempXingzhengjibieData As Xydc.Platform.Common.Data.XingzhengjibieData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getXingzhengjibieData = False
            objXingzhengjibieData = Nothing
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
                    objTempXingzhengjibieData = New Xydc.Platform.Common.Data.XingzhengjibieData(Xydc.Platform.Common.Data.XingzhengjibieData.enumTableType.GG_B_XINGZHENGJIBIE)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from 公共_B_行政级别 a " + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.级别代码 " + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempXingzhengjibieData.Tables(Xydc.Platform.Common.Data.XingzhengjibieData.TABLE_GG_B_XINGZHENGJIBIE))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempXingzhengjibieData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.XingzhengjibieData.SafeRelease(objTempXingzhengjibieData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objXingzhengjibieData = objTempXingzhengjibieData
            getXingzhengjibieData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.XingzhengjibieData.SafeRelease(objTempXingzhengjibieData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 检查“公共_B_行政级别”的数据的合法性
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
        Public Function doVerifyXingzhengjibieData( _
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

            doVerifyXingzhengjibieData = False

            Try
                Dim strOldJBDM As String
                Dim strJBDM As String
                Dim strJBMC As String
                Dim intXZJB As Integer
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
                        strOldJBDM = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.XingzhengjibieData.FIELD_GG_B_XINGZHENGJIBIE_JBDM), "")
                End Select

                '获取表结构定义
                strSQL = "select top 0 * from 公共_B_行政级别"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, strUserId, strPassword, strSQL, "公共_B_行政级别", objDataSet) = False Then
                    GoTo errProc
                End If

                '检查数据长度
                strJBDM = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.XingzhengjibieData.FIELD_GG_B_XINGZHENGJIBIE_JBDM), "")
                If strJBDM = "" Then
                    strErrMsg = "错误：[级别代码]不能为空！"
                    GoTo errProc
                End If
                With objDataSet.Tables(0).Columns(Xydc.Platform.Common.Data.XingzhengjibieData.FIELD_GG_B_XINGZHENGJIBIE_JBDM)
                    intLen = objPulicParameters.getStringLength(strJBDM)
                    If intLen > .MaxLength Then
                        strErrMsg = "错误：[级别代码]长度不能超过[" + .MaxLength.ToString() + "]，实际有[" + intLen.ToString() + "]！"
                        GoTo errProc
                    End If
                End With

                strJBMC = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.XingzhengjibieData.FIELD_GG_B_XINGZHENGJIBIE_JBMC), "")
                If strJBMC = "" Then
                    strErrMsg = "错误：[级别名称]不能为空！"
                    GoTo errProc
                End If
                With objDataSet.Tables(0).Columns(Xydc.Platform.Common.Data.XingzhengjibieData.FIELD_GG_B_XINGZHENGJIBIE_JBMC)
                    intLen = objPulicParameters.getStringLength(strJBMC)
                    If intLen > .MaxLength Then
                        strErrMsg = "错误：[级别名称]长度不能超过[" + .MaxLength.ToString() + "]，实际有[" + intLen.ToString() + "]！"
                        GoTo errProc
                    End If
                End With

                intXZJB = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.XingzhengjibieData.FIELD_GG_B_XINGZHENGJIBIE_XZJB), 0)
                If intXZJB < 0 Or intXZJB > 9999 Then
                    strErrMsg = "错误：[行政级别]长度不能超过[0,9999]，实际有[" + intXZJB.ToString() + "]！"
                    GoTo errProc
                End If

                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '检查约束
                objListDictionary = New System.Collections.Specialized.ListDictionary
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                        strSQL = "select * from 公共_B_行政级别 where 级别代码 = @jbdm"
                        objListDictionary.Add("@jbdm", strJBDM)
                    Case Else
                        strSQL = "select * from 公共_B_行政级别 where 级别代码 = @jbdm and 级别代码 <> @oldjbdm"
                        objListDictionary.Add("@jbdm", strJBDM)
                        objListDictionary.Add("@oldjbdm", strOldJBDM)
                End Select
                If objdacCommon.getDataSetBySQL(strErrMsg, strUserId, strPassword, strSQL, objListDictionary, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strErrMsg = "错误：[" + strJBDM + "]已经存在！"
                    GoTo errProc
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing
                objListDictionary.Clear()

                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                        strSQL = "select * from 公共_B_行政级别 where 级别名称 = @jbmc"
                        objListDictionary.Add("@jbmc", strJBMC)
                    Case Else
                        strSQL = "select * from 公共_B_行政级别 where 级别名称 = @jbmc and 级别代码 <> @oldjbdm"
                        objListDictionary.Add("@jbmc", strJBMC)
                        objListDictionary.Add("@oldjbdm", strOldJBDM)
                End Select
                If objdacCommon.getDataSetBySQL(strErrMsg, strUserId, strPassword, strSQL, objListDictionary, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strErrMsg = "错误：[" + strJBMC + "]已经存在！"
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

            doVerifyXingzhengjibieData = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objListDictionary)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 保存“公共_B_行政级别”的数据
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
        Public Function doSaveXingzhengjibieData( _
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
            doSaveXingzhengjibieData = False
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
                    Dim strOldJBDM As String
                    Dim strJBDM As String
                    Dim strJBMC As String
                    Dim intXZJB As Integer
                    strJBDM = objNewData(Xydc.Platform.Common.Data.XingzhengjibieData.FIELD_GG_B_XINGZHENGJIBIE_JBDM)
                    strJBMC = objNewData(Xydc.Platform.Common.Data.XingzhengjibieData.FIELD_GG_B_XINGZHENGJIBIE_JBMC)
                    With objPulicParameters
                        intXZJB = .getObjectValue(objNewData(Xydc.Platform.Common.Data.XingzhengjibieData.FIELD_GG_B_XINGZHENGJIBIE_XZJB), 0)
                    End With
                    Select Case objenumEditType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                            strSQL = ""
                            strSQL = strSQL + " insert into 公共_B_行政级别 (级别代码,级别名称,行政级别)"
                            strSQL = strSQL + " values (@jbdm, @jbmc, @xzjb)"
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@jbdm", strJBDM)
                            objSqlCommand.Parameters.AddWithValue("@jbmc", strJBMC)
                            objSqlCommand.Parameters.AddWithValue("@xzjb", intXZJB)
                        Case Else
                            With objPulicParameters
                                strOldJBDM = .getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.XingzhengjibieData.FIELD_GG_B_XINGZHENGJIBIE_JBDM), "")
                            End With
                            strSQL = ""
                            strSQL = strSQL + " update 公共_B_行政级别 set "
                            strSQL = strSQL + "   级别代码 = @jbdm,"
                            strSQL = strSQL + "   级别名称 = @jbmc,"
                            strSQL = strSQL + "   行政级别 = @xzjb "
                            strSQL = strSQL + " where 级别代码 = @oldjbdm"
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@jbdm", strJBDM)
                            objSqlCommand.Parameters.AddWithValue("@jbmc", strJBMC)
                            objSqlCommand.Parameters.AddWithValue("@xzjb", intXZJB)
                            objSqlCommand.Parameters.AddWithValue("@oldjbdm", strOldJBDM)
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
            doSaveXingzhengjibieData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 删除“公共_B_行政级别”的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteXingzhengjibieData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            doDeleteXingzhengjibieData = False
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
                    Dim strOldJBDM As String
                    With New Xydc.Platform.Common.Utilities.PulicParameters
                        strOldJBDM = .getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.XingzhengjibieData.FIELD_GG_B_XINGZHENGJIBIE_JBDM), "")
                    End With
                    strSQL = ""
                    strSQL = strSQL + " delete from 公共_B_行政级别 "
                    strSQL = strSQL + " where 级别代码 = @oldjbdm"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@oldjbdm", strOldJBDM)

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
            doDeleteXingzhengjibieData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

    End Class

End Namespace
