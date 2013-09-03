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
    ' �����ռ䣺Xydc.Platform.DataAccess
    ' ����    ��dacMyJiaotan
    '
    ' ����������
    '     �ṩ�ԡ�����_B_��̸��ģ���漰�����ݲ����
    '----------------------------------------------------------------

    Public Class dacMyJiaotan
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.DataAccess.dacMyJiaotan)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub










        '----------------------------------------------------------------
        ' SqlDataAdapter����
        '----------------------------------------------------------------
        Protected ReadOnly Property SqlDataAdapter() As System.Data.SqlClient.SqlDataAdapter
            Get
                SqlDataAdapter = m_objSqlDataAdapter
            End Get
        End Property








        '----------------------------------------------------------------
        ' �����ʱ�������ݵ�Excel
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objDataSet           ��Ҫ���������ݼ�
        '     strExcelFile         ��������WEB�������е�Excel�ļ�·��
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
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
        ' ��ȡ[������=strUserXM]�Ľ�̸����
        ' ��ȡ������_B_��̸�������ݼ�(�Է���ʱ�併������)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strUserXM            ����ǰ����Ա����
        '     strWhere             �������ַ���
        '     objJiaotanDataSet    ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByVal strWhere As String, _
            ByRef objJiaotanDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData) As Boolean

            Dim objTempJiaotanDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '��ʼ��
            getDataSet = False
            objJiaotanDataSet = Nothing
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ����
                Try
                    '�������ݼ�
                    objTempJiaotanDataSet = New Xydc.Platform.Common.Data.grswMyJiaotanData(Xydc.Platform.Common.Data.grswMyJiaotanData.enumTableType.GG_B_JIAOTAN)

                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ�м���
                    With Me.m_objSqlDataAdapter
                        '׼��SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from ����_B_��̸ a " + vbCr
                        strSQL = strSQL + " where a.������ = '" + strUserXM + "'" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " and " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.����ʱ�� desc " + vbCr

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        'ִ�в���
                        .Fill(objTempJiaotanDataSet.Tables(Xydc.Platform.Common.Data.grswMyJiaotanData.TABLE_GG_B_JIAOTAN))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempJiaotanDataSet.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.grswMyJiaotanData.SafeRelease(objTempJiaotanDataSet)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objJiaotanDataSet = objTempJiaotanDataSet
            getDataSet = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.grswMyJiaotanData.SafeRelease(objTempJiaotanDataSet)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ[������=strUserXM]����������
        ' ��ȡ������_B_��̸�������ݼ�(�Է���ʱ�併������)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strUserXM            ����ǰ����Ա����
        '     strWhere             �������ַ���
        '     blnUnused            ���ӿ�������
        '     objJiaotanDataSet    ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByVal strWhere As String, _
            ByVal blnUnused As Boolean, _
            ByRef objJiaotanDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData) As Boolean

            Dim objTempJiaotanDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '��ʼ��
            getDataSet = False
            objJiaotanDataSet = Nothing
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ����
                Try
                    '�������ݼ�
                    objTempJiaotanDataSet = New Xydc.Platform.Common.Data.grswMyJiaotanData(Xydc.Platform.Common.Data.grswMyJiaotanData.enumTableType.GG_B_JIAOTAN)

                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ�м���
                    With Me.m_objSqlDataAdapter
                        '׼��SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from ����_B_��̸ a " + vbCr
                        strSQL = strSQL + " where a.������ = '" + strUserXM + "'" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " and " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.����ʱ�� desc " + vbCr

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        'ִ�в���
                        .Fill(objTempJiaotanDataSet.Tables(Xydc.Platform.Common.Data.grswMyJiaotanData.TABLE_GG_B_JIAOTAN))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempJiaotanDataSet.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.grswMyJiaotanData.SafeRelease(objTempJiaotanDataSet)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objJiaotanDataSet = objTempJiaotanDataSet
            getDataSet = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.grswMyJiaotanData.SafeRelease(objTempJiaotanDataSet)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ������ˮ�Ż�ȡ��̸��Ϣ
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strLSH               ����ˮ��
        '     objJiaotanDataSet    ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strLSH As String, _
            ByRef objJiaotanDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData) As Boolean

            Dim objTempJiaotanDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '��ʼ��
            getDataSet = False
            objJiaotanDataSet = Nothing
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim
                If strLSH Is Nothing Then strLSH = ""
                strLSH = strLSH.Trim
                If strLSH = "" Then strLSH = "0"

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ����
                Try
                    '�������ݼ�
                    objTempJiaotanDataSet = New Xydc.Platform.Common.Data.grswMyJiaotanData(Xydc.Platform.Common.Data.grswMyJiaotanData.enumTableType.GG_B_JIAOTAN)

                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ�м���
                    With Me.m_objSqlDataAdapter
                        '׼��SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from ����_B_��̸ a " + vbCr
                        strSQL = strSQL + " where a.��ˮ�� = " + strLSH + vbCr

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        'ִ�в���
                        .Fill(objTempJiaotanDataSet.Tables(Xydc.Platform.Common.Data.grswMyJiaotanData.TABLE_GG_B_JIAOTAN))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempJiaotanDataSet.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.grswMyJiaotanData.SafeRelease(objTempJiaotanDataSet)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objJiaotanDataSet = objTempJiaotanDataSet
            getDataSet = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.grswMyJiaotanData.SafeRelease(objTempJiaotanDataSet)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡstrUserXM���ͻ���յĽ�̸����(��������Ϣ,HTML��ʽ)
        ' ��ȡ������_B_��̸�������ݼ�(�Է���ʱ�併������)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strUserXM            ����ǰ����Ա����
        '     strWhere             �������ַ���
        '     objJiaotanDataSet    ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getDataSetHtml( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByVal strWhere As String, _
            ByRef objJiaotanDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            Dim objTempJiaotanDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '��ʼ��
            getDataSetHtml = False
            objJiaotanDataSet = Nothing
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ����
                Try
                    '�������ݼ�
                    objTempJiaotanDataSet = New Xydc.Platform.Common.Data.grswMyJiaotanData(Xydc.Platform.Common.Data.grswMyJiaotanData.enumTableType.GG_B_VT_JIAOTAN_FJXX)

                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ�м���
                    With Me.m_objSqlDataAdapter
                        '׼��SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.*" + vbCr
                        strSQL = strSQL + " from" + vbCr
                        strSQL = strSQL + " (" + vbCr
                        strSQL = strSQL + "   select a.*," + vbCr
                        strSQL = strSQL + "     ����     = dbo.GetJsjlFjxxHtmlByWJBS(a.Ψһ��ʶ)," + vbCr
                        strSQL = strSQL + "     �Ѷ�״̬ = case when a.��־ = 1 then @true else @false end" + vbCr
                        strSQL = strSQL + "   from ����_B_��̸ a" + vbCr
                        strSQL = strSQL + "   where (a.������ = @userxm or a.������ = @userxm)" + vbCr
                        strSQL = strSQL + " ) a" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.����ʱ�� desc " + vbCr

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@true", Xydc.Platform.Common.Utilities.PulicParameters.CharTrue)
                        objSqlCommand.Parameters.AddWithValue("@false", Xydc.Platform.Common.Utilities.PulicParameters.CharFalse)
                        objSqlCommand.Parameters.AddWithValue("@userxm", strUserXM)
                        .SelectCommand = objSqlCommand

                        'ִ�в���
                        .Fill(objTempJiaotanDataSet.Tables(Xydc.Platform.Common.Data.grswMyJiaotanData.TABLE_GG_B_VT_JIAOTAN_FJXX))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempJiaotanDataSet.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.grswMyJiaotanData.SafeRelease(objTempJiaotanDataSet)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objJiaotanDataSet = objTempJiaotanDataSet
            getDataSetHtml = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.grswMyJiaotanData.SafeRelease(objTempJiaotanDataSet)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡstrUserXM���ͻ���յĽ�̸����(��������Ϣ,Text��ʽ)
        ' ��ȡ������_B_��̸�������ݼ�(�Է���ʱ�併������)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strUserXM            ����ǰ����Ա����
        '     strWhere             �������ַ���
        '     objJiaotanDataSet    ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getDataSetText( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByVal strWhere As String, _
            ByRef objJiaotanDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            Dim objTempJiaotanDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '��ʼ��
            getDataSetText = False
            objJiaotanDataSet = Nothing
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ����
                Try
                    '�������ݼ�
                    objTempJiaotanDataSet = New Xydc.Platform.Common.Data.grswMyJiaotanData(Xydc.Platform.Common.Data.grswMyJiaotanData.enumTableType.GG_B_VT_JIAOTAN_FJXX)

                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ�м���
                    With Me.m_objSqlDataAdapter
                        '׼��SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.*" + vbCr
                        strSQL = strSQL + " from" + vbCr
                        strSQL = strSQL + " (" + vbCr
                        strSQL = strSQL + "   select a.*," + vbCr
                        strSQL = strSQL + "     ����     = dbo.GetJsjlFjxxTextByWJBS(a.Ψһ��ʶ)," + vbCr
                        strSQL = strSQL + "     �Ѷ�״̬ = case when a.��־ = 1 then @true else @false end" + vbCr
                        strSQL = strSQL + "   from ����_B_��̸ a" + vbCr
                        strSQL = strSQL + "   where (a.������ = @userxm or a.������ = @userxm)" + vbCr
                        strSQL = strSQL + " ) a" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.����ʱ�� desc " + vbCr

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@true", Xydc.Platform.Common.Utilities.PulicParameters.CharTrue)
                        objSqlCommand.Parameters.AddWithValue("@false", Xydc.Platform.Common.Utilities.PulicParameters.CharFalse)
                        objSqlCommand.Parameters.AddWithValue("@userxm", strUserXM)
                        .SelectCommand = objSqlCommand

                        'ִ�в���
                        .Fill(objTempJiaotanDataSet.Tables(Xydc.Platform.Common.Data.grswMyJiaotanData.TABLE_GG_B_VT_JIAOTAN_FJXX))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempJiaotanDataSet.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.grswMyJiaotanData.SafeRelease(objTempJiaotanDataSet)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objJiaotanDataSet = objTempJiaotanDataSet
            getDataSetText = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.grswMyJiaotanData.SafeRelease(objTempJiaotanDataSet)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��顰����_B_��̸�������ݵĺϷ���
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     objOldData           ��������
        '     objNewData           ��(����)������
        '     objenumEditType      ���༭����

        ' ����
        '     True                 ���Ϸ�
        '     False                �����Ϸ��������������
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

            doVerifyData = False

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "����δ�����µ����ݣ�"
                    GoTo errProc
                End If
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                    Case Else
                        If objOldData Is Nothing Then
                            strErrMsg = "����δ����ɵ����ݣ�"
                            GoTo errProc
                        End If
                End Select
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim

                '��ȡ��ṹ����
                strSQL = "select top 0 * from ����_B_��̸"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, strUserId, strPassword, strSQL, "����_B_��̸", objDataSet) = False Then
                    GoTo errProc
                End If

                '������ݳ���
                Dim intCount As Integer = objNewData.Count
                Dim strField As String
                Dim strValue As String
                Dim intLen As Integer
                Dim i As Integer
                For i = 0 To intCount - 1 Step 1
                    strField = objNewData.GetKey(i).Trim()
                    strValue = objNewData.Item(i).Trim()
                    Select Case strField
                        Case Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_LSH
                            '�Զ�ֵ

                        Case Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_WYBS
                            'ϵͳ�Զ�����ֵ
                            If strValue = "" Then
                                If objdacCommon.getNewGUID(strErrMsg, strUserId, strPassword, strValue) = False Then
                                    GoTo errProc
                                End If
                            End If

                        Case Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_FSSJ
                            If strValue = "" Then
                                strValue = Format(Now, "yyyy-MM-dd HH:mm:ss")
                            End If
                            If objPulicParameters.isDatetimeString(strValue) = False Then
                                strErrMsg = "����[" + strField + "]������Ч�����ڣ�"
                                GoTo errProc
                            End If
                            strValue = Format(CType(strValue, System.DateTime), "yyyy-MM-dd HH:mm:ss")

                        Case Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_FSR, _
                            Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_JSR, _
                            Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_XX
                            If strValue = "" Then
                                strErrMsg = "����[" + strField + "]����Ϊ�գ�"
                                GoTo errProc
                            End If
                            With objDataSet.Tables(0).Columns(strField)
                                intLen = objPulicParameters.getStringLength(strValue)
                                If intLen > .MaxLength Then
                                    strErrMsg = "����[" + strField + "]���Ȳ��ܳ���[" + .MaxLength.ToString() + "]��ʵ����[" + intLen.ToString() + "]��"
                                    GoTo errProc
                                End If
                            End With

                        Case Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_BZ
                            If strValue = "" Then
                                strValue = "0"
                            End If
                            If objPulicParameters.isIntegerString(strValue) = False Then
                                strErrMsg = "����[" + strField + "]������Ч�����֣�"
                                GoTo errProc
                            End If
                            If strValue <> "0" Then
                                strValue = "1"
                            End If

                        Case Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_TS
                            If strValue = "" Then
                                strValue = "0"
                            End If
                            If strValue <> "0" Then
                                strValue = "1"
                            End If

                        Case Else
                            If strValue <> "" Then
                                With objDataSet.Tables(0).Columns(strField)
                                    intLen = objPulicParameters.getStringLength(strValue)
                                    If intLen > .MaxLength Then
                                        strErrMsg = "����[" + strField + "]���Ȳ��ܳ���[" + .MaxLength.ToString() + "]��ʵ����[" + intLen.ToString() + "]��"
                                        GoTo errProc
                                    End If
                                End With
                            End If
                    End Select

                    objNewData(strField) = strValue
                Next
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            doVerifyData = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���桰����_B_��̸��������
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

            '��ʼ��
            doSaveData = False
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId.Trim = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "����δ�����µ����ݣ�"
                    GoTo errProc
                End If
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                    Case Else
                        If objOldData Is Nothing Then
                            strErrMsg = "����δ����ɵ����ݣ�"
                            GoTo errProc
                        End If
                End Select
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim

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
                    Dim strFileds As String = ""
                    Dim strValues As String = ""
                    Dim strField As String
                    Dim intCount As Integer
                    Dim i As Integer = 0
                    Select Case objenumEditType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                            '��������ֶ��б�
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_LSH
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
                            '׼��SQL
                            strSQL = ""
                            strSQL = strSQL + " insert into ����_B_��̸ (" + strFileds + ")"
                            strSQL = strSQL + " values (" + strValues + ")"
                            '׼������
                            objSqlCommand.Parameters.Clear()
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_LSH
                                    Case Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_FSSJ
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), System.DBNull.Value)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(objNewData.Item(i), System.DateTime))
                                        End If
                                    Case Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_BZ
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
                            'ִ��SQL
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()

                        Case Else
                            '��ȡԭ����ʶ��
                            Dim intOldLSH As Integer
                            intOldLSH = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_LSH), 0)
                            '��������ֶ��б�
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_LSH
                                    Case Else
                                        If strFileds = "" Then
                                            strFileds = objNewData.GetKey(i) + " = @A" + i.ToString()
                                        Else
                                            strFileds = strFileds + "," + objNewData.GetKey(i) + " = @A" + i.ToString()
                                        End If
                                End Select
                            Next
                            '׼��SQL
                            strSQL = ""
                            strSQL = strSQL + " update ����_B_��̸ set " + vbCr
                            strSQL = strSQL + "   " + strFileds + vbCr
                            strSQL = strSQL + " where ��ˮ�� = @oldlsh" + vbCr
                            '׼������
                            objSqlCommand.Parameters.Clear()
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_LSH
                                    Case Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_FSSJ
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), System.DBNull.Value)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(objNewData.Item(i), System.DateTime))
                                        End If
                                    Case Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_BZ
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
                            objSqlCommand.Parameters.AddWithValue("@oldlsh", intOldLSH)
                            'ִ��SQL
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
        ' ���桰����_B_��̸��������(��������)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objSqlTransaction    ����������
        '     objOldData           ��������
        '     objNewData           ��������
        '     objenumEditType      ���༭����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doSaveData( _
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

            '��ʼ��
            doSaveData = False
            strErrMsg = ""

            Try
                '���
                If objSqlTransaction Is Nothing Then
                    strErrMsg = "����δ������������"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "����δ�����µ����ݣ�"
                    GoTo errProc
                End If
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                    Case Else
                        If objOldData Is Nothing Then
                            strErrMsg = "����δ����ɵ����ݣ�"
                            GoTo errProc
                        End If
                End Select

                '��ȡ����
                objSqlConnection = objSqlTransaction.Connection

                '��������
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '����SQL
                    Dim strFileds As String = ""
                    Dim strValues As String = ""
                    Dim strField As String
                    Dim intCount As Integer
                    Dim i As Integer = 0
                    Select Case objenumEditType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                            '��������ֶ��б�
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_LSH
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
                            '׼��SQL
                            strSQL = ""
                            strSQL = strSQL + " insert into ����_B_��̸ (" + strFileds + ")"
                            strSQL = strSQL + " values (" + strValues + ")"
                            '׼������
                            objSqlCommand.Parameters.Clear()
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_LSH
                                    Case Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_FSSJ
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), System.DBNull.Value)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(objNewData.Item(i), System.DateTime))
                                        End If
                                    Case Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_BZ
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
                            'ִ��SQL
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()

                        Case Else
                            '��ȡԭ����ʶ��
                            Dim intOldLSH As Integer
                            intOldLSH = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_LSH), 0)
                            '��������ֶ��б�
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_LSH
                                    Case Else
                                        If strFileds = "" Then
                                            strFileds = objNewData.GetKey(i) + " = @A" + i.ToString()
                                        Else
                                            strFileds = strFileds + "," + objNewData.GetKey(i) + " = @A" + i.ToString()
                                        End If
                                End Select
                            Next
                            '׼��SQL
                            strSQL = ""
                            strSQL = strSQL + " update ����_B_��̸ set " + vbCr
                            strSQL = strSQL + "   " + strFileds + vbCr
                            strSQL = strSQL + " where ��ˮ�� = @oldlsh" + vbCr
                            '׼������
                            objSqlCommand.Parameters.Clear()
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_LSH
                                    Case Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_FSSJ
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), System.DBNull.Value)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(objNewData.Item(i), System.DateTime))
                                        End If
                                    Case Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_BZ
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
                            objSqlCommand.Parameters.AddWithValue("@oldlsh", intOldLSH)
                            'ִ��SQL
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

            '����
            doSaveData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ɾ��������_B_��̸��������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     objOldData           ��Ҫɾ��������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doDeleteData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            '��ʼ��
            doDeleteData = False
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId.Trim = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If
                If objOldData Is Nothing Then
                    strErrMsg = "����δ����Ҫɾ�������ݣ�"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim

                '��ȡ��Ψһ��ʶ��
                Dim strWYBS As String
                strWYBS = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_WYBS), "")

                'ɾ������
                If Me.doDeleteData(strErrMsg, strUserId, strPassword, strWYBS) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            '����
            doDeleteData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ɾ��ָ��strWJBS�ġ�����_B_��̸��������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strWJBS              ��Ψһ��ʶ
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doDeleteData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWJBS As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseFTP As New Xydc.Platform.Common.Utilities.BaseFTP
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objdacXitongpeizhi As New Xydc.Platform.DataAccess.dacXitongpeizhi
            Dim objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            Dim objFJDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData

            Dim strSQL As String

            '��ʼ��
            doDeleteData = False
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId.Trim = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim
                If strWJBS Is Nothing Then strWJBS = ""
                strWJBS = strWJBS.Trim
                If strWJBS = "" Then strWJBS = ""

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡFTP���Ӳ���
                If objdacXitongpeizhi.getFtpServerParam(strErrMsg, objSqlConnection, objFTPProperty) = False Then
                    GoTo errProc
                End If

                '��ȡ��������
                If Me.getFujianDataSet(strErrMsg, strUserId, strPassword, strWJBS, objFJDataSet) = False Then
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
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ɾ��������_B_��̸_��������Ϣ
                    strSQL = ""
                    strSQL = strSQL + " delete from ����_B_��̸_���� " + vbCr
                    strSQL = strSQL + " where �ļ���ʶ = @wjbs" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    'ɾ��������_B_��̸��
                    strSQL = ""
                    strSQL = strSQL + " delete from ����_B_��̸ " + vbCr
                    strSQL = strSQL + " where Ψһ��ʶ = @wybs" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@wybs", strWJBS)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    'ɾ��������_B_��̸_��������Ӧ�ļ�����
                    Dim strFilePath As String
                    Dim intCount As Integer
                    Dim strUrl As String
                    Dim i As Integer
                    With objFJDataSet.Tables(Xydc.Platform.Common.Data.grswMyJiaotanData.TABLE_GG_B_JIAOTAN_FUJIAN)
                        intCount = .Rows.Count
                        For i = 0 To intCount - 1 Step 1
                            strFilePath = objPulicParameters.getObjectValue(.Rows(i).Item("λ��"), "")
                            If strFilePath <> "" Then
                                With objFTPProperty
                                    strUrl = .getUrl(strFilePath)
                                    If objBaseFTP.doDeleteFile(strErrMsg, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword) = False Then
                                        '���Բ��ɹ����γ������ļ���
                                    End If
                                End With
                            End If
                        Next
                    End With
                    objFJDataSet.Dispose()
                    objFJDataSet = Nothing

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
            Xydc.Platform.DataAccess.dacXitongpeizhi.SafeRelease(objdacXitongpeizhi)
            Xydc.Platform.Common.Utilities.FTPProperty.SafeRelease(objFTPProperty)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            doDeleteData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacXitongpeizhi.SafeRelease(objdacXitongpeizhi)
            Xydc.Platform.Common.Utilities.FTPProperty.SafeRelease(objFTPProperty)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ[������=strUserXM]��û���Ķ��Ľ�̸����
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strUserXM            ����ǰ����Ա����
        '     objJiaotanDataSet    ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getDataSetWYD( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByRef objJiaotanDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '��ʼ��
            getDataSetWYD = False
            objJiaotanDataSet = Nothing
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ����
                Try
                    '�������ݼ�
                    objDataSet = New Xydc.Platform.Common.Data.grswMyJiaotanData(Xydc.Platform.Common.Data.grswMyJiaotanData.enumTableType.GG_B_JIAOTAN)

                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ�м���
                    With Me.m_objSqlDataAdapter
                        '׼��SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from ����_B_��̸ a " + vbCr
                        strSQL = strSQL + " where (������ = '" + strUserXM + "' and isnull(��־,0) = 0) " + vbCr   '�͸���+δ�Ķ�
                        strSQL = strSQL + " order by a.����ʱ��" + vbCr

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        'ִ�в���
                        .Fill(objDataSet.Tables(Xydc.Platform.Common.Data.grswMyJiaotanData.TABLE_GG_B_JIAOTAN))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objDataSet.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.grswMyJiaotanData.SafeRelease(objDataSet)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objJiaotanDataSet = objDataSet
            getDataSetWYD = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.grswMyJiaotanData.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡstrUserXM��ָ��֮��֮���ͻ���յĽ�̸����
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strUserXM            ����ǰ����Ա����
        '     strZDSJ              ��ָ��ʱ��
        '     objJiaotanDataSet    ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getDataSetAfterTime( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByVal strZDSJ As String, _
            ByRef objJiaotanDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '��ʼ��
            getDataSetAfterTime = False
            objJiaotanDataSet = Nothing
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If
                If strZDSJ Is Nothing Then strZDSJ = ""
                strZDSJ = strZDSJ.Trim
                If strZDSJ = "" Then
                    strErrMsg = "����δָ��Ҫʱ�䣡"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ����
                Try
                    '�������ݼ�
                    objDataSet = New Xydc.Platform.Common.Data.grswMyJiaotanData(Xydc.Platform.Common.Data.grswMyJiaotanData.enumTableType.GG_B_JIAOTAN)

                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ�м���
                    With Me.m_objSqlDataAdapter
                        '׼��SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from ����_B_��̸ a " + vbCr
                        strSQL = strSQL + " where (������ = @userxm or ������ = @userxm)" + vbCr  '�͸��һ��ҷ���
                        strSQL = strSQL + " and   ����ʱ�� >= @zdsj" + vbCr                       'ָ��ʱ�������
                        strSQL = strSQL + " order by a.����ʱ��" + vbCr

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@userxm", strUserXM)
                        objSqlCommand.Parameters.AddWithValue("@zdsj", strZDSJ)
                        .SelectCommand = objSqlCommand

                        'ִ�в���
                        .Fill(objDataSet.Tables(Xydc.Platform.Common.Data.grswMyJiaotanData.TABLE_GG_B_JIAOTAN))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objDataSet.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.grswMyJiaotanData.SafeRelease(objDataSet)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objJiaotanDataSet = objDataSet
            getDataSetAfterTime = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.grswMyJiaotanData.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �������Ѿ��Ķ�strLSH��Ϣ
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strUserXM            ����ǰ����Ա����
        '     strLSH               ����ˮ��
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doSetReadFlag( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByVal strLSH As String) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '��ʼ��
            doSetReadFlag = False
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If
                If strLSH Is Nothing Then strLSH = ""
                strLSH = strLSH.Trim
                If strLSH = "" Then strLSH = "0"
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ����
                Try
                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '��ʼ����
                    objSqlTransaction = objSqlConnection.BeginTransaction
                    objSqlCommand.Transaction = objSqlTransaction

                    Try
                        '׼��SQL
                        strSQL = ""
                        strSQL = strSQL + " update ����_B_��̸ set " + vbCr
                        strSQL = strSQL + "   ��־ = @bz" + vbCr
                        strSQL = strSQL + " where ��ˮ�� = @lsh" + vbCr

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@bz", 1)
                        objSqlCommand.Parameters.AddWithValue("@lsh", CType(strLSH, Integer))

                        'ִ��
                        objSqlCommand.ExecuteNonQuery()

                    Catch ex As Exception
                        objSqlTransaction.Rollback()
                        GoTo errProc
                    End Try

                    '�ύ����
                    objSqlTransaction.Commit()

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
            doSetReadFlag = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���ݸ����ļ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objFTPProperty       ��FTP����������
        '     objFJData            ����������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doBackupFiles_FJ( _
            ByRef strErrMsg As String, _
            ByVal objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty, _
            ByVal objFJData As Xydc.Platform.Common.Data.grswMyJiaotanData) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.grswMyJiaotanData.TABLE_GG_B_JIAOTAN_FUJIAN
            Dim strBakExt As String = Xydc.Platform.Common.Utilities.PulicParameters.BACKUPFILEEXT

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objBaseFTP As New Xydc.Platform.Common.Utilities.BaseFTP

            doBackupFiles_FJ = False
            strErrMsg = ""

            Try
                If objFTPProperty Is Nothing Then
                    strErrMsg = "����δ����FTP������������"
                    GoTo errProc
                End If
                If objFJData Is Nothing Then
                    Exit Try
                End If
                If objFJData.Tables(strTable) Is Nothing Then
                    Exit Try
                End If

                '����ԭ�ļ�
                Dim blnExisted As Boolean
                Dim strFileName As String
                Dim strOldFile As String
                Dim strUrl As String
                Dim intCount As Integer
                Dim i As Integer
                With objFJData.Tables(strTable)
                    intCount = .DefaultView.Count
                    For i = intCount - 1 To 0 Step -1
                        strOldFile = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_FUJIAN_WJWZ), "")
                        If strOldFile <> "" Then
                            With objFTPProperty
                                strUrl = .getUrl(strOldFile)
                                If objBaseFTP.isFileExisted(strErrMsg, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword, blnExisted) = False Then
                                    '���Բ��ɹ����������ļ�������
                                Else
                                    If blnExisted = True Then
                                        strFileName = objBaseLocalFile.getFileName(strOldFile) + strBakExt
                                        If objBaseFTP.doRenameFile(strErrMsg, strUrl, strFileName, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword, True) = False Then
                                            GoTo errProc
                                        End If
                                    End If
                                End If
                            End With
                        End If
                    Next
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)

            doBackupFiles_FJ = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ɾ�������ı����ļ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objFTPProperty       ��FTP����������
        '     objFJData            ����������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doDeleteBackupFiles_FJ( _
            ByRef strErrMsg As String, _
            ByVal objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty, _
            ByVal objFJData As Xydc.Platform.Common.Data.grswMyJiaotanData) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.grswMyJiaotanData.TABLE_GG_B_JIAOTAN_FUJIAN
            Dim strBakExt As String = Xydc.Platform.Common.Utilities.PulicParameters.BACKUPFILEEXT

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseFTP As New Xydc.Platform.Common.Utilities.BaseFTP

            doDeleteBackupFiles_FJ = False
            strErrMsg = ""

            Try
                If objFTPProperty Is Nothing Then
                    strErrMsg = "����δ����FTP������������"
                    GoTo errProc
                End If
                If objFJData Is Nothing Then
                    Exit Try
                End If
                If objFJData.Tables(strTable) Is Nothing Then
                    Exit Try
                End If

                Dim strOldFile As String
                Dim intCount As Integer
                Dim strUrl As String
                Dim i As Integer
                With objFJData.Tables(strTable)
                    intCount = .DefaultView.Count
                    For i = intCount - 1 To 0 Step -1
                        strOldFile = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_FUJIAN_WJWZ), "")
                        If strOldFile <> "" Then
                            With objFTPProperty
                                strUrl = .getUrl(strOldFile + strBakExt)
                                If objBaseFTP.doDeleteFile(strErrMsg, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword) = False Then
                                    '���Բ��ɹ�,�γ���������
                                End If
                            End With
                        End If
                    Next
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)

            doDeleteBackupFiles_FJ = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �ӱ��ݻ��������ļ��лָ�ԭ�����ļ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strWJBS              ���ļ���ʶ
        '     intWJND              �����ļ���ŵ����
        '     objFTPProperty       ��FTP����������
        '     objNewData           ���¸�������
        '     objOldData           ��ԭ��������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doRestoreFiles_FJ( _
            ByRef strErrMsg As String, _
            ByVal strWJBS As String, _
            ByVal intWJND As Integer, _
            ByVal objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty, _
            ByVal objNewData As Xydc.Platform.Common.Data.grswMyJiaotanData, _
            ByVal objOldData As Xydc.Platform.Common.Data.grswMyJiaotanData) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.grswMyJiaotanData.TABLE_GG_B_JIAOTAN_FUJIAN
            Dim strBakExt As String = Xydc.Platform.Common.Utilities.PulicParameters.BACKUPFILEEXT

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objBaseFTP As New Xydc.Platform.Common.Utilities.BaseFTP

            doRestoreFiles_FJ = False
            strErrMsg = ""

            Try
                If objFTPProperty Is Nothing Then
                    strErrMsg = "����δ����FTP������������"
                    GoTo errProc
                End If
                If objOldData Is Nothing Then
                    Exit Try
                End If
                If objOldData.Tables(strTable) Is Nothing Then
                    Exit Try
                End If

                '���ȴӱ����ļ��ع�
                Dim strBasePath As String = Xydc.Platform.Common.Data.grswMyJiaotanData.FILEDIR_FJ
                Dim blnExisted As Boolean
                Dim strNewWJWZ As String
                Dim strOldWJWZ As String
                Dim strNewFile As String
                Dim strOldFile As String
                Dim strToUrl As String
                Dim strUrl As String
                Dim blnDo As Boolean
                Dim intCountA As Integer
                Dim intCount As Integer
                Dim i As Integer
                Dim j As Integer
                With objOldData.Tables(strTable)
                    intCount = .DefaultView.Count
                    For i = intCount - 1 To 0 Step -1
                        strOldFile = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_FUJIAN_WJWZ), "")
                        strOldWJWZ = strOldFile.ToUpper
                        If strOldFile <> "" Then
                            With objFTPProperty
                                '�ȴӱ����лָ�
                                strUrl = .getUrl(strOldFile + strBakExt)
                                If objBaseFTP.isFileExisted(strErrMsg, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword, blnExisted) = False Then
                                    blnExisted = False
                                End If
                                If blnExisted = True Then
                                    '�����ļ����ڣ���ӱ����ļ��о����ָܻ�
                                    strToUrl = .getUrl(strOldFile)
                                    objBaseFTP.doRenameFile(strErrMsg, strUrl, strToUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword)
                                Else
                                    '�����ļ������ڣ����鱸���ļ��Ƿ��Ѹ���Ϊ��Ӧ�����ļ���
                                    If Not (objNewData Is Nothing) Then
                                        blnDo = False
                                        With objNewData.Tables(strTable)
                                            intCountA = .DefaultView.Count
                                            For j = 0 To intCountA - 1 Step 1
                                                strNewWJWZ = objPulicParameters.getObjectValue(.DefaultView.Item(j).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_WJWZ), "")
                                                If strOldWJWZ = strNewWJWZ.ToUpper Then
                                                    '��ȡ��Ӧ�����ļ�
                                                    If Me.getFTPFileName_FJ(strErrMsg, strOldFile, intWJND, strWJBS, j + 1, strBasePath, strNewFile) = False Then
                                                        blnDo = False
                                                    Else
                                                        blnDo = True
                                                    End If
                                                    Exit For
                                                End If
                                            Next
                                        End With
                                        If blnDo = True Then
                                            strUrl = .getUrl(strNewFile)
                                            If objBaseFTP.isFileExisted(strErrMsg, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword, blnExisted) = False Then
                                                blnExisted = False
                                            End If
                                            If blnExisted = True Then
                                                '�Ѿ����ļ����ڣ���ִ�д����ļ��о����ָܻ�
                                                strToUrl = .getUrl(strOldFile)
                                                objBaseFTP.doRenameFile(strErrMsg, strUrl, strToUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword)
                                            End If
                                        End If
                                    End If
                                End If
                            End With
                        End If
                    Next
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)

            doRestoreFiles_FJ = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���ݱ����ļ���ȡFTP�������ļ�������
        ' �ļ����������������ļ���ʶ-FJ-���
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strLocalFile         �������ļ���
        '     intWJND              ���ļ����
        '     strWJBS              ���ļ���ʶ
        '     intXH                �����
        '     strBasePath          ������Ŀ¼����Ŀ¼
        '     strRemoteFile        ������FTP�������ļ�·��
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getFTPFileName_FJ( _
            ByRef strErrMsg As String, _
            ByVal strLocalFile As String, _
            ByVal intWJND As Integer, _
            ByVal strWJBS As String, _
            ByVal intXH As Integer, _
            ByVal strBasePath As String, _
            ByRef strRemoteFile As String) As Boolean

            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile

            getFTPFileName_FJ = False
            strRemoteFile = ""

            Try
                '���
                If strLocalFile Is Nothing Then strLocalFile = ""
                strLocalFile = strLocalFile.Trim()
                If strLocalFile = "" Then
                    Exit Try
                End If
                If strWJBS Is Nothing Then strWJBS = ""
                strWJBS = strWJBS.Trim()
                If strWJBS = "" Then
                    Exit Try
                End If
                If strBasePath Is Nothing Then strBasePath = ""
                strBasePath = strBasePath.Trim

                '��ȡ�ļ���
                Dim strFileName As String = ""
                Dim strFileExt As String = ""
                strFileExt = objBaseLocalFile.getExtension(strLocalFile)

                '�ļ����������������ļ���ʶ-FJ-���
                strFileName = strWJBS + "-FJ-" + intXH.ToString() + strFileExt
                strFileName = objBaseLocalFile.doMakePath(intWJND.ToString(), strFileName)

                '����Ŀ¼+�ļ�
                strFileName = objBaseLocalFile.doMakePath(strBasePath, strFileName)

                '����
                strRemoteFile = strFileName

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)

            getFTPFileName_FJ = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���渽������
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strUserId              ���û���ʶ
        '     strPassword            ���û�����
        '     strWJBS                ���ļ���ʶ
        '     intWJND                �����ļ���ŵ����
        '     objSqlTransaction      ����������
        '     objFTPProperty         ��FTP����������
        '     objNewData             ����¼��ֵ(���ر�������ֵ)
        '     objOldData             ����¼��ֵ
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Function doSaveFujian( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWJBS As String, _
            ByVal intWJND As Integer, _
            ByVal objSqlTransaction As System.Data.SqlClient.SqlTransaction, _
            ByVal objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty, _
            ByRef objNewData As Xydc.Platform.Common.Data.grswMyJiaotanData, _
            ByVal objOldData As Xydc.Platform.Common.Data.grswMyJiaotanData) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            Dim strTable As String = Xydc.Platform.Common.Data.grswMyJiaotanData.TABLE_GG_B_JIAOTAN_FUJIAN
            Dim strBakExt As String = Xydc.Platform.Common.Utilities.PulicParameters.BACKUPFILEEXT
            Dim blnNewTrans As Boolean = False
            Dim strSQL As String

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objBaseFTP As New Xydc.Platform.Common.Utilities.BaseFTP
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '��ʼ��
            doSaveFujian = False
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "����δ���������û���"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "����δ�����µ����ݣ�"
                    GoTo errProc
                End If
                If objFTPProperty Is Nothing Then
                    strErrMsg = "����δ����FTP������������"
                    GoTo errProc
                End If
                If strWJBS Is Nothing Then strWJBS = ""
                strWJBS = strWJBS.Trim
                If strWJBS = "" Then
                    Exit Try
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim

                '��ȡ������Ϣ
                If objSqlTransaction Is Nothing Then
                    If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                        GoTo errProc
                    End If
                Else
                    objSqlConnection = objSqlTransaction.Connection
                End If

                '��ʼ����
                If objSqlTransaction Is Nothing Then
                    blnNewTrans = True
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Else
                    blnNewTrans = False
                End If

                '��������
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ɾ��������_B_��̸_����������
                    strSQL = ""
                    strSQL = strSQL + " delete from ����_B_��̸_���� " + vbCr
                    strSQL = strSQL + " where �ļ���ʶ = @wjbs" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    Try
                        '��Դ�ļ���ͬĿ¼�н��ļ�����
                        If Me.doBackupFiles_FJ(strErrMsg, objFTPProperty, objOldData) = False Then
                            GoTo rollDatabaseAndFile
                        End If

                        '����������
                        Dim strBasePath As String = Xydc.Platform.Common.Data.grswMyJiaotanData.FILEDIR_FJ
                        Dim blnExisted As Boolean
                        Dim strOldFile As String
                        Dim strLocFile As String
                        Dim strNewFile As String
                        Dim strToUrl As String
                        Dim strUrl As String
                        Dim intCount As Integer
                        Dim i As Integer
                        With objNewData.Tables(strTable)
                            intCount = .DefaultView.Count
                            For i = 0 To intCount - 1 Step 1
                                '��ȡԭFTP·�����±����ļ�·��
                                strOldFile = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_FUJIAN_WJWZ), "")
                                strLocFile = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_FUJIAN_BDWJ), "")
                                strNewFile = ""
                                '�ϴ��ļ�
                                If strLocFile <> "" Then
                                    '�ļ�����?
                                    If objBaseLocalFile.doFileExisted(strErrMsg, strLocFile, blnExisted) = False Then
                                        GoTo rollDatabaseAndFile
                                    End If
                                    If blnExisted = True Then
                                        '��ȡFTP�ļ�·��
                                        If Me.getFTPFileName_FJ(strErrMsg, strLocFile, intWJND, strWJBS, i + 1, strBasePath, strNewFile) = False Then
                                            GoTo rollDatabaseAndFile
                                        End If
                                        '�б����ļ�������Ҫ����
                                        With objFTPProperty
                                            strUrl = .getUrl(strNewFile)
                                            If objBaseFTP.doPutFile(strErrMsg, strLocFile, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword) = False Then
                                                GoTo rollDatabaseAndFile
                                            End If
                                        End With
                                    Else
                                        strErrMsg = "����[" + strLocFile + "]�����ڣ�"
                                        GoTo rollDatabaseAndFile
                                    End If
                                Else
                                    If strOldFile <> "" Then
                                        '
                                        'δ��FTP����������
                                        '
                                        '�ӱ����ļ��ָ�����ǰ�е��ļ�
                                        With objFTPProperty
                                            strUrl = .getUrl(strOldFile + strBakExt)
                                            If objBaseFTP.isFileExisted(strErrMsg, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword, blnExisted) = False Then
                                                '���Բ��ɹ�
                                            Else
                                                If blnExisted = True Then
                                                    '��ȡFTP�ļ�·��
                                                    If Me.getFTPFileName_FJ(strErrMsg, strOldFile, intWJND, strWJBS, i + 1, strBasePath, strNewFile) = False Then
                                                        GoTo rollDatabaseAndFile
                                                    End If
                                                    strToUrl = .getUrl(strNewFile)
                                                    '�����ļ���
                                                    If objBaseFTP.doRenameFile(strErrMsg, strUrl, strToUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword) = False Then
                                                        GoTo rollDatabaseAndFile
                                                    End If
                                                End If
                                            End If
                                        End With
                                    Else
                                        'û�е����ļ�
                                    End If
                                End If

                                'д����
                                strSQL = ""
                                strSQL = strSQL + " insert into ����_B_��̸_���� (" + vbCr
                                strSQL = strSQL + "   �ļ���ʶ, ���, ˵��, ҳ��, λ��" + vbCr
                                strSQL = strSQL + " ) values (" + vbCr
                                strSQL = strSQL + "   @wjbs, @wjxh, @wjsm, @wjys, @wjwz" + vbCr
                                strSQL = strSQL + " )" + vbCr
                                objSqlCommand.Parameters.Clear()
                                objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                                objSqlCommand.Parameters.AddWithValue("@wjxh", (i + 1))
                                objSqlCommand.Parameters.AddWithValue("@wjsm", objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_FUJIAN_WJSM), ""))
                                objSqlCommand.Parameters.AddWithValue("@wjys", objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_FUJIAN_WJYS), 0))
                                objSqlCommand.Parameters.AddWithValue("@wjwz", strNewFile)
                                objSqlCommand.CommandText = strSQL
                                objSqlCommand.ExecuteNonQuery()
                            Next
                        End With

                        'ɾ�����б����ļ�
                        If blnNewTrans = True Then
                            If Me.doDeleteBackupFiles_FJ(strErrMsg, objFTPProperty, objOldData) = False Then
                                '���Բ��ɹ����γ������ļ���
                            End If
                        End If

                    Catch ex As Exception
                        strErrMsg = ex.Message
                        GoTo rollDatabaseAndFile
                    End Try

                Catch ex As Exception
                    GoTo rollDatabase
                End Try

                '�ύ����
                If blnNewTrans = True Then
                    objSqlTransaction.Commit()
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            If blnNewTrans = True Then
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            End If

            '����
            doSaveFujian = True
            Exit Function

