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
Imports System.Security.Cryptography
Imports Microsoft.VisualBasic

Imports Xydc.Platform.SystemFramework
Imports Xydc.Platform.Common.Data
Imports Xydc.Platform.BusinessRules

Namespace Xydc.Platform.BusinessFacade
    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��systemMyJiaotan
    '
    ' ���������� 
    '     �ṩ�ԡ�����_B_��̸��ģ���漰�ı��ֲ����
    '----------------------------------------------------------------
    Public Class systemMyJiaotan
        Inherits MarshalByRefObject

        'chat_ydxx.aspx��chat_fsxx.aspx��QueryString��������
        Public Const QUERYSTRING_LSH As String = "LSH"

        'chat_xzfj.aspx��QueryString��������
        Public Const QUERYSTRING_WJBS As String = "WJBS"
        Public Const QUERYSTRING_WJXH As String = "WJXH"







        '----------------------------------------------------------------
        ' ��ȫ�ͷű�����Դ
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.systemMyJiaotan)
            Try
                If Not (obj Is Nothing) Then
                    'obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub








        '----------------------------------------------------------------
        ' ��ȡ����������Ϣ����ʾ(HTML��ʽ)
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strUserId              ���û���ʶ
        '     strPassword            ���û�����
        '     strUserXM              ����ǰ����Ա����
        '     objChatDataRow         ��Ҫ��ʾ�Ľ�̸����
        '     strNotice              �����ص���������Ϣ����ʾ
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Function getDisplayContent( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByVal objChatDataRow As System.Data.DataRow, _
            ByRef strNotice As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData
            Dim strNoticeFJ As String = ""

            getDisplayContent = False
            strNotice = ""
            strErrMsg = ""

            Try
                Dim strMessage As String
                Dim strWYBS As String
                Dim strFSRQ As String
                Dim strFSR As String
                Dim strJSR As String
                Dim strLSH As String
                Dim intBZ As Integer
                Dim datFSRQ As DateTime

                '��ȡ��Ϣ
                strMessage = objPulicParameters.getObjectValue(objChatDataRow.Item(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_XX), "")
                strWYBS = objPulicParameters.getObjectValue(objChatDataRow.Item(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_WYBS), "")
                datFSRQ = objPulicParameters.getObjectValue(objChatDataRow.Item(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_FSSJ), datFSRQ)
                strFSRQ = Format(datFSRQ, "MM-dd HH:mm")
                strLSH = objPulicParameters.getObjectValue(objChatDataRow.Item(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_LSH), "")
                strFSR = objPulicParameters.getObjectValue(objChatDataRow.Item(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_FSR), "")
                strJSR = objPulicParameters.getObjectValue(objChatDataRow.Item(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_JSR), "")
                intBZ = objPulicParameters.getObjectValue(objChatDataRow.Item(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_BZ), 0)

                '��ȡ������Ϣ
                If Me.getFujianDataSet(strErrMsg, strUserId, strPassword, strWYBS, objDataSet) = False Then
                    GoTo errProc
                End If


                '���㸽����Ϣ
                Dim strValue(3) As String
                Dim strAttach As String = ""
                Dim intCount As Integer
                Dim i As Integer
                If Not (objDataSet Is Nothing) Then
                    If Not (objDataSet.Tables(Xydc.Platform.Common.Data.grswMyJiaotanData.TABLE_GG_B_JIAOTAN_FUJIAN) Is Nothing) Then
                        With objDataSet.Tables(Xydc.Platform.Common.Data.grswMyJiaotanData.TABLE_GG_B_JIAOTAN_FUJIAN)
                            intCount = .Rows.Count
                            For i = 0 To intCount - 1 Step 1
                                strValue(0) = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_FUJIAN_WJSM), "")
                                strValue(1) = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_FUJIAN_WJYS), "")
                                strValue(2) = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_FUJIAN_WJXH), "")

                                strValue(3) = strValue(0) + "(" + strValue(1) + "KB)"
                                strValue(3) = "<a href='chat_xzfj.aspx?WJBS=" + strWYBS + "&WJXH=" + strValue(2) + "' target='_blank'>" + strValue(3) + "</a>"

                                If strAttach = "" Then
                                    strAttach = strValue(3)
                                Else
                                    strAttach = strAttach + "&nbsp;&nbsp;" + strValue(3)
                                End If
                            Next
                        End With
                    End If
                End If
                If strAttach <> "" Then
                    strNoticeFJ = "&nbsp;&nbsp;" + strAttach
                Else
                    strNoticeFJ = strAttach
                End If

                '������ʾ��Ϣ
                If strFSR.ToUpper = strUserXM.ToUpper Then
                    If strNotice = "" Then
                        strNotice = "[" + strFSRQ + "][" + strJSR + "��]��" + strMessage + strNoticeFJ + "<br>"
                    Else
                        strNotice = strNotice + "[" + strFSRQ + "][" + strJSR + "��]��" + strMessage + strNoticeFJ + "<br>"
                    End If
                Else
                    If strNotice = "" Then
                        If intBZ = 1 Then
                            strNotice = "[" + strFSRQ + "][" + strFSR + "��]��" + strMessage + strNoticeFJ + "[<a href='chat_fsxx.aspx?LSH=" + strLSH + "' target='chatFSFrame'>�ظ�</a>]<br>"
                        Else
                            strNotice = "[" + strFSRQ + "][" + strFSR + "��]��" + strMessage + strNoticeFJ + "[<a href='chat_fsxx.aspx?LSH=" + strLSH + "' target='chatFSFrame'>�ظ�</a>&nbsp;<a href='chat_ydxx.aspx?LSH=" + strLSH + "' target='chatYDFrame'>�ѿ�</a>]<br>"
                        End If
                    Else
                        If intBZ = 1 Then
                            strNotice = strNotice + "[" + strFSRQ + "][" + strFSR + "��]��" + strMessage + strNoticeFJ + "[<a href='chat_fsxx.aspx?LSH=" + strLSH + "' target='chatFSFrame'>�ظ�</a>]<br>"
                        Else
                            strNotice = strNotice + "[" + strFSRQ + "][" + strFSR + "��]��" + strMessage + strNoticeFJ + "[<a href='chat_fsxx.aspx?LSH=" + strLSH + "' target='chatFSFrame'>�ظ�</a>&nbsp;<a href='chat_ydxx.aspx?LSH=" + strLSH + "' target='chatYDFrame'>�ѿ�</a>]<br>"
                        End If
                    End If
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Data.grswMyJiaotanData.SafeRelease(objDataSet)

            getDisplayContent = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Data.grswMyJiaotanData.SafeRelease(objDataSet)
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

            Try
                With New Xydc.Platform.BusinessRules.rulesMyJiaotan
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
                With New Xydc.Platform.BusinessRules.rulesMyJiaotan
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
                With New Xydc.Platform.BusinessRules.rulesMyJiaotan
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
                With New Xydc.Platform.BusinessRules.rulesMyJiaotan
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
                With New Xydc.Platform.BusinessRules.rulesMyJiaotan
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
                With New Xydc.Platform.BusinessRules.rulesMyJiaotan
                    doSaveData = .doSaveData(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType)
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

            Try
                With New Xydc.Platform.BusinessRules.rulesMyJiaotan
                    doSendChat = .doSendChat(strErrMsg, strUserId, strPassword, strFSR, strJSR, strMsg)
                End With
            Catch ex As Exception
                doSendChat = False
                strErrMsg = ex.Message
            End Try

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
                With New Xydc.Platform.BusinessRules.rulesMyJiaotan
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
                With New Xydc.Platform.BusinessRules.rulesMyJiaotan
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
                With New Xydc.Platform.BusinessRules.rulesMyJiaotan
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
                With New Xydc.Platform.BusinessRules.rulesMyJiaotan
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
                With New Xydc.Platform.BusinessRules.rulesMyJiaotan
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

            Try
                With New Xydc.Platform.BusinessRules.rulesMyJiaotan
                    doSaveData = .doSaveData(strErrMsg, strUserId, strPassword, objNewData, objOldData, objenumEditType, objNewFJData)
                End With
            Catch ex As Exception
                doSaveData = False
                strErrMsg = ex.Message
            End Try

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
                With New Xydc.Platform.BusinessRules.rulesMyJiaotan
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
                With New Xydc.Platform.BusinessRules.rulesMyJiaotan
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
                With New Xydc.Platform.BusinessRules.rulesMyJiaotan
                    doExportToExcel = .doExportToExcel(strErrMsg, objDataSet, strExcelFile)
                End With
            Catch ex As Exception
                doExportToExcel = False
                strErrMsg = ex.Message
            End Try

        End Function

    End Class

End Namespace
