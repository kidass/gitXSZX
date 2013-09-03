Imports System.Web.Security

Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.web
    ' 类名    ：dmxz_zzry
    ' 
    ' 调用性质：
    '     可被其他模块调用，本身不调用其他模块
    '
    ' 功能描述： 
    '     选择范围、组织、人员。
    '
    ' 接口参数：
    '     参见IDmxzZzry接口类描述
    '----------------------------------------------------------------

    Partial Public Class dmxz_zzry
        Inherits Xydc.Platform.web.PageBase


        '----------------------------------------------------------------
        '模块私用参数
        '----------------------------------------------------------------
        '本模块相对image的相对路径
        Private m_cstrRelativePathToImage As String = "../../"

        '----------------------------------------------------------------
        '与数据网格grdBMRY相关的参数
        '----------------------------------------------------------------
        '网格中模板列中的控件ID
        Private Const m_cstrCheckBoxIdInDataGrid_BMRY As String = "chkBMRY"
        '包含网格的DIV对象ID
        Private Const m_cstrDataGridInDIV_BMRY As String = "divBMRY"
        '网格要锁定的列数
        Private m_intFixedColumns_BMRY As Integer

        '----------------------------------------------------------------
        '与数据网格grdFWLIST相关的参数
        '----------------------------------------------------------------
        '网格中模板列中的控件ID
        Private Const m_cstrCheckBoxIdInDataGrid_FWLIST As String = "chkFWLIST"
        '包含网格的DIV对象ID
        Private Const m_cstrDataGridInDIV_FWLIST As String = "divFWLIST"
        '网格要锁定的列数
        Private m_intFixedColumns_FWLIST As Integer

        '----------------------------------------------------------------
        '与数据网格grdJCLXR相关的参数
        '----------------------------------------------------------------
        '网格中模板列中的控件ID
        Private Const m_cstrCheckBoxIdInDataGrid_JCLXR As String = "chkJCLXR"
        '包含网格的DIV对象ID
        Private Const m_cstrDataGridInDIV_JCLXR As String = "divJCLXR"
        '网格要锁定的列数
        Private m_intFixedColumns_JCLXR As Integer

        '----------------------------------------------------------------
        '与数据网格grdSELRY相关的参数
        '----------------------------------------------------------------
        '网格中模板列中的控件ID
        Private Const m_cstrCheckBoxIdInDataGrid_SELRY As String = "chkSELRY"
        '包含网格的DIV对象ID
        Private Const m_cstrDataGridInDIV_SELRY As String = "divSELRY"
        '网格要锁定的列数
        Private m_intFixedColumns_SELRY As Integer

        '----------------------------------------------------------------
        '模块接口参数
        '----------------------------------------------------------------
        Private m_objIDmxzZzry As Xydc.Platform.BusinessFacade.IDmxzZzry

        '----------------------------------------------------------------
        '要访问的数据
        '----------------------------------------------------------------
        Private m_objDataSet_BMXX As Xydc.Platform.Common.Data.CustomerData
        Private m_objDataSet_BMRY As Xydc.Platform.Common.Data.CustomerData
        Private m_strQuery_BMRY As String '记录m_objDataSet_BMRY搜索串
        Private m_intRows_BMRY As Integer '记录m_objDataSet_BMRY的DefaultView记录数
        Private m_objDataSet_FWLIST As Xydc.Platform.Common.Data.FenfafanweiData
        Private m_strQuery_FWLIST As String '记录m_objDataSet_FWLIST搜索串
        Private m_intRows_FWLIST As Integer '记录m_objDataSet_FWLIST的DefaultView记录数
        Private m_objDataSet_JCLXR As Xydc.Platform.Common.Data.JingchanglianxirenData
        Private m_strQuery_JCLXR As String '记录m_objDataSet_JCLXR搜索串
        Private m_intRows_JCLXR As Integer '记录m_objDataSet_JCLXR的DefaultView记录数
        Private m_objDataSet_SELRY As Xydc.Platform.Common.Data.CustomerData
        Private m_strSessionId_SELRY As String '缓存m_objDataSet_SELRY的SessionId

        '----------------------------------------------------------------
        '其他参数
        '----------------------------------------------------------------
        '发送限制条件(where子句)
        Private m_strRenyuanRestrictWhere As String
        Private m_strSendRestrictWhere As String










        '----------------------------------------------------------------
        ' 释放接口参数
        '----------------------------------------------------------------
        Private Sub releaseInterfaceParameters()

            Try
                If Not (Me.m_objIDmxzZzry Is Nothing) Then
                    If Me.m_objIDmxzZzry.iInterfaceType = Xydc.Platform.BusinessFacade.ICallInterface.enumInterfaceType.InputOnly Then
                        '释放Session
                        Session.Remove(Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.ISessionId))
                        '释放对象
                        Me.m_objIDmxzZzry.Dispose()
                        Me.m_objIDmxzZzry = Nothing
                    End If
                End If
            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' 获取接口参数(没有接口参数则显示错误信息页面)
        '----------------------------------------------------------------
        Private Function getInterfaceParameters(ByRef strErrMsg As String) As Boolean

            getInterfaceParameters = False

            '从QueryString中解析接口参数(不论是否回发)
            Dim objTemp As Object
            Try
                objTemp = Session.Item(Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.ISessionId))
                m_objIDmxzZzry = CType(objTemp, Xydc.Platform.BusinessFacade.IDmxzZzry)
            Catch ex As Exception
                m_objIDmxzZzry = Nothing
            End Try

            '必须有接口参数
            If m_objIDmxzZzry Is Nothing Then
                '显示错误信息
                Me.panelError.Visible = True
                Me.panelMain.Visible = Not Me.panelError.Visible
                strErrMsg = "本模块必须提供输入接口参数！"
                GoTo errProc
            End If

            '获取局部接口参数
            Me.m_strSessionId_SELRY = Me.htxtSessionIdSELRY.Value

            With New Xydc.Platform.Common.Utilities.PulicParameters
                '记录m_objDataSet_BMRY的DefaultView记录数
                Me.m_intRows_BMRY = .getObjectValue(Me.htxtBMRYRows.Value, 0)

                '记录m_objDataSet_FWLIST的DefaultView记录数
                Me.m_intRows_FWLIST = .getObjectValue(Me.htxtFWLISTRows.Value, 0)

                '记录m_objDataSet_JCLXR的DefaultView记录数
                Me.m_intRows_JCLXR = .getObjectValue(Me.htxtJCLXRRows.Value, 0)

                Me.m_intFixedColumns_BMRY = .getObjectValue(Me.htxtBMRYFixed.Value, 0)
                Me.m_intFixedColumns_FWLIST = .getObjectValue(Me.htxtFWLISTFixed.Value, 0)
                Me.m_intFixedColumns_JCLXR = .getObjectValue(Me.htxtJCLXRFixed.Value, 0)
                Me.m_intFixedColumns_SELRY = .getObjectValue(Me.htxtSELRYFixed.Value, 0)
            End With

            If Me.m_objIDmxzZzry.iSendRestrict = True Then
                With New Xydc.Platform.BusinessFacade.systemCustomer
                    Dim strArray() As String
                    If Me.m_objIDmxzZzry.iWeiTuoRen.Trim = "" Then
                        strArray = Nothing
                    Else
                        strArray = Me.m_objIDmxzZzry.iWeiTuoRen.Split(Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate.ToCharArray)
                    End If
                    Me.m_strSendRestrictWhere = .getSendRestrictWhere(Me.m_objIDmxzZzry.iCurrentBlr, strArray)
                End With
            Else
                Me.m_strSendRestrictWhere = ""
            End If
            If Me.m_objIDmxzZzry.iRestrictRenyuanList = True Then
                Me.m_strRenyuanRestrictWhere = Me.m_objIDmxzZzry.iRestrictRenyuanListSQL
            Else
                Me.m_strRenyuanRestrictWhere = ""
            End If

            If Me.IsPostBack = False Then
                Dim strQuery As String
                Dim strRYDM As String

                'BMRY的默认搜索条件
                strRYDM = "a." + Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYDM
                strQuery = ""
                If Me.m_strSendRestrictWhere <> "" Then
                    If strQuery <> "" Then
                        strQuery = strQuery + vbCr + " and " + vbCr + strRYDM + " in (" + vbCr + Me.m_strSendRestrictWhere + vbCr + ")" + vbCr
                    Else
                        strQuery = strRYDM + " in (" + vbCr + Me.m_strSendRestrictWhere + vbCr + ")" + vbCr
                    End If
                End If
                If Me.m_strRenyuanRestrictWhere <> "" Then
                    If strQuery <> "" Then
                        strQuery = strQuery + vbCr + " and " + vbCr + strRYDM + " in (" + vbCr + Me.m_strRenyuanRestrictWhere + vbCr + ")" + vbCr
                    Else
                        strQuery = strRYDM + " in (" + vbCr + Me.m_strRenyuanRestrictWhere + vbCr + ")" + vbCr
                    End If
                End If
                Me.m_strQuery_BMRY = strQuery

                'JCLXR的默认搜索条件
                strRYDM = "a." + Xydc.Platform.Common.Data.JingchanglianxirenData.FIELD_GW_B_JINGCHANGLIANXIREN_LXRDM
                strQuery = ""
                If Me.m_strSendRestrictWhere <> "" Then
                    If strQuery <> "" Then
                        strQuery = strQuery + vbCr + " and " + vbCr + strRYDM + " in (" + vbCr + Me.m_strSendRestrictWhere + vbCr + ")" + vbCr
                    Else
                        strQuery = strRYDM + " in (" + vbCr + Me.m_strSendRestrictWhere + vbCr + ")" + vbCr
                    End If
                End If
                If Me.m_strRenyuanRestrictWhere <> "" Then
                    If strQuery <> "" Then
                        strQuery = strQuery + vbCr + " and " + vbCr + strRYDM + " in (" + vbCr + Me.m_strRenyuanRestrictWhere + vbCr + ")" + vbCr
                    Else
                        strQuery = strRYDM + " in (" + vbCr + Me.m_strRenyuanRestrictWhere + vbCr + ")" + vbCr
                    End If
                End If
                Me.m_strQuery_JCLXR = strQuery

                'FWLIST的默认搜索条件
                Me.m_strQuery_FWLIST = ""

                '保存搜索条件
                Me.htxtBMRYQuery.Value = Me.m_strQuery_BMRY
                Me.htxtFWLISTQuery.Value = Me.m_strQuery_FWLIST
                Me.htxtJCLXRQuery.Value = Me.m_strQuery_JCLXR
            Else
                Me.m_strQuery_BMRY = Me.htxtBMRYQuery.Value
                Me.m_strQuery_FWLIST = Me.htxtFWLISTQuery.Value
                Me.m_strQuery_JCLXR = Me.htxtJCLXRQuery.Value
            End If

            getInterfaceParameters = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 释放本模块缓存的参数
        '----------------------------------------------------------------
        Private Sub releaseModuleParameters()

            'Try
            '    If Not (Me.m_objDataSet_SELRY Is Nothing) Then
            '        '释放Session
            '        Session.Remove(Me.m_strSessionId_SELRY)
            '        '释放对象
            '        '对象用于返回，不能释放
            '    End If
            'Catch ex As Exception
            'End Try
            Try

                If Me.m_strSessionId_SELRY.Trim <> "" Then
                    Dim objTempDataSet As Xydc.Platform.Common.Data.CustomerData = Nothing
                    Try
                        objTempDataSet = CType(Session(Me.m_strSessionId_SELRY), Xydc.Platform.Common.Data.CustomerData)
                    Catch ex As Exception
                        objTempDataSet = Nothing
                    End Try
                    Xydc.Platform.Common.Data.CustomerData.SafeRelease(objTempDataSet)
                    Session.Remove(Me.m_strSessionId_SELRY)
                End If

            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' 获取grdBMRY的搜索条件(默认表前缀a.)
        '     strErrMsg      ：返回错误信息
        '     strQuery       ：返回的搜索条件
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function getQueryString_BMRY( _
            ByRef strErrMsg As String, _
            ByRef strQuery As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            getQueryString_BMRY = False
            strQuery = ""

            Try
                '按人员名称搜索
                Dim strRYMC As String = "a." + Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYMC
                If Me.txtBMRYSearch_RYMC.Text.Length > 0 Then Me.txtBMRYSearch_RYMC.Text = Me.txtBMRYSearch_RYMC.Text.Trim()
                If Me.txtBMRYSearch_RYMC.Text <> "" Then
                    If strQuery = "" Then
                        strQuery = strRYMC + " like '" + Me.txtBMRYSearch_RYMC.Text + "%'"
                    Else
                        strQuery = strQuery + " and " + strRYMC + " like '" + Me.txtBMRYSearch_RYMC.Text + "%'"
                    End If
                End If

                '按部门名称搜索
                Dim strBMMC As String = "a." + Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_ZZMC
                If Me.txtBMRYSearch_BMMC.Text.Length > 0 Then Me.txtBMRYSearch_BMMC.Text = Me.txtBMRYSearch_BMMC.Text.Trim()
                If Me.txtBMRYSearch_BMMC.Text <> "" Then
                    If strQuery = "" Then
                        strQuery = strBMMC + " like '" + Me.txtBMRYSearch_BMMC.Text + "%'"
                    Else
                        strQuery = strQuery + " and " + strBMMC + " like '" + Me.txtBMRYSearch_BMMC.Text + "%'"
                    End If
                End If

                '按人员序号搜索
                Dim strRYXH As String = "a." + Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYXH
                Dim intMin As Integer
                Dim intMax As Integer
                If Me.txtBMRYSearch_RYXHMin.Text.Length > 0 Then Me.txtBMRYSearch_RYXHMin.Text = Me.txtBMRYSearch_RYXHMin.Text.Trim()
                If Me.txtBMRYSearch_RYXHMax.Text.Length > 0 Then Me.txtBMRYSearch_RYXHMax.Text = Me.txtBMRYSearch_RYXHMax.Text.Trim()
                If Me.txtBMRYSearch_RYXHMin.Text <> "" And Me.txtBMRYSearch_RYXHMax.Text <> "" Then
                    intMin = objPulicParameters.getObjectValue(Me.txtBMRYSearch_RYXHMin.Text, 1)
                    intMax = objPulicParameters.getObjectValue(Me.txtBMRYSearch_RYXHMax.Text, 1)
                    If intMin > intMax Then
                        Me.txtBMRYSearch_RYXHMin.Text = intMax.ToString()
                        Me.txtBMRYSearch_RYXHMax.Text = intMin.ToString()
                    Else
                        Me.txtBMRYSearch_RYXHMin.Text = intMin.ToString()
                        Me.txtBMRYSearch_RYXHMax.Text = intMax.ToString()
                    End If
                    If strQuery = "" Then
                        strQuery = strRYXH + " between " + Me.txtBMRYSearch_RYXHMin.Text + " and " + Me.txtBMRYSearch_RYXHMax.Text
                    Else
                        strQuery = strQuery + " and " + strRYXH + " between " + Me.txtBMRYSearch_RYXHMin.Text + " and " + Me.txtBMRYSearch_RYXHMax.Text
                    End If
                ElseIf Me.txtBMRYSearch_RYXHMin.Text <> "" Then
                    intMin = objPulicParameters.getObjectValue(Me.txtBMRYSearch_RYXHMin.Text, 1)
                    Me.txtBMRYSearch_RYXHMin.Text = intMin.ToString()
                    If strQuery = "" Then
                        strQuery = strRYXH + " >= " + Me.txtBMRYSearch_RYXHMin.Text
                    Else
                        strQuery = strQuery + " and " + strRYXH + " >= " + Me.txtBMRYSearch_RYXHMin.Text
                    End If
                ElseIf Me.txtBMRYSearch_RYXHMax.Text <> "" Then
                    intMax = objPulicParameters.getObjectValue(Me.txtBMRYSearch_RYXHMax.Text, 1)
                    Me.txtBMRYSearch_RYXHMax.Text = intMax.ToString()
                    If strQuery = "" Then
                        strQuery = strRYXH + " <= " + Me.txtBMRYSearch_RYXHMax.Text
                    Else
                        strQuery = strQuery + " and " + strRYXH + " <= " + Me.txtBMRYSearch_RYXHMax.Text
                    End If
                Else
                End If

                '按行政级别搜索
                Dim strJBMC As String = "a." + Xydc.Platform.Common.Data.XingzhengjibieData.FIELD_GG_B_XINGZHENGJIBIE_JBMC
                If Me.txtBMRYSearch_RYJBMC.Text.Length > 0 Then Me.txtBMRYSearch_RYJBMC.Text = Me.txtBMRYSearch_RYJBMC.Text.Trim()
                If Me.txtBMRYSearch_RYJBMC.Text <> "" Then
                    If strQuery = "" Then
                        strQuery = strJBMC + " like '" + Me.txtBMRYSearch_RYJBMC.Text + "%'"
                    Else
                        strQuery = strQuery + " and " + strJBMC + " like '" + Me.txtBMRYSearch_RYJBMC.Text + "%'"
                    End If
                End If

                '按担任职务搜索
                Dim strGWLB As String = "a." + Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_FULLJOIN_GWLB
                If Me.txtBMRYSearch_RYDRZW.Text.Length > 0 Then Me.txtBMRYSearch_RYDRZW.Text = Me.txtBMRYSearch_RYDRZW.Text.Trim()
                If Me.txtBMRYSearch_RYDRZW.Text <> "" Then
                    If strQuery = "" Then
                        strQuery = strGWLB + " like '" + Me.txtBMRYSearch_RYDRZW.Text + "%'"
                    Else
                        strQuery = strQuery + " and " + strGWLB + " like '" + Me.txtBMRYSearch_RYDRZW.Text + "%'"
                    End If
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            getQueryString_BMRY = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取grdFWLIST的搜索条件(默认表前缀a.)
        '     strErrMsg      ：返回错误信息
        '     strQuery       ：返回的搜索条件
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function getQueryString_FWLIST( _
            ByRef strErrMsg As String, _
            ByRef strQuery As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            getQueryString_FWLIST = False
            strQuery = ""

            Try
                '按范围名称搜索
                Dim strFWMC As String = "a." + Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_FWMC
                If Me.txtFWLISTSearch_FWMC.Text.Length > 0 Then Me.txtFWLISTSearch_FWMC.Text = Me.txtFWLISTSearch_FWMC.Text.Trim()
                If Me.txtFWLISTSearch_FWMC.Text <> "" Then
                    Me.txtFWLISTSearch_FWMC.Text = objPulicParameters.getNewSearchString(Me.txtFWLISTSearch_FWMC.Text)
                    If strQuery = "" Then
                        strQuery = strFWMC + " like '" + Me.txtFWLISTSearch_FWMC.Text + "%'"
                    Else
                        strQuery = strQuery + " and " + strFWMC + " like '" + Me.txtFWLISTSearch_FWMC.Text + "%'"
                    End If
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            getQueryString_FWLIST = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取grdJCLXR的搜索条件(默认表前缀a.)
        '     strErrMsg      ：返回错误信息
        '     strQuery       ：返回的搜索条件
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function getQueryString_JCLXR( _
            ByRef strErrMsg As String, _
            ByRef strQuery As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            getQueryString_JCLXR = False
            strQuery = ""

            Try
                '按联系人名称搜索
                Dim strRYMC As String = "a." + Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYMC
                If Me.txtJCLXRSearch_RYMC.Text.Length > 0 Then Me.txtJCLXRSearch_RYMC.Text = Me.txtJCLXRSearch_RYMC.Text.Trim()
                If Me.txtJCLXRSearch_RYMC.Text <> "" Then
                    Me.txtJCLXRSearch_RYMC.Text = objPulicParameters.getNewSearchString(Me.txtJCLXRSearch_RYMC.Text)
                    If strQuery = "" Then
                        strQuery = strRYMC + " like '" + Me.txtJCLXRSearch_RYMC.Text + "%'"
                    Else
                        strQuery = strQuery + " and " + strRYMC + " like '" + Me.txtJCLXRSearch_RYMC.Text + "%'"
                    End If
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            getQueryString_JCLXR = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取grdSELRY的搜索条件(由于缓存了数据，采用RowFilter方式)
        '     strErrMsg      ：返回错误信息
        '     strQuery       ：返回的搜索条件
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function getQueryString_SELRY( _
            ByRef strErrMsg As String, _
            ByRef strQuery As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            getQueryString_SELRY = False
            strQuery = ""

            Try
                '个人/单位/范围的名称搜索
                Dim strXZMC As String = Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_MC
                If Me.txtSELRYSearch_XZMC.Text.Length > 0 Then Me.txtSELRYSearch_XZMC.Text = Me.txtSELRYSearch_XZMC.Text.Trim()
                If Me.txtSELRYSearch_XZMC.Text <> "" Then
                    Me.txtSELRYSearch_XZMC.Text = objPulicParameters.getNewSearchString(Me.txtSELRYSearch_XZMC.Text)
                    If strQuery = "" Then
                        strQuery = strXZMC + " like '" + Me.txtSELRYSearch_XZMC.Text + "%'"
                    Else
                        strQuery = strQuery + " and " + strXZMC + " like '" + Me.txtSELRYSearch_XZMC.Text + "%'"
                    End If
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            getQueryString_SELRY = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取tvwBMLIST要显示的数据信息
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function getModuleData_BMXX( _
            ByRef strErrMsg As String) As Boolean

            Dim objsystemCustomer As New Xydc.Platform.BusinessFacade.systemCustomer

            getModuleData_BMXX = False

            Try
                '释放资源
                If Not (Me.m_objDataSet_BMXX Is Nothing) Then
                    Me.m_objDataSet_BMXX.Dispose()
                    Me.m_objDataSet_BMXX = Nothing
                End If

                '重新检索数据
                If objsystemCustomer.getBumenData(strErrMsg, MyBase.UserId, MyBase.UserPassword, Me.m_objDataSet_BMXX) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.BusinessFacade.systemCustomer.SafeRelease(objsystemCustomer)

            getModuleData_BMXX = True
            Exit Function

errProc:
            Xydc.Platform.BusinessFacade.systemCustomer.SafeRelease(objsystemCustomer)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取grdBMRY要显示的数据信息
        '     strErrMsg      ：返回错误信息
        '     strWhere       ：搜索条件
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function getModuleData_BMRY( _
            ByRef strErrMsg As String, _
            ByVal strWhere As String) As Boolean

            Dim objsystemCustomer As New Xydc.Platform.BusinessFacade.systemCustomer
            Dim objTreeviewProcess As New Xydc.Platform.web.TreeviewProcess

            getModuleData_BMRY = False

            Try
                '从TreeView中获取组织代码
                Dim objTreeNode As Microsoft.Web.UI.WebControls.TreeNode
                Dim strZZDM As String
                If Me.tvwBMLIST.SelectedNodeIndex = "" Then
                    strErrMsg = "错误：没有选择单位！"
                    GoTo errProc
                End If
                objTreeNode = Me.tvwBMLIST.GetNodeFromIndex(Me.tvwBMLIST.SelectedNodeIndex)
                If objTreeNode Is Nothing Then
                    strErrMsg = "错误：没有选择单位！"
                    GoTo errProc
                End If
                strZZDM = objTreeviewProcess.getCodeValueFromNodeId(objTreeNode.ID)

                '备份Sort字符串
                Dim strSort As String
                strSort = Me.htxtBMRYSort.Value
                If strSort.Length > 0 Then strSort = strSort.Trim

                '释放资源
                If Not (Me.m_objDataSet_BMRY Is Nothing) Then
                    Me.m_objDataSet_BMRY.Dispose()
                    Me.m_objDataSet_BMRY = Nothing
                End If

                '重新检索数据
                If objsystemCustomer.getRenyuanInBumenData(strErrMsg, MyBase.UserId, MyBase.UserPassword, strZZDM, True, strWhere, Me.m_objDataSet_BMRY) = False Then
                    GoTo errProc
                End If

                '恢复Sort字符串
                With Me.m_objDataSet_BMRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN)
                    .DefaultView.Sort = strSort
                End With

                '缓存参数
                With Me.m_objDataSet_BMRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN)
                    Me.htxtBMRYRows.Value = .DefaultView.Count.ToString()
                    Me.m_intRows_BMRY = .DefaultView.Count
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.BusinessFacade.systemCustomer.SafeRelease(objsystemCustomer)
            Xydc.Platform.web.TreeviewProcess.SafeRelease(objTreeviewProcess)

            getModuleData_BMRY = True
            Exit Function

errProc:
            Xydc.Platform.BusinessFacade.systemCustomer.SafeRelease(objsystemCustomer)
            Xydc.Platform.web.TreeviewProcess.SafeRelease(objTreeviewProcess)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取grdFWLIST要显示的数据信息
        '     strErrMsg      ：返回错误信息
        '     strWhere       ：搜索条件
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function getModuleData_FWLIST( _
            ByRef strErrMsg As String, _
            ByVal strWhere As String) As Boolean

            Dim objsystemFenfafanwei As New Xydc.Platform.BusinessFacade.systemFenfafanwei

            getModuleData_FWLIST = False

            Try
                '备份Sort字符串
                Dim strSort As String
                strSort = Me.htxtFWLISTSort.Value
                If strSort.Length > 0 Then strSort = strSort.Trim

                '释放资源
                If Not (Me.m_objDataSet_FWLIST Is Nothing) Then
                    Me.m_objDataSet_FWLIST.Dispose()
                    Me.m_objDataSet_FWLIST = Nothing
                End If

                '重新检索数据
                If objsystemFenfafanwei.getFenfafanweiData(strErrMsg, MyBase.UserId, MyBase.UserPassword, strWhere, Me.m_objDataSet_FWLIST) = False Then
                    GoTo errProc
                End If

                '恢复Sort字符串
                With Me.m_objDataSet_FWLIST.Tables(Xydc.Platform.Common.Data.FenfafanweiData.TABLE_GW_B_FENFAFANWEI)
                    .DefaultView.Sort = strSort
                End With

                '缓存参数
                With Me.m_objDataSet_FWLIST.Tables(Xydc.Platform.Common.Data.FenfafanweiData.TABLE_GW_B_FENFAFANWEI)
                    Me.htxtFWLISTRows.Value = .DefaultView.Count.ToString()
                    Me.m_intRows_FWLIST = .DefaultView.Count
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.BusinessFacade.systemFenfafanwei.SafeRelease(objsystemFenfafanwei)

            getModuleData_FWLIST = True
            Exit Function

errProc:
            Xydc.Platform.BusinessFacade.systemFenfafanwei.SafeRelease(objsystemFenfafanwei)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取grdJCLXR要显示的数据信息
        '     strErrMsg      ：返回错误信息
        '     strWhere       ：搜索条件
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function getModuleData_JCLXR( _
            ByRef strErrMsg As String, _
            ByVal strWhere As String) As Boolean

            Dim objsystemJingchanglianxiren As New Xydc.Platform.BusinessFacade.systemJingchanglianxiren

            getModuleData_JCLXR = False

            Try
                '备份Sort字符串
                Dim strSort As String
                strSort = Me.htxtJCLXRSort.Value
                If strSort.Length > 0 Then strSort = strSort.Trim

                '释放资源
                If Not (Me.m_objDataSet_JCLXR Is Nothing) Then
                    Me.m_objDataSet_JCLXR.Dispose()
                    Me.m_objDataSet_JCLXR = Nothing
                End If

                '重新检索数据
                If objsystemJingchanglianxiren.getJclxrData(strErrMsg, MyBase.UserId, MyBase.UserPassword, MyBase.UserId, strWhere, Me.m_objDataSet_JCLXR) = False Then
                    GoTo errProc
                End If

                '恢复Sort字符串
                With Me.m_objDataSet_JCLXR.Tables(Xydc.Platform.Common.Data.JingchanglianxirenData.TABLE_GW_B_JINGCHANGLIANXIREN)
                    .DefaultView.Sort = strSort
                End With

                '缓存参数
                With Me.m_objDataSet_JCLXR.Tables(Xydc.Platform.Common.Data.JingchanglianxirenData.TABLE_GW_B_JINGCHANGLIANXIREN)
                    Me.htxtJCLXRRows.Value = .DefaultView.Count.ToString()
                    Me.m_intRows_JCLXR = .DefaultView.Count
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.BusinessFacade.systemJingchanglianxiren.SafeRelease(objsystemJingchanglianxiren)

            getModuleData_JCLXR = True
            Exit Function

errProc:
            Xydc.Platform.BusinessFacade.systemJingchanglianxiren.SafeRelease(objsystemJingchanglianxiren)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取grdSELRY要显示的数据信息，并进行session缓存
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function getModuleData_SELRY( _
            ByRef strErrMsg As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            getModuleData_SELRY = False

            Dim strGuid As String
            Try
                If Me.IsPostBack = False Then
                    '获取Session的Id
                    strGuid = objPulicParameters.getNewGuid()
                    If strGuid = "" Then
                        strErrMsg = "无法产生GUID！"
                        GoTo errProc
                    End If

                    '初次调用空数据
                    Me.m_objDataSet_SELRY = New Xydc.Platform.Common.Data.CustomerData(Xydc.Platform.Common.Data.CustomerData.enumTableType.GG_B_RENYUAN_SELECT)

                    '根据初始值设置信息
                    If Me.m_objIDmxzZzry.iRenyuanList <> "" Then
                        Dim strValue() As String
                        strValue = Me.m_objIDmxzZzry.iRenyuanList.Split(Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate.ToCharArray())
                        Dim objDataRow As System.Data.DataRow
                        Dim intCount As Integer
                        Dim i As Integer
                        intCount = strValue.Length
                        For i = 0 To intCount - 1 Step 1
                            With Me.m_objDataSet_SELRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT)
                                objDataRow = .NewRow()
                                objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_MC) = strValue(i)
                                .Rows.Add(objDataRow)
                            End With
                        Next
                    End If

                    '缓存信息
                    Me.m_strSessionId_SELRY = strGuid
                    Session.Add(Me.m_strSessionId_SELRY, Me.m_objDataSet_SELRY)
                    Me.htxtSessionIdSELRY.Value = Me.m_strSessionId_SELRY
                Else
                    '直接引用数据
                    Me.m_objDataSet_SELRY = CType(Session.Item(Me.m_strSessionId_SELRY), Xydc.Platform.Common.Data.CustomerData)
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            getModuleData_SELRY = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据屏幕搜索条件搜索grdBMRY数据
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function searchModuleData_BMRY(ByRef strErrMsg As String) As Boolean

            searchModuleData_BMRY = False

            Try
                '获取搜索字符串
                Dim strQuery As String
                If Me.getQueryString_BMRY(strErrMsg, strQuery) = False Then
                    GoTo errProc
                End If

                '合并限制条件
                Dim strRYDM As String = "a." + Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYDM
                If Me.m_strSendRestrictWhere <> "" Then
                    If strQuery <> "" Then
                        strQuery = strQuery + vbCr + " and " + vbCr + strRYDM + " in (" + vbCr + Me.m_strSendRestrictWhere + vbCr + ")" + vbCr
                    Else
                        strQuery = strRYDM + " in (" + vbCr + Me.m_strSendRestrictWhere + vbCr + ")" + vbCr
                    End If
                End If
                If Me.m_strRenyuanRestrictWhere <> "" Then
                    If strQuery <> "" Then
                        strQuery = strQuery + vbCr + " and " + vbCr + strRYDM + " in (" + vbCr + Me.m_strRenyuanRestrictWhere + vbCr + ")" + vbCr
                    Else
                        strQuery = strRYDM + " in (" + vbCr + Me.m_strRenyuanRestrictWhere + vbCr + ")" + vbCr
                    End If
                End If

                '搜索数据
                If Me.getModuleData_BMRY(strErrMsg, strQuery) = False Then
                    GoTo errProc
                End If

                '记录搜索字符串
                Me.m_strQuery_BMRY = strQuery
                Me.htxtBMRYQuery.Value = Me.m_strQuery_BMRY

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            searchModuleData_BMRY = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据屏幕搜索条件搜索grdFWLIST数据
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function searchModuleData_FWLIST(ByRef strErrMsg As String) As Boolean

            searchModuleData_FWLIST = False

            Try
                '获取搜索字符串
                Dim strQuery As String
                If Me.getQueryString_FWLIST(strErrMsg, strQuery) = False Then
                    GoTo errProc
                End If

                '搜索数据
                If Me.getModuleData_FWLIST(strErrMsg, strQuery) = False Then
                    GoTo errProc
                End If

                '记录搜索字符串
                Me.m_strQuery_FWLIST = strQuery
                Me.htxtFWLISTQuery.Value = Me.m_strQuery_FWLIST

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            searchModuleData_FWLIST = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据屏幕搜索条件搜索grdJCLXR数据
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function searchModuleData_JCLXR(ByRef strErrMsg As String) As Boolean

            searchModuleData_JCLXR = False

            Try
                '获取搜索字符串
                Dim strQuery As String
                If Me.getQueryString_JCLXR(strErrMsg, strQuery) = False Then
                    GoTo errProc
                End If

                '合并限制条件
                Dim strRYDM As String = "a." + Xydc.Platform.Common.Data.JingchanglianxirenData.FIELD_GW_B_JINGCHANGLIANXIREN_LXRDM
                If Me.m_strSendRestrictWhere <> "" Then
                    If strQuery <> "" Then
                        strQuery = strQuery + vbCr + " and " + vbCr + strRYDM + " in (" + vbCr + Me.m_strSendRestrictWhere + vbCr + ")" + vbCr
                    Else
                        strQuery = strRYDM + " in (" + vbCr + Me.m_strSendRestrictWhere + vbCr + ")" + vbCr
                    End If
                End If
                If Me.m_strRenyuanRestrictWhere <> "" Then
                    If strQuery <> "" Then
                        strQuery = strQuery + vbCr + " and " + vbCr + strRYDM + " in (" + vbCr + Me.m_strRenyuanRestrictWhere + vbCr + ")" + vbCr
                    Else
                        strQuery = strRYDM + " in (" + vbCr + Me.m_strRenyuanRestrictWhere + vbCr + ")" + vbCr
                    End If
                End If

                '搜索数据
                If Me.getModuleData_JCLXR(strErrMsg, strQuery) = False Then
                    GoTo errProc
                End If

                '记录搜索字符串
                Me.m_strQuery_JCLXR = strQuery
                Me.htxtJCLXRQuery.Value = Me.m_strQuery_JCLXR

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            searchModuleData_JCLXR = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 根据屏幕搜索条件搜索grdSELRY数据(RowFilter)
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function searchModuleData_SELRY(ByRef strErrMsg As String) As Boolean

            searchModuleData_SELRY = False

            Try
                '获取搜索字符串
                Dim strQuery As String
                If Me.getQueryString_SELRY(strErrMsg, strQuery) = False Then
                    GoTo errProc
                End If

                '搜索数据
                If Me.getModuleData_SELRY(strErrMsg) = False Then
                    GoTo errProc
                End If

                '设置新的搜索条件
                Me.m_objDataSet_SELRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT).DefaultView.RowFilter = strQuery

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            searchModuleData_SELRY = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示tvwBMLIST的数据
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showTreeViewInfo_BMXX( _
            ByRef strErrMsg As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objTreeviewProcess As New Xydc.Platform.web.TreeviewProcess
            Dim objDateZZJG As Xydc.Platform.Common.Data.CustomerData
            Dim objsystemCustomer As New Xydc.Platform.BusinessFacade.systemCustomer

            showTreeViewInfo_BMXX = False

            'TreeView显示处理
            Try
                '初始化tvwBMLIST
                If objTreeviewProcess.doDisplayTreeViewAll(strErrMsg, Me.tvwBMLIST, _
                    Me.m_objDataSet_BMXX.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU), _
                    Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_ZZDM, _
                    Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_ZZMC, _
                    True, True, Xydc.Platform.Common.Data.CustomerData.intZZDM_FJCDSM) = False Then
                    GoTo errProc
                End If

                '定位到操作人员所在单位的直接上级单位
                '秘书处大部分的工作补登领导的意见，因此秘书选择人员窗直接定位到办领导
                Dim strBMDM As String

                Dim strJBDM As String = ""
                Dim strBMMC As String = ""
                Dim intJBDM As Integer

                With MyBase.Customer.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN)
                    If .Rows.Count <= 0 Then
                        strBMDM = ""
                    Else
                        strBMDM = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_ZZDM), "")

                        strBMMC = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_ZZMC), "")
                    End If
                End With


                If strBMDM.Length > 0 Then
                    If strBMMC = "秘书处" Then
                        strBMDM = ""
                        If objsystemCustomer.getZzdmByZzmc(strErrMsg, MyBase.UserId, MyBase.UserPassword, "办领导", strBMDM) = False Then
                            GoTo errProc
                        End If
                        'strBMDM = "1010"
                        strBMDM = strBMDM.Trim()

                    End If
                End If

                Dim objTreeNode As Microsoft.Web.UI.WebControls.TreeNode
                Dim intLevel As Integer
                With New Xydc.Platform.Common.Utilities.PulicParameters
                    intLevel = .getCodeLevel(Xydc.Platform.Common.Data.CustomerData.intZZDM_FJCDSM, strBMDM.Length())
                End With
                If intLevel > 1 Then

                    '重新检索数据
                    If objsystemCustomer.getBumenData(strErrMsg, MyBase.UserId, MyBase.UserPassword, strBMDM, True, objDateZZJG) = False Then
                        GoTo errProc
                    End If
                    With objDateZZJG.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU)
                        If .Rows.Count < 1 Then
                            Exit Try
                        Else
                            strJBDM = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_JBDM), "")
                        End If
                    End With

                    '
                    intJBDM = CType(strJBDM, Integer)
                    If intJBDM >= 30 Then
                        strBMDM = strBMDM.Substring(0, Xydc.Platform.Common.Data.CustomerData.intZZDM_FJCDSM(intLevel - 1 - 1))
                    End If


                    objTreeNode = objTreeviewProcess.getTreeNodeByValue(Me.tvwBMLIST, strBMDM)
                    If Not (objTreeNode Is Nothing) Then
                        Me.tvwBMLIST.SelectedNodeIndex = objTreeNode.GetNodeIndex()
                    End If
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.TreeviewProcess.SafeRelease(objTreeviewProcess)

            showTreeViewInfo_BMXX = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.TreeviewProcess.SafeRelease(objTreeviewProcess)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示grdBMRY的数据
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showDataGridInfo_BMRY( _
            ByRef strErrMsg As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess

            showDataGridInfo_BMRY = False

            '获取系统保存的网格排序数据
            Dim intSortColumnIndex As Integer
            intSortColumnIndex = objPulicParameters.getObjectValue(Me.htxtBMRYSortColumnIndex.Value, -1)
            Dim objSortType As Xydc.Platform.Common.Utilities.PulicParameters.enumSortType
            Try
                objSortType = CType(Me.htxtBMRYSortType.Value, Xydc.Platform.Common.Utilities.PulicParameters.enumSortType)
            Catch ex As Exception
                objSortType = Xydc.Platform.Common.Utilities.PulicParameters.enumSortType.None
            End Try

            '网格显示处理
            Try
                '在获取数据时已经恢复了RowFilter、Sort的现场
                '设置数据源
                If Me.m_objDataSet_BMRY Is Nothing Then
                    Me.grdBMRY.DataSource = Nothing
                Else
                    With Me.m_objDataSet_BMRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN)
                        Me.grdBMRY.DataSource = .DefaultView
                    End With
                End If

                '调整网格参数
                With Me.m_objDataSet_BMRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN)
                    If objDataGridProcess.onBeforeDataGridBind(strErrMsg, Me.grdBMRY, .DefaultView.Count) = False Then
                        GoTo errProc
                    End If
                End With

                '恢复列标题中的排序信息
                If intSortColumnIndex >= 0 Then
                    objDataGridProcess.doClearSortCharInDataGridHead(Me.grdBMRY)
                    With Me.grdBMRY.Columns(intSortColumnIndex)
                        .HeaderText = objDataGridProcess.getColumnSortHeadString(.HeaderText, objSortType)
                    End With
                End If

                '绑定数据
                Me.grdBMRY.DataBind()

                '----------------------------------------------------------------
                '因为这些信息是非绑定的，所以下面的操作必须等绑定完成后执行！！！
                '一旦在后续处理中执行了DataBind，则信息会丢失！！！
                '----------------------------------------------------------------
                '恢复网格中的CheckBox状态
                If objDataGridProcess.doRestoreDataGridCheckBoxStatus(strErrMsg, Me.grdBMRY, Request, 0, Me.m_cstrCheckBoxIdInDataGrid_BMRY) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)

            showDataGridInfo_BMRY = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示grdFWLIST的数据
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showDataGridInfo_FWLIST( _
            ByRef strErrMsg As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess

            showDataGridInfo_FWLIST = False

            '获取系统保存的网格排序数据
            Dim intSortColumnIndex As Integer
            intSortColumnIndex = objPulicParameters.getObjectValue(Me.htxtFWLISTSortColumnIndex.Value, -1)
            Dim objSortType As Xydc.Platform.Common.Utilities.PulicParameters.enumSortType
            Try
                objSortType = CType(Me.htxtFWLISTSortType.Value, Xydc.Platform.Common.Utilities.PulicParameters.enumSortType)
            Catch ex As Exception
                objSortType = Xydc.Platform.Common.Utilities.PulicParameters.enumSortType.None
            End Try

            '网格显示处理
            Try
                '在获取数据时已经恢复了RowFilter、Sort的现场
                '设置数据源
                If Me.m_objDataSet_FWLIST Is Nothing Then
                    Me.grdFWLIST.DataSource = Nothing
                Else
                    With Me.m_objDataSet_FWLIST.Tables(Xydc.Platform.Common.Data.FenfafanweiData.TABLE_GW_B_FENFAFANWEI)
                        Me.grdFWLIST.DataSource = .DefaultView
                    End With
                End If

                '调整网格参数
                With Me.m_objDataSet_FWLIST.Tables(Xydc.Platform.Common.Data.FenfafanweiData.TABLE_GW_B_FENFAFANWEI)
                    If objDataGridProcess.onBeforeDataGridBind(strErrMsg, Me.grdFWLIST, .DefaultView.Count) = False Then
                        GoTo errProc
                    End If
                End With

                '恢复列标题中的排序信息
                If intSortColumnIndex >= 0 Then
                    objDataGridProcess.doClearSortCharInDataGridHead(Me.grdFWLIST)
                    With Me.grdFWLIST.Columns(intSortColumnIndex)
                        .HeaderText = objDataGridProcess.getColumnSortHeadString(.HeaderText, objSortType)
                    End With
                End If

                '绑定数据
                Me.grdFWLIST.DataBind()

                '----------------------------------------------------------------
                '因为这些信息是非绑定的，所以下面的操作必须等绑定完成后执行！！！
                '一旦在后续处理中执行了DataBind，则信息会丢失！！！
                '----------------------------------------------------------------
                '恢复网格中的CheckBox状态
                If objDataGridProcess.doRestoreDataGridCheckBoxStatus(strErrMsg, Me.grdFWLIST, Request, 0, Me.m_cstrCheckBoxIdInDataGrid_FWLIST) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)

            showDataGridInfo_FWLIST = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示grdJCLXR的数据
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showDataGridInfo_JCLXR( _
            ByRef strErrMsg As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess

            showDataGridInfo_JCLXR = False

            '获取系统保存的网格排序数据
            Dim intSortColumnIndex As Integer
            intSortColumnIndex = objPulicParameters.getObjectValue(Me.htxtJCLXRSortColumnIndex.Value, -1)
            Dim objSortType As Xydc.Platform.Common.Utilities.PulicParameters.enumSortType
            Try
                objSortType = CType(Me.htxtJCLXRSortType.Value, Xydc.Platform.Common.Utilities.PulicParameters.enumSortType)
            Catch ex As Exception
                objSortType = Xydc.Platform.Common.Utilities.PulicParameters.enumSortType.None
            End Try

            '网格显示处理
            Try
                '在获取数据时已经恢复了RowFilter、Sort的现场
                '设置数据源
                If Me.m_objDataSet_JCLXR Is Nothing Then
                    Me.grdJCLXR.DataSource = Nothing
                Else
                    With Me.m_objDataSet_JCLXR.Tables(Xydc.Platform.Common.Data.JingchanglianxirenData.TABLE_GW_B_JINGCHANGLIANXIREN)
                        Me.grdJCLXR.DataSource = .DefaultView
                    End With
                End If

                '调整网格参数
                With Me.m_objDataSet_JCLXR.Tables(Xydc.Platform.Common.Data.JingchanglianxirenData.TABLE_GW_B_JINGCHANGLIANXIREN)
                    If objDataGridProcess.onBeforeDataGridBind(strErrMsg, Me.grdJCLXR, .DefaultView.Count) = False Then
                        GoTo errProc
                    End If
                End With

                '恢复列标题中的排序信息
                If intSortColumnIndex >= 0 Then
                    objDataGridProcess.doClearSortCharInDataGridHead(Me.grdJCLXR)
                    With Me.grdJCLXR.Columns(intSortColumnIndex)
                        .HeaderText = objDataGridProcess.getColumnSortHeadString(.HeaderText, objSortType)
                    End With
                End If

                '绑定数据
                Me.grdJCLXR.DataBind()

                '----------------------------------------------------------------
                '因为这些信息是非绑定的，所以下面的操作必须等绑定完成后执行！！！
                '一旦在后续处理中执行了DataBind，则信息会丢失！！！
                '----------------------------------------------------------------
                '恢复网格中的CheckBox状态
                If objDataGridProcess.doRestoreDataGridCheckBoxStatus(strErrMsg, Me.grdJCLXR, Request, 0, Me.m_cstrCheckBoxIdInDataGrid_JCLXR) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)

            showDataGridInfo_JCLXR = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示grdSELRY的数据
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showDataGridInfo_SELRY( _
            ByRef strErrMsg As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess

            showDataGridInfo_SELRY = False

            '获取系统保存的网格排序数据
            Dim intSortColumnIndex As Integer
            intSortColumnIndex = objPulicParameters.getObjectValue(Me.htxtSELRYSortColumnIndex.Value, -1)
            Dim objSortType As Xydc.Platform.Common.Utilities.PulicParameters.enumSortType
            Try
                objSortType = CType(Me.htxtSELRYSortType.Value, Xydc.Platform.Common.Utilities.PulicParameters.enumSortType)
            Catch ex As Exception
                objSortType = Xydc.Platform.Common.Utilities.PulicParameters.enumSortType.None
            End Try

            '网格显示处理
            Try
                '在获取数据时已经恢复了RowFilter、Sort的现场
                '设置数据源
                If Me.m_objDataSet_SELRY Is Nothing Then
                    Me.grdSELRY.DataSource = Nothing
                Else
                    With Me.m_objDataSet_SELRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT)
                        Me.grdSELRY.DataSource = .DefaultView
                    End With
                End If

                '调整网格参数
                With Me.m_objDataSet_SELRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT)
                    If objDataGridProcess.onBeforeDataGridBind(strErrMsg, Me.grdSELRY, .DefaultView.Count) = False Then
                        GoTo errProc
                    End If
                End With

                '恢复列标题中的排序信息
                If intSortColumnIndex >= 0 Then
                    objDataGridProcess.doClearSortCharInDataGridHead(Me.grdSELRY)
                    With Me.grdSELRY.Columns(intSortColumnIndex)
                        .HeaderText = objDataGridProcess.getColumnSortHeadString(.HeaderText, objSortType)
                    End With
                End If

                '绑定数据
                Me.grdSELRY.DataBind()

                '----------------------------------------------------------------
                '因为这些信息是非绑定的，所以下面的操作必须等绑定完成后执行！！！
                '一旦在后续处理中执行了DataBind，则信息会丢失！！！
                '----------------------------------------------------------------
                '恢复网格中的CheckBox状态
                If objDataGridProcess.doRestoreDataGridCheckBoxStatus(strErrMsg, Me.grdSELRY, Request, 0, Me.m_cstrCheckBoxIdInDataGrid_SELRY) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)

            showDataGridInfo_SELRY = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示grdBMRY及相关信息
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showModuleData_BMRY( _
            ByRef strErrMsg As String) As Boolean

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess

            showModuleData_BMRY = False

            Try
                '显示网格信息
                If Me.showDataGridInfo_BMRY(strErrMsg) = False Then
                    GoTo errProc
                End If

                '显示与网格紧密相关的操作或信息提示
                With Me.m_objDataSet_BMRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN).DefaultView
                    '显示网格位置信息
                    Me.lblBMRYGridLocInfo.Text = objDataGridProcess.getDataGridLocation(Me.grdBMRY, .Count)
                    '显示页面浏览功能
                    Me.lnkCZBMRYMoveFirst.Enabled = objDataGridProcess.canDoMoveFirstPage(Me.grdBMRY, .Count)
                    Me.lnkCZBMRYMoveLast.Enabled = objDataGridProcess.canDoMoveLastPage(Me.grdBMRY, .Count)
                    Me.lnkCZBMRYMovePrev.Enabled = objDataGridProcess.canDoMovePreviousPage(Me.grdBMRY, .Count)
                    Me.lnkCZBMRYMoveNext.Enabled = objDataGridProcess.canDoMoveNextPage(Me.grdBMRY, .Count)
                    '显示相关操作
                    Dim blnEnabled As Boolean
                    If .Count < 1 Then
                        blnEnabled = False
                    Else
                        blnEnabled = True
                    End If
                    Me.lnkCZBMRYDeSelectAll.Enabled = blnEnabled
                    Me.lnkCZBMRYSelectAll.Enabled = blnEnabled
                    Me.lnkCZBMRYGotoPage.Enabled = blnEnabled
                    Me.lnkCZBMRYSetPageSize.Enabled = blnEnabled
                    Me.btnBMRYAdd.Enabled = blnEnabled
                    Me.btnBMRYAddLxr.Enabled = blnEnabled
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)

            showModuleData_BMRY = True
            Exit Function

