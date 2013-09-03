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
    ' ����    ��systemFenfafanwei
    '
    ' ���������� 
    '   ���ṩ�ԡ�����_B_�ַ���Χ����Ϣ����ı��ֲ�֧��
    '----------------------------------------------------------------
    Public Class systemFenfafanwei
        Inherits MarshalByRefObject







        '----------------------------------------------------------------
        ' ��ȫ�ͷű�����Դ
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.systemFenfafanwei)
            Try
                If Not (obj Is Nothing) Then
                    'obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub









        '----------------------------------------------------------------
        ' ��ȡ������_B_�ַ���Χ������¼�����ݼ�(�Է�Χ������������)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strWhere             ����������(Ĭ�ϱ�ǰ׺a.)
        '     objFenfafanweiData   ���ַ���Χ��Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getFenfafanweiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objFenfafanweiData As Xydc.Platform.Common.Data.FenfafanweiData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesFenfafanwei
                    getFenfafanweiData = .getFenfafanweiData(strErrMsg, strUserId, strPassword, strWhere, objFenfafanweiData)
                End With
            Catch ex As Exception
                getFenfafanweiData = False
                strErrMsg = ex.Message
            End Try

        End Function



        '----------------------------------------------------------------
        ' ��ȡָ����Ա�ļ��뷶Χ�����ݼ�(�Գ�Աλ����������)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strWhere             ����������(Ĭ�ϱ�ǰ׺a.)
        '     objFenfafanweiData   ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��

        '----------------------------------------------------------------
        Public Function getFenfafanweiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByVal strWhere As String, _
            ByRef objFenfafanweiData As Xydc.Platform.Common.Data.FenfafanweiData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesFenfafanwei
                    getFenfafanweiData = .getFenfafanweiData(strErrMsg, strUserId, strPassword, strUserXM, strWhere, objFenfafanweiData)
                End With
            Catch ex As Exception
                getFenfafanweiData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡָ����Ա�ļ��뷶Χ���ݼ�(�Է�Χ������������)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strWhere             ����������(Ĭ�ϱ�ǰ׺a.)
        '     objFenfafanweiData   ����Ϣ���ݼ�
        '     blnNone              ��������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��

        '----------------------------------------------------------------
        Public Function getFenfafanweiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objFenfafanweiData As Xydc.Platform.Common.Data.FenfafanweiData, _
            ByVal blnNone As Boolean) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesFenfafanwei
                    getFenfafanweiData = .getFenfafanweiData(strErrMsg, strUserId, strPassword, strWhere, objFenfafanweiData, blnNone)
                End With
            Catch ex As Exception
                getFenfafanweiData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���桰����_B_�ַ���Χ��������(��Χ����¼)
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
        Public Function doSaveFenfafanweiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            doSaveFenfafanweiData = False
            Try
                With New Xydc.Platform.BusinessRules.rulesFenfafanwei
                    doSaveFenfafanweiData = .doSaveFenfafanweiData(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ɾ��������_B_�ַ���Χ��������(��Χ����¼)��ͬʱɾ����Ա��¼
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     objOldData           ��������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doDeleteFenfafanweiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesFenfafanwei
                    doDeleteFenfafanweiData = .doDeleteFenfafanweiData(strErrMsg, strUserId, strPassword, objOldData)
                End With
            Catch ex As Exception
                doDeleteFenfafanweiData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���桰����_B_�ַ���Χ��������(��Χ��Ա��¼)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     objOldData           ��������
        '     objNewData           ��������
        '     blnIsFWCY            �������ӿ�����ʹ��
        '     objenumEditType      ���༭����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doSaveFenfafanweiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal blnIsFWCY As Boolean, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            doSaveFenfafanweiData = False
            Try
                With New Xydc.Platform.BusinessRules.rulesFenfafanwei
                    doSaveFenfafanweiData = .doSaveFenfafanweiData(strErrMsg, strUserId, strPassword, objOldData, objNewData, blnIsFWCY, objenumEditType)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ɾ��������_B_�ַ���Χ��������(��Χ��Ա��¼)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     objOldData           ��������
        '     blnIsFWCY            �������ӿ�����ʹ��
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doDeleteFenfafanweiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal blnIsFWCY As Boolean) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesFenfafanwei
                    doDeleteFenfafanweiData = .doDeleteFenfafanweiData(strErrMsg, strUserId, strPassword, objOldData, blnIsFWCY)
                End With
            Catch ex As Exception
                doDeleteFenfafanweiData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���桰����_B_�ַ���Χ��������(����Ա���뼸�����÷�Χ��)
        '     strErrMsg                 ����������򷵻ش�����Ϣ
        '     strUserId                 ���û���ʶ
        '     strPassword               ���û�����
        '     objDataSet_ChoiceCYFW     ���·�Χ����
        '     objNewData                ���³�Ա����
        '     objOldDataSet_ChoiceCYFW  ���ɷ�Χ����
        ' ����
        '     True                      ���ɹ�
        '     False                     ��ʧ��

        '----------------------------------------------------------------
        Public Function doSaveFenfafanweiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objDataSet_ChoiceCYFW As Xydc.Platform.Common.Data.FenfafanweiData, _
            ByVal objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objOldDataSet_ChoiceCYFW As Xydc.Platform.Common.Data.FenfafanweiData) As Boolean
            doSaveFenfafanweiData = False
            Try
                With New Xydc.Platform.BusinessRules.rulesFenfafanwei
                    doSaveFenfafanweiData = .doSaveFenfafanweiData(strErrMsg, strUserId, strPassword, objDataSet_ChoiceCYFW, objNewData, objOldDataSet_ChoiceCYFW)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡ�µġ�����_B_�ַ���Χ���ĳ�Աλ��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strFWMC              ����ǰ��Χ����
        '     intCYWZ              ���µĳ�Աλ��(����)
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getNewCYWZ( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strFWMC As String, _
            ByRef intCYWZ As Integer) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesFenfafanwei
                    getNewCYWZ = .getNewCYWZ(strErrMsg, strUserId, strPassword, strFWMC, intCYWZ)
                End With
            Catch ex As Exception
                getNewCYWZ = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ָ����Χ�ڵ�ָ����Աλ������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     objChengyuanData     ����Ա����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doChengyuanMoveUp( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objChengyuanData As System.Data.DataRow) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesFenfafanwei
                    doChengyuanMoveUp = .doChengyuanMoveUp(strErrMsg, strUserId, strPassword, objChengyuanData)
                End With
            Catch ex As Exception
                doChengyuanMoveUp = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ָ����Χ�ڵ�ָ����Աλ������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     objChengyuanData     ����Ա����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doChengyuanMoveDown( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objChengyuanData As System.Data.DataRow) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesFenfafanwei
                    doChengyuanMoveDown = .doChengyuanMoveDown(strErrMsg, strUserId, strPassword, objChengyuanData)
                End With
            Catch ex As Exception
                doChengyuanMoveDown = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ָ����Χ�ڵ�ָ����ԱobjChengyuanDataλ���ƶ���objChengyuanDataTo
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     objChengyuanData     ��׼���ƶ��ĳ�Ա����
        '     objChengyuanDataTo   ���ƶ����ĳ�Ա����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doChengyuanMoveTo( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objChengyuanData As System.Data.DataRow, _
            ByVal objChengyuanDataTo As System.Data.DataRow) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesFenfafanwei
                    doChengyuanMoveTo = .doChengyuanMoveTo(strErrMsg, strUserId, strPassword, objChengyuanData, objChengyuanDataTo)
                End With
            Catch ex As Exception
                doChengyuanMoveTo = False
                strErrMsg = ex.Message
            End Try

        End Function

    End Class

End Namespace
