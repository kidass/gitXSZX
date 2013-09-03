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
    ' 类名    ：systemLingdaoanpai
    '
    ' 功能描述： 
    '     提供对“领导活动安排”模块涉及的表现层操作
    '----------------------------------------------------------------
    Public Class systemLingdaoanpai
        Implements System.IDisposable

        Private m_objrulesLingdaoanpai As Xydc.Platform.BusinessRules.rulesLingdaoanpai








        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
            m_objrulesLingdaoanpai = New Xydc.Platform.BusinessRules.rulesLingdaoanpai
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
            If Not (m_objrulesLingdaoanpai Is Nothing) Then
                m_objrulesLingdaoanpai.Dispose()
                m_objrulesLingdaoanpai = Nothing
            End If
        End Sub

        '----------------------------------------------------------------
        ' 安全释放本身资源
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.systemLingdaoanpai)
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
                With m_objrulesLingdaoanpai
                    doExportToExcel = .doExportToExcel(strErrMsg, objDataSet, strExcelFile, strMacroName, strMacroValue)
                End With
            Catch ex As Exception
                doExportToExcel = False
                strErrMsg = ex.Message
            End Try

        End Function





        '----------------------------------------------------------------
        ' 获取领导活动安排数据（按“排序”升序）- 列表显示模式
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     strWhere                    ：搜索字符串
        '     objLingdaoanpaiData         ：信息数据集
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objLingdaoanpaiData As Xydc.Platform.Common.Data.ggxxLingdaoanpaiData) As Boolean

            Try
                With m_objrulesLingdaoanpai
                    getDataSet = .getDataSet(strErrMsg, strUserId, strPassword, strWhere, objLingdaoanpaiData)
                End With
            Catch ex As Exception
                getDataSet = False
                strErrMsg = ex.Message
            End Try

        End Function


        '----------------------------------------------------------------
        ' 获取领导活动安排数据（按“排序”升序）- 列表显示模式
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     strWhere                    ：搜索字符串
        '     objLingdaoanpaiData         ：信息数据集
        '     blnNone                     :重载用
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objLingdaoanpaiData As Xydc.Platform.Common.Data.ggxxLingdaoanpaiData, _
            ByVal blnNone As Boolean) As Boolean

            Try
                With m_objrulesLingdaoanpai
                    getDataSet = .getDataSet(strErrMsg, strUserId, strPassword, strWhere, objLingdaoanpaiData, blnNone)
                End With
            Catch ex As Exception
                getDataSet = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取[序号=intXH]的领导活动安排数据
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     intXH                       ：公告序号
        '     objLingdaoanpaiData        ：信息数据集
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intXH As Integer, _
            ByRef objLingdaoanpaiData As Xydc.Platform.Common.Data.ggxxLingdaoanpaiData) As Boolean

            Try
                With m_objrulesLingdaoanpai
                    getDataSet = .getDataSet(strErrMsg, strUserId, strPassword, intXH, objLingdaoanpaiData)
                End With
            Catch ex As Exception
                getDataSet = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取领导活动安排数据（按“组织代码”+“排序”升序）
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     objDate                     ：指定日期
        '     objLingdaoanpaiData         ：信息数据集
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objDate As System.DateTime, _
            ByRef objLingdaoanpaiData As Xydc.Platform.Common.Data.ggxxLingdaoanpaiData) As Boolean

            Try
                With m_objrulesLingdaoanpai
                    getDataSet = .getDataSet(strErrMsg, strUserId, strPassword, objDate, objLingdaoanpaiData)
                End With
            Catch ex As Exception
                getDataSet = False
                strErrMsg = ex.Message
            End Try

        End Function





        '----------------------------------------------------------------
        ' 删除领导活动安排
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
                With m_objrulesLingdaoanpai
                    doDelete = .doDelete(strErrMsg, strUserId, strPassword, intXH)
                End With
            Catch ex As Exception
                doDelete = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 复制领导活动安排，从[strFromRQ]复制到[strToRQ]
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strFromRQ            ：要复制的安排日期
        '     strToRQ              ：复制到的安排日期
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doCopy( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strFromRQ As String, _
            ByVal strToRQ As String) As Boolean

            Try
                With m_objrulesLingdaoanpai
                    doCopy = .doCopy(strErrMsg, strUserId, strPassword, strFromRQ, strToRQ)
                End With
            Catch ex As Exception
                doCopy = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 保存领导活动安排数据记录(整个事务完成)
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
                With m_objrulesLingdaoanpai
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
                With m_objrulesLingdaoanpai
                    getNewPX = .getNewPX(strErrMsg, strUserId, strPassword, strRQ, strNewPX)
                End With
            Catch ex As Exception
                getNewPX = False
                strErrMsg = ex.Message
            End Try

        End Function


        '----------------------------------------------------------------
        ' 更新领导活动安排的排序
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     intSrcIndex          ：原排序
        '     intDesIndex          ：目标排序
        '     intSrcXH             ：原序号
        '     intDesXh             ：目标序号
        ' 返回
        '     True                 ：成功
        '     False                ：失败

        '----------------------------------------------------------------
        Public Function doUpdatePX( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intSrcIndex As Integer, _
            ByVal intDesIndex As Integer, _
            ByVal intSrcXH As Integer, _
            ByVal intDesXH As Integer) As Boolean

            Try
                With m_objrulesLingdaoanpai
                    doUpdatePX = .doUpdatePX(strErrMsg, strUserId, strPassword, intSrcIndex, intDesIndex, intSrcXH, intDesXH)
                End With
            Catch ex As Exception
                doUpdatePX = False
                strErrMsg = ex.Message
            End Try

        End Function

    End Class

End Namespace