errProc:
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示grdFWLIST及相关信息
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showModuleData_FWLIST( _
            ByRef strErrMsg As String) As Boolean

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess

            showModuleData_FWLIST = False

            Try
                '显示网格信息
                If Me.showDataGridInfo_FWLIST(strErrMsg) = False Then
                    GoTo errProc
                End If

                '显示与网格紧密相关的操作或信息提示
                With Me.m_objDataSet_FWLIST.Tables(Xydc.Platform.Common.Data.FenfafanweiData.TABLE_GW_B_FENFAFANWEI).DefaultView
                    '显示网格位置信息
                    Me.lblFWLISTGridLocInfo.Text = objDataGridProcess.getDataGridLocation(Me.grdFWLIST, .Count)
                    '显示页面浏览功能

                    '显示相关操作
                    Dim blnEnabled As Boolean
                    If .Count < 1 Then
                        blnEnabled = False
                    Else
                        blnEnabled = True
                    End If
                    Me.lnkCZFWLISTDeSelectAll.Enabled = blnEnabled
                    Me.lnkCZFWLISTSelectAll.Enabled = blnEnabled
                    Me.btnFWLISTAdd.Enabled = blnEnabled
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)

            showModuleData_FWLIST = True
            Exit Function

errProc:
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示grdJCLXR及相关信息
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showModuleData_JCLXR( _
            ByRef strErrMsg As String) As Boolean

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess

            showModuleData_JCLXR = False

            Try
                '显示网格信息
                If Me.showDataGridInfo_JCLXR(strErrMsg) = False Then
                    GoTo errProc
                End If

                '显示与网格紧密相关的操作或信息提示
                With Me.m_objDataSet_JCLXR.Tables(Xydc.Platform.Common.Data.JingchanglianxirenData.TABLE_GW_B_JINGCHANGLIANXIREN).DefaultView
                    '显示网格位置信息
                    Me.lblJCLXRGridLocInfo.Text = objDataGridProcess.getDataGridLocation(Me.grdJCLXR, .Count)
                    '显示页面浏览功能

                    '显示相关操作
                    Dim blnEnabled As Boolean
                    If .Count < 1 Then
                        blnEnabled = False
                    Else
                        blnEnabled = True
                    End If
                    Me.lnkCZJCLXRDeSelectAll.Enabled = blnEnabled
                    Me.lnkCZJCLXRSelectAll.Enabled = blnEnabled
                    Me.btnJCLXRAdd.Enabled = blnEnabled
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)

            showModuleData_JCLXR = True
            Exit Function

