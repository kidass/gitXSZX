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
    ' 类名    ：systemMyJiaotan
    '
    ' 功能描述： 
    '     提供对“公共_B_交谈”模块涉及的表现层操作
    '----------------------------------------------------------------
    Public Class systemMyJiaotan
        Inherits MarshalByRefObject

        'chat_ydxx.aspx、chat_fsxx.aspx的QueryString参数定义
        Public Const QUERYSTRING_LSH As String = "LSH"

        'chat_xzfj.aspx的QueryString参数定义
        Public Const QUERYSTRING_WJBS As String = "WJBS"
        Public Const QUERYSTRING_WJXH As String = "WJXH"







        '----------------------------------------------------------------
        ' 安全释放本身资源
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.systemMyJiaotan)
            Try
                If Not (obj Is Nothing) Then
                    'obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub








        '----------------------------------------------------------------
        ' 获取单条交流信息的显示(HTML格式)
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserId              ：用户标识
        '     strPassword            ：用户密码
        '     strUserXM              ：当前操作员名称
        '     objChatDataRow         ：要显示的交谈数据
        '     strNotice              ：返回单条交流信息的显示
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function getDisplayContent( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByVal objChatDataRow As System.Data.DataRow, _
            ByRef strNotice As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData
            Dim strNoticeFJ As String = ""

            getDisplayContent = False
            strNotice = ""
            strErrMsg = ""

            Try
                Dim strMessage As String
                Dim strWYBS As String
                Dim strFSRQ As String
                Dim strFSR As String
                Dim strJSR As String
                Dim strLSH As String
                Dim intBZ As Integer
                Dim datFSRQ As DateTime

                '获取信息
                strMessage = objPulicParameters.getObjectValue(objChatDataRow.Item(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_XX), "")
                strWYBS = objPulicParameters.getObjectValue(objChatDataRow.Item(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_WYBS), "")
                datFSRQ = objPulicParameters.getObjectValue(objChatDataRow.Item(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_FSSJ), datFSRQ)
                strFSRQ = Format(datFSRQ, "MM-dd HH:mm")
                strLSH = objPulicParameters.getObjectValue(objChatDataRow.Item(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_LSH), "")
                strFSR = objPulicParameters.getObjectValue(objChatDataRow.Item(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_FSR), "")
                strJSR = objPulicParameters.getObjectValue(objChatDataRow.Item(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_JSR), "")
                intBZ = objPulicParameters.getObjectValue(objChatDataRow.Item(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_BZ), 0)

                '获取附件信息
                If Me.getFujianDataSet(strErrMsg, strUserId, strPassword, strWYBS, objDataSet) = False Then
                    GoTo errProc
                End If


                '计算附件信息
                Dim strValue(3) As String
                Dim strAttach As String = ""
                Dim intCount As Integer
                Dim i As Integer
                If Not (objDataSet Is Nothing) Then
                    If Not (objDataSet.Tables(Xydc.Platform.Common.Data.grswMyJiaotanData.TABLE_GG_B_JIAOTAN_FUJIAN) Is Nothing) Then
                        With objDataSet.Tables(Xydc.Platform.Common.Data.grswMyJiaotanData.TABLE_GG_B_JIAOTAN_FUJIAN)
                            intCount = .Rows.Count
                            For i = 0 To intCount - 1 Step 1
                                strValue(0) = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_FUJIAN_WJSM), "")
                                strValue(1) = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_FUJIAN_WJYS), "")
                                strValue(2) = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.grswMyJiaotanData.FIELD_GG_B_JIAOTAN_FUJIAN_WJXH), "")

                                strValue(3) = strValue(0) + "(" + strValue(1) + "KB)"
                                strValue(3) = "<a href='chat_xzfj.aspx?WJBS=" + strWYBS + "&WJXH=" + strValue(2) + "' target='_blank'>" + strValue(3) + "</a>"

                                If strAttach = "" Then
                                    strAttach = strValue(3)
                                Else
                                    strAttach = strAttach + "&nbsp;&nbsp;" + strValue(3)
                                End If
                            Next
                        End With
                    End If
                End If
                If strAttach <> "" Then
                    strNoticeFJ = "&nbsp;&nbsp;" + strAttach
                Else
                    strNoticeFJ = strAttach
                End If

                '计算显示信息
                If strFSR.ToUpper = strUserXM.ToUpper Then
                    If strNotice = "" Then
                        strNotice = "[" + strFSRQ + "][" + strJSR + "←]：" + strMessage + strNoticeFJ + "<br>"
                    Else
                        strNotice = strNotice + "[" + strFSRQ + "][" + strJSR + "←]：" + strMessage + strNoticeFJ + "<br>"
                    End If
                Else
                    If strNotice = "" Then
                        If intBZ = 1 Then
                            strNotice = "[" + strFSRQ + "][" + strFSR + "→]：" + strMessage + strNoticeFJ + "[<a href='chat_fsxx.aspx?LSH=" + strLSH + "' target='chatFSFrame'>回复</a>]<br>"
                        Else
                            strNotice = "[" + strFSRQ + "][" + strFSR + "→]：" + strMessage + strNoticeFJ + "[<a href='chat_fsxx.aspx?LSH=" + strLSH + "' target='chatFSFrame'>回复</a>&nbsp;<a href='chat_ydxx.aspx?LSH=" + strLSH + "' target='chatYDFrame'>已看</a>]<br>"
                        End If
                    Else
                        If intBZ = 1 Then
                            strNotice = strNotice + "[" + strFSRQ + "][" + strFSR + "→]：" + strMessage + strNoticeFJ + "[<a href='chat_fsxx.aspx?LSH=" + strLSH + "' target='chatFSFrame'>回复</a>]<br>"
                        Else
                            strNotice = strNotice + "[" + strFSRQ + "][" + strFSR + "→]：" + strMessage + strNoticeFJ + "[<a href='chat_fsxx.aspx?LSH=" + strLSH + "' target='chatFSFrame'>回复</a>&nbsp;<a href='chat_ydxx.aspx?LSH=" + strLSH + "' target='chatYDFrame'>已看</a>]<br>"
                        End If
                    End If
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Data.grswMyJiaotanData.SafeRelease(objDataSet)

            getDisplayContent = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Data.grswMyJiaotanData.SafeRelease(objDataSet)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取[发送人=strUserXM]的交谈数据
        ' 获取“公共_B_交谈”的数据集(以发送时间降序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strUserXM            ：当前操作员名称
        '     strWhere             ：搜索字符串
        '     objJiaotanDataSet    ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByVal strWhere As String, _
            ByRef objJiaotanDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesMyJiaotan
                    getDataSet = .getDataSet(strErrMsg, strUserId, strPassword, strUserXM, strWhere, objJiaotanDataSet)
                End With
            Catch ex As Exception
                getDataSet = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取[接收人=strUserXM]的留言数据
        ' 获取“公共_B_交谈”的数据集(以发送时间降序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strUserXM            ：当前操作员名称
        '     strWhere             ：搜索字符串
        '     blnUnused            ：接口重载用
        '     objJiaotanDataSet    ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByVal strWhere As String, _
            ByVal blnUnused As Boolean, _
            ByRef objJiaotanDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesMyJiaotan
                    getDataSet = .getDataSet(strErrMsg, strUserId, strPassword, strUserXM, strWhere, blnUnused, objJiaotanDataSet)
                End With
            Catch ex As Exception
                getDataSet = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据流水号获取交谈信息
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strLSH               ：流水号
        '     objJiaotanDataSet    ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strLSH As String, _
            ByRef objJiaotanDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesMyJiaotan
                    getDataSet = .getDataSet(strErrMsg, strUserId, strPassword, strLSH, objJiaotanDataSet)
                End With
            Catch ex As Exception
                getDataSet = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取strUserXM发送或接收的交谈数据(带附件信息,HTML格式)
        ' 获取“公共_B_交谈”的数据集(以发送时间降序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strUserXM            ：当前操作员名称
        '     strWhere             ：搜索字符串
        '     objJiaotanDataSet    ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDataSetHtml( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByVal strWhere As String, _
            ByRef objJiaotanDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesMyJiaotan
                    getDataSetHtml = .getDataSetHtml(strErrMsg, strUserId, strPassword, strUserXM, strWhere, objJiaotanDataSet)
                End With
            Catch ex As Exception
                getDataSetHtml = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取strUserXM发送或接收的交谈数据(带附件信息,Text格式)
        ' 获取“公共_B_交谈”的数据集(以发送时间降序排序)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strUserXM            ：当前操作员名称
        '     strWhere             ：搜索字符串
        '     objJiaotanDataSet    ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDataSetText( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByVal strWhere As String, _
            ByRef objJiaotanDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesMyJiaotan
                    getDataSetText = .getDataSetText(strErrMsg, strUserId, strPassword, strUserXM, strWhere, objJiaotanDataSet)
                End With
            Catch ex As Exception
                getDataSetText = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 保存“公共_B_交谈”的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
        '     objNewData           ：新数据
        '     objenumEditType      ：编辑类型
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doSaveData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesMyJiaotan
                    doSaveData = .doSaveData(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType)
                End With
            Catch ex As Exception
                doSaveData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' strFSR向strJSR发送交谈信息strMsg
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doSendChat( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strFSR As String, _
            ByVal strJSR As String, _
            ByVal strMsg As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesMyJiaotan
                    doSendChat = .doSendChat(strErrMsg, strUserId, strPassword, strFSR, strJSR, strMsg)
                End With
            Catch ex As Exception
                doSendChat = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 删除“公共_B_交谈”的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：要删除的数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesMyJiaotan
                    doDeleteData = .doDeleteData(strErrMsg, strUserId, strPassword, objOldData)
                End With
            Catch ex As Exception
                doDeleteData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 删除指定strWJBS的“公共_B_交谈”的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWJBS              ：唯一标识
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWJBS As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesMyJiaotan
                    doDeleteData = .doDeleteData(strErrMsg, strUserId, strPassword, strWJBS)
                End With
            Catch ex As Exception
                doDeleteData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取[接收人=strUserXM]的没有阅读的交谈数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strUserXM            ：当前操作员名称
        '     objJiaotanDataSet    ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDataSetWYD( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByRef objJiaotanDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesMyJiaotan
                    getDataSetWYD = .getDataSetWYD(strErrMsg, strUserId, strPassword, strUserXM, objJiaotanDataSet)
                End With
            Catch ex As Exception
                getDataSetWYD = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取strUserXM在指定之间之后发送或接收的交谈数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strUserXM            ：当前操作员名称
        '     strZDSJ              ：指定时间
        '     objJiaotanDataSet    ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getDataSetAfterTime( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByVal strZDSJ As String, _
            ByRef objJiaotanDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesMyJiaotan
                    getDataSetAfterTime = .getDataSetAfterTime(strErrMsg, strUserId, strPassword, strUserXM, strZDSJ, objJiaotanDataSet)
                End With
            Catch ex As Exception
                getDataSetAfterTime = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 设置我已经阅读strLSH信息
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strUserXM            ：当前操作员名称
        '     strLSH               ：流水号
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doSetReadFlag( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strUserXM As String, _
            ByVal strLSH As String) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesMyJiaotan
                    doSetReadFlag = .doSetReadFlag(strErrMsg, strUserId, strPassword, strUserXM, strLSH)
                End With
            Catch ex As Exception
                doSetReadFlag = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 保存交谈数据记录(整个事务完成)
        '     strErrMsg              ：如果错误，则返回错误信息
        '     strUserId              ：用户标识
        '     strPassword            ：用户密码
        '     objNewData             ：记录新值(返回保存后的新值)
        '     objOldData             ：记录旧值
        '     objenumEditType        ：编辑类型
        '     objNewFJData           ：要保存的附件数据
        ' 返回
        '     True                   ：成功
        '     False                  ：失败
        '----------------------------------------------------------------
        Public Function doSaveData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType, _
            ByVal objNewFJData As Xydc.Platform.Common.Data.grswMyJiaotanData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesMyJiaotan
                    doSaveData = .doSaveData(strErrMsg, strUserId, strPassword, objNewData, objOldData, objenumEditType, objNewFJData)
                End With
            Catch ex As Exception
                doSaveData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据文件标识获取交谈的附件信息
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWJBS              ：文件标识
        '     objJiaotanDataSet    ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getFujianDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWJBS As String, _
            ByRef objJiaotanDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesMyJiaotan
                    getFujianDataSet = .getFujianDataSet(strErrMsg, strUserId, strPassword, strWJBS, objJiaotanDataSet)
                End With
            Catch ex As Exception
                getFujianDataSet = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据文件标识、序号获取交谈的附件信息
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strWJBS              ：文件标识
        '     strWJXH              ：序号
        '     objJiaotanDataSet    ：信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getFujianDataSet( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWJBS As String, _
            ByVal strWJXH As String, _
            ByRef objJiaotanDataSet As Xydc.Platform.Common.Data.grswMyJiaotanData) As Boolean

            Try
                With New Xydc.Platform.BusinessRules.rulesMyJiaotan
                    getFujianDataSet = .getFujianDataSet(strErrMsg, strUserId, strPassword, strWJBS, strWJXH, objJiaotanDataSet)
                End With
            Catch ex As Exception
                getFujianDataSet = False
                strErrMsg = ex.Message
            End Try

        End Function

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
                With New Xydc.Platform.BusinessRules.rulesMyJiaotan
                    doExportToExcel = .doExportToExcel(strErrMsg, objDataSet, strExcelFile)
                End With
            Catch ex As Exception
                doExportToExcel = False
                strErrMsg = ex.Message
            End Try

        End Function

    End Class

End Namespace
