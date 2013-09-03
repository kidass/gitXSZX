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
    ' 类名    ：rulesSijuzhanganpai
    '
    ' 功能描述： 
    '     提供对“司局长京外活动安排”模块涉及的业务逻辑层操作
    '----------------------------------------------------------------
    Public Class rulesSijuzhanganpai
        Implements System.IDisposable

        Private m_objdacSijuzhanganpai As Xydc.Platform.DataAccess.dacSijuzhanganpai










        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
            m_objdacSijuzhanganpai = New Xydc.Platform.DataAccess.dacSijuzhanganpai
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
            If Not (m_objdacSijuzhanganpai Is Nothing) Then
                m_objdacSijuzhanganpai.Dispose()
                m_objdacSijuzhanganpai = Nothing
            End If
        End Sub

        '----------------------------------------------------------------
        ' 安全释放本身资源
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessRules.rulesSijuzhanganpai)
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
                With m_objdacSijuzhanganpai
                    doExportToExcel = .doExportToExcel(strErrMsg, objDataSet, strExcelFile, strMacroName, strMacroValue)
                End With
            Catch ex As Exception
                doExportToExcel = False
                strErrMsg = ex.Message
            End Try

        End Function


        '----------------------------------------------------------------
        ' 获取司局长活动安排数据（按“排序”升序）- 列表显示模式
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     strWhere                    ：搜索字符串
        '     objSijuzhanganpaiData         ：信息数据集
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objSijuzhanganpaiData As Xydc.Platform.Common.Data.ggxxSijuzhanganpaiData) As Boolean

            Try
                With m_objdacSijuzhanganpai
                    getDataSet = .getDataSet(strErrMsg, strUserId, strPassword, strWhere, objSijuzhanganpaiData)
                End With
            Catch ex As Exception
                getDataSet = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取[序号=intXH]的司局长活动安排数据
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     intXH                       ：公告序号
        '     objSijuzhanganpaiData        ：信息数据集
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intXH As Integer, _
            ByRef objSijuzhanganpaiData As Xydc.Platform.Common.Data.ggxxSijuzhanganpaiData) As Boolean

            Try
                With m_objdacSijuzhanganpai
                    getDataSet = .getDataSet(strErrMsg, strUserId, strPassword, intXH, objSijuzhanganpaiData)
                End With
            Catch ex As Exception
                getDataSet = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取司局长活动安排数据（按“组织代码”+“排序”升序）
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     objDate                     ：指定日期
        '     objSijuzhanganpaiData         ：信息数据集
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objDate As System.DateTime, _
            ByRef objSijuzhanganpaiData As Xydc.Platform.Common.Data.ggxxSijuzhanganpaiData) As Boolean

            Try
                With m_objdacSijuzhanganpai
                    getDataSet = .getDataSet(strErrMsg, strUserId, strPassword, objDate, objSijuzhanganpaiData)
                End With
            Catch ex As Exception
                getDataSet = False
                strErrMsg = ex.Message
            End Try

        End Function




        '----------------------------------------------------------------
        ' 删除司局长活动安排
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     intXH                ：公告序号
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDelete( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intXH As Integer) As Boolean

            Try
                With m_objdacSijuzhanganpai
                    doDelete = .doDelete(strErrMsg, strUserId, strPassword, intXH)
                End With
            Catch ex As Exception
                doDelete = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 保存司局长活动安排数据记录(整个事务完成)
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
                With m_objdacSijuzhanganpai
                    doSave = .doSave(strErrMsg, strUserId, strPassword, objNewData, objOldData, objenumEditType)
                End With
            Catch ex As Exception
                doSave = False
                strErrMsg = ex.Message
            End Try

        End Function



        '----------------------------------------------------------------
        ' 获取新的排序
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strRQ                ：指定日期
        '     strNewPX             ：(返回)新排序
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getNewPX( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRQ As String, _
            ByRef strNewPX As String) As Boolean

            Try
                With m_objdacSijuzhanganpai
                    getNewPX = .getNewPX(strErrMsg, strUserId, strPassword, strRQ, strNewPX)
                End With
            Catch ex As Exception
                getNewPX = False
                strErrMsg = ex.Message
            End Try

        End Function


    End Class 'rulesSijuzhanganpai

End Namespace 'Xydc.Platform.BusinessRules

