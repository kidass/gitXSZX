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

Imports Microsoft.VisualBasic

Imports System
Imports System.Data
Imports System.Data.SqlTypes
Imports System.Data.SqlClient

Imports Xydc.Platform.Common
Imports Xydc.Platform.Common.Data
Imports Xydc.Platform.SystemFramework

Namespace Xydc.Platform.DataAccess

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.DataAccess
    ' ����    ��dacMyTask
    '
    ' ����������
    '     �ṩ�ԡ��ҵ����ˡ�ģ���漰�����ݲ����
    '----------------------------------------------------------------

    Public Class dacMyTask
        Implements IDisposable

        Private m_objSqlDataAdapter As System.Data.SqlClient.SqlDataAdapter








        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
            m_objSqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter
        End Sub

        '----------------------------------------------------------------
        ' ������������
        '----------------------------------------------------------------
        Public Sub Dispose() Implements IDisposable.Dispose
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
            If Not m_objSqlDataAdapter Is Nothing Then
                m_objSqlDataAdapter.Dispose()
                m_objSqlDataAdapter = Nothing
            End If
        End Sub

        '----------------------------------------------------------------
        ' ��ȫ�ͷű�����Դ
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.DataAccess.dacMyTask)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub










        '----------------------------------------------------------------
        ' SqlDataAdapter����
        '----------------------------------------------------------------
        Protected ReadOnly Property SqlDataAdapter() As System.Data.SqlClient.SqlDataAdapter
            Get
                SqlDataAdapter = m_objSqlDataAdapter
            End Get
        End Property









        '----------------------------------------------------------------
        ' ��ȡ������_B_�ҵ�����_�ڵ㡱�Ĺ������ڵ�����ݼ�
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strUserId              ���û���ʶ
        '     strPassword            ���û�����
        '     objParent              ���ϼ��ڵ�����
        '     intParentLevel         ���ϼ��ڵ㼶��(1,...)
        '     objgrswMyTaskData      ����Ϣ���ݼ�
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Private Function getMyTaskNodeDataFlow( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objParent As System.Data.DataRow, _
            ByVal intParentLevel As Integer, _
            ByRef objgrswMyTaskData As Xydc.Platform.Common.Data.grswMyTaskData) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objFlowTypeName As System.Collections.Specialized.NameValueCollection
            Dim objFlowTypeBLLX As System.Collections.Specialized.NameValueCollection

            getMyTaskNodeDataFlow = False

            Try
                Dim objDataRow As System.Data.DataRow
                Dim strPrevCode As String
                Dim strKSSJ As String
                Dim strJSSJ As String
                Dim strCode As String

                '��ȡ�ϼ���Ϣ
                strPrevCode = objPulicParameters.getObjectValue(objParent.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_CODE), "")
                strKSSJ = objPulicParameters.getObjectValue(objParent.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ), "")
                strJSSJ = objPulicParameters.getObjectValue(objParent.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ), "")

                '��ȡ���������ͼ���(0,...)
                objFlowTypeName = Xydc.Platform.DataAccess.FlowObject.FlowTypeNameCollection
                objFlowTypeBLLX = Xydc.Platform.DataAccess.FlowObject.FlowTypeBLLXCollection

                '�������
                Dim intCount As Integer
                Dim i As Integer
                With objgrswMyTaskData.Tables(Xydc.Platform.Common.Data.grswMyTaskData.TABLE_GR_B_MYTASK_NODE)
                    intCount = objFlowTypeName.Count
                    For i = 1 To intCount Step 1
                        strCode = Right("00" + i.ToString(), Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(intParentLevel) - Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(intParentLevel - 1))
                        strCode = strPrevCode + strCode

                        objDataRow = .NewRow()

                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_CODE) = strCode
                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = objFlowTypeName(i - 1)
                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_WJLX) = objFlowTypeName(i - 1)
                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_BLLX) = objFlowTypeBLLX(i - 1)
                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = strKSSJ
                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = strJSSJ

                        .Rows.Add(objDataRow)
                    Next
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFlowTypeName)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFlowTypeBLLX)

            getMyTaskNodeDataFlow = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFlowTypeName)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objFlowTypeBLLX)
            Exit Function

        End Function

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

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objTempgrswMyTaskData As Xydc.Platform.Common.Data.grswMyTaskData

            '��ʼ��
            getMyTaskNodeData = False
            objgrswMyTaskData = Nothing
            strErrMsg = ""

            Try
                If strUserId Is Nothing Then strUserId = ""
                If strPassword Is Nothing Then strPassword = ""
                strUserId = strUserId.Trim()
                strPassword = strPassword.Trim()

                '���
                If strUserId.Trim = "" Then
                    strErrMsg = "����δָ��Ҫ��ȡ��Ϣ���û���"
                    GoTo errProc
                End If

                '��ȡ����
                Try
                    '�������ݼ�
                    objTempgrswMyTaskData = New Xydc.Platform.Common.Data.grswMyTaskData(Xydc.Platform.Common.Data.grswMyTaskData.enumTableType.GR_B_MYTASK_NODE)

                    '��������
                    Dim objDataRow As System.Data.DataRow
                    With objTempgrswMyTaskData.Tables(Xydc.Platform.Common.Data.grswMyTaskData.TABLE_GR_B_MYTASK_NODE)
                        Dim objMonthStart As System.DateTime
                        Dim objMonthEnd As System.DateTime
                        Dim objWeekStart As System.DateTime
                        Dim objWeekEnd As System.DateTime
                        Dim strCode As String
                        Dim i As Integer
                        Dim j As Integer
                        For i = 1 To 11 Step 1
                            objDataRow = .NewRow()
                            strCode = Right("00" + i.ToString(), Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(0))
                            objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_CODE) = strCode
                            objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = ""
                            objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = ""
                            objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_WJLX) = ""
                            objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_BLLX) = ""

                            Select Case i
                                Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.DBSY
                                    '����
                                    objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "�ҵ�δ������"
                                    .Rows.Add(objDataRow)

                                    If objPulicParameters.getMonthStartAndEndDay(strErrMsg, Now, objMonthStart, objMonthEnd) = False Then
                                        GoTo errProc
                                    End If
                                    If objPulicParameters.getWeekStartAndEndDay(strErrMsg, Now, objWeekStart, objWeekEnd) = False Then
                                        GoTo errProc
                                    End If

                                    For j = 1 To 4 Step 1
                                        '����
                                        objDataRow = .NewRow()
                                        strCode = Right("00" + i.ToString(), Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(0)) + Right("00" + j.ToString(), Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(1) - Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(0))
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_CODE) = strCode
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_WJLX) = ""
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_BLLX) = ""
                                        Select Case j
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.JINTIAN
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "�����յ���"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Now.ToString("yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Now.ToString("yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENZHOU
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "�����յ���"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Format(objWeekStart, "yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Format(objWeekEnd, "yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENYUEN
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "�����յ���"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Format(objMonthStart, "yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Format(objMonthEnd, "yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENYUES
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "������ǰ�յ���"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = ""
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Format(objMonthStart, "yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                        End Select

                                        '����
                                        If getMyTaskNodeDataFlow(strErrMsg, strUserId, strPassword, objDataRow, 2, objTempgrswMyTaskData) = False Then
                                            GoTo errProc
                                        End If
                                    Next

                                Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.DPWJ
                                    objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "�ҵĴ����ļ�"
                                    .Rows.Add(objDataRow)

                                    If objPulicParameters.getMonthStartAndEndDay(strErrMsg, Now, objMonthStart, objMonthEnd) = False Then
                                        GoTo errProc
                                    End If
                                    If objPulicParameters.getWeekStartAndEndDay(strErrMsg, Now, objWeekStart, objWeekEnd) = False Then
                                        GoTo errProc
                                    End If

                                    For j = 1 To 4 Step 1
                                        '����
                                        objDataRow = .NewRow()
                                        strCode = Right("00" + i.ToString(), Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(0)) + Right("00" + j.ToString(), Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(1) - Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(0))
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_CODE) = strCode
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_WJLX) = ""
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_BLLX) = ""
                                        Select Case j
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.JINTIAN
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "�����ͳ���"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Now.ToString("yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Now.ToString("yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENZHOU
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "�����ͳ���"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Format(objWeekStart, "yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Format(objWeekEnd, "yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENYUEN
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "�����ͳ���"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Format(objMonthStart, "yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Format(objMonthEnd, "yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENYUES
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "������ǰ�ͳ���"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = ""
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Format(objMonthStart, "yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                        End Select

                                        '����
                                        If getMyTaskNodeDataFlow(strErrMsg, strUserId, strPassword, objDataRow, 2, objTempgrswMyTaskData) = False Then
                                            GoTo errProc
                                        End If
                                    Next

                                Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.HBWJ
                                    objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "�ҵĻ����ļ�"
                                    .Rows.Add(objDataRow)

                                    If objPulicParameters.getMonthStartAndEndDay(strErrMsg, Now, objMonthStart, objMonthEnd) = False Then
                                        GoTo errProc
                                    End If
                                    If objPulicParameters.getWeekStartAndEndDay(strErrMsg, Now, objWeekStart, objWeekEnd) = False Then
                                        GoTo errProc
                                    End If

                                    For j = 1 To 4 Step 1
                                        '����
                                        objDataRow = .NewRow()
                                        strCode = Right("00" + i.ToString(), Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(0)) + Right("00" + j.ToString(), Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(1) - Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(0))
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_CODE) = strCode
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_WJLX) = ""
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_BLLX) = ""
                                        Select Case j
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.JINTIAN
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "���컺���"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Now.ToString("yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Now.ToString("yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENZHOU
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "���ܻ����"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Format(objWeekStart, "yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Format(objWeekEnd, "yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENYUEN
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "���»����"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Format(objMonthStart, "yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Format(objMonthEnd, "yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENYUES
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "������ǰ�����"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = ""
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Format(objMonthStart, "yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                        End Select

                                        '����
                                        If getMyTaskNodeDataFlow(strErrMsg, strUserId, strPassword, objDataRow, 2, objTempgrswMyTaskData) = False Then
                                            GoTo errProc
                                        End If
                                    Next

                                Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.YBSY
                                    objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "�ҵ��Ѱ�����"
                                    .Rows.Add(objDataRow)

                                    If objPulicParameters.getMonthStartAndEndDay(strErrMsg, Now, objMonthStart, objMonthEnd) = False Then
                                        GoTo errProc
                                    End If
                                    If objPulicParameters.getWeekStartAndEndDay(strErrMsg, Now, objWeekStart, objWeekEnd) = False Then
                                        GoTo errProc
                                    End If

                                    For j = 1 To 4 Step 1
                                        '����
                                        objDataRow = .NewRow()
                                        strCode = Right("00" + i.ToString(), Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(0)) + Right("00" + j.ToString(), Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(1) - Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(0))
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_CODE) = strCode
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_WJLX) = ""
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_BLLX) = ""
                                        Select Case j
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.JINTIAN
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "��������"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Now.ToString("yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Now.ToString("yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENZHOU
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "���ܰ����"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Format(objWeekStart, "yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Format(objWeekEnd, "yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENYUEN
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "���°����"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Format(objMonthStart, "yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Format(objMonthEnd, "yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENYUES
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "������ǰ�����"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = ""
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Format(objMonthStart, "yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                        End Select

                                        '����
                                        If getMyTaskNodeDataFlow(strErrMsg, strUserId, strPassword, objDataRow, 2, objTempgrswMyTaskData) = False Then
                                            GoTo errProc
                                        End If
                                    Next

                                Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.GQSY
                                    objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "�ҵĹ�������"
                                    .Rows.Add(objDataRow)

                                    If objPulicParameters.getMonthStartAndEndDay(strErrMsg, Now, objMonthStart) = False Then
                                        GoTo errProc
                                    End If
                                    If objPulicParameters.getWeekStartAndEndDay(strErrMsg, Now, objWeekStart) = False Then
                                        GoTo errProc
                                    End If

                                    For j = 1 To 4 Step 1
                                        '
                                        'KSSJ<JSSJ��(now - ��������) <= (JSSJ-KSSJ) and now >= ��������
                                        'KSSJ>JSSJ��(now - ��������) >  (KSSJ-JSSJ) and now >= ��������
                                        '
                                        '����
                                        objDataRow = .NewRow()
                                        strCode = Right("00" + i.ToString(), Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(0)) + Right("00" + j.ToString(), Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(1) - Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(0))
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_CODE) = strCode
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_WJLX) = ""
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_BLLX) = ""
                                        Select Case j
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.JINTIAN
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "����չ��ڵ�"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Format(Now.AddDays(-1), "yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Now.ToString("yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENZHOU
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "���ڲ���һ�ܵ�"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Format(Now.AddDays(-7), "yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Now.ToString("yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENYUEN
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "���ڲ���һ�µ�"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Format(Now.AddDays(-30), "yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Now.ToString("yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENYUES
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "����һ�����ϵ�"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Now.ToString("yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Format(Now.AddDays(-30), "yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                        End Select

                                        '����
                                        If getMyTaskNodeDataFlow(strErrMsg, strUserId, strPassword, objDataRow, 2, objTempgrswMyTaskData) = False Then
                                            GoTo errProc
                                        End If
                                    Next

                                Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.CBSY
                                    objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "�Ҵ߰������"
                                    .Rows.Add(objDataRow)

                                    If objPulicParameters.getMonthStartAndEndDay(strErrMsg, Now, objMonthStart) = False Then
                                        GoTo errProc
                                    End If
                                    If objPulicParameters.getWeekStartAndEndDay(strErrMsg, Now, objWeekStart) = False Then
                                        GoTo errProc
                                    End If

                                    For j = 1 To 4 Step 1
                                        '
                                        'KSSJ<JSSJ��(now - �߰�����) <= (JSSJ-KSSJ) and now >= �߰�����
                                        'KSSJ>JSSJ��(now - �߰�����) >  (KSSJ-JSSJ) and now >= �߰�����
                                        '
                                        '����
                                        objDataRow = .NewRow()
                                        strCode = Right("00" + i.ToString(), Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(0)) + Right("00" + j.ToString(), Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(1) - Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(0))
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_CODE) = strCode
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_WJLX) = ""
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_BLLX) = ""
                                        Select Case j
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.JINTIAN
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "����߰��"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Format(Now.AddDays(-1), "yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Now.ToString("yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENZHOU
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "�߰첻��һ�ܵ�"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Format(Now.AddDays(-7), "yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Now.ToString("yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENYUEN
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "�߰첻��һ�µ�"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Format(Now.AddDays(-30), "yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Now.ToString("yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENYUES
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "�߰�һ�����ϵ�"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Now.ToString("yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Format(Now.AddDays(-30), "yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                        End Select

                                        '����
                                        If getMyTaskNodeDataFlow(strErrMsg, strUserId, strPassword, objDataRow, 2, objTempgrswMyTaskData) = False Then
                                            GoTo errProc
                                        End If
                                    Next

                                Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.BCSY
                                    objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "�ұ��ߵ�����"
                                    .Rows.Add(objDataRow)

                                    If objPulicParameters.getMonthStartAndEndDay(strErrMsg, Now, objMonthStart) = False Then
                                        GoTo errProc
                                    End If
                                    If objPulicParameters.getWeekStartAndEndDay(strErrMsg, Now, objWeekStart) = False Then
                                        GoTo errProc
                                    End If

                                    For j = 1 To 4 Step 1
                                        '
                                        'KSSJ<JSSJ��(now - �߰�����) <= (JSSJ-KSSJ) and now >= �߰�����
                                        'KSSJ>JSSJ��(now - �߰�����) >  (KSSJ-JSSJ) and now >= �߰�����
                                        '
                                        '����
                                        objDataRow = .NewRow()
                                        strCode = Right("00" + i.ToString(), Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(0)) + Right("00" + j.ToString(), Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(1) - Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(0))
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_CODE) = strCode
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_WJLX) = ""
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_BLLX) = ""
                                        Select Case j
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.JINTIAN
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "���챻�߰��"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Format(Now.AddDays(-1), "yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Now.ToString("yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENZHOU
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "���߰첻��һ�ܵ�"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Format(Now.AddDays(-7), "yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Now.ToString("yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENYUEN
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "���߰첻��һ�µ�"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Format(Now.AddDays(-30), "yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Now.ToString("yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENYUES
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "���߰�һ�����ϵ�"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Now.ToString("yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Format(Now.AddDays(-30), "yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                        End Select

                                        '����
                                        If getMyTaskNodeDataFlow(strErrMsg, strUserId, strPassword, objDataRow, 2, objTempgrswMyTaskData) = False Then
                                            GoTo errProc
                                        End If
                                    Next

                                Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.DBWJ
                                    objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "�Ҷ��������"
                                    .Rows.Add(objDataRow)

                                    If objPulicParameters.getMonthStartAndEndDay(strErrMsg, Now, objMonthStart) = False Then
                                        GoTo errProc
                                    End If
                                    If objPulicParameters.getWeekStartAndEndDay(strErrMsg, Now, objWeekStart) = False Then
                                        GoTo errProc
                                    End If

                                    For j = 1 To 4 Step 1
                                        '
                                        'KSSJ<JSSJ��(now - ��������) <= (JSSJ-KSSJ) and now >= ��������
                                        'KSSJ>JSSJ��(now - ��������) >  (KSSJ-JSSJ) and now >= ��������
                                        '
                                        '����
                                        objDataRow = .NewRow()
                                        strCode = Right("00" + i.ToString(), Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(0)) + Right("00" + j.ToString(), Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(1) - Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(0))
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_CODE) = strCode
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_WJLX) = ""
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_BLLX) = ""
                                        Select Case j
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.JINTIAN
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "���춽���"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Format(Now.AddDays(-1), "yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Now.ToString("yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENZHOU
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "���첻��һ�ܵ�"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Format(Now.AddDays(-7), "yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Now.ToString("yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENYUEN
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "���첻��һ�µ�"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Format(Now.AddDays(-30), "yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Now.ToString("yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENYUES
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "����һ�����ϵ�"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Now.ToString("yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Format(Now.AddDays(-30), "yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                        End Select

                                        '����
                                        If getMyTaskNodeDataFlow(strErrMsg, strUserId, strPassword, objDataRow, 2, objTempgrswMyTaskData) = False Then
                                            GoTo errProc
                                        End If
                                    Next

                                Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.BDWJ
                                    objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "�ұ���������"
                                    .Rows.Add(objDataRow)

                                    If objPulicParameters.getMonthStartAndEndDay(strErrMsg, Now, objMonthStart) = False Then
                                        GoTo errProc
                                    End If
                                    If objPulicParameters.getWeekStartAndEndDay(strErrMsg, Now, objWeekStart) = False Then
                                        GoTo errProc
                                    End If

                                    For j = 1 To 4 Step 1
                                        '
                                        'KSSJ<JSSJ��(now - ��������) <= (JSSJ-KSSJ) and now >= ��������
                                        'KSSJ>JSSJ��(now - ��������) >  (KSSJ-JSSJ) and now >= ��������
                                        '
                                        '����
                                        objDataRow = .NewRow()
                                        strCode = Right("00" + i.ToString(), Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(0)) + Right("00" + j.ToString(), Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(1) - Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(0))
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_CODE) = strCode
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_WJLX) = ""
                                        objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_BLLX) = ""
                                        Select Case j
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.JINTIAN
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "���챻�����"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Format(Now.AddDays(-1), "yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Now.ToString("yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENZHOU
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "�����첻��һ�ܵ�"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Format(Now.AddDays(-7), "yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Now.ToString("yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENYUEN
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "�����첻��һ�µ�"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Format(Now.AddDays(-30), "yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Now.ToString("yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                            Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel2.BENYUES
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "������һ�����ϵ�"
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ) = Now.ToString("yyyy-MM-dd")
                                                objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ) = Format(Now.AddDays(-30), "yyyy-MM-dd")
                                                .Rows.Add(objDataRow)
                                        End Select

                                        '����
                                        If getMyTaskNodeDataFlow(strErrMsg, strUserId, strPassword, objDataRow, 2, objTempgrswMyTaskData) = False Then
                                            GoTo errProc
                                        End If
                                    Next

                                Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.QBSY
                                    objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "�ҵ�ȫ������"
                                    .Rows.Add(objDataRow)

                                    '����
                                    If getMyTaskNodeDataFlow(strErrMsg, strUserId, strPassword, objDataRow, 1, objTempgrswMyTaskData) = False Then
                                        GoTo errProc
                                    End If

                                Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.BWTX
                                    objDataRow.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_NAME) = "�ҵı�������"
                                    .Rows.Add(objDataRow)

                                    '����
                                    If getMyTaskNodeDataFlow(strErrMsg, strUserId, strPassword, objDataRow, 1, objTempgrswMyTaskData) = False Then
                                        GoTo errProc
                                    End If
                            End Select
                        Next
                    End With

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
                If objTempgrswMyTaskData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Data.grswMyTaskData.SafeRelease(objTempgrswMyTaskData)
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            '����
            objgrswMyTaskData = objTempgrswMyTaskData
            getMyTaskNodeData = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Data.grswMyTaskData.SafeRelease(objTempgrswMyTaskData)
            Exit Function

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

            getMyTaskNodeData = False
            objNodeData = Nothing

            Try
                With objgrswMyTaskData.Tables(Xydc.Platform.Common.Data.grswMyTaskData.TABLE_GR_B_MYTASK_NODE)
                    .DefaultView.RowFilter = Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_CODE + " = '" + strCode + "'"
                    If .DefaultView.Count > 0 Then
                        objNodeData = .DefaultView.Item(0).Row
                    End If
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getMyTaskNodeData = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �����ļ����͡���ʼ���ڡ��������ڼ���δ�����˵��ļ�����SQL
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strUserXM              ����ǰ������Ա����
        '     strBLLX                ����������
        '     strWJLX                ���ļ�����-����������
        '     strQSRQ                ����ʼ����
        '     strZZRQ                ����������
        '     strWhere               ����������
        '     strSQL                 ��(����)�ļ�����SQL
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Private Function getSQLDBSY_FILE( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByVal strBLLX As String, _
            ByVal strWJLX As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String, _
            ByVal strWhere As String, _
            ByRef strSQL As String) As Boolean

            getSQLDBSY_FILE = False
            strSQL = ""

            Try
                Dim strFileAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.FileStatusAllYWCList
                Dim strTaskAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TaskStatusAllYWCList
                Dim strLF As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhLf
                Dim strRT As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhRt

                '��ʼ������
                If strQSRQ Is Nothing Then strQSRQ = ""
                strQSRQ = strQSRQ.Trim
                If strQSRQ <> "" Then
                    If strQSRQ.IndexOf(" ") < 0 Then
                        strQSRQ = strQSRQ + " 00:00:00"
                    End If
                End If
                If strZZRQ Is Nothing Then strZZRQ = ""
                strZZRQ = strZZRQ.Trim
                If strZZRQ <> "" Then
                    If strZZRQ.IndexOf(" ") < 0 Then
                        strZZRQ = strZZRQ + " 23:59:59"
                    End If
                End If

                '�ҵ��ļ�
                strSQL = ""
                strSQL = strSQL + " select a.*" + vbCr
                strSQL = strSQL + " from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select" + vbCr
                strSQL = strSQL + "     a.�ļ���ʶ, a.��ˮ��  ," + vbCr
                strSQL = strSQL + "     a.��������, a.����״̬, a.�ļ�����, a.�ļ�����," + vbCr
                strSQL = strSQL + "     a.�ļ�����, a.���͵�λ, a.�ļ��ֺ�, a.�����̶�, a.���ܵȼ�," + vbCr
                strSQL = strSQL + "     a.���ش���, a.�ļ����, a.�ļ����," + vbCr
                strSQL = strSQL + "     a.�����  , a.���쵥λ, a.�����  , a.�������,a.��������," + vbCr
                strSQL = strSQL + "     �������� = max(a.��������)," + vbCr
                strSQL = strSQL + "     �������� = max(a.��������)," + vbCr
                strSQL = strSQL + "     ������� = max(a.�������)," + vbCr
                strSQL = strSQL + "     a.��������" + vbCr
                strSQL = strSQL + "   from" + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select" + vbCr
                strSQL = strSQL + "       a.�ļ���ʶ, b.��ˮ��  ," + vbCr
                strSQL = strSQL + "       a.��������, b.����״̬, b.�ļ�����, b.�ļ�����," + vbCr
                strSQL = strSQL + "       b.�ļ�����, b.���͵�λ, b.�ļ��ֺ�, b.�����̶�, b.���ܵȼ�," + vbCr
                strSQL = strSQL + "       b.���ش���, b.�ļ����, b.�ļ����," + vbCr
                strSQL = strSQL + "       b.�����  , b.���쵥λ, b.�����  , b.�������," + vbCr
                strSQL = strSQL + "       a.��������, a.��������, a.�������, b.��������," + vbCr
                strSQL = strSQL + "       �������� = case when c.�������� is null then '��' else c.�������� end" + vbCr
                strSQL = strSQL + "     from" + vbCr

                '��ȡ�����¼
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select a.�ļ���ʶ,a.��������,a.���ӱ�ʶ," + vbCr
                strSQL = strSQL + "         �������� = max(a.��������)," + vbCr
                strSQL = strSQL + "         �������� = max(a.�����������)," + vbCr
                strSQL = strSQL + "         ������� = max(a.�������)" + vbCr
                strSQL = strSQL + "       from" + vbCr
                strSQL = strSQL + "       (" + vbCr
                strSQL = strSQL + "         select" + vbCr
                strSQL = strSQL + "           �ļ���ʶ, ��������,��������,�����������,�������," + vbCr
                strSQL = strSQL + "           ���ӱ�ʶ = case when ���ӱ�ʶ like '_____1%' then '1' else '0' end" + vbCr
                strSQL = strSQL + "         from ����_B_����" + vbCr
                strSQL = strSQL + "         where ������   =    '" + Trim(strUserXM) + "'" + vbCr              '��Ҫ��
                strSQL = strSQL + "         and   ���ӱ�ʶ like '__1%'" + vbCr                                 '�ҿɼ�
                strSQL = strSQL + "         and   ����״̬ not in (" + strTaskAllYWCList + ")" + vbCr          'û�а���
                strSQL = strSQL + "         and   �������� is not null" + vbCr
                If strBLLX <> "" Then
                    strSQL = strSQL + "         and �������� = '" + strBLLX + "'" + vbCr                       'ָ������
                End If
                If strQSRQ <> "" And strZZRQ <> "" Then                                                        'ָ������
                    strSQL = strSQL + "         and �������� between '" + strQSRQ + "' and '" + strZZRQ + "' " + vbCr
                ElseIf strQSRQ <> "" Then
                    strSQL = strSQL + "         and �������� >= '" + strQSRQ + "'" + vbCr
                ElseIf strZZRQ <> "" Then
                    strSQL = strSQL + "         and �������� <= '" + strZZRQ + "'" + vbCr
                Else
                End If
                strSQL = strSQL + "       ) a" + vbCr
                strSQL = strSQL + "       group by a.�ļ���ʶ,a.��������,a.���ӱ�ʶ" + vbCr
                strSQL = strSQL + "     ) a " + vbCr
                '��ȡ�����¼

                '��ȡ��������
                strSQL = strSQL + "     left join" + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select �ļ���ʶ,�������� = case when max(��������) = 1 then '��' else '��' end" + vbCr
                strSQL = strSQL + "       from ����_B_����" + vbCr
                strSQL = strSQL + "       where ������ = '" + strUserXM + "'" + vbCr
                strSQL = strSQL + "       group by �ļ���ʶ" + vbCr
                strSQL = strSQL + "     ) c on a.�ļ���ʶ = c.�ļ���ʶ" + vbCr
                '��ȡ��������


                '��ȡ�ļ���Ϣ
                strSQL = strSQL + "     left join " + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select " + vbCr
                strSQL = strSQL + "         a.�ļ���ʶ, a.��ˮ��  , " + vbCr
                strSQL = strSQL + "         a.��������, a.����״̬, a.�ļ�����, a.�ļ�����," + vbCr
                strSQL = strSQL + "         a.�ļ�����, a.���͵�λ, " + vbCr
                strSQL = strSQL + "         a.�ļ��ֺ�, a.�����̶�, a.���ܵȼ�," + vbCr
                strSQL = strSQL + "         a.���ش���, a.�ļ����, a.�ļ����," + vbCr
                strSQL = strSQL + "         a.�����  , a.���쵥λ, a.�����  , a.�������," + vbCr
                strSQL = strSQL + "         a.��������" + vbCr
                strSQL = strSQL + "       from ����_V_ȫ�������ļ��� a" + vbCr
                strSQL = strSQL + "       left join" + vbCr
                strSQL = strSQL + "       (" + vbCr
                strSQL = strSQL + "           select �ļ���ʶ" + vbCr
                strSQL = strSQL + "           from ����_B_����" + vbCr
                strSQL = strSQL + "           where ������   =    '" + Trim(strUserXM) + "'" + vbCr              '��Ҫ��
                strSQL = strSQL + "           and   ���ӱ�ʶ like '__1%'" + vbCr                                 '�ҿɼ�
                strSQL = strSQL + "           and   ����״̬ not in (" + strTaskAllYWCList + ")" + vbCr          'û�а���
                strSQL = strSQL + "           and   �������� is not null" + vbCr
                If strBLLX <> "" Then
                    strSQL = strSQL + "           and �������� = '" + strBLLX + "'" + vbCr                       'ָ������
                End If
                If strQSRQ <> "" And strZZRQ <> "" Then                                                          'ָ������
                    strSQL = strSQL + "           and �������� between '" + strQSRQ + "' and '" + strZZRQ + "' " + vbCr
                ElseIf strQSRQ <> "" Then
                    strSQL = strSQL + "           and �������� >= '" + strQSRQ + "'" + vbCr
                ElseIf strZZRQ <> "" Then
                    strSQL = strSQL + "           and �������� <= '" + strZZRQ + "'" + vbCr
                Else
                End If
                strSQL = strSQL + "           group by �ļ���ʶ" + vbCr
                strSQL = strSQL + "       ) b on a.�ļ���ʶ = b.�ļ���ʶ" + vbCr
                strSQL = strSQL + "       where b.�ļ���ʶ is not null" + vbCr
                If strWJLX <> "" Then
                    strSQL = strSQL + "       and a.�ļ����� = '" + strWJLX + "'" + vbCr
                End If
                strSQL = strSQL + "     ) b on a.�ļ���ʶ = b.�ļ���ʶ " + vbCr
                '��ȡ�ļ���Ϣ


                strSQL = strSQL + "     where b.�ļ���ʶ Is Not Null " + vbCr
                strSQL = strSQL + "     and (" + vbCr
                strSQL = strSQL + "       (a.���ӱ�ʶ = '1')" + vbCr                                                            '֪ͨ����Ϣ
                strSQL = strSQL + "       or " + vbCr
                strSQL = strSQL + "       (b.�������� =   1)" + vbCr                                                            '��������
                strSQL = strSQL + "       or " + vbCr
                strSQL = strSQL + "       (b.����״̬ not in (" + strFileAllYWCList + ")) " + vbCr                              '�ļ�δ����
                strSQL = strSQL + "     ) " + vbCr
                strSQL = strSQL + "   ) a " + vbCr
                strSQL = strSQL + "   group by " + vbCr
                strSQL = strSQL + "     a.�ļ���ʶ, a.��ˮ��  ," + vbCr
                strSQL = strSQL + "     a.��������, a.����״̬, a.�ļ�����, a.�ļ�����," + vbCr
                strSQL = strSQL + "     a.�ļ�����, a.���͵�λ, a.�ļ��ֺ�, a.�����̶�, a.���ܵȼ�," + vbCr
                strSQL = strSQL + "     a.���ش���, a.�ļ����, a.�ļ����," + vbCr
                strSQL = strSQL + "     a.�����  , a.���쵥λ, a.�����  , a.�������, a.��������, a.��������" + vbCr
                strSQL = strSQL + " ) a" + vbCr
                If strWhere <> "" Then
                    strSQL = strSQL + " where " + strWhere + vbCr
                End If
                strSQL = strSQL + " order by a.�������� desc, a.�ļ���� desc, a.���ش���, a.�ļ���� desc" + vbCr
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getSQLDBSY_FILE = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �����ļ����͡���ʼ���ڡ��������ڼ���δ�����˵���������SQL
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strWJBS                ��Ҫ�鿴���ļ���ʶ
        '     strUserXM              ����ǰ������Ա����
        '     strBLLX                ����������
        '     strWJLX                ���ļ�����-����������
        '     strQSRQ                ����ʼ����
        '     strZZRQ                ����������
        '     strWhere               ����������
        '     strSQL                 ��(����)��������SQL
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Private Function getSQLDBSY_TASK( _
            ByRef strErrMsg As String, _
            ByVal strWJBS As String, _
            ByVal strUserXM As String, _
            ByVal strBLLX As String, _
            ByVal strWJLX As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String, _
            ByVal strWhere As String, _
            ByRef strSQL As String) As Boolean

            getSQLDBSY_TASK = False
            strSQL = ""

            Try
                Dim strFileAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.FileStatusAllYWCList
                Dim strTaskAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TaskStatusAllYWCList
                Dim strLF As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhLf
                Dim strRT As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhRt
                Dim strGWTHCL As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_THCL
                Dim strGWSHCL As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_SHCL
                Dim strGWHFCL As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_HFCL

                '��ʼ������
                If strQSRQ Is Nothing Then strQSRQ = ""
                strQSRQ = strQSRQ.Trim
                If strQSRQ <> "" Then
                    If strQSRQ.IndexOf(" ") < 0 Then
                        strQSRQ = strQSRQ + " 00:00:00"
                    End If
                End If
                If strZZRQ Is Nothing Then strZZRQ = ""
                strZZRQ = strZZRQ.Trim
                If strZZRQ <> "" Then
                    If strZZRQ.IndexOf(" ") < 0 Then
                        strZZRQ = strZZRQ + " 23:59:59"
                    End If
                End If

                '�ҵ��ļ�
                strSQL = ""
                strSQL = strSQL + " select * from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select" + vbCr
                strSQL = strSQL + "     a.�ļ���ʶ, b.��ˮ��  ," + vbCr
                strSQL = strSQL + "     a.��������, a.����״̬, a.��������, b.�ļ�����, b.�ļ�����," + vbCr
                strSQL = strSQL + "     b.�ļ�����, b.���ش���, b.�ļ����, b.�ļ����, b.���쵥λ," + vbCr
                strSQL = strSQL + "     a.������  , a.������  , a.ί����  , a.����˵��" + vbCr
                strSQL = strSQL + "   from" + vbCr

                '��ȡ�����¼
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select" + vbCr
                strSQL = strSQL + "       �ļ���ʶ, ��������, ����״̬," + vbCr
                strSQL = strSQL + "       �������� = case " + vbCr
                strSQL = strSQL + "         when ���ӱ�ʶ like '___1%'    then '" + strGWTHCL + "'" + vbCr
                strSQL = strSQL + "         when ���ӱ�ʶ like '____1%'   then '" + strGWSHCL + "'" + vbCr
                strSQL = strSQL + "         when ���ӱ�ʶ like '______1%' then '" + strGWHFCL + "'" + vbCr
                strSQL = strSQL + "         else �������� end," + vbCr
                strSQL = strSQL + "       ������, ������, ί����, ���ӱ�ʶ, ����˵�� " + vbCr
                strSQL = strSQL + "     from ����_B_����" + vbCr
                strSQL = strSQL + "     where �ļ���ʶ = '" + strWJBS + "'" + vbCr                         'ָ���ļ�
                strSQL = strSQL + "     and   ������   = '" + Trim(strUserXM) + "'" + vbCr                 '��Ҫ��
                strSQL = strSQL + "     and   ���ӱ�ʶ like '__1%'" + vbCr                                 '�ҿɼ�
                strSQL = strSQL + "     and   ����״̬ not in (" + strTaskAllYWCList + ")" + vbCr          'û�а���
                strSQL = strSQL + "     and   �������� is not null" + vbCr
                If strBLLX <> "" Then
                    strSQL = strSQL + "     and �������� = '" + strBLLX + "'" + vbCr                       'ָ������
                End If
                If strQSRQ <> "" And strZZRQ <> "" Then                                                    'ָ������
                    strSQL = strSQL + "     and �������� between '" + strQSRQ + "' and '" + strZZRQ + "' " + vbCr
                ElseIf strQSRQ <> "" Then
                    strSQL = strSQL + "     and �������� >= '" + strQSRQ + "'" + vbCr
                ElseIf strZZRQ <> "" Then
                    strSQL = strSQL + "     and �������� <= '" + strZZRQ + "'" + vbCr
                Else
                End If
                strSQL = strSQL + "   ) a " + vbCr
                '��ȡ�����¼


                '��ȡ�ļ���Ϣ
                strSQL = strSQL + "   left join " + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select " + vbCr
                strSQL = strSQL + "       �ļ���ʶ, ��ˮ��  , " + vbCr
                strSQL = strSQL + "       ��������, ����״̬, �ļ�����, �ļ�����," + vbCr
                strSQL = strSQL + "       �ļ�����, ���͵�λ, " + vbCr
                strSQL = strSQL + "       �ļ��ֺ�, �����̶�, ���ܵȼ�," + vbCr
                strSQL = strSQL + "       ���ش���, �ļ����, �ļ����," + vbCr
                strSQL = strSQL + "       �����  , ���쵥λ, �����  , �������," + vbCr
                strSQL = strSQL + "       ��������" + vbCr
                strSQL = strSQL + "     from ����_V_ȫ�������ļ��� " + vbCr
                strSQL = strSQL + "     where �ļ���ʶ = '" + strWJBS + "'" + vbCr                                            'ָ���ļ�
                If strWJLX <> "" Then
                    strSQL = strSQL + "     and   �ļ����� = '" + strWJLX + "'" + vbCr                                        '����������=�ļ���������
                End If
                strSQL = strSQL + "   ) b on a.�ļ���ʶ = b.�ļ���ʶ " + vbCr
                '��ȡ�ļ���Ϣ


                strSQL = strSQL + "   where b.�ļ���ʶ Is Not Null " + vbCr
                strSQL = strSQL + "   and (" + vbCr
                strSQL = strSQL + "     (a.���ӱ�ʶ like '_____1%')" + vbCr                                                   '֪ͨ����Ϣ
                strSQL = strSQL + "     or " + vbCr
                strSQL = strSQL + "     (b.�������� =   1)" + vbCr                                                            '��������
                strSQL = strSQL + "     or " + vbCr
                strSQL = strSQL + "     (b.����״̬ not in (" + strFileAllYWCList + ")) " + vbCr                              '�ļ�δ����
                strSQL = strSQL + "   ) " + vbCr
                strSQL = strSQL + " ) a " + vbCr
                If strWhere <> "" Then
                    strSQL = strSQL + " where " + strWhere + vbCr
                End If
                strSQL = strSQL + " group by " + vbCr
                strSQL = strSQL + "   a.�ļ���ʶ, a.��ˮ��  ," + vbCr
                strSQL = strSQL + "   a.��������, a.����״̬, a.��������, a.�ļ�����, a.�ļ�����," + vbCr
                strSQL = strSQL + "   a.�ļ�����, a.���ش���, a.�ļ����, a.�ļ����, a.���쵥λ," + vbCr
                strSQL = strSQL + "   a.������  , a.������  , a.ί����  , a.����˵��" + vbCr
                strSQL = strSQL + " order by a.�ļ���� desc, a.���ش���, a.�ļ���� desc" + vbCr
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getSQLDBSY_TASK = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �����ļ����͡���ʼ���ڡ��������ڼ�������ļ����ļ�����SQL
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strUserXM              ����ǰ������Ա����
        '     strBLLX                ����������
        '     strWJLX                ���ļ�����-����������
        '     strQSRQ                ����ʼ����
        '     strZZRQ                ����������
        '     strWhere               ����������
        '     strSQL                 ��(����)�ļ�����SQL
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Private Function getSQLDPWJ_FILE( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByVal strBLLX As String, _
            ByVal strWJLX As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String, _
            ByVal strWhere As String, _
            ByRef strSQL As String) As Boolean

            getSQLDPWJ_FILE = False
            strSQL = ""

            Try
                Dim strFileAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.FileStatusAllYWCList
                Dim strTaskAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TaskStatusAllYWCList
                Dim strLF As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhLf
                Dim strRT As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhRt

                '��ʼ������
                If strQSRQ Is Nothing Then strQSRQ = ""
                strQSRQ = strQSRQ.Trim
                If strQSRQ <> "" Then
                    If strQSRQ.IndexOf(" ") < 0 Then
                        strQSRQ = strQSRQ + " 00:00:00"
                    End If
                End If
                If strZZRQ Is Nothing Then strZZRQ = ""
                strZZRQ = strZZRQ.Trim
                If strZZRQ <> "" Then
                    If strZZRQ.IndexOf(" ") < 0 Then
                        strZZRQ = strZZRQ + " 23:59:59"
                    End If
                End If

                '�ҵ��ļ�
                strSQL = ""
                strSQL = strSQL + " select a.*" + vbCr
                strSQL = strSQL + " from" + vbCr
                strSQL = strSQL + " ("
                strSQL = strSQL + "   select" + vbCr
                strSQL = strSQL + "     a.�ļ���ʶ, a.��ˮ��  ," + vbCr
                strSQL = strSQL + "     a.��������, a.����״̬, a.�ļ�����, a.�ļ�����," + vbCr
                strSQL = strSQL + "     a.�ļ�����, a.���͵�λ, a.�ļ��ֺ�, a.�����̶�, a.���ܵȼ�," + vbCr
                strSQL = strSQL + "     a.���ش���, a.�ļ����, a.�ļ����," + vbCr
                strSQL = strSQL + "     a.�����  , a.���쵥λ, a.�����  , a.�������,a.��������," + vbCr
                strSQL = strSQL + "     �������� = max(a.��������)," + vbCr
                strSQL = strSQL + "     �������� = max(a.��������)," + vbCr
                strSQL = strSQL + "     ������� = max(a.�������)," + vbCr
                strSQL = strSQL + "     a.��������" + vbCr
                strSQL = strSQL + "   from" + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select" + vbCr
                strSQL = strSQL + "       a.�ļ���ʶ, b.��ˮ��  ," + vbCr
                strSQL = strSQL + "       a.��������, b.����״̬, b.�ļ�����, b.�ļ�����," + vbCr
                strSQL = strSQL + "       b.�ļ�����, b.���͵�λ, b.�ļ��ֺ�, b.�����̶�, b.���ܵȼ�," + vbCr
                strSQL = strSQL + "       b.���ش���, b.�ļ����, b.�ļ����," + vbCr
                strSQL = strSQL + "       b.�����  , b.���쵥λ, b.�����  , b.�������," + vbCr
                strSQL = strSQL + "       a.��������, a.��������, a.�������, b.��������," + vbCr
                strSQL = strSQL + "       �������� = case when c.�������� is null then '��' else c.�������� end" + vbCr
                strSQL = strSQL + "     from" + vbCr

                '��ȡ�����¼
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select" + vbCr
                strSQL = strSQL + "         �ļ���ʶ, ��������," + vbCr
                strSQL = strSQL + "         �������� = max(��������)," + vbCr
                strSQL = strSQL + "         �������� = max(�����������)," + vbCr
                strSQL = strSQL + "         ������� = max(�������)" + vbCr
                strSQL = strSQL + "       from ����_B_����" + vbCr
                strSQL = strSQL + "       where ������   =    '" + Trim(strUserXM) + "'" + vbCr              '������
                strSQL = strSQL + "       and   ���ӱ�ʶ like '11_0000%'" + vbCr                             '��������
                strSQL = strSQL + "       and   ����״̬ not in (" + strTaskAllYWCList + ")" + vbCr          'û�а���
                strSQL = strSQL + "       and   �������� is not null" + vbCr
                If strBLLX <> "" Then
                    strSQL = strSQL + "       and �������� = '" + strBLLX + "'" + vbCr                       'ָ������
                End If
                If strQSRQ <> "" And strZZRQ <> "" Then                                                      'ָ������
                    strSQL = strSQL + "       and �������� between '" + strQSRQ + "' and '" + strZZRQ + "' " + vbCr
                ElseIf strQSRQ <> "" Then
                    strSQL = strSQL + "       and �������� >= '" + strQSRQ + "'" + vbCr
                ElseIf strZZRQ <> "" Then
                    strSQL = strSQL + "       and �������� <= '" + strZZRQ + "'" + vbCr
                Else
                End If
                strSQL = strSQL + "       group by �ļ���ʶ,��������" + vbCr
                strSQL = strSQL + "     ) a " + vbCr
                '��ȡ�����¼

                '��ȡ��������
                strSQL = strSQL + "     left join" + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select �ļ���ʶ,�������� = case when max(��������) = 1 then '��' else '��' end" + vbCr
                strSQL = strSQL + "       from ����_B_����" + vbCr
                strSQL = strSQL + "       where ������ = '" + strUserXM + "'" + vbCr
                strSQL = strSQL + "       group by �ļ���ʶ" + vbCr
                strSQL = strSQL + "     ) c on a.�ļ���ʶ = c.�ļ���ʶ" + vbCr
                '��ȡ��������


                '��ȡ�ļ���Ϣ
                strSQL = strSQL + "     left join " + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select " + vbCr
                strSQL = strSQL + "         a.�ļ���ʶ, a.��ˮ��  , " + vbCr
                strSQL = strSQL + "         a.��������, a.����״̬, a.�ļ�����, a.�ļ�����," + vbCr
                strSQL = strSQL + "         a.�ļ�����, a.���͵�λ, " + vbCr
                strSQL = strSQL + "         a.�ļ��ֺ�, a.�����̶�, a.���ܵȼ�," + vbCr
                strSQL = strSQL + "         a.���ش���, a.�ļ����, a.�ļ����," + vbCr
                strSQL = strSQL + "         a.�����  , a.���쵥λ, a.�����  , a.�������," + vbCr
                strSQL = strSQL + "         a.��������" + vbCr
                strSQL = strSQL + "       from ����_V_ȫ�������ļ��� a" + vbCr
                strSQL = strSQL + "       left join" + vbCr
                strSQL = strSQL + "       (" + vbCr
                strSQL = strSQL + "         select �ļ���ʶ" + vbCr
                strSQL = strSQL + "         from ����_B_����" + vbCr
                strSQL = strSQL + "         where ������   =    '" + Trim(strUserXM) + "'" + vbCr              '������
                strSQL = strSQL + "         and   ���ӱ�ʶ like '11_0000%'" + vbCr                             '��������
                strSQL = strSQL + "         and   ����״̬ not in (" + strTaskAllYWCList + ")" + vbCr          'û�а���
                strSQL = strSQL + "         and   �������� is not null" + vbCr
                If strBLLX <> "" Then
                    strSQL = strSQL + "         and �������� = '" + strBLLX + "'" + vbCr                       'ָ������
                End If
                If strQSRQ <> "" And strZZRQ <> "" Then                                                        'ָ������
                    strSQL = strSQL + "         and �������� between '" + strQSRQ + "' and '" + strZZRQ + "' " + vbCr
                ElseIf strQSRQ <> "" Then
                    strSQL = strSQL + "         and �������� >= '" + strQSRQ + "'" + vbCr
                ElseIf strZZRQ <> "" Then
                    strSQL = strSQL + "         and �������� <= '" + strZZRQ + "'" + vbCr
                Else
                End If
                strSQL = strSQL + "         group by �ļ���ʶ" + vbCr
                strSQL = strSQL + "       ) b on a.�ļ���ʶ = b.�ļ���ʶ" + vbCr
                strSQL = strSQL + "       where b.�ļ���ʶ is not null" + vbCr
                If strWJLX <> "" Then
                    strSQL = strSQL + "       and a.�ļ����� = '" + strWJLX + "'" + vbCr
                End If
                strSQL = strSQL + "     ) b on a.�ļ���ʶ = b.�ļ���ʶ " + vbCr
                '��ȡ�ļ���Ϣ


                strSQL = strSQL + "     where b.�ļ���ʶ Is Not Null " + vbCr
                strSQL = strSQL + "     and (" + vbCr
                strSQL = strSQL + "       (b.�������� =   1)" + vbCr                                                            '��������
                strSQL = strSQL + "       or " + vbCr
                strSQL = strSQL + "       (b.����״̬ not in (" + strFileAllYWCList + ")) " + vbCr                              '�ļ�δ����
                strSQL = strSQL + "     ) " + vbCr
                strSQL = strSQL + "   ) a " + vbCr
                strSQL = strSQL + "   group by " + vbCr
                strSQL = strSQL + "     a.�ļ���ʶ, a.��ˮ��  ," + vbCr
                strSQL = strSQL + "     a.��������, a.����״̬, a.�ļ�����, a.�ļ�����," + vbCr
                strSQL = strSQL + "     a.�ļ�����, a.���͵�λ, a.�ļ��ֺ�, a.�����̶�, a.���ܵȼ�," + vbCr
                strSQL = strSQL + "     a.���ش���, a.�ļ����, a.�ļ����," + vbCr
                strSQL = strSQL + "     a.�����  , a.���쵥λ, a.�����  , a.�������, a.��������, a.��������" + vbCr
                strSQL = strSQL + " ) a" + vbCr
                If strWhere <> "" Then
                    strSQL = strSQL + " where " + strWhere + vbCr
                End If
                strSQL = strSQL + " order by a.�������� desc, a.�ļ���� desc, a.���ش���, a.�ļ���� desc" + vbCr
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getSQLDPWJ_FILE = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �����ļ����͡���ʼ���ڡ��������ڼ�������ļ�����������SQL
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strWJBS                ��Ҫ�鿴���ļ���ʶ
        '     strUserXM              ����ǰ������Ա����
        '     strBLLX                ����������
        '     strWJLX                ���ļ�����-����������
        '     strQSRQ                ����ʼ����
        '     strZZRQ                ����������
        '     strWhere               ����������
        '     strSQL                 ��(����)��������SQL
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Private Function getSQLDPWJ_TASK( _
            ByRef strErrMsg As String, _
            ByVal strWJBS As String, _
            ByVal strUserXM As String, _
            ByVal strBLLX As String, _
            ByVal strWJLX As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String, _
            ByVal strWhere As String, _
            ByRef strSQL As String) As Boolean

            getSQLDPWJ_TASK = False
            strSQL = ""

            Try
                Dim strFileAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.FileStatusAllYWCList
                Dim strTaskAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TaskStatusAllYWCList
                Dim strLF As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhLf
                Dim strRT As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhRt
                Dim strGWTHCL As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_THCL
                Dim strGWSHCL As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_SHCL
                Dim strGWHFCL As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_HFCL

                '��ʼ������
                If strQSRQ Is Nothing Then strQSRQ = ""
                strQSRQ = strQSRQ.Trim
                If strQSRQ <> "" Then
                    If strQSRQ.IndexOf(" ") < 0 Then
                        strQSRQ = strQSRQ + " 00:00:00"
                    End If
                End If
                If strZZRQ Is Nothing Then strZZRQ = ""
                strZZRQ = strZZRQ.Trim
                If strZZRQ <> "" Then
                    If strZZRQ.IndexOf(" ") < 0 Then
                        strZZRQ = strZZRQ + " 23:59:59"
                    End If
                End If

                '�ҵ��ļ�
                strSQL = ""
                strSQL = strSQL + " select * from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select" + vbCr
                strSQL = strSQL + "     a.�ļ���ʶ, b.��ˮ��  ," + vbCr
                strSQL = strSQL + "     a.��������, a.����״̬, a.��������, b.�ļ�����, b.�ļ�����," + vbCr
                strSQL = strSQL + "     b.�ļ�����, b.���ش���, b.�ļ����, b.�ļ����, b.���쵥λ," + vbCr
                strSQL = strSQL + "     a.������  , a.������  , a.ί����  , a.����˵��" + vbCr
                strSQL = strSQL + "   from" + vbCr

                '��ȡ�����¼
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select" + vbCr
                strSQL = strSQL + "      �ļ���ʶ, ��������, ����״̬," + vbCr
                strSQL = strSQL + "      �������� = case " + vbCr
                strSQL = strSQL + "        when ���ӱ�ʶ like '___1%'    then '" + strGWTHCL + "' " + vbCr
                strSQL = strSQL + "        when ���ӱ�ʶ like '____1%'   then '" + strGWSHCL + "' " + vbCr
                strSQL = strSQL + "        when ���ӱ�ʶ like '______1%' then '" + strGWHFCL + "' " + vbCr
                strSQL = strSQL + "        else �������� end," + vbCr
                strSQL = strSQL + "      ������, ������, ί����, ���ӱ�ʶ, ����˵�� " + vbCr
                strSQL = strSQL + "     from ����_B_����" + vbCr
                strSQL = strSQL + "     where �ļ���ʶ = '" + strWJBS + "'" + vbCr                         'ָ���ļ�
                strSQL = strSQL + "     and   ������   = '" + Trim(strUserXM) + "'" + vbCr                 '������
                strSQL = strSQL + "     and   ���ӱ�ʶ like '11_0000%'" + vbCr                             '��������
                strSQL = strSQL + "     and   ����״̬ not in (" + strTaskAllYWCList + ")" + vbCr          'û�а���
                strSQL = strSQL + "     and   �������� is not null" + vbCr
                If strBLLX <> "" Then
                    strSQL = strSQL + "     and �������� = '" + strBLLX + "'" + vbCr                       'ָ������
                End If
                If strQSRQ <> "" And strZZRQ <> "" Then                                                    'ָ������
                    strSQL = strSQL + "     and �������� between '" + strQSRQ + "' and '" + strZZRQ + "' " + vbCr
                ElseIf strQSRQ <> "" Then
                    strSQL = strSQL + "     and �������� >= '" + strQSRQ + "'" + vbCr
                ElseIf strZZRQ <> "" Then
                    strSQL = strSQL + "     and �������� <= '" + strZZRQ + "'" + vbCr
                Else
                End If
                strSQL = strSQL + "   ) a " + vbCr
                '��ȡ�����¼


                '��ȡ�ļ���Ϣ
                strSQL = strSQL + "   left join " + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select " + vbCr
                strSQL = strSQL + "       �ļ���ʶ, ��ˮ��  , " + vbCr
                strSQL = strSQL + "       ��������, ����״̬, �ļ�����, �ļ�����," + vbCr
                strSQL = strSQL + "       �ļ�����, ���͵�λ, " + vbCr
                strSQL = strSQL + "       �ļ��ֺ�, �����̶�, ���ܵȼ�," + vbCr
                strSQL = strSQL + "       ���ش���, �ļ����, �ļ����," + vbCr
                strSQL = strSQL + "       �����  , ���쵥λ, �����  , �������," + vbCr
                strSQL = strSQL + "       ��������" + vbCr
                strSQL = strSQL + "     from ����_V_ȫ�������ļ��� " + vbCr
                strSQL = strSQL + "     where �ļ���ʶ = '" + strWJBS + "'" + vbCr                                            'ָ���ļ�
                If strWJLX <> "" Then
                    strSQL = strSQL + "     and   �ļ����� = '" + strWJLX + "'" + vbCr                                        '����������=�ļ���������
                End If
                strSQL = strSQL + "   ) b on a.�ļ���ʶ = b.�ļ���ʶ " + vbCr
                '��ȡ�ļ���Ϣ


                strSQL = strSQL + "   where b.�ļ���ʶ Is Not Null " + vbCr
                strSQL = strSQL + "   and (" + vbCr
                strSQL = strSQL + "     (b.�������� =   1)" + vbCr                                                            '��������
                strSQL = strSQL + "     or " + vbCr
                strSQL = strSQL + "     (b.����״̬ not in (" + strFileAllYWCList + ")) " + vbCr                              '�ļ�δ����
                strSQL = strSQL + "   ) " + vbCr
                strSQL = strSQL + " ) a " + vbCr
                If strWhere <> "" Then
                    strSQL = strSQL + " where " + strWhere + vbCr
                End If
                strSQL = strSQL + " group by " + vbCr
                strSQL = strSQL + "   a.�ļ���ʶ, a.��ˮ��  ," + vbCr
                strSQL = strSQL + "   a.��������, a.����״̬, a.��������, a.�ļ�����, a.�ļ�����," + vbCr
                strSQL = strSQL + "   a.�ļ�����, a.���ش���, a.�ļ����, a.�ļ����, a.���쵥λ," + vbCr
                strSQL = strSQL + "   a.������  , a.������  , a.ί����  , a.����˵��" + vbCr
                strSQL = strSQL + " order by a.�ļ���� desc, a.���ش���, a.�ļ���� desc" + vbCr
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getSQLDPWJ_TASK = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �����ļ����͡���ʼ���ڡ��������ڼ��㻺���ļ����ļ�����SQL
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strUserXM              ����ǰ������Ա����
        '     strBLLX                ����������
        '     strWJLX                ���ļ�����-����������
        '     strQSRQ                ����ʼ����
        '     strZZRQ                ����������
        '     strWhere               ����������
        '     strSQL                 ��(����)�ļ�����SQL
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Private Function getSQLHBWJ_FILE( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByVal strBLLX As String, _
            ByVal strWJLX As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String, _
            ByVal strWhere As String, _
            ByRef strSQL As String) As Boolean

            getSQLHBWJ_FILE = False
            strSQL = ""

            Try
                Dim strFileAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.FileStatusAllYWCList
                Dim strTaskYTBList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TaskStatusYTBList
                Dim strLF As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhLf
                Dim strRT As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhRt

                '��ʼ������
                If strQSRQ Is Nothing Then strQSRQ = ""
                strQSRQ = strQSRQ.Trim
                If strQSRQ <> "" Then
                    If strQSRQ.IndexOf(" ") < 0 Then
                        strQSRQ = strQSRQ + " 00:00:00"
                    End If
                End If
                If strZZRQ Is Nothing Then strZZRQ = ""
                strZZRQ = strZZRQ.Trim
                If strZZRQ <> "" Then
                    If strZZRQ.IndexOf(" ") < 0 Then
                        strZZRQ = strZZRQ + " 23:59:59"
                    End If
                End If

                '�ҵ��ļ�
                strSQL = ""
                strSQL = strSQL + " select a.*" + vbCr
                strSQL = strSQL + " from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select" + vbCr
                strSQL = strSQL + "     a.�ļ���ʶ, a.��ˮ��  ," + vbCr
                strSQL = strSQL + "     a.��������, a.����״̬, a.�ļ�����, a.�ļ�����," + vbCr
                strSQL = strSQL + "     a.�ļ�����, a.���͵�λ, a.�ļ��ֺ�, a.�����̶�, a.���ܵȼ�," + vbCr
                strSQL = strSQL + "     a.���ش���, a.�ļ����, a.�ļ����," + vbCr
                strSQL = strSQL + "     a.�����  , a.���쵥λ, a.�����  , a.�������,a.��������," + vbCr
                strSQL = strSQL + "     �������� = max(a.��������)," + vbCr
                strSQL = strSQL + "     �������� = max(a.��������)," + vbCr
                strSQL = strSQL + "     ������� = max(a.�������)," + vbCr
                strSQL = strSQL + "     a.��������" + vbCr
                strSQL = strSQL + "   from" + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select" + vbCr
                strSQL = strSQL + "       a.�ļ���ʶ, b.��ˮ��  ," + vbCr
                strSQL = strSQL + "       a.��������, b.����״̬, b.�ļ�����, b.�ļ�����," + vbCr
                strSQL = strSQL + "       b.�ļ�����, b.���͵�λ, b.�ļ��ֺ�, b.�����̶�, b.���ܵȼ�," + vbCr
                strSQL = strSQL + "       b.���ش���, b.�ļ����, b.�ļ����," + vbCr
                strSQL = strSQL + "       b.�����  , b.���쵥λ, b.�����  , b.�������," + vbCr
                strSQL = strSQL + "       a.��������, a.��������, a.�������, b.��������," + vbCr
                strSQL = strSQL + "       �������� = case when c.�������� is null then '��' else c.�������� end" + vbCr
                strSQL = strSQL + "     from" + vbCr

                '��ȡ�����¼
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select" + vbCr
                strSQL = strSQL + "         �ļ���ʶ, ��������," + vbCr
                strSQL = strSQL + "         �������� = max(��������)," + vbCr
                strSQL = strSQL + "         �������� = max(�����������)," + vbCr
                strSQL = strSQL + "         ������� = max(�������)" + vbCr
                strSQL = strSQL + "       from ����_B_����" + vbCr
                strSQL = strSQL + "       where ������   =    '" + Trim(strUserXM) + "'" + vbCr              '������
                strSQL = strSQL + "       and   ���ӱ�ʶ like '__1%'" + vbCr                                 '�ҿɼ�
                strSQL = strSQL + "       and   ����״̬ in (" + strTaskYTBList + ")" + vbCr                 '��ͣ��
                strSQL = strSQL + "       and   ������� is not null" + vbCr
                If strBLLX <> "" Then
                    strSQL = strSQL + "       and �������� = '" + strBLLX + "'" + vbCr                       'ָ������
                End If
                If strQSRQ <> "" And strZZRQ <> "" Then                                                      'ָ������
                    strSQL = strSQL + "       and ������� between '" + strQSRQ + "' and '" + strZZRQ + "' " + vbCr
                ElseIf strQSRQ <> "" Then
                    strSQL = strSQL + "       and ������� >= '" + strQSRQ + "'" + vbCr
                ElseIf strZZRQ <> "" Then
                    strSQL = strSQL + "       and ������� <= '" + strZZRQ + "'" + vbCr
                Else
                End If
                strSQL = strSQL + "       group by �ļ���ʶ,��������" + vbCr
                strSQL = strSQL + "     ) a " + vbCr
                '��ȡ�����¼

                '��ȡ��������
                strSQL = strSQL + "     left join" + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select �ļ���ʶ,�������� = case when max(��������) = 1 then '��' else '��' end" + vbCr
                strSQL = strSQL + "       from ����_B_����" + vbCr
                strSQL = strSQL + "       where ������ = '" + strUserXM + "'" + vbCr
                strSQL = strSQL + "       group by �ļ���ʶ" + vbCr
                strSQL = strSQL + "     ) c on a.�ļ���ʶ = c.�ļ���ʶ" + vbCr
                '��ȡ��������


                '��ȡ�ļ���Ϣ
                strSQL = strSQL + "     left join " + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select " + vbCr
                strSQL = strSQL + "         a.�ļ���ʶ, a.��ˮ��  , " + vbCr
                strSQL = strSQL + "         a.��������, a.����״̬, a.�ļ�����, a.�ļ�����," + vbCr
                strSQL = strSQL + "         a.�ļ�����, a.���͵�λ, " + vbCr
                strSQL = strSQL + "         a.�ļ��ֺ�, a.�����̶�, a.���ܵȼ�," + vbCr
                strSQL = strSQL + "         a.���ش���, a.�ļ����, a.�ļ����," + vbCr
                strSQL = strSQL + "         a.�����  , a.���쵥λ, a.�����  , a.�������," + vbCr
                strSQL = strSQL + "         a.��������" + vbCr
                strSQL = strSQL + "       from ����_V_ȫ�������ļ��� a" + vbCr
                strSQL = strSQL + "       left join" + vbCr
                strSQL = strSQL + "       (" + vbCr
                strSQL = strSQL + "         select �ļ���ʶ" + vbCr
                strSQL = strSQL + "         from ����_B_����" + vbCr
                strSQL = strSQL + "         where ������   =    '" + Trim(strUserXM) + "'" + vbCr              '������
                strSQL = strSQL + "         and   ���ӱ�ʶ like '__1%'" + vbCr                                 '�ҿɼ�
                strSQL = strSQL + "         and   ����״̬ in (" + strTaskYTBList + ")" + vbCr                 '��ͣ��
                strSQL = strSQL + "         and   ������� is not null" + vbCr
                If strBLLX <> "" Then
                    strSQL = strSQL + "         and �������� = '" + strBLLX + "'" + vbCr                       'ָ������
                End If
                If strQSRQ <> "" And strZZRQ <> "" Then                                                        'ָ������
                    strSQL = strSQL + "         and ������� between '" + strQSRQ + "' and '" + strZZRQ + "' " + vbCr
                ElseIf strQSRQ <> "" Then
                    strSQL = strSQL + "         and ������� >= '" + strQSRQ + "'" + vbCr
                ElseIf strZZRQ <> "" Then
                    strSQL = strSQL + "         and ������� <= '" + strZZRQ + "'" + vbCr
                Else
                End If
                strSQL = strSQL + "         group by �ļ���ʶ" + vbCr
                strSQL = strSQL + "       ) b on a.�ļ���ʶ = b.�ļ���ʶ" + vbCr
                strSQL = strSQL + "       where b.�ļ���ʶ is not null" + vbCr
                If strWJLX <> "" Then
                    strSQL = strSQL + "       and a.�ļ����� = '" + strWJLX + "'" + vbCr
                End If
                strSQL = strSQL + "     ) b on a.�ļ���ʶ = b.�ļ���ʶ " + vbCr
                '��ȡ�ļ���Ϣ


                strSQL = strSQL + "     where b.�ļ���ʶ Is Not Null " + vbCr
                strSQL = strSQL + "   ) a " + vbCr
                strSQL = strSQL + "   group by " + vbCr
                strSQL = strSQL + "     a.�ļ���ʶ, a.��ˮ��  ," + vbCr
                strSQL = strSQL + "     a.��������, a.����״̬, a.�ļ�����, a.�ļ�����," + vbCr
                strSQL = strSQL + "     a.�ļ�����, a.���͵�λ, a.�ļ��ֺ�, a.�����̶�, a.���ܵȼ�," + vbCr
                strSQL = strSQL + "     a.���ش���, a.�ļ����, a.�ļ����," + vbCr
                strSQL = strSQL + "     a.�����  , a.���쵥λ, a.�����  , a.�������, a.��������, a.��������" + vbCr
                strSQL = strSQL + " ) a" + vbCr
                If strWhere <> "" Then
                    strSQL = strSQL + " where " + strWhere + vbCr
                End If
                strSQL = strSQL + " order by a.�������� desc, a.�ļ���� desc, a.���ش���, a.�ļ���� desc" + vbCr
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getSQLHBWJ_FILE = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �����ļ����͡���ʼ���ڡ��������ڼ��㻺���ļ�����������SQL
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strWJBS                ��Ҫ�鿴���ļ���ʶ
        '     strUserXM              ����ǰ������Ա����
        '     strBLLX                ����������
        '     strWJLX                ���ļ�����-����������
        '     strQSRQ                ����ʼ����
        '     strZZRQ                ����������
        '     strWhere               ����������
        '     strSQL                 ��(����)��������SQL
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Private Function getSQLHBWJ_TASK( _
            ByRef strErrMsg As String, _
            ByVal strWJBS As String, _
            ByVal strUserXM As String, _
            ByVal strBLLX As String, _
            ByVal strWJLX As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String, _
            ByVal strWhere As String, _
            ByRef strSQL As String) As Boolean

            getSQLHBWJ_TASK = False
            strSQL = ""

            Try
                Dim strFileAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.FileStatusAllYWCList
                Dim strTaskYTBList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TaskStatusYTBList
                Dim strLF As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhLf
                Dim strRT As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhRt
                Dim strGWTHCL As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_THCL
                Dim strGWSHCL As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_SHCL
                Dim strGWHFCL As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_HFCL

                '��ʼ������
                If strQSRQ Is Nothing Then strQSRQ = ""
                strQSRQ = strQSRQ.Trim
                If strQSRQ <> "" Then
                    If strQSRQ.IndexOf(" ") < 0 Then
                        strQSRQ = strQSRQ + " 00:00:00"
                    End If
                End If
                If strZZRQ Is Nothing Then strZZRQ = ""
                strZZRQ = strZZRQ.Trim
                If strZZRQ <> "" Then
                    If strZZRQ.IndexOf(" ") < 0 Then
                        strZZRQ = strZZRQ + " 23:59:59"
                    End If
                End If

                '�ҵ��ļ�
                strSQL = ""
                strSQL = strSQL + " select * from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select" + vbCr
                strSQL = strSQL + "     a.�ļ���ʶ, b.��ˮ��  ," + vbCr
                strSQL = strSQL + "     a.��������, a.����״̬, a.��������, b.�ļ�����, b.�ļ�����," + vbCr
                strSQL = strSQL + "     b.�ļ�����, b.���ش���, b.�ļ����, b.�ļ����, b.���쵥λ," + vbCr
                strSQL = strSQL + "     a.������  , a.������  , a.ί����  , a.����˵��" + vbCr
                strSQL = strSQL + "   from" + vbCr

                '��ȡ�����¼
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select" + vbCr
                strSQL = strSQL + "       �ļ���ʶ, ��������, ����״̬," + vbCr
                strSQL = strSQL + "       �������� = case " + vbCr
                strSQL = strSQL + "         when ���ӱ�ʶ like '___1%'    then '" + strGWTHCL + "' " + vbCr
                strSQL = strSQL + "         when ���ӱ�ʶ like '____1%'   then '" + strGWSHCL + "' " + vbCr
                strSQL = strSQL + "         when ���ӱ�ʶ like '______1%' then '" + strGWHFCL + "' " + vbCr
                strSQL = strSQL + "         else �������� end," + vbCr
                strSQL = strSQL + "       ������, ������, ί����, ���ӱ�ʶ, ����˵�� " + vbCr
                strSQL = strSQL + "     from ����_B_����" + vbCr
                strSQL = strSQL + "     where �ļ���ʶ = '" + strWJBS + "'" + vbCr                         'ָ���ļ�
                strSQL = strSQL + "     and   ������   = '" + Trim(strUserXM) + "'" + vbCr                 '������
                strSQL = strSQL + "     and   ���ӱ�ʶ like '__1%'" + vbCr                                 '�ҿɼ�
                strSQL = strSQL + "     and   ����״̬ in (" + strTaskYTBList + ")" + vbCr                 '��ͣ��
                strSQL = strSQL + "     and   ������� is not null" + vbCr
                If strBLLX <> "" Then
                    strSQL = strSQL + "     and �������� = '" + strBLLX + "'" + vbCr                       'ָ������
                End If
                If strQSRQ <> "" And strZZRQ <> "" Then                                                    'ָ������
                    strSQL = strSQL + "     and ������� between '" + strQSRQ + "' and '" + strZZRQ + "' " + vbCr
                ElseIf strQSRQ <> "" Then
                    strSQL = strSQL + "     and ������� >= '" + strQSRQ + "'" + vbCr
                ElseIf strZZRQ <> "" Then
                    strSQL = strSQL + "     and ������� <= '" + strZZRQ + "'" + vbCr
                Else
                End If
                strSQL = strSQL + "   ) a " + vbCr
                '��ȡ�����¼


                '��ȡ�ļ���Ϣ
                strSQL = strSQL + "   left join " + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select " + vbCr
                strSQL = strSQL + "       �ļ���ʶ, ��ˮ��  , " + vbCr
                strSQL = strSQL + "       ��������, ����״̬, �ļ�����, �ļ�����," + vbCr
                strSQL = strSQL + "       �ļ�����, ���͵�λ, " + vbCr
                strSQL = strSQL + "       �ļ��ֺ�, �����̶�, ���ܵȼ�," + vbCr
                strSQL = strSQL + "       ���ش���, �ļ����, �ļ����," + vbCr
                strSQL = strSQL + "       �����  , ���쵥λ, �����  , �������," + vbCr
                strSQL = strSQL + "       ��������" + vbCr
                strSQL = strSQL + "     from ����_V_ȫ�������ļ��� " + vbCr
                strSQL = strSQL + "     where �ļ���ʶ = '" + strWJBS + "'" + vbCr                                            'ָ���ļ�
                If strWJLX <> "" Then
                    strSQL = strSQL + "     and   �ļ����� = '" + strWJLX + "'" + vbCr                                        '����������=�ļ���������
                End If
                strSQL = strSQL + "   ) b on a.�ļ���ʶ = b.�ļ���ʶ " + vbCr
                '��ȡ�ļ���Ϣ


                strSQL = strSQL + "   where b.�ļ���ʶ Is Not Null " + vbCr
                strSQL = strSQL + " ) a " + vbCr
                If strWhere <> "" Then
                    strSQL = strSQL + " where " + strWhere + vbCr
                End If
                strSQL = strSQL + " group by " + vbCr
                strSQL = strSQL + "   a.�ļ���ʶ, a.��ˮ��  ," + vbCr
                strSQL = strSQL + "   a.��������, a.����״̬, a.��������, a.�ļ�����, a.�ļ�����," + vbCr
                strSQL = strSQL + "   a.�ļ�����, a.���ش���, a.�ļ����, a.�ļ����, a.���쵥λ," + vbCr
                strSQL = strSQL + "   a.������  , a.������  , a.ί����  , a.����˵��" + vbCr
                strSQL = strSQL + " order by a.�ļ���� desc, a.���ش���, a.�ļ���� desc" + vbCr
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getSQLHBWJ_TASK = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �����ļ����͡���ʼ���ڡ��������ڼ����Ѱ��ļ����ļ�����SQL
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strUserXM              ����ǰ������Ա����
        '     strBLLX                ����������
        '     strWJLX                ���ļ�����-����������
        '     strQSRQ                ����ʼ����
        '     strZZRQ                ����������
        '     strWhere               ����������
        '     strSQL                 ��(����)�ļ�����SQL
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Private Function getSQLYBSY_FILE( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByVal strBLLX As String, _
            ByVal strWJLX As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String, _
            ByVal strWhere As String, _
            ByRef strSQL As String) As Boolean

            getSQLYBSY_FILE = False
            strSQL = ""

            Try
                Dim strFileAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.FileStatusAllYWCList
                Dim strTaskAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TaskStatusAllYWCList
                Dim strLF As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhLf
                Dim strRT As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhRt

                '��ʼ������
                If strQSRQ Is Nothing Then strQSRQ = ""
                strQSRQ = strQSRQ.Trim
                If strQSRQ <> "" Then
                    If strQSRQ.IndexOf(" ") < 0 Then
                        strQSRQ = strQSRQ + " 00:00:00"
                    End If
                End If
                If strZZRQ Is Nothing Then strZZRQ = ""
                strZZRQ = strZZRQ.Trim
                If strZZRQ <> "" Then
                    If strZZRQ.IndexOf(" ") < 0 Then
                        strZZRQ = strZZRQ + " 23:59:59"
                    End If
                End If

                '�ҵ��ļ�
                strSQL = ""
                strSQL = strSQL + " select a.*" + vbCr
                strSQL = strSQL + " from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select" + vbCr
                strSQL = strSQL + "     a.�ļ���ʶ, a.��ˮ��  ," + vbCr
                strSQL = strSQL + "     a.��������, a.����״̬, a.�ļ�����, a.�ļ�����," + vbCr
                strSQL = strSQL + "     a.�ļ�����, a.���͵�λ, a.�ļ��ֺ�, a.�����̶�, a.���ܵȼ�," + vbCr
                strSQL = strSQL + "     a.���ش���, a.�ļ����, a.�ļ����," + vbCr
                strSQL = strSQL + "     a.�����  , a.���쵥λ, a.�����  , a.�������,a.��������," + vbCr
                strSQL = strSQL + "     �������� = max(a.��������)," + vbCr
                strSQL = strSQL + "     �������� = max(a.��������)," + vbCr
                strSQL = strSQL + "     ������� = max(a.�������)," + vbCr
                strSQL = strSQL + "     a.��������" + vbCr
                strSQL = strSQL + "   from" + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select" + vbCr
                strSQL = strSQL + "       a.�ļ���ʶ, b.��ˮ��  ," + vbCr
                strSQL = strSQL + "       a.��������, b.����״̬, b.�ļ�����, b.�ļ�����," + vbCr
                strSQL = strSQL + "       b.�ļ�����, b.���͵�λ, b.�ļ��ֺ�, b.�����̶�, b.���ܵȼ�," + vbCr
                strSQL = strSQL + "       b.���ش���, b.�ļ����, b.�ļ����," + vbCr
                strSQL = strSQL + "       b.�����  , b.���쵥λ, b.�����  , b.�������," + vbCr
                strSQL = strSQL + "       a.��������, a.��������, a.�������, b.��������," + vbCr
                strSQL = strSQL + "       �������� = case when c.�������� is null then '��' else c.�������� end" + vbCr
                strSQL = strSQL + "     from" + vbCr

                '��ȡ�����¼
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select" + vbCr
                strSQL = strSQL + "         �ļ���ʶ, ��������," + vbCr
                strSQL = strSQL + "         �������� = max(��������)," + vbCr
                strSQL = strSQL + "         �������� = max(�����������)," + vbCr
                strSQL = strSQL + "         ������� = max(�������)" + vbCr
                strSQL = strSQL + "       from ����_B_����" + vbCr
                strSQL = strSQL + "       where ������   =    '" + Trim(strUserXM) + "'" + vbCr              '������
                strSQL = strSQL + "       and   ���ӱ�ʶ like '__1%'" + vbCr                                 '�ҿɼ�
                strSQL = strSQL + "       and   ����״̬ in (" + strTaskAllYWCList + ")" + vbCr              '�Ѱ���
                If strBLLX <> "" Then
                    strSQL = strSQL + "       and �������� = '" + strBLLX + "'" + vbCr                       'ָ������
                End If
                If strQSRQ <> "" And strZZRQ <> "" Then                                                      'ָ������
                    strSQL = strSQL + "       and ������� between '" + strQSRQ + "' and '" + strZZRQ + "' " + vbCr
                ElseIf strQSRQ <> "" Then
                    strSQL = strSQL + "       and ������� >= '" + strQSRQ + "'" + vbCr
                ElseIf strZZRQ <> "" Then
                    strSQL = strSQL + "       and ������� <= '" + strZZRQ + "'" + vbCr
                Else
                End If
                strSQL = strSQL + "       group by �ļ���ʶ, ��������" + vbCr
                strSQL = strSQL + "     ) a " + vbCr
                '��ȡ�����¼

                '��ȡ��������
                strSQL = strSQL + "     left join" + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select �ļ���ʶ,�������� = case when max(��������) = 1 then '��' else '��' end" + vbCr
                strSQL = strSQL + "       from ����_B_����" + vbCr
                strSQL = strSQL + "       where ������ = '" + strUserXM + "'" + vbCr
                strSQL = strSQL + "       group by �ļ���ʶ" + vbCr
                strSQL = strSQL + "     ) c on a.�ļ���ʶ = c.�ļ���ʶ" + vbCr
                '��ȡ��������


                '��ȡ�ļ���Ϣ
                strSQL = strSQL + "     left join " + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select " + vbCr
                strSQL = strSQL + "         a.�ļ���ʶ, a.��ˮ��  , " + vbCr
                strSQL = strSQL + "         a.��������, a.����״̬, a.�ļ�����, a.�ļ�����," + vbCr
                strSQL = strSQL + "         a.�ļ�����, a.���͵�λ, " + vbCr
                strSQL = strSQL + "         a.�ļ��ֺ�, a.�����̶�, a.���ܵȼ�," + vbCr
                strSQL = strSQL + "         a.���ش���, a.�ļ����, a.�ļ����," + vbCr
                strSQL = strSQL + "         a.�����  , a.���쵥λ, a.�����  , a.�������," + vbCr
                strSQL = strSQL + "         a.��������" + vbCr
                strSQL = strSQL + "       from ����_V_ȫ�������ļ��� a" + vbCr
                strSQL = strSQL + "       left join" + vbCr
                strSQL = strSQL + "       (" + vbCr
                strSQL = strSQL + "         select �ļ���ʶ" + vbCr
                strSQL = strSQL + "         from ����_B_����" + vbCr
                strSQL = strSQL + "         where ������   =    '" + Trim(strUserXM) + "'" + vbCr              '������
                strSQL = strSQL + "         and   ���ӱ�ʶ like '__1%'" + vbCr                                 '�ҿɼ�
                strSQL = strSQL + "         and   ����״̬ in (" + strTaskAllYWCList + ")" + vbCr              '�Ѱ���
                If strBLLX <> "" Then
                    strSQL = strSQL + "         and �������� = '" + strBLLX + "'" + vbCr                       'ָ������
                End If
                If strQSRQ <> "" And strZZRQ <> "" Then                                                        'ָ������
                    strSQL = strSQL + "         and ������� between '" + strQSRQ + "' and '" + strZZRQ + "' " + vbCr
                ElseIf strQSRQ <> "" Then
                    strSQL = strSQL + "         and ������� >= '" + strQSRQ + "'" + vbCr
                ElseIf strZZRQ <> "" Then
                    strSQL = strSQL + "         and ������� <= '" + strZZRQ + "'" + vbCr
                Else
                End If
                strSQL = strSQL + "         group by �ļ���ʶ" + vbCr
                strSQL = strSQL + "       ) b on a.�ļ���ʶ = b.�ļ���ʶ" + vbCr
                strSQL = strSQL + "       where b.�ļ���ʶ is not null" + vbCr
                If strWJLX <> "" Then
                    strSQL = strSQL + "       and a.�ļ����� = '" + strWJLX + "'" + vbCr
                End If
                strSQL = strSQL + "     ) b on a.�ļ���ʶ = b.�ļ���ʶ " + vbCr
                '��ȡ�ļ���Ϣ


                strSQL = strSQL + "     where b.�ļ���ʶ Is Not Null " + vbCr
                strSQL = strSQL + "   ) a " + vbCr
                strSQL = strSQL + "   group by " + vbCr
                strSQL = strSQL + "     a.�ļ���ʶ, a.��ˮ��  ," + vbCr
                strSQL = strSQL + "     a.��������, a.����״̬, a.�ļ�����, a.�ļ�����," + vbCr
                strSQL = strSQL + "     a.�ļ�����, a.���͵�λ, a.�ļ��ֺ�, a.�����̶�, a.���ܵȼ�," + vbCr
                strSQL = strSQL + "     a.���ش���, a.�ļ����, a.�ļ����," + vbCr
                strSQL = strSQL + "     a.�����  , a.���쵥λ, a.�����  , a.�������, a.��������, a.��������" + vbCr
                strSQL = strSQL + " ) a" + vbCr
                If strWhere <> "" Then
                    strSQL = strSQL + " where " + strWhere + vbCr
                End If
                strSQL = strSQL + " order by a.�������� desc, a.�ļ���� desc, a.���ش���, a.�ļ���� desc" + vbCr
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getSQLYBSY_FILE = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �����ļ����͡���ʼ���ڡ��������ڼ����Ѱ����˵���������SQL
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strWJBS                ��Ҫ�鿴���ļ���ʶ
        '     strUserXM              ����ǰ������Ա����
        '     strBLLX                ����������
        '     strWJLX                ���ļ�����-����������
        '     strQSRQ                ����ʼ����
        '     strZZRQ                ����������
        '     strWhere               ����������
        '     strSQL                 ��(����)��������SQL
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Private Function getSQLYBSY_TASK( _
            ByRef strErrMsg As String, _
            ByVal strWJBS As String, _
            ByVal strUserXM As String, _
            ByVal strBLLX As String, _
            ByVal strWJLX As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String, _
            ByVal strWhere As String, _
            ByRef strSQL As String) As Boolean

            getSQLYBSY_TASK = False
            strSQL = ""

            Try
                Dim strFileAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.FileStatusAllYWCList
                Dim strTaskAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TaskStatusAllYWCList
                Dim strLF As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhLf
                Dim strRT As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhRt
                Dim strGWTHCL As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_THCL
                Dim strGWSHCL As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_SHCL
                Dim strGWHFCL As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_HFCL

                '��ʼ������
                If strQSRQ Is Nothing Then strQSRQ = ""
                strQSRQ = strQSRQ.Trim
                If strQSRQ <> "" Then
                    If strQSRQ.IndexOf(" ") < 0 Then
                        strQSRQ = strQSRQ + " 00:00:00"
                    End If
                End If
                If strZZRQ Is Nothing Then strZZRQ = ""
                strZZRQ = strZZRQ.Trim
                If strZZRQ <> "" Then
                    If strZZRQ.IndexOf(" ") < 0 Then
                        strZZRQ = strZZRQ + " 23:59:59"
                    End If
                End If

                '�ҵ��ļ�
                strSQL = ""
                strSQL = strSQL + " select * from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select" + vbCr
                strSQL = strSQL + "     a.�ļ���ʶ, b.��ˮ��  ," + vbCr
                strSQL = strSQL + "     a.��������, a.����״̬, a.��������, b.�ļ�����, b.�ļ�����," + vbCr
                strSQL = strSQL + "     b.�ļ�����, b.���ش���, b.�ļ����, b.�ļ����, b.���쵥λ," + vbCr
                strSQL = strSQL + "     a.������  , a.������  , a.ί����  , a.����˵��" + vbCr
                strSQL = strSQL + "   from" + vbCr

                '��ȡ�����¼
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select" + vbCr
                strSQL = strSQL + "       �ļ���ʶ, ��������, ����״̬," + vbCr
                strSQL = strSQL + "       �������� = case " + vbCr
                strSQL = strSQL + "         when ���ӱ�ʶ like '___1%'    then '" + strGWTHCL + "' " + vbCr
                strSQL = strSQL + "         when ���ӱ�ʶ like '____1%'   then '" + strGWSHCL + "' " + vbCr
                strSQL = strSQL + "         when ���ӱ�ʶ like '______1%' then '" + strGWHFCL + "' " + vbCr
                strSQL = strSQL + "         else �������� end," + vbCr
                strSQL = strSQL + "       ������, ������, ί����, ���ӱ�ʶ, ����˵�� " + vbCr
                strSQL = strSQL + "     from ����_B_����" + vbCr
                strSQL = strSQL + "     where �ļ���ʶ = '" + strWJBS + "'" + vbCr                         'ָ���ļ�
                strSQL = strSQL + "     and   ������   = '" + Trim(strUserXM) + "'" + vbCr                 '������
                strSQL = strSQL + "     and   ���ӱ�ʶ like '__1%'" + vbCr                                 '�ҿɼ�
                strSQL = strSQL + "     and   ����״̬ in (" + strTaskAllYWCList + ")" + vbCr              '�Ѱ���
                If strBLLX <> "" Then
                    strSQL = strSQL + "     and �������� = '" + strBLLX + "'" + vbCr                       'ָ������
                End If
                If strQSRQ <> "" And strZZRQ <> "" Then                                                    'ָ������
                    strSQL = strSQL + "     and ������� between '" + strQSRQ + "' and '" + strZZRQ + "' " + vbCr
                ElseIf strQSRQ <> "" Then
                    strSQL = strSQL + "     and ������� >= '" + strQSRQ + "'" + vbCr
                ElseIf strZZRQ <> "" Then
                    strSQL = strSQL + "     and ������� <= '" + strZZRQ + "'" + vbCr
                Else
                End If
                strSQL = strSQL + "   ) a " + vbCr
                '��ȡ�����¼


                '��ȡ�ļ���Ϣ
                strSQL = strSQL + "   left join " + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select " + vbCr
                strSQL = strSQL + "       �ļ���ʶ, ��ˮ��  , " + vbCr
                strSQL = strSQL + "       ��������, ����״̬, �ļ�����, �ļ�����," + vbCr
                strSQL = strSQL + "       �ļ�����, ���͵�λ, " + vbCr
                strSQL = strSQL + "       �ļ��ֺ�, �����̶�, ���ܵȼ�," + vbCr
                strSQL = strSQL + "       ���ش���, �ļ����, �ļ����," + vbCr
                strSQL = strSQL + "       �����  , ���쵥λ, �����  , �������," + vbCr
                strSQL = strSQL + "       ��������" + vbCr
                strSQL = strSQL + "     from ����_V_ȫ�������ļ��� " + vbCr
                strSQL = strSQL + "     where �ļ���ʶ = '" + strWJBS + "'" + vbCr                                            'ָ���ļ�
                If strWJLX <> "" Then
                    strSQL = strSQL + "     and   �ļ����� = '" + strWJLX + "'" + vbCr                                        '����������=�ļ���������
                End If
                strSQL = strSQL + "   ) b on a.�ļ���ʶ = b.�ļ���ʶ " + vbCr
                '��ȡ�ļ���Ϣ


                strSQL = strSQL + "   where b.�ļ���ʶ Is Not Null " + vbCr
                strSQL = strSQL + " ) a " + vbCr
                If strWhere <> "" Then
                    strSQL = strSQL + " where " + strWhere + vbCr
                End If
                strSQL = strSQL + " group by " + vbCr
                strSQL = strSQL + "   a.�ļ���ʶ, a.��ˮ��  ," + vbCr
                strSQL = strSQL + "   a.��������, a.����״̬, a.��������, a.�ļ�����, a.�ļ�����," + vbCr
                strSQL = strSQL + "   a.�ļ�����, a.���ش���, a.�ļ����, a.�ļ����, a.���쵥λ," + vbCr
                strSQL = strSQL + "   a.������  , a.������  , a.ί����  , a.����˵��" + vbCr
                strSQL = strSQL + " order by a.�ļ���� desc, a.���ش���, a.�ļ���� desc" + vbCr
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getSQLYBSY_TASK = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �����ļ����͡���ʼ���ڡ��������ڼ�������ļ����ļ�����SQL
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strUserXM              ����ǰ������Ա����
        '     strBLLX                ����������
        '     strWJLX                ���ļ�����-����������
        '     strQSRQ                ����ʼ����
        '     strZZRQ                ����������
        '     strWhere               ����������
        '     strSQL                 ��(����)�ļ�����SQL
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Private Function getSQLGQSY_FILE( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByVal strBLLX As String, _
            ByVal strWJLX As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String, _
            ByVal strWhere As String, _
            ByRef strSQL As String) As Boolean

            getSQLGQSY_FILE = False
            strSQL = ""

            Try
                Dim strFileAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.FileStatusAllYWCList
                Dim strTaskAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TaskStatusAllYWCList
                Dim strLF As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhLf
                Dim strRT As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhRt

                '������ڼ��
                Dim lngDays As Long
                Dim strOP As String
                If strQSRQ <> "" And strZZRQ <> "" Then
                    Dim objDate(2) As System.DateTime
                    objDate(0) = CType(strQSRQ, System.DateTime)
                    objDate(1) = CType(strZZRQ, System.DateTime)
                    If objDate(0) <= objDate(1) Then
                        strOP = "<="
                    Else
                        strOP = ">"
                    End If
                    lngDays = DateDiff(DateInterval.Day, objDate(0), objDate(1))
                    lngDays = Math.Abs(lngDays)
                Else
                    strOP = ""
                End If

                '�ҵ��ļ�
                strSQL = ""
                strSQL = strSQL + " select a.*" + vbCr
                strSQL = strSQL + " from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select" + vbCr
                strSQL = strSQL + "     a.�ļ���ʶ, a.��ˮ��  ," + vbCr
                strSQL = strSQL + "     a.��������, a.����״̬, a.�ļ�����, a.�ļ�����," + vbCr
                strSQL = strSQL + "     a.�ļ�����, a.���͵�λ, a.�ļ��ֺ�, a.�����̶�, a.���ܵȼ�," + vbCr
                strSQL = strSQL + "     a.���ش���, a.�ļ����, a.�ļ����," + vbCr
                strSQL = strSQL + "     a.�����  , a.���쵥λ, a.�����  , a.�������,a.��������," + vbCr
                strSQL = strSQL + "     �������� = max(a.��������)," + vbCr
                strSQL = strSQL + "     �������� = max(a.��������)," + vbCr
                strSQL = strSQL + "     ������� = max(a.�������)," + vbCr
                strSQL = strSQL + "     a.��������" + vbCr
                strSQL = strSQL + "   from" + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select" + vbCr
                strSQL = strSQL + "       a.�ļ���ʶ, b.��ˮ��  ," + vbCr
                strSQL = strSQL + "       a.��������, b.����״̬, b.�ļ�����, b.�ļ�����," + vbCr
                strSQL = strSQL + "       b.�ļ�����, b.���͵�λ, b.�ļ��ֺ�, b.�����̶�, b.���ܵȼ�," + vbCr
                strSQL = strSQL + "       b.���ش���, b.�ļ����, b.�ļ����," + vbCr
                strSQL = strSQL + "       b.�����  , b.���쵥λ, b.�����  , b.�������," + vbCr
                strSQL = strSQL + "       a.��������, a.��������, a.�������, b.��������," + vbCr
                strSQL = strSQL + "       �������� = case when c.�������� is null then '��' else c.�������� end" + vbCr
                strSQL = strSQL + "     from" + vbCr

                '��ȡ�����¼
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select" + vbCr
                strSQL = strSQL + "         �ļ���ʶ, ��������," + vbCr
                strSQL = strSQL + "         �������� = max(��������)," + vbCr
                strSQL = strSQL + "         �������� = max(�����������)," + vbCr
                strSQL = strSQL + "         ������� = max(�������)" + vbCr
                strSQL = strSQL + "       from ����_B_����" + vbCr
                strSQL = strSQL + "       where ������   =    '" + Trim(strUserXM) + "'" + vbCr              '������
                strSQL = strSQL + "       and   ���ӱ�ʶ like '__1%'" + vbCr                                 '�ҿɼ�
                strSQL = strSQL + "       and   ����״̬ not in (" + strTaskAllYWCList + ")" + vbCr          'δ����
                strSQL = strSQL + "       and   ����������� <= '" + Now.ToString("yyyy-MM-dd") + "'" + vbCr '��������
                strSQL = strSQL + "       and   ����������� is not null" + vbCr
                If strBLLX <> "" Then
                    strSQL = strSQL + "       and �������� = '" + strBLLX + "'" + vbCr                       'ָ������
                End If
                If strOP <> "" Then                                                                          'ָ������
                    strSQL = strSQL + "       and datediff(d, �����������, '" + Now.ToString("yyyy-MM-dd") + "') " + strOP + " " + lngDays.ToString() + vbCr
                End If
                strSQL = strSQL + "       group by �ļ���ʶ,��������" + vbCr
                strSQL = strSQL + "     ) a " + vbCr
                '��ȡ�����¼

                '��ȡ��������
                strSQL = strSQL + "     left join" + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select �ļ���ʶ,�������� = case when max(��������) = 1 then '��' else '��' end" + vbCr
                strSQL = strSQL + "       from ����_B_����" + vbCr
                strSQL = strSQL + "       where ������ = '" + strUserXM + "'" + vbCr
                strSQL = strSQL + "       group by �ļ���ʶ" + vbCr
                strSQL = strSQL + "     ) c on a.�ļ���ʶ = c.�ļ���ʶ" + vbCr
                '��ȡ��������


                '��ȡ�ļ���Ϣ
                strSQL = strSQL + "     left join " + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select " + vbCr
                strSQL = strSQL + "         a.�ļ���ʶ, a.��ˮ��  , " + vbCr
                strSQL = strSQL + "         a.��������, a.����״̬, a.�ļ�����, a.�ļ�����," + vbCr
                strSQL = strSQL + "         a.�ļ�����, a.���͵�λ, " + vbCr
                strSQL = strSQL + "         a.�ļ��ֺ�, a.�����̶�, a.���ܵȼ�," + vbCr
                strSQL = strSQL + "         a.���ش���, a.�ļ����, a.�ļ����," + vbCr
                strSQL = strSQL + "         a.�����  , a.���쵥λ, a.�����  , a.�������," + vbCr
                strSQL = strSQL + "         a.��������" + vbCr
                strSQL = strSQL + "       from ����_V_ȫ�������ļ��� a" + vbCr
                strSQL = strSQL + "       left join" + vbCr
                strSQL = strSQL + "       (" + vbCr
                strSQL = strSQL + "         select �ļ���ʶ" + vbCr
                strSQL = strSQL + "         from ����_B_����" + vbCr
                strSQL = strSQL + "         where ������   =    '" + Trim(strUserXM) + "'" + vbCr              '������
                strSQL = strSQL + "         and   ���ӱ�ʶ like '__1%'" + vbCr                                 '�ҿɼ�
                strSQL = strSQL + "         and   ����״̬ not in (" + strTaskAllYWCList + ")" + vbCr          'δ����
                strSQL = strSQL + "         and   ����������� <= '" + Now.ToString("yyyy-MM-dd") + "'" + vbCr '��������
                strSQL = strSQL + "         and   ����������� is not null" + vbCr
                If strBLLX <> "" Then
                    strSQL = strSQL + "         and �������� = '" + strBLLX + "'" + vbCr                       'ָ������
                End If
                If strOP <> "" Then                                                                            'ָ������
                    strSQL = strSQL + "         and datediff(d, �����������, '" + Now.ToString("yyyy-MM-dd") + "') " + strOP + " " + lngDays.ToString() + vbCr
                End If
                strSQL = strSQL + "         group by �ļ���ʶ" + vbCr
                strSQL = strSQL + "       ) b on a.�ļ���ʶ = b.�ļ���ʶ" + vbCr
                strSQL = strSQL + "       where b.�ļ���ʶ is not null" + vbCr
                If strWJLX <> "" Then
                    strSQL = strSQL + "       and a.�ļ����� = '" + strWJLX + "'" + vbCr
                End If
                strSQL = strSQL + "     ) b on a.�ļ���ʶ = b.�ļ���ʶ " + vbCr
                '��ȡ�ļ���Ϣ


                strSQL = strSQL + "     where b.�ļ���ʶ Is Not Null " + vbCr
                strSQL = strSQL + "   ) a " + vbCr
                strSQL = strSQL + "   group by " + vbCr
                strSQL = strSQL + "     a.�ļ���ʶ, a.��ˮ��  ," + vbCr
                strSQL = strSQL + "     a.��������, a.����״̬, a.�ļ�����, a.�ļ�����," + vbCr
                strSQL = strSQL + "     a.�ļ�����, a.���͵�λ, a.�ļ��ֺ�, a.�����̶�, a.���ܵȼ�," + vbCr
                strSQL = strSQL + "     a.���ش���, a.�ļ����, a.�ļ����," + vbCr
                strSQL = strSQL + "     a.�����  , a.���쵥λ, a.�����  , a.�������, a.��������, a.��������" + vbCr
                strSQL = strSQL + " ) a" + vbCr
                If strWhere <> "" Then
                    strSQL = strSQL + " where " + strWhere + vbCr
                End If
                strSQL = strSQL + " order by a.�������� desc, a.�ļ���� desc, a.���ش���, a.�ļ���� desc" + vbCr
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getSQLGQSY_FILE = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �����ļ����͡���ʼ���ڡ��������ڼ���������˵���������SQL
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strWJBS                ��Ҫ�鿴���ļ���ʶ
        '     strUserXM              ����ǰ������Ա����
        '     strBLLX                ����������
        '     strWJLX                ���ļ�����-����������
        '     strQSRQ                ����ʼ����
        '     strZZRQ                ����������
        '     strWhere               ����������
        '     strSQL                 ��(����)��������SQL
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Private Function getSQLGQSY_TASK( _
            ByRef strErrMsg As String, _
            ByVal strWJBS As String, _
            ByVal strUserXM As String, _
            ByVal strBLLX As String, _
            ByVal strWJLX As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String, _
            ByVal strWhere As String, _
            ByRef strSQL As String) As Boolean

            getSQLGQSY_TASK = False
            strSQL = ""

            Try
                Dim strFileAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.FileStatusAllYWCList
                Dim strTaskAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TaskStatusAllYWCList
                Dim strLF As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhLf
                Dim strRT As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhRt
                Dim strGWTHCL As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_THCL
                Dim strGWSHCL As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_SHCL
                Dim strGWHFCL As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_HFCL

                '������ڼ��
                Dim lngDays As Long
                Dim strOP As String
                If strQSRQ <> "" And strZZRQ <> "" Then
                    Dim objDate(2) As System.DateTime
                    objDate(0) = CType(strQSRQ, System.DateTime)
                    objDate(1) = CType(strZZRQ, System.DateTime)
                    If objDate(0) <= objDate(1) Then
                        strOP = "<="
                    Else
                        strOP = ">"
                    End If
                    lngDays = DateDiff(DateInterval.Day, objDate(0), objDate(1))
                    lngDays = Math.Abs(lngDays)
                Else
                    strOP = ""
                End If

                '�ҵ��ļ�
                strSQL = ""
                strSQL = strSQL + " select * from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select" + vbCr
                strSQL = strSQL + "     a.�ļ���ʶ, b.��ˮ��  ," + vbCr
                strSQL = strSQL + "     a.��������, a.����״̬, a.��������, b.�ļ�����, b.�ļ�����," + vbCr
                strSQL = strSQL + "     b.�ļ�����, b.���ش���, b.�ļ����, b.�ļ����, b.���쵥λ," + vbCr
                strSQL = strSQL + "     a.������  , a.������  , a.ί����  , a.����˵��" + vbCr
                strSQL = strSQL + "   from" + vbCr

                '��ȡ�����¼
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select" + vbCr
                strSQL = strSQL + "       �ļ���ʶ, ��������, ����״̬," + vbCr
                strSQL = strSQL + "       �������� = case " + vbCr
                strSQL = strSQL + "         when ���ӱ�ʶ like '___1%'    then '" + strGWTHCL + "' " + vbCr
                strSQL = strSQL + "         when ���ӱ�ʶ like '____1%'   then '" + strGWSHCL + "' " + vbCr
                strSQL = strSQL + "         when ���ӱ�ʶ like '______1%' then '" + strGWHFCL + "' " + vbCr
                strSQL = strSQL + "         else �������� end," + vbCr
                strSQL = strSQL + "       ������, ������, ί����, ���ӱ�ʶ, ����˵�� " + vbCr
                strSQL = strSQL + "     from ����_B_����" + vbCr
                strSQL = strSQL + "     where �ļ���ʶ = '" + strWJBS + "'" + vbCr                         'ָ���ļ�
                strSQL = strSQL + "     and   ������   = '" + Trim(strUserXM) + "'" + vbCr                 '������
                strSQL = strSQL + "     and   ���ӱ�ʶ like '__1%'" + vbCr                                 '�ҿɼ�
                strSQL = strSQL + "     and   ����״̬ not in (" + strTaskAllYWCList + ")" + vbCr          'δ����
                strSQL = strSQL + "     and   ����������� <= '" + Now.ToString("yyyy-MM-dd") + "'" + vbCr '��������
                strSQL = strSQL + "     and   ����������� is not null" + vbCr
                If strBLLX <> "" Then
                    strSQL = strSQL + "     and �������� = '" + strBLLX + "'" + vbCr                       'ָ������
                End If
                If strOP <> "" Then                                                                        'ָ������
                    strSQL = strSQL + "     and datediff(d, �����������, '" + Now.ToString("yyyy-MM-dd") + "') " + strOP + " " + lngDays.ToString() + vbCr
                End If
                strSQL = strSQL + "   ) a " + vbCr
                '��ȡ�����¼


                '��ȡ�ļ���Ϣ
                strSQL = strSQL + "   left join " + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select " + vbCr
                strSQL = strSQL + "       �ļ���ʶ, ��ˮ��  , " + vbCr
                strSQL = strSQL + "       ��������, ����״̬, �ļ�����, �ļ�����," + vbCr
                strSQL = strSQL + "       �ļ�����, ���͵�λ, " + vbCr
                strSQL = strSQL + "       �ļ��ֺ�, �����̶�, ���ܵȼ�," + vbCr
                strSQL = strSQL + "       ���ش���, �ļ����, �ļ����," + vbCr
                strSQL = strSQL + "       �����  , ���쵥λ, �����  , �������," + vbCr
                strSQL = strSQL + "       ��������" + vbCr
                strSQL = strSQL + "     from ����_V_ȫ�������ļ��� " + vbCr
                strSQL = strSQL + "     where �ļ���ʶ = '" + strWJBS + "'" + vbCr                                            'ָ���ļ�
                If strWJLX <> "" Then
                    strSQL = strSQL + "     and   �ļ����� = '" + strWJLX + "'" + vbCr                                        '����������=�ļ���������
                End If
                strSQL = strSQL + "   ) b on a.�ļ���ʶ = b.�ļ���ʶ " + vbCr
                '��ȡ�ļ���Ϣ


                strSQL = strSQL + "   where b.�ļ���ʶ Is Not Null " + vbCr
                strSQL = strSQL + " ) a " + vbCr
                If strWhere <> "" Then
                    strSQL = strSQL + " where " + strWhere + vbCr
                End If
                strSQL = strSQL + " group by " + vbCr
                strSQL = strSQL + "   a.�ļ���ʶ, a.��ˮ��  ," + vbCr
                strSQL = strSQL + "   a.��������, a.����״̬, a.��������, a.�ļ�����, a.�ļ�����," + vbCr
                strSQL = strSQL + "   a.�ļ�����, a.���ش���, a.�ļ����, a.�ļ����, a.���쵥λ," + vbCr
                strSQL = strSQL + "   a.������  , a.������  , a.ί����  , a.����˵��" + vbCr
                strSQL = strSQL + " order by a.�ļ���� desc, a.���ش���, a.�ļ���� desc" + vbCr
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getSQLGQSY_TASK = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �����ļ����͡���ʼ���ڡ��������ڼ���߰��ļ����ļ�����SQL
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strUserXM              ����ǰ������Ա����
        '     strBLLX                ����������
        '     strWJLX                ���ļ�����-����������
        '     strQSRQ                ����ʼ����
        '     strZZRQ                ����������
        '     strWhere               ����������
        '     strSQL                 ��(����)�ļ�����SQL
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Private Function getSQLCBSY_FILE( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByVal strBLLX As String, _
            ByVal strWJLX As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String, _
            ByVal strWhere As String, _
            ByRef strSQL As String) As Boolean

            getSQLCBSY_FILE = False
            strSQL = ""

            Try
                Dim strTASK_CBWJ As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_CBWJ
                Dim strLF As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhLf
                Dim strRT As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhRt

                '������ڼ��
                Dim lngDays As Long
                Dim strOP As String
                If strQSRQ <> "" And strZZRQ <> "" Then
                    Dim objDate(2) As System.DateTime
                    objDate(0) = CType(strQSRQ, System.DateTime)
                    objDate(1) = CType(strZZRQ, System.DateTime)
                    If objDate(0) <= objDate(1) Then
                        strOP = "<="
                    Else
                        strOP = ">"
                    End If
                    lngDays = DateDiff(DateInterval.Day, objDate(0), objDate(1))
                    lngDays = Math.Abs(lngDays)
                Else
                    strOP = ""
                End If

                '�ҵ��ļ�
                strSQL = ""
                strSQL = strSQL + " select a.*" + vbCr
                strSQL = strSQL + " from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select" + vbCr
                strSQL = strSQL + "     a.�ļ���ʶ, a.��ˮ��  ," + vbCr
                strSQL = strSQL + "     a.��������, a.����״̬, a.�ļ�����, a.�ļ�����," + vbCr
                strSQL = strSQL + "     a.�ļ�����, a.���͵�λ, a.�ļ��ֺ�, a.�����̶�, a.���ܵȼ�," + vbCr
                strSQL = strSQL + "     a.���ش���, a.�ļ����, a.�ļ����," + vbCr
                strSQL = strSQL + "     a.�����  , a.���쵥λ, a.�����  , a.�������,a.��������," + vbCr
                strSQL = strSQL + "     �������� = max(a.��������)," + vbCr
                strSQL = strSQL + "     �������� = NULL," + vbCr
                strSQL = strSQL + "     ������� = NULL," + vbCr
                strSQL = strSQL + "     a.��������" + vbCr
                strSQL = strSQL + "   from" + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select" + vbCr
                strSQL = strSQL + "       a.�ļ���ʶ, b.��ˮ��  ," + vbCr
                strSQL = strSQL + "       b.��������, b.����״̬, b.�ļ�����, b.�ļ�����," + vbCr
                strSQL = strSQL + "       b.�ļ�����, b.���͵�λ, b.�ļ��ֺ�, b.�����̶�, b.���ܵȼ�," + vbCr
                strSQL = strSQL + "       b.���ش���, b.�ļ����, b.�ļ����," + vbCr
                strSQL = strSQL + "       b.�����  , b.���쵥λ, b.�����  , b.�������," + vbCr
                strSQL = strSQL + "       a.��������, a.��������, a.�������, b.��������," + vbCr
                strSQL = strSQL + "       �������� = case when c.�������� is null then '��' else c.�������� end" + vbCr
                strSQL = strSQL + "     from" + vbCr

                '��ȡ�����¼
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select" + vbCr
                strSQL = strSQL + "         �ļ���ʶ," + vbCr
                strSQL = strSQL + "         �������� = min(�߰�����)," + vbCr
                strSQL = strSQL + "         �������� = NULL," + vbCr
                strSQL = strSQL + "         ������� = NULL" + vbCr
                strSQL = strSQL + "       from ����_B_�߰�" + vbCr
                strSQL = strSQL + "       where �߰��� = '" + Trim(strUserXM) + "'" + vbCr                   '�Ҵ߰�
                strSQL = strSQL + "       and   �߰����� is not null" + vbCr
                If strOP <> "" Then                                                                          'ָ������
                    strSQL = strSQL + "       and abs(datediff(d, �߰�����, '" + Now.ToString("yyyy-MM-dd") + "')) " + strOP + " " + lngDays.ToString() + vbCr
                End If
                strSQL = strSQL + "       group by �ļ���ʶ" + vbCr
                strSQL = strSQL + "     ) a " + vbCr
                '��ȡ�����¼

                '��ȡ��������
                strSQL = strSQL + "     left join" + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select �ļ���ʶ,�������� = case when max(��������) = 1 then '��' else '��' end" + vbCr
                strSQL = strSQL + "       from ����_B_����" + vbCr
                strSQL = strSQL + "       where ������ = '" + strUserXM + "'" + vbCr
                strSQL = strSQL + "       group by �ļ���ʶ" + vbCr
                strSQL = strSQL + "     ) c on a.�ļ���ʶ = c.�ļ���ʶ" + vbCr
                '��ȡ��������


                '��ȡ�ļ���Ϣ
                strSQL = strSQL + "     left join " + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select " + vbCr
                strSQL = strSQL + "         a.�ļ���ʶ, a.��ˮ��  , " + vbCr
                strSQL = strSQL + "         a.��������, a.����״̬, a.�ļ�����, �������� = '" + strTASK_CBWJ + "', a.�ļ�����," + vbCr
                strSQL = strSQL + "         a.�ļ�����, a.���͵�λ, " + vbCr
                strSQL = strSQL + "         a.�ļ��ֺ�, a.�����̶�, a.���ܵȼ�," + vbCr
                strSQL = strSQL + "         a.���ش���, a.�ļ����, a.�ļ����," + vbCr
                strSQL = strSQL + "         a.�����  , a.���쵥λ, a.�����  , a.�������," + vbCr
                strSQL = strSQL + "         a.��������" + vbCr
                strSQL = strSQL + "       from ����_V_ȫ�������ļ��� a" + vbCr
                strSQL = strSQL + "       left join" + vbCr
                strSQL = strSQL + "       (" + vbCr
                strSQL = strSQL + "         select �ļ���ʶ" + vbCr
                strSQL = strSQL + "         from ����_B_�߰�" + vbCr
                strSQL = strSQL + "         where �߰��� = '" + Trim(strUserXM) + "'" + vbCr                   '�Ҵ߰�
                strSQL = strSQL + "         and   �߰����� is not null" + vbCr
                If strOP <> "" Then                                                                            'ָ������
                    strSQL = strSQL + "         and abs(datediff(d, �߰�����, '" + Now.ToString("yyyy-MM-dd") + "')) " + strOP + " " + lngDays.ToString() + vbCr
                End If
                strSQL = strSQL + "         group by �ļ���ʶ" + vbCr
                strSQL = strSQL + "       ) b on a.�ļ���ʶ = b.�ļ���ʶ" + vbCr
                strSQL = strSQL + "       where b.�ļ���ʶ is not null" + vbCr
                If strWJLX <> "" Then
                    strSQL = strSQL + "       and a.�ļ����� = '" + strWJLX + "'" + vbCr
                End If
                strSQL = strSQL + "     ) b on a.�ļ���ʶ = b.�ļ���ʶ " + vbCr
                '��ȡ�ļ���Ϣ


                strSQL = strSQL + "     where b.�ļ���ʶ Is Not Null " + vbCr
                strSQL = strSQL + "   ) a " + vbCr
                strSQL = strSQL + "   group by " + vbCr
                strSQL = strSQL + "     a.�ļ���ʶ, a.��ˮ��  ," + vbCr
                strSQL = strSQL + "     a.��������, a.����״̬, a.�ļ�����, a.�ļ�����," + vbCr
                strSQL = strSQL + "     a.�ļ�����, a.���͵�λ, a.�ļ��ֺ�, a.�����̶�, a.���ܵȼ�," + vbCr
                strSQL = strSQL + "     a.���ش���, a.�ļ����, a.�ļ����," + vbCr
                strSQL = strSQL + "     a.�����  , a.���쵥λ, a.�����  , a.�������, a.��������, a.��������" + vbCr
                strSQL = strSQL + " ) a" + vbCr
                If strWhere <> "" Then
                    strSQL = strSQL + " where " + strWhere + vbCr
                End If
                strSQL = strSQL + " order by a.�������� desc, a.�ļ���� desc, a.���ش���, a.�ļ���� desc" + vbCr
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getSQLCBSY_FILE = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �����ļ����͡���ʼ���ڡ��������ڼ���߰��ļ�����������SQL
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strWJBS                ��Ҫ�鿴���ļ���ʶ
        '     strUserXM              ����ǰ������Ա����
        '     strBLLX                ����������
        '     strWJLX                ���ļ�����-����������
        '     strQSRQ                ����ʼ����
        '     strZZRQ                ����������
        '     strWhere               ����������
        '     strSQL                 ��(����)��������SQL
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Private Function getSQLCBSY_TASK( _
            ByRef strErrMsg As String, _
            ByVal strWJBS As String, _
            ByVal strUserXM As String, _
            ByVal strBLLX As String, _
            ByVal strWJLX As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String, _
            ByVal strWhere As String, _
            ByRef strSQL As String) As Boolean

            getSQLCBSY_TASK = False
            strSQL = ""

            Try
                Dim strTASKSTATUS_ZJB As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASKSTATUS_ZJB
                Dim strTASK_CBWJ As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_CBWJ
                Dim strLF As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhLf
                Dim strRT As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhRt

                '������ڼ��
                Dim lngDays As Long
                Dim strOP As String
                If strQSRQ <> "" And strZZRQ <> "" Then
                    Dim objDate(2) As System.DateTime
                    objDate(0) = CType(strQSRQ, System.DateTime)
                    objDate(1) = CType(strZZRQ, System.DateTime)
                    If objDate(0) <= objDate(1) Then
                        strOP = "<="
                    Else
                        strOP = ">"
                    End If
                    lngDays = DateDiff(DateInterval.Day, objDate(0), objDate(1))
                    lngDays = Math.Abs(lngDays)
                Else
                    strOP = ""
                End If

                '�ҵ��ļ�
                strSQL = ""
                strSQL = strSQL + " select * from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select" + vbCr
                strSQL = strSQL + "     a.�ļ���ʶ, b.��ˮ��  ," + vbCr
                strSQL = strSQL + "     b.��������, a.����״̬, a.��������, b.�ļ�����, b.�ļ�����," + vbCr
                strSQL = strSQL + "     b.�ļ�����, b.���ش���, b.�ļ����, b.�ļ����, b.���쵥λ," + vbCr
                strSQL = strSQL + "     a.������  , a.������  , a.ί����  , a.����˵��" + vbCr
                strSQL = strSQL + "   from" + vbCr

                '��ȡ�����¼
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select" + vbCr
                strSQL = strSQL + "      �ļ���ʶ, ����״̬ = '" + strTASKSTATUS_ZJB + "', �������� = '" + strTASK_CBWJ + "',"
                strSQL = strSQL + "      ������ = �߰���, ������ = ���߰���, ί���� = ' ', ����˵�� = �߰�˵��"
                strSQL = strSQL + "     from ����_B_�߰�" + vbCr
                strSQL = strSQL + "     where �ļ���ʶ = '" + strWJBS + "'" + vbCr                         'ָ���ļ�
                strSQL = strSQL + "     and   �߰���   = '" + Trim(strUserXM) + "'" + vbCr                 '�Ҵ߰�
                strSQL = strSQL + "     and   �߰����� is not null" + vbCr
                If strOP <> "" Then                                                                        'ָ������
                    strSQL = strSQL + "     and abs(datediff(d, �߰�����, '" + Now.ToString("yyyy-MM-dd") + "')) " + strOP + " " + lngDays.ToString() + vbCr
                End If
                strSQL = strSQL + "   ) a " + vbCr
                '��ȡ�����¼


                '��ȡ�ļ���Ϣ
                strSQL = strSQL + "   left join " + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select " + vbCr
                strSQL = strSQL + "       �ļ���ʶ, ��ˮ��  , " + vbCr
                strSQL = strSQL + "       ��������, ����״̬, �ļ�����, �ļ�����," + vbCr
                strSQL = strSQL + "       �ļ�����, ���͵�λ, " + vbCr
                strSQL = strSQL + "       �ļ��ֺ�, �����̶�, ���ܵȼ�," + vbCr
                strSQL = strSQL + "       ���ش���, �ļ����, �ļ����," + vbCr
                strSQL = strSQL + "       �����  , ���쵥λ, �����  , �������," + vbCr
                strSQL = strSQL + "       ��������" + vbCr
                strSQL = strSQL + "     from ����_V_ȫ�������ļ��� " + vbCr
                strSQL = strSQL + "     where �ļ���ʶ = '" + strWJBS + "'" + vbCr                                            'ָ���ļ�
                If strWJLX <> "" Then
                    strSQL = strSQL + "     and   �ļ����� = '" + strWJLX + "'" + vbCr                                        '����������=�ļ���������
                End If
                strSQL = strSQL + "   ) b on a.�ļ���ʶ = b.�ļ���ʶ " + vbCr
                '��ȡ�ļ���Ϣ


                strSQL = strSQL + "   where b.�ļ���ʶ Is Not Null " + vbCr
                strSQL = strSQL + " ) a " + vbCr
                If strWhere <> "" Then
                    strSQL = strSQL + " where " + strWhere + vbCr
                End If
                strSQL = strSQL + " group by " + vbCr
                strSQL = strSQL + "   a.�ļ���ʶ, a.��ˮ��  ," + vbCr
                strSQL = strSQL + "   a.��������, a.����״̬, a.��������, a.�ļ�����, a.�ļ�����," + vbCr
                strSQL = strSQL + "   a.�ļ�����, a.���ش���, a.�ļ����, a.�ļ����, a.���쵥λ," + vbCr
                strSQL = strSQL + "   a.������  , a.������  , a.ί����  , a.����˵��" + vbCr
                strSQL = strSQL + " order by a.�ļ���� desc, a.���ش���, a.�ļ���� desc" + vbCr
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getSQLCBSY_TASK = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �����ļ����͡���ʼ���ڡ��������ڼ��㱻�߰��ļ����ļ�����SQL
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strUserXM              ����ǰ������Ա����
        '     strBLLX                ����������
        '     strWJLX                ���ļ�����-����������
        '     strQSRQ                ����ʼ����
        '     strZZRQ                ����������
        '     strWhere               ����������
        '     strSQL                 ��(����)�ļ�����SQL
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Private Function getSQLBCSY_FILE( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByVal strBLLX As String, _
            ByVal strWJLX As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String, _
            ByVal strWhere As String, _
            ByRef strSQL As String) As Boolean

            getSQLBCSY_FILE = False
            strSQL = ""

            Try
                Dim strTASK_CBWJ As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_CBWJ
                Dim strLF As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhLf
                Dim strRT As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhRt

                '������ڼ��
                Dim lngDays As Long
                Dim strOP As String
                If strQSRQ <> "" And strZZRQ <> "" Then
                    Dim objDate(2) As System.DateTime
                    objDate(0) = CType(strQSRQ, System.DateTime)
                    objDate(1) = CType(strZZRQ, System.DateTime)
                    If objDate(0) <= objDate(1) Then
                        strOP = "<="
                    Else
                        strOP = ">"
                    End If
                    lngDays = DateDiff(DateInterval.Day, objDate(0), objDate(1))
                    lngDays = Math.Abs(lngDays)
                Else
                    strOP = ""
                End If

                '�ҵ��ļ�
                strSQL = ""
                strSQL = strSQL + " select a.*" + vbCr
                strSQL = strSQL + " from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select" + vbCr
                strSQL = strSQL + "     a.�ļ���ʶ, a.��ˮ��  ," + vbCr
                strSQL = strSQL + "     a.��������, a.����״̬, a.�ļ�����, a.�ļ�����," + vbCr
                strSQL = strSQL + "     a.�ļ�����, a.���͵�λ, a.�ļ��ֺ�, a.�����̶�, a.���ܵȼ�," + vbCr
                strSQL = strSQL + "     a.���ش���, a.�ļ����, a.�ļ����," + vbCr
                strSQL = strSQL + "     a.�����  , a.���쵥λ, a.�����  , a.�������,a.��������," + vbCr
                strSQL = strSQL + "     �������� = max(a.��������)," + vbCr
                strSQL = strSQL + "     �������� = NULL," + vbCr
                strSQL = strSQL + "     ������� = NULL," + vbCr
                strSQL = strSQL + "     a.��������" + vbCr
                strSQL = strSQL + "   from" + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select" + vbCr
                strSQL = strSQL + "       a.�ļ���ʶ, b.��ˮ��  ," + vbCr
                strSQL = strSQL + "       b.��������, b.����״̬, b.�ļ�����, b.�ļ�����," + vbCr
                strSQL = strSQL + "       b.�ļ�����, b.���͵�λ, b.�ļ��ֺ�, b.�����̶�, b.���ܵȼ�," + vbCr
                strSQL = strSQL + "       b.���ش���, b.�ļ����, b.�ļ����," + vbCr
                strSQL = strSQL + "       b.�����  , b.���쵥λ, b.�����  , b.�������," + vbCr
                strSQL = strSQL + "       a.��������, a.��������, a.�������, b.��������," + vbCr
                strSQL = strSQL + "       �������� = case when c.�������� is null then '��' else c.�������� end" + vbCr
                strSQL = strSQL + "     from" + vbCr

                '��ȡ�����¼
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select" + vbCr
                strSQL = strSQL + "         �ļ���ʶ," + vbCr
                strSQL = strSQL + "         �������� = min(�߰�����)," + vbCr
                strSQL = strSQL + "         �������� = NULL," + vbCr
                strSQL = strSQL + "         ������� = NULL" + vbCr
                strSQL = strSQL + "       from ����_B_�߰�" + vbCr
                strSQL = strSQL + "       where ���߰��� = '" + Trim(strUserXM) + "'" + vbCr                 '�ұ��߰�
                strSQL = strSQL + "       and   �߰����� is not null" + vbCr
                If strOP <> "" Then                                                                          'ָ������
                    strSQL = strSQL + "       and abs(datediff(d, �߰�����, '" + Now.ToString("yyyy-MM-dd") + "')) " + strOP + " " + lngDays.ToString() + vbCr
                End If
                strSQL = strSQL + "       group by �ļ���ʶ" + vbCr
                strSQL = strSQL + "     ) a " + vbCr
                '��ȡ�����¼

                '��ȡ��������
                strSQL = strSQL + "     left join" + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select �ļ���ʶ,�������� = case when max(��������) = 1 then '��' else '��' end" + vbCr
                strSQL = strSQL + "       from ����_B_����" + vbCr
                strSQL = strSQL + "       where ������ = '" + strUserXM + "'" + vbCr
                strSQL = strSQL + "       group by �ļ���ʶ" + vbCr
                strSQL = strSQL + "     ) c on a.�ļ���ʶ = c.�ļ���ʶ" + vbCr
                '��ȡ��������


                '��ȡ�ļ���Ϣ
                strSQL = strSQL + "     left join " + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select " + vbCr
                strSQL = strSQL + "         a.�ļ���ʶ, a.��ˮ��  , " + vbCr
                strSQL = strSQL + "         a.��������, a.����״̬, a.�ļ�����, �������� = '" + strTASK_CBWJ + "', a.�ļ�����," + vbCr
                strSQL = strSQL + "         a.�ļ�����, a.���͵�λ, " + vbCr
                strSQL = strSQL + "         a.�ļ��ֺ�, a.�����̶�, a.���ܵȼ�," + vbCr
                strSQL = strSQL + "         a.���ش���, a.�ļ����, a.�ļ����," + vbCr
                strSQL = strSQL + "         a.�����  , a.���쵥λ, a.�����  , a.�������," + vbCr
                strSQL = strSQL + "         a.��������" + vbCr
                strSQL = strSQL + "       from ����_V_ȫ�������ļ��� a" + vbCr
                strSQL = strSQL + "       left join" + vbCr
                strSQL = strSQL + "       (" + vbCr
                strSQL = strSQL + "         select �ļ���ʶ" + vbCr
                strSQL = strSQL + "         from ����_B_�߰�" + vbCr
                strSQL = strSQL + "         where ���߰��� = '" + Trim(strUserXM) + "'" + vbCr                 '�ұ��߰�
                strSQL = strSQL + "         and   �߰����� is not null" + vbCr
                If strOP <> "" Then                                                                            'ָ������
                    strSQL = strSQL + "         and abs(datediff(d, �߰�����, '" + Now.ToString("yyyy-MM-dd") + "')) " + strOP + " " + lngDays.ToString() + vbCr
                End If
                strSQL = strSQL + "         group by �ļ���ʶ" + vbCr
                strSQL = strSQL + "       ) b on a.�ļ���ʶ = b.�ļ���ʶ" + vbCr
                strSQL = strSQL + "       where b.�ļ���ʶ is not null" + vbCr
                If strWJLX <> "" Then
                    strSQL = strSQL + "       and a.�ļ����� = '" + strWJLX + "'" + vbCr
                End If
                strSQL = strSQL + "     ) b on a.�ļ���ʶ = b.�ļ���ʶ " + vbCr
                '��ȡ�ļ���Ϣ


                strSQL = strSQL + "     where b.�ļ���ʶ Is Not Null " + vbCr
                strSQL = strSQL + "   ) a " + vbCr
                strSQL = strSQL + "   group by " + vbCr
                strSQL = strSQL + "     a.�ļ���ʶ, a.��ˮ��  ," + vbCr
                strSQL = strSQL + "     a.��������, a.����״̬, a.�ļ�����, a.�ļ�����," + vbCr
                strSQL = strSQL + "     a.�ļ�����, a.���͵�λ, a.�ļ��ֺ�, a.�����̶�, a.���ܵȼ�," + vbCr
                strSQL = strSQL + "     a.���ش���, a.�ļ����, a.�ļ����," + vbCr
                strSQL = strSQL + "     a.�����  , a.���쵥λ, a.�����  , a.�������, a.��������, a.��������" + vbCr
                strSQL = strSQL + " ) a" + vbCr
                If strWhere <> "" Then
                    strSQL = strSQL + " where " + strWhere + vbCr
                End If
                strSQL = strSQL + " order by a.�������� desc, a.�ļ���� desc, a.���ش���, a.�ļ���� desc" + vbCr
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getSQLBCSY_FILE = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �����ļ����͡���ʼ���ڡ��������ڼ��㱻�߰��ļ�����������SQL
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strWJBS                ��Ҫ�鿴���ļ���ʶ
        '     strUserXM              ����ǰ������Ա����
        '     strBLLX                ����������
        '     strWJLX                ���ļ�����-����������
        '     strQSRQ                ����ʼ����
        '     strZZRQ                ����������
        '     strWhere               ����������
        '     strSQL                 ��(����)��������SQL
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Private Function getSQLBCSY_TASK( _
            ByRef strErrMsg As String, _
            ByVal strWJBS As String, _
            ByVal strUserXM As String, _
            ByVal strBLLX As String, _
            ByVal strWJLX As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String, _
            ByVal strWhere As String, _
            ByRef strSQL As String) As Boolean

            getSQLBCSY_TASK = False
            strSQL = ""

            Try
                Dim strTASKSTATUS_ZJB As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASKSTATUS_ZJB
                Dim strTASK_CBWJ As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_CBWJ
                Dim strLF As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhLf
                Dim strRT As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhRt

                '������ڼ��
                Dim lngDays As Long
                Dim strOP As String
                If strQSRQ <> "" And strZZRQ <> "" Then
                    Dim objDate(2) As System.DateTime
                    objDate(0) = CType(strQSRQ, System.DateTime)
                    objDate(1) = CType(strZZRQ, System.DateTime)
                    If objDate(0) <= objDate(1) Then
                        strOP = "<="
                    Else
                        strOP = ">"
                    End If
                    lngDays = DateDiff(DateInterval.Day, objDate(0), objDate(1))
                    lngDays = Math.Abs(lngDays)
                Else
                    strOP = ""
                End If

                '�ҵ��ļ�
                strSQL = ""
                strSQL = strSQL + " select * from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select" + vbCr
                strSQL = strSQL + "     a.�ļ���ʶ, b.��ˮ��  ," + vbCr
                strSQL = strSQL + "     b.��������, a.����״̬, a.��������, b.�ļ�����, b.�ļ�����," + vbCr
                strSQL = strSQL + "     b.�ļ�����, b.���ش���, b.�ļ����, b.�ļ����, b.���쵥λ," + vbCr
                strSQL = strSQL + "     a.������  , a.������  , a.ί����  , a.����˵��" + vbCr
                strSQL = strSQL + "   from" + vbCr

                '��ȡ�����¼
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select" + vbCr
                strSQL = strSQL + "      �ļ���ʶ, ����״̬ = '" + strTASKSTATUS_ZJB + "', �������� = '" + strTASK_CBWJ + "',"
                strSQL = strSQL + "      ������ = �߰���, ������ = ���߰���, ί���� = ' ', ����˵�� = �߰�˵��"
                strSQL = strSQL + "     from ����_B_�߰�" + vbCr
                strSQL = strSQL + "     where �ļ���ʶ = '" + strWJBS + "'" + vbCr                         'ָ���ļ�
                strSQL = strSQL + "     and   ���߰��� = '" + Trim(strUserXM) + "'" + vbCr                 '�ұ��߰�
                strSQL = strSQL + "     and   �߰����� is not null" + vbCr
                If strOP <> "" Then                                                                        'ָ������
                    strSQL = strSQL + "       and abs(datediff(d, �߰�����, '" + Now.ToString("yyyy-MM-dd") + "')) " + strOP + " " + lngDays.ToString() + vbCr
                End If
                strSQL = strSQL + "   ) a " + vbCr
                '��ȡ�����¼


                '��ȡ�ļ���Ϣ
                strSQL = strSQL + "   left join " + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select " + vbCr
                strSQL = strSQL + "       �ļ���ʶ, ��ˮ��  , " + vbCr
                strSQL = strSQL + "       ��������, ����״̬, �ļ�����, �ļ�����," + vbCr
                strSQL = strSQL + "       �ļ�����, ���͵�λ, " + vbCr
                strSQL = strSQL + "       �ļ��ֺ�, �����̶�, ���ܵȼ�," + vbCr
                strSQL = strSQL + "       ���ش���, �ļ����, �ļ����," + vbCr
                strSQL = strSQL + "       �����  , ���쵥λ, �����  , �������," + vbCr
                strSQL = strSQL + "       ��������" + vbCr
                strSQL = strSQL + "     from ����_V_ȫ�������ļ��� " + vbCr
                strSQL = strSQL + "     where �ļ���ʶ = '" + strWJBS + "'" + vbCr                                            'ָ���ļ�
                If strWJLX <> "" Then
                    strSQL = strSQL + "     and   �ļ����� = '" + strWJLX + "'" + vbCr                                        '����������=�ļ���������
                End If
                strSQL = strSQL + "   ) b on a.�ļ���ʶ = b.�ļ���ʶ " + vbCr
                '��ȡ�ļ���Ϣ


                strSQL = strSQL + "   where b.�ļ���ʶ Is Not Null " + vbCr
                strSQL = strSQL + " ) a " + vbCr
                If strWhere <> "" Then
                    strSQL = strSQL + " where " + strWhere + vbCr
                End If
                strSQL = strSQL + " group by " + vbCr
                strSQL = strSQL + "   a.�ļ���ʶ, a.��ˮ��  ," + vbCr
                strSQL = strSQL + "   a.��������, a.����״̬, a.��������, a.�ļ�����, a.�ļ�����," + vbCr
                strSQL = strSQL + "   a.�ļ�����, a.���ش���, a.�ļ����, a.�ļ����, a.���쵥λ," + vbCr
                strSQL = strSQL + "   a.������  , a.������  , a.ί����  , a.����˵��" + vbCr
                strSQL = strSQL + " order by a.�ļ���� desc, a.���ش���, a.�ļ���� desc" + vbCr
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getSQLBCSY_TASK = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �����ļ����͡���ʼ���ڡ��������ڼ��㶽���ļ����ļ�����SQL
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strUserXM              ����ǰ������Ա����
        '     strBLLX                ����������
        '     strWJLX                ���ļ�����-����������
        '     strQSRQ                ����ʼ����
        '     strZZRQ                ����������
        '     strWhere               ����������
        '     strSQL                 ��(����)�ļ�����SQL
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Private Function getSQLDBWJ_FILE( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByVal strBLLX As String, _
            ByVal strWJLX As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String, _
            ByVal strWhere As String, _
            ByRef strSQL As String) As Boolean

            getSQLDBWJ_FILE = False
            strSQL = ""

            Try
                Dim strTASK_DBWJ As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_DBWJ
                Dim strLF As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhLf
                Dim strRT As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhRt

                '������ڼ��
                Dim lngDays As Long
                Dim strOP As String
                If strQSRQ <> "" And strZZRQ <> "" Then
                    Dim objDate(2) As System.DateTime
                    objDate(0) = CType(strQSRQ, System.DateTime)
                    objDate(1) = CType(strZZRQ, System.DateTime)
                    If objDate(0) <= objDate(1) Then
                        strOP = "<="
                    Else
                        strOP = ">"
                    End If
                    lngDays = DateDiff(DateInterval.Day, objDate(0), objDate(1))
                    lngDays = Math.Abs(lngDays)
                Else
                    strOP = ""
                End If

                '�ҵ��ļ�
                strSQL = ""
                strSQL = strSQL + " select a.*" + vbCr
                strSQL = strSQL + " from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select" + vbCr
                strSQL = strSQL + "     a.�ļ���ʶ, a.��ˮ��  ," + vbCr
                strSQL = strSQL + "     a.��������, a.����״̬, a.�ļ�����, a.�ļ�����," + vbCr
                strSQL = strSQL + "     a.�ļ�����, a.���͵�λ, a.�ļ��ֺ�, a.�����̶�, a.���ܵȼ�," + vbCr
                strSQL = strSQL + "     a.���ش���, a.�ļ����, a.�ļ����," + vbCr
                strSQL = strSQL + "     a.�����  , a.���쵥λ, a.�����  , a.�������,a.��������," + vbCr
                strSQL = strSQL + "     �������� = max(a.��������)," + vbCr
                strSQL = strSQL + "     �������� = NULL," + vbCr
                strSQL = strSQL + "     ������� = NULL," + vbCr
                strSQL = strSQL + "     a.��������" + vbCr
                strSQL = strSQL + "   from" + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select" + vbCr
                strSQL = strSQL + "       a.�ļ���ʶ, b.��ˮ��  ," + vbCr
                strSQL = strSQL + "       b.��������, b.����״̬, b.�ļ�����, b.�ļ�����," + vbCr
                strSQL = strSQL + "       b.�ļ�����, b.���͵�λ, b.�ļ��ֺ�, b.�����̶�, b.���ܵȼ�," + vbCr
                strSQL = strSQL + "       b.���ش���, b.�ļ����, b.�ļ����," + vbCr
                strSQL = strSQL + "       b.�����  , b.���쵥λ, b.�����  , b.�������," + vbCr
                strSQL = strSQL + "       a.��������, a.��������, a.�������, b.��������," + vbCr
                strSQL = strSQL + "       �������� = case when c.�������� is null then '��' else c.�������� end" + vbCr
                strSQL = strSQL + "     from" + vbCr

                '��ȡ�����¼
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select" + vbCr
                strSQL = strSQL + "         �ļ���ʶ," + vbCr
                strSQL = strSQL + "         �������� = min(��������)," + vbCr
                strSQL = strSQL + "         �������� = NULL," + vbCr
                strSQL = strSQL + "         ������� = NULL" + vbCr
                strSQL = strSQL + "       from ����_B_����" + vbCr
                strSQL = strSQL + "       where ������ = '" + Trim(strUserXM) + "'" + vbCr                   '�Ҷ���
                strSQL = strSQL + "       and   �������� is not null" + vbCr
                If strOP <> "" Then                                                                          'ָ������
                    strSQL = strSQL + "       and abs(datediff(d, ��������, '" + Now.ToString("yyyy-MM-dd") + "')) " + strOP + " " + lngDays.ToString() + vbCr
                End If
                strSQL = strSQL + "       group by �ļ���ʶ" + vbCr
                strSQL = strSQL + "     ) a " + vbCr
                '��ȡ�����¼

                '��ȡ��������
                strSQL = strSQL + "     left join" + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select �ļ���ʶ,�������� = case when max(��������) = 1 then '��' else '��' end" + vbCr
                strSQL = strSQL + "       from ����_B_����" + vbCr
                strSQL = strSQL + "       where ������ = '" + strUserXM + "'" + vbCr
                strSQL = strSQL + "       group by �ļ���ʶ" + vbCr
                strSQL = strSQL + "     ) c on a.�ļ���ʶ = c.�ļ���ʶ" + vbCr
                '��ȡ��������


                '��ȡ�ļ���Ϣ
                strSQL = strSQL + "     left join " + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select " + vbCr
                strSQL = strSQL + "         a.�ļ���ʶ, a.��ˮ��  , " + vbCr
                strSQL = strSQL + "         a.��������, a.����״̬, a.�ļ�����, �������� = '" + strTASK_DBWJ + "', a.�ļ�����," + vbCr
                strSQL = strSQL + "         a.�ļ�����, a.���͵�λ, " + vbCr
                strSQL = strSQL + "         a.�ļ��ֺ�, a.�����̶�, a.���ܵȼ�," + vbCr
                strSQL = strSQL + "         a.���ش���, a.�ļ����, a.�ļ����," + vbCr
                strSQL = strSQL + "         a.�����  , a.���쵥λ, a.�����  , a.�������," + vbCr
                strSQL = strSQL + "         a.��������" + vbCr
                strSQL = strSQL + "       from ����_V_ȫ�������ļ��� a" + vbCr
                strSQL = strSQL + "       left join" + vbCr
                strSQL = strSQL + "       (" + vbCr
                strSQL = strSQL + "         select �ļ���ʶ" + vbCr
                strSQL = strSQL + "         from ����_B_����" + vbCr
                strSQL = strSQL + "         where ������ = '" + Trim(strUserXM) + "'" + vbCr                   '�Ҷ���
                strSQL = strSQL + "         and   �������� is not null" + vbCr
                If strOP <> "" Then                                                                            'ָ������
                    strSQL = strSQL + "         and abs(datediff(d, ��������, '" + Now.ToString("yyyy-MM-dd") + "')) " + strOP + " " + lngDays.ToString() + vbCr
                End If
                strSQL = strSQL + "         group by �ļ���ʶ" + vbCr
                strSQL = strSQL + "       ) b on a.�ļ���ʶ = b.�ļ���ʶ" + vbCr
                strSQL = strSQL + "       where b.�ļ���ʶ is not null" + vbCr
                If strWJLX <> "" Then
                    strSQL = strSQL + "       and a.�ļ����� = '" + strWJLX + "'" + vbCr
                End If
                strSQL = strSQL + "     ) b on a.�ļ���ʶ = b.�ļ���ʶ " + vbCr
                '��ȡ�ļ���Ϣ


                strSQL = strSQL + "     where b.�ļ���ʶ Is Not Null " + vbCr
                strSQL = strSQL + "   ) a " + vbCr
                strSQL = strSQL + "   group by " + vbCr
                strSQL = strSQL + "     a.�ļ���ʶ, a.��ˮ��  ," + vbCr
                strSQL = strSQL + "     a.��������, a.����״̬, a.�ļ�����, a.�ļ�����," + vbCr
                strSQL = strSQL + "     a.�ļ�����, a.���͵�λ, a.�ļ��ֺ�, a.�����̶�, a.���ܵȼ�," + vbCr
                strSQL = strSQL + "     a.���ش���, a.�ļ����, a.�ļ����," + vbCr
                strSQL = strSQL + "     a.�����  , a.���쵥λ, a.�����  , a.�������, a.��������, a.��������" + vbCr
                strSQL = strSQL + " ) a" + vbCr
                If strWhere <> "" Then
                    strSQL = strSQL + " where " + strWhere + vbCr
                End If
                strSQL = strSQL + " order by a.�������� desc, a.�ļ���� desc, a.���ش���, a.�ļ���� desc" + vbCr
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getSQLDBWJ_FILE = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �����ļ����͡���ʼ���ڡ��������ڼ��㶽���ļ�����������SQL
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strWJBS                ��Ҫ�鿴���ļ���ʶ
        '     strUserXM              ����ǰ������Ա����
        '     strBLLX                ����������
        '     strWJLX                ���ļ�����-����������
        '     strQSRQ                ����ʼ����
        '     strZZRQ                ����������
        '     strWhere               ����������
        '     strSQL                 ��(����)��������SQL
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Private Function getSQLDBWJ_TASK( _
            ByRef strErrMsg As String, _
            ByVal strWJBS As String, _
            ByVal strUserXM As String, _
            ByVal strBLLX As String, _
            ByVal strWJLX As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String, _
            ByVal strWhere As String, _
            ByRef strSQL As String) As Boolean

            getSQLDBWJ_TASK = False
            strSQL = ""

            Try
                Dim strTASKSTATUS_ZJB As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASKSTATUS_ZJB
                Dim strTASK_DBWJ As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_DBWJ
                Dim strLF As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhLf
                Dim strRT As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhRt

                '������ڼ��
                Dim lngDays As Long
                Dim strOP As String
                If strQSRQ <> "" And strZZRQ <> "" Then
                    Dim objDate(2) As System.DateTime
                    objDate(0) = CType(strQSRQ, System.DateTime)
                    objDate(1) = CType(strZZRQ, System.DateTime)
                    If objDate(0) <= objDate(1) Then
                        strOP = "<="
                    Else
                        strOP = ">"
                    End If
                    lngDays = DateDiff(DateInterval.Day, objDate(0), objDate(1))
                    lngDays = Math.Abs(lngDays)
                Else
                    strOP = ""
                End If

                '�ҵ��ļ�
                strSQL = ""
                strSQL = strSQL + " select * from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select" + vbCr
                strSQL = strSQL + "     a.�ļ���ʶ, b.��ˮ��  ," + vbCr
                strSQL = strSQL + "     b.��������, a.����״̬, a.��������, b.�ļ�����, b.�ļ�����," + vbCr
                strSQL = strSQL + "     b.�ļ�����, b.���ش���, b.�ļ����, b.�ļ����, b.���쵥λ," + vbCr
                strSQL = strSQL + "     a.������  , a.������  , a.ί����  , a.����˵��" + vbCr
                strSQL = strSQL + "   from" + vbCr

                '��ȡ�����¼
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select" + vbCr
                strSQL = strSQL + "      �ļ���ʶ, ����״̬ = '" + strTASKSTATUS_ZJB + "', �������� = '" + strTASK_DBWJ + "',"
                strSQL = strSQL + "      ������ = ������, ������ = ��������, ί���� = ' ', ����˵�� = ����Ҫ��"
                strSQL = strSQL + "     from ����_B_����" + vbCr
                strSQL = strSQL + "     where �ļ���ʶ = '" + strWJBS + "'" + vbCr                         'ָ���ļ�
                strSQL = strSQL + "     and   ������   = '" + Trim(strUserXM) + "'" + vbCr                 '�Ҷ���
                strSQL = strSQL + "     and   �������� is not null" + vbCr
                If strOP <> "" Then                                                                        'ָ������
                    strSQL = strSQL + "       and abs(datediff(d, ��������, '" + Now.ToString("yyyy-MM-dd") + "')) " + strOP + " " + lngDays.ToString() + vbCr
                End If
                strSQL = strSQL + "   ) a " + vbCr
                '��ȡ�����¼


                '��ȡ�ļ���Ϣ
                strSQL = strSQL + "   left join " + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select " + vbCr
                strSQL = strSQL + "       �ļ���ʶ, ��ˮ��  , " + vbCr
                strSQL = strSQL + "       ��������, ����״̬, �ļ�����, �ļ�����," + vbCr
                strSQL = strSQL + "       �ļ�����, ���͵�λ, " + vbCr
                strSQL = strSQL + "       �ļ��ֺ�, �����̶�, ���ܵȼ�," + vbCr
                strSQL = strSQL + "       ���ش���, �ļ����, �ļ����," + vbCr
                strSQL = strSQL + "       �����  , ���쵥λ, �����  , �������," + vbCr
                strSQL = strSQL + "       ��������" + vbCr
                strSQL = strSQL + "     from ����_V_ȫ�������ļ��� " + vbCr
                strSQL = strSQL + "     where �ļ���ʶ = '" + strWJBS + "'" + vbCr                                            'ָ���ļ�
                If strWJLX <> "" Then
                    strSQL = strSQL + "     and   �ļ����� = '" + strWJLX + "'" + vbCr                                        '����������=�ļ���������
                End If
                strSQL = strSQL + "   ) b on a.�ļ���ʶ = b.�ļ���ʶ " + vbCr
                '��ȡ�ļ���Ϣ


                strSQL = strSQL + "   where b.�ļ���ʶ Is Not Null " + vbCr
                strSQL = strSQL + " ) a " + vbCr
                If strWhere <> "" Then
                    strSQL = strSQL + " where " + strWhere + vbCr
                End If
                strSQL = strSQL + " group by " + vbCr
                strSQL = strSQL + "   a.�ļ���ʶ, a.��ˮ��  ," + vbCr
                strSQL = strSQL + "   a.��������, a.����״̬, a.��������, a.�ļ�����, a.�ļ�����," + vbCr
                strSQL = strSQL + "   a.�ļ�����, a.���ش���, a.�ļ����, a.�ļ����, a.���쵥λ," + vbCr
                strSQL = strSQL + "   a.������  , a.������  , a.ί����  , a.����˵��" + vbCr
                strSQL = strSQL + " order by a.�ļ���� desc, a.���ش���, a.�ļ���� desc" + vbCr
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getSQLDBWJ_TASK = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �����ļ����͡���ʼ���ڡ��������ڼ��㱻�����ļ����ļ�����SQL
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strUserXM              ����ǰ������Ա����
        '     strBLLX                ����������
        '     strWJLX                ���ļ�����-����������
        '     strQSRQ                ����ʼ����
        '     strZZRQ                ����������
        '     strWhere               ����������
        '     strSQL                 ��(����)�ļ�����SQL
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Private Function getSQLBDWJ_FILE( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByVal strBLLX As String, _
            ByVal strWJLX As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String, _
            ByVal strWhere As String, _
            ByRef strSQL As String) As Boolean

            getSQLBDWJ_FILE = False
            strSQL = ""

            Try
                Dim strTASK_DBWJ As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_DBWJ
                Dim strLF As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhLf
                Dim strRT As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhRt

                '������ڼ��
                Dim lngDays As Long
                Dim strOP As String
                If strQSRQ <> "" And strZZRQ <> "" Then
                    Dim objDate(2) As System.DateTime
                    objDate(0) = CType(strQSRQ, System.DateTime)
                    objDate(1) = CType(strZZRQ, System.DateTime)
                    If objDate(0) <= objDate(1) Then
                        strOP = "<="
                    Else
                        strOP = ">"
                    End If
                    lngDays = DateDiff(DateInterval.Day, objDate(0), objDate(1))
                    lngDays = Math.Abs(lngDays)
                Else
                    strOP = ""
                End If

                '�ҵ��ļ�
                strSQL = ""
                strSQL = strSQL + " select a.*" + vbCr
                strSQL = strSQL + " from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select" + vbCr
                strSQL = strSQL + "     a.�ļ���ʶ, a.��ˮ��  ," + vbCr
                strSQL = strSQL + "     a.��������, a.����״̬, a.�ļ�����, a.�ļ�����," + vbCr
                strSQL = strSQL + "     a.�ļ�����, a.���͵�λ, a.�ļ��ֺ�, a.�����̶�, a.���ܵȼ�," + vbCr
                strSQL = strSQL + "     a.���ش���, a.�ļ����, a.�ļ����," + vbCr
                strSQL = strSQL + "     a.�����  , a.���쵥λ, a.�����  , a.�������,a.��������," + vbCr
                strSQL = strSQL + "     �������� = max(a.��������)," + vbCr
                strSQL = strSQL + "     �������� = NULL," + vbCr
                strSQL = strSQL + "     ������� = NULL," + vbCr
                strSQL = strSQL + "     a.��������" + vbCr
                strSQL = strSQL + "   from" + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select" + vbCr
                strSQL = strSQL + "       a.�ļ���ʶ, b.��ˮ��  ," + vbCr
                strSQL = strSQL + "       b.��������, b.����״̬, b.�ļ�����, b.�ļ�����," + vbCr
                strSQL = strSQL + "       b.�ļ�����, b.���͵�λ, b.�ļ��ֺ�, b.�����̶�, b.���ܵȼ�," + vbCr
                strSQL = strSQL + "       b.���ش���, b.�ļ����, b.�ļ����," + vbCr
                strSQL = strSQL + "       b.�����  , b.���쵥λ, b.�����  , b.�������," + vbCr
                strSQL = strSQL + "       a.��������, a.��������, a.�������, b.��������," + vbCr
                strSQL = strSQL + "       �������� = case when c.�������� is null then '��' else c.�������� end" + vbCr
                strSQL = strSQL + "     from" + vbCr

                '��ȡ�����¼
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select" + vbCr
                strSQL = strSQL + "         �ļ���ʶ," + vbCr
                strSQL = strSQL + "         �������� = min(��������)," + vbCr
                strSQL = strSQL + "         �������� = NULL," + vbCr
                strSQL = strSQL + "         ������� = NULL" + vbCr
                strSQL = strSQL + "       from ����_B_����" + vbCr
                strSQL = strSQL + "       where �������� = '" + Trim(strUserXM) + "'" + vbCr                 '�ұ�����
                strSQL = strSQL + "       and   �������� is not null" + vbCr
                If strOP <> "" Then                                                                          'ָ������
                    strSQL = strSQL + "       and abs(datediff(d, ��������, '" + Now.ToString("yyyy-MM-dd") + "')) " + strOP + " " + lngDays.ToString() + vbCr
                End If
                strSQL = strSQL + "       group by �ļ���ʶ" + vbCr
                strSQL = strSQL + "     ) a " + vbCr
                '��ȡ�����¼

                '��ȡ��������
                strSQL = strSQL + "     left join" + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select �ļ���ʶ,�������� = case when max(��������) = 1 then '��' else '��' end" + vbCr
                strSQL = strSQL + "       from ����_B_����" + vbCr
                strSQL = strSQL + "       where ������ = '" + strUserXM + "'" + vbCr
                strSQL = strSQL + "       group by �ļ���ʶ" + vbCr
                strSQL = strSQL + "     ) c on a.�ļ���ʶ = c.�ļ���ʶ" + vbCr
                '��ȡ��������


                '��ȡ�ļ���Ϣ
                strSQL = strSQL + "     left join " + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select " + vbCr
                strSQL = strSQL + "         a.�ļ���ʶ, a.��ˮ��  , " + vbCr
                strSQL = strSQL + "         a.��������, a.����״̬, a.�ļ�����, �������� = '" + strTASK_DBWJ + "', a.�ļ�����," + vbCr
                strSQL = strSQL + "         a.�ļ�����, a.���͵�λ, " + vbCr
                strSQL = strSQL + "         a.�ļ��ֺ�, a.�����̶�, a.���ܵȼ�," + vbCr
                strSQL = strSQL + "         a.���ش���, a.�ļ����, a.�ļ����," + vbCr
                strSQL = strSQL + "         a.�����  , a.���쵥λ, a.�����  , a.�������," + vbCr
                strSQL = strSQL + "         a.��������" + vbCr
                strSQL = strSQL + "       from ����_V_ȫ�������ļ��� a" + vbCr
                strSQL = strSQL + "       left join" + vbCr
                strSQL = strSQL + "       (" + vbCr
                strSQL = strSQL + "         select �ļ���ʶ" + vbCr
                strSQL = strSQL + "         from ����_B_����" + vbCr
                strSQL = strSQL + "         where �������� = '" + Trim(strUserXM) + "'" + vbCr                 '�ұ�����
                strSQL = strSQL + "         and   �������� is not null" + vbCr
                If strOP <> "" Then                                                                            'ָ������
                    strSQL = strSQL + "         and abs(datediff(d, ��������, '" + Now.ToString("yyyy-MM-dd") + "')) " + strOP + " " + lngDays.ToString() + vbCr
                End If
                strSQL = strSQL + "         group by �ļ���ʶ" + vbCr
                strSQL = strSQL + "       ) b on a.�ļ���ʶ = b.�ļ���ʶ" + vbCr
                strSQL = strSQL + "       where b.�ļ���ʶ is not null" + vbCr
                If strWJLX <> "" Then
                    strSQL = strSQL + "       and a.�ļ����� = '" + strWJLX + "'" + vbCr
                End If
                strSQL = strSQL + "     ) b on a.�ļ���ʶ = b.�ļ���ʶ " + vbCr
                '��ȡ�ļ���Ϣ


                strSQL = strSQL + "     where b.�ļ���ʶ Is Not Null " + vbCr
                strSQL = strSQL + "   ) a " + vbCr
                strSQL = strSQL + "   group by " + vbCr
                strSQL = strSQL + "     a.�ļ���ʶ, a.��ˮ��  ," + vbCr
                strSQL = strSQL + "     a.��������, a.����״̬, a.�ļ�����, a.�ļ�����," + vbCr
                strSQL = strSQL + "     a.�ļ�����, a.���͵�λ, a.�ļ��ֺ�, a.�����̶�, a.���ܵȼ�," + vbCr
                strSQL = strSQL + "     a.���ش���, a.�ļ����, a.�ļ����," + vbCr
                strSQL = strSQL + "     a.�����  , a.���쵥λ, a.�����  , a.�������, a.��������, a.��������" + vbCr
                strSQL = strSQL + " ) a" + vbCr
                If strWhere <> "" Then
                    strSQL = strSQL + " where " + strWhere + vbCr
                End If
                strSQL = strSQL + " order by a.�������� desc, a.�ļ���� desc, a.���ش���, a.�ļ���� desc" + vbCr
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getSQLBDWJ_FILE = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �����ļ����͡���ʼ���ڡ��������ڼ��㱻�����ļ�����������SQL
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strWJBS                ��Ҫ�鿴���ļ���ʶ
        '     strUserXM              ����ǰ������Ա����
        '     strBLLX                ����������
        '     strWJLX                ���ļ�����-����������
        '     strQSRQ                ����ʼ����
        '     strZZRQ                ����������
        '     strWhere               ����������
        '     strSQL                 ��(����)��������SQL
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Private Function getSQLBDWJ_TASK( _
            ByRef strErrMsg As String, _
            ByVal strWJBS As String, _
            ByVal strUserXM As String, _
            ByVal strBLLX As String, _
            ByVal strWJLX As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String, _
            ByVal strWhere As String, _
            ByRef strSQL As String) As Boolean

            getSQLBDWJ_TASK = False
            strSQL = ""

            Try
                Dim strTASKSTATUS_ZJB As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASKSTATUS_ZJB
                Dim strTASK_DBWJ As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_DBWJ
                Dim strLF As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhLf
                Dim strRT As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhRt

                '������ڼ��
                Dim lngDays As Long
                Dim strOP As String
                If strQSRQ <> "" And strZZRQ <> "" Then
                    Dim objDate(2) As System.DateTime
                    objDate(0) = CType(strQSRQ, System.DateTime)
                    objDate(1) = CType(strZZRQ, System.DateTime)
                    If objDate(0) <= objDate(1) Then
                        strOP = "<="
                    Else
                        strOP = ">"
                    End If
                    lngDays = DateDiff(DateInterval.Day, objDate(0), objDate(1))
                    lngDays = Math.Abs(lngDays)
                Else
                    strOP = ""
                End If

                '�ҵ��ļ�
                strSQL = ""
                strSQL = strSQL + " select * from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select" + vbCr
                strSQL = strSQL + "     a.�ļ���ʶ, b.��ˮ��  ," + vbCr
                strSQL = strSQL + "     b.��������, a.����״̬, a.��������, b.�ļ�����, b.�ļ�����," + vbCr
                strSQL = strSQL + "     b.�ļ�����, b.���ش���, b.�ļ����, b.�ļ����, b.���쵥λ," + vbCr
                strSQL = strSQL + "     a.������  , a.������  , a.ί����  , a.����˵��" + vbCr
                strSQL = strSQL + "   from" + vbCr

                '��ȡ�����¼
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select" + vbCr
                strSQL = strSQL + "      �ļ���ʶ, ����״̬ = '" + strTASKSTATUS_ZJB + "', �������� = '" + strTASK_DBWJ + "',"
                strSQL = strSQL + "      ������ = ������, ������ = ��������, ί���� = ' ', ����˵�� = ����Ҫ��"
                strSQL = strSQL + "     from ����_B_����" + vbCr
                strSQL = strSQL + "     where �ļ���ʶ = '" + strWJBS + "'" + vbCr                         'ָ���ļ�
                strSQL = strSQL + "     and   �������� = '" + Trim(strUserXM) + "'" + vbCr                 '�ұ�����
                strSQL = strSQL + "     and   �������� is not null" + vbCr
                If strOP <> "" Then                                                                        'ָ������
                    strSQL = strSQL + "       and abs(datediff(d, ��������, '" + Now.ToString("yyyy-MM-dd") + "')) " + strOP + " " + lngDays.ToString() + vbCr
                End If
                strSQL = strSQL + "   ) a " + vbCr
                '��ȡ�����¼


                '��ȡ�ļ���Ϣ
                strSQL = strSQL + "   left join " + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select " + vbCr
                strSQL = strSQL + "       �ļ���ʶ, ��ˮ��  , " + vbCr
                strSQL = strSQL + "       ��������, ����״̬, �ļ�����, �ļ�����," + vbCr
                strSQL = strSQL + "       �ļ�����, ���͵�λ, " + vbCr
                strSQL = strSQL + "       �ļ��ֺ�, �����̶�, ���ܵȼ�," + vbCr
                strSQL = strSQL + "       ���ش���, �ļ����, �ļ����," + vbCr
                strSQL = strSQL + "       �����  , ���쵥λ, �����  , �������," + vbCr
                strSQL = strSQL + "       ��������" + vbCr
                strSQL = strSQL + "     from ����_V_ȫ�������ļ��� " + vbCr
                strSQL = strSQL + "     where �ļ���ʶ = '" + strWJBS + "'" + vbCr                                            'ָ���ļ�
                If strWJLX <> "" Then
                    strSQL = strSQL + "     and   �ļ����� = '" + strWJLX + "'" + vbCr                                        '����������=�ļ���������
                End If
                strSQL = strSQL + "   ) b on a.�ļ���ʶ = b.�ļ���ʶ " + vbCr
                '��ȡ�ļ���Ϣ


                strSQL = strSQL + "   where b.�ļ���ʶ Is Not Null " + vbCr
                strSQL = strSQL + " ) a " + vbCr
                If strWhere <> "" Then
                    strSQL = strSQL + " where " + strWhere + vbCr
                End If
                strSQL = strSQL + " group by " + vbCr
                strSQL = strSQL + "   a.�ļ���ʶ, a.��ˮ��  ," + vbCr
                strSQL = strSQL + "   a.��������, a.����״̬, a.��������, a.�ļ�����, a.�ļ�����," + vbCr
                strSQL = strSQL + "   a.�ļ�����, a.���ش���, a.�ļ����, a.�ļ����, a.���쵥λ," + vbCr
                strSQL = strSQL + "   a.������  , a.������  , a.ί����  , a.����˵��" + vbCr
                strSQL = strSQL + " order by a.�ļ���� desc, a.���ش���, a.�ļ���� desc" + vbCr
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getSQLBDWJ_TASK = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �����ļ����͡���ʼ���ڡ��������ڼ���ȫ�����˵��ļ�����SQL
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strUserXM              ����ǰ������Ա����
        '     strBLLX                ����������
        '     strWJLX                ���ļ�����-����������
        '     strQSRQ                ����ʼ����
        '     strZZRQ                ����������
        '     strWhere               ����������
        '     strSQL                 ��(����)�ļ�����SQL
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Private Function getSQLQBSY_FILE( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByVal strBLLX As String, _
            ByVal strWJLX As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String, _
            ByVal strWhere As String, _
            ByRef strSQL As String) As Boolean

            getSQLQBSY_FILE = False
            strSQL = ""

            Try
                Dim strFileAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.FileStatusAllYWCList
                Dim strTaskAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TaskStatusAllYWCList
                Dim strLF As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhLf
                Dim strRT As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhRt

                '��ʼ������
                If strQSRQ Is Nothing Then strQSRQ = ""
                strQSRQ = strQSRQ.Trim
                If strQSRQ <> "" Then
                    If strQSRQ.IndexOf(" ") < 0 Then
                        strQSRQ = strQSRQ + " 00:00:00"
                    End If
                End If
                If strZZRQ Is Nothing Then strZZRQ = ""
                strZZRQ = strZZRQ.Trim
                If strZZRQ <> "" Then
                    If strZZRQ.IndexOf(" ") < 0 Then
                        strZZRQ = strZZRQ + " 23:59:59"
                    End If
                End If

                '�ҵ��ļ�
                strSQL = ""
                strSQL = strSQL + " select a.*" + vbCr
                strSQL = strSQL + " from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select" + vbCr
                strSQL = strSQL + "     a.�ļ���ʶ, a.��ˮ��  ," + vbCr
                strSQL = strSQL + "     a.��������, a.����״̬, a.�ļ�����, a.�ļ�����," + vbCr
                strSQL = strSQL + "     a.�ļ�����, a.���͵�λ, a.�ļ��ֺ�, a.�����̶�, a.���ܵȼ�," + vbCr
                strSQL = strSQL + "     a.���ش���, a.�ļ����, a.�ļ����," + vbCr
                strSQL = strSQL + "     a.�����  , a.���쵥λ, a.�����  , a.�������,a.��������," + vbCr
                strSQL = strSQL + "     �������� = max(a.��������)," + vbCr
                strSQL = strSQL + "     �������� = max(a.��������)," + vbCr
                strSQL = strSQL + "     ������� = max(a.�������)," + vbCr
                strSQL = strSQL + "     a.��������" + vbCr
                strSQL = strSQL + "   from" + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select" + vbCr
                strSQL = strSQL + "       a.�ļ���ʶ, b.��ˮ��  ," + vbCr
                strSQL = strSQL + "       a.��������, b.����״̬, b.�ļ�����, b.�ļ�����," + vbCr
                strSQL = strSQL + "       b.�ļ�����, b.���͵�λ, b.�ļ��ֺ�, b.�����̶�, b.���ܵȼ�," + vbCr
                strSQL = strSQL + "       b.���ش���, b.�ļ����, b.�ļ����," + vbCr
                strSQL = strSQL + "       b.�����  , b.���쵥λ, b.�����  , b.�������," + vbCr
                strSQL = strSQL + "       a.��������, a.��������, a.�������, b.��������," + vbCr
                strSQL = strSQL + "       �������� = case when c.�������� is null then '��' else c.�������� end" + vbCr
                strSQL = strSQL + "     from" + vbCr

                '��ȡ�����¼
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select" + vbCr
                strSQL = strSQL + "         �ļ���ʶ, ��������," + vbCr
                strSQL = strSQL + "         �������� = max(��������)," + vbCr
                strSQL = strSQL + "         �������� = max(�����������)," + vbCr
                strSQL = strSQL + "         ������� = max(�������)" + vbCr
                strSQL = strSQL + "       from ����_B_����" + vbCr
                strSQL = strSQL + "       where ((������ = '" + Trim(strUserXM) + "' and ���ӱ�ʶ like '__1%')" + vbCr              '���յ���
                strSQL = strSQL + "       or     (������ = '" + Trim(strUserXM) + "' and ���ӱ�ʶ like '_1%'))" + vbCr              '���ͳ���
                If strBLLX <> "" Then
                    strSQL = strSQL + "       and �������� = '" + strBLLX + "'" + vbCr                                              'ָ������
                End If
                If strQSRQ <> "" And strZZRQ <> "" Then                                                                             'ָ������
                    strSQL = strSQL + "       and �������� between '" + strQSRQ + "' and '" + strZZRQ + "' " + vbCr
                ElseIf strQSRQ <> "" Then
                    strSQL = strSQL + "       and �������� >= '" + strQSRQ + "'" + vbCr
                ElseIf strZZRQ <> "" Then
                    strSQL = strSQL + "       and �������� <= '" + strZZRQ + "'" + vbCr
                Else
                End If
                strSQL = strSQL + "       group by �ļ���ʶ, ��������" + vbCr
                strSQL = strSQL + "     ) a " + vbCr
                '��ȡ�����¼

                '��ȡ��������
                strSQL = strSQL + "     left join" + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select �ļ���ʶ,�������� = case when max(��������) = 1 then '��' else '��' end" + vbCr
                strSQL = strSQL + "       from ����_B_����" + vbCr
                strSQL = strSQL + "       where ������ = '" + strUserXM + "'" + vbCr
                strSQL = strSQL + "       group by �ļ���ʶ" + vbCr
                strSQL = strSQL + "     ) c on a.�ļ���ʶ = c.�ļ���ʶ" + vbCr
                '��ȡ��������


                '��ȡ�ļ���Ϣ
                strSQL = strSQL + "     left join " + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select " + vbCr
                strSQL = strSQL + "         a.�ļ���ʶ, a.��ˮ��  , " + vbCr
                strSQL = strSQL + "         a.��������, a.����״̬, a.�ļ�����, a.�ļ�����," + vbCr
                strSQL = strSQL + "         a.�ļ�����, a.���͵�λ, " + vbCr
                strSQL = strSQL + "         a.�ļ��ֺ�, a.�����̶�, a.���ܵȼ�," + vbCr
                strSQL = strSQL + "         a.���ش���, a.�ļ����, a.�ļ����," + vbCr
                strSQL = strSQL + "         a.�����  , a.���쵥λ, a.�����  , a.�������," + vbCr
                strSQL = strSQL + "         a.��������" + vbCr
                strSQL = strSQL + "       from ����_V_ȫ�������ļ��� a" + vbCr
                strSQL = strSQL + "       left join" + vbCr
                strSQL = strSQL + "       (" + vbCr
                strSQL = strSQL + "         select �ļ���ʶ" + vbCr
                strSQL = strSQL + "         from ����_B_����" + vbCr
                strSQL = strSQL + "         where ((������ = '" + Trim(strUserXM) + "' and ���ӱ�ʶ like '__1%')" + vbCr              '���յ���
                strSQL = strSQL + "         or     (������ = '" + Trim(strUserXM) + "' and ���ӱ�ʶ like '_1%'))" + vbCr              '���ͳ���
                If strBLLX <> "" Then
                    strSQL = strSQL + "         and �������� = '" + strBLLX + "'" + vbCr                                              'ָ������
                End If
                If strQSRQ <> "" And strZZRQ <> "" Then                                                                               'ָ������
                    strSQL = strSQL + "         and �������� between '" + strQSRQ + "' and '" + strZZRQ + "' " + vbCr
                ElseIf strQSRQ <> "" Then
                    strSQL = strSQL + "         and �������� >= '" + strQSRQ + "'" + vbCr
                ElseIf strZZRQ <> "" Then
                    strSQL = strSQL + "         and �������� <= '" + strZZRQ + "'" + vbCr
                Else
                End If
                strSQL = strSQL + "         group by �ļ���ʶ" + vbCr
                strSQL = strSQL + "       ) b on a.�ļ���ʶ = b.�ļ���ʶ" + vbCr
                strSQL = strSQL + "       where b.�ļ���ʶ is not null" + vbCr
                If strWJLX <> "" Then
                    strSQL = strSQL + "       and a.�ļ����� = '" + strWJLX + "'" + vbCr
                End If
                strSQL = strSQL + "     ) b on a.�ļ���ʶ = b.�ļ���ʶ " + vbCr
                '��ȡ�ļ���Ϣ


                strSQL = strSQL + "     where b.�ļ���ʶ Is Not Null " + vbCr
                strSQL = strSQL + "   ) a " + vbCr
                strSQL = strSQL + "   group by " + vbCr
                strSQL = strSQL + "     a.�ļ���ʶ, a.��ˮ��  ," + vbCr
                strSQL = strSQL + "     a.��������, a.����״̬, a.�ļ�����, a.�ļ�����," + vbCr
                strSQL = strSQL + "     a.�ļ�����, a.���͵�λ, a.�ļ��ֺ�, a.�����̶�, a.���ܵȼ�," + vbCr
                strSQL = strSQL + "     a.���ش���, a.�ļ����, a.�ļ����," + vbCr
                strSQL = strSQL + "     a.�����  , a.���쵥λ, a.�����  , a.�������, a.��������, a.��������" + vbCr
                strSQL = strSQL + " ) a" + vbCr
                If strWhere <> "" Then
                    strSQL = strSQL + " where " + strWhere + vbCr
                End If
                strSQL = strSQL + " order by a.�������� desc, a.�ļ���� desc, a.���ش���, a.�ļ���� desc" + vbCr
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getSQLQBSY_FILE = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �����ļ����͡���ʼ���ڡ��������ڼ���ȫ�����˵���������SQL
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strWJBS                ��Ҫ�鿴���ļ���ʶ
        '     strUserXM              ����ǰ������Ա����
        '     strBLLX                ����������
        '     strWJLX                ���ļ�����-����������
        '     strQSRQ                ����ʼ����
        '     strZZRQ                ����������
        '     strWhere               ����������
        '     strSQL                 ��(����)��������SQL
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Private Function getSQLQBSY_TASK( _
            ByRef strErrMsg As String, _
            ByVal strWJBS As String, _
            ByVal strUserXM As String, _
            ByVal strBLLX As String, _
            ByVal strWJLX As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String, _
            ByVal strWhere As String, _
            ByRef strSQL As String) As Boolean

            getSQLQBSY_TASK = False
            strSQL = ""

            Try
                Dim strFileAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.FileStatusAllYWCList
                Dim strTaskAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TaskStatusAllYWCList
                Dim strLF As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhLf
                Dim strRT As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhRt
                Dim strGWTHCL As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_THCL
                Dim strGWSHCL As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_SHCL
                Dim strGWHFCL As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_HFCL

                '��ʼ������
                If strQSRQ Is Nothing Then strQSRQ = ""
                strQSRQ = strQSRQ.Trim
                If strQSRQ <> "" Then
                    If strQSRQ.IndexOf(" ") < 0 Then
                        strQSRQ = strQSRQ + " 00:00:00"
                    End If
                End If
                If strZZRQ Is Nothing Then strZZRQ = ""
                strZZRQ = strZZRQ.Trim
                If strZZRQ <> "" Then
                    If strZZRQ.IndexOf(" ") < 0 Then
                        strZZRQ = strZZRQ + " 23:59:59"
                    End If
                End If

                '�ҵ��ļ�
                strSQL = ""
                strSQL = strSQL + " select * from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select" + vbCr
                strSQL = strSQL + "     a.�ļ���ʶ, b.��ˮ��  ," + vbCr
                strSQL = strSQL + "     a.��������, a.����״̬, a.��������, b.�ļ�����, b.�ļ�����," + vbCr
                strSQL = strSQL + "     b.�ļ�����, b.���ش���, b.�ļ����, b.�ļ����, b.���쵥λ," + vbCr
                strSQL = strSQL + "     a.������  , a.������  , a.ί����  , a.����˵��" + vbCr
                strSQL = strSQL + "   from" + vbCr

                '��ȡ�����¼
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select" + vbCr
                strSQL = strSQL + "       �ļ���ʶ, ��������, ����״̬," + vbCr
                strSQL = strSQL + "       �������� = case " + vbCr
                strSQL = strSQL + "         when ���ӱ�ʶ like '___1%'    then '" + strGWTHCL + "' " + vbCr
                strSQL = strSQL + "         when ���ӱ�ʶ like '____1%'   then '" + strGWSHCL + "' " + vbCr
                strSQL = strSQL + "         when ���ӱ�ʶ like '______1%' then '" + strGWHFCL + "' " + vbCr
                strSQL = strSQL + "         else �������� end," + vbCr
                strSQL = strSQL + "       ������, ������, ί����, ���ӱ�ʶ, ����˵�� " + vbCr
                strSQL = strSQL + "     from ����_B_����" + vbCr
                strSQL = strSQL + "     where   �ļ���ʶ = '" + strWJBS + "'" + vbCr                                     'ָ���ļ�
                strSQL = strSQL + "     and   ((������   = '" + Trim(strUserXM) + "' and ���ӱ�ʶ like '__1%')" + vbCr   '���յ���
                strSQL = strSQL + "     or     (������   = '" + Trim(strUserXM) + "' and ���ӱ�ʶ like '_1%'))" + vbCr   '���ͳ���                              '�ҿɼ�
                If strBLLX <> "" Then
                    strSQL = strSQL + "     and �������� = '" + strBLLX + "'" + vbCr                                     'ָ������
                End If
                If strQSRQ <> "" And strZZRQ <> "" Then                                                                  'ָ������
                    strSQL = strSQL + "     and �������� between '" + strQSRQ + "' and '" + strZZRQ + "' " + vbCr
                ElseIf strQSRQ <> "" Then
                    strSQL = strSQL + "     and �������� >= '" + strQSRQ + "'" + vbCr
                ElseIf strZZRQ <> "" Then
                    strSQL = strSQL + "     and �������� <= '" + strZZRQ + "'" + vbCr
                Else
                End If
                strSQL = strSQL + "   ) a " + vbCr
                '��ȡ�����¼


                '��ȡ�ļ���Ϣ
                strSQL = strSQL + "   left join " + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select " + vbCr
                strSQL = strSQL + "       �ļ���ʶ, ��ˮ��  , " + vbCr
                strSQL = strSQL + "       ��������, ����״̬, �ļ�����, �ļ�����," + vbCr
                strSQL = strSQL + "       �ļ�����, ���͵�λ, " + vbCr
                strSQL = strSQL + "       �ļ��ֺ�, �����̶�, ���ܵȼ�," + vbCr
                strSQL = strSQL + "       ���ش���, �ļ����, �ļ����," + vbCr
                strSQL = strSQL + "       �����  , ���쵥λ, �����  , �������," + vbCr
                strSQL = strSQL + "       ��������" + vbCr
                strSQL = strSQL + "     from ����_V_ȫ�������ļ��� " + vbCr
                strSQL = strSQL + "     where �ļ���ʶ = '" + strWJBS + "'" + vbCr                                            'ָ���ļ�
                If strWJLX <> "" Then
                    strSQL = strSQL + "     and   �ļ����� = '" + strWJLX + "'" + vbCr                                        '����������=�ļ���������
                End If
                strSQL = strSQL + "   ) b on a.�ļ���ʶ = b.�ļ���ʶ " + vbCr
                '��ȡ�ļ���Ϣ


                strSQL = strSQL + "   where b.�ļ���ʶ Is Not Null " + vbCr
                strSQL = strSQL + " ) a " + vbCr
                If strWhere <> "" Then
                    strSQL = strSQL + " where " + strWhere + vbCr
                End If
                strSQL = strSQL + " group by " + vbCr
                strSQL = strSQL + "   a.�ļ���ʶ, a.��ˮ��  ," + vbCr
                strSQL = strSQL + "   a.��������, a.����״̬, a.��������, a.�ļ�����, a.�ļ�����," + vbCr
                strSQL = strSQL + "   a.�ļ�����, a.���ش���, a.�ļ����, a.�ļ����, a.���쵥λ," + vbCr
                strSQL = strSQL + "   a.������  , a.������  , a.ί����  , a.����˵��" + vbCr
                strSQL = strSQL + " order by a.�ļ���� desc, a.���ش���, a.�ļ���� desc" + vbCr
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getSQLQBSY_TASK = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �����ļ����͡���ʼ���ڡ��������ڼ�����Ҫ�������ѵ��ļ�����SQL
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strUserXM              ����ǰ������Ա����
        '     strBLLX                ����������
        '     strWJLX                ���ļ�����-����������
        '     strQSRQ                ����ʼ����
        '     strZZRQ                ����������
        '     strWhere               ����������
        '     strSQL                 ��(����)�ļ�����SQL
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Private Function getSQLBWTX_FILE( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByVal strBLLX As String, _
            ByVal strWJLX As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String, _
            ByVal strWhere As String, _
            ByRef strSQL As String) As Boolean

            getSQLBWTX_FILE = False
            strSQL = ""

            Try
                Dim strFileAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.FileStatusAllYWCList
                Dim strTaskAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TaskStatusAllYWCList
                Dim strLF As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhLf
                Dim strRT As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhRt

                '��ʼ������
                If strQSRQ Is Nothing Then strQSRQ = ""
                strQSRQ = strQSRQ.Trim
                If strQSRQ <> "" Then
                    If strQSRQ.IndexOf(" ") < 0 Then
                        strQSRQ = strQSRQ + " 00:00:00"
                    End If
                End If
                If strZZRQ Is Nothing Then strZZRQ = ""
                strZZRQ = strZZRQ.Trim
                If strZZRQ <> "" Then
                    If strZZRQ.IndexOf(" ") < 0 Then
                        strZZRQ = strZZRQ + " 23:59:59"
                    End If
                End If

                '�ҵ��ļ�
                strSQL = ""
                strSQL = strSQL + " select a.*" + vbCr
                strSQL = strSQL + " from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select" + vbCr
                strSQL = strSQL + "     a.�ļ���ʶ, a.��ˮ��," + vbCr
                strSQL = strSQL + "     a.��������, a.����״̬, a.�ļ�����, a.�ļ�����," + vbCr
                strSQL = strSQL + "     a.�ļ�����, a.���͵�λ, a.�ļ��ֺ�, a.�����̶�, a.���ܵȼ�," + vbCr
                strSQL = strSQL + "     a.���ش���, a.�ļ����, a.�ļ����," + vbCr
                strSQL = strSQL + "     a.�����  , a.���쵥λ, a.�����  , a.�������,a.��������," + vbCr
                strSQL = strSQL + "     �������� = max(a.��������)," + vbCr
                strSQL = strSQL + "     �������� = max(a.��������)," + vbCr
                strSQL = strSQL + "     ������� = max(a.�������)," + vbCr
                strSQL = strSQL + "     a.��������" + vbCr
                strSQL = strSQL + "   from" + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select" + vbCr
                strSQL = strSQL + "       a.�ļ���ʶ, b.��ˮ��  ," + vbCr
                strSQL = strSQL + "       a.��������, b.����״̬, b.�ļ�����, b.�ļ�����," + vbCr
                strSQL = strSQL + "       b.�ļ�����, b.���͵�λ, b.�ļ��ֺ�, b.�����̶�, b.���ܵȼ�," + vbCr
                strSQL = strSQL + "       b.���ش���, b.�ļ����, b.�ļ����," + vbCr
                strSQL = strSQL + "       b.�����  , b.���쵥λ, b.�����  , b.�������," + vbCr
                strSQL = strSQL + "       a.��������, a.��������, a.�������, b.��������," + vbCr
                strSQL = strSQL + "       �������� = case when c.�������� is null then '��' else c.�������� end" + vbCr
                strSQL = strSQL + "     from" + vbCr

                '��ȡ�����¼
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select a.�ļ���ʶ,a.��������,a.���ӱ�ʶ," + vbCr
                strSQL = strSQL + "         �������� = max(a.��������)," + vbCr
                strSQL = strSQL + "         �������� = max(a.�����������)," + vbCr
                strSQL = strSQL + "         ������� = max(a.�������)" + vbCr
                strSQL = strSQL + "       from" + vbCr
                strSQL = strSQL + "       (" + vbCr
                strSQL = strSQL + "         select" + vbCr
                strSQL = strSQL + "           �ļ���ʶ,��������," + vbCr
                strSQL = strSQL + "           ���ӱ�ʶ = case when ���ӱ�ʶ like '_____1%' then '1' else '0' end," + vbCr
                strSQL = strSQL + "           ��������," + vbCr
                strSQL = strSQL + "           �����������," + vbCr
                strSQL = strSQL + "           �������" + vbCr
                strSQL = strSQL + "         from ����_B_����" + vbCr
                strSQL = strSQL + "         where ������ = '" + Trim(strUserXM) + "'" + vbCr                   '��Ҫ��
                strSQL = strSQL + "         and   ���ӱ�ʶ like '__1%'" + vbCr                                 '�ҿɼ�
                strSQL = strSQL + "         and   isnull(��������,0) = 1" + vbCr                               '��Ҫ��������
                If strBLLX <> "" Then
                    strSQL = strSQL + "         and �������� = '" + strBLLX + "'" + vbCr                       'ָ������
                End If
                If strQSRQ <> "" And strZZRQ <> "" Then                                                        'ָ������
                    strSQL = strSQL + "         and �������� between '" + strQSRQ + "' and '" + strZZRQ + "' " + vbCr
                ElseIf strQSRQ <> "" Then
                    strSQL = strSQL + "         and �������� >= '" + strQSRQ + "'" + vbCr
                ElseIf strZZRQ <> "" Then
                    strSQL = strSQL + "         and �������� <= '" + strZZRQ + "'" + vbCr
                Else
                End If
                strSQL = strSQL + "       ) a" + vbCr
                strSQL = strSQL + "       group by a.�ļ���ʶ,a.��������,a.���ӱ�ʶ" + vbCr
                strSQL = strSQL + "     ) a " + vbCr
                '��ȡ�����¼

                '��ȡ��������
                strSQL = strSQL + "     left join" + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select �ļ���ʶ,�������� = case when max(��������) = 1 then '��' else '��' end" + vbCr
                strSQL = strSQL + "       from ����_B_����" + vbCr
                strSQL = strSQL + "       where ������ = '" + strUserXM + "'" + vbCr
                strSQL = strSQL + "       group by �ļ���ʶ" + vbCr
                strSQL = strSQL + "     ) c on a.�ļ���ʶ = c.�ļ���ʶ" + vbCr
                '��ȡ��������


                '��ȡ�ļ���Ϣ
                strSQL = strSQL + "     left join " + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select " + vbCr
                strSQL = strSQL + "         a.�ļ���ʶ, a.��ˮ��  , " + vbCr
                strSQL = strSQL + "         a.��������, a.����״̬, a.�ļ�����, a.�ļ�����," + vbCr
                strSQL = strSQL + "         a.�ļ�����, a.���͵�λ, " + vbCr
                strSQL = strSQL + "         a.�ļ��ֺ�, a.�����̶�, a.���ܵȼ�," + vbCr
                strSQL = strSQL + "         a.���ش���, a.�ļ����, a.�ļ����," + vbCr
                strSQL = strSQL + "         a.�����  , a.���쵥λ, a.�����  , a.�������," + vbCr
                strSQL = strSQL + "         a.��������" + vbCr
                strSQL = strSQL + "       from ����_V_ȫ�������ļ��� a" + vbCr
                strSQL = strSQL + "       left join" + vbCr
                strSQL = strSQL + "       (" + vbCr
                strSQL = strSQL + "         select �ļ���ʶ" + vbCr
                strSQL = strSQL + "         from ����_B_����" + vbCr
                strSQL = strSQL + "         where ������ = '" + Trim(strUserXM) + "'" + vbCr                   '��Ҫ��
                strSQL = strSQL + "         and   ���ӱ�ʶ like '__1%'" + vbCr                                 '�ҿɼ�
                strSQL = strSQL + "         and   isnull(��������,0) = 1" + vbCr                               '��Ҫ��������
                If strBLLX <> "" Then
                    strSQL = strSQL + "         and �������� = '" + strBLLX + "'" + vbCr                       'ָ������
                End If
                If strQSRQ <> "" And strZZRQ <> "" Then                                                        'ָ������
                    strSQL = strSQL + "         and �������� between '" + strQSRQ + "' and '" + strZZRQ + "' " + vbCr
                ElseIf strQSRQ <> "" Then
                    strSQL = strSQL + "         and �������� >= '" + strQSRQ + "'" + vbCr
                ElseIf strZZRQ <> "" Then
                    strSQL = strSQL + "         and �������� <= '" + strZZRQ + "'" + vbCr
                Else
                End If
                strSQL = strSQL + "         group by �ļ���ʶ" + vbCr
                strSQL = strSQL + "       ) b on a.�ļ���ʶ = b.�ļ���ʶ" + vbCr
                strSQL = strSQL + "       where b.�ļ���ʶ is not null" + vbCr
                strSQL = strSQL + "       and a.����״̬ not in (" + strFileAllYWCList + ")" + vbCr                             '�ļ�δ����
                If strWJLX <> "" Then
                    strSQL = strSQL + "       and a.�ļ����� = '" + strWJLX + "'" + vbCr
                End If
                strSQL = strSQL + "     ) b on a.�ļ���ʶ = b.�ļ���ʶ " + vbCr
                '��ȡ�ļ���Ϣ


                strSQL = strSQL + "     where b.�ļ���ʶ Is Not Null " + vbCr
                strSQL = strSQL + "     and (" + vbCr
                strSQL = strSQL + "       (a.���ӱ�ʶ = '1')" + vbCr                                                            '֪ͨ����Ϣ
                strSQL = strSQL + "       or " + vbCr
                strSQL = strSQL + "       (b.�������� =   1)" + vbCr                                                            '��������
                strSQL = strSQL + "       or " + vbCr
                strSQL = strSQL + "       (b.����״̬ not in (" + strFileAllYWCList + ")) " + vbCr                              '�ļ�δ����
                strSQL = strSQL + "     ) " + vbCr
                strSQL = strSQL + "   ) a " + vbCr
                strSQL = strSQL + "   group by " + vbCr
                strSQL = strSQL + "     a.�ļ���ʶ, a.��ˮ��  ," + vbCr
                strSQL = strSQL + "     a.��������, a.����״̬, a.�ļ�����, a.�ļ�����," + vbCr
                strSQL = strSQL + "     a.�ļ�����, a.���͵�λ, a.�ļ��ֺ�, a.�����̶�, a.���ܵȼ�," + vbCr
                strSQL = strSQL + "     a.���ش���, a.�ļ����, a.�ļ����," + vbCr
                strSQL = strSQL + "     a.�����  , a.���쵥λ, a.�����  , a.�������, a.��������, a.��������" + vbCr
                strSQL = strSQL + " ) a" + vbCr
                If strWhere <> "" Then
                    strSQL = strSQL + " where " + strWhere + vbCr
                End If
                strSQL = strSQL + " order by a.�������� desc, a.�ļ���� desc, a.���ش���, a.�ļ���� desc" + vbCr
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getSQLBWTX_FILE = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �����ļ����͡���ʼ���ڡ��������ڼ��㱸�����ѵ���������SQL
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strWJBS                ��Ҫ�鿴���ļ���ʶ
        '     strUserXM              ����ǰ������Ա����
        '     strBLLX                ����������
        '     strWJLX                ���ļ�����-����������
        '     strQSRQ                ����ʼ����
        '     strZZRQ                ����������
        '     strWhere               ����������
        '     strSQL                 ��(����)��������SQL
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Private Function getSQLBWTX_TASK( _
            ByRef strErrMsg As String, _
            ByVal strWJBS As String, _
            ByVal strUserXM As String, _
            ByVal strBLLX As String, _
            ByVal strWJLX As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String, _
            ByVal strWhere As String, _
            ByRef strSQL As String) As Boolean

            getSQLBWTX_TASK = False
            strSQL = ""

            Try
                Dim strFileAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.FileStatusAllYWCList
                Dim strTaskAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TaskStatusAllYWCList
                Dim strLF As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhLf
                Dim strRT As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhRt
                Dim strGWTHCL As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_THCL
                Dim strGWSHCL As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_SHCL
                Dim strGWHFCL As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TASK_HFCL

                '��ʼ������
                If strQSRQ Is Nothing Then strQSRQ = ""
                strQSRQ = strQSRQ.Trim
                If strQSRQ <> "" Then
                    If strQSRQ.IndexOf(" ") < 0 Then
                        strQSRQ = strQSRQ + " 00:00:00"
                    End If
                End If
                If strZZRQ Is Nothing Then strZZRQ = ""
                strZZRQ = strZZRQ.Trim
                If strZZRQ <> "" Then
                    If strZZRQ.IndexOf(" ") < 0 Then
                        strZZRQ = strZZRQ + " 23:59:59"
                    End If
                End If

                '�ҵ��ļ�
                strSQL = ""
                strSQL = strSQL + " select * from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select" + vbCr
                strSQL = strSQL + "     a.�ļ���ʶ, b.��ˮ��  ," + vbCr
                strSQL = strSQL + "     a.��������, a.����״̬, a.��������, b.�ļ�����, b.�ļ�����," + vbCr
                strSQL = strSQL + "     b.�ļ�����, b.���ش���, b.�ļ����, b.�ļ����, b.���쵥λ," + vbCr
                strSQL = strSQL + "     a.������  , a.������  , a.ί����  , a.����˵��" + vbCr
                strSQL = strSQL + "   from" + vbCr

                '��ȡ�����¼
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select" + vbCr
                strSQL = strSQL + "       �ļ���ʶ, ��������, ����״̬," + vbCr
                strSQL = strSQL + "       �������� = case " + vbCr
                strSQL = strSQL + "         when ���ӱ�ʶ like '___1%'    then '" + strGWTHCL + "' " + vbCr
                strSQL = strSQL + "         when ���ӱ�ʶ like '____1%'   then '" + strGWSHCL + "' " + vbCr
                strSQL = strSQL + "         when ���ӱ�ʶ like '______1%' then '" + strGWHFCL + "' " + vbCr
                strSQL = strSQL + "         else �������� end," + vbCr
                strSQL = strSQL + "       ������, ������, ί����, ���ӱ�ʶ, ����˵�� " + vbCr
                strSQL = strSQL + "     from ����_B_����" + vbCr
                strSQL = strSQL + "     where   �ļ���ʶ = '" + strWJBS + "'" + vbCr                                     'ָ���ļ�
                strSQL = strSQL + "     and   ((������   = '" + Trim(strUserXM) + "' and ���ӱ�ʶ like '__1%')" + vbCr   '���յ���
                strSQL = strSQL + "     or     (������   = '" + Trim(strUserXM) + "' and ���ӱ�ʶ like '_1%'))" + vbCr   '���ͳ���                              '�ҿɼ�
                strSQL = strSQL + "     and   isnull(��������,0) = 1" + vbCr                                             '��Ҫ��������
                If strBLLX <> "" Then
                    strSQL = strSQL + "     and �������� = '" + strBLLX + "'" + vbCr                                     'ָ������
                End If
                If strQSRQ <> "" And strZZRQ <> "" Then                                                                  'ָ������
                    strSQL = strSQL + "     and �������� between '" + strQSRQ + "' and '" + strZZRQ + "' " + vbCr
                ElseIf strQSRQ <> "" Then
                    strSQL = strSQL + "     and �������� >= '" + strQSRQ + "'" + vbCr
                ElseIf strZZRQ <> "" Then
                    strSQL = strSQL + "     and �������� <= '" + strZZRQ + "'" + vbCr
                Else
                End If
                strSQL = strSQL + "   ) a " + vbCr
                '��ȡ�����¼


                '��ȡ�ļ���Ϣ
                strSQL = strSQL + "   left join " + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select " + vbCr
                strSQL = strSQL + "       �ļ���ʶ, ��ˮ��  , " + vbCr
                strSQL = strSQL + "       ��������, ����״̬, �ļ�����, �ļ�����," + vbCr
                strSQL = strSQL + "       �ļ�����, ���͵�λ, " + vbCr
                strSQL = strSQL + "       �ļ��ֺ�, �����̶�, ���ܵȼ�," + vbCr
                strSQL = strSQL + "       ���ش���, �ļ����, �ļ����," + vbCr
                strSQL = strSQL + "       �����  , ���쵥λ, �����  , �������," + vbCr
                strSQL = strSQL + "       ��������" + vbCr
                strSQL = strSQL + "     from ����_V_ȫ�������ļ��� " + vbCr
                strSQL = strSQL + "     where �ļ���ʶ = '" + strWJBS + "'" + vbCr                                            'ָ���ļ�
                If strWJLX <> "" Then
                    strSQL = strSQL + "     and   �ļ����� = '" + strWJLX + "'" + vbCr                                        '����������=�ļ���������
                End If
                strSQL = strSQL + "   ) b on a.�ļ���ʶ = b.�ļ���ʶ " + vbCr
                '��ȡ�ļ���Ϣ


                strSQL = strSQL + "   where b.�ļ���ʶ Is Not Null " + vbCr
                strSQL = strSQL + " ) a " + vbCr
                If strWhere <> "" Then
                    strSQL = strSQL + " where " + strWhere + vbCr
                End If
                strSQL = strSQL + " group by " + vbCr
                strSQL = strSQL + "   a.�ļ���ʶ, a.��ˮ��  ," + vbCr
                strSQL = strSQL + "   a.��������, a.����״̬, a.��������, a.�ļ�����, a.�ļ�����," + vbCr
                strSQL = strSQL + "   a.�ļ�����, a.���ش���, a.�ļ����, a.�ļ����, a.���쵥λ," + vbCr
                strSQL = strSQL + "   a.������  , a.������  , a.ί����  , a.����˵��" + vbCr
                strSQL = strSQL + " order by a.�ļ���� desc, a.���ش���, a.�ļ���� desc" + vbCr
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getSQLBWTX_TASK = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �����ļ����͡���ʼ���ڡ��������ڼ���ָ��ʱ����յ����ļ�����SQL
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strUserXM              ����ǰ������Ա����
        '     strBLLX                ����������
        '     strWJLX                ���ļ�����-����������
        '     strQSRQ                ����ʼ����
        '     strZZRQ                ����������
        '     strWhere               ����������
        '     strSQL                 ��(����)�ļ�����SQL
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Private Function getSQLRecv_FILE( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByVal strBLLX As String, _
            ByVal strWJLX As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String, _
            ByVal strWhere As String, _
            ByRef strSQL As String) As Boolean

            getSQLRecv_FILE = False
            strSQL = ""

            Try
                Dim strFileAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.FileStatusAllYWCList
                Dim strTaskAllYWCList As String = Xydc.Platform.Common.Workflow.BaseFlowObject.TaskStatusAllYWCList
                Dim strLF As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhLf
                Dim strRT As String = Xydc.Platform.Common.Utilities.PulicParameters.CharWjzhRt

                '��ʼ������
                If strQSRQ Is Nothing Then strQSRQ = ""
                strQSRQ = strQSRQ.Trim
                If strQSRQ <> "" Then
                    If strQSRQ.IndexOf(" ") < 0 Then
                        strQSRQ = strQSRQ + " 00:00:00"
                    End If
                End If
                If strZZRQ Is Nothing Then strZZRQ = ""
                strZZRQ = strZZRQ.Trim
                If strZZRQ <> "" Then
                    If strZZRQ.IndexOf(" ") < 0 Then
                        strZZRQ = strZZRQ + " 23:59:59"
                    End If
                End If

                '�ҵ��ļ�
                strSQL = ""
                strSQL = strSQL + " select a.*" + vbCr
                strSQL = strSQL + " from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select" + vbCr
                strSQL = strSQL + "     a.�ļ���ʶ, a.��ˮ��  ," + vbCr
                strSQL = strSQL + "     a.��������, a.����״̬, a.�ļ�����, a.�ļ�����," + vbCr
                strSQL = strSQL + "     a.�ļ�����, a.���͵�λ, a.�ļ��ֺ�, a.�����̶�, a.���ܵȼ�," + vbCr
                strSQL = strSQL + "     a.���ش���, a.�ļ����, a.�ļ����," + vbCr
                strSQL = strSQL + "     a.�����  , a.���쵥λ, a.�����  , a.�������,a.��������," + vbCr
                strSQL = strSQL + "     �������� = max(a.��������)," + vbCr
                strSQL = strSQL + "     �������� = max(a.��������)," + vbCr
                strSQL = strSQL + "     ������� = max(a.�������)," + vbCr
                strSQL = strSQL + "     a.��������" + vbCr
                strSQL = strSQL + "   from" + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select" + vbCr
                strSQL = strSQL + "       a.�ļ���ʶ, b.��ˮ��  ," + vbCr
                strSQL = strSQL + "       a.��������, b.����״̬, b.�ļ�����, b.�ļ�����," + vbCr
                strSQL = strSQL + "       b.�ļ�����, b.���͵�λ, b.�ļ��ֺ�, b.�����̶�, b.���ܵȼ�," + vbCr
                strSQL = strSQL + "       b.���ش���, b.�ļ����, b.�ļ����," + vbCr
                strSQL = strSQL + "       b.�����  , b.���쵥λ, b.�����  , b.�������," + vbCr
                strSQL = strSQL + "       a.��������, a.��������, a.�������, b.��������," + vbCr
                strSQL = strSQL + "       �������� = case when c.�������� is null then '��' else c.�������� end" + vbCr
                strSQL = strSQL + "     from" + vbCr

                '��ȡ�����¼
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select a.�ļ���ʶ,a.��������,a.���ӱ�ʶ," + vbCr
                strSQL = strSQL + "         �������� = max(a.��������)," + vbCr
                strSQL = strSQL + "         �������� = max(a.�����������)," + vbCr
                strSQL = strSQL + "         ������� = max(a.�������)" + vbCr
                strSQL = strSQL + "       from" + vbCr
                strSQL = strSQL + "       (" + vbCr
                strSQL = strSQL + "         select" + vbCr
                strSQL = strSQL + "           �ļ���ʶ, ��������," + vbCr
                strSQL = strSQL + "           ���ӱ�ʶ = case when ���ӱ�ʶ like '_____1%' then '1' else '0' end," + vbCr
                strSQL = strSQL + "           ��������," + vbCr
                strSQL = strSQL + "           �����������," + vbCr
                strSQL = strSQL + "           �������" + vbCr
                strSQL = strSQL + "         from ����_B_����" + vbCr
                strSQL = strSQL + "         where ������   =    '" + Trim(strUserXM) + "'" + vbCr              '��Ҫ��
                strSQL = strSQL + "         and   ���ӱ�ʶ like '__1%'" + vbCr                                 '�ҿɼ�
                strSQL = strSQL + "         and   ����״̬ not in (" + strTaskAllYWCList + ")" + vbCr          'û�а���
                If strBLLX <> "" Then
                    strSQL = strSQL + "         and �������� = '" + strBLLX + "'" + vbCr                       'ָ������
                End If
                If strQSRQ <> "" And strZZRQ <> "" Then                                                        'ָ������
                    strSQL = strSQL + "         and �������� between '" + strQSRQ + "' and '" + strZZRQ + "' " + vbCr
                ElseIf strQSRQ <> "" Then
                    strSQL = strSQL + "         and �������� >= '" + strQSRQ + "'" + vbCr
                ElseIf strZZRQ <> "" Then
                    strSQL = strSQL + "         and �������� <= '" + strZZRQ + "'" + vbCr
                Else
                End If
                strSQL = strSQL + "       ) a" + vbCr
                strSQL = strSQL + "       group by a.�ļ���ʶ,a.��������,a.���ӱ�ʶ" + vbCr
                strSQL = strSQL + "     ) a " + vbCr
                '��ȡ�����¼

                '��ȡ��������
                strSQL = strSQL + "     left join" + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select �ļ���ʶ,�������� = case when max(��������) = 1 then '��' else '��' end" + vbCr
                strSQL = strSQL + "       from ����_B_����" + vbCr
                strSQL = strSQL + "       where ������ = '" + strUserXM + "'" + vbCr
                strSQL = strSQL + "       group by �ļ���ʶ" + vbCr
                strSQL = strSQL + "     ) c on a.�ļ���ʶ = c.�ļ���ʶ" + vbCr
                '��ȡ��������


                '��ȡ�ļ���Ϣ
                strSQL = strSQL + "     left join " + vbCr
                strSQL = strSQL + "     (" + vbCr
                strSQL = strSQL + "       select " + vbCr
                strSQL = strSQL + "         a.�ļ���ʶ, a.��ˮ��  , " + vbCr
                strSQL = strSQL + "         a.��������, a.����״̬, a.�ļ�����, a.�ļ�����," + vbCr
                strSQL = strSQL + "         a.�ļ�����, a.���͵�λ, " + vbCr
                strSQL = strSQL + "         a.�ļ��ֺ�, a.�����̶�, a.���ܵȼ�," + vbCr
                strSQL = strSQL + "         a.���ش���, a.�ļ����, a.�ļ����," + vbCr
                strSQL = strSQL + "         a.�����  , a.���쵥λ, a.�����  , a.�������," + vbCr
                strSQL = strSQL + "         a.��������" + vbCr
                strSQL = strSQL + "       from ����_V_ȫ�������ļ��� a" + vbCr
                strSQL = strSQL + "       left join" + vbCr
                strSQL = strSQL + "       (" + vbCr
                strSQL = strSQL + "         select �ļ���ʶ" + vbCr
                strSQL = strSQL + "         from ����_B_����" + vbCr
                strSQL = strSQL + "         where ������   =    '" + Trim(strUserXM) + "'" + vbCr              '��Ҫ��
                strSQL = strSQL + "         and   ���ӱ�ʶ like '__1%'" + vbCr                                 '�ҿɼ�
                strSQL = strSQL + "         and   ����״̬ not in (" + strTaskAllYWCList + ")" + vbCr          'û�а���
                If strBLLX <> "" Then
                    strSQL = strSQL + "         and �������� = '" + strBLLX + "'" + vbCr                       'ָ������
                End If
                If strQSRQ <> "" And strZZRQ <> "" Then                                                        'ָ������
                    strSQL = strSQL + "         and �������� between '" + strQSRQ + "' and '" + strZZRQ + "' " + vbCr
                ElseIf strQSRQ <> "" Then
                    strSQL = strSQL + "         and �������� >= '" + strQSRQ + "'" + vbCr
                ElseIf strZZRQ <> "" Then
                    strSQL = strSQL + "         and �������� <= '" + strZZRQ + "'" + vbCr
                Else
                End If
                strSQL = strSQL + "         group by �ļ���ʶ" + vbCr
                strSQL = strSQL + "       ) b on a.�ļ���ʶ = b.�ļ���ʶ" + vbCr
                strSQL = strSQL + "       where b.�ļ���ʶ is not null" + vbCr
                If strWJLX <> "" Then
                    strSQL = strSQL + "       and a.�ļ����� = '" + strWJLX + "'" + vbCr
                End If
                strSQL = strSQL + "     ) b on a.�ļ���ʶ = b.�ļ���ʶ " + vbCr
                '��ȡ�ļ���Ϣ


                strSQL = strSQL + "     where b.�ļ���ʶ Is Not Null " + vbCr
                strSQL = strSQL + "     and (" + vbCr
                strSQL = strSQL + "       (a.���ӱ�ʶ = '1')" + vbCr                                                            '֪ͨ����Ϣ
                strSQL = strSQL + "       or " + vbCr
                strSQL = strSQL + "       (b.�������� =   1)" + vbCr                                                            '��������
                strSQL = strSQL + "       or " + vbCr
                strSQL = strSQL + "       (b.����״̬ not in (" + strFileAllYWCList + ")) " + vbCr                              '�ļ�δ����
                strSQL = strSQL + "     ) " + vbCr
                strSQL = strSQL + "   ) a " + vbCr
                strSQL = strSQL + "   group by " + vbCr
                strSQL = strSQL + "     a.�ļ���ʶ, a.��ˮ��  ," + vbCr
                strSQL = strSQL + "     a.��������, a.����״̬, a.�ļ�����, a.�ļ�����," + vbCr
                strSQL = strSQL + "     a.�ļ�����, a.���͵�λ, a.�ļ��ֺ�, a.�����̶�, a.���ܵȼ�," + vbCr
                strSQL = strSQL + "     a.���ش���, a.�ļ����, a.�ļ����," + vbCr
                strSQL = strSQL + "     a.�����  , a.���쵥λ, a.�����  , a.�������, a.��������, a.��������" + vbCr
                strSQL = strSQL + " ) a" + vbCr
                If strWhere <> "" Then
                    strSQL = strSQL + " where " + strWhere + vbCr
                End If
                strSQL = strSQL + " order by a.�������� desc, a.�ļ���� desc, a.���ش���, a.�ļ���� desc" + vbCr
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getSQLRecv_FILE = True
            Exit Function
errProc:
            Exit Function

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

            Dim objTempFileData As Xydc.Platform.Common.Data.grswMyTaskData

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            getMyTaskFileData = False
            objFileData = Nothing
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                If strUserXM Is Nothing Then strUserXM = ""
                If strWhere Is Nothing Then strWhere = ""
                strUserId = strUserId.Trim()
                strUserXM = strUserXM.Trim()
                strWhere = strWhere.Trim()
                If strUserId = "" Then
                    strErrMsg = "����δָ�������û���"
                    GoTo errProc
                End If
                If strUserXM = "" Then
                    strErrMsg = "����δָ����ǰ�û���"
                    GoTo errProc
                End If
                If objNodeData Is Nothing Then
                    strErrMsg = "����δѡ������"
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '�������ݼ�
                objTempFileData = New Xydc.Platform.Common.Data.grswMyTaskData(Xydc.Platform.Common.Data.grswMyTaskData.enumTableType.GR_B_MYTASK_FILE)

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '��ȡ������Ϣ
                Dim strCode As String
                Dim strWJLX As String
                Dim strBLLX As String
                Dim strQSRQ As String
                Dim strZZRQ As String
                strCode = objPulicParameters.getObjectValue(objNodeData.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_CODE), "")
                strWJLX = objPulicParameters.getObjectValue(objNodeData.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_WJLX), "")
                strBLLX = objPulicParameters.getObjectValue(objNodeData.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_BLLX), "")
                strQSRQ = objPulicParameters.getObjectValue(objNodeData.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ), "")
                strZZRQ = objPulicParameters.getObjectValue(objNodeData.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ), "")

                'ִ�м���
                With Me.SqlDataAdapter
                    '����SQL
                    Dim intType As Integer = CType(strCode.Substring(0, Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(0)), Integer)
                    Select Case intType
                        Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.DBSY  '��������
                            If Me.getSQLDBSY_FILE(strErrMsg, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, strWhere, strSQL) = False Then
                                GoTo errProc
                            End If

                        Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.DPWJ  '�����ļ�
                            If Me.getSQLDPWJ_FILE(strErrMsg, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, strWhere, strSQL) = False Then
                                GoTo errProc
                            End If

                        Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.HBWJ  '�����ļ�
                            If Me.getSQLHBWJ_FILE(strErrMsg, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, strWhere, strSQL) = False Then
                                GoTo errProc
                            End If

                        Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.YBSY  '�Ѱ�����
                            If Me.getSQLYBSY_FILE(strErrMsg, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, strWhere, strSQL) = False Then
                                GoTo errProc
                            End If

                        Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.GQSY  '��������
                            If Me.getSQLGQSY_FILE(strErrMsg, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, strWhere, strSQL) = False Then
                                GoTo errProc
                            End If

                        Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.CBSY  '�߰�����
                            If Me.getSQLCBSY_FILE(strErrMsg, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, strWhere, strSQL) = False Then
                                GoTo errProc
                            End If

                        Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.BCSY  '��������
                            If Me.getSQLBCSY_FILE(strErrMsg, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, strWhere, strSQL) = False Then
                                GoTo errProc
                            End If

                        Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.DBWJ  '�����ļ�
                            If Me.getSQLDBWJ_FILE(strErrMsg, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, strWhere, strSQL) = False Then
                                GoTo errProc
                            End If

                        Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.BDWJ  '�����ļ�
                            If Me.getSQLBDWJ_FILE(strErrMsg, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, strWhere, strSQL) = False Then
                                GoTo errProc
                            End If

                        Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.QBSY  'ȫ������
                            If Me.getSQLQBSY_FILE(strErrMsg, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, strWhere, strSQL) = False Then
                                GoTo errProc
                            End If

                        Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.BWTX  '��������
                            If Me.getSQLBWTX_FILE(strErrMsg, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, strWhere, strSQL) = False Then
                                GoTo errProc
                            End If

                    End Select

                    '���ò���
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    'ִ�в���
                    .Fill(objTempFileData.Tables(Xydc.Platform.Common.Data.grswMyTaskData.TABLE_GR_B_MYTASK_FILE))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            objFileData = objTempFileData
            getMyTaskFileData = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.grswMyTaskData.SafeRelease(objTempFileData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

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

            Dim objTempTaskData As Xydc.Platform.Common.Data.grswMyTaskData

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            getMyTaskTaskData = False
            objTaskData = Nothing
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                If strUserXM Is Nothing Then strUserXM = ""
                If strWJBS Is Nothing Then strWJBS = ""
                If strWhere Is Nothing Then strWhere = ""
                strUserId = strUserId.Trim()
                strUserXM = strUserXM.Trim()
                strWJBS = strWJBS.Trim()
                strWhere = strWhere.Trim()
                If strUserId = "" Then
                    strErrMsg = "����δָ�������û���"
                    GoTo errProc
                End If
                If strUserXM = "" Then
                    strErrMsg = "����δָ����ǰ�û���"
                    GoTo errProc
                End If
                If objNodeData Is Nothing Then
                    strErrMsg = "����δѡ������"
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '�������ݼ�
                objTempTaskData = New Xydc.Platform.Common.Data.grswMyTaskData(Xydc.Platform.Common.Data.grswMyTaskData.enumTableType.GR_B_MYTASK_TASK)

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '��ȡ������Ϣ
                Dim strCode As String
                Dim strWJLX As String
                Dim strBLLX As String
                Dim strQSRQ As String
                Dim strZZRQ As String
                strCode = objPulicParameters.getObjectValue(objNodeData.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_CODE), "")
                strWJLX = objPulicParameters.getObjectValue(objNodeData.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_WJLX), "")
                strBLLX = objPulicParameters.getObjectValue(objNodeData.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_BLLX), "")
                strQSRQ = objPulicParameters.getObjectValue(objNodeData.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_KSSJ), "")
                strZZRQ = objPulicParameters.getObjectValue(objNodeData.Item(Xydc.Platform.Common.Data.grswMyTaskData.FIELD_GR_B_MYTASK_NODE_JSSJ), "")

                'ִ�м���
                With Me.SqlDataAdapter
                    '����SQL
                    Dim intType As Integer = CType(strCode.Substring(0, Xydc.Platform.Common.Data.grswMyTaskData.intJDDM_FJCDSM(0)), Integer)
                    Select Case intType
                        Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.DBSY  '��������
                            If Me.getSQLDBSY_TASK(strErrMsg, strWJBS, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, strWhere, strSQL) = False Then
                                GoTo errProc
                            End If

                        Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.DPWJ  '�����ļ�
                            If Me.getSQLDPWJ_TASK(strErrMsg, strWJBS, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, strWhere, strSQL) = False Then
                                GoTo errProc
                            End If

                        Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.HBWJ  '�����ļ�
                            If Me.getSQLHBWJ_TASK(strErrMsg, strWJBS, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, strWhere, strSQL) = False Then
                                GoTo errProc
                            End If

                        Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.YBSY  '�Ѱ�����
                            If Me.getSQLYBSY_TASK(strErrMsg, strWJBS, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, strWhere, strSQL) = False Then
                                GoTo errProc
                            End If

                        Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.GQSY  '��������
                            If Me.getSQLGQSY_TASK(strErrMsg, strWJBS, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, strWhere, strSQL) = False Then
                                GoTo errProc
                            End If

                        Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.CBSY  '�߰�����
                            If Me.getSQLCBSY_TASK(strErrMsg, strWJBS, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, strWhere, strSQL) = False Then
                                GoTo errProc
                            End If

                        Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.BCSY  '��������
                            If Me.getSQLBCSY_TASK(strErrMsg, strWJBS, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, strWhere, strSQL) = False Then
                                GoTo errProc
                            End If

                        Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.DBWJ  '�����ļ�
                            If Me.getSQLDBWJ_TASK(strErrMsg, strWJBS, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, strWhere, strSQL) = False Then
                                GoTo errProc
                            End If

                        Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.BDWJ  '�����ļ�
                            If Me.getSQLBDWJ_TASK(strErrMsg, strWJBS, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, strWhere, strSQL) = False Then
                                GoTo errProc
                            End If

                        Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.QBSY  'ȫ������
                            If Me.getSQLQBSY_TASK(strErrMsg, strWJBS, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, strWhere, strSQL) = False Then
                                GoTo errProc
                            End If

                        Case Xydc.Platform.Common.Data.grswMyTaskData.enumTaskTypeLevel1.BWTX  '��������
                            If Me.getSQLBWTX_TASK(strErrMsg, strWJBS, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, strWhere, strSQL) = False Then
                                GoTo errProc
                            End If

                    End Select

                    '���ò���
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    'ִ�в���
                    .Fill(objTempTaskData.Tables(Xydc.Platform.Common.Data.grswMyTaskData.TABLE_GR_B_MYTASK_TASK))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            objTaskData = objTempTaskData
            getMyTaskTaskData = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.grswMyTaskData.SafeRelease(objTempTaskData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

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

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objDataSet As Xydc.Platform.Common.Data.grswMyTaskData
            Dim strSQL As String

            getDataSetDBSY = False
            objDataSetDBSY = Nothing
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim()
                If strUserId = "" Then
                    strErrMsg = "����δָ�������û���"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()
                If strUserXM = "" Then
                    strErrMsg = "����δָ����ǰ�û���"
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '�������ݼ�
                objDataSet = New Xydc.Platform.Common.Data.grswMyTaskData(Xydc.Platform.Common.Data.grswMyTaskData.enumTableType.GR_B_MYTASK_FILE)

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '��ȡ������Ϣ
                Dim strWJLX As String = ""
                Dim strBLLX As String = ""
                Dim strQSRQ As String = ""
                Dim strZZRQ As String = ""

                'ִ�м���
                With Me.SqlDataAdapter
                    '����SQL
                    If Me.getSQLDBSY_FILE(strErrMsg, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, "", strSQL) = False Then
                        GoTo errProc
                    End If

                    '���ò���
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    'ִ�в���
                    .Fill(objDataSet.Tables(Xydc.Platform.Common.Data.grswMyTaskData.TABLE_GR_B_MYTASK_FILE))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            objDataSetDBSY = objDataSet
            getDataSetDBSY = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.grswMyTaskData.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

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

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objDataSet As Xydc.Platform.Common.Data.grswMyTaskData
            Dim strSQL As String

            getDataSetGQSY = False
            objDataSetGQSY = Nothing
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim()
                If strUserId = "" Then
                    strErrMsg = "����δָ�������û���"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()
                If strUserXM = "" Then
                    strErrMsg = "����δָ����ǰ�û���"
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '�������ݼ�
                objDataSet = New Xydc.Platform.Common.Data.grswMyTaskData(Xydc.Platform.Common.Data.grswMyTaskData.enumTableType.GR_B_MYTASK_FILE)

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '��ȡ������Ϣ
                Dim strWJLX As String = ""
                Dim strBLLX As String = ""
                Dim strQSRQ As String = ""
                Dim strZZRQ As String = Now.ToString("yyyy-MM-dd")

                'ִ�м���
                With Me.SqlDataAdapter
                    '����SQL
                    If Me.getSQLGQSY_FILE(strErrMsg, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, "", strSQL) = False Then
                        GoTo errProc
                    End If

                    '���ò���
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    'ִ�в���
                    .Fill(objDataSet.Tables(Xydc.Platform.Common.Data.grswMyTaskData.TABLE_GR_B_MYTASK_FILE))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            objDataSetGQSY = objDataSet
            getDataSetGQSY = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.grswMyTaskData.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

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

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objDataSet As Xydc.Platform.Common.Data.grswMyTaskData
            Dim strSQL As String

            getDataSetBWTX = False
            objDataSetBWTX = Nothing
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim()
                If strUserId = "" Then
                    strErrMsg = "����δָ�������û���"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()
                If strUserXM = "" Then
                    strErrMsg = "����δָ����ǰ�û���"
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '�������ݼ�
                objDataSet = New Xydc.Platform.Common.Data.grswMyTaskData(Xydc.Platform.Common.Data.grswMyTaskData.enumTableType.GR_B_MYTASK_FILE)

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '��ȡ������Ϣ
                Dim strWJLX As String = ""
                Dim strBLLX As String = ""
                Dim strQSRQ As String = ""
                Dim strZZRQ As String = ""

                'ִ�м���
                With Me.SqlDataAdapter
                    '����SQL
                    If Me.getSQLBWTX_FILE(strErrMsg, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, "", strSQL) = False Then
                        GoTo errProc
                    End If

                    '���ò���
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    'ִ�в���
                    .Fill(objDataSet.Tables(Xydc.Platform.Common.Data.grswMyTaskData.TABLE_GR_B_MYTASK_FILE))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            objDataSetBWTX = objDataSet
            getDataSetBWTX = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.grswMyTaskData.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

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

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            getCountDBSY = False
            intCountDBSY = 0
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim()
                If strUserId = "" Then
                    strErrMsg = "����δָ�������û���"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()
                If strUserXM = "" Then
                    strErrMsg = "����δָ����ǰ�û���"
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '�������ݼ�
                objDataSet = New System.Data.DataSet

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '��ȡ������Ϣ
                Dim strWJLX As String = ""
                Dim strBLLX As String = ""
                Dim strQSRQ As String = ""
                Dim strZZRQ As String = ""

                'ִ�м���
                With Me.SqlDataAdapter
                    '����SQL
                    If Me.getSQLDBSY_FILE(strErrMsg, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, "", strSQL) = False Then
                        GoTo errProc
                    End If

                    '�ؽ�SQL
                    Dim strTempSQL As String
                    Dim intEnd As Integer
                    strSQL = strSQL.Trim
                    intEnd = strSQL.IndexOf("order by ")
                    strTempSQL = strSQL.Substring(0, intEnd)
                    strSQL = ""
                    strSQL = strSQL + " select count(*)" + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   " + strTempSQL + vbCr
                    strSQL = strSQL + " ) a" + vbCr

                    '���ò���
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    'ִ�в���
                    .Fill(objDataSet)
                End With

                '������Ϣ
                If Not (objDataSet Is Nothing) Then
                    If Not (objDataSet.Tables(0) Is Nothing) Then
                        intCountDBSY = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item(0), 0)
                    End If
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getCountDBSY = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

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

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            getCountGQSY = False
            intCountGQSY = 0
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim()
                If strUserId = "" Then
                    strErrMsg = "����δָ�������û���"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()
                If strUserXM = "" Then
                    strErrMsg = "����δָ����ǰ�û���"
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '�������ݼ�
                objDataSet = New System.Data.DataSet

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '��ȡ������Ϣ
                Dim strWJLX As String = ""
                Dim strBLLX As String = ""
                Dim strQSRQ As String = ""
                Dim strZZRQ As String = Now.ToString("yyyy-MM-dd")

                'ִ�м���
                With Me.SqlDataAdapter
                    '����SQL
                    If Me.getSQLGQSY_FILE(strErrMsg, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, "", strSQL) = False Then
                        GoTo errProc
                    End If

                    '�ؽ�SQL
                    Dim strTempSQL As String
                    Dim intEnd As Integer
                    strSQL = strSQL.Trim
                    intEnd = strSQL.IndexOf("order by ")
                    strTempSQL = strSQL.Substring(0, intEnd)
                    strSQL = ""
                    strSQL = strSQL + " select count(*)" + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   " + strTempSQL + vbCr
                    strSQL = strSQL + " ) a" + vbCr

                    '���ò���
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    'ִ�в���
                    .Fill(objDataSet)
                End With

                '������Ϣ
                If Not (objDataSet Is Nothing) Then
                    If Not (objDataSet.Tables(0) Is Nothing) Then
                        intCountGQSY = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item(0), 0)
                    End If
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getCountGQSY = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

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

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            getCountBWTX = False
            intCountBWTX = 0
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim()
                If strUserId = "" Then
                    strErrMsg = "����δָ�������û���"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()
                If strUserXM = "" Then
                    strErrMsg = "����δָ����ǰ�û���"
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '�������ݼ�
                objDataSet = New System.Data.DataSet

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '��ȡ������Ϣ
                Dim strWJLX As String = ""
                Dim strBLLX As String = ""
                Dim strQSRQ As String = ""
                Dim strZZRQ As String = ""

                'ִ�м���
                With Me.SqlDataAdapter
                    '����SQL
                    If Me.getSQLBWTX_FILE(strErrMsg, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, "", strSQL) = False Then
                        GoTo errProc
                    End If

                    '�ؽ�SQL
                    Dim strTempSQL As String
                    Dim intEnd As Integer
                    strSQL = strSQL.Trim
                    intEnd = strSQL.IndexOf("order by ")
                    strTempSQL = strSQL.Substring(0, intEnd)
                    strSQL = ""
                    strSQL = strSQL + " select count(*)" + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   " + strTempSQL + vbCr
                    strSQL = strSQL + " ) a" + vbCr

                    '���ò���
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    'ִ�в���
                    .Fill(objDataSet)
                End With

                '������Ϣ
                If Not (objDataSet Is Nothing) Then
                    If Not (objDataSet.Tables(0) Is Nothing) Then
                        intCountBWTX = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item(0), 0)
                    End If
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getCountBWTX = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

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

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            getCountRecv = False
            intCountRecv = 0
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim()
                If strUserId = "" Then
                    strErrMsg = "����δָ�������û���"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()
                If strUserXM = "" Then
                    strErrMsg = "����δָ����ǰ�û���"
                    GoTo errProc
                End If
                If strZDSJ Is Nothing Then strZDSJ = ""
                strZDSJ = strZDSJ.Trim
                If strZDSJ = "" Then
                    strErrMsg = "����δָ��ʱ�䣡"
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '�������ݼ�
                objDataSet = New System.Data.DataSet

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '��ȡ������Ϣ
                Dim strWJLX As String = ""
                Dim strBLLX As String = ""
                Dim strQSRQ As String = strZDSJ
                Dim strZZRQ As String = ""

                'ִ�м���
                With Me.SqlDataAdapter
                    '����SQL
                    If Me.getSQLRecv_FILE(strErrMsg, strUserXM, strBLLX, strWJLX, strQSRQ, strZZRQ, "", strSQL) = False Then
                        GoTo errProc
                    End If

                    '�ؽ�SQL
                    Dim strTempSQL As String
                    Dim intEnd As Integer
                    strSQL = strSQL.Trim
                    intEnd = strSQL.IndexOf("order by ")
                    strTempSQL = strSQL.Substring(0, intEnd)
                    strSQL = ""
                    strSQL = strSQL + " select count(*)" + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   " + strTempSQL + vbCr
                    strSQL = strSQL + " ) a" + vbCr

                    '���ò���
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    'ִ�в���
                    .Fill(objDataSet)
                End With

                '������Ϣ
                If Not (objDataSet Is Nothing) Then
                    If Not (objDataSet.Tables(0) Is Nothing) Then
                        intCountRecv = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item(0), 0)
                    End If
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getCountRecv = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

    End Class

End Namespace
