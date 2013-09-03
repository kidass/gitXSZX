<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="xtgl_mkgl.aspx.vb" Inherits="Xydc.Platform.web.xtgl_mkgl" %>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls" %>
<%@ Register TagPrefix="uwin" Namespace="Josco.Web" Assembly="Josco.Web.PopMessage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>应用模块管理窗</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../filecss/styles01.css" type="text/css" rel="stylesheet">
		<style>
			TD.grdObjectLocked { ; LEFT: expression(divObject.scrollLeft); POSITION: relative }
			TH.grdObjectLocked { ; LEFT: expression(divObject.scrollLeft); POSITION: relative }
			TH { Z-INDEX: 10; POSITION: relative }
			TH.grdObjectLocked { Z-INDEX: 99 }
		</style>
		<script src="../../scripts/transkey.js"></script>
		<script language="javascript">
		<!--
			function window_onresize() 
			{
				var dblHeight = 0;
				var dblWidth  = 0;
				var strHeight = "";
				var strWidth  = "";
				var dblDeltaY = 20;
				var dblDeltaX = 0;
				
				if (document.all("divObject") == null)
					return;
				
				dblHeight = 410 + dblDeltaY + document.body.clientHeight - 570; //default state : 410px
				strHeight = parseInt(dblHeight.toString(), 10).toString() + "px";
				dblWidth  = 540 + dblDeltaX + document.body.clientWidth  - 850; //default state : 540px
				strWidth = parseInt(dblWidth.toString(), 10).toString() + "px";
				divObject.style.width  = strWidth;
				divObject.style.height = strHeight;
				divObject.style.clip   = "rect(0px " + strWidth + " " + strHeight + " 0px)";
				
				var objTreeView = null;
				dblHeight = 460 + dblDeltaY + document.body.clientHeight - 570; //default state : 460px
				strHeight = parseInt(dblHeight.toString(), 10).toString() + "px";
				objTreeView = document.getElementById("tvwObject");
				if (objTreeView)
					objTreeView.style.height = strHeight;
			}
			function document_onreadystatechange() 
			{
				return window_onresize();
			}
		//-->
		</script>
		<script language="javascript" for="document" event="onreadystatechange">
		<!--
			return document_onreadystatechange()
		//-->
		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0" onresize="return window_onresize()" background="../../images/oabk.gif">
		<form id="frmMKGL" method="post" runat="server">
			<asp:Panel id="panelMain" Runat="server">
				<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD colSpan="3" height="5"></TD>
					</TR>
					<TR>
						<TD width="5"></TD>
						<TD align="center" style="BORDER-BOTTOM: #99cccc 2px solid">
							<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR vAlign="middle" align="left" height="24">
									<TD vAlign="middle" align="center" width="100"><asp:linkbutton id="lnkMLSelect" runat="server" Font-Name="宋体" Font-Size="12px"><img src="../../images/OPEN.GIF" alt="open" border="0" width="16" height="16">查看模块</asp:linkbutton></TD>
									<TD vAlign="middle" align="center" width="100"><asp:linkbutton id="lnkMLAddNewTJ" runat="server" Font-Name="宋体" Font-Size="12px"><img src="../../images/NEW.GIF" alt="open" border="0" width="16" height="16">增加同级</asp:linkbutton></TD>
									<TD vAlign="middle" align="center" width="100"><asp:linkbutton id="lnkMLAddNewXJ" runat="server" Font-Name="宋体" Font-Size="12px"><img src="../../images/NEW.GIF" alt="open" border="0" width="16" height="16">增加下级</asp:linkbutton></TD>
									<TD vAlign="middle" align="center" width="100"><asp:linkbutton id="lnkMLUpdate" runat="server" Font-Name="宋体" Font-Size="12px"><img src="../../images/MODIFY.ICO" alt="open" border="0" width="16" height="16">修改模块</asp:linkbutton></TD>
									<TD vAlign="middle" align="center" width="100"><asp:linkbutton id="lnkMLDelete" runat="server" Font-Name="宋体" Font-Size="12px"><img src="../../images/DELETE.GIF" alt="open" border="0" width="16" height="16">删除模块</asp:linkbutton></TD>
									<TD vAlign="middle" align="center" width="100"><asp:linkbutton id="lnkMLRefresh" runat="server" Font-Name="宋体" Font-Size="12px"><img src="../../images/REFRESH.ICO" alt="open" border="0" width="16" height="16">刷新数据</asp:linkbutton></TD>
									<TD vAlign="middle" align="center" width="100"><asp:linkbutton id="lnkMLClose" runat="server" Font-Size="12px" Font-Name="宋体"><img src="../../images/CLOSE.GIF" alt="返回上级" border="0" width="16" height="16">返回上级</asp:linkbutton></TD>
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
												<TD><iewc:treeview id="tvwObject" runat="server" Font-Name="宋体" Font-Size="12px" Height="460px" Width="260px" AutoPostBack="True" CssClass="label"></iewc:treeview></TD>
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
															<TD class="label" vAlign="middle">代码&nbsp;</TD>
															<TD class="label" align="left"><asp:textbox id="txtSearchDM" runat="server" Font-Name="宋体" Font-Size="12px" Height="22px" CssClass="textbox" Columns="20"></asp:textbox></TD>
															<TD class="label" vAlign="middle">&nbsp;&nbsp;名称&nbsp;</TD>
															<TD class="label" align="left"><asp:textbox id="txtSearchMC" runat="server" Font-Name="宋体" Font-Size="12px" Height="22px" CssClass="textbox" Columns="30"></asp:textbox></TD>
															<TD class="label" vAlign="middle">&nbsp;&nbsp;级别&nbsp;</TD>
															<TD class="label" align="left"><asp:textbox id="txtSearchJBMin" runat="server" Font-Name="宋体" Font-Size="12px" Height="22px" CssClass="textbox" Columns="2"></asp:textbox>~<asp:textbox id="txtSearchJBMax" runat="server" Font-Name="宋体" Font-Size="12px" Height="22px" CssClass="textbox" Columns="2"></asp:textbox></TD>
															<TD class="label" vAlign="middle">&nbsp;&nbsp;说明&nbsp;</TD>
															<TD class="label" align="left"><asp:textbox id="txtSearchSM" runat="server" Font-Name="宋体" Font-Size="12px" Height="22px" CssClass="textbox" Columns="20"></asp:textbox></TD>
															<TD class="label">&nbsp;&nbsp;<asp:button id="btnSearch" Runat="server" Font-Name="宋体" Font-Size="12px" CssClass="button" Text="搜索"></asp:button></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
											<TR>
												<TD>
													<DIV id="divObject" style="TABLE-LAYOUT: fixed; OVERFLOW: auto; WIDTH: 540px; CLIP: rect(0px 540px 410px 0px); HEIGHT: 410px">
														<asp:datagrid id="grdObject" runat="server" Font-Size="12px" CssClass="label" AutoGenerateColumns="False"
															GridLines="Vertical" Font-Names="宋体" BackColor="White" BorderStyle="None" CellPadding="4"
															AllowPaging="True" PageSize="30" BorderColor="#DEDFDE" BorderWidth="0px" UseAccessibleHeader="True"
															AllowSorting="True">
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
																		<asp:CheckBox id="chkObject" runat="server" AutoPostBack="False"></asp:CheckBox>
																	</ItemTemplate>
																</asp:TemplateColumn>
																<asp:ButtonColumn Visible="False" DataTextField="模块标识" SortExpression="模块标识" HeaderText="模块标识" CommandName="Select">
																	<HeaderStyle Width="0px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn DataTextField="模块代码" SortExpression="模块代码" HeaderText="代码" CommandName="Select">
																	<HeaderStyle Width="100px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn DataTextField="模块名称" SortExpression="模块名称" HeaderText="名称" CommandName="Select">
																	<HeaderStyle Width="540px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn DataTextField="模块级别" SortExpression="模块级别" HeaderText="级别" CommandName="Select">
																	<HeaderStyle Width="60px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn Visible="False" DataTextField="本级代码" SortExpression="本级代码" HeaderText="本级代码" CommandName="Select">
																	<HeaderStyle Width="0px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn Visible="False" DataTextField="顶级模块" SortExpression="顶级模块" HeaderText="顶级模块" CommandName="Select">
																	<HeaderStyle Width="0px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn Visible="False" DataTextField="上级模块" SortExpression="上级模块" HeaderText="上级模块" CommandName="Select">
																	<HeaderStyle Width="0px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn DataTextField="说明" SortExpression="说明" HeaderText="说明" CommandName="Select">
																	<HeaderStyle Width="300px"></HeaderStyle>
																</asp:ButtonColumn>
															</Columns>
															<PagerStyle Visible="False" NextPageText="下页" Font-Size="12px" Font-Names="宋体" PrevPageText="上页" HorizontalAlign="Right" ForeColor="Black" Position="TopAndBottom" BackColor="SkyBlue"></PagerStyle>
														</asp:datagrid><INPUT id="htxtObjectFixed" type="hidden" value="0" runat="server"></DIV>
												</TD>
											</TR>
											<TR align="center">
												<TD class="label">
													<TABLE cellSpacing="0" cellPadding="0" border="0" width="100%">
														<TR>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZDeSelectAll" runat="server" CssClass="labelBlack">不选</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZSelectAll" runat="server" CssClass="labelBlack">全选</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZMoveFirst" runat="server" CssClass="labelBlack">最前</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZMovePrev" runat="server" CssClass="labelBlack">前页</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZMoveNext" runat="server" CssClass="labelBlack">下页</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZMoveLast" runat="server" CssClass="labelBlack">最后</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZGotoPage" runat="server" CssClass="labelBlack">前往</asp:linkbutton><asp:textbox id="txtPageIndex" runat="server" Font-Name="宋体" Font-Size="12px" Height="22px" Width="40px" CssClass="textbox" Columns="2">1</asp:textbox>页</TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZSetPageSize" runat="server" CssClass="labelBlack">每页</asp:linkbutton><asp:textbox id="txtPageSize" runat="server" Font-Name="宋体" Font-Size="12px" Height="22px" Width="40px" CssClass="textbox" Columns="3">30</asp:textbox>条</TD>
															<TD class="labelBlack" vAlign="middle" align="right" width="140"><asp:label id="lblGridLocInfo" runat="server" CssClass="labelBlack">1/10 N/15</asp:label></TD>
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
				</TABLE>
			</asp:Panel>
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
						<input id="htxtObjectQuery" type="hidden" runat="server">
						<input id="htxtObjectRows" type="hidden" runat="server">
						<input id="htxtObjectSort" type="hidden" runat="server">
						<input id="htxtObjectSortColumnIndex" type="hidden" runat="server">
						<input id="htxtObjectSortType" type="hidden" runat="server">
						<input id="htxtDivLeftObject" type="hidden" runat="server">
						<input id="htxtDivTopObject" type="hidden" runat="server">
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
								oText=document.getElementById("htxtDivTopObject");
								if (oText != null) oText.value = divObject.scrollTop;
								oText=null;
								oText=document.getElementById("htxtDivLeftObject");
								if (oText != null) oText.value = divObject.scrollLeft;
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
								oText=document.getElementById("htxtDivTopObject");
								if (oText != null) divObject.scrollTop = oText.value;
								oText=null;
								oText=document.getElementById("htxtDivLeftObject");
								if (oText != null) divObject.scrollLeft = oText.value;

								document.body.onscroll = ScrollProc_Body;
								divObject.onscroll = ScrollProc_DivObject;
							}
							catch (e) {}
						</script>
					</td>
				</tr>
				<tr>
					<td>
						<script language="javascript">window_onresize();</script>
						<uwin:popmessage id="popMessageObject" runat="server" width="96px" height="48px" Visible="False" ActionType="OpenWindow" PopupWindowType="Normal" EnableViewState="False"></uwin:popmessage>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
