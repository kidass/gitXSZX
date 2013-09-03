Imports System.Web.Security

Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.web
    ' 类名    ：xtgl_wjzh
    ' 
    ' 调用性质：
    '     独立运行
    '
    ' 功能描述： 
    '   　文件转换处理模块
    '----------------------------------------------------------------

    Partial Public Class xtgl_wjzh
        Inherits Xydc.Platform.web.PageBase

        '----------------------------------------------------------------
        '模块私用参数
        '----------------------------------------------------------------
        '本模块相对image的相对路径
        Private m_cstrRelativePathToImage As String = "../../"

        '----------------------------------------------------------------
        '模块授权参数
        '----------------------------------------------------------------
        Private m_cstrPrevilegeParamPrefix As String = "xtgl_wjzh_previlege_param"
        Private m_blnPrevilegeParams(2) As Boolean

        '----------------------------------------------------------------
        '模块现场保留参数，恢复完成后立即释放session资源
        '----------------------------------------------------------------
        Private m_blnSaveScence As Boolean

        '----------------------------------------------------------------
        '模块接口参数
        '----------------------------------------------------------------
        Private m_blnInterface As Boolean










        '----------------------------------------------------------------
        ' 获取权限参数
        '     strErrMsg          ：返回错误信息
        '     blnContinueExecute ：是否继续执行后续程序？
        ' 返回
        '     True               ：成功
        '     False              ：失败
        '----------------------------------------------------------------
        Private Function getPrevilegeParams( _
            ByRef strErrMsg As String, _
            ByRef blnContinueExecute As Boolean) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objsystemAppManager As New Xydc.Platform.BusinessFacade.systemAppManager
            Dim objMokuaiQXData As Xydc.Platform.Common.Data.AppManagerData

            getPrevilegeParams = False
            blnContinueExecute = False

            Try
                Dim intCount As Integer
                Dim i As Integer

                '根据登录用户获取模块权限数据
                If MyBase.UserId.ToUpper() = "SA" Then
                    '管理员权限
                    intCount = Me.m_blnPrevilegeParams.Length
                    For i = 0 To intCount - 1 Step 1
                        Me.m_blnPrevilegeParams(i) = True
                    Next
                    blnContinueExecute = True
                    Exit Try
                Else
                    '普通用户权限
                    If objsystemAppManager.getDBUserMokuaiQXData(strErrMsg, MyBase.UserId, MyBase.UserPassword, MyBase.UserId, objMokuaiQXData) = False Then
                        GoTo errProc
                    End If
                End If

                '检查权限
                Dim strFirstParamValue As String
                Dim strParamValue As String
                Dim strParamName As String
                Dim strMKMC As String
                Dim strFilter As String
                strMKMC = Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAIQX_MKMC
                With objMokuaiQXData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_YINGYONGXITONG_MOKUAIQX)
                    intCount = Me.m_blnPrevilegeParams.Length
                    For i = 0 To intCount - 1 Step 1
                        '计算参数名
                        strParamName = i.ToString()
                        If strParamName.Length < 2 Then strParamName = "0" + strParamName
                        strParamName = Me.m_cstrPrevilegeParamPrefix + strParamName

                        '获取参数值
                        With objPulicParameters
                            strParamValue = .getObjectValue(System.Configuration.ConfigurationManager.AppSettings(strParamName), "")
                        End With
                        If i = 0 Then strFirstParamValue = strParamValue

                        '获取参数对应的权限
                        strFilter = strMKMC + " = '" + strParamValue + "'"
                        .DefaultView.RowFilter = strFilter
                        If .DefaultView.Count > 0 Then
                            Me.m_blnPrevilegeParams(i) = True
                        Else
                            Me.m_blnPrevilegeParams(i) = False
                        End If
                    Next

                End With

                '是否继续执行
                If Me.m_blnPrevilegeParams(0) = True Then
                    blnContinueExecute = True
                Else
                    Me.panelError.Visible = True
                    Me.lblMessage.Text = "错误：您没有[" + strFirstParamValue + "]的执行权限，请与系统管理员联系，谢谢！"
                    Me.panelMain.Visible = Not Me.panelError.Visible
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.BusinessFacade.systemAppManager.SafeRelease(objsystemAppManager)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objMokuaiQXData)

            getPrevilegeParams = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.BusinessFacade.systemAppManager.SafeRelease(objsystemAppManager)
            Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objMokuaiQXData)
            Exit Function

        End Function








        '----------------------------------------------------------------
        ' 复原模块现场信息并释放相应的资源
        '----------------------------------------------------------------
        Private Sub restoreModuleInformation(ByVal strSessionId As String)

        End Sub

        '----------------------------------------------------------------
        ' 保存模块现场信息并返回相应的SessionId
        '----------------------------------------------------------------
        Private Function saveModuleInformation() As String
            saveModuleInformation = ""
        End Function

        '----------------------------------------------------------------
        ' 从调用模块中获取数据
        '----------------------------------------------------------------
        Private Function getDataFromCallModule(ByRef strErrMsg As String) As Boolean

            getDataFromCallModule = False

            Try
            Catch ex As Exception
            End Try

            getDataFromCallModule = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 释放接口参数
        '----------------------------------------------------------------
        Private Sub releaseInterfaceParameters()

            Try
            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' 释放接口参数
        '----------------------------------------------------------------
        Private Function showModuleData_Main(ByRef strErrMsg As String) As Boolean

            showModuleData_Main = False
            strErrMsg = ""

            Try
                Me.btnZhuanhuan.Enabled = Me.m_blnPrevilegeParams(1)
                Me.btnFanZhuanhuan.Enabled = Me.m_blnPrevilegeParams(2)
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            showModuleData_Main = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取接口参数(没有接口参数则显示错误信息页面)
        '----------------------------------------------------------------
        Private Function getInterfaceParameters(ByRef strErrMsg As String) As Boolean

            Dim strSessionId As String = ""

            getInterfaceParameters = False

            Try
                '没有有接口参数
                Me.m_blnInterface = False

                '获取恢复现场参数
                Me.m_blnSaveScence = False
                If Me.IsPostBack = False Then
                    Me.m_blnSaveScence = False

                    '恢复现场参数后释放该资源
                    Me.restoreModuleInformation(strSessionId)

                    '处理调用模块返回后的信息并同时释放相应资源
                    If Me.getDataFromCallModule(strErrMsg) = False Then
                        GoTo errProc
                    End If
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getInterfaceParameters = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 释放本模块缓存的参数
        '----------------------------------------------------------------
        Private Sub releaseModuleParameters()

            Try
            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' 初始化控件
        '----------------------------------------------------------------
        Private Function initializeControls(ByRef strErrMsg As String) As Boolean

            Dim objControlProcess As New Xydc.Platform.web.ControlProcess

            initializeControls = False

            '仅在第一次调用页面时执行
            If Me.IsPostBack = False Then
                '显示Pannel
                Me.panelMain.Visible = True
                Me.panelError.Visible = Not Me.panelMain.Visible

                '执行键转译(不论是否是“回发”)
                objControlProcess.doTranslateKey(Me.txtDIR)

                '显示信息
                If Me.showModuleData_Main(strErrMsg) = False Then
                    GoTo errProc
                End If
            End If

            Xydc.Platform.web.ControlProcess.SafeRelease(objControlProcess)

            initializeControls = True
            Exit Function