errProc:
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 显示grdSELRY及相关信息
        '     strErrMsg      ：返回错误信息
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Private Function showModuleData_SELRY( _
            ByRef strErrMsg As String) As Boolean

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess

            showModuleData_SELRY = False

            Try
                '显示网格信息
                If Me.showDataGridInfo_SELRY(strErrMsg) = False Then
                    GoTo errProc
                End If

                '显示与网格紧密相关的操作或信息提示
                With Me.m_objDataSet_SELRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT).DefaultView
                    '显示网格位置信息
                    Me.lblSELRYGridLocInfo.Text = objDataGridProcess.getDataGridLocation(Me.grdSELRY, .Count)
                    '显示页面浏览功能
                    Me.lnkCZSELRYMoveFirst.Enabled = objDataGridProcess.canDoMoveFirstPage(Me.grdSELRY, .Count)
                    Me.lnkCZSELRYMoveLast.Enabled = objDataGridProcess.canDoMoveLastPage(Me.grdSELRY, .Count)
                    Me.lnkCZSELRYMovePrev.Enabled = objDataGridProcess.canDoMovePreviousPage(Me.grdSELRY, .Count)
                    Me.lnkCZSELRYMoveNext.Enabled = objDataGridProcess.canDoMoveNextPage(Me.grdSELRY, .Count)
                    '显示相关操作
                    Dim blnEnabled As Boolean
                    If .Count < 1 Then
                        blnEnabled = False
                    Else
                        blnEnabled = True
                    End If
                    Me.lnkCZSELRYDeSelectAll.Enabled = blnEnabled
                    Me.lnkCZSELRYSelectAll.Enabled = blnEnabled
                    Me.lnkCZSELRYGotoPage.Enabled = blnEnabled
                    Me.lnkCZSELRYSetPageSize.Enabled = blnEnabled
                    Me.btnSELRYDelete.Enabled = blnEnabled
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)

            showModuleData_SELRY = True
            Exit Function

