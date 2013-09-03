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
    ' ����    ��CustomerData
    '
    ' ����������
    '     ��������Ա��Ϣ��ر�����ݷ��ʸ�ʽ
    '----------------------------------------------------------------
    <System.ComponentModel.DesignerCategory("BASE"), SerializableAttribute()> Public Class CustomerData
        Inherits System.Data.DataSet

        '��������
        Public Const STATUS_LOGIN As String = "��¼"
        Public Const STATUS_LOGOUT As String = "�˳�"

        '����_B_��Ա����Ϣ����
        '������
        Public Const TABLE_GG_B_RENYUAN As String = "����_B_��Ա"
        '�ֶ�����
        Public Const FIELD_GG_B_RENYUAN_RYDM As String = "��Ա����"
        Public Const FIELD_GG_B_RENYUAN_RYMC As String = "��Ա����"
        Public Const FIELD_GG_B_RENYUAN_RYXH As String = "��Ա���"
        Public Const FIELD_GG_B_RENYUAN_ZZDM As String = "��֯����"
        Public Const FIELD_GG_B_RENYUAN_JBDM As String = "�������"
        Public Const FIELD_GG_B_RENYUAN_MSDM As String = "�������"
        Public Const FIELD_GG_B_RENYUAN_LXDH As String = "��ϵ�绰"
        Public Const FIELD_GG_B_RENYUAN_SJHM As String = "�ֻ�����"
        Public Const FIELD_GG_B_RENYUAN_FTPDZ As String = "FTP��ַ"
        Public Const FIELD_GG_B_RENYUAN_YXDZ As String = "�����ַ"
        Public Const FIELD_GG_B_RENYUAN_ZDQS As String = "�Զ�ǩ��"
        Public Const FIELD_GG_B_RENYUAN_JJXSMC As String = "������ʾ����"
        Public Const FIELD_GG_B_RENYUAN_KCKXM As String = "�ɲ鿴����"
        Public Const FIELD_GG_B_RENYUAN_KZSRY As String = "��ֱ����Ա"
        Public Const FIELD_GG_B_RENYUAN_QTYZS As String = "������ת��"
        Public Const FIELD_GG_B_RENYUAN_SFJM As String = "�Ƿ����"

        Public Const FIELD_GG_B_RENYUAN_RYZM As String = "��Ա����"


        'Լ��������Ϣ

        '����_B_��֯��������Ϣ����
        '������
        Public Const TABLE_GG_B_ZUZHIJIGOU As String = "����_B_��֯����"
        '�ֶ�����
        Public Const FIELD_GG_B_ZUZHIJIGOU_ZZDM As String = "��֯����"
        Public Const FIELD_GG_B_ZUZHIJIGOU_ZZMC As String = "��֯����"
        Public Const FIELD_GG_B_ZUZHIJIGOU_ZZBM As String = "��֯����"
        Public Const FIELD_GG_B_ZUZHIJIGOU_JBDM As String = "�������"
        Public Const FIELD_GG_B_ZUZHIJIGOU_MSDM As String = "�������"
        Public Const FIELD_GG_B_ZUZHIJIGOU_LXDH As String = "��ϵ�绰"
        Public Const FIELD_GG_B_ZUZHIJIGOU_SJHM As String = "�ֻ�����"
        Public Const FIELD_GG_B_ZUZHIJIGOU_FTPDZ As String = "FTP��ַ"
        Public Const FIELD_GG_B_ZUZHIJIGOU_YXDZ As String = "�����ַ"
        Public Const FIELD_GG_B_ZUZHIJIGOU_LXDZ As String = "��ϵ��ַ"
        Public Const FIELD_GG_B_ZUZHIJIGOU_YZBM As String = "��������"
        Public Const FIELD_GG_B_ZUZHIJIGOU_LXR As String = "��ϵ��"
        'Լ��������Ϣ

        '����_B_�ϸڱ���Ϣ����
        '������
        Public Const TABLE_GG_B_SHANGGANG As String = "����_B_�ϸ�"
        '�ֶ�����
        Public Const FIELD_GG_B_SHANGGANG_RYDM As String = "��Ա����"
        Public Const FIELD_GG_B_SHANGGANG_GWDM As String = "��λ����"
        'Լ��������Ϣ

        '����_B_��Ա�����ȫ����
        '������
        Public Const TABLE_GG_B_RENYUAN_FULLJOIN As String = "����_B_��Ա��ȫ���ӱ�"
        '�ֶ�����
        Public Const FIELD_GG_B_RENYUAN_FULLJOIN_GWLB As String = "��λ�б�"
        Public Const FIELD_GG_B_RENYUAN_FULLJOIN_MSMC As String = "��������"
        Public Const FIELD_GG_B_RENYUAN_FULLJOIN_SFSQ As String = "�Ƿ�����"
        Public Const FIELD_GG_B_RENYUAN_FULLJOIN_QTYZSMC As String = "������ת������"


        '��ʾ�ֶ�
        Public Const FIELD_GG_B_RENYUAN_FULLJOIN_BH As String = "���"


        '��Ա/��λ/��Χѡ�����Ϣ
        '������
        Public Const TABLE_GG_B_RENYUAN_SELECT As String = "����_B_��Ա��λ��Χѡ���"
        '�ֶ�����
        Public Const FIELD_GG_B_RENYUAN_SELECT_MC As String = "����"
        Public Const FIELD_GG_B_RENYUAN_SELECT_LX As String = "����"
        Public Const FIELD_GG_B_RENYUAN_SELECT_XH As String = "���"
        Public Const FIELD_GG_B_RENYUAN_SELECT_BM As String = "����"
        Public Const FIELD_GG_B_RENYUAN_SELECT_ZW As String = "ְ��"
        Public Const FIELD_GG_B_RENYUAN_SELECT_JB As String = "����"
        Public Const FIELD_GG_B_RENYUAN_SELECT_MS As String = "����"
        Public Const FIELD_GG_B_RENYUAN_SELECT_LXDH As String = "��ϵ�绰"
        Public Const FIELD_GG_B_RENYUAN_SELECT_SJHM As String = "�ֻ�����"
        Public Const FIELD_GG_B_RENYUAN_SELECT_FTPDZ As String = "FTP��ַ"
        Public Const FIELD_GG_B_RENYUAN_SELECT_YXDZ As String = "�����ַ"

        '��λ/��Χѡ�����Ϣ
        '������
        Public Const TABLE_GG_B_ZUZHIJIGOU_SELECT As String = "����_B_��λ��Χѡ���"
        '�ֶ�����
        Public Const FIELD_GG_B_ZUZHIJIGOU_SELECT_DWMC As String = "��λ����"
        Public Const FIELD_GG_B_ZUZHIJIGOU_SELECT_XZLX As String = "ѡ������"
        Public Const FIELD_GG_B_ZUZHIJIGOU_SELECT_DWQC As String = "��λȫ��"
        Public Const FIELD_GG_B_ZUZHIJIGOU_SELECT_DWJB As String = "��λ����"
        Public Const FIELD_GG_B_ZUZHIJIGOU_SELECT_DWMS As String = "��λ����"
        Public Const FIELD_GG_B_ZUZHIJIGOU_SELECT_LXDH As String = "��ϵ�绰"
        Public Const FIELD_GG_B_ZUZHIJIGOU_SELECT_SJHM As String = "�ֻ�����"
        Public Const FIELD_GG_B_ZUZHIJIGOU_SELECT_FTPDZ As String = "FTP��ַ"
        Public Const FIELD_GG_B_ZUZHIJIGOU_SELECT_YXDZ As String = "�����ַ"

        '����_B_��֯���������ȫ����
        '������
        Public Const TABLE_GG_B_ZUZHIJIGOU_FULLJOIN As String = "����_B_��֯������ȫ���ӱ�"
        '�ֶ�����
        Public Const FIELD_GG_B_ZUZHIJIGOU_FULLJOIN_LXRMC As String = "��ϵ������"

        '������_B_ϵͳ������־����Ϣ
        '������
        Public Const TABLE_GL_B_XITONGJINCHURIZHI As String = "����_B_ϵͳ������־"
        '�ֶ�����
        Public Const FIELD_GL_B_XITONGJINCHURIZHI_XH As String = "���"
        Public Const FIELD_GL_B_XITONGJINCHURIZHI_CZR As String = "������"
        Public Const FIELD_GL_B_XITONGJINCHURIZHI_CZSJ As String = "����ʱ��"
        Public Const FIELD_GL_B_XITONGJINCHURIZHI_CZLX As String = "��������"
        Public Const FIELD_GL_B_XITONGJINCHURIZHI_JQDZ As String = "������ַ"

        Public Const FIELD_GL_B_XITONGJINCHURIZHI_JQMC As String = "��������"


        '��ʾ�ֶ�����
        Public Const FIELD_GL_B_XITONGJINCHURIZHI_CZRMC As String = "����������"

        '������_B_�����û�����Ϣ
        '������
        Public Const TABLE_GL_B_ZAIXIANYONGHU As String = "����_B_�����û�"
        '�ֶ�����
        Public Const FIELD_GL_B_ZAIXIANYONGHU_CZR As String = "������"
        Public Const FIELD_GL_B_ZAIXIANYONGHU_SXSJ As String = "����ʱ��"
        '��ʾ�ֶ�����
        Public Const FIELD_GL_B_ZAIXIANYONGHU_CZRMC As String = "����������"
        Public Const FIELD_GL_B_ZAIXIANYONGHU_SXSC As String = "����ʱ��"

        '������_B_�û�������־����Ϣ
        '������
        Public Const TABLE_GL_B_YONGHUCAOZUORIZHI As String = "����_B_�û�������־"
        '�ֶ�����
        Public Const FIELD_GL_B_YONGHUCAOZUORIZHI_XH As String = "���"
        Public Const FIELD_GL_B_YONGHUCAOZUORIZHI_CZR As String = "������"
        Public Const FIELD_GL_B_YONGHUCAOZUORIZHI_CZSJ As String = "����ʱ��"
        Public Const FIELD_GL_B_YONGHUCAOZUORIZHI_JQDZ As String = "������ַ"
        Public Const FIELD_GL_B_YONGHUCAOZUORIZHI_CZSM As String = "����˵��"

        Public Const FIELD_GL_B_YONGHUCAOZUORIZHI_JQMC As String = "��������"


        '��ʾ�ֶ�����








        '�����ʼ��������enum
        Public Enum enumTableType
            GG_B_RENYUAN = 1
            GG_B_RENYUAN_SELECT = 2
            GG_B_RENYUAN_FULLJOIN = 3
            GG_B_ZUZHIJIGOU = 4
            GG_B_ZUZHIJIGOU_SELECT = 5
            GG_B_ZUZHIJIGOU_FULLJOIN = 6
            GG_B_SHANGGANG = 7
            GL_B_XITONGJINCHURIZHI = 8
            GL_B_ZAIXIANYONGHU = 9
            GL_B_YONGHUCAOZUORIZHI = 10
        End Enum

        '��֯����ּ�����˵��
        Public Shared intZZDM_FJCDSM() As Integer = {2, 4, 6, 8, 10, 12}









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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.Common.Data.CustomerData)
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
                Case enumTableType.GG_B_RENYUAN
                    table = createDataTables_Renyuan(strErrMsg)
                Case enumTableType.GG_B_RENYUAN_SELECT
                    table = createDataTables_Renyuan_Select(strErrMsg)
                Case enumTableType.GG_B_RENYUAN_FULLJOIN
                    table = createDataTables_Renyuan_FullJoin(strErrMsg)

                Case enumTableType.GG_B_ZUZHIJIGOU
                    table = createDataTables_Zuzhijigou(strErrMsg)
                Case enumTableType.GG_B_ZUZHIJIGOU_SELECT
                    table = createDataTables_Zuzhijigou_Select(strErrMsg)
                Case enumTableType.GG_B_ZUZHIJIGOU_FULLJOIN
                    table = createDataTables_Zuzhijigou_FullJoin(strErrMsg)

                Case enumTableType.GG_B_SHANGGANG
                    table = createDataTables_Shanggang(strErrMsg)

                Case enumTableType.GL_B_XITONGJINCHURIZHI
                    table = createDataTables_Xitongjinchurizhi(strErrMsg)
                Case enumTableType.GL_B_ZAIXIANYONGHU
                    table = createDataTables_Zaixianyonghu(strErrMsg)
                Case enumTableType.GL_B_YONGHUCAOZUORIZHI
                    table = createDataTables_YonghuCaozuoRizhi(strErrMsg)

                Case Else
                    strErrMsg = "��Ч�ı����ͣ�"
                    table = Nothing
            End Select

            createDataTables = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GG_B_RENYUAN
        '----------------------------------------------------------------
        Private Function createDataTables_Renyuan(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GG_B_RENYUAN)
                With table.Columns
                    .Add(FIELD_GG_B_RENYUAN_RYDM, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_RYMC, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_RYXH, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_ZZDM, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_JBDM, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_MSDM, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_LXDH, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_SJHM, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_FTPDZ, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_YXDZ, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_ZDQS, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_JJXSMC, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_KCKXM, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_KZSRY, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_QTYZS, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_SFJM, GetType(System.Int32))

                    .Add(FIELD_GG_B_RENYUAN_RYZM, GetType(System.String))

                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Renyuan = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GG_B_ZUZHIJIGOU
        '----------------------------------------------------------------
        Private Function createDataTables_Zuzhijigou(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GG_B_ZUZHIJIGOU)
                With table.Columns
                    .Add(FIELD_GG_B_ZUZHIJIGOU_ZZDM, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_ZZMC, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_ZZBM, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_JBDM, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_MSDM, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_LXDH, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_SJHM, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_FTPDZ, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_YXDZ, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_LXDZ, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_YZBM, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_LXR, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Zuzhijigou = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GG_B_SHANGGANG
        '----------------------------------------------------------------
        Private Function createDataTables_Shanggang(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GG_B_SHANGGANG)
                With table.Columns
                    .Add(FIELD_GG_B_SHANGGANG_RYDM, GetType(System.String))
                    .Add(FIELD_GG_B_SHANGGANG_GWDM, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Shanggang = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GG_B_RENYUAN_FULLJOIN
        '----------------------------------------------------------------
        Private Function createDataTables_Renyuan_FullJoin(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GG_B_RENYUAN_FULLJOIN)
                With table.Columns


                    '����_B_��Աȫ���ֶ�

                    .Add(FIELD_GG_B_RENYUAN_FULLJOIN_BH, GetType(System.String))

                    .Add(FIELD_GG_B_RENYUAN_RYDM, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_RYMC, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_RYXH, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_ZZDM, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_JBDM, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_MSDM, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_LXDH, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_SJHM, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_FTPDZ, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_YXDZ, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_ZDQS, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_JJXSMC, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_KCKXM, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_KZSRY, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_QTYZS, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_SFJM, GetType(System.Int32))

                    '����_B_��֯���������֯���ơ���֯����
                    .Add(FIELD_GG_B_ZUZHIJIGOU_ZZMC, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_ZZBM, GetType(System.String))

                    '����_B_�ϸڱ��Ӧ�Ĺ���_B_������λ�еĸ�λ���Ƽ��ϣ��ֺŷָ���
                    .Add(FIELD_GG_B_RENYUAN_FULLJOIN_GWLB, GetType(System.String))

                    '����_B_���������еļ������ơ���������
                    .Add(Xydc.Platform.Common.Data.XingzhengjibieData.FIELD_GG_B_XINGZHENGJIBIE_JBMC, GetType(System.String))
                    .Add(Xydc.Platform.Common.Data.XingzhengjibieData.FIELD_GG_B_XINGZHENGJIBIE_XZJB, GetType(System.Int32))

                    '����_B_��Ա�м���������������
                    .Add(FIELD_GG_B_RENYUAN_FULLJOIN_MSMC, GetType(System.String))

                    '����_B_��Ա�м�������������ת��������
                    .Add(FIELD_GG_B_RENYUAN_FULLJOIN_QTYZSMC, GetType(System.String))

                    '�Ƿ�����ID?
                    .Add(FIELD_GG_B_RENYUAN_FULLJOIN_SFSQ, GetType(System.String))

                    .Add(FIELD_GG_B_RENYUAN_RYZM, GetType(System.String))



                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Renyuan_FullJoin = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GG_B_RENYUAN_SELECT
        '----------------------------------------------------------------
        Private Function createDataTables_Renyuan_Select(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GG_B_RENYUAN_SELECT)
                With table.Columns
                    .Add(FIELD_GG_B_RENYUAN_SELECT_MC, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_SELECT_LX, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_SELECT_XH, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_SELECT_BM, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_SELECT_ZW, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_SELECT_JB, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_SELECT_MS, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_SELECT_LXDH, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_SELECT_SJHM, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_SELECT_FTPDZ, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_SELECT_YXDZ, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Renyuan_Select = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GG_B_ZUZHIJIGOU_FULLJOIN
        '----------------------------------------------------------------
        Private Function createDataTables_Zuzhijigou_FullJoin(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GG_B_ZUZHIJIGOU_FULLJOIN)
                With table.Columns
                    .Add(FIELD_GG_B_ZUZHIJIGOU_ZZDM, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_ZZMC, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_ZZBM, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_JBDM, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_MSDM, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_LXDH, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_SJHM, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_FTPDZ, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_YXDZ, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_LXDZ, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_YZBM, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_LXR, GetType(System.String))

                    '����_B_���������еļ������ơ���������
                    .Add(Xydc.Platform.Common.Data.XingzhengjibieData.FIELD_GG_B_XINGZHENGJIBIE_JBMC, GetType(System.String))
                    .Add(Xydc.Platform.Common.Data.XingzhengjibieData.FIELD_GG_B_XINGZHENGJIBIE_XZJB, GetType(System.Int32))

                    '����_B_��Ա�м���������������
                    .Add(FIELD_GG_B_RENYUAN_FULLJOIN_MSMC, GetType(System.String))

                    '����_B_��Ա�м���������ϵ������
                    .Add(FIELD_GG_B_ZUZHIJIGOU_FULLJOIN_LXRMC, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Zuzhijigou_FullJoin = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GG_B_ZUZHIJIGOU_SELECT
        '----------------------------------------------------------------
        Private Function createDataTables_Zuzhijigou_Select(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GG_B_ZUZHIJIGOU_SELECT)
                With table.Columns
                    .Add(FIELD_GG_B_ZUZHIJIGOU_SELECT_DWMC, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_SELECT_XZLX, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_SELECT_DWQC, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_SELECT_DWJB, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_SELECT_DWMS, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_SELECT_LXDH, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_SELECT_SJHM, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_SELECT_FTPDZ, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_SELECT_YXDZ, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Zuzhijigou_Select = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GL_B_XITONGJINCHURIZHI
        '----------------------------------------------------------------
        Private Function createDataTables_Xitongjinchurizhi(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GL_B_XITONGJINCHURIZHI)
                With table.Columns
                    .Add(FIELD_GL_B_XITONGJINCHURIZHI_XH, GetType(System.Int32))
                    .Add(FIELD_GL_B_XITONGJINCHURIZHI_CZR, GetType(System.String))
                    .Add(FIELD_GL_B_XITONGJINCHURIZHI_CZSJ, GetType(System.DateTime))
                    .Add(FIELD_GL_B_XITONGJINCHURIZHI_CZLX, GetType(System.String))
                    .Add(FIELD_GL_B_XITONGJINCHURIZHI_JQDZ, GetType(System.String))

                    .Add(FIELD_GL_B_XITONGJINCHURIZHI_CZRMC, GetType(System.String))

                    .Add(FIELD_GL_B_XITONGJINCHURIZHI_JQMC, GetType(System.String))

                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Xitongjinchurizhi = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GL_B_ZAIXIANYONGHU
        '----------------------------------------------------------------
        Private Function createDataTables_Zaixianyonghu(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GL_B_ZAIXIANYONGHU)
                With table.Columns
                    .Add(FIELD_GL_B_ZAIXIANYONGHU_CZR, GetType(System.String))
                    .Add(FIELD_GL_B_ZAIXIANYONGHU_SXSJ, GetType(System.DateTime))

                    .Add(FIELD_GL_B_ZAIXIANYONGHU_CZRMC, GetType(System.String))
                    .Add(FIELD_GL_B_ZAIXIANYONGHU_SXSC, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Zaixianyonghu = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GL_B_YONGHUCAOZUORIZHI
        '----------------------------------------------------------------
        Private Function createDataTables_YonghuCaozuoRizhi(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GL_B_YONGHUCAOZUORIZHI)
                With table.Columns
                    .Add(FIELD_GL_B_YONGHUCAOZUORIZHI_XH, GetType(System.Int32))
                    .Add(FIELD_GL_B_YONGHUCAOZUORIZHI_CZR, GetType(System.String))
                    .Add(FIELD_GL_B_YONGHUCAOZUORIZHI_CZSJ, GetType(System.DateTime))
                    .Add(FIELD_GL_B_YONGHUCAOZUORIZHI_JQDZ, GetType(System.String))
                    .Add(FIELD_GL_B_YONGHUCAOZUORIZHI_CZSM, GetType(System.String))

                    .Add(FIELD_GL_B_YONGHUCAOZUORIZHI_JQMC, GetType(System.String))

                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_YonghuCaozuoRizhi = table

        End Function

    End Class 'CustomerData

End Namespace 'Xydc.Platform.Common.Data
