<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="main.aspx.vb" Inherits="Xydc.Platform.web.main" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register TagPrefix="uwin" Namespace="Josco.Web" Assembly="Josco.Web.PopMessage" %>
<%@ Import namespace="Xydc.Platform.Common.Utilities.PulicParameters" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>系统主界面</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		<link href="../filecss/mnuStyle01.css" type="text/css" rel="stylesheet"/>
		<LINK href="../filecss/styles01.css" type="text/css" rel="stylesheet"/>
		<script src="../scripts/transkey.js"></script>
		<script  type="text/javascript" language="vb" runat="server">
			'获取Unicode的字符串转换为MBCS字符串的字节长度
			Public Function getStringLength(ByVal strValue As String) As Integer
				Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
				getStringLength = objPulicParameters.getStringLength(strValue)
				Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
			End Function

			'从Unicode的字符串中获取指定长度的字符串，长度按MBCS计算
			Public Function getSubString(ByVal strValue As String, ByVal intLen As Integer) As String
				Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
				getSubString = objPulicParameters.getSubString(strValue, intLen)
				Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
			End Function

			'获取应用根目录HTTP路径
			Public Function getApplicationPath() As String
				getApplicationPath = Request.ApplicationPath
			End Function

			'----------------------------------------------------------------
			' 隐藏没有权限的菜单
			'----------------------------------------------------------------
			Private Sub mnuMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuMain.Load
				Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
				Dim objsystemAppManager As New Xydc.Platform.BusinessFacade.systemAppManager
				Dim objMokuaiQXData As Xydc.Platform.Common.Data.AppManagerData = Nothing
				Dim strErrMsg As String = ""
				Try
					'根据登录用户获取模块权限数据
		            If MyBase.UserId.ToUpper() = "SA" Then
		               
		                Exit Try
		            End If
					'普通用户权限
					If objsystemAppManager.getDBUserMokuaiQXData(strErrMsg, MyBase.UserId, MyBase.UserPassword, MyBase.UserId, objMokuaiQXData) = True Then
						Dim strMKMC As String = Xydc.Platform.Common.Data.AppManagerData.FIELD_GL_B_YINGYONGXITONG_MOKUAIQX_MKMC
						Dim blnVisible As Boolean = False
						Dim strParamValue As String = ""
						Dim strFilter As String = ""
						With objMokuaiQXData.Tables(Xydc.Platform.Common.Data.AppManagerData.TABLE_GL_B_YINGYONGXITONG_MOKUAIQX)
							strParamValue = "应用系统-权限管理-文件转换"
							strFilter = strMKMC + " = '" + strParamValue + "'"
							.DefaultView.RowFilter = strFilter
							If .DefaultView.Count < 1 Then
								Me.mnuMain.FindItemById("mnuXTGL_5001").Visible = False
							End If
							blnVisible = blnVisible Or Me.mnuMain.FindItemById("mnuXTGL_5001").Visible
							'*******************************************************************************
							strParamValue = "应用系统-特殊处理-工作流文件处理"
							strFilter = strMKMC + " = '" + strParamValue + "'"
							.DefaultView.RowFilter = strFilter
							If .DefaultView.Count < 1 Then
								Me.mnuMain.FindItemById("mnuXTGL_5002").Visible = False
							End If
							blnVisible = blnVisible Or Me.mnuMain.FindItemById("mnuXTGL_5002").Visible
							Me.mnuMain.FindItemById("mnuXTGL_Bar4").Visible = blnVisible
							
		                    'Me.strDisplay_dbsy.Visible = False
						End With
					End If
				Catch ex As Exception
				End Try
				Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
				Xydc.Platform.BusinessFacade.systemAppManager.SafeRelease(objsystemAppManager)
				Xydc.Platform.Common.Data.AppManagerData.SafeRelease(objMokuaiQXData)
			End Sub		
		    
		    '显示数据
		    Public Sub doDisplayData()
		        Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
		        Dim objDataSet As System.Data.DataSet = Nothing
		        Dim blnWriteNull As Boolean = False
		        Dim intMaxItemLen As Integer = 62
		        Dim intMaxItems As Integer = 7
		        Dim strSQL As String = ""
		        
		        Try
		            '获取数据
		            'strSQL = "select scraped_date,count(*) as '下载数',sum(today_sold_num_residence) as '当日成交数'  from [house].[dbo].[projects] where scraped_date between dateadd(dd,-7,getdate()) and getdate() group by scraped_date order by scraped_date "
		            strSQL = "select convert(varchar(10),[C_TIME],120) as scraped_date,count(*) as '下载数',sum(C_ZZ_YSTS_DR) as '当日成交数'  from [xszxDB].[dbo].[T_HOUSE_INFO] where C_TIME between dateadd(dd,-7,getdate()) and dateadd(dd,1,getdate()) group by convert(varchar(10),[C_TIME],120) order by convert(varchar(10),[C_TIME],120) "
		            objDataSet = getDataSet(strSQL)
		            '无法获取数据
		            If objDataSet Is Nothing Then blnWriteNull = True
		            '没有数据
		            With objDataSet.Tables(0)
		                If .Rows.Count < 1 Then blnWriteNull = True
		                intMaxItems = .Rows.Count
		            End With
		            If blnWriteNull = True Then
		                Response.Write("&nbsp;")
		                Exit Try
		            End If
		            '输出数据
		            Dim intLen As Integer = 0
		            Dim strIN As String = ""
		            Dim strRQ As String = ""
		            Dim strDW As String = ""
		            Dim strFA As String = ""
		            Dim strLX As String = ""
		            
		            Dim i As Integer = 0
		            Response.Write("<table cellpadding='0' cellspacing='0' border='1'>" + vbCr)
		            Response.Write("  <tr>" + vbCr)
		            Response.Write("    <td width='150px' height='18' class='label12_01'>" + vbCr)
		            Response.Write("      &nbsp;&nbsp;时间" + vbCr)
		            Response.Write("    </td>" + vbCr)
		            'Response.Write("    <td width='50px' height='18' class='label12_01'>" + vbCr)
		            'Response.Write("      &nbsp;&nbsp;实际数" + vbCr)
		            'Response.Write("    </td>" + vbCr)
		            Response.Write("    <td  width='50px' height='18' class='label12_01'>" + vbCr)
		            Response.Write("      &nbsp;&nbsp;下载数" + vbCr)
		            Response.Write("    </td>" + vbCr)
		            'Response.Write("    <td  width='50px' height='18' class='label12_01'>" + vbCr)
		            'Response.Write("      &nbsp;&nbsp;失败数" + vbCr)
		            'Response.Write("    </td>" + vbCr)
		            Response.Write("    <td  width='120px' height='18' class='label12_01'>" + vbCr)
		            Response.Write("      &nbsp;&nbsp;住宅当日成交套数" + vbCr)
		            Response.Write("    </td>" + vbCr)
		            Response.Write("  </tr>" + vbCr)
		            With objDataSet.Tables(0).DefaultView
		                For i = 0 To intMaxItems - 1 Step 1
		                    '没有足够数据
		                    If i >= .Count Then Exit For
		                    '获取数据
		                    strRQ = objPulicParameters.getObjectValue(.Item(i).Item("scraped_date"), "yyyy-MM-dd", "")
		                    'strIN = objPulicParameters.getObjectValue(.Item(i).Item("c_intact_count"), "")
		                    strDW = objPulicParameters.getObjectValue(.Item(i).Item("下载数"), "")
		                    'strFA = objPulicParameters.getObjectValue(.Item(i).Item("c_download_fail_count"), "")
		                    'strLX = objPulicParameters.getObjectValue(.Item(i).Item("c_table_type"), "")
		                    strLX = objPulicParameters.getObjectValue(.Item(i).Item("当日成交数"), "")
		                    
		                    '显示数据
		                    Response.Write("  <tr>" + vbCr)
		                    Response.Write("    <td width='150px' height='18' class='label12_01'>" + vbCr)
		                    Response.Write("      &nbsp;&nbsp;" + strRQ + vbCr)
		                    Response.Write("    </td>" + vbCr)
		                    'Response.Write("    <td width='50px' height='18' class='label12_01'>" + vbCr)
		                    'Response.Write("      &nbsp;&nbsp;" + strIN + vbCr)
		                    'Response.Write("    </td>" + vbCr)
		                    Response.Write("    <td  width='50px' height='18' class='label12_01'>" + vbCr)
		                    Response.Write("      &nbsp;&nbsp;" + strDW + vbCr)
		                    Response.Write("    </td>" + vbCr)
		                    'Response.Write("    <td  width='50px' height='18' class='label12_01'>" + vbCr)
		                    'Response.Write("      &nbsp;&nbsp;" + strFA + vbCr)
		                    'Response.Write("    </td>" + vbCr)
		                    Response.Write("    <td  width='100px' height='18' class='label12_01'>" + vbCr)
		                    Response.Write("      &nbsp;&nbsp;" + strLX + vbCr)
		                    Response.Write("    </td>" + vbCr)
		                    Response.Write("  </tr>" + vbCr)
		                Next
		            End With
		            Response.Write("</table>" + vbCr)
		        Catch ex As Exception
		        End Try
		        
		        Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
		    End Sub
		    
		    '获取数据集
		    Public Function getDataSet(ByVal strSQL As String) As System.Data.DataSet
		        Dim objsystemCommon As New Xydc.Platform.BusinessFacade.systemCommon
		        Dim objDataSet As System.Data.DataSet = Nothing
		        Dim strErrMsg As String = ""
		        Try
		            If objsystemCommon.getDataSetBySQL(strErrMsg, MyBase.UserId, MyBase.UserPassword, strSQL, objDataSet) = True Then
		                getDataSet = objDataSet
		            End If
		        Catch ex As Exception
		            getDataSet = Nothing
		        End Try
		        Xydc.Platform.BusinessFacade.systemCommon.SafeRelease(objsystemCommon)
		    End Function
		    
		</script>
		<script language="javascript">
            function openWindow(url) 
            {
				try 
				{
					url = encodeURI(url);
					window.open(url,"mainFrame");
				} catch (e) {}
            }
            function closeWindow() 
            {
				try 
				{
					if (window.parent)
						window.parent.close();
				} catch (e) {}
            } 
			
