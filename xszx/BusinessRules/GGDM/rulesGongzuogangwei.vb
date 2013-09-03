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
    ' ����    ��rulesGongzuogangwei
    '
    ' ���������� 
    '   ���ṩ�Թ�����λ��Ϣ�����ҵ�����
    '----------------------------------------------------------------
    Public Class rulesGongzuogangwei

        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
        End Sub

        '----------------------------------------------------------------
        ' ��ȫ�ͷű�����Դ
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessRules.rulesGongzuogangwei)
            Try
                If Not (obj Is Nothing) Then
                    'obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub











        '----------------------------------------------------------------
        ' ��ȡ������_B_������λ����SQL���(�Ը�λ������������)
        ' ����
        '                          ��SQL
        '----------------------------------------------------------------
        Public Function getGongzuogangweiSQL() As String
            Try
                With New Xydc.Platform.DataAccess.dacGongzuogangwei
                    getGongzuogangweiSQL = .getGongzuogangweiSQL()
                End With
            Catch ex As Exception
                getGongzuogangweiSQL = ""
            End Try
        End Function

        '----------------------------------------------------------------
        ' ��ȡ������_B_������λ����ȫ���ݵ����ݼ�(�Ը�λ������������)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strWhere             �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     objGongzuogangweiData����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getGangweiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objGongzuogangweiData As Xydc.Platform.Common.Data.GongzuogangweiData) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacGongzuogangwei
                    getGangweiData = .getGangweiData(strErrMsg, strUserId, strPassword, strWhere, objGongzuogangweiData)
                End With
            Catch ex As Exception
                getGangweiData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���桰����_B_������λ��������
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
        Public Function doSaveGongzuogangweiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacGongzuogangwei
                    '�������
                    If .doVerifyGongzuogangweiData(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType) = False Then
                        doSaveGongzuogangweiData = False
                        Exit Try
                    End If
                    '��������
                    doSaveGongzuogangweiData = .doSaveGongzuogangweiData(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType)
                End With
            Catch ex As Exception
                doSaveGongzuogangweiData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ɾ��������_B_������λ��������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     objOldData           ��������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doDeleteGongzuogangweiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacGongzuogangwei
                    doDeleteGongzuogangweiData = .doDeleteGongzuogangweiData(strErrMsg, strUserId, strPassword, objOldData)
                End With
            Catch ex As Exception
                doDeleteGongzuogangweiData = False
                strErrMsg = ex.Message
            End Try

        End Function

    End Class 'rulesGongzuogangwei

End Namespace 'Xydc.Platform.BusinessRules