errProc:
            Xydc.Platform.web.ControlProcess.SafeRelease(objControlProcess)
            Exit Function

        End Function

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String
            Dim strUrl As String

            '预处理
            If MyBase.doPagePreprocess(True, Me.IsPostBack And Me.m_blnSaveScence) = True Then
                Exit Sub
            End If

            '检查权限(不论是否回发！)
            Dim blnDo As Boolean
            If Me.getPrevilegeParams(strErrMsg, blnDo) = False Then
                GoTo errProc
            End If
            If blnDo = False Then
                GoTo normExit
            End If

            '获取接口参数
            If Me.getInterfaceParameters(strErrMsg) = False Then
                GoTo errProc
            End If

            '控件初始化
            If Me.initializeControls(strErrMsg) = False Then
                GoTo errProc
            End If

normExit:
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub










        Private Sub doGetFile(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '检查目录
                If Me.txtDIR.Text.Trim = "" Then
                    strErrMsg = "错误：没有指定目录！"
                    GoTo errProc
                End If
                Me.txtDIR.Text = Me.txtDIR.Text.Trim
                If System.IO.Directory.Exists(Me.txtDIR.Text) = False Then
                    strErrMsg = "错误：目录[" + Me.txtDIR.Text + "]不存在！"
                    GoTo errProc
                End If

                '清除列表
                Me.lstFILE.Items.Clear()

                '获取文件
                Dim strFiles() As String
                strFiles = System.IO.Directory.GetFiles(Me.txtDIR.Text)
                Dim intCount As Integer
                Dim i As Integer
                intCount = strFiles.Length
                For i = 0 To intCount - 1 Step 1
                    Me.lstFILE.Items.Add(strFiles(i))
                Next

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub doZhuanhuan(ByVal strControlId As String)

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String
            Dim intStep As Integer

            Try
                intStep = 1
                '检查目录
                If Me.txtDIR.Text.Trim = "" Then
                    strErrMsg = "错误：没有指定目录！"
                    GoTo errProc
                End If
                Me.txtDIR.Text = Me.txtDIR.Text.Trim
                Dim intSelected As Integer = 0
                Dim intCount As Integer
                Dim i As Integer
                intCount = Me.lstFILE.Items.Count
                For i = 0 To intCount - 1 Step 1
                    If Me.lstFILE.Items(i).Selected = True Then
                        intSelected += 1
                    End If
                Next
                If intSelected < 1 Then
                    strErrMsg = "错误：没有指定要转换的文件！"
                    GoTo errProc
                End If

                intStep = 2
                '询问
                If objMessageProcess.isExecuteCode(Request, Me.popMessageObject.UniqueID, intStep) = True Then
                    objMessageProcess.doConfirmMessage(Me.popMessageObject, "警告：您确定要转换选定的[" + intSelected.ToString + "]文件吗（是/否）？", strControlId, intStep)
                    Exit Try
                End If

                intStep = 3
                '处理
                If objMessageProcess.isExecuteCode(Request, Me.popMessageObject.UniqueID, intStep) = True Then
                    Dim strFile As String
                    Dim blnDo As Boolean
                    intCount = Me.lstFILE.Items.Count
                    For i = 0 To intCount - 1 Step 1
                        If Me.lstFILE.Items(i).Selected = True Then
                            strFile = Me.lstFILE.Items(i).Text

                            '存在？
                            If objBaseLocalFile.doFileExisted(strErrMsg, strFile, blnDo) = False Then
                                GoTo errProc
                            End If
                            If blnDo = False Then
                                strErrMsg = "错误：文件[" + strFile + "]不存在！"
                                GoTo errProc
                            End If

                            '转换
                            If objPulicParameters.doEncryptFile(strErrMsg, strFile) = False Then
                                GoTo errProc
                            End If
                        End If
                    Next

                    '提示成功
                    objMessageProcess.doAlertMessage(Me.popMessageObject, "提示：成功转换了选定的[" + intSelected.ToString + "]文件！")
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub doFanZhuanhuan(ByVal strControlId As String)

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String
            Dim intStep As Integer

            Try
                intStep = 1
                '检查目录
                If Me.txtDIR.Text.Trim = "" Then
                    strErrMsg = "错误：没有指定目录！"
                    GoTo errProc
                End If
                Me.txtDIR.Text = Me.txtDIR.Text.Trim
                Dim intSelected As Integer = 0
                Dim intCount As Integer
                Dim i As Integer
                intCount = Me.lstFILE.Items.Count
                For i = 0 To intCount - 1 Step 1
                    If Me.lstFILE.Items(i).Selected = True Then
                        intSelected += 1
                    End If
                Next
                If intSelected < 1 Then
                    strErrMsg = "错误：没有指定要转换的文件！"
                    GoTo errProc
                End If

                intStep = 2
                '询问
                If objMessageProcess.isExecuteCode(Request, Me.popMessageObject.UniqueID, intStep) = True Then
                    objMessageProcess.doConfirmMessage(Me.popMessageObject, "警告：您确定要取消选定的[" + intSelected.ToString + "]文件的转换吗（是/否）？", strControlId, intStep)
                    Exit Try
                End If

                intStep = 3
                '处理
                If objMessageProcess.isExecuteCode(Request, Me.popMessageObject.UniqueID, intStep) = True Then
                    Dim strFile As String
                    Dim blnDo As Boolean
                    intCount = Me.lstFILE.Items.Count
                    For i = 0 To intCount - 1 Step 1
                        If Me.lstFILE.Items(i).Selected = True Then
                            strFile = Me.lstFILE.Items(i).Text

                            '存在？
                            If objBaseLocalFile.doFileExisted(strErrMsg, strFile, blnDo) = False Then
                                GoTo errProc
                            End If
                            If blnDo = False Then
                                strErrMsg = "错误：文件[" + strFile + "]不存在！"
                                GoTo errProc
                            End If

                            '转换
                            If objPulicParameters.doDecryptFile(strErrMsg, strFile) = False Then
                                GoTo errProc
                            End If
                        End If
                    Next

                    '提示成功
                    objMessageProcess.doAlertMessage(Me.popMessageObject, "提示：成功取消了选定的[" + intSelected.ToString + "]文件的转换！")
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub doClose(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                Dim strSessionId As String
                Dim strUrl As String
                If Me.m_blnInterface = True Then
                    '返回到调用模块，并附加返回参数
                    '要返回的SessionId
                    'SessionId附加到返回的Url
                    strSessionId = Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.ISessionId)
                Else
                    strUrl = Xydc.Platform.Common.jsoaConfiguration.GeneralReturnUrl
                End If

                '释放模块资源
                Me.releaseModuleParameters()
                Me.releaseInterfaceParameters()

                '返回
                If strUrl <> "" Then
                    Response.Redirect(strUrl)
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
            Me.doClose("btnCancel")
        End Sub

        Private Sub btnGetFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGetFile.Click
            Me.doGetFile("btnGetFile")
        End Sub

        Private Sub btnZhuanhuan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnZhuanhuan.Click
            Me.doZhuanhuan("btnZhuanhuan")
        End Sub

        Private Sub btnFanZhuanhuan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFanZhuanhuan.Click
            Me.doFanZhuanhuan("btnFanZhuanhuan")
        End Sub

    End Class
End Namespace
