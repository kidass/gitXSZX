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
    ' 命名空间：Xydc.Platform.BusinessRules
    ' 类名    ：rulesEditWorkFlow
    '
    ' 功能描述： 
    '   　提供对“所有工作流”信息处理的业务规则
    '----------------------------------------------------------------
    Public Class rulesEditWorkFlow
        Implements IDisposable

        Private m_objdacEditWorkFlow As Xydc.Platform.DataAccess.dacEditWorkFlow









        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
            m_objdacEditWorkFlow = New Xydc.Platform.DataAccess.dacEditWorkFlow
        End Sub

        '----------------------------------------------------------------
        ' 析构函数(子类可重载)
        '----------------------------------------------------------------
        Public Sub Dispose() Implements System.IDisposable.Dispose
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
            Xydc.Platform.DataAccess.dacEditWorkFlow.SafeRelease(m_objdacEditWorkFlow)
        End Sub

        '----------------------------------------------------------------
        ' 安全释放本身资源
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessRules.rulesEditWorkFlow)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub











        '----------------------------------------------------------------
        ' 获取“公文_V_全部审批文件新”完全数据的数据集(以“拟稿日期”升序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWhere             ：搜索字符串
        '     objDataSet_WFS       ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_WFS( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objDataSet_WFS As Xydc.Platform.Common.Data.FlowData) As Boolean
            With m_objdacEditWorkFlow
                getDataSet_WFS = .getDataSet_WFS(strErrMsg, strUserId, strPassword, strWhere, objDataSet_WFS)
            End With
        End Function

        '--------------------------------------------------------------
        ' 删除“公文_B_交接”的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '更改说明
        '     
        '----------------------------------------------------------------
        Public Function doDeleteGWJJData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacEditWorkFlow
                    doDeleteGWJJData = .doDeleteGWJJData(strErrMsg, strUserId, strPassword, objOldData)
                End With
            Catch ex As Exception
                doDeleteGWJJData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 解除操作人员目前存在的文件编辑封锁
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strCzyId             ：操作员ID
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        ' 更改说明：
        '      2009-03-12 创建
        '----------------------------------------------------------------
        Public Function doUnLockAll( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strCzyId As String) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacEditWorkFlow
                    doUnLockAll = .doUnLockAll(strErrMsg, strUserId, strPassword, strCzyId)
                End With
            Catch ex As Exception
                doUnLockAll = False
                strErrMsg = ex.Message
            End Try

        End Function

    End Class 'rulesEditWorkFlow

End Namespace 'Xydc.Platform.BusinessRules
