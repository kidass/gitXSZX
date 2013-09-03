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
    ' ����    ��systemAppManager
    '
    ' ���������� 
    '     �ṩ��Ӧ��ϵͳ�����ܵı��ֲ�֧��
    '----------------------------------------------------------------
    Public Class systemAppManager
        Inherits MarshalByRefObject








        '----------------------------------------------------------------
        ' ��ȫ�ͷű�����Դ
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.systemAppManager)
            Try
                If Not (obj Is Nothing) Then
                    'obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub








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

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doExportToExcel = .doExportToExcel(strErrMsg, objDataSet, strExcelFile, strMacroName, strMacroValue)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doExportToExcel = False
            End Try

        End Function









        '----------------------------------------------------------------
        ' ��ȡ��Ա����ID��������ݼ�(����֯���롢��Ա�����������)
        ' ����Ա��ȫ����������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strWhere             �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     objRenyuanData       ��ָ����֯�����µ���Ա��Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getRenyuanApplyIdData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objRenyuanData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getRenyuanApplyIdData = .getRenyuanApplyIdData(strErrMsg, strUserId, strPassword, strWhere, objRenyuanData)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getRenyuanApplyIdData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ����Login
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strLoginId           ��Ҫ�����loginId
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doApplyId( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strLoginId As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doApplyId = .doApplyId(strErrMsg, strUserId, strPassword, strLoginId)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doApplyId = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ע��Login
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strLoginId           ��Ҫע����loginId
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doDropId( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strLoginId As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doDropId = .doDropId(strErrMsg, strUserId, strPassword, strLoginId)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doDropId = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���Login
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     blnISNull            ��TRUE-�����룬FALSE-δ����
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strLoginId           ��Ҫ����loginId
        ' ����
        '     True                 ��������
        '     False                ��δ����

        '----------------------------------------------------------------
        Public Function doCheckId( _
            ByRef strErrMsg As String, _
            ByRef blnISNull As Boolean, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strLoginId As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doCheckId = .doCheckId(strErrMsg, blnISNull, strUserId, strPassword, strLoginId)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doCheckId = False
            End Try

        End Function


        '----------------------------------------------------------------
        ' ��ȡ������_B_���ݿ�_�������������ݼ�(��������������)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strWhere             �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     objFuwuqiData        ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getFuwuqiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objFuwuqiData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getFuwuqiData = .getFuwuqiData(strErrMsg, strUserId, strPassword, strWhere, objFuwuqiData)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getFuwuqiData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���ݷ���������ȡ������_B_���ݿ�_�������������ݼ�(��������������)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strServerName        ����������
        '     strWhere             �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     objFuwuqiData        ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getFuwuqiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strServerName As String, _
            ByVal strWhere As String, _
            ByRef objFuwuqiData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getFuwuqiData = .getFuwuqiData(strErrMsg, strUserId, strPassword, strServerName, strWhere, objFuwuqiData)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getFuwuqiData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���ݼ������Ӵ���ȡ���Ӳ���
        '     strErrMsg             ����������򷵻ش�����Ϣ
        '     objConnectionProperty ���û���ʶ
        '     value                 �������ַ����ļ�������
        ' ����
        '     True                  ���ɹ�
        '     False                 ��ʧ��
        '----------------------------------------------------------------
        Public Function getServerConnectionProperty( _
            ByRef strErrMsg As String, _
            ByRef objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal value As Object) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getServerConnectionProperty = .getServerConnectionProperty(strErrMsg, objConnectionProperty, value)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getServerConnectionProperty = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���ݷ���������ȡ���Ӳ���
        '     strErrMsg             ����������򷵻ش�����Ϣ
        '     strUserId             ���û���ʶ
        '     strPassword           ���û�����
        '     strServerName         ����������
        '     objConnectionProperty ���������Ӳ���
        ' ����
        '     True                  ���ɹ�
        '     False                 ��ʧ��
        '----------------------------------------------------------------
        Public Function getServerConnectionProperty( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strServerName As String, _
            ByRef objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getServerConnectionProperty = .getServerConnectionProperty(strErrMsg, strUserId, strPassword, strServerName, objConnectionProperty)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getServerConnectionProperty = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡ������_B_���ݿ�_���ݿ⡱�����ݼ�(�Է������������ݿ�����������)
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     objConnectionProperty ����������Ϣ
        '     strWhere                    �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     objShujukuData              ����Ϣ���ݼ�
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function getShujukuData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strWhere As String, _
            ByRef objShujukuData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getShujukuData = .getShujukuData(strErrMsg, objConnectionProperty, strWhere, objShujukuData)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getShujukuData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡ������_B_���ݿ�_���󡱵����ݼ�(�����ݿ�����������)
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     objConnectionProperty ����������Ϣ
        '     strWhere                    �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     objDuixiangData             ����Ϣ���ݼ�
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function getDuixiangData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strWhere As String, _
            ByRef objDuixiangData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getDuixiangData = .getDuixiangData(strErrMsg, objConnectionProperty, strWhere, objDuixiangData)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getDuixiangData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���桰����_B_���ݿ�_��������������
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
        Public Function doSaveFuwuqiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.ListDictionary, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doSaveFuwuqiData = .doSaveFuwuqiData(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doSaveFuwuqiData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ɾ��������_B_���ݿ�_��������������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strServerName        ����������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doDeleteFuwuqiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strServerName As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doDeleteFuwuqiData = .doDeleteFuwuqiData(strErrMsg, strUserId, strPassword, strServerName)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doDeleteFuwuqiData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���ݷ������������ݿ�����ȡ������_B_���ݿ�_���ݿ⡱�����ݼ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strServerName        ����������
        '     strDBName            �����ݿ���
        '     strWhere             �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     objShujukuData       ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getShujukuData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strServerName As String, _
            ByVal strDBName As String, _
            ByVal strWhere As String, _
            ByRef objShujukuData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getShujukuData = .getShujukuData(strErrMsg, strUserId, strPassword, strServerName, strDBName, strWhere, objShujukuData)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getShujukuData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���ݷ������������ݿ������������ơ���������
        ' ��ȡ������_B_���ݿ�_���󡱵����ݼ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strServerName        ����������
        '     strDBName            �����ݿ���
        '     strDXLX              �����ݿ��������
        '     strDXMC              �����ݿ������
        '     strWhere             �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     objDuixiangData      ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getDuixiangData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strServerName As String, _
            ByVal strDBName As String, _
            ByVal strDXLX As String, _
            ByVal strDXMC As String, _
            ByVal strWhere As String, _
            ByRef objDuixiangData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getDuixiangData = .getDuixiangData(strErrMsg, strUserId, strPassword, strServerName, strDBName, strDXLX, strDXMC, strWhere, objDuixiangData)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getDuixiangData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���ݶ����ʶ��ȡ������_B_���ݿ�_���󡱵����ݼ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     intDXBS              �������ʶ
        '     strWhere             �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     objDuixiangData      ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getDuixiangData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intDXBS As Integer, _
            ByVal strWhere As String, _
            ByRef objDuixiangData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getDuixiangData = .getDuixiangData(strErrMsg, strUserId, strPassword, intDXBS, strWhere, objDuixiangData)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getDuixiangData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���桰����_B_���ݿ�_���ݿ⡱������
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
        Public Function doSaveShujukuData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.ListDictionary, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doSaveShujukuData = .doSaveShujukuData(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doSaveShujukuData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���桰����_B_���ݿ�_���󡱵�����
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
        Public Function doSaveDuixiangData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.ListDictionary, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doSaveDuixiangData = .doSaveDuixiangData(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doSaveDuixiangData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ɾ��������_B_���ݿ�_���ݿ⡱������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strServerName        ����������
        '     strDBName            �����ݿ���
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doDeleteShujukuData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strServerName As String, _
            ByVal strDBName As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doDeleteShujukuData = .doDeleteShujukuData(strErrMsg, strUserId, strPassword, strServerName, strDBName)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doDeleteShujukuData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ɾ��������_B_���ݿ�_���󡱵�����
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strServerName        ����������
        '     strDBName            �����ݿ���
        '     strDXLX              ����������
        '     strDXMC              ����������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doDeleteDuixiangData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strServerName As String, _
            ByVal strDBName As String, _
            ByVal strDXLX As String, _
            ByVal strDXMC As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doDeleteDuixiangData = .doDeleteDuixiangData(strErrMsg, strUserId, strPassword, strServerName, strDBName, strDXLX, strDXMC)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doDeleteDuixiangData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ɾ��������_B_���ݿ�_���󡱵�����
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     intDXBS              �������ʶ
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doDeleteDuixiangData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intDXBS As Integer) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doDeleteDuixiangData = .doDeleteDuixiangData(strErrMsg, strUserId, strPassword, intDXBS)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doDeleteDuixiangData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' �Զ��������_B_���ݿ�_���ݿ⡢����_B_���ݿ�_�����е���Ч����
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doAutoCleanManageData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doAutoCleanManageData = .doAutoCleanManageData(strErrMsg, strUserId, strPassword)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doAutoCleanManageData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡָ��objConnectionProperty�е����ݿ��ɫ
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     objConnectionProperty ����������Ϣ
        '     strWhere                    �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     objRoleData                 ����Ϣ���ݼ�
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function getRoleData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strWhere As String, _
            ByRef objRoleData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getRoleData = .getRoleData(strErrMsg, objConnectionProperty, strWhere, objRoleData)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getRoleData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡ�Ѿ����뵽��ɫstrRoleName����Ա�б�(����Ա��ȫ����������)
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     objConnectionProperty       ����������Ϣ
        '     strRoleName                 ����ɫ��
        '     strWhere                    �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     objRenyuanData              ��ָ����֯�����µ���Ա��Ϣ���ݼ�
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function getRenyuanInRoleData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strRoleName As String, _
            ByVal strWhere As String, _
            ByRef objRenyuanData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getRenyuanInRoleData = .getRenyuanInRoleData(strErrMsg, objConnectionProperty, strRoleName, strWhere, objRenyuanData)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getRenyuanInRoleData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡû�м��뵽��ɫstrRoleName����Ա�б�(����Ա��ȫ����������)
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     objConnectionProperty       ����������Ϣ
        '     strRoleName                 ����ɫ��
        '     strWhere                    �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     objRenyuanData              ��ָ����֯�����µ���Ա��Ϣ���ݼ�
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function getRenyuanNotInRoleData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strRoleName As String, _
            ByVal strWhere As String, _
            ByRef objRenyuanData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getRenyuanNotInRoleData = .getRenyuanNotInRoleData(strErrMsg, objConnectionProperty, strRoleName, strWhere, objRenyuanData)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getRenyuanNotInRoleData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ָ��������objConnectionProperty�д�����ɫstrRoleName
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     objConnectionProperty       ����������Ϣ
        '     strRoleName                 ����ɫ��
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function doAddRole( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strRoleName As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doAddRole = .doAddRole(strErrMsg, objConnectionProperty, strRoleName)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doAddRole = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ָ��������objConnectionProperty��ɾ����ɫstrRoleName
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     objConnectionProperty       ����������Ϣ
        '     strRoleName                 ����ɫ��
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function doDropRole( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strRoleName As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doDropRole = .doDropRole(strErrMsg, objConnectionProperty, strRoleName)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doDropRole = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ָ��������objConnectionPropertyָ����ɫstrRoleName�м����Ա
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     objConnectionProperty       ����������Ϣ
        '     strRoleName                 ����ɫ��
        '     strMemberName               ����Ա��
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function doAddRoleMember( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strRoleName As String, _
            ByVal strMemberName As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doAddRoleMember = .doAddRoleMember(strErrMsg, objConnectionProperty, strRoleName, strMemberName)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doAddRoleMember = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ָ��������objConnectionPropertyָ����ɫstrRoleName��ɾ����Ա
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     objConnectionProperty       ����������Ϣ
        '     strRoleName                 ����ɫ��
        '     strMemberName               ����Ա��
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function doDropRoleMember( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strRoleName As String, _
            ByVal strMemberName As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doDropRoleMember = .doDropRoleMember(strErrMsg, objConnectionProperty, strRoleName, strMemberName)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doDropRoleMember = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡ��ɫ��Ȩ����������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objConnectionProperty�����Ӳ���
        '     strRoleName          ����ɫ��
        '     strWhere             �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     objRoleQXData        ����ɫȨ������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getRolePermissionsData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strRoleName As String, _
            ByVal strWhere As String, _
            ByRef objRoleQXData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getRolePermissionsData = .getRolePermissionsData(strErrMsg, objConnectionProperty, strRoleName, strWhere, objRoleQXData)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getRolePermissionsData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ����ɫstrRoleName����ָ������strObjectName��Ȩ��objOptions
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objConnectionProperty�����Ӳ���
        '     strRoleName          ����ɫ��
        '     strObjectName        ��������
        '     strObjectType        ����������
        '     objOptions           ����ɫȨ������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doGrantRole( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strRoleName As String, _
            ByVal strObjectName As String, _
            ByVal strObjectType As String, _
            ByVal objOptions As System.Collections.Specialized.ListDictionary) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doGrantRole = .doGrantRole(strErrMsg, objConnectionProperty, strRoleName, strObjectName, strObjectType, objOptions)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doGrantRole = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' �ӽ�ɫstrRoleName����ָ������strObjectName��Ȩ��objOptions
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objConnectionProperty�����Ӳ���
        '     strRoleName          ����ɫ��
        '     strObjectName        ��������
        '     strObjectType        ����������
        '     objOptions           ����ɫȨ������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doRevokeRole( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strRoleName As String, _
            ByVal strObjectName As String, _
            ByVal strObjectType As String, _
            ByVal objOptions As System.Collections.Specialized.ListDictionary) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doRevokeRole = .doRevokeRole(strErrMsg, objConnectionProperty, strRoleName, strObjectName, strObjectType, objOptions)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doRevokeRole = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡ��ָ�����ݿ��д�ȡȨ�޵���Ա��������ݼ�
        ' ����֯���롢��Ա�����������
        ' ����Ա��ȫ����������
        '     strErrMsg             ����������򷵻ش�����Ϣ
        '     objConnectionProperty �����Ӳ���
        '     strWhere              �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     objRenyuanGrantedData ��ָ����֯�����µ���Ա��Ϣ���ݼ�
        ' ����
        '     True                  ���ɹ�
        '     False                 ��ʧ��
        '----------------------------------------------------------------
        Public Function getRenyuanGrantedData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strWhere As String, _
            ByRef objRenyuanGrantedData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getRenyuanGrantedData = .getRenyuanGrantedData(strErrMsg, objConnectionProperty, strWhere, objRenyuanGrantedData)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getRenyuanGrantedData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡ��ָ�����ݿ�û�д�ȡȨ�޵���Ա��������ݼ�
        ' ����֯���롢��Ա�����������
        ' ����Ա��ȫ����������
        '     strErrMsg               ����������򷵻ش�����Ϣ
        '     objConnectionProperty   �����Ӳ���
        '     strWhere                �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     objRenyuanUngrantedData ��ָ����֯�����µ���Ա��Ϣ���ݼ�
        ' ����
        '     True                    ���ɹ�
        '     False                   ��ʧ��
        '----------------------------------------------------------------
        Public Function getRenyuanUngrantedData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strWhere As String, _
            ByRef objRenyuanUngrantedData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getRenyuanUngrantedData = .getRenyuanUngrantedData(strErrMsg, objConnectionProperty, strWhere, objRenyuanUngrantedData)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getRenyuanUngrantedData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��strLoginName�����ȡ���ݿ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objConnectionProperty�����Ӳ���
        '     strLoginName         ����ɫ��
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doGrantDatabase( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strLoginName As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doGrantDatabase = .doGrantDatabase(strErrMsg, objConnectionProperty, strLoginName)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doGrantDatabase = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��strLoginNameȡ����ȡ���ݿ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objConnectionProperty�����Ӳ���
        '     strLoginName         ����ɫ��
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doRevokeDatabase( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strLoginName As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doRevokeDatabase = .doRevokeDatabase(strErrMsg, objConnectionProperty, strLoginName)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doRevokeDatabase = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡָ��objConnectionProperty�е����ݿ���û�
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     objConnectionProperty       ����������Ϣ
        '     strWhere                    �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     objDBUserData               ����Ϣ���ݼ�
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function getDBUserData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strWhere As String, _
            ByRef objDBUserData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getDBUserData = .getDBUserData(strErrMsg, objConnectionProperty, strWhere, objDBUserData)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getDBUserData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡ��ɫ��Ȩ����������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objConnectionProperty�����Ӳ���
        '     strDBUserName        ���û���
        '     strWhere             �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     objDBUserQXData      ����ɫȨ������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getDBUserPermissionsData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strDBUserName As String, _
            ByVal strWhere As String, _
            ByRef objDBUserQXData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getDBUserPermissionsData = .getDBUserPermissionsData(strErrMsg, objConnectionProperty, strDBUserName, strWhere, objDBUserQXData)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getDBUserPermissionsData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���û�strDBUserName����ָ������strObjectName��Ȩ��objOptions
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objConnectionProperty�����Ӳ���
        '     strDBUserName        ���û���
        '     strObjectName        ��������
        '     strObjectType        ����������
        '     objOptions           ����ɫȨ������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doGrantDBUser( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strDBUserName As String, _
            ByVal strObjectName As String, _
            ByVal strObjectType As String, _
            ByVal objOptions As System.Collections.Specialized.ListDictionary) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doGrantDBUser = .doGrantDBUser(strErrMsg, objConnectionProperty, strDBUserName, strObjectName, strObjectType, objOptions)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doGrantDBUser = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���û�strDBUserName����ָ������strObjectName��Ȩ��objOptions
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objConnectionProperty�����Ӳ���
        '     strDBUserName        ���û���
        '     strObjectName        ��������
        '     strObjectType        ����������
        '     objOptions           ����ɫȨ������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doRevokeDBUser( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strDBUserName As String, _
            ByVal strObjectName As String, _
            ByVal strObjectType As String, _
            ByVal objOptions As System.Collections.Specialized.ListDictionary) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doRevokeDBUser = .doRevokeDBUser(strErrMsg, objConnectionProperty, strDBUserName, strObjectName, strObjectType, objOptions)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doRevokeDBUser = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡ������_B_Ӧ��ϵͳ_ģ�顱�����ݼ�(��ģ�������������)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strWhere             �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     objMokuaiData        ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getMokuaiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objMokuaiData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getMokuaiData = .getMokuaiData(strErrMsg, strUserId, strPassword, strWhere, objMokuaiData)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getMokuaiData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡָ��strMKDM�¼��ġ�����_B_Ӧ��ϵͳ_ģ�顱�����ݼ�(��ģ�������������)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strMKDM              ��ģ�����
        '     strWhere             �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     objMokuaiData        ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getMokuaiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strMKDM As String, _
            ByVal strWhere As String, _
            ByRef objMokuaiData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getMokuaiData = .getMokuaiData(strErrMsg, strUserId, strPassword, strMKDM, strWhere, objMokuaiData)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getMokuaiData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ����ָ��strMKDM��ȡ������_B_Ӧ��ϵͳ_ģ�顱�����ݼ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strMKDM              ��ģ�����
        '     blnUnused            ��������
        '     objMokuaiData        ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getMokuaiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strMKDM As String, _
            ByVal blnUnused As Boolean, _
            ByRef objMokuaiData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getMokuaiData = .getMokuaiData(strErrMsg, strUserId, strPassword, strMKDM, blnUnused, objMokuaiData)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getMokuaiData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ����ָ��strMKDM��ȡ������_B_Ӧ��ϵͳ_ģ�顱�����ݼ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     intMKBS              ��ģ���ʶ
        '     blnUnused            ��������
        '     objMokuaiData        ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getMokuaiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intMKBS As Integer, _
            ByVal blnUnused As Boolean, _
            ByRef objMokuaiData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getMokuaiData = .getMokuaiData(strErrMsg, strUserId, strPassword, intMKBS, blnUnused, objMokuaiData)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getMokuaiData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' �����ϼ�ģ������ȡ�¼���ģ�����
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strPrevMKDM          ���ϼ�ģ�����
        '     strNewMKDM           ����ģ�����(����)
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getNewMKDM( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strPrevMKDM As String, _
            ByRef strNewMKDM As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getNewMKDM = .getNewMKDM(strErrMsg, strUserId, strPassword, strPrevMKDM, strNewMKDM)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getNewMKDM = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���桰����_B_Ӧ��ϵͳ_ģ�顱������
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
        Public Function doSaveMokuaiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.ListDictionary, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doSaveMokuaiData = .doSaveMokuaiData(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doSaveMokuaiData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ����ģ�����ɾ��������_B_Ӧ��ϵͳ_ģ�顱������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strMKDM              ��ģ�����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doDeleteMokuaiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strMKDM As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doDeleteMokuaiData = .doDeleteMokuaiData(strErrMsg, strUserId, strPassword, strMKDM)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doDeleteMokuaiData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡ��ɫ��ģ��Ȩ����������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objConnectionProperty�����Ӳ���
        '     strRoleName          ����ɫ��
        '     strWhere             �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     objRoleMKQXData      ����ɫȨ������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getRoleMokuaiQXData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strRoleName As String, _
            ByVal strWhere As String, _
            ByRef objRoleMKQXData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getRoleMokuaiQXData = .getRoleMokuaiQXData(strErrMsg, objConnectionProperty, strRoleName, strWhere, objRoleMKQXData)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getRoleMokuaiQXData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡ�û���ģ��Ȩ����������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objConnectionProperty�����Ӳ���
        '     strDBUserName        ���û���
        '     strWhere             �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     objDBUserMKQXData    ����ɫȨ������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getDBUserMokuaiQXData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strDBUserName As String, _
            ByVal strWhere As String, _
            ByRef objDBUserMKQXData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getDBUserMokuaiQXData = .getDBUserMokuaiQXData(strErrMsg, objConnectionProperty, strDBUserName, strWhere, objDBUserMKQXData)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getDBUserMokuaiQXData = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ����ɫstrRoleName����ָ��ģ��strMKBS��Ȩ��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strRoleName          ����ɫ��
        '     strMKBS              ��ģ���ʶ
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doGrantRoleMokuaiQX( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRoleName As String, _
            ByVal strMKBS As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doGrantRoleMokuaiQX = .doGrantRoleMokuaiQX(strErrMsg, strUserId, strPassword, strRoleName, strMKBS)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doGrantRoleMokuaiQX = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' �ӽ�ɫstrRoleName����ָ��ģ��strMKBS��Ȩ��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strRoleName          ����ɫ��
        '     strMKBS              ��ģ���ʶ
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doRevokeRoleMokuaiQX( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRoleName As String, _
            ByVal strMKBS As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doRevokeRoleMokuaiQX = .doRevokeRoleMokuaiQX(strErrMsg, strUserId, strPassword, strRoleName, strMKBS)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doRevokeRoleMokuaiQX = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���û�strDBUserName����ָ��ģ��strMKBS��Ȩ��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strDBUserName        ���û���
        '     strMKBS              ��ģ���ʶ
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doGrantDBuserMokuaiQX( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strDBUserName As String, _
            ByVal strMKBS As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doGrantDBuserMokuaiQX = .doGrantDBuserMokuaiQX(strErrMsg, strUserId, strPassword, strDBUserName, strMKBS)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doGrantDBuserMokuaiQX = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���û�strDBUserName����ָ��ģ��strMKBS��Ȩ��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strDBUserName        ���û���
        '     strMKBS              ��ģ���ʶ
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doRevokeDBUserMokuaiQX( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strDBUserName As String, _
            ByVal strMKBS As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doRevokeDBUserMokuaiQX = .doRevokeDBUserMokuaiQX(strErrMsg, strUserId, strPassword, strDBUserName, strMKBS)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doRevokeDBUserMokuaiQX = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡ�û���ģ��Ȩ����������(ͬʱ����û�������ɫ��Ȩ������)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strDBUserName        ���û���
        '     objDBUserMKQXData    ����ɫȨ������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getDBUserMokuaiQXData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strDBUserName As String, _
            ByRef objDBUserMKQXData As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getDBUserMokuaiQXData = .getDBUserMokuaiQXData(strErrMsg, strUserId, strPassword, strDBUserName, objDBUserMKQXData)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getDBUserMokuaiQXData = False
            End Try

        End Function







        '----------------------------------------------------------------
        ' ��ȡһ���û�������־
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strTempPath          ����ʱ�ļ�Ŀ¼
        '     strWhere             �������ַ���(���ݼ������ַ���)
        '     objLogDataSet        ����������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getDataSet_JSOALOG( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strTempPath As String, _
            ByVal strWhere As String, _
            ByRef objLogDataSet As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getDataSet_JSOALOG = .getDataSet_JSOALOG(strErrMsg, strUserId, strPassword, strTempPath, strWhere, objLogDataSet)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getDataSet_JSOALOG = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡ���ù���Ա������־
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strTempPath          ����ʱ�ļ�Ŀ¼
        '     strWhere             �������ַ���(���ݼ������ַ���)
        '     objLogDataSet        ����������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getDataSet_AUDITPZLOG( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strTempPath As String, _
            ByVal strWhere As String, _
            ByRef objLogDataSet As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getDataSet_AUDITPZLOG = .getDataSet_AUDITPZLOG(strErrMsg, strUserId, strPassword, strTempPath, strWhere, objLogDataSet)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getDataSet_AUDITPZLOG = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡ��ȫ����Ա������־
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strTempPath          ����ʱ�ļ�Ŀ¼
        '     strWhere             �������ַ���(���ݼ������ַ���)
        '     objLogDataSet        ����������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getDataSet_AUDITAQLOG( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strTempPath As String, _
            ByVal strWhere As String, _
            ByRef objLogDataSet As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getDataSet_AUDITAQLOG = .getDataSet_AUDITAQLOG(strErrMsg, strUserId, strPassword, strTempPath, strWhere, objLogDataSet)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getDataSet_AUDITAQLOG = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡ��ƹ���Ա������־
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strTempPath          ����ʱ�ļ�Ŀ¼
        '     strWhere             �������ַ���(���ݼ������ַ���)
        '     objLogDataSet        ����������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getDataSet_AUDITSJLOG( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strTempPath As String, _
            ByVal strWhere As String, _
            ByRef objLogDataSet As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getDataSet_AUDITSJLOG = .getDataSet_AUDITSJLOG(strErrMsg, strUserId, strPassword, strTempPath, strWhere, objLogDataSet)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getDataSet_AUDITSJLOG = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡ��Ա�Ѿ����뵽��ɫstrRoleName���б�
        '----------------------------------------------------------------
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     objConnectionProperty       ����������Ϣ
        '     strWhere                    �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     objRoleData                 ����Ϣ���ݼ�
        '     blnNone                     ������
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��

        '----------------------------------------------------------------
        Public Function getRoleData( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strWhere As String, _
            ByRef objRoleData As Xydc.Platform.Common.Data.AppManagerData, _
            ByVal blnNone As Boolean) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    getRoleData = .getRoleData(strErrMsg, objConnectionProperty, strWhere, objRoleData, blnNone)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                getRoleData = False
            End Try

        End Function

        '-------------------------------------------------------------------------------------------
        ' ��ָ��������objConnectionPropertyָ����ԱstrUserId�����ɫ(m_objNewDataSet_ChoiceRole)��
        '-------------------------------------------------------------------------------------------
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     objConnectionProperty       ����������Ϣ
        '     strUserId                   ��ָ����Ա
        '     m_objNewDataSet_ChoiceRole  �����½�ɫ���ݼ�
        '     m_objOldDataSet_ChoiceRole  ��ԭ��ɫ���ݼ�
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��

        '----------------------------------------------------------------
        Public Function doAddRoleMember( _
            ByRef strErrMsg As String, _
            ByVal objConnectionProperty As Xydc.Platform.Common.Utilities.ConnectionProperty, _
            ByVal strUserId As String, _
            ByVal m_objNewDataSet_ChoiceRole As Xydc.Platform.Common.Data.AppManagerData, _
            ByVal m_objOldDataSet_ChoiceRole As Xydc.Platform.Common.Data.AppManagerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesAppManager
                    doAddRoleMember = .doAddRoleMember(strErrMsg, objConnectionProperty, strUserId, m_objNewDataSet_ChoiceRole, m_objOldDataSet_ChoiceRole)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doAddRoleMember = False
            End Try

        End Function


    End Class

End Namespace