errProc:
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 初始化控件
        '----------------------------------------------------------------
        Private Function initializeControls(ByRef strErrMsg As String) As Boolean

            initializeControls = False

            '仅在第一次调用页面时执行
            If Me.IsPostBack = False Then
                Try
                    '根据接口参数设置
                    If Me.m_objIDmxzZzry.iSelectMode = True Then
                        Me.lblTitle.Text = "[选择人员/单位/范围、"
                    Else
                        If Me.m_objIDmxzZzry.iSelectFFFW = True And Me.m_objIDmxzZzry.iSelectBMMC = True Then
                            Me.lblTitle.Text = "[选择人员/单位/范围、"
                        ElseIf Me.m_objIDmxzZzry.iSelectFFFW = True Then
                            Me.lblTitle.Text = "[选择人员/范围、"
                        ElseIf Me.m_objIDmxzZzry.iSelectBMMC = True Then
                            Me.lblTitle.Text = "[选择人员/单位、"
                        Else
                            Me.lblTitle.Text = "[选择人员、"
                        End If
                    End If
                    If Me.m_objIDmxzZzry.iMultiSelect = True Then
                        Me.lblTitle.Text += "多选]"
                    Else
                        Me.lblTitle.Text += "单选]"
                    End If
                    '允许手工输入？
                    Me.txtNewRYMC.Enabled = Me.m_objIDmxzZzry.iAllowInput
                    Me.btnAddNew.Enabled = Me.txtNewRYMC.Enabled
                    Me.rblXZLX.Enabled = Me.txtNewRYMC.Enabled
                Catch ex As Exception
                End Try

                '显示Pannel
                Me.panelMain.Visible = True
                Me.panelError.Visible = Not Me.panelMain.Visible

                '执行键转译(不论是否是“回发”)
                Try
                    With New Xydc.Platform.web.ControlProcess
                        .doTranslateKey(Me.txtBMRYPageIndex)
                        .doTranslateKey(Me.txtBMRYPageSize)
                        .doTranslateKey(Me.txtBMRYSearch_RYMC)
                        .doTranslateKey(Me.txtBMRYSearch_RYXHMin)
                        .doTranslateKey(Me.txtBMRYSearch_RYXHMax)
                        .doTranslateKey(Me.txtBMRYSearch_RYJBMC)
                        .doTranslateKey(Me.txtBMRYSearch_RYDRZW)
                        .doTranslateKey(Me.txtFWLISTSearch_FWMC)
                        .doTranslateKey(Me.txtJCLXRSearch_RYMC)
                        .doTranslateKey(Me.txtSELRYPageIndex)
                        .doTranslateKey(Me.txtSELRYPageSize)
                        .doTranslateKey(Me.txtSELRYSearch_XZMC)
                        .doTranslateKey(Me.txtNewRYMC)
                    End With
                Catch ex As Exception
                    strErrMsg = ex.Message
                    GoTo errProc
                End Try
            End If

            If Me.IsPostBack = False Then
                '获取数据
                If Me.getModuleData_BMXX(strErrMsg) = False Then
                    GoTo errProc
                End If
                '显示数据
                If Me.showTreeViewInfo_BMXX(strErrMsg) = False Then
                    GoTo errProc
                End If

                '获取数据
                If Me.getModuleData_BMRY(strErrMsg, Me.m_strQuery_BMRY) = False Then
                    GoTo errProc
                End If
                '显示数据
                If Me.showModuleData_BMRY(strErrMsg) = False Then
                    GoTo errProc
                End If

                '获取数据
                If Me.getModuleData_FWLIST(strErrMsg, Me.m_strQuery_FWLIST) = False Then
                    GoTo errProc
                End If
                '显示数据
                If Me.showModuleData_FWLIST(strErrMsg) = False Then
                    GoTo errProc
                End If

                '获取数据
                If Me.getModuleData_JCLXR(strErrMsg, Me.m_strQuery_JCLXR) = False Then
                    GoTo errProc
                End If
                '显示数据
                If Me.showModuleData_JCLXR(strErrMsg) = False Then
                    GoTo errProc
                End If

                '获取数据
                If Me.getModuleData_SELRY(strErrMsg) = False Then
                    GoTo errProc
                End If
                '显示数据
                If Me.showModuleData_SELRY(strErrMsg) = False Then
                    GoTo errProc
                End If
            End If

            initializeControls = True
            Exit Function

errProc:
            Exit Function

        End Function

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String
            Dim strUrl As String

            '预处理
            If MyBase.doPagePreprocess(True, False) = True Then
                Exit Sub
            End If
            '获取接口参数
            If Me.getInterfaceParameters(strErrMsg) = False Then
                GoTo errProc
            End If

            '控件初始化
            If Me.initializeControls(strErrMsg) = False Then
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
        '网格事件处理器
        '----------------------------------------------------------------
        '实现对grdBMRY网格行、列的固定
        Sub grdBMRY_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles grdBMRY.ItemDataBound

            Dim intCells As Integer
            Dim i As Integer

            Try
                If e.Item.ItemIndex < 0 Then
                    '标题行,输出标题锁定一般属性
                    intCells = e.Item.Cells.Count
                    For i = 0 To intCells - 1 Step 1
                        e.Item.Cells(i).Attributes.CssStyle.Add("top", "expression(" + Me.m_cstrDataGridInDIV_BMRY + ".scrollTop)")
                    Next
                End If
                If Me.m_intFixedColumns_BMRY > 0 Then
                    '锁定列
                    For i = 0 To Me.m_intFixedColumns_BMRY - 1 Step 1
                        e.Item.Cells(i).CssClass = Me.grdBMRY.ID + "Locked"
                    Next
                End If
            Catch ex As Exception
            End Try

        End Sub

        '实现对grdFWLIST网格行、列的固定
        Sub grdFWLIST_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles grdFWLIST.ItemDataBound

            Dim intCells As Integer
            Dim i As Integer

            Try
                If e.Item.ItemIndex < 0 Then
                    '标题行,输出标题锁定一般属性
                    intCells = e.Item.Cells.Count
                    For i = 0 To intCells - 1 Step 1
                        e.Item.Cells(i).Attributes.CssStyle.Add("top", "expression(" + Me.m_cstrDataGridInDIV_FWLIST + ".scrollTop)")
                    Next
                End If
                If Me.m_intFixedColumns_FWLIST > 0 Then
                    '锁定列
                    For i = 0 To Me.m_intFixedColumns_FWLIST - 1 Step 1
                        e.Item.Cells(i).CssClass = Me.grdFWLIST.ID + "Locked"
                    Next
                End If
            Catch ex As Exception
            End Try

        End Sub

        '实现对grdJCLXR网格行、列的固定
        Sub grdJCLXR_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles grdJCLXR.ItemDataBound

            Dim intCells As Integer
            Dim i As Integer

            Try
                If e.Item.ItemIndex < 0 Then
                    '标题行,输出标题锁定一般属性
                    intCells = e.Item.Cells.Count
                    For i = 0 To intCells - 1 Step 1
                        e.Item.Cells(i).Attributes.CssStyle.Add("top", "expression(" + Me.m_cstrDataGridInDIV_JCLXR + ".scrollTop)")
                    Next
                End If
                If Me.m_intFixedColumns_JCLXR > 0 Then
                    '锁定列
                    For i = 0 To Me.m_intFixedColumns_JCLXR - 1 Step 1
                        e.Item.Cells(i).CssClass = Me.grdJCLXR.ID + "Locked"
                    Next
                End If
            Catch ex As Exception
            End Try

        End Sub

        '实现对grdSELRY网格行、列的固定
        Sub grdSELRY_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles grdSELRY.ItemDataBound

            Dim intCells As Integer
            Dim i As Integer

            Try
                If e.Item.ItemIndex < 0 Then
                    '标题行,输出标题锁定一般属性
                    intCells = e.Item.Cells.Count
                    For i = 0 To intCells - 1 Step 1
                        e.Item.Cells(i).Attributes.CssStyle.Add("top", "expression(" + Me.m_cstrDataGridInDIV_SELRY + ".scrollTop)")
                    Next
                End If
                If Me.m_intFixedColumns_SELRY > 0 Then
                    '锁定列
                    For i = 0 To Me.m_intFixedColumns_SELRY - 1 Step 1
                        e.Item.Cells(i).CssClass = Me.grdSELRY.ID + "Locked"
                    Next
                End If
            Catch ex As Exception
            End Try

        End Sub

        Private Sub tvwBMLIST_SelectedIndexChange(ByVal sender As Object, ByVal e As Microsoft.Web.UI.WebControls.TreeViewSelectEventArgs) Handles tvwBMLIST.SelectedIndexChange

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '设置新索引
                Me.tvwBMLIST.SelectedNodeIndex = e.NewNode

                '获取数据
                If Me.getModuleData_BMRY(strErrMsg, Me.m_strQuery_BMRY) = False Then
                    GoTo errProc
                End If
                '显示数据
                If Me.showModuleData_BMRY(strErrMsg) = False Then
                    GoTo errProc
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

        Private Sub tvwBMLIST_Check(ByVal sender As Object, ByVal e As Microsoft.Web.UI.WebControls.TreeViewClickEventArgs) Handles tvwBMLIST.Check

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objsystemCustomer As New Xydc.Platform.BusinessFacade.systemCustomer
            Dim objsystemCommon As New Xydc.Platform.BusinessFacade.systemCommon
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim objTreeviewProcess As New Xydc.Platform.web.TreeviewProcess
            Dim objRenyuanData As Xydc.Platform.Common.Data.CustomerData
            Dim objBumenData As Xydc.Platform.Common.Data.CustomerData
            Dim strErrMsg As String

            Try
                Dim objDataRow As System.Data.DataRow

                '获取选定节点
                Dim objTreeNode As Microsoft.Web.UI.WebControls.TreeNode
                objTreeNode = Me.tvwBMLIST.GetNodeFromIndex(e.Node)
                If objTreeNode Is Nothing Then
                    strErrMsg = "错误：未给部门打勾！"
                    GoTo errProc
                End If
                If objTreeNode.Checked = False Then
                    GoTo normExit
                End If

                '从节点ID中获取部门代码
                Dim strZZDM As String
                strZZDM = objTreeviewProcess.getCodeValueFromNodeId(objTreeNode.ID)
                If strZZDM = "" Then
                    GoTo normExit
                End If

                '获取部门信息
                If objsystemCustomer.getBumenData(strErrMsg, MyBase.UserId, MyBase.UserPassword, strZZDM, objBumenData) = False Then
                    GoTo errProc
                End If
                With objBumenData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU_FULLJOIN)
                    '没有数据
                    If .Rows.Count < 1 Then
                        GoTo normExit
                    End If
                End With

                '获取SELRY的数据
                If Me.getModuleData_SELRY(strErrMsg) = False Then
                    GoTo errProc
                End If

                '根据接口参数处理
                Dim blnFound As Boolean
                Dim strMC As String
                If Me.m_objIDmxzZzry.iSelectMode = True Or Me.m_objIDmxzZzry.iSelectBMMC = True Then '可选择部门
                    '检查是否存在
                    With objBumenData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU_FULLJOIN)
                        strMC = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_ZZMC), "")
                    End With
                    If objsystemCommon.doFindInDataTable(strErrMsg, _
                        Me.m_objDataSet_SELRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT), _
                        Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_MC, _
                        strMC, blnFound) = False Then
                        GoTo errProc
                    End If
                    If blnFound = True Then '存在
                        GoTo normExit
                    End If

                    '复制到m_objDataSet_SELRY
                    objDataRow = Me.m_objDataSet_SELRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT).NewRow()
                    With objBumenData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU_FULLJOIN)
                        '设置数据
                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_MC) = .Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_ZZMC)
                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_LX) = Xydc.Platform.Common.Data.FenfafanweiData.CYLX_DANWEI
                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_BM) = .Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_ZZMC)
                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_JB) = .Rows(0).Item(Xydc.Platform.Common.Data.XingzhengjibieData.FIELD_GG_B_XINGZHENGJIBIE_JBMC)
                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_MS) = .Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_FULLJOIN_MSMC)
                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_SJHM) = .Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_SJHM)
                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_LXDH) = .Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_LXDH)
                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_FTPDZ) = .Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_FTPDZ)
                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_YXDZ) = .Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_YXDZ)
                    End With

                    '加入表
                    With Me.m_objDataSet_SELRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT)
                        .Rows.Add(objDataRow)
                    End With

                Else '不能选择部门
                    '获取秘书代码
                    Dim strMSDM As String
                    With objBumenData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU_FULLJOIN).Rows(0)
                        strMSDM = objPulicParameters.getObjectValue(.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_MSDM), "")
                    End With
                    If strMSDM = "" Then
                        strErrMsg = "错误：单位没有特定文秘！"
                        GoTo errProc
                    End If

                    '获取秘书信息
                    If objsystemCustomer.getRenyuanData(strErrMsg, MyBase.UserId, MyBase.UserPassword, strMSDM, "0001", objRenyuanData) = False Then
                        GoTo errProc
                    End If
                    With objRenyuanData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN)
                        If .Rows.Count < 1 Then
                            GoTo normExit
                        End If
                    End With

                    '检查是否存在
                    With objRenyuanData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN)
                        strMC = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYMC), "")
                    End With
                    If objsystemCommon.doFindInDataTable(strErrMsg, _
                        Me.m_objDataSet_SELRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT), _
                        Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_MC, _
                        strMC, blnFound) = False Then
                        GoTo errProc
                    End If
                    If blnFound = True Then '存在
                        GoTo normExit
                    End If

                    '复制到m_objDataSet_SELRY
                    objDataRow = Me.m_objDataSet_SELRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT).NewRow()
                    With objRenyuanData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN)
                        '设置数据
                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_MC) = .Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYMC)
                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_LX) = Xydc.Platform.Common.Data.FenfafanweiData.CYLX_GEREN
                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_BM) = .Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_ZZMC)
                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_JB) = .Rows(0).Item(Xydc.Platform.Common.Data.XingzhengjibieData.FIELD_GG_B_XINGZHENGJIBIE_JBMC)
                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_MS) = .Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_FULLJOIN_MSMC)
                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_SJHM) = .Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SJHM)
                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_LXDH) = .Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_LXDH)
                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_FTPDZ) = .Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_FTPDZ)
                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_YXDZ) = .Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_YXDZ)
                    End With
                    '加入表
                    With Me.m_objDataSet_SELRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT)
                        .Rows.Add(objDataRow)
                    End With
                End If

                '重新显示网格
                If Me.showModuleData_SELRY(strErrMsg) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

