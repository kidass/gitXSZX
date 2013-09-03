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
    ' ����    ��systemFlowObject
    '
    ' ���������� 
    '   ������������ı��ֲ�Ļ�����
    '----------------------------------------------------------------
    Public MustInherit Class systemFlowObject
        Inherits MarshalByRefObject
        Implements IDisposable

        '�������͡����󴴽��ӿ�ע����(���ж�����)
        Private Shared m_objFlowTypeNameEnum As System.Collections.Specialized.NameValueCollection
        Private Shared m_objFlowTypeEnum As System.Collections.Specialized.ListDictionary

        '��ҵ�߼������
        Protected m_objrulesFlowObject As Xydc.Platform.BusinessRules.rulesFlowObject

        '���������������Ƿ����ִ�У�
        Protected m_blnFSWJ As Boolean '�����ļ�
        Protected m_blnTHWJ As Boolean '�˻��ļ�
        Protected m_blnJSWJ As Boolean '�����ļ�
        Protected m_blnSHWJ As Boolean '�ջ��ļ�
        Protected m_blnXGWJ As Boolean '�޸��ļ�
        Protected m_blnBCWJ As Boolean '�����ļ�
        Protected m_blnQXXG As Boolean 'ȡ���޸�
        Protected m_blnSXWJ As Boolean 'ˢ���ļ�
        Protected m_blnTXYJ As Boolean '��д���
        Protected m_blnBDPS As Boolean '������ʾ
        Protected m_blnCBWJ As Boolean '�߰��ļ�
        Protected m_blnDBWJ As Boolean '�����ļ�
        Protected m_blnDBJG As Boolean '������
        Protected m_blnBYBL As Boolean '�Ҳ��ð�
        Protected m_blnBLWB As Boolean '���Ѱ���
        Protected m_blnWYYZ As Boolean '������֪
        Protected m_blnZHBL As Boolean '�ݻ�����
        Protected m_blnJXBL As Boolean '��������
        Protected m_blnZFWJ As Boolean '�����ļ�
        Protected m_blnQYWJ As Boolean '�����ļ�
        Protected m_blnWJBY As Boolean '�ļ�����
        Protected m_blnCYYJ As Boolean '�������
        Protected m_blnCKLZ As Boolean '�鿴��ת
        Protected m_blnCKRZ As Boolean '�鿴��־
        Protected m_blnCKBY As Boolean '�鿴����
        Protected m_blnCKCB As Boolean '�鿴�߰�
        Protected m_blnCKDB As Boolean '�鿴����
        Protected m_blnDYGZ As Boolean '��ӡ��ֽ
        Protected m_blnDYBJ As Boolean '��ӡ���
        Protected m_blnWJBJ As Boolean '�ļ����
        Protected m_blnFHSJ As Boolean '�����ϼ�

        'ģ�鸽����Ϣ
        Protected m_blnEnforeEdit As Boolean  '���ǿ�Ʊ༭
        Protected m_blnMustJieshou As Boolean '�����Ƚ����ļ�









        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Protected Sub New()
            MyBase.New()

            m_objrulesFlowObject = Nothing

            'ģ������
            m_blnFSWJ = False '�����ļ�
            m_blnTHWJ = False '�˻��ļ�
            m_blnJSWJ = False '�����ļ�
            m_blnSHWJ = False '�ջ��ļ�
            m_blnXGWJ = False '�޸��ļ�
            m_blnBCWJ = False '�����ļ�
            m_blnQXXG = False 'ȡ���޸�
            m_blnSXWJ = False 'ˢ���ļ�
            m_blnTXYJ = False '��д���
            m_blnBDPS = False '������ʾ
            m_blnCBWJ = False '�߰��ļ�
            m_blnDBWJ = False '�����ļ�
            m_blnDBJG = False '������
            m_blnBYBL = False '�Ҳ��ð�
            m_blnBLWB = False '���Ѱ���
            m_blnWYYZ = False '������֪
            m_blnZHBL = False '�ݻ�����
            m_blnJXBL = False '��������
            m_blnZFWJ = False '�����ļ�
            m_blnQYWJ = False '�����ļ�
            m_blnWJBY = False '�ļ�����
            m_blnCYYJ = False '�������
            m_blnCKLZ = False '�鿴��ת
            m_blnCKRZ = False '�鿴��־
            m_blnCKBY = False '�鿴����
            m_blnCKCB = False '�鿴�߰�
            m_blnCKDB = False '�鿴����
            m_blnDYGZ = False '��ӡ��ֽ
            m_blnDYBJ = False '��ӡ���
            m_blnWJBJ = False '�ļ����
            m_blnFHSJ = False '�����ϼ�

            'ģ�鸽����Ϣ
            m_blnEnforeEdit = False  '���ǿ�Ʊ༭
            m_blnMustJieshou = False '�����Ƚ����ļ�

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
                m_objrulesFlowObject = Xydc.Platform.BusinessRules.rulesFlowObject.Create(strFlowType, strFlowTypeName)
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
            If Not (m_objrulesFlowObject Is Nothing) Then
                m_objrulesFlowObject.Dispose()
                m_objrulesFlowObject = Nothing
            End If
        End Sub

        '----------------------------------------------------------------
        ' ��ȫ�ͷű�����Դ
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.systemFlowObject)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub










        '----------------------------------------------------------------
        ' ����������ע����
        '     strFlowType          �����������ʹ���
        '     strFlowTypeName      ����������������
        '     objCreator           ������������ISystemFlowObjectCreate�ӿ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Shared Function RegisterFlowType( _
            ByVal strFlowType As String, _
            ByVal strFlowTypeName As String, _
            ByVal objCreator As Xydc.Platform.BusinessFacade.ISystemFlowObjectCreate) As Boolean

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
                    Throw New Exception("����[ISystemFlowObjectCreate]����Ϊ�գ�")
                End If

                '�������ͻ㼯��
                If m_objFlowTypeEnum Is Nothing Then
                    m_objFlowTypeEnum = New System.Collections.Specialized.ListDictionary
                End If
                If m_objFlowTypeNameEnum Is Nothing Then
                    m_objFlowTypeNameEnum = New System.Collections.Specialized.NameValueCollection
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
        '                          ��Xydc.Platform.BusinessFacade.systemFlowObject����
        '----------------------------------------------------------------
        Public Shared Function Create( _
            ByVal strFlowType As String, _
            ByVal strFlowTypeName As String) As Xydc.Platform.BusinessFacade.systemFlowObject

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

                'ע���Ѿ�ʵ�ֵ�ISystemFlowObjectCreate
                Dim strType As String
                Dim strName As String

                
                '*****************************************************************************************************
                '���鵥������
                'strType = Xydc.Platform.Common.Workflow.BaseFlowDuchadan.FLOWCODE
                'strName = Xydc.Platform.Common.Workflow.BaseFlowDuchadan.FLOWNAME
                'If m_objFlowTypeEnum Is Nothing Then
                '    RegisterFlowType(strType, strName, New Xydc.Platform.BusinessFacade.systemFlowObjectDuchadanCreator)
                'Else
                '    If m_objFlowTypeEnum.Item(strType) Is Nothing Then
                '        RegisterFlowType(strType, strName, New Xydc.Platform.BusinessFacade.systemFlowObjectDuchadanCreator)
                '    End If
                'End If

                '��ȡ�ӿ�
                Dim objCreator As Object
                objCreator = m_objFlowTypeEnum.Item(strFlowType)
                If objCreator Is Nothing Then
                    Throw New Exception("����[" + strFlowType + "]��֧�֣�")
                End If
                Dim objISystemFlowObjectCreate As Xydc.Platform.BusinessFacade.ISystemFlowObjectCreate
                objISystemFlowObjectCreate = CType(objCreator, Xydc.Platform.BusinessFacade.ISystemFlowObjectCreate)
                If objISystemFlowObjectCreate Is Nothing Then
                    Throw New Exception("����[" + strFlowType + "]��֧�֣�")
                End If

                '���ýӿڴ�������
                Create = objISystemFlowObjectCreate.Create(strFlowType, strFlowTypeName)

                '�Զ�������������
                Create.FlowData.FlowType = strFlowType
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

            getFlowType = Xydc.Platform.BusinessRules.rulesFlowObject.getFlowType(strFlowTypeName)

        End Function

        '----------------------------------------------------------------
        ' ����strFlowTypeName��ȡstrFlowType
        '     strFlowTypeName      ����������������
        ' ����
        '                          ��strFlowType
        '----------------------------------------------------------------
        Public Shared Function getFlowTypeCollection() As System.Collections.Specialized.NameValueCollection

            getFlowTypeCollection = m_objFlowTypeNameEnum

        End Function









        '----------------------------------------------------------------
        ' FlowData����
        '----------------------------------------------------------------
        Public ReadOnly Property FlowData() As Xydc.Platform.Common.Workflow.BaseFlowObject
            Get
                FlowData = m_objrulesFlowObject.FlowData
            End Get
        End Property

        '----------------------------------------------------------------
        ' IsInitialized����
        '----------------------------------------------------------------
        Public ReadOnly Property IsInitialized() As Boolean
            Get
                IsInitialized = m_objrulesFlowObject.IsInitialized
            End Get
        End Property

        '----------------------------------------------------------------
        ' IsFillData����
        '----------------------------------------------------------------
        Public ReadOnly Property IsFillData() As Boolean
            Get
                IsFillData = m_objrulesFlowObject.IsFillData
            End Get
        End Property



        '----------------------------------------------------------------
        ' mlFSWJ����
        '----------------------------------------------------------------
        Public ReadOnly Property mlFSWJ() As Boolean
            Get
                mlFSWJ = m_blnFSWJ
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlTHWJ����
        '----------------------------------------------------------------
        Public ReadOnly Property mlTHWJ() As Boolean
            Get
                mlTHWJ = m_blnTHWJ
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlJSWJ����
        '----------------------------------------------------------------
        Public ReadOnly Property mlJSWJ() As Boolean
            Get
                mlJSWJ = m_blnJSWJ
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlSHWJ����
        '----------------------------------------------------------------
        Public ReadOnly Property mlSHWJ() As Boolean
            Get
                mlSHWJ = m_blnSHWJ
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlXGWJ����
        '----------------------------------------------------------------
        Public ReadOnly Property mlXGWJ() As Boolean
            Get
                mlXGWJ = m_blnXGWJ
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlBCWJ����
        '----------------------------------------------------------------
        Public ReadOnly Property mlBCWJ() As Boolean
            Get
                mlBCWJ = m_blnBCWJ
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlQXXG����
        '----------------------------------------------------------------
        Public ReadOnly Property mlQXXG() As Boolean
            Get
                mlQXXG = m_blnQXXG
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlSXWJ����
        '----------------------------------------------------------------
        Public ReadOnly Property mlSXWJ() As Boolean
            Get
                mlSXWJ = m_blnSXWJ
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlTXYJ����
        '----------------------------------------------------------------
        Public ReadOnly Property mlTXYJ() As Boolean
            Get
                mlTXYJ = m_blnTXYJ
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlBDPS����
        '----------------------------------------------------------------
        Public ReadOnly Property mlBDPS() As Boolean
            Get
                mlBDPS = m_blnBDPS
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlCBWJ����
        '----------------------------------------------------------------
        Public ReadOnly Property mlCBWJ() As Boolean
            Get
                mlCBWJ = m_blnCBWJ
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlDBWJ����
        '----------------------------------------------------------------
        Public ReadOnly Property mlDBWJ() As Boolean
            Get
                mlDBWJ = m_blnDBWJ
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlDBJG����
        '----------------------------------------------------------------
        Public ReadOnly Property mlDBJG() As Boolean
            Get
                mlDBJG = m_blnDBJG
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlBYBL����
        '----------------------------------------------------------------
        Public ReadOnly Property mlBYBL() As Boolean
            Get
                mlBYBL = m_blnBYBL
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlBLWB����
        '----------------------------------------------------------------
        Public ReadOnly Property mlBLWB() As Boolean
            Get
                mlBLWB = m_blnBLWB
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlWYYZ����
        '----------------------------------------------------------------
        Public ReadOnly Property mlWYYZ() As Boolean
            Get
                mlWYYZ = m_blnWYYZ
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlZHBL����
        '----------------------------------------------------------------
        Public ReadOnly Property mlZHBL() As Boolean
            Get
                mlZHBL = m_blnZHBL
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlJXBL����
        '----------------------------------------------------------------
        Public ReadOnly Property mlJXBL() As Boolean
            Get
                mlJXBL = m_blnJXBL
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlZFWJ����
        '----------------------------------------------------------------
        Public ReadOnly Property mlZFWJ() As Boolean
            Get
                mlZFWJ = m_blnZFWJ
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlQYWJ����
        '----------------------------------------------------------------
        Public ReadOnly Property mlQYWJ() As Boolean
            Get
                mlQYWJ = m_blnQYWJ
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlWJBY����
        '----------------------------------------------------------------
        Public ReadOnly Property mlWJBY() As Boolean
            Get
                mlWJBY = m_blnWJBY
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlCYYJ����
        '----------------------------------------------------------------
        Public ReadOnly Property mlCYYJ() As Boolean
            Get
                mlCYYJ = m_blnCYYJ
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlCKLZ����
        '----------------------------------------------------------------
        Public ReadOnly Property mlCKLZ() As Boolean
            Get
                mlCKLZ = m_blnCKLZ
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlCKRZ����
        '----------------------------------------------------------------
        Public ReadOnly Property mlCKRZ() As Boolean
            Get
                mlCKRZ = m_blnCKRZ
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlCKBY����
        '----------------------------------------------------------------
        Public ReadOnly Property mlCKBY() As Boolean
            Get
                mlCKBY = m_blnCKBY
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlCKCB����
        '----------------------------------------------------------------
        Public ReadOnly Property mlCKCB() As Boolean
            Get
                mlCKCB = m_blnCKCB
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlCKDB����
        '----------------------------------------------------------------
        Public ReadOnly Property mlCKDB() As Boolean
            Get
                mlCKDB = m_blnCKDB
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlDYGZ����
        '----------------------------------------------------------------
        Public ReadOnly Property mlDYGZ() As Boolean
            Get
                mlDYGZ = m_blnDYGZ
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlDYBJ����
        '----------------------------------------------------------------
        Public ReadOnly Property mlDYBJ() As Boolean
            Get
                mlDYBJ = m_blnDYBJ
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlWJBJ����
        '----------------------------------------------------------------
        Public ReadOnly Property mlWJBJ() As Boolean
            Get
                mlWJBJ = m_blnWJBJ
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlFHSJ����
        '----------------------------------------------------------------
        Public ReadOnly Property mlFHSJ() As Boolean
            Get
                mlFHSJ = m_blnFHSJ
            End Get
        End Property



        '----------------------------------------------------------------
        ' pmEnforeEdit����
        '----------------------------------------------------------------
        Public ReadOnly Property pmEnforeEdit() As Boolean
            Get
                pmEnforeEdit = m_blnEnforeEdit
            End Get
        End Property

        '----------------------------------------------------------------
        ' pmMustJieshou����
        '----------------------------------------------------------------
        Public ReadOnly Property pmMustJieshou() As Boolean
            Get
                pmMustJieshou = m_blnMustJieshou
            End Get
        End Property




        '----------------------------------------------------------------
        ' swgjShowTrackRevisions����
        '----------------------------------------------------------------
        Public Overridable ReadOnly Property swgjShowTrackRevisions() As Boolean
            Get
                swgjShowTrackRevisions = True
            End Get
        End Property

        '----------------------------------------------------------------
        ' swgjSelectGJ����
        '----------------------------------------------------------------
        Public Overridable ReadOnly Property swgjSelectGJ() As Boolean
            Get
                swgjSelectGJ = False
            End Get
        End Property

        '----------------------------------------------------------------
        ' swgjImportFile����
        '----------------------------------------------------------------
        Public Overridable ReadOnly Property swgjImportFile() As Boolean
            Get
                swgjImportFile = False
            End Get
        End Property

        '----------------------------------------------------------------
        ' swgjExportFile����
        '----------------------------------------------------------------
        Public Overridable ReadOnly Property swgjExportFile() As Boolean
            Get
                swgjExportFile = True
            End Get
        End Property





        '----------------------------------------------------------------
        ' ��ȡ�������ļ�Ŀǰ�ܽ��е�����
        '     strErrMsg      �����ش�����Ϣ
        '     objTask        �������ܽ��е�����
        ' ���� 
        '     True           ���ɹ�
        '     False          ��ʧ��
        '----------------------------------------------------------------
        Public MustOverride Function getCanDoTaskList( _
            ByRef strErrMsg As String, _
            ByRef objTask As System.Collections.Specialized.NameValueCollection) As Boolean

        '----------------------------------------------------------------
        ' ��ָ���ļ��Ĺ�����ģ��
        ' ׼�����������ýӿڲ�����Ҫ���ʹ�������Url
        '     strErrMsg      �����ش�����Ϣ
        '     strControlId   ����ǰ�������ID
        '     strWJBS        ���ļ���ʶ
        '     strMSessionId  �����ñ�������ģ��ĸ�ģ���MSessionId
        '     strISessionId  �����ñ�������ģ��ĸ�ģ���ISessionId
        '     objEditType    ���������༭����
        '     Request        ����ǰHttpRequest
        '     Session        ����ǰHttpSessionState
        '     strUrl         ������Ҫ�򿪹������ļ���Url
        ' ���� 
        '     True           ���ɹ�
        '     False          ��ʧ��
        '----------------------------------------------------------------
        Public MustOverride Function doFileOpen( _
            ByRef strErrMsg As String, _
            ByVal strControlId As String, _
            ByVal strWJBS As String, _
            ByVal strMSessionId As String, _
            ByVal strISessionId As String, _
            ByVal objEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType, _
            ByVal Request As System.Web.HttpRequest, _
            ByVal Session As System.Web.SessionState.HttpSessionState, _
            ByRef strUrl As String) As Boolean

        '----------------------------------------------------------------
        ' ���½��ļ��Ĺ�����ģ��
        ' ׼�����������ýӿڲ�����Ҫ���ʹ�������Url
        '     strErrMsg      �����ش�����Ϣ
        '     strControlId   ����ǰ�������ID
        '     strWJBS        ���ļ���ʶ=""
        '     strMSessionId  �����ñ�������ģ��ĸ�ģ���MSessionId
        '     strISessionId  �����ñ�������ģ��ĸ�ģ���ISessionId
        '     objEditType    ���������༭����
        '     Request        ����ǰHttpRequest
        '     Session        ����ǰHttpSessionState
        '     strUrl         ������Ҫ�򿪹������ļ���Url
        '     strRSessionId  �������´����ĻỰID
        ' ���� 
        '     True           ���ɹ�
        '     False          ��ʧ��
        '----------------------------------------------------------------
        Public MustOverride Function doFileNew( _
            ByRef strErrMsg As String, _
            ByVal strControlId As String, _
            ByVal strWJBS As String, _
            ByVal strMSessionId As String, _
            ByVal strISessionId As String, _
            ByVal objEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType, _
            ByVal Request As System.Web.HttpRequest, _
            ByVal Session As System.Web.SessionState.HttpSessionState, _
            ByRef strUrl As String, _
            ByRef strRSessionId As String) As Boolean

        '----------------------------------------------------------------
        ' ��ȡ��ǰ��������webҳ��Url(���Ӧ�õĸ�·��)
        ' ����
        '                    ����ǰ��������webҳ��Url
        '----------------------------------------------------------------
        Public MustOverride Function getPageUrl() As String

        '----------------------------------------------------------------
        ' ����ɶԵ�ǰ�ļ����еĲ���
        '     strErrMsg      �����ش�����Ϣ
        '     strCzyId       ����ǰ�û�����
        '     strUserXM      ����ǰ�û�����
        '     strUserBMDM    ����ǰ�û���λ����
        ' ����
        '     True           ���ɹ�
        '     False          ��ʧ��
        '----------------------------------------------------------------
        Public MustOverride Function getCanExecuteCommand( _
            ByRef strErrMsg As String, _
            ByVal strCzyId As String, _
            ByVal strUserXM As String, _
            ByVal strUserBMDM As String) As Boolean


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
                '��ʼ������������
                If Me.m_objrulesFlowObject.doInitialize(strErrMsg, strUserId, strPassword, strWJBS, blnFillData) = False Then
                    GoTo errProc
                End If
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
            With Me.m_objrulesFlowObject
                getJiaojieData = .getJiaojieData(strErrMsg, strWhere, blnUnused, objJiaoJieData)
            End With
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
            With Me.m_objrulesFlowObject
                doSaveData_Jiaojie = .doSaveData_Jiaojie(strErrMsg, objOldData, objNewData, objenumEditType)
            End With
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
            With Me.m_objrulesFlowObject
                doUpdateJiaojie = .doUpdateJiaojie(strErrMsg, strWhere, strFileds)
            End With
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
            With Me.m_objrulesFlowObject
                getBanliData = .getBanliData(strErrMsg, strWhere, blnUnused, objBanliData)
            End With
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
            With Me.m_objrulesFlowObject
                doSaveData_Banli = .doSaveData_Banli(strErrMsg, objOldData, objNewData, objenumEditType)
            End With
        End Function


        '----------------------------------------------------------------
        ' ����strWJBS��ȡ������_B_�����������ݼ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strWJBS              ���ļ���ʶ
        '     objFujianData        ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getFujianData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWJBS As String, _
            ByRef objFujianData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Try
                getFujianData = Me.m_objrulesFlowObject.getFujianData(strErrMsg, strUserId, strPassword, strWJBS, objFujianData)
            Catch ex As Exception
                getFujianData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ����strWJBS��ȡ������_B_�����������ݼ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objFujianData        ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getFujianData( _
            ByRef strErrMsg As String, _
            ByRef objFujianData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Try
                getFujianData = Me.m_objrulesFlowObject.getFujianData(strErrMsg, objFujianData)
            Catch ex As Exception
                getFujianData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ����strWJBS��ȡ������_B_����ļ��������ݼ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strWJBS              ���ļ���ʶ
        '     objXGWJData          ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getXgwjData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWJBS As String, _
            ByRef objXGWJData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Try
                getXgwjData = Me.m_objrulesFlowObject.getXgwjData(strErrMsg, strUserId, strPassword, strWJBS, objXGWJData)
            Catch ex As Exception
                getXgwjData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ����strWJBS��ȡ������_B_����ļ��������ݼ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objXGWJData          ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getXgwjData( _
            ByRef strErrMsg As String, _
            ByRef objXGWJData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Try
                getXgwjData = Me.m_objrulesFlowObject.getXgwjData(strErrMsg, objXGWJData)
            Catch ex As Exception
                getXgwjData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ����strWJBS��ȡ������_B_���ӡ������ݼ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strWJBS              ���ļ���ʶ
        '     objJiaojieData       ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getJiaojieData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWJBS As String, _
            ByRef objJiaojieData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Try
                getJiaojieData = Me.m_objrulesFlowObject.getJiaojieData(strErrMsg, strUserId, strPassword, strWJBS, objJiaojieData)
            Catch ex As Exception
                getJiaojieData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ����strWJBS��ȡ������_B_���ӡ������ݼ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objJiaojieData       ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getJiaojieData( _
            ByRef strErrMsg As String, _
            ByRef objJiaojieData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Try
                getJiaojieData = Me.m_objrulesFlowObject.getJiaojieData(strErrMsg, objJiaojieData)
            Catch ex As Exception
                getJiaojieData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡstrUserXM���Ķ��������������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strWJBS              ���ļ���ʶ
        '     strUserXM            ��Ҫ�쿴���û�����
        '     objOpinionData       ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getOpinionData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWJBS As String, _
            ByVal strUserXM As String, _
            ByRef objOpinionData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Try
                getOpinionData = Me.m_objrulesFlowObject.getOpinionData(strErrMsg, strUserId, strPassword, strWJBS, strUserXM, objOpinionData)
            Catch ex As Exception
                getOpinionData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡstrUserXM���Ķ��������������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ��Ҫ�쿴���û�����
        '     objOpinionData       ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getOpinionData( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByRef objOpinionData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Try
                getOpinionData = Me.m_objrulesFlowObject.getOpinionData(strErrMsg, strUserXM, objOpinionData)
            Catch ex As Exception
                getOpinionData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡstrUserXM���Ķ��������������(����������)
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ��Ҫ�쿴���û�����
        '     strWhere             ����������
        '     objOpinionData       ����Ϣ���ݼ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getOpinionData( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByVal strWhere As String, _
            ByRef objOpinionData As Xydc.Platform.Common.Data.FlowData) As Boolean

            getOpinionData = False
            objOpinionData = Nothing

            Try
                If Me.m_objrulesFlowObject.getOpinionData(strErrMsg, strUserXM, strWhere, objOpinionData) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getOpinionData = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ�µ��ļ���ˮ��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strWJBS              ���ļ���ʶ
        '     strLSH               �������ļ���ˮ��
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getNewLSH( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWJBS As String, _
            ByRef strLSH As String) As Boolean

            Try
                getNewLSH = Me.m_objrulesFlowObject.getNewLSH(strErrMsg, strUserId, strPassword, strWJBS, strLSH)
            Catch ex As Exception
                getNewLSH = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡ�µ��ļ���ˮ��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strLSH               �������ļ���ˮ��
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getNewLSH( _
            ByRef strErrMsg As String, _
            ByRef strLSH As String) As Boolean

            Try
                getNewLSH = Me.m_objrulesFlowObject.getNewLSH(strErrMsg, strLSH)
            Catch ex As Exception
                getNewLSH = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡ�µ��ļ���ʶ
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strWJBS              ���ļ���ʶ
        '     strNewWJBS           �������ļ���ʶ
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getNewWJBS( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWJBS As String, _
            ByRef strNewWJBS As String) As Boolean

            Try
                getNewWJBS = Me.m_objrulesFlowObject.getNewWJBS(strErrMsg, strUserId, strPassword, strWJBS, strNewWJBS)
            Catch ex As Exception
                getNewWJBS = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡ�µ��ļ���ʶ
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strNewWJBS           �������ļ���ʶ
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getNewWJBS( _
            ByRef strErrMsg As String, _
            ByRef strNewWJBS As String) As Boolean

            Try
                getNewWJBS = Me.m_objrulesFlowObject.getNewWJBS(strErrMsg, strNewWJBS)
            Catch ex As Exception
                getNewWJBS = False
                strErrMsg = ex.Message
            End Try

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

            Try
                getNewFSXH = Me.m_objrulesFlowObject.getNewFSXH(strErrMsg, strFSXH)
            Catch ex As Exception
                getNewFSXH = False
                strErrMsg = ex.Message
            End Try

        End Function




        '----------------------------------------------------------------
        ' �ж��ļ��Ƿ�������?
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strWJBS              ���ļ���ʶ
        '     blnComplete          �������Ƿ�������?
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function isFileComplete( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWJBS As String, _
            ByRef blnComplete As Boolean) As Boolean

            Try
                isFileComplete = Me.m_objrulesFlowObject.isFileComplete(strErrMsg, strUserId, strPassword, strWJBS, blnComplete)
            Catch ex As Exception
                isFileComplete = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' �ж��ļ��Ƿ�������?
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     blnComplete          �������Ƿ�������?
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function isFileComplete( _
            ByRef strErrMsg As String, _
            ByRef blnComplete As Boolean) As Boolean

            Try
                isFileComplete = Me.m_objrulesFlowObject.isFileComplete(strErrMsg, blnComplete)
            Catch ex As Exception
                isFileComplete = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' �ж��ļ��Ƿ��Ѿ�����?
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strWJBS              ���ļ���ʶ
        '     blnDinggao           �������Ƿ��Ѿ�����?
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function isFileDinggao( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWJBS As String, _
            ByRef blnDinggao As Boolean) As Boolean

            Try
                isFileDinggao = Me.m_objrulesFlowObject.isFileDinggao(strErrMsg, strUserId, strPassword, strWJBS, blnDinggao)
            Catch ex As Exception
                isFileDinggao = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' �ж��ļ��Ƿ��Ѿ�����?
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     blnDinggao           �������Ƿ��Ѿ�����?
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function isFileDinggao( _
            ByRef strErrMsg As String, _
            ByRef blnDinggao As Boolean) As Boolean

            Try
                isFileDinggao = Me.m_objrulesFlowObject.isFileDinggao(strErrMsg, blnDinggao)
            Catch ex As Exception
                isFileDinggao = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' �ж��ļ��Ƿ��Ѿ�����?
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strWJBS              ���ļ���ʶ
        '     blnZuofei            �������Ƿ��Ѿ�����?
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function isFileZuofei( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWJBS As String, _
            ByRef blnZuofei As Boolean) As Boolean

            Try
                isFileZuofei = Me.m_objrulesFlowObject.isFileZuofei(strErrMsg, strUserId, strPassword, strWJBS, blnZuofei)
            Catch ex As Exception
                isFileZuofei = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' �ж��ļ��Ƿ��Ѿ�����?
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     blnZuofei            �������Ƿ��Ѿ�����?
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function isFileZuofei( _
            ByRef strErrMsg As String, _
            ByRef blnZuofei As Boolean) As Boolean

            Try
                isFileZuofei = Me.m_objrulesFlowObject.isFileZuofei(strErrMsg, blnZuofei)
            Catch ex As Exception
                isFileZuofei = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' �ж��ļ��Ƿ��Ѿ�ͣ��?
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strWJBS              ���ļ���ʶ
        '     blnTingban           �������Ƿ��Ѿ�ͣ��?
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function isFileTingban( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWJBS As String, _
            ByRef blnTingban As Boolean) As Boolean

            Try
                isFileTingban = Me.m_objrulesFlowObject.isFileTingban(strErrMsg, strUserId, strPassword, strWJBS, blnTingban)
            Catch ex As Exception
                isFileTingban = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' �ж��ļ��Ƿ��Ѿ�ͣ��?
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     blnTingban           �������Ƿ��Ѿ�ͣ��?
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function isFileTingban( _
            ByRef strErrMsg As String, _
            ByRef blnTingban As Boolean) As Boolean

            Try
                isFileTingban = Me.m_objrulesFlowObject.isFileTingban(strErrMsg, blnTingban)
            Catch ex As Exception
                isFileTingban = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' �ж�strUserXM�Ƿ����ļ���ԭʼ����?
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strWJBS              ���ļ���ʶ
        '     strUserXM            ����Ա����
        '     blnIs                �������Ƿ�?
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function isOriginalPeople( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWJBS As String, _
            ByVal strUserXM As String, _
            ByRef blnIs As Boolean) As Boolean

            Try
                isOriginalPeople = Me.m_objrulesFlowObject.isOriginalPeople(strErrMsg, strUserId, strPassword, strWJBS, strUserXM, blnIs)
            Catch ex As Exception
                isOriginalPeople = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' �ж�strUserXM�Ƿ����ļ���ԭʼ����?
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ����Ա����
        '     blnIs                �������Ƿ�?
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function isOriginalPeople( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByRef blnIs As Boolean) As Boolean

            Try
                isOriginalPeople = Me.m_objrulesFlowObject.isOriginalPeople(strErrMsg, strUserXM, blnIs)
            Catch ex As Exception
                isOriginalPeople = False
                strErrMsg = ex.Message
            End Try

        End Function





        '----------------------------------------------------------------
        ' �ж�ָ����ԱstrCzyId�Ƿ�ɶ����ļ���
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strWJBS              ���ļ���ʶ
        '     strCzyId             ����Ա����
        '     strBMDM              ��strCzyId������λ����
        '     blnCanDuban          �����أ��Ƿ���ԣ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function canDubanFile( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWJBS As String, _
            ByVal strCzyId As String, _
            ByVal strBMDM As String, _
            ByRef blnCanDuban As Boolean) As Boolean

            Try
                canDubanFile = Me.m_objrulesFlowObject.canDubanFile(strErrMsg, strUserId, strPassword, strWJBS, strCzyId, strBMDM, blnCanDuban)
            Catch ex As Exception
                canDubanFile = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' �ж�ָ����ԱstrCzyId�Ƿ�ɶ����ļ���
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strCzyId             ����Ա����
        '     strBMDM              ��strCzyId������λ����
        '     blnCanDuban          �����أ��Ƿ���ԣ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function canDubanFile( _
            ByRef strErrMsg As String, _
            ByVal strCzyId As String, _
            ByVal strBMDM As String, _
            ByRef blnCanDuban As Boolean) As Boolean

            Try
                canDubanFile = Me.m_objrulesFlowObject.canDubanFile(strErrMsg, strCzyId, strBMDM, blnCanDuban)
            Catch ex As Exception
                canDubanFile = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' �ж�ָ����ԱstrUserXM�Ƿ����д��������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strWJBS              ���ļ���ʶ
        '     strUserXM            ����ǰ������Ա����
        '     blnCanWrite          �����أ��Ƿ���ԣ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function canWriteDubanResult( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWJBS As String, _
            ByVal strUserXM As String, _
            ByRef blnCanWrite As Boolean) As Boolean

            Try
                canWriteDubanResult = Me.m_objrulesFlowObject.canWriteDubanResult(strErrMsg, strUserId, strPassword, strWJBS, strUserXM, blnCanWrite)
            Catch ex As Exception
                canWriteDubanResult = False
                strErrMsg = ex.Message
            End Try

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

            Try
                canWriteDubanResult = Me.m_objrulesFlowObject.canWriteDubanResult(strErrMsg, strUserXM, blnCanWrite)
            Catch ex As Exception
                canWriteDubanResult = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' �ж�ָ����Ա�Ƿ�ɴ߰��ļ���
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strWJBS              ���ļ���ʶ
        '     strUserXM            ��׼���߰��ļ�����Ա����
        '     blnCanCuiban         �����أ��Ƿ���Դ߰죿
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function canCuibanFile( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWJBS As String, _
            ByVal strUserXM As String, _
            ByRef blnCanCuiban As Boolean) As Boolean

            Try
                canCuibanFile = Me.m_objrulesFlowObject.canCuibanFile(strErrMsg, strUserId, strPassword, strWJBS, strUserXM, blnCanCuiban)
            Catch ex As Exception
                canCuibanFile = False
                strErrMsg = ex.Message
            End Try

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

            Try
                canCuibanFile = Me.m_objrulesFlowObject.canCuibanFile(strErrMsg, strUserXM, blnCanCuiban)
            Catch ex As Exception
                canCuibanFile = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' �ж�ָ����Ա�Ƿ�ɲ����쵼�����
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strWJBS              ���ļ���ʶ
        '     strCzyId             ��׼�������쵼�������Ա����
        '     strBMDM              ��׼�������쵼�������Ա������λ����
        '     blnCanBudeng         �����أ��Ƿ���ԣ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function canBuDengFile( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWJBS As String, _
            ByVal strCzyId As String, _
            ByVal strBMDM As String, _
            ByRef blnCanBudeng As Boolean) As Boolean

            Try
                canBuDengFile = Me.m_objrulesFlowObject.canBuDengFile(strErrMsg, strUserId, strPassword, strWJBS, strCzyId, strBMDM, blnCanBudeng)
            Catch ex As Exception
                canBuDengFile = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' �ж�ָ����Ա�Ƿ�ɲ����쵼�����
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strCzyId             ��׼�������쵼�������Ա����
        '     strBMDM              ��׼�������쵼�������Ա������λ����
        '     blnCanBudeng         �����أ��Ƿ���ԣ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function canBuDengFile( _
            ByRef strErrMsg As String, _
            ByVal strCzyId As String, _
            ByVal strBMDM As String, _
            ByRef blnCanBudeng As Boolean) As Boolean

            Try
                canBuDengFile = Me.m_objrulesFlowObject.canBuDengFile(strErrMsg, strCzyId, strBMDM, blnCanBudeng)
            Catch ex As Exception
                canBuDengFile = False
                strErrMsg = ex.Message
            End Try

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

            Try
                canReadFile = Me.m_objrulesFlowObject.canReadFile(strErrMsg, strUserXM, blnCanRead)
            Catch ex As Exception
                canReadFile = False
                strErrMsg = ex.Message
            End Try

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

            Try
                canSendTo = Me.m_objrulesFlowObject.canSendTo(strErrMsg, strSenderList, strReceiver, blnCanSend, strNewReceiver)
            Catch ex As Exception
                canSendTo = False
                strErrMsg = ex.Message
            End Try

        End Function





        '----------------------------------------------------------------
        ' strUserXM�Ƿ�Ϊ�Զ�ǩ���ļ���
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strWJBS              ���ļ���ʶ
        '     strUserXM            ���û�����
        '     blnAutoReceive       �������Ƿ�?
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function isAutoReceive( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWJBS As String, _
            ByVal strUserXM As String, _
            ByRef blnAutoReceive As Boolean) As Boolean

            Try
                isAutoReceive = Me.m_objrulesFlowObject.isAutoReceive(strErrMsg, strUserId, strPassword, strWJBS, strUserXM, blnAutoReceive)
            Catch ex As Exception
                isAutoReceive = False
                strErrMsg = ex.Message
            End Try

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

            Try
                isAutoReceive = Me.m_objrulesFlowObject.isAutoReceive(strErrMsg, strUserXM, blnAutoReceive)
            Catch ex As Exception
                isAutoReceive = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' �ж�ָ����ԱstrUserXM�Ƿ���Խ����ļ���
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strWJBS              ���ļ���ʶ
        '     strUserXM            ��������Ա����
        '     blnCanDoJieshou      �����أ��Ƿ���ԣ�
        '     strFSRList           �����أ������������б�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function canDoJieshouFile( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWJBS As String, _
            ByVal strUserXM As String, _
            ByRef blnCanDoJieshou As Boolean, _
            ByRef strFSRList As String) As Boolean

            Try
                canDoJieshouFile = Me.m_objrulesFlowObject.canDoJieshouFile(strErrMsg, strUserId, strPassword, strWJBS, strUserXM, blnCanDoJieshou, strFSRList)
            Catch ex As Exception
                canDoJieshouFile = False
                strErrMsg = ex.Message
            End Try

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

            Try
                canDoJieshouFile = Me.m_objrulesFlowObject.canDoJieshouFile(strErrMsg, strUserXM, blnCanDoJieshou, strFSRList)
            Catch ex As Exception
                canDoJieshouFile = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' �ļ��Ƿ��͹���
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strWJBS              ���ļ���ʶ
        '     blnSendOnce          �������Ƿ�?
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function isFileSendOnce( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWJBS As String, _
            ByRef blnSendOnce As Boolean) As Boolean

            Try
                isFileSendOnce = Me.m_objrulesFlowObject.isFileSendOnce(strErrMsg, strUserId, strPassword, strWJBS, blnSendOnce)
            Catch ex As Exception
                isFileSendOnce = False
                strErrMsg = ex.Message
            End Try

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

            Try
                isFileSendOnce = Me.m_objrulesFlowObject.isFileSendOnce(strErrMsg, blnSendOnce)
            Catch ex As Exception
                isFileSendOnce = False
                strErrMsg = ex.Message
            End Try

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

            Try
                isReceiveZhizhi = Me.m_objrulesFlowObject.isReceiveZhizhi(strErrMsg, strUserXM, blnReceive)
            Catch ex As Exception
                isReceiveZhizhi = False
                strErrMsg = ex.Message
            End Try

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

            Try
                isSendZhizhi = Me.m_objrulesFlowObject.isSendZhizhi(strErrMsg, strUserXM, blnSend)
            Catch ex As Exception
                isSendZhizhi = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡstrUserXMû�а��������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strWJBS              ���ļ���ʶ
        '     strUserXM            ���û�����
        '     objJiaoJieData       �����ؽ�������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getNotCompletedTaskData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWJBS As String, _
            ByVal strUserXM As String, _
            ByRef objJiaoJieData As Xydc.Platform.Common.Data.FlowData) As Boolean

            Try
                getNotCompletedTaskData = Me.m_objrulesFlowObject.getNotCompletedTaskData(strErrMsg, strUserId, strPassword, strWJBS, strUserXM, objJiaoJieData)
            Catch ex As Exception
                getNotCompletedTaskData = False
                strErrMsg = ex.Message
            End Try

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

            Try
                getNotCompletedTaskData = Me.m_objrulesFlowObject.getNotCompletedTaskData(strErrMsg, strUserXM, objJiaoJieData)
            Catch ex As Exception
                getNotCompletedTaskData = False
                strErrMsg = ex.Message
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
            isTaskComplete = Me.m_objrulesFlowObject.isTaskComplete(strTaskBLZT)
        End Function

        '----------------------------------------------------------------
        ' �Ƿ��˻ص����ˣ�
        '     strTaskStatus        ������״̬
        ' ����
        '     True                 ����
        '     False                ����
        '----------------------------------------------------------------
        Public Overridable Function isTaskTuihui(ByVal strTaskStatus As String) As Boolean
            isTaskTuihui = Me.m_objrulesFlowObject.isTaskTuihui(strTaskStatus)
        End Function

        '----------------------------------------------------------------
        ' �Ƿ��ջص����ˣ�
        '     strTaskStatus        ������״̬
        ' ����
        '     True                 ����
        '     False                ����
        '----------------------------------------------------------------
        Public Overridable Function isTaskShouhui(ByVal strTaskStatus As String) As Boolean
            isTaskShouhui = Me.m_objrulesFlowObject.isTaskShouhui(strTaskStatus)
        End Function

        '----------------------------------------------------------------
        ' �Ƿ�Ϊ֪ͨ�����ˣ�
        '     strTaskStatus        ������״̬
        ' ����
        '     True                 ����
        '     False                ����
        '----------------------------------------------------------------
        Public Overridable Function isTaskTongzhi(ByVal strTaskStatus As String) As Boolean
            isTaskTongzhi = Me.m_objrulesFlowObject.isTaskTongzhi(strTaskStatus)
        End Function

        '----------------------------------------------------------------
        ' �Ƿ�Ϊ�ظ������ˣ�
        '     strTaskStatus        ������״̬
        ' ����
        '     True                 ����
        '     False                ����
        '----------------------------------------------------------------
        Public Overridable Function isTaskHuifu(ByVal strTaskStatus As String) As Boolean
            isTaskHuifu = Me.m_objrulesFlowObject.isTaskHuifu(strTaskStatus)
        End Function

        '----------------------------------------------------------------
        ' �Ƿ�Ϊ�������ˣ�
        '     strTaskBLZL          ����������
        ' ����
        '     True                 ����
        '     False                ����
        '----------------------------------------------------------------
        Public Overridable Function isTaskTingban(ByVal strTaskBLZL As String) As Boolean
            isTaskTingban = Me.m_objrulesFlowObject.isTaskTingban(strTaskBLZL)
        End Function

        '----------------------------------------------------------------
        ' �ж�strBLSY�Ƿ��Ѿ���׼?
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strWJBS              ���ļ���ʶ
        '     strBLSY              ����������
        '     blnApproved          �������Ƿ�?
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function isTaskApproved( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWJBS As String, _
            ByVal strBLSY As String, _
            ByRef blnApproved As Boolean) As Boolean

            Try
                isTaskApproved = Me.m_objrulesFlowObject.isTaskApproved(strErrMsg, strUserId, strPassword, strWJBS, strBLSY, blnApproved)
            Catch ex As Exception
                isTaskApproved = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' �ж�strBLSY�Ƿ��Ѿ���׼?
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strBLSY              ����������
        '     blnApproved          �������Ƿ�?
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function isTaskApproved( _
            ByRef strErrMsg As String, _
            ByVal strBLSY As String, _
            ByRef blnApproved As Boolean) As Boolean

            Try
                isTaskApproved = Me.m_objrulesFlowObject.isTaskApproved(strErrMsg, strBLSY, blnApproved)
            Catch ex As Exception
                isTaskApproved = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' strUserXM�Ƿ���δ���֪ͨ�����ˣ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strWJBS              ���ļ���ʶ
        '     strUserXM            ���û�����
        '     blnHas               �������Ƿ�?
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function isHasNotCompleteTongzhi( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWJBS As String, _
            ByVal strUserXM As String, _
            ByRef blnHas As Boolean) As Boolean

            Try
                isHasNotCompleteTongzhi = Me.m_objrulesFlowObject.isHasNotCompleteTongzhi(strErrMsg, strUserId, strPassword, strWJBS, strUserXM, blnHas)
            Catch ex As Exception
                isHasNotCompleteTongzhi = False
                strErrMsg = ex.Message
            End Try

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

            Try
                isHasNotCompleteTongzhi = Me.m_objrulesFlowObject.isHasNotCompleteTongzhi(strErrMsg, strUserXM, blnHas)
            Catch ex As Exception
                isHasNotCompleteTongzhi = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' �Զ������ļ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strWJBS              ���ļ���ʶ
        '     strUserXM            ����Ա����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doAutoReceive( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWJBS As String, _
            ByVal strUserXM As String) As Boolean

            Try
                doAutoReceive = Me.m_objrulesFlowObject.doAutoReceive(strErrMsg, strUserId, strPassword, strWJBS, strUserXM)
            Catch ex As Exception
                doAutoReceive = False
                strErrMsg = ex.Message
            End Try

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

            Try
                doAutoReceive = Me.m_objrulesFlowObject.doAutoReceive(strErrMsg, strUserXM)
            Catch ex As Exception
                doAutoReceive = False
                strErrMsg = ex.Message
            End Try

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

            Try
                getOpinion = Me.m_objrulesFlowObject.getOpinion(strErrMsg, objOpinionData, strYJLX, strQSYJ, strBJYJ)
            Catch ex As Exception
                getOpinion = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ����strUserXM�Ѿ��Ķ���ָ���ļ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        '     strWJBS              ���ļ���ʶ
        '     strUserXM            ��������Ա����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doSetHasReadFile( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWJBS As String, _
            ByVal strUserXM As String) As Boolean

            Try
                doSetHasReadFile = Me.m_objrulesFlowObject.doSetHasReadFile(strErrMsg, strUserId, strPassword, strWJBS, strUserXM)
            Catch ex As Exception
                doSetHasReadFile = False
                strErrMsg = ex.Message
            End Try

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

            Try
                doSetHasReadFile = Me.m_objrulesFlowObject.doSetHasReadFile(strErrMsg, strUserXM)
            Catch ex As Exception
                doSetHasReadFile = False
                strErrMsg = ex.Message
            End Try

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

            Try
                getFileLocked = Me.m_objrulesFlowObject.getFileLocked(strErrMsg, blnLocked, strBMMC, strRYMC)
            Catch ex As Exception
                getFileLocked = False
                strErrMsg = ex.Message
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

            Try
                doLockFile = Me.m_objrulesFlowObject.doLockFile(strErrMsg, strUserId, blnLocked)
            Catch ex As Exception
                doLockFile = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȡ��ǰ�ļ��ĸ������
        '     strErrMsg      �����ش�����Ϣ
        '     blnEditMode    ���༭ģʽ
        '     strCacheFile   ��(����)��ǰ�����ļ���(����)
        '     strMBPath      ��ģ���ļ�Ŀ¼
        '     strGJPath      ������ļ�Ŀ¼
        '     strGJFile      ��(����)���ص�HTTP�������е���ʱ�ļ���
        ' ����
        '     True           ���ɹ�
        '     False          ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getGJFile( _
            ByRef strErrMsg As String, _
            ByVal blnEditMode As Boolean, _
            ByRef strCacheFile As String, _
            ByVal strMBPath As String, _
            ByVal strGJPath As String, _
            ByRef strGJFile As String) As Boolean

            Try
                getGJFile = Me.m_objrulesFlowObject.getGJFile(strErrMsg, blnEditMode, strCacheFile, strMBPath, strGJPath, strGJFile)
            Catch ex As Exception
                getGJFile = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ɾ���ļ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserId            ���û���ʶ
        '     strPassword          ���û�����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doDeleteFile( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String) As Boolean

            Try
                doDeleteFile = Me.m_objrulesFlowObject.doDeleteFile(strErrMsg, strUserId, strPassword)
            Catch ex As Exception
                doDeleteFile = False
                strErrMsg = ex.Message
            End Try

        End Function

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
        Public Overridable Function doVerifyFile( _
            ByRef strErrMsg As String, _
            ByRef objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objOldData As Xydc.Platform.Common.Workflow.BaseFlowObject, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Try
                doVerifyFile = Me.m_objrulesFlowObject.doVerifyFile(strErrMsg, objNewData, objOldData, objenumEditType)
            Catch ex As Exception
                doVerifyFile = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' �����¼
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objNewData           ����¼��ֵ(���ر�������ֵ)
        '     objOldData           ����¼��ֵ
        '     objenumEditType      ���༭����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doSaveFile( _
            ByRef strErrMsg As String, _
            ByRef objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objOldData As Xydc.Platform.Common.Workflow.BaseFlowObject, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Try
                doSaveFile = Me.m_objrulesFlowObject.doSaveFile(strErrMsg, objNewData, objOldData, objenumEditType)
            Catch ex As Exception
                doSaveFile = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���湤������¼(�����������)
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strUserId              ���û���ʶ
        '     strPassword            ���û�����
        '     strUserXM              ����ǰ������Ա
        '     blnEnforeEdit          ��ǿ�Ʊ༭�ļ�����
        '     objNewData             ����¼��ֵ(���ر�������ֵ)
        '     objOldData             ����¼��ֵ
        '     objenumEditType        ���༭����
        '     strGJFile              ��Ҫ����ĸ���ļ��ı��ػ����ļ�����·��
        '     objDataSet_FJ          ��Ҫ����ĸ�������
        '     objDataSet_XGWJ        ��Ҫ���������ļ�����
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doSaveFile( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByVal blnEnforeEdit As Boolean, _
            ByRef objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objOldData As Xydc.Platform.Common.Workflow.BaseFlowObject, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType, _
            ByVal strGJFile As String, _
            ByVal objDataSet_FJ As Xydc.Platform.Common.Data.FlowData, _
            ByVal objDataSet_XGWJ As Xydc.Platform.Common.Data.FlowData) As Boolean

            Try
                doSaveFile = Me.m_objrulesFlowObject.doSaveFile( _
                    strErrMsg, _
                    strUserId, strPassword, strUserXM, _
                    blnEnforeEdit, objNewData, objOldData, objenumEditType, _
                    strGJFile, objDataSet_FJ, objDataSet_XGWJ)
            Catch ex As Exception
                doSaveFile = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���湤������¼(�����������)
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strUserId              ���û���ʶ
        '     strPassword            ���û�����
        '     strUserXM              ����ǰ������Ա
        '     blnEnforeEdit          ��ǿ�Ʊ༭�ļ�����
        '     objNewData             ����¼��ֵ(���ر�������ֵ)
        '     objOldData             ����¼��ֵ
        '     objenumEditType        ���༭����
        '     strGJFile              ��Ҫ����ĸ���ļ��ı��ػ����ļ�����·��
        '     objDataSet_FJ          ��Ҫ����ĸ�������
        '     objDataSet_XGWJ        ��Ҫ���������ļ�����
        '     objParams              ������Ҫ�������ύ������
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doSaveFileVariantParam( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByVal blnEnforeEdit As Boolean, _
            ByRef objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objOldData As Xydc.Platform.Common.Workflow.BaseFlowObject, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType, _
            ByVal strGJFile As String, _
            ByVal objDataSet_FJ As Xydc.Platform.Common.Data.FlowData, _
            ByVal objDataSet_XGWJ As Xydc.Platform.Common.Data.FlowData, _
            ByVal objParams As System.Collections.Specialized.ListDictionary) As Boolean

            Try
                doSaveFileVariantParam = Me.m_objrulesFlowObject.doSaveFileVariantParam( _
                    strErrMsg, _
                    strUserId, strPassword, strUserXM, _
                    blnEnforeEdit, objNewData, objOldData, objenumEditType, _
                    strGJFile, objDataSet_FJ, objDataSet_XGWJ, objParams)
            Catch ex As Exception
                doSaveFileVariantParam = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���湤�������������������ļ���¼(�����������)
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strUserId              ���û���ʶ
        '     strPassword            ���û�����
        '     strGJFile              ��Ҫ����ĸ���ļ��ı��ػ����ļ�����·��
        '     objDataSet_FJ          ��Ҫ����ĸ�������
        '     objDataSet_XGWJ        ��Ҫ���������ļ�����
        '     strUserXM              ����ǰ������Ա
        '     blnEnforeEdit          ��ǿ�Ʊ༭�ļ�����
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doSaveFileZDBC( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strGJFile As String, _
            ByVal objDataSet_FJ As Xydc.Platform.Common.Data.FlowData, _
            ByVal objDataSet_XGWJ As Xydc.Platform.Common.Data.FlowData, _
            ByVal strUserXM As String, _
            ByVal blnEnforeEdit As Boolean) As Boolean

            Try
                doSaveFileZDBC = Me.m_objrulesFlowObject.doSaveFileZDBC( _
                    strErrMsg, _
                    strUserId, strPassword, _
                    strGJFile, _
                    objDataSet_FJ, objDataSet_XGWJ, _
                    strUserXM, blnEnforeEdit)
            Catch ex As Exception
                doSaveFileZDBC = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���湤�������������������ļ���¼(�����������)
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strUserId              ���û���ʶ
        '     strPassword            ���û�����
        '     strGJFile              ��Ҫ����ĸ���ļ��ı��ػ����ļ�����·��
        '     objDataSet_FJ          ��Ҫ����ĸ�������
        '     objDataSet_XGWJ        ��Ҫ���������ļ�����
        '     strUserXM              ����ǰ������Ա
        '     blnEnforeEdit          ��ǿ�Ʊ༭�ļ�����
        '     objParams              ������Ҫ�������ύ������
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doSaveFileZDBCVariantParam( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strGJFile As String, _
            ByVal objDataSet_FJ As Xydc.Platform.Common.Data.FlowData, _
            ByVal objDataSet_XGWJ As Xydc.Platform.Common.Data.FlowData, _
            ByVal strUserXM As String, _
            ByVal blnEnforeEdit As Boolean, _
            ByVal objParams As System.Collections.Specialized.ListDictionary) As Boolean

            Try
                doSaveFileZDBCVariantParam = Me.m_objrulesFlowObject.doSaveFileZDBCVariantParam( _
                    strErrMsg, _
                    strUserId, strPassword, _
                    strGJFile, _
                    objDataSet_FJ, objDataSet_XGWJ, _
                    strUserXM, blnEnforeEdit, _
                    objParams)
            Catch ex As Exception
                doSaveFileZDBCVariantParam = False
                strErrMsg = ex.Message
            End Try

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

            Try
                doSaveFujian = Me.m_objrulesFlowObject.doSaveFujian(strErrMsg, blnEnforeEdit, strUserXM, objNewData)
            Catch ex As Exception
                doSaveFujian = False
                strErrMsg = ex.Message
            End Try

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

            Try
                doSaveFujian = Me.m_objrulesFlowObject.doSaveFujian(strErrMsg, blnEnforeEdit, strUserXM, objNewData)
            Catch ex As Exception
                doSaveFujian = False
                strErrMsg = ex.Message
            End Try

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

            Try
                doSaveXgwj = Me.m_objrulesFlowObject.doSaveXgwj(strErrMsg, blnEnforeEdit, strUserXM, objNewData)
            Catch ex As Exception
                doSaveXgwj = False
                strErrMsg = ex.Message
            End Try

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

            Try
                doSaveXgwjFujian = Me.m_objrulesFlowObject.doSaveXgwjFujian(strErrMsg, blnEnforeEdit, strUserXM, objNewData)
            Catch ex As Exception
                doSaveXgwjFujian = False
                strErrMsg = ex.Message
            End Try

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

            Try
                doVerifyFujian = Me.m_objrulesFlowObject.doVerifyFujian(strErrMsg, objNewData)
            Catch ex As Exception
                doVerifyFujian = False
                strErrMsg = ex.Message
            End Try

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

            Try
                doVerifyXgwjFujian = Me.m_objrulesFlowObject.doVerifyXgwjFujian(strErrMsg, objNewData)
            Catch ex As Exception
                doVerifyXgwjFujian = False
                strErrMsg = ex.Message
            End Try

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

            doDeleteData_FJ = False

            Try
                If Me.m_objrulesFlowObject.doDeleteData_FJ(strErrMsg, objOldData) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doDeleteData_FJ = True
            Exit Function
errProc:
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

            doDeleteData_XGWJ = False

            Try
                If Me.m_objrulesFlowObject.doDeleteData_XGWJ(strErrMsg, objOldData) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doDeleteData_XGWJ = True
            Exit Function
errProc:
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
            With Me.m_objrulesFlowObject
                doDeleteData_Banli = .doDeleteData_Banli(strErrMsg, intJJXH)
            End With
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

            doMoveTo_FJ = False

            Try
                If Me.m_objrulesFlowObject.doMoveTo_FJ(strErrMsg, objSrcData, objDesData) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

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

            doMoveTo_XGWJ = False

            Try
                If Me.m_objrulesFlowObject.doMoveTo_XGWJ(strErrMsg, objSrcData, objDesData) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

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

            doAutoAdjustXSXH_FJ = False

            Try
                If Me.m_objrulesFlowObject.doAutoAdjustXSXH_FJ(strErrMsg, objFJData) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

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

            doAutoAdjustXSXH_XGWJ = False

            Try
                If Me.m_objrulesFlowObject.doAutoAdjustXSXH_XGWJ(strErrMsg, objXGWJData) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doAutoAdjustXSXH_XGWJ = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ɾ�����ػ����ص�Web����������ʱ�ļ�
        '     strErrMsg      �����ش�����Ϣ
        '     objFJDataSet   ���������ݼ�
        ' ����
        '     True           ���ɹ�
        '     False          ��ʧ��
        '----------------------------------------------------------------
        Public Function doDeleteCacheFile_FJ( _
            ByRef strErrMsg As String, _
            ByVal objFJDataSet As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile

            doDeleteCacheFile_FJ = False

            Try
                '���
                If objFJDataSet Is Nothing Then
                    Exit Try
                End If
                If objFJDataSet.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_FUJIAN) Is Nothing Then
                    Exit Try
                End If

                '���ɾ����ʱ�ļ�
                Dim strFile As String
                Dim intCount As Integer
                Dim i As Integer
                With objFJDataSet.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_FUJIAN)
                    intCount = .Rows.Count
                    For i = intCount - 1 To 0 Step -1
                        strFile = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_BDWJ), "")
                        If strFile <> "" Then
                            If objBaseLocalFile.doDeleteFile(strErrMsg, strFile) = False Then
                                '���Բ��ɹ���������������
                            End If
                        End If
                    Next
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)

            doDeleteCacheFile_FJ = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ɾ�����ػ����ص�Web����������ʱ�ļ�
        '     strErrMsg      �����ش�����Ϣ
        '     objXGWJDataSet ���������+��ظ������ݼ�
        ' ����
        '     True           ���ɹ�
        '     False          ��ʧ��
        '----------------------------------------------------------------
        Public Function doDeleteCacheFile_XGWJ( _
            ByRef strErrMsg As String, _
            ByVal objXGWJDataSet As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile

            doDeleteCacheFile_XGWJ = False

            Try
                '���
                If objXGWJDataSet Is Nothing Then
                    Exit Try
                End If
                If objXGWJDataSet.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_SHENPIWENJIAN_FUJIAN) Is Nothing Then
                    Exit Try
                End If

                '���ɾ����ʱ�ļ�
                Dim strFile As String
                Dim intCount As Integer
                Dim i As Integer
                With objXGWJDataSet.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_SHENPIWENJIAN_FUJIAN)
                    intCount = .Rows.Count
                    For i = intCount - 1 To 0 Step -1
                        strFile = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_BDWJ), "")
                        If strFile <> "" Then
                            If objBaseLocalFile.doDeleteFile(strErrMsg, strFile) = False Then
                                '���Բ��ɹ���������������
                            End If
                        End If
                    Next
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)

            doDeleteCacheFile_XGWJ = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ����ܷ��Զ�����༭ģʽ�༭���������������ļ�����
        '     strErrMsg            �����ش�����Ϣ
        '     strUserId            ����ǰ��Ա��ʶ
        '     blnEditMode          ���༭ģʽ
        '     blnCanModify         ���ܷ���б༭��
        '     blnEnforeEdit        ���Ƿ�Ϊǿ�б༭��
        '     blnAutoEnter         �������ܷ��Զ�����༭ģʽ
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getCanAutoEnterEditMode( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal blnEditMode As Boolean, _
            ByVal blnCanModify As Boolean, _
            ByVal blnEnforeEdit As Boolean, _
            ByRef blnAutoEnter As Boolean) As Boolean

            getCanAutoEnterEditMode = False
            blnAutoEnter = False

            Try
                '���
                If Me.IsInitialized = False Then
                    strErrMsg = "���󣺹���������û�г�ʼ����"
                    GoTo errProc
                End If
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim

                If blnEditMode = True Then
                    '�༭ģʽ��
                Else
                    '�鿴ģʽ��
                    If blnCanModify = True Then
                        '���޸��ļ�
                        '�Զ�����Լ����ļ��ķ���
                        If Me.doLockFile(strErrMsg, strUserId, False) = False Then
                            GoTo errProc
                        End If
                        '��ȡ��ǰ�ļ��༭���
                        Dim strBMMC As String
                        Dim strRYMC As String
                        Dim blnDo As Boolean
                        If Me.getFileLocked(strErrMsg, blnDo, strBMMC, strRYMC) = False Then
                            GoTo errProc
                        End If
                        If blnDo = True Then
                            '�������ڱ༭��
                        Else
                            If blnEnforeEdit = True Then
                                '�Ѿ����壬���Զ����룡
                                blnAutoEnter = False
                            Else
                                blnAutoEnter = True
                            End If
                        End If
                    Else
                        '�����޸��ļ�
                    End If
                End If


                '����Զ�����༭״̬��������б༭����
                If blnAutoEnter = True Then
                    If Me.doLockFile(strErrMsg, strUserId, True) = False Then
                        GoTo errProc
                    End If
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getCanAutoEnterEditMode = True
            Exit Function
