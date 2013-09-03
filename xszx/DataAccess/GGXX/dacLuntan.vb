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
    ' ����    ��dacLuntan
    '
    ' ����������
    '     �ṩ�ԡ��ڲ����ۡ�ģ���漰�����ݲ����
    '----------------------------------------------------------------

    Public Class dacLuntan
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
        ' SqlDataAdapter����
        '----------------------------------------------------------------
        Protected ReadOnly Property SqlDataAdapter() As System.Data.SqlClient.SqlDataAdapter
            Get
                SqlDataAdapter = m_objSqlDataAdapter
            End Get
        End Property










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
        ' �ж�strRYDM�Ƿ���Ч��
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     strUserId                   ���û���ʶ
        '     strPassword                 ���û�����
        '     strRYDM                     ����Ա����
        '     blnValid                    �������أ�=True��Ч��=Falseͣ��
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
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

            '��ʼ��
            isValid = False
            blnValid = False
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
                If strRYDM Is Nothing Then strRYDM = ""
                strRYDM = strRYDM.Trim

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ����
                strSQL = ""
                strSQL = strSQL + " select *" + vbCr
                strSQL = strSQL + " from ����_B_�����û�" + vbCr
                strSQL = strSQL + " where ��Ա���� = '" + strRYDM + "'" + vbCr
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

            '����
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
        ' �ж�strRYDM�Ƿ�ע�᣿
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     strUserId                   ���û���ʶ
        '     strPassword                 ���û�����
        '     strRYDM                     ����Ա����
        '     blnRegister                 �������أ�=True��ע�ᣬ=Falseδע��
        '     strRYNC                     �������ע�ᣬ������Ա�ǳ�
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
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

            '��ʼ��
            isRegistered = False
            blnRegister = False
            strRYNC = ""
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
                If strRYDM Is Nothing Then strRYDM = ""
                strRYDM = strRYDM.Trim

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ����
                strSQL = ""
                strSQL = strSQL + " select *" + vbCr
                strSQL = strSQL + " from ����_B_�����û�" + vbCr
                strSQL = strSQL + " where ��Ա���� = '" + strRYDM + "'" + vbCr
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

            '����
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
        ' ע�ύ���û�
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strUserId              ���û���ʶ
        '     strPassword            ���û�����
        '     strRYDM                ����Ա����
        '     strRYNC                ����Ա�ǳ�
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
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
        ' ��ȡ�����û����ݣ�������֯���롱+����Ա��š�����
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     strUserId                   ���û���ʶ
        '     strPassword                 ���û�����
        '     strWhere                    �������ַ���
        '     objLuntanData               ����Ϣ���ݼ�
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
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

            '��ʼ��
            getDataSet_Yonghu = False
            objLuntanData = Nothing
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

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ����
                Try
                    '�������ݼ�
                    objTempLuntanData = New Xydc.Platform.Common.Data.ggxxLuntanData(Xydc.Platform.Common.Data.ggxxLuntanData.enumTableType.GR_B_JIAOLIUYONGHU)

                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ�м���
                    Dim strFalse As String = Xydc.Platform.Common.Utilities.PulicParameters.CharFalse
                    Dim strTrue As String = Xydc.Platform.Common.Utilities.PulicParameters.CharTrue
                    With Me.m_objSqlDataAdapter
                        '׼��SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.*" + vbCr
                        strSQL = strSQL + " from" + vbCr
                        strSQL = strSQL + " (" + vbCr
                        strSQL = strSQL + "   select" + vbCr
                        strSQL = strSQL + "     a.��Ա����," + vbCr
                        strSQL = strSQL + "     ��Ա�ǳ� = case when b.��Ա���� is null then a.��Ա���� else b.��Ա�ǳ� end," + vbCr
                        strSQL = strSQL + "     b.�Ƿ���Ч," + vbCr
                        strSQL = strSQL + "     a.��֯����," + vbCr
                        strSQL = strSQL + "     ��Ա��� = cast(a.��Ա��� as integer)," + vbCr
                        strSQL = strSQL + "     a.��Ա����," + vbCr
                        strSQL = strSQL + "     ��Ч���� = case when b.��Ա���� is null then @True" + vbCr
                        strSQL = strSQL + "                     when isnull(b.�Ƿ���Ч,0) = 1 then @True" + vbCr
                        strSQL = strSQL + "                     else @False end," + vbCr
                        strSQL = strSQL + "     ע������ = case when b.��Ա���� is null then @False else @True end" + vbCr
                        strSQL = strSQL + "   from ����_B_��Ա a" + vbCr
                        strSQL = strSQL + "   left join ����_B_�����û� b on a.��Ա���� = b.��Ա����" + vbCr
                        strSQL = strSQL + " ) a" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.��֯����,a.��Ա���" + vbCr

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@True", strTrue)
                        objSqlCommand.Parameters.AddWithValue("@False", strFalse)
                        .SelectCommand = objSqlCommand

                        'ִ�в���
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

            '����
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
        ' ����strRYDM��ȡ�����û�����
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     strUserId                   ���û���ʶ
        '     strPassword                 ���û�����
        '     strRYDM                     ����Ա����
        '     blnUnused                   ��������
        '     objLuntanData               ����Ϣ���ݼ�
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
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

            '��ʼ��
            getDataSet_Yonghu = False
            objLuntanData = Nothing
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
                If strRYDM Is Nothing Then strRYDM = ""
                strRYDM = strRYDM.Trim

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ����
                Try
                    '�������ݼ�
                    objTempLuntanData = New Xydc.Platform.Common.Data.ggxxLuntanData(Xydc.Platform.Common.Data.ggxxLuntanData.enumTableType.GR_B_JIAOLIUYONGHU)

                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ�м���
                    Dim strFalse As String = Xydc.Platform.Common.Utilities.PulicParameters.CharFalse
                    Dim strTrue As String = Xydc.Platform.Common.Utilities.PulicParameters.CharTrue
                    With Me.m_objSqlDataAdapter
                        '׼��SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.*" + vbCr
                        strSQL = strSQL + " from" + vbCr
                        strSQL = strSQL + " (" + vbCr
                        strSQL = strSQL + "   select" + vbCr
                        strSQL = strSQL + "     a.��Ա����," + vbCr
                        strSQL = strSQL + "     ��Ա�ǳ� = case when b.��Ա���� is null then a.��Ա���� else b.��Ա�ǳ� end," + vbCr
                        strSQL = strSQL + "     b.�Ƿ���Ч," + vbCr
                        strSQL = strSQL + "     a.��֯����," + vbCr
                        strSQL = strSQL + "     ��Ա��� = cast(a.��Ա��� as integer)," + vbCr
                        strSQL = strSQL + "     a.��Ա����," + vbCr
                        strSQL = strSQL + "     ��Ч���� = case when b.��Ա���� is null then @True" + vbCr
                        strSQL = strSQL + "                     when isnull(b.�Ƿ���Ч,0) = 1 then @True" + vbCr
                        strSQL = strSQL + "                     else @False end," + vbCr
                        strSQL = strSQL + "     ע������ = case when b.��Ա���� is null then @False else @True end" + vbCr
                        strSQL = strSQL + "   from ����_B_��Ա a" + vbCr
                        strSQL = strSQL + "   left join ����_B_�����û� b on a.��Ա���� = b.��Ա����" + vbCr
                        strSQL = strSQL + " ) a" + vbCr
                        strSQL = strSQL + " where a.��Ա���� = @rydm" + vbCr
                        strSQL = strSQL + " order by a.��֯����,a.��Ա���" + vbCr

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@True", strTrue)
                        objSqlCommand.Parameters.AddWithValue("@False", strFalse)
                        objSqlCommand.Parameters.AddWithValue("@rydm", strRYDM)
                        .SelectCommand = objSqlCommand

                        'ִ�в���
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

            '����
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
        ' ��齻���û������ݵĺϷ���
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
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew, _
                        Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eCpyNew
                    Case Else
                        If objOldData Is Nothing Then
                            strErrMsg = "����δ����ɵ����ݣ�"
                            GoTo errProc
                        End If
                End Select
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim

                '��ȡ��ṹ����
                strSQL = "select top 0 * from ����_B_�����û�"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, strUserId, strPassword, strSQL, "����_B_�����û�", objDataSet) = False Then
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
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
                        Case Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_ZZDM, _
                            Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_RYXH, _
                            Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_RYMC, _
                            Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_YXMS, _
                            Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_ZCMS
                            '������

                        Case Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_SFYX
                            If strValue = "" Then
                                strValue = "0"
                            End If
                            If objPulicParameters.isIntegerString(strValue) = False Then
                                strErrMsg = "����[" + strField + "]������Ч�����֣�"
                                GoTo errProc
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

                'У�顰��Ա���롱
                Dim strRYDM As String = objNewData(Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_RYDM)
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew, _
                        Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eCpyNew
                        strSQL = ""
                        strSQL = strSQL + " select * from ����_B_�����û�" + vbCr
                        strSQL = strSQL + " where ��Ա���� = '" + strRYDM + "'" + vbCr
                    Case Else
                        Dim strOldRydm As String
                        strOldRydm = objPulicParameters.getObjectValue(objOldData(Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_RYDM), "")
                        strSQL = ""
                        strSQL = strSQL + " select * from ����_B_�����û�" + vbCr
                        strSQL = strSQL + " where ��Ա���� =  '" + strRYDM + "'" + vbCr
                        strSQL = strSQL + " and   ��Ա���� <> '" + strOldRydm + "'" + vbCr
                End Select
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strErrMsg = "����[��Ա����]�Ѿ����ڣ�"
                    GoTo errProc
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                'У�顰��Ա�ǳơ�
                Dim strRYNC As String = objNewData(Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_RYNC)
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew, _
                        Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eCpyNew
                        strSQL = ""
                        strSQL = strSQL + " select * from ����_B_�����û�" + vbCr
                        strSQL = strSQL + " where ��Ա�ǳ� = '" + strRYNC + "'" + vbCr
                    Case Else
                        Dim strOldRydm As String
                        strOldRydm = objPulicParameters.getObjectValue(objOldData(Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_RYDM), "")
                        strSQL = ""
                        strSQL = strSQL + " select * from ����_B_�����û�" + vbCr
                        strSQL = strSQL + " where ��Ա�ǳ� =  '" + strRYNC + "'" + vbCr
                        strSQL = strSQL + " and   ��Ա���� <> '" + strOldRydm + "'" + vbCr
                End Select
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strErrMsg = "����[��Ա�ǳ�]�Ѿ����ڣ��뻻һ����"
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
        ' ���潻���û�������(��������)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objSqlTransaction    ����������
        '     objOldData           ��������
        '     objNewData           ��������
        '     objenumEditType      ���༭����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
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

            '��ʼ��
            doSave_Yonghu = False
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
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew, _
                        Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eCpyNew
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
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew, _
                            Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eCpyNew
                            '��������ֶ��б�
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_ZZDM, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_RYXH, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_RYMC, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_YXMS, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_ZCMS
                                        '������
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
                            strSQL = strSQL + " insert into ����_B_�����û� (" + strFileds + ")"
                            strSQL = strSQL + " values (" + strValues + ")"
                            '׼������
                            objSqlCommand.Parameters.Clear()
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_ZZDM, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_RYXH, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_RYMC, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_YXMS, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_ZCMS
                                        '������
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
                            'ִ��SQL
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()

                        Case Else
                            '��ȡԭ����ʶ��
                            Dim strOldRydm As String
                            strOldRydm = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_RYDM), "")
                            '��������ֶ��б�
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_ZZDM, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_RYXH, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_RYMC, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_YXMS, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_ZCMS
                                        '������
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
                            strSQL = strSQL + " update ����_B_�����û� set " + vbCr
                            strSQL = strSQL + "   " + strFileds + vbCr
                            strSQL = strSQL + " where ��Ա���� = @oldxh" + vbCr
                            '׼������
                            objSqlCommand.Parameters.Clear()
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_ZZDM, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_RYXH, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_RYMC, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_YXMS, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_ZCMS
                                        '������
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
            doSave_Yonghu = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���潻���û����ݼ�¼(�����������)
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strUserId              ���û���ʶ
        '     strPassword            ���û�����
        '     strRYDM                ����Ա����
        '     strRYNC                ����Ա�ǳ�
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
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
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim
                If strRYDM Is Nothing Then strRYDM = ""
                strRYDM = strRYDM.Trim
                If strRYDM = "" Then
                    strErrMsg = "����û��ָ��Ҫע�����Ա��"
                    GoTo errProc
                End If
                If strRYNC Is Nothing Then strRYNC = ""
                strRYNC = strRYNC.Trim
                If strRYNC = "" Or strRYDM = "" Then
                    strErrMsg = "����û��ָ��Ҫע�����Ա���ǳƣ�"
                    GoTo errProc
                End If

                '����������
                objNewData.Add(Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_RYDM, strRYDM)
                objNewData.Add(Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_RYNC, strRYNC)

                '�Ƿ�ע�᣿
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
                    '��ȡע������
                    If Me.getDataSet_Yonghu(strErrMsg, strUserId, strPassword, strRYDM, True, objDataSet) = False Then
                        GoTo errProc
                    End If
                    If objDataSet.Tables(Xydc.Platform.Common.Data.ggxxLuntanData.TABLE_GR_B_JIAOLIUYONGHU) Is Nothing Then
                        strErrMsg = "�����޷���ȡ[" + strRYDM + "]��ע�����ݣ�"
                        GoTo errProc
                    End If
                    With objDataSet.Tables(Xydc.Platform.Common.Data.ggxxLuntanData.TABLE_GR_B_JIAOLIUYONGHU)
                        If .Rows.Count < 1 Then
                            strErrMsg = "�����޷���ȡ[" + strRYDM + "]��ע�����ݣ�"
                            GoTo errProc
                        End If
                        objOldData = .Rows(0)
                    End With

                    strTemp = objPulicParameters.getObjectValue(objOldData(Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_SFYX), "")
                    objNewData.Add(Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUYONGHU_SFYX, strTemp)
                End If

                '�������¼
                If Me.doVerify_Yonghu(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType) = False Then
                    GoTo errProc
                End If

                '��ȡ��������
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ʼ����
                objSqlTransaction = objSqlConnection.BeginTransaction

                'ִ������
                Try
                    '��������¼
                    If Me.doSave_Yonghu(strErrMsg, objSqlTransaction, objOldData, objNewData, objenumEditType) = False Then
                        GoTo rollDatabase
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
        ' ɾ�������û�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strRYDM              ����Ա����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
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

            '��ʼ��
            doDelete_Yonghu = False
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
                If strRYDM Is Nothing Then strRYDM = ""
                strRYDM = strRYDM.Trim
                If strRYDM = "" Then
                    strErrMsg = "����δָ��[��Ա����]��"
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ʼ����
                objSqlTransaction = objSqlConnection.BeginTransaction()

                'ɾ������
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ɾ��������_B_�����û�����Ϣ
                    strSQL = ""
                    strSQL = strSQL + " delete from ����_B_�����û� " + vbCr
                    strSQL = strSQL + " where ��Ա���� = @rydm" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@rydm", strRYDM)
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
        ' ͣ��/���ý����û�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strRYDM              ����Ա����
        '     blnValid             ��True-���ã�False-ͣ��
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
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

            '��ʼ��
            doValid_Yonghu = False
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
                If strRYDM Is Nothing Then strRYDM = ""
                strRYDM = strRYDM.Trim
                If strRYDM = "" Then
                    strErrMsg = "����δָ��[��Ա����]��"
                    GoTo errProc
                End If

                '����Ƿ����
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

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ʼ����
                objSqlTransaction = objSqlConnection.BeginTransaction()

                '����
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    If blnDo = True Then
                        strSQL = ""
                        strSQL = strSQL + " update ����_B_�����û� set" + vbCr
                        strSQL = strSQL + "   �Ƿ���Ч = @sfyx" + vbCr
                        strSQL = strSQL + " where ��Ա���� = @rydm" + vbCr
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@sfyx", intSfyx)
                        objSqlCommand.Parameters.AddWithValue("@rydm", strRYDM)
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.ExecuteNonQuery()
                    Else
                        strSQL = ""
                        strSQL = strSQL + " insert into ����_B_�����û� (" + vbCr
                        strSQL = strSQL + "   ��Ա����,��Ա�ǳ�,�Ƿ���Ч" + vbCr
                        strSQL = strSQL + " )" + vbCr
                        strSQL = strSQL + " select" + vbCr
                        strSQL = strSQL + "   ��Ա����,��Ա�ǳ�=��Ա����,�Ƿ���Ч=@sfyx" + vbCr
                        strSQL = strSQL + " from ����_B_��Ա" + vbCr
                        strSQL = strSQL + " where ��Ա���� = @rydm"
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
        ' ɾ����������(ȫ�����)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
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

            '��ʼ��
            doDelete_Jiaoliu = False
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

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ʼ����
                objSqlTransaction = objSqlConnection.BeginTransaction()

                'ɾ������
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ɾ��������_B_������¼����Ϣ
                    strSQL = ""
                    strSQL = strSQL + " delete from ����_B_������¼ " + vbCr
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

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
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
        ' ɾ���������ݣ�ָ��ʱ���
        ' ָ��strQSRQ��strJSRQ��strQSRQ <= �������� <= strJSRQ
        ' ָ��strQSRQ         ��strQSRQ <= ��������
        ' ָ��strJSRQ         ���������� <= strJSRQ
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strQSRQ              ����ʼ����
        '     strJSRQ              ����������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
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

            '��ʼ��
            doDelete_Jiaoliu = False
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
                If strQSRQ Is Nothing Then strQSRQ = ""
                strQSRQ = strQSRQ.Trim
                If strJSRQ Is Nothing Then strJSRQ = ""
                strJSRQ = strJSRQ.Trim
                If strJSRQ = "" And strQSRQ = "" Then
                    Exit Try
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ʼ����
                objSqlTransaction = objSqlConnection.BeginTransaction()

                'ɾ������
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ɾ��������_B_������¼����Ϣ
                    strSQL = ""
                    strSQL = strSQL + " delete from ����_B_������¼ " + vbCr
                    If strQSRQ <> "" And strJSRQ <> "" Then
                        strSQL = strSQL + " where convert(varchar(10),��������,120) between '" + strQSRQ + "' and '" + strJSRQ + "'" + vbCr
                    ElseIf strQSRQ <> "" Then
                        strSQL = strSQL + " where convert(varchar(10),��������,120) >= '" + strQSRQ + "'" + vbCr
                    ElseIf strJSRQ <> "" Then
                        strSQL = strSQL + " where convert(varchar(10),��������,120) <= '" + strJSRQ + "'" + vbCr
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
        ' ɾ����������(ָ����¼)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     intJLBH              ���������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
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

            '��ʼ��
            doDelete_Jiaoliu = False
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

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ʼ����
                objSqlTransaction = objSqlConnection.BeginTransaction()

                'ɾ������
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ɾ�������µ����ۼ�¼��Ϣ
                    strSQL = ""
                    strSQL = strSQL + " delete ����_B_������¼" + vbCr
                    strSQL = strSQL + " from ����_B_������¼ a" + vbCr
                    strSQL = strSQL + " left join" + vbCr
                    strSQL = strSQL + " ("
                    strSQL = strSQL + "   select �������" + vbCr
                    strSQL = strSQL + "   from ����_B_������¼" + vbCr
                    strSQL = strSQL + "   where �ϼ���� = @jlbh" + vbCr
                    strSQL = strSQL + "   and   �������� > 1" + vbCr
                    strSQL = strSQL + " ) b on a.������� = b.�������" + vbCr
                    strSQL = strSQL + " where b.������� is not null" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@jlbh", intJLBH)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    'ɾ��������_B_������¼����Ϣ
                    strSQL = ""
                    strSQL = strSQL + " delete from ����_B_������¼ " + vbCr
                    strSQL = strSQL + " where ������� = @jlbh" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@jlbh", intJLBH)
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
        ' ��ȡ������������(����������Ŀ������)
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     strUserId                   ���û���ʶ
        '     strPassword                 ���û�����
        '     strWhere                    ����������
        '     objLuntanData               ����Ϣ���ݼ�
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
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

            '��ʼ��
            getDataSet_Jiaoliu = False
            objLuntanData = Nothing
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

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ����
                Try
                    '�������ݼ�
                    objTempLuntanData = New Xydc.Platform.Common.Data.ggxxLuntanData(Xydc.Platform.Common.Data.ggxxLuntanData.enumTableType.GR_B_JIAOLIUJILU)

                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ�м���
                    With Me.m_objSqlDataAdapter
                        '׼��SQL
                        strSQL = ""
                        strSQL = strSQL + " select" + vbCr
                        strSQL = strSQL + "   a.*" + vbCr
                        strSQL = strSQL + " from" + vbCr
                        strSQL = strSQL + " (" + vbCr
                        strSQL = strSQL + "   select a.*," + vbCr
                        strSQL = strSQL + "     b.��Ա����," + vbCr
                        strSQL = strSQL + "     c.��Ա�ǳ�," + vbCr
                        strSQL = strSQL + "     ������Ŀ = dbo.Ggxx_GetZT_Tlsm(a.�������)" + vbCr
                        strSQL = strSQL + "   from" + vbCr
                        strSQL = strSQL + "   (" + vbCr
                        strSQL = strSQL + "     select *" + vbCr
                        strSQL = strSQL + "     from ����_B_������¼" + vbCr
                        strSQL = strSQL + "     where �������� = 1" + vbCr
                        strSQL = strSQL + "   ) a" + vbCr
                        strSQL = strSQL + "   left join ����_B_��Ա     b on a.��Ա���� = b.��Ա����" + vbCr
                        strSQL = strSQL + "   left join ����_B_�����û� c on a.��Ա���� = c.��Ա����" + vbCr
                        strSQL = strSQL + " ) a" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.������Ŀ desc" + vbCr

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        'ִ�в���
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

            '����
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
        ' ��ȡ�����µ���������(�����������ڡ�����)
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     strUserId                   ���û���ʶ
        '     strPassword                 ���û�����
        '     intJLBH                     ��������
        '     strWhere                    ����������
        '     objLuntanData               ����Ϣ���ݼ�
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
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

            '��ʼ��
            getDataSet_Jiaoliu = False
            objLuntanData = Nothing
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

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ����
                Try
                    '�������ݼ�
                    objTempLuntanData = New Xydc.Platform.Common.Data.ggxxLuntanData(Xydc.Platform.Common.Data.ggxxLuntanData.enumTableType.GR_B_JIAOLIUJILU)

                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ�м���
                    With Me.m_objSqlDataAdapter
                        '׼��SQL
                        strSQL = ""
                        strSQL = strSQL + " select" + vbCr
                        strSQL = strSQL + "   a.*" + vbCr
                        strSQL = strSQL + " from" + vbCr
                        strSQL = strSQL + " (" + vbCr
                        strSQL = strSQL + "   select a.*," + vbCr
                        strSQL = strSQL + "     b.��Ա����," + vbCr
                        strSQL = strSQL + "     c.��Ա�ǳ�," + vbCr
                        strSQL = strSQL + "     ������Ŀ = dbo.Ggxx_GetZT_Tlsm(a.�������)" + vbCr
                        strSQL = strSQL + "   from" + vbCr
                        strSQL = strSQL + "   (" + vbCr
                        strSQL = strSQL + "     select *" + vbCr
                        strSQL = strSQL + "     from ����_B_������¼" + vbCr
                        strSQL = strSQL + "     where �ϼ���� = @jlbh" + vbCr
                        strSQL = strSQL + "   ) a" + vbCr
                        strSQL = strSQL + "   left join ����_B_��Ա     b on a.��Ա���� = b.��Ա����" + vbCr
                        strSQL = strSQL + "   left join ����_B_�����û� c on a.��Ա���� = c.��Ա����" + vbCr
                        strSQL = strSQL + " ) a" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.�������� desc" + vbCr

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@jlbh", intJLBH)
                        .SelectCommand = objSqlCommand

                        'ִ�в���
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

            '����
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
        ' ��ȡָ����������
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     strUserId                   ���û���ʶ
        '     strPassword                 ���û�����
        '     intJLBH                     ��������
        '     objLuntanData               ����Ϣ���ݼ�
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
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

            '��ʼ��
            getDataSet_Jiaoliu = False
            objLuntanData = Nothing
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

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ����
                Try
                    '�������ݼ�
                    objTempLuntanData = New Xydc.Platform.Common.Data.ggxxLuntanData(Xydc.Platform.Common.Data.ggxxLuntanData.enumTableType.GR_B_JIAOLIUJILU)

                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ�м���
                    With Me.m_objSqlDataAdapter
                        '׼��SQL
                        strSQL = ""
                        strSQL = strSQL + " select" + vbCr
                        strSQL = strSQL + "   a.*" + vbCr
                        strSQL = strSQL + " from" + vbCr
                        strSQL = strSQL + " (" + vbCr
                        strSQL = strSQL + "   select a.*," + vbCr
                        strSQL = strSQL + "     b.��Ա����," + vbCr
                        strSQL = strSQL + "     c.��Ա�ǳ�," + vbCr
                        strSQL = strSQL + "     ������Ŀ = dbo.Ggxx_GetZT_Tlsm(a.�������)" + vbCr
                        strSQL = strSQL + "   from" + vbCr
                        strSQL = strSQL + "   (" + vbCr
                        strSQL = strSQL + "     select *" + vbCr
                        strSQL = strSQL + "     from ����_B_������¼" + vbCr
                        strSQL = strSQL + "     where ������� = @jlbh" + vbCr
                        strSQL = strSQL + "   ) a" + vbCr
                        strSQL = strSQL + "   left join ����_B_��Ա     b on a.��Ա���� = b.��Ա����" + vbCr
                        strSQL = strSQL + "   left join ����_B_�����û� c on a.��Ա���� = c.��Ա����" + vbCr
                        strSQL = strSQL + " ) a" + vbCr
                        strSQL = strSQL + " order by a.�������� desc" + vbCr

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@jlbh", intJLBH)
                        .SelectCommand = objSqlCommand

                        'ִ�в���
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

            '����
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
        ' ��顰����_B_������¼�������ݵĺϷ���
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
                strSQL = "select top 0 * from ����_B_������¼"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, strUserId, strPassword, strSQL, "����_B_������¼", objDataSet) = False Then
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
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
                        Case Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_RYMC, _
                            Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_RYNC, _
                            Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_JLSM
                            '������

                        Case Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_JLBH
                            '�Զ���

                        Case Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_FBRQ
                            If strValue = "" Then
                                strValue = Format(Now, "yyyy-MM-dd HH:mm:ss")
                            End If
                            If objPulicParameters.isDatetimeString(strValue) = False Then
                                strErrMsg = "����[" + strField + "]������Ч�����ڣ�"
                                GoTo errProc
                            End If
                            strValue = Format(CType(strValue, System.DateTime), "yyyy-MM-dd HH:mm:ss")

                        Case Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_RYDM, _
                            Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_JLZT
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

                        Case Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_JLJB, _
                            Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_SJBH
                            If strValue = "" Then
                                strValue = "0"
                            End If
                            If objPulicParameters.isIntegerString(strValue) = False Then
                                strErrMsg = "����[" + strField + "]������Ч�����֣�"
                                GoTo errProc
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

                '��顰�ϼ���š��Ƿ���ڣ����Զ����á���������
                Dim strSJBH As String
                strSJBH = objNewData.Item(Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_SJBH).Trim()
                Select Case strSJBH
                    Case "0", ""
                        objNewData.Item(Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_JLJB) = "1"
                        objNewData.Item(Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_SJBH) = "0"
                    Case Else
                        strSQL = ""
                        strSQL = strSQL + " select * from ����_B_������¼" + vbCr
                        strSQL = strSQL + " where ������� = " + strSJBH + vbCr
                        If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                            GoTo errProc
                        End If
                        If objDataSet.Tables(0).Rows.Count < 1 Then
                            strErrMsg = "�����ϼ����ⲻ���ڣ�"
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
        ' ���桰����_B_������¼��������(��������)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objSqlTransaction    ����������
        '     objOldData           ��������
        '     objNewData           ��������
        '     objenumEditType      ���༭����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
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

            '��ʼ��
            doSave_Jiaoliu = False
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
                                    Case Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_RYMC, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_RYNC, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_JLSM
                                        '������
                                    Case Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_JLBH
                                        '�Զ���
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
                            strSQL = strSQL + " insert into ����_B_������¼ (" + strFileds + ")"
                            strSQL = strSQL + " values (" + strValues + ")"
                            '׼������
                            objSqlCommand.Parameters.Clear()
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_RYMC, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_RYNC, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_JLSM
                                        '������
                                    Case Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_JLBH
                                        '�Զ���
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
                            'ִ��SQL
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()

                        Case Else
                            '��ȡԭ��������š�
                            Dim intOldJLBH As Integer
                            intOldJLBH = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_JLBH), 0)
                            '��������ֶ��б�
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_RYMC, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_RYNC, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_JLSM
                                        '������
                                    Case Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_JLBH
                                        '�Զ���
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
                            strSQL = strSQL + " update ����_B_������¼ set " + vbCr
                            strSQL = strSQL + "   " + strFileds + vbCr
                            strSQL = strSQL + " where ������� = @oldjlbh" + vbCr
                            '׼������
                            objSqlCommand.Parameters.Clear()
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_RYMC, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_RYNC, _
                                        Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_JLSM
                                        '������
                                    Case Xydc.Platform.Common.Data.ggxxLuntanData.FIELD_GR_B_JIAOLIUJILU_JLBH
                                        '�Զ���
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
            doSave_Jiaoliu = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���潻����¼���ݼ�¼(�����������)
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strUserId              ���û���ʶ
        '     strPassword            ���û�����
        '     objNewData             ����¼��ֵ(���ر�������ֵ)
        '     objOldData             ����¼��ֵ
        '     objenumEditType        ���༭����
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
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
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim

                '�������¼
                If Me.doVerify_Jiaoliu(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType) = False Then
                    GoTo errProc
                End If

                '��ȡ��������
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ʼ����
                objSqlTransaction = objSqlConnection.BeginTransaction

                'ִ������
                Try
                    '��������¼
                    If Me.doSave_Jiaoliu(strErrMsg, objSqlTransaction, objOldData, objNewData, objenumEditType) = False Then
                        GoTo rollDatabase
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
        ' ����intJLBH��ȡ��������
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     strUserId                   ���û���ʶ
        '     strPassword                 ���û�����
        '     intJLBH                     ��������
        '     strJLZT                     ��(����)��������
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function getJlztByJlbh( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intJLBH As Integer, _
            ByRef strJLZT As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objggxxLuntanData As Xydc.Platform.Common.Data.ggxxLuntanData

            '��ʼ��
            getJlztByJlbh = False
            strJLZT = ""
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

                '��ȡ��Ϣ
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

            '����
            getJlztByJlbh = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Data.ggxxLuntanData.SafeRelease(objggxxLuntanData)
            Exit Function

        End Function

    End Class

End Namespace
