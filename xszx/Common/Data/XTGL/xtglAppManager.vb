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
Imports System.Runtime.Serialization

Namespace Xydc.Platform.Common.Data

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.Common.Data
    ' ����    ��AppManagerData
    '
    ' ����������
    '     ����ϵͳ�����йص����ݷ��ʸ�ʽ
    '----------------------------------------------------------------
    <System.ComponentModel.DesignerCategory("Code"), SerializableAttribute()> Public Class AppManagerData
        Inherits System.Data.DataSet

        '����_B_���ݿ�_����������Ϣ����
        '������
        Public Const TABLE_GL_B_SHUJUKU_FUWUQI As String = "����_B_���ݿ�_������"
        '�ֶ�����
        Public Const FIELD_GL_B_SHUJUKU_FUWUQI_MC As String = "����"
        Public Const FIELD_GL_B_SHUJUKU_FUWUQI_LX As String = "����"
        Public Const FIELD_GL_B_SHUJUKU_FUWUQI_TGZ As String = "�ṩ��"
        Public Const FIELD_GL_B_SHUJUKU_FUWUQI_LJC As String = "���Ӵ�"
        Public Const FIELD_GL_B_SHUJUKU_FUWUQI_SM As String = "˵��"
        'Լ��������Ϣ

        '����_B_���ݿ�_���ݿ����Ϣ����
        '������
        Public Const TABLE_GL_B_SHUJUKU_SHUJUKU As String = "����_B_���ݿ�_���ݿ�"
        '�ֶ�����
        Public Const FIELD_GL_B_SHUJUKU_SHUJUKU_FWQM As String = "��������"
        Public Const FIELD_GL_B_SHUJUKU_SHUJUKU_SJKM As String = "���ݿ���"
        Public Const FIELD_GL_B_SHUJUKU_SHUJUKU_SJKZWM As String = "���ݿ�������"
        Public Const FIELD_GL_B_SHUJUKU_SHUJUKU_SM As String = "˵��"
        'Լ��������Ϣ

        '����_B_���ݿ�_�������Ϣ����
        '������
        Public Const TABLE_GL_B_SHUJUKU_DUIXIANG As String = "����_B_���ݿ�_����"
        '�ֶ�����
        Public Const FIELD_GL_B_SHUJUKU_DUIXIANG_DXBS As String = "�����ʶ"
        Public Const FIELD_GL_B_SHUJUKU_DUIXIANG_FWQM As String = "��������"
        Public Const FIELD_GL_B_SHUJUKU_DUIXIANG_SJKM As String = "���ݿ���"
        Public Const FIELD_GL_B_SHUJUKU_DUIXIANG_DXMC As String = "��������"
        Public Const FIELD_GL_B_SHUJUKU_DUIXIANG_DXLX As String = "��������"
        Public Const FIELD_GL_B_SHUJUKU_DUIXIANG_DXZWM As String = "����������"
        Public Const FIELD_GL_B_SHUJUKU_DUIXIANG_SM As String = "˵��"
        'Լ��������Ϣ

        '���ݿ��ɫ����
        '������
        Public Const TABLE_GL_B_SHUJUKU_JIAOSE As String = "����_B_���ݿ�_��ɫ"
        '�ֶ�����
        Public Const FIELD_GL_B_SHUJUKU_JIAOSE_UID As String = "UID"
        Public Const FIELD_GL_B_SHUJUKU_JIAOSE_NAME As String = "NAME"
        'Լ��������Ϣ

        '���ݿ��û�����
        '������
        Public Const TABLE_GL_B_SHUJUKU_DBUSER As String = "����_B_���ݿ�_�û�"
        '�ֶ�����
        Public Const FIELD_GL_B_SHUJUKU_DBUSER_UID As String = "UID"
        Public Const FIELD_GL_B_SHUJUKU_DBUSER_NAME As String = "NAME"
        'Լ��������Ϣ

        '���ݿ����Ȩ�޶���
        '������
        Public Const TABLE_GL_B_SHUJUKU_DUIXIANGQX As String = "����_B_���ݿ�_����Ȩ��"
        '�ֶ�����
        Public Const FIELD_GL_B_SHUJUKU_DUIXIANGQX_DXMC As String = "��������"
        Public Const FIELD_GL_B_SHUJUKU_DUIXIANGQX_DXLX As String = "��������"
        Public Const FIELD_GL_B_SHUJUKU_DUIXIANGQX_DXZWM As String = "����������"
        Public Const FIELD_GL_B_SHUJUKU_DUIXIANGQX_DXSELECT As String = "ѡ��Ȩ"
        Public Const FIELD_GL_B_SHUJUKU_DUIXIANGQX_DXADDNEW As String = "����Ȩ"
        Public Const FIELD_GL_B_SHUJUKU_DUIXIANGQX_DXUPDATE As String = "�༭Ȩ"
        Public Const FIELD_GL_B_SHUJUKU_DUIXIANGQX_DXDELETE As String = "ɾ��Ȩ"
        Public Const FIELD_GL_B_SHUJUKU_DUIXIANGQX_DXEXECUTE As String = "ִ��Ȩ"
        'Լ��������Ϣ

        '����_B_Ӧ��ϵͳ_ģ�鶨��
        '������
        Public Const TABLE_GL_B_YINGYONGXITONG_MOKUAI As String = "����_B_Ӧ��ϵͳ_ģ��"
        '�ֶ�����
        Public Const FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKBS As String = "ģ���ʶ"
        Public Const FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKDM As String = "ģ�����"
        Public Const FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKJB As String = "ģ�鼶��"
        Public Const FIELD_GL_B_YINGYONGXITONG_MOKUAI_BJDM As String = "��������"
        Public Const FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKMC As String = "ģ������"
        Public Const FIELD_GL_B_YINGYONGXITONG_MOKUAI_DJMK As String = "����ģ��"
        Public Const FIELD_GL_B_YINGYONGXITONG_MOKUAI_SJMK As String = "�ϼ�ģ��"
        Public Const FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKSM As String = "˵��"
        'Լ��������Ϣ

        '����_B_Ӧ��ϵͳ_ģ�鶨��
        '������
        Public Const TABLE_GL_B_YINGYONGXITONG_MOKUAIQX As String = "����_B_Ӧ��ϵͳ_ģ��Ȩ��"
        '�ֶ�����
        Public Const FIELD_GL_B_YINGYONGXITONG_MOKUAIQX_MKBS As String = "ģ���ʶ"
        Public Const FIELD_GL_B_YINGYONGXITONG_MOKUAIQX_MKDM As String = "ģ�����"
        Public Const FIELD_GL_B_YINGYONGXITONG_MOKUAIQX_MKMC As String = "ģ������"
        Public Const FIELD_GL_B_YINGYONGXITONG_MOKUAIQX_MKSM As String = "˵��"
        Public Const FIELD_GL_B_YINGYONGXITONG_MOKUAIQX_QXDM As String = "Ȩ�޴���"
        Public Const FIELD_GL_B_YINGYONGXITONG_MOKUAIQX_YHBS As String = "�û���ʶ"
        Public Const FIELD_GL_B_YINGYONGXITONG_MOKUAIQX_YHLX As String = "�û�����"
        Public Const FIELD_GL_B_YINGYONGXITONG_MOKUAIQX_EXECUTE As String = "ִ��Ȩ"
        'Լ��������Ϣ

        '��auditaqlog���������
        '������
        Public Const TABLE_GL_VT_B_AUDITAQLOG As String = "auditaqlogItem"
        '�ֶ�����
        Public Const FIELD_GL_VT_B_AUDITAQLOG_OPTIME As String = "optime"
        Public Const FIELD_GL_VT_B_AUDITAQLOG_OPADDR As String = "opaddr"

        Public Const FIELD_GL_VT_B_AUDITAQLOG_OPMACH As String = "opmach"

        Public Const FIELD_GL_VT_B_AUDITAQLOG_OPUSER As String = "opuser"
        Public Const FIELD_GL_VT_B_AUDITAQLOG_OPNOTE As String = "opnote"
        'Լ��������Ϣ

        '��auditpzlog���������
        '������
        Public Const TABLE_GL_VT_B_AUDITPZLOG As String = "auditpzlogItem"
        '�ֶ�����
        Public Const FIELD_GL_VT_B_AUDITPZLOG_OPTIME As String = "optime"
        Public Const FIELD_GL_VT_B_AUDITPZLOG_OPADDR As String = "opaddr"

        Public Const FIELD_GL_VT_B_AUDITPZLOG_OPMACH As String = "opmach"

        Public Const FIELD_GL_VT_B_AUDITPZLOG_OPUSER As String = "opuser"
        Public Const FIELD_GL_VT_B_AUDITPZLOG_OPNOTE As String = "opnote"
        'Լ��������Ϣ

        '��auditsjlog���������
        '������
        Public Const TABLE_GL_VT_B_AUDITSJLOG As String = "auditsjlogItem"
        '�ֶ�����
        Public Const FIELD_GL_VT_B_AUDITSJLOG_OPTIME As String = "optime"
        Public Const FIELD_GL_VT_B_AUDITSJLOG_OPADDR As String = "opaddr"

        Public Const FIELD_GL_VT_B_AUDITSJLOG_OPMACH As String = "opmach"

        Public Const FIELD_GL_VT_B_AUDITSJLOG_OPUSER As String = "opuser"
        Public Const FIELD_GL_VT_B_AUDITSJLOG_OPNOTE As String = "opnote"
        'Լ��������Ϣ

        '��jsoalog���������
        '������
        Public Const TABLE_GL_VT_B_JSOALOG As String = "jsoalogItem"
        '�ֶ�����
        Public Const FIELD_GL_VT_B_JSOALOG_OPTIME As String = "optime"
        Public Const FIELD_GL_VT_B_JSOALOG_OPADDR As String = "opaddr"

        Public Const FIELD_GL_VT_B_JSOALOG_OPMACH As String = "opmach"

        Public Const FIELD_GL_VT_B_JSOALOG_OPUSER As String = "opuser"
        Public Const FIELD_GL_VT_B_JSOALOG_OPNOTE As String = "opnote"
        'Լ��������Ϣ








        '�����ʼ��������enum
        Public Enum enumTableType
            GL_B_SHUJUKU_FUWUQI = 1
            GL_B_SHUJUKU_SHUJUKU = 2
            GL_B_SHUJUKU_DUIXIANG = 3
            GL_B_SHUJUKU_JIAOSE = 4
            GL_B_SHUJUKU_DBUSER = 5
            GL_B_SHUJUKU_DUIXIANGQX = 6
            GL_B_YINGYONGXITONG_MOKUAI = 7
            GL_B_YINGYONGXITONG_MOKUAIQX = 8
            GL_VT_B_JSOALOG = 9
            GL_VT_B_AUDITPZLOG = 10
            GL_VT_B_AUDITAQLOG = 11
            GL_VT_B_AUDITSJLOG = 11
        End Enum

        '�������ݿ��������
        Public Enum enumDatabaseObjectType
            S = 1    'ϵͳ��
            U = 2    '�û���
            V = 3    '��ͼ
            TR = 4   '������
            FN = 5   '��������
            P = 6    '�洢����
            X = 7    '��չ�洢����
            FIF = 8  '��Ƕ����
            TF = 9   '��Ƕ����(1)
        End Enum
        'ϵͳҪ����Ķ�������
        Public Const OBJECTTYPELIST As String = "'U','V','FN','P','IF','TF'"

        '����Ȩ�޲�������
        Public Enum enumPermissionType
            GrantSelect = 1
            GrantInsert = 2
            GrantUpdate = 3
            GrantDelete = 4
            GrantExecute = 5
        End Enum

        '�����û�����
        Public Enum enumUserType
            isSqlUser = 1
            isSqlRole = 2
            isNTUser = 4
        End Enum








        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Private Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
            MyBase.New(info, context)
        End Sub

        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
        End Sub

        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New(ByVal objenumTableType As enumTableType)
            MyBase.New()
            Try
                Dim objDataTable As System.Data.DataTable
                Dim strErrMsg As String
                objDataTable = Me.createDataTables(strErrMsg, objenumTableType)
                If Not (objDataTable Is Nothing) Then
                    Me.Tables.Add(objDataTable)
                End If
            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' ��ȫ�ͷű�����Դ
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.Common.Data.AppManagerData)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub









        '----------------------------------------------------------------
        ' �������ͻ�ȡ�����ַ���
        '----------------------------------------------------------------
        Public Function getDatabaseObjectTypeString(ByVal value As enumDatabaseObjectType) As String

            Try
                Select Case value
                    Case enumDatabaseObjectType.S
                        getDatabaseObjectTypeString = "S"
                    Case enumDatabaseObjectType.U
                        getDatabaseObjectTypeString = "U"
                    Case enumDatabaseObjectType.V
                        getDatabaseObjectTypeString = "V"
                    Case enumDatabaseObjectType.TR
                        getDatabaseObjectTypeString = "TR"
                    Case enumDatabaseObjectType.FN
                        getDatabaseObjectTypeString = "FN"
                    Case enumDatabaseObjectType.FIF
                        getDatabaseObjectTypeString = "IF"
                    Case enumDatabaseObjectType.TF
                        getDatabaseObjectTypeString = "TF"
                    Case enumDatabaseObjectType.P
                        getDatabaseObjectTypeString = "P"
                    Case enumDatabaseObjectType.X
                        getDatabaseObjectTypeString = "X"
                    Case Else
                        getDatabaseObjectTypeString = ""
                End Select
            Catch
                getDatabaseObjectTypeString = ""
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���������ַ�����ȡ����
        '----------------------------------------------------------------
        Public Function getDatabaseObjectType(ByVal value As String) As enumDatabaseObjectType

            Try
                Select Case value.ToUpper()
                    Case "S"
                        getDatabaseObjectType = enumDatabaseObjectType.S
                    Case "U"
                        getDatabaseObjectType = enumDatabaseObjectType.U
                    Case "V"
                        getDatabaseObjectType = enumDatabaseObjectType.V
                    Case "TR"
                        getDatabaseObjectType = enumDatabaseObjectType.TR
                    Case "FN"
                        getDatabaseObjectType = enumDatabaseObjectType.FN
                    Case "IF"
                        getDatabaseObjectType = enumDatabaseObjectType.FIF
                    Case "TF"
                        getDatabaseObjectType = enumDatabaseObjectType.TF
                    Case "P"
                        getDatabaseObjectType = enumDatabaseObjectType.P
                    Case "X"
                        getDatabaseObjectType = enumDatabaseObjectType.X
                    Case Else
                        getDatabaseObjectType = Nothing
                End Select
            Catch
                getDatabaseObjectType = Nothing
            End Try

        End Function

        '----------------------------------------------------------------
        ' �������ͻ�ȡ�����ַ���
        '----------------------------------------------------------------
        Public Function getPermissionTypeString(ByVal value As enumPermissionType) As String

            Try
                Select Case value
                    Case enumPermissionType.GrantSelect
                        getPermissionTypeString = "SELECT"
                    Case enumPermissionType.GrantUpdate
                        getPermissionTypeString = "UPDATE"
                    Case enumPermissionType.GrantInsert
                        getPermissionTypeString = "INSERT"
                    Case enumPermissionType.GrantDelete
                        getPermissionTypeString = "DELETE"
                    Case enumPermissionType.GrantExecute
                        getPermissionTypeString = "EXECUTE"
                    Case Else
                        getPermissionTypeString = ""
                End Select
            Catch
                getPermissionTypeString = ""
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���������ַ�����ȡ����
        '----------------------------------------------------------------
        Public Function getPermissionType(ByVal value As String) As enumPermissionType

            Try
                Select Case value.ToUpper()
                    Case "SELECT"
                        getPermissionType = enumPermissionType.GrantSelect
                    Case "UPDATE"
                        getPermissionType = enumPermissionType.GrantUpdate
                    Case "INSERT"
                        getPermissionType = enumPermissionType.GrantInsert
                    Case "DELETE"
                        getPermissionType = enumPermissionType.GrantDelete
                    Case "EXECUTE"
                        getPermissionType = enumPermissionType.GrantExecute
                    Case Else
                        getPermissionType = Nothing
                End Select
            Catch
                getPermissionType = Nothing
            End Try

        End Function

        '----------------------------------------------------------------
        '������DataTable���뵽DataSet��
        '----------------------------------------------------------------
        Public Function appendDataTable(ByVal table As System.Data.DataTable) As String

            Dim strErrMsg As String = ""

            Try
                Me.Tables.Add(table)
            Catch ex As Exception
                strErrMsg = ex.Message
            End Try

            appendDataTable = strErrMsg

        End Function

        '----------------------------------------------------------------
        '����ָ�����ʹ���dataTable
        '----------------------------------------------------------------
        Public Function createDataTables( _
            ByRef strErrMsg As String, _
            ByVal enumType As enumTableType) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Select Case enumType
                Case enumTableType.GL_B_SHUJUKU_FUWUQI
                    table = createDataTables_Fuwuqi(strErrMsg)
                Case enumTableType.GL_B_SHUJUKU_SHUJUKU
                    table = createDataTables_Shujuku(strErrMsg)
                Case enumTableType.GL_B_SHUJUKU_DUIXIANG
                    table = createDataTables_Duixiang(strErrMsg)
                Case enumTableType.GL_B_SHUJUKU_JIAOSE
                    table = createDataTables_Jiaose(strErrMsg)
                Case enumTableType.GL_B_SHUJUKU_DBUSER
                    table = createDataTables_DBUser(strErrMsg)
                Case enumTableType.GL_B_SHUJUKU_DUIXIANGQX
                    table = createDataTables_DuixiangQX(strErrMsg)
                Case enumTableType.GL_B_YINGYONGXITONG_MOKUAI
                    table = createDataTables_Mokuai(strErrMsg)
                Case enumTableType.GL_B_YINGYONGXITONG_MOKUAIQX
                    table = createDataTables_MokuaiQX(strErrMsg)

                Case enumTableType.GL_VT_B_JSOALOG
                    table = createDataTables_jsoaLog(strErrMsg)
                Case enumTableType.GL_VT_B_AUDITPZLOG
                    table = createDataTables_auditpzLog(strErrMsg)
                Case enumTableType.GL_VT_B_AUDITAQLOG
                    table = createDataTables_auditaqLog(strErrMsg)
                Case enumTableType.GL_VT_B_AUDITSJLOG
                    table = createDataTables_auditsjLog(strErrMsg)

                Case Else
                    strErrMsg = "��Ч�ı����ͣ�"
                    table = Nothing
            End Select

            createDataTables = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GL_B_SHUJUKU_FUWUQI
        '----------------------------------------------------------------
        Private Function createDataTables_Fuwuqi(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GL_B_SHUJUKU_FUWUQI)
                With table.Columns
                    .Add(FIELD_GL_B_SHUJUKU_FUWUQI_MC, GetType(System.String))
                    .Add(FIELD_GL_B_SHUJUKU_FUWUQI_LX, GetType(System.String))
                    .Add(FIELD_GL_B_SHUJUKU_FUWUQI_TGZ, GetType(System.String))
                    .Add(FIELD_GL_B_SHUJUKU_FUWUQI_LJC, GetType(System.Byte()))
                    .Add(FIELD_GL_B_SHUJUKU_FUWUQI_SM, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Fuwuqi = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GL_B_SHUJUKU_SHUJUKU
        '----------------------------------------------------------------
        Private Function createDataTables_Shujuku(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GL_B_SHUJUKU_SHUJUKU)
                With table.Columns
                    .Add(FIELD_GL_B_SHUJUKU_SHUJUKU_FWQM, GetType(System.String))
                    .Add(FIELD_GL_B_SHUJUKU_SHUJUKU_SJKM, GetType(System.String))
                    .Add(FIELD_GL_B_SHUJUKU_SHUJUKU_SJKZWM, GetType(System.String))
                    .Add(FIELD_GL_B_SHUJUKU_SHUJUKU_SM, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Shujuku = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GL_B_SHUJUKU_DUIXIANG
        '----------------------------------------------------------------
        Private Function createDataTables_Duixiang(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GL_B_SHUJUKU_DUIXIANG)
                With table.Columns
                    .Add(FIELD_GL_B_SHUJUKU_DUIXIANG_DXBS, GetType(System.Int32))
                    .Add(FIELD_GL_B_SHUJUKU_DUIXIANG_FWQM, GetType(System.String))
                    .Add(FIELD_GL_B_SHUJUKU_DUIXIANG_SJKM, GetType(System.String))
                    .Add(FIELD_GL_B_SHUJUKU_DUIXIANG_DXMC, GetType(System.String))
                    .Add(FIELD_GL_B_SHUJUKU_DUIXIANG_DXLX, GetType(System.String))
                    .Add(FIELD_GL_B_SHUJUKU_DUIXIANG_DXZWM, GetType(System.String))
                    .Add(FIELD_GL_B_SHUJUKU_DUIXIANG_SM, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Duixiang = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GL_B_SHUJUKU_JIAOSE
        '----------------------------------------------------------------
        Private Function createDataTables_Jiaose(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GL_B_SHUJUKU_JIAOSE)
                With table.Columns
                    .Add(FIELD_GL_B_SHUJUKU_JIAOSE_UID, GetType(System.Int32))
                    .Add(FIELD_GL_B_SHUJUKU_JIAOSE_NAME, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Jiaose = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GL_B_SHUJUKU_DBUSER
        '----------------------------------------------------------------
        Private Function createDataTables_DBUser(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GL_B_SHUJUKU_DBUSER)
                With table.Columns
                    .Add(FIELD_GL_B_SHUJUKU_DBUSER_UID, GetType(System.Int32))
                    .Add(FIELD_GL_B_SHUJUKU_DBUSER_NAME, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_DBUser = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GL_B_SHUJUKU_DUIXIANGQX
        '----------------------------------------------------------------
        Private Function createDataTables_DuixiangQX(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GL_B_SHUJUKU_DUIXIANGQX)
                With table.Columns
                    .Add(FIELD_GL_B_SHUJUKU_DUIXIANGQX_DXMC, GetType(System.String))
                    .Add(FIELD_GL_B_SHUJUKU_DUIXIANGQX_DXLX, GetType(System.String))
                    .Add(FIELD_GL_B_SHUJUKU_DUIXIANGQX_DXZWM, GetType(System.String))
                    .Add(FIELD_GL_B_SHUJUKU_DUIXIANGQX_DXSELECT, GetType(System.String))
                    .Add(FIELD_GL_B_SHUJUKU_DUIXIANGQX_DXADDNEW, GetType(System.String))
                    .Add(FIELD_GL_B_SHUJUKU_DUIXIANGQX_DXUPDATE, GetType(System.String))
                    .Add(FIELD_GL_B_SHUJUKU_DUIXIANGQX_DXDELETE, GetType(System.String))
                    .Add(FIELD_GL_B_SHUJUKU_DUIXIANGQX_DXEXECUTE, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_DuixiangQX = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GL_B_YINGYONGXITONG_MOKUAI
        '----------------------------------------------------------------
        Private Function createDataTables_Mokuai(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GL_B_YINGYONGXITONG_MOKUAI)
                With table.Columns
                    .Add(FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKBS, GetType(System.Int32))
                    .Add(FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKDM, GetType(System.String))
                    .Add(FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKJB, GetType(System.Int32))
                    .Add(FIELD_GL_B_YINGYONGXITONG_MOKUAI_BJDM, GetType(System.Int32))
                    .Add(FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKMC, GetType(System.String))
                    .Add(FIELD_GL_B_YINGYONGXITONG_MOKUAI_DJMK, GetType(System.Int32))
                    .Add(FIELD_GL_B_YINGYONGXITONG_MOKUAI_SJMK, GetType(System.Int32))
                    .Add(FIELD_GL_B_YINGYONGXITONG_MOKUAI_MKSM, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Mokuai = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GL_B_YINGYONGXITONG_MOKUAIQX
        '----------------------------------------------------------------
        Private Function createDataTables_MokuaiQX(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GL_B_YINGYONGXITONG_MOKUAIQX)
                With table.Columns
                    .Add(FIELD_GL_B_YINGYONGXITONG_MOKUAIQX_MKBS, GetType(System.Int32))
                    .Add(FIELD_GL_B_YINGYONGXITONG_MOKUAIQX_MKDM, GetType(System.String))
                    .Add(FIELD_GL_B_YINGYONGXITONG_MOKUAIQX_MKMC, GetType(System.String))
                    .Add(FIELD_GL_B_YINGYONGXITONG_MOKUAIQX_MKSM, GetType(System.String))
                    .Add(FIELD_GL_B_YINGYONGXITONG_MOKUAIQX_QXDM, GetType(System.Int32))
                    .Add(FIELD_GL_B_YINGYONGXITONG_MOKUAIQX_YHBS, GetType(System.String))
                    .Add(FIELD_GL_B_YINGYONGXITONG_MOKUAIQX_YHLX, GetType(System.Int32))
                    .Add(FIELD_GL_B_YINGYONGXITONG_MOKUAIQX_EXECUTE, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_MokuaiQX = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GL_VT_B_JSOALOG 
        '----------------------------------------------------------------
        Private Function createDataTables_jsoaLog(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GL_VT_B_JSOALOG)
                With table.Columns
                    .Add(FIELD_GL_VT_B_JSOALOG_OPTIME, GetType(System.String))
                    .Add(FIELD_GL_VT_B_JSOALOG_OPADDR, GetType(System.String))

                    .Add(FIELD_GL_VT_B_JSOALOG_OPMACH, GetType(System.String))

                    .Add(FIELD_GL_VT_B_JSOALOG_OPUSER, GetType(System.String))
                    .Add(FIELD_GL_VT_B_JSOALOG_OPNOTE, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_jsoaLog = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GL_VT_B_AUDITPZLOG 
        '----------------------------------------------------------------
        Private Function createDataTables_auditpzLog(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GL_VT_B_AUDITPZLOG)
                With table.Columns
                    .Add(FIELD_GL_VT_B_AUDITPZLOG_OPTIME, GetType(System.String))
                    .Add(FIELD_GL_VT_B_AUDITPZLOG_OPADDR, GetType(System.String))

                    .Add(FIELD_GL_VT_B_AUDITPZLOG_OPMACH, GetType(System.String))

                    .Add(FIELD_GL_VT_B_AUDITPZLOG_OPUSER, GetType(System.String))
                    .Add(FIELD_GL_VT_B_AUDITPZLOG_OPNOTE, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_auditpzLog = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GL_VT_B_AUDITAQLOG 
        '----------------------------------------------------------------
        Private Function createDataTables_auditaqLog(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GL_VT_B_AUDITAQLOG)
                With table.Columns
                    .Add(FIELD_GL_VT_B_AUDITAQLOG_OPTIME, GetType(System.String))
                    .Add(FIELD_GL_VT_B_AUDITAQLOG_OPADDR, GetType(System.String))

                    .Add(FIELD_GL_VT_B_AUDITPZLOG_OPMACH, GetType(System.String))

                    .Add(FIELD_GL_VT_B_AUDITAQLOG_OPUSER, GetType(System.String))
                    .Add(FIELD_GL_VT_B_AUDITAQLOG_OPNOTE, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_auditaqLog = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GL_VT_B_AUDITSJLOG 
        '----------------------------------------------------------------
        Private Function createDataTables_auditsjLog(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GL_VT_B_AUDITSJLOG)
                With table.Columns
                    .Add(FIELD_GL_VT_B_AUDITSJLOG_OPTIME, GetType(System.String))
                    .Add(FIELD_GL_VT_B_AUDITSJLOG_OPADDR, GetType(System.String))

                    .Add(FIELD_GL_VT_B_AUDITPZLOG_OPMACH, GetType(System.String))

                    .Add(FIELD_GL_VT_B_AUDITSJLOG_OPUSER, GetType(System.String))
                    .Add(FIELD_GL_VT_B_AUDITSJLOG_OPNOTE, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_auditsjLog = table

        End Function

    End Class 'AppManagerData

End Namespace 'Xydc.Platform.Common.Data
