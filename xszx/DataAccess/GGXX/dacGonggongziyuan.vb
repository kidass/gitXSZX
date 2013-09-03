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
Imports System.Web
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient

Imports Xydc.Platform.Common
Imports Xydc.Platform.Common.Data
Imports Xydc.Platform.SystemFramework

Namespace Xydc.Platform.DataAccess

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.DataAccess
    ' ����    ��dacGonggongziyuan
    '
    ' ����������
    '     �ṩ�ԡ�������Դ���漰�����ݲ����
    '----------------------------------------------------------------

    Public Class dacGonggongziyuan
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.DataAccess.dacGonggongziyuan)
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
        ' ��ȡ����Ϣ_B_������Դ_��Ŀ�������ݼ�(�ԡ���Ŀ���롱��������)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strWhere             �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     objLanmuData         ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getLanmuData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objLanmuData As Xydc.Platform.Common.Data.ggxxGonggongziyuanData) As Boolean

            Dim objTempLanmuData As Xydc.Platform.Common.Data.ggxxGonggongziyuanData
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '��ʼ��
            getLanmuData = False
            objLanmuData = Nothing
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
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim()

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ����
                Try
                    '�������ݼ�
                    objTempLanmuData = New Xydc.Platform.Common.Data.ggxxGonggongziyuanData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.enumTableType.XX_B_GONGGONGZIYUAN_LANMU)

                    '����SqlCommand
                    objSqlCommand = New System.Data.SqlClient.SqlCommand
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ�м���
                    With Me.m_objSqlDataAdapter
                        '׼��SQL
                        strSQL = ""
                        strSQL = strSQL + " select a.* " + vbCr
                        strSQL = strSQL + " from ��Ϣ_B_������Դ_��Ŀ a " + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.��Ŀ���� " + vbCr

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        .SelectCommand = objSqlCommand

                        'ִ�в���
                        .Fill(objTempLanmuData.Tables(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.TABLE_XX_B_GONGGONGZIYUAN_LANMU))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempLanmuData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.ggxxGonggongziyuanData.SafeRelease(objTempLanmuData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objLanmuData = objTempLanmuData
            getLanmuData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.ggxxGonggongziyuanData.SafeRelease(objTempLanmuData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡָ��strLMDM�¼��ġ���Ϣ_B_������Դ_��Ŀ�������ݼ�(�ԡ���Ŀ���롱��������)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strLMDM              ����Ŀ����
        '     strWhere             �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     objLanmuData         ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getLanmuData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strLMDM As String, _
            ByVal strWhere As String, _
            ByRef objLanmuData As Xydc.Platform.Common.Data.ggxxGonggongziyuanData) As Boolean

            Dim objTempLanmuData As Xydc.Platform.Common.Data.ggxxGonggongziyuanData
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '��ʼ��
            getLanmuData = False
            objLanmuData = Nothing
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
                If strLMDM Is Nothing Then strLMDM = ""
                strLMDM = strLMDM.Trim()
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim()

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ����
                Try
                    '�������ݼ�
                    objTempLanmuData = New Xydc.Platform.Common.Data.ggxxGonggongziyuanData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.enumTableType.XX_B_GONGGONGZIYUAN_LANMU)

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
                        strSQL = strSQL + " from ��Ϣ_B_������Դ_��Ŀ a " + vbCr
                        strSQL = strSQL + " where (a.��Ŀ���� like @lmdm + '" + strSep + "%' or a.��Ŀ���� = @lmdm)" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " and " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.��Ŀ���� " + vbCr

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@lmdm", strLMDM)
                        .SelectCommand = objSqlCommand

                        'ִ�в���
                        .Fill(objTempLanmuData.Tables(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.TABLE_XX_B_GONGGONGZIYUAN_LANMU))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempLanmuData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.ggxxGonggongziyuanData.SafeRelease(objTempLanmuData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objLanmuData = objTempLanmuData
            getLanmuData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.ggxxGonggongziyuanData.SafeRelease(objTempLanmuData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ����ָ��strLMDM��ȡ����Ϣ_B_������Դ_��Ŀ�������ݼ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strLMDM              ����Ŀ����
        '     blnUnused            ��������
        '     objLanmuData         ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getLanmuData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strLMDM As String, _
            ByVal blnUnused As Boolean, _
            ByRef objLanmuData As Xydc.Platform.Common.Data.ggxxGonggongziyuanData) As Boolean

            Dim objTempLanmuData As Xydc.Platform.Common.Data.ggxxGonggongziyuanData
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '��ʼ��
            getLanmuData = False
            objLanmuData = Nothing
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
                If strLMDM Is Nothing Then strLMDM = ""
                strLMDM = strLMDM.Trim()

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ����
                Try
                    '�������ݼ�
                    objTempLanmuData = New Xydc.Platform.Common.Data.ggxxGonggongziyuanData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.enumTableType.XX_B_GONGGONGZIYUAN_LANMU)

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
                        strSQL = strSQL + " from ��Ϣ_B_������Դ_��Ŀ a " + vbCr
                        strSQL = strSQL + " where a.��Ŀ���� = @lmdm" + vbCr
                        strSQL = strSQL + " order by a.��Ŀ���� " + vbCr

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@lmdm", strLMDM)
                        .SelectCommand = objSqlCommand

                        'ִ�в���
                        .Fill(objTempLanmuData.Tables(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.TABLE_XX_B_GONGGONGZIYUAN_LANMU))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempLanmuData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.ggxxGonggongziyuanData.SafeRelease(objTempLanmuData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objLanmuData = objTempLanmuData
            getLanmuData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.ggxxGonggongziyuanData.SafeRelease(objTempLanmuData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ����ָ��intMKBS��ȡ����Ϣ_B_������Դ_��Ŀ�������ݼ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     intMKBS              ����Ŀ��ʶ
        '     blnUnused            ��������
        '     objLanmuData         ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getLanmuData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intMKBS As Integer, _
            ByVal blnUnused As Boolean, _
            ByRef objLanmuData As Xydc.Platform.Common.Data.ggxxGonggongziyuanData) As Boolean

            Dim objTempLanmuData As Xydc.Platform.Common.Data.ggxxGonggongziyuanData
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            '��ʼ��
            getLanmuData = False
            objLanmuData = Nothing
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

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ����
                Dim strSQL As String
                Try
                    '�������ݼ�
                    objTempLanmuData = New Xydc.Platform.Common.Data.ggxxGonggongziyuanData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.enumTableType.XX_B_GONGGONGZIYUAN_LANMU)

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
                        strSQL = strSQL + " from ��Ϣ_B_������Դ_��Ŀ a " + vbCr
                        strSQL = strSQL + " where a.��Ŀ��ʶ = @mkbs" + vbCr
                        strSQL = strSQL + " order by a.��Ŀ���� " + vbCr

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@mkbs", intMKBS)
                        .SelectCommand = objSqlCommand

                        'ִ�в���
                        .Fill(objTempLanmuData.Tables(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.TABLE_XX_B_GONGGONGZIYUAN_LANMU))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempLanmuData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.ggxxGonggongziyuanData.SafeRelease(objTempLanmuData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objLanmuData = objTempLanmuData
            getLanmuData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.ggxxGonggongziyuanData.SafeRelease(objTempLanmuData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �����ϼ���Ŀ�����ȡ�¼�����Ŀ����
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strPrevLMDM          ���ϼ���Ŀ����
        '     strNewLMDM           ������Ŀ����(����)
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getNewLMDM( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strPrevLMDM As String, _
            ByRef strNewLMDM As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            '��ʼ��
            getNewLMDM = False
            strNewLMDM = ""
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim()
                If strUserId.Trim = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim()
                If strPrevLMDM Is Nothing Then strPrevLMDM = ""
                strPrevLMDM = strPrevLMDM.Trim()

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ�ϼ���Ŀ����
                Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharFjdmSeparate
                Dim intLevel As Integer = objPulicParameters.getCodeLevel(strPrevLMDM, strSep)
                If intLevel < 0 Then
                    intLevel = 0
                End If

                '��ȡ����
                strSQL = ""
                strSQL = strSQL + " select max(��������) " + vbCr
                strSQL = strSQL + " from ��Ϣ_B_������Դ_��Ŀ " + vbCr
                strSQL = strSQL + " where ��Ŀ���� = " + (intLevel + 1).ToString() + vbCr         'ֱ���¼�
                If strPrevLMDM <> "" Then
                    strSQL = strSQL + " and ��Ŀ���� like '" + strPrevLMDM + strSep + "%'" + vbCr '�¼�
                End If
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    If strPrevLMDM = "" Then
                        strNewLMDM = "1"
                    Else
                        strNewLMDM = strPrevLMDM + strSep + "1"
                    End If
                Else
                    Dim intValue As Integer
                    With objDataSet.Tables(0).Rows(0)
                        intValue = objPulicParameters.getObjectValue(.Item(0), 0)
                    End With
                    intValue += 1
                    If strPrevLMDM = "" Then
                        strNewLMDM = intValue.ToString()
                    Else
                        strNewLMDM = strPrevLMDM + strSep + intValue.ToString()
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
            getNewLMDM = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ�µ���Ŀ��ʶ
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strNewLMBS           ������Ŀ��ʶ(����)
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getNewLMBS( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef strNewLMBS As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            '��ʼ��
            getNewLMBS = False
            strNewLMBS = ""
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

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getNewCode(strErrMsg, objSqlConnection, "��Ŀ��ʶ", "��Ϣ_B_������Դ_��Ŀ", True, strNewLMBS) = False Then
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
            getNewLMBS = True
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
        Public Function getLanmuDefaultValue( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef objNewData As System.Collections.Specialized.ListDictionary, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            getLanmuDefaultValue = False

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

                '��ȡ��Ŀ��ʶ
                Dim strLMBS As String
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                        If Me.getNewLMBS(strErrMsg, strUserId, strPassword, strLMBS) = False Then
                            GoTo errProc
                        End If
                        objNewData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMBS) = objPulicParameters.getObjectValue(strLMBS, 0)
                    Case Else
                End Select

                '��ȡ��Ŀ����
                Dim strLMDM As String
                strLMDM = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMDM), "")
                strLMDM = strLMDM.Trim()
                If strLMDM = "" Then
                    strErrMsg = "����[��Ŀ����]����Ϊ�գ�"
                    GoTo errProc
                End If
                Dim strTemp As String = strLMDM
                strTemp = strTemp.Replace(Xydc.Platform.Common.Utilities.PulicParameters.CharFjdmSeparate, "")
                If objPulicParameters.isNumericString(strTemp) = False Then
                    strErrMsg = "����[��Ŀ����]�д��ڷǷ��ַ���"
                    GoTo errProc
                End If

                '������Ŀ�����ȡ��Ŀ����
                Dim intLevel As Integer
                intLevel = objPulicParameters.getCodeLevel(strLMDM, Xydc.Platform.Common.Utilities.PulicParameters.CharFjdmSeparate)
                objNewData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMJB) = intLevel

                '������Ŀ�����ȡ��������
                Dim strBJDM As String
                strBJDM = objPulicParameters.getCodeValue(strLMDM, Xydc.Platform.Common.Utilities.PulicParameters.CharFjdmSeparate, intLevel)
                objNewData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_BJDM) = objPulicParameters.getObjectValue(strBJDM, 0)

                '������Ŀ�����ȡ������Ŀ
                Dim objggxxGonggongziyuanData As Xydc.Platform.Common.Data.ggxxGonggongziyuanData
                If intLevel <= 1 Then
                    objNewData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_DJLM) = objNewData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMBS)
                Else
                    Dim strDJLM As String
                    strDJLM = objPulicParameters.getCodeValue(strLMDM, Xydc.Platform.Common.Utilities.PulicParameters.CharFjdmSeparate, 1, True)

                    '���ݶ�����Ŀ�����ȡ������Ŀ��ʶ
                    If Me.getLanmuData(strErrMsg, strUserId, strPassword, strDJLM, True, objggxxGonggongziyuanData) = False Then
                        GoTo errProc
                    End If
                    With objggxxGonggongziyuanData.Tables(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.TABLE_XX_B_GONGGONGZIYUAN_LANMU)
                        If .Rows.Count < 1 Then
                            strErrMsg = "����[" + strDJLM + "]�����ڣ�"
                            GoTo errProc
                        Else
                            objNewData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_DJLM) = .Rows(0).Item(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMBS)
                        End If
                    End With
                    Xydc.Platform.Common.Data.ggxxGonggongziyuanData.SafeRelease(objggxxGonggongziyuanData)
                End If

                '������Ŀ�����ȡ�ϼ���Ŀ
                If intLevel <= 1 Then
                    objNewData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_SJLM) = objNewData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_DJLM)
                Else
                    Dim strSJLM As String
                    strSJLM = objPulicParameters.getCodeValue(strLMDM, Xydc.Platform.Common.Utilities.PulicParameters.CharFjdmSeparate, intLevel - 1, True)

                    '���ݶ�����Ŀ�����ȡ�ϼ���Ŀ��ʶ
                    If Me.getLanmuData(strErrMsg, strUserId, strPassword, strSJLM, True, objggxxGonggongziyuanData) = False Then
                        GoTo errProc
                    End If
                    With objggxxGonggongziyuanData.Tables(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.TABLE_XX_B_GONGGONGZIYUAN_LANMU)
                        If .Rows.Count < 1 Then
                            strErrMsg = "����[" + strSJLM + "]�����ڣ�"
                            GoTo errProc
                        Else
                            objNewData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_SJLM) = .Rows(0).Item(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMBS)
                        End If
                    End With
                    Xydc.Platform.Common.Data.ggxxGonggongziyuanData.SafeRelease(objggxxGonggongziyuanData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            getLanmuDefaultValue = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���ݡ���Ŀ���ơ���ȡ����Ŀ��ʶ��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strLMMC              ����Ŀ����
        '     strLMBS              ��(����)��Ŀ��ʶ
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getLmbsByLmmc( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strLMMC As String, _
            ByRef strLMBS As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            getLmbsByLmmc = False
            strLMBS = ""

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
                If strLMMC Is Nothing Then strLMMC = ""
                strLMMC = strLMMC.Trim

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ��Ϣ
                strSQL = ""
                strSQL = strSQL + " select ��Ŀ��ʶ" + vbCr
                strSQL = strSQL + " from ��Ϣ_B_������Դ_��Ŀ" + vbCr
                strSQL = strSQL + " where ��Ŀ���� = '" + strLMMC + "'" + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If

                '������Ϣ
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strLMBS = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item(0), "")
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getLmbsByLmmc = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���ݡ���Ŀ���ơ���ȡ����Ŀ���롱
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strLMMC              ����Ŀ����
        '     strLMDM              ��(����)��Ŀ����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getLmdmByLmmc( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strLMMC As String, _
            ByRef strLMDM As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            getLmdmByLmmc = False
            strLMDM = ""

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
                If strLMMC Is Nothing Then strLMMC = ""
                strLMMC = strLMMC.Trim

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ��Ϣ
                strSQL = ""
                strSQL = strSQL + " select ��Ŀ����" + vbCr
                strSQL = strSQL + " from ��Ϣ_B_������Դ_��Ŀ" + vbCr
                strSQL = strSQL + " where ��Ŀ���� = '" + strLMMC + "'" + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If

                '������Ϣ
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strLMDM = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item(0), "")
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getLmdmByLmmc = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function





        '----------------------------------------------------------------
        ' ��顰��Ϣ_B_������Դ_��Ŀ�������ݵĺϷ���
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
        Public Function doVerifyLanmuData( _
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

            doVerifyLanmuData = False

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                If strUserId.Trim = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                If objNewData Is Nothing Then
                    strErrMsg = "����δ�����µ����ݣ�"
                    GoTo errProc
                End If
                Dim intOldLMBS As Integer
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                    Case Else
                        If objOldData Is Nothing Then
                            strErrMsg = "����δ����ɵ����ݣ�"
                            GoTo errProc
                        End If
                        intOldLMBS = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMBS), 0)
                End Select

                '��������ֵ���������Զ�ֵ����У�鶥�����ϼ�����
                If Me.getLanmuDefaultValue(strErrMsg, strUserId, strPassword, objNewData, objenumEditType) = False Then
                    GoTo errProc
                End If

                '��ȡ��ṹ����
                strSQL = "select top 0 * from ��Ϣ_B_������Դ_��Ŀ"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, strUserId, strPassword, strSQL, "��Ϣ_B_������Դ_��Ŀ", objDataSet) = False Then
                    GoTo errProc
                End If

                '������ݳ���
                Dim objDictionaryEntry As System.Collections.DictionaryEntry
                Dim strField As String
                Dim intLen As Integer
                For Each objDictionaryEntry In objNewData
                    strField = objPulicParameters.getObjectValue(objDictionaryEntry.Key, "")
                    Select Case strField
                        Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMBS, _
                            Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMJB, _
                            Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_BJDM, _
                            Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_DJLM, _
                            Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_SJLM

                        Case Else
                            Dim strValue As String
                            strValue = objPulicParameters.getObjectValue(objDictionaryEntry.Value, "")
                            If strValue = "" Then
                                Select Case strField
                                    Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMSM
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

                '��飺��Ŀ��ʶ
                Dim intNewLMBS As Integer
                intNewLMBS = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMBS), 0)
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                        strSQL = ""
                        strSQL = strSQL + " select * from ��Ϣ_B_������Դ_��Ŀ "
                        strSQL = strSQL + " where ��Ŀ��ʶ = @newlmbs"
                        objListDictionary.Add("@newlmbs", intNewLMBS)
                    Case Else
                        strSQL = ""
                        strSQL = strSQL + " select * from ��Ϣ_B_������Դ_��Ŀ "
                        strSQL = strSQL + " where ��Ŀ��ʶ =  @newlmbs"
                        strSQL = strSQL + " and   ��Ŀ��ʶ <> @oldlmbs"
                        objListDictionary.Add("@newlmbs", intNewLMBS)
                        objListDictionary.Add("@oldlmbs", intOldLMBS)
                End Select
                If objdacCommon.getDataSetBySQL(strErrMsg, strUserId, strPassword, strSQL, objListDictionary, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strErrMsg = "����[" + intNewLMBS.ToString() + "]�Ѿ����ڣ�"
                    GoTo errProc
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objListDictionary.Clear()

                '��飺��Ŀ����
                Dim strNewLMDM As String
                strNewLMDM = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMDM), "")
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                        strSQL = ""
                        strSQL = strSQL + " select * from ��Ϣ_B_������Դ_��Ŀ "
                        strSQL = strSQL + " where ��Ŀ���� = @newlmdm"
                        objListDictionary.Add("@newlmdm", strNewLMDM)
                    Case Else
                        strSQL = ""
                        strSQL = strSQL + " select * from ��Ϣ_B_������Դ_��Ŀ "
                        strSQL = strSQL + " where ��Ŀ���� =  @newlmdm"
                        strSQL = strSQL + " and   ��Ŀ��ʶ <> @oldlmbs"
                        objListDictionary.Add("@newlmdm", strNewLMDM)
                        objListDictionary.Add("@oldlmbs", intOldLMBS)
                End Select
                If objdacCommon.getDataSetBySQL(strErrMsg, strUserId, strPassword, strSQL, objListDictionary, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strErrMsg = "����[" + strNewLMDM.ToString() + "]�Ѿ����ڣ�"
                    GoTo errProc
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objListDictionary.Clear()

                '��飺��Ŀ����
                Dim strNewLMMC As String
                strNewLMMC = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMMC), "")
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                        strSQL = ""
                        strSQL = strSQL + " select * from ��Ϣ_B_������Դ_��Ŀ "
                        strSQL = strSQL + " where ��Ŀ���� = @newlmmc"
                        objListDictionary.Add("@newlmmc", strNewLMMC)
                    Case Else
                        strSQL = ""
                        strSQL = strSQL + " select * from ��Ϣ_B_������Դ_��Ŀ "
                        strSQL = strSQL + " where ��Ŀ���� =  @newlmmc"
                        strSQL = strSQL + " and   ��Ŀ��ʶ <> @oldlmbs"
                        objListDictionary.Add("@newlmmc", strNewLMMC)
                        objListDictionary.Add("@oldlmbs", intOldLMBS)
                End Select
                If objdacCommon.getDataSetBySQL(strErrMsg, strUserId, strPassword, strSQL, objListDictionary, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strErrMsg = "����[" + strNewLMMC.ToString() + "]�Ѿ����ڣ�"
                    GoTo errProc
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
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

            doVerifyLanmuData = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objListDictionary)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���桰��Ϣ_B_������Դ_��Ŀ��������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     objOldData           ��������
        '     objNewData           ��������(����)
        '     objenumEditType      ���༭����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doSaveLanmuData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByRef objNewData As System.Collections.Specialized.ListDictionary, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '��ʼ��
            doSaveLanmuData = False
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                If strUserId.Trim = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                If objNewData Is Nothing Then
                    strErrMsg = "����δ�����µ����ݣ�"
                    GoTo errProc
                End If
                Dim strOldLMDM As String
                Dim intOldLMBS As Integer
                Dim strNewLMDM As String
                Dim intNewLMBS As Integer
                intNewLMBS = objPulicParameters.getObjectValue(objNewData.Item(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMBS), 0)
                strNewLMDM = objPulicParameters.getObjectValue(objNewData.Item(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMDM), "")
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                    Case Else
                        If objOldData Is Nothing Then
                            strErrMsg = "����δ����ɵ����ݣ�"
                            GoTo errProc
                        End If
                        intOldLMBS = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMBS), 0)
                        strOldLMDM = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMDM), "")
                End Select

                'У��
                If Me.doVerifyLanmuData(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType) = False Then
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
                            strSQL = strSQL + " insert into ��Ϣ_B_������Դ_��Ŀ (" + strFileds + ")"
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
                            strSQL = strSQL + " update ��Ϣ_B_������Դ_��Ŀ set "
                            strSQL = strSQL + "   " + strFileds
                            strSQL = strSQL + " where ��Ŀ��ʶ = @oldlmbs"
                            objSqlCommand.Parameters.Clear()
                            i = 0
                            For Each objDictionaryEntry In objNewData
                                objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), objDictionaryEntry.Value)
                                i += 1
                            Next
                            objSqlCommand.Parameters.AddWithValue("@oldlmbs", intOldLMBS)
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()

                            If strNewLMDM.ToUpper() <> strOldLMDM.ToUpper() Then
                                Dim intOldLMJB As Integer
                                intOldLMJB = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMJB), 0)
                                Dim intNewLMJB As Integer
                                intNewLMJB = objPulicParameters.getObjectValue(objNewData.Item(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMJB), 0)
                                Dim intNewDJLM As Integer
                                intNewDJLM = objPulicParameters.getObjectValue(objNewData.Item(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LANMU_DJLM), 0)

                                '����ԭ�¼��Ĵ���
                                strSQL = ""
                                strSQL = strSQL + " update ��Ϣ_B_������Դ_��Ŀ set "
                                strSQL = strSQL + "   ��Ŀ���� = @newlmdm + substring(��Ŀ����, @oldlmdmlen + 1, len(��Ŀ����) - @oldlmdmlen),"
                                strSQL = strSQL + "   ��Ŀ���� = @newlmjb + ��Ŀ���� - @oldlmjb,"
                                strSQL = strSQL + "   ������Ŀ = @newdjlm "
                                strSQL = strSQL + " where ��Ŀ���� like @oldlmdm + @sep + '%'" '����Ŀ���¼�
                                objSqlCommand.Parameters.Clear()
                                objSqlCommand.Parameters.AddWithValue("@newlmdm", strNewLMDM)
                                objSqlCommand.Parameters.AddWithValue("@oldlmdmlen", strOldLMDM.Length)
                                objSqlCommand.Parameters.AddWithValue("@newlmjb", intNewLMJB)
                                objSqlCommand.Parameters.AddWithValue("@oldlmjb", intOldLMJB)
                                objSqlCommand.Parameters.AddWithValue("@newdjlm", intNewDJLM)
                                objSqlCommand.Parameters.AddWithValue("@newlmbs", intNewLMBS)
                                objSqlCommand.Parameters.AddWithValue("@oldlmdm", strOldLMDM)
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
            doSaveLanmuData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ������Ŀ����ɾ������Ϣ_B_������Դ_��Ŀ��������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strLMDM              ����Ŀ����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doDeleteLanmuData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strLMDM As String) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            '��ʼ��
            doDeleteLanmuData = False
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
                If strLMDM Is Nothing Then strLMDM = ""
                strLMDM = strLMDM.Trim()

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

                    'ɾ����Ϣ_B_������Դ_��Ŀ
                    strSQL = ""
                    strSQL = strSQL + " delete from ��Ϣ_B_������Դ_��Ŀ "
                    strSQL = strSQL + " where ��Ŀ���� like @lmdm + @sep +'%' "
                    strSQL = strSQL + " or    ��Ŀ���� = @lmdm"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@lmdm", strLMDM)
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
            doDeleteLanmuData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function





        '----------------------------------------------------------------
        ' ��ȡ[��Ա����=strCzydm]�Ĺ�����Դ���ݣ������������ڡ����򣩣���
        ' �Ҹ��𷢲��Ĺ�����Դ����
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     strUserId                   ���û���ʶ
        '     strPassword                 ���û�����
        '     strCzydm                    ������Ա��ʶ
        '     strWhere                    �������ַ���
        '     objGonggongziyuanData       ����Ϣ���ݼ�
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
            ByRef objGonggongziyuanData As Xydc.Platform.Common.Data.ggxxGonggongziyuanData) As Boolean

            Dim objTempGonggongziyuanData As Xydc.Platform.Common.Data.ggxxGonggongziyuanData
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon


            Dim objdacAppManager As New Xydc.Platform.DataAccess.dacAppManager
            Dim objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty
            Dim strServerName As String = Xydc.Platform.Common.jsoaConfiguration.DatabaseServerName


            '��ʼ��
            getDataSet = False
            objGonggongziyuanData = Nothing
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


                If objdacAppManager.getServerConnectionProperty(strErrMsg, strUserId, strPassword, strServerName, objConnectionProperty) = False Then
                    GoTo errProc
                End If
                Dim strRoleName As String = Xydc.Platform.Common.jsoaConfiguration.Administrators
                Dim blnRoleName As Boolean
                Dim strWhere_0 As String = "where a.name ='" + strUserId + "'"
                blnRoleName = doVerifyRoleData(strErrMsg, objConnectionProperty, strWhere_0, strRoleName, strUserId, strPassword)

                If strUserId = "sa" Then
                    blnRoleName = True
                End If



                '��ȡ����
                Try
                    '�������ݼ�
                    objTempGonggongziyuanData = New Xydc.Platform.Common.Data.ggxxGonggongziyuanData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.enumTableType.XX_B_GONGGONGZIYUAN)

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
                        strSQL = strSQL + " select distinct a.*" + vbCr
                        strSQL = strSQL + " from" + vbCr
                        strSQL = strSQL + " (" + vbCr
                        strSQL = strSQL + "   select " + vbCr
                        strSQL = strSQL + "     a.��Դ��ʶ,a.��Դ���,a.��������,a.��Ŀ��ʶ,a.��Ա����,a.��֯����,a.��������,"
                        strSQL = strSQL + "     a.��Դ����,a.�ļ�λ��,a.��������,a.������ʶ,a.��������,a.������Χ,��Դ����=''," + vbCr
                        strSQL = strSQL + "     �Ķ����� = case when b.��Ա���� is null then '" + strFalse + "' else '" + strTrue + "' end," + vbCr
                        strSQL = strSQL + "     �������� = case when isnull(a.������ʶ,0) = 0 then '" + strFalse + "' else '" + strTrue + "' end," + vbCr
                        strSQL = strSQL + "     �������� = case when isnull(a.��������,0) = 0 then '" + strFalse + "' else '" + strTrue + "' end," + vbCr
                        strSQL = strSQL + "     c.��Ŀ����,c.��Ŀ����," + vbCr
                        strSQL = strSQL + "     d.��Ա����," + vbCr
                        strSQL = strSQL + "     e.��֯���� " + vbCr
                        strSQL = strSQL + "   from" + vbCr
                        strSQL = strSQL + "   ("
                        strSQL = strSQL + "     select *" + vbCr
                        strSQL = strSQL + "     from ��Ϣ_B_������Դ" + vbCr

                        If blnRoleName = False Then

                            strSQL = strSQL + "     where ��Ա���� = @czydm" + vbCr

                        End If

                        strSQL = strSQL + "   ) a" + vbCr
                        strSQL = strSQL + "   left join ��Ϣ_B_������Դ_��Ŀ c on a.��Ŀ��ʶ = c.��Ŀ��ʶ" + vbCr
                        strSQL = strSQL + "   left join ����_B_��Ա          d on a.��Ա���� = d.��Ա����" + vbCr
                        strSQL = strSQL + "   left join ����_B_��֯����      e on a.��֯���� = e.��֯����" + vbCr
                        strSQL = strSQL + "   left join " + vbCr
                        strSQL = strSQL + "   (" + vbCr
                        strSQL = strSQL + "     select *" + vbCr
                        strSQL = strSQL + "     from ��Ϣ_B_������Դ_�Ķ����" + vbCr

                        If blnRoleName = False Then

                            strSQL = strSQL + "     where ��Ա���� = @ydry" + vbCr

                        End If

                        strSQL = strSQL + "   ) b on a.��Դ��ʶ = b.��Դ��ʶ" + vbCr
                        strSQL = strSQL + " ) a" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.�������� desc " + vbCr

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@czydm", strCzydm)
                        objSqlCommand.Parameters.AddWithValue("@ydry", strCzydm)
                        .SelectCommand = objSqlCommand

                        'ִ�в���
                        .Fill(objTempGonggongziyuanData.Tables(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.TABLE_XX_B_GONGGONGZIYUAN))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempGonggongziyuanData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.ggxxGonggongziyuanData.SafeRelease(objTempGonggongziyuanData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            Xydc.Platform.DataAccess.dacAppManager.SafeRelease(objdacAppManager)
            Xydc.Platform.Common.Utilities.ConnectionProperty.SafeRelease(objConnectionProperty)


            '����
            objGonggongziyuanData = objTempGonggongziyuanData
            getDataSet = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.ggxxGonggongziyuanData.SafeRelease(objTempGonggongziyuanData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            Xydc.Platform.DataAccess.dacAppManager.SafeRelease(objdacAppManager)
            Xydc.Platform.Common.Utilities.ConnectionProperty.SafeRelease(objConnectionProperty)

            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ[��Դ��ʶ=strZYBS]�Ĺ�����Դ����
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     strUserId                   ���û���ʶ
        '     strPassword                 ���û�����
        '     strZYBS                     ����Դ��ʶ
        '     objGonggongziyuanData       ����Ϣ���ݼ�
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZYBS As String, _
            ByRef objGonggongziyuanData As Xydc.Platform.Common.Data.ggxxGonggongziyuanData) As Boolean

            Dim objTempGonggongziyuanData As Xydc.Platform.Common.Data.ggxxGonggongziyuanData
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '��ʼ��
            getDataSet = False
            objGonggongziyuanData = Nothing
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
                If strZYBS Is Nothing Then strZYBS = ""
                strZYBS = strZYBS.Trim

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ����
                Try
                    '�������ݼ�
                    objTempGonggongziyuanData = New Xydc.Platform.Common.Data.ggxxGonggongziyuanData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.enumTableType.XX_B_GONGGONGZIYUAN)

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
                        strSQL = strSQL + "     �Ķ����� = case when b.��Ա���� is null then '" + strFalse + "' else '" + strTrue + "' end," + vbCr
                        strSQL = strSQL + "     �������� = case when isnull(a.������ʶ,0) = 0 then '" + strFalse + "' else '" + strTrue + "' end," + vbCr
                        strSQL = strSQL + "     �������� = case when isnull(a.��������,0) = 0 then '" + strFalse + "' else '" + strTrue + "' end," + vbCr
                        strSQL = strSQL + "     c.��Ŀ����,c.��Ŀ����," + vbCr
                        strSQL = strSQL + "     d.��Ա����," + vbCr
                        strSQL = strSQL + "     e.��֯���� " + vbCr
                        strSQL = strSQL + "   from" + vbCr
                        strSQL = strSQL + "   ("
                        strSQL = strSQL + "     select *" + vbCr
                        strSQL = strSQL + "     from ��Ϣ_B_������Դ" + vbCr
                        strSQL = strSQL + "     where ��Դ��ʶ = @zybs" + vbCr
                        strSQL = strSQL + "   ) a" + vbCr
                        strSQL = strSQL + "   left join ��Ϣ_B_������Դ_��Ŀ c on a.��Ŀ��ʶ = c.��Ŀ��ʶ" + vbCr
                        strSQL = strSQL + "   left join ����_B_��Ա          d on a.��Ա���� = d.��Ա����" + vbCr
                        strSQL = strSQL + "   left join ����_B_��֯����      e on a.��֯���� = e.��֯����" + vbCr
                        strSQL = strSQL + "   left join " + vbCr
                        strSQL = strSQL + "   (" + vbCr
                        strSQL = strSQL + "     select *" + vbCr
                        strSQL = strSQL + "     from ��Ϣ_B_������Դ_�Ķ����" + vbCr
                        strSQL = strSQL + "     where ��Ա���� = @ydry" + vbCr
                        strSQL = strSQL + "   ) b on a.��Դ��ʶ = b.��Դ��ʶ" + vbCr
                        strSQL = strSQL + " ) a" + vbCr
                        strSQL = strSQL + " order by a.�������� desc " + vbCr

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@zybs", strZYBS)
                        objSqlCommand.Parameters.AddWithValue("@ydry", strUserId)
                        .SelectCommand = objSqlCommand

                        'ִ�в���
                        .Fill(objTempGonggongziyuanData.Tables(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.TABLE_XX_B_GONGGONGZIYUAN))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempGonggongziyuanData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.ggxxGonggongziyuanData.SafeRelease(objTempGonggongziyuanData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objGonggongziyuanData = objTempGonggongziyuanData
            getDataSet = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.ggxxGonggongziyuanData.SafeRelease(objTempGonggongziyuanData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡstrUserId���ܹ��Ķ����ѷ����Ĺ�����Դ���ݣ������������ڡ�����
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     strUserId                   ���û���ʶ
        '     strPassword                 ���û�����
        '     strWhere                    �������ַ���
        '     blnUnused                   ��������
        '     objGonggongziyuanData       ����Ϣ���ݼ�
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByVal blnUnused As Boolean, _
            ByRef objGonggongziyuanData As Xydc.Platform.Common.Data.ggxxGonggongziyuanData) As Boolean

            Dim objTempGonggongziyuanData As Xydc.Platform.Common.Data.ggxxGonggongziyuanData
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '��ʼ��
            getDataSet = False
            objGonggongziyuanData = Nothing
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
                    objTempGonggongziyuanData = New Xydc.Platform.Common.Data.ggxxGonggongziyuanData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.enumTableType.XX_B_GONGGONGZIYUAN)

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
                        strSQL = strSQL + "   select " + vbCr
                        strSQL = strSQL + "     a.��Դ��ʶ,a.��Դ���,a.��������,a.��Ŀ��ʶ,a.��Ա����,a.��֯����,a.��������,"
                        strSQL = strSQL + "     a.��Դ����,a.�ļ�λ��,a.��������,a.������ʶ,a.��������,a.������Χ,a.��Դ����," + vbCr
                        strSQL = strSQL + "     �Ķ����� = case when b.��Ա���� is null then '" + strFalse + "' else '" + strTrue + "' end," + vbCr
                        strSQL = strSQL + "     �������� = case when isnull(a.������ʶ,0) = 0 then '" + strFalse + "' else '" + strTrue + "' end," + vbCr
                        strSQL = strSQL + "     �������� = case when isnull(a.��������,0) = 0 then '" + strFalse + "' else '" + strTrue + "' end," + vbCr
                        strSQL = strSQL + "     c.��Ŀ����,c.��Ŀ����," + vbCr
                        strSQL = strSQL + "     d.��Ա����," + vbCr
                        strSQL = strSQL + "     e.��֯���� " + vbCr
                        strSQL = strSQL + "   from" + vbCr
                        strSQL = strSQL + "   (" + vbCr
                        strSQL = strSQL + "     select *" + vbCr
                        strSQL = strSQL + "     from ��Ϣ_B_������Դ" + vbCr
                        strSQL = strSQL + "     where ������ʶ = 1" + vbCr '�ѷ���
                        strSQL = strSQL + "   ) a" + vbCr
                        strSQL = strSQL + "   left join ��Ϣ_B_������Դ_��Ŀ c on a.��Ŀ��ʶ = c.��Ŀ��ʶ" + vbCr
                        strSQL = strSQL + "   left join ����_B_��Ա          d on a.��Ա���� = d.��Ա����" + vbCr
                        strSQL = strSQL + "   left join ����_B_��֯����      e on a.��֯���� = e.��֯����" + vbCr
                        strSQL = strSQL + "   left join " + vbCr
                        strSQL = strSQL + "   (" + vbCr
                        strSQL = strSQL + "     select *" + vbCr
                        strSQL = strSQL + "     from ��Ϣ_B_������Դ_�Ķ����" + vbCr
                        strSQL = strSQL + "     where ��Ա���� = @ydry" + vbCr
                        strSQL = strSQL + "   ) b on a.��Դ��ʶ = b.��Դ��ʶ" + vbCr
                        strSQL = strSQL + "   left join " + vbCr
                        strSQL = strSQL + "   (" + vbCr
                        strSQL = strSQL + "     select *" + vbCr
                        strSQL = strSQL + "     from ��Ϣ_B_������Դ_�Ķ���Χ" + vbCr
                        strSQL = strSQL + "     where ��Ա���� = @ydry" + vbCr
                        strSQL = strSQL + "   ) f on a.��Դ��ʶ = f.��Դ��ʶ" + vbCr
                        strSQL = strSQL + "   where ((isnull(a.��������,0) = 0) or (isnull(a.��������,0) = 1 and f.��Ա���� is not null) or (a.��Ա���� = @ydry))" '���Ķ�
                        strSQL = strSQL + " ) a" + vbCr
                        If strWhere <> "" Then
                            strSQL = strSQL + " where " + strWhere + vbCr
                        End If
                        strSQL = strSQL + " order by a.�������� desc " + vbCr

                        '���ò���
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@ydry", strUserId)
                        .SelectCommand = objSqlCommand

                        'ִ�в���
                        .Fill(objTempGonggongziyuanData.Tables(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.TABLE_XX_B_GONGGONGZIYUAN))
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempGonggongziyuanData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.ggxxGonggongziyuanData.SafeRelease(objTempGonggongziyuanData)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            objGonggongziyuanData = objTempGonggongziyuanData
            getDataSet = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.ggxxGonggongziyuanData.SafeRelease(objTempGonggongziyuanData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ[��Դ��ʶ=strZYBS]�Ĺ�����Դ�������Ķ���Ա����
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     strUserId                   ���û���ʶ
        '     strPassword                 ���û�����
        '     strZYBS                     ����Դ��ʶ
        '     strYDRYMC                   �������أ������Ķ���Ա����(��Ա����)
        '     strYDRYDM                   �������أ������Ķ���Ա����(��Ա����)
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function getKeYueduRenyuan( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZYBS As String, _
            ByRef strYDRYMC As String, _
            ByRef strYDRYDM As String) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim strSQL As String

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet

            '��ʼ��
            getKeYueduRenyuan = False
            strErrMsg = ""
            strYDRYMC = ""
            strYDRYDM = ""

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
                If strZYBS Is Nothing Then strZYBS = ""
                strZYBS = strZYBS.Trim
                If strZYBS = "" Then
                    strErrMsg = "����δָ��[��Դ��ʶ]��"
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ���ݼ�
                strSQL = ""
                strSQL = strSQL + " select a.��Ա����, b.��Ա����" + vbCr
                strSQL = strSQL + " from" + vbCr
                strSQL = strSQL + " ("
                strSQL = strSQL + "   select *" + vbCr
                strSQL = strSQL + "   from ��Ϣ_B_������Դ_�Ķ���Χ" + vbCr
                strSQL = strSQL + "   where ��Դ��ʶ = '" + strZYBS + "'" + vbCr
                strSQL = strSQL + " ) a" + vbCr
                strSQL = strSQL + " left join ����_B_��Ա b on a.��Ա���� = b.��Ա����" + vbCr
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
                                strTemp = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_YUEDUFANWEI_RYMC), "")
                                If strTemp <> "" Then
                                    If strYDRYMC = "" Then
                                        strYDRYMC = strTemp
                                    Else
                                        strYDRYMC = strYDRYMC + objPulicParameters.CharSeparate + strTemp
                                    End If
                                End If

                                strTemp = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_YUEDUFANWEI_RYDM), "")
                                If strTemp <> "" Then
                                    If strYDRYDM = "" Then
                                        strYDRYDM = strTemp
                                    Else
                                        strYDRYDM = strYDRYDM + objPulicParameters.CharSeparate + strTemp
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
        ' ȡ���ѷ����Ĺ�����Դ �� ����������Դ
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strZYBS              ����Դ��ʶ
        '     blnFabu              ��True-������False-ȡ������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doFabu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZYBS As String, _
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
                If strZYBS Is Nothing Then strZYBS = ""
                strZYBS = strZYBS.Trim
                If strZYBS = "" Then
                    strErrMsg = "����δָ��[��Դ��ʶ]��"
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
                        strSQL = strSQL + " update ��Ϣ_B_������Դ set" + vbCr
                        strSQL = strSQL + "   ������ʶ = 1," + vbCr
                        strSQL = strSQL + "   �������� = @rq" + vbCr
                        strSQL = strSQL + " where ��Դ��ʶ = @zybs" + vbCr
                        strSQL = strSQL + " and   ������ʶ <> 1" + vbCr
                        objSqlCommand.Parameters.AddWithValue("@rq", Now)
                        objSqlCommand.Parameters.AddWithValue("@zybs", strZYBS)
                    Else
                        strSQL = ""
                        strSQL = strSQL + " update ��Ϣ_B_������Դ set" + vbCr
                        strSQL = strSQL + "   ������ʶ = 0" + vbCr
                        strSQL = strSQL + " where ��Դ��ʶ = @zybs" + vbCr
                        strSQL = strSQL + " and   ������ʶ <> 0" + vbCr
                        objSqlCommand.Parameters.AddWithValue("@zybs", strZYBS)
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
        '     strZYBS              ����Դ��ʶ
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doSetHasRead( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZYBS As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
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
                If strZYBS Is Nothing Then strZYBS = ""
                strZYBS = strZYBS.Trim
                If strZYBS = "" Then
                    strErrMsg = "����δָ��[��Դ��ʶ]��"
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
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
                    strSQL = strSQL + " delete from ��Ϣ_B_������Դ_�Ķ����" + vbCr
                    strSQL = strSQL + " where ��Դ��ʶ = @zybs" + vbCr
                    strSQL = strSQL + " and   ��Ա���� = @ydry" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@zybs", strZYBS)
                    objSqlCommand.Parameters.AddWithValue("@ydry", strUserId)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '�����Ķ���¼
                    strSQL = ""
                    strSQL = strSQL + " insert into ��Ϣ_B_������Դ_�Ķ���� (" + vbCr
                    strSQL = strSQL + "   ��Դ��ʶ,��Ա����" + vbCr
                    strSQL = strSQL + " ) values (" + vbCr
                    strSQL = strSQL + "   @zybs,@ydry" + vbCr
                    strSQL = strSQL + " )" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@zybs", strZYBS)
                    objSqlCommand.Parameters.AddWithValue("@ydry", strUserId)
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
            doSetHasRead = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ɾ��������Դ
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strZYBS              ����Դ��ʶ
        '     strAppRoot           ��Ӧ�ø�Http·��(����/)
        '     objServer            ������������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doDelete( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZYBS As String, _
            ByVal strAppRoot As String, _
            ByVal objServer As System.Web.HttpServerUtility) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            Dim objDataSet As Xydc.Platform.Common.Data.ggxxGonggongziyuanData
            Dim strZWNR As String = ""
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
                strZYBS = strZYBS.Trim
                If strZYBS = "" Then
                    strErrMsg = "����δָ��[��Դ��ʶ]��"
                    GoTo errProc
                End If
                If objServer Is Nothing Then
                    strErrMsg = "����δָ��[System.Web.HttpServerUtility]��"
                    GoTo errProc
                End If
                If strAppRoot Is Nothing Then strAppRoot = ""
                strAppRoot = strAppRoot.Trim

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ��������
                If Me.getDataSet(strErrMsg, strUserId, strPassword, strZYBS, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables.Count < 1 Then
                    strErrMsg = "�����޷���ȡ���ݣ�"
                    GoTo errProc
                End If
                If objDataSet.Tables(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.TABLE_XX_B_GONGGONGZIYUAN) Is Nothing Then
                    strErrMsg = "�����޷���ȡ���ݣ�"
                    GoTo errProc
                End If
                If objDataSet.Tables(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.TABLE_XX_B_GONGGONGZIYUAN).Rows.Count < 1 Then
                    strErrMsg = "�����޷���ȡ���ݣ�"
                    GoTo errProc
                End If
                With objDataSet.Tables(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.TABLE_XX_B_GONGGONGZIYUAN).Rows(0)
                    strZWNR = objPulicParameters.getObjectValue(.Item(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_WJWZ), "")
                End With
                Xydc.Platform.Common.Data.ggxxGonggongziyuanData.SafeRelease(objDataSet)

                '��ʼ����
                objSqlTransaction = objSqlConnection.BeginTransaction()

                'ɾ������
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ɾ������Ϣ_B_������Դ_�Ķ���Χ����Ϣ
                    strSQL = ""
                    strSQL = strSQL + " delete from ��Ϣ_B_������Դ_�Ķ���Χ " + vbCr
                    strSQL = strSQL + " where ��Դ��ʶ = @zybs" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@zybs", strZYBS)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    'ɾ������Ϣ_B_������Դ_�Ķ��������Ϣ
                    strSQL = ""
                    strSQL = strSQL + " delete from ��Ϣ_B_������Դ_�Ķ���� " + vbCr
                    strSQL = strSQL + " where ��Դ��ʶ = @zybs" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@zybs", strZYBS)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    'ɾ������Ϣ_B_������Դ����Ϣ
                    strSQL = ""
                    strSQL = strSQL + " delete from ��Ϣ_B_������Դ " + vbCr
                    strSQL = strSQL + " where ��Դ��ʶ = @zybs" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@zybs", strZYBS)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    'ɾ�����ļ�λ�á���Ӧ�ļ�����
                    Dim strLocalFile As String = ""
                    If strZWNR <> "" Then
                        '����HTTPλ��
                        strLocalFile = strAppRoot + Xydc.Platform.Common.Utilities.BaseURI.DEFAULT_DIRSEP + strZWNR
                        strLocalFile = objServer.MapPath(strLocalFile)
                        'ɾ���ļ�
                        If objBaseLocalFile.doDeleteFile(strErrMsg, strLocalFile) = False Then
                            '���Բ��ɹ����γ������ļ���
                        End If
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
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.ggxxGonggongziyuanData.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            doDelete = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.ggxxGonggongziyuanData.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��顰��Ϣ_B_������Դ�������ݵĺϷ���
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     objOldData           ��������
        '     objNewData           ��(����)������
        '     objenumEditType      ���༭����
        '     strUploadFile        �������ļ���WEB������ȫ·��
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
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType, _
            ByVal strUploadFile As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
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
                If strUploadFile Is Nothing Then strUploadFile = ""
                strUploadFile = strUploadFile.Trim

                '��ȡ��ṹ����
                strSQL = "select top 0 * from ��Ϣ_B_������Դ"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, strUserId, strPassword, strSQL, "��Ϣ_B_������Դ", objDataSet) = False Then
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

                        Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZYNR
                            'TEXT��

                        Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LMMC, _
                            Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LMDM, _
                            Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_RYMC, _
                            Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZZMC, _
                            Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_FBMS, _
                            Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_KZMS, _
                            Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_YDMS
                            '������

                        Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZYBS
                            'ϵͳ�Զ�����ֵ
                            If strValue = "" Then
                                If objdacCommon.getNewGUID(strErrMsg, strUserId, strPassword, strValue) = False Then
                                    GoTo errProc
                                End If
                            End If

                        Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_FBRQ
                            If strValue = "" Then
                                strValue = Format(Now, "yyyy-MM-dd HH:mm:ss")
                            End If
                            If objPulicParameters.isDatetimeString(strValue) = False Then
                                strErrMsg = "����[" + strField + "]������Ч�����ڣ�"
                                GoTo errProc
                            End If
                            strValue = Format(CType(strValue, System.DateTime), "yyyy-MM-dd HH:mm:ss")
                        Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_BLRQ
                            If strValue <> "" Then
                                If objPulicParameters.isDatetimeString(strValue) = False Then
                                    strErrMsg = "����[" + strField + "]������Ч�����ڣ�"
                                    GoTo errProc
                                End If
                                strValue = Format(CType(strValue, System.DateTime), "yyyy-MM-dd HH:mm:ss")
                            End If

                        Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_FBBS, _
                            Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_FBKZ
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
                        Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_NRLX, _
                            Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LMBS
                            If strValue = "" Then
                                strErrMsg = "����[" + strField + "]����Ϊ�գ�"
                                GoTo errProc
                            End If
                            If objPulicParameters.isIntegerString(strValue) = False Then
                                strErrMsg = "����[" + strField + "]������Ч�����֣�"
                                GoTo errProc
                            End If
                        Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZYXH
                            '�����

                        Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_RYDM, _
                            Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZZDM, _
                            Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZYBT, _
                            Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZZMC

                            If strValue = "" Then
                                If strField = Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZZMC Then
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

                '��顰��Ŀ��ʶ��+����Դ��š�
                Dim strLMBS As String = objNewData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LMBS)
                Dim strZYXH As String
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                        '�Զ����á���Դ��š�
                        If objdacCommon.getNewCode(strErrMsg, objSqlConnection, "��Դ���", "��Ŀ��ʶ", strLMBS, "��Ϣ_B_������Դ", True, strZYXH) = False Then
                            GoTo errProc
                        Else
                            objNewData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZYXH) = strZYXH
                        End If

                        strSQL = ""
                        strSQL = strSQL + " select *" + vbCr
                        strSQL = strSQL + " from ��Ϣ_B_������Դ" + vbCr
                        strSQL = strSQL + " where ��Ŀ��ʶ = " + strLMBS + "" + vbCr
                        strSQL = strSQL + " and   ��Դ��� = " + strZYXH + "" + vbCr

                    Case Else
                        Dim strZYBS As String = objNewData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZYBS)
                        strZYXH = objNewData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZYXH)

                        strSQL = ""
                        strSQL = strSQL + " select *" + vbCr
                        strSQL = strSQL + " from ��Ϣ_B_������Դ" + vbCr
                        strSQL = strSQL + " where ��Ŀ��ʶ =   " + strLMBS + " " + vbCr
                        strSQL = strSQL + " and   ��Դ��� =   " + strZYXH + " " + vbCr
                        strSQL = strSQL + " and   ��Դ��ʶ <> '" + strZYBS + "'" + vbCr
                End Select
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strErrMsg = "����[��Ŀ��ʶ]+[��Դ���]�Ѿ����ڣ�"
                    GoTo errProc
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '��顰�������͡�+����Դ���ݡ�
                Dim intNRLX As Integer
                intNRLX = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_NRLX), 0)
                Dim strZYNR As String
                strZYNR = objNewData.Item(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZYNR)
                Dim blnDo As Boolean
                Select Case intNRLX
                    Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.enumZiyuanType.Text
                        If strZYNR.Trim = "" Then
                            strErrMsg = "����û������[��Դ����]��"
                            GoTo errProc
                        End If
                        objNewData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_WJWZ) = ""
                    Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.enumZiyuanType.Tuwen
                        If strZYNR.Trim = "" Then
                            strErrMsg = "����û������[��Դ����]��"
                            GoTo errProc
                        End If
                        If strUploadFile = "" Then
                            strErrMsg = "����û���ϴ�[��Դ�ļ�]��"
                            GoTo errProc
                        End If
                        If objBaseLocalFile.doFileExisted(strErrMsg, strUploadFile, blnDo) = False Then
                            GoTo errProc
                        End If
                        If blnDo = False Then
                            strErrMsg = "����[" + strUploadFile + "]�����ڣ�"
                            GoTo errProc
                        End If
                    Case Else
                        If strUploadFile = "" Then
                            strErrMsg = "����û���ϴ�[��Դ�ļ�]��"
                            GoTo errProc
                        End If
                        If objBaseLocalFile.doFileExisted(strErrMsg, strUploadFile, blnDo) = False Then
                            GoTo errProc
                        End If
                        If blnDo = False Then
                            strErrMsg = "����[" + strUploadFile + "]�����ڣ�"
                            GoTo errProc
                        End If
                        objNewData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZYNR) = ""
                End Select
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            doVerify = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���桰��Ϣ_B_������Դ��������(��������)
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

                                    Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZYNR

                                    Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LMMC, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LMDM, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_RYMC, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZZMC, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_FBMS, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_KZMS, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_YDMS
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
                            strSQL = strSQL + " insert into ��Ϣ_B_������Դ (" + strFileds + ")"
                            strSQL = strSQL + " values (" + strValues + ")"
                            '׼������
                            objSqlCommand.Parameters.Clear()
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)

                                    Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZYNR

                                    Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LMMC, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LMDM, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_RYMC, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZZMC, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_FBMS, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_KZMS, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_YDMS
                                        '������
                                    Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_FBRQ, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_BLRQ
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), System.DBNull.Value)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(objNewData.Item(i), System.DateTime))
                                        End If
                                    Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LMBS, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_NRLX, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_FBBS, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_FBKZ, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZYXH
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
                            '��ȡԭ����Դ��ʶ��
                            Dim strOldZYBS As String
                            strOldZYBS = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZYBS), "")
                            '��������ֶ��б�
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)

                                    Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZYNR

                                    Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LMMC, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LMDM, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_RYMC, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZZMC, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_FBMS, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_KZMS, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_YDMS
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
                            strSQL = strSQL + " update ��Ϣ_B_������Դ set " + vbCr
                            strSQL = strSQL + "   " + strFileds + vbCr
                            strSQL = strSQL + " where ��Դ��ʶ = @oldzybs" + vbCr
                            '׼������
                            objSqlCommand.Parameters.Clear()
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)

                                    Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZYNR

                                    Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LMMC, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LMDM, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_RYMC, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZZMC, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_FBMS, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_KZMS, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_YDMS
                                        '������
                                    Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_FBRQ, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_BLRQ
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), System.DBNull.Value)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(objNewData.Item(i), System.DateTime))
                                        End If
                                    Case Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_LMBS, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_NRLX, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_FBBS, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_FBKZ, _
                                        Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZYXH
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
                            objSqlCommand.Parameters.AddWithValue("@oldzybs", strOldZYBS)
                            'ִ��SQL
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()
                    End Select


                    'text�д���
                    Dim strValue As String
                    Dim strZYBS As String
                    Dim strName As String
                    strZYBS = objNewData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZYBS)
                    strName = Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZYNR
                    If Not (objNewData(strName) Is Nothing) Then
                        strValue = objNewData(strName)
                        strValue = strValue.Replace("'", "''")
                        strSQL = ""
                        strSQL = strSQL + " DECLARE @ptrval binary(16)" + vbCr
                        strSQL = strSQL + " select @ptrval = TEXTPTR(" + strName + ")" + vbCr
                        strSQL = strSQL + " from ��Ϣ_B_������Դ" + vbCr
                        strSQL = strSQL + " where ��Դ��ʶ = @wybs" + vbCr
                        strSQL = strSQL + " WRITETEXT ��Ϣ_B_������Դ." + strName + " @ptrval '" + strValue + "'" + vbCr
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@wybs", strZYBS)
                        objSqlCommand.CommandText = strSQL
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
        ' ���桰��Ϣ_B_������Դ_�Ķ���Χ��������(��������)
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
            Dim strRymcList As String
            Dim strRydmList As String

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
                    strRymcList = ""
                    strRydmList = ""
                Else
                    '������ʱ����
                    objNewSqlConnection = New System.Data.SqlClient.SqlConnection(objSqlConnection.ConnectionString)
                    objNewSqlConnection.Open()
                    '����
                    If objdacCustomer.getRenyuanList(strErrMsg, objNewSqlConnection, strFBFW, objPulicParameters.CharSeparate, strRymcList, strRydmList) = False Then
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
                            Dim strOldZybs As String
                            strOldZybs = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZYBS), "")
                            strSQL = ""
                            strSQL = strSQL + " delete from ��Ϣ_B_������Դ_�Ķ���Χ" + vbCr
                            strSQL = strSQL + " where ��Դ��ʶ = @zybs" + vbCr
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@zybs", strOldZybs)
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()
                    End Select

                    '������������
                    If strRydmList <> "" Then
                        Dim strNewZybs As String
                        strNewZybs = objPulicParameters.getObjectValue(objNewData.Item(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZYBS), "")

                        Dim strArray() As String
                        Dim intCount As Integer
                        Dim i As Integer
                        strArray = strRydmList.Split(Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate.ToCharArray)
                        intCount = strArray.Length
                        For i = 0 To intCount - 1 Step 1
                            strSQL = ""
                            strSQL = strSQL + " insert into ��Ϣ_B_������Դ_�Ķ���Χ (" + vbCr
                            strSQL = strSQL + "   ��Դ��ʶ,��Ա����" + vbCr
                            strSQL = strSQL + " ) values (" + vbCr
                            strSQL = strSQL + "   @zybs, @ydry"
                            strSQL = strSQL + " )"
                            objSqlCommand.Parameters.Clear()
                            objSqlCommand.Parameters.AddWithValue("@zybs", strNewZybs)
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

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objNewSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            doSave = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objNewSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���ݱ����ļ���ȡ��Դ�ļ���HTTP�������ļ�������
        ' ������������Դ��ʶ+�ļ���չ��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strLocalFile         �������ļ���
        '     intWJND              ���ļ����
        '     strZYBS              ����Դ��ʶ
        '     strBasePath          �����ļ���ŵ�HTTP��׼·��(/)
        '     strRemoteFile        ������HTTP�������ļ�·��(���ַ�����/)
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getHTTPFileName( _
            ByRef strErrMsg As String, _
            ByVal strLocalFile As String, _
            ByVal intWJND As Integer, _
            ByVal strZYBS As String, _
            ByVal strBasePath As String, _
            ByRef strRemoteFile As String) As Boolean

            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile

            getHTTPFileName = False
            strRemoteFile = ""

            Try
                '���
                If strLocalFile Is Nothing Then strLocalFile = ""
                strLocalFile = strLocalFile.Trim()
                If strLocalFile = "" Then
                    Exit Try
                End If
                If strZYBS Is Nothing Then strZYBS = ""
                strZYBS = strZYBS.Trim()
                If strZYBS = "" Then
                    Exit Try
                End If
                If strBasePath Is Nothing Then strBasePath = ""
                strBasePath = strBasePath.Trim
                strBasePath = strBasePath.Replace(Xydc.Platform.Common.Utilities.BaseURI.DEFAULT_DIRSEP, Xydc.Platform.Common.Utilities.BaseLocalFile.DEFAULT_DIRSEP)

                '��ȡ�ļ���
                Dim strFileName As String = ""
                Dim strFileExt As String = ""
                strFileExt = objBaseLocalFile.getExtension(strLocalFile)

                '������������Դ��ʶ+�ļ���չ��
                strFileName = strZYBS + strFileExt
                strFileName = objBaseLocalFile.doMakePath(intWJND.ToString(), strFileName)

                '����Ŀ¼+�ļ�
                strFileName = objBaseLocalFile.doMakePath(strBasePath, strFileName)

                'ת���ָ���
                strFileName = strFileName.Replace(Xydc.Platform.Common.Utilities.BaseLocalFile.DEFAULT_DIRSEP, Xydc.Platform.Common.Utilities.BaseURI.DEFAULT_DIRSEP)
                If strFileName.Substring(0) = Xydc.Platform.Common.Utilities.BaseURI.DEFAULT_DIRSEP Then
                    strFileName = strFileName.Substring(1)
                End If

                '����
                strRemoteFile = strFileName

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)

            getHTTPFileName = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ������Դ�ļ�
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strGJFTPSpec           ����Դ�ļ�����HTTP·��
        '     strAppRoot             ��Ӧ�ø�Http·��(����/)
        '     objServer              ������������
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Function doBackupFiles( _
            ByRef strErrMsg As String, _
            ByVal strGJFTPSpec As String, _
            ByVal strAppRoot As String, _
            ByVal objServer As System.Web.HttpServerUtility) As Boolean

            Dim strBakExt As String = Xydc.Platform.Common.Utilities.PulicParameters.BACKUPFILEEXT
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile

            doBackupFiles = False
            strErrMsg = ""

            Try
                '���
                If strGJFTPSpec Is Nothing Then strGJFTPSpec = ""
                strGJFTPSpec = strGJFTPSpec.Trim
                If strGJFTPSpec = "" Then
                    Exit Try
                End If
                If objServer Is Nothing Then
                    Exit Try
                End If
                If strAppRoot Is Nothing Then strAppRoot = ""
                strAppRoot = strAppRoot.Trim

                '����
                Dim strLocalFile As String = ""
                Dim strHttpFile As String = ""
                strHttpFile = strAppRoot + Xydc.Platform.Common.Utilities.BaseURI.DEFAULT_DIRSEP + strGJFTPSpec
                strLocalFile = objServer.MapPath(strHttpFile)
                Dim blnDo As Boolean
                If objBaseLocalFile.doFileExisted(strErrMsg, strLocalFile, blnDo) = False Then
                    Exit Try
                End If
                If blnDo = True Then
                    '�����ļ�
                    If objBaseLocalFile.doCopyFile(strErrMsg, strLocalFile, strLocalFile + strBakExt, True) = False Then
                        GoTo errProc
                    End If
                    'ɾ�������ļ�
                    If objBaseLocalFile.doDeleteFile(strErrMsg, strLocalFile) = False Then
                        '�γ������ļ���
                    End If
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)

            doBackupFiles = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ɾ����Դ�����ļ�
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strGJFTPSpec           ����Դ�ļ���ԭHTTP·��
        '     strAppRoot             ��Ӧ�ø�Http·��(����/)
        '     objServer              ������������
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doDeleteBackupFiles( _
            ByRef strErrMsg As String, _
            ByVal strGJFTPSpec As String, _
            ByVal strAppRoot As String, _
            ByVal objServer As System.Web.HttpServerUtility) As Boolean

            Dim strBakExt As String = Xydc.Platform.Common.Utilities.PulicParameters.BACKUPFILEEXT
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile

            doDeleteBackupFiles = False
            strErrMsg = ""

            Try
                '���
                If strGJFTPSpec Is Nothing Then strGJFTPSpec = ""
                strGJFTPSpec = strGJFTPSpec.Trim
                If strGJFTPSpec = "" Then
                    Exit Try
                End If
                If objServer Is Nothing Then
                    Exit Try
                End If
                If strAppRoot Is Nothing Then strAppRoot = ""
                strAppRoot = strAppRoot.Trim

                'ɾ������
                Dim strLocalFile As String = ""
                Dim strHttpFile As String = ""
                strHttpFile = strAppRoot + Xydc.Platform.Common.Utilities.BaseURI.DEFAULT_DIRSEP + strGJFTPSpec
                strLocalFile = objServer.MapPath(strHttpFile)
                strLocalFile = strLocalFile + strBakExt
                If objBaseLocalFile.doDeleteFile(strErrMsg, strLocalFile) = False Then
                    '�γ������ļ���
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)

            doDeleteBackupFiles = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �ӱ����лָ���Դ�ļ�
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strGJFTPSpec           ����Դ�ļ���ԭHTTP·��
        '     strAppRoot             ��Ӧ�ø�Http·��(����/)
        '     objServer              ������������
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doRestoreFiles( _
            ByRef strErrMsg As String, _
            ByVal strGJFTPSpec As String, _
            ByVal strAppRoot As String, _
            ByVal objServer As System.Web.HttpServerUtility) As Boolean

            Dim strBakExt As String = Xydc.Platform.Common.Utilities.PulicParameters.BACKUPFILEEXT
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile

            doRestoreFiles = False
            strErrMsg = ""

            Try
                '���
                If strGJFTPSpec Is Nothing Then strGJFTPSpec = ""
                strGJFTPSpec = strGJFTPSpec.Trim
                If strGJFTPSpec = "" Then
                    Exit Try
                End If
                If objServer Is Nothing Then
                    Exit Try
                End If
                If strAppRoot Is Nothing Then strAppRoot = ""
                strAppRoot = strAppRoot.Trim

                '�ָ�
                Dim strLocalFile As String = ""
                Dim strHttpFile As String = ""
                strHttpFile = strAppRoot + Xydc.Platform.Common.Utilities.BaseURI.DEFAULT_DIRSEP + strGJFTPSpec
                strLocalFile = objServer.MapPath(strHttpFile)
                '�ָ��ļ�
                If objBaseLocalFile.doCopyFile(strErrMsg, strLocalFile + strBakExt, strLocalFile, True) = False Then
                    GoTo errProc
                End If
                'ɾ������
                If objBaseLocalFile.doDeleteFile(strErrMsg, strLocalFile + strBakExt) = False Then
                    '�γ������ļ�
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)

            doRestoreFiles = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ������Դ�ļ�
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     objSqlTransaction      ����������
        '     strUserId              ���û���ʶ
        '     strPassword            ���û�����
        '     strZYBS                ����Դ��ʶ
        '     strOldFile             �����ļ�·��(���HTTP��Ŀ¼·��)
        '     strGJFile              ��Ҫ�������Դ�ļ��ı��ػ����ļ�����·��
        '     intWJND                ��Ҫ���浽�����
        '     strAppRoot             ��Ӧ�ø�Http·��(����/)
        '     strBasePath            ����Ӧ�ø�����ŵص����HTTPĿ¼(��ͷ����/)
        '     objServer              ������������
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Function doSaveFile( _
            ByRef strErrMsg As String, _
            ByVal objSqlTransaction As System.Data.SqlClient.SqlTransaction, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZYBS As String, _
            ByVal strOldFile As String, _
            ByVal strGJFile As String, _
            ByVal intWJND As Integer, _
            ByVal strAppRoot As String, _
            ByVal strBasePath As String, _
            ByVal objServer As System.Web.HttpServerUtility) As Boolean

            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim blnNewTrans As Boolean = False
            Dim strWJWZ As String
            Dim strSQL As String

            doSaveFile = False
            strErrMsg = ""

            Try
                '����������
                If objSqlTransaction Is Nothing Then
                    If strUserId Is Nothing Then strUserId = ""
                    strUserId = strUserId.Trim
                    If strUserId = "" Then
                        strErrMsg = "����δ���������û���"
                        GoTo errProc
                    End If
                End If
                If strGJFile Is Nothing Then strGJFile = ""
                strGJFile = strGJFile.Trim()
                If strGJFile = "" Then
                    '���ñ���
                    Exit Try
                End If
                If objServer Is Nothing Then
                    strErrMsg = "����δ����[System.Web.HttpServerUtility]��"
                    GoTo errProc
                End If
                If strAppRoot Is Nothing Then strAppRoot = ""
                strAppRoot = strAppRoot.Trim
                If strZYBS Is Nothing Then strZYBS = ""
                strZYBS = strZYBS.Trim
                If strZYBS = "" Then
                    Exit Try
                End If
                If strOldFile Is Nothing Then strOldFile = ""
                strOldFile = strOldFile.Trim
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim
                If strBasePath Is Nothing Then strBasePath = ""
                strBasePath = strBasePath.Trim

                '����ļ��Ƿ����?
                Dim blnExisted As Boolean
                If objBaseLocalFile.doFileExisted(strErrMsg, strGJFile, blnExisted) = False Then
                    GoTo errProc
                End If
                If blnExisted = False Then
                    strErrMsg = "������Դ�ļ�[" + strGJFile + "]�����ڣ�"
                    GoTo errProc
                End If

                '��ȡ�ļ���Ϣ
                strWJWZ = strOldFile

                '��ȡ�������ļ�
                Dim strDesFile As String
                If Me.getHTTPFileName(strErrMsg, strGJFile, intWJND, strZYBS, strBasePath, strDesFile) = False Then
                    GoTo errProc
                End If

                '�����ļ�·��
                '��ȡ��������
                If objSqlTransaction Is Nothing Then
                    If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                        GoTo errProc
                    End If
                Else
                    objSqlConnection = objSqlTransaction.Connection
                End If

                '����ԭ�ļ�
                If Me.doBackupFiles(strErrMsg, strWJWZ, strAppRoot, objServer) = False Then
                    GoTo errProc
                End If

                '��ʼ����
                If objSqlTransaction Is Nothing Then
                    blnNewTrans = True
                    objSqlTransaction = objSqlConnection.BeginTransaction
                Else
                    blnNewTrans = False
                End If

                '�����ļ�
                Dim strHttpFile As String = strAppRoot + Xydc.Platform.Common.Utilities.BaseURI.DEFAULT_DIRSEP + strDesFile
                Dim strLocalFile As String = objServer.MapPath(strHttpFile)
                '����·��
                If objBaseLocalFile.doCreateDirectory(strErrMsg, strLocalFile) = False Then
                    GoTo errProc
                End If
                '�ϴ���HTTP����λ��
                If objBaseLocalFile.doCopyFile(strErrMsg, strGJFile, strLocalFile, True) = False Then
                    GoTo rollDatabaseAndFile
                End If

                Try
                    '׼���������
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '׼��SQL
                    strSQL = ""
                    strSQL = strSQL + " update ��Ϣ_B_������Դ set " + vbCr
                    strSQL = strSQL + "   �ļ�λ�� = @wjwz " + vbCr
                    strSQL = strSQL + " where ��Դ��ʶ  = @wjbs " + vbCr
                    strSQL = strSQL + " and   �ļ�λ�� <> @wjwz " + vbCr

                    'ִ������
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@wjwz", strDesFile)
                    objSqlCommand.Parameters.AddWithValue("@wjbs", strZYBS)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo rollDatabaseAndFile
                End Try

                '�ύ����
                If blnNewTrans = True Then
                    objSqlTransaction.Commit()
                End If

                'ɾ�������ļ�
                If blnNewTrans = True Then
                    If Me.doDeleteBackupFiles(strErrMsg, strWJWZ, strAppRoot, objServer) = False Then
                        '���Բ��ɹ����γ������ļ���
                    End If
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            If blnNewTrans = True Then
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            End If

            doSaveFile = True
            Exit Function

