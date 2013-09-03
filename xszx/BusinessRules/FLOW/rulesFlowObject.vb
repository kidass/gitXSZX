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
Imports System.Text.RegularExpressions
Imports Microsoft.VisualBasic

Imports Xydc.Platform.SystemFramework
Imports Xydc.Platform.Common
Imports Xydc.Platform.Common.Data
Imports Xydc.Platform.DataAccess

Namespace Xydc.Platform.BusinessRules

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessRules
    ' ����    ��rulesFlowObject
    '
    ' ���������� 
    '   ���������������ҵ�߼���Ļ�����
    '----------------------------------------------------------------
    Public MustInherit Class rulesFlowObject
        Implements IDisposable

        '�������͡����󴴽��ӿ�ע����(���ж�����)
        Private Shared m_objFlowTypeNameEnum As System.Collections.Specialized.NameValueCollection
        Private Shared m_objFlowTypeEnum As System.Collections.Specialized.ListDictionary

        '���ݲ����
        Protected m_objFlowObject As Xydc.Platform.DataAccess.FlowObject









        '----------------------------------------------------------------
        ' �������캯��
        '----------------------------------------------------------------
        Protected Sub New()
            MyBase.New()
            m_objFlowObject = Nothing
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
                m_objFlowObject = Xydc.Platform.DataAccess.FlowObject.Create(strFlowType, strFlowTypeName)
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
            If Not (m_objFlowObject Is Nothing) Then
                m_objFlowObject.Dispose()
                m_objFlowObject = Nothing
            End If
        End Sub

        '----------------------------------------------------------------
        ' ��ȫ�ͷű�����Դ
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessRules.rulesFlowObject)
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
        '     objCreator           ������������IRulesFlowObjectCreate�ӿ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Shared Function RegisterFlowType( _
            ByVal strFlowType As String, _
            ByVal strFlowTypeName As String, _
            ByVal objCreator As Xydc.Platform.BusinessRules.IRulesFlowObjectCreate) As Boolean

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
                    Throw New Exception("����[IRulesFlowObjectCreate]����Ϊ�գ�")
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
        '                          ��Xydc.Platform.BusinessRules.rulesFlowObject����
        '----------------------------------------------------------------
        Public Shared Function Create( _
            ByVal strFlowType As String, _
            ByVal strFlowTypeName As String) As Xydc.Platform.BusinessRules.rulesFlowObject

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

                'ע���Ѿ�ʵ�ֵ�RulesFlowObject
                Dim strType As String
                Dim strName As String

                '****************************************************************************************************
                
                '****************************************************************************************************
                '���鵥������
                'strType = Xydc.Platform.Common.Workflow.BaseFlowDuchadan.FLOWCODE
                'strName = Xydc.Platform.Common.Workflow.BaseFlowDuchadan.FLOWNAME
                'If m_objFlowTypeEnum Is Nothing Then
                '    RegisterFlowType(strType, strName, New Xydc.Platform.BusinessRules.rulesFlowObjectDuchadanCreator)
                'Else
                '    If m_objFlowTypeEnum.Item(strType) Is Nothing Then
                '        RegisterFlowType(strType, strName, New Xydc.Platform.BusinessRules.rulesFlowObjectDuchadanCreator)
                '    End If
                'End If

                '��ȡ�ӿ�zaz
                Dim objCreator As Object
                objCreator = m_objFlowTypeEnum.Item(strFlowType)
                If objCreator Is Nothing Then
                    Throw New Exception("����[" + strFlowType + "]��֧�֣�")
                End If
                Dim objIRulesFlowObjectCreate As Xydc.Platform.BusinessRules.IRulesFlowObjectCreate
                objIRulesFlowObjectCreate = CType(objCreator, Xydc.Platform.BusinessRules.IRulesFlowObjectCreate)
                If objIRulesFlowObjectCreate Is Nothing Then
                    Throw New Exception("����[" + strFlowType + "]��֧�֣�")
                End If

                '���ýӿڴ�������
                Create = objIRulesFlowObjectCreate.Create(strFlowType, strFlowTypeName)

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

            getFlowType = Xydc.Platform.DataAccess.FlowObject.getFlowType(strFlowTypeName)

        End Function

        '----------------------------------------------------------------
        ' FlowData����
        '----------------------------------------------------------------
        Public ReadOnly Property FlowData() As Xydc.Platform.Common.Workflow.BaseFlowObject
            Get
                FlowData = m_objFlowObject.FlowData
            End Get
        End Property

        '----------------------------------------------------------------
        ' IsInitialized����
        '----------------------------------------------------------------
        Public ReadOnly Property IsInitialized() As Boolean
            Get
                IsInitialized = m_objFlowObject.IsInitialized
            End Get
        End Property

        '----------------------------------------------------------------
        ' IsFillData����
        '----------------------------------------------------------------
        Public ReadOnly Property IsFillData() As Boolean
            Get
                IsFillData = m_objFlowObject.IsFillData
            End Get
        End Property










        '----------------------------------------------------------------
        ' ��ȡ���ģ���ļ���
        ' ����
        '                    �����ģ���ļ���
        '----------------------------------------------------------------
        Public MustOverride Function getGJMBFile() As String

        '----------------------------------------------------------------
        ' ��ȡ����ļ���FTP·����
        ' ����
        '                    ������ļ���FTP·����
        '----------------------------------------------------------------
        Public MustOverride Function getGJFTPFile() As String

        '----------------------------------------------------------------
        ' ��ģ���л�ȡ��ǰ�ļ��ĸ������
        '     strErrMsg      �����ش�����Ϣ
        '     strMBPath      ��ģ���ļ�Ŀ¼
        '     strGJPath      ������ļ�Ŀ¼
        '     strGJFile      ���������ص�HTTP�������е���ʱ�ļ���
        ' ����
        '     True           ���ɹ�
        '     False          ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getGJFileFromMB( _
            ByRef strErrMsg As String, _
            ByVal strMBPath As String, _
            ByVal strGJPath As String, _
            ByRef strGJFile As String) As Boolean

            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile

            getGJFileFromMB = False

            Try
                '��ȡģ���ļ�·��
                Dim strMBFileName As String = Me.getGJMBFile()
                Dim strSrcFile As String = ""
                strSrcFile = objBaseLocalFile.doMakePath(strMBPath, strMBFileName)

                '��ȡĿ��·��
                Dim strDesPath As String = strGJPath

                '����������Ŀ¼����ʱ�ļ���
                Dim strTempFile As String = ""
                If objBaseLocalFile.doCopyToTempFile(strErrMsg, strSrcFile, strDesPath, strTempFile) = False Then
                    GoTo errProc
                End If

                '����
                strGJFile = strTempFile
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)

            getGJFileFromMB = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��FTP�������л�ȡ��ǰ�ļ��ĸ������
        '     strErrMsg      �����ش�����Ϣ
        '     strMBPath      ��ģ���ļ�Ŀ¼
        '     strGJPath      ������ļ�Ŀ¼
        '     strGJFile      ���������ص�HTTP�������е���ʱ�ļ���
        ' ����
        '     True           ���ɹ�
        '     False          ��ʧ��
        '----------------------------------------------------------------
        Public Overridable Function getGJFileFromFTP( _
            ByRef strErrMsg As String, _
            ByVal strMBPath As String, _
            ByVal strGJPath As String, _
            ByRef strGJFile As String) As Boolean

            Dim objdacXitongpeizhi As New Xydc.Platform.DataAccess.dacXitongpeizhi
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objFtpServerParam As Xydc.Platform.Common.Utilities.FTPProperty
            Dim objBaseFTP As New Xydc.Platform.Common.Utilities.BaseFTP
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection

            getGJFileFromFTP = False

            Try
                '��ȡ�����FTPĿ¼
                Dim strFTPFile As String = ""
                strFTPFile = Me.getGJFTPFile()

                'û�и��,ȡĬ�ϸ��
                If strFTPFile = "" Then
                    If Me.getGJFileFromMB(strErrMsg, strMBPath, strGJPath, strGJFile) = False Then
                        GoTo errProc
                    End If
                    Exit Try
                End If

                '��ȡFTP����
                objSqlConnection = Me.m_objFlowObject.SqlConnection
                If objdacXitongpeizhi.getFtpServerParam(strErrMsg, objSqlConnection, objFtpServerParam) = False Then
                    GoTo errProc
                End If

                '��ȡĿ��·��
                Dim strDesPath As String = strGJPath

                '��ȡĿ���ļ���������·��
                Dim strTempFile As String = ""
                Dim strDesFile As String = ""
                If objBaseLocalFile.doCreateTempFile(strErrMsg, strFTPFile, True, strTempFile) = False Then
                    GoTo errProc
                End If
                strDesFile = objBaseLocalFile.doMakePath(strDesPath, strTempFile)

                'ִ����������
                Dim strUrl As String = ""
                strUrl = objFtpServerParam.getUrl(strFTPFile)
                With objFtpServerParam
                    If objBaseFTP.doGetFile( _
                        strErrMsg, _
                        strDesFile, _
                        strUrl, _
                        .UserID, .Password, _
                        .ProxyUrl, .ProxyUserID, .ProxyPassword) = False Then
                        GoTo errProc
                    End If
                End With

                '����
                strGJFile = strTempFile
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.FTPProperty.SafeRelease(objFtpServerParam)
            Xydc.Platform.DataAccess.dacXitongpeizhi.SafeRelease(objdacXitongpeizhi)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)

            getGJFileFromFTP = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.FTPProperty.SafeRelease(objFtpServerParam)
            Xydc.Platform.DataAccess.dacXitongpeizhi.SafeRelease(objdacXitongpeizhi)
            Xydc.Platform.Common.Utilities.BaseFTP.SafeRelease(objBaseFTP)
            Exit Function

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

            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim strWJBS As String

            getGJFile = False
            strGJFile = ""

            Try
                If strCacheFile Is Nothing Then strCacheFile = ""
                strCacheFile = strCacheFile.Trim
                If strMBPath Is Nothing Then strMBPath = ""
                strMBPath = strMBPath.Trim
                If strGJPath Is Nothing Then strGJPath = ""
                strGJPath.Trim()
                If strMBPath = "" Then
                    strErrMsg = "����û��ָ���ļ�ģ���·����"
                    GoTo errProc
                End If
                If strGJPath = "" Then
                    strErrMsg = "����û��ָ���ļ������·����"
                    GoTo errProc
                End If
                strWJBS = Me.FlowData.WJBS

                If blnEditMode = True Then
                    '��黺���ļ��Ƿ�ȷʵ���ڣ�
                    If strCacheFile <> "" Then
                        Dim strTemp As String = ""
                        strTemp = objBaseLocalFile.doMakePath(strGJPath, strCacheFile)
                        Dim blnDo As Boolean = False
                        If objBaseLocalFile.doFileExisted(strErrMsg, strTemp, blnDo) = False Then
                            GoTo errProc
                        End If
                        If blnDo = False Then
                            strErrMsg = "���󣺻����ļ�[" + strTemp + "]�����ڣ�"
                            GoTo errProc
                        End If
                    End If

                    If strCacheFile = "" Then
                        If strWJBS = "" Then
                            '��ģ���ļ���ȡ
                            If Me.getGJFileFromMB(strErrMsg, strMBPath, strGJPath, strGJFile) = False Then
                                GoTo errProc
                            End If
                        Else
                            '��FTP��ȡ
                            If Me.getGJFileFromFTP(strErrMsg, strMBPath, strGJPath, strGJFile) = False Then
                                GoTo errProc
                            End If
                        End If
                    Else
                        '��ȡ�����ļ�
                        strGJFile = strCacheFile
                    End If
                Else
                    'ɾ����ʱ�ļ�
                    Dim strFileSpec As String = ""
                    If strCacheFile <> "" Then
                        strFileSpec = objBaseLocalFile.doMakePath(strGJPath, strCacheFile)
                        If objBaseLocalFile.doDeleteFile(strErrMsg, strFileSpec) = False Then
                            '����ɾ�����ɹ���
                        End If
                    End If

                    '�鿴״̬,��ȡ����
                    If Me.getGJFileFromFTP(strErrMsg, strMBPath, strGJPath, strGJFile) = False Then
                        GoTo errProc
                    End If
                End If

                '�����ļ�
                strCacheFile = strGJFile
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)

            getGJFile = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
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
            strErrMsg = ""

            Try
                If Me.m_objFlowObject.getTaskLevel(strErrMsg, strBLSY, intLevel) = False Then
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
                If Me.m_objFlowObject.doInitialize(strErrMsg, strUserId, strPassword, strWJBS, blnFillData) = False Then
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

            getFujianData = False
            objFujianData = Nothing

            Try
                If Me.m_objFlowObject.doInitialize(strErrMsg, strUserId, strPassword, strWJBS, False) = False Then
                    GoTo errProc
                End If

                If Me.m_objFlowObject.getFujianData(strErrMsg, objFujianData) = False Then
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

            getFujianData = False
            objFujianData = Nothing

            Try
                If Me.m_objFlowObject.getFujianData(strErrMsg, objFujianData) = False Then
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

            getXgwjData = False
            objXGWJData = Nothing

            Try
                If Me.m_objFlowObject.doInitialize(strErrMsg, strUserId, strPassword, strWJBS, False) = False Then
                    GoTo errProc
                End If

                If Me.m_objFlowObject.getXgwjData(strErrMsg, objXGWJData) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getXgwjData = True
            Exit Function