rollDatabaseAndFile:
            If blnNewTrans = True Then
                objSqlTransaction.Rollback()
                If Me.doRestoreFiles_FJ(strSQL, strWJBS, intWJND, objFTPProperty, objNewData, objOldData) = False Then
                    '�޷��ָ��ɹ��������ˣ�
                End If
            End If
            GoTo errProc

rollDatabase:
            If blnNewTrans = True Then
                objSqlTransaction.Rollback()
            End If
            GoTo errProc

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            If blnNewTrans = True Then
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            End If
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �����ļ���ʶ��ȡ��̸�ĸ�����Ϣ
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strWJBS              ���ļ���ʶ
        '     objJiaotanDataSet    ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getFujianDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWJBS As String, _
            ByRef objJiaotanDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objTempJiaotanDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '��ʼ��
            getFujianDataSet = False
            objJiaotanDataSet = Nothing
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim
                If strWJBS Is Nothing Then strWJBS = ""
                strWJBS = strWJBS.Trim

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ����
                Try
                    '�������ݼ�
                    objTempJiaotanDataSet = New Xydc.Platform.Common.Data.grswMyJiaotanData(Xydc.Platform.Common.Data.grswMyJiaotanData.enumTableType.GG_B_JIAOTAN_FUJIAN)

                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ�м���
                    With Me.m_objSqlDataAdapter
                        '׼��SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from ����_B_��̸_���� a " + vbCr
                        strSQL = strSQL + " where a.�ļ���ʶ = '" + strWJBS + "'" + vbCr
                        strSQL = strSQL + " order by a.���" + vbCr
                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        'ִ�в���
                        .Fill(objTempJiaotanDataSet.Tables(Xydc.Platform.Common.Data.grswMyJiaotanData.TABLE_GG_B_JIAOTAN_FUJIAN))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempJiaotanDataSet.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.grswMyJiaotanData.SafeRelease(objTempJiaotanDataSet)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objJiaotanDataSet = objTempJiaotanDataSet
            getFujianDataSet = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.grswMyJiaotanData.SafeRelease(objTempJiaotanDataSet)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �����ļ���ʶ����Ż�ȡ��̸�ĸ�����Ϣ
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strWJBS              ���ļ���ʶ
        '     strWJXH              �����
        '     objJiaotanDataSet    ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getFujianDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWJBS As String, _
            ByVal strWJXH As String, _
            ByRef objJiaotanDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objTempJiaotanDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '��ʼ��
            getFujianDataSet = False
            objJiaotanDataSet = Nothing
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim
                If strWJBS Is Nothing Then strWJBS = ""
                strWJBS = strWJBS.Trim
                If strWJXH Is Nothing Then strWJXH = ""
                strWJXH = strWJXH.Trim
                If strWJXH = "" Then strWJXH = "0"

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ����
                Try
                    '�������ݼ�
                    objTempJiaotanDataSet = New Xydc.Platform.Common.Data.grswMyJiaotanData(Xydc.Platform.Common.Data.grswMyJiaotanData.enumTableType.GG_B_JIAOTAN_FUJIAN)

                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ�м���
                    With Me.m_objSqlDataAdapter
                        '׼��SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from ����_B_��̸_���� a " + vbCr
                        strSQL = strSQL + " where a.�ļ���ʶ = '" + strWJBS + "'" + vbCr
                        strSQL = strSQL + " and   a.���     =  " + strWJXH + " " + vbCr
                        strSQL = strSQL + " order by a.���" + vbCr
                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        'ִ�в���
                        .Fill(objTempJiaotanDataSet.Tables(Xydc.Platform.Common.Data.grswMyJiaotanData.TABLE_GG_B_JIAOTAN_FUJIAN))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempJiaotanDataSet.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.grswMyJiaotanData.SafeRelease(objTempJiaotanDataSet)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objJiaotanDataSet = objTempJiaotanDataSet
            getFujianDataSet = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.grswMyJiaotanData.SafeRelease(objTempJiaotanDataSet)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���潻̸���ݼ�¼(�����������)
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strUserId              ���û���ʶ
        '     strPassword            ���û�����
        '     objNewData             ����¼��ֵ(���ر�������ֵ)
        '     objOldData             ����¼��ֵ
        '     objenumEditType        ���༭����
        '     objNewFJData           ��Ҫ����ĸ�������
        '     objConnectionProperty  ��FTP���Ӳ���
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Function doSaveData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType, _
            ByVal objNewFJData As Xydc.Platform.Common.Data.grswMyJiaotanData, _
            ByVal objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection

            Dim objOldFJData As Xydc.Platform.Common.Data.grswMyJiaotanData
            Dim intWJND As Integer = Year(Now)
            Dim strWJBS As String
            Dim strSQL As String

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            doSaveData = False

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "����δ���������û���"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "����û��ָ��Ҫ��������ݣ�"
                    GoTo errProc
                End If
                If objFTPProperty Is Nothing Then
                    strErrMsg = "����û��ָ��FTP���Ӳ�����"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim

                '��鷢������¼
                If Me.doVerifyData(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType) = False Then
                    GoTo errProc
                End If

                '��ȡ��������
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡԭ��������
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew, _
                        Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eCpyNew
                        objOldFJData = Nothing
                    Case Else
                        strWJBS = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_WYBS), "")
                        If Me.getFujianDataSet(strErrMsg, strUserId, strPassword, strWJBS, objOldFJData) = False Then
                            GoTo errProc
                        End If
                End Select

                '��ʼ����
                objSqlTransaction = objSqlConnection.BeginTransaction

                'ִ������
                Try
                    '���淢������¼
                    If Me.doSaveData(strErrMsg, objSqlTransaction, objOldData, objNewData, objenumEditType) = False Then
                        GoTo rollDatabase
                    End If

                    '�������ļ���ʶ
                    strWJBS = objNewData(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_WYBS)

                    '���渽���ļ�
                    If Me.doSaveFujian(strErrMsg, strUserId, strPassword, strWJBS, intWJND, objSqlTransaction, objFTPProperty, objNewFJData, objOldFJData) = False Then
                        GoTo rollDatabaseAndFJFile
                    End If

                    '��������ļ�
                    If Me.doDeleteBackupFiles_FJ(strErrMsg, objFTPProperty, objOldFJData) = False Then
                        '���Բ��ɹ����γ������ļ�
                    End If

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo rollDatabase
                End Try

                '�ύ����
                objSqlTransaction.Commit()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Data.grswMyJiaotanData.SafeRelease(objOldFJData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            doSaveData = True
            Exit Function

rollDatabaseAndFJFile:
            objSqlTransaction.Rollback()
            If Me.doRestoreFiles_FJ(strSQL, strWJBS, intWJND, objFTPProperty, objNewFJData, objOldFJData) = False Then
                '�Ѿ������ˣ�
            End If
            GoTo errProc

rollDatabase:
            objSqlTransaction.Rollback()
            GoTo errProc

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Data.grswMyJiaotanData.SafeRelease(objOldFJData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

    End Class

End Namespace
