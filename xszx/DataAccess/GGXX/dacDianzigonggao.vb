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
    ' ����    ��dacDianzigonggao
    '
    ' ����������
    '     �ṩ�ԡ����ӹ��桱ģ���漰�����ݲ����
    '----------------------------------------------------------------

    Public Class dacDianzigonggao
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.DataAccess.dacDianzigonggao)
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
        ' ��ȡ[����Ա����=strCzydm]�ĵ��ӹ������ݣ��������ڡ����򣩣���
        ' �Ҹ��𷢲��ĵ��ӹ�������
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     strUserId                   ���û���ʶ
        '     strPassword                 ���û�����
        '     strCzydm                    ����ǰ����Ա��ʶ
        '     strWhere                    �������ַ���
        '     objDianzigonggaoData        ����Ϣ���ݼ�
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strCzydm As String, _
            ByVal strWhere As String, _
            ByRef objDianzigonggaoData As Xydc.Platform.Common.Data.ggxxDianzigonggaoData) As Boolean

            Dim objTempDianzigonggaoData As Xydc.Platform.Common.Data.ggxxDianzigonggaoData
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            Dim objdacCustomer As New Xydc.Platform.DataAccess.dacCustomer
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '��ʼ��
            getDataSet = False
            objDianzigonggaoData = Nothing
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
                If strCzydm Is Nothing Then strCzydm = ""
                strCzydm = strCzydm.Trim
                If strCzydm = "" Then
                    strErrMsg = "����δָ��[������]��"
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ����Ա���ơ�
                Dim strUserXM As String
                If objdacCustomer.getRymcByRydm(strErrMsg, objSqlConnection, strUserId, strUserXM) = False Then
                    GoTo errProc
                End If
                If strUserXM = "" Then
                    strErrMsg = "���󣺷�����[" + strUserId + "]�ı�ʶ�����ڣ�"
                    GoTo errProc
                End If

                '��ȡ����
                Try
                    '�������ݼ�
                    objTempDianzigonggaoData = New Xydc.Platform.Common.Data.ggxxDianzigonggaoData(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.enumTableType.GR_B_GONGGAOLAN)

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
                        strSQL = strSQL + "   select a.*," + vbCr
                        strSQL = strSQL + "     �Ƿ��Ķ� = case when b.����Ա���� is null then '" + strFalse + "' else '" + strTrue + "' end," + vbCr
                        strSQL = strSQL + "     �������� = case when isnull(a.������ʶ,0) = 0 then '" + strFalse + "' else '" + strTrue + "' end" + vbCr
                        strSQL = strSQL + "   from" + vbCr
                        strSQL = strSQL + "   ("
                        strSQL = strSQL + "     select *" + vbCr
                        strSQL = strSQL + "     from ����_B_������" + vbCr
                        strSQL = strSQL + "     where ����Ա���� = @czydm" + vbCr
                        strSQL = strSQL + "   ) a" + vbCr
                        strSQL = strSQL + "   left join " + vbCr
                        strSQL = strSQL + "   (" + vbCr
                        strSQL = strSQL + "     select *" + vbCr
                        strSQL = strSQL + "     from ����_B_�������Ķ����" + vbCr
                        strSQL = strSQL + "     where �Ķ���Ա = @ydry" + vbCr
                        strSQL = strSQL + "   ) b on a.����Ա���� = b.����Ա���� and a.��� = b.���" + vbCr
                        strSQL = strSQL + " ) a" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.���� desc " + vbCr

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@czydm", strCzydm)
                        objSqlCommand.Parameters.AddWithValue("@ydry", strUserXM)
                        .SelectCommand = objSqlCommand

                        'ִ�в���
                        .Fill(objTempDianzigonggaoData.Tables(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.TABLE_GR_B_GONGGAOLAN))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDianzigonggaoData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.ggxxDianzigonggaoData.SafeRelease(objTempDianzigonggaoData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objDianzigonggaoData = objTempDianzigonggaoData
            getDataSet = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.ggxxDianzigonggaoData.SafeRelease(objTempDianzigonggaoData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ[����Ա����=strCzydm�����=intXH]�ĵ��ӹ�������
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     strUserId                   ���û���ʶ
        '     strPassword                 ���û�����
        '     strCzydm                    ����ǰ����Ա��ʶ
        '     intXH                       ���������
        '     objDianzigonggaoData        ����Ϣ���ݼ�
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strCzydm As String, _
            ByVal intXH As Integer, _
            ByRef objDianzigonggaoData As Xydc.Platform.Common.Data.ggxxDianzigonggaoData) As Boolean

            Dim objTempDianzigonggaoData As Xydc.Platform.Common.Data.ggxxDianzigonggaoData
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            Dim objdacCustomer As New Xydc.Platform.DataAccess.dacCustomer
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '��ʼ��
            getDataSet = False
            objDianzigonggaoData = Nothing
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
                If strCzydm Is Nothing Then strCzydm = ""
                strCzydm = strCzydm.Trim

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ����Ա���ơ�
                Dim strUserXM As String
                If objdacCustomer.getRymcByRydm(strErrMsg, objSqlConnection, strUserId, strUserXM) = False Then
                    GoTo errProc
                End If
                If strUserXM = "" Then
                    strErrMsg = "���󣺷�����[" + strUserId + "]�ı�ʶ�����ڣ�"
                    GoTo errProc
                End If

                '��ȡ����
                Try
                    '�������ݼ�
                    objTempDianzigonggaoData = New Xydc.Platform.Common.Data.ggxxDianzigonggaoData(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.enumTableType.GR_B_GONGGAOLAN)

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
                        strSQL = strSQL + "   select a.*," + vbCr
                        strSQL = strSQL + "     �Ƿ��Ķ� = case when b.����Ա���� is null then '" + strFalse + "' else '" + strTrue + "' end," + vbCr
                        strSQL = strSQL + "     �������� = case when isnull(a.������ʶ,0) = 0 then '" + strFalse + "' else '" + strTrue + "' end" + vbCr
                        strSQL = strSQL + "   from" + vbCr
                        strSQL = strSQL + "   ("
                        strSQL = strSQL + "     select *" + vbCr
                        strSQL = strSQL + "     from ����_B_������" + vbCr
                        strSQL = strSQL + "     where ����Ա���� = @czydm" + vbCr
                        strSQL = strSQL + "     and   ��� = @xh" + vbCr
                        strSQL = strSQL + "   ) a" + vbCr
                        strSQL = strSQL + "   left join " + vbCr
                        strSQL = strSQL + "   (" + vbCr
                        strSQL = strSQL + "     select *" + vbCr
                        strSQL = strSQL + "     from ����_B_�������Ķ����" + vbCr
                        strSQL = strSQL + "     where ����Ա���� = @czydm" + vbCr
                        strSQL = strSQL + "     and   ��� = @xh" + vbCr
                        strSQL = strSQL + "     and   �Ķ���Ա = @ydry" + vbCr
                        strSQL = strSQL + "   ) b on a.����Ա���� = b.����Ա���� and a.��� = b.���" + vbCr
                        strSQL = strSQL + " ) a" + vbCr
                        strSQL = strSQL + " order by a.���� desc " + vbCr

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@czydm", strCzydm)
                        objSqlCommand.Parameters.AddWithValue("@xh", intXH)
                        objSqlCommand.Parameters.AddWithValue("@ydry", strUserXM)
                        .SelectCommand = objSqlCommand

                        'ִ�в���
                        .Fill(objTempDianzigonggaoData.Tables(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.TABLE_GR_B_GONGGAOLAN))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDianzigonggaoData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.ggxxDianzigonggaoData.SafeRelease(objTempDianzigonggaoData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objDianzigonggaoData = objTempDianzigonggaoData
            getDataSet = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.ggxxDianzigonggaoData.SafeRelease(objTempDianzigonggaoData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡstrUserId���ܹ��Ķ����ѷ����ĵ��ӹ������ݣ��������ڡ����򣩣���
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     strUserId                   ���û���ʶ
        '     strPassword                 ���û�����
        '     strWhere                    �������ַ���
        '     objDianzigonggaoData        ����Ϣ���ݼ�
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objDianzigonggaoData As Xydc.Platform.Common.Data.ggxxDianzigonggaoData) As Boolean

            Dim objTempDianzigonggaoData As Xydc.Platform.Common.Data.ggxxDianzigonggaoData
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            Dim objdacCustomer As New Xydc.Platform.DataAccess.dacCustomer
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '��ʼ��
            getDataSet = False
            objDianzigonggaoData = Nothing
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

                '��ȡ����Ա���ơ�
                Dim strUserXM As String
                If objdacCustomer.getRymcByRydm(strErrMsg, objSqlConnection, strUserId, strUserXM) = False Then
                    GoTo errProc
                End If
                If strUserXM = "" Then
                    strErrMsg = "���󣺷�����[" + strUserId + "]�ı�ʶ�����ڣ�"
                    GoTo errProc
                End If

                '��ȡ����
                Try
                    '�������ݼ�
                    objTempDianzigonggaoData = New Xydc.Platform.Common.Data.ggxxDianzigonggaoData(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.enumTableType.GR_B_GONGGAOLAN)

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
                        strSQL = strSQL + "   select a.*," + vbCr
                        strSQL = strSQL + "     �Ƿ��Ķ� = case when b.����Ա���� is null then '" + strFalse + "' else '" + strTrue + "' end," + vbCr
                        strSQL = strSQL + "     �������� = case when isnull(a.������ʶ,0) = 0 then '" + strFalse + "' else '" + strTrue + "' end" + vbCr
                        strSQL = strSQL + "   from" + vbCr
                        strSQL = strSQL + "   ("
                        strSQL = strSQL + "     select *" + vbCr
                        strSQL = strSQL + "     from ����_B_������" + vbCr
                        strSQL = strSQL + "     where ������ʶ = 1" + vbCr  '�ѷ���
                        strSQL = strSQL + "   ) a" + vbCr
                        strSQL = strSQL + "   left join " + vbCr
                        strSQL = strSQL + "   (" + vbCr
                        strSQL = strSQL + "     select *" + vbCr
                        strSQL = strSQL + "     from ����_B_�������Ķ����" + vbCr
                        strSQL = strSQL + "     where �Ķ���Ա = @ydry" + vbCr
                        strSQL = strSQL + "   ) b on a.����Ա���� = b.����Ա���� and a.��� = b.���" + vbCr
                        strSQL = strSQL + "   left join " + vbCr
                        strSQL = strSQL + "   (" + vbCr
                        strSQL = strSQL + "     select *" + vbCr
                        strSQL = strSQL + "     from ����_B_�������Ķ���Χ" + vbCr
                        strSQL = strSQL + "     where �Ķ���Ա = @ydry" + vbCr
                        strSQL = strSQL + "   ) c on a.����Ա���� = c.����Ա���� and a.��� = c.���" + vbCr
                        strSQL = strSQL + "   where ((isnull(a.�Ķ�����,0) = 0) or (isnull(a.�Ķ�����,0) = 1 and c.����Ա���� is not null) or (a.����Ա = '" + strUserXM + "'))" '���Ķ�
                        strSQL = strSQL + " ) a" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.���� desc " + vbCr

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@ydry", strUserXM)
                        .SelectCommand = objSqlCommand

                        'ִ�в���
                        .Fill(objTempDianzigonggaoData.Tables(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.TABLE_GR_B_GONGGAOLAN))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempDianzigonggaoData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.ggxxDianzigonggaoData.SafeRelease(objTempDianzigonggaoData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objDianzigonggaoData = objTempDianzigonggaoData
            getDataSet = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.ggxxDianzigonggaoData.SafeRelease(objTempDianzigonggaoData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ[����Ա����=strCzydm�����=intXH]�ĵ��ӹ���������Ķ���Ա����
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     strUserId                   ���û���ʶ
        '     strPassword                 ���û�����
        '     strCzydm                    ����ǰ����Ա��ʶ
        '     intXH                       ���������
        '     strYDRY                     �������أ������Ķ���Ա����
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function getKeYueduRenyuan( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strCzydm As String, _
            ByVal intXH As Integer, _
            ByRef strYDRY As String) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim strSQL As String

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet

            '��ʼ��
            getKeYueduRenyuan = False
            strErrMsg = ""
            strYDRY = ""

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
                If strCzydm Is Nothing Then strCzydm = ""
                strCzydm = strCzydm.Trim
                If strCzydm = "" Then
                    strErrMsg = "����δָ��[������]��"
                    GoTo errProc
                End If
                If intXH <= 0 Then
                    strErrMsg = "����δָ����Ч��[�������]��"
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ���ݼ�
                strSQL = ""
                strSQL = strSQL + " select * from ����_B_�������Ķ���Χ" + vbCr
                strSQL = strSQL + " where ����Ա���� = '" + strCzydm + "'" + vbCr
                strSQL = strSQL + " and   ���       =  " + intXH.ToString + "" + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If

                '����
                If objDataSet.Tables.Count > 0 Then
                    If Not (objDataSet.Tables(0) Is Nothing) Then
                        Dim strTemp As String = ""
                        Dim intCount As Integer
                        Dim i As Integer
                        With objDataSet.Tables(0)
                            intCount = .Rows.Count
                            For i = 0 To intCount - 1 Step 1
                                strTemp = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_YUEDUFANWEI_YDRY), "")
                                If strTemp <> "" Then
                                    If strYDRY = "" Then
                                        strYDRY = strTemp
                                    Else
                                        strYDRY = strYDRY + objPulicParameters.CharSeparate + strTemp
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

            '����
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
        ' ȡ���ѷ����ĵ��ӹ��� �� �������ӹ���
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strCzydm             �������˴���
        '     intXH                ���������
        '     blnFabu              ��True-������False-ȡ������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doFabu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strCzydm As String, _
            ByVal intXH As Integer, _
            ByVal blnFabu As Boolean) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '��ʼ��
            doFabu = False
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
                If strCzydm Is Nothing Then strCzydm = ""
                strCzydm = strCzydm.Trim
                If strCzydm = "" Then
                    strErrMsg = "����δָ��[������]��"
                    GoTo errProc
                End If
                If intXH <= 0 Then
                    strErrMsg = "����δָ��[�������]��"
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ʼ����
                objSqlTransaction = objSqlConnection.BeginTransaction

                '����/ȡ������
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '����SQL
                    objSqlCommand.Parameters.Clear()
                    If blnFabu = True Then
                        strSQL = ""
                        strSQL = strSQL + " update ����_B_������ set" + vbCr
                        strSQL = strSQL + "   ������ʶ = 1," + vbCr
                        strSQL = strSQL + "   ���� = @rq" + vbCr
                        strSQL = strSQL + " where ����Ա���� = @czydm" + vbCr
                        strSQL = strSQL + " and   ��� = @xh" + vbCr
                        strSQL = strSQL + " and   ������ʶ <> 1" + vbCr
                        objSqlCommand.Parameters.AddWithValue("@rq", Now)
                        objSqlCommand.Parameters.AddWithValue("@czydm", strCzydm)
                        objSqlCommand.Parameters.AddWithValue("@xh", intXH)
                    Else
                        strSQL = ""
                        strSQL = strSQL + " update ����_B_������ set" + vbCr
                        strSQL = strSQL + "   ������ʶ = 0" + vbCr
                        strSQL = strSQL + " where ����Ա���� = @czydm" + vbCr
                        strSQL = strSQL + " and   ��� = @xh" + vbCr
                        strSQL = strSQL + " and   ������ʶ <> 0" + vbCr
                        objSqlCommand.Parameters.AddWithValue("@czydm", strCzydm)
                        objSqlCommand.Parameters.AddWithValue("@xh", intXH)
                    End If

                    'ִ��
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
        ' ���á��Ѿ��Ķ���
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strCzydm             �������˴���
        '     intXH                ���������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doSetHasRead( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strCzydm As String, _
            ByVal intXH As Integer) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCustomer As New Xydc.Platform.DataAccess.dacCustomer
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '��ʼ��
            doSetHasRead = False
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
                If strCzydm Is Nothing Then strCzydm = ""
                strCzydm = strCzydm.Trim
                If strCzydm = "" Then
                    strErrMsg = "����δָ��[������]��"
                    GoTo errProc
                End If
                If intXH <= 0 Then
                    strErrMsg = "����δָ��[�������]��"
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ����Ա���ơ�
                Dim strUserXM As String
                If objdacCustomer.getRymcByRydm(strErrMsg, objSqlConnection, strUserId, strUserXM) = False Then
                    GoTo errProc
                End If
                If strUserXM = "" Then
                    strErrMsg = "���󣺷�����[" + strUserId + "]�ı�ʶ�����ڣ�"
                    GoTo errProc
                End If

                '��ʼ����
                objSqlTransaction = objSqlConnection.BeginTransaction

                '�����Ѿ��Ķ�
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '����Ķ���¼
                    strSQL = ""
                    strSQL = strSQL + " delete from ����_B_�������Ķ����" + vbCr
                    strSQL = strSQL + " where ����Ա���� = @czydm" + vbCr
                    strSQL = strSQL + " and   ���       = @xh" + vbCr
                    strSQL = strSQL + " and   �Ķ���Ա   = @ydry" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@czydm", strCzydm)
                    objSqlCommand.Parameters.AddWithValue("@xh", intXH)
                    objSqlCommand.Parameters.AddWithValue("@ydry", strUserXM)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '�����Ķ���¼
                    strSQL = ""
                    strSQL = strSQL + " insert into ����_B_�������Ķ���� (" + vbCr
                    strSQL = strSQL + "   ����Ա����,���,�Ķ���Ա" + vbCr
                    strSQL = strSQL + " ) values (" + vbCr
                    strSQL = strSQL + "   @czydm,@xh,@ydry" + vbCr
                    strSQL = strSQL + " )" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@czydm", strCzydm)
                    objSqlCommand.Parameters.AddWithValue("@xh", intXH)
                    objSqlCommand.Parameters.AddWithValue("@ydry", strUserXM)
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
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            doSetHasRead = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ɾ�����ӹ���
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strCzydm             �������˴���
        '     intXH                ���������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doDelete( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strCzydm As String, _
            ByVal intXH As Integer) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseFTP As New Xydc.Platform.Common.Utilities.BaseFTP
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objdacXitongpeizhi As New Xydc.Platform.DataAccess.dacXitongpeizhi
            Dim objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            Dim objDataSet As Xydc.Platform.Common.Data.ggxxDianzigonggaoData
            Dim objDataSet_FJ As System.Data.DataSet
            Dim strZWNR As String = ""
            Dim strSQL As String
            Dim strWJBS As String
            '��ʼ��
            doDelete = False
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
                strCzydm = strCzydm.Trim
                If strCzydm = "" Then
                    strErrMsg = "����δָ��[������]��"
                    GoTo errProc
                End If
                If intXH <= 0 Then
                    strErrMsg = "����δָ��[�������]��"
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ��������


                If Me.getDataSet(strErrMsg, strUserId, strPassword, strCzydm, intXH, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables.Count < 1 Then
                    strErrMsg = "�����޷���ȡ�������ݣ�"
                    GoTo errProc
                End If
                If objDataSet.Tables(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.TABLE_GR_B_GONGGAOLAN) Is Nothing Then
                    strErrMsg = "�����޷���ȡ�������ݣ�"
                    GoTo errProc
                End If
                If objDataSet.Tables(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.TABLE_GR_B_GONGGAOLAN).Rows.Count < 1 Then
                    strErrMsg = "�����޷���ȡ�������ݣ�"
                    GoTo errProc
                End If
                With objDataSet.Tables(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.TABLE_GR_B_GONGGAOLAN).Rows(0)
                    strZWNR = objPulicParameters.getObjectValue(.Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_ZWNR), "")
                    strWJBS = objPulicParameters.getObjectValue(.Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_WJBS), "")
                End With
                If Not (objDataSet Is Nothing) Then
                    Xydc.Platform.Common.Data.ggxxDianzigonggaoData.SafeRelease(objDataSet)
                End If

                '��ȡ�����б�
                strSQL = "select * from ���ӹ���_B_���� where �ļ���ʶ = '" & strWJBS & "'"
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet_FJ) = False Then
                    GoTo errProc
                End If

                '��ȡFTP���Ӳ���
                If objdacXitongpeizhi.getFtpServerParam(strErrMsg, objSqlConnection, objFTPProperty) = False Then
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

                    'ɾ��������_B_�������Ķ���Χ����Ϣ
                    strSQL = ""
                    strSQL = strSQL + " delete from ����_B_�������Ķ���Χ " + vbCr
                    strSQL = strSQL + " where ����Ա���� = @czydm" + vbCr
                    strSQL = strSQL + " and   ���       = @xh" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@czydm", strCzydm)
                    objSqlCommand.Parameters.AddWithValue("@xh", intXH)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    'ɾ��������_B_�������Ķ��������Ϣ
                    strSQL = ""
                    strSQL = strSQL + " delete from ����_B_�������Ķ���� " + vbCr
                    strSQL = strSQL + " where ����Ա���� = @czydm" + vbCr
                    strSQL = strSQL + " and   ���       = @xh" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@czydm", strCzydm)
                    objSqlCommand.Parameters.AddWithValue("@xh", intXH)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    'ɾ��������_B_����������Ϣ
                    strSQL = ""
                    strSQL = strSQL + " delete from ����_B_������ " + vbCr
                    strSQL = strSQL + " where ����Ա���� = @czydm" + vbCr
                    strSQL = strSQL + " and   ���       = @xh" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@czydm", strCzydm)
                    objSqlCommand.Parameters.AddWithValue("@xh", intXH)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    'ɾ�����������ݡ���Ӧ�ļ�����
                    Dim strFilePath As String
                    Dim strUrl As String
                    strFilePath = strZWNR
                    If strFilePath <> "" Then
                        With objFTPProperty
                            strUrl = .getUrl(strFilePath)
                            If objBaseFTP.doDeleteFile(strErrMsg, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword) = False Then
                                '���Բ��ɹ����γ������ļ���
                            End If
                        End With
                    End If

                    'ɾ��������Ϣ
                    'ɾ����Ӧ��FTP�ļ�
                    Dim intcount As Integer
                    Dim i As Integer
                    With objDataSet_FJ.Tables(0)
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
                    objDataSet_FJ.Dispose()
                    objDataSet_FJ = Nothing

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
            Xydc.Platform.Common.Data.ggxxDianzigonggaoData.SafeRelease(objDataSet)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            doDelete = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacXitongpeizhi.SafeRelease(objdacXitongpeizhi)
            Xydc.Platform.Common.Utilities.FTPProperty.SafeRelease(objFTPProperty)
            Xydc.Platform.Common.Data.ggxxDianzigonggaoData.SafeRelease(objDataSet)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��顰����_B_�������������ݵĺϷ���
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
        Public Function doVerify( _
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

            doVerify = False

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
                strSQL = "select top 0 * from ����_B_������"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, strUserId, strPassword, strSQL, "����_B_������", objDataSet) = False Then
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
                        Case Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_SFYD, _
                            Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_FBMS
                            '������

                        Case Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_WJBS
                            'ϵͳ�Զ�����ֵ
                            If strValue = "" Then
                                If objdacCommon.getNewGUID(strErrMsg, strUserId, strPassword, strValue) = False Then
                                    GoTo errProc
                                End If
                            End If

                        Case Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_XH
                            '�����

                        Case Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_RQ
                            If strValue = "" Then
                                strValue = Format(Now, "yyyy-MM-dd HH:mm:ss")
                            End If
                            If objPulicParameters.isDatetimeString(strValue) = False Then
                                strErrMsg = "����[" + strField + "]������Ч�����ڣ�"
                                GoTo errProc
                            End If
                            strValue = Format(CType(strValue, System.DateTime), "yyyy-MM-dd HH:mm:ss")

                        Case Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_BLRQ
                            If strValue <> "" Then
                                If objPulicParameters.isDatetimeString(strValue) = False Then
                                    strErrMsg = "����[" + strField + "]������Ч�����ڣ�"
                                    GoTo errProc
                                End If
                                strValue = Format(CType(strValue, System.DateTime), "yyyy-MM-dd HH:mm:ss")
                            End If

                        Case Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_CZYDM, _
                            Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_ZZDM, _
                            Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_BT, _
                            Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_ZZMC

                            If strValue = "" Then
                                If strField = Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_ZZMC Then
                                    strErrMsg = "����[������λ]����Ϊ�գ�"
                                    GoTo errProc
                                Else
                                    strErrMsg = "����[" + strField + "]����Ϊ�գ�"
                                    GoTo errProc
                                End If

                            End If
                            With objDataSet.Tables(0).Columns(strField)
                                intLen = objPulicParameters.getStringLength(strValue)
                                If intLen > .MaxLength Then
                                    strErrMsg = "����[" + strField + "]���Ȳ��ܳ���[" + .MaxLength.ToString() + "]��ʵ����[" + intLen.ToString() + "]��"
                                    GoTo errProc
                                End If
                            End With

                        Case Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_NR
                            If strValue = "" Then
                                strErrMsg = "����[" + strField + "]����Ϊ�գ�"
                                GoTo errProc
                            End If
                            strValue = objNewData.Item(i).TrimEnd(" ".ToCharArray)
                            With objDataSet.Tables(0).Columns(strField)
                                intLen = objPulicParameters.getStringLength(strValue)
                                If intLen > .MaxLength Then
                                    strErrMsg = "����[" + strField + "]���Ȳ��ܳ���[" + .MaxLength.ToString() + "]��ʵ����[" + intLen.ToString() + "]��"
                                    GoTo errProc
                                End If
                            End With

                        Case Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_FBBS, _
                            Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_YDKZ
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

                '��顰��š�
                Dim strCZYDM As String
                Dim strXH As String
                strCZYDM = objNewData.Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_CZYDM).Trim()
                strXH = objNewData.Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_XH).Trim()
                If strXH = "" Then
                    '�Զ��������
                    If objdacCommon.getNewCode(strErrMsg, objSqlConnection, "���", "����Ա����", strCZYDM, "����_B_������", True, strXH) = False Then
                        GoTo errProc
                    End If
                    objNewData.Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_XH) = strXH
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            doVerify = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���桰����_B_��������������(��������)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objSqlTransaction    ����������
        '     objOldData           ��������
        '     objNewData           ��������
        '     objenumEditType      ���༭����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
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

            '��ʼ��
            doSave = False
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
                                    Case Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_SFYD, _
                                        Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_FBMS
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
                            strSQL = strSQL + " insert into ����_B_������ (" + strFileds + ")"
                            strSQL = strSQL + " values (" + strValues + ")"
                            '׼������
                            objSqlCommand.Parameters.Clear()
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_SFYD, _
                                        Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_FBMS
                                        '������
                                    Case Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_RQ, _
                                        Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_BLRQ
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), System.DBNull.Value)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(objNewData.Item(i), System.DateTime))
                                        End If
                                    Case Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_XH, _
                                        Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_FBBS
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), 0)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(objNewData.Item(i), System.Int32))
                                        End If
                                    Case Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_YDKZ
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), "0")
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), objNewData.Item(i))
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
                            Dim strOldCZYDM As String
                            Dim intOldXH As Integer
                            strOldCZYDM = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_CZYDM), "")
                            intOldXH = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_XH), 0)
                            '��������ֶ��б�
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_SFYD, _
                                        Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_FBMS
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
                            strSQL = strSQL + " update ����_B_������ set " + vbCr
                            strSQL = strSQL + "   " + strFileds + vbCr
                            strSQL = strSQL + " where ����Ա���� = @oldczydm" + vbCr
                            strSQL = strSQL + " and   ���       = @oldxh" + vbCr
                            '׼������
                            objSqlCommand.Parameters.Clear()
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_SFYD, _
                                        Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_FBMS
                                        '������
                                    Case Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_RQ, _
                                        Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_BLRQ
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), System.DBNull.Value)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(objNewData.Item(i), System.DateTime))
                                        End If
                                    Case Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_XH, _
                                        Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_FBBS
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), 0)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(objNewData.Item(i), System.Int32))
                                        End If
                                    Case Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_YDKZ
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), "0")
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), objNewData.Item(i))
                                        End If
                                    Case Else
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), " ")
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), objNewData.Item(i))
                                        End If
                                End Select
                            Next
                            objSqlCommand.Parameters.AddWithValue("@oldczydm", strOldCZYDM)
                            objSqlCommand.Parameters.AddWithValue("@oldxh", intOldXH)
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
            doSave = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���桰����_B_�����������Ķ���Χ����(��������)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objSqlTransaction    ����������
        '     objOldData           ��������
        '     objNewData           ��������
        '     strFBFW              ��������Χ(��Χ����֯����Ա)
        '     objenumEditType      ���༭����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
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
            Dim strRYLIST As String

            '��ʼ��
            doSave = False
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
                If strFBFW Is Nothing Then strFBFW = ""
                strFBFW = strFBFW.Trim

                '��ȡ����
                objSqlConnection = objSqlTransaction.Connection

                '����strFBFW
                If strFBFW = "" Then
                    strRYLIST = ""
                Else
                    '������ʱ����
                    objNewSqlConnection = New System.Data.SqlClient.SqlConnection(objSqlConnection.ConnectionString)
                    objNewSqlConnection.Open()
                    '����
                    If objdacCustomer.getRenyuanList(strErrMsg, objNewSqlConnection, strFBFW, objPulicParameters.CharSeparate, strRYLIST) = False Then
                        GoTo errProc
                    End If
                End If

                '��������
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ɾ��ԭ������
                    Select Case objenumEditType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                        Case Else
                            Dim strOldCZYDM As String
                            Dim intOldXH As Integer
                            strOldCZYDM = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_CZYDM), "")
                            intOldXH = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_XH), 0)
                            strSQL = ""
                            strSQL = strSQL + " delete from ����_B_�������Ķ���Χ" + vbCr
                            strSQL = strSQL + " where ����Ա���� = @czydm" + vbCr
                            strSQL = strSQL + " and   ���       = @xh" + vbCr
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@czydm", strOldCZYDM)
                            objSqlCommand.Parameters.AddWithValue("@xh", intOldXH)
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()
                    End Select

                    '������������
                    If strRYLIST <> "" Then
                        Dim strNewCZYDM As String
                        Dim intNewXH As Integer
                        strNewCZYDM = objPulicParameters.getObjectValue(objNewData.Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_CZYDM), "")
                        intNewXH = objPulicParameters.getObjectValue(objNewData.Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_XH), 0)

                        Dim strArray() As String
                        Dim intCount As Integer
                        Dim i As Integer
                        strArray = strRYLIST.Split(Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate.ToCharArray)
                        intCount = strArray.Length
                        For i = 0 To intCount - 1 Step 1
                            strSQL = ""
                            strSQL = strSQL + " insert into ����_B_�������Ķ���Χ (" + vbCr
                            strSQL = strSQL + "   ����Ա����,���,�Ķ���Ա" + vbCr
                            strSQL = strSQL + " ) values (" + vbCr
                            strSQL = strSQL + "   @czydm, @xh, @ydry"
                            strSQL = strSQL + " )"
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@czydm", strNewCZYDM)
                            objSqlCommand.Parameters.AddWithValue("@xh", intNewXH)
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

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objNewSqlConnection)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            doSave = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objNewSqlConnection)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ������ӹ������ݼ�¼(�����������)
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strUserId              ���û���ʶ
        '     strPassword            ���û�����
        '     objNewData             ����¼��ֵ(���ر�������ֵ)
        '     objOldData             ����¼��ֵ
        '     strFBFW                ��������Χ
        '     objenumEditType        ���༭����
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Function doSave( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal strFBFW As String, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim strSQL As String

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            doSave = False

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
                If strFBFW Is Nothing Then strFBFW = ""
                strFBFW = strFBFW.Trim

                '�������¼
                If Me.doVerify(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType) = False Then
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
                    '�Զ����á��Ķ����ơ�
                    objNewData.Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_YDKZ) = "0"
                    If strFBFW <> "" Then
                        objNewData.Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_YDKZ) = "1"
                    End If

                    '��������¼
                    If Me.doSave(strErrMsg, objSqlTransaction, objOldData, objNewData, objenumEditType) = False Then
                        GoTo rollDatabase
                    End If

                    '�����֡��Ķ���Χ��
                    If Me.doSave(strErrMsg, objSqlTransaction, objOldData, objNewData, strFBFW, objenumEditType) = False Then
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

            doSave = True
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
        ' ������ӹ������ݼ�¼(�����������)
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strUserId              ���û���ʶ
        '     strPassword            ���û�����
        '     objNewData             ����¼��ֵ(���ر�������ֵ)
        '     objOldData             ����¼��ֵ
        '     strFBFW                ��������Χ
        '     objenumEditType        ���༭����
        '     objDataSet_FJ          ���������ݼ�
        '     objFTPProperty         ��FTP����
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Function doSave( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal strFBFW As String, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType, _
            ByVal objDataSet_FJ As Xydc.Platform.Common.Data.ggxxDianzigonggaoData, _
            ByVal objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objOldFJData As Xydc.Platform.Common.Data.ggxxDianzigonggaoData

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim strCZSM As String = Xydc.Platform.Common.Workflow.BaseFlowObject.LOGO_QXBJ
            Dim intWJND As Integer = Year(Now)
            Dim strOldZWNR As String
            Dim strWJBS As String
            Dim strSQL As String

            doSave = False

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
                If strFBFW Is Nothing Then strFBFW = ""
                strFBFW = strFBFW.Trim

                '�������¼
                If Me.doVerify(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType) = False Then
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
                        strWJBS = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_WJBS), "")
                        If Me.getFujianData(strErrMsg, objSqlConnection, strWJBS, objOldFJData) = False Then
                            GoTo errProc
                        End If
                End Select

                '��ʼ����
                objSqlTransaction = objSqlConnection.BeginTransaction

                'ִ������
                Try
                    '�Զ����á��Ķ����ơ�
                    objNewData.Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_YDKZ) = "0"
                    If strFBFW <> "" Then
                        objNewData.Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_YDKZ) = "1"
                    End If

                    '��������¼
                    If Me.doSave(strErrMsg, objSqlTransaction, objOldData, objNewData, objenumEditType) = False Then
                        GoTo rollDatabase
                    End If

                    '�����֡��Ķ���Χ��
                    If Me.doSave(strErrMsg, objSqlTransaction, objOldData, objNewData, strFBFW, objenumEditType) = False Then
                        GoTo rollDatabase
                    End If

                    '�������ļ���ʶ
                    strWJBS = objNewData(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_GR_B_GONGGAOLAN_WJBS)

                    '���渽���ļ�
                    If Me.doSaveFujian(strErrMsg, strWJBS, intWJND, objSqlTransaction, objFTPProperty, objDataSet_FJ, objOldFJData) = False Then
                        GoTo rollGJAndFJFile
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
            Xydc.Platform.Common.Data.ggxxDianzigonggaoData.SafeRelease(objOldFJData)

            doSave = True
            Exit Function

