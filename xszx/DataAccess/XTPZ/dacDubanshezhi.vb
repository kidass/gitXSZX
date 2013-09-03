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
    ' 类名    ：dacDubanshezhi
    '
    ' 功能描述：
    '     提供对系统配置相关表：“管理_B_督办设置”等数据的
    '     增加、修改、删除、检索等操作
    '----------------------------------------------------------------

    Public Class dacDubanshezhi
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.DataAccess.dacDubanshezhi)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub








        '----------------------------------------------------------------
        ' 获取“管理_B_督办设置”的SQL语句(以岗位代码升序排序)
        ' 返回
        '                          ：SQL
        '----------------------------------------------------------------
        Public Function getMainSQL() As String
            getMainSQL = "select * from 管理_B_督办设置 order by 岗位代码"
        End Function

        '----------------------------------------------------------------
        ' 获取“管理_B_督办设置”的数据集(以岗位代码升序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWhere             ：搜索字符串
        '     objDubanshezhiData   ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objDubanshezhiData As Xydc.Platform.Common.Data.DubanshezhiData) As Boolean

            Dim objTempDubanshezhiData As Xydc.Platform.Common.Data.DubanshezhiData
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '初始化
            getDataSet = False
            objDubanshezhiData = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strWhere.Length > 0 Then strWhere = strWhere.Trim()
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
                    objTempDubanshezhiData = New Xydc.Platform.Common.Data.DubanshezhiData(Xydc.Platform.Common.Data.DubanshezhiData.enumTableType.GL_B_DUBANSHEZHI)

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
                        strSQL = strSQL + "     b.岗位名称," + vbCr
                        strSQL = strSQL + "     督办范围名称 = case when a.督办范围 = 0 then '整个单位'" + vbCr
                        strSQL = strSQL + "                         when a.督办范围 = 1 then '指定级数以下部门'" + vbCr
                        strSQL = strSQL + "                         when a.督办范围 = 2 then '本部门以及下级部门'" + vbCr
                        strSQL = strSQL + "                         else ' ' end," + vbCr
                        strSQL = strSQL + "     级数限制名称 = case when a.级数限制 = 1 then '限一级单位以下'" + vbCr
                        strSQL = strSQL + "                         when a.级数限制 = 2 then '限二级单位以下'" + vbCr
                        strSQL = strSQL + "                         when a.级数限制 = 3 then '限三级单位以下'" + vbCr
                        strSQL = strSQL + "                         when a.级数限制 = 4 then '限四级单位以下'" + vbCr
                        strSQL = strSQL + "                         when a.级数限制 = 5 then '限五级单位以下'" + vbCr
                        strSQL = strSQL + "                         when a.级数限制 = 6 then '限六级单位以下'" + vbCr
                        strSQL = strSQL + "                         else ' ' end" + vbCr
                        strSQL = strSQL + "   from 管理_B_督办设置 a" + vbCr
                        strSQL = strSQL + "   left join 公共_B_工作岗位 b on a.岗位代码 = b.岗位代码" + vbCr
                        strSQL = strSQL + " ) a" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.岗位代码 " + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempDubanshezhiData.Tables(Xydc.Platform.Common.Data.DubanshezhiData.TABLE_GL_B_DUBANSHEZHI))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDubanshezhiData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.DubanshezhiData.SafeRelease(objTempDubanshezhiData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objDubanshezhiData = objTempDubanshezhiData
            getDataSet = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.DubanshezhiData.SafeRelease(objTempDubanshezhiData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 检查“管理_B_督办设置”的数据的合法性
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
        Public Function doVerifyData( _
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

            doVerifyData = False

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
                strSQL = "select top 0 * from 管理_B_督办设置"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, strUserId, strPassword, strSQL, "管理_B_督办设置", objDataSet) = False Then
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
                        Case Xydc.Platform.Common.Data.DubanshezhiData.FIELD_GL_B_DUBANSHEZHI_GWMC, _
                            Xydc.Platform.Common.Data.DubanshezhiData.FIELD_GL_B_DUBANSHEZHI_DBFWMC, _
                            Xydc.Platform.Common.Data.DubanshezhiData.FIELD_GL_B_DUBANSHEZHI_JSXZMC
                            '不检查

                        Case Xydc.Platform.Common.Data.DubanshezhiData.FIELD_GL_B_DUBANSHEZHI_DBFW, _
                            Xydc.Platform.Common.Data.DubanshezhiData.FIELD_GL_B_DUBANSHEZHI_JSXZ
                            '数字检查
                            strValue = objPulicParameters.getObjectValue(objDictionaryEntry.Value, "")
                            If strValue = "" Then
                                strErrMsg = "错误：[" + strField + "]必须输入！"
                                GoTo errProc
                            End If
                            If objPulicParameters.isIntegerString(strValue) = False Then
                                strErrMsg = "错误：[" + strField + "]必须是数字！"
                                GoTo errProc
                            End If
                            objDictionaryEntry.Value = strValue

                        Case Xydc.Platform.Common.Data.DubanshezhiData.FIELD_GL_B_DUBANSHEZHI_GWDM
                            strValue = objPulicParameters.getObjectValue(objDictionaryEntry.Value, "")
                            If strValue = "" Then
                                strErrMsg = "错误：[" + strField + "]必须输入！"
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
                            End If
                    End Select
                Next
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '检查唯一性
                Dim strNewGWDM As String
                Dim strOldGWDM As String
                strNewGWDM = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.DubanshezhiData.FIELD_GL_B_DUBANSHEZHI_GWDM), "")
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew, Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eCpyNew
                        strSQL = "select * from 管理_B_督办设置 where 岗位代码 = '" + strNewGWDM + "'"
                    Case Else
                        strOldGWDM = objPulicParameters.getObjectValue(objOldData(Xydc.Platform.Common.Data.DubanshezhiData.FIELD_GL_B_DUBANSHEZHI_GWDM), "")
                        strSQL = "select * from 管理_B_督办设置 where 岗位代码 = '" + strNewGWDM + "' and 岗位代码 <> '" + strOldGWDM + "'"
                End Select
                If objdacCommon.getDataSetBySQL(strErrMsg, strUserId, strPassword, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strErrMsg = "错误：[督办人职务]已经存在，请换一个职务！"
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objListDictionary)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            doVerifyData = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objListDictionary)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 保存“管理_B_督办设置”的数据
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
            ByVal objNewData As System.Collections.Specialized.ListDictionary, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '初始化
            doSaveData = False
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
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '计算SQL
                    Dim objDictionaryEntry As System.Collections.DictionaryEntry
                    Dim strOldGWDM As String
                    Dim strFields As String
                    Dim strValues As String
                    Dim strField As String
                    Dim i As Integer
                    Select Case objenumEditType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew, Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eCpyNew
                            '计算字段列表、字段值
                            objSqlCommand.Parameters.Clear()
                            strFields = ""
                            strValues = ""
                            i = 0
                            For Each objDictionaryEntry In objNewData
                                strField = objPulicParameters.getObjectValue(objDictionaryEntry.Key, "")
                                Select Case strField
                                    Case Xydc.Platform.Common.Data.DubanshezhiData.FIELD_GL_B_DUBANSHEZHI_DBFWMC, _
                                        Xydc.Platform.Common.Data.DubanshezhiData.FIELD_GL_B_DUBANSHEZHI_GWMC, _
                                        Xydc.Platform.Common.Data.DubanshezhiData.FIELD_GL_B_DUBANSHEZHI_JSXZMC
                                        '不用提交
                                    Case Else
                                        If strFields = "" Then
                                            strFields = strField
                                            strValues = "@A" + i.ToString()
                                        Else
                                            strFields = strFields + "," + strField
                                            strValues = strValues + "," + "@A" + i.ToString()
                                        End If
                                        Select Case strField
                                            Case Xydc.Platform.Common.Data.DubanshezhiData.FIELD_GL_B_DUBANSHEZHI_DBFW, _
                                                Xydc.Platform.Common.Data.DubanshezhiData.FIELD_GL_B_DUBANSHEZHI_JSXZ
                                                Dim intValue As Integer
                                                intValue = objPulicParameters.getObjectValue(objDictionaryEntry.Value, 0)
                                                objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), intValue)
                                            Case Else
                                                objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), objDictionaryEntry.Value)
                                        End Select
                                End Select
                                i = i + 1
                            Next

                            '准备SQL语句
                            strSQL = ""
                            strSQL = strSQL + " insert into 管理_B_督办设置 (" + vbCr
                            strSQL = strSQL + "   " + strFields + vbCr
                            strSQL = strSQL + " ) values (" + vbCr
                            strSQL = strSQL + "   " + strValues + vbCr
                            strSQL = strSQL + " )" + vbCr

                        Case Else
                            '获取代码
                            strOldGWDM = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.DubanshezhiData.FIELD_GL_B_DUBANSHEZHI_GWDM), "")

                            '计算字段列表、字段值
                            objSqlCommand.Parameters.Clear()
                            strFields = ""
                            i = 0
                            For Each objDictionaryEntry In objNewData
                                strField = objPulicParameters.getObjectValue(objDictionaryEntry.Key, "")
                                Select Case strField
                                    Case Xydc.Platform.Common.Data.DubanshezhiData.FIELD_GL_B_DUBANSHEZHI_DBFWMC, _
                                        Xydc.Platform.Common.Data.DubanshezhiData.FIELD_GL_B_DUBANSHEZHI_JSXZMC, _
                                        Xydc.Platform.Common.Data.DubanshezhiData.FIELD_GL_B_DUBANSHEZHI_GWMC
                                    Case Else
                                        If strFields = "" Then
                                            strFields = strField + " = @A" + i.ToString()
                                        Else
                                            strFields = strFields + "," + strField + " = @A" + i.ToString()
                                        End If
                                        Select Case strField
                                            Case Xydc.Platform.Common.Data.DubanshezhiData.FIELD_GL_B_DUBANSHEZHI_DBFW, _
                                                Xydc.Platform.Common.Data.DubanshezhiData.FIELD_GL_B_DUBANSHEZHI_JSXZ
                                                Dim intValue As Integer
                                                intValue = objPulicParameters.getObjectValue(objDictionaryEntry.Value, 0)
                                                objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), intValue)
                                            Case Else
                                                objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), objDictionaryEntry.Value)
                                        End Select
                                End Select
                                i = i + 1
                            Next
                            objSqlCommand.Parameters.AddWithValue("@oldgwdm", strOldGWDM)

                            '准备SQL语句
                            strSQL = ""
                            strSQL = strSQL + " update 管理_B_督办设置 set " + vbCr
                            strSQL = strSQL + "   " + strFields + vbCr
                            strSQL = strSQL + " where 岗位代码 = @oldgwdm" + vbCr

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
        ' 删除“管理_B_督办设置”的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
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
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            Dim strSQL As String

            '初始化
            doDeleteData = False
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
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
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '计算SQL
                    Dim strOldGWDM As String
                    strOldGWDM = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.DubanshezhiData.FIELD_GL_B_DUBANSHEZHI_GWDM), "")
                    strSQL = ""
                    strSQL = strSQL + " delete from 管理_B_督办设置 "
                    strSQL = strSQL + " where 岗位代码 = @oldgwdm"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@oldgwdm", strOldGWDM)

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
            doDeleteData = True
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
