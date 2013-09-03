<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="xtgl_rz_aq.aspx.vb" Inherits="Xydc.Platform.web.xtgl_rz_aq" %>
<%@ Register TagPrefix="uwin" Namespace="Josco.Web" Assembly="Josco.Web.PopMessage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>安全管理员审计日志</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../filecss/styles01.css" type="text/css" rel="stylesheet">
		<style>
			TD.grdLOGLocked { ; LEFT: expression(divLOG.scrollLeft); POSITION: relative }
			TH.grdLOGLocked { ; LEFT: expression(divLOG.scrollLeft); POSITION: relative }
			TH.grdLOGLocked { Z-INDEX: 99 }
			TH { Z-INDEX: 10; POSITION: relative }
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
				
				if (document.all("divLOG") == null)
					return;
				
				dblHeight = 420 + dblDeltaY + document.body.clientHeight - 570; //default state : 420px
				strHeight = parseInt(dblHeight.toString(), 10).toString() + "px";
				dblWidth  = 800 + dblDeltaX + document.body.clientWidth  - 850; //default state : 800px
				strWidth = parseInt(dblWidth.toString(), 10).toString() + "px";
				divLOG.style.width  = strWidth;
				divLOG.style.height = strHeight;
				divLOG.style.clip   = "rect(0px " + strWidth + " " + strHeight + " 0px)";
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
		<form id="frmXTGL_RZ_AQ" method="post" runat="server">
			<asp:panel id="panelMain" Runat="server">
				<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
					<TR>
						<TD width="5"></TD>
						<TD vAlign="top" align="center">
							<TABLE cellSpacing="0" cellPadding="0" border="0">
								<TR>
									<TD class="title" align="center" colSpan="3" height="30">安全管理员审计日志<asp:LinkButton id="lnkBlank" Runat="server" Width="0px"></asp:LinkButton></TD>
								</TR>
								<TR>
									<TD width="5"></TD>
									<TD vAlign="top" style="BORDER-RIGHT: #99cccc 1px solid; BORDER-TOP: #99cccc 1px solid; BORDER-LEFT: #99cccc 1px solid; BORDER-BOTTOM: #99cccc 1px solid">
										<TABLE cellSpacing="0" cellPadding="0" border="0">
											<TR>
												<TD class="label" align="left">
													<TABLE cellSpacing="0" cellPadding="0" border="0">
														<TR>
															<TD class="label" vAlign="middle">用户标识&nbsp;</TD>
															<TD class="label" align="left"><asp:textbox id="txtLOGSearch_YHBS" runat="server" Font-Size="12px" CssClass="textbox" Columns="16" Font-Names="宋体"></asp:textbox></TD>
															<TD class="label" vAlign="middle">&nbsp;&nbsp;操作描述&nbsp;</TD>
															<TD class="label" align="left"><asp:textbox id="txtLOGSearch_CZMS" runat="server" Font-Size="12px" CssClass="textbox" Columns="30" Font-Names="宋体"></asp:textbox></TD>
															<TD class="label" vAlign="middle">&nbsp;&nbsp;操作时间&nbsp;</TD>
															<TD class="label" align="left"><asp:textbox id="txtLOGSearch_CZSJMin" runat="server" Font-Size="12px" CssClass="textbox" Columns="19" Font-Names="宋体"></asp:textbox>~<asp:textbox id="txtLOGSearch_CZSJMax" runat="server" Font-Size="12px" CssClass="textbox" Columns="19" Font-Names="宋体"></asp:textbox></TD>
															<TD class="label">&nbsp;&nbsp;<asp:button id="btnLOGSearch" Runat="server" Font-Size="12px" Font-Name="宋体" CssClass="button" Text="搜索"></asp:button></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
											<TR>
												<TD>
													<DIV id="divLOG" style="TABLE-LAYOUT: fixed; OVERFLOW: auto; WIDTH: 800px; CLIP: rect(0px 800px 420px 0px); HEIGHT: 420px">
														<asp:datagrid id="grdLOG" runat="server" Font-Size="12px" CssClass="label" Font-Names="宋体" CellPadding="4"
															AllowSorting="True" BorderWidth="0px" BorderColor="#DEDFDE" PageSize="30" BorderStyle="None"
															BackColor="White" GridLines="Vertical" AutoGenerateColumns="False" AllowPaging="True" UseAccessibleHeader="True">
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
																		<asp:CheckBox id="chkLOG" runat="server" AutoPostBack="False"></asp:CheckBox>
																	</ItemTemplate>
																</asp:TemplateColumn>
																<asp:ButtonColumn DataTextField="opuser" SortExpression="opuser" HeaderText="用户标识" CommandName="Select">
																	<HeaderStyle Width="120px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn DataTextField="optime" SortExpression="optime" HeaderText="操作时间" CommandName="Select" DataTextFormatString="{0:yyyy-MM-dd HH:mm:ss}">
																	<HeaderStyle Width="160px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn DataTextField="opaddr" SortExpression="opaddr" HeaderText="主机地址" CommandName="Select">
																	<HeaderStyle Width="140px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn DataTextField="opmach" SortExpression="opmach" HeaderText="主机名称" CommandName="Select">
																	<HeaderStyle Width="200px"></HeaderStyle>
																</asp:ButtonColumn>
																<asp:ButtonColumn DataTextField="opnote" SortExpression="opnote" HeaderText="操作描述" CommandName="Select">
																	<HeaderStyle Width="600px"></HeaderStyle>
																</asp:ButtonColumn>
															</Columns>
															<PagerStyle Visible="False" NextPageText="下页" Font-Size="12px" Font-Names="宋体" PrevPageText="上页" HorizontalAlign="Right" ForeColor="Black" Position="TopAndBottom" BackColor="SkyBlue"></PagerStyle>
														</asp:datagrid><INPUT id="htxtLOGFixed" type="hidden" value="0" runat="server"></DIV>
												</TD>
											</TR>
											<TR>
												<TD class="label">
													<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
														<TR>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZLOGDeSelectAll" runat="server" CssClass="labelBlack">不选</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZLOGSelectAll" runat="server" CssClass="labelBlack">全选</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZLOGMoveFirst" runat="server" CssClass="labelBlack">最前</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZLOGMovePrev" runat="server" CssClass="labelBlack">前页</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZLOGMoveNext" runat="server" CssClass="labelBlack">下页</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZLOGMoveLast" runat="server" CssClass="labelBlack">最后</asp:linkbutton></TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZLOGGotoPage" runat="server"  CssClass="labelBlack">前往</asp:linkbutton><asp:textbox id="txtLOGPageIndex" runat="server" Font-Size="12px" Font-Name="宋体" CssClass="textbox" Columns="3">1</asp:textbox>页</TD>
															<TD class="labelBlack" vAlign="middle" align="left"><asp:linkbutton id="lnkCZLOGSetPageSize" runat="server" CssClass="labelBlack">每页</asp:linkbutton><asp:textbox id="txtLOGPageSize" runat="server" Font-Size="12px" Font-Name="宋体" CssClass="textbox" Columns="3">30</asp:textbox>条</TD>
															<TD class="labelBlack" vAlign="middle" noWrap align="right"><asp:label id="lblLOGGridLocInfo" runat="server" Font-Size="12px" CssClass="labelBlack">1/10 N/15</asp:label></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
										</TABLE>
									</TD>
									<TD width="5"></TD>
								</TR>
								<TR>
									<TD colSpan="5" height="3"></TD>
								</TR>
							</TABLE>
						</TD>
						<TD width="5"></TD>
					</TR>
					<TR>
						<TD colSpan="3" height="3"></TD>
					</TR>
					<TR>
						<TD align="center" colSpan="3">
							<asp:Button id="btnPrint" Runat="server" Font-Size="12px" Font-Name="宋体" CssClass="button" Text=" 打    印 " Height="36px"></asp:Button>
							<asp:Button id="btnClose" Runat="server" Font-Size="12px" Font-Name="宋体" CssClass="button" Text=" 返    回 " Height="36px"></asp:Button>
						</TD>
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
									<TD id="tdErrInfo" style="FONT-SIZE: 32pt; COLOR: black; LINE-HEIGHT: 40pt; FONT-FAMILY: 宋体; LETTER-SPACING: 2pt" align="center"><asp:Label id="lblMessage" Runat="server"></asp:Label><P>&nbsp;&nbsp;</P><P><INPUT id="btnGoBack" style="FONT-SIZE: 24pt; FONT-FAMILY: 宋体" onclick="javascript:history.back();" type="button" value=" 返回 "></P></TD>
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
						<input id="htxtSessionIdQuery" type="hidden" runat="server">
						<input id="htxtLOGQuery" type="hidden" runat="server">
						<input id="htxtLOGRows" type="hidden" runat="server">
						<input id="htxtLOGSort" type="hidden" runat="server">
						<input id="htxtLOGSortColumnIndex" type="hidden" runat="server">
						<input id="htxtLOGSortType" type="hidden" runat="server">
						<input id="htxtDivLeftLOG" type="hidden" runat="server">
						<input id="htxtDivTopLOG" type="hidden" runat="server">
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
							function ScrollProc_divLOG() {
								var oText;
								oText=null;
								oText=document.getElementById("htxtDivTopLOG");
								if (oText != null) oText.value = divLOG.scrollTop;
								oText=null;
								oText=document.getElementById("htxtDivLeftLOG");
								if (oText != null) oText.value = divLOG.scrollLeft;
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
								oText=document.getElementById("htxtDivTopLOG");
								if (oText != null) divLOG.scrollTop = oText.value;
								oText=null;
								oText=document.getElementById("htxtDivLeftLOG");
								if (oText != null) divLOG.scrollLeft = oText.value;

								document.body.onscroll = ScrollProc_Body;
								divLOG.onscroll = ScrollProc_divLOG;
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