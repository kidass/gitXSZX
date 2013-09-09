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
    ' 类名    ：dacSystemOperate
    '
    ' 功能描述：
    '     提供对SystemOperate数据相关的数据层操作    

    '----------------------------------------------------------------
    Public Class dacSystemOperate
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.DataAccess.dacSystemOperate)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub

        '----------------------------------------------------------------
        ' 保存“应用操作日志”的数据
        '     strErrMsg             ：如果错误，则返回错误信息
        '     strUserId             ：用户标识
        '     strPassword           ：用户密码
        '     strUserHostAddress    ：主机地址
        '     strUserHostName       ：主机名
        '     strOperateType        ：操作方式
        '     strOperateTable       ：操作表
        '     strOperateContent     ：操作内容
        ' 返回
        '     True                  ：成功
        '     False                 ：失败
        '----------------------------------------------------------------
        Public Function doSaveOperateLogData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserHostAddress As String, _
            ByVal strUserHostName As String, _
            ByVal strOperateType As String, _
            ByVal strOperateTable As String, _
            ByVal strOperateContent As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String
            Dim objNewData As System.Collections.Specialized.NameValueCollection
            Dim strTable As String = Xydc.Platform.Common.Data.LogData.TABLE_System_B_OperateLog
            '初始化
            doSaveOperateLogData = False
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                objNewData = New System.Collections.Specialized.NameValueCollection

                objNewData.Clear()
                '添加数据
                objNewData.Add(Xydc.Platform.Common.Data.LogData.FIELD_System_B_OperateLog_UserHostAddress, strUserHostAddress)
                objNewData.Add(Xydc.Platform.Common.Data.LogData.FIELD_System_B_OperateLog_UserHostName, strUserHostName)
                objNewData.Add(Xydc.Platform.Common.Data.LogData.FIELD_System_B_OperateLog_UserID, strUserId)
                objNewData.Add(Xydc.Platform.Common.Data.LogData.FIELD_System_B_OperateLog_OperateTime, "")
                objNewData.Add(Xydc.Platform.Common.Data.LogData.FIELD_System_B_OperateLog_OperateType, strOperateType)
                objNewData.Add(Xydc.Platform.Common.Data.LogData.FIELD_System_B_OperateLog_OperateContent, strOperateContent)
                objNewData.Add(Xydc.Platform.Common.Data.LogData.FIELD_System_B_OperateLog_OperateTable, strOperateTable)

                '保存数据
                If Me.doSaveLogData(strErrMsg, strUserId, strPassword, objNewData, strTable) = False Then
                    GoTo errProc
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
            doSaveOperateLogData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 保存“访问操作日志”的数据
        '     strErrMsg             ：如果错误，则返回错误信息
        '     strUserId             ：用户标识
        '     strPassword           ：用户密码
        '     strUserHostAddress    ：主机地址
        '     strUserHostName       ：主机名
        '     strVisitURL           ：操作方式
        '     strVisitModel         ：操作内容
        ' 返回
        '     True                  ：成功
        '     False                 ：失败
        '----------------------------------------------------------------
        Public Function doSaveVisitLogData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserHostAddress As String, _
            ByVal strUserHostName As String, _
            ByVal strVisitURL As String, _
            ByVal strVisitModel As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String
            Dim objNewData As System.Collections.Specialized.NameValueCollection
            Dim strTable As String = Xydc.Platform.Common.Data.LogData.TABLE_System_B_VisitLog

            '初始化
            doSaveVisitLogData = False
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strUserId.Trim = "" Then
                    strErrMsg = "错误：未指定要获取信息的用户！"
                    GoTo errProc
                End If
                objNewData = New System.Collections.Specialized.NameValueCollection

                objNewData.Clear()
                '添加数据
                objNewData.Add(Xydc.Platform.Common.Data.LogData.FIELD_System_B_OperateLog_UserHostAddress, strUserHostAddress)
                objNewData.Add(Xydc.Platform.Common.Data.LogData.FIELD_System_B_OperateLog_UserHostName, strUserHostName)
                objNewData.Add(Xydc.Platform.Common.Data.LogData.FIELD_System_B_OperateLog_UserID, strUserId)
                objNewData.Add(Xydc.Platform.Common.Data.LogData.FIELD_System_B_OperateLog_OperateTime, "")
                objNewData.Add(Xydc.Platform.Common.Data.LogData.FIELD_System_B_VisitLog_VisitURL, strVisitURL)
                objNewData.Add(Xydc.Platform.Common.Data.LogData.FIELD_System_B_VisitLog_VisitModel, strVisitModel)

                '保存数据
                If Me.doSaveLogData(strErrMsg, strUserId, strPassword, objNewData, strTable) = False Then
                    GoTo errProc
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
            doSaveVisitLogData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 保存“操作日志的数据”操作
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objNewData           ：数据
        '     strTable             ：更新的表
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doSaveLogData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal strTable As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '初始化
            doSaveLogData = False
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
                    Dim strFields As String
                    Dim strValues As String
                    Dim intCount As Integer
                    Dim strValue As String
                    Dim i As Integer

                    intCount = objNewData.Count

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
                    strSQL = strSQL + " insert into " + strTable + " (" + strFields + ")"
                    strSQL = strSQL + " values (" + strValues + ")"
                    objSqlCommand.Parameters.Clear()
                    For i = 0 To intCount - 1 Step 1
                        strValue = objNewData.Item(i).Trim()
                        Select Case objNewData.GetKey(i)
                            Case Xydc.Platform.Common.Data.LogData.FIELD_System_B_OperateLog_OperateTime
                                objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), Now)
                            Case Else
                                If strValue = "" Then strValue = " "
                                objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), strValue)
                        End Select
                    Next
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
            doSaveLogData = True
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
