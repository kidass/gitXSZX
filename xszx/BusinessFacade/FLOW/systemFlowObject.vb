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
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：systemFlowObject
    '
    ' 功能描述： 
    '   　工作流对象的表现层的基对象
    '----------------------------------------------------------------
    Public MustInherit Class systemFlowObject
        Inherits MarshalByRefObject
        Implements IDisposable

        '对象类型、对象创建接口注册器(所有对象共享)
        Private Shared m_objFlowTypeNameEnum As System.Collections.Specialized.NameValueCollection
        Private Shared m_objFlowTypeEnum As System.Collections.Specialized.ListDictionary

        '商业逻辑层对象
        Protected m_objrulesFlowObject As Xydc.Platform.BusinessRules.rulesFlowObject

        '工作流常用命令是否可以执行？
        Protected m_blnFSWJ As Boolean '发送文件
        Protected m_blnTHWJ As Boolean '退回文件
        Protected m_blnJSWJ As Boolean '接收文件
        Protected m_blnSHWJ As Boolean '收回文件
        Protected m_blnXGWJ As Boolean '修改文件
        Protected m_blnBCWJ As Boolean '保存文件
        Protected m_blnQXXG As Boolean '取消修改
        Protected m_blnSXWJ As Boolean '刷新文件
        Protected m_blnTXYJ As Boolean '填写意见
        Protected m_blnBDPS As Boolean '补登批示
        Protected m_blnCBWJ As Boolean '催办文件
        Protected m_blnDBWJ As Boolean '督办文件
        Protected m_blnDBJG As Boolean '督办结果
        Protected m_blnBYBL As Boolean '我不用办
        Protected m_blnBLWB As Boolean '我已办完
        Protected m_blnWYYZ As Boolean '我已阅知
        Protected m_blnZHBL As Boolean '暂缓办理
        Protected m_blnJXBL As Boolean '继续办理
        Protected m_blnZFWJ As Boolean '作废文件
        Protected m_blnQYWJ As Boolean '启用文件
        Protected m_blnWJBY As Boolean '文件补阅
        Protected m_blnCYYJ As Boolean '查阅意见
        Protected m_blnCKLZ As Boolean '查看流转
        Protected m_blnCKRZ As Boolean '查看日志
        Protected m_blnCKBY As Boolean '查看补阅
        Protected m_blnCKCB As Boolean '查看催办
        Protected m_blnCKDB As Boolean '查看督办
        Protected m_blnDYGZ As Boolean '打印稿纸
        Protected m_blnDYBJ As Boolean '打印便笺
        Protected m_blnWJBJ As Boolean '文件办结
        Protected m_blnFHSJ As Boolean '返回上级

        '模块附加信息
        Protected m_blnEnforeEdit As Boolean  '标记强制编辑
        Protected m_blnMustJieshou As Boolean '必须先接收文件









        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Protected Sub New()
            MyBase.New()

            m_objrulesFlowObject = Nothing

            '模块命令
            m_blnFSWJ = False '发送文件
            m_blnTHWJ = False '退回文件
            m_blnJSWJ = False '接收文件
            m_blnSHWJ = False '收回文件
            m_blnXGWJ = False '修改文件
            m_blnBCWJ = False '保存文件
            m_blnQXXG = False '取消修改
            m_blnSXWJ = False '刷新文件
            m_blnTXYJ = False '填写意见
            m_blnBDPS = False '补登批示
            m_blnCBWJ = False '催办文件
            m_blnDBWJ = False '督办文件
            m_blnDBJG = False '督办结果
            m_blnBYBL = False '我不用办
            m_blnBLWB = False '我已办完
            m_blnWYYZ = False '我已阅知
            m_blnZHBL = False '暂缓办理
            m_blnJXBL = False '继续办理
            m_blnZFWJ = False '作废文件
            m_blnQYWJ = False '启用文件
            m_blnWJBY = False '文件补阅
            m_blnCYYJ = False '查阅意见
            m_blnCKLZ = False '查看流转
            m_blnCKRZ = False '查看日志
            m_blnCKBY = False '查看补阅
            m_blnCKCB = False '查看催办
            m_blnCKDB = False '查看督办
            m_blnDYGZ = False '打印稿纸
            m_blnDYBJ = False '打印便笺
            m_blnWJBJ = False '文件办结
            m_blnFHSJ = False '返回上级

            '模块附加信息
            m_blnEnforeEdit = False  '标记强制编辑
            m_blnMustJieshou = False '必须先接收文件

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
                m_objrulesFlowObject = Xydc.Platform.BusinessRules.rulesFlowObject.Create(strFlowType, strFlowTypeName)
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
            If Not (m_objrulesFlowObject Is Nothing) Then
                m_objrulesFlowObject.Dispose()
                m_objrulesFlowObject = Nothing
            End If
        End Sub

        '----------------------------------------------------------------
        ' 安全释放本身资源
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
        ' 工作流对象注册器
        '     strFlowType          ：工作流类型代码
        '     strFlowTypeName      ：工作流类型名称
        '     objCreator           ：工作流对象ISystemFlowObjectCreate接口
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Shared Function RegisterFlowType( _
            ByVal strFlowType As String, _
            ByVal strFlowTypeName As String, _
            ByVal objCreator As Xydc.Platform.BusinessFacade.ISystemFlowObjectCreate) As Boolean

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
                    Throw New Exception("错误：[ISystemFlowObjectCreate]不能为空！")
                End If

                '生成类型汇集器
                If m_objFlowTypeEnum Is Nothing Then
                    m_objFlowTypeEnum = New System.Collections.Specialized.ListDictionary
                End If
                If m_objFlowTypeNameEnum Is Nothing Then
                    m_objFlowTypeNameEnum = New System.Collections.Specialized.NameValueCollection
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
        '                          ：Xydc.Platform.BusinessFacade.systemFlowObject对象
        '----------------------------------------------------------------
        Public Shared Function Create( _
            ByVal strFlowType As String, _
            ByVal strFlowTypeName As String) As Xydc.Platform.BusinessFacade.systemFlowObject

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

                '注册已经实现的ISystemFlowObjectCreate
                Dim strType As String
                Dim strName As String

                
                '*****************************************************************************************************
                '督查单工作流
                'strType = Xydc.Platform.Common.Workflow.BaseFlowDuchadan.FLOWCODE
                'strName = Xydc.Platform.Common.Workflow.BaseFlowDuchadan.FLOWNAME
                'If m_objFlowTypeEnum Is Nothing Then
                '    RegisterFlowType(strType, strName, New Xydc.Platform.BusinessFacade.systemFlowObjectDuchadanCreator)
                'Else
                '    If m_objFlowTypeEnum.Item(strType) Is Nothing Then
                '        RegisterFlowType(strType, strName, New Xydc.Platform.BusinessFacade.systemFlowObjectDuchadanCreator)
                '    End If
                'End If

                '获取接口
                Dim objCreator As Object
                objCreator = m_objFlowTypeEnum.Item(strFlowType)
                If objCreator Is Nothing Then
                    Throw New Exception("错误：[" + strFlowType + "]不支持！")
                End If
                Dim objISystemFlowObjectCreate As Xydc.Platform.BusinessFacade.ISystemFlowObjectCreate
                objISystemFlowObjectCreate = CType(objCreator, Xydc.Platform.BusinessFacade.ISystemFlowObjectCreate)
                If objISystemFlowObjectCreate Is Nothing Then
                    Throw New Exception("错误：[" + strFlowType + "]不支持！")
                End If

                '利用接口创建对象
                Create = objISystemFlowObjectCreate.Create(strFlowType, strFlowTypeName)

                '自动设置类型属性
                Create.FlowData.FlowType = strFlowType
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

            getFlowType = Xydc.Platform.BusinessRules.rulesFlowObject.getFlowType(strFlowTypeName)

        End Function

        '----------------------------------------------------------------
        ' 根据strFlowTypeName获取strFlowType
        '     strFlowTypeName      ：工作流类型名称
        ' 返回
        '                          ：strFlowType
        '----------------------------------------------------------------
        Public Shared Function getFlowTypeCollection() As System.Collections.Specialized.NameValueCollection

            getFlowTypeCollection = m_objFlowTypeNameEnum

        End Function









        '----------------------------------------------------------------
        ' FlowData属性
        '----------------------------------------------------------------
        Public ReadOnly Property FlowData() As Xydc.Platform.Common.Workflow.BaseFlowObject
            Get
                FlowData = m_objrulesFlowObject.FlowData
            End Get
        End Property

        '----------------------------------------------------------------
        ' IsInitialized属性
        '----------------------------------------------------------------
        Public ReadOnly Property IsInitialized() As Boolean
            Get
                IsInitialized = m_objrulesFlowObject.IsInitialized
            End Get
        End Property

        '----------------------------------------------------------------
        ' IsFillData属性
        '----------------------------------------------------------------
        Public ReadOnly Property IsFillData() As Boolean
            Get
                IsFillData = m_objrulesFlowObject.IsFillData
            End Get
        End Property



        '----------------------------------------------------------------
        ' mlFSWJ属性
        '----------------------------------------------------------------
        Public ReadOnly Property mlFSWJ() As Boolean
            Get
                mlFSWJ = m_blnFSWJ
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlTHWJ属性
        '----------------------------------------------------------------
        Public ReadOnly Property mlTHWJ() As Boolean
            Get
                mlTHWJ = m_blnTHWJ
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlJSWJ属性
        '----------------------------------------------------------------
        Public ReadOnly Property mlJSWJ() As Boolean
            Get
                mlJSWJ = m_blnJSWJ
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlSHWJ属性
        '----------------------------------------------------------------
        Public ReadOnly Property mlSHWJ() As Boolean
            Get
                mlSHWJ = m_blnSHWJ
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlXGWJ属性
        '----------------------------------------------------------------
        Public ReadOnly Property mlXGWJ() As Boolean
            Get
                mlXGWJ = m_blnXGWJ
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlBCWJ属性
        '----------------------------------------------------------------
        Public ReadOnly Property mlBCWJ() As Boolean
            Get
                mlBCWJ = m_blnBCWJ
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlQXXG属性
        '----------------------------------------------------------------
        Public ReadOnly Property mlQXXG() As Boolean
            Get
                mlQXXG = m_blnQXXG
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlSXWJ属性
        '----------------------------------------------------------------
        Public ReadOnly Property mlSXWJ() As Boolean
            Get
                mlSXWJ = m_blnSXWJ
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlTXYJ属性
        '----------------------------------------------------------------
        Public ReadOnly Property mlTXYJ() As Boolean
            Get
                mlTXYJ = m_blnTXYJ
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlBDPS属性
        '----------------------------------------------------------------
        Public ReadOnly Property mlBDPS() As Boolean
            Get
                mlBDPS = m_blnBDPS
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlCBWJ属性
        '----------------------------------------------------------------
        Public ReadOnly Property mlCBWJ() As Boolean
            Get
                mlCBWJ = m_blnCBWJ
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlDBWJ属性
        '----------------------------------------------------------------
        Public ReadOnly Property mlDBWJ() As Boolean
            Get
                mlDBWJ = m_blnDBWJ
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlDBJG属性
        '----------------------------------------------------------------
        Public ReadOnly Property mlDBJG() As Boolean
            Get
                mlDBJG = m_blnDBJG
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlBYBL属性
        '----------------------------------------------------------------
        Public ReadOnly Property mlBYBL() As Boolean
            Get
                mlBYBL = m_blnBYBL
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlBLWB属性
        '----------------------------------------------------------------
        Public ReadOnly Property mlBLWB() As Boolean
            Get
                mlBLWB = m_blnBLWB
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlWYYZ属性
        '----------------------------------------------------------------
        Public ReadOnly Property mlWYYZ() As Boolean
            Get
                mlWYYZ = m_blnWYYZ
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlZHBL属性
        '----------------------------------------------------------------
        Public ReadOnly Property mlZHBL() As Boolean
            Get
                mlZHBL = m_blnZHBL
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlJXBL属性
        '----------------------------------------------------------------
        Public ReadOnly Property mlJXBL() As Boolean
            Get
                mlJXBL = m_blnJXBL
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlZFWJ属性
        '----------------------------------------------------------------
        Public ReadOnly Property mlZFWJ() As Boolean
            Get
                mlZFWJ = m_blnZFWJ
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlQYWJ属性
        '----------------------------------------------------------------
        Public ReadOnly Property mlQYWJ() As Boolean
            Get
                mlQYWJ = m_blnQYWJ
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlWJBY属性
        '----------------------------------------------------------------
        Public ReadOnly Property mlWJBY() As Boolean
            Get
                mlWJBY = m_blnWJBY
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlCYYJ属性
        '----------------------------------------------------------------
        Public ReadOnly Property mlCYYJ() As Boolean
            Get
                mlCYYJ = m_blnCYYJ
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlCKLZ属性
        '----------------------------------------------------------------
        Public ReadOnly Property mlCKLZ() As Boolean
            Get
                mlCKLZ = m_blnCKLZ
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlCKRZ属性
        '----------------------------------------------------------------
        Public ReadOnly Property mlCKRZ() As Boolean
            Get
                mlCKRZ = m_blnCKRZ
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlCKBY属性
        '----------------------------------------------------------------
        Public ReadOnly Property mlCKBY() As Boolean
            Get
                mlCKBY = m_blnCKBY
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlCKCB属性
        '----------------------------------------------------------------
        Public ReadOnly Property mlCKCB() As Boolean
            Get
                mlCKCB = m_blnCKCB
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlCKDB属性
        '----------------------------------------------------------------
        Public ReadOnly Property mlCKDB() As Boolean
            Get
                mlCKDB = m_blnCKDB
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlDYGZ属性
        '----------------------------------------------------------------
        Public ReadOnly Property mlDYGZ() As Boolean
            Get
                mlDYGZ = m_blnDYGZ
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlDYBJ属性
        '----------------------------------------------------------------
        Public ReadOnly Property mlDYBJ() As Boolean
            Get
                mlDYBJ = m_blnDYBJ
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlWJBJ属性
        '----------------------------------------------------------------
        Public ReadOnly Property mlWJBJ() As Boolean
            Get
                mlWJBJ = m_blnWJBJ
            End Get
        End Property

        '----------------------------------------------------------------
        ' mlFHSJ属性
        '----------------------------------------------------------------
        Public ReadOnly Property mlFHSJ() As Boolean
            Get
                mlFHSJ = m_blnFHSJ
            End Get
        End Property



        '----------------------------------------------------------------
        ' pmEnforeEdit属性
        '----------------------------------------------------------------
        Public ReadOnly Property pmEnforeEdit() As Boolean
            Get
                pmEnforeEdit = m_blnEnforeEdit
            End Get
        End Property

        '----------------------------------------------------------------
        ' pmMustJieshou属性
        '----------------------------------------------------------------
        Public ReadOnly Property pmMustJieshou() As Boolean
            Get
                pmMustJieshou = m_blnMustJieshou
            End Get
        End Property




        '----------------------------------------------------------------
        ' swgjShowTrackRevisions属性
        '----------------------------------------------------------------
        Public Overridable ReadOnly Property swgjShowTrackRevisions() As Boolean
            Get
                swgjShowTrackRevisions = True
            End Get
        End Property

        '----------------------------------------------------------------
        ' swgjSelectGJ属性
        '----------------------------------------------------------------
        Public Overridable ReadOnly Property swgjSelectGJ() As Boolean
            Get
                swgjSelectGJ = False
            End Get
        End Property

        '----------------------------------------------------------------
        ' swgjImportFile属性
        '----------------------------------------------------------------
        Public Overridable ReadOnly Property swgjImportFile() As Boolean
            Get
                swgjImportFile = False
            End Get
        End Property

        '----------------------------------------------------------------
        ' swgjExportFile属性
        '----------------------------------------------------------------
        Public Overridable ReadOnly Property swgjExportFile() As Boolean
            Get
                swgjExportFile = True
            End Get
        End Property





        '----------------------------------------------------------------
        ' 获取工作流文件目前能进行的任务
        '     strErrMsg      ：返回错误信息
        '     objTask        ：返回能进行的任务
        ' 返回 
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Public MustOverride Function getCanDoTaskList( _
            ByRef strErrMsg As String, _
            ByRef objTask As System.Collections.Specialized.NameValueCollection) As Boolean

        '----------------------------------------------------------------
        ' 打开指定文件的工作流模块
        ' 准备工作流调用接口并返回要访问工作流的Url
        '     strErrMsg      ：返回错误信息
        '     strControlId   ：当前操作组件ID
        '     strWJBS        ：文件标识
        '     strMSessionId  ：调用本工作流模块的父模块的MSessionId
        '     strISessionId  ：调用本工作流模块的父模块的ISessionId
        '     objEditType    ：工作流编辑类型
        '     Request        ：当前HttpRequest
        '     Session        ：当前HttpSessionState
        '     strUrl         ：返回要打开工作流文件的Url
        ' 返回 
        '     True           ：成功
        '     False          ：失败
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
        ' 打开新建文件的工作流模块
        ' 准备工作流调用接口并返回要访问工作流的Url
        '     strErrMsg      ：返回错误信息
        '     strControlId   ：当前操作组件ID
        '     strWJBS        ：文件标识=""
        '     strMSessionId  ：调用本工作流模块的父模块的MSessionId
        '     strISessionId  ：调用本工作流模块的父模块的ISessionId
        '     objEditType    ：工作流编辑类型
        '     Request        ：当前HttpRequest
        '     Session        ：当前HttpSessionState
        '     strUrl         ：返回要打开工作流文件的Url
        '     strRSessionId  ：返回新创建的会话ID
        ' 返回 
        '     True           ：成功
        '     False          ：失败
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
        ' 获取当前工作流的web页的Url(相对应用的根路径)
        ' 返回
        '                    ：当前工作流的web页的Url
        '----------------------------------------------------------------
        Public MustOverride Function getPageUrl() As String

        '----------------------------------------------------------------
        ' 计算可对当前文件进行的操作
        '     strErrMsg      ：返回错误信息
        '     strCzyId       ：当前用户代码
        '     strUserXM      ：当前用户名称
        '     strUserBMDM    ：当前用户单位代码
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Public MustOverride Function getCanExecuteCommand( _
            ByRef strErrMsg As String, _
            ByVal strCzyId As String, _
            ByVal strUserXM As String, _
            ByVal strUserBMDM As String) As Boolean


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
                '初始化工作流对象
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
            With Me.m_objrulesFlowObject
                getJiaojieData = .getJiaojieData(strErrMsg, strWhere, blnUnused, objJiaoJieData)
            End With
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
            With Me.m_objrulesFlowObject
                doSaveData_Jiaojie = .doSaveData_Jiaojie(strErrMsg, objOldData, objNewData, objenumEditType)
            End With
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
            With Me.m_objrulesFlowObject
                doUpdateJiaojie = .doUpdateJiaojie(strErrMsg, strWhere, strFileds)
            End With
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
            With Me.m_objrulesFlowObject
                getBanliData = .getBanliData(strErrMsg, strWhere, blnUnused, objBanliData)
            End With
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
            With Me.m_objrulesFlowObject
                doSaveData_Banli = .doSaveData_Banli(strErrMsg, objOldData, objNewData, objenumEditType)
            End With
        End Function


        '----------------------------------------------------------------
        ' 根据strWJBS获取“公文_B_附件”的数据集
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWJBS              ：文件标识
        '     objFujianData        ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
        ' 根据strWJBS获取“公文_B_附件”的数据集
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objFujianData        ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
        ' 根据strWJBS获取“公文_B_相关文件”的数据集
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWJBS              ：文件标识
        '     objXGWJData          ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
        ' 根据strWJBS获取“公文_B_相关文件”的数据集
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objXGWJData          ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
        ' 根据strWJBS获取“公文_B_交接”的数据集
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWJBS              ：文件标识
        '     objJiaojieData       ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
        ' 根据strWJBS获取“公文_B_交接”的数据集
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objJiaojieData       ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
        ' 获取strUserXM能阅读的审批意见数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWJBS              ：文件标识
        '     strUserXM            ：要察看的用户名称
        '     objOpinionData       ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
        ' 获取strUserXM能阅读的审批意见数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：要察看的用户名称
        '     objOpinionData       ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
        ' 获取strUserXM能阅读的审批意见数据(按搜索条件)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：要察看的用户名称
        '     strWhere             ：搜索条件
        '     objOpinionData       ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
        ' 获取新的文件流水号
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWJBS              ：文件标识
        '     strLSH               ：返回文件流水号
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
        ' 获取新的文件流水号
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strLSH               ：返回文件流水号
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
        ' 获取新的文件标识
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWJBS              ：文件标识
        '     strNewWJBS           ：返回文件标识
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
        ' 获取新的文件标识
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strNewWJBS           ：返回文件标识
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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

            Try
                getNewFSXH = Me.m_objrulesFlowObject.getNewFSXH(strErrMsg, strFSXH)
            Catch ex As Exception
                getNewFSXH = False
                strErrMsg = ex.Message
            End Try

        End Function




        '----------------------------------------------------------------
        ' 判断文件是否办理完毕?
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWJBS              ：文件标识
        '     blnComplete          ：返回是否办理完毕?
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
        ' 判断文件是否办理完毕?
        '     strErrMsg            ：如果错误，则返回错误信息
        '     blnComplete          ：返回是否办理完毕?
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
        ' 判断文件是否已经定稿?
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWJBS              ：文件标识
        '     blnDinggao           ：返回是否已经定稿?
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
        ' 判断文件是否已经定稿?
        '     strErrMsg            ：如果错误，则返回错误信息
        '     blnDinggao           ：返回是否已经定稿?
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
        ' 判断文件是否已经作废?
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWJBS              ：文件标识
        '     blnZuofei            ：返回是否已经作废?
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
        ' 判断文件是否已经作废?
        '     strErrMsg            ：如果错误，则返回错误信息
        '     blnZuofei            ：返回是否已经作废?
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
        ' 判断文件是否已经停办?
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWJBS              ：文件标识
        '     blnTingban           ：返回是否已经停办?
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
        ' 判断文件是否已经停办?
        '     strErrMsg            ：如果错误，则返回错误信息
        '     blnTingban           ：返回是否已经停办?
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
        ' 判断strUserXM是否是文件的原始作者?
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWJBS              ：文件标识
        '     strUserXM            ：人员名称
        '     blnIs                ：返回是否?
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
        ' 判断strUserXM是否是文件的原始作者?
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：人员名称
        '     blnIs                ：返回是否?
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
        ' 判断指定人员strCzyId是否可督办文件？
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWJBS              ：文件标识
        '     strCzyId             ：人员代码
        '     strBMDM              ：strCzyId所属单位代码
        '     blnCanDuban          ：返回：是否可以？
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
        ' 判断指定人员strCzyId是否可督办文件？
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strCzyId             ：人员代码
        '     strBMDM              ：strCzyId所属单位代码
        '     blnCanDuban          ：返回：是否可以？
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
        ' 判断指定人员strUserXM是否可填写督办结果？
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWJBS              ：文件标识
        '     strUserXM            ：当前操作人员名称
        '     blnCanWrite          ：返回：是否可以？
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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

            Try
                canWriteDubanResult = Me.m_objrulesFlowObject.canWriteDubanResult(strErrMsg, strUserXM, blnCanWrite)
            Catch ex As Exception
                canWriteDubanResult = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 判断指定人员是否可催办文件？
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWJBS              ：文件标识
        '     strUserXM            ：准备催办文件的人员名称
        '     blnCanCuiban         ：返回：是否可以催办？
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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

            Try
                canCuibanFile = Me.m_objrulesFlowObject.canCuibanFile(strErrMsg, strUserXM, blnCanCuiban)
            Catch ex As Exception
                canCuibanFile = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 判断指定人员是否可补登领导意见？
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWJBS              ：文件标识
        '     strCzyId             ：准备补登领导意见的人员代码
        '     strBMDM              ：准备补登领导意见的人员所属单位代码
        '     blnCanBudeng         ：返回：是否可以？
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
        ' 判断指定人员是否可补登领导意见？
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strCzyId             ：准备补登领导意见的人员代码
        '     strBMDM              ：准备补登领导意见的人员所属单位代码
        '     blnCanBudeng         ：返回：是否可以？
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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

            Try
                canReadFile = Me.m_objrulesFlowObject.canReadFile(strErrMsg, strUserXM, blnCanRead)
            Catch ex As Exception
                canReadFile = False
                strErrMsg = ex.Message
            End Try

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

            Try
                canSendTo = Me.m_objrulesFlowObject.canSendTo(strErrMsg, strSenderList, strReceiver, blnCanSend, strNewReceiver)
            Catch ex As Exception
                canSendTo = False
                strErrMsg = ex.Message
            End Try

        End Function





        '----------------------------------------------------------------
        ' strUserXM是否为自动签收文件？
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWJBS              ：文件标识
        '     strUserXM            ：用户名称
        '     blnAutoReceive       ：返回是否?
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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

            Try
                isAutoReceive = Me.m_objrulesFlowObject.isAutoReceive(strErrMsg, strUserXM, blnAutoReceive)
            Catch ex As Exception
                isAutoReceive = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 判断指定人员strUserXM是否可以接收文件？
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWJBS              ：文件标识
        '     strUserXM            ：操作人员名称
        '     blnCanDoJieshou      ：返回：是否可以？
        '     strFSRList           ：返回：发送人名称列表
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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

            Try
                canDoJieshouFile = Me.m_objrulesFlowObject.canDoJieshouFile(strErrMsg, strUserXM, blnCanDoJieshou, strFSRList)
            Catch ex As Exception
                canDoJieshouFile = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 文件是否发送过？
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWJBS              ：文件标识
        '     blnSendOnce          ：返回是否?
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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

            Try
                isFileSendOnce = Me.m_objrulesFlowObject.isFileSendOnce(strErrMsg, blnSendOnce)
            Catch ex As Exception
                isFileSendOnce = False
                strErrMsg = ex.Message
            End Try

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

            Try
                isReceiveZhizhi = Me.m_objrulesFlowObject.isReceiveZhizhi(strErrMsg, strUserXM, blnReceive)
            Catch ex As Exception
                isReceiveZhizhi = False
                strErrMsg = ex.Message
            End Try

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

            Try
                isSendZhizhi = Me.m_objrulesFlowObject.isSendZhizhi(strErrMsg, strUserXM, blnSend)
            Catch ex As Exception
                isSendZhizhi = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取strUserXM没有办完的事宜
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWJBS              ：文件标识
        '     strUserXM            ：用户名称
        '     objJiaoJieData       ：返回交接数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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

            Try
                getNotCompletedTaskData = Me.m_objrulesFlowObject.getNotCompletedTaskData(strErrMsg, strUserXM, objJiaoJieData)
            Catch ex As Exception
                getNotCompletedTaskData = False
                strErrMsg = ex.Message
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
            isTaskComplete = Me.m_objrulesFlowObject.isTaskComplete(strTaskBLZT)
        End Function

        '----------------------------------------------------------------
        ' 是否被退回的事宜？
        '     strTaskStatus        ：事宜状态
        ' 返回
        '     True                 ：是
        '     False                ：否
        '----------------------------------------------------------------
        Public Overridable Function isTaskTuihui(ByVal strTaskStatus As String) As Boolean
            isTaskTuihui = Me.m_objrulesFlowObject.isTaskTuihui(strTaskStatus)
        End Function

        '----------------------------------------------------------------
        ' 是否被收回的事宜？
        '     strTaskStatus        ：事宜状态
        ' 返回
        '     True                 ：是
        '     False                ：否
        '----------------------------------------------------------------
        Public Overridable Function isTaskShouhui(ByVal strTaskStatus As String) As Boolean
            isTaskShouhui = Me.m_objrulesFlowObject.isTaskShouhui(strTaskStatus)
        End Function

        '----------------------------------------------------------------
        ' 是否为通知类事宜？
        '     strTaskStatus        ：事宜状态
        ' 返回
        '     True                 ：是
        '     False                ：否
        '----------------------------------------------------------------
        Public Overridable Function isTaskTongzhi(ByVal strTaskStatus As String) As Boolean
            isTaskTongzhi = Me.m_objrulesFlowObject.isTaskTongzhi(strTaskStatus)
        End Function

        '----------------------------------------------------------------
        ' 是否为回复类事宜？
        '     strTaskStatus        ：事宜状态
        ' 返回
        '     True                 ：是
        '     False                ：否
        '----------------------------------------------------------------
        Public Overridable Function isTaskHuifu(ByVal strTaskStatus As String) As Boolean
            isTaskHuifu = Me.m_objrulesFlowObject.isTaskHuifu(strTaskStatus)
        End Function

        '----------------------------------------------------------------
        ' 是否为缓办事宜？
        '     strTaskBLZL          ：办理子类
        ' 返回
        '     True                 ：是
        '     False                ：否
        '----------------------------------------------------------------
        Public Overridable Function isTaskTingban(ByVal strTaskBLZL As String) As Boolean
            isTaskTingban = Me.m_objrulesFlowObject.isTaskTingban(strTaskBLZL)
        End Function

        '----------------------------------------------------------------
        ' 判断strBLSY是否已经批准?
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWJBS              ：文件标识
        '     strBLSY              ：事宜名称
        '     blnApproved          ：返回是否?
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
        ' 判断strBLSY是否已经批准?
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strBLSY              ：事宜名称
        '     blnApproved          ：返回是否?
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
        ' strUserXM是否有未办的通知类事宜？
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWJBS              ：文件标识
        '     strUserXM            ：用户名称
        '     blnHas               ：返回是否?
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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

            Try
                isHasNotCompleteTongzhi = Me.m_objrulesFlowObject.isHasNotCompleteTongzhi(strErrMsg, strUserXM, blnHas)
            Catch ex As Exception
                isHasNotCompleteTongzhi = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 自动接收文件
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWJBS              ：文件标识
        '     strUserXM            ：人员名称
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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

            Try
                doAutoReceive = Me.m_objrulesFlowObject.doAutoReceive(strErrMsg, strUserXM)
            Catch ex As Exception
                doAutoReceive = False
                strErrMsg = ex.Message
            End Try

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

            Try
                getOpinion = Me.m_objrulesFlowObject.getOpinion(strErrMsg, objOpinionData, strYJLX, strQSYJ, strBJYJ)
            Catch ex As Exception
                getOpinion = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 设置strUserXM已经阅读过指定文件
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWJBS              ：文件标识
        '     strUserXM            ：操作人员名称
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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

            Try
                doSetHasReadFile = Me.m_objrulesFlowObject.doSetHasReadFile(strErrMsg, strUserXM)
            Catch ex As Exception
                doSetHasReadFile = False
                strErrMsg = ex.Message
            End Try

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

            Try
                getFileLocked = Me.m_objrulesFlowObject.getFileLocked(strErrMsg, blnLocked, strBMMC, strRYMC)
            Catch ex As Exception
                getFileLocked = False
                strErrMsg = ex.Message
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

            Try
                doLockFile = Me.m_objrulesFlowObject.doLockFile(strErrMsg, strUserId, blnLocked)
            Catch ex As Exception
                doLockFile = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取当前文件的稿件内容
        '     strErrMsg      ：返回错误信息
        '     blnEditMode    ：编辑模式
        '     strCacheFile   ：(返回)当前缓存文件名(返回)
        '     strMBPath      ：模板文件目录
        '     strGJPath      ：稿件文件目录
        '     strGJFile      ：(返回)下载到HTTP服务器中的临时文件名
        ' 返回
        '     True           ：成功
        '     False          ：失败
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
        ' 删除文件
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
        ' 判断记录数据是否有效？
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objNewData           ：记录新值(返回推荐值)
        '     objOldData           ：记录旧值
        '     objenumEditType      ：编辑类型
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
        ' 保存记录
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objNewData           ：记录新值(返回保存后的新值)
        '     objOldData           ：记录旧值
        '     objenumEditType      ：编辑类型
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
        ' 保存工作流记录(完整事务操作)
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserId              ：用户标识
        '     strPassword            ：用户密码
        '     strUserXM              ：当前操作人员
        '     blnEnforeEdit          ：强制编辑文件数据
        '     objNewData             ：记录新值(返回保存后的新值)
        '     objOldData             ：记录旧值
        '     objenumEditType        ：编辑类型
        '     strGJFile              ：要保存的稿件文件的本地缓存文件完整路径
        '     objDataSet_FJ          ：要保存的附件数据
        '     objDataSet_XGWJ        ：要保存的相关文件数据
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
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
        ' 保存工作流记录(完整事务操作)
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserId              ：用户标识
        '     strPassword            ：用户密码
        '     strUserXM              ：当前操作人员
        '     blnEnforeEdit          ：强制编辑文件数据
        '     objNewData             ：记录新值(返回保存后的新值)
        '     objOldData             ：记录旧值
        '     objenumEditType        ：编辑类型
        '     strGJFile              ：要保存的稿件文件的本地缓存文件完整路径
        '     objDataSet_FJ          ：要保存的附件数据
        '     objDataSet_XGWJ        ：要保存的相关文件数据
        '     objParams              ：其他要随事务提交的数据
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
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
        ' 保存工作流稿件、附件、相关文件记录(完整事务操作)
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserId              ：用户标识
        '     strPassword            ：用户密码
        '     strGJFile              ：要保存的稿件文件的本地缓存文件完整路径
        '     objDataSet_FJ          ：要保存的附件数据
        '     objDataSet_XGWJ        ：要保存的相关文件数据
        '     strUserXM              ：当前操作人员
        '     blnEnforeEdit          ：强制编辑文件数据
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
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
        ' 保存工作流稿件、附件、相关文件记录(完整事务操作)
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserId              ：用户标识
        '     strPassword            ：用户密码
        '     strGJFile              ：要保存的稿件文件的本地缓存文件完整路径
        '     objDataSet_FJ          ：要保存的附件数据
        '     objDataSet_XGWJ        ：要保存的相关文件数据
        '     strUserXM              ：当前操作人员
        '     blnEnforeEdit          ：强制编辑文件数据
        '     objParams              ：其他要随事务提交的数据
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
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

            Try
                doSaveFujian = Me.m_objrulesFlowObject.doSaveFujian(strErrMsg, blnEnforeEdit, strUserXM, objNewData)
            Catch ex As Exception
                doSaveFujian = False
                strErrMsg = ex.Message
            End Try

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

            Try
                doSaveFujian = Me.m_objrulesFlowObject.doSaveFujian(strErrMsg, blnEnforeEdit, strUserXM, objNewData)
            Catch ex As Exception
                doSaveFujian = False
                strErrMsg = ex.Message
            End Try

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

            Try
                doSaveXgwj = Me.m_objrulesFlowObject.doSaveXgwj(strErrMsg, blnEnforeEdit, strUserXM, objNewData)
            Catch ex As Exception
                doSaveXgwj = False
                strErrMsg = ex.Message
            End Try

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

            Try
                doSaveXgwjFujian = Me.m_objrulesFlowObject.doSaveXgwjFujian(strErrMsg, blnEnforeEdit, strUserXM, objNewData)
            Catch ex As Exception
                doSaveXgwjFujian = False
                strErrMsg = ex.Message
            End Try

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

            Try
                doVerifyFujian = Me.m_objrulesFlowObject.doVerifyFujian(strErrMsg, objNewData)
            Catch ex As Exception
                doVerifyFujian = False
                strErrMsg = ex.Message
            End Try

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

            Try
                doVerifyXgwjFujian = Me.m_objrulesFlowObject.doVerifyXgwjFujian(strErrMsg, objNewData)
            Catch ex As Exception
                doVerifyXgwjFujian = False
                strErrMsg = ex.Message
            End Try

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
            With Me.m_objrulesFlowObject
                doDeleteData_Banli = .doDeleteData_Banli(strErrMsg, intJJXH)
            End With
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
        ' 删除下载或上载到Web服务器的临时文件
        '     strErrMsg      ：返回错误信息
        '     objFJDataSet   ：附件数据集
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Public Function doDeleteCacheFile_FJ( _
            ByRef strErrMsg As String, _
            ByVal objFJDataSet As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile

            doDeleteCacheFile_FJ = False

            Try
                '检查
                If objFJDataSet Is Nothing Then
                    Exit Try
                End If
                If objFJDataSet.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_FUJIAN) Is Nothing Then
                    Exit Try
                End If

                '逐个删除临时文件
                Dim strFile As String
                Dim intCount As Integer
                Dim i As Integer
                With objFJDataSet.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_FUJIAN)
                    intCount = .Rows.Count
                    For i = intCount - 1 To 0 Step -1
                        strFile = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_FUJIAN_BDWJ), "")
                        If strFile <> "" Then
                            If objBaseLocalFile.doDeleteFile(strErrMsg, strFile) = False Then
                                '可以不成功，产生垃圾数据
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
        ' 删除下载或上载到Web服务器的临时文件
        '     strErrMsg      ：返回错误信息
        '     objXGWJDataSet ：相关链接+相关附件数据集
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Public Function doDeleteCacheFile_XGWJ( _
            ByRef strErrMsg As String, _
            ByVal objXGWJDataSet As Xydc.Platform.Common.Data.FlowData) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile

            doDeleteCacheFile_XGWJ = False

            Try
                '检查
                If objXGWJDataSet Is Nothing Then
                    Exit Try
                End If
                If objXGWJDataSet.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_SHENPIWENJIAN_FUJIAN) Is Nothing Then
                    Exit Try
                End If

                '逐个删除临时文件
                Dim strFile As String
                Dim intCount As Integer
                Dim i As Integer
                With objXGWJDataSet.Tables(Xydc.Platform.Common.Data.FlowData.TABLE_GW_B_SHENPIWENJIAN_FUJIAN)
                    intCount = .Rows.Count
                    For i = intCount - 1 To 0 Step -1
                        strFile = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.FlowData.FIELD_GW_B_SHENPIWENJIAN_FUJIAN_BDWJ), "")
                        If strFile <> "" Then
                            If objBaseLocalFile.doDeleteFile(strErrMsg, strFile) = False Then
                                '可以不成功，产生垃圾数据
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
        ' 检查能否自动进入编辑模式编辑稿件、附件、相关文件内容
        '     strErrMsg            ：返回错误信息
        '     strUserId            ：当前人员标识
        '     blnEditMode          ：编辑模式
        '     blnCanModify         ：能否进行编辑？
        '     blnEnforeEdit        ：是否为强行编辑？
        '     blnAutoEnter         ：返回能否自动进入编辑模式
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
                '检查
                If Me.IsInitialized = False Then
                    strErrMsg = "错误：工作流对象没有初始化！"
                    GoTo errProc
                End If
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim

                If blnEditMode = True Then
                    '编辑模式下
                Else
                    '查看模式下
                    If blnCanModify = True Then
                        '能修改文件
                        '自动清除自己对文件的封锁
                        If Me.doLockFile(strErrMsg, strUserId, False) = False Then
                            GoTo errProc
                        End If
                        '获取当前文件编辑情况
                        Dim strBMMC As String
                        Dim strRYMC As String
                        Dim blnDo As Boolean
                        If Me.getFileLocked(strErrMsg, blnDo, strBMMC, strRYMC) = False Then
                            GoTo errProc
                        End If
                        If blnDo = True Then
                            '别人正在编辑！
                        Else
                            If blnEnforeEdit = True Then
                                '已经定稿，不自动进入！
                                blnAutoEnter = False
                            Else
                                blnAutoEnter = True
                            End If
                        End If
                    Else
                        '不能修改文件
                    End If
                End If


                '如果自动进入编辑状态，则需进行编辑锁定
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
        ' 计算strBLSY的级别
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strBLSY              ：事宜名称
        '     intLevel             ：返回级别
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
        ' strSender向strReceiver发送补阅交接单，并自动设置已经阅读
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strSender            ：发送人员名称
        '     strReceiver          ：接收人员名称
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
        ' 翻译办理事宜
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strOldBlsy           ：翻译前的办理事宜
        '     strNewBlsy           ：翻译后的办理事宜
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
        ' 处理“启用文件”业务
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：用户名称
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
        ' 处理“作废文件”业务
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：用户名称
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
        ' 处理“继续办理”业务
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：用户名称
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
        ' 处理“暂缓处理”业务
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：用户名称
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
        ' 处理“文件办结”业务
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserXM            ：用户名称
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
        ' 获取“批件原件”字段值
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strPJYJ              ：(返回)批件原件字段值
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
        ' 处理“导入签批件”业务
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strFileSpec          ：要导入的文件路径(WEB服务器本地完全路径)
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
        ' 获取“正式文件”字段值
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strZSWJ              ：(返回)正式文件字段值
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
        ' 处理“导入正式文件”业务
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strFileSpec          ：要导入的文件路径(WEB服务器本地完全路径)
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
        ' 保存催办数据
        '     strErrMsg              ：如果错误，则返回错误信息
        '     objNewData             ：记录新值(返回保存后的新值)
        '     objOldData             ：记录旧值
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
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
        ' 保存督办数据
        '     strErrMsg              ：如果错误，则返回错误信息
        '     objNewData             ：记录新值(返回保存后的新值)
        '     objOldData             ：记录旧值
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
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
        ' 保存督办结果数据
        '     strErrMsg              ：如果错误，则返回错误信息
        '     intJJXH                ：交接序号
        '     intDBXH                ：督办序号
        '     strDBJG                ：督办结果
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
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
        ' strSender向strReceiver发送补阅请求
        '     strErrMsg            ：如果错误，则返回错误信息
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
        ' strSender向strReceiver发送补阅通知
        '     strErrMsg            ：如果错误，则返回错误信息
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
        ' 收回补阅请求
        '     strErrMsg              ：如果错误，则返回错误信息
        '     intJJXH                ：交接序号
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
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
        ' 收回补阅通知
        '     strErrMsg              ：如果错误，则返回错误信息
        '     intJJXH                ：交接序号
        ' 返回
        '     True                   ：成功
        '     False                  ：失败

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
        ' 批准补阅请求
        '     strErrMsg              ：如果错误，则返回错误信息
        '     intJJXH                ：交接序号
        '     strFSXH                ：发送批次
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
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
        ' 拒绝补阅请求
        '     strErrMsg              ：如果错误，则返回错误信息
        '     intJJXH                ：交接序号
        '     strFSXH                ：发送批次
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
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
        ' 转发补阅请求
        '     strErrMsg              ：如果错误，则返回错误信息
        '     intJJXH                ：交接序号
        '     strFSXH                ：发送批次
        '     strZFJSR               ：转发请求的接收人列表
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
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
        ' 签名确认
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strYjlx              ：要确认的意见类型
        '     strSPR               ：审批人
        '     intMode              ：签批模式：0-单独签，1-共同签
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
        ' 取消签名
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strYjlx              ：要取消的意见类型
        '     strSPR               ：审批人
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
        ' 获取工作流能进行的签批意见列表
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objYjlx              ：签批意见类型+显示名称集合
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
        ' 保存审批意见数据
        '     strErrMsg              ：如果错误，则返回错误信息
        '     intJJXH                ：交接序号
        '     objNewData             ：记录新值(返回保存后的新值)
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
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
        ' 取消intJJXH指定的办理意见
        '     strErrMsg            ：如果错误，则返回错误信息
        '     intJJXH              ：交接序号
        ' 返回
        '     True                 ：成功
        '     False                ：失败
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
        ' 翻译“是否批准”标志
        ' 返回
        '                          ：翻译后的字符串
        '----------------------------------------------------------------
        Public Overridable Function doTranslateSFPZ(ByVal strSFPZ As String) As String
            doTranslateSFPZ = Me.m_objrulesFlowObject.doTranslateSFPZ(strSFPZ)
        End Function

        '----------------------------------------------------------------
        ' 获取“批准”办理标志
        ' 返回
        '                          ：办理标志
        '----------------------------------------------------------------
        Public Overridable Function getPizhunBLBZ() As String
            getPizhunBLBZ = Me.m_objrulesFlowObject.getPizhunBLBZ()
        End Function

        '----------------------------------------------------------------
        ' 获取“保存意见”办理标志
        ' 返回
        '                          ：办理标志
        '----------------------------------------------------------------
        Public Overridable Function getBaocunYijianBLBZ() As String
            getBaocunYijianBLBZ = Me.m_objrulesFlowObject.getBaocunYijianBLBZ()
        End Function

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
        ' 是需要签名确认的审批事宜?
        '     strYjlx              ：审批事宜
        ' 返回
        '     True                 ：需要签名
        '     False                ：不需要签名
        '----------------------------------------------------------------
        Public Overridable Function isQianminTask(ByVal strYjlx As String) As Boolean
            isQianminTask = Me.m_objrulesFlowObject.isQianminTask(strYjlx)
        End Function

        '----------------------------------------------------------------
        ' 是对整个文件签名的审批意见?如果是，返回提醒字符串
        '     strYjlx              ：审批事宜
        ' 返回
        '     True                 ：需要签名
        '     False                ：不需要签名
        '----------------------------------------------------------------
        Public Overridable Function isFileQianminTask( _
            ByVal strYjlx As String, _
            ByRef strPrompt As String) As Boolean
            isFileQianminTask = Me.m_objrulesFlowObject.isFileQianminTask(strYjlx, strPrompt)
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
        ' 获取缺省意见内容
        '     strYjlx              ：审批事宜
        ' 返回
        '                          ：办理标志
        '----------------------------------------------------------------
        Public Overridable Function getDefaultYJNR(ByVal strYJLX As String) As String
            getDefaultYJNR = Me.m_objrulesFlowObject.getDefaultYJNR(strYJLX)
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
        ' 保存协办标志数据(公文_B_交接)
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserXM              ：人员名称
        '     strNewXBBZ             ：协办标志
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
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
            getYijiaoData = Xydc.Platform.BusinessRules.rulesFlowObject.getYijiaoData(strErrMsg, strUserId, strPassword, strYJR, strJSR, strWhere, objYijiaoData)
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
            getJieshouData = Xydc.Platform.BusinessRules.rulesFlowObject.getJieshouData(strErrMsg, strUserId, strPassword, strYJR, strJSR, strWhere, objJieshouData)
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
            getYjrListData = Xydc.Platform.BusinessRules.rulesFlowObject.getYjrListData(strErrMsg, strUserId, strPassword, strJSR, objYjrData)
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
            doFile_Yijiao = Xydc.Platform.BusinessRules.rulesFlowObject.doFile_Yijiao(strErrMsg, strUserId, strPassword, strYJR, strJSR, strWJBS, strYJSM)
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
            getWJLX = Xydc.Platform.BusinessRules.rulesFlowObject.getWJLX(strErrMsg, strUserId, strPassword, strWJBS, strWJLX)
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
            doFile_Jieshou = Xydc.Platform.BusinessRules.rulesFlowObject.doFile_Jieshou(strErrMsg, strUserId, strPassword, strYJR, strJSR, strWJBS)
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

            With Me.m_objrulesFlowObject
                doUpdateWJXX = .doUpdateWJXX(strErrMsg, objNewData)
            End With

        End Function
    End Class

End Namespace
