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
    ' 类名    ：rulesLuntan
    '
    ' 功能描述： 
    '     提供对“内部论坛”模块涉及的业务逻辑层操作
    '----------------------------------------------------------------
    Public Class rulesLuntan
        Implements System.IDisposable

        Private m_objdacLuntan As Xydc.Platform.DataAccess.dacLuntan









        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
            m_objdacLuntan = New Xydc.Platform.DataAccess.dacLuntan
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
            If Not (m_objdacLuntan Is Nothing) Then
                m_objdacLuntan.Dispose()
                m_objdacLuntan = Nothing
            End If
        End Sub

        '----------------------------------------------------------------
        ' 安全释放本身资源
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessRules.rulesLuntan)
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
                With m_objdacLuntan
                    doExportToExcel = .doExportToExcel(strErrMsg, objDataSet, strExcelFile, strMacroName, strMacroValue)
                End With
            Catch ex As Exception
                doExportToExcel = False
                strErrMsg = ex.Message
            End Try

        End Function





        '----------------------------------------------------------------
        ' 判断strRYDM是否有效？
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     strRYDM                     ：人员代码
        '     blnValid                    ：（返回）=True有效，=False停用
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function isValid( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRYDM As String, _
            ByRef blnValid As Boolean) As Boolean

            Try
                With m_objdacLuntan
                    isValid = .isValid(strErrMsg, strUserId, strPassword, strRYDM, blnValid)
                End With
            Catch ex As Exception
                isValid = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 判断strRYDM是否注册？
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     strRYDM                     ：人员代码
        '     blnRegister                 ：（返回）=True已注册，=False未注册
        '     strRYNC                     ：如果已注册，返回人员昵称
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function isRegistered( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRYDM As String, _
            ByRef blnRegister As Boolean, _
            ByRef strRYNC As String) As Boolean

            Try
                With m_objdacLuntan
                    isRegistered = .isRegistered(strErrMsg, strUserId, strPassword, strRYDM, blnRegister, strRYNC)
                End With
            Catch ex As Exception
                isRegistered = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 注册交流用户
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserId              ：用户标识
        '     strPassword            ：用户密码
        '     strRYDM                ：人员代码
        '     strRYNC                ：人员昵称
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function doRegister( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRYDM As String, _
            ByVal strRYNC As String) As Boolean

            Try
                With m_objdacLuntan
                    doRegister = .doRegister(strErrMsg, strUserId, strPassword, strRYDM, strRYNC)
                End With
            Catch ex As Exception
                doRegister = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取交流用户数据（按“组织代码”+“人员序号”升序）
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     strWhere                    ：搜索字符串
        '     objLuntanData               ：信息数据集
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_Yonghu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objLuntanData As Xydc.Platform.Common.Data.ggxxLuntanData) As Boolean

            Try
                With m_objdacLuntan
                    getDataSet_Yonghu = .getDataSet_Yonghu(strErrMsg, strUserId, strPassword, strWhere, objLuntanData)
                End With
            Catch ex As Exception
                getDataSet_Yonghu = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据strRYDM获取交流用户数据
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     strRYDM                     ：人员代码
        '     blnUnused                   ：重载用
        '     objLuntanData               ：信息数据集
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_Yonghu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRYDM As String, _
            ByVal blnUnused As Boolean, _
            ByRef objLuntanData As Xydc.Platform.Common.Data.ggxxLuntanData) As Boolean

            Try
                With m_objdacLuntan
                    getDataSet_Yonghu = .getDataSet_Yonghu(strErrMsg, strUserId, strPassword, strRYDM, blnUnused, objLuntanData)
                End With
            Catch ex As Exception
                getDataSet_Yonghu = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 保存交流用户数据记录(整个事务完成)
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserId              ：用户标识
        '     strPassword            ：用户密码
        '     strRYDM                ：人员代码
        '     strRYNC                ：人员昵称
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function doSave_Yonghu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRYDM As String, _
            ByVal strRYNC As String) As Boolean

            Try
                With m_objdacLuntan
                    doSave_Yonghu = .doSave_Yonghu(strErrMsg, strUserId, strPassword, strRYDM, strRYNC)
                End With
            Catch ex As Exception
                doSave_Yonghu = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 删除交流用户
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strRYDM              ：人员代码
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDelete_Yonghu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRYDM As String) As Boolean

            Try
                With m_objdacLuntan
                    doDelete_Yonghu = .doDelete_Yonghu(strErrMsg, strUserId, strPassword, strRYDM)
                End With
            Catch ex As Exception
                doDelete_Yonghu = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 停用/启用交流用户
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strRYDM              ：人员代码
        '     blnValid             ：True-启用，False-停用
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doValid_Yonghu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRYDM As String, _
            ByVal blnValid As Boolean) As Boolean

            Try
                With m_objdacLuntan
                    doValid_Yonghu = .doValid_Yonghu(strErrMsg, strUserId, strPassword, strRYDM, blnValid)
                End With
            Catch ex As Exception
                doValid_Yonghu = False
                strErrMsg = ex.Message
            End Try

        End Function






        '----------------------------------------------------------------
        ' 删除交流数据(全部清除)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDelete_Jiaoliu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String) As Boolean

            Try
                With m_objdacLuntan
                    doDelete_Jiaoliu = .doDelete_Jiaoliu(strErrMsg, strUserId, strPassword)
                End With
            Catch ex As Exception
                doDelete_Jiaoliu = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 删除交流数据：指定时间段
        ' 指定strQSRQ，strJSRQ：strQSRQ <= 发表日期 <= strJSRQ
        ' 指定strQSRQ         ：strQSRQ <= 发表日期
        ' 指定strJSRQ         ：发表日期 <= strJSRQ
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strQSRQ              ：开始日期
        '     strJSRQ              ：结束日期
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDelete_Jiaoliu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strQSRQ As String, _
            ByVal strJSRQ As String) As Boolean

            Try
                With m_objdacLuntan
                    doDelete_Jiaoliu = .doDelete_Jiaoliu(strErrMsg, strUserId, strPassword, strQSRQ, strJSRQ)
                End With
            Catch ex As Exception
                doDelete_Jiaoliu = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 删除交流数据(指定记录)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     intJLBH              ：交流编号
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDelete_Jiaoliu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intJLBH As Integer) As Boolean

            Try
                With m_objdacLuntan
                    doDelete_Jiaoliu = .doDelete_Jiaoliu(strErrMsg, strUserId, strPassword, intJLBH)
                End With
            Catch ex As Exception
                doDelete_Jiaoliu = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取交流主题数据(按“交流数目”降序)
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     strWhere                    ：搜索条件
        '     objLuntanData               ：信息数据集
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_Jiaoliu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objLuntanData As Xydc.Platform.Common.Data.ggxxLuntanData) As Boolean

            Try
                With m_objdacLuntan
                    getDataSet_Jiaoliu = .getDataSet_Jiaoliu(strErrMsg, strUserId, strPassword, strWhere, objLuntanData)
                End With
            Catch ex As Exception
                getDataSet_Jiaoliu = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取主题下的讨论数据(按“发表日期”降序)
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     intJLBH                     ：主题编号
        '     strWhere                    ：搜索条件
        '     objLuntanData               ：信息数据集
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_Jiaoliu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intJLBH As Integer, _
            ByVal strWhere As String, _
            ByRef objLuntanData As Xydc.Platform.Common.Data.ggxxLuntanData) As Boolean

            Try
                With m_objdacLuntan
                    getDataSet_Jiaoliu = .getDataSet_Jiaoliu(strErrMsg, strUserId, strPassword, intJLBH, strWhere, objLuntanData)
                End With
            Catch ex As Exception
                getDataSet_Jiaoliu = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取指定主题数据
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     intJLBH                     ：主题编号
        '     objLuntanData               ：信息数据集
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getDataSet_Jiaoliu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intJLBH As Integer, _
            ByRef objLuntanData As Xydc.Platform.Common.Data.ggxxLuntanData) As Boolean

            Try
                With m_objdacLuntan
                    getDataSet_Jiaoliu = .getDataSet_Jiaoliu(strErrMsg, strUserId, strPassword, intJLBH, objLuntanData)
                End With
            Catch ex As Exception
                getDataSet_Jiaoliu = False
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
        Public Function doSave_Jiaoliu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Try
                With m_objdacLuntan
                    doSave_Jiaoliu = .doSave_Jiaoliu(strErrMsg, strUserId, strPassword, objNewData, objOldData, objenumEditType)
                End With
            Catch ex As Exception
                doSave_Jiaoliu = False
                strErrMsg = ex.Message
            End Try

        End Function






        '----------------------------------------------------------------
        ' 根据intJLBH获取交流主题
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     intJLBH                     ：主题编号
        '     strJLZT                     ：(返回)交流主题
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getJlztByJlbh( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intJLBH As Integer, _
            ByRef strJLZT As String) As Boolean

            Try
                With m_objdacLuntan
                    getJlztByJlbh = .getJlztByJlbh(strErrMsg, strUserId, strPassword, intJLBH, strJLZT)
                End With
            Catch ex As Exception
                getJlztByJlbh = False
                strErrMsg = ex.Message
            End Try

        End Function

    End Class 'rulesLuntan

End Namespace 'Xydc.Platform.BusinessRules