errProc:
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

            getWorkflowFileData = False

            Try
                If Me.m_objrulesFlowObject.getWorkflowFileData(strErrMsg, strUserXM, strWhere, objFileDataSet) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getWorkflowFileData = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ����strBLSY�ļ���
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strBLSY              ����������
        '     intLevel             �����ؼ���
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getTaskLevel( _
            ByRef strErrMsg As String, _
            ByVal strBLSY As String, _
            ByRef intLevel As Integer) As Boolean

            getTaskLevel = False

            Try
                If Me.m_objrulesFlowObject.getTaskLevel(strErrMsg, strBLSY, intLevel) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getTaskLevel = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' strSender��strReceiver���Ͳ��Ľ��ӵ������Զ������Ѿ��Ķ�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strSender            ��������Ա����
        '     strReceiver          ��������Ա����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doSendBuyueJJD( _
            ByRef strErrMsg As String, _
            ByVal strSender As String, _
            ByVal strReceiver As String) As Boolean

            doSendBuyueJJD = False

            Try
                If Me.m_objrulesFlowObject.doSendBuyueJJD(strErrMsg, strSender, strReceiver) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doSendBuyueJJD = True
            Exit Function
errProc:
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

            getWeituoren = False

            Try
                If Me.m_objrulesFlowObject.getWeituoren(strErrMsg, strUserXM, strWTR) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getWeituoren = True
            Exit Function