rollDatabaseAndFile:
            If blnNewTrans = True Then
                objSqlTransaction.Rollback()
                If Me.doRestoreFiles(strSQL, strWJWZ, strAppRoot, objServer) = False Then
                    '���Բ��ɹ����γ���������
                End If
            End If
            GoTo errProc

errProc:
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            If blnNewTrans = True Then
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            End If
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ɾ����Դ�ļ�
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     objSqlTransaction      ����������
        '     objConnectionProperty  ��FTP���Ӳ���
        '     strUserId              ���û���ʶ
        '     strPassword            ���û�����
        '     strZYBS                ����Դ��ʶ
        '     strOldFile             �����ļ�·��(���Ӧ�ø�Ŀ¼·��)
        '     strAppRoot             ��Ӧ�ø�Http·��(����/)
        '     objServer              ������������
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Function doDeleteFile( _
            ByRef strErrMsg As String, _
            ByVal objSqlTransaction As System.Data.SqlClient.SqlTransaction, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZYBS As String, _
            ByVal strOldFile As String, _
            ByVal strAppRoot As String, _
            ByVal objServer As System.Web.HttpServerUtility) As Boolean

            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim blnNewTrans As Boolean = False
            Dim strWJWZ As String
            Dim strSQL As String

            doDeleteFile = False
            strErrMsg = ""

            Try
                '����������
                If objServer Is Nothing Then
                    strErrMsg = "����[doDeleteFile]û��ָ��[System.Web.HttpServerUtility]��"
                    GoTo errProc
                End If
                If objSqlTransaction Is Nothing Then
                    If strUserId Is Nothing Then strUserId = ""
                    strUserId = strUserId.Trim
                    If strUserId = "" Then
                        strErrMsg = "����δ���������û���"
                        GoTo errProc
                    End If
                End If
                If strZYBS Is Nothing Then strZYBS = ""
                strZYBS = strZYBS.Trim
                If strZYBS = "" Then
                    Exit Try
                End If
                If strOldFile Is Nothing Then strOldFile = ""
                strOldFile = strOldFile.Trim
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim
                If strAppRoot Is Nothing Then strAppRoot = ""
                strAppRoot = strAppRoot.Trim

                '��ȡ�ļ���Ϣ
                strWJWZ = strOldFile

                '��ȡ��������
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
                    objSqlTransaction = objSqlConnection.BeginTransaction
                Else
                    blnNewTrans = False
                End If

                'ɾ���ļ�
                If strWJWZ <> "" Then
                    Dim strHttpFile As String = strAppRoot + Xydc.Platform.Common.Utilities.BaseURI.DEFAULT_DIRSEP + strWJWZ
                    Dim strLocalFile As String = objServer.MapPath(strHttpFile)
                    If objBaseLocalFile.doDeleteFile(strErrMsg, strLocalFile) = False Then
                        '�γ������ļ���
                    End If
                End If

                Try
                    '׼���������
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '׼��SQL
                    strSQL = ""
                    strSQL = strSQL + " update ��Ϣ_B_������Դ set " + vbCr
                    strSQL = strSQL + "   �ļ�λ�� = @wjwz " + vbCr
                    strSQL = strSQL + " where ��Դ��ʶ  = @wjbs " + vbCr

                    'ִ������
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@wjwz", " ")
                    objSqlCommand.Parameters.AddWithValue("@wjbs", strZYBS)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo rollDatabaseAndFile
                End Try

                '�ύ����
                If blnNewTrans = True Then
                    objSqlTransaction.Commit()
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            If blnNewTrans = True Then
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            End If

            doDeleteFile = True
            Exit Function

