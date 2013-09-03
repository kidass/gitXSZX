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
    ' ����    ��dacXitongpeizhi
    '
    ' ����������
    '     �ṩ��ϵͳ������ر�������_B_ϵͳ�����������ݵ�
    '     ���ӡ��޸ġ�ɾ���������Ȳ���
    '----------------------------------------------------------------

    Public Class dacXitongpeizhi
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
        ' ��ȡ������_B_ϵͳ��������SQL���(�Ա�ʶ��������)
        ' ����
        '                          ��SQL
        '----------------------------------------------------------------
        Public Function getXitongcanshuSQL() As String
            getXitongcanshuSQL = "select * from ����_B_ϵͳ���� order by ��ʶ"
        End Function

        '----------------------------------------------------------------
        ' ��ȡ������_B_ϵͳ�����������ݼ�(�Ա�ʶ��������)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strWhere             �������ַ���
        '     objXitongcanshuData  ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
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

            '��ʼ��
            getXitongcanshuData = False
            objXitongcanshuData = Nothing
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strWhere.Length > 0 Then strWhere = strWhere.Trim()

                '���
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
                    objTempXitongcanshuData = New Xydc.Platform.Common.Data.XitongcanshuData(Xydc.Platform.Common.Data.XitongcanshuData.enumTableType.GL_B_XITONGCANSHU)

                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ�м���
                    With Me.m_objSqlDataAdapter
                        '׼��SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from ����_B_ϵͳ���� a " + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.��ʶ " + vbCr

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        'ִ�в���
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

            '����
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
        ' ��ȡ������_B_ϵͳ�����������ݼ�(�Ա�ʶ��������)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objSqlConnection     ��ָ������
        '     strWhere             �������ַ���
        '     objXitongcanshuData  ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getXitongcanshuData( _
            ByRef strErrMsg As String, _
            ByVal objSqlConnection As System.Data.SqlClient.SqlConnection, _
            ByVal strWhere As String, _
            ByRef objXitongcanshuData As Xydc.Platform.Common.Data.XitongcanshuData) As Boolean

            Dim objTempXitongcanshuData As Xydc.Platform.Common.Data.XitongcanshuData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '��ʼ��
            getXitongcanshuData = False
            objXitongcanshuData = Nothing
            strErrMsg = ""

            Try
                If objSqlConnection Is Nothing Then
                    strErrMsg = "����[getXitongcanshuData]δָ�����ӣ�"
                    GoTo errProc
                End If
                If strWhere.Length > 0 Then strWhere = strWhere.Trim()

                '��ȡ����
                Dim strSQL As String
                Try
                    '�������ݼ�
                    objTempXitongcanshuData = New Xydc.Platform.Common.Data.XitongcanshuData(Xydc.Platform.Common.Data.XitongcanshuData.enumTableType.GL_B_XITONGCANSHU)

                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ�м���
                    With Me.m_objSqlDataAdapter
                        '׼��SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from ����_B_ϵͳ���� a " + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.��ʶ " + vbCr

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        'ִ�в���
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

            '����
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
        ' ��顰����_B_ϵͳ�����������ݵĺϷ���
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     objOldData           ��������
        '     objNewData           ��������(У����ɺ��������)
        '     objenumEditType      ���༭����

        ' ����
        '     True                 ���Ϸ�
        '     False                �����Ϸ��������������
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
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                    Case Else
                        If objOldData Is Nothing Then
                            strErrMsg = "����δ����ɵ����ݣ�"
                            GoTo errProc
                        End If
                End Select

                '��ȡ��ṹ����
                strSQL = "select top 0 * from ����_B_ϵͳ����"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, strUserId, strPassword, strSQL, "����_B_ϵͳ����", objDataSet) = False Then
                    GoTo errProc
                End If

                '������ݳ���
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
                            '�����

                        Case Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_SFJM
                            '���ּ��
                            strValue = objPulicParameters.getObjectValue(objDictionaryEntry.Value, "")
                            If strValue = "" Then strValue = "0"
                            If objPulicParameters.isIntegerString(strValue) = False Then
                                strErrMsg = "����[" + strField + "]���������֣�"
                                GoTo errProc
                            End If
                            objDictionaryEntry.Value = strValue

                        Case Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_ZFTPDK, _
                            Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_CFTPDK
                            '���ּ��
                            strValue = objPulicParameters.getObjectValue(objDictionaryEntry.Value, "")
                            If strValue = "" Then strValue = "21"
                            If objPulicParameters.isIntegerString(strValue) = False Then
                                strErrMsg = "����[" + strField + "]���������֣�"
                                GoTo errProc
                            End If
                            With objDataSet.Tables(0).Columns(strField)
                                intLen = objPulicParameters.getStringLength(strValue)
                                If intLen > .MaxLength Then
                                    strErrMsg = "����[" + strField + "]���Ȳ��ܳ���[" + .MaxLength.ToString() + "]��ʵ����[" + intLen.ToString() + "]��"
                                    GoTo errProc
                                End If
                            End With
                            objDictionaryEntry.Value = strValue

                        Case Else
                            '�ַ������
                            strValue = objPulicParameters.getObjectValue(objDictionaryEntry.Value, "")
                            If strValue <> "" Then
                                With objDataSet.Tables(0).Columns(strField)
                                    intLen = objPulicParameters.getStringLength(strValue)
                                    If intLen > .MaxLength Then
                                        strErrMsg = "����[" + strField + "]���Ȳ��ܳ���[" + .MaxLength.ToString() + "]��ʵ����[" + intLen.ToString() + "]��"
                                        GoTo errProc
                                    End If
                                End With
                            Else
                                Select Case strField
                                    Case Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_ZFTPFWQ, _
                                        Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_ZFTPYH
                                        strErrMsg = "����[" + strField + "]�������룡"
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
        ' ���桰����_B_ϵͳ������������
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

            '��ʼ��
            doSaveXitongcanshuData = False
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""

                '���
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

                    '����SQL
                    Dim objDictionaryEntry As System.Collections.DictionaryEntry
                    Dim strFields As String
                    Dim strValues As String
                    Dim strField As String
                    Dim intOldBS As Integer
                    Dim i As Integer
                    Select Case objenumEditType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                            '�����ֶ��б��ֶ�ֵ
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

                            '׼��SQL���
                            strSQL = ""
                            strSQL = strSQL + " insert into ����_B_ϵͳ���� (" + vbCr
                            strSQL = strSQL + "   " + strFields + vbCr
                            strSQL = strSQL + " ) values (" + vbCr
                            strSQL = strSQL + "   " + strValues + vbCr
                            strSQL = strSQL + " )" + vbCr

                            '׼���йز���
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
                            '��ȡԭ��ʶ
                            intOldBS = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_BS), 0)

                            '�����ֶ��б��ֶ�ֵ
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

                            '׼��SQL���
                            strSQL = ""
                            strSQL = strSQL + " update ����_B_ϵͳ���� set " + vbCr
                            strSQL = strSQL + "   " + strFields + vbCr
                            strSQL = strSQL + " where ��ʶ = @oldbs" + vbCr

                            '׼���йز���
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

                    'ִ��SQL
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

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
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
        ' ɾ��������_B_ϵͳ������������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     objOldData           ��������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
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

            '��ʼ��
            doDeleteXitongcanshuData = False
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""

                '���
                If strUserId.Trim = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If
                If objOldData Is Nothing Then
                    strErrMsg = "����δ����ɵ����ݣ�"
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

                    '����SQL
                    Dim intOldBS As Integer
                    intOldBS = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_BS), 0)
                    strSQL = ""
                    strSQL = strSQL + " delete from ����_B_ϵͳ���� "
                    strSQL = strSQL + " where ��ʶ = @oldbs"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@oldbs", intOldBS)

                    'ִ��SQL
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

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
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
        ' ��ȡϵͳ�����е�FTP������������Ϣ
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     objFTPProperty       ��FTP����������(����)
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getFtpServerParam( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objTempXitongcanshuData As Xydc.Platform.Common.Data.XitongcanshuData
            Dim strSQL As String

            '��ʼ��
            getFtpServerParam = False
            objFTPProperty = Nothing
            strErrMsg = ""

            Try
                '��ȡ����
                If Me.getXitongcanshuData(strErrMsg, strUserId, strPassword, "", objTempXitongcanshuData) = False Then
                    GoTo errProc
                End If
                If objTempXitongcanshuData.Tables.Count < 1 Then
                    strErrMsg = "����û������ϵͳ���в�����"
                    GoTo errProc
                End If
                If objTempXitongcanshuData.Tables(Xydc.Platform.Common.Data.XitongcanshuData.TABLE_GL_B_XITONGCANSHU) Is Nothing Then
                    strErrMsg = "����û������ϵͳ���в�����"
                    GoTo errProc
                End If
                With objTempXitongcanshuData.Tables(Xydc.Platform.Common.Data.XitongcanshuData.TABLE_GL_B_XITONGCANSHU)
                    If .Rows.Count < 1 Then
                        strErrMsg = "����û������ϵͳ���в�����"
                        GoTo errProc
                    End If
                End With

                '��������
                objFTPProperty = New Xydc.Platform.Common.Utilities.FTPProperty

                '���ز���
                Dim strFtpPassword As String = ""
                Dim blnSFJM As Boolean = False
                Dim intSFJM As Integer = 0
                Dim objMM As Byte()
                With objTempXitongcanshuData.Tables(Xydc.Platform.Common.Data.XitongcanshuData.TABLE_GL_B_XITONGCANSHU).Rows(0)
                    '�Ƿ����
                    intSFJM = objPulicParameters.getObjectValue(.Item(Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_SFJM), 0)
                    If intSFJM = 0 Then
                        blnSFJM = False
                    Else
                        blnSFJM = True
                    End If

                    '�Ǽ��ܲ���
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

            '����
            getFtpServerParam = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.XitongcanshuData.SafeRelease(objTempXitongcanshuData)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.FTPProperty.SafeRelease(objFTPProperty)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡϵͳ�����е�FTP������������Ϣ
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objSqlConnection     ��ָ������
        '     objFTPProperty       ��FTP����������(����)
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getFtpServerParam( _
            ByRef strErrMsg As String, _
            ByVal objSqlConnection As System.Data.SqlClient.SqlConnection, _
            ByRef objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objTempXitongcanshuData As Xydc.Platform.Common.Data.XitongcanshuData
            Dim strSQL As String

            '��ʼ��
            getFtpServerParam = False
            objFTPProperty = Nothing
            strErrMsg = ""

            Try
                '��ȡ����
                If Me.getXitongcanshuData(strErrMsg, objSqlConnection, "", objTempXitongcanshuData) = False Then
                    GoTo errProc
                End If
                If objTempXitongcanshuData.Tables.Count < 1 Then
                    strErrMsg = "����û������ϵͳ���в�����"
                    GoTo errProc
                End If
                If objTempXitongcanshuData.Tables(Xydc.Platform.Common.Data.XitongcanshuData.TABLE_GL_B_XITONGCANSHU) Is Nothing Then
                    strErrMsg = "����û������ϵͳ���в�����"
                    GoTo errProc
                End If
                With objTempXitongcanshuData.Tables(Xydc.Platform.Common.Data.XitongcanshuData.TABLE_GL_B_XITONGCANSHU)
                    If .Rows.Count < 1 Then
                        strErrMsg = "����û������ϵͳ���в�����"
                        GoTo errProc
                    End If
                End With

                '��������
                objFTPProperty = New Xydc.Platform.Common.Utilities.FTPProperty

                '���ز���
                Dim strFtpPassword As String = ""
                Dim blnSFJM As Boolean = False
                Dim intSFJM As Integer = 0
                Dim objMM As Byte()
                With objTempXitongcanshuData.Tables(Xydc.Platform.Common.Data.XitongcanshuData.TABLE_GL_B_XITONGCANSHU).Rows(0)
                    '�Ƿ����
                    intSFJM = objPulicParameters.getObjectValue(.Item(Xydc.Platform.Common.Data.XitongcanshuData.FIELD_GL_B_XITONGCANSHU_SFJM), 0)
                    If intSFJM = 0 Then
                        blnSFJM = False
                    Else
                        blnSFJM = True
                    End If

                    '�Ǽ��ܲ���
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

            '����
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
