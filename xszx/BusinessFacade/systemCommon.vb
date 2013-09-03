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
    ' ����    ��systemCommon
    '
    ' ���������� 
    '   ���ṩ��ͨ��������Ϣ����ı��ֲ�֧��
    '----------------------------------------------------------------
    Public Class systemCommon
        Inherits MarshalByRefObject

        '----------------------------------------------------------------
        ' ��ȫ�ͷű�����Դ
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.systemCommon)
            Try
                If Not (obj Is Nothing) Then
                    'obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub









        '----------------------------------------------------------------
        ' ��ȡ��¼��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strTable             ������
        '     strWhere             : ����
        '     strOrderby           : ����
        '     objDataSet           ����Ϣ���ݼ� 
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strTable As String, _
            ByVal strWhere As String, _
            ByVal strOrderby As String, _
            ByRef objDataSet As System.Data.DataSet) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCommon
                    getDataSet = .getDataSet(strErrMsg, strUserId, strPassword, strTable, strWhere, strOrderby, objDataSet)
                End With
            Catch ex As Exception
                getDataSet = False
                strErrMsg = ex.Message
            End Try

        End Function




        '----------------------------------------------------------------
        ' ��������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strTable             ������
        '     strWhere             : ����
        '     objType              ��true-�ֶα���û�д����ͣ����Զ��壻FALSE-�ֶα��������ĸ�����Դ�����
        '                          'C=�ַ��ͣ�i=�����ͣ�d=����           
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
            ByVal strTable As String, _
            ByVal strWhere As String, _
            ByVal objType As Boolean, _
            ByVal objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCommon
                    doSaveData = .doSaveData(strErrMsg, strUserId, strPassword, strTable, strWhere, objType, objNewData, objenumEditType)
                End With
            Catch ex As Exception
                doSaveData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ɾ������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strTable             ������
        '     strWhere             : ����
        '     objOldData           ��������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doDeleteData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strTable As String, _
            ByVal strWhere As String, _
            ByVal objOldData As System.Data.DataRow) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCommon
                    doDeleteData = .doDeleteData(strErrMsg, strUserId, strPassword, strTable, strWhere, objOldData)
                End With
            Catch ex As Exception
                doDeleteData = False
                strErrMsg = ex.Message
            End Try
        End Function



        '----------------------------------------------------------------
        ' ����select,from,where,orderby��ȡSQL���
        '     strSelect            ��select
        '     strFrom              ��from
        '     strWhere             ��where
        '     strOrderBy           ��order by
        ' ����
        '                          ���ϳɺ��SQL
        '----------------------------------------------------------------
        Public Function getSqlString( _
            ByVal strSelect As String, _
            ByVal strFrom As String, _
            ByVal strWhere As String, _
            ByVal strOrderBy As String) As String

            Try
                With New Xydc.Platform.BusinessRules.rulesCommon
                    getSqlString = .getSqlString(strSelect, strFrom, strWhere, strOrderBy)
                End With
            Catch ex As Exception
                getSqlString = ""
            End Try

        End Function

        '----------------------------------------------------------------
        ' ����SQL����ȡ��׼��DataSet
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strSQL               ��SQL���
        '     objDataSet           ���������ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getDataSetBySQL( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strSQL As String, _
            ByRef objDataSet As System.Data.DataSet) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCommon
                    getDataSetBySQL = .getDataSetBySQL(strErrMsg, strUserId, strPassword, strSQL, objDataSet)
                End With
            Catch ex As Exception
                getDataSetBySQL = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��objDataTable��strField��������strValue
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objDataTable         ����objDataTable������
        '     strField             ����objDataTable������strField
        '     strValue             ��Ҫ������ֵ
        '     blnFound             ��True-���ڣ�False-������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doFindInDataTable( _
            ByRef strErrMsg As String, _
            ByVal objDataTable As System.Data.DataTable, _
            ByVal strField As String, _
            ByVal strValue As String, _
            ByRef blnFound As Boolean) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCommon
                    doFindInDataTable = .doFindInDataTable(strErrMsg, objDataTable, strField, strValue, blnFound)
                End With
            Catch ex As Exception
                doFindInDataTable = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��objDataTable��strField��������intValue
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objDataTable         ����objDataTable������
        '     strField             ����objDataTable������strField
        '     intValue             ��Ҫ������ֵ
        '     blnFound             ��True-���ڣ�False-������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doFindInDataTable( _
            ByRef strErrMsg As String, _
            ByVal objDataTable As System.Data.DataTable, _
            ByVal strField As String, _
            ByVal intValue As Integer, _
            ByRef blnFound As Boolean) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCommon
                    doFindInDataTable = .doFindInDataTable(strErrMsg, objDataTable, strField, intValue, blnFound)
                End With
            Catch ex As Exception
                doFindInDataTable = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��objDataTable��strField��������dblValue
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objDataTable         ����objDataTable������
        '     strField             ����objDataTable������strField
        '     dblValue             ��Ҫ������ֵ
        '     blnFound             ��True-���ڣ�False-������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doFindInDataTable( _
            ByRef strErrMsg As String, _
            ByVal objDataTable As System.Data.DataTable, _
            ByVal strField As String, _
            ByVal dblValue As Double, _
            ByRef blnFound As Boolean) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCommon
                    doFindInDataTable = .doFindInDataTable(strErrMsg, objDataTable, strField, dblValue, blnFound)
                End With
            Catch ex As Exception
                doFindInDataTable = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ָ��FTPλ�������ļ���ָ����WEB������Ŀ¼�µ��ļ���
        ' ���ָ����strDesSpec����ɲ�����strDesPath��strDesFile
        ' ���δָ��strDesSpec�����������strDesPath
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strFTPPath           ��ָ��FTPλ��(·�����ļ���)
        '     strDesSpec           ������WEB������Ŀ¼+�ļ�(����)
        '     strDesPath           ��WEB������Ŀ¼(����)
        '     strDesFile           ��WEB������Ŀ¼����ʱ�ļ���(����)
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doFTPDownLoadFile( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strFTPPath As String, _
            ByRef strDesSpec As String, _
            ByRef strDesPath As String, _
            ByRef strDesFile As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCommon
                    doFTPDownLoadFile = .doFTPDownLoadFile(strErrMsg, strUserId, strPassword, strFTPPath, strDesSpec, strDesPath, strDesFile)
                End With
            Catch ex As Exception
                doFTPDownLoadFile = False
                strErrMsg = ex.Message
            End Try

        End Function

    End Class

End Namespace