errProc:
            Exit Function

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

            getXgwjData = False
            objXGWJData = Nothing

            Try
                If Me.m_objFlowObject.getXgwjData(strErrMsg, objXGWJData) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getXgwjData = True
            Exit Function
errProc:
            Exit Function

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

            getJiaojieData = False
            objJiaojieData = Nothing

            Try
                If Me.m_objFlowObject.doInitialize(strErrMsg, strUserId, strPassword, strWJBS, False) = False Then
                    GoTo errProc
                End If

                If Me.m_objFlowObject.getJiaojieData(strErrMsg, objJiaojieData) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getJiaojieData = True
            Exit Function
errProc:
            Exit Function

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

            getJiaojieData = False
            objJiaojieData = Nothing

            Try
                If Me.m_objFlowObject.getJiaojieData(strErrMsg, objJiaojieData) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getJiaojieData = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ȡstrUserXM���Ķ��������������(ȫ��)
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

            getOpinionData = False
            objOpinionData = Nothing

            Try
                If Me.m_objFlowObject.doInitialize(strErrMsg, strUserId, strPassword, strWJBS, False) = False Then
                    GoTo errProc
                End If

                If Me.m_objFlowObject.getCanReadOpinion(strErrMsg, strUserXM, "", objOpinionData) = False Then
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
        ' ��ȡstrUserXM���Ķ��������������(ȫ��)
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

            getOpinionData = False
            objOpinionData = Nothing

            Try
                If Me.m_objFlowObject.getCanReadOpinion(strErrMsg, strUserXM, "", objOpinionData) = False Then
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
                If Me.m_objFlowObject.getCanReadOpinion(strErrMsg, strUserXM, strWhere, objOpinionData) = False Then
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

            getNewLSH = False
            strLSH = ""

            Try
                If Me.m_objFlowObject.doInitialize(strErrMsg, strUserId, strPassword, strWJBS, False) = False Then
                    GoTo errProc
                End If

                If Me.m_objFlowObject.getNewLSH(strErrMsg, strLSH) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getNewLSH = True
            Exit Function
