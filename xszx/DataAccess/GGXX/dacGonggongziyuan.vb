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
Imports System.Web
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient

Imports Xydc.Platform.Common
Imports Xydc.Platform.Common.Data
Imports Xydc.Platform.SystemFramework

Namespace Xydc.Platform.DataAccess

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.DataAccess
    ' 类名    ：dacGonggongziyuan
    '
    ' 功能描述：
    '     提供对“公共资源”涉及的数据层操作
    '----------------------------------------------------------------

    Public Class dacGonggongziyuan
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.DataAccess.dacGonggongziyuan)
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
        ' 获取“信息_B_公共资源_栏目”的数据集(以“栏目代码”升序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWhere             ：搜索字符串(默认表前缀a.)
        '     objLanmuData         ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getLanmuData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objLanmuData As Xydc.Platform.Common.Data.ggxxGonggongziyuanData) As Boolean

            Dim objTempLanmuData As Xydc.Platform.Common.Data.ggxxGonggongziyuanData
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '初始化
            getLanmuData = False
            objLanmuData = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim()
                If strUserId = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim()
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim()

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                Try
                    '创建数据集
                    objTempLanmuData = New Xydc.Platform.Common.Data.ggxxGonggongziyuanData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.enumTableType.XX_B_GONGGONGZIYUAN_LANMU)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from 信息_B_公共资源_栏目 a " + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.栏目代码 " + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempLanmuData.Tables(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.TABLE_XX_B_GONGGONGZIYUAN_LANMU))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempLanmuData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.ggxxGonggongziyuanData.SafeRelease(objTempLanmuData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objLanmuData = objTempLanmuData
            getLanmuData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.ggxxGonggongziyuanData.SafeRelease(objTempLanmuData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取指定strLMDM下级的“信息_B_公共资源_栏目”的数据集(以“栏目代码”升序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strLMDM              ：栏目代码
        '     strWhere             ：搜索字符串(默认表前缀a.)
        '     objLanmuData         ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getLanmuData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strLMDM As String, _
            ByVal strWhere As String, _
            ByRef objLanmuData As Xydc.Platform.Common.Data.ggxxGonggongziyuanData) As Boolean

            Dim objTempLanmuData As Xydc.Platform.Common.Data.ggxxGonggongziyuanData
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '初始化
            getLanmuData = False
            objLanmuData = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim()
                If strUserId = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim()
                If strLMDM Is Nothing Then strLMDM = ""
                strLMDM = strLMDM.Trim()
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim()

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                Try
                    '创建数据集
                    objTempLanmuData = New Xydc.Platform.Common.Data.ggxxGonggongziyuanData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.enumTableType.XX_B_GONGGONGZIYUAN_LANMU)

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
                        strSQL = strSQL + " from 信息_B_公共资源_栏目 a " + vbCr
                        strSQL = strSQL + " where (a.栏目代码 like @lmdm + '" + strSep + "%' or a.栏目代码 = @lmdm)" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " and " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.栏目代码 " + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@lmdm", strLMDM)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempLanmuData.Tables(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.TABLE_XX_B_GONGGONGZIYUAN_LANMU))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempLanmuData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.ggxxGonggongziyuanData.SafeRelease(objTempLanmuData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objLanmuData = objTempLanmuData
            getLanmuData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.ggxxGonggongziyuanData.SafeRelease(objTempLanmuData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据指定strLMDM获取“信息_B_公共资源_栏目”的数据集
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strLMDM              ：栏目代码
        '     blnUnused            ：重载用
        '     objLanmuData         ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getLanmuData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strLMDM As String, _
            ByVal blnUnused As Boolean, _
            ByRef objLanmuData As Xydc.Platform.Common.Data.ggxxGonggongziyuanData) As Boolean

            Dim objTempLanmuData As Xydc.Platform.Common.Data.ggxxGonggongziyuanData
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '初始化
            getLanmuData = False
            objLanmuData = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim()
                If strUserId = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim()
                If strLMDM Is Nothing Then strLMDM = ""
                strLMDM = strLMDM.Trim()

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                Try
                    '创建数据集
                    objTempLanmuData = New Xydc.Platform.Common.Data.ggxxGonggongziyuanData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.enumTableType.XX_B_GONGGONGZIYUAN_LANMU)

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
                        strSQL = strSQL + " from 信息_B_公共资源_栏目 a " + vbCr
                        strSQL = strSQL + " where a.栏目代码 = @lmdm" + vbCr
                        strSQL = strSQL + " order by a.栏目代码 " + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@lmdm", strLMDM)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempLanmuData.Tables(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.TABLE_XX_B_GONGGONGZIYUAN_LANMU))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempLanmuData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.ggxxGonggongziyuanData.SafeRelease(objTempLanmuData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objLanmuData = objTempLanmuData
            getLanmuData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.ggxxGonggongziyuanData.SafeRelease(objTempLanmuData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据指定intMKBS获取“信息_B_公共资源_栏目”的数据集
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     intMKBS              ：栏目标识
        '     blnUnused            ：重载用
        '     objLanmuData         ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getLanmuData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intMKBS As Integer, _
            ByVal blnUnused As Boolean, _
            ByRef objLanmuData As Xydc.Platform.Common.Data.ggxxGonggongziyuanData) As Boolean

            Dim objTempLanmuData As Xydc.Platform.Common.Data.ggxxGonggongziyuanData
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getLanmuData = False
            objLanmuData = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim()
                If strUserId = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim()

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                Dim strSQL As String
                Try
                    '创建数据集
                    objTempLanmuData = New Xydc.Platform.Common.Data.ggxxGonggongziyuanData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.enumTableType.XX_B_GONGGONGZIYUAN_LANMU)

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
                        strSQL = strSQL + " from 信息_B_公共资源_栏目 a " + vbCr
                        strSQL = strSQL + " where a.栏目标识 = @mkbs" + vbCr
                        strSQL = strSQL + " order by a.栏目代码 " + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@mkbs", intMKBS)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempLanmuData.Tables(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.TABLE_XX_B_GONGGONGZIYUAN_LANMU))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempLanmuData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.ggxxGonggongziyuanData.SafeRelease(objTempLanmuData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objLanmuData = objTempLanmuData
            getLanmuData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.ggxxGonggongziyuanData.SafeRelease(objTempLanmuData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据上级栏目代码获取下级的栏目代码
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strPrevLMDM          ：上级栏目代码
        '     strNewLMDM           ：新栏目代码(返回)
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getNewLMDM( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strPrevLMDM As String, _
            ByRef strNewLMDM As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            '初始化
            getNewLMDM = False
            strNewLMDM = ""
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim()
                If strPrevLMDM Is Nothing Then strPrevLMDM = ""
                strPrevLMDM = strPrevLMDM.Trim()

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取上级栏目级别
                Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharFjdmSeparate
                Dim intLevel As Integer = objPulicParameters.getCodeLevel(strPrevLMDM, strSep)
                If intLevel < 0 Then
                    intLevel = 0
                End If

                '获取数据
                strSQL = ""
                strSQL = strSQL + " select max(本级代码) " + vbCr
                strSQL = strSQL + " from 信息_B_公共资源_栏目 " + vbCr
                strSQL = strSQL + " where 栏目级别 = " + (intLevel + 1).ToString() + vbCr         '直接下级
                If strPrevLMDM <> "" Then
                    strSQL = strSQL + " and 栏目代码 like '" + strPrevLMDM + strSep + "%'" + vbCr '下级
                End If
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    If strPrevLMDM = "" Then
                        strNewLMDM = "1"
                    Else
                        strNewLMDM = strPrevLMDM + strSep + "1"
                    End If
                Else
                    Dim intValue As Integer
                    With objDataSet.Tables(0).Rows(0)
                        intValue = objPulicParameters.getObjectValue(.Item(0), 0)
                    End With
                    intValue += 1
                    If strPrevLMDM = "" Then
                        strNewLMDM = intValue.ToString()
                    Else
                        strNewLMDM = strPrevLMDM + strSep + intValue.ToString()
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
            getNewLMDM = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取新的栏目标识
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strNewLMBS           ：新栏目标识(返回)
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getNewLMBS( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef strNewLMBS As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            '初始化
            getNewLMBS = False
            strNewLMBS = ""
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim()
                If strUserId = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim()

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                If objdacCommon.getNewCode(strErrMsg, objSqlConnection, "栏目标识", "信息_B_公共资源_栏目", True, strNewLMBS) = False Then
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
            getNewLMBS = True
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
        Public Function getLanmuDefaultValue( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef objNewData As System.Collections.Specialized.ListDictionary, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            getLanmuDefaultValue = False

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim()
                If strUserId = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim()

                '获取栏目标识
                Dim strLMBS As String
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                        If Me.getNewLMBS(strErrMsg, strUserId, strPassword, strLMBS) = False Then
                            GoTo errProc
                        End If
                        objNewData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMBS) = objPulicParameters.getObjectValue(strLMBS, 0)
                    Case Else
                End Select

                '获取栏目代码
                Dim strLMDM As String
                strLMDM = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMDM), "")
                strLMDM = strLMDM.Trim()
                If strLMDM = "" Then
                    strErrMsg = "错误：[栏目代码]不能为空！"
                    GoTo errProc
                End If
                Dim strTemp As String = strLMDM
                strTemp = strTemp.Replace(Xydc.Platform.Common.Utilities.PulicParameters.CharFjdmSeparate, "")
                If objPulicParameters.isNumericString(strTemp) = False Then
                    strErrMsg = "错误：[栏目代码]中存在非法字符！"
                    GoTo errProc
                End If

                '根据栏目代码获取栏目级别
                Dim intLevel As Integer
                intLevel = objPulicParameters.getCodeLevel(strLMDM, Xydc.Platform.Common.Utilities.PulicParameters.CharFjdmSeparate)
                objNewData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMJB) = intLevel

                '根据栏目代码获取本级代码
                Dim strBJDM As String
                strBJDM = objPulicParameters.getCodeValue(strLMDM, Xydc.Platform.Common.Utilities.PulicParameters.CharFjdmSeparate, intLevel)
                objNewData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_BJDM) = objPulicParameters.getObjectValue(strBJDM, 0)

                '根据栏目代码获取顶级栏目
                Dim objggxxGonggongziyuanData As Xydc.Platform.Common.Data.ggxxGonggongziyuanData
                If intLevel <= 1 Then
                    objNewData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_DJLM) = objNewData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMBS)
                Else
                    Dim strDJLM As String
                    strDJLM = objPulicParameters.getCodeValue(strLMDM, Xydc.Platform.Common.Utilities.PulicParameters.CharFjdmSeparate, 1, True)

                    '根据顶级栏目代码获取顶级栏目标识
                    If Me.getLanmuData(strErrMsg, strUserId, strPassword, strDJLM, True, objggxxGonggongziyuanData) = False Then
                        GoTo errProc
                    End If
                    With objggxxGonggongziyuanData.Tables(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.TABLE_XX_B_GONGGONGZIYUAN_LANMU)
                        If .Rows.Count < 1 Then
                            strErrMsg = "错误：[" + strDJLM + "]不存在！"
                            GoTo errProc
                        Else
                            objNewData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_DJLM) = .Rows(0).Item(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMBS)
                        End If
                    End With
                    Xydc.Platform.Common.Data.ggxxGonggongziyuanData.SafeRelease(objggxxGonggongziyuanData)
                End If

                '根据栏目代码获取上级栏目
                If intLevel <= 1 Then
                    objNewData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_SJLM) = objNewData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_DJLM)
                Else
                    Dim strSJLM As String
                    strSJLM = objPulicParameters.getCodeValue(strLMDM, Xydc.Platform.Common.Utilities.PulicParameters.CharFjdmSeparate, intLevel - 1, True)

                    '根据顶级栏目代码获取上级栏目标识
                    If Me.getLanmuData(strErrMsg, strUserId, strPassword, strSJLM, True, objggxxGonggongziyuanData) = False Then
                        GoTo errProc
                    End If
                    With objggxxGonggongziyuanData.Tables(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.TABLE_XX_B_GONGGONGZIYUAN_LANMU)
                        If .Rows.Count < 1 Then
                            strErrMsg = "错误：[" + strSJLM + "]不存在！"
                            GoTo errProc
                        Else
                            objNewData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_SJLM) = .Rows(0).Item(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMBS)
                        End If
                    End With
                    Xydc.Platform.Common.Data.ggxxGonggongziyuanData.SafeRelease(objggxxGonggongziyuanData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            getLanmuDefaultValue = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据“栏目名称”获取“栏目标识”
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strLMMC              ：栏目名称
        '     strLMBS              ：(返回)栏目标识
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getLmbsByLmmc( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strLMMC As String, _
            ByRef strLMBS As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            getLmbsByLmmc = False
            strLMBS = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim()
                If strUserId = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim()
                If strLMMC Is Nothing Then strLMMC = ""
                strLMMC = strLMMC.Trim

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取信息
                strSQL = ""
                strSQL = strSQL + " select 栏目标识" + vbCr
                strSQL = strSQL + " from 信息_B_公共资源_栏目" + vbCr
                strSQL = strSQL + " where 栏目名称 = '" + strLMMC + "'" + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If

                '返回信息
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strLMBS = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item(0), "")
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getLmbsByLmmc = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据“栏目名称”获取“栏目代码”
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strLMMC              ：栏目名称
        '     strLMDM              ：(返回)栏目代码
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getLmdmByLmmc( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strLMMC As String, _
            ByRef strLMDM As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            getLmdmByLmmc = False
            strLMDM = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim()
                If strUserId = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim()
                If strLMMC Is Nothing Then strLMMC = ""
                strLMMC = strLMMC.Trim

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取信息
                strSQL = ""
                strSQL = strSQL + " select 栏目代码" + vbCr
                strSQL = strSQL + " from 信息_B_公共资源_栏目" + vbCr
                strSQL = strSQL + " where 栏目名称 = '" + strLMMC + "'" + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If

                '返回信息
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strLMDM = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item(0), "")
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getLmdmByLmmc = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function





        '----------------------------------------------------------------
        ' 检查“信息_B_公共资源_栏目”的数据的合法性
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
        Public Function doVerifyLanmuData( _
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

            doVerifyLanmuData = False

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                If objNewData Is Nothing Then
                    strErrMsg = "错误：未传入新的数据！"
                    GoTo errProc
                End If
                Dim intOldLMBS As Integer
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                    Case Else
                        If objOldData Is Nothing Then
                            strErrMsg = "错误：未传入旧的数据！"
                            GoTo errProc
                        End If
                        intOldLMBS = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMBS), 0)
                End Select

                '根据输入值计算其他自动值，并校验顶级、上级代码
                If Me.getLanmuDefaultValue(strErrMsg, strUserId, strPassword, objNewData, objenumEditType) = False Then
                    GoTo errProc
                End If

                '获取表结构定义
                strSQL = "select top 0 * from 信息_B_公共资源_栏目"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, strUserId, strPassword, strSQL, "信息_B_公共资源_栏目", objDataSet) = False Then
                    GoTo errProc
                End If

                '检查数据长度
                Dim objDictionaryEntry As System.Collections.DictionaryEntry
                Dim strField As String
                Dim intLen As Integer
                For Each objDictionaryEntry In objNewData
                    strField = objPulicParameters.getObjectValue(objDictionaryEntry.Key, "")
                    Select Case strField
                        Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMBS, _
                            Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMJB, _
                            Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_BJDM, _
                            Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_DJLM, _
                            Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_SJLM

                        Case Else
                            Dim strValue As String
                            strValue = objPulicParameters.getObjectValue(objDictionaryEntry.Value, "")
                            If strValue = "" Then
                                Select Case strField
                                    Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMSM
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

                '检查：栏目标识
                Dim intNewLMBS As Integer
                intNewLMBS = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMBS), 0)
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                        strSQL = ""
                        strSQL = strSQL + " select * from 信息_B_公共资源_栏目 "
                        strSQL = strSQL + " where 栏目标识 = @newlmbs"
                        objListDictionary.Add("@newlmbs", intNewLMBS)
                    Case Else
                        strSQL = ""
                        strSQL = strSQL + " select * from 信息_B_公共资源_栏目 "
                        strSQL = strSQL + " where 栏目标识 =  @newlmbs"
                        strSQL = strSQL + " and   栏目标识 <> @oldlmbs"
                        objListDictionary.Add("@newlmbs", intNewLMBS)
                        objListDictionary.Add("@oldlmbs", intOldLMBS)
                End Select
                If objdacCommon.getDataSetBySQL(strErrMsg, strUserId, strPassword, strSQL, objListDictionary, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strErrMsg = "错误：[" + intNewLMBS.ToString() + "]已经存在！"
                    GoTo errProc
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objListDictionary.Clear()

                '检查：栏目代码
                Dim strNewLMDM As String
                strNewLMDM = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMDM), "")
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                        strSQL = ""
                        strSQL = strSQL + " select * from 信息_B_公共资源_栏目 "
                        strSQL = strSQL + " where 栏目代码 = @newlmdm"
                        objListDictionary.Add("@newlmdm", strNewLMDM)
                    Case Else
                        strSQL = ""
                        strSQL = strSQL + " select * from 信息_B_公共资源_栏目 "
                        strSQL = strSQL + " where 栏目代码 =  @newlmdm"
                        strSQL = strSQL + " and   栏目标识 <> @oldlmbs"
                        objListDictionary.Add("@newlmdm", strNewLMDM)
                        objListDictionary.Add("@oldlmbs", intOldLMBS)
                End Select
                If objdacCommon.getDataSetBySQL(strErrMsg, strUserId, strPassword, strSQL, objListDictionary, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strErrMsg = "错误：[" + strNewLMDM.ToString() + "]已经存在！"
                    GoTo errProc
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objListDictionary.Clear()

                '检查：栏目名称
                Dim strNewLMMC As String
                strNewLMMC = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMMC), "")
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                        strSQL = ""
                        strSQL = strSQL + " select * from 信息_B_公共资源_栏目 "
                        strSQL = strSQL + " where 栏目名称 = @newlmmc"
                        objListDictionary.Add("@newlmmc", strNewLMMC)
                    Case Else
                        strSQL = ""
                        strSQL = strSQL + " select * from 信息_B_公共资源_栏目 "
                        strSQL = strSQL + " where 栏目名称 =  @newlmmc"
                        strSQL = strSQL + " and   栏目标识 <> @oldlmbs"
                        objListDictionary.Add("@newlmmc", strNewLMMC)
                        objListDictionary.Add("@oldlmbs", intOldLMBS)
                End Select
                If objdacCommon.getDataSetBySQL(strErrMsg, strUserId, strPassword, strSQL, objListDictionary, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strErrMsg = "错误：[" + strNewLMMC.ToString() + "]已经存在！"
                    GoTo errProc
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
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

            doVerifyLanmuData = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objListDictionary)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 保存“信息_B_公共资源_栏目”的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
        '     objNewData           ：新数据(返回)
        '     objenumEditType      ：编辑类型
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doSaveLanmuData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByRef objNewData As System.Collections.Specialized.ListDictionary, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '初始化
            doSaveLanmuData = False
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                If objNewData Is Nothing Then
                    strErrMsg = "错误：未传入新的数据！"
                    GoTo errProc
                End If
                Dim strOldLMDM As String
                Dim intOldLMBS As Integer
                Dim strNewLMDM As String
                Dim intNewLMBS As Integer
                intNewLMBS = objPulicParameters.getObjectValue(objNewData.Item(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMBS), 0)
                strNewLMDM = objPulicParameters.getObjectValue(objNewData.Item(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMDM), "")
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                    Case Else
                        If objOldData Is Nothing Then
                            strErrMsg = "错误：未传入旧的数据！"
                            GoTo errProc
                        End If
                        intOldLMBS = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMBS), 0)
                        strOldLMDM = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMDM), "")
                End Select

                '校验
                If Me.doVerifyLanmuData(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType) = False Then
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
                            strSQL = strSQL + " insert into 信息_B_公共资源_栏目 (" + strFileds + ")"
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
                            strSQL = strSQL + " update 信息_B_公共资源_栏目 set "
                            strSQL = strSQL + "   " + strFileds
                            strSQL = strSQL + " where 栏目标识 = @oldlmbs"
                            objSqlCommand.Parameters.Clear()
                            i = 0
                            For Each objDictionaryEntry In objNewData
                                objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), objDictionaryEntry.Value)
                                i += 1
                            Next
                            objSqlCommand.Parameters.AddWithValue("@oldlmbs", intOldLMBS)
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()

                            If strNewLMDM.ToUpper() <> strOldLMDM.ToUpper() Then
                                Dim intOldLMJB As Integer
                                intOldLMJB = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMJB), 0)
                                Dim intNewLMJB As Integer
                                intNewLMJB = objPulicParameters.getObjectValue(objNewData.Item(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMJB), 0)
                                Dim intNewDJLM As Integer
                                intNewDJLM = objPulicParameters.getObjectValue(objNewData.Item(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_DJLM), 0)

                                '更改原下级的代码
                                strSQL = ""
                                strSQL = strSQL + " update 信息_B_公共资源_栏目 set "
                                strSQL = strSQL + "   栏目代码 = @newlmdm + substring(栏目代码, @oldlmdmlen + 1, len(栏目代码) - @oldlmdmlen),"
                                strSQL = strSQL + "   栏目级别 = @newlmjb + 栏目级别 - @oldlmjb,"
                                strSQL = strSQL + "   顶级栏目 = @newdjlm "
                                strSQL = strSQL + " where 栏目代码 like @oldlmdm + @sep + '%'" '本栏目的下级
                                objSqlCommand.Parameters.Clear()
                                objSqlCommand.Parameters.AddWithValue("@newlmdm", strNewLMDM)
                                objSqlCommand.Parameters.AddWithValue("@oldlmdmlen", strOldLMDM.Length)
                                objSqlCommand.Parameters.AddWithValue("@newlmjb", intNewLMJB)
                                objSqlCommand.Parameters.AddWithValue("@oldlmjb", intOldLMJB)
                                objSqlCommand.Parameters.AddWithValue("@newdjlm", intNewDJLM)
                                objSqlCommand.Parameters.AddWithValue("@newlmbs", intNewLMBS)
                                objSqlCommand.Parameters.AddWithValue("@oldlmdm", strOldLMDM)
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
            doSaveLanmuData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据栏目代码删除“信息_B_公共资源_栏目”的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strLMDM              ：栏目代码
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteLanmuData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strLMDM As String) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '初始化
            doDeleteLanmuData = False
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim()
                If strUserId = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim()
                If strLMDM Is Nothing Then strLMDM = ""
                strLMDM = strLMDM.Trim()

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

                    '删除信息_B_公共资源_栏目
                    strSQL = ""
                    strSQL = strSQL + " delete from 信息_B_公共资源_栏目 "
                    strSQL = strSQL + " where 栏目代码 like @lmdm + @sep +'%' "
                    strSQL = strSQL + " or    栏目代码 = @lmdm"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@lmdm", strLMDM)
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
            doDeleteLanmuData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function





        '----------------------------------------------------------------
        ' 获取[人员代码=strCzydm]的公共资源数据（按“发布日期”降序），即
        ' 我负责发布的公共资源数据
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     strCzydm                    ：操作员标识
        '     strWhere                    ：搜索字符串
        '     objGonggongziyuanData       ：信息数据集
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
            ByRef objGonggongziyuanData As Xydc.Platform.Common.Data.ggxxGonggongziyuanData) As Boolean

            Dim objTempGonggongziyuanData As Xydc.Platform.Common.Data.ggxxGonggongziyuanData
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon


            Dim objdacAppManager As New Xydc.Platform.DataAccess.dacAppManager
            Dim objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty
            Dim strServerName As String = Xydc.Platform.Common.jsoaConfiguration.DatabaseServerName


            '初始化
            getDataSet = False
            objGonggongziyuanData = Nothing
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


                If objdacAppManager.getServerConnectionProperty(strErrMsg, strUserId, strPassword, strServerName, objConnectionProperty) = False Then
                    GoTo errProc
                End If
                Dim strRoleName As String = Xydc.Platform.Common.jsoaConfiguration.Administrators
                Dim blnRoleName As Boolean
                Dim strWhere_0 As String = "where a.name ='" + strUserId + "'"
                blnRoleName = doVerifyRoleData(strErrMsg, objConnectionProperty, strWhere_0, strRoleName, strUserId, strPassword)

                If strUserId = "sa" Then
                    blnRoleName = True
                End If



                '获取数据
                Try
                    '创建数据集
                    objTempGonggongziyuanData = New Xydc.Platform.Common.Data.ggxxGonggongziyuanData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.enumTableType.XX_B_GONGGONGZIYUAN)

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
                        strSQL = strSQL + " select distinct a.*" + vbCr
                        strSQL = strSQL + " from" + vbCr
                        strSQL = strSQL + " (" + vbCr
                        strSQL = strSQL + "   select " + vbCr
                        strSQL = strSQL + "     a.资源标识,a.资源序号,a.发布日期,a.栏目标识,a.人员代码,a.组织代码,a.内容类型,"
                        strSQL = strSQL + "     a.资源标题,a.文件位置,a.保留日期,a.发布标识,a.发布控制,a.发布范围,资源内容=''," + vbCr
                        strSQL = strSQL + "     阅读描述 = case when b.人员代码 is null then '" + strFalse + "' else '" + strTrue + "' end," + vbCr
                        strSQL = strSQL + "     发布描述 = case when isnull(a.发布标识,0) = 0 then '" + strFalse + "' else '" + strTrue + "' end," + vbCr
                        strSQL = strSQL + "     控制描述 = case when isnull(a.发布控制,0) = 0 then '" + strFalse + "' else '" + strTrue + "' end," + vbCr
                        strSQL = strSQL + "     c.栏目名称,c.栏目代码," + vbCr
                        strSQL = strSQL + "     d.人员名称," + vbCr
                        strSQL = strSQL + "     e.组织名称 " + vbCr
                        strSQL = strSQL + "   from" + vbCr
                        strSQL = strSQL + "   ("
                        strSQL = strSQL + "     select *" + vbCr
                        strSQL = strSQL + "     from 信息_B_公共资源" + vbCr

                        If blnRoleName = False Then

                            strSQL = strSQL + "     where 人员代码 = @czydm" + vbCr

                        End If

                        strSQL = strSQL + "   ) a" + vbCr
                        strSQL = strSQL + "   left join 信息_B_公共资源_栏目 c on a.栏目标识 = c.栏目标识" + vbCr
                        strSQL = strSQL + "   left join 公共_B_人员          d on a.人员代码 = d.人员代码" + vbCr
                        strSQL = strSQL + "   left join 公共_B_组织机构      e on a.组织代码 = e.组织代码" + vbCr
                        strSQL = strSQL + "   left join " + vbCr
                        strSQL = strSQL + "   (" + vbCr
                        strSQL = strSQL + "     select *" + vbCr
                        strSQL = strSQL + "     from 信息_B_公共资源_阅读情况" + vbCr

                        If blnRoleName = False Then

                            strSQL = strSQL + "     where 人员代码 = @ydry" + vbCr

                        End If

                        strSQL = strSQL + "   ) b on a.资源标识 = b.资源标识" + vbCr
                        strSQL = strSQL + " ) a" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.发布日期 desc " + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@czydm", strCzydm)
                        objSqlCommand.Parameters.AddWithValue("@ydry", strCzydm)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempGonggongziyuanData.Tables(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.TABLE_XX_B_GONGGONGZIYUAN))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempGonggongziyuanData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.ggxxGonggongziyuanData.SafeRelease(objTempGonggongziyuanData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            Xydc.Platform.DataAccess.dacAppManager.SafeRelease(objdacAppManager)
            Xydc.Platform.Common.Utilities.ConnectionProperty.SafeRelease(objConnectionProperty)


            '返回
            objGonggongziyuanData = objTempGonggongziyuanData
            getDataSet = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.ggxxGonggongziyuanData.SafeRelease(objTempGonggongziyuanData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            Xydc.Platform.DataAccess.dacAppManager.SafeRelease(objdacAppManager)
            Xydc.Platform.Common.Utilities.ConnectionProperty.SafeRelease(objConnectionProperty)

            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取[资源标识=strZYBS]的公共资源数据
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     strZYBS                     ：资源标识
        '     objGonggongziyuanData       ：信息数据集
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZYBS As String, _
            ByRef objGonggongziyuanData As Xydc.Platform.Common.Data.ggxxGonggongziyuanData) As Boolean

            Dim objTempGonggongziyuanData As Xydc.Platform.Common.Data.ggxxGonggongziyuanData
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '初始化
            getDataSet = False
            objGonggongziyuanData = Nothing
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
                If strZYBS Is Nothing Then strZYBS = ""
                strZYBS = strZYBS.Trim

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                Try
                    '创建数据集
                    objTempGonggongziyuanData = New Xydc.Platform.Common.Data.ggxxGonggongziyuanData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.enumTableType.XX_B_GONGGONGZIYUAN)

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
                        strSQL = strSQL + "     阅读描述 = case when b.人员代码 is null then '" + strFalse + "' else '" + strTrue + "' end," + vbCr
                        strSQL = strSQL + "     发布描述 = case when isnull(a.发布标识,0) = 0 then '" + strFalse + "' else '" + strTrue + "' end," + vbCr
                        strSQL = strSQL + "     控制描述 = case when isnull(a.发布控制,0) = 0 then '" + strFalse + "' else '" + strTrue + "' end," + vbCr
                        strSQL = strSQL + "     c.栏目名称,c.栏目代码," + vbCr
                        strSQL = strSQL + "     d.人员名称," + vbCr
                        strSQL = strSQL + "     e.组织名称 " + vbCr
                        strSQL = strSQL + "   from" + vbCr
                        strSQL = strSQL + "   ("
                        strSQL = strSQL + "     select *" + vbCr
                        strSQL = strSQL + "     from 信息_B_公共资源" + vbCr
                        strSQL = strSQL + "     where 资源标识 = @zybs" + vbCr
                        strSQL = strSQL + "   ) a" + vbCr
                        strSQL = strSQL + "   left join 信息_B_公共资源_栏目 c on a.栏目标识 = c.栏目标识" + vbCr
                        strSQL = strSQL + "   left join 公共_B_人员          d on a.人员代码 = d.人员代码" + vbCr
                        strSQL = strSQL + "   left join 公共_B_组织机构      e on a.组织代码 = e.组织代码" + vbCr
                        strSQL = strSQL + "   left join " + vbCr
                        strSQL = strSQL + "   (" + vbCr
                        strSQL = strSQL + "     select *" + vbCr
                        strSQL = strSQL + "     from 信息_B_公共资源_阅读情况" + vbCr
                        strSQL = strSQL + "     where 人员代码 = @ydry" + vbCr
                        strSQL = strSQL + "   ) b on a.资源标识 = b.资源标识" + vbCr
                        strSQL = strSQL + " ) a" + vbCr
                        strSQL = strSQL + " order by a.发布日期 desc " + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@zybs", strZYBS)
                        objSqlCommand.Parameters.AddWithValue("@ydry", strUserId)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempGonggongziyuanData.Tables(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.TABLE_XX_B_GONGGONGZIYUAN))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempGonggongziyuanData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.ggxxGonggongziyuanData.SafeRelease(objTempGonggongziyuanData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objGonggongziyuanData = objTempGonggongziyuanData
            getDataSet = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.ggxxGonggongziyuanData.SafeRelease(objTempGonggongziyuanData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取strUserId的能够阅读的已发布的公共资源数据（按“发布日期”降序）
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     strWhere                    ：搜索字符串
        '     blnUnused                   ：重载用
        '     objGonggongziyuanData       ：信息数据集
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByVal blnUnused As Boolean, _
            ByRef objGonggongziyuanData As Xydc.Platform.Common.Data.ggxxGonggongziyuanData) As Boolean

            Dim objTempGonggongziyuanData As Xydc.Platform.Common.Data.ggxxGonggongziyuanData
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '初始化
            getDataSet = False
            objGonggongziyuanData = Nothing
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
                    objTempGonggongziyuanData = New Xydc.Platform.Common.Data.ggxxGonggongziyuanData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.enumTableType.XX_B_GONGGONGZIYUAN)

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
                        strSQL = strSQL + "   select " + vbCr
                        strSQL = strSQL + "     a.资源标识,a.资源序号,a.发布日期,a.栏目标识,a.人员代码,a.组织代码,a.内容类型,"
                        strSQL = strSQL + "     a.资源标题,a.文件位置,a.保留日期,a.发布标识,a.发布控制,a.发布范围,a.资源内容," + vbCr
                        strSQL = strSQL + "     阅读描述 = case when b.人员代码 is null then '" + strFalse + "' else '" + strTrue + "' end," + vbCr
                        strSQL = strSQL + "     发布描述 = case when isnull(a.发布标识,0) = 0 then '" + strFalse + "' else '" + strTrue + "' end," + vbCr
                        strSQL = strSQL + "     控制描述 = case when isnull(a.发布控制,0) = 0 then '" + strFalse + "' else '" + strTrue + "' end," + vbCr
                        strSQL = strSQL + "     c.栏目名称,c.栏目代码," + vbCr
                        strSQL = strSQL + "     d.人员名称," + vbCr
                        strSQL = strSQL + "     e.组织名称 " + vbCr
                        strSQL = strSQL + "   from" + vbCr
                        strSQL = strSQL + "   (" + vbCr
                        strSQL = strSQL + "     select *" + vbCr
                        strSQL = strSQL + "     from 信息_B_公共资源" + vbCr
                        strSQL = strSQL + "     where 发布标识 = 1" + vbCr '已发布
                        strSQL = strSQL + "   ) a" + vbCr
                        strSQL = strSQL + "   left join 信息_B_公共资源_栏目 c on a.栏目标识 = c.栏目标识" + vbCr
                        strSQL = strSQL + "   left join 公共_B_人员          d on a.人员代码 = d.人员代码" + vbCr
                        strSQL = strSQL + "   left join 公共_B_组织机构      e on a.组织代码 = e.组织代码" + vbCr
                        strSQL = strSQL + "   left join " + vbCr
                        strSQL = strSQL + "   (" + vbCr
                        strSQL = strSQL + "     select *" + vbCr
                        strSQL = strSQL + "     from 信息_B_公共资源_阅读情况" + vbCr
                        strSQL = strSQL + "     where 人员代码 = @ydry" + vbCr
                        strSQL = strSQL + "   ) b on a.资源标识 = b.资源标识" + vbCr
                        strSQL = strSQL + "   left join " + vbCr
                        strSQL = strSQL + "   (" + vbCr
                        strSQL = strSQL + "     select *" + vbCr
                        strSQL = strSQL + "     from 信息_B_公共资源_阅读范围" + vbCr
                        strSQL = strSQL + "     where 人员代码 = @ydry" + vbCr
                        strSQL = strSQL + "   ) f on a.资源标识 = f.资源标识" + vbCr
                        strSQL = strSQL + "   where ((isnull(a.发布控制,0) = 0) or (isnull(a.发布控制,0) = 1 and f.人员代码 is not null) or (a.人员代码 = @ydry))" '能阅读
                        strSQL = strSQL + " ) a" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.发布日期 desc " + vbCr

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@ydry", strUserId)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempGonggongziyuanData.Tables(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.TABLE_XX_B_GONGGONGZIYUAN))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempGonggongziyuanData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.ggxxGonggongziyuanData.SafeRelease(objTempGonggongziyuanData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objGonggongziyuanData = objTempGonggongziyuanData
            getDataSet = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.ggxxGonggongziyuanData.SafeRelease(objTempGonggongziyuanData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取[资源标识=strZYBS]的公共资源的限制阅读人员数据
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     strZYBS                     ：资源标识
        '     strYDRYMC                   ：（返回）限制阅读人员数据(人员名称)
        '     strYDRYDM                   ：（返回）限制阅读人员数据(人员代码)
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getKeYueduRenyuan( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZYBS As String, _
            ByRef strYDRYMC As String, _
            ByRef strYDRYDM As String) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim strSQL As String

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet

            '初始化
            getKeYueduRenyuan = False
            strErrMsg = ""
            strYDRYMC = ""
            strYDRYDM = ""

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
                If strZYBS Is Nothing Then strZYBS = ""
                strZYBS = strZYBS.Trim
                If strZYBS = "" Then
                    strErrMsg = "错误：未指定[资源标识]！"
                    GoTo errProc
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据集
                strSQL = ""
                strSQL = strSQL + " select a.人员代码, b.人员名称" + vbCr
                strSQL = strSQL + " from" + vbCr
                strSQL = strSQL + " ("
                strSQL = strSQL + "   select *" + vbCr
                strSQL = strSQL + "   from 信息_B_公共资源_阅读范围" + vbCr
                strSQL = strSQL + "   where 资源标识 = '" + strZYBS + "'" + vbCr
                strSQL = strSQL + " ) a" + vbCr
                strSQL = strSQL + " left join 公共_B_人员 b on a.人员代码 = b.人员代码" + vbCr
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
                                strTemp = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_YUEDUFANWEI_RYMC), "")
                                If strTemp <> "" Then
                                    If strYDRYMC = "" Then
                                        strYDRYMC = strTemp
                                    Else
                                        strYDRYMC = strYDRYMC + objPulicParameters.CharSeparate + strTemp
                                    End If
                                End If

                                strTemp = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_YUEDUFANWEI_RYDM), "")
                                If strTemp <> "" Then
                                    If strYDRYDM = "" Then
                                        strYDRYDM = strTemp
                                    Else
                                        strYDRYDM = strYDRYDM + objPulicParameters.CharSeparate + strTemp
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
        ' 取消已发布的公共资源 或 发布公共资源
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strZYBS              ：资源标识
        '     blnFabu              ：True-发布，False-取消发布
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doFabu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZYBS As String, _
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
                If strZYBS Is Nothing Then strZYBS = ""
                strZYBS = strZYBS.Trim
                If strZYBS = "" Then
                    strErrMsg = "错误：未指定[资源标识]！"
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
                        strSQL = strSQL + " update 信息_B_公共资源 set" + vbCr
                        strSQL = strSQL + "   发布标识 = 1," + vbCr
                        strSQL = strSQL + "   发布日期 = @rq" + vbCr
                        strSQL = strSQL + " where 资源标识 = @zybs" + vbCr
                        strSQL = strSQL + " and   发布标识 <> 1" + vbCr
                        objSqlCommand.Parameters.AddWithValue("@rq", Now)
                        objSqlCommand.Parameters.AddWithValue("@zybs", strZYBS)
                    Else
                        strSQL = ""
                        strSQL = strSQL + " update 信息_B_公共资源 set" + vbCr
                        strSQL = strSQL + "   发布标识 = 0" + vbCr
                        strSQL = strSQL + " where 资源标识 = @zybs" + vbCr
                        strSQL = strSQL + " and   发布标识 <> 0" + vbCr
                        objSqlCommand.Parameters.AddWithValue("@zybs", strZYBS)
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
        '     strZYBS              ：资源标识
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doSetHasRead( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZYBS As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
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
                If strZYBS Is Nothing Then strZYBS = ""
                strZYBS = strZYBS.Trim
                If strZYBS = "" Then
                    strErrMsg = "错误：未指定[资源标识]！"
                    GoTo errProc
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
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
                    strSQL = strSQL + " delete from 信息_B_公共资源_阅读情况" + vbCr
                    strSQL = strSQL + " where 资源标识 = @zybs" + vbCr
                    strSQL = strSQL + " and   人员代码 = @ydry" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@zybs", strZYBS)
                    objSqlCommand.Parameters.AddWithValue("@ydry", strUserId)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '设置阅读记录
                    strSQL = ""
                    strSQL = strSQL + " insert into 信息_B_公共资源_阅读情况 (" + vbCr
                    strSQL = strSQL + "   资源标识,人员代码" + vbCr
                    strSQL = strSQL + " ) values (" + vbCr
                    strSQL = strSQL + "   @zybs,@ydry" + vbCr
                    strSQL = strSQL + " )" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@zybs", strZYBS)
                    objSqlCommand.Parameters.AddWithValue("@ydry", strUserId)
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
            doSetHasRead = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 删除公共资源
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strZYBS              ：资源标识
        '     strAppRoot           ：应用根Http路径(不带/)
        '     objServer            ：服务器对象
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDelete( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZYBS As String, _
            ByVal strAppRoot As String, _
            ByVal objServer As System.Web.HttpServerUtility) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            Dim objDataSet As Xydc.Platform.Common.Data.ggxxGonggongziyuanData
            Dim strZWNR As String = ""
            Dim strSQL As String

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
                strZYBS = strZYBS.Trim
                If strZYBS = "" Then
                    strErrMsg = "错误：未指定[资源标识]！"
                    GoTo errProc
                End If
                If objServer Is Nothing Then
                    strErrMsg = "错误：未指定[System.Web.HttpServerUtility]！"
                    GoTo errProc
                End If
                If strAppRoot Is Nothing Then strAppRoot = ""
                strAppRoot = strAppRoot.Trim

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取公告数据
                If Me.getDataSet(strErrMsg, strUserId, strPassword, strZYBS, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables.Count < 1 Then
                    strErrMsg = "错误：无法获取数据！"
                    GoTo errProc
                End If
                If objDataSet.Tables(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.TABLE_XX_B_GONGGONGZIYUAN) Is Nothing Then
                    strErrMsg = "错误：无法获取数据！"
                    GoTo errProc
                End If
                If objDataSet.Tables(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.TABLE_XX_B_GONGGONGZIYUAN).Rows.Count < 1 Then
                    strErrMsg = "错误：无法获取数据！"
                    GoTo errProc
                End If
                With objDataSet.Tables(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.TABLE_XX_B_GONGGONGZIYUAN).Rows(0)
                    strZWNR = objPulicParameters.getObjectValue(.Item(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_WJWZ), "")
                End With
                Xydc.Platform.Common.Data.ggxxGonggongziyuanData.SafeRelease(objDataSet)

                '开始事务
                objSqlTransaction = objSqlConnection.BeginTransaction()

                '删除数据
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '删除“信息_B_公共资源_阅读范围”信息
                    strSQL = ""
                    strSQL = strSQL + " delete from 信息_B_公共资源_阅读范围 " + vbCr
                    strSQL = strSQL + " where 资源标识 = @zybs" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@zybs", strZYBS)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '删除“信息_B_公共资源_阅读情况”信息
                    strSQL = ""
                    strSQL = strSQL + " delete from 信息_B_公共资源_阅读情况 " + vbCr
                    strSQL = strSQL + " where 资源标识 = @zybs" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@zybs", strZYBS)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '删除“信息_B_公共资源”信息
                    strSQL = ""
                    strSQL = strSQL + " delete from 信息_B_公共资源 " + vbCr
                    strSQL = strSQL + " where 资源标识 = @zybs" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@zybs", strZYBS)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '删除“文件位置”对应文件数据
                    Dim strLocalFile As String = ""
                    If strZWNR <> "" Then
                        '计算HTTP位置
                        strLocalFile = strAppRoot + Xydc.Platform.Common.Utilities.BaseURI.DEFAULT_DIRSEP + strZWNR
                        strLocalFile = objServer.MapPath(strLocalFile)
                        '删除文件
                        If objBaseLocalFile.doDeleteFile(strErrMsg, strLocalFile) = False Then
                            '可以不成功，形成垃圾文件！
                        End If
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
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.ggxxGonggongziyuanData.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doDelete = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.ggxxGonggongziyuanData.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 检查“信息_B_公共资源”的数据的合法性
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
        '     objNewData           ：(返回)新数据
        '     objenumEditType      ：编辑类型
        '     strUploadFile        ：上载文件的WEB本地完全路径
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
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType, _
            ByVal strUploadFile As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
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
                If strUploadFile Is Nothing Then strUploadFile = ""
                strUploadFile = strUploadFile.Trim

                '获取表结构定义
                strSQL = "select top 0 * from 信息_B_公共资源"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, strUserId, strPassword, strSQL, "信息_B_公共资源", objDataSet) = False Then
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

                        Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZYNR
                            'TEXT列

                        Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LMMC, _
                            Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LMDM, _
                            Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_RYMC, _
                            Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZZMC, _
                            Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_FBMS, _
                            Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_KZMS, _
                            Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_YDMS
                            '计算列

                        Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZYBS
                            '系统自动给定值
                            If strValue = "" Then
                                If objdacCommon.getNewGUID(strErrMsg, strUserId, strPassword, strValue) = False Then
                                    GoTo errProc
                                End If
                            End If

                        Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_FBRQ
                            If strValue = "" Then
                                strValue = Format(Now, "yyyy-MM-dd HH:mm:ss")
                            End If
                            If objPulicParameters.isDatetimeString(strValue) = False Then
                                strErrMsg = "错误：[" + strField + "]输入无效的日期！"
                                GoTo errProc
                            End If
                            strValue = Format(CType(strValue, System.DateTime), "yyyy-MM-dd HH:mm:ss")
                        Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_BLRQ
                            If strValue <> "" Then
                                If objPulicParameters.isDatetimeString(strValue) = False Then
                                    strErrMsg = "错误：[" + strField + "]输入无效的日期！"
                                    GoTo errProc
                                End If
                                strValue = Format(CType(strValue, System.DateTime), "yyyy-MM-dd HH:mm:ss")
                            End If

                        Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_FBBS, _
                            Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_FBKZ
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
                        Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_NRLX, _
                            Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LMBS
                            If strValue = "" Then
                                strErrMsg = "错误：[" + strField + "]不能为空！"
                                GoTo errProc
                            End If
                            If objPulicParameters.isIntegerString(strValue) = False Then
                                strErrMsg = "错误：[" + strField + "]输入无效的数字！"
                                GoTo errProc
                            End If
                        Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZYXH
                            '随后检查

                        Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_RYDM, _
                            Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZZDM, _
                            Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZYBT, _
                            Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZZMC

                            If strValue = "" Then
                                If strField = Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZZMC Then
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

                '检查“栏目标识”+“资源序号”
                Dim strLMBS As String = objNewData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LMBS)
                Dim strZYXH As String
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                        '自动设置“资源序号”
                        If objdacCommon.getNewCode(strErrMsg, objSqlConnection, "资源序号", "栏目标识", strLMBS, "信息_B_公共资源", True, strZYXH) = False Then
                            GoTo errProc
                        Else
                            objNewData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZYXH) = strZYXH
                        End If

                        strSQL = ""
                        strSQL = strSQL + " select *" + vbCr
                        strSQL = strSQL + " from 信息_B_公共资源" + vbCr
                        strSQL = strSQL + " where 栏目标识 = " + strLMBS + "" + vbCr
                        strSQL = strSQL + " and   资源序号 = " + strZYXH + "" + vbCr

                    Case Else
                        Dim strZYBS As String = objNewData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZYBS)
                        strZYXH = objNewData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZYXH)

                        strSQL = ""
                        strSQL = strSQL + " select *" + vbCr
                        strSQL = strSQL + " from 信息_B_公共资源" + vbCr
                        strSQL = strSQL + " where 栏目标识 =   " + strLMBS + " " + vbCr
                        strSQL = strSQL + " and   资源序号 =   " + strZYXH + " " + vbCr
                        strSQL = strSQL + " and   资源标识 <> '" + strZYBS + "'" + vbCr
                End Select
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strErrMsg = "错误：[栏目标识]+[资源序号]已经存在！"
                    GoTo errProc
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '检查“内容类型”+“资源内容”
                Dim intNRLX As Integer
                intNRLX = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_NRLX), 0)
                Dim strZYNR As String
                strZYNR = objNewData.Item(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZYNR)
                Dim blnDo As Boolean
                Select Case intNRLX
                    Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.enumZiyuanType.Text
                        If strZYNR.Trim = "" Then
                            strErrMsg = "错误：没有输入[资源内容]！"
                            GoTo errProc
                        End If
                        objNewData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_WJWZ) = ""
                    Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.enumZiyuanType.Tuwen
                        If strZYNR.Trim = "" Then
                            strErrMsg = "错误：没有输入[资源内容]！"
                            GoTo errProc
                        End If
                        If strUploadFile = "" Then
                            strErrMsg = "错误：没有上传[资源文件]！"
                            GoTo errProc
                        End If
                        If objBaseLocalFile.doFileExisted(strErrMsg, strUploadFile, blnDo) = False Then
                            GoTo errProc
                        End If
                        If blnDo = False Then
                            strErrMsg = "错误：[" + strUploadFile + "]不存在！"
                            GoTo errProc
                        End If
                    Case Else
                        If strUploadFile = "" Then
                            strErrMsg = "错误：没有上传[资源文件]！"
                            GoTo errProc
                        End If
                        If objBaseLocalFile.doFileExisted(strErrMsg, strUploadFile, blnDo) = False Then
                            GoTo errProc
                        End If
                        If blnDo = False Then
                            strErrMsg = "错误：[" + strUploadFile + "]不存在！"
                            GoTo errProc
                        End If
                        objNewData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZYNR) = ""
                End Select
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            doVerify = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 保存“信息_B_公共资源”的数据(现有事务)
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

                                    Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZYNR

                                    Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LMMC, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LMDM, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_RYMC, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZZMC, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_FBMS, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_KZMS, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_YDMS
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
                            strSQL = strSQL + " insert into 信息_B_公共资源 (" + strFileds + ")"
                            strSQL = strSQL + " values (" + strValues + ")"
                            '准备参数
                            objSqlCommand.Parameters.Clear()
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)

                                    Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZYNR

                                    Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LMMC, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LMDM, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_RYMC, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZZMC, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_FBMS, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_KZMS, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_YDMS
                                        '计算列
                                    Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_FBRQ, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_BLRQ
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), System.DBNull.Value)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(objNewData.Item(i), System.DateTime))
                                        End If
                                    Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LMBS, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_NRLX, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_FBBS, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_FBKZ, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZYXH
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
                            '获取原“资源标识”
                            Dim strOldZYBS As String
                            strOldZYBS = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZYBS), "")
                            '计算更新字段列表
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)

                                    Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZYNR

                                    Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LMMC, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LMDM, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_RYMC, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZZMC, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_FBMS, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_KZMS, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_YDMS
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
                            strSQL = strSQL + " update 信息_B_公共资源 set " + vbCr
                            strSQL = strSQL + "   " + strFileds + vbCr
                            strSQL = strSQL + " where 资源标识 = @oldzybs" + vbCr
                            '准备参数
                            objSqlCommand.Parameters.Clear()
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)

                                    Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZYNR

                                    Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LMMC, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LMDM, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_RYMC, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZZMC, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_FBMS, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_KZMS, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_YDMS
                                        '计算列
                                    Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_FBRQ, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_BLRQ
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), System.DBNull.Value)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(objNewData.Item(i), System.DateTime))
                                        End If
                                    Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LMBS, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_NRLX, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_FBBS, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_FBKZ, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZYXH
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
                            objSqlCommand.Parameters.AddWithValue("@oldzybs", strOldZYBS)
                            '执行SQL
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()
                    End Select


                    'text列处理
                    Dim strValue As String
                    Dim strZYBS As String
                    Dim strName As String
                    strZYBS = objNewData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZYBS)
                    strName = Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZYNR
                    If Not (objNewData(strName) Is Nothing) Then
                        strValue = objNewData(strName)
                        strValue = strValue.Replace("'", "''")
                        strSQL = ""
                        strSQL = strSQL + " DECLARE @ptrval binary(16)" + vbCr
                        strSQL = strSQL + " select @ptrval = TEXTPTR(" + strName + ")" + vbCr
                        strSQL = strSQL + " from 信息_B_公共资源" + vbCr
                        strSQL = strSQL + " where 资源标识 = @wybs" + vbCr
                        strSQL = strSQL + " WRITETEXT 信息_B_公共资源." + strName + " @ptrval '" + strValue + "'" + vbCr
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@wybs", strZYBS)
                        objSqlCommand.CommandText = strSQL
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
        ' 保存“信息_B_公共资源_阅读范围”的数据(现有事务)
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
            Dim strRymcList As String
            Dim strRydmList As String

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
                    strRymcList = ""
                    strRydmList = ""
                Else
                    '创建临时连接
                    objNewSqlConnection = New System.Data.SqlClient.SqlConnection(objSqlConnection.ConnectionString)
                    objNewSqlConnection.Open()
                    '解析
                    If objdacCustomer.getRenyuanList(strErrMsg, objNewSqlConnection, strFBFW, objPulicParameters.CharSeparate, strRymcList, strRydmList) = False Then
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
                            Dim strOldZybs As String
                            strOldZybs = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZYBS), "")
                            strSQL = ""
                            strSQL = strSQL + " delete from 信息_B_公共资源_阅读范围" + vbCr
                            strSQL = strSQL + " where 资源标识 = @zybs" + vbCr
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@zybs", strOldZybs)
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()
                    End Select

                    '保存现有数据
                    If strRydmList <> "" Then
                        Dim strNewZybs As String
                        strNewZybs = objPulicParameters.getObjectValue(objNewData.Item(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZYBS), "")

                        Dim strArray() As String
                        Dim intCount As Integer
                        Dim i As Integer
                        strArray = strRydmList.Split(Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate.ToCharArray)
                        intCount = strArray.Length
                        For i = 0 To intCount - 1 Step 1
                            strSQL = ""
                            strSQL = strSQL + " insert into 信息_B_公共资源_阅读范围 (" + vbCr
                            strSQL = strSQL + "   资源标识,人员代码" + vbCr
                            strSQL = strSQL + " ) values (" + vbCr
                            strSQL = strSQL + "   @zybs, @ydry"
                            strSQL = strSQL + " )"
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@zybs", strNewZybs)
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

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objNewSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doSave = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objNewSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据本地文件获取资源文件的HTTP服务器文件的命名
        ' 命名方案：资源标识+文件扩展名
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strLocalFile         ：本地文件名
        '     intWJND              ：文件年度
        '     strZYBS              ：资源标识
        '     strBasePath          ：该文件存放的HTTP基准路径(/)
        '     strRemoteFile        ：返回HTTP服务器文件路径(首字符不带/)
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getHTTPFileName( _
            ByRef strErrMsg As String, _
            ByVal strLocalFile As String, _
            ByVal intWJND As Integer, _
            ByVal strZYBS As String, _
            ByVal strBasePath As String, _
            ByRef strRemoteFile As String) As Boolean

            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile

            getHTTPFileName = False
            strRemoteFile = ""

            Try
                '检查
                If strLocalFile Is Nothing Then strLocalFile = ""
                strLocalFile = strLocalFile.Trim()
                If strLocalFile = "" Then
                    Exit Try
                End If
                If strZYBS Is Nothing Then strZYBS = ""
                strZYBS = strZYBS.Trim()
                If strZYBS = "" Then
                    Exit Try
                End If
                If strBasePath Is Nothing Then strBasePath = ""
                strBasePath = strBasePath.Trim
                strBasePath = strBasePath.Replace(Xydc.Platform.Common.Utilities.BaseURI.DEFAULT_DIRSEP, Xydc.Platform.Common.Utilities.BaseLocalFile.DEFAULT_DIRSEP)

                '获取文件名
                Dim strFileName As String = ""
                Dim strFileExt As String = ""
                strFileExt = objBaseLocalFile.getExtension(strLocalFile)

                '命名方案：资源标识+文件扩展名
                strFileName = strZYBS + strFileExt
                strFileName = objBaseLocalFile.doMakePath(intWJND.ToString(), strFileName)

                '复合目录+文件
                strFileName = objBaseLocalFile.doMakePath(strBasePath, strFileName)

                '转换分隔符
                strFileName = strFileName.Replace(Xydc.Platform.Common.Utilities.BaseLocalFile.DEFAULT_DIRSEP, Xydc.Platform.Common.Utilities.BaseURI.DEFAULT_DIRSEP)
                If strFileName.Substring(0) = Xydc.Platform.Common.Utilities.BaseURI.DEFAULT_DIRSEP Then
                    strFileName = strFileName.Substring(1)
                End If

                '返回
                strRemoteFile = strFileName

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)

            getHTTPFileName = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 备份资源文件
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strGJFTPSpec           ：资源文件的现HTTP路径
        '     strAppRoot             ：应用根Http路径(不带/)
        '     objServer              ：服务器对象
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function doBackupFiles( _
            ByRef strErrMsg As String, _
            ByVal strGJFTPSpec As String, _
            ByVal strAppRoot As String, _
            ByVal objServer As System.Web.HttpServerUtility) As Boolean

            Dim strBakExt As String = Xydc.Platform.Common.Utilities.PulicParameters.BACKUPFILEEXT
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile

            doBackupFiles = False
            strErrMsg = ""

            Try
                '检查
                If strGJFTPSpec Is Nothing Then strGJFTPSpec = ""
                strGJFTPSpec = strGJFTPSpec.Trim
                If strGJFTPSpec = "" Then
                    Exit Try
                End If
                If objServer Is Nothing Then
                    Exit Try
                End If
                If strAppRoot Is Nothing Then strAppRoot = ""
                strAppRoot = strAppRoot.Trim

                '备份
                Dim strLocalFile As String = ""
                Dim strHttpFile As String = ""
                strHttpFile = strAppRoot + Xydc.Platform.Common.Utilities.BaseURI.DEFAULT_DIRSEP + strGJFTPSpec
                strLocalFile = objServer.MapPath(strHttpFile)
                Dim blnDo As Boolean
                If objBaseLocalFile.doFileExisted(strErrMsg, strLocalFile, blnDo) = False Then
                    Exit Try
                End If
                If blnDo = True Then
                    '备份文件
                    If objBaseLocalFile.doCopyFile(strErrMsg, strLocalFile, strLocalFile + strBakExt, True) = False Then
                        GoTo errProc
                    End If
                    '删除现有文件
                    If objBaseLocalFile.doDeleteFile(strErrMsg, strLocalFile) = False Then
                        '形成垃圾文件！
                    End If
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)

            doBackupFiles = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 删除资源备份文件
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strGJFTPSpec           ：资源文件的原HTTP路径
        '     strAppRoot             ：应用根Http路径(不带/)
        '     objServer              ：服务器对象
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Overridable Function doDeleteBackupFiles( _
            ByRef strErrMsg As String, _
            ByVal strGJFTPSpec As String, _
            ByVal strAppRoot As String, _
            ByVal objServer As System.Web.HttpServerUtility) As Boolean

            Dim strBakExt As String = Xydc.Platform.Common.Utilities.PulicParameters.BACKUPFILEEXT
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile

            doDeleteBackupFiles = False
            strErrMsg = ""

            Try
                '检查
                If strGJFTPSpec Is Nothing Then strGJFTPSpec = ""
                strGJFTPSpec = strGJFTPSpec.Trim
                If strGJFTPSpec = "" Then
                    Exit Try
                End If
                If objServer Is Nothing Then
                    Exit Try
                End If
                If strAppRoot Is Nothing Then strAppRoot = ""
                strAppRoot = strAppRoot.Trim

                '删除备份
                Dim strLocalFile As String = ""
                Dim strHttpFile As String = ""
                strHttpFile = strAppRoot + Xydc.Platform.Common.Utilities.BaseURI.DEFAULT_DIRSEP + strGJFTPSpec
                strLocalFile = objServer.MapPath(strHttpFile)
                strLocalFile = strLocalFile + strBakExt
                If objBaseLocalFile.doDeleteFile(strErrMsg, strLocalFile) = False Then
                    '形成垃圾文件！
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)

            doDeleteBackupFiles = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 从备份中恢复资源文件
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strGJFTPSpec           ：资源文件的原HTTP路径
        '     strAppRoot             ：应用根Http路径(不带/)
        '     objServer              ：服务器对象
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Overridable Function doRestoreFiles( _
            ByRef strErrMsg As String, _
            ByVal strGJFTPSpec As String, _
            ByVal strAppRoot As String, _
            ByVal objServer As System.Web.HttpServerUtility) As Boolean

            Dim strBakExt As String = Xydc.Platform.Common.Utilities.PulicParameters.BACKUPFILEEXT
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile

            doRestoreFiles = False
            strErrMsg = ""

            Try
                '检查
                If strGJFTPSpec Is Nothing Then strGJFTPSpec = ""
                strGJFTPSpec = strGJFTPSpec.Trim
                If strGJFTPSpec = "" Then
                    Exit Try
                End If
                If objServer Is Nothing Then
                    Exit Try
                End If
                If strAppRoot Is Nothing Then strAppRoot = ""
                strAppRoot = strAppRoot.Trim

                '恢复
                Dim strLocalFile As String = ""
                Dim strHttpFile As String = ""
                strHttpFile = strAppRoot + Xydc.Platform.Common.Utilities.BaseURI.DEFAULT_DIRSEP + strGJFTPSpec
                strLocalFile = objServer.MapPath(strHttpFile)
                '恢复文件
                If objBaseLocalFile.doCopyFile(strErrMsg, strLocalFile + strBakExt, strLocalFile, True) = False Then
                    GoTo errProc
                End If
                '删除备份
                If objBaseLocalFile.doDeleteFile(strErrMsg, strLocalFile + strBakExt) = False Then
                    '形成垃圾文件
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)

            doRestoreFiles = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 保存资源文件
        '     strErrMsg              ：如果错误，则返回错误信息
        '     objSqlTransaction      ：现有事务
        '     strUserId              ：用户标识
        '     strPassword            ：用户密码
        '     strZYBS                ：资源标识
        '     strOldFile             ：旧文件路径(相对HTTP根目录路径)
        '     strGJFile              ：要保存的资源文件的本地缓存文件完整路径
        '     intWJND                ：要保存到的年度
        '     strAppRoot             ：应用根Http路径(不带/)
        '     strBasePath            ：从应用根到存放地的相对HTTP目录(开头不带/)
        '     objServer              ：服务器对象
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function doSaveFile( _
            ByRef strErrMsg As String, _
            ByVal objSqlTransaction As System.Data.SqlClient.SqlTransaction, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZYBS As String, _
            ByVal strOldFile As String, _
            ByVal strGJFile As String, _
            ByVal intWJND As Integer, _
            ByVal strAppRoot As String, _
            ByVal strBasePath As String, _
            ByVal objServer As System.Web.HttpServerUtility) As Boolean

            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim blnNewTrans As Boolean = False
            Dim strWJWZ As String
            Dim strSQL As String

            doSaveFile = False
            strErrMsg = ""

            Try
                '检查输入参数
                If objSqlTransaction Is Nothing Then
                    If strUserId Is Nothing Then strUserId = ""
                    strUserId = strUserId.Trim
                    If strUserId = "" Then
                        strErrMsg = "错误：未传入连接用户！"
                        GoTo errProc
                    End If
                End If
                If strGJFile Is Nothing Then strGJFile = ""
                strGJFile = strGJFile.Trim()
                If strGJFile = "" Then
                    '不用保存
                    Exit Try
                End If
                If objServer Is Nothing Then
                    strErrMsg = "错误：未传入[System.Web.HttpServerUtility]！"
                    GoTo errProc
                End If
                If strAppRoot Is Nothing Then strAppRoot = ""
                strAppRoot = strAppRoot.Trim
                If strZYBS Is Nothing Then strZYBS = ""
                strZYBS = strZYBS.Trim
                If strZYBS = "" Then
                    Exit Try
                End If
                If strOldFile Is Nothing Then strOldFile = ""
                strOldFile = strOldFile.Trim
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim
                If strBasePath Is Nothing Then strBasePath = ""
                strBasePath = strBasePath.Trim

                '检查文件是否存在?
                Dim blnExisted As Boolean
                If objBaseLocalFile.doFileExisted(strErrMsg, strGJFile, blnExisted) = False Then
                    GoTo errProc
                End If
                If blnExisted = False Then
                    strErrMsg = "错误：资源文件[" + strGJFile + "]不存在！"
                    GoTo errProc
                End If

                '获取文件信息
                strWJWZ = strOldFile

                '获取服务器文件
                Dim strDesFile As String
                If Me.getHTTPFileName(strErrMsg, strGJFile, intWJND, strZYBS, strBasePath, strDesFile) = False Then
                    GoTo errProc
                End If

                '更新文件路径
                '获取事务连接
                If objSqlTransaction Is Nothing Then
                    If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                        GoTo errProc
                    End If
                Else
                    objSqlConnection = objSqlTransaction.Connection
                End If

                '备份原文件
                If Me.doBackupFiles(strErrMsg, strWJWZ, strAppRoot, objServer) = False Then
                    GoTo errProc
                End If

                '开始事务
                If objSqlTransaction Is Nothing Then
                    blnNewTrans = True
                    objSqlTransaction = objSqlConnection.BeginTransaction
                Else
                    blnNewTrans = False
                End If

                '保存文件
                Dim strHttpFile As String = strAppRoot + Xydc.Platform.Common.Utilities.BaseURI.DEFAULT_DIRSEP + strDesFile
                Dim strLocalFile As String = objServer.MapPath(strHttpFile)
                '创建路径
                If objBaseLocalFile.doCreateDirectory(strErrMsg, strLocalFile) = False Then
                    GoTo errProc
                End If
                '上传到HTTP最终位置
                If objBaseLocalFile.doCopyFile(strErrMsg, strGJFile, strLocalFile, True) = False Then
                    GoTo rollDatabaseAndFile
                End If

                Try
                    '准备命令参数
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '准备SQL
                    strSQL = ""
                    strSQL = strSQL + " update 信息_B_公共资源 set " + vbCr
                    strSQL = strSQL + "   文件位置 = @wjwz " + vbCr
                    strSQL = strSQL + " where 资源标识  = @wjbs " + vbCr
                    strSQL = strSQL + " and   文件位置 <> @wjwz " + vbCr

                    '执行命令
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@wjwz", strDesFile)
                    objSqlCommand.Parameters.AddWithValue("@wjbs", strZYBS)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo rollDatabaseAndFile
                End Try

                '提交事务
                If blnNewTrans = True Then
                    objSqlTransaction.Commit()
                End If

                '删除备份文件
                If blnNewTrans = True Then
                    If Me.doDeleteBackupFiles(strErrMsg, strWJWZ, strAppRoot, objServer) = False Then
                        '可以不成功，形成垃圾文件！
                    End If
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            If blnNewTrans = True Then
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            End If

            doSaveFile = True
            Exit Function

