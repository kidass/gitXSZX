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
    ' ����    ��rulesLingdaoanpai
    '
    ' ���������� 
    '     �ṩ�ԡ��쵼����š�ģ���漰��ҵ���߼������
    '----------------------------------------------------------------
    Public Class rulesLingdaoanpai
        Implements System.IDisposable

        Private m_objdacLingdaoanpai As Xydc.Platform.DataAccess.dacLingdaoanpai










        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
            m_objdacLingdaoanpai = New Xydc.Platform.DataAccess.dacLingdaoanpai
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
            If Not (m_objdacLingdaoanpai Is Nothing) Then
                m_objdacLingdaoanpai.Dispose()
                m_objdacLingdaoanpai = Nothing
            End If
        End Sub

        '----------------------------------------------------------------
        ' ��ȫ�ͷű�����Դ
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessRules.rulesLingdaoanpai)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
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
                With m_objdacLingdaoanpai
                    doExportToExcel = .doExportToExcel(strErrMsg, objDataSet, strExcelFile, strMacroName, strMacroValue)
                End With
            Catch ex As Exception
                doExportToExcel = False
                strErrMsg = ex.Message
            End Try

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

            Try
                With m_objdacLingdaoanpai
                    getDataSet = .getDataSet(strErrMsg, strUserId, strPassword, strWhere, objLingdaoanpaiData)
                End With
            Catch ex As Exception
                getDataSet = False
                strErrMsg = ex.Message
            End Try

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

            Try
                With m_objdacLingdaoanpai
                    getDataSet = .getDataSet(strErrMsg, strUserId, strPassword, strWhere, objLingdaoanpaiData, blnNone)
                End With
            Catch ex As Exception
                getDataSet = False
                strErrMsg = ex.Message
            End Try

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

            Try
                With m_objdacLingdaoanpai
                    getDataSet = .getDataSet(strErrMsg, strUserId, strPassword, intXH, objLingdaoanpaiData)
                End With
            Catch ex As Exception
                getDataSet = False
                strErrMsg = ex.Message
            End Try

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

            Try
                With m_objdacLingdaoanpai
                    getDataSet = .getDataSet(strErrMsg, strUserId, strPassword, objDate, objLingdaoanpaiData)
                End With
            Catch ex As Exception
                getDataSet = False
                strErrMsg = ex.Message
            End Try

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

            Try
                With m_objdacLingdaoanpai
                    doDelete = .doDelete(strErrMsg, strUserId, strPassword, intXH)
                End With
            Catch ex As Exception
                doDelete = False
                strErrMsg = ex.Message
            End Try

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

            Try
                With m_objdacLingdaoanpai
                    doCopy = .doCopy(strErrMsg, strUserId, strPassword, strFromRQ, strToRQ)
                End With
            Catch ex As Exception
                doCopy = False
                strErrMsg = ex.Message
            End Try

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

            Try
                With m_objdacLingdaoanpai
                    doSave = .doSave(strErrMsg, strUserId, strPassword, objNewData, objOldData, objenumEditType)
                End With
            Catch ex As Exception
                doSave = False
                strErrMsg = ex.Message
            End Try

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

            Try
                With m_objdacLingdaoanpai
                    getNewPX = .getNewPX(strErrMsg, strUserId, strPassword, strRQ, strNewPX)
                End With
            Catch ex As Exception
                getNewPX = False
                strErrMsg = ex.Message
            End Try

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

            Try
                With m_objdacLingdaoanpai
                    doUpdatePX = .doUpdatePX(strErrMsg, strUserId, strPassword, intSrcIndex, intDesIndex, intSrcXH, intDesXH)
                End With
            Catch ex As Exception
                doUpdatePX = False
                strErrMsg = ex.Message
            End Try

        End Function

    End Class 'rulesLingdaoanpai

End Namespace 'Xydc.Platform.BusinessRules