errProc:
            Exit Function

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

            getNewLSH = False
            strLSH = ""

            Try
                If Me.m_objFlowObject.getNewLSH(strErrMsg, strLSH) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getNewLSH = True
            Exit Function
errProc:
            Exit Function

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

            getNewWJBS = False
            strNewWJBS = ""

            Try
                If Me.m_objFlowObject.doInitialize(strErrMsg, strUserId, strPassword, strWJBS, False) = False Then
                    GoTo errProc
                End If

                If Me.m_objFlowObject.getNewWJBS(strErrMsg, strNewWJBS) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getNewWJBS = True
            Exit Function
errProc:
            Exit Function

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

            getNewWJBS = False
            strNewWJBS = ""

            Try
                If Me.m_objFlowObject.getNewWJBS(strErrMsg, strNewWJBS) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getNewWJBS = True
            Exit Function
errProc:
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

            getNewFSXH = False
            strFSXH = ""

            Try
                If Me.m_objFlowObject.getNewFSXH(strErrMsg, strFSXH) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getNewFSXH = True
            Exit Function
errProc:
            Exit Function

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

            isFileComplete = False
            blnComplete = False

            Try
                If Me.m_objFlowObject.doInitialize(strErrMsg, strUserId, strPassword, strWJBS, False) = False Then
                    GoTo errProc
                End If

                If Me.m_objFlowObject.isFileComplete(strErrMsg, blnComplete) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            isFileComplete = True
            Exit Function
errProc:
            Exit Function

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

            isFileComplete = False
            blnComplete = False

            Try
                If Me.m_objFlowObject.isFileComplete(strErrMsg, blnComplete) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            isFileComplete = True
            Exit Function
errProc:
            Exit Function

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

            isFileDinggao = False
            blnDinggao = False

            Try
                If Me.m_objFlowObject.doInitialize(strErrMsg, strUserId, strPassword, strWJBS, False) = False Then
                    GoTo errProc
                End If

                If Me.m_objFlowObject.isFileDinggao(strErrMsg, blnDinggao) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            isFileDinggao = True
            Exit Function
errProc:
            Exit Function

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

            isFileDinggao = False
            blnDinggao = False

            Try
                If Me.m_objFlowObject.isFileDinggao(strErrMsg, blnDinggao) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            isFileDinggao = True
            Exit Function
errProc:
            Exit Function

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

            isFileZuofei = False
            blnZuofei = False

            Try
                If Me.m_objFlowObject.doInitialize(strErrMsg, strUserId, strPassword, strWJBS, False) = False Then
                    GoTo errProc
                End If

                If Me.m_objFlowObject.isFileZuofei(strErrMsg, blnZuofei) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            isFileZuofei = True
            Exit Function
errProc:
            Exit Function

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

            isFileZuofei = False
            blnZuofei = False

            Try
                If Me.m_objFlowObject.isFileZuofei(strErrMsg, blnZuofei) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            isFileZuofei = True
            Exit Function
errProc:
            Exit Function

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

            isFileTingban = False
            blnTingban = False

            Try
                If Me.m_objFlowObject.doInitialize(strErrMsg, strUserId, strPassword, strWJBS, False) = False Then
                    GoTo errProc
                End If

                If Me.m_objFlowObject.isFileTingban(strErrMsg, blnTingban) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            isFileTingban = True
            Exit Function
errProc:
            Exit Function

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

            isFileTingban = False
            blnTingban = False

            Try
                If Me.m_objFlowObject.isFileTingban(strErrMsg, blnTingban) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            isFileTingban = True
            Exit Function
errProc:
            Exit Function

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

            isOriginalPeople = False
            blnIs = False

            Try
                If Me.m_objFlowObject.doInitialize(strErrMsg, strUserId, strPassword, strWJBS, False) = False Then
                    GoTo errProc
                End If

                If Me.m_objFlowObject.isOriginalPeople(strErrMsg, strUserXM, blnIs) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            isOriginalPeople = True
            Exit Function
errProc:
            Exit Function

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

            isOriginalPeople = False
            blnIs = False

            Try
                If Me.m_objFlowObject.isOriginalPeople(strErrMsg, strUserXM, blnIs) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            isOriginalPeople = True
            Exit Function
errProc:
            Exit Function

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

            canDubanFile = False
            blnCanDuban = False

            Try
                If Me.m_objFlowObject.doInitialize(strErrMsg, strUserId, strPassword, strWJBS, False) = False Then
                    GoTo errProc
                End If

                If Me.m_objFlowObject.canDubanFile(strErrMsg, strCzyId, strBMDM, blnCanDuban) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            canDubanFile = True
            Exit Function
