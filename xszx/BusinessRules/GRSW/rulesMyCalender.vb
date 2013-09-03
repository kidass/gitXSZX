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
    ' 类名    ：rulesMyCalender
    '
    ' 功能描述： 
    '     提供对“我的日程安排”模块涉及的业务逻辑层操作
    '----------------------------------------------------------------
    Public Class rulesMyCalender
        Implements System.IDisposable

        Private m_objdacMyCalender As Xydc.Platform.DataAccess.dacMyCalender









        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
            m_objdacMyCalender = New Xydc.Platform.DataAccess.dacMyCalender
        End Sub

        '----------------------------------------------------------------
        ' 虚拟析构函数
        '----------------------------------------------------------------
        Public Sub Dispose() Implements System.IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(True)
        End Sub

        '----------------------------------------------------------------
        ' 析构函数重载
        '----------------------------------------------------------------
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If (Not disposing) Then
                Exit Sub
            End If
            If Not (m_objdacMyCalender Is Nothing) Then
                m_objdacMyCalender.Dispose()
                m_objdacMyCalender = Nothing
            End If
        End Sub

        '----------------------------------------------------------------
        ' 安全释放本身资源
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessRules.rulesMyCalender)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub










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
        Public Function doExportToExcel( _
            ByRef strErrMsg As String, _
            ByVal objDataSet As System.Data.DataSet, _
            ByVal strExcelFile As String, _
            Optional ByVal strMacroName As String = "", _
            Optional ByVal strMacroValue As String = "") As Boolean

            Try
                With m_objdacMyCalender
                    doExportToExcel = .doExportToExcel(strErrMsg, objDataSet, strExcelFile, strMacroName, strMacroValue)
                End With
            Catch ex As Exception
                doExportToExcel = False
                strErrMsg = ex.Message
            End Try

        End Function




        '----------------------------------------------------------------
        ' 获取给定人员的日程安排信息
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strSYZ               ：所有者（人员代码）
        '     strWhere             ：搜索条件
        '     objCalenderData      ：返回数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strSYZ As String, _
            ByVal strWhere As String, _
            ByRef objCalenderData As Xydc.Platform.Common.Data.grswMyCalenderData) As Boolean

            Try
                With m_objdacMyCalender
                    getDataSet = .getDataSet(strErrMsg, strUserId, strPassword, strSYZ, strWhere, objCalenderData)
                End With
            Catch ex As Exception
                getDataSet = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取给定编号的日程安排信息
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     intBH                ：编号
        '     objCalenderData      ：返回数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intBH As Integer, _
            ByRef objCalenderData As Xydc.Platform.Common.Data.grswMyCalenderData) As Boolean

            Try
                With m_objdacMyCalender
                    getDataSet = .getDataSet(strErrMsg, strUserId, strPassword, intBH, objCalenderData)
                End With
            Catch ex As Exception
                getDataSet = False
                strErrMsg = ex.Message
            End Try

        End Function





        '----------------------------------------------------------------
        ' 保存个人日志数据记录(整个事务完成)
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserId              ：用户标识
        '     strPassword            ：用户密码
        '     objNewData             ：记录新值(返回保存后的新值)
        '     objOldData             ：记录旧值
        '     objenumEditType        ：编辑类型
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function doSave( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Try
                With m_objdacMyCalender
                    doSave = .doSave(strErrMsg, strUserId, strPassword, objNewData, objOldData, objenumEditType)
                End With
            Catch ex As Exception
                doSave = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 删除“个人_B_个人日志”的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     intBH                ：编号
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDelete( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intBH As Integer) As Boolean

            Try
                With m_objdacMyCalender
                    doDelete = .doDelete(strErrMsg, strUserId, strPassword, intBH)
                End With
            Catch ex As Exception
                doDelete = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 设置日程已经办完
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     intBH                ：编号
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doSetComplete( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intBH As Integer) As Boolean

            Try
                With m_objdacMyCalender
                    doSetComplete = .doSetComplete(strErrMsg, strUserId, strPassword, intBH)
                End With
            Catch ex As Exception
                doSetComplete = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 清除日程的提醒设置
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     intBH                ：编号
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doClearTixing( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intBH As Integer) As Boolean

            Try
                With m_objdacMyCalender
                    doClearTixing = .doClearTixing(strErrMsg, strUserId, strPassword, intBH)
                End With
            Catch ex As Exception
                doClearTixing = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 设置日程的提醒
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     intBH                ：编号
        '     intHour              ：小时数
        '     intMinute            ：分钟数
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doSetTixing( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intBH As Integer, _
            ByVal intHour As Integer, _
            ByVal intMinute As Integer) As Boolean

            Try
                With m_objdacMyCalender
                    doSetTixing = .doSetTixing(strErrMsg, strUserId, strPassword, intBH, intHour, intMinute)
                End With
            Catch ex As Exception
                doSetTixing = False
                strErrMsg = ex.Message
            End Try

        End Function




        '----------------------------------------------------------------
        ' 获取新的排序号
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strSYZ               ：所有者
        '     intPXH               ：新排序号
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getNewPXH( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strSYZ As String, _
            ByRef intPXH As Integer) As Boolean

            Try
                With m_objdacMyCalender
                    getNewPXH = .getNewPXH(strErrMsg, strUserId, strPassword, strSYZ, intPXH)
                End With
            Catch ex As Exception
                getNewPXH = False
                strErrMsg = ex.Message
            End Try

        End Function





        '----------------------------------------------------------------
        ' 获取需要提醒的事宜数目(以当前时间为界)
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserId              ：用户标识
        '     strPassword            ：用户密码
        '     strSYZ                 ：所有者
        '     intCountTXSY           ：需要提醒的事宜数目
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function getCountTXSY( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strSYZ As String, _
            ByRef intCountTXSY As Integer) As Boolean

            Try
                With m_objdacMyCalender
                    getCountTXSY = .getCountTXSY(strErrMsg, strUserId, strPassword, strSYZ, intCountTXSY)
                End With
            Catch ex As Exception
                getCountTXSY = False
                strErrMsg = ex.Message
            End Try

        End Function

    End Class 'rulesMyCalender

End Namespace 'Xydc.Platform.BusinessRules
