Option Strict On
Option Explicit On 

Imports System
Imports System.IO
Imports System.Data

Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.web
    ' 类名    ：PersonConfig
    '
    ' 功能描述： 
    '   　处理个人的运行参数配置
    '----------------------------------------------------------------

    Public Class PersonConfig
        Implements IDisposable

        '内部常数
        Private Const TABLENAME As String = "PersonalProfile"
        Private Const F_GRQDXX As String = "个人启动选项"
        Private Const F_ZTSXKG As String = "状态刷新开关"
        Private Const F_ZTSXJG As String = "状态刷新间隔"
        Private Const F_TZSXKG As String = "通知刷新开关"
        Private Const F_TZSXJG As String = "通知刷新间隔"
        Private Const F_LTSXKG As String = "聊天刷新开关"
        Private Const F_LTSXJG As String = "聊天刷新间隔"

        '输入参数
        '    用户标识
        Private m_strUserName As String
        '    配置文件路径
        Private m_strFilePath As String

        '输出参数
        '    启动选项: 0-主界面，1-个人事宜
        Private m_intStartupOption As Integer
        '    状态刷新开关: 1-开，0-关
        Private m_blnStatusRefreshSwitch As Boolean
        '    状态刷新间隔, 单位秒
        Private m_intStatusRefreshTime As Integer
        '    通知刷新开关: 1-开，0-关
        Private m_blnNoticeRefreshSwitch As Boolean
        '    通知刷新间隔, 单位秒
        Private m_intNoticeRefreshTime As Integer
        '    聊天刷新开关: 1-开，0-关
        Private m_blnChatRefreshSwitch As Boolean
        '    聊天刷新间隔, 单位秒
        Private m_intChatRefreshTime As Integer












        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Protected Sub New()
            MyBase.New()
        End Sub

        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New(ByVal strUserName As String, ByVal strFilePath As String)
            MyBase.New()
            Try
                '检查
                If strUserName Is Nothing Then
                    Throw New Exception("错误：没有指定[用户标识]！")
                End If
                strUserName = strUserName.Trim
                If strUserName = "" Then
                    Throw New Exception("错误：没有指定[用户标识]！")
                End If
                m_strUserName = strUserName
                '********************************************************************
                If strFilePath Is Nothing Then
                    Throw New Exception("错误：没有指定[配置文件路径]！")
                End If
                strFilePath = strFilePath.Trim
                If strFilePath = "" Then
                    Throw New Exception("错误：没有指定[配置文件路径]！")
                End If
                m_strFilePath = strFilePath

                '初始化
                m_intStartupOption = 0
                m_blnStatusRefreshSwitch = True
                m_intStatusRefreshTime = 1800
                m_blnNoticeRefreshSwitch = True
                m_intNoticeRefreshTime = 600
                m_blnChatRefreshSwitch = True
                m_intChatRefreshTime = 10

                '获取实际定义的数据
                Try
                    getPerson()
                Catch ex As Exception
                End Try
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        '----------------------------------------------------------------
        ' 虚拟析构函数
        '----------------------------------------------------------------
        Public Sub Dispose() Implements System.IDisposable.Dispose
            Try
                Dispose(True)
                GC.SuppressFinalize(True)
            Catch ex As Exception
            End Try
        End Sub

        '----------------------------------------------------------------
        ' 析构函数重载
        '----------------------------------------------------------------
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If (Not disposing) Then
                Exit Sub
            End If
        End Sub

        '----------------------------------------------------------------
        ' 安全释放本身资源
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
        ' 启动选项
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
        ' 状态刷新开关
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
        ' 状态刷新间隔
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
        ' 通知刷新开关
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
        ' 通知刷新间隔
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
        ' 聊天刷新开关
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
        ' 聊天刷新间隔
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
        ' 根据用户标识获取配置数据
        '----------------------------------------------------------------
        Protected Sub getPerson()

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objDataSet As New System.Data.DataSet
            Dim strTemp As String = ""

            Try
                '检查xml文件是否存在？
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

                '检查xsd文件是否存在？
                Dim strXsdFileSpec As String = ""
                strXsdFileSpec = objBaseLocalFile.doMakePath(m_strFilePath, "person.xsd")
                If objBaseLocalFile.doFileExisted(strErrMsg, strXsdFileSpec, blnDo) = False Then
                    Exit Try
                End If
                If blnDo = False Then
                    Exit Try
                End If

                '装载xsd
                Try
                    objDataSet.ReadXmlSchema(strXsdFileSpec)
                Catch ex As Exception
                End Try

                '装载数据
                Try
                    objDataSet.ReadXml(strXmlFileSpec, System.Data.XmlReadMode.Auto)
                Catch ex As Exception
                End Try

                '获取数据
                If objDataSet.Tables.Count < 1 Then
                    '不存在，用缺省！
                    Exit Try
                End If
                If objDataSet.Tables(TABLENAME) Is Nothing Then
                    '不存在，用缺省！
                    Exit Try
                End If
                If objDataSet.Tables(TABLENAME).Rows.Count < 1 Then
                    '不存在，用缺省！
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
        ' 保存当前配置信息到配置文件
        '----------------------------------------------------------------
        Public Sub doSave()

            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objDataSet As New System.Data.DataSet

            Try
                '获取目标文件路径
                Dim strXmlFileSpec As String = ""
                strXmlFileSpec = objBaseLocalFile.doMakePath(m_strFilePath, m_strUserName + ".xml")
                Dim strXsdFileSpec As String = ""
                strXsdFileSpec = objBaseLocalFile.doMakePath(m_strFilePath, "person.xsd")

                '装载架构
                objDataSet.ReadXmlSchema(strXsdFileSpec)
                objDataSet.ReadXml(strXmlFileSpec)

                '写数据集
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

                '保存到XML
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