errProc:
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

            getLastZJBJiaojieData = False

            Try
                If Me.m_objrulesFlowObject.getLastZJBJiaojieData(strErrMsg, strUserXM, objJiaoJieData) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getLastZJBJiaojieData = True
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

            doSend = False

            Try
                If Me.m_objrulesFlowObject.doSend(strErrMsg, objJSRDataSet, strFSXH, strYJJH, intBLJB, strAddedJJXHList) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doSend = True
            Exit Function
errProc:
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

            doSetTaskComplete = False
            Try
                If Me.m_objrulesFlowObject.doSetTaskComplete(strErrMsg, strBLR) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try
            doSetTaskComplete = True
            Exit Function
errProc:
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

            doSetTaskComplete = False
            Try
                If Me.m_objrulesFlowObject.doSetTaskComplete(strErrMsg, strBLR, strNewJJXHList) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try
            doSetTaskComplete = True
            Exit Function
errProc:
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

            doSetTaskBWTX = False

            Try
                If Me.m_objrulesFlowObject.doSetTaskBWTX(strErrMsg, strBLR, blnBWTX) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doSetTaskBWTX = True
            Exit Function
errProc:
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

            doSendReply = False

            Try
                If Me.m_objrulesFlowObject.doSendReply(strErrMsg, strBLR, intMaxJJXH, strFSXH, strAddedJJXHList) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doSendReply = True
            Exit Function
