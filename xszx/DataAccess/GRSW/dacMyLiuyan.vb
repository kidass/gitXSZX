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
    ' ����    ��dacMyLiuyan
    '
    ' ����������
    '     �ṩ�ԡ��ҵ��뿪���ԡ�ģ���漰�����ݲ����
    '----------------------------------------------------------------

    Public Class dacMyLiuyan
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.DataAccess.dacMyLiuyan)
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
        ' ��ȡ������Ա��Ч��ί��������Ϣ
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strUserXMArray       ���û���������
        '     objLiuyanData        ������ί����������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXMArray As String(), _
            ByRef objLiuyanData As Xydc.Platform.Common.Data.grswMyLiuyanData) As Boolean

            Dim objTempLiuyanData As Xydc.Platform.Common.Data.grswMyLiuyanData

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            getDataSet = False
            objLiuyanData = Nothing
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim()
                If strUserId = "" Then
                    strErrMsg = "����δָ�������û���"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim

                'ת��ΪSQL�б�
                Dim strList As String = ""
                If objdacCommon.doConvertToSQLValueList(strErrMsg, strUserXMArray, strList) = False Then
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '�������ݼ�
                objTempLiuyanData = New Xydc.Platform.Common.Data.grswMyLiuyanData(Xydc.Platform.Common.Data.grswMyLiuyanData.enumTableType.GR_B_LIKAILIUYAN)

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                'ִ�м���
                With Me.SqlDataAdapter
                    '����SQL
                    strSQL = ""
                    strSQL = strSQL + " select a.*,��Ч����=convert(char(10),a.��Ч����,102)+ '��' + convert(char(10),a.ʧЧ����,102) " + vbCr
                    strSQL = strSQL + " from ����_B_�뿪���� a" + vbCr
                    strSQL = strSQL + " where getdate() between ��Ч���� and ʧЧ����" + vbCr
                    If strList = "" Then
                        strSQL = strSQL + " and ������ = ''" + vbCr
                    Else
                        strSQL = strSQL + " and ������ in (" + strList + ")" + vbCr
                    End If
                    strSQL = strSQL + " order by a.������,a.ί�д�����,a.�������� desc"

                    '���ò���
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    'ִ�в���
                    .Fill(objTempLiuyanData.Tables(Xydc.Platform.Common.Data.grswMyLiuyanData.TABLE_GR_B_LIKAILIUYAN))
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            objLiuyanData = objTempLiuyanData
            getDataSet = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.grswMyLiuyanData.SafeRelease(objTempLiuyanData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ[������=strUserXM]����������
        ' ��ȡ������_B_�뿪���ԡ���ȫ���ݵ����ݼ�(���������ڽ�������)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strUserXM            ����ǰ����Ա����
        '     strWhere             �������ַ���
        '     objLKLYDataSet       ����Ϣ���ݼ�
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
            ByRef objLKLYDataSet As Xydc.Platform.Common.Data.grswMyLiuyanData) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objTempLKLYDataSet As Xydc.Platform.Common.Data.grswMyLiuyanData
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '��ʼ��
            getDataSet = False
            objLKLYDataSet = Nothing
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
                    objTempLKLYDataSet = New Xydc.Platform.Common.Data.grswMyLiuyanData(Xydc.Platform.Common.Data.grswMyLiuyanData.enumTableType.GR_B_LIKAILIUYAN)

                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ�м���
                    With Me.m_objSqlDataAdapter
                        '׼��SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.*,��Ч����=convert(char(10),a.��Ч����,102)+ '��' + convert(char(10),a.ʧЧ����,102) " + vbCr
                        strSQL = strSQL + " from ����_B_�뿪���� a " + vbCr
                        strSQL = strSQL + " where ������ = '" + strUserXM + "'" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " and " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.�������� desc " + vbCr

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        'ִ�в���
                        .Fill(objTempLKLYDataSet.Tables(Xydc.Platform.Common.Data.grswMyLiuyanData.TABLE_GR_B_LIKAILIUYAN))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempLKLYDataSet.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.grswMyLiuyanData.SafeRelease(objTempLKLYDataSet)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objLKLYDataSet = objTempLKLYDataSet
            getDataSet = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Data.grswMyLiuyanData.SafeRelease(objTempLKLYDataSet)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ[ί�д�����=strUserXM]����������
        ' ��ȡ������_B_�뿪���ԡ���ȫ���ݵ����ݼ�(���������ڽ�������)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strUserXM            ����ǰ����Ա����
        '     strWhere             �������ַ���
        '     blnUnused            ���ӿ�������
        '     objLKLYDataSet       ����Ϣ���ݼ�
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
            ByRef objLKLYDataSet As Xydc.Platform.Common.Data.grswMyLiuyanData) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objTempLKLYDataSet As Xydc.Platform.Common.Data.grswMyLiuyanData
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '��ʼ��
            getDataSet = False
            objLKLYDataSet = Nothing
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
                    objTempLKLYDataSet = New Xydc.Platform.Common.Data.grswMyLiuyanData(Xydc.Platform.Common.Data.grswMyLiuyanData.enumTableType.GR_B_LIKAILIUYAN)

                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ�м���
                    With Me.m_objSqlDataAdapter
                        '׼��SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.*,��Ч����=convert(char(10),a.��Ч����,102)+ '��' + convert(char(10),a.ʧЧ����,102) " + vbCr
                        strSQL = strSQL + " from ����_B_�뿪���� a " + vbCr
                        strSQL = strSQL + " where ί�д����� = '" + strUserXM + "'" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " and " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.�������� desc " + vbCr

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        'ִ�в���
                        .Fill(objTempLKLYDataSet.Tables(Xydc.Platform.Common.Data.grswMyLiuyanData.TABLE_GR_B_LIKAILIUYAN))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempLKLYDataSet.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.grswMyLiuyanData.SafeRelease(objTempLKLYDataSet)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objLKLYDataSet = objTempLKLYDataSet
            getDataSet = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Data.grswMyLiuyanData.SafeRelease(objTempLKLYDataSet)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��顰����_B_�뿪���ԡ������ݵĺϷ���
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
                strSQL = "select top 0 * from ����_B_�뿪����"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, strUserId, strPassword, strSQL, "����_B_�뿪����", objDataSet) = False Then
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
                        Case Xydc.Platform.Common.Data.grswMyLiuyanData.FIELD_GR_B_LIKAILIUYAN_BS
                            '�Զ�ֵ

                        Case Xydc.Platform.Common.Data.grswMyLiuyanData.FIELD_GR_B_LIKAILIUYAN_LYRQ, _
                            Xydc.Platform.Common.Data.grswMyLiuyanData.FIELD_GR_B_LIKAILIUYAN_SXRQ, _
                            Xydc.Platform.Common.Data.grswMyLiuyanData.FIELD_GR_B_LIKAILIUYAN_ZFRQ
                            If strValue = "" Then
                                strErrMsg = "����[" + strField + "]û�����룡"
                                GoTo errProc
                            End If
                            If objPulicParameters.isDatetimeString(strValue) = False Then
                                strErrMsg = "����[" + strField + "]������Ч�����ڣ�"
                                GoTo errProc
                            End If
                            strValue = Format(CType(strValue, System.DateTime), "yyyy-MM-dd HH:mm:ss")

                        Case Xydc.Platform.Common.Data.grswMyLiuyanData.FIELD_GR_B_LIKAILIUYAN_LYR
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
        ' ���桰����_B_�뿪���ԡ�������
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
                                    Case Xydc.Platform.Common.Data.grswMyLiuyanData.FIELD_GR_B_LIKAILIUYAN_BS
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
                            strSQL = strSQL + " insert into ����_B_�뿪���� (" + strFileds + ")"
                            strSQL = strSQL + " values (" + strValues + ")"
                            '׼������
                            objSqlCommand.Parameters.Clear()
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.grswMyLiuyanData.FIELD_GR_B_LIKAILIUYAN_BS
                                    Case Xydc.Platform.Common.Data.grswMyLiuyanData.FIELD_GR_B_LIKAILIUYAN_LYRQ, _
                                        Xydc.Platform.Common.Data.grswMyLiuyanData.FIELD_GR_B_LIKAILIUYAN_SXRQ, _
                                        Xydc.Platform.Common.Data.grswMyLiuyanData.FIELD_GR_B_LIKAILIUYAN_ZFRQ
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), System.DBNull.Value)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(objNewData.Item(i), System.DateTime))
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
                            Dim intOldBS As Integer
                            intOldBS = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.grswMyLiuyanData.FIELD_GR_B_LIKAILIUYAN_BS), 0)
                            '��������ֶ��б�
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.grswMyLiuyanData.FIELD_GR_B_LIKAILIUYAN_BS
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
                            strSQL = strSQL + " update ����_B_�뿪���� set " + vbCr
                            strSQL = strSQL + "   " + strFileds + vbCr
                            strSQL = strSQL + " where ��ʶ = @oldbs" + vbCr
                            '׼������
                            objSqlCommand.Parameters.Clear()
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.grswMyLiuyanData.FIELD_GR_B_LIKAILIUYAN_BS
                                    Case Xydc.Platform.Common.Data.grswMyLiuyanData.FIELD_GR_B_LIKAILIUYAN_LYRQ, _
                                        Xydc.Platform.Common.Data.grswMyLiuyanData.FIELD_GR_B_LIKAILIUYAN_SXRQ, _
                                        Xydc.Platform.Common.Data.grswMyLiuyanData.FIELD_GR_B_LIKAILIUYAN_ZFRQ
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), System.DBNull.Value)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(objNewData.Item(i), System.DateTime))
                                        End If
                                    Case Else
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), " ")
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), objNewData.Item(i))
                                        End If
                                End Select
                            Next
                            objSqlCommand.Parameters.AddWithValue("@oldbs", intOldBS)
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
        ' ɾ��������_B_�뿪���ԡ�������
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

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
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
                If objOldData Is Nothing Then
                    strErrMsg = "����δ����ɵ����ݣ�"
                    GoTo errProc
                End If
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

                'ɾ������
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '����SQL
                    Dim intOldBS As Integer
                    intOldBS = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.grswMyLiuyanData.FIELD_GR_B_LIKAILIUYAN_BS), 0)
                    strSQL = ""
                    strSQL = strSQL + " delete from ����_B_�뿪���� " + vbCr
                    strSQL = strSQL + " where ��ʶ = @oldbs" + vbCr
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
        ' �ܾ�����ָ��ί��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     objOldData           ��ί������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doReject( _
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

            '��ʼ��
            doReject = False
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
                    strErrMsg = "����δ����Ҫ�ܾ���ί�����ݣ�"
                    GoTo errProc
                End If
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

                '�ܾ�����
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '����SQL
                    Dim intOldBS As Integer
                    intOldBS = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.grswMyLiuyanData.FIELD_GR_B_LIKAILIUYAN_BS), 0)
                    strSQL = ""
                    strSQL = strSQL + " update ����_B_�뿪���� set " + vbCr
                    strSQL = strSQL + "   ί�д����� = @wtdlr" + vbCr
                    strSQL = strSQL + " where ��ʶ = @oldbs" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@wtdlr", " ")
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
            doReject = True
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
