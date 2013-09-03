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
Imports System.Web
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
    ' 类名    ：rulesGonggongziyuan
    '
    ' 功能描述： 
    '     提供对“公共资源”涉及的业务逻辑层操作
    '----------------------------------------------------------------
    Public Class rulesGonggongziyuan
        Implements System.IDisposable

        Private m_objdacGonggongziyuan As Xydc.Platform.DataAccess.dacGonggongziyuan










        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
            m_objdacGonggongziyuan = New Xydc.Platform.DataAccess.dacGonggongziyuan
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
            If Not (m_objdacGonggongziyuan Is Nothing) Then
                m_objdacGonggongziyuan.Dispose()
                m_objdacGonggongziyuan = Nothing
            End If
        End Sub

        '----------------------------------------------------------------
        ' 安全释放本身资源
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessRules.rulesGonggongziyuan)
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
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doExportToExcel( _
            ByRef strErrMsg As String, _
            ByVal objDataSet As System.Data.DataSet, _
            ByVal strExcelFile As String) As Boolean

            Try
                With m_objdacGonggongziyuan
                    doExportToExcel = .doExportToExcel(strErrMsg, objDataSet, strExcelFile)
                End With
            Catch ex As Exception
                doExportToExcel = False
                strErrMsg = ex.Message
            End Try

        End Function




        '----------------------------------------------------------------
        ' 获取“信息_B_公共资源_栏目”的数据集(以“栏目代码”升序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWhere             ：搜索字符串(默认表前缀a.)
        '     objLanmuData         ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getLanmuData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objLanmuData As Xydc.Platform.Common.Data.ggxxGonggongziyuanData) As Boolean

            Try
                With m_objdacGonggongziyuan
                    getLanmuData = .getLanmuData(strErrMsg, strUserId, strPassword, strWhere, objLanmuData)
                End With
            Catch ex As Exception
                getLanmuData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取指定strLMDM下级的“信息_B_公共资源_栏目”的数据集(以“栏目代码”升序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strLMDM              ：栏目代码
        '     strWhere             ：搜索字符串(默认表前缀a.)
        '     objLanmuData         ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getLanmuData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strLMDM As String, _
            ByVal strWhere As String, _
            ByRef objLanmuData As Xydc.Platform.Common.Data.ggxxGonggongziyuanData) As Boolean

            Try
                With m_objdacGonggongziyuan
                    getLanmuData = .getLanmuData(strErrMsg, strUserId, strPassword, strLMDM, strWhere, objLanmuData)
                End With
            Catch ex As Exception
                getLanmuData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据指定strLMDM获取“信息_B_公共资源_栏目”的数据集
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strLMDM              ：栏目代码
        '     blnUnused            ：重载用
        '     objLanmuData         ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getLanmuData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strLMDM As String, _
            ByVal blnUnused As Boolean, _
            ByRef objLanmuData As Xydc.Platform.Common.Data.ggxxGonggongziyuanData) As Boolean

            Try
                With m_objdacGonggongziyuan
                    getLanmuData = .getLanmuData(strErrMsg, strUserId, strPassword, strLMDM, blnUnused, objLanmuData)
                End With
            Catch ex As Exception
                getLanmuData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据指定intMKBS获取“信息_B_公共资源_栏目”的数据集
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     intMKBS              ：栏目标识
        '     blnUnused            ：重载用
        '     objLanmuData         ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getLanmuData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intMKBS As Integer, _
            ByVal blnUnused As Boolean, _
            ByRef objLanmuData As Xydc.Platform.Common.Data.ggxxGonggongziyuanData) As Boolean

            Try
                With m_objdacGonggongziyuan
                    getLanmuData = .getLanmuData(strErrMsg, strUserId, strPassword, intMKBS, blnUnused, objLanmuData)
                End With
            Catch ex As Exception
                getLanmuData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据上级栏目代码获取下级的栏目代码
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strPrevLMDM          ：上级栏目代码
        '     strNewLMDM           ：新栏目代码(返回)
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getNewLMDM( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strPrevLMDM As String, _
            ByRef strNewLMDM As String) As Boolean

            Try
                With m_objdacGonggongziyuan
                    getNewLMDM = .getNewLMDM(strErrMsg, strUserId, strPassword, strPrevLMDM, strNewLMDM)
                End With
            Catch ex As Exception
                getNewLMDM = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取新的栏目标识
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strNewLMBS           ：新栏目标识(返回)
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getNewLMBS( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef strNewLMBS As String) As Boolean

            Try
                With m_objdacGonggongziyuan
                    getNewLMBS = .getNewLMBS(strErrMsg, strUserId, strPassword, strNewLMBS)
                End With
            Catch ex As Exception
                getNewLMBS = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据现有新值计算其他系统自动计算的值
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objNewData           ：新数据(返回)
        '     objenumEditType      ：编辑类型
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getLanmuDefaultValue( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef objNewData As System.Collections.Specialized.ListDictionary, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Try
                With m_objdacGonggongziyuan
                    getLanmuDefaultValue = .getLanmuDefaultValue(strErrMsg, strUserId, strPassword, objNewData, objenumEditType)
                End With
            Catch ex As Exception
                getLanmuDefaultValue = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据“栏目名称”获取“栏目标识”
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strLMMC              ：栏目名称
        '     strLMBS              ：(返回)栏目标识
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getLmbsByLmmc( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strLMMC As String, _
            ByRef strLMBS As String) As Boolean

            Try
                With m_objdacGonggongziyuan
                    getLmbsByLmmc = .getLmbsByLmmc(strErrMsg, strUserId, strPassword, strLMMC, strLMBS)
                End With
            Catch ex As Exception
                getLmbsByLmmc = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据“栏目名称”获取“栏目代码”
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strLMMC              ：栏目名称
        '     strLMDM              ：(返回)栏目代码
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getLmdmByLmmc( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strLMMC As String, _
            ByRef strLMDM As String) As Boolean

            Try
                With m_objdacGonggongziyuan
                    getLmdmByLmmc = .getLmdmByLmmc(strErrMsg, strUserId, strPassword, strLMMC, strLMDM)
                End With
            Catch ex As Exception
                getLmdmByLmmc = False
                strErrMsg = ex.Message
            End Try

        End Function



        '----------------------------------------------------------------
        ' 保存“信息_B_公共资源_栏目”的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
        '     objNewData           ：新数据(返回)
        '     objenumEditType      ：编辑类型
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doSaveLanmuData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByRef objNewData As System.Collections.Specialized.ListDictionary, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Try
                With m_objdacGonggongziyuan
                    doSaveLanmuData = .doSaveLanmuData(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType)
                End With
            Catch ex As Exception
                doSaveLanmuData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据栏目代码删除“信息_B_公共资源_栏目”的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strLMDM              ：栏目代码
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteLanmuData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strLMDM As String) As Boolean

            Try
                With m_objdacGonggongziyuan
                    doDeleteLanmuData = .doDeleteLanmuData(strErrMsg, strUserId, strPassword, strLMDM)
                End With
            Catch ex As Exception
                doDeleteLanmuData = False
                strErrMsg = ex.Message
            End Try

        End Function




        '----------------------------------------------------------------
        ' 获取[人员代码=strCzydm]的公共资源数据（按“发布日期”降序），即
        ' 我负责发布的公共资源数据
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     strCzydm                    ：操作员标识
        '     strWhere                    ：搜索字符串
        '     objGonggongziyuanData       ：信息数据集
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
            ByRef objGonggongziyuanData As Xydc.Platform.Common.Data.ggxxGonggongziyuanData) As Boolean

            Try
                With m_objdacGonggongziyuan
                    getDataSet = .getDataSet(strErrMsg, strUserId, strPassword, strCzydm, strWhere, objGonggongziyuanData)
                End With
            Catch ex As Exception
                getDataSet = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取[资源标识=strZYBS]的公共资源数据
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     strZYBS                     ：资源标识
        '     objGonggongziyuanData       ：信息数据集
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZYBS As String, _
            ByRef objGonggongziyuanData As Xydc.Platform.Common.Data.ggxxGonggongziyuanData) As Boolean

            Try
                With m_objdacGonggongziyuan
                    getDataSet = .getDataSet(strErrMsg, strUserId, strPassword, strZYBS, objGonggongziyuanData)
                End With
            Catch ex As Exception
                getDataSet = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取strUserId的能够阅读的已发布的公共资源数据（按“发布日期”降序）
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     strWhere                    ：搜索字符串
        '     blnUnused                   ：重载用
        '     objGonggongziyuanData       ：信息数据集
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByVal blnUnused As Boolean, _
            ByRef objGonggongziyuanData As Xydc.Platform.Common.Data.ggxxGonggongziyuanData) As Boolean

            Try
                With m_objdacGonggongziyuan
                    getDataSet = .getDataSet(strErrMsg, strUserId, strPassword, strWhere, blnUnused, objGonggongziyuanData)
                End With
            Catch ex As Exception
                getDataSet = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取[资源标识=strZYBS]的公共资源的限制阅读人员数据
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     strZYBS                     ：资源标识
        '     strYDRYMC                   ：（返回）限制阅读人员数据(人员名称)
        '     strYDRYDM                   ：（返回）限制阅读人员数据(人员代码)
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function getKeYueduRenyuan( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZYBS As String, _
            ByRef strYDRYMC As String, _
            ByRef strYDRYDM As String) As Boolean

            Try
                With m_objdacGonggongziyuan
                    getKeYueduRenyuan = .getKeYueduRenyuan(strErrMsg, strUserId, strPassword, strZYBS, strYDRYMC, strYDRYDM)
                End With
            Catch ex As Exception
                getKeYueduRenyuan = False
                strErrMsg = ex.Message
            End Try

        End Function




        '----------------------------------------------------------------
        ' 取消已发布的公共资源 或 发布公共资源
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strZYBS              ：资源标识
        '     blnFabu              ：True-发布，False-取消发布
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doFabu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZYBS As String, _
            ByVal blnFabu As Boolean) As Boolean

            Try
                With m_objdacGonggongziyuan
                    doFabu = .doFabu(strErrMsg, strUserId, strPassword, strZYBS, blnFabu)
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
        '     strZYBS              ：资源标识
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doSetHasRead( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZYBS As String) As Boolean

            Try
                With m_objdacGonggongziyuan
                    doSetHasRead = .doSetHasRead(strErrMsg, strUserId, strPassword, strZYBS)
                End With
            Catch ex As Exception
                doSetHasRead = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 删除公共资源
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strZYBS              ：资源标识
        '     strAppRoot           ：应用根Http路径(不带/)
        '     objServer            ：服务器对象
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDelete( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZYBS As String, _
            ByVal strAppRoot As String, _
            ByVal objServer As System.Web.HttpServerUtility) As Boolean

            Try
                With m_objdacGonggongziyuan
                    doDelete = .doDelete(strErrMsg, strUserId, strPassword, strZYBS, strAppRoot, objServer)
                End With
            Catch ex As Exception
                doDelete = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 保存公共资源数据记录(整个事务完成)
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserId              ：用户标识
        '     strPassword            ：用户密码
        '     objNewData             ：记录新值(返回保存后的新值)
        '     objOldData             ：记录旧值
        '     strFBFW                ：发布范围
        '     objenumEditType        ：编辑类型
        '     strUploadFile          ：上载文件的WEB本地完全路径
        '     strAppRoot             ：应用根Http路径(不带/)
        '     strBasePath            ：从应用根到存放地的相对HTTP目录(开头不带/)
        '     objServer              ：服务器对象
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
            ByVal strUploadFile As String, _
            ByVal strAppRoot As String, _
            ByVal strBasePath As String, _
            ByVal objServer As System.Web.HttpServerUtility) As Boolean

            Try
                With m_objdacGonggongziyuan
                    doSave = .doSave(strErrMsg, strUserId, strPassword, objNewData, objOldData, strFBFW, objenumEditType, strUploadFile, strAppRoot, strBasePath, objServer)
                End With
            Catch ex As Exception
                doSave = False
                strErrMsg = ex.Message
            End Try

        End Function






        '----------------------------------------------------------------
        ' 判断strUserId是否能够阅读的已发布的strZYBS公共资源数据
        '     strErrMsg                   ：如果错误，则返回错误信息
        '     strUserId                   ：用户标识
        '     strPassword                 ：用户密码
        '     strZYBS                     ：资源标识
        '     blnYuedu                    ：（返回）True-能,False-不能
        ' 返回
        '     True                        ：成功
        '     False                       ：失败
        '----------------------------------------------------------------
        Public Function isCanRead( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZYBS As String, _
            ByRef blnYuedu As Boolean) As Boolean

            Try
                With m_objdacGonggongziyuan
                    isCanRead = .isCanRead(strErrMsg, strUserId, strPassword, strZYBS, blnYuedu)
                End With
            Catch ex As Exception
                isCanRead = False
                strErrMsg = ex.Message
            End Try

        End Function

    End Class 'rulesGonggongziyuan

End Namespace 'Xydc.Platform.BusinessRules