errProc:
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

            doDeleteJiaojie = False

            Try
                If Me.m_objrulesFlowObject.doDeleteJiaojie(strErrMsg, strAddedJJXHList) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doDeleteJiaojie = True
            Exit Function
errProc:
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

            getMaxJJXH = False

            Try
                If Me.m_objrulesFlowObject.getMaxJJXH(strErrMsg, intMaxJJXH) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getMaxJJXH = True
            Exit Function
errProc:
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

            getJieshouDataSet = False

            Try
                If Me.m_objrulesFlowObject.getJieshouDataSet(strErrMsg, strUserXM, strWhere, objJieshouDataSet) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getJieshouDataSet = True
            Exit Function
errProc:
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

            doReceiveFile = False

            Try
                If Me.m_objrulesFlowObject.doReceiveFile(strErrMsg, objJiaojieData) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doReceiveFile = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �����������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strOldBlsy           ������ǰ�İ�������
        '     strNewBlsy           �������İ�������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doTranslateTask( _
            ByRef strErrMsg As String, _
            ByVal strOldBlsy As String, _
            ByRef strNewBlsy As String) As Boolean

            doTranslateTask = False

            Try
                If Me.m_objrulesFlowObject.doTranslateTask(strErrMsg, strOldBlsy, strNewBlsy) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doTranslateTask = True
            Exit Function
