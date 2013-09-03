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
Imports System.Web
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
    ' ����    ��rulesGonggongziyuan
    '
    ' ���������� 
    '     �ṩ�ԡ�������Դ���漰��ҵ���߼������
    '----------------------------------------------------------------
    Public Class rulesGonggongziyuan
        Implements System.IDisposable

        Private m_objdacGonggongziyuan As Xydc.Platform.DataAccess.dacGonggongziyuan










        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
            m_objdacGonggongziyuan = New Xydc.Platform.DataAccess.dacGonggongziyuan
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
            If Not (m_objdacGonggongziyuan Is Nothing) Then
                m_objdacGonggongziyuan.Dispose()
                m_objdacGonggongziyuan = Nothing
            End If
        End Sub

        '----------------------------------------------------------------
        ' ��ȫ�ͷű�����Դ
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessRules.rulesGonggongziyuan)
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
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doExportToExcel( _
            ByRef strErrMsg As String, _
            ByVal objDataSet As System.Data.DataSet, _
            ByVal strExcelFile As String) As Boolean

            Try
                With m_objdacGonggongziyuan
                    doExportToExcel = .doExportToExcel(strErrMsg, objDataSet, strExcelFile)
                End With
            Catch ex As Exception
                doExportToExcel = False
                strErrMsg = ex.Message
            End Try

        End Function




        '----------------------------------------------------------------
        ' ��ȡ����Ϣ_B_������Դ_��Ŀ�������ݼ�(�ԡ���Ŀ���롱��������)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strWhere             �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     objLanmuData         ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getLanmuData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objLanmuData As Xydc.Platform.Common.Data.ggxxGonggongziyuanData) As Boolean

            Try
                With m_objdacGonggongziyuan
                    getLanmuData = .getLanmuData(strErrMsg, strUserId, strPassword, strWhere, objLanmuData)
                End With
            Catch ex As Exception
                getLanmuData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡָ��strLMDM�¼��ġ���Ϣ_B_������Դ_��Ŀ�������ݼ�(�ԡ���Ŀ���롱��������)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strLMDM              ����Ŀ����
        '     strWhere             �������ַ���(Ĭ�ϱ�ǰ׺a.)
        '     objLanmuData         ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getLanmuData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strLMDM As String, _
            ByVal strWhere As String, _
            ByRef objLanmuData As Xydc.Platform.Common.Data.ggxxGonggongziyuanData) As Boolean

            Try
                With m_objdacGonggongziyuan
                    getLanmuData = .getLanmuData(strErrMsg, strUserId, strPassword, strLMDM, strWhere, objLanmuData)
                End With
            Catch ex As Exception
                getLanmuData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ����ָ��strLMDM��ȡ����Ϣ_B_������Դ_��Ŀ�������ݼ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strLMDM              ����Ŀ����
        '     blnUnused            ��������
        '     objLanmuData         ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getLanmuData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strLMDM As String, _
            ByVal blnUnused As Boolean, _
            ByRef objLanmuData As Xydc.Platform.Common.Data.ggxxGonggongziyuanData) As Boolean

            Try
                With m_objdacGonggongziyuan
                    getLanmuData = .getLanmuData(strErrMsg, strUserId, strPassword, strLMDM, blnUnused, objLanmuData)
                End With
            Catch ex As Exception
                getLanmuData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ����ָ��intMKBS��ȡ����Ϣ_B_������Դ_��Ŀ�������ݼ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     intMKBS              ����Ŀ��ʶ
        '     blnUnused            ��������
        '     objLanmuData         ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getLanmuData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intMKBS As Integer, _
            ByVal blnUnused As Boolean, _
            ByRef objLanmuData As Xydc.Platform.Common.Data.ggxxGonggongziyuanData) As Boolean

            Try
                With m_objdacGonggongziyuan
                    getLanmuData = .getLanmuData(strErrMsg, strUserId, strPassword, intMKBS, blnUnused, objLanmuData)
                End With
            Catch ex As Exception
                getLanmuData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' �����ϼ���Ŀ�����ȡ�¼�����Ŀ����
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strPrevLMDM          ���ϼ���Ŀ����
        '     strNewLMDM           ������Ŀ����(����)
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getNewLMDM( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strPrevLMDM As String, _
            ByRef strNewLMDM As String) As Boolean

            Try
                With m_objdacGonggongziyuan
                    getNewLMDM = .getNewLMDM(strErrMsg, strUserId, strPassword, strPrevLMDM, strNewLMDM)
                End With
            Catch ex As Exception
                getNewLMDM = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡ�µ���Ŀ��ʶ
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strNewLMBS           ������Ŀ��ʶ(����)
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getNewLMBS( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef strNewLMBS As String) As Boolean

            Try
                With m_objdacGonggongziyuan
                    getNewLMBS = .getNewLMBS(strErrMsg, strUserId, strPassword, strNewLMBS)
                End With
            Catch ex As Exception
                getNewLMBS = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ����������ֵ��������ϵͳ�Զ������ֵ
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     objNewData           ��������(����)
        '     objenumEditType      ���༭����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getLanmuDefaultValue( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef objNewData As System.Collections.Specialized.ListDictionary, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Try
                With m_objdacGonggongziyuan
                    getLanmuDefaultValue = .getLanmuDefaultValue(strErrMsg, strUserId, strPassword, objNewData, objenumEditType)
                End With
            Catch ex As Exception
                getLanmuDefaultValue = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���ݡ���Ŀ���ơ���ȡ����Ŀ��ʶ��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strLMMC              ����Ŀ����
        '     strLMBS              ��(����)��Ŀ��ʶ
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getLmbsByLmmc( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strLMMC As String, _
            ByRef strLMBS As String) As Boolean

            Try
                With m_objdacGonggongziyuan
                    getLmbsByLmmc = .getLmbsByLmmc(strErrMsg, strUserId, strPassword, strLMMC, strLMBS)
                End With
            Catch ex As Exception
                getLmbsByLmmc = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���ݡ���Ŀ���ơ���ȡ����Ŀ���롱
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strLMMC              ����Ŀ����
        '     strLMDM              ��(����)��Ŀ����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getLmdmByLmmc( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strLMMC As String, _
            ByRef strLMDM As String) As Boolean

            Try
                With m_objdacGonggongziyuan
                    getLmdmByLmmc = .getLmdmByLmmc(strErrMsg, strUserId, strPassword, strLMMC, strLMDM)
                End With
            Catch ex As Exception
                getLmdmByLmmc = False
                strErrMsg = ex.Message
            End Try

        End Function



        '----------------------------------------------------------------
        ' ���桰��Ϣ_B_������Դ_��Ŀ��������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     objOldData           ��������
        '     objNewData           ��������(����)
        '     objenumEditType      ���༭����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doSaveLanmuData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByRef objNewData As System.Collections.Specialized.ListDictionary, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Try
                With m_objdacGonggongziyuan
                    doSaveLanmuData = .doSaveLanmuData(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType)
                End With
            Catch ex As Exception
                doSaveLanmuData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ������Ŀ����ɾ������Ϣ_B_������Դ_��Ŀ��������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strLMDM              ����Ŀ����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doDeleteLanmuData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strLMDM As String) As Boolean

            Try
                With m_objdacGonggongziyuan
                    doDeleteLanmuData = .doDeleteLanmuData(strErrMsg, strUserId, strPassword, strLMDM)
                End With
            Catch ex As Exception
                doDeleteLanmuData = False
                strErrMsg = ex.Message
            End Try

        End Function




        '----------------------------------------------------------------
        ' ��ȡ[��Ա����=strCzydm]�Ĺ�����Դ���ݣ������������ڡ����򣩣���
        ' �Ҹ��𷢲��Ĺ�����Դ����
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     strUserId                   ���û���ʶ
        '     strPassword                 ���û�����
        '     strCzydm                    ������Ա��ʶ
        '     strWhere                    �������ַ���
        '     objGonggongziyuanData       ����Ϣ���ݼ�
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strCzydm As String, _
            ByVal strWhere As String, _
            ByRef objGonggongziyuanData As Xydc.Platform.Common.Data.ggxxGonggongziyuanData) As Boolean

            Try
                With m_objdacGonggongziyuan
                    getDataSet = .getDataSet(strErrMsg, strUserId, strPassword, strCzydm, strWhere, objGonggongziyuanData)
                End With
            Catch ex As Exception
                getDataSet = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡ[��Դ��ʶ=strZYBS]�Ĺ�����Դ����
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     strUserId                   ���û���ʶ
        '     strPassword                 ���û�����
        '     strZYBS                     ����Դ��ʶ
        '     objGonggongziyuanData       ����Ϣ���ݼ�
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZYBS As String, _
            ByRef objGonggongziyuanData As Xydc.Platform.Common.Data.ggxxGonggongziyuanData) As Boolean

            Try
                With m_objdacGonggongziyuan
                    getDataSet = .getDataSet(strErrMsg, strUserId, strPassword, strZYBS, objGonggongziyuanData)
                End With
            Catch ex As Exception
                getDataSet = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡstrUserId���ܹ��Ķ����ѷ����Ĺ�����Դ���ݣ������������ڡ�����
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     strUserId                   ���û���ʶ
        '     strPassword                 ���û�����
        '     strWhere                    �������ַ���
        '     blnUnused                   ��������
        '     objGonggongziyuanData       ����Ϣ���ݼ�
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByVal blnUnused As Boolean, _
            ByRef objGonggongziyuanData As Xydc.Platform.Common.Data.ggxxGonggongziyuanData) As Boolean

            Try
                With m_objdacGonggongziyuan
                    getDataSet = .getDataSet(strErrMsg, strUserId, strPassword, strWhere, blnUnused, objGonggongziyuanData)
                End With
            Catch ex As Exception
                getDataSet = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡ[��Դ��ʶ=strZYBS]�Ĺ�����Դ�������Ķ���Ա����
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     strUserId                   ���û���ʶ
        '     strPassword                 ���û�����
        '     strZYBS                     ����Դ��ʶ
        '     strYDRYMC                   �������أ������Ķ���Ա����(��Ա����)
        '     strYDRYDM                   �������أ������Ķ���Ա����(��Ա����)
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function getKeYueduRenyuan( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZYBS As String, _
            ByRef strYDRYMC As String, _
            ByRef strYDRYDM As String) As Boolean

            Try
                With m_objdacGonggongziyuan
                    getKeYueduRenyuan = .getKeYueduRenyuan(strErrMsg, strUserId, strPassword, strZYBS, strYDRYMC, strYDRYDM)
                End With
            Catch ex As Exception
                getKeYueduRenyuan = False
                strErrMsg = ex.Message
            End Try

        End Function




        '----------------------------------------------------------------
        ' ȡ���ѷ����Ĺ�����Դ �� ����������Դ
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strZYBS              ����Դ��ʶ
        '     blnFabu              ��True-������False-ȡ������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doFabu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZYBS As String, _
            ByVal blnFabu As Boolean) As Boolean

            Try
                With m_objdacGonggongziyuan
                    doFabu = .doFabu(strErrMsg, strUserId, strPassword, strZYBS, blnFabu)
                End With
            Catch ex As Exception
                doFabu = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���á��Ѿ��Ķ���
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strZYBS              ����Դ��ʶ
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doSetHasRead( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZYBS As String) As Boolean

            Try
                With m_objdacGonggongziyuan
                    doSetHasRead = .doSetHasRead(strErrMsg, strUserId, strPassword, strZYBS)
                End With
            Catch ex As Exception
                doSetHasRead = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ɾ��������Դ
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strZYBS              ����Դ��ʶ
        '     strAppRoot           ��Ӧ�ø�Http·��(����/)
        '     objServer            ������������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doDelete( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZYBS As String, _
            ByVal strAppRoot As String, _
            ByVal objServer As System.Web.HttpServerUtility) As Boolean

            Try
                With m_objdacGonggongziyuan
                    doDelete = .doDelete(strErrMsg, strUserId, strPassword, strZYBS, strAppRoot, objServer)
                End With
            Catch ex As Exception
                doDelete = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���湫����Դ���ݼ�¼(�����������)
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strUserId              ���û���ʶ
        '     strPassword            ���û�����
        '     objNewData             ����¼��ֵ(���ر�������ֵ)
        '     objOldData             ����¼��ֵ
        '     strFBFW                ��������Χ
        '     objenumEditType        ���༭����
        '     strUploadFile          �������ļ���WEB������ȫ·��
        '     strAppRoot             ��Ӧ�ø�Http·��(����/)
        '     strBasePath            ����Ӧ�ø�����ŵص����HTTPĿ¼(��ͷ����/)
        '     objServer              ������������
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
            ByVal strFBFW As String, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType, _
            ByVal strUploadFile As String, _
            ByVal strAppRoot As String, _
            ByVal strBasePath As String, _
            ByVal objServer As System.Web.HttpServerUtility) As Boolean

            Try
                With m_objdacGonggongziyuan
                    doSave = .doSave(strErrMsg, strUserId, strPassword, objNewData, objOldData, strFBFW, objenumEditType, strUploadFile, strAppRoot, strBasePath, objServer)
                End With
            Catch ex As Exception
                doSave = False
                strErrMsg = ex.Message
            End Try

        End Function






        '----------------------------------------------------------------
        ' �ж�strUserId�Ƿ��ܹ��Ķ����ѷ�����strZYBS������Դ����
        '     strErrMsg                   ����������򷵻ش�����Ϣ
        '     strUserId                   ���û���ʶ
        '     strPassword                 ���û�����
        '     strZYBS                     ����Դ��ʶ
        '     blnYuedu                    �������أ�True-��,False-����
        ' ����
        '     True                        ���ɹ�
        '     False                       ��ʧ��
        '----------------------------------------------------------------
        Public Function isCanRead( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZYBS As String, _
            ByRef blnYuedu As Boolean) As Boolean

            Try
                With m_objdacGonggongziyuan
                    isCanRead = .isCanRead(strErrMsg, strUserId, strPassword, strZYBS, blnYuedu)
                End With
            Catch ex As Exception
                isCanRead = False
                strErrMsg = ex.Message
            End Try

        End Function

    End Class 'rulesGonggongziyuan

End Namespace 'Xydc.Platform.BusinessRules
