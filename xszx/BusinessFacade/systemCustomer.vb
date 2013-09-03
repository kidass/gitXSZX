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
    ' ����    ��systemCustomer
    '
    ' ���������� 
    '   ���ṩ���û���Ϣ����ı��ֲ�֧��
    '----------------------------------------------------------------
    Public Class systemCustomer
        Inherits MarshalByRefObject

        '----------------------------------------------------------------
        ' ��ȫ�ͷű�����Դ
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.systemCustomer)
            Try
                If Not (obj Is Nothing) Then
                    'obj.Dispose()
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
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doExportToExcel( _
            ByRef strErrMsg As String, _
            ByVal objDataSet As System.Data.DataSet, _
            ByVal strExcelFile As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    doExportToExcel = .doExportToExcel(strErrMsg, objDataSet, strExcelFile)
                End With
            Catch ex As Exception
                doExportToExcel = False
                strErrMsg = ex.Message
            End Try

        End Function









        '----------------------------------------------------------------
        ' ��֤�û��������Ƿ�ƥ�䣿���ȼ��ܺ��������֤�������֤�ɹ��򷵻أ�
        ' ��������������֤���ɹ����������м��ܲ��Զ�����Ϊ�������룬
        ' ���ɹ��򷵻ش���
        '     strErrMsg     ����������򷵻ش�����Ϣ
        '     strUserId     ��Ҫ��֤���û���ʶ
        '     strPassword   ��Ҫ��֤���û�������(�û����������-����)
        '     strNewPassword��������֤���������(���ܺ������)
        ' ����
        '     True          ���û�������һ��
        '     False         ���û������벻ƥ��
        '----------------------------------------------------------------
        Public Function doVerifyUserPassword( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef strNewPassword As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    doVerifyUserPassword = .doVerifyUserPassword(strErrMsg, strUserId, strPassword, strNewPassword)
                End With
            Catch ex As Exception
                doVerifyUserPassword = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��֤���ݿ����Ӵ�
        '     strErrMsg     ����������򷵻ش�����Ϣ
        '     strConnect    ��Ҫ��֤�����Ӵ�
        ' ����
        '     True          ����Ч
        '     False         ����Ч
        '----------------------------------------------------------------
        Public Function doVerifyConnectionString( _
            ByRef strErrMsg As String, _
            ByVal strConnect As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    doVerifyConnectionString = .doVerifyConnectionString(strErrMsg, strConnect)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doVerifyConnectionString = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' �����û����룺���strCzyId=strUserId�����Լ������Լ������룬
        ' ����ΪSAǿ�Ƹ���strUserId�����롣�ɹ����ؼ��ܺ�������룬
        ' ���ɹ��򷵻ش�������û�=SA���򲻼���
        '     strErrMsg      ����������򷵻ش�����Ϣ
        '     strCzyId       ����ǰ����Ա
        '     strCzyPassword ����ǰ����Ա������
        '     strUserId      ��Ҫ����������û���ʶ
        '     strNewPassword1��������1
        '     strNewPassword2��������2
        '     strNewPassword �����ؼ��ܺ��������
        ' ����
        '     True           ���ɹ�
        '     False          ��ʧ��
        '----------------------------------------------------------------
        Public Function doModifyPassword( _
            ByRef strErrMsg As String, _
            ByVal strCzyId As String, _
            ByVal strCzyPassword As String, _
            ByVal strUserId As String, _
            ByVal strNewPassword1 As String, _
            ByVal strNewPassword2 As String, _
            ByRef strNewPassword As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    doModifyPassword = .doModifyPassword(strErrMsg, strCzyId, strCzyPassword, strUserId, strNewPassword1, strNewPassword2, strNewPassword)
                End With
            Catch ex As Exception
                doModifyPassword = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡ�����û���Ϣ���ݼ�
        '     strErrMsg      ����������򷵻ش�����Ϣ
        '     strUserId      ���û���ʶ
        '     strPassword    ���û�����
        '     strWhere       ����������
        '     blnUnused      ��������
        '     objCustomerData���û���Ϣ���ݼ�
        ' ����
        '     True           ���ɹ�
        '     False          ��ʧ��
        '----------------------------------------------------------------
        Public Function getRenyuanData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByVal blnUnused As Boolean, _
            ByRef objCustomerData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    getRenyuanData = .getRenyuanData(strErrMsg, strUserId, strPassword, strWhere, blnUnused, objCustomerData)
                End With
            Catch ex As Exception
                getRenyuanData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' �����û�Id��ȡ�û���Ϣ���ݼ�
        '     strErrMsg      ����������򷵻ش�����Ϣ
        '     strUserId      ���û���ʶ
        '     strPassword    ���û�����
        '     strOptions     ����ȡ����ѡ��ABCD
        '                      A=1 ��ȡ��Ա��������
        '                      B=1 ��ȡ��Ա����֯������������
        '                      C=1 ��ȡ��Ա���ϸڵ�������
        '                      D=1 ��ȡ��Ա����ȫ���ӵı�����
        '     objCustomerData���û���Ϣ���ݼ�
        ' ����
        '     True           ���ɹ�
        '     False          ��ʧ��
        '----------------------------------------------------------------
        Public Function getRenyuanData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strOptions As String, _
            ByRef objCustomerData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    getRenyuanData = .getRenyuanData(strErrMsg, strUserId, strPassword, strOptions, objCustomerData)
                End With
            Catch ex As Exception
                getRenyuanData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ����strRYDM��ȡ�û���Ϣ���ݼ�
        '     strErrMsg      ����������򷵻ش�����Ϣ
        '     strUserId      ���û���ʶ
        '     strPassword    ���û�����
        '     strRYDM        ����Ա����
        '     strOptions     ����ȡ����ѡ��ABCD
        '                      A=1 ��ȡ��Ա��������
        '                      B=1 ��ȡ��Ա����֯������������
        '                      C=1 ��ȡ��Ա���ϸڵ�������
        '                      D=1 ��ȡ��Ա����ȫ���ӵı�����
        '     objCustomerData���û���Ϣ���ݼ�
        ' ����
        '     True           ���ɹ�
        '     False          ��ʧ��
        '----------------------------------------------------------------
        Public Function getRenyuanData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRYDM As String, _
            ByVal strOptions As String, _
            ByRef objCustomerData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    getRenyuanData = .getRenyuanData(strErrMsg, strUserId, strPassword, strRYDM, strOptions, objCustomerData)
                End With
            Catch ex As Exception
                getRenyuanData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ����strRYMC��ȡ�û���Ϣ���ݼ�
        '     strErrMsg      ����������򷵻ش�����Ϣ
        '     strUserId      ���û���ʶ
        '     strPassword    ���û�����
        '     strRYDM        ����Ա����
        '     strRYMC        ����Ա����
        '     strOptions     ����ȡ����ѡ��ABCD
        '                      A=1 ��ȡ��Ա��������
        '                      B=1 ��ȡ��Ա����֯������������
        '                      C=1 ��ȡ��Ա���ϸڵ�������
        '                      D=1 ��ȡ��Ա����ȫ���ӵı�����
        '     objCustomerData���û���Ϣ���ݼ�
        ' ����
        '     True           ���ɹ�
        '     False          ��ʧ��
        '----------------------------------------------------------------
        Public Function getRenyuanData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRYDM As String, _
            ByVal strRYMC As String, _
            ByVal strOptions As String, _
            ByRef objCustomerData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    getRenyuanData = .getRenyuanData(strErrMsg, strUserId, strPassword, strRYDM, strRYMC, strOptions, objCustomerData)
                End With
            Catch ex As Exception
                getRenyuanData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ����strRYDM��ȡ�û���Ϣ���ݼ�
        '     strErrMsg      ����������򷵻ش�����Ϣ
        '     strUserId      ���û���ʶ
        '     strPassword    ���û�����
        '     strRYDM        ����Ա����
        '     strZZDM        ��Ҫ��ȡ����֯����
        '     strOptions     ����ȡ����ѡ��ABCD
        '                      A=1 ��ȡ��Ա��������
        '                      B=1 ��ȡ��Ա����֯������������
        '                      C=1 ��ȡ��Ա���ϸڵ�������
        '                      D=1 ��ȡ��Ա����ȫ���ӵı�����
        '     blnUser        ������
        '     objCustomerData���û���Ϣ���ݼ�
        ' ����
        '     True           ���ɹ�
        '     False          ��ʧ��

        '----------------------------------------------------------------
        Public Function getRenyuanData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRYDM As String, _
            ByVal strZZDM As String, _
            ByVal strOptions As String, _
            ByVal blnUser As Boolean, _
            ByRef objCustomerData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    getRenyuanData = .getRenyuanData(strErrMsg, strUserId, strPassword, strRYDM, strZZDM, strOptions, blnUser, objCustomerData)
                End With
            Catch ex As Exception
                getRenyuanData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡ��֯������Ϣ���ݼ�(����֯������������,������������)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     objBumenData         ����֯������Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getBumenData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef objBumenData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    getBumenData = .getBumenData(strErrMsg, strUserId, strPassword, objBumenData)
                End With
            Catch ex As Exception
                getBumenData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ������֯�����ȡ��֯����ȫ������Ϣ���ݼ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strZZDM              ����֯����
        '     objBumenData         ����֯������Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getBumenData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZZDM As String, _
            ByRef objBumenData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    getBumenData = .getBumenData(strErrMsg, strUserId, strPassword, strZZDM, objBumenData)
                End With
            Catch ex As Exception
                getBumenData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ������֯�����ȡ��֯����������Ϣ���ݼ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strZZDM              ����֯����
        '     blnUnused            ��������
        '     objBumenData         ����֯������Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getBumenData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZZDM As String, _
            ByVal blnUnused As Boolean, _
            ByRef objBumenData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    getBumenData = .getBumenData(strErrMsg, strUserId, strPassword, strZZDM, blnUnused, objBumenData)
                End With
            Catch ex As Exception
                getBumenData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ������֯���ƻ�ȡ��֯����ȫ������Ϣ���ݼ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strZZDM              ����֯����(�ӿ�������)
        '     strZZMC              ����֯����
        '     objBumenData         ����֯������Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getBumenData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZZDM As String, _
            ByVal strZZMC As String, _
            ByRef objBumenData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    getBumenData = .getBumenData(strErrMsg, strUserId, strPassword, strZZDM, strZZMC, objBumenData)
                End With
            Catch ex As Exception
                getBumenData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ������֯���ƻ�ȡ��֯����������Ϣ���ݼ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     blnUnused            ��������
        '     strZZMC              ����֯����
        '     objBumenData         ����֯������Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getBumenData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal blnUnused As Boolean, _
            ByVal strZZMC As String, _
            ByRef objBumenData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    getBumenData = .getBumenData(strErrMsg, strUserId, strPassword, blnUnused, strZZMC, objBumenData)
                End With
            Catch ex As Exception
                getBumenData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡָ����֯�����µ���Ա��Ϣ���ݼ�(����֯���롢��Ա�����������)
        ' ����Ա��ȫ����������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strZZDM              ��ָ����֯��������
        '     blnBaohanXiaji       ���Ƿ�����¼�����
        '     strWhere             �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     objRenyuanData       ��ָ����֯�����µ���Ա��Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getRenyuanInBumenData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZZDM As String, _
            ByVal blnBaohanXiaji As Boolean, _
            ByVal strWhere As String, _
            ByRef objRenyuanData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    getRenyuanInBumenData = .getRenyuanInBumenData(strErrMsg, strUserId, strPassword, strZZDM, blnBaohanXiaji, strWhere, objRenyuanData)
                End With
            Catch ex As Exception
                getRenyuanInBumenData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ����ָ����Χ���ƻ�ȡ��Χ�µ���֯��Ϣ����Ա��Ϣ
        ' ����Ա��ȫ����������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strFWMC              ��ָ����Χ����
        '     blnAllowBM           ����������Ϣֱ��ѡ��
        '     strWhere             ����������(Ĭ�ϱ�ǰ׺a.)
        '     objSelectRenyuanData ��ָ����֯�����µ���Ա��Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getRenyuanOrBumenInFanweiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strFWMC As String, _
            ByVal blnAllowBM As Boolean, _
            ByVal strWhere As String, _
            ByRef objSelectRenyuanData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    getRenyuanOrBumenInFanweiData = .getRenyuanOrBumenInFanweiData(strErrMsg, strUserId, strPassword, strFWMC, blnAllowBM, strWhere, objSelectRenyuanData)
                End With
            Catch ex As Exception
                getRenyuanOrBumenInFanweiData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ����ָ����Χ���ƻ�ȡ��Χ�µ���֯��Ϣ
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strFWMC              ��ָ����Χ����
        '     strWhere             ����������(Ĭ�ϱ�ǰ׺a.)
        '     objSelectBumenData   ��ָ����֯�����µ���Ա��Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getBumenInFanweiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strFWMC As String, _
            ByVal strWhere As String, _
            ByRef objSelectBumenData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    getBumenInFanweiData = .getBumenInFanweiData(strErrMsg, strUserId, strPassword, strFWMC, strWhere, objSelectBumenData)
                End With
            Catch ex As Exception
                getBumenInFanweiData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡstrBLR��strWTR��ֱ�ӷ��͵���Ա�����б��SQL���
        '     strBLR               ����ǰ�����˵�����
        '     strWTRArray          ��strBLR��strWTRί�н��д���
        ' ����
        '                          ��SQL���
        '----------------------------------------------------------------
        Public Function getSendRestrictWhere( _
            ByVal strBLR As String, _
            ByVal strWTRArray As String()) As String

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    getSendRestrictWhere = .getSendRestrictWhere(strBLR, strWTRArray)
                End With
            Catch ex As Exception
                getSendRestrictWhere = ""
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡstrBLR��strWTR��ֱ�ӷ��͵���Ա�����б��SQL���
        '     strBLR               ����ǰ�����˵�����
        '     strWTR               ��strBLR��strWTRί�н��д���
        ' ����
        '                          ��SQL���
        '----------------------------------------------------------------
        Public Function getSendRestrictWhere( _
            ByVal strBLR As String, _
            ByVal strWTR As String) As String

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    getSendRestrictWhere = .getSendRestrictWhere(strBLR, strWTR)
                End With
            Catch ex As Exception
                getSendRestrictWhere = ""
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡstrBLR��strWTR��ֱ�ӷ��͵���Ա�����б��SQL���
        '     strBLR               ����ǰ�����˵�����
        '     strWTR               ��strBLR��strWTRί�н��д���
        '     blnByRYDM            ��ָ��������Ա����
        ' ����
        '                          ��SQL���
        '----------------------------------------------------------------
        Public Function getSendRestrictWhere( _
            ByVal strBLR As String, _
            ByVal strWTR As String, _
            ByVal blnByRYDM As Boolean) As String

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    getSendRestrictWhere = .getSendRestrictWhere(strBLR, strWTR, blnByRYDM)
                End With
            Catch ex As Exception
                getSendRestrictWhere = ""
            End Try

        End Function

        '----------------------------------------------------------------
        ' ������Ա���ƻ�ȡ��Ա����
        '     strErrMsg     ����������򷵻ش�����Ϣ
        '     strUserId     ���û���ʶ
        '     strPassword   ���û�����
        '     strRYMC       ����Ա����
        '     strRYDM       ����Ա����(����)
        ' ����
        '     True          ���ɹ�
        '     False         ��ʧ��
        '----------------------------------------------------------------
        Public Function getRydmByRymc( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRYMC As String, _
            ByRef strRYDM As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    getRydmByRymc = .getRydmByRymc(strErrMsg, strUserId, strPassword, strRYMC, strRYDM)
                End With
            Catch ex As Exception
                getRydmByRymc = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ������Ա�����ȡ��Ա����
        '     strErrMsg     ����������򷵻ش�����Ϣ
        '     strUserId     ���û���ʶ
        '     strPassword   ���û�����
        '     strRYDM       ����Ա����
        '     strRYMC       ����Ա����(����)
        ' ����
        '     True          ���ɹ�
        '     False         ��ʧ��
        '----------------------------------------------------------------
        Public Function getRymcByRydm( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRYDM As String, _
            ByRef strRYMC As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    getRymcByRydm = .getRymcByRydm(strErrMsg, strUserId, strPassword, strRYDM, strRYMC)
                End With
            Catch ex As Exception
                getRymcByRydm = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���ݵ�λ���ƻ�ȡ��λ����
        '     strErrMsg     ����������򷵻ش�����Ϣ
        '     strUserId     ���û���ʶ
        '     strPassword   ���û�����
        '     strZZMC       ����λ����
        '     strZZDM       ����λ����(����)
        ' ����
        '     True          ���ɹ�
        '     False         ��ʧ��
        '----------------------------------------------------------------
        Public Function getZzdmByZzmc( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZZMC As String, _
            ByRef strZZDM As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    getZzdmByZzmc = .getZzdmByZzmc(strErrMsg, strUserId, strPassword, strZZMC, strZZDM)
                End With
            Catch ex As Exception
                getZzdmByZzmc = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���ݵ�λ���ƻ�ȡ��λ����(ȫ��)
        '     strErrMsg     ����������򷵻ش�����Ϣ
        '     strUserId     ���û���ʶ
        '     strPassword   ���û�����
        '     strZZMC       ����λ����
        '     strZZBM       ����λ����(����)
        ' ����
        '     True          ���ɹ�
        '     False         ��ʧ��
        '----------------------------------------------------------------
        Public Function getZzbmByZzmc( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZZMC As String, _
            ByRef strZZBM As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    getZzbmByZzmc = .getZzbmByZzmc(strErrMsg, strUserId, strPassword, strZZMC, strZZBM)
                End With
            Catch ex As Exception
                getZzbmByZzmc = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���ݵ�λ�����ȡ��λ����
        '     strErrMsg     ����������򷵻ش�����Ϣ
        '     strUserId     ���û���ʶ
        '     strPassword   ���û�����
        '     strZZDM       ����λ����
        '     strZZMC       ����λ����(����)
        ' ����
        '     True          ���ɹ�
        '     False         ��ʧ��
        '----------------------------------------------------------------
        Public Function getZzmcByZzdm( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZZDM As String, _
            ByRef strZZMC As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    getZzmcByZzdm = .getZzmcByZzdm(strErrMsg, strUserId, strPassword, strZZDM, strZZMC)
                End With
            Catch ex As Exception
                getZzmcByZzdm = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���ݵ�λ�����ȡ��֯���ƣ���֯����
        '     strErrMsg     ����������򷵻ش�����Ϣ
        '     strUserId     ���û���ʶ
        '     strPassword   ���û�����
        '     strZZDM       ����λ����
        '     strBMXX()     ��strBMXX(0)=��֯����,strBMXX(1)=��֯����(����)
        ' ����
        '     True          ���ɹ�
        '     False         ��ʧ��
        '----------------------------------------------------------------
        Public Function getZzmcByZzbm( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZZDM As String, _
            ByRef strBMXX() As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    getZzmcByZzbm = .getZzmcByZzbm(strErrMsg, strUserId, strPassword, strZZDM, strBMXX)
                End With
            Catch ex As Exception
                getZzmcByZzbm = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ������Ա�����ȡ��λ����
        '     strErrMsg     ����������򷵻ش�����Ϣ
        '     strUserId     ���û���ʶ
        '     strPassword   ���û�����
        '     strRYDM       ����Ա����
        '     strZZMC       ����λ����(����)
        ' ����
        '     True          ���ɹ�
        '     False         ��ʧ��
        '----------------------------------------------------------------
        Public Function getZzmcByRydm( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRYDM As String, _
            ByRef strZZMC As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    getZzmcByRydm = .getZzmcByRydm(strErrMsg, strUserId, strPassword, strRYDM, strZZMC)
                End With
            Catch ex As Exception
                getZzmcByRydm = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ������Ա���ƻ�ȡ��λ����
        '     strErrMsg     ����������򷵻ش�����Ϣ
        '     strUserId     ���û���ʶ
        '     strPassword   ���û�����
        '     strRYMC       ����Ա����
        '     strZZMC       ����λ����(����)
        ' ����
        '     True          ���ɹ�
        '     False         ��ʧ��
        '----------------------------------------------------------------
        Public Function getZzmcByRymc( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRYMC As String, _
            ByRef strZZMC As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    getZzmcByRymc = .getZzmcByRymc(strErrMsg, strUserId, strPassword, strRYMC, strZZMC)
                End With
            Catch ex As Exception
                getZzmcByRymc = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ����ָ���ϼ������ȡ�¼�����ֵ
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strSJDM              ���ϼ�����
        '     intFJCDSM            ������ּ�����
        '     strNewZZDM           ���´��루���أ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getNewZZDM( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strSJDM As String, _
            ByVal intFJCDSM() As Integer, _
            ByRef strNewZZDM As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    getNewZZDM = .getNewZZDM(strErrMsg, strUserId, strPassword, strSJDM, intFJCDSM, strNewZZDM)
                End With
            Catch ex As Exception
                getNewZZDM = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���桰����_B_��֯������������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     objOldData           ��������
        '     objNewData           ��������
        '     objenumEditType      ���༭����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doSaveZuzhijigouData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            doSaveZuzhijigouData = False
            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    doSaveZuzhijigouData = .doSaveZuzhijigouData(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ɾ��������_B_��֯������������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     objOldData           ��������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doDeleteZuzhijigouData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    doDeleteZuzhijigouData = .doDeleteZuzhijigouData(strErrMsg, strUserId, strPassword, objOldData)
                End With
            Catch ex As Exception
                doDeleteZuzhijigouData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡ�µ���Ա���
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strZZDM              ��������֯����
        '     strNewRYXH           ������Ա���(����)
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getNewRYXH( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZZDM As String, _
            ByRef strNewRYXH As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    getNewRYXH = .getNewRYXH(strErrMsg, strUserId, strPassword, strZZDM, strNewRYXH)
                End With
            Catch ex As Exception
                getNewRYXH = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���桰����_B_��Ա��������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     objOldData           ��������
        '     objNewData           ��������
        '     objenumEditType      ���༭����
        '     objNewDataSG         ���ϸ����ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doSaveRenyuanData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType, _
            ByVal objNewDataSG As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    doSaveRenyuanData = .doSaveRenyuanData(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType, objNewDataSG)
                End With
            Catch ex As Exception
                doSaveRenyuanData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���桰����_B_��Ա_���Ρ�������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     objOldData           ��������
        '     objNewData           ��������
        '     objUpdateData        �����¡�����_B_��Ա������ 
        '     objenumEditType      ���༭����
        '     objNewDataSG         ���ϸ����ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��

        '----------------------------------------------------------------
        Public Function doSaveRenyuanData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objUpdateData As System.Collections.Specialized.NameValueCollection, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType, _
            ByVal objNewDataSG As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    doSaveRenyuanData = .doSaveRenyuanData(strErrMsg, strUserId, strPassword, objOldData, objNewData, objUpdateData, objenumEditType, objNewDataSG)
                End With
            Catch ex As Exception
                doSaveRenyuanData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ɾ��������_B_��Ա��������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     objOldData           ��������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doDeleteRenyuanData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    doDeleteRenyuanData = .doDeleteRenyuanData(strErrMsg, strUserId, strPassword, objOldData)
                End With
            Catch ex As Exception
                doDeleteRenyuanData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ָ����ԱobjRenyuanDataλ���ƶ���objRenyuanDataTo
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     objRenyuanData       ��׼���ƶ�����Ա����
        '     objRenyuanDataTo     ���ƶ�������Ա����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doRenyuanMoveTo( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objRenyuanData As System.Data.DataRow, _
            ByVal objRenyuanDataTo As System.Data.DataRow) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    doRenyuanMoveTo = .doRenyuanMoveTo(strErrMsg, strUserId, strPassword, objRenyuanData, objRenyuanDataTo)
                End With
            Catch ex As Exception
                doRenyuanMoveTo = False
                strErrMsg = ex.Message
            End Try

        End Function




        '----------------------------------------------------------------
        ' ��ȡϵͳ������־����
        '     strErrMsg                ����������򷵻ش�����Ϣ
        '     strUserId                ���û���ʶ
        '     strPassword              ���û�����
        '     strWhere                 ����������
        '     objXitongJinchuRizhiData ��ϵͳ������־��Ϣ���ݼ�
        ' ����
        '     True                     ���ɹ�
        '     False                    ��ʧ��
        '----------------------------------------------------------------
        Public Function getXitongJinchuRizhiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objXitongJinchuRizhiData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    getXitongJinchuRizhiData = .getXitongJinchuRizhiData(strErrMsg, strUserId, strPassword, strWhere, objXitongJinchuRizhiData)
                End With
            Catch ex As Exception
                getXitongJinchuRizhiData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡϵͳ�����û�����
        '     strErrMsg                ����������򷵻ش�����Ϣ
        '     strUserId                ���û���ʶ
        '     strPassword              ���û�����
        '     strWhere                 ����������
        '     objZaixianYonghuData     �������û���Ϣ���ݼ�
        ' ����
        '     True                     ���ɹ�
        '     False                    ��ʧ��
        '----------------------------------------------------------------
        Public Function getZaixianYonghuData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objZaixianYonghuData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    getZaixianYonghuData = .getZaixianYonghuData(strErrMsg, strUserId, strPassword, strWhere, objZaixianYonghuData)
                End With
            Catch ex As Exception
                getZaixianYonghuData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' д��ϵͳ������־��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strCZLX              ����������
        '     strAddress           ��������ַ
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doWriteXitongJinchuRizhi( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strCZLX As String, _
            ByVal strAddress As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    doWriteXitongJinchuRizhi = .doWriteXitongJinchuRizhi(strErrMsg, strUserId, strPassword, strCZLX, strAddress)
                End With
            Catch ex As Exception
                doWriteXitongJinchuRizhi = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' �����ϵͳ������־��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doDeleteXitongJinchuRizhi( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    doDeleteXitongJinchuRizhi = .doDeleteXitongJinchuRizhi(strErrMsg, strUserId, strPassword)
                End With
            Catch ex As Exception
                doDeleteXitongJinchuRizhi = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ɾ����ϵͳ������־��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     intXH                ��Ҫɾ�������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doDeleteXitongJinchuRizhi( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intXH As Integer) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    doDeleteXitongJinchuRizhi = .doDeleteXitongJinchuRizhi(strErrMsg, strUserId, strPassword, intXH)
                End With
            Catch ex As Exception
                doDeleteXitongJinchuRizhi = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ɾ����ϵͳ������־��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strQSRQ              ��Ҫɾ���Ŀ�ʼ����
        '     strZZRQ              ��Ҫɾ���Ľ�������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doDeleteXitongJinchuRizhi( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    doDeleteXitongJinchuRizhi = .doDeleteXitongJinchuRizhi(strErrMsg, strUserId, strPassword, strQSRQ, strZZRQ)
                End With
            Catch ex As Exception
                doDeleteXitongJinchuRizhi = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' д�������û�������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doWriteZaixianYonghu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    doWriteZaixianYonghu = .doWriteZaixianYonghu(strErrMsg, strUserId, strPassword)
                End With
            Catch ex As Exception
                doWriteZaixianYonghu = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ɾ���������û�������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doDeleteZaixianYonghu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    doDeleteZaixianYonghu = .doDeleteZaixianYonghu(strErrMsg, strUserId, strPassword)
                End With
            Catch ex As Exception
                doDeleteZaixianYonghu = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡ�û�������־����
        '     strErrMsg                ����������򷵻ش�����Ϣ
        '     strUserId                ���û���ʶ
        '     strPassword              ���û�����
        '     strWhere                 ����������
        '     objLogData               ��(����)���ݼ�
        ' ����
        '     True                     ���ɹ�
        '     False                    ��ʧ��
        '----------------------------------------------------------------
        Public Function getYonghuCaozuoRizhiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objLogData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    getYonghuCaozuoRizhiData = .getYonghuCaozuoRizhiData(strErrMsg, strUserId, strPassword, strWhere, objLogData)
                End With
            Catch ex As Exception
                getYonghuCaozuoRizhiData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' д���û�������־��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strAddress           ��������ַ
        '     strCZSM              ������˵��
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doWriteYonghuCaozuoRizhi( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strAddress As String, _
            ByVal strCZSM As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    doWriteYonghuCaozuoRizhi = .doWriteYonghuCaozuoRizhi(strErrMsg, strUserId, strPassword, strAddress, strCZSM)
                End With
            Catch ex As Exception
                doWriteYonghuCaozuoRizhi = False
                strErrMsg = ex.Message
            End Try

        End Function


        '----------------------------------------------------------------
        ' д��ϵͳ������־��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strCZLX              ����������
        '     strAddress           ��������ַ
        '     strMachine           ����������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        ' ����˵����
        '      ����strMachine��������ش���
        '----------------------------------------------------------------
        Public Function doWriteXitongJinchuRizhi( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strCZLX As String, _
            ByVal strAddress As String, _
            ByVal strMachine As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    doWriteXitongJinchuRizhi = .doWriteXitongJinchuRizhi(strErrMsg, strUserId, strPassword, strCZLX, strAddress, strMachine)
                End With
            Catch ex As Exception
                doWriteXitongJinchuRizhi = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' д���û�������־��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strAddress           ��������ַ
        '     strMachine           ����������
        '     strCZSM              ������˵��
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        ' ����˵����
        '      ����strMachine��������ش���
        '----------------------------------------------------------------
        Public Function doWriteYonghuCaozuoRizhi( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strAddress As String, _
            ByVal strMachine As String, _
            ByVal strCZSM As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    doWriteYonghuCaozuoRizhi = .doWriteYonghuCaozuoRizhi(strErrMsg, strUserId, strPassword, strAddress, strMachine, strCZSM)
                End With
            Catch ex As Exception
                doWriteYonghuCaozuoRizhi = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��顰����_B_��Ա���ı�ʶ�Ƿ��Ѵ���
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strNewUserId         �������û���ʶ
        '     strNewUserZZDM       �������û���֯����
        ' ����
        '     intType              ��1-ͬ������ӣ�0-��ͬ�������
        '     objCustomerData      ��������ڣ��ͷ��ش��ڵļ�¼��
        '     True                 ��������
        '     False                ������

        '----------------------------------------------------------------
        Public Function doVerifyRenyuanData( _
           ByRef strErrMsg As String, _
           ByVal strUserId As String, _
           ByVal strPassword As String, _
           ByVal strNewUserId As String, _
           ByVal strNewUserZZDM As String, _
           ByRef intType As Integer, _
           ByRef objCustomerData As Xydc.Platform.Common.Data.CustomerData) As Boolean
            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    doVerifyRenyuanData = .doVerifyRenyuanData(strErrMsg, strUserId, strPassword, strNewUserId, strNewUserZZDM, intType, objCustomerData)
                End With
            Catch ex As Exception
                doVerifyRenyuanData = False
                strErrMsg = ex.Message
            End Try

        End Function
    End Class

End Namespace
