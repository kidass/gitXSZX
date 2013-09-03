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
    ' 命名空间：Xydc.Platform.DataAccess
    ' 类名    ：FlowObject
    '
    ' 功能描述：
    '     工作流对象的数据层的基对象
    '----------------------------------------------------------------
    Public MustInherit Class FlowObject
        Implements IDisposable

        '对象类型、对象创建接口注册器(所有对象共享)
        Private Shared m_objFlowTypeBLLXEnum As System.Collections.Specialized.NameValueCollection
        Private Shared m_objFlowTypeNameEnum As System.Collections.Specialized.NameValueCollection
        Private Shared m_objFlowTypeEnum As System.Collections.Specialized.ListDictionary

        '对象初始化标志
        Private m_blnInitialized As Boolean      '对象是否初始化？
        Private m_blnFillData As Boolean         '是否已填充数据

        '数据库连接适配器
        Private m_objSqlDataAdapter As System.Data.SqlClient.SqlDataAdapter
        Private m_objSqlConnection As System.Data.SqlClient.SqlConnection

        '工作流对应的应用数据
        Private m_objFlowAppData As Xydc.Platform.Common.Workflow.BaseFlowObject









        '----------------------------------------------------------------
        ' 保护构造函数
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
        ' 保护构造函数
        '----------------------------------------------------------------
        Protected Sub New(ByVal strFlowType As String, ByVal strFlowTypeName As String)

            Me.New()

            '注册检查
            Try
                Dim strType As String
                Dim strName As String
                strType = strFlowType
                strName = strFlowTypeName
                If m_objFlowTypeEnum Is Nothing Then
                    Throw New Exception("错误：请用[Create]方法创建[" + strFlowTypeName + "]工作流！")
                Else
                    If m_objFlowTypeEnum.Item(strType) Is Nothing Then
                        Throw New Exception("错误：请用[Create]方法创建[" + strFlowTypeName + "]工作流！")
                    End If
                End If
            Catch ex As Exception
                Throw ex
            End Try

            '生成数据
            Try
                m_objFlowAppData = Xydc.Platform.Common.Workflow.BaseFlowObject.Create(strFlowType)
            Catch ex As Exception
                Throw ex
            End Try

        End Sub

        '----------------------------------------------------------------
        ' 析构函数(子类可重载)
        '----------------------------------------------------------------
        Public Overridable Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(True)
        End Sub

        '----------------------------------------------------------------
        ' 析构函数(自身)
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
        ' 安全释放本身资源
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
        ' SqlDataAdapter属性
        '----------------------------------------------------------------
        Protected ReadOnly Property SqlDataAdapter() As System.Data.SqlClient.SqlDataAdapter
            Get
                SqlDataAdapter = m_objSqlDataAdapter
            End Get
        End Property

        '----------------------------------------------------------------
        ' SqlConnection属性
        '----------------------------------------------------------------
        Public ReadOnly Property SqlConnection() As System.Data.SqlClient.SqlConnection
            Get
                SqlConnection = m_objSqlConnection
            End Get
        End Property

        '----------------------------------------------------------------
        ' FlowData属性
        '----------------------------------------------------------------
        Public ReadOnly Property FlowData() As Xydc.Platform.Common.Workflow.BaseFlowObject
            Get
                FlowData = m_objFlowAppData
            End Get
        End Property

        '----------------------------------------------------------------
        ' IsInitialized属性
        '----------------------------------------------------------------
        Public ReadOnly Property IsInitialized() As Boolean
            Get
                IsInitialized = m_blnInitialized
            End Get
        End Property

        '----------------------------------------------------------------
        ' IsFillData属性
        '----------------------------------------------------------------
        Public ReadOnly Property IsFillData() As Boolean
            Get
                IsFillData = Me.m_blnFillData
            End Get
        End Property

        '----------------------------------------------------------------
        ' FlowType属性
        '----------------------------------------------------------------
        Public ReadOnly Property FlowType() As String
            Get
                FlowType = FlowData.FlowType
            End Get
        End Property

        '----------------------------------------------------------------
        ' FlowTypeName属性
        '----------------------------------------------------------------
        Public ReadOnly Property FlowTypeName() As String
            Get
                FlowTypeName = FlowData.FlowTypeName
            End Get
        End Property

        '----------------------------------------------------------------
        ' FlowBLLXName属性
        '----------------------------------------------------------------
        Public ReadOnly Property FlowBLLXName() As String
            Get
                FlowBLLXName = Me.m_objFlowTypeBLLXEnum(FlowData.FlowType)
            End Get
        End Property

        '----------------------------------------------------------------
        ' WJBS属性
        '----------------------------------------------------------------
        Public ReadOnly Property WJBS() As String
            Get
                WJBS = FlowData.WJBS
            End Get
        End Property

        '----------------------------------------------------------------
        ' LSH属性
        '----------------------------------------------------------------
        Public ReadOnly Property LSH() As String
            Get
                LSH = FlowData.LSH
            End Get
        End Property

        '----------------------------------------------------------------
        ' Status属性
        '----------------------------------------------------------------
        Public ReadOnly Property Status() As String
            Get
                Status = FlowData.Status
            End Get
        End Property

        '----------------------------------------------------------------
        ' PZR属性
        '----------------------------------------------------------------
        Public ReadOnly Property PZR() As String
            Get
                PZR = FlowData.PZR
            End Get
        End Property

        '----------------------------------------------------------------
        ' PZRQ属性
        '----------------------------------------------------------------
        Public ReadOnly Property PZRQ() As System.DateTime
            Get
                PZRQ = FlowData.PZRQ
            End Get
        End Property

        '----------------------------------------------------------------
        ' DDSZ属性
        '----------------------------------------------------------------
        Public ReadOnly Property DDSZ() As Integer
            Get
                DDSZ = FlowData.DDSZ
            End Get
        End Property







        '----------------------------------------------------------------
        ' FlowTypeNameCollection属性
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
        ' FlowTypeBLLXCollection属性
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
        ' 工作流对象注册器
        '     strFlowType          ：工作流类型代码
        '     strFlowTypeName      ：工作流类型名称 - 具体工作流名称
        '     strFlowTypeBLLX      ：strFlowTypeName属于strFlowTypeBLLX类
        '     objCreator           ：工作流对象IFlowObjectCreate接口
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Shared Function RegisterFlowType( _
            ByVal strFlowType As String, _
            ByVal strFlowTypeName As String, _
            ByVal strFlowTypeBLLX As String, _
            ByVal objCreator As Xydc.Platform.DataAccess.IFlowObjectCreate) As Boolean

            RegisterFlowType = False

            Try
                '参数检查
                If strFlowType Is Nothing Then
                    Throw New Exception("错误：[工作流类型]不能为空！")
                End If
                strFlowType = strFlowType.Trim()
                If strFlowType = "" Then
                    Throw New Exception("错误：[工作流类型]不能为空！")
                End If
                If strFlowTypeName Is Nothing Then
                    Throw New Exception("错误：[工作流类型名称]不能为空！")
                End If
                strFlowTypeName = strFlowTypeName.Trim()
                If strFlowTypeName = "" Then
                    Throw New Exception("错误：[工作流类型名称]不能为空！")
                End If
                If objCreator Is Nothing Then
                    Throw New Exception("错误：[IFlowObjectCreate]不能为空！")
                End If

                '生成类型汇集器
                If m_objFlowTypeEnum Is Nothing Then
                    m_objFlowTypeEnum = New System.Collections.Specialized.ListDictionary
                End If
                If m_objFlowTypeNameEnum Is Nothing Then
                    m_objFlowTypeNameEnum = New System.Collections.Specialized.NameValueCollection
                End If
                If m_objFlowTypeBLLXEnum Is Nothing Then
                    m_objFlowTypeBLLXEnum = New System.Collections.Specialized.NameValueCollection
                End If

                '检查类型是否存在
                If Not (m_objFlowTypeEnum.Item(strFlowType) Is Nothing) Then
                    Exit Try
                End If

                '检查类型名称是否重复
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
                        Throw New Exception("错误：[" + strNewName + "]已经注册过！")
                    End If
                Next

                '注册
                m_objFlowTypeEnum.Add(strFlowType, objCreator)
                m_objFlowTypeNameEnum.Add(strFlowType, strFlowTypeName)
                m_objFlowTypeBLLXEnum.Add(strFlowType, strFlowTypeBLLX)

                RegisterFlowType = True

            Catch ex As Exception
                Throw ex
            End Try

        End Function

        '----------------------------------------------------------------
        ' 创建工作流对象
        '     strFlowType          ：工作流类型代码
        '     strFlowTypeName      ：工作流类型名称
        ' 返回
        '                          ：Xydc.Platform.DataAccess.FlowObject对象
        '----------------------------------------------------------------
        Public Shared Function Create( _
            ByVal strFlowType As String, _
            ByVal strFlowTypeName As String) As Xydc.Platform.DataAccess.FlowObject

            Create = Nothing

            Try
                '参数检查
                If strFlowType Is Nothing Then
                    Throw New Exception("错误：[工作流类型]不能为空！")
                End If
                strFlowType = strFlowType.Trim()
                If strFlowType = "" Then
                    Throw New Exception("错误：[工作流类型]不能为空！")
                End If
                If strFlowTypeName Is Nothing Then
                    Throw New Exception("错误：[工作流类型名称]不能为空！")
                End If
                strFlowTypeName = strFlowTypeName.Trim()
                If strFlowTypeName = "" Then
                    Throw New Exception("错误：[工作流类型名称]不能为空！")
                End If

                '注册已经实现的FlowObject
                Dim strType As String
                Dim strName As String
                Dim strBLLX As String

                

                '************************************************************************************************************
                '督查单工作流
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

                '获取接口
                Dim objCreator As Object
                objCreator = m_objFlowTypeEnum.Item(strFlowType)
                If objCreator Is Nothing Then
                    Throw New Exception("错误：[" + strFlowType + "]不支持！")
                End If
                strBLLX = m_objFlowTypeBLLXEnum.Item(strFlowType)
                Dim objIFlowObjectCreate As Xydc.Platform.DataAccess.IFlowObjectCreate
                objIFlowObjectCreate = CType(objCreator, Xydc.Platform.DataAccess.IFlowObjectCreate)
                If objIFlowObjectCreate Is Nothing Then
                    Throw New Exception("错误：[" + strFlowType + "]不支持！")
                End If

                '利用接口创建对象
                Create = objIFlowObjectCreate.Create(strFlowType, strFlowTypeName)

                '自动设置类型属性
                Create.FlowData.FlowType = strFlowType
                Create.FlowData.FlowTypeBLLX = strBLLX
                Create.FlowData.FlowTypeName = strFlowTypeName

            Catch ex As Exception
                Throw ex
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据strFlowTypeName获取strFlowType
        '     strFlowTypeName      ：工作流类型名称
        ' 返回
        '                          ：strFlowType
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
        ' 获取文件strWJBS的办理类型?
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objSqlConnection     ：数据库连接
        '     strWJBS              ：文件标识
        '     strBLLX              ：返回办理类型
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If strWJBS Is Nothing Then strWJBS = ""
                strWJBS = strWJBS.Trim()
                If objSqlConnection Is Nothing Then
                    strErrMsg = "错误：未指定连接对象！"
                    GoTo errProc
                End If
                If strWJBS = "" Then Exit Try

                '检索数据
                strSQL = ""
                strSQL = strSQL + " select * from 公文_V_全部审批文件新 " + vbCr
                strSQL = strSQL + " where 文件标识 = '" + strWJBS + "' " + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    Exit Try
                End If

                '返回信息
                With objDataSet.Tables(0).Rows(0)
                    strBLLX = objPulicParameters.getObjectValue(.Item("办理类型"), "")
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
        ' 获取文件strWJBS的办理子类?
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objSqlConnection     ：数据库连接
        '     strWJBS              ：文件标识
        '     strBLZL              ：返回办理子类
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If strWJBS Is Nothing Then strWJBS = ""
                strWJBS = strWJBS.Trim()
                If objSqlConnection Is Nothing Then
                    strErrMsg = "错误：未指定连接对象！"
                    GoTo errProc
                End If
                If strWJBS = "" Then Exit Try

                '检索数据
                strSQL = ""
                strSQL = strSQL + " select * from 公文_V_全部审批文件新 " + vbCr
                strSQL = strSQL + " where 文件标识 = '" + strWJBS + "' " + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    Exit Try
                End If

                '返回信息
                With objDataSet.Tables(0).Rows(0)
                    strBLZL = objPulicParameters.getObjectValue(.Item("文件子类"), "")
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
        ' 将本工作流文件加入到指定的案卷中
        '     strErrMsg            ：返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strAJBS              ：指定案卷标识
        '     strTempPath          ：下载文件临时存放路径
        ' 返回
        '     True                 ：成功
        '     False                ：不成功
        ' 备注
        '     载体                 ：电子
        '     保管期限             ：长期
        '     档案分类             ：文书档案
        '----------------------------------------------------------------
        Public MustOverride Function doAddToAnjuan( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strAJBS As String, _
            ByVal strTempPath As String) As Boolean

        '----------------------------------------------------------------
        ' 获取缺省意见内容
        '     strYjlx              ：审批事宜
        ' 返回
        '                          ：办理标志
        '----------------------------------------------------------------
        Public MustOverride Function getDefaultYJNR(ByVal strYJLX As String) As String

        '----------------------------------------------------------------
        ' 获取“保存意见”办理标志
        ' 返回
        '                          ：办理标志
        '----------------------------------------------------------------
        Public MustOverride Function getBaocunYijianBLBZ() As String

        '----------------------------------------------------------------
        ' 获取“批准”办理标志
        ' 返回
        '                          ：办理标志
        '----------------------------------------------------------------
        Public MustOverride Function getPizhunBLBZ() As String

        '----------------------------------------------------------------
        ' 是对整个文件签名的审批意见?如果是，返回提醒字符串
        '     strYjlx              ：审批事宜
        ' 返回
        '     True                 ：需要签名
        '     False                ：不需要签名
        '----------------------------------------------------------------
        Public MustOverride Function isFileQianminTask( _
            ByVal strYjlx As String, _
            ByRef strPrompt As String) As Boolean

        '----------------------------------------------------------------
        ' 是需要签名确认的审批事宜?
        '     strYjlx              ：审批事宜
        ' 返回
        '     True                 ：需要签名
        '     False                ：不需要签名
        '----------------------------------------------------------------
        Public MustOverride Function isQianminTask(ByVal strYjlx As String) As Boolean

        '----------------------------------------------------------------
        ' 需要签名确认提示?
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strYjlx              ：要确认的意见类型
        '     strSPR               ：审批人
        '     blnNeed              ：(返回)是否需要提示
        '     strXyrList           ：(返回)已有签名人列表
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public MustOverride Function isNeedQianminQuerenPrompt( _
            ByRef strErrMsg As String, _
            ByVal strYjlx As String, _
            ByVal strSPR As String, _
            ByRef blnNeed As Boolean, _
            ByRef strXyrList As String) As Boolean

        '----------------------------------------------------------------
        ' 翻译“是否批准”标志
        ' 返回
        '                          ：翻译后的字符串
        '----------------------------------------------------------------
        Public MustOverride Function doTranslateSFPZ(ByVal strSFPZ As String) As String

        '----------------------------------------------------------------
        ' 签名确认
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strYjlx              ：要确认的意见类型
        '     strSPR               ：审批人
        '     intMode              ：签批模式：0-单独签，1-共同签
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public MustOverride Function doQianminQueren( _
            ByRef strErrMsg As String, _
            ByVal strYjlx As String, _
            ByVal strSPR As String, _
            ByVal intMode As Integer) As Boolean

        '----------------------------------------------------------------
        ' 取消签名
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strYjlx              ：要取消的意见类型
        '     strSPR               ：审批人
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public MustOverride Function doQianminCancel( _
            ByRef strErrMsg As String, _
            ByVal strYjlx As String, _
            ByVal strSPR As String) As Boolean

        '----------------------------------------------------------------
        ' 获取工作流能进行的签批意见列表
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objYjlx              ：签批意见类型+显示名称集合
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public MustOverride Function getAllYjlx( _
            ByRef strErrMsg As String, _
            ByRef objYjlx As System.Collections.Specialized.NameValueCollection) As Boolean

        '----------------------------------------------------------------
        ' 处理“导入正式文件”业务
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strFileSpec          ：要导入的文件路径(WEB服务器本地完全路径)
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public MustOverride Function doImportZS( _
            ByRef strErrMsg As String, _
            ByVal strFileSpec As String) As Boolean

        '----------------------------------------------------------------
        ' 获取“正式文件”字段值
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strZSWJ              ：(返回)正式文件字段值
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public MustOverride Function getZSWJ( _
            ByRef strErrMsg As String, _
            ByRef strZSWJ As String) As Boolean

        '----------------------------------------------------------------
        ' 处理“导入签批件”业务
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strFileSpec          ：要导入的文件路径(WEB服务器本地完全路径)
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public MustOverride Function doImportQP( _
            ByRef strErrMsg As String, _
            ByVal strFileSpec As String) As Boolean

        '----------------------------------------------------------------
        ' 获取“批件原件”字段值
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strPJYJ              ：(返回)批件原件字段值
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public MustOverride Function getPJYJ( _
            ByRef strErrMsg As String, _
            ByRef strPJYJ As String) As Boolean

        '----------------------------------------------------------------
        ' 处理“文件办结”业务
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：用户名称
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public MustOverride Function doCompleteFile( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String) As Boolean

        '----------------------------------------------------------------
        ' 处理“启用文件”业务
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：用户名称
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public MustOverride Function doIQiyongFile( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String) As Boolean

        '----------------------------------------------------------------
        ' 处理“作废文件”业务
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：用户名称
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public MustOverride Function doIZuofeiFile( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String) As Boolean

        '----------------------------------------------------------------
        ' 处理“继续办理”业务
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：用户名称
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public MustOverride Function doIContinueFile( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String) As Boolean

        '----------------------------------------------------------------
        ' 处理“暂缓处理”业务
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：用户名称
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public MustOverride Function doIStopFile( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String) As Boolean

        '----------------------------------------------------------------
        ' 翻译办理事宜
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strOldBlsy           ：翻译前的办理事宜
        '     strNewBlsy           ：翻译后的办理事宜
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public MustOverride Function doTranslateTask( _
            ByRef strErrMsg As String, _
            ByVal strOldBlsy As String, _
            ByRef strNewBlsy As String) As Boolean

        '----------------------------------------------------------------
        ' 保存工作流稿件、附件、相关文件记录(完整事务操作)
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strGJFile              ：要保存的稿件文件的本地缓存文件完整路径
        '     objDataSet_FJ          ：要保存的附件数据
        '     objDataSet_XGWJ        ：要保存的相关文件数据
        '     strUserXM              ：当前操作人员
        '     blnEnforeEdit          ：强制编辑文件数据
        '     objFTPProperty         ：FTP连接参数
        '     objParams              ：其他要随事务提交的数据
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
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
        ' 保存工作流稿件、附件、相关文件记录(完整事务操作)
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strGJFile              ：要保存的稿件文件的本地缓存文件完整路径
        '     objDataSet_FJ          ：要保存的附件数据
        '     objDataSet_XGWJ        ：要保存的相关文件数据
        '     strUserXM              ：当前操作人员
        '     blnEnforeEdit          ：强制编辑文件数据
        '     objFTPProperty         ：FTP连接参数
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
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
        ' 保存工作流记录(完整事务操作)
        '     strErrMsg              ：如果错误，则返回错误信息
        '     objNewData             ：记录新值(返回保存后的新值)
        '     objOldData             ：记录旧值
        '     objenumEditType        ：编辑类型
        '     strGJFile              ：要保存的稿件文件的本地缓存文件完整路径
        '     objDataSet_FJ          ：要保存的附件数据
        '     objDataSet_XGWJ        ：要保存的相关文件数据
        '     strUserXM              ：当前操作人员
        '     blnEnforeEdit          ：强制编辑文件数据
        '     objFTPProperty         ：FTP连接参数
        '     objParams              ：其他要随事务提交的数据
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
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
        ' 保存工作流记录(完整事务操作)
        '     strErrMsg              ：如果错误，则返回错误信息
        '     objNewData             ：记录新值(返回保存后的新值)
        '     objOldData             ：记录旧值
        '     objenumEditType        ：编辑类型
        '     strGJFile              ：要保存的稿件文件的本地缓存文件完整路径
        '     objDataSet_FJ          ：要保存的附件数据
        '     objDataSet_XGWJ        ：要保存的相关文件数据
        '     strUserXM              ：当前操作人员
        '     blnEnforeEdit          ：强制编辑文件数据
        '     objFTPProperty         ：FTP连接参数
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
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
        ' 保存稿件文件
        '     strErrMsg              ：如果错误，则返回错误信息
        '     intWJND                ：要保存到的年度
        '     objSqlTransaction      ：现有事务
        '     objConnectionProperty  ：FTP连接参数
        '     strGJFile              ：要保存的稿件文件的本地缓存文件完整路径
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public MustOverride Function doSaveGJFile( _
            ByRef strErrMsg As String, _
            ByVal intWJND As Integer, _
            ByVal objSqlTransaction As System.Data.SqlClient.SqlTransaction, _
            ByVal objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty, _
            ByVal strGJFile As String) As Boolean

        '----------------------------------------------------------------
        ' 保存记录
        '     strErrMsg              ：如果错误，则返回错误信息
        '     objSqlTransaction      ：现有事务
        '     objNewData             ：记录新值(返回保存后的新值)
        '     objOldData             ：记录旧值
        '     objenumEditType        ：编辑类型
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public MustOverride Function doSaveFile( _
            ByRef strErrMsg As String, _
            ByVal objSqlTransaction As System.Data.SqlClient.SqlTransaction, _
            ByRef objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objOldData As Xydc.Platform.Common.Workflow.BaseFlowObject, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

        '----------------------------------------------------------------
        ' 判断记录数据是否有效？
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objNewData           ：记录新值(返回推荐值)
        '     objOldData           ：记录旧值
        '     objenumEditType      ：编辑类型
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public MustOverride Function doVerifyFile( _
            ByRef strErrMsg As String, _
            ByRef objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objOldData As Xydc.Platform.Common.Workflow.BaseFlowObject, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

        '----------------------------------------------------------------
        ' 获取工作流相关文件附件的基本目录
        '----------------------------------------------------------------
        Public MustOverride Function getBasePath_XGWJFJ() As String

        '----------------------------------------------------------------
        ' 获取工作流稿件的基本目录
        '----------------------------------------------------------------
        Public MustOverride Function getBasePath_GJ() As String

        '----------------------------------------------------------------
        ' 获取工作流附件的基本目录
        '----------------------------------------------------------------
        Public MustOverride Function getBasePath_FJ() As String

        '----------------------------------------------------------------
        ' 获取工作流开始的任务名称
        '----------------------------------------------------------------
        Public MustOverride Function getInitTask() As String

        '----------------------------------------------------------------
        ' 根据“文件标识”获取工作流主表数据(须在子类中实现)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objDataSet           ：返回对象的主表数据集
        '     strTableName         ：返回主表在数据集中的表名
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public MustOverride Function getMainFlowData( _
            ByRef strErrMsg As String, _
            ByRef objDataSet As System.Data.DataSet, _
            ByRef strTableName As String) As Boolean

        '----------------------------------------------------------------
        ' 判断strUserXM是否可以填写承办的办理结果?
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：人员名称
        '     blnCanWrite          ：返回：是否可以?
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public MustOverride Function canWriteChengbanResult( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByRef blnCanWrite As Boolean) As Boolean

        '----------------------------------------------------------------
        ' 判断strUserXM是否承办过文件?
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：用户名称
        '     strXBBZ              ：如果承办过，则返回协办标志
        '     blnHasChengban       ：返回是否承办过文件?
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public MustOverride Function isRenyuanHasChengban( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByRef strXBBZ As String, _
            ByRef blnHasChengban As Boolean) As Boolean

        '----------------------------------------------------------------
        ' 判断strUserXM是否可以加印文件?
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：人员名称
        '     blnCanJiayin         ：返回：是否可以?
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public MustOverride Function canJiayinFile( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByRef blnCanJiayin As Boolean) As Boolean

        '----------------------------------------------------------------
        ' 判断strUserXM是否登记办理结果?
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：人员名称
        '     blnCan               ：返回：是否可以?
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public MustOverride Function canDengjiBLJG( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByRef blnCan As Boolean) As Boolean

        '----------------------------------------------------------------
        ' 判断文件是否办理完毕?
        '     strErrMsg            ：如果错误，则返回错误信息
        '     blnComplete          ：返回是否办理完毕?
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public MustOverride Function isFileComplete( _
            ByRef strErrMsg As String, _
            ByRef blnComplete As Boolean) As Boolean

        '----------------------------------------------------------------
        ' 判断文件是否已经定稿?
        '     strErrMsg            ：如果错误，则返回错误信息
        '     blnDinggao           ：返回是否已经定稿?
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public MustOverride Function isFileDinggao( _
            ByRef strErrMsg As String, _
            ByRef blnDinggao As Boolean) As Boolean

        '----------------------------------------------------------------
        ' 判断文件是否已经作废?
        '     strErrMsg            ：如果错误，则返回错误信息
        '     blnZuofei            ：返回是否已经作废?
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public MustOverride Function isFileZuofei( _
            ByRef strErrMsg As String, _
            ByRef blnZuofei As Boolean) As Boolean

        '----------------------------------------------------------------
        ' 判断文件是否已经停办?
        '     strErrMsg            ：如果错误，则返回错误信息
        '     blnTingban           ：返回是否已经停办?
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public MustOverride Function isFileTingban( _
            ByRef strErrMsg As String, _
            ByRef blnTingban As Boolean) As Boolean

        '----------------------------------------------------------------
        ' 判断strUserXM是否是文件的原始作者?
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：人员名称
        '     blnIs                ：返回是否?
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public MustOverride Function isOriginalPeople( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByRef blnIs As Boolean) As Boolean

        '----------------------------------------------------------------
        ' 判断strBLSY是否已经批准?
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strBLSY              ：事宜名称
        '     blnApproved          ：返回是否?
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public MustOverride Function isTaskApproved( _
            ByRef strErrMsg As String, _
            ByVal strBLSY As String, _
            ByRef blnApproved As Boolean) As Boolean

        '----------------------------------------------------------------
        ' 计算strBLSY的级别
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strBLSY              ：事宜名称
        '     intLevel             ：返回级别
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public MustOverride Function getTaskLevel( _
            ByRef strErrMsg As String, _
            ByVal strBLSY As String, _
            ByRef intLevel As Integer) As Boolean

        '----------------------------------------------------------------
        ' 判断strBLSY是否为审批事宜？
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strBLSY              ：事宜名称
        '     intLevel             ：事宜级别
        '     blnIsShenpi          ：返回：是否？
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public MustOverride Function isShenpiTask( _
            ByRef strErrMsg As String, _
            ByVal strBLSY As String, _
            ByVal intLevel As Integer, _
            ByRef blnIsShenpi As Boolean) As Boolean

        '----------------------------------------------------------------
        ' 判断strBLSY是否为审批事宜？
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strBLSY              ：事宜名称
        '     blnIsShenpi          ：返回：是否？
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public MustOverride Function isShenpiTask( _
            ByRef strErrMsg As String, _
            ByVal strBLSY As String, _
            ByRef blnIsShenpi As Boolean) As Boolean

        '----------------------------------------------------------------
        ' 获取strUserXM能阅读的审批意见
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：用户名称
        '     strWhere             ：搜索条件
        '     objOpinionData       ：返回：意见数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public MustOverride Function getCanReadOpinion( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByVal strWhere As String, _
            ByRef objOpinionData As Xydc.Platform.Common.Data.FlowData) As Boolean

        '----------------------------------------------------------------
        ' 删除文件
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objFTPProperty       ：FTP服务器连接参数
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public MustOverride Function doDeleteFile( _
            ByRef strErrMsg As String, _
            ByVal objFTPProperty As Xydc.Platform.Common.Utilities.FTPProperty) As Boolean

        '----------------------------------------------------------------
        ' 根据“文件标识”填充工作流对象数据(须在子类中实现)
        '     strErrMsg            ：如果错误，则返回错误信息
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public MustOverride Function doFillFlowData( _
            ByRef strErrMsg As String) As Boolean

        '----------------------------------------------------------------
        ' 获取新的文件流水号
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strLSH               ：返回文件流水号
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public MustOverride Function getNewLSH( _
            ByRef strErrMsg As String, _
            ByRef strLSH As String) As Boolean









        '----------------------------------------------------------------
        ' Flow对象初始化
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strWJBS              ：文件标识
        '     objSqlConnection     ：数据库连接
        '     blnFillData          ：是否填充数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Overridable Function doInitialize( _
            ByRef strErrMsg As String, _
            ByVal strWJBS As String, _
            ByVal objSqlConnection As System.Data.SqlClient.SqlConnection, _
            ByVal blnFillData As Boolean) As Boolean

            doInitialize = False
            strErrMsg = ""

            Try
                '检查
                If strWJBS Is Nothing Then strWJBS = ""
                strWJBS = strWJBS.Trim()
                If objSqlConnection Is Nothing Then
                    strErrMsg = "错误：未初始化[数据库连接]！"
                    GoTo errProc
                End If

                '打开连接
                Select Case objSqlConnection.State
                    Case System.Data.ConnectionState.Closed
                        m_objSqlConnection.ConnectionString = objSqlConnection.ConnectionString
                        m_objSqlConnection.Open()
                    Case Else
                End Select

                '设置flow对象初始值
                Me.FlowData.WJBS = strWJBS

                '填充对象其他数据
                If blnFillData = True Then
                    If Me.doFillFlowData(strErrMsg) = False Then
                        Exit Try
                    End If
                    Me.m_blnFillData = True
                End If

                '初始化成功
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
        ' Flow对象初始化
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWJBS              ：文件标识
        '     blnFillData          ：是否填充数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If strWJBS Is Nothing Then strWJBS = ""
                strWJBS = strWJBS.Trim()
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim()
                If strUserId = "" Then
                    strErrMsg = "错误：未指定连接用户！"
                    GoTo errProc
                End If

                '打开连接
                Dim strConnectString As String
                Select Case Me.m_objSqlConnection.State
                    Case System.Data.ConnectionState.Closed
                        '获取连接串
                        With New Xydc.Platform.Common.jsoaConfiguration
                            strConnectString = .getConnectionString(strUserId, strPassword)
                        End With
                        '打开连接
                        m_objSqlConnection.ConnectionString = strConnectString
                        m_objSqlConnection.Open()
                End Select

                '设置flow对象初始值
                Me.FlowData.WJBS = strWJBS

                '填充对象其他数据
                If blnFillData = True Then
                    If Me.doFillFlowData(strErrMsg) = False Then
                        Exit Try
                    End If
                    Me.m_blnFillData = True
                End If

                '初始化成功
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
        ' 判断指定人员是否可阅读文件？
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：人员名称
        '     blnCanRead           ：返回：是否可以？
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '
                '如果是管理员，则可以看
                If strUserXM = "管理员" Then
                    blnCanRead = True
                Else
                    '获取发送人可以看 或 接收人可以看 的交接数据
                    strSQL = ""
                    strSQL = strSQL + " select * from 公文_B_交接 " + vbCr
                    strSQL = strSQL + " where 文件标识 = '" + strWJBS + "' " + vbCr
                    strSQL = strSQL + " and ((发送人   = '" + strUserXM + "' and rtrim(交接标识) like '_1%' ) " + vbCr
                    strSQL = strSQL + " or   (接收人   = '" + strUserXM + "' and rtrim(交接标识) like '__1%')) " + vbCr
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
        ' 判断指定人员是否可以进行文件补阅?
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：人员名称
        '     blnCanRead           ：返回：是否可以?
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '交接办理完毕状态SQL列表
                Dim strTaskStatusYWCList As String = Me.FlowData.TaskStatusYWCList

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strBYQQ As String = Me.FlowData.TASK_BYQQ
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                strSQL = ""
                '收到补阅请求、没有办完的交接
                strSQL = strSQL + " select * from 公文_B_交接 "
                strSQL = strSQL + " where 文件标识 = '" + strWJBS + "' "
                strSQL = strSQL + " and   办理子类 = '" + strBYQQ + "' "
                strSQL = strSQL + " and   办理状态 not in (" + strTaskStatusYWCList + ") "
                strSQL = strSQL + " and  (接收人 = '" + strUserXM + "' and rtrim(交接标识) like '__1%') "
                strSQL = strSQL + " union "
                '正常处理的交接
                strSQL = strSQL + " select * from 公文_B_交接 "
                strSQL = strSQL + " where 文件标识 = '" + strWJBS + "' "
                strSQL = strSQL + " and  (接收人 = '" + strUserXM + "' and rtrim(交接标识) like '__1__0%') "
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
        ' 判断指定人员strUserId是否可督办文件？
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：人员代码
        '     strBMDM              ：strUserId所属单位代码
        '     blnCanDuban          ：返回：是否可以？
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim()
                If strBMDM Is Nothing Then strBMDM = ""
                strBMDM = strBMDM.Trim

                '交接办理完毕状态SQL列表
                Dim strTaskStatusYWCList As String = Me.FlowData.TaskStatusYWCList

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '可以督办全单位?
                Dim intDBFW As Integer
                intDBFW = CType(Xydc.Platform.Common.Data.DubanshezhiData.enumDubanfanweiType.All, Integer)
                strSQL = ""
                strSQL = strSQL + " select a.* " + vbCr
                strSQL = strSQL + " from 管理_B_督办设置 a " + vbCr
                strSQL = strSQL + " left join (" + vbCr
                strSQL = strSQL + "   select * from 公共_B_上岗 " + vbCr
                strSQL = strSQL + "   where 人员代码  = '" + strUserId + "' " + vbCr
                strSQL = strSQL + " ) b on a.岗位代码 = b.岗位代码 " + vbCr
                strSQL = strSQL + " where b.岗位代码 is not null " + vbCr
                strSQL = strSQL + " and   a.督办范围 = " + intDBFW.ToString() + vbCr
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

                '可以督办指定级数以下单位
                Dim blnDefined As Boolean
                Dim intMinJSXZ As Integer
                Dim intMinJSDM As Integer
                '获取指定人员可以督办的最小组织代码级数限制
                intDBFW = CType(Xydc.Platform.Common.Data.DubanshezhiData.enumDubanfanweiType.Level, Integer)
                strSQL = ""
                strSQL = strSQL + " select min(a.级数限制) " + vbCr
                strSQL = strSQL + " from 管理_B_督办设置 a " + vbCr
                strSQL = strSQL + " left join ( " + vbCr
                strSQL = strSQL + "   select * from 公共_B_上岗 " + vbCr
                strSQL = strSQL + "   where 人员代码  = '" + strUserId + "' " + vbCr
                strSQL = strSQL + " ) b on a.岗位代码 = b.岗位代码 " + vbCr
                strSQL = strSQL + " where b.岗位代码 Is Not Null " + vbCr
                strSQL = strSQL + " and   a.督办范围 = " + intDBFW.ToString() + vbCr
                strSQL = strSQL + " and   a.级数限制 is not null " + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                intMinJSXZ = Xydc.Platform.Common.Data.CustomerData.intZZDM_FJCDSM.Length + 1           '组织代码最低级+1
                intMinJSXZ = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item(0), intMinJSXZ)
                If intMinJSXZ > Xydc.Platform.Common.Data.CustomerData.intZZDM_FJCDSM.Length Then
                    blnDefined = False
                ElseIf intMinJSXZ < 1 Then
                    blnDefined = False
                Else
                    intMinJSDM = Xydc.Platform.Common.Data.CustomerData.intZZDM_FJCDSM(intMinJSXZ - 1)  '组织代码长度
                    blnDefined = True
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing
                If blnDefined = True Then
                    '获取当前文件的接收人未办理完成、指定intMinJSXZ的下级部门
                    strSQL = ""
                    strSQL = strSQL + " select a.* from " + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select * from 公文_B_交接 " + vbCr
                    strSQL = strSQL + "   where 文件标识 = '" + strWJBS + "' " + vbCr                               '当前文件
                    strSQL = strSQL + "   and   rtrim(交接标识) like '1_1__0%' "                                           '已发送+接收人能看+非通知
                    strSQL = strSQL + "   and   办理状态 not in (" + strTaskStatusYWCList + ") " + vbCr             '未办完
                    strSQL = strSQL + " ) a " + vbCr
                    strSQL = strSQL + " left join 公共_B_人员 b on a.接收人 = b.人员名称 " + vbCr
                    strSQL = strSQL + " where b.人员代码 is not null " + vbCr
                    strSQL = strSQL + " and   b.组织代码 is not null " + vbCr
                    strSQL = strSQL + " and   len(rtrim(b.组织代码)) >= " + intMinJSDM.ToString() + vbCr            '指定级别以下单位
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

                '可以督办本部门指定级数以下单位
                '获取指定人员可以督办的最小级数限制
                intDBFW = CType(Xydc.Platform.Common.Data.DubanshezhiData.enumDubanfanweiType.BumenLevel, Integer)
                strSQL = ""
                strSQL = strSQL + " select min(a.级数限制) from 管理_B_督办设置 a " + vbCr
                strSQL = strSQL + " left join ( " + vbCr
                strSQL = strSQL + "   select * from 公共_B_上岗 " + vbCr
                strSQL = strSQL + "   where 人员代码 = '" + strUserId + "' " + vbCr
                strSQL = strSQL + " ) b on a.岗位代码 = b.岗位代码 " + vbCr
                strSQL = strSQL + " where b.岗位代码 Is Not Null " + vbCr
                strSQL = strSQL + " and a.督办范围 = " + intDBFW.ToString() + vbCr
                strSQL = strSQL + " and a.级数限制 is not null " + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                intMinJSXZ = Xydc.Platform.Common.Data.CustomerData.intZZDM_FJCDSM.Length + 1           '组织代码最低级+1
                intMinJSXZ = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item(0), intMinJSXZ)
                If intMinJSXZ > Xydc.Platform.Common.Data.CustomerData.intZZDM_FJCDSM.Length Then
                    blnDefined = False
                ElseIf intMinJSXZ < 1 Then
                    blnDefined = False
                Else
                    intMinJSDM = Xydc.Platform.Common.Data.CustomerData.intZZDM_FJCDSM(intMinJSXZ - 1)  '组织代码长度
                    blnDefined = True
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing
                If blnDefined = True Then
                    '获取当前文件的接收人未办理完成、接收人所在部门是指定人员的指定intMinJSXZ的下级部门
                    strSQL = ""
                    strSQL = strSQL + " select a.* from " + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select * from 公文_B_交接 " + vbCr
                    strSQL = strSQL + "   where 文件标识 = '" + strWJBS + "' " + vbCr                             '当前文件
                    strSQL = strSQL + "   and   rtrim(交接标识) like '1_1__0%' " + vbCr                                  '已发送+接收人能看+非通知类
                    strSQL = strSQL + "   and   办理状态 not in (" + strTaskStatusYWCList + ") " + vbCr           '未办完
                    strSQL = strSQL + " ) a " + vbCr
                    strSQL = strSQL + " left join 公共_B_人员 b on a.接收人 = b.人员名称 " + vbCr
                    strSQL = strSQL + " where b.人员代码 is not null " + vbCr
                    strSQL = strSQL + " and b.组织代码 is not null " + vbCr
                    strSQL = strSQL + " and rtrim(b.组织代码) like '" + strBMDM + "' + '%' " + vbCr               '本级或下级部门
                    strSQL = strSQL + " and len(rtrim(b.组织代码)) >= " + intMinJSDM.ToString() + vbCr            '指定级别以下单位
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
        ' 判断指定人员strUserId是否可对strJSR进行督办文件？
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：准备督办文件的人员标识
        '     strBMDM              ：strUserId所属单位代码
        '     strJSR               ：接收人名称
        '     blnCanDuban          ：返回：是否可以？
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim()
                If strBMDM Is Nothing Then strBMDM = ""
                strBMDM = strBMDM.Trim
                If strJSR Is Nothing Then strJSR = ""
                strJSR = strJSR.Trim()

                '交接办理完毕状态SQL列表
                Dim strTaskStatusYWCList As String = Me.FlowData.TaskStatusYWCList

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '可以督办全单位?
                Dim intDBFW As Integer
                intDBFW = CType(Xydc.Platform.Common.Data.DubanshezhiData.enumDubanfanweiType.All, Integer)
                strSQL = ""
                strSQL = strSQL + " select a.* " + vbCr
                strSQL = strSQL + " from 管理_B_督办设置 a " + vbCr
                strSQL = strSQL + " left join (" + vbCr
                strSQL = strSQL + "   select * from 公共_B_上岗 " + vbCr
                strSQL = strSQL + "   where 人员代码  = '" + strUserId + "' " + vbCr
                strSQL = strSQL + " ) b on a.岗位代码 = b.岗位代码 " + vbCr
                strSQL = strSQL + " where b.岗位代码 is not null " + vbCr
                strSQL = strSQL + " and   a.督办范围 = " + intDBFW.ToString() + vbCr
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

                '可以督办指定级数以下单位
                Dim blnDefined As Boolean
                Dim intMinJSXZ As Integer
                Dim intMinJSDM As Integer
                '获取指定人员可以督办的最小级数限制
                intDBFW = CType(Xydc.Platform.Common.Data.DubanshezhiData.enumDubanfanweiType.Level, Integer)
                strSQL = ""
                strSQL = strSQL + " select min(a.级数限制) " + vbCr
                strSQL = strSQL + " from 管理_B_督办设置 a " + vbCr
                strSQL = strSQL + " left join ( " + vbCr
                strSQL = strSQL + "   select * from 公共_B_上岗 " + vbCr
                strSQL = strSQL + "   where 人员代码  = '" + strUserId + "' " + vbCr
                strSQL = strSQL + " ) b on a.岗位代码 = b.岗位代码 " + vbCr
                strSQL = strSQL + " where b.岗位代码 Is Not Null " + vbCr
                strSQL = strSQL + " and   a.督办范围 = " + intDBFW.ToString() + vbCr
                strSQL = strSQL + " and   a.级数限制 is not null " + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                intMinJSXZ = Xydc.Platform.Common.Data.CustomerData.intZZDM_FJCDSM.Length + 1           '组织代码最低级+1
                intMinJSXZ = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item(0), intMinJSXZ)
                If intMinJSXZ > Xydc.Platform.Common.Data.CustomerData.intZZDM_FJCDSM.Length Then
                    blnDefined = False
                ElseIf intMinJSXZ < 1 Then
                    blnDefined = False
                Else
                    intMinJSDM = Xydc.Platform.Common.Data.CustomerData.intZZDM_FJCDSM(intMinJSXZ - 1)  '组织代码长度
                    blnDefined = True
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing
                If blnDefined = True Then
                    '获取当前文件的接收人strJSR未办理完成、指定intMinJSXZ的下级部门
                    strSQL = ""
                    strSQL = strSQL + " select a.* from " + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select * from 公文_B_交接 " + vbCr
                    strSQL = strSQL + "   where 文件标识 = '" + strWJBS + "' " + vbCr                           '当前文件
                    strSQL = strSQL + "   and   rtrim(交接标识) like '1_1__0%' " + vbCr                                '已发送+接收人能看+非通知
                    strSQL = strSQL + "   and   办理状态 not in (" + strTaskStatusYWCList + ") " + vbCr         '未办完
                    strSQL = strSQL + "   and   接收人   = '" + strJSR + "' " + vbCr                            '指定接收人
                    strSQL = strSQL + " ) a " + vbCr
                    strSQL = strSQL + " left join 公共_B_人员 b on a.接收人 = b.人员名称 " + vbCr
                    strSQL = strSQL + " where b.人员代码 is not null " + vbCr
                    strSQL = strSQL + " and   b.组织代码 is not null " + vbCr
                    strSQL = strSQL + " and   len(rtrim(b.组织代码)) >= " + intMinJSDM.ToString() + vbCr        '指定级别以下单位
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

                '可以督办本部门指定级数以下单位
                '获取指定人员可以督办的最小级数限制
                intDBFW = CType(Xydc.Platform.Common.Data.DubanshezhiData.enumDubanfanweiType.BumenLevel, Integer)
                strSQL = ""
                strSQL = strSQL + " select min(a.级数限制) " + vbCr
                strSQL = strSQL + " from 管理_B_督办设置 a " + vbCr
                strSQL = strSQL + " left join ( " + vbCr
                strSQL = strSQL + "   select * from 公共_B_上岗 " + vbCr
                strSQL = strSQL + "   where 人员代码 = '" + strUserId + "' " + vbCr
                strSQL = strSQL + " ) b on a.岗位代码 = b.岗位代码 " + vbCr
                strSQL = strSQL + " where b.岗位代码 Is Not Null " + vbCr
                strSQL = strSQL + " and a.督办范围 = " + intDBFW.ToString() + vbCr
                strSQL = strSQL + " and a.级数限制 is not null " + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                intMinJSXZ = Xydc.Platform.Common.Data.CustomerData.intZZDM_FJCDSM.Length + 1           '组织代码最低级+1
                intMinJSXZ = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item(0), intMinJSXZ)
                If intMinJSXZ > Xydc.Platform.Common.Data.CustomerData.intZZDM_FJCDSM.Length Then
                    blnDefined = False
                ElseIf intMinJSXZ < 1 Then
                    blnDefined = False
                Else
                    intMinJSDM = Xydc.Platform.Common.Data.CustomerData.intZZDM_FJCDSM(intMinJSXZ - 1)  '组织代码长度
                    blnDefined = True
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing
                If blnDefined = True Then
                    '获取当前文件的接收人strJSR未办理完成、接收人所在部门是指定人员的指定intMinJSXZ的下级部门
                    strSQL = ""
                    strSQL = strSQL + " select a.* from " + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select * from 公文_B_交接 " + vbCr
                    strSQL = strSQL + "   where 文件标识 = '" + strWJBS + "' " + vbCr                           '当前文件
                    strSQL = strSQL + "   and   rtrim(交接标识) like '1_1__0%' " + vbCr                                '已发送+接收人能看+非通知
                    strSQL = strSQL + "   and   办理状态 not in (" + strTaskStatusYWCList + ") " + vbCr         '未办完
                    strSQL = strSQL + "   and   接收人   = '" + strJSR + "' " + vbCr                            '指定接收人
                    strSQL = strSQL + " ) a " + vbCr
                    strSQL = strSQL + " left join 公共_B_人员 b on a.接收人 = b.人员名称 " + vbCr
                    strSQL = strSQL + " where b.人员代码 is not null " + vbCr
                    strSQL = strSQL + " and b.组织代码 is not null " + vbCr
                    strSQL = strSQL + " and rtrim(b.组织代码) like '" + strBMDM + "' + '%' " + vbCr             '本级或下级部门
                    strSQL = strSQL + " and len(rtrim(b.组织代码)) >= " + intMinJSDM.ToString() + vbCr          '指定级别以下单位
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
        ' 判断指定人员strUserXM是否可填写督办结果？
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：当前操作人员名称
        '     blnCanWrite          ：返回：是否可以？
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '获取strUserXM被督办且存在交接记录的督办数据
                strSQL = ""
                strSQL = strSQL + " select a.* from " + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select * from 公文_B_督办 " + vbCr
                strSQL = strSQL + "   where 文件标识 = '" + strWJBS + "' " + vbCr
                strSQL = strSQL + "   and   被督办人 = '" + strUserXM + "' " + vbCr
                strSQL = strSQL + " ) a " + vbCr
                strSQL = strSQL + " left join " + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select * from 公文_B_交接 " + vbCr
                strSQL = strSQL + "   where 文件标识 = '" + strWJBS + "' " + vbCr                           '当前文件
                strSQL = strSQL + "   and   接收人   = '" + strUserXM + "' " + vbCr                         '接收人
                strSQL = strSQL + "   and   rtrim(交接标识) like '1_1__0%' " + vbCr                                '已发送+接收人能看+非通知
                strSQL = strSQL + " ) b on a.文件标识 = b.文件标识 and a.交接序号 = b.交接序号 " + vbCr
                strSQL = strSQL + " where b.文件标识 is not null " + vbCr                                   '一定满足
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
        ' 判断指定人员是否可催办文件？
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：准备催办文件的人员名称
        '     blnCanCuiban         ：返回：是否可以催办？
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '交接办理完毕状态SQL列表
                Dim strTaskStatusYWCList As String = Me.FlowData.TaskStatusYWCList

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '获取发送人=指定人员且接收人未完成的交接单
                strSQL = ""
                strSQL = strSQL + " select * from 公文_B_交接 " + vbCr
                strSQL = strSQL + " where 文件标识 = '" + strWJBS + "' " + vbCr                    '当前文件
                strSQL = strSQL + " and   发送人   = '" + strUserXM + "' " + vbCr                  '发送人
                strSQL = strSQL + " and   rtrim(交接标识) like '1_1__0%' " + vbCr                         '已发送+接收人能看+非通知
                strSQL = strSQL + " and   办理状态 not in (" + strTaskStatusYWCList + ") " + vbCr  '未办完
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
        ' 判断指定人员是否可补登领导意见？
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：准备补登领导意见的人员代码
        '     strBMDM              ：准备补登领导意见的人员所属单位代码
        '     blnCanBudeng         ：返回：是否可以？
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim()
                If strBMDM Is Nothing Then strBMDM = ""
                strBMDM = strBMDM.Trim()

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '补登无限制
                Dim intBDFW As Integer
                intBDFW = CType(Xydc.Platform.Common.Data.BudengshezhiData.enumBudengfanweiType.All, Integer)
                strSQL = ""
                strSQL = strSQL + " select a.* " + vbCr
                strSQL = strSQL + " from 管理_B_补登设置 a " + vbCr
                strSQL = strSQL + " left join (" + vbCr
                strSQL = strSQL + "   select * from 公共_B_上岗 " + vbCr
                strSQL = strSQL + "   where 人员代码 = '" + strUserId + "' " + vbCr
                strSQL = strSQL + " ) b on a.岗位代码 = b.岗位代码 " + vbCr
                strSQL = strSQL + " where b.岗位代码 is not null " + vbCr
                strSQL = strSQL + " and   a.补登范围 = " + intBDFW.ToString() + vbCr
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

                '获取指定人员的补登设置情况1
                Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate
                Dim strTemp As String = ""
                intBDFW = CType(Xydc.Platform.Common.Data.BudengshezhiData.enumBudengfanweiType.Zhiwu, Integer)
                strSQL = ""
                strSQL = strSQL + " select a.* " + vbCr
                strSQL = strSQL + " from 管理_B_补登设置 a " + vbCr
                strSQL = strSQL + " left join (" + vbCr
                strSQL = strSQL + "   select * from 公共_B_上岗 " + vbCr
                strSQL = strSQL + "   where 人员代码 = '" + strUserId + "' " + vbCr
                strSQL = strSQL + " ) b on a.岗位代码 = b.岗位代码 " + vbCr
                strSQL = strSQL + " where b.岗位代码 is not null " + vbCr
                strSQL = strSQL + " and a.补登范围 = " + intBDFW.ToString() + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    '获取补登指定职务列表1
                    With objDataSet.Tables(0)
                        Dim intCount As Integer
                        Dim i As Integer
                        intCount = .Rows.Count
                        For i = 0 To intCount - 1 Step 1
                            If strTemp = "" Then
                                strTemp = objPulicParameters.getObjectValue(.Rows(i).Item("职务列表"), "").Trim()
                            Else
                                strTemp = strTemp + strSep + objPulicParameters.getObjectValue(.Rows(i).Item("职务列表"), "").Trim()
                            End If
                        Next
                    End With
                    Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                    objDataSet = Nothing
                    If strTemp <> "" Then
                        '检查指定文件中是否有指定部门的指定职务strTemp的人存在
                        strSQL = ""
                        strSQL = strSQL + " select * from 公文_B_交接 " + vbCr
                        strSQL = strSQL + " where 文件标识 = '" + strWJBS + "' " + vbCr
                        strSQL = strSQL + " and   rtrim(交接标识) like '1%' " + vbCr
                        strSQL = strSQL + " and   接收人 in " + vbCr
                        strSQL = strSQL + " (" + vbCr
                        strSQL = strSQL + "   select b.人员名称 from 公共_B_上岗 a " + vbCr
                        strSQL = strSQL + "   left join 公共_B_人员     b on a.人员代码 = b.人员代码 " + vbCr
                        strSQL = strSQL + "   left join 公共_B_工作岗位 c on a.岗位代码 = c.岗位代码 " + vbCr
                        strSQL = strSQL + "   where b.人员名称 is not null " + vbCr
                        strSQL = strSQL + "   and   c.岗位名称 is not null " + vbCr
                        strSQL = strSQL + "   and '" + strTemp + "' + '" + strSep + "' like '%'+rtrim(c.岗位名称)+'" + strSep + "%' " + vbCr
                        strSQL = strSQL + "   group by b.人员名称" + vbCr
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

                '获取指定人员的补登设置情况2
                intBDFW = CType(Xydc.Platform.Common.Data.BudengshezhiData.enumBudengfanweiType.ZhiwuBumenLevel, Integer)
                strSQL = ""
                strSQL = strSQL + " select a.* from 管理_B_补登设置 a " + vbCr
                strSQL = strSQL + " left join (" + vbCr
                strSQL = strSQL + "   select * from 公共_B_上岗 " + vbCr
                strSQL = strSQL + "   where  人员代码 = '" + strUserId + "' " + vbCr
                strSQL = strSQL + " ) b on a.岗位代码 = b.岗位代码 " + vbCr
                strSQL = strSQL + " where b.岗位代码 is not null " + vbCr
                strSQL = strSQL + " and   a.补登范围 = " + intBDFW.ToString() + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    blnCanBudeng = False
                    Exit Try
                End If
                '获取补登指定职务列表2
                With objDataSet.Tables(0)
                    Dim blnDefined As Boolean
                    Dim intMinJSDM As Integer
                    Dim intMinJSXZ As Integer
                    Dim intCount As Integer
                    Dim i As Integer
                    intcount = .Rows.Count
                    For i = 0 To intcount - 1 Step 1
                        strTemp = ""
                        strTemp = objPulicParameters.getObjectValue(.Rows(i).Item("职务列表"), "").Trim()
                        intMinJSXZ = Xydc.Platform.Common.Data.CustomerData.intZZDM_FJCDSM.Length + 1      '组织代码最低级+1
                        intMinJSXZ = objPulicParameters.getObjectValue(.Rows(i).Item("级数限制"), intMinJSXZ)
                        If intMinJSXZ > Xydc.Platform.Common.Data.CustomerData.intZZDM_FJCDSM.Length Then
                            blnDefined = False
                        ElseIf intMinJSXZ < 1 Then
                            blnDefined = False
                        Else
                            intMinJSDM = Xydc.Platform.Common.Data.CustomerData.intZZDM_FJCDSM(intMinJSXZ - 1)
                            blnDefined = True
                        End If
                        If strTemp <> "" And blnDefined = True Then
                            '检查指定文件中是否有指定部门的指定职务strTemp的人存在
                            strSQL = ""
                            strSQL = strSQL + " select * from 公文_B_交接 " + vbCr
                            strSQL = strSQL + " where 文件标识 = '" + strWJBS + "' " + vbCr                 '当前文件
                            strSQL = strSQL + " and   rtrim(交接标识) like '1_1__0%' " + vbCr                      '已发送+接收人能看+非通知
                            strSQL = strSQL + " and   接收人 in " + vbCr                                    '可能接收人
                            strSQL = strSQL + " (" + vbCr
                            strSQL = strSQL + "   select b.人员名称 from 公共_B_上岗 a " + vbCr
                            strSQL = strSQL + "   left join 公共_B_人员     b on a.人员代码 = b.人员代码 " + vbCr
                            strSQL = strSQL + "   left join 公共_B_工作岗位 c on a.岗位代码 = c.岗位代码 " + vbCr
                            strSQL = strSQL + "   where b.人员名称 is not null " + vbCr
                            strSQL = strSQL + "   and   b.组织代码 is not null " + vbCr
                            strSQL = strSQL + "   and   c.岗位名称 is not null " + vbCr
                            strSQL = strSQL + "   and '" + strTemp + "' + '" + strSep + "' like '%'+rtrim(c.岗位名称)+'" + strSep + "%' " + vbCr  '指定职务
                            strSQL = strSQL + "   and len(rtrim(b.组织代码)) >= " + intMinJSDM.ToString() + vbCr                                  '指定级别以下
                            strSQL = strSQL + "   and rtrim(b.组织代码) like '" + strBMDM + "%' " + vbCr                                          '本级及下级单位
                            strSQL = strSQL + "   group by b.人员名称" + vbCr
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
        ' 判断指定人员是否可补登strJSR签署的意见？
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：准备补登领导意见的人员代码
        '     strBMDM              ：准备补登领导意见的人员所属单位代码
        '     strJSR               ：领导名称
        '     blnCanBudeng         ：返回：是否可以？
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim()
                If strBMDM Is Nothing Then strBMDM = ""
                strBMDM = strBMDM.Trim()
                If strJSR Is Nothing Then strJSR = ""
                strJSR = strJSR.Trim()

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '补登无限制
                Dim intBDFW As Integer
                intBDFW = CType(Xydc.Platform.Common.Data.BudengshezhiData.enumBudengfanweiType.All, Integer)
                strSQL = ""
                strSQL = strSQL + " select a.* from 管理_B_补登设置 a " + vbCr
                strSQL = strSQL + " left join (" + vbCr
                strSQL = strSQL + "   select * from 公共_B_上岗 " + vbCr
                strSQL = strSQL + "   where 人员代码 = '" + strUserId + "' " + vbCr
                strSQL = strSQL + " ) b on a.岗位代码 = b.岗位代码 " + vbCr
                strSQL = strSQL + " where b.岗位代码 is not null " + vbCr
                strSQL = strSQL + " and   a.补登范围 = " + intBDFW.ToString() + vbCr
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

                '获取指定人员的补登设置情况1
                Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate
                Dim strTemp As String = ""
                intBDFW = CType(Xydc.Platform.Common.Data.BudengshezhiData.enumBudengfanweiType.Zhiwu, Integer)
                strSQL = ""
                strSQL = strSQL + " select a.* from 管理_B_补登设置 a " + vbCr
                strSQL = strSQL + " left join (" + vbCr
                strSQL = strSQL + "   select * from 公共_B_上岗 " + vbCr
                strSQL = strSQL + "   where 人员代码 = '" + strUserId + "' " + vbCr
                strSQL = strSQL + " ) b on a.岗位代码 = b.岗位代码 " + vbCr
                strSQL = strSQL + " where b.岗位代码 is not null " + vbCr
                strSQL = strSQL + " and a.补登范围 = " + intBDFW.ToString() + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    '获取补登指定职务列表1
                    With objDataSet.Tables(0)
                        Dim intCount As Integer
                        Dim i As Integer
                        intCount = .Rows.Count
                        For i = 0 To intCount - 1 Step 1
                            If strTemp = "" Then
                                strTemp = objPulicParameters.getObjectValue(.Rows(i).Item("职务列表"), "").Trim()
                            Else
                                strTemp = strTemp + strSep + objPulicParameters.getObjectValue(.Rows(i).Item("职务列表"), "").Trim()
                            End If
                        Next
                    End With
                    Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                    objDataSet = Nothing
                    If strTemp <> "" Then
                        '检查指定文件中是否有指定部门的指定职务strTemp的人存在
                        strSQL = ""
                        strSQL = strSQL + " select * from 公文_B_交接 " + vbCr
                        strSQL = strSQL + " where 文件标识 = '" + strWJBS + "' " + vbCr
                        strSQL = strSQL + " and   rtrim(交接标识) like '1%' " + vbCr
                        strSQL = strSQL + " and   接收人 = '" + strJSR + "'" + vbCr
                        strSQL = strSQL + " and   接收人 in " + vbCr
                        strSQL = strSQL + " (" + vbCr
                        strSQL = strSQL + "   select b.人员名称 from 公共_B_上岗 a " + vbCr
                        strSQL = strSQL + "   left join 公共_B_人员     b on a.人员代码 = b.人员代码 " + vbCr
                        strSQL = strSQL + "   left join 公共_B_工作岗位 c on a.岗位代码 = c.岗位代码 " + vbCr
                        strSQL = strSQL + "   where b.人员名称 is not null " + vbCr
                        strSQL = strSQL + "   and   c.岗位名称 is not null " + vbCr
                        strSQL = strSQL + "   and '" + strTemp + "' + '" + strSep + "' like '%'+rtrim(c.岗位名称)+'" + strSep + "%' " + vbCr
                        strSQL = strSQL + "   group by b.人员名称" + vbCr
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

                '获取指定人员的补登设置情况2
                intBDFW = CType(Xydc.Platform.Common.Data.BudengshezhiData.enumBudengfanweiType.ZhiwuBumenLevel, Integer)
                strSQL = ""
                strSQL = strSQL + " select a.* from 管理_B_补登设置 a " + vbCr
                strSQL = strSQL + " left join (" + vbCr
                strSQL = strSQL + "   select * from 公共_B_上岗 " + vbCr
                strSQL = strSQL + "   where  人员代码 = '" + strUserId + "' " + vbCr
                strSQL = strSQL + " ) b on a.岗位代码 = b.岗位代码 " + vbCr
                strSQL = strSQL + " where b.岗位代码 is not null " + vbCr
                strSQL = strSQL + " and   a.补登范围 = " + intBDFW.ToString() + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    blnCanBudeng = False
                    Exit Try
                End If
                '获取补登指定职务列表2
                With objDataSet.Tables(0)
                    Dim blnDefined As Boolean
                    Dim intMinJSDM As Integer
                    Dim intMinJSXZ As Integer
                    Dim intCount As Integer
                    Dim i As Integer
                    intcount = .Rows.Count
                    For i = 0 To intcount - 1 Step 1
                        strTemp = ""
                        strTemp = objPulicParameters.getObjectValue(.Rows(i).Item("职务列表"), "").Trim()
                        intMinJSXZ = Xydc.Platform.Common.Data.CustomerData.intZZDM_FJCDSM.Length + 1      '组织代码最低级+1
                        intMinJSXZ = objPulicParameters.getObjectValue(.Rows(i).Item("级数限制"), intMinJSXZ)
                        If intMinJSXZ > Xydc.Platform.Common.Data.CustomerData.intZZDM_FJCDSM.Length Then
                            blnDefined = False
                        ElseIf intMinJSXZ < 1 Then
                            blnDefined = False
                        Else
                            intMinJSDM = Xydc.Platform.Common.Data.CustomerData.intZZDM_FJCDSM(intMinJSXZ - 1)
                            blnDefined = True
                        End If
                        If strTemp <> "" And blnDefined = True Then
                            '检查指定文件中是否有指定部门的指定职务strTemp的人存在
                            strSQL = ""
                            strSQL = strSQL + " select * from 公文_B_交接 " + vbCr
                            strSQL = strSQL + " where 文件标识 = '" + strWJBS + "' " + vbCr
                            strSQL = strSQL + " and   rtrim(交接标识) like '1_1%' " + vbCr
                            strSQL = strSQL + " and   接收人 = '" + strJSR + "'" + vbCr
                            strSQL = strSQL + " and   接收人 in " + vbCr
                            strSQL = strSQL + " (" + vbCr
                            strSQL = strSQL + "   select b.人员名称 from 公共_B_上岗 a " + vbCr
                            strSQL = strSQL + "   left join 公共_B_人员     b on a.人员代码 = b.人员代码 " + vbCr
                            strSQL = strSQL + "   left join 公共_B_工作岗位 c on a.岗位代码 = c.岗位代码 " + vbCr
                            strSQL = strSQL + "   where b.人员名称 is not null " + vbCr
                            strSQL = strSQL + "   and   b.组织代码 is not null " + vbCr
                            strSQL = strSQL + "   and   c.岗位名称 is not null " + vbCr
                            strSQL = strSQL + "   and '" + strTemp + "' + '" + strSep + "' like '%'+rtrim(c.岗位名称)+'" + strSep + "%' " + vbCr '指定职务
                            strSQL = strSQL + "   and len(rtrim(b.组织代码)) >= " + intMinJSDM.ToString() + vbCr                                 '指定级别以下
                            strSQL = strSQL + "   and rtrim(b.组织代码) like '" + strBMDM + "%' " + vbCr                                         '本级及下级单位
                            strSQL = strSQL + "   group by b.人员名称" + vbCr
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
        ' 判断指定人员strSender是否可以直接发送给strReceiver？
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strSender            ：发送人名称
        '     strSenderBMDM        ：发送人所属单位代码
        '     strReceiver          ：接收人名称
        '     blnCanSend           ：返回：是否可以？
        '     strNewReceiver       ：返回：转送人员名称
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strSender Is Nothing Then strSender = ""
                strSender = strSender.Trim()
                If strSenderBMDM Is Nothing Then strSenderBMDM = ""
                strSenderBMDM = strSenderBMDM.Trim()
                If strReceiver Is Nothing Then strReceiver = ""
                strReceiver = strReceiver.Trim()

                '自己给自己
                If strSender = strReceiver Then
                    blnCanSend = True
                    Exit Try
                End If

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                'strReceiver有无直接发送限制??
                strSQL = ""
                strSQL = strSQL + " select a.*," + vbCr
                strSQL = strSQL + "   其他由转送名称=b.人员名称 " + vbCr
                strSQL = strSQL + " from 公共_B_人员 a" + vbCr
                strSQL = strSQL + " left join 公共_B_人员 b on a.其他由转送 = b.人员代码 "
                strSQL = strSQL + " where a.人员名称 = '" + strReceiver + "' " + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    blnCanSend = True
                    Exit Try
                End If
                Dim strKZSRY As String = ""
                strKZSRY = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item("可直送人员"), "")
                If strKZSRY = "" Then
                    blnCanSend = True
                    Exit Try
                End If
                Dim strZSRY As String = ""
                strZSRY = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item("其他由转送名称"), "")
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '计算限制列表
                Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate
                Dim strKZSRYList As String
                If objdacCommon.doConvertToSQLValueList(strErrMsg, strKZSRY, strSep, strKZSRYList) = False Then
                    GoTo errProc
                End If

                '在可直接发送的部门内
                strSQL = ""
                strSQL = strSQL + " select count(*) from 公共_B_组织机构 " + vbCr
                strSQL = strSQL + " where 组织名称 in (" + strKZSRYList + ") " + vbCr
                strSQL = strSQL + " and '" + strSenderBMDM + "' like rtrim(组织代码) + '%'" + vbCr
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

                '在可直接发送的人员内
                strSQL = ""
                strSQL = strSQL + " select count(*) from 公共_B_人员 " + vbCr
                strSQL = strSQL + " where 人员名称 in (" + strKZSRYList + ") " + vbCr
                strSQL = strSQL + " and   人员名称 = '" + strSender + "' " + vbCr
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

                '不能直接发送
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
        ' 判断指定人员strSenderList是否可以直接发送给strReceiver？
        ' 只要有1个能直接发送就可以！
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strSenderList        ：发送人名称列表
        '     strReceiver          ：接收人名称
        '     blnCanSend           ：返回：是否可以？
        '     strNewReceiver       ：返回：转送人员名称
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strSenderList Is Nothing Then strSenderList = ""
                strSenderList = strSenderList.Trim()
                If strReceiver Is Nothing Then strReceiver = ""
                strReceiver = strReceiver.Trim()

                '自己给自己
                If strSenderList = strReceiver Then
                    blnCanSend = True
                    Exit Try
                End If

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                'strReceiver有无直接发送限制??
                strSQL = ""
                strSQL = strSQL + " select a.*," + vbCr
                strSQL = strSQL + "   其他由转送名称=b.人员名称 " + vbCr
                strSQL = strSQL + " from 公共_B_人员 a" + vbCr
                strSQL = strSQL + " left join 公共_B_人员 b on a.其他由转送 = b.人员代码 "
                strSQL = strSQL + " where a.人员名称 = '" + strReceiver + "' " + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    blnCanSend = True
                    Exit Try
                End If
                Dim strKZSRY As String = ""
                strKZSRY = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item("可直送人员"), "")
                If strKZSRY = "" Then
                    blnCanSend = True
                    Exit Try
                End If
                Dim strZSRY As String = ""
                strZSRY = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item("其他由转送名称"), "")
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '计算限制列表
                Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate
                Dim strKZSRYList As String
                If objdacCommon.doConvertToSQLValueList(strErrMsg, strKZSRY, strSep, strKZSRYList) = False Then
                    GoTo errProc
                End If

                '逐个检查发送人
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

                    '获取单位代码
                    strSQL = ""
                    strSQL = strSQL + " select 组织代码 "
                    strSQL = strSQL + " from 公共_B_人员 " + vbCr
                    strSQL = strSQL + " where 人员名称 = '" + strSender + "'" + vbCr
                    If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                        GoTo errProc
                    End If
                    If objDataSet.Tables(0).Rows.Count < 1 Then
                        strSenderBMDM = ""
                    Else
                        strSenderBMDM = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item("组织代码"), "")
                    End If
                    Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                    objDataSet = Nothing

                    '在可直接发送的部门内
                    strSQL = ""
                    strSQL = strSQL + " select count(*) from 公共_B_组织机构 " + vbCr
                    strSQL = strSQL + " where 组织名称 in (" + strKZSRYList + ") " + vbCr
                    strSQL = strSQL + " and '" + strSenderBMDM + "' like rtrim(组织代码) + '%'" + vbCr
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

                    '在可直接发送的人员内
                    strSQL = ""
                    strSQL = strSQL + " select count(*) from 公共_B_人员 " + vbCr
                    strSQL = strSQL + " where 人员名称 in (" + strKZSRYList + ") " + vbCr
                    strSQL = strSQL + " and   人员名称 = '" + strSender + "' " + vbCr
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

                '不能直接发送
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
        ' 判断指定人员strUserXM是否可以接收文件？
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：操作人员名称
        '     blnCanDoJieshou      ：返回：是否可以？
        '     strFSRList           ：返回：发送人名称列表
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strTaskStatusWJSList As String = Me.FlowData.TaskStatusWJSList
                Dim strWJBS As String = Me.WJBS

                '获取当前操作员未接收的交接单
                strSQL = ""
                strSQL = strSQL + " select 发送人 from 公文_B_交接 "
                strSQL = strSQL + " where 文件标识 = '" + strWJBS + "' "
                strSQL = strSQL + " and   rtrim(交接标识) like '1%' "
                strSQL = strSQL + " and   接收人   = '" + strUserXM + "' "
                strSQL = strSQL + " and   办理状态 in (" + strTaskStatusWJSList + ") "
                strSQL = strSQL + " group by 发送人 "
                strSQL = strSQL + " order by 发送人 "
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    Exit Try
                End If

                '计算发送人列表
                Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate
                Dim strFSR As String
                Dim intCount As Integer
                Dim i As Integer
                With objDataSet.Tables(0)
                    intCount = .Rows.Count
                    For i = 0 To intCount - 1 Step 1
                        strFSR = objPulicParameters.getObjectValue(.Rows(i).Item("发送人"), "")
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
        ' 获取strUserXM的可以督办哪些人员?
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：人员名称
        '     strUserId            ：人员代码
        '     strBMDM              ：strUserXM所属单位代码
        '     strRYLIST            ：返回人员列表,标准分隔符分隔
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '交接办理完毕状态SQL列表
                Dim strTaskStatusYWCList As String = Me.FlowData.TaskStatusYWCList

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '计算没有办理完毕、正常处理的交接单
                strSQL = ""
                strSQL = strSQL + " select * from 公文_B_交接 " + vbCr
                strSQL = strSQL + " where 文件标识 = '" + strWJBS + "' " + vbCr                       '当前文件
                strSQL = strSQL + " and   rtrim(交接标识) like '1_1__0%' " + vbCr                            '已发送+接收人能看+非通知
                strSQL = strSQL + " and   办理状态 not in (" + strTaskStatusYWCList + ") " + vbCr     '未完成
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If

                '合并
                With objDataSet.Tables(0)
                    Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate
                    Dim blnCanDuban As Boolean
                    Dim intCount As Integer
                    Dim strJSR As String
                    Dim i As Integer
                    intCount = .Rows.Count
                    For i = 0 To intCount Step 1
                        strJSR = objPulicParameters.getObjectValue(.Rows(i).Item("接收人"), "")
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
        ' 获取指定人员strUserXM的被催办数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：人员名称
        '     objBeicuibanData     ：返回数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '创建数据集
                objTempBeicuibanData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_CUIBAN_JIAOJIE)
                If strWJBS = "" Then Exit Try

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '执行检索
                With Me.m_objSqlDataAdapter
                    '获取指定人员已经被催办的情况
                    strSQL = ""
                    strSQL = strSQL + " select a.*, b.办理子类, b.办理状态 from " + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select * from 公文_B_催办 " + vbCr
                    strSQL = strSQL + "   where 文件标识 = '" + strWJBS + "' " + vbCr
                    strSQL = strSQL + "   and   被催办人 = '" + strUserXM + "' " + vbCr
                    strSQL = strSQL + " ) a " + vbCr
                    strSQL = strSQL + " left join " + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select * from 公文_B_交接 " + vbCr
                    strSQL = strSQL + "   where 文件标识 = '" + strWJBS + "' " + vbCr
                    strSQL = strSQL + " ) b on a.文件标识 = b.文件标识 and a.交接序号 = b.交接序号 " + vbCr
                    strSQL = strSQL + " order by a.催办序号 " + vbCr

                    '设置参数
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    '执行操作
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
        ' 获取strUserXM还没有接收哪些人员送来的交接单?
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：人员名称
        '     strRYLIST            ：返回人员列表,标准分隔符分隔
        '     blnJieshouAll        ：返回是否全部接收?
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '交接未接收状态SQL列表
                Dim strTaskStatusWJSList As String = Me.FlowData.TaskStatusWJSList

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '获取当前操作员未接收的交接单
                strSQL = ""
                strSQL = strSQL + " select 发送人 from 公文_B_交接 " + vbCr
                strSQL = strSQL + " where 文件标识 = '" + strWJBS + "' " + vbCr
                strSQL = strSQL + " and   rtrim(交接标识) like '1%' " + vbCr
                strSQL = strSQL + " and   接收人   = '" + strUserXM + "' " + vbCr
                strSQL = strSQL + " and   办理状态 in (" + strTaskStatusWJSList + ") " + vbCr
                strSQL = strSQL + " group by 发送人 " + vbCr
                strSQL = strSQL + " order by 发送人 " + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    blnJieshouAll = True
                    Exit Try
                End If

                '合并
                With objDataSet.Tables(0)
                    Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate
                    Dim intCount As Integer
                    Dim strFSR As String
                    Dim i As Integer
                    intCount = .Rows.Count
                    For i = 0 To intCount Step 1
                        strFSR = objPulicParameters.getObjectValue(.Rows(i).Item("发送人"), "")
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
        ' 获取文件的编辑封锁信息?
        '     strErrMsg            ：如果错误，则返回错误信息
        '     blnLocked            ：返回是否封锁?
        '     strBMMC              ：如果封锁，则返回编辑人员所在单位名称
        '     strRYMC              ：如果封锁，则返回编辑人员名称
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '获取正在编辑本文件的人员、部门信息
                strSQL = ""
                strSQL = strSQL + " select a.*, " + vbCr
                strSQL = strSQL + "   b.人员名称, c.组织名称 " + vbCr
                strSQL = strSQL + " from 管理_B_文件封锁 a " + vbCr
                strSQL = strSQL + " left join 公共_B_人员     b on a.人员代码 = b.人员代码 " + vbCr
                strSQL = strSQL + " left join 公共_B_组织机构 c on b.组织代码 = c.组织代码 " + vbCr
                strSQL = strSQL + " where a.文件标识 = '" + strWJBS + "'" + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    Exit Try
                End If

                '返回信息
                blnLocked = True
                With objDataSet.Tables(0).Rows(0)
                    strBMMC = objPulicParameters.getObjectValue(.Item("组织名称"), "")
                    strRYMC = objPulicParameters.getObjectValue(.Item("人员名称"), "")
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
        ' 获取新的文件标识
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strWJBS              ：返回文件标识
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection

                '检索数据
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
        ' 获取新的发送序号
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strFSXH              ：返回发送序号
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If Me.m_blnFillData = False Then
                    strErrMsg = "错误：对象还没有填充数据，不能使用！"
                    GoTo errProc
                End If

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.FlowData.WJBS

                '检索数据
                If objdacCommon.getNewCode(strErrMsg, objSqlConnection, "发送序号", "文件标识", strWJBS, "公文_B_交接", True, strFSXH) = False Then
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
        ' 获取strUserXM最后1次的正常处理的交接单(不论是否办完！)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：用户名称
        '     objJiaoJieData       ：返回最后1次交接数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '计算最近交接序号，准则：接收人可看本交接、发送、正常消息
                strSQL = ""
                strSQL = strSQL + " select isnull(max(交接序号),0) as 交接序号 " + vbCr
                strSQL = strSQL + " from 公文_B_交接 " + vbCr
                strSQL = strSQL + " where 文件标识 = '" + strWJBS + "' " + vbCr           '当前文件
                strSQL = strSQL + " and   接收人   = '" + strUserXM + "' " + vbCr         '接收人
                strSQL = strSQL + " and   rtrim(交接标识) like '__1__0_%' " + vbCr               '接收人能看+非通知类
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                Dim intXH As Integer
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    intXH = 0
                Else
                    With objDataSet.Tables(0).Rows(0)
                        intXH = objPulicParameters.getObjectValue(.Item("交接序号"), 0)
                    End With
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '创建数据集
                objTempJiaoJieData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_JIAOJIE)
                If strWJBS = "" Then Exit Try

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '执行检索
                With Me.SqlDataAdapter
                    '计算交接信息
                    strSQL = ""
                    strSQL = strSQL + " select * from 公文_B_交接 " + vbCr
                    strSQL = strSQL + " where 文件标识 = '" + strWJBS + "' " + vbCr
                    strSQL = strSQL + " and   接收人   = '" + strUserXM + "' " + vbCr
                    strSQL = strSQL + " and   交接序号 = " + intXH.ToString() + vbCr

                    '设置参数
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    '执行操作
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
        ' 获取strUserXM最后1次的正常处理的未办理完毕的交接单
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：用户名称
        '     objJiaoJieData       ：返回最后1次交接数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strTaskStatusZJBList As String = Me.FlowData.TaskStatusZJBList
                Dim strWJBS As String = Me.WJBS

                '计算最近交接序号，准则：接收人可看本交接、发送、正常消息
                strSQL = ""
                strSQL = strSQL + " select isnull(max(交接序号),0) as 交接序号 " + vbCr
                strSQL = strSQL + " from 公文_B_交接 " + vbCr
                strSQL = strSQL + " where 文件标识 = '" + strWJBS + "' " + vbCr                       '当前文件
                strSQL = strSQL + " and   接收人   = '" + strUserXM + "' " + vbCr                     '接收人
                strSQL = strSQL + " and   办理状态 in (" + strTaskStatusZJBList + ")" + vbCr          '接收人未办完
                strSQL = strSQL + " and   rtrim(交接标识) like '__1__0_%' " + vbCr                    '接收人能看+非通知类
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                Dim intXH As Integer
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    intXH = 0
                Else
                    intXH = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item("交接序号"), 0)
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '创建数据集
                objTempJiaoJieData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_JIAOJIE)
                If strWJBS = "" Then Exit Try

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '执行检索
                With Me.SqlDataAdapter
                    '计算交接信息
                    strSQL = ""
                    strSQL = strSQL + " select * from 公文_B_交接 " + vbCr
                    strSQL = strSQL + " where 文件标识 = '" + strWJBS + "' " + vbCr
                    strSQL = strSQL + " and   接收人   = '" + strUserXM + "' " + vbCr
                    strSQL = strSQL + " and   交接序号 = " + intXH.ToString() + vbCr

                    '设置参数
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    '执行操作
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
        ' 根据给定状态strStatus获取正常处理的交接单
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strStatus            ：交接状态SQL值列表
        '     objJiaojieData       ：返回交接单
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strStatus Is Nothing Then strStatus = ""
                strStatus = strStatus.Trim()

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '创建数据集
                objTempJiaojieData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_JIAOJIE)
                If strWJBS = "" Then Exit Try

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '执行检索
                With Me.m_objSqlDataAdapter
                    '获取指定状态的交接处理单
                    strSQL = ""
                    strSQL = strSQL + " select * from 公文_B_交接 " + vbCr
                    strSQL = strSQL + " where 文件标识 = '" + strWJBS + "' " + vbCr
                    strSQL = strSQL + " and   rtrim(交接标识) like '1____0%' " + vbCr
                    strSQL = strSQL + " and   办理状态 in (" + strStatus + ")" + vbCr

                    '设置参数
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    '执行操作
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
        ' 根据strWJBS获取交接单
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strWhere             ：搜索条件
        '     blnUnused            ：接口重载
        '     objJiaoJieData       ：返回交接数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        ' 修改记录
        '      增加
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '创建数据集
                objTempJiaoJieData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_JIAOJIE)
                If strWJBS = "" Then Exit Try

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '执行检索
                With Me.SqlDataAdapter
                    '计算交接信息
                    strSQL = ""
                    strSQL = strSQL + " select * from 公文_B_交接 a" + vbCr
                    strSQL = strSQL + " where a.文件标识 = '" + strWJBS + "' " + vbCr
                    If strWhere <> "" Then
                        strSQL = strSQL + " and " + strWhere + vbCr
                    End If
                    strSQL = strSQL + " order by a.文件标识,a.交接序号" + vbCr

                    '设置参数
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    '执行操作
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
        ' 根据intXH获取交接单
        '     strErrMsg            ：如果错误，则返回错误信息
        '     intXH                ：交接序号
        '     objJiaoJieData       ：返回交接数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '创建数据集
                objTempJiaoJieData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_JIAOJIE)
                If strWJBS = "" Then Exit Try

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '执行检索
                With Me.SqlDataAdapter
                    '计算交接信息
                    strSQL = ""
                    strSQL = strSQL + " select * from 公文_B_交接 " + vbCr
                    strSQL = strSQL + " where 文件标识 = '" + strWJBS + "' " + vbCr
                    strSQL = strSQL + " and   交接序号 = " + intXH.ToString() + vbCr

                    '设置参数
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    '执行操作
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
        ' 根据strWJBS获取交接单
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objJiaoJieData       ：返回交接数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '创建数据集
                objTempJiaoJieData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_JIAOJIE)
                If strWJBS = "" Then Exit Try

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '执行检索
                With Me.SqlDataAdapter
                    '计算交接信息
                    strSQL = ""
                    strSQL = strSQL + " select * from 公文_B_交接 " + vbCr
                    strSQL = strSQL + " where 文件标识 = '" + strWJBS + "' " + vbCr

                    '设置参数
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    '执行操作
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
        ' 获取strUserXM没有办完的事宜
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：用户名称
        '     objJiaoJieData       ：返回交接数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strTaskStatusYWCList As String = Me.FlowData.TaskStatusYWCList
                Dim strWJBS As String = Me.WJBS

                '创建数据集
                objTempJiaoJieData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_JIAOJIE)
                If strWJBS = "" Then Exit Try

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '执行检索
                With Me.SqlDataAdapter
                    '计算交接信息
                    strSQL = ""
                    strSQL = strSQL + " select * from 公文_B_交接" + vbCr
                    strSQL = strSQL + " where 文件标识 = '" + strWJBS + "'" + vbCr                     '当前文件
                    strSQL = strSQL + " and   接收人   = '" + strUserXM + "'" + vbCr                   '接收人
                    strSQL = strSQL + " and   rtrim(交接标识) like '__1__0%'" + vbCr                   '接收人能看+非通知类事宜
                    strSQL = strSQL + " and   办理状态 not in (" + strTaskStatusYWCList + ")" + vbCr   '接收人未办完

                    '设置参数
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    '执行操作
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
        ' 获取审批文件的附件信息
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strFJNR              ：返回附件的序号与说明信息
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '打开附件列表
                strSQL = ""
                strSQL = strSQL + " select * from 公文_B_附件 " + vbCr
                strSQL = strSQL + " where 文件标识 = '" + strWJBS + "'" + vbCr
                strSQL = strSQL + " order by 序号" + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    Exit Try
                End If

                '返回信息
                Dim strInfo As String
                Dim strSM As String
                Dim strXH As String
                Dim intCount As Integer
                Dim i As Integer
                With objDataSet.Tables(0)
                    intCount = .Rows.Count
                    For i = 0 To intCount - 1 Step 1
                        strXH = objPulicParameters.getObjectValue(.Rows(i).Item("序号"), "")
                        strSM = objPulicParameters.getObjectValue(.Rows(i).Item("说明"), "")
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
        ' 获取正式文件的附件信息
        '     strErrMsg            ：如果错误，则返回错误信息
        '     blnZSWJ              ：重载用
        '     strFJNR              ：返回附件的序号与说明信息
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '打开附件列表
                strSQL = ""
                strSQL = strSQL + " select * from 公文_B_附件 " + vbCr
                strSQL = strSQL + " where 文件标识 = '" + strWJBS + "'" + vbCr
                strSQL = strSQL + " order by 序号" + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    Exit Try
                End If

                '返回信息
                Dim strInfo As String
                Dim strSM As String
                Dim strXH As String
                Dim intCount As Integer
                Dim i As Integer
                With objDataSet.Tables(0)
                    intCount = .Rows.Count
                    For i = 0 To intCount - 1 Step 1
                        strXH = objPulicParameters.getObjectValue(.Rows(i).Item("序号"), "")
                        strSM = objPulicParameters.getObjectValue(.Rows(i).Item("说明"), "")
                        strInfo = strXH + ". " + strSM

                        If strFJNR = "" Then
                            strFJNR = strInfo
                        Else
                            strFJNR = strFJNR + Chr(13) + Chr(10) + "          " + strInfo
                        End If
                    Next
                    strFJNR = "    附件：" + strFJNR
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
        ' 获取strUserXM未完成的审批事宜的数目
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：用户名称
        '     intNum               ：返回未完成的审批事宜的数目
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strTaskStatusYWCList As String = Me.FlowData.TaskStatusYWCList
                Dim strTaskBlzlSPSYList As String = Me.FlowData.TaskBlzlSPSYList
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '计算
                strSQL = ""
                strSQL = strSQL + " select count(*) "
                strSQL = strSQL + " from 公文_B_交接 "
                strSQL = strSQL + " where 文件标识 = '" + strWJBS + "' "
                strSQL = strSQL + " and   接收人   = '" + strUserXM + "' "
                strSQL = strSQL + " and   办理子类 in (" + strTaskBlzlSPSYList + ") "
                strSQL = strSQL + " and   办理状态 not in (" + strTaskStatusYWCList + ") "
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    Exit Try
                End If

                '返回信息
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
        ' 获取文件的相关文件信息(相关文件+相关附件)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objXGWJData          ：返回数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '创建数据集
                objTempXGWJData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_SHENPIWENJIAN_FUJIAN)
                If strWJBS = "" Then Exit Try

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '执行检索
                Dim intXGWJLB_Fujian As Integer = Xydc.Platform.Common.Data.FlowData.enumXGWJLB.FujianFile
                Dim intXGWJLB_File As Integer = Xydc.Platform.Common.Data.FlowData.enumXGWJLB.FlowFile
                With Me.m_objSqlDataAdapter
                    '获本文件的所有相关文件的具体信息
                    strSQL = ""
                    strSQL = strSQL + " select a.*" + vbCr
                    strSQL = strSQL + " from " + vbCr
                    strSQL = strSQL + " (" + vbCr
                    '**********************************************************************************
                    strSQL = strSQL + "   select a.*" + vbCr
                    strSQL = strSQL + "   from" + vbCr
                    strSQL = strSQL + "   (" + vbCr
                    strSQL = strSQL + "     select "
                    strSQL = strSQL + "       b.文件标识," + vbCr
                    strSQL = strSQL + "       b.文件类型," + vbCr
                    strSQL = strSQL + "       b.办理类型," + vbCr
                    strSQL = strSQL + "       b.文件子类," + vbCr
                    strSQL = strSQL + "       b.主送单位," + vbCr
                    strSQL = strSQL + "       b.文件标题," + vbCr
                    strSQL = strSQL + "       b.机关代字," + vbCr
                    strSQL = strSQL + "       b.文件年份," + vbCr
                    strSQL = strSQL + "       b.文件序号," + vbCr
                    strSQL = strSQL + "       b.文件年度," + vbCr
                    strSQL = strSQL + "       b.主办单位," + vbCr
                    strSQL = strSQL + "       b.拟稿人," + vbCr
                    strSQL = strSQL + "       b.拟稿日期," + vbCr
                    strSQL = strSQL + "       b.办理状态," + vbCr
                    strSQL = strSQL + "       b.流水号," + vbCr
                    strSQL = strSQL + "       b.主题词," + vbCr
                    strSQL = strSQL + "       b.快速收文," + vbCr
                    '**********************************************************************************
                    strSQL = strSQL + "       类别标识 = " + intXGWJLB_File.ToString() + "," + vbCr
                    strSQL = strSQL + "       序号     = a.顺序号," + vbCr
                    strSQL = strSQL + "       页数     = 0," + vbCr
                    strSQL = strSQL + "       位置     = ' '," + vbCr
                    '**********************************************************************************
                    strSQL = strSQL + "       显示序号 = a.顺序号," + vbCr
                    strSQL = strSQL + "       本地文件 = ''," + vbCr
                    strSQL = strSQL + "       下载标志 = 0" + vbCr
                    strSQL = strSQL + "     from" + vbCr
                    strSQL = strSQL + "     (" + vbCr
                    strSQL = strSQL + "       select 当前文件标识,顺序号 " + vbCr
                    strSQL = strSQL + "       from 公文_B_相关文件" + vbCr
                    strSQL = strSQL + "       where 上级文件标识 = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + "     ) a" + vbCr
                    strSQL = strSQL + "     left join" + vbCr
                    strSQL = strSQL + "     (" + vbCr
                    strSQL = strSQL + "       select * " + vbCr
                    strSQL = strSQL + "       from 公文_V_全部审批文件新" + vbCr
                    strSQL = strSQL + "     ) b on a.当前文件标识 = b.文件标识" + vbCr
                    strSQL = strSQL + "     where b.文件标识 is not null" + vbCr
                    strSQL = strSQL + "   ) a" + vbCr
                    '**********************************************************************************
                    strSQL = strSQL + "   union" + vbCr
                    '**********************************************************************************
                    '获取相关附件
                    strSQL = strSQL + "   select " + vbCr
                    strSQL = strSQL + "     文件标识," + vbCr
                    strSQL = strSQL + "     文件类型 = '附件'," + vbCr
                    strSQL = strSQL + "     办理类型 = '附件'," + vbCr
                    strSQL = strSQL + "     文件子类 = '附件'," + vbCr
                    strSQL = strSQL + "     主送单位 = ' '," + vbCr
                    strSQL = strSQL + "     文件标题 = 说明," + vbCr
                    strSQL = strSQL + "     机关代字 = ' '," + vbCr
                    strSQL = strSQL + "     文件年份 = ' '," + vbCr
                    strSQL = strSQL + "     文件序号 = ' '," + vbCr
                    strSQL = strSQL + "     文件年度 = 0," + vbCr
                    strSQL = strSQL + "     主办单位 = ' '," + vbCr
                    strSQL = strSQL + "     拟稿人   = ' '," + vbCr
                    strSQL = strSQL + "     拟稿日期 = null," + vbCr
                    strSQL = strSQL + "     办理状态 = ' '," + vbCr
                    strSQL = strSQL + "     流水号   = ' '," + vbCr
                    strSQL = strSQL + "     主题词   = ' '," + vbCr
                    strSQL = strSQL + "     快速收文 = 0," + vbCr
                    '**********************************************************************************
                    strSQL = strSQL + "     类别标识 = " + intXGWJLB_Fujian.ToString() + "," + vbCr
                    strSQL = strSQL + "     序号," + vbCr
                    strSQL = strSQL + "     页数," + vbCr
                    strSQL = strSQL + "     位置," + vbCr
                    '**********************************************************************************
                    strSQL = strSQL + "     显示序号 = 序号," + vbCr
                    strSQL = strSQL + "     本地文件 = ''," + vbCr
                    strSQL = strSQL + "     下载标志 = 0" + vbCr
                    strSQL = strSQL + "   from 公文_B_相关文件附件 " + vbCr
                    strSQL = strSQL + "   where 文件标识 = '" + strWJBS + "'" + vbCr
                    '**********************************************************************************
                    strSQL = strSQL + " ) a" + vbCr
                    strSQL = strSQL + " order by a.显示序号" + vbCr

                    '设置参数
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    '执行操作
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
        ' 根据文件序号获取相关文件附件的特定附件信息
        '     strErrMsg            ：如果错误，则返回错误信息
        '     intWJXH              ：文件序号
        '     objXgwjFujianData    ：返回数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '创建数据集
                objTempXgwjFujianData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_XIANGGUANWENJIANFUJIAN)
                If strWJBS = "" Then Exit Try

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '执行检索
                With Me.m_objSqlDataAdapter
                    '获取附件数据
                    strSQL = ""
                    strSQL = strSQL + " select a.*" + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select *,"
                    strSQL = strSQL + "     显示序号 = 序号,"
                    strSQL = strSQL + "     本地文件 = '',"
                    strSQL = strSQL + "     下载标志 = 0 " + vbCr
                    strSQL = strSQL + "   from 公文_B_相关文件附件 " + vbCr
                    strSQL = strSQL + "   where 文件标识 = @wjbs" + vbCr
                    strSQL = strSQL + "   and   序号     = @wjxh" + vbCr
                    strSQL = strSQL + " ) a" + vbCr
                    strSQL = strSQL + " order by a.显示序号" + vbCr

                    '设置参数
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                    objSqlCommand.Parameters.AddWithValue("@wjxh", intWJXH)
                    .SelectCommand = objSqlCommand

                    '执行操作
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
        ' 获取文件的附件信息
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objFujianData        ：返回数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '创建数据集
                objTempFujianData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_FUJIAN)
                If strWJBS = "" Then Exit Try

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '执行检索
                With Me.m_objSqlDataAdapter
                    '获取附件数据
                    strSQL = ""
                    strSQL = strSQL + " select a.*" + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select *,"
                    strSQL = strSQL + "     显示序号 = 序号,"
                    strSQL = strSQL + "     本地文件 = '',"
                    strSQL = strSQL + "     下载标志 = 0 " + vbCr
                    strSQL = strSQL + "   from 公文_B_附件 " + vbCr
                    strSQL = strSQL + "   where 文件标识 = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + " ) a" + vbCr
                    strSQL = strSQL + " order by a.显示序号" + vbCr

                    '设置参数
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    '执行操作
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
        ' 根据文件序号获取文件的特定附件信息
        '     strErrMsg            ：如果错误，则返回错误信息
        '     intWJXH              ：文件序号
        '     objFujianData        ：返回数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '创建数据集
                objTempFujianData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_FUJIAN)
                If strWJBS = "" Then Exit Try

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '执行检索
                With Me.m_objSqlDataAdapter
                    '获取附件数据
                    strSQL = ""
                    strSQL = strSQL + " select a.*" + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select *,"
                    strSQL = strSQL + "     显示序号 = 序号,"
                    strSQL = strSQL + "     本地文件 = '',"
                    strSQL = strSQL + "     下载标志 = 0 " + vbCr
                    strSQL = strSQL + "   from 公文_B_附件 " + vbCr
                    strSQL = strSQL + "   where 文件标识 = @wjbs" + vbCr
                    strSQL = strSQL + "   and   序号     = @wjxh" + vbCr
                    strSQL = strSQL + " ) a" + vbCr
                    strSQL = strSQL + " order by a.显示序号" + vbCr

                    '设置参数
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                    objSqlCommand.Parameters.AddWithValue("@wjxh", intWJXH)
                    .SelectCommand = objSqlCommand

                    '执行操作
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
        ' 获取第1个发送给strUserXM的发送人名称
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：用户名称
        '     strFirstSender       ：返回发送人名称
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '计算
                strSQL = ""
                strSQL = strSQL + " select * from 公文_B_交接 " + vbCr
                strSQL = strSQL + " where 文件标识 = '" + strWJBS + "' " + vbCr
                strSQL = strSQL + " and   接收人   = '" + strUserXM + "' " + vbCr
                strSQL = strSQL + " order by 交接序号" + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    Exit Try
                End If

                '返回信息
                With objDataSet.Tables(0).Rows(0)
                    strFirstSender = objPulicParameters.getObjectValue(.Item("发送人"), "")
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
        ' 获取指定人员strCurUser是否可以在交接记录中查看strCheckUser的名称
        '     strErrMsg             ：如果错误，则返回错误信息
        '     strCurUser            ：当前人员名称
        '     strCurUserBMDM        ：当前人员所属单位代码
        '     strCheckUser          ：检查的人员名称
        '     strNewName            ：返回：要显示的名称
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strCurUser Is Nothing Then strCurUser = ""
                strCurUser = strCurUser.Trim()
                If strCurUserBMDM Is Nothing Then strCurUserBMDM = ""
                strCurUserBMDM = strCurUserBMDM.Trim()
                If strCheckUser Is Nothing Then strCheckUser = ""
                strCheckUser = strCheckUser.Trim()

                '自己看自己
                If strCurUser = strCheckUser Then
                    Exit Try
                End If

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                'strCheckUser有无查看限制??
                strSQL = ""
                strSQL = strSQL + " select a.* " + vbCr
                strSQL = strSQL + " from 公共_B_人员 a" + vbCr
                strSQL = strSQL + " where a.人员名称 = '" + strCheckUser + "' " + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    Exit Try
                End If
                Dim strKCKXM As String = ""
                strKCKXM = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item("可查看姓名"), "")
                If strKCKXM = "" Then
                    Exit Try
                End If
                Dim strJJXSMC As String = ""
                strJJXSMC = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item("交接显示名称"), "")
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '计算限制列表
                Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate
                Dim strKCKXMList As String
                If objdacCommon.doConvertToSQLValueList(strErrMsg, strKCKXM, strSep, strKCKXMList) = False Then
                    GoTo errProc
                End If

                '在可查看的部门内
                strSQL = ""
                strSQL = strSQL + " select count(*) from 公共_B_组织机构 " + vbCr
                strSQL = strSQL + " where 组织名称 in (" + strKCKXMList + ") " + vbCr
                strSQL = strSQL + " and '" + strCurUserBMDM + "' like rtrim(组织代码) + '%'" + vbCr
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

                '在可查看的人员内
                strSQL = ""
                strSQL = strSQL + " select count(*) from 公共_B_人员 " + vbCr
                strSQL = strSQL + " where 人员名称 in (" + strKCKXMList + ") " + vbCr
                strSQL = strSQL + " and   人员名称 = '" + strCurUser + "' " + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                intCount = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item(0), 0)
                If intCount > 0 Then
                    Exit Try
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '不能查看
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
        ' 获取指定交接对应的审批意见类型
        '     strErrMsg             ：如果错误，则返回错误信息
        '     intJJXH               ：交接序号
        '     strType               ：返回：审批意见类型
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '计算
                strSQL = ""
                strSQL = strSQL + " select 办理子类 "
                strSQL = strSQL + " from 公文_B_办理 "
                strSQL = strSQL + " where 文件标识 = '" + strWJBS + "' "
                strSQL = strSQL + " and   交接序号 = " + intJJXH.ToString() + " "
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    Exit Try
                End If

                '返回
                strType = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item("办理子类"), "")

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
        ' 获取批示意见与相应便笺意见
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objOpinionData       ：要显示的意见信息
        '     strYJLX              ：要显示的意见类型(办理表中的办理子类)
        '     strQSYJ              ：返回：正常意见
        '     strBJYJ              ：返回：便笺意见
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查信息
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

                '逐条处理
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
                        '获取信息
                        strJSR = objPulicParameters.getObjectValue(.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIYIJIAN_JSR), "")
                        strBLRQ = objPulicParameters.getObjectValue(.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIYIJIAN_BLRQ), "")
                        strBLYJ = objPulicParameters.getObjectValue(.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIYIJIAN_BLYJ), "")
                        strBJNR = objPulicParameters.getObjectValue(.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIYIJIAN_BJNR), "")
                        If strBLRQ <> "" Then
                            strBLRQ = Format(System.DateTime.Parse(strBLRQ), "yyyy-MM-dd")
                        End If

                        '当前审批意见
                        strTempYJ = ""
                        If strBLYJ <> "" Then
                            strTempYJ = strTempYJ + strBLYJ + Chr(13) + Chr(10)
                            strTempYJ = strTempYJ + "    " + strJSR + "  " + strBLRQ + Chr(13) + Chr(10)
                        End If
                        '复合意见
                        If strTempYJ <> "" Then
                            If strQSYJ = "" Then
                                strQSYJ = strTempYJ
                            Else
                                strQSYJ = strQSYJ + strTempYJ
                            End If
                        End If

                        '当前便笺意见
                        strTempYJ = ""
                        If strBJNR <> "" Then
                            strTempYJ = strTempYJ + strBJNR + Chr(13) + Chr(10)
                            strTempYJ = strTempYJ + "    " + strJSR + "  " + strBLRQ + Chr(13) + Chr(10)
                        End If
                        '复合意见
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
        ' 获取指定人员strUserXM受strWTR委托处理业务
        '     strErrMsg             ：如果错误，则返回错误信息
        '     strUserXM             ：人员名称
        '     strWTR                ：返回：委托人
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strTaskStatusYWCList As String = Me.FlowData.TaskStatusYWCList
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '获取strUseXM未处理完的非通知事宜中的委托人信息
                strSQL = ""
                strSQL = strSQL + " select * from 公文_B_交接 " + vbCr
                strSQL = strSQL + " where 文件标识 = '" + strWJBS + "' " + vbCr
                strSQL = strSQL + " and   rtrim(交接标识) like '__1__0%' " + vbCr
                strSQL = strSQL + " and   接收人   = '" + strUserXM + "' " + vbCr
                strSQL = strSQL + " and   办理状态 not in (" + strTaskStatusYWCList + ") " + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    Exit Try
                End If

                '返回
                Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate
                Dim strName As String = ""
                Dim strTemp As String = ""
                Dim intCount As Integer
                Dim i As Integer
                With objDataSet.Tables(0)
                    intCount = .Rows.Count
                    For i = 0 To intCount - 1 Step 1
                        strName = ""
                        strName = objPulicParameters.getObjectValue(.Rows(i).Item("委托人"), "")
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
        ' 获取strUserXM能够查看的工作流文件数据集
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：用户名称
        '     strWhere             ：搜索条件
        '     objFileDataSet       ：返回工作流文件数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.IsInitialized = False Then
                    strErrMsg = "错误：工作流对象没有初始化！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim
                If strUserXM = "" Then
                    strErrMsg = "错误：没有输入用户标识！"
                    GoTo errProc
                End If
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim

                '获取连接
                objSqlConnection = Me.SqlConnection

                '创建数据集
                objTempFileDataSet = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_V_SHENPIWENJIAN_NEW)

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '执行检索
                With Me.SqlDataAdapter
                    '计算信息
                    strSQL = ""
                    strSQL = strSQL + " select a.*" + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select a.*" + vbCr
                    strSQL = strSQL + "   from" + vbCr
                    strSQL = strSQL + "   (" + vbCr
                    strSQL = strSQL + "     select a.* " + vbCr
                    strSQL = strSQL + "     from 公文_V_全部审批文件新 a" + vbCr
                    If strWhere <> "" Then
                        strSQL = strSQL + "     where " + strWhere + vbCr
                    End If
                    strSQL = strSQL + "   ) a" + vbCr
                    strSQL = strSQL + "   left join" + vbCr
                    strSQL = strSQL + "   (" + vbCr
                    strSQL = strSQL + "     select 文件标识" + vbCr
                    strSQL = strSQL + "     from 公文_B_交接" + vbCr
                    strSQL = strSQL + "     where ((发送人 = '" + strUserXM + "' and rtrim(交接标识) like '_1%') " + vbCr
                    strSQL = strSQL + "     or     (接收人 = '" + strUserXM + "' and rtrim(交接标识) like '__1%')) " + vbCr
                    strSQL = strSQL + "     group by 文件标识" + vbCr
                    strSQL = strSQL + "   ) b on a.文件标识 = b.文件标识" + vbCr
                    strSQL = strSQL + "   where b.文件标识 is not null" + vbCr
                    strSQL = strSQL + " ) a" + vbCr
                    strSQL = strSQL + " order by a.拟稿日期 desc" + vbCr

                    '设置参数
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    '执行操作
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
        ' 文件是否发送过?
        '     strErrMsg            ：如果错误，则返回错误信息
        '     blnHasSend           ：返回是否发送过?
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '获取数据
                strSQL = ""
                strSQL = strSQL + " select * from 公文_B_交接 " + vbCr
                strSQL = strSQL + " where 文件标识 = '" + strWJBS + "' " + vbCr
                strSQL = strSQL + " and   rtrim(交接标识) like '1%' " + vbCr
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
        ' strUserXM是否为自动签收文件？
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：用户名称
        '     blnAutoReceive       ：返回是否?
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '打开人员表
                strSQL = ""
                strSQL = strSQL + " select * from 公共_B_人员 " + vbCr
                strSQL = strSQL + " where 人员名称 = '" + strUserXM + "'" + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    Exit Try
                End If

                '返回信息
                Dim strZDQS As String
                With objDataSet.Tables(0).Rows(0)
                    strZDQS = objPulicParameters.getObjectValue(.Item("自动签收"), "")
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
        ' 文件是否发送过？
        '     strErrMsg            ：如果错误，则返回错误信息
        '     blnSendOnce          ：返回是否?
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '检查
                strSQL = ""
                strSQL = strSQL + " select * from 公文_B_交接 "
                strSQL = strSQL + " where 文件标识 = '" + strWJBS + "' "
                strSQL = strSQL + " and   rtrim(交接标识) like '1%' "
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    Exit Try
                End If

                '返回信息
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
        ' strUserXM是否收到纸质文件的交接单？(从“未办事宜”中检索)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：用户名称
        '     blnReceive           ：返回是否?
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strTaskStatusYWCList As String = Me.FlowData.TaskStatusYWCList
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '检查
                strSQL = ""
                strSQL = strSQL + " select * from 公文_B_交接 " + vbCr
                strSQL = strSQL + " where 文件标识 =  '" + strWJBS + "' " + vbCr                   '当前文件
                strSQL = strSQL + " and   接收人   =  '" + strUserXM + "' " + vbCr                 '接收的
                strSQL = strSQL + " and   rtrim(交接标识) like '__1%' " + vbCr                            '接收人能看
                strSQL = strSQL + " and   办理状态 not in (" + strTaskStatusYWCList + ")" + vbCr   '接收人未办完
                strSQL = strSQL + " and  (发送纸质文件 > 0 or 发送纸质附件 > 0) " + vbCr           '有纸质文件
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
        ' strUserXM是否发送纸质文件的交接单？(从“未办事宜”中检索)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：用户名称
        '     blnSend              ：返回是否?
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strTaskStatusYWCList As String = Me.FlowData.TaskStatusYWCList
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '检查
                strSQL = ""
                strSQL = strSQL + " select * from 公文_B_交接 " + vbCr
                strSQL = strSQL + " where 文件标识 =  '" + strWJBS + "' " + vbCr                  '当前文件
                strSQL = strSQL + " and   发送人   =  '" + strUserXM + "' " + vbCr                '发送的
                strSQL = strSQL + " and   rtrim(交接标识) like '_1%' " + vbCr                            '发送人能看
                strSQL = strSQL + " and   办理状态 not in (" + strTaskStatusYWCList + ")" + vbCr  '接收人未办完
                strSQL = strSQL + " and  (发送纸质文件 > 0 or 发送纸质附件 > 0) " + vbCr          '有纸质文件
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
        ' 文件是否有纸质文件在流转？
        '     strErrMsg            ：如果错误，则返回错误信息
        '     blnHas               ：返回是否?
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection

                'Dim strBLLX As String = Me.FlowTypeName
                Dim strBLLX As String = FlowBLLXName

                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '检查
                strSQL = ""
                strSQL = strSQL + " select * from 公文_B_交接 " + vbCr
                strSQL = strSQL + " where 文件标识 = '" + strWJBS + "' " + vbCr
                strSQL = strSQL + " and   办理类型 = '" + strBLLX + "' " + vbCr
                strSQL = strSQL + " and   rtrim(交接标识) like '1____0%' " + vbCr
                strSQL = strSQL + " and  (发送纸质文件 > 0 or 发送纸质附件 > 0) " + vbCr
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
        ' strUserXM是否已经阅读过正文内容？
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：用户名称
        '     blnRead              ：返回是否?
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strTaskStatusYWCList As String = Me.FlowData.TaskStatusYWCList
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '检查
                strSQL = ""
                strSQL = strSQL + " select sum(是否读过) as 是否读过 " + vbCr
                strSQL = strSQL + " from 公文_B_交接 " + vbCr
                strSQL = strSQL + " where 文件标识 = '" + strWJBS + "' " + vbCr
                strSQL = strSQL + " and   接收人   = '" + strUserXM + "' " + vbCr
                strSQL = strSQL + " and   办理状态 not in (" + strTaskStatusYWCList + ") " + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    Exit Try
                End If

                '返回信息
                Dim intNum As Integer
                intNum = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item("是否读过"), 0)
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
        ' strUserXM是否有未办的通知类事宜？
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：用户名称
        '     blnHas               ：返回是否?
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strTaskStatusYWCList As String = Me.FlowData.TaskStatusYWCList
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '检查
                strSQL = ""
                strSQL = strSQL + " select * from 公文_B_交接 " + vbCr
                strSQL = strSQL + " where 文件标识 = '" + strWJBS + "' " + vbCr                   '当前文件
                strSQL = strSQL + " and   接收人   = '" + strUserXM + "' " + vbCr                 '接收人
                strSQL = strSQL + " and   办理状态 not in (" + strTaskStatusYWCList + ") " + vbCr '接收人未完成
                strSQL = strSQL + " and   rtrim(交接标识) like '__1__1%' " + vbCr                        '接收人能看+通知类
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    Exit Try
                End If

                '返回信息
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
        ' 是否被退回的事宜？
        '     strTaskStatus        ：事宜状态
        ' 返回
        '     True                 ：是
        '     False                ：否
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
        ' 是否被收回的事宜？
        '     strTaskStatus        ：事宜状态
        ' 返回
        '     True                 ：是
        '     False                ：否
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
        ' 是否为通知类事宜？
        '     strTaskStatus        ：事宜状态
        ' 返回
        '     True                 ：是
        '     False                ：否
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
        ' 是否为回复类事宜？
        '     strTaskStatus        ：事宜状态
        ' 返回
        '     True                 ：是
        '     False                ：否
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
        ' 是否为缓办事宜？
        '     strTaskBLZL          ：办理子类
        ' 返回
        '     True                 ：是
        '     False                ：否
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
        ' 事宜是否办完？
        '     strTaskBLZT          ：办理状态
        ' 返回
        '     True                 ：是
        '     False                ：否
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
        ' 封锁文件或解除文件封锁
        ' strUserId  = "" and blnLocked = false：解除整个文件的封锁
        ' strUserId <> "" and blnLocked = false：解除strUserId对文件的封锁
        ' blnLocked  = true 时strUserId <> ""
        '
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：人员代码
        '     blnLocked            ：true-封锁,false-解锁
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim()
                If blnLocked = True And strUserId = "" Then
                    strErrMsg = "错误：未指定对文件封锁的人员！"
                    GoTo errProc
                End If

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '开始事务
                objSqlTransaction = objSqlConnection.BeginTransaction
                objSqlCommand.Transaction = objSqlTransaction

                '事务处理
                Try
                    '文件解锁
                    strSQL = ""
                    strSQL = strSQL + " delete from 管理_B_文件封锁 " + vbCr
                    strSQL = strSQL + " where 文件标识 = '" + strWJBS + "'" + vbCr

                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.ExecuteNonQuery()

                    '文件封锁
                    If blnLocked = True Then
                        strSQL = ""
                        strSQL = strSQL + " insert into 管理_B_文件封锁 (" + vbCr
                        strSQL = strSQL + "   文件标识,人员代码" + vbCr
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

                '提交事务
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
        ' 封锁文件或解除文件封锁
        ' strUserId  = "" and blnLocked = false：解除整个文件的封锁
        ' strUserId <> "" and blnLocked = false：解除strUserId对文件的封锁
        ' blnLocked  = true 时strUserId <> ""
        '
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objSqlTransaction    ：现有事务
        '     strUserId            ：人员代码
        '     blnLocked            ：true-封锁,false-解锁
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim()
                If blnLocked = True And strUserId = "" Then
                    strErrMsg = "错误：未指定对文件封锁的人员！"
                    GoTo errProc
                End If

                '获取文件标识
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '获取事务连接
                If objSqlTransaction Is Nothing Then
                    objSqlConnection = Me.SqlConnection
                Else
                    objSqlConnection = objSqlTransaction.Connection
                End If

                '创建查询连接
                objLocalSqlConnection = New System.Data.SqlClient.SqlConnection(objSqlConnection.ConnectionString)
                objLocalSqlConnection.Open()

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '开始事务
                If objSqlTransaction Is Nothing Then
                    blnNewTrans = True
                    objSqlTransaction = objSqlConnection.BeginTransaction
                Else
                    blnNewTrans = False
                End If
                objSqlCommand.Transaction = objSqlTransaction

                '事务处理
                Try
                    '文件解锁
                    strSQL = ""
                    strSQL = strSQL + " delete from 管理_B_文件封锁 " + vbCr
                    strSQL = strSQL + " where 文件标识 = '" + strWJBS + "'" + vbCr

                    'If strUserId <> "" Then
                    '    strSQL = strSQL + " and 人员代码 = '" + strUserId + "'" + vbCr
                    'End If
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.ExecuteNonQuery()

                    '文件封锁
                    If blnLocked = True Then
                        strSQL = ""
                        strSQL = strSQL + " insert into 管理_B_文件封锁 (" + vbCr
                        strSQL = strSQL + "   文件标识,人员代码" + vbCr
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

                '提交事务
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
        ' 自动接收文件
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：人员名称
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strTaskStatusWJSList As String = Me.FlowData.TaskStatusWJSList
                Dim strTaskStatusZJB As String = Me.FlowData.TASKSTATUS_ZJB

                'Dim strBLLX As String = Me.FlowTypeName
                Dim strBLLX As String = Me.FlowBLLXName

                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '计算未接收的交接单
                strSQL = ""
                strSQL = strSQL + " select * from 公文_B_交接 " + vbCr
                strSQL = strSQL + " where 文件标识 = '" + strWJBS + "' " + vbCr
                strSQL = strSQL + " and   办理类型 = '" + strBLLX + "' " + vbCr
                strSQL = strSQL + " and   接收人   = '" + strUserXM + "' " + vbCr
                strSQL = strSQL + " and   办理状态 in (" + strTaskStatusWJSList + ") " + vbCr
                strSQL = strSQL + " order by 发送人" + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    Exit Try
                End If

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '开始事务
                objSqlTransaction = objSqlConnection.BeginTransaction
                objSqlCommand.Transaction = objSqlTransaction

                '事务处理
                Dim strTemp(2) As String
                Dim intCount As Integer
                Dim i As Integer
                intCount = objDataSet.Tables(0).Rows.Count
                Try
                    For i = 0 To intCount - 1 Step 1
                        '获取数据
                        With objDataSet.Tables(0)
                            strTemp(0) = objPulicParameters.getObjectValue(.Rows(i).Item("文件标识"), "")
                            strTemp(1) = objPulicParameters.getObjectValue(.Rows(i).Item("交接序号"), "")
                        End With

                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " update 公文_B_交接 set " + vbCr
                        strSQL = strSQL + "   接收日期     = '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "'," + vbCr
                        strSQL = strSQL + "   接收纸质文件 = 发送纸质文件," + vbCr
                        strSQL = strSQL + "   接收电子文件 = 发送电子文件," + vbCr
                        strSQL = strSQL + "   接收纸质附件 = 发送纸质附件," + vbCr
                        strSQL = strSQL + "   接收电子附件 = 发送电子附件," + vbCr
                        strSQL = strSQL + "   办理状态     = '" + strTaskStatusZJB + "' " + vbCr
                        strSQL = strSQL + " where 文件标识 = '" + strTemp(0) + "' " + vbCr
                        strSQL = strSQL + " and   交接序号 =  " + strTemp(1) + " " + vbCr

                        '执行
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.ExecuteNonQuery()
                    Next

                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '提交事务
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
        ' strSender向strReceiver发送补阅交接单，并自动设置已经阅读
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objSqlTransaction    ：现有事务
        '     strSender            ：发送人员名称
        '     strReceiver          ：接收人员名称
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strSender Is Nothing Then strSender = ""
                strSender = strSender.Trim()
                If strReceiver Is Nothing Then strReceiver = ""
                strReceiver = strReceiver.Trim()
                If strReceiver = "" Or strSender = "" Then
                    strErrMsg = "错误：未指定[发送人]或[接收人]！"
                    GoTo errProc
                End If

                '获取文件信息
                Dim strTaskStatusYYD As String = Me.FlowData.TASKSTATUS_YYD
                Dim strBYTZ As String = Me.FlowData.TASK_BYTZ
                Dim strBLLX As String = Me.FlowBLLXName
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '获取事务连接
                If objSqlTransaction Is Nothing Then
                    objSqlConnection = Me.SqlConnection
                Else
                    objSqlConnection = objSqlTransaction.Connection
                End If

                '创建查询连接
                objLocalSqlConnection = New System.Data.SqlClient.SqlConnection(objSqlConnection.ConnectionString)
                objLocalSqlConnection.Open()

                '获取新交接单号
                Dim strJJXH As String
                If objdacCommon.getNewCode(strErrMsg, objLocalSqlConnection, "交接序号", "文件标识", strWJBS, "公文_B_交接", True, strJJXH) = False Then
                    GoTo errProc
                End If
                '计算新的发送序号
                Dim strFSXH As String
                If objdacCommon.getNewCode(strErrMsg, objLocalSqlConnection, "发送序号", "文件标识", strWJBS, "公文_B_交接", True, strFSXH) = False Then
                    GoTo errProc
                End If
                '计算接收序号
                Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate
                Dim strRelaFields As String = "文件标识" + strSep + "发送序号"
                Dim strRelaValue As String = strWJBS + strSep + strFSXH
                Dim strJSXH As String
                If objdacCommon.getNewCode(strErrMsg, objLocalSqlConnection, "接收序号", strRelaFields, strRelaValue, "公文_B_交接", True, strJSXH) = False Then
                    GoTo errProc
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objLocalSqlConnection)

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '开始事务
                If objSqlTransaction Is Nothing Then
                    blnNewTrans = True
                    objSqlTransaction = objSqlConnection.BeginTransaction
                Else
                    blnNewTrans = False
                End If
                objSqlCommand.Transaction = objSqlTransaction

                '事务处理
                Dim intZDBY As Integer = Xydc.Platform.Common.Data.FlowData.YJJH_ZHUDONGBUYUE
                Try
                    '提交新的补阅交接单(主动补阅)
                    strSQL = ""
                    strSQL = strSQL + " insert into 公文_B_交接 (" + vbCr
                    strSQL = strSQL + "   文件标识," + vbCr
                    strSQL = strSQL + "   交接序号," + vbCr
                    strSQL = strSQL + "   原交接号," + vbCr
                    strSQL = strSQL + "   发送序号," + vbCr
                    strSQL = strSQL + "   发送人," + vbCr
                    strSQL = strSQL + "   发送日期," + vbCr
                    strSQL = strSQL + "   接收序号," + vbCr
                    strSQL = strSQL + "   接收人," + vbCr
                    strSQL = strSQL + "   接收日期," + vbCr
                    strSQL = strSQL + "   办理最后期限," + vbCr
                    strSQL = strSQL + "   完成日期," + vbCr
                    strSQL = strSQL + "   办理类型," + vbCr
                    strSQL = strSQL + "   办理子类," + vbCr
                    strSQL = strSQL + "   办理状态," + vbCr
                    '自动补阅时,备注添加系统内部处理
                    ' 
                    'strSQL = strSQL + "   交接标识" + vbCr
                    strSQL = strSQL + "   交接标识," + vbCr
                    strSQL = strSQL + "   交接备注 " + vbCr
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
                    '自动补阅时,备注添加系统内部处理
                    ' 
                    'strSQL = strSQL + " '" + "10100100" + "'" + vbCr
                    strSQL = strSQL + " '" + "10100100" + "'," + vbCr
                    strSQL = strSQL + " '" + "系统自动处理" + "'" + vbCr
                    ' 
                    strSQL = strSQL + " )" + vbCr

                    '执行
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

                '提交事务
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
        ' 保存初始交接数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objSqlTransaction    ：现有事务
        '     strSender            ：发送人员名称
        '     strReceiver          ：接收人员名称
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strSender Is Nothing Then strSender = ""
                strSender = strSender.Trim()
                If strReceiver Is Nothing Then strReceiver = ""
                strReceiver = strReceiver.Trim()
                If strReceiver = "" Or strSender = "" Then
                    strErrMsg = "错误：未指定[发送人]或[接收人]！"
                    GoTo errProc
                End If

                '获取文件信息
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try
                Dim strBLLX As String = Me.FlowBLLXName
                Dim strInitTask As String = Me.getInitTask()
                Dim strTaskStatusZJB As String = Me.FlowData.TASKSTATUS_ZJB

                '获取事务连接
                If objSqlTransaction Is Nothing Then
                    objSqlConnection = Me.SqlConnection
                Else
                    objSqlConnection = objSqlTransaction.Connection
                End If

                '创建查询连接
                objLocalSqlConnection = New System.Data.SqlClient.SqlConnection(objSqlConnection.ConnectionString)
                objLocalSqlConnection.Open()

                '获取新交接单号
                Dim strJJXH As String
                If objdacCommon.getNewCode(strErrMsg, objLocalSqlConnection, "交接序号", "文件标识", strWJBS, "公文_B_交接", True, strJJXH) = False Then
                    GoTo errProc
                End If
                '计算新的发送序号
                Dim strFSXH As String
                If objdacCommon.getNewCode(strErrMsg, objLocalSqlConnection, "发送序号", "文件标识", strWJBS, "公文_B_交接", True, strFSXH) = False Then
                    GoTo errProc
                End If
                '计算接收序号
                Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate
                Dim strRelaFields As String = "文件标识" + strSep + "发送序号"
                Dim strRelaValue As String = strWJBS + strSep + strFSXH
                Dim strJSXH As String
                If objdacCommon.getNewCode(strErrMsg, objLocalSqlConnection, "接收序号", strRelaFields, strRelaValue, "公文_B_交接", True, strJSXH) = False Then
                    GoTo errProc
                End If

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '开始事务
                If objSqlTransaction Is Nothing Then
                    objSqlTransaction = objSqlConnection.BeginTransaction
                    blnNewTrans = True
                Else
                    blnNewTrans = False
                End If
                objSqlCommand.Transaction = objSqlTransaction

                '事务处理
                Try
                    strSQL = ""
                    strSQL = strSQL + " insert into 公文_B_交接 (" + vbCr
                    strSQL = strSQL + "   文件标识, 交接序号, 原交接号, 发送序号, 发送人, 发送日期," + vbCr
                    strSQL = strSQL + "   发送纸质文件,发送电子文件,发送纸质附件,发送电子附件," + vbCr
                    strSQL = strSQL + "   接收序号,接收人,接收日期,接收纸质文件,接收电子文件," + vbCr
                    strSQL = strSQL + "   接收纸质附件,接收电子附件,办理类型,办理子类," + vbCr
                    strSQL = strSQL + "   办理状态,交接标识" + vbCr
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
                    strSQL = strSQL + " '01100000'" + vbCr        '初始交接状态
                    strSQL = strSQL + " )" + vbCr

                    '执行
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

                '提交事务
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
        ' 写文件操作日志
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objSqlTransaction    ：现有事务
        '     strUserXM            ：操作人员名称
        '     strCZSM              ：操作情况说明
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()
                If strCZSM Is Nothing Then strCZSM = ""
                strCZSM = strCZSM.Trim()
                If strUserXM = "" Then
                    strErrMsg = "错误：未指定[操作人员]！"
                    GoTo errProc
                End If

                '获取文件数据
                Dim strTaskStatusYYD As String = Me.FlowData.TASKSTATUS_YYD
                Dim strBYTZ As String = Me.FlowData.TASK_BYTZ
                Dim strBLLX As String = Me.FlowTypeName
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '获取事务连接
                If objSqlTransaction Is Nothing Then
                    objSqlConnection = Me.SqlConnection
                Else
                    objSqlConnection = objSqlTransaction.Connection
                End If

                '打开查询连接
                objLocalSqlConnection = New System.Data.SqlClient.SqlConnection(objSqlConnection.ConnectionString)
                objLocalSqlConnection.Open()

                '获取操作序号
                Dim strCZXH As String
                If objdacCommon.getNewCode(strErrMsg, objLocalSqlConnection, "操作序号", "文件标识", strWJBS, "公文_B_操作日志", True, strCZXH) = False Then
                    GoTo errProc
                End If

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '开始事务
                If objSqlTransaction Is Nothing Then
                    blnNewTrans = True
                    objSqlTransaction = objSqlConnection.BeginTransaction
                Else
                    blnNewTrans = False
                End If
                objSqlCommand.Transaction = objSqlTransaction

                '事务处理
                Try
                    '记录
                    strSQL = ""
                    strSQL = strSQL + " insert into 公文_B_操作日志 (" + vbCr
                    strSQL = strSQL + "   文件标识,操作序号,操作人,操作时间,操作说明" + vbCr
                    strSQL = strSQL + " ) values (" + vbCr
                    strSQL = strSQL + "'" + strWJBS + "'," + vbCr
                    strSQL = strSQL + " " + strCZXH + " ," + vbCr
                    strSQL = strSQL + "'" + strUserXM + "'," + vbCr
                    strSQL = strSQL + "'" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "'," + vbCr
                    strSQL = strSQL + "'" + strCZSM + "' " + vbCr
                    strSQL = strSQL + ")" + vbCr

                    '执行
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

                '提交事务
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
        ' 检查“公文_B_交接”的数据的合法性
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objOldData           ：旧数据
        '     objNewData           ：新数据
        '     objenumEditType      ：编辑类型
        ' 返回
        '     True                 ：合法
        '     False                ：不合法或其他程序错误
        ' 修改记录
        '      增加
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "错误：未传入新的数据！"
                    GoTo errProc
                End If
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew, _
                        Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eCpyNew
                    Case Else
                        If objOldData Is Nothing Then
                            strErrMsg = "错误：未传入旧的数据！"
                            GoTo errProc
                        End If
                End Select

                '获取信息
                objSqlConnection = Me.SqlConnection
                strWJBS = Me.WJBS

                '获取表结构定义
                strSQL = "select top 0 * from 公文_B_交接"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, objSqlConnection, strSQL, "公文_B_交接", objDataSet) = False Then
                    GoTo errProc
                End If

                '检查数据长度
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
                            '自动列
                            If strValue = "" Then
                                Dim intJJXH As Integer = 0
                                If Me.getMaxJJXH(strErrMsg, intJJXH) = False Then
                                    GoTo errProc
                                End If
                                strValue = (intJJXH + 1).ToString
                            End If
                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_WJBS
                            '自动列
                            If strValue = "" Then
                                strValue = strWJBS
                            End If

                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_YJJH, _
                            Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSXH
                            If strValue = "" Then
                                strErrMsg = "错误：没有输入[" + strField + "]！"
                                GoTo errProc
                            End If
                            If objPulicParameters.isFloatString(strValue) = False Then
                                strErrMsg = "错误：[" + strValue + "]是无效的数值！"
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
                                    strErrMsg = "错误：[" + strValue + "]是无效的数字！"
                                    GoTo errProc
                                End If
                            End If

                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSRQ
                            If strValue = "" Then
                                strErrMsg = "错误：没有输入[" + strField + "]！"
                                GoTo errProc
                            End If
                            If objPulicParameters.isDatetimeString(strValue) = False Then
                                strErrMsg = "错误：[" + strValue + "]是无效的日期！"
                                GoTo errProc
                            End If
                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSRQ, _
                            Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_BLZHQX, _
                            Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_WCRQ
                            If strValue <> "" Then
                                If objPulicParameters.isDatetimeString(strValue) = False Then
                                    strErrMsg = "错误：[" + strValue + "]是无效的日期！"
                                    GoTo errProc
                                End If
                            End If

                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSR
                            If strValue = "" Then
                                strErrMsg = "错误：没有输入[" + strField + "]！"
                                GoTo errProc
                            End If
                            With objDataSet.Tables(0).Columns(strField)
                                intLen = objPulicParameters.getStringLength(strValue)
                                If intLen > .MaxLength Then
                                    strErrMsg = "错误：[" + strField + "]长度不能超过[" + .MaxLength.ToString() + "]，实际有[" + intLen.ToString() + "]！"
                                    GoTo errProc
                                End If
                            End With
                        Case Else
                            If strValue <> "" Then
                                With objDataSet.Tables(0).Columns(strField)
                                    intLen = objPulicParameters.getStringLength(strValue)
                                    If intLen > .MaxLength Then
                                        strErrMsg = "错误：[" + strField + "]长度不能超过[" + .MaxLength.ToString() + "]，实际有[" + intLen.ToString() + "]！"
                                        GoTo errProc
                                    End If
                                End With
                            End If
                    End Select
                    objNewData(strField) = strValue
                Next
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)

                '检查约束
                Dim intNewJJXH As Integer = objPulicParameters.getObjectValue(objNewData.Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JJXH), 0)
                objListDictionary = New System.Collections.Specialized.ListDictionary
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                        strSQL = "select * from 公文_B_交接 where 文件标识 = @wjbs and 交接序号 = @jjxh"
                        objListDictionary.Add("@wjbs", strWJBS)
                        objListDictionary.Add("@jjxh", intNewJJXH)
                    Case Else
                        Dim intOldJJXH As Integer = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JJXH), 0)
                        strSQL = "select * from 公文_B_交接 where 文件标识 = @wjbs and 交接序号 = @jjxh and 交接序号 <> @oldjjxh"
                        objListDictionary.Add("@wjbs", strWJBS)
                        objListDictionary.Add("@jjxh", intNewJJXH)
                        objListDictionary.Add("@oldjjxh", intOldJJXH)
                End Select
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objListDictionary, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strErrMsg = "错误：[" + intNewJJXH.ToString + "]已经存在！"
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
        ' 设置strUserXM已经阅读过指定文件
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：操作人员名称
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()
                If strUserXM = "" Then
                    strErrMsg = "错误：未指定[操作人员]！"
                    GoTo errProc
                End If

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strTaskStatusYWCList As String = Me.FlowData.TaskStatusYWCList
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '开始事务
                objSqlTransaction = objSqlConnection.BeginTransaction
                objSqlCommand.Transaction = objSqlTransaction

                '事务处理
                Try
                    '记录
                    strSQL = ""
                    strSQL = strSQL + " update 公文_B_交接 set " + vbCr
                    strSQL = strSQL + "   是否读过 = 1 " + vbCr
                    strSQL = strSQL + " where 文件标识 = '" + strWJBS + "' " + vbCr
                    strSQL = strSQL + " and   接收人   = '" + strUserXM + "' " + vbCr
                    strSQL = strSQL + " and   办理状态 not in (" + strTaskStatusYWCList + ") " + vbCr
                    strSQL = strSQL + " and   是否读过 <> 1" + vbCr

                    '执行
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '提交事务
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
        ' 判断附件记录数据是否有效？
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objNewData           ：记录新值(返回推荐值)
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.IsInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "错误：未传入新的数据！"
                    GoTo errProc
                End If

                '获取现有信息
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection

                '获取表结构定义
                strSQL = "select top 0 * from 公文_B_附件"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, objSqlConnection, strSQL, "公文_B_附件", objDataSet) = False Then
                    GoTo errProc
                End If

                '检查数据长度
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
                            '显示字段，不用处理

                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_WJXH
                            If strValue = "" Then
                                strErrMsg = "错误：[" + strField + "]不能为空！"
                                GoTo errProc
                            End If
                            If objPulicParameters.isIntegerString(strValue) = False Then
                                strErrMsg = "错误：[" + strField + "]必须是数字！"
                                GoTo errProc
                            End If
                            intLen = CType(strValue, Integer)
                            If intLen < 1 Or intLen > 999999 Then
                                strErrMsg = "错误：[" + strField + "]必须是[1,999999]！"
                                GoTo errProc
                            End If
                            strValue = intLen.ToString()

                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_WJSM
                            If strValue = "" Then
                                strErrMsg = "错误：[" + strField + "]不能为空！"
                                GoTo errProc
                            End If
                            With objDataSet.Tables(0).Columns(strField)
                                intLen = objPulicParameters.getStringLength(strValue)
                                If intLen > .MaxLength Then
                                    strErrMsg = "错误：[" + strField + "]长度不能超过[" + .MaxLength.ToString() + "]，实际有[" + intLen.ToString() + "]！"
                                    GoTo errProc
                                End If
                            End With

                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_WJYS
                            If strValue = "" Then strValue = "1"
                            If objPulicParameters.isIntegerString(strValue) = False Then
                                strErrMsg = "错误：[" + strField + "]必须是数字！"
                                GoTo errProc
                            End If
                            intLen = CType(strValue, Integer)
                            If intLen < 1 Or intLen > 999999 Then
                                strErrMsg = "错误：[" + strField + "]必须是[1,999999]！"
                                GoTo errProc
                            End If
                            strValue = intLen.ToString()

                        Case Else
                            If strValue <> "" Then
                                With objDataSet.Tables(0).Columns(strField)
                                    intLen = objPulicParameters.getStringLength(strValue)
                                    If intLen > .MaxLength Then
                                        strErrMsg = "错误：[" + strField + "]长度不能超过[" + .MaxLength.ToString() + "]，实际有[" + intLen.ToString() + "]！"
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
        ' 判断相关文件的附件记录数据是否有效？
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objNewData           ：记录新值(返回推荐值)
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.IsInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "错误：未传入新的数据！"
                    GoTo errProc
                End If

                '获取现有信息
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection

                '获取表结构定义
                strSQL = "select top 0 * from 公文_B_相关文件附件"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, objSqlConnection, strSQL, "公文_B_相关文件附件", objDataSet) = False Then
                    GoTo errProc
                End If

                '检查数据长度
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
                            '显示字段，不用处理

                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_XIANGGUANWENJIANFUJIAN_WJXH
                            If strValue = "" Then
                                strErrMsg = "错误：[" + strField + "]不能为空！"
                                GoTo errProc
                            End If
                            If objPulicParameters.isIntegerString(strValue) = False Then
                                strErrMsg = "错误：[" + strField + "]必须是数字！"
                                GoTo errProc
                            End If
                            intLen = CType(strValue, Integer)
                            If intLen < 1 Or intLen > 999999 Then
                                strErrMsg = "错误：[" + strField + "]必须是[1,999999]！"
                                GoTo errProc
                            End If
                            strValue = intLen.ToString()

                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_XIANGGUANWENJIANFUJIAN_WJSM
                            If strValue = "" Then
                                strErrMsg = "错误：[" + strField + "]不能为空！"
                                GoTo errProc
                            End If
                            With objDataSet.Tables(0).Columns(strField)
                                intLen = objPulicParameters.getStringLength(strValue)
                                If intLen > .MaxLength Then
                                    strErrMsg = "错误：[" + strField + "]长度不能超过[" + .MaxLength.ToString() + "]，实际有[" + intLen.ToString() + "]！"
                                    GoTo errProc
                                End If
                            End With

                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_XIANGGUANWENJIANFUJIAN_WJYS
                            If strValue = "" Then strValue = "1"
                            If objPulicParameters.isIntegerString(strValue) = False Then
                                strErrMsg = "错误：[" + strField + "]必须是数字！"
                                GoTo errProc
                            End If
                            intLen = CType(strValue, Integer)
                            If intLen < 1 Or intLen > 999999 Then
                                strErrMsg = "错误：[" + strField + "]必须是[1,999999]！"
                                GoTo errProc
                            End If
                            strValue = intLen.ToString()

                        Case Else
                            If strValue <> "" Then
                                With objDataSet.Tables(0).Columns(strField)
                                    intLen = objPulicParameters.getStringLength(strValue)
                                    If intLen > .MaxLength Then
                                        strErrMsg = "错误：[" + strField + "]长度不能超过[" + .MaxLength.ToString() + "]，实际有[" + intLen.ToString() + "]！"
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
        ' 根据本地文件获取FTP服务器文件的命名
        ' 稿件命名方案：文件标识+文件扩展名
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strLocalFile         ：本地文件名
        '     intWJND              ：文件年度
        '     strWJBS              ：文件标识
        '     strBasePath          ：附件目录基本目录
        '     strRemoteFile        ：返回FTP服务器文件路径
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
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

                '获取文件名
                Dim strFileName As String = ""
                Dim strFileExt As String = ""
                strFileExt = objBaseLocalFile.getExtension(strLocalFile)

                '稿件命名方案：文件标识+文件扩展名
                strFileName = strWJBS + strFileExt
                strFileName = objBaseLocalFile.doMakePath(intWJND.ToString(), strFileName)

                '复合目录+文件
                strFileName = objBaseLocalFile.doMakePath(strBasePath, strFileName)

                '返回
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
        ' 根据本地文件获取FTP服务器文件的命名
        ' 文件附件命名方案：文件标识-FJ-序号
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strLocalFile         ：本地文件名
        '     intWJND              ：文件年度
        '     strWJBS              ：文件标识
        '     intXH                ：序号
        '     strBasePath          ：附件目录基本目录
        '     strRemoteFile        ：返回FTP服务器文件路径
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
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

                '获取文件名
                Dim strFileName As String = ""
                Dim strFileExt As String = ""
                strFileExt = objBaseLocalFile.getExtension(strLocalFile)

                '文件附件命名方案：文件标识-FJ-序号
                strFileName = strWJBS + "-FJ-" + intXH.ToString() + strFileExt
                strFileName = objBaseLocalFile.doMakePath(intWJND.ToString(), strFileName)

                '复合目录+文件
                strFileName = objBaseLocalFile.doMakePath(strBasePath, strFileName)

                '返回
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
        ' 根据本地文件获取FTP服务器文件的命名
        ' 相关文件附件命名方案：文件标识-XGFJ-序号
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strLocalFile         ：本地文件名
        '     intWJND              ：文件年度
        '     strWJBS              ：文件标识
        '     intXH                ：序号
        '     strBasePath          ：附件目录基本目录
        '     strRemoteFile        ：返回FTP服务器文件路径
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
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

                '获取文件名
                Dim strFileName As String = ""
                Dim strFileExt As String = ""
                strFileExt = objBaseLocalFile.getExtension(strLocalFile)

                '文件附件命名方案：文件标识-XGFJ-序号
                strFileName = strWJBS + "-XGFJ-" + intXH.ToString() + strFileExt
                strFileName = objBaseLocalFile.doMakePath(intWJND.ToString(), strFileName)

                '复合目录+文件
                strFileName = objBaseLocalFile.doMakePath(strBasePath, strFileName)

                '返回
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
        ' 备份稿件文件
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strGJFTPSpec           ：稿件文件的现FTP路径
        '     objFTPProperty         ：FTP连接参数
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
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
                '检查
                If strGJFTPSpec Is Nothing Then strGJFTPSpec = ""
                strGJFTPSpec = strGJFTPSpec.Trim
                If strGJFTPSpec = "" Then
                    Exit Try
                End If
                If objFTPProperty Is Nothing Then
                    strErrMsg = "错误：未指定FTP服务器连接参数！"
                    GoTo errProc
                End If

                '备份
                Dim strOldFile As String = strGJFTPSpec
                Dim blnExisted As Boolean
                Dim strFileName As String
                Dim strUrl As String
                If strOldFile <> "" Then
                    With objFTPProperty
                        strUrl = .getUrl(strOldFile)
                        If objBaseFTP.isFileExisted(strErrMsg, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword, blnExisted) = False Then
                            '可以不成功：可能是文件不存在
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
        ' 备份附件文件
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objFTPProperty       ：FTP服务器属性
        '     objFJData            ：附件数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                    strErrMsg = "错误：未传入FTP服务器参数！"
                    GoTo errProc
                End If
                If objFJData Is Nothing Then
                    Exit Try
                End If
                If objFJData.Tables(strTable) Is Nothing Then
                    Exit Try
                End If

                '备份原文件
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
                                    '可以不成功：可能是文件不存在
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
        ' 备份相关文件附件文件
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objFTPProperty       ：FTP服务器属性
        '     objXGWJFJData        ：相关文件附件数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                    strErrMsg = "错误：未传入FTP服务器参数！"
                    GoTo errProc
                End If
                If objXGWJFJData Is Nothing Then
                    Exit Try
                End If
                If objXGWJFJData.Tables(strTable) Is Nothing Then
                    Exit Try
                End If

                '备份原文件
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
                                    '可以不成功：可能是文件不存在
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
        ' 从备份中恢复稿件文件
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strGJFTPSpec           ：稿件文件的原FTP路径
        '     objFTPProperty         ：FTP连接参数
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
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
                '检查
                If strGJFTPSpec Is Nothing Then strGJFTPSpec = ""
                strGJFTPSpec = strGJFTPSpec.Trim
                If strGJFTPSpec = "" Then
                    Exit Try
                End If
                If objFTPProperty Is Nothing Then
                    strErrMsg = "错误：未指定FTP服务器连接参数！"
                    GoTo errProc
                End If

                '备份
                Dim strOldFile As String = strGJFTPSpec
                Dim blnExisted As Boolean
                Dim strToUrl As String
                Dim strUrl As String
                If strOldFile <> "" Then
                    With objFTPProperty
                        strUrl = .getUrl(strOldFile + strBakExt)
                        If objBaseFTP.isFileExisted(strErrMsg, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword, blnExisted) = False Then
                            '可以不成功：可能是文件不存在
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
        ' 从备份或新命名文件中恢复原附件文件
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strWJBS              ：文件标识
        '     intWJND              ：新文件存放的年度
        '     objFTPProperty       ：FTP服务器属性
        '     objNewData           ：新附件数据
        '     objOldData           ：原附件数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                    strErrMsg = "错误：未传入FTP服务器参数！"
                    GoTo errProc
                End If
                If objOldData Is Nothing Then
                    Exit Try
                End If
                If objOldData.Tables(strTable) Is Nothing Then
                    Exit Try
                End If

                '优先从备份文件回滚
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
                                '先从备份中恢复
                                strUrl = .getUrl(strOldFile + strBakExt)
                                If objBaseFTP.isFileExisted(strErrMsg, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword, blnExisted) = False Then
                                    blnExisted = False
                                End If
                                If blnExisted = True Then
                                    '备份文件存在，则从备份文件中尽可能恢复
                                    strToUrl = .getUrl(strOldFile)
                                    objBaseFTP.doRenameFile(strErrMsg, strUrl, strToUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword)
                                Else
                                    '备份文件不存在，则检查备份文件是否已改名为对应的新文件？
                                    If Not (objNewData Is Nothing) Then
                                        blnDo = False
                                        With objNewData.Tables(strTable)
                                            intCountA = .DefaultView.Count
                                            For j = 0 To intCountA - 1 Step 1
                                                strNewWJWZ = objPulicParameters.getObjectValue(.DefaultView.Item(j).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_WJWZ), "")
                                                If strOldWJWZ = strNewWJWZ.ToUpper Then
                                                    '获取对应的新文件
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
                                                '已经新文件存在，则执行从新文件中尽可能恢复
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
        ' 从备份或新命名文件中恢复原相关文件附件文件
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strWJBS              ：文件标识
        '     intWJND              ：新文件存放的年度
        '     objFTPProperty       ：FTP服务器属性
        '     objNewData           ：新相关文件附件数据
        '     objOldData           ：原相关文件附件数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                    strErrMsg = "错误：未传入FTP服务器参数！"
                    GoTo errProc
                End If
                If objOldData Is Nothing Then
                    Exit Try
                End If
                If objOldData.Tables(strTable) Is Nothing Then
                    Exit Try
                End If

                '优先从备份文件回滚
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
                                '先从备份中恢复
                                strUrl = .getUrl(strOldFile + strBakExt)
                                If objBaseFTP.isFileExisted(strErrMsg, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword, blnExisted) = False Then
                                    blnExisted = False
                                End If
                                If blnExisted = True Then
                                    '备份文件存在，则从备份文件中尽可能恢复
                                    strToUrl = .getUrl(strOldFile)
                                    objBaseFTP.doRenameFile(strErrMsg, strUrl, strToUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword)
                                Else
                                    '备份文件不存在，则检查备份文件是否已改名为对应的新文件？
                                    If Not (objNewData Is Nothing) Then
                                        blnDo = False
                                        With objNewData.Tables(strTable)
                                            intCountA = .DefaultView.Count
                                            For j = 0 To intCountA - 1 Step 1
                                                strNewWJWZ = objPulicParameters.getObjectValue(.DefaultView.Item(j).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_XIANGGUANWENJIANFUJIAN_WJWZ), "")
                                                If strOldWJWZ = strNewWJWZ.ToUpper Then
                                                    '获取对应的新文件
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
                                                '已经新文件存在，则执行从新文件中尽可能恢复
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
        ' 删除稿件备份文件
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strGJFTPSpec           ：稿件文件的原FTP路径
        '     objFTPProperty         ：FTP连接参数
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
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
                '检查
                If strGJFTPSpec Is Nothing Then strGJFTPSpec = ""
                strGJFTPSpec = strGJFTPSpec.Trim
                If strGJFTPSpec = "" Then
                    Exit Try
                End If
                If objFTPProperty Is Nothing Then
                    strErrMsg = "错误：未指定FTP服务器连接参数！"
                    GoTo errProc
                End If

                '删除备份
                Dim strOldFile As String = strGJFTPSpec
                Dim strUrl As String
                If strOldFile <> "" Then
                    With objFTPProperty
                        strUrl = .getUrl(strOldFile + strBakExt)
                        If objBaseFTP.doDeleteFile(strErrMsg, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword) = False Then
                            '可以不成功,形成垃圾数据
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
        ' 删除附件的备份文件
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objFTPProperty       ：FTP服务器属性
        '     objFJData            ：附件数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                    strErrMsg = "错误：未传入FTP服务器参数！"
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
                                    '可以不成功,形成垃圾数据
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
        ' 删除相关文件附件的备份文件
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objFTPProperty       ：FTP服务器属性
        '     objXGWJFJData        ：相关文件附件数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                    strErrMsg = "错误：未传入FTP服务器参数！"
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
                                    '可以不成功,形成垃圾数据
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
        ' 保存附件数据
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strWJBS                ：文件标识
        '     intWJND                ：新文件存放的年度
        '     objSqlTransaction      ：现有事务
        '     objFTPProperty         ：FTP服务器属性
        '     objNewData             ：记录新值(返回保存后的新值)
        '     objOldData             ：记录旧值
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
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

            '初始化
            doSaveFujian = False
            strErrMsg = ""

            Try
                '检查
                If Me.IsInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "错误：未传入新的数据！"
                    GoTo errProc
                End If
                If objFTPProperty Is Nothing Then
                    strErrMsg = "错误：未传入FTP服务器参数！"
                    GoTo errProc
                End If
                If strWJBS Is Nothing Then strWJBS = ""
                strWJBS = strWJBS.Trim
                If strWJBS = "" Then
                    Exit Try
                End If

                '获取现有信息
                If objSqlTransaction Is Nothing Then
                    objSqlConnection = Me.SqlConnection
                Else
                    objSqlConnection = objSqlTransaction.Connection
                End If

                '开始事务
                If objSqlTransaction Is Nothing Then
                    blnNewTrans = True
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Else
                    blnNewTrans = False
                End If

                '保存数据
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '删除“公文_B_附件”数据
                    strSQL = ""
                    strSQL = strSQL + " delete from 公文_B_附件 " + vbCr
                    strSQL = strSQL + " where 文件标识 = @wjbs" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    Try
                        '在源文件的同目录中将文件备份
                        If Me.doBackupFiles_FJ(strErrMsg, objFTPProperty, objOldData) = False Then
                            GoTo rollDatabaseAndFile
                        End If

                        '保存新数据
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
                                '获取原FTP路径和新本地文件路径
                                strOldFile = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_WJWZ), "")
                                strLocFile = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_BDWJ), "")
                                strNewFile = ""
                                '上传文件
                                If strLocFile <> "" Then
                                    '文件存在?
                                    If objBaseLocalFile.doFileExisted(strErrMsg, strLocFile, blnExisted) = False Then
                                        GoTo rollDatabaseAndFile
                                    End If
                                    If blnExisted = True Then
                                        '获取FTP文件路径
                                        If Me.getFTPFileName_FJ(strErrMsg, strLocFile, intWJND, strWJBS, i + 1, strBasePath, strNewFile) = False Then
                                            GoTo rollDatabaseAndFile
                                        End If
                                        '有本地文件，则需要上载
                                        With objFTPProperty
                                            strUrl = .getUrl(strNewFile)
                                            If objBaseFTP.doPutFile(strErrMsg, strLocFile, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword) = False Then
                                                GoTo rollDatabaseAndFile
                                            End If
                                        End With
                                    Else
                                        strErrMsg = "错误：[" + strLocFile + "]不存在！"
                                        GoTo rollDatabaseAndFile
                                    End If
                                Else
                                    If strOldFile <> "" Then
                                        '
                                        '未从FTP服务器下载
                                        '
                                        '从备份文件恢复到当前行的文件
                                        With objFTPProperty
                                            strUrl = .getUrl(strOldFile + strBakExt)
                                            If objBaseFTP.isFileExisted(strErrMsg, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword, blnExisted) = False Then
                                                '可以不成功
                                            Else
                                                If blnExisted = True Then
                                                    '获取FTP文件路径
                                                    If Me.getFTPFileName_FJ(strErrMsg, strOldFile, intWJND, strWJBS, i + 1, strBasePath, strNewFile) = False Then
                                                        GoTo rollDatabaseAndFile
                                                    End If
                                                    strToUrl = .getUrl(strNewFile)
                                                    '更改文件名
                                                    If objBaseFTP.doRenameFile(strErrMsg, strUrl, strToUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword) = False Then
                                                        GoTo rollDatabaseAndFile
                                                    End If
                                                End If
                                            End If
                                        End With
                                    Else
                                        '没有电子文件
                                    End If
                                End If

                                '写数据
                                strSQL = ""
                                strSQL = strSQL + " insert into 公文_B_附件 (" + vbCr
                                strSQL = strSQL + "   文件标识, 序号, 说明, 页数, 位置" + vbCr
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

                        '删除所有备份文件
                        If blnNewTrans = True Then
                            If Me.doDeleteBackupFiles_FJ(strErrMsg, objFTPProperty, objOldData) = False Then
                                '可以不成功，形成垃圾文件！
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

                '提交事务
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

            '返回
            doSaveFujian = True
            Exit Function

rollDatabaseAndFile:
            If blnNewTrans = True Then
                objSqlTransaction.Rollback()
                If Me.doRestoreFiles_FJ(strSQL, strWJBS, intWJND, objFTPProperty, objNewData, objOldData) = False Then
                    '无法恢复成功，尽力了！
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
        ' 保存附件数据
        '     strErrMsg              ：如果错误，则返回错误信息
        '     blnEnforeEdit          ：是否强制修改
        '     strUserXM              ：操作员名称
        '     objNewData             ：记录新值(返回保存后的新值)
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
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

            '初始化
            doSaveFujian = False
            strErrMsg = ""

            Try
                '检查
                If Me.IsInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "错误：未传入新的数据！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim

                '获取现有信息
                objSqlConnection = Me.SqlConnection
                strWJBS = Me.WJBS
                If strWJBS Is Nothing Then strWJBS = ""
                strWJBS = strWJBS.Trim
                If strWJBS = "" Then
                    Exit Try
                End If

                '获取原附件数据
                If Me.getFujianData(strErrMsg, objOldData) = False Then
                    GoTo errProc
                End If

                '获取FTP连接参数
                If objdacXitongpeizhi.getFtpServerParam(strErrMsg, objSqlConnection, objFTPProperty) = False Then
                    GoTo errProc
                End If

                '开始事务
                objSqlTransaction = objSqlConnection.BeginTransaction()

                '保存数据
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '删除“公文_B_附件”数据
                    strSQL = ""
                    strSQL = strSQL + " delete from 公文_B_附件 " + vbCr
                    strSQL = strSQL + " where 文件标识 = @wjbs" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    Try
                        '在源文件的同目录中将文件备份
                        If Me.doBackupFiles_FJ(strErrMsg, objFTPProperty, objOldData) = False Then
                            GoTo rollDatabaseAndFile
                        End If

                        '保存新数据
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
                                '获取原FTP路径和新本地文件路径
                                strOldFile = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_WJWZ), "")
                                strLocFile = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_BDWJ), "")
                                strNewFile = ""
                                '上传文件
                                If strLocFile <> "" Then
                                    '文件存在?
                                    If objBaseLocalFile.doFileExisted(strErrMsg, strLocFile, blnExisted) = False Then
                                        GoTo rollDatabaseAndFile
                                    End If
                                    If blnExisted = True Then
                                        '获取FTP文件路径
                                        If Me.getFTPFileName_FJ(strErrMsg, strLocFile, intWJND, strWJBS, i + 1, strBasePath, strNewFile) = False Then
                                            GoTo rollDatabaseAndFile
                                        End If
                                        '有本地文件，则需要上载
                                        With objFTPProperty
                                            strUrl = .getUrl(strNewFile)
                                            If objBaseFTP.doPutFile(strErrMsg, strLocFile, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword) = False Then
                                                GoTo rollDatabaseAndFile
                                            End If
                                        End With
                                    Else
                                        strErrMsg = "错误：[" + strLocFile + "]不存在！"
                                        GoTo rollDatabaseAndFile
                                    End If
                                Else
                                    If strOldFile <> "" Then
                                        '
                                        '未从FTP服务器下载
                                        '
                                        '从备份文件恢复到当前行的文件
                                        With objFTPProperty
                                            strUrl = .getUrl(strOldFile + strBakExt)
                                            If objBaseFTP.isFileExisted(strErrMsg, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword, blnExisted) = False Then
                                                '可以不成功
                                            Else
                                                If blnExisted = True Then
                                                    '获取FTP文件路径
                                                    If Me.getFTPFileName_FJ(strErrMsg, strOldFile, intWJND, strWJBS, i + 1, strBasePath, strNewFile) = False Then
                                                        GoTo rollDatabaseAndFile
                                                    End If
                                                    strToUrl = .getUrl(strNewFile)
                                                    '更改文件名
                                                    If objBaseFTP.doRenameFile(strErrMsg, strUrl, strToUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword) = False Then
                                                        GoTo rollDatabaseAndFile
                                                    End If
                                                End If
                                            End If
                                        End With
                                    Else
                                        '没有电子文件
                                    End If
                                End If

                                '写数据
                                strSQL = ""
                                strSQL = strSQL + " insert into 公文_B_附件 (" + vbCr
                                strSQL = strSQL + "   文件标识, 序号, 说明, 页数, 位置" + vbCr
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

                        '如果是强制编辑
                        Dim strCZSM As String = Xydc.Platform.Common.Workflow.BaseFlowObject.LOGO_QXBJ
                        If blnEnforeEdit = True Then
                            If Me.doWriteFileLogo(strErrMsg, objSqlTransaction, strUserXM, strCZSM) = False Then
                                GoTo rollDatabaseAndFile
                            End If
                        End If

                        '解除文件编辑封锁
                        If Me.doLockFile(strErrMsg, objSqlTransaction, "", False) = False Then
                            GoTo rollDatabaseAndFile
                        End If

                        '删除所有备份文件
                        If Me.doDeleteBackupFiles_FJ(strErrMsg, objFTPProperty, objOldData) = False Then
                            '可以不成功，形成垃圾文件！
                        End If

                    Catch ex As Exception
                        strErrMsg = ex.Message
                        GoTo rollDatabaseAndFile
                    End Try

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo rollDatabase
                End Try

                '提交事务
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

            '返回
            doSaveFujian = True
            Exit Function

rollDatabaseAndFile:
            objSqlTransaction.Rollback()
            If Me.doRestoreFiles_FJ(strSQL, strWJBS, intWJND, objFTPProperty, objNewData, objOldData) = False Then
                '无法恢复成功，尽力了！
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
        ' Update状态下保存单个附件数据(序号不能修改！)
        '     strErrMsg              ：如果错误，则返回错误信息
        '     blnEnforeEdit          ：是否强制修改
        '     strUserXM              ：操作员名称
        '     objNewData             ：记录新值
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
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

            '初始化
            doSaveFujian = False
            strErrMsg = ""

            Try
                '检查
                If Me.IsInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "错误：未传入新的数据！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim

                '获取现有信息
                objSqlConnection = Me.SqlConnection
                strWJBS = Me.WJBS
                If strWJBS Is Nothing Then strWJBS = ""
                strWJBS = strWJBS.Trim
                If strWJBS = "" Then
                    Exit Try
                End If

                '获取FTP连接参数
                If objdacXitongpeizhi.getFtpServerParam(strErrMsg, objSqlConnection, objFTPProperty) = False Then
                    GoTo errProc
                End If

                '获取文件序号及新本地文件路径
                intWJXH = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_WJXH), 0)
                strBDWJ = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_BDWJ), "")

                '获取原附件数据
                If Me.getFujianData(strErrMsg, intWJXH, objOldData) = False Then
                    GoTo errProc
                End If
                If objOldData.Tables(strTable).DefaultView.Count < 1 Then
                    '记录不存在，不在不例程处理范围！
                    Exit Try
                End If

                '开始事务
                objSqlTransaction = objSqlConnection.BeginTransaction()

                '保存数据
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    Try
                        Dim strNewFile As String = ""
                        If strBDWJ <> "" Then
                            '获取新本地文件路径
                            Dim strLocFile As String = strBDWJ
                            Dim blnExisted As Boolean

                            '文件存在?
                            If objBaseLocalFile.doFileExisted(strErrMsg, strLocFile, blnExisted) = False Then
                                GoTo rollDatabase
                            End If
                            If blnExisted = False Then
                                strErrMsg = "错误：[" + strLocFile + "]不存在！"
                                GoTo rollDatabase
                            End If

                            '上传新文件
                            Dim strBasePath As String = Me.getBasePath_FJ
                            Dim strUrl As String
                            '获取FTP文件路径
                            If Me.getFTPFileName_FJ(strErrMsg, strLocFile, intWJND, strWJBS, intWJXH, strBasePath, strNewFile) = False Then
                                GoTo rollDatabase
                            End If
                            '在源文件的同目录中将文件备份
                            If Me.doBackupFiles_FJ(strErrMsg, objFTPProperty, objOldData) = False Then
                                GoTo rollDatabaseAndFile
                            End If
                            '上载
                            With objFTPProperty
                                strUrl = .getUrl(strNewFile)
                                If objBaseFTP.doPutFile(strErrMsg, strLocFile, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword) = False Then
                                    GoTo rollDatabaseAndFile
                                End If
                            End With
                        End If

                        '写数据
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@wjsm", objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_WJSM), ""))
                        objSqlCommand.Parameters.AddWithValue("@wjys", objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_WJYS), 0))
                        If strNewFile <> "" Then
                            strSQL = ""
                            strSQL = strSQL + " update 公文_B_附件 set " + vbCr
                            strSQL = strSQL + "   说明 = @wjsm, 页数 = @wjys, 位置 = @wjwz" + vbCr
                            strSQL = strSQL + " from 公文_B_附件" + vbCr
                            strSQL = strSQL + " where 文件标识 = @wjbs" + vbCr
                            strSQL = strSQL + " and   序号     = @wjxh" + vbCr
                            objSqlCommand.Parameters.AddWithValue("@wjwz", strNewFile)
                        Else
                            strSQL = ""
                            strSQL = strSQL + " update 公文_B_附件 set " + vbCr
                            strSQL = strSQL + "   说明 = @wjsm, 页数 = @wjys" + vbCr
                            strSQL = strSQL + " from 公文_B_附件" + vbCr
                            strSQL = strSQL + " where 文件标识 = @wjbs" + vbCr
                            strSQL = strSQL + " and   序号     = @wjxh" + vbCr
                        End If
                        objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                        objSqlCommand.Parameters.AddWithValue("@wjxh", intWJXH)
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.ExecuteNonQuery()

                        '如果是强制编辑
                        Dim strCZSM As String = Xydc.Platform.Common.Workflow.BaseFlowObject.LOGO_QXBJ
                        If blnEnforeEdit = True Then
                            If Me.doWriteFileLogo(strErrMsg, objSqlTransaction, strUserXM, strCZSM) = False Then
                                GoTo rollDatabaseAndFile
                            End If
                        End If

                        '解除文件编辑封锁
                        If Me.doLockFile(strErrMsg, objSqlTransaction, "", False) = False Then
                            GoTo rollDatabaseAndFile
                        End If

                        '删除所有备份文件
                        If Me.doDeleteBackupFiles_FJ(strErrMsg, objFTPProperty, objOldData) = False Then
                            '可以不成功，形成垃圾文件！
                        End If

                    Catch ex As Exception
                        strErrMsg = ex.Message
                        GoTo rollDatabaseAndFile
                    End Try

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo rollDatabase
                End Try

                '提交事务
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

            '返回
            doSaveFujian = True
            Exit Function

rollDatabaseAndFile:
            objSqlTransaction.Rollback()
            If Me.doRestoreFiles_FJ(strSQL, strWJBS, intWJND, objFTPProperty, Nothing, objOldData) = False Then
                '无法恢复成功，尽力了！
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
        ' 从相关链接+相关附件数据集中拆离出相关附件数据集
        '     strErrMsg              ：如果错误，则返回错误信息
        '     objSrcData             ：相关链接+相关附件数据集
        '     objDesData             ：相关附件数据集
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
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

                '检查
                If objSrcData Is Nothing Then
                    Exit Try
                End If
                If objSrcData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_SHENPIWENJIAN_FUJIAN) Is Nothing Then
                    Exit Try
                End If

                '创建空数据集
                objDesData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_XIANGGUANWENJIANFUJIAN)

                '拆离
                Dim strOldFilter As String
                With objSrcData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_SHENPIWENJIAN_FUJIAN)
                    '备份过滤串
                    strOldFilter = .DefaultView.RowFilter

                    '设置过滤附件
                    .DefaultView.RowFilter = Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_LBBS + " = " + intFJBS.ToString()

                    '创建全附件数据集
                    intCount = .DefaultView.Count
                    For i = 0 To intCount - 1 Step 1
                        '空行
                        With objDesData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_XIANGGUANWENJIANFUJIAN)
                            objDataRow = .NewRow
                        End With

                        '设置数据
                        objDataRow.Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_XIANGGUANWENJIANFUJIAN_BDWJ) = .DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_BDWJ)
                        objDataRow.Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_XIANGGUANWENJIANFUJIAN_WJBS) = .DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_WJBS)
                        objDataRow.Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_XIANGGUANWENJIANFUJIAN_WJSM) = .DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_WJBT)
                        objDataRow.Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_XIANGGUANWENJIANFUJIAN_WJWZ) = .DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_FJWZ)
                        objDataRow.Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_XIANGGUANWENJIANFUJIAN_WJXH) = .DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_FJXH)
                        objDataRow.Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_XIANGGUANWENJIANFUJIAN_WJYS) = .DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_FJYS)
                        objDataRow.Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_XIANGGUANWENJIANFUJIAN_XSXH) = .DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_XSXH)
                        objDataRow.Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_XIANGGUANWENJIANFUJIAN_XZBZ) = .DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_XZBZ)

                        '加行
                        With objDesData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_XIANGGUANWENJIANFUJIAN)
                            .Rows.Add(objDataRow)
                        End With
                    Next

                    '复原
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
        ' 保存相关文件数据：相关附件和相关链接
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strWJBS                ：文件标识
        '     intWJND                ：新文件存放的年度
        '     objSqlTransaction      ：现有事务
        '     objFTPProperty         ：FTP服务器属性
        '     objNewData             ：相关文件记录新值(返回保存后的新值)
        '     objOldData             ：相关文件记录旧值
        '     objNewFJData           ：相关文件中的附件新值
        '     objOldFJData           ：相关文件中的附件旧值
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
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

            '初始化
            doSaveXgwj = False
            strErrMsg = ""

            Try
                '检查
                If Me.IsInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "错误：未传入新的数据！"
                    GoTo errProc
                End If
                If objFTPProperty Is Nothing Then
                    strErrMsg = "错误：未传入FTP服务器参数！"
                    GoTo errProc
                End If
                If strWJBS Is Nothing Then strWJBS = ""
                strWJBS = strWJBS.Trim
                If strWJBS = "" Then
                    Exit Try
                End If

                '获取现有信息
                If objSqlTransaction Is Nothing Then
                    objSqlConnection = Me.SqlConnection
                Else
                    objSqlConnection = objSqlTransaction.Connection
                End If

                '开始事务
                If objSqlTransaction Is Nothing Then
                    blnNewTrans = True
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Else
                    blnNewTrans = False
                End If

                '保存数据
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '删除“公文_B_相关文件”数据
                    strSQL = ""
                    strSQL = strSQL + " delete from 公文_B_相关文件 " + vbCr
                    strSQL = strSQL + " where 上级文件标识 = @wjbs" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '删除“公文_B_相关文件附件”数据
                    strSQL = ""
                    strSQL = strSQL + " delete from 公文_B_相关文件附件 " + vbCr
                    strSQL = strSQL + " where 文件标识 = @wjbs" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    Try
                        '在源文件的同目录中将文件备份
                        If Me.doBackupFiles_XGWJFJ(strErrMsg, objFTPProperty, objOldFJData) = False Then
                            GoTo rollDatabaseAndFile
                        End If

                        '保存新数据
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
                                '获取相关文件类型
                                intLBBS = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_LBBS), 0)

                                '分类处理
                                Select Case intLBBS
                                    Case Xydc.Platform.Common.Data.FlowData.enumXGWJLB.FujianFile
                                        '获取原FTP路径和新本地文件路径
                                        strOldFile = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_FJWZ), "")
                                        strLocFile = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_BDWJ), "")
                                        strNewFile = ""

                                        '上传文件
                                        If strLocFile <> "" Then
                                            '文件存在?
                                            If objBaseLocalFile.doFileExisted(strErrMsg, strLocFile, blnExisted) = False Then
                                                GoTo rollDatabaseAndFile
                                            End If
                                            If blnExisted = True Then
                                                '获取FTP文件路径
                                                If Me.getFTPFileName_XGWJFJ(strErrMsg, strLocFile, intWJND, strWJBS, i + 1, strBasePath, strNewFile) = False Then
                                                    GoTo rollDatabaseAndFile
                                                End If
                                                '有本地文件，则需要上载
                                                With objFTPProperty
                                                    strUrl = .getUrl(strNewFile)
                                                    If objBaseFTP.doPutFile(strErrMsg, strLocFile, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword) = False Then
                                                        GoTo rollDatabaseAndFile
                                                    End If
                                                End With
                                            Else
                                                strErrMsg = "错误：[" + strLocFile + "]不存在！"
                                                GoTo rollDatabaseAndFile
                                            End If
                                        Else
                                            If strOldFile <> "" Then
                                                '
                                                '未从FTP服务器下载
                                                '
                                                '从备份文件恢复到当前行的文件
                                                With objFTPProperty
                                                    strUrl = .getUrl(strOldFile + strBakExt)
                                                    If objBaseFTP.isFileExisted(strErrMsg, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword, blnExisted) = False Then
                                                        '可以不成功
                                                    Else
                                                        If blnExisted = True Then
                                                            '获取FTP文件路径
                                                            If Me.getFTPFileName_XGWJFJ(strErrMsg, strOldFile, intWJND, strWJBS, i + 1, strBasePath, strNewFile) = False Then
                                                                GoTo rollDatabaseAndFile
                                                            End If
                                                            strToUrl = .getUrl(strNewFile)
                                                            '更改文件名
                                                            If objBaseFTP.doRenameFile(strErrMsg, strUrl, strToUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword) = False Then
                                                                GoTo rollDatabaseAndFile
                                                            End If
                                                        End If
                                                    End If
                                                End With
                                            Else
                                                '没有电子文件
                                            End If
                                        End If

                                        '写数据
                                        strSQL = ""
                                        strSQL = strSQL + " insert into 公文_B_相关文件附件 (" + vbCr
                                        strSQL = strSQL + "   文件标识, 序号, 说明, 页数, 位置" + vbCr
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
                                        '写数据
                                        strSQL = ""
                                        strSQL = strSQL + " insert into 公文_B_相关文件 (" + vbCr
                                        strSQL = strSQL + "   上级文件标识,顺序号,当前文件标识,顶层文件标识" + vbCr
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
                                        strErrMsg = "错误：无效类型！"
                                        GoTo rollDatabaseAndFile
                                End Select
                            Next
                        End With

                        '删除所有备份文件
                        If blnNewTrans = True Then
                            If Me.doDeleteBackupFiles_XGWJFJ(strErrMsg, objFTPProperty, objOldFJData) = False Then
                                '可以不成功，形成垃圾文件！
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

                '提交事务
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

            '返回
            doSaveXgwj = True
            Exit Function

rollDatabaseAndFile:
            If blnNewTrans = True Then
                objSqlTransaction.Rollback()
                If Me.doRestoreFiles_XGWJFJ(strSQL, strWJBS, intWJND, objFTPProperty, objNewFJData, objOldFJData) = False Then
                    '无法恢复成功，尽力了！
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
        ' 保存相关文件数据：相关附件和相关链接
        '     strErrMsg              ：如果错误，则返回错误信息
        '     blnEnforeEdit          ：是否强制修改
        '     strUserXM              ：操作员名称
        '     objNewData             ：相关链接+相关附件新值
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
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

            '初始化
            doSaveXgwj = False
            strErrMsg = ""

            Try
                '检查
                If Me.IsInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "错误：未传入新的数据！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim

                '获取现有信息
                objSqlConnection = Me.SqlConnection
                strWJBS = Me.WJBS
                If strWJBS Is Nothing Then strWJBS = ""
                strWJBS = strWJBS.Trim
                If strWJBS = "" Then
                    Exit Try
                End If

                '获取FTP连接参数
                If objdacXitongpeizhi.getFtpServerParam(strErrMsg, objSqlConnection, objFTPProperty) = False Then
                    GoTo errProc
                End If

                '获取旧相关文件数据
                If Me.getXgwjData(strErrMsg, objOldData) = False Then
                    GoTo errProc
                End If
                '拆离旧相关附件
                If Me.doSplitXGWJDataSet(strErrMsg, objOldData, objOldFJData) = False Then
                    GoTo errProc
                End If
                '拆离新相关附件
                If Me.doSplitXGWJDataSet(strErrMsg, objNewData, objNewFJData) = False Then
                    GoTo errProc
                End If

                '开始事务
                objSqlTransaction = objSqlConnection.BeginTransaction()

                '保存数据
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '删除“公文_B_相关文件”数据
                    strSQL = ""
                    strSQL = strSQL + " delete from 公文_B_相关文件 " + vbCr
                    strSQL = strSQL + " where 上级文件标识 = @wjbs" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '删除“公文_B_相关文件附件”数据
                    strSQL = ""
                    strSQL = strSQL + " delete from 公文_B_相关文件附件 " + vbCr
                    strSQL = strSQL + " where 文件标识 = @wjbs" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    Try
                        '在源文件的同目录中将文件备份
                        If Me.doBackupFiles_XGWJFJ(strErrMsg, objFTPProperty, objOldFJData) = False Then
                            GoTo rollDatabaseAndFile
                        End If

                        '保存新数据
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
                                '获取相关文件类型
                                intLBBS = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_LBBS), 0)

                                '分类处理
                                Select Case intLBBS
                                    Case Xydc.Platform.Common.Data.FlowData.enumXGWJLB.FujianFile
                                        '获取原FTP路径和新本地文件路径
                                        strOldFile = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_FJWZ), "")
                                        strLocFile = objPulicParameters.getObjectValue(.DefaultView.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_BDWJ), "")
                                        strNewFile = ""

                                        '上传文件
                                        If strLocFile <> "" Then
                                            '文件存在?
                                            If objBaseLocalFile.doFileExisted(strErrMsg, strLocFile, blnExisted) = False Then
                                                GoTo rollDatabaseAndFile
                                            End If
                                            If blnExisted = True Then
                                                '获取FTP文件路径
                                                If Me.getFTPFileName_XGWJFJ(strErrMsg, strLocFile, intWJND, strWJBS, i + 1, strBasePath, strNewFile) = False Then
                                                    GoTo rollDatabaseAndFile
                                                End If
                                                '有本地文件，则需要上载
                                                With objFTPProperty
                                                    strUrl = .getUrl(strNewFile)
                                                    If objBaseFTP.doPutFile(strErrMsg, strLocFile, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword) = False Then
                                                        GoTo rollDatabaseAndFile
                                                    End If
                                                End With
                                            Else
                                                strErrMsg = "错误：[" + strLocFile + "]不存在！"
                                                GoTo rollDatabaseAndFile
                                            End If
                                        Else
                                            If strOldFile <> "" Then
                                                '
                                                '未从FTP服务器下载
                                                '
                                                '从备份文件恢复到当前行的文件
                                                With objFTPProperty
                                                    strUrl = .getUrl(strOldFile + strBakExt)
                                                    If objBaseFTP.isFileExisted(strErrMsg, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword, blnExisted) = False Then
                                                        '可以不成功
                                                    Else
                                                        If blnExisted = True Then
                                                            '获取FTP文件路径
                                                            If Me.getFTPFileName_XGWJFJ(strErrMsg, strOldFile, intWJND, strWJBS, i + 1, strBasePath, strNewFile) = False Then
                                                                GoTo rollDatabaseAndFile
                                                            End If
                                                            strToUrl = .getUrl(strNewFile)
                                                            '更改文件名
                                                            If objBaseFTP.doRenameFile(strErrMsg, strUrl, strToUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword) = False Then
                                                                GoTo rollDatabaseAndFile
                                                            End If
                                                        End If
                                                    End If
                                                End With
                                            Else
                                                '没有电子文件
                                            End If
                                        End If

                                        '写数据
                                        strSQL = ""
                                        strSQL = strSQL + " insert into 公文_B_相关文件附件 (" + vbCr
                                        strSQL = strSQL + "   文件标识, 序号, 说明, 页数, 位置" + vbCr
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
                                        '写数据
                                        strSQL = ""
                                        strSQL = strSQL + " insert into 公文_B_相关文件 (" + vbCr
                                        strSQL = strSQL + "   上级文件标识,顺序号,当前文件标识,顶层文件标识" + vbCr
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
                                        strErrMsg = "错误：无效类型！"
                                        GoTo rollDatabaseAndFile
                                End Select
                            Next
                        End With

                        '如果是强制编辑
                        Dim strCZSM As String = Xydc.Platform.Common.Workflow.BaseFlowObject.LOGO_QXBJ
                        If blnEnforeEdit = True Then
                            If Me.doWriteFileLogo(strErrMsg, objSqlTransaction, strUserXM, strCZSM) = False Then
                                GoTo rollDatabaseAndFile
                            End If
                        End If

                        '解除文件编辑封锁
                        If Me.doLockFile(strErrMsg, objSqlTransaction, "", False) = False Then
                            GoTo rollDatabaseAndFile
                        End If

                        '删除所有备份文件
                        If Me.doDeleteBackupFiles_XGWJFJ(strErrMsg, objFTPProperty, objOldFJData) = False Then
                            '可以不成功，形成垃圾文件！
                        End If

                    Catch ex As Exception
                        strErrMsg = ex.Message
                        GoTo rollDatabaseAndFile
                    End Try

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo rollDatabase
                End Try

                '提交事务
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

            '返回
            doSaveXgwj = True
            Exit Function

rollDatabaseAndFile:
            objSqlTransaction.Rollback()
            If Me.doRestoreFiles_XGWJFJ(strSQL, strWJBS, intWJND, objFTPProperty, objNewFJData, objOldFJData) = False Then
                '无法恢复成功，尽力了！
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
        ' Update状态下保存相关文件附件的单个附件数据(序号不能修改！)
        '     strErrMsg              ：如果错误，则返回错误信息
        '     blnEnforeEdit          ：是否强制修改
        '     strUserXM              ：操作员名称
        '     objNewData             ：记录新值
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
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

            '初始化
            doSaveXgwjFujian = False
            strErrMsg = ""

            Try
                '检查
                If Me.IsInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "错误：未传入新的数据！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim

                '获取现有信息
                objSqlConnection = Me.SqlConnection
                strWJBS = Me.WJBS
                If strWJBS Is Nothing Then strWJBS = ""
                strWJBS = strWJBS.Trim
                If strWJBS = "" Then
                    Exit Try
                End If

                '获取FTP连接参数
                If objdacXitongpeizhi.getFtpServerParam(strErrMsg, objSqlConnection, objFTPProperty) = False Then
                    GoTo errProc
                End If

                '获取文件序号及新本地文件路径
                intWJXH = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_XIANGGUANWENJIANFUJIAN_WJXH), 0)
                strBDWJ = objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_XIANGGUANWENJIANFUJIAN_BDWJ), "")

                '获取原附件数据
                If Me.getXgwjFujianData(strErrMsg, intWJXH, objOldData) = False Then
                    GoTo errProc
                End If
                If objOldData.Tables(strTable).DefaultView.Count < 1 Then
                    '记录不存在，不在不例程处理范围！
                    Exit Try
                End If

                '开始事务
                objSqlTransaction = objSqlConnection.BeginTransaction()

                '保存数据
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    Try
                        Dim strNewFile As String = ""
                        If strBDWJ <> "" Then
                            '获取新本地文件路径
                            Dim strLocFile As String = strBDWJ
                            Dim blnExisted As Boolean

                            '文件存在?
                            If objBaseLocalFile.doFileExisted(strErrMsg, strLocFile, blnExisted) = False Then
                                GoTo rollDatabase
                            End If
                            If blnExisted = False Then
                                strErrMsg = "错误：[" + strLocFile + "]不存在！"
                                GoTo rollDatabase
                            End If

                            '上传新文件
                            Dim strBasePath As String = Me.getBasePath_XGWJFJ
                            Dim strUrl As String
                            '获取FTP文件路径
                            If Me.getFTPFileName_XGWJFJ(strErrMsg, strLocFile, intWJND, strWJBS, intWJXH, strBasePath, strNewFile) = False Then
                                GoTo rollDatabase
                            End If
                            '在源文件的同目录中将文件备份
                            If Me.doBackupFiles_XGWJFJ(strErrMsg, objFTPProperty, objOldData) = False Then
                                GoTo rollDatabaseAndFile
                            End If
                            '上载
                            With objFTPProperty
                                strUrl = .getUrl(strNewFile)
                                If objBaseFTP.doPutFile(strErrMsg, strLocFile, strUrl, .UserID, .Password, .ProxyUrl, .ProxyUserID, .ProxyPassword) = False Then
                                    GoTo rollDatabaseAndFile
                                End If
                            End With
                        End If

                        '写数据
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@wjsm", objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_XIANGGUANWENJIANFUJIAN_WJSM), ""))
                        objSqlCommand.Parameters.AddWithValue("@wjys", objPulicParameters.getObjectValue(objNewData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_XIANGGUANWENJIANFUJIAN_WJYS), 0))
                        If strNewFile <> "" Then
                            strSQL = ""
                            strSQL = strSQL + " update 公文_B_相关文件附件 set " + vbCr
                            strSQL = strSQL + "   说明 = @wjsm, 页数 = @wjys, 位置 = @wjwz" + vbCr
                            strSQL = strSQL + " from 公文_B_相关文件附件" + vbCr
                            strSQL = strSQL + " where 文件标识 = @wjbs" + vbCr
                            strSQL = strSQL + " and   序号     = @wjxh" + vbCr
                            objSqlCommand.Parameters.AddWithValue("@wjwz", strNewFile)
                        Else
                            strSQL = ""
                            strSQL = strSQL + " update 公文_B_相关文件附件 set " + vbCr
                            strSQL = strSQL + "   说明 = @wjsm, 页数 = @wjys" + vbCr
                            strSQL = strSQL + " from 公文_B_相关文件附件" + vbCr
                            strSQL = strSQL + " where 文件标识 = @wjbs" + vbCr
                            strSQL = strSQL + " and   序号     = @wjxh" + vbCr
                        End If
                        objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                        objSqlCommand.Parameters.AddWithValue("@wjxh", intWJXH)
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.ExecuteNonQuery()

                        '如果是强制编辑
                        Dim strCZSM As String = Xydc.Platform.Common.Workflow.BaseFlowObject.LOGO_QXBJ
                        If blnEnforeEdit = True Then
                            If Me.doWriteFileLogo(strErrMsg, objSqlTransaction, strUserXM, strCZSM) = False Then
                                GoTo rollDatabaseAndFile
                            End If
                        End If

                        '解除文件编辑封锁
                        If Me.doLockFile(strErrMsg, objSqlTransaction, "", False) = False Then
                            GoTo rollDatabaseAndFile
                        End If

                        '删除所有备份文件
                        If Me.doDeleteBackupFiles_XGWJFJ(strErrMsg, objFTPProperty, objOldData) = False Then
                            '可以不成功，形成垃圾文件！
                        End If

                    Catch ex As Exception
                        strErrMsg = ex.Message
                        GoTo rollDatabaseAndFile
                    End Try

                Catch ex As Exception
                    GoTo rollDatabase
                End Try

                '提交事务
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

            '返回
            doSaveXgwjFujian = True
            Exit Function

rollDatabaseAndFile:
            objSqlTransaction.Rollback()
            If Me.doRestoreFiles_XGWJFJ(strSQL, strWJBS, intWJND, objFTPProperty, Nothing, objOldData) = False Then
                '无法恢复成功，尽力了！
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
        ' 保存“公文_B_交接”的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objOldData           ：旧数据
        '     objNewData           ：新数据
        '     objenumEditType      ：编辑类型
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        ' 修改记录
        '      增加
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

            '初始化
            doSaveData_Jiaojie = False
            strErrMsg = ""

            Try
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If

                '获取连接
                objSqlConnection = Me.SqlConnection

                '检查数据
                If Me.doVerifyData_Jiaojie(strErrMsg, objOldData, objNewData, objenumEditType) = False Then
                    GoTo errProc
                End If

                '开始事务
                Try
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '保存数据
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '计算SQL
                    Dim intDefaultValue As Integer = 0
                    Dim strFileds As String = ""
                    Dim strValues As String = ""
                    Dim strField As String = ""
                    Dim intCount As Integer = 0
                    Dim i As Integer = 0
                    Select Case objenumEditType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew, _
                            Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eCpyNew
                            '计算更新字段列表
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
                            '准备SQL
                            strSQL = ""
                            strSQL = strSQL + " insert into 公文_B_交接 (" + strFileds + ")"
                            strSQL = strSQL + " values (" + strValues + ")"
                            '准备参数
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
                            '执行SQL
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()

                        Case Else
                            '获取原“文件标识”
                            Dim strOldWJBS As String
                            Dim intOldJJXH As Integer
                            strOldWJBS = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_WJBS), "")
                            intOldJJXH = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JJXH), 0)
                            '计算更新字段列表
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
                            '准备SQL
                            strSQL = ""
                            strSQL = strSQL + " update 公文_B_交接 set " + vbCr
                            strSQL = strSQL + "   " + strFileds + vbCr
                            strSQL = strSQL + " where 文件标识 = @oldwjbs" + vbCr
                            strSQL = strSQL + " and   交接序号 = @oldjjxh" + vbCr
                            '准备参数
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
                            '执行SQL
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()
                    End Select
                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '提交事务
                objSqlTransaction.Commit()
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doSaveData_Jiaojie = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 更新“公文_B_交接”的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strWhere             ：更新条件
        '     strFileds            ：更新语句
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        ' 修改记录
        '      增加
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

            '初始化
            doUpdateJiaojie = False
            strErrMsg = ""

            Try
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If

                If strFileds = "" Then
                    Exit Try
                End If

                '获取连接
                objSqlConnection = Me.SqlConnection

                '开始事务
                Try
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '保存数据
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '准备SQL
                    strSQL = ""
                    strSQL = strSQL + " update 公文_B_交接 " + vbCr
                    strSQL = strSQL + "   " + strFileds + vbCr
                    If strWhere <> "" Then
                        strSQL = strSQL + strWhere
                    End If

                    '执行SQL
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '提交事务
                objSqlTransaction.Commit()
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doUpdateJiaojie = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function


        '----------------------------------------------------------------
        ' 根据strWJBS获取“公文_B_办理”数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strWhere             ：搜索条件
        '     blnUnused            ：接口重载
        '     objBanliData         ：返回办理数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        ' 修改记录
        '      增加
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '创建数据集
                objTempBanliData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_BANLI)
                If strWJBS = "" Then Exit Try

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '执行检索
                With Me.SqlDataAdapter
                    '计算交接信息
                    strSQL = ""
                    strSQL = strSQL + " select a.*" + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select * from 公文_B_办理" + vbCr
                    strSQL = strSQL + "   where 文件标识 = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + " ) a" + vbCr
                    If strWhere <> "" Then
                        strSQL = strSQL + " where " + strWhere + vbCr
                    End If
                    strSQL = strSQL + " order by a.显示序号,a.办理日期 desc" + vbCr

                    '设置参数
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    '执行操作
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
        ' 检查“公文_B_办理”的数据的合法性
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objOldData           ：旧数据
        '     objNewData           ：新数据
        '     objenumEditType      ：编辑类型
        ' 返回
        '     True                 ：合法
        '     False                ：不合法或其他程序错误
        ' 修改记录
        '      增加
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "错误：未传入新的数据！"
                    GoTo errProc
                End If
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew, _
                        Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eCpyNew
                    Case Else
                        If objOldData Is Nothing Then
                            strErrMsg = "错误：未传入旧的数据！"
                            GoTo errProc
                        End If
                End Select

                '获取信息
                objSqlConnection = Me.SqlConnection
                strWJBS = Me.WJBS

                '获取表结构定义
                strSQL = "select top 0 * from 公文_B_办理"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, objSqlConnection, strSQL, "公文_B_办理", objDataSet) = False Then
                    GoTo errProc
                End If

                '检查数据长度
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
                            '自动列
                            If strValue = "" Then
                                strValue = strWJBS
                            End If

                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_BANLI_JJXH
                            If strValue = "" Then
                                strErrMsg = "错误：没有输入[" + strField + "]！"
                                GoTo errProc
                            End If
                            If objPulicParameters.isFloatString(strValue) = False Then
                                strErrMsg = "错误：[" + strValue + "]是无效的数值！"
                                GoTo errProc
                            End If
                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_BANLI_XSXH
                            If strValue <> "" Then
                                If objPulicParameters.isIntegerString(strValue) = False Then
                                    strErrMsg = "错误：[" + strValue + "]是无效的数字！"
                                    GoTo errProc
                                End If
                            End If

                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_BANLI_BLRQ, _
                            Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_BANLI_DLRQ, _
                            Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_BANLI_TXRQ
                            If strValue <> "" Then
                                If objPulicParameters.isDatetimeString(strValue) = False Then
                                    strErrMsg = "错误：[" + strValue + "]是无效的日期！"
                                    GoTo errProc
                                End If
                            End If

                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_BANLI_BLLX, _
                            Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_BANLI_BLZL, _
                            Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_BANLI_BLR
                            If strValue = "" Then
                                strErrMsg = "错误：没有输入[" + strField + "]！"
                                GoTo errProc
                            End If
                            With objDataSet.Tables(0).Columns(strField)
                                intLen = objPulicParameters.getStringLength(strValue)
                                If intLen > .MaxLength Then
                                    strErrMsg = "错误：[" + strField + "]长度不能超过[" + .MaxLength.ToString() + "]，实际有[" + intLen.ToString() + "]！"
                                    GoTo errProc
                                End If
                            End With
                        Case Else
                            If strValue <> "" Then
                                With objDataSet.Tables(0).Columns(strField)
                                    intLen = objPulicParameters.getStringLength(strValue)
                                    If intLen > .MaxLength Then
                                        strErrMsg = "错误：[" + strField + "]长度不能超过[" + .MaxLength.ToString() + "]，实际有[" + intLen.ToString() + "]！"
                                        GoTo errProc
                                    End If
                                End With
                            End If
                    End Select
                    objNewData(strField) = strValue
                Next
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)

                '检查约束
                Dim intNewJJXH As Integer = objPulicParameters.getObjectValue(objNewData.Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_BANLI_JJXH), 0)
                objListDictionary = New System.Collections.Specialized.ListDictionary
                Select Case objenumEditType
                    Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew
                        strSQL = "select * from 公文_B_办理 where 文件标识 = @wjbs and 交接序号 = @jjxh"
                        objListDictionary.Add("@wjbs", strWJBS)
                        objListDictionary.Add("@jjxh", intNewJJXH)
                    Case Else
                        Dim intOldJJXH As Integer = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_BANLI_JJXH), 0)
                        strSQL = "select * from 公文_B_办理 where 文件标识 = @wjbs and 交接序号 = @jjxh and 交接序号 <> @oldjjxh"
                        objListDictionary.Add("@wjbs", strWJBS)
                        objListDictionary.Add("@jjxh", intNewJJXH)
                        objListDictionary.Add("@oldjjxh", intOldJJXH)
                End Select
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objListDictionary, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count > 0 Then
                    strErrMsg = "错误：[" + intNewJJXH.ToString + "]已经存在！"
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
        ' 保存“公文_B_办理”的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objOldData           ：旧数据
        '     objNewData           ：新数据
        '     objenumEditType      ：编辑类型
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        ' 修改记录
        '      增加
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

            '初始化
            doSaveData_Banli = False
            strErrMsg = ""

            Try
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If

                '获取连接
                objSqlConnection = Me.SqlConnection

                '检查数据
                If Me.doVerifyData_Banli(strErrMsg, objOldData, objNewData, objenumEditType) = False Then
                    GoTo errProc
                End If

                '开始事务
                Try
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '保存数据
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '计算SQL
                    Dim intDefaultValue As Integer = 0
                    Dim strFileds As String = ""
                    Dim strValues As String = ""
                    Dim strField As String = ""
                    Dim intCount As Integer = 0
                    Dim i As Integer = 0
                    Select Case objenumEditType
                        Case Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eAddNew, _
                            Xydc.Platform.Common.Utilities.PulicParameters.enumEditType.eCpyNew
                            '计算更新字段列表
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
                            '准备SQL
                            strSQL = ""
                            strSQL = strSQL + " insert into 公文_B_办理 (" + strFileds + ")"
                            strSQL = strSQL + " values (" + strValues + ")"
                            '准备参数
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
                            '执行SQL
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()

                        Case Else
                            '获取原“文件标识”
                            Dim strOldWJBS As String
                            Dim intOldJJXH As Integer
                            strOldWJBS = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_BANLI_WJBS), "")
                            intOldJJXH = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_BANLI_JJXH), 0)
                            '计算更新字段列表
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
                            '准备SQL
                            strSQL = ""
                            strSQL = strSQL + " update 公文_B_办理 set " + vbCr
                            strSQL = strSQL + "   " + strFileds + vbCr
                            strSQL = strSQL + " where 文件标识 = @oldwjbs" + vbCr
                            strSQL = strSQL + " and   交接序号 = @oldjjxh" + vbCr
                            '准备参数
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
                            '执行SQL
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()
                    End Select
                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '提交事务
                objSqlTransaction.Commit()
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doSaveData_Banli = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 在附件缓存数据中删除“公文_B_附件”的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objOldData           ：旧数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Overridable Function doDeleteData_FJ( _
            ByRef strErrMsg As String, _
            ByVal objOldData As System.Data.DataRow) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile

            '初始化
            doDeleteData_FJ = False
            strErrMsg = ""

            Try
                '检查
                If objOldData Is Nothing Then
                    strErrMsg = "错误：未传入要删除的数据！"
                    GoTo errProc
                End If

                '备份临时文件
                Dim strTempFile As String = ""
                strTempFile = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_BDWJ), "")

                '删除数据
                objOldData.Delete()

                '删除临时文件
                If strTempFile <> "" Then
                    If objBaseLocalFile.doDeleteFile(strErrMsg, strTempFile) = False Then
                        '形成垃圾文件
                    End If
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)

            '返回
            doDeleteData_FJ = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 在相关文件附件的缓存数据中删除相关文件的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objOldData           ：旧数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Overridable Function doDeleteData_XGWJ( _
            ByRef strErrMsg As String, _
            ByVal objOldData As System.Data.DataRow) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile

            '初始化
            doDeleteData_XGWJ = False
            strErrMsg = ""

            Try
                '检查
                If objOldData Is Nothing Then
                    strErrMsg = "错误：未传入要删除的数据！"
                    GoTo errProc
                End If

                '备份临时文件
                Dim strTempFile As String = ""
                strTempFile = objPulicParameters.getObjectValue(objOldData.Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_BDWJ), "")

                '删除数据
                objOldData.Delete()

                '删除临时文件
                If strTempFile <> "" Then
                    If objBaseLocalFile.doDeleteFile(strErrMsg, strTempFile) = False Then
                        '形成垃圾文件
                    End If
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)

            '返回
            doDeleteData_XGWJ = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 删除“公文_B_办理”的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     intJJXH              ：交接序号
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        ' 修改记录
        '      增加
        '----------------------------------------------------------------
        Public Overridable Function doDeleteData_Banli( _
            ByRef strErrMsg As String, _
            ByVal intJJXH As Integer) As Boolean

            Dim objSqlTransaction As System.Data.SqlClient.SqlTransaction = Nothing
            Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Nothing
            Dim objSqlCommand As System.Data.SqlClient.SqlCommand = Nothing
            Dim strSQL As String = ""

            '初始化
            doDeleteData_Banli = False
            strErrMsg = ""

            Try
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If

                '获取连接
                Dim strWJBS As String = Me.WJBS
                objSqlConnection = Me.SqlConnection

                '开始事务
                Try
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '删除数据
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行SQL
                    strSQL = "delete from 公文_B_办理 where 文件标识 = @wjbs and 交接序号 = @jjxh"
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

                '提交事务
                objSqlTransaction.Commit()
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            '返回
            doDeleteData_Banli = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 在附件缓存数据中将指定行objSrcData移动到指定行objDesData
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objSrcData           ：要移动的数据
        '     objDesData           ：要移动到的数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Overridable Function doMoveTo_FJ( _
            ByRef strErrMsg As String, _
            ByRef objSrcData As System.Data.DataRow, _
            ByRef objDesData As System.Data.DataRow) As Boolean

            '初始化
            doMoveTo_FJ = False
            strErrMsg = ""

            Try
                '检查
                If objSrcData Is Nothing Then
                    strErrMsg = "错误：未传入要移动的数据！"
                    GoTo errProc
                End If
                If objDesData Is Nothing Then
                    strErrMsg = "错误：未传入要移动到的数据！"
                    GoTo errProc
                End If

                '移动
                Dim strField As String = Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_XSXH
                Dim objTemp As Object
                objTemp = objSrcData.Item(strField)
                objSrcData.Item(strField) = objDesData.Item(strField)
                objDesData.Item(strField) = objTemp

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            '返回
            doMoveTo_FJ = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 在相关文件附件的缓存数据中将指定行objSrcData移动到指定行objDesData
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objSrcData           ：要移动的数据
        '     objDesData           ：要移动到的数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Overridable Function doMoveTo_XGWJ( _
            ByRef strErrMsg As String, _
            ByRef objSrcData As System.Data.DataRow, _
            ByRef objDesData As System.Data.DataRow) As Boolean

            '初始化
            doMoveTo_XGWJ = False
            strErrMsg = ""

            Try
                '检查
                If objSrcData Is Nothing Then
                    strErrMsg = "错误：未传入要移动的数据！"
                    GoTo errProc
                End If
                If objDesData Is Nothing Then
                    strErrMsg = "错误：未传入要移动到的数据！"
                    GoTo errProc
                End If

                '移动
                Dim strField As String = Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_XSXH
                Dim objTemp As Object
                objTemp = objSrcData.Item(strField)
                objSrcData.Item(strField) = objDesData.Item(strField)
                objDesData.Item(strField) = objTemp

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            '返回
            doMoveTo_XGWJ = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 在附件缓存数据中自动调整显示序号=数据集中的行序号+1
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objFJData            ：缓存数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Overridable Function doAutoAdjustXSXH_FJ( _
            ByRef strErrMsg As String, _
            ByRef objFJData As Xydc.Platform.Common.Data.FlowData) As Boolean

            '初始化
            doAutoAdjustXSXH_FJ = False
            strErrMsg = ""

            Try
                '检查
                If objFJData Is Nothing Then
                    strErrMsg = "错误：未传入文件数据！"
                    GoTo errProc
                End If

                '自动设置序号
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

            '返回
            doAutoAdjustXSXH_FJ = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 在相关文件附件的缓存数据中自动调整显示序号=数据集中的行序号+1
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objXGWJData          ：缓存数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Overridable Function doAutoAdjustXSXH_XGWJ( _
            ByRef strErrMsg As String, _
            ByRef objXGWJData As Xydc.Platform.Common.Data.FlowData) As Boolean

            '初始化
            doAutoAdjustXSXH_XGWJ = False
            strErrMsg = ""

            Try
                '检查
                If objXGWJData Is Nothing Then
                    strErrMsg = "错误：未传入文件数据！"
                    GoTo errProc
                End If

                '自动设置序号
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

            '返回
            doAutoAdjustXSXH_XGWJ = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据objJSRDataSet进行发送处理
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objJSRDataSet        ：接收人数据集
        '     strFSXH              ：发送批次=发送序号
        '     strYJJH              ：最近发给当前发送人的交接序号
        '     intBLJB              ：发送人最近未办完事宜的事宜级别
        '     strAddedJJXHList     ：返回新增加的交接序号
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If objJSRDataSet Is Nothing Then
                    strErrMsg = "错误：未指定[接收人]数据！"
                    GoTo errProc
                End If
                If objJSRDataSet.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_VT_WENJIANFASONG) Is Nothing Then
                    strErrMsg = "错误：未指定[接收人]数据！"
                    GoTo errProc
                End If
                With objJSRDataSet.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_VT_WENJIANFASONG)
                    If .Rows.Count < 1 Then
                        strErrMsg = "错误：未指定[接收人]数据！"
                        GoTo errProc
                    End If
                End With
                If strFSXH Is Nothing Then strFSXH = ""
                strFSXH = strFSXH.Trim
                If strFSXH = "" Then
                    strErrMsg = "错误：未指定[发送批次]数据！"
                    GoTo errProc
                End If
                If strYJJH Is Nothing Then strYJJH = ""
                strYJJH = strYJJH.Trim
                If strYJJH = "" Then
                    strErrMsg = "错误：未指定[原交接序号]数据！"
                    GoTo errProc
                End If
                If strAddedJJXHList Is Nothing Then strAddedJJXHList = ""
                strAddedJJXHList = strAddedJJXHList.Trim

                '获取文件信息
                Dim strTaskStatusWJS As String = Me.FlowData.TASKSTATUS_WJS                
                Dim strBLLX As String = Me.FlowBLLXName                
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then
                    Exit Try
                End If

                '获取事务连接
                objSqlConnection = Me.SqlConnection

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '是否为审批事宜
                Dim blnIsShenpiTask(2) As Boolean
                If Me.isShenpiTask(strErrMsg, "", intBLJB, blnIsShenpiTask(0)) = False Then
                    GoTo errProc
                End If

                '逐条发送
                Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate
                Dim strRelaFields As String = "文件标识" + strSep + "发送序号"
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
                        '获取新交接单号
                        If objdacCommon.getNewCode(strErrMsg, objSqlConnection, "交接序号", "文件标识", strWJBS, "公文_B_交接", True, strJJXH) = False Then
                            GoTo errProc
                        End If

                        '计算接收序号
                        If objdacCommon.getNewCode(strErrMsg, objSqlConnection, "接收序号", strRelaFields, strRelaValue, "公文_B_交接", True, strJSXH) = False Then
                            GoTo errProc
                        End If

                        '计算交接标识
                        intLevel = CType(.Rows(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_VT_WENJIANFASONG_SYJB), Integer)
                        If Me.isShenpiTask(strErrMsg, "", intLevel, blnIsShenpiTask(1)) = False Then
                            GoTo errProc
                        End If
                        If blnIsShenpiTask(0) = False And blnIsShenpiTask(1) = False Then
                            '不显示在我的待批事宜中
                            '发送1，发送人0，接收人1，被退回0，被收回0，通知0，答复0
                            strJJBS = "10100000"
                        Else
                            If intBLJB < intLevel Then
                                '显示在我的待批事宜中
                                '发送1，发送人1，接收人1，被退回0，被收回0，通知0，答复0
                                strJJBS = "11100000"
                            Else
                                '不显示在我的待批事宜中
                                '发送1，发送人0，接收人1，被退回0，被收回0，通知0，答复0
                                strJJBS = "10100000"
                            End If
                        End If

                        '设置记录新值
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


                        '开始事务
                        objSqlTransaction = objSqlConnection.BeginTransaction
                        objSqlCommand.Transaction = objSqlTransaction

                        '事务处理
                        Try
                            '清空参数
                            objSqlCommand.Parameters.Clear()

                            '准备字段、值参数
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

                            '计算SQL
                            strSQL = " insert into 公文_B_交接 (" + strFields + ") values (" + strValues + ")"

                            '执行
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()

                        Catch ex As Exception
                            objSqlTransaction.Rollback()
                            strErrMsg = ex.Message
                            GoTo errProc
                        End Try

                        '提交事务
                        objSqlTransaction.Commit()

                        '清空缓冲区
                        objValues.Clear()

                        '记录增加的交接
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
        ' 设置strBLR的事宜办理完毕
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strBLR               ：当前办理人
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strBLR Is Nothing Then strBLR = ""
                strBLR = strBLR.Trim
                If strBLR = "" Then
                    strErrMsg = "错误：未指定[当前办理人]数据！"
                    GoTo errProc
                End If

                '获取文件信息
                Dim strTaskStatusWJSList As String = Me.FlowData.TaskStatusWJSList
                Dim strTaskStatusZJBList As String = Me.FlowData.TaskStatusZJBList
                Dim strTaskStatusYWC As String = Me.FlowData.TASKSTATUS_YWC
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '获取事务连接
                objSqlConnection = Me.SqlConnection

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '开始事务
                objSqlTransaction = objSqlConnection.BeginTransaction
                objSqlCommand.Transaction = objSqlTransaction

                '执行事务
                Try
                    '未接收的事宜办理完毕
                    strSQL = ""
                    strSQL = strSQL + " update 公文_B_交接 set " + vbCr
                    strSQL = strSQL + "   接收日期 = '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "'," + vbCr
                    strSQL = strSQL + "   接收纸质文件 = 发送纸质文件," + vbCr
                    strSQL = strSQL + "   接收电子文件 = 发送电子文件," + vbCr
                    strSQL = strSQL + "   接收纸质附件 = 发送纸质附件," + vbCr
                    strSQL = strSQL + "   接收电子附件 = 发送电子附件," + vbCr
                    strSQL = strSQL + "   办理状态 = '" + strTaskStatusYWC + "'," + vbCr
                    strSQL = strSQL + "   完成日期 = '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "' " + vbCr
                    strSQL = strSQL + " where 文件标识 = '" + strWJBS + "' " + vbCr
                    strSQL = strSQL + " and   接收人   = '" + strBLR + "' " + vbCr
                    strSQL = strSQL + " and   办理状态 in (" + strTaskStatusWJSList + ") " + vbCr
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.ExecuteNonQuery()

                    '未办完的事宜办理完毕
                    strSQL = ""
                    strSQL = strSQL + " update 公文_B_交接 set " + vbCr
                    strSQL = strSQL + "   办理状态 = '" + strTaskStatusYWC + "'," + vbCr
                    strSQL = strSQL + "   完成日期 = '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "' " + vbCr
                    strSQL = strSQL + " where 文件标识 = '" + strWJBS + "' " + vbCr
                    strSQL = strSQL + " and   接收人   = '" + strBLR + "' " + vbCr
                    strSQL = strSQL + " and   办理状态 in (" + strTaskStatusZJBList + ") " + vbCr
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '提交事务
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
        ' 设置strBLR的事宜办理完毕
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strBLR               ：当前办理人
        '     strNewJJXHList       ：不能设置完毕的交接单
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strBLR Is Nothing Then strBLR = ""
                strBLR = strBLR.Trim
                If strBLR = "" Then
                    strErrMsg = "错误：未指定[当前办理人]数据！"
                    GoTo errProc
                End If
                If strNewJJXHList Is Nothing Then strNewJJXHList = ""
                strNewJJXHList = strNewJJXHList.Trim

                '获取文件信息
                Dim strTaskStatusWJSList As String = Me.FlowData.TaskStatusWJSList
                Dim strTaskStatusZJBList As String = Me.FlowData.TaskStatusZJBList
                Dim strTaskStatusYWC As String = Me.FlowData.TASKSTATUS_YWC
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '获取事务连接
                objSqlConnection = Me.SqlConnection

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '开始事务
                objSqlTransaction = objSqlConnection.BeginTransaction
                objSqlCommand.Transaction = objSqlTransaction

                '执行事务
                Try
                    '未接收的事宜办理完毕
                    strSQL = ""
                    strSQL = strSQL + " update 公文_B_交接 set " + vbCr
                    strSQL = strSQL + "   接收日期 = '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "'," + vbCr
                    strSQL = strSQL + "   接收纸质文件 = 发送纸质文件," + vbCr
                    strSQL = strSQL + "   接收电子文件 = 发送电子文件," + vbCr
                    strSQL = strSQL + "   接收纸质附件 = 发送纸质附件," + vbCr
                    strSQL = strSQL + "   接收电子附件 = 发送电子附件," + vbCr
                    strSQL = strSQL + "   办理状态 = '" + strTaskStatusYWC + "'," + vbCr
                    strSQL = strSQL + "   完成日期 = '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "' " + vbCr
                    strSQL = strSQL + " where 文件标识 = '" + strWJBS + "' " + vbCr
                    strSQL = strSQL + " and   接收人   = '" + strBLR + "' " + vbCr
                    strSQL = strSQL + " and   办理状态 in (" + strTaskStatusWJSList + ") " + vbCr
                    If strNewJJXHList <> "" Then
                        strSQL = strSQL + " and   交接序号 not in (" + strNewJJXHList + ")" + vbCr
                    End If
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.ExecuteNonQuery()

                    '未办完的事宜办理完毕
                    strSQL = ""
                    strSQL = strSQL + " update 公文_B_交接 set " + vbCr
                    strSQL = strSQL + "   办理状态 = '" + strTaskStatusYWC + "'," + vbCr
                    strSQL = strSQL + "   完成日期 = '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "' " + vbCr
                    strSQL = strSQL + " where 文件标识 = '" + strWJBS + "' " + vbCr
                    strSQL = strSQL + " and   接收人   = '" + strBLR + "' " + vbCr
                    strSQL = strSQL + " and   办理状态 in (" + strTaskStatusZJBList + ") " + vbCr
                    If strNewJJXHList <> "" Then
                        strSQL = strSQL + " and   交接序号 not in (" + strNewJJXHList + ")" + vbCr
                    End If
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '提交事务
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
        ' 设置或清除strBLR的备忘提醒
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strBLR               ：当前办理人
        '     blnBWTX              ：True-设置备忘提醒，False-清除备忘提醒
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strBLR Is Nothing Then strBLR = ""
                strBLR = strBLR.Trim
                If strBLR = "" Then
                    strErrMsg = "错误：未指定[当前办理人]数据！"
                    GoTo errProc
                End If

                '获取文件信息
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '获取事务连接
                objSqlConnection = Me.SqlConnection

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '开始事务
                objSqlTransaction = objSqlConnection.BeginTransaction
                objSqlCommand.Transaction = objSqlTransaction

                '执行事务
                Try
                    If blnBWTX = True Then
                        strSQL = ""
                        strSQL = strSQL + " update 公文_B_交接 set " + vbCr
                        strSQL = strSQL + "   备忘提醒 = 1 " + vbCr
                        strSQL = strSQL + " where 文件标识 = '" + strWJBS + "'" + vbCr
                        strSQL = strSQL + " and   接收人   = '" + strBLR + "'" + vbCr
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.ExecuteNonQuery()
                    Else
                        strSQL = ""
                        strSQL = strSQL + " update 公文_B_交接 set " + vbCr
                        strSQL = strSQL + "   备忘提醒 = 0 " + vbCr
                        strSQL = strSQL + " where 文件标识 = '" + strWJBS + "'" + vbCr
                        strSQL = strSQL + " and   接收人   = '" + strBLR + "'" + vbCr
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.ExecuteNonQuery()
                    End If

                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '提交事务
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
        ' 向送给当前处理人的有关人员发送回复通知( < intMaxJJXH)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strBLR               ：当前办理人
        '     intMaxJJXH           ：本批次发送前最大的交接序号
        '     strFSXH              ：发送批次=发送序号
        '     strAddedJJXHList     ：返回新增加的交接序号
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strBLR Is Nothing Then strBLR = ""
                strBLR = strBLR.Trim
                If strBLR = "" Then
                    strErrMsg = "错误：未指定[当前办理人]数据！"
                    GoTo errProc
                End If
                If strFSXH Is Nothing Then strFSXH = ""
                strFSXH = strFSXH.Trim
                If strFSXH = "" Then
                    strErrMsg = "错误：未指定[发送批次]数据！"
                    GoTo errProc
                End If
                If strAddedJJXHList Is Nothing Then strAddedJJXHList = ""
                strAddedJJXHList = strAddedJJXHList.Trim

                '获取文件信息                
                Dim strBLLX As String = Me.FlowBLLXName
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '获取事务连接
                objSqlConnection = Me.SqlConnection

                '获取要回复的人员信息
                strSQL = ""
                strSQL = strSQL + " select * from 公文_B_交接" + vbCr
                strSQL = strSQL + " where 文件标识  = '" + strWJBS + "'" + vbCr                '当前文件
                strSQL = strSQL + " and   接收人    = '" + strBLR + "'" + vbCr                 '接收人
                strSQL = strSQL + " and   发送人   <> '" + strBLR + "'" + vbCr                 '发送人不是当前办理人
                strSQL = strSQL + " and   交接序号  <  " + intMaxJJXH.ToString + " " + vbCr    '本次发送之前发生的
                strSQL = strSQL + " and   rtrim(交接标识) like '__1__0_%'" + vbCr                     '接收人能看+非通知类
                strSQL = strSQL + " order by 交接序号 desc"
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables.Count < 1 Then
                    Exit Try
                End If

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '逐条发送
                Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate
                Dim strRelaFields As String = "文件标识" + strSep + "发送序号"
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
                            '不重复发送！
                            GoTo nextRY
                        End If

                        '获取新交接单号
                        If objdacCommon.getNewCode(strErrMsg, objSqlConnection, "交接序号", "文件标识", strWJBS, "公文_B_交接", True, strJJXH) = False Then
                            GoTo errProc
                        End If

                        '计算接收序号
                        If objdacCommon.getNewCode(strErrMsg, objSqlConnection, "接收序号", strRelaFields, strRelaValue, "公文_B_交接", True, strJSXH) = False Then
                            GoTo errProc
                        End If

                        '计算交接标识
                        '发送人看不见，接收人看见，通知消息
                        '发送1，发送人0，接收人1，被退回0，被收回0，通知1，答复0
                        strJJBS = "10100100"

                        '设置记录新值
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


                        '开始事务
                        objSqlTransaction = objSqlConnection.BeginTransaction
                        objSqlCommand.Transaction = objSqlTransaction

                        '事务处理
                        Try
                            '清空参数
                            objSqlCommand.Parameters.Clear()

                            '准备字段、值参数
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

                            '计算SQL
                            strSQL = " insert into 公文_B_交接 (" + strFields + ") values (" + strValues + ")"

                            '执行
                            objSqlCommand.CommandText = strSQL
                            objSqlCommand.ExecuteNonQuery()

                        Catch ex As Exception
                            objSqlTransaction.Rollback()
                            strErrMsg = ex.Message
                            GoTo errProc
                        End Try

                        '提交事务
                        objSqlTransaction.Commit()

                        '清空缓冲区
                        objValues.Clear()

                        '记录加入的交接
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
        ' 删除指定交接序号的交接数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strAddedJJXHList     ：新增加的交接序号
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strAddedJJXHList Is Nothing Then strAddedJJXHList = ""
                strAddedJJXHList = strAddedJJXHList.Trim
                If strAddedJJXHList = "" Then
                    Exit Try
                End If

                '获取文件信息
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then
                    Exit Try
                End If

                '获取事务连接
                objSqlConnection = Me.SqlConnection

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '开始事务
                objSqlTransaction = objSqlConnection.BeginTransaction
                objSqlCommand.Transaction = objSqlTransaction

                '事务处理
                Try
                    strSQL = ""
                    strSQL = strSQL + " delete from 公文_B_交接" + vbCr
                    strSQL = strSQL + " where 文件标识  = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + " and   交接序号 in (" + strAddedJJXHList + ")" + vbCr
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '提交事务
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
        ' 获取目前为止最大的交接序号
        '     strErrMsg            ：如果错误，则返回错误信息
        '     intMaxJJXH           ：返回目前为止最大的交接序号
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If

                '获取文件信息
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then
                    Exit Try
                End If

                '获取事务连接
                objSqlConnection = Me.SqlConnection

                '获取要回复的人员信息
                strSQL = ""
                strSQL = strSQL + " select isnull(max(交接序号),0)" + vbCr
                strSQL = strSQL + " from 公文_B_交接" + vbCr
                strSQL = strSQL + " where 文件标识  = '" + strWJBS + "'" + vbCr
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
        ' 获取strUserXM准备要接收的文件交接信息数据集
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：当前操作员名称
        '     strWhere             ：搜索条件
        '     objJieshouDataSet    ：返回要接收的文件交接信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strTaskStatusWJSList As String = Me.FlowData.TaskStatusWJSList
                Dim strWJBS As String = Me.WJBS

                '创建数据集
                objTempJieshouDataSet = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_VT_WENJIANJIESHOU)
                If strWJBS = "" Then Exit Try

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '执行检索
                With Me.m_objSqlDataAdapter
                    '获取没有接收的交接处理单
                    strSQL = ""
                    strSQL = strSQL + " select a.*" + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select " + vbCr
                    strSQL = strSQL + "     a.发送人," + vbCr
                    strSQL = strSQL + "     a.发送日期," + vbCr
                    strSQL = strSQL + "     交办事宜 = case when substring(a.交接标识,4,1)='1' then '" + Me.FlowData.TASK_THCL + "'" + vbCr
                    strSQL = strSQL + "                     when substring(a.交接标识,5,1)='1' then '" + Me.FlowData.TASK_SHCL + "'" + vbCr
                    strSQL = strSQL + "                     when substring(a.交接标识,7,1)='1' then '" + Me.FlowData.TASK_HFCL + "'" + vbCr
                    strSQL = strSQL + "                     else a.办理子类 end," + vbCr
                    strSQL = strSQL + "     a.接收日期," + vbCr
                    strSQL = strSQL + "     送来纸质文件份数 = a.发送纸质文件," + vbCr
                    strSQL = strSQL + "     送来电子文件份数 = a.发送电子文件," + vbCr
                    strSQL = strSQL + "     送来纸质附件份数 = a.发送纸质附件," + vbCr
                    strSQL = strSQL + "     送来电子附件份数 = a.发送电子附件," + vbCr
                    strSQL = strSQL + "     接收纸质文件份数 = a.发送纸质文件," + vbCr
                    strSQL = strSQL + "     接收电子文件份数 = a.发送电子文件," + vbCr
                    strSQL = strSQL + "     接收纸质附件份数 = a.发送纸质附件," + vbCr
                    strSQL = strSQL + "     接收电子附件份数 = a.发送电子附件," + vbCr
                    strSQL = strSQL + "     a.交接序号," + vbCr
                    strSQL = strSQL + "     a.发送序号," + vbCr
                    strSQL = strSQL + "     a.原交接号," + vbCr
                    strSQL = strSQL + "     a.交接标识," + vbCr
                    strSQL = strSQL + "     a.协办," + vbCr
                    strSQL = strSQL + "     发送人办理事宜 = b.办理子类," + vbCr
                    strSQL = strSQL + "     发送人协办     = b.协办" + vbCr
                    strSQL = strSQL + "   from" + vbCr
                    strSQL = strSQL + "   (" + vbCr
                    strSQL = strSQL + "     select * from 公文_B_交接 " + vbCr
                    strSQL = strSQL + "     where 文件标识 =  '" + strWJBS + "'" + vbCr                 '当前文件
                    strSQL = strSQL + "     and   接收人   =  '" + strUserXM + "'" + vbCr               'strUserXM准备接收
                    strSQL = strSQL + "     and   rtrim(交接标识) like '__1%'" + vbCr                          '接收人能看见
                    strSQL = strSQL + "     and   办理状态 in (" + strTaskStatusWJSList + ")" + vbCr    '接收人未接收的
                    strSQL = strSQL + "   ) a" + vbCr
                    strSQL = strSQL + "   left join" + vbCr
                    strSQL = strSQL + "   (" + vbCr
                    strSQL = strSQL + "     select *" + vbCr
                    strSQL = strSQL + "     from 公文_B_交接" + vbCr
                    strSQL = strSQL + "     where 文件标识 = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + "   ) b on a.原交接号 = b.交接序号" + vbCr
                    strSQL = strSQL + " ) a" + vbCr
                    If strWhere <> "" Then
                        strSQL = strSQL + " where " + strWhere + vbCr
                    End If
                    strSQL = strSQL + " order by a.发送日期 desc" + vbCr

                    '设置参数
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    '执行操作
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
        ' 根据给定参数接收文件(1个交接单)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objJiaojieData       ：要准备更新的交接数据(文件标识、交接序号必须)
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If objJiaojieData Is Nothing Then
                    Exit Try
                End If
                If objJiaojieData.Count < 1 Then
                    Exit Try
                End If

                '获取文件信息
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then
                    Exit Try
                End If

                '获取事务连接
                objSqlConnection = Me.SqlConnection

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '开始事务
                objSqlTransaction = objSqlConnection.BeginTransaction
                objSqlCommand.Transaction = objSqlTransaction

                '事务处理
                Try
                    Dim strFields As String = ""
                    Dim strValue As String
                    Dim intJJXH As Integer
                    Dim intCount As Integer
                    Dim i As Integer

                    '获取交接序号
                    intJJXH = objPulicParameters.getObjectValue(objJiaojieData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JJXH), 0)

                    '准备SQL参数
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

                    '准备SQL
                    strSQL = ""
                    strSQL = strSQL + " update 公文_B_交接 set " + vbCr
                    strSQL = strSQL + "   " + strFields + vbCr
                    strSQL = strSQL + " where 文件标识 = @wjbs" + vbCr
                    strSQL = strSQL + " and   交接序号 = @jjxh" + vbCr

                    '执行
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '提交事务
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
        ' 根据给定参数退回文件并自动发送退回通知(1个交接单)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strYBLSY             ：本次交接的发送人自己的办理事宜
        '     strYXB               ：原协办标志
        '     strFSXH              ：本发送批次号
        '     objJiaojieData       ：要退回的交接数据(文件标识、交接序号必须)
        '     blnCanReadFile       ：保留阅读文件权利
        '     objHasSendNoticeRY   ：(返回)已发退回通知的人员列表
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
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

                '获取文件信息                
                Dim strBLLX As String = Me.FlowBLLXName
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then
                    Exit Try
                End If

                '获取事务连接
                objSqlConnection = Me.SqlConnection

                '获取新交接单号
                Dim strSep As String = objPulicParameters.CharSeparate
                Dim strJJXH As String
                If objdacCommon.getNewCode(strErrMsg, objSqlConnection, "交接序号", "文件标识", strWJBS, "公文_B_交接", True, strJJXH) = False Then
                    GoTo errProc
                End If
                '计算接收序号
                Dim strRelaFields As String = "文件标识" + strSep + "发送序号"
                Dim strRelaValue As String = strWJBS + strSep + strFSXH
                Dim strJSXH As String
                If objdacCommon.getNewCode(strErrMsg, objSqlConnection, "接收序号", strRelaFields, strRelaValue, "公文_B_交接", True, strJSXH) = False Then
                    GoTo errProc
                End If

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '开始事务
                objSqlTransaction = objSqlConnection.BeginTransaction
                objSqlCommand.Transaction = objSqlTransaction

                '事务处理
                Try
                    Dim strFields As String = ""
                    Dim strValues As String = ""
                    Dim strValue As String
                    Dim intJJXH As Integer
                    Dim strFSR As String
                    Dim strJSR As String
                    Dim intCount As Integer
                    Dim i As Integer

                    '获取交接序号
                    intJJXH = objPulicParameters.getObjectValue(objJiaojieData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JJXH), 0)
                    strJSR = objPulicParameters.getObjectValue(objJiaojieData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSR), "")
                    strFSR = objPulicParameters.getObjectValue(objJiaojieData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSR), "")

                    '退回处理
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
                                    '发送1，发送人1，接收人1，被退回0，被收回0，通知0，答复0
                                    strValue = "11100000"
                                Else
                                    '发送1，发送人1，接收人0，被退回0，被收回0，通知0，答复0
                                    strValue = "11000000"
                                End If
                                objSqlCommand.Parameters.AddWithValue("@A" + i.ToString, strValue)
                            Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_BLZT
                                '被退回
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
                    strSQL = strSQL + " update 公文_B_交接 set " + vbCr
                    strSQL = strSQL + "   " + strFields + vbCr
                    strSQL = strSQL + " where 文件标识 = @wjbs" + vbCr
                    strSQL = strSQL + " and   交接序号 = @jjxh" + vbCr
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '发退回处理单
                    If strFSR = "" Then
                        Exit Try
                    End If
                    If Not (objHasSendNoticeRY Is Nothing) Then
                        If Not (objHasSendNoticeRY(strFSR) Is Nothing) Then
                            '已经发送
                            Exit Try
                        End If
                    End If
                    '计算交接标识
                    '发送人看不见，接收人看见，被退回，回复
                    '发送1，发送人0，接收人1，被退回1，被收回0，通知0，答复0
                    Dim strJJBS As String
                    strJJBS = "10110000"
                    '设置记录新值
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


                    '清空参数
                    objSqlCommand.Parameters.Clear()
                    '准备字段、值参数
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
                    '计算SQL
                    strSQL = " insert into 公文_B_交接 (" + strFields + ") values (" + strValues + ")"
                    '执行
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '返回已发退回处理单的人员列表
                    If objHasSendNoticeRY Is Nothing Then
                        objHasSendNoticeRY = New System.Collections.Specialized.NameValueCollection
                    End If
                    objHasSendNoticeRY.Add(strFSR, strFSR)

                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '提交事务
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
        ' 获取strUserXM准备要收回的文件交接信息数据集
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：当前操作员名称
        '     strWhere             ：搜索条件
        '     objShouhuiDataSet    ：返回要收回的文件交接信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strTaskStatusYWCList As String = Me.FlowData.TaskStatusYWCList
                Dim strWJBS As String = Me.WJBS

                '创建数据集
                objTempShouhuiDataSet = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_VT_WENJIANSHOUHUI)
                If strWJBS = "" Then Exit Try

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '执行检索
                With Me.m_objSqlDataAdapter
                    '获取已发送+接收人没有接收的交接处理单
                    strSQL = ""
                    strSQL = strSQL + " select a.*" + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select " + vbCr
                    strSQL = strSQL + "     a.接收人," + vbCr
                    strSQL = strSQL + "     交办事宜 = a.办理子类," + vbCr
                    strSQL = strSQL + "     a.发送日期," + vbCr
                    strSQL = strSQL + "     发送纸质文件份数 = a.发送纸质文件," + vbCr
                    strSQL = strSQL + "     发送电子文件份数 = a.发送电子文件," + vbCr
                    strSQL = strSQL + "     发送纸质附件份数 = a.发送纸质附件," + vbCr
                    strSQL = strSQL + "     发送电子附件份数 = a.发送电子附件," + vbCr
                    strSQL = strSQL + "     a.接收日期," + vbCr
                    strSQL = strSQL + "     接收纸质文件份数 = a.发送纸质文件," + vbCr
                    strSQL = strSQL + "     接收电子文件份数 = a.发送电子文件," + vbCr
                    strSQL = strSQL + "     接收纸质附件份数 = a.发送纸质附件," + vbCr
                    strSQL = strSQL + "     接收电子附件份数 = a.发送电子附件," + vbCr
                    strSQL = strSQL + "     a.交接序号," + vbCr
                    strSQL = strSQL + "     a.发送序号," + vbCr
                    strSQL = strSQL + "     a.原交接号," + vbCr
                    strSQL = strSQL + "     a.交接标识," + vbCr
                    strSQL = strSQL + "     a.发送人," + vbCr
                    strSQL = strSQL + "     a.协办," + vbCr
                    strSQL = strSQL + "     a.是否读过," + vbCr
                    strSQL = strSQL + "     发送人办理事宜 = b.办理子类," + vbCr
                    strSQL = strSQL + "     发送人协办     = b.协办" + vbCr
                    strSQL = strSQL + "   from" + vbCr
                    strSQL = strSQL + "   (" + vbCr
                    strSQL = strSQL + "     select * from 公文_B_交接 " + vbCr
                    strSQL = strSQL + "     where 文件标识 =  '" + strWJBS + "'" + vbCr                  '当前文件
                    strSQL = strSQL + "     and   发送人   =  '" + strUserXM + "'" + vbCr                'strUserXM发送

                    strSQL = strSQL + "     and   接收人   <>    '" + strUserXM + "'" + vbCr                'strUserXM发送

                    strSQL = strSQL + "     and   rtrim(交接标识) like '1_1__0%'" + vbCr                        '已发送+接收人能看+非通知
                    strSQL = strSQL + "     and   办理状态 not in (" + strTaskStatusYWCList + ")" + vbCr '接收人未办完
                    strSQL = strSQL + "   ) a" + vbCr
                    strSQL = strSQL + "   left join" + vbCr
                    strSQL = strSQL + "   (" + vbCr
                    strSQL = strSQL + "     select *" + vbCr
                    strSQL = strSQL + "     from 公文_B_交接" + vbCr
                    strSQL = strSQL + "     where 文件标识 = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + "   ) b on a.原交接号 = b.交接序号" + vbCr
                    strSQL = strSQL + " ) a" + vbCr
                    If strWhere <> "" Then
                        strSQL = strSQL + " where " + strWhere + vbCr
                    End If
                    strSQL = strSQL + " order by a.发送日期 desc" + vbCr

                    '设置参数
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    '执行操作
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
        ' 根据给定参数收回文件并根据需要发送收回通知(1个交接单)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strFSXH              ：本发送批次号
        '     objJiaojieData       ：要收回的交接数据(文件标识、交接序号必须)
        '     blnSendNotice        ：是否要发送收回通知
        '     objHasSendNoticeRY   ：(返回)已发收回通知的人员列表
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
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

                '获取文件信息                
                Dim strBLLX As String = Me.FlowBLLXName
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then
                    Exit Try
                End If

                '获取事务连接
                objSqlConnection = Me.SqlConnection

                '获取新交接单号
                Dim strSep As String = objPulicParameters.CharSeparate
                Dim strJJXH As String
                If objdacCommon.getNewCode(strErrMsg, objSqlConnection, "交接序号", "文件标识", strWJBS, "公文_B_交接", True, strJJXH) = False Then
                    GoTo errProc
                End If
                '计算接收序号
                Dim strRelaFields As String = "文件标识" + strSep + "发送序号"
                Dim strRelaValue As String = strWJBS + strSep + strFSXH
                Dim strJSXH As String
                If objdacCommon.getNewCode(strErrMsg, objSqlConnection, "接收序号", strRelaFields, strRelaValue, "公文_B_交接", True, strJSXH) = False Then
                    GoTo errProc
                End If

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '开始事务
                objSqlTransaction = objSqlConnection.BeginTransaction
                objSqlCommand.Transaction = objSqlTransaction

                '事务处理
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

                    '获取交接序号
                    intJJXH = objPulicParameters.getObjectValue(objJiaojieData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JJXH), 0)
                    intYJJH = objPulicParameters.getObjectValue(objJiaojieData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_YJJH), 0)
                    strJSR = objPulicParameters.getObjectValue(objJiaojieData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_JSR), "")
                    strFSR = objPulicParameters.getObjectValue(objJiaojieData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_FSR), "")

                    '收回处理
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
                                '发送1，发送人1，接收人0，被退回0，被收回0，通知0，答复0
                                strValue = "11000000"
                                objSqlCommand.Parameters.AddWithValue("@A" + i.ToString, strValue)
                            Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_JIAOJIE_BLZT
                                '被收回
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
                    strSQL = strSQL + " update 公文_B_交接 set " + vbCr
                    strSQL = strSQL + "   " + strFields + vbCr
                    strSQL = strSQL + " where 文件标识 = @wjbs" + vbCr
                    strSQL = strSQL + " and   交接序号 = @jjxh" + vbCr
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '设置原交接单的状态为“正在办理”+“接收人能看”+“正常事宜”
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@blzt", Me.FlowData.TASKSTATUS_ZJB)
                    objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                    objSqlCommand.Parameters.AddWithValue("@jjxh", intYJJH)
                    strSQL = ""
                    strSQL = strSQL + " update 公文_B_交接 set " + vbCr
                    strSQL = strSQL + "   办理状态 = @blzt, " + vbCr
                    strSQL = strSQL + "   交接标识 = substring(交接标识,1,2) + '100000'" + vbCr
                    strSQL = strSQL + " where 文件标识 = @wjbs" + vbCr
                    strSQL = strSQL + " and   交接序号 = @jjxh" + vbCr
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '发送收回通知
                    If blnSendNotice = False Then
                        Exit Try
                    End If
                    If strJSR = "" Then
                        Exit Try
                    End If
                    If Not (objHasSendNoticeRY Is Nothing) Then
                        If Not (objHasSendNoticeRY(strJSR) Is Nothing) Then
                            '已经发送
                            Exit Try
                        End If
                    End If
                    '计算交接标识
                    '发送1，发送人0，接收人1，被退回0，被收回0，通知1，答复0
                    Dim strJJBS As String
                    strJJBS = "10100100"
                    '设置记录新值
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


                    '清空参数
                    objSqlCommand.Parameters.Clear()
                    '准备字段、值参数
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
                    '计算SQL
                    strSQL = " insert into 公文_B_交接 (" + strFields + ") values (" + strValues + ")"
                    '执行
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '返回已发收回通知人列表
                    If objHasSendNoticeRY Is Nothing Then
                        objHasSendNoticeRY = New System.Collections.Specialized.NameValueCollection
                    End If
                    objHasSendNoticeRY.Add(strJSR, strJSR)

                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '提交事务
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
        ' strUserXM是否正在编辑文件?
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：用户名称
        '     blnDo                ：返回是否正在编辑文件?
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '获取数据
                strSQL = ""
                strSQL = strSQL + " select a.*" + vbCr
                strSQL = strSQL + " from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select * from 管理_B_文件封锁 " + vbCr
                strSQL = strSQL + "   where 文件标识 = '" + strWJBS + "' " + vbCr
                strSQL = strSQL + " ) a" + vbCr
                strSQL = strSQL + " left join" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select 人员代码 from 公共_B_人员" + vbCr
                strSQL = strSQL + "   where 人员名称 = '" + strUserXM + "'" + vbCr
                strSQL = strSQL + " ) b on a.人员代码 = b.人员代码" + vbCr
                strSQL = strSQL + " where b.人员代码 is not null" + vbCr
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
        ' 获取strUserXM准备要退回的文件交接信息数据集
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：当前操作员名称
        '     strWhere             ：搜索条件
        '     objTuihuiDataSet     ：返回要退回的文件交接信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strTaskStatusWJSList As String = Me.FlowData.TaskStatusWJSList
                Dim strWJBS As String = Me.WJBS

                '创建数据集
                objTempTuihuiDataSet = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_VT_WENJIANTUIHUI)
                If strWJBS = "" Then Exit Try

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '执行检索
                With Me.m_objSqlDataAdapter
                    '获取发送给strUserXM的正常交接处理单
                    strSQL = ""
                    strSQL = strSQL + " select a.*" + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select " + vbCr
                    strSQL = strSQL + "     a.发送人," + vbCr
                    strSQL = strSQL + "     a.发送日期," + vbCr
                    strSQL = strSQL + "     交办事宜 = case when substring(a.交接标识,4,1)='1' then '" + Me.FlowData.TASK_THCL + "'" + vbCr
                    strSQL = strSQL + "                     when substring(a.交接标识,5,1)='1' then '" + Me.FlowData.TASK_SHCL + "'" + vbCr
                    strSQL = strSQL + "                     when substring(a.交接标识,7,1)='1' then '" + Me.FlowData.TASK_HFCL + "'" + vbCr
                    strSQL = strSQL + "                     else a.办理子类 end," + vbCr
                    strSQL = strSQL + "     a.接收日期," + vbCr
                    strSQL = strSQL + "     送来纸质文件份数 = a.发送纸质文件," + vbCr
                    strSQL = strSQL + "     送来电子文件份数 = a.发送电子文件," + vbCr
                    strSQL = strSQL + "     送来纸质附件份数 = a.发送纸质附件," + vbCr
                    strSQL = strSQL + "     送来电子附件份数 = a.发送电子附件," + vbCr
                    strSQL = strSQL + "     接收纸质文件份数 = a.发送纸质文件," + vbCr
                    strSQL = strSQL + "     接收电子文件份数 = a.发送电子文件," + vbCr
                    strSQL = strSQL + "     接收纸质附件份数 = a.发送纸质附件," + vbCr
                    strSQL = strSQL + "     接收电子附件份数 = a.发送电子附件," + vbCr
                    strSQL = strSQL + "     a.交接序号," + vbCr
                    strSQL = strSQL + "     a.发送序号," + vbCr
                    strSQL = strSQL + "     a.原交接号," + vbCr
                    strSQL = strSQL + "     a.交接标识," + vbCr
                    strSQL = strSQL + "     a.协办," + vbCr
                    strSQL = strSQL + "     发送人办理事宜 = b.办理子类," + vbCr
                    strSQL = strSQL + "     发送人协办     = b.协办" + vbCr
                    strSQL = strSQL + "   from" + vbCr
                    strSQL = strSQL + "   (" + vbCr
                    strSQL = strSQL + "     select * from 公文_B_交接 " + vbCr
                    strSQL = strSQL + "     where 文件标识 =  '" + strWJBS + "'" + vbCr                 '当前文件
                    strSQL = strSQL + "     and   接收人   =  '" + strUserXM + "'" + vbCr               'strUserXM接收
                    strSQL = strSQL + "     and   rtrim(交接标识) like '__1__0%'" + vbCr                       '接收人能看见+非通知
                    strSQL = strSQL + "   ) a" + vbCr
                    strSQL = strSQL + "   left join" + vbCr
                    strSQL = strSQL + "   (" + vbCr
                    strSQL = strSQL + "     select *" + vbCr
                    strSQL = strSQL + "     from 公文_B_交接" + vbCr
                    strSQL = strSQL + "     where 文件标识 = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + "   ) b on a.原交接号 = b.交接序号" + vbCr
                    strSQL = strSQL + " ) a" + vbCr
                    If strWhere <> "" Then
                        strSQL = strSQL + " where " + strWhere + vbCr
                    End If
                    strSQL = strSQL + " order by a.发送日期 desc" + vbCr

                    '设置参数
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    '执行操作
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
        ' 完成“我已阅读通知”的任务
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：用户名称
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim

                '获取文件信息
                Dim strTaskStatusYWCList As String = Me.FlowData.TaskStatusYWCList
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then
                    Exit Try
                End If

                '获取事务连接
                objSqlConnection = Me.SqlConnection

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '开始事务
                objSqlTransaction = objSqlConnection.BeginTransaction
                objSqlCommand.Transaction = objSqlTransaction

                '事务处理
                Try
                    '补接收信息
                    strSQL = ""
                    strSQL = strSQL + " update 公文_B_交接 set" + vbCr
                    strSQL = strSQL + "   接收日期 = '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "'," + vbCr
                    strSQL = strSQL + "   接收纸质文件 = 发送纸质文件," + vbCr
                    strSQL = strSQL + "   接收电子文件 = 发送电子文件," + vbCr
                    strSQL = strSQL + "   接收纸质附件 = 发送纸质附件," + vbCr
                    strSQL = strSQL + "   接收电子附件 = 发送电子附件 " + vbCr
                    strSQL = strSQL + "where 文件标识 = '" + strWJBS + "'" + vbCr                    '当前文件
                    strSQL = strSQL + "and   接收人   = '" + strUserXM + "'" + vbCr                  '接收人
                    strSQL = strSQL + "and   rtrim(交接标识) like '_____1%' " + vbCr                        '通知类
                    strSQL = strSQL + "and   办理状态 not in (" + strTaskStatusYWCList + ")" + vbCr  '未完成
                    strSQL = strSQL + "and   接收日期 is null" + vbCr                                '未接收
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.ExecuteNonQuery()

                    '设置为完成
                    strSQL = ""
                    strSQL = strSQL + " update 公文_B_交接 set" + vbCr
                    strSQL = strSQL + "   办理状态 = '" + Me.FlowData.TASKSTATUS_YYD + "'," + vbCr
                    strSQL = strSQL + "   完成日期 = '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "'" + vbCr
                    strSQL = strSQL + "where 文件标识 = '" + strWJBS + "'" + vbCr                    '当前文件
                    strSQL = strSQL + "and   接收人   = '" + strUserXM + "'" + vbCr                  '接收人
                    strSQL = strSQL + "and   rtrim(交接标识) like '_____1%' " + vbCr                        '通知类
                    strSQL = strSQL + "and   办理状态 not in (" + strTaskStatusYWCList + ")" + vbCr  '未完成
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '提交事务
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
        ' 完成“我不用处理”的任务
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：用户名称
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim

                '获取文件信息
                Dim strTaskStatusYWCList As String = Me.FlowData.TaskStatusYWCList
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then
                    Exit Try
                End If

                '获取事务连接
                objSqlConnection = Me.SqlConnection

                '是否有退回的或收回的事宜
                strSQL = ""
                strSQL = strSQL + " select * from 公文_B_交接" + vbCr
                strSQL = strSQL + " where 文件标识 = '" + strWJBS + "'" + vbCr                       '当前文件
                strSQL = strSQL + " and   接收人   = '" + strUserXM + "'" + vbCr                     '接收人
                strSQL = strSQL + " and  (rtrim(交接标识) like '___1%' or rtrim(交接标识) like '____1%')" + vbCr   '被收回或被退回
                strSQL = strSQL + " and   rtrim(交接标识) like '_____0%'" + vbCr                            '非通知类
                strSQL = strSQL + " and   办理状态 not in (" + strTaskStatusYWCList + ")" + vbCr     '未完成
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    strErrMsg = "错误：该文件没有退回给我办理、或没有被收回！"
                    GoTo errProc
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '检查是否为最后1个在办人员(非通知)
                strSQL = ""
                strSQL = strSQL + " select * from 公文_B_交接" + vbCr
                strSQL = strSQL + " where 文件标识 = '" + strWJBS + "'" + vbCr                     '当前文件
                strSQL = strSQL + " and   接收人   <> '" + strUserXM + "'" + vbCr                  '接收人非指定人
                strSQL = strSQL + " and   rtrim(交接标识) like '_____0%'" + vbCr                          '非通知
                strSQL = strSQL + " and   办理状态 not in (" + strTaskStatusYWCList + ")" + vbCr   '未完成
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    '目前没有其他人在办
                    Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                    objDataSet = Nothing
                    strSQL = ""
                    strSQL = strSQL + " select * from 公文_B_交接" + vbCr
                    strSQL = strSQL + " where 文件标识 = '" + strWJBS + "'" + vbCr                           '当前文件
                    strSQL = strSQL + " and   接收人   = '" + strUserXM + "'" + vbCr                         '接收人
                    strSQL = strSQL + " and   not (rtrim(交接标识) like '___1%' or rtrim(交接标识) like '____1%')" + vbCr  '不是退回的或收回的
                    strSQL = strSQL + " and   办理状态 not in (" + strTaskStatusYWCList + ")" + vbCr         '未办完
                    If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                        GoTo errProc
                    End If
                    If objDataSet.Tables(0).Rows.Count < 1 Then
                        strErrMsg = "错误：您是本文件的唯一的在办人，不能直接使用[不用处理]，请使用[发送]继续处理！"
                        GoTo errProc
                    End If
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '开始事务
                objSqlTransaction = objSqlConnection.BeginTransaction
                objSqlCommand.Transaction = objSqlTransaction

                '事务处理
                Try
                    strSQL = ""
                    strSQL = strSQL + " update 公文_B_交接 set" + vbCr
                    strSQL = strSQL + "   办理状态 = '" + Me.FlowData.TASKSTATUS_BYB + "'," + vbCr
                    strSQL = strSQL + "   完成日期 = '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "'" + vbCr
                    strSQL = strSQL + " where 文件标识 = '" + strWJBS + "'" + vbCr                       '当前文件
                    strSQL = strSQL + " and   接收人   = '" + strUserXM + "'" + vbCr                     '接收人
                    strSQL = strSQL + " and  (rtrim(交接标识) like '___1%' or rtrim(交接标识) like '____1%')" + vbCr   '被收回或被退回
                    strSQL = strSQL + " and   rtrim(交接标识) like '_____0%'" + vbCr                            '非通知类
                    strSQL = strSQL + " and   办理状态 not in (" + strTaskStatusYWCList + ")" + vbCr     '未完成
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '提交事务
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
        ' 完成“我处理完毕”的任务
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：用户名称
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim

                '获取文件信息
                Dim strTaskStatusYWCList As String = Me.FlowData.TaskStatusYWCList
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then
                    Exit Try
                End If

                '获取事务连接
                objSqlConnection = Me.SqlConnection

                '检查是否为最后1个在办人员(非通知)
                strSQL = ""
                strSQL = strSQL + " select * from 公文_B_交接" + vbCr
                strSQL = strSQL + " where 文件标识 = '" + strWJBS + "'" + vbCr                     '当前文件
                strSQL = strSQL + " and   接收人   <> '" + strUserXM + "'" + vbCr                  '接收人非指定人
                strSQL = strSQL + " and   rtrim(交接标识) like '_____0%'" + vbCr                          '非通知
                strSQL = strSQL + " and   办理状态 not in (" + strTaskStatusYWCList + ")" + vbCr   '未完成
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    '目前没有其他人在办
                    strErrMsg = "错误：本文件只有您一人正在处理，您不能直接设置为[处理完毕]，必须交发送给他人继续处理或发送给专人进行办结处理！"
                    GoTo errProc
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '开始事务
                objSqlTransaction = objSqlConnection.BeginTransaction
                objSqlCommand.Transaction = objSqlTransaction

                '事务处理
                Try
                    strSQL = ""
                    strSQL = strSQL + " update 公文_B_交接 set" + vbCr
                    strSQL = strSQL + "   办理状态 = '" + Me.FlowData.TASKSTATUS_YWC + "'," + vbCr
                    strSQL = strSQL + "   完成日期 = '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "'" + vbCr
                    strSQL = strSQL + " where 文件标识 = '" + strWJBS + "'" + vbCr                       '当前文件
                    strSQL = strSQL + " and   接收人   = '" + strUserXM + "'" + vbCr                     '接收人
                    strSQL = strSQL + " and   rtrim(交接标识) like '_____0%'" + vbCr                            '非通知类
                    strSQL = strSQL + " and   办理状态 not in (" + strTaskStatusYWCList + ")" + vbCr     '未完成
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '提交事务
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
        ' 获取除strUserXM外正常事宜没有办理完毕的人员列表
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：用户名称
        '     strUserList          ：(返回)没有办理完毕的人员列表
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim

                '获取文件信息
                Dim strTaskStatusYWCList As String = Me.FlowData.TaskStatusYWCList
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then
                    Exit Try
                End If

                '获取事务连接
                objSqlConnection = Me.SqlConnection

                '检查没有完成的人员（除自己外）
                strSQL = ""
                strSQL = strSQL + " select * from 公文_B_交接" + vbCr
                strSQL = strSQL + " where 文件标识 =  '" + strWJBS + "'" + vbCr                    '当前文件
                strSQL = strSQL + " and   接收人   <> '" + strUserXM + "'" + vbCr                  '接收人不是指定人
                strSQL = strSQL + " and   rtrim(交接标识) like '__1__0%'" + vbCr                          '接收人能看见+非通知
                strSQL = strSQL + " and   办理状态 not in (" + strTaskStatusYWCList + ")" + vbCr   '未办完
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If

                '返回
                Dim intCount As Integer
                Dim strValue As String
                Dim i As Integer
                With objDataSet.Tables(0)
                    '没有未办完的其他人员
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
        ' 获取strUserXM的可以催办哪些交接单?
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：人员名称
        '     objKeCuibanData      ：返回可以催办的交接单
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '交接办理完毕状态SQL列表

                '获取文件标识
                Dim strTaskStatusYWCList As String = Me.FlowData.TaskStatusYWCList
                objSqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '创建数据集
                objTempKeCuibanData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_CUIBAN_JIAOJIE)
                If strWJBS = "" Then Exit Try

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '执行检索
                With Me.m_objSqlDataAdapter
                    '获取可催办的交接信息
                    strSQL = ""
                    strSQL = strSQL + " select a.*" + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select" + vbCr
                    strSQL = strSQL + "     文件标识," + vbCr
                    strSQL = strSQL + "     交接序号," + vbCr
                    strSQL = strSQL + "     催办序号 = 0," + vbCr
                    strSQL = strSQL + "     催办人 = 发送人," + vbCr
                    strSQL = strSQL + "     催办日期 = getdate()," + vbCr
                    strSQL = strSQL + "     被催办人 = 接收人," + vbCr
                    strSQL = strSQL + "     催办说明 = '请尽快处理！'," + vbCr
                    strSQL = strSQL + "     办理子类," + vbCr
                    strSQL = strSQL + "     办理状态" + vbCr
                    strSQL = strSQL + "   from 公文_B_交接" + vbCr
                    strSQL = strSQL + "   where 文件标识 = '" + strWJBS + "' " + vbCr                      '当前文件
                    strSQL = strSQL + "   and   发送人   = '" + strUserXM + "' " + vbCr                    '我发送
                    strSQL = strSQL + "   and   rtrim(交接标识) like '1_1__0%' " + vbCr                           '已送过+非通知+接收人可见
                    strSQL = strSQL + "   and   办理状态 not in (" + strTaskStatusYWCList + ") " + vbCr    '未办完
                    strSQL = strSQL + " ) a" + vbCr
                    strSQL = strSQL + " order by a.催办序号" + vbCr

                    '设置参数
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    '执行操作
                    .Fill(objTempKeCuibanData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_CUIBAN_JIAOJIE))
                End With

                '设置“催办序号”
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
        ' 获取指定人员strUserXM的催办数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：人员名称
        '     objCuibanData        ：返回数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '创建数据集
                objTempCuibanData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_CUIBAN_JIAOJIE)
                If strWJBS = "" Then Exit Try

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '执行检索
                With Me.m_objSqlDataAdapter
                    '获取指定人员催办的情况
                    strSQL = ""
                    strSQL = strSQL + " select a.*" + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select a.*," + vbCr
                    strSQL = strSQL + "     b.办理子类, b.办理状态" + vbCr
                    strSQL = strSQL + "   from" + vbCr
                    strSQL = strSQL + "   (" + vbCr
                    strSQL = strSQL + "     select * from 公文_B_催办" + vbCr
                    strSQL = strSQL + "     where 文件标识 = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + "     and   催办人   = '" + strUserXM + "'" + vbCr
                    strSQL = strSQL + "   ) a " + vbCr
                    strSQL = strSQL + "   left join" + vbCr
                    strSQL = strSQL + "   (" + vbCr
                    strSQL = strSQL + "     select * from 公文_B_交接" + vbCr
                    strSQL = strSQL + "     where 文件标识 = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + "   ) b on a.文件标识 = b.文件标识 and a.交接序号 = b.交接序号 " + vbCr
                    strSQL = strSQL + " ) a" + vbCr
                    strSQL = strSQL + " order by a.催办序号 " + vbCr

                    '设置参数
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    '执行操作
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
        ' 获取文件的催办数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objCuibanData        ：返回数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '创建数据集
                objTempCuibanData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_CUIBAN_JIAOJIE)
                If strWJBS = "" Then Exit Try

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '执行检索
                With Me.m_objSqlDataAdapter
                    '获取催办的情况
                    strSQL = ""
                    strSQL = strSQL + " select a.*" + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select a.*," + vbCr
                    strSQL = strSQL + "     b.办理子类, b.办理状态" + vbCr
                    strSQL = strSQL + "   from" + vbCr
                    strSQL = strSQL + "   (" + vbCr
                    strSQL = strSQL + "     select * from 公文_B_催办" + vbCr
                    strSQL = strSQL + "     where 文件标识 = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + "   ) a " + vbCr
                    strSQL = strSQL + "   left join" + vbCr
                    strSQL = strSQL + "   (" + vbCr
                    strSQL = strSQL + "     select * from 公文_B_交接" + vbCr
                    strSQL = strSQL + "     where 文件标识 = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + "   ) b on a.文件标识 = b.文件标识 and a.交接序号 = b.交接序号 " + vbCr
                    strSQL = strSQL + " ) a" + vbCr
                    strSQL = strSQL + " order by a.催办日期 desc" + vbCr

                    '设置参数
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    '执行操作
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
        ' 判断催办数据数据是否有效？
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objOldData           ：记录旧值
        '     objNewData           ：记录新值(返回推荐值)
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.IsInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "错误：未传入要校验的数据！"
                    GoTo errProc
                End If

                '获取现有信息
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strSep As String = objPulicParameters.CharSeparate
                Dim strWJBS As String = Me.FlowData.WJBS

                '获取表结构定义
                strSQL = "select top 0 * from 公文_B_催办"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, objSqlConnection, strSQL, "公文_B_催办", objDataSet) = False Then
                    GoTo errProc
                End If

                '检查数据长度
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
                                '自动计算
                                strValue = objNewData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_CUIBAN_JJXH)
                                If objdacCommon.getNewCode(strErrMsg, objSqlConnection, "催办序号", "文件标识" + strSep + "交接序号", strWJBS + strSep + strValue, "公文_B_催办", True, strCBXH) = False Then
                                    GoTo errProc
                                End If
                                strValue = strCBXH
                            Else
                                If strValue = "" Then
                                    strErrMsg = "错误：[" + strField + "]不能为空！"
                                    GoTo errProc
                                End If
                                If objPulicParameters.isIntegerString(strValue) = False Then
                                    strErrMsg = "错误：[" + strField + "]必须是数字！"
                                    GoTo errProc
                                End If
                                intLen = CType(strValue, Integer)
                                If intLen < 1 Or intLen > 999999 Then
                                    strErrMsg = "错误：[" + strField + "]必须是[1,999999]！"
                                    GoTo errProc
                                End If
                                strValue = intLen.ToString()
                            End If

                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_CUIBAN_JJXH
                            If strValue = "" Then
                                strErrMsg = "错误：[" + strField + "]不能为空！"
                                GoTo errProc
                            End If
                            If objPulicParameters.isIntegerString(strValue) = False Then
                                strErrMsg = "错误：[" + strField + "]必须是数字！"
                                GoTo errProc
                            End If
                            intLen = CType(strValue, Integer)
                            If intLen < 1 Or intLen > 999999 Then
                                strErrMsg = "错误：[" + strField + "]必须是[1,999999]！"
                                GoTo errProc
                            End If
                            strValue = intLen.ToString()

                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_CUIBAN_CBR, _
                            Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_CUIBAN_BCBR
                            If strValue = "" Then
                                strErrMsg = "错误：[" + strField + "]不能为空！"
                                GoTo errProc
                            End If
                            With objDataSet.Tables(0).Columns(strField)
                                intLen = objPulicParameters.getStringLength(strValue)
                                If intLen > .MaxLength Then
                                    strErrMsg = "错误：[" + strField + "]长度不能超过[" + .MaxLength.ToString() + "]，实际有[" + intLen.ToString() + "]！"
                                    GoTo errProc
                                End If
                            End With

                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_CUIBAN_CBRQ
                            If strValue = "" Then strValue = Format(Now, "yyyy-MM-dd HH:mm:ss")
                            If objPulicParameters.isDatetimeString(strValue) = False Then
                                strErrMsg = "错误：[" + strField + "]必须是有效日期！"
                                GoTo errProc
                            End If

                        Case Else
                            If strValue <> "" Then
                                With objDataSet.Tables(0).Columns(strField)
                                    intLen = objPulicParameters.getStringLength(strValue)
                                    If intLen > .MaxLength Then
                                        strErrMsg = "错误：[" + strField + "]长度不能超过[" + .MaxLength.ToString() + "]，实际有[" + intLen.ToString() + "]！"
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
        ' 保存催办数据
        '     strErrMsg              ：如果错误，则返回错误信息
        '     objSqlTransaction      ：现有事务
        '     objNewData             ：记录新值(返回保存后的新值)
        '     objOldData             ：记录旧值
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
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

            '初始化
            doSaveCuiban = False
            strErrMsg = ""

            Try
                '检查
                If Me.IsInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "错误：未传入新的数据！"
                    GoTo errProc
                End If

                '获取现有信息
                strWJBS = Me.FlowData.WJBS
                If objSqlTransaction Is Nothing Then
                    objSqlConnection = Me.SqlConnection
                Else
                    objSqlConnection = objSqlTransaction.Connection
                End If

                '开始事务
                If objSqlTransaction Is Nothing Then
                    blnNewTrans = True
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Else
                    blnNewTrans = False
                End If

                '保存数据
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
                        '新增
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

                        strSQL = " insert into 公文_B_催办(" + vbCr + strFields + vbCr + ") values (" + vbCr + strValues + ")" + vbCr
                    Else
                        Dim intJJXH As Integer
                        Dim intCBXH As Integer
                        intJJXH = objPulicParameters.getObjectValue(objOldData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_CUIBAN_JJXH), 0)
                        intCBXH = objPulicParameters.getObjectValue(objOldData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_CUIBAN_CBXH), 0)

                        '更改
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
                        strSQL = strSQL + " update 公文_B_催办 set " + vbCr + strFields + vbCr
                        strSQL = strSQL + " where 文件标识 = @wjbs" + vbCr
                        strSQL = strSQL + " and   交接序号 = @jjxh" + vbCr
                        strSQL = strSQL + " and   催办序号 = @cbxh" + vbCr
                    End If
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '发送即时消息通知
                    Dim objNewDataJSXX As New System.Collections.Specialized.NameValueCollection
                    Dim strJSXX As String
                    Dim strFSR As String
                    Dim strJSR As String
                    strJSXX = objNewData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_CUIBAN_CBSM) + "(详细请查看您被催办的文件！)"
                    strFSR = objNewData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_CUIBAN_CBR)
                    strJSR = objNewData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_CUIBAN_BCBR)
                    objNewDataJSXX.Add(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_WYBS, "") '让系统生成
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

                '提交事务
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

            '返回
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
        ' 获取指定人员strUserXM的督办数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：人员名称
        '     objDubanData         ：返回数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '创建数据集
                objTempDubanData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_DUBAN_JIAOJIE)
                If strWJBS = "" Then Exit Try

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '执行检索
                With Me.m_objSqlDataAdapter
                    '获取指定人员督办的情况
                    strSQL = ""
                    strSQL = strSQL + " select a.*, b.办理子类, b.办理状态 from " + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select * from 公文_B_督办 " + vbCr
                    strSQL = strSQL + "   where 文件标识 = '" + strWJBS + "' " + vbCr
                    strSQL = strSQL + "   and   督办人   = '" + strUserXM + "' " + vbCr
                    strSQL = strSQL + " ) a " + vbCr
                    strSQL = strSQL + " left join " + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select * from 公文_B_交接 " + vbCr
                    strSQL = strSQL + "   where 文件标识 = '" + strWJBS + "' " + vbCr
                    strSQL = strSQL + " ) b on a.文件标识 = b.文件标识 and a.交接序号 = b.交接序号 " + vbCr
                    strSQL = strSQL + " order by a.督办序号 " + vbCr

                    '设置参数
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    '执行操作
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
        ' 获取文件的督办数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objDubanData         ：返回数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '创建数据集
                objTempDubanData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_DUBAN_JIAOJIE)
                If strWJBS = "" Then Exit Try

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '执行检索
                With Me.m_objSqlDataAdapter
                    '获取指定人员督办的情况
                    strSQL = ""
                    strSQL = strSQL + " select a.*, b.办理子类, b.办理状态 from " + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select * from 公文_B_督办 " + vbCr
                    strSQL = strSQL + "   where 文件标识 = '" + strWJBS + "' " + vbCr
                    strSQL = strSQL + " ) a " + vbCr
                    strSQL = strSQL + " left join " + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select * from 公文_B_交接 " + vbCr
                    strSQL = strSQL + "   where 文件标识 = '" + strWJBS + "' " + vbCr
                    strSQL = strSQL + " ) b on a.文件标识 = b.文件标识 and a.交接序号 = b.交接序号 " + vbCr
                    strSQL = strSQL + " order by a.督办序号 " + vbCr

                    '设置参数
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    '执行操作
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
        ' 获取strUserXM的可以督办哪些交接单?
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：人员名称
        '     objKeDubanData       ：返回可以督办的交接单
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '交接办理完毕状态SQL列表

                '获取文件标识
                Dim strTaskStatusYWCList As String = Me.FlowData.TaskStatusYWCList
                objSqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '创建数据集
                objTempKeDubanData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_DUBAN_JIAOJIE)
                If strWJBS = "" Then Exit Try

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '执行检索
                With Me.m_objSqlDataAdapter
                    '获取可督办的交接信息
                    strSQL = ""
                    strSQL = strSQL + " select a.*" + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select" + vbCr
                    strSQL = strSQL + "     文件标识," + vbCr
                    strSQL = strSQL + "     交接序号," + vbCr
                    strSQL = strSQL + "     督办序号 = 0," + vbCr
                    strSQL = strSQL + "     督办人 = '" + strUserXM + "'," + vbCr
                    strSQL = strSQL + "     督办日期 = getdate()," + vbCr
                    strSQL = strSQL + "     被督办人 = 接收人," + vbCr
                    strSQL = strSQL + "     督办要求 = '请尽快处理！'," + vbCr
                    strSQL = strSQL + "     督办结果 = ''," + vbCr
                    strSQL = strSQL + "     办理子类," + vbCr
                    strSQL = strSQL + "     办理状态" + vbCr
                    strSQL = strSQL + "   from 公文_B_交接" + vbCr
                    strSQL = strSQL + "   where 文件标识 = '" + strWJBS + "' " + vbCr                      '当前文件
                    strSQL = strSQL + "   and   rtrim(交接标识) like '1_1__0%' " + vbCr                           '已送过+非通知+接收人可见
                    strSQL = strSQL + "   and   办理状态 not in (" + strTaskStatusYWCList + ") " + vbCr    '未办完
                    strSQL = strSQL + " ) a" + vbCr
                    strSQL = strSQL + " order by a.督办序号" + vbCr

                    '设置参数
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    '执行操作
                    .Fill(objTempKeDubanData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_DUBAN_JIAOJIE))
                End With

                '计算部门信息
                Dim strUserId As String
                Dim strBmdm As String
                Dim strBmmc As String
                If objdacCustomer.getBmdmAndBmmcByRymc(strErrMsg, objSqlConnection, strUserXM, strBmdm, strBmmc) = False Then
                    GoTo errProc
                End If
                If objdacCustomer.getRydmByRymc(strErrMsg, objSqlConnection, strUserXM, strUserId) = False Then
                    GoTo errProc
                End If

                '删除不能督办的数据
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

                '设置“督办序号”
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
        ' 判断督办数据数据是否有效？
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objOldData           ：记录旧值
        '     objNewData           ：记录新值(返回推荐值)
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.IsInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "错误：未传入要校验的数据！"
                    GoTo errProc
                End If

                '获取现有信息
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strSep As String = objPulicParameters.CharSeparate
                Dim strWJBS As String = Me.FlowData.WJBS

                '获取表结构定义
                strSQL = "select top 0 * from 公文_B_督办"
                If objdacCommon.getDataSetWithSchemaBySQL(strErrMsg, objSqlConnection, strSQL, "公文_B_督办", objDataSet) = False Then
                    GoTo errProc
                End If

                '检查数据长度
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
                                '自动计算
                                strValue = objNewData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_DUBAN_JJXH)
                                If objdacCommon.getNewCode(strErrMsg, objSqlConnection, "督办序号", "文件标识" + strSep + "交接序号", strWJBS + strSep + strValue, "公文_B_督办", True, strDBXH) = False Then
                                    GoTo errProc
                                End If
                                strValue = strDBXH
                            Else
                                If strValue = "" Then
                                    strErrMsg = "错误：[" + strField + "]不能为空！"
                                    GoTo errProc
                                End If
                                If objPulicParameters.isIntegerString(strValue) = False Then
                                    strErrMsg = "错误：[" + strField + "]必须是数字！"
                                    GoTo errProc
                                End If
                                intLen = CType(strValue, Integer)
                                If intLen < 1 Or intLen > 999999 Then
                                    strErrMsg = "错误：[" + strField + "]必须是[1,999999]！"
                                    GoTo errProc
                                End If
                                strValue = intLen.ToString()
                            End If

                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_DUBAN_JJXH
                            If strValue = "" Then
                                strErrMsg = "错误：[" + strField + "]不能为空！"
                                GoTo errProc
                            End If
                            If objPulicParameters.isIntegerString(strValue) = False Then
                                strErrMsg = "错误：[" + strField + "]必须是数字！"
                                GoTo errProc
                            End If
                            intLen = CType(strValue, Integer)
                            If intLen < 1 Or intLen > 999999 Then
                                strErrMsg = "错误：[" + strField + "]必须是[1,999999]！"
                                GoTo errProc
                            End If
                            strValue = intLen.ToString()

                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_DUBAN_DBR, _
                            Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_DUBAN_BDBR
                            If strValue = "" Then
                                strErrMsg = "错误：[" + strField + "]不能为空！"
                                GoTo errProc
                            End If
                            With objDataSet.Tables(0).Columns(strField)
                                intLen = objPulicParameters.getStringLength(strValue)
                                If intLen > .MaxLength Then
                                    strErrMsg = "错误：[" + strField + "]长度不能超过[" + .MaxLength.ToString() + "]，实际有[" + intLen.ToString() + "]！"
                                    GoTo errProc
                                End If
                            End With

                        Case Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_DUBAN_DBRQ
                            If strValue = "" Then strValue = Format(Now, "yyyy-MM-dd HH:mm:ss")
                            If objPulicParameters.isDatetimeString(strValue) = False Then
                                strErrMsg = "错误：[" + strField + "]必须是有效日期！"
                                GoTo errProc
                            End If

                        Case Else
                            If strValue <> "" Then
                                With objDataSet.Tables(0).Columns(strField)
                                    intLen = objPulicParameters.getStringLength(strValue)
                                    If intLen > .MaxLength Then
                                        strErrMsg = "错误：[" + strField + "]长度不能超过[" + .MaxLength.ToString() + "]，实际有[" + intLen.ToString() + "]！"
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
        ' 保存督办数据
        '     strErrMsg              ：如果错误，则返回错误信息
        '     objSqlTransaction      ：现有事务
        '     objNewData             ：记录新值(返回保存后的新值)
        '     objOldData             ：记录旧值
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
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

            '初始化
            doSaveDuban = False
            strErrMsg = ""

            Try
                '检查
                If Me.IsInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "错误：未传入新的数据！"
                    GoTo errProc
                End If

                '获取现有信息
                strWJBS = Me.FlowData.WJBS
                If objSqlTransaction Is Nothing Then
                    objSqlConnection = Me.SqlConnection
                Else
                    objSqlConnection = objSqlTransaction.Connection
                End If

                '开始事务
                If objSqlTransaction Is Nothing Then
                    blnNewTrans = True
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Else
                    blnNewTrans = False
                End If

                '保存数据
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
                        '新增
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

                        strSQL = " insert into 公文_B_督办(" + vbCr + strFields + vbCr + ") values (" + vbCr + strValues + ")" + vbCr
                    Else
                        Dim intJJXH As Integer
                        Dim intDBXH As Integer
                        intJJXH = objPulicParameters.getObjectValue(objOldData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_DUBAN_JJXH), 0)
                        intDBXH = objPulicParameters.getObjectValue(objOldData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_DUBAN_DBXH), 0)

                        '更改
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
                        strSQL = strSQL + " update 公文_B_督办 set " + vbCr + strFields + vbCr
                        strSQL = strSQL + " where 文件标识 = @wjbs" + vbCr
                        strSQL = strSQL + " and   交接序号 = @jjxh" + vbCr
                        strSQL = strSQL + " and   督办序号 = @dbxh" + vbCr
                    End If
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '发送即时消息通知
                    Dim objNewDataJSXX As New System.Collections.Specialized.NameValueCollection
                    Dim strJSXX As String
                    Dim strFSR As String
                    Dim strJSR As String
                    strJSXX = objNewData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_DUBAN_DBYQ) + "(详细请查看您被督办的文件！)"
                    strFSR = objNewData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_DUBAN_DBR)
                    strJSR = objNewData(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_DUBAN_BDBR)
                    objNewDataJSXX.Add(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_WYBS, "") '让系统生成
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

                '提交事务
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

            '返回
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
        ' 获取指定人员strUserXM的被督办数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：人员名称
        '     objBeidubanData      ：返回数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '创建数据集
                objTempBeidubanData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_DUBAN_JIAOJIE)
                If strWJBS = "" Then Exit Try

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '执行检索
                With Me.m_objSqlDataAdapter
                    '获取指定人员被督办的情况
                    strSQL = ""
                    strSQL = strSQL + " select a.*, b.办理子类, b.办理状态 from " + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select * from 公文_B_督办 " + vbCr
                    strSQL = strSQL + "   where 文件标识 = '" + strWJBS + "' " + vbCr
                    strSQL = strSQL + "   and   被督办人 = '" + strUserXM + "' " + vbCr
                    strSQL = strSQL + " ) a " + vbCr
                    strSQL = strSQL + " left join " + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select * from 公文_B_交接 " + vbCr
                    strSQL = strSQL + "   where 文件标识 = '" + strWJBS + "' " + vbCr
                    strSQL = strSQL + " ) b on a.文件标识 = b.文件标识 and a.交接序号 = b.交接序号 " + vbCr
                    strSQL = strSQL + " order by a.督办序号 " + vbCr

                    '设置参数
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    '执行操作
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
        ' 保存督办结果数据
        '     strErrMsg              ：如果错误，则返回错误信息
        '     objSqlTransaction      ：现有事务
        '     intJJXH                ：交接序号
        '     intDBXH                ：督办序号
        '     strDBJG                ：督办结果
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
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

            '初始化
            doSaveDuban = False
            strErrMsg = ""

            Try
                '检查
                If Me.IsInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strDBJG Is Nothing Then strDBJG = ""
                strDBJG = strDBJG.Trim

                '获取现有信息
                strWJBS = Me.FlowData.WJBS
                If objSqlTransaction Is Nothing Then
                    objSqlConnection = Me.SqlConnection
                Else
                    objSqlConnection = objSqlTransaction.Connection
                End If

                '开始事务
                If objSqlTransaction Is Nothing Then
                    blnNewTrans = True
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Else
                    blnNewTrans = False
                End If

                '保存数据
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
                    strSQL = strSQL + " update 公文_B_督办 set " + vbCr
                    strSQL = strSQL + "   督办结果 = @dbjg" + vbCr
                    strSQL = strSQL + " where 文件标识 = @wjbs" + vbCr
                    strSQL = strSQL + " and   交接序号 = @jjxh" + vbCr
                    strSQL = strSQL + " and   督办序号 = @dbxh" + vbCr
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo rollDatabase
                End Try

                '提交事务
                If blnNewTrans = True Then
                    objSqlTransaction.Commit()
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            '返回
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
        ' 根据strWJBS获取交接单(翻译事宜+检查查看限制)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：当前查看人
        '     strWhere             ：搜索条件(a.)
        '     objJiaoJieData       ：返回交接数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '创建数据集
                objTempJiaoJieData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_JIAOJIE)
                If strWJBS = "" Then
                    Exit Try
                End If

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '执行检索
                With Me.SqlDataAdapter
                    '计算交接信息
                    strSQL = ""
                    strSQL = strSQL + " select a.*" + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select" + vbCr
                    strSQL = strSQL + "     文件标识,交接序号,原交接号,发送序号,发送人,发送日期," + vbCr
                    strSQL = strSQL + "     发送纸质文件,发送电子文件,发送纸质附件,发送电子附件," + vbCr
                    strSQL = strSQL + "     接收序号,接收人,协办,接收日期," + vbCr
                    strSQL = strSQL + "     接收纸质文件,接收电子文件,接收纸质附件,接收电子附件, " + vbCr
                    strSQL = strSQL + "     办理最后期限,完成日期,办理类型,办理状态,交接标识,委托人," + vbCr
                    strSQL = strSQL + "     办理子类 = case " + vbCr
                    strSQL = strSQL + "       when substring(交接标识,4,1)='1' then '" + Me.FlowData.TASK_THCL + "'" + vbCr
                    strSQL = strSQL + "       when substring(交接标识,5,1)='1' then '" + Me.FlowData.TASK_SHCL + "'" + vbCr
                    strSQL = strSQL + "       when substring(交接标识,7,1)='1' then '" + Me.FlowData.TASK_HFCL + "'" + vbCr
                    strSQL = strSQL + "       else 办理子类 end" + vbCr
                    strSQL = strSQL + "   from 公文_B_交接" + vbCr
                    strSQL = strSQL + "   where 文件标识 = '" + strWJBS + "'" + vbCr       '当前文件
                    strSQL = strSQL + "   and   rtrim(交接标识) like '1____0%'" + vbCr            '非通知类
                    strSQL = strSQL + " ) a" + vbCr
                    If strWhere <> "" Then
                        strSQL = strSQL + " where " + strWhere + vbCr
                    End If
                    strSQL = strSQL + " order by 交接序号" + vbCr

                    '设置参数
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    '执行操作
                    .Fill(objTempJiaoJieData.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_JIAOJIE))
                End With

                '获取用户ID和单位代码
                Dim strBmdm As String = ""
                Dim strBmmc As String = ""
                If objdacCustomer.getBmdmAndBmmcByRymc(strErrMsg, objSqlConnection, strUserXM, strBmdm, strBmmc) = False Then
                    GoTo errProc
                End If

                '检查是否显示真名
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
        ' 获取文件的操作日志数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strWhere             ：搜索条件(a.)
        '     objCaozuorizhiData   ：返回数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim()

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '创建数据集
                objTempCaozuorizhiData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_CAOZUORIZHI)
                If strWJBS = "" Then Exit Try

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '执行检索
                With Me.m_objSqlDataAdapter
                    '获取操作日志
                    strSQL = ""
                    strSQL = strSQL + " select a.*" + vbCr
                    strSQL = strSQL + " from 公文_B_操作日志 a" + vbCr
                    strSQL = strSQL + " where a.文件标识 = '" + strWJBS + "'" + vbCr
                    If strWhere <> "" Then
                        strSQL = strSQL + " and " + strWhere + vbCr
                    End If
                    strSQL = strSQL + " order by a.操作序号"

                    '设置参数
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    '执行操作
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
        ' 获取文件的补阅数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strCksyList          ：要查看特定补阅事宜(特殊原交接号列表)
        '     strWhere             ：搜索条件(a.)
        '     objBuyueData         ：返回数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim()
                If strCksyList Is Nothing Then strCksyList = ""
                strCksyList = strCksyList.Trim

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '创建数据集
                objTempBuyueData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_VT_WENJIANBUYUE)
                If strWJBS = "" Then Exit Try

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '执行检索
                With Me.m_objSqlDataAdapter
                    '打开所有补阅的数据
                    strSQL = ""
                    strSQL = strSQL + " select a.*" + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select a.*," + vbCr
                    strSQL = strSQL + "     办理情况 = case " + vbCr
                    strSQL = strSQL + "       when isnull(b.是否批准,' ') = '1100' then '批准'" + vbCr
                    strSQL = strSQL + "       when isnull(b.是否批准,' ') = '1110' then '转送'" + vbCr
                    strSQL = strSQL + "       when isnull(b.是否批准,' ') = '1000' then '拒绝'" + vbCr
                    strSQL = strSQL + "       else '    ' end" + vbCr
                    strSQL = strSQL + "   from" + vbCr
                    strSQL = strSQL + "   (" + vbCr
                    strSQL = strSQL + "     select a.*" + vbCr
                    strSQL = strSQL + "     from 公文_B_交接 a" + vbCr
                    strSQL = strSQL + "     where a.文件标识 = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + "     and   a.办理子类 = '" + Me.FlowData.TASK_BYQQ + "'" + vbCr
                    strSQL = strSQL + "     union" + vbCr
                    strSQL = strSQL + "     select a.*" + vbCr
                    strSQL = strSQL + "     from 公文_B_交接 a" + vbCr
                    strSQL = strSQL + "     where a.文件标识 = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + "     and   a.办理子类 = '" + Me.FlowData.TASK_BYTZ + "'" + vbCr
                    If Trim(strCksyList) = "" Then
                        strSQL = strSQL + "     and a.原交接号 in (" + Me.FlowData.TaskStatusZDTZList + ")" + vbCr
                    Else
                        strSQL = strSQL + "     and a.原交接号 in (" + strCksyList + ")" + vbCr
                    End If
                    strSQL = strSQL + "   ) a"
                    strSQL = strSQL + "   left join" + vbCr
                    strSQL = strSQL + "   (" + vbCr
                    strSQL = strSQL + "     select a.*" + vbCr
                    strSQL = strSQL + "     from 公文_B_办理 a" + vbCr
                    strSQL = strSQL + "     where a.文件标识 = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + "     and   a.办理子类 = '" + Me.FlowData.TASK_BYQQ + "'" + vbCr
                    strSQL = strSQL + "  ) b on a.文件标识=b.文件标识 and a.交接序号=b.交接序号" + vbCr
                    strSQL = strSQL + " ) a" + vbCr
                    If strWhere <> "" Then
                        strSQL = strSQL + " where " + strWhere + vbCr
                    End If
                    strSQL = strSQL + " order by a.交接序号" + vbCr

                    '设置参数
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    '执行操作
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
        ' 获取指定人员发送的补阅数据(补阅请求与补阅通知)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：人员名称
        '     strWhere             ：搜索条件(a.)
        '     objBuyueData         ：返回数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim()
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '创建数据集
                objTempBuyueData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_VT_WENJIANBUYUE)
                If strWJBS = "" Then Exit Try

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '执行检索
                Dim intZDBY As Integer = Xydc.Platform.Common.Data.FlowData.YJJH_ZHUDONGBUYUE
                With Me.m_objSqlDataAdapter
                    '打开我发送的数据
                    strSQL = ""
                    strSQL = strSQL + " select a.*" + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select a.*," + vbCr
                    strSQL = strSQL + "     办理情况 = case " + vbCr
                    strSQL = strSQL + "       when isnull(b.是否批准,' ') = '1100' then '批准'" + vbCr
                    strSQL = strSQL + "       when isnull(b.是否批准,' ') = '1110' then '转送'" + vbCr
                    strSQL = strSQL + "       when isnull(b.是否批准,' ') = '1000' then '拒绝'" + vbCr
                    strSQL = strSQL + "       else '    ' end" + vbCr
                    strSQL = strSQL + "   from" + vbCr
                    strSQL = strSQL + "   (" + vbCr
                    strSQL = strSQL + "     select a.*" + vbCr
                    strSQL = strSQL + "     from 公文_B_交接 a" + vbCr
                    strSQL = strSQL + "     where a.文件标识 = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + "     and  (a.发送人   = '" + strUserXM + "' or a.委托人 = '" + strUserXM + "')" + vbCr
                    strSQL = strSQL + "     and   a.办理子类 = '" + Me.FlowData.TASK_BYQQ + "'" + vbCr
                    strSQL = strSQL + "     union" + vbCr
                    strSQL = strSQL + "     select a.*" + vbCr
                    strSQL = strSQL + "     from 公文_B_交接 a" + vbCr
                    strSQL = strSQL + "     where a.文件标识 = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + "     and   a.发送人   = '" + strUserXM + "'" + vbCr
                    strSQL = strSQL + "     and   a.办理子类 = '" + Me.FlowData.TASK_BYTZ + "'" + vbCr
                    strSQL = strSQL + "     and   a.原交接号 =  " + intZDBY.ToString + vbCr
                    strSQL = strSQL + "   ) a " + vbCr
                    strSQL = strSQL + "   left join" + vbCr
                    strSQL = strSQL + "   (" + vbCr
                    strSQL = strSQL + "     select a.*" + vbCr
                    strSQL = strSQL + "     from 公文_B_办理 a" + vbCr
                    strSQL = strSQL + "     where a.文件标识 = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + "     and   a.办理子类 = '" + Me.FlowData.TASK_BYQQ + "'" + vbCr
                    strSQL = strSQL + "   ) b on a.文件标识=b.文件标识 and a.交接序号=b.交接序号" + vbCr
                    strSQL = strSQL + " ) a" + vbCr
                    If strWhere <> "" Then
                        strSQL = strSQL + " where " + strWhere + vbCr
                    End If
                    strSQL = strSQL + " order by a.发送日期 desc" + vbCr

                    '设置参数
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    '执行操作
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
        ' 获取指定人员接收的补阅数据(补阅请求与补阅通知)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：人员名称
        '     strWhere             ：搜索条件(a.)
        '     objBuyueData         ：返回数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strWhere Is Nothing Then strWhere = ""
                strWhere = strWhere.Trim()
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '创建数据集
                objTempBuyueData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_VT_WENJIANBUYUE)
                If strWJBS = "" Then Exit Try

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '执行检索
                Dim intZDBY As Integer = Xydc.Platform.Common.Data.FlowData.YJJH_ZHUDONGBUYUE
                With Me.m_objSqlDataAdapter
                    strSQL = ""
                    strSQL = strSQL + " select a.*" + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select a.*," + vbCr
                    strSQL = strSQL + "     办理情况 = case " + vbCr
                    strSQL = strSQL + "       when isnull(b.是否批准,' ') = '1100' then '批准'" + vbCr
                    strSQL = strSQL + "       when isnull(b.是否批准,' ') = '1110' then '转送'" + vbCr
                    strSQL = strSQL + "       when isnull(b.是否批准,' ') = '1000' then '拒绝'" + vbCr
                    strSQL = strSQL + "       else '    ' end" + vbCr
                    strSQL = strSQL + "   from" + vbCr
                    strSQL = strSQL + "   (" + vbCr
                    strSQL = strSQL + "     select a.*" + vbCr
                    strSQL = strSQL + "     from 公文_B_交接 a" + vbCr
                    strSQL = strSQL + "     where a.文件标识 = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + "     and   a.接收人   = '" + strUserXM + "'" + vbCr
                    strSQL = strSQL + "     and   a.办理子类 = '" + Me.FlowData.TASK_BYQQ + "'" + vbCr
                    strSQL = strSQL + "     union" + vbCr
                    strSQL = strSQL + "     select a.*" + vbCr
                    strSQL = strSQL + "     from 公文_B_交接 a" + vbCr
                    strSQL = strSQL + "     where a.文件标识 = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + "     and   a.接收人   = '" + strUserXM + "'" + vbCr
                    strSQL = strSQL + "     and   a.办理子类 = '" + Me.FlowData.TASK_BYTZ + "'" + vbCr
                    strSQL = strSQL + "     and   a.原交接号 =  " + intZDBY.ToString + vbCr
                    strSQL = strSQL + "   ) a" + vbCr
                    strSQL = strSQL + "   left join" + vbCr
                    strSQL = strSQL + "   (" + vbCr
                    strSQL = strSQL + "     select a.*" + vbCr
                    strSQL = strSQL + "     from 公文_B_办理 a" + vbCr
                    strSQL = strSQL + "     where a.文件标识 = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + "     and   a.办理子类 = '" + Me.FlowData.TASK_BYQQ + "'" + vbCr
                    strSQL = strSQL + "   ) b on a.文件标识=b.文件标识 and a.交接序号=b.交接序号" + vbCr
                    strSQL = strSQL + " ) a" + vbCr
                    If strWhere <> "" Then
                        strSQL = strSQL + " where " + strWhere + vbCr
                    End If
                    strSQL = strSQL + " order by a.发送日期 desc" + vbCr

                    '设置参数
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    '执行操作
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
        ' strSender向strReceiver发送补阅请求
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objSqlTransaction    ：现有事务
        '     strFSXH              ：发送批次
        '     strSender            ：发送人员名称
        '     strReceiver          ：接收人员名称
        '     strJJSM              ：交接说明
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strSender Is Nothing Then strSender = ""
                strSender = strSender.Trim()
                If strReceiver Is Nothing Then strReceiver = ""
                strReceiver = strReceiver.Trim()
                If strReceiver = "" Or strSender = "" Then
                    strErrMsg = "错误：未指定[发送人]或[接收人]！"
                    GoTo errProc
                End If
                If strFSXH Is Nothing Then strFSXH = ""
                strFSXH = strFSXH.Trim
                If strJJSM Is Nothing Then strJJSM = ""
                strJJSM = strJJSM.Trim

                '获取文件信息
                Dim strTaskStatusWJS As String = Me.FlowData.TASKSTATUS_WJS
                Dim strBYQQ As String = Me.FlowData.TASK_BYQQ                
                Dim strBLLX As String = Me.FlowBLLXName               
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '获取事务连接
                If objSqlTransaction Is Nothing Then
                    objSqlConnection = Me.SqlConnection
                Else
                    objSqlConnection = objSqlTransaction.Connection
                End If

                '创建查询连接
                objLocalSqlConnection = New System.Data.SqlClient.SqlConnection(objSqlConnection.ConnectionString)
                objLocalSqlConnection.Open()

                '获取新交接单号
                Dim strJJXH As String
                If objdacCommon.getNewCode(strErrMsg, objLocalSqlConnection, "交接序号", "文件标识", strWJBS, "公文_B_交接", True, strJJXH) = False Then
                    GoTo errProc
                End If
                '计算接收序号
                Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate
                Dim strRelaFields As String = "文件标识" + strSep + "发送序号"
                Dim strRelaValue As String = strWJBS + strSep + strFSXH
                Dim strJSXH As String
                If objdacCommon.getNewCode(strErrMsg, objLocalSqlConnection, "接收序号", strRelaFields, strRelaValue, "公文_B_交接", True, strJSXH) = False Then
                    GoTo errProc
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objLocalSqlConnection)

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '开始事务
                If objSqlTransaction Is Nothing Then
                    blnNewTrans = True
                    objSqlTransaction = objSqlConnection.BeginTransaction
                Else
                    blnNewTrans = False
                End If
                objSqlCommand.Transaction = objSqlTransaction

                '事务处理
                Dim intBYTZ As Integer = Xydc.Platform.Common.Data.FlowData.YJJH_YIBANTONGZHI
                Try
                    '提交新的补阅交接单(主动补阅)
                    strSQL = ""
                    strSQL = strSQL + " insert into 公文_B_交接 (" + vbCr
                    strSQL = strSQL + "   文件标识," + vbCr
                    strSQL = strSQL + "   交接序号," + vbCr
                    strSQL = strSQL + "   原交接号," + vbCr
                    strSQL = strSQL + "   发送序号," + vbCr
                    strSQL = strSQL + "   发送人," + vbCr
                    strSQL = strSQL + "   发送日期," + vbCr
                    strSQL = strSQL + "   接收序号," + vbCr
                    strSQL = strSQL + "   接收人," + vbCr
                    strSQL = strSQL + "   接收日期," + vbCr
                    strSQL = strSQL + "   办理最后期限," + vbCr
                    strSQL = strSQL + "   完成日期," + vbCr
                    strSQL = strSQL + "   办理类型," + vbCr
                    strSQL = strSQL + "   办理子类," + vbCr
                    strSQL = strSQL + "   办理状态," + vbCr
                    strSQL = strSQL + "   交接标识," + vbCr
                    strSQL = strSQL + "   委托人  ," + vbCr
                    strSQL = strSQL + "   交接说明 " + vbCr
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

                    '执行
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

                '提交事务
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
        ' strSender向strReceiver发送补阅通知
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objSqlTransaction    ：现有事务
        '     strFSXH              ：发送批次
        '     strSender            ：发送人员名称
        '     strReceiver          ：接收人员名称
        '     strJJSM              ：交接说明
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strSender Is Nothing Then strSender = ""
                strSender = strSender.Trim()
                If strReceiver Is Nothing Then strReceiver = ""
                strReceiver = strReceiver.Trim()
                If strReceiver = "" Or strSender = "" Then
                    strErrMsg = "错误：未指定[发送人]或[接收人]！"
                    GoTo errProc
                End If
                If strFSXH Is Nothing Then strFSXH = ""
                strFSXH = strFSXH.Trim
                If strJJSM Is Nothing Then strJJSM = ""
                strJJSM = strJJSM.Trim

                '获取文件信息
                Dim strTaskStatusWJS As String = Me.FlowData.TASKSTATUS_WJS
                Dim strBYTZ As String = Me.FlowData.TASK_BYTZ               
                Dim strBLLX As String = Me.FlowBLLXName               
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '获取事务连接
                If objSqlTransaction Is Nothing Then
                    objSqlConnection = Me.SqlConnection
                Else
                    objSqlConnection = objSqlTransaction.Connection
                End If

                '创建查询连接
                objLocalSqlConnection = New System.Data.SqlClient.SqlConnection(objSqlConnection.ConnectionString)
                objLocalSqlConnection.Open()

                '获取新交接单号
                Dim strJJXH As String
                If objdacCommon.getNewCode(strErrMsg, objLocalSqlConnection, "交接序号", "文件标识", strWJBS, "公文_B_交接", True, strJJXH) = False Then
                    GoTo errProc
                End If
                '计算接收序号
                Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate
                Dim strRelaFields As String = "文件标识" + strSep + "发送序号"
                Dim strRelaValue As String = strWJBS + strSep + strFSXH
                Dim strJSXH As String
                If objdacCommon.getNewCode(strErrMsg, objLocalSqlConnection, "接收序号", strRelaFields, strRelaValue, "公文_B_交接", True, strJSXH) = False Then
                    GoTo errProc
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objLocalSqlConnection)

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '开始事务
                If objSqlTransaction Is Nothing Then
                    blnNewTrans = True
                    objSqlTransaction = objSqlConnection.BeginTransaction
                Else
                    blnNewTrans = False
                End If
                objSqlCommand.Transaction = objSqlTransaction

                '事务处理
                Dim intZDTZ As Integer = Xydc.Platform.Common.Data.FlowData.YJJH_ZHUDONGBUYUE
                Try
                    '提交新的补阅交接单(主动补阅)
                    strSQL = ""
                    strSQL = strSQL + " insert into 公文_B_交接 (" + vbCr
                    strSQL = strSQL + "   文件标识," + vbCr
                    strSQL = strSQL + "   交接序号," + vbCr
                    strSQL = strSQL + "   原交接号," + vbCr
                    strSQL = strSQL + "   发送序号," + vbCr
                    strSQL = strSQL + "   发送人," + vbCr
                    strSQL = strSQL + "   发送日期," + vbCr
                    strSQL = strSQL + "   接收序号," + vbCr
                    strSQL = strSQL + "   接收人," + vbCr
                    strSQL = strSQL + "   接收日期," + vbCr
                    strSQL = strSQL + "   办理最后期限," + vbCr
                    strSQL = strSQL + "   完成日期," + vbCr
                    strSQL = strSQL + "   办理类型," + vbCr
                    strSQL = strSQL + "   办理子类," + vbCr
                    strSQL = strSQL + "   办理状态," + vbCr
                    strSQL = strSQL + "   交接标识," + vbCr
                    strSQL = strSQL + "   委托人  ," + vbCr
                    strSQL = strSQL + "   交接说明 " + vbCr
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

                    '执行
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

                '提交事务
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
        ' strSender向strReceiver发送补阅通知
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objSqlTransaction    ：现有事务(本功能不支持整体事务)
        '     strSender            ：发送人员名称
        '     strReceiver          ：接收人员列表
        '     strJJSM              ：交接说明
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strSender Is Nothing Then strSender = ""
                strSender = strSender.Trim()
                If strReceiverList Is Nothing Then strReceiverList = ""
                strReceiverList = strReceiverList.Trim()
                If strReceiverList = "" Or strSender = "" Then
                    strErrMsg = "错误：未指定[发送人]或[接收人]！"
                    GoTo errProc
                End If
                If strJJSM Is Nothing Then strJJSM = ""
                strJJSM = strJJSM.Trim

                '获取文件信息
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then
                    Exit Try
                End If

                '计算人员列表
                Dim strJSR() As String
                strJSR = strReceiverList.Split(Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate.ToCharArray)
                If strJSR.Length < 1 Then
                    Exit Try
                End If

                '获取事务连接
                If objSqlTransaction Is Nothing Then
                    objSqlConnection = Me.SqlConnection
                Else
                    objSqlConnection = objSqlTransaction.Connection
                End If

                '创建查询连接
                objLocalSqlConnection = New System.Data.SqlClient.SqlConnection(objSqlConnection.ConnectionString)
                objLocalSqlConnection.Open()

                '获取新发送序号
                Dim strFSXH As String
                If objdacCommon.getNewCode(strErrMsg, objLocalSqlConnection, "发送序号", "文件标识", strWJBS, "公文_B_交接", True, strFSXH) = False Then
                    GoTo errProc
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objLocalSqlConnection)

                '逐个发放通知
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
        ' 收回补阅请求
        '     strErrMsg              ：如果错误，则返回错误信息
        '     objSqlTransaction      ：现有事务
        '     intJJXH                ：交接序号
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
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

            '初始化
            doShouhuiBuyueRequest = False
            strErrMsg = ""

            Try
                '检查
                If Me.IsInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If


                '获取现有信息
                strWJBS = Me.FlowData.WJBS
                If objSqlTransaction Is Nothing Then
                    objSqlConnection = Me.SqlConnection
                Else
                    objSqlConnection = objSqlTransaction.Connection
                End If

                '开始事务
                If objSqlTransaction Is Nothing Then
                    blnNewTrans = True
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Else
                    blnNewTrans = False
                End If

                '保存数据
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
                    strSQL = strSQL + " update 公文_B_交接 set " + vbCr
                    strSQL = strSQL + "   完成日期 = @wcrq," + vbCr
                    strSQL = strSQL + "   办理状态 = @blzt " + vbCr
                    strSQL = strSQL + " where 文件标识 = @wjbs" + vbCr
                    strSQL = strSQL + " and   交接序号 = @jjxh" + vbCr

                    strSQL = strSQL + " and   交接标识 = @jjbs" + vbCr

                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo rollDatabase
                End Try

                '提交事务
                If blnNewTrans = True Then
                    objSqlTransaction.Commit()
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            '返回
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
        ' 收回补阅通知
        '     strErrMsg              ：如果错误，则返回错误信息
        '     objSqlTransaction      ：现有事务
        '     intJJXH                ：交接序号
        ' 返回
        '     True                   ：成功
        '     False                  ：失败

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

            '初始化
            doShouhuiBuyueTongzhi = False
            strErrMsg = ""

            Try
                '检查
                If Me.IsInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If


                '获取现有信息
                strWJBS = Me.FlowData.WJBS
                If objSqlTransaction Is Nothing Then
                    objSqlConnection = Me.SqlConnection
                Else
                    objSqlConnection = objSqlTransaction.Connection
                End If

                '开始事务
                If objSqlTransaction Is Nothing Then
                    blnNewTrans = True
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Else
                    blnNewTrans = False
                End If

                '保存数据
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
                    strSQL = strSQL + " update 公文_B_交接 set " + vbCr
                    strSQL = strSQL + "   完成日期 = @wcrq," + vbCr
                    strSQL = strSQL + "   办理状态 = @blzt " + vbCr

                    strSQL = strSQL + "   ,交接标识 = stuff(交接标识,3,1,'0') " + vbCr

                    strSQL = strSQL + " where 文件标识 = @wjbs" + vbCr
                    strSQL = strSQL + " and   交接序号 = @jjxh" + vbCr
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo rollDatabase
                End Try

                '提交事务
                If blnNewTrans = True Then
                    objSqlTransaction.Commit()
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            '返回
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
        ' 批准补阅请求
        '     strErrMsg              ：如果错误，则返回错误信息
        '     objSqlTransaction      ：现有事务
        '     intJJXH                ：交接序号
        '     strFSXH                ：发送批次
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
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

            '初始化
            doPizhunBuyueRequest = False
            strErrMsg = ""

            Try
                '检查
                If Me.IsInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strFSXH Is Nothing Then strFSXH = ""
                strFSXH = strFSXH.Trim
                If strFSXH = "" Then strFSXH = "0"

                '获取现有信息
                Dim strBLLX As String = Me.FlowBLLXName
                strWJBS = Me.FlowData.WJBS
                If objSqlTransaction Is Nothing Then
                    objSqlConnection = Me.SqlConnection
                Else
                    objSqlConnection = objSqlTransaction.Connection
                End If

                '创建查询连接
                objLocalSqlConnection = New System.Data.SqlClient.SqlConnection(objSqlConnection.ConnectionString)
                objLocalSqlConnection.Open()

                '计算intJJXH是否已有办理信息
                Dim blnHasBLXX As Boolean = False
                strSQL = ""
                strSQL = strSQL + " select *" + vbCr
                strSQL = strSQL + " from 公文_B_办理" + vbCr
                strSQL = strSQL + " where 文件标识 = '" + strWJBS + "'" + vbCr
                strSQL = strSQL + " and   交接序号 =  " + intJJXH.ToString + "" + vbCr
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

                '获取交接信息
                strSQL = ""
                strSQL = strSQL + " select *" + vbCr
                strSQL = strSQL + " from 公文_B_交接" + vbCr
                strSQL = strSQL + " where 文件标识 = '" + strWJBS + "'" + vbCr
                strSQL = strSQL + " and   交接序号 =  " + intJJXH.ToString + "" + vbCr
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
                    strErrMsg = "错误：指定的请求不存在！"
                    GoTo errProc
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '获取新交接单号
                Dim strJJXH As String
                If objdacCommon.getNewCode(strErrMsg, objLocalSqlConnection, "交接序号", "文件标识", strWJBS, "公文_B_交接", True, strJJXH) = False Then
                    GoTo errProc
                End If
                '计算接收序号
                Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate
                Dim strRelaFields As String = "文件标识" + strSep + "发送序号"
                Dim strRelaValue As String = strWJBS + strSep + strFSXH
                Dim strJSXH As String
                If objdacCommon.getNewCode(strErrMsg, objLocalSqlConnection, "接收序号", strRelaFields, strRelaValue, "公文_B_交接", True, strJSXH) = False Then
                    GoTo errProc
                End If

                '开始事务
                If objSqlTransaction Is Nothing Then
                    blnNewTrans = True
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Else
                    blnNewTrans = False
                End If

                '保存数据
                Dim intBYTZ As Integer = Xydc.Platform.Common.Data.FlowData.YJJH_YIBANTONGZHI
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '发送“批准通知”
                    strSQL = ""
                    strSQL = strSQL + " insert into 公文_B_交接 (" + vbCr
                    strSQL = strSQL + "   文件标识," + vbCr
                    strSQL = strSQL + "   交接序号," + vbCr
                    strSQL = strSQL + "   原交接号," + vbCr
                    strSQL = strSQL + "   发送序号," + vbCr
                    strSQL = strSQL + "   接收序号," + vbCr
                    strSQL = strSQL + "   办理类型," + vbCr
                    strSQL = strSQL + "   办理子类," + vbCr
                    strSQL = strSQL + "   办理状态," + vbCr
                    strSQL = strSQL + "   交接标识," + vbCr
                    strSQL = strSQL + "   发送人," + vbCr
                    strSQL = strSQL + "   发送日期," + vbCr
                    strSQL = strSQL + "   接收人," + vbCr
                    strSQL = strSQL + "   办理最后期限," + vbCr
                    strSQL = strSQL + "   委托人," + vbCr
                    strSQL = strSQL + "   交接说明" + vbCr
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
                    strSQL = strSQL + " '" + "您的补阅请求已被批准！" + "'" + vbCr
                    strSQL = strSQL + " )" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '补接收日期
                    strSQL = ""
                    strSQL = strSQL + " update 公文_B_交接 set" + vbCr
                    strSQL = strSQL + "   接收日期 = '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "'," + vbCr
                    strSQL = strSQL + "   接收纸质文件 = 发送纸质文件," + vbCr
                    strSQL = strSQL + "   接收电子文件 = 发送电子文件," + vbCr
                    strSQL = strSQL + "   接收纸质附件 = 发送纸质附件," + vbCr
                    strSQL = strSQL + "   接收电子附件 = 发送电子附件 " + vbCr
                    strSQL = strSQL + " where 文件标识 = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + " and   交接序号 =  " + intJJXH.ToString + vbCr
                    strSQL = strSQL + " and   接收日期 is null" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '设置事宜已经完成
                    strSQL = ""
                    strSQL = strSQL + " update 公文_B_交接 set" + vbCr
                    strSQL = strSQL + "   完成日期 = '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "'," + vbCr
                    strSQL = strSQL + "   办理状态 = '" + Me.FlowData.TASKSTATUS_YWC + "'" + vbCr
                    strSQL = strSQL + " where 文件标识 = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + " and   交接序号 =  " + intJJXH.ToString + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '“批准处理”
                    If blnHasBLXX = False Then
                        strSQL = ""
                        strSQL = strSQL + " insert into 公文_B_办理 ("
                        strSQL = strSQL + "   文件标识,"
                        strSQL = strSQL + "   交接序号,"
                        strSQL = strSQL + "   办理人,"
                        strSQL = strSQL + "   办理类型,"
                        strSQL = strSQL + "   办理子类,"
                        strSQL = strSQL + "   办理日期,"
                        strSQL = strSQL + "   是否批准 "
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
                        strSQL = strSQL + " update 公文_B_办理 set" + vbCr
                        strSQL = strSQL + "   办理日期 = '" + Format(Now, "yyyy-MM-dd") + "'," + vbCr
                        strSQL = strSQL + "   是否批准 = '" + "1100" + "'" + vbCr
                        strSQL = strSQL + " where 文件标识 = '" + strWJBS + "'" + vbCr
                        strSQL = strSQL + " and   交接序号 =  " + intJJXH.ToString + " " + vbCr
                    End If
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo rollDatabase
                End Try

                '提交事务
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

            '返回
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
        ' 拒绝补阅请求
        '     strErrMsg              ：如果错误，则返回错误信息
        '     objSqlTransaction      ：现有事务
        '     intJJXH                ：交接序号
        '     strFSXH                ：发送批次
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
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

            '初始化
            doJujueBuyueRequest = False
            strErrMsg = ""

            Try
                '检查
                If Me.IsInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strFSXH Is Nothing Then strFSXH = ""
                strFSXH = strFSXH.Trim
                If strFSXH = "" Then strFSXH = "0"

                '获取现有信息
                Dim strBLLX As String = Me.FlowBLLXName
                strWJBS = Me.FlowData.WJBS
                If objSqlTransaction Is Nothing Then
                    objSqlConnection = Me.SqlConnection
                Else
                    objSqlConnection = objSqlTransaction.Connection
                End If

                '创建查询连接
                objLocalSqlConnection = New System.Data.SqlClient.SqlConnection(objSqlConnection.ConnectionString)
                objLocalSqlConnection.Open()

                '计算intJJXH是否已有办理信息
                Dim blnHasBLXX As Boolean = False
                strSQL = ""
                strSQL = strSQL + " select *" + vbCr
                strSQL = strSQL + " from 公文_B_办理" + vbCr
                strSQL = strSQL + " where 文件标识 = '" + strWJBS + "'" + vbCr
                strSQL = strSQL + " and   交接序号 =  " + intJJXH.ToString + "" + vbCr
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

                '获取交接信息
                strSQL = ""
                strSQL = strSQL + " select *" + vbCr
                strSQL = strSQL + " from 公文_B_交接" + vbCr
                strSQL = strSQL + " where 文件标识 = '" + strWJBS + "'" + vbCr
                strSQL = strSQL + " and   交接序号 =  " + intJJXH.ToString + "" + vbCr
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
                    strErrMsg = "错误：指定的请求不存在！"
                    GoTo errProc
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '获取新交接单号
                Dim strJJXH As String
                If objdacCommon.getNewCode(strErrMsg, objLocalSqlConnection, "交接序号", "文件标识", strWJBS, "公文_B_交接", True, strJJXH) = False Then
                    GoTo errProc
                End If
                '计算接收序号
                Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate
                Dim strRelaFields As String = "文件标识" + strSep + "发送序号"
                Dim strRelaValue As String = strWJBS + strSep + strFSXH
                Dim strJSXH As String
                If objdacCommon.getNewCode(strErrMsg, objLocalSqlConnection, "接收序号", strRelaFields, strRelaValue, "公文_B_交接", True, strJSXH) = False Then
                    GoTo errProc
                End If

                '开始事务
                If objSqlTransaction Is Nothing Then
                    blnNewTrans = True
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Else
                    blnNewTrans = False
                End If

                '保存数据
                Dim intBYTZ As Integer = Xydc.Platform.Common.Data.FlowData.YJJH_YIBANTONGZHI
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '发送“拒绝通知”
                    strSQL = ""
                    strSQL = strSQL + " insert into 公文_B_交接 (" + vbCr
                    strSQL = strSQL + "   文件标识," + vbCr
                    strSQL = strSQL + "   交接序号," + vbCr
                    strSQL = strSQL + "   原交接号," + vbCr
                    strSQL = strSQL + "   发送序号," + vbCr
                    strSQL = strSQL + "   接收序号," + vbCr
                    strSQL = strSQL + "   办理类型," + vbCr
                    strSQL = strSQL + "   办理子类," + vbCr
                    strSQL = strSQL + "   办理状态," + vbCr
                    strSQL = strSQL + "   交接标识," + vbCr
                    strSQL = strSQL + "   发送人," + vbCr
                    strSQL = strSQL + "   发送日期," + vbCr
                    strSQL = strSQL + "   接收人," + vbCr
                    strSQL = strSQL + "   办理最后期限," + vbCr
                    strSQL = strSQL + "   委托人," + vbCr
                    strSQL = strSQL + "   交接说明" + vbCr
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
                    strSQL = strSQL + " '" + "您的补阅请求没有批准！" + "'" + vbCr
                    strSQL = strSQL + " )" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '补接收日期
                    strSQL = ""
                    strSQL = strSQL + " update 公文_B_交接 set" + vbCr
                    strSQL = strSQL + "   接收日期 = '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "'," + vbCr
                    strSQL = strSQL + "   接收纸质文件 = 发送纸质文件," + vbCr
                    strSQL = strSQL + "   接收电子文件 = 发送电子文件," + vbCr
                    strSQL = strSQL + "   接收纸质附件 = 发送纸质附件," + vbCr
                    strSQL = strSQL + "   接收电子附件 = 发送电子附件 " + vbCr
                    strSQL = strSQL + " where 文件标识 = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + " and   交接序号 =  " + intJJXH.ToString + vbCr
                    strSQL = strSQL + " and   接收日期 is null" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '设置事宜已经完成
                    strSQL = ""
                    strSQL = strSQL + " update 公文_B_交接 set" + vbCr
                    strSQL = strSQL + "   完成日期 = '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "'," + vbCr
                    strSQL = strSQL + "   办理状态 = '" + Me.FlowData.TASKSTATUS_YWC + "'" + vbCr
                    strSQL = strSQL + " where 文件标识 = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + " and   交接序号 =  " + intJJXH.ToString + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '“拒绝处理”
                    If blnHasBLXX = False Then
                        strSQL = ""
                        strSQL = strSQL + " insert into 公文_B_办理 ("
                        strSQL = strSQL + "   文件标识,"
                        strSQL = strSQL + "   交接序号,"
                        strSQL = strSQL + "   办理人,"
                        strSQL = strSQL + "   办理类型,"
                        strSQL = strSQL + "   办理子类,"
                        strSQL = strSQL + "   办理日期,"
                        strSQL = strSQL + "   是否批准 "
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
                        strSQL = strSQL + " update 公文_B_办理 set" + vbCr
                        strSQL = strSQL + "   办理日期 = '" + Format(Now, "yyyy-MM-dd") + "'," + vbCr
                        strSQL = strSQL + "   是否批准 = '" + "1000" + "'" + vbCr
                        strSQL = strSQL + " where 文件标识 = '" + strWJBS + "'" + vbCr
                        strSQL = strSQL + " and   交接序号 =  " + intJJXH.ToString + " " + vbCr
                    End If
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo rollDatabase
                End Try

                '提交事务
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

            '返回
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
        ' 转发补阅请求
        '     strErrMsg              ：如果错误，则返回错误信息
        '     objSqlTransaction      ：现有事务
        '     intJJXH                ：交接序号
        '     strFSXH                ：发送批次
        '     strZFJSR               ：转发请求的接收人列表
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
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

            '初始化
            doZhuanfaBuyueRequest = False
            strErrMsg = ""

            Try
                '检查
                If Me.IsInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strFSXH Is Nothing Then strFSXH = ""
                strFSXH = strFSXH.Trim
                If strFSXH = "" Then strFSXH = "0"
                If strZFJSR Is Nothing Then strZFJSR = ""
                strZFJSR = strZFJSR.Trim
                If strZFJSR = "" Then
                    strErrMsg = "错误：没有指定转发给谁！"
                    GoTo errProc
                End If
                Dim strArray() As String
                strArray = strZFJSR.Split(objPulicParameters.CharSeparate.ToCharArray)

                '获取现有信息
                Dim strBLLX As String = Me.FlowBLLXName
                strWJBS = Me.FlowData.WJBS
                If objSqlTransaction Is Nothing Then
                    objSqlConnection = Me.SqlConnection
                Else
                    objSqlConnection = objSqlTransaction.Connection
                End If

                '创建查询连接
                objLocalSqlConnection = New System.Data.SqlClient.SqlConnection(objSqlConnection.ConnectionString)
                objLocalSqlConnection.Open()

                '计算intJJXH是否已有办理信息
                Dim blnHasBLXX As Boolean = False
                strSQL = ""
                strSQL = strSQL + " select *" + vbCr
                strSQL = strSQL + " from 公文_B_办理" + vbCr
                strSQL = strSQL + " where 文件标识 = '" + strWJBS + "'" + vbCr
                strSQL = strSQL + " and   交接序号 =  " + intJJXH.ToString + "" + vbCr
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

                '获取交接信息
                strSQL = ""
                strSQL = strSQL + " select *" + vbCr
                strSQL = strSQL + " from 公文_B_交接" + vbCr
                strSQL = strSQL + " where 文件标识 = '" + strWJBS + "'" + vbCr
                strSQL = strSQL + " and   交接序号 =  " + intJJXH.ToString + "" + vbCr
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
                    strErrMsg = "错误：指定的请求不存在！"
                    GoTo errProc
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '转发请求
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
                    '获取新交接单号
                    If objdacCommon.getNewCode(strErrMsg, objLocalSqlConnection, "交接序号", "文件标识", strWJBS, "公文_B_交接", True, strJJXH) = False Then
                        GoTo errProc
                    End If
                    '计算接收序号
                    strRelaFields = "文件标识" + strSep + "发送序号"
                    strRelaValue = strWJBS + strSep + strFSXH
                    If objdacCommon.getNewCode(strErrMsg, objLocalSqlConnection, "接收序号", strRelaFields, strRelaValue, "公文_B_交接", True, strJSXH) = False Then
                        GoTo errProc
                    End If

                    '开始事务
                    objLocalSqlTransaction = objLocalSqlConnection.BeginTransaction()
                    objSqlCommand.Transaction = objLocalSqlTransaction

                    '保存数据
                    Try
                        '转发“补阅请求”
                        strSQL = ""
                        strSQL = strSQL + " insert into 公文_B_交接 (" + vbCr
                        strSQL = strSQL + "   文件标识," + vbCr
                        strSQL = strSQL + "   交接序号," + vbCr
                        strSQL = strSQL + "   原交接号," + vbCr
                        strSQL = strSQL + "   发送序号," + vbCr
                        strSQL = strSQL + "   接收序号," + vbCr
                        strSQL = strSQL + "   办理类型," + vbCr
                        strSQL = strSQL + "   办理子类," + vbCr
                        strSQL = strSQL + "   办理状态," + vbCr
                        strSQL = strSQL + "   交接标识," + vbCr
                        strSQL = strSQL + "   发送人," + vbCr
                        strSQL = strSQL + "   发送日期," + vbCr
                        strSQL = strSQL + "   接收人," + vbCr
                        strSQL = strSQL + "   办理最后期限," + vbCr
                        strSQL = strSQL + "   委托人," + vbCr
                        strSQL = strSQL + "   交接说明" + vbCr
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

                    '提交事务
                    objLocalSqlTransaction.Commit()
                Next
                If Not (objSqlCommand Is Nothing) Then
                    Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
                    objSqlCommand = Nothing
                End If

                '开始事务
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

                    '补接收日期
                    strSQL = ""
                    strSQL = strSQL + " update 公文_B_交接 set" + vbCr
                    strSQL = strSQL + "   接收日期 = '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "'," + vbCr
                    strSQL = strSQL + "   接收纸质文件 = 发送纸质文件," + vbCr
                    strSQL = strSQL + "   接收电子文件 = 发送电子文件," + vbCr
                    strSQL = strSQL + "   接收纸质附件 = 发送纸质附件," + vbCr
                    strSQL = strSQL + "   接收电子附件 = 发送电子附件 " + vbCr
                    strSQL = strSQL + " where 文件标识 = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + " and   交接序号 =  " + intJJXH.ToString + vbCr
                    strSQL = strSQL + " and   接收日期 is null" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '设置事宜已经完成
                    strSQL = ""
                    strSQL = strSQL + " update 公文_B_交接 set" + vbCr
                    strSQL = strSQL + "   完成日期 = '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "'," + vbCr
                    strSQL = strSQL + "   办理状态 = '" + Me.FlowData.TASKSTATUS_YWC + "'" + vbCr
                    strSQL = strSQL + " where 文件标识 = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + " and   交接序号 =  " + intJJXH.ToString + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '“转发处理”
                    If blnHasBLXX = False Then
                        strSQL = ""
                        strSQL = strSQL + " insert into 公文_B_办理 ("
                        strSQL = strSQL + "   文件标识,"
                        strSQL = strSQL + "   交接序号,"
                        strSQL = strSQL + "   办理人,"
                        strSQL = strSQL + "   办理类型,"
                        strSQL = strSQL + "   办理子类,"
                        strSQL = strSQL + "   办理日期,"
                        strSQL = strSQL + "   是否批准 "
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
                        strSQL = strSQL + " update 公文_B_办理 set" + vbCr
                        strSQL = strSQL + "   办理日期 = '" + Format(Now, "yyyy-MM-dd") + "'," + vbCr
                        strSQL = strSQL + "   是否批准 = '" + "1110" + "'" + vbCr
                        strSQL = strSQL + " where 文件标识 = '" + strWJBS + "'" + vbCr
                        strSQL = strSQL + " and   交接序号 =  " + intJJXH.ToString + " " + vbCr
                    End If
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo rollDatabase
                End Try

                '提交事务
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

            '返回
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
        ' 已经阅读指定补阅通知
        '     strErrMsg            ：如果错误，则返回错误信息
        '     intJJXH              ：交接序号
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If

                '获取文件信息
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then
                    Exit Try
                End If

                '获取事务连接
                objSqlConnection = Me.SqlConnection

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '开始事务
                objSqlTransaction = objSqlConnection.BeginTransaction
                objSqlCommand.Transaction = objSqlTransaction

                '事务处理
                Try
                    '补接收信息
                    strSQL = ""
                    strSQL = strSQL + " update 公文_B_交接 set" + vbCr
                    strSQL = strSQL + "   接收日期 = '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "'," + vbCr
                    strSQL = strSQL + "   接收纸质文件 = 发送纸质文件," + vbCr
                    strSQL = strSQL + "   接收电子文件 = 发送电子文件," + vbCr
                    strSQL = strSQL + "   接收纸质附件 = 发送纸质附件," + vbCr
                    strSQL = strSQL + "   接收电子附件 = 发送电子附件 " + vbCr
                    strSQL = strSQL + "where 文件标识 = '" + strWJBS + "'" + vbCr                    '当前文件
                    strSQL = strSQL + "and   交接序号 =  " + intJJXH.ToString + "" + vbCr            '指定交接
                    strSQL = strSQL + "and   接收日期 is null" + vbCr                                '未接收
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.ExecuteNonQuery()

                    '设置为完成
                    strSQL = ""
                    strSQL = strSQL + " update 公文_B_交接 set" + vbCr
                    strSQL = strSQL + "   办理状态 = '" + Me.FlowData.TASKSTATUS_YYD + "'," + vbCr
                    strSQL = strSQL + "   完成日期 = '" + Format(Now, "yyyy-MM-dd HH:mm:ss") + "'" + vbCr
                    strSQL = strSQL + "where 文件标识 = '" + strWJBS + "'" + vbCr                    '当前文件
                    strSQL = strSQL + "and   交接序号 =  " + intJJXH.ToString + "" + vbCr            '指定交接
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    objSqlTransaction.Rollback()
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '提交事务
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
        ' 获取当前文件所有能看见文件的人员代码的SQL语句
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：操作员名称
        '     strSQL               ：(返回)人员代码的SQL
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Overridable Function getAllJsrSql( _
            ByRef strErrMsg As String, _
            ByVal strUserXM As String, _
            ByRef strSQL As String) As Boolean

            getAllJsrSql = False
            strErrMsg = ""
            strSQL = ""

            Try
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim
                If strUserXM = "" Then
                    strErrMsg = "错误：没有指定当前操作人员！"
                    GoTo errProc
                End If

                '获取文件信息
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then
                    Exit Try
                End If

                '计算SQL
                strSQL = ""
                strSQL = strSQL + " select b.人员代码" + vbCr
                strSQL = strSQL + " from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select 接收人 as 人员名称" + vbCr
                strSQL = strSQL + "   from 公文_B_交接" + vbCr
                strSQL = strSQL + "   where 文件标识 =  '" + strWJBS + "'" + vbCr
                strSQL = strSQL + "   and   接收人   <> '" + strUserXM + "'" + vbCr
                strSQL = strSQL + "   and   rtrim(交接标识) like '__1%'" + vbCr
                strSQL = strSQL + "   group by 接收人" + vbCr
                strSQL = strSQL + "   union" + vbCr
                strSQL = strSQL + "   select 发送人 as 人员名称" + vbCr
                strSQL = strSQL + "   from 公文_B_交接" + vbCr
                strSQL = strSQL + "   where 文件标识 =  '" + strWJBS + "'" + vbCr
                strSQL = strSQL + "   and   发送人   <> '" + strUserXM + "'" + vbCr
                strSQL = strSQL + "   and   rtrim(交接标识) like '_1%'" + vbCr
                strSQL = strSQL + "   group by 发送人" + vbCr
                strSQL = strSQL + " ) a" + vbCr
                strSQL = strSQL + " left join 公共_B_人员 b on a.人员名称 = b.人员名称" + vbCr
                strSQL = strSQL + " group by b.人员代码" + vbCr

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
        ' 获取strUserXM能补登当前文件哪些领导的意见
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：补登人名称
        '     strList              ：(返回)人员名称列表
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim
                If strUserXM = "" Then
                    strErrMsg = "错误：没有指定补登人名称！"
                    GoTo errProc
                End If

                '获取文件信息
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then
                    Exit Try
                End If
                objSqlConnection = Me.SqlConnection

                '计算人员代码、单位代码
                Dim strBdrBmdm As String = ""
                Dim strBdrBmmc As String = ""
                Dim strBdrId As String = ""
                If objdacCustomer.getRydmByRymc(strErrMsg, objSqlConnection, strUserXM, strBdrId) = False Then
                    GoTo errProc
                End If
                If objdacCustomer.getBmdmAndBmmcByRymc(strErrMsg, objSqlConnection, strUserXM, strBdrBmdm, strBdrBmmc) = False Then
                    GoTo errProc
                End If

                '获取本文件中的所有审批人
                strSQL = ""
                strSQL = strSQL + " select 接收人" + vbCr
                strSQL = strSQL + " from 公文_B_交接" + vbCr
                strSQL = strSQL + " where 文件标识 = '" + strWJBS + "'" + vbCr                         '当前文件
                strSQL = strSQL + " and   办理子类 in (" + Me.FlowData.TaskBlzlSPSYList + ")" + vbCr   '审批事宜
                strSQL = strSQL + " and   rtrim(交接标识) like '1_1__0%'" + vbCr                              '已发送+接收人可见+非通知
                strSQL = strSQL + " group by 接收人" + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If

                '计算返回
                Dim intCount As Integer
                Dim blnDo As Boolean
                Dim strJSR As String
                Dim i As Integer
                With objDataSet.Tables(0)
                    intCount = .Rows.Count
                    For i = 0 To intCount - 1 Step 1
                        strJSR = objPulicParameters.getObjectValue(.Rows(i).Item("接收人"), "")
                        If strJSR <> "" Then
                            '是否可补登？
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
        ' 获取strUserXM最后1次的正常处理的交接单(审批事宜)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：用户名称
        '     blnZTXZ              ：=True：未办完，False：不限状态
        '     objJiaoJieData       ：返回最后1次交接数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '计算最近交接序号，准则：接收人可看本交接、发送、正常消息
                strSQL = ""
                strSQL = strSQL + " select isnull(max(交接序号),0) as 交接序号 " + vbCr
                strSQL = strSQL + " from 公文_B_交接" + vbCr
                strSQL = strSQL + " where 文件标识 =  '" + strWJBS + "' " + vbCr                              '当前文件
                strSQL = strSQL + " and   接收人   =  '" + strUserXM + "' " + vbCr                            '接收人
                strSQL = strSQL + " and   办理子类 in (" + Me.FlowData.TaskBlzlSPSYList + ")" + vbCr          '审批事宜
                If blnZTXZ = True Then
                    strSQL = strSQL + " and   办理状态 not in (" + Me.FlowData.TaskStatusYWCList + ")" + vbCr '未办完
                End If
                strSQL = strSQL + " and   rtrim(交接标识) like '1_1__0_%' " + vbCr                                   '已发送+接收人能看+非通知类
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                Dim intXH As Integer
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    intXH = 0
                Else
                    With objDataSet.Tables(0).Rows(0)
                        intXH = objPulicParameters.getObjectValue(.Item("交接序号"), 0)
                    End With
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                objDataSet = Nothing

                '创建数据集
                objTempJiaoJieData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_JIAOJIE)
                If strWJBS = "" Then Exit Try

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '执行检索
                With Me.SqlDataAdapter
                    '计算交接信息
                    strSQL = ""
                    strSQL = strSQL + " select * from 公文_B_交接 " + vbCr
                    strSQL = strSQL + " where 文件标识 = '" + strWJBS + "' " + vbCr
                    strSQL = strSQL + " and   接收人   = '" + strUserXM + "' " + vbCr
                    strSQL = strSQL + " and   交接序号 = " + intXH.ToString() + vbCr

                    '设置参数
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    '执行操作
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
        ' 保存审批意见数据
        '     strErrMsg              ：如果错误，则返回错误信息
        '     objSqlTransaction      ：现有事务
        '     intJJXH                ：交接序号
        '     objNewData             ：记录新值(返回保存后的新值)
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
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

            '初始化
            doSaveSpyj = False
            strErrMsg = ""

            Try
                '检查
                If Me.IsInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If objNewData Is Nothing Then
                    strErrMsg = "错误：未传入新的数据！"
                    GoTo errProc
                End If

                '获取现有信息
                strWJBS = Me.FlowData.WJBS
                If objSqlTransaction Is Nothing Then
                    objSqlConnection = Me.SqlConnection
                Else
                    objSqlConnection = objSqlTransaction.Connection
                End If

                '计算是否存在？
                strSQL = ""
                strSQL = strSQL + " select * from 公文_B_办理" + vbCr
                strSQL = strSQL + " where 文件标识 = '" + strWJBS + "'" + vbCr
                strSQL = strSQL + " and   交接序号 =  " + intJJXH.ToString + vbCr
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

                '开始事务
                If objSqlTransaction Is Nothing Then
                    blnNewTrans = True
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Else
                    blnNewTrans = False
                End If

                '保存数据
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
                        '新增
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

                        strSQL = " insert into 公文_B_办理 (" + vbCr + strFields + vbCr + ") values (" + vbCr + strValues + ")" + vbCr
                    Else
                        '更改
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
                        strSQL = strSQL + " update 公文_B_办理 set " + vbCr + strFields + vbCr
                        strSQL = strSQL + " where 文件标识 = @wjbs" + vbCr
                        strSQL = strSQL + " and   交接序号 = @jjxh" + vbCr
                    End If
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo rollDatabase
                End Try

                '提交事务
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

            '返回
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
        ' 取消intJJXH指定的办理意见
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objSqlTransaction    ：现有事务
        '     intJJXH              ：交接序号
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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

            '初始化
            doBanliCancel = False
            strErrMsg = ""

            Try
                '检查
                If Me.IsInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If

                '获取现有信息
                If objSqlTransaction Is Nothing Then
                    objSqlConnection = Me.SqlConnection
                Else
                    objSqlConnection = objSqlTransaction.Connection
                End If
                strWJBS = Me.FlowData.WJBS
                If strWJBS = "" Then
                    Exit Try
                End If

                '开始事务
                If objSqlTransaction Is Nothing Then
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                    blnNewTrans = True
                Else
                    blnNewTrans = False
                End If

                '取消操作
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    strSQL = ""
                    strSQL = strSQL + " delete from 公文_B_办理" + vbCr
                    strSQL = strSQL + " where 文件标识 = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + " and   交接序号 =  " + intJJXH.ToString + " " + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo rollDatabase
                End Try

                '提交事务
                If blnNewTrans = True Then
                    objSqlTransaction.Commit()
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            '返回
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
        ' 获取文件指定intJJXH的办理数据集
        '     strErrMsg            ：如果错误，则返回错误信息
        '     intJJXH              ：交接序号
        '     objBanliData         ：返回数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '创建数据集
                objTempBanliData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_B_BANLI)
                If strWJBS = "" Then Exit Try

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '执行检索
                With Me.m_objSqlDataAdapter
                    strSQL = ""
                    strSQL = strSQL + " select * from 公文_B_办理" + vbCr
                    strSQL = strSQL + " where 文件标识 = '" + strWJBS + "'" + vbCr
                    strSQL = strSQL + " and   交接序号 =  " + intJJXH.ToString + " " + vbCr

                    '设置参数
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    .SelectCommand = objSqlCommand

                    '执行操作
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
        ' 判断指定人员的审批事宜是否全部办理完毕？
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：人员名称
        '     blnComplete          ：返回：是否完毕？
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then Exit Try

                '计算
                strSQL = ""
                strSQL = strSQL + " select * from 公文_B_交接" + vbCr
                strSQL = strSQL + " where 文件标识 = '" + strWJBS + "'" + vbCr                            '当前文件
                strSQL = strSQL + " and   接收人   = '" + strUserXM + "'" + vbCr                          '接收人
                strSQL = strSQL + " and   办理子类 in (" + Me.FlowData.TaskBlzlSPSYList + ")" + vbCr      '审批事宜
                strSQL = strSQL + " and   办理状态 not in (" + Me.FlowData.TaskStatusYWCList + ")" + vbCr '未办完
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
        ' 输出数据到Excel
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objDataSet           ：要导出的数据集
        '     strExcelFile         ：导出到WEB服务器中的Excel文件路径
        '     strMacroName         ：宏名列表
        '     strMacroValue        ：宏值列表
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
        ' 获取发送给strUserXM的正常处理的交接单中的发送人列表(不论是否办完！)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：用户名称
        '     strSenderList        ：返回发送人列表
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim()

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS

                '计算最近交接序号，准则：接收人可看本交接、发送、正常消息
                strSQL = ""
                strSQL = strSQL + " select 发送人 " + vbCr
                strSQL = strSQL + " from 公文_B_交接 " + vbCr
                strSQL = strSQL + " where 文件标识 = '" + strWJBS + "'" + vbCr           '当前文件
                strSQL = strSQL + " and   接收人   = '" + strUserXM + "'" + vbCr         '接收人
                strSQL = strSQL + " and   rtrim(交接标识) like '1_1__0_%' " + vbCr              '接收人能看+非通知类
                strSQL = strSQL + " and   发送人  <> '" + strUserXM + "'" + vbCr         '去掉自己
                strSQL = strSQL + " group by 发送人" + vbCr
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
                            strFSR = objPulicParameters.getObjectValue(.Rows(i).Item("发送人"), "")
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
        ' 写用户操作审计日志
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strAddress           ：机器地址
        '     strMachine           ：机器名称
        '     strCZMS              ：操作描述
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        ' 更改说明：
        '      增加strMachine参数及相关处理
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
                '检查
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

                '写审计日志
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
        ' 写附件操作的审计日志
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strAddress           ：机器地址
        '     strMachine           ：机器名称
        '     objNewFJData         ：附件现有数据
        '     objOldFJData         ：附件原有数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        ' 更改说明：
        '      增加strMachine参数及相关处理
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
                '检查
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

                '逐个比较，检查是否被删除？
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
                            '备份RowFilter
                            strOldFilter = .DefaultView.RowFilter
                            '检查是否存在？
                            .DefaultView.RowFilter = Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_WJXH + " = " + strXH
                            If .DefaultView.Count < 1 Then
                                '被删除！
                                strCZMS = "删除了[" + Me.FlowData.WJBS + "]文件的第[" + strXH + "]个附件！"
                                If objdacCustomer.doWriteYonghuCaozuoRizhi(strErrMsg, strUserId, strPassword, strAddress, strMachine, strCZMS) Then
                                    '忽略
                                End If
                            End If
                            '恢复RowFilter
                            .DefaultView.RowFilter = strOldFilter
                        End With
                    Next
                End With

                '检查新附件
                Dim strBDWJ As String
                Dim strWJWZ As String
                With objNewFJData.Tables(strTable).DefaultView
                    intCountA = .Count
                    For i = 0 To intCountA - 1 Step 1
                        strXH = objPulicParameters.getObjectValue(.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_WJXH), "")
                        strBDWJ = objPulicParameters.getObjectValue(.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_BDWJ), "")
                        strWJWZ = objPulicParameters.getObjectValue(.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_WJWZ), "")
                        If strXH = "" Then
                            '新增加！
                            strCZMS = "添加了[" + Me.FlowData.WJBS + "]文件的第[" + (i + 1).ToString + "]个附件！"
                            If objdacCustomer.doWriteYonghuCaozuoRizhi(strErrMsg, strUserId, strPassword, strAddress, strMachine, strCZMS) Then
                                '忽略
                            End If
                        Else
                            If strBDWJ <> "" Then
                                '更改了附件文件
                                strCZMS = "重新上传了[" + Me.FlowData.WJBS + "]文件的第[" + (i + 1).ToString + "]个附件！"
                                If objdacCustomer.doWriteYonghuCaozuoRizhi(strErrMsg, strUserId, strPassword, strAddress, strMachine, strCZMS) Then
                                    '忽略
                                End If
                            End If
                            If CType(strXH, Integer) <> (i + 1) Then
                                '调整了附件位置
                                strCZMS = "将[" + Me.FlowData.WJBS + "]文件的第[" + strXH + "]个附件调整到第[" + (i + 1).ToString + "]个附件！"
                                If objdacCustomer.doWriteYonghuCaozuoRizhi(strErrMsg, strUserId, strPassword, strAddress, strMachine, strCZMS) Then
                                    '忽略
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
        ' 写相关文件操作的审计日志
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strAddress           ：机器地址
        '     strMachine           ：机器名称
        '     objNewXGWJData       ：相关文件现有数据
        '     objOldXGWJData       ：相关文件原有数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        ' 更改说明：
        '      增加strMachine参数及相关处理
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
                '检查
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

                '逐个比较，检查是否被删除？
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
                                    '备份RowFilter
                                    strOldFilter = .DefaultView.RowFilter
                                    '检查是否存在？
                                    strNewFilter = Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_FJXH + " = " + strXH
                                    strNewFilter = strNewFilter + " and " + Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_LBBS + " = 0"
                                    .DefaultView.RowFilter = strNewFilter
                                    If .DefaultView.Count < 1 Then
                                        '被删除！
                                        strCZMS = "删除了[" + Me.FlowData.WJBS + "]文件的第[" + strXH + "]个相关文件！"
                                        If objdacCustomer.doWriteYonghuCaozuoRizhi(strErrMsg, strUserId, strPassword, strAddress, strMachine, strCZMS) Then
                                            '忽略
                                        End If
                                    End If
                                    '恢复RowFilter
                                    .DefaultView.RowFilter = strOldFilter
                                End With

                            Case Xydc.Platform.Common.Data.FlowData.enumXGWJLB.FujianFile
                                strXH = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_FJXH), "")
                                With objNewXGWJData.Tables(strTable)
                                    '备份RowFilter
                                    strOldFilter = .DefaultView.RowFilter
                                    '检查是否存在？
                                    strNewFilter = Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_FJXH + " = " + strXH
                                    strNewFilter = strNewFilter + " and " + Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_LBBS + " = 1"
                                    .DefaultView.RowFilter = strNewFilter
                                    If .DefaultView.Count < 1 Then
                                        '被删除！
                                        strCZMS = "删除了[" + Me.FlowData.WJBS + "]文件的第[" + strXH + "]个相关文件！"
                                        If objdacCustomer.doWriteYonghuCaozuoRizhi(strErrMsg, strUserId, strPassword, strAddress, strMachine, strCZMS) Then
                                            '忽略
                                        End If
                                    End If
                                    '恢复RowFilter
                                    .DefaultView.RowFilter = strOldFilter
                                End With
                        End Select
                    Next
                End With

                '检查新相关文件
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
                                    '新增加！
                                    strCZMS = "添加了[" + Me.FlowData.WJBS + "]文件的第[" + (i + 1).ToString + "]个相关文件，文件标识为[" + strBDWJ + "]！"
                                    If objdacCustomer.doWriteYonghuCaozuoRizhi(strErrMsg, strUserId, strPassword, strAddress, strMachine, strCZMS) Then
                                        '忽略
                                    End If
                                Else
                                    If CType(strXH, Integer) <> (i + 1) Then
                                        '调整了附件位置
                                        strCZMS = "将[" + Me.FlowData.WJBS + "]文件的第[" + strXH + "]个相关文件调整到第[" + (i + 1).ToString + "]个相关文件！"
                                        If objdacCustomer.doWriteYonghuCaozuoRizhi(strErrMsg, strUserId, strPassword, strAddress, strMachine, strCZMS) Then
                                            '忽略
                                        End If
                                    End If
                                End If
                            Case Xydc.Platform.Common.Data.FlowData.enumXGWJLB.FujianFile
                                strXH = objPulicParameters.getObjectValue(.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_FJXH), "")
                                strBDWJ = objPulicParameters.getObjectValue(.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_BDWJ), "")
                                strWJWZ = objPulicParameters.getObjectValue(.Item(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_FJWZ), "")
                                If strXH = "" Then
                                    '新增加！
                                    strCZMS = "添加了[" + Me.FlowData.WJBS + "]文件的第[" + (i + 1).ToString + "]个相关文件！"
                                    If objdacCustomer.doWriteYonghuCaozuoRizhi(strErrMsg, strUserId, strPassword, strAddress, strMachine, strCZMS) Then
                                        '忽略
                                    End If
                                Else
                                    If strBDWJ <> "" Then
                                        '更改了附件文件
                                        strCZMS = "重新上传了[" + Me.FlowData.WJBS + "]文件的第[" + (i + 1).ToString + "]个相关文件！"
                                        If objdacCustomer.doWriteYonghuCaozuoRizhi(strErrMsg, strUserId, strPassword, strAddress, strMachine, strCZMS) Then
                                            '忽略
                                        End If
                                    End If
                                    If CType(strXH, Integer) <> (i + 1) Then
                                        '调整了附件位置
                                        strCZMS = "将[" + Me.FlowData.WJBS + "]文件的第[" + strXH + "]个相关文件调整到第[" + (i + 1).ToString + "]个相关文件！"
                                        If objdacCustomer.doWriteYonghuCaozuoRizhi(strErrMsg, strUserId, strPassword, strAddress, strMachine, strCZMS) Then
                                            '忽略
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
        ' 获取指定交接的“协办”标志
        '     strErrMsg             ：如果错误，则返回错误信息
        '     intJJXH               ：交接序号
        '     strWTR                ：返回：“协办”标志
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If

                '获取文件标识
                Dim objSqlConnection As System.Data.SqlClient.SqlConnection = Me.SqlConnection
                Dim strWJBS As String = Me.WJBS
                If strWJBS = "" Then
                    Exit Try
                End If

                '获取strUseXM未处理完的非通知事宜中的委托人信息
                strSQL = ""
                strSQL = strSQL + " select 协办 from 公文_B_交接 " + vbCr
                strSQL = strSQL + " where 文件标识 = '" + strWJBS + "' " + vbCr
                strSQL = strSQL + " and   交接序号 =  " + intJJXH.ToString + vbCr
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables(0).Rows.Count < 1 Then
                    Exit Try
                End If

                '返回
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
        ' 保存协办标志数据(公文_B_交接)
        '     strErrMsg              ：如果错误，则返回错误信息
        '     objSqlTransaction      ：现有事务
        '     strUserXM              ：人员名称
        '     strNewXBBZ             ：协办标志
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
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

            '初始化
            doSetJiaojieXBBZ = False
            strErrMsg = ""

            Try
                '检查
                If Me.IsInitialized = False Then
                    strErrMsg = "错误：[doSetJiaojieXBBZ]对象还没有初始化，不能使用！"
                    GoTo errProc
                End If
                If strUserXM Is Nothing Then strUserXM = ""
                strUserXM = strUserXM.Trim
                If strUserXM = "" Then
                    strErrMsg = "错误：[isXBBZConflict]没有输入[用户名称]！"
                    GoTo errProc
                End If
                If strNewXBBZ Is Nothing Then strNewXBBZ = ""
                strNewXBBZ = strNewXBBZ.Trim
                If strNewXBBZ = "" Then strNewXBBZ = Xydc.Platform.Common.Utilities.PulicParameters.CharFalse

                '获取现有信息
                If objSqlTransaction Is Nothing Then
                    objSqlConnection = Me.SqlConnection
                Else
                    objSqlConnection = objSqlTransaction.Connection
                End If
                strWJBS = Me.WJBS

                '开始事务
                If objSqlTransaction Is Nothing Then
                    blnNewTrans = True
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Else
                    blnNewTrans = False
                End If

                '保存数据
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    strSQL = ""
                    strSQL = strSQL + " update 公文_B_交接 set " + vbCr
                    strSQL = strSQL + "   协办 = @xbbz" + vbCr
                    strSQL = strSQL + " where 文件标识 = @wjbs" + vbCr
                    strSQL = strSQL + " and   接收人   = @jsr" + vbCr
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

                '提交事务
                If blnNewTrans = True Then
                    objSqlTransaction.Commit()
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)

            '返回
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
        ' 按照“显示序号”、“行政级别”、“组织代码”、“人员序号”、
        '“办理日期 desc”的排序原则向“公文_B_办理”写入“显示序号”内容
        '     strErrMsg            ：返回错误信息
        ' 返回
        '     True                 ：成功
        '     False                ：不成功
        ' 备注:
        '     增加
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

            '初始化
            doWriteXSXH = False
            strErrMsg = ""

            Try
                '检查
                If Me.IsInitialized = False Then
                    strErrMsg = "错误：[doWriteXSXH]对象还没有初始化，不能使用！"
                    GoTo errProc
                End If

                '获取现有信息
                objSqlConnection = Me.SqlConnection
                strWJBS = Me.WJBS
                If strWJBS Is Nothing Then strWJBS = ""
                strWJBS = strWJBS.Trim
                If strWJBS = "" Then
                    '不用处理！
                    Exit Try
                End If

                '获取本文件的全部审批意见
                strSQL = ""
                strSQL = strSQL + " select a.*" + vbCr
                strSQL = strSQL + " from" + vbCr
                strSQL = strSQL + " (" + vbCr
                strSQL = strSQL + "   select a.*,b.行政级别,b.组织代码,b.人员序号" + vbCr
                strSQL = strSQL + "   from" + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select * from 公文_B_办理" + vbCr
                strSQL = strSQL + "     where 文件标识 = '" + strWJBS + "'" + vbCr
                strSQL = strSQL + "   ) a" + vbCr
                strSQL = strSQL + "   left join" + vbCr
                strSQL = strSQL + "   (" + vbCr
                strSQL = strSQL + "     select a.*,b.级别名称,b.行政级别" + vbCr
                strSQL = strSQL + "     from 公共_B_人员 a" + vbCr
                strSQL = strSQL + "     left join 公共_B_行政级别 b on a.级别代码 = b.级别代码" + vbCr
                strSQL = strSQL + "   ) b on a.办理人 = b.人员名称" + vbCr
                strSQL = strSQL + " ) a" + vbCr
                strSQL = strSQL + " order by a.显示序号,a.行政级别,a.组织代码,a.人员序号,a.办理日期 desc"
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objOpinionData) = False Then
                    GoTo errProc
                End If

                '开始事务
                objSqlTransaction = objSqlConnection.BeginTransaction()

                '保存数据
                Dim intCount, i, intJJXH As Integer
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '按顺序写入“显示序号”
                    intCount = objOpinionData.Tables(0).Rows.Count
                    For i = 0 To intCount - 1 Step 1
                        '获取数据
                        strWJBS = objPulicParameters.getObjectValue(objOpinionData.Tables(0).Rows(i).Item("文件标识"), "")
                        intJJXH = objPulicParameters.getObjectValue(objOpinionData.Tables(0).Rows(i).Item("交接序号"), 0)

                        '准备SQL
                        strSQL = ""
                        strSQL = strSQL + " update 公文_B_办理 set" + vbCr
                        strSQL = strSQL + "   显示序号 = @xsxh" + vbCr
                        strSQL = strSQL + " where 文件标识 = @wjbs" + vbCr
                        strSQL = strSQL + " and   交接序号 = @jjxh" + vbCr
                        objSqlCommand.CommandText = strSQL
                        objSqlCommand.Parameters.Clear()
                        objSqlCommand.Parameters.AddWithValue("@xsxh", i + 1)
                        objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                        objSqlCommand.Parameters.AddWithValue("@jjxh", intJJXH)
                        '提交执行
                        objSqlCommand.ExecuteNonQuery()
                    Next

                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo rollDatabase
                End Try

                '提交事务
                objSqlTransaction.Commit()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objOpinionData)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
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
        ' 获取移交人strYJR可移交的工作流文件。如果文件已经移交给strJSR，则同时获取移交信息
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户ID
        '     strPassword          ：用户密码
        '     strYJR               ：移交人(姓名)
        '     strJSR               ：接收人(姓名)
        '     strWhere             ：搜索条件
        '     objYijiaoData        ：返回数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        ' 修改记录
        '      增加
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
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "错误：[getYijiaoData]没有指定[用户ID]！"
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

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '创建数据集
                objTempYijiaoData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_V_YIJIAOWENJIAN)
                If strYJR = "" Then Exit Try
                If strJSR = "" Then Exit Try

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '执行检索
                With objSqlDataAdapter
                    '计算
                    strSQL = ""
                    strSQL = strSQL + " select a.*" + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select a.*," + vbCr
                    strSQL = strSQL + "     b.移交人,b.接收人,b.移交日期,b.移交说明,b.接收日期," + vbCr
                    strSQL = strSQL + "     是否移交 = case when b.文件标识 is null then @false else @true end," + vbCr
                    strSQL = strSQL + "     是否接收 = case when b.接收日期 is null then @false else @true end " + vbCr
                    strSQL = strSQL + "   from" + vbCr
                    strSQL = strSQL + "   (" + vbCr
                    strSQL = strSQL + "     select a.* from 公文_V_全部审批文件新 a" + vbCr
                    strSQL = strSQL + "     left join" + vbCr
                    strSQL = strSQL + "     (" + vbCr
                    strSQL = strSQL + "       select 文件标识" + vbCr
                    strSQL = strSQL + "       from 公文_B_交接" + vbCr
                    strSQL = strSQL + "       where ((接收人=@yjr and 交接标识 like '__1%') or (发送人=@yjr and 交接标识 like '_1%'))"
                    strSQL = strSQL + "       group by 文件标识"
                    strSQL = strSQL + "     ) b on a.文件标识 = b.文件标识" + vbCr
                    strSQL = strSQL + "     where b.文件标识 is not null" + vbCr

                    'strSQL = strSQL + "     and a.办理状态 ='办理完毕'" + vbCr
                    strSQL = strSQL + "   and a.办理状态 = '" + Xydc.Platform.Common.Workflow.BaseFlowObject.FILESTATUS_YWC + "'" + vbCr

                    strSQL = strSQL + "   ) a" + vbCr
                    strSQL = strSQL + "   left join" + vbCr
                    strSQL = strSQL + "   (" + vbCr
                    strSQL = strSQL + "     select *" + vbCr
                    strSQL = strSQL + "     from 公文_B_移交" + vbCr
                    strSQL = strSQL + "     where 移交人=@yjr" + vbCr
                    strSQL = strSQL + "     and   接收人=@jsr" + vbCr
                    strSQL = strSQL + "   ) b on a.文件标识 = b.文件标识" + vbCr
                    strSQL = strSQL + " ) a" + vbCr
                    If strWhere <> "" Then
                        strSQL = strSQL + " where " + strWhere + vbCr
                    End If
                    strSQL = strSQL + " order by a.文件年度 desc,a.文件类型,a.文件字号" + vbCr

                    '设置参数
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@false", Xydc.Platform.Common.Utilities.PulicParameters.CharFalse)
                    objSqlCommand.Parameters.AddWithValue("@true", Xydc.Platform.Common.Utilities.PulicParameters.CharTrue)
                    objSqlCommand.Parameters.AddWithValue("@yjr", strYJR)
                    objSqlCommand.Parameters.AddWithValue("@jsr", strJSR)
                    .SelectCommand = objSqlCommand

                    '执行操作
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
        ' 获取移交人strYJR移交给strJSR的工作流文件
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户ID
        '     strPassword          ：用户密码
        '     strYJR               ：移交人(姓名)
        '     strJSR               ：接收人(姓名)
        '     strWhere             ：搜索条件
        '     objJieshouData       ：返回数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        ' 修改记录
        '      增加
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
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "错误：[getJieshouData]没有指定[用户ID]！"
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

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '创建数据集
                objTempJieshouData = New Xydc.Platform.Common.Data.FlowData(Xydc.Platform.Common.Data.FlowData.enumTableType.GW_V_YIJIAOWENJIAN)
                If strYJR = "" Then Exit Try
                If strJSR = "" Then Exit Try

                '创建SqlCommand
                objSqlCommand = New System.Data.SqlClient.SqlCommand
                objSqlCommand.Connection = objSqlConnection
                objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                '执行检索
                With objSqlDataAdapter
                    '计算
                    strSQL = ""
                    strSQL = strSQL + " select a.*" + vbCr
                    strSQL = strSQL + " from" + vbCr
                    strSQL = strSQL + " (" + vbCr
                    strSQL = strSQL + "   select b.*," + vbCr
                    strSQL = strSQL + "     a.移交人,a.接收人,a.移交日期,a.移交说明,a.接收日期," + vbCr
                    strSQL = strSQL + "     是否移交 = @true," + vbCr
                    strSQL = strSQL + "     是否接收 = case when a.接收日期 is null then @false else @true end " + vbCr
                    strSQL = strSQL + "   from" + vbCr
                    strSQL = strSQL + "   ("
                    strSQL = strSQL + "     select * from 公文_B_移交" + vbCr
                    strSQL = strSQL + "     where 移交人 = @yjr" + vbCr
                    strSQL = strSQL + "     and   接收人 = @jsr" + vbCr
                    strSQL = strSQL + "   ) a" + vbCr
                    strSQL = strSQL + "   left join 公文_V_全部审批文件新 b on a.文件标识 = b.文件标识" + vbCr
                    strSQL = strSQL + "   where b.文件标识 is not null" + vbCr
                    strSQL = strSQL + " ) a" + vbCr
                    If strWhere <> "" Then
                        strSQL = strSQL + " where " + strWhere + vbCr
                    End If
                    strSQL = strSQL + " order by a.文件年度 desc,a.文件类型,a.文件字号" + vbCr

                    '设置参数
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@false", Xydc.Platform.Common.Utilities.PulicParameters.CharFalse)
                    objSqlCommand.Parameters.AddWithValue("@true", Xydc.Platform.Common.Utilities.PulicParameters.CharTrue)
                    objSqlCommand.Parameters.AddWithValue("@yjr", strYJR)
                    objSqlCommand.Parameters.AddWithValue("@jsr", strJSR)
                    .SelectCommand = objSqlCommand

                    '执行操作
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
        ' 获取移交给strJSR的移交人列表
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户ID
        '     strPassword          ：用户密码
        '     strJSR               ：接收人(姓名)
        '     objYjrData           ：返回数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        ' 修改记录
        '      增加
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
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "错误：[getYjrListData]没有指定[用户ID]！"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim
                If strJSR Is Nothing Then strJSR = ""
                strJSR = strJSR.Trim

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '计算
                strSQL = "select 移交人 from 公文_B_移交 where 接收人 = '" + strJSR + "' group by 移交人 order by 移交人"
                If objdacCommon.getDataSetBySQL(strErrMsg, objSqlConnection, strSQL, objYjrData) = False Then
                    GoTo errProc
                End If
                If objYjrData.Tables.Count < 1 Then
                    Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objYjrData)
                    strErrMsg = "错误：[getYjrListData]无法获取[移交人]数据！"
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
        ' strYJR向strJSR移交文件strWJBS
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户ID
        '     strPassword          ：用户密码
        '     strYJR               ：移交人(姓名)
        '     strJSR               ：接收人(姓名)
        '     strWJBS              ：要移交的工作流文件标识
        '     strYJSM              ：移交描述
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        ' 修改记录
        '      增加
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

            '初始化
            doFile_Yijiao = False
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "错误：[doFile_Yijiao]没有指定[用户ID]！"
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
                    strErrMsg = "错误：[doFile_Yijiao]没有指定[移交人]、[接收人]、或[要移交的文件]！"
                    GoTo errProc
                End If
                If strYJSM Is Nothing Then strYJSM = ""
                strYJSM = strYJSM.Trim

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '开始事务
                Try
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '移交处理
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
                        '执行SQL
                        strSQL = ""
                        strSQL = strSQL + " insert into 公文_B_移交 (" + vbCr
                        strSQL = strSQL + "   移交人,接收人,文件标识,移交日期,移交说明,接收日期" + vbCr
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

                '提交事务
                objSqlTransaction.Commit()
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
            doFile_Yijiao = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据strWJBS获取strWJLX
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户ID
        '     strPassword          ：用户密码
        '     strWJBS              ：文件标识
        '     strWJLX              ：返回工作流类型名称
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        ' 修改记录
        '      增加
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
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "错误：[getWJLX]没有指定[用户ID]！"
                    GoTo errProc
                End If
                If strPassword Is Nothing Then strPassword = ""
                strPassword = strPassword.Trim
                If strWJBS Is Nothing Then strWJBS = ""
                strWJBS = strWJBS.Trim
                If strWJBS = "" Then
                    Exit Try
                End If

                '执行
                strSQL = "select 文件类型 from 公文_V_全部审批文件新 where 文件标识 = '" + strWJBS + "'"
                If objdacCommon.getDataSetBySQL(strErrMsg, strUserId, strPassword, strSQL, objDataSet) = False Then
                    GoTo errProc
                End If
                If objDataSet.Tables.Count > 0 Then
                    If objDataSet.Tables(0).Rows.Count > 0 Then
                        strWJLX = objPulicParameters.getObjectValue(objDataSet.Tables(0).Rows(0).Item("文件类型"), "")
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
        ' strJSR接收strYJR移交的文件strWJBS
        ' 如果strJSR不能看该文件，则strYJR自动向strJSR发送“补阅”单并自动标记“已阅读”
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户ID
        '     strPassword          ：用户密码
        '     strYJR               ：移交人(姓名)
        '     strJSR               ：接收人(姓名)
        '     strWJBS              ：要移交的工作流文件标识
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        ' 修改记录
        '      增加
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

            '初始化
            doFile_Jieshou = False
            strErrMsg = ""

            Try
                '检查
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    strErrMsg = "错误：[doFile_Jieshou]没有指定[用户ID]！"
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
                    strErrMsg = "错误：[doFile_Jieshou]没有指定[移交人]、[接收人]、或[要接收的文件]！"
                    GoTo errProc
                End If

                '获取工作流类型
                Dim strType As String = ""
                Dim strName As String = ""
                If Xydc.Platform.DataAccess.FlowObject.getWJLX(strErrMsg, strUserId, strPassword, strWJBS, strName) = False Then
                    GoTo errProc
                End If
                strType = Xydc.Platform.DataAccess.FlowObject.getFlowType(strName)
                If strType = "" Then
                    strErrMsg = "错误：[doFile_Jieshou]不支持指定的工作流[" + strName + "]！"
                    GoTo errProc
                End If

                '创建工作流
                objFlowObject = Xydc.Platform.DataAccess.FlowObject.Create(strType, strName)
                If objFlowObject.doInitialize(strErrMsg, strUserId, strPassword, strWJBS, False) = False Then
                    GoTo errProc
                End If
                Dim blnCanRead As Boolean = False
                If objFlowObject.canReadFile(strErrMsg, strJSR, blnCanRead) = False Then
                    GoTo errProc
                End If

                '获取连接
                If objdacCommon.getConnection(strErrMsg, strUserId, strPassword, objSqlConnection) = False Then
                    GoTo errProc
                End If

                '开始事务
                Try
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '接收处理
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '执行SQL
                    strSQL = ""
                    strSQL = strSQL + " update 公文_B_移交 set" + vbCr
                    strSQL = strSQL + "   接收日期 = @jsrq" + vbCr
                    strSQL = strSQL + " where 移交人   = @yjr" + vbCr
                    strSQL = strSQL + " and   接收人   = @jsr" + vbCr
                    strSQL = strSQL + " and   文件标识 = @wjbs" + vbCr
                    strSQL = strSQL + " and   接收日期 is null" + vbCr
                    objSqlCommand.Parameters.Clear()
                    objSqlCommand.Parameters.AddWithValue("@jsrq", Now)
                    objSqlCommand.Parameters.AddWithValue("@yjr", strYJR)
                    objSqlCommand.Parameters.AddWithValue("@jsr", strJSR)
                    objSqlCommand.Parameters.AddWithValue("@wjbs", strWJBS)
                    objSqlCommand.CommandText = strSQL
                    objSqlCommand.ExecuteNonQuery()

                    '写“补阅”单
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

                '提交事务
                objSqlTransaction.Commit()
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlConnection)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.FlowObject.SafeRelease(objFlowObject)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
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
        ' 根据strWJBS,strWJLX更新文件信息
        '     strErrMsg            ：如果错误，则返回错误信息       
        '     objNewData           : 新的数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        ' 修改记录
        '      2008-08-04 增加
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

            '初始化
            doUpdateWJXX = False
            strErrMsg = ""

            Try
                '检查
                If Me.m_blnInitialized = False Then
                    strErrMsg = "错误：对象还没有初始化，不能使用！"
                    GoTo errProc
                End If

                '获取连接
                objSqlConnection = Me.SqlConnection

                '开始事务
                Try
                    objSqlTransaction = objSqlConnection.BeginTransaction()
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try

                '保存数据
                Try
                    objSqlCommand = objSqlConnection.CreateCommand()
                    objSqlCommand.Connection = objSqlConnection
                    objSqlCommand.Transaction = objSqlTransaction
                    objSqlCommand.CommandTimeout = Xydc.Platform.Common.jsoaConfiguration.CommandTimeout

                    '计算SQL
                    Dim intDefaultValue As Integer = 0
                    Dim strFileds As String = ""
                    Dim strValues As String = ""
                    Dim strField As String = ""
                    Dim intCount As Integer = 0
                    Dim i As Integer = 0

                    '获取原“文件标识”
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

                    '计算更新字段列表
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

                    '准备SQL
                    strSQL = ""
                    strSQL = strSQL + " update " + strTable + " set " + vbCr
                    strSQL = strSQL + "   " + strFileds + vbCr
                    strSQL = strSQL + " where 文件标识 = '" + strOldWJBS + "'" + vbCr

                    '准备参数
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

                '提交事务
                objSqlTransaction.Commit()
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSqlCommand)
            Xydc.Platform.DataAccess.dacCommon.SafeRelease(objdacCommon)

            '返回
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
