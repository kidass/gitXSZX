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
    ' ����    ��ggxxGonggongziyuanData
    '
    ' ����������
    '     ���塰������Դ���йص����ݷ��ʸ�ʽ
    '----------------------------------------------------------------
    <System.ComponentModel.DesignerCategory("Code"), SerializableAttribute()> Public Class ggxxGonggongziyuanData
        Inherits System.Data.DataSet

        '������Դ����
        Public Enum enumZiyuanType
            Text = 0       '���ı�
            Image = 1      'ͼƬ�ļ�
            Html = 2       'Html�ļ�
            Office = 3     'Office�ļ�
            Media = 4      'ý���ļ�
            Other = 5      '�����ļ�
            Tuwen = 6      'ͼ��
        End Enum

        '������Դ�ļ�Ŀ¼
        Public Const FILEDIR_GGZY_WJ As String = "GGXX\GGZY\WJ"

        '����Ϣ_B_������Դ_��Ŀ������
        '������
        Public Const TABLE_XX_B_GONGGONGZIYUAN_LANMU As String = "��Ϣ_B_������Դ_��Ŀ"
        '�ֶ�����
        Public Const FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMBS As String = "��Ŀ��ʶ"
        Public Const FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMDM As String = "��Ŀ����"
        Public Const FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMJB As String = "��Ŀ����"
        Public Const FIELD_XX_B_GONGGONGZIYUAN_LANMU_BJDM As String = "��������"
        Public Const FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMMC As String = "��Ŀ����"
        Public Const FIELD_XX_B_GONGGONGZIYUAN_LANMU_DJLM As String = "������Ŀ"
        Public Const FIELD_XX_B_GONGGONGZIYUAN_LANMU_SJLM As String = "�ϼ���Ŀ"
        Public Const FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMSM As String = "˵��"
        'Լ��������Ϣ

        '����Ϣ_B_������Դ������
        '������
        Public Const TABLE_XX_B_GONGGONGZIYUAN As String = "��Ϣ_B_������Դ"
        '�ֶ�����
        Public Const FIELD_XX_B_GONGGONGZIYUAN_ZYBS As String = "��Դ��ʶ"
        Public Const FIELD_XX_B_GONGGONGZIYUAN_ZYXH As String = "��Դ���"
        Public Const FIELD_XX_B_GONGGONGZIYUAN_FBRQ As String = "��������"
        Public Const FIELD_XX_B_GONGGONGZIYUAN_LMBS As String = "��Ŀ��ʶ"
        Public Const FIELD_XX_B_GONGGONGZIYUAN_RYDM As String = "��Ա����"
        Public Const FIELD_XX_B_GONGGONGZIYUAN_ZZDM As String = "��֯����"
        Public Const FIELD_XX_B_GONGGONGZIYUAN_NRLX As String = "��������"
        Public Const FIELD_XX_B_GONGGONGZIYUAN_ZYBT As String = "��Դ����"
        Public Const FIELD_XX_B_GONGGONGZIYUAN_ZYNR As String = "��Դ����"
        Public Const FIELD_XX_B_GONGGONGZIYUAN_WJWZ As String = "�ļ�λ��"
        Public Const FIELD_XX_B_GONGGONGZIYUAN_BLRQ As String = "��������"
        Public Const FIELD_XX_B_GONGGONGZIYUAN_FBBS As String = "������ʶ"
        Public Const FIELD_XX_B_GONGGONGZIYUAN_FBKZ As String = "��������"
        Public Const FIELD_XX_B_GONGGONGZIYUAN_FBFW As String = "������Χ"
        '��ʾ�ֶ�����
        Public Const FIELD_XX_B_GONGGONGZIYUAN_LMMC As String = "��Ŀ����" '��Ŀ��ʶ
        Public Const FIELD_XX_B_GONGGONGZIYUAN_LMDM As String = "��Ŀ����" '��Ŀ��ʶ
        Public Const FIELD_XX_B_GONGGONGZIYUAN_RYMC As String = "��Ա����" '��Ա����
        Public Const FIELD_XX_B_GONGGONGZIYUAN_ZZMC As String = "��֯����" '��֯����
        Public Const FIELD_XX_B_GONGGONGZIYUAN_FBMS As String = "��������" '������ʶ
        Public Const FIELD_XX_B_GONGGONGZIYUAN_KZMS As String = "��������" '��������
        Public Const FIELD_XX_B_GONGGONGZIYUAN_YDMS As String = "�Ķ�����"
        'Լ��������Ϣ

        '����Ϣ_B_������Դ_�Ķ����������
        '������
        Public Const TABLE_XX_B_GONGGONGZIYUAN_YUEDUQINGKUANG As String = "��Ϣ_B_������Դ_�Ķ����"
        '�ֶ�����
        Public Const FIELD_XX_B_GONGGONGZIYUAN_YUEDUQINGKUANG_ZYBS As String = "��Դ��ʶ"
        Public Const FIELD_XX_B_GONGGONGZIYUAN_YUEDUQINGKUANG_RYDM As String = "��Ա����"
        '��ʾ�ֶ�����
        Public Const FIELD_XX_B_GONGGONGZIYUAN_YUEDUQINGKUANG_RYMC As String = "��Ա����" '��Ա����
        'Լ��������Ϣ

        '����Ϣ_B_������Դ_�Ķ���Χ������
        '������
        Public Const TABLE_XX_B_GONGGONGZIYUAN_YUEDUFANWEI As String = "��Ϣ_B_������Դ_�Ķ���Χ"
        '�ֶ�����
        Public Const FIELD_XX_B_GONGGONGZIYUAN_YUEDUFANWEI_ZYBS As String = "��Դ��ʶ"
        Public Const FIELD_XX_B_GONGGONGZIYUAN_YUEDUFANWEI_RYDM As String = "��Ա����"
        '��ʾ�ֶ�����
        Public Const FIELD_XX_B_GONGGONGZIYUAN_YUEDUFANWEI_RYMC As String = "��Ա����" '��Ա����
        'Լ��������Ϣ


        '�����ʼ��������enum
        Public Enum enumTableType
            XX_B_GONGGONGZIYUAN_LANMU = 1
            XX_B_GONGGONGZIYUAN = 2
            XX_B_GONGGONGZIYUAN_YUEDUQINGKUANG = 3
            XX_B_GONGGONGZIYUAN_YUEDUFANWEI = 4
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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.Common.Data.ggxxGonggongziyuanData)
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
                Case enumTableType.XX_B_GONGGONGZIYUAN_LANMU
                    table = createDataTables_Lanmu(strErrMsg)

                Case enumTableType.XX_B_GONGGONGZIYUAN
                    table = createDataTables_Ziyuan(strErrMsg)
                Case enumTableType.XX_B_GONGGONGZIYUAN_YUEDUQINGKUANG
                    table = createDataTables_Ziyuan_YueduQingkuang(strErrMsg)
                Case enumTableType.XX_B_GONGGONGZIYUAN_YUEDUFANWEI
                    table = createDataTables_Ziyuan_YueduFanwei(strErrMsg)

                Case Else
                    strErrMsg = "��Ч�ı����ͣ�"
                    table = Nothing
            End Select

            createDataTables = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_XX_B_GONGGONGZIYUAN_LANMU
        '----------------------------------------------------------------
        Private Function createDataTables_Lanmu(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_XX_B_GONGGONGZIYUAN_LANMU)
                With table.Columns
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMBS, GetType(System.Int32))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMDM, GetType(System.String))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMJB, GetType(System.Int32))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_LANMU_BJDM, GetType(System.Int32))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMMC, GetType(System.String))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_LANMU_DJLM, GetType(System.Int32))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_LANMU_SJLM, GetType(System.Int32))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_LANMU_LMSM, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Lanmu = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_XX_B_GONGGONGZIYUAN
        '----------------------------------------------------------------
        Private Function createDataTables_Ziyuan(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_XX_B_GONGGONGZIYUAN)
                With table.Columns
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_ZYBS, GetType(System.String))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_ZYXH, GetType(System.Int32))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_FBRQ, GetType(System.DateTime))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_LMBS, GetType(System.String))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_RYDM, GetType(System.String))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_ZZDM, GetType(System.String))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_NRLX, GetType(System.Int32))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_ZYBT, GetType(System.String))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_ZYNR, GetType(System.String))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_WJWZ, GetType(System.String))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_BLRQ, GetType(System.DateTime))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_FBBS, GetType(System.Int32))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_FBKZ, GetType(System.Int32))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_FBFW, GetType(System.String))

                    .Add(FIELD_XX_B_GONGGONGZIYUAN_LMMC, GetType(System.String))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_RYMC, GetType(System.String))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_ZZMC, GetType(System.String))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_FBMS, GetType(System.String))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_KZMS, GetType(System.String))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_YDMS, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Ziyuan = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_XX_B_GONGGONGZIYUAN_YUEDUQINGKUANG
        '----------------------------------------------------------------
        Private Function createDataTables_Ziyuan_YueduQingkuang(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_XX_B_GONGGONGZIYUAN_YUEDUQINGKUANG)
                With table.Columns
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_YUEDUQINGKUANG_ZYBS, GetType(System.String))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_YUEDUQINGKUANG_RYDM, GetType(System.String))

                    .Add(FIELD_XX_B_GONGGONGZIYUAN_YUEDUQINGKUANG_RYMC, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Ziyuan_YueduQingkuang = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_XX_B_GONGGONGZIYUAN_YUEDUFANWEI
        '----------------------------------------------------------------
        Private Function createDataTables_Ziyuan_YueduFanwei(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_XX_B_GONGGONGZIYUAN_YUEDUFANWEI)
                With table.Columns
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_YUEDUFANWEI_ZYBS, GetType(System.String))
                    .Add(FIELD_XX_B_GONGGONGZIYUAN_YUEDUFANWEI_RYDM, GetType(System.String))

                    .Add(FIELD_XX_B_GONGGONGZIYUAN_YUEDUFANWEI_RYMC, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Ziyuan_YueduFanwei = table

        End Function

    End Class 'ggxxGonggongziyuanData

End Namespace 'Xydc.Platform.Common.Data
