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
    ' ����    ��rulesEditWorkFlow
    '
    ' ���������� 
    '   ���ṩ�ԡ����й���������Ϣ�����ҵ�����
    '----------------------------------------------------------------
    Public Class rulesEditWorkFlow
        Implements IDisposable

        Private m_objdacEditWorkFlow As Xydc.Platform.DataAccess.dacEditWorkFlow









        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
            m_objdacEditWorkFlow = New Xydc.Platform.DataAccess.dacEditWorkFlow
        End Sub

        '----------------------------------------------------------------
        ' ��������(���������)
        '----------------------------------------------------------------
        Public Sub Dispose() Implements System.IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(True)
        End Sub

        '----------------------------------------------------------------
        ' ��������(����)
        '----------------------------------------------------------------
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If (Not disposing) Then
                Exit Sub
            End If
            Xydc.Platform.DataAccess.dacEditWorkFlow.SafeRelease(m_objdacEditWorkFlow)
        End Sub

        '----------------------------------------------------------------
        ' ��ȫ�ͷű�����Դ
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessRules.rulesEditWorkFlow)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub











        '----------------------------------------------------------------
        ' ��ȡ������_V_ȫ�������ļ��¡���ȫ���ݵ����ݼ�(�ԡ�������ڡ���������)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strWhere             �������ַ���
        '     objDataSet_WFS       ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getDataSet_WFS( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objDataSet_WFS As Xydc.Platform.Common.Data.FlowData) As Boolean
            With m_objdacEditWorkFlow
                getDataSet_WFS = .getDataSet_WFS(strErrMsg, strUserId, strPassword, strWhere, objDataSet_WFS)
            End With
        End Function

        '--------------------------------------------------------------
        ' ɾ��������_B_���ӡ�������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     objOldData           ��������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '����˵��
        '     
        '----------------------------------------------------------------
        Public Function doDeleteGWJJData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacEditWorkFlow
                    doDeleteGWJJData = .doDeleteGWJJData(strErrMsg, strUserId, strPassword, objOldData)
                End With
            Catch ex As Exception
                doDeleteGWJJData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���������ԱĿǰ���ڵ��ļ��༭����
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strCzyId             ������ԱID
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        ' ����˵����
        '      2009-03-12 ����
        '----------------------------------------------------------------
        Public Function doUnLockAll( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strCzyId As String) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacEditWorkFlow
                    doUnLockAll = .doUnLockAll(strErrMsg, strUserId, strPassword, strCzyId)
                End With
            Catch ex As Exception
                doUnLockAll = False
                strErrMsg = ex.Message
            End Try

        End Function

    End Class 'rulesEditWorkFlow

End Namespace 'Xydc.Platform.BusinessRules