normExit:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.BusinessFacade.systemCustomer.SafeRelease(objsystemCustomer)
            Xydc.Platform.BusinessFacade.systemCommon.SafeRelease(objsystemCommon)
            Xydc.Platform.Common.Data.CustomerData.SafeRelease(objRenyuanData)
            Xydc.Platform.Common.Data.CustomerData.SafeRelease(objBumenData)
            Xydc.Platform.web.TreeviewProcess.SafeRelease(objTreeviewProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.BusinessFacade.systemCustomer.SafeRelease(objsystemCustomer)
            Xydc.Platform.BusinessFacade.systemCommon.SafeRelease(objsystemCommon)
            Xydc.Platform.Common.Data.CustomerData.SafeRelease(objRenyuanData)
            Xydc.Platform.Common.Data.CustomerData.SafeRelease(objBumenData)
            Xydc.Platform.web.TreeviewProcess.SafeRelease(objTreeviewProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub grdBMRY_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdBMRY.SelectedIndexChanged

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '显示记录位置
                With New Xydc.Platform.web.DataGridProcess
                    Me.lblBMRYGridLocInfo.Text = .getDataGridLocation(Me.grdBMRY, Me.m_intRows_BMRY)
                End With
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

        Private Sub grdFWLIST_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdFWLIST.SelectedIndexChanged

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '显示记录位置
                With New Xydc.Platform.web.DataGridProcess
                    Me.lblFWLISTGridLocInfo.Text = .getDataGridLocation(Me.grdFWLIST, Me.m_intRows_FWLIST)
                End With
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

        Private Sub grdJCLXR_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdJCLXR.SelectedIndexChanged

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '显示记录位置
                With New Xydc.Platform.web.DataGridProcess
                    Me.lblJCLXRGridLocInfo.Text = .getDataGridLocation(Me.grdJCLXR, Me.m_intRows_JCLXR)
                End With
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

        Private Sub grdSELRY_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSELRY.SelectedIndexChanged

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '获取数据
                If Me.getModuleData_SELRY(strErrMsg) = False Then
                    GoTo errProc
                End If
                '显示数据
                If Me.showModuleData_SELRY(strErrMsg) = False Then
                    GoTo errProc
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

        Private Sub grdBMRY_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles grdBMRY.SortCommand

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                Dim objenumSortType As Xydc.Platform.Common.Utilities.PulicParameters.enumSortType
                Dim objDataGridItem As System.Web.UI.WebControls.DataGridItem
                Dim strFinalCommand As String
                Dim strOldCommand As String
                Dim strUniqueId As String
                Dim intColumnIndex As Integer

                '获取数据
                If Me.getModuleData_BMRY(strErrMsg, Me.m_strQuery_BMRY) = False Then
                    GoTo errProc
                End If

                '获取原排序命令
                strOldCommand = Me.m_objDataSet_BMRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN).DefaultView.Sort

                '获取要执行的排序命令
                objDataGridProcess.getColumnSortCommand(strErrMsg, strOldCommand, e.SortExpression, strFinalCommand, objenumSortType)
                If strErrMsg <> "" Then
                    GoTo errProc
                End If

                '执行排序
                Me.m_objDataSet_BMRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN).DefaultView.Sort = strFinalCommand

                '获取排序列的列索引
                objDataGridItem = CType(e.CommandSource, System.Web.UI.WebControls.DataGridItem)
                strUniqueId = Request.Form("__EVENTTARGET")
                intColumnIndex = objDataGridProcess.getColumnIndexByUniqueIdInRow(objDataGridItem, strUniqueId)

                '保存排序信息
                Me.htxtBMRYSortColumnIndex.Value = intColumnIndex.ToString()
                Me.htxtBMRYSortType.Value = CType(objenumSortType, Integer).ToString()
                Me.htxtBMRYSort.Value = strFinalCommand

                '重新显示数据
                If Me.showModuleData_BMRY(strErrMsg) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub grdFWLIST_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles grdFWLIST.SortCommand

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                Dim objenumSortType As Xydc.Platform.Common.Utilities.PulicParameters.enumSortType
                Dim objDataGridItem As System.Web.UI.WebControls.DataGridItem
                Dim strFinalCommand As String
                Dim strOldCommand As String
                Dim strUniqueId As String
                Dim intColumnIndex As Integer

                '获取数据
                If Me.getModuleData_FWLIST(strErrMsg, Me.m_strQuery_FWLIST) = False Then
                    GoTo errProc
                End If

                '获取原排序命令
                strOldCommand = Me.m_objDataSet_FWLIST.Tables(Xydc.Platform.Common.Data.FenfafanweiData.TABLE_GW_B_FENFAFANWEI).DefaultView.Sort

                '获取要执行的排序命令
                objDataGridProcess.getColumnSortCommand(strErrMsg, strOldCommand, e.SortExpression, strFinalCommand, objenumSortType)
                If strErrMsg <> "" Then
                    GoTo errProc
                End If

                '执行排序
                Me.m_objDataSet_FWLIST.Tables(Xydc.Platform.Common.Data.FenfafanweiData.TABLE_GW_B_FENFAFANWEI).DefaultView.Sort = strFinalCommand

                '获取排序列的列索引
                objDataGridItem = CType(e.CommandSource, System.Web.UI.WebControls.DataGridItem)
                strUniqueId = Request.Form("__EVENTTARGET")
                intColumnIndex = objDataGridProcess.getColumnIndexByUniqueIdInRow(objDataGridItem, strUniqueId)

                '保存排序信息
                Me.htxtFWLISTSortColumnIndex.Value = intColumnIndex.ToString()
                Me.htxtFWLISTSortType.Value = CType(objenumSortType, Integer).ToString()
                Me.htxtFWLISTSort.Value = strFinalCommand

                '重新显示数据
                If Me.showModuleData_FWLIST(strErrMsg) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub grdJCLXR_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles grdJCLXR.SortCommand

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                Dim objenumSortType As Xydc.Platform.Common.Utilities.PulicParameters.enumSortType
                Dim objDataGridItem As System.Web.UI.WebControls.DataGridItem
                Dim strFinalCommand As String
                Dim strOldCommand As String
                Dim strUniqueId As String
                Dim intColumnIndex As Integer

                '获取数据
                If Me.getModuleData_JCLXR(strErrMsg, Me.m_strQuery_JCLXR) = False Then
                    GoTo errProc
                End If

                '获取原排序命令
                strOldCommand = Me.m_objDataSet_JCLXR.Tables(Xydc.Platform.Common.Data.JingchanglianxirenData.TABLE_GW_B_JINGCHANGLIANXIREN).DefaultView.Sort

                '获取要执行的排序命令
                objDataGridProcess.getColumnSortCommand(strErrMsg, strOldCommand, e.SortExpression, strFinalCommand, objenumSortType)
                If strErrMsg <> "" Then
                    GoTo errProc
                End If

                '执行排序
                Me.m_objDataSet_JCLXR.Tables(Xydc.Platform.Common.Data.JingchanglianxirenData.TABLE_GW_B_JINGCHANGLIANXIREN).DefaultView.Sort = strFinalCommand

                '获取排序列的列索引
                objDataGridItem = CType(e.CommandSource, System.Web.UI.WebControls.DataGridItem)
                strUniqueId = Request.Form("__EVENTTARGET")
                intColumnIndex = objDataGridProcess.getColumnIndexByUniqueIdInRow(objDataGridItem, strUniqueId)

                '保存排序信息
                Me.htxtJCLXRSortColumnIndex.Value = intColumnIndex.ToString()
                Me.htxtJCLXRSortType.Value = CType(objenumSortType, Integer).ToString()
                Me.htxtJCLXRSort.Value = strFinalCommand

                '重新显示数据
                If Me.showModuleData_JCLXR(strErrMsg) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)

            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub grdSELRY_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles grdSELRY.SortCommand

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                Dim objenumSortType As Xydc.Platform.Common.Utilities.PulicParameters.enumSortType
                Dim objDataGridItem As System.Web.UI.WebControls.DataGridItem
                Dim strFinalCommand As String
                Dim strOldCommand As String
                Dim strUniqueId As String
                Dim intColumnIndex As Integer

                '获取数据
                If Me.getModuleData_SELRY(strErrMsg) = False Then
                    GoTo errProc
                End If

                '获取原排序命令
                strOldCommand = Me.m_objDataSet_SELRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT).DefaultView.Sort

                '获取要执行的排序命令
                objDataGridProcess.getColumnSortCommand(strErrMsg, strOldCommand, e.SortExpression, strFinalCommand, objenumSortType)
                If strErrMsg <> "" Then
                    GoTo errProc
                End If

                '执行排序
                Me.m_objDataSet_SELRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT).DefaultView.Sort = strFinalCommand

                '获取排序列的列索引
                objDataGridItem = CType(e.CommandSource, System.Web.UI.WebControls.DataGridItem)
                strUniqueId = Request.Form("__EVENTTARGET")
                intColumnIndex = objDataGridProcess.getColumnIndexByUniqueIdInRow(objDataGridItem, strUniqueId)

                '保存排序信息
                Me.htxtSELRYSortColumnIndex.Value = intColumnIndex.ToString()
                Me.htxtSELRYSortType.Value = CType(objenumSortType, Integer).ToString()

                '重新显示数据
                If Me.showModuleData_SELRY(strErrMsg) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub doMoveFirst_BMRY(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_BMRY(strErrMsg, Me.m_strQuery_BMRY) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(0, Me.grdBMRY.PageCount)
                Me.grdBMRY.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_BMRY(strErrMsg) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub doMoveLast_BMRY(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_BMRY(strErrMsg, Me.m_strQuery_BMRY) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(Me.grdBMRY.PageCount - 1, Me.grdBMRY.PageCount)
                Me.grdBMRY.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_BMRY(strErrMsg) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub doMoveNext_BMRY(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_BMRY(strErrMsg, Me.m_strQuery_BMRY) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(Me.grdBMRY.CurrentPageIndex + 1, Me.grdBMRY.PageCount)
                Me.grdBMRY.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_BMRY(strErrMsg) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub doMovePrevious_BMRY(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_BMRY(strErrMsg, Me.m_strQuery_BMRY) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(Me.grdBMRY.CurrentPageIndex - 1, Me.grdBMRY.PageCount)
                Me.grdBMRY.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_BMRY(strErrMsg) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub doMoveFirst_SELRY(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_SELRY(strErrMsg) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(0, Me.grdSELRY.PageCount)
                Me.grdSELRY.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_SELRY(strErrMsg) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub doMoveLast_SELRY(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_SELRY(strErrMsg) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(Me.grdSELRY.PageCount - 1, Me.grdSELRY.PageCount)
                Me.grdSELRY.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_SELRY(strErrMsg) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub doMoveNext_SELRY(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_SELRY(strErrMsg) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(Me.grdSELRY.CurrentPageIndex + 1, Me.grdSELRY.PageCount)
                Me.grdSELRY.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_SELRY(strErrMsg) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub doMovePrevious_SELRY(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Dim intPageIndex As Integer
            Try
                '获取数据
                If Me.getModuleData_SELRY(strErrMsg) = False Then
                    GoTo errProc
                End If

                '设置新的页
                intPageIndex = objDataGridProcess.doMoveToPage(Me.grdSELRY.CurrentPageIndex - 1, Me.grdSELRY.PageCount)
                Me.grdSELRY.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_SELRY(strErrMsg) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub doGotoPage_BMRY(ByVal strControlId As String)

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            '获取新的页大小
            Dim intPageIndex As Integer
            intPageIndex = objPulicParameters.getObjectValue(Me.txtBMRYPageIndex.Text, 0)
            If intPageIndex <= 0 Then
                intPageIndex = 0
            Else
                intPageIndex -= 1
            End If

            Try
                '获取数据
                If Me.getModuleData_BMRY(strErrMsg, Me.m_strQuery_BMRY) = False Then
                    GoTo errProc
                End If

                '设置新的页
                Me.grdBMRY.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_BMRY(strErrMsg) = False Then
                    GoTo errProc
                End If

                '信息同步
                Me.txtBMRYPageIndex.Text = (Me.grdBMRY.CurrentPageIndex + 1).ToString()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub doGotoPage_SELRY(ByVal strControlId As String)

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            '获取新的页大小
            Dim intPageIndex As Integer
            intPageIndex = objPulicParameters.getObjectValue(Me.txtSELRYPageIndex.Text, 0)
            If intPageIndex <= 0 Then
                intPageIndex = 0
            Else
                intPageIndex -= 1
            End If

            Try
                '获取数据
                If Me.getModuleData_SELRY(strErrMsg) = False Then
                    GoTo errProc
                End If

                '设置新的页
                Me.grdSELRY.CurrentPageIndex = intPageIndex

                '刷新网格显示
                If Me.showModuleData_SELRY(strErrMsg) = False Then
                    GoTo errProc
                End If

                '信息同步
                Me.txtSELRYPageIndex.Text = (Me.grdSELRY.CurrentPageIndex + 1).ToString()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub doSetPageSize_BMRY(ByVal strControlId As String)

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            '获取新的页大小
            Dim intPageSize As Integer
            intPageSize = objPulicParameters.getObjectValue(Me.txtBMRYPageSize.Text, 100)
            If intPageSize <= 0 Then intPageSize = 100

            Try
                '获取数据
                If Me.getModuleData_BMRY(strErrMsg, Me.m_strQuery_BMRY) = False Then
                    GoTo errProc
                End If

                '设置新的页大小
                Me.grdBMRY.PageSize = intPageSize

                '刷新网格显示
                If Me.showModuleData_BMRY(strErrMsg) = False Then
                    GoTo errProc
                End If

                '信息同步
                Me.txtBMRYPageSize.Text = (Me.grdBMRY.PageSize).ToString()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub doSetPageSize_SELRY(ByVal strControlId As String)

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            '获取新的页大小
            Dim intPageSize As Integer
            intPageSize = objPulicParameters.getObjectValue(Me.txtSELRYPageSize.Text, 100)
            If intPageSize <= 0 Then intPageSize = 100

            Try
                '获取数据
                If Me.getModuleData_SELRY(strErrMsg) = False Then
                    GoTo errProc
                End If

                '设置新的页大小
                Me.grdSELRY.PageSize = intPageSize

                '刷新网格显示
                If Me.showModuleData_SELRY(strErrMsg) = False Then
                    GoTo errProc
                End If

                '信息同步
                Me.txtSELRYPageSize.Text = (Me.grdSELRY.PageSize).ToString()

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub doSelectAll_BMRY(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                If objDataGridProcess.doCheckedDataGridCheckBox(strErrMsg, Me.grdBMRY, 0, Me.m_cstrCheckBoxIdInDataGrid_BMRY, True) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub doSelectAll_SELRY(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                If objDataGridProcess.doCheckedDataGridCheckBox(strErrMsg, Me.grdSELRY, 0, Me.m_cstrCheckBoxIdInDataGrid_SELRY, True) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub doSelectAll_FWLIST(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                If objDataGridProcess.doCheckedDataGridCheckBox(strErrMsg, Me.grdFWLIST, 0, Me.m_cstrCheckBoxIdInDataGrid_FWLIST, True) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub doSelectAll_JCLXR(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                If objDataGridProcess.doCheckedDataGridCheckBox(strErrMsg, Me.grdJCLXR, 0, Me.m_cstrCheckBoxIdInDataGrid_JCLXR, True) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub doDeSelectAll_BMRY(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                If objDataGridProcess.doCheckedDataGridCheckBox(strErrMsg, Me.grdBMRY, 0, Me.m_cstrCheckBoxIdInDataGrid_BMRY, False) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub doDeSelectAll_SELRY(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                If objDataGridProcess.doCheckedDataGridCheckBox(strErrMsg, Me.grdSELRY, 0, Me.m_cstrCheckBoxIdInDataGrid_SELRY, False) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub doDeSelectAll_FWLIST(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                If objDataGridProcess.doCheckedDataGridCheckBox(strErrMsg, Me.grdFWLIST, 0, Me.m_cstrCheckBoxIdInDataGrid_FWLIST, False) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub doDeSelectAll_JCLXR(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                If objDataGridProcess.doCheckedDataGridCheckBox(strErrMsg, Me.grdJCLXR, 0, Me.m_cstrCheckBoxIdInDataGrid_JCLXR, False) = False Then
                    GoTo errProc
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub doSearch_BMRY(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '搜索数据
                If Me.searchModuleData_BMRY(strErrMsg) = False Then
                    GoTo errProc
                End If

                '刷新网格显示
                If Me.showModuleData_BMRY(strErrMsg) = False Then
                    GoTo errProc
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

        Private Sub doSearch_SELRY(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '搜索数据
                If Me.searchModuleData_SELRY(strErrMsg) = False Then
                    GoTo errProc
                End If

                '刷新网格显示
                If Me.showModuleData_SELRY(strErrMsg) = False Then
                    GoTo errProc
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

        Private Sub doSearch_FWLIST(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '搜索数据
                If Me.searchModuleData_FWLIST(strErrMsg) = False Then
                    GoTo errProc
                End If

                '刷新网格显示
                If Me.showModuleData_FWLIST(strErrMsg) = False Then
                    GoTo errProc
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

        Private Sub doSearch_JCLXR(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '搜索数据
                If Me.searchModuleData_JCLXR(strErrMsg) = False Then
                    GoTo errProc
                End If

                '刷新网格显示
                If Me.showModuleData_JCLXR(strErrMsg) = False Then
                    GoTo errProc
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

        Private Sub lnkCZBMRYMoveFirst_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZBMRYMoveFirst.Click
            Me.doMoveFirst_BMRY("lnkCZBMRYMoveFirst")
        End Sub

        Private Sub lnkCZBMRYMoveLast_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZBMRYMoveLast.Click
            Me.doMoveLast_BMRY("lnkCZBMRYMoveLast")
        End Sub

        Private Sub lnkCZBMRYMoveNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZBMRYMoveNext.Click
            Me.doMoveNext_BMRY("lnkCZBMRYMoveNext")
        End Sub

        Private Sub lnkCZBMRYMovePrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZBMRYMovePrev.Click
            Me.doMovePrevious_BMRY("lnkCZBMRYMovePrev")
        End Sub

        Private Sub lnkCZSELRYMoveFirst_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZSELRYMoveFirst.Click
            Me.doMoveFirst_SELRY("lnkCZSELRYMoveFirst")
        End Sub

        Private Sub lnkCZSELRYMoveLast_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZSELRYMoveLast.Click
            Me.doMoveLast_SELRY("lnkCZSELRYMoveLast")
        End Sub

        Private Sub lnkCZSELRYMoveNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZSELRYMoveNext.Click
            Me.doMoveNext_SELRY("lnkCZSELRYMoveNext")
        End Sub

        Private Sub lnkCZSELRYMovePrev_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZSELRYMovePrev.Click
            Me.doMovePrevious_SELRY("lnkCZSELRYMovePrev")
        End Sub

        Private Sub lnkCZBMRYGotoPage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZBMRYGotoPage.Click
            Me.doGotoPage_BMRY("lnkCZBMRYGotoPage")
        End Sub

        Private Sub lnkCZSELRYGotoPage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZSELRYGotoPage.Click
            Me.doGotoPage_SELRY("lnkCZSELRYGotoPage")
        End Sub

        Private Sub lnkCZBMRYSetPageSize_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZBMRYSetPageSize.Click
            Me.doSetPageSize_BMRY("lnkCZBMRYSetPageSize")
        End Sub

        Private Sub lnkCZSELRYSetPageSize_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZSELRYSetPageSize.Click
            Me.doSetPageSize_SELRY("lnkCZSELRYSetPageSize")
        End Sub

        Private Sub lnkCZBMRYSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZBMRYSelectAll.Click
            Me.doSelectAll_BMRY("lnkCZBMRYSelectAll")
        End Sub

        Private Sub lnkCZSELRYSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZSELRYSelectAll.Click
            Me.doSelectAll_SELRY("lnkCZSELRYSelectAll")
        End Sub

        Private Sub lnkCZFWLISTSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZFWLISTSelectAll.Click
            Me.doSelectAll_FWLIST("lnkCZFWLISTSelectAll")
        End Sub

        Private Sub lnkCZJCLXRSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZJCLXRSelectAll.Click
            Me.doSelectAll_JCLXR("lnkCZJCLXRSelectAll")
        End Sub

        Private Sub lnkCZBMRYDeSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZBMRYDeSelectAll.Click
            Me.doDeSelectAll_BMRY("lnkCZBMRYDeSelectAll")
        End Sub

        Private Sub lnkCZSELRYDeSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZSELRYDeSelectAll.Click
            Me.doDeSelectAll_SELRY("lnkCZSELRYDeSelectAll")
        End Sub

        Private Sub lnkCZFWLISTDeSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZFWLISTDeSelectAll.Click
            Me.doDeSelectAll_FWLIST("lnkCZFWLISTDeSelectAll")
        End Sub

        Private Sub lnkCZJCLXRDeSelectAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCZJCLXRDeSelectAll.Click
            Me.doDeSelectAll_JCLXR("lnkCZJCLXRDeSelectAll")
        End Sub

        Private Sub btnBMRYSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBMRYSearch.Click
            Me.doSearch_BMRY("btnBMRYSearch")
        End Sub

        Private Sub btnSELRYSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSELRYSearch.Click
            Me.doSearch_SELRY("btnSELRYSearch")
        End Sub

        Private Sub btnFWLISTSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFWLISTSearch.Click
            Me.doSearch_FWLIST("btnFWLISTSearch")
        End Sub

        Private Sub btnJCLXRSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnJCLXRSearch.Click
            Me.doSearch_JCLXR("btnJCLXRSearch")
        End Sub



        '----------------------------------------------------------------
        '模块特殊操作处理器
        '----------------------------------------------------------------
        '处理“取消”按钮
        Private Sub doCancel(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            '设置返回参数
            Me.m_objIDmxzZzry.oExitMode = False

            '释放模块资源
            Me.releaseModuleParameters()
            Me.releaseInterfaceParameters()

            '返回到调用模块，并附加返回参数
            '要返回的SessionId
            Dim strSessionId As String
            strSessionId = Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.ISessionId)
            'SessionId附加到返回的Url
            Dim strUrl As String
            strUrl = Me.m_objIDmxzZzry.getReturnUrl(Server, Xydc.Platform.Common.Utilities.PulicParameters.OSessionId, strSessionId)
            If Me.m_objIDmxzZzry.iDifferentFrame = True Then
                Me.htxtCloseWindow.Value = "1"
                Me.htxtReturnUrl.Value = MyBase.UrlHost + strUrl
            Else
                '返回
                Me.htxtCloseWindow.Value = "0"
                Me.htxtReturnUrl.Value = ""
                Response.Redirect(strUrl)
            End If

            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        '处理选择人员“移出”按钮
        Private Sub doDelete_SELRY(ByVal strControlId As String)

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '获取数据
                If Me.getModuleData_SELRY(strErrMsg) = False Then
                    GoTo errProc
                End If

                '检查选择
                Dim blnChecked As Boolean
                Dim intRecPos As Integer
                Dim blnDo As Boolean
                Dim intCount As Integer
                Dim i As Integer
                intCount = Me.grdSELRY.Items.Count
                blnDo = False
                For i = intCount - 1 To 0 Step -1
                    blnChecked = objDataGridProcess.isDataGridItemChecked(Me.grdSELRY.Items(i), 0, Me.m_cstrCheckBoxIdInDataGrid_SELRY)
                    If blnChecked = True Then
                        '获取记录位置
                        intRecPos = objDataGridProcess.getRecordPosition(i, Me.grdSELRY.CurrentPageIndex, Me.grdSELRY.PageSize)

                        '删除
                        With Me.m_objDataSet_SELRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT)
                            .Rows.Remove(.DefaultView.Item(intRecPos).Row)
                        End With

                        '标志发生修改
                        blnDo = True
                    End If
                Next

                '刷新显示
                If blnDo = True Then
                    If Me.showModuleData_SELRY(strErrMsg) = False Then
                        GoTo errProc
                    End If
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

normExit:
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        '处理部门人员“加入”按钮
        Private Sub doAddfromBMRY_SELRY(ByVal strControlId As String)

            Dim objsystemCommon As New Xydc.Platform.BusinessFacade.systemCommon
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '获取数据
                If Me.getModuleData_SELRY(strErrMsg) = False Then
                    GoTo errProc
                End If

                '获取SELRY对应到BMRY表中的列索引
                Dim intColIndex(10) As Integer
                intColIndex(0) = objDataGridProcess.getDataGridColumnIndex(Me.grdBMRY, Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYMC)
                intColIndex(1) = objDataGridProcess.getDataGridColumnIndex(Me.grdBMRY, Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYXH)
                intColIndex(2) = objDataGridProcess.getDataGridColumnIndex(Me.grdBMRY, Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_ZZMC)
                intColIndex(3) = objDataGridProcess.getDataGridColumnIndex(Me.grdBMRY, Xydc.Platform.Common.Data.XingzhengjibieData.FIELD_GG_B_XINGZHENGJIBIE_JBMC)
                intColIndex(4) = objDataGridProcess.getDataGridColumnIndex(Me.grdBMRY, Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_FULLJOIN_GWLB)
                intColIndex(5) = objDataGridProcess.getDataGridColumnIndex(Me.grdBMRY, Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_FULLJOIN_MSMC)
                intColIndex(6) = objDataGridProcess.getDataGridColumnIndex(Me.grdBMRY, Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SJHM)
                intColIndex(7) = objDataGridProcess.getDataGridColumnIndex(Me.grdBMRY, Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_LXDH)
                intColIndex(8) = objDataGridProcess.getDataGridColumnIndex(Me.grdBMRY, Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_FTPDZ)
                intColIndex(9) = objDataGridProcess.getDataGridColumnIndex(Me.grdBMRY, Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_YXDZ)

                '检查选择
                Dim blnChecked As Boolean
                Dim blnFound As Boolean
                Dim blnDo As Boolean
                Dim intCount As Integer
                Dim i As Integer
                intCount = Me.grdBMRY.Items.Count
                blnDo = False
                For i = 0 To intCount - 1 Step 1
                    blnChecked = objDataGridProcess.isDataGridItemChecked(Me.grdBMRY.Items(i), 0, Me.m_cstrCheckBoxIdInDataGrid_BMRY)
                    If blnChecked = True Then
                        '检查是否存在？
                        Dim strMC As String
                        strMC = objDataGridProcess.getDataGridCellValue(Me.grdBMRY.Items(i), intColIndex(0))
                        If objsystemCommon.doFindInDataTable(strErrMsg, _
                            Me.m_objDataSet_SELRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT), _
                            Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_MC, _
                            strMC, blnFound) = False Then
                            GoTo errProc
                        End If

                        If blnFound = False Then
                            '加入
                            Dim objDataRow As System.Data.DataRow
                            With Me.m_objDataSet_SELRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT)
                                objDataRow = .NewRow()
                            End With
                            With objDataGridProcess
                                objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_LX) = Xydc.Platform.Common.Data.FenfafanweiData.CYLX_GEREN
                                objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_MC) = .getDataGridCellValue(Me.grdBMRY.Items(i), intColIndex(0))
                                objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_XH) = .getDataGridCellValue(Me.grdBMRY.Items(i), intColIndex(1))
                                objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_BM) = .getDataGridCellValue(Me.grdBMRY.Items(i), intColIndex(2))
                                objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_JB) = .getDataGridCellValue(Me.grdBMRY.Items(i), intColIndex(3))
                                objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_ZW) = .getDataGridCellValue(Me.grdBMRY.Items(i), intColIndex(4))
                                objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_MS) = .getDataGridCellValue(Me.grdBMRY.Items(i), intColIndex(5))
                                objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_SJHM) = .getDataGridCellValue(Me.grdBMRY.Items(i), intColIndex(6))
                                objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_LXDH) = .getDataGridCellValue(Me.grdBMRY.Items(i), intColIndex(7))
                                objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_FTPDZ) = .getDataGridCellValue(Me.grdBMRY.Items(i), intColIndex(8))
                                objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_YXDZ) = .getDataGridCellValue(Me.grdBMRY.Items(i), intColIndex(9))
                            End With
                            With Me.m_objDataSet_SELRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT)
                                .Rows.Add(objDataRow)
                            End With

                            '标志发生修改
                            blnDo = True
                        End If
                    End If
                Next

                '刷新显示
                If blnDo = True Then
                    If Me.showModuleData_SELRY(strErrMsg) = False Then
                        GoTo errProc
                    End If
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

normExit:
            Xydc.Platform.BusinessFacade.systemCommon.SafeRelease(objsystemCommon)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.BusinessFacade.systemCommon.SafeRelease(objsystemCommon)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        '处理部门人员“加到联系人”按钮
        Private Sub doAddfromBMRY_LXR(ByVal strControlId As String)

            Dim objsystemJingchanglianxiren As New Xydc.Platform.BusinessFacade.systemJingchanglianxiren
            Dim objsystemCustomer As New Xydc.Platform.BusinessFacade.systemCustomer
            Dim objsystemCommon As New Xydc.Platform.BusinessFacade.systemCommon
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                Dim strRYMC As String = Me.m_objIDmxzZzry.iCurrentBlr
                Dim strRYDM As String = ""
                If strRYMC <> "" Then
                    If objsystemCustomer.getRydmByRymc(strErrMsg, MyBase.UserId, MyBase.UserPassword, strRYMC, strRYDM) = False Then
                        GoTo errProc
                    End If
                Else
                    strRYDM = MyBase.UserId
                End If
                If strRYDM = "" Then
                    strErrMsg = "错误：无法获取[" + strRYMC + "]的人员标识！"
                    GoTo errProc
                End If

                '获取SELRY对应到BMRY表中的列索引
                Dim intColIndex(10) As Integer
                intColIndex(0) = objDataGridProcess.getDataGridColumnIndex(Me.grdBMRY, Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYDM)

                '检查选择
                Dim blnChecked As Boolean
                Dim strLXRDM As String
                Dim blnDo As Boolean
                Dim intCount As Integer
                Dim i As Integer
                intCount = Me.grdBMRY.Items.Count
                blnDo = False
                For i = 0 To intCount - 1 Step 1
                    blnChecked = objDataGridProcess.isDataGridItemChecked(Me.grdBMRY.Items(i), 0, Me.m_cstrCheckBoxIdInDataGrid_BMRY)
                    If blnChecked = True Then
                        '获取“人员代码”
                        strLXRDM = objDataGridProcess.getDataGridCellValue(Me.grdBMRY.Items(i), intColIndex(0))
                        If strLXRDM <> "" Then
                            If objsystemJingchanglianxiren.doAddJclxr(strErrMsg, MyBase.UserId, MyBase.UserPassword, strRYDM, strLXRDM) = False Then
                                GoTo errProc
                            Else
                                '标志发生修改
                                blnDo = True
                            End If
                        End If
                    End If
                Next

                '刷新显示
                If blnDo = True Then
                    If Me.getModuleData_JCLXR(strErrMsg, Me.m_strQuery_JCLXR) = False Then
                        GoTo errProc
                    End If
                    If Me.showModuleData_JCLXR(strErrMsg) = False Then
                        GoTo errProc
                    End If
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

normExit:
            Xydc.Platform.BusinessFacade.systemJingchanglianxiren.SafeRelease(objsystemJingchanglianxiren)
            Xydc.Platform.BusinessFacade.systemCustomer.SafeRelease(objsystemCustomer)
            Xydc.Platform.BusinessFacade.systemCommon.SafeRelease(objsystemCommon)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.BusinessFacade.systemJingchanglianxiren.SafeRelease(objsystemJingchanglianxiren)
            Xydc.Platform.BusinessFacade.systemCustomer.SafeRelease(objsystemCustomer)
            Xydc.Platform.BusinessFacade.systemCommon.SafeRelease(objsystemCommon)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        '处理经常联系人“移出”按钮
        Private Sub btnJCLXRDelte_JCLXR(ByVal strControlId As String)

            Dim objsystemJingchanglianxiren As New Xydc.Platform.BusinessFacade.systemJingchanglianxiren
            Dim objsystemCustomer As New Xydc.Platform.BusinessFacade.systemCustomer
            Dim objsystemCommon As New Xydc.Platform.BusinessFacade.systemCommon
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                Dim strRYMC As String = Me.m_objIDmxzZzry.iCurrentBlr
                Dim strRYDM As String = ""
                If strRYMC <> "" Then
                    If objsystemCustomer.getRydmByRymc(strErrMsg, MyBase.UserId, MyBase.UserPassword, strRYMC, strRYDM) = False Then
                        GoTo errProc
                    End If
                Else
                    strRYDM = MyBase.UserId
                End If
                If strRYDM = "" Then
                    strErrMsg = "错误：无法获取[" + strRYMC + "]的人员标识！"
                    GoTo errProc
                End If

                '获取SELRY对应的列索引
                Dim intColIndex(10) As Integer
                intColIndex(0) = objDataGridProcess.getDataGridColumnIndex(Me.grdJCLXR, Xydc.Platform.Common.Data.JingchanglianxirenData.FIELD_GW_B_JINGCHANGLIANXIREN_LXRDM)

                '检查选择
                Dim blnChecked As Boolean
                Dim strLXRDM As String = ""
                Dim blnDo As Boolean
                Dim intCount As Integer
                Dim i As Integer
                intCount = Me.grdJCLXR.Items.Count
                blnDo = False
                For i = 0 To intCount - 1 Step 1
                    blnChecked = objDataGridProcess.isDataGridItemChecked(Me.grdJCLXR.Items(i), 0, Me.m_cstrCheckBoxIdInDataGrid_JCLXR)
                    If blnChecked = True Then
                        '获取“人员代码”
                        strLXRDM = objDataGridProcess.getDataGridCellValue(Me.grdJCLXR.Items(i), intColIndex(0))
                        If strLXRDM <> "" Then
                            If objsystemJingchanglianxiren.doDeleteJclxr(strErrMsg, MyBase.UserId, MyBase.UserPassword, strRYDM, strLXRDM) = False Then
                                GoTo errProc
                            Else
                                '标志发生修改
                                blnDo = True
                            End If
                        End If
                    End If
                Next

                '刷新显示
                If blnDo = True Then
                    If Me.getModuleData_JCLXR(strErrMsg, Me.m_strQuery_JCLXR) = False Then
                        GoTo errProc
                    End If
                    If Me.showModuleData_JCLXR(strErrMsg) = False Then
                        GoTo errProc
                    End If
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

normExit:
            Xydc.Platform.BusinessFacade.systemJingchanglianxiren.SafeRelease(objsystemJingchanglianxiren)
            Xydc.Platform.BusinessFacade.systemCustomer.SafeRelease(objsystemCustomer)
            Xydc.Platform.BusinessFacade.systemCommon.SafeRelease(objsystemCommon)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.BusinessFacade.systemJingchanglianxiren.SafeRelease(objsystemJingchanglianxiren)
            Xydc.Platform.BusinessFacade.systemCustomer.SafeRelease(objsystemCustomer)
            Xydc.Platform.BusinessFacade.systemCommon.SafeRelease(objsystemCommon)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        '处理经常联系人“加入”按钮
        Private Sub doAddfromJCLXR_SELRY(ByVal strControlId As String)

            Dim objsystemCommon As New Xydc.Platform.BusinessFacade.systemCommon
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '获取数据
                If Me.getModuleData_SELRY(strErrMsg) = False Then
                    GoTo errProc
                End If

                '获取SELRY对应到JCLXR表中的列索引
                Dim intColIndex(10) As Integer
                intColIndex(0) = objDataGridProcess.getDataGridColumnIndex(Me.grdJCLXR, Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYMC)
                intColIndex(1) = objDataGridProcess.getDataGridColumnIndex(Me.grdJCLXR, Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYXH)
                intColIndex(2) = objDataGridProcess.getDataGridColumnIndex(Me.grdJCLXR, Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_ZZMC)
                intColIndex(3) = objDataGridProcess.getDataGridColumnIndex(Me.grdJCLXR, Xydc.Platform.Common.Data.XingzhengjibieData.FIELD_GG_B_XINGZHENGJIBIE_JBMC)
                intColIndex(4) = objDataGridProcess.getDataGridColumnIndex(Me.grdJCLXR, Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_FULLJOIN_GWLB)
                intColIndex(5) = objDataGridProcess.getDataGridColumnIndex(Me.grdJCLXR, Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_FULLJOIN_MSMC)
                intColIndex(6) = objDataGridProcess.getDataGridColumnIndex(Me.grdJCLXR, Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SJHM)
                intColIndex(7) = objDataGridProcess.getDataGridColumnIndex(Me.grdJCLXR, Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_LXDH)
                intColIndex(8) = objDataGridProcess.getDataGridColumnIndex(Me.grdJCLXR, Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_FTPDZ)
                intColIndex(9) = objDataGridProcess.getDataGridColumnIndex(Me.grdJCLXR, Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_YXDZ)

                '检查选择
                Dim blnChecked As Boolean
                Dim blnFound As Boolean
                Dim blnDo As Boolean
                Dim intCount As Integer
                Dim i As Integer
                intCount = Me.grdJCLXR.Items.Count
                blnDo = False
                For i = 0 To intCount - 1 Step 1
                    blnChecked = objDataGridProcess.isDataGridItemChecked(Me.grdJCLXR.Items(i), 0, Me.m_cstrCheckBoxIdInDataGrid_JCLXR)
                    If blnChecked = True Then
                        '检查是否存在？
                        Dim strMC As String
                        strMC = objDataGridProcess.getDataGridCellValue(Me.grdJCLXR.Items(i), intColIndex(0))
                        If objsystemCommon.doFindInDataTable(strErrMsg, _
                            Me.m_objDataSet_SELRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT), _
                            Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_MC, _
                            strMC, blnFound) = False Then
                            GoTo errProc
                        End If

                        If blnFound = False Then
                            '加入
                            Dim objDataRow As System.Data.DataRow
                            With Me.m_objDataSet_SELRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT)
                                objDataRow = .NewRow()
                            End With
                            With objDataGridProcess
                                objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_LX) = Xydc.Platform.Common.Data.FenfafanweiData.CYLX_GEREN
                                objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_MC) = .getDataGridCellValue(Me.grdJCLXR.Items(i), intColIndex(0))
                                objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_XH) = .getDataGridCellValue(Me.grdJCLXR.Items(i), intColIndex(1))
                                objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_BM) = .getDataGridCellValue(Me.grdJCLXR.Items(i), intColIndex(2))
                                objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_JB) = .getDataGridCellValue(Me.grdJCLXR.Items(i), intColIndex(3))
                                objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_ZW) = .getDataGridCellValue(Me.grdJCLXR.Items(i), intColIndex(4))
                                objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_MS) = .getDataGridCellValue(Me.grdJCLXR.Items(i), intColIndex(5))
                                objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_SJHM) = .getDataGridCellValue(Me.grdJCLXR.Items(i), intColIndex(6))
                                objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_LXDH) = .getDataGridCellValue(Me.grdJCLXR.Items(i), intColIndex(7))
                                objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_FTPDZ) = .getDataGridCellValue(Me.grdJCLXR.Items(i), intColIndex(8))
                                objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_YXDZ) = .getDataGridCellValue(Me.grdJCLXR.Items(i), intColIndex(9))
                            End With
                            With Me.m_objDataSet_SELRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT)
                                .Rows.Add(objDataRow)
                            End With

                            '标志发生修改
                            blnDo = True
                        End If
                    End If
                Next

                '刷新显示
                If blnDo = True Then
                    If Me.showModuleData_SELRY(strErrMsg) = False Then
                        GoTo errProc
                    End If
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