rollDatabaseAndFile:
            If blnNewTrans = True Then
                objSqlTransaction.Rollback()
                If Me.doRestoreFiles(strSQL, strWJWZ, strAppRoot, objServer) = False Then
                    '可以不成功，形成垃圾数据
                End If
            End If
            GoTo errProc

errProc:
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            If blnNewTrans = True Then
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            End If
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 删除资源文件
        '     strErrMsg              ：如果错误，则返回错误信息
        '     objSqlTransaction      ：现有事务
        '     objConnectionProperty  ：FTP连接参数
        '     strUserId              ：用户标识
        '     strPassword            ：用户密码
        '     strZYBS                ：资源标识
        '     strOldFile             ：旧文件路径(相对应用根目录路径)
        '     strAppRoot             ：应用根Http路径(不带/)
        '     objServer              ：服务器对象
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function doDeleteFile( _
            ByRef strErrMsg As String, _
            ByVal objSqlTransaction As System.Data.SqlClient.SqlTransaction, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZYBS As String, _
            ByVal strOldFile As String, _
            ByVal strAppRoot As String, _
            ByVal objServer As System.Web.HttpServerUtility) As Boolean

            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim blnNewTrans As Boolean = False
            Dim strWJWZ As String
            Dim strSQL As String

            doDeleteFile = False
            strErrMsg = ""

            Try
                '检查输入参数
                If objServer Is Nothing Then
                    strErrMsg = "错误：[doDeleteFile]没有指定[System.Web.HttpServerUtility]！"
                    GoTo errProc
                End If
                If objSqlTransaction Is Nothing Then
                    If strUserId Is Nothing Then strUserId = ""
                    strUserId = strUserId.Trim
                    If strUserId = "" Then
                        strErrMsg = "错误：未传入连接用户！"
                        GoTo errProc
                    End If
                End If
                If strZYBS Is Nothing Then strZYBS = ""
                strZYBS = strZYBS.Trim
                If strZYBS = "" Then
                    Exit Try
                End If
                If strOldFile Is Nothing Then strOldFile = ""
                strOldFile = strOldFile.Trim
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim
                If strAppRoot Is Nothing Then strAppRoot = ""
                strAppRoot = strAppRoot.Trim

                '获取文件信息
                strWJWZ = strOldFile

                '获取事务连接
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
                    objSqlTransaction = objSqlConnection.BeginTransaction
                Else
                    blnNewTrans = False
                End If

                '删除文件
                If strWJWZ <> "" Then
                    Dim strHttpFile As String = strAppRoot + Xydc.Platform.Common.Utilities.BaseURI.DEFAULT_DIRSEP + strWJWZ
                    Dim strLocalFile As String = objServer.MapPath(strHttpFile)
                    If objBaseLocalFile.doDeleteFile(strErrMsg, strLocalFile) = False Then
                        '形成垃圾文件！
                    End If
                End If

                Try
                    '准备命令参数
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '准备SQL
                    strSQL = ""
                    strSQL = strSQL + " update 信息_B_公共资源 set " + vbCr
                    strSQL = strSQL + "   文件位置 = @wjwz " + vbCr
                    strSQL = strSQL + " where 资源标识  = @wjbs " + vbCr

                    '执行命令
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@wjwz", " ")
                    objSqlCommand.Parameters.AddWithValue("@wjbs", strZYBS)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo rollDatabaseAndFile
                End Try

                '提交事务
                If blnNewTrans = True Then
                    objSqlTransaction.Commit()
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            If blnNewTrans = True Then
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            End If

            doDeleteFile = True
            Exit Function

