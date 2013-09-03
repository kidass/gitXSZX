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
    ' 类名    ：dacLuntan
    '
    ' 功能描述：
    '     提供对“内部讨论”模块涉及的数据层操作
    '----------------------------------------------------------------

    Public Class dacLuntan
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.DataAccess.dacLuntan)
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
        ' 判断strRYDM是否有效？
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     strRYDM                     ：人员代码
        '     blnValid                    ：（返回）=True有效，=False停用
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function isValid( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRYDM As String, _
            ByRef blnValid As Boolean) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '初始化
            isValid = False
            blnValid = False
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
                If strRYDM Is Nothing Then strRYDM = ""
                strRYDM = strRYDM.Trim

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                strSQL = ""
                strSQL = strSQL + " select *" + vbCr
                strSQL = strSQL + " from 个人_B_交流用户" + vbCr
                strSQL = strSQL + " where 人员代码 = '" + strRYDM + "'" + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    Dim intSFYX As Integer
                    With objDataSet.Tables(0).Rows(0)
                        intSFYX = objPulicParameters.getObjectValue(.Item(Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_SFYX), 0)
                        Select Case intSFYX
                            Case 1
                                blnValid = True
                            Case Else
                        End Select
                    End With
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
            isValid = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 判断strRYDM是否注册？
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     strRYDM                     ：人员代码
        '     blnRegister                 ：（返回）=True已注册，=False未注册
        '     strRYNC                     ：如果已注册，返回人员昵称
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function isRegistered( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRYDM As String, _
            ByRef blnRegister As Boolean, _
            ByRef strRYNC As String) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '初始化
            isRegistered = False
            blnRegister = False
            strRYNC = ""
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
                If strRYDM Is Nothing Then strRYDM = ""
                strRYDM = strRYDM.Trim

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                strSQL = ""
                strSQL = strSQL + " select *" + vbCr
                strSQL = strSQL + " from 个人_B_交流用户" + vbCr
                strSQL = strSQL + " where 人员代码 = '" + strRYDM + "'" + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    blnRegister = True
                    strRYNC = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item(Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_RYNC), "")
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
            isRegistered = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 注册交流用户
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserId              ：用户标识
        '     strPassword            ：用户密码
        '     strRYDM                ：人员代码
        '     strRYNC                ：人员昵称
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function doRegister( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRYDM As String, _
            ByVal strRYNC As String) As Boolean

            doRegister = False

            Try
                If Me.doSave_Yonghu(strErrMsg, strUserId, strPassword, strRYDM, strRYNC) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doRegister = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取交流用户数据（按“组织代码”+“人员序号”升序）
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     strWhere                    ：搜索字符串
        '     objLuntanData               ：信息数据集
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_Yonghu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objLuntanData As Xydc.Platform.Common.Data.ggxxLuntanData) As Boolean

            Dim objTempLuntanData As Xydc.Platform.Common.Data.ggxxLuntanData
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '初始化
            getDataSet_Yonghu = False
            objLuntanData = Nothing
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
                    objTempLuntanData = New Xydc.Platform.Common.Data.ggxxLuntanData(Xydc.Platform.Common.Data.ggxxLuntanData.enumTableType.GR_B_JIAOLIUYONGHU)

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
                        strSQL = strSQL + "   select" + vbCr
                        strSQL = strSQL + "     a.人员代码," + vbCr
                        strSQL = strSQL + "     人员昵称 = case when b.人员代码 is null then a.人员名称 else b.人员昵称 end," + vbCr
                        strSQL = strSQL + "     b.是否有效," + vbCr
                        strSQL = strSQL + "     a.组织代码," + vbCr
                        strSQL = strSQL + "     人员序号 = cast(a.人员序号 as integer)," + vbCr
                        strSQL = strSQL + "     a.人员名称," + vbCr
                        strSQL = strSQL + "     有效描述 = case when b.人员代码 is null then @True" + vbCr
                        strSQL = strSQL + "                     when isnull(b.是否有效,0) = 1 then @True" + vbCr
                        strSQL = strSQL + "                     else @False end," + vbCr
                        strSQL = strSQL + "     注册描述 = case when b.人员代码 is null then @False else @True end" + vbCr
                        strSQL = strSQL + "   from 公共_B_人员 a" + vbCr
                        strSQL = strSQL + "   left join 个人_B_交流用户 b on a.人员代码 = b.人员代码" + vbCr
                        strSQL = strSQL + " ) a" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.组织代码,a.人员序号" + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@True", strTrue)
                        objSqlCommand.Parameters.AddWithValue("@False", strFalse)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempLuntanData.Tables(Xydc.Platform.Common.Data.ggxxLuntanData.TABLE_GR_B_JIAOLIUYONGHU))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempLuntanData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.ggxxLuntanData.SafeRelease(objTempLuntanData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objLuntanData = objTempLuntanData
            getDataSet_Yonghu = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.ggxxLuntanData.SafeRelease(objTempLuntanData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据strRYDM获取交流用户数据
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     strRYDM                     ：人员代码
        '     blnUnused                   ：重载用
        '     objLuntanData               ：信息数据集
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_Yonghu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRYDM As String, _
            ByVal blnUnused As Boolean, _
            ByRef objLuntanData As Xydc.Platform.Common.Data.ggxxLuntanData) As Boolean

            Dim objTempLuntanData As Xydc.Platform.Common.Data.ggxxLuntanData
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '初始化
            getDataSet_Yonghu = False
            objLuntanData = Nothing
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
                If strRYDM Is Nothing Then strRYDM = ""
                strRYDM = strRYDM.Trim

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                Try
                    '创建数据集
                    objTempLuntanData = New Xydc.Platform.Common.Data.ggxxLuntanData(Xydc.Platform.Common.Data.ggxxLuntanData.enumTableType.GR_B_JIAOLIUYONGHU)

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
                        strSQL = strSQL + "   select" + vbCr
                        strSQL = strSQL + "     a.人员代码," + vbCr
                        strSQL = strSQL + "     人员昵称 = case when b.人员代码 is null then a.人员名称 else b.人员昵称 end," + vbCr
                        strSQL = strSQL + "     b.是否有效," + vbCr
                        strSQL = strSQL + "     a.组织代码," + vbCr
                        strSQL = strSQL + "     人员序号 = cast(a.人员序号 as integer)," + vbCr
                        strSQL = strSQL + "     a.人员名称," + vbCr
                        strSQL = strSQL + "     有效描述 = case when b.人员代码 is null then @True" + vbCr
                        strSQL = strSQL + "                     when isnull(b.是否有效,0) = 1 then @True" + vbCr
                        strSQL = strSQL + "                     else @False end," + vbCr
                        strSQL = strSQL + "     注册描述 = case when b.人员代码 is null then @False else @True end" + vbCr
                        strSQL = strSQL + "   from 公共_B_人员 a" + vbCr
                        strSQL = strSQL + "   left join 个人_B_交流用户 b on a.人员代码 = b.人员代码" + vbCr
                        strSQL = strSQL + " ) a" + vbCr
                        strSQL = strSQL + " where a.人员代码 = @rydm" + vbCr
                        strSQL = strSQL + " order by a.组织代码,a.人员序号" + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@True", strTrue)
                        objSqlCommand.Parameters.AddWithValue("@False", strFalse)
                        objSqlCommand.Parameters.AddWithValue("@rydm", strRYDM)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempLuntanData.Tables(Xydc.Platform.Common.Data.ggxxLuntanData.TABLE_GR_B_JIAOLIUYONGHU))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempLuntanData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.ggxxLuntanData.SafeRelease(objTempLuntanData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objLuntanData = objTempLuntanData
            getDataSet_Yonghu = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.ggxxLuntanData.SafeRelease(objTempLuntanData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 检查交流用户的数据的合法性
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
        Public Function doVerify_Yonghu( _
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

            doVerify_Yonghu = False

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
                strSQL = "select top 0 * from 个人_B_交流用户"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, strUserId, strPassword, strSQL, "个人_B_交流用户", objDataSet) = False Then
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
                        Case Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_ZZDM, _
                            Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_RYXH, _
                            Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_RYMC, _
                            Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_YXMS, _
                            Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_ZCMS
                            '计算列

                        Case Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_SFYX
                            If strValue = "" Then
                                strValue = "0"
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

                '校验“人员代码”
                Dim strRYDM As String = objNewData(Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_RYDM)
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew, _
                        Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eCpyNew
                        strSQL = ""
                        strSQL = strSQL + " select * from 个人_B_交流用户" + vbCr
                        strSQL = strSQL + " where 人员代码 = '" + strRYDM + "'" + vbCr
                    Case Else
                        Dim strOldRydm As String
                        strOldRydm = objPulicParameters.getObjectValue(objOldData(Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_RYDM), "")
                        strSQL = ""
                        strSQL = strSQL + " select * from 个人_B_交流用户" + vbCr
                        strSQL = strSQL + " where 人员代码 =  '" + strRYDM + "'" + vbCr
                        strSQL = strSQL + " and   人员代码 <> '" + strOldRydm + "'" + vbCr
                End Select
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strErrMsg = "错误：[人员代码]已经存在！"
                    GoTo errProc
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '校验“人员昵称”
                Dim strRYNC As String = objNewData(Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_RYNC)
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew, _
                        Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eCpyNew
                        strSQL = ""
                        strSQL = strSQL + " select * from 个人_B_交流用户" + vbCr
                        strSQL = strSQL + " where 人员昵称 = '" + strRYNC + "'" + vbCr
                    Case Else
                        Dim strOldRydm As String
                        strOldRydm = objPulicParameters.getObjectValue(objOldData(Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_RYDM), "")
                        strSQL = ""
                        strSQL = strSQL + " select * from 个人_B_交流用户" + vbCr
                        strSQL = strSQL + " where 人员昵称 =  '" + strRYNC + "'" + vbCr
                        strSQL = strSQL + " and   人员代码 <> '" + strOldRydm + "'" + vbCr
                End Select
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strErrMsg = "错误：[人员昵称]已经存在，请换一个！"
                    GoTo errProc
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            doVerify_Yonghu = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 保存交流用户的数据(现有事务)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objSqlTransaction    ：现有事务
        '     objOldData           ：旧数据
        '     objNewData           ：新数据
        '     objenumEditType      ：编辑类型
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doSave_Yonghu( _
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
            doSave_Yonghu = False
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
                                    Case Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_ZZDM, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_RYXH, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_RYMC, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_YXMS, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_ZCMS
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
                            strSQL = strSQL + " insert into 个人_B_交流用户 (" + strFileds + ")"
                            strSQL = strSQL + " values (" + strValues + ")"
                            '准备参数
                            objSqlCommand.Parameters.Clear()
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_ZZDM, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_RYXH, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_RYMC, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_YXMS, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_ZCMS
                                        '计算列
                                    Case Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_SFYX
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
                            Dim strOldRydm As String
                            strOldRydm = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_RYDM), "")
                            '计算更新字段列表
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_ZZDM, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_RYXH, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_RYMC, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_YXMS, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_ZCMS
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
                            strSQL = strSQL + " update 个人_B_交流用户 set " + vbCr
                            strSQL = strSQL + "   " + strFileds + vbCr
                            strSQL = strSQL + " where 人员代码 = @oldxh" + vbCr
                            '准备参数
                            objSqlCommand.Parameters.Clear()
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_ZZDM, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_RYXH, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_RYMC, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_YXMS, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_ZCMS
                                        '计算列
                                    Case Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_SFYX
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
                            objSqlCommand.Parameters.AddWithValue("@oldxh", strOldRydm)
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
            doSave_Yonghu = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 保存交流用户数据记录(整个事务完成)
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserId              ：用户标识
        '     strPassword            ：用户密码
        '     strRYDM                ：人员代码
        '     strRYNC                ：人员昵称
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function doSave_Yonghu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRYDM As String, _
            ByVal strRYNC As String) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objDataSet As Xydc.Platform.Common.Data.ggxxLuntanData
            Dim strSQL As String

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType
            Dim objNewData As New System.Collections.Specialized.NameValueCollection
            Dim objOldData As System.Data.DataRow

            doSave_Yonghu = False

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
                If strRYDM Is Nothing Then strRYDM = ""
                strRYDM = strRYDM.Trim
                If strRYDM = "" Then
                    strErrMsg = "错误：没有指定要注册的人员！"
                    GoTo errProc
                End If
                If strRYNC Is Nothing Then strRYNC = ""
                strRYNC = strRYNC.Trim
                If strRYNC = "" Or strRYDM = "" Then
                    strErrMsg = "错误：没有指定要注册的人员的昵称！"
                    GoTo errProc
                End If

                '设置新数据
                objNewData.Add(Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_RYDM, strRYDM)
                objNewData.Add(Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_RYNC, strRYNC)

                '是否注册？
                Dim strTemp As String
                Dim blnDo As Boolean
                If Me.isRegistered(strErrMsg, strUserId, strPassword, strRYDM, blnDo, strTemp) = False Then
                    GoTo errProc
                End If
                If blnDo = False Then
                    objenumEditType = Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                    objOldData = Nothing

                    objNewData.Add(Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_SFYX, "1")
                Else
                    objenumEditType = Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eUpdate
                    '获取注册数据
                    If Me.getDataSet_Yonghu(strErrMsg, strUserId, strPassword, strRYDM, True, objDataSet) = False Then
                        GoTo errProc
                    End If
                    If objDataSet.Tables(Xydc.Platform.Common.Data.ggxxLuntanData.TABLE_GR_B_JIAOLIUYONGHU) Is Nothing Then
                        strErrMsg = "错误：无法获取[" + strRYDM + "]的注册数据！"
                        GoTo errProc
                    End If
                    With objDataSet.Tables(Xydc.Platform.Common.Data.ggxxLuntanData.TABLE_GR_B_JIAOLIUYONGHU)
                        If .Rows.Count < 1 Then
                            strErrMsg = "错误：无法获取[" + strRYDM + "]的注册数据！"
                            GoTo errProc
                        End If
                        objOldData = .Rows(0)
                    End With

                    strTemp = objPulicParameters.getObjectValue(objOldData(Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_SFYX), "")
                    objNewData.Add(Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_SFYX, strTemp)
                End If

                '检查主记录
                If Me.doVerify_Yonghu(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType) = False Then
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
                    If Me.doSave_Yonghu(strErrMsg, objSqlTransaction, objOldData, objNewData, objenumEditType) = False Then
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
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objNewData)
            Xydc.Platform.Common.Data.ggxxLuntanData.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            doSave_Yonghu = True
            Exit Function

