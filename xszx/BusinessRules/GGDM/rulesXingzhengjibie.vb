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
    ' ����    ��rulesXingzhengjibie
    '
    ' ���������� 
    '   ���ṩ�ԡ�����_B_����������Ϣ�����ҵ�����
    '----------------------------------------------------------------
    Public Class rulesXingzhengjibie

        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
        End Sub

        '----------------------------------------------------------------
        ' ��ȫ�ͷű�����Դ
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessRules.rulesXingzhengjibie)
            Try
                If Not (obj Is Nothing) Then
                    'obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub









        '----------------------------------------------------------------
        ' ��ȡ������_B_�������𡱵�SQL���(�Լ��������������)
        ' ����
        '                          ��SQL
        '----------------------------------------------------------------
        Public Function getXingzhengjibieSQL() As String
            Try
                With New Xydc.Platform.DataAccess.dacXingzhengjibie
                    getXingzhengjibieSQL = .getXingzhengjibieSQL()
                End With
            Catch ex As Exception
                getXingzhengjibieSQL = ""
            End Try
        End Function

        '----------------------------------------------------------------
        ' ���ݼ�������ȡ������_B_�������𡱵����ݼ�
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strUserId              ���û���ʶ
        '     strPassword            ���û�����
        '     strJBDM                ���������
        '     blnUnused              ��������
        '     objXingzhengjibieData  ����Ϣ���ݼ�
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Function getXingzhengjibieData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strJBDM As String, _
            ByVal blnUnused As Boolean, _
            ByRef objXingzhengjibieData As Xydc.Platform.Common.Data.XingzhengjibieData) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacXingzhengjibie
                    getXingzhengjibieData = .getXingzhengjibieData(strErrMsg, strUserId, strPassword, strJBDM, blnUnused, objXingzhengjibieData)
                End With
            Catch ex As Exception
                getXingzhengjibieData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���ݼ������ƻ�ȡ������_B_�������𡱵����ݼ�
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strUserId              ���û���ʶ
        '     strPassword            ���û�����
        '     blnUnused              ��������
        '     strJBMC                ����������
        '     objXingzhengjibieData  ����Ϣ���ݼ�
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Function getXingzhengjibieData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal blnUnused As Boolean, _
            ByVal strJBMC As String, _
            ByRef objXingzhengjibieData As Xydc.Platform.Common.Data.XingzhengjibieData) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacXingzhengjibie
                    getXingzhengjibieData = .getXingzhengjibieData(strErrMsg, strUserId, strPassword, blnUnused, strJBMC, objXingzhengjibieData)
                End With
            Catch ex As Exception
                getXingzhengjibieData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡ������_B_�������𡱵����ݼ�(�Դ�����������)
        '     strErrMsg             ����������򷵻ش�����Ϣ
        '     strUserId             ���û���ʶ
        '     strPassword           ���û�����
        '     strWhere              �������ַ���
        '     objXingzhengjibieData ����Ϣ���ݼ�
        ' ����
        '     True                  ���ɹ�
        '     False                 ��ʧ��
        '----------------------------------------------------------------
        Public Function getXingzhengjibieData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objXingzhengjibieData As Xydc.Platform.Common.Data.XingzhengjibieData) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacXingzhengjibie
                    getXingzhengjibieData = .getXingzhengjibieData(strErrMsg, strUserId, strPassword, strWhere, objXingzhengjibieData)
                End With
            Catch ex As Exception
                getXingzhengjibieData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���桰����_B_�������𡱵�����
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
        Public Function doSaveXingzhengjibieData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacXingzhengjibie
                    '�������
                    If .doVerifyXingzhengjibieData(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType) = False Then
                        doSaveXingzhengjibieData = False
                        Exit Try
                    End If
                    '��������
                    doSaveXingzhengjibieData = .doSaveXingzhengjibieData(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType)
                End With
            Catch ex As Exception
                doSaveXingzhengjibieData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ɾ��������_B_�������𡱵�����
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     objOldData           ��������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doDeleteXingzhengjibieData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacXingzhengjibie
                    doDeleteXingzhengjibieData = .doDeleteXingzhengjibieData(strErrMsg, strUserId, strPassword, objOldData)
                End With
            Catch ex As Exception
                doDeleteXingzhengjibieData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���ݼ������ƻ�ȡ�������
        '     strErrMsg     ����������򷵻ش�����Ϣ
        '     strUserId     ���û���ʶ
        '     strPassword   ���û�����
        '     strJBMC       ����������
        '     strJBDM       ���������(����)
        ' ����
        '     True          ���ɹ�
        '     False         ��ʧ��
        '----------------------------------------------------------------
        Public Function getJbdmByJbmc( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strJBMC As String, _
            ByRef strJBDM As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objXingzhengjibieData As Xydc.Platform.Common.Data.XingzhengjibieData

            getJbdmByJbmc = False
            strJBDM = ""

            Try
                With New Xydc.Platform.DataAccess.dacXingzhengjibie
                    If .getXingzhengjibieData(strErrMsg, strUserId, strPassword, True, strJBMC, objXingzhengjibieData) = False Then
                        Exit Try
                    End If
                End With

                With objXingzhengjibieData.Tables(Xydc.Platform.Common.Data.XingzhengjibieData.TABLE_GG_B_XINGZHENGJIBIE)
                    If .Rows.Count > 0 Then
                        strJBDM = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.XingzhengjibieData.FIELD_GG_B_XINGZHENGJIBIE_JBDM), "")
                    End If
                End With

                getJbdmByJbmc = True

            Catch ex As Exception
                strErrMsg = ex.Message
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Data.XingzhengjibieData.SafeRelease(objXingzhengjibieData)

        End Function

        '----------------------------------------------------------------
        ' ���ݼ�������ȡ��������
        '     strErrMsg     ����������򷵻ش�����Ϣ
        '     strUserId     ���û���ʶ
        '     strPassword   ���û�����
        '     strRYDM       ���������
        '     strRYMC       ����������(����)
        ' ����
        '     True          ���ɹ�
        '     False         ��ʧ��
        '----------------------------------------------------------------
        Public Function getJbmcByJbdm( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strJBDM As String, _
            ByRef strJBMC As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objXingzhengjibieData As Xydc.Platform.Common.Data.XingzhengjibieData

            getJbmcByJbdm = False
            strJBMC = ""

            Try
                With New Xydc.Platform.DataAccess.dacXingzhengjibie
                    If .getXingzhengjibieData(strErrMsg, strUserId, strPassword, strJBDM, True, objXingzhengjibieData) = False Then
                        Exit Try
                    End If
                End With

                With objXingzhengjibieData.Tables(Xydc.Platform.Common.Data.XingzhengjibieData.TABLE_GG_B_XINGZHENGJIBIE)
                    If .Rows.Count > 0 Then
                        strJBMC = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.XingzhengjibieData.FIELD_GG_B_XINGZHENGJIBIE_JBMC), "")
                    End If
                End With

                getJbmcByJbdm = True

            Catch ex As Exception
                strErrMsg = ex.Message
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Data.XingzhengjibieData.SafeRelease(objXingzhengjibieData)

        End Function

    End Class 'rulesXingzhengjibie

End Namespace 'Xydc.Platform.BusinessRules