normExit:
            Xydc.Platform.BusinessFacade.systemCommon.SafeRelease(objsystemCommon)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.BusinessFacade.systemCommon.SafeRelease(objsystemCommon)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        '处理我的常用范围“加入”按钮
        Private Sub doAddfromFWLIST_SELRY(ByVal strControlId As String)

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objsystemCustomer As New Xydc.Platform.BusinessFacade.systemCustomer
            Dim objsystemCommon As New Xydc.Platform.BusinessFacade.systemCommon
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '获取数据
                If Me.getModuleData_SELRY(strErrMsg) = False Then
                    GoTo errProc
                End If

                '获取SELRY对应到BMRY表中的列索引
                Dim intColIndex(10) As Integer
                intColIndex(0) = objDataGridProcess.getDataGridColumnIndex(Me.grdFWLIST, Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_FWMC)
                intColIndex(1) = -1
                intColIndex(2) = -1
                intColIndex(3) = -1
                intColIndex(4) = -1
                intColIndex(5) = -1
                intColIndex(6) = objDataGridProcess.getDataGridColumnIndex(Me.grdFWLIST, Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_SJHM)
                intColIndex(7) = objDataGridProcess.getDataGridColumnIndex(Me.grdFWLIST, Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_LXDH)
                intColIndex(8) = objDataGridProcess.getDataGridColumnIndex(Me.grdFWLIST, Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_FTPDZ)
                intColIndex(9) = objDataGridProcess.getDataGridColumnIndex(Me.grdFWLIST, Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_YXDZ)

                '检查选择
                Dim objDataRow As System.Data.DataRow
                Dim blnChecked As Boolean
                Dim blnFound As Boolean
                Dim strFWMC As String
                Dim blnDo As Boolean
                Dim intCount As Integer
                Dim i As Integer
                intCount = Me.grdFWLIST.Items.Count
                blnDo = False
                For i = 0 To intCount - 1 Step 1
                    blnChecked = objDataGridProcess.isDataGridItemChecked(Me.grdFWLIST.Items(i), 0, Me.m_cstrCheckBoxIdInDataGrid_FWLIST)
                    If blnChecked = True Then
                        '获取范围名称
                        strFWMC = objDataGridProcess.getDataGridCellValue(Me.grdFWLIST.Items(i), intColIndex(0))

                        If Me.m_objIDmxzZzry.iSelectMode = True Or Me.m_objIDmxzZzry.iSelectFFFW = True Then '可以直接选择范围
                            '检查是否存在？
                            If objsystemCommon.doFindInDataTable(strErrMsg, _
                                Me.m_objDataSet_SELRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT), _
                                Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_MC, _
                                strFWMC, blnFound) = False Then
                                GoTo errProc
                            End If

                            If blnFound = False Then
                                '加入
                                With Me.m_objDataSet_SELRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT)
                                    objDataRow = .NewRow()
                                End With
                                With objDataGridProcess
                                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_LX) = Xydc.Platform.Common.Data.FenfafanweiData.CYLX_FANWEI
                                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_MC) = .getDataGridCellValue(Me.grdFWLIST.Items(i), intColIndex(0))
                                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_XH) = ""
                                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_BM) = ""
                                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_JB) = ""
                                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_ZW) = ""
                                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_MS) = ""
                                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_SJHM) = .getDataGridCellValue(Me.grdFWLIST.Items(i), intColIndex(6))
                                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_LXDH) = .getDataGridCellValue(Me.grdFWLIST.Items(i), intColIndex(7))
                                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_FTPDZ) = .getDataGridCellValue(Me.grdFWLIST.Items(i), intColIndex(8))
                                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_YXDZ) = .getDataGridCellValue(Me.grdFWLIST.Items(i), intColIndex(9))
                                End With
                                With Me.m_objDataSet_SELRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT)
                                    .Rows.Add(objDataRow)
                                End With

                                '标志发生修改
                                blnDo = True
                            End If

                        Else '只能选择范围下的部门或人员
                            Dim objBumenRenyuanDataInFanwei As Xydc.Platform.Common.Data.CustomerData
                            Dim intCYCount As Integer
                            Dim strCYMC As String
                            Dim j As Integer

                            If Me.m_objIDmxzZzry.iSelectBMMC = True Then '可选择部门
                                '获取范围内的部门与人员
                                If objsystemCustomer.getRenyuanOrBumenInFanweiData(strErrMsg, _
                                    MyBase.UserId, MyBase.UserPassword, _
                                    strFWMC, True, "", _
                                    objBumenRenyuanDataInFanwei) = False Then
                                    GoTo errProc
                                End If
                            Else '只能选择人员
                                '获取范围内人员
                                If objsystemCustomer.getRenyuanOrBumenInFanweiData(strErrMsg, _
                                    MyBase.UserId, MyBase.UserPassword, _
                                    strFWMC, False, "", _
                                    objBumenRenyuanDataInFanwei) = False Then
                                    GoTo errProc
                                End If
                            End If

                            With objBumenRenyuanDataInFanwei.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT)
                                intCYCount = .Rows.Count
                                For j = 0 To intCYCount - 1 Step 1
                                    '计算成员名称
                                    strCYMC = objPulicParameters.getObjectValue(.Rows(j).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_MC), "")

                                    '检查是否存在？
                                    If objsystemCommon.doFindInDataTable(strErrMsg, _
                                        Me.m_objDataSet_SELRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT), _
                                        Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_MC, _
                                        strCYMC, blnFound) = False Then
                                        GoTo errProc
                                    End If

                                    If blnFound = False Then
                                        '加入
                                        With Me.m_objDataSet_SELRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT)
                                            objDataRow = .NewRow()
                                        End With
                                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_LX) = .Rows(j).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_LX)
                                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_MC) = .Rows(j).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_MC)
                                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_XH) = .Rows(j).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_XH)
                                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_BM) = .Rows(j).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_BM)
                                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_JB) = .Rows(j).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_JB)
                                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_ZW) = .Rows(j).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_ZW)
                                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_MS) = .Rows(j).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_MS)
                                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_SJHM) = .Rows(j).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_SJHM)
                                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_LXDH) = .Rows(j).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_LXDH)
                                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_FTPDZ) = .Rows(j).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_FTPDZ)
                                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_YXDZ) = .Rows(j).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_YXDZ)
                                        With Me.m_objDataSet_SELRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT)
                                            .Rows.Add(objDataRow)
                                        End With

                                        '标志发生修改
                                        blnDo = True
                                    End If
                                Next
                            End With

                            '释放临时资源
                            If Not (objBumenRenyuanDataInFanwei Is Nothing) Then
                                objBumenRenyuanDataInFanwei.Dispose()
                                objBumenRenyuanDataInFanwei = Nothing
                            End If
                        End If
                    End If
                Next

                '刷新显示
                If blnDo = True Then
                    If Me.showModuleData_SELRY(strErrMsg) = False Then
                        GoTo errProc
                    End If
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

