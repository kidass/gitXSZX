Imports System.Web.Security

Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.web
    ' 类名    ：main
    '
    ' 功能描述： 
    '     用户登录检查模块。
    '----------------------------------------------------------------

    Partial Public Class main
        Inherits Xydc.Platform.web.PageBase

        '----------------------------------------------------------------
        '模块私用参数
        '----------------------------------------------------------------
        '本模块相对image的相对路径
        Protected m_cstrRelativePathToImage As String = "../"
        '文件下载后的缓存路径
        Protected m_cstrUrlBaseToFileCache As String = "/temp/filecache/"
        '打印模版相对于应用根的路径
        Protected m_cstrExcelMBRelativePathToAppRoot As String = "/template/excel/"
        '打印文件缓存目录相对于应用根的路径
        Protected m_cstrPrintCacheRelativePathToAppRoot As String = "/temp/printcache/"


        '----------------------------------------------------------------
        '模块现场保留参数，恢复完成后立即释放session资源
        '----------------------------------------------------------------
        Private m_blnSaveScence As Boolean

        '----------------------------------------------------------------
        '模块接口参数
        '----------------------------------------------------------------
        Private m_blnInterface As Boolean









        '----------------------------------------------------------------
        ' 从调用模块中获取数据
        '----------------------------------------------------------------
        Private Function getDataFromCallModule(ByRef strErrMsg As String) As Boolean

            Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate
            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            Try
                If Me.IsPostBack = True Then
                    Exit Try
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            getDataFromCallModule = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取接口参数(没有接口参数则显示错误信息页面)
        '----------------------------------------------------------------
        Private Function getInterfaceParameters(ByRef strErrMsg As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            getInterfaceParameters = False

            Try
                '获取接口参数
                m_blnInterface = False

                '获取恢复现场参数
                Me.m_blnSaveScence = False
                If Me.IsPostBack = False Then
                    '处理调用模块返回后的信息并同时释放相应资源
                    If Me.getDataFromCallModule(strErrMsg) = False Then
                        GoTo errProc
                    End If
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            getInterfaceParameters = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function










        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String
            Dim strUrl As String

            '页面预处理
            If MyBase.doPagePreprocess(True, False) = True Then
                Exit Sub
            End If


            '获取接口参数
            If Me.getInterfaceParameters(strErrMsg) = False Then
                GoTo errProc
            End If


            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub


        '----------------------------------------------------------------
        ' 建筑面积段
        '     strErrMsg      ：返回错误信息
        '     strControlId   ：当前操作控件ID
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function doBuildingAreaInterval( _
            ByRef strErrMsg As String, _
            ByVal strControlId As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim strNewSessionId As String = ""
            Dim strMSessionId As String = ""
            Dim strUrl As String

            doBuildingAreaInterval = False

            Try
                '准备调用接口
                Dim objIDeepData_monthCompute As Xydc.Platform.BusinessFacade.IDeepData_monthCompute
                objIDeepData_monthCompute = New Xydc.Platform.BusinessFacade.IDeepData_monthCompute
                With objIDeepData_monthCompute
                    .iSourceControlId = strControlId
                    strUrl = ""
                    strUrl += Request.Url.AbsolutePath
                    strUrl += "?"
                    strUrl += Xydc.Platform.Common.Utilities.PulicParameters.MSessionId
                    strUrl += "="
                    strUrl += strMSessionId
                    .iReturnUrl = strUrl
                End With

                '调用模块
                strNewSessionId = objPulicParameters.getNewGuid()
                If strNewSessionId = "" Then
                    strErrMsg = "错误：不能初始化调用接口！"
                    GoTo errProc
                End If
                Session.Add(strNewSessionId, objIDeepData_monthCompute)
                strUrl = ""
                strUrl += "./depthData/configuration/deepData_BuildingArea_interval.aspx"
                strUrl += "?"
                strUrl += Xydc.Platform.Common.Utilities.PulicParameters.ISessionId
                strUrl += "="
                strUrl += strNewSessionId
                Response.Redirect(strUrl)

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            doBuildingAreaInterval = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function

        '
        '----------------------------------------------------------------
        ' 套内面积段
        '     strErrMsg      ：返回错误信息
        '     strControlId   ：当前操作控件ID
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function doFloorAreaInterval( _
            ByRef strErrMsg As String, _
            ByVal strControlId As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim strNewSessionId As String = ""
            Dim strMSessionId As String = ""
            Dim strUrl As String

            doFloorAreaInterval = False

            Try
                '准备调用接口
                Dim objIDeepData_monthCompute As Xydc.Platform.BusinessFacade.IDeepData_monthCompute
                objIDeepData_monthCompute = New Xydc.Platform.BusinessFacade.IDeepData_monthCompute
                With objIDeepData_monthCompute
                    .iSourceControlId = strControlId
                    strUrl = ""
                    strUrl += Request.Url.AbsolutePath
                    strUrl += "?"
                    strUrl += Xydc.Platform.Common.Utilities.PulicParameters.MSessionId
                    strUrl += "="
                    strUrl += strMSessionId
                    .iReturnUrl = strUrl
                End With

                '调用模块
                strNewSessionId = objPulicParameters.getNewGuid()
                If strNewSessionId = "" Then
                    strErrMsg = "错误：不能初始化调用接口！"
                    GoTo errProc
                End If
                Session.Add(strNewSessionId, objIDeepData_monthCompute)
                strUrl = ""
                strUrl += "./depthData/configuration/deepData_FloorArea_interval.aspx"
                strUrl += "?"
                strUrl += Xydc.Platform.Common.Utilities.PulicParameters.ISessionId
                strUrl += "="
                strUrl += strNewSessionId
                Response.Redirect(strUrl)

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            doFloorAreaInterval = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 单价段
        '     strErrMsg      ：返回错误信息
        '     strControlId   ：当前操作控件ID
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function doUnitPriceInterval( _
            ByRef strErrMsg As String, _
            ByVal strControlId As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim strNewSessionId As String = ""
            Dim strMSessionId As String = ""
            Dim strUrl As String

            doUnitPriceInterval = False

            Try
                '准备调用接口
                Dim objIDeepData_monthCompute As Xydc.Platform.BusinessFacade.IDeepData_monthCompute
                objIDeepData_monthCompute = New Xydc.Platform.BusinessFacade.IDeepData_monthCompute
                With objIDeepData_monthCompute
                    .iSourceControlId = strControlId
                    strUrl = ""
                    strUrl += Request.Url.AbsolutePath
                    strUrl += "?"
                    strUrl += Xydc.Platform.Common.Utilities.PulicParameters.MSessionId
                    strUrl += "="
                    strUrl += strMSessionId
                    .iReturnUrl = strUrl
                End With

                '调用模块
                strNewSessionId = objPulicParameters.getNewGuid()
                If strNewSessionId = "" Then
                    strErrMsg = "错误：不能初始化调用接口！"
                    GoTo errProc
                End If
                Session.Add(strNewSessionId, objIDeepData_monthCompute)
                strUrl = ""
                strUrl += "./depthData/configuration/deepData_UnitPrice_interval.aspx"
                strUrl += "?"
                strUrl += Xydc.Platform.Common.Utilities.PulicParameters.ISessionId
                strUrl += "="
                strUrl += strNewSessionId
                Response.Redirect(strUrl)

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            doUnitPriceInterval = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function


        '----------------------------------------------------------------
        ' 总价段
        '     strErrMsg      ：返回错误信息
        '     strControlId   ：当前操作控件ID
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function doTotalPriceInterval( _
            ByRef strErrMsg As String, _
            ByVal strControlId As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim strNewSessionId As String = ""
            Dim strMSessionId As String = ""
            Dim strUrl As String

            doTotalPriceInterval = False

            Try
                '准备调用接口
                Dim objIDeepData_monthCompute As Xydc.Platform.BusinessFacade.IDeepData_monthCompute
                objIDeepData_monthCompute = New Xydc.Platform.BusinessFacade.IDeepData_monthCompute
                With objIDeepData_monthCompute
                    .iSourceControlId = strControlId
                    strUrl = ""
                    strUrl += Request.Url.AbsolutePath
                    strUrl += "?"
                    strUrl += Xydc.Platform.Common.Utilities.PulicParameters.MSessionId
                    strUrl += "="
                    strUrl += strMSessionId
                    .iReturnUrl = strUrl
                End With

                '调用模块
                strNewSessionId = objPulicParameters.getNewGuid()
                If strNewSessionId = "" Then
                    strErrMsg = "错误：不能初始化调用接口！"
                    GoTo errProc
                End If
                Session.Add(strNewSessionId, objIDeepData_monthCompute)
                strUrl = ""
                strUrl += "./depthData/configuration/deepData_TotalPrice_interval.aspx"
                strUrl += "?"
                strUrl += Xydc.Platform.Common.Utilities.PulicParameters.ISessionId
                strUrl += "="
                strUrl += strNewSessionId
                Response.Redirect(strUrl)

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            doTotalPriceInterval = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 单位人员
        '     strErrMsg      ：返回错误信息
        '     strControlId   ：当前操作控件ID
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function doOpenDepartmentEmployee( _
            ByRef strErrMsg As String, _
            ByVal strControlId As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim strNewSessionId As String = ""
            Dim strMSessionId As String = ""
            Dim strUrl As String

            doOpenDepartmentEmployee = False

            Try
                '准备调用接口
                Dim objIDmxzZzry As Xydc.Platform.BusinessFacade.IDmxzZzry
                objIDmxzZzry = New Xydc.Platform.BusinessFacade.IDmxzZzry
                With objIDmxzZzry
                    .iSourceControlId = strControlId
                    strUrl = ""
                    strUrl += Request.Url.AbsolutePath
                    strUrl += "?"
                    strUrl += Xydc.Platform.Common.Utilities.PulicParameters.MSessionId
                    strUrl += "="
                    strUrl += strMSessionId
                    .iReturnUrl = strUrl
                End With

                '调用模块
                strNewSessionId = objPulicParameters.getNewGuid()
                If strNewSessionId = "" Then
                    strErrMsg = "错误：不能初始化调用接口！"
                    GoTo errProc
                End If
                Session.Add(strNewSessionId, objIDmxzZzry)
                strUrl = ""
                strUrl += "./bmry/ggdm_bmry.aspx"
                strUrl += "?"
                strUrl += Xydc.Platform.Common.Utilities.PulicParameters.ISessionId
                strUrl += "="
                strUrl += strNewSessionId
                Response.Redirect(strUrl)

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            doOpenDepartmentEmployee = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function


        '----------------------------------------------------------------
        ' 阳光家缘综合查询
        '     strErrMsg      ：返回错误信息
        '     strControlId   ：当前操作控件ID
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function doSunshineSearch( _
            ByRef strErrMsg As String, _
            ByVal strControlId As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim strNewSessionId As String = ""
            Dim strMSessionId As String = ""
            Dim strUrl As String

            doSunshineSearch = False

            Try
                '准备调用接口
                Dim objIDeepData_monthCompute As Xydc.Platform.BusinessFacade.IDeepData_monthCompute
                objIDeepData_monthCompute = New Xydc.Platform.BusinessFacade.IDeepData_monthCompute
                With objIDeepData_monthCompute
                    .iSourceControlId = strControlId
                    strUrl = ""
                    strUrl += Request.Url.AbsolutePath
                    strUrl += "?"
                    strUrl += Xydc.Platform.Common.Utilities.PulicParameters.MSessionId
                    strUrl += "="
                    strUrl += strMSessionId
                    .iReturnUrl = strUrl
                End With

                '调用模块
                strNewSessionId = objPulicParameters.getNewGuid()
                If strNewSessionId = "" Then
                    strErrMsg = "错误：不能初始化调用接口！"
                    GoTo errProc
                End If
                Session.Add(strNewSessionId, objIDeepData_monthCompute)
                strUrl = ""

                Select Case strControlId
                    Case "mnuSJPZ_1005"
                        strUrl += "./sunshineData/configuration/sunhineData_House_Sort.aspx"

                    Case "mnuSJPZ_1006"
                        strUrl += "./sunshineData/configuration/sunshineData_MonthMatchHouse.aspx"

                    Case "mnuSJPZ_1007"
                        strUrl += "./sunshineData/configuration/sunhineData_House_Sort.aspx"

                    Case "mnuSunshine_002"
                        strUrl += "./sunshineData/configuration/sunshineData_buildingVerify_x2.aspx"

                    Case "mnuSunshine_007"
                        strUrl += "./sunshineData/configuration/sunshineData_houseAveragePrice_compute.aspx"

                    Case Else
                        strUrl += "./sunshineData/configuration/sunshineData_buildingVerify.aspx"
                End Select

                strUrl += "?"
                strUrl += Xydc.Platform.Common.Utilities.PulicParameters.ISessionId
                strUrl += "="
                strUrl += strNewSessionId
                Response.Redirect(strUrl)

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            doSunshineSearch = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function



        Private Sub lnkMenu_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkMenu.Click

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '获取点击菜单
                Dim strMenuId As String = Me.htxtSelectMenuID.Value
                '处理菜单命令
                Select Case strMenuId.ToUpper()
                    '--阳光家缘数据
                    '综合查询
                    Case "mnuSunshine_001".ToUpper()
                        If Me.doSunshineSearch(strErrMsg, "lnkMenu") = False Then
                            GoTo errProc
                        End If

                    Case "mnuSunshine_002".ToUpper()
                        If Me.doSunshineSearch(strErrMsg, "mnuSunshine_002") = False Then
                            GoTo errProc
                        End If

                    Case "mnuSunshine_007".ToUpper()
                        If Me.doSunshineSearch(strErrMsg, "mnuSunshine_007") = False Then
                            GoTo errProc
                        End If


                        '--月度深度数据
                        '建筑面积段
                    Case "mnuSJPZ_1001".ToUpper()
                        If Me.doBuildingAreaInterval(strErrMsg, "lnkMenu") = False Then
                            GoTo errProc
                        End If
                        '套内面积段
                    Case "mnuSJPZ_1002".ToUpper()
                        If Me.doFloorAreaInterval(strErrMsg, "lnkMenu") = False Then
                            GoTo errProc
                        End If
                        '单价段
                    Case "mnuSJPZ_1003".ToUpper()
                        If Me.doUnitPriceInterval(strErrMsg, "lnkMenu") = False Then
                            GoTo errProc
                        End If

                        '总价段
                    Case "mnuSJPZ_1004".ToUpper()
                        If Me.doTotalPriceInterval(strErrMsg, "lnkMenu") = False Then
                            GoTo errProc
                        End If

                        '单位人员
                    Case "mnuXTPZ_2001".ToUpper()
                        If Me.doOpenDepartmentEmployee(strErrMsg, "lnkMenu") = False Then
                            GoTo errProc
                        End If

                    Case "mnuSJPZ_1005".ToUpper()
                        If Me.doSunshineSearch(strErrMsg, "mnuSJPZ_1005") = False Then
                            GoTo errProc
                        End If

                    Case "mnuSJPZ_1006".ToUpper()
                        If Me.doSunshineSearch(strErrMsg, "mnuSJPZ_1006") = False Then
                            GoTo errProc
                        End If

                    Case "mnuSJPZ_1007".ToUpper()
                        If Me.doSunshineSearch(strErrMsg, "mnuSJPZ_1007") = False Then
                            GoTo errProc
                        End If

                    Case Else
                End Select

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

    End Class
End Namespace