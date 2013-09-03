Option Strict On
Option Explicit On 

Imports System
Imports System.IO
Imports System.Data

Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.web
    ' ����    ��PersonConfig
    '
    ' ���������� 
    '   ��������˵����в�������
    '----------------------------------------------------------------

    Public Class PersonConfig
        Implements IDisposable

        '�ڲ�����
        Private Const TABLENAME As String = "PersonalProfile"
        Private Const F_GRQDXX As String = "��������ѡ��"
        Private Const F_ZTSXKG As String = "״̬ˢ�¿���"
        Private Const F_ZTSXJG As String = "״̬ˢ�¼��"
        Private Const F_TZSXKG As String = "֪ͨˢ�¿���"
        Private Const F_TZSXJG As String = "֪ͨˢ�¼��"
        Private Const F_LTSXKG As String = "����ˢ�¿���"
        Private Const F_LTSXJG As String = "����ˢ�¼��"

        '�������
        '    �û���ʶ
        Private m_strUserName As String
        '    �����ļ�·��
        Private m_strFilePath As String

        '�������
        '    ����ѡ��: 0-�����棬1-��������
        Private m_intStartupOption As Integer
        '    ״̬ˢ�¿���: 1-����0-��
        Private m_blnStatusRefreshSwitch As Boolean
        '    ״̬ˢ�¼��, ��λ��
        Private m_intStatusRefreshTime As Integer
        '    ֪ͨˢ�¿���: 1-����0-��
        Private m_blnNoticeRefreshSwitch As Boolean
        '    ֪ͨˢ�¼��, ��λ��
        Private m_intNoticeRefreshTime As Integer
        '    ����ˢ�¿���: 1-����0-��
        Private m_blnChatRefreshSwitch As Boolean
        '    ����ˢ�¼��, ��λ��
        Private m_intChatRefreshTime As Integer












        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Protected Sub New()
            MyBase.New()
        End Sub

        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New(ByVal strUserName As String, ByVal strFilePath As String)
            MyBase.New()
            Try
                '���
                If strUserName Is Nothing Then
                    Throw New Exception("����û��ָ��[�û���ʶ]��")
                End If
                strUserName = strUserName.Trim
                If strUserName = "" Then
                    Throw New Exception("����û��ָ��[�û���ʶ]��")
                End If
                m_strUserName = strUserName
                '********************************************************************
                If strFilePath Is Nothing Then
                    Throw New Exception("����û��ָ��[�����ļ�·��]��")
                End If
                strFilePath = strFilePath.Trim
                If strFilePath = "" Then
                    Throw New Exception("����û��ָ��[�����ļ�·��]��")
                End If
                m_strFilePath = strFilePath

                '��ʼ��
                m_intStartupOption = 0
                m_blnStatusRefreshSwitch = True
                m_intStatusRefreshTime = 1800
                m_blnNoticeRefreshSwitch = True
                m_intNoticeRefreshTime = 600
                m_blnChatRefreshSwitch = True
                m_intChatRefreshTime = 10

                '��ȡʵ�ʶ��������
                Try
                    getPerson()
                Catch ex As Exception
                End Try
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        '----------------------------------------------------------------
        ' ������������
        '----------------------------------------------------------------
        Public Sub Dispose() Implements System.IDisposable.Dispose
            Try
                Dispose(True)
                GC.SuppressFinalize(True)
            Catch ex As Exception
            End Try
        End Sub

        '----------------------------------------------------------------
        ' ������������
        '----------------------------------------------------------------
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If (Not disposing) Then
                Exit Sub
            End If
        End Sub

        '----------------------------------------------------------------
        ' ��ȫ�ͷű�����Դ
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.web.PersonConfig)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub










        '----------------------------------------------------------------
        ' ����ѡ��
        '----------------------------------------------------------------
        Public Property propStartupOption() As Integer
            Get
                propStartupOption = m_intStartupOption
            End Get
            Set(ByVal Value As Integer)
                m_intStartupOption = Value
            End Set
        End Property

        '----------------------------------------------------------------
        ' ״̬ˢ�¿���
        '----------------------------------------------------------------
        Public Property propStatusRefreshSwitch() As Boolean
            Get
                propStatusRefreshSwitch = m_blnStatusRefreshSwitch
            End Get
            Set(ByVal Value As Boolean)
                m_blnStatusRefreshSwitch = Value
            End Set
        End Property

        '----------------------------------------------------------------
        ' ״̬ˢ�¼��
        '----------------------------------------------------------------
        Public Property propStatusRefreshTime() As Integer
            Get
                propStatusRefreshTime = m_intStatusRefreshTime
            End Get
            Set(ByVal Value As Integer)
                m_intStatusRefreshTime = Value
            End Set
        End Property

        '----------------------------------------------------------------
        ' ֪ͨˢ�¿���
        '----------------------------------------------------------------
        Public Property propNoticeRefreshSwitch() As Boolean
            Get
                propNoticeRefreshSwitch = m_blnNoticeRefreshSwitch
            End Get
            Set(ByVal Value As Boolean)
                m_blnNoticeRefreshSwitch = Value
            End Set
        End Property

        '----------------------------------------------------------------
        ' ֪ͨˢ�¼��
        '----------------------------------------------------------------
        Public Property propNoticeRefreshTime() As Integer
            Get
                propNoticeRefreshTime = m_intNoticeRefreshTime
            End Get
            Set(ByVal Value As Integer)
                m_intNoticeRefreshTime = Value
            End Set
        End Property

        '----------------------------------------------------------------
        ' ����ˢ�¿���
        '----------------------------------------------------------------
        Public Property propChatRefreshSwitch() As Boolean
            Get
                propChatRefreshSwitch = m_blnChatRefreshSwitch
            End Get
            Set(ByVal Value As Boolean)
                m_blnChatRefreshSwitch = Value
            End Set
        End Property

        '----------------------------------------------------------------
        ' ����ˢ�¼��
        '----------------------------------------------------------------
        Public Property propChatRefreshTime() As Integer
            Get
                propChatRefreshTime = m_intChatRefreshTime
            End Get
            Set(ByVal Value As Integer)
                m_intChatRefreshTime = Value
            End Set
        End Property










        '----------------------------------------------------------------
        ' �����û���ʶ��ȡ��������
        '----------------------------------------------------------------
        Protected Sub getPerson()

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objDataSet As New System.Data.DataSet
            Dim strTemp As String = ""

            Try
                '���xml�ļ��Ƿ���ڣ�
                Dim strXmlFileSpec As String = ""
                Dim strErrMsg As String = ""
                Dim blnDo As Boolean = False
                strXmlFileSpec = objBaseLocalFile.doMakePath(m_strFilePath, m_strUserName + ".xml")
                If objBaseLocalFile.doFileExisted(strErrMsg, strXmlFileSpec, blnDo) = False Then
                    Exit Try
                End If
                If blnDo = False Then
                    strTemp = objBaseLocalFile.doMakePath(m_strFilePath, "person.xml")
                    If objBaseLocalFile.doCopyFile(strErrMsg, strTemp, strXmlFileSpec, True) = False Then
                        Exit Try
                    End If
                End If

                '���xsd�ļ��Ƿ���ڣ�
                Dim strXsdFileSpec As String = ""
                strXsdFileSpec = objBaseLocalFile.doMakePath(m_strFilePath, "person.xsd")
                If objBaseLocalFile.doFileExisted(strErrMsg, strXsdFileSpec, blnDo) = False Then
                    Exit Try
                End If
                If blnDo = False Then
                    Exit Try
                End If

                'װ��xsd
                Try
                    objDataSet.ReadXmlSchema(strXsdFileSpec)
                Catch ex As Exception
                End Try

                'װ������
                Try
                    objDataSet.ReadXml(strXmlFileSpec, System.Data.XmlReadMode.Auto)
                Catch ex As Exception
                End Try

                '��ȡ����
                If objDataSet.Tables.Count < 1 Then
                    '�����ڣ���ȱʡ��
                    Exit Try
                End If
                If objDataSet.Tables(TABLENAME) Is Nothing Then
                    '�����ڣ���ȱʡ��
                    Exit Try
                End If
                If objDataSet.Tables(TABLENAME).Rows.Count < 1 Then
                    '�����ڣ���ȱʡ��
                    Exit Try
                End If
                With objDataSet.Tables(TABLENAME).Rows(0)
                    m_intStartupOption = objPulicParameters.getObjectValue(.Item(F_GRQDXX), 0)
                    '*************************************************************************************
                    If objPulicParameters.getObjectValue(.Item(F_ZTSXKG), 1) = 0 Then
                        m_blnStatusRefreshSwitch = False
                    Else
                        m_blnStatusRefreshSwitch = True
                    End If
                    m_intStatusRefreshTime = objPulicParameters.getObjectValue(.Item(F_ZTSXJG), 1800)
                    '*************************************************************************************
                    If objPulicParameters.getObjectValue(.Item(F_TZSXKG), 1) = 0 Then
                        m_blnNoticeRefreshSwitch = False
                    Else
                        m_blnNoticeRefreshSwitch = True
                    End If
                    m_intNoticeRefreshTime = objPulicParameters.getObjectValue(.Item(F_TZSXJG), 600)
                    '*************************************************************************************
                    If objPulicParameters.getObjectValue(.Item(F_LTSXKG), 1) = 0 Then
                        m_blnChatRefreshSwitch = False
                    Else
                        m_blnChatRefreshSwitch = True
                    End If
                    m_intChatRefreshTime = objPulicParameters.getObjectValue(.Item(F_LTSXJG), 10)
                End With
            Catch ex As Exception
                Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
                Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                Throw ex
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Exit Sub

        End Sub

        '----------------------------------------------------------------
        ' ���浱ǰ������Ϣ�������ļ�
        '----------------------------------------------------------------
        Public Sub doSave()

            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objDataSet As New System.Data.DataSet

            Try
                '��ȡĿ���ļ�·��
                Dim strXmlFileSpec As String = ""
                strXmlFileSpec = objBaseLocalFile.doMakePath(m_strFilePath, m_strUserName + ".xml")
                Dim strXsdFileSpec As String = ""
                strXsdFileSpec = objBaseLocalFile.doMakePath(m_strFilePath, "person.xsd")

                'װ�ؼܹ�
                objDataSet.ReadXmlSchema(strXsdFileSpec)
                objDataSet.ReadXml(strXmlFileSpec)

                'д���ݼ�
                Dim objDataRow As System.Data.DataRow = Nothing
                Dim intFalse As Integer = 0
                Dim intTrue As Integer = 1
                With objDataSet.Tables(TABLENAME)
                    If .Rows.Count < 1 Then
                        objDataRow = .NewRow()
                    Else
                        objDataRow = .Rows(0)
                    End If

                    objDataRow.Item(F_GRQDXX) = m_intStartupOption
                    objDataRow.Item(F_ZTSXKG) = IIf(m_blnStatusRefreshSwitch = True, intTrue, intFalse)
                    objDataRow.Item(F_ZTSXJG) = m_intStatusRefreshTime
                    objDataRow.Item(F_TZSXKG) = IIf(m_blnNoticeRefreshSwitch = True, intTrue, intFalse)
                    objDataRow.Item(F_TZSXJG) = m_intNoticeRefreshTime
                    objDataRow.Item(F_LTSXKG) = IIf(m_blnChatRefreshSwitch = True, intTrue, intFalse)
                    objDataRow.Item(F_LTSXJG) = m_intChatRefreshTime

                    If .Rows.Count < 1 Then
                        .Rows.Add(objDataRow)
                    End If
                End With

                '���浽XML
                objDataSet.WriteXml(strXmlFileSpec, System.Data.XmlWriteMode.IgnoreSchema)
            Catch ex As Exception
                Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                Throw ex
            End Try

            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Exit Sub

        End Sub

    End Class

End Namespace