errProc:
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

            doTuihuiFile = False

            Try
                If Me.m_objrulesFlowObject.doTuihuiFile(strErrMsg, strYBLSY, strYXB, strFSXH, objJiaojieData, blnCanReadFile, objHasSendNoticeRY) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doTuihuiFile = True
            Exit Function
errProc:
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

            getShouhuiDataSet = False

            Try
                If Me.m_objrulesFlowObject.getShouhuiDataSet(strErrMsg, strUserXM, strWhere, objShouhuiDataSet) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getShouhuiDataSet = True
            Exit Function
errProc:
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

            doShouhuiFile = False

            Try
                If Me.m_objrulesFlowObject.doShouhuiFile(strErrMsg, strFSXH, objJiaojieData, blnSendNotice, objHasSendNoticeRY) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doShouhuiFile = True
            Exit Function
errProc:
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

            isEditFile = False

            Try
                If Me.m_objrulesFlowObject.isEditFile(strErrMsg, strUserXM, blnDo) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            isEditFile = True
            Exit Function
errProc:
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

            getTuihuiDataSet = False

            Try
                If Me.m_objrulesFlowObject.getTuihuiDataSet(strErrMsg, strUserXM, strWhere, objTuihuiDataSet) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getTuihuiDataSet = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���������ļ���ҵ��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ���û�����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doIQiyongFile( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String) As Boolean

            doIQiyongFile = False

            Try
                If Me.m_objrulesFlowObject.doIQiyongFile(strErrMsg, strUserXM) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doIQiyongFile = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���������ļ���ҵ��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ���û�����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doIZuofeiFile( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String) As Boolean

            doIZuofeiFile = False

            Try
                If Me.m_objrulesFlowObject.doIZuofeiFile(strErrMsg, strUserXM) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doIZuofeiFile = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ������������ҵ��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ���û�����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doIContinueFile( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String) As Boolean

            doIContinueFile = False

            Try
                If Me.m_objrulesFlowObject.doIContinueFile(strErrMsg, strUserXM) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doIContinueFile = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �����ݻ�����ҵ��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ���û�����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doIStopFile( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String) As Boolean

            doIStopFile = False

            Try
                If Me.m_objrulesFlowObject.doIStopFile(strErrMsg, strUserXM) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doIStopFile = True
            Exit Function