normExit:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.BusinessFacade.systemCustomer.SafeRelease(objsystemCustomer)
            Xydc.Platform.BusinessFacade.systemCommon.SafeRelease(objsystemCommon)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.BusinessFacade.systemCustomer.SafeRelease(objsystemCustomer)
            Xydc.Platform.BusinessFacade.systemCommon.SafeRelease(objsystemCommon)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        '处理手工输入代码的按钮
        Private Sub doAddfromInput_SELRY(ByVal strControlId As String)

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '检查输入
                Dim objListItem As System.Web.UI.WebControls.ListItem
                With New Xydc.Platform.web.RadioButtonListProcess
                    objListItem = .getCheckedItem(Me.rblXZLX)
                End With
                If objListItem Is Nothing Then
                    strErrMsg = "错误：没有指定类型[个人/单位/范围]！"
                    GoTo errProc
                End If
                If Me.txtNewRYMC.Text.Length > 0 Then Me.txtNewRYMC.Text = Me.txtNewRYMC.Text.Trim()
                If Me.txtNewRYMC.Text = "" Then
                    strErrMsg = "错误：没有输入[个人/单位/范围]的值！"
                    GoTo errProc
                End If

                '获取数据
                If Me.getModuleData_SELRY(strErrMsg) = False Then
                    GoTo errProc
                End If

                '检查是否存在？
                Dim blnFound As Boolean
                With New Xydc.Platform.BusinessFacade.systemCommon
                    If .doFindInDataTable(strErrMsg, _
                        Me.m_objDataSet_SELRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT), _
                        Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_MC, _
                        Me.txtNewRYMC.Text, blnFound) = False Then
                        GoTo errProc
                    End If
                End With

                Dim objDataRow As System.Data.DataRow
                Dim blnDo As Boolean = False
                If blnFound = False Then
                    '加入
                    With Me.m_objDataSet_SELRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT)
                        objDataRow = .NewRow()
                    End With
                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_LX) = objListItem.Text
                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_MC) = Me.txtNewRYMC.Text
                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_XH) = ""
                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_BM) = ""
                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_JB) = ""
                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_ZW) = ""
                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_MS) = ""
                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_SJHM) = ""
                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_LXDH) = ""
                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_FTPDZ) = ""
                    objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_YXDZ) = ""
                    With Me.m_objDataSet_SELRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT)
                        .Rows.Add(objDataRow)
                    End With

                    '标志发生修改
                    blnDo = True
                End If

                '刷新显示
                If blnDo = True Then
                    If Me.showModuleData_SELRY(strErrMsg) = False Then
                        GoTo errProc
                    End If
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

        '处理“确定”按钮
        Private Sub doConfirm(ByVal strControlId As String)

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            '获取选择数据
            If Me.getModuleData_SELRY(strErrMsg) = False Then
                GoTo errProc
            End If

            Dim strReturnValue As String = ""
            Try
                '检查选择数据
                With Me.m_objDataSet_SELRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT)
                    If .Rows.Count < 1 And Me.m_objIDmxzZzry.iAllowNull = False Then
                        strErrMsg = "错误：没有选择任何内容！"
                        GoTo errProc
                    End If
                    If Me.m_objIDmxzZzry.iMultiSelect = False Then
                        If .Rows.Count > 1 Then
                            strErrMsg = "错误：只允许选择1条！"
                            GoTo errProc
                        End If
                    End If
                End With

                With Me.m_objDataSet_SELRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT)
                    If .Rows.Count < 1 Then
                        '设置返回值
                        Me.m_objIDmxzZzry.oRenyuanList = ""
                    Else
                        '获取返回参数
                        Dim strSep As String = Xydc.Platform.Common.Utilities.PulicParameters.CharSeparate
                        Dim strValue As String
                        Dim intCount As Integer
                        Dim i As Integer
                        With Me.m_objDataSet_SELRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT)
                            intCount = .Rows.Count
                            For i = 0 To intCount - 1 Step 1
                                strValue = objPulicParameters.getObjectValue(.Rows(i).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_MC), "")
                                If strValue <> "" Then
                                    If strReturnValue <> "" Then
                                        strReturnValue = strReturnValue + strSep + strValue
                                    Else
                                        strReturnValue = strValue
                                    End If
                                End If
                            Next
                        End With

                        '清除所有的RowFilter
                        With Me.m_objDataSet_SELRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT)
                            .DefaultView.RowFilter = ""
                        End With

                        '设置返回值
                        Me.m_objIDmxzZzry.oRenyuanList = strReturnValue
                        Me.m_objIDmxzZzry.oDataSet = Me.m_objDataSet_SELRY


                        If Me.m_strSessionId_SELRY.Trim <> "" Then
                            Try
                                Session.Remove(Me.m_strSessionId_SELRY)
                            Catch ex As Exception
                            End Try
                            Me.m_strSessionId_SELRY = ""
                        End If

                    End If
                End With

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            '设置返回参数
            Me.m_objIDmxzZzry.oExitMode = True

            '释放模块资源
            Me.releaseModuleParameters()
            Me.releaseInterfaceParameters()

            '返回到调用模块，并附加返回参数
            '要返回的SessionId
            Dim strSessionId As String
            strSessionId = Request.QueryString(Xydc.Platform.Common.Utilities.PulicParameters.ISessionId)
            'SessionId附加到返回的Url
            Dim strUrl As String
            strUrl = Me.m_objIDmxzZzry.getReturnUrl(Server, Xydc.Platform.Common.Utilities.PulicParameters.OSessionId, strSessionId)
            If Me.m_objIDmxzZzry.iDifferentFrame = True Then
                Me.htxtCloseWindow.Value = "1"
                Me.htxtReturnUrl.Value = MyBase.UrlHost + strUrl
            Else
                Me.htxtCloseWindow.Value = "0"
                Me.htxtReturnUrl.Value = ""
                Response.Redirect(strUrl)
            End If

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Sub

        End Sub

        Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
            Me.doCancel("btnCancel")
        End Sub

        Private Sub btnSELRYDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSELRYDelete.Click
            Me.doDelete_SELRY("btnSELRYDelete")
        End Sub

        Private Sub btnBMRYAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBMRYAdd.Click
            Me.doAddfromBMRY_SELRY("btnBMRYAdd")
        End Sub

        Private Sub btnBMRYAddLxr_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBMRYAddLxr.Click
            Me.doAddfromBMRY_LXR("btnBMRYAddLxr")
        End Sub

        Private Sub btnJCLXRAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnJCLXRAdd.Click
            Me.doAddfromJCLXR_SELRY("btnJCLXRAdd")
        End Sub

        Private Sub btnJCLXRDelte_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnJCLXRDelte.Click
            Me.btnJCLXRDelte_JCLXR("btnJCLXRAdd")
        End Sub

        Private Sub btnFWLISTAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFWLISTAdd.Click
            Me.doAddfromFWLIST_SELRY("btnFWLISTAdd")
        End Sub

        Private Sub btnAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
            Me.doAddfromInput_SELRY("btnAddNew")
        End Sub

        Private Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.Click
            Me.doConfirm("btnOK")
        End Sub

        '处理单个部门人员“加入”按钮
        Private Function doAddfromBMRY_SELRY_One(ByRef strErrMsg As String) As Boolean

            Dim objsystemCommon As New Xydc.Platform.BusinessFacade.systemCommon
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess

            doAddfromBMRY_SELRY_One = False

            Try
                '检查当前行
                If Me.grdBMRY.Items.Count < 1 Then
                    strErrMsg = "错误：没有数据！"
                    GoTo errProc
                End If
                If Me.grdBMRY.SelectedIndex < 0 Then
                    strErrMsg = "错误：没有选定数据！"
                    GoTo errProc
                End If

                '获取数据
                If Me.getModuleData_SELRY(strErrMsg) = False Then
                    GoTo errProc
                End If

                '获取SELRY对应到BMRY表中的列索引
                Dim intColIndex(10) As Integer
                intColIndex(0) = objDataGridProcess.getDataGridColumnIndex(Me.grdBMRY, Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYMC)
                intColIndex(1) = objDataGridProcess.getDataGridColumnIndex(Me.grdBMRY, Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYXH)
                intColIndex(2) = objDataGridProcess.getDataGridColumnIndex(Me.grdBMRY, Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_ZZMC)
                intColIndex(3) = objDataGridProcess.getDataGridColumnIndex(Me.grdBMRY, Xydc.Platform.Common.Data.XingzhengjibieData.FIELD_GG_B_XINGZHENGJIBIE_JBMC)
                intColIndex(4) = objDataGridProcess.getDataGridColumnIndex(Me.grdBMRY, Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_FULLJOIN_GWLB)
                intColIndex(5) = objDataGridProcess.getDataGridColumnIndex(Me.grdBMRY, Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_FULLJOIN_MSMC)
                intColIndex(6) = objDataGridProcess.getDataGridColumnIndex(Me.grdBMRY, Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SJHM)
                intColIndex(7) = objDataGridProcess.getDataGridColumnIndex(Me.grdBMRY, Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_LXDH)
                intColIndex(8) = objDataGridProcess.getDataGridColumnIndex(Me.grdBMRY, Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_FTPDZ)
                intColIndex(9) = objDataGridProcess.getDataGridColumnIndex(Me.grdBMRY, Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_YXDZ)

                '检查选择
                Dim blnFound As Boolean
                Dim blnDo As Boolean
                Dim i As Integer
                i = Me.grdBMRY.SelectedIndex
                blnDo = False

                '检查是否存在？
                Dim strMC As String
                strMC = objDataGridProcess.getDataGridCellValue(Me.grdBMRY.Items(i), intColIndex(0))
                If objsystemCommon.doFindInDataTable(strErrMsg, _
                    Me.m_objDataSet_SELRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT), _
                    Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_MC, _
                    strMC, blnFound) = False Then
                    GoTo errProc
                End If

                '加入
                If blnFound = False Then
                    Dim objDataRow As System.Data.DataRow
                    With Me.m_objDataSet_SELRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT)
                        objDataRow = .NewRow()
                    End With
                    With objDataGridProcess
                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_LX) = Xydc.Platform.Common.Data.FenfafanweiData.CYLX_GEREN
                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_MC) = .getDataGridCellValue(Me.grdBMRY.Items(i), intColIndex(0))
                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_XH) = .getDataGridCellValue(Me.grdBMRY.Items(i), intColIndex(1))
                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_BM) = .getDataGridCellValue(Me.grdBMRY.Items(i), intColIndex(2))
                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_JB) = .getDataGridCellValue(Me.grdBMRY.Items(i), intColIndex(3))
                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_ZW) = .getDataGridCellValue(Me.grdBMRY.Items(i), intColIndex(4))
                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_MS) = .getDataGridCellValue(Me.grdBMRY.Items(i), intColIndex(5))
                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_SJHM) = .getDataGridCellValue(Me.grdBMRY.Items(i), intColIndex(6))
                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_LXDH) = .getDataGridCellValue(Me.grdBMRY.Items(i), intColIndex(7))
                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_FTPDZ) = .getDataGridCellValue(Me.grdBMRY.Items(i), intColIndex(8))
                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_YXDZ) = .getDataGridCellValue(Me.grdBMRY.Items(i), intColIndex(9))
                    End With
                    With Me.m_objDataSet_SELRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT)
                        .Rows.Add(objDataRow)
                    End With

                    '标志发生修改
                    blnDo = True
                End If

                '刷新显示
                If blnDo = True Then
                    If Me.showModuleData_SELRY(strErrMsg) = False Then
                        GoTo errProc
                    End If
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

