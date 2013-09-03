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
    ' ����    ��rulesGuizhangzhidu
    '
    ' ���������� 
    '     �ṩ�ԡ������ƶȡ�ģ���漰��ҵ���߼������
    '----------------------------------------------------------------
    Public Class rulesGuizhangzhidu
        Implements System.IDisposable

        Private m_objdacGuizhangzhidu As Xydc.Platform.DataAccess.dacGuizhangzhidu










        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
            m_objdacGuizhangzhidu = New Xydc.Platform.DataAccess.dacGuizhangzhidu
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
            If Not (m_objdacGuizhangzhidu Is Nothing) Then
                m_objdacGuizhangzhidu.Dispose()
                m_objdacGuizhangzhidu = Nothing
            End If
        End Sub

        '----------------------------------------------------------------
        ' ��ȫ�ͷű�����Դ
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessRules.rulesGuizhangzhidu)
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
                With m_objdacGuizhangzhidu
                    doExportToExcel = .doExportToExcel(strErrMsg, objDataSet, strExcelFile, strMacroName, strMacroValue)
                End With
            Catch ex As Exception
                doExportToExcel = False
                strErrMsg = ex.Message
            End Try

        End Function





        '----------------------------------------------------------------
        ' ��ȡ�����ƶ�����(��������š�����)
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     strUserId                   ���û���ʶ
        '     strPassword                 ���û�����
        '     objGuizhangzhiduData        ����Ϣ���ݼ�
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function getDataSet_Tree( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef objGuizhangzhiduData As Xydc.Platform.Common.Data.ggxxGuizhangzhiduData) As Boolean

            Try
                With m_objdacGuizhangzhidu
                    getDataSet_Tree = .getDataSet_Tree(strErrMsg, strUserId, strPassword, objGuizhangzhiduData)
                End With
            Catch ex As Exception
                getDataSet_Tree = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡָ����ŵ��¼��ƶ�����(��������š�����)
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     strUserId                   ���û���ʶ
        '     strPassword                 ���û�����
        '     intSJBH                     ���ϼ����
        '     objGuizhangzhiduData        ����Ϣ���ݼ�
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function getDataSet_Tree( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intSJBH As Integer, _
            ByRef objGuizhangzhiduData As Xydc.Platform.Common.Data.ggxxGuizhangzhiduData) As Boolean

            Try
                With m_objdacGuizhangzhidu
                    getDataSet_Tree = .getDataSet_Tree(strErrMsg, strUserId, strPassword, intSJBH, objGuizhangzhiduData)
                End With
            Catch ex As Exception
                getDataSet_Tree = False
                strErrMsg = ex.Message
            End Try

        End Function





        '----------------------------------------------------------------
        ' ɾ��ָ������(ָ����¼)-ͬʱɾ���¼�����
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     intBH                �����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doDelete( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intBH As Integer) As Boolean

            Try
                With m_objdacGuizhangzhidu
                    doDelete = .doDelete(strErrMsg, strUserId, strPassword, intBH)
                End With
            Catch ex As Exception
                doDelete = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���潻����¼���ݼ�¼(�����������)
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
                With m_objdacGuizhangzhidu
                    doSave = .doSave(strErrMsg, strUserId, strPassword, objNewData, objOldData, objenumEditType)
                End With
            Catch ex As Exception
                doSave = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     intBH                �����
        '     intPXH               ���������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doUpdatePXH( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intBH As Integer, _
            ByVal intPXH As Integer) As Boolean

            Try
                With m_objdacGuizhangzhidu
                    doUpdatePXH = .doUpdatePXH(strErrMsg, strUserId, strPassword, intBH, intPXH)
                End With
            Catch ex As Exception
                doUpdatePXH = False
                strErrMsg = ex.Message
            End Try

        End Function






        '----------------------------------------------------------------
        ' ��ȡָ����ŵ��ƶ�����
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     strUserId                   ���û���ʶ
        '     strPassword                 ���û�����
        '     intBH                       �����
        '     objGuizhangzhiduData        ����Ϣ���ݼ�
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intBH As Integer, _
            ByRef objGuizhangzhiduData As Xydc.Platform.Common.Data.ggxxGuizhangzhiduData) As Boolean

            Try
                With m_objdacGuizhangzhidu
                    getDataSet = .getDataSet(strErrMsg, strUserId, strPassword, intBH, objGuizhangzhiduData)
                End With
            Catch ex As Exception
                getDataSet = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡ�µ������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     intSJBH              ���ϼ����
        '     intPXH               ���������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getNewPXH( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intSJBH As Integer, _
            ByRef intPXH As Integer) As Boolean

            Try
                With m_objdacGuizhangzhidu
                    getNewPXH = .getNewPXH(strErrMsg, strUserId, strPassword, intSJBH, intPXH)
                End With
            Catch ex As Exception
                getNewPXH = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ����intBH��ȡ�ϼ����
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     strUserId                   ���û���ʶ
        '     strPassword                 ���û�����
        '     intBH                       �����
        '     intSJBH                     ��(����)�ϼ����
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function getSjbhByBh( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intBH As Integer, _
            ByRef intSJBH As Integer) As Boolean

            Try
                With m_objdacGuizhangzhidu
                    getSjbhByBh = .getSjbhByBh(strErrMsg, strUserId, strPassword, intBH, intSJBH)
                End With
            Catch ex As Exception
                getSjbhByBh = False
                strErrMsg = ex.Message
            End Try

        End Function

    End Class 'rulesGuizhangzhidu

End Namespace 'Xydc.Platform.BusinessRules
