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
    ' 类名    ：dacJingchanglianxiren
    '
    ' 功能描述：
    '     提供对“公文_B_经常联系人”数据的增加、修改、删除、检索等操作
    '----------------------------------------------------------------

    Public Class dacJingchanglianxiren
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.DataAccess.dacJingchanglianxiren)
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
        ' 根据指定人员获取他的经常联系人信息的数据集(人员信息完全连接)
        ' 以组织代码、人员序号升序排序
        '     strErrMsg                ：如果错误，则返回错误信息
        '     strUserId                ：用户标识
        '     strPassword              ：用户密码
        '     strRYDM                  ：指定人员代码
        '     strWhere                 ：搜索串(默认表前缀a.)
        '     objJingchanglianxirenData：信息数据集
        ' 返回
        '     True                     ：成功
        '     False                    ：失败
        '----------------------------------------------------------------
        Public Function getJclxrData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRYDM As String, _
            ByVal strWhere As String, _
            ByRef objJingchanglianxirenData As Xydc.Platform.Common.Data.JingchanglianxirenData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempJingchanglianxirenData As Xydc.Platform.Common.Data.JingchanglianxirenData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '初始化
            getJclxrData = False
            objJingchanglianxirenData = Nothing
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strRYDM.Length > 0 Then strRYDM = strRYDM.Trim()

                '检查
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If strRYDM.Length < 1 Then
                    strErrMsg = "错误：未指定人员代码！"
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
                    objTempJingchanglianxirenData = New Xydc.Platform.Common.Data.JingchanglianxirenData(Xydc.Platform.Common.Data.JingchanglianxirenData.enumTableType.GW_B_JINGCHANGLIANXIREN)

                    '创建SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行检索
                    With Me.m_objSqlDataAdapter
                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* from (" + vbCr
                        strSQL = strSQL + "   select a.人员代码,a.联系人代码," + vbCr
                        strSQL = strSQL + "     b.人员名称,b.人员序号,b.组织代码,b.级别代码,b.秘书代码,b.联系电话,b.手机号码," + vbCr
                        strSQL = strSQL + "     b.FTP地址,b.邮箱地址,b.自动签收,b.交接显示名称,b.可查看姓名,b.可直送人员," + vbCr
                        strSQL = strSQL + "     b.其他由转送,b.是否加密," + vbCr
                        strSQL = strSQL + "     c.组织名称,c.组织别名," + vbCr
                        strSQL = strSQL + "     岗位列表 = dbo.GetGWMCByRydm(b.人员代码,@separate)," + vbCr
                        strSQL = strSQL + "     d.级别名称,d.行政级别," + vbCr
                        strSQL = strSQL + "     秘书名称 = e.人员名称," + vbCr
                        strSQL = strSQL + "     是否申请 = @charfalse" + vbCr
                        strSQL = strSQL + "   from " + vbCr
                        strSQL = strSQL + "   (" + vbCr
                        strSQL = strSQL + "     select * from 公文_B_经常联系人 " + vbCr
                        strSQL = strSQL + "     where 人员代码 = @rydm" + vbCr
                        strSQL = strSQL + "   ) a " + vbCr
                        strSQL = strSQL + "   left join 公共_B_人员     b on a.联系人代码 = b.人员代码 " + vbCr
                        strSQL = strSQL + "   left join 公共_B_组织机构 c on b.组织代码   = c.组织代码 " + vbCr
                        strSQL = strSQL + "   left join 公共_B_行政级别 d on b.级别代码   = d.级别代码 " + vbCr
                        strSQL = strSQL + "   left join 公共_B_人员     e on b.秘书代码   = e.人员代码 " + vbCr
                        strSQL = strSQL + "   where b.人员代码 is not null " + vbCr
                        strSQL = strSQL + " ) a " + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.组织代码,a.人员序号"

                        '设置参数
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@separate", Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate)
                        objSqlCommand.Parameters.AddWithValue("@charfalse", Xydc.Platform.Common.Utilities.PulicParameters.CharFalse)
                        objSqlCommand.Parameters.AddWithValue("@rydm", strRYDM)
                        .SelectCommand = objSqlCommand

                        '执行操作
                        .Fill(objTempJingchanglianxirenData.Tables(Xydc.Platform.Common.Data.JingchanglianxirenData.TABLE_GW_B_JINGCHANGLIANXIREN))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempJingchanglianxirenData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.JingchanglianxirenData.SafeRelease(objTempJingchanglianxirenData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            objJingchanglianxirenData = objTempJingchanglianxirenData
            getJclxrData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.JingchanglianxirenData.SafeRelease(objTempJingchanglianxirenData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取给定人员的经常联系人数据
        '     strErrMsg                ：如果错误，则返回错误信息
        '     strUserId                ：用户标识
        '     strPassword              ：用户密码
        '     strCzyId                 ：操作员标识
        '     objJinchanglianxirenData ：返回数据
        ' 返回
        '     True                     ：成功
        '     False                    ：失败
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strCzyId As String, _
            ByRef objJinchanglianxirenData As Xydc.Platform.Common.Data.JingchanglianxirenData) As Boolean

            Dim objTempJinchanglianxirenData As Xydc.Platform.Common.Data.JingchanglianxirenData

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            getDataSet = False
            objJinchanglianxirenData = Nothing
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim()
                If strUserId = "" Then
                    strErrMsg = "错误：未指定连接用户！"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim
                If strCzyId Is Nothing Then strCzyId = ""
                strCzyId = strCzyId.Trim()
                If strCzyId = "" Then
                    strErrMsg = "错误：未指定[操作员]！"
                    GoTo errProc
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '创建数据集
                objTempJinchanglianxirenData = New Xydc.Platform.Common.Data.JingchanglianxirenData(Xydc.Platform.Common.Data.JingchanglianxirenData.enumTableType.GW_B_JINGCHANGLIANXIREN)

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '执行检索
                With Me.SqlDataAdapter
                    '计算SQL
                    strSQL = ""
                    strSQL = strSQL + " select a.* " + vbCr
                    strSQL = strSQL + " from 公文_B_经常联系人 a" + vbCr
                    strSQL = strSQL + " where a.人员代码 = @rydm" + vbCr
                    strSQL = strSQL + " order by a.联系人代码"

                    '设置参数
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@rydm", strCzyId)
                    .SelectCommand = objSqlCommand

                    '执行操作
                    .Fill(objTempJinchanglianxirenData.Tables(Xydc.Platform.Common.Data.JingchanglianxirenData.TABLE_GW_B_JINGCHANGLIANXIREN))
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            objJinchanglianxirenData = objTempJinchanglianxirenData
            getDataSet = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.JingchanglianxirenData.SafeRelease(objTempJinchanglianxirenData)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 检查“公文_B_经常联系人”的数据的合法性
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
        Public Function doVerifyData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByRef objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            Dim objListDictionary As System.Collections.Specialized.ListDictionary

            doVerifyData = False

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
                strSQL = "select top 0 * from 公文_B_经常联系人"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, strUserId, strPassword, strSQL, "公文_B_经常联系人", objDataSet) = False Then
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
                        Case Xydc.Platform.Common.Data.JingchanglianxirenData.FIELD_GW_B_JINGCHANGLIANXIREN_RYDM, _
                            Xydc.Platform.Common.Data.JingchanglianxirenData.FIELD_GW_B_JINGCHANGLIANXIREN_LXRDM
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

                        Case Else
                            '不用检查
                    End Select

                    objNewData(strField) = strValue
                Next
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '检查键值
                Dim strLXRDM As String = ""
                Dim strRYDM As String = ""
                strRYDM = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.JingchanglianxirenData.FIELD_GW_B_JINGCHANGLIANXIREN_RYDM), "")
                strLXRDM = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.JingchanglianxirenData.FIELD_GW_B_JINGCHANGLIANXIREN_LXRDM), "")
                objListDictionary = New System.Collections.Specialized.ListDictionary
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                        strSQL = "select * from 公文_B_经常联系人 where 人员代码 = @rydm and 联系人代码 = @lxrdm"
                        objListDictionary.Add("@rydm", strRYDM)
                        objListDictionary.Add("@lxrdm", strLXRDM)
                    Case Else
                        Dim strOldLXRDM As String = ""
                        Dim strOldRYDM As String = ""
                        strOldLXRDM = objPulicParameters.getObjectValue(objOldData(Xydc.Platform.Common.Data.JingchanglianxirenData.FIELD_GW_B_JINGCHANGLIANXIREN_LXRDM), "")
                        strOldRYDM = objPulicParameters.getObjectValue(objOldData(Xydc.Platform.Common.Data.JingchanglianxirenData.FIELD_GW_B_JINGCHANGLIANXIREN_RYDM), "")
                        strSQL = "select * from 公文_B_经常联系人 where 人员代码 = @rydm and 联系人代码 = @lxrdm and not (人员代码 = @oldrydm and 联系人代码 = @oldlxrdm)"
                        objListDictionary.Add("@rydm", strRYDM)
                        objListDictionary.Add("@lxrdm", strLXRDM)
                        objListDictionary.Add("@oldrydm", strOldRYDM)
                        objListDictionary.Add("@oldlxrdm", strOldLXRDM)
                End Select
                If objdacCommon.getDataSetBySQL(strErrMsg, strUserId, strPassword, strSQL, objListDictionary, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strErrMsg = "错误：[" + strRYDM + "]+[" + strLXRDM + "]已经存在！"
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
        ' 保存“公文_B_经常联系人”的数据
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
            ByVal objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '初始化
            doSaveData = False
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
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
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim

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
                                    Case Xydc.Platform.Common.Data.JingchanglianxirenData.FIELD_GW_B_JINGCHANGLIANXIREN_RYDM, _
                                        Xydc.Platform.Common.Data.JingchanglianxirenData.FIELD_GW_B_JINGCHANGLIANXIREN_LXRDM
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
                                    Case Else
                                        '不用处理
                                End Select
                            Next
                            '准备SQL
                            strSQL = ""
                            strSQL = strSQL + " insert into 公文_B_经常联系人 (" + strFileds + ")"
                            strSQL = strSQL + " values (" + strValues + ")"
                            '准备参数
                            objSqlCommand.Parameters.Clear()
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.JingchanglianxirenData.FIELD_GW_B_JINGCHANGLIANXIREN_RYDM, _
                                        Xydc.Platform.Common.Data.JingchanglianxirenData.FIELD_GW_B_JINGCHANGLIANXIREN_LXRDM
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), " ")
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), objNewData.Item(i))
                                        End If
                                    Case Else
                                        '不用处理
                                End Select
                            Next
                            '执行SQL
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()

                        Case Else
                            Dim strOldLXRDM As String
                            Dim strOldRYDM As String
                            strOldLXRDM = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.JingchanglianxirenData.FIELD_GW_B_JINGCHANGLIANXIREN_LXRDM), "")
                            strOldRYDM = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.JingchanglianxirenData.FIELD_GW_B_JINGCHANGLIANXIREN_RYDM), "")
                            '计算更新字段列表
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.JingchanglianxirenData.FIELD_GW_B_JINGCHANGLIANXIREN_RYDM, _
                                        Xydc.Platform.Common.Data.JingchanglianxirenData.FIELD_GW_B_JINGCHANGLIANXIREN_LXRDM
                                        If strFileds = "" Then
                                            strFileds = objNewData.GetKey(i) + " = @A" + i.ToString()
                                        Else
                                            strFileds = strFileds + "," + objNewData.GetKey(i) + " = @A" + i.ToString()
                                        End If
                                    Case Else
                                        '不用处理
                                End Select
                            Next
                            '准备SQL
                            strSQL = ""
                            strSQL = strSQL + " update 公文_B_经常联系人 set " + vbCr
                            strSQL = strSQL + "   " + strFileds + vbCr
                            strSQL = strSQL + " where 人员代码   = @oldrydm" + vbCr
                            strSQL = strSQL + " and   联系人代码 = @oldlxrdm" + vbCr
                            '准备参数
                            objSqlCommand.Parameters.Clear()
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.JingchanglianxirenData.FIELD_GW_B_JINGCHANGLIANXIREN_RYDM, _
                                        Xydc.Platform.Common.Data.JingchanglianxirenData.FIELD_GW_B_JINGCHANGLIANXIREN_LXRDM
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), " ")
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), objNewData.Item(i))
                                        End If
                                    Case Else
                                        '不用处理
                                End Select
                            Next
                            objSqlCommand.Parameters.AddWithValue("@oldrydm", strOldRYDM)
                            objSqlCommand.Parameters.AddWithValue("@oldlxrdm", strOldLXRDM)
                            '执行SQL
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
        ' 删除“公文_B_经常联系人”的数据
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

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '初始化
            doDeleteData = False
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                If objOldData Is Nothing Then
                    strErrMsg = "错误：未传入旧的数据！"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim

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
                    Dim strOldLXRDM As String
                    Dim strOldRYDM As String
                    strOldLXRDM = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.JingchanglianxirenData.FIELD_GW_B_JINGCHANGLIANXIREN_LXRDM), "")
                    strOldRYDM = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.JingchanglianxirenData.FIELD_GW_B_JINGCHANGLIANXIREN_RYDM), "")
                    strSQL = ""
                    strSQL = strSQL + " delete from 公文_B_经常联系人 " + vbCr
                    strSQL = strSQL + " where 人员代码   = @oldrydm" + vbCr
                    strSQL = strSQL + " and   联系人代码 = @oldlxrdm" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@oldrydm", strOldRYDM)
                    objSqlCommand.Parameters.AddWithValue("@oldlxrdm", strOldLXRDM)

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

        '----------------------------------------------------------------
        ' 删除“公文_B_经常联系人”的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strRYDM              ：人员代码
        '     strLXRDM             ：联系人代码
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRYDM As String, _
            ByVal strLXRDM As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '初始化
            doDeleteData = False
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
                    strSQL = ""
                    strSQL = strSQL + " delete from 公文_B_经常联系人 " + vbCr
                    strSQL = strSQL + " where 人员代码   = @oldrydm" + vbCr
                    strSQL = strSQL + " and   联系人代码 = @oldlxrdm" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@oldrydm", strRYDM)
                    objSqlCommand.Parameters.AddWithValue("@oldlxrdm", strLXRDM)

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
