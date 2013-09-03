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
    ' ����    ��systemXitongpeizhi
    '
    ' ���������� 
    '   ���ṩ�ԡ�ϵͳ���á������Ϣ����ı��ֲ�֧��
    '----------------------------------------------------------------
    Public Class systemXitongpeizhi
        Inherits MarshalByRefObject








        '----------------------------------------------------------------
        ' ��ȫ�ͷű�����Դ
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.systemXitongpeizhi)
            Try
                If Not (obj Is Nothing) Then
                    'obj.Dispose()
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
            Try
                With New Xydc.Platform.BusinessRules.rulesXitongpeizhi
                    getXitongcanshuSQL = .getXitongcanshuSQL()
                End With
            Catch ex As Exception
                getXitongcanshuSQL = ""
            End Try
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

            Try
                With New Xydc.Platform.BusinessRules.rulesXitongpeizhi
                    getXitongcanshuData = .getXitongcanshuData(strErrMsg, strUserId, strPassword, strWhere, objXitongcanshuData)
                End With
            Catch ex As Exception
                getXitongcanshuData = False
                strErrMsg = ex.Message
            End Try

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

            Try
                With New Xydc.Platform.BusinessRules.rulesXitongpeizhi
                    doSaveXitongcanshuData = .doSaveXitongcanshuData(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType)
                End With
            Catch ex As Exception
                doSaveXitongcanshuData = False
                strErrMsg = ex.Message
            End Try

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

            Try
                With New Xydc.Platform.BusinessRules.rulesXitongpeizhi
                    doDeleteXitongcanshuData = .doDeleteXitongcanshuData(strErrMsg, strUserId, strPassword, objOldData)
                End With
            Catch ex As Exception
                doDeleteXitongcanshuData = False
                strErrMsg = ex.Message
            End Try

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

            Try
                With New Xydc.Platform.BusinessRules.rulesXitongpeizhi
                    getFtpServerParam = .getFtpServerParam(strErrMsg, strUserId, strPassword, objFTPProperty)
                End With
            Catch ex As Exception
                getFtpServerParam = False
                strErrMsg = ex.Message
            End Try

        End Function

    End Class

End Namespace