rollDatabaseAndFile:
            If blnNewTrans = True Then
                objSqlTransaction.Rollback()
            End If
            GoTo errProc

errProc:
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            If blnNewTrans = True Then
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            End If
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 保存公共资源数据记录(整个事务完成)
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserId              ：用户标识
        '     strPassword            ：用户密码
        '     objNewData             ：记录新值(返回保存后的新值)
        '     objOldData             ：记录旧值
        '     strFBFW                ：发布范围
        '     objenumEditType        ：编辑类型
        '     strUploadFile          ：上载文件的WEB本地完全路径
        '     strAppRoot             ：应用根Http路径(不带/)
        '     strBasePath            ：从应用根到存放地的相对HTTP目录(开头不带/)
        '     objServer              ：服务器对象
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
            ByVal strUploadFile As String, _
            ByVal strAppRoot As String, _
            ByVal strBasePath As String, _
            ByVal objServer As System.Web.HttpServerUtility) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim strWJWZ As String = ""
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
                If objServer Is Nothing Then
                    strErrMsg = "错误：没有指定[System.Web.HttpServerUtility]！"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim
                If strFBFW Is Nothing Then strFBFW = ""
                strFBFW = strFBFW.Trim
                If strUploadFile Is Nothing Then strUploadFile = ""
                strUploadFile = strUploadFile.Trim
                If strAppRoot Is Nothing Then strAppRoot = ""
                strAppRoot = strAppRoot.Trim
                If strBasePath Is Nothing Then strBasePath = strBasePath.Trim
                strBasePath = strBasePath.Trim

                '检查主记录
                If Me.doVerify(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType, strUploadFile) = False Then
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
                    '自动设置“发布控制”
                    objNewData.Item(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_FBKZ) = "0"
                    If strFBFW <> "" Then
                        objNewData.Item(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_FBKZ) = "1"
                    End If

                    '保存主记录
                    If Me.doSave(strErrMsg, objSqlTransaction, objOldData, objNewData, objenumEditType) = False Then
                        GoTo rollDatabase
                    End If

                    '保存“发布范围”解析后的人员列表
                    If Me.doSave(strErrMsg, objSqlTransaction, objOldData, objNewData, strFBFW, objenumEditType) = False Then
                        GoTo rollDatabase
                    End If

                    '保存资源文件
                    Dim strZYBS As String = objNewData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZYBS)
                    If objOldData Is Nothing Then
                        strWJWZ = ""
                    Else
                        strWJWZ = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_WJWZ), "")
                    End If
                    If strUploadFile <> "" Then
                        '保存新文件
                        Dim intWJND As Integer = Year(Now)
                        If Me.doSaveFile(strErrMsg, objSqlTransaction, strUserId, strPassword, strZYBS, strWJWZ, strUploadFile, intWJND, strAppRoot, strBasePath, objServer) = False Then
                            GoTo rollDatabaseAndFile
                        End If
                    Else
                        '删除现有文件
                        If Me.doDeleteFile(strErrMsg, objSqlTransaction, strUserId, strPassword, strZYBS, strWJWZ, strAppRoot, objServer) = False Then
                            GoTo rollDatabaseAndFile
                        End If
                    End If

                    '删除备份文件
                    If Me.doDeleteBackupFiles(strErrMsg, strWJWZ, strAppRoot, objServer) = False Then
                        '可以不成功，形成垃圾文件！
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