errProc:
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

            doIReadFile = False

            Try
                If Me.m_objrulesFlowObject.doIReadFile(strErrMsg, strUserXM) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doIReadFile = True
            Exit Function
errProc:
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

            doIDoNotProcess = False

            Try
                If Me.m_objrulesFlowObject.doIDoNotProcess(strErrMsg, strUserXM) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doIDoNotProcess = True
            Exit Function
errProc:
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

            doICompleteTask = False

            Try
                If Me.m_objrulesFlowObject.doICompleteTask(strErrMsg, strUserXM) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doICompleteTask = True
            Exit Function
errProc:
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

            getUncompleteTaskRY = False

            Try
                If Me.m_objrulesFlowObject.getUncompleteTaskRY(strErrMsg, strUserXM, strUserList) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getUncompleteTaskRY = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �����ļ���ᡱҵ��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strUserXM            ���û�����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doCompleteFile( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String) As Boolean

            doCompleteFile = False

            Try
                If Me.m_objrulesFlowObject.doCompleteFile(strErrMsg, strUserXM) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doCompleteFile = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ������ԭ�����ֶ�ֵ
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strPJYJ              ��(����)����ԭ���ֶ�ֵ
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getPJYJ( _
            ByRef strErrMsg As String, _
            ByRef strPJYJ As String) As Boolean

            getPJYJ = False

            Try
                If Me.m_objrulesFlowObject.getPJYJ(strErrMsg, strPJYJ) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getPJYJ = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��������ǩ������ҵ��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strFileSpec          ��Ҫ������ļ�·��(WEB������������ȫ·��)
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doImportQP( _
            ByRef strErrMsg As String, _
            ByVal strFileSpec As String) As Boolean

            doImportQP = False

            Try
                If Me.m_objrulesFlowObject.doImportQP(strErrMsg, strFileSpec) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doImportQP = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ����ʽ�ļ����ֶ�ֵ
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strZSWJ              ��(����)��ʽ�ļ��ֶ�ֵ
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getZSWJ( _
            ByRef strErrMsg As String, _
            ByRef strZSWJ As String) As Boolean

            getZSWJ = False

            Try
                If Me.m_objrulesFlowObject.getZSWJ(strErrMsg, strZSWJ) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getZSWJ = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ����������ʽ�ļ���ҵ��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strFileSpec          ��Ҫ������ļ�·��(WEB������������ȫ·��)
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doImportZS( _
            ByRef strErrMsg As String, _
            ByVal strFileSpec As String) As Boolean

            doImportZS = False

            Try
                If Me.m_objrulesFlowObject.doImportZS(strErrMsg, strFileSpec) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doImportZS = True
            Exit Function
