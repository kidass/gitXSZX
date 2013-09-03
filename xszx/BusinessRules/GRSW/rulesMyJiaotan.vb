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

Imports System
Imports System.Data
Imports System.Text.RegularExpressions
Imports Microsoft.VisualBasic

Imports Xydc.Platform.SystemFramework
Imports Xydc.Platform.Common
Imports Xydc.Platform.Common.Data
Imports Xydc.Platform.DataAccess

Namespace Xydc.Platform.BusinessRules

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessRules
    ' ����    ��rulesMyJiaotan
    '
    ' ���������� 
    '     �ṩ�ԡ�����_B_��̸��ģ���漰��ҵ���߼������
    '----------------------------------------------------------------
    Public Class rulesMyJiaotan

        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
        End Sub

        '----------------------------------------------------------------
        ' ��ȫ�ͷű�����Դ
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessRules.rulesMyJiaotan)
            Try
                If Not (obj Is Nothing) Then
                    'obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub









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

            Try
                With New Xydc.Platform.DataAccess.dacMyJiaotan
                    getDataSet = .getDataSet(strErrMsg, strUserId, strPassword, strUserXM, strWhere, objJiaotanDataSet)
                End With
            Catch ex As Exception
                getDataSet = False
                strErrMsg = ex.Message
            End Try

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

            Try
                With New Xydc.Platform.DataAccess.dacMyJiaotan
                    getDataSet = .getDataSet(strErrMsg, strUserId, strPassword, strUserXM, strWhere, blnUnused, objJiaotanDataSet)
                End With
            Catch ex As Exception
                getDataSet = False
                strErrMsg = ex.Message
            End Try

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

            Try
                With New Xydc.Platform.DataAccess.dacMyJiaotan
                    getDataSet = .getDataSet(strErrMsg, strUserId, strPassword, strLSH, objJiaotanDataSet)
                End With
            Catch ex As Exception
                getDataSet = False
                strErrMsg = ex.Message
            End Try

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

            Try
                With New Xydc.Platform.DataAccess.dacMyJiaotan
                    getDataSetHtml = .getDataSetHtml(strErrMsg, strUserId, strPassword, strUserXM, strWhere, objJiaotanDataSet)
                End With
            Catch ex As Exception
                getDataSetHtml = False
                strErrMsg = ex.Message
            End Try

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

            Try
                With New Xydc.Platform.DataAccess.dacMyJiaotan
                    getDataSetText = .getDataSetText(strErrMsg, strUserId, strPassword, strUserXM, strWhere, objJiaotanDataSet)
                End With
            Catch ex As Exception
                getDataSetText = False
                strErrMsg = ex.Message
            End Try

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

            Try
                With New Xydc.Platform.DataAccess.dacMyJiaotan
                    doSaveData = .doVerifyData(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType)
                    If doSaveData = True Then
                        doSaveData = .doSaveData(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType)
                    End If
                End With
            Catch ex As Exception
                doSaveData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' strFSR��strJSR���ͽ�̸��ϢstrMsg
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doSendChat( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strFSR As String, _
            ByVal strJSR As String, _
            ByVal strMsg As String) As Boolean

            Dim objNewData As New System.Collections.Specialized.NameValueCollection

            Try
                With New Xydc.Platform.DataAccess.dacMyJiaotan
                    objNewData.Add(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_FSR, strFSR)
                    objNewData.Add(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_JSR, strJSR)
                    objNewData.Add(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_XX, strMsg)
                    objNewData.Add(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_BZ, "0")
                    objNewData.Add(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_TS, "0")
                    objNewData.Add(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_FSSJ, Format(Now, "yyyy-MM-dd HH:mm:ss"))
                    objNewData.Add(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_WYBS, "")

                    doSendChat = .doVerifyData(strErrMsg, strUserId, strPassword, Nothing, objNewData, Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew)
                    If doSendChat = True Then
                        doSendChat = .doSaveData(strErrMsg, strUserId, strPassword, Nothing, objNewData, Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew)
                    End If
                End With
            Catch ex As Exception
                doSendChat = False
                strErrMsg = ex.Message
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objNewData)

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

            Try
                With New Xydc.Platform.DataAccess.dacMyJiaotan
                    doDeleteData = .doDeleteData(strErrMsg, strUserId, strPassword, objOldData)
                End With
            Catch ex As Exception
                doDeleteData = False
                strErrMsg = ex.Message
            End Try

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

            Try
                With New Xydc.Platform.DataAccess.dacMyJiaotan
                    doDeleteData = .doDeleteData(strErrMsg, strUserId, strPassword, strWJBS)
                End With
            Catch ex As Exception
                doDeleteData = False
                strErrMsg = ex.Message
            End Try

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

            Try
                With New Xydc.Platform.DataAccess.dacMyJiaotan
                    getDataSetWYD = .getDataSetWYD(strErrMsg, strUserId, strPassword, strUserXM, objJiaotanDataSet)
                End With
            Catch ex As Exception
                getDataSetWYD = False
                strErrMsg = ex.Message
            End Try

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

            Try
                With New Xydc.Platform.DataAccess.dacMyJiaotan
                    getDataSetAfterTime = .getDataSetAfterTime(strErrMsg, strUserId, strPassword, strUserXM, strZDSJ, objJiaotanDataSet)
                End With
            Catch ex As Exception
                getDataSetAfterTime = False
                strErrMsg = ex.Message
            End Try

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

            Try
                With New Xydc.Platform.DataAccess.dacMyJiaotan
                    doSetReadFlag = .doSetReadFlag(strErrMsg, strUserId, strPassword, strUserXM, strLSH)
                End With
            Catch ex As Exception
                doSetReadFlag = False
                strErrMsg = ex.Message
            End Try

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
            ByVal objNewFJData As Xydc.Platform.Common.Data.grswMyJiaotanData) As Boolean

            Dim objdacXitongpeizhi As New Xydc.Platform.DataAccess.dacXitongpeizhi
            Dim objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty

            doSaveData = False
            strErrMsg = ""

            Try
                '��ȡFTP���Ӳ���
                If objdacXitongpeizhi.getFtpServerParam(strErrMsg, strUserId, strPassword, objFTPProperty) = False Then
                    GoTo errProc
                End If

                '������Ϣ
                With New Xydc.Platform.DataAccess.dacMyJiaotan
                    If .doSaveData(strErrMsg, strUserId, strPassword, objNewData, objOldData, objenumEditType, objNewFJData, objFTPProperty) = False Then
                        GoTo errProc
                    End If
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.DataAccess.dacXitongpeizhi.SafeRelease(objdacXitongpeizhi)

            doSaveData = True
            Exit Function

errProc:
            Xydc.Platform.DataAccess.dacXitongpeizhi.SafeRelease(objdacXitongpeizhi)
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

            Try
                With New Xydc.Platform.DataAccess.dacMyJiaotan
                    getFujianDataSet = .getFujianDataSet(strErrMsg, strUserId, strPassword, strWJBS, objJiaotanDataSet)
                End With
            Catch ex As Exception
                getFujianDataSet = False
                strErrMsg = ex.Message
            End Try

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

            Try
                With New Xydc.Platform.DataAccess.dacMyJiaotan
                    getFujianDataSet = .getFujianDataSet(strErrMsg, strUserId, strPassword, strWJBS, strWJXH, objJiaotanDataSet)
                End With
            Catch ex As Exception
                getFujianDataSet = False
                strErrMsg = ex.Message
            End Try

        End Function

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

            Try
                With New Xydc.Platform.DataAccess.dacMyJiaotan
                    doExportToExcel = .doExportToExcel(strErrMsg, objDataSet, strExcelFile)
                End With
            Catch ex As Exception
                doExportToExcel = False
                strErrMsg = ex.Message
            End Try

        End Function

    End Class 'rulesMyJiaotan

End Namespace 'Xydc.Platform.BusinessRules