rollDatabaseAndFile:
            If Me.doRestoreFiles(strSQL, strWJWZ, strAppRoot, objServer) = False Then
                '可以不成功，形成垃圾数据
            End If
            GoTo rollDatabase

rollDatabase:
            objSqlTransaction.Rollback()
            GoTo errProc

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function






        '----------------------------------------------------------------
        ' 判断strUserId是否能够阅读的已发布的strZYBS公共资源数据
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     strZYBS                     ：资源标识
        '     blnYuedu                    ：（返回）True-能,False-不能
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function isCanRead( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZYBS As String, _
            ByRef blnYuedu As Boolean) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

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
                If strZYBS Is Nothing Then strZYBS = ""
                strZYBS = strZYBS.Trim
                If strZYBS = "" Then
                    Exit Try
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '获取数据
                Dim strFalse As String = Xydc.Platform.Common.Utilities.PulicParameters.CharFalse
                Dim strTrue As String = Xydc.Platform.Common.Utilities.PulicParameters.CharTrue
                strSQL = ""
                strSQL = strSQL + " select a.*" + vbCr
                strSQL = strSQL + " from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select a.*," + vbCr
                strSQL = strSQL + "     阅读描述 = case when b.人员代码 is null then '" + strFalse + "' else '" + strTrue + "' end," + vbCr
                strSQL = strSQL + "     发布描述 = case when isnull(a.发布标识,0) = 0 then '" + strFalse + "' else '" + strTrue + "' end," + vbCr
                strSQL = strSQL + "     控制描述 = case when isnull(a.发布控制,0) = 0 then '" + strFalse + "' else '" + strTrue + "' end," + vbCr
                strSQL = strSQL + "     c.栏目名称,c.栏目代码," + vbCr
                strSQL = strSQL + "     d.人员名称," + vbCr
                strSQL = strSQL + "     e.组织名称 " + vbCr
                strSQL = strSQL + "   from" + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select *" + vbCr
                strSQL = strSQL + "     from 信息_B_公共资源" + vbCr
                strSQL = strSQL + "     where 资源标识 = '" + strZYBS + "'" + vbCr
                strSQL = strSQL + "   ) a" + vbCr
                strSQL = strSQL + "   left join 信息_B_公共资源_栏目 c on a.栏目标识 = c.栏目标识" + vbCr
                strSQL = strSQL + "   left join 公共_B_人员          d on a.人员代码 = d.人员代码" + vbCr
                strSQL = strSQL + "   left join 公共_B_组织机构      e on a.组织代码 = e.组织代码" + vbCr
                strSQL = strSQL + "   left join " + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select *" + vbCr
                strSQL = strSQL + "     from 信息_B_公共资源_阅读情况" + vbCr
                strSQL = strSQL + "     where 人员代码 = '" + strUserId + "'" + vbCr
                strSQL = strSQL + "   ) b on a.资源标识 = b.资源标识" + vbCr
                strSQL = strSQL + "   left join " + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select *" + vbCr
                strSQL = strSQL + "     from 信息_B_公共资源_阅读范围" + vbCr
                strSQL = strSQL + "     where 人员代码 = '" + strUserId + "'" + vbCr
                strSQL = strSQL + "   ) f on a.资源标识 = f.资源标识" + vbCr
                strSQL = strSQL + "   where (a.发布标识 = 1 and ((isnull(a.发布控制,0) = 0) or (isnull(a.发布控制,0) = 1 and f.人员代码 is not null))) or (a.人员代码 = '" + strUserId + "')"
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
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            isCanRead = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 检查某人员是否已经加入到角色strRoleName的列表
        '----------------------------------------------------------------
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     objConnectionProperty       ：服务器信息
        '     strWhere                    ：搜索字符串(默认表前缀a.)
        '     strRoleName                 ：角色名字
        '     strUserId                   ：用户标识
        '     strPassWord                 ：用户密码
        ' 返回
        '     True                        ：成功
        '     False                       ：失败

        '----------------------------------------------------------------
        Public Function doVerifyRoleData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strWhere As String, _
            ByVal strRoleName As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '初始化
            doVerifyRoleData = False
            strErrMsg = ""

            Try
                '检查
                If strWhere Is Nothing Then strWhere = ""
                If strRoleName Is Nothing Then Exit Try
                strRoleName = strRoleName.Trim()
                strWhere = strWhere.Trim()


                '获取数据
                Dim strSQL As String
                Dim objDataset As New System.Data.DataSet
                Dim strDefDB As String = Xydc.Platform.Common.jsoaConfiguration.DatabaseServerUserDB
                Dim strDatabase As String = objConnectionProperty.InitialCatalog

                '准备SQL
                strSQL = ""
                strSQL = strSQL + " select distinct 'a'  where '" + strRoleName + "' in " + vbCr
                strSQL = strSQL + " ("
                strSQL = strSQL + " select a.rollname as 'NAME' from ( " + vbCr
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
                strSQL = strSQL + " )"

                If objdacCommon.getDataSetBySQL(strErrMsg, strUserId, strPassword, strSQL, objDataset) = False Then
                    GoTo errProc
                End If

                If objDataset.Tables(0).Rows.Count < 1 Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doVerifyRoleData = True
            Exit Function

errProc:
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

    End Class

End Namespace