rollDatabaseAndFile:
            If blnNewTrans = True Then
                objSqlTransaction.Rollback()
            End If
            GoTo errProc

errProc:
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            If blnNewTrans = True Then
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            End If
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���湫����Դ���ݼ�¼(�����������)
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strUserId              ���û���ʶ
        '     strPassword            ���û�����
        '     objNewData             ����¼��ֵ(���ر�������ֵ)
        '     objOldData             ����¼��ֵ
        '     strFBFW                ��������Χ
        '     objenumEditType        ���༭����
        '     strUploadFile          �������ļ���WEB������ȫ·��
        '     strAppRoot             ��Ӧ�ø�Http·��(����/)
        '     strBasePath            ����Ӧ�ø�����ŵص����HTTPĿ¼(��ͷ����/)
        '     objServer              ������������
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
            ByVal strUploadFile As String, _
            ByVal strAppRoot As String, _
            ByVal strBasePath As String, _
            ByVal objServer As System.Web.HttpServerUtility) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim strWJWZ As String = ""
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
                If objServer Is Nothing Then
                    strErrMsg = "����û��ָ��[System.Web.HttpServerUtility]��"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim
                If strFBFW Is Nothing Then strFBFW = ""
                strFBFW = strFBFW.Trim
                If strUploadFile Is Nothing Then strUploadFile = ""
                strUploadFile = strUploadFile.Trim
                If strAppRoot Is Nothing Then strAppRoot = ""
                strAppRoot = strAppRoot.Trim
                If strBasePath Is Nothing Then strBasePath = strBasePath.Trim
                strBasePath = strBasePath.Trim

                '�������¼
                If Me.doVerify(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType, strUploadFile) = False Then
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
                    '�Զ����á��������ơ�
                    objNewData.Item(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_FBKZ) = "0"
                    If strFBFW <> "" Then
                        objNewData.Item(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_FBKZ) = "1"
                    End If

                    '��������¼
                    If Me.doSave(strErrMsg, objSqlTransaction, objOldData, objNewData, objenumEditType) = False Then
                        GoTo rollDatabase
                    End If

                    '���桰������Χ�����������Ա�б�
                    If Me.doSave(strErrMsg, objSqlTransaction, objOldData, objNewData, strFBFW, objenumEditType) = False Then
                        GoTo rollDatabase
                    End If

                    '������Դ�ļ�
                    Dim strZYBS As String = objNewData(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_ZYBS)
                    If objOldData Is Nothing Then
                        strWJWZ = ""
                    Else
                        strWJWZ = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.ggxxGonggongziyuanData.FIELD_XX_B_GONGGONGZIYUAN_WJWZ), "")
                    End If
                    If strUploadFile <> "" Then
                        '�������ļ�
                        Dim intWJND As Integer = Year(Now)
                        If Me.doSaveFile(strErrMsg, objSqlTransaction, strUserId, strPassword, strZYBS, strWJWZ, strUploadFile, intWJND, strAppRoot, strBasePath, objServer) = False Then
                            GoTo rollDatabaseAndFile
                        End If
                    Else
                        'ɾ�������ļ�
                        If Me.doDeleteFile(strErrMsg, objSqlTransaction, strUserId, strPassword, strZYBS, strWJWZ, strAppRoot, objServer) = False Then
                            GoTo rollDatabaseAndFile
                        End If
                    End If

                    'ɾ�������ļ�
                    If Me.doDeleteBackupFiles(strErrMsg, strWJWZ, strAppRoot, objServer) = False Then
                        '���Բ��ɹ����γ������ļ���
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

