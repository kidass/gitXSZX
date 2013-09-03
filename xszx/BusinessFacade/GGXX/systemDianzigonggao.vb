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
    ' 类名    ：systemDianzigonggao
    '
    ' 功能描述： 
    '     提供对“电子公告”模块涉及的表现层操作
    '----------------------------------------------------------------
    Public Class systemDianzigonggao
        Implements System.IDisposable

        Private m_objrulesDianzigonggao As Xydc.Platform.BusinessRules.rulesDianzigonggao








        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
            m_objrulesDianzigonggao = New Xydc.Platform.BusinessRules.rulesDianzigonggao
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
            If Not (m_objrulesDianzigonggao Is Nothing) Then
                m_objrulesDianzigonggao.Dispose()
                m_objrulesDianzigonggao = Nothing
            End If
        End Sub

        '----------------------------------------------------------------
        ' 安全释放本身资源
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.systemDianzigonggao)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub









        '----------------------------------------------------------------
        ' 输出即时交流数据到Excel
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objDataSet           ：要导出的数据集
        '     strExcelFile         ：导出到WEB服务器中的Excel文件路径
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doExportToExcel( _
            ByRef strErrMsg As String, _
            ByVal objDataSet As System.Data.DataSet, _
            ByVal strExcelFile As String) As Boolean

            Try
                With m_objrulesDianzigonggao
                    doExportToExcel = .doExportToExcel(strErrMsg, objDataSet, strExcelFile)
                End With
            Catch ex As Exception
                doExportToExcel = False
                strErrMsg = ex.Message
            End Try

        End Function






        '----------------------------------------------------------------
        ' 获取[操作员代码=strCzydm]的电子公告数据（按“日期”降序），即
        ' 我负责发布的电子公告数据
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     strCzydm                    ：当前操作员标识
        '     strWhere                    ：搜索字符串
        '     objDianzigonggaoData        ：信息数据集
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strCzydm As String, _
            ByVal strWhere As String, _
            ByRef objDianzigonggaoData As Xydc.Platform.Common.Data.ggxxDianzigonggaoData) As Boolean

            Try
                With m_objrulesDianzigonggao
                    getDataSet = .getDataSet(strErrMsg, strUserId, strPassword, strCzydm, strWhere, objDianzigonggaoData)
                End With
            Catch ex As Exception
                getDataSet = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取[操作员代码=strCzydm、序号=intXH]的电子公告数据
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     strCzydm                    ：当前操作员标识
        '     intXH                       ：公告序号
        '     objDianzigonggaoData        ：信息数据集
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strCzydm As String, _
            ByVal intXH As Integer, _
            ByRef objDianzigonggaoData As Xydc.Platform.Common.Data.ggxxDianzigonggaoData) As Boolean

            Try
                With m_objrulesDianzigonggao
                    getDataSet = .getDataSet(strErrMsg, strUserId, strPassword, strCzydm, intXH, objDianzigonggaoData)
                End With
            Catch ex As Exception
                getDataSet = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取strUserId的能够阅读的已发布的电子公告数据（按“日期”降序），即
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     strWhere                    ：搜索字符串
        '     objDianzigonggaoData        ：信息数据集
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objDianzigonggaoData As Xydc.Platform.Common.Data.ggxxDianzigonggaoData) As Boolean

            Try
                With m_objrulesDianzigonggao
                    getDataSet = .getDataSet(strErrMsg, strUserId, strPassword, strWhere, objDianzigonggaoData)
                End With
            Catch ex As Exception
                getDataSet = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取[操作员代码=strCzydm、序号=intXH]的电子公告的限制阅读人员数据
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     strCzydm                    ：当前操作员标识
        '     intXH                       ：公告序号
        '     strYDRY                     ：（返回）限制阅读人员数据
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getKeYueduRenyuan( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strCzydm As String, _
            ByVal intXH As Integer, _
            ByRef strYDRY As String) As Boolean

            Try
                With m_objrulesDianzigonggao
                    getKeYueduRenyuan = .getKeYueduRenyuan(strErrMsg, strUserId, strPassword, strCzydm, intXH, strYDRY)
                End With
            Catch ex As Exception
                getKeYueduRenyuan = False
                strErrMsg = ex.Message
            End Try

        End Function




        '----------------------------------------------------------------
        ' 取消已发布的电子公告 或 发布电子公告
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strCzydm             ：发布人代码
        '     intXH                ：公告序号
        '     blnFabu              ：True-发布，False-取消发布
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doFabu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strCzydm As String, _
            ByVal intXH As Integer, _
            ByVal blnFabu As Boolean) As Boolean

            Try
                With m_objrulesDianzigonggao
                    doFabu = .doFabu(strErrMsg, strUserId, strPassword, strCzydm, intXH, blnFabu)
                End With
            Catch ex As Exception
                doFabu = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 设置“已经阅读”
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strCzydm             ：发布人代码
        '     intXH                ：公告序号
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doSetHasRead( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strCzydm As String, _
            ByVal intXH As Integer) As Boolean

            Try
                With m_objrulesDianzigonggao
                    doSetHasRead = .doSetHasRead(strErrMsg, strUserId, strPassword, strCzydm, intXH)
                End With
            Catch ex As Exception
                doSetHasRead = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 删除电子公告
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strCzydm             ：发布人代码
        '     intXH                ：公告序号
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDelete( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strCzydm As String, _
            ByVal intXH As Integer) As Boolean

            Try
                With m_objrulesDianzigonggao
                    doDelete = .doDelete(strErrMsg, strUserId, strPassword, strCzydm, intXH)
                End With
            Catch ex As Exception
                doDelete = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 保存电子公告数据记录(整个事务完成)
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserId              ：用户标识
        '     strPassword            ：用户密码
        '     objNewData             ：记录新值(返回保存后的新值)
        '     objOldData             ：记录旧值
        '     strFBFW                ：发布范围
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
            ByVal strFBFW As String, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Try
                With m_objrulesDianzigonggao
                    doSave = .doSave(strErrMsg, strUserId, strPassword, objNewData, objOldData, strFBFW, objenumEditType)
                End With
            Catch ex As Exception
                doSave = False
                strErrMsg = ex.Message
            End Try

        End Function


        '----------------------------------------------------------------
        ' 保存电子公告数据记录(整个事务完成)
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserId              ：用户标识
        '     strPassword            ：用户密码
        '     objNewData             ：记录新值(返回保存后的新值)
        '     objOldData             ：记录旧值
        '     strFBFW                ：发布范围
        '     objenumEditType        ：编辑类型
        '     objDataSet_FJ          : 附件数据集
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
            ByVal strFBFW As String, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType, _
            ByVal objDataSet_FJ As Xydc.Platform.Common.Data.ggxxDianzigonggaoData) As Boolean

            Try
                With m_objrulesDianzigonggao
                    doSave = .doSave(strErrMsg, strUserId, strPassword, objNewData, objOldData, strFBFW, objenumEditType, objDataSet_FJ)
                End With
            Catch ex As Exception
                doSave = False
                strErrMsg = ex.Message
            End Try

        End Function





        '----------------------------------------------------------------
        ' 判断strUserId是否能够阅读的已发布strZcydm+intXH的电子公告数据
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     strCzydm                    ：操作员代码
        '     intXH                       ：公告序号
        '     blnYuedu                    ：（返回）True-能，False-不能
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function isCanRead( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strCzydm As String, _
            ByVal intXH As Integer, _
            ByRef blnYuedu As Boolean) As Boolean

            Try
                With m_objrulesDianzigonggao
                    isCanRead = .isCanRead(strErrMsg, strUserId, strPassword, strCzydm, intXH, blnYuedu)
                End With
            Catch ex As Exception
                isCanRead = False
                strErrMsg = ex.Message
            End Try

        End Function


        '----------------------------------------------------------------
        ' 根据strWJBS获取“电子公告_B_附件”的数据集
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     strWJBS                     ：文件标识        '
        '     objFujianData        ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getFujianData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWJBS As String, _
            ByRef objFujianData As Xydc.Platform.Common.Data.ggxxDianzigonggaoData) As Boolean

            Try
                With m_objrulesDianzigonggao
                    getFujianData = .getFujianData(strErrMsg, strUserId, strPassword, strWJBS, objFujianData)
                End With
            Catch ex As Exception
                getFujianData = False
                strErrMsg = ex.Message
            End Try

        End Function


        '----------------------------------------------------------------
        ' 判断附件记录数据是否有效？
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objNewData           ：记录新值(返回推荐值)
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doVerifyFujian( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef objNewData As System.Collections.Specialized.NameValueCollection) As Boolean

            Try
                With m_objrulesDianzigonggao
                    doVerifyFujian = .doVerifyFujian(strErrMsg, strUserId, strPassword, objNewData)
                End With
            Catch ex As Exception
                doVerifyFujian = False
                strErrMsg = ex.Message
            End Try

        End Function


        '----------------------------------------------------------------
        ' 保存附件数据
        '     strErrMsg              ：如果错误，则返回错误信息
        '     blnEnforeEdit          ：是否强制修改
        '     strUserId              ：用户标识
        '     strPassword            ：用户密码
        '     strUserXM              ：操作员名称
        '     strWJBS                : 文件标识
        '     objNewData             ：记录新值(返回保存后的新值)
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function doSaveFujian( _
            ByRef strErrMsg As String, _
            ByVal blnEnforeEdit As Boolean, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByVal strWJBS As String, _
            ByRef objNewData As Xydc.Platform.Common.Data.ggxxDianzigonggaoData) As Boolean

            Try
                With m_objrulesDianzigonggao
                    doSaveFujian = .doSaveFujian(strErrMsg, blnEnforeEdit, strUserId, strPassword, strUserXM, strWJBS, objNewData)
                End With
            Catch ex As Exception
                doSaveFujian = False
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
        Public Function doDeleteData_FJ( _
            ByRef strErrMsg As String, _
            ByVal objOldData As System.Data.DataRow) As Boolean

            Try
                With m_objrulesDianzigonggao
                    doDeleteData_FJ = .doDeleteData_FJ(strErrMsg, objOldData)
                End With
            Catch ex As Exception
                doDeleteData_FJ = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 在附件缓存数据中自动调整显示序号=数据集中的行序号+1
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objFJData            ：缓存数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doAutoAdjustXSXH_FJ( _
            ByRef strErrMsg As String, _
            ByRef objFJData As Xydc.Platform.Common.Data.ggxxDianzigonggaoData) As Boolean

            Try
                With m_objrulesDianzigonggao
                    doAutoAdjustXSXH_FJ = .doAutoAdjustXSXH_FJ(strErrMsg, objFJData)
                End With
            Catch ex As Exception
                doAutoAdjustXSXH_FJ = False
                strErrMsg = ex.Message
            End Try

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
        Public Function doMoveTo_FJ( _
            ByRef strErrMsg As String, _
            ByRef objSrcData As System.Data.DataRow, _
            ByRef objDesData As System.Data.DataRow) As Boolean

            Try
                With m_objrulesDianzigonggao
                    doMoveTo_FJ = .doMoveTo_FJ(strErrMsg, objSrcData, objDesData)
                End With
            Catch ex As Exception
                doMoveTo_FJ = False
                strErrMsg = ex.Message
            End Try
        End Function


    End Class

End Namespace
