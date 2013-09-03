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
Imports System.IO
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient

Imports Xydc.Platform.Common
Imports Xydc.Platform.Common.Data
Imports Xydc.Platform.SystemFramework

Namespace Xydc.Platform.DataAccess

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.DataAccess
    ' ����    ��dacAppManager
    '
    ' ����������
    '     �ṩ��Ӧ��ϵͳ�����ܵ����ݷ��ʲ�֧��
    '----------------------------------------------------------------

    Public Class dacAppManager
        Implements IDisposable

        Private m_objSqlDataAdapter As System.Data.SqlClient.SqlDataAdapter








        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
            m_objSqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter
        End Sub

        '----------------------------------------------------------------
        ' ������������
        '----------------------------------------------------------------
        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(True)
        End Sub

        '----------------------------------------------------------------
        ' ������������
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
        ' ��ȫ�ͷű�����Դ
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.DataAccess.dacAppManager)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub










        '----------------------------------------------------------------
        ' ������ݵ�Excel
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objDataSet           ��Ҫ���������ݼ�
        '     strExcelFile         ��������WEB�������е�Excel�ļ�·��
        '     strMacroName         �������б�
        '     strMacroValue        ����ֵ�б�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
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
        ' ��ȡ��Ա����ID��������ݼ�(����֯���롢��Ա�����������)
        ' ����Ա��ȫ����������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strWhere             �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     objRenyuanData       ��ָ����֯�����µ���Ա��Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getRenyuanApplyIdData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objRenyuanData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempRenyuanData As Xydc.Platform.Common.Data.CustomerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '��ʼ��
            getRenyuanApplyIdData = False
            objRenyuanData = Nothing
            strErrMsg = ""

            Try
                '���
                If strUserId is nothing Then strUserId = ""
                If strPassword is nothing Then strPassword = ""
                If strWhere.Length > 0 Then strWhere = strWhere.Trim()
                If strUserId.trim = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ����
                Dim strSQL As String
                Try
                    '�������ݼ�
                    objTempRenyuanData = New Xydc.Platform.Common.Data.CustomerData(Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_RENYUAN_FULLJOIN)

                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ�м���
                    With Me.m_objSqlDataAdapter
                        '׼��SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* from ("
                        strSQL = strSQL + "   select a.*," + vbCr
                        strSQL = strSQL + "     b.��֯����,b.��֯����," + vbCr
                        strSQL = strSQL + "     ��λ�б� = dbo.GetGWMCByRydm(a.��Ա����,@separate)," + vbCr
                        strSQL = strSQL + "     c.��������,c.��������," + vbCr
                        strSQL = strSQL + "     �������� = d.��Ա����," + vbCr
                        strSQL = strSQL + "     ������ת������ = e.��Ա����," + vbCr
                        strSQL = strSQL + "     �Ƿ����� = case when f.name is null then @charfalse else @chartrue end " + vbCr
                        strSQL = strSQL + "   from ����_B_��Ա a " + vbCr
                        strSQL = strSQL + "   left join ����_B_��֯���� b on a.��֯����   = b.��֯���� " + vbCr
                        strSQL = strSQL + "   left join ����_B_�������� c on a.�������   = c.������� " + vbCr
                        strSQL = strSQL + "   left join ����_B_��Ա     d on a.�������   = d.��Ա���� " + vbCr
                        strSQL = strSQL + "   left join ����_B_��Ա     e on a.������ת�� = e.��Ա���� " + vbCr
                        strSQL = strSQL + "   left join" + vbCr
                        strSQL = strSQL + "   (" + vbCr
                        strSQL = strSQL + "     select name from master.dbo.syslogins"
                        strSQL = strSQL + "   ) f on a.��Ա���� = f.name" + vbCr
                        strSQL = strSQL + " ) a "
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.��֯����, cast(a.��Ա��� as integer)"

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@separate", Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate)
                        objSqlCommand.Parameters.AddWithValue("@charfalse", Xydc.Platform.Common.Utilities.PulicParameters.CharFalse)
                        objSqlCommand.Parameters.AddWithValue("@chartrue", Xydc.Platform.Common.Utilities.PulicParameters.CharTrue)
                        .SelectCommand = objSqlCommand

                        'ִ�в���
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

            '����
            objRenyuanData = objTempRenyuanData
            getRenyuanApplyIdData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempRenyuanData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ����Login
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strLoginId           ��Ҫ�����loginId
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doApplyId( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strLoginId As String) As Boolean

            Dim objdacCustomer As New Xydc.Platform.DataAccess.dacCustomer
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '��ʼ��
            doApplyId = False
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strLoginId Is Nothing Then strLoginId = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                strLoginId = strLoginId.Trim()
                If strUserId.trim = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If
                If strLoginId = "" Then
                    strErrMsg = "����δָ��Ҫ������Login��"
                    GoTo errProc
                End If

                '��ȡ��������
                Dim strNewPassword As String
                strNewPassword = objdacCustomer.doEncryptPassowrd("")

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ����
                Dim strSQL As String
                Try
                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '���ò���
                    strSQL = "exec sp_addlogin @loginid, @password, @defdb"
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@loginid", strLoginId)
                    objSqlCommand.Parameters.AddWithValue("@password", strNewPassword)
                    objSqlCommand.Parameters.AddWithValue("@defdb", "master")
                    objSqlCommand.ExecuteNonQuery()
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
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            doApplyId = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ע��Login
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strLoginId           ��Ҫע����loginId
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doDropId( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strLoginId As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objDataSetUser As System.Data.DataSet
            Dim objDataSetDB As System.Data.DataSet
            Dim strSQL As String

            '��ʼ��
            doDropId = False
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strLoginId Is Nothing Then strLoginId = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                strLoginId = strLoginId.Trim()
                If strUserId.trim = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If
                If strLoginId = "" Then
                    strErrMsg = "����δָ��Ҫע����Login��"
                    GoTo errProc
                End If
                If strLoginId.ToUpper() = "SA" Then
                    '����ɾ��
                    Exit Try
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ�������ݿ�
                strSQL = "select name from master.dbo.sysdatabases where name <> 'tempdb'"
                If objdacCommon.getDataSetBySQL(strErrMsg, strUserId, strPassword, strSQL, objDataSetDB) = False Then
                    GoTo errProc
                End If

                '��ȡ����
                Try
                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '������ݿ�ɾ��user
                    Dim strDBName As String
                    Dim intCount As Integer
                    Dim i As Integer
                    With objDataSetDB.Tables(0)
                        intCount = .Rows.Count
                        For i = 0 To intCount - 1 Step 1
                            strDBName = objPulicParameters.getObjectValue(.Rows(i).Item("name"), "")
                            strSQL = ""
                            strSQL = strSQL + " use " + strDBName + vbCr
                            strSQL = strSQL + " select name from sysusers where issqluser = 1 and name = '" + strLoginId + "'" + vbCr
                            If objdacCommon.getDataSetBySQL(strErrMsg, strUserId, strPassword, strSQL, objDataSetUser) = False Then
                                GoTo errProc
                            End If
                            If objDataSetUser.Tables(0).Rows.Count > 0 Then
                                strSQL = ""
                                strSQL = strSQL + " use " + strDBName + vbCr
                                strSQL = strSQL + " exec sp_dropuser '" + strLoginId + "'"
                                objSqlCommand.CommandText = strSQL
                                objSqlCommand.Parameters.Clear()
                                objSqlCommand.ExecuteNonQuery()
                            End If
                            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSetUser)
                            objDataSetUser = Nothing
                        Next
                    End With
                    Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSetDB)
                    objDataSetDB = Nothing

                    'ɾ��login
                    strSQL = ""
                    strSQL = strSQL + " use master" + vbCr
                    strSQL = strSQL + " exec sp_droplogin '" + strLoginId + "'"
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSetUser)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSetDB)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            doDropId = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSetUser)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSetDB)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function


        '----------------------------------------------------------------
        ' ���Login
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     blnISNull            ��TRUE-�����룬FALSE-δ����
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strLoginId           ��Ҫ����loginId
        ' ����
        '     True                 ��������
        '     False                ��δ����

        '----------------------------------------------------------------
        Public Function doCheckId( _
            ByRef strErrMsg As String, _
            ByRef blnISNull As Boolean, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strLoginId As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            '��ʼ��
            doCheckId = False
            strErrMsg = ""
            blnISNull = False

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strLoginId Is Nothing Then strLoginId = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                strLoginId = strLoginId.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If
                If strLoginId = "" Then
                    strErrMsg = "����δָ��Ҫע����Login��"
                    GoTo errProc
                End If
                If strLoginId.ToUpper() = "SA" Then
                    blnISNull = True
                    Exit Try
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '���login
                strSQL = ""
                strSQL = strSQL + " select * from master.dbo.syslogins where name='" + strLoginId + "'" + vbCr

                If objdacCommon.getDataSetBySQL(strErrMsg, strUserId, strPassword, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If

                If objDataSet.Tables(0).Rows.Count > 0 Then
                    blnISNull = True
                Else
                    blnISNull = False
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            doCheckId = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ������_B_���ݿ�_�������������ݼ�(��������������)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strWhere             �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     objFuwuqiData        ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getFuwuqiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objFuwuqiData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempFuwuqiData As Xydc.Platform.Common.Data.AppManagerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '��ʼ��
            getFuwuqiData = False
            objFuwuqiData = Nothing
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strWhere Is Nothing Then strWhere = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                strWhere = strWhere.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ����
                Dim strSQL As String
                Try
                    '�������ݼ�
                    objTempFuwuqiData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_SHUJUKU_FUWUQI)

                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ�м���
                    With Me.m_objSqlDataAdapter
                        '׼��SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from ����_B_���ݿ�_������ a " + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.���� " + vbCr

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        'ִ�в���
                        .Fill(objTempFuwuqiData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_FUWUQI))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempFuwuqiData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempFuwuqiData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objFuwuqiData = objTempFuwuqiData
            getFuwuqiData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempFuwuqiData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���ݷ���������ȡ������_B_���ݿ�_�������������ݼ�(��������������)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strServerName        ����������
        '     strWhere             �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     objFuwuqiData        ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getFuwuqiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strServerName As String, _
            ByVal strWhere As String, _
            ByRef objFuwuqiData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempFuwuqiData As Xydc.Platform.Common.Data.AppManagerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '��ʼ��
            getFuwuqiData = False
            objFuwuqiData = Nothing
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strServerName Is Nothing Then strServerName = ""
                If strWhere Is Nothing Then strWhere = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                strServerName = strServerName.Trim()
                strWhere = strWhere.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ����
                Dim strSQL As String
                Try
                    '�������ݼ�
                    objTempFuwuqiData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_SHUJUKU_FUWUQI)

                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ�м���
                    With Me.m_objSqlDataAdapter
                        '׼��SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from ����_B_���ݿ�_������ a " + vbCr
                        strSQL = strSQL + " where a.���� = @fwqm" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " and " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.���� " + vbCr

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@fwqm", strServerName)
                        .SelectCommand = objSqlCommand

                        'ִ�в���
                        .Fill(objTempFuwuqiData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_FUWUQI))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempFuwuqiData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempFuwuqiData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objFuwuqiData = objTempFuwuqiData
            getFuwuqiData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempFuwuqiData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���ݼ������Ӵ���ȡ���Ӳ���
        '     strErrMsg             ����������򷵻ش�����Ϣ
        '     objConnectionProperty ���û���ʶ
        '     value                 �������ַ����ļ�������
        ' ����
        '     True                  ���ɹ�
        '     False                 ��ʧ��
        '----------------------------------------------------------------
        Public Function getServerConnectionProperty( _
            ByRef strErrMsg As String, _
            ByRef objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal value As Object) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            getServerConnectionProperty = False
            objConnectionProperty = Nothing

            Try
                '��ȡ�����ֽ�����
                Dim bData() As Byte
                bData = objPulicParameters.getObjectValue(value, New Byte(0) {})
                If bData.Length < 1 Then
                    strErrMsg = "����û�����ݣ�"
                    GoTo errProc
                End If

                '��������
                Dim strConnection As String
                If objPulicParameters.doDecryptString(strErrMsg, bData, strConnection) = False Then
                    GoTo errProc
                End If

                '��ȡConnectionProperty
                objConnectionProperty = New Xydc.Platform.Common.Utilities.ConnectionProperty(strConnection)

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            getServerConnectionProperty = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���ݷ���������ȡ���Ӳ���
        '     strErrMsg             ����������򷵻ش�����Ϣ
        '     strUserId             ���û���ʶ
        '     strPassword           ���û�����
        '     strServerName         ����������
        '     objConnectionProperty ���������Ӳ���
        ' ����
        '     True                  ���ɹ�
        '     False                 ��ʧ��
        '----------------------------------------------------------------
        Public Function getServerConnectionProperty( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strServerName As String, _
            ByRef objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objAppManagerData As Xydc.Platform.Common.Data.AppManagerData

            getServerConnectionProperty = False
            objConnectionProperty = Nothing

            Try
                '���ݷ���������ȡ��¼
                Dim bData() As Byte
                If Me.getFuwuqiData(strErrMsg, strUserId, strPassword, strServerName, "", objAppManagerData) = False Then
                    GoTo errProc
                End If
                If objAppManagerData.Tables.Count < 1 Then
                    strErrMsg = "����û�����ݣ�"
                    GoTo errProc
                End If
                If objAppManagerData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_FUWUQI) Is Nothing Then
                    strErrMsg = "����û�����ݣ�"
                    GoTo errProc
                End If
                With objAppManagerData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_FUWUQI)
                    If .Rows.Count < 1 Then
                        strErrMsg = "����û�����ݣ�"
                        GoTo errProc
                    End If

                    '��ȡ�����ֽ�����
                    bData = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_FUWUQI_LJC), New Byte(0) {})
                    If bData.Length < 1 Then
                        strErrMsg = "����û�����ݣ�"
                        GoTo errProc
                    End If
                End With
                Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objAppManagerData)
                objAppManagerData = Nothing

                '��������
                Dim strConnection As String
                If objPulicParameters.doDecryptString(strErrMsg, bData, strConnection) = False Then
                    GoTo errProc
                End If

                '��ȡConnectionProperty
                objConnectionProperty = New Xydc.Platform.Common.Utilities.ConnectionProperty(strConnection)

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objAppManagerData)

            getServerConnectionProperty = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objAppManagerData)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ������_B_���ݿ�_���ݿ⡱�����ݼ�(�Է������������ݿ�����������)
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     objConnectionProperty ����������Ϣ
        '     strWhere                    �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     objShujukuData              ����Ϣ���ݼ�
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function getShujukuData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strWhere As String, _
            ByRef objShujukuData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempShujukuData As Xydc.Platform.Common.Data.AppManagerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '��ʼ��
            getShujukuData = False
            objShujukuData = Nothing
            strErrMsg = ""

            Try
                '���
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim()
                If objConnectionProperty Is Nothing Then
                    '�������ݼ�
                    objTempShujukuData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_SHUJUKU_SHUJUKU)
                    Exit Try
                End If

                '��ȡ����
                With objConnectionProperty

                    'If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, -1, .InitialCatalog, .DataSource) = False Then
                    '    GoTo errProc
                    'End If
                    If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, Platform.Common.jsoaConfiguration.ConnectionTestTimeout, .InitialCatalog, .DataSource) = False Then
                        GoTo errProc
                    End If

                End With

                '��ȡ����
                Dim strSQL As String
                Try
                    '�������ݼ�
                    objTempShujukuData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_SHUJUKU_SHUJUKU)

                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ�м���
                    With Me.m_objSqlDataAdapter
                        If objConnectionProperty.DataSource.ToUpper() = Xydc.Platform.Common.jsoaConfiguration.DatabaseServerName.ToUpper() Then
                            'ͬ������
                            Dim strDefDB As String = Xydc.Platform.Common.jsoaConfiguration.DatabaseServerUserDB
                            '׼��SQL
                            strSQL = ""
                            strSQL = strSQL + " select a.* from (" + vbCr
                            strSQL = strSQL + "   select a.��������,a.���ݿ���," + vbCr
                            strSQL = strSQL + "     ���ݿ�������=case when b.�������� is null then a.���ݿ������� else b.���ݿ������� end," + vbCr
                            strSQL = strSQL + "     ˵��=case when b.�������� is null then a.˵�� else b.˵�� end" + vbCr
                            strSQL = strSQL + "   from (" + vbCr
                            strSQL = strSQL + "     select ��������=@fwqm,���ݿ���=name,���ݿ�������=name,˵��=@sm " + vbCr
                            strSQL = strSQL + "     from master.dbo.sysdatabases" + vbCr
                            strSQL = strSQL + "     where name <> 'tempdb'" + vbCr
                            strSQL = strSQL + "   ) a " + vbCr
                            strSQL = strSQL + "   left join " + strDefDB + ".dbo.����_B_���ݿ�_���ݿ� b on a.�������� = b.�������� and a.���ݿ���=b.���ݿ��� "
                            strSQL = strSQL + " ) a" + vbCr
                            If strWhere <> "" Then
                                strSQL = strSQL + " where " + strWhere + vbCr
                            End If
                            strSQL = strSQL + " order by a.��������,a.���ݿ���" + vbCr
                            '���ò���
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@fwqm", objConnectionProperty.DataSource)
                            objSqlCommand.Parameters.AddWithValue("@sm", " ")
                            .SelectCommand = objSqlCommand
                        Else
                            '��ͬ������
                            '׼��SQL
                            strSQL = ""
                            strSQL = strSQL + " select a.* " + vbCr
                            strSQL = strSQL + " from (" + vbCr
                            strSQL = strSQL + "   select ��������=@fwqm,���ݿ���=name,���ݿ�������=name,˵��=@sm " + vbCr
                            strSQL = strSQL + "   from master.dbo.sysdatabases" + vbCr
                            strSQL = strSQL + "   where name <> 'tempdb'" + vbCr
                            strSQL = strSQL + " ) a " + vbCr
                            If strWhere <> "" Then
                                strSQL = strSQL + " where " + strWhere + vbCr
                            End If
                            strSQL = strSQL + " order by a.��������,a.���ݿ���" + vbCr
                            '���ò���
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@fwqm", objConnectionProperty.DataSource)
                            objSqlCommand.Parameters.AddWithValue("@sm", " ")
                            .SelectCommand = objSqlCommand
                        End If

                        'ִ�в���
                        .Fill(objTempShujukuData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_SHUJUKU))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempShujukuData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempShujukuData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objShujukuData = objTempShujukuData
            getShujukuData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempShujukuData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function


        '----------------------------------------------------------------
        ' ��ȡ������_B_���ݿ�_���󡱵����ݼ�(�����ݿ�����������)
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     objConnectionProperty ����������Ϣ
        '     strWhere                    �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     objDuixiangData             ����Ϣ���ݼ�
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function getDuixiangData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strWhere As String, _
            ByRef objDuixiangData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempDuixiangData As Xydc.Platform.Common.Data.AppManagerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '��ʼ��
            getDuixiangData = False
            objDuixiangData = Nothing
            strErrMsg = ""

            Try
                '���
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim()

                '���ؿ�����
                If objConnectionProperty Is Nothing Then
                    '�������ݼ�
                    objTempDuixiangData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_SHUJUKU_DUIXIANG)
                    Exit Try
                End If

                '��ȡ����
                With objConnectionProperty

                    'If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, -1, .InitialCatalog, .DataSource) = False Then
                    '    GoTo errProc
                    'End If
                    If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, Platform.Common.jsoaConfiguration.ConnectionTestTimeout, .InitialCatalog, .DataSource) = False Then
                        GoTo errProc
                    End If

                End With

                '��ȡ����
                Dim strSQL As String
                Try
                    '�������ݼ�
                    objTempDuixiangData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_SHUJUKU_DUIXIANG)

                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ�м���
                    With Me.m_objSqlDataAdapter
                        Dim strCurDB As String = objConnectionProperty.InitialCatalog
                        Dim strXType As String = Xydc.Platform.Common.Data.AppManagerData.OBJECTTYPELIST
                        If objConnectionProperty.DataSource.ToUpper() = Xydc.Platform.Common.jsoaConfiguration.DatabaseServerName.ToUpper() Then
                            'ͬ������
                            Dim strDefDB As String = Xydc.Platform.Common.jsoaConfiguration.DatabaseServerUserDB
                            '׼��SQL
                            strSQL = ""
                            strSQL = strSQL + " select a.* from (" + vbCr
                            strSQL = strSQL + "   select a.��������,a.���ݿ���,a.��������,a.��������," + vbCr
                            strSQL = strSQL + "     ����������=case when b.�������� is null then a.���������� else b.���������� end," + vbCr
                            strSQL = strSQL + "     ˵��=case when b.�������� is null then a.˵�� else b.˵�� end," + vbCr
                            strSQL = strSQL + "     b.�����ʶ" + vbCr
                            strSQL = strSQL + "   from (" + vbCr
                            strSQL = strSQL + "     select ��������=@fwqm,���ݿ���=@sjkm,��������=name,��������=xtype,����������=name,˵��=@sm " + vbCr
                            strSQL = strSQL + "     from " + strCurDB + ".dbo.sysobjects" + vbCr
                            strSQL = strSQL + "     where xtype in (" + strXType + ")" + vbCr 'ȷ��Ҫ����Ķ���
                            strSQL = strSQL + "     and   status > 0" + vbCr                  '�ų�ϵͳ����
                            strSQL = strSQL + "   ) a " + vbCr
                            strSQL = strSQL + "   left join " + strDefDB + ".dbo.����_B_���ݿ�_���� b on a.�������� = b.�������� and a.���ݿ���=b.���ݿ��� and a.��������=b.�������� and a.��������=b.�������� "
                            strSQL = strSQL + " ) a" + vbCr
                            If strWhere <> "" Then
                                strSQL = strSQL + " where " + strWhere + vbCr
                            End If
                            strSQL = strSQL + " order by a.��������,a.���ݿ���,a.��������,a.��������" + vbCr
                            '���ò���
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@fwqm", objConnectionProperty.DataSource)
                            objSqlCommand.Parameters.AddWithValue("@sjkm", strCurDB)
                            objSqlCommand.Parameters.AddWithValue("@sm", " ")
                            .SelectCommand = objSqlCommand
                        Else
                            '��ͬ������
                            '׼��SQL
                            strSQL = ""
                            strSQL = strSQL + " select a.* " + vbCr
                            strSQL = strSQL + " from (" + vbCr
                            strSQL = strSQL + "   select ��������=@fwqm,���ݿ���=@sjkm,��������=name,��������=xtype,����������=name,˵��=@sm " + vbCr
                            strSQL = strSQL + "   from " + strCurDB + ".dbo.sysobjects" + vbCr
                            strSQL = strSQL + "   where xtype in (" + strXType + ")" + vbCr 'ȷ��Ҫ����Ķ���
                            strSQL = strSQL + "   and   status > 0" + vbCr                  '�ų�ϵͳ����
                            strSQL = strSQL + " ) a " + vbCr
                            If strWhere <> "" Then
                                strSQL = strSQL + " where " + strWhere + vbCr
                            End If
                            strSQL = strSQL + " order by a.��������,a.���ݿ���,a.��������,a.��������" + vbCr
                            '���ò���
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@fwqm", objConnectionProperty.DataSource)
                            objSqlCommand.Parameters.AddWithValue("@sjkm", strCurDB)
                            objSqlCommand.Parameters.AddWithValue("@sm", " ")
                            .SelectCommand = objSqlCommand
                        End If

                        'ִ�в���
                        .Fill(objTempDuixiangData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_DUIXIANG))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDuixiangData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempDuixiangData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objDuixiangData = objTempDuixiangData
            getDuixiangData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempDuixiangData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��顰����_B_���ݿ�_�������������ݵĺϷ���
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     objOldData           ��������
        '     objNewData           ��������
        '     objenumEditType      ���༭����

        ' ����
        '     True                 ���Ϸ�
        '     False                �����Ϸ��������������
        '----------------------------------------------------------------
        Public Function doVerifyFuwuqiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.ListDictionary, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objListDictionary As New System.Collections.Specialized.ListDictionary
            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            doVerifyFuwuqiData = False

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strUserId.Trim = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "����δ�����µ����ݣ�"
                    GoTo errProc
                End If
                Dim strOldMC As String
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                    Case Else
                        If objOldData Is Nothing Then
                            strErrMsg = "����δ����ɵ����ݣ�"
                            GoTo errProc
                        End If
                        strOldMC = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_FUWUQI_MC), "")
                End Select

                '��ȡ��ṹ����
                strSQL = "select top 0 * from ����_B_���ݿ�_������"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, strUserId, strPassword, strSQL, "����_B_���ݿ�_������", objDataSet) = False Then
                    GoTo errProc
                End If

                '������ݳ���
                Dim objDictionaryEntry As System.Collections.DictionaryEntry
                Dim strField As String
                Dim intLen As Integer
                For Each objDictionaryEntry In objNewData
                    strField = objPulicParameters.getObjectValue(objDictionaryEntry.Key, "")
                    Select Case strField
                        Case Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_FUWUQI_LJC
                            Dim bData() As Byte
                            bData = objPulicParameters.getObjectValue(objDictionaryEntry.Value, New Byte(0) {})
                            If bData.Length < 1 Then
                                strErrMsg = "����[" + strField + "]����Ϊ�գ�"
                                GoTo errProc
                            End If
                            Exit Select

                        Case Else
                            Dim strValue As String
                            strValue = objPulicParameters.getObjectValue(objDictionaryEntry.Value, "")
                            If strValue = "" Then
                                Select Case strField
                                    Case Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_FUWUQI_MC, _
                                        Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_FUWUQI_LX, _
                                        Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_FUWUQI_TGZ
                                        strErrMsg = "����[" + strField + "]����Ϊ�գ�"
                                        GoTo errProc
                                End Select
                            End If
                            With objDataSet.Tables(0).Columns(strField)
                                intLen = objPulicParameters.getStringLength(strValue)
                                If intLen > .MaxLength Then
                                    strErrMsg = "����[" + strField + "]���Ȳ��ܳ���[" + .MaxLength.ToString() + "]��ʵ����[" + intLen.ToString() + "]��"
                                    GoTo errProc
                                End If
                            End With
                    End Select
                Next
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '�������
                Dim strMC As String
                strMC = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_FUWUQI_MC), "")
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                        strSQL = "select * from ����_B_���ݿ�_������ where ���� = @mc"
                        objListDictionary.Add("@mc", strMC)
                    Case Else
                        strSQL = "select * from ����_B_���ݿ�_������ where ���� = @mc and ���� <> @oldmc"
                        objListDictionary.Add("@mc", strMC)
                        objListDictionary.Add("@oldmc", strOldMC)
                End Select
                If objdacCommon.getDataSetBySQL(strErrMsg, strUserId, strPassword, strSQL, objListDictionary, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strErrMsg = "����[" + strMC + "]�Ѿ����ڣ�"
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

            doVerifyFuwuqiData = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objListDictionary)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���桰����_B_���ݿ�_��������������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     objOldData           ��������
        '     objNewData           ��������
        '     objenumEditType      ���༭����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doSaveFuwuqiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.ListDictionary, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '��ʼ��
            doSaveFuwuqiData = False
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strUserId.Trim = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "����δ�����µ����ݣ�"
                    GoTo errProc
                End If
                Dim strOldFWQMC As String
                Dim strNewFWQMC As String
                strNewFWQMC = objPulicParameters.getObjectValue(objNewData.Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_FUWUQI_MC), "")
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                    Case Else
                        If objOldData Is Nothing Then
                            strErrMsg = "����δ����ɵ����ݣ�"
                            GoTo errProc
                        End If
                        strOldFWQMC = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_FUWUQI_MC), "")
                End Select

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ʼ����
                Try
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '��������
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '����SQL
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
                            strSQL = strSQL + " insert into ����_B_���ݿ�_������ (" + strFileds + ")"
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
                            strSQL = strSQL + " update ����_B_���ݿ�_������ set "
                            strSQL = strSQL + "   " + strFileds
                            strSQL = strSQL + " where ���� = @oldfwqm"
                            objSqlCommand.Parameters.Clear()
                            i = 0
                            For Each objDictionaryEntry In objNewData
                                objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), objDictionaryEntry.Value)
                                i += 1
                            Next
                            objSqlCommand.Parameters.AddWithValue("@oldfwqm", strOldFWQMC)
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()

                            If strNewFWQMC.ToUpper() <> strOldFWQMC.ToUpper() Then
                                strSQL = ""
                                strSQL = strSQL + " update ����_B_���ݿ�_���ݿ� set "
                                strSQL = strSQL + "   �������� = @newfwqm"
                                strSQL = strSQL + " where �������� = @oldfwqm"
                                objSqlCommand.Parameters.Clear()
                                objSqlCommand.Parameters.AddWithValue("@newfwqm", strNewFWQMC)
                                objSqlCommand.Parameters.AddWithValue("@oldfwqm", strOldFWQMC)
                                objSqlCommand.CommandText = strSQL
                                objSqlCommand.ExecuteNonQuery()

                                strSQL = ""
                                strSQL = strSQL + " update ����_B_���ݿ�_���� set "
                                strSQL = strSQL + "   �������� = @newfwqm"
                                strSQL = strSQL + " where �������� = @oldfwqm"
                                objSqlCommand.Parameters.Clear()
                                objSqlCommand.Parameters.AddWithValue("@newfwqm", strNewFWQMC)
                                objSqlCommand.Parameters.AddWithValue("@oldfwqm", strOldFWQMC)
                                objSqlCommand.CommandText = strSQL
                                objSqlCommand.ExecuteNonQuery()
                            End If
                    End Select

                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '�ύ����
                objSqlTransaction.Commit()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            doSaveFuwuqiData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ɾ��������_B_���ݿ�_��������������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strServerName        ����������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doDeleteFuwuqiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strServerName As String) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '��ʼ��
            doDeleteFuwuqiData = False
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strServerName Is Nothing Then strServerName = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                strServerName = strServerName.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ʼ����
                Try
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                'ɾ������
                Dim strSQL As String
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ɾ������_B_���ݿ�_����
                    strSQL = ""
                    strSQL = strSQL + " delete from ����_B_���ݿ�_���� "
                    strSQL = strSQL + " where �������� = @fwqm"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@fwqm", strServerName)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    'ɾ������_B_���ݿ�_���ݿ�
                    strSQL = ""
                    strSQL = strSQL + " delete from ����_B_���ݿ�_���ݿ� "
                    strSQL = strSQL + " where �������� = @fwqm"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@fwqm", strServerName)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    'ɾ������_B_���ݿ�_������
                    strSQL = ""
                    strSQL = strSQL + " delete from ����_B_���ݿ�_������ "
                    strSQL = strSQL + " where ���� = @fwqm"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@fwqm", strServerName)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '�ύ����
                objSqlTransaction.Commit()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            doDeleteFuwuqiData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���ݷ������������ݿ�����ȡ������_B_���ݿ�_���ݿ⡱�����ݼ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strServerName        ����������
        '     strDBName            �����ݿ���
        '     strWhere             �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     objShujukuData       ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getShujukuData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strServerName As String, _
            ByVal strDBName As String, _
            ByVal strWhere As String, _
            ByRef objShujukuData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempShujukuData As Xydc.Platform.Common.Data.AppManagerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '��ʼ��
            getShujukuData = False
            objShujukuData = Nothing
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strServerName Is Nothing Then strServerName = ""
                If strDBName Is Nothing Then strDBName = ""
                If strWhere Is Nothing Then strWhere = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                strServerName = strServerName.Trim()
                strDBName = strDBName.Trim()
                strWhere = strWhere.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ����
                Dim strSQL As String
                Try
                    '�������ݼ�
                    objTempShujukuData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_SHUJUKU_SHUJUKU)

                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ�м���
                    With Me.m_objSqlDataAdapter
                        '׼��SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from ����_B_���ݿ�_���ݿ� a " + vbCr
                        strSQL = strSQL + " where a.�������� = @fwqm" + vbCr
                        strSQL = strSQL + " and   a.���ݿ��� = @sjkm" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " and " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.��������,a.���ݿ��� " + vbCr

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@fwqm", strServerName)
                        objSqlCommand.Parameters.AddWithValue("@sjkm", strDBName)
                        .SelectCommand = objSqlCommand

                        'ִ�в���
                        .Fill(objTempShujukuData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_SHUJUKU))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempShujukuData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempShujukuData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objShujukuData = objTempShujukuData
            getShujukuData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempShujukuData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���ݷ������������ݿ������������ơ���������
        ' ��ȡ������_B_���ݿ�_���󡱵����ݼ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strServerName        ����������
        '     strDBName            �����ݿ���
        '     strDXLX              �����ݿ��������
        '     strDXMC              �����ݿ������
        '     strWhere             �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     objDuixiangData      ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getDuixiangData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strServerName As String, _
            ByVal strDBName As String, _
            ByVal strDXLX As String, _
            ByVal strDXMC As String, _
            ByVal strWhere As String, _
            ByRef objDuixiangData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempDuixiangData As Xydc.Platform.Common.Data.AppManagerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '��ʼ��
            getDuixiangData = False
            objDuixiangData = Nothing
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strServerName Is Nothing Then strServerName = ""
                If strDBName Is Nothing Then strDBName = ""
                If strDXMC Is Nothing Then strDXMC = ""
                If strDXLX Is Nothing Then strDXLX = ""
                If strWhere Is Nothing Then strWhere = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                strServerName = strServerName.Trim()
                strDBName = strDBName.Trim()
                strDXMC = strDXMC.Trim()
                strDXLX = strDXLX.Trim()
                strWhere = strWhere.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ����
                Dim strSQL As String
                Try
                    '�������ݼ�
                    objTempDuixiangData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_SHUJUKU_DUIXIANG)

                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ�м���
                    With Me.m_objSqlDataAdapter
                        '׼��SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from ����_B_���ݿ�_���� a " + vbCr
                        strSQL = strSQL + " where a.�������� = @fwqm" + vbCr
                        strSQL = strSQL + " and   a.���ݿ��� = @sjkm" + vbCr
                        strSQL = strSQL + " and   a.�������� = @dxmc" + vbCr
                        strSQL = strSQL + " and   a.�������� = @dxlx" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " and " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.��������,a.���ݿ���,a.��������,a.�������� " + vbCr

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@fwqm", strServerName)
                        objSqlCommand.Parameters.AddWithValue("@sjkm", strDBName)
                        objSqlCommand.Parameters.AddWithValue("@dxmc", strDXMC)
                        objSqlCommand.Parameters.AddWithValue("@dxlx", strDXLX)
                        .SelectCommand = objSqlCommand

                        'ִ�в���
                        .Fill(objTempDuixiangData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_DUIXIANG))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDuixiangData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempDuixiangData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objDuixiangData = objTempDuixiangData
            getDuixiangData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempDuixiangData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���ݶ����ʶ��ȡ������_B_���ݿ�_���󡱵����ݼ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     intDXBS              �������ʶ
        '     strWhere             �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     objDuixiangData      ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getDuixiangData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intDXBS As Integer, _
            ByVal strWhere As String, _
            ByRef objDuixiangData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempDuixiangData As Xydc.Platform.Common.Data.AppManagerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '��ʼ��
            getDuixiangData = False
            objDuixiangData = Nothing
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strWhere Is Nothing Then strWhere = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                strWhere = strWhere.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ����
                Dim strSQL As String
                Try
                    '�������ݼ�
                    objTempDuixiangData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_SHUJUKU_DUIXIANG)

                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ�м���
                    With Me.m_objSqlDataAdapter
                        '׼��SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from ����_B_���ݿ�_���� a " + vbCr
                        strSQL = strSQL + " where a.�����ʶ = @dxbs" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " and " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.��������,a.���ݿ���,a.��������,a.�������� " + vbCr

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@dxbs", intDXBS)
                        .SelectCommand = objSqlCommand

                        'ִ�в���
                        .Fill(objTempDuixiangData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_DUIXIANG))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDuixiangData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempDuixiangData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objDuixiangData = objTempDuixiangData
            getDuixiangData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempDuixiangData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��顰����_B_���ݿ�_���ݿ⡱�����ݵĺϷ���
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     objOldData           ��������
        '     objNewData           ��������
        '     objenumEditType      ���༭����

        ' ����
        '     True                 ���Ϸ�
        '     False                �����Ϸ��������������
        '----------------------------------------------------------------
        Public Function doVerifyShujukuData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.ListDictionary, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objListDictionary As New System.Collections.Specialized.ListDictionary
            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            doVerifyShujukuData = False

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strUserId.Trim = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "����δ�����µ����ݣ�"
                    GoTo errProc
                End If
                Dim strOldFWQMC As String
                Dim strOldSJKMC As String
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                    Case Else
                        If objOldData Is Nothing Then
                            strErrMsg = "����δ����ɵ����ݣ�"
                            GoTo errProc
                        End If
                        strOldFWQMC = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_SHUJUKU_FWQM), "")
                        strOldSJKMC = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_SHUJUKU_SJKM), "")
                End Select

                '��ȡ��ṹ����
                strSQL = "select top 0 * from ����_B_���ݿ�_���ݿ�"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, strUserId, strPassword, strSQL, "����_B_���ݿ�_���ݿ�", objDataSet) = False Then
                    GoTo errProc
                End If

                '������ݳ���
                Dim objDictionaryEntry As System.Collections.DictionaryEntry
                Dim strField As String
                Dim intLen As Integer
                For Each objDictionaryEntry In objNewData
                    strField = objPulicParameters.getObjectValue(objDictionaryEntry.Key, "")
                    Select Case strField
                        Case Else
                            Dim strValue As String
                            strValue = objPulicParameters.getObjectValue(objDictionaryEntry.Value, "")
                            If strValue = "" Then
                                Select Case strField
                                    Case Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_SHUJUKU_SM
                                    Case Else
                                        strErrMsg = "����[" + strField + "]����Ϊ�գ�"
                                        GoTo errProc
                                End Select
                            End If
                            With objDataSet.Tables(0).Columns(strField)
                                intLen = objPulicParameters.getStringLength(strValue)
                                If intLen > .MaxLength Then
                                    strErrMsg = "����[" + strField + "]���Ȳ��ܳ���[" + .MaxLength.ToString() + "]��ʵ����[" + intLen.ToString() + "]��"
                                    GoTo errProc
                                End If
                            End With
                    End Select
                Next
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '��飺��������+���ݿ���
                Dim strFWQMC As String
                Dim strSJKMC As String
                strFWQMC = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_SHUJUKU_FWQM), "")
                strSJKMC = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_SHUJUKU_SJKM), "")
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                        strSQL = ""
                        strSQL = strSQL + " select * from ����_B_���ݿ�_���ݿ� "
                        strSQL = strSQL + " where �������� = @fwqm and ���ݿ���=@sjkm"
                        objListDictionary.Add("@fwqm", strFWQMC)
                        objListDictionary.Add("@sjkm", strSJKMC)
                    Case Else
                        strSQL = ""
                        strSQL = strSQL + " select * from ����_B_���ݿ�_���ݿ� "
                        strSQL = strSQL + " where �������� = @fwqm and ���ݿ���=@sjkm "
                        strSQL = strSQL + " and   not (�������� = @oldfwqm and ���ݿ���=@oldsjkm) "
                        objListDictionary.Add("@fwqm", strFWQMC)
                        objListDictionary.Add("@sjkm", strSJKMC)
                        objListDictionary.Add("@oldfwqm", strOldFWQMC)
                        objListDictionary.Add("@oldsjkm", strOldSJKMC)
                End Select
                If objdacCommon.getDataSetBySQL(strErrMsg, strUserId, strPassword, strSQL, objListDictionary, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strErrMsg = "����[" + strFWQMC + "+" + strSJKMC + "]�Ѿ����ڣ�"
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

            doVerifyShujukuData = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objListDictionary)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��顰����_B_���ݿ�_���󡱵����ݵĺϷ���
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     objOldData           ��������
        '     objNewData           ��������
        '     objenumEditType      ���༭����

        ' ����
        '     True                 ���Ϸ�
        '     False                �����Ϸ��������������
        '----------------------------------------------------------------
        Public Function doVerifyDuixiangData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.ListDictionary, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objListDictionary As New System.Collections.Specialized.ListDictionary
            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            doVerifyDuixiangData = False

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strUserId.Trim = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "����δ�����µ����ݣ�"
                    GoTo errProc
                End If
                Dim intOldDXBS As Integer
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                    Case Else
                        If objOldData Is Nothing Then
                            strErrMsg = "����δ����ɵ����ݣ�"
                            GoTo errProc
                        End If
                        intOldDXBS = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_DUIXIANG_DXBS), 0)
                End Select

                '��ȡ��ṹ����
                strSQL = "select top 0 * from ����_B_���ݿ�_����"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, strUserId, strPassword, strSQL, "����_B_���ݿ�_����", objDataSet) = False Then
                    GoTo errProc
                End If

                '������ݳ���
                Dim objDictionaryEntry As System.Collections.DictionaryEntry
                Dim strField As String
                Dim intLen As Integer
                For Each objDictionaryEntry In objNewData
                    strField = objPulicParameters.getObjectValue(objDictionaryEntry.Key, "")
                    Select Case strField
                        Case Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_DUIXIANG_DXBS
                            '�Զ�ֵ�������
                        Case Else
                            Dim strValue As String
                            strValue = objPulicParameters.getObjectValue(objDictionaryEntry.Value, "")
                            If strValue = "" Then
                                Select Case strField
                                    Case Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_DUIXIANG_SM
                                    Case Else
                                        strErrMsg = "����[" + strField + "]����Ϊ�գ�"
                                        GoTo errProc
                                End Select
                            End If
                            With objDataSet.Tables(0).Columns(strField)
                                intLen = objPulicParameters.getStringLength(strValue)
                                If intLen > .MaxLength Then
                                    strErrMsg = "����[" + strField + "]���Ȳ��ܳ���[" + .MaxLength.ToString() + "]��ʵ����[" + intLen.ToString() + "]��"
                                    GoTo errProc
                                End If
                            End With
                    End Select
                Next
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '��飺��������+���ݿ���
                Dim strFWQMC As String
                Dim strSJKMC As String
                Dim strDXMC As String
                Dim strDXLX As String
                strFWQMC = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_DUIXIANG_FWQM), "")
                strSJKMC = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_DUIXIANG_SJKM), "")
                strDXMC = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_DUIXIANG_DXMC), "")
                strDXLX = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_DUIXIANG_DXLX), "")
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                        strSQL = ""
                        strSQL = strSQL + " select * from ����_B_���ݿ�_���� "
                        strSQL = strSQL + " where �������� = @fwqm "
                        strSQL = strSQL + " and   ���ݿ��� = @sjkm"
                        strSQL = strSQL + " and   �������� = @dxlx"
                        strSQL = strSQL + " and   �������� = @dxmc"
                        objListDictionary.Add("@fwqm", strFWQMC)
                        objListDictionary.Add("@sjkm", strSJKMC)
                        objListDictionary.Add("@dxlx", strDXLX)
                        objListDictionary.Add("@dxmc", strDXMC)
                    Case Else
                        strSQL = ""
                        strSQL = strSQL + " select * from ����_B_���ݿ�_���� "
                        strSQL = strSQL + " where �������� = @fwqm "
                        strSQL = strSQL + " and   ���ݿ��� = @sjkm"
                        strSQL = strSQL + " and   �������� = @dxlx"
                        strSQL = strSQL + " and   �������� = @dxmc"
                        strSQL = strSQL + " and   �����ʶ <> @olddxbs "
                        objListDictionary.Add("@fwqm", strFWQMC)
                        objListDictionary.Add("@sjkm", strSJKMC)
                        objListDictionary.Add("@dxlx", strDXLX)
                        objListDictionary.Add("@dxmc", strDXMC)
                        objListDictionary.Add("@olddxbs", intOldDXBS)
                End Select
                If objdacCommon.getDataSetBySQL(strErrMsg, strUserId, strPassword, strSQL, objListDictionary, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strErrMsg = "����[" + strFWQMC + "+" + strSJKMC + "+" + strDXLX + "]+" + strDXMC + "�Ѿ����ڣ�"
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

            doVerifyDuixiangData = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objListDictionary)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���桰����_B_���ݿ�_���ݿ⡱������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     objOldData           ��������
        '     objNewData           ��������
        '     objenumEditType      ���༭����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doSaveShujukuData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.ListDictionary, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '��ʼ��
            doSaveShujukuData = False
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strUserId.Trim = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "����δ�����µ����ݣ�"
                    GoTo errProc
                End If
                Dim strOldFWQMC As String
                Dim strOldSJKMC As String
                Dim strNewFWQMC As String
                Dim strNewSJKMC As String
                strNewFWQMC = objPulicParameters.getObjectValue(objNewData.Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_SHUJUKU_FWQM), "")
                strNewSJKMC = objPulicParameters.getObjectValue(objNewData.Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_SHUJUKU_SJKM), "")
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                    Case Else
                        If objOldData Is Nothing Then
                            strErrMsg = "����δ����ɵ����ݣ�"
                            GoTo errProc
                        End If
                        strOldFWQMC = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_SHUJUKU_FWQM), "")
                        strOldSJKMC = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_SHUJUKU_SJKM), "")
                End Select

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ʼ����
                Try
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '��������
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '����SQL
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
                            strSQL = strSQL + " insert into ����_B_���ݿ�_���ݿ� (" + strFileds + ")"
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
                            strSQL = strSQL + " update ����_B_���ݿ�_���ݿ� set "
                            strSQL = strSQL + "   " + strFileds
                            strSQL = strSQL + " where �������� = @oldfwqm"
                            strSQL = strSQL + " and   ���ݿ��� = @oldsjkm"
                            objSqlCommand.Parameters.Clear()
                            i = 0
                            For Each objDictionaryEntry In objNewData
                                objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), objDictionaryEntry.Value)
                                i += 1
                            Next
                            objSqlCommand.Parameters.AddWithValue("@oldfwqm", strOldFWQMC)
                            objSqlCommand.Parameters.AddWithValue("@oldsjkm", strOldSJKMC)
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()

                            If (strNewFWQMC.ToUpper() = strOldFWQMC.ToUpper() And strNewSJKMC.ToUpper() = strOldSJKMC.ToUpper()) = False Then
                                strSQL = ""
                                strSQL = strSQL + " update ����_B_���ݿ�_���� set "
                                strSQL = strSQL + "   �������� = @newfwqm,"
                                strSQL = strSQL + "   ���ݿ��� = @newsjkm "
                                strSQL = strSQL + " where �������� = @oldfwqm"
                                strSQL = strSQL + " and   ���ݿ��� = @oldsjkm"
                                objSqlCommand.Parameters.Clear()
                                objSqlCommand.Parameters.AddWithValue("@newfwqm", strNewFWQMC)
                                objSqlCommand.Parameters.AddWithValue("@newsjkm", strNewSJKMC)
                                objSqlCommand.Parameters.AddWithValue("@oldfwqm", strOldFWQMC)
                                objSqlCommand.Parameters.AddWithValue("@oldsjkm", strOldSJKMC)
                                objSqlCommand.CommandText = strSQL
                                objSqlCommand.ExecuteNonQuery()
                            End If
                    End Select

                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '�ύ����
                objSqlTransaction.Commit()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            doSaveShujukuData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���桰����_B_���ݿ�_���󡱵�����
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     objOldData           ��������
        '     objNewData           ��������
        '     objenumEditType      ���༭����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doSaveDuixiangData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.ListDictionary, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '��ʼ��
            doSaveDuixiangData = False
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strUserId.Trim = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "����δ�����µ����ݣ�"
                    GoTo errProc
                End If
                Dim intOldDXBS As Integer
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                    Case Else
                        If objOldData Is Nothing Then
                            strErrMsg = "����δ����ɵ����ݣ�"
                            GoTo errProc
                        End If
                        intOldDXBS = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_DUIXIANG_DXBS), 0)
                End Select

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ʼ����
                Try
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '��������
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '����SQL
                    Dim objDictionaryEntry As System.Collections.DictionaryEntry
                    Dim strFileds As String = ""
                    Dim strValues As String = ""
                    Dim strField As String
                    Dim i As Integer = 0
                    Select Case objenumEditType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                            For Each objDictionaryEntry In objNewData
                                Select Case CType(objDictionaryEntry.Key, String)
                                    Case Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_DUIXIANG_DXBS
                                        '�Զ�ֵ
                                    Case Else
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
                                End Select
                            Next
                            strSQL = ""
                            strSQL = strSQL + " insert into ����_B_���ݿ�_���� (" + strFileds + ")"
                            strSQL = strSQL + " values (" + strValues + ")"
                            objSqlCommand.Parameters.Clear()
                            i = 0
                            For Each objDictionaryEntry In objNewData
                                Select Case CType(objDictionaryEntry.Key, String)
                                    Case Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_DUIXIANG_DXBS
                                    Case Else
                                        objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), objDictionaryEntry.Value)
                                        i += 1
                                End Select
                            Next
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()

                        Case Else
                            For Each objDictionaryEntry In objNewData
                                Select Case CType(objDictionaryEntry.Key, String)
                                    Case Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_DUIXIANG_DXBS
                                    Case Else
                                        If strFileds = "" Then
                                            strFileds = objPulicParameters.getObjectValue(objDictionaryEntry.Key, "") + " = @A" + i.ToString()
                                        Else
                                            strFileds = strFileds + "," + objPulicParameters.getObjectValue(objDictionaryEntry.Key, "") + " = @A" + i.ToString()
                                        End If
                                        i += 1
                                End Select
                            Next
                            strSQL = ""
                            strSQL = strSQL + " update ����_B_���ݿ�_���� set "
                            strSQL = strSQL + "   " + strFileds
                            strSQL = strSQL + " where �����ʶ = @oldDXBS"
                            objSqlCommand.Parameters.Clear()
                            i = 0
                            For Each objDictionaryEntry In objNewData
                                Select Case CType(objDictionaryEntry.Key, String)
                                    Case Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_DUIXIANG_DXBS
                                    Case Else
                                        objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), objDictionaryEntry.Value)
                                        i += 1
                                End Select
                            Next
                            objSqlCommand.Parameters.AddWithValue("@oldDXBS", intOldDXBS)
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()
                    End Select

                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '�ύ����
                objSqlTransaction.Commit()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            doSaveDuixiangData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ɾ��������_B_���ݿ�_���ݿ⡱������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strServerName        ����������
        '     strDBName            �����ݿ���
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doDeleteShujukuData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strServerName As String, _
            ByVal strDBName As String) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '��ʼ��
            doDeleteShujukuData = False
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strServerName Is Nothing Then strServerName = ""
                If strDBName Is Nothing Then strDBName = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                strServerName = strServerName.Trim()
                strDBName = strDBName.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ʼ����
                Try
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                'ɾ������
                Dim strSQL As String
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ɾ������_B_���ݿ�_����
                    strSQL = ""
                    strSQL = strSQL + " delete from ����_B_���ݿ�_���� "
                    strSQL = strSQL + " where �������� = @fwqm"
                    strSQL = strSQL + " and   ���ݿ��� = @sjkm"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@fwqm", strServerName)
                    objSqlCommand.Parameters.AddWithValue("@sjkm", strDBName)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    'ɾ������_B_���ݿ�_���ݿ�
                    strSQL = ""
                    strSQL = strSQL + " delete from ����_B_���ݿ�_���ݿ� "
                    strSQL = strSQL + " where �������� = @fwqm"
                    strSQL = strSQL + " and   ���ݿ��� = @sjkm"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@fwqm", strServerName)
                    objSqlCommand.Parameters.AddWithValue("@sjkm", strDBName)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '�ύ����
                objSqlTransaction.Commit()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            doDeleteShujukuData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ɾ��������_B_���ݿ�_���󡱵�����
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strServerName        ����������
        '     strDBName            �����ݿ���
        '     strDXLX              ����������
        '     strDXMC              ����������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doDeleteDuixiangData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strServerName As String, _
            ByVal strDBName As String, _
            ByVal strDXLX As String, _
            ByVal strDXMC As String) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '��ʼ��
            doDeleteDuixiangData = False
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strServerName Is Nothing Then strServerName = ""
                If strDBName Is Nothing Then strDBName = ""
                If strDXLX Is Nothing Then strDXLX = ""
                If strDXMC Is Nothing Then strDXMC = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                strServerName = strServerName.Trim()
                strDBName = strDBName.Trim()
                strDXLX = strDXLX.Trim()
                strDXMC = strDXMC.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ʼ����
                Try
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                'ɾ������
                Dim strSQL As String
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ɾ������_B_���ݿ�_����
                    strSQL = ""
                    strSQL = strSQL + " delete from ����_B_���ݿ�_���� "
                    strSQL = strSQL + " where �������� = @fwqm"
                    strSQL = strSQL + " and   ���ݿ��� = @sjkm"
                    strSQL = strSQL + " and   �������� = @dxlx"
                    strSQL = strSQL + " and   �������� = @dxmc"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@fwqm", strServerName)
                    objSqlCommand.Parameters.AddWithValue("@sjkm", strDBName)
                    objSqlCommand.Parameters.AddWithValue("@dxlx", strDXLX)
                    objSqlCommand.Parameters.AddWithValue("@dxmc", strDXMC)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '�ύ����
                objSqlTransaction.Commit()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            doDeleteDuixiangData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ɾ��������_B_���ݿ�_���󡱵�����
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     intDXBS              �������ʶ
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doDeleteDuixiangData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intDXBS As Integer) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '��ʼ��
            doDeleteDuixiangData = False
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ʼ����
                Try
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                'ɾ������
                Dim strSQL As String
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ɾ������_B_���ݿ�_����
                    strSQL = ""
                    strSQL = strSQL + " delete from ����_B_���ݿ�_���� "
                    strSQL = strSQL + " where �����ʶ = @dxbs"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@dxbs", intDXBS)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '�ύ����
                objSqlTransaction.Commit()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            doDeleteDuixiangData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �Զ��������_B_���ݿ�_���ݿ⡢����_B_���ݿ�_�����е���Ч����
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doAutoCleanManageData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '��ʼ��
            doAutoCleanManageData = False
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ʼ����
                Try
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '��������
                Dim strSQL As String
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ɾ������_B_���ݿ�_����
                    strSQL = ""
                    strSQL = strSQL + " delete ����_B_���ݿ�_���� " + vbCr
                    strSQL = strSQL + " from ����_B_���ݿ�_���� a " + vbCr
                    strSQL = strSQL + " left join " + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select ��������=���� " + vbCr
                    strSQL = strSQL + "   from ����_B_���ݿ�_������ " + vbCr
                    strSQL = strSQL + "   group by ����" + vbCr
                    strSQL = strSQL + " ) b on a.�������� = b.�������� " + vbCr
                    strSQL = strSQL + " where b.�������� is null " + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    strSQL = ""
                    strSQL = strSQL + " delete ����_B_���ݿ�_���� " + vbCr
                    strSQL = strSQL + " from ����_B_���ݿ�_���� a " + vbCr
                    strSQL = strSQL + " left join " + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select ��������,���ݿ��� " + vbCr
                    strSQL = strSQL + "   from ����_B_���ݿ�_���ݿ� " + vbCr
                    strSQL = strSQL + "   group by ��������,���ݿ���" + vbCr
                    strSQL = strSQL + " ) b on a.�������� = b.�������� and a.���ݿ���=b.���ݿ��� " + vbCr
                    strSQL = strSQL + " where b.�������� is null " + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    'ɾ������_B_���ݿ�_����
                    strSQL = ""
                    strSQL = strSQL + " delete ����_B_���ݿ�_���ݿ� " + vbCr
                    strSQL = strSQL + " from ����_B_���ݿ�_���ݿ� a " + vbCr
                    strSQL = strSQL + " left join " + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select ��������=���� " + vbCr
                    strSQL = strSQL + "   from ����_B_���ݿ�_������ " + vbCr
                    strSQL = strSQL + "   group by ����" + vbCr
                    strSQL = strSQL + " ) b on a.�������� = b.�������� " + vbCr
                    strSQL = strSQL + " where b.�������� is null " + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '�ύ����
                objSqlTransaction.Commit()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            doAutoCleanManageData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡָ��objConnectionProperty�е����ݿ��ɫ
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     objConnectionProperty ����������Ϣ
        '     strWhere                    �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     objRoleData                 ����Ϣ���ݼ�
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function getRoleData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strWhere As String, _
            ByRef objRoleData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempRoleData As Xydc.Platform.Common.Data.AppManagerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '��ʼ��
            getRoleData = False
            objRoleData = Nothing
            strErrMsg = ""

            Try
                '���
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim()
                If objConnectionProperty Is Nothing Then
                    '�������ݼ�
                    objTempRoleData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_SHUJUKU_JIAOSE)
                    Exit Try
                End If

                '��ȡ����
                With objConnectionProperty

                    'If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, -1, .InitialCatalog, .DataSource) = False Then
                    '    GoTo errProc
                    'End If
                    If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, Platform.Common.jsoaConfiguration.ConnectionTestTimeout, .InitialCatalog, .DataSource) = False Then
                        GoTo errProc
                    End If

                End With

                '��ȡ����
                Dim strSQL As String
                Try
                    '�������ݼ�
                    objTempRoleData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_SHUJUKU_JIAOSE)

                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ�м���
                    With Me.m_objSqlDataAdapter
                        '׼��SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.uid,a.name " + vbCr
                        strSQL = strSQL + " from " + objConnectionProperty.InitialCatalog + ".dbo.sysusers a" + vbCr
                        strSQL = strSQL + " where issqlrole = 1" + vbCr   '��ɫ
                        strSQL = strSQL + " and gid > 0" + vbCr           '��ϵͳ��ɫ
                        If strWhere <> "" Then
                            strSQL = strSQL + " and " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.name"

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        'ִ�в���
                        .Fill(objTempRoleData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_JIAOSE))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempRoleData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempRoleData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objRoleData = objTempRoleData
            getRoleData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempRoleData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ��Ա�Ѿ����뵽��ɫstrRoleName���б�
        '----------------------------------------------------------------
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     objConnectionProperty       ����������Ϣ
        '     strWhere                    �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     objRoleData                 ����Ϣ���ݼ�
        '     blnNone                     ������
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��

        '----------------------------------------------------------------
        Public Function getRoleData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strWhere As String, _
            ByRef objRoleData As Xydc.Platform.Common.Data.AppManagerData, _
            ByVal blnNone As Boolean) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempRoleData As Xydc.Platform.Common.Data.AppManagerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '��ʼ��
            getRoleData = False
            objRoleData = Nothing
            strErrMsg = ""

            Try
                '���
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim()
                If objConnectionProperty Is Nothing Then
                    '�������ݼ�
                    objTempRoleData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_SHUJUKU_JIAOSE)
                    Exit Try
                End If

                '��ȡ����
                With objConnectionProperty

                    'If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, -1, .InitialCatalog, .DataSource) = False Then
                    '    GoTo errProc
                    'End If
                    If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, Platform.Common.jsoaConfiguration.ConnectionTestTimeout, .InitialCatalog, .DataSource) = False Then
                        GoTo errProc
                    End If

                End With

                '��ȡ����
                Dim strSQL As String
                Try
                    '�������ݼ�
                    objTempRoleData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_SHUJUKU_JIAOSE)

                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ�м���
                    With Me.m_objSqlDataAdapter
                        Dim strDefDB As String = Xydc.Platform.Common.jsoaConfiguration.DatabaseServerUserDB
                        Dim strDatabase As String = objConnectionProperty.InitialCatalog

                        '׼��SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.gid as 'UID',a.rollname as 'NAME' from ( " + vbCr
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
                        strSQL = strSQL + " order by a.name"

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        'ִ�в���
                        .Fill(objTempRoleData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_JIAOSE))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempRoleData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempRoleData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objRoleData = objTempRoleData
            getRoleData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempRoleData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ�Ѿ����뵽��ɫstrRoleName����Ա�б�(����Ա��ȫ����������)
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     objConnectionProperty       ����������Ϣ
        '     strRoleName                 ����ɫ��
        '     strWhere                    �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     objRenyuanData              ��ָ����֯�����µ���Ա��Ϣ���ݼ�
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function getRenyuanInRoleData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strRoleName As String, _
            ByVal strWhere As String, _
            ByRef objRenyuanData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempRenyuanData As Xydc.Platform.Common.Data.CustomerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '��ʼ��
            getRenyuanInRoleData = False
            objRenyuanData = Nothing
            strErrMsg = ""

            Try
                '���
                If strRoleName Is Nothing Then strRoleName = ""
                If strWhere Is Nothing Then strWhere = ""
                strRoleName = strRoleName.Trim()
                strWhere = strWhere.Trim()
                If objConnectionProperty Is Nothing Then
                    '�������ݼ�
                    objTempRenyuanData = New Xydc.Platform.Common.Data.CustomerData(Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_RENYUAN_FULLJOIN)
                    Exit Try
                End If

                '��ͬ������
                If objConnectionProperty.DataSource.ToUpper() <> Xydc.Platform.Common.jsoaConfiguration.DatabaseServerName.ToUpper() Then
                    '�������ݼ�
                    objTempRenyuanData = New Xydc.Platform.Common.Data.CustomerData(Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_RENYUAN_FULLJOIN)
                    Exit Try
                End If

                '��ȡ����
                With objConnectionProperty

                    'If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, -1, .InitialCatalog, .DataSource) = False Then
                    '    GoTo errProc
                    'End If
                    If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, Platform.Common.jsoaConfiguration.ConnectionTestTimeout, .InitialCatalog, .DataSource) = False Then
                        GoTo errProc
                    End If

                End With

                '��ȡ����
                Dim strSQL As String
                Try
                    '�������ݼ�
                    objTempRenyuanData = New Xydc.Platform.Common.Data.CustomerData(Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_RENYUAN_FULLJOIN)

                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ�м���
                    With Me.m_objSqlDataAdapter
                        Dim strDefDB As String = Xydc.Platform.Common.jsoaConfiguration.DatabaseServerUserDB
                        Dim strDatabase As String = objConnectionProperty.InitialCatalog

                        '׼��SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* from ("
                        strSQL = strSQL + "   select a.*," + vbCr
                        strSQL = strSQL + "     b.��֯����,b.��֯����," + vbCr
                        strSQL = strSQL + "     ��λ�б� = " + strDefDB + ".dbo.GetGWMCByRydm(a.��Ա����,@separate)," + vbCr
                        strSQL = strSQL + "     c.��������,c.��������," + vbCr
                        strSQL = strSQL + "     �������� = d.��Ա����," + vbCr
                        strSQL = strSQL + "     ������ת������ = e.��Ա����," + vbCr
                        strSQL = strSQL + "     �Ƿ����� = @charfalse" + vbCr
                        strSQL = strSQL + "   from " + strDefDB + ".dbo.����_B_��Ա a " + vbCr
                        strSQL = strSQL + "   left join " + strDefDB + ".dbo.����_B_��֯���� b on a.��֯����   = b.��֯���� " + vbCr
                        strSQL = strSQL + "   left join " + strDefDB + ".dbo.����_B_�������� c on a.�������   = c.������� " + vbCr
                        strSQL = strSQL + "   left join " + strDefDB + ".dbo.����_B_��Ա     d on a.�������   = d.��Ա���� " + vbCr
                        strSQL = strSQL + "   left join " + strDefDB + ".dbo.����_B_��Ա     e on a.������ת�� = e.��Ա���� " + vbCr
                        strSQL = strSQL + "   left join" + vbCr
                        strSQL = strSQL + "   (" + vbCr
                        strSQL = strSQL + "     select c.name" + vbCr
                        strSQL = strSQL + "     from " + strDatabase + ".dbo.sysmembers a " + vbCr
                        strSQL = strSQL + "     left join " + vbCr
                        strSQL = strSQL + "     (" + vbCr
                        strSQL = strSQL + "       select gid from " + strDatabase + ".dbo.sysusers " + vbCr
                        strSQL = strSQL + "       where issqlrole=1 and gid>0" + vbCr
                        strSQL = strSQL + "       and name = @rolename" + vbCr
                        strSQL = strSQL + "     ) b on a.groupuid = b.gid" + vbCr
                        strSQL = strSQL + "     left join " + strDatabase + ".dbo.sysusers c on a.memberuid = c.uid" + vbCr
                        strSQL = strSQL + "     where b.gid is not null" + vbCr
                        strSQL = strSQL + "     and c.uid is not null" + vbCr
                        strSQL = strSQL + "   ) f on a.��Ա���� = f.name" + vbCr
                        strSQL = strSQL + "   where f.name is not null" + vbCr        '��ɫ��
                        strSQL = strSQL + " ) a " + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.��֯����, cast(a.��Ա��� as integer)"

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@separate", Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate)
                        objSqlCommand.Parameters.AddWithValue("@charfalse", Xydc.Platform.Common.Utilities.PulicParameters.CharFalse)
                        objSqlCommand.Parameters.AddWithValue("@rolename", strRoleName)
                        .SelectCommand = objSqlCommand

                        'ִ�в���
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

            '����
            objRenyuanData = objTempRenyuanData
            getRenyuanInRoleData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempRenyuanData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡû�м��뵽��ɫstrRoleName����Ա�б�(����Ա��ȫ����������)
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     objConnectionProperty       ����������Ϣ
        '     strRoleName                 ����ɫ��
        '     strWhere                    �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     objRenyuanData              ��ָ����֯�����µ���Ա��Ϣ���ݼ�
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function getRenyuanNotInRoleData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strRoleName As String, _
            ByVal strWhere As String, _
            ByRef objRenyuanData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempRenyuanData As Xydc.Platform.Common.Data.CustomerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '��ʼ��
            getRenyuanNotInRoleData = False
            objRenyuanData = Nothing
            strErrMsg = ""

            Try
                '���
                If strRoleName Is Nothing Then strRoleName = ""
                If strWhere Is Nothing Then strWhere = ""
                strRoleName = strRoleName.Trim()
                strWhere = strWhere.Trim()
                If objConnectionProperty Is Nothing Then
                    '�������ݼ�
                    objTempRenyuanData = New Xydc.Platform.Common.Data.CustomerData(Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_RENYUAN_FULLJOIN)
                    Exit Try
                End If

                '��ͬ������
                If objConnectionProperty.DataSource.ToUpper() <> Xydc.Platform.Common.jsoaConfiguration.DatabaseServerName.ToUpper() Then
                    '�������ݼ�
                    objTempRenyuanData = New Xydc.Platform.Common.Data.CustomerData(Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_RENYUAN_FULLJOIN)
                    Exit Try
                End If

                '��ȡ����
                With objConnectionProperty

                    'If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, -1, .InitialCatalog, .DataSource) = False Then
                    '    GoTo errProc
                    'End If
                    If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, Platform.Common.jsoaConfiguration.ConnectionTestTimeout, .InitialCatalog, .DataSource) = False Then
                        GoTo errProc
                    End If

                End With

                '��ȡ����
                Dim strSQL As String
                Try
                    '�������ݼ�
                    objTempRenyuanData = New Xydc.Platform.Common.Data.CustomerData(Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_RENYUAN_FULLJOIN)

                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ�м���
                    With Me.m_objSqlDataAdapter
                        Dim strDefDB As String = Xydc.Platform.Common.jsoaConfiguration.DatabaseServerUserDB
                        Dim strDatabase As String = objConnectionProperty.InitialCatalog

                        '׼��SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* from ("
                        strSQL = strSQL + "   select a.*," + vbCr
                        strSQL = strSQL + "     b.��֯����,b.��֯����," + vbCr
                        strSQL = strSQL + "     ��λ�б� = " + strDefDB + ".dbo.GetGWMCByRydm(a.��Ա����,@separate)," + vbCr
                        strSQL = strSQL + "     c.��������,c.��������," + vbCr
                        strSQL = strSQL + "     �������� = d.��Ա����," + vbCr
                        strSQL = strSQL + "     ������ת������ = e.��Ա����," + vbCr
                        strSQL = strSQL + "     �Ƿ����� = @charfalse" + vbCr
                        strSQL = strSQL + "   from " + strDefDB + ".dbo.����_B_��Ա a " + vbCr
                        strSQL = strSQL + "   left join " + strDefDB + ".dbo.����_B_��֯���� b on a.��֯����   = b.��֯���� " + vbCr
                        strSQL = strSQL + "   left join " + strDefDB + ".dbo.����_B_�������� c on a.�������   = c.������� " + vbCr
                        strSQL = strSQL + "   left join " + strDefDB + ".dbo.����_B_��Ա     d on a.�������   = d.��Ա���� " + vbCr
                        strSQL = strSQL + "   left join " + strDefDB + ".dbo.����_B_��Ա     e on a.������ת�� = e.��Ա���� " + vbCr
                        strSQL = strSQL + "   left join" + vbCr
                        strSQL = strSQL + "   (" + vbCr
                        strSQL = strSQL + "     select c.name" + vbCr
                        strSQL = strSQL + "     from " + strDatabase + ".dbo.sysmembers a " + vbCr
                        strSQL = strSQL + "     left join " + vbCr
                        strSQL = strSQL + "     (" + vbCr
                        strSQL = strSQL + "       select gid from " + strDatabase + ".dbo.sysusers " + vbCr
                        strSQL = strSQL + "       where issqlrole=1 and gid>0" + vbCr
                        strSQL = strSQL + "       and name = @rolename" + vbCr
                        strSQL = strSQL + "     ) b on a.groupuid = b.gid" + vbCr
                        strSQL = strSQL + "     left join " + strDatabase + ".dbo.sysusers c on a.memberuid = c.uid" + vbCr
                        strSQL = strSQL + "     where b.gid is not null" + vbCr
                        strSQL = strSQL + "     and c.uid is not null" + vbCr
                        strSQL = strSQL + "   ) f on a.��Ա���� = f.name" + vbCr
                        strSQL = strSQL + "   left join " + strDatabase + ".dbo.sysusers g on a.��Ա���� = g.name" + vbCr
                        strSQL = strSQL + "   where f.name is null" + vbCr             '���ڽ�ɫ��
                        strSQL = strSQL + "   and   g.name is not null" + vbCr         '�Ѿ���Ȩ��ȡ����Ա
                        strSQL = strSQL + " ) a " + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.��֯����, cast(a.��Ա��� as integer)"

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@separate", Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate)
                        objSqlCommand.Parameters.AddWithValue("@charfalse", Xydc.Platform.Common.Utilities.PulicParameters.CharFalse)
                        objSqlCommand.Parameters.AddWithValue("@rolename", strRoleName)
                        .SelectCommand = objSqlCommand

                        'ִ�в���
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

            '����
            objRenyuanData = objTempRenyuanData
            getRenyuanNotInRoleData = True
            Exit Function

            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempRenyuanData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ָ��������objConnectionProperty�д�����ɫstrRoleName
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     objConnectionProperty       ����������Ϣ
        '     strRoleName                 ����ɫ��
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function doAddRole( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strRoleName As String) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '��ʼ��
            doAddRole = False
            strErrMsg = ""

            Try
                '���
                If strRoleName Is Nothing Then strRoleName = ""
                strRoleName = strRoleName.Trim()
                If objConnectionProperty Is Nothing Then
                    strErrMsg = "����δָ��������������"
                    GoTo errProc
                End If

                '��ȡ����
                With objConnectionProperty

                    'If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, -1, .InitialCatalog, .DataSource) = False Then
                    '    GoTo errProc
                    'End If
                    If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, Platform.Common.jsoaConfiguration.ConnectionTestTimeout, .InitialCatalog, .DataSource) = False Then
                        GoTo errProc
                    End If

                End With

                '��ȡ����
                Dim strSQL As String
                Try
                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ�в���
                    strSQL = "exec sp_addrole @rolename"
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@rolename", strRoleName)
                    objSqlCommand.ExecuteNonQuery()

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
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            doAddRole = True
            Exit Function

            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ָ��������objConnectionProperty��ɾ����ɫstrRoleName
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     objConnectionProperty       ����������Ϣ
        '     strRoleName                 ����ɫ��
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function doDropRole( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strRoleName As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '��ʼ��
            doDropRole = False
            strErrMsg = ""

            Try
                '���
                If strRoleName Is Nothing Then strRoleName = ""
                strRoleName = strRoleName.Trim()
                If objConnectionProperty Is Nothing Then
                    strErrMsg = "����δָ��������������"
                    GoTo errProc
                End If

                '��ȡ����
                With objConnectionProperty

                    'If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, -1, .InitialCatalog, .DataSource) = False Then
                    '    GoTo errProc
                    'End If
                    If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, Platform.Common.jsoaConfiguration.ConnectionTestTimeout, .InitialCatalog, .DataSource) = False Then
                        GoTo errProc
                    End If

                End With

                '��ȡ����
                Dim strSQL As String
                Try
                    Dim strDBName As String = objConnectionProperty.InitialCatalog

                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '��ȡ��ɫ��Ա
                    strSQL = ""
                    strSQL = strSQL + " select c.name" + vbCr
                    strSQL = strSQL + " from " + strDBName + ".dbo.sysmembers a" + vbCr
                    strSQL = strSQL + " left join " + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select uid" + vbCr
                    strSQL = strSQL + "   from " + strDBName + ".dbo.sysusers" + vbCr
                    strSQL = strSQL + "   where issqlrole = 1 " + vbCr
                    strSQL = strSQL + "   and   gid > 0" + vbCr
                    strSQL = strSQL + "   and   name = @rolename" + vbCr
                    strSQL = strSQL + " ) b on a.groupuid = b.uid" + vbCr
                    strSQL = strSQL + " left join " + strDBName + ".dbo.sysusers c on a.memberuid = c.uid" + vbCr
                    strSQL = strSQL + " where b.uid is not null" + vbCr
                    strSQL = strSQL + " and   c.uid is not null" + vbCr
                    Dim objListDictionary As New System.Collections.Specialized.ListDictionary
                    Dim objDataSet As System.Data.DataSet
                    objListDictionary.Clear()
                    objListDictionary.Add("@rolename", strRoleName)
                    If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objListDictionary, objDataSet) = False Then
                        GoTo errProc
                    End If

                    '���ɾ����ɫ��Ա
                    With objDataSet.Tables(0)
                        Dim intCount As Integer = .Rows.Count
                        Dim strName As String
                        Dim i As Integer
                        For i = 0 To intCount - 1 Step 1
                            strName = objPulicParameters.getObjectValue(.Rows(i).Item("name"), "")
                            strSQL = "exec sp_droprolemember @rolename, @membername"
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@rolename", strRoleName)
                            objSqlCommand.Parameters.AddWithValue("@membername", strName)
                            objSqlCommand.ExecuteNonQuery()
                        Next
                    End With
                    objListDictionary.Clear()
                    Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objListDictionary)
                    Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                    objDataSet = Nothing

                    'ɾ����ɫ
                    strSQL = "exec sp_droprole @rolename"
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@rolename", strRoleName)
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            doDropRole = True
            Exit Function

            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ָ��������objConnectionPropertyָ����ɫstrRoleName�м����Ա
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     objConnectionProperty       ����������Ϣ
        '     strRoleName                 ����ɫ��
        '     strMemberName               ����Ա��
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function doAddRoleMember( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strRoleName As String, _
            ByVal strMemberName As String) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '��ʼ��
            doAddRoleMember = False
            strErrMsg = ""

            Try
                '���
                If strRoleName Is Nothing Then strRoleName = ""
                If strMemberName Is Nothing Then strMemberName = ""
                strRoleName = strRoleName.Trim()
                strMemberName = strMemberName.Trim()
                If objConnectionProperty Is Nothing Then
                    strErrMsg = "����δָ��������������"
                    GoTo errProc
                End If

                '��ȡ����
                With objConnectionProperty

                    'If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, -1, .InitialCatalog, .DataSource) = False Then
                    '    GoTo errProc
                    'End If
                    If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, Platform.Common.jsoaConfiguration.ConnectionTestTimeout, .InitialCatalog, .DataSource) = False Then
                        GoTo errProc
                    End If

                End With

                '��ȡ����
                Dim strSQL As String
                Try
                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ�в���
                    strSQL = "exec sp_addrolemember @rolename, @membername"
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@rolename", strRoleName)
                    objSqlCommand.Parameters.AddWithValue("@membername", strMemberName)
                    objSqlCommand.ExecuteNonQuery()

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
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            doAddRoleMember = True
            Exit Function

            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '-------------------------------------------------------------------------------------------
        ' ��ָ��������objConnectionPropertyָ����ԱstrUserId�����ɫ(m_objNewDataSet_ChoiceRole)��
        '-------------------------------------------------------------------------------------------
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     objConnectionProperty       ����������Ϣ
        '     strUserId                   ��ָ����Ա
        '     m_objNewDataSet_ChoiceRole  �����½�ɫ���ݼ�
        '     m_objOldDataSet_ChoiceRole  ��ԭ��ɫ���ݼ�
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��

        '----------------------------------------------------------------
        Public Function doAddRoleMember( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strUserId As String, _
            ByVal m_objNewDataSet_ChoiceRole As Xydc.Platform.Common.Data.AppManagerData, _
            ByVal m_objOldDataSet_ChoiceRole As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim strRoleName As String

            '��ʼ��
            doAddRoleMember = False
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim()
                If objConnectionProperty Is Nothing Then
                    strErrMsg = "����δָ��������������"
                    GoTo errProc
                End If

                '��ȡ����
                With objConnectionProperty

                    'If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, -1, .InitialCatalog, .DataSource) = False Then
                    '    GoTo errProc
                    'End If
                    If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, Platform.Common.jsoaConfiguration.ConnectionTestTimeout, .InitialCatalog, .DataSource) = False Then
                        GoTo errProc
                    End If

                End With

                '��ȡ����
                Dim strSQL As String
                Dim intNewCount As Integer
                Dim intOldCount As Integer
                Dim i As Integer
                Try
                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ��ɾ������
                    With m_objOldDataSet_ChoiceRole.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_JIAOSE)
                        intOldCount = .Rows.Count
                        For i = 0 To intOldCount - 1 Step 1
                            strRoleName = ""
                            strRoleName = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_JIAOSE_NAME), " ")
                            strSQL = "exec sp_droprolemember @rolename, @membername"
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@rolename", strRoleName)
                            objSqlCommand.Parameters.AddWithValue("@membername", strUserId)
                            objSqlCommand.ExecuteNonQuery()
                        Next i
                    End With

                    'ִ�м������
                    With m_objNewDataSet_ChoiceRole.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_JIAOSE)
                        intNewCount = .Rows.Count
                        For i = 0 To intNewCount - 1 Step 1
                            If .Rows(i).RowState <> DataRowState.Deleted Then
                                strRoleName = ""
                                strRoleName = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_SHUJUKU_JIAOSE_NAME), " ")


                                strSQL = "exec sp_addrolemember @rolename, @membername"
                                objSqlCommand.CommandText = strSQL
                                objSqlCommand.Parameters.Clear()
                                objSqlCommand.Parameters.AddWithValue("@rolename", strRoleName)
                                objSqlCommand.Parameters.AddWithValue("@membername", strUserId)
                                objSqlCommand.ExecuteNonQuery()
                            End If
                        Next i
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            doAddRoleMember = True
            Exit Function

            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ָ��������objConnectionPropertyָ����ɫstrRoleName��ɾ����Ա
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     objConnectionProperty       ����������Ϣ
        '     strRoleName                 ����ɫ��
        '     strMemberName               ����Ա��
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function doDropRoleMember( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strRoleName As String, _
            ByVal strMemberName As String) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '��ʼ��
            doDropRoleMember = False
            strErrMsg = ""

            Try
                '���
                If strRoleName Is Nothing Then strRoleName = ""
                If strMemberName Is Nothing Then strMemberName = ""
                strRoleName = strRoleName.Trim()
                strMemberName = strMemberName.Trim()
                If objConnectionProperty Is Nothing Then
                    strErrMsg = "����δָ��������������"
                    GoTo errProc
                End If

                '��ȡ����
                With objConnectionProperty

                    'If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, -1, .InitialCatalog, .DataSource) = False Then
                    '    GoTo errProc
                    'End If
                    If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, Platform.Common.jsoaConfiguration.ConnectionTestTimeout, .InitialCatalog, .DataSource) = False Then
                        GoTo errProc
                    End If

                End With

                '��ȡ����
                Dim strSQL As String
                Try
                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ�в���
                    strSQL = "exec sp_droprolemember @rolename, @membername"
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@rolename", strRoleName)
                    objSqlCommand.Parameters.AddWithValue("@membername", strMemberName)
                    objSqlCommand.ExecuteNonQuery()

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
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            doDropRoleMember = True
            Exit Function

            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ��ɫ��Ȩ����������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objConnectionProperty�����Ӳ���
        '     strRoleName          ����ɫ��
        '     strWhere             �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     objRoleQXData        ����ɫȨ������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getRolePermissionsData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strRoleName As String, _
            ByVal strWhere As String, _
            ByRef objRoleQXData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempRoleQXData As Xydc.Platform.Common.Data.AppManagerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '��ʼ��
            getRolePermissionsData = False
            objRoleQXData = Nothing
            strErrMsg = ""

            Try
                '���
                If strRoleName Is Nothing Then strRoleName = ""
                If strWhere Is Nothing Then strWhere = ""
                strRoleName = strRoleName.Trim()
                strWhere = strWhere.Trim()
                If objConnectionProperty Is Nothing Then
                    objTempRoleQXData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_SHUJUKU_DUIXIANGQX)
                    Exit Try
                End If

                '��ȡ����
                With objConnectionProperty

                    'If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, -1, .InitialCatalog, .DataSource) = False Then
                    '    GoTo errProc
                    'End If
                    If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, Platform.Common.jsoaConfiguration.ConnectionTestTimeout, .InitialCatalog, .DataSource) = False Then
                        GoTo errProc
                    End If

                End With

                '��ȡ����
                Dim strSQL As String
                Try
                    '�������ݼ�
                    objTempRoleQXData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_SHUJUKU_DUIXIANGQX)

                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ�м���
                    With Me.m_objSqlDataAdapter
                        Dim strDefServer As String = Xydc.Platform.Common.jsoaConfiguration.DatabaseServerName
                        Dim strDefDB As String = Xydc.Platform.Common.jsoaConfiguration.DatabaseServerUserDB
                        Dim strCurServer As String = objConnectionProperty.DataSource
                        Dim strCurDB As String = objConnectionProperty.InitialCatalog
                        Dim strXType As String = Xydc.Platform.Common.Data.AppManagerData.OBJECTTYPELIST

                        If strCurServer.ToUpper() = strDefServer.ToUpper() Then
                            'ͬһ������

                            '׼��SQL
                            strSQL = ""
                            strSQL = strSQL + " select a.*" + vbCr
                            strSQL = strSQL + " from" + vbCr
                            strSQL = strSQL + " (" + vbCr
                            strSQL = strSQL + "   select " + vbCr
                            strSQL = strSQL + "     a.��������,a.��������," + vbCr
                            strSQL = strSQL + "     ���������� = case when c.�������� is null then a.�������� else c.���������� end," + vbCr
                            strSQL = strSQL + "     ѡ��Ȩ = case when b.ѡ��Ȩ=1 then @True else @False end,"
                            strSQL = strSQL + "     �༭Ȩ = case when b.�༭Ȩ=1 then @True else @False end,"
                            strSQL = strSQL + "     ����Ȩ = case when b.����Ȩ=1 then @True else @False end,"
                            strSQL = strSQL + "     ɾ��Ȩ = case when b.ɾ��Ȩ=1 then @True else @False end,"
                            strSQL = strSQL + "     ִ��Ȩ = case when b.ִ��Ȩ=1 then @True else @False end "
                            strSQL = strSQL + "   from " + vbCr
                            strSQL = strSQL + "   (  " + vbCr
                            strSQL = strSQL + "     select ��������=name,��������=xtype" + vbCr
                            strSQL = strSQL + "     from " + strCurDB + ".dbo.sysobjects " + vbCr
                            strSQL = strSQL + "     where xtype in (" + strXType + ")" + vbCr
                            strSQL = strSQL + "   ) a " + vbCr
                            strSQL = strSQL + "   left join " + vbCr
                            strSQL = strSQL + "   (" + vbCr
                            strSQL = strSQL + " select ��������,��������,ѡ��Ȩ=sum(ѡ��Ȩ),�༭Ȩ=sum(�༭Ȩ),����Ȩ=sum(����Ȩ),ɾ��Ȩ=sum(ɾ��Ȩ),ִ��Ȩ=sum(ִ��Ȩ) from"
                            strSQL = strSQL + "  ("
                            strSQL = strSQL + "     select "
                            strSQL = strSQL + "     �������� = b.name,"
                            strSQL = strSQL + "     �������� = b.xtype,"
                            strSQL = strSQL + "     ѡ��Ȩ   = case when a.type='SL' then 1 else 0 end,"
                            strSQL = strSQL + "     �༭Ȩ   = case when a.type='UP' then 1 else 0 end,"
                            strSQL = strSQL + "     ����Ȩ   = case when a.type='IN' then 1 else 0 end,"
                            strSQL = strSQL + "     ɾ��Ȩ   = case when a.type='DL' then 1 else 0 end,"
                            strSQL = strSQL + "     ִ��Ȩ   = case when a.type='EX' then 1 else 0 end "
                            strSQL = strSQL + "     from " + strCurDB + ".sys.database_permissions a " + vbCr
                            strSQL = strSQL + "     left join " + strCurDB + ".dbo.sysobjects b on a.major_id=b.id " + vbCr
                            strSQL = strSQL + "     left join " + strCurDB + ".dbo.sysusers   c on a.grantee_principal_id=c.uid" + vbCr
                            strSQL = strSQL + "     where c.issqlrole = 1 " + vbCr
                            strSQL = strSQL + "     and   c.gid > 0" + vbCr
                            strSQL = strSQL + "     and   c.name = @rolename" + vbCr
                            strSQL = strSQL + "     )a group by a.��������,a.��������"
                            strSQL = strSQL + "   ) b on a.��������=b.�������� and a.��������=b.��������" + vbCr
                            strSQL = strSQL + "   left join" + vbCr
                            strSQL = strSQL + "   (" + vbCr
                            strSQL = strSQL + "     select * from " + strDefDB + ".dbo.����_B_���ݿ�_����" + vbCr
                            strSQL = strSQL + "     where �������� = @server" + vbCr
                            strSQL = strSQL + "     and   ���ݿ��� = @dbname" + vbCr
                            strSQL = strSQL + "   ) c on a.��������=c.�������� and a.��������=c.��������" + vbCr
                            strSQL = strSQL + " ) a" + vbCr
                            If strWhere <> "" Then
                                strSQL = strSQL + " where " + strWhere + vbCr
                            End If
                            strSQL = strSQL + " order by a.��������,a.��������" + vbCr

                            '���ò���
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@False", Xydc.Platform.Common.Utilities.PulicParameters.CharFalse)
                            objSqlCommand.Parameters.AddWithValue("@True", Xydc.Platform.Common.Utilities.PulicParameters.CharTrue)
                            objSqlCommand.Parameters.AddWithValue("@rolename", strRoleName)
                            objSqlCommand.Parameters.AddWithValue("@server", strCurServer)
                            objSqlCommand.Parameters.AddWithValue("@dbname", strCurDB)
                            .SelectCommand = objSqlCommand
                        Else
                            '��ͬ������

                            '׼��SQL
                            strSQL = ""
                            strSQL = strSQL + " select a.*" + vbCr
                            strSQL = strSQL + " from" + vbCr
                            strSQL = strSQL + " (" + vbCr
                            strSQL = strSQL + "   select " + vbCr
                            strSQL = strSQL + "     a.��������,a.��������," + vbCr
                            strSQL = strSQL + "     ����������=a.��������," + vbCr
                            strSQL = strSQL + "     ѡ��Ȩ = case when b.ѡ��Ȩ=1 then @True else @False end,"
                            strSQL = strSQL + "     �༭Ȩ = case when b.�༭Ȩ=1 then @True else @False end,"
                            strSQL = strSQL + "     ����Ȩ = case when b.����Ȩ=1 then @True else @False end,"
                            strSQL = strSQL + "     ɾ��Ȩ = case when b.ɾ��Ȩ=1 then @True else @False end,"
                            strSQL = strSQL + "     ִ��Ȩ = case when b.ִ��Ȩ=1 then @True else @False end "
                            strSQL = strSQL + "   from " + vbCr
                            strSQL = strSQL + "   (  " + vbCr
                            strSQL = strSQL + "     select ��������=name,��������=xtype" + vbCr
                            strSQL = strSQL + "     from " + strCurDB + ".dbo.sysobjects " + vbCr
                            strSQL = strSQL + "     where xtype in (" + strXType + ")" + vbCr
                            strSQL = strSQL + "   ) a " + vbCr
                            strSQL = strSQL + "   left join " + vbCr
                            strSQL = strSQL + "   (" + vbCr
                            strSQL = strSQL + " select ��������,��������,ѡ��Ȩ=sum(ѡ��Ȩ),�༭Ȩ=sum(�༭Ȩ),����Ȩ=sum(����Ȩ),ɾ��Ȩ=sum(ɾ��Ȩ),ִ��Ȩ=sum(ִ��Ȩ) from"
                            strSQL = strSQL + "  ("
                            strSQL = strSQL + "     select "
                            strSQL = strSQL + "     �������� = b.name,"
                            strSQL = strSQL + "     �������� = b.xtype,"
                            strSQL = strSQL + "     ѡ��Ȩ   = case when a.type='SL' then 1 else 0 end,"
                            strSQL = strSQL + "     �༭Ȩ   = case when a.type='UP' then 1 else 0 end,"
                            strSQL = strSQL + "     ����Ȩ   = case when a.type='IN' then 1 else 0 end,"
                            strSQL = strSQL + "     ɾ��Ȩ   = case when a.type='DL' then 1 else 0 end,"
                            strSQL = strSQL + "     ִ��Ȩ   = case when a.type='EX' then 1 else 0 end "
                            strSQL = strSQL + "     from " + strCurDB + ".sys.database_permissions a " + vbCr
                            strSQL = strSQL + "     left join " + strCurDB + ".dbo.sysobjects b on a.major_id=b.id " + vbCr
                            strSQL = strSQL + "     left join " + strCurDB + ".dbo.sysusers   c on a.grantee_principal_id=c.uid" + vbCr
                            strSQL = strSQL + "     where c.issqlrole = 1 " + vbCr
                            strSQL = strSQL + "     and   c.gid > 0" + vbCr
                            strSQL = strSQL + "     and   c.name = @rolename" + vbCr
                            strSQL = strSQL + "     )a group by a.��������,a.��������"
                            strSQL = strSQL + "   ) b on a.��������=b.�������� and a.��������=b.��������" + vbCr
                            strSQL = strSQL + " ) a" + vbCr
                            If strWhere <> "" Then
                                strSQL = strSQL + " where " + strWhere + vbCr
                            End If
                            strSQL = strSQL + " order by a.��������,a.��������" + vbCr

                            '���ò���
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@False", Xydc.Platform.Common.Utilities.PulicParameters.CharFalse)
                            objSqlCommand.Parameters.AddWithValue("@True", Xydc.Platform.Common.Utilities.PulicParameters.CharTrue)
                            objSqlCommand.Parameters.AddWithValue("@rolename", strRoleName)
                            .SelectCommand = objSqlCommand
                        End If

                        'ִ�в���
                        .Fill(objTempRoleQXData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_DUIXIANGQX))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempRoleQXData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempRoleQXData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objRoleQXData = objTempRoleQXData
            getRolePermissionsData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempRoleQXData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ����ɫstrRoleName����ָ������strObjectName��Ȩ��objOptions
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objConnectionProperty�����Ӳ���
        '     strRoleName          ����ɫ��
        '     strObjectName        ��������
        '     strObjectType        ����������
        '     objOptions           ����ɫȨ������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doGrantRole( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strRoleName As String, _
            ByVal strObjectName As String, _
            ByVal strObjectType As String, _
            ByVal objOptions As System.Collections.Specialized.ListDictionary) As Boolean

            Dim objAppManagerData As New Xydc.Platform.Common.Data.AppManagerData
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '��ʼ��
            doGrantRole = False
            strErrMsg = ""

            Try
                '���
                If strRoleName Is Nothing Then strRoleName = ""
                If strObjectName Is Nothing Then strObjectName = ""
                If strObjectType Is Nothing Then strObjectType = ""
                strRoleName = strRoleName.Trim()
                strObjectName = strObjectName.Trim()
                strObjectType = strObjectType.Trim()
                If objConnectionProperty Is Nothing Then
                    strErrMsg = "����û��ָ��������������"
                    GoTo errProc
                End If
                If objOptions Is Nothing Then
                    strErrMsg = "����û��ָ��Ȩ�޲�����"
                    GoTo errProc
                End If

                '��ȡ����
                With objConnectionProperty

                    'If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, -1, .InitialCatalog, .DataSource) = False Then
                    '    GoTo errProc
                    'End If
                    If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, Platform.Common.jsoaConfiguration.ConnectionTestTimeout, .InitialCatalog, .DataSource) = False Then
                        GoTo errProc
                    End If

                End With

                '��ȡ����
                Dim strGrant As String = ""
                Dim strSQL As String = ""
                Try
                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '��Ȩ
                    Dim strU As String = objAppManagerData.getDatabaseObjectTypeString(Xydc.Platform.Common.Data.AppManagerData.enumDatabaseObjectType.U)
                    Dim strV As String = objAppManagerData.getDatabaseObjectTypeString(Xydc.Platform.Common.Data.AppManagerData.enumDatabaseObjectType.V)
                    Dim strP As String = objAppManagerData.getDatabaseObjectTypeString(Xydc.Platform.Common.Data.AppManagerData.enumDatabaseObjectType.P)
                    Dim strFN As String = objAppManagerData.getDatabaseObjectTypeString(Xydc.Platform.Common.Data.AppManagerData.enumDatabaseObjectType.FN)
                    Dim strIF As String = objAppManagerData.getDatabaseObjectTypeString(Xydc.Platform.Common.Data.AppManagerData.enumDatabaseObjectType.FIF)
                    Dim strTF As String = objAppManagerData.getDatabaseObjectTypeString(Xydc.Platform.Common.Data.AppManagerData.enumDatabaseObjectType.TF)
                    If strObjectType = strU Or strObjectType = strV Or strObjectType = strIF Or strObjectType = strTF Then
                        '����ͼ����Ƕ����
                        Dim objenumPermissionType As Xydc.Platform.Common.Data.AppManagerData.enumPermissionType
                        Dim objDictionaryEntry As System.Collections.DictionaryEntry
                        Dim strValue As String
                        Dim i As Integer
                        For Each objDictionaryEntry In objOptions
                            strValue = ""
                            Try
                                objenumPermissionType = CType(objDictionaryEntry.Key, Xydc.Platform.Common.Data.AppManagerData.enumPermissionType)
                            Catch ex As Exception
                                objenumPermissionType = Nothing
                            End Try
                            Select Case objenumPermissionType
                                Case Xydc.Platform.Common.Data.AppManagerData.enumPermissionType.GrantSelect
                                    strValue = objAppManagerData.getPermissionTypeString(objenumPermissionType)
                                Case Xydc.Platform.Common.Data.AppManagerData.enumPermissionType.GrantUpdate
                                    strValue = objAppManagerData.getPermissionTypeString(objenumPermissionType)
                                Case Xydc.Platform.Common.Data.AppManagerData.enumPermissionType.GrantInsert
                                    strValue = objAppManagerData.getPermissionTypeString(objenumPermissionType)
                                Case Xydc.Platform.Common.Data.AppManagerData.enumPermissionType.GrantDelete
                                    strValue = objAppManagerData.getPermissionTypeString(objenumPermissionType)
                                Case Else
                            End Select
                            If strValue <> "" Then
                                If strGrant = "" Then
                                    strGrant = strValue
                                Else
                                    strGrant = strGrant + "," + strValue
                                End If
                            End If
                        Next
                        If strGrant <> "" Then
                            strSQL = "grant " + strGrant + " on " + strObjectName + " to " + strRoleName
                        End If

                    ElseIf strObjectType = strP Or strObjectType = strFN Then
                        '�洢���̡�����
                        Dim objenumPermissionType As Xydc.Platform.Common.Data.AppManagerData.enumPermissionType
                        Dim objDictionaryEntry As System.Collections.DictionaryEntry
                        Dim strValue As String
                        Dim i As Integer
                        For Each objDictionaryEntry In objOptions
                            strValue = ""
                            Try
                                objenumPermissionType = CType(objDictionaryEntry.Key, Xydc.Platform.Common.Data.AppManagerData.enumPermissionType)
                            Catch ex As Exception
                                objenumPermissionType = Nothing
                            End Try
                            Select Case objenumPermissionType
                                Case Xydc.Platform.Common.Data.AppManagerData.enumPermissionType.GrantExecute
                                    strValue = objAppManagerData.getPermissionTypeString(objenumPermissionType)
                                Case Else
                            End Select
                            If strValue <> "" Then
                                If strGrant = "" Then
                                    strGrant = strValue
                                Else
                                    strGrant = strGrant + "," + strValue
                                End If
                            End If
                        Next
                        If strGrant <> "" Then
                            strSQL = "grant " + strGrant + " on " + strObjectName + " to " + strRoleName
                        End If

                    Else
                    End If

                    If strSQL <> "" Then
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
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

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objAppManagerData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            doGrantRole = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objAppManagerData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �ӽ�ɫstrRoleName����ָ������strObjectName��Ȩ��objOptions
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objConnectionProperty�����Ӳ���
        '     strRoleName          ����ɫ��
        '     strObjectName        ��������
        '     strObjectType        ����������
        '     objOptions           ����ɫȨ������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doRevokeRole( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strRoleName As String, _
            ByVal strObjectName As String, _
            ByVal strObjectType As String, _
            ByVal objOptions As System.Collections.Specialized.ListDictionary) As Boolean

            Dim objAppManagerData As New Xydc.Platform.Common.Data.AppManagerData
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '��ʼ��
            doRevokeRole = False
            strErrMsg = ""

            Try
                '���
                If strRoleName Is Nothing Then strRoleName = ""
                If strObjectName Is Nothing Then strObjectName = ""
                If strObjectType Is Nothing Then strObjectType = ""
                strRoleName = strRoleName.Trim()
                strObjectName = strObjectName.Trim()
                strObjectType = strObjectType.Trim()
                If objConnectionProperty Is Nothing Then
                    strErrMsg = "����û��ָ��������������"
                    GoTo errProc
                End If
                If objOptions Is Nothing Then
                    strErrMsg = "����û��ָ��Ȩ�޲�����"
                    GoTo errProc
                End If

                '��ȡ����
                With objConnectionProperty

                    'If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, -1, .InitialCatalog, .DataSource) = False Then
                    '    GoTo errProc
                    'End If
                    If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, Platform.Common.jsoaConfiguration.ConnectionTestTimeout, .InitialCatalog, .DataSource) = False Then
                        GoTo errProc
                    End If

                End With

                '��ȡ����
                Dim strGrant As String = ""
                Dim strSQL As String = ""
                Try
                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '��Ȩ
                    Dim strU As String = objAppManagerData.getDatabaseObjectTypeString(Xydc.Platform.Common.Data.AppManagerData.enumDatabaseObjectType.U)
                    Dim strV As String = objAppManagerData.getDatabaseObjectTypeString(Xydc.Platform.Common.Data.AppManagerData.enumDatabaseObjectType.V)
                    Dim strP As String = objAppManagerData.getDatabaseObjectTypeString(Xydc.Platform.Common.Data.AppManagerData.enumDatabaseObjectType.P)
                    Dim strFN As String = objAppManagerData.getDatabaseObjectTypeString(Xydc.Platform.Common.Data.AppManagerData.enumDatabaseObjectType.FN)
                    Dim strIF As String = objAppManagerData.getDatabaseObjectTypeString(Xydc.Platform.Common.Data.AppManagerData.enumDatabaseObjectType.FIF)
                    Dim strTF As String = objAppManagerData.getDatabaseObjectTypeString(Xydc.Platform.Common.Data.AppManagerData.enumDatabaseObjectType.TF)
                    If strObjectType = strU Or strObjectType = strV Or strObjectType = strIF Or strObjectType = strTF Then
                        '����ͼ����Ƕ����
                        Dim objenumPermissionType As Xydc.Platform.Common.Data.AppManagerData.enumPermissionType
                        Dim objDictionaryEntry As System.Collections.DictionaryEntry
                        Dim strValue As String
                        Dim i As Integer
                        For Each objDictionaryEntry In objOptions
                            strValue = ""
                            Try
                                objenumPermissionType = CType(objDictionaryEntry.Key, Xydc.Platform.Common.Data.AppManagerData.enumPermissionType)
                            Catch ex As Exception
                                objenumPermissionType = Nothing
                            End Try
                            Select Case objenumPermissionType
                                Case Xydc.Platform.Common.Data.AppManagerData.enumPermissionType.GrantSelect
                                    strValue = objAppManagerData.getPermissionTypeString(objenumPermissionType)
                                Case Xydc.Platform.Common.Data.AppManagerData.enumPermissionType.GrantUpdate
                                    strValue = objAppManagerData.getPermissionTypeString(objenumPermissionType)
                                Case Xydc.Platform.Common.Data.AppManagerData.enumPermissionType.GrantInsert
                                    strValue = objAppManagerData.getPermissionTypeString(objenumPermissionType)
                                Case Xydc.Platform.Common.Data.AppManagerData.enumPermissionType.GrantDelete
                                    strValue = objAppManagerData.getPermissionTypeString(objenumPermissionType)
                                Case Else
                            End Select
                            If strValue <> "" Then
                                If strGrant = "" Then
                                    strGrant = strValue
                                Else
                                    strGrant = strGrant + "," + strValue
                                End If
                            End If
                        Next
                        If strGrant <> "" Then
                            strSQL = "revoke " + strGrant + " on " + strObjectName + " from " + strRoleName
                        End If

                    ElseIf strObjectType = strP Or strObjectType = strFN Then
                        '�洢���̡�����
                        Dim objenumPermissionType As Xydc.Platform.Common.Data.AppManagerData.enumPermissionType
                        Dim objDictionaryEntry As System.Collections.DictionaryEntry
                        Dim strValue As String
                        Dim i As Integer
                        For Each objDictionaryEntry In objOptions
                            strValue = ""
                            Try
                                objenumPermissionType = CType(objDictionaryEntry.Key, Xydc.Platform.Common.Data.AppManagerData.enumPermissionType)
                            Catch ex As Exception
                                objenumPermissionType = Nothing
                            End Try
                            Select Case objenumPermissionType
                                Case Xydc.Platform.Common.Data.AppManagerData.enumPermissionType.GrantExecute
                                    strValue = objAppManagerData.getPermissionTypeString(objenumPermissionType)
                                Case Else
                            End Select
                            If strValue <> "" Then
                                If strGrant = "" Then
                                    strGrant = strValue
                                Else
                                    strGrant = strGrant + "," + strValue
                                End If
                            End If
                        Next
                        If strGrant <> "" Then
                            strSQL = "revoke " + strGrant + " on " + strObjectName + " from " + strRoleName
                        End If

                    Else
                    End If

                    If strSQL <> "" Then
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
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

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objAppManagerData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            doRevokeRole = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objAppManagerData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ��ָ�����ݿ��д�ȡȨ�޵���Ա��������ݼ�
        ' ����֯���롢��Ա�����������
        ' ����Ա��ȫ����������
        '     strErrMsg             ����������򷵻ش�����Ϣ
        '     objConnectionProperty �����Ӳ���
        '     strWhere              �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     objRenyuanGrantedData ��ָ����֯�����µ���Ա��Ϣ���ݼ�
        ' ����
        '     True                  ���ɹ�
        '     False                 ��ʧ��
        '----------------------------------------------------------------
        Public Function getRenyuanGrantedData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strWhere As String, _
            ByRef objRenyuanGrantedData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempRenyuanGrantedData As Xydc.Platform.Common.Data.CustomerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '��ʼ��
            getRenyuanGrantedData = False
            objRenyuanGrantedData = Nothing
            strErrMsg = ""

            Try
                '���
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim()
                If objConnectionProperty Is Nothing Then
                    objTempRenyuanGrantedData = New Xydc.Platform.Common.Data.CustomerData(Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_RENYUAN_FULLJOIN)
                    Exit Try
                End If

                '��ͬ������
                If objConnectionProperty.DataSource.ToUpper() <> Xydc.Platform.Common.jsoaConfiguration.DatabaseServerName.ToUpper() Then
                    objTempRenyuanGrantedData = New Xydc.Platform.Common.Data.CustomerData(Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_RENYUAN_FULLJOIN)
                    Exit Try
                End If

                '��ȡ����
                With objConnectionProperty

                    'If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, -1, .InitialCatalog, .DataSource) = False Then
                    '    GoTo errProc
                    'End If
                    If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, Platform.Common.jsoaConfiguration.ConnectionTestTimeout, .InitialCatalog, .DataSource) = False Then
                        GoTo errProc
                    End If

                End With

                '��ȡ����
                Dim strSQL As String
                Try
                    '�������ݼ�
                    objTempRenyuanGrantedData = New Xydc.Platform.Common.Data.CustomerData(Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_RENYUAN_FULLJOIN)

                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ�м���
                    With Me.m_objSqlDataAdapter
                        Dim strDefDB As String = Xydc.Platform.Common.jsoaConfiguration.DatabaseServerUserDB
                        Dim strCurDB As String = objConnectionProperty.InitialCatalog

                        '׼��SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* from ("
                        strSQL = strSQL + "   select a.*," + vbCr
                        strSQL = strSQL + "     b.��֯����,b.��֯����," + vbCr
                        strSQL = strSQL + "     ��λ�б� = " + strDefDB + ".dbo.GetGWMCByRydm(a.��Ա����,@separate)," + vbCr
                        strSQL = strSQL + "     c.��������,c.��������," + vbCr
                        strSQL = strSQL + "     �������� = d.��Ա����," + vbCr
                        strSQL = strSQL + "     ������ת������ = e.��Ա����," + vbCr
                        strSQL = strSQL + "     �Ƿ����� = @charfalse" + vbCr
                        strSQL = strSQL + "   from      " + strDefDB + ".dbo.����_B_��Ա     a " + vbCr
                        strSQL = strSQL + "   left join " + strDefDB + ".dbo.����_B_��֯���� b on a.��֯����   = b.��֯���� " + vbCr
                        strSQL = strSQL + "   left join " + strDefDB + ".dbo.����_B_�������� c on a.�������   = c.������� " + vbCr
                        strSQL = strSQL + "   left join " + strDefDB + ".dbo.����_B_��Ա     d on a.�������   = d.��Ա���� " + vbCr
                        strSQL = strSQL + "   left join " + strDefDB + ".dbo.����_B_��Ա     e on a.������ת�� = e.��Ա���� " + vbCr
                        strSQL = strSQL + "   left join " + strCurDB + ".dbo.sysusers        f on a.��Ա����   = f.name " + vbCr
                        strSQL = strSQL + "   left join           master.dbo.syslogins       g on a.��Ա����   = g.name " + vbCr
                        strSQL = strSQL + "   where ((f.name is not null and f.issqluser = 1) or (a.��Ա����='sa'))" + vbCr    'Login��User
                        strSQL = strSQL + "   and   g.name is not null " + vbCr                                                '������Login
                        strSQL = strSQL + " ) a "
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.��֯����, cast(a.��Ա��� as integer)"

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@separate", Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate)
                        objSqlCommand.Parameters.AddWithValue("@charfalse", Xydc.Platform.Common.Utilities.PulicParameters.CharFalse)
                        .SelectCommand = objSqlCommand

                        'ִ�в���
                        .Fill(objTempRenyuanGrantedData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempRenyuanGrantedData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempRenyuanGrantedData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objRenyuanGrantedData = objTempRenyuanGrantedData
            getRenyuanGrantedData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempRenyuanGrantedData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ��ָ�����ݿ�û�д�ȡȨ�޵���Ա��������ݼ�
        ' ����֯���롢��Ա�����������
        ' ����Ա��ȫ����������
        '     strErrMsg               ����������򷵻ش�����Ϣ
        '     objConnectionProperty   �����Ӳ���
        '     strWhere                �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     objRenyuanUngrantedData ��ָ����֯�����µ���Ա��Ϣ���ݼ�
        ' ����
        '     True                    ���ɹ�
        '     False                   ��ʧ��
        '----------------------------------------------------------------
        Public Function getRenyuanUngrantedData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strWhere As String, _
            ByRef objRenyuanUngrantedData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempRenyuanUngrantedData As Xydc.Platform.Common.Data.CustomerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '��ʼ��
            getRenyuanUngrantedData = False
            objRenyuanUngrantedData = Nothing
            strErrMsg = ""

            Try
                '���
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim()
                If objConnectionProperty Is Nothing Then
                    objTempRenyuanUngrantedData = New Xydc.Platform.Common.Data.CustomerData(Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_RENYUAN_FULLJOIN)
                    Exit Try
                End If

                '��ͬ������
                If objConnectionProperty.DataSource.ToUpper() <> Xydc.Platform.Common.jsoaConfiguration.DatabaseServerName.ToUpper() Then
                    objTempRenyuanUngrantedData = New Xydc.Platform.Common.Data.CustomerData(Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_RENYUAN_FULLJOIN)
                    Exit Try
                End If

                '��ȡ����
                With objConnectionProperty

                    'If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, -1, .InitialCatalog, .DataSource) = False Then
                    '    GoTo errProc
                    'End If
                    If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, Platform.Common.jsoaConfiguration.ConnectionTestTimeout, .InitialCatalog, .DataSource) = False Then
                        GoTo errProc
                    End If

                End With

                '��ȡ����
                Dim strSQL As String
                Try
                    '�������ݼ�
                    objTempRenyuanUngrantedData = New Xydc.Platform.Common.Data.CustomerData(Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_RENYUAN_FULLJOIN)

                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ�м���
                    With Me.m_objSqlDataAdapter
                        Dim strDefDB As String = Xydc.Platform.Common.jsoaConfiguration.DatabaseServerUserDB
                        Dim strCurDB As String = objConnectionProperty.InitialCatalog

                        '׼��SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* from ("
                        strSQL = strSQL + "   select a.*," + vbCr
                        strSQL = strSQL + "     b.��֯����,b.��֯����," + vbCr
                        strSQL = strSQL + "     ��λ�б� = " + strDefDB + ".dbo.GetGWMCByRydm(a.��Ա����,@separate)," + vbCr
                        strSQL = strSQL + "     c.��������,c.��������," + vbCr
                        strSQL = strSQL + "     �������� = d.��Ա����," + vbCr
                        strSQL = strSQL + "     ������ת������ = e.��Ա����," + vbCr
                        strSQL = strSQL + "     �Ƿ����� = @charfalse" + vbCr
                        strSQL = strSQL + "   from      " + strDefDB + ".dbo.����_B_��Ա     a " + vbCr
                        strSQL = strSQL + "   left join " + strDefDB + ".dbo.����_B_��֯���� b on a.��֯����   = b.��֯���� " + vbCr
                        strSQL = strSQL + "   left join " + strDefDB + ".dbo.����_B_�������� c on a.�������   = c.������� " + vbCr
                        strSQL = strSQL + "   left join " + strDefDB + ".dbo.����_B_��Ա     d on a.�������   = d.��Ա���� " + vbCr
                        strSQL = strSQL + "   left join " + strDefDB + ".dbo.����_B_��Ա     e on a.������ת�� = e.��Ա���� " + vbCr
                        strSQL = strSQL + "   left join " + strCurDB + ".dbo.sysusers        f on a.��Ա����   = f.name " + vbCr
                        strSQL = strSQL + "   left join           master.dbo.syslogins       g on a.��Ա����   = g.name " + vbCr
                        strSQL = strSQL + "   where not ((f.name is not null and f.issqluser = 1) or (a.��Ա����='sa'))" + vbCr    'Loginû��User
                        strSQL = strSQL + "   and   g.name is not null " + vbCr                                                    '������Login
                        strSQL = strSQL + " ) a "
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.��֯����, cast(a.��Ա��� as integer)"

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@separate", Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate)
                        objSqlCommand.Parameters.AddWithValue("@charfalse", Xydc.Platform.Common.Utilities.PulicParameters.CharFalse)
                        .SelectCommand = objSqlCommand

                        'ִ�в���
                        .Fill(objTempRenyuanUngrantedData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempRenyuanUngrantedData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempRenyuanUngrantedData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objRenyuanUngrantedData = objTempRenyuanUngrantedData
            getRenyuanUngrantedData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempRenyuanUngrantedData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��strLoginName�����ȡ���ݿ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objConnectionProperty�����Ӳ���
        '     strLoginName         ����ɫ��
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doGrantDatabase( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strLoginName As String) As Boolean

            Dim objAppManagerData As New Xydc.Platform.Common.Data.AppManagerData
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '��ʼ��
            doGrantDatabase = False
            strErrMsg = ""

            Try
                '���
                If strLoginName Is Nothing Then strLoginName = ""
                strLoginName = strLoginName.Trim()
                If objConnectionProperty Is Nothing Then
                    strErrMsg = "����û��ָ��������������"
                    GoTo errProc
                End If
                If strLoginName.ToUpper() = "SA" Then
                    Exit Try
                End If

                '��ȡ����
                With objConnectionProperty

                    'If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, -1, .InitialCatalog, .DataSource) = False Then
                    '    GoTo errProc
                    'End If
                    If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, Platform.Common.jsoaConfiguration.ConnectionTestTimeout, .InitialCatalog, .DataSource) = False Then
                        GoTo errProc
                    End If

                End With

                '��ȡ����
                Dim strGrant As String = ""
                Dim strSQL As String = ""
                Try
                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '��Ȩ
                    strSQL = "exec sp_grantdbaccess @loginname, @username"
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@loginname", strLoginName)
                    objSqlCommand.Parameters.AddWithValue("@username", strLoginName)
                    objSqlCommand.ExecuteNonQuery()
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
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objAppManagerData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            doGrantDatabase = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objAppManagerData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��strLoginNameȡ����ȡ���ݿ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objConnectionProperty�����Ӳ���
        '     strLoginName         ����ɫ��
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doRevokeDatabase( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strLoginName As String) As Boolean

            Dim objAppManagerData As New Xydc.Platform.Common.Data.AppManagerData
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '��ʼ��
            doRevokeDatabase = False
            strErrMsg = ""

            Try
                '���
                If strLoginName Is Nothing Then strLoginName = ""
                strLoginName = strLoginName.Trim()
                If objConnectionProperty Is Nothing Then
                    strErrMsg = "����û��ָ��������������"
                    GoTo errProc
                End If
                If strLoginName.ToUpper() = "SA" Then
                    Exit Try
                End If

                '��ȡ����
                With objConnectionProperty

                    'If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, -1, .InitialCatalog, .DataSource) = False Then
                    '    GoTo errProc
                    'End If
                    If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, Platform.Common.jsoaConfiguration.ConnectionTestTimeout, .InitialCatalog, .DataSource) = False Then
                        GoTo errProc
                    End If

                End With

                '��ȡ����
                Dim strGrant As String = ""
                Dim strSQL As String = ""
                Try
                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '��Ȩ
                    Dim strDBName As String = objConnectionProperty.InitialCatalog
                    strSQL = "exec sp_revokedbaccess @loginname"
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@loginname", strLoginName)
                    objSqlCommand.ExecuteNonQuery()
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
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objAppManagerData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            doRevokeDatabase = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objAppManagerData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡָ��objConnectionProperty�е����ݿ���û�
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     objConnectionProperty       ����������Ϣ
        '     strWhere                    �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     objDBUserData               ����Ϣ���ݼ�
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function getDBUserData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strWhere As String, _
            ByRef objDBUserData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempDBUserData As Xydc.Platform.Common.Data.AppManagerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '��ʼ��
            getDBUserData = False
            objDBUserData = Nothing
            strErrMsg = ""

            Try
                '���
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim()
                If objConnectionProperty Is Nothing Then
                    '�������ݼ�
                    objTempDBUserData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_SHUJUKU_DBUSER)
                    Exit Try
                End If

                '��ȡ����
                With objConnectionProperty

                    'If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, -1, .InitialCatalog, .DataSource) = False Then
                    '    GoTo errProc
                    'End If
                    If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, Platform.Common.jsoaConfiguration.ConnectionTestTimeout, .InitialCatalog, .DataSource) = False Then
                        GoTo errProc
                    End If

                End With

                '��ȡ����
                Dim strSQL As String
                Try
                    '�������ݼ�
                    objTempDBUserData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_SHUJUKU_DBUSER)

                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ�м���
                    With Me.m_objSqlDataAdapter
                        '׼��SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.uid,a.name " + vbCr
                        strSQL = strSQL + " from " + objConnectionProperty.InitialCatalog + ".dbo.sysusers a" + vbCr
                        strSQL = strSQL + " where issqluser = 1" + vbCr             '�û�
                        strSQL = strSQL + " and   name <> 'guest'" + vbCr           '��guest
                        If strWhere <> "" Then
                            strSQL = strSQL + " and " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.name"

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        'ִ�в���
                        .Fill(objTempDBUserData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_DBUSER))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDBUserData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempDBUserData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objDBUserData = objTempDBUserData
            getDBUserData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempDBUserData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ��ɫ��Ȩ����������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objConnectionProperty�����Ӳ���
        '     strDBUserName        ���û���
        '     strWhere             �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     objDBUserQXData      ����ɫȨ������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getDBUserPermissionsData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strDBUserName As String, _
            ByVal strWhere As String, _
            ByRef objDBUserQXData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempDBUserQXData As Xydc.Platform.Common.Data.AppManagerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '��ʼ��
            getDBUserPermissionsData = False
            objDBUserQXData = Nothing
            strErrMsg = ""

            Try
                '���
                If strDBUserName Is Nothing Then strDBUserName = ""
                If strWhere Is Nothing Then strWhere = ""
                strDBUserName = strDBUserName.Trim()
                strWhere = strWhere.Trim()
                If objConnectionProperty Is Nothing Then
                    objTempDBUserQXData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_SHUJUKU_DUIXIANGQX)
                    Exit Try
                End If

                '��ȡ����
                With objConnectionProperty

                    'If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, -1, .InitialCatalog, .DataSource) = False Then
                    '    GoTo errProc
                    'End If
                    If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, Platform.Common.jsoaConfiguration.ConnectionTestTimeout, .InitialCatalog, .DataSource) = False Then
                        GoTo errProc
                    End If

                End With

                '��ȡ����
                Dim strSQL As String
                Try
                    '�������ݼ�
                    objTempDBUserQXData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_SHUJUKU_DUIXIANGQX)

                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ�м���
                    With Me.m_objSqlDataAdapter
                        Dim strDefServer As String = Xydc.Platform.Common.jsoaConfiguration.DatabaseServerName
                        Dim strDefDB As String = Xydc.Platform.Common.jsoaConfiguration.DatabaseServerUserDB
                        Dim strCurServer As String = objConnectionProperty.DataSource
                        Dim strCurDB As String = objConnectionProperty.InitialCatalog
                        Dim strXType As String = Xydc.Platform.Common.Data.AppManagerData.OBJECTTYPELIST

                        If strCurServer.ToUpper() = strDefServer.ToUpper() Then
                            'ͬһ������

                            '׼��SQL
                            strSQL = ""
                            strSQL = strSQL + " select a.*" + vbCr
                            strSQL = strSQL + " from" + vbCr
                            strSQL = strSQL + " (" + vbCr
                            strSQL = strSQL + "   select " + vbCr
                            strSQL = strSQL + "     a.��������,a.��������," + vbCr
                            strSQL = strSQL + "     ���������� = case when c.�������� is null then a.�������� else c.���������� end," + vbCr
                            strSQL = strSQL + "     ѡ��Ȩ     = case when b.�������� is null then @False else b.ѡ��Ȩ end," + vbCr
                            strSQL = strSQL + "     �༭Ȩ     = case when b.�������� is null then @False else b.�༭Ȩ end," + vbCr
                            strSQL = strSQL + "     ����Ȩ     = case when b.�������� is null then @False else b.����Ȩ end," + vbCr
                            strSQL = strSQL + "     ɾ��Ȩ     = case when b.�������� is null then @False else b.ɾ��Ȩ end," + vbCr
                            strSQL = strSQL + "     ִ��Ȩ     = case when b.�������� is null then @False else b.ִ��Ȩ end " + vbCr
                            strSQL = strSQL + "   from " + vbCr
                            strSQL = strSQL + "   (  " + vbCr
                            strSQL = strSQL + "     select ��������=name,��������=xtype" + vbCr
                            strSQL = strSQL + "     from " + strCurDB + ".dbo.sysobjects " + vbCr
                            strSQL = strSQL + "     where xtype in (" + strXType + ")" + vbCr
                            strSQL = strSQL + "     and status > 0" + vbCr
                            strSQL = strSQL + "   ) a " + vbCr
                            strSQL = strSQL + "   left join " + vbCr
                            strSQL = strSQL + "   (" + vbCr
                            strSQL = strSQL + "     select " + vbCr
                            strSQL = strSQL + "       �������� = b.name," + vbCr
                            strSQL = strSQL + "       �������� = b.xtype," + vbCr
                            strSQL = strSQL + "       ѡ��Ȩ   = case when a.actadd&1  > 0 then @True else @False end," + vbCr
                            strSQL = strSQL + "       �༭Ȩ   = case when a.actadd&2  > 0 then @True else @False end," + vbCr
                            strSQL = strSQL + "       ����Ȩ   = case when a.actadd&8  > 0 then @True else @False end," + vbCr
                            strSQL = strSQL + "       ɾ��Ȩ   = case when a.actadd&16 > 0 then @True else @False end," + vbCr
                            strSQL = strSQL + "       ִ��Ȩ   = case when a.actadd&32 > 0 then @True else @False end " + vbCr
                            strSQL = strSQL + "     from " + strCurDB + ".dbo.syspermissions a " + vbCr
                            strSQL = strSQL + "     left join " + strCurDB + ".dbo.sysobjects b on a.id      = b.id" + vbCr
                            strSQL = strSQL + "     left join " + strCurDB + ".dbo.sysusers   c on a.grantee = c.uid" + vbCr
                            strSQL = strSQL + "     where c.issqluser = 1 " + vbCr
                            strSQL = strSQL + "     and   c.name <> 'guest'" + vbCr
                            strSQL = strSQL + "     and   c.name = @username" + vbCr
                            strSQL = strSQL + "   ) b on a.��������=b.�������� and a.��������=b.��������" + vbCr
                            strSQL = strSQL + "   left join" + vbCr
                            strSQL = strSQL + "   (" + vbCr
                            strSQL = strSQL + "     select * from " + strDefDB + ".dbo.����_B_���ݿ�_����" + vbCr
                            strSQL = strSQL + "     where �������� = @server" + vbCr
                            strSQL = strSQL + "     and   ���ݿ��� = @dbname" + vbCr
                            strSQL = strSQL + "   ) c on a.��������=c.�������� and a.��������=c.��������" + vbCr
                            strSQL = strSQL + " ) a" + vbCr
                            If strWhere <> "" Then
                                strSQL = strSQL + " where " + strWhere + vbCr
                            End If
                            strSQL = strSQL + " order by a.��������,a.��������" + vbCr

                            '���ò���
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@False", Xydc.Platform.Common.Utilities.PulicParameters.CharFalse)
                            objSqlCommand.Parameters.AddWithValue("@True", Xydc.Platform.Common.Utilities.PulicParameters.CharTrue)
                            objSqlCommand.Parameters.AddWithValue("@username", strDBUserName)
                            objSqlCommand.Parameters.AddWithValue("@server", strCurServer)
                            objSqlCommand.Parameters.AddWithValue("@dbname", strCurDB)
                            .SelectCommand = objSqlCommand
                        Else
                            '��ͬ������

                            '׼��SQL
                            strSQL = ""
                            strSQL = strSQL + " select a.*" + vbCr
                            strSQL = strSQL + " from" + vbCr
                            strSQL = strSQL + " (" + vbCr
                            strSQL = strSQL + "   select " + vbCr
                            strSQL = strSQL + "     a.��������,a.��������," + vbCr
                            strSQL = strSQL + "     ����������=a.��������," + vbCr
                            strSQL = strSQL + "     ѡ��Ȩ = case when b.�������� is null then @False else b.ѡ��Ȩ end," + vbCr
                            strSQL = strSQL + "     �༭Ȩ = case when b.�������� is null then @False else b.�༭Ȩ end," + vbCr
                            strSQL = strSQL + "     ����Ȩ = case when b.�������� is null then @False else b.����Ȩ end," + vbCr
                            strSQL = strSQL + "     ɾ��Ȩ = case when b.�������� is null then @False else b.ɾ��Ȩ end," + vbCr
                            strSQL = strSQL + "     ִ��Ȩ = case when b.�������� is null then @False else b.ִ��Ȩ end " + vbCr
                            strSQL = strSQL + "   from " + vbCr
                            strSQL = strSQL + "   (  " + vbCr
                            strSQL = strSQL + "     select ��������=name,��������=xtype" + vbCr
                            strSQL = strSQL + "     from " + strCurDB + ".dbo.sysobjects " + vbCr
                            strSQL = strSQL + "     where xtype in (" + strXType + ")" + vbCr
                            strSQL = strSQL + "     and status > 0" + vbCr
                            strSQL = strSQL + "   ) a " + vbCr
                            strSQL = strSQL + "   left join " + vbCr
                            strSQL = strSQL + "   (" + vbCr
                            strSQL = strSQL + "     select " + vbCr
                            strSQL = strSQL + "       �������� = b.name," + vbCr
                            strSQL = strSQL + "       �������� = b.xtype," + vbCr
                            strSQL = strSQL + "       ѡ��Ȩ   = case when a.actadd&1  > 0 then @True else @False end," + vbCr
                            strSQL = strSQL + "       �༭Ȩ   = case when a.actadd&2  > 0 then @True else @False end," + vbCr
                            strSQL = strSQL + "       ����Ȩ   = case when a.actadd&8  > 0 then @True else @False end," + vbCr
                            strSQL = strSQL + "       ɾ��Ȩ   = case when a.actadd&16 > 0 then @True else @False end," + vbCr
                            strSQL = strSQL + "       ִ��Ȩ   = case when a.actadd&32 > 0 then @True else @False end " + vbCr
                            strSQL = strSQL + "     from " + strCurDB + ".dbo.syspermissions a " + vbCr
                            strSQL = strSQL + "     left join " + strCurDB + ".dbo.sysobjects b on a.id      = b.id" + vbCr
                            strSQL = strSQL + "     left join " + strCurDB + ".dbo.sysusers   c on a.grantee = c.uid" + vbCr
                            strSQL = strSQL + "     where c.issqluser = 1 " + vbCr
                            strSQL = strSQL + "     and   c.name <> 'guest'" + vbCr
                            strSQL = strSQL + "     and   c.name = @username" + vbCr
                            strSQL = strSQL + "   ) b on a.��������=b.�������� and a.��������=b.��������" + vbCr
                            strSQL = strSQL + " ) a" + vbCr
                            If strWhere <> "" Then
                                strSQL = strSQL + " where " + strWhere + vbCr
                            End If
                            strSQL = strSQL + " order by a.��������,a.��������" + vbCr

                            '���ò���
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@False", Xydc.Platform.Common.Utilities.PulicParameters.CharFalse)
                            objSqlCommand.Parameters.AddWithValue("@True", Xydc.Platform.Common.Utilities.PulicParameters.CharTrue)
                            objSqlCommand.Parameters.AddWithValue("@username", strDBUserName)
                            .SelectCommand = objSqlCommand
                        End If

                        'ִ�в���
                        .Fill(objTempDBUserQXData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_SHUJUKU_DUIXIANGQX))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDBUserQXData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempDBUserQXData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objDBUserQXData = objTempDBUserQXData
            getDBUserPermissionsData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempDBUserQXData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���û�strDBUserName����ָ������strObjectName��Ȩ��objOptions
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objConnectionProperty�����Ӳ���
        '     strDBUserName        ���û���
        '     strObjectName        ��������
        '     strObjectType        ����������
        '     objOptions           ����ɫȨ������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doGrantDBUser( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strDBUserName As String, _
            ByVal strObjectName As String, _
            ByVal strObjectType As String, _
            ByVal objOptions As System.Collections.Specialized.ListDictionary) As Boolean

            Dim objAppManagerData As New Xydc.Platform.Common.Data.AppManagerData
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '��ʼ��
            doGrantDBUser = False
            strErrMsg = ""

            Try
                '���
                If strDBUserName Is Nothing Then strDBUserName = ""
                If strObjectName Is Nothing Then strObjectName = ""
                If strObjectType Is Nothing Then strObjectType = ""
                strDBUserName = strDBUserName.Trim()
                strObjectName = strObjectName.Trim()
                strObjectType = strObjectType.Trim()
                If objConnectionProperty Is Nothing Then
                    strErrMsg = "����û��ָ��������������"
                    GoTo errProc
                End If
                If objOptions Is Nothing Then
                    strErrMsg = "����û��ָ��Ȩ�޲�����"
                    GoTo errProc
                End If

                '��ȡ����
                With objConnectionProperty

                    'If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, -1, .InitialCatalog, .DataSource) = False Then
                    '    GoTo errProc
                    'End If
                    If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, Platform.Common.jsoaConfiguration.ConnectionTestTimeout, .InitialCatalog, .DataSource) = False Then
                        GoTo errProc
                    End If

                End With

                '��ȡ����
                Dim strGrant As String = ""
                Dim strSQL As String = ""
                Try
                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '��Ȩ
                    Dim strU As String = objAppManagerData.getDatabaseObjectTypeString(Xydc.Platform.Common.Data.AppManagerData.enumDatabaseObjectType.U)
                    Dim strV As String = objAppManagerData.getDatabaseObjectTypeString(Xydc.Platform.Common.Data.AppManagerData.enumDatabaseObjectType.V)
                    Dim strP As String = objAppManagerData.getDatabaseObjectTypeString(Xydc.Platform.Common.Data.AppManagerData.enumDatabaseObjectType.P)
                    Dim strFN As String = objAppManagerData.getDatabaseObjectTypeString(Xydc.Platform.Common.Data.AppManagerData.enumDatabaseObjectType.FN)
                    Dim strIF As String = objAppManagerData.getDatabaseObjectTypeString(Xydc.Platform.Common.Data.AppManagerData.enumDatabaseObjectType.FIF)
                    Dim strTF As String = objAppManagerData.getDatabaseObjectTypeString(Xydc.Platform.Common.Data.AppManagerData.enumDatabaseObjectType.TF)
                    If strObjectType = strU Or strObjectType = strV Or strObjectType = strIF Or strObjectType = strTF Then
                        '����ͼ����Ƕ����
                        Dim objenumPermissionType As Xydc.Platform.Common.Data.AppManagerData.enumPermissionType
                        Dim objDictionaryEntry As System.Collections.DictionaryEntry
                        Dim strValue As String
                        Dim i As Integer
                        For Each objDictionaryEntry In objOptions
                            strValue = ""
                            Try
                                objenumPermissionType = CType(objDictionaryEntry.Key, Xydc.Platform.Common.Data.AppManagerData.enumPermissionType)
                            Catch ex As Exception
                                objenumPermissionType = Nothing
                            End Try
                            Select Case objenumPermissionType
                                Case Xydc.Platform.Common.Data.AppManagerData.enumPermissionType.GrantSelect
                                    strValue = objAppManagerData.getPermissionTypeString(objenumPermissionType)
                                Case Xydc.Platform.Common.Data.AppManagerData.enumPermissionType.GrantUpdate
                                    strValue = objAppManagerData.getPermissionTypeString(objenumPermissionType)
                                Case Xydc.Platform.Common.Data.AppManagerData.enumPermissionType.GrantInsert
                                    strValue = objAppManagerData.getPermissionTypeString(objenumPermissionType)
                                Case Xydc.Platform.Common.Data.AppManagerData.enumPermissionType.GrantDelete
                                    strValue = objAppManagerData.getPermissionTypeString(objenumPermissionType)
                                Case Else
                            End Select
                            If strValue <> "" Then
                                If strGrant = "" Then
                                    strGrant = strValue
                                Else
                                    strGrant = strGrant + "," + strValue
                                End If
                            End If
                        Next
                        If strGrant <> "" Then
                            strSQL = "grant " + strGrant + " on " + strObjectName + " to " + strDBUserName
                        End If

                    ElseIf strObjectType = strP Or strObjectType = strFN Then
                        '�洢���̡�����
                        Dim objenumPermissionType As Xydc.Platform.Common.Data.AppManagerData.enumPermissionType
                        Dim objDictionaryEntry As System.Collections.DictionaryEntry
                        Dim strValue As String
                        Dim i As Integer
                        For Each objDictionaryEntry In objOptions
                            strValue = ""
                            Try
                                objenumPermissionType = CType(objDictionaryEntry.Key, Xydc.Platform.Common.Data.AppManagerData.enumPermissionType)
                            Catch ex As Exception
                                objenumPermissionType = Nothing
                            End Try
                            Select Case objenumPermissionType
                                Case Xydc.Platform.Common.Data.AppManagerData.enumPermissionType.GrantExecute
                                    strValue = objAppManagerData.getPermissionTypeString(objenumPermissionType)
                                Case Else
                            End Select
                            If strValue <> "" Then
                                If strGrant = "" Then
                                    strGrant = strValue
                                Else
                                    strGrant = strGrant + "," + strValue
                                End If
                            End If
                        Next
                        If strGrant <> "" Then
                            strSQL = "grant " + strGrant + " on " + strObjectName + " to " + strDBUserName
                        End If

                    Else
                    End If

                    If strSQL <> "" Then
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
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

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objAppManagerData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            doGrantDBUser = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objAppManagerData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���û�strDBUserName����ָ������strObjectName��Ȩ��objOptions
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objConnectionProperty�����Ӳ���
        '     strDBUserName        ���û���
        '     strObjectName        ��������
        '     strObjectType        ����������
        '     objOptions           ����ɫȨ������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doRevokeDBUser( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strDBUserName As String, _
            ByVal strObjectName As String, _
            ByVal strObjectType As String, _
            ByVal objOptions As System.Collections.Specialized.ListDictionary) As Boolean

            Dim objAppManagerData As New Xydc.Platform.Common.Data.AppManagerData
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '��ʼ��
            doRevokeDBUser = False
            strErrMsg = ""

            Try
                '���
                If strDBUserName Is Nothing Then strDBUserName = ""
                If strObjectName Is Nothing Then strObjectName = ""
                If strObjectType Is Nothing Then strObjectType = ""
                strDBUserName = strDBUserName.Trim()
                strObjectName = strObjectName.Trim()
                strObjectType = strObjectType.Trim()
                If objConnectionProperty Is Nothing Then
                    strErrMsg = "����û��ָ��������������"
                    GoTo errProc
                End If
                If objOptions Is Nothing Then
                    strErrMsg = "����û��ָ��Ȩ�޲�����"
                    GoTo errProc
                End If

                '��ȡ����
                With objConnectionProperty

                    'If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, -1, .InitialCatalog, .DataSource) = False Then
                    '    GoTo errProc
                    'End If
                    If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, Platform.Common.jsoaConfiguration.ConnectionTestTimeout, .InitialCatalog, .DataSource) = False Then
                        GoTo errProc
                    End If

                End With

                '��ȡ����
                Dim strGrant As String = ""
                Dim strSQL As String = ""
                Try
                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '��Ȩ
                    Dim strU As String = objAppManagerData.getDatabaseObjectTypeString(Xydc.Platform.Common.Data.AppManagerData.enumDatabaseObjectType.U)
                    Dim strV As String = objAppManagerData.getDatabaseObjectTypeString(Xydc.Platform.Common.Data.AppManagerData.enumDatabaseObjectType.V)
                    Dim strP As String = objAppManagerData.getDatabaseObjectTypeString(Xydc.Platform.Common.Data.AppManagerData.enumDatabaseObjectType.P)
                    Dim strFN As String = objAppManagerData.getDatabaseObjectTypeString(Xydc.Platform.Common.Data.AppManagerData.enumDatabaseObjectType.FN)
                    Dim strIF As String = objAppManagerData.getDatabaseObjectTypeString(Xydc.Platform.Common.Data.AppManagerData.enumDatabaseObjectType.FIF)
                    Dim strTF As String = objAppManagerData.getDatabaseObjectTypeString(Xydc.Platform.Common.Data.AppManagerData.enumDatabaseObjectType.TF)
                    If strObjectType = strU Or strObjectType = strV Or strObjectType = strIF Or strObjectType = strTF Then
                        '����ͼ����Ƕ����
                        Dim objenumPermissionType As Xydc.Platform.Common.Data.AppManagerData.enumPermissionType
                        Dim objDictionaryEntry As System.Collections.DictionaryEntry
                        Dim strValue As String
                        Dim i As Integer
                        For Each objDictionaryEntry In objOptions
                            strValue = ""
                            Try
                                objenumPermissionType = CType(objDictionaryEntry.Key, Xydc.Platform.Common.Data.AppManagerData.enumPermissionType)
                            Catch ex As Exception
                                objenumPermissionType = Nothing
                            End Try
                            Select Case objenumPermissionType
                                Case Xydc.Platform.Common.Data.AppManagerData.enumPermissionType.GrantSelect
                                    strValue = objAppManagerData.getPermissionTypeString(objenumPermissionType)
                                Case Xydc.Platform.Common.Data.AppManagerData.enumPermissionType.GrantUpdate
                                    strValue = objAppManagerData.getPermissionTypeString(objenumPermissionType)
                                Case Xydc.Platform.Common.Data.AppManagerData.enumPermissionType.GrantInsert
                                    strValue = objAppManagerData.getPermissionTypeString(objenumPermissionType)
                                Case Xydc.Platform.Common.Data.AppManagerData.enumPermissionType.GrantDelete
                                    strValue = objAppManagerData.getPermissionTypeString(objenumPermissionType)
                                Case Else
                            End Select
                            If strValue <> "" Then
                                If strGrant = "" Then
                                    strGrant = strValue
                                Else
                                    strGrant = strGrant + "," + strValue
                                End If
                            End If
                        Next
                        If strGrant <> "" Then
                            strSQL = "revoke " + strGrant + " on " + strObjectName + " from " + strDBUserName
                        End If

                    ElseIf strObjectType = strP Or strObjectType = strFN Then
                        '�洢���̡�����
                        Dim objenumPermissionType As Xydc.Platform.Common.Data.AppManagerData.enumPermissionType
                        Dim objDictionaryEntry As System.Collections.DictionaryEntry
                        Dim strValue As String
                        Dim i As Integer
                        For Each objDictionaryEntry In objOptions
                            strValue = ""
                            Try
                                objenumPermissionType = CType(objDictionaryEntry.Key, Xydc.Platform.Common.Data.AppManagerData.enumPermissionType)
                            Catch ex As Exception
                                objenumPermissionType = Nothing
                            End Try
                            Select Case objenumPermissionType
                                Case Xydc.Platform.Common.Data.AppManagerData.enumPermissionType.GrantExecute
                                    strValue = objAppManagerData.getPermissionTypeString(objenumPermissionType)
                                Case Else
                            End Select
                            If strValue <> "" Then
                                If strGrant = "" Then
                                    strGrant = strValue
                                Else
                                    strGrant = strGrant + "," + strValue
                                End If
                            End If
                        Next
                        If strGrant <> "" Then
                            strSQL = "revoke " + strGrant + " on " + strObjectName + " from " + strDBUserName
                        End If

                    Else
                    End If

                    If strSQL <> "" Then
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
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

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objAppManagerData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            doRevokeDBUser = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objAppManagerData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ������_B_Ӧ��ϵͳ_ģ�顱�����ݼ�(��ģ�������������)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strWhere             �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     objMokuaiData        ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getMokuaiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objMokuaiData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempMokuaiData As Xydc.Platform.Common.Data.AppManagerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '��ʼ��
            getMokuaiData = False
            objMokuaiData = Nothing
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strWhere Is Nothing Then strWhere = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                strWhere = strWhere.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ����
                Dim strSQL As String
                Try
                    '�������ݼ�
                    objTempMokuaiData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_YINGYONGXITONG_MOKUAI)

                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ�м���
                    With Me.m_objSqlDataAdapter
                        '׼��SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from ����_B_Ӧ��ϵͳ_ģ�� a " + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.ģ����� " + vbCr

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        'ִ�в���
                        .Fill(objTempMokuaiData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_YINGYONGXITONG_MOKUAI))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempMokuaiData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempMokuaiData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objMokuaiData = objTempMokuaiData
            getMokuaiData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempMokuaiData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡָ��strMKDM�¼��ġ�����_B_Ӧ��ϵͳ_ģ�顱�����ݼ�(��ģ�������������)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strMKDM              ��ģ�����
        '     strWhere             �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     objMokuaiData        ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getMokuaiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strMKDM As String, _
            ByVal strWhere As String, _
            ByRef objMokuaiData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempMokuaiData As Xydc.Platform.Common.Data.AppManagerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '��ʼ��
            getMokuaiData = False
            objMokuaiData = Nothing
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strMKDM Is Nothing Then strMKDM = ""
                If strWhere Is Nothing Then strWhere = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                strMKDM = strMKDM.Trim()
                strWhere = strWhere.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ����
                Dim strSQL As String
                Try
                    '�������ݼ�
                    objTempMokuaiData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_YINGYONGXITONG_MOKUAI)

                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ�м���
                    Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharFjdmSeparate
                    With Me.m_objSqlDataAdapter
                        '׼��SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from ����_B_Ӧ��ϵͳ_ģ�� a " + vbCr
                        strSQL = strSQL + " where (a.ģ����� like @mkdm + '" + strSep + "%' or a.ģ����� = @mkdm)" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " and " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.ģ����� " + vbCr

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@mkdm", strMKDM)
                        .SelectCommand = objSqlCommand

                        'ִ�в���
                        .Fill(objTempMokuaiData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_YINGYONGXITONG_MOKUAI))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempMokuaiData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempMokuaiData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objMokuaiData = objTempMokuaiData
            getMokuaiData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempMokuaiData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ����ָ��strMKDM��ȡ������_B_Ӧ��ϵͳ_ģ�顱�����ݼ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strMKDM              ��ģ�����
        '     blnUnused            ��������
        '     objMokuaiData        ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getMokuaiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strMKDM As String, _
            ByVal blnUnused As Boolean, _
            ByRef objMokuaiData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempMokuaiData As Xydc.Platform.Common.Data.AppManagerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '��ʼ��
            getMokuaiData = False
            objMokuaiData = Nothing
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strMKDM Is Nothing Then strMKDM = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                strMKDM = strMKDM.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ����
                Dim strSQL As String
                Try
                    '�������ݼ�
                    objTempMokuaiData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_YINGYONGXITONG_MOKUAI)

                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ�м���
                    Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharFjdmSeparate
                    With Me.m_objSqlDataAdapter
                        '׼��SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from ����_B_Ӧ��ϵͳ_ģ�� a " + vbCr
                        strSQL = strSQL + " where a.ģ����� = @mkdm" + vbCr
                        strSQL = strSQL + " order by a.ģ����� " + vbCr

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@mkdm", strMKDM)
                        .SelectCommand = objSqlCommand

                        'ִ�в���
                        .Fill(objTempMokuaiData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_YINGYONGXITONG_MOKUAI))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempMokuaiData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempMokuaiData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objMokuaiData = objTempMokuaiData
            getMokuaiData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempMokuaiData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ����ָ��strMKDM��ȡ������_B_Ӧ��ϵͳ_ģ�顱�����ݼ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     intMKBS              ��ģ���ʶ
        '     blnUnused            ��������
        '     objMokuaiData        ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getMokuaiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intMKBS As Integer, _
            ByVal blnUnused As Boolean, _
            ByRef objMokuaiData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempMokuaiData As Xydc.Platform.Common.Data.AppManagerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '��ʼ��
            getMokuaiData = False
            objMokuaiData = Nothing
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ����
                Dim strSQL As String
                Try
                    '�������ݼ�
                    objTempMokuaiData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_YINGYONGXITONG_MOKUAI)

                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ�м���
                    Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharFjdmSeparate
                    With Me.m_objSqlDataAdapter
                        '׼��SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from ����_B_Ӧ��ϵͳ_ģ�� a " + vbCr
                        strSQL = strSQL + " where a.ģ���ʶ = @mkbs" + vbCr
                        strSQL = strSQL + " order by a.ģ����� " + vbCr

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@mkbs", intMKBS)
                        .SelectCommand = objSqlCommand

                        'ִ�в���
                        .Fill(objTempMokuaiData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_YINGYONGXITONG_MOKUAI))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempMokuaiData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempMokuaiData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objMokuaiData = objTempMokuaiData
            getMokuaiData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempMokuaiData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �����ϼ�ģ������ȡ�¼���ģ�����
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strPrevMKDM          ���ϼ�ģ�����
        '     strNewMKDM           ����ģ�����(����)
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getNewMKDM( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strPrevMKDM As String, _
            ByRef strNewMKDM As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            '��ʼ��
            getNewMKDM = False
            strNewMKDM = ""
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strPrevMKDM Is Nothing Then strPrevMKDM = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                strPrevMKDM = strPrevMKDM.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ�ϼ�ģ�鼶��
                Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharFjdmSeparate
                Dim intLevel As Integer = objPulicParameters.getCodeLevel(strPrevMKDM, strSep)
                If intLevel < 0 Then intLevel = 0

                '��ȡ����
                strSQL = ""
                strSQL = strSQL + " select max(��������) " + vbCr
                strSQL = strSQL + " from ����_B_Ӧ��ϵͳ_ģ�� " + vbCr
                strSQL = strSQL + " where ģ�鼶�� = " + (intLevel + 1).ToString() + vbCr         'ֱ���¼�
                If strPrevMKDM <> "" Then
                    strSQL = strSQL + " and ģ����� like '" + strPrevMKDM + strSep + "%'" + vbCr '�¼�
                End If
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    If strPrevMKDM = "" Then
                        strNewMKDM = "1"
                    Else
                        strNewMKDM = strPrevMKDM + strSep + "1"
                    End If
                Else
                    Dim intValue As Integer
                    With objDataSet.Tables(0).Rows(0)
                        intValue = objPulicParameters.getObjectValue(.Item(0), 0)
                    End With
                    intValue += 1
                    If strPrevMKDM = "" Then
                        strNewMKDM = intValue.ToString()
                    Else
                        strNewMKDM = strPrevMKDM + strSep + intValue.ToString()
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

            '����
            getNewMKDM = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ�µ�ģ���ʶ
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strNewMKBS           ����ģ���ʶ(����)
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getNewMKBS( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef strNewMKBS As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            '��ʼ��
            getNewMKBS = False
            strNewMKBS = ""
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getNewCode(strErrMsg, objSqlConnection, "ģ���ʶ", "����_B_Ӧ��ϵͳ_ģ��", True, strNewMKBS) = False Then
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

            '����
            getNewMKBS = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ����������ֵ��������ϵͳ�Զ������ֵ
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     objNewData           ��������(����)
        '     objenumEditType      ���༭����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getMokuaiDefaultValue( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef objNewData As System.Collections.Specialized.ListDictionary, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            getMokuaiDefaultValue = False

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If

                '��ȡģ���ʶ
                Dim strMKBS As String
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                        If Me.getNewMKBS(strErrMsg, strUserId, strPassword, strMKBS) = False Then
                            GoTo errProc
                        End If
                        objNewData(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKBS) = objPulicParameters.getObjectValue(strMKBS, 0)
                    Case Else
                End Select

                '��ȡģ�����
                Dim strMKDM As String
                strMKDM = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKDM), "")
                strMKDM = strMKDM.Trim()
                If strMKDM = "" Then
                    strErrMsg = "����[ģ�����]����Ϊ�գ�"
                    GoTo errProc
                End If
                Dim strTemp As String = strMKDM
                strTemp = strTemp.Replace(Xydc.Platform.Common.Utilities.PulicParameters.CharFjdmSeparate, "")
                If objPulicParameters.isNumericString(strTemp) = False Then
                    strErrMsg = "����[ģ�����]�д��ڷǷ��ַ���"
                    GoTo errProc
                End If

                '����ģ������ȡģ�鼶��
                Dim intLevel As Integer
                intLevel = objPulicParameters.getCodeLevel(strMKDM, Xydc.Platform.Common.Utilities.PulicParameters.CharFjdmSeparate)
                objNewData(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKJB) = intLevel

                '����ģ������ȡ��������
                Dim strBJDM As String
                strBJDM = objPulicParameters.getCodeValue(strMKDM, Xydc.Platform.Common.Utilities.PulicParameters.CharFjdmSeparate, intLevel)
                objNewData(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_BJDM) = objPulicParameters.getObjectValue(strBJDM, 0)

                '����ģ������ȡ����ģ��
                Dim objAppManagerData As Xydc.Platform.Common.Data.AppManagerData
                If intLevel <= 1 Then
                    objNewData(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_DJMK) = objNewData(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKBS)
                Else
                    Dim strDJMK As String
                    strDJMK = objPulicParameters.getCodeValue(strMKDM, Xydc.Platform.Common.Utilities.PulicParameters.CharFjdmSeparate, 1, True)

                    '���ݶ���ģ������ȡ����ģ���ʶ
                    If Me.getMokuaiData(strErrMsg, strUserId, strPassword, strDJMK, True, objAppManagerData) = False Then
                        GoTo errProc
                    End If
                    With objAppManagerData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_YINGYONGXITONG_MOKUAI)
                        If .Rows.Count < 1 Then
                            strErrMsg = "����[" + strDJMK + "]�����ڣ�"
                            GoTo errProc
                        Else
                            objNewData(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_DJMK) = .Rows(0).Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKBS)
                        End If
                    End With
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objAppManagerData)
                    objAppManagerData = Nothing
                End If

                '����ģ������ȡ�ϼ�ģ��
                If intLevel <= 1 Then
                    objNewData(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_SJMK) = objNewData(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_DJMK)
                Else
                    Dim strSJMK As String
                    strSJMK = objPulicParameters.getCodeValue(strMKDM, Xydc.Platform.Common.Utilities.PulicParameters.CharFjdmSeparate, intLevel - 1, True)

                    '���ݶ���ģ������ȡ�ϼ�ģ���ʶ
                    If Me.getMokuaiData(strErrMsg, strUserId, strPassword, strSJMK, True, objAppManagerData) = False Then
                        GoTo errProc
                    End If
                    With objAppManagerData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_YINGYONGXITONG_MOKUAI)
                        If .Rows.Count < 1 Then
                            strErrMsg = "����[" + strSJMK + "]�����ڣ�"
                            GoTo errProc
                        Else
                            objNewData(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_SJMK) = .Rows(0).Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKBS)
                        End If
                    End With
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objAppManagerData)
                    objAppManagerData = Nothing
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            getMokuaiDefaultValue = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��顰����_B_Ӧ��ϵͳ_ģ�顱�����ݵĺϷ���
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     objOldData           ��������
        '     objNewData           ��������
        '     objenumEditType      ���༭����
        ' ����
        '     True                 ���Ϸ�
        '     False                �����Ϸ��������������
        '----------------------------------------------------------------
        Public Function doVerifyMokuaiData( _
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

            doVerifyMokuaiData = False

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strUserId.Trim = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "����δ�����µ����ݣ�"
                    GoTo errProc
                End If
                Dim intOldMKBS As Integer
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                    Case Else
                        If objOldData Is Nothing Then
                            strErrMsg = "����δ����ɵ����ݣ�"
                            GoTo errProc
                        End If
                        intOldMKBS = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKBS), 0)
                End Select

                '��������ֵ���������Զ�ֵ����У�鶥�����ϼ�����
                If Me.getMokuaiDefaultValue(strErrMsg, strUserId, strPassword, objNewData, objenumEditType) = False Then
                    GoTo errProc
                End If

                '��ȡ��ṹ����
                strSQL = "select top 0 * from ����_B_Ӧ��ϵͳ_ģ��"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, strUserId, strPassword, strSQL, "����_B_Ӧ��ϵͳ_ģ��", objDataSet) = False Then
                    GoTo errProc
                End If

                '������ݳ���
                Dim objDictionaryEntry As System.Collections.DictionaryEntry
                Dim strField As String
                Dim intLen As Integer
                For Each objDictionaryEntry In objNewData
                    strField = objPulicParameters.getObjectValue(objDictionaryEntry.Key, "")
                    Select Case strField
                        Case Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKBS, _
                            Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKJB, _
                            Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_BJDM, _
                            Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_DJMK, _
                            Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_SJMK

                        Case Else
                            Dim strValue As String
                            strValue = objPulicParameters.getObjectValue(objDictionaryEntry.Value, "")
                            If strValue = "" Then
                                Select Case strField
                                    Case Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKSM
                                    Case Else
                                        strErrMsg = "����[" + strField + "]����Ϊ�գ�"
                                        GoTo errProc
                                End Select
                            End If
                            With objDataSet.Tables(0).Columns(strField)
                                intLen = objPulicParameters.getStringLength(strValue)
                                If intLen > .MaxLength Then
                                    strErrMsg = "����[" + strField + "]���Ȳ��ܳ���[" + .MaxLength.ToString() + "]��ʵ����[" + intLen.ToString() + "]��"
                                    GoTo errProc
                                End If
                            End With
                    End Select
                Next
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '��飺ģ���ʶ
                Dim intNewMKBS As Integer
                intNewMKBS = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKBS), 0)
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                        strSQL = ""
                        strSQL = strSQL + " select * from ����_B_Ӧ��ϵͳ_ģ�� "
                        strSQL = strSQL + " where ģ���ʶ = @newmkbs"
                        objListDictionary.Add("@newmkbs", intNewMKBS)
                    Case Else
                        strSQL = ""
                        strSQL = strSQL + " select * from ����_B_Ӧ��ϵͳ_ģ�� "
                        strSQL = strSQL + " where ģ���ʶ =  @newmkbs"
                        strSQL = strSQL + " and   ģ���ʶ <> @oldmkbs"
                        objListDictionary.Add("@newmkbs", intNewMKBS)
                        objListDictionary.Add("@oldmkbs", intOldMKBS)
                End Select
                If objdacCommon.getDataSetBySQL(strErrMsg, strUserId, strPassword, strSQL, objListDictionary, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strErrMsg = "����[" + intNewMKBS.ToString() + "]�Ѿ����ڣ�"
                    GoTo errProc
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing
                objListDictionary.Clear()

                '��飺ģ�����
                Dim strNewMKDM As String
                strNewMKDM = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKDM), "")
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                        strSQL = ""
                        strSQL = strSQL + " select * from ����_B_Ӧ��ϵͳ_ģ�� "
                        strSQL = strSQL + " where ģ����� = @newmkdm"
                        objListDictionary.Add("@newmkdm", strNewMKDM)
                    Case Else
                        strSQL = ""
                        strSQL = strSQL + " select * from ����_B_Ӧ��ϵͳ_ģ�� "
                        strSQL = strSQL + " where ģ����� =  @newmkdm"
                        strSQL = strSQL + " and   ģ���ʶ <> @oldmkbs"
                        objListDictionary.Add("@newmkdm", strNewMKDM)
                        objListDictionary.Add("@oldmkbs", intOldMKBS)
                End Select
                If objdacCommon.getDataSetBySQL(strErrMsg, strUserId, strPassword, strSQL, objListDictionary, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strErrMsg = "����[" + strNewMKDM.ToString() + "]�Ѿ����ڣ�"
                    GoTo errProc
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing
                objListDictionary.Clear()

                '��飺ģ������
                Dim strNewMKMC As String
                strNewMKMC = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKMC), "")
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                        strSQL = ""
                        strSQL = strSQL + " select * from ����_B_Ӧ��ϵͳ_ģ�� "
                        strSQL = strSQL + " where ģ������ = @newmkmc"
                        objListDictionary.Add("@newmkmc", strNewMKMC)
                    Case Else
                        strSQL = ""
                        strSQL = strSQL + " select * from ����_B_Ӧ��ϵͳ_ģ�� "
                        strSQL = strSQL + " where ģ������ =  @newmkmc"
                        strSQL = strSQL + " and   ģ���ʶ <> @oldmkbs"
                        objListDictionary.Add("@newmkmc", strNewMKMC)
                        objListDictionary.Add("@oldmkbs", intOldMKBS)
                End Select
                If objdacCommon.getDataSetBySQL(strErrMsg, strUserId, strPassword, strSQL, objListDictionary, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strErrMsg = "����[" + strNewMKMC.ToString() + "]�Ѿ����ڣ�"
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

            doVerifyMokuaiData = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objListDictionary)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���桰����_B_Ӧ��ϵͳ_ģ�顱������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     objOldData           ��������
        '     objNewData           ��������
        '     objenumEditType      ���༭����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doSaveMokuaiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.ListDictionary, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '��ʼ��
            doSaveMokuaiData = False
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strUserId.Trim = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "����δ�����µ����ݣ�"
                    GoTo errProc
                End If
                Dim strOldMKDM As String
                Dim intOldMKBS As Integer
                Dim strNewMKDM As String
                Dim intNewMKBS As Integer
                intNewMKBS = objPulicParameters.getObjectValue(objNewData.Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKBS), 0)
                strNewMKDM = objPulicParameters.getObjectValue(objNewData.Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKDM), "")
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                    Case Else
                        If objOldData Is Nothing Then
                            strErrMsg = "����δ����ɵ����ݣ�"
                            GoTo errProc
                        End If
                        intOldMKBS = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKBS), 0)
                        strOldMKDM = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKDM), "")
                End Select

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ʼ����
                Try
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '��������
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '����SQL
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
                            strSQL = strSQL + " insert into ����_B_Ӧ��ϵͳ_ģ�� (" + strFileds + ")"
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
                            strSQL = strSQL + " update ����_B_Ӧ��ϵͳ_ģ�� set "
                            strSQL = strSQL + "   " + strFileds
                            strSQL = strSQL + " where ģ���ʶ = @oldmkbs"
                            objSqlCommand.Parameters.Clear()
                            i = 0
                            For Each objDictionaryEntry In objNewData
                                objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), objDictionaryEntry.Value)
                                i += 1
                            Next
                            objSqlCommand.Parameters.AddWithValue("@oldmkbs", intOldMKBS)
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()

                            If strNewMKDM.ToUpper() <> strOldMKDM.ToUpper() Then
                                Dim intOldMKJB As Integer
                                intOldMKJB = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKJB), 0)
                                Dim intNewMKJB As Integer
                                intNewMKJB = objPulicParameters.getObjectValue(objNewData.Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKJB), 0)
                                Dim intNewDJMK As Integer
                                intNewDJMK = objPulicParameters.getObjectValue(objNewData.Item(Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAI_DJMK), 0)

                                '����ԭ�¼��Ĵ���
                                strSQL = ""
                                strSQL = strSQL + " update ����_B_Ӧ��ϵͳ_ģ�� set "
                                strSQL = strSQL + "   ģ����� = @newmkdm + substring(ģ�����, @oldmkdmlen + 1, len(ģ�����) - @oldmkdmlen),"
                                strSQL = strSQL + "   ģ�鼶�� = @newmkjb + ģ�鼶�� - @oldmkjb,"
                                strSQL = strSQL + "   ����ģ�� = @newdjmk "
                                strSQL = strSQL + " where ģ����� like @oldmkdm + @sep + '%'" '��ģ����¼�
                                objSqlCommand.Parameters.Clear()
                                objSqlCommand.Parameters.AddWithValue("@newmkdm", strNewMKDM)
                                objSqlCommand.Parameters.AddWithValue("@oldmkdmlen", strOldMKDM.Length)
                                objSqlCommand.Parameters.AddWithValue("@newmkjb", intNewMKJB)
                                objSqlCommand.Parameters.AddWithValue("@oldmkjb", intOldMKJB)
                                objSqlCommand.Parameters.AddWithValue("@newdjmk", intNewDJMK)
                                objSqlCommand.Parameters.AddWithValue("@newmkbs", intNewMKBS)
                                objSqlCommand.Parameters.AddWithValue("@oldmkdm", strOldMKDM)
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

                '�ύ����
                objSqlTransaction.Commit()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            doSaveMokuaiData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ����ģ�����ɾ��������_B_Ӧ��ϵͳ_ģ�顱������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strMKDM              ��ģ�����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doDeleteMokuaiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strMKDM As String) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '��ʼ��
            doDeleteMokuaiData = False
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strMKDM Is Nothing Then strMKDM = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                strMKDM = strMKDM.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ʼ����
                Try
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                'ɾ������
                Dim strSQL As String
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ɾ������_B_Ӧ��ϵͳ_ģ��
                    strSQL = ""
                    strSQL = strSQL + " delete from ����_B_Ӧ��ϵͳ_ģ�� "
                    strSQL = strSQL + " where ģ����� like @mkdm + @sep +'%' "
                    strSQL = strSQL + " or    ģ����� = @mkdm"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@mkdm", strMKDM)
                    objSqlCommand.Parameters.AddWithValue("@sep", Xydc.Platform.Common.Utilities.PulicParameters.CharFjdmSeparate)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '�ύ����
                objSqlTransaction.Commit()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            doDeleteMokuaiData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ��ɫ��ģ��Ȩ����������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objConnectionProperty�����Ӳ���
        '     strRoleName          ����ɫ��
        '     strWhere             �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     objRoleMKQXData      ����ɫȨ������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getRoleMokuaiQXData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strRoleName As String, _
            ByVal strWhere As String, _
            ByRef objRoleMKQXData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempRoleMKQXData As Xydc.Platform.Common.Data.AppManagerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '��ʼ��
            getRoleMokuaiQXData = False
            objRoleMKQXData = Nothing
            strErrMsg = ""

            Try
                '���
                If strRoleName Is Nothing Then strRoleName = ""
                If strWhere Is Nothing Then strWhere = ""
                strRoleName = strRoleName.Trim()
                strWhere = strWhere.Trim()
                If objConnectionProperty Is Nothing Then
                    objTempRoleMKQXData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_YINGYONGXITONG_MOKUAIQX)
                    Exit Try
                End If

                '��ͬ������
                If objConnectionProperty.DataSource.ToUpper() <> Xydc.Platform.Common.jsoaConfiguration.DatabaseServerName.ToUpper() Then
                    objTempRoleMKQXData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_YINGYONGXITONG_MOKUAIQX)
                    Exit Try
                End If

                '��ȡ����
                With objConnectionProperty

                    'If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, -1, .InitialCatalog, .DataSource) = False Then
                    '    GoTo errProc
                    'End If
                    If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, Platform.Common.jsoaConfiguration.ConnectionTestTimeout, .InitialCatalog, .DataSource) = False Then
                        GoTo errProc
                    End If

                End With

                '��ȡ����
                Dim strSQL As String
                Try
                    '�������ݼ�
                    objTempRoleMKQXData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_YINGYONGXITONG_MOKUAIQX)

                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ�м���
                    With Me.m_objSqlDataAdapter
                        Dim strDefDB As String = Xydc.Platform.Common.jsoaConfiguration.DatabaseServerUserDB
                        Dim strCurDB As String = objConnectionProperty.InitialCatalog
                        Dim intUserType As Integer = Xydc.Platform.Common.Data.AppManagerData.enumUserType.isSqlRole

                        '׼��SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.*" + vbCr
                        strSQL = strSQL + " from (" + vbCr
                        strSQL = strSQL + "   select " + vbCr
                        strSQL = strSQL + "     a.ģ���ʶ, a.ģ�����, a.ģ������, a.˵��," + vbCr
                        strSQL = strSQL + "     b.Ȩ�޴���, b.�û���ʶ, b.�û�����," + vbCr
                        strSQL = strSQL + "     ִ��Ȩ = case when b.Ȩ�޴��� is null then @False else @True end" + vbCr
                        strSQL = strSQL + "   from " + strDefDB + ".dbo.����_B_Ӧ��ϵͳ_ģ�� a" + vbCr
                        strSQL = strSQL + "   left join (" + vbCr
                        strSQL = strSQL + "     select Ȩ�޴���,�û���ʶ,�û�����,ģ���ʶ" + vbCr
                        strSQL = strSQL + "     from " + strDefDB + ".dbo.����_B_Ӧ��ϵͳ_ģ��Ȩ��" + vbCr
                        strSQL = strSQL + "     where �û���ʶ = @rolename" + vbCr
                        strSQL = strSQL + "     and   �û����� = @usertype" + vbCr
                        strSQL = strSQL + "   ) b on a.ģ���ʶ = b.ģ���ʶ " + vbCr
                        strSQL = strSQL + " ) a" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.ģ�����" + vbCr

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@False", Xydc.Platform.Common.Utilities.PulicParameters.CharFalse)
                        objSqlCommand.Parameters.AddWithValue("@True", Xydc.Platform.Common.Utilities.PulicParameters.CharTrue)
                        objSqlCommand.Parameters.AddWithValue("@rolename", strRoleName)
                        objSqlCommand.Parameters.AddWithValue("@usertype", intUserType)
                        .SelectCommand = objSqlCommand

                        'ִ�в���
                        .Fill(objTempRoleMKQXData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_YINGYONGXITONG_MOKUAIQX))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempRoleMKQXData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempRoleMKQXData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objRoleMKQXData = objTempRoleMKQXData
            getRoleMokuaiQXData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempRoleMKQXData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ�û���ģ��Ȩ����������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objConnectionProperty�����Ӳ���
        '     strDBUserName        ���û���
        '     strWhere             �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     objDBUserMKQXData    ����ɫȨ������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getDBUserMokuaiQXData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strDBUserName As String, _
            ByVal strWhere As String, _
            ByRef objDBUserMKQXData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempDBUserMKQXData As Xydc.Platform.Common.Data.AppManagerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '��ʼ��
            getDBUserMokuaiQXData = False
            objDBUserMKQXData = Nothing
            strErrMsg = ""

            Try
                '���
                If strDBUserName Is Nothing Then strDBUserName = ""
                If strWhere Is Nothing Then strWhere = ""
                strDBUserName = strDBUserName.Trim()
                strWhere = strWhere.Trim()
                If objConnectionProperty Is Nothing Then
                    objTempDBUserMKQXData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_YINGYONGXITONG_MOKUAIQX)
                    Exit Try
                End If

                '��ͬ������
                If objConnectionProperty.DataSource.ToUpper() <> Xydc.Platform.Common.jsoaConfiguration.DatabaseServerName.ToUpper() Then
                    objTempDBUserMKQXData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_YINGYONGXITONG_MOKUAIQX)
                    Exit Try
                End If

                '��ȡ����
                With objConnectionProperty

                    'If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, -1, .InitialCatalog, .DataSource) = False Then
                    '    GoTo errProc
                    'End If
                    If objdacCommon.getConnection(strErrMsg, objSqlConnection, .UserID, .Password, Platform.Common.jsoaConfiguration.ConnectionTestTimeout, .InitialCatalog, .DataSource) = False Then
                        GoTo errProc
                    End If

                End With

                '��ȡ����
                Dim strSQL As String
                Try
                    '�������ݼ�
                    objTempDBUserMKQXData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_YINGYONGXITONG_MOKUAIQX)

                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ�м���
                    With Me.m_objSqlDataAdapter
                        Dim strDefDB As String = Xydc.Platform.Common.jsoaConfiguration.DatabaseServerUserDB
                        Dim strCurDB As String = objConnectionProperty.InitialCatalog
                        Dim intUserType As Integer = Xydc.Platform.Common.Data.AppManagerData.enumUserType.isSqlUser

                        '׼��SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.*" + vbCr
                        strSQL = strSQL + " from (" + vbCr
                        strSQL = strSQL + "   select " + vbCr
                        strSQL = strSQL + "     a.ģ���ʶ, a.ģ�����, a.ģ������, a.˵��," + vbCr
                        strSQL = strSQL + "     b.Ȩ�޴���, b.�û���ʶ, b.�û�����," + vbCr
                        strSQL = strSQL + "     ִ��Ȩ = case when b.Ȩ�޴��� is null then @False else @True end" + vbCr
                        strSQL = strSQL + "   from " + strDefDB + ".dbo.����_B_Ӧ��ϵͳ_ģ�� a" + vbCr
                        strSQL = strSQL + "   left join (" + vbCr
                        strSQL = strSQL + "     select Ȩ�޴���,�û���ʶ,�û�����,ģ���ʶ" + vbCr
                        strSQL = strSQL + "     from " + strDefDB + ".dbo.����_B_Ӧ��ϵͳ_ģ��Ȩ��" + vbCr
                        strSQL = strSQL + "     where �û���ʶ = @dbusername" + vbCr
                        strSQL = strSQL + "     and   �û����� = @usertype" + vbCr
                        strSQL = strSQL + "   ) b on a.ģ���ʶ = b.ģ���ʶ " + vbCr
                        strSQL = strSQL + " ) a" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.ģ�����" + vbCr

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@False", Xydc.Platform.Common.Utilities.PulicParameters.CharFalse)
                        objSqlCommand.Parameters.AddWithValue("@True", Xydc.Platform.Common.Utilities.PulicParameters.CharTrue)
                        objSqlCommand.Parameters.AddWithValue("@dbusername", strDBUserName)
                        objSqlCommand.Parameters.AddWithValue("@usertype", intUserType)
                        .SelectCommand = objSqlCommand

                        'ִ�в���
                        .Fill(objTempDBUserMKQXData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_YINGYONGXITONG_MOKUAIQX))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDBUserMKQXData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempDBUserMKQXData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objDBUserMKQXData = objTempDBUserMKQXData
            getDBUserMokuaiQXData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempDBUserMKQXData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ����ɫstrRoleName����ָ��ģ��strMKBS��Ȩ��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strRoleName          ����ɫ��
        '     strMKBS              ��ģ���ʶ
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doGrantRoleMokuaiQX( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRoleName As String, _
            ByVal strMKBS As String) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '��ʼ��
            doGrantRoleMokuaiQX = False
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strRoleName Is Nothing Then strRoleName = ""
                If strMKBS Is Nothing Then strMKBS = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                strRoleName = strRoleName.Trim()
                strMKBS = strMKBS.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ʼ����
                Try
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '��ȡ����
                Dim strGrant As String = ""
                Dim strSQL As String = ""
                Try
                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '����
                    Dim intUserType As Integer = Xydc.Platform.Common.Data.AppManagerData.enumUserType.isSqlRole
                    strSQL = ""
                    strSQL = strSQL + " delete from ����_B_Ӧ��ϵͳ_ģ��Ȩ�� " + vbCr
                    strSQL = strSQL + " where �û���ʶ = @rolename " + vbCr
                    strSQL = strSQL + " and   �û����� = @usertype " + vbCr
                    strSQL = strSQL + " and   ģ���ʶ = @mkbs" + vbCr
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@rolename", strRoleName)
                    objSqlCommand.Parameters.AddWithValue("@usertype", intUserType)
                    objSqlCommand.Parameters.AddWithValue("@mkbs", strMKBS)
                    objSqlCommand.ExecuteNonQuery()

                    strSQL = ""
                    strSQL = strSQL + " insert into ����_B_Ӧ��ϵͳ_ģ��Ȩ��(�û���ʶ,�û�����,ģ���ʶ,ִ��Ȩ) " + vbCr
                    strSQL = strSQL + " values(@rolename,@usertype,@mkbs,@execute) " + vbCr
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@rolename", strRoleName)
                    objSqlCommand.Parameters.AddWithValue("@usertype", intUserType)
                    objSqlCommand.Parameters.AddWithValue("@mkbs", strMKBS)
                    objSqlCommand.Parameters.AddWithValue("@execute", 1)
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '�ύ����
                objSqlTransaction.Commit()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            doGrantRoleMokuaiQX = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �ӽ�ɫstrRoleName����ָ��ģ��strMKBS��Ȩ��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strRoleName          ����ɫ��
        '     strMKBS              ��ģ���ʶ
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doRevokeRoleMokuaiQX( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRoleName As String, _
            ByVal strMKBS As String) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '��ʼ��
            doRevokeRoleMokuaiQX = False
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strRoleName Is Nothing Then strRoleName = ""
                If strMKBS Is Nothing Then strMKBS = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                strRoleName = strRoleName.Trim()
                strMKBS = strMKBS.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ʼ����
                Try
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '��ȡ����
                Dim strGrant As String = ""
                Dim strSQL As String = ""
                Try
                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '����
                    Dim intUserType As Integer = Xydc.Platform.Common.Data.AppManagerData.enumUserType.isSqlRole
                    strSQL = ""
                    strSQL = strSQL + " delete from ����_B_Ӧ��ϵͳ_ģ��Ȩ�� " + vbCr
                    strSQL = strSQL + " where �û���ʶ = @rolename " + vbCr
                    strSQL = strSQL + " and   �û����� = @usertype " + vbCr
                    strSQL = strSQL + " and   ģ���ʶ = @mkbs" + vbCr
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@rolename", strRoleName)
                    objSqlCommand.Parameters.AddWithValue("@usertype", intUserType)
                    objSqlCommand.Parameters.AddWithValue("@mkbs", strMKBS)
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '�ύ����
                objSqlTransaction.Commit()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            doRevokeRoleMokuaiQX = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���û�strDBUserName����ָ��ģ��strMKBS��Ȩ��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strDBUserName        ���û���
        '     strMKBS              ��ģ���ʶ
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doGrantDBuserMokuaiQX( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strDBUserName As String, _
            ByVal strMKBS As String) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '��ʼ��
            doGrantDBuserMokuaiQX = False
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strDBUserName Is Nothing Then strDBUserName = ""
                If strMKBS Is Nothing Then strMKBS = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                strDBUserName = strDBUserName.Trim()
                strMKBS = strMKBS.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ʼ����
                Try
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '��ȡ����
                Dim strGrant As String = ""
                Dim strSQL As String = ""
                Try
                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '����
                    Dim intUserType As Integer = Xydc.Platform.Common.Data.AppManagerData.enumUserType.isSqlUser
                    strSQL = ""
                    strSQL = strSQL + " delete from ����_B_Ӧ��ϵͳ_ģ��Ȩ�� " + vbCr
                    strSQL = strSQL + " where �û���ʶ = @dbusername " + vbCr
                    strSQL = strSQL + " and   �û����� = @usertype " + vbCr
                    strSQL = strSQL + " and   ģ���ʶ = @mkbs" + vbCr
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@dbusername", strDBUserName)
                    objSqlCommand.Parameters.AddWithValue("@usertype", intUserType)
                    objSqlCommand.Parameters.AddWithValue("@mkbs", strMKBS)
                    objSqlCommand.ExecuteNonQuery()

                    strSQL = ""
                    strSQL = strSQL + " insert into ����_B_Ӧ��ϵͳ_ģ��Ȩ��(�û���ʶ,�û�����,ģ���ʶ,ִ��Ȩ) " + vbCr
                    strSQL = strSQL + " values(@dbusername,@usertype,@mkbs,@execute) " + vbCr
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@dbusername", strDBUserName)
                    objSqlCommand.Parameters.AddWithValue("@usertype", intUserType)
                    objSqlCommand.Parameters.AddWithValue("@mkbs", strMKBS)
                    objSqlCommand.Parameters.AddWithValue("@execute", 1)
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '�ύ����
                objSqlTransaction.Commit()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            doGrantDBuserMokuaiQX = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���û�strDBUserName����ָ��ģ��strMKBS��Ȩ��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strDBUserName        ���û���
        '     strMKBS              ��ģ���ʶ
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doRevokeDBUserMokuaiQX( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strDBUserName As String, _
            ByVal strMKBS As String) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '��ʼ��
            doRevokeDBUserMokuaiQX = False
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strDBUserName Is Nothing Then strDBUserName = ""
                If strMKBS Is Nothing Then strMKBS = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                strDBUserName = strDBUserName.Trim()
                strMKBS = strMKBS.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ʼ����
                Try
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '��ȡ����
                Dim strGrant As String = ""
                Dim strSQL As String = ""
                Try
                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '����
                    Dim intUserType As Integer = Xydc.Platform.Common.Data.AppManagerData.enumUserType.isSqlUser
                    strSQL = ""
                    strSQL = strSQL + " delete from ����_B_Ӧ��ϵͳ_ģ��Ȩ�� " + vbCr
                    strSQL = strSQL + " where �û���ʶ = @dbusername " + vbCr
                    strSQL = strSQL + " and   �û����� = @usertype " + vbCr
                    strSQL = strSQL + " and   ģ���ʶ = @mkbs" + vbCr
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@dbusername", strDBUserName)
                    objSqlCommand.Parameters.AddWithValue("@usertype", intUserType)
                    objSqlCommand.Parameters.AddWithValue("@mkbs", strMKBS)
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '�ύ����
                objSqlTransaction.Commit()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            doRevokeDBUserMokuaiQX = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ�û���ģ��Ȩ����������(ͬʱ����û�������ɫ��Ȩ������)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strDBUserName        ���û���
        '     objDBUserMKQXData    ����ɫȨ������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getDBUserMokuaiQXData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strDBUserName As String, _
            ByRef objDBUserMKQXData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempDBUserMKQXData As Xydc.Platform.Common.Data.AppManagerData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '��ʼ��
            getDBUserMokuaiQXData = False
            objDBUserMKQXData = Nothing
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strDBUserName Is Nothing Then strDBUserName = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()
                strDBUserName = strDBUserName.Trim()
                If strUserId = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ����
                Dim strSQL As String
                Try
                    '�������ݼ�
                    objTempDBUserMKQXData = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_B_YINGYONGXITONG_MOKUAIQX)

                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ�м���
                    With Me.m_objSqlDataAdapter
                        Dim strDefDB As String = Xydc.Platform.Common.jsoaConfiguration.DatabaseServerUserDB
                        Dim intUserType As Integer = Xydc.Platform.Common.Data.AppManagerData.enumUserType.isSqlUser
                        Dim intRoleType As Integer = Xydc.Platform.Common.Data.AppManagerData.enumUserType.isSqlRole

                        '׼��SQL
                        If strDBUserName.ToUpper = "SA" Then
                            'ȫ��Ȩ�ޣ�����
                            strSQL = ""
                            strSQL = strSQL + " select a.*" + vbCr
                            strSQL = strSQL + " from (" + vbCr
                            strSQL = strSQL + "   select " + vbCr
                            strSQL = strSQL + "     Ȩ�޴���=0,a.�û���ʶ=@dbusername,a.�û�����=@usertype,a.ģ���ʶ," + vbCr
                            strSQL = strSQL + "     a.ģ�����,a.ģ������,a.˵��," + vbCr
                            strSQL = strSQL + "     ִ��Ȩ=@True" + vbCr
                            strSQL = strSQL + "   from " + strDefDB + ".dbo.����_B_Ӧ��ϵͳ_ģ�� a " + vbCr
                            strSQL = strSQL + " ) a " + vbCr
                            strSQL = strSQL + " order by a.ģ�����" + vbCr

                            '���ò���
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@True", Xydc.Platform.Common.Utilities.PulicParameters.CharTrue)
                            objSqlCommand.Parameters.AddWithValue("@dbusername", strDBUserName)
                            objSqlCommand.Parameters.AddWithValue("@usertype", intUserType)
                            .SelectCommand = objSqlCommand
                        Else
                            strSQL = ""
                            strSQL = strSQL + " select a.*" + vbCr
                            strSQL = strSQL + " from (" + vbCr
                            strSQL = strSQL + "   select " + vbCr
                            strSQL = strSQL + "     a.Ȩ�޴���,a.�û���ʶ,a.�û�����,a.ģ���ʶ," + vbCr
                            strSQL = strSQL + "     b.ģ�����,b.ģ������,b.˵��," + vbCr
                            strSQL = strSQL + "     ִ��Ȩ=@True" + vbCr
                            strSQL = strSQL + "   from " + strDefDB + ".dbo.����_B_Ӧ��ϵͳ_ģ��Ȩ�� a " + vbCr
                            strSQL = strSQL + "   left join " + strDefDB + ".dbo.����_B_Ӧ��ϵͳ_ģ�� b on a.ģ���ʶ = b.ģ���ʶ " + vbCr
                            strSQL = strSQL + "   left join (" + vbCr                                               '�û�������ɫ
                            strSQL = strSQL + "     select �û���ʶ=c.name, �û�����=@roletype" + vbCr
                            strSQL = strSQL + "     from " + strDefDB + ".dbo.sysmembers a " + vbCr
                            strSQL = strSQL + "     left join " + strDefDB + ".dbo.sysusers b on a.memberuid = b.uid" + vbCr
                            strSQL = strSQL + "     left join " + strDefDB + ".dbo.sysusers c on a.groupuid  = c.uid" + vbCr
                            strSQL = strSQL + "     where b.name = @dbusername" + vbCr
                            strSQL = strSQL + "     group by c.name" + vbCr
                            strSQL = strSQL + "   ) c on a.�û���ʶ=c.�û���ʶ and a.�û�����=c.�û�����" + vbCr
                            strSQL = strSQL + "   where b.ģ����� is not null" + vbCr                             'ģ�����
                            strSQL = strSQL + "   and ((a.�û���ʶ=@dbusername and a.�û�����=@usertype) " + vbCr  '�û���Ȩ
                            strSQL = strSQL + "   or   (c.�û���ʶ is not null)) " + vbCr                          '��ɫ��Ȩ
                            strSQL = strSQL + " ) a " + vbCr
                            strSQL = strSQL + " order by a.ģ�����" + vbCr

                            '���ò���
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@True", Xydc.Platform.Common.Utilities.PulicParameters.CharTrue)
                            objSqlCommand.Parameters.AddWithValue("@roletype", intRoleType)
                            objSqlCommand.Parameters.AddWithValue("@dbusername", strDBUserName)
                            objSqlCommand.Parameters.AddWithValue("@usertype", intUserType)
                            .SelectCommand = objSqlCommand
                        End If

                        'ִ�в���
                        .Fill(objTempDBUserMKQXData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_YINGYONGXITONG_MOKUAIQX))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDBUserMKQXData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempDBUserMKQXData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objDBUserMKQXData = objTempDBUserMKQXData
            getDBUserMokuaiQXData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempDBUserMKQXData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function








        '----------------------------------------------------------------
        ' ��ȡһ���û�������־
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strTempPath          ����ʱ�ļ�Ŀ¼
        '     strWhere             �������ַ���(���ݼ������ַ���)
        '     objLogDataSet        ����������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getDataSet_JSOALOG( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strTempPath As String, _
            ByVal strWhere As String, _
            ByRef objLogDataSet As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objTempLogDataSet As Xydc.Platform.Common.Data.AppManagerData
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '��ʼ��
            getDataSet_JSOALOG = False
            objLogDataSet = Nothing
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "����[getDataSet_JSOALOG]δָ�������û���"
                    GoTo errProc
                End If
                If strTempPath Is Nothing Then strTempPath = ""
                strTempPath = strTempPath.Trim
                If strTempPath = "" Then
                    strErrMsg = "����[getDataSet_JSOALOG]δָ����ʱĿ¼��"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim

                '��ȡ����
                Try
                    '�������ݼ�
                    objTempLogDataSet = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_VT_B_JSOALOG)

                    '��ȡXML�ļ�
                    Dim strXMLFile As String = Xydc.Platform.SystemFramework.ApplicationConfiguration.TracingTraceFile

                    '���Ƶ���ʱ�ļ�
                    Dim strFileName As String
                    If objBaseLocalFile.doCopyToTempFile(strErrMsg, strXMLFile, strTempPath, strFileName) = False Then
                        GoTo errProc
                    End If
                    strFileName = objBaseLocalFile.doMakePath(strTempPath, strFileName)

                    'дXML�ļ�������־
                    Dim objFileInfo As New System.IO.FileInfo(strFileName)
                    Dim objFileStream As System.IO.FileStream
                    objFileStream = objFileInfo.Open(FileMode.Append, FileAccess.Write, FileShare.ReadWrite)
                    Dim objStreamWriter As System.IO.StreamWriter
                    objStreamWriter = New System.IO.StreamWriter(objFileStream)
                    objStreamWriter.WriteLine("</jsoalog>")
                    objStreamWriter.Flush()
                    objStreamWriter.Close()
                    objFileStream.Close()
                    objStreamWriter = Nothing
                    objFileStream = Nothing
                    objFileInfo = Nothing

                    '��XML��������
                    objTempLogDataSet.ReadXml(strFileName)

                    '���ù�������
                    With objTempLogDataSet.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_VT_B_JSOALOG)
                        .DefaultView.RowFilter = strWhere
                    End With

                    'ɾ����ʱ�ļ�
                    objBaseLocalFile.doDeleteFile(strErrMsg, strFileName)

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempLogDataSet.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempLogDataSet)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objLogDataSet = objTempLogDataSet
            getDataSet_JSOALOG = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ���ù���Ա������־
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strTempPath          ����ʱ�ļ�Ŀ¼
        '     strWhere             �������ַ���(���ݼ������ַ���)
        '     objLogDataSet        ����������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getDataSet_AUDITPZLOG( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strTempPath As String, _
            ByVal strWhere As String, _
            ByRef objLogDataSet As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objTempLogDataSet As Xydc.Platform.Common.Data.AppManagerData
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '��ʼ��
            getDataSet_AUDITPZLOG = False
            objLogDataSet = Nothing
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "����[getDataSet_AUDITPZLOG]δָ�������û���"
                    GoTo errProc
                End If
                If strTempPath Is Nothing Then strTempPath = ""
                strTempPath = strTempPath.Trim
                If strTempPath = "" Then
                    strErrMsg = "����[getDataSet_AUDITPZLOG]δָ����ʱĿ¼��"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim

                '��ȡ����
                Try
                    '�������ݼ�
                    objTempLogDataSet = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_VT_B_AUDITPZLOG)

                    '��ȡXML�ļ�
                    Dim strXMLFile As String = Xydc.Platform.SystemFramework.ApplicationConfiguration.TracingAuditPZFile

                    '���Ƶ���ʱ�ļ�
                    Dim strFileName As String
                    If objBaseLocalFile.doCopyToTempFile(strErrMsg, strXMLFile, strTempPath, strFileName) = False Then
                        GoTo errProc
                    End If
                    strFileName = objBaseLocalFile.doMakePath(strTempPath, strFileName)

                    'дXML�ļ�������־
                    Dim objFileInfo As New System.IO.FileInfo(strFileName)
                    Dim objFileStream As System.IO.FileStream
                    objFileStream = objFileInfo.Open(FileMode.Append, FileAccess.Write, FileShare.ReadWrite)
                    Dim objStreamWriter As System.IO.StreamWriter
                    objStreamWriter = New System.IO.StreamWriter(objFileStream)
                    objStreamWriter.WriteLine("</auditpzlog>")
                    objStreamWriter.Flush()
                    objStreamWriter.Close()
                    objFileStream.Close()
                    objStreamWriter = Nothing
                    objFileStream = Nothing
                    objFileInfo = Nothing

                    '��XML��������
                    objTempLogDataSet.ReadXml(strFileName)

                    '���ù�������
                    With objTempLogDataSet.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_VT_B_AUDITPZLOG)
                        .DefaultView.RowFilter = strWhere
                    End With

                    'ɾ����ʱ�ļ�
                    objBaseLocalFile.doDeleteFile(strErrMsg, strFileName)

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempLogDataSet.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempLogDataSet)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objLogDataSet = objTempLogDataSet
            getDataSet_AUDITPZLOG = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ��ȫ����Ա������־
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strTempPath          ����ʱ�ļ�Ŀ¼
        '     strWhere             �������ַ���(���ݼ������ַ���)
        '     objLogDataSet        ����������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getDataSet_AUDITAQLOG( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strTempPath As String, _
            ByVal strWhere As String, _
            ByRef objLogDataSet As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objTempLogDataSet As Xydc.Platform.Common.Data.AppManagerData
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '��ʼ��
            getDataSet_AUDITAQLOG = False
            objLogDataSet = Nothing
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "����[getDataSet_AUDITAQLOG]δָ�������û���"
                    GoTo errProc
                End If
                If strTempPath Is Nothing Then strTempPath = ""
                strTempPath = strTempPath.Trim
                If strTempPath = "" Then
                    strErrMsg = "����[getDataSet_AUDITAQLOG]δָ����ʱĿ¼��"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim

                '��ȡ����
                Try
                    '�������ݼ�
                    objTempLogDataSet = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_VT_B_AUDITAQLOG)

                    '��ȡXML�ļ�
                    Dim strXMLFile As String = Xydc.Platform.SystemFramework.ApplicationConfiguration.TracingAuditAQFile

                    '���Ƶ���ʱ�ļ�
                    Dim strFileName As String
                    If objBaseLocalFile.doCopyToTempFile(strErrMsg, strXMLFile, strTempPath, strFileName) = False Then
                        GoTo errProc
                    End If
                    strFileName = objBaseLocalFile.doMakePath(strTempPath, strFileName)

                    'дXML�ļ�������־
                    Dim objFileInfo As New System.IO.FileInfo(strFileName)
                    Dim objFileStream As System.IO.FileStream
                    objFileStream = objFileInfo.Open(FileMode.Append, FileAccess.Write, FileShare.ReadWrite)
                    Dim objStreamWriter As System.IO.StreamWriter
                    objStreamWriter = New System.IO.StreamWriter(objFileStream)
                    objStreamWriter.WriteLine("</auditaqlog>")
                    objStreamWriter.Flush()
                    objStreamWriter.Close()
                    objFileStream.Close()
                    objStreamWriter = Nothing
                    objFileStream = Nothing
                    objFileInfo = Nothing

                    '��XML��������
                    objTempLogDataSet.ReadXml(strFileName)

                    '���ù�������
                    With objTempLogDataSet.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_VT_B_AUDITAQLOG)
                        .DefaultView.RowFilter = strWhere
                    End With

                    'ɾ����ʱ�ļ�
                    objBaseLocalFile.doDeleteFile(strErrMsg, strFileName)

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempLogDataSet.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempLogDataSet)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objLogDataSet = objTempLogDataSet
            getDataSet_AUDITAQLOG = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ��ƹ���Ա������־
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strTempPath          ����ʱ�ļ�Ŀ¼
        '     strWhere             �������ַ���(���ݼ������ַ���)
        '     objLogDataSet        ����������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getDataSet_AUDITSJLOG( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strTempPath As String, _
            ByVal strWhere As String, _
            ByRef objLogDataSet As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objTempLogDataSet As Xydc.Platform.Common.Data.AppManagerData
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '��ʼ��
            getDataSet_AUDITSJLOG = False
            objLogDataSet = Nothing
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "����[getDataSet_AUDITSJLOG]δָ�������û���"
                    GoTo errProc
                End If
                If strTempPath Is Nothing Then strTempPath = ""
                strTempPath = strTempPath.Trim
                If strTempPath = "" Then
                    strErrMsg = "����[getDataSet_AUDITSJLOG]δָ����ʱĿ¼��"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim

                '��ȡ����
                Try
                    '�������ݼ�
                    objTempLogDataSet = New Xydc.Platform.Common.Data.AppManagerData(Xydc.Platform.Common.Data.AppManagerData.enumTableType.GL_VT_B_AUDITSJLOG)

                    '��ȡXML�ļ�
                    Dim strXMLFile As String = Xydc.Platform.SystemFramework.ApplicationConfiguration.TracingAuditSJFile

                    '���Ƶ���ʱ�ļ�
                    Dim strFileName As String
                    If objBaseLocalFile.doCopyToTempFile(strErrMsg, strXMLFile, strTempPath, strFileName) = False Then
                        GoTo errProc
                    End If
                    strFileName = objBaseLocalFile.doMakePath(strTempPath, strFileName)

                    'дXML�ļ�������־
                    Dim objFileInfo As New System.IO.FileInfo(strFileName)
                    Dim objFileStream As System.IO.FileStream
                    objFileStream = objFileInfo.Open(FileMode.Append, FileAccess.Write, FileShare.ReadWrite)
                    Dim objStreamWriter As System.IO.StreamWriter
                    objStreamWriter = New System.IO.StreamWriter(objFileStream)
                    objStreamWriter.WriteLine("</auditsjlog>")
                    objStreamWriter.Flush()
                    objStreamWriter.Close()
                    objFileStream.Close()
                    objStreamWriter = Nothing
                    objFileStream = Nothing
                    objFileInfo = Nothing

                    '��XML��������
                    objTempLogDataSet.ReadXml(strFileName)

                    '���ù�������
                    With objTempLogDataSet.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_VT_B_AUDITSJLOG)
                        .DefaultView.RowFilter = strWhere
                    End With

                    'ɾ����ʱ�ļ�
                    objBaseLocalFile.doDeleteFile(strErrMsg, strFileName)

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempLogDataSet.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objTempLogDataSet)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objLogDataSet = objTempLogDataSet
            getDataSet_AUDITSJLOG = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

       
    End Class

End Namespace