errProc:
            Exit Function

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

            canDubanFile = False
            blnCanDuban = False

            Try
                If Me.m_objFlowObject.canDubanFile(strErrMsg, strCzyId, strBMDM, blnCanDuban) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            canDubanFile = True
            Exit Function
errProc:
            Exit Function

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

            canWriteDubanResult = False
            blnCanWrite = False

            Try
                If Me.m_objFlowObject.doInitialize(strErrMsg, strUserId, strPassword, strWJBS, False) = False Then
                    GoTo errProc
                End If

                If Me.m_objFlowObject.canWriteDubanResult(strErrMsg, strUserXM, blnCanWrite) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            canWriteDubanResult = True
            Exit Function
errProc:
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

            canWriteDubanResult = False
            blnCanWrite = False

            Try
                If Me.m_objFlowObject.canWriteDubanResult(strErrMsg, strUserXM, blnCanWrite) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            canWriteDubanResult = True
            Exit Function
errProc:
            Exit Function

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

            canCuibanFile = False
            blnCanCuiban = False

            Try
                If Me.m_objFlowObject.doInitialize(strErrMsg, strUserId, strPassword, strWJBS, False) = False Then
                    GoTo errProc
                End If

                If Me.m_objFlowObject.canCuibanFile(strErrMsg, strUserXM, blnCanCuiban) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            canCuibanFile = True
            Exit Function
errProc:
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

            canCuibanFile = False
            blnCanCuiban = False

            Try
                If Me.m_objFlowObject.canCuibanFile(strErrMsg, strUserXM, blnCanCuiban) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            canCuibanFile = True
            Exit Function
errProc:
            Exit Function

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

            canBuDengFile = False
            blnCanBudeng = False

            Try
                If Me.m_objFlowObject.doInitialize(strErrMsg, strUserId, strPassword, strWJBS, False) = False Then
                    GoTo errProc
                End If

                If Me.m_objFlowObject.canBuDengFile(strErrMsg, strCzyId, strBMDM, blnCanBudeng) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            canBuDengFile = True
            Exit Function
errProc:
            Exit Function

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

            canBuDengFile = False
            blnCanBudeng = False

            Try
                If Me.m_objFlowObject.canBuDengFile(strErrMsg, strCzyId, strBMDM, blnCanBudeng) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            canBuDengFile = True
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

            canReadFile = False
            blnCanRead = False

            Try
                If Me.m_objFlowObject.canReadFile(strErrMsg, strUserXM, blnCanRead) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            canReadFile = True
            Exit Function
errProc:
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

            canSendTo = False
            blnCanSend = False

            Try
                If Me.m_objFlowObject.canSendTo(strErrMsg, strSenderList, strReceiver, blnCanSend, strNewReceiver) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            canSendTo = True
            Exit Function
errProc:
            Exit Function

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

            isAutoReceive = False
            blnAutoReceive = False

            Try
                If Me.m_objFlowObject.doInitialize(strErrMsg, strUserId, strPassword, strWJBS, False) = False Then
                    GoTo errProc
                End If

                If Me.m_objFlowObject.isAutoReceive(strErrMsg, strUserXM, blnAutoReceive) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            isAutoReceive = True
            Exit Function
errProc:
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

            isAutoReceive = False
            blnAutoReceive = False

            Try
                If Me.m_objFlowObject.isAutoReceive(strErrMsg, strUserXM, blnAutoReceive) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            isAutoReceive = True
            Exit Function
errProc:
            Exit Function

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

            canDoJieshouFile = False
            blnCanDoJieshou = False

            Try
                If Me.m_objFlowObject.doInitialize(strErrMsg, strUserId, strPassword, strWJBS, False) = False Then
                    GoTo errProc
                End If

                If Me.m_objFlowObject.canDoJieshouFile(strErrMsg, strUserXM, blnCanDoJieshou, strFSRList) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            canDoJieshouFile = True
            Exit Function
errProc:
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

            canDoJieshouFile = False
            blnCanDoJieshou = False

            Try
                If Me.m_objFlowObject.canDoJieshouFile(strErrMsg, strUserXM, blnCanDoJieshou, strFSRList) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            canDoJieshouFile = True
            Exit Function
errProc:
            Exit Function

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

            isFileSendOnce = False
            blnSendOnce = False

            Try
                If Me.m_objFlowObject.doInitialize(strErrMsg, strUserId, strPassword, strWJBS, False) = False Then
                    GoTo errProc
                End If

                If Me.m_objFlowObject.isFileSendOnce(strErrMsg, blnSendOnce) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            isFileSendOnce = True
            Exit Function
errProc:
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

            isFileSendOnce = False
            blnSendOnce = False

            Try
                If Me.m_objFlowObject.isFileSendOnce(strErrMsg, blnSendOnce) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            isFileSendOnce = True
            Exit Function
errProc:
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

            isReceiveZhizhi = False
            blnReceive = False

            Try
                If Me.m_objFlowObject.isReceiveZhizhi(strErrMsg, strUserXM, blnReceive) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            isReceiveZhizhi = True
            Exit Function
errProc:
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

            isSendZhizhi = False
            blnSend = False

            Try
                If Me.m_objFlowObject.isSendZhizhi(strErrMsg, strUserXM, blnSend) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            isSendZhizhi = True
            Exit Function
errProc:
            Exit Function

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

            getNotCompletedTaskData = False
            objJiaoJieData = Nothing

            Try
                If Me.m_objFlowObject.doInitialize(strErrMsg, strUserId, strPassword, strWJBS, False) = False Then
                    GoTo errProc
                End If

                If Me.m_objFlowObject.getNotCompletedTaskData(strErrMsg, strUserXM, objJiaoJieData) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getNotCompletedTaskData = True
            Exit Function
