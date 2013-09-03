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
    ' ����    ��systemDianzigonggao
    '
    ' ���������� 
    '     �ṩ�ԡ����ӹ��桱ģ���漰�ı��ֲ����
    '----------------------------------------------------------------
    Public Class systemDianzigonggao
        Implements System.IDisposable

        Private m_objrulesDianzigonggao As Xydc.Platform.BusinessRules.rulesDianzigonggao








        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
            m_objrulesDianzigonggao = New Xydc.Platform.BusinessRules.rulesDianzigonggao
        End Sub

        '----------------------------------------------------------------
        ' ������������
        '----------------------------------------------------------------
        Public Sub Dispose() Implements System.IDisposable.Dispose
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
            If Not (m_objrulesDianzigonggao Is Nothing) Then
                m_objrulesDianzigonggao.Dispose()
                m_objrulesDianzigonggao = Nothing
            End If
        End Sub

        '----------------------------------------------------------------
        ' ��ȫ�ͷű�����Դ
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.systemDianzigonggao)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub









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
                With m_objrulesDianzigonggao
                    doExportToExcel = .doExportToExcel(strErrMsg, objDataSet, strExcelFile)
                End With
            Catch ex As Exception
                doExportToExcel = False
                strErrMsg = ex.Message
            End Try

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

            Try
                With m_objrulesDianzigonggao
                    getDataSet = .getDataSet(strErrMsg, strUserId, strPassword, strCzydm, strWhere, objDianzigonggaoData)
                End With
            Catch ex As Exception
                getDataSet = False
                strErrMsg = ex.Message
            End Try

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

            Try
                With m_objrulesDianzigonggao
                    getDataSet = .getDataSet(strErrMsg, strUserId, strPassword, strCzydm, intXH, objDianzigonggaoData)
                End With
            Catch ex As Exception
                getDataSet = False
                strErrMsg = ex.Message
            End Try

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

            Try
                With m_objrulesDianzigonggao
                    getDataSet = .getDataSet(strErrMsg, strUserId, strPassword, strWhere, objDianzigonggaoData)
                End With
            Catch ex As Exception
                getDataSet = False
                strErrMsg = ex.Message
            End Try

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

            Try
                With m_objrulesDianzigonggao
                    getKeYueduRenyuan = .getKeYueduRenyuan(strErrMsg, strUserId, strPassword, strCzydm, intXH, strYDRY)
                End With
            Catch ex As Exception
                getKeYueduRenyuan = False
                strErrMsg = ex.Message
            End Try

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

            Try
                With m_objrulesDianzigonggao
                    doFabu = .doFabu(strErrMsg, strUserId, strPassword, strCzydm, intXH, blnFabu)
                End With
            Catch ex As Exception
                doFabu = False
                strErrMsg = ex.Message
            End Try

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

            Try
                With m_objrulesDianzigonggao
                    doSetHasRead = .doSetHasRead(strErrMsg, strUserId, strPassword, strCzydm, intXH)
                End With
            Catch ex As Exception
                doSetHasRead = False
                strErrMsg = ex.Message
            End Try

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

            Try
                With m_objrulesDianzigonggao
                    doDelete = .doDelete(strErrMsg, strUserId, strPassword, strCzydm, intXH)
                End With
            Catch ex As Exception
                doDelete = False
                strErrMsg = ex.Message
            End Try

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

            Try
                With m_objrulesDianzigonggao
                    doSave = .doSave(strErrMsg, strUserId, strPassword, objNewData, objOldData, strFBFW, objenumEditType)
                End With
            Catch ex As Exception
                doSave = False
                strErrMsg = ex.Message
            End Try

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
        '     objDataSet_FJ          : �������ݼ�
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
            ByVal objDataSet_FJ As Xydc.Platform.Common.Data.ggxxDianzigonggaoData) As Boolean

            Try
                With m_objrulesDianzigonggao
                    doSave = .doSave(strErrMsg, strUserId, strPassword, objNewData, objOldData, strFBFW, objenumEditType, objDataSet_FJ)
                End With
            Catch ex As Exception
                doSave = False
                strErrMsg = ex.Message
            End Try

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

            Try
                With m_objrulesDianzigonggao
                    isCanRead = .isCanRead(strErrMsg, strUserId, strPassword, strCzydm, intXH, blnYuedu)
                End With
            Catch ex As Exception
                isCanRead = False
                strErrMsg = ex.Message
            End Try

        End Function


        '----------------------------------------------------------------
        ' ����strWJBS��ȡ�����ӹ���_B_�����������ݼ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId                   ���û���ʶ
        '     strPassword                 ���û�����
        '     strWJBS                     ���ļ���ʶ        '
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

            Try
                With m_objrulesDianzigonggao
                    getFujianData = .getFujianData(strErrMsg, strUserId, strPassword, strWJBS, objFujianData)
                End With
            Catch ex As Exception
                getFujianData = False
                strErrMsg = ex.Message
            End Try

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

            Try
                With m_objrulesDianzigonggao
                    doVerifyFujian = .doVerifyFujian(strErrMsg, strUserId, strPassword, objNewData)
                End With
            Catch ex As Exception
                doVerifyFujian = False
                strErrMsg = ex.Message
            End Try

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

            Try
                With m_objrulesDianzigonggao
                    doSaveFujian = .doSaveFujian(strErrMsg, blnEnforeEdit, strUserId, strPassword, strUserXM, strWJBS, objNewData)
                End With
            Catch ex As Exception
                doSaveFujian = False
                strErrMsg = ex.Message
            End Try

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

            Try
                With m_objrulesDianzigonggao
                    doDeleteData_FJ = .doDeleteData_FJ(strErrMsg, objOldData)
                End With
            Catch ex As Exception
                doDeleteData_FJ = False
                strErrMsg = ex.Message
            End Try

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

            Try
                With m_objrulesDianzigonggao
                    doAutoAdjustXSXH_FJ = .doAutoAdjustXSXH_FJ(strErrMsg, objFJData)
                End With
            Catch ex As Exception
                doAutoAdjustXSXH_FJ = False
                strErrMsg = ex.Message
            End Try

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

            Try
                With m_objrulesDianzigonggao
                    doMoveTo_FJ = .doMoveTo_FJ(strErrMsg, objSrcData, objDesData)
                End With
            Catch ex As Exception
                doMoveTo_FJ = False
                strErrMsg = ex.Message
            End Try
        End Function


    End Class

End Namespace