rollGJAndFJFile:
            objSqlTransaction.Rollback()
            If Me.doRestoreFiles_FJ(strSQL, strWJBS, intWJND, objFTPProperty, objDataSet_FJ, objOldFJData) = False Then
                '�Ѿ������ˣ�
            End If
            GoTo errProc

rollDatabase:
            objSqlTransaction.Rollback()
            GoTo errProc

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Xydc.Platform.Common.Data.ggxxDianzigonggaoData.SafeRelease(objOldFJData)
            Exit Function

        End Function








        '----------------------------------------------------------------
        ' �ж�strUserId�Ƿ��ܹ��Ķ����ѷ���strZcydm+intXH�ĵ��ӹ�������
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     strUserId                   ���û���ʶ
        '     strPassword                 ���û�����
        '     strCzydm                    ������Ա����
        '     intXH                       ���������
        '     blnYuedu                    �������أ�True-�ܣ�False-����
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function isCanRead( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strCzydm As String, _
            ByVal intXH As Integer, _
            ByRef blnYuedu As Boolean) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            Dim objdacCustomer As New Xydc.Platform.DataAccess.dacCustomer
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '��ʼ��
            isCanRead = False
            blnYuedu = False
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
                If strCzydm Is Nothing Then strCzydm = ""
                strCzydm = strCzydm.Trim
                If strCzydm = "" Then
                    Exit Try
                End If
                If intXH < 0 Then
                    Exit Try
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ����Ա���ơ�
                Dim strUserXM As String
                If objdacCustomer.getRymcByRydm(strErrMsg, objSqlConnection, strUserId, strUserXM) = False Then
                    GoTo errProc
                End If
                If strUserXM = "" Then
                    strErrMsg = "���󣺷�����[" + strUserId + "]�ı�ʶ�����ڣ�"
                    GoTo errProc
                End If

                '��ȡ����
                Dim strFalse As String = Xydc.Platform.Common.Utilities.PulicParameters.CharFalse
                Dim strTrue As String = Xydc.Platform.Common.Utilities.PulicParameters.CharTrue
                '׼��SQL
                strSQL = ""
                strSQL = strSQL + " select a.*" + vbCr
                strSQL = strSQL + " from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select a.*," + vbCr
                strSQL = strSQL + "     �Ƿ��Ķ� = case when b.����Ա���� is null then '" + strFalse + "' else '" + strTrue + "' end," + vbCr
                strSQL = strSQL + "     �������� = case when isnull(a.������ʶ,0) = 0 then '" + strFalse + "' else '" + strTrue + "' end" + vbCr
                strSQL = strSQL + "   from" + vbCr
                strSQL = strSQL + "   ("
                strSQL = strSQL + "     select *" + vbCr
                strSQL = strSQL + "     from ����_B_������" + vbCr
                strSQL = strSQL + "     where ����Ա���� = '" + strCzydm + "'" + vbCr
                strSQL = strSQL + "     and   ���       =  " + intXH.ToString + vbCr
                strSQL = strSQL + "   ) a" + vbCr
                strSQL = strSQL + "   left join " + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select *" + vbCr
                strSQL = strSQL + "     from ����_B_�������Ķ����" + vbCr
                strSQL = strSQL + "     where �Ķ���Ա = '" + strUserXM + "'" + vbCr
                strSQL = strSQL + "   ) b on a.����Ա���� = b.����Ա���� and a.��� = b.���" + vbCr
                strSQL = strSQL + "   left join " + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select *" + vbCr
                strSQL = strSQL + "     from ����_B_�������Ķ���Χ" + vbCr
                strSQL = strSQL + "     where �Ķ���Ա = '" + strUserXM + "'" + vbCr
                strSQL = strSQL + "   ) c on a.����Ա���� = c.����Ա���� and a.��� = c.���" + vbCr
                strSQL = strSQL + "   where (a.������ʶ = 1 and ((isnull(a.�Ķ�����,0) = 0) or (isnull(a.�Ķ�����,0) = 1 and c.����Ա���� is not null))) or (a.����Ա = '" + strUserXM + "')"
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
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            isCanRead = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function


        '----------------------------------------------------------------
        ' ����strWJBSH��ȡ�����ӹ���_B_�����������ݼ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId                   ���û���ʶ
        '     strPassword                 ���û�����
        '     strWJBS                    ������Ա����
        '     objFujianData        ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getFujianData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWJBS As String, _
            ByRef objFujianData As Xydc.Platform.Common.Data.ggxxDianzigonggaoData) As Boolean

            Dim objTempFujianData As Xydc.Platform.Common.Data.ggxxDianzigonggaoData
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            Dim objdacCustomer As New Xydc.Platform.DataAccess.dacCustomer
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '��ʼ��
            getFujianData = False
            objFujianData = Nothing
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

                '��ȡ����Ա���ơ�
                Dim strUserXM As String
                If objdacCustomer.getRymcByRydm(strErrMsg, objSqlConnection, strUserId, strUserXM) = False Then
                    GoTo errProc
                End If
                If strUserXM = "" Then
                    strErrMsg = "���󣺷�����[" + strUserId + "]�ı�ʶ�����ڣ�"
                    GoTo errProc
                End If

                '��ȡ����
                Dim strFalse As String = Xydc.Platform.Common.Utilities.PulicParameters.CharFalse
                Dim strTrue As String = Xydc.Platform.Common.Utilities.PulicParameters.CharTrue


                '�������ݼ�
                objTempFujianData = New Xydc.Platform.Common.Data.ggxxDianzigonggaoData(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.enumTableType.DZGG_B_FUJIAN)

                If strWJBS = "" Then Exit Try

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                'ִ�м���
                With Me.m_objSqlDataAdapter
                    '��ȡ��������
                    strSQL = ""
                    strSQL = strSQL + " select a.*" + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select *,"
                    strSQL = strSQL + "     ��ʾ��� = ���,"
                    strSQL = strSQL + "     �����ļ� = '',"
                    strSQL = strSQL + "     ���ر�־ = 0 " + vbCr
                    strSQL = strSQL + "   from ���ӹ���_B_���� " + vbCr
                    strSQL = strSQL + "   where  �ļ���ʶ = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + " ) a" + vbCr
                    strSQL = strSQL + " order by a.��ʾ���" + vbCr

                    '���ò���
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    'ִ�в���
                    .Fill(objTempFujianData.Tables(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.TABLE_DZGG_B_FUJIAN))
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objFujianData = objTempFujianData
            getFujianData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Xydc.Platform.Common.Data.ggxxDianzigonggaoData.SafeRelease(objTempFujianData)
            Exit Function

        End Function


        '----------------------------------------------------------------
        ' ��ȡ�ļ��ĸ�����Ϣ
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objFujianData        ����������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getFujianData( _
            ByRef strErrMsg As String, _
            ByVal objSqlConnection As System.Data.SqlClient.SqlConnection, _
            ByVal strWJBS As String, _
            ByRef objFujianData As Xydc.Platform.Common.Data.ggxxDianzigonggaoData) As Boolean

            Dim objTempFujianData As Xydc.Platform.Common.Data.ggxxDianzigonggaoData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            getFujianData = False
            objFujianData = Nothing
            strErrMsg = ""

            Try
                '��ȡ�ļ���ʶ


                '�������ݼ�
                objTempFujianData = New Xydc.Platform.Common.Data.ggxxDianzigonggaoData(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.enumTableType.DZGG_B_FUJIAN)
                If strWJBS = "" Then Exit Try

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                'ִ�м���
                With Me.m_objSqlDataAdapter
                    '��ȡ��������
                    strSQL = ""
                    strSQL = strSQL + " select a.*" + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select *,"
                    strSQL = strSQL + "     ��ʾ��� = ���,"
                    strSQL = strSQL + "     �����ļ� = '',"
                    strSQL = strSQL + "     ���ر�־ = 0 " + vbCr
                    strSQL = strSQL + "   from ���ӹ���_B_���� " + vbCr
                    strSQL = strSQL + "   where �ļ���ʶ = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + " ) a" + vbCr
                    strSQL = strSQL + " order by a.��ʾ���" + vbCr

                    '���ò���
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    'ִ�в���
                    .Fill(objTempFujianData.Tables(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.TABLE_DZGG_B_FUJIAN))
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            objFujianData = objTempFujianData
            getFujianData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.ggxxDianzigonggaoData.SafeRelease(objTempFujianData)
            Exit Function

        End Function


        '----------------------------------------------------------------
        ' �жϸ�����¼�����Ƿ���Ч��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     objNewData           ����¼��ֵ(�����Ƽ�ֵ)
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doVerifyFujian( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef objNewData As System.Collections.Specialized.NameValueCollection) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            Dim objdacCustomer As New Xydc.Platform.DataAccess.dacCustomer
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '��ʼ��
            doVerifyFujian = False
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
                If objNewData Is Nothing Then
                    strErrMsg = "����δ�����µ����ݣ�"
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ��ṹ����
                strSQL = "select top 0 * from ���ӹ���_B_����"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, objSqlConnection, strSQL, "���ӹ���_B_����", objDataSet) = False Then
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
                        Case Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_DZGG_B_FUJIAN_BDWJ, _
                            Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_DZGG_B_FUJIAN_XZBZ, _
                            Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_DZGG_B_FUJIAN_XSXH
                            '��ʾ�ֶΣ����ô���

                        Case Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_DZGG_B_FUJIAN_WJXH
                            If strValue = "" Then
                                strErrMsg = "����[" + strField + "]����Ϊ�գ�"
                                GoTo errProc
                            End If
                            If objPulicParameters.isIntegerString(strValue) = False Then
                                strErrMsg = "����[" + strField + "]���������֣�"
                                GoTo errProc
                            End If
                            intLen = CType(strValue, Integer)
                            If intLen < 1 Or intLen > 999999 Then
                                strErrMsg = "����[" + strField + "]������[1,999999]��"
                                GoTo errProc
                            End If
                            strValue = intLen.ToString()

                        Case Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_DZGG_B_FUJIAN_WJSM
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

                        Case Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_DZGG_B_FUJIAN_WJYS
                            If strValue = "" Then strValue = "1"
                            If objPulicParameters.isIntegerString(strValue) = False Then
                                strErrMsg = "����[" + strField + "]���������֣�"
                                GoTo errProc
                            End If
                            intLen = CType(strValue, Integer)
                            If intLen < 1 Or intLen > 999999 Then
                                strErrMsg = "����[" + strField + "]������[1,999999]��"
                                GoTo errProc
                            End If
                            strValue = intLen.ToString()

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

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            doVerifyFujian = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function


        '----------------------------------------------------------------
        ' ���渽������
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     blnEnforeEdit          ���Ƿ�ǿ���޸�
        '     strUserId              ���û���ʶ
        '     strPassword            ���û�����
        '     strUserXM              ������Ա����
        '     strWJBS                : �ļ���ʶ
        '     objNewData             ����¼��ֵ(���ر�������ֵ)
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Function doSaveFujian( _
            ByRef strErrMsg As String, _
            ByVal blnEnforeEdit As Boolean, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByVal strWJBS As String, _
            ByRef objNewData As Xydc.Platform.Common.Data.ggxxDianzigonggaoData) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objOldData As Xydc.Platform.Common.Data.ggxxDianzigonggaoData
            Dim objDataSet As System.Data.DataSet

            Dim objdacCustomer As New Xydc.Platform.DataAccess.dacCustomer
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon


            Dim strBakExt As String = Xydc.Platform.Common.Utilities.PulicParameters.BACKUPFILEEXT
            Dim strTable As String = Xydc.Platform.Common.Data.ggxxDianzigonggaoData.TABLE_DZGG_B_FUJIAN
            Dim intWJND As Integer = Year(Now)
            Dim strSQL As String

            Dim objdacXitongpeizhi As New Xydc.Platform.DataAccess.dacXitongpeizhi
            Dim objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objBaseFTP As New Xydc.Platform.Common.Utilities.BaseFTP

            Dim objFlowObject As Xydc.Platform.DataAccess.FlowObject

            '��ʼ��
            doSaveFujian = False
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

                '��ȡԭ��������
                If Me.getFujianData(strErrMsg, objSqlConnection, strWJBS, objOldData) = False Then
                    GoTo errProc
                End If

                '��ȡFTP���Ӳ���
                If objdacXitongpeizhi.getFtpServerParam(strErrMsg, objSqlConnection, objFTPProperty) = False Then
                    GoTo errProc
                End If

                '��ʼ����
                objSqlTransaction = objSqlConnection.BeginTransaction()

                '��������
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ɾ��������_B_����������
                    strSQL = ""
                    strSQL = strSQL + " delete from ���ӹ���_B_���� " + vbCr
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
                        Dim strBasePath As String = Me.getBasePath_FJ
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
                                strOldFile = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_DZGG_B_FUJIAN_WJWZ), "")
                                strLocFile = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_DZGG_B_FUJIAN_BDWJ), "")
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
                                strSQL = strSQL + " insert into ���ӹ���_B_���� (" + vbCr
                                strSQL = strSQL + "   �ļ���ʶ, ���, ˵��, ҳ��, λ��" + vbCr
                                strSQL = strSQL + " ) values (" + vbCr
                                strSQL = strSQL + "   @wjbs, @wjxh, @wjsm, @wjys, @wjwz" + vbCr
                                strSQL = strSQL + " )" + vbCr
                                objSqlCommand.Parameters.Clear()
                                objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                                objSqlCommand.Parameters.AddWithValue("@wjxh", (i + 1))
                                objSqlCommand.Parameters.AddWithValue("@wjsm", objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_DZGG_B_FUJIAN_WJSM), ""))
                                objSqlCommand.Parameters.AddWithValue("@wjys", objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_DZGG_B_FUJIAN_WJYS), 0))
                                objSqlCommand.Parameters.AddWithValue("@wjwz", strNewFile)
                                objSqlCommand.CommandText = strSQL
                                objSqlCommand.ExecuteNonQuery()
                            Next
                        End With


                        'ɾ�����б����ļ�
                        If Me.doDeleteBackupFiles_FJ(strErrMsg, objFTPProperty, objOldData) = False Then
                            '���Բ��ɹ����γ������ļ���
                        End If

                    Catch ex As Exception
                        strErrMsg = ex.Message
                        GoTo rollDatabaseAndFile
                    End Try

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
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacXitongpeizhi.SafeRelease(objdacXitongpeizhi)
            Xydc.Platform.Common.Utilities.FTPProperty.SafeRelease(objFTPProperty)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)
            Xydc.Platform.Common.Data.ggxxDianzigonggaoData.SafeRelease(objOldData)
            Xydc.Platform.DataAccess.FlowObject.SafeRelease(objFlowObject)
            '����
            doSaveFujian = True
            Exit Function

