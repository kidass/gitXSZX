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
    ' ����    ��systemLuntan
    '
    ' ���������� 
    '     �ṩ�ԡ��ڲ���̳��ģ���漰�ı��ֲ����
    '----------------------------------------------------------------
    Public Class systemLuntan
        Implements System.IDisposable

        Private m_objrulesLuntan As Xydc.Platform.BusinessRules.rulesLuntan








        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
            m_objrulesLuntan = New Xydc.Platform.BusinessRules.rulesLuntan
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
            If Not (m_objrulesLuntan Is Nothing) Then
                m_objrulesLuntan.Dispose()
                m_objrulesLuntan = Nothing
            End If
        End Sub

        '----------------------------------------------------------------
        ' ��ȫ�ͷű�����Դ
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.systemLuntan)
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
                With m_objrulesLuntan
                    doExportToExcel = .doExportToExcel(strErrMsg, objDataSet, strExcelFile, strMacroName, strMacroValue)
                End With
            Catch ex As Exception
                doExportToExcel = False
                strErrMsg = ex.Message
            End Try

        End Function






        '----------------------------------------------------------------
        ' �ж�strRYDM�Ƿ���Ч��
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     strUserId                   ���û���ʶ
        '     strPassword                 ���û�����
        '     strRYDM                     ����Ա����
        '     blnValid                    �������أ�=True��Ч��=Falseͣ��
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function isValid( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRYDM As String, _
            ByRef blnValid As Boolean) As Boolean

            Try
                With m_objrulesLuntan
                    isValid = .isValid(strErrMsg, strUserId, strPassword, strRYDM, blnValid)
                End With
            Catch ex As Exception
                isValid = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' �ж�strRYDM�Ƿ�ע�᣿
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     strUserId                   ���û���ʶ
        '     strPassword                 ���û�����
        '     strRYDM                     ����Ա����
        '     blnRegister                 �������أ�=True��ע�ᣬ=Falseδע��
        '     strRYNC                     �������ע�ᣬ������Ա�ǳ�
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function isRegistered( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRYDM As String, _
            ByRef blnRegister As Boolean, _
            ByRef strRYNC As String) As Boolean

            Try
                With m_objrulesLuntan
                    isRegistered = .isRegistered(strErrMsg, strUserId, strPassword, strRYDM, blnRegister, strRYNC)
                End With
            Catch ex As Exception
                isRegistered = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ע�ύ���û�
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strUserId              ���û���ʶ
        '     strPassword            ���û�����
        '     strRYDM                ����Ա����
        '     strRYNC                ����Ա�ǳ�
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Function doRegister( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRYDM As String, _
            ByVal strRYNC As String) As Boolean

            Try
                With m_objrulesLuntan
                    doRegister = .doRegister(strErrMsg, strUserId, strPassword, strRYDM, strRYNC)
                End With
            Catch ex As Exception
                doRegister = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡ�����û����ݣ�������֯���롱+����Ա��š�����
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     strUserId                   ���û���ʶ
        '     strPassword                 ���û�����
        '     strWhere                    �������ַ���
        '     objLuntanData               ����Ϣ���ݼ�
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function getDataSet_Yonghu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objLuntanData As Xydc.Platform.Common.Data.ggxxLuntanData) As Boolean

            Try
                With m_objrulesLuntan
                    getDataSet_Yonghu = .getDataSet_Yonghu(strErrMsg, strUserId, strPassword, strWhere, objLuntanData)
                End With
            Catch ex As Exception
                getDataSet_Yonghu = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ����strRYDM��ȡ�����û�����
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     strUserId                   ���û���ʶ
        '     strPassword                 ���û�����
        '     strRYDM                     ����Ա����
        '     blnUnused                   ��������
        '     objLuntanData               ����Ϣ���ݼ�
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function getDataSet_Yonghu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRYDM As String, _
            ByVal blnUnused As Boolean, _
            ByRef objLuntanData As Xydc.Platform.Common.Data.ggxxLuntanData) As Boolean

            Try
                With m_objrulesLuntan
                    getDataSet_Yonghu = .getDataSet_Yonghu(strErrMsg, strUserId, strPassword, strRYDM, blnUnused, objLuntanData)
                End With
            Catch ex As Exception
                getDataSet_Yonghu = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���潻���û����ݼ�¼(�����������)
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strUserId              ���û���ʶ
        '     strPassword            ���û�����
        '     strRYDM                ����Ա����
        '     strRYNC                ����Ա�ǳ�
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Function doSave_Yonghu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRYDM As String, _
            ByVal strRYNC As String) As Boolean

            Try
                With m_objrulesLuntan
                    doSave_Yonghu = .doSave_Yonghu(strErrMsg, strUserId, strPassword, strRYDM, strRYNC)
                End With
            Catch ex As Exception
                doSave_Yonghu = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ɾ�������û�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strRYDM              ����Ա����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doDelete_Yonghu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRYDM As String) As Boolean

            Try
                With m_objrulesLuntan
                    doDelete_Yonghu = .doDelete_Yonghu(strErrMsg, strUserId, strPassword, strRYDM)
                End With
            Catch ex As Exception
                doDelete_Yonghu = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ͣ��/���ý����û�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strRYDM              ����Ա����
        '     blnValid             ��True-���ã�False-ͣ��
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doValid_Yonghu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRYDM As String, _
            ByVal blnValid As Boolean) As Boolean

            Try
                With m_objrulesLuntan
                    doValid_Yonghu = .doValid_Yonghu(strErrMsg, strUserId, strPassword, strRYDM, blnValid)
                End With
            Catch ex As Exception
                doValid_Yonghu = False
                strErrMsg = ex.Message
            End Try

        End Function






        '----------------------------------------------------------------
        ' ɾ����������(ȫ�����)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doDelete_Jiaoliu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String) As Boolean

            Try
                With m_objrulesLuntan
                    doDelete_Jiaoliu = .doDelete_Jiaoliu(strErrMsg, strUserId, strPassword)
                End With
            Catch ex As Exception
                doDelete_Jiaoliu = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ɾ���������ݣ�ָ��ʱ���
        ' ָ��strQSRQ��strJSRQ��strQSRQ <= �������� <= strJSRQ
        ' ָ��strQSRQ         ��strQSRQ <= ��������
        ' ָ��strJSRQ         ���������� <= strJSRQ
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strQSRQ              ����ʼ����
        '     strJSRQ              ����������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doDelete_Jiaoliu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strQSRQ As String, _
            ByVal strJSRQ As String) As Boolean

            Try
                With m_objrulesLuntan
                    doDelete_Jiaoliu = .doDelete_Jiaoliu(strErrMsg, strUserId, strPassword, strQSRQ, strJSRQ)
                End With
            Catch ex As Exception
                doDelete_Jiaoliu = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ɾ����������(ָ����¼)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     intJLBH              ���������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doDelete_Jiaoliu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intJLBH As Integer) As Boolean

            Try
                With m_objrulesLuntan
                    doDelete_Jiaoliu = .doDelete_Jiaoliu(strErrMsg, strUserId, strPassword, intJLBH)
                End With
            Catch ex As Exception
                doDelete_Jiaoliu = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡ������������(����������Ŀ������)
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     strUserId                   ���û���ʶ
        '     strPassword                 ���û�����
        '     strWhere                    ����������
        '     objLuntanData               ����Ϣ���ݼ�
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function getDataSet_Jiaoliu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objLuntanData As Xydc.Platform.Common.Data.ggxxLuntanData) As Boolean

            Try
                With m_objrulesLuntan
                    getDataSet_Jiaoliu = .getDataSet_Jiaoliu(strErrMsg, strUserId, strPassword, strWhere, objLuntanData)
                End With
            Catch ex As Exception
                getDataSet_Jiaoliu = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡ�����µ���������(�����������ڡ�����)
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     strUserId                   ���û���ʶ
        '     strPassword                 ���û�����
        '     intJLBH                     ��������
        '     strWhere                    ����������
        '     objLuntanData               ����Ϣ���ݼ�
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function getDataSet_Jiaoliu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intJLBH As Integer, _
            ByVal strWhere As String, _
            ByRef objLuntanData As Xydc.Platform.Common.Data.ggxxLuntanData) As Boolean

            Try
                With m_objrulesLuntan
                    getDataSet_Jiaoliu = .getDataSet_Jiaoliu(strErrMsg, strUserId, strPassword, intJLBH, strWhere, objLuntanData)
                End With
            Catch ex As Exception
                getDataSet_Jiaoliu = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡָ����������
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     strUserId                   ���û���ʶ
        '     strPassword                 ���û�����
        '     intJLBH                     ��������
        '     objLuntanData               ����Ϣ���ݼ�
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function getDataSet_Jiaoliu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intJLBH As Integer, _
            ByRef objLuntanData As Xydc.Platform.Common.Data.ggxxLuntanData) As Boolean

            Try
                With m_objrulesLuntan
                    getDataSet_Jiaoliu = .getDataSet_Jiaoliu(strErrMsg, strUserId, strPassword, intJLBH, objLuntanData)
                End With
            Catch ex As Exception
                getDataSet_Jiaoliu = False
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
        Public Function doSave_Jiaoliu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Try
                With m_objrulesLuntan
                    doSave_Jiaoliu = .doSave_Jiaoliu(strErrMsg, strUserId, strPassword, objNewData, objOldData, objenumEditType)
                End With
            Catch ex As Exception
                doSave_Jiaoliu = False
                strErrMsg = ex.Message
            End Try

        End Function





        '----------------------------------------------------------------
        ' ����intJLBH��ȡ��������
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     strUserId                   ���û���ʶ
        '     strPassword                 ���û�����
        '     intJLBH                     ��������
        '     strJLZT                     ��(����)��������
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function getJlztByJlbh( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intJLBH As Integer, _
            ByRef strJLZT As String) As Boolean

            Try
                With m_objrulesLuntan
                    getJlztByJlbh = .getJlztByJlbh(strErrMsg, strUserId, strPassword, intJLBH, strJLZT)
                End With
            Catch ex As Exception
                getJlztByJlbh = False
                strErrMsg = ex.Message
            End Try

        End Function

    End Class

End Namespace