errProc:
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

            getKeCuibanData = False

            Try
                If Me.m_objrulesFlowObject.getKeCuibanData(strErrMsg, strUserXM, objKeCuibanData) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getKeCuibanData = True
            Exit Function
errProc:
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

            getCuibanData = False

            Try
                If Me.m_objrulesFlowObject.getCuibanData(strErrMsg, strUserXM, objCuibanData) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getCuibanData = True
            Exit Function
errProc:
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

            getCuibanData = False

            Try
                If Me.m_objrulesFlowObject.getCuibanData(strErrMsg, objCuibanData) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getCuibanData = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ����߰�����
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     objNewData             ����¼��ֵ(���ر�������ֵ)
        '     objOldData             ����¼��ֵ
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doSaveCuiban( _
            ByRef strErrMsg As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByRef objNewData As System.Collections.Specialized.NameValueCollection) As Boolean

            doSaveCuiban = False

            Try
                If Me.m_objrulesFlowObject.doSaveCuiban(strErrMsg, objOldData, objNewData) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doSaveCuiban = True
            Exit Function
errProc:
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

            getDubanData = False

            Try
                If Me.m_objrulesFlowObject.getDubanData(strErrMsg, strUserXM, objDubanData) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getDubanData = True
            Exit Function
errProc:
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

            getDubanData = False

            Try
                If Me.m_objrulesFlowObject.getDubanData(strErrMsg, objDubanData) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getDubanData = True
            Exit Function
errProc:
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

            getKeDubanData = False

            Try
                If Me.m_objrulesFlowObject.getKeDubanData(strErrMsg, strUserXM, objKeDubanData) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getKeDubanData = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���涽������
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     objNewData             ����¼��ֵ(���ر�������ֵ)
        '     objOldData             ����¼��ֵ
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doSaveDuban( _
            ByRef strErrMsg As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByRef objNewData As System.Collections.Specialized.NameValueCollection) As Boolean

            doSaveDuban = False

            Try
                If Me.m_objrulesFlowObject.doSaveDuban(strErrMsg, objOldData, objNewData) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doSaveDuban = True
            Exit Function
errProc:
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

            getBeidubanData = False

            Try
                If Me.m_objrulesFlowObject.getBeidubanData(strErrMsg, strUserXM, objBeidubanData) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getBeidubanData = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���涽��������
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     intJJXH                ���������
        '     intDBXH                ���������
        '     strDBJG                ��������
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doSaveDuban( _
            ByRef strErrMsg As String, _
            ByVal intJJXH As Integer, _
            ByVal intDBXH As Integer, _
            ByVal strDBJG As String) As Boolean

            doSaveDuban = False

            Try
                If Me.m_objrulesFlowObject.doSaveDuban(strErrMsg, intJJXH, intDBXH, strDBJG) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doSaveDuban = True
            Exit Function
errProc:
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

            getLZQKDataSet = False

            Try
                If Me.m_objrulesFlowObject.getLZQKDataSet(strErrMsg, strUserXM, strWhere, objJiaoJieData) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getLZQKDataSet = True
            Exit Function
errProc:
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

            getCaozuorizhiData = False

            Try
                If Me.m_objrulesFlowObject.getCaozuorizhiData(strErrMsg, strWhere, objCaozuorizhiData) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getCaozuorizhiData = True
            Exit Function
errProc:
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

            getBuyueData = False

            Try
                If Me.m_objrulesFlowObject.getBuyueData(strErrMsg, strCksyList, strWhere, objBuyueData) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getBuyueData = True
            Exit Function
errProc:
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

            getBuyueSendData = False

            Try
                If Me.m_objrulesFlowObject.getBuyueSendData(strErrMsg, strUserXM, strWhere, objBuyueData) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getBuyueSendData = True
            Exit Function
errProc:
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

            getBuyueRecvData = False

            Try
                If Me.m_objrulesFlowObject.getBuyueRecvData(strErrMsg, strUserXM, strWhere, objBuyueData) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getBuyueRecvData = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' strSender��strReceiver���Ͳ�������
        '     strErrMsg            ����������򷵻ش�����Ϣ
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
            ByVal strFSXH As String, _
            ByVal strSender As String, _
            ByVal strReceiver As String, _
            ByVal strJJSM As String) As Boolean

            doSendBuyueRequest = False

            Try
                If Me.m_objrulesFlowObject.doSendBuyueRequest(strErrMsg, strFSXH, strSender, strReceiver, strJJSM) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doSendBuyueRequest = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' strSender��strReceiver���Ͳ���֪ͨ
        '     strErrMsg            ����������򷵻ش�����Ϣ
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
            ByVal strFSXH As String, _
            ByVal strSender As String, _
            ByVal strReceiver As String, _
            ByVal strJJSM As String) As Boolean

            doSendBuyueTongzhi = False

            Try
                If Me.m_objrulesFlowObject.doSendBuyueTongzhi(strErrMsg, strFSXH, strSender, strReceiver, strJJSM) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doSendBuyueTongzhi = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �ջز�������
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     intJJXH                ���������
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doShouhuiBuyueRequest( _
            ByRef strErrMsg As String, _
            ByVal intJJXH As Integer) As Boolean

            doShouhuiBuyueRequest = False

            Try
                If Me.m_objrulesFlowObject.doShouhuiBuyueRequest(strErrMsg, intJJXH) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doShouhuiBuyueRequest = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �ջز���֪ͨ
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     intJJXH                ���������
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��

        '----------------------------------------------------------------
        Public Overridable Function doShouhuiBuyueTongzhi( _
            ByRef strErrMsg As String, _
            ByVal intJJXH As Integer) As Boolean

            doShouhuiBuyueTongzhi = False

            Try
                If Me.m_objrulesFlowObject.doShouhuiBuyueTongzhi(strErrMsg, intJJXH) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doShouhuiBuyueTongzhi = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��׼��������
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     intJJXH                ���������
        '     strFSXH                ����������
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doPizhunBuyueRequest( _
            ByRef strErrMsg As String, _
            ByVal intJJXH As Integer, _
            ByVal strFSXH As String) As Boolean

            doPizhunBuyueRequest = False

            Try
                If Me.m_objrulesFlowObject.doPizhunBuyueRequest(strErrMsg, intJJXH, strFSXH) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doPizhunBuyueRequest = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �ܾ���������
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     intJJXH                ���������
        '     strFSXH                ����������
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doJujueBuyueRequest( _
            ByRef strErrMsg As String, _
            ByVal intJJXH As Integer, _
            ByVal strFSXH As String) As Boolean

            doJujueBuyueRequest = False

            Try
                If Me.m_objrulesFlowObject.doJujueBuyueRequest(strErrMsg, intJJXH, strFSXH) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doJujueBuyueRequest = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ת����������
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     intJJXH                ���������
        '     strFSXH                ����������
        '     strZFJSR               ��ת������Ľ������б�
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doZhuanfaBuyueRequest( _
            ByRef strErrMsg As String, _
            ByVal intJJXH As Integer, _
            ByVal strFSXH As String, _
            ByVal strZFJSR As String) As Boolean

            doZhuanfaBuyueRequest = False

            Try
                If Me.m_objrulesFlowObject.doZhuanfaBuyueRequest(strErrMsg, intJJXH, strFSXH, strZFJSR) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doZhuanfaBuyueRequest = True
            Exit Function