rollDatabaseAndFile:
            objSqlTransaction.Rollback()
            If Me.doRestoreFiles_FJ(strSQL, strWJBS, intWJND, objFTPProperty, objNewData, objOldData) = False Then
                '�޷��ָ��ɹ��������ˣ�
            End If
            GoTo errProc

rollDatabase:
            objSqlTransaction.Rollback()
            GoTo errProc

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacXitongpeizhi.SafeRelease(objdacXitongpeizhi)
            Xydc.Platform.Common.Utilities.FTPProperty.SafeRelease(objFTPProperty)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)
            Xydc.Platform.Common.Data.ggxxDianzigonggaoData.SafeRelease(objOldData)
            Xydc.Platform.DataAccess.FlowObject.SafeRelease(objFlowObject)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���渽������
        '     strErrMsg              ����������򷵻ش�����Ϣ
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
            ByVal strWJBS As String, _
            ByVal intWJND As Integer, _
            ByVal objSqlTransaction As System.Data.SqlClient.SqlTransaction, _
            ByVal objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty, _
            ByRef objNewData As Xydc.Platform.Common.Data.ggxxDianzigonggaoData, _
            ByVal objOldData As Xydc.Platform.Common.Data.ggxxDianzigonggaoData) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            Dim strBakExt As String = Xydc.Platform.Common.Utilities.PulicParameters.BACKUPFILEEXT
            Dim strTable As String = Xydc.Platform.Common.Data.ggxxDianzigonggaoData.TABLE_DZGG_B_FUJIAN
            Dim blnNewTrans As Boolean = False
            Dim strSQL As String

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objBaseFTP As New Xydc.Platform.Common.Utilities.BaseFTP
            Dim objFlowObject As Xydc.Platform.DataAccess.FlowObject

            '��ʼ��
            doSaveFujian = False
            strErrMsg = ""

            Try
                '���
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

                '��ȡ������Ϣ               
                objSqlConnection = objSqlTransaction.Connection


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

                    'ɾ��������_B_����������
                    strSQL = ""
                    strSQL = strSQL + " delete from ���ӹ���_B_���� " + vbCr
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
                        Dim strBasePath As String = Me.getBasePath_FJ
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
                                strOldFile = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_DZGG_B_FUJIAN_WJWZ), "")
                                strLocFile = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_DZGG_B_FUJIAN_BDWJ), "")
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
                                strSQL = strSQL + " insert into ���ӹ���_B_���� (" + vbCr
                                strSQL = strSQL + "   �ļ���ʶ, ���, ˵��, ҳ��, λ��" + vbCr
                                strSQL = strSQL + " ) values (" + vbCr
                                strSQL = strSQL + "   @wjbs, @wjxh, @wjsm, @wjys, @wjwz" + vbCr
                                strSQL = strSQL + " )" + vbCr
                                objSqlCommand.Parameters.Clear()
                                objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                                objSqlCommand.Parameters.AddWithValue("@wjxh", (i + 1))
                                objSqlCommand.Parameters.AddWithValue("@wjsm", objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_DZGG_B_FUJIAN_WJSM), ""))
                                objSqlCommand.Parameters.AddWithValue("@wjys", objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_DZGG_B_FUJIAN_WJYS), 0))
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
                    strErrMsg = ex.Message
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
            Xydc.Platform.DataAccess.FlowObject.SafeRelease(objFlowObject)

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
            Xydc.Platform.DataAccess.FlowObject.SafeRelease(objFlowObject)
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
        ' ��ȡ�����������Ļ���Ŀ¼
        '----------------------------------------------------------------
        Public Function getBasePath_FJ() As String
            getBasePath_FJ = Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FILEDIR_FJ
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
            ByVal objFJData As Xydc.Platform.Common.Data.ggxxDianzigonggaoData) As Boolean

            Dim strBakExt As String = Xydc.Platform.Common.Utilities.PulicParameters.BACKUPFILEEXT
            Dim strTable As String = Xydc.Platform.Common.Data.ggxxDianzigonggaoData.TABLE_DZGG_B_FUJIAN

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
                        strOldFile = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_DZGG_B_FUJIAN_WJWZ), "")
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
            ByVal objFJData As Xydc.Platform.Common.Data.ggxxDianzigonggaoData) As Boolean

            Dim strBakExt As String = Xydc.Platform.Common.Utilities.PulicParameters.BACKUPFILEEXT
            Dim strTable As String = Xydc.Platform.Common.Data.ggxxDianzigonggaoData.TABLE_DZGG_B_FUJIAN

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
                        strOldFile = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_DZGG_B_FUJIAN_WJWZ), "")
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
            ByVal objNewData As Xydc.Platform.Common.Data.ggxxDianzigonggaoData, _
            ByVal objOldData As Xydc.Platform.Common.Data.ggxxDianzigonggaoData) As Boolean

            Dim strBakExt As String = Xydc.Platform.Common.Utilities.PulicParameters.BACKUPFILEEXT
            Dim strTable As String = Xydc.Platform.Common.Data.ggxxDianzigonggaoData.TABLE_DZGG_B_FUJIAN

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objBaseFTP As New Xydc.Platform.Common.Utilities.BaseFTP
            Dim objFlowObject As Xydc.Platform.DataAccess.FlowObject
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
                Dim strBasePath As String = Me.getBasePath_FJ()
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
                        strOldFile = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_DZGG_B_FUJIAN_WJWZ), "")
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
                                                strNewWJWZ = objPulicParameters.getObjectValue(.DefaultView.Item(j).Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_DZGG_B_FUJIAN_WJWZ), "")
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
            Xydc.Platform.DataAccess.FlowObject.SafeRelease(objFlowObject)

            doRestoreFiles_FJ = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)
            Xydc.Platform.DataAccess.FlowObject.SafeRelease(objFlowObject)
            Exit Function

        End Function


        '----------------------------------------------------------------
        ' �ڸ�������������ɾ��������_B_������������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objOldData           ��������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doDeleteData_FJ( _
            ByRef strErrMsg As String, _
            ByVal objOldData As System.Data.DataRow) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile

            '��ʼ��
            doDeleteData_FJ = False
            strErrMsg = ""

            Try
                '���
                If objOldData Is Nothing Then
                    strErrMsg = "����δ����Ҫɾ�������ݣ�"
                    GoTo errProc
                End If

                '������ʱ�ļ�
                Dim strTempFile As String = ""
                strTempFile = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_DZGG_B_FUJIAN_BDWJ), "")

                'ɾ������
                objOldData.Delete()

                'ɾ����ʱ�ļ�
                If strTempFile <> "" Then
                    If objBaseLocalFile.doDeleteFile(strErrMsg, strTempFile) = False Then
                        '�γ������ļ�
                    End If
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)

            '����
            doDeleteData_FJ = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �ڸ��������������Զ�������ʾ���=���ݼ��е������+1
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objFJData            ����������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doAutoAdjustXSXH_FJ( _
            ByRef strErrMsg As String, _
            ByRef objFJData As Xydc.Platform.Common.Data.ggxxDianzigonggaoData) As Boolean

            '��ʼ��
            doAutoAdjustXSXH_FJ = False
            strErrMsg = ""

            Try
                '���
                If objFJData Is Nothing Then
                    strErrMsg = "����δ�����ļ����ݣ�"
                    GoTo errProc
                End If

                '�Զ��������
                Dim strField As String = Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_DZGG_B_FUJIAN_XSXH
                Dim objTemp As Object
                Dim intCount As Integer
                Dim i As Integer
                With objFJData.Tables(Xydc.Platform.Common.Data.ggxxDianzigonggaoData.TABLE_DZGG_B_FUJIAN)
                    intCount = .DefaultView.Count
                    For i = 0 To intCount - 1 Step 1
                        .DefaultView.Item(i).Item(strField) = i + 1
                    Next
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            '����
            doAutoAdjustXSXH_FJ = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �ڸ������������н�ָ����objSrcData�ƶ���ָ����objDesData
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objSrcData           ��Ҫ�ƶ�������
        '     objDesData           ��Ҫ�ƶ���������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doMoveTo_FJ( _
            ByRef strErrMsg As String, _
            ByRef objSrcData As System.Data.DataRow, _
            ByRef objDesData As System.Data.DataRow) As Boolean

            '��ʼ��
            doMoveTo_FJ = False
            strErrMsg = ""

            Try
                '���
                If objSrcData Is Nothing Then
                    strErrMsg = "����δ����Ҫ�ƶ������ݣ�"
                    GoTo errProc
                End If
                If objDesData Is Nothing Then
                    strErrMsg = "����δ����Ҫ�ƶ��������ݣ�"
                    GoTo errProc
                End If

                '�ƶ�
                Dim strField As String = Xydc.Platform.Common.Data.ggxxDianzigonggaoData.FIELD_DZGG_B_FUJIAN_XSXH
                Dim objTemp As Object
                objTemp = objSrcData.Item(strField)
                objSrcData.Item(strField) = objDesData.Item(strField)
                objDesData.Item(strField) = objTemp

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            '����
            doMoveTo_FJ = True
            Exit Function

errProc:
            Exit Function

        End Function

    End Class

End Namespace
