<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="xtpz_bdkz.aspx.vb" Inherits=" Xydc.Platform.web.xtpz_bdkz" %>
<%@ Register TagPrefix="uwin" Namespace="Josco.Web" Assembly="Josco.Web.PopMessage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>补登领导批示控制处理窗</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../filecss/styles01.css" type="text/css" rel="stylesheet">
		<style>
			TD.grdBDKZLocked { ; LEFT: expression(divBDKZ.scrollLeft); POSITION: relative }
			TH.grdBDKZLocked { ; LEFT: expression(divBDKZ.scrollLeft); POSITION: relative }
			TH.grdBDKZLocked { Z-INDEX: 99 }
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
				
				if (document.all("divBDKZ") == null)
					return;
				
				dblHeight = 430 + dblDeltaY + document.body.clientHeight - 570; //default state : 430px
				strHeight = parseInt(dblHeight.toString(), 10).toString() + "px";
				dblWidth  = 520 + dblDeltaX + document.body.clientWidth  - 850; //default state : 520px
				strWidth = parseInt(dblWidth.toString(), 10).toString() + "px";
				divBDKZ.style.width  = strWidth;
				divBDKZ.style.height = strHeight;
				divBDKZ.style.clip = "rect(0px " + strWidth + " " + strHeight + " 0px)";
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
		<form id="frmXTPZ_BDKZ" method="post" runat="server">
			<asp:panel id="panelMain" Runat="server">
				<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD width="3"></TD>
						<TD vAlign="top" align="center">
							<TABLE cellSpacing="0" cellPadding="0" border="0">
								<TR>
									<TD class="title" align="left" colSpan="3" height="24" align="center"><asp:LinkButton id="lnkBlank" Runat="server" Width="0px" Height="5px"></asp:LinkButton></TD>
									<TD width="3"></TD>
								</TR>
								<TR>
									<TD width="3"></TD>
									<TD vAlign="top">
										<TABLE cellSpacing="0" cellPadding="0" border="0">
											<TR align="center">
												<TD class="label" align="left">
													<TABLE cellSpacing="0" cellPadding="0" border="0">
														<TR>
															<TD class="label" vAlign="middle" noWrap align="right">补登人职务</TD>
															<TD class="label" align="left"><asp:textbox id="txtBDKZSearch_ZWMC" runat="server" Font-Size="12px" CssClass="textbox" Columns="12" Font-Names="宋体"></asp:textbox></TD>
															<TD class="label" vAlign="middle" noWrap align="right">&nbsp;&nbsp;补登范围</TD>
															<TD class="label" align="left"><asp:textbox id="txtBDKZSearch_BDFW" runat="server" Font-Size="12px" CssClass="textbox" Columns="16" Font-Names="宋体"></asp:textbox></TD>
															<TD class="label" vAlign="middle" noWrap align="right">&nbsp;&nbsp;补充说明</TD>
															<TD class="label" align="left"><asp:textbox id="txtBDKZSearch_BCSM" runat="server" Font-Size="12px" CssClass="textbox" Columns="16" Font-Names="宋体"></asp:textbox></TD>
															<TD class="label"><asp:button id="btnBDKZQuery" Runat="server" Font-Name="宋体" Font-Size="12px" CssClass="button" Text="搜索"></asp:button></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
											<TR>
												<TD>
													<DIV id="divBDKZ" style="BORDER-RIGHT: #99cccc 1px solid; TABLE-LAYOUT: fixed; BORDER-TOP: #99cccc 1px solid; OVERFLOW: auto; BORDER-LEFT: #99cccc 1px solid; WIDTH: 520px; CLIP: rect(0px 520px 430px 0px); BORDER-BOTTOM: #99cccc 1px solid; HEIGHT: 430px">
														<asp:datagrid id="grdBDKZ" runat="server" Font-Size="12px" CssClass="label" Font-Names="宋体" UseAccessibleHeader="True"
															AutoGenerateColumns="False" GridLines="Vertical" BackColor="White" BorderStyle="None" CellPadding="4"
															AllowPaging="True" PageSize="30" BorderColor="#DEDFDE" BorderWidth="0px" AllowSorting="True" Width="760px">
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
																		<asp:CheckBox id="chkBDKZ" runat="server" AutoPostBack="False"></asp:CheckBox>
																	</ItemTemplate>
																</asp:TemplateColumn>
																<asp:ButtonColumn Visible="False" DataTextField="岗位代码" SortExpression="岗位代码" HeaderText="岗位代码" CommandName="Select">
																	<HeaderStyle Width="0px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn DataTextField="岗位名称" SortExpression="岗位名称" HeaderText="督办人职务" CommandName="Select">
																	<HeaderStyle Width="120px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn Visible="False" DataTextField="补登范围" SortExpression="补登范围" HeaderText="补登范围代码" CommandName="Select">
																	<HeaderStyle Width="0px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn DataTextField="补登范围名称" SortExpression="补登范围名称" HeaderText="补登范围" CommandName="Select">
																	<HeaderStyle Width="200px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn DataTextField="职务列表" SortExpression="职务列表" HeaderText="职务列表" CommandName="Select">
																	<HeaderStyle Width="200px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn Visible="False" DataTextField="级数限制" SortExpression="级数限制" HeaderText="级数限制代码" CommandName="Select">
																	<HeaderStyle Width="0px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn DataTextField="级数限制名称" SortExpression="级数限制名称" HeaderText="补登范围补充说明" CommandName="Select">
																	<HeaderStyle Width="200px"></HeaderStyle>
																</asp:ButtonColumn>
															</Columns>
															<PagerStyle Visible="False" NextPageText="下页" Font-Size="12px" Font-Names="宋体" PrevPageText="上页" HorizontalAlign="Right" ForeColor="Black" Position="TopAndBottom" BackColor="SkyBlue"></PagerStyle>
														</asp:datagrid><INPUT id="htxtBDKZFixed" type="hidden" value="0" runat="server"></DIV>
												</TD>
											</TR>
											<TR align="center">
												<TD class="label">
													<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
														<TR align="center">
															<TD class="label" vAlign="middle"><asp:linkbutton id="lnkCZBDKZDeSelectAll" runat="server" CssClass="labelBlack">不选</asp:linkbutton></TD>
															<TD class="label" vAlign="middle"><asp:linkbutton id="lnkCZBDKZSelectAll" runat="server" CssClass="labelBlack">全选</asp:linkbutton></TD>
															<TD class="label" vAlign="middle"><asp:linkbutton id="lnkCZBDKZMoveFirst" runat="server" CssClass="labelBlack">最前</asp:linkbutton></TD>
															<TD class="label" vAlign="middle"><asp:linkbutton id="lnkCZBDKZMovePrev" runat="server" CssClass="labelBlack">前页</asp:linkbutton></TD>
															<TD class="label" vAlign="middle"><asp:linkbutton id="lnkCZBDKZMoveNext" runat="server" CssClass="labelBlack">下页</asp:linkbutton></TD>
															<TD class="label" vAlign="middle"><asp:linkbutton id="lnkCZBDKZMoveLast" runat="server" CssClass="labelBlack">最后</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" noWrap><asp:linkbutton id="lnkCZBDKZGotoPage" runat="server" CssClass="labelBlack">前往</asp:linkbutton><asp:textbox id="txtBDKZPageIndex" runat="server" Font-Name="宋体" Font-Size="12px" CssClass="textbox" Columns="2">1</asp:textbox>页</TD>
															<TD class="labelBlack" vAlign="middle" noWrap><asp:linkbutton id="lnkCZBDKZSetPageSize" runat="server" CssClass="labelBlack">每页</asp:linkbutton><asp:textbox id="txtBDKZPageSize" runat="server" Font-Name="宋体" Font-Size="12px" CssClass="textbox" Columns="3">30</asp:textbox>条</TD>
															<TD class="labelBlack" vAlign="middle" align="right"><asp:label id="lblBDKZGridLocInfo" runat="server" CssClass="labelBlack">1/10 N/15</asp:label></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
											<TR>
												<TD height="3"></TD>
											</TR>
											<TR>
												<TD align="center">
													<asp:Button id="btnBDKZAddNew" Runat="server" Font-Name="宋体" Font-Size="12px" CssClass="button" Text=" 新增设定 "></asp:Button>
													<asp:Button id="btnBDKZModify" Runat="server" Font-Name="宋体" Font-Size="12px" CssClass="button" Text=" 修改设定 "></asp:Button>
													<asp:Button id="btnBDKZDelete" Runat="server" Font-Name="宋体" Font-Size="12px" CssClass="button" Text=" 删除设定 "></asp:Button>
													<asp:Button id="btnBDKZSearch" Runat="server" Font-Name="宋体" Font-Size="12px" CssClass="button" Text=" 全文检索 "></asp:Button>
													<asp:Button id="btnClose" Runat="server" Font-Name="宋体" Font-Size="12px" CssClass="button" Text=" 返  回 "></asp:Button>
									
												</TD>
											</TR>
											<TR>
												<TD height="3"></TD>
											</TR>
										</TABLE>
									</TD>
									<TD width="6"></TD>
									<TD vAlign="top">
										<TABLE cellSpacing="0" cellPadding="0" border="0">
											<TR>
												<TD class="title" style="BORDER-BOTTOM: #99cccc 1px solid" align="center" height="30"><B>设置信息查看与编辑窗</B></TD>
											</TR>
											<TR>
												<TD class="label" align="center" height="10"></TD>
											</TR>
											<TR>
												<TD class="labelNotNull" align="left">补登人职务：</TD>
											</TR>
											<TR>
												<TD class="label" align="left"><asp:textbox id="txtZWMC" Runat="server" Width="250px" Font-Name="宋体" Font-Size="12px" CssClass="textbox"></asp:textbox><asp:LinkButton id="lnkCZSelectZW" Runat="server" CssClass="button"><img src="../../images/glist.gif" border="0" width="16" height="19" align="absmiddle"></asp:LinkButton><INPUT id="htxtZWDM" type="hidden" runat="server"></TD>
											</TR>
											<TR>
												<TD class="label" align="center" height="10"></TD>
											</TR>
											<TR>
												<TD class="labelNotNull" align="left">补登范围：</TD>
											</TR>
											<TR>
												<TD class="label" align="left" width="270">
													<asp:DropDownList id="ddlBDFW" Runat="server" Width="100%" Font-Name="宋体" Font-Size="12px">
														<asp:ListItem Value="0">不限制</asp:ListItem>
														<asp:ListItem Value="1">可以补登所有指定职务列表中的处理意见</asp:ListItem>
														<asp:ListItem Value="2">可以补登本部门指定职务列表中的处理意见</asp:ListItem>
													</asp:DropDownList></TD>
											</TR>
											<TR>
												<TD class="label" align="center" height="10"></TD>
											</TR>
											<TR>
												<TD class="labelNotNull" align="left">被补登领导对应的职务：</TD>
											</TR>
											<TR>
												<TD class="label" align="center" height="10"></TD>
											</TR>
											<TR>
												<TD class="label" align="left" valign="top"><asp:TextBox id="txtZWLB" Runat="server" Width="250px" Font-Name="宋体" Font-Size="12px" TextMode="MultiLine" Rows="6" Height="160px"></asp:TextBox><asp:LinkButton id="lnkCZSelectZWLIST" Runat="server" CssClass="button"><img src="../../images/glist.gif" border="0" width="16" height="19" align="top"></asp:LinkButton></TD>
											</TR>
											<TR>
												<TD class="labelNotNull" align="left">补登范围补充说明：</TD>
											</TR>
											<TR>
												<TD class="label" align="left">
													<asp:DropDownList id="ddlBCSM" Runat="server" Width="100%" Font-Name="宋体" Font-Size="12px">
														<asp:ListItem Value="1">限一级单位以下</asp:ListItem>
														<asp:ListItem Value="2">限二级单位以下</asp:ListItem>
														<asp:ListItem Value="3">限三级单位以下</asp:ListItem>
														<asp:ListItem Value="4">限四级单位以下</asp:ListItem>
														<asp:ListItem Value="5">限五级单位以下</asp:ListItem>
														<asp:ListItem Value="6">限六级单位以下</asp:ListItem>
													</asp:DropDownList></TD>
											</TR>
											<TR>
												<TD class="label" align="center" height="10"></TD>
											</TR>
											<TR>
												<TD class="label" align="center">
													<asp:button id="btnSave" Runat="server" Width="96px" Height="24px" Font-Name="宋体" Font-Size="12px" CssClass="button" Text="保存"></asp:button>
													<asp:button id="btnCancel" Runat="server" Width="96px" Height="24px" Font-Name="宋体" Font-Size="12px" CssClass="button" Text="取消"></asp:button>
												</TD>
											</TR>
											<TR>
												<TD class="label" align="center" height="3"></TD>
											</TR>
										</TABLE>
									</TD>
									<TD width="3"></TD>
								</TR>
								<TR>
									<TD colSpan="3" height="3"></TD>
								</TR>
							</TABLE>
						</TD>
						<TD width="3"></TD>
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
						<input id="htxtSessionIdBDKZQuery" type="hidden" runat="server">
						<input id="htxtCurrentPage" type="hidden" runat="server">
						<input id="htxtCurrentRow" type="hidden" runat="server">
						<input id="htxtEditMode" type="hidden" runat="server">
						<input id="htxtEditType" type="hidden" runat="server">
						<input id="htxtBDKZQuery" type="hidden" runat="server">
						<input id="htxtBDKZRows" type="hidden" runat="server">
						<input id="htxtBDKZSort" type="hidden" runat="server">
						<input id="htxtBDKZSortColumnIndex" type="hidden" runat="server">
						<input id="htxtBDKZSortType" type="hidden" runat="server">
						<input id="htxtDivLeftBDKZ" type="hidden" runat="server">
						<input id="htxtDivTopBDKZ" type="hidden" runat="server">
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
							function ScrollProc_divBDKZ() {
								var oText;
								oText=null;
								oText=document.getElementById("htxtDivTopBDKZ");
								if (oText != null) oText.value = divBDKZ.scrollTop;
								oText=null;
								oText=document.getElementById("htxtDivLeftBDKZ");
								if (oText != null) oText.value = divBDKZ.scrollLeft;
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
								oText=document.getElementById("htxtDivTopBDKZ");
								if (oText != null) divBDKZ.scrollTop = oText.value;
								oText=null;
								oText=document.getElementById("htxtDivLeftBDKZ");
								if (oText != null) divBDKZ.scrollLeft = oText.value;

								document.body.onscroll = ScrollProc_Body;
								divBDKZ.onscroll = ScrollProc_divBDKZ;
							}
							catch (e) {}
						</script>
					</td>
				</tr>
				<tr>
					<td>
						<script language="javascript">window_onresize();</script>
						<uwin:popmessage id="popMessageObject" runat="server" height="48px" width="96px" Visible="False" ActionType="OpenWindow" PopupWindowType="Normal" EnableViewState="False"></uwin:popmessage>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>