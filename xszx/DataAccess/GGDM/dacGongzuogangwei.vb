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
    ' 类名    ：dacGongzuogangwei
    '
    ' 功能描述：
    '     提供对“公共_B_工作岗位”数据的增加、修改、删除、检索等操作
    '----------------------------------------------------------------

    Public Class dacGongzuogangwei
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.DataAccess.dacGongzuogangwei)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub









        '----------------------------------------------------------------
        ' 获取“公共_B_工作岗位”的SQL语句(以岗位代码升序排序)
        ' 返回
        '                          ：SQL
        '----------------------------------------------------------------
        Public Function getGongzuogangweiSQL() As String
            getGongzuogangweiSQL = "select * from 公共_B_工作岗位 order by 岗位代码"
        End Function

        '----------------------------------------------------------------
        ' 获取“公共_B_工作岗位”完全数据的数据集(以岗位代码升序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWhere             ：搜索字符串
        '     objGongzuogangweiData：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getGangweiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objGongzuogangweiData As Xydc.Platform.Common.Data.GongzuogangweiData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempGongzuogangweiData As Xydc.Platform.Common.Data.GongzuogangweiData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getGangweiData = False
            objGongzuogangweiData = Nothing
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
                    objTempGongzuogangweiData = New Xydc.Platform.Common.Data.GongzuogangweiData(Xydc.Platform.Common.Data.GongzuogangweiData.enumTableType.GG_B_GONGZUOGANGWEI)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from 公共_B_工作岗位 a " + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.岗位代码 " + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempGongzuogangweiData.Tables(Xydc.Platform.Common.Data.GongzuogangweiData.TABLE_GG_B_GONGZUOGANGWEI))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempGongzuogangweiData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.GongzuogangweiData.SafeRelease(objTempGongzuogangweiData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objGongzuogangweiData = objTempGongzuogangweiData
            getGangweiData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.GongzuogangweiData.SafeRelease(objTempGongzuogangweiData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 检查“公共_B_工作岗位”的数据的合法性
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
        Public Function doVerifyGongzuogangweiData( _
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

            doVerifyGongzuogangweiData = False

            Try
                Dim strOldGWDM As String
                Dim strGWDM As String
                Dim strGWMC As String
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
                        strOldGWDM = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.GongzuogangweiData.FIELD_GG_B_GONGZUOGANGWEI_GWDM), "")
                End Select

                '获取表结构定义
                strSQL = "select top 0 * from 公共_B_工作岗位"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, strUserId, strPassword, strSQL, "公共_B_工作岗位", objDataSet) = False Then
                    GoTo errProc
                End If

                '检查数据长度
                strGWDM = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.GongzuogangweiData.FIELD_GG_B_GONGZUOGANGWEI_GWDM), "")
                If strGWDM = "" Then
                    strErrMsg = "错误：[岗位代码]不能为空！"
                    GoTo errProc
                End If
                With objDataSet.Tables(0).Columns(Xydc.Platform.Common.Data.GongzuogangweiData.FIELD_GG_B_GONGZUOGANGWEI_GWDM)
                    intLen = objPulicParameters.getStringLength(strGWDM)
                    If intLen > .MaxLength Then
                        strErrMsg = "错误：[岗位代码]长度不能超过[" + .MaxLength.ToString() + "]，实际有[" + intLen.ToString() + "]！"
                        GoTo errProc
                    End If
                End With

                strGWMC = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.GongzuogangweiData.FIELD_GG_B_GONGZUOGANGWEI_GWMC), "")
                If strGWMC = "" Then
                    strErrMsg = "错误：[岗位名称]不能为空！"
                    GoTo errProc
                End If
                With objDataSet.Tables(0).Columns(Xydc.Platform.Common.Data.GongzuogangweiData.FIELD_GG_B_GONGZUOGANGWEI_GWMC)
                    intLen = objPulicParameters.getStringLength(strGWMC)
                    If intLen > .MaxLength Then
                        strErrMsg = "错误：[岗位名称]长度不能超过[" + .MaxLength.ToString() + "]，实际有[" + intLen.ToString() + "]！"
                        GoTo errProc
                    End If
                End With

                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '检查约束
                objListDictionary = New System.Collections.Specialized.ListDictionary
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                        strSQL = "select * from 公共_B_工作岗位 where 岗位代码 = @gwdm"
                        objListDictionary.Add("@gwdm", strGWDM)
                    Case Else
                        strSQL = "select * from 公共_B_工作岗位 where 岗位代码 = @gwdm and 岗位代码 <> @oldgwdm"
                        objListDictionary.Add("@gwdm", strGWDM)
                        objListDictionary.Add("@oldgwdm", strOldGWDM)
                End Select
                If objdacCommon.getDataSetBySQL(strErrMsg, strUserId, strPassword, strSQL, objListDictionary, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strErrMsg = "错误：[" + strGWDM + "]已经存在！"
                    GoTo errProc
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing
                objListDictionary.Clear()

                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                        strSQL = "select * from 公共_B_工作岗位 where 岗位名称 = @gwmc"
                        objListDictionary.Add("@gwmc", strGWMC)
                    Case Else
                        strSQL = "select * from 公共_B_工作岗位 where 岗位名称 = @gwmc and 岗位代码 <> @oldgwdm"
                        objListDictionary.Add("@gwmc", strGWMC)
                        objListDictionary.Add("@oldgwdm", strOldGWDM)
                End Select
                If objdacCommon.getDataSetBySQL(strErrMsg, strUserId, strPassword, strSQL, objListDictionary, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strErrMsg = "错误：[" + strGWMC + "]已经存在！"
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

            doVerifyGongzuogangweiData = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objListDictionary)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 保存“公共_B_工作岗位”的数据
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
        Public Function doSaveGongzuogangweiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            doSaveGongzuogangweiData = False
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
                    Dim strOldGWDM As String
                    Dim strGWDM As String
                    Dim strGWMC As String
                    strGWDM = objNewData(Xydc.Platform.Common.Data.GongzuogangweiData.FIELD_GG_B_GONGZUOGANGWEI_GWDM)
                    strGWMC = objNewData(Xydc.Platform.Common.Data.GongzuogangweiData.FIELD_GG_B_GONGZUOGANGWEI_GWMC)
                    Select Case objenumEditType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                            strSQL = ""
                            strSQL = strSQL + " insert into 公共_B_工作岗位 (岗位代码,岗位名称)"
                            strSQL = strSQL + " values (@gwdm, @gwmc)"
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@gwdm", strGWDM)
                            objSqlCommand.Parameters.AddWithValue("@gwmc", strGWMC)
                        Case Else
                            With New Xydc.Platform.Common.Utilities.PulicParameters
                                strOldGWDM = .getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.GongzuogangweiData.FIELD_GG_B_GONGZUOGANGWEI_GWDM), "")
                            End With
                            strSQL = ""
                            strSQL = strSQL + " update 公共_B_工作岗位 set "
                            strSQL = strSQL + "   岗位代码 = @gwdm,"
                            strSQL = strSQL + "   岗位名称 = @gwmc"
                            strSQL = strSQL + " where 岗位代码 = @oldgwdm"
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@gwdm", strGWDM)
                            objSqlCommand.Parameters.AddWithValue("@gwmc", strGWMC)
                            objSqlCommand.Parameters.AddWithValue("@oldgwdm", strOldGWDM)
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

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doSaveGongzuogangweiData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 删除“公共_B_工作岗位”的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteGongzuogangweiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            doDeleteGongzuogangweiData = False
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
                    Dim strOldGWDM As String
                    With New Xydc.Platform.Common.Utilities.PulicParameters
                        strOldGWDM = .getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.GongzuogangweiData.FIELD_GG_B_GONGZUOGANGWEI_GWDM), "")
                    End With
                    strSQL = ""
                    strSQL = strSQL + " delete from 公共_B_工作岗位 "
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

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doDeleteGongzuogangweiData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

    End Class

End Namespace
