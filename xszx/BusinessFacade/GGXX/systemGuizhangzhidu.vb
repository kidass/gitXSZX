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
    ' 类名    ：systemGuizhangzhidu
    '
    ' 功能描述： 
    '     提供对“规章制度”模块涉及的表现层操作
    '----------------------------------------------------------------
    Public Class systemGuizhangzhidu
        Implements System.IDisposable

        Private m_objrulesGuizhangzhidu As Xydc.Platform.BusinessRules.rulesGuizhangzhidu








        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
            m_objrulesGuizhangzhidu = New Xydc.Platform.BusinessRules.rulesGuizhangzhidu
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
            If Not (m_objrulesGuizhangzhidu Is Nothing) Then
                m_objrulesGuizhangzhidu.Dispose()
                m_objrulesGuizhangzhidu = Nothing
            End If
        End Sub

        '----------------------------------------------------------------
        ' 安全释放本身资源
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.systemGuizhangzhidu)
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
                With m_objrulesGuizhangzhidu
                    doExportToExcel = .doExportToExcel(strErrMsg, objDataSet, strExcelFile, strMacroName, strMacroValue)
                End With
            Catch ex As Exception
                doExportToExcel = False
                strErrMsg = ex.Message
            End Try

        End Function





        '----------------------------------------------------------------
        ' 获取顶级制度数据(按“排序号”升序)
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     objGuizhangzhiduData        ：信息数据集
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_Tree( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef objGuizhangzhiduData As Xydc.Platform.Common.Data.ggxxGuizhangzhiduData) As Boolean

            Try
                With m_objrulesGuizhangzhidu
                    getDataSet_Tree = .getDataSet_Tree(strErrMsg, strUserId, strPassword, objGuizhangzhiduData)
                End With
            Catch ex As Exception
                getDataSet_Tree = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取指定编号的下级制度数据(按“排序号”升序)
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     intSJBH                     ：上级编号
        '     objGuizhangzhiduData        ：信息数据集
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_Tree( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intSJBH As Integer, _
            ByRef objGuizhangzhiduData As Xydc.Platform.Common.Data.ggxxGuizhangzhiduData) As Boolean

            Try
                With m_objrulesGuizhangzhidu
                    getDataSet_Tree = .getDataSet_Tree(strErrMsg, strUserId, strPassword, intSJBH, objGuizhangzhiduData)
                End With
            Catch ex As Exception
                getDataSet_Tree = False
                strErrMsg = ex.Message
            End Try

        End Function





        '----------------------------------------------------------------
        ' 删除指定数据(指定记录)-同时删除下级数据
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
                With m_objrulesGuizhangzhidu
                    doDelete = .doDelete(strErrMsg, strUserId, strPassword, intBH)
                End With
            Catch ex As Exception
                doDelete = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 保存交流记录数据记录(整个事务完成)
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
                With m_objrulesGuizhangzhidu
                    doSave = .doSave(strErrMsg, strUserId, strPassword, objNewData, objOldData, objenumEditType)
                End With
            Catch ex As Exception
                doSave = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 更改排序号
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     intBH                ：编号
        '     intPXH               ：新排序号
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doUpdatePXH( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intBH As Integer, _
            ByVal intPXH As Integer) As Boolean

            Try
                With m_objrulesGuizhangzhidu
                    doUpdatePXH = .doUpdatePXH(strErrMsg, strUserId, strPassword, intBH, intPXH)
                End With
            Catch ex As Exception
                doUpdatePXH = False
                strErrMsg = ex.Message
            End Try

        End Function






        '----------------------------------------------------------------
        ' 获取指定编号的制度数据
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     intBH                       ：编号
        '     objGuizhangzhiduData        ：信息数据集
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intBH As Integer, _
            ByRef objGuizhangzhiduData As Xydc.Platform.Common.Data.ggxxGuizhangzhiduData) As Boolean

            Try
                With m_objrulesGuizhangzhidu
                    getDataSet = .getDataSet(strErrMsg, strUserId, strPassword, intBH, objGuizhangzhiduData)
                End With
            Catch ex As Exception
                getDataSet = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取新的排序号
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     intSJBH              ：上级编号
        '     intPXH               ：新排序号
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getNewPXH( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intSJBH As Integer, _
            ByRef intPXH As Integer) As Boolean

            Try
                With m_objrulesGuizhangzhidu
                    getNewPXH = .getNewPXH(strErrMsg, strUserId, strPassword, intSJBH, intPXH)
                End With
            Catch ex As Exception
                getNewPXH = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据intBH获取上级编号
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     intBH                       ：编号
        '     intSJBH                     ：(返回)上级编号
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getSjbhByBh( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intBH As Integer, _
            ByRef intSJBH As Integer) As Boolean

            Try
                With m_objrulesGuizhangzhidu
                    getSjbhByBh = .getSjbhByBh(strErrMsg, strUserId, strPassword, intBH, intSJBH)
                End With
            Catch ex As Exception
                getSjbhByBh = False
                strErrMsg = ex.Message
            End Try

        End Function

    End Class

End Namespace
