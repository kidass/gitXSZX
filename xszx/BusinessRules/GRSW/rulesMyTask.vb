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
    ' ����    ��rulesMyTask
    '
    ' ���������� 
    '     �ṩ�ԡ��ҵ����ˡ�ģ���漰��ҵ���߼������
    '----------------------------------------------------------------
    Public Class rulesMyTask

        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
        End Sub

        '----------------------------------------------------------------
        ' ��ȫ�ͷű�����Դ
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessRules.rulesMyTask)
            Try
                If Not (obj Is Nothing) Then
                    'obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub









        '----------------------------------------------------------------
        ' ��ȡ������_B_�ҵ�����_�ڵ㡱�����ݼ�
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strUserId              ���û���ʶ
        '     strPassword            ���û�����
        '     objgrswMyTaskData      ����Ϣ���ݼ�
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Function getMyTaskNodeData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef objgrswMyTaskData As Xydc.Platform.Common.Data.grswMyTaskData) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacMyTask
                    getMyTaskNodeData = .getMyTaskNodeData(strErrMsg, strUserId, strPassword, objgrswMyTaskData)
                End With
            Catch ex As Exception
                getMyTaskNodeData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���ݸ��������ȡ��Ӧ������������
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strCode                �������ڵ����(Ψһ�Ա�֤)
        '     objgrswMyTaskData      ���ڵ���Ϣ���ݼ�
        '     objNodeData            ��(����)ָ���ڵ������������
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Function getMyTaskNodeData( _
            ByRef strErrMsg As String, _
            ByVal strCode As String, _
            ByVal objgrswMyTaskData As Xydc.Platform.Common.Data.grswMyTaskData, _
            ByRef objNodeData As System.Data.DataRow) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacMyTask
                    getMyTaskNodeData = .getMyTaskNodeData(strErrMsg, strCode, objgrswMyTaskData, objNodeData)
                End With
            Catch ex As Exception
                getMyTaskNodeData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���ݵ�ǰѡ������������������ȡ��ǰ�û���Ҫ�鿴���ļ�����
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strUserXM            ���û�����
        '     objNodeData          ����ǰ����ڵ�������
        '     strWhere             ����ǰ��������(a.)
        '     objFileData          ������Ҫ�鿴���ļ�����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getMyTaskFileData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByVal objNodeData As System.Data.DataRow, _
            ByVal strWhere As String, _
            ByRef objFileData As Xydc.Platform.Common.Data.grswMyTaskData) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacMyTask
                    getMyTaskFileData = .getMyTaskFileData(strErrMsg, strUserId, strPassword, strUserXM, objNodeData, strWhere, objFileData)
                End With
            Catch ex As Exception
                getMyTaskFileData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���ݵ�ǰѡ������������������ȡ��ǰ�û���Ҫ�鿴����������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strWJBS              ��Ҫ�鿴���ļ���ʶ
        '     strUserXM            ���û�����
        '     objNodeData          ����ǰ����ڵ�������
        '     strWhere             ����ǰ��������(a.)
        '     objTaskData          ������Ҫ�鿴����������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getMyTaskTaskData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWJBS As String, _
            ByVal strUserXM As String, _
            ByVal objNodeData As System.Data.DataRow, _
            ByVal strWhere As String, _
            ByRef objTaskData As Xydc.Platform.Common.Data.grswMyTaskData) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacMyTask
                    getMyTaskTaskData = .getMyTaskTaskData(strErrMsg, strUserId, strPassword, strWJBS, strUserXM, objNodeData, strWhere, objTaskData)
                End With
            Catch ex As Exception
                getMyTaskTaskData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡ�ҵ�δ���������ݼ�
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strUserId              ���û���ʶ
        '     strPassword            ���û�����
        '     strUserXM              ���û�����
        '     objDataSetDBSY         ��δ���������ݼ�
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Function getDataSetDBSY( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByRef objDataSetDBSY As Xydc.Platform.Common.Data.grswMyTaskData) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacMyTask
                    getDataSetDBSY = .getDataSetDBSY(strErrMsg, strUserId, strPassword, strUserXM, objDataSetDBSY)
                End With
            Catch ex As Exception
                getDataSetDBSY = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡ�ҵ��Ѿ������ļ�+����Ҫ�������ݼ�
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strUserId              ���û���ʶ
        '     strPassword            ���û�����
        '     strUserXM              ���û�����
        '     objDataSetGQSY         ���Ѿ������ļ�+����Ҫ�������ݼ�
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Function getDataSetGQSY( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByRef objDataSetGQSY As Xydc.Platform.Common.Data.grswMyTaskData) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacMyTask
                    getDataSetGQSY = .getDataSetGQSY(strErrMsg, strUserId, strPassword, strUserXM, objDataSetGQSY)
                End With
            Catch ex As Exception
                getDataSetGQSY = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡ�ҵı����������ݼ�
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strUserId              ���û���ʶ
        '     strPassword            ���û�����
        '     strUserXM              ���û�����
        '     objDataSetBWTX         �������������ݼ�
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Function getDataSetBWTX( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByRef objDataSetBWTX As Xydc.Platform.Common.Data.grswMyTaskData) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacMyTask
                    getDataSetBWTX = .getDataSetBWTX(strErrMsg, strUserId, strPassword, strUserXM, objDataSetBWTX)
                End With
            Catch ex As Exception
                getDataSetBWTX = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡ�ҵ�δ��������Ŀ
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strUserId              ���û���ʶ
        '     strPassword            ���û�����
        '     strUserXM              ���û�����
        '     intCountDBSY           ��δ��������Ŀ
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Function getCountDBSY( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByRef intCountDBSY As Integer) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacMyTask
                    getCountDBSY = .getCountDBSY(strErrMsg, strUserId, strPassword, strUserXM, intCountDBSY)
                End With
            Catch ex As Exception
                getCountDBSY = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡ�ҵ��Ѿ������ļ�+����Ҫ�����ļ���Ŀ
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strUserId              ���û���ʶ
        '     strPassword            ���û�����
        '     strUserXM              ���û�����
        '     intCountGQSY           ���Ѿ������ļ�+����Ҫ�����ļ���Ŀ
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Function getCountGQSY( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByRef intCountGQSY As Integer) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacMyTask
                    getCountGQSY = .getCountGQSY(strErrMsg, strUserId, strPassword, strUserXM, intCountGQSY)
                End With
            Catch ex As Exception
                getCountGQSY = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡ�ҵı��������ļ���Ŀ
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strUserId              ���û���ʶ
        '     strPassword            ���û�����
        '     strUserXM              ���û�����
        '     intCountBWTX           �����������ļ���Ŀ
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Function getCountBWTX( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByRef intCountBWTX As Integer) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacMyTask
                    getCountBWTX = .getCountBWTX(strErrMsg, strUserId, strPassword, strUserXM, intCountBWTX)
                End With
            Catch ex As Exception
                getCountBWTX = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡָ��ʱ����յ����ļ���Ŀ
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strUserId              ���û���ʶ
        '     strPassword            ���û�����
        '     strUserXM              ���û�����
        '     strZDSJ                ��ָ��ʱ��(����+ʱ���ʽ)
        '     intCountRecv           ���ļ���Ŀ
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Function getCountRecv( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByVal strZDSJ As String, _
            ByRef intCountRecv As Integer) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacMyTask
                    getCountRecv = .getCountRecv(strErrMsg, strUserId, strPassword, strUserXM, strZDSJ, intCountRecv)
                End With
            Catch ex As Exception
                getCountRecv = False
                strErrMsg = ex.Message
            End Try

        End Function

    End Class 'rulesMyTask

End Namespace 'Xydc.Platform.BusinessRules