//            function openChat() 
//            {
//				try
//				{
//					var objLeftFrame = null;
//					objLeftFrame = getFrame(window.parent.frames, "leftFrame");
//					if (objLeftFrame)
//						objLeftFrame.window.execScript("openChat();");
//				} catch (e) {}
//            }
			
			function minLeftFrame() 
			{
				try
				{
					window.parent.doHideLeftFrame(); 
				} catch (e) {}
			}
			function document_onreadystatechange() 
			{
				minLeftFrame();
			}
			
			function doMenuItemClick(menuItemId) 
			{
				try 
				{
					document.all("htxtSelectMenuID").value = menuItemId;
					window.setTimeout("__doPostBack('lnkMenu', '');", 500);
				} catch (e) {}
			}
			
		</script>
		<script language="javascript" for="document" event="onreadystatechange">
			return document_onreadystatechange()
		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" background="../images/bgmain.gif">
		<form id="frmMAIN" method="post" runat="server">
			<asp:panel id="panelMain" Runat="server">
				<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<td><asp:LinkButton id="lnkBlank" Runat="server" Width="0px"></asp:LinkButton><asp:LinkButton id="lnkMenu" Runat="server" Width="0px"></asp:LinkButton><INPUT id="htxtSelectMenuID" type="hidden" size="1" runat="server"></td>
						<TD>
							<ComponentArt:Menu id="mnuMain" runat="server" width="100%" Orientation="Horizontal" CssClass="TopGroup"
								DefaultGroupCssClass="MenuGroup" DefaultSubGroupExpandOffsetX="-10" DefaultSubGroupExpandOffsetY="-5"
								DefaultItemLookID="DefaultItemLook" TopGroupItemSpacing="1" DefaultGroupItemSpacing="2" ImagesBaseUrl="../images/"
								EnableViewState="false" ExpandDelay="100" DefaultTarget="mainFrame">
								<ITEMS>
									<COMPONENTART:MENUITEM id="mnuXTZY" Target="mainFrame" Text="主页" DisabledLookId="MenuItemDisabledLook">
										<COMPONENTART:MENUITEM id="mnuXTZY_1001" Target="mainFrame" Text="欢迎页面" ClientSideCommand="openWindow('../areaContent.aspx');" DisabledLookId="MenuItemDisabledLook"></COMPONENTART:MENUITEM>
										<COMPONENTART:MENUITEM id="mnuXTZY_Bar1" Target="mainFrame" LookId="BreakItem" DisabledLookId="MenuItemDisabledLook"></COMPONENTART:MENUITEM>
										<COMPONENTART:MENUITEM id="mnuXTZY_2001" Target="mainFrame" Text="用户信息" ClientSideCommand="openWindow('./userinfo.aspx');" DisabledLookId="MenuItemDisabledLook"></COMPONENTART:MENUITEM>
										<COMPONENTART:MENUITEM id="mnuXTZY_2002" Target="mainFrame" Text="更改密码" ClientSideCommand="openWindow('./modifypwd.aspx');" DisabledLookId="MenuItemDisabledLook"></COMPONENTART:MENUITEM>
										<COMPONENTART:MENUITEM id="mnuXTZY_Bar2" Target="mainFrame" LookId="BreakItem" DisabledLookId="MenuItemDisabledLook"></COMPONENTART:MENUITEM>
										<COMPONENTART:MENUITEM id="mnuXTZY_3001" Target="mainFrame" Text="退出系统" ClientSideCommand="closeWindow();" DisabledLookId="MenuItemDisabledLook"></COMPONENTART:MENUITEM>
									</COMPONENTART:MENUITEM>
									<COMPONENTART:MENUITEM id="mnuTop_Bar05" LookId="BreakItemV"></COMPONENTART:MENUITEM>
									<COMPONENTART:MENUITEM id="mnuSunshine" Target="mainFrame" Text="阳光家缘数据">
										<COMPONENTART:MENUITEM id="mnuSunshine_005" Target="mainFrame" Text="房产楼盘名称匹配_x2" ClientSideCommand="doMenuItemClick('mnuSunshine_002');"></COMPONENTART:MENUITEM>
										<COMPONENTART:MENUITEM id="mnuSunshine_007" Target="mainFrame" Text="房产楼盘均价匹配" ClientSideCommand="doMenuItemClick('mnuSunshine_007');"></COMPONENTART:MENUITEM>
									    <COMPONENTART:MENUITEM id="mnuSunshine_002" Target="mainFrame" Text="房产销售信息综合查询" ClientSideCommand="openWindow('./sunshineData/compute/sunshineData_houseInfo_compute.aspx');"></COMPONENTART:MENUITEM>
									    <COMPONENTART:MENUITEM id="mnuSunshine_003" Target="mainFrame" Text="房产销售周信息查询" ClientSideCommand="openWindow('./sunshineData/compute/sunshineData_weekInfo_compute.aspx');"></COMPONENTART:MENUITEM>
									    <COMPONENTART:MENUITEM id="mnuSunshine_004" Target="mainFrame" Text="房产销售周区域统计信息" ClientSideCommand="openWindow('./sunshineData/compute/sunshineData_RegionInfo_compute.aspx');"></COMPONENTART:MENUITEM>
									    <COMPONENTART:MENUITEM id="mnuSunshine_006" Target="mainFrame" Text="房产销售N周价格、套数" ClientSideCommand="openWindow('./sunshineData/compute/sunshineData_nWeek_compute.aspx');"></COMPONENTART:MENUITEM>
									</COMPONENTART:MENUITEM>
									
									<COMPONENTART:MENUITEM id="mnuTop_Bar09" LookId="BreakItemV"></COMPONENTART:MENUITEM>
									<COMPONENTART:MENUITEM id="mnuDayData" Target="mainFrame" Text="阳光家缘日数据">
									    <COMPONENTART:MENUITEM id="mnuDayData_01" Target="mainFrame" Text="房产销售信息综合查询V2" ClientSideCommand="openWindow('./sunshineData/daycompute/sunshineData_houseInfo_compute_v2.aspx');"></COMPONENTART:MENUITEM>
									    <COMPONENTART:MENUITEM id="mnuDayData_02" Target="mainFrame" Text="房产销售周信息查询V2" ClientSideCommand="openWindow('./sunshineData/daycompute/sunshineData_weekInfo_compute_v2.aspx');"></COMPONENTART:MENUITEM>
									    <COMPONENTART:MENUITEM id="mnuDayData_03" Target="mainFrame" Text="房产销售周区域统计信息V2" ClientSideCommand="openWindow('./sunshineData/daycompute/sunshineData_RegionInfo_compute_v2.aspx');"></COMPONENTART:MENUITEM>
									    <COMPONENTART:MENUITEM id="mnuDayData_04" Target="mainFrame" Text="房产销售N周价格、套数V2" ClientSideCommand="openWindow('./sunshineData/daycompute/sunshineData_nWeek_compute_v2.aspx');"></COMPONENTART:MENUITEM>
									</COMPONENTART:MENUITEM>
									
									<COMPONENTART:MENUITEM id="mnuTop_Bar11" LookId="BreakItemV"></COMPONENTART:MENUITEM>
									<COMPONENTART:MENUITEM id="mnuDayData_001" Target="mainFrame" Text="阳光家缘日楼盘数据">
									    <COMPONENTART:MENUITEM id="mnuDayData_002" Target="mainFrame" Text="房产销售信息综合查询V3" ClientSideCommand="openWindow('./sunshineData/dayhousecompute/sunshineData_houseInfo_compute_v3.aspx');"></COMPONENTART:MENUITEM>
									    <COMPONENTART:MENUITEM id="mnuDayData_003" Target="mainFrame" Text="房产销售周信息查询V3" ClientSideCommand="openWindow('./sunshineData/dayhousecompute/sunshineData_weekInfo_compute_v3.aspx');"></COMPONENTART:MENUITEM>
									    <COMPONENTART:MENUITEM id="mnuDayData_004" Target="mainFrame" Text="房产销售周区域统计信息V3" ClientSideCommand="openWindow('./sunshineData/dayhousecompute/sunshineData_RegionInfo_compute_v3.aspx');"></COMPONENTART:MENUITEM>
									    <COMPONENTART:MENUITEM id="mnuDayData_005" Target="mainFrame" Text="房产销售N周价格、套数V3" ClientSideCommand="openWindow('./sunshineData/dayhousecompute/sunshineData_nWeek_compute_v3.aspx');"></COMPONENTART:MENUITEM>
									    <COMPONENTART:MENUITEM id="mnuDayData_006" Target="mainFrame" Text="房产销售楼盘明细查询" ClientSideCommand="openWindow('./sunshineData/dayhousecompute/sunshineData_houseDetail_compute.aspx');"></COMPONENTART:MENUITEM>
									</COMPONENTART:MENUITEM>
									    									
									<COMPONENTART:MENUITEM id="mnuTop_Bar06" LookId="BreakItemV"></COMPONENTART:MENUITEM>
									<COMPONENTART:MENUITEM id="mnuDepthData" Target="mainFrame" Text="深度销售数据">
										<COMPONENTART:MENUITEM id="mnuDepthData_1001" Target="mainFrame" Text="综合查询" ClientSideCommand="openWindow('./depthData/compute/deepData_detailCompute.aspx');"></COMPONENTART:MENUITEM>
										<COMPONENTART:MENUITEM id="mnuDepthData_1002" Target="mainFrame" Text="月度数据分析" ClientSideCommand="openWindow('./depthData/compute/deepData_monthCompute.aspx');"></COMPONENTART:MENUITEM>
									    <COMPONENTART:MENUITEM id="mnuDepthData_1003" Target="mainFrame" Text="明细查询" ClientSideCommand="openWindow('./depthData/compute/deepData_detail.aspx');"></COMPONENTART:MENUITEM>
									</COMPONENTART:MENUITEM>
									<COMPONENTART:MENUITEM id="mnuTop_Bar04" LookId="BreakItemV"></COMPONENTART:MENUITEM>
									<COMPONENTART:MENUITEM id="mnuCustomer" Target="mainFrame" Text="深度客户数据">
										<COMPONENTART:MENUITEM id="mnuCustomer_001" Target="mainFrame" Text="明细查询" ClientSideCommand="openWindow('./customer/customer_detail.aspx');"></COMPONENTART:MENUITEM>
										<COMPONENTART:MENUITEM id="mnuCustomer_002" Target="mainFrame" Text="年龄段分析" ClientSideCommand="openWindow('./customer/customer_AgeRatio.aspx');"></COMPONENTART:MENUITEM>
										<COMPONENTART:MENUITEM id="mnuCustomer_003" Target="mainFrame" Text="通信地址手工匹配" ClientSideCommand="openWindow('./customer/search_match.aspx');"></COMPONENTART:MENUITEM>
									</COMPONENTART:MENUITEM>
									<COMPONENTART:MENUITEM id="mnuCustomer_medium" Target="mainFrame" Text="二手客户数据">
										<COMPONENTART:MENUITEM id="mnuCustomer_medium_001" Target="mainFrame" Text="明细查询" ClientSideCommand="openWindow('./customer/customer_detail_medium.aspx');"></COMPONENTART:MENUITEM>										
									</COMPONENTART:MENUITEM>
									<COMPONENTART:MENUITEM id="mnuTop_Bar07" LookId="BreakItemV"></COMPONENTART:MENUITEM>
									<COMPONENTART:MENUITEM id="mnuSJPZ" Target="mainFrame" Text="数据配置">
										<COMPONENTART:MENUITEM id="mnuSJPZ_1001" Target="mainFrame" Text="建筑面积段" ClientSideCommand="doMenuItemClick('mnuSJPZ_1001');"></COMPONENTART:MENUITEM>
										<COMPONENTART:MENUITEM id="mnuSJPZ_1002" Target="mainFrame" Text="套内面积段" ClientSideCommand="doMenuItemClick('mnuSJPZ_1002');"></COMPONENTART:MENUITEM>
										<COMPONENTART:MENUITEM id="mnuSJPZ_1003" Target="mainFrame" Text="单价段" ClientSideCommand="doMenuItemClick('mnuSJPZ_1003');"></COMPONENTART:MENUITEM>
										<COMPONENTART:MENUITEM id="mnuSJPZ_1004" Target="mainFrame" Text="总额段" ClientSideCommand="doMenuItemClick('mnuSJPZ_1004');"></COMPONENTART:MENUITEM>
									    <COMPONENTART:MENUITEM id="mnuSJPZ_Bra1" Target="mainFrame" LookId="BreakItem"></COMPONENTART:MENUITEM>
									    <COMPONENTART:MENUITEM id="mnuSJPZ_1005" Target="mainFrame" Text="周楼盘匹配" ClientSideCommand="doMenuItemClick('mnuSJPZ_1005');"></COMPONENTART:MENUITEM>
									    <COMPONENTART:MENUITEM id="mnuSJPZ_1006" Target="mainFrame" Text="月楼盘匹配" ClientSideCommand="doMenuItemClick('mnuSJPZ_1006');"></COMPONENTART:MENUITEM>
									    <COMPONENTART:MENUITEM id="mnuSJPZ_1007" Target="mainFrame" Text="楼盘排序" ClientSideCommand="doMenuItemClick('mnuSJPZ_1007');"></COMPONENTART:MENUITEM>
									</COMPONENTART:MENUITEM>
									<COMPONENTART:MENUITEM id="mnuTop_Bar08" LookId="BreakItemV"></COMPONENTART:MENUITEM>
									<COMPONENTART:MENUITEM id="mnuXTPZ" Target="mainFrame" Text="系统配置" DisabledLookId="MenuItemDisabledLook">
										<COMPONENTART:MENUITEM id="mnuXTPZ_1001" Target="mainFrame" Text="运行参数" ClientSideCommand="openWindow('./xtpz/xtpz_xtcs.aspx');" DisabledLookId="MenuItemDisabledLook"></COMPONENTART:MENUITEM>
										<COMPONENTART:MENUITEM id="mnuXTPZ_Bar1" Target="mainFrame" LookId="BreakItem"></COMPONENTART:MENUITEM>
										<COMPONENTART:MENUITEM id="mnuXTPZ_2001" Target="mainFrame" Text="单位人员" ClientSideCommand="doMenuItemClick('mnuXTPZ_2001');" DisabledLookId="MenuItemDisabledLook"></COMPONENTART:MENUITEM>
										<COMPONENTART:MENUITEM id="mnuXTPZ_2002" Target="mainFrame" Text="常用范围" ClientSideCommand="openWindow('./gwdm/gwdm_cyfw.aspx');" DisabledLookId="MenuItemDisabledLook"></COMPONENTART:MENUITEM>
									</COMPONENTART:MENUITEM>
									<COMPONENTART:MENUITEM id="mnuXTGL" Target="mainFrame" Text="系统管理" DisabledLookId="MenuItemDisabledLook">
										<COMPONENTART:MENUITEM id="mnuXTGL_1001" Target="mainFrame" Text="数据对象" ClientSideCommand="openWindow('./xtgl/xtgl_sjdx.aspx');" DisabledLookId="MenuItemDisabledLook"></COMPONENTART:MENUITEM>
										<COMPONENTART:MENUITEM id="mnuXTGL_Bar1" Target="mainFrame" LookId="BreakItem"></COMPONENTART:MENUITEM>
										<COMPONENTART:MENUITEM id="mnuXTGL_2001" Target="mainFrame" Text="用户管理" ClientSideCommand="openWindow('./xtgl/xtgl_yhgl_yh.aspx');" DisabledLookId="MenuItemDisabledLook"></COMPONENTART:MENUITEM>
										<COMPONENTART:MENUITEM id="mnuXTGL_2002" Target="mainFrame" Text="数据授权" ClientSideCommand="openWindow('./xtgl/xtgl_sjqx_js.aspx');" DisabledLookId="MenuItemDisabledLook"></COMPONENTART:MENUITEM>
										<COMPONENTART:MENUITEM id="mnuXTGL_2003" Target="mainFrame" Text="模块管理" ClientSideCommand="openWindow('./xtgl/xtgl_mkgl.aspx');" DisabledLookId="MenuItemDisabledLook"></COMPONENTART:MENUITEM>
										<COMPONENTART:MENUITEM id="mnuXTGL_2004" Target="mainFrame" Text="模块授权" ClientSideCommand="openWindow('./xtgl/xtgl_mkqx_js.aspx');" DisabledLookId="MenuItemDisabledLook"></COMPONENTART:MENUITEM>
										<COMPONENTART:MENUITEM id="mnuXTGL_Bar2" Target="mainFrame" LookId="BreakItem"></COMPONENTART:MENUITEM>
										<COMPONENTART:MENUITEM id="mnuXTGL_3001" Target="mainFrame" Text="在线用户" ClientSideCommand="openWindow('./xtgl/xtgl_rzgl_zxyh.aspx');" DisabledLookId="MenuItemDisabledLook"></COMPONENTART:MENUITEM>
										<COMPONENTART:MENUITEM id="mnuXTGL_3002" Target="mainFrame" Text="进出日志" ClientSideCommand="openWindow('./xtgl/xtgl_rzgl_jcrz.aspx');" DisabledLookId="MenuItemDisabledLook"></COMPONENTART:MENUITEM>
										<COMPONENTART:MENUITEM id="mnuXTGL_Bar3" Target="mainFrame" LookId="BreakItem"></COMPONENTART:MENUITEM>
										<COMPONENTART:MENUITEM id="mnuXTGL_4001" Target="mainFrame" Text="用户日志" ClientSideCommand="openWindow('./xtgl/xtgl_rz_cz.aspx');" DisabledLookId="MenuItemDisabledLook"></COMPONENTART:MENUITEM>
										<COMPONENTART:MENUITEM id="mnuXTGL_4002" Target="mainFrame" Text="访问审计" ClientSideCommand="openWindow('./xtgl/xtgl_rz_fw.aspx');" DisabledLookId="MenuItemDisabledLook"></COMPONENTART:MENUITEM>
										<COMPONENTART:MENUITEM id="mnuXTGL_4003" Target="mainFrame" Text="安全审计" ClientSideCommand="openWindow('./xtgl/xtgl_rz_aq.aspx');" DisabledLookId="MenuItemDisabledLook"></COMPONENTART:MENUITEM>
										<COMPONENTART:MENUITEM id="mnuXTGL_4004" Target="mainFrame" Text="配置审计" ClientSideCommand="openWindow('./xtgl/xtgl_rz_pz.aspx');" DisabledLookId="MenuItemDisabledLook"></COMPONENTART:MENUITEM>
										<COMPONENTART:MENUITEM id="mnuXTGL_4005" Target="mainFrame" Text="审计日志" ClientSideCommand="openWindow('./xtgl/xtgl_rz_sj.aspx');" DisabledLookId="MenuItemDisabledLook"></COMPONENTART:MENUITEM>
										<COMPONENTART:MENUITEM id="mnuXTGL_Bar4" Target="mainFrame" LookId="BreakItem"></COMPONENTART:MENUITEM>
										<COMPONENTART:MENUITEM id="mnuXTGL_5001" Target="mainFrame" Text="文件转换" ClientSideCommand="openWindow('./xtgl/xtgl_wjzh.aspx');" DisabledLookId="MenuItemDisabledLook"></COMPONENTART:MENUITEM>
										<COMPONENTART:MENUITEM id="mnuXTGL_5002" Target="mainFrame" Text="强制编辑" ClientSideCommand="openWindow('./gzflow/gzsp_admin_bz.aspx');" DisabledLookId="MenuItemDisabledLook"></COMPONENTART:MENUITEM>
									</COMPONENTART:MENUITEM>
									
									
								</ITEMS>
								<ITEMLOOKS>
									<COMPONENTART:ItemLook LookID="TopItemLook" CssClass="TopMenuItem" HoverCssClass="TopMenuItemHover" LabelPaddingLeft="15" LabelPaddingRight="15" LabelPaddingTop="4" LabelPaddingBottom="4" />
									<COMPONENTART:ItemLook LookID="DefaultItemLook" CssClass="MenuItem" HoverCssClass="MenuItemHover" ExpandedCssClass="MenuItemHover" LabelPaddingLeft="18" LabelPaddingRight="12" LabelPaddingTop="4" LabelPaddingBottom="4" />
									<COMPONENTART:ItemLook LookID="MenuItemDisabledLook" CssClass="MenuItemDisabled" HoverCssClass="" ExpandedCssClass="" LabelPaddingLeft="18" LabelPaddingRight="12" LabelPaddingTop="4" LabelPaddingBottom="4" />
									<COMPONENTART:ItemLook LookID="BreakItem" CssClass="MenuBreak" ImageHeight="2" ImageWidth="100%" ImageUrl="../images/menu01/break.gif" />
								</ITEMLOOKS>
							</ComponentArt:Menu>
						</TD>
					</TR>
					<TR>
					  
						<TD align="center" colspan="2"  ><div id="strDisplay_dbsy" runat="server">
						    <table cellpadding="0" cellspacing="0" border="0">						      
						        <tr>	
						            <td valign=top>
					                    <table cellpadding="0" cellspacing="0" border="0">					        
					                          <tr>
									            <td colspan="3"><img src="../images/welcome02/gzgg_top.jpg" border="0" alt="公告栏"></td>
								            </tr>
						                    <tr>
									            <td width="10"><img src="../images/welcome02/gzgg_left.jpg" border="0" width="10" height="248"></td>
									            <td valign="top" align="left" bgcolor="white">
            										<marquee id="syslogoId" width="215" height="248" onmouseover="syslogoId.stop();" onmouseout="syslogoId.start();"
																	scrollAmount="1" scrollDelay="15" direction="up" behavior="scroll" loop="0">
																	&nbsp;&nbsp;&nbsp;&nbsp;阳光家缘第二版9月27日正式上线！<br />
																	&nbsp;&nbsp;&nbsp;&nbsp;1、别墅和洋房可以分开了！<br />
																	&nbsp;&nbsp;&nbsp;&nbsp;2、新版匹配，支持预售证匹配！<br />
																	&nbsp;&nbsp;&nbsp;&nbsp;3、区域统计、10周套数和价格变化即将上线！<br />																	
																	&nbsp;&nbsp;&nbsp;&nbsp;10.08更新！<br />
																	&nbsp;&nbsp;&nbsp;&nbsp;1、阳光家缘综合查询，同时支持月度和周度楼盘匹配！<br />
																	&nbsp;&nbsp;&nbsp;&nbsp;2、区域统计增加了成交金额和成交面积！<br />
																	&nbsp;&nbsp;&nbsp;&nbsp;3、修改了区域统计不能导出的问题！<br />
																	&nbsp;&nbsp;&nbsp;&nbsp;10.15 更新！<br />
																	&nbsp;&nbsp;&nbsp;&nbsp;1、阳光家缘添加了新模块！！N周价格、套数统计模块！<br />
																	&nbsp;&nbsp;&nbsp;&nbsp;2、修改了周报表，导出时没有项目类型字段！<br />
																	&nbsp;&nbsp;&nbsp;&nbsp;3、所有的查询界面，返回按钮重新定位在右上角！<br />
																	&nbsp;&nbsp;&nbsp;&nbsp;<B>2013.06.25 更新！<br />
																	&nbsp;&nbsp;&nbsp;&nbsp;1、明细表数据导出，成交日期按年月日拆分！<br />
																	&nbsp;&nbsp;&nbsp;&nbsp;2、月度数据分析，各选择条件，支持组合查询！<br />
																	&nbsp;&nbsp;&nbsp;&nbsp;3、周数据查询测试版！<br />
																</marquee>
									            </td>
									            <td align="left"><img src="../images/welcome02/gzgg_right.jpg" border="0" height="248" width="10"></td>
								            </tr>
								            <tr>
									            <td colspan="3"><img src="../images/welcome02/gzgg_bottom.jpg" border="0" alt="更多公告…" usemap="#DzggMap"></td>
								            </tr>
					                    </table>
					                </td>	
					                <td>
					                    <table cellpadding="0" cellspacing="0" border="0">
					                        <tr>
					                            <td style="height:14px"><%doDisplayData()%></td>		
					                        </tr>					                        
					                    </table>
					                </td>							
															
								</tr>
						    </table></div>
						</TD>
					</TR>
				</TABLE>
			</asp:panel>
			<asp:Panel id="panelError" Runat="server" Visible="False">
				<TABLE height="98%" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD width="5%"></TD>
						<TD>
							<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD>&nbsp;&nbsp;&nbsp;&nbsp;</TD>
									<TD style="FONT-SIZE: 32pt; COLOR: black; LINE-HEIGHT: 40pt; FONT-FAMILY: 宋体; LETTER-SPACING: 2pt" align="center"><asp:Label id="lblMessage" Runat="server"></asp:Label><P>&nbsp;&nbsp;</P><P><INPUT id="btnGoBack" style="FONT-SIZE: 24pt; FONT-FAMILY: 宋体" onclick="javascript:history.back();" type="button" value=" 返回 "></P></TD>
									<TD>&nbsp;&nbsp;&nbsp;&nbsp;</TD>
								</TR>
							</TABLE>
						</TD>
						<TD width="5%"></TD>
					</TR>
				</TABLE>
			</asp:Panel>
			<table cellSpacing="0" cellPadding="0" align="center" border="0">
				<tr>
					<td>
						<uwin:popmessage id="popMessageObject" runat="server" height="48px" width="96px" Visible="False" ActionType="OpenWindow" PopupWindowType="Normal" EnableViewState="False"></uwin:popmessage>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>