errProc:
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

            getNotCompletedTaskData = False
            objJiaoJieData = Nothing

            Try
                If Me.m_objFlowObject.getNotCompletedTaskData(strErrMsg, strUserXM, objJiaoJieData) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getNotCompletedTaskData = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �����Ƿ���ꣿ
        '     strTaskBLZT          ������״̬
        ' ����
        '     True                 ����
        '     False                ����
        '----------------------------------------------------------------
        Public Overridable Function isTaskComplete(ByVal strTaskBLZT As String) As Boolean
            isTaskComplete = Me.m_objFlowObject.isTaskComplete(strTaskBLZT)
        End Function

        '----------------------------------------------------------------
        ' �Ƿ��˻ص����ˣ�
        '     strTaskStatus        ������״̬
        ' ����
        '     True                 ����
        '     False                ����
        '----------------------------------------------------------------
        Public Overridable Function isTaskTuihui(ByVal strTaskStatus As String) As Boolean
            isTaskTuihui = Me.m_objFlowObject.isTaskTuihui(strTaskStatus)
        End Function

        '----------------------------------------------------------------
        ' �Ƿ��ջص����ˣ�
        '     strTaskStatus        ������״̬
        ' ����
        '     True                 ����
        '     False                ����
        '----------------------------------------------------------------
        Public Overridable Function isTaskShouhui(ByVal strTaskStatus As String) As Boolean
            isTaskShouhui = Me.m_objFlowObject.isTaskShouhui(strTaskStatus)
        End Function

        '----------------------------------------------------------------
        ' �Ƿ�Ϊ֪ͨ�����ˣ�
        '     strTaskStatus        ������״̬
        ' ����
        '     True                 ����
        '     False                ����
        '----------------------------------------------------------------
        Public Overridable Function isTaskTongzhi(ByVal strTaskStatus As String) As Boolean
            isTaskTongzhi = Me.m_objFlowObject.isTaskTongzhi(strTaskStatus)
        End Function

        '----------------------------------------------------------------
        ' �Ƿ�Ϊ�ظ������ˣ�
        '     strTaskStatus        ������״̬
        ' ����
        '     True                 ����
        '     False                ����
        '----------------------------------------------------------------
        Public Overridable Function isTaskHuifu(ByVal strTaskStatus As String) As Boolean
            isTaskHuifu = Me.m_objFlowObject.isTaskHuifu(strTaskStatus)
        End Function

        '----------------------------------------------------------------
        ' �Ƿ�Ϊ�������ˣ�
        '     strTaskBLZL          ����������
        ' ����
        '     True                 ����
        '     False                ����
        '----------------------------------------------------------------
        Public Overridable Function isTaskTingban(ByVal strTaskBLZL As String) As Boolean
            isTaskTingban = Me.m_objFlowObject.isTaskTingban(strTaskBLZL)
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

            isTaskApproved = False
            blnApproved = False

            Try
                If Me.m_objFlowObject.doInitialize(strErrMsg, strUserId, strPassword, strWJBS, False) = False Then
                    GoTo errProc
                End If

                If Me.m_objFlowObject.isTaskApproved(strErrMsg, strBLSY, blnApproved) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            isTaskApproved = True
            Exit Function
errProc:
            Exit Function

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

            isTaskApproved = False
            blnApproved = False

            Try
                If Me.m_objFlowObject.isTaskApproved(strErrMsg, strBLSY, blnApproved) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            isTaskApproved = True
            Exit Function
errProc:
            Exit Function

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

            isHasNotCompleteTongzhi = False
            blnHas = False

            Try
                If Me.m_objFlowObject.doInitialize(strErrMsg, strUserId, strPassword, strWJBS, False) = False Then
                    GoTo errProc
                End If

                If Me.m_objFlowObject.isHasNotCompleteTongzhi(strErrMsg, strUserXM, blnHas) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            isHasNotCompleteTongzhi = True
            Exit Function
errProc:
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

            isHasNotCompleteTongzhi = False
            blnHas = False

            Try
                If Me.m_objFlowObject.isHasNotCompleteTongzhi(strErrMsg, strUserXM, blnHas) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            isHasNotCompleteTongzhi = True
            Exit Function
errProc:
            Exit Function

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

            doAutoReceive = False

            Try
                If Me.m_objFlowObject.doInitialize(strErrMsg, strUserId, strPassword, strWJBS, False) = False Then
                    GoTo errProc
                End If

                If Me.m_objFlowObject.doAutoReceive(strErrMsg, strUserXM) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doAutoReceive = True
            Exit Function
errProc:
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

            doAutoReceive = False

            Try
                If Me.m_objFlowObject.doAutoReceive(strErrMsg, strUserXM) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doAutoReceive = True
            Exit Function
errProc:
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

            getOpinion = False

            Try
                If Me.m_objFlowObject.getOpinion(strErrMsg, objOpinionData, strYJLX, strQSYJ, strBJYJ) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getOpinion = True
            Exit Function
errProc:
            Exit Function

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

            doSetHasReadFile = False

            Try
                If Me.m_objFlowObject.doInitialize(strErrMsg, strUserId, strPassword, strWJBS, False) = False Then
                    GoTo errProc
                End If

                If Me.m_objFlowObject.doSetHasReadFile(strErrMsg, strUserXM) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doSetHasReadFile = True
            Exit Function
errProc:
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

            doSetHasReadFile = False

            Try
                If Me.m_objFlowObject.doSetHasReadFile(strErrMsg, strUserXM) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doSetHasReadFile = True
            Exit Function
errProc:
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

            getFileLocked = False

            Try
                If Me.m_objFlowObject.getFileLocked(strErrMsg, blnLocked, strBMMC, strRYMC) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getFileLocked = True
            Exit Function
errProc:
            Exit Function

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

            doLockFile = False

            Try
                If Me.m_objFlowObject.doLockFile(strErrMsg, strUserId, blnLocked) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doLockFile = True
            Exit Function
errProc:
            Exit Function

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

            Dim objdacXitongpeizhi As New Xydc.Platform.DataAccess.dacXitongpeizhi
            Dim objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty

            doDeleteFile = False

            Try
                '��ȡFTP����
                If objdacXitongpeizhi.getFtpServerParam(strErrMsg, strUserId, strPassword, objFTPProperty) = False Then
                    GoTo errProc
                End If

                'ɾ���ļ�
                If Me.m_objFlowObject.doDeleteFile(strErrMsg, objFTPProperty) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.DataAccess.dacXitongpeizhi.SafeRelease(objdacXitongpeizhi)
            Xydc.Platform.Common.Utilities.FTPProperty.SafeRelease(objFTPProperty)

            doDeleteFile = True
            Exit Function
errProc:
            Xydc.Platform.DataAccess.dacXitongpeizhi.SafeRelease(objdacXitongpeizhi)
            Xydc.Platform.Common.Utilities.FTPProperty.SafeRelease(objFTPProperty)
            Exit Function

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

            doVerifyFile = False

            Try
                If Me.m_objFlowObject.doVerifyFile(strErrMsg, objNewData, objOldData, objenumEditType) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doVerifyFile = True
            Exit Function
errProc:
            Exit Function

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

            doSaveFile = False

            Try
                If Me.m_objFlowObject.doVerifyFile(strErrMsg, objNewData, objOldData, objenumEditType) = False Then
                    GoTo errProc
                End If
                If Me.m_objFlowObject.doSaveFile(strErrMsg, Nothing, objNewData, objOldData, objenumEditType) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doSaveFile = True
            Exit Function