rollDatabaseAndFile:
            If Me.doRestoreFiles(strSQL, strWJWZ, strAppRoot, objServer) = False Then
                '���Բ��ɹ����γ���������
            End If
            GoTo rollDatabase

rollDatabase:
            objSqlTransaction.Rollback()
            GoTo errProc

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function






        '----------------------------------------------------------------
        ' �ж�strUserId�Ƿ��ܹ��Ķ����ѷ�����strZYBS������Դ����
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     strUserId                   ���û���ʶ
        '     strPassword                 ���û�����
        '     strZYBS                     ����Դ��ʶ
        '     blnYuedu                    �������أ�True-��,False-����
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function isCanRead( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZYBS As String, _
            ByRef blnYuedu As Boolean) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

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
                If strZYBS Is Nothing Then strZYBS = ""
                strZYBS = strZYBS.Trim
                If strZYBS = "" Then
                    Exit Try
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ȡ����
                Dim strFalse As String = Xydc.Platform.Common.Utilities.PulicParameters.CharFalse
                Dim strTrue As String = Xydc.Platform.Common.Utilities.PulicParameters.CharTrue
                strSQL = ""
                strSQL = strSQL + " select a.*" + vbCr
                strSQL = strSQL + " from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select a.*," + vbCr
                strSQL = strSQL + "     �Ķ����� = case when b.��Ա���� is null then '" + strFalse + "' else '" + strTrue + "' end," + vbCr
                strSQL = strSQL + "     �������� = case when isnull(a.������ʶ,0) = 0 then '" + strFalse + "' else '" + strTrue + "' end," + vbCr
                strSQL = strSQL + "     �������� = case when isnull(a.��������,0) = 0 then '" + strFalse + "' else '" + strTrue + "' end," + vbCr
                strSQL = strSQL + "     c.��Ŀ����,c.��Ŀ����," + vbCr
                strSQL = strSQL + "     d.��Ա����," + vbCr
                strSQL = strSQL + "     e.��֯���� " + vbCr
                strSQL = strSQL + "   from" + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select *" + vbCr
                strSQL = strSQL + "     from ��Ϣ_B_������Դ" + vbCr
                strSQL = strSQL + "     where ��Դ��ʶ = '" + strZYBS + "'" + vbCr
                strSQL = strSQL + "   ) a" + vbCr
                strSQL = strSQL + "   left join ��Ϣ_B_������Դ_��Ŀ c on a.��Ŀ��ʶ = c.��Ŀ��ʶ" + vbCr
                strSQL = strSQL + "   left join ����_B_��Ա          d on a.��Ա���� = d.��Ա����" + vbCr
                strSQL = strSQL + "   left join ����_B_��֯����      e on a.��֯���� = e.��֯����" + vbCr
                strSQL = strSQL + "   left join " + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select *" + vbCr
                strSQL = strSQL + "     from ��Ϣ_B_������Դ_�Ķ����" + vbCr
                strSQL = strSQL + "     where ��Ա���� = '" + strUserId + "'" + vbCr
                strSQL = strSQL + "   ) b on a.��Դ��ʶ = b.��Դ��ʶ" + vbCr
                strSQL = strSQL + "   left join " + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select *" + vbCr
                strSQL = strSQL + "     from ��Ϣ_B_������Դ_�Ķ���Χ" + vbCr
                strSQL = strSQL + "     where ��Ա���� = '" + strUserId + "'" + vbCr
                strSQL = strSQL + "   ) f on a.��Դ��ʶ = f.��Դ��ʶ" + vbCr
                strSQL = strSQL + "   where (a.������ʶ = 1 and ((isnull(a.��������,0) = 0) or (isnull(a.��������,0) = 1 and f.��Ա���� is not null))) or (a.��Ա���� = '" + strUserId + "')"
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
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            isCanRead = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���ĳ��Ա�Ƿ��Ѿ����뵽��ɫstrRoleName���б�
        '----------------------------------------------------------------
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     objConnectionProperty       ����������Ϣ
        '     strWhere                    �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     strRoleName                 ����ɫ����
        '     strUserId                   ���û���ʶ
        '     strPassWord                 ���û�����
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��

        '----------------------------------------------------------------
        Public Function doVerifyRoleData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strWhere As String, _
            ByVal strRoleName As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '��ʼ��
            doVerifyRoleData = False
            strErrMsg = ""

            Try
                '���
                If strWhere Is Nothing Then strWhere = ""
                If strRoleName Is Nothing Then Exit Try
                strRoleName = strRoleName.Trim()
                strWhere = strWhere.Trim()


                '��ȡ����
                Dim strSQL As String
                Dim objDataset As New System.Data.DataSet
                Dim strDefDB As String = Xydc.Platform.Common.jsoaConfiguration.DatabaseServerUserDB
                Dim strDatabase As String = objConnectionProperty.InitialCatalog

                '׼��SQL
                strSQL = ""
                strSQL = strSQL + " select distinct 'a'  where '" + strRoleName + "' in " + vbCr
                strSQL = strSQL + " ("
                strSQL = strSQL + " select a.rollname as 'NAME' from ( " + vbCr
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
                strSQL = strSQL + " )"

                If objdacCommon.getDataSetBySQL(strErrMsg, strUserId, strPassword, strSQL, objDataset) = False Then
                    GoTo errProc
                End If

                If objDataset.Tables(0).Rows.Count < 1 Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            doVerifyRoleData = True
            Exit Function

errProc:
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

    End Class

End Namespace
