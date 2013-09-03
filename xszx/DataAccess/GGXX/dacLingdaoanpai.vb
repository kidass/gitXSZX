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
    ' ����    ��dacLingdaoanpai
    '
    ' ����������
    '     �ṩ�ԡ��쵼����š�ģ���漰�����ݲ����
    '----------------------------------------------------------------

    Public Class dacLingdaoanpai
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.DataAccess.dacLingdaoanpai)
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
        ' ��ȡ�쵼��������ݣ�������������- �б���ʾģʽ
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     strUserId                   ���û���ʶ
        '     strPassword                 ���û�����
        '     strWhere                    �������ַ���
        '     objLingdaoanpaiData         ����Ϣ���ݼ�
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objLingdaoanpaiData As Xydc.Platform.Common.Data.ggxxLingdaoanpaiData) As Boolean

            Dim objTempLingdaoanpaiData As Xydc.Platform.Common.Data.ggxxLingdaoanpaiData
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '��ʼ��
            getDataSet = False
            objLingdaoanpaiData = Nothing
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
                    objTempLingdaoanpaiData = New Xydc.Platform.Common.Data.ggxxLingdaoanpaiData(Xydc.Platform.Common.Data.ggxxLingdaoanpaiData.enumTableType.GR_B_LINGDAOHUODONGANPAI)

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
                        strSQL = strSQL + "   select *," + vbCr
                        strSQL = strSQL + "     ���� = datename(dw,����)"
                        strSQL = strSQL + "   from ����_B_�쵼�����" + vbCr
                        strSQL = strSQL + " ) a" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.����,a.����" + vbCr

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        'ִ�в���
                        .Fill(objTempLingdaoanpaiData.Tables(Xydc.Platform.Common.Data.ggxxLingdaoanpaiData.TABLE_GR_B_LINGDAOHUODONGANPAI))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempLingdaoanpaiData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.ggxxLingdaoanpaiData.SafeRelease(objTempLingdaoanpaiData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objLingdaoanpaiData = objTempLingdaoanpaiData
            getDataSet = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.ggxxLingdaoanpaiData.SafeRelease(objTempLingdaoanpaiData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function


        '----------------------------------------------------------------
        ' ��ȡ�쵼��������ݣ�������������- �б���ʾģʽ
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     strUserId                   ���û���ʶ
        '     strPassword                 ���û�����
        '     strWhere                    �������ַ���
        '     objLingdaoanpaiData         ����Ϣ���ݼ�
        '     blnNone                     :������
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objLingdaoanpaiData As Xydc.Platform.Common.Data.ggxxLingdaoanpaiData, _
            ByVal blnNone As Boolean) As Boolean

            Dim objTempLingdaoanpaiData As Xydc.Platform.Common.Data.ggxxLingdaoanpaiData
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '��ʼ��
            getDataSet = False
            objLingdaoanpaiData = Nothing
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
                    objTempLingdaoanpaiData = New Xydc.Platform.Common.Data.ggxxLingdaoanpaiData(Xydc.Platform.Common.Data.ggxxLingdaoanpaiData.enumTableType.GR_B_LINGDAOHUODONGANPAI)

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
                        strSQL = strSQL + " select a.���,�ճ� = convert(char(10),a.����,120) + '       (' + a.���� + ')',a.ʱ��,a.����,a.�ص�,a.�μ��쵼,a.�����,a.����,a.��ע,a.��ʼʱ��,a.����ʱ�� " + vbCr
                        strSQL = strSQL + " from" + vbCr
                        strSQL = strSQL + " (" + vbCr
                        strSQL = strSQL + "   select *," + vbCr
                        strSQL = strSQL + "     ���� = datename(dw,����)"
                        strSQL = strSQL + "   from ����_B_�쵼�����" + vbCr
                        strSQL = strSQL + " ) a" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.����,a.����" + vbCr

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        'ִ�в���
                        .Fill(objTempLingdaoanpaiData.Tables(Xydc.Platform.Common.Data.ggxxLingdaoanpaiData.TABLE_GR_B_LINGDAOHUODONGANPAI))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempLingdaoanpaiData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.ggxxLingdaoanpaiData.SafeRelease(objTempLingdaoanpaiData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objLingdaoanpaiData = objTempLingdaoanpaiData
            getDataSet = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.ggxxLingdaoanpaiData.SafeRelease(objTempLingdaoanpaiData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ[���=intXH]���쵼���������
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     strUserId                   ���û���ʶ
        '     strPassword                 ���û�����
        '     intXH                       ���������
        '     objLingdaoanpaiData        ����Ϣ���ݼ�
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intXH As Integer, _
            ByRef objLingdaoanpaiData As Xydc.Platform.Common.Data.ggxxLingdaoanpaiData) As Boolean

            Dim objTempLingdaoanpaiData As Xydc.Platform.Common.Data.ggxxLingdaoanpaiData
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '��ʼ��
            getDataSet = False
            objLingdaoanpaiData = Nothing
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
                    objTempLingdaoanpaiData = New Xydc.Platform.Common.Data.ggxxLingdaoanpaiData(Xydc.Platform.Common.Data.ggxxLingdaoanpaiData.enumTableType.GR_B_LINGDAOHUODONGANPAI)

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
                        strSQL = strSQL + "   select *," + vbCr
                        strSQL = strSQL + "     ���� = datename(dw,����)"
                        strSQL = strSQL + "   from ����_B_�쵼�����" + vbCr
                        strSQL = strSQL + "   where ��� = @xh" + vbCr
                        strSQL = strSQL + " ) a" + vbCr

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@xh", intXH)
                        .SelectCommand = objSqlCommand

                        'ִ�в���
                        .Fill(objTempLingdaoanpaiData.Tables(Xydc.Platform.Common.Data.ggxxLingdaoanpaiData.TABLE_GR_B_LINGDAOHUODONGANPAI))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempLingdaoanpaiData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.ggxxLingdaoanpaiData.SafeRelease(objTempLingdaoanpaiData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objLingdaoanpaiData = objTempLingdaoanpaiData
            getDataSet = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.ggxxLingdaoanpaiData.SafeRelease(objTempLingdaoanpaiData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ�쵼��������ݣ�������֯���롱+����������
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     strUserId                   ���û���ʶ
        '     strPassword                 ���û�����
        '     objDate                     ��ָ������
        '     objLingdaoanpaiData         ����Ϣ���ݼ�
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objDate As System.DateTime, _
            ByRef objLingdaoanpaiData As Xydc.Platform.Common.Data.ggxxLingdaoanpaiData) As Boolean

            Dim objTempLingdaoanpaiData As Xydc.Platform.Common.Data.ggxxLingdaoanpaiData
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '��ʼ��
            getDataSet = False
            objLingdaoanpaiData = Nothing
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
                    objTempLingdaoanpaiData = New Xydc.Platform.Common.Data.ggxxLingdaoanpaiData(Xydc.Platform.Common.Data.ggxxLingdaoanpaiData.enumTableType.GR_B_LINGDAOHUODONGANPAI_DAYIN01)

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
                        strSQL = strSQL + "   select" + vbCr
                        strSQL = strSQL + "     ���� = @rq," + vbCr
                        strSQL = strSQL + "     ���� = datename(dw,@rq)," + vbCr
                        strSQL = strSQL + "     a.�μ��쵼," + vbCr
                        strSQL = strSQL + "     ��֯���� = b.��֯����," + vbCr
                        strSQL = strSQL + "     ���� = cast(b.��Ա��� as integer)," + vbCr
                        strSQL = strSQL + "     ���� = dbo.Ggxx_GetLdap(a.�μ��쵼, @rq, '����', @fgf)," + vbCr
                        strSQL = strSQL + "     ���� = dbo.Ggxx_GetLdap(a.�μ��쵼, @rq, '����', @fgf) " + vbCr
                        strSQL = strSQL + "   from" + vbCr
                        strSQL = strSQL + "   (" + vbCr
                        strSQL = strSQL + "     select �μ��쵼" + vbCr
                        strSQL = strSQL + "     from ����_B_�쵼�����" + vbCr
                        strSQL = strSQL + "     where ���� = @rq" + vbCr
                        strSQL = strSQL + "     group by �μ��쵼"
                        strSQL = strSQL + "   ) a" + vbCr
                        strSQL = strSQL + "   left join ����_B_��Ա b on a.�μ��쵼 = b.��Ա����" + vbCr
                        strSQL = strSQL + " ) a" + vbCr
                        strSQL = strSQL + " order by a.��֯����,a.����" + vbCr

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@rq", objDate)
                        objSqlCommand.Parameters.AddWithValue("@fgf", Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate)
                        .SelectCommand = objSqlCommand

                        'ִ�в���
                        .Fill(objTempLingdaoanpaiData.Tables(Xydc.Platform.Common.Data.ggxxLingdaoanpaiData.TABLE_GR_B_LINGDAOHUODONGANPAI_DAYIN01))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempLingdaoanpaiData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.ggxxLingdaoanpaiData.SafeRelease(objTempLingdaoanpaiData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objLingdaoanpaiData = objTempLingdaoanpaiData
            getDataSet = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.ggxxLingdaoanpaiData.SafeRelease(objTempLingdaoanpaiData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function






        '----------------------------------------------------------------
        ' ɾ���쵼�����
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     intXH                ���������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doDelete( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intXH As Integer) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

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
                If intXH <= 0 Then
                    strErrMsg = "����δָ��[���]��"
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

                    'ɾ��������_B_�쵼����š���Ϣ
                    strSQL = ""
                    strSQL = strSQL + " delete from ����_B_�쵼����� " + vbCr
                    strSQL = strSQL + " where ��� = @xh" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@xh", intXH)
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
            doDelete = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �����쵼����ţ���[strFromRQ]���Ƶ�[strToRQ]
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strFromRQ            ��Ҫ���Ƶİ�������
        '     strToRQ              �����Ƶ��İ�������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doCopy( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strFromRQ As String, _
            ByVal strToRQ As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '��ʼ��
            doCopy = False
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

                If strFromRQ Is Nothing Then strFromRQ = ""
                strFromRQ = strFromRQ.Trim
                If strFromRQ = "" Then
                    strErrMsg = "����δָ��Ҫ����[��������]��"
                    GoTo errProc
                End If
                If objPulicParameters.isDatetimeString(strFromRQ) = False Then
                    strErrMsg = "����[" + strFromRQ + "]����Ч�����ڣ�"
                    GoTo errProc
                End If

                If strToRQ Is Nothing Then strToRQ = ""
                strToRQ = strToRQ.Trim
                If strToRQ = "" Then
                    strErrMsg = "����δָ�����Ƶ���[��������]��"
                    GoTo errProc
                End If
                If objPulicParameters.isDatetimeString(strToRQ) = False Then
                    strErrMsg = "����[" + strToRQ + "]����Ч�����ڣ�"
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
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

                    strSQL = ""
                    strSQL = strSQL + " insert into ����_B_�쵼����� (" + vbCr
                    strSQL = strSQL + "   ����,ʱ��,��ʼʱ��,����ʱ��,�ص�,�μ��쵼,�����,����,��ע" + vbCr
                    strSQL = strSQL + " )" + vbCr
                    strSQL = strSQL + " select " + vbCr
                    strSQL = strSQL + "   ���� = @torq," + vbCr
                    strSQL = strSQL + "   ʱ��," + vbCr
                    strSQL = strSQL + "   ��ʼʱ�� = @torq + ' ' + DATENAME(hh, ��ʼʱ��) + ':' + DATENAME(mi, ��ʼʱ��) + ':' + DATENAME(ss, ��ʼʱ��)," + vbCr
                    strSQL = strSQL + "   ����ʱ�� = @torq + ' ' + DATENAME(hh, ����ʱ��) + ':' + DATENAME(mi, ����ʱ��) + ':' + DATENAME(ss, ����ʱ��)," + vbCr
                    strSQL = strSQL + "   �ص�,�μ��쵼,�����,����,��ע" + vbCr
                    strSQL = strSQL + " from ����_B_�쵼�����" + vbCr
                    strSQL = strSQL + " where ���� = @fromrq" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@fromrq", strFromRQ)
                    objSqlCommand.Parameters.AddWithValue("@torq", strToRQ)
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
            doCopy = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��顰����_B_�쵼����š������ݵĺϷ���
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
                strSQL = "select top 0 * from ����_B_�쵼�����"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, strUserId, strPassword, strSQL, "����_B_�쵼�����", objDataSet) = False Then
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
                        Case Xydc.Platform.Common.Data.ggxxLingdaoanpaiData.FIELD_GR_B_LINGDAOHUODONGANPAI_XQ
                            '������
                        Case Xydc.Platform.Common.Data.ggxxLingdaoanpaiData.FIELD_GR_B_LINGDAOHUODONGANPAI_XH
                            '�Զ���

                        Case Xydc.Platform.Common.Data.ggxxLingdaoanpaiData.FIELD_GR_B_LINGDAOHUODONGANPAI_RQ
                            If strValue = "" Then
                                strValue = Format(Now, "yyyy-MM-dd")
                            End If
                            If objPulicParameters.isDatetimeString(strValue) = False Then
                                strErrMsg = "����[" + strField + "]������Ч�����ڣ�"
                                GoTo errProc
                            End If
                            strValue = Format(CType(strValue, System.DateTime), "yyyy-MM-dd")

                        Case Xydc.Platform.Common.Data.ggxxLingdaoanpaiData.FIELD_GR_B_LINGDAOHUODONGANPAI_CJLD
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

                        Case Xydc.Platform.Common.Data.ggxxLingdaoanpaiData.FIELD_GR_B_LINGDAOHUODONGANPAI_PX
                            If strValue = "" Then
                                strErrMsg = "����[" + strField + "]����Ϊ�գ�"
                                GoTo errProc
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

                'У�顰���ڡ�+������
                Dim strRQ As String = objNewData(Xydc.Platform.Common.Data.ggxxLingdaoanpaiData.FIELD_GR_B_LINGDAOHUODONGANPAI_RQ)
                Dim strPX As String = objNewData(Xydc.Platform.Common.Data.ggxxLingdaoanpaiData.FIELD_GR_B_LINGDAOHUODONGANPAI_PX)
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew, _
                        Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eCpyNew
                        strSQL = ""
                        strSQL = strSQL + " select * from ����_B_�쵼�����" + vbCr
                        strSQL = strSQL + " where ���� = '" + strRQ + "'" + vbCr
                        strSQL = strSQL + " and   ���� =  " + strPX + vbCr
                    Case Else
                        Dim intOldXH As Integer
                        intOldXH = objPulicParameters.getObjectValue(objOldData(Xydc.Platform.Common.Data.ggxxLingdaoanpaiData.FIELD_GR_B_LINGDAOHUODONGANPAI_XH), 0)
                        strSQL = ""
                        strSQL = strSQL + " select * from ����_B_�쵼�����" + vbCr
                        strSQL = strSQL + " where ���� = '" + strRQ + "'" + vbCr
                        strSQL = strSQL + " and   ���� =  " + strPX + vbCr
                        strSQL = strSQL + " and   ��� <> " + intOldXH.ToString + vbCr
                End Select
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    '�Զ���ȡ���
                    If objdacCommon.getNewCode(strErrMsg, objSqlConnection, "����", "����", strRQ, "����_B_�쵼�����", True, strPX) = False Then
                        GoTo errProc
                    End If
                    objNewData(Xydc.Platform.Common.Data.ggxxLingdaoanpaiData.FIELD_GR_B_LINGDAOHUODONGANPAI_PX) = strPX
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
        ' ���桰����_B_�쵼����š�������(��������)
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
                                    Case Xydc.Platform.Common.Data.ggxxLingdaoanpaiData.FIELD_GR_B_LINGDAOHUODONGANPAI_XQ
                                        '������
                                    Case Xydc.Platform.Common.Data.ggxxLingdaoanpaiData.FIELD_GR_B_LINGDAOHUODONGANPAI_XH
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
                            strSQL = strSQL + " insert into ����_B_�쵼����� (" + strFileds + ")"
                            strSQL = strSQL + " values (" + strValues + ")"
                            '׼������
                            objSqlCommand.Parameters.Clear()
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.ggxxLingdaoanpaiData.FIELD_GR_B_LINGDAOHUODONGANPAI_XQ
                                        '������
                                    Case Xydc.Platform.Common.Data.ggxxLingdaoanpaiData.FIELD_GR_B_LINGDAOHUODONGANPAI_XH
                                        '�Զ���
                                    Case Xydc.Platform.Common.Data.ggxxLingdaoanpaiData.FIELD_GR_B_LINGDAOHUODONGANPAI_RQ
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), System.DBNull.Value)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(objNewData.Item(i), System.DateTime))
                                        End If
                                    Case Xydc.Platform.Common.Data.ggxxLingdaoanpaiData.FIELD_GR_B_LINGDAOHUODONGANPAI_PX
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
                            Dim intOldXH As Integer
                            intOldXH = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.ggxxLingdaoanpaiData.FIELD_GR_B_LINGDAOHUODONGANPAI_XH), 0)
                            '��������ֶ��б�
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.ggxxLingdaoanpaiData.FIELD_GR_B_LINGDAOHUODONGANPAI_XQ
                                        '������
                                    Case Xydc.Platform.Common.Data.ggxxLingdaoanpaiData.FIELD_GR_B_LINGDAOHUODONGANPAI_XH
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
                            strSQL = strSQL + " update ����_B_�쵼����� set " + vbCr
                            strSQL = strSQL + "   " + strFileds + vbCr
                            strSQL = strSQL + " where ��� = @oldxh" + vbCr
                            '׼������
                            objSqlCommand.Parameters.Clear()
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.ggxxLingdaoanpaiData.FIELD_GR_B_LINGDAOHUODONGANPAI_XQ
                                        '������
                                    Case Xydc.Platform.Common.Data.ggxxLingdaoanpaiData.FIELD_GR_B_LINGDAOHUODONGANPAI_XH
                                        '�Զ���
                                    Case Xydc.Platform.Common.Data.ggxxLingdaoanpaiData.FIELD_GR_B_LINGDAOHUODONGANPAI_RQ

                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), System.DBNull.Value)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(objNewData.Item(i), System.DateTime))
                                        End If
                                    Case Xydc.Platform.Common.Data.ggxxLingdaoanpaiData.FIELD_GR_B_LINGDAOHUODONGANPAI_PX
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
        ' �����쵼��������ݼ�¼(�����������)
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
        Public Function doSave( _
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
                    '��������¼
                    If Me.doSave(strErrMsg, objSqlTransaction, objOldData, objNewData, objenumEditType) = False Then
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
        ' ��ȡ�µ�����
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strRQ                ��ָ������
        '     strNewPX             ��(����)������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getNewPX( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRQ As String, _
            ByRef strNewPX As String) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim strSQL As String

            '��ʼ��
            getNewPX = False
            strNewPX = ""
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim()
                If strUserId = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim()
                If strRQ Is Nothing Then strRQ = ""
                strRQ = strRQ.Trim
                If strRQ = "" Then
                    strErrMsg = "����û��ָ��[��������]��"
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getNewCode(strErrMsg, objSqlConnection, "����", "����", strRQ, "����_B_�쵼�����", True, strNewPX) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            getNewPX = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �����쵼����ŵ�����
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     intSrcIndex          ��ԭ����
        '     intDesIndex          ��Ŀ������
        '     intSrcXH             ��ԭ���
        '     intDesXh             ��Ŀ�����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��

        '----------------------------------------------------------------
        Public Function doUpdatePX( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intSrcIndex As Integer, _
            ByVal intDesIndex As Integer, _
            ByVal intSrcXH As Integer, _
            ByVal intDesXH As Integer) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '��ʼ��
            doUpdatePX = False
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
                If intSrcXH <= 0 Then
                    strErrMsg = "����δָ��[ԭ���]��"
                    GoTo errProc
                End If
                If intDesXH <= 0 Then
                    strErrMsg = "����δָ��[Ŀ�����]��"
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
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

                    '���¡�����_B_�쵼����š���Ϣ
                    strSQL = ""
                    strSQL = strSQL + " update ����_B_�쵼����� set ����=@px" + vbCr
                    strSQL = strSQL + " where ��� = @xh" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@px", intDesIndex)
                    objSqlCommand.Parameters.AddWithValue("@xh", intSrcXH)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    strSQL = ""
                    strSQL = strSQL + " update ����_B_�쵼����� set ����=@px" + vbCr
                    strSQL = strSQL + " where ��� = @xh" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@px", intSrcIndex)
                    objSqlCommand.Parameters.AddWithValue("@xh", intDesXH)
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
            doUpdatePX = True
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