errProc:
            Exit Function

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

            Dim objdacXitongpeizhi As New Xydc.Platform.DataAccess.dacXitongpeizhi
            Dim objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty

            doSaveFile = False

            Try
                '��ȡFTP����
                If objdacXitongpeizhi.getFtpServerParam(strErrMsg, strUserId, strPassword, objFTPProperty) = False Then
                    GoTo errProc
                End If

                '�����ļ�
                If Me.m_objFlowObject.doSaveFileTransaction(strErrMsg, _
                    objNewData, objOldData, objenumEditType, _
                    strGJFile, _
                    objDataSet_FJ, objDataSet_XGWJ, _
                    strUserXM, blnEnforeEdit, _
                    objFTPProperty) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.DataAccess.dacXitongpeizhi.SafeRelease(objdacXitongpeizhi)
            Xydc.Platform.Common.Utilities.FTPProperty.SafeRelease(objFTPProperty)

            doSaveFile = True
            Exit Function
errProc:
            Xydc.Platform.DataAccess.dacXitongpeizhi.SafeRelease(objdacXitongpeizhi)
            Xydc.Platform.Common.Utilities.FTPProperty.SafeRelease(objFTPProperty)
            Exit Function

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

            Dim objdacXitongpeizhi As New Xydc.Platform.DataAccess.dacXitongpeizhi
            Dim objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty

            doSaveFileVariantParam = False

            Try
                '��ȡFTP����
                If objdacXitongpeizhi.getFtpServerParam(strErrMsg, strUserId, strPassword, objFTPProperty) = False Then
                    GoTo errProc
                End If

                '�����ļ�
                If Me.m_objFlowObject.doSaveFileTransactionVariantParam(strErrMsg, _
                    objNewData, objOldData, objenumEditType, _
                    strGJFile, _
                    objDataSet_FJ, objDataSet_XGWJ, _
                    strUserXM, blnEnforeEdit, _
                    objFTPProperty, _
                    objParams) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.DataAccess.dacXitongpeizhi.SafeRelease(objdacXitongpeizhi)
            Xydc.Platform.Common.Utilities.FTPProperty.SafeRelease(objFTPProperty)

            doSaveFileVariantParam = True
            Exit Function
errProc:
            Xydc.Platform.DataAccess.dacXitongpeizhi.SafeRelease(objdacXitongpeizhi)
            Xydc.Platform.Common.Utilities.FTPProperty.SafeRelease(objFTPProperty)
            Exit Function

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

            Dim objdacXitongpeizhi As New Xydc.Platform.DataAccess.dacXitongpeizhi
            Dim objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty

            doSaveFileZDBC = False

            Try
                '��ȡFTP����
                If objdacXitongpeizhi.getFtpServerParam(strErrMsg, strUserId, strPassword, objFTPProperty) = False Then
                    GoTo errProc
                End If

                '�����ļ�
                If Me.m_objFlowObject.doSaveFileTransactionZDBC(strErrMsg, _
                    strGJFile, _
                    objDataSet_FJ, objDataSet_XGWJ, _
                    strUserXM, blnEnforeEdit, _
                    objFTPProperty) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.DataAccess.dacXitongpeizhi.SafeRelease(objdacXitongpeizhi)
            Xydc.Platform.Common.Utilities.FTPProperty.SafeRelease(objFTPProperty)

            doSaveFileZDBC = True
            Exit Function
errProc:
            Xydc.Platform.DataAccess.dacXitongpeizhi.SafeRelease(objdacXitongpeizhi)
            Xydc.Platform.Common.Utilities.FTPProperty.SafeRelease(objFTPProperty)
            Exit Function

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

            Dim objdacXitongpeizhi As New Xydc.Platform.DataAccess.dacXitongpeizhi
            Dim objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty

            doSaveFileZDBCVariantParam = False

            Try
                '��ȡFTP����
                If objdacXitongpeizhi.getFtpServerParam(strErrMsg, strUserId, strPassword, objFTPProperty) = False Then
                    GoTo errProc
                End If

                '�����ļ�
                If Me.m_objFlowObject.doSaveFileTransactionZDBCVariantParam(strErrMsg, _
                    strGJFile, _
                    objDataSet_FJ, objDataSet_XGWJ, _
                    strUserXM, blnEnforeEdit, _
                    objFTPProperty, _
                    objParams) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.DataAccess.dacXitongpeizhi.SafeRelease(objdacXitongpeizhi)
            Xydc.Platform.Common.Utilities.FTPProperty.SafeRelease(objFTPProperty)

            doSaveFileZDBCVariantParam = True
            Exit Function
errProc:
            Xydc.Platform.DataAccess.dacXitongpeizhi.SafeRelease(objdacXitongpeizhi)
            Xydc.Platform.Common.Utilities.FTPProperty.SafeRelease(objFTPProperty)
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

            doSaveFujian = False

            Try
                If Me.m_objFlowObject.doSaveFujian(strErrMsg, blnEnforeEdit, strUserXM, objNewData) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doSaveFujian = True
            Exit Function
errProc:
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

            doSaveFujian = False

            Try
                If Me.m_objFlowObject.doSaveFujian(strErrMsg, blnEnforeEdit, strUserXM, objNewData) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doSaveFujian = True
            Exit Function
errProc:
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

            doSaveXgwj = False

            Try
                If Me.m_objFlowObject.doSaveXgwj(strErrMsg, blnEnforeEdit, strUserXM, objNewData) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doSaveXgwj = True
            Exit Function
errProc:
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

            doSaveXgwjFujian = False

            Try
                If Me.m_objFlowObject.doSaveXgwjFujian(strErrMsg, blnEnforeEdit, strUserXM, objNewData) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doSaveXgwjFujian = True
            Exit Function
errProc:
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

            doVerifyFujian = False

            Try
                If Me.m_objFlowObject.doVerifyFujian(strErrMsg, objNewData) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doVerifyFujian = True
            Exit Function
errProc:
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

            doVerifyXgwjFujian = False

            Try
                If Me.m_objFlowObject.doVerifyXgwjFujian(strErrMsg, objNewData) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doVerifyXgwjFujian = True
            Exit Function