errProc:
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

            doReadBuyueTongzhi = False

            Try
                If Me.m_objrulesFlowObject.doReadBuyueTongzhi(strErrMsg, intJJXH) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doReadBuyueTongzhi = True
            Exit Function
errProc:
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

            Try
                If Me.m_objrulesFlowObject.getAllJsrSql(strErrMsg, strUserXM, strSQL) = False Then
                    GoTo errProc
                End If
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
        ' ǩ��ȷ��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strYjlx              ��Ҫȷ�ϵ��������
        '     strSPR               ��������
        '     intMode              ��ǩ��ģʽ��0-����ǩ��1-��ͬǩ
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doQianminQueren( _
            ByRef strErrMsg As String, _
            ByVal strYjlx As String, _
            ByVal strSPR As String, _
            ByVal intMode As Integer) As Boolean

            doQianminQueren = False

            Try
                If Me.m_objrulesFlowObject.doQianminQueren(strErrMsg, strYjlx, strSPR, intMode) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doQianminQueren = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ȡ��ǩ��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strYjlx              ��Ҫȡ�����������
        '     strSPR               ��������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doQianminCancel( _
            ByRef strErrMsg As String, _
            ByVal strYjlx As String, _
            ByVal strSPR As String) As Boolean

            doQianminCancel = False

            Try
                If Me.m_objrulesFlowObject.doQianminCancel(strErrMsg, strYjlx, strSPR) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doQianminCancel = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡ�������ܽ��е�ǩ������б�
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objYjlx              ��ǩ���������+��ʾ���Ƽ���
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getAllYjlx( _
            ByRef strErrMsg As String, _
            ByRef objYjlx As System.Collections.Specialized.NameValueCollection) As Boolean

            getAllYjlx = False

            Try
                If Me.m_objrulesFlowObject.getAllYjlx(strErrMsg, objYjlx) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getAllYjlx = True
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

            getKeBudengLingdao = False

            Try
                If Me.m_objrulesFlowObject.getKeBudengLingdao(strErrMsg, strUserXM, strList) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getKeBudengLingdao = True
            Exit Function
errProc:
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

            getLastSpsyJiaojieData = False

            Try
                If Me.m_objrulesFlowObject.getLastSpsyJiaojieData(strErrMsg, strUserXM, blnZTXZ, objJiaoJieData) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getLastSpsyJiaojieData = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���������������
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     intJJXH                ���������
        '     objNewData             ����¼��ֵ(���ر�������ֵ)
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doSaveSpyj( _
            ByRef strErrMsg As String, _
            ByVal intJJXH As Integer, _
            ByRef objNewData As System.Collections.Specialized.NameValueCollection) As Boolean

            doSaveSpyj = False

            Try
                If Me.m_objrulesFlowObject.doSaveSpyj(strErrMsg, intJJXH, objNewData) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doSaveSpyj = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ȡ��intJJXHָ���İ������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     intJJXH              ���������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doBanliCancel( _
            ByRef strErrMsg As String, _
            ByVal intJJXH As Integer) As Boolean

            doBanliCancel = False

            Try
                If Me.m_objrulesFlowObject.doBanliCancel(strErrMsg, intJJXH) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doBanliCancel = True
            Exit Function
errProc:
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

            getBanliData = False

            Try
                If Me.m_objrulesFlowObject.getBanliData(strErrMsg, intJJXH, objBanliData) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getBanliData = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���롰�Ƿ���׼����־
        ' ����
        '                          ���������ַ���
        '----------------------------------------------------------------
        Public Overridable Function doTranslateSFPZ(ByVal strSFPZ As String) As String
            doTranslateSFPZ = Me.m_objrulesFlowObject.doTranslateSFPZ(strSFPZ)
        End Function

        '----------------------------------------------------------------
        ' ��ȡ����׼�������־
        ' ����
        '                          �������־
        '----------------------------------------------------------------
        Public Overridable Function getPizhunBLBZ() As String
            getPizhunBLBZ = Me.m_objrulesFlowObject.getPizhunBLBZ()
        End Function

        '----------------------------------------------------------------
        ' ��ȡ����������������־
        ' ����
        '                          �������־
        '----------------------------------------------------------------
        Public Overridable Function getBaocunYijianBLBZ() As String
            getBaocunYijianBLBZ = Me.m_objrulesFlowObject.getBaocunYijianBLBZ()
        End Function

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
        Public Overridable Function isNeedQianminQuerenPrompt( _
            ByRef strErrMsg As String, _
            ByVal strYjlx As String, _
            ByVal strSPR As String, _
            ByRef blnNeed As Boolean, _
            ByRef strXyrList As String) As Boolean

            isNeedQianminQuerenPrompt = False

            Try
                If Me.m_objrulesFlowObject.isNeedQianminQuerenPrompt(strErrMsg, strYjlx, strSPR, blnNeed, strXyrList) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            isNeedQianminQuerenPrompt = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ����Ҫǩ��ȷ�ϵ���������?
        '     strYjlx              ����������
        ' ����
        '     True                 ����Ҫǩ��
        '     False                ������Ҫǩ��
        '----------------------------------------------------------------
        Public Overridable Function isQianminTask(ByVal strYjlx As String) As Boolean
            isQianminTask = Me.m_objrulesFlowObject.isQianminTask(strYjlx)
        End Function

        '----------------------------------------------------------------
        ' �Ƕ������ļ�ǩ�����������?����ǣ����������ַ���
        '     strYjlx              ����������
        ' ����
        '     True                 ����Ҫǩ��
        '     False                ������Ҫǩ��
        '----------------------------------------------------------------
        Public Overridable Function isFileQianminTask( _
            ByVal strYjlx As String, _
            ByRef strPrompt As String) As Boolean
            isFileQianminTask = Me.m_objrulesFlowObject.isFileQianminTask(strYjlx, strPrompt)
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

            isAllTaskComplete = False

            Try
                If Me.m_objrulesFlowObject.isAllTaskComplete(strErrMsg, strUserXM, blnComplete) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            isAllTaskComplete = True
            Exit Function
errProc:
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

            getFujianData = False

            Try
                If Me.m_objrulesFlowObject.getFujianData(strErrMsg, blnZSWJ, strFJNR) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getFujianData = True
            Exit Function
errProc:
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

            getFujianData = False

            Try
                If Me.m_objrulesFlowObject.getFujianData(strErrMsg, strFJNR) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getFujianData = True
            Exit Function
errProc:
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

            Try
                If Me.m_objrulesFlowObject.doExportToExcel(strErrMsg, objDataSet, strExcelFile) = False Then
                    GoTo errProc
                End If
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
        ' ��ȡȱʡ�������
        '     strYjlx              ����������
        ' ����
        '                          �������־
        '----------------------------------------------------------------
        Public Overridable Function getDefaultYJNR(ByVal strYJLX As String) As String
            getDefaultYJNR = Me.m_objrulesFlowObject.getDefaultYJNR(strYJLX)
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

            getSenderList = False
            Try
                If Me.m_objrulesFlowObject.getSenderList(strErrMsg, strUserXM, strSenderList) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try
            getSenderList = True
            Exit Function
errProc:
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
        Public Overridable Function doAddToAnjuan( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strAJBS As String, _
            ByVal strTempPath As String) As Boolean

            doAddToAnjuan = False

            Try
                If Me.m_objrulesFlowObject.doAddToAnjuan(strErrMsg, strUserId, strPassword, strAJBS, strTempPath) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doAddToAnjuan = True
            Exit Function
errProc:
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
            Try
                If Me.m_objrulesFlowObject.doWriteUserLog(strErrMsg, strUserId, strPassword, strAddress, strMachine, strCZMS) = False Then
                    GoTo errProc
                End If
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

            doWriteUserLog_Fujian = False
            Try
                If Me.m_objrulesFlowObject.doWriteUserLog_Fujian(strErrMsg, strUserId, strPassword, strAddress, strMachine, objNewFJData, objOldFJData) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try
            doWriteUserLog_Fujian = True
            Exit Function
errProc:
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

            doWriteUserLog_XGWJ = False
            Try
                If Me.m_objrulesFlowObject.doWriteUserLog_XGWJ(strErrMsg, strUserId, strPassword, strAddress, strMachine, objNewXGWJData, objOldXGWJData) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try
            doWriteUserLog_XGWJ = True
            Exit Function
errProc:
            Exit Function

        End Function
        '----------------------------------------------------------------
        ' ����Э���־����(����_B_����)
        '     strErrMsg              ����������򷵻ش�����Ϣ
        '     strUserXM              ����Ա����
        '     strNewXBBZ             ��Э���־
        ' ����
        '     True                   ���ɹ�
        '     False                  ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function doSetJiaojieXBBZ( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByVal strNewXBBZ As String) As Boolean

            doSetJiaojieXBBZ = False
            Try
                If Me.m_objrulesFlowObject.doSetJiaojieXBBZ(strErrMsg, strUserXM, strNewXBBZ) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try
            doSetJiaojieXBBZ = True
            Exit Function
errProc:
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

            doWriteXSXH = False
            Try
                If Me.m_objrulesFlowObject.doWriteXSXH(strErrMsg) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try
            doWriteXSXH = True
            Exit Function
errProc:
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
            getYijiaoData = Xydc.Platform.BusinessRules.rulesFlowObject.getYijiaoData(strErrMsg, strUserId, strPassword, strYJR, strJSR, strWhere, objYijiaoData)
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
            getJieshouData = Xydc.Platform.BusinessRules.rulesFlowObject.getJieshouData(strErrMsg, strUserId, strPassword, strYJR, strJSR, strWhere, objJieshouData)
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
            getYjrListData = Xydc.Platform.BusinessRules.rulesFlowObject.getYjrListData(strErrMsg, strUserId, strPassword, strJSR, objYjrData)
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
            doFile_Yijiao = Xydc.Platform.BusinessRules.rulesFlowObject.doFile_Yijiao(strErrMsg, strUserId, strPassword, strYJR, strJSR, strWJBS, strYJSM)
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
            getWJLX = Xydc.Platform.BusinessRules.rulesFlowObject.getWJLX(strErrMsg, strUserId, strPassword, strWJBS, strWJLX)
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
            doFile_Jieshou = Xydc.Platform.BusinessRules.rulesFlowObject.doFile_Jieshou(strErrMsg, strUserId, strPassword, strYJR, strJSR, strWJBS)
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

            With Me.m_objrulesFlowObject
                doUpdateWJXX = .doUpdateWJXX(strErrMsg, objNewData)
            End With

        End Function
    End Class

End Namespace
