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
    ' ����    ��ggxxDianzigonggaoData
    '
    ' ����������
    '     ���塰���ӹ��桱�йص����ݷ��ʸ�ʽ
    '----------------------------------------------------------------
    <System.ComponentModel.DesignerCategory("Code"), SerializableAttribute()> Public Class ggxxDianzigonggaoData
        Inherits System.Data.DataSet

        '������_B_������������Ϣ����
        '������
        Public Const TABLE_GR_B_GONGGAOLAN As String = "����_B_������"
        '�ֶ�����
        Public Const FIELD_GR_B_GONGGAOLAN_CZYDM As String = "����Ա����"
        Public Const FIELD_GR_B_GONGGAOLAN_XH As String = "���"
        Public Const FIELD_GR_B_GONGGAOLAN_WJBS As String = "�ļ���ʶ"
        Public Const FIELD_GR_B_GONGGAOLAN_ZZDM As String = "��֯����"
        Public Const FIELD_GR_B_GONGGAOLAN_ZZMC As String = "��֯����"
        Public Const FIELD_GR_B_GONGGAOLAN_CZY As String = "����Ա"
        Public Const FIELD_GR_B_GONGGAOLAN_RQ As String = "����"
        Public Const FIELD_GR_B_GONGGAOLAN_BT As String = "����"
        Public Const FIELD_GR_B_GONGGAOLAN_NR As String = "����"
        Public Const FIELD_GR_B_GONGGAOLAN_ZWNR As String = "��������"
        Public Const FIELD_GR_B_GONGGAOLAN_BLRQ As String = "��������"
        Public Const FIELD_GR_B_GONGGAOLAN_FBBS As String = "������ʶ"
        Public Const FIELD_GR_B_GONGGAOLAN_YDKZ As String = "�Ķ�����"
        Public Const FIELD_GR_B_GONGGAOLAN_YDFW As String = "�Ķ���Χ"
        '�����ֶ�
        Public Const FIELD_GR_B_GONGGAOLAN_SFYD As String = "�Ƿ��Ķ�"
        Public Const FIELD_GR_B_GONGGAOLAN_FBMS As String = "��������"
        'Լ��������Ϣ

        Public Enum enumFileDownloadStatus
            NotDownload = 0 'û������
            HasDownload = 1 '�Ѿ�����
        End Enum



        '�����ӹ���_B_����������Ϣ����
        '������
        Public Const TABLE_DZGG_B_FUJIAN As String = "���ӹ���_B_����"
        '�ֶ�����
        Public Const FIELD_DZGG_B_FUJIAN_WJBS As String = "�ļ���ʶ"
        Public Const FIELD_DZGG_B_FUJIAN_WJXH As String = "���"
        Public Const FIELD_DZGG_B_FUJIAN_WJSM As String = "˵��"
        Public Const FIELD_DZGG_B_FUJIAN_WJYS As String = "ҳ��"
        Public Const FIELD_DZGG_B_FUJIAN_WJWZ As String = "λ��"        '�������ļ�λ��(�����FTP����·��)
        '������Ϣ(��ʾ/�༭ʱ��)
        Public Const FIELD_DZGG_B_FUJIAN_XSXH As String = "��ʾ���"
        Public Const FIELD_DZGG_B_FUJIAN_BDWJ As String = "�����ļ�"    '���غ���ļ�λ��(����·��)
        Public Const FIELD_DZGG_B_FUJIAN_XZBZ As String = "���ر�־"    '�Ƿ�����?
        'Լ��������Ϣ


        'Ŀ¼�趨
        Public Const FILEDIR_GJ As String = "DZGG\GJ"          '���ӹ�����������Ŀ¼
        Public Const FILEDIR_HJ As String = "DZGG\HJ"          '���ӹ���ۼ��ļ�Ŀ¼

        Public Const FILEDIR_FJ As String = "DZGG\FJ"          '���ӹ��渽��Ŀ¼


        '������_B_�������Ķ����������Ϣ����
        '������
        Public Const TABLE_GR_B_GONGGAOLAN_YUEDUQINGKUANG As String = "����_B_�������Ķ����"
        '�ֶ�����
        Public Const FIELD_GR_B_GONGGAOLAN_YUEDUQINGKUANG_CZYDM As String = "����Ա����"
        Public Const FIELD_GR_B_GONGGAOLAN_YUEDUQINGKUANG_XH As String = "���"
        Public Const FIELD_GR_B_GONGGAOLAN_YUEDUQINGKUANG_YDRY As String = "�Ķ���Ա"
        '������Ϣ(��ʾ/�༭ʱ��)
        'Լ��������Ϣ

        '������_B_�������Ķ���Χ������Ϣ����
        '������
        Public Const TABLE_GR_B_GONGGAOLAN_YUEDUFANWEI As String = "����_B_�������Ķ���Χ"
        '�ֶ�����
        Public Const FIELD_GR_B_GONGGAOLAN_YUEDUFANWEI_CZYDM As String = "����Ա����"
        Public Const FIELD_GR_B_GONGGAOLAN_YUEDUFANWEI_XH As String = "���"
        Public Const FIELD_GR_B_GONGGAOLAN_YUEDUFANWEI_YDRY As String = "�Ķ���Ա"
        '������Ϣ(��ʾ/�༭ʱ��)
        'Լ��������Ϣ




        '�����ʼ��������enum
        Public Enum enumTableType
            GR_B_GONGGAOLAN = 1
            GR_B_GONGGAOLAN_YUEDUQINGKUANG = 2
            GR_B_GONGGAOLAN_YUEDUFANWEI = 3
            DZGG_B_FUJIAN = 4
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.Common.Data.ggxxDianzigonggaoData)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub









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
                Case enumTableType.GR_B_GONGGAOLAN
                    table = createDataTables_Gonggaolan(strErrMsg)

                Case enumTableType.GR_B_GONGGAOLAN_YUEDUQINGKUANG
                    table = createDataTables_Gonggaolan_YueduQingkuang(strErrMsg)

                Case enumTableType.GR_B_GONGGAOLAN_YUEDUFANWEI
                    table = createDataTables_Gonggaolan_YueduFanwei(strErrMsg)

                Case enumTableType.DZGG_B_FUJIAN
                    table = createDataTables_DZGG_FUJIAN(strErrMsg)
                Case Else
                    strErrMsg = "��Ч�ı����ͣ�"
                    table = Nothing
            End Select

            createDataTables = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_DZGG_B_FUJIAN
        '----------------------------------------------------------------
        Private Function createDataTables_DZGG_FUJIAN(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_DZGG_B_FUJIAN)
                With table.Columns
                    .Add(FIELD_DZGG_B_FUJIAN_WJBS, GetType(System.String))
                    .Add(FIELD_DZGG_B_FUJIAN_WJXH, GetType(System.Int32))

                    .Add(FIELD_DZGG_B_FUJIAN_WJSM, GetType(System.String))
                    .Add(FIELD_DZGG_B_FUJIAN_WJYS, GetType(System.Int32))
                    .Add(FIELD_DZGG_B_FUJIAN_WJWZ, GetType(System.String))

                    .Add(FIELD_DZGG_B_FUJIAN_XSXH, GetType(System.Int32))
                    .Add(FIELD_DZGG_B_FUJIAN_BDWJ, GetType(System.String))
                    .Add(FIELD_DZGG_B_FUJIAN_XZBZ, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_DZGG_FUJIAN = table

        End Function


        '----------------------------------------------------------------
        '����TABLE_GR_B_GONGGAOLAN
        '----------------------------------------------------------------
        Private Function createDataTables_Gonggaolan(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GR_B_GONGGAOLAN)
                With table.Columns
                    .Add(FIELD_GR_B_GONGGAOLAN_CZYDM, GetType(System.String))
                    .Add(FIELD_GR_B_GONGGAOLAN_XH, GetType(System.Int32))
                    .Add(FIELD_GR_B_GONGGAOLAN_WJBS, GetType(System.String))
                    .Add(FIELD_GR_B_GONGGAOLAN_ZZDM, GetType(System.String))
                    .Add(FIELD_GR_B_GONGGAOLAN_ZZMC, GetType(System.String))
                    .Add(FIELD_GR_B_GONGGAOLAN_CZY, GetType(System.String))
                    .Add(FIELD_GR_B_GONGGAOLAN_RQ, GetType(System.DateTime))
                    .Add(FIELD_GR_B_GONGGAOLAN_BT, GetType(System.String))
                    .Add(FIELD_GR_B_GONGGAOLAN_NR, GetType(System.String))
                    .Add(FIELD_GR_B_GONGGAOLAN_ZWNR, GetType(System.String))
                    .Add(FIELD_GR_B_GONGGAOLAN_BLRQ, GetType(System.DateTime))
                    .Add(FIELD_GR_B_GONGGAOLAN_FBBS, GetType(System.Int32))
                    .Add(FIELD_GR_B_GONGGAOLAN_YDKZ, GetType(System.String))
                    .Add(FIELD_GR_B_GONGGAOLAN_YDFW, GetType(System.String))

                    .Add(FIELD_GR_B_GONGGAOLAN_SFYD, GetType(System.String))
                    .Add(FIELD_GR_B_GONGGAOLAN_FBMS, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Gonggaolan = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GR_B_GONGGAOLAN_YUEDUQINGKUANG
        '----------------------------------------------------------------
        Private Function createDataTables_Gonggaolan_YueduQingkuang(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GR_B_GONGGAOLAN_YUEDUQINGKUANG)
                With table.Columns
                    .Add(FIELD_GR_B_GONGGAOLAN_YUEDUQINGKUANG_CZYDM, GetType(System.String))
                    .Add(FIELD_GR_B_GONGGAOLAN_YUEDUQINGKUANG_XH, GetType(System.Int32))
                    .Add(FIELD_GR_B_GONGGAOLAN_YUEDUQINGKUANG_YDRY, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Gonggaolan_YueduQingkuang = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GR_B_GONGGAOLAN_YUEDUFANWEI
        '----------------------------------------------------------------
        Private Function createDataTables_Gonggaolan_YueduFanwei(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GR_B_GONGGAOLAN_YUEDUFANWEI)
                With table.Columns
                    .Add(FIELD_GR_B_GONGGAOLAN_YUEDUQINGKUANG_CZYDM, GetType(System.String))
                    .Add(FIELD_GR_B_GONGGAOLAN_YUEDUQINGKUANG_XH, GetType(System.Int32))
                    .Add(FIELD_GR_B_GONGGAOLAN_YUEDUQINGKUANG_YDRY, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Gonggaolan_YueduFanwei = table

        End Function

    End Class 'ggxxDianzigonggaoData

End Namespace 'Xydc.Platform.Common.Data