errProc:
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

            doDeleteData_FJ = False

            Try
                If Me.m_objFlowObject.doDeleteData_FJ(strErrMsg, objOldData) = False Then
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
                If Me.m_objFlowObject.doDeleteData_XGWJ(strErrMsg, objOldData) = False Then
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
            With Me.m_objFlowObject
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
                If Me.m_objFlowObject.doMoveTo_FJ(strErrMsg, objSrcData, objDesData) = False Then
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
                If Me.m_objFlowObject.doMoveTo_XGWJ(strErrMsg, objSrcData, objDesData) = False Then
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
                If Me.m_objFlowObject.doAutoAdjustXSXH_FJ(strErrMsg, objFJData) = False Then
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
                If Me.m_objFlowObject.doAutoAdjustXSXH_XGWJ(strErrMsg, objXGWJData) = False Then
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
                If Me.m_objFlowObject.getWorkflowFileData(strErrMsg, strUserXM, strWhere, objFileDataSet) = False Then
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
                If Me.m_objFlowObject.doSendBuyueJJD(strErrMsg, Nothing, strSender, strReceiver) = False Then
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
                If Me.m_objFlowObject.getWeituoren(strErrMsg, strUserXM, strWTR) = False Then
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
                If Me.m_objFlowObject.getLastZJBJiaojieData(strErrMsg, strUserXM, objJiaoJieData) = False Then
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
                If Me.m_objFlowObject.doSend(strErrMsg, objJSRDataSet, strFSXH, strYJJH, intBLJB, strAddedJJXHList) = False Then
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
                If Me.m_objFlowObject.doSetTaskBWTX(strErrMsg, strBLR, blnBWTX) = False Then
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
                If Me.m_objFlowObject.doSetTaskComplete(strErrMsg, strBLR) = False Then
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
                If Me.m_objFlowObject.doSetTaskComplete(strErrMsg, strBLR, strNewJJXHList) = False Then
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
                If Me.m_objFlowObject.doSendReply(strErrMsg, strBLR, intMaxJJXH, strFSXH, strAddedJJXHList) = False Then
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
                If Me.m_objFlowObject.doDeleteJiaojie(strErrMsg, strAddedJJXHList) = False Then
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
                If Me.m_objFlowObject.getMaxJJXH(strErrMsg, intMaxJJXH) = False Then
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
                If Me.m_objFlowObject.getJieshouDataSet(strErrMsg, strUserXM, strWhere, objJieshouDataSet) = False Then
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
                If Me.m_objFlowObject.doReceiveFile(strErrMsg, objJiaojieData) = False Then
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
                If Me.m_objFlowObject.doTranslateTask(strErrMsg, strOldBlsy, strNewBlsy) = False Then
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
                If Me.m_objFlowObject.doTuihuiFile(strErrMsg, strYBLSY, strYXB, strFSXH, objJiaojieData, blnCanReadFile, objHasSendNoticeRY) = False Then
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
                If Me.m_objFlowObject.getShouhuiDataSet(strErrMsg, strUserXM, strWhere, objShouhuiDataSet) = False Then
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
                If Me.m_objFlowObject.doShouhuiFile(strErrMsg, strFSXH, objJiaojieData, blnSendNotice, objHasSendNoticeRY) = False Then
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
                If Me.m_objFlowObject.isEditFile(strErrMsg, strUserXM, blnDo) = False Then
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
                If Me.m_objFlowObject.getTuihuiDataSet(strErrMsg, strUserXM, strWhere, objTuihuiDataSet) = False Then
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
                If Me.m_objFlowObject.doIQiyongFile(strErrMsg, strUserXM) = False Then
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
                If Me.m_objFlowObject.doIZuofeiFile(strErrMsg, strUserXM) = False Then
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
                If Me.m_objFlowObject.doIContinueFile(strErrMsg, strUserXM) = False Then
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
                If Me.m_objFlowObject.doIStopFile(strErrMsg, strUserXM) = False Then
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
                If Me.m_objFlowObject.doIReadFile(strErrMsg, strUserXM) = False Then
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
                If Me.m_objFlowObject.doIDoNotProcess(strErrMsg, strUserXM) = False Then
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
                If Me.m_objFlowObject.doICompleteTask(strErrMsg, strUserXM) = False Then
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
                If Me.m_objFlowObject.getUncompleteTaskRY(strErrMsg, strUserXM, strUserList) = False Then
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
                If Me.m_objFlowObject.doCompleteFile(strErrMsg, strUserXM) = False Then
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
                If Me.m_objFlowObject.getPJYJ(strErrMsg, strPJYJ) = False Then
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
                If Me.m_objFlowObject.doImportQP(strErrMsg, strFileSpec) = False Then
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
                If Me.m_objFlowObject.getZSWJ(strErrMsg, strZSWJ) = False Then
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
                If Me.m_objFlowObject.doImportZS(strErrMsg, strFileSpec) = False Then
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
                If Me.m_objFlowObject.getKeCuibanData(strErrMsg, strUserXM, objKeCuibanData) = False Then
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
                If Me.m_objFlowObject.getCuibanData(strErrMsg, strUserXM, objCuibanData) = False Then
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
                If Me.m_objFlowObject.getCuibanData(strErrMsg, objCuibanData) = False Then
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
                'У��
                If Me.m_objFlowObject.doVerifyCuiban(strErrMsg, objOldData, objNewData) = False Then
                    GoTo errProc
                End If

                '����
                If Me.m_objFlowObject.doSaveCuiban(strErrMsg, Nothing, objOldData, objNewData) = False Then
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
                If Me.m_objFlowObject.getDubanData(strErrMsg, strUserXM, objDubanData) = False Then
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
                If Me.m_objFlowObject.getDubanData(strErrMsg, objDubanData) = False Then
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
                If Me.m_objFlowObject.getKeDubanData(strErrMsg, strUserXM, objKeDubanData) = False Then
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
                'У��
                If Me.m_objFlowObject.doVerifyDuban(strErrMsg, objOldData, objNewData) = False Then
                    GoTo errProc
                End If

                '����
                If Me.m_objFlowObject.doSaveDuban(strErrMsg, Nothing, objOldData, objNewData) = False Then
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
                If Me.m_objFlowObject.getBeidubanData(strErrMsg, strUserXM, objBeidubanData) = False Then
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
                If Me.m_objFlowObject.doSaveDuban(strErrMsg, Nothing, intJJXH, intDBXH, strDBJG) = False Then
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
                If Me.m_objFlowObject.getLZQKDataSet(strErrMsg, strUserXM, strWhere, objJiaoJieData) = False Then
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
                If Me.m_objFlowObject.getCaozuorizhiData(strErrMsg, strWhere, objCaozuorizhiData) = False Then
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
                If Me.m_objFlowObject.getBuyueData(strErrMsg, strCksyList, strWhere, objBuyueData) = False Then
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
                If Me.m_objFlowObject.getBuyueSendData(strErrMsg, strUserXM, strWhere, objBuyueData) = False Then
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
                If Me.m_objFlowObject.getBuyueRecvData(strErrMsg, strUserXM, strWhere, objBuyueData) = False Then
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
                If Me.m_objFlowObject.doSendBuyueRequest(strErrMsg, Nothing, strFSXH, strSender, strReceiver, strJJSM) = False Then
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
                If Me.m_objFlowObject.doSendBuyueTongzhi(strErrMsg, Nothing, strFSXH, strSender, strReceiver, strJJSM) = False Then
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
                If Me.m_objFlowObject.doShouhuiBuyueRequest(strErrMsg, Nothing, intJJXH) = False Then
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
                If Me.m_objFlowObject.doShouhuiBuyueTongzhi(strErrMsg, Nothing, intJJXH) = False Then
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
                If Me.m_objFlowObject.doPizhunBuyueRequest(strErrMsg, Nothing, intJJXH, strFSXH) = False Then
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
                If Me.m_objFlowObject.doJujueBuyueRequest(strErrMsg, Nothing, intJJXH, strFSXH) = False Then
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
                If Me.m_objFlowObject.doZhuanfaBuyueRequest(strErrMsg, Nothing, intJJXH, strFSXH, strZFJSR) = False Then
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
                If Me.m_objFlowObject.doReadBuyueTongzhi(strErrMsg, intJJXH) = False Then
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
                If Me.m_objFlowObject.getAllJsrSql(strErrMsg, strUserXM, strSQL) = False Then
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
                If Me.m_objFlowObject.doQianminQueren(strErrMsg, strYjlx, strSPR, intMode) = False Then
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
                If Me.m_objFlowObject.doQianminCancel(strErrMsg, strYjlx, strSPR) = False Then
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
                If Me.m_objFlowObject.getAllYjlx(strErrMsg, objYjlx) = False Then
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
                If Me.m_objFlowObject.getKeBudengLingdao(strErrMsg, strUserXM, strList) = False Then
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
                If Me.m_objFlowObject.getLastSpsyJiaojieData(strErrMsg, strUserXM, blnZTXZ, objJiaoJieData) = False Then
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
                If Me.m_objFlowObject.doSaveSpyj(strErrMsg, Nothing, intJJXH, objNewData) = False Then
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
                If Me.m_objFlowObject.doBanliCancel(strErrMsg, Nothing, intJJXH) = False Then
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
                If Me.m_objFlowObject.getBanliData(strErrMsg, intJJXH, objBanliData) = False Then
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
            doTranslateSFPZ = Me.m_objFlowObject.doTranslateSFPZ(strSFPZ)
        End Function

        '----------------------------------------------------------------
        ' ��ȡ����׼�������־
        ' ����
        '                          �������־
        '----------------------------------------------------------------
        Public Overridable Function getPizhunBLBZ() As String
            getPizhunBLBZ = Me.m_objFlowObject.getPizhunBLBZ()
        End Function

        '----------------------------------------------------------------
        ' ��ȡ����������������־
        ' ����
        '                          �������־
        '----------------------------------------------------------------
        Public Overridable Function getBaocunYijianBLBZ() As String
            getBaocunYijianBLBZ = Me.m_objFlowObject.getBaocunYijianBLBZ()
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
                If Me.m_objFlowObject.isNeedQianminQuerenPrompt(strErrMsg, strYjlx, strSPR, blnNeed, strXyrList) = False Then
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
            isQianminTask = Me.m_objFlowObject.isQianminTask(strYjlx)
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
            isFileQianminTask = Me.m_objFlowObject.isFileQianminTask(strYjlx, strPrompt)
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
                If Me.m_objFlowObject.isAllTaskComplete(strErrMsg, strUserXM, blnComplete) = False Then
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
                If Me.m_objFlowObject.getFujianData(strErrMsg, blnZSWJ, strFJNR) = False Then
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
                If Me.m_objFlowObject.getFujianData(strErrMsg, strFJNR) = False Then
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
            With Me.m_objFlowObject
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
            With Me.m_objFlowObject
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
            With Me.m_objFlowObject
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
            With Me.m_objFlowObject
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
            With Me.m_objFlowObject
                doSaveData_Banli = .doSaveData_Banli(strErrMsg, objOldData, objNewData, objenumEditType)
            End With
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
                If Me.m_objFlowObject.doExportToExcel(strErrMsg, objDataSet, strExcelFile, strMacroName, strMacroValue) = False Then
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
            getDefaultYJNR = Me.m_objFlowObject.getDefaultYJNR(strYJLX)
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
                If Me.m_objFlowObject.getSenderList(strErrMsg, strUserXM, strSenderList) = False Then
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
                If Me.m_objFlowObject.doAddToAnjuan(strErrMsg, strUserId, strPassword, strAJBS, strTempPath) = False Then
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
                If Me.m_objFlowObject.doWriteUserLog(strErrMsg, strUserId, strPassword, strAddress, strMachine, strCZMS) = False Then
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
                If Me.m_objFlowObject.doWriteUserLog_Fujian(strErrMsg, strUserId, strPassword, strAddress, strMachine, objNewFJData, objOldFJData) = False Then
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
                If Me.m_objFlowObject.doWriteUserLog_XGWJ(strErrMsg, strUserId, strPassword, strAddress, strMachine, objNewXGWJData, objOldXGWJData) = False Then
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
                If Me.m_objFlowObject.doSetJiaojieXBBZ(strErrMsg, Nothing, strUserXM, strNewXBBZ) = False Then
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
                If Me.m_objFlowObject.doWriteXSXH(strErrMsg) = False Then
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
            getYijiaoData = Xydc.Platform.DataAccess.FlowObject.getYijiaoData(strErrMsg, strUserId, strPassword, strYJR, strJSR, strWhere, objYijiaoData)
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
            getJieshouData = Xydc.Platform.DataAccess.FlowObject.getJieshouData(strErrMsg, strUserId, strPassword, strYJR, strJSR, strWhere, objJieshouData)
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
            getYjrListData = Xydc.Platform.DataAccess.FlowObject.getYjrListData(strErrMsg, strUserId, strPassword, strJSR, objYjrData)
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
            doFile_Yijiao = Xydc.Platform.DataAccess.FlowObject.doFile_Yijiao(strErrMsg, strUserId, strPassword, strYJR, strJSR, strWJBS, strYJSM)
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
            getWJLX = Xydc.Platform.DataAccess.FlowObject.getWJLX(strErrMsg, strUserId, strPassword, strWJBS, strWJLX)
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
            doFile_Jieshou = Xydc.Platform.DataAccess.FlowObject.doFile_Jieshou(strErrMsg, strUserId, strPassword, strYJR, strJSR, strWJBS)
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
            With Me.m_objFlowObject
                doUpdateWJXX = .doUpdateWJXX(strErrMsg, objNewData)
            End With

        End Function

    End Class 'rulesFlowObject

End Namespace 'Xydc.Platform.BusinessRules
