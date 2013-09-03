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
    ' 类名    ：dacXitongpeizhi
    '
    ' 功能描述：
    '     提供对系统配置相关表：“管理_B_系统参数”等数据的
    '     增加、修改、删除、检索等操作
    '----------------------------------------------------------------

    Public Class dacXitongpeizhi
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.DataAccess.dacXitongpeizhi)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub








        '----------------------------------------------------------------
        ' 获取“管理_B_系统参数”的SQL语句(以标识升序排序)
        ' 返回
        '                          ：SQL
        '----------------------------------------------------------------
        Public Function getXitongcanshuSQL() As String
            getXitongcanshuSQL = "select * from 管理_B_系统参数 order by 标识"
        End Function

        '----------------------------------------------------------------
        ' 获取“管理_B_系统参数”的数据集(以标识升序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWhere             ：搜索字符串
        '     objXitongcanshuData  ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getXitongcanshuData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objXitongcanshuData As Xydc.Platform.Common.Data.XitongcanshuData) As Boolean

            Dim objTempXitongcanshuData As Xydc.Platform.Common.Data.XitongcanshuData
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '初始化
            getXitongcanshuData = False
            objXitongcanshuData = Nothing
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
                    objTempXitongcanshuData = New Xydc.Platform.Common.Data.XitongcanshuData(Xydc.Platform.Common.Data.XitongcanshuData.enumTableType.GL_B_XITONGCANSHU)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from 管理_B_系统参数 a " + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.标识 " + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempXitongcanshuData.Tables(Xydc.Platform.Common.Data.XitongcanshuData.TABLE_GL_B_XITONGCANSHU))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempXitongcanshuData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.XitongcanshuData.SafeRelease(objTempXitongcanshuData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objXitongcanshuData = objTempXitongcanshuData
            getXitongcanshuData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.XitongcanshuData.SafeRelease(objTempXitongcanshuData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取“管理_B_系统参数”的数据集(以标识升序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objSqlConnection     ：指定连接
        '     strWhere             ：搜索字符串
        '     objXitongcanshuData  ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getXitongcanshuData( _
            ByRef strErrMsg As String, _
            ByVal objSqlConnection As System.Data.SqlClient.SqlConnection, _
            ByVal strWhere As String, _
            ByRef objXitongcanshuData As Xydc.Platform.Common.Data.XitongcanshuData) As Boolean

            Dim objTempXitongcanshuData As Xydc.Platform.Common.Data.XitongcanshuData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '初始化
            getXitongcanshuData = False
            objXitongcanshuData = Nothing
            strErrMsg = ""

            Try
                If objSqlConnection Is Nothing Then
                    strErrMsg = "错误：[getXitongcanshuData]未指定连接！"
                    GoTo errProc
                End If
                If strWhere.Length > 0 Then strWhere = strWhere.Trim()

                '获取数据
                Dim strSQL As String
                Try
                    '创建数据集
                    objTempXitongcanshuData = New Xydc.Platform.Common.Data.XitongcanshuData(Xydc.Platform.Common.Data.XitongcanshuData.enumTableType.GL_B_XITONGCANSHU)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from 管理_B_系统参数 a " + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.标识 " + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempXitongcanshuData.Tables(Xydc.Platform.Common.Data.XitongcanshuData.TABLE_GL_B_XITONGCANSHU))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempXitongcanshuData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.XitongcanshuData.SafeRelease(objTempXitongcanshuData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objXitongcanshuData = objTempXitongcanshuData
            getXitongcanshuData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.XitongcanshuData.SafeRelease(objTempXitongcanshuData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 检查“管理_B_系统参数”的数据的合法性
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
        '     objNewData           ：新数据(校验完成后的新数据)
        '     objenumEditType      ：编辑类型

        ' 返回
        '     True                 ：合法
        '     False                ：不合法或其他程序错误
        '----------------------------------------------------------------
        Public Function doVerifyXitongcanshuData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByRef objNewData As System.Collections.Specialized.ListDictionary, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objListDictionary As System.Collections.Specialized.ListDictionary
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            doVerifyXitongcanshuData = False

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
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                    Case Else
                        If objOldData Is Nothing Then
                            strErrMsg = "错误：未传入旧的数据！"
                            GoTo errProc
                        End If
                End Select

                '获取表结构定义
                strSQL = "select top 0 * from 管理_B_系统参数"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, strUserId, strPassword, strSQL, "管理_B_系统参数", objDataSet) = False Then
                    GoTo errProc
                End If

                '检查数据长度
                Dim objDictionaryEntry As System.Collections.DictionaryEntry
                Dim strField As String
                Dim strValue As String
                Dim intLen As Integer
                Dim i As Integer = 0
                For Each objDictionaryEntry In objNewData
                    strField = objPulicParameters.getObjectValue(objDictionaryEntry.Key, "")
                    Select Case strField
                        Case Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_BS, _
                            Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_ZFTPMMJM, _
                            Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_CFTPMMJM
                            '不检查

                        Case Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_SFJM
                            '数字检查
                            strValue = objPulicParameters.getObjectValue(objDictionaryEntry.Value, "")
                            If strValue = "" Then strValue = "0"
                            If objPulicParameters.isIntegerString(strValue) = False Then
                                strErrMsg = "错误：[" + strField + "]必须是数字！"
                                GoTo errProc
                            End If
                            objDictionaryEntry.Value = strValue

                        Case Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_ZFTPDK, _
                            Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_CFTPDK
                            '数字检查
                            strValue = objPulicParameters.getObjectValue(objDictionaryEntry.Value, "")
                            If strValue = "" Then strValue = "21"
                            If objPulicParameters.isIntegerString(strValue) = False Then
                                strErrMsg = "错误：[" + strField + "]必须是数字！"
                                GoTo errProc
                            End If
                            With objDataSet.Tables(0).Columns(strField)
                                intLen = objPulicParameters.getStringLength(strValue)
                                If intLen > .MaxLength Then
                                    strErrMsg = "错误：[" + strField + "]长度不能超过[" + .MaxLength.ToString() + "]，实际有[" + intLen.ToString() + "]！"
                                    GoTo errProc
                                End If
                            End With
                            objDictionaryEntry.Value = strValue

                        Case Else
                            '字符串检查
                            strValue = objPulicParameters.getObjectValue(objDictionaryEntry.Value, "")
                            If strValue <> "" Then
                                With objDataSet.Tables(0).Columns(strField)
                                    intLen = objPulicParameters.getStringLength(strValue)
                                    If intLen > .MaxLength Then
                                        strErrMsg = "错误：[" + strField + "]长度不能超过[" + .MaxLength.ToString() + "]，实际有[" + intLen.ToString() + "]！"
                                        GoTo errProc
                                    End If
                                End With
                            Else
                                Select Case strField
                                    Case Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_ZFTPFWQ, _
                                        Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_ZFTPYH
                                        strErrMsg = "错误：[" + strField + "]必须输入！"
                                        GoTo errProc
                                    Case Else
                                End Select
                            End If
                    End Select
                Next
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objListDictionary)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            doVerifyXitongcanshuData = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objListDictionary)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 保存“管理_B_系统参数”的数据
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
        Public Function doSaveXitongcanshuData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.ListDictionary, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            doSaveXitongcanshuData = False
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
                    Dim objDictionaryEntry As System.Collections.DictionaryEntry
                    Dim strFields As String
                    Dim strValues As String
                    Dim strField As String
                    Dim intOldBS As Integer
                    Dim i As Integer
                    Select Case objenumEditType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                            '计算字段列表、字段值
                            strFields = ""
                            strValues = ""
                            i = 0
                            For Each objDictionaryEntry In objNewData
                                strField = objPulicParameters.getObjectValue(objDictionaryEntry.Key, "")
                                Select Case strField
                                    Case Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_BS
                                    Case Else
                                        If strFields = "" Then
                                            strFields = strField
                                        Else
                                            strFields = strFields + "," + strField
                                        End If

                                        If strValues = "" Then
                                            strValues = "@A" + i.ToString()
                                        Else
                                            strValues = strValues + "," + "@A" + i.ToString()
                                        End If
                                End Select
                                i = i + 1
                            Next

                            '准备SQL语句
                            strSQL = ""
                            strSQL = strSQL + " insert into 管理_B_系统参数 (" + vbCr
                            strSQL = strSQL + "   " + strFields + vbCr
                            strSQL = strSQL + " ) values (" + vbCr
                            strSQL = strSQL + "   " + strValues + vbCr
                            strSQL = strSQL + " )" + vbCr

                            '准备有关参数
                            objSqlCommand.Parameters.Clear()
                            i = 0
                            For Each objDictionaryEntry In objNewData
                                strField = objPulicParameters.getObjectValue(objDictionaryEntry.Key, "")
                                Select Case strField
                                    Case Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_BS
                                    Case Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_SFJM
                                        Dim intValue As Integer
                                        intValue = objPulicParameters.getObjectValue(objDictionaryEntry.Value, 0)
                                        objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), intValue)
                                    Case Else
                                        objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), objDictionaryEntry.Value)
                                End Select
                                i = i + 1
                            Next

                        Case Else
                            '获取原标识
                            intOldBS = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_BS), 0)

                            '计算字段列表、字段值
                            strFields = ""
                            i = 0
                            For Each objDictionaryEntry In objNewData
                                strField = objPulicParameters.getObjectValue(objDictionaryEntry.Key, "")
                                Select Case strField
                                    Case Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_BS
                                    Case Else
                                        If strFields = "" Then
                                            strFields = strField + " = @A" + i.ToString()
                                        Else
                                            strFields = strFields + "," + strField + " = @A" + i.ToString()
                                        End If
                                End Select
                                i = i + 1
                            Next

                            '准备SQL语句
                            strSQL = ""
                            strSQL = strSQL + " update 管理_B_系统参数 set " + vbCr
                            strSQL = strSQL + "   " + strFields + vbCr
                            strSQL = strSQL + " where 标识 = @oldbs" + vbCr

                            '准备有关参数
                            objSqlCommand.Parameters.Clear()
                            i = 0
                            For Each objDictionaryEntry In objNewData
                                strField = objPulicParameters.getObjectValue(objDictionaryEntry.Key, "")
                                Select Case strField
                                    Case Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_BS
                                    Case Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_SFJM
                                        Dim intValue As Integer
                                        intValue = objPulicParameters.getObjectValue(objDictionaryEntry.Value, 0)
                                        objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), intValue)
                                    Case Else
                                        objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), objDictionaryEntry.Value)
                                End Select
                                i = i + 1
                            Next
                            objSqlCommand.Parameters.AddWithValue("@oldbs", intOldBS)
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
            doSaveXitongcanshuData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 删除“管理_B_系统参数”的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteXitongcanshuData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            doDeleteXitongcanshuData = False
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
                    Dim intOldBS As Integer
                    intOldBS = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_BS), 0)
                    strSQL = ""
                    strSQL = strSQL + " delete from 管理_B_系统参数 "
                    strSQL = strSQL + " where 标识 = @oldbs"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@oldbs", intOldBS)

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
            doDeleteXitongcanshuData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取系统配置中的FTP服务器参数信息
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objFTPProperty       ：FTP服务器参数(返回)
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getFtpServerParam( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objTempXitongcanshuData As Xydc.Platform.Common.Data.XitongcanshuData
            Dim strSQL As String

            '初始化
            getFtpServerParam = False
            objFTPProperty = Nothing
            strErrMsg = ""

            Try
                '获取数据
                If Me.getXitongcanshuData(strErrMsg, strUserId, strPassword, "", objTempXitongcanshuData) = False Then
                    GoTo errProc
                End If
                If objTempXitongcanshuData.Tables.Count < 1 Then
                    strErrMsg = "错误：没有配置系统运行参数！"
                    GoTo errProc
                End If
                If objTempXitongcanshuData.Tables(Xydc.Platform.Common.Data.XitongcanshuData.TABLE_GL_B_XITONGCANSHU) Is Nothing Then
                    strErrMsg = "错误：没有配置系统运行参数！"
                    GoTo errProc
                End If
                With objTempXitongcanshuData.Tables(Xydc.Platform.Common.Data.XitongcanshuData.TABLE_GL_B_XITONGCANSHU)
                    If .Rows.Count < 1 Then
                        strErrMsg = "错误：没有配置系统运行参数！"
                        GoTo errProc
                    End If
                End With

                '创建对象
                objFTPProperty = New Xydc.Platform.Common.Utilities.FTPProperty

                '返回参数
                Dim strFtpPassword As String = ""
                Dim blnSFJM As Boolean = False
                Dim intSFJM As Integer = 0
                Dim objMM As Byte()
                With objTempXitongcanshuData.Tables(Xydc.Platform.Common.Data.XitongcanshuData.TABLE_GL_B_XITONGCANSHU).Rows(0)
                    '是否加密
                    intSFJM = objPulicParameters.getObjectValue(.Item(Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_SFJM), 0)
                    If intSFJM = 0 Then
                        blnSFJM = False
                    Else
                        blnSFJM = True
                    End If

                    '非加密参数
                    If blnSFJM = False Then
                        strFtpPassword = objPulicParameters.getObjectValue(.Item(Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_ZFTPMM), "")
                    Else
                        Try
                            objMM = CType(.Item(Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_ZFTPMMJM), Byte())
                            If objMM.Length > 0 Then
                                If objPulicParameters.doDecryptString(strErrMsg, objMM, strFtpPassword) = False Then
                                    GoTo errProc
                                End If
                            End If
                        Catch ex As Exception
                            strErrMsg = ex.Message
                            GoTo errProc
                        End Try
                    End If

                    objFTPProperty.ServerName = objPulicParameters.getObjectValue(.Item(Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_ZFTPFWQ), "")
                    objFTPProperty.Port = objPulicParameters.getObjectValue(.Item(Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_ZFTPDK), 21)
                    objFTPProperty.UserID = objPulicParameters.getObjectValue(.Item(Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_ZFTPYH), "")
                    objFTPProperty.Password = strFtpPassword
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Data.XitongcanshuData.SafeRelease(objTempXitongcanshuData)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            '返回
            getFtpServerParam = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.XitongcanshuData.SafeRelease(objTempXitongcanshuData)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.FTPProperty.SafeRelease(objFTPProperty)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取系统配置中的FTP服务器参数信息
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objSqlConnection     ：指定连接
        '     objFTPProperty       ：FTP服务器参数(返回)
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getFtpServerParam( _
            ByRef strErrMsg As String, _
            ByVal objSqlConnection As System.Data.SqlClient.SqlConnection, _
            ByRef objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objTempXitongcanshuData As Xydc.Platform.Common.Data.XitongcanshuData
            Dim strSQL As String

            '初始化
            getFtpServerParam = False
            objFTPProperty = Nothing
            strErrMsg = ""

            Try
                '获取数据
                If Me.getXitongcanshuData(strErrMsg, objSqlConnection, "", objTempXitongcanshuData) = False Then
                    GoTo errProc
                End If
                If objTempXitongcanshuData.Tables.Count < 1 Then
                    strErrMsg = "错误：没有配置系统运行参数！"
                    GoTo errProc
                End If
                If objTempXitongcanshuData.Tables(Xydc.Platform.Common.Data.XitongcanshuData.TABLE_GL_B_XITONGCANSHU) Is Nothing Then
                    strErrMsg = "错误：没有配置系统运行参数！"
                    GoTo errProc
                End If
                With objTempXitongcanshuData.Tables(Xydc.Platform.Common.Data.XitongcanshuData.TABLE_GL_B_XITONGCANSHU)
                    If .Rows.Count < 1 Then
                        strErrMsg = "错误：没有配置系统运行参数！"
                        GoTo errProc
                    End If
                End With

                '创建对象
                objFTPProperty = New Xydc.Platform.Common.Utilities.FTPProperty

                '返回参数
                Dim strFtpPassword As String = ""
                Dim blnSFJM As Boolean = False
                Dim intSFJM As Integer = 0
                Dim objMM As Byte()
                With objTempXitongcanshuData.Tables(Xydc.Platform.Common.Data.XitongcanshuData.TABLE_GL_B_XITONGCANSHU).Rows(0)
                    '是否加密
                    intSFJM = objPulicParameters.getObjectValue(.Item(Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_SFJM), 0)
                    If intSFJM = 0 Then
                        blnSFJM = False
                    Else
                        blnSFJM = True
                    End If

                    '非加密参数
                    If blnSFJM = False Then
                        strFtpPassword = objPulicParameters.getObjectValue(.Item(Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_ZFTPMM), "")
                    Else
                        strFtpPassword = ""
                        Try
                            objMM = CType(.Item(Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_ZFTPMMJM), Byte())
                            If objMM.Length > 0 Then
                                If objPulicParameters.doDecryptString(strErrMsg, objMM, strFtpPassword) = False Then
                                    GoTo errProc
                                End If
                            End If
                        Catch ex As Exception
                            strErrMsg = ex.Message
                            GoTo errProc
                        End Try
                    End If

                    objFTPProperty.ServerName = objPulicParameters.getObjectValue(.Item(Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_ZFTPFWQ), "")
                    objFTPProperty.Port = objPulicParameters.getObjectValue(.Item(Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_ZFTPDK), 21)
                    objFTPProperty.UserID = objPulicParameters.getObjectValue(.Item(Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_ZFTPYH), "")
                    objFTPProperty.Password = strFtpPassword
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Data.XitongcanshuData.SafeRelease(objTempXitongcanshuData)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            '返回
            getFtpServerParam = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.XitongcanshuData.SafeRelease(objTempXitongcanshuData)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.FTPProperty.SafeRelease(objFTPProperty)
            Exit Function

        End Function

    End Class


End Namespace