normExit:
            Xydc.Platform.BusinessFacade.systemCommon.SafeRelease(objsystemCommon)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)

            doAddfromBMRY_SELRY_One = True
            Exit Function

errProc:
            Xydc.Platform.BusinessFacade.systemCommon.SafeRelease(objsystemCommon)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Function

        End Function

        Private Sub grdBMRY_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdBMRY.ItemCommand

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '修改当前行
                Me.grdBMRY.SelectedIndex = e.Item.ItemIndex

                '显示记录位置
                With New Xydc.Platform.web.DataGridProcess
                    Me.lblBMRYGridLocInfo.Text = .getDataGridLocation(Me.grdBMRY, Me.m_intRows_BMRY)
                End With

                '处理
                Select Case e.CommandName.ToUpper()
                    Case "AddOneRow".ToUpper()
                        If Me.doAddfromBMRY_SELRY_One(strErrMsg) = False Then
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

        '处理单个经常联系人“加入”按钮
        Private Function doAddfromJCLXR_SELRY_One(ByRef strErrMsg As String) As Boolean

            Dim objsystemCommon As New Xydc.Platform.BusinessFacade.systemCommon
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess

            doAddfromJCLXR_SELRY_One = False

            Try
                '检查当前行
                If Me.grdJCLXR.Items.Count < 1 Then
                    strErrMsg = "错误：没有数据！"
                    GoTo errProc
                End If
                If Me.grdJCLXR.SelectedIndex < 0 Then
                    strErrMsg = "错误：没有选定数据！"
                    GoTo errProc
                End If

                '获取数据
                If Me.getModuleData_SELRY(strErrMsg) = False Then
                    GoTo errProc
                End If

                '获取SELRY对应到JCLXR表中的列索引
                Dim intColIndex(10) As Integer
                intColIndex(0) = objDataGridProcess.getDataGridColumnIndex(Me.grdJCLXR, Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYMC)
                intColIndex(1) = objDataGridProcess.getDataGridColumnIndex(Me.grdJCLXR, Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYXH)
                intColIndex(2) = objDataGridProcess.getDataGridColumnIndex(Me.grdJCLXR, Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_ZZMC)
                intColIndex(3) = objDataGridProcess.getDataGridColumnIndex(Me.grdJCLXR, Xydc.Platform.Common.Data.XingzhengjibieData.FIELD_GG_B_XINGZHENGJIBIE_JBMC)
                intColIndex(4) = objDataGridProcess.getDataGridColumnIndex(Me.grdJCLXR, Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_FULLJOIN_GWLB)
                intColIndex(5) = objDataGridProcess.getDataGridColumnIndex(Me.grdJCLXR, Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_FULLJOIN_MSMC)
                intColIndex(6) = objDataGridProcess.getDataGridColumnIndex(Me.grdJCLXR, Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SJHM)
                intColIndex(7) = objDataGridProcess.getDataGridColumnIndex(Me.grdJCLXR, Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_LXDH)
                intColIndex(8) = objDataGridProcess.getDataGridColumnIndex(Me.grdJCLXR, Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_FTPDZ)
                intColIndex(9) = objDataGridProcess.getDataGridColumnIndex(Me.grdJCLXR, Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_YXDZ)

                '检查选择
                Dim blnFound As Boolean
                Dim blnDo As Boolean
                Dim i As Integer
                i = Me.grdJCLXR.SelectedIndex
                blnDo = False

                '检查是否存在？
                Dim strMC As String
                strMC = objDataGridProcess.getDataGridCellValue(Me.grdJCLXR.Items(i), intColIndex(0))
                If objsystemCommon.doFindInDataTable(strErrMsg, _
                    Me.m_objDataSet_SELRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT), _
                    Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_MC, _
                    strMC, blnFound) = False Then
                    GoTo errProc
                End If

                '加入
                If blnFound = False Then
                    Dim objDataRow As System.Data.DataRow
                    With Me.m_objDataSet_SELRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT)
                        objDataRow = .NewRow()
                    End With
                    With objDataGridProcess
                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_LX) = Xydc.Platform.Common.Data.FenfafanweiData.CYLX_GEREN
                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_MC) = .getDataGridCellValue(Me.grdJCLXR.Items(i), intColIndex(0))
                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_XH) = .getDataGridCellValue(Me.grdJCLXR.Items(i), intColIndex(1))
                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_BM) = .getDataGridCellValue(Me.grdJCLXR.Items(i), intColIndex(2))
                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_JB) = .getDataGridCellValue(Me.grdJCLXR.Items(i), intColIndex(3))
                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_ZW) = .getDataGridCellValue(Me.grdJCLXR.Items(i), intColIndex(4))
                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_MS) = .getDataGridCellValue(Me.grdJCLXR.Items(i), intColIndex(5))
                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_SJHM) = .getDataGridCellValue(Me.grdJCLXR.Items(i), intColIndex(6))
                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_LXDH) = .getDataGridCellValue(Me.grdJCLXR.Items(i), intColIndex(7))
                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_FTPDZ) = .getDataGridCellValue(Me.grdJCLXR.Items(i), intColIndex(8))
                        objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_YXDZ) = .getDataGridCellValue(Me.grdJCLXR.Items(i), intColIndex(9))
                    End With
                    With Me.m_objDataSet_SELRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT)
                        .Rows.Add(objDataRow)
                    End With

                    '标志发生修改
                    blnDo = True
                End If

                '刷新显示
                If blnDo = True Then
                    If Me.showModuleData_SELRY(strErrMsg) = False Then
                        GoTo errProc
                    End If
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

normExit:
            Xydc.Platform.BusinessFacade.systemCommon.SafeRelease(objsystemCommon)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)

            doAddfromJCLXR_SELRY_One = True
            Exit Function

errProc:
            Xydc.Platform.BusinessFacade.systemCommon.SafeRelease(objsystemCommon)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Function

        End Function

        Private Sub grdJCLXR_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdJCLXR.ItemCommand

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '修改当前行
                Me.grdJCLXR.SelectedIndex = e.Item.ItemIndex

                '显示记录位置
                With New Xydc.Platform.web.DataGridProcess
                    Me.lblJCLXRGridLocInfo.Text = .getDataGridLocation(Me.grdJCLXR, Me.m_intRows_JCLXR)
                End With

                '处理
                Select Case e.CommandName.ToUpper()
                    Case "AddOneRow".ToUpper()
                        If Me.doAddfromJCLXR_SELRY_One(strErrMsg) = False Then
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

        '处理单个我的常用范围“加入”按钮
        Private Function doAddfromFWLIST_SELRY_One(ByRef strErrMsg As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objsystemCustomer As New Xydc.Platform.BusinessFacade.systemCustomer
            Dim objsystemCommon As New Xydc.Platform.BusinessFacade.systemCommon
            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess

            doAddfromFWLIST_SELRY_One = False

            Try
                '检查当前行
                If Me.grdFWLIST.Items.Count < 1 Then
                    strErrMsg = "错误：没有数据！"
                    GoTo errProc
                End If
                If Me.grdFWLIST.SelectedIndex < 0 Then
                    strErrMsg = "错误：没有选定数据！"
                    GoTo errProc
                End If

                '获取数据
                If Me.getModuleData_SELRY(strErrMsg) = False Then
                    GoTo errProc
                End If

                '获取SELRY对应到BMRY表中的列索引
                Dim intColIndex(10) As Integer
                intColIndex(0) = objDataGridProcess.getDataGridColumnIndex(Me.grdFWLIST, Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_FWMC)
                intColIndex(1) = -1
                intColIndex(2) = -1
                intColIndex(3) = -1
                intColIndex(4) = -1
                intColIndex(5) = -1
                intColIndex(6) = objDataGridProcess.getDataGridColumnIndex(Me.grdFWLIST, Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_SJHM)
                intColIndex(7) = objDataGridProcess.getDataGridColumnIndex(Me.grdFWLIST, Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_LXDH)
                intColIndex(8) = objDataGridProcess.getDataGridColumnIndex(Me.grdFWLIST, Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_FTPDZ)
                intColIndex(9) = objDataGridProcess.getDataGridColumnIndex(Me.grdFWLIST, Xydc.Platform.Common.Data.FenfafanweiData.FIELD_GW_B_FENFAFANWEI_YXDZ)

                '检查选择
                Dim objDataRow As System.Data.DataRow
                Dim blnFound As Boolean
                Dim strFWMC As String
                Dim blnDo As Boolean
                Dim i As Integer
                i = Me.grdFWLIST.SelectedIndex
                blnDo = False

                '获取范围名称
                strFWMC = objDataGridProcess.getDataGridCellValue(Me.grdFWLIST.Items(i), intColIndex(0))

                If Me.m_objIDmxzZzry.iSelectMode = True Or Me.m_objIDmxzZzry.iSelectFFFW = True Then '可以直接选择范围
                    '检查是否存在？
                    If objsystemCommon.doFindInDataTable(strErrMsg, _
                        Me.m_objDataSet_SELRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT), _
                        Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_MC, _
                        strFWMC, blnFound) = False Then
                        GoTo errProc
                    End If

                    '加入
                    If blnFound = False Then
                        With Me.m_objDataSet_SELRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT)
                            objDataRow = .NewRow()
                        End With
                        With objDataGridProcess
                            objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_LX) = Xydc.Platform.Common.Data.FenfafanweiData.CYLX_FANWEI
                            objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_MC) = .getDataGridCellValue(Me.grdFWLIST.Items(i), intColIndex(0))
                            objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_XH) = ""
                            objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_BM) = ""
                            objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_JB) = ""
                            objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_ZW) = ""
                            objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_MS) = ""
                            objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_SJHM) = .getDataGridCellValue(Me.grdFWLIST.Items(i), intColIndex(6))
                            objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_LXDH) = .getDataGridCellValue(Me.grdFWLIST.Items(i), intColIndex(7))
                            objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_FTPDZ) = .getDataGridCellValue(Me.grdFWLIST.Items(i), intColIndex(8))
                            objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_YXDZ) = .getDataGridCellValue(Me.grdFWLIST.Items(i), intColIndex(9))
                        End With
                        With Me.m_objDataSet_SELRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT)
                            .Rows.Add(objDataRow)
                        End With

                        '标志发生修改
                        blnDo = True
                    End If

                Else '只能选择范围下的部门或人员
                    Dim objBumenRenyuanDataInFanwei As Xydc.Platform.Common.Data.CustomerData
                    Dim intCYCount As Integer
                    Dim strCYMC As String
                    Dim j As Integer

                    If Me.m_objIDmxzZzry.iSelectBMMC = True Then '可选择部门
                        '获取范围内的部门与人员
                        If objsystemCustomer.getRenyuanOrBumenInFanweiData(strErrMsg, _
                            MyBase.UserId, MyBase.UserPassword, _
                            strFWMC, True, "", _
                            objBumenRenyuanDataInFanwei) = False Then
                            GoTo errProc
                        End If
                    Else '只能选择人员
                        '获取范围内人员
                        If objsystemCustomer.getRenyuanOrBumenInFanweiData(strErrMsg, _
                            MyBase.UserId, MyBase.UserPassword, _
                            strFWMC, False, "", _
                            objBumenRenyuanDataInFanwei) = False Then
                            GoTo errProc
                        End If
                    End If

                    With objBumenRenyuanDataInFanwei.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT)
                        intCYCount = .Rows.Count
                        For j = 0 To intCYCount - 1 Step 1
                            '计算成员名称
                            strCYMC = objPulicParameters.getObjectValue(.Rows(j).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_MC), "")

                            '检查是否存在？
                            If objsystemCommon.doFindInDataTable(strErrMsg, _
                                Me.m_objDataSet_SELRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT), _
                                Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_MC, _
                                strCYMC, blnFound) = False Then
                                GoTo errProc
                            End If

                            If blnFound = False Then
                                '加入
                                With Me.m_objDataSet_SELRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT)
                                    objDataRow = .NewRow()
                                End With
                                objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_LX) = .Rows(j).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_LX)
                                objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_MC) = .Rows(j).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_MC)
                                objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_XH) = .Rows(j).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_XH)
                                objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_BM) = .Rows(j).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_BM)
                                objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_JB) = .Rows(j).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_JB)
                                objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_ZW) = .Rows(j).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_ZW)
                                objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_MS) = .Rows(j).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_MS)
                                objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_SJHM) = .Rows(j).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_SJHM)
                                objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_LXDH) = .Rows(j).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_LXDH)
                                objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_FTPDZ) = .Rows(j).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_FTPDZ)
                                objDataRow.Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_YXDZ) = .Rows(j).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_SELECT_YXDZ)
                                With Me.m_objDataSet_SELRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT)
                                    .Rows.Add(objDataRow)
                                End With

                                '标志发生修改
                                blnDo = True
                            End If
                        Next
                    End With

                    '释放临时资源
                    If Not (objBumenRenyuanDataInFanwei Is Nothing) Then
                        objBumenRenyuanDataInFanwei.Dispose()
                        objBumenRenyuanDataInFanwei = Nothing
                    End If
                End If

                '刷新显示
                If blnDo = True Then
                    If Me.showModuleData_SELRY(strErrMsg) = False Then
                        GoTo errProc
                    End If
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

normExit:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.BusinessFacade.systemCustomer.SafeRelease(objsystemCustomer)
            Xydc.Platform.BusinessFacade.systemCommon.SafeRelease(objsystemCommon)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)

            doAddfromFWLIST_SELRY_One = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.BusinessFacade.systemCustomer.SafeRelease(objsystemCustomer)
            Xydc.Platform.BusinessFacade.systemCommon.SafeRelease(objsystemCommon)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Function

        End Function

        Private Sub grdFWLIST_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdFWLIST.ItemCommand

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '修改当前行
                Me.grdFWLIST.SelectedIndex = e.Item.ItemIndex

                '显示记录位置
                With New Xydc.Platform.web.DataGridProcess
                    Me.lblFWLISTGridLocInfo.Text = .getDataGridLocation(Me.grdFWLIST, Me.m_intRows_FWLIST)
                End With

                '处理
                Select Case e.CommandName.ToUpper()
                    Case "AddOneRow".ToUpper()
                        If Me.doAddfromFWLIST_SELRY_One(strErrMsg) = False Then
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

        '处理单个选择人员“移出”按钮
        Private Function doDelete_SELRY_One(ByRef strErrMsg As String) As Boolean

            Dim objDataGridProcess As New Xydc.Platform.web.DataGridProcess
            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess

            doDelete_SELRY_One = False

            Try
                '检查当前行
                If Me.grdSELRY.Items.Count < 1 Then
                    strErrMsg = "错误：没有数据！"
                    GoTo errProc
                End If
                If Me.grdSELRY.SelectedIndex < 0 Then
                    strErrMsg = "错误：没有选定数据！"
                    GoTo errProc
                End If

                '获取数据
                If Me.getModuleData_SELRY(strErrMsg) = False Then
                    GoTo errProc
                End If

                '检查选择
                Dim intRecPos As Integer
                Dim blnDo As Boolean
                Dim i As Integer
                i = Me.grdSELRY.SelectedIndex
                blnDo = False

                '获取记录位置
                intRecPos = objDataGridProcess.getRecordPosition(i, Me.grdSELRY.CurrentPageIndex, Me.grdSELRY.PageSize)

                '删除
                With Me.m_objDataSet_SELRY.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_SELECT)
                    .Rows.Remove(.DefaultView.Item(intRecPos).Row)
                End With

                '标志发生修改
                blnDo = True

                '刷新显示
                If blnDo = True Then
                    If Me.showModuleData_SELRY(strErrMsg) = False Then
                        GoTo errProc
                    End If
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

normExit:
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)

            doDelete_SELRY_One = True
            Exit Function

errProc:
            objMessageProcess.doAlertMessage(Me.popMessageObject, strErrMsg)
            Xydc.Platform.web.DataGridProcess.SafeRelease(objDataGridProcess)
            Xydc.Platform.web.MessageProcess.SafeRelease(objMessageProcess)
            Exit Function

        End Function

        Private Sub grdSELRY_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdSELRY.ItemCommand

            Dim objMessageProcess As New Xydc.Platform.web.MessageProcess
            Dim strErrMsg As String

            Try
                '修改当前行
                Me.grdSELRY.SelectedIndex = e.Item.ItemIndex

                '处理
                Select Case e.CommandName.ToUpper()
                    Case "DeleteOneRow".ToUpper()
                        If Me.doDelete_SELRY_One(strErrMsg) = False Then
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