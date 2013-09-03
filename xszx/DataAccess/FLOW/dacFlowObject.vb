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
    ' ����    ��FlowObject
    '
    ' ����������
    '     ��������������ݲ�Ļ�����
    '----------------------------------------------------------------
    Public MustInherit Class FlowObject
        Implements IDisposable

        '�������͡����󴴽��ӿ�ע����(���ж�����)
        Private Shared m_objFlowTypeBLLXEnum As System.Collections.Specialized.NameValueCollection
        Private Shared m_objFlowTypeNameEnum As System.Collections.Specialized.NameValueCollection
        Private Shared m_objFlowTypeEnum As System.Collections.Specialized.ListDictionary

        '�����ʼ����־
        Private m_blnInitialized As Boolean      '�����Ƿ��ʼ����
        Private m_blnFillData As Boolean         '�Ƿ����������

        '���ݿ�����������
        Private m_objSqlDataAdapter As System.Data.SqlClient.SqlDataAdapter
        Private m_objSqlConnection As System.Data.SqlClient.SqlConnection

        '��������Ӧ��Ӧ������
        Private m_objFlowAppData As Xydc.Platform.Common.Workflow.BaseFlowObject









        '----------------------------------------------------------------
        ' �������캯��
        '----------------------------------------------------------------
        Protected Sub New()
            MyBase.New()

            m_blnInitialized = False
            m_blnFillData = False

            m_objSqlDataAdapter = New System.Data.SqlClient.SqlDataAdapter
            m_objSqlConnection = New System.Data.SqlClient.SqlConnection
            m_objFlowAppData = Nothing

        End Sub

        '----------------------------------------------------------------
        ' �������캯��
        '----------------------------------------------------------------
        Protected Sub New(ByVal strFlowType As String, ByVal strFlowTypeName As String)

            Me.New()

            'ע����
            Try
                Dim strType As String
                Dim strName As String
                strType = strFlowType
                strName = strFlowTypeName
                If m_objFlowTypeEnum Is Nothing Then
                    Throw New Exception("��������[Create]��������[" + strFlowTypeName + "]��������")
                Else
                    If m_objFlowTypeEnum.Item(strType) Is Nothing Then
                        Throw New Exception("��������[Create]��������[" + strFlowTypeName + "]��������")
                    End If
                End If
            Catch ex As Exception
                Throw ex
            End Try

            '��������
            Try
                m_objFlowAppData = Xydc.Platform.Common.Workflow.BaseFlowObject.Create(strFlowType)
            Catch ex As Exception
                Throw ex
            End Try

        End Sub

        '----------------------------------------------------------------
        ' ��������(���������)
        '----------------------------------------------------------------
        Public Overridable Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(True)
        End Sub

        '----------------------------------------------------------------
        ' ��������(����)
        '----------------------------------------------------------------
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If (Not disposing) Then
                Exit Sub
            End If
            If Not (m_objSqlConnection Is Nothing) Then
                m_objSqlConnection.Dispose()
                m_objSqlConnection = Nothing
            End If
            If Not (m_objFlowAppData Is Nothing) Then
                m_objFlowAppData.Dispose()
                m_objFlowAppData = Nothing
            End If
            If Not m_objSqlDataAdapter Is Nothing Then
                m_objSqlDataAdapter.Dispose()
                m_objSqlDataAdapter = Nothing
            End If
        End Sub

        '----------------------------------------------------------------
        ' ��ȫ�ͷű�����Դ
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.DataAccess.FlowObject)
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
        ' SqlConnection����
        '----------------------------------------------------------------
        Public ReadOnly Property SqlConnection() As System.Data.SqlClient.SqlConnection
            Get
                SqlConnection = m_objSqlConnection
            End Get
        End Property

        '----------------------------------------------------------------
        ' FlowData����
        '----------------------------------------------------------------
        Public ReadOnly Property FlowData() As Xydc.Platform.Common.Workflow.BaseFlowObject
            Get
                FlowData = m_objFlowAppData
            End Get
        End Property

        '----------------------------------------------------------------
        ' IsInitialized����
        '----------------------------------------------------------------
        Public ReadOnly Property IsInitialized() As Boolean
            Get
                IsInitialized = m_blnInitialized
            End Get
        End Property

        '----------------------------------------------------------------
        ' IsFillData����
        '----------------------------------------------------------------
        Public ReadOnly Property IsFillData() As Boolean
            Get
                IsFillData = Me.m_blnFillData
            End Get
        End Property

        '----------------------------------------------------------------
        ' FlowType����
        '----------------------------------------------------------------
        Public ReadOnly Property FlowType() As String
            Get
                FlowType = FlowData.FlowType
            End Get
        End Property

        '----------------------------------------------------------------
        ' FlowTypeName����
        '----------------------------------------------------------------
        Public ReadOnly Property FlowTypeName() As String
            Get
                FlowTypeName = FlowData.FlowTypeName
            End Get
        End Property

        '----------------------------------------------------------------
        ' FlowBLLXName����
        '----------------------------------------------------------------
        Public ReadOnly Property FlowBLLXName() As String
            Get
                FlowBLLXName = Me.m_objFlowTypeBLLXEnum(FlowData.FlowType)
            End Get
        End Property

        '----------------------------------------------------------------
        ' WJBS����
        '----------------------------------------------------------------
        Public ReadOnly Property WJBS() As String
            Get
                WJBS = FlowData.WJBS
            End Get
        End Property

        '----------------------------------------------------------------
        ' LSH����
        '----------------------------------------------------------------
        Public ReadOnly Property LSH() As String
            Get
                LSH = FlowData.LSH
            End Get
        End Property

        '----------------------------------------------------------------
        ' Status����
        '----------------------------------------------------------------
        Public ReadOnly Property Status() As String
            Get
                Status = FlowData.Status
            End Get
        End Property

        '----------------------------------------------------------------
        ' PZR����
        '----------------------------------------------------------------
        Public ReadOnly Property PZR() As String
            Get
                PZR = FlowData.PZR
            End Get
        End Property

        '----------------------------------------------------------------
        ' PZRQ����
        '----------------------------------------------------------------
        Public ReadOnly Property PZRQ() As System.DateTime
            Get
                PZRQ = FlowData.PZRQ
            End Get
        End Property

        '----------------------------------------------------------------
        ' DDSZ����
        '----------------------------------------------------------------
        Public ReadOnly Property DDSZ() As Integer
            Get
                DDSZ = FlowData.DDSZ
            End Get
        End Property







        '----------------------------------------------------------------
        ' FlowTypeNameCollection����
        '----------------------------------------------------------------
        Public Shared ReadOnly Property FlowTypeNameCollection() As System.Collections.Specialized.NameValueCollection
            Get
                Try
                    FlowTypeNameCollection = New System.Collections.Specialized.NameValueCollection
                    Dim intCount As Integer
                    Dim i As Integer
                    intCount = m_objFlowTypeNameEnum.Count
                    For i = 0 To intCount - 1 Step 1
                        FlowTypeNameCollection.Add(m_objFlowTypeNameEnum.GetKey(i), m_objFlowTypeNameEnum(i))
                    Next
                Catch ex As Exception
                    FlowTypeNameCollection = Nothing
                End Try
            End Get
        End Property

        '----------------------------------------------------------------
        ' FlowTypeBLLXCollection����
        '----------------------------------------------------------------
        Public Shared ReadOnly Property FlowTypeBLLXCollection() As System.Collections.Specialized.NameValueCollection
            Get
                Try
                    FlowTypeBLLXCollection = New System.Collections.Specialized.NameValueCollection
                    Dim intCount As Integer
                    Dim i As Integer
                    intCount = m_objFlowTypeBLLXEnum.Count
                    For i = 0 To intCount - 1 Step 1
                        FlowTypeBLLXCollection.Add(m_objFlowTypeBLLXEnum.GetKey(i), m_objFlowTypeBLLXEnum(i))
                    Next
                Catch ex As Exception
                    FlowTypeBLLXCollection = Nothing
                End Try
            End Get
        End Property

        '----------------------------------------------------------------
        ' ����������ע����
        '     strFlowType          �����������ʹ���
        '     strFlowTypeName      ���������������� - ���幤��������
        '     strFlowTypeBLLX      ��strFlowTypeName����strFlowTypeBLLX��
        '     objCreator           ������������IFlowObjectCreate�ӿ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Shared Function RegisterFlowType( _
            ByVal strFlowType As String, _
            ByVal strFlowTypeName As String, _
            ByVal strFlowTypeBLLX As String, _
            ByVal objCreator As Xydc.Platform.DataAccess.IFlowObjectCreate) As Boolean

            RegisterFlowType = False

            Try
                '�������
                If strFlowType Is Nothing Then
                    Throw New Exception("����[����������]����Ϊ�գ�")
                End If
                strFlowType = strFlowType.Trim()
                If strFlowType = "" Then
                    Throw New Exception("����[����������]����Ϊ�գ�")
                End If
                If strFlowTypeName Is Nothing Then
                    Throw New Exception("����[��������������]����Ϊ�գ�")
                End If
                strFlowTypeName = strFlowTypeName.Trim()
                If strFlowTypeName = "" Then
                    Throw New Exception("����[��������������]����Ϊ�գ�")
                End If
                If objCreator Is Nothing Then
                    Throw New Exception("����[IFlowObjectCreate]����Ϊ�գ�")
                End If

                '�������ͻ㼯��
                If m_objFlowTypeEnum Is Nothing Then
                    m_objFlowTypeEnum = New System.Collections.Specialized.ListDictionary
                End If
                If m_objFlowTypeNameEnum Is Nothing Then
                    m_objFlowTypeNameEnum = New System.Collections.Specialized.NameValueCollection
                End If
                If m_objFlowTypeBLLXEnum Is Nothing Then
                    m_objFlowTypeBLLXEnum = New System.Collections.Specialized.NameValueCollection
                End If

                '��������Ƿ����
                If Not (m_objFlowTypeEnum.Item(strFlowType) Is Nothing) Then
                    Exit Try
                End If

                '������������Ƿ��ظ�
                Dim strNewName As String = strFlowTypeName
                Dim strOldName As String
                Dim intCount As Integer
                Dim i As Integer
                intCount = m_objFlowTypeNameEnum.Count
                strNewName = strNewName.ToUpper()
                For i = 0 To intCount - 1 Step 1
                    strOldName = m_objFlowTypeNameEnum.Item(i)
                    strOldName = strOldName.Trim()
                    strOldName = strOldName.ToUpper()
                    If strNewName = strOldName Then
                        Throw New Exception("����[" + strNewName + "]�Ѿ�ע�����")
                    End If
                Next

                'ע��
                m_objFlowTypeEnum.Add(strFlowType, objCreator)
                m_objFlowTypeNameEnum.Add(strFlowType, strFlowTypeName)
                m_objFlowTypeBLLXEnum.Add(strFlowType, strFlowTypeBLLX)

                RegisterFlowType = True

            Catch ex As Exception
                Throw ex
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��������������
        '     strFlowType          �����������ʹ���
        '     strFlowTypeName      ����������������
        ' ����
        '                          ��Xydc.Platform.DataAccess.FlowObject����
        '----------------------------------------------------------------
        Public Shared Function Create( _
            ByVal strFlowType As String, _
            ByVal strFlowTypeName As String) As Xydc.Platform.DataAccess.FlowObject

            Create = Nothing

            Try
                '�������
                If strFlowType Is Nothing Then
                    Throw New Exception("����[����������]����Ϊ�գ�")
                End If
                strFlowType = strFlowType.Trim()
                If strFlowType = "" Then
                    Throw New Exception("����[����������]����Ϊ�գ�")
                End If
                If strFlowTypeName Is Nothing Then
                    Throw New Exception("����[��������������]����Ϊ�գ�")
                End If
                strFlowTypeName = strFlowTypeName.Trim()
                If strFlowTypeName = "" Then
                    Throw New Exception("����[��������������]����Ϊ�գ�")
                End If

                'ע���Ѿ�ʵ�ֵ�FlowObject
                Dim strType As String
                Dim strName As String
                Dim strBLLX As String

                

                '************************************************************************************************************
                '���鵥������
                'strType = Xydc.Platform.Common.Workflow.BaseFlowDuchadan.FLOWCODE
                'strName = Xydc.Platform.Common.Workflow.BaseFlowDuchadan.FLOWNAME
                'strBLLX = Xydc.Platform.Common.Workflow.BaseFlowDuchadan.FLOWBLLX
                'If m_objFlowTypeEnum Is Nothing Then
                '    RegisterFlowType(strType, strName, strBLLX, New Xydc.Platform.DataAccess.FlowObjectDuchadanCreator)
                'Else
                '    If m_objFlowTypeEnum.Item(strType) Is Nothing Then
                '        RegisterFlowType(strType, strName, strBLLX, New Xydc.Platform.DataAccess.FlowObjectDuchadanCreator)
                '    End If
                'End If

                '��ȡ�ӿ�
                Dim objCreator As Object
                objCreator = m_objFlowTypeEnum.Item(strFlowType)
                If objCreator Is Nothing Then
                    Throw New Exception("����[" + strFlowType + "]��֧�֣�")
                End If
                strBLLX = m_objFlowTypeBLLXEnum.Item(strFlowType)
                Dim objIFlowObjectCreate As Xydc.Platform.DataAccess.IFlowObjectCreate
                objIFlowObjectCreate = CType(objCreator, Xydc.Platform.DataAccess.IFlowObjectCreate)
                If objIFlowObjectCreate Is Nothing Then
                    Throw New Exception("����[" + strFlowType + "]��֧�֣�")
                End If

                '���ýӿڴ�������
                Create = objIFlowObjectCreate.Create(strFlowType, strFlowTypeName)

                '�Զ�������������
                Create.FlowData.FlowType = strFlowType
                Create.FlowData.FlowTypeBLLX = strBLLX
                Create.FlowData.FlowTypeName = strFlowTypeName

            Catch ex As Exception
                Throw ex
            End Try

        End Function

        '----------------------------------------------------------------
        ' ����strFlowTypeName��ȡstrFlowType
        '     strFlowTypeName      ����������������
        ' ����
        '                          ��strFlowType
        '----------------------------------------------------------------
        Public Shared Function getFlowType(ByVal strFlowTypeName As String) As String

            getFlowType = ""
            Try
                Dim intCount As Integer
                Dim i As Integer
                intCount = m_objFlowTypeNameEnum.Count
                For i = 0 To intCount - 1 Step 1
                    If m_objFlowTypeNameEnum.Item(i).ToUpper() = strFlowTypeName.ToUpper() Then
                        getFlowType = m_objFlowTypeNameEnum.Keys(i)
                        Exit For
                    End If
                Next
            Catch ex As Exception
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡ�ļ�strWJBS�İ�������?
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objSqlConnection     �����ݿ�����
        '     strWJBS              ���ļ���ʶ
        '     strBLLX              �����ذ�������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Shared Function getFileBLLX( _
            ByRef strErrMsg As String, _
            ByVal objSqlConnection As System.Data.SqlClient.SqlConnection, _
            ByVal strWJBS As String, _
            ByRef strBLLX As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            getFileBLLX = False
            strBLLX = ""
            strErrMsg = ""

            Try
                '���
                If strWJBS Is Nothing Then strWJBS = ""
                strWJBS = strWJBS.Trim()
                If objSqlConnection Is Nothing Then
                    strErrMsg = "����δָ�����Ӷ���"
                    GoTo errProc
                End If
                If strWJBS = "" Then Exit Try

                '��������
                strSQL = ""
                strSQL = strSQL + " select * from ����_V_ȫ�������ļ��� " + vbCr
                strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "' " + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    Exit Try
                End If

                '������Ϣ
                With objDataSet.Tables(0).Rows(0)
                    strBLLX = objPulicParameters.getObjectValue(.Item("��������"), "")
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getFileBLLX = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ�ļ�strWJBS�İ�������?
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objSqlConnection     �����ݿ�����
        '     strWJBS              ���ļ���ʶ
        '     strBLZL              �����ذ�������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Shared Function getFileBLZL( _
            ByRef strErrMsg As String, _
            ByVal objSqlConnection As System.Data.SqlClient.SqlConnection, _
            ByVal strWJBS As String, _
            ByRef strBLZL As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            getFileBLZL = False
            strBLZL = ""
            strErrMsg = ""

            Try
                '���
                If strWJBS Is Nothing Then strWJBS = ""
                strWJBS = strWJBS.Trim()
                If objSqlConnection Is Nothing Then
                    strErrMsg = "����δָ�����Ӷ���"
                    GoTo errProc
                End If
                If strWJBS = "" Then Exit Try

                '��������
                strSQL = ""
                strSQL = strSQL + " select * from ����_V_ȫ�������ļ��� " + vbCr
                strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "' " + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    Exit Try
                End If

                '������Ϣ
                With objDataSet.Tables(0).Rows(0)
                    strBLZL = objPulicParameters.getObjectValue(.Item("�ļ�����"), "")
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getFileBLZL = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function








        '----------------------------------------------------------------
        ' �����������ļ����뵽ָ���İ�����
        '     strErrMsg            �����ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strAJBS              ��ָ�������ʶ
        '     strTempPath          �������ļ���ʱ���·��
        ' ����
        '     True                 ���ɹ�
        '     False                �����ɹ�
        ' ��ע
        '     ����                 ������
        '     ��������             ������
        '     ��������             �����鵵��
        '----------------------------------------------------------------
        Public MustOverride Function doAddToAnjuan( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strAJBS As String, _
            ByVal strTempPath As String) As Boolean

        '----------------------------------------------------------------
        ' ��ȡȱʡ�������
        '     strYjlx              ����������
        ' ����
        '                          �������־
        '----------------------------------------------------------------
        Public MustOverride Function getDefaultYJNR(ByVal strYJLX As String) As String

        '----------------------------------------------------------------
        ' ��ȡ����������������־
        ' ����
        '                          �������־
        '----------------------------------------------------------------
        Public MustOverride Function getBaocunYijianBLBZ() As String

        '----------------------------------------------------------------
        ' ��ȡ����׼�������־
        ' ����
        '                          �������־
        '----------------------------------------------------------------
        Public MustOverride Function getPizhunBLBZ() As String

        '----------------------------------------------------------------
        ' �Ƕ������ļ�ǩ�����������?����ǣ����������ַ���
        '     strYjlx              ����������
        ' ����
        '     True                 ����Ҫǩ��
        '     False                ������Ҫǩ��
        '----------------------------------------------------------------
        Public MustOverride Function isFileQianminTask( _
            ByVal strYjlx As String, _
            ByRef strPrompt As String) As Boolean

        '----------------------------------------------------------------
        ' ����Ҫǩ��ȷ�ϵ���������?
        '     strYjlx              ����������
        ' ����
        '     True                 ����Ҫǩ��
        '     False                ������Ҫǩ��
        '----------------------------------------------------------------
        Public MustOverride Function isQianminTask(ByVal strYjlx As String) As Boolean

        '----------------------------------------------------------------
        ' ��Ҫǩ��ȷ����ʾ?
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strYjlx              ��Ҫȷ�ϵ��������
        '     strSPR               ��������
        '     blnNeed              ��(����)�Ƿ���Ҫ��ʾ
        '     strXyrList           ��(����)����ǩ�����б�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public MustOverride Function isNeedQianminQuerenPrompt( _
            ByRef strErrMsg As String, _
            ByVal strYjlx As String, _
            ByVal strSPR As String, _
            ByRef blnNeed As Boolean, _
            ByRef strXyrList As String) As Boolean

        '----------------------------------------------------------------
        ' ���롰�Ƿ���׼����־
        ' ����
        '                          ���������ַ���
        '----------------------------------------------------------------
        Public MustOverride Function doTranslateSFPZ(ByVal strSFPZ As String) As String

        '----------------------------------------------------------------
        ' ǩ��ȷ��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strYjlx              ��Ҫȷ�ϵ��������
        '     strSPR               ��������
        '     intMode              ��ǩ��ģʽ��0-����ǩ��1-��ͬǩ
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public MustOverride Function doQianminQueren( _
            ByRef strErrMsg As String, _
            ByVal strYjlx As String, _
            ByVal strSPR As String, _
            ByVal intMode As Integer) As Boolean

        '----------------------------------------------------------------
        ' ȡ��ǩ��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strYjlx              ��Ҫȡ�����������
        '     strSPR               ��������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public MustOverride Function doQianminCancel( _
            ByRef strErrMsg As String, _
            ByVal strYjlx As String, _
            ByVal strSPR As String) As Boolean

        '----------------------------------------------------------------
        ' ��ȡ�������ܽ��е�ǩ������б�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objYjlx              ��ǩ���������+��ʾ���Ƽ���
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public MustOverride Function getAllYjlx( _
            ByRef strErrMsg As String, _
            ByRef objYjlx As System.Collections.Specialized.NameValueCollection) As Boolean

        '----------------------------------------------------------------
        ' ����������ʽ�ļ���ҵ��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strFileSpec          ��Ҫ������ļ�·��(WEB������������ȫ·��)
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public MustOverride Function doImportZS( _
            ByRef strErrMsg As String, _
            ByVal strFileSpec As String) As Boolean

        '----------------------------------------------------------------
        ' ��ȡ����ʽ�ļ����ֶ�ֵ
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strZSWJ              ��(����)��ʽ�ļ��ֶ�ֵ
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public MustOverride Function getZSWJ( _
            ByRef strErrMsg As String, _
            ByRef strZSWJ As String) As Boolean

        '----------------------------------------------------------------
        ' ��������ǩ������ҵ��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strFileSpec          ��Ҫ������ļ�·��(WEB������������ȫ·��)
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public MustOverride Function doImportQP( _
            ByRef strErrMsg As String, _
            ByVal strFileSpec As String) As Boolean

        '----------------------------------------------------------------
        ' ��ȡ������ԭ�����ֶ�ֵ
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strPJYJ              ��(����)����ԭ���ֶ�ֵ
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public MustOverride Function getPJYJ( _
            ByRef strErrMsg As String, _
            ByRef strPJYJ As String) As Boolean

        '----------------------------------------------------------------
        ' �����ļ���ᡱҵ��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ���û�����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public MustOverride Function doCompleteFile( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String) As Boolean

        '----------------------------------------------------------------
        ' ���������ļ���ҵ��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ���û�����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public MustOverride Function doIQiyongFile( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String) As Boolean

        '----------------------------------------------------------------
        ' ���������ļ���ҵ��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ���û�����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public MustOverride Function doIZuofeiFile( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String) As Boolean

        '----------------------------------------------------------------
        ' ������������ҵ��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ���û�����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public MustOverride Function doIContinueFile( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String) As Boolean

        '----------------------------------------------------------------
        ' �����ݻ�����ҵ��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ���û�����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public MustOverride Function doIStopFile( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String) As Boolean

        '----------------------------------------------------------------
        ' �����������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strOldBlsy           ������ǰ�İ�������
        '     strNewBlsy           �������İ�������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public MustOverride Function doTranslateTask( _
            ByRef strErrMsg As String, _
            ByVal strOldBlsy As String, _
            ByRef strNewBlsy As String) As Boolean

        '----------------------------------------------------------------
        ' ���湤�������������������ļ���¼(�����������)
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strGJFile              ��Ҫ����ĸ���ļ��ı��ػ����ļ�����·��
        '     objDataSet_FJ          ��Ҫ����ĸ�������
        '     objDataSet_XGWJ        ��Ҫ���������ļ�����
        '     strUserXM              ����ǰ������Ա
        '     blnEnforeEdit          ��ǿ�Ʊ༭�ļ�����
        '     objFTPProperty         ��FTP���Ӳ���
        '     objParams              ������Ҫ�������ύ������
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public MustOverride Function doSaveFileTransactionZDBCVariantParam( _
            ByRef strErrMsg As String, _
            ByVal strGJFile As String, _
            ByVal objDataSet_FJ As Xydc.Platform.Common.Data.FlowData, _
            ByVal objDataSet_XGWJ As Xydc.Platform.Common.Data.FlowData, _
            ByVal strUserXM As String, _
            ByVal blnEnforeEdit As Boolean, _
            ByVal objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty, _
            ByVal objParams As System.Collections.Specialized.ListDictionary) As Boolean

        '----------------------------------------------------------------
        ' ���湤�������������������ļ���¼(�����������)
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strGJFile              ��Ҫ����ĸ���ļ��ı��ػ����ļ�����·��
        '     objDataSet_FJ          ��Ҫ����ĸ�������
        '     objDataSet_XGWJ        ��Ҫ���������ļ�����
        '     strUserXM              ����ǰ������Ա
        '     blnEnforeEdit          ��ǿ�Ʊ༭�ļ�����
        '     objFTPProperty         ��FTP���Ӳ���
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public MustOverride Function doSaveFileTransactionZDBC( _
            ByRef strErrMsg As String, _
            ByVal strGJFile As String, _
            ByVal objDataSet_FJ As Xydc.Platform.Common.Data.FlowData, _
            ByVal objDataSet_XGWJ As Xydc.Platform.Common.Data.FlowData, _
            ByVal strUserXM As String, _
            ByVal blnEnforeEdit As Boolean, _
            ByVal objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty) As Boolean

        '----------------------------------------------------------------
        ' ���湤������¼(�����������)
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     objNewData             ����¼��ֵ(���ر�������ֵ)
        '     objOldData             ����¼��ֵ
        '     objenumEditType        ���༭����
        '     strGJFile              ��Ҫ����ĸ���ļ��ı��ػ����ļ�����·��
        '     objDataSet_FJ          ��Ҫ����ĸ�������
        '     objDataSet_XGWJ        ��Ҫ���������ļ�����
        '     strUserXM              ����ǰ������Ա
        '     blnEnforeEdit          ��ǿ�Ʊ༭�ļ�����
        '     objFTPProperty         ��FTP���Ӳ���
        '     objParams              ������Ҫ�������ύ������
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public MustOverride Function doSaveFileTransactionVariantParam( _
            ByRef strErrMsg As String, _
            ByRef objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objOldData As Xydc.Platform.Common.Workflow.BaseFlowObject, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType, _
            ByVal strGJFile As String, _
            ByVal objDataSet_FJ As Xydc.Platform.Common.Data.FlowData, _
            ByVal objDataSet_XGWJ As Xydc.Platform.Common.Data.FlowData, _
            ByVal strUserXM As String, _
            ByVal blnEnforeEdit As Boolean, _
            ByVal objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty, _
            ByVal objParams As System.Collections.Specialized.ListDictionary) As Boolean

        '----------------------------------------------------------------
        ' ���湤������¼(�����������)
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     objNewData             ����¼��ֵ(���ر�������ֵ)
        '     objOldData             ����¼��ֵ
        '     objenumEditType        ���༭����
        '     strGJFile              ��Ҫ����ĸ���ļ��ı��ػ����ļ�����·��
        '     objDataSet_FJ          ��Ҫ����ĸ�������
        '     objDataSet_XGWJ        ��Ҫ���������ļ�����
        '     strUserXM              ����ǰ������Ա
        '     blnEnforeEdit          ��ǿ�Ʊ༭�ļ�����
        '     objFTPProperty         ��FTP���Ӳ���
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public MustOverride Function doSaveFileTransaction( _
            ByRef strErrMsg As String, _
            ByRef objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objOldData As Xydc.Platform.Common.Workflow.BaseFlowObject, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType, _
            ByVal strGJFile As String, _
            ByVal objDataSet_FJ As Xydc.Platform.Common.Data.FlowData, _
            ByVal objDataSet_XGWJ As Xydc.Platform.Common.Data.FlowData, _
            ByVal strUserXM As String, _
            ByVal blnEnforeEdit As Boolean, _
            ByVal objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty) As Boolean

        '----------------------------------------------------------------
        ' �������ļ�
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     intWJND                ��Ҫ���浽�����
        '     objSqlTransaction      ����������
        '     objConnectionProperty  ��FTP���Ӳ���
        '     strGJFile              ��Ҫ����ĸ���ļ��ı��ػ����ļ�����·��
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public MustOverride Function doSaveGJFile( _
            ByRef strErrMsg As String, _
            ByVal intWJND As Integer, _
            ByVal objSqlTransaction As System.Data.SqlClient.SqlTransaction, _
            ByVal objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty, _
            ByVal strGJFile As String) As Boolean

        '----------------------------------------------------------------
        ' �����¼
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     objSqlTransaction      ����������
        '     objNewData             ����¼��ֵ(���ر�������ֵ)
        '     objOldData             ����¼��ֵ
        '     objenumEditType        ���༭����
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public MustOverride Function doSaveFile( _
            ByRef strErrMsg As String, _
            ByVal objSqlTransaction As System.Data.SqlClient.SqlTransaction, _
            ByRef objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objOldData As Xydc.Platform.Common.Workflow.BaseFlowObject, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

        '----------------------------------------------------------------
        ' �жϼ�¼�����Ƿ���Ч��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objNewData           ����¼��ֵ(�����Ƽ�ֵ)
        '     objOldData           ����¼��ֵ
        '     objenumEditType      ���༭����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public MustOverride Function doVerifyFile( _
            ByRef strErrMsg As String, _
            ByRef objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objOldData As Xydc.Platform.Common.Workflow.BaseFlowObject, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

        '----------------------------------------------------------------
        ' ��ȡ����������ļ������Ļ���Ŀ¼
        '----------------------------------------------------------------
        Public MustOverride Function getBasePath_XGWJFJ() As String

        '----------------------------------------------------------------
        ' ��ȡ����������Ļ���Ŀ¼
        '----------------------------------------------------------------
        Public MustOverride Function getBasePath_GJ() As String

        '----------------------------------------------------------------
        ' ��ȡ�����������Ļ���Ŀ¼
        '----------------------------------------------------------------
        Public MustOverride Function getBasePath_FJ() As String

        '----------------------------------------------------------------
        ' ��ȡ��������ʼ����������
        '----------------------------------------------------------------
        Public MustOverride Function getInitTask() As String

        '----------------------------------------------------------------
        ' ���ݡ��ļ���ʶ����ȡ��������������(����������ʵ��)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objDataSet           �����ض�����������ݼ�
        '     strTableName         ���������������ݼ��еı���
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public MustOverride Function getMainFlowData( _
            ByRef strErrMsg As String, _
            ByRef objDataSet As System.Data.DataSet, _
            ByRef strTableName As String) As Boolean

        '----------------------------------------------------------------
        ' �ж�strUserXM�Ƿ������д�а�İ�����?
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ����Ա����
        '     blnCanWrite          �����أ��Ƿ����?
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public MustOverride Function canWriteChengbanResult( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByRef blnCanWrite As Boolean) As Boolean

        '----------------------------------------------------------------
        ' �ж�strUserXM�Ƿ�а���ļ�?
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ���û�����
        '     strXBBZ              ������а�����򷵻�Э���־
        '     blnHasChengban       �������Ƿ�а���ļ�?
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public MustOverride Function isRenyuanHasChengban( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByRef strXBBZ As String, _
            ByRef blnHasChengban As Boolean) As Boolean

        '----------------------------------------------------------------
        ' �ж�strUserXM�Ƿ���Լ�ӡ�ļ�?
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ����Ա����
        '     blnCanJiayin         �����أ��Ƿ����?
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public MustOverride Function canJiayinFile( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByRef blnCanJiayin As Boolean) As Boolean

        '----------------------------------------------------------------
        ' �ж�strUserXM�Ƿ�Ǽǰ�����?
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ����Ա����
        '     blnCan               �����أ��Ƿ����?
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public MustOverride Function canDengjiBLJG( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByRef blnCan As Boolean) As Boolean

        '----------------------------------------------------------------
        ' �ж��ļ��Ƿ�������?
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     blnComplete          �������Ƿ�������?
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public MustOverride Function isFileComplete( _
            ByRef strErrMsg As String, _
            ByRef blnComplete As Boolean) As Boolean

        '----------------------------------------------------------------
        ' �ж��ļ��Ƿ��Ѿ�����?
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     blnDinggao           �������Ƿ��Ѿ�����?
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public MustOverride Function isFileDinggao( _
            ByRef strErrMsg As String, _
            ByRef blnDinggao As Boolean) As Boolean

        '----------------------------------------------------------------
        ' �ж��ļ��Ƿ��Ѿ�����?
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     blnZuofei            �������Ƿ��Ѿ�����?
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public MustOverride Function isFileZuofei( _
            ByRef strErrMsg As String, _
            ByRef blnZuofei As Boolean) As Boolean

        '----------------------------------------------------------------
        ' �ж��ļ��Ƿ��Ѿ�ͣ��?
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     blnTingban           �������Ƿ��Ѿ�ͣ��?
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public MustOverride Function isFileTingban( _
            ByRef strErrMsg As String, _
            ByRef blnTingban As Boolean) As Boolean

        '----------------------------------------------------------------
        ' �ж�strUserXM�Ƿ����ļ���ԭʼ����?
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ����Ա����
        '     blnIs                �������Ƿ�?
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public MustOverride Function isOriginalPeople( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByRef blnIs As Boolean) As Boolean

        '----------------------------------------------------------------
        ' �ж�strBLSY�Ƿ��Ѿ���׼?
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strBLSY              ����������
        '     blnApproved          �������Ƿ�?
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public MustOverride Function isTaskApproved( _
            ByRef strErrMsg As String, _
            ByVal strBLSY As String, _
            ByRef blnApproved As Boolean) As Boolean

        '----------------------------------------------------------------
        ' ����strBLSY�ļ���
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strBLSY              ����������
        '     intLevel             �����ؼ���
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public MustOverride Function getTaskLevel( _
            ByRef strErrMsg As String, _
            ByVal strBLSY As String, _
            ByRef intLevel As Integer) As Boolean

        '----------------------------------------------------------------
        ' �ж�strBLSY�Ƿ�Ϊ�������ˣ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strBLSY              ����������
        '     intLevel             �����˼���
        '     blnIsShenpi          �����أ��Ƿ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public MustOverride Function isShenpiTask( _
            ByRef strErrMsg As String, _
            ByVal strBLSY As String, _
            ByVal intLevel As Integer, _
            ByRef blnIsShenpi As Boolean) As Boolean

        '----------------------------------------------------------------
        ' �ж�strBLSY�Ƿ�Ϊ�������ˣ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strBLSY              ����������
        '     blnIsShenpi          �����أ��Ƿ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public MustOverride Function isShenpiTask( _
            ByRef strErrMsg As String, _
            ByVal strBLSY As String, _
            ByRef blnIsShenpi As Boolean) As Boolean

        '----------------------------------------------------------------
        ' ��ȡstrUserXM���Ķ����������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ���û�����
        '     strWhere             ����������
        '     objOpinionData       �����أ��������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public MustOverride Function getCanReadOpinion( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByVal strWhere As String, _
            ByRef objOpinionData As Xydc.Platform.Common.Data.FlowData) As Boolean

        '----------------------------------------------------------------
        ' ɾ���ļ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objFTPProperty       ��FTP���������Ӳ���
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public MustOverride Function doDeleteFile( _
            ByRef strErrMsg As String, _
            ByVal objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty) As Boolean

        '----------------------------------------------------------------
        ' ���ݡ��ļ���ʶ����乤������������(����������ʵ��)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public MustOverride Function doFillFlowData( _
            ByRef strErrMsg As String) As Boolean

        '----------------------------------------------------------------
        ' ��ȡ�µ��ļ���ˮ��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strLSH               �������ļ���ˮ��
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public MustOverride Function getNewLSH( _
            ByRef strErrMsg As String, _
            ByRef strLSH As String) As Boolean









        '----------------------------------------------------------------
        ' Flow�����ʼ��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strWJBS              ���ļ���ʶ
        '     objSqlConnection     �����ݿ�����
        '     blnFillData          ���Ƿ��������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doInitialize( _
            ByRef strErrMsg As String, _
            ByVal strWJBS As String, _
            ByVal objSqlConnection As System.Data.SqlClient.SqlConnection, _
            ByVal blnFillData As Boolean) As Boolean

            doInitialize = False
            strErrMsg = ""

            Try
                '���
                If strWJBS Is Nothing Then strWJBS = ""
                strWJBS = strWJBS.Trim()
                If objSqlConnection Is Nothing Then
                    strErrMsg = "����δ��ʼ��[���ݿ�����]��"
                    GoTo errProc
                End If

                '������
                Select Case objSqlConnection.State
                    Case System.Data.ConnectionState.Closed
                        m_objSqlConnection.ConnectionString = objSqlConnection.ConnectionString
                        m_objSqlConnection.Open()
                    Case Else
                End Select

                '����flow�����ʼֵ
                Me.FlowData.WJBS = strWJBS

                '��������������
                If blnFillData = True Then
                    If Me.doFillFlowData(strErrMsg) = False Then
                        Exit Try
                    End If
                    Me.m_blnFillData = True
                End If

                '��ʼ���ɹ�
                Me.m_blnInitialized = True

                doInitialize = True

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doInitialize = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' Flow�����ʼ��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strWJBS              ���ļ���ʶ
        '     blnFillData          ���Ƿ��������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doInitialize( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWJBS As String, _
            ByVal blnFillData As Boolean) As Boolean

            doInitialize = False
            strErrMsg = ""

            Try
                '���
                If strWJBS Is Nothing Then strWJBS = ""
                strWJBS = strWJBS.Trim()
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim()
                If strUserId = "" Then
                    strErrMsg = "����δָ�������û���"
                    GoTo errProc
                End If

                '������
                Dim strConnectString As String
                Select Case Me.m_objSqlConnection.State
                    Case System.Data.ConnectionState.Closed
                        '��ȡ���Ӵ�
                        With New Xydc.Platform.Common.jsoaConfiguration
                            strConnectString = .getConnectionString(strUserId, strPassword)
                        End With
                        '������
                        m_objSqlConnection.ConnectionString = strConnectString
                        m_objSqlConnection.Open()
                End Select

                '����flow�����ʼֵ
                Me.FlowData.WJBS = strWJBS

                '��������������
                If blnFillData = True Then
                    If Me.doFillFlowData(strErrMsg) = False Then
                        Exit Try
                    End If
                    Me.m_blnFillData = True
                End If

                '��ʼ���ɹ�
                Me.m_blnInitialized = True

                doInitialize = True

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doInitialize = True
            Exit Function

errProc:
            Exit Function

        End Function










        '----------------------------------------------------------------
        ' �ж�ָ����Ա�Ƿ���Ķ��ļ���
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ����Ա����
        '     blnCanRead           �����أ��Ƿ���ԣ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function canReadFile( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByRef blnCanRead As Boolean) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            canReadFile = False
            strErrMsg = ""
            blnCanRead = False

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '
                '����ǹ���Ա������Կ�
                If strUserXM = "����Ա" Then
                    blnCanRead = True
                Else
                    '��ȡ�����˿��Կ� �� �����˿��Կ� �Ľ�������
                    strSQL = ""
                    strSQL = strSQL + " select * from ����_B_���� " + vbCr
                    strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "' " + vbCr
                    strSQL = strSQL + " and ((������   = '" + strUserXM + "' and rtrim(���ӱ�ʶ) like '_1%' ) " + vbCr
                    strSQL = strSQL + " or   (������   = '" + strUserXM + "' and rtrim(���ӱ�ʶ) like '__1%')) " + vbCr
                    If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                        GoTo errProc
                    End If
                    If objDataSet.Tables(0).Rows.Count > 0 Then
                        blnCanRead = True
                    End If
                End If
                '

                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            canReadFile = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �ж�ָ����Ա�Ƿ���Խ����ļ�����?
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ����Ա����
        '     blnCanRead           �����أ��Ƿ����?
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function canBuyueFile( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByRef blnCanBuyue As Boolean) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            canBuyueFile = False
            strErrMsg = ""
            blnCanBuyue = False

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '���Ӱ������״̬SQL�б�
                Dim strTaskStatusYWCList As String = Me.FlowData.TaskStatusYWCList

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strBYQQ As String = Me.FlowData.TASK_BYQQ
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                strSQL = ""
                '�յ���������û�а���Ľ���
                strSQL = strSQL + " select * from ����_B_���� "
                strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "' "
                strSQL = strSQL + " and   �������� = '" + strBYQQ + "' "
                strSQL = strSQL + " and   ����״̬ not in (" + strTaskStatusYWCList + ") "
                strSQL = strSQL + " and  (������ = '" + strUserXM + "' and rtrim(���ӱ�ʶ) like '__1%') "
                strSQL = strSQL + " union "
                '��������Ľ���
                strSQL = strSQL + " select * from ����_B_���� "
                strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "' "
                strSQL = strSQL + " and  (������ = '" + strUserXM + "' and rtrim(���ӱ�ʶ) like '__1__0%') "
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    blnCanBuyue = True
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            canBuyueFile = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �ж�ָ����ԱstrUserId�Ƿ�ɶ����ļ���
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ����Ա����
        '     strBMDM              ��strUserId������λ����
        '     blnCanDuban          �����أ��Ƿ���ԣ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function canDubanFile( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strBMDM As String, _
            ByRef blnCanDuban As Boolean) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            canDubanFile = False
            strErrMsg = ""
            blnCanDuban = False

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim()
                If strBMDM Is Nothing Then strBMDM = ""
                strBMDM = strBMDM.Trim

                '���Ӱ������״̬SQL�б�
                Dim strTaskStatusYWCList As String = Me.FlowData.TaskStatusYWCList

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '���Զ���ȫ��λ?
                Dim intDBFW As Integer
                intDBFW = CType(Xydc.Platform.Common.Data.DubanshezhiData.enumDubanfanweiType.All, Integer)
                strSQL = ""
                strSQL = strSQL + " select a.* " + vbCr
                strSQL = strSQL + " from ����_B_�������� a " + vbCr
                strSQL = strSQL + " left join (" + vbCr
                strSQL = strSQL + "   select * from ����_B_�ϸ� " + vbCr
                strSQL = strSQL + "   where ��Ա����  = '" + strUserId + "' " + vbCr
                strSQL = strSQL + " ) b on a.��λ���� = b.��λ���� " + vbCr
                strSQL = strSQL + " where b.��λ���� is not null " + vbCr
                strSQL = strSQL + " and   a.���췶Χ = " + intDBFW.ToString() + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    blnCanDuban = True
                    Exit Try
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing
                If strWJBS = "" Then Exit Try

                '���Զ���ָ���������µ�λ
                Dim blnDefined As Boolean
                Dim intMinJSXZ As Integer
                Dim intMinJSDM As Integer
                '��ȡָ����Ա���Զ������С��֯���뼶������
                intDBFW = CType(Xydc.Platform.Common.Data.DubanshezhiData.enumDubanfanweiType.Level, Integer)
                strSQL = ""
                strSQL = strSQL + " select min(a.��������) " + vbCr
                strSQL = strSQL + " from ����_B_�������� a " + vbCr
                strSQL = strSQL + " left join ( " + vbCr
                strSQL = strSQL + "   select * from ����_B_�ϸ� " + vbCr
                strSQL = strSQL + "   where ��Ա����  = '" + strUserId + "' " + vbCr
                strSQL = strSQL + " ) b on a.��λ���� = b.��λ���� " + vbCr
                strSQL = strSQL + " where b.��λ���� Is Not Null " + vbCr
                strSQL = strSQL + " and   a.���췶Χ = " + intDBFW.ToString() + vbCr
                strSQL = strSQL + " and   a.�������� is not null " + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                intMinJSXZ = Xydc.Platform.Common.Data.CustomerData.intZZDM_FJCDSM.Length + 1           '��֯������ͼ�+1
                intMinJSXZ = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item(0), intMinJSXZ)
                If intMinJSXZ > Xydc.Platform.Common.Data.CustomerData.intZZDM_FJCDSM.Length Then
                    blnDefined = False
                ElseIf intMinJSXZ < 1 Then
                    blnDefined = False
                Else
                    intMinJSDM = Xydc.Platform.Common.Data.CustomerData.intZZDM_FJCDSM(intMinJSXZ - 1)  '��֯���볤��
                    blnDefined = True
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing
                If blnDefined = True Then
                    '��ȡ��ǰ�ļ��Ľ�����δ������ɡ�ָ��intMinJSXZ���¼�����
                    strSQL = ""
                    strSQL = strSQL + " select a.* from " + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select * from ����_B_���� " + vbCr
                    strSQL = strSQL + "   where �ļ���ʶ = '" + strWJBS + "' " + vbCr                               '��ǰ�ļ�
                    strSQL = strSQL + "   and   rtrim(���ӱ�ʶ) like '1_1__0%' "                                           '�ѷ���+�������ܿ�+��֪ͨ
                    strSQL = strSQL + "   and   ����״̬ not in (" + strTaskStatusYWCList + ") " + vbCr             'δ����
                    strSQL = strSQL + " ) a " + vbCr
                    strSQL = strSQL + " left join ����_B_��Ա b on a.������ = b.��Ա���� " + vbCr
                    strSQL = strSQL + " where b.��Ա���� is not null " + vbCr
                    strSQL = strSQL + " and   b.��֯���� is not null " + vbCr
                    strSQL = strSQL + " and   len(rtrim(b.��֯����)) >= " + intMinJSDM.ToString() + vbCr            'ָ���������µ�λ
                    If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                        GoTo errProc
                    End If
                    If objDataSet.Tables(0).Rows.Count > 0 Then
                        blnCanDuban = True
                        Exit Try
                    End If
                    Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                    objDataSet = Nothing
                End If

                '���Զ��챾����ָ���������µ�λ
                '��ȡָ����Ա���Զ������С��������
                intDBFW = CType(Xydc.Platform.Common.Data.DubanshezhiData.enumDubanfanweiType.BumenLevel, Integer)
                strSQL = ""
                strSQL = strSQL + " select min(a.��������) from ����_B_�������� a " + vbCr
                strSQL = strSQL + " left join ( " + vbCr
                strSQL = strSQL + "   select * from ����_B_�ϸ� " + vbCr
                strSQL = strSQL + "   where ��Ա���� = '" + strUserId + "' " + vbCr
                strSQL = strSQL + " ) b on a.��λ���� = b.��λ���� " + vbCr
                strSQL = strSQL + " where b.��λ���� Is Not Null " + vbCr
                strSQL = strSQL + " and a.���췶Χ = " + intDBFW.ToString() + vbCr
                strSQL = strSQL + " and a.�������� is not null " + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                intMinJSXZ = Xydc.Platform.Common.Data.CustomerData.intZZDM_FJCDSM.Length + 1           '��֯������ͼ�+1
                intMinJSXZ = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item(0), intMinJSXZ)
                If intMinJSXZ > Xydc.Platform.Common.Data.CustomerData.intZZDM_FJCDSM.Length Then
                    blnDefined = False
                ElseIf intMinJSXZ < 1 Then
                    blnDefined = False
                Else
                    intMinJSDM = Xydc.Platform.Common.Data.CustomerData.intZZDM_FJCDSM(intMinJSXZ - 1)  '��֯���볤��
                    blnDefined = True
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing
                If blnDefined = True Then
                    '��ȡ��ǰ�ļ��Ľ�����δ������ɡ����������ڲ�����ָ����Ա��ָ��intMinJSXZ���¼�����
                    strSQL = ""
                    strSQL = strSQL + " select a.* from " + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select * from ����_B_���� " + vbCr
                    strSQL = strSQL + "   where �ļ���ʶ = '" + strWJBS + "' " + vbCr                             '��ǰ�ļ�
                    strSQL = strSQL + "   and   rtrim(���ӱ�ʶ) like '1_1__0%' " + vbCr                                  '�ѷ���+�������ܿ�+��֪ͨ��
                    strSQL = strSQL + "   and   ����״̬ not in (" + strTaskStatusYWCList + ") " + vbCr           'δ����
                    strSQL = strSQL + " ) a " + vbCr
                    strSQL = strSQL + " left join ����_B_��Ա b on a.������ = b.��Ա���� " + vbCr
                    strSQL = strSQL + " where b.��Ա���� is not null " + vbCr
                    strSQL = strSQL + " and b.��֯���� is not null " + vbCr
                    strSQL = strSQL + " and rtrim(b.��֯����) like '" + strBMDM + "' + '%' " + vbCr               '�������¼�����
                    strSQL = strSQL + " and len(rtrim(b.��֯����)) >= " + intMinJSDM.ToString() + vbCr            'ָ���������µ�λ
                    If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                        GoTo errProc
                    End If
                    If objDataSet.Tables(0).Rows.Count > 0 Then
                        blnCanDuban = True
                        Exit Try
                    End If
                    Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                    objDataSet = Nothing
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            canDubanFile = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �ж�ָ����ԱstrUserId�Ƿ�ɶ�strJSR���ж����ļ���
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ��׼�������ļ�����Ա��ʶ
        '     strBMDM              ��strUserId������λ����
        '     strJSR               ������������
        '     blnCanDuban          �����أ��Ƿ���ԣ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function canDubanFile( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strBMDM As String, _
            ByVal strJSR As String, _
            ByRef blnCanDuban As Boolean) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            canDubanFile = False
            strErrMsg = ""
            blnCanDuban = False

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim()
                If strBMDM Is Nothing Then strBMDM = ""
                strBMDM = strBMDM.Trim
                If strJSR Is Nothing Then strJSR = ""
                strJSR = strJSR.Trim()

                '���Ӱ������״̬SQL�б�
                Dim strTaskStatusYWCList As String = Me.FlowData.TaskStatusYWCList

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '���Զ���ȫ��λ?
                Dim intDBFW As Integer
                intDBFW = CType(Xydc.Platform.Common.Data.DubanshezhiData.enumDubanfanweiType.All, Integer)
                strSQL = ""
                strSQL = strSQL + " select a.* " + vbCr
                strSQL = strSQL + " from ����_B_�������� a " + vbCr
                strSQL = strSQL + " left join (" + vbCr
                strSQL = strSQL + "   select * from ����_B_�ϸ� " + vbCr
                strSQL = strSQL + "   where ��Ա����  = '" + strUserId + "' " + vbCr
                strSQL = strSQL + " ) b on a.��λ���� = b.��λ���� " + vbCr
                strSQL = strSQL + " where b.��λ���� is not null " + vbCr
                strSQL = strSQL + " and   a.���췶Χ = " + intDBFW.ToString() + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    blnCanDuban = True
                    Exit Try
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing
                If strWJBS = "" Then Exit Try

                '���Զ���ָ���������µ�λ
                Dim blnDefined As Boolean
                Dim intMinJSXZ As Integer
                Dim intMinJSDM As Integer
                '��ȡָ����Ա���Զ������С��������
                intDBFW = CType(Xydc.Platform.Common.Data.DubanshezhiData.enumDubanfanweiType.Level, Integer)
                strSQL = ""
                strSQL = strSQL + " select min(a.��������) " + vbCr
                strSQL = strSQL + " from ����_B_�������� a " + vbCr
                strSQL = strSQL + " left join ( " + vbCr
                strSQL = strSQL + "   select * from ����_B_�ϸ� " + vbCr
                strSQL = strSQL + "   where ��Ա����  = '" + strUserId + "' " + vbCr
                strSQL = strSQL + " ) b on a.��λ���� = b.��λ���� " + vbCr
                strSQL = strSQL + " where b.��λ���� Is Not Null " + vbCr
                strSQL = strSQL + " and   a.���췶Χ = " + intDBFW.ToString() + vbCr
                strSQL = strSQL + " and   a.�������� is not null " + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                intMinJSXZ = Xydc.Platform.Common.Data.CustomerData.intZZDM_FJCDSM.Length + 1           '��֯������ͼ�+1
                intMinJSXZ = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item(0), intMinJSXZ)
                If intMinJSXZ > Xydc.Platform.Common.Data.CustomerData.intZZDM_FJCDSM.Length Then
                    blnDefined = False
                ElseIf intMinJSXZ < 1 Then
                    blnDefined = False
                Else
                    intMinJSDM = Xydc.Platform.Common.Data.CustomerData.intZZDM_FJCDSM(intMinJSXZ - 1)  '��֯���볤��
                    blnDefined = True
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing
                If blnDefined = True Then
                    '��ȡ��ǰ�ļ��Ľ�����strJSRδ������ɡ�ָ��intMinJSXZ���¼�����
                    strSQL = ""
                    strSQL = strSQL + " select a.* from " + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select * from ����_B_���� " + vbCr
                    strSQL = strSQL + "   where �ļ���ʶ = '" + strWJBS + "' " + vbCr                           '��ǰ�ļ�
                    strSQL = strSQL + "   and   rtrim(���ӱ�ʶ) like '1_1__0%' " + vbCr                                '�ѷ���+�������ܿ�+��֪ͨ
                    strSQL = strSQL + "   and   ����״̬ not in (" + strTaskStatusYWCList + ") " + vbCr         'δ����
                    strSQL = strSQL + "   and   ������   = '" + strJSR + "' " + vbCr                            'ָ��������
                    strSQL = strSQL + " ) a " + vbCr
                    strSQL = strSQL + " left join ����_B_��Ա b on a.������ = b.��Ա���� " + vbCr
                    strSQL = strSQL + " where b.��Ա���� is not null " + vbCr
                    strSQL = strSQL + " and   b.��֯���� is not null " + vbCr
                    strSQL = strSQL + " and   len(rtrim(b.��֯����)) >= " + intMinJSDM.ToString() + vbCr        'ָ���������µ�λ
                    If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                        GoTo errProc
                    End If
                    If objDataSet.Tables(0).Rows.Count > 0 Then
                        blnCanDuban = True
                        Exit Try
                    End If
                    Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                    objDataSet = Nothing
                End If

                '���Զ��챾����ָ���������µ�λ
                '��ȡָ����Ա���Զ������С��������
                intDBFW = CType(Xydc.Platform.Common.Data.DubanshezhiData.enumDubanfanweiType.BumenLevel, Integer)
                strSQL = ""
                strSQL = strSQL + " select min(a.��������) " + vbCr
                strSQL = strSQL + " from ����_B_�������� a " + vbCr
                strSQL = strSQL + " left join ( " + vbCr
                strSQL = strSQL + "   select * from ����_B_�ϸ� " + vbCr
                strSQL = strSQL + "   where ��Ա���� = '" + strUserId + "' " + vbCr
                strSQL = strSQL + " ) b on a.��λ���� = b.��λ���� " + vbCr
                strSQL = strSQL + " where b.��λ���� Is Not Null " + vbCr
                strSQL = strSQL + " and a.���췶Χ = " + intDBFW.ToString() + vbCr
                strSQL = strSQL + " and a.�������� is not null " + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                intMinJSXZ = Xydc.Platform.Common.Data.CustomerData.intZZDM_FJCDSM.Length + 1           '��֯������ͼ�+1
                intMinJSXZ = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item(0), intMinJSXZ)
                If intMinJSXZ > Xydc.Platform.Common.Data.CustomerData.intZZDM_FJCDSM.Length Then
                    blnDefined = False
                ElseIf intMinJSXZ < 1 Then
                    blnDefined = False
                Else
                    intMinJSDM = Xydc.Platform.Common.Data.CustomerData.intZZDM_FJCDSM(intMinJSXZ - 1)  '��֯���볤��
                    blnDefined = True
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing
                If blnDefined = True Then
                    '��ȡ��ǰ�ļ��Ľ�����strJSRδ������ɡ����������ڲ�����ָ����Ա��ָ��intMinJSXZ���¼�����
                    strSQL = ""
                    strSQL = strSQL + " select a.* from " + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select * from ����_B_���� " + vbCr
                    strSQL = strSQL + "   where �ļ���ʶ = '" + strWJBS + "' " + vbCr                           '��ǰ�ļ�
                    strSQL = strSQL + "   and   rtrim(���ӱ�ʶ) like '1_1__0%' " + vbCr                                '�ѷ���+�������ܿ�+��֪ͨ
                    strSQL = strSQL + "   and   ����״̬ not in (" + strTaskStatusYWCList + ") " + vbCr         'δ����
                    strSQL = strSQL + "   and   ������   = '" + strJSR + "' " + vbCr                            'ָ��������
                    strSQL = strSQL + " ) a " + vbCr
                    strSQL = strSQL + " left join ����_B_��Ա b on a.������ = b.��Ա���� " + vbCr
                    strSQL = strSQL + " where b.��Ա���� is not null " + vbCr
                    strSQL = strSQL + " and b.��֯���� is not null " + vbCr
                    strSQL = strSQL + " and rtrim(b.��֯����) like '" + strBMDM + "' + '%' " + vbCr             '�������¼�����
                    strSQL = strSQL + " and len(rtrim(b.��֯����)) >= " + intMinJSDM.ToString() + vbCr          'ָ���������µ�λ
                    If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                        GoTo errProc
                    End If
                    If objDataSet.Tables(0).Rows.Count > 0 Then
                        blnCanDuban = True
                        Exit Try
                    End If
                    Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                    objDataSet = Nothing
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            canDubanFile = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �ж�ָ����ԱstrUserXM�Ƿ����д��������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ����ǰ������Ա����
        '     blnCanWrite          �����أ��Ƿ���ԣ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function canWriteDubanResult( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByRef blnCanWrite As Boolean) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            canWriteDubanResult = False
            strErrMsg = ""
            blnCanWrite = False

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '��ȡstrUserXM�������Ҵ��ڽ��Ӽ�¼�Ķ�������
                strSQL = ""
                strSQL = strSQL + " select a.* from " + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select * from ����_B_���� " + vbCr
                strSQL = strSQL + "   where �ļ���ʶ = '" + strWJBS + "' " + vbCr
                strSQL = strSQL + "   and   �������� = '" + strUserXM + "' " + vbCr
                strSQL = strSQL + " ) a " + vbCr
                strSQL = strSQL + " left join " + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select * from ����_B_���� " + vbCr
                strSQL = strSQL + "   where �ļ���ʶ = '" + strWJBS + "' " + vbCr                           '��ǰ�ļ�
                strSQL = strSQL + "   and   ������   = '" + strUserXM + "' " + vbCr                         '������
                strSQL = strSQL + "   and   rtrim(���ӱ�ʶ) like '1_1__0%' " + vbCr                                '�ѷ���+�������ܿ�+��֪ͨ
                strSQL = strSQL + " ) b on a.�ļ���ʶ = b.�ļ���ʶ and a.������� = b.������� " + vbCr
                strSQL = strSQL + " where b.�ļ���ʶ is not null " + vbCr                                   'һ������
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    blnCanWrite = True
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            canWriteDubanResult = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �ж�ָ����Ա�Ƿ�ɴ߰��ļ���
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ��׼���߰��ļ�����Ա����
        '     blnCanCuiban         �����أ��Ƿ���Դ߰죿
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function canCuibanFile( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByRef blnCanCuiban As Boolean) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            canCuibanFile = False
            strErrMsg = ""
            blnCanCuiban = False

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '���Ӱ������״̬SQL�б�
                Dim strTaskStatusYWCList As String = Me.FlowData.TaskStatusYWCList

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '��ȡ������=ָ����Ա�ҽ�����δ��ɵĽ��ӵ�
                strSQL = ""
                strSQL = strSQL + " select * from ����_B_���� " + vbCr
                strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "' " + vbCr                    '��ǰ�ļ�
                strSQL = strSQL + " and   ������   = '" + strUserXM + "' " + vbCr                  '������
                strSQL = strSQL + " and   rtrim(���ӱ�ʶ) like '1_1__0%' " + vbCr                         '�ѷ���+�������ܿ�+��֪ͨ
                strSQL = strSQL + " and   ����״̬ not in (" + strTaskStatusYWCList + ") " + vbCr  'δ����
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    blnCanCuiban = True
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            canCuibanFile = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �ж�ָ����Ա�Ƿ�ɲ����쵼�����
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ��׼�������쵼�������Ա����
        '     strBMDM              ��׼�������쵼�������Ա������λ����
        '     blnCanBudeng         �����أ��Ƿ���ԣ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function canBuDengFile( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strBMDM As String, _
            ByRef blnCanBudeng As Boolean) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            canBuDengFile = False
            strErrMsg = ""
            blnCanBudeng = False

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim()
                If strBMDM Is Nothing Then strBMDM = ""
                strBMDM = strBMDM.Trim()

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '����������
                Dim intBDFW As Integer
                intBDFW = CType(Xydc.Platform.Common.Data.BudengshezhiData.enumBudengfanweiType.All, Integer)
                strSQL = ""
                strSQL = strSQL + " select a.* " + vbCr
                strSQL = strSQL + " from ����_B_�������� a " + vbCr
                strSQL = strSQL + " left join (" + vbCr
                strSQL = strSQL + "   select * from ����_B_�ϸ� " + vbCr
                strSQL = strSQL + "   where ��Ա���� = '" + strUserId + "' " + vbCr
                strSQL = strSQL + " ) b on a.��λ���� = b.��λ���� " + vbCr
                strSQL = strSQL + " where b.��λ���� is not null " + vbCr
                strSQL = strSQL + " and   a.���Ƿ�Χ = " + intBDFW.ToString() + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    blnCanBudeng = True
                    Exit Try
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing
                If strWJBS = "" Then Exit Try

                '��ȡָ����Ա�Ĳ����������1
                Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate
                Dim strTemp As String = ""
                intBDFW = CType(Xydc.Platform.Common.Data.BudengshezhiData.enumBudengfanweiType.Zhiwu, Integer)
                strSQL = ""
                strSQL = strSQL + " select a.* " + vbCr
                strSQL = strSQL + " from ����_B_�������� a " + vbCr
                strSQL = strSQL + " left join (" + vbCr
                strSQL = strSQL + "   select * from ����_B_�ϸ� " + vbCr
                strSQL = strSQL + "   where ��Ա���� = '" + strUserId + "' " + vbCr
                strSQL = strSQL + " ) b on a.��λ���� = b.��λ���� " + vbCr
                strSQL = strSQL + " where b.��λ���� is not null " + vbCr
                strSQL = strSQL + " and a.���Ƿ�Χ = " + intBDFW.ToString() + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    '��ȡ����ָ��ְ���б�1
                    With objDataSet.Tables(0)
                        Dim intCount As Integer
                        Dim i As Integer
                        intCount = .Rows.Count
                        For i = 0 To intCount - 1 Step 1
                            If strTemp = "" Then
                                strTemp = objPulicParameters.getObjectValue(.Rows(i).Item("ְ���б�"), "").Trim()
                            Else
                                strTemp = strTemp + strSep + objPulicParameters.getObjectValue(.Rows(i).Item("ְ���б�"), "").Trim()
                            End If
                        Next
                    End With
                    Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                    objDataSet = Nothing
                    If strTemp <> "" Then
                        '���ָ���ļ����Ƿ���ָ�����ŵ�ָ��ְ��strTemp���˴���
                        strSQL = ""
                        strSQL = strSQL + " select * from ����_B_���� " + vbCr
                        strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "' " + vbCr
                        strSQL = strSQL + " and   rtrim(���ӱ�ʶ) like '1%' " + vbCr
                        strSQL = strSQL + " and   ������ in " + vbCr
                        strSQL = strSQL + " (" + vbCr
                        strSQL = strSQL + "   select b.��Ա���� from ����_B_�ϸ� a " + vbCr
                        strSQL = strSQL + "   left join ����_B_��Ա     b on a.��Ա���� = b.��Ա���� " + vbCr
                        strSQL = strSQL + "   left join ����_B_������λ c on a.��λ���� = c.��λ���� " + vbCr
                        strSQL = strSQL + "   where b.��Ա���� is not null " + vbCr
                        strSQL = strSQL + "   and   c.��λ���� is not null " + vbCr
                        strSQL = strSQL + "   and '" + strTemp + "' + '" + strSep + "' like '%'+rtrim(c.��λ����)+'" + strSep + "%' " + vbCr
                        strSQL = strSQL + "   group by b.��Ա����" + vbCr
                        strSQL = strSQL + ")"
                        If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                            GoTo errProc
                        End If
                        If objDataSet.Tables(0).Rows.Count > 0 Then
                            blnCanBudeng = True
                            Exit Try
                        End If
                        Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                        objDataSet = Nothing
                    End If
                End If

                '��ȡָ����Ա�Ĳ����������2
                intBDFW = CType(Xydc.Platform.Common.Data.BudengshezhiData.enumBudengfanweiType.ZhiwuBumenLevel, Integer)
                strSQL = ""
                strSQL = strSQL + " select a.* from ����_B_�������� a " + vbCr
                strSQL = strSQL + " left join (" + vbCr
                strSQL = strSQL + "   select * from ����_B_�ϸ� " + vbCr
                strSQL = strSQL + "   where  ��Ա���� = '" + strUserId + "' " + vbCr
                strSQL = strSQL + " ) b on a.��λ���� = b.��λ���� " + vbCr
                strSQL = strSQL + " where b.��λ���� is not null " + vbCr
                strSQL = strSQL + " and   a.���Ƿ�Χ = " + intBDFW.ToString() + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    blnCanBudeng = False
                    Exit Try
                End If
                '��ȡ����ָ��ְ���б�2
                With objDataSet.Tables(0)
                    Dim blnDefined As Boolean
                    Dim intMinJSDM As Integer
                    Dim intMinJSXZ As Integer
                    Dim intCount As Integer
                    Dim i As Integer
                    intcount = .Rows.Count
                    For i = 0 To intcount - 1 Step 1
                        strTemp = ""
                        strTemp = objPulicParameters.getObjectValue(.Rows(i).Item("ְ���б�"), "").Trim()
                        intMinJSXZ = Xydc.Platform.Common.Data.CustomerData.intZZDM_FJCDSM.Length + 1      '��֯������ͼ�+1
                        intMinJSXZ = objPulicParameters.getObjectValue(.Rows(i).Item("��������"), intMinJSXZ)
                        If intMinJSXZ > Xydc.Platform.Common.Data.CustomerData.intZZDM_FJCDSM.Length Then
                            blnDefined = False
                        ElseIf intMinJSXZ < 1 Then
                            blnDefined = False
                        Else
                            intMinJSDM = Xydc.Platform.Common.Data.CustomerData.intZZDM_FJCDSM(intMinJSXZ - 1)
                            blnDefined = True
                        End If
                        If strTemp <> "" And blnDefined = True Then
                            '���ָ���ļ����Ƿ���ָ�����ŵ�ָ��ְ��strTemp���˴���
                            strSQL = ""
                            strSQL = strSQL + " select * from ����_B_���� " + vbCr
                            strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "' " + vbCr                 '��ǰ�ļ�
                            strSQL = strSQL + " and   rtrim(���ӱ�ʶ) like '1_1__0%' " + vbCr                      '�ѷ���+�������ܿ�+��֪ͨ
                            strSQL = strSQL + " and   ������ in " + vbCr                                    '���ܽ�����
                            strSQL = strSQL + " (" + vbCr
                            strSQL = strSQL + "   select b.��Ա���� from ����_B_�ϸ� a " + vbCr
                            strSQL = strSQL + "   left join ����_B_��Ա     b on a.��Ա���� = b.��Ա���� " + vbCr
                            strSQL = strSQL + "   left join ����_B_������λ c on a.��λ���� = c.��λ���� " + vbCr
                            strSQL = strSQL + "   where b.��Ա���� is not null " + vbCr
                            strSQL = strSQL + "   and   b.��֯���� is not null " + vbCr
                            strSQL = strSQL + "   and   c.��λ���� is not null " + vbCr
                            strSQL = strSQL + "   and '" + strTemp + "' + '" + strSep + "' like '%'+rtrim(c.��λ����)+'" + strSep + "%' " + vbCr  'ָ��ְ��
                            strSQL = strSQL + "   and len(rtrim(b.��֯����)) >= " + intMinJSDM.ToString() + vbCr                                  'ָ����������
                            strSQL = strSQL + "   and rtrim(b.��֯����) like '" + strBMDM + "%' " + vbCr                                          '�������¼���λ
                            strSQL = strSQL + "   group by b.��Ա����" + vbCr
                            strSQL = strSQL + " )"
                            Dim objDataSetA As System.Data.DataSet
                            If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSetA) = False Then
                                GoTo errProc
                            End If
                            If objDataSetA.Tables(0).Rows.Count > 0 Then
                                blnCanBudeng = True
                                Exit Try
                            End If
                            objDataSetA.Dispose()
                            objDataSetA = Nothing
                        End If
                    Next
                End With
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            canBuDengFile = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �ж�ָ����Ա�Ƿ�ɲ���strJSRǩ��������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ��׼�������쵼�������Ա����
        '     strBMDM              ��׼�������쵼�������Ա������λ����
        '     strJSR               ���쵼����
        '     blnCanBudeng         �����أ��Ƿ���ԣ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function canBuDengFile( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strBMDM As String, _
            ByVal strJSR As String, _
            ByRef blnCanBudeng As Boolean) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            canBuDengFile = False
            strErrMsg = ""
            blnCanBudeng = False

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim()
                If strBMDM Is Nothing Then strBMDM = ""
                strBMDM = strBMDM.Trim()
                If strJSR Is Nothing Then strJSR = ""
                strJSR = strJSR.Trim()

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '����������
                Dim intBDFW As Integer
                intBDFW = CType(Xydc.Platform.Common.Data.BudengshezhiData.enumBudengfanweiType.All, Integer)
                strSQL = ""
                strSQL = strSQL + " select a.* from ����_B_�������� a " + vbCr
                strSQL = strSQL + " left join (" + vbCr
                strSQL = strSQL + "   select * from ����_B_�ϸ� " + vbCr
                strSQL = strSQL + "   where ��Ա���� = '" + strUserId + "' " + vbCr
                strSQL = strSQL + " ) b on a.��λ���� = b.��λ���� " + vbCr
                strSQL = strSQL + " where b.��λ���� is not null " + vbCr
                strSQL = strSQL + " and   a.���Ƿ�Χ = " + intBDFW.ToString() + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    blnCanBudeng = True
                    Exit Try
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing
                If strWJBS = "" Then Exit Try

                '��ȡָ����Ա�Ĳ����������1
                Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate
                Dim strTemp As String = ""
                intBDFW = CType(Xydc.Platform.Common.Data.BudengshezhiData.enumBudengfanweiType.Zhiwu, Integer)
                strSQL = ""
                strSQL = strSQL + " select a.* from ����_B_�������� a " + vbCr
                strSQL = strSQL + " left join (" + vbCr
                strSQL = strSQL + "   select * from ����_B_�ϸ� " + vbCr
                strSQL = strSQL + "   where ��Ա���� = '" + strUserId + "' " + vbCr
                strSQL = strSQL + " ) b on a.��λ���� = b.��λ���� " + vbCr
                strSQL = strSQL + " where b.��λ���� is not null " + vbCr
                strSQL = strSQL + " and a.���Ƿ�Χ = " + intBDFW.ToString() + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    '��ȡ����ָ��ְ���б�1
                    With objDataSet.Tables(0)
                        Dim intCount As Integer
                        Dim i As Integer
                        intCount = .Rows.Count
                        For i = 0 To intCount - 1 Step 1
                            If strTemp = "" Then
                                strTemp = objPulicParameters.getObjectValue(.Rows(i).Item("ְ���б�"), "").Trim()
                            Else
                                strTemp = strTemp + strSep + objPulicParameters.getObjectValue(.Rows(i).Item("ְ���б�"), "").Trim()
                            End If
                        Next
                    End With
                    Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                    objDataSet = Nothing
                    If strTemp <> "" Then
                        '���ָ���ļ����Ƿ���ָ�����ŵ�ָ��ְ��strTemp���˴���
                        strSQL = ""
                        strSQL = strSQL + " select * from ����_B_���� " + vbCr
                        strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "' " + vbCr
                        strSQL = strSQL + " and   rtrim(���ӱ�ʶ) like '1%' " + vbCr
                        strSQL = strSQL + " and   ������ = '" + strJSR + "'" + vbCr
                        strSQL = strSQL + " and   ������ in " + vbCr
                        strSQL = strSQL + " (" + vbCr
                        strSQL = strSQL + "   select b.��Ա���� from ����_B_�ϸ� a " + vbCr
                        strSQL = strSQL + "   left join ����_B_��Ա     b on a.��Ա���� = b.��Ա���� " + vbCr
                        strSQL = strSQL + "   left join ����_B_������λ c on a.��λ���� = c.��λ���� " + vbCr
                        strSQL = strSQL + "   where b.��Ա���� is not null " + vbCr
                        strSQL = strSQL + "   and   c.��λ���� is not null " + vbCr
                        strSQL = strSQL + "   and '" + strTemp + "' + '" + strSep + "' like '%'+rtrim(c.��λ����)+'" + strSep + "%' " + vbCr
                        strSQL = strSQL + "   group by b.��Ա����" + vbCr
                        strSQL = strSQL + ")" + vbCr
                        If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                            GoTo errProc
                        End If
                        If objDataSet.Tables(0).Rows.Count > 0 Then
                            blnCanBudeng = True
                            Exit Try
                        End If
                        Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                        objDataSet = Nothing
                    End If
                End If

                '��ȡָ����Ա�Ĳ����������2
                intBDFW = CType(Xydc.Platform.Common.Data.BudengshezhiData.enumBudengfanweiType.ZhiwuBumenLevel, Integer)
                strSQL = ""
                strSQL = strSQL + " select a.* from ����_B_�������� a " + vbCr
                strSQL = strSQL + " left join (" + vbCr
                strSQL = strSQL + "   select * from ����_B_�ϸ� " + vbCr
                strSQL = strSQL + "   where  ��Ա���� = '" + strUserId + "' " + vbCr
                strSQL = strSQL + " ) b on a.��λ���� = b.��λ���� " + vbCr
                strSQL = strSQL + " where b.��λ���� is not null " + vbCr
                strSQL = strSQL + " and   a.���Ƿ�Χ = " + intBDFW.ToString() + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    blnCanBudeng = False
                    Exit Try
                End If
                '��ȡ����ָ��ְ���б�2
                With objDataSet.Tables(0)
                    Dim blnDefined As Boolean
                    Dim intMinJSDM As Integer
                    Dim intMinJSXZ As Integer
                    Dim intCount As Integer
                    Dim i As Integer
                    intcount = .Rows.Count
                    For i = 0 To intcount - 1 Step 1
                        strTemp = ""
                        strTemp = objPulicParameters.getObjectValue(.Rows(i).Item("ְ���б�"), "").Trim()
                        intMinJSXZ = Xydc.Platform.Common.Data.CustomerData.intZZDM_FJCDSM.Length + 1      '��֯������ͼ�+1
                        intMinJSXZ = objPulicParameters.getObjectValue(.Rows(i).Item("��������"), intMinJSXZ)
                        If intMinJSXZ > Xydc.Platform.Common.Data.CustomerData.intZZDM_FJCDSM.Length Then
                            blnDefined = False
                        ElseIf intMinJSXZ < 1 Then
                            blnDefined = False
                        Else
                            intMinJSDM = Xydc.Platform.Common.Data.CustomerData.intZZDM_FJCDSM(intMinJSXZ - 1)
                            blnDefined = True
                        End If
                        If strTemp <> "" And blnDefined = True Then
                            '���ָ���ļ����Ƿ���ָ�����ŵ�ָ��ְ��strTemp���˴���
                            strSQL = ""
                            strSQL = strSQL + " select * from ����_B_���� " + vbCr
                            strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "' " + vbCr
                            strSQL = strSQL + " and   rtrim(���ӱ�ʶ) like '1_1%' " + vbCr
                            strSQL = strSQL + " and   ������ = '" + strJSR + "'" + vbCr
                            strSQL = strSQL + " and   ������ in " + vbCr
                            strSQL = strSQL + " (" + vbCr
                            strSQL = strSQL + "   select b.��Ա���� from ����_B_�ϸ� a " + vbCr
                            strSQL = strSQL + "   left join ����_B_��Ա     b on a.��Ա���� = b.��Ա���� " + vbCr
                            strSQL = strSQL + "   left join ����_B_������λ c on a.��λ���� = c.��λ���� " + vbCr
                            strSQL = strSQL + "   where b.��Ա���� is not null " + vbCr
                            strSQL = strSQL + "   and   b.��֯���� is not null " + vbCr
                            strSQL = strSQL + "   and   c.��λ���� is not null " + vbCr
                            strSQL = strSQL + "   and '" + strTemp + "' + '" + strSep + "' like '%'+rtrim(c.��λ����)+'" + strSep + "%' " + vbCr 'ָ��ְ��
                            strSQL = strSQL + "   and len(rtrim(b.��֯����)) >= " + intMinJSDM.ToString() + vbCr                                 'ָ����������
                            strSQL = strSQL + "   and rtrim(b.��֯����) like '" + strBMDM + "%' " + vbCr                                         '�������¼���λ
                            strSQL = strSQL + "   group by b.��Ա����" + vbCr
                            strSQL = strSQL + " )" + vbCr
                            Dim objDataSetA As System.Data.DataSet
                            If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSetA) = False Then
                                GoTo errProc
                            End If
                            If objDataSetA.Tables(0).Rows.Count > 0 Then
                                blnCanBudeng = True
                                Exit Try
                            End If
                            objDataSetA.Dispose()
                            objDataSetA = Nothing
                        End If
                    Next
                End With
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            canBuDengFile = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �ж�ָ����ԱstrSender�Ƿ����ֱ�ӷ��͸�strReceiver��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strSender            ������������
        '     strSenderBMDM        ��������������λ����
        '     strReceiver          ������������
        '     blnCanSend           �����أ��Ƿ���ԣ�
        '     strNewReceiver       �����أ�ת����Ա����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function canSendTo( _
            ByRef strErrMsg As String, _
            ByVal strSender As String, _
            ByVal strSenderBMDM As String, _
            ByVal strReceiver As String, _
            ByRef blnCanSend As Boolean, _
            ByRef strNewReceiver As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            canSendTo = False
            strErrMsg = ""
            blnCanSend = False
            strNewReceiver = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strSender Is Nothing Then strSender = ""
                strSender = strSender.Trim()
                If strSenderBMDM Is Nothing Then strSenderBMDM = ""
                strSenderBMDM = strSenderBMDM.Trim()
                If strReceiver Is Nothing Then strReceiver = ""
                strReceiver = strReceiver.Trim()

                '�Լ����Լ�
                If strSender = strReceiver Then
                    blnCanSend = True
                    Exit Try
                End If

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                'strReceiver����ֱ�ӷ�������??
                strSQL = ""
                strSQL = strSQL + " select a.*," + vbCr
                strSQL = strSQL + "   ������ת������=b.��Ա���� " + vbCr
                strSQL = strSQL + " from ����_B_��Ա a" + vbCr
                strSQL = strSQL + " left join ����_B_��Ա b on a.������ת�� = b.��Ա���� "
                strSQL = strSQL + " where a.��Ա���� = '" + strReceiver + "' " + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    blnCanSend = True
                    Exit Try
                End If
                Dim strKZSRY As String = ""
                strKZSRY = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item("��ֱ����Ա"), "")
                If strKZSRY = "" Then
                    blnCanSend = True
                    Exit Try
                End If
                Dim strZSRY As String = ""
                strZSRY = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item("������ת������"), "")
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '���������б�
                Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate
                Dim strKZSRYList As String
                If objdacCommon.doConvertToSQLValueList(strErrMsg, strKZSRY, strSep, strKZSRYList) = False Then
                    GoTo errProc
                End If

                '�ڿ�ֱ�ӷ��͵Ĳ�����
                strSQL = ""
                strSQL = strSQL + " select count(*) from ����_B_��֯���� " + vbCr
                strSQL = strSQL + " where ��֯���� in (" + strKZSRYList + ") " + vbCr
                strSQL = strSQL + " and '" + strSenderBMDM + "' like rtrim(��֯����) + '%'" + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                Dim intCount As Integer
                intCount = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item(0), 0)
                If intCount > 0 Then
                    blnCanSend = True
                    Exit Try
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '�ڿ�ֱ�ӷ��͵���Ա��
                strSQL = ""
                strSQL = strSQL + " select count(*) from ����_B_��Ա " + vbCr
                strSQL = strSQL + " where ��Ա���� in (" + strKZSRYList + ") " + vbCr
                strSQL = strSQL + " and   ��Ա���� = '" + strSender + "' " + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                intCount = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item(0), 0)
                If intCount > 0 Then
                    blnCanSend = True
                    Exit Try
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '����ֱ�ӷ���
                strNewReceiver = strZSRY

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            canSendTo = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �ж�ָ����ԱstrSenderList�Ƿ����ֱ�ӷ��͸�strReceiver��
        ' ֻҪ��1����ֱ�ӷ��;Ϳ��ԣ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strSenderList        �������������б�
        '     strReceiver          ������������
        '     blnCanSend           �����أ��Ƿ���ԣ�
        '     strNewReceiver       �����أ�ת����Ա����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function canSendTo( _
            ByRef strErrMsg As String, _
            ByVal strSenderList As String, _
            ByVal strReceiver As String, _
            ByRef blnCanSend As Boolean, _
            ByRef strNewReceiver As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            canSendTo = False
            strErrMsg = ""
            blnCanSend = False
            strNewReceiver = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strSenderList Is Nothing Then strSenderList = ""
                strSenderList = strSenderList.Trim()
                If strReceiver Is Nothing Then strReceiver = ""
                strReceiver = strReceiver.Trim()

                '�Լ����Լ�
                If strSenderList = strReceiver Then
                    blnCanSend = True
                    Exit Try
                End If

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                'strReceiver����ֱ�ӷ�������??
                strSQL = ""
                strSQL = strSQL + " select a.*," + vbCr
                strSQL = strSQL + "   ������ת������=b.��Ա���� " + vbCr
                strSQL = strSQL + " from ����_B_��Ա a" + vbCr
                strSQL = strSQL + " left join ����_B_��Ա b on a.������ת�� = b.��Ա���� "
                strSQL = strSQL + " where a.��Ա���� = '" + strReceiver + "' " + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    blnCanSend = True
                    Exit Try
                End If
                Dim strKZSRY As String = ""
                strKZSRY = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item("��ֱ����Ա"), "")
                If strKZSRY = "" Then
                    blnCanSend = True
                    Exit Try
                End If
                Dim strZSRY As String = ""
                strZSRY = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item("������ת������"), "")
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '���������б�
                Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate
                Dim strKZSRYList As String
                If objdacCommon.doConvertToSQLValueList(strErrMsg, strKZSRY, strSep, strKZSRYList) = False Then
                    GoTo errProc
                End If

                '�����鷢����
                Dim strValue() As String = strSenderList.Split(strSep.ToCharArray())
                Dim strSenderBMDM As String
                Dim strSender As String
                Dim intCount As Integer
                Dim intNum As Integer
                Dim i As Integer
                If strValue.Length < 1 Then
                    Exit Try
                End If
                intCount = strValue.Length
                For i = 0 To intCount - 1 Step 1
                    strSender = strValue(i)

                    '��ȡ��λ����
                    strSQL = ""
                    strSQL = strSQL + " select ��֯���� "
                    strSQL = strSQL + " from ����_B_��Ա " + vbCr
                    strSQL = strSQL + " where ��Ա���� = '" + strSender + "'" + vbCr
                    If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                        GoTo errProc
                    End If
                    If objDataSet.Tables(0).Rows.Count < 1 Then
                        strSenderBMDM = ""
                    Else
                        strSenderBMDM = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item("��֯����"), "")
                    End If
                    Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                    objDataSet = Nothing

                    '�ڿ�ֱ�ӷ��͵Ĳ�����
                    strSQL = ""
                    strSQL = strSQL + " select count(*) from ����_B_��֯���� " + vbCr
                    strSQL = strSQL + " where ��֯���� in (" + strKZSRYList + ") " + vbCr
                    strSQL = strSQL + " and '" + strSenderBMDM + "' like rtrim(��֯����) + '%'" + vbCr
                    If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                        GoTo errProc
                    End If
                    intNum = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item(0), 0)
                    If intNum > 0 Then
                        blnCanSend = True
                        Exit Try
                    End If
                    Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                    objDataSet = Nothing

                    '�ڿ�ֱ�ӷ��͵���Ա��
                    strSQL = ""
                    strSQL = strSQL + " select count(*) from ����_B_��Ա " + vbCr
                    strSQL = strSQL + " where ��Ա���� in (" + strKZSRYList + ") " + vbCr
                    strSQL = strSQL + " and   ��Ա���� = '" + strSender + "' " + vbCr
                    If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                        GoTo errProc
                    End If
                    intNum = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item(0), 0)
                    If intNum > 0 Then
                        blnCanSend = True
                        Exit Try
                    End If
                    Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                    objDataSet = Nothing
                Next

                '����ֱ�ӷ���
                strNewReceiver = strZSRY

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            canSendTo = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �ж�ָ����ԱstrUserXM�Ƿ���Խ����ļ���
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ��������Ա����
        '     blnCanDoJieshou      �����أ��Ƿ���ԣ�
        '     strFSRList           �����أ������������б�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function canDoJieshouFile( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByRef blnCanDoJieshou As Boolean, _
            ByRef strFSRList As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            canDoJieshouFile = False
            strErrMsg = ""
            blnCanDoJieshou = False
            strFSRList = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strTaskStatusWJSList As String = Me.FlowData.TaskStatusWJSList
                Dim strWJBS As String = Me.WJBS

                '��ȡ��ǰ����Աδ���յĽ��ӵ�
                strSQL = ""
                strSQL = strSQL + " select ������ from ����_B_���� "
                strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "' "
                strSQL = strSQL + " and   rtrim(���ӱ�ʶ) like '1%' "
                strSQL = strSQL + " and   ������   = '" + strUserXM + "' "
                strSQL = strSQL + " and   ����״̬ in (" + strTaskStatusWJSList + ") "
                strSQL = strSQL + " group by ������ "
                strSQL = strSQL + " order by ������ "
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    Exit Try
                End If

                '���㷢�����б�
                Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate
                Dim strFSR As String
                Dim intCount As Integer
                Dim i As Integer
                With objDataSet.Tables(0)
                    intCount = .Rows.Count
                    For i = 0 To intCount - 1 Step 1
                        strFSR = objPulicParameters.getObjectValue(.Rows(i).Item("������"), "")
                        If strFSR <> "" Then
                            If strFSRList = "" Then
                                strFSRList = strFSR
                            Else
                                strFSRList = strFSRList + strSep + strFSR
                            End If
                        End If
                    Next
                End With
                blnCanDoJieshou = True

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            canDoJieshouFile = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function









        '----------------------------------------------------------------
        ' ��ȡstrUserXM�Ŀ��Զ�����Щ��Ա?
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ����Ա����
        '     strUserId            ����Ա����
        '     strBMDM              ��strUserXM������λ����
        '     strRYLIST            ��������Ա�б�,��׼�ָ����ָ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getKebeidubanRenyuan( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByVal strUserId As String, _
            ByVal strBMDM As String, _
            ByRef strRYLIST As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            getKebeidubanRenyuan = False
            strRYLIST = ""
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '���Ӱ������״̬SQL�б�
                Dim strTaskStatusYWCList As String = Me.FlowData.TaskStatusYWCList

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '����û�а�����ϡ���������Ľ��ӵ�
                strSQL = ""
                strSQL = strSQL + " select * from ����_B_���� " + vbCr
                strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "' " + vbCr                       '��ǰ�ļ�
                strSQL = strSQL + " and   rtrim(���ӱ�ʶ) like '1_1__0%' " + vbCr                            '�ѷ���+�������ܿ�+��֪ͨ
                strSQL = strSQL + " and   ����״̬ not in (" + strTaskStatusYWCList + ") " + vbCr     'δ���
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If

                '�ϲ�
                With objDataSet.Tables(0)
                    Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate
                    Dim blnCanDuban As Boolean
                    Dim intCount As Integer
                    Dim strJSR As String
                    Dim i As Integer
                    intCount = .Rows.Count
                    For i = 0 To intCount Step 1
                        strJSR = objPulicParameters.getObjectValue(.Rows(i).Item("������"), "")
                        If strJSR <> "" Then
                            If Me.canDubanFile(strErrMsg, strUserId, strBMDM, strJSR, blnCanDuban) = False Then
                                GoTo errProc
                            End If
                            If blnCanDuban = True Then
                                If strRYLIST = "" Then
                                    strRYLIST = strJSR
                                Else
                                    strRYLIST = strRYLIST + strSep + strJSR
                                End If
                            End If
                        End If
                    Next
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getKebeidubanRenyuan = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡָ����ԱstrUserXM�ı��߰�����
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ����Ա����
        '     objBeicuibanData     ����������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getBeicuibanData( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByRef objBeicuibanData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim objTempBeicuibanData As Xydc.Platform.Common.Data.FlowData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            getBeicuibanData = False
            objBeicuibanData = Nothing
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '�������ݼ�
                objTempBeicuibanData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_CUIBAN_JIAOJIE)
                If strWJBS = "" Then Exit Try

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                'ִ�м���
                With Me.m_objSqlDataAdapter
                    '��ȡָ����Ա�Ѿ����߰�����
                    strSQL = ""
                    strSQL = strSQL + " select a.*, b.��������, b.����״̬ from " + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select * from ����_B_�߰� " + vbCr
                    strSQL = strSQL + "   where �ļ���ʶ = '" + strWJBS + "' " + vbCr
                    strSQL = strSQL + "   and   ���߰��� = '" + strUserXM + "' " + vbCr
                    strSQL = strSQL + " ) a " + vbCr
                    strSQL = strSQL + " left join " + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select * from ����_B_���� " + vbCr
                    strSQL = strSQL + "   where �ļ���ʶ = '" + strWJBS + "' " + vbCr
                    strSQL = strSQL + " ) b on a.�ļ���ʶ = b.�ļ���ʶ and a.������� = b.������� " + vbCr
                    strSQL = strSQL + " order by a.�߰���� " + vbCr

                    '���ò���
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    'ִ�в���
                    .Fill(objTempBeicuibanData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_CUIBAN_JIAOJIE))
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            objBeicuibanData = objTempBeicuibanData
            getBeicuibanData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.FlowData.SafeRelease(objTempBeicuibanData)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡstrUserXM��û�н�����Щ��Ա�����Ľ��ӵ�?
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ����Ա����
        '     strRYLIST            ��������Ա�б�,��׼�ָ����ָ�
        '     blnJieshouAll        �������Ƿ�ȫ������?
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getKeJieshouRenyuan( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByRef strRYLIST As String, _
            ByRef blnJieshouAll As Boolean) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            getKeJieshouRenyuan = False
            blnJieshouAll = False
            strRYLIST = ""
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '����δ����״̬SQL�б�
                Dim strTaskStatusWJSList As String = Me.FlowData.TaskStatusWJSList

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '��ȡ��ǰ����Աδ���յĽ��ӵ�
                strSQL = ""
                strSQL = strSQL + " select ������ from ����_B_���� " + vbCr
                strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "' " + vbCr
                strSQL = strSQL + " and   rtrim(���ӱ�ʶ) like '1%' " + vbCr
                strSQL = strSQL + " and   ������   = '" + strUserXM + "' " + vbCr
                strSQL = strSQL + " and   ����״̬ in (" + strTaskStatusWJSList + ") " + vbCr
                strSQL = strSQL + " group by ������ " + vbCr
                strSQL = strSQL + " order by ������ " + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    blnJieshouAll = True
                    Exit Try
                End If

                '�ϲ�
                With objDataSet.Tables(0)
                    Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate
                    Dim intCount As Integer
                    Dim strFSR As String
                    Dim i As Integer
                    intCount = .Rows.Count
                    For i = 0 To intCount Step 1
                        strFSR = objPulicParameters.getObjectValue(.Rows(i).Item("������"), "")
                        If strFSR <> "" Then
                            If strRYLIST = "" Then
                                strRYLIST = strFSR
                            Else
                                strRYLIST = strRYLIST + strSep + strFSR
                            End If
                        End If
                    Next
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getKeJieshouRenyuan = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ�ļ��ı༭������Ϣ?
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     blnLocked            �������Ƿ����?
        '     strBMMC              ������������򷵻ر༭��Ա���ڵ�λ����
        '     strRYMC              ������������򷵻ر༭��Ա����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getFileLocked( _
            ByRef strErrMsg As String, _
            ByRef blnLocked As Boolean, _
            ByRef strBMMC As String, _
            ByRef strRYMC As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            getFileLocked = False
            blnLocked = False
            strBMMC = ""
            strRYMC = ""
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '��ȡ���ڱ༭���ļ�����Ա��������Ϣ
                strSQL = ""
                strSQL = strSQL + " select a.*, " + vbCr
                strSQL = strSQL + "   b.��Ա����, c.��֯���� " + vbCr
                strSQL = strSQL + " from ����_B_�ļ����� a " + vbCr
                strSQL = strSQL + " left join ����_B_��Ա     b on a.��Ա���� = b.��Ա���� " + vbCr
                strSQL = strSQL + " left join ����_B_��֯���� c on b.��֯���� = c.��֯���� " + vbCr
                strSQL = strSQL + " where a.�ļ���ʶ = '" + strWJBS + "'" + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    Exit Try
                End If

                '������Ϣ
                blnLocked = True
                With objDataSet.Tables(0).Rows(0)
                    strBMMC = objPulicParameters.getObjectValue(.Item("��֯����"), "")
                    strRYMC = objPulicParameters.getObjectValue(.Item("��Ա����"), "")
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getFileLocked = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ�µ��ļ���ʶ
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strWJBS              �������ļ���ʶ
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getNewWJBS( _
            ByRef strErrMsg As String, _
            ByRef strWJBS As String) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim strSQL As String

            getNewWJBS = False
            strWJBS = ""
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection

                '��������
                If objdacCommon.getNewGUID(strErrMsg, objSqlConnection, strWJBS) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getNewWJBS = True
            Exit Function

errProc:
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ�µķ������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strFSXH              �����ط������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getNewFSXH( _
            ByRef strErrMsg As String, _
            ByRef strFSXH As String) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim strSQL As String

            getNewFSXH = False
            strErrMsg = ""
            strFSXH = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If Me.m_blnFillData = False Then
                    strErrMsg = "���󣺶���û��������ݣ�����ʹ�ã�"
                    GoTo errProc
                End If

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.FlowData.WJBS

                '��������
                If objdacCommon.getNewCode(strErrMsg, objSqlConnection, "�������", "�ļ���ʶ", strWJBS, "����_B_����", True, strFSXH) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getNewFSXH = True
            Exit Function

errProc:
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡstrUserXM���1�ε���������Ľ��ӵ�(�����Ƿ���꣡)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ���û�����
        '     objJiaoJieData       ���������1�ν�������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getLastJiaojieData( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByRef objJiaoJieData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objTempJiaoJieData As Xydc.Platform.Common.Data.FlowData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            getLastJiaojieData = False
            objJiaoJieData = Nothing
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '�������������ţ�׼�򣺽����˿ɿ������ӡ����͡�������Ϣ
                strSQL = ""
                strSQL = strSQL + " select isnull(max(�������),0) as ������� " + vbCr
                strSQL = strSQL + " from ����_B_���� " + vbCr
                strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "' " + vbCr           '��ǰ�ļ�
                strSQL = strSQL + " and   ������   = '" + strUserXM + "' " + vbCr         '������
                strSQL = strSQL + " and   rtrim(���ӱ�ʶ) like '__1__0_%' " + vbCr               '�������ܿ�+��֪ͨ��
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                Dim intXH As Integer
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    intXH = 0
                Else
                    With objDataSet.Tables(0).Rows(0)
                        intXH = objPulicParameters.getObjectValue(.Item("�������"), 0)
                    End With
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '�������ݼ�
                objTempJiaoJieData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_JIAOJIE)
                If strWJBS = "" Then Exit Try

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                'ִ�м���
                With Me.SqlDataAdapter
                    '���㽻����Ϣ
                    strSQL = ""
                    strSQL = strSQL + " select * from ����_B_���� " + vbCr
                    strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "' " + vbCr
                    strSQL = strSQL + " and   ������   = '" + strUserXM + "' " + vbCr
                    strSQL = strSQL + " and   ������� = " + intXH.ToString() + vbCr

                    '���ò���
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    'ִ�в���
                    .Fill(objTempJiaoJieData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_JIAOJIE))
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            objJiaoJieData = objTempJiaoJieData
            getLastJiaojieData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.Common.Data.FlowData.SafeRelease(objTempJiaoJieData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡstrUserXM���1�ε����������δ������ϵĽ��ӵ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ���û�����
        '     objJiaoJieData       ���������1�ν�������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getLastZJBJiaojieData( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByRef objJiaoJieData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objTempJiaoJieData As Xydc.Platform.Common.Data.FlowData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            getLastZJBJiaojieData = False
            objJiaoJieData = Nothing
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strTaskStatusZJBList As String = Me.FlowData.TaskStatusZJBList
                Dim strWJBS As String = Me.WJBS

                '�������������ţ�׼�򣺽����˿ɿ������ӡ����͡�������Ϣ
                strSQL = ""
                strSQL = strSQL + " select isnull(max(�������),0) as ������� " + vbCr
                strSQL = strSQL + " from ����_B_���� " + vbCr
                strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "' " + vbCr                       '��ǰ�ļ�
                strSQL = strSQL + " and   ������   = '" + strUserXM + "' " + vbCr                     '������
                strSQL = strSQL + " and   ����״̬ in (" + strTaskStatusZJBList + ")" + vbCr          '������δ����
                strSQL = strSQL + " and   rtrim(���ӱ�ʶ) like '__1__0_%' " + vbCr                    '�������ܿ�+��֪ͨ��
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                Dim intXH As Integer
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    intXH = 0
                Else
                    intXH = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item("�������"), 0)
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '�������ݼ�
                objTempJiaoJieData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_JIAOJIE)
                If strWJBS = "" Then Exit Try

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                'ִ�м���
                With Me.SqlDataAdapter
                    '���㽻����Ϣ
                    strSQL = ""
                    strSQL = strSQL + " select * from ����_B_���� " + vbCr
                    strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "' " + vbCr
                    strSQL = strSQL + " and   ������   = '" + strUserXM + "' " + vbCr
                    strSQL = strSQL + " and   ������� = " + intXH.ToString() + vbCr

                    '���ò���
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    'ִ�в���
                    .Fill(objTempJiaoJieData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_JIAOJIE))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            objJiaoJieData = objTempJiaoJieData
            getLastZJBJiaojieData = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.Common.Data.FlowData.SafeRelease(objTempJiaoJieData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���ݸ���״̬strStatus��ȡ��������Ľ��ӵ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strStatus            ������״̬SQLֵ�б�
        '     objJiaojieData       �����ؽ��ӵ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getJiaojieData( _
            ByRef strErrMsg As String, _
            ByVal strStatus As String, _
            ByRef objJiaojieData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim objTempJiaojieData As Xydc.Platform.Common.Data.FlowData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            getJiaojieData = False
            objJiaojieData = Nothing
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strStatus Is Nothing Then strStatus = ""
                strStatus = strStatus.Trim()

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '�������ݼ�
                objTempJiaojieData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_JIAOJIE)
                If strWJBS = "" Then Exit Try

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                'ִ�м���
                With Me.m_objSqlDataAdapter
                    '��ȡָ��״̬�Ľ��Ӵ���
                    strSQL = ""
                    strSQL = strSQL + " select * from ����_B_���� " + vbCr
                    strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "' " + vbCr
                    strSQL = strSQL + " and   rtrim(���ӱ�ʶ) like '1____0%' " + vbCr
                    strSQL = strSQL + " and   ����״̬ in (" + strStatus + ")" + vbCr

                    '���ò���
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    'ִ�в���
                    .Fill(objTempJiaojieData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_JIAOJIE))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            objJiaojieData = objTempJiaojieData
            getJiaojieData = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.FlowData.SafeRelease(objTempJiaojieData)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ����strWJBS��ȡ���ӵ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strWhere             ����������
        '     blnUnused            ���ӿ�����
        '     objJiaoJieData       �����ؽ�������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        ' �޸ļ�¼
        '      ����
        '----------------------------------------------------------------
        Public Overridable Function getJiaojieData( _
            ByRef strErrMsg As String, _
            ByVal strWhere As String, _
            ByVal blnUnused As Boolean, _
            ByRef objJiaoJieData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim objTempJiaoJieData As Xydc.Platform.Common.Data.FlowData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            getJiaojieData = False
            objJiaoJieData = Nothing
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '�������ݼ�
                objTempJiaoJieData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_JIAOJIE)
                If strWJBS = "" Then Exit Try

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                'ִ�м���
                With Me.SqlDataAdapter
                    '���㽻����Ϣ
                    strSQL = ""
                    strSQL = strSQL + " select * from ����_B_���� a" + vbCr
                    strSQL = strSQL + " where a.�ļ���ʶ = '" + strWJBS + "' " + vbCr
                    If strWhere <> "" Then
                        strSQL = strSQL + " and " + strWhere + vbCr
                    End If
                    strSQL = strSQL + " order by a.�ļ���ʶ,a.�������" + vbCr

                    '���ò���
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    'ִ�в���
                    .Fill(objTempJiaoJieData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_JIAOJIE))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            objJiaoJieData = objTempJiaoJieData
            getJiaojieData = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.FlowData.SafeRelease(objTempJiaoJieData)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ����intXH��ȡ���ӵ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     intXH                ���������
        '     objJiaoJieData       �����ؽ�������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getJiaojieData( _
            ByRef strErrMsg As String, _
            ByVal intXH As Integer, _
            ByRef objJiaoJieData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim objTempJiaoJieData As Xydc.Platform.Common.Data.FlowData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            getJiaojieData = False
            objJiaoJieData = Nothing
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '�������ݼ�
                objTempJiaoJieData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_JIAOJIE)
                If strWJBS = "" Then Exit Try

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                'ִ�м���
                With Me.SqlDataAdapter
                    '���㽻����Ϣ
                    strSQL = ""
                    strSQL = strSQL + " select * from ����_B_���� " + vbCr
                    strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "' " + vbCr
                    strSQL = strSQL + " and   ������� = " + intXH.ToString() + vbCr

                    '���ò���
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    'ִ�в���
                    .Fill(objTempJiaoJieData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_JIAOJIE))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            objJiaoJieData = objTempJiaoJieData
            getJiaojieData = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.FlowData.SafeRelease(objTempJiaoJieData)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ����strWJBS��ȡ���ӵ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objJiaoJieData       �����ؽ�������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getJiaojieData( _
            ByRef strErrMsg As String, _
            ByRef objJiaoJieData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim objTempJiaoJieData As Xydc.Platform.Common.Data.FlowData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            getJiaojieData = False
            objJiaoJieData = Nothing
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '�������ݼ�
                objTempJiaoJieData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_JIAOJIE)
                If strWJBS = "" Then Exit Try

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                'ִ�м���
                With Me.SqlDataAdapter
                    '���㽻����Ϣ
                    strSQL = ""
                    strSQL = strSQL + " select * from ����_B_���� " + vbCr
                    strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "' " + vbCr

                    '���ò���
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    'ִ�в���
                    .Fill(objTempJiaoJieData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_JIAOJIE))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            objJiaoJieData = objTempJiaoJieData
            getJiaojieData = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.FlowData.SafeRelease(objTempJiaoJieData)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡstrUserXMû�а��������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ���û�����
        '     objJiaoJieData       �����ؽ�������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getNotCompletedTaskData( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByRef objJiaoJieData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim objTempJiaoJieData As Xydc.Platform.Common.Data.FlowData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            getNotCompletedTaskData = False
            objJiaoJieData = Nothing
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strTaskStatusYWCList As String = Me.FlowData.TaskStatusYWCList
                Dim strWJBS As String = Me.WJBS

                '�������ݼ�
                objTempJiaoJieData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_JIAOJIE)
                If strWJBS = "" Then Exit Try

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                'ִ�м���
                With Me.SqlDataAdapter
                    '���㽻����Ϣ
                    strSQL = ""
                    strSQL = strSQL + " select * from ����_B_����" + vbCr
                    strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "'" + vbCr                     '��ǰ�ļ�
                    strSQL = strSQL + " and   ������   = '" + strUserXM + "'" + vbCr                   '������
                    strSQL = strSQL + " and   rtrim(���ӱ�ʶ) like '__1__0%'" + vbCr                   '�������ܿ�+��֪ͨ������
                    strSQL = strSQL + " and   ����״̬ not in (" + strTaskStatusYWCList + ")" + vbCr   '������δ����

                    '���ò���
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    'ִ�в���
                    .Fill(objTempJiaoJieData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_JIAOJIE))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            objJiaoJieData = objTempJiaoJieData
            getNotCompletedTaskData = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.FlowData.SafeRelease(objTempJiaoJieData)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ�����ļ��ĸ�����Ϣ
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strFJNR              �����ظ����������˵����Ϣ
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getFujianData( _
            ByRef strErrMsg As String, _
            ByRef strFJNR As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            getFujianData = False
            strFJNR = ""
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '�򿪸����б�
                strSQL = ""
                strSQL = strSQL + " select * from ����_B_���� " + vbCr
                strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "'" + vbCr
                strSQL = strSQL + " order by ���" + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    Exit Try
                End If

                '������Ϣ
                Dim strInfo As String
                Dim strSM As String
                Dim strXH As String
                Dim intCount As Integer
                Dim i As Integer
                With objDataSet.Tables(0)
                    intCount = .Rows.Count
                    For i = 0 To intCount - 1 Step 1
                        strXH = objPulicParameters.getObjectValue(.Rows(i).Item("���"), "")
                        strSM = objPulicParameters.getObjectValue(.Rows(i).Item("˵��"), "")
                        strInfo = strXH + ". " + strSM

                        If strFJNR = "" Then
                            strFJNR = strInfo
                        Else
                            strFJNR = strFJNR + Chr(13) + Chr(10) + strInfo
                        End If
                    Next
                End With


            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getFujianData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ��ʽ�ļ��ĸ�����Ϣ
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     blnZSWJ              ��������
        '     strFJNR              �����ظ����������˵����Ϣ
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getFujianData( _
            ByRef strErrMsg As String, _
            ByVal blnZSWJ As Boolean, _
            ByRef strFJNR As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            getFujianData = False
            strFJNR = ""
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '�򿪸����б�
                strSQL = ""
                strSQL = strSQL + " select * from ����_B_���� " + vbCr
                strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "'" + vbCr
                strSQL = strSQL + " order by ���" + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    Exit Try
                End If

                '������Ϣ
                Dim strInfo As String
                Dim strSM As String
                Dim strXH As String
                Dim intCount As Integer
                Dim i As Integer
                With objDataSet.Tables(0)
                    intCount = .Rows.Count
                    For i = 0 To intCount - 1 Step 1
                        strXH = objPulicParameters.getObjectValue(.Rows(i).Item("���"), "")
                        strSM = objPulicParameters.getObjectValue(.Rows(i).Item("˵��"), "")
                        strInfo = strXH + ". " + strSM

                        If strFJNR = "" Then
                            strFJNR = strInfo
                        Else
                            strFJNR = strFJNR + Chr(13) + Chr(10) + "          " + strInfo
                        End If
                    Next
                    strFJNR = "    ������" + strFJNR
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getFujianData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡstrUserXMδ��ɵ��������˵���Ŀ
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ���û�����
        '     intNum               ������δ��ɵ��������˵���Ŀ
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getNotCompleteSPSYNum( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByRef intNum As Integer) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            getNotCompleteSPSYNum = False
            intNum = 0
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strTaskStatusYWCList As String = Me.FlowData.TaskStatusYWCList
                Dim strTaskBlzlSPSYList As String = Me.FlowData.TaskBlzlSPSYList
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '����
                strSQL = ""
                strSQL = strSQL + " select count(*) "
                strSQL = strSQL + " from ����_B_���� "
                strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "' "
                strSQL = strSQL + " and   ������   = '" + strUserXM + "' "
                strSQL = strSQL + " and   �������� in (" + strTaskBlzlSPSYList + ") "
                strSQL = strSQL + " and   ����״̬ not in (" + strTaskStatusYWCList + ") "
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    Exit Try
                End If

                '������Ϣ
                With objDataSet.Tables(0).Rows(0)
                    intNum = objPulicParameters.getObjectValue(.Item(0), 0)
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getNotCompleteSPSYNum = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ�ļ�������ļ���Ϣ(����ļ�+��ظ���)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objXGWJData          ����������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getXgwjData( _
            ByRef strErrMsg As String, _
            ByRef objXGWJData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim objTempXGWJData As Xydc.Platform.Common.Data.FlowData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            getXgwjData = False
            objXGWJData = Nothing
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '�������ݼ�
                objTempXGWJData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_SHENPIWENJIAN_FUJIAN)
                If strWJBS = "" Then Exit Try

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                'ִ�м���
                Dim intXGWJLB_Fujian As Integer = Xydc.Platform.Common.Data.FlowData.enumXGWJLB.FujianFile
                Dim intXGWJLB_File As Integer = Xydc.Platform.Common.Data.FlowData.enumXGWJLB.FlowFile
                With Me.m_objSqlDataAdapter
                    '���ļ�����������ļ��ľ�����Ϣ
                    strSQL = ""
                    strSQL = strSQL + " select a.*" + vbCr
                    strSQL = strSQL + " from " + vbCr
                    strSQL = strSQL + " (" + vbCr
                    '**********************************************************************************
                    strSQL = strSQL + "   select a.*" + vbCr
                    strSQL = strSQL + "   from" + vbCr
                    strSQL = strSQL + "   (" + vbCr
                    strSQL = strSQL + "     select "
                    strSQL = strSQL + "       b.�ļ���ʶ," + vbCr
                    strSQL = strSQL + "       b.�ļ�����," + vbCr
                    strSQL = strSQL + "       b.��������," + vbCr
                    strSQL = strSQL + "       b.�ļ�����," + vbCr
                    strSQL = strSQL + "       b.���͵�λ," + vbCr
                    strSQL = strSQL + "       b.�ļ�����," + vbCr
                    strSQL = strSQL + "       b.���ش���," + vbCr
                    strSQL = strSQL + "       b.�ļ����," + vbCr
                    strSQL = strSQL + "       b.�ļ����," + vbCr
                    strSQL = strSQL + "       b.�ļ����," + vbCr
                    strSQL = strSQL + "       b.���쵥λ," + vbCr
                    strSQL = strSQL + "       b.�����," + vbCr
                    strSQL = strSQL + "       b.�������," + vbCr
                    strSQL = strSQL + "       b.����״̬," + vbCr
                    strSQL = strSQL + "       b.��ˮ��," + vbCr
                    strSQL = strSQL + "       b.�����," + vbCr
                    strSQL = strSQL + "       b.��������," + vbCr
                    '**********************************************************************************
                    strSQL = strSQL + "       ����ʶ = " + intXGWJLB_File.ToString() + "," + vbCr
                    strSQL = strSQL + "       ���     = a.˳���," + vbCr
                    strSQL = strSQL + "       ҳ��     = 0," + vbCr
                    strSQL = strSQL + "       λ��     = ' '," + vbCr
                    '**********************************************************************************
                    strSQL = strSQL + "       ��ʾ��� = a.˳���," + vbCr
                    strSQL = strSQL + "       �����ļ� = ''," + vbCr
                    strSQL = strSQL + "       ���ر�־ = 0" + vbCr
                    strSQL = strSQL + "     from" + vbCr
                    strSQL = strSQL + "     (" + vbCr
                    strSQL = strSQL + "       select ��ǰ�ļ���ʶ,˳��� " + vbCr
                    strSQL = strSQL + "       from ����_B_����ļ�" + vbCr
                    strSQL = strSQL + "       where �ϼ��ļ���ʶ = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + "     ) a" + vbCr
                    strSQL = strSQL + "     left join" + vbCr
                    strSQL = strSQL + "     (" + vbCr
                    strSQL = strSQL + "       select * " + vbCr
                    strSQL = strSQL + "       from ����_V_ȫ�������ļ���" + vbCr
                    strSQL = strSQL + "     ) b on a.��ǰ�ļ���ʶ = b.�ļ���ʶ" + vbCr
                    strSQL = strSQL + "     where b.�ļ���ʶ is not null" + vbCr
                    strSQL = strSQL + "   ) a" + vbCr
                    '**********************************************************************************
                    strSQL = strSQL + "   union" + vbCr
                    '**********************************************************************************
                    '��ȡ��ظ���
                    strSQL = strSQL + "   select " + vbCr
                    strSQL = strSQL + "     �ļ���ʶ," + vbCr
                    strSQL = strSQL + "     �ļ����� = '����'," + vbCr
                    strSQL = strSQL + "     �������� = '����'," + vbCr
                    strSQL = strSQL + "     �ļ����� = '����'," + vbCr
                    strSQL = strSQL + "     ���͵�λ = ' '," + vbCr
                    strSQL = strSQL + "     �ļ����� = ˵��," + vbCr
                    strSQL = strSQL + "     ���ش��� = ' '," + vbCr
                    strSQL = strSQL + "     �ļ���� = ' '," + vbCr
                    strSQL = strSQL + "     �ļ���� = ' '," + vbCr
                    strSQL = strSQL + "     �ļ���� = 0," + vbCr
                    strSQL = strSQL + "     ���쵥λ = ' '," + vbCr
                    strSQL = strSQL + "     �����   = ' '," + vbCr
                    strSQL = strSQL + "     ������� = null," + vbCr
                    strSQL = strSQL + "     ����״̬ = ' '," + vbCr
                    strSQL = strSQL + "     ��ˮ��   = ' '," + vbCr
                    strSQL = strSQL + "     �����   = ' '," + vbCr
                    strSQL = strSQL + "     �������� = 0," + vbCr
                    '**********************************************************************************
                    strSQL = strSQL + "     ����ʶ = " + intXGWJLB_Fujian.ToString() + "," + vbCr
                    strSQL = strSQL + "     ���," + vbCr
                    strSQL = strSQL + "     ҳ��," + vbCr
                    strSQL = strSQL + "     λ��," + vbCr
                    '**********************************************************************************
                    strSQL = strSQL + "     ��ʾ��� = ���," + vbCr
                    strSQL = strSQL + "     �����ļ� = ''," + vbCr
                    strSQL = strSQL + "     ���ر�־ = 0" + vbCr
                    strSQL = strSQL + "   from ����_B_����ļ����� " + vbCr
                    strSQL = strSQL + "   where �ļ���ʶ = '" + strWJBS + "'" + vbCr
                    '**********************************************************************************
                    strSQL = strSQL + " ) a" + vbCr
                    strSQL = strSQL + " order by a.��ʾ���" + vbCr

                    '���ò���
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    'ִ�в���
                    .Fill(objTempXGWJData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_SHENPIWENJIAN_FUJIAN))
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            objXGWJData = objTempXGWJData
            getXgwjData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.FlowData.SafeRelease(objTempXGWJData)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �����ļ���Ż�ȡ����ļ��������ض�������Ϣ
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     intWJXH              ���ļ����
        '     objXgwjFujianData    ����������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getXgwjFujianData( _
            ByRef strErrMsg As String, _
            ByVal intWJXH As Integer, _
            ByRef objXgwjFujianData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim objTempXgwjFujianData As Xydc.Platform.Common.Data.FlowData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            getXgwjFujianData = False
            objXgwjFujianData = Nothing
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '�������ݼ�
                objTempXgwjFujianData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_XIANGGUANWENJIANFUJIAN)
                If strWJBS = "" Then Exit Try

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                'ִ�м���
                With Me.m_objSqlDataAdapter
                    '��ȡ��������
                    strSQL = ""
                    strSQL = strSQL + " select a.*" + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select *,"
                    strSQL = strSQL + "     ��ʾ��� = ���,"
                    strSQL = strSQL + "     �����ļ� = '',"
                    strSQL = strSQL + "     ���ر�־ = 0 " + vbCr
                    strSQL = strSQL + "   from ����_B_����ļ����� " + vbCr
                    strSQL = strSQL + "   where �ļ���ʶ = @wjbs" + vbCr
                    strSQL = strSQL + "   and   ���     = @wjxh" + vbCr
                    strSQL = strSQL + " ) a" + vbCr
                    strSQL = strSQL + " order by a.��ʾ���" + vbCr

                    '���ò���
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                    objSqlCommand.Parameters.AddWithValue("@wjxh", intWJXH)
                    .SelectCommand = objSqlCommand

                    'ִ�в���
                    .Fill(objTempXgwjFujianData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_XIANGGUANWENJIANFUJIAN))
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            objXgwjFujianData = objTempXgwjFujianData
            getXgwjFujianData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.FlowData.SafeRelease(objTempXgwjFujianData)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ�ļ��ĸ�����Ϣ
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objFujianData        ����������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getFujianData( _
            ByRef strErrMsg As String, _
            ByRef objFujianData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim objTempFujianData As Xydc.Platform.Common.Data.FlowData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            getFujianData = False
            objFujianData = Nothing
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '�������ݼ�
                objTempFujianData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_FUJIAN)
                If strWJBS = "" Then Exit Try

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                'ִ�м���
                With Me.m_objSqlDataAdapter
                    '��ȡ��������
                    strSQL = ""
                    strSQL = strSQL + " select a.*" + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select *,"
                    strSQL = strSQL + "     ��ʾ��� = ���,"
                    strSQL = strSQL + "     �����ļ� = '',"
                    strSQL = strSQL + "     ���ر�־ = 0 " + vbCr
                    strSQL = strSQL + "   from ����_B_���� " + vbCr
                    strSQL = strSQL + "   where �ļ���ʶ = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + " ) a" + vbCr
                    strSQL = strSQL + " order by a.��ʾ���" + vbCr

                    '���ò���
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    'ִ�в���
                    .Fill(objTempFujianData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_FUJIAN))
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            objFujianData = objTempFujianData
            getFujianData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.FlowData.SafeRelease(objTempFujianData)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �����ļ���Ż�ȡ�ļ����ض�������Ϣ
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     intWJXH              ���ļ����
        '     objFujianData        ����������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getFujianData( _
            ByRef strErrMsg As String, _
            ByVal intWJXH As Integer, _
            ByRef objFujianData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim objTempFujianData As Xydc.Platform.Common.Data.FlowData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            getFujianData = False
            objFujianData = Nothing
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '�������ݼ�
                objTempFujianData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_FUJIAN)
                If strWJBS = "" Then Exit Try

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                'ִ�м���
                With Me.m_objSqlDataAdapter
                    '��ȡ��������
                    strSQL = ""
                    strSQL = strSQL + " select a.*" + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select *,"
                    strSQL = strSQL + "     ��ʾ��� = ���,"
                    strSQL = strSQL + "     �����ļ� = '',"
                    strSQL = strSQL + "     ���ر�־ = 0 " + vbCr
                    strSQL = strSQL + "   from ����_B_���� " + vbCr
                    strSQL = strSQL + "   where �ļ���ʶ = @wjbs" + vbCr
                    strSQL = strSQL + "   and   ���     = @wjxh" + vbCr
                    strSQL = strSQL + " ) a" + vbCr
                    strSQL = strSQL + " order by a.��ʾ���" + vbCr

                    '���ò���
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                    objSqlCommand.Parameters.AddWithValue("@wjxh", intWJXH)
                    .SelectCommand = objSqlCommand

                    'ִ�в���
                    .Fill(objTempFujianData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_FUJIAN))
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            objFujianData = objTempFujianData
            getFujianData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.FlowData.SafeRelease(objTempFujianData)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ��1�����͸�strUserXM�ķ���������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ���û�����
        '     strFirstSender       �����ط���������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getFirstSender( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByRef strFirstSender As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            getFirstSender = False
            strFirstSender = ""
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '����
                strSQL = ""
                strSQL = strSQL + " select * from ����_B_���� " + vbCr
                strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "' " + vbCr
                strSQL = strSQL + " and   ������   = '" + strUserXM + "' " + vbCr
                strSQL = strSQL + " order by �������" + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    Exit Try
                End If

                '������Ϣ
                With objDataSet.Tables(0).Rows(0)
                    strFirstSender = objPulicParameters.getObjectValue(.Item("������"), "")
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getFirstSender = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡָ����ԱstrCurUser�Ƿ�����ڽ��Ӽ�¼�в鿴strCheckUser������
        '     strErrMsg             ����������򷵻ش�����Ϣ
        '     strCurUser            ����ǰ��Ա����
        '     strCurUserBMDM        ����ǰ��Ա������λ����
        '     strCheckUser          ��������Ա����
        '     strNewName            �����أ�Ҫ��ʾ������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getDisplayName( _
            ByRef strErrMsg As String, _
            ByVal strCurUser As String, _
            ByVal strCurUserBMDM As String, _
            ByVal strCheckUser As String, _
            ByRef strNewName As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            getDisplayName = False
            strErrMsg = ""
            strNewName = strCheckUser

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strCurUser Is Nothing Then strCurUser = ""
                strCurUser = strCurUser.Trim()
                If strCurUserBMDM Is Nothing Then strCurUserBMDM = ""
                strCurUserBMDM = strCurUserBMDM.Trim()
                If strCheckUser Is Nothing Then strCheckUser = ""
                strCheckUser = strCheckUser.Trim()

                '�Լ����Լ�
                If strCurUser = strCheckUser Then
                    Exit Try
                End If

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                'strCheckUser���޲鿴����??
                strSQL = ""
                strSQL = strSQL + " select a.* " + vbCr
                strSQL = strSQL + " from ����_B_��Ա a" + vbCr
                strSQL = strSQL + " where a.��Ա���� = '" + strCheckUser + "' " + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    Exit Try
                End If
                Dim strKCKXM As String = ""
                strKCKXM = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item("�ɲ鿴����"), "")
                If strKCKXM = "" Then
                    Exit Try
                End If
                Dim strJJXSMC As String = ""
                strJJXSMC = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item("������ʾ����"), "")
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '���������б�
                Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate
                Dim strKCKXMList As String
                If objdacCommon.doConvertToSQLValueList(strErrMsg, strKCKXM, strSep, strKCKXMList) = False Then
                    GoTo errProc
                End If

                '�ڿɲ鿴�Ĳ�����
                strSQL = ""
                strSQL = strSQL + " select count(*) from ����_B_��֯���� " + vbCr
                strSQL = strSQL + " where ��֯���� in (" + strKCKXMList + ") " + vbCr
                strSQL = strSQL + " and '" + strCurUserBMDM + "' like rtrim(��֯����) + '%'" + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                Dim intCount As Integer
                intCount = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item(0), 0)
                If intCount > 0 Then
                    Exit Try
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '�ڿɲ鿴����Ա��
                strSQL = ""
                strSQL = strSQL + " select count(*) from ����_B_��Ա " + vbCr
                strSQL = strSQL + " where ��Ա���� in (" + strKCKXMList + ") " + vbCr
                strSQL = strSQL + " and   ��Ա���� = '" + strCurUser + "' " + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                intCount = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item(0), 0)
                If intCount > 0 Then
                    Exit Try
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '���ܲ鿴
                strNewName = strJJXSMC

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getDisplayName = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡָ�����Ӷ�Ӧ�������������
        '     strErrMsg             ����������򷵻ش�����Ϣ
        '     intJJXH               ���������
        '     strType               �����أ������������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getOpinionType( _
            ByRef strErrMsg As String, _
            ByVal intJJXH As Integer, _
            ByRef strType As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            getOpinionType = False
            strErrMsg = ""
            strType = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '����
                strSQL = ""
                strSQL = strSQL + " select �������� "
                strSQL = strSQL + " from ����_B_���� "
                strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "' "
                strSQL = strSQL + " and   ������� = " + intJJXH.ToString() + " "
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    Exit Try
                End If

                '����
                strType = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item("��������"), "")

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getOpinionType = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ��ʾ�������Ӧ������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objOpinionData       ��Ҫ��ʾ�������Ϣ
        '     strYJLX              ��Ҫ��ʾ���������(������еİ�������)
        '     strQSYJ              �����أ��������
        '     strBJYJ              �����أ�������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getOpinion( _
            ByRef strErrMsg As String, _
            ByVal objOpinionData As Xydc.Platform.Common.Data.FlowData, _
            ByVal strYJLX As String, _
            ByRef strQSYJ As String, _
            ByRef strBJYJ As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            getOpinion = False
            strQSYJ = ""
            strBJYJ = ""

            Try
                '�����Ϣ
                If objOpinionData Is Nothing Then
                    Exit Try
                End If
                If objOpinionData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_SHENPIYIJIAN) Is Nothing Then
                    Exit Try
                End If
                With objOpinionData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_SHENPIYIJIAN)
                    If .Rows.Count < 1 Then
                        Exit Try
                    End If
                End With
                If strYJLX Is Nothing Then strYJLX = ""
                strYJLX = strYJLX.Trim()

                Dim strFieldBLZL As String = Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIYIJIAN_BLZL
                With objOpinionData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_SHENPIYIJIAN)
                    .DefaultView.RowFilter = strFieldBLZL + " = '" + strYJLX + "'"
                    If .DefaultView.Count < 1 Then
                        Exit Try
                    End If
                End With

                '��������
                Dim strTempYJ As String = ""
                Dim strBJNR As String = ""
                Dim strBLYJ As String = ""
                Dim strBLRQ As String = ""
                Dim strJSR As String = ""
                Dim intCount As Integer
                Dim i As Integer
                With objOpinionData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_SHENPIYIJIAN).DefaultView
                    intCount = .Count
                    For i = 0 To intCount - 1 Step 1
                        '��ȡ��Ϣ
                        strJSR = objPulicParameters.getObjectValue(.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIYIJIAN_JSR), "")
                        strBLRQ = objPulicParameters.getObjectValue(.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIYIJIAN_BLRQ), "")
                        strBLYJ = objPulicParameters.getObjectValue(.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIYIJIAN_BLYJ), "")
                        strBJNR = objPulicParameters.getObjectValue(.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIYIJIAN_BJNR), "")
                        If strBLRQ <> "" Then
                            strBLRQ = Format(System.DateTime.Parse(strBLRQ), "yyyy-MM-dd")
                        End If

                        '��ǰ�������
                        strTempYJ = ""
                        If strBLYJ <> "" Then
                            strTempYJ = strTempYJ + strBLYJ + Chr(13) + Chr(10)
                            strTempYJ = strTempYJ + "    " + strJSR + "  " + strBLRQ + Chr(13) + Chr(10)
                        End If
                        '�������
                        If strTempYJ <> "" Then
                            If strQSYJ = "" Then
                                strQSYJ = strTempYJ
                            Else
                                strQSYJ = strQSYJ + strTempYJ
                            End If
                        End If

                        '��ǰ������
                        strTempYJ = ""
                        If strBJNR <> "" Then
                            strTempYJ = strTempYJ + strBJNR + Chr(13) + Chr(10)
                            strTempYJ = strTempYJ + "    " + strJSR + "  " + strBLRQ + Chr(13) + Chr(10)
                        End If
                        '�������
                        If strTempYJ <> "" Then
                            If strBJYJ = "" Then
                                strBJYJ = strTempYJ
                            Else
                                strBJYJ = strBJYJ + strTempYJ
                            End If
                        End If
                    Next
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            getOpinion = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡָ����ԱstrUserXM��strWTRί�д���ҵ��
        '     strErrMsg             ����������򷵻ش�����Ϣ
        '     strUserXM             ����Ա����
        '     strWTR                �����أ�ί����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getWeituoren( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByRef strWTR As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            getWeituoren = False
            strErrMsg = ""
            strWTR = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strTaskStatusYWCList As String = Me.FlowData.TaskStatusYWCList
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '��ȡstrUseXMδ������ķ�֪ͨ�����е�ί������Ϣ
                strSQL = ""
                strSQL = strSQL + " select * from ����_B_���� " + vbCr
                strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "' " + vbCr
                strSQL = strSQL + " and   rtrim(���ӱ�ʶ) like '__1__0%' " + vbCr
                strSQL = strSQL + " and   ������   = '" + strUserXM + "' " + vbCr
                strSQL = strSQL + " and   ����״̬ not in (" + strTaskStatusYWCList + ") " + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    Exit Try
                End If

                '����
                Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate
                Dim strName As String = ""
                Dim strTemp As String = ""
                Dim intCount As Integer
                Dim i As Integer
                With objDataSet.Tables(0)
                    intCount = .Rows.Count
                    For i = 0 To intCount - 1 Step 1
                        strName = ""
                        strName = objPulicParameters.getObjectValue(.Rows(i).Item("ί����"), "")
                        If strName <> "" Then
                            If strTemp = "" Then
                                strTemp = strName
                            Else
                                strTemp = strTemp + strSep + strName
                            End If
                        End If
                    Next
                End With
                strWTR = strTemp

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getWeituoren = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡstrUserXM�ܹ��鿴�Ĺ������ļ����ݼ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ���û�����
        '     strWhere             ����������
        '     objFileDataSet       �����ع������ļ����ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getWorkflowFileData( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByVal strWhere As String, _
            ByRef objFileDataSet As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objTempFileDataSet As Xydc.Platform.Common.Data.FlowData
            Dim strSQL As String

            getWorkflowFileData = False
            objFileDataSet = Nothing
            strErrMsg = ""

            Try
                '���
                If Me.IsInitialized = False Then
                    strErrMsg = "���󣺹���������û�г�ʼ����"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim
                If strUserXM = "" Then
                    strErrMsg = "����û�������û���ʶ��"
                    GoTo errProc
                End If
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim

                '��ȡ����
                objSqlConnection = Me.SqlConnection

                '�������ݼ�
                objTempFileDataSet = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_V_SHENPIWENJIAN_NEW)

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                'ִ�м���
                With Me.SqlDataAdapter
                    '������Ϣ
                    strSQL = ""
                    strSQL = strSQL + " select a.*" + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select a.*" + vbCr
                    strSQL = strSQL + "   from" + vbCr
                    strSQL = strSQL + "   (" + vbCr
                    strSQL = strSQL + "     select a.* " + vbCr
                    strSQL = strSQL + "     from ����_V_ȫ�������ļ��� a" + vbCr
                    If strWhere <> "" Then
                        strSQL = strSQL + "     where " + strWhere + vbCr
                    End If
                    strSQL = strSQL + "   ) a" + vbCr
                    strSQL = strSQL + "   left join" + vbCr
                    strSQL = strSQL + "   (" + vbCr
                    strSQL = strSQL + "     select �ļ���ʶ" + vbCr
                    strSQL = strSQL + "     from ����_B_����" + vbCr
                    strSQL = strSQL + "     where ((������ = '" + strUserXM + "' and rtrim(���ӱ�ʶ) like '_1%') " + vbCr
                    strSQL = strSQL + "     or     (������ = '" + strUserXM + "' and rtrim(���ӱ�ʶ) like '__1%')) " + vbCr
                    strSQL = strSQL + "     group by �ļ���ʶ" + vbCr
                    strSQL = strSQL + "   ) b on a.�ļ���ʶ = b.�ļ���ʶ" + vbCr
                    strSQL = strSQL + "   where b.�ļ���ʶ is not null" + vbCr
                    strSQL = strSQL + " ) a" + vbCr
                    strSQL = strSQL + " order by a.������� desc" + vbCr

                    '���ò���
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    'ִ�в���
                    .Fill(objTempFileDataSet.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_V_SHENPIWENJIAN_NEW))
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            objFileDataSet = objTempFileDataSet
            getWorkflowFileData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.FlowData.SafeRelease(objTempFileDataSet)
            Exit Function

        End Function




        '----------------------------------------------------------------
        ' �ļ��Ƿ��͹�?
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     blnHasSend           �������Ƿ��͹�?
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function isFileHasSend( _
            ByRef strErrMsg As String, _
            ByRef blnHasSend As Boolean) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            isFileHasSend = False
            blnHasSend = False
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '��ȡ����
                strSQL = ""
                strSQL = strSQL + " select * from ����_B_���� " + vbCr
                strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "' " + vbCr
                strSQL = strSQL + " and   rtrim(���ӱ�ʶ) like '1%' " + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    blnHasSend = True
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            isFileHasSend = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' strUserXM�Ƿ�Ϊ�Զ�ǩ���ļ���
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ���û�����
        '     blnAutoReceive       �������Ƿ�?
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function isAutoReceive( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByRef blnAutoReceive As Boolean) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            isAutoReceive = False
            blnAutoReceive = False
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '����Ա��
                strSQL = ""
                strSQL = strSQL + " select * from ����_B_��Ա " + vbCr
                strSQL = strSQL + " where ��Ա���� = '" + strUserXM + "'" + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    Exit Try
                End If

                '������Ϣ
                Dim strZDQS As String
                With objDataSet.Tables(0).Rows(0)
                    strZDQS = objPulicParameters.getObjectValue(.Item("�Զ�ǩ��"), "")
                End With
                Select Case strZDQS
                    Case Xydc.Platform.Common.Utilities.PulicParameters.CharTrue
                        blnAutoReceive = True
                    Case Else
                End Select

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            isAutoReceive = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �ļ��Ƿ��͹���
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     blnSendOnce          �������Ƿ�?
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function isFileSendOnce( _
            ByRef strErrMsg As String, _
            ByRef blnSendOnce As Boolean) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            isFileSendOnce = False
            blnSendOnce = False
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '���
                strSQL = ""
                strSQL = strSQL + " select * from ����_B_���� "
                strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "' "
                strSQL = strSQL + " and   rtrim(���ӱ�ʶ) like '1%' "
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    Exit Try
                End If

                '������Ϣ
                blnSendOnce = True

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            isFileSendOnce = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' strUserXM�Ƿ��յ�ֽ���ļ��Ľ��ӵ���(�ӡ�δ�����ˡ��м���)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ���û�����
        '     blnReceive           �������Ƿ�?
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function isReceiveZhizhi( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByRef blnReceive As Boolean) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            isReceiveZhizhi = False
            blnReceive = False
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strTaskStatusYWCList As String = Me.FlowData.TaskStatusYWCList
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '���
                strSQL = ""
                strSQL = strSQL + " select * from ����_B_���� " + vbCr
                strSQL = strSQL + " where �ļ���ʶ =  '" + strWJBS + "' " + vbCr                   '��ǰ�ļ�
                strSQL = strSQL + " and   ������   =  '" + strUserXM + "' " + vbCr                 '���յ�
                strSQL = strSQL + " and   rtrim(���ӱ�ʶ) like '__1%' " + vbCr                            '�������ܿ�
                strSQL = strSQL + " and   ����״̬ not in (" + strTaskStatusYWCList + ")" + vbCr   '������δ����
                strSQL = strSQL + " and  (����ֽ���ļ� > 0 or ����ֽ�ʸ��� > 0) " + vbCr           '��ֽ���ļ�
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    blnReceive = True
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            isReceiveZhizhi = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' strUserXM�Ƿ���ֽ���ļ��Ľ��ӵ���(�ӡ�δ�����ˡ��м���)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ���û�����
        '     blnSend              �������Ƿ�?
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function isSendZhizhi( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByRef blnSend As Boolean) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            isSendZhizhi = False
            blnSend = False
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strTaskStatusYWCList As String = Me.FlowData.TaskStatusYWCList
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '���
                strSQL = ""
                strSQL = strSQL + " select * from ����_B_���� " + vbCr
                strSQL = strSQL + " where �ļ���ʶ =  '" + strWJBS + "' " + vbCr                  '��ǰ�ļ�
                strSQL = strSQL + " and   ������   =  '" + strUserXM + "' " + vbCr                '���͵�
                strSQL = strSQL + " and   rtrim(���ӱ�ʶ) like '_1%' " + vbCr                            '�������ܿ�
                strSQL = strSQL + " and   ����״̬ not in (" + strTaskStatusYWCList + ")" + vbCr  '������δ����
                strSQL = strSQL + " and  (����ֽ���ļ� > 0 or ����ֽ�ʸ��� > 0) " + vbCr          '��ֽ���ļ�
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    blnSend = True
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            isSendZhizhi = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �ļ��Ƿ���ֽ���ļ�����ת��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     blnHas               �������Ƿ�?
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function isFileHasZhizhi( _
            ByRef strErrMsg As String, _
            ByRef blnHas As Boolean) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            isFileHasZhizhi = False
            blnHas = False
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection

                'Dim strBLLX As String = Me.FlowTypeName
                Dim strBLLX As String = FlowBLLXName

                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '���
                strSQL = ""
                strSQL = strSQL + " select * from ����_B_���� " + vbCr
                strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "' " + vbCr
                strSQL = strSQL + " and   �������� = '" + strBLLX + "' " + vbCr
                strSQL = strSQL + " and   rtrim(���ӱ�ʶ) like '1____0%' " + vbCr
                strSQL = strSQL + " and  (����ֽ���ļ� > 0 or ����ֽ�ʸ��� > 0) " + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    blnHas = True
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            isFileHasZhizhi = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' strUserXM�Ƿ��Ѿ��Ķ����������ݣ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ���û�����
        '     blnRead              �������Ƿ�?
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function isHasReadZWNR( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByRef blnRead As Boolean) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            isHasReadZWNR = False
            blnRead = False
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strTaskStatusYWCList As String = Me.FlowData.TaskStatusYWCList
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '���
                strSQL = ""
                strSQL = strSQL + " select sum(�Ƿ����) as �Ƿ���� " + vbCr
                strSQL = strSQL + " from ����_B_���� " + vbCr
                strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "' " + vbCr
                strSQL = strSQL + " and   ������   = '" + strUserXM + "' " + vbCr
                strSQL = strSQL + " and   ����״̬ not in (" + strTaskStatusYWCList + ") " + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    Exit Try
                End If

                '������Ϣ
                Dim intNum As Integer
                intNum = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item("�Ƿ����"), 0)
                If intNum > 0 Then
                    blnRead = True
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            isHasReadZWNR = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' strUserXM�Ƿ���δ���֪ͨ�����ˣ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ���û�����
        '     blnHas               �������Ƿ�?
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function isHasNotCompleteTongzhi( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByRef blnHas As Boolean) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            isHasNotCompleteTongzhi = False
            blnHas = False
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strTaskStatusYWCList As String = Me.FlowData.TaskStatusYWCList
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '���
                strSQL = ""
                strSQL = strSQL + " select * from ����_B_���� " + vbCr
                strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "' " + vbCr                   '��ǰ�ļ�
                strSQL = strSQL + " and   ������   = '" + strUserXM + "' " + vbCr                 '������
                strSQL = strSQL + " and   ����״̬ not in (" + strTaskStatusYWCList + ") " + vbCr '������δ���
                strSQL = strSQL + " and   rtrim(���ӱ�ʶ) like '__1__1%' " + vbCr                        '�������ܿ�+֪ͨ��
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    Exit Try
                End If

                '������Ϣ
                blnHas = True

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            isHasNotCompleteTongzhi = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �Ƿ��˻ص����ˣ�
        '     strTaskStatus        ������״̬
        ' ����
        '     True                 ����
        '     False                ����
        '----------------------------------------------------------------
        Public Overridable Function isTaskTuihui(ByVal strTaskStatus As String) As Boolean

            isTaskTuihui = False
            Try
                If strTaskStatus Is Nothing Then strTaskStatus = ""
                strTaskStatus = strTaskStatus.Trim()
                If strTaskStatus.Substring(3, 1) = "1" Then
                    isTaskTuihui = True
                End If
            Catch ex As Exception
            End Try

        End Function

        '----------------------------------------------------------------
        ' �Ƿ��ջص����ˣ�
        '     strTaskStatus        ������״̬
        ' ����
        '     True                 ����
        '     False                ����
        '----------------------------------------------------------------
        Public Overridable Function isTaskShouhui(ByVal strTaskStatus As String) As Boolean

            isTaskShouhui = False
            Try
                If strTaskStatus Is Nothing Then strTaskStatus = ""
                strTaskStatus = strTaskStatus.Trim()
                If strTaskStatus.Substring(4, 1) = "1" Then
                    isTaskShouhui = True
                End If
            Catch ex As Exception
            End Try

        End Function

        '----------------------------------------------------------------
        ' �Ƿ�Ϊ֪ͨ�����ˣ�
        '     strTaskStatus        ������״̬
        ' ����
        '     True                 ����
        '     False                ����
        '----------------------------------------------------------------
        Public Overridable Function isTaskTongzhi(ByVal strTaskStatus As String) As Boolean

            isTaskTongzhi = False
            Try
                If strTaskStatus Is Nothing Then strTaskStatus = ""
                strTaskStatus = strTaskStatus.Trim()
                If strTaskStatus.Substring(5, 1) = "1" Then
                    isTaskTongzhi = True
                End If
            Catch ex As Exception
            End Try

        End Function

        '----------------------------------------------------------------
        ' �Ƿ�Ϊ�ظ������ˣ�
        '     strTaskStatus        ������״̬
        ' ����
        '     True                 ����
        '     False                ����
        '----------------------------------------------------------------
        Public Overridable Function isTaskHuifu(ByVal strTaskStatus As String) As Boolean

            isTaskHuifu = False
            Try
                If strTaskStatus Is Nothing Then strTaskStatus = ""
                strTaskStatus = strTaskStatus.Trim()
                If strTaskStatus.Substring(6, 1) = "1" Then
                    isTaskHuifu = True
                End If
            Catch ex As Exception
            End Try

        End Function

        '----------------------------------------------------------------
        ' �Ƿ�Ϊ�������ˣ�
        '     strTaskBLZL          ����������
        ' ����
        '     True                 ����
        '     False                ����
        '----------------------------------------------------------------
        Public Overridable Function isTaskTingban(ByVal strTaskBLZL As String) As Boolean

            isTaskTingban = False
            Try
                If strTaskBLZL Is Nothing Then strTaskBLZL = ""
                strTaskBLZL = strTaskBLZL.Trim()
                Select Case strTaskBLZL
                    Case Me.FlowData.TASKSTATUS_YTB
                        isTaskTingban = True
                    Case Else
                End Select
            Catch ex As Exception
            End Try

        End Function

        '----------------------------------------------------------------
        ' �����Ƿ���ꣿ
        '     strTaskBLZT          ������״̬
        ' ����
        '     True                 ����
        '     False                ����
        '----------------------------------------------------------------
        Public Overridable Function isTaskComplete(ByVal strTaskBLZT As String) As Boolean

            isTaskComplete = False
            Try
                If strTaskBLZT Is Nothing Then strTaskBLZT = ""
                strTaskBLZT = strTaskBLZT.Trim()
                Select Case strTaskBLZT
                    Case Xydc.Platform.Common.Workflow.BaseFlowObject.TASKSTATUS_BSH, _
                        Xydc.Platform.Common.Workflow.BaseFlowObject.TASKSTATUS_BTH, _
                        Xydc.Platform.Common.Workflow.BaseFlowObject.TASKSTATUS_BYB, _
                        Xydc.Platform.Common.Workflow.BaseFlowObject.TASKSTATUS_YTB, _
                        Xydc.Platform.Common.Workflow.BaseFlowObject.TASKSTATUS_YWC, _
                        Xydc.Platform.Common.Workflow.BaseFlowObject.TASKSTATUS_YYD
                        isTaskComplete = True
                    Case Else
                End Select
            Catch ex As Exception
            End Try

        End Function




        '----------------------------------------------------------------
        ' �����ļ������ļ�����
        ' strUserId  = "" and blnLocked = false����������ļ��ķ���
        ' strUserId <> "" and blnLocked = false�����strUserId���ļ��ķ���
        ' blnLocked  = true ʱstrUserId <> ""
        '
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ����Ա����
        '     blnLocked            ��true-����,false-����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doLockFile( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal blnLocked As Boolean) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            doLockFile = False
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim()
                If blnLocked = True And strUserId = "" Then
                    strErrMsg = "����δָ�����ļ���������Ա��"
                    GoTo errProc
                End If

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '��ʼ����
                objSqlTransaction = objSqlConnection.BeginTransaction
                objSqlCommand.Transaction = objSqlTransaction

                '������
                Try
                    '�ļ�����
                    strSQL = ""
                    strSQL = strSQL + " delete from ����_B_�ļ����� " + vbCr
                    strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "'" + vbCr

                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.ExecuteNonQuery()

                    '�ļ�����
                    If blnLocked = True Then
                        strSQL = ""
                        strSQL = strSQL + " insert into ����_B_�ļ����� (" + vbCr
                        strSQL = strSQL + "   �ļ���ʶ,��Ա����" + vbCr
                        strSQL = strSQL + " ) values (" + vbCr
                        strSQL = strSQL + " '" + strWJBS + "','" + strUserId + "'" + vbCr
                        strSQL = strSQL + " )" + vbCr
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.ExecuteNonQuery()
                    End If

                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '�ύ����
                objSqlTransaction.Commit()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            doLockFile = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �����ļ������ļ�����
        ' strUserId  = "" and blnLocked = false����������ļ��ķ���
        ' strUserId <> "" and blnLocked = false�����strUserId���ļ��ķ���
        ' blnLocked  = true ʱstrUserId <> ""
        '
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objSqlTransaction    ����������
        '     strUserId            ����Ա����
        '     blnLocked            ��true-����,false-����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doLockFile( _
            ByRef strErrMsg As String, _
            ByVal objSqlTransaction As System.Data.SqlClient.SqlTransaction, _
            ByVal strUserId As String, _
            ByVal blnLocked As Boolean) As Boolean

            Dim objLocalSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim blnNewTrans As Boolean
            Dim strSQL As String

            doLockFile = False
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim()
                If blnLocked = True And strUserId = "" Then
                    strErrMsg = "����δָ�����ļ���������Ա��"
                    GoTo errProc
                End If

                '��ȡ�ļ���ʶ
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '��ȡ��������
                If objSqlTransaction Is Nothing Then
                    objSqlConnection = Me.SqlConnection
                Else
                    objSqlConnection = objSqlTransaction.Connection
                End If

                '������ѯ����
                objLocalSqlConnection = New System.Data.SqlClient.SqlConnection(objSqlConnection.ConnectionString)
                objLocalSqlConnection.Open()

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '��ʼ����
                If objSqlTransaction Is Nothing Then
                    blnNewTrans = True
                    objSqlTransaction = objSqlConnection.BeginTransaction
                Else
                    blnNewTrans = False
                End If
                objSqlCommand.Transaction = objSqlTransaction

                '������
                Try
                    '�ļ�����
                    strSQL = ""
                    strSQL = strSQL + " delete from ����_B_�ļ����� " + vbCr
                    strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "'" + vbCr

                    'If strUserId <> "" Then
                    '    strSQL = strSQL + " and ��Ա���� = '" + strUserId + "'" + vbCr
                    'End If
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.ExecuteNonQuery()

                    '�ļ�����
                    If blnLocked = True Then
                        strSQL = ""
                        strSQL = strSQL + " insert into ����_B_�ļ����� (" + vbCr
                        strSQL = strSQL + "   �ļ���ʶ,��Ա����" + vbCr
                        strSQL = strSQL + " ) values (" + vbCr
                        strSQL = strSQL + " '" + strWJBS + "','" + strUserId + "'" + vbCr
                        strSQL = strSQL + " )" + vbCr
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.ExecuteNonQuery()
                    End If

                Catch ex As Exception
                    If blnNewTrans = True Then
                        objSqlTransaction.Rollback()
                    End If
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '�ύ����
                If blnNewTrans = True Then
                    objSqlTransaction.Commit()
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objLocalSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            doLockFile = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objLocalSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �Զ������ļ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ����Ա����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doAutoReceive( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String
            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet

            doAutoReceive = False
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strTaskStatusWJSList As String = Me.FlowData.TaskStatusWJSList
                Dim strTaskStatusZJB As String = Me.FlowData.TASKSTATUS_ZJB

                'Dim strBLLX As String = Me.FlowTypeName
                Dim strBLLX As String = Me.FlowBLLXName

                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '����δ���յĽ��ӵ�
                strSQL = ""
                strSQL = strSQL + " select * from ����_B_���� " + vbCr
                strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "' " + vbCr
                strSQL = strSQL + " and   �������� = '" + strBLLX + "' " + vbCr
                strSQL = strSQL + " and   ������   = '" + strUserXM + "' " + vbCr
                strSQL = strSQL + " and   ����״̬ in (" + strTaskStatusWJSList + ") " + vbCr
                strSQL = strSQL + " order by ������" + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    Exit Try
                End If

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '��ʼ����
                objSqlTransaction = objSqlConnection.BeginTransaction
                objSqlCommand.Transaction = objSqlTransaction

                '������
                Dim strTemp(2) As String
                Dim intCount As Integer
                Dim i As Integer
                intCount = objDataSet.Tables(0).Rows.Count
                Try
                    For i = 0 To intCount - 1 Step 1
                        '��ȡ����
                        With objDataSet.Tables(0)
                            strTemp(0) = objPulicParameters.getObjectValue(.Rows(i).Item("�ļ���ʶ"), "")
                            strTemp(1) = objPulicParameters.getObjectValue(.Rows(i).Item("�������"), "")
                        End With

                        '׼��SQL
                        strSQL = ""
                        strSQL = strSQL + " update ����_B_���� set " + vbCr
                        strSQL = strSQL + "   ��������     = '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "'," + vbCr
                        strSQL = strSQL + "   ����ֽ���ļ� = ����ֽ���ļ�," + vbCr
                        strSQL = strSQL + "   ���յ����ļ� = ���͵����ļ�," + vbCr
                        strSQL = strSQL + "   ����ֽ�ʸ��� = ����ֽ�ʸ���," + vbCr
                        strSQL = strSQL + "   ���յ��Ӹ��� = ���͵��Ӹ���," + vbCr
                        strSQL = strSQL + "   ����״̬     = '" + strTaskStatusZJB + "' " + vbCr
                        strSQL = strSQL + " where �ļ���ʶ = '" + strTemp(0) + "' " + vbCr
                        strSQL = strSQL + " and   ������� =  " + strTemp(1) + " " + vbCr

                        'ִ��
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.ExecuteNonQuery()
                    Next

                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '�ύ����
                objSqlTransaction.Commit()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            doAutoReceive = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' strSender��strReceiver���Ͳ��Ľ��ӵ������Զ������Ѿ��Ķ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objSqlTransaction    ����������
        '     strSender            ��������Ա����
        '     strReceiver          ��������Ա����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doSendBuyueJJD( _
            ByRef strErrMsg As String, _
            ByVal objSqlTransaction As System.Data.SqlClient.SqlTransaction, _
            ByVal strSender As String, _
            ByVal strReceiver As String) As Boolean

            Dim objLocalSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim blnNewTrans As Boolean
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            doSendBuyueJJD = False
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strSender Is Nothing Then strSender = ""
                strSender = strSender.Trim()
                If strReceiver Is Nothing Then strReceiver = ""
                strReceiver = strReceiver.Trim()
                If strReceiver = "" Or strSender = "" Then
                    strErrMsg = "����δָ��[������]��[������]��"
                    GoTo errProc
                End If

                '��ȡ�ļ���Ϣ
                Dim strTaskStatusYYD As String = Me.FlowData.TASKSTATUS_YYD
                Dim strBYTZ As String = Me.FlowData.TASK_BYTZ
                Dim strBLLX As String = Me.FlowBLLXName
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '��ȡ��������
                If objSqlTransaction Is Nothing Then
                    objSqlConnection = Me.SqlConnection
                Else
                    objSqlConnection = objSqlTransaction.Connection
                End If

                '������ѯ����
                objLocalSqlConnection = New System.Data.SqlClient.SqlConnection(objSqlConnection.ConnectionString)
                objLocalSqlConnection.Open()

                '��ȡ�½��ӵ���
                Dim strJJXH As String
                If objdacCommon.getNewCode(strErrMsg, objLocalSqlConnection, "�������", "�ļ���ʶ", strWJBS, "����_B_����", True, strJJXH) = False Then
                    GoTo errProc
                End If
                '�����µķ������
                Dim strFSXH As String
                If objdacCommon.getNewCode(strErrMsg, objLocalSqlConnection, "�������", "�ļ���ʶ", strWJBS, "����_B_����", True, strFSXH) = False Then
                    GoTo errProc
                End If
                '����������
                Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate
                Dim strRelaFields As String = "�ļ���ʶ" + strSep + "�������"
                Dim strRelaValue As String = strWJBS + strSep + strFSXH
                Dim strJSXH As String
                If objdacCommon.getNewCode(strErrMsg, objLocalSqlConnection, "�������", strRelaFields, strRelaValue, "����_B_����", True, strJSXH) = False Then
                    GoTo errProc
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objLocalSqlConnection)

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '��ʼ����
                If objSqlTransaction Is Nothing Then
                    blnNewTrans = True
                    objSqlTransaction = objSqlConnection.BeginTransaction
                Else
                    blnNewTrans = False
                End If
                objSqlCommand.Transaction = objSqlTransaction

                '������
                Dim intZDBY As Integer = Xydc.Platform.Common.Data.FlowData.YJJH_ZHUDONGBUYUE
                Try
                    '�ύ�µĲ��Ľ��ӵ�(��������)
                    strSQL = ""
                    strSQL = strSQL + " insert into ����_B_���� (" + vbCr
                    strSQL = strSQL + "   �ļ���ʶ," + vbCr
                    strSQL = strSQL + "   �������," + vbCr
                    strSQL = strSQL + "   ԭ���Ӻ�," + vbCr
                    strSQL = strSQL + "   �������," + vbCr
                    strSQL = strSQL + "   ������," + vbCr
                    strSQL = strSQL + "   ��������," + vbCr
                    strSQL = strSQL + "   �������," + vbCr
                    strSQL = strSQL + "   ������," + vbCr
                    strSQL = strSQL + "   ��������," + vbCr
                    strSQL = strSQL + "   �����������," + vbCr
                    strSQL = strSQL + "   �������," + vbCr
                    strSQL = strSQL + "   ��������," + vbCr
                    strSQL = strSQL + "   ��������," + vbCr
                    strSQL = strSQL + "   ����״̬," + vbCr
                    '�Զ�����ʱ,��ע���ϵͳ�ڲ�����
                    ' 
                    'strSQL = strSQL + "   ���ӱ�ʶ" + vbCr
                    strSQL = strSQL + "   ���ӱ�ʶ," + vbCr
                    strSQL = strSQL + "   ���ӱ�ע " + vbCr
                    ' 
                    strSQL = strSQL + " ) values (" + vbCr
                    strSQL = strSQL + " '" + strWJBS + "'," + vbCr
                    strSQL = strSQL + "  " + strJJXH + " ," + vbCr
                    strSQL = strSQL + "  " + intZDBY.ToString() + " ," + vbCr
                    strSQL = strSQL + "  " + strFSXH + " ," + vbCr
                    strSQL = strSQL + " '" + strSender + "'," + vbCr
                    strSQL = strSQL + " '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "'," + vbCr
                    strSQL = strSQL + "  " + strJSXH + "," + vbCr
                    strSQL = strSQL + " '" + strReceiver + "'," + vbCr
                    strSQL = strSQL + " '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "'," + vbCr
                    strSQL = strSQL + " '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "'," + vbCr
                    strSQL = strSQL + " '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "'," + vbCr
                    strSQL = strSQL + " '" + strBLLX + "'," + vbCr
                    strSQL = strSQL + " '" + strBYTZ + "'," + vbCr
                    strSQL = strSQL + " '" + strTaskStatusYYD + "'," + vbCr
                    '�Զ�����ʱ,��ע���ϵͳ�ڲ�����
                    ' 
                    'strSQL = strSQL + " '" + "10100100" + "'" + vbCr
                    strSQL = strSQL + " '" + "10100100" + "'," + vbCr
                    strSQL = strSQL + " '" + "ϵͳ�Զ�����" + "'" + vbCr
                    ' 
                    strSQL = strSQL + " )" + vbCr

                    'ִ��
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    If blnNewTrans = True Then
                        objSqlTransaction.Rollback()
                    End If
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '�ύ����
                If blnNewTrans = True Then
                    objSqlTransaction.Commit()
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objLocalSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            doSendBuyueJJD = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objLocalSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �����ʼ��������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objSqlTransaction    ����������
        '     strSender            ��������Ա����
        '     strReceiver          ��������Ա����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doSaveInitJJD( _
            ByRef strErrMsg As String, _
            ByVal objSqlTransaction As System.Data.SqlClient.SqlTransaction, _
            ByVal strSender As String, _
            ByVal strReceiver As String) As Boolean

            Dim objLocalSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim blnNewTrans As Boolean
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            doSaveInitJJD = False
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strSender Is Nothing Then strSender = ""
                strSender = strSender.Trim()
                If strReceiver Is Nothing Then strReceiver = ""
                strReceiver = strReceiver.Trim()
                If strReceiver = "" Or strSender = "" Then
                    strErrMsg = "����δָ��[������]��[������]��"
                    GoTo errProc
                End If

                '��ȡ�ļ���Ϣ
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try
                Dim strBLLX As String = Me.FlowBLLXName
                Dim strInitTask As String = Me.getInitTask()
                Dim strTaskStatusZJB As String = Me.FlowData.TASKSTATUS_ZJB

                '��ȡ��������
                If objSqlTransaction Is Nothing Then
                    objSqlConnection = Me.SqlConnection
                Else
                    objSqlConnection = objSqlTransaction.Connection
                End If

                '������ѯ����
                objLocalSqlConnection = New System.Data.SqlClient.SqlConnection(objSqlConnection.ConnectionString)
                objLocalSqlConnection.Open()

                '��ȡ�½��ӵ���
                Dim strJJXH As String
                If objdacCommon.getNewCode(strErrMsg, objLocalSqlConnection, "�������", "�ļ���ʶ", strWJBS, "����_B_����", True, strJJXH) = False Then
                    GoTo errProc
                End If
                '�����µķ������
                Dim strFSXH As String
                If objdacCommon.getNewCode(strErrMsg, objLocalSqlConnection, "�������", "�ļ���ʶ", strWJBS, "����_B_����", True, strFSXH) = False Then
                    GoTo errProc
                End If
                '����������
                Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate
                Dim strRelaFields As String = "�ļ���ʶ" + strSep + "�������"
                Dim strRelaValue As String = strWJBS + strSep + strFSXH
                Dim strJSXH As String
                If objdacCommon.getNewCode(strErrMsg, objLocalSqlConnection, "�������", strRelaFields, strRelaValue, "����_B_����", True, strJSXH) = False Then
                    GoTo errProc
                End If

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '��ʼ����
                If objSqlTransaction Is Nothing Then
                    objSqlTransaction = objSqlConnection.BeginTransaction
                    blnNewTrans = True
                Else
                    blnNewTrans = False
                End If
                objSqlCommand.Transaction = objSqlTransaction

                '������
                Try
                    strSQL = ""
                    strSQL = strSQL + " insert into ����_B_���� (" + vbCr
                    strSQL = strSQL + "   �ļ���ʶ, �������, ԭ���Ӻ�, �������, ������, ��������," + vbCr
                    strSQL = strSQL + "   ����ֽ���ļ�,���͵����ļ�,����ֽ�ʸ���,���͵��Ӹ���," + vbCr
                    strSQL = strSQL + "   �������,������,��������,����ֽ���ļ�,���յ����ļ�," + vbCr
                    strSQL = strSQL + "   ����ֽ�ʸ���,���յ��Ӹ���,��������,��������," + vbCr
                    strSQL = strSQL + "   ����״̬,���ӱ�ʶ" + vbCr
                    strSQL = strSQL + " ) values (" + vbCr
                    strSQL = strSQL + " '" + strWJBS + "'," + vbCr
                    strSQL = strSQL + " '" + strJJXH + "'," + vbCr
                    strSQL = strSQL + " '0'," + vbCr
                    strSQL = strSQL + " '" + strFSXH + "'," + vbCr
                    strSQL = strSQL + " '" + strSender + "'," + vbCr
                    strSQL = strSQL + " '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "'," + vbCr
                    strSQL = strSQL + " 0,1,0,1,1," + vbCr
                    strSQL = strSQL + " '" + strReceiver + "'," + vbCr
                    strSQL = strSQL + " '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "'," + vbCr
                    strSQL = strSQL + " 0,1,0,1," + vbCr
                    strSQL = strSQL + " '" + strBLLX + "'," + vbCr
                    strSQL = strSQL + " '" + strInitTask + "'," + vbCr
                    strSQL = strSQL + " '" + strTaskStatusZJB + "'," + vbCr
                    strSQL = strSQL + " '01100000'" + vbCr        '��ʼ����״̬
                    strSQL = strSQL + " )" + vbCr

                    'ִ��
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    If blnNewTrans = True Then
                        objSqlTransaction.Rollback()
                    End If
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '�ύ����
                If blnNewTrans = True Then
                    objSqlTransaction.Commit()
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objLocalSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            doSaveInitJJD = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objLocalSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' д�ļ�������־
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objSqlTransaction    ����������
        '     strUserXM            ��������Ա����
        '     strCZSM              ���������˵��
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doWriteFileLogo( _
            ByRef strErrMsg As String, _
            ByVal objSqlTransaction As System.Data.SqlClient.SqlTransaction, _
            ByVal strUserXM As String, _
            ByVal strCZSM As String) As Boolean

            Dim objLocalSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim blnNewTrans As Boolean
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            doWriteFileLogo = False
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()
                If strCZSM Is Nothing Then strCZSM = ""
                strCZSM = strCZSM.Trim()
                If strUserXM = "" Then
                    strErrMsg = "����δָ��[������Ա]��"
                    GoTo errProc
                End If

                '��ȡ�ļ�����
                Dim strTaskStatusYYD As String = Me.FlowData.TASKSTATUS_YYD
                Dim strBYTZ As String = Me.FlowData.TASK_BYTZ
                Dim strBLLX As String = Me.FlowTypeName
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '��ȡ��������
                If objSqlTransaction Is Nothing Then
                    objSqlConnection = Me.SqlConnection
                Else
                    objSqlConnection = objSqlTransaction.Connection
                End If

                '�򿪲�ѯ����
                objLocalSqlConnection = New System.Data.SqlClient.SqlConnection(objSqlConnection.ConnectionString)
                objLocalSqlConnection.Open()

                '��ȡ�������
                Dim strCZXH As String
                If objdacCommon.getNewCode(strErrMsg, objLocalSqlConnection, "�������", "�ļ���ʶ", strWJBS, "����_B_������־", True, strCZXH) = False Then
                    GoTo errProc
                End If

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '��ʼ����
                If objSqlTransaction Is Nothing Then
                    blnNewTrans = True
                    objSqlTransaction = objSqlConnection.BeginTransaction
                Else
                    blnNewTrans = False
                End If
                objSqlCommand.Transaction = objSqlTransaction

                '������
                Try
                    '��¼
                    strSQL = ""
                    strSQL = strSQL + " insert into ����_B_������־ (" + vbCr
                    strSQL = strSQL + "   �ļ���ʶ,�������,������,����ʱ��,����˵��" + vbCr
                    strSQL = strSQL + " ) values (" + vbCr
                    strSQL = strSQL + "'" + strWJBS + "'," + vbCr
                    strSQL = strSQL + " " + strCZXH + " ," + vbCr
                    strSQL = strSQL + "'" + strUserXM + "'," + vbCr
                    strSQL = strSQL + "'" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "'," + vbCr
                    strSQL = strSQL + "'" + strCZSM + "' " + vbCr
                    strSQL = strSQL + ")" + vbCr

                    'ִ��
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    If blnNewTrans = True Then
                        objSqlTransaction.Rollback()
                    End If
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '�ύ����
                If blnNewTrans = True Then
                    objSqlTransaction.Commit()
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objLocalSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            doWriteFileLogo = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objLocalSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��顰����_B_���ӡ������ݵĺϷ���
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objOldData           ��������
        '     objNewData           ��������
        '     objenumEditType      ���༭����
        ' ����
        '     True                 ���Ϸ�
        '     False                �����Ϸ��������������
        ' �޸ļ�¼
        '      ����
        '----------------------------------------------------------------
        Public Overridable Function doVerifyData_Jiaojie( _
            ByRef strErrMsg As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByRef objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objListDictionary As System.Collections.Specialized.ListDictionary = Nothing
            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Nothing
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet = Nothing
            Dim strWJBS As String = ""
            Dim intLen As Integer = 0
            Dim strSQL As String = ""

            doVerifyData_Jiaojie = False

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "����δ�����µ����ݣ�"
                    GoTo errProc
                End If
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew, _
                        Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eCpyNew
                    Case Else
                        If objOldData Is Nothing Then
                            strErrMsg = "����δ����ɵ����ݣ�"
                            GoTo errProc
                        End If
                End Select

                '��ȡ��Ϣ
                objSqlConnection = Me.SqlConnection
                strWJBS = Me.WJBS

                '��ȡ��ṹ����
                strSQL = "select top 0 * from ����_B_����"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, objSqlConnection, strSQL, "����_B_����", objDataSet) = False Then
                    GoTo errProc
                End If

                '������ݳ���
                Dim intCount As Integer = 0
                Dim strValue As String = ""
                Dim strField As String = ""
                Dim i As Integer = 0
                intCount = objNewData.Count
                For i = 0 To intCount - 1 Step 1
                    strField = objNewData.GetKey(i).Trim
                    strValue = objNewData.Item(i).Trim
                    Select Case strField
                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JJXH
                            '�Զ���
                            If strValue = "" Then
                                Dim intJJXH As Integer = 0
                                If Me.getMaxJJXH(strErrMsg, intJJXH) = False Then
                                    GoTo errProc
                                End If
                                strValue = (intJJXH + 1).ToString
                            End If
                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_WJBS
                            '�Զ���
                            If strValue = "" Then
                                strValue = strWJBS
                            End If

                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_YJJH, _
                            Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSXH
                            If strValue = "" Then
                                strErrMsg = "����û������[" + strField + "]��"
                                GoTo errProc
                            End If
                            If objPulicParameters.isFloatString(strValue) = False Then
                                strErrMsg = "����[" + strValue + "]����Ч����ֵ��"
                                GoTo errProc
                            End If
                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSZZWJ, _
                            Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSDZWJ, _
                            Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSZZFJ, _
                            Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSDZFJ, _
                            Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSZZWJ, _
                            Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSDZWJ, _
                            Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSZZFJ, _
                            Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSDZFJ, _
                            Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSXH, _
                            Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_SFDG, _
                            Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_BWTX
                            If strValue <> "" Then
                                If objPulicParameters.isIntegerString(strValue) = False Then
                                    strErrMsg = "����[" + strValue + "]����Ч�����֣�"
                                    GoTo errProc
                                End If
                            End If

                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSRQ
                            If strValue = "" Then
                                strErrMsg = "����û������[" + strField + "]��"
                                GoTo errProc
                            End If
                            If objPulicParameters.isDatetimeString(strValue) = False Then
                                strErrMsg = "����[" + strValue + "]����Ч�����ڣ�"
                                GoTo errProc
                            End If
                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSRQ, _
                            Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_BLZHQX, _
                            Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_WCRQ
                            If strValue <> "" Then
                                If objPulicParameters.isDatetimeString(strValue) = False Then
                                    strErrMsg = "����[" + strValue + "]����Ч�����ڣ�"
                                    GoTo errProc
                                End If
                            End If

                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSR
                            If strValue = "" Then
                                strErrMsg = "����û������[" + strField + "]��"
                                GoTo errProc
                            End If
                            With objDataSet.Tables(0).Columns(strField)
                                intLen = objPulicParameters.getStringLength(strValue)
                                If intLen > .MaxLength Then
                                    strErrMsg = "����[" + strField + "]���Ȳ��ܳ���[" + .MaxLength.ToString() + "]��ʵ����[" + intLen.ToString() + "]��"
                                    GoTo errProc
                                End If
                            End With
                        Case Else
                            If strValue <> "" Then
                                With objDataSet.Tables(0).Columns(strField)
                                    intLen = objPulicParameters.getStringLength(strValue)
                                    If intLen > .MaxLength Then
                                        strErrMsg = "����[" + strField + "]���Ȳ��ܳ���[" + .MaxLength.ToString() + "]��ʵ����[" + intLen.ToString() + "]��"
                                        GoTo errProc
                                    End If
                                End With
                            End If
                    End Select
                    objNewData(strField) = strValue
                Next
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)

                '���Լ��
                Dim intNewJJXH As Integer = objPulicParameters.getObjectValue(objNewData.Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JJXH), 0)
                objListDictionary = New System.Collections.Specialized.ListDictionary
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                        strSQL = "select * from ����_B_���� where �ļ���ʶ = @wjbs and ������� = @jjxh"
                        objListDictionary.Add("@wjbs", strWJBS)
                        objListDictionary.Add("@jjxh", intNewJJXH)
                    Case Else
                        Dim intOldJJXH As Integer = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JJXH), 0)
                        strSQL = "select * from ����_B_���� where �ļ���ʶ = @wjbs and ������� = @jjxh and ������� <> @oldjjxh"
                        objListDictionary.Add("@wjbs", strWJBS)
                        objListDictionary.Add("@jjxh", intNewJJXH)
                        objListDictionary.Add("@oldjjxh", intOldJJXH)
                End Select
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objListDictionary, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strErrMsg = "����[" + intNewJJXH.ToString + "]�Ѿ����ڣ�"
                    GoTo errProc
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objListDictionary)
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objListDictionary)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            doVerifyData_Jiaojie = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objListDictionary)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ����strUserXM�Ѿ��Ķ���ָ���ļ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ��������Ա����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doSetHasReadFile( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            doSetHasReadFile = False
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()
                If strUserXM = "" Then
                    strErrMsg = "����δָ��[������Ա]��"
                    GoTo errProc
                End If

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strTaskStatusYWCList As String = Me.FlowData.TaskStatusYWCList
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '��ʼ����
                objSqlTransaction = objSqlConnection.BeginTransaction
                objSqlCommand.Transaction = objSqlTransaction

                '������
                Try
                    '��¼
                    strSQL = ""
                    strSQL = strSQL + " update ����_B_���� set " + vbCr
                    strSQL = strSQL + "   �Ƿ���� = 1 " + vbCr
                    strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "' " + vbCr
                    strSQL = strSQL + " and   ������   = '" + strUserXM + "' " + vbCr
                    strSQL = strSQL + " and   ����״̬ not in (" + strTaskStatusYWCList + ") " + vbCr
                    strSQL = strSQL + " and   �Ƿ���� <> 1" + vbCr

                    'ִ��
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '�ύ����
                objSqlTransaction.Commit()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            doSetHasReadFile = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �жϸ�����¼�����Ƿ���Ч��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objNewData           ����¼��ֵ(�����Ƽ�ֵ)
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doVerifyFujian( _
            ByRef strErrMsg As String, _
            ByRef objNewData As System.Collections.Specialized.NameValueCollection) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            doVerifyFujian = False

            Try
                '���
                If Me.IsInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "����δ�����µ����ݣ�"
                    GoTo errProc
                End If

                '��ȡ������Ϣ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection

                '��ȡ��ṹ����
                strSQL = "select top 0 * from ����_B_����"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, objSqlConnection, strSQL, "����_B_����", objDataSet) = False Then
                    GoTo errProc
                End If

                '������ݳ���
                Dim intCount As Integer = objNewData.Count
                Dim strField As String
                Dim strValue As String
                Dim intLen As Integer
                Dim i As Integer
                For i = 0 To intCount - 1 Step 1
                    strField = objNewData.GetKey(i).Trim()
                    strValue = objNewData.Item(i).Trim()
                    Select Case strField
                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_BDWJ, _
                            Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_XZBZ, _
                            Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_XSXH
                            '��ʾ�ֶΣ����ô���

                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_WJXH
                            If strValue = "" Then
                                strErrMsg = "����[" + strField + "]����Ϊ�գ�"
                                GoTo errProc
                            End If
                            If objPulicParameters.isIntegerString(strValue) = False Then
                                strErrMsg = "����[" + strField + "]���������֣�"
                                GoTo errProc
                            End If
                            intLen = CType(strValue, Integer)
                            If intLen < 1 Or intLen > 999999 Then
                                strErrMsg = "����[" + strField + "]������[1,999999]��"
                                GoTo errProc
                            End If
                            strValue = intLen.ToString()

                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_WJSM
                            If strValue = "" Then
                                strErrMsg = "����[" + strField + "]����Ϊ�գ�"
                                GoTo errProc
                            End If
                            With objDataSet.Tables(0).Columns(strField)
                                intLen = objPulicParameters.getStringLength(strValue)
                                If intLen > .MaxLength Then
                                    strErrMsg = "����[" + strField + "]���Ȳ��ܳ���[" + .MaxLength.ToString() + "]��ʵ����[" + intLen.ToString() + "]��"
                                    GoTo errProc
                                End If
                            End With

                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_WJYS
                            If strValue = "" Then strValue = "1"
                            If objPulicParameters.isIntegerString(strValue) = False Then
                                strErrMsg = "����[" + strField + "]���������֣�"
                                GoTo errProc
                            End If
                            intLen = CType(strValue, Integer)
                            If intLen < 1 Or intLen > 999999 Then
                                strErrMsg = "����[" + strField + "]������[1,999999]��"
                                GoTo errProc
                            End If
                            strValue = intLen.ToString()

                        Case Else
                            If strValue <> "" Then
                                With objDataSet.Tables(0).Columns(strField)
                                    intLen = objPulicParameters.getStringLength(strValue)
                                    If intLen > .MaxLength Then
                                        strErrMsg = "����[" + strField + "]���Ȳ��ܳ���[" + .MaxLength.ToString() + "]��ʵ����[" + intLen.ToString() + "]��"
                                        GoTo errProc
                                    End If
                                End With
                            End If
                    End Select

                    objNewData(strField) = strValue
                Next
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            doVerifyFujian = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �ж�����ļ��ĸ�����¼�����Ƿ���Ч��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objNewData           ����¼��ֵ(�����Ƽ�ֵ)
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doVerifyXgwjFujian( _
            ByRef strErrMsg As String, _
            ByRef objNewData As System.Collections.Specialized.NameValueCollection) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            doVerifyXgwjFujian = False

            Try
                '���
                If Me.IsInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "����δ�����µ����ݣ�"
                    GoTo errProc
                End If

                '��ȡ������Ϣ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection

                '��ȡ��ṹ����
                strSQL = "select top 0 * from ����_B_����ļ�����"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, objSqlConnection, strSQL, "����_B_����ļ�����", objDataSet) = False Then
                    GoTo errProc
                End If

                '������ݳ���
                Dim intCount As Integer = objNewData.Count
                Dim strField As String
                Dim strValue As String
                Dim intLen As Integer
                Dim i As Integer
                For i = 0 To intCount - 1 Step 1
                    strField = objNewData.GetKey(i).Trim()
                    strValue = objNewData.Item(i).Trim()
                    Select Case strField
                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_XIANGGUANWENJIANFUJIAN_BDWJ, _
                            Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_XIANGGUANWENJIANFUJIAN_XZBZ, _
                            Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_XIANGGUANWENJIANFUJIAN_XSXH
                            '��ʾ�ֶΣ����ô���

                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_XIANGGUANWENJIANFUJIAN_WJXH
                            If strValue = "" Then
                                strErrMsg = "����[" + strField + "]����Ϊ�գ�"
                                GoTo errProc
                            End If
                            If objPulicParameters.isIntegerString(strValue) = False Then
                                strErrMsg = "����[" + strField + "]���������֣�"
                                GoTo errProc
                            End If
                            intLen = CType(strValue, Integer)
                            If intLen < 1 Or intLen > 999999 Then
                                strErrMsg = "����[" + strField + "]������[1,999999]��"
                                GoTo errProc
                            End If
                            strValue = intLen.ToString()

                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_XIANGGUANWENJIANFUJIAN_WJSM
                            If strValue = "" Then
                                strErrMsg = "����[" + strField + "]����Ϊ�գ�"
                                GoTo errProc
                            End If
                            With objDataSet.Tables(0).Columns(strField)
                                intLen = objPulicParameters.getStringLength(strValue)
                                If intLen > .MaxLength Then
                                    strErrMsg = "����[" + strField + "]���Ȳ��ܳ���[" + .MaxLength.ToString() + "]��ʵ����[" + intLen.ToString() + "]��"
                                    GoTo errProc
                                End If
                            End With

                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_XIANGGUANWENJIANFUJIAN_WJYS
                            If strValue = "" Then strValue = "1"
                            If objPulicParameters.isIntegerString(strValue) = False Then
                                strErrMsg = "����[" + strField + "]���������֣�"
                                GoTo errProc
                            End If
                            intLen = CType(strValue, Integer)
                            If intLen < 1 Or intLen > 999999 Then
                                strErrMsg = "����[" + strField + "]������[1,999999]��"
                                GoTo errProc
                            End If
                            strValue = intLen.ToString()

                        Case Else
                            If strValue <> "" Then
                                With objDataSet.Tables(0).Columns(strField)
                                    intLen = objPulicParameters.getStringLength(strValue)
                                    If intLen > .MaxLength Then
                                        strErrMsg = "����[" + strField + "]���Ȳ��ܳ���[" + .MaxLength.ToString() + "]��ʵ����[" + intLen.ToString() + "]��"
                                        GoTo errProc
                                    End If
                                End With
                            End If

                    End Select

                    objNewData(strField) = strValue
                Next
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            doVerifyXgwjFujian = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���ݱ����ļ���ȡFTP�������ļ�������
        ' ��������������ļ���ʶ+�ļ���չ��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strLocalFile         �������ļ���
        '     intWJND              ���ļ����
        '     strWJBS              ���ļ���ʶ
        '     strBasePath          ������Ŀ¼����Ŀ¼
        '     strRemoteFile        ������FTP�������ļ�·��
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getFTPFileName_GJ( _
            ByRef strErrMsg As String, _
            ByVal strLocalFile As String, _
            ByVal intWJND As Integer, _
            ByVal strWJBS As String, _
            ByVal strBasePath As String, _
            ByRef strRemoteFile As String) As Boolean

            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile

            getFTPFileName_GJ = False
            strRemoteFile = ""

            Try
                '���
                If strLocalFile Is Nothing Then strLocalFile = ""
                strLocalFile = strLocalFile.Trim()
                If strLocalFile = "" Then
                    Exit Try
                End If
                If strWJBS Is Nothing Then strWJBS = ""
                strWJBS = strWJBS.Trim()
                If strWJBS = "" Then
                    Exit Try
                End If
                If strBasePath Is Nothing Then strBasePath = ""
                strBasePath = strBasePath.Trim

                '��ȡ�ļ���
                Dim strFileName As String = ""
                Dim strFileExt As String = ""
                strFileExt = objBaseLocalFile.getExtension(strLocalFile)

                '��������������ļ���ʶ+�ļ���չ��
                strFileName = strWJBS + strFileExt
                strFileName = objBaseLocalFile.doMakePath(intWJND.ToString(), strFileName)

                '����Ŀ¼+�ļ�
                strFileName = objBaseLocalFile.doMakePath(strBasePath, strFileName)

                '����
                strRemoteFile = strFileName

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)

            getFTPFileName_GJ = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���ݱ����ļ���ȡFTP�������ļ�������
        ' �ļ����������������ļ���ʶ-FJ-���
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strLocalFile         �������ļ���
        '     intWJND              ���ļ����
        '     strWJBS              ���ļ���ʶ
        '     intXH                �����
        '     strBasePath          ������Ŀ¼����Ŀ¼
        '     strRemoteFile        ������FTP�������ļ�·��
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getFTPFileName_FJ( _
            ByRef strErrMsg As String, _
            ByVal strLocalFile As String, _
            ByVal intWJND As Integer, _
            ByVal strWJBS As String, _
            ByVal intXH As Integer, _
            ByVal strBasePath As String, _
            ByRef strRemoteFile As String) As Boolean

            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile

            getFTPFileName_FJ = False
            strRemoteFile = ""

            Try
                '���
                If strLocalFile Is Nothing Then strLocalFile = ""
                strLocalFile = strLocalFile.Trim()
                If strLocalFile = "" Then
                    Exit Try
                End If
                If strWJBS Is Nothing Then strWJBS = ""
                strWJBS = strWJBS.Trim()
                If strWJBS = "" Then
                    Exit Try
                End If
                If strBasePath Is Nothing Then strBasePath = ""
                strBasePath = strBasePath.Trim

                '��ȡ�ļ���
                Dim strFileName As String = ""
                Dim strFileExt As String = ""
                strFileExt = objBaseLocalFile.getExtension(strLocalFile)

                '�ļ����������������ļ���ʶ-FJ-���
                strFileName = strWJBS + "-FJ-" + intXH.ToString() + strFileExt
                strFileName = objBaseLocalFile.doMakePath(intWJND.ToString(), strFileName)

                '����Ŀ¼+�ļ�
                strFileName = objBaseLocalFile.doMakePath(strBasePath, strFileName)

                '����
                strRemoteFile = strFileName

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)

            getFTPFileName_FJ = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���ݱ����ļ���ȡFTP�������ļ�������
        ' ����ļ����������������ļ���ʶ-XGFJ-���
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strLocalFile         �������ļ���
        '     intWJND              ���ļ����
        '     strWJBS              ���ļ���ʶ
        '     intXH                �����
        '     strBasePath          ������Ŀ¼����Ŀ¼
        '     strRemoteFile        ������FTP�������ļ�·��
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getFTPFileName_XGWJFJ( _
            ByRef strErrMsg As String, _
            ByVal strLocalFile As String, _
            ByVal intWJND As Integer, _
            ByVal strWJBS As String, _
            ByVal intXH As Integer, _
            ByVal strBasePath As String, _
            ByRef strRemoteFile As String) As Boolean

            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile

            getFTPFileName_XGWJFJ = False
            strRemoteFile = ""

            Try
                '���
                If strLocalFile Is Nothing Then strLocalFile = ""
                strLocalFile = strLocalFile.Trim()
                If strLocalFile = "" Then
                    Exit Try
                End If
                If strWJBS Is Nothing Then strWJBS = ""
                strWJBS = strWJBS.Trim()
                If strWJBS = "" Then
                    Exit Try
                End If
                If strBasePath Is Nothing Then strBasePath = ""
                strBasePath = strBasePath.Trim

                '��ȡ�ļ���
                Dim strFileName As String = ""
                Dim strFileExt As String = ""
                strFileExt = objBaseLocalFile.getExtension(strLocalFile)

                '�ļ����������������ļ���ʶ-XGFJ-���
                strFileName = strWJBS + "-XGFJ-" + intXH.ToString() + strFileExt
                strFileName = objBaseLocalFile.doMakePath(intWJND.ToString(), strFileName)

                '����Ŀ¼+�ļ�
                strFileName = objBaseLocalFile.doMakePath(strBasePath, strFileName)

                '����
                strRemoteFile = strFileName

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)

            getFTPFileName_XGWJFJ = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���ݸ���ļ�
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strGJFTPSpec           ������ļ�����FTP·��
        '     objFTPProperty         ��FTP���Ӳ���
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doBackupFiles_GJ( _
            ByRef strErrMsg As String, _
            ByVal strGJFTPSpec As String, _
            ByVal objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty) As Boolean

            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objBaseFTP As New Xydc.Platform.Common.Utilities.BaseFTP

            Dim strBakExt As String = Xydc.Platform.Common.Utilities.PulicParameters.BACKUPFILEEXT

            doBackupFiles_GJ = False
            strErrMsg = ""

            Try
                '���
                If strGJFTPSpec Is Nothing Then strGJFTPSpec = ""
                strGJFTPSpec = strGJFTPSpec.Trim
                If strGJFTPSpec = "" Then
                    Exit Try
                End If
                If objFTPProperty Is Nothing Then
                    strErrMsg = "����δָ��FTP���������Ӳ�����"
                    GoTo errProc
                End If

                '����
                Dim strOldFile As String = strGJFTPSpec
                Dim blnExisted As Boolean
                Dim strFileName As String
                Dim strUrl As String
                If strOldFile <> "" Then
                    With objFTPProperty
                        strUrl = .getUrl(strOldFile)
                        If objBaseFTP.isFileExisted(strErrMsg, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword, blnExisted) = False Then
                            '���Բ��ɹ����������ļ�������
                        Else
                            If blnExisted = True Then
                                strFileName = objBaseLocalFile.getFileName(strOldFile) + strBakExt
                                If objBaseFTP.doRenameFile(strErrMsg, strUrl, strFileName, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword, True) = False Then
                                    GoTo errproc
                                End If
                            End If
                        End If
                    End With
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)

            doBackupFiles_GJ = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���ݸ����ļ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objFTPProperty       ��FTP����������
        '     objFJData            ����������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doBackupFiles_FJ( _
            ByRef strErrMsg As String, _
            ByVal objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty, _
            ByVal objFJData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim strBakExt As String = Xydc.Platform.Common.Utilities.PulicParameters.BACKUPFILEEXT
            Dim strTable As String = Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_FUJIAN

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objBaseFTP As New Xydc.Platform.Common.Utilities.BaseFTP

            doBackupFiles_FJ = False
            strErrMsg = ""

            Try
                If objFTPProperty Is Nothing Then
                    strErrMsg = "����δ����FTP������������"
                    GoTo errProc
                End If
                If objFJData Is Nothing Then
                    Exit Try
                End If
                If objFJData.Tables(strTable) Is Nothing Then
                    Exit Try
                End If

                '����ԭ�ļ�
                Dim blnExisted As Boolean
                Dim strFileName As String
                Dim strOldFile As String
                Dim strUrl As String
                Dim intCount As Integer
                Dim i As Integer
                With objFJData.Tables(strTable)
                    intCount = .DefaultView.Count
                    For i = intCount - 1 To 0 Step -1
                        strOldFile = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_WJWZ), "")
                        If strOldFile <> "" Then
                            With objFTPProperty
                                strUrl = .getUrl(strOldFile)
                                If objBaseFTP.isFileExisted(strErrMsg, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword, blnExisted) = False Then
                                    '���Բ��ɹ����������ļ�������
                                Else
                                    If blnExisted = True Then
                                        strFileName = objBaseLocalFile.getFileName(strOldFile) + strBakExt
                                        If objBaseFTP.doRenameFile(strErrMsg, strUrl, strFileName, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword, True) = False Then
                                            GoTo errProc
                                        End If
                                    End If
                                End If
                            End With
                        End If
                    Next
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)

            doBackupFiles_FJ = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��������ļ������ļ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objFTPProperty       ��FTP����������
        '     objXGWJFJData        ������ļ���������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doBackupFiles_XGWJFJ( _
            ByRef strErrMsg As String, _
            ByVal objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty, _
            ByVal objXGWJFJData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_XIANGGUANWENJIANFUJIAN
            Dim strBakExt As String = Xydc.Platform.Common.Utilities.PulicParameters.BACKUPFILEEXT

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objBaseFTP As New Xydc.Platform.Common.Utilities.BaseFTP

            doBackupFiles_XGWJFJ = False
            strErrMsg = ""

            Try
                If objFTPProperty Is Nothing Then
                    strErrMsg = "����δ����FTP������������"
                    GoTo errProc
                End If
                If objXGWJFJData Is Nothing Then
                    Exit Try
                End If
                If objXGWJFJData.Tables(strTable) Is Nothing Then
                    Exit Try
                End If

                '����ԭ�ļ�
                Dim blnExisted As Boolean
                Dim strFileName As String
                Dim strOldFile As String
                Dim strUrl As String
                Dim intCount As Integer
                Dim i As Integer
                With objXGWJFJData.Tables(strTable)
                    intCount = .DefaultView.Count
                    For i = intCount - 1 To 0 Step -1
                        strOldFile = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_XIANGGUANWENJIANFUJIAN_WJWZ), "")
                        If strOldFile <> "" Then
                            With objFTPProperty
                                strUrl = .getUrl(strOldFile)
                                If objBaseFTP.isFileExisted(strErrMsg, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword, blnExisted) = False Then
                                    '���Բ��ɹ����������ļ�������
                                Else
                                    If blnExisted = True Then
                                        strFileName = objBaseLocalFile.getFileName(strOldFile) + strBakExt
                                        If objBaseFTP.doRenameFile(strErrMsg, strUrl, strFileName, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword, True) = False Then
                                            GoTo errProc
                                        End If
                                    End If
                                End If
                            End With
                        End If
                    Next
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)

            doBackupFiles_XGWJFJ = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �ӱ����лָ�����ļ�
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strGJFTPSpec           ������ļ���ԭFTP·��
        '     objFTPProperty         ��FTP���Ӳ���
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doRestoreFiles_GJ( _
            ByRef strErrMsg As String, _
            ByVal strGJFTPSpec As String, _
            ByVal objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty) As Boolean

            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objBaseFTP As New Xydc.Platform.Common.Utilities.BaseFTP

            Dim strBakExt As String = Xydc.Platform.Common.Utilities.PulicParameters.BACKUPFILEEXT

            doRestoreFiles_GJ = False
            strErrMsg = ""

            Try
                '���
                If strGJFTPSpec Is Nothing Then strGJFTPSpec = ""
                strGJFTPSpec = strGJFTPSpec.Trim
                If strGJFTPSpec = "" Then
                    Exit Try
                End If
                If objFTPProperty Is Nothing Then
                    strErrMsg = "����δָ��FTP���������Ӳ�����"
                    GoTo errProc
                End If

                '����
                Dim strOldFile As String = strGJFTPSpec
                Dim blnExisted As Boolean
                Dim strToUrl As String
                Dim strUrl As String
                If strOldFile <> "" Then
                    With objFTPProperty
                        strUrl = .getUrl(strOldFile + strBakExt)
                        If objBaseFTP.isFileExisted(strErrMsg, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword, blnExisted) = False Then
                            '���Բ��ɹ����������ļ�������
                        Else
                            If blnExisted = True Then
                                strToUrl = .getUrl(strOldFile)
                                objBaseFTP.doRenameFile(strErrMsg, strUrl, strToUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword)
                            End If
                        End If
                    End With
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)

            doRestoreFiles_GJ = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �ӱ��ݻ��������ļ��лָ�ԭ�����ļ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strWJBS              ���ļ���ʶ
        '     intWJND              �����ļ���ŵ����
        '     objFTPProperty       ��FTP����������
        '     objNewData           ���¸�������
        '     objOldData           ��ԭ��������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doRestoreFiles_FJ( _
            ByRef strErrMsg As String, _
            ByVal strWJBS As String, _
            ByVal intWJND As Integer, _
            ByVal objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty, _
            ByVal objNewData As Xydc.Platform.Common.Data.FlowData, _
            ByVal objOldData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim strBakExt As String = Xydc.Platform.Common.Utilities.PulicParameters.BACKUPFILEEXT
            Dim strTable As String = Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_FUJIAN

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objBaseFTP As New Xydc.Platform.Common.Utilities.BaseFTP

            doRestoreFiles_FJ = False
            strErrMsg = ""

            Try
                If objFTPProperty Is Nothing Then
                    strErrMsg = "����δ����FTP������������"
                    GoTo errProc
                End If
                If objOldData Is Nothing Then
                    Exit Try
                End If
                If objOldData.Tables(strTable) Is Nothing Then
                    Exit Try
                End If

                '���ȴӱ����ļ��ع�
                Dim strBasePath As String = Me.getBasePath_FJ()
                Dim blnExisted As Boolean
                Dim strNewWJWZ As String
                Dim strOldWJWZ As String
                Dim strNewFile As String
                Dim strOldFile As String
                Dim strToUrl As String
                Dim strUrl As String
                Dim blnDo As Boolean
                Dim intCountA As Integer
                Dim intCount As Integer
                Dim i As Integer
                Dim j As Integer
                With objOldData.Tables(strTable)
                    intCount = .DefaultView.Count
                    For i = intCount - 1 To 0 Step -1
                        strOldFile = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_WJWZ), "")
                        strOldWJWZ = strOldFile.ToUpper
                        If strOldFile <> "" Then
                            With objFTPProperty
                                '�ȴӱ����лָ�
                                strUrl = .getUrl(strOldFile + strBakExt)
                                If objBaseFTP.isFileExisted(strErrMsg, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword, blnExisted) = False Then
                                    blnExisted = False
                                End If
                                If blnExisted = True Then
                                    '�����ļ����ڣ���ӱ����ļ��о����ָܻ�
                                    strToUrl = .getUrl(strOldFile)
                                    objBaseFTP.doRenameFile(strErrMsg, strUrl, strToUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword)
                                Else
                                    '�����ļ������ڣ����鱸���ļ��Ƿ��Ѹ���Ϊ��Ӧ�����ļ���
                                    If Not (objNewData Is Nothing) Then
                                        blnDo = False
                                        With objNewData.Tables(strTable)
                                            intCountA = .DefaultView.Count
                                            For j = 0 To intCountA - 1 Step 1
                                                strNewWJWZ = objPulicParameters.getObjectValue(.DefaultView.Item(j).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_WJWZ), "")
                                                If strOldWJWZ = strNewWJWZ.ToUpper Then
                                                    '��ȡ��Ӧ�����ļ�
                                                    If Me.getFTPFileName_FJ(strErrMsg, strOldFile, intWJND, strWJBS, j + 1, strBasePath, strNewFile) = False Then
                                                        blnDo = False
                                                    Else
                                                        blnDo = True
                                                    End If
                                                    Exit For
                                                End If
                                            Next
                                        End With
                                        If blnDo = True Then
                                            strUrl = .getUrl(strNewFile)
                                            If objBaseFTP.isFileExisted(strErrMsg, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword, blnExisted) = False Then
                                                blnExisted = False
                                            End If
                                            If blnExisted = True Then
                                                '�Ѿ����ļ����ڣ���ִ�д����ļ��о����ָܻ�
                                                strToUrl = .getUrl(strOldFile)
                                                objBaseFTP.doRenameFile(strErrMsg, strUrl, strToUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword)
                                            End If
                                        End If
                                    End If
                                End If
                            End With
                        End If
                    Next
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)

            doRestoreFiles_FJ = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �ӱ��ݻ��������ļ��лָ�ԭ����ļ������ļ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strWJBS              ���ļ���ʶ
        '     intWJND              �����ļ���ŵ����
        '     objFTPProperty       ��FTP����������
        '     objNewData           ��������ļ���������
        '     objOldData           ��ԭ����ļ���������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doRestoreFiles_XGWJFJ( _
            ByRef strErrMsg As String, _
            ByVal strWJBS As String, _
            ByVal intWJND As Integer, _
            ByVal objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty, _
            ByVal objNewData As Xydc.Platform.Common.Data.FlowData, _
            ByVal objOldData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_XIANGGUANWENJIANFUJIAN
            Dim strBakExt As String = Xydc.Platform.Common.Utilities.PulicParameters.BACKUPFILEEXT

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objBaseFTP As New Xydc.Platform.Common.Utilities.BaseFTP

            doRestoreFiles_XGWJFJ = False
            strErrMsg = ""

            Try
                If objFTPProperty Is Nothing Then
                    strErrMsg = "����δ����FTP������������"
                    GoTo errProc
                End If
                If objOldData Is Nothing Then
                    Exit Try
                End If
                If objOldData.Tables(strTable) Is Nothing Then
                    Exit Try
                End If

                '���ȴӱ����ļ��ع�
                Dim strBasePath As String = Me.getBasePath_XGWJFJ()
                Dim blnExisted As Boolean
                Dim strNewWJWZ As String
                Dim strOldWJWZ As String
                Dim strNewFile As String
                Dim strOldFile As String
                Dim strToUrl As String
                Dim strUrl As String
                Dim blnDo As Boolean
                Dim intCountA As Integer
                Dim intCount As Integer
                Dim i As Integer
                Dim j As Integer
                With objOldData.Tables(strTable)
                    intCount = .DefaultView.Count
                    For i = intCount - 1 To 0 Step -1
                        strOldFile = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_XIANGGUANWENJIANFUJIAN_WJWZ), "")
                        strOldWJWZ = strOldFile.ToUpper
                        If strOldFile <> "" Then
                            With objFTPProperty
                                '�ȴӱ����лָ�
                                strUrl = .getUrl(strOldFile + strBakExt)
                                If objBaseFTP.isFileExisted(strErrMsg, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword, blnExisted) = False Then
                                    blnExisted = False
                                End If
                                If blnExisted = True Then
                                    '�����ļ����ڣ���ӱ����ļ��о����ָܻ�
                                    strToUrl = .getUrl(strOldFile)
                                    objBaseFTP.doRenameFile(strErrMsg, strUrl, strToUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword)
                                Else
                                    '�����ļ������ڣ����鱸���ļ��Ƿ��Ѹ���Ϊ��Ӧ�����ļ���
                                    If Not (objNewData Is Nothing) Then
                                        blnDo = False
                                        With objNewData.Tables(strTable)
                                            intCountA = .DefaultView.Count
                                            For j = 0 To intCountA - 1 Step 1
                                                strNewWJWZ = objPulicParameters.getObjectValue(.DefaultView.Item(j).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_XIANGGUANWENJIANFUJIAN_WJWZ), "")
                                                If strOldWJWZ = strNewWJWZ.ToUpper Then
                                                    '��ȡ��Ӧ�����ļ�
                                                    If Me.getFTPFileName_XGWJFJ(strErrMsg, strOldFile, intWJND, strWJBS, j + 1, strBasePath, strNewFile) = False Then
                                                        blnDo = False
                                                    Else
                                                        blnDo = True
                                                    End If
                                                    Exit For
                                                End If
                                            Next
                                        End With
                                        If blnDo = True Then
                                            strUrl = .getUrl(strNewFile)
                                            If objBaseFTP.isFileExisted(strErrMsg, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword, blnExisted) = False Then
                                                blnExisted = False
                                            End If
                                            If blnExisted = True Then
                                                '�Ѿ����ļ����ڣ���ִ�д����ļ��о����ָܻ�
                                                strToUrl = .getUrl(strOldFile)
                                                objBaseFTP.doRenameFile(strErrMsg, strUrl, strToUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword)
                                            End If
                                        End If
                                    End If
                                End If
                            End With
                        End If
                    Next
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)

            doRestoreFiles_XGWJFJ = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ɾ����������ļ�
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strGJFTPSpec           ������ļ���ԭFTP·��
        '     objFTPProperty         ��FTP���Ӳ���
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doDeleteBackupFiles_GJ( _
            ByRef strErrMsg As String, _
            ByVal strGJFTPSpec As String, _
            ByVal objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty) As Boolean

            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objBaseFTP As New Xydc.Platform.Common.Utilities.BaseFTP

            Dim strBakExt As String = Xydc.Platform.Common.Utilities.PulicParameters.BACKUPFILEEXT

            doDeleteBackupFiles_GJ = False
            strErrMsg = ""

            Try
                '���
                If strGJFTPSpec Is Nothing Then strGJFTPSpec = ""
                strGJFTPSpec = strGJFTPSpec.Trim
                If strGJFTPSpec = "" Then
                    Exit Try
                End If
                If objFTPProperty Is Nothing Then
                    strErrMsg = "����δָ��FTP���������Ӳ�����"
                    GoTo errProc
                End If

                'ɾ������
                Dim strOldFile As String = strGJFTPSpec
                Dim strUrl As String
                If strOldFile <> "" Then
                    With objFTPProperty
                        strUrl = .getUrl(strOldFile + strBakExt)
                        If objBaseFTP.doDeleteFile(strErrMsg, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword) = False Then
                            '���Բ��ɹ�,�γ���������
                        End If
                    End With
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)

            doDeleteBackupFiles_GJ = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ɾ�������ı����ļ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objFTPProperty       ��FTP����������
        '     objFJData            ����������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doDeleteBackupFiles_FJ( _
            ByRef strErrMsg As String, _
            ByVal objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty, _
            ByVal objFJData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim strBakExt As String = Xydc.Platform.Common.Utilities.PulicParameters.BACKUPFILEEXT
            Dim strTable As String = Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_FUJIAN

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseFTP As New Xydc.Platform.Common.Utilities.BaseFTP

            doDeleteBackupFiles_FJ = False
            strErrMsg = ""

            Try
                If objFTPProperty Is Nothing Then
                    strErrMsg = "����δ����FTP������������"
                    GoTo errProc
                End If
                If objFJData Is Nothing Then
                    Exit Try
                End If
                If objFJData.Tables(strTable) Is Nothing Then
                    Exit Try
                End If

                Dim strOldFile As String
                Dim intCount As Integer
                Dim strUrl As String
                Dim i As Integer
                With objFJData.Tables(strTable)
                    intCount = .DefaultView.Count
                    For i = intCount - 1 To 0 Step -1
                        strOldFile = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_WJWZ), "")
                        If strOldFile <> "" Then
                            With objFTPProperty
                                strUrl = .getUrl(strOldFile + strBakExt)
                                If objBaseFTP.doDeleteFile(strErrMsg, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword) = False Then
                                    '���Բ��ɹ�,�γ���������
                                End If
                            End With
                        End If
                    Next
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)

            doDeleteBackupFiles_FJ = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ɾ������ļ������ı����ļ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objFTPProperty       ��FTP����������
        '     objXGWJFJData        ������ļ���������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doDeleteBackupFiles_XGWJFJ( _
            ByRef strErrMsg As String, _
            ByVal objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty, _
            ByVal objXGWJFJData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_XIANGGUANWENJIANFUJIAN
            Dim strBakExt As String = Xydc.Platform.Common.Utilities.PulicParameters.BACKUPFILEEXT

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseFTP As New Xydc.Platform.Common.Utilities.BaseFTP

            doDeleteBackupFiles_XGWJFJ = False
            strErrMsg = ""

            Try
                If objFTPProperty Is Nothing Then
                    strErrMsg = "����δ����FTP������������"
                    GoTo errProc
                End If
                If objXGWJFJData Is Nothing Then
                    Exit Try
                End If
                If objXGWJFJData.Tables(strTable) Is Nothing Then
                    Exit Try
                End If

                Dim strOldFile As String
                Dim intCount As Integer
                Dim strUrl As String
                Dim i As Integer
                With objXGWJFJData.Tables(strTable)
                    intCount = .DefaultView.Count
                    For i = intCount - 1 To 0 Step -1
                        strOldFile = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_XIANGGUANWENJIANFUJIAN_WJWZ), "")
                        If strOldFile <> "" Then
                            With objFTPProperty
                                strUrl = .getUrl(strOldFile + strBakExt)
                                If objBaseFTP.doDeleteFile(strErrMsg, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword) = False Then
                                    '���Բ��ɹ�,�γ���������
                                End If
                            End With
                        End If
                    Next
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)

            doDeleteBackupFiles_XGWJFJ = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���渽������
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strWJBS                ���ļ���ʶ
        '     intWJND                �����ļ���ŵ����
        '     objSqlTransaction      ����������
        '     objFTPProperty         ��FTP����������
        '     objNewData             ����¼��ֵ(���ر�������ֵ)
        '     objOldData             ����¼��ֵ
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doSaveFujian( _
            ByRef strErrMsg As String, _
            ByVal strWJBS As String, _
            ByVal intWJND As Integer, _
            ByVal objSqlTransaction As System.Data.SqlClient.SqlTransaction, _
            ByVal objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty, _
            ByRef objNewData As Xydc.Platform.Common.Data.FlowData, _
            ByVal objOldData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            Dim strBakExt As String = Xydc.Platform.Common.Utilities.PulicParameters.BACKUPFILEEXT
            Dim strTable As String = Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_FUJIAN
            Dim blnNewTrans As Boolean = False
            Dim strSQL As String

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objBaseFTP As New Xydc.Platform.Common.Utilities.BaseFTP

            '��ʼ��
            doSaveFujian = False
            strErrMsg = ""

            Try
                '���
                If Me.IsInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "����δ�����µ����ݣ�"
                    GoTo errProc
                End If
                If objFTPProperty Is Nothing Then
                    strErrMsg = "����δ����FTP������������"
                    GoTo errProc
                End If
                If strWJBS Is Nothing Then strWJBS = ""
                strWJBS = strWJBS.Trim
                If strWJBS = "" Then
                    Exit Try
                End If

                '��ȡ������Ϣ
                If objSqlTransaction Is Nothing Then
                    objSqlConnection = Me.SqlConnection
                Else
                    objSqlConnection = objSqlTransaction.Connection
                End If

                '��ʼ����
                If objSqlTransaction Is Nothing Then
                    blnNewTrans = True
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Else
                    blnNewTrans = False
                End If

                '��������
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ɾ��������_B_����������
                    strSQL = ""
                    strSQL = strSQL + " delete from ����_B_���� " + vbCr
                    strSQL = strSQL + " where �ļ���ʶ = @wjbs" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    Try
                        '��Դ�ļ���ͬĿ¼�н��ļ�����
                        If Me.doBackupFiles_FJ(strErrMsg, objFTPProperty, objOldData) = False Then
                            GoTo rollDatabaseAndFile
                        End If

                        '����������
                        Dim strBasePath As String = Me.getBasePath_FJ
                        Dim blnExisted As Boolean
                        Dim strOldFile As String
                        Dim strLocFile As String
                        Dim strNewFile As String
                        Dim strToUrl As String
                        Dim strUrl As String
                        Dim intCount As Integer
                        Dim i As Integer
                        With objNewData.Tables(strTable)
                            intCount = .DefaultView.Count
                            For i = 0 To intCount - 1 Step 1
                                '��ȡԭFTP·�����±����ļ�·��
                                strOldFile = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_WJWZ), "")
                                strLocFile = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_BDWJ), "")
                                strNewFile = ""
                                '�ϴ��ļ�
                                If strLocFile <> "" Then
                                    '�ļ�����?
                                    If objBaseLocalFile.doFileExisted(strErrMsg, strLocFile, blnExisted) = False Then
                                        GoTo rollDatabaseAndFile
                                    End If
                                    If blnExisted = True Then
                                        '��ȡFTP�ļ�·��
                                        If Me.getFTPFileName_FJ(strErrMsg, strLocFile, intWJND, strWJBS, i + 1, strBasePath, strNewFile) = False Then
                                            GoTo rollDatabaseAndFile
                                        End If
                                        '�б����ļ�������Ҫ����
                                        With objFTPProperty
                                            strUrl = .getUrl(strNewFile)
                                            If objBaseFTP.doPutFile(strErrMsg, strLocFile, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword) = False Then
                                                GoTo rollDatabaseAndFile
                                            End If
                                        End With
                                    Else
                                        strErrMsg = "����[" + strLocFile + "]�����ڣ�"
                                        GoTo rollDatabaseAndFile
                                    End If
                                Else
                                    If strOldFile <> "" Then
                                        '
                                        'δ��FTP����������
                                        '
                                        '�ӱ����ļ��ָ�����ǰ�е��ļ�
                                        With objFTPProperty
                                            strUrl = .getUrl(strOldFile + strBakExt)
                                            If objBaseFTP.isFileExisted(strErrMsg, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword, blnExisted) = False Then
                                                '���Բ��ɹ�
                                            Else
                                                If blnExisted = True Then
                                                    '��ȡFTP�ļ�·��
                                                    If Me.getFTPFileName_FJ(strErrMsg, strOldFile, intWJND, strWJBS, i + 1, strBasePath, strNewFile) = False Then
                                                        GoTo rollDatabaseAndFile
                                                    End If
                                                    strToUrl = .getUrl(strNewFile)
                                                    '�����ļ���
                                                    If objBaseFTP.doRenameFile(strErrMsg, strUrl, strToUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword) = False Then
                                                        GoTo rollDatabaseAndFile
                                                    End If
                                                End If
                                            End If
                                        End With
                                    Else
                                        'û�е����ļ�
                                    End If
                                End If

                                'д����
                                strSQL = ""
                                strSQL = strSQL + " insert into ����_B_���� (" + vbCr
                                strSQL = strSQL + "   �ļ���ʶ, ���, ˵��, ҳ��, λ��" + vbCr
                                strSQL = strSQL + " ) values (" + vbCr
                                strSQL = strSQL + "   @wjbs, @wjxh, @wjsm, @wjys, @wjwz" + vbCr
                                strSQL = strSQL + " )" + vbCr
                                objSqlCommand.Parameters.Clear()
                                objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                                objSqlCommand.Parameters.AddWithValue("@wjxh", (i + 1))
                                objSqlCommand.Parameters.AddWithValue("@wjsm", objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_WJSM), ""))
                                objSqlCommand.Parameters.AddWithValue("@wjys", objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_WJYS), 0))
                                objSqlCommand.Parameters.AddWithValue("@wjwz", strNewFile)
                                objSqlCommand.CommandText = strSQL
                                objSqlCommand.ExecuteNonQuery()
                            Next
                        End With

                        'ɾ�����б����ļ�
                        If blnNewTrans = True Then
                            If Me.doDeleteBackupFiles_FJ(strErrMsg, objFTPProperty, objOldData) = False Then
                                '���Բ��ɹ����γ������ļ���
                            End If
                        End If

                    Catch ex As Exception
                        strErrMsg = ex.Message
                        GoTo rollDatabaseAndFile
                    End Try

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo rollDatabase
                End Try

                '�ύ����
                If blnNewTrans = True Then
                    objSqlTransaction.Commit()
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)

            '����
            doSaveFujian = True
            Exit Function

rollDatabaseAndFile:
            If blnNewTrans = True Then
                objSqlTransaction.Rollback()
                If Me.doRestoreFiles_FJ(strSQL, strWJBS, intWJND, objFTPProperty, objNewData, objOldData) = False Then
                    '�޷��ָ��ɹ��������ˣ�
                End If
            End If
            GoTo errProc

rollDatabase:
            If blnNewTrans = True Then
                objSqlTransaction.Rollback()
            End If
            GoTo errProc

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���渽������
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     blnEnforeEdit          ���Ƿ�ǿ���޸�
        '     strUserXM              ������Ա����
        '     objNewData             ����¼��ֵ(���ر�������ֵ)
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doSaveFujian( _
            ByRef strErrMsg As String, _
            ByVal blnEnforeEdit As Boolean, _
            ByVal strUserXM As String, _
            ByRef objNewData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objOldData As Xydc.Platform.Common.Data.FlowData

            Dim strBakExt As String = Xydc.Platform.Common.Utilities.PulicParameters.BACKUPFILEEXT
            Dim strTable As String = Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_FUJIAN
            Dim intWJND As Integer = Year(Now)
            Dim strWJBS As String
            Dim strSQL As String

            Dim objdacXitongpeizhi As New Xydc.Platform.DataAccess.dacXitongpeizhi
            Dim objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objBaseFTP As New Xydc.Platform.Common.Utilities.BaseFTP

            '��ʼ��
            doSaveFujian = False
            strErrMsg = ""

            Try
                '���
                If Me.IsInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "����δ�����µ����ݣ�"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim

                '��ȡ������Ϣ
                objSqlConnection = Me.SqlConnection
                strWJBS = Me.WJBS
                If strWJBS Is Nothing Then strWJBS = ""
                strWJBS = strWJBS.Trim
                If strWJBS = "" Then
                    Exit Try
                End If

                '��ȡԭ��������
                If Me.getFujianData(strErrMsg, objOldData) = False Then
                    GoTo errProc
                End If

                '��ȡFTP���Ӳ���
                If objdacXitongpeizhi.getFtpServerParam(strErrMsg, objSqlConnection, objFTPProperty) = False Then
                    GoTo errProc
                End If

                '��ʼ����
                objSqlTransaction = objSqlConnection.BeginTransaction()

                '��������
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ɾ��������_B_����������
                    strSQL = ""
                    strSQL = strSQL + " delete from ����_B_���� " + vbCr
                    strSQL = strSQL + " where �ļ���ʶ = @wjbs" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    Try
                        '��Դ�ļ���ͬĿ¼�н��ļ�����
                        If Me.doBackupFiles_FJ(strErrMsg, objFTPProperty, objOldData) = False Then
                            GoTo rollDatabaseAndFile
                        End If

                        '����������
                        Dim strBasePath As String = Me.getBasePath_FJ
                        Dim blnExisted As Boolean
                        Dim strOldFile As String
                        Dim strLocFile As String
                        Dim strNewFile As String
                        Dim strToUrl As String
                        Dim strUrl As String
                        Dim intCount As Integer
                        Dim i As Integer
                        With objNewData.Tables(strTable)
                            intCount = .DefaultView.Count
                            For i = 0 To intCount - 1 Step 1
                                '��ȡԭFTP·�����±����ļ�·��
                                strOldFile = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_WJWZ), "")
                                strLocFile = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_BDWJ), "")
                                strNewFile = ""
                                '�ϴ��ļ�
                                If strLocFile <> "" Then
                                    '�ļ�����?
                                    If objBaseLocalFile.doFileExisted(strErrMsg, strLocFile, blnExisted) = False Then
                                        GoTo rollDatabaseAndFile
                                    End If
                                    If blnExisted = True Then
                                        '��ȡFTP�ļ�·��
                                        If Me.getFTPFileName_FJ(strErrMsg, strLocFile, intWJND, strWJBS, i + 1, strBasePath, strNewFile) = False Then
                                            GoTo rollDatabaseAndFile
                                        End If
                                        '�б����ļ�������Ҫ����
                                        With objFTPProperty
                                            strUrl = .getUrl(strNewFile)
                                            If objBaseFTP.doPutFile(strErrMsg, strLocFile, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword) = False Then
                                                GoTo rollDatabaseAndFile
                                            End If
                                        End With
                                    Else
                                        strErrMsg = "����[" + strLocFile + "]�����ڣ�"
                                        GoTo rollDatabaseAndFile
                                    End If
                                Else
                                    If strOldFile <> "" Then
                                        '
                                        'δ��FTP����������
                                        '
                                        '�ӱ����ļ��ָ�����ǰ�е��ļ�
                                        With objFTPProperty
                                            strUrl = .getUrl(strOldFile + strBakExt)
                                            If objBaseFTP.isFileExisted(strErrMsg, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword, blnExisted) = False Then
                                                '���Բ��ɹ�
                                            Else
                                                If blnExisted = True Then
                                                    '��ȡFTP�ļ�·��
                                                    If Me.getFTPFileName_FJ(strErrMsg, strOldFile, intWJND, strWJBS, i + 1, strBasePath, strNewFile) = False Then
                                                        GoTo rollDatabaseAndFile
                                                    End If
                                                    strToUrl = .getUrl(strNewFile)
                                                    '�����ļ���
                                                    If objBaseFTP.doRenameFile(strErrMsg, strUrl, strToUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword) = False Then
                                                        GoTo rollDatabaseAndFile
                                                    End If
                                                End If
                                            End If
                                        End With
                                    Else
                                        'û�е����ļ�
                                    End If
                                End If

                                'д����
                                strSQL = ""
                                strSQL = strSQL + " insert into ����_B_���� (" + vbCr
                                strSQL = strSQL + "   �ļ���ʶ, ���, ˵��, ҳ��, λ��" + vbCr
                                strSQL = strSQL + " ) values (" + vbCr
                                strSQL = strSQL + "   @wjbs, @wjxh, @wjsm, @wjys, @wjwz" + vbCr
                                strSQL = strSQL + " )" + vbCr
                                objSqlCommand.Parameters.Clear()
                                objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                                objSqlCommand.Parameters.AddWithValue("@wjxh", (i + 1))
                                objSqlCommand.Parameters.AddWithValue("@wjsm", objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_WJSM), ""))
                                objSqlCommand.Parameters.AddWithValue("@wjys", objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_WJYS), 0))
                                objSqlCommand.Parameters.AddWithValue("@wjwz", strNewFile)
                                objSqlCommand.CommandText = strSQL
                                objSqlCommand.ExecuteNonQuery()
                            Next
                        End With

                        '�����ǿ�Ʊ༭
                        Dim strCZSM As String = Xydc.Platform.Common.Workflow.BaseFlowObject.LOGO_QXBJ
                        If blnEnforeEdit = True Then
                            If Me.doWriteFileLogo(strErrMsg, objSqlTransaction, strUserXM, strCZSM) = False Then
                                GoTo rollDatabaseAndFile
                            End If
                        End If

                        '����ļ��༭����
                        If Me.doLockFile(strErrMsg, objSqlTransaction, "", False) = False Then
                            GoTo rollDatabaseAndFile
                        End If

                        'ɾ�����б����ļ�
                        If Me.doDeleteBackupFiles_FJ(strErrMsg, objFTPProperty, objOldData) = False Then
                            '���Բ��ɹ����γ������ļ���
                        End If

                    Catch ex As Exception
                        strErrMsg = ex.Message
                        GoTo rollDatabaseAndFile
                    End Try

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo rollDatabase
                End Try

                '�ύ����
                objSqlTransaction.Commit()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacXitongpeizhi.SafeRelease(objdacXitongpeizhi)
            Xydc.Platform.Common.Utilities.FTPProperty.SafeRelease(objFTPProperty)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)
            Xydc.Platform.Common.Data.FlowData.SafeRelease(objOldData)

            '����
            doSaveFujian = True
            Exit Function

rollDatabaseAndFile:
            objSqlTransaction.Rollback()
            If Me.doRestoreFiles_FJ(strSQL, strWJBS, intWJND, objFTPProperty, objNewData, objOldData) = False Then
                '�޷��ָ��ɹ��������ˣ�
            End If
            GoTo errProc

rollDatabase:
            objSqlTransaction.Rollback()
            GoTo errProc

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacXitongpeizhi.SafeRelease(objdacXitongpeizhi)
            Xydc.Platform.Common.Utilities.FTPProperty.SafeRelease(objFTPProperty)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)
            Xydc.Platform.Common.Data.FlowData.SafeRelease(objOldData)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' Update״̬�±��浥����������(��Ų����޸ģ�)
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     blnEnforeEdit          ���Ƿ�ǿ���޸�
        '     strUserXM              ������Ա����
        '     objNewData             ����¼��ֵ
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doSaveFujian( _
            ByRef strErrMsg As String, _
            ByVal blnEnforeEdit As Boolean, _
            ByVal strUserXM As String, _
            ByRef objNewData As System.Collections.Specialized.NameValueCollection) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objOldData As Xydc.Platform.Common.Data.FlowData

            Dim objdacXitongpeizhi As New Xydc.Platform.DataAccess.dacXitongpeizhi
            Dim objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty

            Dim strTable As String = Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_FUJIAN
            Dim intWJND As Integer = Year(Now)
            Dim strBDWJ As String = ""
            Dim strWJBS As String
            Dim intWJXH As Integer
            Dim strSQL As String

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objBaseFTP As New Xydc.Platform.Common.Utilities.BaseFTP

            '��ʼ��
            doSaveFujian = False
            strErrMsg = ""

            Try
                '���
                If Me.IsInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "����δ�����µ����ݣ�"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim

                '��ȡ������Ϣ
                objSqlConnection = Me.SqlConnection
                strWJBS = Me.WJBS
                If strWJBS Is Nothing Then strWJBS = ""
                strWJBS = strWJBS.Trim
                If strWJBS = "" Then
                    Exit Try
                End If

                '��ȡFTP���Ӳ���
                If objdacXitongpeizhi.getFtpServerParam(strErrMsg, objSqlConnection, objFTPProperty) = False Then
                    GoTo errProc
                End If

                '��ȡ�ļ���ż��±����ļ�·��
                intWJXH = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_WJXH), 0)
                strBDWJ = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_BDWJ), "")

                '��ȡԭ��������
                If Me.getFujianData(strErrMsg, intWJXH, objOldData) = False Then
                    GoTo errProc
                End If
                If objOldData.Tables(strTable).DefaultView.Count < 1 Then
                    '��¼�����ڣ����ڲ����̴���Χ��
                    Exit Try
                End If

                '��ʼ����
                objSqlTransaction = objSqlConnection.BeginTransaction()

                '��������
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    Try
                        Dim strNewFile As String = ""
                        If strBDWJ <> "" Then
                            '��ȡ�±����ļ�·��
                            Dim strLocFile As String = strBDWJ
                            Dim blnExisted As Boolean

                            '�ļ�����?
                            If objBaseLocalFile.doFileExisted(strErrMsg, strLocFile, blnExisted) = False Then
                                GoTo rollDatabase
                            End If
                            If blnExisted = False Then
                                strErrMsg = "����[" + strLocFile + "]�����ڣ�"
                                GoTo rollDatabase
                            End If

                            '�ϴ����ļ�
                            Dim strBasePath As String = Me.getBasePath_FJ
                            Dim strUrl As String
                            '��ȡFTP�ļ�·��
                            If Me.getFTPFileName_FJ(strErrMsg, strLocFile, intWJND, strWJBS, intWJXH, strBasePath, strNewFile) = False Then
                                GoTo rollDatabase
                            End If
                            '��Դ�ļ���ͬĿ¼�н��ļ�����
                            If Me.doBackupFiles_FJ(strErrMsg, objFTPProperty, objOldData) = False Then
                                GoTo rollDatabaseAndFile
                            End If
                            '����
                            With objFTPProperty
                                strUrl = .getUrl(strNewFile)
                                If objBaseFTP.doPutFile(strErrMsg, strLocFile, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword) = False Then
                                    GoTo rollDatabaseAndFile
                                End If
                            End With
                        End If

                        'д����
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@wjsm", objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_WJSM), ""))
                        objSqlCommand.Parameters.AddWithValue("@wjys", objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_WJYS), 0))
                        If strNewFile <> "" Then
                            strSQL = ""
                            strSQL = strSQL + " update ����_B_���� set " + vbCr
                            strSQL = strSQL + "   ˵�� = @wjsm, ҳ�� = @wjys, λ�� = @wjwz" + vbCr
                            strSQL = strSQL + " from ����_B_����" + vbCr
                            strSQL = strSQL + " where �ļ���ʶ = @wjbs" + vbCr
                            strSQL = strSQL + " and   ���     = @wjxh" + vbCr
                            objSqlCommand.Parameters.AddWithValue("@wjwz", strNewFile)
                        Else
                            strSQL = ""
                            strSQL = strSQL + " update ����_B_���� set " + vbCr
                            strSQL = strSQL + "   ˵�� = @wjsm, ҳ�� = @wjys" + vbCr
                            strSQL = strSQL + " from ����_B_����" + vbCr
                            strSQL = strSQL + " where �ļ���ʶ = @wjbs" + vbCr
                            strSQL = strSQL + " and   ���     = @wjxh" + vbCr
                        End If
                        objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                        objSqlCommand.Parameters.AddWithValue("@wjxh", intWJXH)
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.ExecuteNonQuery()

                        '�����ǿ�Ʊ༭
                        Dim strCZSM As String = Xydc.Platform.Common.Workflow.BaseFlowObject.LOGO_QXBJ
                        If blnEnforeEdit = True Then
                            If Me.doWriteFileLogo(strErrMsg, objSqlTransaction, strUserXM, strCZSM) = False Then
                                GoTo rollDatabaseAndFile
                            End If
                        End If

                        '����ļ��༭����
                        If Me.doLockFile(strErrMsg, objSqlTransaction, "", False) = False Then
                            GoTo rollDatabaseAndFile
                        End If

                        'ɾ�����б����ļ�
                        If Me.doDeleteBackupFiles_FJ(strErrMsg, objFTPProperty, objOldData) = False Then
                            '���Բ��ɹ����γ������ļ���
                        End If

                    Catch ex As Exception
                        strErrMsg = ex.Message
                        GoTo rollDatabaseAndFile
                    End Try

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo rollDatabase
                End Try

                '�ύ����
                objSqlTransaction.Commit()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacXitongpeizhi.SafeRelease(objdacXitongpeizhi)
            Xydc.Platform.Common.Utilities.FTPProperty.SafeRelease(objFTPProperty)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)
            Xydc.Platform.Common.Data.FlowData.SafeRelease(objOldData)

            '����
            doSaveFujian = True
            Exit Function

rollDatabaseAndFile:
            objSqlTransaction.Rollback()
            If Me.doRestoreFiles_FJ(strSQL, strWJBS, intWJND, objFTPProperty, Nothing, objOldData) = False Then
                '�޷��ָ��ɹ��������ˣ�
            End If
            GoTo errProc

rollDatabase:
            objSqlTransaction.Rollback()
            GoTo errProc

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacXitongpeizhi.SafeRelease(objdacXitongpeizhi)
            Xydc.Platform.Common.Utilities.FTPProperty.SafeRelease(objFTPProperty)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)
            Xydc.Platform.Common.Data.FlowData.SafeRelease(objOldData)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���������+��ظ������ݼ��в������ظ������ݼ�
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     objSrcData             ���������+��ظ������ݼ�
        '     objDesData             ����ظ������ݼ�
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Function doSplitXGWJDataSet( _
            ByRef strErrMsg As String, _
            ByVal objSrcData As Xydc.Platform.Common.Data.FlowData, _
            ByRef objDesData As Xydc.Platform.Common.Data.FlowData) As Boolean

            doSplitXGWJDataSet = False
            objDesData = Nothing

            Try
                Dim intFJBS As Integer = Xydc.Platform.Common.Data.FlowData.enumXGWJLB.FujianFile
                Dim intLJBS As Integer = Xydc.Platform.Common.Data.FlowData.enumXGWJLB.FlowFile
                Dim objDataRow As System.Data.DataRow
                Dim intCount As Integer
                Dim i As Integer

                '���
                If objSrcData Is Nothing Then
                    Exit Try
                End If
                If objSrcData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_SHENPIWENJIAN_FUJIAN) Is Nothing Then
                    Exit Try
                End If

                '���������ݼ�
                objDesData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_XIANGGUANWENJIANFUJIAN)

                '����
                Dim strOldFilter As String
                With objSrcData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_SHENPIWENJIAN_FUJIAN)
                    '���ݹ��˴�
                    strOldFilter = .DefaultView.RowFilter

                    '���ù��˸���
                    .DefaultView.RowFilter = Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_LBBS + " = " + intFJBS.ToString()

                    '����ȫ�������ݼ�
                    intCount = .DefaultView.Count
                    For i = 0 To intCount - 1 Step 1
                        '����
                        With objDesData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_XIANGGUANWENJIANFUJIAN)
                            objDataRow = .NewRow
                        End With

                        '��������
                        objDataRow.Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_XIANGGUANWENJIANFUJIAN_BDWJ) = .DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_BDWJ)
                        objDataRow.Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_XIANGGUANWENJIANFUJIAN_WJBS) = .DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_WJBS)
                        objDataRow.Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_XIANGGUANWENJIANFUJIAN_WJSM) = .DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_WJBT)
                        objDataRow.Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_XIANGGUANWENJIANFUJIAN_WJWZ) = .DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_FJWZ)
                        objDataRow.Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_XIANGGUANWENJIANFUJIAN_WJXH) = .DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_FJXH)
                        objDataRow.Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_XIANGGUANWENJIANFUJIAN_WJYS) = .DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_FJYS)
                        objDataRow.Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_XIANGGUANWENJIANFUJIAN_XSXH) = .DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_XSXH)
                        objDataRow.Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_XIANGGUANWENJIANFUJIAN_XZBZ) = .DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_XZBZ)

                        '����
                        With objDesData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_XIANGGUANWENJIANFUJIAN)
                            .Rows.Add(objDataRow)
                        End With
                    Next

                    '��ԭ
                    .DefaultView.RowFilter = strOldFilter
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doSplitXGWJDataSet = True
            Exit Function

errProc:
            Xydc.Platform.Common.Data.FlowData.SafeRelease(objDesData)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��������ļ����ݣ���ظ������������
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strWJBS                ���ļ���ʶ
        '     intWJND                �����ļ���ŵ����
        '     objSqlTransaction      ����������
        '     objFTPProperty         ��FTP����������
        '     objNewData             ������ļ���¼��ֵ(���ر�������ֵ)
        '     objOldData             ������ļ���¼��ֵ
        '     objNewFJData           ������ļ��еĸ�����ֵ
        '     objOldFJData           ������ļ��еĸ�����ֵ
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doSaveXgwj( _
            ByRef strErrMsg As String, _
            ByVal strWJBS As String, _
            ByVal intWJND As Integer, _
            ByVal objSqlTransaction As System.Data.SqlClient.SqlTransaction, _
            ByVal objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty, _
            ByRef objNewData As Xydc.Platform.Common.Data.FlowData, _
            ByVal objOldData As Xydc.Platform.Common.Data.FlowData, _
            ByVal objNewFJData As Xydc.Platform.Common.Data.FlowData, _
            ByVal objOldFJData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            Dim strTable As String = Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_SHENPIWENJIAN_FUJIAN
            Dim strBakExt As String = Xydc.Platform.Common.Utilities.PulicParameters.BACKUPFILEEXT
            Dim blnNewTrans As Boolean = False
            Dim strSQL As String

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objBaseFTP As New Xydc.Platform.Common.Utilities.BaseFTP

            '��ʼ��
            doSaveXgwj = False
            strErrMsg = ""

            Try
                '���
                If Me.IsInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "����δ�����µ����ݣ�"
                    GoTo errProc
                End If
                If objFTPProperty Is Nothing Then
                    strErrMsg = "����δ����FTP������������"
                    GoTo errProc
                End If
                If strWJBS Is Nothing Then strWJBS = ""
                strWJBS = strWJBS.Trim
                If strWJBS = "" Then
                    Exit Try
                End If

                '��ȡ������Ϣ
                If objSqlTransaction Is Nothing Then
                    objSqlConnection = Me.SqlConnection
                Else
                    objSqlConnection = objSqlTransaction.Connection
                End If

                '��ʼ����
                If objSqlTransaction Is Nothing Then
                    blnNewTrans = True
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Else
                    blnNewTrans = False
                End If

                '��������
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ɾ��������_B_����ļ�������
                    strSQL = ""
                    strSQL = strSQL + " delete from ����_B_����ļ� " + vbCr
                    strSQL = strSQL + " where �ϼ��ļ���ʶ = @wjbs" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    'ɾ��������_B_����ļ�����������
                    strSQL = ""
                    strSQL = strSQL + " delete from ����_B_����ļ����� " + vbCr
                    strSQL = strSQL + " where �ļ���ʶ = @wjbs" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    Try
                        '��Դ�ļ���ͬĿ¼�н��ļ�����
                        If Me.doBackupFiles_XGWJFJ(strErrMsg, objFTPProperty, objOldFJData) = False Then
                            GoTo rollDatabaseAndFile
                        End If

                        '����������
                        Dim strBasePath As String = Me.getBasePath_XGWJFJ
                        Dim blnExisted As Boolean
                        Dim strOldFile As String
                        Dim strLocFile As String
                        Dim strNewFile As String
                        Dim intLBBS As Integer
                        Dim strToUrl As String
                        Dim strUrl As String
                        Dim intCount As Integer
                        Dim i As Integer
                        With objNewData.Tables(strTable)
                            intCount = .DefaultView.Count
                            For i = 0 To intCount - 1 Step 1
                                '��ȡ����ļ�����
                                intLBBS = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_LBBS), 0)

                                '���ദ��
                                Select Case intLBBS
                                    Case Xydc.Platform.Common.Data.FlowData.enumXGWJLB.FujianFile
                                        '��ȡԭFTP·�����±����ļ�·��
                                        strOldFile = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_FJWZ), "")
                                        strLocFile = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_BDWJ), "")
                                        strNewFile = ""

                                        '�ϴ��ļ�
                                        If strLocFile <> "" Then
                                            '�ļ�����?
                                            If objBaseLocalFile.doFileExisted(strErrMsg, strLocFile, blnExisted) = False Then
                                                GoTo rollDatabaseAndFile
                                            End If
                                            If blnExisted = True Then
                                                '��ȡFTP�ļ�·��
                                                If Me.getFTPFileName_XGWJFJ(strErrMsg, strLocFile, intWJND, strWJBS, i + 1, strBasePath, strNewFile) = False Then
                                                    GoTo rollDatabaseAndFile
                                                End If
                                                '�б����ļ�������Ҫ����
                                                With objFTPProperty
                                                    strUrl = .getUrl(strNewFile)
                                                    If objBaseFTP.doPutFile(strErrMsg, strLocFile, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword) = False Then
                                                        GoTo rollDatabaseAndFile
                                                    End If
                                                End With
                                            Else
                                                strErrMsg = "����[" + strLocFile + "]�����ڣ�"
                                                GoTo rollDatabaseAndFile
                                            End If
                                        Else
                                            If strOldFile <> "" Then
                                                '
                                                'δ��FTP����������
                                                '
                                                '�ӱ����ļ��ָ�����ǰ�е��ļ�
                                                With objFTPProperty
                                                    strUrl = .getUrl(strOldFile + strBakExt)
                                                    If objBaseFTP.isFileExisted(strErrMsg, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword, blnExisted) = False Then
                                                        '���Բ��ɹ�
                                                    Else
                                                        If blnExisted = True Then
                                                            '��ȡFTP�ļ�·��
                                                            If Me.getFTPFileName_XGWJFJ(strErrMsg, strOldFile, intWJND, strWJBS, i + 1, strBasePath, strNewFile) = False Then
                                                                GoTo rollDatabaseAndFile
                                                            End If
                                                            strToUrl = .getUrl(strNewFile)
                                                            '�����ļ���
                                                            If objBaseFTP.doRenameFile(strErrMsg, strUrl, strToUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword) = False Then
                                                                GoTo rollDatabaseAndFile
                                                            End If
                                                        End If
                                                    End If
                                                End With
                                            Else
                                                'û�е����ļ�
                                            End If
                                        End If

                                        'д����
                                        strSQL = ""
                                        strSQL = strSQL + " insert into ����_B_����ļ����� (" + vbCr
                                        strSQL = strSQL + "   �ļ���ʶ, ���, ˵��, ҳ��, λ��" + vbCr
                                        strSQL = strSQL + " ) values (" + vbCr
                                        strSQL = strSQL + "   @wjbs, @wjxh, @wjsm, @wjys, @wjwz" + vbCr
                                        strSQL = strSQL + " )" + vbCr
                                        objSqlCommand.Parameters.Clear()
                                        objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                                        objSqlCommand.Parameters.AddWithValue("@wjxh", (i + 1))
                                        objSqlCommand.Parameters.AddWithValue("@wjsm", objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_WJBT), ""))
                                        objSqlCommand.Parameters.AddWithValue("@wjys", objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_FJYS), 0))
                                        objSqlCommand.Parameters.AddWithValue("@wjwz", strNewFile)
                                        objSqlCommand.CommandText = strSQL
                                        objSqlCommand.ExecuteNonQuery()

                                    Case Xydc.Platform.Common.Data.FlowData.enumXGWJLB.FlowFile
                                        'д����
                                        strSQL = ""
                                        strSQL = strSQL + " insert into ����_B_����ļ� (" + vbCr
                                        strSQL = strSQL + "   �ϼ��ļ���ʶ,˳���,��ǰ�ļ���ʶ,�����ļ���ʶ" + vbCr
                                        strSQL = strSQL + " ) values (" + vbCr
                                        strSQL = strSQL + "   @sjwjbs,@sxh,@dqwjbs,@dcwjbs" + vbCr
                                        strSQL = strSQL + " )" + vbCr
                                        objSqlCommand.Parameters.Clear()
                                        objSqlCommand.Parameters.AddWithValue("@sjwjbs", strWJBS)
                                        objSqlCommand.Parameters.AddWithValue("@sxh", (i + 1))
                                        objSqlCommand.Parameters.AddWithValue("@dqwjbs", objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_WJBS), ""))
                                        objSqlCommand.Parameters.AddWithValue("@dcwjbs", strWJBS)
                                        objSqlCommand.CommandText = strSQL
                                        objSqlCommand.ExecuteNonQuery()

                                    Case Else
                                        strErrMsg = "������Ч���ͣ�"
                                        GoTo rollDatabaseAndFile
                                End Select
                            Next
                        End With

                        'ɾ�����б����ļ�
                        If blnNewTrans = True Then
                            If Me.doDeleteBackupFiles_XGWJFJ(strErrMsg, objFTPProperty, objOldFJData) = False Then
                                '���Բ��ɹ����γ������ļ���
                            End If
                        End If

                    Catch ex As Exception
                        strErrMsg = ex.Message
                        GoTo rollDatabaseAndFile
                    End Try

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo rollDatabase
                End Try

                '�ύ����
                If blnNewTrans = True Then
                    objSqlTransaction.Commit()
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)

            '����
            doSaveXgwj = True
            Exit Function

rollDatabaseAndFile:
            If blnNewTrans = True Then
                objSqlTransaction.Rollback()
                If Me.doRestoreFiles_XGWJFJ(strSQL, strWJBS, intWJND, objFTPProperty, objNewFJData, objOldFJData) = False Then
                    '�޷��ָ��ɹ��������ˣ�
                End If
            End If
            GoTo errProc

rollDatabase:
            If blnNewTrans = True Then
                objSqlTransaction.Rollback()
            End If
            GoTo errProc

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��������ļ����ݣ���ظ������������
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     blnEnforeEdit          ���Ƿ�ǿ���޸�
        '     strUserXM              ������Ա����
        '     objNewData             ���������+��ظ�����ֵ
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doSaveXgwj( _
            ByRef strErrMsg As String, _
            ByVal blnEnforeEdit As Boolean, _
            ByVal strUserXM As String, _
            ByRef objNewData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objOldData As Xydc.Platform.Common.Data.FlowData
            Dim objOldFJData As Xydc.Platform.Common.Data.FlowData
            Dim objNewFJData As Xydc.Platform.Common.Data.FlowData

            Dim strTable As String = Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_SHENPIWENJIAN_FUJIAN
            Dim strBakExt As String = Xydc.Platform.Common.Utilities.PulicParameters.BACKUPFILEEXT
            Dim intWJND As Integer = Year(Now)
            Dim strWJBS As String
            Dim strSQL As String

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objBaseFTP As New Xydc.Platform.Common.Utilities.BaseFTP

            Dim objdacXitongpeizhi As New Xydc.Platform.DataAccess.dacXitongpeizhi
            Dim objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty

            '��ʼ��
            doSaveXgwj = False
            strErrMsg = ""

            Try
                '���
                If Me.IsInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "����δ�����µ����ݣ�"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim

                '��ȡ������Ϣ
                objSqlConnection = Me.SqlConnection
                strWJBS = Me.WJBS
                If strWJBS Is Nothing Then strWJBS = ""
                strWJBS = strWJBS.Trim
                If strWJBS = "" Then
                    Exit Try
                End If

                '��ȡFTP���Ӳ���
                If objdacXitongpeizhi.getFtpServerParam(strErrMsg, objSqlConnection, objFTPProperty) = False Then
                    GoTo errProc
                End If

                '��ȡ������ļ�����
                If Me.getXgwjData(strErrMsg, objOldData) = False Then
                    GoTo errProc
                End If
                '�������ظ���
                If Me.doSplitXGWJDataSet(strErrMsg, objOldData, objOldFJData) = False Then
                    GoTo errProc
                End If
                '��������ظ���
                If Me.doSplitXGWJDataSet(strErrMsg, objNewData, objNewFJData) = False Then
                    GoTo errProc
                End If

                '��ʼ����
                objSqlTransaction = objSqlConnection.BeginTransaction()

                '��������
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ɾ��������_B_����ļ�������
                    strSQL = ""
                    strSQL = strSQL + " delete from ����_B_����ļ� " + vbCr
                    strSQL = strSQL + " where �ϼ��ļ���ʶ = @wjbs" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    'ɾ��������_B_����ļ�����������
                    strSQL = ""
                    strSQL = strSQL + " delete from ����_B_����ļ����� " + vbCr
                    strSQL = strSQL + " where �ļ���ʶ = @wjbs" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    Try
                        '��Դ�ļ���ͬĿ¼�н��ļ�����
                        If Me.doBackupFiles_XGWJFJ(strErrMsg, objFTPProperty, objOldFJData) = False Then
                            GoTo rollDatabaseAndFile
                        End If

                        '����������
                        Dim strBasePath As String = Me.getBasePath_XGWJFJ
                        Dim blnExisted As Boolean
                        Dim strOldFile As String
                        Dim strLocFile As String
                        Dim strNewFile As String
                        Dim intLBBS As Integer
                        Dim strToUrl As String
                        Dim strUrl As String
                        Dim intCount As Integer
                        Dim i As Integer
                        With objNewData.Tables(strTable)
                            intCount = .DefaultView.Count
                            For i = 0 To intCount - 1 Step 1
                                '��ȡ����ļ�����
                                intLBBS = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_LBBS), 0)

                                '���ദ��
                                Select Case intLBBS
                                    Case Xydc.Platform.Common.Data.FlowData.enumXGWJLB.FujianFile
                                        '��ȡԭFTP·�����±����ļ�·��
                                        strOldFile = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_FJWZ), "")
                                        strLocFile = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_BDWJ), "")
                                        strNewFile = ""

                                        '�ϴ��ļ�
                                        If strLocFile <> "" Then
                                            '�ļ�����?
                                            If objBaseLocalFile.doFileExisted(strErrMsg, strLocFile, blnExisted) = False Then
                                                GoTo rollDatabaseAndFile
                                            End If
                                            If blnExisted = True Then
                                                '��ȡFTP�ļ�·��
                                                If Me.getFTPFileName_XGWJFJ(strErrMsg, strLocFile, intWJND, strWJBS, i + 1, strBasePath, strNewFile) = False Then
                                                    GoTo rollDatabaseAndFile
                                                End If
                                                '�б����ļ�������Ҫ����
                                                With objFTPProperty
                                                    strUrl = .getUrl(strNewFile)
                                                    If objBaseFTP.doPutFile(strErrMsg, strLocFile, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword) = False Then
                                                        GoTo rollDatabaseAndFile
                                                    End If
                                                End With
                                            Else
                                                strErrMsg = "����[" + strLocFile + "]�����ڣ�"
                                                GoTo rollDatabaseAndFile
                                            End If
                                        Else
                                            If strOldFile <> "" Then
                                                '
                                                'δ��FTP����������
                                                '
                                                '�ӱ����ļ��ָ�����ǰ�е��ļ�
                                                With objFTPProperty
                                                    strUrl = .getUrl(strOldFile + strBakExt)
                                                    If objBaseFTP.isFileExisted(strErrMsg, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword, blnExisted) = False Then
                                                        '���Բ��ɹ�
                                                    Else
                                                        If blnExisted = True Then
                                                            '��ȡFTP�ļ�·��
                                                            If Me.getFTPFileName_XGWJFJ(strErrMsg, strOldFile, intWJND, strWJBS, i + 1, strBasePath, strNewFile) = False Then
                                                                GoTo rollDatabaseAndFile
                                                            End If
                                                            strToUrl = .getUrl(strNewFile)
                                                            '�����ļ���
                                                            If objBaseFTP.doRenameFile(strErrMsg, strUrl, strToUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword) = False Then
                                                                GoTo rollDatabaseAndFile
                                                            End If
                                                        End If
                                                    End If
                                                End With
                                            Else
                                                'û�е����ļ�
                                            End If
                                        End If

                                        'д����
                                        strSQL = ""
                                        strSQL = strSQL + " insert into ����_B_����ļ����� (" + vbCr
                                        strSQL = strSQL + "   �ļ���ʶ, ���, ˵��, ҳ��, λ��" + vbCr
                                        strSQL = strSQL + " ) values (" + vbCr
                                        strSQL = strSQL + "   @wjbs, @wjxh, @wjsm, @wjys, @wjwz" + vbCr
                                        strSQL = strSQL + " )" + vbCr
                                        objSqlCommand.Parameters.Clear()
                                        objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                                        objSqlCommand.Parameters.AddWithValue("@wjxh", (i + 1))
                                        objSqlCommand.Parameters.AddWithValue("@wjsm", objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_WJBT), ""))
                                        objSqlCommand.Parameters.AddWithValue("@wjys", objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_FJYS), 0))
                                        objSqlCommand.Parameters.AddWithValue("@wjwz", strNewFile)
                                        objSqlCommand.CommandText = strSQL
                                        objSqlCommand.ExecuteNonQuery()

                                    Case Xydc.Platform.Common.Data.FlowData.enumXGWJLB.FlowFile
                                        'д����
                                        strSQL = ""
                                        strSQL = strSQL + " insert into ����_B_����ļ� (" + vbCr
                                        strSQL = strSQL + "   �ϼ��ļ���ʶ,˳���,��ǰ�ļ���ʶ,�����ļ���ʶ" + vbCr
                                        strSQL = strSQL + " ) values (" + vbCr
                                        strSQL = strSQL + "   @sjwjbs,@sxh,@dqwjbs,@dcwjbs" + vbCr
                                        strSQL = strSQL + " )" + vbCr
                                        objSqlCommand.Parameters.Clear()
                                        objSqlCommand.Parameters.AddWithValue("@sjwjbs", strWJBS)
                                        objSqlCommand.Parameters.AddWithValue("@sxh", (i + 1))
                                        objSqlCommand.Parameters.AddWithValue("@dqwjbs", objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_WJBS), ""))
                                        objSqlCommand.Parameters.AddWithValue("@dcwjbs", strWJBS)
                                        objSqlCommand.CommandText = strSQL
                                        objSqlCommand.ExecuteNonQuery()

                                    Case Else
                                        strErrMsg = "������Ч���ͣ�"
                                        GoTo rollDatabaseAndFile
                                End Select
                            Next
                        End With

                        '�����ǿ�Ʊ༭
                        Dim strCZSM As String = Xydc.Platform.Common.Workflow.BaseFlowObject.LOGO_QXBJ
                        If blnEnforeEdit = True Then
                            If Me.doWriteFileLogo(strErrMsg, objSqlTransaction, strUserXM, strCZSM) = False Then
                                GoTo rollDatabaseAndFile
                            End If
                        End If

                        '����ļ��༭����
                        If Me.doLockFile(strErrMsg, objSqlTransaction, "", False) = False Then
                            GoTo rollDatabaseAndFile
                        End If

                        'ɾ�����б����ļ�
                        If Me.doDeleteBackupFiles_XGWJFJ(strErrMsg, objFTPProperty, objOldFJData) = False Then
                            '���Բ��ɹ����γ������ļ���
                        End If

                    Catch ex As Exception
                        strErrMsg = ex.Message
                        GoTo rollDatabaseAndFile
                    End Try

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo rollDatabase
                End Try

                '�ύ����
                objSqlTransaction.Commit()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacXitongpeizhi.SafeRelease(objdacXitongpeizhi)
            Xydc.Platform.Common.Utilities.FTPProperty.SafeRelease(objFTPProperty)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)
            Xydc.Platform.Common.Data.FlowData.SafeRelease(objNewFJData)
            Xydc.Platform.Common.Data.FlowData.SafeRelease(objOldFJData)
            Xydc.Platform.Common.Data.FlowData.SafeRelease(objOldData)

            '����
            doSaveXgwj = True
            Exit Function

rollDatabaseAndFile:
            objSqlTransaction.Rollback()
            If Me.doRestoreFiles_XGWJFJ(strSQL, strWJBS, intWJND, objFTPProperty, objNewFJData, objOldFJData) = False Then
                '�޷��ָ��ɹ��������ˣ�
            End If
            GoTo errProc

rollDatabase:
            objSqlTransaction.Rollback()
            GoTo errProc

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacXitongpeizhi.SafeRelease(objdacXitongpeizhi)
            Xydc.Platform.Common.Utilities.FTPProperty.SafeRelease(objFTPProperty)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)
            Xydc.Platform.Common.Data.FlowData.SafeRelease(objNewFJData)
            Xydc.Platform.Common.Data.FlowData.SafeRelease(objOldFJData)
            Xydc.Platform.Common.Data.FlowData.SafeRelease(objOldData)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' Update״̬�±�������ļ������ĵ�����������(��Ų����޸ģ�)
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     blnEnforeEdit          ���Ƿ�ǿ���޸�
        '     strUserXM              ������Ա����
        '     objNewData             ����¼��ֵ
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doSaveXgwjFujian( _
            ByRef strErrMsg As String, _
            ByVal blnEnforeEdit As Boolean, _
            ByVal strUserXM As String, _
            ByRef objNewData As System.Collections.Specialized.NameValueCollection) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objOldData As Xydc.Platform.Common.Data.FlowData

            Dim objdacXitongpeizhi As New Xydc.Platform.DataAccess.dacXitongpeizhi
            Dim objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty

            Dim strTable As String = Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_XIANGGUANWENJIANFUJIAN
            Dim intWJND As Integer = Year(Now)
            Dim strBDWJ As String = ""
            Dim strWJBS As String
            Dim intWJXH As Integer
            Dim strSQL As String

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objBaseFTP As New Xydc.Platform.Common.Utilities.BaseFTP

            '��ʼ��
            doSaveXgwjFujian = False
            strErrMsg = ""

            Try
                '���
                If Me.IsInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "����δ�����µ����ݣ�"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim

                '��ȡ������Ϣ
                objSqlConnection = Me.SqlConnection
                strWJBS = Me.WJBS
                If strWJBS Is Nothing Then strWJBS = ""
                strWJBS = strWJBS.Trim
                If strWJBS = "" Then
                    Exit Try
                End If

                '��ȡFTP���Ӳ���
                If objdacXitongpeizhi.getFtpServerParam(strErrMsg, objSqlConnection, objFTPProperty) = False Then
                    GoTo errProc
                End If

                '��ȡ�ļ���ż��±����ļ�·��
                intWJXH = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_XIANGGUANWENJIANFUJIAN_WJXH), 0)
                strBDWJ = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_XIANGGUANWENJIANFUJIAN_BDWJ), "")

                '��ȡԭ��������
                If Me.getXgwjFujianData(strErrMsg, intWJXH, objOldData) = False Then
                    GoTo errProc
                End If
                If objOldData.Tables(strTable).DefaultView.Count < 1 Then
                    '��¼�����ڣ����ڲ����̴���Χ��
                    Exit Try
                End If

                '��ʼ����
                objSqlTransaction = objSqlConnection.BeginTransaction()

                '��������
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    Try
                        Dim strNewFile As String = ""
                        If strBDWJ <> "" Then
                            '��ȡ�±����ļ�·��
                            Dim strLocFile As String = strBDWJ
                            Dim blnExisted As Boolean

                            '�ļ�����?
                            If objBaseLocalFile.doFileExisted(strErrMsg, strLocFile, blnExisted) = False Then
                                GoTo rollDatabase
                            End If
                            If blnExisted = False Then
                                strErrMsg = "����[" + strLocFile + "]�����ڣ�"
                                GoTo rollDatabase
                            End If

                            '�ϴ����ļ�
                            Dim strBasePath As String = Me.getBasePath_XGWJFJ
                            Dim strUrl As String
                            '��ȡFTP�ļ�·��
                            If Me.getFTPFileName_XGWJFJ(strErrMsg, strLocFile, intWJND, strWJBS, intWJXH, strBasePath, strNewFile) = False Then
                                GoTo rollDatabase
                            End If
                            '��Դ�ļ���ͬĿ¼�н��ļ�����
                            If Me.doBackupFiles_XGWJFJ(strErrMsg, objFTPProperty, objOldData) = False Then
                                GoTo rollDatabaseAndFile
                            End If
                            '����
                            With objFTPProperty
                                strUrl = .getUrl(strNewFile)
                                If objBaseFTP.doPutFile(strErrMsg, strLocFile, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword) = False Then
                                    GoTo rollDatabaseAndFile
                                End If
                            End With
                        End If

                        'д����
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@wjsm", objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_XIANGGUANWENJIANFUJIAN_WJSM), ""))
                        objSqlCommand.Parameters.AddWithValue("@wjys", objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_XIANGGUANWENJIANFUJIAN_WJYS), 0))
                        If strNewFile <> "" Then
                            strSQL = ""
                            strSQL = strSQL + " update ����_B_����ļ����� set " + vbCr
                            strSQL = strSQL + "   ˵�� = @wjsm, ҳ�� = @wjys, λ�� = @wjwz" + vbCr
                            strSQL = strSQL + " from ����_B_����ļ�����" + vbCr
                            strSQL = strSQL + " where �ļ���ʶ = @wjbs" + vbCr
                            strSQL = strSQL + " and   ���     = @wjxh" + vbCr
                            objSqlCommand.Parameters.AddWithValue("@wjwz", strNewFile)
                        Else
                            strSQL = ""
                            strSQL = strSQL + " update ����_B_����ļ����� set " + vbCr
                            strSQL = strSQL + "   ˵�� = @wjsm, ҳ�� = @wjys" + vbCr
                            strSQL = strSQL + " from ����_B_����ļ�����" + vbCr
                            strSQL = strSQL + " where �ļ���ʶ = @wjbs" + vbCr
                            strSQL = strSQL + " and   ���     = @wjxh" + vbCr
                        End If
                        objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                        objSqlCommand.Parameters.AddWithValue("@wjxh", intWJXH)
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.ExecuteNonQuery()

                        '�����ǿ�Ʊ༭
                        Dim strCZSM As String = Xydc.Platform.Common.Workflow.BaseFlowObject.LOGO_QXBJ
                        If blnEnforeEdit = True Then
                            If Me.doWriteFileLogo(strErrMsg, objSqlTransaction, strUserXM, strCZSM) = False Then
                                GoTo rollDatabaseAndFile
                            End If
                        End If

                        '����ļ��༭����
                        If Me.doLockFile(strErrMsg, objSqlTransaction, "", False) = False Then
                            GoTo rollDatabaseAndFile
                        End If

                        'ɾ�����б����ļ�
                        If Me.doDeleteBackupFiles_XGWJFJ(strErrMsg, objFTPProperty, objOldData) = False Then
                            '���Բ��ɹ����γ������ļ���
                        End If

                    Catch ex As Exception
                        strErrMsg = ex.Message
                        GoTo rollDatabaseAndFile
                    End Try

                Catch ex As Exception
                    GoTo rollDatabase
                End Try

                '�ύ����
                objSqlTransaction.Commit()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacXitongpeizhi.SafeRelease(objdacXitongpeizhi)
            Xydc.Platform.Common.Utilities.FTPProperty.SafeRelease(objFTPProperty)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)
            Xydc.Platform.Common.Data.FlowData.SafeRelease(objOldData)

            '����
            doSaveXgwjFujian = True
            Exit Function

rollDatabaseAndFile:
            objSqlTransaction.Rollback()
            If Me.doRestoreFiles_XGWJFJ(strSQL, strWJBS, intWJND, objFTPProperty, Nothing, objOldData) = False Then
                '�޷��ָ��ɹ��������ˣ�
            End If
            GoTo errProc

rollDatabase:
            objSqlTransaction.Rollback()
            GoTo errProc

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacXitongpeizhi.SafeRelease(objdacXitongpeizhi)
            Xydc.Platform.Common.Utilities.FTPProperty.SafeRelease(objFTPProperty)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)
            Xydc.Platform.Common.Data.FlowData.SafeRelease(objOldData)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���桰����_B_���ӡ�������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objOldData           ��������
        '     objNewData           ��������
        '     objenumEditType      ���༭����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        ' �޸ļ�¼
        '      ����
        '----------------------------------------------------------------
        Public Overridable Function doSaveData_Jiaojie( _
            ByRef strErrMsg As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByRef objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction = Nothing
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Nothing
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand = Nothing
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim strSQL As String = ""

            '��ʼ��
            doSaveData_Jiaojie = False
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If

                '��ȡ����
                objSqlConnection = Me.SqlConnection

                '�������
                If Me.doVerifyData_Jiaojie(strErrMsg, objOldData, objNewData, objenumEditType) = False Then
                    GoTo errProc
                End If

                '��ʼ����
                Try
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '��������
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '����SQL
                    Dim intDefaultValue As Integer = 0
                    Dim strFileds As String = ""
                    Dim strValues As String = ""
                    Dim strField As String = ""
                    Dim intCount As Integer = 0
                    Dim i As Integer = 0
                    Select Case objenumEditType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew, _
                            Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eCpyNew
                            '��������ֶ��б�
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Else
                                        If strFileds = "" Then
                                            strFileds = objNewData.GetKey(i)
                                        Else
                                            strFileds = strFileds + "," + objNewData.GetKey(i)
                                        End If
                                        If strValues = "" Then
                                            strValues = "@A" + i.ToString()
                                        Else
                                            strValues = strValues + "," + "@A" + i.ToString()
                                        End If
                                End Select
                            Next
                            '׼��SQL
                            strSQL = ""
                            strSQL = strSQL + " insert into ����_B_���� (" + strFileds + ")"
                            strSQL = strSQL + " values (" + strValues + ")"
                            '׼������
                            objSqlCommand.Parameters.Clear()
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSRQ, _
                                        Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSRQ, _
                                        Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_BLZHQX, _
                                        Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_WCRQ
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), System.DBNull.Value)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(objNewData.Item(i), System.DateTime))
                                        End If
                                    Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JJXH, _
                                        Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_YJJH, _
                                        Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSXH, _
                                        Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSZZWJ, _
                                        Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSDZWJ, _
                                        Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSZZFJ, _
                                        Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSDZFJ, _
                                        Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSXH, _
                                        Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSZZWJ, _
                                        Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSDZWJ, _
                                        Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSZZFJ, _
                                        Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSDZFJ, _
                                        Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_SFDG, _
                                        Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_BWTX
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), intDefaultValue)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(objNewData.Item(i), Integer))
                                        End If
                                    Case Else
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), " ")
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), objNewData.Item(i))
                                        End If
                                End Select
                            Next
                            'ִ��SQL
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()

                        Case Else
                            '��ȡԭ���ļ���ʶ��
                            Dim strOldWJBS As String
                            Dim intOldJJXH As Integer
                            strOldWJBS = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_WJBS), "")
                            intOldJJXH = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JJXH), 0)
                            '��������ֶ��б�
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Else
                                        If strFileds = "" Then
                                            strFileds = objNewData.GetKey(i) + " = @A" + i.ToString()
                                        Else
                                            strFileds = strFileds + "," + objNewData.GetKey(i) + " = @A" + i.ToString()
                                        End If
                                End Select
                            Next
                            '׼��SQL
                            strSQL = ""
                            strSQL = strSQL + " update ����_B_���� set " + vbCr
                            strSQL = strSQL + "   " + strFileds + vbCr
                            strSQL = strSQL + " where �ļ���ʶ = @oldwjbs" + vbCr
                            strSQL = strSQL + " and   ������� = @oldjjxh" + vbCr
                            '׼������
                            objSqlCommand.Parameters.Clear()
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSRQ, _
                                        Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSRQ, _
                                        Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_BLZHQX, _
                                        Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_WCRQ
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), System.DBNull.Value)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(objNewData.Item(i), System.DateTime))
                                        End If
                                    Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JJXH, _
                                        Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_YJJH, _
                                        Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSXH, _
                                        Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSZZWJ, _
                                        Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSDZWJ, _
                                        Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSZZFJ, _
                                        Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSDZFJ, _
                                        Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSXH, _
                                        Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSZZWJ, _
                                        Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSDZWJ, _
                                        Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSZZFJ, _
                                        Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSDZFJ, _
                                        Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_SFDG, _
                                        Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_BWTX
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), intDefaultValue)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(objNewData.Item(i), Integer))
                                        End If
                                    Case Else
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), " ")
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), objNewData.Item(i))
                                        End If
                                End Select
                            Next

                            objSqlCommand.Parameters.AddWithValue("@oldwjbs", strOldWJBS)
                            objSqlCommand.Parameters.AddWithValue("@oldjjxh", intOldJJXH)
                            'ִ��SQL
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()
                    End Select
                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '�ύ����
                objSqlTransaction.Commit()
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            doSaveData_Jiaojie = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���¡�����_B_���ӡ�������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strWhere             ����������
        '     strFileds            ���������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        ' �޸ļ�¼
        '      ����
        '----------------------------------------------------------------
        Public Overridable Function doUpdateJiaojie( _
            ByRef strErrMsg As String, _
            ByVal strWhere As String, _
            ByVal strFileds As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction = Nothing
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Nothing
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand = Nothing
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim strSQL As String = ""

            '��ʼ��
            doUpdateJiaojie = False
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If

                If strFileds = "" Then
                    Exit Try
                End If

                '��ȡ����
                objSqlConnection = Me.SqlConnection

                '��ʼ����
                Try
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '��������
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '׼��SQL
                    strSQL = ""
                    strSQL = strSQL + " update ����_B_���� " + vbCr
                    strSQL = strSQL + "   " + strFileds + vbCr
                    If strWhere <> "" Then
                        strSQL = strSQL + strWhere
                    End If

                    'ִ��SQL
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '�ύ����
                objSqlTransaction.Commit()
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            doUpdateJiaojie = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function


        '----------------------------------------------------------------
        ' ����strWJBS��ȡ������_B_��������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strWhere             ����������
        '     blnUnused            ���ӿ�����
        '     objBanliData         �����ذ�������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        ' �޸ļ�¼
        '      ����
        '----------------------------------------------------------------
        Public Overridable Function getBanliData( _
            ByRef strErrMsg As String, _
            ByVal strWhere As String, _
            ByVal blnUnused As Boolean, _
            ByRef objBanliData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim objTempBanliData As Xydc.Platform.Common.Data.FlowData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            getBanliData = False
            objBanliData = Nothing
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '�������ݼ�
                objTempBanliData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_BANLI)
                If strWJBS = "" Then Exit Try

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                'ִ�м���
                With Me.SqlDataAdapter
                    '���㽻����Ϣ
                    strSQL = ""
                    strSQL = strSQL + " select a.*" + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select * from ����_B_����" + vbCr
                    strSQL = strSQL + "   where �ļ���ʶ = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + " ) a" + vbCr
                    If strWhere <> "" Then
                        strSQL = strSQL + " where " + strWhere + vbCr
                    End If
                    strSQL = strSQL + " order by a.��ʾ���,a.�������� desc" + vbCr

                    '���ò���
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    'ִ�в���
                    .Fill(objTempBanliData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_BANLI))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            objBanliData = objTempBanliData
            getBanliData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.FlowData.SafeRelease(objTempBanliData)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��顰����_B_���������ݵĺϷ���
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objOldData           ��������
        '     objNewData           ��������
        '     objenumEditType      ���༭����
        ' ����
        '     True                 ���Ϸ�
        '     False                �����Ϸ��������������
        ' �޸ļ�¼
        '      ����
        '----------------------------------------------------------------
        Public Overridable Function doVerifyData_Banli( _
            ByRef strErrMsg As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByRef objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objListDictionary As System.Collections.Specialized.ListDictionary = Nothing
            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Nothing
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet = Nothing
            Dim strWJBS As String = ""
            Dim intLen As Integer = 0
            Dim strSQL As String = ""

            doVerifyData_Banli = False

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "����δ�����µ����ݣ�"
                    GoTo errProc
                End If
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew, _
                        Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eCpyNew
                    Case Else
                        If objOldData Is Nothing Then
                            strErrMsg = "����δ����ɵ����ݣ�"
                            GoTo errProc
                        End If
                End Select

                '��ȡ��Ϣ
                objSqlConnection = Me.SqlConnection
                strWJBS = Me.WJBS

                '��ȡ��ṹ����
                strSQL = "select top 0 * from ����_B_����"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, objSqlConnection, strSQL, "����_B_����", objDataSet) = False Then
                    GoTo errProc
                End If

                '������ݳ���
                Dim intCount As Integer = 0
                Dim strValue As String = ""
                Dim strField As String = ""
                Dim i As Integer = 0
                intCount = objNewData.Count
                For i = 0 To intCount - 1 Step 1
                    strField = objNewData.GetKey(i).Trim
                    strValue = objNewData.Item(i).Trim
                    Select Case strField
                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_BANLI_WJBS
                            '�Զ���
                            If strValue = "" Then
                                strValue = strWJBS
                            End If

                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_BANLI_JJXH
                            If strValue = "" Then
                                strErrMsg = "����û������[" + strField + "]��"
                                GoTo errProc
                            End If
                            If objPulicParameters.isFloatString(strValue) = False Then
                                strErrMsg = "����[" + strValue + "]����Ч����ֵ��"
                                GoTo errProc
                            End If
                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_BANLI_XSXH
                            If strValue <> "" Then
                                If objPulicParameters.isIntegerString(strValue) = False Then
                                    strErrMsg = "����[" + strValue + "]����Ч�����֣�"
                                    GoTo errProc
                                End If
                            End If

                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_BANLI_BLRQ, _
                            Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_BANLI_DLRQ, _
                            Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_BANLI_TXRQ
                            If strValue <> "" Then
                                If objPulicParameters.isDatetimeString(strValue) = False Then
                                    strErrMsg = "����[" + strValue + "]����Ч�����ڣ�"
                                    GoTo errProc
                                End If
                            End If

                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_BANLI_BLLX, _
                            Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_BANLI_BLZL, _
                            Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_BANLI_BLR
                            If strValue = "" Then
                                strErrMsg = "����û������[" + strField + "]��"
                                GoTo errProc
                            End If
                            With objDataSet.Tables(0).Columns(strField)
                                intLen = objPulicParameters.getStringLength(strValue)
                                If intLen > .MaxLength Then
                                    strErrMsg = "����[" + strField + "]���Ȳ��ܳ���[" + .MaxLength.ToString() + "]��ʵ����[" + intLen.ToString() + "]��"
                                    GoTo errProc
                                End If
                            End With
                        Case Else
                            If strValue <> "" Then
                                With objDataSet.Tables(0).Columns(strField)
                                    intLen = objPulicParameters.getStringLength(strValue)
                                    If intLen > .MaxLength Then
                                        strErrMsg = "����[" + strField + "]���Ȳ��ܳ���[" + .MaxLength.ToString() + "]��ʵ����[" + intLen.ToString() + "]��"
                                        GoTo errProc
                                    End If
                                End With
                            End If
                    End Select
                    objNewData(strField) = strValue
                Next
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)

                '���Լ��
                Dim intNewJJXH As Integer = objPulicParameters.getObjectValue(objNewData.Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_BANLI_JJXH), 0)
                objListDictionary = New System.Collections.Specialized.ListDictionary
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                        strSQL = "select * from ����_B_���� where �ļ���ʶ = @wjbs and ������� = @jjxh"
                        objListDictionary.Add("@wjbs", strWJBS)
                        objListDictionary.Add("@jjxh", intNewJJXH)
                    Case Else
                        Dim intOldJJXH As Integer = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_BANLI_JJXH), 0)
                        strSQL = "select * from ����_B_���� where �ļ���ʶ = @wjbs and ������� = @jjxh and ������� <> @oldjjxh"
                        objListDictionary.Add("@wjbs", strWJBS)
                        objListDictionary.Add("@jjxh", intNewJJXH)
                        objListDictionary.Add("@oldjjxh", intOldJJXH)
                End Select
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objListDictionary, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strErrMsg = "����[" + intNewJJXH.ToString + "]�Ѿ����ڣ�"
                    GoTo errProc
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objListDictionary)
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objListDictionary)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            doVerifyData_Banli = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objListDictionary)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���桰����_B_����������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objOldData           ��������
        '     objNewData           ��������
        '     objenumEditType      ���༭����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        ' �޸ļ�¼
        '      ����
        '----------------------------------------------------------------
        Public Overridable Function doSaveData_Banli( _
            ByRef strErrMsg As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByRef objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction = Nothing
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Nothing
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand = Nothing
            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim strSQL As String = ""

            '��ʼ��
            doSaveData_Banli = False
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If

                '��ȡ����
                objSqlConnection = Me.SqlConnection

                '�������
                If Me.doVerifyData_Banli(strErrMsg, objOldData, objNewData, objenumEditType) = False Then
                    GoTo errProc
                End If

                '��ʼ����
                Try
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '��������
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '����SQL
                    Dim intDefaultValue As Integer = 0
                    Dim strFileds As String = ""
                    Dim strValues As String = ""
                    Dim strField As String = ""
                    Dim intCount As Integer = 0
                    Dim i As Integer = 0
                    Select Case objenumEditType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew, _
                            Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eCpyNew
                            '��������ֶ��б�
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Else
                                        If strFileds = "" Then
                                            strFileds = objNewData.GetKey(i)
                                        Else
                                            strFileds = strFileds + "," + objNewData.GetKey(i)
                                        End If
                                        If strValues = "" Then
                                            strValues = "@A" + i.ToString()
                                        Else
                                            strValues = strValues + "," + "@A" + i.ToString()
                                        End If
                                End Select
                            Next
                            '׼��SQL
                            strSQL = ""
                            strSQL = strSQL + " insert into ����_B_���� (" + strFileds + ")"
                            strSQL = strSQL + " values (" + strValues + ")"
                            '׼������
                            objSqlCommand.Parameters.Clear()
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_BANLI_BLRQ, _
                                        Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_BANLI_DLRQ, _
                                        Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_BANLI_TXRQ
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), System.DBNull.Value)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(objNewData.Item(i), System.DateTime))
                                        End If
                                    Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_BANLI_JJXH, _
                                        Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_BANLI_XSXH
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), intDefaultValue)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(objNewData.Item(i), Integer))
                                        End If
                                    Case Else
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), " ")
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), objNewData.Item(i))
                                        End If
                                End Select
                            Next
                            'ִ��SQL
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()

                        Case Else
                            '��ȡԭ���ļ���ʶ��
                            Dim strOldWJBS As String
                            Dim intOldJJXH As Integer
                            strOldWJBS = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_BANLI_WJBS), "")
                            intOldJJXH = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_BANLI_JJXH), 0)
                            '��������ֶ��б�
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Else
                                        If strFileds = "" Then
                                            strFileds = objNewData.GetKey(i) + " = @A" + i.ToString()
                                        Else
                                            strFileds = strFileds + "," + objNewData.GetKey(i) + " = @A" + i.ToString()
                                        End If
                                End Select
                            Next
                            '׼��SQL
                            strSQL = ""
                            strSQL = strSQL + " update ����_B_���� set " + vbCr
                            strSQL = strSQL + "   " + strFileds + vbCr
                            strSQL = strSQL + " where �ļ���ʶ = @oldwjbs" + vbCr
                            strSQL = strSQL + " and   ������� = @oldjjxh" + vbCr
                            '׼������
                            objSqlCommand.Parameters.Clear()
                            intCount = objNewData.Count
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_BANLI_BLRQ, _
                                        Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_BANLI_DLRQ, _
                                        Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_BANLI_TXRQ
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), System.DBNull.Value)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(objNewData.Item(i), System.DateTime))
                                        End If
                                    Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_BANLI_JJXH, _
                                        Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_BANLI_XSXH
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), intDefaultValue)
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(objNewData.Item(i), Integer))
                                        End If
                                    Case Else
                                        If objNewData.Item(i) = "" Then
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), " ")
                                        Else
                                            objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), objNewData.Item(i))
                                        End If
                                End Select
                            Next
                            objSqlCommand.Parameters.AddWithValue("@oldwjbs", strOldWJBS)
                            objSqlCommand.Parameters.AddWithValue("@oldjjxh", intOldJJXH)
                            'ִ��SQL
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()
                    End Select
                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '�ύ����
                objSqlTransaction.Commit()
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            doSaveData_Banli = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �ڸ�������������ɾ��������_B_������������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objOldData           ��������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doDeleteData_FJ( _
            ByRef strErrMsg As String, _
            ByVal objOldData As System.Data.DataRow) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile

            '��ʼ��
            doDeleteData_FJ = False
            strErrMsg = ""

            Try
                '���
                If objOldData Is Nothing Then
                    strErrMsg = "����δ����Ҫɾ�������ݣ�"
                    GoTo errProc
                End If

                '������ʱ�ļ�
                Dim strTempFile As String = ""
                strTempFile = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_BDWJ), "")

                'ɾ������
                objOldData.Delete()

                'ɾ����ʱ�ļ�
                If strTempFile <> "" Then
                    If objBaseLocalFile.doDeleteFile(strErrMsg, strTempFile) = False Then
                        '�γ������ļ�
                    End If
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)

            '����
            doDeleteData_FJ = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ������ļ������Ļ���������ɾ������ļ�������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objOldData           ��������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doDeleteData_XGWJ( _
            ByRef strErrMsg As String, _
            ByVal objOldData As System.Data.DataRow) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile

            '��ʼ��
            doDeleteData_XGWJ = False
            strErrMsg = ""

            Try
                '���
                If objOldData Is Nothing Then
                    strErrMsg = "����δ����Ҫɾ�������ݣ�"
                    GoTo errProc
                End If

                '������ʱ�ļ�
                Dim strTempFile As String = ""
                strTempFile = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_BDWJ), "")

                'ɾ������
                objOldData.Delete()

                'ɾ����ʱ�ļ�
                If strTempFile <> "" Then
                    If objBaseLocalFile.doDeleteFile(strErrMsg, strTempFile) = False Then
                        '�γ������ļ�
                    End If
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)

            '����
            doDeleteData_XGWJ = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ɾ��������_B_����������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     intJJXH              ���������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        ' �޸ļ�¼
        '      ����
        '----------------------------------------------------------------
        Public Overridable Function doDeleteData_Banli( _
            ByRef strErrMsg As String, _
            ByVal intJJXH As Integer) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction = Nothing
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Nothing
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand = Nothing
            Dim strSQL As String = ""

            '��ʼ��
            doDeleteData_Banli = False
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If

                '��ȡ����
                Dim strWJBS As String = Me.WJBS
                objSqlConnection = Me.SqlConnection

                '��ʼ����
                Try
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                'ɾ������
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ��SQL
                    strSQL = "delete from ����_B_���� where �ļ���ʶ = @wjbs and ������� = @jjxh"
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                    objSqlCommand.Parameters.AddWithValue("@jjxh", intJJXH)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()
                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '�ύ����
                objSqlTransaction.Commit()
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            '����
            doDeleteData_Banli = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �ڸ������������н�ָ����objSrcData�ƶ���ָ����objDesData
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objSrcData           ��Ҫ�ƶ�������
        '     objDesData           ��Ҫ�ƶ���������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doMoveTo_FJ( _
            ByRef strErrMsg As String, _
            ByRef objSrcData As System.Data.DataRow, _
            ByRef objDesData As System.Data.DataRow) As Boolean

            '��ʼ��
            doMoveTo_FJ = False
            strErrMsg = ""

            Try
                '���
                If objSrcData Is Nothing Then
                    strErrMsg = "����δ����Ҫ�ƶ������ݣ�"
                    GoTo errProc
                End If
                If objDesData Is Nothing Then
                    strErrMsg = "����δ����Ҫ�ƶ��������ݣ�"
                    GoTo errProc
                End If

                '�ƶ�
                Dim strField As String = Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_XSXH
                Dim objTemp As Object
                objTemp = objSrcData.Item(strField)
                objSrcData.Item(strField) = objDesData.Item(strField)
                objDesData.Item(strField) = objTemp

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            '����
            doMoveTo_FJ = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ������ļ������Ļ��������н�ָ����objSrcData�ƶ���ָ����objDesData
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objSrcData           ��Ҫ�ƶ�������
        '     objDesData           ��Ҫ�ƶ���������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doMoveTo_XGWJ( _
            ByRef strErrMsg As String, _
            ByRef objSrcData As System.Data.DataRow, _
            ByRef objDesData As System.Data.DataRow) As Boolean

            '��ʼ��
            doMoveTo_XGWJ = False
            strErrMsg = ""

            Try
                '���
                If objSrcData Is Nothing Then
                    strErrMsg = "����δ����Ҫ�ƶ������ݣ�"
                    GoTo errProc
                End If
                If objDesData Is Nothing Then
                    strErrMsg = "����δ����Ҫ�ƶ��������ݣ�"
                    GoTo errProc
                End If

                '�ƶ�
                Dim strField As String = Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_XSXH
                Dim objTemp As Object
                objTemp = objSrcData.Item(strField)
                objSrcData.Item(strField) = objDesData.Item(strField)
                objDesData.Item(strField) = objTemp

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            '����
            doMoveTo_XGWJ = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �ڸ��������������Զ�������ʾ���=���ݼ��е������+1
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objFJData            ����������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doAutoAdjustXSXH_FJ( _
            ByRef strErrMsg As String, _
            ByRef objFJData As Xydc.Platform.Common.Data.FlowData) As Boolean

            '��ʼ��
            doAutoAdjustXSXH_FJ = False
            strErrMsg = ""

            Try
                '���
                If objFJData Is Nothing Then
                    strErrMsg = "����δ�����ļ����ݣ�"
                    GoTo errProc
                End If

                '�Զ��������
                Dim strField As String = Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_XSXH
                Dim objTemp As Object
                Dim intCount As Integer
                Dim i As Integer
                With objFJData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_FUJIAN)
                    intCount = .DefaultView.Count
                    For i = 0 To intCount - 1 Step 1
                        .DefaultView.Item(i).Item(strField) = i + 1
                    Next
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            '����
            doAutoAdjustXSXH_FJ = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ������ļ������Ļ����������Զ�������ʾ���=���ݼ��е������+1
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objXGWJData          ����������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doAutoAdjustXSXH_XGWJ( _
            ByRef strErrMsg As String, _
            ByRef objXGWJData As Xydc.Platform.Common.Data.FlowData) As Boolean

            '��ʼ��
            doAutoAdjustXSXH_XGWJ = False
            strErrMsg = ""

            Try
                '���
                If objXGWJData Is Nothing Then
                    strErrMsg = "����δ�����ļ����ݣ�"
                    GoTo errProc
                End If

                '�Զ��������
                Dim strField As String = Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_XSXH
                Dim objTemp As Object
                Dim intCount As Integer
                Dim i As Integer
                With objXGWJData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_SHENPIWENJIAN_FUJIAN)
                    intCount = .DefaultView.Count
                    For i = 0 To intCount - 1 Step 1
                        .DefaultView.Item(i).Item(strField) = i + 1
                    Next
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            '����
            doAutoAdjustXSXH_XGWJ = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ����objJSRDataSet���з��ʹ���
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objJSRDataSet        �����������ݼ�
        '     strFSXH              ����������=�������
        '     strYJJH              �����������ǰ�����˵Ľ������
        '     intBLJB              �����������δ�������˵����˼���
        '     strAddedJJXHList     �����������ӵĽ������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doSend( _
            ByRef strErrMsg As String, _
            ByVal objJSRDataSet As Xydc.Platform.Common.Data.FlowData, _
            ByVal strFSXH As String, _
            ByVal strYJJH As String, _
            ByVal intBLJB As Integer, _
            ByRef strAddedJJXHList As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            Dim objValues As New System.Collections.Specialized.ListDictionary
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            doSend = False
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If objJSRDataSet Is Nothing Then
                    strErrMsg = "����δָ��[������]���ݣ�"
                    GoTo errProc
                End If
                If objJSRDataSet.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_VT_WENJIANFASONG) Is Nothing Then
                    strErrMsg = "����δָ��[������]���ݣ�"
                    GoTo errProc
                End If
                With objJSRDataSet.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_VT_WENJIANFASONG)
                    If .Rows.Count < 1 Then
                        strErrMsg = "����δָ��[������]���ݣ�"
                        GoTo errProc
                    End If
                End With
                If strFSXH Is Nothing Then strFSXH = ""
                strFSXH = strFSXH.Trim
                If strFSXH = "" Then
                    strErrMsg = "����δָ��[��������]���ݣ�"
                    GoTo errProc
                End If
                If strYJJH Is Nothing Then strYJJH = ""
                strYJJH = strYJJH.Trim
                If strYJJH = "" Then
                    strErrMsg = "����δָ��[ԭ�������]���ݣ�"
                    GoTo errProc
                End If
                If strAddedJJXHList Is Nothing Then strAddedJJXHList = ""
                strAddedJJXHList = strAddedJJXHList.Trim

                '��ȡ�ļ���Ϣ
                Dim strTaskStatusWJS As String = Me.FlowData.TASKSTATUS_WJS                
                Dim strBLLX As String = Me.FlowBLLXName                
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then
                    Exit Try
                End If

                '��ȡ��������
                objSqlConnection = Me.SqlConnection

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '�Ƿ�Ϊ��������
                Dim blnIsShenpiTask(2) As Boolean
                If Me.isShenpiTask(strErrMsg, "", intBLJB, blnIsShenpiTask(0)) = False Then
                    GoTo errProc
                End If

                '��������
                Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate
                Dim strRelaFields As String = "�ļ���ʶ" + strSep + "�������"
                Dim strRelaValue As String = strWJBS + strSep + strFSXH
                Dim intLevel As Integer
                Dim strJJBS As String
                Dim strJSXH As String
                Dim strJJXH As String
                Dim intCount As Integer
                Dim i As Integer
                With objJSRDataSet.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_VT_WENJIANFASONG)
                    intCount = .Rows.Count
                    For i = 0 To intCount - 1 Step 1
                        '��ȡ�½��ӵ���
                        If objdacCommon.getNewCode(strErrMsg, objSqlConnection, "�������", "�ļ���ʶ", strWJBS, "����_B_����", True, strJJXH) = False Then
                            GoTo errProc
                        End If

                        '����������
                        If objdacCommon.getNewCode(strErrMsg, objSqlConnection, "�������", strRelaFields, strRelaValue, "����_B_����", True, strJSXH) = False Then
                            GoTo errProc
                        End If

                        '���㽻�ӱ�ʶ
                        intLevel = CType(.Rows(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_VT_WENJIANFASONG_SYJB), Integer)
                        If Me.isShenpiTask(strErrMsg, "", intLevel, blnIsShenpiTask(1)) = False Then
                            GoTo errProc
                        End If
                        If blnIsShenpiTask(0) = False And blnIsShenpiTask(1) = False Then
                            '����ʾ���ҵĴ���������
                            '����1��������0��������1�����˻�0�����ջ�0��֪ͨ0����0
                            strJJBS = "10100000"
                        Else
                            If intBLJB < intLevel Then
                                '��ʾ���ҵĴ���������
                                '����1��������1��������1�����˻�0�����ջ�0��֪ͨ0����0
                                strJJBS = "11100000"
                            Else
                                '����ʾ���ҵĴ���������
                                '����1��������0��������1�����˻�0�����ջ�0��֪ͨ0����0
                                strJJBS = "10100000"
                            End If
                        End If

                        '���ü�¼��ֵ
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_WJBS, strWJBS)
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JJXH, CType(strJJXH, Integer))
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_YJJH, CType(strYJJH, Integer))
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSXH, CType(strFSXH, Integer))
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSR, objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_VT_WENJIANFASONG_FSR), ""))
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSRQ, .Rows(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_VT_WENJIANFASONG_FSRQ))
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSZZWJ, objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_VT_WENJIANFASONG_WJZZFS), ""))
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSDZWJ, objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_VT_WENJIANFASONG_WJDZFS), ""))
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSZZFJ, objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_VT_WENJIANFASONG_FJZZFS), ""))
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSDZFJ, objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_VT_WENJIANFASONG_FJDZFS), ""))
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSXH, CType(strJSXH, Integer))
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSR, objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_VT_WENJIANFASONG_JSR), ""))
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_XB, objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_VT_WENJIANFASONG_XB), ""))
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSRQ, System.DBNull.Value)
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSZZWJ, objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_VT_WENJIANFASONG_WJZZFS), ""))
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSDZWJ, objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_VT_WENJIANFASONG_WJDZFS), ""))
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSZZFJ, objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_VT_WENJIANFASONG_FJZZFS), ""))
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSDZFJ, objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_VT_WENJIANFASONG_FJDZFS), ""))
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_BLZHQX, objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_VT_WENJIANFASONG_BLQX), ""))
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_WCRQ, System.DBNull.Value)
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_WTR, objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_VT_WENJIANFASONG_WTR), ""))
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_BLLX, strBLLX)
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_BLZL, objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_VT_WENJIANFASONG_BLSY), ""))
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_BLZT, strTaskStatusWJS)
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JJBS, strJJBS)
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_SFDG, 0)
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JJSM, " ")
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_BWTX, 0)

                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JJBZ, " ")


                        '��ʼ����
                        objSqlTransaction = objSqlConnection.BeginTransaction
                        objSqlCommand.Transaction = objSqlTransaction

                        '������
                        Try
                            '��ղ���
                            objSqlCommand.Parameters.Clear()

                            '׼���ֶΡ�ֵ����
                            Dim objDictionaryEntry As System.Collections.DictionaryEntry
                            Dim strFields As String = ""
                            Dim strValues As String = ""
                            Dim j As Integer = 0
                            For Each objDictionaryEntry In objValues
                                If strFields = "" Then
                                    strFields = CType(objDictionaryEntry.Key, String)
                                    strValues = "@A" + j.ToString
                                Else
                                    strFields = strFields + "," + CType(objDictionaryEntry.Key, String)
                                    strValues = strValues + "," + "@A" + j.ToString
                                End If
                                objSqlCommand.Parameters.AddWithValue("@A" + j.ToString, objDictionaryEntry.Value)
                                j += 1
                            Next

                            '����SQL
                            strSQL = " insert into ����_B_���� (" + strFields + ") values (" + strValues + ")"

                            'ִ��
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()

                        Catch ex As Exception
                            objSqlTransaction.Rollback()
                            strErrMsg = ex.Message
                            GoTo errProc
                        End Try

                        '�ύ����
                        objSqlTransaction.Commit()

                        '��ջ�����
                        objValues.Clear()

                        '��¼���ӵĽ���
                        If strAddedJJXHList = "" Then
                            strAddedJJXHList = strJJXH
                        Else
                            strAddedJJXHList = strAddedJJXHList + "," + strJJXH
                        End If
                    Next
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objValues)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            doSend = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objValues)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ����strBLR�����˰������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strBLR               ����ǰ������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doSetTaskComplete( _
            ByRef strErrMsg As String, _
            ByVal strBLR As String) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            doSetTaskComplete = False
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strBLR Is Nothing Then strBLR = ""
                strBLR = strBLR.Trim
                If strBLR = "" Then
                    strErrMsg = "����δָ��[��ǰ������]���ݣ�"
                    GoTo errProc
                End If

                '��ȡ�ļ���Ϣ
                Dim strTaskStatusWJSList As String = Me.FlowData.TaskStatusWJSList
                Dim strTaskStatusZJBList As String = Me.FlowData.TaskStatusZJBList
                Dim strTaskStatusYWC As String = Me.FlowData.TASKSTATUS_YWC
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '��ȡ��������
                objSqlConnection = Me.SqlConnection

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '��ʼ����
                objSqlTransaction = objSqlConnection.BeginTransaction
                objSqlCommand.Transaction = objSqlTransaction

                'ִ������
                Try
                    'δ���յ����˰������
                    strSQL = ""
                    strSQL = strSQL + " update ����_B_���� set " + vbCr
                    strSQL = strSQL + "   �������� = '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "'," + vbCr
                    strSQL = strSQL + "   ����ֽ���ļ� = ����ֽ���ļ�," + vbCr
                    strSQL = strSQL + "   ���յ����ļ� = ���͵����ļ�," + vbCr
                    strSQL = strSQL + "   ����ֽ�ʸ��� = ����ֽ�ʸ���," + vbCr
                    strSQL = strSQL + "   ���յ��Ӹ��� = ���͵��Ӹ���," + vbCr
                    strSQL = strSQL + "   ����״̬ = '" + strTaskStatusYWC + "'," + vbCr
                    strSQL = strSQL + "   ������� = '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "' " + vbCr
                    strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "' " + vbCr
                    strSQL = strSQL + " and   ������   = '" + strBLR + "' " + vbCr
                    strSQL = strSQL + " and   ����״̬ in (" + strTaskStatusWJSList + ") " + vbCr
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.ExecuteNonQuery()

                    'δ��������˰������
                    strSQL = ""
                    strSQL = strSQL + " update ����_B_���� set " + vbCr
                    strSQL = strSQL + "   ����״̬ = '" + strTaskStatusYWC + "'," + vbCr
                    strSQL = strSQL + "   ������� = '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "' " + vbCr
                    strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "' " + vbCr
                    strSQL = strSQL + " and   ������   = '" + strBLR + "' " + vbCr
                    strSQL = strSQL + " and   ����״̬ in (" + strTaskStatusZJBList + ") " + vbCr
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '�ύ����
                objSqlTransaction.Commit()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            doSetTaskComplete = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ����strBLR�����˰������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strBLR               ����ǰ������
        '     strNewJJXHList       ������������ϵĽ��ӵ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doSetTaskComplete( _
            ByRef strErrMsg As String, _
            ByVal strBLR As String, _
            ByVal strNewJJXHList As String) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            doSetTaskComplete = False
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strBLR Is Nothing Then strBLR = ""
                strBLR = strBLR.Trim
                If strBLR = "" Then
                    strErrMsg = "����δָ��[��ǰ������]���ݣ�"
                    GoTo errProc
                End If
                If strNewJJXHList Is Nothing Then strNewJJXHList = ""
                strNewJJXHList = strNewJJXHList.Trim

                '��ȡ�ļ���Ϣ
                Dim strTaskStatusWJSList As String = Me.FlowData.TaskStatusWJSList
                Dim strTaskStatusZJBList As String = Me.FlowData.TaskStatusZJBList
                Dim strTaskStatusYWC As String = Me.FlowData.TASKSTATUS_YWC
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '��ȡ��������
                objSqlConnection = Me.SqlConnection

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '��ʼ����
                objSqlTransaction = objSqlConnection.BeginTransaction
                objSqlCommand.Transaction = objSqlTransaction

                'ִ������
                Try
                    'δ���յ����˰������
                    strSQL = ""
                    strSQL = strSQL + " update ����_B_���� set " + vbCr
                    strSQL = strSQL + "   �������� = '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "'," + vbCr
                    strSQL = strSQL + "   ����ֽ���ļ� = ����ֽ���ļ�," + vbCr
                    strSQL = strSQL + "   ���յ����ļ� = ���͵����ļ�," + vbCr
                    strSQL = strSQL + "   ����ֽ�ʸ��� = ����ֽ�ʸ���," + vbCr
                    strSQL = strSQL + "   ���յ��Ӹ��� = ���͵��Ӹ���," + vbCr
                    strSQL = strSQL + "   ����״̬ = '" + strTaskStatusYWC + "'," + vbCr
                    strSQL = strSQL + "   ������� = '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "' " + vbCr
                    strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "' " + vbCr
                    strSQL = strSQL + " and   ������   = '" + strBLR + "' " + vbCr
                    strSQL = strSQL + " and   ����״̬ in (" + strTaskStatusWJSList + ") " + vbCr
                    If strNewJJXHList <> "" Then
                        strSQL = strSQL + " and   ������� not in (" + strNewJJXHList + ")" + vbCr
                    End If
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.ExecuteNonQuery()

                    'δ��������˰������
                    strSQL = ""
                    strSQL = strSQL + " update ����_B_���� set " + vbCr
                    strSQL = strSQL + "   ����״̬ = '" + strTaskStatusYWC + "'," + vbCr
                    strSQL = strSQL + "   ������� = '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "' " + vbCr
                    strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "' " + vbCr
                    strSQL = strSQL + " and   ������   = '" + strBLR + "' " + vbCr
                    strSQL = strSQL + " and   ����״̬ in (" + strTaskStatusZJBList + ") " + vbCr
                    If strNewJJXHList <> "" Then
                        strSQL = strSQL + " and   ������� not in (" + strNewJJXHList + ")" + vbCr
                    End If
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '�ύ����
                objSqlTransaction.Commit()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            doSetTaskComplete = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���û����strBLR�ı�������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strBLR               ����ǰ������
        '     blnBWTX              ��True-���ñ������ѣ�False-�����������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doSetTaskBWTX( _
            ByRef strErrMsg As String, _
            ByVal strBLR As String, _
            ByVal blnBWTX As Boolean) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            doSetTaskBWTX = False
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strBLR Is Nothing Then strBLR = ""
                strBLR = strBLR.Trim
                If strBLR = "" Then
                    strErrMsg = "����δָ��[��ǰ������]���ݣ�"
                    GoTo errProc
                End If

                '��ȡ�ļ���Ϣ
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '��ȡ��������
                objSqlConnection = Me.SqlConnection

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '��ʼ����
                objSqlTransaction = objSqlConnection.BeginTransaction
                objSqlCommand.Transaction = objSqlTransaction

                'ִ������
                Try
                    If blnBWTX = True Then
                        strSQL = ""
                        strSQL = strSQL + " update ����_B_���� set " + vbCr
                        strSQL = strSQL + "   �������� = 1 " + vbCr
                        strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "'" + vbCr
                        strSQL = strSQL + " and   ������   = '" + strBLR + "'" + vbCr
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.ExecuteNonQuery()
                    Else
                        strSQL = ""
                        strSQL = strSQL + " update ����_B_���� set " + vbCr
                        strSQL = strSQL + "   �������� = 0 " + vbCr
                        strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "'" + vbCr
                        strSQL = strSQL + " and   ������   = '" + strBLR + "'" + vbCr
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.ExecuteNonQuery()
                    End If

                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '�ύ����
                objSqlTransaction.Commit()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            doSetTaskBWTX = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���͸���ǰ�����˵��й���Ա���ͻظ�֪ͨ( < intMaxJJXH)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strBLR               ����ǰ������
        '     intMaxJJXH           �������η���ǰ���Ľ������
        '     strFSXH              ����������=�������
        '     strAddedJJXHList     �����������ӵĽ������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doSendReply( _
            ByRef strErrMsg As String, _
            ByVal strBLR As String, _
            ByVal intMaxJJXH As Integer, _
            ByVal strFSXH As String, _
            ByRef strAddedJJXHList As String) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            Dim objJSR As New System.Collections.Specialized.NameValueCollection
            Dim objValues As New System.Collections.Specialized.ListDictionary
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            doSendReply = False
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strBLR Is Nothing Then strBLR = ""
                strBLR = strBLR.Trim
                If strBLR = "" Then
                    strErrMsg = "����δָ��[��ǰ������]���ݣ�"
                    GoTo errProc
                End If
                If strFSXH Is Nothing Then strFSXH = ""
                strFSXH = strFSXH.Trim
                If strFSXH = "" Then
                    strErrMsg = "����δָ��[��������]���ݣ�"
                    GoTo errProc
                End If
                If strAddedJJXHList Is Nothing Then strAddedJJXHList = ""
                strAddedJJXHList = strAddedJJXHList.Trim

                '��ȡ�ļ���Ϣ                
                Dim strBLLX As String = Me.FlowBLLXName
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '��ȡ��������
                objSqlConnection = Me.SqlConnection

                '��ȡҪ�ظ�����Ա��Ϣ
                strSQL = ""
                strSQL = strSQL + " select * from ����_B_����" + vbCr
                strSQL = strSQL + " where �ļ���ʶ  = '" + strWJBS + "'" + vbCr                '��ǰ�ļ�
                strSQL = strSQL + " and   ������    = '" + strBLR + "'" + vbCr                 '������
                strSQL = strSQL + " and   ������   <> '" + strBLR + "'" + vbCr                 '�����˲��ǵ�ǰ������
                strSQL = strSQL + " and   �������  <  " + intMaxJJXH.ToString + " " + vbCr    '���η���֮ǰ������
                strSQL = strSQL + " and   rtrim(���ӱ�ʶ) like '__1__0_%'" + vbCr                     '�������ܿ�+��֪ͨ��
                strSQL = strSQL + " order by ������� desc"
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables.Count < 1 Then
                    Exit Try
                End If

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '��������
                Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate
                Dim strRelaFields As String = "�ļ���ʶ" + strSep + "�������"
                Dim strRelaValue As String = strWJBS + strSep + strFSXH
                Dim strJJBS As String
                Dim strJSXH As String
                Dim strJJXH As String
                Dim strJSR As String
                Dim intCount As Integer
                Dim i As Integer
                With objDataSet.Tables(0)
                    intCount = .Rows.Count
                    For i = 0 To intCount - 1 Step 1
                        strJSR = CType(.Rows(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSR), String)
                        If objJSR(strJSR) Is Nothing Then
                            objJSR.Add(strJSR, strJSR)
                        Else
                            '���ظ����ͣ�
                            GoTo nextRY
                        End If

                        '��ȡ�½��ӵ���
                        If objdacCommon.getNewCode(strErrMsg, objSqlConnection, "�������", "�ļ���ʶ", strWJBS, "����_B_����", True, strJJXH) = False Then
                            GoTo errProc
                        End If

                        '����������
                        If objdacCommon.getNewCode(strErrMsg, objSqlConnection, "�������", strRelaFields, strRelaValue, "����_B_����", True, strJSXH) = False Then
                            GoTo errProc
                        End If

                        '���㽻�ӱ�ʶ
                        '�����˿������������˿�����֪ͨ��Ϣ
                        '����1��������0��������1�����˻�0�����ջ�0��֪ͨ1����0
                        strJJBS = "10100100"

                        '���ü�¼��ֵ
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_WJBS, strWJBS)
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JJXH, CType(strJJXH, Integer))
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_YJJH, .Rows(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JJXH))
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSXH, CType(strFSXH, Integer))
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSR, strBLR)
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSRQ, Format(Now, "yyyy-MM-dd HH:mm:ss"))
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSZZWJ, 0)
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSDZWJ, 1)
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSZZFJ, 0)
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSDZFJ, 1)
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSXH, CType(strJSXH, Integer))
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSR, .Rows(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSR))
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_XB, System.DBNull.Value)
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSRQ, System.DBNull.Value)
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSZZWJ, 0)
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSDZWJ, 1)
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSZZFJ, 0)
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSDZFJ, 1)
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_BLZHQX, Format(Now.AddDays(3), "yyyy-MM-dd HH:mm:ss"))
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_WCRQ, System.DBNull.Value)
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_WTR, " ")
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_BLLX, strBLLX)
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_BLZL, Me.FlowData.TASK_HFTZ)
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_BLZT, Me.FlowData.TASKSTATUS_WJS)
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JJBS, strJJBS)
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_SFDG, 0)
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JJSM, " ")
                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_BWTX, 0)


                        objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JJBZ, " ")


                        '��ʼ����
                        objSqlTransaction = objSqlConnection.BeginTransaction
                        objSqlCommand.Transaction = objSqlTransaction

                        '������
                        Try
                            '��ղ���
                            objSqlCommand.Parameters.Clear()

                            '׼���ֶΡ�ֵ����
                            Dim objDictionaryEntry As System.Collections.DictionaryEntry
                            Dim strFields As String = ""
                            Dim strValues As String = ""
                            Dim j As Integer = 0
                            For Each objDictionaryEntry In objValues
                                If strFields = "" Then
                                    strFields = CType(objDictionaryEntry.Key, String)
                                    strValues = "@A" + j.ToString
                                Else
                                    strFields = strFields + "," + CType(objDictionaryEntry.Key, String)
                                    strValues = strValues + "," + "@A" + j.ToString
                                End If
                                objSqlCommand.Parameters.AddWithValue("@A" + j.ToString, objDictionaryEntry.Value)
                                j += 1
                            Next

                            '����SQL
                            strSQL = " insert into ����_B_���� (" + strFields + ") values (" + strValues + ")"

                            'ִ��
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()

                        Catch ex As Exception
                            objSqlTransaction.Rollback()
                            strErrMsg = ex.Message
                            GoTo errProc
                        End Try

                        '�ύ����
                        objSqlTransaction.Commit()

                        '��ջ�����
                        objValues.Clear()

                        '��¼����Ľ���
                        If strAddedJJXHList = "" Then
                            strAddedJJXHList = strJJXH
                        Else
                            strAddedJJXHList = strAddedJJXHList + "," + strJJXH
                        End If
nextRY:
                    Next
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objValues)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objJSR)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            doSendReply = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objValues)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objJSR)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ɾ��ָ��������ŵĽ�������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strAddedJJXHList     �������ӵĽ������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doDeleteJiaojie( _
            ByRef strErrMsg As String, _
            ByVal strAddedJJXHList As String) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            doDeleteJiaojie = False
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strAddedJJXHList Is Nothing Then strAddedJJXHList = ""
                strAddedJJXHList = strAddedJJXHList.Trim
                If strAddedJJXHList = "" Then
                    Exit Try
                End If

                '��ȡ�ļ���Ϣ
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then
                    Exit Try
                End If

                '��ȡ��������
                objSqlConnection = Me.SqlConnection

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '��ʼ����
                objSqlTransaction = objSqlConnection.BeginTransaction
                objSqlCommand.Transaction = objSqlTransaction

                '������
                Try
                    strSQL = ""
                    strSQL = strSQL + " delete from ����_B_����" + vbCr
                    strSQL = strSQL + " where �ļ���ʶ  = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + " and   ������� in (" + strAddedJJXHList + ")" + vbCr
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '�ύ����
                objSqlTransaction.Commit()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            doDeleteJiaojie = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡĿǰΪֹ���Ľ������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     intMaxJJXH           ������ĿǰΪֹ���Ľ������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getMaxJJXH( _
            ByRef strErrMsg As String, _
            ByRef intMaxJJXH As Integer) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            getMaxJJXH = False
            intMaxJJXH = 0
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If

                '��ȡ�ļ���Ϣ
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then
                    Exit Try
                End If

                '��ȡ��������
                objSqlConnection = Me.SqlConnection

                '��ȡҪ�ظ�����Ա��Ϣ
                strSQL = ""
                strSQL = strSQL + " select isnull(max(�������),0)" + vbCr
                strSQL = strSQL + " from ����_B_����" + vbCr
                strSQL = strSQL + " where �ļ���ʶ  = '" + strWJBS + "'" + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables.Count < 1 Then
                    Exit Try
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    Exit Try
                End If
                intMaxJJXH = CType(objDataSet.Tables(0).Rows(0).Item(0), Integer)

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getMaxJJXH = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡstrUserXM׼��Ҫ���յ��ļ�������Ϣ���ݼ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ����ǰ����Ա����
        '     strWhere             ����������
        '     objJieshouDataSet    ������Ҫ���յ��ļ�������Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getJieshouDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByVal strWhere As String, _
            ByRef objJieshouDataSet As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim objTempJieshouDataSet As Xydc.Platform.Common.Data.FlowData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            getJieshouDataSet = False
            objJieshouDataSet = Nothing
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strTaskStatusWJSList As String = Me.FlowData.TaskStatusWJSList
                Dim strWJBS As String = Me.WJBS

                '�������ݼ�
                objTempJieshouDataSet = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_VT_WENJIANJIESHOU)
                If strWJBS = "" Then Exit Try

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                'ִ�м���
                With Me.m_objSqlDataAdapter
                    '��ȡû�н��յĽ��Ӵ���
                    strSQL = ""
                    strSQL = strSQL + " select a.*" + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select " + vbCr
                    strSQL = strSQL + "     a.������," + vbCr
                    strSQL = strSQL + "     a.��������," + vbCr
                    strSQL = strSQL + "     �������� = case when substring(a.���ӱ�ʶ,4,1)='1' then '" + Me.FlowData.TASK_THCL + "'" + vbCr
                    strSQL = strSQL + "                     when substring(a.���ӱ�ʶ,5,1)='1' then '" + Me.FlowData.TASK_SHCL + "'" + vbCr
                    strSQL = strSQL + "                     when substring(a.���ӱ�ʶ,7,1)='1' then '" + Me.FlowData.TASK_HFCL + "'" + vbCr
                    strSQL = strSQL + "                     else a.�������� end," + vbCr
                    strSQL = strSQL + "     a.��������," + vbCr
                    strSQL = strSQL + "     ����ֽ���ļ����� = a.����ֽ���ļ�," + vbCr
                    strSQL = strSQL + "     ���������ļ����� = a.���͵����ļ�," + vbCr
                    strSQL = strSQL + "     ����ֽ�ʸ������� = a.����ֽ�ʸ���," + vbCr
                    strSQL = strSQL + "     �������Ӹ������� = a.���͵��Ӹ���," + vbCr
                    strSQL = strSQL + "     ����ֽ���ļ����� = a.����ֽ���ļ�," + vbCr
                    strSQL = strSQL + "     ���յ����ļ����� = a.���͵����ļ�," + vbCr
                    strSQL = strSQL + "     ����ֽ�ʸ������� = a.����ֽ�ʸ���," + vbCr
                    strSQL = strSQL + "     ���յ��Ӹ������� = a.���͵��Ӹ���," + vbCr
                    strSQL = strSQL + "     a.�������," + vbCr
                    strSQL = strSQL + "     a.�������," + vbCr
                    strSQL = strSQL + "     a.ԭ���Ӻ�," + vbCr
                    strSQL = strSQL + "     a.���ӱ�ʶ," + vbCr
                    strSQL = strSQL + "     a.Э��," + vbCr
                    strSQL = strSQL + "     �����˰������� = b.��������," + vbCr
                    strSQL = strSQL + "     ������Э��     = b.Э��" + vbCr
                    strSQL = strSQL + "   from" + vbCr
                    strSQL = strSQL + "   (" + vbCr
                    strSQL = strSQL + "     select * from ����_B_���� " + vbCr
                    strSQL = strSQL + "     where �ļ���ʶ =  '" + strWJBS + "'" + vbCr                 '��ǰ�ļ�
                    strSQL = strSQL + "     and   ������   =  '" + strUserXM + "'" + vbCr               'strUserXM׼������
                    strSQL = strSQL + "     and   rtrim(���ӱ�ʶ) like '__1%'" + vbCr                          '�������ܿ���
                    strSQL = strSQL + "     and   ����״̬ in (" + strTaskStatusWJSList + ")" + vbCr    '������δ���յ�
                    strSQL = strSQL + "   ) a" + vbCr
                    strSQL = strSQL + "   left join" + vbCr
                    strSQL = strSQL + "   (" + vbCr
                    strSQL = strSQL + "     select *" + vbCr
                    strSQL = strSQL + "     from ����_B_����" + vbCr
                    strSQL = strSQL + "     where �ļ���ʶ = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + "   ) b on a.ԭ���Ӻ� = b.�������" + vbCr
                    strSQL = strSQL + " ) a" + vbCr
                    If strWhere <> "" Then
                        strSQL = strSQL + " where " + strWhere + vbCr
                    End If
                    strSQL = strSQL + " order by a.�������� desc" + vbCr

                    '���ò���
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    'ִ�в���
                    .Fill(objTempJieshouDataSet.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_VT_WENJIANJIESHOU))
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            objJieshouDataSet = objTempJieshouDataSet
            getJieshouDataSet = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.FlowData.SafeRelease(objTempJieshouDataSet)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���ݸ������������ļ�(1�����ӵ�)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objJiaojieData       ��Ҫ׼�����µĽ�������(�ļ���ʶ��������ű���)
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doReceiveFile( _
            ByRef strErrMsg As String, _
            ByVal objJiaojieData As System.Collections.Specialized.NameValueCollection) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            doReceiveFile = False
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If objJiaojieData Is Nothing Then
                    Exit Try
                End If
                If objJiaojieData.Count < 1 Then
                    Exit Try
                End If

                '��ȡ�ļ���Ϣ
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then
                    Exit Try
                End If

                '��ȡ��������
                objSqlConnection = Me.SqlConnection

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '��ʼ����
                objSqlTransaction = objSqlConnection.BeginTransaction
                objSqlCommand.Transaction = objSqlTransaction

                '������
                Try
                    Dim strFields As String = ""
                    Dim strValue As String
                    Dim intJJXH As Integer
                    Dim intCount As Integer
                    Dim i As Integer

                    '��ȡ�������
                    intJJXH = objPulicParameters.getObjectValue(objJiaojieData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JJXH), 0)

                    '׼��SQL����
                    objSqlCommand.Parameters.Clear()
                    intCount = objJiaojieData.Count
                    For i = 0 To intCount - 1 Step 1
                        If strFields = "" Then
                            strFields = objJiaojieData.GetKey(i) + " = @A" + i.ToString
                        Else
                            strFields = strFields + "," + vbCr + objJiaojieData.GetKey(i) + " = @A" + i.ToString
                        End If
                        Select Case objJiaojieData.GetKey(i)
                            Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JJXH, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_YJJH, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSXH, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JJXH, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSZZWJ, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSDZWJ, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSZZFJ, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSDZFJ, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSXH, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSZZWJ, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSDZWJ, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSZZFJ, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSDZFJ, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_SFDG, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_BWTX
                                strValue = objJiaojieData(i)
                                If strValue Is Nothing Then strValue = ""
                                strValue = strValue.Trim
                                If strValue = "" Then strValue = "0"
                                objSqlCommand.Parameters.AddWithValue("@A" + i.ToString, CType(strValue, Integer))

                            Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSRQ, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSRQ, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_BLZHQX, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_WCRQ
                                strValue = objJiaojieData(i)
                                If strValue Is Nothing Then strValue = ""
                                strValue = strValue.Trim
                                If strValue = "" Then
                                    objSqlCommand.Parameters.AddWithValue("@A" + i.ToString, System.DBNull.Value)
                                Else
                                    objSqlCommand.Parameters.AddWithValue("@A" + i.ToString, CType(strValue, System.DateTime))
                                End If

                            Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_BLZT
                                strValue = Me.FlowData.TASKSTATUS_ZJB
                                objSqlCommand.Parameters.AddWithValue("@A" + i.ToString, strValue)

                            Case Else
                                strValue = objJiaojieData(i)
                                If strValue Is Nothing Then strValue = ""
                                strValue = strValue.Trim
                                If strValue = "" Then strValue = " "
                                objSqlCommand.Parameters.AddWithValue("@A" + i.ToString, strValue)
                        End Select
                    Next
                    objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                    objSqlCommand.Parameters.AddWithValue("@jjxh", intJJXH)

                    '׼��SQL
                    strSQL = ""
                    strSQL = strSQL + " update ����_B_���� set " + vbCr
                    strSQL = strSQL + "   " + strFields + vbCr
                    strSQL = strSQL + " where �ļ���ʶ = @wjbs" + vbCr
                    strSQL = strSQL + " and   ������� = @jjxh" + vbCr

                    'ִ��
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '�ύ����
                objSqlTransaction.Commit()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            doReceiveFile = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���ݸ��������˻��ļ����Զ������˻�֪ͨ(1�����ӵ�)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strYBLSY             �����ν��ӵķ������Լ��İ�������
        '     strYXB               ��ԭЭ���־
        '     strFSXH              �����������κ�
        '     objJiaojieData       ��Ҫ�˻صĽ�������(�ļ���ʶ��������ű���)
        '     blnCanReadFile       �������Ķ��ļ�Ȩ��
        '     objHasSendNoticeRY   ��(����)�ѷ��˻�֪ͨ����Ա�б�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doTuihuiFile( _
            ByRef strErrMsg As String, _
            ByVal strYBLSY As String, _
            ByVal strYXB As String, _
            ByVal strFSXH As String, _
            ByVal objJiaojieData As System.Collections.Specialized.NameValueCollection, _
            ByVal blnCanReadFile As Boolean, _
            ByRef objHasSendNoticeRY As System.Collections.Specialized.NameValueCollection) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objValues As New System.Collections.Specialized.ListDictionary
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            doTuihuiFile = False
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If objJiaojieData Is Nothing Then
                    Exit Try
                End If
                If objJiaojieData.Count < 1 Then
                    Exit Try
                End If
                If strFSXH Is Nothing Then strFSXH = ""
                strFSXH = strFSXH.Trim
                If strFSXH = "" Then strFSXH = "0"
                If strYBLSY Is Nothing Then strYBLSY = ""
                strYBLSY = strYBLSY.Trim
                If strYXB Is Nothing Then strYXB = ""
                strYXB = strYXB.Trim
                If strYXB = "" Then strYXB = objPulicParameters.CharFalse

                '��ȡ�ļ���Ϣ                
                Dim strBLLX As String = Me.FlowBLLXName
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then
                    Exit Try
                End If

                '��ȡ��������
                objSqlConnection = Me.SqlConnection

                '��ȡ�½��ӵ���
                Dim strSep As String = objPulicParameters.CharSeparate
                Dim strJJXH As String
                If objdacCommon.getNewCode(strErrMsg, objSqlConnection, "�������", "�ļ���ʶ", strWJBS, "����_B_����", True, strJJXH) = False Then
                    GoTo errProc
                End If
                '����������
                Dim strRelaFields As String = "�ļ���ʶ" + strSep + "�������"
                Dim strRelaValue As String = strWJBS + strSep + strFSXH
                Dim strJSXH As String
                If objdacCommon.getNewCode(strErrMsg, objSqlConnection, "�������", strRelaFields, strRelaValue, "����_B_����", True, strJSXH) = False Then
                    GoTo errProc
                End If

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '��ʼ����
                objSqlTransaction = objSqlConnection.BeginTransaction
                objSqlCommand.Transaction = objSqlTransaction

                '������
                Try
                    Dim strFields As String = ""
                    Dim strValues As String = ""
                    Dim strValue As String
                    Dim intJJXH As Integer
                    Dim strFSR As String
                    Dim strJSR As String
                    Dim intCount As Integer
                    Dim i As Integer

                    '��ȡ�������
                    intJJXH = objPulicParameters.getObjectValue(objJiaojieData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JJXH), 0)
                    strJSR = objPulicParameters.getObjectValue(objJiaojieData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSR), "")
                    strFSR = objPulicParameters.getObjectValue(objJiaojieData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSR), "")

                    '�˻ش���
                    objSqlCommand.Parameters.Clear()
                    intCount = objJiaojieData.Count
                    strFields = ""
                    strValues = ""
                    For i = 0 To intCount - 1 Step 1
                        If strFields = "" Then
                            strFields = objJiaojieData.GetKey(i) + " = @A" + i.ToString
                        Else
                            strFields = strFields + "," + vbCr + objJiaojieData.GetKey(i) + " = @A" + i.ToString
                        End If
                        Select Case objJiaojieData.GetKey(i)
                            Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JJXH, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_YJJH, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSXH, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JJXH, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSZZWJ, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSDZWJ, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSZZFJ, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSDZFJ, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSXH, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSZZWJ, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSDZWJ, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSZZFJ, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSDZFJ, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_SFDG, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_BWTX
                                strValue = objJiaojieData(i)
                                If strValue Is Nothing Then strValue = ""
                                strValue = strValue.Trim
                                If strValue = "" Then strValue = "0"
                                objSqlCommand.Parameters.AddWithValue("@A" + i.ToString, CType(strValue, Integer))
                            Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSRQ, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSRQ, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_BLZHQX, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_WCRQ
                                strValue = objJiaojieData(i)
                                If strValue Is Nothing Then strValue = ""
                                strValue = strValue.Trim
                                If strValue = "" Then
                                    objSqlCommand.Parameters.AddWithValue("@A" + i.ToString, System.DBNull.Value)
                                Else
                                    objSqlCommand.Parameters.AddWithValue("@A" + i.ToString, CType(strValue, System.DateTime))
                                End If
                            Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JJBS
                                If blnCanReadFile = True Then
                                    '����1��������1��������1�����˻�0�����ջ�0��֪ͨ0����0
                                    strValue = "11100000"
                                Else
                                    '����1��������1��������0�����˻�0�����ջ�0��֪ͨ0����0
                                    strValue = "11000000"
                                End If
                                objSqlCommand.Parameters.AddWithValue("@A" + i.ToString, strValue)
                            Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_BLZT
                                '���˻�
                                strValue = Me.FlowData.TASKSTATUS_BTH
                                objSqlCommand.Parameters.AddWithValue("@A" + i.ToString, strValue)
                            Case Else
                                strValue = objJiaojieData(i)
                                If strValue Is Nothing Then strValue = ""
                                strValue = strValue.Trim
                                If strValue = "" Then strValue = " "
                                objSqlCommand.Parameters.AddWithValue("@A" + i.ToString, strValue)
                        End Select
                    Next
                    objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                    objSqlCommand.Parameters.AddWithValue("@jjxh", intJJXH)
                    strSQL = ""
                    strSQL = strSQL + " update ����_B_���� set " + vbCr
                    strSQL = strSQL + "   " + strFields + vbCr
                    strSQL = strSQL + " where �ļ���ʶ = @wjbs" + vbCr
                    strSQL = strSQL + " and   ������� = @jjxh" + vbCr
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '���˻ش���
                    If strFSR = "" Then
                        Exit Try
                    End If
                    If Not (objHasSendNoticeRY Is Nothing) Then
                        If Not (objHasSendNoticeRY(strFSR) Is Nothing) Then
                            '�Ѿ�����
                            Exit Try
                        End If
                    End If
                    '���㽻�ӱ�ʶ
                    '�����˿������������˿��������˻أ��ظ�
                    '����1��������0��������1�����˻�1�����ջ�0��֪ͨ0����0
                    Dim strJJBS As String
                    strJJBS = "10110000"
                    '���ü�¼��ֵ
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_WJBS, strWJBS)
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JJXH, CType(strJJXH, Integer))
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_YJJH, intJJXH)
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSXH, CType(strFSXH, Integer))
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSR, strJSR)
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSRQ, Format(Now, "yyyy-MM-dd HH:mm:ss"))
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSZZWJ, CType(objJiaojieData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSZZWJ), Integer))
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSDZWJ, CType(objJiaojieData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSDZWJ), Integer))
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSZZFJ, CType(objJiaojieData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSZZFJ), Integer))
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSDZFJ, CType(objJiaojieData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSDZFJ), Integer))
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSXH, CType(strJSXH, Integer))
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSR, strFSR)
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_XB, strYXB)
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSRQ, System.DBNull.Value)
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSZZWJ, CType(objJiaojieData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSZZWJ), Integer))
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSDZWJ, CType(objJiaojieData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSDZWJ), Integer))
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSZZFJ, CType(objJiaojieData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSZZFJ), Integer))
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSDZFJ, CType(objJiaojieData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSDZFJ), Integer))
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_BLZHQX, Format(Now.AddDays(3), "yyyy-MM-dd HH:mm:ss"))
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_WCRQ, System.DBNull.Value)
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_WTR, " ")
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_BLLX, strBLLX)
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_BLZL, strYBLSY)
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_BLZT, Me.FlowData.TASKSTATUS_WJS)
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JJBS, strJJBS)
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_SFDG, 0)
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JJSM, " ")
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_BWTX, 0)

                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JJBZ, " ")


                    '��ղ���
                    objSqlCommand.Parameters.Clear()
                    '׼���ֶΡ�ֵ����
                    Dim objDictionaryEntry As System.Collections.DictionaryEntry
                    Dim j As Integer = 0
                    strFields = ""
                    strValues = ""
                    For Each objDictionaryEntry In objValues
                        If strFields = "" Then
                            strFields = CType(objDictionaryEntry.Key, String)
                            strValues = "@A" + j.ToString
                        Else
                            strFields = strFields + "," + CType(objDictionaryEntry.Key, String)
                            strValues = strValues + "," + "@A" + j.ToString
                        End If
                        objSqlCommand.Parameters.AddWithValue("@A" + j.ToString, objDictionaryEntry.Value)
                        j += 1
                    Next
                    '����SQL
                    strSQL = " insert into ����_B_���� (" + strFields + ") values (" + strValues + ")"
                    'ִ��
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '�����ѷ��˻ش�������Ա�б�
                    If objHasSendNoticeRY Is Nothing Then
                        objHasSendNoticeRY = New System.Collections.Specialized.NameValueCollection
                    End If
                    objHasSendNoticeRY.Add(strFSR, strFSR)

                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '�ύ����
                objSqlTransaction.Commit()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objValues)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            doTuihuiFile = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objValues)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡstrUserXM׼��Ҫ�ջص��ļ�������Ϣ���ݼ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ����ǰ����Ա����
        '     strWhere             ����������
        '     objShouhuiDataSet    ������Ҫ�ջص��ļ�������Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getShouhuiDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByVal strWhere As String, _
            ByRef objShouhuiDataSet As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim objTempShouhuiDataSet As Xydc.Platform.Common.Data.FlowData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            getShouhuiDataSet = False
            objShouhuiDataSet = Nothing
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strTaskStatusYWCList As String = Me.FlowData.TaskStatusYWCList
                Dim strWJBS As String = Me.WJBS

                '�������ݼ�
                objTempShouhuiDataSet = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_VT_WENJIANSHOUHUI)
                If strWJBS = "" Then Exit Try

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                'ִ�м���
                With Me.m_objSqlDataAdapter
                    '��ȡ�ѷ���+������û�н��յĽ��Ӵ���
                    strSQL = ""
                    strSQL = strSQL + " select a.*" + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select " + vbCr
                    strSQL = strSQL + "     a.������," + vbCr
                    strSQL = strSQL + "     �������� = a.��������," + vbCr
                    strSQL = strSQL + "     a.��������," + vbCr
                    strSQL = strSQL + "     ����ֽ���ļ����� = a.����ֽ���ļ�," + vbCr
                    strSQL = strSQL + "     ���͵����ļ����� = a.���͵����ļ�," + vbCr
                    strSQL = strSQL + "     ����ֽ�ʸ������� = a.����ֽ�ʸ���," + vbCr
                    strSQL = strSQL + "     ���͵��Ӹ������� = a.���͵��Ӹ���," + vbCr
                    strSQL = strSQL + "     a.��������," + vbCr
                    strSQL = strSQL + "     ����ֽ���ļ����� = a.����ֽ���ļ�," + vbCr
                    strSQL = strSQL + "     ���յ����ļ����� = a.���͵����ļ�," + vbCr
                    strSQL = strSQL + "     ����ֽ�ʸ������� = a.����ֽ�ʸ���," + vbCr
                    strSQL = strSQL + "     ���յ��Ӹ������� = a.���͵��Ӹ���," + vbCr
                    strSQL = strSQL + "     a.�������," + vbCr
                    strSQL = strSQL + "     a.�������," + vbCr
                    strSQL = strSQL + "     a.ԭ���Ӻ�," + vbCr
                    strSQL = strSQL + "     a.���ӱ�ʶ," + vbCr
                    strSQL = strSQL + "     a.������," + vbCr
                    strSQL = strSQL + "     a.Э��," + vbCr
                    strSQL = strSQL + "     a.�Ƿ����," + vbCr
                    strSQL = strSQL + "     �����˰������� = b.��������," + vbCr
                    strSQL = strSQL + "     ������Э��     = b.Э��" + vbCr
                    strSQL = strSQL + "   from" + vbCr
                    strSQL = strSQL + "   (" + vbCr
                    strSQL = strSQL + "     select * from ����_B_���� " + vbCr
                    strSQL = strSQL + "     where �ļ���ʶ =  '" + strWJBS + "'" + vbCr                  '��ǰ�ļ�
                    strSQL = strSQL + "     and   ������   =  '" + strUserXM + "'" + vbCr                'strUserXM����

                    strSQL = strSQL + "     and   ������   <>    '" + strUserXM + "'" + vbCr                'strUserXM����

                    strSQL = strSQL + "     and   rtrim(���ӱ�ʶ) like '1_1__0%'" + vbCr                        '�ѷ���+�������ܿ�+��֪ͨ
                    strSQL = strSQL + "     and   ����״̬ not in (" + strTaskStatusYWCList + ")" + vbCr '������δ����
                    strSQL = strSQL + "   ) a" + vbCr
                    strSQL = strSQL + "   left join" + vbCr
                    strSQL = strSQL + "   (" + vbCr
                    strSQL = strSQL + "     select *" + vbCr
                    strSQL = strSQL + "     from ����_B_����" + vbCr
                    strSQL = strSQL + "     where �ļ���ʶ = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + "   ) b on a.ԭ���Ӻ� = b.�������" + vbCr
                    strSQL = strSQL + " ) a" + vbCr
                    If strWhere <> "" Then
                        strSQL = strSQL + " where " + strWhere + vbCr
                    End If
                    strSQL = strSQL + " order by a.�������� desc" + vbCr

                    '���ò���
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    'ִ�в���
                    .Fill(objTempShouhuiDataSet.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_VT_WENJIANSHOUHUI))
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            objShouhuiDataSet = objTempShouhuiDataSet
            getShouhuiDataSet = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.FlowData.SafeRelease(objTempShouhuiDataSet)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���ݸ��������ջ��ļ���������Ҫ�����ջ�֪ͨ(1�����ӵ�)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strFSXH              �����������κ�
        '     objJiaojieData       ��Ҫ�ջصĽ�������(�ļ���ʶ��������ű���)
        '     blnSendNotice        ���Ƿ�Ҫ�����ջ�֪ͨ
        '     objHasSendNoticeRY   ��(����)�ѷ��ջ�֪ͨ����Ա�б�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doShouhuiFile( _
            ByRef strErrMsg As String, _
            ByVal strFSXH As String, _
            ByVal objJiaojieData As System.Collections.Specialized.NameValueCollection, _
            ByVal blnSendNotice As Boolean, _
            ByRef objHasSendNoticeRY As System.Collections.Specialized.NameValueCollection) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objValues As New System.Collections.Specialized.ListDictionary
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            doShouhuiFile = False
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If objJiaojieData Is Nothing Then
                    Exit Try
                End If
                If objJiaojieData.Count < 1 Then
                    Exit Try
                End If
                If strFSXH Is Nothing Then strFSXH = ""
                strFSXH = strFSXH.Trim
                If strFSXH = "" Then strFSXH = "0"

                '��ȡ�ļ���Ϣ                
                Dim strBLLX As String = Me.FlowBLLXName
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then
                    Exit Try
                End If

                '��ȡ��������
                objSqlConnection = Me.SqlConnection

                '��ȡ�½��ӵ���
                Dim strSep As String = objPulicParameters.CharSeparate
                Dim strJJXH As String
                If objdacCommon.getNewCode(strErrMsg, objSqlConnection, "�������", "�ļ���ʶ", strWJBS, "����_B_����", True, strJJXH) = False Then
                    GoTo errProc
                End If
                '����������
                Dim strRelaFields As String = "�ļ���ʶ" + strSep + "�������"
                Dim strRelaValue As String = strWJBS + strSep + strFSXH
                Dim strJSXH As String
                If objdacCommon.getNewCode(strErrMsg, objSqlConnection, "�������", strRelaFields, strRelaValue, "����_B_����", True, strJSXH) = False Then
                    GoTo errProc
                End If

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '��ʼ����
                objSqlTransaction = objSqlConnection.BeginTransaction
                objSqlCommand.Transaction = objSqlTransaction

                '������
                Try
                    Dim strFields As String = ""
                    Dim strValues As String = ""
                    Dim strValue As String
                    Dim intYJJH As Integer
                    Dim intJJXH As Integer
                    Dim strFSR As String
                    Dim strJSR As String
                    Dim intCount As Integer
                    Dim i As Integer

                    '��ȡ�������
                    intJJXH = objPulicParameters.getObjectValue(objJiaojieData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JJXH), 0)
                    intYJJH = objPulicParameters.getObjectValue(objJiaojieData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_YJJH), 0)
                    strJSR = objPulicParameters.getObjectValue(objJiaojieData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSR), "")
                    strFSR = objPulicParameters.getObjectValue(objJiaojieData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSR), "")

                    '�ջش���
                    objSqlCommand.Parameters.Clear()
                    intCount = objJiaojieData.Count
                    strFields = ""
                    strValues = ""
                    For i = 0 To intCount - 1 Step 1
                        If strFields = "" Then
                            strFields = objJiaojieData.GetKey(i) + " = @A" + i.ToString
                        Else
                            strFields = strFields + "," + vbCr + objJiaojieData.GetKey(i) + " = @A" + i.ToString
                        End If
                        Select Case objJiaojieData.GetKey(i)
                            Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JJXH, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_YJJH, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSXH, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JJXH, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSZZWJ, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSDZWJ, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSZZFJ, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSDZFJ, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSXH, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSZZWJ, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSDZWJ, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSZZFJ, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSDZFJ, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_SFDG, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_BWTX
                                strValue = objJiaojieData(i)
                                If strValue Is Nothing Then strValue = ""
                                strValue = strValue.Trim
                                If strValue = "" Then strValue = "0"
                                objSqlCommand.Parameters.AddWithValue("@A" + i.ToString, CType(strValue, Integer))
                            Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSRQ, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSRQ, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_BLZHQX, _
                                Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_WCRQ
                                strValue = objJiaojieData(i)
                                If strValue Is Nothing Then strValue = ""
                                strValue = strValue.Trim
                                If strValue = "" Then
                                    objSqlCommand.Parameters.AddWithValue("@A" + i.ToString, System.DBNull.Value)
                                Else
                                    objSqlCommand.Parameters.AddWithValue("@A" + i.ToString, CType(strValue, System.DateTime))
                                End If
                            Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JJBS
                                '����1��������1��������0�����˻�0�����ջ�0��֪ͨ0����0
                                strValue = "11000000"
                                objSqlCommand.Parameters.AddWithValue("@A" + i.ToString, strValue)
                            Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_BLZT
                                '���ջ�
                                strValue = Me.FlowData.TASKSTATUS_BSH
                                objSqlCommand.Parameters.AddWithValue("@A" + i.ToString, strValue)
                            Case Else
                                strValue = objJiaojieData(i)
                                If strValue Is Nothing Then strValue = ""
                                strValue = strValue.Trim
                                If strValue = "" Then strValue = " "
                                objSqlCommand.Parameters.AddWithValue("@A" + i.ToString, strValue)
                        End Select
                    Next
                    objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                    objSqlCommand.Parameters.AddWithValue("@jjxh", intJJXH)
                    strSQL = ""
                    strSQL = strSQL + " update ����_B_���� set " + vbCr
                    strSQL = strSQL + "   " + strFields + vbCr
                    strSQL = strSQL + " where �ļ���ʶ = @wjbs" + vbCr
                    strSQL = strSQL + " and   ������� = @jjxh" + vbCr
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '����ԭ���ӵ���״̬Ϊ�����ڰ���+���������ܿ���+���������ˡ�
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@blzt", Me.FlowData.TASKSTATUS_ZJB)
                    objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                    objSqlCommand.Parameters.AddWithValue("@jjxh", intYJJH)
                    strSQL = ""
                    strSQL = strSQL + " update ����_B_���� set " + vbCr
                    strSQL = strSQL + "   ����״̬ = @blzt, " + vbCr
                    strSQL = strSQL + "   ���ӱ�ʶ = substring(���ӱ�ʶ,1,2) + '100000'" + vbCr
                    strSQL = strSQL + " where �ļ���ʶ = @wjbs" + vbCr
                    strSQL = strSQL + " and   ������� = @jjxh" + vbCr
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '�����ջ�֪ͨ
                    If blnSendNotice = False Then
                        Exit Try
                    End If
                    If strJSR = "" Then
                        Exit Try
                    End If
                    If Not (objHasSendNoticeRY Is Nothing) Then
                        If Not (objHasSendNoticeRY(strJSR) Is Nothing) Then
                            '�Ѿ�����
                            Exit Try
                        End If
                    End If
                    '���㽻�ӱ�ʶ
                    '����1��������0��������1�����˻�0�����ջ�0��֪ͨ1����0
                    Dim strJJBS As String
                    strJJBS = "10100100"
                    '���ü�¼��ֵ
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_WJBS, strWJBS)
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JJXH, CType(strJJXH, Integer))
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_YJJH, intJJXH)
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSXH, CType(strFSXH, Integer))
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSR, strFSR)
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSRQ, Format(Now, "yyyy-MM-dd HH:mm:ss"))
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSZZWJ, 0)
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSDZWJ, 1)
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSZZFJ, 0)
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSDZFJ, 1)
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSXH, CType(strJSXH, Integer))
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSR, strJSR)
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_XB, objPulicParameters.CharFalse)
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSRQ, System.DBNull.Value)
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSZZWJ, 0)
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSDZWJ, 1)
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSZZFJ, 0)
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSDZFJ, 1)
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_BLZHQX, Format(Now.AddDays(3), "yyyy-MM-dd HH:mm:ss"))
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_WCRQ, System.DBNull.Value)
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_WTR, " ")
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_BLLX, strBLLX)
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_BLZL, Me.FlowData.TASK_SHTZ)
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_BLZT, Me.FlowData.TASKSTATUS_WJS)
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JJBS, strJJBS)
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_SFDG, 0)
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JJSM, " ")
                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_BWTX, 0)

                    objValues.Add(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JJBZ, " ")


                    '��ղ���
                    objSqlCommand.Parameters.Clear()
                    '׼���ֶΡ�ֵ����
                    Dim objDictionaryEntry As System.Collections.DictionaryEntry
                    Dim j As Integer = 0
                    strFields = ""
                    strValues = ""
                    For Each objDictionaryEntry In objValues
                        If strFields = "" Then
                            strFields = CType(objDictionaryEntry.Key, String)
                            strValues = "@A" + j.ToString
                        Else
                            strFields = strFields + "," + CType(objDictionaryEntry.Key, String)
                            strValues = strValues + "," + "@A" + j.ToString
                        End If
                        objSqlCommand.Parameters.AddWithValue("@A" + j.ToString, objDictionaryEntry.Value)
                        j += 1
                    Next
                    '����SQL
                    strSQL = " insert into ����_B_���� (" + strFields + ") values (" + strValues + ")"
                    'ִ��
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '�����ѷ��ջ�֪ͨ���б�
                    If objHasSendNoticeRY Is Nothing Then
                        objHasSendNoticeRY = New System.Collections.Specialized.NameValueCollection
                    End If
                    objHasSendNoticeRY.Add(strJSR, strJSR)

                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '�ύ����
                objSqlTransaction.Commit()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objValues)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            doShouhuiFile = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objValues)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' strUserXM�Ƿ����ڱ༭�ļ�?
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ���û�����
        '     blnDo                �������Ƿ����ڱ༭�ļ�?
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function isEditFile( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByRef blnDo As Boolean) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            isEditFile = False
            blnDo = False
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '��ȡ����
                strSQL = ""
                strSQL = strSQL + " select a.*" + vbCr
                strSQL = strSQL + " from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select * from ����_B_�ļ����� " + vbCr
                strSQL = strSQL + "   where �ļ���ʶ = '" + strWJBS + "' " + vbCr
                strSQL = strSQL + " ) a" + vbCr
                strSQL = strSQL + " left join" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select ��Ա���� from ����_B_��Ա" + vbCr
                strSQL = strSQL + "   where ��Ա���� = '" + strUserXM + "'" + vbCr
                strSQL = strSQL + " ) b on a.��Ա���� = b.��Ա����" + vbCr
                strSQL = strSQL + " where b.��Ա���� is not null" + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    blnDo = True
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            isEditFile = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡstrUserXM׼��Ҫ�˻ص��ļ�������Ϣ���ݼ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ����ǰ����Ա����
        '     strWhere             ����������
        '     objTuihuiDataSet     ������Ҫ�˻ص��ļ�������Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getTuihuiDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByVal strWhere As String, _
            ByRef objTuihuiDataSet As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim objTempTuihuiDataSet As Xydc.Platform.Common.Data.FlowData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            getTuihuiDataSet = False
            objTuihuiDataSet = Nothing
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strTaskStatusWJSList As String = Me.FlowData.TaskStatusWJSList
                Dim strWJBS As String = Me.WJBS

                '�������ݼ�
                objTempTuihuiDataSet = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_VT_WENJIANTUIHUI)
                If strWJBS = "" Then Exit Try

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                'ִ�м���
                With Me.m_objSqlDataAdapter
                    '��ȡ���͸�strUserXM���������Ӵ���
                    strSQL = ""
                    strSQL = strSQL + " select a.*" + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select " + vbCr
                    strSQL = strSQL + "     a.������," + vbCr
                    strSQL = strSQL + "     a.��������," + vbCr
                    strSQL = strSQL + "     �������� = case when substring(a.���ӱ�ʶ,4,1)='1' then '" + Me.FlowData.TASK_THCL + "'" + vbCr
                    strSQL = strSQL + "                     when substring(a.���ӱ�ʶ,5,1)='1' then '" + Me.FlowData.TASK_SHCL + "'" + vbCr
                    strSQL = strSQL + "                     when substring(a.���ӱ�ʶ,7,1)='1' then '" + Me.FlowData.TASK_HFCL + "'" + vbCr
                    strSQL = strSQL + "                     else a.�������� end," + vbCr
                    strSQL = strSQL + "     a.��������," + vbCr
                    strSQL = strSQL + "     ����ֽ���ļ����� = a.����ֽ���ļ�," + vbCr
                    strSQL = strSQL + "     ���������ļ����� = a.���͵����ļ�," + vbCr
                    strSQL = strSQL + "     ����ֽ�ʸ������� = a.����ֽ�ʸ���," + vbCr
                    strSQL = strSQL + "     �������Ӹ������� = a.���͵��Ӹ���," + vbCr
                    strSQL = strSQL + "     ����ֽ���ļ����� = a.����ֽ���ļ�," + vbCr
                    strSQL = strSQL + "     ���յ����ļ����� = a.���͵����ļ�," + vbCr
                    strSQL = strSQL + "     ����ֽ�ʸ������� = a.����ֽ�ʸ���," + vbCr
                    strSQL = strSQL + "     ���յ��Ӹ������� = a.���͵��Ӹ���," + vbCr
                    strSQL = strSQL + "     a.�������," + vbCr
                    strSQL = strSQL + "     a.�������," + vbCr
                    strSQL = strSQL + "     a.ԭ���Ӻ�," + vbCr
                    strSQL = strSQL + "     a.���ӱ�ʶ," + vbCr
                    strSQL = strSQL + "     a.Э��," + vbCr
                    strSQL = strSQL + "     �����˰������� = b.��������," + vbCr
                    strSQL = strSQL + "     ������Э��     = b.Э��" + vbCr
                    strSQL = strSQL + "   from" + vbCr
                    strSQL = strSQL + "   (" + vbCr
                    strSQL = strSQL + "     select * from ����_B_���� " + vbCr
                    strSQL = strSQL + "     where �ļ���ʶ =  '" + strWJBS + "'" + vbCr                 '��ǰ�ļ�
                    strSQL = strSQL + "     and   ������   =  '" + strUserXM + "'" + vbCr               'strUserXM����
                    strSQL = strSQL + "     and   rtrim(���ӱ�ʶ) like '__1__0%'" + vbCr                       '�������ܿ���+��֪ͨ
                    strSQL = strSQL + "   ) a" + vbCr
                    strSQL = strSQL + "   left join" + vbCr
                    strSQL = strSQL + "   (" + vbCr
                    strSQL = strSQL + "     select *" + vbCr
                    strSQL = strSQL + "     from ����_B_����" + vbCr
                    strSQL = strSQL + "     where �ļ���ʶ = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + "   ) b on a.ԭ���Ӻ� = b.�������" + vbCr
                    strSQL = strSQL + " ) a" + vbCr
                    If strWhere <> "" Then
                        strSQL = strSQL + " where " + strWhere + vbCr
                    End If
                    strSQL = strSQL + " order by a.�������� desc" + vbCr

                    '���ò���
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    'ִ�в���
                    .Fill(objTempTuihuiDataSet.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_VT_WENJIANTUIHUI))
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            objTuihuiDataSet = objTempTuihuiDataSet
            getTuihuiDataSet = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.FlowData.SafeRelease(objTempTuihuiDataSet)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ɡ������Ķ�֪ͨ��������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ���û�����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doIReadFile( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            doIReadFile = False
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim

                '��ȡ�ļ���Ϣ
                Dim strTaskStatusYWCList As String = Me.FlowData.TaskStatusYWCList
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then
                    Exit Try
                End If

                '��ȡ��������
                objSqlConnection = Me.SqlConnection

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '��ʼ����
                objSqlTransaction = objSqlConnection.BeginTransaction
                objSqlCommand.Transaction = objSqlTransaction

                '������
                Try
                    '��������Ϣ
                    strSQL = ""
                    strSQL = strSQL + " update ����_B_���� set" + vbCr
                    strSQL = strSQL + "   �������� = '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "'," + vbCr
                    strSQL = strSQL + "   ����ֽ���ļ� = ����ֽ���ļ�," + vbCr
                    strSQL = strSQL + "   ���յ����ļ� = ���͵����ļ�," + vbCr
                    strSQL = strSQL + "   ����ֽ�ʸ��� = ����ֽ�ʸ���," + vbCr
                    strSQL = strSQL + "   ���յ��Ӹ��� = ���͵��Ӹ��� " + vbCr
                    strSQL = strSQL + "where �ļ���ʶ = '" + strWJBS + "'" + vbCr                    '��ǰ�ļ�
                    strSQL = strSQL + "and   ������   = '" + strUserXM + "'" + vbCr                  '������
                    strSQL = strSQL + "and   rtrim(���ӱ�ʶ) like '_____1%' " + vbCr                        '֪ͨ��
                    strSQL = strSQL + "and   ����״̬ not in (" + strTaskStatusYWCList + ")" + vbCr  'δ���
                    strSQL = strSQL + "and   �������� is null" + vbCr                                'δ����
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.ExecuteNonQuery()

                    '����Ϊ���
                    strSQL = ""
                    strSQL = strSQL + " update ����_B_���� set" + vbCr
                    strSQL = strSQL + "   ����״̬ = '" + Me.FlowData.TASKSTATUS_YYD + "'," + vbCr
                    strSQL = strSQL + "   ������� = '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "'" + vbCr
                    strSQL = strSQL + "where �ļ���ʶ = '" + strWJBS + "'" + vbCr                    '��ǰ�ļ�
                    strSQL = strSQL + "and   ������   = '" + strUserXM + "'" + vbCr                  '������
                    strSQL = strSQL + "and   rtrim(���ӱ�ʶ) like '_____1%' " + vbCr                        '֪ͨ��
                    strSQL = strSQL + "and   ����״̬ not in (" + strTaskStatusYWCList + ")" + vbCr  'δ���
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '�ύ����
                objSqlTransaction.Commit()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            doIReadFile = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ɡ��Ҳ��ô���������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ���û�����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doIDoNotProcess( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            doIDoNotProcess = False
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim

                '��ȡ�ļ���Ϣ
                Dim strTaskStatusYWCList As String = Me.FlowData.TaskStatusYWCList
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then
                    Exit Try
                End If

                '��ȡ��������
                objSqlConnection = Me.SqlConnection

                '�Ƿ����˻صĻ��ջص�����
                strSQL = ""
                strSQL = strSQL + " select * from ����_B_����" + vbCr
                strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "'" + vbCr                       '��ǰ�ļ�
                strSQL = strSQL + " and   ������   = '" + strUserXM + "'" + vbCr                     '������
                strSQL = strSQL + " and  (rtrim(���ӱ�ʶ) like '___1%' or rtrim(���ӱ�ʶ) like '____1%')" + vbCr   '���ջػ��˻�
                strSQL = strSQL + " and   rtrim(���ӱ�ʶ) like '_____0%'" + vbCr                            '��֪ͨ��
                strSQL = strSQL + " and   ����״̬ not in (" + strTaskStatusYWCList + ")" + vbCr     'δ���
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    strErrMsg = "���󣺸��ļ�û���˻ظ��Ұ�����û�б��ջأ�"
                    GoTo errProc
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '����Ƿ�Ϊ���1���ڰ���Ա(��֪ͨ)
                strSQL = ""
                strSQL = strSQL + " select * from ����_B_����" + vbCr
                strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "'" + vbCr                     '��ǰ�ļ�
                strSQL = strSQL + " and   ������   <> '" + strUserXM + "'" + vbCr                  '�����˷�ָ����
                strSQL = strSQL + " and   rtrim(���ӱ�ʶ) like '_____0%'" + vbCr                          '��֪ͨ
                strSQL = strSQL + " and   ����״̬ not in (" + strTaskStatusYWCList + ")" + vbCr   'δ���
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    'Ŀǰû���������ڰ�
                    Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                    objDataSet = Nothing
                    strSQL = ""
                    strSQL = strSQL + " select * from ����_B_����" + vbCr
                    strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "'" + vbCr                           '��ǰ�ļ�
                    strSQL = strSQL + " and   ������   = '" + strUserXM + "'" + vbCr                         '������
                    strSQL = strSQL + " and   not (rtrim(���ӱ�ʶ) like '___1%' or rtrim(���ӱ�ʶ) like '____1%')" + vbCr  '�����˻صĻ��ջص�
                    strSQL = strSQL + " and   ����״̬ not in (" + strTaskStatusYWCList + ")" + vbCr         'δ����
                    If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                        GoTo errProc
                    End If
                    If objDataSet.Tables(0).Rows.Count < 1 Then
                        strErrMsg = "�������Ǳ��ļ���Ψһ���ڰ��ˣ�����ֱ��ʹ��[���ô���]����ʹ��[����]��������"
                        GoTo errProc
                    End If
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '��ʼ����
                objSqlTransaction = objSqlConnection.BeginTransaction
                objSqlCommand.Transaction = objSqlTransaction

                '������
                Try
                    strSQL = ""
                    strSQL = strSQL + " update ����_B_���� set" + vbCr
                    strSQL = strSQL + "   ����״̬ = '" + Me.FlowData.TASKSTATUS_BYB + "'," + vbCr
                    strSQL = strSQL + "   ������� = '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "'" + vbCr
                    strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "'" + vbCr                       '��ǰ�ļ�
                    strSQL = strSQL + " and   ������   = '" + strUserXM + "'" + vbCr                     '������
                    strSQL = strSQL + " and  (rtrim(���ӱ�ʶ) like '___1%' or rtrim(���ӱ�ʶ) like '____1%')" + vbCr   '���ջػ��˻�
                    strSQL = strSQL + " and   rtrim(���ӱ�ʶ) like '_____0%'" + vbCr                            '��֪ͨ��
                    strSQL = strSQL + " and   ����״̬ not in (" + strTaskStatusYWCList + ")" + vbCr     'δ���
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '�ύ����
                objSqlTransaction.Commit()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            doIDoNotProcess = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ɡ��Ҵ�����ϡ�������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ���û�����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doICompleteTask( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            doICompleteTask = False
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim

                '��ȡ�ļ���Ϣ
                Dim strTaskStatusYWCList As String = Me.FlowData.TaskStatusYWCList
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then
                    Exit Try
                End If

                '��ȡ��������
                objSqlConnection = Me.SqlConnection

                '����Ƿ�Ϊ���1���ڰ���Ա(��֪ͨ)
                strSQL = ""
                strSQL = strSQL + " select * from ����_B_����" + vbCr
                strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "'" + vbCr                     '��ǰ�ļ�
                strSQL = strSQL + " and   ������   <> '" + strUserXM + "'" + vbCr                  '�����˷�ָ����
                strSQL = strSQL + " and   rtrim(���ӱ�ʶ) like '_____0%'" + vbCr                          '��֪ͨ
                strSQL = strSQL + " and   ����״̬ not in (" + strTaskStatusYWCList + ")" + vbCr   'δ���
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    'Ŀǰû���������ڰ�
                    strErrMsg = "���󣺱��ļ�ֻ����һ�����ڴ���������ֱ������Ϊ[�������]�����뽻���͸����˼���������͸�ר�˽��а�ᴦ��"
                    GoTo errProc
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '��ʼ����
                objSqlTransaction = objSqlConnection.BeginTransaction
                objSqlCommand.Transaction = objSqlTransaction

                '������
                Try
                    strSQL = ""
                    strSQL = strSQL + " update ����_B_���� set" + vbCr
                    strSQL = strSQL + "   ����״̬ = '" + Me.FlowData.TASKSTATUS_YWC + "'," + vbCr
                    strSQL = strSQL + "   ������� = '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "'" + vbCr
                    strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "'" + vbCr                       '��ǰ�ļ�
                    strSQL = strSQL + " and   ������   = '" + strUserXM + "'" + vbCr                     '������
                    strSQL = strSQL + " and   rtrim(���ӱ�ʶ) like '_____0%'" + vbCr                            '��֪ͨ��
                    strSQL = strSQL + " and   ����״̬ not in (" + strTaskStatusYWCList + ")" + vbCr     'δ���
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '�ύ����
                objSqlTransaction.Commit()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            doICompleteTask = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ��strUserXM����������û�а�����ϵ���Ա�б�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ���û�����
        '     strUserList          ��(����)û�а�����ϵ���Ա�б�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getUncompleteTaskRY( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByRef strUserList As String) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            getUncompleteTaskRY = False
            strUserList = ""
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim

                '��ȡ�ļ���Ϣ
                Dim strTaskStatusYWCList As String = Me.FlowData.TaskStatusYWCList
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then
                    Exit Try
                End If

                '��ȡ��������
                objSqlConnection = Me.SqlConnection

                '���û����ɵ���Ա�����Լ��⣩
                strSQL = ""
                strSQL = strSQL + " select * from ����_B_����" + vbCr
                strSQL = strSQL + " where �ļ���ʶ =  '" + strWJBS + "'" + vbCr                    '��ǰ�ļ�
                strSQL = strSQL + " and   ������   <> '" + strUserXM + "'" + vbCr                  '�����˲���ָ����
                strSQL = strSQL + " and   rtrim(���ӱ�ʶ) like '__1__0%'" + vbCr                          '�������ܿ���+��֪ͨ
                strSQL = strSQL + " and   ����״̬ not in (" + strTaskStatusYWCList + ")" + vbCr   'δ����
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If

                '����
                Dim intCount As Integer
                Dim strValue As String
                Dim i As Integer
                With objDataSet.Tables(0)
                    'û��δ�����������Ա
                    If .Rows.Count < 1 Then
                        Exit Try
                    End If
                    intCount = .Rows.Count
                    For i = 0 To intCount - 1 Step 1
                        strValue = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSR), "")
                        If strValue <> "" Then
                            If strUserList = "" Then
                                strUserList = strValue
                            Else
                                strUserList = strUserList + objPulicParameters.CharSeparate + strValue
                            End If
                        End If
                    Next
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getUncompleteTaskRY = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡstrUserXM�Ŀ��Դ߰���Щ���ӵ�?
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ����Ա����
        '     objKeCuibanData      �����ؿ��Դ߰�Ľ��ӵ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getKeCuibanData( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByRef objKeCuibanData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempKeCuibanData As Xydc.Platform.Common.Data.FlowData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            getKeCuibanData = False
            objKeCuibanData = Nothing
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '���Ӱ������״̬SQL�б�

                '��ȡ�ļ���ʶ
                Dim strTaskStatusYWCList As String = Me.FlowData.TaskStatusYWCList
                objSqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '�������ݼ�
                objTempKeCuibanData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_CUIBAN_JIAOJIE)
                If strWJBS = "" Then Exit Try

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                'ִ�м���
                With Me.m_objSqlDataAdapter
                    '��ȡ�ɴ߰�Ľ�����Ϣ
                    strSQL = ""
                    strSQL = strSQL + " select a.*" + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select" + vbCr
                    strSQL = strSQL + "     �ļ���ʶ," + vbCr
                    strSQL = strSQL + "     �������," + vbCr
                    strSQL = strSQL + "     �߰���� = 0," + vbCr
                    strSQL = strSQL + "     �߰��� = ������," + vbCr
                    strSQL = strSQL + "     �߰����� = getdate()," + vbCr
                    strSQL = strSQL + "     ���߰��� = ������," + vbCr
                    strSQL = strSQL + "     �߰�˵�� = '�뾡�촦��'," + vbCr
                    strSQL = strSQL + "     ��������," + vbCr
                    strSQL = strSQL + "     ����״̬" + vbCr
                    strSQL = strSQL + "   from ����_B_����" + vbCr
                    strSQL = strSQL + "   where �ļ���ʶ = '" + strWJBS + "' " + vbCr                      '��ǰ�ļ�
                    strSQL = strSQL + "   and   ������   = '" + strUserXM + "' " + vbCr                    '�ҷ���
                    strSQL = strSQL + "   and   rtrim(���ӱ�ʶ) like '1_1__0%' " + vbCr                           '���͹�+��֪ͨ+�����˿ɼ�
                    strSQL = strSQL + "   and   ����״̬ not in (" + strTaskStatusYWCList + ") " + vbCr    'δ����
                    strSQL = strSQL + " ) a" + vbCr
                    strSQL = strSQL + " order by a.�߰����" + vbCr

                    '���ò���
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    'ִ�в���
                    .Fill(objTempKeCuibanData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_CUIBAN_JIAOJIE))
                End With

                '���á��߰���š�
                Dim intCount As Integer
                Dim i As Integer
                With objTempKeCuibanData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_CUIBAN_JIAOJIE)
                    intCount = .Rows.Count
                    For i = 0 To intCount - 1 Step 1
                        .Rows(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_CUIBAN_CBXH) = i + 1
                    Next
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            objKeCuibanData = objTempKeCuibanData
            getKeCuibanData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.FlowData.SafeRelease(objTempKeCuibanData)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡָ����ԱstrUserXM�Ĵ߰�����
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ����Ա����
        '     objCuibanData        ����������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getCuibanData( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByRef objCuibanData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim objTempCuibanData As Xydc.Platform.Common.Data.FlowData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            getCuibanData = False
            objCuibanData = Nothing
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '�������ݼ�
                objTempCuibanData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_CUIBAN_JIAOJIE)
                If strWJBS = "" Then Exit Try

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                'ִ�м���
                With Me.m_objSqlDataAdapter
                    '��ȡָ����Ա�߰�����
                    strSQL = ""
                    strSQL = strSQL + " select a.*" + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select a.*," + vbCr
                    strSQL = strSQL + "     b.��������, b.����״̬" + vbCr
                    strSQL = strSQL + "   from" + vbCr
                    strSQL = strSQL + "   (" + vbCr
                    strSQL = strSQL + "     select * from ����_B_�߰�" + vbCr
                    strSQL = strSQL + "     where �ļ���ʶ = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + "     and   �߰���   = '" + strUserXM + "'" + vbCr
                    strSQL = strSQL + "   ) a " + vbCr
                    strSQL = strSQL + "   left join" + vbCr
                    strSQL = strSQL + "   (" + vbCr
                    strSQL = strSQL + "     select * from ����_B_����" + vbCr
                    strSQL = strSQL + "     where �ļ���ʶ = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + "   ) b on a.�ļ���ʶ = b.�ļ���ʶ and a.������� = b.������� " + vbCr
                    strSQL = strSQL + " ) a" + vbCr
                    strSQL = strSQL + " order by a.�߰���� " + vbCr

                    '���ò���
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    'ִ�в���
                    .Fill(objTempCuibanData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_CUIBAN_JIAOJIE))
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            objCuibanData = objTempCuibanData
            getCuibanData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.FlowData.SafeRelease(objTempCuibanData)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ�ļ��Ĵ߰�����
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objCuibanData        ����������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getCuibanData( _
            ByRef strErrMsg As String, _
            ByRef objCuibanData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim objTempCuibanData As Xydc.Platform.Common.Data.FlowData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            getCuibanData = False
            objCuibanData = Nothing
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '�������ݼ�
                objTempCuibanData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_CUIBAN_JIAOJIE)
                If strWJBS = "" Then Exit Try

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                'ִ�м���
                With Me.m_objSqlDataAdapter
                    '��ȡ�߰�����
                    strSQL = ""
                    strSQL = strSQL + " select a.*" + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select a.*," + vbCr
                    strSQL = strSQL + "     b.��������, b.����״̬" + vbCr
                    strSQL = strSQL + "   from" + vbCr
                    strSQL = strSQL + "   (" + vbCr
                    strSQL = strSQL + "     select * from ����_B_�߰�" + vbCr
                    strSQL = strSQL + "     where �ļ���ʶ = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + "   ) a " + vbCr
                    strSQL = strSQL + "   left join" + vbCr
                    strSQL = strSQL + "   (" + vbCr
                    strSQL = strSQL + "     select * from ����_B_����" + vbCr
                    strSQL = strSQL + "     where �ļ���ʶ = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + "   ) b on a.�ļ���ʶ = b.�ļ���ʶ and a.������� = b.������� " + vbCr
                    strSQL = strSQL + " ) a" + vbCr
                    strSQL = strSQL + " order by a.�߰����� desc" + vbCr

                    '���ò���
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    'ִ�в���
                    .Fill(objTempCuibanData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_CUIBAN_JIAOJIE))
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            objCuibanData = objTempCuibanData
            getCuibanData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.FlowData.SafeRelease(objTempCuibanData)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �жϴ߰����������Ƿ���Ч��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objOldData           ����¼��ֵ
        '     objNewData           ����¼��ֵ(�����Ƽ�ֵ)
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doVerifyCuiban( _
            ByRef strErrMsg As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByRef objNewData As System.Collections.Specialized.NameValueCollection) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            Dim strTable As String = Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_CUIBAN_JIAOJIE

            doVerifyCuiban = False

            Try
                '���
                If Me.IsInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "����δ����ҪУ������ݣ�"
                    GoTo errProc
                End If

                '��ȡ������Ϣ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strSep As String = objPulicParameters.CharSeparate
                Dim strWJBS As String = Me.FlowData.WJBS

                '��ȡ��ṹ����
                strSQL = "select top 0 * from ����_B_�߰�"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, objSqlConnection, strSQL, "����_B_�߰�", objDataSet) = False Then
                    GoTo errProc
                End If

                '������ݳ���
                Dim intCount As Integer = objNewData.Count
                Dim strField As String
                Dim strValue As String
                Dim intLen As Integer
                Dim i As Integer
                For i = 0 To intCount - 1 Step 1
                    strField = objNewData.GetKey(i).Trim()
                    strValue = objNewData.Item(i).Trim()
                    Select Case strField
                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_CUIBAN_WJBS
                            strValue = strWJBS

                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_CUIBAN_CBXH
                            Dim strCBXH As String = ""
                            If objOldData Is Nothing Then
                                '�Զ�����
                                strValue = objNewData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_CUIBAN_JJXH)
                                If objdacCommon.getNewCode(strErrMsg, objSqlConnection, "�߰����", "�ļ���ʶ" + strSep + "�������", strWJBS + strSep + strValue, "����_B_�߰�", True, strCBXH) = False Then
                                    GoTo errProc
                                End If
                                strValue = strCBXH
                            Else
                                If strValue = "" Then
                                    strErrMsg = "����[" + strField + "]����Ϊ�գ�"
                                    GoTo errProc
                                End If
                                If objPulicParameters.isIntegerString(strValue) = False Then
                                    strErrMsg = "����[" + strField + "]���������֣�"
                                    GoTo errProc
                                End If
                                intLen = CType(strValue, Integer)
                                If intLen < 1 Or intLen > 999999 Then
                                    strErrMsg = "����[" + strField + "]������[1,999999]��"
                                    GoTo errProc
                                End If
                                strValue = intLen.ToString()
                            End If

                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_CUIBAN_JJXH
                            If strValue = "" Then
                                strErrMsg = "����[" + strField + "]����Ϊ�գ�"
                                GoTo errProc
                            End If
                            If objPulicParameters.isIntegerString(strValue) = False Then
                                strErrMsg = "����[" + strField + "]���������֣�"
                                GoTo errProc
                            End If
                            intLen = CType(strValue, Integer)
                            If intLen < 1 Or intLen > 999999 Then
                                strErrMsg = "����[" + strField + "]������[1,999999]��"
                                GoTo errProc
                            End If
                            strValue = intLen.ToString()

                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_CUIBAN_CBR, _
                            Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_CUIBAN_BCBR
                            If strValue = "" Then
                                strErrMsg = "����[" + strField + "]����Ϊ�գ�"
                                GoTo errProc
                            End If
                            With objDataSet.Tables(0).Columns(strField)
                                intLen = objPulicParameters.getStringLength(strValue)
                                If intLen > .MaxLength Then
                                    strErrMsg = "����[" + strField + "]���Ȳ��ܳ���[" + .MaxLength.ToString() + "]��ʵ����[" + intLen.ToString() + "]��"
                                    GoTo errProc
                                End If
                            End With

                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_CUIBAN_CBRQ
                            If strValue = "" Then strValue = Format(Now, "yyyy-MM-dd HH:mm:ss")
                            If objPulicParameters.isDatetimeString(strValue) = False Then
                                strErrMsg = "����[" + strField + "]��������Ч���ڣ�"
                                GoTo errProc
                            End If

                        Case Else
                            If strValue <> "" Then
                                With objDataSet.Tables(0).Columns(strField)
                                    intLen = objPulicParameters.getStringLength(strValue)
                                    If intLen > .MaxLength Then
                                        strErrMsg = "����[" + strField + "]���Ȳ��ܳ���[" + .MaxLength.ToString() + "]��ʵ����[" + intLen.ToString() + "]��"
                                        GoTo errProc
                                    End If
                                End With
                            End If

                    End Select

                    objNewData(strField) = strValue
                Next
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            doVerifyCuiban = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ����߰�����
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     objSqlTransaction      ����������
        '     objNewData             ����¼��ֵ(���ر�������ֵ)
        '     objOldData             ����¼��ֵ
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doSaveCuiban( _
            ByRef strErrMsg As String, _
            ByVal objSqlTransaction As System.Data.SqlClient.SqlTransaction, _
            ByVal objOldData As System.Data.DataRow, _
            ByRef objNewData As System.Collections.Specialized.NameValueCollection) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            Dim strTable As String = Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_CUIBAN_JIAOJIE
            Dim blnNewTrans As Boolean = False
            Dim strWJBS As String
            Dim strSQL As String

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacMyJiaotan As New Xydc.Platform.DataAccess.dacMyJiaotan

            '��ʼ��
            doSaveCuiban = False
            strErrMsg = ""

            Try
                '���
                If Me.IsInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "����δ�����µ����ݣ�"
                    GoTo errProc
                End If

                '��ȡ������Ϣ
                strWJBS = Me.FlowData.WJBS
                If objSqlTransaction Is Nothing Then
                    objSqlConnection = Me.SqlConnection
                Else
                    objSqlConnection = objSqlTransaction.Connection
                End If

                '��ʼ����
                If objSqlTransaction Is Nothing Then
                    blnNewTrans = True
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Else
                    blnNewTrans = False
                End If

                '��������
                Dim strFields As String
                Dim strValues As String
                Dim strField As String
                Dim strValue As String
                Dim intCount As Integer
                Dim i As Integer
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    objSqlCommand.Parameters.Clear()
                    If objOldData Is Nothing Then
                        '����
                        intCount = objNewData.Count
                        strFields = ""
                        strValues = ""
                        For i = 0 To intCount - 1 Step 1
                            strField = objNewData.GetKey(i)
                            strValue = objNewData(i)

                            If strFields = "" Then
                                strFields = strField
                                strValues = "@A" + i.ToString
                            Else
                                strFields = strFields + "," + vbCr + strField
                                strValues = strValues + "," + vbCr + "@A" + i.ToString
                            End If

                            Select Case strField
                                Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_CUIBAN_JJXH, _
                                    Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_CUIBAN_CBXH
                                    objSqlCommand.Parameters.AddWithValue("@A" + i.ToString, CType(strValue, Integer))
                                Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_CUIBAN_CBRQ
                                    If strValue = "" Then
                                        objSqlCommand.Parameters.AddWithValue("@A" + i.ToString, System.DBNull.Value)
                                    Else
                                        objSqlCommand.Parameters.AddWithValue("@A" + i.ToString, CType(strValue, System.DateTime))
                                    End If
                                Case Else
                                    objSqlCommand.Parameters.AddWithValue("@A" + i.ToString, strValue)
                            End Select
                        Next

                        strSQL = " insert into ����_B_�߰�(" + vbCr + strFields + vbCr + ") values (" + vbCr + strValues + ")" + vbCr
                    Else
                        Dim intJJXH As Integer
                        Dim intCBXH As Integer
                        intJJXH = objPulicParameters.getObjectValue(objOldData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_CUIBAN_JJXH), 0)
                        intCBXH = objPulicParameters.getObjectValue(objOldData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_CUIBAN_CBXH), 0)

                        '����
                        intCount = objNewData.Count
                        strFields = ""
                        strValues = ""
                        For i = 0 To intCount - 1 Step 1
                            strField = objNewData.GetKey(i)
                            strValue = objNewData(i)

                            If strFields = "" Then
                                strFields = strField + " = @A" + i.ToString
                            Else
                                strFields = strFields + "," + vbCr + strField + " = @A" + i.ToString
                            End If

                            Select Case strField
                                Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_CUIBAN_JJXH, _
                                    Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_CUIBAN_CBXH
                                    objSqlCommand.Parameters.AddWithValue("@A" + i.ToString, CType(strValue, Integer))
                                Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_CUIBAN_CBRQ
                                    If strValue = "" Then
                                        objSqlCommand.Parameters.AddWithValue("@A" + i.ToString, System.DBNull.Value)
                                    Else
                                        objSqlCommand.Parameters.AddWithValue("@A" + i.ToString, CType(strValue, System.DateTime))
                                    End If
                                Case Else
                                    objSqlCommand.Parameters.AddWithValue("@A" + i.ToString, strValue)
                            End Select
                        Next
                        objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                        objSqlCommand.Parameters.AddWithValue("@jjxh", intJJXH)
                        objSqlCommand.Parameters.AddWithValue("@cbxh", intCBXH)

                        strSQL = ""
                        strSQL = strSQL + " update ����_B_�߰� set " + vbCr + strFields + vbCr
                        strSQL = strSQL + " where �ļ���ʶ = @wjbs" + vbCr
                        strSQL = strSQL + " and   ������� = @jjxh" + vbCr
                        strSQL = strSQL + " and   �߰���� = @cbxh" + vbCr
                    End If
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '���ͼ�ʱ��Ϣ֪ͨ
                    Dim objNewDataJSXX As New System.Collections.Specialized.NameValueCollection
                    Dim strJSXX As String
                    Dim strFSR As String
                    Dim strJSR As String
                    strJSXX = objNewData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_CUIBAN_CBSM) + "(��ϸ��鿴�����߰���ļ���)"
                    strFSR = objNewData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_CUIBAN_CBR)
                    strJSR = objNewData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_CUIBAN_BCBR)
                    objNewDataJSXX.Add(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_WYBS, "") '��ϵͳ����
                    objNewDataJSXX.Add(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_FSR, strFSR)
                    objNewDataJSXX.Add(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_JSR, strJSR)
                    objNewDataJSXX.Add(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_XX, strJSXX)
                    objNewDataJSXX.Add(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_FSSJ, Format(Now, "yyyy-MM-dd HH:mm:ss"))
                    objNewDataJSXX.Add(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_BZ, "0")
                    objNewDataJSXX.Add(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_TS, "0")
                    If objdacMyJiaotan.doSaveData(strErrMsg, objSqlTransaction, Nothing, objNewDataJSXX, Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew) = False Then
                        GoTo rollDatabase
                    End If

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo rollDatabase
                End Try

                '�ύ����
                If blnNewTrans = True Then
                    objSqlTransaction.Commit()
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacMyJiaotan.SafeRelease(objdacMyJiaotan)

            '����
            doSaveCuiban = True
            Exit Function

rollDatabase:
            If blnNewTrans = True Then
                objSqlTransaction.Rollback()
            End If
            GoTo errProc

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacMyJiaotan.SafeRelease(objdacMyJiaotan)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡָ����ԱstrUserXM�Ķ�������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ����Ա����
        '     objDubanData         ����������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getDubanData( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByRef objDubanData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim objTempDubanData As Xydc.Platform.Common.Data.FlowData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            getDubanData = False
            objDubanData = Nothing
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '�������ݼ�
                objTempDubanData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_DUBAN_JIAOJIE)
                If strWJBS = "" Then Exit Try

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                'ִ�м���
                With Me.m_objSqlDataAdapter
                    '��ȡָ����Ա��������
                    strSQL = ""
                    strSQL = strSQL + " select a.*, b.��������, b.����״̬ from " + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select * from ����_B_���� " + vbCr
                    strSQL = strSQL + "   where �ļ���ʶ = '" + strWJBS + "' " + vbCr
                    strSQL = strSQL + "   and   ������   = '" + strUserXM + "' " + vbCr
                    strSQL = strSQL + " ) a " + vbCr
                    strSQL = strSQL + " left join " + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select * from ����_B_���� " + vbCr
                    strSQL = strSQL + "   where �ļ���ʶ = '" + strWJBS + "' " + vbCr
                    strSQL = strSQL + " ) b on a.�ļ���ʶ = b.�ļ���ʶ and a.������� = b.������� " + vbCr
                    strSQL = strSQL + " order by a.������� " + vbCr

                    '���ò���
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    'ִ�в���
                    .Fill(objTempDubanData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_DUBAN_JIAOJIE))
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            objDubanData = objTempDubanData
            getDubanData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.FlowData.SafeRelease(objTempDubanData)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ�ļ��Ķ�������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objDubanData         ����������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getDubanData( _
            ByRef strErrMsg As String, _
            ByRef objDubanData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim objTempDubanData As Xydc.Platform.Common.Data.FlowData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            getDubanData = False
            objDubanData = Nothing
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '�������ݼ�
                objTempDubanData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_DUBAN_JIAOJIE)
                If strWJBS = "" Then Exit Try

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                'ִ�м���
                With Me.m_objSqlDataAdapter
                    '��ȡָ����Ա��������
                    strSQL = ""
                    strSQL = strSQL + " select a.*, b.��������, b.����״̬ from " + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select * from ����_B_���� " + vbCr
                    strSQL = strSQL + "   where �ļ���ʶ = '" + strWJBS + "' " + vbCr
                    strSQL = strSQL + " ) a " + vbCr
                    strSQL = strSQL + " left join " + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select * from ����_B_���� " + vbCr
                    strSQL = strSQL + "   where �ļ���ʶ = '" + strWJBS + "' " + vbCr
                    strSQL = strSQL + " ) b on a.�ļ���ʶ = b.�ļ���ʶ and a.������� = b.������� " + vbCr
                    strSQL = strSQL + " order by a.������� " + vbCr

                    '���ò���
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    'ִ�в���
                    .Fill(objTempDubanData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_DUBAN_JIAOJIE))
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            objDubanData = objTempDubanData
            getDubanData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.FlowData.SafeRelease(objTempDubanData)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡstrUserXM�Ŀ��Զ�����Щ���ӵ�?
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ����Ա����
        '     objKeDubanData       �����ؿ��Զ���Ľ��ӵ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getKeDubanData( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByRef objKeDubanData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCustomer As New Xydc.Platform.DataAccess.dacCustomer

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objTempKeDubanData As Xydc.Platform.Common.Data.FlowData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            getKeDubanData = False
            objKeDubanData = Nothing
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '���Ӱ������״̬SQL�б�

                '��ȡ�ļ���ʶ
                Dim strTaskStatusYWCList As String = Me.FlowData.TaskStatusYWCList
                objSqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '�������ݼ�
                objTempKeDubanData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_DUBAN_JIAOJIE)
                If strWJBS = "" Then Exit Try

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                'ִ�м���
                With Me.m_objSqlDataAdapter
                    '��ȡ�ɶ���Ľ�����Ϣ
                    strSQL = ""
                    strSQL = strSQL + " select a.*" + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select" + vbCr
                    strSQL = strSQL + "     �ļ���ʶ," + vbCr
                    strSQL = strSQL + "     �������," + vbCr
                    strSQL = strSQL + "     ������� = 0," + vbCr
                    strSQL = strSQL + "     ������ = '" + strUserXM + "'," + vbCr
                    strSQL = strSQL + "     �������� = getdate()," + vbCr
                    strSQL = strSQL + "     �������� = ������," + vbCr
                    strSQL = strSQL + "     ����Ҫ�� = '�뾡�촦��'," + vbCr
                    strSQL = strSQL + "     ������ = ''," + vbCr
                    strSQL = strSQL + "     ��������," + vbCr
                    strSQL = strSQL + "     ����״̬" + vbCr
                    strSQL = strSQL + "   from ����_B_����" + vbCr
                    strSQL = strSQL + "   where �ļ���ʶ = '" + strWJBS + "' " + vbCr                      '��ǰ�ļ�
                    strSQL = strSQL + "   and   rtrim(���ӱ�ʶ) like '1_1__0%' " + vbCr                           '���͹�+��֪ͨ+�����˿ɼ�
                    strSQL = strSQL + "   and   ����״̬ not in (" + strTaskStatusYWCList + ") " + vbCr    'δ����
                    strSQL = strSQL + " ) a" + vbCr
                    strSQL = strSQL + " order by a.�������" + vbCr

                    '���ò���
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    'ִ�в���
                    .Fill(objTempKeDubanData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_DUBAN_JIAOJIE))
                End With

                '���㲿����Ϣ
                Dim strUserId As String
                Dim strBmdm As String
                Dim strBmmc As String
                If objdacCustomer.getBmdmAndBmmcByRymc(strErrMsg, objSqlConnection, strUserXM, strBmdm, strBmmc) = False Then
                    GoTo errProc
                End If
                If objdacCustomer.getRydmByRymc(strErrMsg, objSqlConnection, strUserXM, strUserId) = False Then
                    GoTo errProc
                End If

                'ɾ�����ܶ��������
                Dim intCount As Integer
                Dim strJsr As String
                Dim blnDo As Boolean
                Dim i As Integer
                With objTempKeDubanData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_DUBAN_JIAOJIE)
                    intCount = .Rows.Count
                    For i = intCount - 1 To 0 Step -1
                        strJsr = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_DUBAN_BDBR), "")
                        If Me.canDubanFile(strErrMsg, strUserId, strBmdm, strJsr, blnDo) = False Then
                            GoTo errProc
                        End If
                        If blnDo = False Then
                            .Rows.RemoveAt(i)
                        End If
                    Next
                End With

                '���á�������š�
                With objTempKeDubanData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_DUBAN_JIAOJIE)
                    intCount = .Rows.Count
                    For i = 0 To intCount - 1 Step 1
                        .Rows(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_DUBAN_DBXH) = i + 1
                    Next
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)

            objKeDubanData = objTempKeDubanData
            getKeDubanData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.FlowData.SafeRelease(objTempKeDubanData)
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �ж϶������������Ƿ���Ч��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objOldData           ����¼��ֵ
        '     objNewData           ����¼��ֵ(�����Ƽ�ֵ)
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doVerifyDuban( _
            ByRef strErrMsg As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByRef objNewData As System.Collections.Specialized.NameValueCollection) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            Dim strTable As String = Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_DUBAN_JIAOJIE

            doVerifyDuban = False

            Try
                '���
                If Me.IsInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "����δ����ҪУ������ݣ�"
                    GoTo errProc
                End If

                '��ȡ������Ϣ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strSep As String = objPulicParameters.CharSeparate
                Dim strWJBS As String = Me.FlowData.WJBS

                '��ȡ��ṹ����
                strSQL = "select top 0 * from ����_B_����"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, objSqlConnection, strSQL, "����_B_����", objDataSet) = False Then
                    GoTo errProc
                End If

                '������ݳ���
                Dim intCount As Integer = objNewData.Count
                Dim strField As String
                Dim strValue As String
                Dim intLen As Integer
                Dim i As Integer
                For i = 0 To intCount - 1 Step 1
                    strField = objNewData.GetKey(i).Trim()
                    strValue = objNewData.Item(i).Trim()
                    Select Case strField
                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_DUBAN_WJBS
                            strValue = strWJBS

                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_DUBAN_DBXH
                            Dim strDBXH As String = ""
                            If objOldData Is Nothing Then
                                '�Զ�����
                                strValue = objNewData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_DUBAN_JJXH)
                                If objdacCommon.getNewCode(strErrMsg, objSqlConnection, "�������", "�ļ���ʶ" + strSep + "�������", strWJBS + strSep + strValue, "����_B_����", True, strDBXH) = False Then
                                    GoTo errProc
                                End If
                                strValue = strDBXH
                            Else
                                If strValue = "" Then
                                    strErrMsg = "����[" + strField + "]����Ϊ�գ�"
                                    GoTo errProc
                                End If
                                If objPulicParameters.isIntegerString(strValue) = False Then
                                    strErrMsg = "����[" + strField + "]���������֣�"
                                    GoTo errProc
                                End If
                                intLen = CType(strValue, Integer)
                                If intLen < 1 Or intLen > 999999 Then
                                    strErrMsg = "����[" + strField + "]������[1,999999]��"
                                    GoTo errProc
                                End If
                                strValue = intLen.ToString()
                            End If

                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_DUBAN_JJXH
                            If strValue = "" Then
                                strErrMsg = "����[" + strField + "]����Ϊ�գ�"
                                GoTo errProc
                            End If
                            If objPulicParameters.isIntegerString(strValue) = False Then
                                strErrMsg = "����[" + strField + "]���������֣�"
                                GoTo errProc
                            End If
                            intLen = CType(strValue, Integer)
                            If intLen < 1 Or intLen > 999999 Then
                                strErrMsg = "����[" + strField + "]������[1,999999]��"
                                GoTo errProc
                            End If
                            strValue = intLen.ToString()

                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_DUBAN_DBR, _
                            Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_DUBAN_BDBR
                            If strValue = "" Then
                                strErrMsg = "����[" + strField + "]����Ϊ�գ�"
                                GoTo errProc
                            End If
                            With objDataSet.Tables(0).Columns(strField)
                                intLen = objPulicParameters.getStringLength(strValue)
                                If intLen > .MaxLength Then
                                    strErrMsg = "����[" + strField + "]���Ȳ��ܳ���[" + .MaxLength.ToString() + "]��ʵ����[" + intLen.ToString() + "]��"
                                    GoTo errProc
                                End If
                            End With

                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_DUBAN_DBRQ
                            If strValue = "" Then strValue = Format(Now, "yyyy-MM-dd HH:mm:ss")
                            If objPulicParameters.isDatetimeString(strValue) = False Then
                                strErrMsg = "����[" + strField + "]��������Ч���ڣ�"
                                GoTo errProc
                            End If

                        Case Else
                            If strValue <> "" Then
                                With objDataSet.Tables(0).Columns(strField)
                                    intLen = objPulicParameters.getStringLength(strValue)
                                    If intLen > .MaxLength Then
                                        strErrMsg = "����[" + strField + "]���Ȳ��ܳ���[" + .MaxLength.ToString() + "]��ʵ����[" + intLen.ToString() + "]��"
                                        GoTo errProc
                                    End If
                                End With
                            End If

                    End Select

                    objNewData(strField) = strValue
                Next
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            doVerifyDuban = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���涽������
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     objSqlTransaction      ����������
        '     objNewData             ����¼��ֵ(���ر�������ֵ)
        '     objOldData             ����¼��ֵ
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doSaveDuban( _
            ByRef strErrMsg As String, _
            ByVal objSqlTransaction As System.Data.SqlClient.SqlTransaction, _
            ByVal objOldData As System.Data.DataRow, _
            ByRef objNewData As System.Collections.Specialized.NameValueCollection) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            Dim strTable As String = Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_DUBAN_JIAOJIE
            Dim blnNewTrans As Boolean = False
            Dim strWJBS As String
            Dim strSQL As String

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacMyJiaotan As New Xydc.Platform.DataAccess.dacMyJiaotan

            '��ʼ��
            doSaveDuban = False
            strErrMsg = ""

            Try
                '���
                If Me.IsInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "����δ�����µ����ݣ�"
                    GoTo errProc
                End If

                '��ȡ������Ϣ
                strWJBS = Me.FlowData.WJBS
                If objSqlTransaction Is Nothing Then
                    objSqlConnection = Me.SqlConnection
                Else
                    objSqlConnection = objSqlTransaction.Connection
                End If

                '��ʼ����
                If objSqlTransaction Is Nothing Then
                    blnNewTrans = True
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Else
                    blnNewTrans = False
                End If

                '��������
                Dim strFields As String
                Dim strValues As String
                Dim strField As String
                Dim strValue As String
                Dim intCount As Integer
                Dim i As Integer
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    objSqlCommand.Parameters.Clear()
                    If objOldData Is Nothing Then
                        '����
                        intCount = objNewData.Count
                        strFields = ""
                        strValues = ""
                        For i = 0 To intCount - 1 Step 1
                            strField = objNewData.GetKey(i)
                            strValue = objNewData(i)

                            If strFields = "" Then
                                strFields = strField
                                strValues = "@A" + i.ToString
                            Else
                                strFields = strFields + "," + vbCr + strField
                                strValues = strValues + "," + vbCr + "@A" + i.ToString
                            End If

                            Select Case strField
                                Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_DUBAN_JJXH, _
                                    Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_DUBAN_DBXH
                                    objSqlCommand.Parameters.AddWithValue("@A" + i.ToString, CType(strValue, Integer))
                                Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_DUBAN_DBRQ
                                    If strValue = "" Then
                                        objSqlCommand.Parameters.AddWithValue("@A" + i.ToString, System.DBNull.Value)
                                    Else
                                        objSqlCommand.Parameters.AddWithValue("@A" + i.ToString, CType(strValue, System.DateTime))
                                    End If
                                Case Else
                                    objSqlCommand.Parameters.AddWithValue("@A" + i.ToString, strValue)
                            End Select
                        Next

                        strSQL = " insert into ����_B_����(" + vbCr + strFields + vbCr + ") values (" + vbCr + strValues + ")" + vbCr
                    Else
                        Dim intJJXH As Integer
                        Dim intDBXH As Integer
                        intJJXH = objPulicParameters.getObjectValue(objOldData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_DUBAN_JJXH), 0)
                        intDBXH = objPulicParameters.getObjectValue(objOldData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_DUBAN_DBXH), 0)

                        '����
                        intCount = objNewData.Count
                        strFields = ""
                        strValues = ""
                        For i = 0 To intCount - 1 Step 1
                            strField = objNewData.GetKey(i)
                            strValue = objNewData(i)

                            If strFields = "" Then
                                strFields = strField + " = @A" + i.ToString
                            Else
                                strFields = strFields + "," + vbCr + strField + " = @A" + i.ToString
                            End If

                            Select Case strField
                                Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_DUBAN_JJXH, _
                                    Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_DUBAN_DBXH
                                    objSqlCommand.Parameters.AddWithValue("@A" + i.ToString, CType(strValue, Integer))
                                Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_DUBAN_DBRQ
                                    If strValue = "" Then
                                        objSqlCommand.Parameters.AddWithValue("@A" + i.ToString, System.DBNull.Value)
                                    Else
                                        objSqlCommand.Parameters.AddWithValue("@A" + i.ToString, CType(strValue, System.DateTime))
                                    End If
                                Case Else
                                    objSqlCommand.Parameters.AddWithValue("@A" + i.ToString, strValue)
                            End Select
                        Next
                        objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                        objSqlCommand.Parameters.AddWithValue("@jjxh", intJJXH)
                        objSqlCommand.Parameters.AddWithValue("@dbxh", intDBXH)

                        strSQL = ""
                        strSQL = strSQL + " update ����_B_���� set " + vbCr + strFields + vbCr
                        strSQL = strSQL + " where �ļ���ʶ = @wjbs" + vbCr
                        strSQL = strSQL + " and   ������� = @jjxh" + vbCr
                        strSQL = strSQL + " and   ������� = @dbxh" + vbCr
                    End If
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '���ͼ�ʱ��Ϣ֪ͨ
                    Dim objNewDataJSXX As New System.Collections.Specialized.NameValueCollection
                    Dim strJSXX As String
                    Dim strFSR As String
                    Dim strJSR As String
                    strJSXX = objNewData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_DUBAN_DBYQ) + "(��ϸ��鿴����������ļ���)"
                    strFSR = objNewData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_DUBAN_DBR)
                    strJSR = objNewData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_DUBAN_BDBR)
                    objNewDataJSXX.Add(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_WYBS, "") '��ϵͳ����
                    objNewDataJSXX.Add(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_FSR, strFSR)
                    objNewDataJSXX.Add(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_JSR, strJSR)
                    objNewDataJSXX.Add(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_XX, strJSXX)
                    objNewDataJSXX.Add(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_FSSJ, Format(Now, "yyyy-MM-dd HH:mm:ss"))
                    objNewDataJSXX.Add(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_BZ, "0")
                    objNewDataJSXX.Add(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_TS, "0")
                    If objdacMyJiaotan.doSaveData(strErrMsg, objSqlTransaction, Nothing, objNewDataJSXX, Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew) = False Then
                        GoTo rollDatabase
                    End If

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo rollDatabase
                End Try

                '�ύ����
                If blnNewTrans = True Then
                    objSqlTransaction.Commit()
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacMyJiaotan.SafeRelease(objdacMyJiaotan)

            '����
            doSaveDuban = True
            Exit Function

rollDatabase:
            If blnNewTrans = True Then
                objSqlTransaction.Rollback()
            End If
            GoTo errProc

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacMyJiaotan.SafeRelease(objdacMyJiaotan)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡָ����ԱstrUserXM�ı���������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ����Ա����
        '     objBeidubanData      ����������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getBeidubanData( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByRef objBeidubanData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim objTempBeidubanData As Xydc.Platform.Common.Data.FlowData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            getBeidubanData = False
            objBeidubanData = Nothing
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '�������ݼ�
                objTempBeidubanData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_DUBAN_JIAOJIE)
                If strWJBS = "" Then Exit Try

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                'ִ�м���
                With Me.m_objSqlDataAdapter
                    '��ȡָ����Ա����������
                    strSQL = ""
                    strSQL = strSQL + " select a.*, b.��������, b.����״̬ from " + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select * from ����_B_���� " + vbCr
                    strSQL = strSQL + "   where �ļ���ʶ = '" + strWJBS + "' " + vbCr
                    strSQL = strSQL + "   and   �������� = '" + strUserXM + "' " + vbCr
                    strSQL = strSQL + " ) a " + vbCr
                    strSQL = strSQL + " left join " + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select * from ����_B_���� " + vbCr
                    strSQL = strSQL + "   where �ļ���ʶ = '" + strWJBS + "' " + vbCr
                    strSQL = strSQL + " ) b on a.�ļ���ʶ = b.�ļ���ʶ and a.������� = b.������� " + vbCr
                    strSQL = strSQL + " order by a.������� " + vbCr

                    '���ò���
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    'ִ�в���
                    .Fill(objTempBeidubanData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_DUBAN_JIAOJIE))
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            objBeidubanData = objTempBeidubanData
            getBeidubanData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.FlowData.SafeRelease(objTempBeidubanData)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���涽��������
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     objSqlTransaction      ����������
        '     intJJXH                ���������
        '     intDBXH                ���������
        '     strDBJG                ��������
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doSaveDuban( _
            ByRef strErrMsg As String, _
            ByVal objSqlTransaction As System.Data.SqlClient.SqlTransaction, _
            ByVal intJJXH As Integer, _
            ByVal intDBXH As Integer, _
            ByVal strDBJG As String) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            Dim blnNewTrans As Boolean = False
            Dim strWJBS As String
            Dim strSQL As String

            '��ʼ��
            doSaveDuban = False
            strErrMsg = ""

            Try
                '���
                If Me.IsInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strDBJG Is Nothing Then strDBJG = ""
                strDBJG = strDBJG.Trim

                '��ȡ������Ϣ
                strWJBS = Me.FlowData.WJBS
                If objSqlTransaction Is Nothing Then
                    objSqlConnection = Me.SqlConnection
                Else
                    objSqlConnection = objSqlTransaction.Connection
                End If

                '��ʼ����
                If objSqlTransaction Is Nothing Then
                    blnNewTrans = True
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Else
                    blnNewTrans = False
                End If

                '��������
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@dbjg", strDBJG)
                    objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                    objSqlCommand.Parameters.AddWithValue("@jjxh", intJJXH)
                    objSqlCommand.Parameters.AddWithValue("@dbxh", intDBXH)
                    strSQL = ""
                    strSQL = strSQL + " update ����_B_���� set " + vbCr
                    strSQL = strSQL + "   ������ = @dbjg" + vbCr
                    strSQL = strSQL + " where �ļ���ʶ = @wjbs" + vbCr
                    strSQL = strSQL + " and   ������� = @jjxh" + vbCr
                    strSQL = strSQL + " and   ������� = @dbxh" + vbCr
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo rollDatabase
                End Try

                '�ύ����
                If blnNewTrans = True Then
                    objSqlTransaction.Commit()
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            '����
            doSaveDuban = True
            Exit Function

rollDatabase:
            If blnNewTrans = True Then
                objSqlTransaction.Rollback()
            End If
            GoTo errProc

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ����strWJBS��ȡ���ӵ�(��������+���鿴����)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ����ǰ�鿴��
        '     strWhere             ����������(a.)
        '     objJiaoJieData       �����ؽ�������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getLZQKDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByVal strWhere As String, _
            ByRef objJiaoJieData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim objTempJiaoJieData As Xydc.Platform.Common.Data.FlowData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCustomer As New Xydc.Platform.DataAccess.dacCustomer

            getLZQKDataSet = False
            objJiaoJieData = Nothing
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '�������ݼ�
                objTempJiaoJieData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_JIAOJIE)
                If strWJBS = "" Then
                    Exit Try
                End If

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                'ִ�м���
                With Me.SqlDataAdapter
                    '���㽻����Ϣ
                    strSQL = ""
                    strSQL = strSQL + " select a.*" + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select" + vbCr
                    strSQL = strSQL + "     �ļ���ʶ,�������,ԭ���Ӻ�,�������,������,��������," + vbCr
                    strSQL = strSQL + "     ����ֽ���ļ�,���͵����ļ�,����ֽ�ʸ���,���͵��Ӹ���," + vbCr
                    strSQL = strSQL + "     �������,������,Э��,��������," + vbCr
                    strSQL = strSQL + "     ����ֽ���ļ�,���յ����ļ�,����ֽ�ʸ���,���յ��Ӹ���, " + vbCr
                    strSQL = strSQL + "     �����������,�������,��������,����״̬,���ӱ�ʶ,ί����," + vbCr
                    strSQL = strSQL + "     �������� = case " + vbCr
                    strSQL = strSQL + "       when substring(���ӱ�ʶ,4,1)='1' then '" + Me.FlowData.TASK_THCL + "'" + vbCr
                    strSQL = strSQL + "       when substring(���ӱ�ʶ,5,1)='1' then '" + Me.FlowData.TASK_SHCL + "'" + vbCr
                    strSQL = strSQL + "       when substring(���ӱ�ʶ,7,1)='1' then '" + Me.FlowData.TASK_HFCL + "'" + vbCr
                    strSQL = strSQL + "       else �������� end" + vbCr
                    strSQL = strSQL + "   from ����_B_����" + vbCr
                    strSQL = strSQL + "   where �ļ���ʶ = '" + strWJBS + "'" + vbCr       '��ǰ�ļ�
                    strSQL = strSQL + "   and   rtrim(���ӱ�ʶ) like '1____0%'" + vbCr            '��֪ͨ��
                    strSQL = strSQL + " ) a" + vbCr
                    If strWhere <> "" Then
                        strSQL = strSQL + " where " + strWhere + vbCr
                    End If
                    strSQL = strSQL + " order by �������" + vbCr

                    '���ò���
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    'ִ�в���
                    .Fill(objTempJiaoJieData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_JIAOJIE))
                End With

                '��ȡ�û�ID�͵�λ����
                Dim strBmdm As String = ""
                Dim strBmmc As String = ""
                If objdacCustomer.getBmdmAndBmmcByRymc(strErrMsg, objSqlConnection, strUserXM, strBmdm, strBmmc) = False Then
                    GoTo errProc
                End If

                '����Ƿ���ʾ����
                Dim strNewName As String = ""
                Dim intCount As Integer
                Dim strJSR As String
                Dim strFSR As String
                Dim i As Integer
                With objTempJiaoJieData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_JIAOJIE)
                    intCount = .Rows.Count
                    For i = 0 To intCount - 1 Step 1
                        strFSR = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSR), "")
                        If Me.getDisplayName(strErrMsg, strUserXM, strBmdm, strFSR, strNewName) = False Then
                            GoTo errProc
                        End If
                        .Rows(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSR) = strNewName

                        strJSR = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSR), "")
                        If Me.getDisplayName(strErrMsg, strUserXM, strBmdm, strJSR, strNewName) = False Then
                            GoTo errProc
                        End If
                        .Rows(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSR) = strNewName
                    Next
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            objJiaoJieData = objTempJiaoJieData
            getLZQKDataSet = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.FlowData.SafeRelease(objTempJiaoJieData)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ�ļ��Ĳ�����־����
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strWhere             ����������(a.)
        '     objCaozuorizhiData   ����������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getCaozuorizhiData( _
            ByRef strErrMsg As String, _
            ByVal strWhere As String, _
            ByRef objCaozuorizhiData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim objTempCaozuorizhiData As Xydc.Platform.Common.Data.FlowData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            getCaozuorizhiData = False
            objCaozuorizhiData = Nothing
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim()

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '�������ݼ�
                objTempCaozuorizhiData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_CAOZUORIZHI)
                If strWJBS = "" Then Exit Try

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                'ִ�м���
                With Me.m_objSqlDataAdapter
                    '��ȡ������־
                    strSQL = ""
                    strSQL = strSQL + " select a.*" + vbCr
                    strSQL = strSQL + " from ����_B_������־ a" + vbCr
                    strSQL = strSQL + " where a.�ļ���ʶ = '" + strWJBS + "'" + vbCr
                    If strWhere <> "" Then
                        strSQL = strSQL + " and " + strWhere + vbCr
                    End If
                    strSQL = strSQL + " order by a.�������"

                    '���ò���
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    'ִ�в���
                    .Fill(objTempCaozuorizhiData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_CAOZUORIZHI))
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            objCaozuorizhiData = objTempCaozuorizhiData
            getCaozuorizhiData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.FlowData.SafeRelease(objTempCaozuorizhiData)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ�ļ��Ĳ�������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strCksyList          ��Ҫ�鿴�ض���������(����ԭ���Ӻ��б�)
        '     strWhere             ����������(a.)
        '     objBuyueData         ����������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getBuyueData( _
            ByRef strErrMsg As String, _
            ByVal strCksyList As String, _
            ByVal strWhere As String, _
            ByRef objBuyueData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim objTempBuyueData As Xydc.Platform.Common.Data.FlowData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            getBuyueData = False
            objBuyueData = Nothing
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim()
                If strCksyList Is Nothing Then strCksyList = ""
                strCksyList = strCksyList.Trim

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '�������ݼ�
                objTempBuyueData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_VT_WENJIANBUYUE)
                If strWJBS = "" Then Exit Try

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                'ִ�м���
                With Me.m_objSqlDataAdapter
                    '�����в��ĵ�����
                    strSQL = ""
                    strSQL = strSQL + " select a.*" + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select a.*," + vbCr
                    strSQL = strSQL + "     ������� = case " + vbCr
                    strSQL = strSQL + "       when isnull(b.�Ƿ���׼,' ') = '1100' then '��׼'" + vbCr
                    strSQL = strSQL + "       when isnull(b.�Ƿ���׼,' ') = '1110' then 'ת��'" + vbCr
                    strSQL = strSQL + "       when isnull(b.�Ƿ���׼,' ') = '1000' then '�ܾ�'" + vbCr
                    strSQL = strSQL + "       else '    ' end" + vbCr
                    strSQL = strSQL + "   from" + vbCr
                    strSQL = strSQL + "   (" + vbCr
                    strSQL = strSQL + "     select a.*" + vbCr
                    strSQL = strSQL + "     from ����_B_���� a" + vbCr
                    strSQL = strSQL + "     where a.�ļ���ʶ = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + "     and   a.�������� = '" + Me.FlowData.TASK_BYQQ + "'" + vbCr
                    strSQL = strSQL + "     union" + vbCr
                    strSQL = strSQL + "     select a.*" + vbCr
                    strSQL = strSQL + "     from ����_B_���� a" + vbCr
                    strSQL = strSQL + "     where a.�ļ���ʶ = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + "     and   a.�������� = '" + Me.FlowData.TASK_BYTZ + "'" + vbCr
                    If Trim(strCksyList) = "" Then
                        strSQL = strSQL + "     and a.ԭ���Ӻ� in (" + Me.FlowData.TaskStatusZDTZList + ")" + vbCr
                    Else
                        strSQL = strSQL + "     and a.ԭ���Ӻ� in (" + strCksyList + ")" + vbCr
                    End If
                    strSQL = strSQL + "   ) a"
                    strSQL = strSQL + "   left join" + vbCr
                    strSQL = strSQL + "   (" + vbCr
                    strSQL = strSQL + "     select a.*" + vbCr
                    strSQL = strSQL + "     from ����_B_���� a" + vbCr
                    strSQL = strSQL + "     where a.�ļ���ʶ = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + "     and   a.�������� = '" + Me.FlowData.TASK_BYQQ + "'" + vbCr
                    strSQL = strSQL + "  ) b on a.�ļ���ʶ=b.�ļ���ʶ and a.�������=b.�������" + vbCr
                    strSQL = strSQL + " ) a" + vbCr
                    If strWhere <> "" Then
                        strSQL = strSQL + " where " + strWhere + vbCr
                    End If
                    strSQL = strSQL + " order by a.�������" + vbCr

                    '���ò���
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    'ִ�в���
                    .Fill(objTempBuyueData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_VT_WENJIANBUYUE))
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            objBuyueData = objTempBuyueData
            getBuyueData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.FlowData.SafeRelease(objTempBuyueData)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡָ����Ա���͵Ĳ�������(���������벹��֪ͨ)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ����Ա����
        '     strWhere             ����������(a.)
        '     objBuyueData         ����������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getBuyueSendData( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByVal strWhere As String, _
            ByRef objBuyueData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim objTempBuyueData As Xydc.Platform.Common.Data.FlowData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            getBuyueSendData = False
            objBuyueData = Nothing
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim()
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '�������ݼ�
                objTempBuyueData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_VT_WENJIANBUYUE)
                If strWJBS = "" Then Exit Try

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                'ִ�м���
                Dim intZDBY As Integer = Xydc.Platform.Common.Data.FlowData.YJJH_ZHUDONGBUYUE
                With Me.m_objSqlDataAdapter
                    '���ҷ��͵�����
                    strSQL = ""
                    strSQL = strSQL + " select a.*" + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select a.*," + vbCr
                    strSQL = strSQL + "     ������� = case " + vbCr
                    strSQL = strSQL + "       when isnull(b.�Ƿ���׼,' ') = '1100' then '��׼'" + vbCr
                    strSQL = strSQL + "       when isnull(b.�Ƿ���׼,' ') = '1110' then 'ת��'" + vbCr
                    strSQL = strSQL + "       when isnull(b.�Ƿ���׼,' ') = '1000' then '�ܾ�'" + vbCr
                    strSQL = strSQL + "       else '    ' end" + vbCr
                    strSQL = strSQL + "   from" + vbCr
                    strSQL = strSQL + "   (" + vbCr
                    strSQL = strSQL + "     select a.*" + vbCr
                    strSQL = strSQL + "     from ����_B_���� a" + vbCr
                    strSQL = strSQL + "     where a.�ļ���ʶ = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + "     and  (a.������   = '" + strUserXM + "' or a.ί���� = '" + strUserXM + "')" + vbCr
                    strSQL = strSQL + "     and   a.�������� = '" + Me.FlowData.TASK_BYQQ + "'" + vbCr
                    strSQL = strSQL + "     union" + vbCr
                    strSQL = strSQL + "     select a.*" + vbCr
                    strSQL = strSQL + "     from ����_B_���� a" + vbCr
                    strSQL = strSQL + "     where a.�ļ���ʶ = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + "     and   a.������   = '" + strUserXM + "'" + vbCr
                    strSQL = strSQL + "     and   a.�������� = '" + Me.FlowData.TASK_BYTZ + "'" + vbCr
                    strSQL = strSQL + "     and   a.ԭ���Ӻ� =  " + intZDBY.ToString + vbCr
                    strSQL = strSQL + "   ) a " + vbCr
                    strSQL = strSQL + "   left join" + vbCr
                    strSQL = strSQL + "   (" + vbCr
                    strSQL = strSQL + "     select a.*" + vbCr
                    strSQL = strSQL + "     from ����_B_���� a" + vbCr
                    strSQL = strSQL + "     where a.�ļ���ʶ = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + "     and   a.�������� = '" + Me.FlowData.TASK_BYQQ + "'" + vbCr
                    strSQL = strSQL + "   ) b on a.�ļ���ʶ=b.�ļ���ʶ and a.�������=b.�������" + vbCr
                    strSQL = strSQL + " ) a" + vbCr
                    If strWhere <> "" Then
                        strSQL = strSQL + " where " + strWhere + vbCr
                    End If
                    strSQL = strSQL + " order by a.�������� desc" + vbCr

                    '���ò���
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    'ִ�в���
                    .Fill(objTempBuyueData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_VT_WENJIANBUYUE))
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            objBuyueData = objTempBuyueData
            getBuyueSendData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.FlowData.SafeRelease(objTempBuyueData)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡָ����Ա���յĲ�������(���������벹��֪ͨ)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ����Ա����
        '     strWhere             ����������(a.)
        '     objBuyueData         ����������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getBuyueRecvData( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByVal strWhere As String, _
            ByRef objBuyueData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim objTempBuyueData As Xydc.Platform.Common.Data.FlowData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            getBuyueRecvData = False
            objBuyueData = Nothing
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim()
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '�������ݼ�
                objTempBuyueData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_VT_WENJIANBUYUE)
                If strWJBS = "" Then Exit Try

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                'ִ�м���
                Dim intZDBY As Integer = Xydc.Platform.Common.Data.FlowData.YJJH_ZHUDONGBUYUE
                With Me.m_objSqlDataAdapter
                    strSQL = ""
                    strSQL = strSQL + " select a.*" + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select a.*," + vbCr
                    strSQL = strSQL + "     ������� = case " + vbCr
                    strSQL = strSQL + "       when isnull(b.�Ƿ���׼,' ') = '1100' then '��׼'" + vbCr
                    strSQL = strSQL + "       when isnull(b.�Ƿ���׼,' ') = '1110' then 'ת��'" + vbCr
                    strSQL = strSQL + "       when isnull(b.�Ƿ���׼,' ') = '1000' then '�ܾ�'" + vbCr
                    strSQL = strSQL + "       else '    ' end" + vbCr
                    strSQL = strSQL + "   from" + vbCr
                    strSQL = strSQL + "   (" + vbCr
                    strSQL = strSQL + "     select a.*" + vbCr
                    strSQL = strSQL + "     from ����_B_���� a" + vbCr
                    strSQL = strSQL + "     where a.�ļ���ʶ = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + "     and   a.������   = '" + strUserXM + "'" + vbCr
                    strSQL = strSQL + "     and   a.�������� = '" + Me.FlowData.TASK_BYQQ + "'" + vbCr
                    strSQL = strSQL + "     union" + vbCr
                    strSQL = strSQL + "     select a.*" + vbCr
                    strSQL = strSQL + "     from ����_B_���� a" + vbCr
                    strSQL = strSQL + "     where a.�ļ���ʶ = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + "     and   a.������   = '" + strUserXM + "'" + vbCr
                    strSQL = strSQL + "     and   a.�������� = '" + Me.FlowData.TASK_BYTZ + "'" + vbCr
                    strSQL = strSQL + "     and   a.ԭ���Ӻ� =  " + intZDBY.ToString + vbCr
                    strSQL = strSQL + "   ) a" + vbCr
                    strSQL = strSQL + "   left join" + vbCr
                    strSQL = strSQL + "   (" + vbCr
                    strSQL = strSQL + "     select a.*" + vbCr
                    strSQL = strSQL + "     from ����_B_���� a" + vbCr
                    strSQL = strSQL + "     where a.�ļ���ʶ = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + "     and   a.�������� = '" + Me.FlowData.TASK_BYQQ + "'" + vbCr
                    strSQL = strSQL + "   ) b on a.�ļ���ʶ=b.�ļ���ʶ and a.�������=b.�������" + vbCr
                    strSQL = strSQL + " ) a" + vbCr
                    If strWhere <> "" Then
                        strSQL = strSQL + " where " + strWhere + vbCr
                    End If
                    strSQL = strSQL + " order by a.�������� desc" + vbCr

                    '���ò���
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    'ִ�в���
                    .Fill(objTempBuyueData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_VT_WENJIANBUYUE))
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            objBuyueData = objTempBuyueData
            getBuyueRecvData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.FlowData.SafeRelease(objTempBuyueData)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' strSender��strReceiver���Ͳ�������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objSqlTransaction    ����������
        '     strFSXH              ����������
        '     strSender            ��������Ա����
        '     strReceiver          ��������Ա����
        '     strJJSM              ������˵��
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doSendBuyueRequest( _
            ByRef strErrMsg As String, _
            ByVal objSqlTransaction As System.Data.SqlClient.SqlTransaction, _
            ByVal strFSXH As String, _
            ByVal strSender As String, _
            ByVal strReceiver As String, _
            ByVal strJJSM As String) As Boolean

            Dim objLocalSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim blnNewTrans As Boolean
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            doSendBuyueRequest = False
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strSender Is Nothing Then strSender = ""
                strSender = strSender.Trim()
                If strReceiver Is Nothing Then strReceiver = ""
                strReceiver = strReceiver.Trim()
                If strReceiver = "" Or strSender = "" Then
                    strErrMsg = "����δָ��[������]��[������]��"
                    GoTo errProc
                End If
                If strFSXH Is Nothing Then strFSXH = ""
                strFSXH = strFSXH.Trim
                If strJJSM Is Nothing Then strJJSM = ""
                strJJSM = strJJSM.Trim

                '��ȡ�ļ���Ϣ
                Dim strTaskStatusWJS As String = Me.FlowData.TASKSTATUS_WJS
                Dim strBYQQ As String = Me.FlowData.TASK_BYQQ                
                Dim strBLLX As String = Me.FlowBLLXName               
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '��ȡ��������
                If objSqlTransaction Is Nothing Then
                    objSqlConnection = Me.SqlConnection
                Else
                    objSqlConnection = objSqlTransaction.Connection
                End If

                '������ѯ����
                objLocalSqlConnection = New System.Data.SqlClient.SqlConnection(objSqlConnection.ConnectionString)
                objLocalSqlConnection.Open()

                '��ȡ�½��ӵ���
                Dim strJJXH As String
                If objdacCommon.getNewCode(strErrMsg, objLocalSqlConnection, "�������", "�ļ���ʶ", strWJBS, "����_B_����", True, strJJXH) = False Then
                    GoTo errProc
                End If
                '����������
                Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate
                Dim strRelaFields As String = "�ļ���ʶ" + strSep + "�������"
                Dim strRelaValue As String = strWJBS + strSep + strFSXH
                Dim strJSXH As String
                If objdacCommon.getNewCode(strErrMsg, objLocalSqlConnection, "�������", strRelaFields, strRelaValue, "����_B_����", True, strJSXH) = False Then
                    GoTo errProc
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objLocalSqlConnection)

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '��ʼ����
                If objSqlTransaction Is Nothing Then
                    blnNewTrans = True
                    objSqlTransaction = objSqlConnection.BeginTransaction
                Else
                    blnNewTrans = False
                End If
                objSqlCommand.Transaction = objSqlTransaction

                '������
                Dim intBYTZ As Integer = Xydc.Platform.Common.Data.FlowData.YJJH_YIBANTONGZHI
                Try
                    '�ύ�µĲ��Ľ��ӵ�(��������)
                    strSQL = ""
                    strSQL = strSQL + " insert into ����_B_���� (" + vbCr
                    strSQL = strSQL + "   �ļ���ʶ," + vbCr
                    strSQL = strSQL + "   �������," + vbCr
                    strSQL = strSQL + "   ԭ���Ӻ�," + vbCr
                    strSQL = strSQL + "   �������," + vbCr
                    strSQL = strSQL + "   ������," + vbCr
                    strSQL = strSQL + "   ��������," + vbCr
                    strSQL = strSQL + "   �������," + vbCr
                    strSQL = strSQL + "   ������," + vbCr
                    strSQL = strSQL + "   ��������," + vbCr
                    strSQL = strSQL + "   �����������," + vbCr
                    strSQL = strSQL + "   �������," + vbCr
                    strSQL = strSQL + "   ��������," + vbCr
                    strSQL = strSQL + "   ��������," + vbCr
                    strSQL = strSQL + "   ����״̬," + vbCr
                    strSQL = strSQL + "   ���ӱ�ʶ," + vbCr
                    strSQL = strSQL + "   ί����  ," + vbCr
                    strSQL = strSQL + "   ����˵�� " + vbCr
                    strSQL = strSQL + " ) values (" + vbCr
                    strSQL = strSQL + " '" + strWJBS + "'," + vbCr
                    strSQL = strSQL + "  " + strJJXH + " ," + vbCr
                    strSQL = strSQL + "  " + intBYTZ.ToString() + " ," + vbCr
                    strSQL = strSQL + "  " + strFSXH + " ," + vbCr
                    strSQL = strSQL + " '" + strSender + "'," + vbCr
                    strSQL = strSQL + " '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "'," + vbCr
                    strSQL = strSQL + "  " + strJSXH + "," + vbCr
                    strSQL = strSQL + " '" + strReceiver + "'," + vbCr
                    strSQL = strSQL + " Null," + vbCr
                    strSQL = strSQL + " '" + Format(Now.AddDays(3), "yyyy-MM-dd HH:mm:ss") + "'," + vbCr
                    strSQL = strSQL + " Null," + vbCr
                    strSQL = strSQL + " '" + strBLLX + "'," + vbCr
                    strSQL = strSQL + " '" + strBYQQ + "'," + vbCr
                    strSQL = strSQL + " '" + strTaskStatusWJS + "'," + vbCr
                    strSQL = strSQL + " '" + "10100100" + "'," + vbCr
                    strSQL = strSQL + " ' '," + vbCr
                    strSQL = strSQL + " '" + strJJSM + "'" + vbCr
                    strSQL = strSQL + " )" + vbCr

                    'ִ��
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    If blnNewTrans = True Then
                        objSqlTransaction.Rollback()
                    End If
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '�ύ����
                If blnNewTrans = True Then
                    objSqlTransaction.Commit()
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objLocalSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            doSendBuyueRequest = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objLocalSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' strSender��strReceiver���Ͳ���֪ͨ
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objSqlTransaction    ����������
        '     strFSXH              ����������
        '     strSender            ��������Ա����
        '     strReceiver          ��������Ա����
        '     strJJSM              ������˵��
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doSendBuyueTongzhi( _
            ByRef strErrMsg As String, _
            ByVal objSqlTransaction As System.Data.SqlClient.SqlTransaction, _
            ByVal strFSXH As String, _
            ByVal strSender As String, _
            ByVal strReceiver As String, _
            ByVal strJJSM As String) As Boolean

            Dim objLocalSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim blnNewTrans As Boolean
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            doSendBuyueTongzhi = False
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strSender Is Nothing Then strSender = ""
                strSender = strSender.Trim()
                If strReceiver Is Nothing Then strReceiver = ""
                strReceiver = strReceiver.Trim()
                If strReceiver = "" Or strSender = "" Then
                    strErrMsg = "����δָ��[������]��[������]��"
                    GoTo errProc
                End If
                If strFSXH Is Nothing Then strFSXH = ""
                strFSXH = strFSXH.Trim
                If strJJSM Is Nothing Then strJJSM = ""
                strJJSM = strJJSM.Trim

                '��ȡ�ļ���Ϣ
                Dim strTaskStatusWJS As String = Me.FlowData.TASKSTATUS_WJS
                Dim strBYTZ As String = Me.FlowData.TASK_BYTZ               
                Dim strBLLX As String = Me.FlowBLLXName               
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '��ȡ��������
                If objSqlTransaction Is Nothing Then
                    objSqlConnection = Me.SqlConnection
                Else
                    objSqlConnection = objSqlTransaction.Connection
                End If

                '������ѯ����
                objLocalSqlConnection = New System.Data.SqlClient.SqlConnection(objSqlConnection.ConnectionString)
                objLocalSqlConnection.Open()

                '��ȡ�½��ӵ���
                Dim strJJXH As String
                If objdacCommon.getNewCode(strErrMsg, objLocalSqlConnection, "�������", "�ļ���ʶ", strWJBS, "����_B_����", True, strJJXH) = False Then
                    GoTo errProc
                End If
                '����������
                Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate
                Dim strRelaFields As String = "�ļ���ʶ" + strSep + "�������"
                Dim strRelaValue As String = strWJBS + strSep + strFSXH
                Dim strJSXH As String
                If objdacCommon.getNewCode(strErrMsg, objLocalSqlConnection, "�������", strRelaFields, strRelaValue, "����_B_����", True, strJSXH) = False Then
                    GoTo errProc
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objLocalSqlConnection)

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '��ʼ����
                If objSqlTransaction Is Nothing Then
                    blnNewTrans = True
                    objSqlTransaction = objSqlConnection.BeginTransaction
                Else
                    blnNewTrans = False
                End If
                objSqlCommand.Transaction = objSqlTransaction

                '������
                Dim intZDTZ As Integer = Xydc.Platform.Common.Data.FlowData.YJJH_ZHUDONGBUYUE
                Try
                    '�ύ�µĲ��Ľ��ӵ�(��������)
                    strSQL = ""
                    strSQL = strSQL + " insert into ����_B_���� (" + vbCr
                    strSQL = strSQL + "   �ļ���ʶ," + vbCr
                    strSQL = strSQL + "   �������," + vbCr
                    strSQL = strSQL + "   ԭ���Ӻ�," + vbCr
                    strSQL = strSQL + "   �������," + vbCr
                    strSQL = strSQL + "   ������," + vbCr
                    strSQL = strSQL + "   ��������," + vbCr
                    strSQL = strSQL + "   �������," + vbCr
                    strSQL = strSQL + "   ������," + vbCr
                    strSQL = strSQL + "   ��������," + vbCr
                    strSQL = strSQL + "   �����������," + vbCr
                    strSQL = strSQL + "   �������," + vbCr
                    strSQL = strSQL + "   ��������," + vbCr
                    strSQL = strSQL + "   ��������," + vbCr
                    strSQL = strSQL + "   ����״̬," + vbCr
                    strSQL = strSQL + "   ���ӱ�ʶ," + vbCr
                    strSQL = strSQL + "   ί����  ," + vbCr
                    strSQL = strSQL + "   ����˵�� " + vbCr
                    strSQL = strSQL + " ) values (" + vbCr
                    strSQL = strSQL + " '" + strWJBS + "'," + vbCr
                    strSQL = strSQL + "  " + strJJXH + " ," + vbCr
                    strSQL = strSQL + "  " + intZDTZ.ToString() + " ," + vbCr
                    strSQL = strSQL + "  " + strFSXH + " ," + vbCr
                    strSQL = strSQL + " '" + strSender + "'," + vbCr
                    strSQL = strSQL + " '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "'," + vbCr
                    strSQL = strSQL + "  " + strJSXH + "," + vbCr
                    strSQL = strSQL + " '" + strReceiver + "'," + vbCr
                    strSQL = strSQL + " Null," + vbCr
                    strSQL = strSQL + " '" + Format(Now.AddDays(3), "yyyy-MM-dd HH:mm:ss") + "'," + vbCr
                    strSQL = strSQL + " Null," + vbCr
                    strSQL = strSQL + " '" + strBLLX + "'," + vbCr
                    strSQL = strSQL + " '" + strBYTZ + "'," + vbCr
                    strSQL = strSQL + " '" + strTaskStatusWJS + "'," + vbCr
                    strSQL = strSQL + " '" + "10100100" + "'," + vbCr
                    strSQL = strSQL + " ' '," + vbCr
                    strSQL = strSQL + " '" + strJJSM + "'" + vbCr
                    strSQL = strSQL + " )" + vbCr

                    'ִ��
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    If blnNewTrans = True Then
                        objSqlTransaction.Rollback()
                    End If
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '�ύ����
                If blnNewTrans = True Then
                    objSqlTransaction.Commit()
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objLocalSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            doSendBuyueTongzhi = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objLocalSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' strSender��strReceiver���Ͳ���֪ͨ
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objSqlTransaction    ����������(�����ܲ�֧����������)
        '     strSender            ��������Ա����
        '     strReceiver          ��������Ա�б�
        '     strJJSM              ������˵��
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doSendBuyueTongzhi( _
            ByRef strErrMsg As String, _
            ByVal objSqlTransaction As System.Data.SqlClient.SqlTransaction, _
            ByVal strSender As String, _
            ByVal strReceiverList As String, _
            ByVal strJJSM As String) As Boolean

            Dim objLocalSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            doSendBuyueTongzhi = False
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strSender Is Nothing Then strSender = ""
                strSender = strSender.Trim()
                If strReceiverList Is Nothing Then strReceiverList = ""
                strReceiverList = strReceiverList.Trim()
                If strReceiverList = "" Or strSender = "" Then
                    strErrMsg = "����δָ��[������]��[������]��"
                    GoTo errProc
                End If
                If strJJSM Is Nothing Then strJJSM = ""
                strJJSM = strJJSM.Trim

                '��ȡ�ļ���Ϣ
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then
                    Exit Try
                End If

                '������Ա�б�
                Dim strJSR() As String
                strJSR = strReceiverList.Split(Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate.ToCharArray)
                If strJSR.Length < 1 Then
                    Exit Try
                End If

                '��ȡ��������
                If objSqlTransaction Is Nothing Then
                    objSqlConnection = Me.SqlConnection
                Else
                    objSqlConnection = objSqlTransaction.Connection
                End If

                '������ѯ����
                objLocalSqlConnection = New System.Data.SqlClient.SqlConnection(objSqlConnection.ConnectionString)
                objLocalSqlConnection.Open()

                '��ȡ�·������
                Dim strFSXH As String
                If objdacCommon.getNewCode(strErrMsg, objLocalSqlConnection, "�������", "�ļ���ʶ", strWJBS, "����_B_����", True, strFSXH) = False Then
                    GoTo errProc
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objLocalSqlConnection)

                '�������֪ͨ
                Dim intCount As Integer
                Dim i As Integer
                intCount = strJSR.Length
                For i = 0 To intCount - 1 Step 1
                    If Me.doSendBuyueTongzhi(strErrMsg, Nothing, strFSXH, strSender, strJSR(i), strJJSM) = False Then
                        GoTo errProc
                    End If
                Next

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objLocalSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            doSendBuyueTongzhi = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objLocalSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �ջز�������
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     objSqlTransaction      ����������
        '     intJJXH                ���������
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doShouhuiBuyueRequest( _
            ByRef strErrMsg As String, _
            ByVal objSqlTransaction As System.Data.SqlClient.SqlTransaction, _
            ByVal intJJXH As Integer) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            Dim blnNewTrans As Boolean = False
            Dim strWJBS As String
            Dim strSQL As String
            Dim strJJBS As String = ""

            '��ʼ��
            doShouhuiBuyueRequest = False
            strErrMsg = ""

            Try
                '���
                If Me.IsInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If


                '��ȡ������Ϣ
                strWJBS = Me.FlowData.WJBS
                If objSqlTransaction Is Nothing Then
                    objSqlConnection = Me.SqlConnection
                Else
                    objSqlConnection = objSqlTransaction.Connection
                End If

                '��ʼ����
                If objSqlTransaction Is Nothing Then
                    blnNewTrans = True
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Else
                    blnNewTrans = False
                End If

                '��������
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout


                    strJJBS = "11000000"

                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@wcrq", Now)
                    objSqlCommand.Parameters.AddWithValue("@blzt", Me.FlowData.TASKSTATUS_BSH)
                    objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                    objSqlCommand.Parameters.AddWithValue("@jjxh", intJJXH)

                    objSqlCommand.Parameters.AddWithValue("@jjbs", strJJBS)


                    strSQL = ""
                    strSQL = strSQL + " update ����_B_���� set " + vbCr
                    strSQL = strSQL + "   ������� = @wcrq," + vbCr
                    strSQL = strSQL + "   ����״̬ = @blzt " + vbCr
                    strSQL = strSQL + " where �ļ���ʶ = @wjbs" + vbCr
                    strSQL = strSQL + " and   ������� = @jjxh" + vbCr

                    strSQL = strSQL + " and   ���ӱ�ʶ = @jjbs" + vbCr

                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo rollDatabase
                End Try

                '�ύ����
                If blnNewTrans = True Then
                    objSqlTransaction.Commit()
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            '����
            doShouhuiBuyueRequest = True
            Exit Function

rollDatabase:
            If blnNewTrans = True Then
                objSqlTransaction.Rollback()
            End If
            GoTo errProc

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �ջز���֪ͨ
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     objSqlTransaction      ����������
        '     intJJXH                ���������
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��

        '----------------------------------------------------------------
        Public Overridable Function doShouhuiBuyueTongzhi( _
            ByRef strErrMsg As String, _
            ByVal objSqlTransaction As System.Data.SqlClient.SqlTransaction, _
            ByVal intJJXH As Integer) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            Dim blnNewTrans As Boolean = False
            Dim strWJBS As String
            Dim strSQL As String
            Dim strJJBS As String = ""

            '��ʼ��
            doShouhuiBuyueTongzhi = False
            strErrMsg = ""

            Try
                '���
                If Me.IsInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If


                '��ȡ������Ϣ
                strWJBS = Me.FlowData.WJBS
                If objSqlTransaction Is Nothing Then
                    objSqlConnection = Me.SqlConnection
                Else
                    objSqlConnection = objSqlTransaction.Connection
                End If

                '��ʼ����
                If objSqlTransaction Is Nothing Then
                    blnNewTrans = True
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Else
                    blnNewTrans = False
                End If

                '��������
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@wcrq", Now)
                    objSqlCommand.Parameters.AddWithValue("@blzt", Me.FlowData.TASKSTATUS_BSH)
                    objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                    objSqlCommand.Parameters.AddWithValue("@jjxh", intJJXH)

                    strSQL = ""
                    strSQL = strSQL + " update ����_B_���� set " + vbCr
                    strSQL = strSQL + "   ������� = @wcrq," + vbCr
                    strSQL = strSQL + "   ����״̬ = @blzt " + vbCr

                    strSQL = strSQL + "   ,���ӱ�ʶ = stuff(���ӱ�ʶ,3,1,'0') " + vbCr

                    strSQL = strSQL + " where �ļ���ʶ = @wjbs" + vbCr
                    strSQL = strSQL + " and   ������� = @jjxh" + vbCr
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo rollDatabase
                End Try

                '�ύ����
                If blnNewTrans = True Then
                    objSqlTransaction.Commit()
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            '����
            doShouhuiBuyueTongzhi = True
            Exit Function

rollDatabase:
            If blnNewTrans = True Then
                objSqlTransaction.Rollback()
            End If
            GoTo errProc

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Exit Function

        End Function


        '----------------------------------------------------------------
        ' ��׼��������
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     objSqlTransaction      ����������
        '     intJJXH                ���������
        '     strFSXH                ����������
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doPizhunBuyueRequest( _
            ByRef strErrMsg As String, _
            ByVal objSqlTransaction As System.Data.SqlClient.SqlTransaction, _
            ByVal intJJXH As Integer, _
            ByVal strFSXH As String) As Boolean

            Dim objLocalSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objDataSet As System.Data.DataSet

            Dim blnNewTrans As Boolean = False
            Dim strWJBS As String
            Dim strSQL As String

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim strBLSY As String
            Dim strJSR As String
            Dim strFSR As String

            '��ʼ��
            doPizhunBuyueRequest = False
            strErrMsg = ""

            Try
                '���
                If Me.IsInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strFSXH Is Nothing Then strFSXH = ""
                strFSXH = strFSXH.Trim
                If strFSXH = "" Then strFSXH = "0"

                '��ȡ������Ϣ
                Dim strBLLX As String = Me.FlowBLLXName
                strWJBS = Me.FlowData.WJBS
                If objSqlTransaction Is Nothing Then
                    objSqlConnection = Me.SqlConnection
                Else
                    objSqlConnection = objSqlTransaction.Connection
                End If

                '������ѯ����
                objLocalSqlConnection = New System.Data.SqlClient.SqlConnection(objSqlConnection.ConnectionString)
                objLocalSqlConnection.Open()

                '����intJJXH�Ƿ����а�����Ϣ
                Dim blnHasBLXX As Boolean = False
                strSQL = ""
                strSQL = strSQL + " select *" + vbCr
                strSQL = strSQL + " from ����_B_����" + vbCr
                strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "'" + vbCr
                strSQL = strSQL + " and   ������� =  " + intJJXH.ToString + "" + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objLocalSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    blnHasBLXX = True
                Else
                    blnHasBLXX = False
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '��ȡ������Ϣ
                strSQL = ""
                strSQL = strSQL + " select *" + vbCr
                strSQL = strSQL + " from ����_B_����" + vbCr
                strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "'" + vbCr
                strSQL = strSQL + " and   ������� =  " + intJJXH.ToString + "" + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objLocalSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    With objDataSet.Tables(0).Rows(0)
                        strBLSY = objPulicParameters.getObjectValue(.Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_BLZL), "")
                        strFSR = objPulicParameters.getObjectValue(.Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSR), "")
                        strJSR = objPulicParameters.getObjectValue(.Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSR), "")
                    End With
                Else
                    strErrMsg = "����ָ�������󲻴��ڣ�"
                    GoTo errProc
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '��ȡ�½��ӵ���
                Dim strJJXH As String
                If objdacCommon.getNewCode(strErrMsg, objLocalSqlConnection, "�������", "�ļ���ʶ", strWJBS, "����_B_����", True, strJJXH) = False Then
                    GoTo errProc
                End If
                '����������
                Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate
                Dim strRelaFields As String = "�ļ���ʶ" + strSep + "�������"
                Dim strRelaValue As String = strWJBS + strSep + strFSXH
                Dim strJSXH As String
                If objdacCommon.getNewCode(strErrMsg, objLocalSqlConnection, "�������", strRelaFields, strRelaValue, "����_B_����", True, strJSXH) = False Then
                    GoTo errProc
                End If

                '��ʼ����
                If objSqlTransaction Is Nothing Then
                    blnNewTrans = True
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Else
                    blnNewTrans = False
                End If

                '��������
                Dim intBYTZ As Integer = Xydc.Platform.Common.Data.FlowData.YJJH_YIBANTONGZHI
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '���͡���׼֪ͨ��
                    strSQL = ""
                    strSQL = strSQL + " insert into ����_B_���� (" + vbCr
                    strSQL = strSQL + "   �ļ���ʶ," + vbCr
                    strSQL = strSQL + "   �������," + vbCr
                    strSQL = strSQL + "   ԭ���Ӻ�," + vbCr
                    strSQL = strSQL + "   �������," + vbCr
                    strSQL = strSQL + "   �������," + vbCr
                    strSQL = strSQL + "   ��������," + vbCr
                    strSQL = strSQL + "   ��������," + vbCr
                    strSQL = strSQL + "   ����״̬," + vbCr
                    strSQL = strSQL + "   ���ӱ�ʶ," + vbCr
                    strSQL = strSQL + "   ������," + vbCr
                    strSQL = strSQL + "   ��������," + vbCr
                    strSQL = strSQL + "   ������," + vbCr
                    strSQL = strSQL + "   �����������," + vbCr
                    strSQL = strSQL + "   ί����," + vbCr
                    strSQL = strSQL + "   ����˵��" + vbCr
                    strSQL = strSQL + " ) values (" + vbCr
                    strSQL = strSQL + " '" + strWJBS + "'," + vbCr
                    strSQL = strSQL + "  " + strJJXH + "," + vbCr
                    strSQL = strSQL + "  " + intBYTZ.ToString + "," + vbCr
                    strSQL = strSQL + "  " + strFSXH + "," + vbCr
                    strSQL = strSQL + "  " + strJSXH + "," + vbCr
                    strSQL = strSQL + " '" + strBLLX + "'," + vbCr
                    strSQL = strSQL + " '" + Me.FlowData.TASK_BYTZ + "'," + vbCr
                    strSQL = strSQL + " '" + Me.FlowData.TASKSTATUS_WJS + "'," + vbCr
                    strSQL = strSQL + " '" + "10100100" + "'," + vbCr
                    strSQL = strSQL + " '" + strJSR + "'," + vbCr
                    strSQL = strSQL + " '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "'," + vbCr
                    strSQL = strSQL + " '" + strFSR + "'," + vbCr
                    strSQL = strSQL + " '" + Format(Now.AddDays(3), "yyyy-MM-dd HH:mm:ss") + "'," + vbCr
                    strSQL = strSQL + " '" + " " + "'," + vbCr
                    strSQL = strSQL + " '" + "���Ĳ��������ѱ���׼��" + "'" + vbCr
                    strSQL = strSQL + " )" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '����������
                    strSQL = ""
                    strSQL = strSQL + " update ����_B_���� set" + vbCr
                    strSQL = strSQL + "   �������� = '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "'," + vbCr
                    strSQL = strSQL + "   ����ֽ���ļ� = ����ֽ���ļ�," + vbCr
                    strSQL = strSQL + "   ���յ����ļ� = ���͵����ļ�," + vbCr
                    strSQL = strSQL + "   ����ֽ�ʸ��� = ����ֽ�ʸ���," + vbCr
                    strSQL = strSQL + "   ���յ��Ӹ��� = ���͵��Ӹ��� " + vbCr
                    strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + " and   ������� =  " + intJJXH.ToString + vbCr
                    strSQL = strSQL + " and   �������� is null" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '���������Ѿ����
                    strSQL = ""
                    strSQL = strSQL + " update ����_B_���� set" + vbCr
                    strSQL = strSQL + "   ������� = '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "'," + vbCr
                    strSQL = strSQL + "   ����״̬ = '" + Me.FlowData.TASKSTATUS_YWC + "'" + vbCr
                    strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + " and   ������� =  " + intJJXH.ToString + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '����׼����
                    If blnHasBLXX = False Then
                        strSQL = ""
                        strSQL = strSQL + " insert into ����_B_���� ("
                        strSQL = strSQL + "   �ļ���ʶ,"
                        strSQL = strSQL + "   �������,"
                        strSQL = strSQL + "   ������,"
                        strSQL = strSQL + "   ��������,"
                        strSQL = strSQL + "   ��������,"
                        strSQL = strSQL + "   ��������,"
                        strSQL = strSQL + "   �Ƿ���׼ "
                        strSQL = strSQL + " ) values ("
                        strSQL = strSQL + " '" + strWJBS + "',"
                        strSQL = strSQL + "  " + intJJXH.ToString + ","
                        strSQL = strSQL + " '" + strJSR + "',"
                        strSQL = strSQL + " '" + strBLLX + "',"
                        strSQL = strSQL + " '" + strBLSY + "',"
                        strSQL = strSQL + " '" + Format(Now, "yyyy-MM-dd") + "',"
                        strSQL = strSQL + " '" + "1100" + "'"
                        strSQL = strSQL + ")"
                    Else
                        strSQL = ""
                        strSQL = strSQL + " update ����_B_���� set" + vbCr
                        strSQL = strSQL + "   �������� = '" + Format(Now, "yyyy-MM-dd") + "'," + vbCr
                        strSQL = strSQL + "   �Ƿ���׼ = '" + "1100" + "'" + vbCr
                        strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "'" + vbCr
                        strSQL = strSQL + " and   ������� =  " + intJJXH.ToString + " " + vbCr
                    End If
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo rollDatabase
                End Try

                '�ύ����
                If blnNewTrans = True Then
                    objSqlTransaction.Commit()
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objLocalSqlConnection)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            doPizhunBuyueRequest = True
            Exit Function

rollDatabase:
            If blnNewTrans = True Then
                objSqlTransaction.Rollback()
            End If
            GoTo errProc

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objLocalSqlConnection)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �ܾ���������
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     objSqlTransaction      ����������
        '     intJJXH                ���������
        '     strFSXH                ����������
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doJujueBuyueRequest( _
            ByRef strErrMsg As String, _
            ByVal objSqlTransaction As System.Data.SqlClient.SqlTransaction, _
            ByVal intJJXH As Integer, _
            ByVal strFSXH As String) As Boolean

            Dim objLocalSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objDataSet As System.Data.DataSet

            Dim blnNewTrans As Boolean = False
            Dim strWJBS As String
            Dim strSQL As String

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim strBLSY As String
            Dim strJSR As String
            Dim strFSR As String

            '��ʼ��
            doJujueBuyueRequest = False
            strErrMsg = ""

            Try
                '���
                If Me.IsInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strFSXH Is Nothing Then strFSXH = ""
                strFSXH = strFSXH.Trim
                If strFSXH = "" Then strFSXH = "0"

                '��ȡ������Ϣ
                Dim strBLLX As String = Me.FlowBLLXName
                strWJBS = Me.FlowData.WJBS
                If objSqlTransaction Is Nothing Then
                    objSqlConnection = Me.SqlConnection
                Else
                    objSqlConnection = objSqlTransaction.Connection
                End If

                '������ѯ����
                objLocalSqlConnection = New System.Data.SqlClient.SqlConnection(objSqlConnection.ConnectionString)
                objLocalSqlConnection.Open()

                '����intJJXH�Ƿ����а�����Ϣ
                Dim blnHasBLXX As Boolean = False
                strSQL = ""
                strSQL = strSQL + " select *" + vbCr
                strSQL = strSQL + " from ����_B_����" + vbCr
                strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "'" + vbCr
                strSQL = strSQL + " and   ������� =  " + intJJXH.ToString + "" + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objLocalSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    blnHasBLXX = True
                Else
                    blnHasBLXX = False
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '��ȡ������Ϣ
                strSQL = ""
                strSQL = strSQL + " select *" + vbCr
                strSQL = strSQL + " from ����_B_����" + vbCr
                strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "'" + vbCr
                strSQL = strSQL + " and   ������� =  " + intJJXH.ToString + "" + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objLocalSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    With objDataSet.Tables(0).Rows(0)
                        strBLSY = objPulicParameters.getObjectValue(.Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_BLZL), "")
                        strFSR = objPulicParameters.getObjectValue(.Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSR), "")
                        strJSR = objPulicParameters.getObjectValue(.Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSR), "")
                    End With
                Else
                    strErrMsg = "����ָ�������󲻴��ڣ�"
                    GoTo errProc
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '��ȡ�½��ӵ���
                Dim strJJXH As String
                If objdacCommon.getNewCode(strErrMsg, objLocalSqlConnection, "�������", "�ļ���ʶ", strWJBS, "����_B_����", True, strJJXH) = False Then
                    GoTo errProc
                End If
                '����������
                Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate
                Dim strRelaFields As String = "�ļ���ʶ" + strSep + "�������"
                Dim strRelaValue As String = strWJBS + strSep + strFSXH
                Dim strJSXH As String
                If objdacCommon.getNewCode(strErrMsg, objLocalSqlConnection, "�������", strRelaFields, strRelaValue, "����_B_����", True, strJSXH) = False Then
                    GoTo errProc
                End If

                '��ʼ����
                If objSqlTransaction Is Nothing Then
                    blnNewTrans = True
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Else
                    blnNewTrans = False
                End If

                '��������
                Dim intBYTZ As Integer = Xydc.Platform.Common.Data.FlowData.YJJH_YIBANTONGZHI
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '���͡��ܾ�֪ͨ��
                    strSQL = ""
                    strSQL = strSQL + " insert into ����_B_���� (" + vbCr
                    strSQL = strSQL + "   �ļ���ʶ," + vbCr
                    strSQL = strSQL + "   �������," + vbCr
                    strSQL = strSQL + "   ԭ���Ӻ�," + vbCr
                    strSQL = strSQL + "   �������," + vbCr
                    strSQL = strSQL + "   �������," + vbCr
                    strSQL = strSQL + "   ��������," + vbCr
                    strSQL = strSQL + "   ��������," + vbCr
                    strSQL = strSQL + "   ����״̬," + vbCr
                    strSQL = strSQL + "   ���ӱ�ʶ," + vbCr
                    strSQL = strSQL + "   ������," + vbCr
                    strSQL = strSQL + "   ��������," + vbCr
                    strSQL = strSQL + "   ������," + vbCr
                    strSQL = strSQL + "   �����������," + vbCr
                    strSQL = strSQL + "   ί����," + vbCr
                    strSQL = strSQL + "   ����˵��" + vbCr
                    strSQL = strSQL + " ) values (" + vbCr
                    strSQL = strSQL + " '" + strWJBS + "'," + vbCr
                    strSQL = strSQL + "  " + strJJXH + "," + vbCr
                    strSQL = strSQL + "  " + intBYTZ.ToString + "," + vbCr
                    strSQL = strSQL + "  " + strFSXH + "," + vbCr
                    strSQL = strSQL + "  " + strJSXH + "," + vbCr
                    strSQL = strSQL + " '" + strBLLX + "'," + vbCr
                    strSQL = strSQL + " '" + Me.FlowData.TASK_BYTZ + "'," + vbCr
                    strSQL = strSQL + " '" + Me.FlowData.TASKSTATUS_WJS + "'," + vbCr
                    strSQL = strSQL + " '" + "10100100" + "'," + vbCr
                    strSQL = strSQL + " '" + strJSR + "'," + vbCr
                    strSQL = strSQL + " '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "'," + vbCr
                    strSQL = strSQL + " '" + strFSR + "'," + vbCr
                    strSQL = strSQL + " '" + Format(Now.AddDays(3), "yyyy-MM-dd HH:mm:ss") + "'," + vbCr
                    strSQL = strSQL + " '" + " " + "'," + vbCr
                    strSQL = strSQL + " '" + "���Ĳ�������û����׼��" + "'" + vbCr
                    strSQL = strSQL + " )" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '����������
                    strSQL = ""
                    strSQL = strSQL + " update ����_B_���� set" + vbCr
                    strSQL = strSQL + "   �������� = '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "'," + vbCr
                    strSQL = strSQL + "   ����ֽ���ļ� = ����ֽ���ļ�," + vbCr
                    strSQL = strSQL + "   ���յ����ļ� = ���͵����ļ�," + vbCr
                    strSQL = strSQL + "   ����ֽ�ʸ��� = ����ֽ�ʸ���," + vbCr
                    strSQL = strSQL + "   ���յ��Ӹ��� = ���͵��Ӹ��� " + vbCr
                    strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + " and   ������� =  " + intJJXH.ToString + vbCr
                    strSQL = strSQL + " and   �������� is null" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '���������Ѿ����
                    strSQL = ""
                    strSQL = strSQL + " update ����_B_���� set" + vbCr
                    strSQL = strSQL + "   ������� = '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "'," + vbCr
                    strSQL = strSQL + "   ����״̬ = '" + Me.FlowData.TASKSTATUS_YWC + "'" + vbCr
                    strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + " and   ������� =  " + intJJXH.ToString + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '���ܾ�����
                    If blnHasBLXX = False Then
                        strSQL = ""
                        strSQL = strSQL + " insert into ����_B_���� ("
                        strSQL = strSQL + "   �ļ���ʶ,"
                        strSQL = strSQL + "   �������,"
                        strSQL = strSQL + "   ������,"
                        strSQL = strSQL + "   ��������,"
                        strSQL = strSQL + "   ��������,"
                        strSQL = strSQL + "   ��������,"
                        strSQL = strSQL + "   �Ƿ���׼ "
                        strSQL = strSQL + " ) values ("
                        strSQL = strSQL + " '" + strWJBS + "',"
                        strSQL = strSQL + "  " + intJJXH.ToString + ","
                        strSQL = strSQL + " '" + strJSR + "',"
                        strSQL = strSQL + " '" + strBLLX + "',"
                        strSQL = strSQL + " '" + strBLSY + "',"
                        strSQL = strSQL + " '" + Format(Now, "yyyy-MM-dd") + "',"
                        strSQL = strSQL + " '" + "1000" + "'"
                        strSQL = strSQL + ")"
                    Else
                        strSQL = ""
                        strSQL = strSQL + " update ����_B_���� set" + vbCr
                        strSQL = strSQL + "   �������� = '" + Format(Now, "yyyy-MM-dd") + "'," + vbCr
                        strSQL = strSQL + "   �Ƿ���׼ = '" + "1000" + "'" + vbCr
                        strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "'" + vbCr
                        strSQL = strSQL + " and   ������� =  " + intJJXH.ToString + " " + vbCr
                    End If
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo rollDatabase
                End Try

                '�ύ����
                If blnNewTrans = True Then
                    objSqlTransaction.Commit()
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objLocalSqlConnection)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            doJujueBuyueRequest = True
            Exit Function

rollDatabase:
            If blnNewTrans = True Then
                objSqlTransaction.Rollback()
            End If
            GoTo errProc

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objLocalSqlConnection)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ת����������
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     objSqlTransaction      ����������
        '     intJJXH                ���������
        '     strFSXH                ����������
        '     strZFJSR               ��ת������Ľ������б�
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doZhuanfaBuyueRequest( _
            ByRef strErrMsg As String, _
            ByVal objSqlTransaction As System.Data.SqlClient.SqlTransaction, _
            ByVal intJJXH As Integer, _
            ByVal strFSXH As String, _
            ByVal strZFJSR As String) As Boolean

            Dim objLocalSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objLocalSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objDataSet As System.Data.DataSet

            Dim blnNewTrans As Boolean = False
            Dim strWJBS As String
            Dim strSQL As String

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim strBLSY As String
            Dim strJSR As String
            Dim strFSR As String

            '��ʼ��
            doZhuanfaBuyueRequest = False
            strErrMsg = ""

            Try
                '���
                If Me.IsInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strFSXH Is Nothing Then strFSXH = ""
                strFSXH = strFSXH.Trim
                If strFSXH = "" Then strFSXH = "0"
                If strZFJSR Is Nothing Then strZFJSR = ""
                strZFJSR = strZFJSR.Trim
                If strZFJSR = "" Then
                    strErrMsg = "����û��ָ��ת����˭��"
                    GoTo errProc
                End If
                Dim strArray() As String
                strArray = strZFJSR.Split(objPulicParameters.CharSeparate.ToCharArray)

                '��ȡ������Ϣ
                Dim strBLLX As String = Me.FlowBLLXName
                strWJBS = Me.FlowData.WJBS
                If objSqlTransaction Is Nothing Then
                    objSqlConnection = Me.SqlConnection
                Else
                    objSqlConnection = objSqlTransaction.Connection
                End If

                '������ѯ����
                objLocalSqlConnection = New System.Data.SqlClient.SqlConnection(objSqlConnection.ConnectionString)
                objLocalSqlConnection.Open()

                '����intJJXH�Ƿ����а�����Ϣ
                Dim blnHasBLXX As Boolean = False
                strSQL = ""
                strSQL = strSQL + " select *" + vbCr
                strSQL = strSQL + " from ����_B_����" + vbCr
                strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "'" + vbCr
                strSQL = strSQL + " and   ������� =  " + intJJXH.ToString + "" + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objLocalSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    blnHasBLXX = True
                Else
                    blnHasBLXX = False
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '��ȡ������Ϣ
                strSQL = ""
                strSQL = strSQL + " select *" + vbCr
                strSQL = strSQL + " from ����_B_����" + vbCr
                strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "'" + vbCr
                strSQL = strSQL + " and   ������� =  " + intJJXH.ToString + "" + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objLocalSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    With objDataSet.Tables(0).Rows(0)
                        strBLSY = objPulicParameters.getObjectValue(.Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_BLZL), "")
                        strFSR = objPulicParameters.getObjectValue(.Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSR), "")
                        strJSR = objPulicParameters.getObjectValue(.Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSR), "")
                    End With
                Else
                    strErrMsg = "����ָ�������󲻴��ڣ�"
                    GoTo errProc
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                'ת������
                Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate
                Dim intZFTZ As Integer = Xydc.Platform.Common.Data.FlowData.YJJH_ZHUANSONGQINGQIU
                Dim strRelaFields As String
                Dim strRelaValue As String
                Dim strJJXH As String
                Dim strJSXH As String
                Dim intCount As Integer
                Dim i As Integer
                objSqlCommand = objLocalSqlConnection.CreateCommand()
                objSqlCommand.Connection = objLocalSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout
                intCount = strArray.Length
                For i = 0 To intCount - 1 Step 1
                    '��ȡ�½��ӵ���
                    If objdacCommon.getNewCode(strErrMsg, objLocalSqlConnection, "�������", "�ļ���ʶ", strWJBS, "����_B_����", True, strJJXH) = False Then
                        GoTo errProc
                    End If
                    '����������
                    strRelaFields = "�ļ���ʶ" + strSep + "�������"
                    strRelaValue = strWJBS + strSep + strFSXH
                    If objdacCommon.getNewCode(strErrMsg, objLocalSqlConnection, "�������", strRelaFields, strRelaValue, "����_B_����", True, strJSXH) = False Then
                        GoTo errProc
                    End If

                    '��ʼ����
                    objLocalSqlTransaction = objLocalSqlConnection.BeginTransaction()
                    objSqlCommand.Transaction = objLocalSqlTransaction

                    '��������
                    Try
                        'ת������������
                        strSQL = ""
                        strSQL = strSQL + " insert into ����_B_���� (" + vbCr
                        strSQL = strSQL + "   �ļ���ʶ," + vbCr
                        strSQL = strSQL + "   �������," + vbCr
                        strSQL = strSQL + "   ԭ���Ӻ�," + vbCr
                        strSQL = strSQL + "   �������," + vbCr
                        strSQL = strSQL + "   �������," + vbCr
                        strSQL = strSQL + "   ��������," + vbCr
                        strSQL = strSQL + "   ��������," + vbCr
                        strSQL = strSQL + "   ����״̬," + vbCr
                        strSQL = strSQL + "   ���ӱ�ʶ," + vbCr
                        strSQL = strSQL + "   ������," + vbCr
                        strSQL = strSQL + "   ��������," + vbCr
                        strSQL = strSQL + "   ������," + vbCr
                        strSQL = strSQL + "   �����������," + vbCr
                        strSQL = strSQL + "   ί����," + vbCr
                        strSQL = strSQL + "   ����˵��" + vbCr
                        strSQL = strSQL + " ) values (" + vbCr
                        strSQL = strSQL + " '" + strWJBS + "'," + vbCr
                        strSQL = strSQL + "  " + strJJXH + "," + vbCr
                        strSQL = strSQL + "  " + intZFTZ.ToString + "," + vbCr
                        strSQL = strSQL + "  " + strFSXH + "," + vbCr
                        strSQL = strSQL + "  " + strJSXH + "," + vbCr
                        strSQL = strSQL + " '" + strBLLX + "'," + vbCr
                        strSQL = strSQL + " '" + Me.FlowData.TASK_BYQQ + "'," + vbCr
                        strSQL = strSQL + " '" + Me.FlowData.TASKSTATUS_WJS + "'," + vbCr
                        strSQL = strSQL + " '" + "10100100" + "'," + vbCr
                        strSQL = strSQL + " '" + strFSR + "'," + vbCr
                        strSQL = strSQL + " '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "'," + vbCr
                        strSQL = strSQL + " '" + strArray(i) + "'," + vbCr
                        strSQL = strSQL + " '" + Format(Now.AddDays(3), "yyyy-MM-dd HH:mm:ss") + "'," + vbCr
                        strSQL = strSQL + " '" + strJSR + "'," + vbCr
                        strSQL = strSQL + " '" + " " + "'" + vbCr
                        strSQL = strSQL + " )" + vbCr
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.ExecuteNonQuery()

                    Catch ex As Exception
                        objLocalSqlTransaction.Rollback()
                        GoTo errProc
                    End Try

                    '�ύ����
                    objLocalSqlTransaction.Commit()
                Next
                If Not (objSqlCommand Is Nothing) Then
                    Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
                    objSqlCommand = Nothing
                End If

                '��ʼ����
                If objSqlTransaction Is Nothing Then
                    blnNewTrans = True
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Else
                    blnNewTrans = False
                End If

                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '����������
                    strSQL = ""
                    strSQL = strSQL + " update ����_B_���� set" + vbCr
                    strSQL = strSQL + "   �������� = '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "'," + vbCr
                    strSQL = strSQL + "   ����ֽ���ļ� = ����ֽ���ļ�," + vbCr
                    strSQL = strSQL + "   ���յ����ļ� = ���͵����ļ�," + vbCr
                    strSQL = strSQL + "   ����ֽ�ʸ��� = ����ֽ�ʸ���," + vbCr
                    strSQL = strSQL + "   ���յ��Ӹ��� = ���͵��Ӹ��� " + vbCr
                    strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + " and   ������� =  " + intJJXH.ToString + vbCr
                    strSQL = strSQL + " and   �������� is null" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '���������Ѿ����
                    strSQL = ""
                    strSQL = strSQL + " update ����_B_���� set" + vbCr
                    strSQL = strSQL + "   ������� = '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "'," + vbCr
                    strSQL = strSQL + "   ����״̬ = '" + Me.FlowData.TASKSTATUS_YWC + "'" + vbCr
                    strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + " and   ������� =  " + intJJXH.ToString + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '��ת������
                    If blnHasBLXX = False Then
                        strSQL = ""
                        strSQL = strSQL + " insert into ����_B_���� ("
                        strSQL = strSQL + "   �ļ���ʶ,"
                        strSQL = strSQL + "   �������,"
                        strSQL = strSQL + "   ������,"
                        strSQL = strSQL + "   ��������,"
                        strSQL = strSQL + "   ��������,"
                        strSQL = strSQL + "   ��������,"
                        strSQL = strSQL + "   �Ƿ���׼ "
                        strSQL = strSQL + " ) values ("
                        strSQL = strSQL + " '" + strWJBS + "',"
                        strSQL = strSQL + "  " + intJJXH.ToString + ","
                        strSQL = strSQL + " '" + strJSR + "',"
                        strSQL = strSQL + " '" + strBLLX + "',"
                        strSQL = strSQL + " '" + strBLSY + "',"
                        strSQL = strSQL + " '" + Format(Now, "yyyy-MM-dd") + "',"
                        strSQL = strSQL + " '" + "1110" + "'"
                        strSQL = strSQL + ")"
                    Else
                        strSQL = ""
                        strSQL = strSQL + " update ����_B_���� set" + vbCr
                        strSQL = strSQL + "   �������� = '" + Format(Now, "yyyy-MM-dd") + "'," + vbCr
                        strSQL = strSQL + "   �Ƿ���׼ = '" + "1110" + "'" + vbCr
                        strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "'" + vbCr
                        strSQL = strSQL + " and   ������� =  " + intJJXH.ToString + " " + vbCr
                    End If
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo rollDatabase
                End Try

                '�ύ����
                If blnNewTrans = True Then
                    objSqlTransaction.Commit()
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objLocalSqlConnection)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            doZhuanfaBuyueRequest = True
            Exit Function

rollDatabase:
            If blnNewTrans = True Then
                objSqlTransaction.Rollback()
            End If
            GoTo errProc

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objLocalSqlConnection)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �Ѿ��Ķ�ָ������֪ͨ
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     intJJXH              ���������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doReadBuyueTongzhi( _
            ByRef strErrMsg As String, _
            ByVal intJJXH As Integer) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            doReadBuyueTongzhi = False
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If

                '��ȡ�ļ���Ϣ
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then
                    Exit Try
                End If

                '��ȡ��������
                objSqlConnection = Me.SqlConnection

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '��ʼ����
                objSqlTransaction = objSqlConnection.BeginTransaction
                objSqlCommand.Transaction = objSqlTransaction

                '������
                Try
                    '��������Ϣ
                    strSQL = ""
                    strSQL = strSQL + " update ����_B_���� set" + vbCr
                    strSQL = strSQL + "   �������� = '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "'," + vbCr
                    strSQL = strSQL + "   ����ֽ���ļ� = ����ֽ���ļ�," + vbCr
                    strSQL = strSQL + "   ���յ����ļ� = ���͵����ļ�," + vbCr
                    strSQL = strSQL + "   ����ֽ�ʸ��� = ����ֽ�ʸ���," + vbCr
                    strSQL = strSQL + "   ���յ��Ӹ��� = ���͵��Ӹ��� " + vbCr
                    strSQL = strSQL + "where �ļ���ʶ = '" + strWJBS + "'" + vbCr                    '��ǰ�ļ�
                    strSQL = strSQL + "and   ������� =  " + intJJXH.ToString + "" + vbCr            'ָ������
                    strSQL = strSQL + "and   �������� is null" + vbCr                                'δ����
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.ExecuteNonQuery()

                    '����Ϊ���
                    strSQL = ""
                    strSQL = strSQL + " update ����_B_���� set" + vbCr
                    strSQL = strSQL + "   ����״̬ = '" + Me.FlowData.TASKSTATUS_YYD + "'," + vbCr
                    strSQL = strSQL + "   ������� = '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "'" + vbCr
                    strSQL = strSQL + "where �ļ���ʶ = '" + strWJBS + "'" + vbCr                    '��ǰ�ļ�
                    strSQL = strSQL + "and   ������� =  " + intJJXH.ToString + "" + vbCr            'ָ������
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '�ύ����
                objSqlTransaction.Commit()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            doReadBuyueTongzhi = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ��ǰ�ļ������ܿ����ļ�����Ա�����SQL���
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ������Ա����
        '     strSQL               ��(����)��Ա�����SQL
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getAllJsrSql( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByRef strSQL As String) As Boolean

            getAllJsrSql = False
            strErrMsg = ""
            strSQL = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim
                If strUserXM = "" Then
                    strErrMsg = "����û��ָ����ǰ������Ա��"
                    GoTo errProc
                End If

                '��ȡ�ļ���Ϣ
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then
                    Exit Try
                End If

                '����SQL
                strSQL = ""
                strSQL = strSQL + " select b.��Ա����" + vbCr
                strSQL = strSQL + " from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select ������ as ��Ա����" + vbCr
                strSQL = strSQL + "   from ����_B_����" + vbCr
                strSQL = strSQL + "   where �ļ���ʶ =  '" + strWJBS + "'" + vbCr
                strSQL = strSQL + "   and   ������   <> '" + strUserXM + "'" + vbCr
                strSQL = strSQL + "   and   rtrim(���ӱ�ʶ) like '__1%'" + vbCr
                strSQL = strSQL + "   group by ������" + vbCr
                strSQL = strSQL + "   union" + vbCr
                strSQL = strSQL + "   select ������ as ��Ա����" + vbCr
                strSQL = strSQL + "   from ����_B_����" + vbCr
                strSQL = strSQL + "   where �ļ���ʶ =  '" + strWJBS + "'" + vbCr
                strSQL = strSQL + "   and   ������   <> '" + strUserXM + "'" + vbCr
                strSQL = strSQL + "   and   rtrim(���ӱ�ʶ) like '_1%'" + vbCr
                strSQL = strSQL + "   group by ������" + vbCr
                strSQL = strSQL + " ) a" + vbCr
                strSQL = strSQL + " left join ����_B_��Ա b on a.��Ա���� = b.��Ա����" + vbCr
                strSQL = strSQL + " group by b.��Ա����" + vbCr

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getAllJsrSql = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡstrUserXM�ܲ��ǵ�ǰ�ļ���Щ�쵼�����
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ������������
        '     strList              ��(����)��Ա�����б�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getKeBudengLingdao( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByRef strList As String) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objDataSet As System.Data.DataSet

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCustomer As New Xydc.Platform.DataAccess.dacCustomer
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim strSQL As String

            getKeBudengLingdao = False
            strErrMsg = ""
            strList = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim
                If strUserXM = "" Then
                    strErrMsg = "����û��ָ�����������ƣ�"
                    GoTo errProc
                End If

                '��ȡ�ļ���Ϣ
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then
                    Exit Try
                End If
                objSqlConnection = Me.SqlConnection

                '������Ա���롢��λ����
                Dim strBdrBmdm As String = ""
                Dim strBdrBmmc As String = ""
                Dim strBdrId As String = ""
                If objdacCustomer.getRydmByRymc(strErrMsg, objSqlConnection, strUserXM, strBdrId) = False Then
                    GoTo errProc
                End If
                If objdacCustomer.getBmdmAndBmmcByRymc(strErrMsg, objSqlConnection, strUserXM, strBdrBmdm, strBdrBmmc) = False Then
                    GoTo errProc
                End If

                '��ȡ���ļ��е�����������
                strSQL = ""
                strSQL = strSQL + " select ������" + vbCr
                strSQL = strSQL + " from ����_B_����" + vbCr
                strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "'" + vbCr                         '��ǰ�ļ�
                strSQL = strSQL + " and   �������� in (" + Me.FlowData.TaskBlzlSPSYList + ")" + vbCr   '��������
                strSQL = strSQL + " and   rtrim(���ӱ�ʶ) like '1_1__0%'" + vbCr                              '�ѷ���+�����˿ɼ�+��֪ͨ
                strSQL = strSQL + " group by ������" + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If

                '���㷵��
                Dim intCount As Integer
                Dim blnDo As Boolean
                Dim strJSR As String
                Dim i As Integer
                With objDataSet.Tables(0)
                    intCount = .Rows.Count
                    For i = 0 To intCount - 1 Step 1
                        strJSR = objPulicParameters.getObjectValue(.Rows(i).Item("������"), "")
                        If strJSR <> "" Then
                            '�Ƿ�ɲ��ǣ�
                            If Me.canBuDengFile(strErrMsg, strBdrId, strBdrBmdm, strJSR, blnDo) = False Then
                                GoTo errProc
                            End If
                            If blnDo = True Then
                                If strList = "" Then
                                    strList = strJSR
                                Else
                                    strList = strList + objPulicParameters.CharSeparate + strJSR
                                End If
                            End If
                        End If
                    Next
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getKeBudengLingdao = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡstrUserXM���1�ε���������Ľ��ӵ�(��������)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ���û�����
        '     blnZTXZ              ��=True��δ���꣬False������״̬
        '     objJiaoJieData       ���������1�ν�������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getLastSpsyJiaojieData( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByVal blnZTXZ As Boolean, _
            ByRef objJiaoJieData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objTempJiaoJieData As Xydc.Platform.Common.Data.FlowData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            getLastSpsyJiaojieData = False
            objJiaoJieData = Nothing
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '�������������ţ�׼�򣺽����˿ɿ������ӡ����͡�������Ϣ
                strSQL = ""
                strSQL = strSQL + " select isnull(max(�������),0) as ������� " + vbCr
                strSQL = strSQL + " from ����_B_����" + vbCr
                strSQL = strSQL + " where �ļ���ʶ =  '" + strWJBS + "' " + vbCr                              '��ǰ�ļ�
                strSQL = strSQL + " and   ������   =  '" + strUserXM + "' " + vbCr                            '������
                strSQL = strSQL + " and   �������� in (" + Me.FlowData.TaskBlzlSPSYList + ")" + vbCr          '��������
                If blnZTXZ = True Then
                    strSQL = strSQL + " and   ����״̬ not in (" + Me.FlowData.TaskStatusYWCList + ")" + vbCr 'δ����
                End If
                strSQL = strSQL + " and   rtrim(���ӱ�ʶ) like '1_1__0_%' " + vbCr                                   '�ѷ���+�������ܿ�+��֪ͨ��
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                Dim intXH As Integer
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    intXH = 0
                Else
                    With objDataSet.Tables(0).Rows(0)
                        intXH = objPulicParameters.getObjectValue(.Item("�������"), 0)
                    End With
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '�������ݼ�
                objTempJiaoJieData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_JIAOJIE)
                If strWJBS = "" Then Exit Try

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                'ִ�м���
                With Me.SqlDataAdapter
                    '���㽻����Ϣ
                    strSQL = ""
                    strSQL = strSQL + " select * from ����_B_���� " + vbCr
                    strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "' " + vbCr
                    strSQL = strSQL + " and   ������   = '" + strUserXM + "' " + vbCr
                    strSQL = strSQL + " and   ������� = " + intXH.ToString() + vbCr

                    '���ò���
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    'ִ�в���
                    .Fill(objTempJiaoJieData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_JIAOJIE))
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            objJiaoJieData = objTempJiaoJieData
            getLastSpsyJiaojieData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.Common.Data.FlowData.SafeRelease(objTempJiaoJieData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���������������
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     objSqlTransaction      ����������
        '     intJJXH                ���������
        '     objNewData             ����¼��ֵ(���ر�������ֵ)
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doSaveSpyj( _
            ByRef strErrMsg As String, _
            ByVal objSqlTransaction As System.Data.SqlClient.SqlTransaction, _
            ByVal intJJXH As Integer, _
            ByRef objNewData As System.Collections.Specialized.NameValueCollection) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim objDataSet As System.Data.DataSet

            Dim blnNewTrans As Boolean = False
            Dim strWJBS As String
            Dim strSQL As String

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon

            '��ʼ��
            doSaveSpyj = False
            strErrMsg = ""

            Try
                '���
                If Me.IsInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "����δ�����µ����ݣ�"
                    GoTo errProc
                End If

                '��ȡ������Ϣ
                strWJBS = Me.FlowData.WJBS
                If objSqlTransaction Is Nothing Then
                    objSqlConnection = Me.SqlConnection
                Else
                    objSqlConnection = objSqlTransaction.Connection
                End If

                '�����Ƿ���ڣ�
                strSQL = ""
                strSQL = strSQL + " select * from ����_B_����" + vbCr
                strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "'" + vbCr
                strSQL = strSQL + " and   ������� =  " + intJJXH.ToString + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                Dim blnHas As Boolean
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    blnHas = True
                Else
                    blnHas = False
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '��ʼ����
                If objSqlTransaction Is Nothing Then
                    blnNewTrans = True
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Else
                    blnNewTrans = False
                End If

                '��������
                Dim strFields As String
                Dim strValues As String
                Dim strField As String
                Dim strValue As String
                Dim intCount As Integer
                Dim i As Integer
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    objSqlCommand.Parameters.Clear()
                    If blnHas = False Then
                        '����
                        intCount = objNewData.Count
                        strFields = ""
                        strValues = ""
                        For i = 0 To intCount - 1 Step 1
                            strField = objNewData.GetKey(i)
                            strValue = objNewData(i)

                            If strFields = "" Then
                                strFields = strField
                                strValues = "@A" + i.ToString
                            Else
                                strFields = strFields + "," + vbCr + strField
                                strValues = strValues + "," + vbCr + "@A" + i.ToString
                            End If

                            Select Case strField

                                Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_BANLI_JJXH, _
                                    Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_BANLI_XSXH


                                    objSqlCommand.Parameters.AddWithValue("@A" + i.ToString, CType(strValue, Integer))

                                Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_BANLI_BLRQ, _
                                    Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_BANLI_DLRQ, _
                                    Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_BANLI_TXRQ
                                    If strValue = "" Then
                                        objSqlCommand.Parameters.AddWithValue("@A" + i.ToString, System.DBNull.Value)
                                    Else
                                        objSqlCommand.Parameters.AddWithValue("@A" + i.ToString, CType(strValue, System.DateTime))
                                    End If

                                Case Else
                                    objSqlCommand.Parameters.AddWithValue("@A" + i.ToString, strValue)
                            End Select
                        Next

                        strSQL = " insert into ����_B_���� (" + vbCr + strFields + vbCr + ") values (" + vbCr + strValues + ")" + vbCr
                    Else
                        '����
                        intCount = objNewData.Count
                        strFields = ""
                        strValues = ""
                        For i = 0 To intCount - 1 Step 1
                            strField = objNewData.GetKey(i)
                            strValue = objNewData(i)

                            If strFields = "" Then
                                strFields = strField + " = @A" + i.ToString
                            Else
                                strFields = strFields + "," + vbCr + strField + " = @A" + i.ToString
                            End If

                            Select Case strField
                                Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_BANLI_JJXH, _
                                    Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_BANLI_XSXH

                                    objSqlCommand.Parameters.AddWithValue("@A" + i.ToString, CType(strValue, Integer))

                                Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_BANLI_BLRQ, _
                                    Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_BANLI_DLRQ, _
                                    Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_BANLI_TXRQ
                                    If strValue = "" Then
                                        objSqlCommand.Parameters.AddWithValue("@A" + i.ToString, System.DBNull.Value)
                                    Else
                                        objSqlCommand.Parameters.AddWithValue("@A" + i.ToString, CType(strValue, System.DateTime))
                                    End If

                                Case Else
                                    objSqlCommand.Parameters.AddWithValue("@A" + i.ToString, strValue)
                            End Select
                        Next
                        objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                        objSqlCommand.Parameters.AddWithValue("@jjxh", intJJXH)

                        strSQL = ""
                        strSQL = strSQL + " update ����_B_���� set " + vbCr + strFields + vbCr
                        strSQL = strSQL + " where �ļ���ʶ = @wjbs" + vbCr
                        strSQL = strSQL + " and   ������� = @jjxh" + vbCr
                    End If
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo rollDatabase
                End Try

                '�ύ����
                If blnNewTrans = True Then
                    objSqlTransaction.Commit()
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            doSaveSpyj = True
            Exit Function

rollDatabase:
            If blnNewTrans = True Then
                objSqlTransaction.Rollback()
            End If
            GoTo errProc

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ȡ��intJJXHָ���İ������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objSqlTransaction    ����������
        '     intJJXH              ���������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doBanliCancel( _
            ByRef strErrMsg As String, _
            ByVal objSqlTransaction As System.Data.SqlClient.SqlTransaction, _
            ByVal intJJXH As Integer) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            Dim blnNewTrans As Boolean = False
            Dim strWJBS As String
            Dim strSQL As String

            '��ʼ��
            doBanliCancel = False
            strErrMsg = ""

            Try
                '���
                If Me.IsInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If

                '��ȡ������Ϣ
                If objSqlTransaction Is Nothing Then
                    objSqlConnection = Me.SqlConnection
                Else
                    objSqlConnection = objSqlTransaction.Connection
                End If
                strWJBS = Me.FlowData.WJBS
                If strWJBS = "" Then
                    Exit Try
                End If

                '��ʼ����
                If objSqlTransaction Is Nothing Then
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                    blnNewTrans = True
                Else
                    blnNewTrans = False
                End If

                'ȡ������
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    strSQL = ""
                    strSQL = strSQL + " delete from ����_B_����" + vbCr
                    strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + " and   ������� =  " + intJJXH.ToString + " " + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo rollDatabase
                End Try

                '�ύ����
                If blnNewTrans = True Then
                    objSqlTransaction.Commit()
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            '����
            doBanliCancel = True
            Exit Function

rollDatabase:
            If blnNewTrans = True Then
                objSqlTransaction.Rollback()
            End If
            GoTo errProc

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ�ļ�ָ��intJJXH�İ������ݼ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     intJJXH              ���������
        '     objBanliData         ����������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getBanliData( _
            ByRef strErrMsg As String, _
            ByVal intJJXH As Integer, _
            ByRef objBanliData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim objTempBanliData As Xydc.Platform.Common.Data.FlowData
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim strSQL As String

            getBanliData = False
            objBanliData = Nothing
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '�������ݼ�
                objTempBanliData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_BANLI)
                If strWJBS = "" Then Exit Try

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                'ִ�м���
                With Me.m_objSqlDataAdapter
                    strSQL = ""
                    strSQL = strSQL + " select * from ����_B_����" + vbCr
                    strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + " and   ������� =  " + intJJXH.ToString + " " + vbCr

                    '���ò���
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    'ִ�в���
                    .Fill(objTempBanliData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_BANLI))
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            objBanliData = objTempBanliData
            getBanliData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.FlowData.SafeRelease(objTempBanliData)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �ж�ָ����Ա�����������Ƿ�ȫ��������ϣ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ����Ա����
        '     blnComplete          �����أ��Ƿ���ϣ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function isAllTaskComplete( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByRef blnComplete As Boolean) As Boolean

            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            isAllTaskComplete = False
            strErrMsg = ""
            blnComplete = True

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '����
                strSQL = ""
                strSQL = strSQL + " select * from ����_B_����" + vbCr
                strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "'" + vbCr                            '��ǰ�ļ�
                strSQL = strSQL + " and   ������   = '" + strUserXM + "'" + vbCr                          '������
                strSQL = strSQL + " and   �������� in (" + Me.FlowData.TaskBlzlSPSYList + ")" + vbCr      '��������
                strSQL = strSQL + " and   ����״̬ not in (" + Me.FlowData.TaskStatusYWCList + ")" + vbCr 'δ����
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    blnComplete = False
                Else
                    blnComplete = True
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            isAllTaskComplete = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

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
        Public Overridable Function doExportToExcel( _
            ByRef strErrMsg As String, _
            ByVal objDataSet As System.Data.DataSet, _
            ByVal strExcelFile As String, _
            Optional ByVal strMacroName As String = "", _
            Optional ByVal strMacroValue As String = "") As Boolean

            doExportToExcel = False
            strErrMsg = ""

            Try
                With New Xydc.Platform.DataAccess.dacExcel
                    If .doExport(strErrMsg, objDataSet, strExcelFile, strMacroName, strMacroValue) = False Then
                        GoTo errProc
                    End If
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doExportToExcel = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ���͸�strUserXM����������Ľ��ӵ��еķ������б�(�����Ƿ���꣡)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ���û�����
        '     strSenderList        �����ط������б�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getSenderList( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByRef strSenderList As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            getSenderList = False
            strSenderList = ""
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '�������������ţ�׼�򣺽����˿ɿ������ӡ����͡�������Ϣ
                strSQL = ""
                strSQL = strSQL + " select ������ " + vbCr
                strSQL = strSQL + " from ����_B_���� " + vbCr
                strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "'" + vbCr           '��ǰ�ļ�
                strSQL = strSQL + " and   ������   = '" + strUserXM + "'" + vbCr         '������
                strSQL = strSQL + " and   rtrim(���ӱ�ʶ) like '1_1__0_%' " + vbCr              '�������ܿ�+��֪ͨ��
                strSQL = strSQL + " and   ������  <> '" + strUserXM + "'" + vbCr         'ȥ���Լ�
                strSQL = strSQL + " group by ������" + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                Dim strSep As String = objPulicParameters.CharSeparate
                Dim intCount As Integer
                Dim strFSR As String
                Dim i As Integer
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    With objDataSet.Tables(0)
                        intCount = .Rows.Count
                        For i = 0 To intCount - 1 Step 1
                            strFSR = objPulicParameters.getObjectValue(.Rows(i).Item("������"), "")
                            If strFSR <> "" Then
                                If strSenderList = "" Then
                                    strSenderList = strFSR
                                Else
                                    strSenderList = strSenderList + strSep + strFSR
                                End If
                            End If
                        Next
                    End With
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getSenderList = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function







        '----------------------------------------------------------------
        ' д�û����������־
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strAddress           ��������ַ
        '     strMachine           ����������
        '     strCZMS              ����������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        ' ����˵����
        '      ����strMachine��������ش���
        '----------------------------------------------------------------
        Public Overridable Function doWriteUserLog( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strAddress As String, _
            ByVal strMachine As String, _
            ByVal strCZMS As String) As Boolean

            doWriteUserLog = False
            strErrMsg = ""

            Try
                '���
                If Xydc.Platform.SystemFramework.ApplicationConfiguration.TracingEnabled = False Then
                    Exit Try
                End If
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    Exit Try
                End If
                If strAddress Is Nothing Then strAddress = ""
                strAddress = strAddress.Trim
                If strAddress = "" Then
                    Exit Try
                End If

                If strMachine Is Nothing Then strMachine = ""
                strMachine = strMachine.Trim
                If strMachine = "" Then
                    Exit Try
                End If

                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim
                If strPassword = "" Then
                    Exit Try
                End If
                If strCZMS Is Nothing Then strCZMS = ""
                strCZMS = strCZMS.Trim

                'д�����־
                With New Xydc.Platform.DataAccess.dacCustomer
                    doWriteUserLog = .doWriteYonghuCaozuoRizhi(strErrMsg, strUserId, strPassword, strAddress, strMachine, strCZMS)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doWriteUserLog = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' д���������������־
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strAddress           ��������ַ
        '     strMachine           ����������
        '     objNewFJData         ��������������
        '     objOldFJData         ������ԭ������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        ' ����˵����
        '      ����strMachine��������ش���
        '----------------------------------------------------------------
        Public Overridable Function doWriteUserLog_Fujian( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strAddress As String, _
            ByVal strMachine As String, _
            ByVal objNewFJData As Xydc.Platform.Common.Data.FlowData, _
            ByVal objOldFJData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_FUJIAN
            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCustomer As New Xydc.Platform.DataAccess.dacCustomer

            doWriteUserLog_Fujian = False
            strErrMsg = ""

            Try
                '���
                If Xydc.Platform.SystemFramework.ApplicationConfiguration.TracingEnabled = False Then
                    Exit Try
                End If
                If objNewFJData Is Nothing Then
                    Exit Try
                End If
                If objOldFJData Is Nothing Then
                    Exit Try
                End If
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    Exit Try
                End If
                If strAddress Is Nothing Then strAddress = ""
                strAddress = strAddress.Trim
                If strAddress = "" Then
                    Exit Try
                End If

                If strMachine Is Nothing Then strMachine = ""
                strMachine = strMachine.Trim
                If strMachine = "" Then
                    Exit Try
                End If

                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim
                If strPassword = "" Then
                    Exit Try
                End If
                If Me.IsInitialized = False Then
                    Exit Try
                End If
                If Me.FlowData.WJBS = "" Then
                    Exit Try
                End If

                '����Ƚϣ�����Ƿ�ɾ����
                Dim strOldFilter As String
                Dim intCountA As Integer
                Dim strCZMS As String
                Dim strXH As String
                Dim i As Integer
                With objOldFJData.Tables(strTable)
                    intCountA = .Rows.Count
                    For i = 0 To intCountA - 1 Step 1
                        strXH = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_WJXH), "")
                        With objNewFJData.Tables(strTable)
                            '����RowFilter
                            strOldFilter = .DefaultView.RowFilter
                            '����Ƿ���ڣ�
                            .DefaultView.RowFilter = Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_WJXH + " = " + strXH
                            If .DefaultView.Count < 1 Then
                                '��ɾ����
                                strCZMS = "ɾ����[" + Me.FlowData.WJBS + "]�ļ��ĵ�[" + strXH + "]��������"
                                If objdacCustomer.doWriteYonghuCaozuoRizhi(strErrMsg, strUserId, strPassword, strAddress, strMachine, strCZMS) Then
                                    '����
                                End If
                            End If
                            '�ָ�RowFilter
                            .DefaultView.RowFilter = strOldFilter
                        End With
                    Next
                End With

                '����¸���
                Dim strBDWJ As String
                Dim strWJWZ As String
                With objNewFJData.Tables(strTable).DefaultView
                    intCountA = .Count
                    For i = 0 To intCountA - 1 Step 1
                        strXH = objPulicParameters.getObjectValue(.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_WJXH), "")
                        strBDWJ = objPulicParameters.getObjectValue(.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_BDWJ), "")
                        strWJWZ = objPulicParameters.getObjectValue(.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_WJWZ), "")
                        If strXH = "" Then
                            '�����ӣ�
                            strCZMS = "�����[" + Me.FlowData.WJBS + "]�ļ��ĵ�[" + (i + 1).ToString + "]��������"
                            If objdacCustomer.doWriteYonghuCaozuoRizhi(strErrMsg, strUserId, strPassword, strAddress, strMachine, strCZMS) Then
                                '����
                            End If
                        Else
                            If strBDWJ <> "" Then
                                '�����˸����ļ�
                                strCZMS = "�����ϴ���[" + Me.FlowData.WJBS + "]�ļ��ĵ�[" + (i + 1).ToString + "]��������"
                                If objdacCustomer.doWriteYonghuCaozuoRizhi(strErrMsg, strUserId, strPassword, strAddress, strMachine, strCZMS) Then
                                    '����
                                End If
                            End If
                            If CType(strXH, Integer) <> (i + 1) Then
                                '�����˸���λ��
                                strCZMS = "��[" + Me.FlowData.WJBS + "]�ļ��ĵ�[" + strXH + "]��������������[" + (i + 1).ToString + "]��������"
                                If objdacCustomer.doWriteYonghuCaozuoRizhi(strErrMsg, strUserId, strPassword, strAddress, strMachine, strCZMS) Then
                                    '����
                                End If
                            End If
                        End If
                    Next
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)

            doWriteUserLog_Fujian = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' д����ļ������������־
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strAddress           ��������ַ
        '     strMachine           ����������
        '     objNewXGWJData       ������ļ���������
        '     objOldXGWJData       ������ļ�ԭ������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        ' ����˵����
        '      ����strMachine��������ش���
        '----------------------------------------------------------------
        Public Overridable Function doWriteUserLog_XGWJ( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strAddress As String, _
            ByVal strMachine As String, _
            ByVal objNewXGWJData As Xydc.Platform.Common.Data.FlowData, _
            ByVal objOldXGWJData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim strTable As String = Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_SHENPIWENJIAN_FUJIAN
            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCustomer As New Xydc.Platform.DataAccess.dacCustomer

            doWriteUserLog_XGWJ = False
            strErrMsg = ""

            Try
                '���
                If Xydc.Platform.SystemFramework.ApplicationConfiguration.TracingEnabled = False Then
                    Exit Try
                End If
                If objNewXGWJData Is Nothing Then
                    Exit Try
                End If
                If objOldXGWJData Is Nothing Then
                    Exit Try
                End If
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    Exit Try
                End If
                If strAddress Is Nothing Then strAddress = ""
                strAddress = strAddress.Trim
                If strAddress = "" Then
                    Exit Try
                End If

                If strMachine Is Nothing Then strMachine = ""
                strMachine = strMachine.Trim
                If strMachine = "" Then
                    Exit Try
                End If

                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim
                If strPassword = "" Then
                    Exit Try
                End If
                If Me.IsInitialized = False Then
                    Exit Try
                End If
                If Me.FlowData.WJBS = "" Then
                    Exit Try
                End If

                '����Ƚϣ�����Ƿ�ɾ����
                Dim strOldFilter As String
                Dim strNewFilter As String
                Dim intCountA As Integer
                Dim strCZMS As String
                Dim intLBBS As Integer
                Dim strXH As String
                Dim i As Integer
                With objOldXGWJData.Tables(strTable)
                    intCountA = .Rows.Count
                    For i = 0 To intCountA - 1 Step 1
                        intLBBS = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_LBBS), 0)
                        Select Case intLBBS
                            Case Xydc.Platform.Common.Data.FlowData.enumXGWJLB.FlowFile
                                strXH = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_FJXH), "")
                                With objNewXGWJData.Tables(strTable)
                                    '����RowFilter
                                    strOldFilter = .DefaultView.RowFilter
                                    '����Ƿ���ڣ�
                                    strNewFilter = Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_FJXH + " = " + strXH
                                    strNewFilter = strNewFilter + " and " + Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_LBBS + " = 0"
                                    .DefaultView.RowFilter = strNewFilter
                                    If .DefaultView.Count < 1 Then
                                        '��ɾ����
                                        strCZMS = "ɾ����[" + Me.FlowData.WJBS + "]�ļ��ĵ�[" + strXH + "]������ļ���"
                                        If objdacCustomer.doWriteYonghuCaozuoRizhi(strErrMsg, strUserId, strPassword, strAddress, strMachine, strCZMS) Then
                                            '����
                                        End If
                                    End If
                                    '�ָ�RowFilter
                                    .DefaultView.RowFilter = strOldFilter
                                End With

                            Case Xydc.Platform.Common.Data.FlowData.enumXGWJLB.FujianFile
                                strXH = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_FJXH), "")
                                With objNewXGWJData.Tables(strTable)
                                    '����RowFilter
                                    strOldFilter = .DefaultView.RowFilter
                                    '����Ƿ���ڣ�
                                    strNewFilter = Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_FJXH + " = " + strXH
                                    strNewFilter = strNewFilter + " and " + Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_LBBS + " = 1"
                                    .DefaultView.RowFilter = strNewFilter
                                    If .DefaultView.Count < 1 Then
                                        '��ɾ����
                                        strCZMS = "ɾ����[" + Me.FlowData.WJBS + "]�ļ��ĵ�[" + strXH + "]������ļ���"
                                        If objdacCustomer.doWriteYonghuCaozuoRizhi(strErrMsg, strUserId, strPassword, strAddress, strMachine, strCZMS) Then
                                            '����
                                        End If
                                    End If
                                    '�ָ�RowFilter
                                    .DefaultView.RowFilter = strOldFilter
                                End With
                        End Select
                    Next
                End With

                '���������ļ�
                Dim strBDWJ As String
                Dim strWJWZ As String
                With objNewXGWJData.Tables(strTable).DefaultView
                    intCountA = .Count
                    For i = 0 To intCountA - 1 Step 1
                        intLBBS = objPulicParameters.getObjectValue(.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_LBBS), 0)
                        Select Case intLBBS
                            Case Xydc.Platform.Common.Data.FlowData.enumXGWJLB.FlowFile
                                strXH = objPulicParameters.getObjectValue(.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_FJXH), "")
                                strBDWJ = objPulicParameters.getObjectValue(.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_WJBS), "")
                                If strXH = "" Then
                                    '�����ӣ�
                                    strCZMS = "�����[" + Me.FlowData.WJBS + "]�ļ��ĵ�[" + (i + 1).ToString + "]������ļ����ļ���ʶΪ[" + strBDWJ + "]��"
                                    If objdacCustomer.doWriteYonghuCaozuoRizhi(strErrMsg, strUserId, strPassword, strAddress, strMachine, strCZMS) Then
                                        '����
                                    End If
                                Else
                                    If CType(strXH, Integer) <> (i + 1) Then
                                        '�����˸���λ��
                                        strCZMS = "��[" + Me.FlowData.WJBS + "]�ļ��ĵ�[" + strXH + "]������ļ���������[" + (i + 1).ToString + "]������ļ���"
                                        If objdacCustomer.doWriteYonghuCaozuoRizhi(strErrMsg, strUserId, strPassword, strAddress, strMachine, strCZMS) Then
                                            '����
                                        End If
                                    End If
                                End If
                            Case Xydc.Platform.Common.Data.FlowData.enumXGWJLB.FujianFile
                                strXH = objPulicParameters.getObjectValue(.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_FJXH), "")
                                strBDWJ = objPulicParameters.getObjectValue(.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_BDWJ), "")
                                strWJWZ = objPulicParameters.getObjectValue(.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_FJWZ), "")
                                If strXH = "" Then
                                    '�����ӣ�
                                    strCZMS = "�����[" + Me.FlowData.WJBS + "]�ļ��ĵ�[" + (i + 1).ToString + "]������ļ���"
                                    If objdacCustomer.doWriteYonghuCaozuoRizhi(strErrMsg, strUserId, strPassword, strAddress, strMachine, strCZMS) Then
                                        '����
                                    End If
                                Else
                                    If strBDWJ <> "" Then
                                        '�����˸����ļ�
                                        strCZMS = "�����ϴ���[" + Me.FlowData.WJBS + "]�ļ��ĵ�[" + (i + 1).ToString + "]������ļ���"
                                        If objdacCustomer.doWriteYonghuCaozuoRizhi(strErrMsg, strUserId, strPassword, strAddress, strMachine, strCZMS) Then
                                            '����
                                        End If
                                    End If
                                    If CType(strXH, Integer) <> (i + 1) Then
                                        '�����˸���λ��
                                        strCZMS = "��[" + Me.FlowData.WJBS + "]�ļ��ĵ�[" + strXH + "]������ļ���������[" + (i + 1).ToString + "]������ļ���"
                                        If objdacCustomer.doWriteYonghuCaozuoRizhi(strErrMsg, strUserId, strPassword, strAddress, strMachine, strCZMS) Then
                                            '����
                                        End If
                                    End If
                                End If
                        End Select
                    Next
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)

            doWriteUserLog_XGWJ = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡָ�����ӵġ�Э�족��־
        '     strErrMsg             ����������򷵻ش�����Ϣ
        '     intJJXH               ���������
        '     strWTR                �����أ���Э�족��־
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getJiaojie_XBBZ( _
            ByRef strErrMsg As String, _
            ByVal intJJXH As Integer, _
            ByRef strXBBZ As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet
            Dim strSQL As String

            getJiaojie_XBBZ = False
            strErrMsg = ""
            strXBBZ = Xydc.Platform.Common.Utilities.PulicParameters.CharFalse

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If

                '��ȡ�ļ���ʶ
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then
                    Exit Try
                End If

                '��ȡstrUseXMδ������ķ�֪ͨ�����е�ί������Ϣ
                strSQL = ""
                strSQL = strSQL + " select Э�� from ����_B_���� " + vbCr
                strSQL = strSQL + " where �ļ���ʶ = '" + strWJBS + "' " + vbCr
                strSQL = strSQL + " and   ������� =  " + intJJXH.ToString + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    Exit Try
                End If

                '����
                strXBBZ = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item(0), Xydc.Platform.Common.Utilities.PulicParameters.CharFalse)

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getJiaojie_XBBZ = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ����Э���־����(����_B_����)
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     objSqlTransaction      ����������
        '     strUserXM              ����Ա����
        '     strNewXBBZ             ��Э���־
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doSetJiaojieXBBZ( _
            ByRef strErrMsg As String, _
            ByVal objSqlTransaction As System.Data.SqlClient.SqlTransaction, _
            ByVal strUserXM As String, _
            ByVal strNewXBBZ As String) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand
            Dim blnNewTrans As Boolean = False
            Dim strWJBS As String
            Dim strSQL As String

            '��ʼ��
            doSetJiaojieXBBZ = False
            strErrMsg = ""

            Try
                '���
                If Me.IsInitialized = False Then
                    strErrMsg = "����[doSetJiaojieXBBZ]����û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim
                If strUserXM = "" Then
                    strErrMsg = "����[isXBBZConflict]û������[�û�����]��"
                    GoTo errProc
                End If
                If strNewXBBZ Is Nothing Then strNewXBBZ = ""
                strNewXBBZ = strNewXBBZ.Trim
                If strNewXBBZ = "" Then strNewXBBZ = Xydc.Platform.Common.Utilities.PulicParameters.CharFalse

                '��ȡ������Ϣ
                If objSqlTransaction Is Nothing Then
                    objSqlConnection = Me.SqlConnection
                Else
                    objSqlConnection = objSqlTransaction.Connection
                End If
                strWJBS = Me.WJBS

                '��ʼ����
                If objSqlTransaction Is Nothing Then
                    blnNewTrans = True
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Else
                    blnNewTrans = False
                End If

                '��������
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    strSQL = ""
                    strSQL = strSQL + " update ����_B_���� set " + vbCr
                    strSQL = strSQL + "   Э�� = @xbbz" + vbCr
                    strSQL = strSQL + " where �ļ���ʶ = @wjbs" + vbCr
                    strSQL = strSQL + " and   ������   = @jsr" + vbCr
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@xbbz", strNewXBBZ)
                    objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                    objSqlCommand.Parameters.AddWithValue("@jsr", strUserXM)
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo rollDatabase
                End Try

                '�ύ����
                If blnNewTrans = True Then
                    objSqlTransaction.Commit()
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            '����
            doSetJiaojieXBBZ = True
            Exit Function

rollDatabase:
            If blnNewTrans = True Then
                objSqlTransaction.Rollback()
            End If
            GoTo errProc

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���ա���ʾ��š������������𡱡�����֯���롱������Ա��š���
        '���������� desc��������ԭ���򡰹���_B_����д�롰��ʾ��š�����
        '     strErrMsg            �����ش�����Ϣ
        ' ����
        '     True                 ���ɹ�
        '     False                �����ɹ�
        ' ��ע:
        '     ����
        '----------------------------------------------------------------
        Public Overridable Function doWriteXSXH(ByRef strErrMsg As String) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objOpinionData As System.Data.DataSet
            Dim strWJBS As String
            Dim strSQL As String

            '��ʼ��
            doWriteXSXH = False
            strErrMsg = ""

            Try
                '���
                If Me.IsInitialized = False Then
                    strErrMsg = "����[doWriteXSXH]����û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If

                '��ȡ������Ϣ
                objSqlConnection = Me.SqlConnection
                strWJBS = Me.WJBS
                If strWJBS Is Nothing Then strWJBS = ""
                strWJBS = strWJBS.Trim
                If strWJBS = "" Then
                    '���ô���
                    Exit Try
                End If

                '��ȡ���ļ���ȫ���������
                strSQL = ""
                strSQL = strSQL + " select a.*" + vbCr
                strSQL = strSQL + " from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select a.*,b.��������,b.��֯����,b.��Ա���" + vbCr
                strSQL = strSQL + "   from" + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select * from ����_B_����" + vbCr
                strSQL = strSQL + "     where �ļ���ʶ = '" + strWJBS + "'" + vbCr
                strSQL = strSQL + "   ) a" + vbCr
                strSQL = strSQL + "   left join" + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select a.*,b.��������,b.��������" + vbCr
                strSQL = strSQL + "     from ����_B_��Ա a" + vbCr
                strSQL = strSQL + "     left join ����_B_�������� b on a.������� = b.�������" + vbCr
                strSQL = strSQL + "   ) b on a.������ = b.��Ա����" + vbCr
                strSQL = strSQL + " ) a" + vbCr
                strSQL = strSQL + " order by a.��ʾ���,a.��������,a.��֯����,a.��Ա���,a.�������� desc"
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objOpinionData) = False Then
                    GoTo errProc
                End If

                '��ʼ����
                objSqlTransaction = objSqlConnection.BeginTransaction()

                '��������
                Dim intCount, i, intJJXH As Integer
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '��˳��д�롰��ʾ��š�
                    intCount = objOpinionData.Tables(0).Rows.Count
                    For i = 0 To intCount - 1 Step 1
                        '��ȡ����
                        strWJBS = objPulicParameters.getObjectValue(objOpinionData.Tables(0).Rows(i).Item("�ļ���ʶ"), "")
                        intJJXH = objPulicParameters.getObjectValue(objOpinionData.Tables(0).Rows(i).Item("�������"), 0)

                        '׼��SQL
                        strSQL = ""
                        strSQL = strSQL + " update ����_B_���� set" + vbCr
                        strSQL = strSQL + "   ��ʾ��� = @xsxh" + vbCr
                        strSQL = strSQL + " where �ļ���ʶ = @wjbs" + vbCr
                        strSQL = strSQL + " and   ������� = @jjxh" + vbCr
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@xsxh", i + 1)
                        objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                        objSqlCommand.Parameters.AddWithValue("@jjxh", intJJXH)
                        '�ύִ��
                        objSqlCommand.ExecuteNonQuery()
                    Next

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo rollDatabase
                End Try

                '�ύ����
                objSqlTransaction.Commit()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objOpinionData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            doWriteXSXH = True
            Exit Function

rollDatabase:
            objSqlTransaction.Rollback()
            GoTo errProc

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objOpinionData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ�ƽ���strYJR���ƽ��Ĺ������ļ�������ļ��Ѿ��ƽ���strJSR����ͬʱ��ȡ�ƽ���Ϣ
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û�ID
        '     strPassword          ���û�����
        '     strYJR               ���ƽ���(����)
        '     strJSR               ��������(����)
        '     strWhere             ����������
        '     objYijiaoData        ����������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        ' �޸ļ�¼
        '      ����
        '----------------------------------------------------------------
        Public Shared Function getYijiaoData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strYJR As String, _
            ByVal strJSR As String, _
            ByVal strWhere As String, _
            ByRef objYijiaoData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Nothing
            Dim objTempYijiaoData As Xydc.Platform.Common.Data.FlowData = Nothing
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand = Nothing
            Dim objSqlDataAdapter As New System.Data.SqlClient.SqlDataAdapter
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim strSQL As String = ""

            getYijiaoData = False
            objYijiaoData = Nothing
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "����[getYijiaoData]û��ָ��[�û�ID]��"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim
                If strYJR Is Nothing Then strYJR = ""
                strYJR = strYJR.Trim
                If strJSR Is Nothing Then strJSR = ""
                strJSR = strJSR.Trim
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '�������ݼ�
                objTempYijiaoData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_V_YIJIAOWENJIAN)
                If strYJR = "" Then Exit Try
                If strJSR = "" Then Exit Try

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                'ִ�м���
                With objSqlDataAdapter
                    '����
                    strSQL = ""
                    strSQL = strSQL + " select a.*" + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select a.*," + vbCr
                    strSQL = strSQL + "     b.�ƽ���,b.������,b.�ƽ�����,b.�ƽ�˵��,b.��������," + vbCr
                    strSQL = strSQL + "     �Ƿ��ƽ� = case when b.�ļ���ʶ is null then @false else @true end," + vbCr
                    strSQL = strSQL + "     �Ƿ���� = case when b.�������� is null then @false else @true end " + vbCr
                    strSQL = strSQL + "   from" + vbCr
                    strSQL = strSQL + "   (" + vbCr
                    strSQL = strSQL + "     select a.* from ����_V_ȫ�������ļ��� a" + vbCr
                    strSQL = strSQL + "     left join" + vbCr
                    strSQL = strSQL + "     (" + vbCr
                    strSQL = strSQL + "       select �ļ���ʶ" + vbCr
                    strSQL = strSQL + "       from ����_B_����" + vbCr
                    strSQL = strSQL + "       where ((������=@yjr and ���ӱ�ʶ like '__1%') or (������=@yjr and ���ӱ�ʶ like '_1%'))"
                    strSQL = strSQL + "       group by �ļ���ʶ"
                    strSQL = strSQL + "     ) b on a.�ļ���ʶ = b.�ļ���ʶ" + vbCr
                    strSQL = strSQL + "     where b.�ļ���ʶ is not null" + vbCr

                    'strSQL = strSQL + "     and a.����״̬ ='�������'" + vbCr
                    strSQL = strSQL + "   and a.����״̬ = '" + Xydc.Platform.Common.Workflow.BaseFlowObject.FILESTATUS_YWC + "'" + vbCr

                    strSQL = strSQL + "   ) a" + vbCr
                    strSQL = strSQL + "   left join" + vbCr
                    strSQL = strSQL + "   (" + vbCr
                    strSQL = strSQL + "     select *" + vbCr
                    strSQL = strSQL + "     from ����_B_�ƽ�" + vbCr
                    strSQL = strSQL + "     where �ƽ���=@yjr" + vbCr
                    strSQL = strSQL + "     and   ������=@jsr" + vbCr
                    strSQL = strSQL + "   ) b on a.�ļ���ʶ = b.�ļ���ʶ" + vbCr
                    strSQL = strSQL + " ) a" + vbCr
                    If strWhere <> "" Then
                        strSQL = strSQL + " where " + strWhere + vbCr
                    End If
                    strSQL = strSQL + " order by a.�ļ���� desc,a.�ļ�����,a.�ļ��ֺ�" + vbCr

                    '���ò���
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@false", Xydc.Platform.Common.Utilities.PulicParameters.CharFalse)
                    objSqlCommand.Parameters.AddWithValue("@true", Xydc.Platform.Common.Utilities.PulicParameters.CharTrue)
                    objSqlCommand.Parameters.AddWithValue("@yjr", strYJR)
                    objSqlCommand.Parameters.AddWithValue("@jsr", strJSR)
                    .SelectCommand = objSqlCommand

                    'ִ�в���
                    .Fill(objTempYijiaoData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_V_YIJIAOWENJIAN))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlDataAdapter)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            objYijiaoData = objTempYijiaoData
            getYijiaoData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlDataAdapter)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.FlowData.SafeRelease(objTempYijiaoData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ�ƽ���strYJR�ƽ���strJSR�Ĺ������ļ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û�ID
        '     strPassword          ���û�����
        '     strYJR               ���ƽ���(����)
        '     strJSR               ��������(����)
        '     strWhere             ����������
        '     objJieshouData       ����������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        ' �޸ļ�¼
        '      ����
        '----------------------------------------------------------------
        Public Shared Function getJieshouData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strYJR As String, _
            ByVal strJSR As String, _
            ByVal strWhere As String, _
            ByRef objJieshouData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Nothing
            Dim objTempJieshouData As Xydc.Platform.Common.Data.FlowData = Nothing
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand = Nothing
            Dim objSqlDataAdapter As New System.Data.SqlClient.SqlDataAdapter
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim strSQL As String = ""

            getJieshouData = False
            objJieshouData = Nothing
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "����[getJieshouData]û��ָ��[�û�ID]��"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim
                If strYJR Is Nothing Then strYJR = ""
                strYJR = strYJR.Trim
                If strJSR Is Nothing Then strJSR = ""
                strJSR = strJSR.Trim
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '�������ݼ�
                objTempJieshouData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_V_YIJIAOWENJIAN)
                If strYJR = "" Then Exit Try
                If strJSR = "" Then Exit Try

                '����SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                'ִ�м���
                With objSqlDataAdapter
                    '����
                    strSQL = ""
                    strSQL = strSQL + " select a.*" + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select b.*," + vbCr
                    strSQL = strSQL + "     a.�ƽ���,a.������,a.�ƽ�����,a.�ƽ�˵��,a.��������," + vbCr
                    strSQL = strSQL + "     �Ƿ��ƽ� = @true," + vbCr
                    strSQL = strSQL + "     �Ƿ���� = case when a.�������� is null then @false else @true end " + vbCr
                    strSQL = strSQL + "   from" + vbCr
                    strSQL = strSQL + "   ("
                    strSQL = strSQL + "     select * from ����_B_�ƽ�" + vbCr
                    strSQL = strSQL + "     where �ƽ��� = @yjr" + vbCr
                    strSQL = strSQL + "     and   ������ = @jsr" + vbCr
                    strSQL = strSQL + "   ) a" + vbCr
                    strSQL = strSQL + "   left join ����_V_ȫ�������ļ��� b on a.�ļ���ʶ = b.�ļ���ʶ" + vbCr
                    strSQL = strSQL + "   where b.�ļ���ʶ is not null" + vbCr
                    strSQL = strSQL + " ) a" + vbCr
                    If strWhere <> "" Then
                        strSQL = strSQL + " where " + strWhere + vbCr
                    End If
                    strSQL = strSQL + " order by a.�ļ���� desc,a.�ļ�����,a.�ļ��ֺ�" + vbCr

                    '���ò���
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@false", Xydc.Platform.Common.Utilities.PulicParameters.CharFalse)
                    objSqlCommand.Parameters.AddWithValue("@true", Xydc.Platform.Common.Utilities.PulicParameters.CharTrue)
                    objSqlCommand.Parameters.AddWithValue("@yjr", strYJR)
                    objSqlCommand.Parameters.AddWithValue("@jsr", strJSR)
                    .SelectCommand = objSqlCommand

                    'ִ�в���
                    .Fill(objTempJieshouData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_V_YIJIAOWENJIAN))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlDataAdapter)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            objJieshouData = objTempJieshouData
            getJieshouData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlDataAdapter)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.Common.Data.FlowData.SafeRelease(objTempJieshouData)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ�ƽ���strJSR���ƽ����б�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û�ID
        '     strPassword          ���û�����
        '     strJSR               ��������(����)
        '     objYjrData           ����������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        ' �޸ļ�¼
        '      ����
        '----------------------------------------------------------------
        Public Shared Function getYjrListData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strJSR As String, _
            ByRef objYjrData As System.Data.DataSet) As Boolean

            Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Nothing
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand = Nothing
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim strSQL As String = ""

            getYjrListData = False
            objYjrData = Nothing
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "����[getYjrListData]û��ָ��[�û�ID]��"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim
                If strJSR Is Nothing Then strJSR = ""
                strJSR = strJSR.Trim

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '����
                strSQL = "select �ƽ��� from ����_B_�ƽ� where ������ = '" + strJSR + "' group by �ƽ��� order by �ƽ���"
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objYjrData) = False Then
                    GoTo errProc
                End If
                If objYjrData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objYjrData)
                    strErrMsg = "����[getYjrListData]�޷���ȡ[�ƽ���]���ݣ�"
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getYjrListData = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' strYJR��strJSR�ƽ��ļ�strWJBS
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û�ID
        '     strPassword          ���û�����
        '     strYJR               ���ƽ���(����)
        '     strJSR               ��������(����)
        '     strWJBS              ��Ҫ�ƽ��Ĺ������ļ���ʶ
        '     strYJSM              ���ƽ�����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        ' �޸ļ�¼
        '      ����
        '----------------------------------------------------------------
        Public Shared Function doFile_Yijiao( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strYJR As String, _
            ByVal strJSR As String, _
            ByVal strWJBS As String, _
            ByVal strYJSM As String) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction = Nothing
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Nothing
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand = Nothing
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim strSQL As String = ""

            '��ʼ��
            doFile_Yijiao = False
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "����[doFile_Yijiao]û��ָ��[�û�ID]��"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim
                If strYJR Is Nothing Then strYJR = ""
                strYJR = strYJR.Trim
                If strJSR Is Nothing Then strJSR = ""
                strJSR = strJSR.Trim
                If strWJBS Is Nothing Then strWJBS = ""
                strWJBS = strWJBS.Trim
                If strYJR = "" Or strJSR = "" Or strWJBS = "" Then
                    strErrMsg = "����[doFile_Yijiao]û��ָ��[�ƽ���]��[������]����[Ҫ�ƽ����ļ�]��"
                    GoTo errProc
                End If
                If strYJSM Is Nothing Then strYJSM = ""
                strYJSM = strYJSM.Trim

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ʼ����
                Try
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '�ƽ�����
                Try

                    Dim strValue() As String
                    strValue = strJSR.Split(Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate.ToCharArray())


                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout


                    Dim intCount As Integer
                    Dim i As Integer
                    intCount = strValue.Length
                    For i = 0 To intCount - 1 Step 1
                        'ִ��SQL
                        strSQL = ""
                        strSQL = strSQL + " insert into ����_B_�ƽ� (" + vbCr
                        strSQL = strSQL + "   �ƽ���,������,�ļ���ʶ,�ƽ�����,�ƽ�˵��,��������" + vbCr
                        strSQL = strSQL + " ) values (" + vbCr
                        strSQL = strSQL + "   @yjr,@jsr,@wjbs,@yjrq,@yjsm,@jsrq"
                        strSQL = strSQL + " )" + vbCr
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@yjr", strYJR)
                        'objSqlCommand.Parameters.AddWithValue("@jsr", strJSR)
                        objSqlCommand.Parameters.AddWithValue("@jsr", strValue(i))
                        objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                        objSqlCommand.Parameters.AddWithValue("@yjrq", Now)
                        objSqlCommand.Parameters.AddWithValue("@yjsm", strYJSM)
                        objSqlCommand.Parameters.AddWithValue("@jsrq", System.DBNull.Value)
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.ExecuteNonQuery()
                    Next i

                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '�ύ����
                objSqlTransaction.Commit()
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            doFile_Yijiao = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ����strWJBS��ȡstrWJLX
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û�ID
        '     strPassword          ���û�����
        '     strWJBS              ���ļ���ʶ
        '     strWJLX              �����ع�������������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        ' �޸ļ�¼
        '      ����
        '----------------------------------------------------------------
        Public Shared Function getWJLX( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWJBS As String, _
            ByRef strWJLX As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Nothing
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim objDataSet As System.Data.DataSet = Nothing
            Dim strSQL As String = ""

            getWJLX = False
            strErrMsg = ""
            strWJLX = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "����[getWJLX]û��ָ��[�û�ID]��"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim
                If strWJBS Is Nothing Then strWJBS = ""
                strWJBS = strWJBS.Trim
                If strWJBS = "" Then
                    Exit Try
                End If

                'ִ��
                strSQL = "select �ļ����� from ����_V_ȫ�������ļ��� where �ļ���ʶ = '" + strWJBS + "'"
                If objdacCommon.getDataSetBySQL(strErrMsg, strUserId, strPassword, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables.Count > 0 Then
                    If objDataSet.Tables(0).Rows.Count > 0 Then
                        strWJLX = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item("�ļ�����"), "")
                    End If
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            getWJLX = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' strJSR����strYJR�ƽ����ļ�strWJBS
        ' ���strJSR���ܿ����ļ�����strYJR�Զ���strJSR���͡����ġ������Զ���ǡ����Ķ���
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û�ID
        '     strPassword          ���û�����
        '     strYJR               ���ƽ���(����)
        '     strJSR               ��������(����)
        '     strWJBS              ��Ҫ�ƽ��Ĺ������ļ���ʶ
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        ' �޸ļ�¼
        '      ����
        '----------------------------------------------------------------
        Public Shared Function doFile_Jieshou( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strYJR As String, _
            ByVal strJSR As String, _
            ByVal strWJBS As String) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction = Nothing
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Nothing
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand = Nothing
            Dim objFlowObject As Xydc.Platform.DataAccess.FlowObject = Nothing
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim strSQL As String = ""

            '��ʼ��
            doFile_Jieshou = False
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "����[doFile_Jieshou]û��ָ��[�û�ID]��"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim
                If strYJR Is Nothing Then strYJR = ""
                strYJR = strYJR.Trim
                If strJSR Is Nothing Then strJSR = ""
                strJSR = strJSR.Trim
                If strWJBS Is Nothing Then strWJBS = ""
                strWJBS = strWJBS.Trim
                If strYJR = "" Or strJSR = "" Or strWJBS = "" Then
                    strErrMsg = "����[doFile_Jieshou]û��ָ��[�ƽ���]��[������]����[Ҫ���յ��ļ�]��"
                    GoTo errProc
                End If

                '��ȡ����������
                Dim strType As String = ""
                Dim strName As String = ""
                If Xydc.Platform.DataAccess.FlowObject.getWJLX(strErrMsg, strUserId, strPassword, strWJBS, strName) = False Then
                    GoTo errProc
                End If
                strType = Xydc.Platform.DataAccess.FlowObject.getFlowType(strName)
                If strType = "" Then
                    strErrMsg = "����[doFile_Jieshou]��֧��ָ���Ĺ�����[" + strName + "]��"
                    GoTo errProc
                End If

                '����������
                objFlowObject = Xydc.Platform.DataAccess.FlowObject.Create(strType, strName)
                If objFlowObject.doInitialize(strErrMsg, strUserId, strPassword, strWJBS, False) = False Then
                    GoTo errProc
                End If
                Dim blnCanRead As Boolean = False
                If objFlowObject.canReadFile(strErrMsg, strJSR, blnCanRead) = False Then
                    GoTo errProc
                End If

                '��ȡ����
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '��ʼ����
                Try
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '���մ���
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    'ִ��SQL
                    strSQL = ""
                    strSQL = strSQL + " update ����_B_�ƽ� set" + vbCr
                    strSQL = strSQL + "   �������� = @jsrq" + vbCr
                    strSQL = strSQL + " where �ƽ���   = @yjr" + vbCr
                    strSQL = strSQL + " and   ������   = @jsr" + vbCr
                    strSQL = strSQL + " and   �ļ���ʶ = @wjbs" + vbCr
                    strSQL = strSQL + " and   �������� is null" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@jsrq", Now)
                    objSqlCommand.Parameters.AddWithValue("@yjr", strYJR)
                    objSqlCommand.Parameters.AddWithValue("@jsr", strJSR)
                    objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    'д�����ġ���
                    If blnCanRead = False Then
                        If objFlowObject.doSendBuyueJJD(strErrMsg, objSqlTransaction, strYJR, strJSR) = False Then
                            objSqlTransaction.Rollback()
                            GoTo errProc
                        End If
                    End If
                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '�ύ����
                objSqlTransaction.Commit()
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.FlowObject.SafeRelease(objFlowObject)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            doFile_Jieshou = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.FlowObject.SafeRelease(objFlowObject)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ����strWJBS,strWJLX�����ļ���Ϣ
        '     strErrMsg            ����������򷵻ش�����Ϣ       
        '     objNewData           : �µ�����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        ' �޸ļ�¼
        '      2008-08-04 ����
        '----------------------------------------------------------------
        Public Overridable Function doUpdateWJXX( _
            ByRef strErrMsg As String, _
            ByVal objNewData As System.Collections.Specialized.NameValueCollection) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction = Nothing
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Nothing
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand = Nothing
            Dim objdacCommon As New Xydc.Platform.DataAccess.dacCommon
            Dim strSQL As String = ""

            '��ʼ��
            doUpdateWJXX = False
            strErrMsg = ""

            Try
                '���
                If Me.m_blnInitialized = False Then
                    strErrMsg = "���󣺶���û�г�ʼ��������ʹ�ã�"
                    GoTo errProc
                End If

                '��ȡ����
                objSqlConnection = Me.SqlConnection

                '��ʼ����
                Try
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '��������
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '����SQL
                    Dim intDefaultValue As Integer = 0
                    Dim strFileds As String = ""
                    Dim strValues As String = ""
                    Dim strField As String = ""
                    Dim intCount As Integer = 0
                    Dim i As Integer = 0

                    '��ȡԭ���ļ���ʶ��
                    Dim strOldWJBS As String
                    Dim strOldWJLX As String
                    strOldWJBS = Me.FlowData.WJBS
                    strOldWJLX = Me.FlowData.FlowTypeBLLX

                    Dim strTable As String = ""
                    Select Case strOldWJLX
                        'Case Xydc.Platform.Common.Workflow.BaseFlowDanganZhuanyi.FLOWBLLX
                        '  strTable = Xydc.Platform.Common.Data.daglDanganData.TABLE_DA_B_ZHUANYISHENGQING

                        Case Else
                    End Select

                    '��������ֶ��б�
                    intCount = objNewData.Count
                    Select Case strOldWJLX
                       
                        Case Else
                            For i = 0 To intCount - 1 Step 1
                                Select Case objNewData.GetKey(i)
                                    Case Else
                                        If strFileds = "" Then
                                            strFileds = objNewData.GetKey(i) + " = @A" + i.ToString()
                                        Else
                                            strFileds = strFileds + "," + objNewData.GetKey(i) + " = @A" + i.ToString()
                                        End If
                                End Select
                            Next
                    End Select

                    '׼��SQL
                    strSQL = ""
                    strSQL = strSQL + " update " + strTable + " set " + vbCr
                    strSQL = strSQL + "   " + strFileds + vbCr
                    strSQL = strSQL + " where �ļ���ʶ = '" + strOldWJBS + "'" + vbCr

                    '׼������
                    objSqlCommand.Parameters.Clear()
                    intCount = objNewData.Count
                    For i = 0 To intCount - 1 Step 1
                        Select Case objNewData.GetKey(i)
                            Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_V_SHENPIWENJIAN_NEW_QFRQ
                                If objNewData.Item(i) = "" Then
                                    objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), System.DBNull.Value)
                                Else
                                    objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), CType(objNewData.Item(i), System.DateTime))
                                End If
                            Case Else
                                If objNewData.Item(i) = "" Then
                                    objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), " ")
                                Else
                                    objSqlCommand.Parameters.AddWithValue("@A" + i.ToString(), objNewData.Item(i))
                                End If
                        End Select
                    Next
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '�ύ����
                objSqlTransaction.Commit()
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '����
            doUpdateWJXX = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function


    End Class

End Namespace
