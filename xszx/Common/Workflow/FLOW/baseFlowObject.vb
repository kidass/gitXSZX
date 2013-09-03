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
    ' 命名空间：Xydc.Platform.Common.Workflow
    ' 类名    ：BaseFlowObject
    '
    ' 功能描述： 
    '     定义与FlowObject相关的参数
    '----------------------------------------------------------------
    Public Class BaseFlowObject
        Implements IDisposable

        '对象类型、对象创建接口注册器(所有对象共享)
        Private Shared m_objFlowTypeEnum As System.Collections.Specialized.ListDictionary

        '对象属性
        Private m_strFlowType As String          '对象类型码(必须唯一)
        Private m_strFlowTypeName As String      '对象类型码名称(必须唯一)
        Private m_strFlowTypeBLLX As String      '对象类型码对应的办理类型

        '子类可以访问
        Private m_strWJBS As String              '文件标识
        Private m_strLSH As String               '文件流水号
        Private m_strStatus As String            '文件办理状态
        Private m_strPZR As String               '文件联合批准人
        Private m_objPZRQ As DateTime            '文件最后批准日期
        Private m_intDDSZ As Integer             '本文件不受流转控制条件限制

        '文件处理状态
        Public Const FILESTATUS_ZJB As String = "正在办理"
        Public Const FILESTATUS_YWC As String = "办理完毕"
        Public Const FILESTATUS_YTB As String = "暂缓办理"
        Public Const FILESTATUS_YZF As String = "文件作废"
        Public Const FILESTATUS_YQF As String = "已经签发"
        Public Const FILESTATUS_YQP As String = "已经签批"
        Public Const FILESTATUS_YPS As String = "已经批示"
        Public Const FILESTATUS_YDJ As String = "已拿文号"
        Public Const FILESTATUS_YDG As String = "已经定稿"

        '交接处理状态
        Public Const TASKSTATUS_WJS As String = "没有接收"
        Public Const TASKSTATUS_ZJB As String = "正在办理"
        Public Const TASKSTATUS_YTB As String = "暂缓办理"
        Public Const TASKSTATUS_YWC As String = "办理完毕"
        Public Const TASKSTATUS_BYB As String = "不用办理"
        Public Const TASKSTATUS_YYD As String = "已经阅读"
        Public Const TASKSTATUS_BSH As String = "文件被收回"
        Public Const TASKSTATUS_BTH As String = "文件被退回"

        '载体类型
        Public Const FILEZTLX_ZZ As String = "纸"
        Public Const FILEZTLX_DZ As String = "电子"
        Public Const FILEZTLX_ZD As String = "纸+电子"

        '工作流一般事宜
        Public Const TASK_HFCL As String = "回复处理"
        Public Const TASK_HFTZ As String = "回复通知"
        Public Const TASK_BYQQ As String = "补阅请求"
        Public Const TASK_BYTZ As String = "补阅通知"
        Public Const TASK_THCL As String = "退回处理"
        Public Const TASK_THTZ As String = "退回通知"
        Public Const TASK_SHCL As String = "收回处理"
        Public Const TASK_SHTZ As String = "收回通知"
        Public Const TASK_SMCL As String = "司秘处理"
        Public Const TASK_MSCL As String = "秘书处理"
        Public Const TASK_LDCL As String = "审批文件"
        Public Const TASK_XGCL As String = "相关处理"
        Public Const TASK_CBWJ As String = "催办文件"
        Public Const TASK_DBWJ As String = "督办文件"

        '强行编辑说明
        Public Const LOGO_QXBJ As String = "文件定稿后强制进行修改操作！"




        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Protected Sub New()
            MyBase.New()

            '初始化工作流对象通用属性
            m_strFlowType = ""
            m_strWJBS = ""
            m_strLSH = ""
            m_strStatus = ""
            m_intDDSZ = 0
            m_strPZR = ""
            m_objPZRQ = Nothing

        End Sub

        '----------------------------------------------------------------
        ' 保护构造函数
        '----------------------------------------------------------------
        Protected Sub New(ByVal strFlowType As String)
            Me.New()
            '注册检查
            Dim strType As String
            Try
                strType = strFlowType
                If m_objFlowTypeEnum Is Nothing Then
                    Throw New Exception("错误：请用[Create]方法创建[" + strFlowType + "]工作流！")
                Else
                    If m_objFlowTypeEnum.Item(strType) Is Nothing Then
                        Throw New Exception("错误：请用[Create]方法创建[" + strFlowType + "]工作流！")
                    End If
                End If
            Catch ex As Exception
                Throw ex
            End Try

        End Sub




        '----------------------------------------------------------------
        ' 析构函数
        '----------------------------------------------------------------
        Public Overridable Sub Dispose() Implements System.IDisposable.Dispose
            Dispose(True)
        End Sub

        '----------------------------------------------------------------
        ' 释放本身资源
        '----------------------------------------------------------------
        Protected Sub Dispose(ByVal disposing As Boolean)
            If (Not disposing) Then
                Exit Sub
            End If
        End Sub

        '----------------------------------------------------------------
        ' 安全释放本身资源
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
        ' BaseFlow对象注册器
        '     strFlowType          ：工作流类型代码
        '     objCreator           ：工作流对象IBaseFlowCreate接口
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Shared Function RegisterFlowType( _
            ByVal strFlowType As String, _
            ByVal objCreator As Xydc.Platform.Common.Workflow.IBaseFlowCreate) As Boolean

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
                If objCreator Is Nothing Then
                    Throw New Exception("错误：[IBaseFlowCreate]不能为空！")
                End If

                '生成类型汇集器
                If m_objFlowTypeEnum Is Nothing Then
                    m_objFlowTypeEnum = New System.Collections.Specialized.ListDictionary
                End If

                '检查类型是否存在
                If Not (m_objFlowTypeEnum.Item(strFlowType) Is Nothing) Then
                    Exit Try
                End If

                '注册
                m_objFlowTypeEnum.Add(strFlowType, objCreator)

                RegisterFlowType = True

            Catch ex As Exception
                Throw ex
            End Try

        End Function

        '----------------------------------------------------------------
        ' 创建BaseFlow
        '     strFlowType          ：工作流类型代码
        ' 返回
        '                          ：Xydc.Platform.Common.Workflow.BaseFlowObject对象
        '----------------------------------------------------------------
        Public Shared Function Create(ByVal strFlowType As String) As Xydc.Platform.Common.Workflow.BaseFlowObject

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

                '注册已实现的BaseFlow
                Dim strType As String

               
                '***********************************************************************************************
                '督查单
                'strType = Xydc.Platform.Common.Workflow.BaseFlowDuchadan.FLOWCODE
                'If m_objFlowTypeEnum Is Nothing Then
                '    RegisterFlowType(strType, New Xydc.Platform.Common.Workflow.BaseFlowDuchadanCreator)
                'Else
                '    If m_objFlowTypeEnum.Item(strType) Is Nothing Then
                '        RegisterFlowType(strType, New Xydc.Platform.Common.Workflow.BaseFlowDuchadanCreator)
                '    End If
                'End If



                '获取接口
                Dim objCreator As Object
                objCreator = m_objFlowTypeEnum.Item(strFlowType)
                If objCreator Is Nothing Then
                    Throw New Exception("错误：[" + strFlowType + "]不支持！")
                End If
                Dim objIBaseFlowCreate As Xydc.Platform.Common.Workflow.IBaseFlowCreate
                objIBaseFlowCreate = CType(objCreator, Xydc.Platform.Common.Workflow.IBaseFlowCreate)
                If objIBaseFlowCreate Is Nothing Then
                    Throw New Exception("错误：[" + strFlowType + "]不支持！")
                End If

                '利用接口创建对象
                Create = objIBaseFlowCreate.Create(strFlowType)

                '自动设置类型属性
                Create.m_strFlowType = strFlowType

            Catch ex As Exception
                Throw ex
            End Try

        End Function




        '----------------------------------------------------------------
        ' FlowType属性
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
        ' FlowTypeName属性
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
        ' FlowTypeBLLX属性
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
        ' WJBS属性
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
        ' LSH属性
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
        ' Status属性
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
        ' PZR属性
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
        ' PZRQ属性
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
        ' DDSZ属性
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
        ' 获取“已经办理完毕”的交接状态SQL值列表 - 正常办理完成
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
        ' 获取“已经办理完毕”的交接状态SQL值列表 - 所有办理完成的状态
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
        ' 获取“已经暂缓办理”的交接状态SQL值列表
        '----------------------------------------------------------------
        Public Shared ReadOnly Property TaskStatusYTBList() As String
            Get
                TaskStatusYTBList = ""
                TaskStatusYTBList = TaskStatusYTBList + "'" + TASKSTATUS_YTB + "'"
            End Get
        End Property

        '----------------------------------------------------------------
        ' 获取“没有接收”的交接状态SQL值列表
        '----------------------------------------------------------------
        Public Shared ReadOnly Property TaskStatusWJSList() As String
            Get
                TaskStatusWJSList = ""
                TaskStatusWJSList = TaskStatusWJSList + "'" + TASKSTATUS_WJS + "'"
            End Get
        End Property

        '----------------------------------------------------------------
        ' 获取“正在办理”的交接状态SQL值列表
        '----------------------------------------------------------------
        Public Shared ReadOnly Property TaskStatusZJBList() As String
            Get
                TaskStatusZJBList = ""
                'TaskStatusZJBList = TaskStatusZJBList + "'" + TASKSTATUS_WJS + "'"

                TaskStatusZJBList = TaskStatusZJBList + "'" + TASKSTATUS_ZJB + "'"

            End Get
        End Property




        '----------------------------------------------------------------
        ' 获取“主动通知”的交接状态SQL值列表
        '----------------------------------------------------------------
        Public Overridable ReadOnly Property TaskStatusZDTZList() As String
            Get
                TaskStatusZDTZList = ""
                TaskStatusZDTZList = TaskStatusZDTZList + Xydc.Platform.Common.Data.FlowData.YJJH_ZHUDONGBUYUE.ToString()
            End Get
        End Property

        '----------------------------------------------------------------
        ' 获取“审批事宜”的办理子类SQL值列表
        '----------------------------------------------------------------
        Public Overridable ReadOnly Property TaskBlzlSPSYList() As String
            Get
                TaskBlzlSPSYList = ""
                TaskBlzlSPSYList = TaskBlzlSPSYList + " " + "'" + TASK_LDCL + "'"
                TaskBlzlSPSYList = TaskBlzlSPSYList + "," + "'" + TASK_XGCL + "'"
            End Get
        End Property

        '----------------------------------------------------------------
        ' 获取“补阅事宜”的办理子类SQL值列表
        '----------------------------------------------------------------
        Public Overridable ReadOnly Property TaskBlzlBYSYList() As String
            Get
                TaskBlzlBYSYList = ""
                TaskBlzlBYSYList = TaskBlzlBYSYList + " " + "'" + TASK_BYQQ + "'"
                TaskBlzlBYSYList = TaskBlzlBYSYList + "," + "'" + TASK_BYTZ + "'"
            End Get
        End Property




        '----------------------------------------------------------------
        ' 获取“已经办理完毕”的文件状态SQL值列表 - 正常办理完毕的状态
        '----------------------------------------------------------------
        Public Shared ReadOnly Property FileStatusYWCList() As String
            Get
                FileStatusYWCList = ""
                FileStatusYWCList = FileStatusYWCList + " " + "'" + FILESTATUS_YWC + "'"
            End Get
        End Property

        '----------------------------------------------------------------
        ' 获取“已经办理完毕”的文件状态SQL值列表 - 所有办理完毕的状态
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
        ' 获取“已经暂缓办理”的文件状态SQL值列表
        '----------------------------------------------------------------
        Public Shared ReadOnly Property FileStatusYTBList() As String
            Get
                FileStatusYTBList = ""
                FileStatusYTBList = FileStatusYTBList + "'" + FILESTATUS_YTB + "'"
            End Get
        End Property

        '----------------------------------------------------------------
        ' 获取“已经作废”的文件状态SQL值列表
        '----------------------------------------------------------------
        Public Shared ReadOnly Property FileStatusYZFList() As String
            Get
                FileStatusYZFList = ""
                FileStatusYZFList = FileStatusYZFList + "'" + FILESTATUS_YZF + "'"
            End Get
        End Property

        '----------------------------------------------------------------
        ' 获取“已经签发”的文件状态SQL值列表
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
        ' 获取“已经定稿”的文件状态SQL值列表
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