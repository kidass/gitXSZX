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

Namespace Xydc.Platform.Common.Workflow

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.Common.Workflow
    ' ����    ��BaseFlowObject
    '
    ' ���������� 
    '     ������FlowObject��صĲ���
    '----------------------------------------------------------------
    Public Class BaseFlowObject
        Implements IDisposable

        '�������͡����󴴽��ӿ�ע����(���ж�����)
        Private Shared m_objFlowTypeEnum As System.Collections.Specialized.ListDictionary

        '��������
        Private m_strFlowType As String          '����������(����Ψһ)
        Private m_strFlowTypeName As String      '��������������(����Ψһ)
        Private m_strFlowTypeBLLX As String      '�����������Ӧ�İ�������

        '������Է���
        Private m_strWJBS As String              '�ļ���ʶ
        Private m_strLSH As String               '�ļ���ˮ��
        Private m_strStatus As String            '�ļ�����״̬
        Private m_strPZR As String               '�ļ�������׼��
        Private m_objPZRQ As DateTime            '�ļ������׼����
        Private m_intDDSZ As Integer             '���ļ�������ת������������

        '�ļ�����״̬
        Public Const FILESTATUS_ZJB As String = "���ڰ���"
        Public Const FILESTATUS_YWC As String = "�������"
        Public Const FILESTATUS_YTB As String = "�ݻ�����"
        Public Const FILESTATUS_YZF As String = "�ļ�����"
        Public Const FILESTATUS_YQF As String = "�Ѿ�ǩ��"
        Public Const FILESTATUS_YQP As String = "�Ѿ�ǩ��"
        Public Const FILESTATUS_YPS As String = "�Ѿ���ʾ"
        Public Const FILESTATUS_YDJ As String = "�����ĺ�"
        Public Const FILESTATUS_YDG As String = "�Ѿ�����"

        '���Ӵ���״̬
        Public Const TASKSTATUS_WJS As String = "û�н���"
        Public Const TASKSTATUS_ZJB As String = "���ڰ���"
        Public Const TASKSTATUS_YTB As String = "�ݻ�����"
        Public Const TASKSTATUS_YWC As String = "�������"
        Public Const TASKSTATUS_BYB As String = "���ð���"
        Public Const TASKSTATUS_YYD As String = "�Ѿ��Ķ�"
        Public Const TASKSTATUS_BSH As String = "�ļ����ջ�"
        Public Const TASKSTATUS_BTH As String = "�ļ����˻�"

        '��������
        Public Const FILEZTLX_ZZ As String = "ֽ"
        Public Const FILEZTLX_DZ As String = "����"
        Public Const FILEZTLX_ZD As String = "ֽ+����"

        '������һ������
        Public Const TASK_HFCL As String = "�ظ�����"
        Public Const TASK_HFTZ As String = "�ظ�֪ͨ"
        Public Const TASK_BYQQ As String = "��������"
        Public Const TASK_BYTZ As String = "����֪ͨ"
        Public Const TASK_THCL As String = "�˻ش���"
        Public Const TASK_THTZ As String = "�˻�֪ͨ"
        Public Const TASK_SHCL As String = "�ջش���"
        Public Const TASK_SHTZ As String = "�ջ�֪ͨ"
        Public Const TASK_SMCL As String = "˾�ش���"
        Public Const TASK_MSCL As String = "���鴦��"
        Public Const TASK_LDCL As String = "�����ļ�"
        Public Const TASK_XGCL As String = "��ش���"
        Public Const TASK_CBWJ As String = "�߰��ļ�"
        Public Const TASK_DBWJ As String = "�����ļ�"

        'ǿ�б༭˵��
        Public Const LOGO_QXBJ As String = "�ļ������ǿ�ƽ����޸Ĳ�����"




        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Protected Sub New()
            MyBase.New()

            '��ʼ������������ͨ������
            m_strFlowType = ""
            m_strWJBS = ""
            m_strLSH = ""
            m_strStatus = ""
            m_intDDSZ = 0
            m_strPZR = ""
            m_objPZRQ = Nothing

        End Sub

        '----------------------------------------------------------------
        ' �������캯��
        '----------------------------------------------------------------
        Protected Sub New(ByVal strFlowType As String)
            Me.New()
            'ע����
            Dim strType As String
            Try
                strType = strFlowType
                If m_objFlowTypeEnum Is Nothing Then
                    Throw New Exception("��������[Create]��������[" + strFlowType + "]��������")
                Else
                    If m_objFlowTypeEnum.Item(strType) Is Nothing Then
                        Throw New Exception("��������[Create]��������[" + strFlowType + "]��������")
                    End If
                End If
            Catch ex As Exception
                Throw ex
            End Try

        End Sub




        '----------------------------------------------------------------
        ' ��������
        '----------------------------------------------------------------
        Public Overridable Sub Dispose() Implements System.IDisposable.Dispose
            Dispose(True)
        End Sub

        '----------------------------------------------------------------
        ' �ͷű�����Դ
        '----------------------------------------------------------------
        Protected Sub Dispose(ByVal disposing As Boolean)
            If (Not disposing) Then
                Exit Sub
            End If
        End Sub

        '----------------------------------------------------------------
        ' ��ȫ�ͷű�����Դ
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.Common.Workflow.BaseFlowObject)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub










        '----------------------------------------------------------------
        ' BaseFlow����ע����
        '     strFlowType          �����������ʹ���
        '     objCreator           ������������IBaseFlowCreate�ӿ�
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Shared Function RegisterFlowType( _
            ByVal strFlowType As String, _
            ByVal objCreator As Xydc.Platform.Common.Workflow.IBaseFlowCreate) As Boolean

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
                If objCreator Is Nothing Then
                    Throw New Exception("����[IBaseFlowCreate]����Ϊ�գ�")
                End If

                '�������ͻ㼯��
                If m_objFlowTypeEnum Is Nothing Then
                    m_objFlowTypeEnum = New System.Collections.Specialized.ListDictionary
                End If

                '��������Ƿ����
                If Not (m_objFlowTypeEnum.Item(strFlowType) Is Nothing) Then
                    Exit Try
                End If

                'ע��
                m_objFlowTypeEnum.Add(strFlowType, objCreator)

                RegisterFlowType = True

            Catch ex As Exception
                Throw ex
            End Try

        End Function

        '----------------------------------------------------------------
        ' ����BaseFlow
        '     strFlowType          �����������ʹ���
        ' ����
        '                          ��Xydc.Platform.Common.Workflow.BaseFlowObject����
        '----------------------------------------------------------------
        Public Shared Function Create(ByVal strFlowType As String) As Xydc.Platform.Common.Workflow.BaseFlowObject

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

                'ע����ʵ�ֵ�BaseFlow
                Dim strType As String

               
                '***********************************************************************************************
                '���鵥
                'strType = Xydc.Platform.Common.Workflow.BaseFlowDuchadan.FLOWCODE
                'If m_objFlowTypeEnum Is Nothing Then
                '    RegisterFlowType(strType, New Xydc.Platform.Common.Workflow.BaseFlowDuchadanCreator)
                'Else
                '    If m_objFlowTypeEnum.Item(strType) Is Nothing Then
                '        RegisterFlowType(strType, New Xydc.Platform.Common.Workflow.BaseFlowDuchadanCreator)
                '    End If
                'End If



                '��ȡ�ӿ�
                Dim objCreator As Object
                objCreator = m_objFlowTypeEnum.Item(strFlowType)
                If objCreator Is Nothing Then
                    Throw New Exception("����[" + strFlowType + "]��֧�֣�")
                End If
                Dim objIBaseFlowCreate As Xydc.Platform.Common.Workflow.IBaseFlowCreate
                objIBaseFlowCreate = CType(objCreator, Xydc.Platform.Common.Workflow.IBaseFlowCreate)
                If objIBaseFlowCreate Is Nothing Then
                    Throw New Exception("����[" + strFlowType + "]��֧�֣�")
                End If

                '���ýӿڴ�������
                Create = objIBaseFlowCreate.Create(strFlowType)

                '�Զ�������������
                Create.m_strFlowType = strFlowType

            Catch ex As Exception
                Throw ex
            End Try

        End Function




        '----------------------------------------------------------------
        ' FlowType����
        '----------------------------------------------------------------
        Public Property FlowType() As String
            Get
                FlowType = m_strFlowType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strFlowType = Value
                Catch ex As Exception
                    m_strFlowType = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' FlowTypeName����
        '----------------------------------------------------------------
        Public Property FlowTypeName() As String
            Get
                FlowTypeName = m_strFlowTypeName
            End Get
            Set(ByVal Value As String)
                Try
                    m_strFlowTypeName = Value
                Catch ex As Exception
                    m_strFlowTypeName = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' FlowTypeBLLX����
        '----------------------------------------------------------------
        Public Property FlowTypeBLLX() As String
            Get
                FlowTypeBLLX = m_strFlowTypeBLLX
            End Get
            Set(ByVal Value As String)
                Try
                    m_strFlowTypeBLLX = Value
                Catch ex As Exception
                    m_strFlowTypeBLLX = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' WJBS����
        '----------------------------------------------------------------
        Public Property WJBS() As String
            Get
                WJBS = m_strWJBS
            End Get
            Set(ByVal Value As String)
                m_strWJBS = Value
            End Set
        End Property

        '----------------------------------------------------------------
        ' LSH����
        '----------------------------------------------------------------
        Public Property LSH() As String
            Get
                LSH = m_strLSH
            End Get
            Set(ByVal Value As String)
                m_strLSH = Value
            End Set
        End Property

        '----------------------------------------------------------------
        ' Status����
        '----------------------------------------------------------------
        Public Property Status() As String
            Get
                Status = m_strStatus
            End Get
            Set(ByVal Value As String)
                m_strStatus = Value
            End Set
        End Property

        '----------------------------------------------------------------
        ' PZR����
        '----------------------------------------------------------------
        Public Property PZR() As String
            Get
                PZR = m_strPZR
            End Get
            Set(ByVal Value As String)
                m_strPZR = Value
            End Set
        End Property

        '----------------------------------------------------------------
        ' PZRQ����
        '----------------------------------------------------------------
        Public Property PZRQ() As System.DateTime
            Get
                PZRQ = m_objPZRQ
            End Get
            Set(ByVal Value As System.DateTime)
                m_objPZRQ = Value
            End Set
        End Property

        '----------------------------------------------------------------
        ' DDSZ����
        '----------------------------------------------------------------
        Public Property DDSZ() As Integer
            Get
                DDSZ = m_intDDSZ
            End Get
            Set(ByVal Value As Integer)
                m_intDDSZ = Value
            End Set
        End Property




        '----------------------------------------------------------------
        ' ��ȡ���Ѿ�������ϡ��Ľ���״̬SQLֵ�б� - �����������
        '----------------------------------------------------------------
        Public Shared ReadOnly Property TaskStatusYWCList() As String
            Get
                TaskStatusYWCList = ""
                TaskStatusYWCList = TaskStatusYWCList + " " + "'" + TASKSTATUS_YWC + "'"
                TaskStatusYWCList = TaskStatusYWCList + "," + "'" + TASKSTATUS_BYB + "'"
                TaskStatusYWCList = TaskStatusYWCList + "," + "'" + TASKSTATUS_YYD + "'"
                TaskStatusYWCList = TaskStatusYWCList + "," + "'" + TASKSTATUS_BSH + "'"
                TaskStatusYWCList = TaskStatusYWCList + "," + "'" + TASKSTATUS_BTH + "'"
            End Get
        End Property

        '----------------------------------------------------------------
        ' ��ȡ���Ѿ�������ϡ��Ľ���״̬SQLֵ�б� - ���а�����ɵ�״̬
        '----------------------------------------------------------------
        Public Shared ReadOnly Property TaskStatusAllYWCList() As String
            Get
                TaskStatusAllYWCList = ""
                TaskStatusAllYWCList = TaskStatusAllYWCList + " " + "'" + TASKSTATUS_YWC + "'"
                TaskStatusAllYWCList = TaskStatusAllYWCList + "," + "'" + TASKSTATUS_BYB + "'"
                TaskStatusAllYWCList = TaskStatusAllYWCList + "," + "'" + TASKSTATUS_YYD + "'"
                TaskStatusAllYWCList = TaskStatusAllYWCList + "," + "'" + TASKSTATUS_BSH + "'"
                TaskStatusAllYWCList = TaskStatusAllYWCList + "," + "'" + TASKSTATUS_BTH + "'"
                TaskStatusAllYWCList = TaskStatusAllYWCList + "," + "'" + TASKSTATUS_YTB + "'"
            End Get
        End Property

        '----------------------------------------------------------------
        ' ��ȡ���Ѿ��ݻ������Ľ���״̬SQLֵ�б�
        '----------------------------------------------------------------
        Public Shared ReadOnly Property TaskStatusYTBList() As String
            Get
                TaskStatusYTBList = ""
                TaskStatusYTBList = TaskStatusYTBList + "'" + TASKSTATUS_YTB + "'"
            End Get
        End Property

        '----------------------------------------------------------------
        ' ��ȡ��û�н��ա��Ľ���״̬SQLֵ�б�
        '----------------------------------------------------------------
        Public Shared ReadOnly Property TaskStatusWJSList() As String
            Get
                TaskStatusWJSList = ""
                TaskStatusWJSList = TaskStatusWJSList + "'" + TASKSTATUS_WJS + "'"
            End Get
        End Property

        '----------------------------------------------------------------
        ' ��ȡ�����ڰ����Ľ���״̬SQLֵ�б�
        '----------------------------------------------------------------
        Public Shared ReadOnly Property TaskStatusZJBList() As String
            Get
                TaskStatusZJBList = ""
                'TaskStatusZJBList = TaskStatusZJBList + "'" + TASKSTATUS_WJS + "'"

                TaskStatusZJBList = TaskStatusZJBList + "'" + TASKSTATUS_ZJB + "'"

            End Get
        End Property




        '----------------------------------------------------------------
        ' ��ȡ������֪ͨ���Ľ���״̬SQLֵ�б�
        '----------------------------------------------------------------
        Public Overridable ReadOnly Property TaskStatusZDTZList() As String
            Get
                TaskStatusZDTZList = ""
                TaskStatusZDTZList = TaskStatusZDTZList + Xydc.Platform.Common.Data.FlowData.YJJH_ZHUDONGBUYUE.ToString()
            End Get
        End Property

        '----------------------------------------------------------------
        ' ��ȡ���������ˡ��İ�������SQLֵ�б�
        '----------------------------------------------------------------
        Public Overridable ReadOnly Property TaskBlzlSPSYList() As String
            Get
                TaskBlzlSPSYList = ""
                TaskBlzlSPSYList = TaskBlzlSPSYList + " " + "'" + TASK_LDCL + "'"
                TaskBlzlSPSYList = TaskBlzlSPSYList + "," + "'" + TASK_XGCL + "'"
            End Get
        End Property

        '----------------------------------------------------------------
        ' ��ȡ���������ˡ��İ�������SQLֵ�б�
        '----------------------------------------------------------------
        Public Overridable ReadOnly Property TaskBlzlBYSYList() As String
            Get
                TaskBlzlBYSYList = ""
                TaskBlzlBYSYList = TaskBlzlBYSYList + " " + "'" + TASK_BYQQ + "'"
                TaskBlzlBYSYList = TaskBlzlBYSYList + "," + "'" + TASK_BYTZ + "'"
            End Get
        End Property




        '----------------------------------------------------------------
        ' ��ȡ���Ѿ�������ϡ����ļ�״̬SQLֵ�б� - ����������ϵ�״̬
        '----------------------------------------------------------------
        Public Shared ReadOnly Property FileStatusYWCList() As String
            Get
                FileStatusYWCList = ""
                FileStatusYWCList = FileStatusYWCList + " " + "'" + FILESTATUS_YWC + "'"
            End Get
        End Property

        '----------------------------------------------------------------
        ' ��ȡ���Ѿ�������ϡ����ļ�״̬SQLֵ�б� - ���а�����ϵ�״̬
        '----------------------------------------------------------------
        Public Shared ReadOnly Property FileStatusAllYWCList() As String
            Get
                FileStatusAllYWCList = ""
                FileStatusAllYWCList = FileStatusAllYWCList + " " + "'" + FILESTATUS_YWC + "'"
                FileStatusAllYWCList = FileStatusAllYWCList + "," + "'" + FILESTATUS_YTB + "'"
                FileStatusAllYWCList = FileStatusAllYWCList + "," + "'" + FILESTATUS_YZF + "'"
            End Get
        End Property

        '----------------------------------------------------------------
        ' ��ȡ���Ѿ��ݻ��������ļ�״̬SQLֵ�б�
        '----------------------------------------------------------------
        Public Shared ReadOnly Property FileStatusYTBList() As String
            Get
                FileStatusYTBList = ""
                FileStatusYTBList = FileStatusYTBList + "'" + FILESTATUS_YTB + "'"
            End Get
        End Property

        '----------------------------------------------------------------
        ' ��ȡ���Ѿ����ϡ����ļ�״̬SQLֵ�б�
        '----------------------------------------------------------------
        Public Shared ReadOnly Property FileStatusYZFList() As String
            Get
                FileStatusYZFList = ""
                FileStatusYZFList = FileStatusYZFList + "'" + FILESTATUS_YZF + "'"
            End Get
        End Property

        '----------------------------------------------------------------
        ' ��ȡ���Ѿ�ǩ�������ļ�״̬SQLֵ�б�
        '----------------------------------------------------------------
        Public Shared ReadOnly Property FileStatusYQFList() As String
            Get
                FileStatusYQFList = ""
                FileStatusYQFList = FileStatusYQFList + " " + "'" + FILESTATUS_YQF + "'"
                FileStatusYQFList = FileStatusYQFList + "," + "'" + FILESTATUS_YQP + "'"
                FileStatusYQFList = FileStatusYQFList + "," + "'" + FILESTATUS_YPS + "'"
                FileStatusYQFList = FileStatusYQFList + "," + "'" + FILESTATUS_YDJ + "'"
                FileStatusYQFList = FileStatusYQFList + "," + "'" + FILESTATUS_YDG + "'"
            End Get
        End Property

        '----------------------------------------------------------------
        ' ��ȡ���Ѿ����塱���ļ�״̬SQLֵ�б�
        '----------------------------------------------------------------
        Public Shared ReadOnly Property FileStatusYDGList() As String
            Get
                FileStatusYDGList = ""
                FileStatusYDGList = FileStatusYDGList + " " + "'" + FILESTATUS_YQF + "'"
                FileStatusYDGList = FileStatusYDGList + "," + "'" + FILESTATUS_YQP + "'"
                FileStatusYDGList = FileStatusYDGList + "," + "'" + FILESTATUS_YPS + "'"
                FileStatusYDGList = FileStatusYDGList + "," + "'" + FILESTATUS_YDJ + "'"
                FileStatusYDGList = FileStatusYDGList + "," + "'" + FILESTATUS_YDG + "'"
            End Get
        End Property

    End Class

End Namespace