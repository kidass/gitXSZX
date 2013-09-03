<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="xtgl_sjqx_cq.aspx.vb" Inherits="Xydc.Platform.web.xtgl_sjqx_cq" %>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls" %>
<%@ Register TagPrefix="uwin" Namespace="Josco.Web" Assembly="Josco.Web.PopMessage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>应用系统数据权限管理窗(用户存取授权)</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../filecss/styles01.css" type="text/css" rel="stylesheet">
		<style>
			TD.grdYRLocked { ; LEFT: expression(divYR.scrollLeft); POSITION: relative }
			TH.grdYRLocked { ; LEFT: expression(divYR.scrollLeft); POSITION: relative }
			TH.grdYRLocked { Z-INDEX: 99 }
			TD.grdWRLocked { ; LEFT: expression(divWR.scrollLeft); POSITION: relative }
			TH.grdWRLocked { ; LEFT: expression(divWR.scrollLeft); POSITION: relative }
			TH.grdWRLocked { Z-INDEX: 99 }
			TH { Z-INDEX: 10; POSITION: relative }
		</style>
		<script src="../../scripts/transkey.js"></script>
		<script language="javascript">
			function window_onresize() 
			{
				var dblHeight = 0;
				var dblWidth  = 0;
				var strHeight = "";
				var strWidth  = "";
				var dblDeltaY = 20;
				var dblDeltaX = 20;
				
				if (document.all("divYR") == null)
					return;
				
				strHeight = divYR.style.height;
				dblWidth  = 570 + dblDeltaX + document.body.clientWidth  - 850; //default state : 570px
				strWidth = parseInt(dblWidth.toString(), 10).toString() + "px";
				divYR.style.width  = strWidth;
				divYR.style.height = strHeight;
				divYR.style.clip = "rect(0px " + strWidth + " " + strHeight + " 0px)";

				strHeight = divWR.style.height;
				dblWidth  = 570 + dblDeltaX + document.body.clientWidth  - 850; //default state : 570px
				strWidth = parseInt(dblWidth.toString(), 10).toString() + "px";
				divWR.style.width  = strWidth;
				divWR.style.height = strHeight;
				divWR.style.clip = "rect(0px " + strWidth + " " + strHeight + " 0px)";
			}
			function document_onreadystatechange() 
			{
				window_onresize();
			}
		</script>
		<script language="javascript" for="document" event="onreadystatechange">
		<!--
			return document_onreadystatechange()
		//-->
		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" onresize="return window_onresize()" background="../../images/oabk.gif">
		<form id="frmDATAQXGLCQ" method="post" runat="server">
			<asp:panel id="panelMain" Runat="server">
				<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD colSpan="3" height="5"></TD>
					</TR>
					<TR>
						<TD width="5"></TD>
						<TD align="center" style="BORDER-BOTTOM: #99cccc 2px solid">
							<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR vAlign="middle" align="left" height="24">
									<TD vAlign="middle" align="center" width="100"><asp:linkbutton id="lnkMLRoleQX" runat="server" Font-Name="宋体" Font-Size="12px"><img src="../../images/users.ico" alt="open" border="0" width="16" height="16">角色授权</asp:linkbutton></TD>
									<TD vAlign="middle" align="center" width="100"><asp:linkbutton id="lnkMLUserQX" runat="server" Font-Name="宋体" Font-Size="12px"><img src="../../images/user.ico" alt="open" border="0" width="16" height="16">用户授权</asp:linkbutton></TD>
									<TD vAlign="middle" align="center" width="100"><asp:linkbutton id="lnkMLAccsQX" runat="server" Font-Name="宋体" Font-Size="12px" Enabled="False"><img src="../../images/access.ico" alt="open" border="0" width="16" height="16">存取授权</asp:linkbutton></TD>
									<TD vAlign="middle" align="center" width="100"><asp:linkbutton id="lnkMLGrant" runat="server" Font-Name="宋体" Font-Size="12px"><img src="../../images/grant.ico" alt="open" border="0" width="16" height="16">赋予权限</asp:linkbutton></TD>
									<TD vAlign="middle" align="center" width="100"><asp:linkbutton id="lnkMLRevoke" runat="server" Font-Name="宋体" Font-Size="12px"><img src="../../images/revoke.ico" alt="open" border="0" width="16" height="16">回收权限</asp:linkbutton></TD>
									<TD vAlign="middle" align="center" width="100"><asp:linkbutton id="lnkMLClose" runat="server" Font-Size="12px" Font-Name="宋体"><img src="../../images/CLOSE.GIF" alt="返回上级" border="0" width="16" height="16">返回上级</asp:linkbutton></TD>
									<TD vAlign="middle" align="center" width="100"></TD>
									<TD vAlign="middle" align="center" width="100"></TD>
								</TR>
							</TABLE>
						</TD>
						<TD width="5"></TD>
					</TR>
					<TR>
						<TD width="5"></TD>
						<TD vAlign="top" align="center">
							<TABLE cellSpacing="0" cellPadding="0" border="0">
								<TR>
									<TD class="tips" align="left" colSpan="5"><asp:LinkButton id="lnkBlank" Runat="server" Height="5px" Width="0px"></asp:LinkButton></TD>
								</TR>
								<TR>
									<TD width="5"></TD>
									<TD vAlign="top" align="left" width="220" style="BORDER-RIGHT: #99cccc 1px solid; BORDER-TOP: #99cccc 1px solid; BORDER-LEFT: #99cccc 1px solid; BORDER-BOTTOM: #99cccc 1px solid">
										<TABLE cellSpacing="0" cellPadding="0" border="0">
											<TR>
												<TD><IEWC:TREEVIEW id="tvwServers" runat="server" Font-Name="宋体" Font-Size="12px" Height="700px" Width="220px" AutoPostBack="True" CssClass="label"></IEWC:TREEVIEW></TD>
											</TR>
										</TABLE>
									</TD>
									<TD width="5"></TD>
									<TD style="BORDER-RIGHT: #99cccc 1px solid; BORDER-TOP: #99cccc 1px solid; BORDER-LEFT: #99cccc 1px solid; BORDER-BOTTOM: #99cccc 1px solid" vAlign="top">
										<TABLE cellSpacing="0" cellPadding="0" border="0">
											<TR align="center">
												<TD class="label" align="left">
													<TABLE cellSpacing="0" cellPadding="0" border="0">
														<TR>
															<TD class="label" vAlign="middle" align="right">标识&nbsp;</TD>
															<TD class="label" align="left"><asp:textbox id="txtYRSearchRYDM" runat="server" Font-Size="12px" CssClass="textbox" Columns="10" height="22px" Font-Names="宋体"></asp:textbox></TD>
															<TD class="label" vAlign="middle" align="right">&nbsp;&nbsp;名称&nbsp;</TD>
															<TD class="label" align="left"><asp:textbox id="txtYRSearchRYMC" runat="server" Font-Size="12px" CssClass="textbox" Columns="12" height="22px" Font-Names="宋体"></asp:textbox></TD>
															<TD class="label" vAlign="middle" align="right">&nbsp;&nbsp;单位&nbsp;</TD>
															<TD class="label" align="left"><asp:textbox id="txtYRSearchZZMC" runat="server" Font-Size="12px" CssClass="textbox" Columns="20" height="22px" Font-Names="宋体"></asp:textbox></TD>
															<TD class="label" vAlign="middle" align="right">&nbsp;&nbsp;职务&nbsp;</TD>
															<TD class="label" align="left"><asp:textbox id="txtYRSearchGWLB" runat="server" Font-Size="12px" CssClass="textbox" Columns="20" height="22px" Font-Names="宋体"></asp:textbox></TD>
															<TD class="label" vAlign="middle" align="right">&nbsp;&nbsp;级别&nbsp;</TD>
															<TD class="label" align="left"><asp:textbox id="txtYRSearchJBMC" runat="server" Font-Size="12px" CssClass="textbox" Columns="12" height="22px" Font-Names="宋体"></asp:textbox></TD>
															<TD class="label">&nbsp;&nbsp;<asp:button id="btnYRSearch" Runat="server" Font-Name="宋体" Font-Size="12px" CssClass="button" Text="搜索"></asp:button></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
											<TR>
												<TD>
													<DIV id="divYR" style="TABLE-LAYOUT: fixed; OVERFLOW: auto; WIDTH: 570px; CLIP: rect(0px 570px 300px 0px); HEIGHT: 300px">
														<asp:datagrid id="grdYR" runat="server" Font-Size="12px" Width="1450px" CssClass="label" Font-Names="宋体"
															UseAccessibleHeader="True" AutoGenerateColumns="False" GridLines="Vertical" BackColor="White"
															BorderStyle="None" CellPadding="4" AllowPaging="True" PageSize="30" BorderColor="#DEDFDE"
															BorderWidth="0px" AllowSorting="True">
															<SelectedItemStyle Font-Size="12px" Font-Names="宋体" Font-Bold="False" VerticalAlign="Middle" ForeColor="#CC0000" BackColor="#FFFFDD"></SelectedItemStyle>
															<EditItemStyle Font-Size="12px" Font-Names="宋体" VerticalAlign="Middle" BackColor="#FFCC00"></EditItemStyle>
															<AlternatingItemStyle Font-Size="12px" Font-Names="宋体" BorderWidth="0px" BorderStyle="Solid" BorderColor="Gold" VerticalAlign="Middle" BackColor="White"></AlternatingItemStyle>
															<ItemStyle Font-Size="12px" Font-Names="宋体" BorderWidth="0px" BorderStyle="Solid" BorderColor="Gold" VerticalAlign="Middle" BackColor="#F7F7F7" ForeColor="Black"></ItemStyle>
															<HeaderStyle Font-Size="12px" Font-Names="宋体" Font-Bold="True" ForeColor="White" VerticalAlign="Middle" BackColor="#87cefa" HorizontalAlign="Left"></HeaderStyle>
															<FooterStyle BackColor="#CCCC99"></FooterStyle>
															<Columns>
																<asp:TemplateColumn HeaderText="选">
																	<HeaderStyle HorizontalAlign="Center" Width="20px"></HeaderStyle>
																	<ItemStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
																	<ItemTemplate>
																		<asp:CheckBox id="chkYR" runat="server" AutoPostBack="False"></asp:CheckBox>
																	</ItemTemplate>
																</asp:TemplateColumn>
																<asp:ButtonColumn DataTextField="人员代码" SortExpression="人员代码" HeaderText="标识" CommandName="Select">
																	<HeaderStyle Width="100px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn DataTextField="人员名称" SortExpression="人员名称" HeaderText="名称" CommandName="Select">
																	<HeaderStyle Width="100px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn DataTextField="组织名称" SortExpression="组织名称" HeaderText="部门" CommandName="Select">
																	<HeaderStyle Width="200px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn DataTextField="人员序号" SortExpression="人员序号" HeaderText="序号" CommandName="Select">
																	<HeaderStyle Width="80px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn DataTextField="岗位列表" SortExpression="岗位列表" HeaderText="职务" CommandName="Select">
																	<HeaderStyle Width="180px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn DataTextField="级别名称" SortExpression="级别名称" HeaderText="级别" CommandName="Select">
																	<HeaderStyle Width="100px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn DataTextField="秘书名称" SortExpression="秘书名称" HeaderText="秘书" CommandName="Select">
																	<HeaderStyle Width="100px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn DataTextField="联系电话" SortExpression="联系电话" HeaderText="联系电话" CommandName="Select">
																	<HeaderStyle Width="120px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn DataTextField="手机号码" SortExpression="手机号码" HeaderText="移动电话" CommandName="Select">
																	<HeaderStyle Width="120px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn DataTextField="FTP地址" SortExpression="FTP地址" HeaderText="内部邮箱" CommandName="Select">
																	<HeaderStyle Width="160px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn DataTextField="邮箱地址" SortExpression="邮箱地址" HeaderText="因特网邮箱" CommandName="Select">
																	<HeaderStyle Width="160px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn Visible="False" DataTextField="是否申请" SortExpression="是否申请" HeaderText="申请?" CommandName="Select">
																	<HeaderStyle Width="0px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn Visible="False" DataTextField="组织代码" SortExpression="组织代码" HeaderText="组织代码" CommandName="Select">
																	<HeaderStyle Width="0px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn Visible="False" DataTextField="级别代码" SortExpression="级别代码" HeaderText="级别代码" CommandName="Select">
																	<HeaderStyle Width="0px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn Visible="False" DataTextField="行政级别" SortExpression="行政级别" HeaderText="行政级别" CommandName="Select">
																	<HeaderStyle Width="0px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn Visible="False" DataTextField="秘书代码" SortExpression="秘书代码" HeaderText="秘书代码" CommandName="Select">
																	<HeaderStyle Width="0px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn Visible="False" DataTextField="自动签收" SortExpression="自动签收" HeaderText="自动签收" CommandName="Select">
																	<HeaderStyle Width="0px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn Visible="False" DataTextField="交接显示名称" SortExpression="交接显示名称" HeaderText="交接显示名称"
																	CommandName="Select">
																	<HeaderStyle Width="0px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn Visible="False" DataTextField="可查看姓名" SortExpression="可查看姓名" HeaderText="可查看姓名"
																	CommandName="Select">
																	<HeaderStyle Width="0px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn Visible="False" DataTextField="可直送人员" SortExpression="可直送人员" HeaderText="可直送人员"
																	CommandName="Select">
																	<HeaderStyle Width="0px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn Visible="False" DataTextField="其他由转送" SortExpression="其他由转送" HeaderText="其他由转送"
																	CommandName="Select">
																	<HeaderStyle Width="0px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn Visible="False" DataTextField="是否加密" SortExpression="是否加密" HeaderText="是否加密" CommandName="Select">
																	<HeaderStyle Width="0px"></HeaderStyle>
																</asp:ButtonColumn>
															</Columns>
															<PagerStyle Visible="False" NextPageText="下页" Font-Size="12px" Font-Names="宋体" PrevPageText="上页" HorizontalAlign="Right" ForeColor="Black" Position="TopAndBottom" BackColor="SkyBlue"></PagerStyle>
														</asp:datagrid><INPUT id="htxtYRFixed" type="hidden" value="0" runat="server"></DIV>
												</TD>
											</TR>
											<TR align="center">
												<TD class="label">
													<TABLE cellSpacing="0" cellPadding="0" border="0" width="100%">
														<TR>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZYRDeSelectAll" runat="server" CssClass="labelBlack">不选</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZYRSelectAll" runat="server" CssClass="labelBlack">全选</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZYRMoveFirst" runat="server" CssClass="labelBlack">最前</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZYRMovePrev" runat="server" CssClass="labelBlack">前页</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZYRMoveNext" runat="server" CssClass="labelBlack">下页</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZYRMoveLast" runat="server" CssClass="labelBlack">最后</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZYRGotoPage" runat="server" CssClass="labelBlack">前往</asp:linkbutton><asp:textbox id="txtYRPageIndex" runat="server" Font-Name="宋体" Font-Size="12px" Height="22px" Width="40px" CssClass="textbox" Columns="2">1</asp:textbox>页</TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZYRSetPageSize" runat="server" CssClass="labelBlack">每页</asp:linkbutton><asp:textbox id="txtYRPageSize" runat="server" Font-Name="宋体" Font-Size="12px" Height="22px" Width="40px" CssClass="textbox" Columns="3">30</asp:textbox>条</TD>
															<TD class="labelBlack" vAlign="middle" align="right" width="140"><asp:label id="lblYRGridLocInfo" runat="server" CssClass="labelBlack">1/10 N/15</asp:label></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
											<TR align="center">
												<TD class="label" align="left">
													<TABLE cellSpacing="0" cellPadding="0" border="0">
														<TR>
															<TD class="label" vAlign="middle" align="right">标识&nbsp;</TD>
															<TD class="label" align="left"><asp:textbox id="txtWRSearchRYDM" runat="server" Font-Size="12px" CssClass="textbox" Columns="10" height="22px" Font-Names="宋体"></asp:textbox></TD>
															<TD class="label" vAlign="middle" align="right">&nbsp;&nbsp;名称&nbsp;</TD>
															<TD class="label" align="left"><asp:textbox id="txtWRSearchRYMC" runat="server" Font-Size="12px" CssClass="textbox" Columns="12" height="22px" Font-Names="宋体"></asp:textbox></TD>
															<TD class="label" vAlign="middle" align="right">&nbsp;&nbsp;单位&nbsp;</TD>
															<TD class="label" align="left"><asp:textbox id="txtWRSearchZZMC" runat="server" Font-Size="12px" CssClass="textbox" Columns="20" height="22px" Font-Names="宋体"></asp:textbox></TD>
															<TD class="label" vAlign="middle" align="right">&nbsp;&nbsp;职务&nbsp;</TD>
															<TD class="label" align="left"><asp:textbox id="txtWRSearchGWLB" runat="server" Font-Size="12px" CssClass="textbox" Columns="20" height="22px" Font-Names="宋体"></asp:textbox></TD>
															<TD class="label" vAlign="middle" align="right">&nbsp;&nbsp;级别&nbsp;</TD>
															<TD class="label" align="left"><asp:textbox id="txtWRSearchJBMC" runat="server" Font-Size="12px" CssClass="textbox" Columns="12" height="22px" Font-Names="宋体"></asp:textbox></TD>
															<TD class="label">&nbsp;&nbsp;<asp:button id="btnWRSearch" Runat="server" Font-Name="宋体" Font-Size="12px" CssClass="button" Text="搜索"></asp:button></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
											<TR>
												<TD>
													<DIV id="divWR" style="TABLE-LAYOUT: fixed; OVERFLOW: auto; WIDTH: 570px; CLIP: rect(0px 570px 300px 0px); HEIGHT: 300px">
														<asp:datagrid id="grdWR" runat="server" Font-Size="12px" Width="1450px" CssClass="label" Font-Names="宋体"
															UseAccessibleHeader="True" AutoGenerateColumns="False" GridLines="Vertical" BackColor="White"
															BorderStyle="None" CellPadding="4" AllowPaging="True" PageSize="30" BorderColor="#DEDFDE"
															BorderWidth="0px" AllowSorting="True">
															<SelectedItemStyle Font-Size="12px" Font-Names="宋体" Font-Bold="False" VerticalAlign="Middle" ForeColor="#CC0000" BackColor="#FFFFDD"></SelectedItemStyle>
															<EditItemStyle Font-Size="12px" Font-Names="宋体" VerticalAlign="Middle" BackColor="#FFCC00"></EditItemStyle>
															<AlternatingItemStyle Font-Size="12px" Font-Names="宋体" BorderWidth="0px" BorderStyle="Solid" BorderColor="Gold" VerticalAlign="Middle" BackColor="White"></AlternatingItemStyle>
															<ItemStyle Font-Size="12px" Font-Names="宋体" BorderWidth="0px" BorderStyle="Solid" BorderColor="Gold" VerticalAlign="Middle" BackColor="#F7F7F7" ForeColor="Black"></ItemStyle>
															<HeaderStyle Font-Size="12px" Font-Names="宋体" Font-Bold="True" ForeColor="White" VerticalAlign="Middle" BackColor="#87cefa" HorizontalAlign="Left"></HeaderStyle>
															<FooterStyle BackColor="#CCCC99"></FooterStyle>
															<Columns>
																<asp:TemplateColumn HeaderText="选">
																	<HeaderStyle HorizontalAlign="Center" Width="20px"></HeaderStyle>
																	<ItemStyle Wrap="False" HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
																	<ItemTemplate>
																		<asp:CheckBox id="chkWR" runat="server" AutoPostBack="False"></asp:CheckBox>
																	</ItemTemplate>
																</asp:TemplateColumn>
																<asp:ButtonColumn DataTextField="人员代码" SortExpression="人员代码" HeaderText="标识" CommandName="Select">
																	<HeaderStyle Width="100px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn DataTextField="人员名称" SortExpression="人员名称" HeaderText="名称" CommandName="Select">
																	<HeaderStyle Width="100px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn DataTextField="组织名称" SortExpression="组织名称" HeaderText="部门" CommandName="Select">
																	<HeaderStyle Width="200px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn DataTextField="人员序号" SortExpression="人员序号" HeaderText="序号" CommandName="Select">
																	<HeaderStyle Width="80px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn DataTextField="岗位列表" SortExpression="岗位列表" HeaderText="职务" CommandName="Select">
																	<HeaderStyle Width="180px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn DataTextField="级别名称" SortExpression="级别名称" HeaderText="级别" CommandName="Select">
																	<HeaderStyle Width="100px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn DataTextField="秘书名称" SortExpression="秘书名称" HeaderText="秘书" CommandName="Select">
																	<HeaderStyle Width="100px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn DataTextField="联系电话" SortExpression="联系电话" HeaderText="联系电话" CommandName="Select">
																	<HeaderStyle Width="120px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn DataTextField="手机号码" SortExpression="手机号码" HeaderText="移动电话" CommandName="Select">
																	<HeaderStyle Width="120px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn DataTextField="FTP地址" SortExpression="FTP地址" HeaderText="内部邮箱" CommandName="Select">
																	<HeaderStyle Width="160px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn DataTextField="邮箱地址" SortExpression="邮箱地址" HeaderText="因特网邮箱" CommandName="Select">
																	<HeaderStyle Width="160px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn Visible="False" DataTextField="是否申请" SortExpression="是否申请" HeaderText="申请?" CommandName="Select">
																	<HeaderStyle Width="0px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn Visible="False" DataTextField="组织代码" SortExpression="组织代码" HeaderText="组织代码" CommandName="Select">
																	<HeaderStyle Width="0px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn Visible="False" DataTextField="级别代码" SortExpression="级别代码" HeaderText="级别代码" CommandName="Select">
																	<HeaderStyle Width="0px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn Visible="False" DataTextField="行政级别" SortExpression="行政级别" HeaderText="行政级别" CommandName="Select">
																	<HeaderStyle Width="0px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn Visible="False" DataTextField="秘书代码" SortExpression="秘书代码" HeaderText="秘书代码" CommandName="Select">
																	<HeaderStyle Width="0px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn Visible="False" DataTextField="自动签收" SortExpression="自动签收" HeaderText="自动签收" CommandName="Select">
																	<HeaderStyle Width="0px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn Visible="False" DataTextField="交接显示名称" SortExpression="交接显示名称" HeaderText="交接显示名称" CommandName="Select">
																	<HeaderStyle Width="0px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn Visible="False" DataTextField="可查看姓名" SortExpression="可查看姓名" HeaderText="可查看姓名" CommandName="Select">
																	<HeaderStyle Width="0px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn Visible="False" DataTextField="可直送人员" SortExpression="可直送人员" HeaderText="可直送人员" CommandName="Select">
																	<HeaderStyle Width="0px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn Visible="False" DataTextField="其他由转送" SortExpression="其他由转送" HeaderText="其他由转送" CommandName="Select">
																	<HeaderStyle Width="0px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn Visible="False" DataTextField="是否加密" SortExpression="是否加密" HeaderText="是否加密" CommandName="Select">
																	<HeaderStyle Width="0px"></HeaderStyle>
																</asp:ButtonColumn>
															</Columns>
															<PagerStyle Visible="False" NextPageText="下页" Font-Size="12px" Font-Names="宋体" PrevPageText="上页" HorizontalAlign="Right" ForeColor="Black" Position="TopAndBottom" BackColor="SkyBlue"></PagerStyle>
														</asp:datagrid><INPUT id="htxtWRFixed" type="hidden" value="0" runat="server"></DIV>
												</TD>
											</TR>
											<TR align="center">
												<TD class="label" style="">
													<TABLE cellSpacing="0" cellPadding="0" border="0" width="100%">
														<TR>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZWRDeSelectAll" runat="server" CssClass="labelBlack">不选</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZWRSelectAll" runat="server" CssClass="labelBlack">全选</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZWRMoveFirst" runat="server" CssClass="labelBlack">最前</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZWRMovePrev" runat="server" CssClass="labelBlack">前页</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZWRMoveNext" runat="server" CssClass="labelBlack">下页</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZWRMoveLast" runat="server" CssClass="labelBlack">最后</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZWRGotoPage" runat="server" CssClass="labelBlack">前往</asp:linkbutton><asp:textbox id="txtWRPageIndex" runat="server" Font-Name="宋体" Font-Size="12px" Height="22px" Width="40px" CssClass="textbox" Columns="2">1</asp:textbox>页</TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZWRSetPageSize" runat="server" CssClass="labelBlack">每页</asp:linkbutton><asp:textbox id="txtWRPageSize" runat="server" Font-Name="宋体" Font-Size="12px" Height="22px" Width="40px" CssClass="textbox" Columns="3">30</asp:textbox>条</TD>
															<TD class="labelBlack" vAlign="middle" align="right" width="140"><asp:label id="lblWRGridLocInfo" runat="server" CssClass="labelBlack">1/10 N/15</asp:label></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
										</TABLE>
									</TD>
									<TD width="5"></TD>
								</TR>
								<TR>
									<TD colSpan="5" height="5"></TD>
								</TR>
							</TABLE>
						</TD>
						<TD width="5"></TD>
					</TR>
					<TR>
						<TD width="5"></TD>
						<TD align="center" style="BORDER-TOP: #99cccc 2px solid">
							<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR vAlign="middle" align="left" height="24">
									<TD vAlign="middle" align="center" width="100"><asp:linkbutton id="lnkMLRoleQXB" runat="server" Font-Name="宋体" Font-Size="12px"><img src="../../images/users.ico" alt="open" border="0" width="16" height="16">角色授权</asp:linkbutton></TD>
									<TD vAlign="middle" align="center" width="100"><asp:linkbutton id="lnkMLUserQXB" runat="server" Font-Name="宋体" Font-Size="12px"><img src="../../images/user.ico" alt="open" border="0" width="16" height="16">用户授权</asp:linkbutton></TD>
									<TD vAlign="middle" align="center" width="100"><asp:linkbutton id="lnkMLAccsQXB" runat="server" Font-Name="宋体" Font-Size="12px" Enabled="False"><img src="../../images/access.ico" alt="open" border="0" width="16" height="16">存取授权</asp:linkbutton></TD>
									<TD vAlign="middle" align="center" width="100"><asp:linkbutton id="lnkMLGrantB" runat="server" Font-Name="宋体" Font-Size="12px"><img src="../../images/grant.ico" alt="open" border="0" width="16" height="16">赋予权限</asp:linkbutton></TD>
									<TD vAlign="middle" align="center" width="100"><asp:linkbutton id="lnkMLRevokeB" runat="server" Font-Name="宋体" Font-Size="12px"><img src="../../images/revoke.ico" alt="open" border="0" width="16" height="16">回收权限</asp:linkbutton></TD>
									<TD vAlign="middle" align="center" width="100"><asp:linkbutton id="lnkMLCloseB" runat="server" Font-Size="12px" Font-Name="宋体"><img src="../../images/CLOSE.GIF" alt="返回上级" border="0" width="16" height="16">返回上级</asp:linkbutton></TD>
									<TD vAlign="middle" align="center" width="100"></TD>
									<TD vAlign="middle" align="center" width="100"></TD>
								</TR>
							</TABLE>
						</TD>
						<TD width="5"></TD>
					</TR>
				</TABLE>
			</asp:panel>
			<asp:Panel id="panelError" Runat="server">
				<TABLE id="tabErrMain" height="98%" cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD width="5%"></TD>
						<TD>
							<TABLE height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD>&nbsp;&nbsp;&nbsp;&nbsp;</TD>
									<TD id="tdErrInfo" style="FONT-SIZE: 32pt; COLOR: black; LINE-HEIGHT: 40pt; FONT-FAMILY: 宋体; LETTER-SPACING: 2pt" align="center"><asp:Label id="lblMessage" Runat="server"></asp:Label><p>&nbsp;&nbsp;</p><p><input type="button" id="btnGoBack" value=" 返回 " style="FONT-SIZE: 24pt; FONT-FAMILY: 宋体" onclick="javascript:history.back();"></p></TD>
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
						<input id="htxtYRQuery" type="hidden" runat="server">
						<input id="htxtYRRows" type="hidden" runat="server">
						<input id="htxtYRSort" type="hidden" runat="server">
						<input id="htxtYRSortColumnIndex" type="hidden" runat="server">
						<input id="htxtYRSortType" type="hidden" runat="server">
						<input id="htxtWRQuery" type="hidden" runat="server">
						<input id="htxtWRRows" type="hidden" runat="server">
						<input id="htxtWRSort" type="hidden" runat="server">
						<input id="htxtWRSortColumnIndex" type="hidden" runat="server">
						<input id="htxtWRSortType" type="hidden" runat="server">
						<input id="htxtDivLeftYR" type="hidden" runat="server">
						<input id="htxtDivTopYR" type="hidden" runat="server">
						<input id="htxtDivLeftWR" type="hidden" runat="server">
						<input id="htxtDivTopWR" type="hidden" runat="server">
						<input id="htxtDivLeftBody" type="hidden" runat="server">
						<input id="htxtDivTopBody" type="hidden" runat="server">
					</td>
				</tr>
				<tr>
					<td>
						<script language="javascript">
							function ScrollProc_Body() {
								var oText;
								oText=null;
								oText=document.getElementById("htxtDivTopBody");
								if (oText != null) oText.value = document.body.scrollTop;
								oText=null;
								oText=document.getElementById("htxtDivLeftBody");
								if (oText != null) oText.value = document.body.scrollLeft;
								return;
							}
							function ScrollProc_DivObject() {
								var oText;
								oText=null;
								oText=document.getElementById("htxtDivTopYR");
								if (oText != null) oText.value = divYR.scrollTop;
								oText=null;
								oText=document.getElementById("htxtDivLeftYR");
								if (oText != null) oText.value = divYR.scrollLeft;
								return;
							}
							function ScrollProc_DivWsObject() {
								var oText;
								oText=null;
								oText=document.getElementById("htxtDivTopWR");
								if (oText != null) oText.value = divWR.scrollTop;
								oText=null;
								oText=document.getElementById("htxtDivLeftWR");
								if (oText != null) oText.value = divWR.scrollLeft;
								return;
							}
							try {
								var Text;

								oText=null;
								oText=document.getElementById("htxtDivTopBody");
								if (oText != null) document.body.scrollTop = oText.value;
								oText=null;
								oText=document.getElementById("htxtDivLeftBody");
								if (oText != null) document.body.scrollLeft = oText.value;

								oText=null;
								oText=document.getElementById("htxtDivTopYR");
								if (oText != null) divYR.scrollTop = oText.value;
								oText=null;
								oText=document.getElementById("htxtDivLeftYR");
								if (oText != null) divYR.scrollLeft = oText.value;

								oText=null;
								oText=document.getElementById("htxtDivTopWR");
								if (oText != null) divWR.scrollTop = oText.value;
								oText=null;
								oText=document.getElementById("htxtDivLeftWR");
								if (oText != null) divWR.scrollLeft = oText.value;

								document.body.onscroll = ScrollProc_Body;
								divYR.onscroll = ScrollProc_DivObject;
								divWR.onscroll = ScrollProc_DivWsObject;
							}
							catch (e) {}
						</script>
					</td>
				</tr>
				<tr>
					<td>
						<script language="javascript">window_onresize();</script>
						<uwin:popmessage id="popMessageObject" runat="server" EnableViewState="False" PopupWindowType="Normal" ActionType="OpenWindow" Visible="False" height="48px" width="96px"></uwin:popmessage>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>