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
    ' ����    ��dacDubanshezhi
    '
    ' ����������
    '     �ṩ��ϵͳ������ر�������_B_�������á������ݵ�
    '     ���ӡ��޸ġ�ɾ���������Ȳ���
    '----------------------------------------------------------------

    Public Class dacDubanshezhi
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
        ' ��ȡ������_B_�������á���SQL���(�Ը�λ������������)
        ' ����
        '                          ��SQL
        '----------------------------------------------------------------
        Public Function getMainSQL() As String
            getMainSQL = "select * from ����_B_�������� order by ��λ����"
        End Function

        '----------------------------------------------------------------
        ' ��ȡ������_B_�������á������ݼ�(�Ը�λ������������)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strWhere             �������ַ���
        '     objDubanshezhiData   ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
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

            '��ʼ��
            getDataSet = False
            objDubanshezhiData = Nothing
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                If strWhere.Length > 0 Then strWhere = strWhere.Trim()
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
                    objTempDubanshezhiData = New Xydc.Platform.Common.Data.DubanshezhiData(Xydc.Platform.Common.Data.DubanshezhiData.enumTableType.GL_B_DUBANSHEZHI)

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
                        strSQL = strSQL + "     b.��λ����," + vbCr
                        strSQL = strSQL + "     ���췶Χ���� = case when a.���췶Χ = 0 then '������λ'" + vbCr
                        strSQL = strSQL + "                         when a.���췶Χ = 1 then 'ָ���������²���'" + vbCr
                        strSQL = strSQL + "                         when a.���췶Χ = 2 then '�������Լ��¼�����'" + vbCr
                        strSQL = strSQL + "                         else ' ' end," + vbCr
                        strSQL = strSQL + "     ������������ = case when a.�������� = 1 then '��һ����λ����'" + vbCr
                        strSQL = strSQL + "                         when a.�������� = 2 then '�޶�����λ����'" + vbCr
                        strSQL = strSQL + "                         when a.�������� = 3 then '��������λ����'" + vbCr
                        strSQL = strSQL + "                         when a.�������� = 4 then '���ļ���λ����'" + vbCr
                        strSQL = strSQL + "                         when a.�������� = 5 then '���弶��λ����'" + vbCr
                        strSQL = strSQL + "                         when a.�������� = 6 then '��������λ����'" + vbCr
                        strSQL = strSQL + "                         else ' ' end" + vbCr
                        strSQL = strSQL + "   from ����_B_�������� a" + vbCr
                        strSQL = strSQL + "   left join ����_B_������λ b on a.��λ���� = b.��λ����" + vbCr
                        strSQL = strSQL + " ) a" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.��λ���� " + vbCr

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        'ִ�в���
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

            '����
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
        ' ��顰����_B_�������á������ݵĺϷ���
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
                strSQL = "select top 0 * from ����_B_��������"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, strUserId, strPassword, strSQL, "����_B_��������", objDataSet) = False Then
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
                        Case Xydc.Platform.Common.Data.DubanshezhiData.FIELD_GL_B_DUBANSHEZHI_GWMC, _
                            Xydc.Platform.Common.Data.DubanshezhiData.FIELD_GL_B_DUBANSHEZHI_DBFWMC, _
                            Xydc.Platform.Common.Data.DubanshezhiData.FIELD_GL_B_DUBANSHEZHI_JSXZMC
                            '�����

                        Case Xydc.Platform.Common.Data.DubanshezhiData.FIELD_GL_B_DUBANSHEZHI_DBFW, _
                            Xydc.Platform.Common.Data.DubanshezhiData.FIELD_GL_B_DUBANSHEZHI_JSXZ
                            '���ּ��
                            strValue = objPulicParameters.getObjectValue(objDictionaryEntry.Value, "")
                            If strValue = "" Then
                                strErrMsg = "����[" + strField + "]�������룡"
                                GoTo errProc
                            End If
                            If objPulicParameters.isIntegerString(strValue) = False Then
                                strErrMsg = "����[" + strField + "]���������֣�"
                                GoTo errProc
                            End If
                            objDictionaryEntry.Value = strValue

                        Case Xydc.Platform.Common.Data.DubanshezhiData.FIELD_GL_B_DUBANSHEZHI_GWDM
                            strValue = objPulicParameters.getObjectValue(objDictionaryEntry.Value, "")
                            If strValue = "" Then
                                strErrMsg = "����[" + strField + "]�������룡"
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
                            End If
                    End Select
                Next
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '���Ψһ��
                Dim strNewGWDM As String
                Dim strOldGWDM As String
                strNewGWDM = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.DubanshezhiData.FIELD_GL_B_DUBANSHEZHI_GWDM), "")
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew, Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eCpyNew
                        strSQL = "select * from ����_B_�������� where ��λ���� = '" + strNewGWDM + "'"
                    Case Else
                        strOldGWDM = objPulicParameters.getObjectValue(objOldData(Xydc.Platform.Common.Data.DubanshezhiData.FIELD_GL_B_DUBANSHEZHI_GWDM), "")
                        strSQL = "select * from ����_B_�������� where ��λ���� = '" + strNewGWDM + "' and ��λ���� <> '" + strOldGWDM + "'"
                End Select
                If objdacCommon.getDataSetBySQL(strErrMsg, strUserId, strPassword, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strErrMsg = "����[������ְ��]�Ѿ����ڣ��뻻һ��ְ��"
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
        ' ���桰����_B_�������á�������
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
            ByVal objNewData As System.Collections.Specialized.ListDictionary, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '��ʼ��
            doSaveData = False
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
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '����SQL
                    Dim objDictionaryEntry As System.Collections.DictionaryEntry
                    Dim strOldGWDM As String
                    Dim strFields As String
                    Dim strValues As String
                    Dim strField As String
                    Dim i As Integer
                    Select Case objenumEditType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew, Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eCpyNew
                            '�����ֶ��б��ֶ�ֵ
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
                                        '�����ύ
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

                            '׼��SQL���
                            strSQL = ""
                            strSQL = strSQL + " insert into ����_B_�������� (" + vbCr
                            strSQL = strSQL + "   " + strFields + vbCr
                            strSQL = strSQL + " ) values (" + vbCr
                            strSQL = strSQL + "   " + strValues + vbCr
                            strSQL = strSQL + " )" + vbCr

                        Case Else
                            '��ȡ����
                            strOldGWDM = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.DubanshezhiData.FIELD_GL_B_DUBANSHEZHI_GWDM), "")

                            '�����ֶ��б��ֶ�ֵ
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

                            '׼��SQL���
                            strSQL = ""
                            strSQL = strSQL + " update ����_B_�������� set " + vbCr
                            strSQL = strSQL + "   " + strFields + vbCr
                            strSQL = strSQL + " where ��λ���� = @oldgwdm" + vbCr

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
        ' ɾ��������_B_�������á�������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     objOldData           ��������
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
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            Dim strSQL As String

            '��ʼ��
            doDeleteData = False
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
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
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '����SQL
                    Dim strOldGWDM As String
                    strOldGWDM = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.DubanshezhiData.FIELD_GL_B_DUBANSHEZHI_GWDM), "")
                    strSQL = ""
                    strSQL = strSQL + " delete from ����_B_�������� "
                    strSQL = strSQL + " where ��λ���� = @oldgwdm"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@oldgwdm", strOldGWDM)

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