rollDatabase:
            objSqlTransaction.Rollback()
            GoTo errProc

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objNewData)
            Xydc.Platform.Common.Data.ggxxLuntanData.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 删除交流用户
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strRYDM              ：人员代码
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDelete_Yonghu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRYDM As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '初始化
            doDelete_Yonghu = False
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
                If strRYDM Is Nothing Then strRYDM = ""
                strRYDM = strRYDM.Trim
                If strRYDM = "" Then
                    strErrMsg = "错误：未指定[人员代码]！"
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

                    '删除“个人_B_交流用户”信息
                    strSQL = ""
                    strSQL = strSQL + " delete from 个人_B_交流用户 " + vbCr
                    strSQL = strSQL + " where 人员代码 = @rydm" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@rydm", strRYDM)
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
            doDelete_Yonghu = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 停用/启用交流用户
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strRYDM              ：人员代码
        '     blnValid             ：True-启用，False-停用
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doValid_Yonghu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRYDM As String, _
            ByVal blnValid As Boolean) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '初始化
            doValid_Yonghu = False
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
                If strRYDM Is Nothing Then strRYDM = ""
                strRYDM = strRYDM.Trim
                If strRYDM = "" Then
                    strErrMsg = "错误：未指定[人员代码]！"
                    GoTo errProc
                End If

                '检查是否存在
                Dim strRync As String
                Dim blnDo As Boolean
                If Me.isRegistered(strErrMsg, strUserId, strPassword, strRYDM, blnDo, strRync) = False Then
                    GoTo errProc
                End If

                Dim intSfyx As Integer
                If blnValid = True Then
                    intSfyx = 1
                Else
                    intSfyx = 0
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '开始事务
                objSqlTransaction = objSqlConnection.BeginTransaction()

                '处理
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    If blnDo = True Then
                        strSQL = ""
                        strSQL = strSQL + " update 个人_B_交流用户 set" + vbCr
                        strSQL = strSQL + "   是否有效 = @sfyx" + vbCr
                        strSQL = strSQL + " where 人员代码 = @rydm" + vbCr
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@sfyx", intSfyx)
                        objSqlCommand.Parameters.AddWithValue("@rydm", strRYDM)
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.ExecuteNonQuery()
                    Else
                        strSQL = ""
                        strSQL = strSQL + " insert into 个人_B_交流用户 (" + vbCr
                        strSQL = strSQL + "   人员代码,人员昵称,是否有效" + vbCr
                        strSQL = strSQL + " )" + vbCr
                        strSQL = strSQL + " select" + vbCr
                        strSQL = strSQL + "   人员代码,人员昵称=人员名称,是否有效=@sfyx" + vbCr
                        strSQL = strSQL + " from 公共_B_人员" + vbCr
                        strSQL = strSQL + " where 人员代码 = @rydm"
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@sfyx", intSfyx)
                        objSqlCommand.Parameters.AddWithValue("@rydm", strRYDM)
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.ExecuteNonQuery()
                    End If

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
            doValid_Yonghu = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function







        '----------------------------------------------------------------
        ' 删除交流数据(全部清除)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDelete_Jiaoliu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '初始化
            doDelete_Jiaoliu = False
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

                    '删除“个人_B_交流记录”信息
                    strSQL = ""
                    strSQL = strSQL + " delete from 个人_B_交流记录 " + vbCr
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

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doDelete_Jiaoliu = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 删除交流数据：指定时间段
        ' 指定strQSRQ，strJSRQ：strQSRQ <= 发表日期 <= strJSRQ
        ' 指定strQSRQ         ：strQSRQ <= 发表日期
        ' 指定strJSRQ         ：发表日期 <= strJSRQ
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strQSRQ              ：开始日期
        '     strJSRQ              ：结束日期
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDelete_Jiaoliu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strQSRQ As String, _
            ByVal strJSRQ As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '初始化
            doDelete_Jiaoliu = False
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
                If strQSRQ Is Nothing Then strQSRQ = ""
                strQSRQ = strQSRQ.Trim
                If strJSRQ Is Nothing Then strJSRQ = ""
                strJSRQ = strJSRQ.Trim
                If strJSRQ = "" And strQSRQ = "" Then
                    Exit Try
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

                    '删除“个人_B_交流记录”信息
                    strSQL = ""
                    strSQL = strSQL + " delete from 个人_B_交流记录 " + vbCr
                    If strQSRQ <> "" And strJSRQ <> "" Then
                        strSQL = strSQL + " where convert(varchar(10),发表日期,120) between '" + strQSRQ + "' and '" + strJSRQ + "'" + vbCr
                    ElseIf strQSRQ <> "" Then
                        strSQL = strSQL + " where convert(varchar(10),发表日期,120) >= '" + strQSRQ + "'" + vbCr
                    ElseIf strJSRQ <> "" Then
                        strSQL = strSQL + " where convert(varchar(10),发表日期,120) <= '" + strJSRQ + "'" + vbCr
                    Else
                    End If
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

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doDelete_Jiaoliu = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 删除交流数据(指定记录)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     intJLBH              ：交流编号
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDelete_Jiaoliu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intJLBH As Integer) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '初始化
            doDelete_Jiaoliu = False
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

                    '删除主题下的讨论记录信息
                    strSQL = ""
                    strSQL = strSQL + " delete 个人_B_交流记录" + vbCr
                    strSQL = strSQL + " from 个人_B_交流记录 a" + vbCr
                    strSQL = strSQL + " left join" + vbCr
                    strSQL = strSQL + " ("
                    strSQL = strSQL + "   select 交流编号" + vbCr
                    strSQL = strSQL + "   from 个人_B_交流记录" + vbCr
                    strSQL = strSQL + "   where 上级编号 = @jlbh" + vbCr
                    strSQL = strSQL + "   and   交流级别 > 1" + vbCr
                    strSQL = strSQL + " ) b on a.交流编号 = b.交流编号" + vbCr
                    strSQL = strSQL + " where b.交流编号 is not null" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@jlbh", intJLBH)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '删除“个人_B_交流记录”信息
                    strSQL = ""
                    strSQL = strSQL + " delete from 个人_B_交流记录 " + vbCr
                    strSQL = strSQL + " where 交流编号 = @jlbh" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@jlbh", intJLBH)
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
            doDelete_Jiaoliu = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取交流主题数据(按“交流数目”降序)
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     strWhere                    ：搜索条件
        '     objLuntanData               ：信息数据集
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_Jiaoliu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objLuntanData As Xydc.Platform.Common.Data.ggxxLuntanData) As Boolean

            Dim objTempLuntanData As Xydc.Platform.Common.Data.ggxxLuntanData
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '初始化
            getDataSet_Jiaoliu = False
            objLuntanData = Nothing
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
                    objTempLuntanData = New Xydc.Platform.Common.Data.ggxxLuntanData(Xydc.Platform.Common.Data.ggxxLuntanData.enumTableType.GR_B_JIAOLIUJILU)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select" + vbCr
                        strSQL = strSQL + "   a.*" + vbCr
                        strSQL = strSQL + " from" + vbCr
                        strSQL = strSQL + " (" + vbCr
                        strSQL = strSQL + "   select a.*," + vbCr
                        strSQL = strSQL + "     b.人员名称," + vbCr
                        strSQL = strSQL + "     c.人员昵称," + vbCr
                        strSQL = strSQL + "     交流数目 = dbo.Ggxx_GetZT_Tlsm(a.交流编号)" + vbCr
                        strSQL = strSQL + "   from" + vbCr
                        strSQL = strSQL + "   (" + vbCr
                        strSQL = strSQL + "     select *" + vbCr
                        strSQL = strSQL + "     from 个人_B_交流记录" + vbCr
                        strSQL = strSQL + "     where 交流级别 = 1" + vbCr
                        strSQL = strSQL + "   ) a" + vbCr
                        strSQL = strSQL + "   left join 公共_B_人员     b on a.人员代码 = b.人员代码" + vbCr
                        strSQL = strSQL + "   left join 个人_B_交流用户 c on a.人员代码 = c.人员代码" + vbCr
                        strSQL = strSQL + " ) a" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.交流数目 desc" + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempLuntanData.Tables(Xydc.Platform.Common.Data.ggxxLuntanData.TABLE_GR_B_JIAOLIUJILU))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempLuntanData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.ggxxLuntanData.SafeRelease(objTempLuntanData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objLuntanData = objTempLuntanData
            getDataSet_Jiaoliu = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.ggxxLuntanData.SafeRelease(objTempLuntanData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取主题下的讨论数据(按“发表日期”降序)
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     intJLBH                     ：主题编号
        '     strWhere                    ：搜索条件
        '     objLuntanData               ：信息数据集
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_Jiaoliu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intJLBH As Integer, _
            ByVal strWhere As String, _
            ByRef objLuntanData As Xydc.Platform.Common.Data.ggxxLuntanData) As Boolean

            Dim objTempLuntanData As Xydc.Platform.Common.Data.ggxxLuntanData
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '初始化
            getDataSet_Jiaoliu = False
            objLuntanData = Nothing
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
                    objTempLuntanData = New Xydc.Platform.Common.Data.ggxxLuntanData(Xydc.Platform.Common.Data.ggxxLuntanData.enumTableType.GR_B_JIAOLIUJILU)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select" + vbCr
                        strSQL = strSQL + "   a.*" + vbCr
                        strSQL = strSQL + " from" + vbCr
                        strSQL = strSQL + " (" + vbCr
                        strSQL = strSQL + "   select a.*," + vbCr
                        strSQL = strSQL + "     b.人员名称," + vbCr
                        strSQL = strSQL + "     c.人员昵称," + vbCr
                        strSQL = strSQL + "     交流数目 = dbo.Ggxx_GetZT_Tlsm(a.交流编号)" + vbCr
                        strSQL = strSQL + "   from" + vbCr
                        strSQL = strSQL + "   (" + vbCr
                        strSQL = strSQL + "     select *" + vbCr
                        strSQL = strSQL + "     from 个人_B_交流记录" + vbCr
                        strSQL = strSQL + "     where 上级编号 = @jlbh" + vbCr
                        strSQL = strSQL + "   ) a" + vbCr
                        strSQL = strSQL + "   left join 公共_B_人员     b on a.人员代码 = b.人员代码" + vbCr
                        strSQL = strSQL + "   left join 个人_B_交流用户 c on a.人员代码 = c.人员代码" + vbCr
                        strSQL = strSQL + " ) a" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.发表日期 desc" + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@jlbh", intJLBH)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempLuntanData.Tables(Xydc.Platform.Common.Data.ggxxLuntanData.TABLE_GR_B_JIAOLIUJILU))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempLuntanData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.ggxxLuntanData.SafeRelease(objTempLuntanData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objLuntanData = objTempLuntanData
            getDataSet_Jiaoliu = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.ggxxLuntanData.SafeRelease(objTempLuntanData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取指定主题数据
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     intJLBH                     ：主题编号
        '     objLuntanData               ：信息数据集
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_Jiaoliu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intJLBH As Integer, _
            ByRef objLuntanData As Xydc.Platform.Common.Data.ggxxLuntanData) As Boolean

            Dim objTempLuntanData As Xydc.Platform.Common.Data.ggxxLuntanData
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '初始化
            getDataSet_Jiaoliu = False
            objLuntanData = Nothing
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
                    objTempLuntanData = New Xydc.Platform.Common.Data.ggxxLuntanData(Xydc.Platform.Common.Data.ggxxLuntanData.enumTableType.GR_B_JIAOLIUJILU)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select" + vbCr
                        strSQL = strSQL + "   a.*" + vbCr
                        strSQL = strSQL + " from" + vbCr
                        strSQL = strSQL + " (" + vbCr
                        strSQL = strSQL + "   select a.*," + vbCr
                        strSQL = strSQL + "     b.人员名称," + vbCr
                        strSQL = strSQL + "     c.人员昵称," + vbCr
                        strSQL = strSQL + "     交流数目 = dbo.Ggxx_GetZT_Tlsm(a.交流编号)" + vbCr
                        strSQL = strSQL + "   from" + vbCr
                        strSQL = strSQL + "   (" + vbCr
                        strSQL = strSQL + "     select *" + vbCr
                        strSQL = strSQL + "     from 个人_B_交流记录" + vbCr
                        strSQL = strSQL + "     where 交流编号 = @jlbh" + vbCr
                        strSQL = strSQL + "   ) a" + vbCr
                        strSQL = strSQL + "   left join 公共_B_人员     b on a.人员代码 = b.人员代码" + vbCr
                        strSQL = strSQL + "   left join 个人_B_交流用户 c on a.人员代码 = c.人员代码" + vbCr
                        strSQL = strSQL + " ) a" + vbCr
                        strSQL = strSQL + " order by a.发表日期 desc" + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@jlbh", intJLBH)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempLuntanData.Tables(Xydc.Platform.Common.Data.ggxxLuntanData.TABLE_GR_B_JIAOLIUJILU))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempLuntanData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.ggxxLuntanData.SafeRelease(objTempLuntanData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objLuntanData = objTempLuntanData
            getDataSet_Jiaoliu = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.ggxxLuntanData.SafeRelease(objTempLuntanData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 检查“个人_B_交流记录”的数据的合法性
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
        Public Function doVerify_Jiaoliu( _
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

            doVerify_Jiaoliu = False

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
                strSQL = "select top 0 * from 个人_B_交流记录"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, strUserId, strPassword, strSQL, "个人_B_交流记录", objDataSet) = False Then
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
                        Case Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_RYMC, _
                            Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_RYNC, _
                            Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_JLSM
                            '计算列

                        Case Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_JLBH
                            '自动列

                        Case Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_FBRQ
                            If strValue = "" Then
                                strValue = Format(Now, "yyyy-MM-dd HH:mm:ss")
                            End If
                            If objPulicParameters.isDatetimeString(strValue) = False Then
                                strErrMsg = "错误：[" + strField + "]输入无效的日期！"
                                GoTo errProc
                            End If
                            strValue = Format(CType(strValue, System.DateTime), "yyyy-MM-dd HH:mm:ss")

                        Case Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_RYDM, _
                            Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_JLZT
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

                        Case Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_JLJB, _
                            Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_SJBH
                            If strValue = "" Then
                                strValue = "0"
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

                '检查“上级编号”是否存在？并自动设置“交流级别”
                Dim strSJBH As String
                strSJBH = objNewData.Item(Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_SJBH).Trim()
                Select Case strSJBH
                    Case "0", ""
                        objNewData.Item(Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_JLJB) = "1"
                        objNewData.Item(Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_SJBH) = "0"
                    Case Else
                        strSQL = ""
                        strSQL = strSQL + " select * from 个人_B_交流记录" + vbCr
                        strSQL = strSQL + " where 交流编号 = " + strSJBH + vbCr
                        If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                            GoTo errProc
                        End If
                        If objDataSet.Tables(0).Rows.Count < 1 Then
                            strErrMsg = "错误：上级主题不存在！"
                            GoTo errProc
                        End If
                        Dim intJLJB As Integer
                        With objDataSet.Tables(0).Rows(0)
                            intJLJB = objPulicParameters.getObjectValue(.Item(Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_JLJB), 0)
                        End With
                        objNewData.Item(Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_JLJB) = (intJLJB + 1).ToString
                End Select

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            doVerify_Jiaoliu = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 保存“个人_B_交流记录”的数据(现有事务)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objSqlTransaction    ：现有事务
        '     objOldData           ：旧数据
        '     objNewData           ：新数据
        '     objenumEditType      ：编辑类型
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doSave_Jiaoliu( _
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
            doSave_Jiaoliu = False
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
                                    Case Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_RYMC, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_RYNC, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_JLSM
                                        '计算列
                                    Case Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_JLBH
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
                            strSQL = strSQL + " insert into 个人_B_交流记录 (" + strFileds + ")"
                            strSQL = strSQL + " values (" + strValues + ")"
                            '准备参数
                            objSqlCommand.Parameters.Clear()
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_RYMC, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_RYNC, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_JLSM
                                        '计算列
                                    Case Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_JLBH
                                        '自动列
                                    Case Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_FBRQ
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), System.DBNull.Value)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(objNewData.Item(i), System.DateTime))
                                        End If
                                    Case Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_JLJB, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_SJBH
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
                            '获取原“交流编号”
                            Dim intOldJLBH As Integer
                            intOldJLBH = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_JLBH), 0)
                            '计算更新字段列表
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_RYMC, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_RYNC, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_JLSM
                                        '计算列
                                    Case Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_JLBH
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
                            strSQL = strSQL + " update 个人_B_交流记录 set " + vbCr
                            strSQL = strSQL + "   " + strFileds + vbCr
                            strSQL = strSQL + " where 交流编号 = @oldjlbh" + vbCr
                            '准备参数
                            objSqlCommand.Parameters.Clear()
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_RYMC, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_RYNC, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_JLSM
                                        '计算列
                                    Case Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_JLBH
                                        '自动列
                                    Case Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_FBRQ
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), System.DBNull.Value)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(objNewData.Item(i), System.DateTime))
                                        End If
                                    Case Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_JLJB, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_SJBH
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
                            objSqlCommand.Parameters.AddWithValue("@oldjlbh", intOldJLBH)
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
            doSave_Jiaoliu = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 保存交流记录数据记录(整个事务完成)
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
        Public Function doSave_Jiaoliu( _
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

            doSave_Jiaoliu = False

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
                If Me.doVerify_Jiaoliu(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType) = False Then
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
                    If Me.doSave_Jiaoliu(strErrMsg, objSqlTransaction, objOldData, objNewData, objenumEditType) = False Then
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

            doSave_Jiaoliu = True
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
        ' 根据intJLBH获取交流主题
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     intJLBH                     ：主题编号
        '     strJLZT                     ：(返回)交流主题
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getJlztByJlbh( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intJLBH As Integer, _
            ByRef strJLZT As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objggxxLuntanData As Xydc.Platform.Common.Data.ggxxLuntanData

            '初始化
            getJlztByJlbh = False
            strJLZT = ""
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

                '获取信息
                If Me.getDataSet_Jiaoliu(strErrMsg, strUserId, strPassword, intJLBH, objggxxLuntanData) = False Then
                    GoTo errProc
                End If
                If objggxxLuntanData.Tables(Xydc.Platform.Common.Data.ggxxLuntanData.TABLE_GR_B_JIAOLIUJILU) Is Nothing Then
                    Exit Try
                End If
                With objggxxLuntanData.Tables(Xydc.Platform.Common.Data.ggxxLuntanData.TABLE_GR_B_JIAOLIUJILU)
                    If .Rows.Count < 1 Then
                        Exit Try
                    End If
                    strJLZT = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_JLZT), "")
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Data.ggxxLuntanData.SafeRelease(objggxxLuntanData)

            '返回
            getJlztByJlbh = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Data.ggxxLuntanData.SafeRelease(objggxxLuntanData)
            Exit Function

        End Function

    End Class

End Namespace
