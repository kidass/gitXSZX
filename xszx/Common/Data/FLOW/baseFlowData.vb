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
    ' ����    ��FlowData
    '
    ' ����������
    '   �����塰����_B_���ӡ���������_B_������������_B_�߰족
    '     ������_B_���족��������_B_������־����������_B_��������
    '     ������_B_����ļ�����������_B_����ļ�������
    '     ����ص����ݷ��ʸ�ʽ
    '----------------------------------------------------------------
    <System.ComponentModel.DesignerCategory("Code"), SerializableAttribute()> Public Class FlowData
        Inherits System.Data.DataSet

        'ԭ���Ӻ�<=0ʱ�����⺬�壨ͨ�ã�
        Public Const YJJH_YIBANTONGZHI As Integer = 0        'һ��֪ͨ
        Public Const YJJH_ZHUANSONGQINGQIU As Integer = -1   'ת�Ͳ�������
        Public Const YJJH_ZHUDONGBUYUE As Integer = -2       '��������

        '������_B_���ӡ�����Ϣ����
        '�������  ���ļ�������ˮ�ţ�ÿ���Ĵ�1��ʼ��
        'ԭ���Ӻ�  ��<=0�����⺬��
        '            >  0����ʾ���������һ�ν��յĽ������
        '            =  0��һ��֪ͨ
        '            = -1��ת������
        '            = -2����������
        '            = -3������֪ͨ
        '            = -4��ת�ͻ���֪ͨ
        '            = -5������֪ͨ
        '            = -6��ת�͵���֪ͨ
        '            = -7�����Ĵ߻�֪ͨ
        '�������  �����ں�
        '�������  �������е�˳���
        '��������  �����ġ����ġ�������
        '��������  ��
        '            ��������  ����ǩ�ա��Ǽǡ���졢�������а졢�鵵����ת
        '            ���ڷ���  �������⡢��ˡ���ǩ�����ˡ�ǩ�����Ǽǡ���ӡ����ӡ���ַ����鵵����ת
        '            ���ڱ������������⡢��ˡ���ǩ��ǩ�����鵵����ת
        '����״̬  ��0-δ���ա�1-���ڰ���2-������ϡ�3-�ļ����ջء�4-�ļ����˻�
        '���ӱ�ʶ  ��ABCDEFGH
        '           1-A = 0-�ļ���δ����
        '           1-A = 1-�ļ����͹�
        '           2-B = 0-�����˲��ܿ������ӵ�
        '           2-B = 1-�����˿ɿ������ӵ�
        '           3-C = 0-�����˲��ܿ������ӵ�
        '           3-C = 1-�����˿ɿ������ӵ�
        '           4-D = 0-����
        '           4-D = 1-�˻�
        '           5-E = 0-����
        '           5-E = 1-�ջ�
        '           6-F = 0-����
        '           6-F = 1-֪ͨ
        '           7-G = 0-����
        '           7-G = 1-�ظ�
        '           8-H = 0-�ļ�δ����
        '           8-H = 1-�ļ��Ѱ���
        '������
        Public Const TABLE_GW_B_JIAOJIE As String = "����_B_����"
        '�ֶ�����
        Public Const FIELD_GW_B_JIAOJIE_WJBS As String = "�ļ���ʶ"
        Public Const FIELD_GW_B_JIAOJIE_JJXH As String = "�������"
        Public Const FIELD_GW_B_JIAOJIE_YJJH As String = "ԭ���Ӻ�"
        Public Const FIELD_GW_B_JIAOJIE_FSXH As String = "�������"
        Public Const FIELD_GW_B_JIAOJIE_FSR As String = "������"
        Public Const FIELD_GW_B_JIAOJIE_FSRQ As String = "��������"
        Public Const FIELD_GW_B_JIAOJIE_FSZZWJ As String = "����ֽ���ļ�"
        Public Const FIELD_GW_B_JIAOJIE_FSDZWJ As String = "���͵����ļ�"
        Public Const FIELD_GW_B_JIAOJIE_FSZZFJ As String = "����ֽ�ʸ���"
        Public Const FIELD_GW_B_JIAOJIE_FSDZFJ As String = "���͵��Ӹ���"
        Public Const FIELD_GW_B_JIAOJIE_JSXH As String = "�������"
        Public Const FIELD_GW_B_JIAOJIE_JSR As String = "������"
        Public Const FIELD_GW_B_JIAOJIE_XB As String = "Э��"
        Public Const FIELD_GW_B_JIAOJIE_JSRQ As String = "��������"
        Public Const FIELD_GW_B_JIAOJIE_JSZZWJ As String = "����ֽ���ļ�"
        Public Const FIELD_GW_B_JIAOJIE_JSDZWJ As String = "���յ����ļ�"
        Public Const FIELD_GW_B_JIAOJIE_JSZZFJ As String = "����ֽ�ʸ���"
        Public Const FIELD_GW_B_JIAOJIE_JSDZFJ As String = "���յ��Ӹ���"
        Public Const FIELD_GW_B_JIAOJIE_BLZHQX As String = "�����������"
        Public Const FIELD_GW_B_JIAOJIE_WCRQ As String = "�������"
        Public Const FIELD_GW_B_JIAOJIE_WTR As String = "ί����"
        Public Const FIELD_GW_B_JIAOJIE_BLLX As String = "��������"
        Public Const FIELD_GW_B_JIAOJIE_BLZL As String = "��������"
        Public Const FIELD_GW_B_JIAOJIE_BLZT As String = "����״̬"
        Public Const FIELD_GW_B_JIAOJIE_JJBS As String = "���ӱ�ʶ"
        Public Const FIELD_GW_B_JIAOJIE_SFDG As String = "�Ƿ����"
        Public Const FIELD_GW_B_JIAOJIE_JJSM As String = "����˵��"
        Public Const FIELD_GW_B_JIAOJIE_BWTX As String = "��������"

        Public Const FIELD_GW_B_JIAOJIE_JJBZ As String = "���ӱ�ע"

        'Լ��������Ϣ

        '������_B_��������Ϣ����
        '�Ƿ���׼����Է��ĵ�ǩ�������ĵ���������������ǩ����
        '�Ƿ���׼��־λ���壺
        '    ���ġ�������
        '        ��1λ��0-��Ч  ��1-��Ч
        '        ��2λ��0-��ͬ�⣬1-ͬ��
        '        ��3λ��0-����  ��1-�������
        '        ��4λ��δ��
        '    ����
        '        ��1λ��0-��Ч  ��1-��Ч
        '        ��2λ��0-����  ��1-��������
        '        ��3λ��0-Ȧ��  ��1-�ҵ����
        '        ��4λ��0-��    , 1-��
        '    ����
        '        ��1  λ��0-��Ч  ��1-��Ч
        '        ��2-3λ��10-ͬ�⣬11-ת�ͣ�00-��ͬ��
        '        ��4  λ��δ��
        '�������ڣ���д���������
        '��д���ڣ���д������������
        '��    ע������_B_���ӱ��еİ��������п����빫��_B_������еİ��������¼�ò�һ��
        '������
        Public Const TABLE_GW_B_BANLI As String = "����_B_����"
        '�ֶ�����
        Public Const FIELD_GW_B_BANLI_WJBS As String = "�ļ���ʶ"
        Public Const FIELD_GW_B_BANLI_JJXH As String = "�������"
        Public Const FIELD_GW_B_BANLI_BLR As String = "������"
        Public Const FIELD_GW_B_BANLI_BLLX As String = "��������"
        Public Const FIELD_GW_B_BANLI_BLZL As String = "��������"

        Public Const FIELD_GW_B_BANLI_XSXH As String = "��ʾ���"

        Public Const FIELD_GW_B_BANLI_BLRQ As String = "��������"
        Public Const FIELD_GW_B_BANLI_SFPZ As String = "�Ƿ���׼"
        Public Const FIELD_GW_B_BANLI_BLYJ As String = "�������"
        Public Const FIELD_GW_B_BANLI_BJNR As String = "�������"
        Public Const FIELD_GW_B_BANLI_DLR As String = "������"
        Public Const FIELD_GW_B_BANLI_DLRQ As String = "��������"
        Public Const FIELD_GW_B_BANLI_BLJG As String = "������"
        Public Const FIELD_GW_B_BANLI_TXRQ As String = "��д����"
        Public Const FIELD_GW_B_BANLI_XZYDRY As String = "�����Ķ���Ա"
        'Լ��������Ϣ

        '������_B_�߰족����Ϣ����
        '������
        Public Const TABLE_GW_B_CUIBAN As String = "����_B_�߰�"
        '�ֶ�����
        Public Const FIELD_GW_B_CUIBAN_WJBS As String = "�ļ���ʶ"
        Public Const FIELD_GW_B_CUIBAN_JJXH As String = "�������"
        Public Const FIELD_GW_B_CUIBAN_CBXH As String = "�߰����"
        Public Const FIELD_GW_B_CUIBAN_CBR As String = "�߰���"
        Public Const FIELD_GW_B_CUIBAN_CBRQ As String = "�߰�����"
        Public Const FIELD_GW_B_CUIBAN_BCBR As String = "���߰���"
        Public Const FIELD_GW_B_CUIBAN_CBSM As String = "�߰�˵��"
        'Լ��������Ϣ

        '������_B_���족����Ϣ����
        '������
        Public Const TABLE_GW_B_DUBAN As String = "����_B_����"
        '�ֶ�����
        Public Const FIELD_GW_B_DUBAN_WJBS As String = "�ļ���ʶ"
        Public Const FIELD_GW_B_DUBAN_JJXH As String = "�������"
        Public Const FIELD_GW_B_DUBAN_DBXH As String = "�������"
        Public Const FIELD_GW_B_DUBAN_DBR As String = "������"
        Public Const FIELD_GW_B_DUBAN_DBRQ As String = "��������"
        Public Const FIELD_GW_B_DUBAN_BDBR As String = "��������"
        Public Const FIELD_GW_B_DUBAN_DBYQ As String = "����Ҫ��"
        Public Const FIELD_GW_B_DUBAN_DBJG As String = "������"
        'Լ��������Ϣ

        '������_B_������־������Ϣ����
        '������
        Public Const TABLE_GW_B_CAOZUORIZHI As String = "����_B_������־"
        '�ֶ�����
        Public Const FIELD_GW_B_CAOZUORIZHI_WJBS As String = "�ļ���ʶ"
        Public Const FIELD_GW_B_CAOZUORIZHI_CZXH As String = "�������"
        Public Const FIELD_GW_B_CAOZUORIZHI_CZR As String = "������"
        Public Const FIELD_GW_B_CAOZUORIZHI_CZSJ As String = "����ʱ��"
        Public Const FIELD_GW_B_CAOZUORIZHI_CZSM As String = "����˵��"
        'Լ��������Ϣ

        Public Enum enumFileDownloadStatus
            NotDownload = 0 'û������
            HasDownload = 1 '�Ѿ�����
        End Enum
        '������_B_����������Ϣ����
        '������
        Public Const TABLE_GW_B_FUJIAN As String = "����_B_����"
        '�ֶ�����
        Public Const FIELD_GW_B_FUJIAN_WJBS As String = "�ļ���ʶ"
        Public Const FIELD_GW_B_FUJIAN_WJXH As String = "���"
        Public Const FIELD_GW_B_FUJIAN_WJSM As String = "˵��"
        Public Const FIELD_GW_B_FUJIAN_WJYS As String = "ҳ��"
        Public Const FIELD_GW_B_FUJIAN_WJWZ As String = "λ��"        '�������ļ�λ��(�����FTP����·��)
        '������Ϣ(��ʾ/�༭ʱ��)
        Public Const FIELD_GW_B_FUJIAN_XSXH As String = "��ʾ���"
        Public Const FIELD_GW_B_FUJIAN_BDWJ As String = "�����ļ�"    '���غ���ļ�λ��(����·��)
        Public Const FIELD_GW_B_FUJIAN_XZBZ As String = "���ر�־"    '�Ƿ�����?
        'Լ��������Ϣ

        '������_B_����ļ�������Ϣ����
        '������
        Public Const TABLE_GW_B_XIANGGUANWENJIAN As String = "����_B_����ļ�"
        '�ֶ�����
        Public Const FIELD_GW_B_XIANGGUANWENJIAN_WJXH As String = "���"
        Public Const FIELD_GW_B_XIANGGUANWENJIAN_NBXH As String = "˳���"
        Public Const FIELD_GW_B_XIANGGUANWENJIAN_DQWJBS As String = "��ǰ�ļ���ʶ"
        Public Const FIELD_GW_B_XIANGGUANWENJIAN_DCWJBS As String = "�����ļ���ʶ"
        Public Const FIELD_GW_B_XIANGGUANWENJIAN_SJWJBS As String = "�ϼ��ļ���ʶ"
        'Լ��������Ϣ

        '������_B_����ļ�����������Ϣ����
        '������
        Public Const TABLE_GW_B_XIANGGUANWENJIANFUJIAN As String = "����_B_����ļ�����"
        '�ֶ�����
        Public Const FIELD_GW_B_XIANGGUANWENJIANFUJIAN_WJBS As String = "�ļ���ʶ"
        Public Const FIELD_GW_B_XIANGGUANWENJIANFUJIAN_WJXH As String = "���"
        Public Const FIELD_GW_B_XIANGGUANWENJIANFUJIAN_WJSM As String = "˵��"
        Public Const FIELD_GW_B_XIANGGUANWENJIANFUJIAN_WJYS As String = "ҳ��"
        Public Const FIELD_GW_B_XIANGGUANWENJIANFUJIAN_WJWZ As String = "λ��"
        '������Ϣ(��ʾ/�༭ʱ��)
        Public Const FIELD_GW_B_XIANGGUANWENJIANFUJIAN_XSXH As String = "��ʾ���"
        Public Const FIELD_GW_B_XIANGGUANWENJIANFUJIAN_BDWJ As String = "�����ļ�"  '���غ���ļ�λ��
        Public Const FIELD_GW_B_XIANGGUANWENJIANFUJIAN_XZBZ As String = "���ر�־"  '�Ƿ�����?
        'Լ��������Ϣ

        '������_B_����_���ӡ�����Ϣ����(����)
        '������
        Public Const TABLE_GW_B_DUBAN_JIAOJIE As String = "����_B_����_����"
        '�ֶ�����
        Public Const FIELD_GW_B_DUBAN_JIAOJIE_BCJG As String = "���ν��"
        'Լ��������Ϣ

        '������_B_�߰�_���ӡ�����Ϣ����(����)
        '������
        Public Const TABLE_GW_B_CUIBAN_JIAOJIE As String = "����_B_�߰�_����"
        '�ֶ�����
        'Լ��������Ϣ

        '������_B_�������������Ϣ����(����)
        '������
        Public Const TABLE_GW_B_SHENPIYIJIAN As String = "����_B_�������"
        '�ֶ�����
        Public Const FIELD_GW_B_SHENPIYIJIAN_WJBS As String = "�ļ���ʶ"
        Public Const FIELD_GW_B_SHENPIYIJIAN_JJXH As String = "�������"
        Public Const FIELD_GW_B_SHENPIYIJIAN_BLLX As String = "��������"
        Public Const FIELD_GW_B_SHENPIYIJIAN_BLZL As String = "��������"
        Public Const FIELD_GW_B_SHENPIYIJIAN_JSR As String = "������"
        Public Const FIELD_GW_B_SHENPIYIJIAN_XB As String = "Э��"
        Public Const FIELD_GW_B_SHENPIYIJIAN_SFTY As String = "�Ƿ�ͬ��"
        Public Const FIELD_GW_B_SHENPIYIJIAN_BLRQ As String = "��������"
        Public Const FIELD_GW_B_SHENPIYIJIAN_BLYJ As String = "�������"
        Public Const FIELD_GW_B_SHENPIYIJIAN_BJNR As String = "�������"
        Public Const FIELD_GW_B_SHENPIYIJIAN_DLR As String = "������"
        Public Const FIELD_GW_B_SHENPIYIJIAN_DLRQ As String = "��������"
        Public Const FIELD_GW_B_SHENPIYIJIAN_BLJG As String = "������"
        Public Const FIELD_GW_B_SHENPIYIJIAN_TXRQ As String = "��д����"
        Public Const FIELD_GW_B_SHENPIYIJIAN_RYXH As String = "��Ա���"
        Public Const FIELD_GW_B_SHENPIYIJIAN_XZJB As String = "��������"
        Public Const FIELD_GW_B_SHENPIYIJIAN_ZZDM As String = "��֯����"

        Public Const FIELD_GW_B_SHENPIYIJIAN_XSXH As String = "��ʾ���"

        'Լ��������Ϣ

        '������_V_ȫ�������ļ�������Ϣ����(��ͼ)
        '������
        Public Const TABLE_GW_V_SHENPIWENJIAN_NEW As String = "����_V_ȫ�������ļ���"
        '�ֶ�����
        Public Const FIELD_GW_V_SHENPIWENJIAN_NEW_WJBS As String = "�ļ���ʶ"
        Public Const FIELD_GW_V_SHENPIWENJIAN_NEW_WJLX As String = "�ļ�����"
        Public Const FIELD_GW_V_SHENPIWENJIAN_NEW_BLLX As String = "��������"
        Public Const FIELD_GW_V_SHENPIWENJIAN_NEW_WJZL As String = "�ļ�����"
        Public Const FIELD_GW_V_SHENPIWENJIAN_NEW_ZSDW As String = "���͵�λ"
        Public Const FIELD_GW_V_SHENPIWENJIAN_NEW_WJBT As String = "�ļ�����"
        Public Const FIELD_GW_V_SHENPIWENJIAN_NEW_WJZH As String = "�ļ��ֺ�"
        Public Const FIELD_GW_V_SHENPIWENJIAN_NEW_JGDZ As String = "���ش���"
        Public Const FIELD_GW_V_SHENPIWENJIAN_NEW_WJNF As String = "�ļ����"
        Public Const FIELD_GW_V_SHENPIWENJIAN_NEW_WJXH As String = "�ļ����"
        Public Const FIELD_GW_V_SHENPIWENJIAN_NEW_MMDJ As String = "���ܵȼ�"
        Public Const FIELD_GW_V_SHENPIWENJIAN_NEW_JJCD As String = "�����̶�"
        Public Const FIELD_GW_V_SHENPIWENJIAN_NEW_WJND As String = "�ļ����"
        Public Const FIELD_GW_V_SHENPIWENJIAN_NEW_ZBDW As String = "���쵥λ"
        Public Const FIELD_GW_V_SHENPIWENJIAN_NEW_NGR As String = "�����"
        Public Const FIELD_GW_V_SHENPIWENJIAN_NEW_NGRQ As String = "�������"
        Public Const FIELD_GW_V_SHENPIWENJIAN_NEW_BLZT As String = "����״̬"
        Public Const FIELD_GW_V_SHENPIWENJIAN_NEW_LSH As String = "��ˮ��"
        Public Const FIELD_GW_V_SHENPIWENJIAN_NEW_ZTC As String = "�����"
        Public Const FIELD_GW_V_SHENPIWENJIAN_NEW_KSSW As String = "��������"

        '2008-08-12
        Public Const FIELD_GW_V_SHENPIWENJIAN_NEW_QFR As String = "ǩ����"
        Public Const FIELD_GW_V_SHENPIWENJIAN_NEW_QFRQ As String = "ǩ������"
        'Լ��������Ϣ

        '�ļ�������ļ����
        Public Enum enumXGWJLB
            FlowFile = 0    'ָ��ϵͳ�ڹ������ļ�
            FujianFile = 1  'ָ�򸽼�ָ���������ļ�
        End Enum

        '������_V_�����ļ�_����������Ϣ����(����)
        '������
        Public Const TABLE_GW_B_SHENPIWENJIAN_FUJIAN As String = "����_V_�����ļ�_����"
        '�ֶ�����
        Public Const FIELD_GW_B_SHENPIWENJIAN_FUJIAN_LBBS As String = "����ʶ"
        Public Const FIELD_GW_B_SHENPIWENJIAN_FUJIAN_WJBS As String = "�ļ���ʶ"
        Public Const FIELD_GW_B_SHENPIWENJIAN_FUJIAN_WJLX As String = "�ļ�����"
        Public Const FIELD_GW_B_SHENPIWENJIAN_FUJIAN_BLLX As String = "��������"
        Public Const FIELD_GW_B_SHENPIWENJIAN_FUJIAN_WJZL As String = "�ļ�����"
        Public Const FIELD_GW_B_SHENPIWENJIAN_FUJIAN_WJBT As String = "�ļ�����"
        Public Const FIELD_GW_B_SHENPIWENJIAN_FUJIAN_ZSDW As String = "���͵�λ"
        Public Const FIELD_GW_B_SHENPIWENJIAN_FUJIAN_JGDZ As String = "���ش���"
        Public Const FIELD_GW_B_SHENPIWENJIAN_FUJIAN_WJNF As String = "�ļ����"
        Public Const FIELD_GW_B_SHENPIWENJIAN_FUJIAN_WJXH As String = "�ļ����"
        Public Const FIELD_GW_B_SHENPIWENJIAN_FUJIAN_WJND As String = "�ļ����"
        Public Const FIELD_GW_B_SHENPIWENJIAN_FUJIAN_ZBDW As String = "���쵥λ"
        Public Const FIELD_GW_B_SHENPIWENJIAN_FUJIAN_NGR As String = "�����"
        Public Const FIELD_GW_B_SHENPIWENJIAN_FUJIAN_NGRQ As String = "�������"
        Public Const FIELD_GW_B_SHENPIWENJIAN_FUJIAN_BLZT As String = "����״̬"
        Public Const FIELD_GW_B_SHENPIWENJIAN_FUJIAN_LSH As String = "��ˮ��"
        Public Const FIELD_GW_B_SHENPIWENJIAN_FUJIAN_ZTC As String = "�����"
        Public Const FIELD_GW_B_SHENPIWENJIAN_FUJIAN_KSSW As String = "��������"
        Public Const FIELD_GW_B_SHENPIWENJIAN_FUJIAN_FJXH As String = "���"
        Public Const FIELD_GW_B_SHENPIWENJIAN_FUJIAN_FJYS As String = "ҳ��"
        Public Const FIELD_GW_B_SHENPIWENJIAN_FUJIAN_FJWZ As String = "λ��"
        '������Ϣ(��ʾ/�༭ʱ��)
        Public Const FIELD_GW_B_SHENPIWENJIAN_FUJIAN_XSXH As String = "��ʾ���"
        Public Const FIELD_GW_B_SHENPIWENJIAN_FUJIAN_BDWJ As String = "�����ļ�"  '���غ���ļ�λ��
        Public Const FIELD_GW_B_SHENPIWENJIAN_FUJIAN_XZBZ As String = "���ر�־"  '�Ƿ�����?
        'Լ��������Ϣ

        '������_B_�ļ��������������Ϣ����
        '������
        Public Const TABLE_GW_B_VT_WENJIANFASONG As String = "����_B_�ļ����������"
        '�ֶ�����
        Public Const FIELD_GW_B_VT_WENJIANFASONG_JSR As String = "������"
        Public Const FIELD_GW_B_VT_WENJIANFASONG_BLSY As String = "��������"
        Public Const FIELD_GW_B_VT_WENJIANFASONG_BLQX As String = "��������"
        Public Const FIELD_GW_B_VT_WENJIANFASONG_FSR As String = "������"
        Public Const FIELD_GW_B_VT_WENJIANFASONG_FSRQ As String = "��������"
        Public Const FIELD_GW_B_VT_WENJIANFASONG_WJZT As String = "�ļ�����"
        Public Const FIELD_GW_B_VT_WENJIANFASONG_WJZZFS As String = "ֽ���ļ�����"
        Public Const FIELD_GW_B_VT_WENJIANFASONG_WJDZFS As String = "�����ļ�����"
        Public Const FIELD_GW_B_VT_WENJIANFASONG_FJZT As String = "��������"
        Public Const FIELD_GW_B_VT_WENJIANFASONG_FJZZFS As String = "ֽ�ʸ�������"
        Public Const FIELD_GW_B_VT_WENJIANFASONG_FJDZFS As String = "���Ӹ�������"
        Public Const FIELD_GW_B_VT_WENJIANFASONG_SYJB As String = "���˼���"
        Public Const FIELD_GW_B_VT_WENJIANFASONG_XB As String = "Э��"
        Public Const FIELD_GW_B_VT_WENJIANFASONG_WTR As String = "ί����"
        'Լ��������Ϣ

        '������_B_�ļ��������������Ϣ����
        '������
        Public Const TABLE_GW_B_VT_WENJIANJIESHOU As String = "����_B_�ļ����������"
        '�ֶ�����
        Public Const FIELD_GW_B_VT_WENJIANJIESHOU_FSR As String = "������"
        Public Const FIELD_GW_B_VT_WENJIANJIESHOU_FSRQ As String = "��������"
        Public Const FIELD_GW_B_VT_WENJIANJIESHOU_BLSY As String = "��������"
        Public Const FIELD_GW_B_VT_WENJIANJIESHOU_JSRQ As String = "��������"
        Public Const FIELD_GW_B_VT_WENJIANJIESHOU_FSWJZZFS As String = "����ֽ���ļ�����"
        Public Const FIELD_GW_B_VT_WENJIANJIESHOU_FSWJDZFS As String = "���������ļ�����"
        Public Const FIELD_GW_B_VT_WENJIANJIESHOU_FSFJZZFS As String = "����ֽ�ʸ�������"
        Public Const FIELD_GW_B_VT_WENJIANJIESHOU_FSFJDZFS As String = "�������Ӹ�������"
        Public Const FIELD_GW_B_VT_WENJIANJIESHOU_JSWJZZFS As String = "����ֽ���ļ�����"
        Public Const FIELD_GW_B_VT_WENJIANJIESHOU_JSWJDZFS As String = "���յ����ļ�����"
        Public Const FIELD_GW_B_VT_WENJIANJIESHOU_JSFJZZFS As String = "����ֽ�ʸ�������"
        Public Const FIELD_GW_B_VT_WENJIANJIESHOU_JSFJDZFS As String = "���յ��Ӹ�������"
        Public Const FIELD_GW_B_VT_WENJIANJIESHOU_JJXH As String = "�������"
        Public Const FIELD_GW_B_VT_WENJIANJIESHOU_FSXH As String = "�������"
        Public Const FIELD_GW_B_VT_WENJIANJIESHOU_YJJH As String = "ԭ���Ӻ�"
        Public Const FIELD_GW_B_VT_WENJIANJIESHOU_FSRBLSY As String = "�����˰�������"
        Public Const FIELD_GW_B_VT_WENJIANJIESHOU_JJBS As String = "���ӱ�ʶ"
        Public Const FIELD_GW_B_VT_WENJIANJIESHOU_XB As String = "Э��"
        Public Const FIELD_GW_B_VT_WENJIANJIESHOU_FSRXB As String = "������Э��"
        'Լ��������Ϣ

        '������_B_�ļ��ջ����������Ϣ����
        '������
        Public Const TABLE_GW_B_VT_WENJIANSHOUHUI As String = "����_B_�ļ��ջ������"
        '�ֶ�����
        Public Const FIELD_GW_B_VT_WENJIANSHOUHUI_JSR As String = "������"
        Public Const FIELD_GW_B_VT_WENJIANSHOUHUI_BLSY As String = "��������"
        Public Const FIELD_GW_B_VT_WENJIANSHOUHUI_FSRQ As String = "��������"
        Public Const FIELD_GW_B_VT_WENJIANSHOUHUI_FSWJZZFS As String = "����ֽ���ļ�����"
        Public Const FIELD_GW_B_VT_WENJIANSHOUHUI_FSWJDZFS As String = "���͵����ļ�����"
        Public Const FIELD_GW_B_VT_WENJIANSHOUHUI_FSFJZZFS As String = "����ֽ�ʸ�������"
        Public Const FIELD_GW_B_VT_WENJIANSHOUHUI_FSFJDZFS As String = "���͵��Ӹ�������"
        Public Const FIELD_GW_B_VT_WENJIANSHOUHUI_JSRQ As String = "��������"
        Public Const FIELD_GW_B_VT_WENJIANSHOUHUI_JSWJZZFS As String = "����ֽ���ļ�����"
        Public Const FIELD_GW_B_VT_WENJIANSHOUHUI_JSWJDZFS As String = "���յ����ļ�����"
        Public Const FIELD_GW_B_VT_WENJIANSHOUHUI_JSFJZZFS As String = "����ֽ�ʸ�������"
        Public Const FIELD_GW_B_VT_WENJIANSHOUHUI_JSFJDZFS As String = "���յ��Ӹ�������"
        Public Const FIELD_GW_B_VT_WENJIANSHOUHUI_JJXH As String = "�������"
        Public Const FIELD_GW_B_VT_WENJIANSHOUHUI_FSXH As String = "�������"
        Public Const FIELD_GW_B_VT_WENJIANSHOUHUI_YJJH As String = "ԭ���Ӻ�"
        Public Const FIELD_GW_B_VT_WENJIANSHOUHUI_JJBS As String = "���ӱ�ʶ"
        Public Const FIELD_GW_B_VT_WENJIANSHOUHUI_FSR As String = "������"
        Public Const FIELD_GW_B_VT_WENJIANSHOUHUI_XB As String = "Э��"
        Public Const FIELD_GW_B_VT_WENJIANSHOUHUI_SFDG As String = "�Ƿ����"
        Public Const FIELD_GW_B_VT_WENJIANSHOUHUI_FSRBLSY As String = "�����˰�������"
        Public Const FIELD_GW_B_VT_WENJIANSHOUHUI_FSRXB As String = "������Э��"
        'Լ��������Ϣ

        '������_B_�ļ��˻����������Ϣ����
        '������
        Public Const TABLE_GW_B_VT_WENJIANTUIHUI As String = "����_B_�ļ��˻������"
        '�ֶ�����
        Public Const FIELD_GW_B_VT_WENJIANTUIHUI_FSR As String = "������"
        Public Const FIELD_GW_B_VT_WENJIANTUIHUI_FSRQ As String = "��������"
        Public Const FIELD_GW_B_VT_WENJIANTUIHUI_BLSY As String = "��������"
        Public Const FIELD_GW_B_VT_WENJIANTUIHUI_JSRQ As String = "��������"
        Public Const FIELD_GW_B_VT_WENJIANTUIHUI_FSWJZZFS As String = "����ֽ���ļ�����"
        Public Const FIELD_GW_B_VT_WENJIANTUIHUI_FSWJDZFS As String = "���������ļ�����"
        Public Const FIELD_GW_B_VT_WENJIANTUIHUI_FSFJZZFS As String = "����ֽ�ʸ�������"
        Public Const FIELD_GW_B_VT_WENJIANTUIHUI_FSFJDZFS As String = "�������Ӹ�������"
        Public Const FIELD_GW_B_VT_WENJIANTUIHUI_JSWJZZFS As String = "����ֽ���ļ�����"
        Public Const FIELD_GW_B_VT_WENJIANTUIHUI_JSWJDZFS As String = "���յ����ļ�����"
        Public Const FIELD_GW_B_VT_WENJIANTUIHUI_JSFJZZFS As String = "����ֽ�ʸ�������"
        Public Const FIELD_GW_B_VT_WENJIANTUIHUI_JSFJDZFS As String = "���յ��Ӹ�������"
        Public Const FIELD_GW_B_VT_WENJIANTUIHUI_JJXH As String = "�������"
        Public Const FIELD_GW_B_VT_WENJIANTUIHUI_FSXH As String = "�������"
        Public Const FIELD_GW_B_VT_WENJIANTUIHUI_YJJH As String = "ԭ���Ӻ�"
        Public Const FIELD_GW_B_VT_WENJIANTUIHUI_FSRBLSY As String = "�����˰�������"
        Public Const FIELD_GW_B_VT_WENJIANTUIHUI_JJBS As String = "���ӱ�ʶ"
        Public Const FIELD_GW_B_VT_WENJIANTUIHUI_XB As String = "Э��"
        Public Const FIELD_GW_B_VT_WENJIANTUIHUI_FSRXB As String = "������Э��"
        'Լ��������Ϣ

        '������_B_�ļ��������������Ϣ����
        '������
        Public Const TABLE_GW_B_VT_WENJIANBUYUE As String = "����_B_�ļ����������"
        '�ֶ�����
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_WJBS As String = "�ļ���ʶ"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_JJXH As String = "�������"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_YJJH As String = "ԭ���Ӻ�"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_FSXH As String = "�������"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_FSR As String = "������"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_FSRQ As String = "��������"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_FSZZWJ As String = "����ֽ���ļ�"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_FSDZWJ As String = "���͵����ļ�"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_FSZZFJ As String = "����ֽ�ʸ���"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_FSDZFJ As String = "���͵��Ӹ���"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_JSXH As String = "�������"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_JSR As String = "������"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_XB As String = "Э��"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_JSRQ As String = "��������"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_JSZZWJ As String = "����ֽ���ļ�"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_JSDZWJ As String = "���յ����ļ�"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_JSZZFJ As String = "����ֽ�ʸ���"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_JSDZFJ As String = "���յ��Ӹ���"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_BLZHQX As String = "�����������"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_WCRQ As String = "�������"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_WTR As String = "ί����"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_BLLX As String = "��������"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_BLZL As String = "��������"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_BLZT As String = "����״̬"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_JJBS As String = "���ӱ�ʶ"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_SFDG As String = "�Ƿ����"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_JJSM As String = "����˵��"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_BWTX As String = "��������"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_BLQK As String = "�������"
        'Լ��������Ϣ

        '������_B_VT_�а�������������Ϣ����
        '�������ڣ���д������������
        '��    ע������_B_���ӱ��еİ��������п����빫��_B_�а���еİ��������¼�ò�һ��.
        '          ����_B_�а���еİ��������Ǿ���������
        '������
        Public Const TABLE_GW_B_VT_CHENGBANQINGKUANG As String = "����_B_VT_�а���������"
        '�ֶ�����
        Public Const FIELD_GW_B_VT_CHENGBANQINGKUANG_WJBS As String = "�ļ���ʶ"
        Public Const FIELD_GW_B_VT_CHENGBANQINGKUANG_JJXH As String = "�������"
        Public Const FIELD_GW_B_VT_CHENGBANQINGKUANG_BLXH As String = "�������"
        Public Const FIELD_GW_B_VT_CHENGBANQINGKUANG_BLLX As String = "��������"
        Public Const FIELD_GW_B_VT_CHENGBANQINGKUANG_BLZL As String = "��������"
        Public Const FIELD_GW_B_VT_CHENGBANQINGKUANG_BLRQ As String = "��������"
        Public Const FIELD_GW_B_VT_CHENGBANQINGKUANG_BLJG As String = "������"
        Public Const FIELD_GW_B_VT_CHENGBANQINGKUANG_BLRY As String = "������"
        Public Const FIELD_GW_B_VT_CHENGBANQINGKUANG_XBBZ As String = "Э��"
        'Լ��������Ϣ

        '������_V_ȫ�������¡�����Ϣ����(��ͼ)
        '������
        Public Const TABLE_GW_V_QUANBUGONGWEN As String = "����_V_ȫ��������"
        '�ֶ�����
        Public Const FIELD_GW_V_QUANBUGONGWEN_WJBS As String = "�ļ���ʶ"
        Public Const FIELD_GW_V_QUANBUGONGWEN_WJLX As String = "�ļ�����"
        Public Const FIELD_GW_V_QUANBUGONGWEN_BLLX As String = "��������"
        Public Const FIELD_GW_V_QUANBUGONGWEN_WJZL As String = "�ļ�����"
        Public Const FIELD_GW_V_QUANBUGONGWEN_ZSDW As String = "���͵�λ"
        Public Const FIELD_GW_V_QUANBUGONGWEN_WJBT As String = "�ļ�����"
        Public Const FIELD_GW_V_QUANBUGONGWEN_WJZH As String = "�ļ��ֺ�"
        Public Const FIELD_GW_V_QUANBUGONGWEN_JGDZ As String = "���ش���"
        Public Const FIELD_GW_V_QUANBUGONGWEN_WJNF As String = "�ļ����"
        Public Const FIELD_GW_V_QUANBUGONGWEN_WJXH As String = "�ļ����"
        Public Const FIELD_GW_V_QUANBUGONGWEN_MMDJ As String = "���ܵȼ�"
        Public Const FIELD_GW_V_QUANBUGONGWEN_JJCD As String = "�����̶�"
        Public Const FIELD_GW_V_QUANBUGONGWEN_WJND As String = "�ļ����"
        Public Const FIELD_GW_V_QUANBUGONGWEN_ZBDW As String = "���쵥λ"
        Public Const FIELD_GW_V_QUANBUGONGWEN_NGR As String = "�����"
        Public Const FIELD_GW_V_QUANBUGONGWEN_NGRQ As String = "�������"
        Public Const FIELD_GW_V_QUANBUGONGWEN_BLZT As String = "����״̬"
        Public Const FIELD_GW_V_QUANBUGONGWEN_LSH As String = "��ˮ��"
        Public Const FIELD_GW_V_QUANBUGONGWEN_ZTC As String = "�����"
        Public Const FIELD_GW_V_QUANBUGONGWEN_KSSW As String = "��������"
        Public Const FIELD_GW_V_QUANBUGONGWEN_WJRQ As String = "�ļ�����"
        Public Const FIELD_GW_V_QUANBUGONGWEN_FSRQ As String = "��������"
        Public Const FIELD_GW_V_QUANBUGONGWEN_BWTX As String = "��������"


        '������_V_ȫ�����鹤��������Ϣ����(��ͼ)
        '������
        Public Const TABLE_GW_V_DUCHAGONGZUO As String = "����_V_ȫ�����鹤��"
        '�ֶ�����
        Public Const FIELD_GW_V_DUCHAGONGZUO_WJBS As String = "�ļ���ʶ"
        Public Const FIELD_GW_V_DUCHAGONGZUO_LXBS As String = "�����ʶ"
        Public Const FIELD_GW_V_DUCHAGONGZUO_BLBS As String = "�����ʶ"
        Public Const FIELD_GW_V_DUCHAGONGZUO_BJBS As String = "����ʶ"
        Public Const FIELD_GW_V_DUCHAGONGZUO_LSH As String = "��ˮ��"
        Public Const FIELD_GW_V_DUCHAGONGZUO_BLLX As String = "��������"
        Public Const FIELD_GW_V_DUCHAGONGZUO_WJZL As String = "�ļ�����"
        Public Const FIELD_GW_V_DUCHAGONGZUO_RWLB As String = "�������"
        Public Const FIELD_GW_V_DUCHAGONGZUO_SCJD As String = "�����׶�"
        Public Const FIELD_GW_V_DUCHAGONGZUO_BLZT As String = "����״̬"
        Public Const FIELD_GW_V_DUCHAGONGZUO_MMDJ As String = "���ܵȼ�"
        Public Const FIELD_GW_V_DUCHAGONGZUO_JJCD As String = "�����̶�"
        Public Const FIELD_GW_V_DUCHAGONGZUO_XMBT As String = "��Ŀ����"
        Public Const FIELD_GW_V_DUCHAGONGZUO_DCBH As String = "������"
        Public Const FIELD_GW_V_DUCHAGONGZUO_DCWH As String = "�����ĺ�"
        Public Const FIELD_GW_V_DUCHAGONGZUO_DCLX As String = "��������"

        Public Const FIELD_GW_V_DUCHAGONGZUO_DCR As String = "������"
        Public Const FIELD_GW_V_DUCHAGONGZUO_BLSX As String = "����ʱ��"
        Public Const FIELD_GW_V_DUCHAGONGZUO_CBDW As String = "�а쵥λ"
        Public Const FIELD_GW_V_DUCHAGONGZUO_CBR As String = "�а���"
        Public Const FIELD_GW_V_DUCHAGONGZUO_XBDW As String = "Э�쵥λ"
        Public Const FIELD_GW_V_DUCHAGONGZUO_XBR As String = "Э����"

        Public Const FIELD_GW_V_DUCHAGONGZUO_LXPZR As String = "������׼��"
        Public Const FIELD_GW_V_DUCHAGONGZUO_LXPZRQ As String = "������׼����"
        Public Const FIELD_GW_V_DUCHAGONGZUO_BJPZR As String = "�����׼��"
        Public Const FIELD_GW_V_DUCHAGONGZUO_BJPZRQ As String = "�����׼����"
        Public Const FIELD_GW_V_DUCHAGONGZUO_LXDW As String = "���λ"
        Public Const FIELD_GW_V_DUCHAGONGZUO_LXR As String = "������"
        Public Const FIELD_GW_V_DUCHAGONGZUO_LXRQ As String = "��������"

        '�µĹ����������鵥
        Public Const FIELD_GW_V_DUCHAGONGZUO_PZR As String = "��׼��"
        Public Const FIELD_GW_V_DUCHAGONGZUO_PZRQ As String = "��׼����"

        '��ʾ�ֶ�
        Public Const FIELD_GW_V_DUCHAGONGZUO_BWTX As String = "��������"


        '������_V_�ƽ��ļ����������Ϣ����
        '������
        Public Const TABLE_GW_V_YIJIAOWENJIAN As String = "����_V_�ƽ��ļ�"
        '�ֶ�����
        Public Const FIELD_GW_V_YIJIAOWENJIAN_WJBS As String = "�ļ���ʶ"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_YJRY As String = "�ƽ���"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_YJRQ As String = "�ƽ�����"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_YJSM As String = "�ƽ�˵��"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_JSRY As String = "������"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_JSRQ As String = "��������"
        '��ʾ�ֶ�
        Public Const FIELD_GW_V_YIJIAOWENJIAN_SFYJ As String = "�Ƿ��ƽ�"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_SFJS As String = "�Ƿ����"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_WJLX As String = "�ļ�����"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_BLLX As String = "��������"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_WJZL As String = "�ļ�����"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_ZSDW As String = "���͵�λ"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_WJBT As String = "�ļ�����"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_WJZH As String = "�ļ��ֺ�"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_JGDZ As String = "���ش���"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_WJNF As String = "�ļ����"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_WJXH As String = "�ļ����"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_MMDJ As String = "���ܵȼ�"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_JJCD As String = "�����̶�"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_WJND As String = "�ļ����"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_ZBDW As String = "���쵥λ"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_NGR As String = "�����"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_NGRQ As String = "�������"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_BLZT As String = "����״̬"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_LSH As String = "��ˮ��"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_ZTC As String = "�����"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_KSSW As String = "��������"






        '�����ʼ��������enum
        Public Enum enumTableType
            '***********************************
            GW_B_JIAOJIE = 1
            GW_B_BANLI = 2
            GW_B_CUIBAN = 3
            GW_B_DUBAN = 4
            GW_B_CAOZUORIZHI = 5
            GW_B_FUJIAN = 6
            GW_B_XIANGGUANWENJIAN = 7
            GW_B_XIANGGUANWENJIANFUJIAN = 8
            '***********************************
            GW_B_CUIBAN_JIAOJIE = 9
            GW_B_DUBAN_JIAOJIE = 10
            '***********************************
            GW_B_SHENPIYIJIAN = 11
            GW_B_SHENPIWENJIAN_FUJIAN = 12
            '***********************************
            GW_V_SHENPIWENJIAN_NEW = 13
            '***********************************
            GW_B_VT_WENJIANFASONG = 14
            '***********************************
            GW_B_VT_WENJIANJIESHOU = 15
            '***********************************
            GW_B_VT_WENJIANSHOUHUI = 16
            '***********************************
            GW_B_VT_WENJIANTUIHUI = 17
            '***********************************
            GW_B_VT_WENJIANBUYUE = 18
            '***********************************
            GW_B_VT_CHENGBANQINGKUANG = 19
            '***********************************
            GW_V_QUANBUGONGWEN = 20
            '***********************************
            GW_V_DUCHAGONGZUO = 21
            '***********************************
            GW_V_YIJIAOWENJIAN = 22

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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.Common.Data.FlowData)
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
                Case enumTableType.GW_B_JIAOJIE
                    table = createDataTables_Jiaojie(strErrMsg)
                Case enumTableType.GW_B_BANLI
                    table = createDataTables_Banli(strErrMsg)
                Case enumTableType.GW_B_CUIBAN
                    table = createDataTables_Cuiban(strErrMsg)
                Case enumTableType.GW_B_DUBAN
                    table = createDataTables_Duban(strErrMsg)
                Case enumTableType.GW_B_CAOZUORIZHI
                    table = createDataTables_Caozuorizhi(strErrMsg)
                Case enumTableType.GW_B_FUJIAN
                    table = createDataTables_Fujian(strErrMsg)
                Case enumTableType.GW_B_XIANGGUANWENJIAN
                    table = createDataTables_Xiangguanwenjian(strErrMsg)
                Case enumTableType.GW_B_XIANGGUANWENJIANFUJIAN
                    table = createDataTables_XiangguanwenjianFujian(strErrMsg)

                Case enumTableType.GW_B_CUIBAN_JIAOJIE
                    table = createDataTables_Cuiban_Jiaojie(strErrMsg)
                Case enumTableType.GW_B_DUBAN_JIAOJIE
                    table = createDataTables_Duban_Jiaojie(strErrMsg)

                Case enumTableType.GW_B_SHENPIYIJIAN
                    table = createDataTables_Shenpiyijian(strErrMsg)
                Case enumTableType.GW_V_SHENPIWENJIAN_NEW
                    table = createDataTables_Shenpiwenjian(strErrMsg)
                Case enumTableType.GW_B_SHENPIWENJIAN_FUJIAN
                    table = createDataTables_Shenpiwenjian_Fujian(strErrMsg)

                Case enumTableType.GW_B_VT_WENJIANFASONG
                    table = createDataTables_VT_Wenjianfasong(strErrMsg)

                Case enumTableType.GW_B_VT_WENJIANJIESHOU
                    table = createDataTables_VT_Wenjianjieshou(strErrMsg)

                Case enumTableType.GW_B_VT_WENJIANSHOUHUI
                    table = createDataTables_VT_Wenjianshouhui(strErrMsg)

                Case enumTableType.GW_B_VT_WENJIANTUIHUI
                    table = createDataTables_VT_Wenjiantuihui(strErrMsg)

                Case enumTableType.GW_B_VT_WENJIANBUYUE
                    table = createDataTables_Buyue(strErrMsg)

                Case enumTableType.GW_B_VT_CHENGBANQINGKUANG
                    table = createDataTables_VT_Chengbanqingkuang(strErrMsg)

                Case enumTableType.GW_V_QUANBUGONGWEN
                    table = createDataTables_QuanbuGongwen(strErrMsg)


                Case enumTableType.GW_V_DUCHAGONGZUO
                    table = createDataTables_Duchagongzuo(strErrMsg)



                Case enumTableType.GW_V_YIJIAOWENJIAN
                    table = createDataTables_YijiaoWenjian(strErrMsg)



                Case Else
                    strErrMsg = "��Ч�ı����ͣ�"
                    table = Nothing
            End Select

            createDataTables = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GW_B_JIAOJIE
        '----------------------------------------------------------------
        Private Function createDataTables_Jiaojie(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GW_B_JIAOJIE)
                With table.Columns
                    .Add(FIELD_GW_B_JIAOJIE_WJBS, GetType(System.String))
                    .Add(FIELD_GW_B_JIAOJIE_JJXH, GetType(System.Int32))
                    .Add(FIELD_GW_B_JIAOJIE_YJJH, GetType(System.Int32))

                    .Add(FIELD_GW_B_JIAOJIE_FSXH, GetType(System.Int32))
                    .Add(FIELD_GW_B_JIAOJIE_FSR, GetType(System.String))
                    .Add(FIELD_GW_B_JIAOJIE_FSRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_B_JIAOJIE_FSZZWJ, GetType(System.Int32))
                    .Add(FIELD_GW_B_JIAOJIE_FSDZWJ, GetType(System.Int32))
                    .Add(FIELD_GW_B_JIAOJIE_FSZZFJ, GetType(System.Int32))
                    .Add(FIELD_GW_B_JIAOJIE_FSDZFJ, GetType(System.Int32))

                    .Add(FIELD_GW_B_JIAOJIE_JSXH, GetType(System.Int32))
                    .Add(FIELD_GW_B_JIAOJIE_JSR, GetType(System.String))
                    .Add(FIELD_GW_B_JIAOJIE_XB, GetType(System.String))
                    .Add(FIELD_GW_B_JIAOJIE_JSRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_B_JIAOJIE_JSZZWJ, GetType(System.Int32))
                    .Add(FIELD_GW_B_JIAOJIE_JSDZWJ, GetType(System.Int32))
                    .Add(FIELD_GW_B_JIAOJIE_JSZZFJ, GetType(System.Int32))
                    .Add(FIELD_GW_B_JIAOJIE_JSDZFJ, GetType(System.Int32))
                    .Add(FIELD_GW_B_JIAOJIE_BLZHQX, GetType(System.DateTime))
                    .Add(FIELD_GW_B_JIAOJIE_WCRQ, GetType(System.DateTime))

                    .Add(FIELD_GW_B_JIAOJIE_WTR, GetType(System.String))
                    .Add(FIELD_GW_B_JIAOJIE_BLLX, GetType(System.String))
                    .Add(FIELD_GW_B_JIAOJIE_BLZL, GetType(System.String))
                    .Add(FIELD_GW_B_JIAOJIE_BLZT, GetType(System.String))
                    .Add(FIELD_GW_B_JIAOJIE_JJBS, GetType(System.String))
                    .Add(FIELD_GW_B_JIAOJIE_SFDG, GetType(System.Int32))
                    .Add(FIELD_GW_B_JIAOJIE_JJSM, GetType(System.String))
                    .Add(FIELD_GW_B_JIAOJIE_BWTX, GetType(System.Int32))

                    .Add(FIELD_GW_B_JIAOJIE_JJBZ, GetType(System.String))

                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Jiaojie = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GW_B_BANLI
        '----------------------------------------------------------------
        Private Function createDataTables_Banli(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GW_B_BANLI)
                With table.Columns
                    .Add(FIELD_GW_B_BANLI_WJBS, GetType(System.String))
                    .Add(FIELD_GW_B_BANLI_JJXH, GetType(System.Int32))

                    .Add(FIELD_GW_B_BANLI_BLR, GetType(System.String))
                    .Add(FIELD_GW_B_BANLI_BLLX, GetType(System.String))
                    .Add(FIELD_GW_B_BANLI_BLZL, GetType(System.String))

                    .Add(FIELD_GW_B_BANLI_XSXH, GetType(System.Int32))


                    .Add(FIELD_GW_B_BANLI_BLRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_B_BANLI_SFPZ, GetType(System.String))
                    .Add(FIELD_GW_B_BANLI_BLYJ, GetType(System.String))
                    .Add(FIELD_GW_B_BANLI_BJNR, GetType(System.String))

                    .Add(FIELD_GW_B_BANLI_DLR, GetType(System.String))
                    .Add(FIELD_GW_B_BANLI_DLRQ, GetType(System.DateTime))

                    .Add(FIELD_GW_B_BANLI_BLJG, GetType(System.String))
                    .Add(FIELD_GW_B_BANLI_TXRQ, GetType(System.DateTime))

                    .Add(FIELD_GW_B_BANLI_XZYDRY, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Banli = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GW_B_CUIBAN
        '----------------------------------------------------------------
        Private Function createDataTables_Cuiban(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GW_B_CUIBAN)
                With table.Columns
                    .Add(FIELD_GW_B_CUIBAN_WJBS, GetType(System.String))
                    .Add(FIELD_GW_B_CUIBAN_JJXH, GetType(System.Int32))
                    .Add(FIELD_GW_B_CUIBAN_CBXH, GetType(System.Int32))

                    .Add(FIELD_GW_B_CUIBAN_CBR, GetType(System.String))
                    .Add(FIELD_GW_B_CUIBAN_CBRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_B_CUIBAN_CBSM, GetType(System.String))
                    .Add(FIELD_GW_B_CUIBAN_BCBR, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Cuiban = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GW_B_DUBAN
        '----------------------------------------------------------------
        Private Function createDataTables_Duban(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GW_B_DUBAN)
                With table.Columns
                    .Add(FIELD_GW_B_DUBAN_WJBS, GetType(System.String))
                    .Add(FIELD_GW_B_DUBAN_JJXH, GetType(System.Int32))
                    .Add(FIELD_GW_B_DUBAN_DBXH, GetType(System.Int32))

                    .Add(FIELD_GW_B_DUBAN_DBR, GetType(System.String))
                    .Add(FIELD_GW_B_DUBAN_DBRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_B_DUBAN_DBYQ, GetType(System.String))
                    .Add(FIELD_GW_B_DUBAN_BDBR, GetType(System.String))

                    .Add(FIELD_GW_B_DUBAN_DBJG, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Duban = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GW_B_CAOZUORIZHI
        '----------------------------------------------------------------
        Private Function createDataTables_Caozuorizhi(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GW_B_CAOZUORIZHI)
                With table.Columns
                    .Add(FIELD_GW_B_CAOZUORIZHI_WJBS, GetType(System.String))
                    .Add(FIELD_GW_B_CAOZUORIZHI_CZXH, GetType(System.Int32))

                    .Add(FIELD_GW_B_CAOZUORIZHI_CZR, GetType(System.String))
                    .Add(FIELD_GW_B_CAOZUORIZHI_CZSJ, GetType(System.DateTime))
                    .Add(FIELD_GW_B_CAOZUORIZHI_CZSM, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Caozuorizhi = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GW_B_FUJIAN
        '----------------------------------------------------------------
        Private Function createDataTables_Fujian(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GW_B_FUJIAN)
                With table.Columns
                    .Add(FIELD_GW_B_FUJIAN_WJBS, GetType(System.String))
                    .Add(FIELD_GW_B_FUJIAN_WJXH, GetType(System.Int32))

                    .Add(FIELD_GW_B_FUJIAN_WJSM, GetType(System.String))
                    .Add(FIELD_GW_B_FUJIAN_WJYS, GetType(System.Int32))
                    .Add(FIELD_GW_B_FUJIAN_WJWZ, GetType(System.String))

                    .Add(FIELD_GW_B_FUJIAN_XSXH, GetType(System.Int32))
                    .Add(FIELD_GW_B_FUJIAN_BDWJ, GetType(System.String))
                    .Add(FIELD_GW_B_FUJIAN_XZBZ, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Fujian = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GW_B_XIANGGUANWENJIAN
        '----------------------------------------------------------------
        Private Function createDataTables_Xiangguanwenjian(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GW_B_XIANGGUANWENJIAN)
                With table.Columns
                    .Add(FIELD_GW_B_XIANGGUANWENJIAN_WJXH, GetType(System.Int32))
                    .Add(FIELD_GW_B_XIANGGUANWENJIAN_NBXH, GetType(System.Int32))

                    .Add(FIELD_GW_B_XIANGGUANWENJIAN_DQWJBS, GetType(System.String))
                    .Add(FIELD_GW_B_XIANGGUANWENJIAN_DCWJBS, GetType(System.String))
                    .Add(FIELD_GW_B_XIANGGUANWENJIAN_SJWJBS, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Xiangguanwenjian = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GW_B_XIANGGUANWENJIANFUJIAN
        '----------------------------------------------------------------
        Private Function createDataTables_XiangguanwenjianFujian(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GW_B_XIANGGUANWENJIANFUJIAN)
                With table.Columns
                    .Add(FIELD_GW_B_XIANGGUANWENJIANFUJIAN_WJBS, GetType(System.String))
                    .Add(FIELD_GW_B_XIANGGUANWENJIANFUJIAN_WJXH, GetType(System.Int32))

                    .Add(FIELD_GW_B_XIANGGUANWENJIANFUJIAN_WJSM, GetType(System.String))
                    .Add(FIELD_GW_B_XIANGGUANWENJIANFUJIAN_WJYS, GetType(System.Int32))
                    .Add(FIELD_GW_B_XIANGGUANWENJIANFUJIAN_WJWZ, GetType(System.String))

                    .Add(FIELD_GW_B_XIANGGUANWENJIANFUJIAN_XSXH, GetType(System.Int32))
                    .Add(FIELD_GW_B_XIANGGUANWENJIANFUJIAN_BDWJ, GetType(System.String))
                    .Add(FIELD_GW_B_XIANGGUANWENJIANFUJIAN_XZBZ, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_XiangguanwenjianFujian = table

        End Function



        '----------------------------------------------------------------
        '����TABLE_GW_B_CUIBAN_JIAOJIE
        '----------------------------------------------------------------
        Private Function createDataTables_Cuiban_Jiaojie(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GW_B_CUIBAN_JIAOJIE)
                With table.Columns
                    .Add(FIELD_GW_B_CUIBAN_WJBS, GetType(System.String))
                    .Add(FIELD_GW_B_CUIBAN_JJXH, GetType(System.Int32))
                    .Add(FIELD_GW_B_CUIBAN_CBXH, GetType(System.Int32))

                    .Add(FIELD_GW_B_CUIBAN_CBR, GetType(System.String))
                    .Add(FIELD_GW_B_CUIBAN_CBRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_B_CUIBAN_CBSM, GetType(System.String))
                    .Add(FIELD_GW_B_CUIBAN_BCBR, GetType(System.String))

                    '���ӱ���Ϣ
                    .Add(FIELD_GW_B_JIAOJIE_BLZL, GetType(System.String))
                    .Add(FIELD_GW_B_JIAOJIE_BLZT, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Cuiban_Jiaojie = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GW_B_DUBAN_JIAOJIE
        '----------------------------------------------------------------
        Private Function createDataTables_Duban_Jiaojie(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GW_B_DUBAN_JIAOJIE)
                With table.Columns
                    .Add(FIELD_GW_B_DUBAN_WJBS, GetType(System.String))
                    .Add(FIELD_GW_B_DUBAN_JJXH, GetType(System.Int32))
                    .Add(FIELD_GW_B_DUBAN_DBXH, GetType(System.Int32))

                    .Add(FIELD_GW_B_DUBAN_DBR, GetType(System.String))
                    .Add(FIELD_GW_B_DUBAN_DBRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_B_DUBAN_DBYQ, GetType(System.String))
                    .Add(FIELD_GW_B_DUBAN_BDBR, GetType(System.String))

                    .Add(FIELD_GW_B_DUBAN_DBJG, GetType(System.String))

                    '���ӱ���Ϣ
                    .Add(FIELD_GW_B_JIAOJIE_BLZL, GetType(System.String))
                    .Add(FIELD_GW_B_JIAOJIE_BLZT, GetType(System.String))

                    .Add(FIELD_GW_B_DUBAN_JIAOJIE_BCJG, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Duban_Jiaojie = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GW_B_SHENPIYIJIAN
        '----------------------------------------------------------------
        Private Function createDataTables_Shenpiyijian(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GW_B_SHENPIYIJIAN)
                With table.Columns
                    .Add(FIELD_GW_B_SHENPIYIJIAN_WJBS, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIYIJIAN_JJXH, GetType(System.Int32))
                    .Add(FIELD_GW_B_SHENPIYIJIAN_BLLX, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIYIJIAN_BLZL, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIYIJIAN_JSR, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIYIJIAN_XB, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIYIJIAN_SFTY, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIYIJIAN_BLRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_B_SHENPIYIJIAN_BLYJ, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIYIJIAN_BJNR, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIYIJIAN_DLR, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIYIJIAN_DLRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_B_SHENPIYIJIAN_BLJG, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIYIJIAN_TXRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_B_SHENPIYIJIAN_RYXH, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIYIJIAN_XZJB, GetType(System.Int32))
                    .Add(FIELD_GW_B_SHENPIYIJIAN_ZZDM, GetType(System.String))

                    .Add(FIELD_GW_B_SHENPIYIJIAN_XSXH, GetType(System.Int32))

                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Shenpiyijian = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GW_V_SHENPIWENJIAN_NEW
        '----------------------------------------------------------------
        Private Function createDataTables_Shenpiwenjian(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GW_V_SHENPIWENJIAN_NEW)
                With table.Columns
                    .Add(FIELD_GW_V_SHENPIWENJIAN_NEW_WJBS, GetType(System.String))
                    .Add(FIELD_GW_V_SHENPIWENJIAN_NEW_BLLX, GetType(System.String))
                    .Add(FIELD_GW_V_SHENPIWENJIAN_NEW_WJZL, GetType(System.String))
                    .Add(FIELD_GW_V_SHENPIWENJIAN_NEW_ZSDW, GetType(System.String))
                    .Add(FIELD_GW_V_SHENPIWENJIAN_NEW_WJBT, GetType(System.String))
                    .Add(FIELD_GW_V_SHENPIWENJIAN_NEW_WJZH, GetType(System.String))
                    .Add(FIELD_GW_V_SHENPIWENJIAN_NEW_JGDZ, GetType(System.String))
                    .Add(FIELD_GW_V_SHENPIWENJIAN_NEW_WJNF, GetType(System.String))
                    .Add(FIELD_GW_V_SHENPIWENJIAN_NEW_WJXH, GetType(System.String))
                    .Add(FIELD_GW_V_SHENPIWENJIAN_NEW_MMDJ, GetType(System.String))
                    .Add(FIELD_GW_V_SHENPIWENJIAN_NEW_JJCD, GetType(System.String))
                    .Add(FIELD_GW_V_SHENPIWENJIAN_NEW_WJND, GetType(System.Int32))
                    .Add(FIELD_GW_V_SHENPIWENJIAN_NEW_ZBDW, GetType(System.String))
                    .Add(FIELD_GW_V_SHENPIWENJIAN_NEW_NGR, GetType(System.String))
                    .Add(FIELD_GW_V_SHENPIWENJIAN_NEW_NGRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_V_SHENPIWENJIAN_NEW_BLZT, GetType(System.String))
                    .Add(FIELD_GW_V_SHENPIWENJIAN_NEW_LSH, GetType(System.String))
                    .Add(FIELD_GW_V_SHENPIWENJIAN_NEW_ZTC, GetType(System.String))
                    .Add(FIELD_GW_V_SHENPIWENJIAN_NEW_KSSW, GetType(System.Int32))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Shenpiwenjian = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GW_B_SHENPIWENJIAN_FUJIAN
        '----------------------------------------------------------------
        Private Function createDataTables_Shenpiwenjian_Fujian(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GW_B_SHENPIWENJIAN_FUJIAN)
                With table.Columns
                    .Add(FIELD_GW_B_SHENPIWENJIAN_FUJIAN_LBBS, GetType(System.Int32))

                    .Add(FIELD_GW_B_SHENPIWENJIAN_FUJIAN_WJBS, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIWENJIAN_FUJIAN_WJLX, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIWENJIAN_FUJIAN_BLLX, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIWENJIAN_FUJIAN_WJZL, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIWENJIAN_FUJIAN_ZSDW, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIWENJIAN_FUJIAN_WJBT, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIWENJIAN_FUJIAN_JGDZ, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIWENJIAN_FUJIAN_WJNF, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIWENJIAN_FUJIAN_WJXH, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIWENJIAN_FUJIAN_WJND, GetType(System.Int32))
                    .Add(FIELD_GW_B_SHENPIWENJIAN_FUJIAN_ZBDW, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIWENJIAN_FUJIAN_NGR, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIWENJIAN_FUJIAN_NGRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_B_SHENPIWENJIAN_FUJIAN_BLZT, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIWENJIAN_FUJIAN_LSH, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIWENJIAN_FUJIAN_ZTC, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIWENJIAN_FUJIAN_KSSW, GetType(System.Int32))

                    .Add(FIELD_GW_B_SHENPIWENJIAN_FUJIAN_FJXH, GetType(System.Int32))
                    .Add(FIELD_GW_B_SHENPIWENJIAN_FUJIAN_FJYS, GetType(System.Int32))
                    .Add(FIELD_GW_B_SHENPIWENJIAN_FUJIAN_FJWZ, GetType(System.String))

                    .Add(FIELD_GW_B_SHENPIWENJIAN_FUJIAN_XSXH, GetType(System.Int32))
                    .Add(FIELD_GW_B_SHENPIWENJIAN_FUJIAN_BDWJ, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIWENJIAN_FUJIAN_XZBZ, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Shenpiwenjian_Fujian = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GW_B_VT_WENJIANFASONG
        '----------------------------------------------------------------
        Private Function createDataTables_VT_Wenjianfasong(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GW_B_VT_WENJIANFASONG)
                With table.Columns
                    .Add(FIELD_GW_B_VT_WENJIANFASONG_JSR, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANFASONG_BLSY, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANFASONG_BLQX, GetType(System.DateTime))
                    .Add(FIELD_GW_B_VT_WENJIANFASONG_FSR, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANFASONG_FSRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_B_VT_WENJIANFASONG_WJZT, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANFASONG_WJZZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANFASONG_WJDZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANFASONG_FJZT, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANFASONG_FJZZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANFASONG_FJDZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANFASONG_SYJB, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANFASONG_XB, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANFASONG_WTR, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_VT_Wenjianfasong = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GW_B_VT_WENJIANJIESHOU
        '----------------------------------------------------------------
        Private Function createDataTables_VT_Wenjianjieshou(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GW_B_VT_WENJIANJIESHOU)
                With table.Columns
                    .Add(FIELD_GW_B_VT_WENJIANJIESHOU_FSR, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANJIESHOU_FSRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_B_VT_WENJIANJIESHOU_BLSY, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANJIESHOU_JSRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_B_VT_WENJIANJIESHOU_FSWJZZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANJIESHOU_FSWJDZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANJIESHOU_FSFJZZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANJIESHOU_FSFJDZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANJIESHOU_JSWJZZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANJIESHOU_JSWJDZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANJIESHOU_JSFJZZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANJIESHOU_JSFJDZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANJIESHOU_JJXH, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANJIESHOU_FSXH, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANJIESHOU_YJJH, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANJIESHOU_FSRBLSY, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANJIESHOU_JJBS, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANJIESHOU_XB, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANJIESHOU_FSRXB, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_VT_Wenjianjieshou = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GW_B_VT_WENJIANSHOUHUI
        '----------------------------------------------------------------
        Private Function createDataTables_VT_Wenjianshouhui(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GW_B_VT_WENJIANSHOUHUI)
                With table.Columns
                    .Add(FIELD_GW_B_VT_WENJIANSHOUHUI_JSR, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANSHOUHUI_BLSY, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANSHOUHUI_FSRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_B_VT_WENJIANSHOUHUI_FSWJZZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANSHOUHUI_FSWJDZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANSHOUHUI_FSFJZZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANSHOUHUI_FSFJDZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANSHOUHUI_JSRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_B_VT_WENJIANSHOUHUI_JSWJZZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANSHOUHUI_JSWJDZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANSHOUHUI_JSFJZZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANSHOUHUI_JSFJDZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANSHOUHUI_JJXH, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANSHOUHUI_FSXH, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANSHOUHUI_YJJH, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANSHOUHUI_JJBS, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANSHOUHUI_FSR, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANSHOUHUI_XB, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANSHOUHUI_SFDG, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANSHOUHUI_FSRBLSY, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANSHOUHUI_FSRXB, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_VT_Wenjianshouhui = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GW_B_VT_WENJIANTUIHUI
        '----------------------------------------------------------------
        Private Function createDataTables_VT_Wenjiantuihui(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GW_B_VT_WENJIANTUIHUI)
                With table.Columns
                    .Add(FIELD_GW_B_VT_WENJIANTUIHUI_FSR, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANTUIHUI_FSRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_B_VT_WENJIANTUIHUI_BLSY, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANTUIHUI_JSRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_B_VT_WENJIANTUIHUI_FSWJZZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANTUIHUI_FSWJDZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANTUIHUI_FSFJZZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANTUIHUI_FSFJDZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANTUIHUI_JSWJZZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANTUIHUI_JSWJDZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANTUIHUI_JSFJZZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANTUIHUI_JSFJDZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANTUIHUI_JJXH, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANTUIHUI_FSXH, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANTUIHUI_YJJH, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANTUIHUI_FSRBLSY, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANTUIHUI_JJBS, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANTUIHUI_XB, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANTUIHUI_FSRXB, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_VT_Wenjiantuihui = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GW_B_VT_WENJIANBUYUE
        '----------------------------------------------------------------
        Private Function createDataTables_Buyue(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GW_B_VT_WENJIANBUYUE)
                With table.Columns
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_WJBS, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_JJXH, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_YJJH, GetType(System.Int32))

                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_FSXH, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_FSR, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_FSRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_FSZZWJ, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_FSDZWJ, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_FSZZFJ, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_FSDZFJ, GetType(System.Int32))

                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_JSXH, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_JSR, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_XB, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_JSRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_JSZZWJ, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_JSDZWJ, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_JSZZFJ, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_JSDZFJ, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_BLZHQX, GetType(System.DateTime))
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_WCRQ, GetType(System.DateTime))

                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_WTR, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_BLLX, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_BLZL, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_BLZT, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_JJBS, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_SFDG, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_JJSM, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_BWTX, GetType(System.Int32))

                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_BLQK, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Buyue = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GW_B_VT_CHENGBANQINGKUANG
        '----------------------------------------------------------------
        Private Function createDataTables_VT_Chengbanqingkuang(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GW_B_VT_CHENGBANQINGKUANG)
                With table.Columns
                    .Add(FIELD_GW_B_VT_CHENGBANQINGKUANG_WJBS, GetType(System.String))
                    .Add(FIELD_GW_B_VT_CHENGBANQINGKUANG_JJXH, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_CHENGBANQINGKUANG_BLXH, GetType(System.Int32))

                    .Add(FIELD_GW_B_VT_CHENGBANQINGKUANG_BLLX, GetType(System.String))
                    .Add(FIELD_GW_B_VT_CHENGBANQINGKUANG_BLZL, GetType(System.String))
                    .Add(FIELD_GW_B_VT_CHENGBANQINGKUANG_BLRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_B_VT_CHENGBANQINGKUANG_BLJG, GetType(System.String))
                    .Add(FIELD_GW_B_VT_CHENGBANQINGKUANG_BLRY, GetType(System.String))
                    .Add(FIELD_GW_B_VT_CHENGBANQINGKUANG_XBBZ, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_VT_Chengbanqingkuang = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GW_V_QUANBUGONGWEN
        '----------------------------------------------------------------
        Private Function createDataTables_QuanbuGongwen(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GW_V_QUANBUGONGWEN)
                With table.Columns
                    .Add(FIELD_GW_V_QUANBUGONGWEN_WJBS, GetType(System.String))
                    .Add(FIELD_GW_V_QUANBUGONGWEN_BLLX, GetType(System.String))
                    .Add(FIELD_GW_V_QUANBUGONGWEN_WJZL, GetType(System.String))
                    .Add(FIELD_GW_V_QUANBUGONGWEN_ZSDW, GetType(System.String))
                    .Add(FIELD_GW_V_QUANBUGONGWEN_WJBT, GetType(System.String))
                    .Add(FIELD_GW_V_QUANBUGONGWEN_WJZH, GetType(System.String))
                    .Add(FIELD_GW_V_QUANBUGONGWEN_JGDZ, GetType(System.String))
                    .Add(FIELD_GW_V_QUANBUGONGWEN_WJNF, GetType(System.String))
                    .Add(FIELD_GW_V_QUANBUGONGWEN_WJXH, GetType(System.String))
                    .Add(FIELD_GW_V_QUANBUGONGWEN_MMDJ, GetType(System.String))
                    .Add(FIELD_GW_V_QUANBUGONGWEN_JJCD, GetType(System.String))
                    .Add(FIELD_GW_V_QUANBUGONGWEN_WJND, GetType(System.Int32))
                    .Add(FIELD_GW_V_QUANBUGONGWEN_ZBDW, GetType(System.String))
                    .Add(FIELD_GW_V_QUANBUGONGWEN_NGR, GetType(System.String))
                    .Add(FIELD_GW_V_QUANBUGONGWEN_NGRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_V_QUANBUGONGWEN_BLZT, GetType(System.String))
                    .Add(FIELD_GW_V_QUANBUGONGWEN_LSH, GetType(System.String))
                    .Add(FIELD_GW_V_QUANBUGONGWEN_ZTC, GetType(System.String))
                    .Add(FIELD_GW_V_QUANBUGONGWEN_KSSW, GetType(System.Int32))
                    .Add(FIELD_GW_V_QUANBUGONGWEN_WJRQ, GetType(System.DateTime))

                    .Add(FIELD_GW_V_QUANBUGONGWEN_FSRQ, GetType(System.DateTime))

                    .Add(FIELD_GW_V_QUANBUGONGWEN_BWTX, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_QuanbuGongwen = table

        End Function

        '----------------------------------------------------------------
        '����TABLE_GW_V_DUCHAGONGZUO
        '----------------------------------------------------------------
        Private Function createDataTables_Duchagongzuo(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GW_V_DUCHAGONGZUO)
                With table.Columns
                    .Add(FIELD_GW_V_DUCHAGONGZUO_WJBS, GetType(System.String))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_LXBS, GetType(System.String))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_BLBS, GetType(System.String))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_BJBS, GetType(System.String))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_LSH, GetType(System.String))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_BLLX, GetType(System.String))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_WJZL, GetType(System.String))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_RWLB, GetType(System.String))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_SCJD, GetType(System.String))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_MMDJ, GetType(System.String))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_JJCD, GetType(System.String))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_BLZT, GetType(System.String))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_XMBT, GetType(System.String))

                    .Add(FIELD_GW_V_DUCHAGONGZUO_DCBH, GetType(System.String))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_DCWH, GetType(System.String))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_DCLX, GetType(System.String))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_DCR, GetType(System.String))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_BLSX, GetType(System.DateTime))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_CBDW, GetType(System.String))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_CBR, GetType(System.String))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_XBDW, GetType(System.String))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_XBR, GetType(System.String))

                    .Add(FIELD_GW_V_DUCHAGONGZUO_LXPZR, GetType(System.String))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_LXPZRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_BJPZR, GetType(System.String))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_BJPZRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_LXDW, GetType(System.String))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_LXR, GetType(System.String))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_LXRQ, GetType(System.DateTime))

                    .Add(FIELD_GW_V_DUCHAGONGZUO_BWTX, GetType(System.String))

                    '�µĹ����������鵥
                    .Add(FIELD_GW_V_DUCHAGONGZUO_PZR, GetType(System.String))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_PZRQ, GetType(System.DateTime))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Duchagongzuo = table

        End Function


        '----------------------------------------------------------------
        '����TABLE_GW_V_YIJIAOWENJIAN
        '----------------------------------------------------------------
        Private Function createDataTables_YijiaoWenjian(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GW_V_YIJIAOWENJIAN)
                With table.Columns
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_WJBS, GetType(System.String))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_YJRY, GetType(System.String))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_YJRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_YJSM, GetType(System.String))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_JSRY, GetType(System.String))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_JSRQ, GetType(System.DateTime))


                    .Add(FIELD_GW_V_YIJIAOWENJIAN_SFYJ, GetType(System.String))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_SFJS, GetType(System.String))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_WJLX, GetType(System.String))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_BLLX, GetType(System.String))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_WJZL, GetType(System.String))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_ZSDW, GetType(System.String))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_WJBT, GetType(System.String))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_WJZH, GetType(System.String))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_JGDZ, GetType(System.String))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_WJNF, GetType(System.String))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_WJXH, GetType(System.String))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_MMDJ, GetType(System.String))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_JJCD, GetType(System.String))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_WJND, GetType(System.Int32))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_ZBDW, GetType(System.String))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_NGR, GetType(System.String))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_NGRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_BLZT, GetType(System.String))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_LSH, GetType(System.String))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_ZTC, GetType(System.String))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_KSSW, GetType(System.Int32))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_YijiaoWenjian = table

        End Function


    End Class 'FlowData

End Namespace 'Xydc.Platform.Common.Data
